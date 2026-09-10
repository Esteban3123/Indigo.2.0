'***********************************************************************
' Assembly         : DistributedService.MixingStation
' Author           : Duván Albeiro Mejia Cortes 
' Created          : 05/10/2021
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
    Implements IMixingStationServiceQualityControl

    ''' <summary>
    ''' Actualiza el estado de calidad del Producto 
    ''' </summary>
    ''' <param name="RequestPackageDetailStatusIds"></param>
    ''' <returns></returns>
    Public Async Function UpdateRequestPackageDetailStatusQS(requestPackageDetailStatusIds As List(Of Integer), arguments As String, audit As AuditMessage) As Task(Of ActionResult) Implements IMixingStationServiceQualityControl.UpdateRequestPackageDetailStatusQS
        Using service As IQualityControlAdminService = Container.Current.Resolve(Of IQualityControlAdminService)()
            Return Await service.UpdateRequestPackageDetailStatusQSAsync(requestPackageDetailStatusIds, arguments, audit)
        End Using
    End Function

    ''' <summary>
    ''' Actualiza el Almacén
    ''' </summary>
    ''' <param name="Data"></param>
    ''' <returns></returns>
    Public Async Function RequestPackageDetailStatusUpdateWarehouseAsync(Data As Tuple(Of Integer, List(Of Integer))) As Task(Of ActionResult) Implements IMixingStationServiceQualityControl.RequestPackageDetailStatusUpdateWarehouseAsync
        Using service As IQualityControlAdminService = Container.Current.Resolve(Of IQualityControlAdminService)()
            Return Await service.RequestPackageDetailStatusUpdateWarehouseAsync(Data)
        End Using
    End Function

    ''' <summary>
    ''' Orden de Traslado
    ''' </summary>
    ''' <param name="transferOrderlist"></param>
    ''' <param name="ProductDetailsList"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Async Function SaveOrderTransferAsync(transferOrderlist As TransferOrder, productDetailsList As List(Of ViewListFinalControlProductModel), audit As AuditMessage) As Task(Of ActionResult) Implements IMixingStationServiceQualityControl.SaveOrderTransferAsync
        Using service As IQualityControlAdminService = Container.Current.Resolve(Of IQualityControlAdminService)()
            Return Await service.SaveOrderTransferAsync(transferOrderlist, productDetailsList, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene el check list
    ''' </summary>
    ''' <param name="requestPackageDetailStatusIds"></param>
    ''' <returns></returns>
    Public Function GetRequestPackageDetailStatusDefectClassification(requestPackageDetailStatusIds As List(Of Integer), unitDoseClass As Integer, Optional flag As Byte = 0) As List(Of DefectClassificationModel) Implements IMixingStationServiceQualityControl.GetRequestPackageDetailStatusDefectClassification
        Using service As IQualityControlAdminService = Container.Current.Resolve(Of IQualityControlAdminService)()
            Return service.GetRequestPackageDetailStatusDefectClassification(requestPackageDetailStatusIds, unitDoseClass, flag)
        End Using
    End Function

    ''' <summary>
    ''' Guarda el checklist
    ''' </summary>
    ''' <param name="requestPackageDetailStatusIds"></param>
    ''' <param name="observation"></param>
    ''' <param name="lst"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Async Function SaveDefectClassificationByRequestPackageDetailStatus(isQuality As Boolean, requestPackageDetailStatusIds As List(Of Integer), DefectClassificationHeader As DefectClassificationHeaderModel, lst As List(Of DefectClassificationModel), audit As AuditMessage, Optional ForceSave As Boolean = False) As Task(Of ActionResult) Implements IMixingStationServiceQualityControl.SaveDefectClassificationByRequestPackageDetailStatus
        Using service As IQualityControlAdminService = Container.Current.Resolve(Of IQualityControlAdminService)()
            Return Await service.SaveDefectClassificationByRequestPackageDetailStatusAsync(isQuality, requestPackageDetailStatusIds, DefectClassificationHeader, lst, audit, ForceSave)
        End Using
    End Function

    ''' <summary>
    ''' funcion para validar si un producto puede ser rechazado, reprocesado o liberado
    ''' </summary>
    ''' <param name="requestPackageStatusIds"></param>
    ''' <param name="TypeAction"></param>
    ''' <returns></returns>
    Public Function ValidationDefectClassification(requestPackageStatusIds As List(Of Integer), TypeAction As Integer) As ActionResult(Of List(Of Integer)) Implements IMixingStationServiceQualityControl.ValidationDefectClassification
        Using service As IQualityControlAdminService = Container.Current.Resolve(Of IQualityControlAdminService)()
            Return service.ValidationDefectClassification(requestPackageStatusIds, TypeAction)
        End Using
    End Function

    Public Function RescheduleRequestMixingStation(requestMixingStationDetailIds As List(Of Integer), audit As AuditMessage) As ActionResult Implements IMixingStationServiceQualityControl.RescheduleRequestMixingStation
        Using service As IQualityControlAdminService = Container.Current.Resolve(Of IQualityControlAdminService)()
            Return service.RescheduleRequestMixingStation(requestMixingStationDetailIds, audit)
        End Using
    End Function
End Class