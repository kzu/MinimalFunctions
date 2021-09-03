# MinimalFunctions

Azure Functions app from multiple ASP.NET Core minimal APIs

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

For convenience, I'm setting [PublishDir](https://github.com/kzu/MinimalFunctions/blob/main/src/Directory.props#L10) and 
forcing a [publish on build](https://github.com/kzu/MinimalFunctions/blob/main/src/Directory.targets#L4). The build 
script then just needs to [zip the folder](https://github.com/kzu/MinimalFunctions/blob/main/.github/workflows/build.yml#L48-L53) 
and [deploy it with a one-liner](https://github.com/kzu/MinimalFunctions/blob/main/.github/workflows/build.yml#L61-L62).