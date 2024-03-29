@minLength(1)
@maxLength(64)
@description('Name of the resource group that will contain all the resources')
param resourceGroupName string = resourceGroup().name

@minLength(1)
@description('Primary location for all resources')
param location string = resourceGroup().location

@minLength(3)
@description('Environment for ASP.NET Core. Like "Development", "Production", ..')
@allowed([
  'Test'
  'Production'
])
param aspnetcoreEnvironment string

param containerRegistryUrl string
param managedIdentityName string
param managedIdentityScope string

param apiserviceContainerImage string
param webfrontendContainerImage string
param centralTestDoubleContainerImage string = ''

var resourceToken = toLower(uniqueString(subscription().id, resourceGroupName, location))

resource managedIdentity 'Microsoft.ManagedIdentity/userAssignedIdentities@2023-07-31-preview' existing = {
  name: managedIdentityName
  scope: resourceGroup(managedIdentityScope)
}

// log analytics
resource logAnalytics 'Microsoft.OperationalInsights/workspaces@2023-09-01' = {
  name: 'logs${resourceToken}'
  location: location
  properties: {
    retentionInDays: 30
    sku: {
      name: 'PerGB2018'
    }
    workspaceCapping: {
      dailyQuotaGb: 1
    }
  }
}

resource applicationInsights 'Microsoft.Insights/components@2020-02-02' = {
  name: 'ai${resourceToken}'
  location: location
  kind: 'web'
  properties: {
    Application_Type: 'web'
    Flow_Type: 'Bluefield'
    WorkspaceResourceId: logAnalytics.id
  }
}

module containerAppsTestDoubles 'containerappenvironment.bicep' = if(aspnetcoreEnvironment == 'Test' && centralTestDoubleContainerImage != '') {
  name: 'testdoubles${resourceToken}'
  params: {
    containerAppsEnvironmentName: 'testdoubles'
    location: location
    logAnalyticsCustomerId: logAnalytics.properties.customerId
    logAnalyticsSharedKey: logAnalytics.listKeys().primarySharedKey
    containerapps: [
      {
        appName: 'centraltestdouble'
        aspnetcoreEnvironment: aspnetcoreEnvironment
        appIngressAllowInsecure: false
        appIngressExternal: true
        applicationInsightsConnectionString: applicationInsights.properties.ConnectionString
        containerImage: centralTestDoubleContainerImage
        containerRegistryUrl: containerRegistryUrl
        managedIdentityClientId: managedIdentity.properties.clientId
        managedIdentityId: managedIdentity.id
        additionalEnvironmentVariables: []
      }
    ]
  }
}

module containerAppsEnvironment 'containerappenvironment.bicep' = {
  name: 'acae${resourceToken}'
  params: {
    containerAppsEnvironmentName: 'acae'
    location: location
    logAnalyticsCustomerId: logAnalytics.properties.customerId
    logAnalyticsSharedKey: logAnalytics.listKeys().primarySharedKey
    containerapps: [
      {
        appName: 'apiservice'
        aspnetcoreEnvironment: aspnetcoreEnvironment
        containerRegistryUrl: containerRegistryUrl
        managedIdentityClientId: managedIdentity.properties.clientId
        managedIdentityId: managedIdentity.id
        containerImage: apiserviceContainerImage
        appIngressAllowInsecure: true
        appIngressExternal: false
        applicationInsightsConnectionString: applicationInsights.properties.ConnectionString
        additionalEnvironmentVariables: [
          {
            name: 'Services__central__0'
            value: 'centraltestdouble.${containerAppsTestDoubles.outputs.containerAppsEnvironmentUrl}'
          }
        ]
      }
      {
        appName: 'webfrontend'
        aspnetcoreEnvironment: aspnetcoreEnvironment
        containerRegistryUrl: containerRegistryUrl
        managedIdentityClientId: managedIdentity.properties.clientId
        managedIdentityId: managedIdentity.id
        containerImage: webfrontendContainerImage
        appIngressAllowInsecure: false
        appIngressExternal: true
        applicationInsightsConnectionString: applicationInsights.properties.ConnectionString
        additionalEnvironmentVariables: []
      }
    ]
  }
}

output webfrontendLatestRevisionFqdn string = containerAppsEnvironment.outputs.containerAppsEnvironmentUrl
