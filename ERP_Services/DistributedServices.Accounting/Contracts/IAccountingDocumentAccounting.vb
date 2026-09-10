#Region "Imports"
Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
#End Region

<ServiceContract>
Public Interface IAccountingDocumentAccounting
#Region "Functions"
    ''' <summary>
    ''' metodo para generar las homologaciones del comprobante contable
    ''' </summary>
    ''' <param name="accountingDocument"></param>
    ''' <param name="legalBookId"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GenerateHomologationsJournalVoucher(accountingDocument As Domain.Entities.JournalVouchers, legalBookId As Integer) As ActionResult(Of List(Of JournalVoucherDetails))
    ''' <summary>
    ''' Saves the accounting document.
    ''' </summary>
    ''' <param name="accountingDocument">The accounting document.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveAccountingDocumentAsync(ByVal accountingDocument As Domain.Entities.JournalVouchers) As Task(Of ActionMessageResult(Of JournalVouchers))

    ''' <summary>
    ''' funcion para obtener el documento contable por consecutibo
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetAccountingDocumenteByConsecutiveAsync(ByVal consecutive As String, ByVal Tracking As Boolean) As Task(Of Domain.Entities.JournalVouchers)

    ''' <summary>
    ''' funcion para obtener el documento contable por el id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetAccountingDocumenteById(ByVal id As String) As Domain.Entities.JournalVouchers

    ''' <summary>
    ''' Gets the accounting document by consecutive identifier document.
    ''' </summary>
    ''' <param name="consecutive">The consecutive.</param>
    ''' <param name="idDocument">The identifier document.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetAccountingDocumentByConsecutiveIdDocument(consecutive As Integer, idDocument As Integer) As Domain.Entities.JournalVouchers

    ''' <summary>
    ''' Gets the accounting document by consecutive identifier document, the journal voucher type and legal book.
    ''' </summary>
    ''' <param name="legalbookId"></param>
    ''' <param name="journalVoucherTypeId"></param>
    ''' <param name="consecutive"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetAccountingDocumentByConsecutiveAndJournalVoucherTypeIdAndLegalBookId(legalbookId As Integer, journalVoucherTypeId As Integer, consecutive As Integer) As Domain.Entities.JournalVouchers

    ''' <summary>
    ''' Gets the journal vouchers by identifier.
    ''' </summary>
    ''' <param name="idJournalVouchers">The identifier journal vouchers.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetJournalVouchersByIdAsync(idJournalVouchers As Long) As Task(Of JournalVouchers)

    ''' <summary>
    ''' funcion para obtener los comprobantes contables por estado
    ''' </summary>
    ''' <param name="Status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetJournalVourchersByStatus(ByVal Status As Integer, ByVal month As Integer, year As Integer) As List(Of SP_GetJournalVouchersByStatus_Result)

    ''' <summary>
    ''' Saves the list journal vouchers.
    ''' </summary>
    ''' <param name="ListIdJournalVouchers">The list identifier journal vouchers.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveListJournalVouchers(ByVal Status As Integer, ListIdJournalVouchers As String, ByVal month As Integer) As List(Of SP_ChangeStatusJournalVouchers_Result)
#End Region

    ''' <summary>
    ''' metodo para copiar y pegar detalles del comprobante contable
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SetJournalVoucherDetailsCopyPaste(LegalBookId As Integer, data As List(Of List(Of String))) As ActionResult(Of List(Of JournalVoucherDetails))


    <OperationContract>
    Function GetJournalVouchersByIdOnlyHead(id As Integer) As JournalVouchers

    <OperationContract>
    Function ValidateFileJournalVoucherDetails(LegalBookId As Integer, data As List(Of ImportFileRow)) As ActionResult(Of List(Of JournalVoucherDetails))

    ''' <summary>
    ''' Calcula la retencion de tipo variable
    ''' </summary>
    ''' <param name="BaseValue"></param>
    ''' <param name="MinBase"></param>
    ''' <param name="Percentaje"></param>
    ''' <param name="ThirdPartyId"></param>
    ''' <param name="MainAccountId"></param>
    ''' <returns></returns>
    <OperationContract>
    Function CalculateRetention(BaseValue As Decimal, MinBase As Decimal, Percentaje As Decimal, ThirdPartyId As Integer, MainAccountId As Integer) As ActionResult(Of Object)

    ''' <summary>
    ''' metodo para recalcular los saldos de contabilidad
    ''' </summary>
    ''' <param name="periodId"></param>
    ''' <param name="legalBookId"></param>
    ''' <param name="mainAccountId"></param>
    ''' <param name="validateMovement"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function RecalculateBalance(periodId As Integer, legalBookId As Integer, mainAccountId As Integer, validateMovement As Boolean, year As Integer) As ActionResult(Of List(Of Tuple(Of String, Integer)))

End Interface
