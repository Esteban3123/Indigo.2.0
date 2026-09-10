'***********************************************************************
' Assembly         : DistributedServices.MixinStation
' Author           : Duván Albeiro Mejia Cortes
' Created          : 05/10/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()>
Public Interface IMixingStationServiceQualityControl

    ''' <summary>
    ''' Actualiza el estado de calidad del Producto 
    ''' </summary>
    ''' <param name="RequestPackageDetailStatusIds"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function UpdateRequestPackageDetailStatusQS(ByVal requestPackageDetailStatusIds As List(Of Integer), arguments As String, audit As AuditMessage) As Task(Of ActionResult)

    ''' <summary>
    ''' Asigna el almacén segun el tipo de Accion: Solitante o Manual 
    ''' </summary>
    ''' <param name="Data"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function RequestPackageDetailStatusUpdateWarehouseAsync(Data As Tuple(Of Integer, List(Of Integer))) As Task(Of ActionResult)

    ''' <summary>
    ''' Genera la Orden de Traslado
    ''' </summary>
    ''' <param name="transferOrderlist"></param>
    ''' <param name="ProductDetailsList"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveOrderTransferAsync(transferOrderlist As TransferOrder, ProductDetailsList As List(Of ViewListFinalControlProductModel), audit As AuditMessage) As Task(Of ActionResult)

    ''' <summary>
    ''' Obtiene el check list
    ''' </summary>
    ''' <param name="requestPackageDetailStatusIds"></param>
    ''' <returns></returns>
    <OperationContract>
    Function GetRequestPackageDetailStatusDefectClassification(requestPackageDetailStatusIds As List(Of Integer), unitDoseClass As Integer, Optional flag As Byte = 0) As List(Of DefectClassificationModel)

    ''' <summary>
    ''' Guarda el checklist
    ''' </summary>
    ''' <param name="requestPackageDetailStatusIds"></param>
    ''' <param name="DefectClassificationHeader"></param>
    ''' <param name="lst"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract>
    Function SaveDefectClassificationByRequestPackageDetailStatus(isQuality As Boolean, requestPackageDetailStatusIds As List(Of Integer), DefectClassificationHeader As DefectClassificationHeaderModel, lst As List(Of DefectClassificationModel), audit As AuditMessage, Optional ForceSave As Boolean = False) As Task(Of ActionResult)

    ''' <summary>
    ''' funcion para validar si un producto puede ser rechazado, reprocesado o liberado
    ''' </summary>
    ''' <param name="requestPackageStatusIds"></param>
    ''' <param name="TypeAction"></param>
    ''' <returns></returns>
    <OperationContract>
    Function ValidationDefectClassification(requestPackageStatusIds As List(Of Integer), TypeAction As Integer) As ActionResult(Of List(Of Integer))

    ''' <summary>
    ''' Reprograma una solicitud
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function RescheduleRequestMixingStation(requestMixingStationDetailIds As List(Of Integer), audit As AuditMessage) As ActionResult

End Interface
