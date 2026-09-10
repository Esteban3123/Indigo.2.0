'***********************************************************************
' Assembly         : Application.Portfolio
' Author           : Carlos Ernesto Cordoba
' Created          : 17-09-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Data.Entity.Core
Imports System.Data.Entity.Validation
Imports System.Transactions
Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Domain.Entities.Service
Imports Domain.Payroll
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources

#End Region
Public Class PortfolioInitialBalanceAdminService
    Implements IPortfolioInitialBalanceAdminService


#Region "Field"
    Private _portfolioInitialBalanceRepository As IPortfolioInitialBalanceRepository
    Private _accountReceivableRepository As IAccountReceivableRepository
    Private _portfolioAdvanceRepository As IPortfolioAdvanceRepository
    Private _sequensePortfolioDRepository As ISequensePortfolioDRepository
    Private _sequensePortfolioCRepository As ISequensePortfolioCRepository
    Private _thirdPartyRepository As IThirdPartyRepository
    Private _customerRepository As ICustomerRepository
    Private _accountingRepository As IPUCRepository
    Private _costCenterRepository As ICostCenterRepository
    Private _portfolioNoteConceptRepository As IPortfolioNoteConceptRepository
    Private _documentTypeRepository As IDocumentTypeRepository
    Dim sequence As PortfolioSequence = Nothing
    Private _billingInvoiceCategoriesRepository As IBillingInvoiceCategories
#End Region

#Region "Builder"
    Public Sub New(portfolioInitialBalanceRepository As IPortfolioInitialBalanceRepository, accountReceivableRepository As IAccountReceivableRepository, portfolioAdvanceRepository As IPortfolioAdvanceRepository,
                   sequensePortfolioDRepository As ISequensePortfolioDRepository, sequensePortfolioCRepository As ISequensePortfolioCRepository, thirdPartyRepository As IThirdPartyRepository,
                   customerRepository As ICustomerRepository, accountingRepository As IPUCRepository, costCenterRepository As ICostCenterRepository,
                   portfolioNoteConceptRepository As IPortfolioNoteConceptRepository, documentTypeRepository As IDocumentTypeRepository,
                   billingInvoiceCategoriesRepository As IBillingInvoiceCategories)
        If portfolioInitialBalanceRepository Is Nothing Then
            Throw New ArgumentNullException("portfolioInitialBalanceRepository")
        End If
        If accountReceivableRepository Is Nothing Then
            Throw New ArgumentNullException("accountReceivableRepository")
        End If
        If portfolioAdvanceRepository Is Nothing Then
            Throw New ArgumentNullException("portfolioAdvanceRepository")
        End If
        If sequensePortfolioDRepository Is Nothing Then
            Throw New ArgumentNullException("sequensePortfolioDRepository")
        End If
        If sequensePortfolioCRepository Is Nothing Then
            Throw New ArgumentNullException("sequensePortfolioCRepository")
        End If
        _portfolioInitialBalanceRepository = portfolioInitialBalanceRepository
        _accountReceivableRepository = accountReceivableRepository
        _portfolioAdvanceRepository = portfolioAdvanceRepository
        _sequensePortfolioDRepository = sequensePortfolioDRepository
        _sequensePortfolioCRepository = sequensePortfolioCRepository
        _thirdPartyRepository = thirdPartyRepository
        _customerRepository = customerRepository
        _accountingRepository = accountingRepository
        _costCenterRepository = costCenterRepository
        _portfolioNoteConceptRepository = portfolioNoteConceptRepository
        _documentTypeRepository = documentTypeRepository
        _billingInvoiceCategoriesRepository = billingInvoiceCategoriesRepository
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' confirmar el saldo incial
    ''' </summary>
    ''' <param name="idPortfolioInitialBalance"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ConfirmPortfolioInitialBalance(idPortfolioInitialBalance As Integer, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult(Of String) Implements IPortfolioInitialBalanceAdminService.ConfirmPortfolioInitialBalance
        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TimeSpan.FromMinutes(30) 'TransactionManager.MaximumTimeout 
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim portfolioInitialBalanceUnitOfWork As IUnitWork = _portfolioInitialBalanceRepository.UnitWork
                Dim accountReceivableUnitOfWork As IUnitWork = _accountReceivableRepository.UnitWork
                Dim advanceUnitOfWork As IUnitWork = _portfolioAdvanceRepository.UnitWork
                Dim sequenceUnitOfWor As IUnitWork = _sequensePortfolioCRepository.UnitWork
                Dim auditProcess As IndigoAuditSimpleEntity(Of PortfolioInitialBalance)
                Dim porfolioInitialBalance = _portfolioInitialBalanceRepository.GetPortfolioInitialBalanceById(idPortfolioInitialBalance)
                Dim listInitialBalanceAccountReceivable = _portfolioInitialBalanceRepository.GetPortfolioInitialBalanceAccountReceivableByIdPortfolioInitialBalance(idPortfolioInitialBalance)
                Dim listInitialBalanceAdvance = _portfolioInitialBalanceRepository.GetPortfolioInitialBalanceAdvanceByIdPortfolioInitialBalance(idPortfolioInitialBalance)
                Dim InvoiceConsecutives As String = String.Empty
                Dim AdvanceConsecutives As String = String.Empty
                'obtengo el total de las facturas que no se le asignaron presupuesto
                Dim listBudgetAssigned = listInitialBalanceAccountReceivable.FindAll(Function(x) x.AffectBudget = False)
                If listBudgetAssigned.Count = 0 Then
                    porfolioInitialBalance.AllBudgetAssigned = True
                End If
                porfolioInitialBalance.AllBudgetAssigned = False
                For Each item In listInitialBalanceAccountReceivable
                    Dim accountReceivable As New AccountReceivable
                    With accountReceivable
                        'Usar el mismo numero de la factura como codigo
                        .Code = item.InvoiceNumber
                        If InvoiceConsecutives = String.Empty Then
                            InvoiceConsecutives = ResourceManager.GetString("Bill", "Portfolio") + .Code
                        Else
                            InvoiceConsecutives += "," + .Code
                        End If
                        .OperatingUnitId = porfolioInitialBalance.OperatingUnitId
                        .AccountReceivableType = item.AccountReceivableType
                        .ThirdPartyId = item.ThirdPartyId
                        .CustomerId = item.CustomerId
                        .InvoiceNumber = item.InvoiceNumber
                        .InvoiceCategoryId = item.InvoiceCategoryId
                        .AccountReceivableDate = item.AccountReceivableDate
                        .Term = item.Term
                        .ExpiredDate = item.ExpiredDate
                        .Observations = item.Observations
                        .PortfolioStatus = item.PortfolioStatus
                        .OpeningBalance = True
                        .PaymentAgreement = False
                        .RegistrationAdjusted = False
                        .MainAccountWithoutFilingId = item.AccountWithoutRadicateId
                        .NumberShares = item.NumberShares
                        .Value = item.Value
                        .Balance = item.Balance
                        .AffectBudget = item.AffectBudget
                        .BudgetId = item.BudgetId
                        .CostCenterId = item.CostCenterId
                        .AccountWithoutRadicateId = item.AccountWithoutRadicateId
                        .AccountRadicateId = item.AccountRadicateId
                        .AccountObjectionRemediedId = item.AccountObjectionRemediedId
                        .AccountConciliationId = item.AccountConciliationId
                        .AccountLegalCollectionId = item.AccountLegalCollectionId
                        .AccountDebtorOrder = item.AccountDebtorOrder
                        .AccountCreditorOrder = item.AccountCreditorOrder
                        .Status = 2
                        .CreationUser = audit.CodeUser
                        .CreationDate = DateTime.Now
                        .ConfirmationDate = DateTime.Now
                        .ConfirmationUser = audit.CodeUser
                        For Each itemAccounting In item.PortfolioInitialBalanceAccountReceivableAccounting
                            Dim accountReceivableAccounting As New AccountReceivableAccounting
                            accountReceivableAccounting.MainAccountId = itemAccounting.MainAccountId
                            accountReceivableAccounting.ThirdPartyId = itemAccounting.ThirdPartyId
                            accountReceivableAccounting.CostCenterId = itemAccounting.CostCenterId
                            accountReceivableAccounting.Value = itemAccounting.Value
                            accountReceivableAccounting.Balance = itemAccounting.Value
                            .AccountReceivableAccounting.Add(accountReceivableAccounting)
                        Next
                        For Each itemShare In item.PortfolioInitialBalanceAccountReceivableShare
                            Dim accountReceivableShare As New AccountReceivableShare
                            accountReceivableShare.Number = itemShare.Number
                            accountReceivableShare.ExpiredDate = itemShare.ExpiredDate
                            accountReceivableShare.Value = itemShare.Value
                            accountReceivableShare.Balance = itemShare.Value
                            .AccountReceivableShare.Add(accountReceivableShare)
                        Next
                    End With
                    _accountReceivableRepository.SaveEntity(accountReceivable)
                    'accountReceivableUnitOfWork.Commit()
                Next
                For Each item In listInitialBalanceAdvance
                    Dim portfolioAdvance As New PortfolioAdvance
                    With portfolioAdvance
                        Dim resultGenerateSecuence = GenerateSequence(False)
                        If resultGenerateSecuence.StateResult = False Then
                            transaction.Dispose()
                            Return New ActionResult(Of String) With {.StateResult = False, .Message = resultGenerateSecuence.Message}
                        End If
                        .Code = resultGenerateSecuence.Message
                        If AdvanceConsecutives = String.Empty Then
                            AdvanceConsecutives = ResourceManager.GetString("Advance", "Portfolio") + .Code
                        Else
                            AdvanceConsecutives += "," + .Code
                        End If
                        .ThirdPartyId = item.ThirdPartyId
                        .MainAccountId = item.MainAccountId
                        .CostCenterId = item.CostCenterId
                        .DocumentDate = item.DocumentDate
                        .CustomerId = item.CustomerId
                        .Value = item.Value
                        .OpeningBalance = True
                        .Balance = item.Value
                        .Observations = item.Observations
                        .Status = 2
                        .CreationDate = DateTime.Now
                        .CreationUser = audit.CodeUser
                        .ConfirmationDate = DateTime.Now
                        .ConfirmationUser = audit.CodeUser
                    End With
                    _portfolioAdvanceRepository.SaveEntity(portfolioAdvance)
                    advanceUnitOfWork.Commit()
                    sequence.PortfolioSequenceDetail(0).Next += 1
                    sequence.PortfolioSequenceDetail(0).MarkAsModified()
                    Me._sequensePortfolioCRepository.SaveEntity(sequence)
                    sequenceUnitOfWor.Commit()
                Next
                porfolioInitialBalance.Status = 2
                porfolioInitialBalance.ConfirmationDate = DateTime.Now
                porfolioInitialBalance.ConfirmationUser = audit.CodeUser
                porfolioInitialBalance.ModificationDate = DateTime.Now
                porfolioInitialBalance.ModificationUser = audit.CodeUser
                porfolioInitialBalance.MarkAsModified()
                _portfolioInitialBalanceRepository.SaveEntity(porfolioInitialBalance)
                portfolioInitialBalanceUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of PortfolioInitialBalance)(porfolioInitialBalance, audit, Infrastructure.CrossCutting.Audit.Actions.Confirm, porfolioInitialBalance.OriginalValue)
                auditProcess.Execute()
                transaction.Complete()
                If listInitialBalanceAccountReceivable.Count > 0 And listInitialBalanceAdvance.Count > 0 Then
                    Return New ActionResult(Of String) With {.StateResult = True, .Message = InvoiceConsecutives + " - " + AdvanceConsecutives, .ObjectEmbbeded = idPortfolioInitialBalance.ToString()}
                ElseIf listInitialBalanceAccountReceivable.Count > 0 Then
                    Return New ActionResult(Of String) With {.StateResult = True, .Message = InvoiceConsecutives, .ObjectEmbbeded = idPortfolioInitialBalance.ToString()}
                Else
                    Return New ActionResult(Of String) With {.StateResult = True, .Message = AdvanceConsecutives, .ObjectEmbbeded = idPortfolioInitialBalance.ToString()}
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

                Dim message = ResourceManager.GetString("ErrorUnknown")
                If ex.InnerException IsNot Nothing AndAlso Not String.IsNullOrEmpty(ex.InnerException.Message) Then
                    message = ex.InnerException.Message
                    If ex.InnerException.InnerException IsNot Nothing AndAlso Not String.IsNullOrEmpty(ex.InnerException.InnerException.Message) Then
                        message = ex.InnerException.InnerException.Message
                    End If
                End If

                Return New ActionResult(Of String) With {.StateResult = False, .Message = message}
            End Try
        End Using
    End Function

    Private Function GenerateSequence(account As Boolean) As ActionResult
        If account = True Then
            sequence = _sequensePortfolioCRepository.GetSequenseByIdForm("682")
        Else
            sequence = _sequensePortfolioCRepository.GetSequenseByIdForm("1507")
        End If
        If sequence IsNot Nothing AndAlso sequence.Id > 0 AndAlso sequence.Sequential AndAlso sequence.PortfolioSequenceDetail IsNot Nothing AndAlso sequence.PortfolioSequenceDetail.Count > 0 Then
            Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(sequence.PortfolioSequenceDetail(0).Sequense.Pattern, sequence.PortfolioSequenceDetail(0).Next)
            If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                Return New ActionResult With {.Message = res, .StateResult = True}
            Else
                If account = True Then
                    Return New ActionResult With {.StateResult = False, .Message = "La secuencia para facturas alcanzo su valor maximo"}
                Else
                    Return New ActionResult With {.StateResult = False, .Message = "La secuencia para anticipos alcanzo su valor maximo"}
                End If
            End If
        Else
            If account = True Then
                Return New ActionResult With {.StateResult = False, .Message = "La secuencia para facturas (682) no esta parametrizada o no es secuencial"}
            Else
                Return New ActionResult With {.StateResult = False, .Message = "La secuencia para anticipos (1507) no esta parametrizada o no es secuencial"}
            End If
        End If
    End Function

    ''' <summary>
    ''' metodo para obtener las facturas del saldo inicial por id del saldo inicial
    ''' </summary>
    ''' <param name="idPortfolioInitialBalance"></param>
    ''' <returns></returns>
    Public Function GetPortfolioInitialBalanceAccountReceivableByIdPortfolioInitialBalance(idPortfolioInitialBalance As Integer) As List(Of Domain.Entities.PortfolioInitialBalanceAccountReceivable) Implements IPortfolioInitialBalanceAdminService.GetPortfolioInitialBalanceAccountReceivableByIdPortfolioInitialBalance
        Try
            Return _portfolioInitialBalanceRepository.GetPortfolioInitialBalanceAccountReceivableByIdPortfolioInitialBalance(idPortfolioInitialBalance)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of PortfolioInitialBalanceAccountReceivable)
        End Try
    End Function

    ''' <summary>
    ''' metodo para obtener los anticipos del saldo inicial por id del saldo inicial
    ''' </summary>
    ''' <param name="idPortfolioInitialBalance"></param>
    ''' <returns></returns>
    Public Function GetPortfolioInitialBalanceAdvanceByIdPortfolioInitialBalance(idPortfolioInitialBalance As Integer) As List(Of Domain.Entities.PortfolioInitialBalanceAdvance) Implements IPortfolioInitialBalanceAdminService.GetPortfolioInitialBalanceAdvanceByIdPortfolioInitialBalance
        Try
            Return _portfolioInitialBalanceRepository.GetPortfolioInitialBalanceAdvanceByIdPortfolioInitialBalance(idPortfolioInitialBalance)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of PortfolioInitialBalanceAdvance)
        End Try
    End Function

    ''' <summary>
    ''' metodo para obtener el saldo inicial por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Code</exception>
    Public Function GetPortfolioInitialBalanceByCode(code As String, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Entities.PortfolioInitialBalance Implements IPortfolioInitialBalanceAdminService.GetPortfolioInitialBalanceByCode
        If code Is String.Empty Then
            Throw New ArgumentNullException("Code")
        End If
        Try
            Dim portfolioInitialBalance = _portfolioInitialBalanceRepository.GetPortfolioInitialBalanceByCode(code)
            If portfolioInitialBalance IsNot Nothing AndAlso portfolioInitialBalance.Id > 0 Then
                Dim auditProcess As New IndigoAuditSimpleEntity(Of PortfolioInitialBalance)(portfolioInitialBalance, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditProcess.Execute()
            End If
            Return portfolioInitialBalance
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New PortfolioInitialBalance()
        End Try
    End Function

    ''' <summary>
    ''' metodo para obtener el saldo inicial por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetPortfolioInitialBalanceById(id As Integer) As Domain.Entities.PortfolioInitialBalance Implements IPortfolioInitialBalanceAdminService.GetPortfolioInitialBalanceById
        Try
            Return _portfolioInitialBalanceRepository.GetPortfolioInitialBalanceById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New PortfolioInitialBalance()
        End Try
    End Function

    ''' <summary>
    ''' metodo para guardar y confirmar el saldo inicial
    ''' </summary>
    ''' <param name="PortfolioInitialBalance"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequence"></param>
    ''' <returns></returns>
    Public Function SaveAndConfirmPortfolioInitialBalance(PortfolioInitialBalance As Domain.Entities.PortfolioInitialBalance, audit As Infrastructure.CrossCutting.Base.AuditMessage, Optional idSequence As Integer = 0) As Domain.Base.Entities.ActionResult(Of String) Implements IPortfolioInitialBalanceAdminService.SaveAndConfirmPortfolioInitialBalance
        Dim message As String = String.Empty
        If PortfolioInitialBalance.Import Then
            'si se esta importando desde un excel
            Dim txSettings As New TransactionOptions()
            txSettings.Timeout = TimeSpan.FromMinutes(30) 'TransactionManager.MaximumTimeout
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
                Dim resultSave = SavePortfolioInitialBalanceImportFile(PortfolioInitialBalance, audit, idSequence)
                If resultSave.StateResult = True Then
                    Dim resultConfirm = ConfirmPortfolioInitialBalanceImportFile(resultSave.ObjectEmbbeded, audit)
                    If resultConfirm.StateResult = True Then
                        transaction.Complete()
                        message = String.Format(ResourceManager.GetString("SaveAndConfirmDocument", "Portfolio"), resultSave.ObjectEmbbeded.Code, resultConfirm.Message)
                        Return New ActionResult(Of String) With {.StateResult = True, .StateResultAux = True, .Message = message, .ObjectEmbbeded = resultConfirm.ObjectEmbbeded}
                    Else
                        transaction.Dispose()
                        message = String.Format(ResourceManager.GetString("NotSave", "Portfolio"), resultConfirm.Message)
                        Return New ActionResult(Of String) With {.StateResult = True, .StateResultAux = False, .Message = message}
                    End If
                Else
                    transaction.Dispose()
                    message = String.Format(ResourceManager.GetString("NotSave", "Portfolio"), resultSave.Message)
                    Return New ActionResult(Of String) With {.StateResult = False, .StateResultAux = False, .Message = message}
                End If
            End Using
        Else
            Dim resultSave = SavePortfolioInitialBalance(PortfolioInitialBalance, audit, idSequence)
            If resultSave.StateResult = True Then
                Dim resultConfirm = ConfirmPortfolioInitialBalance(resultSave.ObjectEmbbeded.Id, audit)
                If resultConfirm.StateResult = True Then
                    message = String.Format(ResourceManager.GetString("SaveAndConfirmDocument", "Portfolio"), resultSave.ObjectEmbbeded.Code, resultConfirm.Message)
                    Return New ActionResult(Of String) With {.StateResult = True, .StateResultAux = True, .Message = message}
                Else
                    message = String.Format(ResourceManager.GetString("SaveButNotConfirm", "Portfolio"), resultSave.ObjectEmbbeded.Code, resultConfirm.Message)
                    Return New ActionResult(Of String) With {.StateResult = True, .StateResultAux = False, .Message = message}
                End If
            Else
                message = String.Format(ResourceManager.GetString("NotSave", "Portfolio"), resultSave.Message)
                Return New ActionResult(Of String) With {.StateResult = False, .StateResultAux = False, .Message = message}
            End If
        End If
    End Function
    ''' <summary>
    ''' metodo para guardar el documento cuando esta importando desde excel
    ''' </summary>
    ''' <param name="PortfolioInitialBalance"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequence"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SavePortfolioInitialBalanceImportFile(PortfolioInitialBalance As Domain.Entities.PortfolioInitialBalance, audit As Infrastructure.CrossCutting.Base.AuditMessage, Optional idSequence As Integer = 0) As Domain.Base.Entities.ActionResult(Of Domain.Entities.PortfolioInitialBalance)

        Try
            Dim seq As PortfolioSequenceDetail = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of PortfolioInitialBalance)
            Dim status As Integer
            Dim auxPortfolioInitialBalance As PortfolioInitialBalance = Nothing
            Dim _portfolioInitialBalanceUnitOfWork As IUnitWork = _portfolioInitialBalanceRepository.UnitWork
            Dim sequenseUnitOfWork As IUnitWork = Me._sequensePortfolioDRepository.UnitWork
            If PortfolioInitialBalance.Code Is Nothing OrElse PortfolioInitialBalance.Code.Trim().Equals(String.Empty) Then
                seq = _sequensePortfolioDRepository.GetSequenseDById(idSequence)
                If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PortfolioSequence.Sequential Then
                    Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                    If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                        PortfolioInitialBalance.Code = res
                        seq.Next += 1
                        Me._sequensePortfolioDRepository.SaveEntity(seq)
                    Else

                        Return New ActionResult(Of PortfolioInitialBalance) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                    End If
                Else

                    Return New ActionResult(Of PortfolioInitialBalance) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                End If
            End If

            Select Case PortfolioInitialBalance.Status
                Case 1
                    If PortfolioInitialBalance.ChangeTracker.State = ObjectState.Added Then
                        PortfolioInitialBalance.CreationUser = audit.CodeUser
                        PortfolioInitialBalance.CreationDate = DateTime.Now
                        status = Infrastructure.CrossCutting.Audit.Actions.Insert
                    Else
                        PortfolioInitialBalance.ModificationDate = DateTime.Now
                        PortfolioInitialBalance.ModificationUser = audit.CodeUser
                        status = Infrastructure.CrossCutting.Audit.Actions.Update
                        auxPortfolioInitialBalance = PortfolioInitialBalance.OriginalValue
                    End If
                Case 3
                    PortfolioInitialBalance.ModificationDate = DateTime.Now
                    PortfolioInitialBalance.ModificationUser = audit.CodeUser
                    PortfolioInitialBalance.AnnulmentDate = DateTime.Now
                    PortfolioInitialBalance.AnnulmentUser = audit.CodeUser
                    auxPortfolioInitialBalance = PortfolioInitialBalance.OriginalValue
            End Select
            _portfolioInitialBalanceRepository.SaveEntity(PortfolioInitialBalance)
            _portfolioInitialBalanceUnitOfWork.Commit()
            sequenseUnitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of PortfolioInitialBalance)(PortfolioInitialBalance, audit, status, auxPortfolioInitialBalance)
            auditProcess.Execute()

            Return New ActionResult(Of PortfolioInitialBalance) With {.ObjectEmbbeded = PortfolioInitialBalance, .StateResult = True}

        Catch ex As OptimisticConcurrencyException

            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of PortfolioInitialBalance) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As DbEntityValidationException

            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of PortfolioInitialBalance) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        Catch ex As Exception

            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of PortfolioInitialBalance) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try


    End Function
    ''' <summary>
    ''' metodo para confirmar el documento cuando esta importando desde excel
    ''' </summary>
    ''' <param name="PortfolioInitialBalance"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConfirmPortfolioInitialBalanceImportFile(PortfolioInitialBalance As Domain.Entities.PortfolioInitialBalance, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult(Of String)
        Try
            Dim portfolioInitialBalanceUnitOfWork As IUnitWork = _portfolioInitialBalanceRepository.UnitWork
            Dim accountReceivableUnitOfWork As IUnitWork = _accountReceivableRepository.UnitWork
            Dim sequenceUnitOfWor As IUnitWork = _sequensePortfolioCRepository.UnitWork
            Dim auditProcess As IndigoAuditSimpleEntity(Of PortfolioInitialBalance)
            Dim porfolioInitialBalance = PortfolioInitialBalance
            Dim listAccountReceivableSave As New List(Of AccountReceivable)
            Dim InvoiceConsecutives As String = String.Empty


            For Each item In porfolioInitialBalance.PortfolioInitialBalanceAccountReceivable
                Dim accountReceivable As AccountReceivable = _accountReceivableRepository.GetAccountByInvoiceNumberAndAccountReceivableType(item.InvoiceNumber, {item.AccountReceivableType})
                If accountReceivable Is Nothing OrElse accountReceivable.Id = 0 Then
                    If listAccountReceivableSave.Any(Function(o) o.InvoiceNumber.Equals(item.InvoiceNumber) AndAlso o.AccountReceivableType = item.AccountReceivableType) Then
                        accountReceivable = listAccountReceivableSave.Find(Function(o) o.InvoiceNumber.Equals(item.InvoiceNumber) AndAlso o.AccountReceivableType = item.AccountReceivableType)
                    Else
                        accountReceivable = New AccountReceivable()
                        With accountReceivable
                            .Code = item.InvoiceNumber
                            If InvoiceConsecutives = String.Empty Then
                                InvoiceConsecutives = "Cuentas por Cobrar: " + .Code
                            Else
                                InvoiceConsecutives += "," + .Code
                            End If
                            .OperatingUnitId = porfolioInitialBalance.OperatingUnitId
                            .AccountReceivableType = item.AccountReceivableType
                            .ThirdPartyId = item.ThirdPartyId
                            .CustomerId = item.CustomerId
                            .InvoiceNumber = item.InvoiceNumber
                            .InvoiceCategoryId = item.InvoiceCategoryId
                            .AccountReceivableDate = item.AccountReceivableDate
                            .Term = item.Term
                            .ExpiredDate = item.ExpiredDate
                            .Observations = item.Observations
                            .PortfolioStatus = item.PortfolioStatus
                            .OpeningBalance = True
                            .PaymentAgreement = False
                            .RegistrationAdjusted = False
                            .MainAccountWithoutFilingId = item.AccountWithoutRadicateId
                            .NumberShares = item.NumberShares
                            .Value = item.Value
                            .Balance = item.Balance
                            .CostCenterId = item.CostCenterId
                            .AccountWithoutRadicateId = item.AccountWithoutRadicateId
                            .AccountRadicateId = item.AccountRadicateId
                            .AccountObjectionRemediedId = item.AccountObjectionRemediedId
                            .AccountConciliationId = item.AccountConciliationId
                            .AccountLegalCollectionId = item.AccountLegalCollectionId
                            .AccountDebtorOrder = item.AccountDebtorOrder
                            .AccountCreditorOrder = item.AccountCreditorOrder
                            .Status = 2
                            .CreationUser = audit.CodeUser
                            .CreationDate = DateTime.Now
                            .ConfirmationDate = DateTime.Now
                            .ConfirmationUser = audit.CodeUser
                        End With
                        For Each itemShare In item.PortfolioInitialBalanceAccountReceivableShare
                            Dim accountReceivableShare As New AccountReceivableShare
                            accountReceivableShare.Number = itemShare.Number
                            accountReceivableShare.ExpiredDate = itemShare.ExpiredDate
                            accountReceivableShare.Value = itemShare.PortfolioInitialBalanceAccountReceivable.Value
                            accountReceivableShare.Balance = itemShare.Value
                            accountReceivable.AccountReceivableShare.Add(accountReceivableShare)
                        Next
                    End If
                End If
                For Each itemAccounting In item.PortfolioInitialBalanceAccountReceivableAccounting
                    Dim accountReceivableAccounting As New AccountReceivableAccounting
                    accountReceivableAccounting.MainAccountId = itemAccounting.MainAccountId
                    accountReceivableAccounting.ThirdPartyId = itemAccounting.ThirdPartyId
                    accountReceivableAccounting.CostCenterId = itemAccounting.CostCenterId
                    accountReceivableAccounting.Value = itemAccounting.Value
                    accountReceivableAccounting.Balance = itemAccounting.PortfolioInitialBalanceAccountReceivable.Balance
                    accountReceivable.AccountReceivableAccounting.Add(accountReceivableAccounting)
                Next
                If accountReceivable.Id > 0 Then
                    accountReceivable.MarkAsModified()
                    _accountReceivableRepository.SaveEntity(accountReceivable)
                ElseIf Not listAccountReceivableSave.Any(Function(o) o.InvoiceNumber.Equals(item.InvoiceNumber) AndAlso o.AccountReceivableType = item.AccountReceivableType) Then
                    listAccountReceivableSave.Add(accountReceivable)
                End If
            Next
            _accountReceivableRepository.SaveListAccountReceivable(listAccountReceivableSave)
            porfolioInitialBalance.Status = 2
            porfolioInitialBalance.ConfirmationDate = DateTime.Now
            porfolioInitialBalance.ConfirmationUser = audit.CodeUser
            porfolioInitialBalance.ModificationDate = DateTime.Now
            porfolioInitialBalance.ModificationUser = audit.CodeUser
            porfolioInitialBalance.MarkAsModified()
            _portfolioInitialBalanceRepository.SaveEntity(porfolioInitialBalance)
            portfolioInitialBalanceUnitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of PortfolioInitialBalance)(porfolioInitialBalance, audit, Infrastructure.CrossCutting.Audit.Actions.Confirm, porfolioInitialBalance.OriginalValue)
            auditProcess.Execute()
            Return New ActionResult(Of String) With {.StateResult = True, .Message = InvoiceConsecutives, .ObjectEmbbeded = porfolioInitialBalance.Id.ToString()}
        Catch ex As OptimisticConcurrencyException
            Return New ActionResult(Of String) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As DbEntityValidationException
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of String) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of String) With {.StateResult = False, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try

    End Function

    ''' <summary>
    ''' guardar saldo inicial
    ''' </summary>
    ''' <param name="PortfolioInitialBalance"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequence"></param>
    ''' <returns></returns>
    Public Function SavePortfolioInitialBalance(PortfolioInitialBalance As Domain.Entities.PortfolioInitialBalance, audit As Infrastructure.CrossCutting.Base.AuditMessage, Optional idSequence As Integer = 0) As Domain.Base.Entities.ActionResult(Of Domain.Entities.PortfolioInitialBalance) Implements IPortfolioInitialBalanceAdminService.SavePortfolioInitialBalance
        If PortfolioInitialBalance Is Nothing Then
            Throw New ArgumentNullException("PortfolioInitialBalance")
        End If
        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TimeSpan.FromMinutes(30) 'TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim seq As PortfolioSequenceDetail = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of PortfolioInitialBalance)
                Dim status As Integer
                Dim auxPortfolioInitialBalance As PortfolioInitialBalance = Nothing
                Dim _portfolioInitialBalanceUnitOfWork As IUnitWork = _portfolioInitialBalanceRepository.UnitWork
                Dim sequenseUnitOfWork As IUnitWork = Me._sequensePortfolioDRepository.UnitWork
                If PortfolioInitialBalance.Code Is Nothing OrElse PortfolioInitialBalance.Code.Trim().Equals(String.Empty) Then
                    seq = _sequensePortfolioDRepository.GetSequenseDById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PortfolioSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            PortfolioInitialBalance.Code = res
                            seq.Next += 1
                            Me._sequensePortfolioDRepository.SaveEntity(seq)
                        Else
                            transaction.Dispose()
                            Return New ActionResult(Of PortfolioInitialBalance) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                        End If
                    Else
                        transaction.Dispose()
                        Return New ActionResult(Of PortfolioInitialBalance) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                    End If
                End If

                Select Case PortfolioInitialBalance.Status
                    Case 1
                        If PortfolioInitialBalance.ChangeTracker.State = ObjectState.Added Then
                            PortfolioInitialBalance.CreationUser = audit.CodeUser
                            PortfolioInitialBalance.CreationDate = DateTime.Now
                            status = Infrastructure.CrossCutting.Audit.Actions.Insert
                        Else
                            PortfolioInitialBalance.ModificationDate = DateTime.Now
                            PortfolioInitialBalance.ModificationUser = audit.CodeUser
                            status = Infrastructure.CrossCutting.Audit.Actions.Update
                            auxPortfolioInitialBalance = PortfolioInitialBalance.OriginalValue
                        End If
                    Case 3
                        PortfolioInitialBalance.ModificationDate = DateTime.Now
                        PortfolioInitialBalance.ModificationUser = audit.CodeUser
                        PortfolioInitialBalance.AnnulmentDate = DateTime.Now
                        PortfolioInitialBalance.AnnulmentUser = audit.CodeUser
                        auxPortfolioInitialBalance = PortfolioInitialBalance.OriginalValue
                End Select
                _portfolioInitialBalanceRepository.SaveEntity(PortfolioInitialBalance)
                _portfolioInitialBalanceUnitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of PortfolioInitialBalance)(PortfolioInitialBalance, audit, status, auxPortfolioInitialBalance)
                auditProcess.Execute()
                transaction.Complete()
                Return New ActionResult(Of PortfolioInitialBalance) With {.ObjectEmbbeded = PortfolioInitialBalance, .StateResult = True}

            Catch ex As OptimisticConcurrencyException
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of PortfolioInitialBalance) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorConcurrence")}
            Catch ex As Exception
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of PortfolioInitialBalance) With {.StateResult = False, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
            End Try
        End Using
    End Function

    Public Function SetAdvanceCopyPaste(data As List(Of List(Of String))) As ActionResult(Of List(Of PortfolioInitialBalanceAdvance)) Implements IPortfolioInitialBalanceAdminService.SetAdvanceCopyPaste
        Dim portfolioService As New PortfolioServices(_accountingRepository, _thirdPartyRepository, _customerRepository, _costCenterRepository)
        Return portfolioService.SetAdvancesCopyPaste(data)
    End Function

    Public Function SetBillsCopyPaste(data As List(Of List(Of String)), companyType As Integer) As ActionResult(Of List(Of PortfolioInitialBalanceAccountReceivable)) Implements IPortfolioInitialBalanceAdminService.SetBillsCopyPaste
        Dim portfolioService As New PortfolioServices(_accountReceivableRepository, _accountingRepository, _thirdPartyRepository, _customerRepository, _costCenterRepository, _portfolioNoteConceptRepository, _documentTypeRepository, _billingInvoiceCategoriesRepository)
        Return portfolioService.SetBillsCopyPaste(data, companyType)
    End Function
#End Region

    ''' <summary>
    ''' metodo para validar la carga del archivo para los saldo iniciales
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    Public Function ValidateFileBillsInitialBalance(data As List(Of ImportFileRow), companyType As Integer) As ActionResult(Of List(Of PortfolioInitialBalanceAccountReceivable)) Implements IPortfolioInitialBalanceAdminService.ValidateFileBillsInitialBalance
        Dim portfolioService As New PortfolioServices(_accountReceivableRepository, _accountingRepository, _thirdPartyRepository, _customerRepository, _costCenterRepository, _portfolioNoteConceptRepository, _documentTypeRepository, _billingInvoiceCategoriesRepository)
        Return portfolioService.SetBillsImportFile(data, companyType)
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _portfolioInitialBalanceRepository = Nothing
            _accountReceivableRepository = Nothing
            _portfolioAdvanceRepository = Nothing
            _sequensePortfolioDRepository = Nothing
            _sequensePortfolioCRepository = Nothing
            _thirdPartyRepository = Nothing
            _customerRepository = Nothing
            _accountingRepository = Nothing
            _costCenterRepository = Nothing
            _portfolioNoteConceptRepository = Nothing
            _documentTypeRepository = Nothing
            _billingInvoiceCategoriesRepository = Nothing
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
