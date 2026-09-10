'**********************************************************************
' Assembly         : Aplication.Accouting
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 17-05-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region
''' <summary>
''' 
''' </summary>
Public Interface IAccountingDocumentAdminService
    Inherits IDisposable

#Region "Functions"
    ''' <summary>
    ''' metodo para generar las homologaciones del comprobante contable
    ''' </summary>
    ''' <param name="accountingDocument"></param>
    ''' <param name="legalBookId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GenerateHomologationsJournalVoucher(accountingDocument As Domain.Entities.JournalVouchers, legalBookId As Integer) As ActionResult(Of List(Of JournalVoucherDetails))

    Function ValidateFileJournalVoucherDetails(LegalBookId As Integer, data As List(Of ImportFileRow)) As ActionResult(Of List(Of JournalVoucherDetails))

    ''' <summary>
    ''' Calcula la retención de tipo variable
    ''' </summary>
    ''' <param name="BaseValue"></param>
    ''' <param name="MinBase"></param>
    ''' <param name="Percentaje"></param>
    ''' <param name="ThirdPartyId"></param>
    ''' <param name="MainAccountId"></param>
    ''' <returns></returns>
    Function CalculateRetention(BaseValue As Decimal, MinBase As Decimal, Percentaje As Decimal, ThirdPartyId As Integer, MainAccountId As Integer) As ActionResult(Of Object)

    Function GetJournalVouchersByIdOnlyHead(id As Integer) As JournalVouchers

    ''' <summary>
    ''' Saves the accounting document.
    ''' </summary>
    ''' <param name="accountingDocument">The accounting document.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <Obsolete("Esta función está obsoleta. Usa SaveAccountingDocumentAsync.", False)>
    Function SaveAccountingDocument(ByVal accountingDocument As Domain.Entities.JournalVouchers, ByVal audit As AuditMessage, Optional ByVal withCommit As Boolean = True) As ActionMessageResult(Of JournalVouchers)

    Function SaveAccountingDocumentAsync(ByVal accountingDocument As Domain.Entities.JournalVouchers, ByVal audit As AuditMessage, Optional ByVal withCommit As Boolean = True) As Task(Of ActionMessageResult(Of JournalVouchers))
    ''' <summary>
    ''' funcion para obtener el documento contable por consecutibo
    ''' </summary>
    ''' <returns></returns>
    Function GetAccountingDocumenteByConsecutiveAsync(ByVal consecutive As String, ByVal Tracking As Boolean) As Task(Of Domain.Entities.JournalVouchers)
    ''' <summary>
    ''' funcion para obtener el documento contable por el id
    ''' </summary>
    ''' <returns></returns>
    Function GetAccountingDocumenteById(ByVal id As Integer) As Domain.Entities.JournalVouchers

    ''' <summary>
    ''' Gets the accounting document by consecutive identifier document.
    ''' </summary>
    ''' <param name="consecutive">The consecutive.</param>
    ''' <param name="idDocument">The identifier document.</param>
    ''' <returns></returns>
    Function GetAccountingDocumentByConsecutiveIdDocument(ByVal consecutive As Integer, ByVal idDocument As Integer) As Domain.Entities.JournalVouchers
    ''' <summary>
    ''' Gets the accounting document by consecutive identifier document, the journal voucher type and legal book.
    ''' </summary>
    ''' <param name="legalbookId"></param>
    ''' <param name="journalVoucherTypeId"></param>
    ''' <param name="consecutive"></param>
    ''' <returns></returns>
    Function GetAccountingDocumentByConsecutiveAndJournalVoucherTypeIdAndLegalBookId(legalbookId As Integer, journalVoucherTypeId As Integer, consecutive As Integer) As Domain.Entities.JournalVouchers
    ''' <summary>
    ''' funcion para obtener el documento contable por id
    ''' </summary>
    ''' <param name="idJournalVouchers">el id del documento contable.</param>    
    ''' <returns></returns>
    Function GetJournalVouchersByIdAsync(ByVal idJournalVouchers As Long) As Task(Of JournalVouchers)

    ''' <summary>
    ''' funcion para obtener los comprobantes contables por estado
    ''' </summary>
    ''' <param name="Status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetJournalVourchersByStatus(ByVal Status As Integer, ByVal month As Integer, year As Integer) As List(Of SP_GetJournalVouchersByStatus_Result)

    ''' <summary>
    ''' Saves the list journal vouchers.
    ''' </summary>
    ''' <param name="ListIdJournalVouchers">The list identifier journal vouchers.</param>
    ''' <returns></returns>
    Function SaveListJournalVouchers(ByVal Status As Integer, ListIdJournalVouchers As String, ByVal month As Integer) As List(Of SP_ChangeStatusJournalVouchers_Result)

    ''' <summary>
    ''' metodo para copiar y pegar detalles del comprobante contable
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetJournalVoucherDetailsCopyPaste(LegalBookId As Integer, data As List(Of List(Of String))) As ActionResult(Of List(Of JournalVoucherDetails))
#End Region

End Interface
