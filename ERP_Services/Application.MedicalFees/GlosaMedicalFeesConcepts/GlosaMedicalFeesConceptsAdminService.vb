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

Public Class GlosaMedicalFeesConceptsAdminService
    Implements IGlosaMedicalFeesConceptsAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio
    ''' </summary>
    Private Const FORM_NAME As String = "FrmGlosaMedicalFeesConcepts"
    Private _GlosaMedicalFeesConceptsRepository As IGlosaMedicalFeesConceptsRepository
    Private _sequenseRepository As IMedicalFeesSecuenceDetailRepository
#End Region

#Region "Constructor"
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal GlosaMedicalFeesConceptsRepository As IGlosaMedicalFeesConceptsRepository, ByVal sequenseRepository As IMedicalFeesSecuenceDetailRepository)
        _GlosaMedicalFeesConceptsRepository = GlosaMedicalFeesConceptsRepository
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
    Public Function GetGlosaMedicalFeesConceptsByCode(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of GlosaMedicalFeesConcepts) Implements IGlosaMedicalFeesConceptsAdminService.GetGlosaMedicalFeesConceptsByCode

        Try
            Dim GlosaMedicalFeesConcepts As GlosaMedicalFeesConcepts = _GlosaMedicalFeesConceptsRepository.GetGlosaMedicalFeesConceptsByCode(code)
            Return New ActionResult(Of GlosaMedicalFeesConcepts) With {.StateResult = True, .ObjectEmbbeded = GlosaMedicalFeesConcepts}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of GlosaMedicalFeesConcepts) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try

    End Function

    ''' <summary>
    ''' Obtiene un Concepto por ID
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function GetGlosaMedicalFeesConceptsById(ByVal id As Integer, ByVal audit As AuditMessage) As ActionResult(Of GlosaMedicalFeesConcepts) Implements IGlosaMedicalFeesConceptsAdminService.GetGlosaMedicalFeesConceptsById

        Try
            Dim GlosaMedicalFeesConcepts As GlosaMedicalFeesConcepts = _GlosaMedicalFeesConceptsRepository.GetGlosaMedicalFeesConceptsById(id)
            Return New ActionResult(Of GlosaMedicalFeesConcepts) With {.StateResult = True, .ObjectEmbbeded = Nothing}

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of GlosaMedicalFeesConcepts) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda y Actualiza un registro
    ''' </summary>
    ''' <param name="GlosaMedicalFeesConcepts"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    Public Function SaveGlosaMedicalFeesConcepts(GlosaMedicalFeesConcepts As GlosaMedicalFeesConcepts, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of GlosaMedicalFeesConcepts) Implements IGlosaMedicalFeesConceptsAdminService.SaveGlosaMedicalFeesConcepts
        If GlosaMedicalFeesConcepts Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._GlosaMedicalFeesConceptsRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequenseRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(GlosaMedicalFeesConcepts.Code) Then
                    Dim seq As MedicalFeesSecuenceDetail = Me._sequenseRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.MedicalFeesSecuence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            GlosaMedicalFeesConcepts.Code = res
                            seq.Next += 1
                            Me._sequenseRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of GlosaMedicalFeesConcepts) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.MedicalFeesSecuence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), GlosaMedicalFeesConcepts.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of GlosaMedicalFeesConcepts) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As GlosaMedicalFeesConcepts = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of GlosaMedicalFeesConcepts)
                Dim status As Integer

                If GlosaMedicalFeesConcepts.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    GlosaMedicalFeesConcepts.CreationUser = audit.CodeUser
                    GlosaMedicalFeesConcepts.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = GlosaMedicalFeesConcepts.OriginalValue
                    GlosaMedicalFeesConcepts.ModificationUser = audit.CodeUser
                    GlosaMedicalFeesConcepts.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._GlosaMedicalFeesConceptsRepository.SaveEntity(GlosaMedicalFeesConcepts)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of GlosaMedicalFeesConcepts)(GlosaMedicalFeesConcepts, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                GlosaMedicalFeesConcepts.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of GlosaMedicalFeesConcepts) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = GlosaMedicalFeesConcepts, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of GlosaMedicalFeesConcepts) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of GlosaMedicalFeesConcepts) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ChangeStateGlosaMedicalFeesConcepts(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of GlosaMedicalFeesConcepts) Implements IGlosaMedicalFeesConceptsAdminService.ChangeStateGlosaMedicalFeesConcepts

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
            Dim GlosaMedicalFeesConcepts As GlosaMedicalFeesConcepts = Me._GlosaMedicalFeesConceptsRepository.GetGlosaMedicalFeesConceptsByCode(code.Trim())
            If GlosaMedicalFeesConcepts IsNot Nothing AndAlso GlosaMedicalFeesConcepts.Id > 0 Then
                GlosaMedicalFeesConcepts.Status = state
            End If
            Dim result = Me.SaveGlosaMedicalFeesConcepts(GlosaMedicalFeesConcepts, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of GlosaMedicalFeesConcepts) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' eliminar un conepto de conciliacion
    ''' </summary>
    ''' <param name="GlosaMedicalFeesConcepts">The portfolio note concept.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">EconomicIndicator Vacio</exception>
    Public Function DeleteGlosaMedicalFeesConcepts(GlosaMedicalFeesConcepts As GlosaMedicalFeesConcepts, audit As AuditMessage) As ActionResult(Of GlosaMedicalFeesConcepts) Implements IGlosaMedicalFeesConceptsAdminService.DeleteGlosaMedicalFeesConcepts

        If GlosaMedicalFeesConcepts Is Nothing Then
            Throw New ArgumentNullException("invoiceCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._GlosaMedicalFeesConceptsRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                GlosaMedicalFeesConcepts.ModificationUser = audit.CodeUser
                GlosaMedicalFeesConcepts.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of GlosaMedicalFeesConcepts)(GlosaMedicalFeesConcepts, audit, status)
                GlosaMedicalFeesConcepts.MarkAsDeleted()
                Me._GlosaMedicalFeesConceptsRepository.SaveEntity(GlosaMedicalFeesConcepts)
                unitOfWork.Commit()
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult(Of GlosaMedicalFeesConcepts) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of GlosaMedicalFeesConcepts) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-999"}), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of GlosaMedicalFeesConcepts) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of GlosaMedicalFeesConcepts) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of GlosaMedicalFeesConcepts) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
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
            _GlosaMedicalFeesConceptsRepository = Nothing
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
