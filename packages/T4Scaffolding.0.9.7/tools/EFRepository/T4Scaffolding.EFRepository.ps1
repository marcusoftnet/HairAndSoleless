[T4Scaffolding.Scaffolder(Description = "Creates a repository")][CmdletBinding()]
param(        
    [parameter(Position = 0, Mandatory = $true, ValueFromPipelineByPropertyName = $true)][string]$ModelType,
    [string]$DbContextType,
	[string]$Folder,
	[string]$Area,
    [string]$Project,
    [string]$CodeLanguage,
	[switch]$NoChildItems = $false,
	[string[]]$TemplateFolders,
	[switch]$Force = $false
)

# Ensure you've referenced System.Data.Entity
(Get-Project $Project).Object.References.Add("System.Data.Entity") | Out-Null

$foundModelType = Get-ProjectType $ModelType -Project $Project -BlockUi
if (!$foundModelType) { return }

$primaryKey = Get-PrimaryKey $foundModelType.FullName -Project $Project -ErrorIfNotFound
if (!$primaryKey) { return }

if(!$DbContextType) { $DbContextType = [System.Text.RegularExpressions.Regex]::Replace((Get-Project $Project).Name, "[^a-zA-Z0-9]", "") + "Context" }
		
$templateFile = Find-ScaffolderTemplate Repository -TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -ErrorIfNotFound
if ($templateFile) {
	$outputPath = Join-Path Models ($foundModelType.Name + "Repository")
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

	$modelTypePluralized = Get-PluralizedWord $foundModelType.Name
	$defaultNamespace = (Get-Project $Project).Properties.Item("DefaultNamespace").Value
	$repositoryNamespace = [T4Scaffolding.Namespaces]::Normalize($defaultNamespace + "." + [System.IO.Path]::GetDirectoryName($outputPath).Replace([System.IO.Path]::DirectorySeparatorChar, "."))
	$modelTypeNamespace = [T4Scaffolding.Namespaces]::GetNamespace($foundModelType.FullName)
	$wroteFile = Invoke-ScaffoldTemplate -Template $templateFile -Model @{ 
		ModelType = [MarshalByRefObject]$foundModelType; 
		PrimaryKey = [string]$primaryKey; 
		DefaultNamespace = $defaultNamespace; 
		RepositoryNamespace = $repositoryNamespace; 
		ModelTypeNamespace = $modelTypeNamespace; 
		ModelTypePluralized = [string]$modelTypePluralized; 
		DbContextType = $DbContextType 
	} -Project $Project -OutputPath $outputPath -Force:$Force 
	if($wroteFile) {
		Write-Host "Added repository '$wroteFile'"
	}

	if (!$NoChildItems) {
		Scaffold DbContext -ModelType $ModelType -DbContextType $DbContextType -Area $Area -Project $Project -CodeLanguage $CodeLanguage -BlockUi
	}
}