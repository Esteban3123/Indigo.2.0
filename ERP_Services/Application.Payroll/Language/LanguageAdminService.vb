'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 25-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Transactions
Imports Infrastructure.CrossCutting.Resources
Imports System.Data.Entity.Core

Public Class LanguageAdminService
    Implements ILanguageAdminService

    Private _languageRepository As ILanguageRepository

    Private _secuenseDRepository As Domain.Entities.IPayrollSequenceDetailRepository

    Private Const FORM_NAME As String = "Idiomas"

    Public Sub New(ByVal languageRepository As ILanguageRepository, secuenseDRepository As Domain.Entities.IPayrollSequenceDetailRepository)
        If languageRepository Is Nothing Then
            Throw New ArgumentNullException("languageRepository Vacio")
        End If
        _languageRepository = languageRepository
        _secuenseDRepository = secuenseDRepository
    End Sub

    ''' <summary>
    ''' Elimina un idioma
    ''' </summary>
    ''' <param name="language">Idioma</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteLanguage(language As Language, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionMessageResult(Of Language) Implements ILanguageAdminService.DeleteLanguage
        If language Is Nothing Then
            Throw New ArgumentNullException("Language Vacio")
        End If
        Dim UnitOfWork As IUnitWork = _languageRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                language.ModificationUser = audit.CodeUser
                language.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of Language)(language, audit, status)

                language.MarkAsDeleted()
                Me._languageRepository.SaveEntity(language)
                UnitOfWork.Commit()
                auditProcess.Execute()
                scope.Complete()
                Return New ActionMessageResult(Of Language) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionMessageResult(Of Language) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of MessageResult)({New MessageResult("-999", language.Code)}), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionMessageResult(Of Language) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of MessageResult)({New MessageResult("c-0000", language.Code)}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionMessageResult(Of Language) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of MessageResult)({New MessageResult("c-0000", language.Code)}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionMessageResult(Of Language) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un idioma especifico
    ''' </summary>
    ''' <param name="code">Codigo del idioma</param>
    ''' <returns>Idioma</returns>
    ''' <remarks></remarks>
    Public Function GetLanguage(code As String) As Language Implements ILanguageAdminService.GetLanguage
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Code Vacio")
        End If
        Try
            Return _languageRepository.GetLanguage(code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New Language()
        End Try
    End Function

    ''' <summary>
    ''' Lista todos los idiomas
    ''' </summary>
    ''' <returns>Lista de idiomas</returns>
    ''' <remarks></remarks>
    Public Function ListAllLanguage() As List(Of Language) Implements ILanguageAdminService.ListAllLanguage
        Try
            Return _languageRepository.ListAllLanguage()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Graba o Actualiza un idioma
    ''' </summary>
    ''' <param name="language">Idioma</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveLanguage(language As Language, audit As Infrastructure.CrossCutting.Base.AuditMessage, Optional idSequence As Long = 0) As ActionResult(Of Language) Implements ILanguageAdminService.SaveLanguage
        If language Is Nothing Then
            Throw New ArgumentNullException("Language Vacio")
        End If
        Dim UnitOfWork As IUnitWork = _languageRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                Dim MessageResult As String = String.Empty

                If language.Code Is Nothing OrElse language.Code.Trim().Equals(String.Empty) Then
                    Dim seq As Domain.Entities.PayrollSequenceDetail = Me._secuenseDRepository.GetSequenseDetailUpdatedById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PayrollSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            language.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of Language) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.PayrollSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), language.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of Language) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If


                Dim auditProcess As IndigoAuditSimpleEntity(Of Language)
                Dim auxLanguage As Language = Nothing
                Dim status As Integer

                If language.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    language.ModificationUser = audit.CodeUser
                    language.ModificationDate = Date.Now()
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                    auxLanguage = _languageRepository.GetLanguage(language.Code, False)
                Else
                    language.CreationUser = audit.CodeUser
                    language.CreationDate = Date.Now()
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                End If

                'Valido si se va a guardar o a eliminar
                _languageRepository.SaveEntity(language)
                UnitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of Language)(language, audit, status, auxLanguage)
                auditProcess.Execute()
                language.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of Language) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = language, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            UnitOfWork.RollbackChanges()
            Return New ActionResult(Of Language) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Language) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function UpdateStateLanguage(code As String, state As Boolean, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult(Of Language) Implements ILanguageAdminService.UpdateStateLanguage
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
            Dim language As Language = Me._languageRepository.GetLanguage(code.Trim())
            If language IsNot Nothing AndAlso language.Id > 0 Then
                language.State = state
            End If
            Dim result = Me.SaveLanguage(language, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Language) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _languageRepository = Nothing
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
