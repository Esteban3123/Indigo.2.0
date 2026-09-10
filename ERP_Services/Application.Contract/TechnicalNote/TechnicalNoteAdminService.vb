#Region "Imports"

Imports System.Data.Entity.Core
Imports System.Data.Entity.Infrastructure
Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions

#End Region

Public Class TechnicalNoteAdminService
    Implements ITechnicalNoteAdminService

#Region "Builder"

    Private _TechnicalNoteRepository As ITechnicalNoteRepository
    Private _secuenseContractDetailRepository As ISequenseContractDRepository

    Public Sub New(ByVal TechnicalNoteRepository As ITechnicalNoteRepository,
                   ByVal secuenseContractDetailRepository As ISequenseContractDRepository)
        _TechnicalNoteRepository = TechnicalNoteRepository
        _secuenseContractDetailRepository = secuenseContractDetailRepository
    End Sub

#End Region

#Region "Methods"

    Public Function GetTechnicalNoteById(id As Integer, audit As AuditMessage) As ActionResult(Of TechnicalNote) Implements ITechnicalNoteAdminService.GetTechnicalNoteById
        Try
            Dim TechnicalNote As TechnicalNote = Me._TechnicalNoteRepository.GetTechnicalNoteById(id)
            If TechnicalNote IsNot Nothing AndAlso TechnicalNote.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of TechnicalNote)(TechnicalNote, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of TechnicalNote) With {.StateResult = True, .ObjectEmbbeded = TechnicalNote}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of TechnicalNote) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    Public Function GetTechnicalNote(code As String, audit As AuditMessage) As ActionResult(Of TechnicalNote) Implements ITechnicalNoteAdminService.GetTechnicalNote
        Try
            Dim TechnicalNote As TechnicalNote = Me._TechnicalNoteRepository.GetTechnicalNote(code.Trim())
            If TechnicalNote IsNot Nothing AndAlso TechnicalNote.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of TechnicalNote)(TechnicalNote, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of TechnicalNote) With {.StateResult = True, .ObjectEmbbeded = TechnicalNote}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of TechnicalNote) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    Public Function SaveTechnicalNote(TechnicalNote As TechnicalNote, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of TechnicalNote) Implements ITechnicalNoteAdminService.SaveTechnicalNote
        Dim unitOfWork As IUnitWork = Me._TechnicalNoteRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseContractDetailRepository.UnitWork
        Try
            If TechnicalNote.Code Is Nothing OrElse TechnicalNote.Code.Trim().Equals(String.Empty) Then
                Dim seq = Me._secuenseContractDetailRepository.GetSequenseDById(idSequense)
                If seq Is Nothing OrElse seq.Id = 0 Then
                    Return New ActionResult(Of TechnicalNote) With {.StateResult = False, .MessageResult = {"No tiene secuencia numérica parametrizada."}.ToList()}
                End If

                If seq.ContractSequence.Sequential Then
                    Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                    If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                        TechnicalNote.Code = res
                        seq.Next += 1
                        Me._secuenseContractDetailRepository.SaveEntity(seq)
                    Else
                        Return New ActionResult(Of TechnicalNote) With {.StateResult = False, .MessageResult = {"La secuencia numérica ya excedió el limite permitido."}.ToList()}
                    End If
                End If
            End If

            Dim auxRateManual As TechnicalNote = Nothing
            Dim status As Integer

            If TechnicalNote.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                TechnicalNote.CreationUser = audit.CodeUser
                TechnicalNote.CreationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                auxRateManual = TechnicalNote.OriginalValue
                TechnicalNote.ModificationUser = audit.CodeUser
                TechnicalNote.ModificationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            End If

            Me._TechnicalNoteRepository.SaveEntity(TechnicalNote)
            unitOfWork.Commit()
            sequenseUnitOfWork.Commit()

            Dim auditProcess = New IndigoAuditSimpleEntity(Of TechnicalNote)(TechnicalNote, audit, status, auxRateManual)
            auditProcess.Execute()

            'Se marca la entidad como sin cambios
            TechnicalNote.MarkAsUnchanged()

            Return New ActionResult(Of TechnicalNote) With {.StateResult = True, .ObjectEmbbeded = TechnicalNote}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of TechnicalNote) With {.StateResult = False, .MessageResult = {"-111"}.ToList(), .Message = "No se puede insertar porque existe código duplicado: " + TechnicalNote.Code}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of TechnicalNote) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of TechnicalNote) With {.StateResult = False, .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList}
        End Try
    End Function

    Public Function ChangeStateTechnicalNote(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of TechnicalNote) Implements ITechnicalNoteAdminService.ChangeStateTechnicalNote
        Dim TechnicalNote As TechnicalNote = _TechnicalNoteRepository.GetTechnicalNote(code)
        TechnicalNote.Status = state
        Return SaveTechnicalNote(TechnicalNote, audit)
    End Function

#End Region

#Region "IDisposable Support"

    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            _TechnicalNoteRepository = Nothing
            _secuenseContractDetailRepository = Nothing
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
