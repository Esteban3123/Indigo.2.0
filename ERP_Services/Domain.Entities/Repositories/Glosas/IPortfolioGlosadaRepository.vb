Imports Domain.Entities
Imports Domain.Base
Imports System.Data

Public Interface IPortfolioGlosadaRepository
    Inherits IRepository(Of GlosaPortfolioGlosada)

    ''' <summary>
    ''' funcion que retorna una cartera glosada por id
    ''' </summary>
    ''' <param name="id">id de la cartera  glosada</param>
    ''' <returns>Una cartera Glosada</returns>
    Function GetPortfolioGlosadaById(id As Integer) As GlosaPortfolioGlosada

    ''' <summary>
    ''' Función que obtiene registros de facturas.
    ''' </summary>
    ''' <param name="Factura">Numero Factura</param>
    ''' <param name="Nit"></param>
    ''' <param name="listStatus"></param>
    ''' <returns>Lista Cartera Glosa</returns>
    Function ListInvoicesByNumber(Factura As String, Nit As String, ByVal listStatus As List(Of String)) As GlosaPortfolioGlosada
    ''' <summary>
    ''' obtiene una cartera glosada por Nit
    ''' </summary>
    ''' <param name="Nit">Nit de la objecion</param>
    ''' <returns>Objeto Cartera Glosa</returns>
    Function ListConfirmGlosaPortfolio(Nit As String) As List(Of GlosaPortfolioGlosada)
    ''' <summary>
    ''' funcion que retorna una cartyera glosada, para validar la existencia de una factura en un oficio
    ''' </summary>
    ''' <param name="InvoiceNumber">Numero de factura</param>
    ''' <returns>Una cartera Glosada</returns>
    Function GetPortfolioGlosada(InvoiceNumber As String) As GlosaPortfolioGlosada

    ''' <summary>
    ''' funcion que retorna una cartera glosada, con agregados para la realizacion de interfacez
    ''' </summary>
    ''' <param name="InvoiceNumber">Numero de factura</param>
    ''' <returns>Observacion de factura a persistir</returns>
    Function GetPortfolioGlosadaWithAggregates(InvoiceNumber As String) As GlosaPortfolioGlosada
    ''' <summary>
    ''' obtiene una lista de cartera donde el estado indique
    ''' que la factura esta con estado pendiente envío de oficio
    ''' </summary>
    ''' <returns>Lista de Objetos Cartera Glosada</returns>
    Function ListPortfolioWithStateSendDocument() As List(Of GlosaPortfolioGlosada)

    ''' <summary>
    ''' Funcion para Listar las facturas que esta Lista para ser conciliadas
    ''' </summary>
    ''' <param name="Nit">entidad de la facturas</param>
    ''' <param name="DateInicial">fecha inicial</param>
    ''' <param name="DateEND">fecha final</param>
    ''' <returns>lista de facturas a conciliar</returns>
    ''' <remarks></remarks>
    Function ListGlosaPortfolioExportExcel(Nit As String, ByVal DateInicial As Date, ByVal DateEND As Date) As List(Of GlosaPortfolioGlosada)

    ''' <summary>
    ''' Funcion para cargar una lista de facturas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListPortfolio(ByVal listinvoice As List(Of String)) As List(Of GlosaPortfolioGlosada)


    ''' <summary>
    ''' funcion que retorna una cartera glosada sin agregados
    ''' </summary>
    ''' <param name="InvoiceNumber">Numero de factura</param>
    ''' <returns>Observacion de factura a persistir</returns>
    Function GetPortfolioGlosadaWithoutAgregates(InvoiceNumber As String) As GlosaPortfolioGlosada

End Interface
