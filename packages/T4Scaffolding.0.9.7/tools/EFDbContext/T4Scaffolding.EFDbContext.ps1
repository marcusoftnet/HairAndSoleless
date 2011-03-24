[T4Scaffolding.Scaffolder(Description = "Makes an EF DbContext able to persist models of a given type, creating the DbContext first if necessary")][CmdletBinding()]
param(        
	[parameter(Mandatory = $true, ValueFromPipelineByPropertyName = $true)][string]$ModelType,
	[parameter(Mandatory = $true, ValueFromPipelineByPropertyName = $true)][string]$DbContextType,
	[string]$Folder,
	[string]$Area,
    [string]$Project,
	[string]$CodeLanguage,
	[string[]]$TemplateFolders,
	[ScriptBlock]$ClassUpdateCallback
)

if (!$CodeLanguage) { $CodeLanguage = Get-ProjectLanguage -Project $Project }

function GetOrCreateDbContextProjectItemViaTemplate($pathExcludingExtension, $template) {	
	$projectItem = Get-ProjectItem "$pathExcludingExtension.$CodeLanguage" -Project $Project
	if (!$projectItem) {
		# The project item doesn't already exist, so create it via template
		$templateFile = Find-ScaffolderTemplate $template -TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -ErrorIfNotFound
		if ($templateFile) {
			# Render it, adding the output to the Visual Studio project
			$defaultNamespace = (Get-Project $Project).Properties.Item("DefaultNamespace").Value
			$templateModel = @{ 
				DefaultNamespace = $defaultNamespace; 
				DbContextNamespace = [T4Scaffolding.Namespaces]::Normalize($defaultNamespace + "." + [System.IO.Path]::GetDirectoryName($pathExcludingExtension).Replace([System.IO.Path]::DirectorySeparatorChar, ".")); 
				DbContextType = $DbContextType; 
			}
			$wroteFile = Invoke-ScaffoldTemplate -Template $templateFile -Model $templateModel -Project $Project -OutputPath $pathExcludingExtension
			if($wroteFile) {
				Write-Host "Added database context '$wroteFile'"
				$projectItem = Get-ProjectItem $wroteFile -Project $Project
				if (!$projectItem) { throw "Created database context $wroteFile, but could not find it as a project item" }
			} else {
				throw "Unable to create database context $pathExcludingExtension.$CodeLanguage"
			}
		}		
	} else {
		Write-Verbose "DbContext $pathExcludingExtension.$CodeLanguage already exists. Skipping template rendering..."
	}
	return $projectItem
}

function GetClassFromProjectItem($projectItem) {
	if (!$projectItem.FileCodeModel) {
		Write-Warning "$($projectItem.Name) is not a valid code file or does not match the project language. Skipping database context update..."
		return
	}

	$classes = $projectItem.FileCodeModel.CodeElements | ? { $_.Kind -eq 1 }
	if(!$classes) {
		# No top-level class found. Try searching inside top-level namespaces
		$classes = $projectItem.FileCodeModel.CodeElements | ? { $_.Kind -eq 5 } | ForEach-Object { $_.Children | ? { $_.Kind -eq 1 } }
	}
	if(($classes | Measure-Object).Count -ne 1) {
		$path = $projectItem.Properties.Item("FullPath").Value
		throw "Found existing DbContext file $path, but can't find the class it contains. Ensure it contains valid code and a single top-level class, or delete the file so it can be recreated."
	}
	return $classes
}

# Ensure we can find the model type
$foundModelType = Get-ProjectType $ModelType -Project $Project
if (!$foundModelType) { return }

# Determine where the output will go
$outputPath = Join-Path Models $DbContextType
if ($Area -and -not $Folder) {
	$Folder = Join-Path Areas $Area
	if (-not (Get-ProjectItem $Folder -Project $Project)) {
		Write-Error "Cannot find area '$Area'. Make sure it exists already."
		return
	}
}
if ($Folder) {
	$outputPath = Join-Path $Folder $outputPath
}

# Find or create DbContext class, then add a new property on it
$projectItem = GetOrCreateDbContextProjectItemViaTemplate $outputPath "DbContext"
if ($projectItem) {
	if (!$ClassUpdateCallback) {
		$ClassUpdateCallback = {
			param($class, $modelType, $codeLanguage)
			$propertyName = Get-PluralizedWord $modelType.Name
			$nameComparisonType = if ($codeLanguage -eq "vb") { [StringComparison]::OrdinalIgnoreCase } else { [StringComparison]::Ordinal }
			$existingProperty = $class.Members | ?{ [String]::Equals($_.Name, $propertyName, $nameComparisonType) }
			if (!$existingProperty) {
				$propertyText = if ($codeLanguage -eq "vb") {
					"Public Property $propertyName As DbSet(Of $($modelType.FullName))"
				} else {
					"public DbSet<$($modelType.FullName)> $propertyName { get; set; }"
				}
				$property = $class.AddProperty($propertyName, $propertyName, "object")
				$property.GetStartPoint().CreateEditPoint().ReplaceText($property.GetEndPoint(), $propertyText, [EnvDTE.vsEPReplaceTextOptions]::vsEPReplaceTextAutoformat)

				Write-Host "Added '$propertyName' to database context '$outputPath.$CodeLanguage'"
			} else {
				Write-Warning "$outputPath.$CodeLanguage already contains '$propertyName'! Skipping..."
			}
		}
	}

	$class = GetClassFromProjectItem $projectItem
	if ($class) {
		. $ClassUpdateCallback $class $foundModelType $CodeLanguage
	}
}