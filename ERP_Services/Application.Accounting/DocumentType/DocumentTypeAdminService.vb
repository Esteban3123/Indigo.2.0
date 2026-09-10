'***********************************************************************
' Assembly         : Domain.Seedwork
' Author           : Juan F. Tamayo
' Created          : 2014-01-15
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Application.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base
Imports System.Data.Entity.Core
Imports System.Data.Entity.Infrastructure
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions

#End Region

''' <summary>
''' Gestiona los servicios disponibles para todas las operaciones
''' con la entidad tipo de documento
''' </summary>
Public Class DocumentTypeAdminService
    Implements IDocumentTypeAdminService

#Region "Fields"
    Private Const FORM_NAME As String = "FrmDocumentType
"
    ''' <summary>
    ''' Repositorio de la entidad tipo de documentos
    ''' </summary>
    Private _repository As IDocumentTypeRepository

    ''' <summary>
    ''' repositorio de las secuencias
    ''' </summary>
    Private _secuenseDRepository As ISequenseAccountingDRepository

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="repository">Repositorio de la entidad tipo de documento</param>
    Public Sub New(ByVal repository As IDocumentTypeRepository, ByVal secuenseDRepository As ISequenseAccountingDRepository)
        If repository Is Nothing Then
            Throw New ArgumentNullException("repository")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        Me._repository = repository
        _secuenseDRepository = secuenseDRepository
    End Sub

#End Region

#Region "Implements"

    ''' <summary>
    ''' Elimina un tipo de documento
    ''' </summary>
    ''' <param name="doc">Tipo de documento a eliminar</param>
    ''' <param name="audit">Mensaje de auditoria</param>
    ''' <returns>Resultado de la acción</returns>
    Public Async Function DeleteDocumentType(doc As JournalVoucherTypes, audit As AuditMessage) As Task(Of ActionResult) Implements IDocumentTypeAdminService.DeleteDocumentType
        If doc Is Nothing Then
            Throw New ArgumentNullException("invoiceCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._repository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required,
                                                New TransactionOptions() With {
                                                .Timeout = TransactionManager.MaximumTimeout,
                                                .IsolationLevel = IsolationLevel.ReadCommitted},
                                                TransactionScopeAsyncFlowOption.Enabled)
                doc.ModificationUser = audit.CodeUser
                doc.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of JournalVoucherTypes)(doc, audit, status)
                Await _secuenseDRepository.DeleteAllSequencesForDocumentTypeAsync(doc)
                doc.MarkAsDeleted()
                _repository.SaveEntity(doc)
                unitOfWork.Commit()
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-999"}), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Updates the state
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Async Function UpdateStateDocumentType(code As String, state As Boolean, audit As AuditMessage) As Task(Of ActionResult(Of JournalVoucherTypes)) Implements IDocumentTypeAdminService.UpdateStateDocumentType
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
            Dim documentType As JournalVoucherTypes = Await Me._repository.GetDocumentTypeAsync(code.Trim())
            If documentType IsNot Nothing AndAlso documentType.Id > 0 Then
                documentType.Status = state
            End If
            Dim result = Await Me.SaveDocumentType(documentType, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of JournalVoucherTypes) With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex), .StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un tipo de documento por su código
    ''' </summary>
    ''' <param name="name">Código del documento a consultar</param>
    ''' <param name="audit">Mensaje de auditoria</param>
    ''' <returns>Tipo de documento consultado</returns>
    Public Async Function GetDocumentType(name As String, audit As AuditMessage) As Task(Of JournalVoucherTypes) Implements IDocumentTypeAdminService.GetDocumentType
        If String.IsNullOrEmpty(name) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim doc As JournalVoucherTypes = Await Me._repository.GetDocumentTypeAsync(name.Trim())
            If doc IsNot Nothing AndAlso doc.Id > 0 Then
                Dim auditProcess As New IndigoAuditSimpleEntity(Of JournalVoucherTypes)(doc, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditProcess.Execute()
            End If
            Return doc
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Graba un tipo de documento
    ''' </summary>
    ''' <param name="audit">Mensaje de auditoria</param>
    ''' <returns>Resultado de la acción</returns>
    Public Async Function SaveDocumentType(documentType As JournalVoucherTypes, audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As Task(Of ActionResult(Of JournalVoucherTypes)) Implements IDocumentTypeAdminService.SaveDocumentType
        If documentType Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._repository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {
                                                .Timeout = TransactionManager.MaximumTimeout,
                                                .IsolationLevel = IsolationLevel.ReadCommitted
                                                },
                                                TransactionScopeAsyncFlowOption.Enabled)

                Dim MessageResult As String = String.Empty

                If documentType.Code Is Nothing OrElse documentType.Code.Trim().Equals(String.Empty) Then
                    Dim seq As GeneralLedgerSequenceDetail = Me._secuenseDRepository.GetSequenseDetailUpdatedById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.GeneralLedgerSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            documentType.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of JournalVoucherTypes) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.GeneralLedgerSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), documentType.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of JournalVoucherTypes) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As JournalVoucherTypes = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of JournalVoucherTypes)
                Dim status As Integer

                If documentType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    documentType.CreationUser = audit.CodeUser
                    documentType.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = documentType.OriginalValue
                    documentType.ModificationUser = audit.CodeUser
                    documentType.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Await ProcessMovementSecuence(documentType, True)
                Me._repository.SaveEntity(documentType)
                unitOfWork.Commit()

                If documentType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    Await _secuenseDRepository.CreateSequencesForNewDocumentTypeAsync(documentType)
                Else
                    Await ProcessMovementSecuence(documentType, False)
                End If
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of JournalVoucherTypes)(documentType, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                documentType.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of JournalVoucherTypes) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = documentType, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of JournalVoucherTypes) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of JournalVoucherTypes) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Async Function ProcessMovementSecuence(documentType As JournalVoucherTypes, Optional IsDelete As Boolean = False) As Task
        If documentType.JournalVoucherTypeConsecutive Is Nothing Then Exit Function
        If IsDelete Then
            For Each item In documentType.JournalVoucherTypeConsecutive
                If item.ChangeTracker.State = ObjectState.Deleted Then
                    Await _secuenseDRepository.DeleteSequenceForDocumentTypeAsync(item)
                End If
            Next
            Exit Function
        End If
        For Each item In documentType.JournalVoucherTypeConsecutive
            Await _secuenseDRepository.CreateSequenceForDocumentTypeAsync(item)
        Next
    End Function

    ''' <summary>
    ''' Obtiene un tipo de documento por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetJournalVoucherById(id As Long, audit As AuditMessage) As ActionResult(Of JournalVoucherTypes) Implements IDocumentTypeAdminService.GetJournalVoucherById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim jorunalVoucherType As JournalVoucherTypes = Me._repository.GetJournalVoucherById(id)
            Return New ActionResult(Of JournalVoucherTypes) With {.StateResult = True, .ObjectEmbbeded = jorunalVoucherType}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of JournalVoucherTypes) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
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
            _repository = Nothing
            _secuenseDRepository = Nothing
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
