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
    Implements IMixingStationServiceExternalCareCenter

    Public Function SaveExternalCareCenter(ExternalCareCenter As ExternalCareCenter, audit As AuditMessage, operatingUnitId As Integer, Optional idSequense As Long = 0) As ActionResult(Of ExternalCareCenter) Implements IMixingStationServiceExternalCareCenter.SaveExternalCareCenter
        Using service As IExternalCareCenterAdminService = Container.Current.Resolve(Of IExternalCareCenterAdminService)()
            Return service.SaveExternalCareCenter(ExternalCareCenter, audit, operatingUnitId, idSequense)
        End Using
    End Function

    Public Function DeleteExternalCareCenter(ExternalCareCenter As ExternalCareCenter, audit As AuditMessage, TransactionalContainer As String) As ActionResult Implements IMixingStationServiceExternalCareCenter.DeleteExternalCareCenter
        Using service As IExternalCareCenterAdminService = Container.Current.Resolve(Of IExternalCareCenterAdminService)()
            Return service.DeleteExternalCareCenter(ExternalCareCenter, audit, TransactionalContainer)
        End Using
    End Function

    Public Function GetExternalCareCenter(code As String, audit As AuditMessage) As ActionResult(Of ExternalCareCenter) Implements IMixingStationServiceExternalCareCenter.GetExternalCareCenter
        Using service As IExternalCareCenterAdminService = Container.Current.Resolve(Of IExternalCareCenterAdminService)()
            Return service.GetExternalCareCenter(code, audit)
        End Using
    End Function

    Public Function GetExternalCareCenterById(id As Integer) As ActionResult(Of ExternalCareCenter) Implements IMixingStationServiceExternalCareCenter.GetExternalCareCenterById
        Using service As IExternalCareCenterAdminService = Container.Current.Resolve(Of IExternalCareCenterAdminService)()
            Return service.GetExternalCareCenterById(id)
        End Using
    End Function

    Public Function ChangeStateExternalCareCenter(code As String, state As Boolean, audit As AuditMessage, operatingUnitId As Integer) As ActionResult(Of ExternalCareCenter) Implements IMixingStationServiceExternalCareCenter.ChangeStateExternalCareCenter
        Using service As IExternalCareCenterAdminService = Container.Current.Resolve(Of IExternalCareCenterAdminService)()
            Return service.ChangeStateExternalCareCenter(code, state, audit, operatingUnitId)
        End Using
    End Function

End Class
