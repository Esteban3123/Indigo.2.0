'***********************************************************************
' Assembly         : Application.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 31-03-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
Imports System.Transactions
Imports Domain.Entities.Service
Imports Domain.Payroll
Imports System.Text

Public Class OpeningBalanceAdminService
    Implements IOpeningBalanceAdminService

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _openingBalanceRepository As IOpeningBalanceRepository
    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As ISequensePaymentsDRepository
    ''' <summary>
    ''' Repositorio de secuencias numericas cabecera
    ''' </summary>
    ''' <remarks></remarks>
    Private _secuenseCRepository As ISequensePaymentsCRepository

    ''' <summary>
    ''' Repositorio de cuentas por pagar
    ''' </summary>
    ''' <remarks></remarks>
    Private _accountPayableRepository As IAccountPayableRepository

    ''' <summary>
    ''' Interfaz de aplicacion de cuentas por pagar
    ''' </summary>
    ''' <remarks></remarks>
    Private _accountPayable As IAccountPayableAdminService

    ''' <summary>
    ''' Interfaz de aplicaicon de anticipo
    ''' </summary>
    ''' <remarks></remarks>
    Private _moneyAdvance As IMoneyAdvanceAdminService

    Private _moneyAdvanceRepository As IMoneyAdvanceRepository

    ''' <summary>
    ''' Variable tipo repositorio para proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Private _supplierRepository As Domain.Maintenance.ISupplierRepository
    ''' <summary>
    ''' Variable tipo repositorio para lineas de distribucion
    ''' </summary>
    ''' <remarks></remarks>
    Private _distributionLinesRepository As IDistributionLinesRepository
    ''' <summary>
    ''' Variable tipo repositorio para la asociacion de proveedor con lineas de distribucion
    ''' </summary>
    ''' <remarks></remarks>
    Private _supplierDistributionLines As ISuppliersDistributionLinesRepository
    ''' <summary>
    ''' Variable tipo repositorio para la cuenta contable
    ''' </summary>
    ''' <remarks></remarks>
    Private _pucRepository As IPUCRepository
    ''' <summary>
    ''' Repositorio de centro de costo
    ''' </summary>
    ''' <remarks></remarks>
    Private _costCenterRepository As ICostCenterRepository

    Private _currencyRepository As ICurrencyRepository

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal openingBalanceRepository As IOpeningBalanceRepository, ByVal secuenseDRepository As ISequensePaymentsDRepository, ByVal accountPayableRepository As IAccountPayableRepository,
                   ByVal accountPayableAdminService As IAccountPayableAdminService, ByVal moneyAdvanceAdminService As IMoneyAdvanceAdminService,
                   ByVal supplierRepository As Domain.Maintenance.ISupplierRepository, ByVal distributionLinesRepository As IDistributionLinesRepository, ByVal supplierDistributionLines As ISuppliersDistributionLinesRepository,
                   ByVal pucRepository As IPUCRepository, ByVal costCenterRepository As ICostCenterRepository, ByVal secuenseCRepository As ISequensePaymentsCRepository,
                   moneyAdvanceRepository As IMoneyAdvanceRepository, currencyRepository As ICurrencyRepository)
        If openingBalanceRepository Is Nothing Then
            Throw New ArgumentNullException("openingBalanceRepository Vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        If accountPayableRepository Is Nothing Then
            Throw New ArgumentNullException("accountPayableRepository")
        End If
        If moneyAdvanceAdminService Is Nothing Then
            Throw New ArgumentNullException("moneyAdvanceAdminService")
        End If
        If secuenseCRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseCRepository")
        End If
        _moneyAdvanceRepository = moneyAdvanceRepository
        _openingBalanceRepository = openingBalanceRepository
        _secuenseDRepository = secuenseDRepository
        _accountPayableRepository = accountPayableRepository
        _accountPayable = accountPayableAdminService
        _moneyAdvance = moneyAdvanceAdminService
        _supplierRepository = supplierRepository
        _distributionLinesRepository = distributionLinesRepository
        _supplierDistributionLines = supplierDistributionLines
        _pucRepository = pucRepository
        _costCenterRepository = costCenterRepository
        _secuenseCRepository = secuenseCRepository
        _currencyRepository = currencyRepository
    End Sub

    ''' <summary>
    ''' Elimina un saldo inicial
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteOpeningBalance(openingBalance As InitialBalance, audit As AuditMessage) As ActionResult Implements IOpeningBalanceAdminService.DeleteOpeningBalance
        If openingBalance Is Nothing Then
            Throw New ArgumentNullException("openingBalance")
        End If
        Dim unitOfWork As IUnitWork = Me._openingBalanceRepository.UnitWork
        Try
            openingBalance.StartTracking()
            While openingBalance.InitialBalanceAdvance.Count > 0
                openingBalance.InitialBalanceAdvance.Item(0).MarkAsDeleted()
            End While
            While openingBalance.InitialBalanceAccountPayable.Count > 0
                openingBalance.InitialBalanceAccountPayable.Item(0).MarkAsDeleted()
            End While
            openingBalance.MarkAsDeleted()
            Me._openingBalanceRepository.SaveEntity(openingBalance)
            unitOfWork.Commit()
            'Auditoria básica
            IndigoAuditBasic.Execute(GetType(InitialBalance).Name, audit.Functional, openingBalance.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, Infrastructure.CrossCutting.Base.ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            'Ausitoria avanzada
            Dim auditObject As New IndigoAuditSimpleEntity(Of InitialBalance)(openingBalance, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
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
    ''' Obtiene un saldo inicial
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetOpeningBalance(code As String, audit As AuditMessage) As ActionResult(Of InitialBalance) Implements IOpeningBalanceAdminService.GetOpeningBalance
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim openingBalance As InitialBalance = Me._openingBalanceRepository.GetOpeningBalance(code.Trim())
            If openingBalance IsNot Nothing AndAlso openingBalance.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of InitialBalance)(openingBalance, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of InitialBalance) With {.StateResult = True, .ObjectEmbbeded = openingBalance}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of InitialBalance) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza un saldo inicial
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveOpeningBalance(openingBalance As InitialBalance, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of InitialBalance) Implements IOpeningBalanceAdminService.SaveOpeningBalance
        If openingBalance Is Nothing Then
            Throw New ArgumentNullException("openingBalance")
        End If
        Dim unitOfWorkAccountPayable As IUnitWork = _accountPayableRepository.UnitWork
        Dim unitOfWork As IUnitWork = Me._openingBalanceRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Dim seq As PaymentsSecuenceDetail = Nothing

            If openingBalance.Code Is Nothing OrElse openingBalance.Code.Trim().Equals(String.Empty) Then
                'seq = Me._secuenseDRepository.GetSequenseDetailUpdatedById(idSequense)
                seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PaymentsSecuence.Sequential Then
                    Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                    If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                        openingBalance.Code = res
                        seq.Next += 1
                        Me._secuenseDRepository.SaveEntity(seq)
                    Else
                        Return New ActionResult(Of InitialBalance) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                    End If
                Else
                    Return New ActionResult(Of InitialBalance) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                End If
            End If

            Dim auxOpeningBalance As InitialBalance = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of InitialBalance)
            Dim status As Integer

            If openingBalance.ChangeTracker.State = ObjectState.Added Then
                openingBalance.CreationUser = audit.CodeUser
                openingBalance.CreationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            ElseIf openingBalance.ChangeTracker.State = ObjectState.Modified Then
                auxOpeningBalance = openingBalance.OriginalValue
                If openingBalance.Status = 1 Then
                    openingBalance.ModificationUser = audit.CodeUser
                    openingBalance.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If
                If openingBalance.Status = 2 Then
                    openingBalance.ModificationUser = audit.CodeUser
                    openingBalance.ModificationDate = DateTime.Now
                    openingBalance.ConfirmationUser = audit.CodeUser
                    openingBalance.ConfirmationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Confirm
                End If
                If openingBalance.Status = 3 Then
                    openingBalance.ModificationUser = audit.CodeUser
                    openingBalance.ModificationDate = DateTime.Now
                    openingBalance.AnnulmentUser = audit.CodeUser
                    openingBalance.AnnulmentDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Annular
                End If
            End If

            Me._openingBalanceRepository.SaveEntity(openingBalance)
            unitOfWork.Commit()
            sequenseUnitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of InitialBalance)(openingBalance, audit, status, auxOpeningBalance)
            auditProcess.Execute()

            'Se marca la entidad como sin cambios
            openingBalance.MarkAsUnchanged()

            Return New ActionResult(Of InitialBalance) With {.StateResult = True, .ObjectEmbbeded = openingBalance}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of InitialBalance) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of InitialBalance) With {.StateResult = False, .MessageResult = {IndigoManagementExceptions.GetExceptionDetails(ex)}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Cambia el estado del registro
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeState(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of InitialBalance) Implements IOpeningBalanceAdminService.ChangeState
        Dim openingBalance As InitialBalance = _openingBalanceRepository.GetOpeningBalance(code)
        openingBalance.Status = state
        Return SaveOpeningBalance(openingBalance, audit)
    End Function

    ''' <summary>
    ''' Guarda el saldo inicial
    ''' </summary>
    ''' <param name="openingBalance"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConfirmOpeningBalance(openingBalance As InitialBalance, modeSaveAndConfirm As Boolean, audit As AuditMessage, Optional idSequense As Long = 0, Optional _idOperativeUnit As Int32 = 0) As ActionResult(Of InitialBalance) Implements IOpeningBalanceAdminService.ConfirmOpeningBalance
        If openingBalance Is Nothing Then
            Throw New ArgumentNullException("openingBalance")
        End If
        If openingBalance.InitialBalanceAdvance Is Nothing Then
            Throw New ArgumentNullException("openingBalance.InitialBalanceAdvance")
        End If
        If openingBalance.InitialBalanceAccountPayable Is Nothing Then
            Throw New ArgumentNullException("openingBalance.InitialBalanceAccountPayable")
        End If
        Dim listConsecutives As New StringBuilder

        Try
            Using transaction As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                Dim resultInitialBalance As ActionResult(Of InitialBalance)
                If modeSaveAndConfirm Then
                    resultInitialBalance = SaveOpeningBalance(openingBalance, audit, idSequense)
                    If resultInitialBalance.StateResult = False Then
                        transaction.Dispose()
                        Return New ActionResult(Of InitialBalance) With {.StateResult = False, .MessageResult = resultInitialBalance.MessageResult}
                    End If
                    openingBalance = resultInitialBalance.ObjectEmbbeded
                End If

                'Inserto el anticipo 
                Dim sequenseCAd = _secuenseCRepository.GetSequenseByIdForm("2814") 'Anticipos de Cuentas por Pagar Saldo Inicial 
                If sequenseCAd Is Nothing OrElse sequenseCAd.Id = 0 Then
                    transaction.Dispose()
                    Return New ActionResult(Of InitialBalance) With {.StateResult = False, .MessageResult = {"No existe secuencia numérica para Anticipos."}.ToList}
                End If
                Dim sequenceNext As Long = 0
                Dim seqPayments As PaymentsSecuenceDetail = Nothing

                If openingBalance.InitialBalanceAdvance IsNot Nothing AndAlso openingBalance.InitialBalanceAdvance.Any() Then
                    If sequenseCAd.Scope = "O" Then 'Si la secuencia es organizacional
                        seqPayments = (From x In sequenseCAd.PaymentsSecuenceDetail Select x).FirstOrDefault()
                    Else 'Si la secuencia es por unidad operativa
                        'Se valida que haya registros de secuencia con la unidad operativa que viene desde presentacion
                        If (From x In sequenseCAd.PaymentsSecuenceDetail Where x.IdOperatingUnit = _idOperativeUnit Select x).Count = 0 Then
                            transaction.Dispose()
                            Return New ActionResult(Of InitialBalance) With {.StateResult = False, .MessageResult = {"No existe secuencia numérica para Anticipos de CxP con la unidad operativa escogida"}.ToList}
                        End If
                        seqPayments = (From x In sequenseCAd.PaymentsSecuenceDetail Where x.IdOperatingUnit = _idOperativeUnit Select x).FirstOrDefault()
                    End If
                    If seqPayments IsNot Nothing AndAlso seqPayments.Id > 0 AndAlso seqPayments.PaymentsSecuence.Sequential Then
                        sequenceNext = seqPayments.Next
                        seqPayments.Next += openingBalance.InitialBalanceAdvance.Count
                        Me._secuenseDRepository.SaveEntity(seqPayments)
                    Else
                        Return New ActionResult(Of InitialBalance) With {.StateResult = False, .MessageResult = {"No existe secuencia numérica para Anticipos de CxP con la unidad operativa escogida"}.ToList()}
                    End If
                    Dim advancePaymentsList As New List(Of AdvancePayments)()
                    For Each itemAdvance As InitialBalanceAdvance In openingBalance.InitialBalanceAdvance
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seqPayments.Sequense.Pattern, sequenceNext)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            Dim advancePayments As New AdvancePayments
                            With advancePayments
                                .Code = res
                                .IdSupplier = itemAdvance.SupplierId
                                .IdAccount = itemAdvance.MainAccountId
                                .IdThirdParty = itemAdvance.ThirdPartyId
                                .IdCostCenter = itemAdvance.CostCenterId
                                .Value = itemAdvance.Value
                                .Balance = itemAdvance.Value
                                .DocumentDate = itemAdvance.AdvancePaymentsDate
                                .Status = 2
                                .CreationUser = audit.CodeUser
                                .CreationDate = DateTime.Now
                            End With
                            sequenceNext += 1
                            'resultAdvancePayments = _moneyAdvance.SaveMoneyAdvance(advancePayments, audit, sequenseCAd.Id)
                            'If resultAdvancePayments.StateResult = False Then
                            '    transaction.Dispose()
                            '    Return New ActionResult(Of InitialBalance) With {.StateResult = False, .MessageResult = resultAdvancePayments.MessageResult}
                            'End If
                            'itemAdvance.AdvancePaymentsId = resultAdvancePayments.ObjectEmbbeded.Id
                            If listConsecutives.Length = 0 Then
                                listConsecutives.Append(res)
                            Else
                                listConsecutives.Append(", " + res)
                            End If
                            advancePaymentsList.Add(advancePayments)
                            itemAdvance.AdvancePayments = advancePayments
                        Else
                            Return New ActionResult(Of InitialBalance) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                        End If
                    Next

                    '_moneyAdvanceRepository.SaveMoneyAdvanceList(advancePaymentsList)
                    '_moneyAdvanceRepository.UnitWork.Commit()

                    'resultAdvancePayments = _moneyAdvance.SaveMoneyAdvance(AdvancePayments, audit, sequenseCAd.Id)
                    'If resultAdvancePayments.StateResult = False Then
                    '    transaction.Dispose()
                    '    Return New ActionResult(Of InitialBalance) With {.StateResult = False, .MessageResult = resultAdvancePayments.MessageResult}
                    'End If
                    'Revisar que se actualice AdvancePaymentsId
                End If

                'Inserto la cxp
                Dim sequenseC = _secuenseCRepository.GetSequenseByIdForm(730)
                If sequenseC Is Nothing Then
                    transaction.Dispose()
                    Return New ActionResult(Of InitialBalance) With {.StateResult = False, .MessageResult = {"No existe secuencia numérica para CxP."}.ToList}
                End If

                If openingBalance.InitialBalanceAccountPayable IsNot Nothing AndAlso openingBalance.InitialBalanceAccountPayable.Any() Then
                    'Id de la secuencia numerica que se envia a los metodos de cxp
                    Dim PaymentSequenseDetailId As Integer = 0
                    'Se valida que la secuencia numerica no sea manual
                    If sequenseC.IsManual Then
                        transaction.Dispose()
                        Return New ActionResult(Of InitialBalance) With {.StateResult = False, .MessageResult = {"La secuencia numérica de CxP está parametrizada como manual y debe ser automática"}.ToList}
                    End If
                    seqPayments = Nothing
                    'Se obtiene el id de la secuencia numerica
                    If sequenseC.Scope = "O" Then 'Si la secuencia es organizacional
                        seqPayments = (From x In sequenseC.PaymentsSecuenceDetail Select x).FirstOrDefault()
                    Else 'Si la secuencia es por unidad operativa
                        'Se valida que haya registros de secuencia con la unidad operativa que viene desde presentacion
                        If (From x In sequenseC.PaymentsSecuenceDetail Where x.IdOperatingUnit = _idOperativeUnit Select x).Count = 0 Then
                            transaction.Dispose()
                            Return New ActionResult(Of InitialBalance) With {.StateResult = False, .MessageResult = {"No existe secuencia numérica para Cuentas por Pagar con la unidad operativa escogida"}.ToList}
                        End If
                        seqPayments = (From x In sequenseC.PaymentsSecuenceDetail Where x.IdOperatingUnit = _idOperativeUnit Select x).FirstOrDefault()
                    End If
                    sequenceNext = 0
                    If seqPayments IsNot Nothing AndAlso seqPayments.Id > 0 AndAlso seqPayments.PaymentsSecuence.Sequential Then
                        sequenceNext = seqPayments.Next
                        seqPayments.Next += openingBalance.InitialBalanceAccountPayable.Count
                        Me._secuenseDRepository.SaveEntity(seqPayments)
                    Else
                        Return New ActionResult(Of InitialBalance) With {.StateResult = False, .MessageResult = {"La secuencia para Cuentas por Pagar no esta parametrizada"}.ToList()}
                    End If

                    'Dim resultAccountPayable As ActionResult(Of Domain.Entities.AccountPayable)
                    For Each itemAccountPayable As InitialBalanceAccountPayable In openingBalance.InitialBalanceAccountPayable
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seqPayments.Sequense.Pattern, sequenceNext)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            Dim accountPayable As New AccountPayable
                            With accountPayable
                                .Code = res
                                .EntityId = openingBalance.Id
                                .EntityCode = openingBalance.Code
                                .EntityName = GetType(InitialBalance).Name
                                .IdSupplier = itemAccountPayable.SupplierId
                                .IdSuppliersDistributionLines = itemAccountPayable.SupplierDistributionLinesID
                                .IdThirdParty = itemAccountPayable.ThirdPartyId
                                .IdAccount = itemAccountPayable.MainAccountId
                                .IdCostCenter = itemAccountPayable.CostCenterId
                                .DocumentDate = openingBalance.DocumentDate
                                .Status = 2
                                .IdInitialBalance = openingBalance.Id
                                .InitialBalance = True
                                .PreviousBudget = False
                                .BillNumber = itemAccountPayable.BillNumber
                                .BillDate = itemAccountPayable.BillDate
                                .Term = itemAccountPayable.Term
                                .ExpirationDate = itemAccountPayable.ExpiredDate
                                .Shares = 1
                                .Value = itemAccountPayable.Value
                                .Balance = itemAccountPayable.Balance
                                .SupplierTypeId = itemAccountPayable.SupplierTypeId
                                .FilingUnitId = itemAccountPayable.FilingUnitId
                                .ServicePeriodDate = itemAccountPayable.ServicePeriodDate
                                .CurrencyId = itemAccountPayable.CurrencyId
                                .IdOperatingUnit = _idOperativeUnit
                                .CreationUser = audit.CodeUser
                                .CreationDate = DateTime.Now
                            End With
                            sequenceNext = sequenceNext + 1
                            Dim accountPayableShare As New AccountPayableShares
                            With accountPayableShare
                                .Share = 1
                                .DateExpires = itemAccountPayable.ExpiredDate
                                .InitialValue = itemAccountPayable.Value
                                .DebitValue = 0
                                .CreditValue = 0
                                .ValueTransfers = 0
                                .PaymentValue = 0
                                .Balance = itemAccountPayable.Balance
                            End With
                            accountPayable.AccountPayableShares.Add(accountPayableShare)
                            'resultAccountPayable = _accountPayable.SaveAccountPayable(accountPayable, audit, PaymentSequenseDetailId)
                            'If resultAccountPayable.StateResult = False Then
                            '    transaction.Dispose()
                            '    Return New ActionResult(Of InitialBalance) With {.StateResult = False, .MessageResult = resultAccountPayable.MessageResult}
                            'End If
                            'itemAccountPayable.AccountPayableId = resultAccountPayable.ObjectEmbbeded.Id
                            itemAccountPayable.AccountPayable = accountPayable
                            If listConsecutives.Length = 0 Then
                                listConsecutives.Append(res)
                            Else
                                listConsecutives.Append(", " + res)
                            End If
                        Else
                            Return New ActionResult(Of InitialBalance) With {.StateResult = False, .MessageResult = {"La secuencia para Cuentas por Pagar alcanzó su valor máximo"}.ToList()}
                        End If
                    Next
                End If

                openingBalance.Status = 2
                resultInitialBalance = SaveOpeningBalance(openingBalance, audit, idSequense)
                If resultInitialBalance.StateResult = False Then
                    transaction.Dispose()
                    Return New ActionResult(Of InitialBalance) With {.StateResult = False, .MessageResult = resultInitialBalance.MessageResult}
                End If
                transaction.Complete()
                Return New ActionResult(Of InitialBalance) With {.StateResult = True, .ObjectEmbbeded = openingBalance, .MessageResult = {listConsecutives.ToString}.ToList}
            End Using
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of InitialBalance) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try

    End Function

    ''' <summary>
    ''' Valida el anticipo del copyAndPaste
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetAdvanceInitialBalance(data As List(Of List(Of String))) As ActionResult(Of List(Of InitialBalanceAdvance)) Implements IOpeningBalanceAdminService.SetAdvanceInitialBalance
        Dim paymentService As New PaymentServices(_supplierRepository, _distributionLinesRepository, _supplierDistributionLines, _pucRepository, _costCenterRepository, _accountPayableRepository)
        Return paymentService.SetAdvanceInitialBalance(data)
    End Function

    ''' <summary>
    ''' Valida la factura del copyAndPaste
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetBillsInitialBalance(data As List(Of List(Of String))) As ActionResult(Of List(Of InitialBalanceAccountPayable)) Implements IOpeningBalanceAdminService.SetBillsInitialBalance
        Dim paymentService As New PaymentServices(
            _supplierRepository,
            _distributionLinesRepository,
            _supplierDistributionLines,
            _pucRepository,
            _costCenterRepository,
            _accountPayableRepository,
            _currencyRepository)
        Return paymentService.SetBillsInitialBalance(data)
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _accountPayable.Dispose()
                _moneyAdvance.Dispose()
            End If
            _moneyAdvanceRepository = Nothing
            _openingBalanceRepository = Nothing
            _secuenseDRepository = Nothing
            _accountPayableRepository = Nothing
            _accountPayable = Nothing
            _moneyAdvance = Nothing
            _supplierRepository = Nothing
            _distributionLinesRepository = Nothing
            _supplierDistributionLines = Nothing
            _pucRepository = Nothing
            _costCenterRepository = Nothing
            _secuenseCRepository = Nothing
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
