'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Carlos Ernesto Cordoba
' Created          : 04-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "imports"
Imports System.Data.Entity.Core
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Validation
Imports System.Text
Imports System.Transactions
Imports Application.Accounting
Imports Application.Base
Imports Application.Budget
Imports Application.Glosas
Imports Application.Payments
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Domain.Entities.Service
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources

#End Region

Public Class CashReceiptsAdminService
    Implements ICashReceiptsAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de conceptos de recibos de caja
    ''' </summary>
    Private _cashReceiptsRepository As ICashReceiptsRepository
    Private treasuryService As ITreasuryServices
    ''' <summary>
    ''' repositorio de caja
    ''' </summary>
    ''' <remarks></remarks>
    Private _cashRegisterRepository As ICashRegisterRepository

    Private _cashRegisterAdminService As ICashRegisterAdminService
    ''' <summary>
    ''' repositorio de banco
    ''' </summary>
    ''' <remarks></remarks>
    Private _entityBankAccountRepository As IEntityBankAccountRepository

    Private _entityBankAccountAdminService As IEntityBankAccountAdminService

    ''' <summary>
    ''' contiene el repositorio de las secuencia numérica
    ''' </summary>
    Private _sequenceRepository As ISequenseTreasuryDRepository
    ''' <summary>
    ''' Variable tipo repositorio para parametros de pago
    ''' </summary>
    ''' <remarks></remarks>
    Private _settingsTreasuryRepository As ISettingsTreasuryRepository
    ''' <summary>
    ''' servicios de aplicacion de contabilidad
    ''' </summary>
    Private _accountingAdminService As IAccountingDocumentAdminService
    ''' <summary>
    ''' repositorio de anticipos
    ''' </summary>
    ''' <remarks></remarks>
    Private _advancePaymentRepository As IMoneyAdvanceRepository
    ''' <summary>
    ''' servicios de aplicacion de anticipos
    ''' </summary>
    ''' <remarks></remarks>
    Private _advancePaymentAdminService As IMoneyAdvanceAdminService
    ''' <summary>
    ''' servicios de aplicacion de control de tesoreria
    ''' </summary>
    Private _treasuryControlAdminService As ITreasuryControlAdminService
    Private _RepositoryCloseMont As ICloseMonthRepository
    Private _RepositoryMainAccounts As IPUCRepository
    Private _accountReceivableRepository As IAccountReceivableRepository
    Private _documentTypeRepository As IDocumentTypeRepository

    Private _portfolioGlosadaRepository As IPortfolioGlosadaRepository

    Private _partialPaymentsCAdminService As IPartialPaymentsCAdminService
    Private _portfolioAdvanceRepository As IPortfolioAdvanceRepository

    Private _cashReceiptsConcepts As ICashReceiptConceptRepository
    '********************presupuesto
    Dim _budgetSequenceRepository As IBudgetSequenceRepository
    Dim _budgetRepository As IBudgetRepository

    Dim _categoryRepository As IBudgetItemRepository

    Dim sequence As BudgetSequence = Nothing

    Dim _recognitionAdminService As IRecognitionAdminService

    Dim _ReclassificationRepository As IReclassificationRepository

    Dim _accountPayableRepository As IAccountPayableRepository

#End Region

#Region "Builder"
    Public Sub New(ByVal cashReceiptsRepository As ICashReceiptsRepository, ByVal cashRegiterRepository As ICashRegisterRepository, cashRegisterAdminService As ICashRegisterAdminService,
                   ByVal entityBankAccountRepository As IEntityBankAccountRepository, entityBankAccountAdminService As IEntityBankAccountAdminService, settingsTreasuryRepository As ISettingsTreasuryRepository,
                   ByVal sequenceRepository As ISequenseTreasuryDRepository, accountingAdminService As IAccountingDocumentAdminService, advancePaymentRepository As IMoneyAdvanceRepository, advancePaymentAdminService As IMoneyAdvanceAdminService,
                   treasuryControlAdminService As ITreasuryControlAdminService, repositoryPUC As IPUCRepository, RepositoryCloseMonth As ICloseMonthRepository, accountReceivableRepository As IAccountReceivableRepository,
                   _treasuryService As ITreasuryServices, documentTypeRepository As IDocumentTypeRepository, portfolioGlosadaRepository As IPortfolioGlosadaRepository,
                   partialPaymentsCAdminService As IPartialPaymentsCAdminService, portfolioAdvanceRepository As IPortfolioAdvanceRepository, cashReceiptsConcepts As ICashReceiptConceptRepository,
                   budgetSequenceRepository As IBudgetSequenceRepository, budgetRepository As IBudgetRepository, categoryRepository As IBudgetItemRepository, recognitionAdminService As IRecognitionAdminService, ReclassificationRepository As IReclassificationRepository,
                   accountPayableRepository As IAccountPayableRepository)
        If cashReceiptsRepository Is Nothing Then
            Throw New ArgumentNullException("cashReceiptsRepository")
        End If
        If cashRegiterRepository Is Nothing Then
            Throw New ArgumentNullException("cashRegisterRepository")
        End If
        If cashRegisterAdminService Is Nothing Then
            Throw New ArgumentNullException("cashRegisterAdminService")
        End If
        If entityBankAccountRepository Is Nothing Then
            Throw New ArgumentNullException("entityBankAccountRepository")
        End If
        If entityBankAccountAdminService Is Nothing Then
            Throw New ArgumentNullException("entityBankAccountAdminService")
        End If
        If settingsTreasuryRepository Is Nothing Then
            Throw New ArgumentNullException("settingsTreasuryRepository")
        End If
        If sequenceRepository Is Nothing Then
            Throw New ArgumentNullException("sequenceRepository")
        End If
        If accountingAdminService Is Nothing Then
            Throw New ArgumentNullException("accountingAdminService")
        End If
        If advancePaymentAdminService Is Nothing Then
            Throw New ArgumentNullException("advancePaymentAdminService")
        End If
        If advancePaymentRepository Is Nothing Then
            Throw New ArgumentNullException("advancePaymentRepository")
        End If
        If treasuryControlAdminService Is Nothing Then
            Throw New ArgumentNullException("treasuryControlAdminService")
        End If
        If ReclassificationRepository Is Nothing Then
            Throw New ArgumentNullException("ReclassificationRepository")
        End If

        _budgetSequenceRepository = budgetSequenceRepository
        _budgetRepository = budgetRepository
        _categoryRepository = categoryRepository
        treasuryService = _treasuryService
        _cashReceiptsRepository = cashReceiptsRepository
        _cashRegisterRepository = cashRegiterRepository
        _cashRegisterAdminService = cashRegisterAdminService
        _entityBankAccountRepository = entityBankAccountRepository
        _entityBankAccountAdminService = entityBankAccountAdminService
        _sequenceRepository = sequenceRepository
        _settingsTreasuryRepository = settingsTreasuryRepository
        _accountingAdminService = accountingAdminService
        _advancePaymentAdminService = advancePaymentAdminService
        _advancePaymentRepository = advancePaymentRepository
        _treasuryControlAdminService = treasuryControlAdminService
        _RepositoryCloseMont = RepositoryCloseMonth
        _RepositoryMainAccounts = repositoryPUC
        _accountReceivableRepository = accountReceivableRepository
        _documentTypeRepository = documentTypeRepository
        _portfolioGlosadaRepository = portfolioGlosadaRepository
        _partialPaymentsCAdminService = partialPaymentsCAdminService
        _portfolioAdvanceRepository = portfolioAdvanceRepository
        _cashReceiptsConcepts = cashReceiptsConcepts
        _recognitionAdminService = recognitionAdminService
        _ReclassificationRepository = ReclassificationRepository
        _accountPayableRepository = accountPayableRepository
    End Sub
#End Region

#Region "Methods"

    Public Function GenerateCashReceiptsREST(invoiceNumber As String, value As Decimal, documentDate As DateTime) As ActionResult Implements ICashReceiptsAdminService.GenerateCashReceiptsREST
        Dim accountReceivable = _cashReceiptsRepository.GetAccountReceivableByInvoiceNumberCashReceipts(invoiceNumber)
        If accountReceivable Is Nothing Then
            Return New ActionResult With {.StatusCode = eStatusResult.WARNING, .Message = "La factura no existe"}
        End If
        Dim third = _cashReceiptsRepository.GetThird(accountReceivable.ThirdPartyId)
        Dim CashReceipts As New CashReceipts
        With CashReceipts
            .IdThirdParty = accountReceivable.ThirdPartyId
            .CollectType = 2 'bancos
            .IdMainAccount = accountReceivable.AccountReceivableAccounting(0).MainAccountId
            .IdCostCenter = accountReceivable.CostCenterId
            .Detail = "Recibo de Caja creado por servicio REST"
            .DocumentDate = documentDate
            .IdBankAccount = _cashReceiptsRepository.GetFirstAccount().Id
            .PaymentResponsibles = third.Name
            .Value = value
            .OperatingUnitId = accountReceivable.OperatingUnitId
            .Status = 2
        End With
        Dim paymentMethod As New PaymentMethods
        With paymentMethod
            .PaymentMethodTypes = 4 'consignacion
            .Value = value
            .IdEntityBankAccount = Nothing
            .DepositType = 1
            .DepositNumber = "656565656"
            .Value = value
        End With


        Dim concept = _cashReceiptsRepository.GetConcept(accountReceivable.AccountReceivableAccounting(0).MainAccountId)
        If concept Is Nothing Then
            Return New ActionResult With {.StatusCode = eStatusResult.WARNING, .Message = "No se encontro concepto para la cuenta contable de la factura"}
        End If

        Dim cashReceiptDetails As New CashReceiptDetails
        With cashReceiptDetails
            .IdThirdParty = accountReceivable.ThirdPartyId
            .IdMainAccount = accountReceivable.AccountReceivableAccounting(0).MainAccountId
            .IdCostCenter = accountReceivable.CostCenterId
            .Nature = concept.Nature
            .IdCashReceiptConcept = concept.Id
            .CashReceiptConceptAffectation = 2
            .Value = value
        End With
        Dim cashReceiptAccountReceivable As New CashReceiptAccountReceivable
        With cashReceiptAccountReceivable
            .AccountReceivableId = accountReceivable.Id
            .InvoiceNumber = invoiceNumber
            .Value = value
        End With
        cashReceiptDetails.CashReceiptAccountReceivable.Add(cashReceiptAccountReceivable)
        CashReceipts.PaymentMethods.Add(paymentMethod)
        CashReceipts.CashReceiptDetails.Add(cashReceiptDetails)

        'se envia a guardar el recibo de caja
        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try

                Dim xml = CashReceipts.ToXML()
                ''My.Computer.FileSystem.WriteAllText("C:\xml.txt", xml, True)
                CType(_cashReceiptsRepository.UnitWork, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
                Dim result = _cashReceiptsRepository.GenerateCashReceiptSP(xml, "999", 2).ToList().ElementAt(0)
                If result.CodeMessage = "999" Then
                    Return New ActionResult With {.StatusCode = eStatusResult.WARNING, .Message = result.Message}
                Else
                    transaction.Complete()
                    Return New ActionResult With {.StatusCode = eStatusResult.SUCCESS, .Message = result.Message}


                End If
            Catch ex As OptimisticConcurrencyException
                transaction.Dispose()
                Return New ActionResult With {.StatusCode = eStatusResult.EXCEPTION, .Message = ResourceManager.GetString("ErrorConcurrence")}
            Catch ex As InvalidOperationException
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult With {.StatusCode = eStatusResult.EXCEPTION, .Message = ResourceManager.GetString("ErrorUnknown")}
            Catch ex As Exception
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
            End Try
        End Using

    End Function
    ''' <summary>
    ''' metodo para eliminar un recibo de caja
    ''' </summary>
    ''' <param name="CashReceipts">The cash receipts.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">CashReceipts</exception>
    Public Function DeleteCashReceipts(CashReceipts As CashReceipts, audit As AuditMessage) As ActionResult Implements ICashReceiptsAdminService.DeleteCashReceipts
        If CashReceipts Is Nothing Then
            Throw New ArgumentNullException("CashReceipts")
        End If
        Dim unitOfWork As IUnitWork = Me._cashReceiptsRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                CashReceipts.ModificationUser = audit.CodeUser
                CashReceipts.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of CashReceipts)(CashReceipts, audit, status)
                CashReceipts.MarkAsDeleted()
                Me._cashReceiptsRepository.SaveEntity(CashReceipts)
                unitOfWork.Commit()
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}
            End Using
        Catch ex As OptimisticConcurrencyException
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-999"}), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As DbUpdateException
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' metodo para obtener el recibo de caja por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">
    ''' </exception>
    Public Function GetCashReceiptsByCode(code As String, audit As AuditMessage) As ActionResult(Of CashReceipts) Implements ICashReceiptsAdminService.GetCashReceiptsByCode
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim cashReceipts As CashReceipts = Me._cashReceiptsRepository.GetCashReceiptsByCode(code.Trim())
            If cashReceipts IsNot Nothing AndAlso cashReceipts.Id > 0 Then
                Dim auditProcess As New IndigoAuditSimpleEntity(Of CashReceipts)(cashReceipts, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditProcess.Execute()
            End If
            Return New ActionResult(Of CashReceipts) With {.StateResult = True, .ObjectEmbbeded = cashReceipts}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CashReceipts) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' metodo para obtener el recibo de caja por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    ''' <exception cref="System.InvalidOperationException">id</exception>
    Public Function GetCashReceiptsById(id As Integer) As CashReceipts Implements ICashReceiptsAdminService.GetCashReceiptsById
        If id = 0 Then
            Throw New InvalidOperationException("id")
        End If
        Try
            Return _cashReceiptsRepository.GetCashReceiptsById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SaveAndConfirm(CashReceipts As CashReceipts, audit As AuditMessage, IndigoSessionValues As SessionValues, Optional idSequence As Integer = 0, Optional ByVal sequenceC As Domain.Entities.TreasurySequence = Nothing) As ActionResult(Of CashReceipts) Implements ICashReceiptsAdminService.SaveAndConfirm
        Return SaveCashReceipts(CashReceipts, audit)
    End Function

    ''' <summary>
    ''' metodo para generar el mensaje del retorno con los documentos generados
    ''' </summary>
    ''' <param name="message"></param>
    ''' <param name="code"></param>
    ''' <param name="documentName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateMessageReturn(message As String, code As String, documentName As String) As StringBuilder
        Dim messageList = message.Split("-")
        Dim messageReturn = New StringBuilder
        If messageList.Length > 1 Then
            messageReturn.AppendLine("El registro se guardo con código " + code)
            messageReturn.AppendLine("Se genero el documento contable " + documentName)
            messageReturn.AppendLine("consecutivo " + messageList(0))
            messageReturn.AppendLine("reconocimiento " + messageList(1))
            messageReturn.AppendLine("recaudo " + messageList(2))

        Else
            messageReturn.AppendLine("El registro se guardo con código " + code)
            messageReturn.AppendLine("Se genero el documento contable " + documentName)
            messageReturn.AppendLine("consecutivo " + messageList(0))

        End If
        Return messageReturn
    End Function

    ''' <summary>
    ''' metodo para guardar un recibo de caja
    ''' </summary>
    ''' <param name="CashReceipts">The cash receipts.</param>
    ''' <param name="audit">The audit.</param>
    ''' <param name="idSequence"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">CashReceipts</exception>
    Public Function SaveCashReceipts(CashReceipts As CashReceipts, audit As AuditMessage, Optional idSequence As Integer = 0, Optional ByVal sequenceC As Domain.Entities.TreasurySequence = Nothing) As ActionResult(Of CashReceipts) Implements ICashReceiptsAdminService.SaveCashReceipts
        If CashReceipts Is Nothing Then
            Throw New ArgumentNullException("CashReceipts")
        End If

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                If CashReceipts.Status = 2 Then
                    Dim resultValidation = treasuryService.ValidateCashReceipts(CashReceipts, audit.IdUser, audit.CodeUser, True)
                    If resultValidation.StateResult = False Then
                        transaction.Dispose()
                        Return New ActionResult(Of CashReceipts) With {.StateResult = False, .Message = resultValidation.Message}
                    End If
                End If
                'Valido que no haya ninguno con forma de pago cero
                If CashReceipts?.PaymentMethods?.Any(Function(type) type.PaymentMethodTypes = 0) Then
                    transaction.Dispose()
                    Return New ActionResult(Of CashReceipts) With {.StateResult = False, .Message = "Debe seleccionar un tipo de forma de pago"}
                End If

                Dim xml = CashReceipts.ToXML()
                CType(_cashReceiptsRepository.UnitWork, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
                Dim result = _cashReceiptsRepository.GenerateCashReceiptSP(xml, audit.CodeUser, audit.CompanyType).ToList().ElementAt(0)
                If result.CodeMessage = "999" Then
                    Return New ActionResult(Of CashReceipts) With {.StateResult = False, .Message = result.Message}
                Else
                    Dim cashReceipt = _cashReceiptsRepository.GetCashReceiptsById(result.CashReceiptId)
                    transaction.Complete()
                    If result.Message.Length = 0 Then
                        Return New ActionResult(Of CashReceipts) With {.StateResult = True, .ObjectEmbbeded = cashReceipt}
                    Else
                        Return New ActionResult(Of CashReceipts) With {.StateResult = True, .Message = result.Message, .ObjectEmbbeded = cashReceipt}
                    End If

                End If
            Catch ex As OptimisticConcurrencyException
                transaction.Dispose()
                Return New ActionResult(Of CashReceipts) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorConcurrence")}
            Catch ex As InvalidOperationException
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of CashReceipts) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
            Catch ex As Exception
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of CashReceipts) With {.StateResult = False, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' metodo para confirmar un recibo de caja
    ''' </summary>
    ''' <param name="idCashReceipts"></param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <Obsolete("This method is deprecated, use SaveCashReceipts with status 2 instead.")>
    Public Function ConfirmCashReceipts(idCashReceipts As Integer, audit As AuditMessage, IndigoSessionValues As SessionValues, Optional CashReceipts As CashReceipts = Nothing) As ActionResult(Of String) Implements ICashReceiptsAdminService.ConfirmCashReceipts
        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Dim flagGlosas As Boolean
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim unitOfWorkCashReceipt As IUnitWork = _cashReceiptsRepository.UnitWork
                Dim accountReceivableUnitOfWork As IUnitWork = _accountReceivableRepository.UnitWork
                Dim cashRegister As CashRegisters = Nothing
                Dim bankAccount As EntityBankAccounts = Nothing
                Dim settingTreasury As SettingsTreasury = Nothing

                Dim auditProcess As IndigoAuditSimpleEntity(Of CashReceipts)

                Dim cashReceipt As CashReceipts
                If CashReceipts Is Nothing Then
                    cashReceipt = _cashReceiptsRepository.GetCashReceiptsById(idCashReceipts)
                    Dim cashReceiptDetail = _cashReceiptsRepository.GetCashReceiptsDetailByIdCashReceipt(cashReceipt.Id)
                    Dim paymentMethods = _cashReceiptsRepository.GetPaymentMethodsByIdCashReceipt(cashReceipt.Id)

                    For Each item In cashReceiptDetail
                        cashReceipt.CashReceiptDetails.Add(item)
                    Next

                    For Each item In paymentMethods
                        cashReceipt.PaymentMethods.Add(item)
                    Next
                Else
                    cashReceipt = CashReceipts
                End If
                Dim userIndigo As String
                Dim idUserIndigo As Integer
                If IndigoSessionValues IsNot Nothing Then
                    userIndigo = IndigoSessionValues.UserIndigo
                    idUserIndigo = IndigoSessionValues.UserIndigoId
                ElseIf audit IsNot Nothing Then
                    userIndigo = audit.CodeUser
                    idUserIndigo = audit.IdUser
                End If
                Dim resultValidation = treasuryService.ValidateCashReceipts(cashReceipt, idUserIndigo, userIndigo, True)
                If resultValidation.StateResult = True Then
                    settingTreasury = _settingsTreasuryRepository.GetSettingsTreasuryByIdUnitOperative(cashReceipt.OperatingUnitId)

                    If cashReceipt.CollectType = 1 Then
                        cashRegister = _cashRegisterRepository.GetCashRegisterById(cashReceipt.IdCashRegister)
                        cashReceipt.CurrencyId = cashRegister.CurrencyId
                        Dim treasuryBalance As New TreasuryBalance With {.DocumentNumber = cashReceipt.Code, .DocumentDate = cashReceipt.DocumentDate, .DocumentType = 1, .Nature = 1, .CashRegisterId = cashRegister.Id, .PreviousBalance = cashRegister.CurrentBalance, .ValueMovement = cashReceipt.Value, .CreationDate = DateTime.Now}
                        cashRegister.CurrentBalance += cashReceipt.Value
                        cashRegister.IsMovement = True
                        cashRegister.TreasuryBalance.Add(treasuryBalance)
                        cashRegister.MarkAsModified()
                        Dim result = _cashRegisterAdminService.SaveCashRegister(cashRegister, audit)
                        If result.StateResult = False Then
                            transaction.Dispose()
                            Return New ActionResult(Of String) With {.StateResult = False, .ObjectEmbbeded = String.Join(Environment.NewLine, result.MessageResult), .MessageResult = result.MessageResult}
                        End If
                    Else
                        bankAccount = _entityBankAccountRepository.GetEntityBankAccountById(cashReceipt.IdBankAccount)
                        CashReceipts.CurrencyId = bankAccount.CurrencyId
                        Dim treasuryBalance As New TreasuryBalance With {.DocumentNumber = cashReceipt.Code, .DocumentDate = cashReceipt.DocumentDate, .DocumentType = 1, .Nature = 1, .EntityBankAccountId = bankAccount.Id, .PreviousBalance = bankAccount.CurrentBalance, .ValueMovement = cashReceipt.Value, .CreationDate = DateTime.Now}
                        bankAccount.TreasuryBalance.Add(treasuryBalance)
                        bankAccount.CurrentBalance += cashReceipt.Value
                        bankAccount.MarkAsModified()
                        Dim result = _entityBankAccountAdminService.SaveEntityBankAccount(bankAccount, audit)
                        If result.StateResult = False Then
                            transaction.Dispose()
                            Return New ActionResult(Of String) With {.StateResult = False, .ObjectEmbbeded = String.Join(Environment.NewLine, result.MessageResult), .MessageResult = result.MessageResult}
                        End If
                    End If
                    Dim conceptDetail As CashReceiptDetails
                    conceptDetail = cashReceipt.CashReceiptDetails.Where(Function(x) x.CashReceiptConceptAffectation = 2).FirstOrDefault()
                    If conceptDetail IsNot Nothing AndAlso conceptDetail.CashReceiptAccountReceivable.Count > 0 Then
                        For Each item In conceptDetail.CashReceiptAccountReceivable
                            Dim invoice = _accountReceivableRepository.GetAccountReceivableById(item.AccountReceivableId)
                            'hacemos integracion con pagos parciales de glosas
                            Dim accounting = invoice.AccountReceivableAccounting.Where(Function(x) x.AccountReceivableId = item.AccountReceivableId And x.MainAccountId = conceptDetail.IdMainAccount).FirstOrDefault()
                            If IndigoSessionValues IsNot Nothing Then
                                If IndigoSessionValues.IndigoCompanyType = 1 Then ' empresa privada
                                    If invoice.AccountObjectionRemediedId IsNot Nothing Then
                                        If conceptDetail.IdMainAccount = invoice.AccountObjectionRemediedId Then

                                            'consulto la informacion de cartera de glosa
                                            Dim glosaPortfolioGlosada = _portfolioGlosadaRepository.GetPortfolioGlosada(invoice.InvoiceNumber)
                                            If glosaPortfolioGlosada.Id = 0 Then
                                                Return New ActionResult(Of String) With {.StateResult = False, .ObjectEmbbeded = "No se encontro información en la cartera de glosa"}
                                            End If

                                            'creamos el objeto de pagos parciales
                                            Dim partialPaymentsC As New Domain.Entities.PartialPaymentsC
                                            With partialPaymentsC
                                                .CustomerId = invoice.CustomerId
                                                .ConfirmUser = audit.CodeUser
                                                .DocumentDate = cashReceipt.DocumentDate
                                                .State = 2
                                                .Comment = "Creado desde recibo de caja"
                                                Dim partialPaymentD As New Domain.Entities.PartialPaymentsD
                                                With partialPaymentD
                                                    .PortfolioGlosaId = glosaPortfolioGlosada.Id
                                                    .InvoiceNumber = invoice.InvoiceNumber
                                                    .InvoiceDate = invoice.AccountReceivableDate
                                                    .RadicatedNumber = glosaPortfolioGlosada.RadicatedNumber
                                                    .RadicatedDate = glosaPortfolioGlosada.RadicatedDate
                                                    .PatientCode = glosaPortfolioGlosada.PatientCode
                                                    .PatientName = glosaPortfolioGlosada.PatientName
                                                    .ContractCode = glosaPortfolioGlosada.ContractCode
                                                    .ValuePendingConciliation = glosaPortfolioGlosada.BalanceGlosa
                                                    .ValuePayments = item.Value
                                                    .State = 2
                                                End With
                                                .PartialPaymentsD.Add(partialPaymentD)
                                            End With

                                            Dim journalXml = convertPartialPaymentsToXml(partialPaymentsC)
                                            Dim resultStore = _ReclassificationRepository.SavePartialPayments(journalXml, audit.CodeUser)
                                            If resultStore.CodeMessage <> 0 Then
                                                Return New ActionResult(Of String) With {.StateResult = False, .ObjectEmbbeded = "Fallo al crear pago parcial glosas"}
                                            End If

                                            'Dim resultGlosa = _partialPaymentsCAdminService.SaveAndConfirmGlossPaymentsC(partialPaymentsC, IndigoSessionValues)
                                            'flagGlosas = resultGlosa.StateResult
                                            'If resultGlosa.StateResult = False Then
                                            '    Return New ActionResult(Of String) With {.StateResult = False, .ObjectEmbbeded = "Ocurrio un error afectando glosas"}
                                            'End If
                                        End If
                                    End If
                                Else
                                    If invoice.AccountRadicateId IsNot Nothing Then
                                        If conceptDetail.IdMainAccount = invoice.AccountRadicateId Then

                                            'consulto la informacion de cartera de glosa
                                            Dim glosaPortfolioGlosada = _portfolioGlosadaRepository.GetPortfolioGlosada(invoice.InvoiceNumber)
                                            If glosaPortfolioGlosada.Id = 0 Then
                                                Return New ActionResult(Of String) With {.StateResult = False, .ObjectEmbbeded = "No se encontro información en la cartera de glosa"}
                                            End If
                                            Dim valuePay = accounting.Balance - glosaPortfolioGlosada.BalanceGlosa - item.Value

                                            If valuePay > 0 Then
                                                'creamos el objeto de pagos parciales
                                                Dim partialPaymentsC As New Domain.Entities.PartialPaymentsC
                                                With partialPaymentsC
                                                    .CustomerId = invoice.CustomerId
                                                    .ConfirmUser = audit.CodeUser
                                                    .DocumentDate = cashReceipt.DocumentDate
                                                    .State = 2
                                                    .Comment = "Creado desde recibo de caja"
                                                    Dim partialPaymentD As New Domain.Entities.PartialPaymentsD
                                                    With partialPaymentD
                                                        .PortfolioGlosaId = glosaPortfolioGlosada.Id
                                                        .InvoiceNumber = invoice.InvoiceNumber
                                                        .InvoiceDate = invoice.AccountReceivableDate
                                                        .RadicatedNumber = glosaPortfolioGlosada.RadicatedNumber
                                                        .RadicatedDate = glosaPortfolioGlosada.RadicatedDate
                                                        .PatientCode = glosaPortfolioGlosada.PatientCode
                                                        .PatientName = glosaPortfolioGlosada.PatientName
                                                        .ContractCode = glosaPortfolioGlosada.ContractCode
                                                        .ValuePendingConciliation = glosaPortfolioGlosada.BalanceGlosa
                                                        .ValuePayments = valuePay
                                                        .State = 2
                                                    End With
                                                    .PartialPaymentsD.Add(partialPaymentD)
                                                End With
                                                Dim resultGlosa = _partialPaymentsCAdminService.SaveAndConfirmGlossPaymentsC(partialPaymentsC, IndigoSessionValues)
                                                flagGlosas = resultGlosa.StateResult
                                                If resultGlosa.StateResult = False Then
                                                    Return New ActionResult(Of String) With {.StateResult = False, .ObjectEmbbeded = "Ocurrio un error afectando glosas"}
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If



                            invoice.Balance -= item.Value
                            invoice.MarkAsModified()

                            accounting.Balance -= item.Value
                            accounting.MarkAsModified()
                            Dim value = item.Value
                            For Each share In invoice.AccountReceivableShare
                                If value <= 0 Then
                                    Exit For
                                End If
                                Dim cashReceiptAccountReceivableShare As New CashReceiptAccountReceivableShare
                                cashReceiptAccountReceivableShare.AccountReceivableShareId = share.Id
                                Dim balance = share.Balance
                                If value >= share.Balance Then
                                    cashReceiptAccountReceivableShare.Value = share.Balance
                                    share.PaymentValue += share.Balance
                                    share.Balance -= share.Balance
                                Else
                                    cashReceiptAccountReceivableShare.Value = value
                                    share.PaymentValue += value
                                    share.Balance -= value
                                End If
                                value -= balance
                                share.MarkAsModified()
                                item.CashReceiptAccountReceivableShare.Add(cashReceiptAccountReceivableShare)
                                _accountReceivableRepository.SaveEntity(invoice)
                                accountReceivableUnitOfWork.Commit()
                            Next
                        Next
                    End If
                    If cashReceipt.PortfolioAdvance.Count > 0 Then
                        cashReceipt.PortfolioAdvance.Item(0).Status = 2
                        cashReceipt.PortfolioAdvance.Item(0).ConfirmationDate = DateTime.Now
                        cashReceipt.PortfolioAdvance.Item(0).ConfirmationUser = audit.CodeUser
                        cashReceipt.PortfolioAdvance.Item(0).MarkAsModified()
                    End If
                    'reintegro de anticipos
                    conceptDetail = cashReceipt.CashReceiptDetails.Where(Function(x) x.CashReceiptConceptAffectation = 3).FirstOrDefault()
                    If conceptDetail IsNot Nothing AndAlso conceptDetail.CashReceiptAdvancePayment.Count > 0 Then
                        For Each item In conceptDetail.CashReceiptAdvancePayment
                            Dim advance = _advancePaymentRepository.GetMoneyAdvanceById(item.AdvancePaymentId)
                            advance.Balance -= item.PaymentValue
                            advance.CreditValue += item.PaymentValue
                            advance.MarkAsModified()
                            Dim result = _advancePaymentAdminService.SaveMoneyAdvance(advance, audit)
                            If result.StateResult = False Then
                                transaction.Dispose()
                                Return New ActionResult(Of String) With {.StateResult = False, .ObjectEmbbeded = String.Join(Environment.NewLine, result.MessageResult), .MessageResult = result.MessageResult}
                            End If
                        Next
                    End If
                    'reintegro de cuentas por pagar
                    conceptDetail = cashReceipt.CashReceiptDetails.Where(Function(x) x.CashReceiptConceptAffectation = 4).FirstOrDefault()
                    If conceptDetail IsNot Nothing AndAlso conceptDetail.CashReceiptDetailAccountPayable.Count > 0 Then
                        For Each item In conceptDetail.CashReceiptDetailAccountPayable
                            Dim refundValue = item.RefundValue

                            Dim accountPayableTmp = _accountPayableRepository.GetAccountPayableByIdForNotes(item.AccountPayableId)
                            For Each itemShare In accountPayableTmp.AccountPayableShares
                                If refundValue <= 0 Then
                                    Exit For
                                End If
                                Dim maximunValueRefund = itemShare.InitialValue - itemShare.Balance
                                If refundValue > maximunValueRefund Then
                                    itemShare.Balance += maximunValueRefund
                                    itemShare.CreditValue += maximunValueRefund
                                    refundValue -= maximunValueRefund
                                Else
                                    itemShare.Balance += refundValue
                                    itemShare.CreditValue += refundValue
                                    refundValue = 0
                                End If
                            Next
                            accountPayableTmp.Balance = accountPayableTmp.AccountPayableShares.Sum(Function(x) x.Balance)
                            accountPayableTmp.MarkAsModified()
                            _accountPayableRepository.SaveEntity(accountPayableTmp)
                            _accountPayableRepository.UnitWork.Commit()
                        Next
                    End If

                    'INTEGRACION CON PRESUPUESTO
                    Dim recognition As Recognition = Nothing
                    'guardo el reconocimietno y aumento la secuencia de presupuesto
                    Dim resultRecognition As ActionResult(Of Recognition) = Nothing
                    'valido que el recibo de caja haga interface con presupuesto
                    If cashReceipt.AllowBudgetInterface Then
                        'presupuesto
                        Dim budget As Domain.Entities.Budget = Nothing
                        'rubro 
                        Dim category As Category = Nothing
                        For Each detail In cashReceipt.CashReceiptDetails
                            Dim concept = _cashReceiptsConcepts.GetCashReceiptConceptById(detail.IdCashReceiptConcept)
                            If concept.AffectBudget Then

                                If recognition Is Nothing Then
                                    'creo la cabecera del reconocimiento
                                    recognition = New Recognition
                                    With recognition
                                        Dim resultGenerateSecuence = GenerateSequence()
                                        If resultGenerateSecuence.StateResult = False Then
                                            transaction.Dispose()
                                            Return New ActionResult(Of String) With {.StateResult = False, .Message = resultGenerateSecuence.Message}
                                        End If
                                        'consulto el presupuesto y el rubro
                                        budget = _budgetRepository.GetBudgetByIdAsNotTracking(concept.BudgetId)
                                        category = _categoryRepository.GetCategoryById(budget.CategoryId)

                                        .Code = resultGenerateSecuence.Message
                                        .BudgetaryValidityId = category.BudgetaryValidityId
                                        .Document = cashReceipt.Code
                                        .DocumentDate = cashReceipt.DocumentDate
                                        .Observations = "Reconocimiento Creado por recibo de caja - " + cashReceipt.Code
                                        .RecognitonType = 1 'reconocimiento
                                        .ThirdPartyId = cashReceipt.IdThirdParty
                                        .DependencyId = 1 ' preguntar de donde se obtiene
                                        .AutomaticCollection = True 'recaudo automatico 
                                        .Applicant = ""
                                        .Status = 2 'confirmado
                                        .CreationUser = userIndigo
                                        .CreationDate = DateTime.Now
                                        .ConfirmationUser = userIndigo
                                        .ConfirmationDate = DateTime.Now
                                    End With
                                End If

                                'valido que no exista un detalle con el mismo rubro
                                Dim recognitionDetail = recognition.RecognitionDetail.Where(Function(x) x.CategoryId = budget.CategoryId And x.RevenueTypeId = budget.RevenueTypeId).FirstOrDefault()
                                If recognitionDetail Is Nothing Then
                                    recognitionDetail = New RecognitionDetail
                                    With recognitionDetail
                                        .CategoryId = budget.CategoryId
                                        .RevenueTypeId = budget.RevenueTypeId
                                        .InitialValue = detail.Value
                                        .TotalRecognition = .InitialValue
                                        .Balance = .InitialValue
                                    End With
                                    recognition.RecognitionDetail.Add(recognitionDetail)
                                Else
                                    recognitionDetail.InitialValue += detail.Value
                                    recognitionDetail.TotalRecognition = recognitionDetail.InitialValue
                                    recognitionDetail.Balance = recognitionDetail.InitialValue
                                End If

                            End If
                        Next


                        If recognition IsNot Nothing Then
                            resultRecognition = _recognitionAdminService.SaveRecognition(recognition, Nothing, audit)
                            If resultRecognition.StateResult = False Then
                                transaction.Dispose()
                                Return New ActionResult(Of String) With {.StateResult = False, .ObjectEmbbeded = resultRecognition.Message}
                            End If


                            sequence.BudgetSequenceDetail(0).Next += 1
                            sequence.BudgetSequenceDetail(0).MarkAsModified()
                            Me._budgetSequenceRepository.SaveEntity(sequence)
                            _budgetSequenceRepository.UnitWork.Commit()
                            'cashReceipt.RecognitionId = recognition.Id
                        End If
                    End If


                    cashReceipt.Status = 2
                    cashReceipt.ConfirmationDate = DateTime.Now
                    cashReceipt.ConfirmationUser = userIndigo
                    cashReceipt.ModificationDate = DateTime.Now
                    cashReceipt.ModificationUser = userIndigo
                    cashReceipt.MarkAsModified()
                    Dim resultGeneralLedger = generateJournalVoucher(settingTreasury, cashReceipt, cashRegister, bankAccount, eActionSave.Confirm, audit)
                    If resultGeneralLedger.StateResult = False Then
                        transaction.Dispose()
                        Return New ActionResult(Of String) With {.StateResult = False, .ObjectEmbbeded = resultGeneralLedger.Message}
                    End If
                    _cashReceiptsRepository.SaveEntity(cashReceipt)
                    unitOfWorkCashReceipt.Commit()
                    Dim treasuryControl = _treasuryControlAdminService.GetTreasuryControlByDocumentNumber(cashReceipt.Code, 1)
                    treasuryControl.MarkAsDeleted()
                    _treasuryControlAdminService.DeleteTreasuryControl(treasuryControl, audit)
                    auditProcess = New IndigoAuditSimpleEntity(Of CashReceipts)(cashReceipt, audit, Infrastructure.CrossCutting.Audit.Actions.Confirm, cashReceipt.OriginalValue)
                    auditProcess.Execute()
                    transaction.Complete()

                    If flagGlosas Then
                        If recognition IsNot Nothing Then
                            Return New ActionResult(Of String) With {.StateResult = True, .ObjectEmbbeded = resultGeneralLedger.ObjectEmbbeded, .MessageResult = {"Glosa"}.ToList()}
                        Else
                            Return New ActionResult(Of String) With {.StateResult = True, .ObjectEmbbeded = resultGeneralLedger.ObjectEmbbeded + "-" + resultRecognition.ObjectEmbbeded.Code + "-" + resultRecognition.Message, .MessageResult = {"Glosa"}.ToList()}
                        End If
                    Else
                        If recognition IsNot Nothing Then
                            Return New ActionResult(Of String) With {.StateResult = True, .ObjectEmbbeded = resultGeneralLedger.ObjectEmbbeded + "-" + resultRecognition.ObjectEmbbeded.Code + "-" + resultRecognition.Message}
                        Else
                            Return New ActionResult(Of String) With {.StateResult = True, .ObjectEmbbeded = resultGeneralLedger.ObjectEmbbeded}
                        End If

                    End If
                Else
                    transaction.Dispose()
                    Return New ActionResult(Of String) With {.StateResult = False, .ObjectEmbbeded = resultValidation.Message}
                End If
            Catch ex As OptimisticConcurrencyException
                transaction.Dispose()
                Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = {ResourceManager.GetString("ErrorConcurrence")}.ToList()}
            Catch ex As DbEntityValidationException
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", IndigoSessionValues)
                Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = {ResourceManager.GetString("ErrorUnknown")}.ToList()}
            Catch ex As Exception
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", IndigoSessionValues)
                Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = {IndigoManagementExceptions.GetExceptionDetails(ex)}.ToList()}
            End Try
        End Using
    End Function


    Private Function convertPartialPaymentsToXml(PartialPaymentsC As PartialPaymentsC) As String
        Dim builder As StringBuilder = New StringBuilder()
        builder.Append("<PartialPaymentsC>")
        builder.Append("<CustomerId>" & PartialPaymentsC.CustomerId & "</CustomerId>")
        builder.Append("<DocumentDate>" & PartialPaymentsC.DocumentDate.ToString("dd/MM/yyyy hh:mm:ss") & "</DocumentDate>")
        builder.Append("<State>" & PartialPaymentsC.State & "</State>")
        builder.Append("<Comments>" & PartialPaymentsC.Comment.Replace("<", " ").Replace("&", " ") & "</Comments>")
        For Each detail As PartialPaymentsD In PartialPaymentsC.PartialPaymentsD
            builder.Append("<PartialPaymentsD>")
            builder.Append("<PortfolioGlosaId>" & detail.PortfolioGlosaId & "</PortfolioGlosaId>")
            builder.Append("<InvoiceNumber>" & detail.InvoiceNumber & "</InvoiceNumber>")
            builder.Append("<InvoiceDate>" & detail.InvoiceDate.ToString("dd/MM/yyyy") & "</InvoiceDate>")
            builder.Append("<RadicatedNumber>" & detail.RadicatedNumber & "</RadicatedNumber>")
            builder.Append("<RadicatedDate>" & detail.RadicatedDate & "</RadicatedDate>")
            builder.Append("<PatientCode>" & detail.PatientCode & "</PatientCode>")
            builder.Append("<PatientName>" & detail.PatientName & "</PatientName>")
            builder.Append("<ContractCode>" & detail.ContractCode & "</ContractCode>")
            builder.Append("<ValuePendingConciliation>" & detail.ValuePendingConciliation.ToString.Replace(",", ".") & "</ValuePendingConciliation>")
            builder.Append("<ValuePayments>" & detail.ValuePayments.ToString.Replace(",", ".") & "</ValuePayments>")
            builder.Append("</PartialPaymentsD>")
        Next
        builder.Append("</PartialPaymentsC>")
        Return builder.ToString()
    End Function

    ''' <summary>
    ''' metodo para generar la secuencia del reconocimiento
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateSequence() As ActionResult

        sequence = _budgetSequenceRepository.GetSequenseByIdForm("213")

        If sequence IsNot Nothing AndAlso sequence.Id > 0 AndAlso sequence.Sequential Then
            Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(sequence.BudgetSequenceDetail(0).Sequense.Pattern, sequence.BudgetSequenceDetail(0).Next)
            If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                Return New ActionResult With {.Message = res, .StateResult = True}
            Else
                Return New ActionResult With {.StateResult = False, .Message = "La secuencia para el reconocimiento alcanzo su valor maximo"}
            End If

        Else
            Return New ActionResult With {.StateResult = False, .Message = "La secuencia para reconocimentos no esta parametrizada o no es secuencial"}
        End If
    End Function

    Private Function ReverseCashReceipt(cashReceiptId As Integer, audit As AuditMessage, treasuryNote As TreasuryNote) As ActionResult(Of String) Implements ICashReceiptsAdminService.ReverseCashReceipt
        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                CType(_cashReceiptsRepository.UnitWork, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
                Dim result As Object
                If treasuryNote.NoteType <> 7 Then
                    result = _cashReceiptsRepository.ReverseCashReceiptSP(cashReceiptId, audit.CodeUser, treasuryNote.Code).ToList().ElementAt(0)
                Else
                    Dim Resultxml = GenXmlDevolutionCashReceipt(cashReceiptId, audit.CodeUser, treasuryNote)
                    result = _cashReceiptsRepository.SP_DevolutionCashReceipt(Resultxml.ObjectEmbbeded).ToList().ElementAt(0)
                End If
                If result.CodeMessage = "999" Then
                    Return New ActionResult(Of String) With {.StateResult = False, .Message = result.Message}
                Else
                    transaction.Complete()
                    Return New ActionResult(Of String) With {.StateResult = True, .ObjectEmbbeded = result.IdJournalVoucher, .Message = result.Message}
                End If
            Catch ex As OptimisticConcurrencyException
                transaction.Dispose()
                Return New ActionResult(Of String) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorConcurrence")}
            Catch ex As DbEntityValidationException
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of String) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
            Catch ex As Exception
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of String) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' metodo privado para generar el comprobante contable
    ''' </summary>
    ''' <param name="cashReceipt"></param>
    ''' <param name="cashRegister"></param>
    ''' <param name="bankAccount"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function generateJournalVoucher(settingTreasury As SettingsTreasury, cashReceipt As CashReceipts, cashRegister As CashRegisters, bankAccount As EntityBankAccounts, action As eActionSave, audit As AuditMessage, Optional treasuryNote As TreasuryNote = Nothing) As ActionResult(Of String)

        If settingTreasury.Id > 0 Then
            Dim accounting As New Domain.Entities.JournalVouchers()
            With accounting
                .IdJournalVoucher = IIf(action = eActionSave.Confirm, settingTreasury.JournalVoucherTypeCashReceipts, settingTreasury.JournalVoucherTypeTreasuryNotes)
                .Status = 2
                .Detail = "Recibo de Caja Nº" & cashReceipt.Code & " " & cashReceipt.Detail
                .BookCurrencyId = cashReceipt.CurrencyId
                If action = eActionSave.Confirm Then
                    .VoucherDate = cashReceipt.DocumentDate
                    .EntityCode = cashReceipt.Code
                    .EntityId = cashReceipt.Id
                    .EntityName = GetType(CashReceipts).Name
                Else
                    .VoucherDate = treasuryNote.NoteDate
                    .EntityCode = treasuryNote.Code
                    .EntityId = treasuryNote.Id
                    .EntityName = GetType(TreasuryNote).Name
                End If
                .IsClosedYear = False
                .CreationDate = DateTime.Now
                .CreationUser = audit.CodeUser
                .ModificationDate = DateTime.Now
                .ModificationUser = audit.CodeUser
                .ConfirmationDate = DateTime.Now
                .ConfirmationUser = audit.CodeUser
                For Each detail In cashReceipt.CashReceiptDetails
                    Dim accountDetail As New JournalVoucherDetails
                    With accountDetail
                        .IdMainAccount = detail.IdMainAccount
                        .IdThirdParty = detail.IdThirdParty
                        .IdCostCenter = detail.IdCostCenter
                        .Detail = detail.Detail
                        .DebitValue = IIf(action = eActionSave.Reverse, IIf(detail.Nature = 1, 0, detail.Value), IIf(detail.Nature = 1, detail.Value, 0))
                        .CreditValue = IIf(action = eActionSave.Reverse, IIf(detail.Nature = 1, detail.Value, 0), IIf(detail.Nature = 1, 0, detail.Value))

                        'If detail.Nature = 1 Then
                        '    .DebitValue = detail.Value
                        'Else
                        '    .CreditValue = detail.Value
                        'End If
                        .IdRetention = detail.IdRetentionConcept
                        .RetentionRate = detail.PercentageRetention
                    End With
                    accounting.JournalVoucherDetails.Add(accountDetail)
                Next

                For Each detail In cashReceipt.PaymentMethods
                    Dim accountDetail As New JournalVoucherDetails
                    With accountDetail
                        If cashReceipt.CollectType = 1 Then
                            .IdMainAccount = cashRegister.IdMainAccount
                            Dim account = _RepositoryMainAccounts.GetAccountById(cashRegister.IdMainAccount, False)
                            If account.HandlesCostCenter Then
                                .IdCostCenter = cashRegister.IdCostCenter
                            End If
                            If settingTreasury.GetThirdPartyCashRegister = 1 Then
                                .IdThirdParty = cashRegister.ThirdPartyId
                            Else
                                .IdThirdParty = cashReceipt.IdThirdParty
                            End If
                        Else
                            .IdMainAccount = bankAccount.IdMainAccount
                            Dim account = _RepositoryMainAccounts.GetAccountById(bankAccount.IdMainAccount, False)
                            If account.HandlesCostCenter Then
                                .IdCostCenter = bankAccount.IdCostCenter
                            End If
                            If settingTreasury.GetThirdPartyBank = 1 Then
                                .IdThirdParty = bankAccount.ThirdPartyId
                            Else
                                .IdThirdParty = cashReceipt.IdThirdParty
                            End If
                        End If
                        '.IdThirdParty = cashReceipt.IdThirdParty
                        .DebitValue = IIf(action = eActionSave.Confirm, detail.Value, 0)
                        .CreditValue = IIf(action = eActionSave.Confirm, 0, detail.Value)
                        '.DebitValue = detail.Value
                    End With
                    accounting.JournalVoucherDetails.Add(accountDetail)
                Next
                Dim resultAccounting = _accountingAdminService.SaveAccountingDocument(accounting, audit)
                If resultAccounting.StateResult = False Then
                    Return New ActionResult(Of String) With {.StateResult = False, .Message = resultAccounting.Message}
                Else
                    Return New ActionResult(Of String) With {.StateResult = True, .ObjectEmbbeded = resultAccounting.ObjectEmbbeded.Consecutive, .MessageResult = {settingTreasury.JournalVoucherTypeTreasuryNotes.ToString()}.ToList()}
                End If
            End With
        Else
            Return New ActionResult(Of String) With {.StateResult = False, .Message = ResourceManager.GetString("SettingTreasuryNotExist", "Treasury")}
        End If
    End Function

    ''' <summary>
    ''' metodo para obtener los detalles del los recibos de caja
    ''' </summary>
    ''' <param name="idCashReceipt"></param>
    ''' <returns></returns>
    ''' <exception cref="System.InvalidOperationException">id</exception>
    Public Function GetCashReceiptDetailsByIdCashReceipt(idCashReceipt As Integer) As List(Of CashReceiptDetails) Implements ICashReceiptsAdminService.GetCashReceiptDetailsByIdCashReceipt
        If idCashReceipt = 0 Then
            Throw New InvalidOperationException("id")
        End If
        Try
            Return _cashReceiptsRepository.GetCashReceiptsDetailByIdCashReceipt(idCashReceipt)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' metodo para obtener los metodos de pago del recibo de caja
    ''' </summary>
    ''' <param name="idCashReceipt"></param>
    ''' <returns></returns>
    ''' <exception cref="System.InvalidOperationException">id</exception>
    Public Function GetPaymentMethodsByIdCashReceip(idCashReceipt As Integer) As List(Of PaymentMethods) Implements ICashReceiptsAdminService.GetPaymentMethodsByIdCashReceip
        If idCashReceipt = 0 Then
            Throw New InvalidOperationException("id")
        End If
        Try
            Return _cashReceiptsRepository.GetPaymentMethodsByIdCashReceipt(idCashReceipt)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function ValidateCostcenterPaymentMethod(PaymentMethods As PaymentMethods, operatingUnitId As Integer) As ActionResult Implements ICashReceiptsAdminService.ValidateCostcenterPaymentMethod
        Try
            Return _cashReceiptsRepository.ValidateCostcenterPaymentMethod(PaymentMethods, operatingUnitId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function
#End Region

#Region "xml"
    ''' <summary>
    ''' se genera el xml, para el sp de devolucion de recibo de caja
    ''' </summary>
    ''' <param name="CashReceiptId"></param>
    ''' <param name="CodeUser"></param>
    ''' <param name="TreasuryNote"></param>
    ''' <returns></returns>
    Private Function GenXmlDevolutionCashReceipt(CashReceiptId As Integer, CodeUser As String, TreasuryNote As TreasuryNote) As ActionResult(Of String)
        Try
            If CashReceiptId = 0 OrElse String.IsNullOrEmpty(CodeUser) OrElse TreasuryNote Is Nothing Then
                Throw New Exception("No existen datos suficiente para generar el xml")
            End If
            Dim xmlDocuments As New StringBuilder()
            xmlDocuments.AppendLine("<DevolutionCashReceipt>")
            xmlDocuments.AppendLine($"<CashReceiptId>{CashReceiptId}</CashReceiptId>")
            xmlDocuments.AppendLine($"<CashRegisterId>{TreasuryNote.CashRegisterId}</CashRegisterId>")
            xmlDocuments.AppendLine($"<User>{CodeUser}</User>")
            xmlDocuments.AppendLine($"<TreasuryNoteCode>{TreasuryNote.Code}</TreasuryNoteCode>")
            xmlDocuments.AppendLine($"<NoteType>{TreasuryNote.NoteType}</NoteType>")
            xmlDocuments.AppendLine($"<IdBankAccount>{TreasuryNote.EntityBankAccountId}</IdBankAccount>")
            xmlDocuments.AppendLine("</DevolutionCashReceipt>")
            Return New ActionResult(Of String) With {.StateResult = True, .ObjectEmbbeded = xmlDocuments.ToString(), .Message = "Xml generado con éxito"}
        Catch ex As Exception
            Return New ActionResult(Of String) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function
#End Region

    ''' <summary>
    ''' metodo para obtener las facturas del tercero por los datos que se pegaron en la rejilla
    ''' </summary>
    ''' <param name="data">informacion que se pego en la rejilla</param>
    ''' <param name="idThirdPaty"></param>
    ''' <param name="idMainAccount"></param>
    ''' <param name="idCostCenter"></param>
    ''' <returns></returns>
    Public Function SetBillsCashReceipts(data As List(Of List(Of String)), idThirdPaty As Integer, idMainAccount As Integer, idCostCenter As Integer, idOperatingUnit As Integer) As ActionResult(Of List(Of AccountReceivable)) Implements ICashReceiptsAdminService.SetBillsCashReceipts
        Try
            Return treasuryService.SetBillsCashReceipts(data, idThirdPaty, idMainAccount, idCostCenter, idOperatingUnit)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Enum eActionSave
        Confirm
        Reverse
    End Enum

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _budgetSequenceRepository = Nothing
            _budgetRepository = Nothing
            _categoryRepository = Nothing
            treasuryService = Nothing
            _cashReceiptsRepository = Nothing
            _cashRegisterRepository = Nothing
            _cashRegisterAdminService = Nothing
            _entityBankAccountRepository = Nothing
            _entityBankAccountAdminService = Nothing
            _sequenceRepository = Nothing
            _settingsTreasuryRepository = Nothing
            _accountingAdminService = Nothing
            _advancePaymentAdminService = Nothing
            _advancePaymentRepository = Nothing
            _treasuryControlAdminService = Nothing
            _RepositoryCloseMont = Nothing
            _RepositoryMainAccounts = Nothing
            _accountReceivableRepository = Nothing
            _documentTypeRepository = Nothing
            _portfolioGlosadaRepository = Nothing
            _partialPaymentsCAdminService = Nothing
            _portfolioAdvanceRepository = Nothing
            _cashReceiptsConcepts = Nothing
            _recognitionAdminService = Nothing
            _ReclassificationRepository = Nothing
            _accountPayableRepository = Nothing
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' TODO: reemplace Finalize() solo si el anterior Dispose(disposing As Boolean) tiene código para liberar recursos no administrados.
    'Protected Overrides Sub Finalize()
    '    ' No cambie este código. Coloque el código de limpieza en el anterior Dispose(disposing As Boolean).
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en el anterior Dispose(disposing As Boolean).
        Dispose(True)
        ' TODO: quite la marca de comentario de la siguiente línea si Finalize() se ha reemplazado antes.
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
