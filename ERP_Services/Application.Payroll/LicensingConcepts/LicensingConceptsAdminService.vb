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
Imports Domain.Entities
Imports System.Data.Entity.Core
Public Class LicensingConceptsAdminService
    Implements ILicensingConceptsAdminService

    Private _licensingConceptsRepository As ILicensingConceptsRepository
    Private _secuenseDRepository As IPayrollSequenceDetailRepository
    Private FORM_NAME As String = "Conceptos de Licencia"

    Public Sub New(ByVal repository As ILicensingConceptsRepository, secuenseDRepository As IPayrollSequenceDetailRepository)
        If (repository Is Nothing = True) Then
            Throw New ArgumentNullException("Concepto de licecencia está vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _licensingConceptsRepository = repository
        _secuenseDRepository = secuenseDRepository
    End Sub

    ''' <summary>
    ''' Lista los conceptos de licencia
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAllLicensingConcepts() As List(Of LicensingConcepts) Implements ILicensingConceptsAdminService.ListAllLicensingConcepts
        Try
            Return _licensingConceptsRepository.ListAllLicensingConcepts()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        End Try
    End Function

    ''' <summary>
    ''' Elimina un concepto de licencia
    ''' </summary>
    ''' <param name="licensingConcepts"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function DeleteLicensingConepts(licensingConcepts As LicensingConcepts, audit As AuditMessage) As ActionMessageResult(Of LicensingConcepts) Implements ILicensingConceptsAdminService.DeleteLicensingConcepts
        Dim result As New ActionMessageResult(Of LicensingConcepts)
        result.StateResult = True
        If licensingConcepts Is Nothing Then
            Throw New ArgumentNullException("Concepto de licencia vacio")
        End If
        Dim unitWork As IUnitWork = _licensingConceptsRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                licensingConcepts.ModificationUser = audit.CodeUser
                licensingConcepts.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of LicensingConcepts)(licensingConcepts, audit, status)

                licensingConcepts.MarkAsDeleted()
                Me._licensingConceptsRepository.SaveEntity(licensingConcepts)
                unitWork.Commit()
                auditProcess.Execute()
                scope.Complete()
                Return New ActionMessageResult(Of LicensingConcepts) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}

            End Using
        Catch ex As DbUpdateException
            unitWork.RollbackChanges()
            result.StateResult = False
            result.StatusCode = eStatusResult.WARNING
            result.Message = ResourceManager.GetString("ErrorDependence")
            result.MessageResult.Add(New MessageResult("c-0000", licensingConcepts.Code))
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

    ''' <summary>
    ''' Guarda el concepto de licencia
    ''' </summary>
    ''' <param name="licensingConcepts"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequence"></param>
    ''' <returns></returns>
    Public Function SavelicensingConcepts(licensingConcepts As LicensingConcepts, audit As AuditMessage, Optional idSequence As Long = 0) As ActionResult(Of LicensingConcepts) Implements ILicensingConceptsAdminService.SavelicensingConcepts
        If licensingConcepts Is Nothing Then
            Throw New ArgumentNullException("Concepto de licencia Vacio")
        End If
        Dim unitWork As IUnitWork = _licensingConceptsRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty
                If String.IsNullOrEmpty(licensingConcepts.Code) Then
                    Dim seq As Domain.Entities.PayrollSequenceDetail = Me._secuenseDRepository.GetSequenseDetailUpdatedById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PayrollSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            licensingConcepts.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of LicensingConcepts) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.PayrollSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), licensingConcepts.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of LicensingConcepts) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If
                Dim auxObjEntity As LicensingConcepts = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of LicensingConcepts)
                Dim status As Integer

                If licensingConcepts.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    licensingConcepts.CreationUser = audit.CodeUser
                    licensingConcepts.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = _licensingConceptsRepository.GetLicensingConcepts(licensingConcepts.Code, False)
                    licensingConcepts.CreationUser = audit.CodeUser
                    licensingConcepts.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._licensingConceptsRepository.SaveEntity(licensingConcepts)
                unitWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of LicensingConcepts)(licensingConcepts, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                licensingConcepts.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of LicensingConcepts) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitWork.RollbackChanges()
            Return New ActionResult(Of LicensingConcepts) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of LicensingConcepts) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}

        End Try
    End Function

    ''' <summary>
    ''' Actualiza el concepto de licencia
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="status"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function UpdatelicensingConcepts(code As String, status As Boolean, audit As AuditMessage) As ActionResult(Of LicensingConcepts) Implements ILicensingConceptsAdminService.UpdatelicensingConcepts
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If String.IsNullOrEmpty(status) Then
            Throw New ArgumentNullException("status")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim licensingConcepts As LicensingConcepts = Me._licensingConceptsRepository.GetLicensingConcepts(code.Trim())
            If licensingConcepts IsNot Nothing AndAlso licensingConcepts.Id > 0 Then
                licensingConcepts.Status = status
            End If
            Dim result = Me.SavelicensingConcepts(licensingConcepts, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of LicensingConcepts) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el concepto de licencia por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function GetLicensingConcepts(code As String, audit As AuditMessage) As LicensingConcepts Implements ILicensingConceptsAdminService.GetLicensingConcepts
        Try
            Return _licensingConceptsRepository.GetLicensingConcepts(code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el concepto de licencia por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetLicensingConceptsById(Id As Integer) As LicensingConcepts Implements ILicensingConceptsAdminService.GetLicensingConceptsById
        If String.IsNullOrEmpty(Id) Then
            Throw New ArgumentNullException("Codigo vacio")
        End If
        Try
            Return _licensingConceptsRepository.GetLicensingConceptsById(Id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New LicensingConcepts()
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

            _licensingConceptsRepository = Nothing
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
