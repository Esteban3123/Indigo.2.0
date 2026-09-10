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
    Implements IMixingStationServiceCauseReprocessingRejection

    Public Function SaveCauseReprocessingRejection(CauseReprocessingRejection As CauseReprocessingRejection, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of CauseReprocessingRejection) Implements IMixingStationServiceCauseReprocessingRejection.SaveCauseReprocessingRejection
        Using service As ICauseReprocessingRejectionAdminService = Container.Current.Resolve(Of ICauseReprocessingRejectionAdminService)()
            Return service.SaveCauseReprocessingRejection(CauseReprocessingRejection, audit, idSequense)
        End Using
    End Function

    Public Function DeleteCauseReprocessingRejection(CauseReprocessingRejection As CauseReprocessingRejection, audit As AuditMessage) As ActionResult Implements IMixingStationServiceCauseReprocessingRejection.DeleteCauseReprocessingRejection
        Using service As ICauseReprocessingRejectionAdminService = Container.Current.Resolve(Of ICauseReprocessingRejectionAdminService)()
            Return service.DeleteCauseReprocessingRejection(CauseReprocessingRejection, audit)
        End Using
    End Function

    Public Function GetCauseReprocessingRejection(code As String, audit As AuditMessage) As CauseReprocessingRejection Implements IMixingStationServiceCauseReprocessingRejection.GetCauseReprocessingRejection
        Using service As ICauseReprocessingRejectionAdminService = Container.Current.Resolve(Of ICauseReprocessingRejectionAdminService)()
            Return service.GetCauseReprocessingRejection(code, audit)
        End Using
    End Function

    Public Function GetCauseReprocessingRejectionById(id As Integer) As CauseReprocessingRejection Implements IMixingStationServiceCauseReprocessingRejection.GetCauseReprocessingRejectionById
        Using service As ICauseReprocessingRejectionAdminService = Container.Current.Resolve(Of ICauseReprocessingRejectionAdminService)()
            Return service.GetCauseReprocessingRejectionById(id)
        End Using
    End Function

    Public Function ChangeStateCauseReprocessingRejection(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of CauseReprocessingRejection) Implements IMixingStationServiceCauseReprocessingRejection.ChangeStateCauseReprocessingRejection
        Using service As ICauseReprocessingRejectionAdminService = Container.Current.Resolve(Of ICauseReprocessingRejectionAdminService)()
            Return service.ChangeStateCauseReprocessingRejection(code, state, audit)
        End Using
    End Function

End Class
