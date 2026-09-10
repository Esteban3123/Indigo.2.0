'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 28/02/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities


Public Interface IConfigurationServicesAmbulatoryAdminService
    Inherits IDisposable

    Function SaveConfigurationServicesAmbulatory(ConfigurationServicesAmbulatory As ConfigurationServicesAmbulatory, ListPortfolioCUPSEntityIds As List(Of Integer), ListPortfolioInventoryProductIds As List(Of Integer), audit As AuditMessage) As ActionResult(Of ConfigurationServicesAmbulatory)

    Function DeleteConfigurationServicesAmbulatory(ListIds As List(Of Integer), TransactionalContainer As String, ByVal audit As AuditMessage) As ActionResult

    Function GetConfigurationServicesAmbulatoryById(id As Integer) As ActionResult(Of ConfigurationServicesAmbulatory)

End Interface
