#Region "Imports"

Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

#End Region

<ServiceContract()>
Public Interface IPortfolioConciliationService
    ''' <summary>
    '''lista todas las conciliacionesa
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ListAllPortfolioConciliation(audit As AuditMessage) As List(Of PortfolioConciliation)

    ''' <summary>
    '''
    ''' </summary>
    ''' <param name="Consecutive"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetConciliationByConsecutive(Consecutive As String) As PortfolioConciliation

    ''' <summary>
    '''
    ''' </summary>
    ''' <param name="nameContainer"></param>
    ''' <param name="nit"></param>
    ''' <param name="InvoiceNumber"></param>
    ''' <param name="stringSQl"></param>
    ''' <param name="FlagNotConfirmInvoice"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetInvoice(ByVal nameContainer As String, nit As String, InvoiceNumber As String, ByVal stringSQl As String, ByVal FlagNotConfirmInvoice As String, session As SessionValues) As SP_invoiceList_Result

    ''' <summary>
    '''
    ''' </summary>
    ''' <param name="ListInvoices"></param>
    ''' <param name="Nit"></param>
    ''' <param name="container"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ValidateListInvoiceSp(ListInvoices As List(Of String), Nit As String, container As String, session As SessionValues) As ActionResult(Of List(Of PortfolioConciliationDetail))

    ''' <summary>
    '''
    ''' </summary>
    ''' <param name="nameContainer"></param>
    ''' <param name="nit"></param>
    ''' <param name="InvoiceNumber"></param>
    ''' <param name="session"></param>
    ''' <param name="stringSQl"></param>
    ''' <param name="TopQuery"></param>
    ''' <param name="FlagNotConfirmInvoice"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ListAllInvoice(nameContainer As String, nit As String, InvoiceNumber As String, session As SessionValues, stringSQl As String, TopQuery As String, ByVal FlagNotConfirmInvoice As String) As ActionResult(Of List(Of SP_invoiceList_Result))

    ''' <summary>
    ''' obtiene una lista de detalles
    ''' </summary>
    ''' <param name="ConciliationId">codigo de la recpcion</param>
    ''' <returns>una lista detalle de oficio</returns>
    <OperationContract()>
    Function GetListConciliationDetail(ByVal ConciliationId As Integer, ByVal session As SessionValues) As List(Of PortfolioConciliationDetail)

    ''' <summary>
    ''' Obtiene los movimientos de una factura hasta la fecha de corte 
    ''' </summary>
    ''' <param name="InvoiceNumber"></param>
    ''' <param name="ClosingDate"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetSP_PortfolioConciliation(InvoiceNumber As String, ClosingDate As Date, Session As SessionValues) As SP_PortfolioConciliation_Result

    ''' <summary>
    ''' Guarda o actualiza un registro
    ''' </summary>
    ''' <param name="PortfolioConciliation"></param>
    ''' <param name="idSequense"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SavePortfolioConciliation(PortfolioConciliation As PortfolioConciliation, idSequense As Int64, audit As AuditMessage) As ActionResult(Of PortfolioConciliation)

#Region "import data excel"
    ''' <summary>
    ''' 'Funcion para validar y crear moviminetos glosas apartir de la carga masiva de datos desde excel
    ''' </summary>
    ''' <param name="dtSet"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function ValidateExcelData(ByVal dtSet As DataSet, ByVal session As SessionValues) As ActionResult

#End Region

End Interface