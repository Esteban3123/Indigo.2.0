'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Andrés Steven Rojas
' Created          : 31/10/2025
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

Public Class CardCollectionsAdminService
    Implements ICardCollectionsAdminService

#Region "Fields"

    ''' <summary>
    ''' Nombre del formulario
    ''' </summary>
    Private Const FORM_NAME As String = "FrmCardCollections"

    ''' <summary>
    ''' Repositorio de recaudo de tarjetas
    ''' </summary>
    Private _cardCollectionsRepository As ICardCollectionsRepository

    ''' <summary>
    ''' Repositorio de las secuencias
    ''' </summary>
    Private _secuenseDRepository As ISequenseTreasuryDRepository

#End Region

#Region "Methods"

    Public Sub New(ByVal cardCollectionsRepository As ICardCollectionsRepository, ByVal secuenseDRepository As ISequenseTreasuryDRepository)
        If cardCollectionsRepository Is Nothing Then
            Throw New ArgumentNullException("cardCollectionsRepository")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _cardCollectionsRepository = cardCollectionsRepository
        _secuenseDRepository = secuenseDRepository
    End Sub

    ''' <summary>
    ''' Obtiene un registro de recaudo de tarjetas por código
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">
    ''' code
    ''' or
    ''' audit
    ''' </exception>
    Public Function GetCardCollections(code As String, audit As AuditMessage) As ActionResult(Of CardCollections) Implements ICardCollectionsAdminService.GetCardCollections
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim cardCollections As CardCollections = Me._cardCollectionsRepository.GetCardCollections(code.Trim())
            If cardCollections IsNot Nothing AndAlso cardCollections.Id > 0 Then
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Print
                Dim auditProcess As New IndigoAuditSimpleEntity(Of CardCollections)(cardCollections, audit, status)
                auditProcess.Execute()
            End If
            Return New ActionResult(Of CardCollections) With {.StateResult = True, .ObjectEmbbeded = cardCollections}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CardCollections) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Guarda un registro de recaudo de tarjetas
    ''' </summary>
    ''' <param name="cardCollections">The card collections.</param>
    ''' <param name="audit">The audit.</param>
    ''' <param name="idSequence">The identifier sequence.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">cardCollections</exception>
    Public Function SaveCardCollections(cardCollections As CardCollections, audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of CardCollections) Implements ICardCollectionsAdminService.SaveCardCollections

        If cardCollections Is Nothing Then
            Throw New ArgumentNullException("cardCollections")
        End If
        Dim unitOfWork As IUnitWork = Me._cardCollectionsRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(cardCollections.Code) Then
                    Dim seq As TreasurySequenceDetail = Me._secuenseDRepository.GetSequenseDById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.TreasurySequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            cardCollections.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of CardCollections) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.TreasurySequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), cardCollections.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of CardCollections) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As CardCollections = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of CardCollections)
                Dim status As Integer

                If cardCollections.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    cardCollections.CreationUser = audit.CodeUser
                    cardCollections.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                ElseIf cardCollections.ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted Then
                    ' Marcar detalles como eliminados
                    While cardCollections.CardCollectionDetails.Count > 0
                        cardCollections.CardCollectionDetails(cardCollections.CardCollectionDetails.Count - 1).MarkAsDeleted()
                    End While
                    cardCollections.ModificationUser = audit.CodeUser
                    cardCollections.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Delete
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = cardCollections.OriginalValue
                    cardCollections.ModificationUser = audit.CodeUser
                    cardCollections.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._cardCollectionsRepository.SaveEntity(cardCollections)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of CardCollections)(cardCollections, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                cardCollections.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of CardCollections) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = cardCollections, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of CardCollections) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of CardCollections) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-000"}.ToList(), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of CardCollections) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-000"}.ToList(), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CardCollections) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un registro de recaudo de tarjetas por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetCardCollectionsById(id As Integer) As CardCollections Implements ICardCollectionsAdminService.GetCardCollectionsById
        Try
            Return _cardCollectionsRepository.GetCardCollectionsById(id)
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
            _cardCollectionsRepository = Nothing
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


