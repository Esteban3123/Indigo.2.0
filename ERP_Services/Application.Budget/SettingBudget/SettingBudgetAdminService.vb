'***********************************************************************
' Assembly         : Application.Budget
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 04-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "imports"
Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

Public Class SettingBudgetAdminService
    Implements ISettingBudgetAdminService

#Region "Fields"
    ''' <summary>
    ''' Variable de tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _settingBudgetRepository As ISettingBudgetRepository
#End Region

#Region "Builder"
    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <param name="settingBudgetRepository"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal settingBudgetRepository As ISettingBudgetRepository)
        If settingBudgetRepository Is Nothing Then
            Throw New ArgumentNullException("settingBudgetReposotory")
        End If
        _settingBudgetRepository = settingBudgetRepository
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Obtiene la configuracion de presupuesto
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">audit</exception>
    Public Function GetSettingBudgetById(id As Integer, audit As AuditMessage) As ActionResult(Of SettingsBudget) Implements ISettingBudgetAdminService.GetSettingBudgetById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            'Dim settingBudget As SettingsBudget = _settingBudgetRepository.GetSettingBudgetByOperatingUnit(id)
            'If settingBudget IsNot Nothing AndAlso settingBudget.Id > 0 Then
            '    IndigoAuditSimpleEntity(Of SettingsBudget).Execute(settingBudget, audit, Infrastructure.CrossCutting.Audit.Actions.Print, audit.Company)
            'End If
            'Return settingBudget
            Dim settingBudget As SettingsBudget = Me._settingBudgetRepository.GetSettingBudgetByOperatingUnit(id)
            If settingBudget IsNot Nothing AndAlso settingBudget.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of SettingsBudget)(settingBudget, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of SettingsBudget) With {.StateResult = True, .ObjectEmbbeded = settingBudget}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of SettingsBudget) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza un parametro de Presupuesto
    ''' </summary>
    ''' <param name="settingBudget"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveSettingBudget(settingBudget As SettingsBudget, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of SettingsBudget) Implements ISettingBudgetAdminService.SaveSettingBudget
        If settingBudget Is Nothing Then
            Throw New ArgumentNullException("settingBudget")
        End If
        Dim unitOfWork As IUnitWork = Me._settingBudgetRepository.UnitWork
        Try

            Dim auxSettingBudget As SettingsBudget = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of SettingsBudget)
            Dim status As Integer

            If settingBudget.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                settingBudget.CreationUser = audit.CodeUser
                settingBudget.CreationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                auxSettingBudget = _settingBudgetRepository.GetSettingBudgetByOperatingUnit(settingBudget.OperatingUnitId)
                settingBudget.ModificationUser = audit.CodeUser
                settingBudget.ModificationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            End If

            Me._settingBudgetRepository.SaveEntity(settingBudget)
            unitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of SettingsBudget)(settingBudget, audit, status, auxSettingBudget)
            auditProcess.Execute()
            'Se marca la entidad como sin cambios
            settingBudget.MarkAsUnchanged()

            Return New ActionResult(Of SettingsBudget) With {.StateResult = True, .ObjectEmbbeded = settingBudget}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of SettingsBudget) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of SettingsBudget) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeState(id As Integer, state As Boolean, audit As AuditMessage) As ActionResult(Of SettingsBudget) Implements ISettingBudgetAdminService.ChangeState

    End Function

    ''' <summary>
    ''' Elimina un parametro de Presupuesto
    ''' </summary>
    ''' <param name="settingBudget"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteSettingBudget(settingBudget As SettingsBudget, audit As AuditMessage) As ActionResult Implements ISettingBudgetAdminService.DeleteSettingBudget
        If settingBudget Is Nothing Then
            Throw New ArgumentNullException("settingPayments")
        End If
        Dim unitOfWork As IUnitWork = Me._settingBudgetRepository.UnitWork
        Try
            If settingBudget.ChangeTracker.State = ObjectState.Deleted Then
                Me._settingBudgetRepository.DeleteEntity(settingBudget)
                unitOfWork.Commit()
                'Auditoria básica
                IndigoAuditBasic.Execute(GetType(SettingsBudget).Name, audit.Functional, settingBudget.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, Infrastructure.CrossCutting.Base.ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
                'Ausitoria avanzada
                Dim auditObject As New IndigoAuditSimpleEntity(Of SettingsBudget)(settingBudget, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
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

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            Me._settingBudgetRepository = Nothing
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
