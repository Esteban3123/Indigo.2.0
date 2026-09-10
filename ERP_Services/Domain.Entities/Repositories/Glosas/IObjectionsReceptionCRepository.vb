'***********************************************************************
' Assembly         : Domain.Glosas
' Author           : RafaelPatiño
' Created          : 09-04-2013
'
' Last Modified By : RafaelPatiño
' Last Modified On : 20-04-2013
'
' Copyright        : (c) . All rights reserved.
'*********************************************************************** 
Imports Domain.Entities
Imports Domain.Base
Imports System.Data

Public Interface IObjectionsReceptionCRepository
    Inherits IRepository(Of GlosaObjectionsReceptionC)

    ''' <summary>
    ''' Lista las recepciones de objeciones por su estado
    ''' </summary>
    ''' <param name="status">Estado de las objeciones a listar</param>
    ''' <returns>Lista de objeciones</returns>
    Function ListObjectionsReceptionCByStatus(status As String) As List(Of GlosaObjectionsReceptionC)

    ''' <summary>
    ''' Lista de todas las recepciones de objeciones
    ''' </summary>
    ''' <returns>lista de Objeciones Recepcionadas </returns>
    Function ListAllObjectionsReceptionC() As List(Of GlosaObjectionsReceptionC)

    ''' <summary>
    ''' obtener una objecion por el codigo 
    ''' </summary>
    ''' <param name="codeObjectionReceptionC">recibe el codigo de la objecion</param>
    ''' <returns>una objecion recepcionada</returns>
    Function GetObjection(ByVal codeObjectionReceptionC As String, Optional tracking As Boolean = True) As GlosaObjectionsReceptionC

    ''' <summary>
    ''' Obtiene una objecion completa con sus agregados por su numero de Id
    ''' </summary>
    ''' <param name="id">Id de la objecion</param>
    ''' <returns>La objecion</returns>
    Function GetObjectionCWithAgregatesById(id As String) As GlosaObjectionsReceptionC

    ''' <summary>
    ''' Obtiene una objecion sin sus agregados por su numero de Id
    ''' </summary>
    ''' <param name="id">Id de la objecion</param>
    ''' <returns>La objecion</returns>
    Function GetObjectionCWithoutAgregatesById(id As Integer) As GlosaObjectionsReceptionC

    ''' <summary>
    ''' lista las facturas a obgetar de una entidad por medio de un SP
    ''' </summary>
    ''' <param name="nameContainer">es el nombre del contenedor a BD del tercero de la que se obtendran las facturas</param>
    ''' <param name="nit">es el nit de la entidad a listar facturas</param>
    ''' <param name="InvoiceNumber">es el nit de la entidad a listar facturas</param>
    '''  <param name="stringSQl">cadena Sql de Filtro avanzados</param>
    ''' <param name="IndigoCompany">numero de contenedor</param>
    ''' <param name="TopQuery">Cantidad de Registro a retornar</param>
    ''' <returns>una lista de todas las facturas de cierta entidad de salud</returns>
    Function ListAllInvoce(ByVal nameContainer As String, ByVal nit As String, ByVal InvoiceNumber As String, ByVal IndigoCompany As String, HISContainer As String, ByVal stringSQl As String, ByVal TopQuery As String, ByVal FlagNotConfirmInvoice As String) As List(Of SP_invoiceList_Result)

    ''' <summary>
    ''' cantidad de registro a retornar por el sp lista facturas
    ''' </summary>
    ''' <param name="codeContainer">es el nombre del contenedor a BD del tercero de la que se obtendran las facturas</param>
    ''' <param name="nit">es el nit de la entidad a listar facturas</param>
    ''' <param name="InvoiceNumber">es el nit de la entidad a listar facturas</param>
    ''' <param name="stringSQl">cadena Sql de Filtro avanzados</param>
    ''' <returns>cantidad de registro a retornar por el sp lista facturas</returns>
    Function CountListAllInvoice(codeContainer As String, nit As String, InvoiceNumber As String, ByVal IndigoCompany As String, HISContainer As String, ByVal stringSQl As String) As Integer

    ''' <summary>
    ''' carga una factura 
    ''' </summary>
    ''' <param name="nameContainer">recibe el nombre del contenedor</param>
    ''' <param name="nit">nit del tercro a buscar facturas</param>
    ''' <param name="InvoiceNumber">numero de factura</param>
    ''' <returns>una factura</returns>
    Function GetInvoice(ByVal nameContainer As String, ByVal nit As String, ByVal InvoiceNumber As String, ByVal IndigoCompany As String, HISContainer As String, ByVal stringSQl As String, ByVal FlagNotConfirmInvoice As String) As SP_invoiceList_Result


    ''' <summary>
    ''' obtiene una lista del detalle de la facturas
    ''' </summary>
    ''' <param name="container">nombre del contenedor</param>
    ''' <param name="invoiceNumber">numero factura</param>
    ''' <param name="consecutiveNumber">numero consecutivo de ingreso</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListInvoiceDetail(ByVal container As String, ByVal HISContainer As String, ByVal SecurityContainer As String, ByVal invoiceNumber As String, ByVal consecutiveNumber As String) As List(Of SP_invoiceDetailList_Result)

    ''' <summary>
    ''' obtiene una lista de detalle de facturas de servico quirurgicos
    ''' </summary>
    ''' <param name="container">nombre del contenedor de datos</param>
    ''' <param name="consecutiveNumber">numero consecutivo</param>
    ''' <param name="ServiceOrder">orden de servicio</param>
    ''' <returns>lista de detalle quirurgicos</returns>
    Function ListInvoiceDetailListQX(ByVal container As String, ByVal consecutiveNumber As String, ByVal ServiceOrder As String, ByVal ServiceCode As String, ByVal consecutiveOrder As String, ByVal ServiceNumber As String, ByVal ConsecutivoInventory As String) As List(Of SP_invoiceDetailListQX_Result)


    ''' <summary>
    ''' lista del detalle de una factura mediante un SP
    ''' </summary>
    ''' <param name="nameContainer">nombre del contenedor o BD a cargar Detalles de facturas</param>
    ''' <param name="invoiceNumber">numero de factura</param>
    ''' <param name="consecutiveNumber">numero consecutivo</param>
    ''' <returns>una lista del detalle de una factura</returns>
    ''' <remarks></remarks>
    Function ListInvoiceDetailFOX(nameContainer As String, HISContainer As String, SecurityContainer As String, invoiceNumber As String, consecutiveNumber As String) As List(Of SP_invoiceDetailList__FOX_Result)

    Function ListInvoiceDetailNET(nameContainer As String, HISContainer As String, SecurityContainer As String, invoiceNumber As String, consecutiveNumber As String) As List(Of SP_invoiceDetailList__NET_Result)


    Function ListInvoiceDetailNAVITEINTEGRATION(nameContainer As String, HISContainer As String, SecurityContainer As String, invoiceNumber As String, consecutiveNumber As String) As List(Of SP_invoiceDetailList_NAVITEINTEGRATION_Result)


    ''' <summary>
    ''' obtiene una lista de detalle de facturas de servico quirurgicos proveniente de 
    ''' </summary>
    ''' <param name="container">nombre del contenedor de datos</param>
    ''' <param name="consecutiveNumber">numero consecutivo</param>
    ''' <param name="ServiceOrder">orden de servicio</param>
    ''' <returns>lista de detalle quirurgicos</returns>
    Function ListInvoiceDetailListQXFOX(container As String, consecutiveNumber As String, ServiceOrder As String, ServiceCode As String, consecutiveOrder As String, ServiceNumber As String, ConsecutivoInventory As String) As List(Of SP_invoiceDetailListQX__FOX_Result)

    Function ListInvoiceDetailListQXNET(container As String, consecutiveNumber As String, ServiceOrder As String, ServiceCode As String, consecutiveOrder As String, ServiceNumber As String, ConsecutivoInventory As String) As List(Of SP_invoiceDetailListQX__NET_Result)

    Function ListInvoiceDetailListQXNAVITEINTEGRATION(container As String, consecutiveNumber As String, ServiceOrder As String, ServiceCode As String, consecutiveOrder As String, ServiceNumber As String, ConsecutivoInventory As String) As List(Of SP_invoiceDetailListQX_NATIVEINTEGRATION_Result)


End Interface
