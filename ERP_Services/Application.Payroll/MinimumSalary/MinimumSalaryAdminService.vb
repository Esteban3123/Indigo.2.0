
Imports Domain.Payroll.Entities
Imports Domain.Payroll
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Transactions
Imports Infrastructure.CrossCutting.Resources
Imports System.Text
Imports Application.Payroll
Imports Domain.Entities
Imports System.Data.Entity.Core

Public Class MinimumSalaryAdminService
    Implements IMinimumSalaryAdminService


    'Repositorio de salarios minimos
    Private _minimumSalaryRepository As IMinimumSalaryRepository
    Private _secuenseDRepository As Domain.Entities.IPayrollSequenceDetailRepository
    Private FORM_NAME As String = "Salario Mínimo"

    ''' <summary>
    ''' inicia el repositorio de Salarios minimos
    ''' </summary>
    ''' <param name="minimumSalaryRepository">Repositorio de salarios minimos</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal repository As IMinimumSalaryRepository, secuenseDRepository As Domain.Entities.IPayrollSequenceDetailRepository)
        If (repository Is Nothing = True) Then
            Throw New ArgumentNullException("Salario Mínimo Vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _minimumSalaryRepository = repository
        _secuenseDRepository = secuenseDRepository
    End Sub

    ''' <summary>
    ''' Lista todos los salarios minimos
    ''' </summary>
    ''' <returns>Lista de salarios minimos</returns>
    Public Function ListAllMinimumSalary() As List(Of MinimumSalary) Implements IMinimumSalaryAdminService.ListAllMinimumSalary
        Try
            Return _minimumSalaryRepository.ListAllMinimumSalary()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        End Try
    End Function

    ''' <summary>
    ''' Elimina un salario minimo
    ''' </summary>
    ''' <param name="minimumSalary">Salario Minimo</param>
    ''' <returns></returns>
    Public Function DeleteMinimunSalary(minimumSalary As MinimumSalary, audit As AuditMessage) As ActionMessageResult(Of MinimumSalary) Implements IMinimumSalaryAdminService.DeleteMinimunSalary
        Dim result As New ActionMessageResult(Of MinimumSalary)
        result.StateResult = True
        If minimumSalary Is Nothing Then
            Throw New ArgumentNullException("Salario mínimo vacio")
        End If
        Dim unitWork As IUnitWork = _minimumSalaryRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                minimumSalary.ModificationUser = audit.CodeUser
                minimumSalary.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of MinimumSalary)(minimumSalary, audit, status)

                minimumSalary.MarkAsDeleted()
                Me._minimumSalaryRepository.SaveEntity(minimumSalary)
                unitWork.Commit()
                auditProcess.Execute()
                scope.Complete()
                Return New ActionMessageResult(Of MinimumSalary) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}

            End Using
        Catch ex As DbUpdateException
            unitWork.RollbackChanges()
            result.StateResult = False
            result.StatusCode = eStatusResult.WARNING
            result.Message = ResourceManager.GetString("ErrorDependence")
            result.MessageResult.Add(New MessageResult("c-0000", minimumSalary.Year))
            Return result
        Catch ex As Exception
            unitWork.RollbackChanges()
            result.StatusCode = eStatusResult.EXCEPTION
            result.StateResult = False
            result.Message = IndigoManagementExceptions.GetExceptionDetails(ex)
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return result

        End Try
    End Function

    Public Function SaveMinimunSalary(minimumSalary As MinimumSalary, audit As AuditMessage, Optional idSequence As Int64 = 0) As ActionResult(Of MinimumSalary) Implements IMinimumSalaryAdminService.SaveMinimunSalary
        If minimumSalary Is Nothing Then
            Throw New ArgumentNullException("Salario Vacio")
        End If
        Dim unitWork As IUnitWork = _minimumSalaryRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty
                If String.IsNullOrEmpty(minimumSalary.Year) Then
                    Dim seq As Domain.Entities.PayrollSequenceDetail = Me._secuenseDRepository.GetSequenseDetailUpdatedById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PayrollSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            minimumSalary.Year = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of MinimumSalary) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.PayrollSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), minimumSalary.Year), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of MinimumSalary) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If
                Dim auxObjEntity As MinimumSalary = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of MinimumSalary)
                Dim status As Integer

                If minimumSalary.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    minimumSalary.CreationUser = audit.CodeUser
                    minimumSalary.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = _minimumSalaryRepository.GetMinimunSalaryByYear(minimumSalary.Year, False)
                    minimumSalary.CreationUser = audit.CodeUser
                    minimumSalary.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._minimumSalaryRepository.SaveEntity(minimumSalary)
                unitWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of MinimumSalary)(minimumSalary, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                minimumSalary.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of MinimumSalary) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = minimumSalary, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitWork.RollbackChanges()
            Return New ActionResult(Of MinimumSalary) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of MinimumSalary) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}

        End Try
    End Function

    Public Function UpdateMinimunSalary(year As String, state As Boolean, audit As AuditMessage) As ActionResult(Of MinimumSalary) Implements IMinimumSalaryAdminService.UpdateMinimunSalary
        If String.IsNullOrEmpty(year) Then
            Throw New ArgumentNullException("code")
        End If
        If String.IsNullOrEmpty(state) Then
            Throw New ArgumentNullException("state")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim minimumSalary As MinimumSalary = Me._minimumSalaryRepository.GetMinimunSalaryByYear(year.Trim())
            If minimumSalary IsNot Nothing AndAlso minimumSalary.Id > 0 Then
                minimumSalary.State = state
            End If
            Dim result = Me.SaveMinimunSalary(minimumSalary, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of MinimumSalary) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function GetMinimunSalary(year As String, audit As AuditMessage) As MinimumSalary Implements IMinimumSalaryAdminService.GetMinimunSalary
        Try
            Return _minimumSalaryRepository.GetMinimunSalaryByYear(year)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function GetMinimumSalaryById(Id As Integer) As MinimumSalary Implements IMinimumSalaryAdminService.GetMinimumSalaryById
        If String.IsNullOrEmpty(Id) Then
            Throw New ArgumentNullException("Codigo vacio")
        End If
        Try
            Return _minimumSalaryRepository.GetMinimunSalaryById(Id)

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New MinimumSalary()
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            _minimumSalaryRepository = Nothing
            _secuenseDRepository = Nothing
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)

        ' TODO: uncomment the following line if Finalize() is overridden above.
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
