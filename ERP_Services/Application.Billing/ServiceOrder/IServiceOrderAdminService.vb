'***********************************************************************
' Assembly         : Application.Billing
' Author           : Carlos Ernesto Cordoba
' Created          : 13-11-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IServiceOrderAdminService
    Inherits IDisposable

    Function GetInvoiceDetailByServiceOrderDetailId(serviceOrderDetailId As Integer) As InvoiceDetail
    ''' <summary>
    ''' Obtener una orden de servicio por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetServiceOrder(ByVal code As String, ByVal audit As AuditMessage) As ServiceOrder
    ''' <summary>
    ''' Obtener una orden de servicio por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetServiceOrderById(id As Integer) As ServiceOrder
    ''' <summary>
    ''' metodo para obtener los detalles de la orden de servicios
    ''' </summary>
    ''' <param name="ServiceOrderId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetServiceOrderDetailByServiceOrderId(ServiceOrderId As Integer) As List(Of ServiceOrderDetail)

    ''' <summary>
    ''' Obtiene un detalle de orden de servicio por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Function GetServiceOrderDetailById(Id As Integer) As ServiceOrderDetail

    ''' <summary>
    ''' Guardar una orden de servicio
    ''' </summary>
    Function SaveServiceOrder(ByVal ServiceOrder As ServiceOrder, ByVal audit As AuditMessage, Optional ByVal idSequence As Int64 = 0, Optional idFolio As Integer = 0, Optional action As EActionServiceOrder = EActionServiceOrder.NoAction) As ActionResult(Of ServiceOrder)

    ''' <summary>
    ''' Obtiene un ingreso por codigo para utilizarlo en orden de servicio
    ''' </summary>
    ''' <param name="admissionNumber"></param>
    ''' <returns></returns>
    Function GetAdmissionByServiceOrder(admissionNumber As String) As String

    ''' <summary>
    ''' Método para listar medicamentos procesados por la central de mezclas para generación de la orden de servicios.
    ''' </summary>
    ''' <param name="admissionNumber">Número de ingreso</param>
    ''' <param name="isChild">Obtiene los registros hijos</param>
    ''' <returns>Listado de medicamentos</returns>
    Function GetProcessedMedicationItemsForBillingList(admissionNumber As String, isChild As Boolean) As List(Of SP_GetProcessedMedicationItemsForBilling_Result)

End Interface
