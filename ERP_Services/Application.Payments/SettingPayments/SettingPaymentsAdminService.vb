'***********************************************************************
' Assembly         : Application.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 01/04/2014
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

Public Class SettingPaymentsAdminService
    Implements ISettingPaymentsAdminService

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _settingPaymentsRepository As ISettingPaymentsRepository

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal settingPaymentsRepository As ISettingPaymentsRepository)
        If settingPaymentsRepository Is Nothing Then
            Throw New ArgumentNullException("settingPaymentsRepository Vacio")
        End If
        _settingPaymentsRepository = settingPaymentsRepository
    End Sub

    ''' <summary>
    ''' Elimina un parametro de pago
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteSettingPayments(settingPayments As SettingPayments, audit As AuditMessage) As ActionResult Implements ISettingPaymentsAdminService.DeleteSettingPayments
        If settingPayments Is Nothing Then
            Throw New ArgumentNullException("settingPayments")
        End If
        Dim unitOfWork As IUnitWork = Me._settingPaymentsRepository.UnitWork
        Try
            If settingPayments.ChangeTracker.State = ObjectState.Deleted Then
                Me._settingPaymentsRepository.DeleteEntity(settingPayments)
                unitOfWork.Commit()
                'Auditoria básica
                IndigoAuditBasic.Execute(GetType(SettingPayments).Name, audit.Functional, settingPayments.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, Infrastructure.CrossCutting.Base.ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
                'Ausitoria avanzada
                Dim auditObject As New IndigoAuditSimpleEntity(Of SettingPayments)(settingPayments, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
                auditObject.Execute()
                Return New ActionResult With {.StateResult = True}
            Else
                Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
            End If
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza un parametro de pago
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveSettingPayments(settingPayments As SettingPayments, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of SettingPayments) Implements ISettingPaymentsAdminService.SaveSettingPayments
        If settingPayments Is Nothing Then
            Throw New ArgumentNullException("settingPayments")
        End If
        Dim unitOfWork As IUnitWork = Me._settingPaymentsRepository.UnitWork
        Try

            Dim auxSettingPayments As SettingPayments = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of SettingPayments)
            Dim status As Integer

            If settingPayments.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                settingPayments.CreationUser = audit.CodeUser
                settingPayments.CreationDate = DateTime.Now
                For Each item In settingPayments.AgesPayments
                    item.CreationDate = DateTime.Now
                    item.CreationUser = audit.CodeUser
                Next
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                auxSettingPayments = _settingPaymentsRepository.GetSettingPaymentsByIdOperatingUnit(settingPayments.IdOperatingUnit)
                settingPayments.ModificationUser = audit.CodeUser
                settingPayments.ModificationDate = DateTime.Now
                For Each item In settingPayments.AgesPayments
                    If item.ChangeTracker.State = ObjectState.Modified Then
                        item.ModificationDate = DateTime.Now
                        item.ModificationUser = audit.CodeUser
                        item.MarkAsModified()
                    ElseIf item.ChangeTracker.State = ObjectState.Added Then
                        item.CreationDate = DateTime.Now
                        item.CreationUser = audit.CodeUser
                    End If
                Next
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            End If

            Me._settingPaymentsRepository.SaveEntity(settingPayments)
            unitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of SettingPayments)(settingPayments, audit, status, auxSettingPayments)
            auditProcess.Execute()
            'Se marca la entidad como sin cambios
            settingPayments.MarkAsUnchanged()

            Return New ActionResult(Of SettingPayments) With {.StateResult = True, .ObjectEmbbeded = settingPayments}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of SettingPayments) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of SettingPayments) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeState(id As Integer, state As Boolean, audit As AuditMessage) As ActionResult(Of SettingPayments) Implements ISettingPaymentsAdminService.ChangeState
        Dim settingPayments As SettingPayments = _settingPaymentsRepository.GetSettingPaymentsByIdOperatingUnit(id, True)
        settingPayments.State = state
        Return SaveSettingPayments(settingPayments, audit)
    End Function

    ''' <summary>
    ''' Concepto por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSettingPaymentsById(id As Integer, audit As AuditMessage) As ActionResult(Of SettingPayments) Implements ISettingPaymentsAdminService.GetSettingPaymentsById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim settingPayments As SettingPayments = Me._settingPaymentsRepository.GetSettingPaymentsByIdOperatingUnit(id)
            If settingPayments IsNot Nothing AndAlso settingPayments.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of SettingPayments)(settingPayments, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of SettingPayments) With {.StateResult = True, .ObjectEmbbeded = settingPayments}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of SettingPayments) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _settingPaymentsRepository = Nothing
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
