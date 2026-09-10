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
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources

#End Region

Public Class BudgetAllocationInitialBalancesAdminService
    Implements IBudgetAllocationInitialBalancesAdminService

    Private _accountReceivableRepository As IAccountReceivableRepository

    Private _portfolioInitialBalanceAccountReceivableRepository As IPortfolioInitialBalanceAccountReceivableRepository

    Private _initialBalanceRepository As IPortfolioInitialBalanceRepository

    Public Sub New(accountReceivableRepository As IAccountReceivableRepository, portfolioInitialBalanceAccountReceivableRepository As IPortfolioInitialBalanceAccountReceivableRepository,
                   initialBalanceRepository As IPortfolioInitialBalanceRepository)

        _accountReceivableRepository = accountReceivableRepository
        _portfolioInitialBalanceAccountReceivableRepository = portfolioInitialBalanceAccountReceivableRepository
        _initialBalanceRepository = initialBalanceRepository
    End Sub

    ''' <summary>
    ''' asigana un presupuesto a las facturas de saldos iniciales
    ''' </summary>
    ''' <param name="listItems"></param>
    ''' <param name="initialBalanceId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveBudgetAllocationInitialBalances(listItems As List(Of Tuple(Of Integer, Integer)), initialBalanceId As Integer, audit As AuditMessage) As ActionResult Implements IBudgetAllocationInitialBalancesAdminService.SaveBudgetAllocationInitialBalances
        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim accountRecivableUnitOfWork = _accountReceivableRepository.UnitWork
                Dim initialBalanceaccounRecivableUnitOfWork = _portfolioInitialBalanceAccountReceivableRepository.UnitWork
                For Each item In listItems
                    Dim initialBalanceAccountReceivable = _portfolioInitialBalanceAccountReceivableRepository.GetPortfolioInitialBalanceAccountReceivableById(item.Item1)
                    initialBalanceAccountReceivable.AffectBudget = True
                    initialBalanceAccountReceivable.BudgetId = item.Item2
                    _portfolioInitialBalanceAccountReceivableRepository.SaveEntity(initialBalanceAccountReceivable)
                    initialBalanceaccounRecivableUnitOfWork.Commit()


                    Dim accountReceivable = _accountReceivableRepository.GetAccountReceivableByInvoiceNumberAndThirdPartyId(initialBalanceAccountReceivable.InvoiceNumber, initialBalanceAccountReceivable.ThirdPartyId)
                    accountReceivable.AffectBudget = True
                    accountReceivable.BudgetId = item.Item2
                    accountReceivable.ModificationUser = audit.CodeUser
                    accountReceivable.ModificationDate = DateTime.Now
                    _accountReceivableRepository.SaveEntity(accountReceivable)
                    accountRecivableUnitOfWork.Commit()
                Next
                Dim totalAffectBudget = _portfolioInitialBalanceAccountReceivableRepository.GetPortgolioInitialBalanceWithOutBudget(initialBalanceId)
                If totalAffectBudget.Count = 0 Then
                    Dim initialBalanceUnitOfWork = _initialBalanceRepository.UnitWork
                    Dim initialBalance = _initialBalanceRepository.GetPortfolioInitialBalanceById(initialBalanceId)
                    initialBalance.AllBudgetAssigned = True
                    _initialBalanceRepository.SaveEntity(initialBalance)
                    initialBalanceUnitOfWork.Commit()
                End If

                transaction.Complete()
                Return New ActionResult With {.StateResult = True}
            Catch ex As OptimisticConcurrencyException
                transaction.Dispose()
                Return New ActionResult With {.StateResult = False, .Message = ResourceManager.GetString("ErrorConcurrence")}
            Catch ex As DbEntityValidationException
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
            Catch ex As Exception
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
            End Try
        End Using
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _accountReceivableRepository = Nothing
            _portfolioInitialBalanceAccountReceivableRepository = Nothing
            _initialBalanceRepository = Nothing
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
