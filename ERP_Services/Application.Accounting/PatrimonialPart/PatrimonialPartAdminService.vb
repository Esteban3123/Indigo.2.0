'***********************************************************************
' Assembly         : Application.Accounting
' Author           : Diego Andrés Roldán Lozano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core

Public Class PatrimonialPartAdminService
    Implements IPatrimonialPartAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de tarjetas
    ''' </summary>
    Private _repository As IPatrimonialPartRepository

    ''' <summary>
    ''' repositorio de las secuencias
    ''' </summary>
    Private _secuenseDRepository As ISequenseAccountingDRepository

#End Region

    Public Sub New(ByVal repository As IPatrimonialPartRepository, ByVal secuenseDRepository As ISequenseAccountingDRepository)
        If repository Is Nothing Then
            Throw New ArgumentNullException("repository")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _repository = repository
        _secuenseDRepository = secuenseDRepository
    End Sub

    ''' <summary>
    ''' Deletes the card.
    ''' </summary>
    ''' <param name="patrimonialPart">The patrimonial part.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">bankCity</exception>
    Public Function DeletePatrimonialPart(patrimonialPart As Shareholding, audit As AuditMessage) As ActionResult Implements IPatrimonialPartAdminService.DeletePatrimonialPart
        If patrimonialPart Is Nothing Then
            Throw New ArgumentNullException("bankCity")
        End If
        Dim unitOfWork As IUnitWork = Me._repository.UnitWork
        Try
            patrimonialPart.ModificationDate = DateTime.Now
            patrimonialPart.ModificationUser = audit.CodeUser
            Dim auditProcess As New IndigoAuditSimpleEntity(Of Shareholding)(patrimonialPart, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._repository.DeleteEntity(patrimonialPart)
            unitOfWork.Commit()
            auditProcess.Execute()
            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Updates the state card.
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function UpdateStatePatrimonialPart(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of Shareholding) Implements IPatrimonialPartAdminService.UpdateStatePatrimonialPart
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
            Dim patrimonialPart As Shareholding = Me._repository.GetPatrimonialPart(code.Trim())
            If patrimonialPart IsNot Nothing AndAlso patrimonialPart.Id > 0 Then
                patrimonialPart.Status = state
            End If
            Return Me.SavePatrimonialPart(patrimonialPart, audit)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Shareholding) With {.StateResult = False}
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
    Public Function GetPatrimonialPart(code As String, audit As AuditMessage) As Shareholding Implements IPatrimonialPartAdminService.GetPatrimonialPart
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim patrimonialPart As Shareholding = Me._repository.GetPatrimonialPart(code.Trim())
            If patrimonialPart IsNot Nothing AndAlso patrimonialPart.Id > 0 Then
                Dim auditProcess As New IndigoAuditSimpleEntity(Of Shareholding)(patrimonialPart, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditProcess.Execute()
            End If
            Return patrimonialPart
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Saves the patrimonial part.
    ''' </summary>
    ''' <param name="patrimonialPart">The patrimonial part.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">bankCity</exception>
    Public Function SavePatrimonialPart(patrimonialPart As Shareholding, audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of Shareholding) Implements IPatrimonialPartAdminService.SavePatrimonialPart
        If patrimonialPart Is Nothing Then
            Throw New ArgumentNullException("patrimonialPart")
        End If
        Dim unitOfWork As IUnitWork = Me._repository.UnitWork
        Dim unitWorkSequence As IUnitWork = Me._secuenseDRepository.UnitWork

        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of Shareholding)
            Dim status As Integer
            Dim auxPatrimonialPart As Shareholding = Nothing

            If String.IsNullOrEmpty(patrimonialPart.Code) Then
                Dim seq As GeneralLedgerSequenceDetail = _secuenseDRepository.GetSequenseDById(idSequence)
                If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.GeneralLedgerSequence.Sequential Then
                    Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                    If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                        patrimonialPart.Code = res
                        seq.Next += 1
                        Me._secuenseDRepository.SaveEntity(seq)
                    Else
                        Return New ActionResult(Of Shareholding) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                    End If
                Else
                    Return New ActionResult(Of Shareholding) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                End If
            End If


            If patrimonialPart.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                patrimonialPart.CreationDate = DateTime.Now
                patrimonialPart.CreationUser = audit.CodeUser
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                patrimonialPart.ModificationDate = DateTime.Now
                patrimonialPart.ModificationUser = audit.CodeUser
                status = Infrastructure.CrossCutting.Audit.Actions.Update
                auxPatrimonialPart = patrimonialPart.OriginalValue
            End If

            Me._repository.SaveEntity(patrimonialPart)
            unitOfWork.Commit()
            unitWorkSequence.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of Shareholding)(patrimonialPart, audit, status, auxPatrimonialPart)
            auditProcess.Execute()

            'Se marca la entidad como sin cambios
            patrimonialPart.MarkAsUnchanged()

            Return New ActionResult(Of Shareholding) With {.StateResult = True, .ObjectEmbbeded = patrimonialPart}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of Shareholding) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Shareholding) With {.StateResult = False}
        End Try
    End Function

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
