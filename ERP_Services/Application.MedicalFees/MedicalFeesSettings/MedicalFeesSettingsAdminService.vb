'***********************************************************************
' Assembly         : Application.MedicalFees
' Author           : Carlos Mario Arias Rubiano
' Created          : 21/01/2015
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

Public Class MedicalFeesSettingsAdminService
    Implements IMedicalFeesSettingsAdminService

#Region "Variables"

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _medicalFeesSettingsRepository As IMedicalFeesSettingsRepository
#End Region

#Region "Builder"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal medicalFeesSettingsRepository As IMedicalFeesSettingsRepository)
        If medicalFeesSettingsRepository Is Nothing Then
            Throw New ArgumentNullException("medicalFeesSettingsRepository Vacio")
        End If
        _medicalFeesSettingsRepository = medicalFeesSettingsRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene los parametros de honorarios medicos
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMedicalFeesSettings(audit As AuditMessage) As ActionResult(Of SettingMedicalFees) Implements IMedicalFeesSettingsAdminService.GetMedicalFeesSettings
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim MedicalFeesSettings As SettingMedicalFees = Me._medicalFeesSettingsRepository.GetMedicalFeesSetting()
            If MedicalFeesSettings IsNot Nothing AndAlso MedicalFeesSettings.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of SettingMedicalFees)(MedicalFeesSettings, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of SettingMedicalFees) With {.StateResult = True, .ObjectEmbbeded = MedicalFeesSettings}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of SettingMedicalFees) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza los parametros de honorarios medicos
    ''' </summary>
    ''' <param name="MedicalFeesSettings"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveMedicalFeesSettings(MedicalFeesSettings As SettingMedicalFees, audit As AuditMessage) As ActionResult(Of SettingMedicalFees) Implements IMedicalFeesSettingsAdminService.SaveMedicalFeesSettings
        If MedicalFeesSettings Is Nothing Then
            Throw New ArgumentNullException("MedicalFeesSettings")
        End If
        Dim unitOfWork As IUnitWork = Me._medicalFeesSettingsRepository.UnitWork
        Try

            Dim auxSetting As SettingMedicalFees = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of SettingMedicalFees)
            Dim status As Integer

            If MedicalFeesSettings.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                MedicalFeesSettings.CreationUser = audit.CodeUser
                MedicalFeesSettings.CreationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                auxSetting = MedicalFeesSettings.OriginalValue
                MedicalFeesSettings.ModificationUser = audit.CodeUser
                MedicalFeesSettings.ModificationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            End If

            Me._medicalFeesSettingsRepository.SaveEntity(MedicalFeesSettings)
            unitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of SettingMedicalFees)(MedicalFeesSettings, audit, status, auxSetting)
            auditProcess.Execute()
            'Se marca la entidad como sin cambios
            MedicalFeesSettings.MarkAsUnchanged()

            Return New ActionResult(Of SettingMedicalFees) With {.StateResult = True, .ObjectEmbbeded = MedicalFeesSettings}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of SettingMedicalFees) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of SettingMedicalFees) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
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
            _medicalFeesSettingsRepository = Nothing
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
