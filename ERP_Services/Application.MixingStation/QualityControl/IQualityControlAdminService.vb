'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Duvan Albeiro Mejia Cortes
' Created          : 05/10/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities


Public Interface IQualityControlAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Ejecuta la accion de cambio de estado  
    ''' </summary>
    ''' <returns></returns>
    Function UpdateRequestPackageDetailStatusQSAsync(ByVal requestPackageDetailStatusIds As List(Of Integer), arguments As String, audit As AuditMessage) As Task(Of ActionResult)

    ''' <summary>
    ''' Actualiza el Almacen en el Dashboard Quality Control 
    ''' </summary>
    ''' <param name="Data"></param>
    ''' <returns></returns>
    Function RequestPackageDetailStatusUpdateWarehouseAsync(Data As Tuple(Of Integer, List(Of Integer))) As Task(Of ActionResult)

    ''' <summary>
    ''' Orden de Traslado
    ''' </summary>
    ''' <param name="transferOrderlist"></param>
    ''' <param name="productDetailsList"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function SaveOrderTransferAsync(transferOrderlist As TransferOrder, productDetailsList As List(Of ViewListFinalControlProductModel), audit As AuditMessage) As Task(Of ActionResult)

    ''' <summary>
    ''' Obtiene el check list
    ''' </summary>
    ''' <param name="requestPackageDetailStatusIds"></param>
    ''' <returns></returns>
    Function GetRequestPackageDetailStatusDefectClassification(requestPackageDetailStatusIds As List(Of Integer), unitDoseClass As Integer, Optional Form As Byte = 0) As List(Of DefectClassificationModel)

    ''' <summary>
    ''' Guarda el checklist
    ''' </summary>
    ''' <param name="requestPackageDetailStatusIds"></param>
    ''' <param name="DefectClassificationHeader"></param>
    ''' <param name="lst"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function SaveDefectClassificationByRequestPackageDetailStatus(isQuality As Boolean, requestPackageDetailStatusIds As List(Of Integer), DefectClassificationHeader As DefectClassificationHeaderModel, lst As List(Of DefectClassificationModel), audit As AuditMessage, Optional ForceSave As Boolean = False) As ActionResult

    ''' <summary>
    ''' funcion para validar si un producto puede ser rechazado, reprocesado o liberado
    ''' </summary>
    ''' <param name="requestPackageStatusIds"></param>
    ''' <param name="TypeAction"></param>
    ''' <returns></returns>
    Function ValidationDefectClassification(requestPackageStatusIds As List(Of Integer), TypeAction As Integer) As ActionResult(Of List(Of Integer))

    ''' <summary>
    ''' Reprograma una solicitud
    ''' </summary>
    ''' <returns></returns>
    Function RescheduleRequestMixingStation(requestMixingStationDetailIds As List(Of Integer), audit As AuditMessage) As ActionResult

End Interface
