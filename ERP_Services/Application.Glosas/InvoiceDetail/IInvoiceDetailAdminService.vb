'***********************************************************************
' Assembly         : Application.Glosas
' Author           : RafaelPatiño
' Created          : 30-05-2013
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IInvoiceDetailAdminService
    Inherits IDisposable


    ''' <summary>
    ''' Funcion para cargar los detalle de cada factura por medio del numero de factura
    ''' </summary>
    ''' <param name="InvoiceNumber">numero de factura</param>
    ''' <returns>lista de detalles de factura</returns>
    ''' <remarks></remarks>
    Function ListGlosaInvoiceDetailByInvoiceNumber(InvoiceNumber As String, ByVal Modulo As String, conciliationId As Integer) As List(Of Domain.Entities.GlosaInvoiceDetail)
    ''' <summary>
    ''' Funcion para cargar los detalle quirurgicos
    ''' </summary>
    ''' <param name="InvoiceDetailId">Id del Detalle de Factura</param>
    ''' <returns>una lista de detalles de factura</returns>
    Function ListGlosaInvoiceDetailQX(InvoiceDetailId As String) As List(Of Domain.Entities.GlosaInvoiceDetailQX)
    ''' <summary>
    ''' Guarda una lista de Detalles de Factura
    ''' </summary>
    ''' <param name="ListInvoiceDetail">Lista de Detalles de Factura</param>
    ''' <returns>ActionResult</returns>
    Function SaveGlosaInvoiceDetail(ByVal ListInvoiceDetail As List(Of GlosaInvoiceDetail), ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Funcion para cargar los detalle quirurgicos
    ''' </summary>
    ''' <param name="Id">Id del Detalle de Factura</param>
    ''' <returns>una lista de detalles de factura</returns>
    Function GetGlosaInvoiceDetail(Id As String) As GlosaInvoiceDetail


    ''' <summary>
    ''' Funcion que retorna un objeto detalle de factura tipo qx
    ''' </summary>
    ''' <param name="InvoiceDetailQXId">Codigo del detalle qx</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetGlosaInvoiceDetailQX(InvoiceDetailQXId As String) As GlosaInvoiceDetailQX


    ''' <summary>
    ''' Función para traer una lista de detalles que tiene cada factura, en el momento de reiteracion  
    ''' </summary>
    ''' <param name="InvoiceNumber">Numero de Factura</param>
    ''' <returns>Lista de Detalles Factura</returns>
    Function ListGlosaInvoiceDetailByInvoiceNumberReiteration(InvoiceNumber As String, ByVal Modulo As String) As ActionResult(Of List(Of GlosaInvoiceDetail))

    ''' <summary>
    ''' Función para traer una lista de detalles no normativos que tiene cada factura, en el momento de reiteracion 
    ''' </summary>
    ''' <param name="InvoiceNumber">Numero de Factura</param>
    ''' <returns>Lista de Detalles Factura</returns>
    Function ListGlosaInvoiceDetailByInvoiceNumberReiterationNoNormative(InvoiceNumber As String, Modulo As String) As ActionResult(Of List(Of GlosaInvoiceDetail))

    ''' <summary>
    ''' Función para traer una lista de detalles que tiene cada factura de una reiteración que no han sido glosadas previamente
    ''' </summary>
    ''' <param name="InvoiceNumber"></param>
    ''' <param name="Modulo"></param>
    ''' <returns></returns>
    Function ListGlosaInvoiceDetailByInvoiceNumberReiterationWithOutMovements(InvoiceNumber As String, Modulo As String) As ActionResult(Of List(Of GlosaInvoiceDetail))

    ''' <summary>
    ''' funcion para cargar la estructura de glosa para la funcionalidad de Excel
    ''' </summary>
    ''' <param name="listInvoice"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListsStructureDetailInvocie(ByVal listInvoice As List(Of String)) As List(Of GlosaInvoiceDetail)

    ''' <summary>
    ''' Función para traer una lista de detalles que tiene cada factura 
    ''' </summary>
    ''' <param name="InvoiceNumber">Numero de Factura</param>
    ''' <returns>Lista de Detalles Factura</returns>
    Function ListGlosaInvoiceDetailForGeneralConciliation(InvoiceNumber As String) As List(Of GlosaInvoiceDetail)

End Interface
