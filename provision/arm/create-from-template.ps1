az account set -s "Nettskyprogrammet"
az group deployment create -g serverless-workshop --template-file "azuredeploy.json" --parameters @parameters/test.parameters.json