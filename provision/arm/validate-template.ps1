az account set -s "Nettskyprogrammet"
az group deployment validate -g serverless-workshop --template-file "azuredeploy.json" --parameters @parameters/test.parameters.json