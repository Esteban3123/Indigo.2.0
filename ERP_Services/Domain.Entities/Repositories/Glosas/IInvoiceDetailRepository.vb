'***********************************************************************
' Assembly         : Domain.Glosas
' Author           : RafaelPatiño
' Created          : 14-06-2013
'
' Last Modified By : 
' Last Modified On :
'
' Copyright        : (c) . All rights reserved.
'*********************************************************************** 



Imports Domain.Entities
Imports Domain.Base
Imports System.Data

Public Interface IInvoiceDetailRepository
    Inherits IRepository(Of GlosaInvoiceDetail)

    ''' <summary>
    ''' Funcion para cargar los detalle de cada factura con quirurgicos
    ''' </summary>
    ''' <param name="InvoiceNumber">Numero de Factura</param>
    ''' <returns>una lista de detalles de factura</returns>
    Function ListGlosaInvoiceDetail(InvoiceNumber As String) As List(Of GlosaInvoiceDetail)

    ''' <summary>
    ''' Funcion para cargar los detalle de factura sin agregados
    ''' </summary>
    ''' <param name="InvoiceNumber">id del detalle, es decir de la factura</param>
    ''' <returns>una lista de detalles de factura</returns>
    Function ListGlosaInvoiceDetailwithoutAggregates(InvoiceNumber As String) As List(Of GlosaInvoiceDetail)

    ''' <summary>
    ''' Función para traer una lista de detalles que tiene cada factura 
    ''' </summary>
    ''' <param name="InvoiceNumber">Numero de Factura</param>
    ''' <returns>Lista de Detalles Factura</returns>
    Function ListGlosaInvoiceDetailByInvoiceNumber(InvoiceNumber As String, ByVal Modulo As String, conciliationId As Integer) As List(Of GlosaInvoiceDetail)

    ''' <summary>
    ''' Funcion para cargar un detalle de Factura
    ''' </summary>
    ''' <param name="Id">Codigo detalle de factura</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetGlosaInvoiceDetail(Id As String) As GlosaInvoiceDetail
    ''' <summary>
    ''' Validar Movimientos
    ''' </summary>
    ''' <param name="InvoiceNumber">Numero de Factura</param>
    ''' <returns>Boolean</returns>
    Function ValidateInvoices(InvoiceNumber As String) As Boolean

    ''' <summary>
    ''' Función para traer una lista de detalles que tiene cada factura, en el momento de reiteracion  
    ''' </summary>
    ''' <param name="InvoiceNumber">Numero de Factura</param>
    ''' <returns>Lista de Detalles Factura</returns>
    Function ListGlosaInvoiceDetailByInvoiceNumberReiteration(InvoiceNumber As String, ByVal Modulo As String) As List(Of GlosaInvoiceDetail)

    ''' <summary>
    ''' funcion para cargar la estructura de glosa para la funcionalidad de Excel
    ''' </summary>
    ''' <param name="listInvoice"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListsStructureDetailInvocie(ByVal listInvoice As List(Of String)) As List(Of GlosaInvoiceDetail)

    ''' <summary>
    ''' Funcion para cargar los detalles de factura con moviminetos y pago parciales
    ''' </summary>
    ''' <param name="InvoiceNumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListGlosaInvoiceDetailWithMovementAndPartialPayments(InvoiceNumber As String) As List(Of GlosaInvoiceDetail)

    ''' <summary>
    ''' Funcion para validar que el id detalle de factura exista en la BD
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidateIdDetail(id As String) As Boolean

    ''' <summary>
    '''  Funcion para validar que el id detalle de factura QX exista en la BD
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidateIdDetailQx(id As String) As Boolean

    ''' <summary>
    ''' Función para traer una lista de detalles que tiene cada factura 
    ''' </summary>
    ''' <param name="InvoiceNumber">Numero de Factura</param>
    ''' <returns>Lista de Detalles Factura</returns>
    Function ListGlosaInvoiceDetailForGeneralConciliation(InvoiceNumber As String) As List(Of GlosaInvoiceDetail)

    ''' <summary>
    ''' Función para traer una lista de detalles que tiene cada factura de una reiteración que no han sido glosadas previamente
    ''' </summary>
    ''' <param name="InvoiceNumber"></param>
    ''' <param name="Modulo"></param>
    ''' <returns></returns>
    Function ListGlosaInvoiceDetailByInvoiceNumberReiterationWithOutMovements(InvoiceNumber As String, Modulo As String) As List(Of GlosaInvoiceDetail)

    ''' <summary>
    ''' Función para traer una lista de detalles no normativos que tiene cada factura, en el momento de reiteracion 
    ''' </summary>
    ''' <param name="InvoiceNumber">Numero de Factura</param>
    ''' <returns>Lista de Detalles Factura</returns>
    Function ListGlosaInvoiceDetailByInvoiceNumberReiterationNoNormative(InvoiceNumber As String, Modulo As String) As List(Of GlosaInvoiceDetail)
End Interface
