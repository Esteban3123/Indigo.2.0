'***********************************************************************
' Assembly         : Application.FixedAsset
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 10-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Domain.Base
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports System.Data.Entity.Core
Imports System.Data.Entity.Infrastructure
Imports System.Transactions
Imports Infrastructure.CrossCutting.Resources

#End Region
Public Class StatementFolioAdminService
    Implements IStatementFolioAdminService

    Private Const FORM_NAME As String = "FrmStatementFolio"

    Private _statementRepository As IStatementFolioRepository

    Private _sequenseAccountingDRepository As ISequenseAccountingDRepository

#Region "Builder"
    Public Sub New(ByVal statementRepository As IStatementFolioRepository, ByVal sequenseRepository As ISequenseAccountingDRepository)
        If statementRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio vacio")
        End If
        If sequenseRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseRepository")
        End If
        _statementRepository = statementRepository
        _sequenseAccountingDRepository = sequenseRepository
    End Sub
#End Region

#Region "Implements"
    ''' <summary>
    ''' Deletes the statement folio.
    ''' </summary>
    ''' <param name="statementfolio">The address.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function DeleteStatementFolio(statementfolio As AttachedDeclarations, audit As AuditMessage) As ActionResult Implements IStatementFolioAdminService.DeleteStatementFolio
        If statementfolio Is Nothing Then
            Throw New ArgumentNullException("statementfolio")
        End If
        Dim UnitOfWork As IUnitWork = _statementRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                statementfolio.ModificationDate = DateTime.Now
                statementfolio.ModificationUser = audit.CodeUser
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of AttachedDeclarations)(statementfolio, audit, status)
                _statementRepository.DeleteEntity(statementfolio)
                UnitOfWork.Commit()
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult With {.StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted"), .StateResult = True}
            End Using
        Catch ex As OptimisticConcurrencyException
            UnitOfWork.RollbackChanges()
            Return New ActionResult With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorConcurrence"), .StateResult = False}
        Catch ex As UpdateException
            UnitOfWork.RollbackChanges()
            Return New ActionResult With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorDependence"), .StateResult = False}
        Catch ex As DbUpdateException
            UnitOfWork.RollbackChanges()
            Return New ActionResult With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorDependence"), .StateResult = False}
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex), .StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Gets the statement folio by code.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Public Function GetStatementFolioByCode(code As String, Optional tracking As Boolean = True) As AttachedDeclarations Implements IStatementFolioAdminService.GetStatementFolioByCode
        Return _statementRepository.GetStatementFoliobyCode(code, tracking)
    End Function

    ''' <summary>
    ''' Gets the statement folio by identifier.
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Public Function GetStatementFolioById(id As Integer, Optional tracking As Boolean = True) As AttachedDeclarations Implements IStatementFolioAdminService.GetStatementFolioById
        Return _statementRepository.GetStatementFoliobyId(id, tracking)
    End Function

    ''' <summary>
    ''' Saves the statement folio.
    ''' </summary>
    ''' <param name="statementfolio">The statementfolio.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function SaveStatementFolio(statementfolio As AttachedDeclarations, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of AttachedDeclarations) Implements IStatementFolioAdminService.SaveStatementFolio
        If statementfolio Is Nothing Then
            Throw New ArgumentNullException("statementfolio")
        End If
        Dim statementfolioUnitOfWork As IUnitWork = _statementRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequenseAccountingDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty
                Dim seq As GeneralLedgerSequenceDetail = Nothing
                If statementfolio.Code Is Nothing OrElse statementfolio.Code.Trim().Equals(String.Empty) Then
                    seq = Me._sequenseAccountingDRepository.GetSequenseDetailUpdatedById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.GeneralLedgerSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            statementfolio.Code = res
                            seq.Next += 1
                            Me._sequenseAccountingDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of AttachedDeclarations) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.GeneralLedgerSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), statementfolio.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of AttachedDeclarations) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As AttachedDeclarations = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of AttachedDeclarations)
                Dim status As Integer

                If statementfolio.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    statementfolio.CreationUser = audit.CodeUser
                    statementfolio.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = statementfolio.OriginalValue
                    statementfolio.ModificationUser = audit.CodeUser
                    statementfolio.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._statementRepository.SaveEntity(statementfolio)
                statementfolioUnitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of AttachedDeclarations)(statementfolio, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                statementfolio.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of AttachedDeclarations) With {.StatusCode = eStatusResult.SUCCESS, .StateResult = True, .ObjectEmbbeded = statementfolio, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            statementfolioUnitOfWork.RollbackChanges()
            Return New ActionResult(Of AttachedDeclarations) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            statementfolioUnitOfWork.RollbackChanges()
        IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of AttachedDeclarations) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function ChangeStateStatementFolio(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of AttachedDeclarations) Implements IStatementFolioAdminService.ChangeStateStatementFolio
        Dim statementFolio As AttachedDeclarations = GetStatementFolioByCode(code)
        statementFolio.Status = state
        Return SaveStatementFolio(statementFolio, audit)
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _statementRepository = Nothing
            _sequenseAccountingDRepository = Nothing
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
