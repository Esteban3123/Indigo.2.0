Imports Domain.Entities
Imports Domain.Base
Imports System.Data

Public Interface IPortfolioConciliationRepository
    Inherits IRepository(Of PortfolioConciliation)

    ''' <summary>
    ''' Lista de todas las recepciones de objeciones
    ''' </summary>
    ''' <returns>lista de Objeciones Recepcionadas </returns>
    Function ListAllPortfolioConciliation() As List(Of PortfolioConciliation)

    ''' <summary>
    ''' obtener una conciliacion por codigo
    ''' </summary>
    ''' <param name="Consecutive">recibe el codigo de la objecion</param>
    ''' <returns>una objecion recepcionada</returns>
    Function GetConciliationByConsecutive(ByVal Consecutive As String, Optional tracking As Boolean = True) As PortfolioConciliation

    ''' <summary>
    ''' Obtiene
    ''' </summary>
    ''' <param name="ConciliationId">Id de la objecion</param>
    ''' <returns>La objecion</returns>
    Function GetListConciliationDetail(ConciliationId As Integer) As List(Of PortfolioConciliationDetail)

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
    ''' Metodo para importar los movimientos de glosa a partir de un archivo de excel
    ''' </summary>
    ''' <param name="XmlObject"></param>
    ''' <returns></returns>
    Function SP_ImportExcelConciliation(XmlObject As String) As List(Of SP_ImportExcelConciliation_Result)

    ''' <summary>
    ''' carga una factura
    ''' </summary>
    ''' <param name="nameContainer">recibe el nombre del contenedor</param>
    ''' <param name="nit">nit del tercro a buscar facturas</param>
    ''' <param name="InvoiceNumber">numero de factura</param>
    ''' <returns>una factura</returns>
    Function GetInvoice(ByVal nameContainer As String, ByVal nit As String, ByVal InvoiceNumber As String, ByVal IndigoCompany As String, HISContainer As String, ByVal stringSQl As String, ByVal FlagNotConfirmInvoice As String) As SP_invoiceList_Result


    ''' <summary>
    ''' Obtiene los movimiento de una factura hasta la fecha de corte
    ''' </summary>
    ''' <param name="InvoiceNumber"></param>
    ''' <param name="ClosingDate"></param>
    ''' <returns></returns>
    Function GetSP_PortfolioConciliation(InvoiceNumber As String, ClosingDate As Date) As SP_PortfolioConciliation_Result

    ''' <summary>
    ''' obtiene una lista del detalle de la facturas
    ''' </summary>
    ''' <param name="container">nombre del contenedor</param>
    ''' <param name="invoiceNumber">numero factura</param>
    ''' <param name="consecutiveNumber">numero consecutivo de ingreso</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListInvoiceDetail(ByVal container As String, ByVal HISContainer As String, ByVal SecurityContainer As String, ByVal invoiceNumber As String, ByVal consecutiveNumber As String) As List(Of SP_invoiceDetailList_Result)

End Interface