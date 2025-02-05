targetScope = 'subscription'

param location string

param principalId string

resource rg 'Microsoft.Resources/resourceGroups@2024-03-01' = {
  name: 'cm1afad70ff42d496'
  location: location
}

module cm 'cm.bicep' = {
  name: 'cm'
  scope: rg
  params: {
    location: location
    principalId: principalId
  }
}