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
    Implements IMixingStationServiceStabilityTable

    Public Function SaveStabilityTable(StabilityTable As StabilityTable, audit As AuditMessage, operatingUnitId As Integer, Optional idSequense As Long = 0) As ActionResult(Of StabilityTable) Implements IMixingStationServiceStabilityTable.SaveStabilityTable
        Using service As IStabilityTableAdminService = Container.Current.Resolve(Of IStabilityTableAdminService)()
            Return service.SaveStabilityTable(StabilityTable, audit, operatingUnitId, idSequense)
        End Using
    End Function

    Public Function DeleteStabilityTable(StabilityTable As StabilityTable, audit As AuditMessage, TransactionalContainer As String) As ActionResult Implements IMixingStationServiceStabilityTable.DeleteStabilityTable
        Using service As IStabilityTableAdminService = Container.Current.Resolve(Of IStabilityTableAdminService)()
            Return service.DeleteStabilityTable(StabilityTable, audit, TransactionalContainer)
        End Using
    End Function

    Public Function GetStabilityTable(code As String, audit As AuditMessage) As ActionResult(Of StabilityTable) Implements IMixingStationServiceStabilityTable.GetStabilityTable
        Using service As IStabilityTableAdminService = Container.Current.Resolve(Of IStabilityTableAdminService)()
            Return service.GetStabilityTable(code, audit)
        End Using
    End Function

    Public Function GetStabilityTableById(id As Integer) As ActionResult(Of StabilityTable) Implements IMixingStationServiceStabilityTable.GetStabilityTableById
        Using service As IStabilityTableAdminService = Container.Current.Resolve(Of IStabilityTableAdminService)()
            Return service.GetStabilityTableById(id)
        End Using
    End Function

    Public Function ChangeStateStabilityTable(code As String, state As Boolean, audit As AuditMessage, operatingUnitId As Integer) As ActionResult(Of StabilityTable) Implements IMixingStationServiceStabilityTable.ChangeStateStabilityTable
        Using service As IStabilityTableAdminService = Container.Current.Resolve(Of IStabilityTableAdminService)()
            Return service.ChangeStateStabilityTable(code, state, audit, operatingUnitId)
        End Using
    End Function

End Class
