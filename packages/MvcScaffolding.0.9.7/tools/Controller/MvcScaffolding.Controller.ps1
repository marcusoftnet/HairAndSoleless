[T4Scaffolding.Scaffolder(Description = "Allows you to modify the T4 template rendered by a scaffolder")][CmdletBinding()]
param(     
	[parameter(Mandatory = $true, ValueFromPipelineByPropertyName = $true)][string]$ControllerName,   
	[string]$ModelType,
    [string]$Project,
    [string]$CodeLanguage,
	[string]$DbContextType,
	[string]$Area,
	[alias("MasterPage")][string]$Layout,
 	[alias("ContentPlaceholderIDs")][string[]]$SectionNames,
	[alias("PrimaryContentPlaceholderID")][string]$PrimarySectionName,
	[switch]$ReferenceScriptLibraries = $false,
	[switch]$Repository = $false,
	[switch]$NoChildItems = $false,
	[string[]]$TemplateFolders,
	[switch]$Force = $false
)

if (!((Get-ProjectAspNetMvcVersion -Project $Project) -ge 3)) {
	Write-Error ("Project '$((Get-Project $Project).Name)' is not an ASP.NET MVC 3 project.")
	return
}

# Ensure you've referenced System.Data.Entity
(Get-Project $Project).Object.References.Add("System.Data.Entity") | Out-Null

# If you haven't specified a model type, we'll guess from the controller name
if (!$ModelType) {
	if ($ControllerName.EndsWith("Controller", [StringComparison]::OrdinalIgnoreCase)) {
		# If you've given "PeopleController" as the full controller name, we're looking for a model called People or Person
		$ModelType = [System.Text.RegularExpressions.Regex]::Replace($ControllerName, "Controller$", "", [System.Text.RegularExpressions.RegexOptions]::IgnoreCase)
		$foundModelType = Get-ProjectType $ModelType -Project $Project -BlockUi -ErrorAction SilentlyContinue
		if (!$foundModelType) {
			$ModelType = [string](Get-SingularizedWord $ModelType)
			$foundModelType = Get-ProjectType $ModelType -Project $Project -BlockUi -ErrorAction SilentlyContinue
		}
	} else {
		# If you've given "people" as the controller name, we're looking for a model called People or Person, and the controller will be PeopleController
		$ModelType = $ControllerName
		$foundModelType = Get-ProjectType $ModelType -Project $Project -BlockUi -ErrorAction SilentlyContinue
		if (!$foundModelType) {
			$ModelType = [string](Get-SingularizedWord $ModelType)
			$foundModelType = Get-ProjectType $ModelType -Project $Project -BlockUi -ErrorAction SilentlyContinue
		}
		if ($foundModelType) {
			$ControllerName = [string](Get-PluralizedWord $foundModelType.Name) + "Controller"
		}
	}
	if (!$foundModelType) { throw "Cannot find a model type corresponding to a controller called '$ControllerName'. Try supplying a -ModelType parameter value." }
} else {
	# If you have specified a model type
	$foundModelType = Get-ProjectType $ModelType -Project $Project -BlockUi
	if (!$foundModelType) { return }
	if (!$ControllerName.EndsWith("Controller", [StringComparison]::OrdinalIgnoreCase)) {
		$ControllerName = $ControllerName + "Controller"
	}
}
Write-Host "Scaffolding $ControllerName..."

$primaryKey = Get-PrimaryKey $foundModelType.FullName -Project $Project -ErrorIfNotFound
if (!$primaryKey) { return }

$templateName = if($Repository) { "ControllerWithRepository" } else { "ControllerWithContext" }
$templateFile = Find-ScaffolderTemplate $templateName -TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -ErrorIfNotFound
if ($templateFile) {
	$outputPath = Join-Path Controllers $ControllerName
	# We don't create areas here, so just ensure that if you specify one, it already exists
	if ($Area) {
		$areaPath = Join-Path Areas $Area
		if (-not (Get-ProjectItem $areaPath -Project $Project)) {
			Write-Error "Cannot find area '$Area'. Make sure it exists already."
			return
		}
		$outputPath = Join-Path $areaPath $outputPath
	}

	# Prepare all the parameter values to pass to the template, then invoke the template with those values
	if(!$DbContextType) { $DbContextType = [System.Text.RegularExpressions.Regex]::Replace((Get-Project $Project).Name, "[^a-zA-Z0-9]", "") + "Context" }
	$repositoryName = $foundModelType.Name + "Repository"
	$defaultNamespace = (Get-Project $Project).Properties.Item("DefaultNamespace").Value
	$modelTypeNamespace = [T4Scaffolding.Namespaces]::GetNamespace($foundModelType.FullName)
	$controllerNamespace = [T4Scaffolding.Namespaces]::Normalize($defaultNamespace + "." + [System.IO.Path]::GetDirectoryName($outputPath).Replace([System.IO.Path]::DirectorySeparatorChar, "."))
	$areaNamespace = if ($Area) { [T4Scaffolding.Namespaces]::Normalize($defaultNamespace + ".Areas.$Area") } else { $defaultNamespace }
	$dbContextNamespace = [T4Scaffolding.Namespaces]::Normalize($areaNamespace + ".Models")
	$modelTypePluralized = Get-PluralizedWord $foundModelType.Name
	$relatedEntities = [Array](Get-RelatedEntities $foundModelType.FullName -Project $project)
	if (!$relatedEntities) { $relatedEntities = @() }
	$wroteFile = Invoke-ScaffoldTemplate -Template $templateFile -Model @{ 
		ControllerName = $ControllerName;
		ModelType = [MarshalByRefObject]$foundModelType; 
		PrimaryKey = [string]$primaryKey; 
		DefaultNamespace = $defaultNamespace; 
		AreaNamespace = $areaNamespace; 
		DbContextNamespace = $dbContextNamespace;
		ModelTypeNamespace = $modelTypeNamespace; 
		ControllerNamespace = $controllerNamespace; 
		DbContextType = $DbContextType; 
		Repository = $repositoryName; 
		ModelTypePluralized = [string]$modelTypePluralized; 
		RelatedEntities = $relatedEntities;
	} -Project $Project -OutputPath $outputPath -Force:$Force 
	if($wroteFile) {
		Write-Host "Added controller '$wroteFile'"
	}

	if (!$NoChildItems) {
		if ($Repository) {
			Scaffold Repository -ModelType $foundModelType.FullName -DbContextType $DbContextType -Area $Area -Project $Project -CodeLanguage $CodeLanguage -Force:$Force -BlockUi
		} else {
			Scaffold DbContext -ModelType $foundModelType.FullName -DbContextType $DbContextType -Area $Area -Project $Project -CodeLanguage $CodeLanguage -BlockUi
		}

		$controllerNameWithoutSuffix = [System.Text.RegularExpressions.Regex]::Replace($ControllerName, "Controller$", "", [System.Text.RegularExpressions.RegexOptions]::IgnoreCase)
		Scaffold Views -Controller $controllerNameWithoutSuffix -ModelType $foundModelType.FullName -Area $Area -Layout $Layout -SectionNames $SectionNames -PrimarySectionName $PrimarySectionName -ReferenceScriptLibraries:$ReferenceScriptLibraries -Project $Project -CodeLanguage $CodeLanguage -Force:$Force
	}
}