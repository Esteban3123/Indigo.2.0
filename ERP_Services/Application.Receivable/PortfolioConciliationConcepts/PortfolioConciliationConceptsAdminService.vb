#Region "imports"
Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions
#End Region

Public Class PortfolioConciliationConceptsAdminService
    Implements IPortfolioConciliationConceptsAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio
    ''' </summary>
    Private Const FORM_NAME As String = "FrmConciliationConcepts"
    Private _PortfolioConciliationConceptsRepository As IPortfolioConciliationConceptsRepository
    Private _sequenseRepository As ISequensePortfolioDRepository
#End Region

#Region "Constructor"
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal PortfolioConciliationConceptsRepository As IPortfolioConciliationConceptsRepository, ByVal sequenseRepository As ISequensePortfolioDRepository)
        _PortfolioConciliationConceptsRepository = PortfolioConciliationConceptsRepository
        _sequenseRepository = sequenseRepository
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene un Concepto por Codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPortfolioConciliationConceptsByCode(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of PortfolioConciliationConcepts) Implements IPortfolioConciliationConceptsAdminService.GetPortfolioConciliationConceptsByCode

        Try
            Dim PortfolioConciliationConcepts As PortfolioConciliationConcepts = _PortfolioConciliationConceptsRepository.GetPortfolioConciliationConceptsByCode(code)
            Return New ActionResult(Of PortfolioConciliationConcepts) With {.StateResult = True, .ObjectEmbbeded = PortfolioConciliationConcepts}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of PortfolioConciliationConcepts) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try

    End Function

    ''' <summary>
    ''' Obtiene un Concepto por ID
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function GetPortfolioConciliationConceptsById(ByVal id As Integer, ByVal audit As AuditMessage) As ActionResult(Of PortfolioConciliationConcepts) Implements IPortfolioConciliationConceptsAdminService.GetPortfolioConciliationConceptsById

        Try
            Dim PortfolioConciliationConcepts As PortfolioConciliationConcepts = _PortfolioConciliationConceptsRepository.GetPortfolioConciliationConceptsById(id)
            Return New ActionResult(Of PortfolioConciliationConcepts) With {.StateResult = True, .ObjectEmbbeded = Nothing}

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of PortfolioConciliationConcepts) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda y Actualiza un registro
    ''' </summary>
    ''' <param name="PortfolioConciliationConcepts"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    Public Function SavePortfolioConciliationConcepts(PortfolioConciliationConcepts As PortfolioConciliationConcepts, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of PortfolioConciliationConcepts) Implements IPortfolioConciliationConceptsAdminService.SavePortfolioConciliationConcepts
        If PortfolioConciliationConcepts Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._PortfolioConciliationConceptsRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequenseRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(PortfolioConciliationConcepts.Code) Then
                    Dim seq As PortfolioSequenceDetail = Me._sequenseRepository.GetSequenseDetailUpdatedById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PortfolioSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            PortfolioConciliationConcepts.Code = res
                            seq.Next += 1
                            Me._sequenseRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of PortfolioConciliationConcepts) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.PortfolioSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), PortfolioConciliationConcepts.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of PortfolioConciliationConcepts) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As PortfolioConciliationConcepts = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of PortfolioConciliationConcepts)
                Dim status As Integer

                If PortfolioConciliationConcepts.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    PortfolioConciliationConcepts.CreationUser = audit.CodeUser
                    PortfolioConciliationConcepts.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = PortfolioConciliationConcepts.OriginalValue
                    PortfolioConciliationConcepts.ModificationUser = audit.CodeUser
                    PortfolioConciliationConcepts.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._PortfolioConciliationConceptsRepository.SaveEntity(PortfolioConciliationConcepts)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of PortfolioConciliationConcepts)(PortfolioConciliationConcepts, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                PortfolioConciliationConcepts.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of PortfolioConciliationConcepts) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = PortfolioConciliationConcepts, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of PortfolioConciliationConcepts) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of PortfolioConciliationConcepts) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ChangeStatePortfolioConciliationConcepts(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of PortfolioConciliationConcepts) Implements IPortfolioConciliationConceptsAdminService.ChangeStatePortfolioConciliationConcepts

        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If String.IsNullOrEmpty(state) Then
            Throw New ArgumentNullException("state")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim PortfolioConciliationConcepts As PortfolioConciliationConcepts = Me._PortfolioConciliationConceptsRepository.GetPortfolioConciliationConceptsByCode(code.Trim())
            If PortfolioConciliationConcepts IsNot Nothing AndAlso PortfolioConciliationConcepts.Id > 0 Then
                PortfolioConciliationConcepts.Status = state
            End If
            Dim result = Me.SavePortfolioConciliationConcepts(PortfolioConciliationConcepts, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of PortfolioConciliationConcepts) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' eliminar un conepto de conciliacion
    ''' </summary>
    ''' <param name="PortfolioConciliationConcepts">The portfolio note concept.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">EconomicIndicator Vacio</exception>
    Public Function DeletePortfolioConciliationConcepts(PortfolioConciliationConcepts As PortfolioConciliationConcepts, audit As AuditMessage) As ActionResult(Of PortfolioConciliationConcepts) Implements IPortfolioConciliationConceptsAdminService.DeletePortfolioConciliationConcepts

        If PortfolioConciliationConcepts Is Nothing Then
            Throw New ArgumentNullException("invoiceCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._PortfolioConciliationConceptsRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                PortfolioConciliationConcepts.ModificationUser = audit.CodeUser
                PortfolioConciliationConcepts.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of PortfolioConciliationConcepts)(PortfolioConciliationConcepts, audit, status)
                PortfolioConciliationConcepts.MarkAsDeleted()
                Me._PortfolioConciliationConceptsRepository.SaveEntity(PortfolioConciliationConcepts)
                unitOfWork.Commit()
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult(Of PortfolioConciliationConcepts) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of PortfolioConciliationConcepts) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-999"}), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of PortfolioConciliationConcepts) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of PortfolioConciliationConcepts) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of PortfolioConciliationConcepts) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _PortfolioConciliationConceptsRepository = Nothing
            _sequenseRepository = Nothing
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
