'***********************************************************************
' Assembly         : Application.Cost
' Author           : Diego Andrés Roldán Lozano
' Created          : 26-02-2016
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
Imports Infrastructure.CrossCutting.Resources

Public Class CostSettingAdminService
    Implements ICostSettingAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de gastos generales
    ''' </summary>
    Private _costSettingRepository As ICostSettingRepository

#End Region

#Region "Methods"

    Public Sub New(ByVal costSettingRepository As ICostSettingRepository)
        If costSettingRepository Is Nothing Then
            Throw New ArgumentNullException("costSettingRepository")
        End If
        _costSettingRepository = costSettingRepository
    End Sub

    Public Function DeleteCostSetting(costSetting As CostSetting, audit As AuditMessage) As ActionResult Implements ICostSettingAdminService.DeleteCostSetting
        If costSetting Is Nothing Then
            Throw New ArgumentNullException("costSetting")
        End If
        Dim unitOfWork As IUnitWork = Me._costSettingRepository.UnitWork
        Try
            costSetting.MarkAsDeleted()
            Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
            Dim auditProcess As New IndigoAuditSimpleEntity(Of CostSetting)(costSetting, audit, status)

            Me._costSettingRepository.SaveEntity(costSetting)
            unitOfWork.Commit()
            auditProcess.Execute()
            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"}), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    Public Function GetCostSetting() As CostSetting Implements ICostSettingAdminService.GetCostSetting
        Try
            Return Me._costSettingRepository.GetCostSetting()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function GetCostSettingById(id As Integer) As CostSetting Implements ICostSettingAdminService.GetCostSettingById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Try
            Return Me._costSettingRepository.GetCostSettingById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SaveCostSetting(costSetting As CostSetting, audit As AuditMessage) As ActionResult(Of CostSetting) Implements ICostSettingAdminService.SaveCostSetting
        If costSetting Is Nothing Then
            Throw New ArgumentNullException("costSetting")
        End If
        Dim unitOfWork As IUnitWork = Me._costSettingRepository.UnitWork
        Try
            Me._costSettingRepository.SaveEntity(costSetting)
            unitOfWork.Commit()
            Dim auditProcess As New IndigoAuditSimpleEntity(Of CostSetting)(costSetting, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
            auditProcess.Execute()
            Return New ActionResult(Of CostSetting) With {.StateResult = True, .ObjectEmbbeded = costSetting}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of CostSetting) With {.StateResult = False, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CostSetting) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
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
            _costSettingRepository = Nothing
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