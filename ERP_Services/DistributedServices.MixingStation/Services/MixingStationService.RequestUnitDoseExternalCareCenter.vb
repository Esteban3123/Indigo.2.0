'***********************************************************************
' Assembly         : DistributedService.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/02/2021
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
    Implements IMixingStationServiceRequestUnitDoseExternalCareCenter

    Public Function SaveRequestUnitDoseExternalCareCenter(RequestUnitDoseExternalCareCenter As RequestUnitDoseExternalCareCenter, audit As AuditMessage, operatingUnitId As Integer, Optional idSequense As Long = 0) As ActionResult(Of RequestUnitDoseExternalCareCenter) Implements IMixingStationServiceRequestUnitDoseExternalCareCenter.SaveRequestUnitDoseExternalCareCenter
        Using service As IRequestUnitDoseExternalCareCenterAdminService = Container.Current.Resolve(Of IRequestUnitDoseExternalCareCenterAdminService)()
            Return service.SaveRequestUnitDoseExternalCareCenter(RequestUnitDoseExternalCareCenter, audit, operatingUnitId, idSequense)
        End Using
    End Function

    Public Function GetRequestUnitDoseExternalCareCenter(code As String, audit As AuditMessage) As ActionResult(Of RequestUnitDoseExternalCareCenter) Implements IMixingStationServiceRequestUnitDoseExternalCareCenter.GetRequestUnitDoseExternalCareCenter
        Using service As IRequestUnitDoseExternalCareCenterAdminService = Container.Current.Resolve(Of IRequestUnitDoseExternalCareCenterAdminService)()
            Return service.GetRequestUnitDoseExternalCareCenter(code, audit)
        End Using
    End Function

    Public Function GetRequestUnitDoseExternalCareCenterById(id As Integer) As ActionResult(Of RequestUnitDoseExternalCareCenter) Implements IMixingStationServiceRequestUnitDoseExternalCareCenter.GetRequestUnitDoseExternalCareCenterById
        Using service As IRequestUnitDoseExternalCareCenterAdminService = Container.Current.Resolve(Of IRequestUnitDoseExternalCareCenterAdminService)()
            Return service.GetRequestUnitDoseExternalCareCenterById(id)
        End Using
    End Function

End Class
