'***********************************************************************
' Assembly         : DistributedService.MixingStation
' Author           : Judy Andrea Díaz Reyes
' Created          : 21-05-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Application.MixingStation
Imports DistributedServices.MixingStation
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity
Imports System.ServiceModel
#End Region

Partial Class MixingStationService
    Implements IMixingStationServiceContractExternalClients

    Public Function SaveContractExternalClients(ContractExternalClients As ContractExternalClients, audit As AuditMessage, operatingUnitId As Integer, Optional idSequense As Long = 0) As ActionResult(Of ContractExternalClients) Implements IMixingStationServiceContractExternalClients.SaveContractExternalClients
        Using service As IContractExternalClientsAdminService = Container.Current.Resolve(Of IContractExternalClientsAdminService)()
            Return service.SaveContractExternalClients(ContractExternalClients, audit, operatingUnitId, idSequense)
        End Using
    End Function

    Public Function DeleteContractExternalClients(ContractExternalClients As ContractExternalClients, audit As AuditMessage, TransactionalContainer As String) As ActionResult Implements IMixingStationServiceContractExternalClients.DeleteContractExternalClients
        Using service As IContractExternalClientsAdminService = Container.Current.Resolve(Of IContractExternalClientsAdminService)()
            Return service.DeleteContractExternalClients(ContractExternalClients, audit, TransactionalContainer)
        End Using
    End Function

    Public Function GetContractExternalClients(code As String, audit As AuditMessage) As ActionResult(Of ContractExternalClients) Implements IMixingStationServiceContractExternalClients.GetContractExternalClients
        Using service As IContractExternalClientsAdminService = Container.Current.Resolve(Of IContractExternalClientsAdminService)()
            Return service.GetContractExternalClients(code, audit)
        End Using
    End Function

    Public Function GetContractExternalClientsById(id As Integer) As ActionResult(Of ContractExternalClients) Implements IMixingStationServiceContractExternalClients.GetContractExternalClientsById
        Using service As IContractExternalClientsAdminService = Container.Current.Resolve(Of IContractExternalClientsAdminService)()
            Return service.GetContractExternalClientsById(id)
        End Using
    End Function

    Public Function ChangeStateContractExternalClients(code As String, state As Boolean, audit As AuditMessage, operatingUnitId As Integer) As ActionResult(Of ContractExternalClients) Implements IMixingStationServiceContractExternalClients.ChangeStateContractExternalClients
        Using service As IContractExternalClientsAdminService = Container.Current.Resolve(Of IContractExternalClientsAdminService)()
            Return service.ChangeStateContractExternalClients(code, state, audit, operatingUnitId)
        End Using
    End Function

    Public Function ImportExceptionsRawMaterial(data As List(Of List(Of String))) As ActionResult(Of List(Of ContractExternalClientsDetail)) Implements IMixingStationServiceContractExternalClients.ImportExceptionsRawMaterial
        Using service As IContractExternalClientsAdminService = Container.Current.Resolve(Of IContractExternalClientsAdminService)()
            Return service.ImportExceptionsRawMaterial(data)
        End Using
    End Function

End Class
