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
    Implements IMixingStationServicePatientExternalCareCenter

    Public Function SavePatientExternalCareCenter(PatientExternalCareCenter As PatientExternalCareCenter, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of PatientExternalCareCenter) Implements IMixingStationServicePatientExternalCareCenter.SavePatientExternalCareCenter
        Using service As IPatientExternalCareCenterAdminService = Container.Current.Resolve(Of IPatientExternalCareCenterAdminService)()
            Return service.SavePatientExternalCareCenter(PatientExternalCareCenter, audit, idSequense)
        End Using
    End Function

    Public Function DeletePatientExternalCareCenter(PatientExternalCareCenter As PatientExternalCareCenter, audit As AuditMessage) As ActionResult Implements IMixingStationServicePatientExternalCareCenter.DeletePatientExternalCareCenter
        Using service As IPatientExternalCareCenterAdminService = Container.Current.Resolve(Of IPatientExternalCareCenterAdminService)()
            Return service.DeletePatientExternalCareCenter(PatientExternalCareCenter, audit)
        End Using
    End Function

    Public Function GetPatientExternalCareCenter(code As String, audit As AuditMessage) As PatientExternalCareCenter Implements IMixingStationServicePatientExternalCareCenter.GetPatientExternalCareCenter
        Using service As IPatientExternalCareCenterAdminService = Container.Current.Resolve(Of IPatientExternalCareCenterAdminService)()
            Return service.GetPatientExternalCareCenter(code, audit)
        End Using
    End Function

    Public Function GetPatientExternalCareCenterById(id As Integer) As PatientExternalCareCenter Implements IMixingStationServicePatientExternalCareCenter.GetPatientExternalCareCenterById
        Using service As IPatientExternalCareCenterAdminService = Container.Current.Resolve(Of IPatientExternalCareCenterAdminService)()
            Return service.GetPatientExternalCareCenterById(id)
        End Using
    End Function

    Public Function ChangeStatePatientExternalCareCenter(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of PatientExternalCareCenter) Implements IMixingStationServicePatientExternalCareCenter.ChangeStatePatientExternalCareCenter
        Using service As IPatientExternalCareCenterAdminService = Container.Current.Resolve(Of IPatientExternalCareCenterAdminService)()
            Return service.ChangeStatePatientExternalCareCenter(code, state, audit)
        End Using
    End Function

End Class
