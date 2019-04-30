# serverless-workshop
Workshop on Azure Functions

## Prerequisites:
- Azure CLI (https://docs.microsoft.com/en-us/cli/azure/install-azure-cli?view=azure-cli-latest)
    - For MacOS: `brew install azure-cli`
- dotnet core 2.1 (https://dotnet.microsoft.com/download/dotnet-core/2.1)
- fake (https://github.com/fsharp/FAKE), install with the following command: `dotnet tool install fake-cli -g --version 5.1.0`
- Microsoft Azure SDK (https://azure.microsoft.com/nb-no/downloads/)
- `az login --tenant ef45xxxx-xxxx-xxxx-xxxx-xxxxxxxxxx82 --subscription xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxx73 --use-device-code`

### Mac users:
- Powershell for mac: `brew cask install powershell`

## Build the solution
- from the root of the project type:
    - type `fake run provision\build.fsx` to build
    - type `fake run provision\build.fsx -t Artifact` to build and create artifact for deployment

## Provision resources
At this point the solution assumes that you have an azure cloud account and a resource group created
- navigate to provision\arm
- log on to azure using: `az login` and follow instructions
- run `create-from-template.ps1`

## Deploy
- first build an artifact using the command described above
- navigate to provision\artifacts
- if not already logged on to azure, do so by: `az login` and follow instructions
- run: `upload.cmd serverless-workshop serverless101-app`
