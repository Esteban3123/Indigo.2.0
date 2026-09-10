'***********************************************************************
' Assembly         : Application.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 31-03-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Data.Entity.Core
Imports System.Data.Entity.Infrastructure
Imports System.Data.SqlClient
Imports System.Text
Imports System.Transactions
Imports Application.Accounting
Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Domain.Entities.Service
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources

Public Class NotesDebitCreditAdminService
    Implements INotesDebitCreditAdminService

#Region "Properties"

    ''' <summary>
    ''' Variable tipo repositorio para notas debito/credito
    ''' </summary>
    ''' <remarks></remarks>
    Private _paymentsNoteRepository As INotesDebitCreditRepository

    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As ISequensePaymentsDRepository

    ''' <summary>
    ''' Repositorio de aplicacion de paymentNotesAccountPayable
    ''' </summary>
    ''' <remarks></remarks>
    Private _paymentNotesAccountPayableAdvance As IPaymentNotesAccountPayableAdvanceAdminService

    ''' <summary>
    ''' Variable tipo repositorio para parametros de pago
    ''' </summary>
    ''' <remarks></remarks>
    Private _settingsPaymentsRepository As ISettingPaymentsRepository

    ''' <summary>
    ''' Variable tipo repositorio para cxp
    ''' </summary>
    ''' <remarks></remarks>
    Private _accountPayableRepository As IAccountPayableRepository

    ''' <summary>
    ''' Variable tipo repositorio para anticipos
    ''' </summary>
    ''' <remarks></remarks>
    Private _advancePaymentsRepository As IMoneyAdvanceRepository

    ''' <summary>
    ''' Repositorio de documento contable
    ''' </summary>
    ''' <remarks></remarks>
    Private _accountingRepository As IAccountingDocumentAdminService

    ''' <summary>
    ''' Repositorio de cuentas por pagar
    ''' </summary>
    ''' <remarks></remarks>
    Private _accountPayableAdminService As IAccountPayableAdminService

    ''' <summary>
    ''' Repositorio de anticipos
    ''' </summary>
    ''' <remarks></remarks>
    Private _advancePaymentsAdminServie As IMoneyAdvanceAdminService

    ''' <summary>
    ''' Servicios de aplicacion de control en pagos
    ''' </summary>
    ''' <remarks></remarks>
    Private _paymentControlAdminService As IPaymentControlAdminService

    Private _repositoryCloseMont As ICloseMonthRepository
    Private _repositoryMainAccounts As IPUCRepository

    ''' <summary>
    ''' Repositorio de documento soporte
    ''' </summary>
    Private _electronicSupportDocumentRepository As IElectronicSupportDocumentRepository

    ''' <summary>
    ''' Repositorio para los parametros de empresa
    ''' </summary>
    ''' <remarks></remarks>
    Private _companySettingsRepository As ICompanySettingsRepository

    ''' <summary>
    ''' Repositorio de PaymentsNoteConcept
    ''' </summary>
    Private _paymentsNoteConceptRepository As IPaymentsNoteConceptRepository

#End Region

#Region "Builder"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal paymentsNoteRepository As INotesDebitCreditRepository, ByVal secuenseDRepository As ISequensePaymentsDRepository,
                   ByVal paymentNotesAccountPayableAdvance As IPaymentNotesAccountPayableAdvanceAdminService,
                   ByVal settingsPaymentsRepository As ISettingPaymentsRepository,
                   ByVal accountingRepository As IAccountingDocumentAdminService, ByVal accountPayableAdminService As IAccountPayableAdminService,
                   ByVal advancePaymentsAdminServie As IMoneyAdvanceAdminService, ByVal accountPayableRepository As IAccountPayableRepository, ByVal advancePaymentsRepository As IMoneyAdvanceRepository,
                   ByVal paymentControlAdminService As IPaymentControlAdminService, ByVal repositoryCloseMont As ICloseMonthRepository, ByVal repositoryMainAccounts As IPUCRepository,
                   ByVal electronicSupportDocumentRepository As IElectronicSupportDocumentRepository,
                   ByVal companySettingsRepository As ICompanySettingsRepository, ByVal paymentsNoteConceptRepository As IPaymentsNoteConceptRepository)
        If paymentsNoteRepository Is Nothing Then
            Throw New ArgumentNullException("paymentsNoteRepository Vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        If paymentNotesAccountPayableAdvance Is Nothing Then
            Throw New ArgumentNullException("paymentNotesAccountPayableAdvance")
        End If
        If settingsPaymentsRepository Is Nothing Then
            Throw New ArgumentNullException("settingsPaymentsRepository Vacio")
        End If
        If accountingRepository Is Nothing Then
            Throw New ArgumentNullException("accountingRepository Vacio")
        End If
        If accountPayableAdminService Is Nothing Then
            Throw New ArgumentNullException("accountPayableAdminService Vacio")
        End If
        If advancePaymentsAdminServie Is Nothing Then
            Throw New ArgumentNullException("advancePaymentsAdminServie Vacio")
        End If
        If accountPayableRepository Is Nothing Then
            Throw New ArgumentNullException("accountPayableRepository Vacio")
        End If
        If advancePaymentsRepository Is Nothing Then
            Throw New ArgumentNullException("advancePaymentsRepository Vacio")
        End If
        If paymentControlAdminService Is Nothing Then
            Throw New ArgumentNullException("paymentControlAdminService Vacio")
        End If
        If companySettingsRepository Is Nothing Then
            Throw New ArgumentNullException("companySettingsRepository Vacio")
        End If
        If paymentsNoteConceptRepository Is Nothing Then
            Throw New ArgumentNullException("paymentsNoteConceptRepository Vacio")
        End If
        _paymentsNoteRepository = paymentsNoteRepository
        _secuenseDRepository = secuenseDRepository
        _paymentNotesAccountPayableAdvance = paymentNotesAccountPayableAdvance
        _settingsPaymentsRepository = settingsPaymentsRepository
        _accountingRepository = accountingRepository
        _accountPayableAdminService = accountPayableAdminService
        _advancePaymentsAdminServie = advancePaymentsAdminServie
        _accountPayableRepository = accountPayableRepository
        _advancePaymentsRepository = advancePaymentsRepository
        _paymentControlAdminService = paymentControlAdminService
        _repositoryCloseMont = repositoryCloseMont
        _repositoryMainAccounts = repositoryMainAccounts
        _electronicSupportDocumentRepository = electronicSupportDocumentRepository
        _companySettingsRepository = companySettingsRepository
        _paymentsNoteConceptRepository = paymentsNoteConceptRepository
    End Sub

#End Region

#Region "Methods"

    Public Function ImportBillsToPortfolioNote(data As List(Of List(Of String)), ParamArray parameters() As Object) As ActionResult(Of List(Of AccountPayable)) Implements INotesDebitCreditAdminService.ImportBillsToPortfolioNote
        If data Is Nothing OrElse data.Count = 0 Then
            Throw New ArgumentNullException("Data")
        End If

        'Listado de errores
        Dim listErrors As New List(Of String)

        'Listado que se devuelve para pegar a la rejilla del form
        Dim ListAccountPayables As New List(Of AccountPayable)

        Try
            Dim xmlObject = ConvertBillsToXml(data)
            Dim xmlParameters = ConvertParametersToXml(parameters)

            'Se consume el procedimiento almacenado
            Dim resultStore = _paymentsNoteRepository.SP_ImportBillsToPortfolioNote(xmlObject, xmlParameters)

            'Se crean los objetos para devolver y pegar en la rejilla
            If resultStore IsNot Nothing AndAlso resultStore.Count > 0 Then
                For Each itemXml In resultStore
                    If itemXml.StatusField = 1 Then 'Si el estado del item es True y pasó todas las validaciones
                        'Se crea el nuevo objeto para agregarlo al listado
                        Dim accountPayable As AccountPayable = ListAccountPayables.Where(Function(x) x.Id = itemXml.AccountPayableId).FirstOrDefault()
                        If accountPayable Is Nothing Then
                            accountPayable = New AccountPayable() With
                            {
                                .Id = itemXml.AccountPayableId,
                                .BillNumber = itemXml.BillNumber,
                                .BillDate = itemXml.BillDate,
                                .ExpirationDate = itemXml.ExpirationDate,
                                .Value = itemXml.Value,
                                .Balance = itemXml.Balance,
                                .Adjustment = itemXml.AdjusmentValue,
                                .Percentage = itemXml.Percentage,
                                .HandlesAddModifyDelete = itemXml.HandlesAddModifyDelete,
                                .IdSupplier = itemXml.IdSupplier,
                                .IdAccount = itemXml.IdAccount,
                                .IdCostCenter = itemXml.IdCostCenter,
                                .IdThirdParty = itemXml.IdThirdParty
                            }

                            ListAccountPayables.Add(accountPayable)
                        End If

                        Dim accountPayableShare As New AccountPayableShares() With
                        {
                            .Id = itemXml.AccountPayableShareId,
                            .Balance = itemXml.BalanceNoteShare,
                            .ValueNoteShare = itemXml.ValueNoteShare
                        }

                        accountPayable.AccountPayableShares.Add(accountPayableShare)
                    Else 'Si el estado del item es False y no pasó alguna validación
                        listErrors.Add(itemXml.MessageField)
                    End If
                Next
            End If

            Return New ActionResult(Of List(Of AccountPayable)) With {.StateResult = True, .ObjectEmbbeded = ListAccountPayables, .MessageResult = listErrors}
        Catch ex As SqlException
            If ex.ErrorCode = -2146232060 Then
                Return New ActionResult(Of List(Of AccountPayable)) With {.StateResult = False, .MessageResult = {"Los valores contienen decimales con un formato no valido, por favor corrija para poder continuar"}.ToList()}
            Else
                Return New ActionResult(Of List(Of AccountPayable)) With {.StateResult = False, .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList()}
            End If
        Catch ex As Exception
            Return New ActionResult(Of List(Of AccountPayable)) With {.StateResult = False, .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList()}
        End Try
    End Function

    Public Function ImportAdvancesToPortfolioNote(data As List(Of List(Of String)), ParamArray parameters() As Object) As ActionResult(Of List(Of AdvancePayments)) Implements INotesDebitCreditAdminService.ImportAdvancesToPortfolioNote
        If data Is Nothing OrElse data.Count = 0 Then
            Throw New ArgumentNullException("Data")
        End If

        'Listado de errores
        Dim listErrors As New List(Of String)

        'Listado que se devuelve para pegar a la rejilla del form
        Dim ListAdvancePayments As New List(Of AdvancePayments)

        Try
            Dim xmlObject = ConvertAdvancesToXml(data)
            Dim xmlParameters = ConvertParametersToXml(parameters)

            'Se consume el procedimiento almacenado
            Dim resultStore = _paymentsNoteRepository.SP_ImportAdvancesToPortfolioNote(xmlObject, xmlParameters)

            'Se crean los objetos para devolver y pegar en la rejilla
            If resultStore IsNot Nothing AndAlso resultStore.Count > 0 Then
                For Each itemXml In resultStore
                    If itemXml.StatusField = 1 Then 'Si el estado del item es True y pasó todas las validaciones
                        'Se crea el nuevo objeto para agregarlo al listado
                        Dim advancePayment As AdvancePayments = ListAdvancePayments.Where(Function(x) x.Id = itemXml.AdvancePaymentId).FirstOrDefault()
                        If advancePayment Is Nothing Then
                            advancePayment = New AdvancePayments() With
                            {
                                .Id = itemXml.AdvancePaymentId,
                                .Code = itemXml.Code,
                                .DocumentDate = itemXml.DocumentDate,
                                .Value = itemXml.Value,
                                .Balance = itemXml.Balance,
                                .Adjustment = itemXml.AdjusmentValue,
                                .Percentage = itemXml.Percentage,
                                .HandlesAddModifyDelete = itemXml.HandlesAddModifyDelete,
                                .IdSupplier = itemXml.IdSupplier,
                                .IdAccount = itemXml.IdAccount,
                                .IdCostCenter = itemXml.IdCostCenter,
                                .IdThirdParty = itemXml.IdThirdParty
                            }

                            ListAdvancePayments.Add(advancePayment)
                        End If
                    Else 'Si el estado del item es False y no pasó alguna validación
                        listErrors.Add(itemXml.MessageField)
                    End If
                Next
            End If

            Return New ActionResult(Of List(Of AdvancePayments)) With {.StateResult = True, .ObjectEmbbeded = ListAdvancePayments, .MessageResult = listErrors}
        Catch ex As SqlException
            If ex.ErrorCode = -2146232060 Then
                Return New ActionResult(Of List(Of AdvancePayments)) With {.StateResult = False, .MessageResult = {"Los valores contienen decimales con un formato no valido, por favor corrija para poder continuar"}.ToList()}
            Else
                Return New ActionResult(Of List(Of AdvancePayments)) With {.StateResult = False, .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList()}
            End If
        Catch ex As Exception
            Return New ActionResult(Of List(Of AdvancePayments)) With {.StateResult = False, .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList()}
        End Try
    End Function

    ''' <summary>
    ''' Elimina una nota debito/credito
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeletePaymentsNote(paymentsNote As PaymentNotes, audit As AuditMessage) As ActionResult Implements INotesDebitCreditAdminService.DeletePaymentsNote
        If paymentsNote Is Nothing Then
            Throw New ArgumentNullException("paymentsNote")
        End If
        Dim unitOfWork As IUnitWork = Me._paymentsNoteRepository.UnitWork
        Try
            paymentsNote.StartTracking()
            While paymentsNote.PaymentsNoteDetails.Count > 0
                paymentsNote.PaymentsNoteDetails.Item(0).MarkAsDeleted()
            End While
            While paymentsNote.PaymentNotesAccountPayableAdvance.Count > 0
                paymentsNote.PaymentNotesAccountPayableAdvance.Item(0).MarkAsDeleted()
            End While
            paymentsNote.MarkAsDeleted()
            Me._paymentsNoteRepository.SaveEntity(paymentsNote)
            unitOfWork.Commit()
            'Auditoria básica
            IndigoAuditBasic.Execute(GetType(PaymentNotes).Name, audit.Functional, paymentsNote.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, Infrastructure.CrossCutting.Base.ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            'Ausitoria avanzada
            Dim auditObject As New IndigoAuditSimpleEntity(Of PaymentNotes)(paymentsNote, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()
            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una determinada nota debito/credito
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPaymentsNote(code As String, audit As AuditMessage) As ActionResult(Of PaymentNotes) Implements INotesDebitCreditAdminService.GetPaymentsNote
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim paymentsNote As PaymentNotes = Me._paymentsNoteRepository.GetPaymentsNote(code.Trim())
            If paymentsNote IsNot Nothing AndAlso paymentsNote.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of PaymentNotes)(paymentsNote, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of PaymentNotes) With {.StateResult = True, .ObjectEmbbeded = paymentsNote}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of PaymentNotes) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza una nota debito/credito
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SavePaymentsNote(paymentsNote As PaymentNotes, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of PaymentNotes) Implements INotesDebitCreditAdminService.SavePaymentsNote
        If paymentsNote Is Nothing Then
            Throw New ArgumentNullException("paymentsNote")
        End If

        If paymentsNote.IndicatesBillAdvance <> 2 Then
            Dim validations = paymentsNote.ValidateIfBalanced()
            If Not validations.Item1 Then
                Return New ActionResult(Of PaymentNotes) With {.StateResult = False, .MessageResult = {validations.Item2}.ToList()}
            End If
        End If

        Dim unitOfWork As IUnitWork = Me._paymentsNoteRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using Transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim seq As PaymentsSecuenceDetail = Nothing
                If paymentsNote.Code Is Nothing OrElse paymentsNote.Code.Trim().Equals(String.Empty) Then
                    seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PaymentsSecuence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            paymentsNote.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            Return New ActionResult(Of PaymentNotes) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = "No se encontró la secuencia numérica (Cuentas por Pagar  - Notas Debito / Credito)"}
                        End If
                    Else
                        Return New ActionResult(Of PaymentNotes) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList(), .Message = "No se encontró la secuencia numérica (Cuentas por Pagar  - Notas Debito / Credito)"}
                    End If
                End If

                Dim auxPaymentsNote As PaymentNotes = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of PaymentNotes)
                Dim status As Integer

                If paymentsNote.ChangeTracker.State = ObjectState.Added Then
                    paymentsNote.CreationUser = audit.CodeUser
                    paymentsNote.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                ElseIf paymentsNote.ChangeTracker.State = ObjectState.Modified Then
                    auxPaymentsNote = paymentsNote.OriginalValue
                    If paymentsNote.Status = 1 Then
                        paymentsNote.ModificationUser = audit.CodeUser
                        paymentsNote.ModificationDate = DateTime.Now
                        status = Infrastructure.CrossCutting.Audit.Actions.Update
                    End If
                    If paymentsNote.Status = 2 Then
                        paymentsNote.ModificationUser = audit.CodeUser
                        paymentsNote.ModificationDate = DateTime.Now
                        paymentsNote.ConfirmationUser = audit.CodeUser
                        paymentsNote.ConfirmationDate = DateTime.Now
                        status = Infrastructure.CrossCutting.Audit.Actions.Confirm
                    End If
                    If paymentsNote.Status = 3 Then
                        paymentsNote.ModificationUser = audit.CodeUser
                        paymentsNote.ModificationDate = DateTime.Now
                        paymentsNote.AnnulmentUser = audit.CodeUser
                        paymentsNote.AnnulmentDate = DateTime.Now
                        status = Infrastructure.CrossCutting.Audit.Actions.Annular
                    End If
                End If

                If paymentsNote.Status = 1 Then
                    If paymentsNote.ChangeTracker.State = ObjectState.Added Then
                        Dim paymentControl As New PaymentsControl
                        paymentControl.DocumentNumber = paymentsNote.Code
                        paymentControl.DocumentType = 2
                        paymentControl.DocumentUser = audit.CodeUser
                        paymentControl.DocumentDate = paymentsNote.NoteDate
                        Dim resultSaveControl = _paymentControlAdminService.SavePaymentControl(paymentControl, audit)
                        If resultSaveControl.StateResult = False Then
                            Transaction.Dispose()
                            Return New ActionResult(Of PaymentNotes) With {.StateResult = False, .Message = ResourceManager.GetString("SavePaymentControlError", "Payments")}
                        End If
                    End If
                ElseIf (paymentsNote.Status = 2 AndAlso paymentsNote.Id > 0) OrElse paymentsNote.Status = 3 Then
                    Dim paymentControl = _paymentControlAdminService.GetPaymentControlByDocumentNumber(paymentsNote.Code, 2)
                    paymentControl.MarkAsDeleted()
                    _paymentControlAdminService.DeletePaymentControl(paymentControl, audit)
                End If

                ''si la entidad paymentsnote no tiene moneda se le asigna la moneda oficial parametrizada
                If paymentsNote.CurrencyId = Nothing Then
                    Dim _companySettings = _companySettingsRepository.GetCompanySettings(True)
                    paymentsNote.CurrencyId = _companySettings?.OfficialCurrencyId
                End If

                Me._paymentsNoteRepository.SaveEntity(paymentsNote)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()

                auditProcess = New IndigoAuditSimpleEntity(Of PaymentNotes)(paymentsNote, audit, status, auxPaymentsNote)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                paymentsNote.MarkAsUnchanged()
                Transaction.Complete()
                Return New ActionResult(Of PaymentNotes) With {.StateResult = True, .ObjectEmbbeded = paymentsNote}
            Catch ex As DbUpdateException
                unitOfWork.RollbackChanges()
                Transaction.Dispose()
                Return New ActionResult(Of PaymentNotes) With {.StateResult = False, .MessageResult = {ex.InnerException.InnerException.Message}.ToList(), .Message = Utils.GetInnerExceptionMessageToString(ex)}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                Transaction.Dispose()
                Return New ActionResult(Of PaymentNotes) With {.StateResult = False, .MessageResult = {"-999"}.ToList(), .Message = Utils.GetInnerExceptionMessageToString(ex)}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of PaymentNotes) With {.StateResult = False, .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeState(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of PaymentNotes) Implements INotesDebitCreditAdminService.ChangeState
        Dim paymentsNote As PaymentNotes = _paymentsNoteRepository.GetPaymentsNote(code)
        paymentsNote.Status = state
        Return SavePaymentsNote(paymentsNote, audit)
    End Function

    ''' <summary>
    ''' Guarda la nota de manera completa
    ''' </summary>
    ''' <param name="paymentNotes"></param>
    ''' <param name="listAccountPayable"></param>
    ''' <param name="listAdvancePayments"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SavePaymentNotesComplete(paymentNotes As PaymentNotes, listAccountPayable As List(Of AccountPayable), listAdvancePayments As List(Of AdvancePayments), modeConfirm As Boolean, audit As AuditMessage, Optional idSequense As Long = 0 _
                                             , Optional FlagDispersion As Boolean = False) As ActionResult(Of PaymentNotes) Implements INotesDebitCreditAdminService.SavePaymentNotesComplete
        If paymentNotes Is Nothing Then
            Throw New ArgumentNullException("paymentNotes")
        End If
        Dim UnitOfWork As IUnitWork = _paymentsNoteRepository.UnitWork
        Dim listCodes As New List(Of String)
        Dim resultPaymenNotes As ActionResult(Of PaymentNotes)

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using Transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                'Pregunto si el periodo esta abierto
                Dim accountingServices As New AccountingServices(_repositoryMainAccounts, _repositoryCloseMont)
                Dim validatePeriod = accountingServices.ValidatePeriodAndReturnOpendMonths(paymentNotes.NoteDate)
                If validatePeriod.StateResult = False Then
                    Transaction.Dispose()
                    Return New ActionResult(Of PaymentNotes) With {.StateResult = False, .MessageResult = {validatePeriod.Message}.ToList()}
                End If

                Dim resultPaymentNotesAccountPayable As ActionResult(Of List(Of PaymentNotesAccountPayableAdvance)) = Nothing
                Dim paymentService As New PaymentServices(_settingsPaymentsRepository, _accountPayableRepository, _advancePaymentsRepository, _electronicSupportDocumentRepository)
                Dim listPaymentNotesAccountPayableAdvance As List(Of PaymentNotesAccountPayableAdvance) = Nothing

                If paymentNotes.IndicatesBillAdvance = 0 Then 'Si es facturas
                    If listAccountPayable Is Nothing OrElse listAccountPayable.Count = 0 Then
                        Transaction.Dispose()
                        Return New ActionResult(Of PaymentNotes) With {.StateResult = False, .MessageResult = {"La nota no contiene ninguna factura en sus detalles"}.ToList()}
                    End If

                    'Se valida que las cuentas por pagar que se agregaron a la rejilla no existan en una programacion de pagos confirmada, si existe alguna no se deja guardar
                    Dim ListCompare = _paymentsNoteRepository.ValidateSchedulePaymentDetailContainsAccountPayable(listAccountPayable)
                    If (ListCompare IsNot Nothing AndAlso ListCompare.Count > 0) AndAlso Not FlagDispersion Then
                        Transaction.Dispose()

                        'Se arma el mensaje a devolver con las facturas que ya tienen una programación de pagos
                        Dim errors As New StringBuilder
                        For Each item In ListCompare
                            errors.AppendLine("La cuenta por pagar " + item.Code + " con No. de factura " + item.BillNumber + " esta incluida en una programación de pagos.")
                        Next
                        Return New ActionResult(Of PaymentNotes) With {.StateResult = False, .MessageResult = {errors.ToString}.ToList()}
                    End If

                    For Each itemAccountPayable As AccountPayable In listAccountPayable
                        Select Case itemAccountPayable.HandlesAddModifyDelete
                            Case 1
                                resultPaymentNotesAccountPayable = paymentService.CreatePaymentNotesAccountPayableAdvance(itemAccountPayable, Nothing, listPaymentNotesAccountPayableAdvance, paymentNotes.IsNative)
                                listPaymentNotesAccountPayableAdvance = resultPaymentNotesAccountPayable.ObjectEmbbeded
                            Case 2
                                For Each itemPAA As PaymentNotesAccountPayableAdvance In paymentNotes.PaymentNotesAccountPayableAdvance
                                    For Each itemS As AccountPayableShares In itemAccountPayable.AccountPayableShares
                                        If itemPAA.AccountPayableShareId = itemS.Id Then
                                            itemPAA.AdjusmentValue = itemAccountPayable.Adjustment
                                            If itemS.ValueNoteShare > 0 Then
                                                itemPAA.AdjustmentValueShare = itemS.ValueNoteShare
                                            End If
                                            itemPAA.PercentageValue = itemAccountPayable.Percentage
                                            If itemAccountPayable.AccountPayableCommitments IsNot Nothing Then
                                                For Each detail In itemAccountPayable.AccountPayableCommitments
                                                    Dim paymentNoteAccountPayableBudget As PaymentNoteAccountPayableBudget = itemPAA.PaymentNoteAccountPayableBudget.FirstOrDefault(Function(d) d.ObligationDetailId = detail.Id)
                                                    If detail.Value > 0 Then
                                                        If paymentNoteAccountPayableBudget Is Nothing Then
                                                            itemPAA.PaymentNoteAccountPayableBudget.Add(New PaymentNoteAccountPayableBudget With {
                                                            .ObligationDetailId = detail.Id,
                                                            .Value = detail.Value
                                                        })
                                                        Else
                                                            paymentNoteAccountPayableBudget.Value = detail.Value
                                                        End If
                                                    Else
                                                        If paymentNoteAccountPayableBudget IsNot Nothing Then
                                                            paymentNoteAccountPayableBudget.MarkAsDeleted()
                                                        End If
                                                    End If
                                                Next
                                            End If
                                            itemPAA.MarkAsModified()
                                        End If
                                    Next
                                Next
                            Case 3
                                For Each itemS As AccountPayableShares In itemAccountPayable.AccountPayableShares
                                    For Each itemPAA As PaymentNotesAccountPayableAdvance In paymentNotes.PaymentNotesAccountPayableAdvance
                                        If itemPAA.AccountPayableShareId = itemS.Id Then
                                            itemPAA.MarkAsDeleted()
                                            Exit For
                                        End If
                                    Next
                                Next
                        End Select
                    Next
                ElseIf paymentNotes.IndicatesBillAdvance = 1 Then 'Si son anticipos
                    If listAdvancePayments Is Nothing OrElse listAdvancePayments.Count = 0 Then
                        Transaction.Dispose()
                        Return New ActionResult(Of PaymentNotes) With {.StateResult = False, .MessageResult = {"La nota no contiene ningun anticipo en sus detalles"}.ToList()}
                    End If

                    For Each itemAdvancePayments As AdvancePayments In listAdvancePayments
                        Select Case itemAdvancePayments.HandlesAddModifyDelete
                            Case 1
                                resultPaymentNotesAccountPayable = paymentService.CreatePaymentNotesAccountPayableAdvance(Nothing, itemAdvancePayments, listPaymentNotesAccountPayableAdvance, paymentNotes.IsNative)
                                listPaymentNotesAccountPayableAdvance = resultPaymentNotesAccountPayable.ObjectEmbbeded
                            Case 2
                                For Each itemPAA As PaymentNotesAccountPayableAdvance In paymentNotes.PaymentNotesAccountPayableAdvance
                                    If itemAdvancePayments.Id = itemPAA.AdvancePaymentId Then
                                        itemPAA.AdjusmentValue = itemAdvancePayments.Adjustment
                                        itemPAA.PercentageValue = itemAdvancePayments.Percentage
                                        itemPAA.MarkAsModified()
                                    End If
                                Next
                            Case 3
                                While paymentNotes.PaymentNotesAccountPayableAdvance.Count > 0
                                    If itemAdvancePayments.Id = paymentNotes.PaymentNotesAccountPayableAdvance.Item(0).AdvancePaymentId Then
                                        paymentNotes.PaymentNotesAccountPayableAdvance.Item(0).MarkAsDeleted()
                                    End If
                                End While
                        End Select
                    Next
                End If

                If Not FlagDispersion Then
                    ' Validamos si los detalles de la nota llevan TaxRegistration en caso de manejar IVA
                    paymentNotes.PaymentsNoteDetails.ToList.ForEach(Sub(x)
                                                                        Dim conceptNote = _paymentsNoteConceptRepository.GetPaymentNoteConceptById(x.IdAccountPayableConceptNotes)
                                                                        If conceptNote IsNot Nothing Then
                                                                            If conceptNote.ManageTax AndAlso (x.TaxRegistration Is Nothing OrElse x.TaxRegistration = 0) Then
                                                                                Throw New ArgumentNullException("TaxRegistration no puede ser vacío o igual a 0.")
                                                                            End If
                                                                        End If
                                                                    End Sub)
                End If

                If listPaymentNotesAccountPayableAdvance IsNot Nothing Then
                    'Se agrega el listado de paymentNotesAccountPayableAdvance a la entidad principal
                    For Each item As PaymentNotesAccountPayableAdvance In listPaymentNotesAccountPayableAdvance
                        paymentNotes.PaymentNotesAccountPayableAdvance.Add(item)
                    Next
                End If

                '***************** Se guarda o actualiza la nota debito/credito o confirma la nota segun corresponda *****************************************************************************
                If Not modeConfirm Then
                    resultPaymenNotes = SavePaymentsNote(paymentNotes, audit, idSequense)
                Else
                    If paymentNotes.Status = 1 Then
                        resultPaymenNotes = SavePaymentsNote(paymentNotes, audit, idSequense)
                        If resultPaymenNotes.StateResult = False Then
                            Transaction.Dispose()
                            Return New ActionResult(Of PaymentNotes) With {.StateResult = False, .MessageResult = resultPaymenNotes.MessageResult, .Message = resultPaymenNotes.Message}
                        End If
                    End If
                    resultPaymenNotes = ConfirmPaymentNotes(paymentNotes, audit)
                End If
                If resultPaymenNotes.StateResult = False Then
                    Transaction.Dispose()
                Else
                    Transaction.Complete()
                End If
                '********************************************************************************************************************************
                Return New ActionResult(Of PaymentNotes) With {.StateResult = resultPaymenNotes.StateResult, .ObjectEmbbeded = paymentNotes, .MessageResult = resultPaymenNotes.MessageResult, .Message = resultPaymenNotes.Message}
            Catch ex As Exception
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of PaymentNotes) With {.StateResult = False, .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Confirma la nota debito
    ''' </summary>
    ''' <param name="paymentNotes"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConfirmPaymentNotes(paymentNotes As PaymentNotes, audit As AuditMessage, Optional isMassiveConfirm As Boolean = False) As ActionResult(Of PaymentNotes) Implements INotesDebitCreditAdminService.ConfirmPaymentNotes
        If paymentNotes Is Nothing Then
            Throw New ArgumentNullException("paymentNotes")
        End If

        Dim UnitOfWork As IUnitWork = _paymentsNoteRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim message As String = String.Empty
                Dim consecutiveAccounting As String = String.Empty

                Dim paymentNotesXml As String = ConvertToXmlPaymentNote(paymentNotes)

                Dim resultStore = Me._paymentsNoteRepository.SP_ConfirmPaymentNotes(paymentNotesXml, audit.CodeUser, isMassiveConfirm)
                If resultStore Is Nothing OrElse resultStore.Count = 0 OrElse resultStore.Any(Function(r) r.CodeMessage <> 0) Then
                    UnitOfWork.RollbackChanges()
                    transaction.Dispose()

                    If (resultStore IsNot Nothing AndAlso resultStore.Count > 0) Then
                        resultStore.Where(Function(r) r.CodeMessage <> 0).ToList().ForEach(Sub(r) message = message + r.Message + vbCrLf)
                    Else
                        message = "Ha ocurrido un error no identificado."
                    End If

                    Return New ActionResult(Of PaymentNotes) With {.StateResult = False, .MessageResult = {message}.ToList, .Message = message}
                End If

                resultStore.ForEach(Sub(r) message = message + r.Message + vbCrLf)

                transaction.Complete()
                Return New ActionResult(Of PaymentNotes) With {.StateResult = True, .ObjectEmbbeded = paymentNotes, .MessageResult = {message}.ToList(), .Message = message}
            Catch ex As Exception
                UnitOfWork.RollbackChanges()
                transaction.Dispose()

                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of PaymentNotes) With {.StateResult = False, .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

    Private Function ConvertBillsToXml(data As List(Of List(Of String)))
        Dim builder As StringBuilder = New StringBuilder()
        builder.Append("<Data>")

        Dim position As Integer = 0
        For Each item In data
            position = position + 1

            If item.Count <> 2 Then
                builder.Append("<Row>")

                builder.Append("<StatusField>" & 0 & "</StatusField>")
                builder.Append("<MessageField>" & String.Format("El registro {0} no tiene la estructura requerida", position) & "</MessageField>")

                builder.Append("</Row>")
                Continue For
            End If

            builder.Append("<Row>")

            builder.Append("<CountFields>" & item.Count & "</CountFields>")
            builder.Append("<StatusField>" & 1 & "</StatusField>")
            builder.Append("<MessageField>" & "Ok" & "</MessageField>")
            builder.Append("<BillNumber>" & item(0) & "</BillNumber>")
            builder.Append("<Adjusment>" & item(1).ToString().Replace(",", ".") & "</Adjusment>")

            builder.Append("</Row>")
        Next

        builder.Append("</Data>")
        Return builder.ToString
    End Function

    Private Function ConvertAdvancesToXml(data As List(Of List(Of String)))
        Dim builder As StringBuilder = New StringBuilder()
        builder.Append("<Data>")

        Dim position As Integer = 0
        For Each item In data
            position = position + 1

            If item.Count <> 2 Then
                builder.Append("<Row>")

                builder.Append("<StatusField>" & 0 & "</StatusField>")
                builder.Append("<MessageField>" & String.Format("El registro {0} no tiene la estructura requerida", position) & "</MessageField>")

                builder.Append("</Row>")
                Continue For
            End If

            builder.Append("<Row>")

            builder.Append("<CountFields>" & item.Count & "</CountFields>")
            builder.Append("<StatusField>" & 1 & "</StatusField>")
            builder.Append("<MessageField>" & "Ok" & "</MessageField>")
            builder.Append("<Code>" & item(0) & "</Code>")
            builder.Append("<Adjusment>" & item(1).ToString().Replace(",", ".") & "</Adjusment>")

            builder.Append("</Row>")
        Next

        builder.Append("</Data>")
        Return builder.ToString
    End Function

    Private Function ConvertParametersToXml(ParamArray parameters() As Object)
        Dim builder As StringBuilder = New StringBuilder()
        builder.Append("<Data>")

        builder.Append("<Nature>" & parameters.ElementAt(0) & "</Nature>")
        builder.Append("<SupplierId>" & parameters.ElementAt(1) & "</SupplierId>")
        builder.Append("<MainAccountId>" & parameters.ElementAt(2) & "</MainAccountId>")

        builder.Append("</Data>")
        Return builder.ToString
    End Function

    Private Function ConvertToXmlPaymentNote(paymentNotes As PaymentNotes) As String
        Dim builder As StringBuilder = New StringBuilder()

        builder.Append("<PaymentNotes>")

        builder.Append("<Id>" & paymentNotes.Id & "</Id>")
        builder.Append("<Code>" & paymentNotes.Code & "</Code>")
        If paymentNotes.JournalVoucherId IsNot Nothing Then
            builder.Append("<JournalVoucherTypeId>" & paymentNotes.JournalVoucherId & "</JournalVoucherTypeId>")
        End If
        If Not String.IsNullOrEmpty(paymentNotes.EntityName) AndAlso paymentNotes.EntityName <> GetType(PaymentNotes).Name Then
            builder.Append("<OriginEntityName>" & paymentNotes.EntityName & "</OriginEntityName>")
        End If

        builder.Append("<ListPaymentNoteDetailConceptNotHomologated>")

        For Each otherNotHomologatedBook In paymentNotes.PaymentsNoteDetailOthersNotHomologatedBooks
            For Each detail In otherNotHomologatedBook.Value
                builder.Append("<PaymentNoteDetailConceptNotHomologated>")

                builder.Append("<PaymentNoteId>" & paymentNotes.Id & "</PaymentNoteId>")
                builder.Append("<LegalBookId>" & otherNotHomologatedBook.Key & "</LegalBookId>")
                builder.Append("<IdMainAccount>" & detail.IdAccount & "</IdMainAccount>")
                builder.Append("<IdThirdParty>" & detail.IdThirdParty & "</IdThirdParty>")
                If detail.IdCostCenter IsNot Nothing Then
                    builder.Append("<IdCostCenter>" & detail.IdCostCenter & "</IdCostCenter>")
                End If
                If detail.Nature = 1 Then
                    builder.Append("<DebitValue>" & detail.Value.ToString().Replace(",", ".") & "</DebitValue>")
                    builder.Append("<CreditValue>" & 0 & "</CreditValue>")
                Else
                    builder.Append("<DebitValue>" & 0 & "</DebitValue>")
                    builder.Append("<CreditValue>" & detail.Value.ToString().Replace(",", ".") & "</CreditValue>")
                End If
                If detail.Comments IsNot Nothing Then
                    builder.Append("<Detail>" & detail.Comments & "</Detail>")
                End If
                If detail.IdRetentionConcept IsNot Nothing Then
                    builder.Append("<IdRetention>" & detail.IdRetentionConcept & "</IdRetention>")
                    If detail.Percentage IsNot Nothing Then
                        builder.Append("<RetentionRate>" & detail.Percentage.ToString().Replace(",", ".") & "</RetentionRate>")
                    End If
                    builder.Append("<BaseValue>" & detail.BaseValue.ToString().Replace(",", ".") & "</BaseValue>")
                    If (detail.BillingValue = 0) Then
                        builder.Append("<BillingValue>" & detail.BaseValue.ToString().Replace(",", ".") & "</BillingValue>")
                    Else
                        builder.Append("<BillingValue>" & detail.BillingValue.ToString().Replace(",", ".") & "</BillingValue>")
                    End If
                End If

                builder.Append("</PaymentNoteDetailConceptNotHomologated>")
            Next
        Next

        builder.Append("</ListPaymentNoteDetailConceptNotHomologated>")

        builder.Append("</PaymentNotes>")

        Return builder.ToString()
    End Function

#End Region

#Region "IDisposable Support"

    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _accountPayableAdminService.Dispose()
                _advancePaymentsAdminServie.Dispose()
                _paymentControlAdminService.Dispose()
            End If
            _paymentsNoteRepository = Nothing
            _secuenseDRepository = Nothing
            _paymentNotesAccountPayableAdvance = Nothing
            _settingsPaymentsRepository = Nothing
            _accountingRepository = Nothing
            _accountPayableAdminService = Nothing
            _advancePaymentsAdminServie = Nothing
            _accountPayableRepository = Nothing
            _advancePaymentsRepository = Nothing
            _paymentControlAdminService = Nothing
            _repositoryCloseMont = Nothing
            _repositoryMainAccounts = Nothing
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