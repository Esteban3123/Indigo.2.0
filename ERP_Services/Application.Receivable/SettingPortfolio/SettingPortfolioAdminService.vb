'***********************************************************************
' Assembly         : Application.Portfolio
' Author           : Diego Andrés Roldán Lozano
' Created          : 29-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Libraries"
Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
Imports Infrastructure.CrossCutting.Resources

#End Region

Public Class SettingPortfolioAdminService
    Implements ISettingPortfolioAdminService



#Region "Fields"
    Private _settingPortfolioRepository As ISettingPortfolioRepository
#End Region

    Public Sub New(settingPortfolioRepository As ISettingPortfolioRepository)
        If settingPortfolioRepository Is Nothing Then
            Throw New ArgumentNullException("settingPortfolioRepository")
        End If
        _settingPortfolioRepository = settingPortfolioRepository
    End Sub


    ''' <summary>
    ''' obtiene los parametros por unidad operativa
    ''' </summary>
    ''' <param name="idOperatingUnit"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function GetSettinPortfolioByIdOperatingUnit(idOperatingUnit As Integer, audit As AuditMessage) As SettingPortfolio Implements ISettingPortfolioAdminService.GetSettinPortfolioByIdOperatingUnit
        Try
            Dim setting = _settingPortfolioRepository.GetSettingPortfolioByIdOperatingUnit(idOperatingUnit)
            If setting IsNot Nothing AndAlso setting.Id > 0 Then
                Dim auditProcess As New IndigoAuditSimpleEntity(Of SettingPortfolio)(setting, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditProcess.Execute()
            End If
            Return setting
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New SettingPortfolio()
        End Try
    End Function

    ''' <summary>
    ''' guardar los parametros de cartera
    ''' </summary>
    ''' <param name="settingPortfolio"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">settingPortfolio</exception>
    Public Function SaveSettingPortfolio(settingPortfolio As SettingPortfolio, audit As AuditMessage) As ActionResult(Of SettingPortfolio) Implements ISettingPortfolioAdminService.SaveSettingPortfolio
        If settingPortfolio Is Nothing Then
            Throw New ArgumentNullException("settingPortfolio")
        End If
        Try
            Dim settingUnitOfWork As IUnitWork = _settingPortfolioRepository.UnitWork
            Dim auditProcess As IndigoAuditSimpleEntity(Of SettingPortfolio)
            Dim status As Integer
            If settingPortfolio.ChangeTracker.State = ObjectState.Added Then
                settingPortfolio.CreationDate = DateTime.Now
                settingPortfolio.CreationUser = audit.CodeUser
                For Each item In settingPortfolio.AgesPortfolio
                    item.CreationDate = DateTime.Now
                    item.CreationUser = audit.CodeUser
                Next
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                settingPortfolio.ModificationDate = DateTime.Now
                settingPortfolio.ModificationUser = audit.CodeUser
                For Each item In settingPortfolio.AgesPortfolio
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
            _settingPortfolioRepository.SaveEntity(settingPortfolio)
            settingUnitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of SettingPortfolio)(settingPortfolio, audit, status, settingPortfolio.OriginalValue)
            auditProcess.Execute()
            If settingPortfolio.ChangeTracker.State = ObjectState.Added Then
                Return New ActionResult(Of SettingPortfolio) With {.StateResult = True, .ObjectEmbbeded = settingPortfolio, .Message = ResourceManager.GetString("SaveMessage")}
            Else
                Return New ActionResult(Of SettingPortfolio) With {.StateResult = True, .ObjectEmbbeded = settingPortfolio, .Message = ResourceManager.GetString("UpdateMessage")}
            End If
        Catch ex As OptimisticConcurrencyException
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of SettingPortfolio) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            If ex.HResult = -2146233087 AndAlso ex.InnerException.InnerException.Message.Contains("FK_ContractDetail_AgesPortfolio") Then
                Return New ActionResult(Of SettingPortfolio) With {.StateResult = False, .Message = "No es posible realizar la operación debido a que no se puede eliminar rangos de edad que están asociados a un contrato"}
            End If
            Return New ActionResult(Of SettingPortfolio) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _settingPortfolioRepository = Nothing
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
