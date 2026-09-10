'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 02/03/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface IAuthorizationServiceConfigurationServicesAmbulatory

    <OperationContract()>
    Function SaveConfigurationServicesAmbulatory(ConfigurationServicesAmbulatory As ConfigurationServicesAmbulatory, ListPortfolioCUPSEntityIds As List(Of Integer), ListPortfolioInventoryProductIds As List(Of Integer), audit As AuditMessage) As ActionResult(Of ConfigurationServicesAmbulatory)

    <OperationContract()>
    Function DeleteConfigurationServicesAmbulatory(ListIds As List(Of Integer), TransactionalContainer As String, ByVal audit As AuditMessage) As ActionResult

    <OperationContract()>
    Function GetConfigurationServicesAmbulatoryById(id As Integer) As ActionResult(Of ConfigurationServicesAmbulatory)

End Interface
