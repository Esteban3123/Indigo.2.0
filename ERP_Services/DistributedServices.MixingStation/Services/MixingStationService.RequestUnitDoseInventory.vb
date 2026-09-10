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
    Implements IMixingStationServiceRequestUnitDoseInventory

    Public Function SaveRequestUnitDoseInventory(RequestUnitDoseInventory As RequestUnitDoseInventory, audit As AuditMessage, operatingUnitId As Integer, Optional idSequense As Long = 0) As ActionResult(Of RequestUnitDoseInventory) Implements IMixingStationServiceRequestUnitDoseInventory.SaveRequestUnitDoseInventory
        Using service As IRequestUnitDoseInventoryAdminService = Container.Current.Resolve(Of IRequestUnitDoseInventoryAdminService)()
            Return service.SaveRequestUnitDoseInventory(RequestUnitDoseInventory, audit, operatingUnitId, idSequense)
        End Using
    End Function

    Public Function GetRequestUnitDoseInventory(code As String, audit As AuditMessage) As ActionResult(Of RequestUnitDoseInventory) Implements IMixingStationServiceRequestUnitDoseInventory.GetRequestUnitDoseInventory
        Using service As IRequestUnitDoseInventoryAdminService = Container.Current.Resolve(Of IRequestUnitDoseInventoryAdminService)()
            Return service.GetRequestUnitDoseInventory(code, audit)
        End Using
    End Function

    Public Function GetRequestUnitDoseInventoryById(id As Integer) As ActionResult(Of RequestUnitDoseInventory) Implements IMixingStationServiceRequestUnitDoseInventory.GetRequestUnitDoseInventoryById
        Using service As IRequestUnitDoseInventoryAdminService = Container.Current.Resolve(Of IRequestUnitDoseInventoryAdminService)()
            Return service.GetRequestUnitDoseInventoryById(id)
        End Using
    End Function

End Class
