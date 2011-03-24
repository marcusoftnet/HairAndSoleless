[T4Scaffolding.Scaffolder(Description = "Adds an ASP.NET MVC view using the Razor view engine")][CmdletBinding()]
param(        
	[parameter(Mandatory = $true, ValueFromPipelineByPropertyName = $true, Position = 0)][string]$Controller,
	[parameter(Mandatory = $true, ValueFromPipelineByPropertyName = $true, Position = 1)][string]$ViewName,
	[string]$ModelType,
	[string]$Template = "Empty",
	[string]$Area,
	[alias("MasterPage")][string]$Layout,	# If not set, we'll use the default layout
 	[string[]]$SectionNames,
	[string]$PrimarySectionName,
	[switch]$ReferenceScriptLibraries = $false,
    [string]$Project,
	[string]$CodeLanguage,
	[string[]]$TemplateFolders,
	[switch]$Force = $false
)

# Ensure this is a valid target project
if (!((Get-ProjectAspNetMvcVersion -Project $Project) -ge 3)) {
	Write-Error ("Project '$((Get-Project $Project).Name)' is not an ASP.NET MVC 3 project.")
	return
}

# Ensure we have a controller name, plus a model type if specified
if ($ModelType) {
	$foundModelType = Get-ProjectType $ModelType -Project $Project
	if (!$foundModelType) { return }
	$primaryKeyName = [string](Get-PrimaryKey $foundModelType.FullName -Project $Project)
}

# Decide where to put the output
$outputFolderName = Join-Path Views $Controller
if ($Area) {
	# We don't create areas here, so just ensure that if you specify one, it already exists
	$areaPath = Join-Path Areas $Area
	if (-not (Get-ProjectItem $areaPath -Project $Project)) {
		Write-Error "Cannot find area '$Area'. Make sure it exists already."
		return
	}
	$outputFolderName = Join-Path $areaPath $outputFolderName
}
$outputPath = Join-Path $outputFolderName $ViewName

# Find the T4 template
$templateFile = Find-ScaffolderTemplate $Template -TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -ErrorIfNotFound

if ($templateFile) {	
	# Render it, adding the output to the Visual Studio project
	
	if ($foundModelType) { $relatedEntities = [Array](Get-RelatedEntities $foundModelType.FullName -Project $project) }
	if (!$relatedEntities) { $relatedEntities = @() }
	$wroteFile = Invoke-ScaffoldTemplate -Template $templateFile -Model @{ 
		IsContentPage = [bool]$Layout;
		Layout = $Layout;
		SectionNames = $SectionNames;
		PrimarySectionName = $PrimarySectionName;
		ReferenceScriptLibraries = $ReferenceScriptLibraries.ToBool();
		ViewName = $ViewName;
		PrimaryKeyName = $primaryKeyName;
		ViewDataType = [MarshalByRefObject]$foundModelType;
		ViewDataTypeName = $foundModelType.Name;
		RelatedEntities = $relatedEntities;
	} -Project $Project -OutputPath $outputPath -Force:$Force

	if($wroteFile) {
		Write-Host "Added $ViewName view at '$wroteFile'"
	}
}