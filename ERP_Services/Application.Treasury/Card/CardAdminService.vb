'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Libraries"
Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
Imports System.Transactions
Imports Infrastructure.CrossCutting.Resources

#End Region

Public Class CardAdminService
    Implements ICardAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de tarjetas
    ''' </summary>
    Private Const FORM_NAME As String = "FrmCards"

    ''' <summary>
    ''' Repositorio de tarjetas
    ''' </summary>
    Private _cardRepository As ICardRepository

    ''' <summary>
    ''' repositorio de las secuencias
    ''' </summary>
    Private _secuenseDRepository As ISequenseTreasuryDRepository

#End Region

#Region "Methods"

    Public Sub New(ByVal cardRepository As ICardRepository, ByVal secuenseDRepository As ISequenseTreasuryDRepository)
        If cardRepository Is Nothing Then
            Throw New ArgumentNullException("TreasuryRepository")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _cardRepository = cardRepository
        _secuenseDRepository = secuenseDRepository
    End Sub

    ''' <summary>
    ''' Deletes the card.
    ''' </summary>
    ''' <param name="card">The card.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">bankCity</exception>
    Public Function DeleteCard(card As Cards, audit As AuditMessage) As ActionResult Implements ICardAdminService.DeleteCard



        If card Is Nothing Then
            Throw New ArgumentNullException("invoiceCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._cardRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                card.ModificationUser = audit.CodeUser
                card.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of Cards)(card, audit, status)

                While card.CardCostCenter.Count > 0
                    card.CardCostCenter(card.CardCostCenter.Count - 1).MarkAsDeleted()
                End While
                card.MarkAsDeleted()
                Me._cardRepository.SaveEntity(card)
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
    ''' Updates the state card.
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function UpdateStateCard(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of Cards) Implements ICardAdminService.UpdateStateCard


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
            Dim invoiceCategories As Cards = Me._cardRepository.GetCard(code.Trim())
            If invoiceCategories IsNot Nothing AndAlso invoiceCategories.Id > 0 Then
                invoiceCategories.Status = state
            End If
            Dim result = Me.SaveCard(invoiceCategories, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Cards) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Gets the card.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">
    ''' code
    ''' or
    ''' audit
    ''' </exception>
    Public Function GetCard(code As String, audit As AuditMessage) As ActionResult(Of Cards) Implements ICardAdminService.GetCard
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim card As Cards = Me._cardRepository.GetCard(code.Trim())
            If card IsNot Nothing AndAlso card.Id > 0 Then
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Print
                Dim auditProcess As New IndigoAuditSimpleEntity(Of Cards)(card, audit, status)
                auditProcess.Execute()
            End If
            Return New ActionResult(Of Cards) With {.StateResult = True, .ObjectEmbbeded = card}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Cards) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Saves the card.
    ''' </summary>
    ''' <param name="card">The card.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">bankCity</exception>
    Public Function SaveCard(card As Cards, audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of Cards) Implements ICardAdminService.SaveCard

        If card Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._cardRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(card.Code) Then
                    Dim seq As TreasurySequenceDetail = Me._secuenseDRepository.GetSequenseDById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.TreasurySequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            card.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of Cards) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.TreasurySequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), card.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of Cards) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As Cards = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of Cards)
                Dim status As Integer

                If card.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    card.CreationUser = audit.CodeUser
                    card.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = card.OriginalValue
                    card.ModificationUser = audit.CodeUser
                    card.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._cardRepository.SaveEntity(card)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of Cards)(card, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                card.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of Cards) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = card, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of Cards) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Cards) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' metodo para obtener una tarjeta por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetCardById(id As Integer) As Cards Implements ICardAdminService.GetCardById
        Try
            Return _cardRepository.GetCardById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
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
            _cardRepository = Nothing
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
