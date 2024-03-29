@minLength(1)
@maxLength(64)
@description('Name of the resource group that will contain all the resources')
param resourceGroupName string = resourceGroup().name

@minLength(1)
@description('Primary location for all resources')
param location string = resourceGroup().location

param containerAppsEnvironmentName string

@description('List of container apps to deploy')
param containerapps containerappDef[]

param logAnalyticsCustomerId string
@secure()
param logAnalyticsSharedKey string

param deployTimestamp string = utcNow()

var resourceToken = toLower(uniqueString(subscription().id, resourceGroupName, location))

resource containerAppsEnvironment 'Microsoft.App/managedEnvironments@2023-11-02-preview' = {
  name: '${containerAppsEnvironmentName}${resourceToken}'
  location: location
  properties: {
    appLogsConfiguration: {
      destination: 'log-analytics'
      logAnalyticsConfiguration: {
        customerId: logAnalyticsCustomerId
        sharedKey: logAnalyticsSharedKey
      }
    }
  }
}

module containerapp2 'containerapp.bicep' = [for containerapp in containerapps: {
  name: '${containerapp.appName}-${deployTimestamp}'
  params: {
    location: location
    appName: containerapp.appName
    aspnetcoreEnvironment: containerapp.aspnetcoreEnvironment
    containerAppsEnvironmentId: containerAppsEnvironment.id
    containerImage: containerapp.containerImage
    containerRegistryUrl: containerapp.containerRegistryUrl
    managedIdentityClientId: containerapp.managedIdentityClientId
    managedIdentityId: containerapp.managedIdentityId
    appIngressAllowInsecure: containerapp.appIngressAllowInsecure
    appIngressExternal: containerapp.appIngressExternal
    applicationInsightsConnectionString: containerapp.applicationInsightsConnectionString
    additionalEnvironmentVariables: containerapp.additionalEnvironmentVariables
  }
}]

output containerAppsEnvironmentUrl string = containerAppsEnvironment.properties.defaultDomain

type containerappDef = {
  appName: string
  aspnetcoreEnvironment: string
  containerImage: string
  containerRegistryUrl: string
  managedIdentityClientId: string
  managedIdentityId: string
  appIngressAllowInsecure: bool
  appIngressExternal: bool
  applicationInsightsConnectionString: string
  additionalEnvironmentVariables: object[]
}
