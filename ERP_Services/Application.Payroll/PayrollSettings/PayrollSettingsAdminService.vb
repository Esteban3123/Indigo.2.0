#Region "Imports"
Imports Domain.Base
Imports Infrastructure.CrossCutting.Base
Imports Application.Payroll
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports System.Data.Entity.Core
Imports Domain.Payroll
Imports Application.Base

#End Region

Public Class PayrollSettingsAdminService

    Implements IPayrollSettingsAdminService

    'Repositorio de la aseguradora
    Private _SettingsRepository As IPayrollSettingsRepository

    ''' <summary>
    ''' inicia el repositorio de bancos
    ''' </summary>
    ''' <param name="ParametersRepository">Repositorio de bancos</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal SettingsRepository As IPayrollSettingsRepository)
        If (SettingsRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de Parámetros vacio")
        End If
        _SettingsRepository = SettingsRepository
    End Sub


    Public Function DeletePayrollSettings(PayrollSettings As PayrollSettings, audit As AuditMessage) As Boolean Implements IPayrollSettingsAdminService.DeletePayrollSettings
        If PayrollSettings Is Nothing Then
            Throw New ArgumentNullException("PayrollSettings vacio")
        End If
        Dim unitWork As IUnitWork = _SettingsRepository.UnitWork
        Try
            _SettingsRepository.DeleteEntity(PayrollSettings)
            unitWork.Commit()
            Dim auditObject As New IndigoAuditSimpleEntity(Of PayrollSettings)(PayrollSettings, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()
            Return True
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    Public Function GetPayrollSettings() As PayrollSettings Implements IPayrollSettingsAdminService.GetPayrollSettings
        Try
            Return _SettingsRepository.GetSettingPayroll()
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return New PayrollSettings()
        End Try
    End Function

    Public Function SavePayrollSettings(PayrollSettings As PayrollSettings, audit As AuditMessage) As ActionResult(Of PayrollSettings) Implements IPayrollSettingsAdminService.SavePayrollSettings
        If PayrollSettings Is Nothing Then
            Throw New ArgumentNullException("SettingFixedAsset")
        End If
        Dim unitOfWork As IUnitWork = Me._SettingsRepository.UnitWork
        Try

            Dim auxSettings As PayrollSettings = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of PayrollSettings)
            Dim status As Integer

            If PayrollSettings.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                'SettingFixedAsset.CreationUser = audit.CodeUser
                'SettingFixedAsset.CreationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                auxSettings = _SettingsRepository.GetSettingPayroll(False)
                'SettingFixedAsset.ModificationUser = audit.CodeUser
                'SettingFixedAsset.ModificationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            End If

            Me._SettingsRepository.SaveEntity(PayrollSettings)
            unitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of PayrollSettings)(PayrollSettings, audit, status, auxSettings)
            auditProcess.Execute()

            'Se marca la entidad como sin cambios
            PayrollSettings.MarkAsUnchanged()

            Return New ActionResult(Of PayrollSettings) With {.StateResult = True, .ObjectEmbbeded = PayrollSettings}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of PayrollSettings) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of PayrollSettings) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _SettingsRepository = Nothing
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
