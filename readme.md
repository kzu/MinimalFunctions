# MinimalFunctions

(Mostly) Minimal .NET 6.0 (preview) APIs in Azure Functions

End-to-end example of building and deploying .NET 6 APIs to 
Azure Functions (current stable, v3) runtime using GitHub 
actions.


Steps to get (this) end-to-end .NET6 function running in Azure Functions:

1. Fork this repo
1. Create a Functions app using a new resource group named `minimalfunction`, .NET 3.1 runtime, Windows hosting
1. Using the Azure CLI, [create credentials](https://github.com/Azure/functions-action#using-azure-service-principal-for-rbac-as-deployment-credential) for the deployment with:
   `az ad sp create-for-rbac --name minimalfunction --role contributor --scopes "/subscriptions/[SUBSCRIPTION_ID]/resourceGroups/minimalfunction" --sdk-auth`
   Note the use of the resource group as the scope, *not* just the web app (didn't work with that for me).
1. Copy the resulting json and set it as a repository secret named `AZURE_CREDENTIALS`
1. Add another repository secret named `AZURE_APPNAME` with your function app name for deployment
1. Set the following MSBuild properties on your functions project:

    ```xml
        <TargetFramework>net6.0</TargetFramework>
        <AzureFunctionsVersion>v3</AzureFunctionsVersion>
        <RuntimeFrameworkVersion>5.0.7</RuntimeFrameworkVersion>
        <RuntimeIdentifier>win10-x86</RuntimeIdentifier>
        <SelfContained>true</SelfContained>
    ```

    In my case, I used a conditional property group for the additional deployment properties since otherwise 
    those pollute the local build:

    ```xml
      <PropertyGroup Condition="$(CI)">
        ...
      </PropertyGroup>
    ```

1. The build script needs to install func v4 with `npm i -g azure-functions-core-tools@4 --unsafe-perm true`, 
   and then do the publish with `func azure functionapp publish ${{ secrets.AZURE_APPNAME }} --force`. 
   
   > Using `--no-build` [option](https://docs.microsoft.com/en-us/azure/azure-functions/functions-core-tools-reference?tabs=v2#func-azure-functionapp-publish) didn't work for me.
   > Additional build args can be passed at the end with `--dotnet-cli-params -- -p:PROP=VALUE ...`.