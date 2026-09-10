'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/10/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity
Imports Application.Authorization
Imports DistribuitedServices.Authorization

Partial Class AuthorizationService
    Implements IAuthorizationServiceConfigurationServicesAmbulatory

    Public Function SaveConfigurationServicesAmbulatory(ConfigurationServicesAmbulatory As ConfigurationServicesAmbulatory, ListPortfolioCUPSEntityIds As List(Of Integer), ListPortfolioInventoryProductIds As List(Of Integer), audit As AuditMessage) As ActionResult(Of ConfigurationServicesAmbulatory) Implements IAuthorizationServiceConfigurationServicesAmbulatory.SaveConfigurationServicesAmbulatory
        Using service As IConfigurationServicesAmbulatoryAdminService = Container.Current.Resolve(Of IConfigurationServicesAmbulatoryAdminService)()
            Return service.SaveConfigurationServicesAmbulatory(ConfigurationServicesAmbulatory, ListPortfolioCUPSEntityIds, ListPortfolioInventoryProductIds, audit)
        End Using
    End Function

    Public Function DeleteConfigurationServicesAmbulatory(ListIds As List(Of Integer), TransactionalContainer As String, audit As AuditMessage) As ActionResult Implements IAuthorizationServiceConfigurationServicesAmbulatory.DeleteConfigurationServicesAmbulatory
        Using service As IConfigurationServicesAmbulatoryAdminService = Container.Current.Resolve(Of IConfigurationServicesAmbulatoryAdminService)()
            Return service.DeleteConfigurationServicesAmbulatory(ListIds, TransactionalContainer, audit)
        End Using
    End Function

    Public Function GetConfigurationServicesAmbulatoryById(id As Integer) As ActionResult(Of ConfigurationServicesAmbulatory) Implements IAuthorizationServiceConfigurationServicesAmbulatory.GetConfigurationServicesAmbulatoryById
        Using service As IConfigurationServicesAmbulatoryAdminService = Container.Current.Resolve(Of IConfigurationServicesAmbulatoryAdminService)()
            Return service.GetConfigurationServicesAmbulatoryById(id)
        End Using
    End Function
End Class
