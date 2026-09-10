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

Public Class ConceptsCausesStatusFolioAdminService
    Implements IConceptsCausesStatusFolioAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio
    ''' </summary>
    Private Const FORM_NAME As String = "FrmConceptsCausesStatusFolio"
    Private _ConceptsCausesStatusFolioRepository As IConceptsCausesStatusFolioRepository
    Private _sequenseRepository As IBillingSequenceDetailRepository
#End Region

#Region "Constructor"
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal ConceptsCausesStatusFolioRepository As IConceptsCausesStatusFolioRepository, ByVal sequenseRepository As IBillingSequenceDetailRepository)
        _ConceptsCausesStatusFolioRepository = ConceptsCausesStatusFolioRepository
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
    Public Function GetConceptsCausesStatusFolioByCode(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of ConceptsCausesStatusFolio) Implements IConceptsCausesStatusFolioAdminService.GetConceptsCausesStatusFolioByCode

        Try
            Dim ConceptsCausesStatusFolio As ConceptsCausesStatusFolio = _ConceptsCausesStatusFolioRepository.GetConceptsCausesStatusFolioByCode(code)
            Return New ActionResult(Of ConceptsCausesStatusFolio) With {.StateResult = True, .ObjectEmbbeded = ConceptsCausesStatusFolio}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ConceptsCausesStatusFolio) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try

    End Function

    ''' <summary>
    ''' Obtiene un Concepto por ID
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function GetConceptsCausesStatusFolioById(ByVal id As Integer, ByVal audit As AuditMessage) As ActionResult(Of ConceptsCausesStatusFolio) Implements IConceptsCausesStatusFolioAdminService.GetConceptsCausesStatusFolioById

        Try
            Dim ConceptsCausesStatusFolio As ConceptsCausesStatusFolio = _ConceptsCausesStatusFolioRepository.GetConceptsCausesStatusFolioById(id)
            Return New ActionResult(Of ConceptsCausesStatusFolio) With {.StateResult = True, .ObjectEmbbeded = Nothing}

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ConceptsCausesStatusFolio) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda y Actualiza un registro
    ''' </summary>
    ''' <param name="ConceptsCausesStatusFolio"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    Public Function SaveConceptsCausesStatusFolio(ConceptsCausesStatusFolio As ConceptsCausesStatusFolio, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of ConceptsCausesStatusFolio) Implements IConceptsCausesStatusFolioAdminService.SaveConceptsCausesStatusFolio
        If ConceptsCausesStatusFolio Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._ConceptsCausesStatusFolioRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequenseRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(ConceptsCausesStatusFolio.Code) Then
                    Dim seq As BillingSequenceDetail = Me._sequenseRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.BillingSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            ConceptsCausesStatusFolio.Code = res
                            seq.Next += 1
                            Me._sequenseRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of ConceptsCausesStatusFolio) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.BillingSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), ConceptsCausesStatusFolio.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of ConceptsCausesStatusFolio) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As ConceptsCausesStatusFolio = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of ConceptsCausesStatusFolio)
                Dim status As Integer

                If ConceptsCausesStatusFolio.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    ConceptsCausesStatusFolio.CreationUser = audit.CodeUser
                    ConceptsCausesStatusFolio.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = ConceptsCausesStatusFolio.OriginalValue
                    ConceptsCausesStatusFolio.ModificationUser = audit.CodeUser
                    ConceptsCausesStatusFolio.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._ConceptsCausesStatusFolioRepository.SaveEntity(ConceptsCausesStatusFolio)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of ConceptsCausesStatusFolio)(ConceptsCausesStatusFolio, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                ConceptsCausesStatusFolio.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of ConceptsCausesStatusFolio) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = ConceptsCausesStatusFolio, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of ConceptsCausesStatusFolio) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ConceptsCausesStatusFolio) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="Id">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ChangeStateConceptsCausesStatusFolio(Id As Integer, state As Boolean, audit As AuditMessage) As ActionResult(Of ConceptsCausesStatusFolio) Implements IConceptsCausesStatusFolioAdminService.ChangeStateConceptsCausesStatusFolio

        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        If String.IsNullOrEmpty(state) Then
            Throw New ArgumentNullException("state")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim ConceptsCausesStatusFolio As ConceptsCausesStatusFolio = Me._ConceptsCausesStatusFolioRepository.GetConceptsCausesStatusFolioById(Id)
            If ConceptsCausesStatusFolio IsNot Nothing AndAlso ConceptsCausesStatusFolio.Id > 0 Then
                ConceptsCausesStatusFolio.Status = state
            End If
            Dim result = Me.SaveConceptsCausesStatusFolio(ConceptsCausesStatusFolio, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ConceptsCausesStatusFolio) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' eliminar
    ''' </summary>
    ''' <param name="ConceptsCausesStatusFolio">The portfolio note concept.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">EconomicIndicator Vacio</exception>
    Public Function DeleteConceptsCausesStatusFolio(ConceptsCausesStatusFolio As ConceptsCausesStatusFolio, audit As AuditMessage) As ActionResult(Of ConceptsCausesStatusFolio) Implements IConceptsCausesStatusFolioAdminService.DeleteConceptsCausesStatusFolio

        If ConceptsCausesStatusFolio Is Nothing Then
            Throw New ArgumentNullException("invoiceCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._ConceptsCausesStatusFolioRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                ConceptsCausesStatusFolio.ModificationUser = audit.CodeUser
                ConceptsCausesStatusFolio.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of ConceptsCausesStatusFolio)(ConceptsCausesStatusFolio, audit, status)
                ConceptsCausesStatusFolio.MarkAsDeleted()
                Me._ConceptsCausesStatusFolioRepository.SaveEntity(ConceptsCausesStatusFolio)
                unitOfWork.Commit()
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult(Of ConceptsCausesStatusFolio) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of ConceptsCausesStatusFolio) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-999"}), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of ConceptsCausesStatusFolio) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of ConceptsCausesStatusFolio) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ConceptsCausesStatusFolio) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
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
            _ConceptsCausesStatusFolioRepository = Nothing
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
