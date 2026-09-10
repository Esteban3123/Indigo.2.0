'**********************************************************************
' Assembly         : Aplication.Accouting
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 17-05-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Data.Entity.Core
Imports System.Text
Imports System.Transactions
Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Domain.Entities.Service
Imports Domain.Payroll
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions

#End Region

Public Class AccountingDocumentAdminService
    Implements IAccountingDocumentAdminService, Inject

#Region "fields"
    Private _RepositoryJournalVouchers As IAccountingDocumentRepository
    Private _RepositoryJournalVouchersType As IDocumentTypeRepository
    Private _RepositoryJournalVouchersTypeCommit As IDocumentTypeRepository
    Private _AdminServiceAccountingBalance As IAccountingBalanceAdminService
    Private _RepositoryBalance As IAccountingBalanceRepository
    Private _PucRepository As IPUCRepository
    Private _CloseMonthRepository As ICloseMonthRepository
    Private _documentType As Domain.Entities.JournalVoucherTypes
    Private _thirdPartyRepository As IThirdPartyRepository
    Private _costCenterRepository As ICostCenterRepository
    Private _retentionConceptRepository As IRetentionConceptRepository
    Private _legalBookRepository As IBookRepository
#End Region

#Region "Builder"
    Public Sub New(ByVal Repository As IAccountingDocumentRepository, ByVal secuenseDRepository As ISequenseAccountingDRepository,
                   ByVal RepositoryDocumentType As IDocumentTypeRepository, ByVal adminServiceAccountingBalance As IAccountingBalanceAdminService, ByVal RepositoryBalance As IAccountingBalanceRepository,
                   ByVal PucRepository As IPUCRepository, ByVal closeMonthRepository As ICloseMonthRepository, ByVal RepositoryJournalVouchersType As IDocumentTypeRepository,
                   thirdPartyRepository As IThirdPartyRepository, costCenterRepository As ICostCenterRepository, retentionConceptRepository As IRetentionConceptRepository, LegalBookRepository As IBookRepository)
        If Repository Is Nothing Or secuenseDRepository Is Nothing Or adminServiceAccountingBalance Is Nothing Then
            Throw New ArgumentNullException("Repositorio vacio")
        End If
        _RepositoryJournalVouchersTypeCommit = RepositoryJournalVouchersType
        _PucRepository = PucRepository
        _CloseMonthRepository = closeMonthRepository
        _RepositoryJournalVouchersType = RepositoryDocumentType
        _RepositoryJournalVouchers = Repository
        _AdminServiceAccountingBalance = adminServiceAccountingBalance
        _RepositoryBalance = RepositoryBalance
        _documentType = Nothing
        _thirdPartyRepository = thirdPartyRepository
        _costCenterRepository = costCenterRepository
        _retentionConceptRepository = retentionConceptRepository
        _legalBookRepository = LegalBookRepository
    End Sub
#End Region

#Region "Functions"

    Public Function GetJournalVouchersByIdOnlyHead(id As Integer) As JournalVouchers Implements IAccountingDocumentAdminService.GetJournalVouchersByIdOnlyHead
        Return _RepositoryJournalVouchers.GetJournalVouchersByIdOnlyHead(id)
    End Function

    ''' <summary>
    ''' funcion para obtener el documento contable por consecutibo
    ''' </summary>
    ''' <param name="consecutive"></param>
    ''' <returns></returns>
    Public Async Function GetAccountingDocumenteByConsecutiveAsync(consecutive As String, ByVal Tracking As Boolean) As Task(Of Domain.Entities.JournalVouchers) Implements IAccountingDocumentAdminService.GetAccountingDocumenteByConsecutiveAsync
        Return Await _RepositoryJournalVouchers.GetAccountingDocumenteByConsecutiveAsync(consecutive, Tracking)
    End Function

    ''' <summary>
    ''' funcion para obtener el documento contable por el id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetAccountingDocumenteById(id As Integer) As Domain.Entities.JournalVouchers Implements IAccountingDocumentAdminService.GetAccountingDocumenteById
        Return _RepositoryJournalVouchers.GetAccountingDocumenteById(id)
    End Function

    Private Function convertJournalToXml(journalVoucher As JournalVouchers, Optional Status As Boolean = True) As String
        Dim builder As StringBuilder = New StringBuilder()
        builder.Append("<JournalVoucher>")
        builder.Append("<Id>" & journalVoucher.Id & "</Id>")
        builder.Append("<Consecutive>" & journalVoucher.Consecutive & "</Consecutive>")
        If journalVoucher.LegalBookId > 0 Then
            builder.Append("<LegalBookId>" & journalVoucher.LegalBookId & "</LegalBookId>")
        End If
        If journalVoucher.AccountingMovementId > 0 Then
            builder.Append("<AccountingMovementId>" & journalVoucher.AccountingMovementId & "</AccountingMovementId>")
        End If
        builder.Append("<IdJournalVoucher>" & journalVoucher.IdJournalVoucher & "</IdJournalVoucher>")
        'Se modifica la funcion de formato de fecha, dejandole el formato de fecha us_english, para evitar inconvenientes con las bases de datos en otro lenguaje
        builder.Append("<DateTRM>" & journalVoucher.DateTRM.ToString("yyyy/MM/dd hh:mm:ss") & "</DateTRM>")
        builder.Append("<VoucherDate>" & journalVoucher.VoucherDate.ToString("yyyy/MM/dd hh:mm:ss") & "</VoucherDate>")
        builder.Append("<Imported>" & journalVoucher.Imported & "</Imported>")
        builder.Append("<Status>" & journalVoucher.Status & "</Status>")
        If journalVoucher.Detail IsNot Nothing Then
            builder.Append("<Detail>" & journalVoucher.Detail.Replace("<", " ").Replace("&", " ") & "</Detail>")
        Else
            builder.Append("<Detail> </Detail>")
        End If
        If journalVoucher.EntityCode IsNot Nothing Then
            builder.Append("<EntityCode>" & journalVoucher.EntityCode & "</EntityCode>")
        End If
        If journalVoucher.EntityId IsNot Nothing Then
            builder.Append("<EntityId>" & journalVoucher.EntityId & "</EntityId>")
        End If
        If journalVoucher.EntityName IsNot Nothing Then
            builder.Append("<EntityName>" & journalVoucher.EntityName & "</EntityName>")
        End If
        If journalVoucher.OriginEntityName IsNot Nothing Then
            builder.Append("<OriginEntityName>" & journalVoucher.OriginEntityName & "</OriginEntityName>")
        End If
        builder.Append("<IsClosedYear>" & journalVoucher.IsClosedYear & "</IsClosedYear>")
        builder.Append("<CurrencyId>" & journalVoucher.BookCurrencyId & "</CurrencyId>")
        builder.Append($"<CreationUser>{ journalVoucher?.CreationUser}</CreationUser>")
        builder.Append($"<ModificationUser>{ journalVoucher?.ModificationUser}</ModificationUser>")
        builder.Append($"<ConfirmationUser>{ journalVoucher?.ConfirmationUser}</ConfirmationUser>")

        builder.Append($"<CreationDate>{ journalVoucher.CreationDate.ToString("yyyy/MM/dd hh:mm:ss")}</CreationDate>")

        If journalVoucher?.ModificationDate IsNot Nothing Then
            builder.Append($"<ModificationDate>{ journalVoucher.ModificationDate.Value.ToString("yyyy/MM/dd hh:mm:ss")}</ModificationDate>")
        End If

        If journalVoucher?.ConfirmationDate IsNot Nothing Then
            builder.Append($"<ConfirmationDate>{journalVoucher.ConfirmationDate.Value.ToString("yyyy/MM/dd hh:mm:ss")}</ConfirmationDate>")
        End If

        If Status Then
            For Each detail As JournalVoucherDetails In journalVoucher.JournalVoucherDetails
                builder.Append("<JournalVoucherDetail>")
                builder.Append("<Id>" & detail.Id & "</Id>")
                builder.Append("<IdMainAccount>" & detail.IdMainAccount & "</IdMainAccount>")
                If detail.IdThirdParty IsNot Nothing Then
                    builder.Append("<IdThirdParty>" & detail.IdThirdParty & "</IdThirdParty>")
                End If
                If detail.IdCostCenter IsNot Nothing Then
                    builder.Append("<IdCostCenter>" & detail.IdCostCenter & "</IdCostCenter>")
                End If
                builder.Append("<DebitValue>" & detail.DebitValue.ToString.Replace(",", ".").ToString & "</DebitValue>")
                builder.Append("<CreditValue>" & detail.CreditValue.ToString.Replace(",", ".").ToString & "</CreditValue>")
                If detail.Detail IsNot Nothing Then
                    builder.Append("<Detail>" & detail.Detail.Replace("<", " ").Replace("&", " ") & "</Detail>")
                End If
                If detail.IdRetention IsNot Nothing Then
                    builder.Append("<IdRetention>" & detail.IdRetention & "</IdRetention>")
                End If
                If detail.RetentionRate IsNot Nothing Then
                    builder.Append("<RetentionRate>" & detail.RetentionRate.ToString().Replace(",", ".") & "</RetentionRate>")
                End If
                If detail.BaseValue IsNot Nothing Then
                    builder.Append("<BaseValue>" & detail.BaseValue.ToString.Replace(",", ".") & "</BaseValue>")
                End If
                If detail.BillingValue IsNot Nothing Then
                    builder.Append("<BillingValue>" & detail.BillingValue.ToString.Replace(",", ".") & "</BillingValue>")
                End If
                If detail.ChangeTracker.State = ObjectState.Deleted Then
                    builder.Append("<IsDelete>1</IsDelete>")
                End If
                builder.Append("</JournalVoucherDetail>")
            Next
        End If
        builder.Append("</JournalVoucher>")
        Return builder.ToString()
    End Function

    ''' <summary>
    ''' Saves the accounting document.
    ''' </summary>
    ''' <returns></returns>
    <Obsolete("Esta función está obsoleta. Usa SaveAccountingDocumentAsync.", False)>
    Public Function SaveAccountingDocument(accountingDocument As JournalVouchers, audit As AuditMessage, Optional ByVal withCommit As Boolean = True) As ActionMessageResult(Of JournalVouchers) Implements IAccountingDocumentAdminService.SaveAccountingDocument
        Return SaveAccountingDocumentAsync(accountingDocument, audit, withCommit).GetAwaiter().GetResult()
    End Function

    Public Async Function SaveAccountingDocumentAsync(accountingDocument As JournalVouchers, audit As AuditMessage, Optional ByVal withCommit As Boolean = True) As Task(Of ActionMessageResult(Of JournalVouchers)) Implements IAccountingDocumentAdminService.SaveAccountingDocumentAsync
        ValidateParameters(accountingDocument, audit)
        Dim UnitOfWork As IUnitWork = _RepositoryJournalVouchers.UnitWork
        Dim result As New ActionMessageResult(Of JournalVouchers)
        Using transaction As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted}, TransactionScopeAsyncFlowOption.Enabled)
            Try
                Dim auxAccountingDoc As JournalVouchers = Await GetAuxAccountingDocIfModifiedAsync(accountingDocument)
                EnsureBookCurrency(accountingDocument)
                Dim status As Integer = GetAuditStatus(accountingDocument.Status)
                Dim resultStore As Object = Await ExecuteJournalVoucherProcessAsync(accountingDocument, audit)
                If resultStore.CodeMessage <> 0 Then
                    Return New ActionMessageResult(Of JournalVouchers) With {.StateResult = False, .Message = resultStore.Message}
                End If
                Dim refreshedVoucher = RefreshJournalVoucher(resultStore.IdJournalVoucher)
                AuditAndCompleteTransaction(accountingDocument, audit, status, auxAccountingDoc, transaction)
                Return New ActionMessageResult(Of JournalVouchers) With {.StateResult = True, .ObjectEmbbeded = refreshedVoucher, .Message = resultStore.Message}
            Catch ex As Exception
                HandleSaveException(ex, UnitOfWork, result, accountingDocument)
            End Try
            Return result
        End Using
    End Function

    Private Sub ValidateParameters(accountingDocument As JournalVouchers, audit As AuditMessage)
        If accountingDocument Is Nothing Then Throw New ArgumentNullException(NameOf(accountingDocument))
        If audit Is Nothing Then Throw New ArgumentNullException(NameOf(audit))
    End Sub

    Private Async Function GetAuxAccountingDocIfModifiedAsync(accountingDocument As JournalVouchers) As Task(Of JournalVouchers)
        If accountingDocument.ChangeTracker.State = ObjectState.Modified Then
            Return Await GetAccountingDocumenteByConsecutiveAsync(accountingDocument.Consecutive, False)
        End If
        Return Nothing
    End Function

    Private Sub EnsureBookCurrency(accountingDocument As JournalVouchers)
        If accountingDocument.BookCurrencyId Is Nothing Then
            accountingDocument.BookCurrencyId = _legalBookRepository.FirstOrDefault(Function(x) x.Id = accountingDocument.LegalBookId)?.OfficialCurrencyId
        End If
    End Sub

    Private Function GetAuditStatus(statusId As Integer) As Integer
        Select Case statusId
            Case 1 : Return Infrastructure.CrossCutting.Audit.Actions.Insert
            Case 2 : Return Infrastructure.CrossCutting.Audit.Actions.Confirm
            Case 3 : Return Infrastructure.CrossCutting.Audit.Actions.Annular
            Case 4 : Return Infrastructure.CrossCutting.Audit.Actions.Disconfirm
            Case Else : Throw New ArgumentOutOfRangeException("Estado no soportado")
        End Select
    End Function

    Private Async Function ExecuteJournalVoucherProcessAsync(accountingDocument As JournalVouchers, audit As AuditMessage) As Task(Of Object)
        Dim journalXml As String
        Select Case accountingDocument.Status
            Case 1, 2
                journalXml = convertJournalToXml(accountingDocument, True)
                Return Await _RepositoryJournalVouchers.CreateAndValidateJournalVoucherAsync(journalXml, audit.CodeUser)
            Case 3, 4
                journalXml = convertJournalToXml(accountingDocument, False)
                Return Await _RepositoryJournalVouchers.UnconfirmOrAnnulJournalVoucherAsync(journalXml, audit.CodeUser)
            Case Else
                Throw New ArgumentOutOfRangeException("Estado no soportado para el proceso contable")
        End Select
    End Function

    Private Function RefreshJournalVoucher(idJournalVoucher As Integer) As JournalVouchers
        Dim refreshedVoucher = _RepositoryJournalVouchers.GetJournalVouchersByIdOnlyHead(idJournalVoucher)
        If refreshedVoucher Is Nothing Then
            refreshedVoucher = New JournalVouchers()
        End If
        Return refreshedVoucher
    End Function

    Private Sub AuditAndCompleteTransaction(accountingDocument As JournalVouchers, audit As AuditMessage, status As Integer, auxAccountingDoc As JournalVouchers, transaction As TransactionScope)
        Dim auditProcess = New IndigoAuditSimpleEntity(Of JournalVouchers)(accountingDocument, audit, status, auxAccountingDoc)
        auditProcess.Execute()
        transaction.Complete()
    End Sub

    Private Sub HandleSaveException(ex As Exception, unitOfWork As IUnitWork, result As ActionMessageResult(Of JournalVouchers), accountingDocument As JournalVouchers)
        unitOfWork.RollbackChanges()
        If TypeOf ex Is ConstraintException OrElse TypeOf ex Is OptimisticConcurrencyException Then
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            result.StateResult = False
            result.MessageResult.Add(New MessageResult("-9999", accountingDocument.Consecutive.ToString()))
            result.Message = IndigoManagementExceptions.GetExceptionDetails(ex)
        Else
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            result.StateResult = False
            result.MessageResult.Add(New MessageResult("-9999", accountingDocument.Consecutive.ToString()))
            result.Message = Utils.GetInnerExceptionMessageToString(ex)
        End If
    End Sub

    ''' <summary>
    ''' metodo para generar las homologaciones del comprobante contable
    ''' </summary>
    ''' <param name="accountingDocument"></param>
    ''' <param name="legalBookId"></param>
    ''' <returns></returns>
    Public Function GenerateHomologationsJournalVoucher(accountingDocument As Domain.Entities.JournalVouchers, legalBookId As Integer) As ActionResult(Of List(Of JournalVoucherDetails)) Implements IAccountingDocumentAdminService.GenerateHomologationsJournalVoucher
        Try
            Dim journalXml = convertJournalToXml(accountingDocument)
            Dim result = _RepositoryJournalVouchers.GenerateHomologationsJournalVoucher(journalXml, legalBookId)
            If result.Count = 0 Then
                Return New ActionResult(Of List(Of JournalVoucherDetails)) With {.StateResult = False, .Message = "No se logro homologar el documento contable"}
            Else
                If result.Any(Function(d) d.CodeMessage <> "0") Then
                    Return New ActionResult(Of List(Of JournalVoucherDetails)) With {.StateResult = False, .Message = result.Where(Function(d) d.CodeMessage <> "0").FirstOrDefault.Message}
                Else

                    Dim listJournalVoucherDetail = New List(Of JournalVoucherDetails)
                    For Each detail In result
                        Dim journalVourcherDetail As New JournalVoucherDetails
                        With journalVourcherDetail
                            .IdMainAccount = detail.MainAccountId
                            .CodeNameMainAccount = detail.NameAccount
                            .IdThirdParty = detail.IdThirdParty
                            .CodeNameThirdParty = detail.NameThirdParty
                            .IdCostCenter = detail.IdCostCenter
                            .CodeNameCostCenter = detail.NameCostCenter
                            .DebitValue = detail.DebitValue
                            .CreditValue = detail.CreditValue
                            .Detail = detail.Detail
                            .IdRetention = detail.IdRetention
                            .RetentionRate = detail.RetentionRate
                            .BaseValue = detail.BaseValue
                            .BillingValue = detail.BillingValue
                        End With
                        listJournalVoucherDetail.Add(journalVourcherDetail)
                    Next
                    Return New ActionResult(Of List(Of JournalVoucherDetails)) With {.StateResult = True, .ObjectEmbbeded = listJournalVoucherDetail}
                End If
            End If
        Catch ex As Exception
            Return New ActionResult(Of List(Of JournalVoucherDetails)) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function


    ''' <summary>
    ''' Gets the accounting document by consecutive identifier document.
    ''' </summary>
    ''' <param name="consecutive">The consecutive.</param>
    ''' <param name="idDocument">The identifier document.</param>
    ''' <returns></returns>
    Public Function GetAccountingDocumentByConsecutiveIdDocument(consecutive As Integer, idDocument As Integer) As Domain.Entities.JournalVouchers Implements IAccountingDocumentAdminService.GetAccountingDocumentByConsecutiveIdDocument
        Return _RepositoryJournalVouchers.GetAccountingDocumentByConsecutiveIdDocument(consecutive, idDocument)
    End Function

    ''' <summary>
    ''' Gets the accounting document by consecutive identifier document, the journal voucher type and legal book.
    ''' </summary>
    ''' <param name="legalbookId"></param>
    ''' <param name="journalVoucherTypeId"></param>
    ''' <param name="consecutive"></param>
    ''' <returns></returns>
    Public Function GetAccountingDocumentByConsecutiveAndJournalVoucherTypeIdAndLegalBookId(legalbookId As Integer, journalVoucherTypeId As Integer, consecutive As Integer) As Domain.Entities.JournalVouchers Implements IAccountingDocumentAdminService.GetAccountingDocumentByConsecutiveAndJournalVoucherTypeIdAndLegalBookId
        Return _RepositoryJournalVouchers.GetAccountingDocumentByConsecutiveAndJournalVoucherTypeIdAndLegalBookId(legalbookId, journalVoucherTypeId, consecutive)
    End Function

    ''' <summary>
    ''' funcion para obtener el documento contable por id
    ''' </summary>
    ''' <param name="idJournalVouchers">el id del documento contable.</param>
    ''' <returns></returns>
    Public Async Function GetJournalVouchersByIdAsync(idJournalVouchers As Long) As Task(Of JournalVouchers) Implements IAccountingDocumentAdminService.GetJournalVouchersByIdAsync
        Return Await _RepositoryJournalVouchers.GetJournalVouchersByIdAsync(idJournalVouchers)
    End Function

    ''' <summary>
    ''' funcion para obtener los comprobantes contables por estado
    ''' </summary>
    ''' <param name="Status"></param>
    ''' <returns></returns>
    Public Function GetJournalVourchersByStatus(Status As Integer, ByVal month As Integer, year As Integer) As List(Of SP_GetJournalVouchersByStatus_Result) Implements IAccountingDocumentAdminService.GetJournalVourchersByStatus
        Return _RepositoryJournalVouchers.GetJournalVourchersByStatus(Status, month, year)
    End Function

    ''' <summary>
    ''' Saves the list journal vouchers.
    ''' </summary>
    ''' <param name="Status"></param>
    ''' <param name="ListIdJournalVouchers">The list identifier journal vouchers.</param>
    ''' <param name="month"></param>
    ''' <returns></returns>
    Public Function SaveListJournalVouchers(Status As Integer, ListIdJournalVouchers As String, month As Integer) As List(Of SP_ChangeStatusJournalVouchers_Result) Implements IAccountingDocumentAdminService.SaveListJournalVouchers
        Return _RepositoryJournalVouchers.SaveListJournalVouchers(Status, ListIdJournalVouchers, month)
    End Function

#End Region

    ''' <summary>
    ''' metodo para copiar y pegar detalles del comprobante contable
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    Public Function SetJournalVoucherDetailsCopyPaste(LegalBookId As Integer, data As List(Of List(Of String))) As ActionResult(Of List(Of JournalVoucherDetails)) Implements IAccountingDocumentAdminService.SetJournalVoucherDetailsCopyPaste
        Dim accountingService = New AccountingServices(_PucRepository, _thirdPartyRepository, _RepositoryJournalVouchers)
        Return accountingService.SetCopyPasteOrImportFilePortfolioNote(LegalBookId, Nothing, data)
    End Function

    ''' <summary>
    ''' metodo para validar la carga del archivo para los saldo iniciales
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    Public Function ValidateFileJournalVoucherDetails(LegalBookId As Integer, data As List(Of ImportFileRow)) As ActionResult(Of List(Of JournalVoucherDetails)) Implements IAccountingDocumentAdminService.ValidateFileJournalVoucherDetails
        Dim accountingService = New AccountingServices(_PucRepository, _thirdPartyRepository, _RepositoryJournalVouchers)
        Return accountingService.SetCopyPasteOrImportFilePortfolioNote(LegalBookId, data, Nothing)
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
    Public Function CalculateRetention(BaseValue As Decimal, MinBase As Decimal, Percentaje As Decimal, ThirdPartyId As Integer, MainAccountId As Integer) As ActionResult(Of Object) Implements IAccountingDocumentAdminService.CalculateRetention
        Dim accountingService = New AccountingServices(_PucRepository, _thirdPartyRepository, _RepositoryJournalVouchers)
        Return accountingService.CalculateRetention(BaseValue, MinBase, Percentaje, ThirdPartyId, MainAccountId)
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _AdminServiceAccountingBalance.Dispose()
            End If
            _RepositoryJournalVouchersTypeCommit = Nothing
            _PucRepository = Nothing
            _CloseMonthRepository = Nothing
            _RepositoryJournalVouchersType = Nothing
            _RepositoryJournalVouchers = Nothing
            _AdminServiceAccountingBalance = Nothing
            _RepositoryBalance = Nothing
            _documentType = Nothing
            _thirdPartyRepository = Nothing
            _costCenterRepository = Nothing
            _retentionConceptRepository = Nothing
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
