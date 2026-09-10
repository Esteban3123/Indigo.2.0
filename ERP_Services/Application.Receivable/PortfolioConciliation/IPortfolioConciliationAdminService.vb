#Region "Imports"

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

#End Region

Public Interface IPortfolioConciliationAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista de todas las objeciones recepcionadas
    ''' </summary>
    ''' <returns>lista de Objeciones Recepcionadas </returns>
    Function ListAllPortfolioConciliation() As List(Of PortfolioConciliation)

    ''' <summary>
    ''' obtener una Conciliacion por el codigo
    ''' </summary>
    ''' <param name="Consecutive">recibe el codigo de la objecion</param>
    ''' <returns>una objecion recepcionada</returns>
    Function GetConciliationByConsecutive(ByVal Consecutive As String) As PortfolioConciliation

    ''' <summary>
    ''' carga una factura
    ''' </summary>
    ''' <param name="nameContainer">recibe el nombre del contenedor</param>
    ''' <param name="nit">nit del tercro a buscar facturas</param>
    ''' <param name="InvoiceNumber">numero de factura</param>
    ''' <returns>una factura</returns>
    Function GetInvoice(ByVal nameContainer As String, nit As String, InvoiceNumber As String, ByVal IndigoCompany As String, ByVal stringSQl As String, ByVal FlagNotConfirmInvoice As String, session As SessionValues) As SP_invoiceList_Result

    ''' <summary>
    ''' Función para validar y agregar facturas desde el sp
    ''' </summary>
    ''' <param name="ListInvoices">Lista de facturas</param>
    ''' <param name="Nit">Numero de Nit</param>
    ''' <param name="container">nombre Contenedor</param>
    ''' <returns>Lista de Factura </returns>
    Function ValidateListInvoiceSp(ListInvoices As List(Of String), Nit As String, container As String, Session As SessionValues) As ActionResult(Of List(Of PortfolioConciliationDetail))

    ''' <summary>
    ''' Lista las facturas por contenedor y nit de la entidad ademas del total de registro
    ''' </summary>
    ''' <param name="nameContainer">recibe el nombre del contenedor</param>
    ''' <param name="nit">nit del tercro a buscar facturas</param>
    ''' <param name="InvoiceNumber">Numero de factura</param>
    ''' <param name="IndigoCompany">numero de contenedor</param>
    '''  <param name="stringSQl">cadena Sql de Filtro avanzados</param>
    ''' <param name="TopQuery">Cantidad de Registro a retornar</param>
    ''' <returns>Un objeto result que tiene una lista de facturas y el conteo de las misma</returns>
    Function ListAllInvoice(nameContainer As String, nit As String, InvoiceNumber As String, IndigoCompany As String, ByVal stringSQl As String, ByVal TopQuery As String, ByVal FlagNotConfirmInvoice As String, session As SessionValues) As ActionResult(Of List(Of Domain.Entities.SP_invoiceList_Result))

    ''' <summary>
    ''' retorna una lista de detalles de facturas
    ''' </summary>
    ''' <param name="container">nombre de contenedor de datos</param>
    ''' <param name="invoiceNumber">numero de la factura a traer detalle</param>
    ''' <param name="ingressNumber">numero de ingreso</param>
    ''' <returns>lista detalle facturas</returns>
    Function ListInvoiceDetails(ByVal container As String, invoiceNumber As String, ingressNumber As String, session As SessionValues) As List(Of SP_invoiceDetailList_Result)

    ''' <summary>
    ''' 'Funcion para validar y crear moviminetos glosas apartir de la carga masiva de datos desde excel
    ''' </summary>
    ''' <param name="dtSet"></param>
    ''' <param name="SessionValues"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidateExcelData(ByVal dtSet As DataSet, SessionValues As SessionValues) As ActionResult

    ''' <summary>
    ''' obtiene una lista de detalles de una recepcion
    ''' </summary>
    ''' <param name="ConciliationId">codigo de la recpcion</param>
    ''' <returns>una lista detalle de oficio</returns>
    Function GetListConciliationDetail(ConciliationId As Integer) As List(Of PortfolioConciliationDetail)

    ''' <summary>
    ''' Obtiene los movmientos de una factura hasta la fecha de corte deseada
    ''' </summary>
    ''' <param name="InvoiceNumber"></param>
    ''' <param name="ClosingDate"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Function GetSP_PortfolioConciliation(InvoiceNumber As String, ClosingDate As Date, Session As SessionValues) As SP_PortfolioConciliation_Result

    ''' <summary>
    ''' guardar
    ''' </summary>
    ''' <param name="PortfolioConciliation">la recepción de la objeción</param>
    ''' <param name="audit">mensaje de auditoria</param>
    ''' <returns>valor de guardado</returns>
    Function SavePortfolioConciliation(ByVal PortfolioConciliation As PortfolioConciliation, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of PortfolioConciliation)

End Interface