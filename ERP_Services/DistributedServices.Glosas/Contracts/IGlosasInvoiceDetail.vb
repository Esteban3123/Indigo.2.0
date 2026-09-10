'***********************************************************************
' Assembly         : DistributedService.Glosas
' Author           : Rafael Eduardo Patiño
' Created          : 07-05-2013
'
' Copyright        : (c) . All rights reserved.
'*********************************************************************** 
Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface IGlosasInvoiceDetail



    ''' <summary>
    ''' Funcion para cargar los detalle de cada factura por medio del numero de factura
    ''' </summary>
    ''' <param name="InvoiceNumber">numero de factura</param>
    ''' <returns>lista de detalles de factura</returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function ListGlosaInvoiceDetailByInvoiceNumber(InvoiceNumber As String, ByVal Modulo As String, conciliationId As Integer, ByVal session As SessionValues) As List(Of Domain.Entities.GlosaInvoiceDetail)
    ''' <summary>
    ''' Funcion para cargar los detalle quirurgicos
    ''' </summary>
    ''' <param name="InvoiceDetailId">Id del Detalle de Factura</param>
    ''' <returns>una lista de detalles de factura</returns>
    <OperationContract>
    Function ListGlosaInvoiceDetailQX(InvoiceDetailId As String, ByVal session As SessionValues) As List(Of Domain.Entities.GlosaInvoiceDetailQX)


    ''' <summary>
    ''' lista de detalle de facturas
    ''' </summary>
    ''' <param name="listInvoice"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function ListsStructureDetailInvocie(listInvoice As List(Of String), ByVal session As SessionValues) As List(Of GlosaInvoiceDetail)

    ''' <summary>
    ''' Guarda una lista de Detalles de Factura
    ''' </summary>
    ''' <param name="ListInvoiceDetail">Lista de Detalles de Factura</param>
    ''' <returns>ActionResult</returns>
    <OperationContract>
    Function SaveGlosaInvoiceDetail(ByVal ListInvoiceDetail As List(Of GlosaInvoiceDetail), ByVal session As SessionValues) As ActionResult


    ''' <summary>
    ''' Funcion para cargar los detalle quirurgicos
    ''' </summary>
    ''' <param name="Id">Id del Detalle de Factura</param>
    ''' <returns>una lista de detalles de factura</returns>
    <OperationContract>
    Function GetGlosaInvoiceDetail(Id As String, ByVal session As SessionValues) As GlosaInvoiceDetail

   ''' <summary>
    ''' Funcion que retorna un objeto detalle de factura tipo qx
    ''' </summary>
    ''' <param name="InvoiceDetailQXId">Codigo del detalle qx</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function GetGlosaInvoiceDetailQX(InvoiceDetailQXId As String, ByVal session As SessionValues) As GlosaInvoiceDetailQX


    ''' <summary>
    ''' Función para traer una lista de detalles que tiene cada factura, en el momento de reiteracion  
    ''' </summary>
    ''' <param name="InvoiceNumber">Numero de Factura</param>
    ''' <returns>Lista de Detalles Factura</returns>
    <OperationContract>
    Function ListGlosaInvoiceDetailByInvoiceNumberReiteration(InvoiceNumber As String, ByVal Modulo As String, ByVal session As SessionValues) As ActionResult(Of List(Of GlosaInvoiceDetail))

    ''' <summary>
    ''' Función para traer una lista de detalles no normativos que tiene cada factura, en el momento de reiteracion 
    ''' </summary>
    ''' <param name="InvoiceNumber">Numero de Factura</param>
    ''' <returns>Lista de Detalles Factura</returns>
    <OperationContract>
    Function ListGlosaInvoiceDetailByInvoiceNumberReiterationNoNormative(InvoiceNumber As String, Modulo As String, ByVal session As SessionValues) As ActionResult(Of List(Of GlosaInvoiceDetail))

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="InvoiceNumber"></param>
    ''' <param name="Modulo"></param>
    ''' <returns></returns>
    <OperationContract>
    Function ListGlosaInvoiceDetailByInvoiceNumberReiterationWithOutMovements(InvoiceNumber As String, Modulo As String, session As SessionValues) As ActionResult(Of List(Of GlosaInvoiceDetail))

    ''' <summary>
    ''' Función para traer una lista de detalles que tiene cada factura 
    ''' </summary>
    ''' <param name="InvoiceNumber">Numero de Factura</param>
    ''' <returns>Lista de Detalles Factura</returns>
    <OperationContract>
    Function ListGlosaInvoiceDetailForGeneralConciliation(InvoiceNumber As String, ByVal session As SessionValues) As List(Of GlosaInvoiceDetail)

End Interface
