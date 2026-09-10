'************************************************************
' Assembly         : Domain.Billing
' Author           : Carlos Ernesto Cordoba
' Created          : 13-11-2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities
Imports System.Data.Entity.Core.Objects

#End Region

Public Interface IServiceOrderRepository
    Inherits IRepository(Of ServiceOrder)

    Function GetServiceOrderByEntityId(EntityId As Integer) As ServiceOrder

    Function GetInvoiceDetailByServiceOrderDetailId(serviceOrderDetailId As Integer) As InvoiceDetail
    ''' <summary>
    ''' Obtener una orden de servicio por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetServiceOrder(ByVal code As String) As ServiceOrder
    ''' <summary>
    ''' Obtener una orden de servicio por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetServiceOrderById(id As Integer) As ServiceOrder
    ''' <summary>
    ''' Obtiene una orden de servicio y sus detalles por Id
    ''' </summary>
    ''' <param name="id">Id de la orden de servicio</param>
    ''' <returns>Orden de servicio</returns>
    Function GetServiceOrderByIdWithDetails(id As Integer) As ServiceOrder
    ''' <summary>
    ''' metodo para obtener los detalles de la orden de servicios
    ''' </summary>
    ''' <param name="ServiceOrderId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetServiceOrderDetailByServiceOrderId(ServiceOrderId As Integer) As List(Of ServiceOrderDetail)

    ''' <summary>
    ''' Obtiene el detalle de una orden de servicio por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Function GetServiceOrderDetailById(Id As Integer) As ServiceOrderDetail
    ''' <summary>
    ''' obtiene una orden de servicio por el codigo de la entidad que lo creo
    ''' </summary>
    ''' <param name="entityCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetServiceOrderByEntityCode(entityCode As String) As ServiceOrder

    Function GetServiceOrderByAdmissionCode(admissionCode As String) As ServiceOrder

    Function GetServiceOrderGeneratedByDispensing(code As String) As Integer

    ''' <summary>
    ''' Metodo para crear una orden de servicio a través de un SP
    ''' </summary>
    ''' <param name="ServiceOrderXml"></param>
    ''' <param name="UserCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GenerateServiceOrderSP(ServiceOrderXml As String, UserCode As String) As ObjectResult(Of SP_GenerateServiceOrder_Result)

    Function SP_GenerateDocuments(xmlData As String, codeUser As String) As SP_GenerateDocuments_Result

    ''' <summary>
    ''' Retorna medicamentos procesados para facturación como una lista.
    ''' </summary>
    ''' <param name="admissionNumber">Número de admisión</param>
    ''' <param name="isChild">Obtiene los registros hijos</param>
    ''' <returns>Listado de medicamentos</returns>
    Function GetProcessedMedicationItemsForBillingList(admissionNumber As String, isChild As Boolean) As List(Of SP_GetProcessedMedicationItemsForBilling_Result)

End Interface
