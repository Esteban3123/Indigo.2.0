Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base
Imports Microsoft.Practices.Unity
Imports Application.Accounting

Partial Public Class AccountingService
    ''' <summary>
    ''' metodo para generar las homologaciones del comprobante contable
    ''' </summary>
    ''' <param name="accountingDocument"></param>
    ''' <param name="legalBookId"></param>
    ''' <returns></returns>
    Public Function GenerateHomologationsJournalVoucher(accountingDocument As Domain.Entities.JournalVouchers, legalBookId As Integer) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.JournalVoucherDetails)) Implements IAccountingDocumentAccounting.GenerateHomologationsJournalVoucher
        Using service As IAccountingDocumentAdminService = Container.Current.Resolve(Of IAccountingDocumentAdminService)()
            Return service.GenerateHomologationsJournalVoucher(accountingDocument, legalBookId)
        End Using
    End Function
    ''' <summary>
    ''' funcion para obtener el documento contable por consecutibo
    ''' </summary>
    ''' <param name="consecutive"></param>
    ''' <returns></returns>
    Public Async Function GetAccountingDocumenteByConsecutiveAsync(consecutive As String, ByVal Tracking As Boolean) As Task(Of Domain.Entities.JournalVouchers) Implements IAccountingDocumentAccounting.GetAccountingDocumenteByConsecutiveAsync
        Using service As IAccountingDocumentAdminService = Container.Current.Resolve(Of IAccountingDocumentAdminService)()
            Return Await service.GetAccountingDocumenteByConsecutiveAsync(consecutive, Tracking)
        End Using
    End Function

    ''' <summary>
    ''' funcion para obtener el documento contable por el id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetAccountingDocumenteById(id As String) As Domain.Entities.JournalVouchers Implements IAccountingDocumentAccounting.GetAccountingDocumenteById
        Using service As IAccountingDocumentAdminService = Container.Current.Resolve(Of IAccountingDocumentAdminService)()
            Return service.GetAccountingDocumenteById(id)
        End Using
        'Return _AccountingDocumentAdminService.GetAccountingDocumenteById(id)
    End Function

    ''' <summary>
    ''' Saves the accounting document.
    ''' </summary>
    ''' <param name="accountingDocument">The accounting document.</param>
    ''' <returns></returns>
    Public Async Function SaveAccountingDocumentAsync(accountingDocument As Domain.Entities.JournalVouchers) As Task(Of Entities.ActionMessageResult(Of Domain.Entities.JournalVouchers)) Implements IAccountingDocumentAccounting.SaveAccountingDocumentAsync
        Using service As IAccountingDocumentAdminService = Container.Current.Resolve(Of IAccountingDocumentAdminService)()
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return Await service.SaveAccountingDocumentAsync(accountingDocument, audit)
        End Using
    End Function

    ''' <summary>
    ''' Gets the accounting document by consecutive identifier document.
    ''' </summary>
    ''' <param name="consecutive">The consecutive.</param>
    ''' <param name="idDocument">The identifier document.</param>
    ''' <returns></returns>
    Public Function GetAccountingDocumentByConsecutiveIdDocument(consecutive As Integer, idDocument As Integer) As Domain.Entities.JournalVouchers Implements IAccountingDocumentAccounting.GetAccountingDocumentByConsecutiveIdDocument
        Using service As IAccountingDocumentAdminService = Container.Current.Resolve(Of IAccountingDocumentAdminService)()
            Return service.GetAccountingDocumentByConsecutiveIdDocument(consecutive, idDocument)
        End Using
    End Function

    ''' <summary>
    ''' Gets the accounting document by consecutive identifier document, the journal voucher type and legal book.
    ''' </summary>
    ''' <param name="legalbookId"></param>
    ''' <param name="journalVoucherTypeId"></param>
    ''' <param name="consecutive"></param>
    ''' <returns></returns>
    Public Function GetAccountingDocumentByConsecutiveAndJournalVoucherTypeIdAndLegalBookId(legalbookId As Integer, journalVoucherTypeId As Integer, consecutive As Integer) As Domain.Entities.JournalVouchers Implements IAccountingDocumentAccounting.GetAccountingDocumentByConsecutiveAndJournalVoucherTypeIdAndLegalBookId
        Using service As IAccountingDocumentAdminService = Container.Current.Resolve(Of IAccountingDocumentAdminService)()
            Return service.GetAccountingDocumentByConsecutiveAndJournalVoucherTypeIdAndLegalBookId(legalbookId, journalVoucherTypeId, consecutive)
        End Using
    End Function

    ''' <summary>
    ''' Gets the journal vouchers by identifier.
    ''' </summary>
    ''' <param name="idJournalVouchers">The identifier journal vouchers.</param>
    ''' <returns></returns>
    Public Async Function GetJournalVouchersByIdAsync(idJournalVouchers As Long) As Task(Of Domain.Entities.JournalVouchers) Implements IAccountingDocumentAccounting.GetJournalVouchersByIdAsync
        Using service As IAccountingDocumentAdminService = Container.Current.Resolve(Of IAccountingDocumentAdminService)()
            Return Await service.GetJournalVouchersByIdAsync(idJournalVouchers)
        End Using
    End Function

    ''' <summary>
    ''' funcion para obtener los comprobantes contables por estado
    ''' </summary>
    ''' <param name="Status"></param>
    ''' <param name="month"></param>
    ''' <returns></returns>
    Public Function GetJournalVourchersByStatus(Status As Integer, ByVal month As Integer, year As Integer) As List(Of Domain.Entities.SP_GetJournalVouchersByStatus_Result) Implements IAccountingDocumentAccounting.GetJournalVourchersByStatus
        Using service As IAccountingDocumentAdminService = Container.Current.Resolve(Of IAccountingDocumentAdminService)()
            Return service.GetJournalVourchersByStatus(Status, month, year)
        End Using
        'Return _AccountingDocumentAdminService.GetJournalVourchersByStatus(Status, month, year)
    End Function

    ''' <summary>
    ''' Saves the list journal vouchers.
    ''' </summary>
    ''' <param name="Status"></param>
    ''' <param name="ListIdJournalVouchers">The list identifier journal vouchers.</param>
    ''' <param name="month"></param>
    ''' <returns></returns>
    Public Function SaveListJournalVouchers(Status As Integer, ListIdJournalVouchers As String, month As Integer) As List(Of Domain.Entities.SP_ChangeStatusJournalVouchers_Result) Implements IAccountingDocumentAccounting.SaveListJournalVouchers
        Using service As IAccountingDocumentAdminService = Container.Current.Resolve(Of IAccountingDocumentAdminService)()
            Return service.SaveListJournalVouchers(Status, ListIdJournalVouchers, month)
        End Using
        'Return _AccountingDocumentAdminService.SaveListJournalVouchers(Status, ListIdJournalVouchers, month)
    End Function


    ''' <summary>
    ''' metodo para copiar y pegar detalles del comprobante contable
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    Public Function SetJournalVoucherDetailsCopyPaste(LegalBookId As Integer, data As List(Of List(Of String))) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.JournalVoucherDetails)) Implements IAccountingDocumentAccounting.SetJournalVoucherDetailsCopyPaste
        Using service As IAccountingDocumentAdminService = Container.Current.Resolve(Of IAccountingDocumentAdminService)()
            Return service.SetJournalVoucherDetailsCopyPaste(LegalBookId, data)
        End Using
        'Return _AccountingDocumentAdminService.SetJournalVoucherDetailsCopyPaste(data)
    End Function

    Public Function GetJournalVouchersByIdOnlyHead(id As Integer) As Domain.Entities.JournalVouchers Implements IAccountingDocumentAccounting.GetJournalVouchersByIdOnlyHead
        Using service As IAccountingDocumentAdminService = Container.Current.Resolve(Of IAccountingDocumentAdminService)()
            Return service.GetJournalVouchersByIdOnlyHead(id)
        End Using
        'Return _AccountingDocumentAdminService.GetJournalVouchersByIdOnlyHead(id)
    End Function

    Public Function ValidateFileJournalVoucherDetails(LegalBookId As Integer, data As List(Of Domain.Base.Entities.ImportFileRow)) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.JournalVoucherDetails)) Implements IAccountingDocumentAccounting.ValidateFileJournalVoucherDetails
        Using service As IAccountingDocumentAdminService = Container.Current.Resolve(Of IAccountingDocumentAdminService)()
            Return service.ValidateFileJournalVoucherDetails(LegalBookId, data)
        End Using
        'Return _AccountingDocumentAdminService.ValidateFileJournalVoucherDetails(data)
    End Function

    ''' <summary>
    ''' Calcula la retencion de tipo variable
    ''' </summary>
    ''' <param name="BaseValue"></param>
    ''' <param name="MinBase"></param>
    ''' <param name="Percentaje"></param>
    ''' <param name="ThirdPartyId"></param>
    ''' <param name="MainAccountId"></param>
    ''' <returns></returns>
    Public Function CalculateRetention(BaseValue As Decimal, MinBase As Decimal, Percentaje As Decimal, ThirdPartyId As Integer, MainAccountId As Integer) As Domain.Base.Entities.ActionResult(Of Object) Implements IAccountingDocumentAccounting.CalculateRetention
        Using service As IAccountingDocumentAdminService = Container.Current.Resolve(Of IAccountingDocumentAdminService)()
            Return service.CalculateRetention(BaseValue, MinBase, Percentaje, ThirdPartyId, MainAccountId)
        End Using
        'Return _AccountingDocumentAdminService.CalculateRetention(BaseValue, MinBase, Percentaje, ThirdPartyId, MainAccountId)
    End Function

    ''' <summary>
    ''' metodo para recalcular los saldos de contabilidad
    ''' </summary>
    ''' <param name="periodId"></param>
    ''' <param name="legalBookId"></param>
    ''' <param name="mainAccountId"></param>
    ''' <param name="validateMovement"></param>
    ''' <returns></returns>
    Public Function RecalculateBalance(periodId As Integer, legalBookId As Integer, mainAccountId As Integer, validateMovement As Boolean, year As Integer) As Domain.Base.Entities.ActionResult(Of List(Of Tuple(Of String, Integer))) Implements IAccountingDocumentAccounting.RecalculateBalance
        Using service As IAccountingBalanceAdminService = Container.Current.Resolve(Of IAccountingBalanceAdminService)()
            Return service.RecalculateBalance(periodId, legalBookId, mainAccountId, validateMovement, year)
        End Using
        'Return _AccountingBalanceAdminService.RecalculateBalance(periodId, legalBookId, mainAccountId, validateMovement, year)
    End Function

End Class
