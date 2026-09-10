'***********************************************************************
' Assembly         : Application.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 30-12-2014
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
Imports Domain.Entities.Service

Public Class SettingBillingAdminService
    Implements ISettingBillingAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de facturas
    ''' </summary>
    Private _settingBillingRepository As ISettingsBillingRepository

    ''' <summary>
    ''' Repositorio de facturas
    ''' </summary>
    Private _customTRMRepository As ICustomTRMRepository

#End Region

#Region "Methods"

    Public Sub New(ByVal settingBilloingRepository As ISettingsBillingRepository, customTRMRepository As ICustomTRMRepository)
        If settingBilloingRepository Is Nothing Then
            Throw New ArgumentNullException("settingBilloingRepository")
        End If
        _settingBillingRepository = settingBilloingRepository
        _customTRMRepository = customTRMRepository
    End Sub

    ''' <summary>
    ''' Obtiene un registro de parámetros por Id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Id</exception>
    Public Function GetSettingsBillingById(id As Integer, tracking As Boolean) As SettingsBilling Implements ISettingBillingAdminService.GetSettingsBillingById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Try
            Return Me._settingBillingRepository.GetSettingsBillingById(id, tracking)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un resgistro de parámetros por el id de la unidad operativa
    ''' </summary>
    ''' <param name="IdUnitOperative"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">IdUnitOperative</exception>
    Public Function GetSettingsBillingByIdUnitOperative(IdUnitOperative As Integer, tracking As Boolean) As SettingsBilling Implements ISettingBillingAdminService.GetSettingsBillingByIdUnitOperative
        If IdUnitOperative = 0 Then
            Throw New ArgumentNullException("IdUnitOperative")
        End If
        Try
            Return Me._settingBillingRepository.GetSettingsBillingByIdUnitOperative(IdUnitOperative, tracking)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un resgistro de parámetros por el id de la unidad operativa
    ''' </summary>
    ''' <param name="IdUnitOperative"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">IdUnitOperative</exception>
    Public Function GetSettingBillingByUnitOperativeForm(IdUnitOperative As Integer) As ActionResult(Of SettingsBilling) Implements ISettingBillingAdminService.GetSettingBillingByUnitOperativeForm
        If IdUnitOperative = 0 Then
            Throw New ArgumentNullException("IdUnitOperative")
        End If
        Try
            Dim settingBilling As Domain.Entities.SettingsBilling = Me._settingBillingRepository.GetSettingBillingByUnitOperativeForm(IdUnitOperative)
            Return New ActionResult(Of Domain.Entities.SettingsBilling) With {.StateResult = True, .ObjectEmbbeded = settingBilling}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of SettingsBilling) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Saves the settings billing.
    ''' </summary>
    ''' <param name="settingBilling">The setting billing.</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveSettingsBilling(settingBilling As SettingsBilling, audit As AuditMessage) As ActionResult(Of SettingsBilling) Implements ISettingBillingAdminService.SaveSettingsBilling
        If settingBilling Is Nothing Then
            Throw New ArgumentNullException("settingsBilling")
        End If
        Dim unitOfWork As IUnitWork = Me._settingBillingRepository.UnitWork
        Try

            Dim auxSettingsBilling As SettingsBilling = Nothing
            Dim status As Integer
            If settingBilling.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                settingBilling.CreationDate = Date.Now
                settingBilling.CreationUser = audit.CodeUser
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                settingBilling.ModificationDate = Date.Now
                settingBilling.ModificationUser = audit.CodeUser
                status = Infrastructure.CrossCutting.Audit.Actions.Update
                auxSettingsBilling = settingBilling.OriginalValue
            End If

            Me._settingBillingRepository.SaveEntity(settingBilling)
            unitOfWork.Commit()
            Dim auditProcess As New IndigoAuditSimpleEntity(Of SettingsBilling)(settingBilling, audit, status, auxSettingsBilling)
            auditProcess.Execute()

            Return New ActionResult(Of SettingsBilling) With {.StateResult = True, .ObjectEmbbeded = settingBilling}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of SettingsBilling) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of SettingsBilling) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Saves the custom TRM.
    ''' </summary>
    ''' <param name="CustomTRM">The CustomTRM.</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveCustomTRM(CustomTRM As List(Of CustomTRM), audit As AuditMessage) As ActionResult(Of CustomTRM) Implements ISettingBillingAdminService.SaveCustomTrm
        If CustomTRM Is Nothing Then
            Throw New ArgumentNullException("settingsBilling")
        End If
        Dim unitOfWork As IUnitWork = Me._settingBillingRepository.UnitWork
        Try
            CustomTRM.FindAll(Function(s) s.ChangeTracker.State <> ObjectState.Deleted).ForEach(Sub(x)
                                                                                                    Me._customTRMRepository.SaveEntity(x)
                                                                                                End Sub)

            CustomTRM.FindAll(Function(s) s.ChangeTracker.State = ObjectState.Deleted).ForEach(Sub(x)
                                                                                                   Me._customTRMRepository.DeleteEntity(x)
                                                                                               End Sub)

            unitOfWork.Commit()
            Return New ActionResult(Of CustomTRM) With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of CustomTRM) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CustomTRM) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene la cantidad de registros de facturas de tipo de documento 5
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCustomTRM(OperatingUnitId As Integer, tracking As Boolean) As ActionResult(Of List(Of CustomTRM)) Implements ISettingBillingAdminService.GetCustomTRM
        If OperatingUnitId = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Try
            Dim query = Me._settingBillingRepository.GetCustomTRMByOperatingUnitId(OperatingUnitId, True)
            If query Is Nothing Then
                query = New List(Of CustomTRM)
            End If

            Return New ActionResult(Of List(Of CustomTRM)) With {.StateResult = True, .ObjectEmbbeded = query}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene la cantidad de registros de facturas de tipo de documento 5
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSettingBillingByUnitOperativeForm() As Integer Implements ISettingBillingAdminService.CountBillingInvoice
        Try
            Return Me._settingBillingRepository.CountBillingInvoice()
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
                ' TODO: elimine el estado administrado (objetos administrados).
            End If
            _settingBillingRepository = Nothing
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