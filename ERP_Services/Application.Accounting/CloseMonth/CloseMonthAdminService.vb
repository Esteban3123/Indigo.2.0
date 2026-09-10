'***********************************************************************
' Assembly         : Application.Accounting.PUCAdminService
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 02-05-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports System.Data.Entity.Core
Imports System.Transactions
Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions

#End Region
Public Class CloseMonthAdminService
    Implements ICloseMonthAdminService


    Private _RepositoryJournalVouchers As IAccountingDocumentRepository
    Private _Repository As ICloseMonthRepository

#Region "Builder"
    Public Sub New(ByVal pucRepository As Domain.Entities.ICloseMonthRepository, ByVal repositoryJournalVouchers As IAccountingDocumentRepository)
        If pucRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio vacio")
        End If
        _RepositoryJournalVouchers = repositoryJournalVouchers
        _Repository = pucRepository
    End Sub
#End Region

#Region "funtions"
    ''' <summary>
    ''' Gets the close month.
    ''' </summary>
    ''' <param name="month">The month.</param>
    ''' <param name="year">The year.</param>
    ''' <returns></returns>
    Public Function GetCloseMonth(month As Integer, year As Integer) As Boolean Implements ICloseMonthAdminService.GetCloseMonth
        Return _Repository.GetPeriod(month, year)
    End Function

    ''' <summary>
    ''' Saves the close month.
    ''' </summary>
    ''' <param name="month">The month.</param>
    ''' <returns></returns>
    Public Function SaveCloseMonth(ByVal month As ClosedMonth, ByVal audit As AuditMessage, ByVal Status As Nullable(Of Integer), ListIdJournalVouchers As String, ByVal idMonth As Nullable(Of Integer)) As ActionResult(Of ClosedMonth) Implements ICloseMonthAdminService.SaveCloseMonth
        Dim UnitOfWork As IUnitWork = _Repository.UnitWork
        Using transaction As New Transactions.TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                Dim auditProcess As IndigoAuditSimpleEntity(Of ClosedMonth)
                Dim statusAudit As Integer
                Dim auxMont As ClosedMonth = Nothing


                If month.Status = False Then
                    If Status IsNot Nothing Then
                        If _RepositoryJournalVouchers.SaveListJournalVouchers(Status, ListIdJournalVouchers, idMonth) Is Nothing Then
                            transaction.Dispose()
                            UnitOfWork.RollbackChanges()
                            Return New ActionResult(Of ClosedMonth) With {.StateResult = False, .MessageResult = {"0002"}.ToList()}
                        End If
                    End If
                End If
                If month.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    month.CreationDate = DateTime.Now
                    month.CreationUser = audit.CodeUser
                    statusAudit = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    month.ModificationDate = DateTime.Now
                    month.ModificationUser = audit.CodeUser
                    statusAudit = Infrastructure.CrossCutting.Audit.Actions.Update
                    auxMont = _Repository.GetMonthClose(month.Month, month.Year)
                End If

                Me._Repository.SaveEntity(month)
                UnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of ClosedMonth)(month, audit, statusAudit, auxMont)
                auditProcess.Execute()
                transaction.Complete()
                month.MarkAsUnchanged()
                Return New ActionResult(Of ClosedMonth) With {.StateResult = True, .ObjectEmbbeded = month}
            Catch ex As OptimisticConcurrencyException
                transaction.Dispose()
                UnitOfWork.RollbackChanges()
                Return New ActionResult(Of ClosedMonth) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
            Catch ex As Exception
                transaction.Dispose()
                UnitOfWork.RollbackChanges()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of ClosedMonth) With {.StateResult = False}
            End Try
        End Using

    End Function

    ''' <summary>
    ''' Funcion para validar si el periodo se encuentra abierto
    ''' </summary>
    ''' <param name="month">el mes</param>
    ''' <param name="year">el año</param>
    ''' <param name="status">el estado del mes que se quiere obtener</param>
    ''' <returns>true si esta abierto.false si esta cerrado.</returns>
    Public Function ValidateOpenMonth(month As Integer, year As Integer, status As Boolean) As Boolean Implements ICloseMonthAdminService.ValidateOpenMonth
        Return _Repository.ValidateOpenMonth(month, year, status)
    End Function

    ''' <summary>
    ''' Metodo para validar si el periodo esta abierto
    ''' </summary>
    ''' <param name="month">el mes.</param>
    ''' <param name="year">el año.</param>
    ''' <returns>
    ''' true = si esta abierto . false = si esta cerrado
    ''' </returns>
    Public Function ValidatePeriodOpen(month As Integer, year As Integer) As Boolean Implements ICloseMonthAdminService.ValidatePeriodOpen
        Return _Repository.ValidatePeriodOpen(month, year)
    End Function

    ''' <summary>
    ''' funcion para obtener el mes
    ''' </summary>
    ''' <param name="month"></param>
    ''' <param name="year"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMonthClose(month As Integer, year As Integer) As ClosedMonth Implements ICloseMonthAdminService.GetMonthClose
        Return _Repository.GetMonthClose(month, year)
    End Function

    ''' <summary>
    ''' Obtiene el registro del periodo abierto
    ''' </summary>
    ''' <returns></returns>
    Public Function GetOpenPeriod() As List(Of ClosedMonth) Implements ICloseMonthAdminService.GetOpenPeriod
        Return _Repository.GetOpenPeriod()
    End Function


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="year"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllMontbyYear(year As Integer, ByVal Status As Boolean) As List(Of CloseMonthComplex) Implements ICloseMonthAdminService.GetAllMontbyYear
        Return _Repository.GetAllMontbyYear(year, Status)
    End Function

    Public Function GetLastMonthOpen(status As Boolean) As Integer Implements ICloseMonthAdminService.GetLastMonthOpen
        Return _Repository.GetAllMonthByStatus(status)
    End Function

    ''' <summary>
    ''' Validates the open period.
    ''' </summary>
    ''' <param name="month">The month.</param>
    ''' <param name="year">The year.</param>
    ''' <returns></returns>
    Public Function ValidateOpenPeriod(month As Integer, year As Integer) As Boolean Implements ICloseMonthAdminService.ValidateOpenPeriod
        Return _Repository.ValidateOpenPeriod(month, year)
    End Function

#End Region

    Public Function ValidateBalanceCloseMonth(period As String, year As Integer) As ActionResult(Of List(Of Tuple(Of String, Integer))) Implements ICloseMonthAdminService.ValidateBalanceCloseMonth
        Dim resultReturn As ActionResult(Of List(Of Tuple(Of String, Integer))) = Nothing
        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = IsolationLevel.ReadCommitted
        Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                If period < 9 Then
                    period = "0" + period
                End If
                Dim result = _Repository.ValidateBalanceCloseMonth(period, year)
                Dim resultList = result.ToList()
                'obtengo los items con estado 4 que es desbalanceado
                Dim listItemsErrors = resultList.FindAll(Function(x) x.Status = 4)
                If listItemsErrors.Count > 1 Then
                    Dim listErrors As New List(Of Tuple(Of String, Integer))
                    For Each item In resultList.FindAll(Function(x) x.DebitValue <> x.CreditValue)
                        listErrors.Add(New Tuple(Of String, Integer)("El comprobante " & item.Consecutive & " con tipo de documento " & item.CodeNameJournalVoucherTye & " esta desbalanceado, creditos(" & CDec(item.CreditValue).ToString("C0") & ") - debitos(" & CDec(item.DebitValue).ToString("C0") & ")", 2))
                    Next
                    scope.Dispose()
                    resultReturn = New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StatusCode = eStatusResult.WARNING, .ObjectEmbbeded = listErrors}
                ElseIf listItemsErrors.Count = 1 Then
                    Dim listErrors As New List(Of Tuple(Of String, Integer))

                    listErrors.Add(New Tuple(Of String, Integer)(listItemsErrors(0).Message, 2))

                    scope.Dispose()
                    resultReturn = New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StatusCode = eStatusResult.WARNING, .ObjectEmbbeded = listErrors}
                Else

                    scope.Complete()
                    resultReturn = New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StatusCode = eStatusResult.SUCCESS}
                End If
            Catch ex As Exception
                scope.Dispose()
                resultReturn = New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
            End Try
        End Using
        Return resultReturn
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _RepositoryJournalVouchers = Nothing
            _Repository = Nothing
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
