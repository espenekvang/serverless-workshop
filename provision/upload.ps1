Param(
    [Parameter(Mandatory=$True,Position=2)]
    [string]$resourceGroup,
    [Parameter(Mandatory=$True,Position=1)]
    [string]$appName    
)
Write-Host "Running the script"
$vstsWorkDir = $env:SYSTEM_DEFAULTWORKINGDIRECTORY
$vstsReleaseDefName = $env:RELEASE_DEFINITIONNAME

if ($vstsWorkDir -and $vstsReleaseDefName) {
    $artifactDir = "$($env:SYSTEM_DEFAULTWORKINGDIRECTORY)/$($env:RELEASE_DEFINITIONNAME)/drop"
    $zip = Get-ChildItem -Path $artifactDir -Filter *.zip | Select-Object -First 1    
    $zipPath = "$artifactDir/$($zip.Name)"
} else {
    $artifactDir = split-path -parent $MyInvocation.MyCommand.Definition    
    $zip = Get-ChildItem -Path $artifactDir -Filter *.zip | Select-Object -First 1
    $zipPath = "$artifactDir/$($zip.Name)"    
}
Write-Host "Deploying $zip"
$deploy = az webapp deployment source config-zip -g $resourceGroup -n $appName --src $zipPath --query "status"
# status 4 = success - status 3 = failed
if ([string]$deploy -ne '4') 
{ 
    Write-Error "Deployment Failed, see https://$appName.scm.azurewebsites.net/zipdeploy for details"; 
    EXIT 1 
} 

Write-Host "Successfully deployed $appName"