'***********************************************************************
' Assembly         : DistributedService.MixingStation
' Author           : Judy Andrea Díaz Reyes
' Created          : 21-05-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Application.MixingStation
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Microsoft.Practices.Unity
Imports Infrastructure.CrossCutting.Base
#End Region

Partial Class MixingStationService
    Implements IMixingStationServiceTransportationAssistant

    Public Function SaveTransportationAssistant(TransportationAssistant As TransportationAssistant, audit As AuditMessage, operatingUnitId As Integer, Optional idSequense As Long = 0) As ActionResult(Of TransportationAssistant) Implements IMixingStationServiceTransportationAssistant.SaveTransportationAssistant
        Using service As ITransportationAssistantAdminService = Container.Current.Resolve(Of ITransportationAssistantAdminService)()
            Return service.SaveTransportationAssistant(TransportationAssistant, audit, operatingUnitId, idSequense)
        End Using
    End Function

    Public Function DeleteTransportationAssistant(TransportationAssistant As TransportationAssistant, audit As AuditMessage, TransactionalContainer As String) As ActionResult Implements IMixingStationServiceTransportationAssistant.DeleteTransportationAssistant
        Using service As ITransportationAssistantAdminService = Container.Current.Resolve(Of ITransportationAssistantAdminService)()
            Return service.DeleteTransportationAssistant(TransportationAssistant, audit, TransactionalContainer)
        End Using
    End Function

    Public Function GetTransportationAssistant(code As String, audit As AuditMessage) As ActionResult(Of TransportationAssistant) Implements IMixingStationServiceTransportationAssistant.GetTransportationAssistant
        Using service As ITransportationAssistantAdminService = Container.Current.Resolve(Of ITransportationAssistantAdminService)()
            Return service.GetTransportationAssistant(code, audit)
        End Using
    End Function

    Public Function GetTransportationAssistantById(id As Integer) As ActionResult(Of TransportationAssistant) Implements IMixingStationServiceTransportationAssistant.GetTransportationAssistantById
        Using service As ITransportationAssistantAdminService = Container.Current.Resolve(Of ITransportationAssistantAdminService)()
            Return service.GetTransportationAssistantById(id)
        End Using
    End Function

    Public Function ChangeStateTransportationAssistant(code As String, state As Boolean, audit As AuditMessage, operatingUnitId As Integer) As ActionResult(Of TransportationAssistant) Implements IMixingStationServiceTransportationAssistant.ChangeStateTransportationAssistant
        Using service As ITransportationAssistantAdminService = Container.Current.Resolve(Of ITransportationAssistantAdminService)()
            Return service.ChangeStateTransportationAssistant(code, state, audit, operatingUnitId)
        End Using
    End Function

End Class
