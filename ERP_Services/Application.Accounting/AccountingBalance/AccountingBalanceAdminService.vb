'************************************************************
' Assembly         : Domain.Entities.Service
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 29-05-2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Base
Imports Application.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports System.Data.Entity.Core
Imports System.Data.Entity.Infrastructure
Imports System.Transactions
Imports Application.Accounting

#End Region

Public Class AccountingBalanceAdminService
    Implements IAccountingBalanceAdminService, Inject

    Private _RepositoryRead As IAccountingBalanceRepository
    Private _RepositorySave As IAccountingBalanceRepository
    Private _closeMonthRepository As ICloseMonthRepository


#Region "Builder"
    Public Sub New(ByVal Repository As IAccountingBalanceRepository, ByVal RepositorySave As IAccountingBalanceRepository, closeMonthRepository As ICloseMonthRepository)
        If Repository Is Nothing Then
            Throw New ArgumentNullException("Repositorio vacio")
        End If

        _RepositoryRead = Repository
        _RepositorySave = RepositorySave
        _closeMonthRepository = closeMonthRepository
    End Sub
#End Region


#Region "member"

#End Region

#Region "Funtions"
    ''' <summary>
    ''' Funcion para obtener el balance por los parametros correspondientes
    ''' </summary>
    ''' <param name="mont">mes.</param>
    ''' <param name="idAccount">id cuenta contable.</param>
    ''' <param name="idThird">id tercero.</param>
    ''' <param name="idCostCenter">id centro de costo.</param>
    ''' <returns></returns>
    Public Function GetBalanceByMonthAccountThirdCostCenter(mont As Integer, year As Integer, idAccount As Integer, idThird As Int32?, idCostCenter As Int32?) As GeneralLedgerBalance Implements IAccountingBalanceAdminService.GetBalanceByMonthAccountThirdCostCenter
        Return _RepositoryRead.GetBalanceByMonthAccountThirdCostCenter(mont, year, idAccount, idThird, idCostCenter)
    End Function

    ''' <summary>
    ''' funcion para guardar las cuentas de balance
    ''' </summary>
    ''' <param name="accountingBalance">The accounting balance.</param>
    ''' <returns></returns>
    Public Function SaveAccountingBalance(ByVal accounting As Domain.Entities.JournalVouchers, audit As AuditMessage, Optional ByVal withCommit As Boolean = True) As Boolean Implements IAccountingBalanceAdminService.SaveAccountingBalance
        Dim UnitOfWork As IUnitWork = _RepositoryRead.UnitWork
        Dim UnitOfWorkSave As IUnitWork = _RepositorySave.UnitWork()
        Using transaccion As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Dim accountingBalance As GeneralLedgerBalance
            Try
                Dim auditProcess As IndigoAuditSimpleEntity(Of GeneralLedgerBalance)
                Dim status As Integer
                Dim AuxAccountingBalance As Domain.Entities.GeneralLedgerBalance = Nothing
                'Me encargo de agrupar y sumar los registros que afecten las mismas cuentas, los mismos terceros y los centros de costo
                Dim listDetail = (From e In accounting.JournalVoucherDetails
                                  Group By e.IdMainAccount, e.IdThirdParty, e.IdCostCenter Into DebitValue = Sum(e.DebitValue), CreditValue = Sum(e.CreditValue)
                                  Select IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue).ToList()
                For Each itemDetail In listDetail
                    accountingBalance = Nothing
                    If accountingBalance Is Nothing Then
                        accountingBalance = _RepositoryRead.GetBalanceByMonthAccountThirdCostCenter(accounting.VoucherDate.Month, accounting.VoucherDate.Year, itemDetail.IdMainAccount, itemDetail.IdThirdParty, itemDetail.IdCostCenter)
                        If accountingBalance Is Nothing Then
                            accountingBalance = New GeneralLedgerBalance()
                            AssignValues(accountingBalance, accounting.VoucherDate.Month, accounting.VoucherDate.Year, itemDetail.IdMainAccount, itemDetail.IdThirdParty, itemDetail.IdCostCenter)
                        End If
                    End If
                    If accounting.Status = 4 Then 'Estado de desconfirmas, entonces se restan los valores
                        accountingBalance.DebitValue = accountingBalance.DebitValue - itemDetail.DebitValue
                        accountingBalance.CreditValue = accountingBalance.CreditValue - itemDetail.CreditValue
                    ElseIf accounting.Status = 2 Then 'Estado confirmado, entonces aumento los valores
                        accountingBalance.DebitValue = accountingBalance.DebitValue + itemDetail.DebitValue
                        accountingBalance.CreditValue = accountingBalance.CreditValue + itemDetail.CreditValue
                    End If

                    If accountingBalance.Id <> 0 Then
                        accountingBalance.MarkAsModified()
                    End If

                    If accountingBalance.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        status = Infrastructure.CrossCutting.Audit.Actions.Insert
                    Else
                        AuxAccountingBalance = accountingBalance.OriginalValue
                        status = Infrastructure.CrossCutting.Audit.Actions.Update
                    End If
                    Me._RepositorySave.SaveEntity(accountingBalance)
                    If withCommit Then
                        UnitOfWorkSave.Commit()
                    End If
                    auditProcess = New IndigoAuditSimpleEntity(Of GeneralLedgerBalance)(accountingBalance, audit, status, AuxAccountingBalance)
                    auditProcess.Execute()
                Next
                transaccion.Complete()
                Return True
            Catch ex As OptimisticConcurrencyException
                transaccion.Dispose()
                UnitOfWorkSave.RollbackChanges()
                Return False
            Catch ex As Exception
                transaccion.Dispose()
                UnitOfWorkSave.RollbackChanges()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return False
            End Try
        End Using


    End Function

    ''' <summary>
    ''' Metodo para asignar los campos a la entidad
    ''' </summary>
    ''' <param name="mont">The mont.</param>
    ''' <param name="idAccount">The identifier account.</param>
    ''' <param name="idThird">The identifier third.</param>
    ''' <param name="idCostCenter">The identifier cost center.</param>
    Private Sub AssignValues(accountingBalance As GeneralLedgerBalance, mont As Integer, year As Integer, idAccount As Integer, idThird As Integer?, idCostCenter As Integer?)
        With accountingBalance
            If idCostCenter Is Nothing Then
                .IdCostCenter = Nothing
            Else
                .IdCostCenter = idCostCenter
            End If
            If idThird Is Nothing Then
                .IdThirdParty = Nothing
            Else
                .IdThirdParty = idThird
            End If
            .IdMainAccount = idAccount
            .Month = mont
            .Year = year
        End With
    End Sub
#End Region

    ''' <summary>
    ''' metodo para recalcular los saldos de contabilidad
    ''' </summary>
    ''' <param name="periodId"></param>
    ''' <param name="legalBookId"></param>
    ''' <param name="mainAccountId"></param>
    ''' <param name="validateMovement"></param>
    ''' <returns></returns>
    Public Function RecalculateBalance(periodId As Integer, legalBookId As Integer, mainAccountId As Integer, validateMovement As Boolean, year As Integer) As ActionResult(Of List(Of Tuple(Of String, Integer))) Implements IAccountingBalanceAdminService.RecalculateBalance
        Dim period = _closeMonthRepository.GetMonthClose(periodId, year)
        If period Is Nothing Then
            Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StatusCode = eStatusResult.WARNING, .Message = "No se encontro el periodo especificado"}
        End If
        period.Status = False
        period.RecalculatingBalances = True
        period.MarkAsModified()
        _closeMonthRepository.SaveEntity(period)
        _closeMonthRepository.UnitWork.Commit()
        Dim resultReturn As ActionResult(Of List(Of Tuple(Of String, Integer))) = Nothing


        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = IsolationLevel.ReadCommitted
        Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                CType(_RepositorySave.UnitWork, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
                Dim result = _RepositorySave.RecalculateBalance(periodId, legalBookId, mainAccountId, validateMovement, year)
                Dim resultList = result.ToList()
                'valido que los debitos y creditos esten bien
                If resultList.FindAll(Function(x) x.Status = 4).Count > 0 Then
                    Dim listErrors As New List(Of Tuple(Of String, Integer))
                    For Each item In resultList.FindAll(Function(x) x.DebitValue <> x.CreditValue)
                        listErrors.Add(New Tuple(Of String, Integer)("El comprobante " & item.Consecutive & " con tipo de documento " & item.CodeNameJournalVoucherType & " esta desbalanceado, creditos(" & CDec(item.CreditValue).ToString("C0") & ") - debitos(" & CDec(item.DebitValue).ToString("C0") & ")", 2))
                    Next
                    resultReturn = New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StatusCode = eStatusResult.WARNING, .ObjectEmbbeded = listErrors}

                ElseIf resultList.FindAll(Function(x) x.Status = 3).Count = 0 Then
                    scope.Complete()
                    resultReturn = New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StatusCode = eStatusResult.SUCCESS, .Message = resultList(0).Message}
                Else
                    Dim listErrors As New List(Of Tuple(Of String, Integer))
                    For Each item In resultList
                        listErrors.Add(New Tuple(Of String, Integer)(item.Message, 2))
                    Next
                    scope.Dispose()
                    resultReturn = New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StatusCode = eStatusResult.WARNING, .ObjectEmbbeded = listErrors}
                End If

            Catch ex As Exception
                scope.Dispose()
                resultReturn = New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
            End Try
        End Using

        period.Status = True
        period.RecalculatingBalances = False
        period.MarkAsModified()
        _closeMonthRepository.SaveEntity(period)
        _closeMonthRepository.UnitWork.Commit()
        Return resultReturn
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If

            _RepositoryRead = Nothing
            _RepositorySave = Nothing
            _closeMonthRepository = Nothing
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
