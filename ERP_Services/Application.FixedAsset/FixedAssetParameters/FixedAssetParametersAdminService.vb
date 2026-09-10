#Region "Imports"
Imports Domain.Base
Imports Infrastructure.CrossCutting.Base
Imports Application.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports System.Data.Entity.Core

#End Region

Public Class SettingFixedAssetAdminService
    Implements ISettingFixedAssetAdminService

    'Repositorio de la aseguradora
    Private _ParametersRepository As ISettingFixedAssetRepository

    ''' <summary>
    ''' inicia el repositorio de bancos
    ''' </summary>
    ''' <param name="ParametersRepository">Repositorio de bancos</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal ParametersRepository As ISettingFixedAssetRepository)
        If (ParametersRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de Parámetros vacio")
        End If
        _ParametersRepository = ParametersRepository
    End Sub

    Public Function DeleteSettingFixedAsset(SettingFixedAsset As SettingFixedAsset, audit As AuditMessage) As Boolean Implements ISettingFixedAssetAdminService.DeleteSettingFixedAsset
        If SettingFixedAsset Is Nothing Then
            Throw New ArgumentNullException("SettingFixedAsset vacio")
        End If
        Dim unitWork As IUnitWork = _ParametersRepository.UnitWork
        Try
            _ParametersRepository.DeleteEntity(SettingFixedAsset)
            unitWork.Commit()
            Dim auditObject As New IndigoAuditSimpleEntity(Of SettingFixedAsset)(SettingFixedAsset, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()
            Return True
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    Public Function GetSettingFixedAssetOperatingUnitId(OperatingUnitId As Integer) As SettingFixedAsset Implements ISettingFixedAssetAdminService.GetSettingFixedAssetByOperatingUnitId
        Try
            Return _ParametersRepository.GetSettingFixedAssetByOperatingUnitId(OperatingUnitId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return New SettingFixedAsset()
        End Try
    End Function

    Public Function SaveSettingFixedAsset(SettingFixedAsset As SettingFixedAsset, audit As AuditMessage) As ActionResult(Of SettingFixedAsset) Implements ISettingFixedAssetAdminService.SaveSettingFixedAsset
        If SettingFixedAsset Is Nothing Then
            Throw New ArgumentNullException("SettingFixedAsset")
        End If
        Dim unitOfWork As IUnitWork = Me._ParametersRepository.UnitWork
        Try

            Dim auxSettings As SettingFixedAsset = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of SettingFixedAsset)
            Dim status As Integer

            If SettingFixedAsset.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                'SettingFixedAsset.CreationUser = audit.CodeUser
                'SettingFixedAsset.CreationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                auxSettings = SettingFixedAsset.OriginalValue
                'SettingFixedAsset.ModificationUser = audit.CodeUser
                'SettingFixedAsset.ModificationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            End If

            Me._ParametersRepository.SaveEntity(SettingFixedAsset)
            unitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of SettingFixedAsset)(SettingFixedAsset, audit, status, auxSettings)
            auditProcess.Execute()

            'Se marca la entidad como sin cambios
            SettingFixedAsset.MarkAsUnchanged()

            Return New ActionResult(Of SettingFixedAsset) With {.StateResult = True, .ObjectEmbbeded = SettingFixedAsset}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of SettingFixedAsset) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of SettingFixedAsset) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _ParametersRepository = Nothing
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
