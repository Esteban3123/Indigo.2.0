'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 26-06-2013
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

Public Class WorkCenterAdminService
    Implements IWorkCenterAdminService

    Private _workCenterRepository As IWorkCenterRepository

    Public Sub New(ByVal workCenterRepository As IWorkCenterRepository)
        If workCenterRepository Is Nothing Then
            Throw New ArgumentNullException("workCenterRepository Vacio")
        End If
        _workCenterRepository = workCenterRepository
    End Sub

    Public Function DeleteWorkCenter(workCenter As WorkCenter, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionMessageResult(Of WorkCenter) Implements IWorkCenterAdminService.DeleteWorkCenter
        Dim result As New ActionMessageResult(Of WorkCenter)
        result.StateResult = True
        If workCenter Is Nothing Then
            Throw New ArgumentNullException("company Vacio")
        End If
        Dim UnitOfWork As IUnitWork = _workCenterRepository.UnitWork
        Try
            _workCenterRepository.DeleteEntity(workCenter)
            UnitOfWork.Commit()
            '/***** Auditoria Basica ********/
            IndigoAuditBasic.Execute(workCenter.GetType.Name, audit.Functional, workCenter.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            '/*****Auditoria Avanzada ******/
            Dim auditObject As New IndigoAuditSimpleEntity(Of WorkCenter)(workCenter, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()
            Return result
        Catch ex As DbUpdateException
            UnitOfWork.RollbackChanges()
            result.StateResult = False
            result.MessageResult.Add(New MessageResult("c-0000", workCenter.Code))
            Return result
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            result.StateResult = False
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return result
        End Try
    End Function

    Public Function GetWorkCenter(code As String) As WorkCenter Implements IWorkCenterAdminService.GetWorkCenter
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Code Vacio")
        End If
        Try
            Dim workCenter = _workCenterRepository.GetWorkCenter(code)
            Return workCenter
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New WorkCenter()
        End Try
    End Function

    Public Function ListAllWorkCenter() As List(Of WorkCenter) Implements IWorkCenterAdminService.ListAllWorkCenter
        Try
            Return _workCenterRepository.ListAllWorkCenter()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SaveWorkCenter(workCenter As WorkCenter, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IWorkCenterAdminService.SaveWorkCenter
        If workCenter Is Nothing Then
            Throw New ArgumentNullException("workCenter Vacio")
        End If
        Dim UnitOfWork As IUnitWork = _workCenterRepository.UnitWork
        Try
            'Dim auxWorkCenter As WorkCenter = Nothing
            'If workCenter.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
            '    auxWorkCenter = _workCenterRepository.GetWorkCenter(workCenter.Code, False)
            'End If
            ''Valido si se va a guarar o actualizar
            '_workCenterRepository.SaveEntity(workCenter)
            'UnitOfWork.Commit()
            ''Agrego la auditoria
            'If workCenter.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
            '    '/***** Auditoria Basica ********/
            '    IndigoAuditBasic.Execute(workCenter.GetType.Name, audit.Functional, workCenter.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
            '    '/*****Auditoria Avanzada ******/
            '    IndigoAuditSimpleEntity(Of WorkCenter).Execute(workCenter, audit, Infrastructure.CrossCutting.Audit.Actions.Insert, audit.Company)
            'ElseIf workCenter.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
            '    '/***** Auditoria Basica ********/
            '    IndigoAuditBasic.Execute(workCenter.GetType.Name, audit.Functional, workCenter.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
            '    '/*****Auditoria Avanzada ******/
            '    IndigoAuditSimpleEntity(Of WorkCenter).Execute(workCenter, audit, Infrastructure.CrossCutting.Audit.Actions.Update, audit.Company, auxWorkCenter)
            'End If
            'Return True

            Dim auditProcess As IndigoAuditSimpleEntity(Of WorkCenter)
            Dim auxWorkCenter As WorkCenter = Nothing
            Dim status As Integer

            If workCenter.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                workCenter.ModificationUser = audit.CodeUser
                workCenter.ModificationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Update
                auxWorkCenter = _workCenterRepository.GetWorkCenter(workCenter.Code, False)
            Else
                workCenter.CreationUser = audit.CodeUser
                workCenter.CreationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            End If

            'Valido si se va a guardar o a eliminar
            _workCenterRepository.SaveEntity(workCenter)
            UnitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of WorkCenter)(workCenter, audit, status, auxWorkCenter)
            auditProcess.Execute()
            Return True
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _workCenterRepository = Nothing
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
