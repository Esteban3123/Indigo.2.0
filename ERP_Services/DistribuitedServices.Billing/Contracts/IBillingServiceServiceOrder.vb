'***********************************************************************
' Assembly         : DistributedService.Billing
' Author           : Carlos Ernesto Cordoba
' Created          : 13-11-2014
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities

#End Region

<ServiceContract()>
Public Interface IBillingServiceServiceOrder
    <OperationContract()>
    Function GetInvoiceDetailByServiceOrderDetailId(serviceOrderDetailId As Integer) As InvoiceDetail
    ''' <summary>
    ''' Obtener una orden de servicio por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetServiceOrder(code As String, audit As AuditMessage) As Domain.Entities.ServiceOrder
    ''' <summary>
    ''' Obtener una orden de servicio por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetServiceOrderById(id As Integer) As ServiceOrder

    ''' <summary>
    ''' Obtiene un detalle de orden de servicio por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetServiceOrderDetailById(Id As Integer) As ServiceOrderDetail

    ''' <summary>
    ''' metodo para obtener los detalles de la orden de servicios
    ''' </summary>
    ''' <param name="ServiceOrderId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetServiceOrderDetailByServiceOrderId(ServiceOrderId As Integer) As List(Of ServiceOrderDetail)

    ''' <summary>
    ''' Guardar una orden de servicio
    ''' </summary>
    <OperationContract()>
    Function SaveServiceOrder(ServiceOrder As Domain.Entities.ServiceOrder, idSequence As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ServiceOrder)

    ''' <summary>
    ''' Obtiene un ingreso por codigo para utilizarlo en orden de servicio
    ''' </summary>
    ''' <param name="admissionNumber"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetAdmissionByServiceOrder(admissionNumber As String) As String

    ''' <summary>
    ''' Lista medicamentos procesados por la central de mezclas para generación de la orden de servicios.
    ''' </summary>
    ''' <param name="admissionNumber">Número de ingreso</param>
    ''' <param name="isChild">Obtiene los registros hijos</param>
    ''' <returns>Listado de medicamentos</returns>
    <OperationContract()>
    Function GetProcessedMedicationItemsForBillingList(admissionNumber As String, isChild As Boolean) As List(Of SP_GetProcessedMedicationItemsForBilling_Result)

End Interface
