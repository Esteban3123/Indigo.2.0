'***********************************************************************
' Assembly         : Application.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 29-12-2014
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
Imports Domain.InteropCost
Imports Domain.InteropCost.Entities

Public Class InteropCostSettingAdminService
    Implements IInteropCostSettingAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de gastos generales
    ''' </summary>
    Private _interopCostSettingRepository As IInteropCostSettingRepository
    Private _ctntipcomRepository As ICTNTIPCOMRepository

#End Region

#Region "Methods"

    Public Sub New(ByVal interopCostSettingRepository As IInteropCostSettingRepository, ctntipcomRepository As ICTNTIPCOMRepository)
        If interopCostSettingRepository Is Nothing Then
            Throw New ArgumentNullException("interopCostSettingRepository")
        End If
        _interopCostSettingRepository = interopCostSettingRepository
        _ctntipcomRepository = ctntipcomRepository
    End Sub

    ''' <summary>
    ''' Elimina un parámetro de costos
    ''' </summary>
    Public Function DeleteInteropCostSetting(interopCostSetting As InteropCostSetting, audit As AuditMessage) As ActionResult Implements IInteropCostSettingAdminService.DeleteInteropCostSetting
        If interopCostSetting Is Nothing Then
            Throw New ArgumentNullException("interopCostSetting")
        End If
        Dim unitOfWork As IUnitWork = Me._interopCostSettingRepository.UnitWork
        Try
            interopCostSetting.MarkAsDeleted()
            Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
            Dim auditProcess As New IndigoAuditSimpleEntity(Of InteropCostSetting)(interopCostSetting, audit, status)

            Me._interopCostSettingRepository.SaveEntity(interopCostSetting)
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

    ''' <summary>
    ''' Obtiene el parámetro actual de costos
    ''' </summary>
    ''' <returns></returns>
    Public Function GetInteropCostSetting() As InteropCostSetting Implements IInteropCostSettingAdminService.GetInteropCostSetting
        Try
            Dim interopCostSetting As InteropCostSetting = Me._interopCostSettingRepository.GetInteropCostSetting()
            If interopCostSetting IsNot Nothing AndAlso interopCostSetting.Id > 0 Then
                If interopCostSetting.JournalVoucherTypeId IsNot Nothing Then
                    Dim tipCOm As CTNTIPCOM = _ctntipcomRepository.GetCTNTIPCOMById(interopCostSetting.JournalVoucherTypeId)
                    If tipCOm IsNot Nothing AndAlso tipCOm.OID > 0 Then
                        interopCostSetting.FullNameJournalVoucherType = String.Concat(tipCOm.TCCODIGO, " - ", tipCOm.TCNOMBRE)
                    End If
                End If
            End If
            Return interopCostSetting
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el parámetro actual de costos por id
    ''' </summary>
    Public Function GetInteropCostSettingById(id As Integer) As InteropCostSetting Implements IInteropCostSettingAdminService.GetInteropCostSettingById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Try
            Return Me._interopCostSettingRepository.GetInteropCostSettingById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda un parámetro de costos
    ''' </summary>
    Public Function SaveInteropCostSetting(interopCostSetting As InteropCostSetting, audit As AuditMessage) As ActionResult(Of InteropCostSetting) Implements IInteropCostSettingAdminService.SaveInteropCostSetting
        If interopCostSetting Is Nothing Then
            Throw New ArgumentNullException("interopCostSetting")
        End If
        Dim unitOfWork As IUnitWork = Me._interopCostSettingRepository.UnitWork
        Try
            Me._interopCostSettingRepository.SaveEntity(interopCostSetting)
            unitOfWork.Commit()
            Dim auditProcess As New IndigoAuditSimpleEntity(Of InteropCostSetting)(interopCostSetting, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
            auditProcess.Execute()
            Return New ActionResult(Of InteropCostSetting) With {.StateResult = True, .ObjectEmbbeded = interopCostSetting}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of InteropCostSetting) With {.StateResult = False, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of InteropCostSetting) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
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
            _interopCostSettingRepository = Nothing
            _ctntipcomRepository = Nothing
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