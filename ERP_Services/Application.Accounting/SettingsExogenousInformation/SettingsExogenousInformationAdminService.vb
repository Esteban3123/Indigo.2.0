#Region "Imports"
Imports Domain.Base
Imports Infrastructure.CrossCutting.Base
Imports Application.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports System.Data.Entity.Core
Imports Application.Accounting

#End Region

Public Class SettingsExogenousInformationAdminService

    Implements ISettingsExogenousInformationAdminService

    'Repositorio de la aseguradora
    Private _ParametersRepository As ISettingsExogenousInformationRepository

    ''' <summary>
    ''' inicia el repositorio de bancos
    ''' </summary>
    ''' <param name="ParametersRepository">Repositorio de bancos</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal ParametersRepository As ISettingsExogenousInformationRepository)
        If (ParametersRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de Parámetros vacio")
        End If
        _ParametersRepository = ParametersRepository
    End Sub

    Public Function GetSettingsExogenousInformation() As List(Of SettingsExogenousInformation) Implements ISettingsExogenousInformationAdminService.GetSettingsExogenousInformation
        Try
            Return _ParametersRepository.GetSettingsExogenousInformation()
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return New List(Of SettingsExogenousInformation)
        End Try
    End Function

    Public Function SaveSettingsExogenousInformation(ListSettingsExogenousInformation As List(Of SettingsExogenousInformation), audit As AuditMessage) As ActionResult(Of List(Of SettingsExogenousInformation)) Implements ISettingsExogenousInformationAdminService.SaveSettingsExogenousInformation
        If ListSettingsExogenousInformation Is Nothing Then
            Throw New ArgumentNullException("SettingsExogenousInformation")
        End If
        Dim unitOfWork As IUnitWork = Me._ParametersRepository.UnitWork
        Try

            For Each SettingsExogenousInformation As SettingsExogenousInformation In ListSettingsExogenousInformation
                Dim auxSettings As SettingsExogenousInformation = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of SettingsExogenousInformation)
                Dim status As Integer

                If SettingsExogenousInformation.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    SettingsExogenousInformation.CreationUser = audit.CodeUser
                    SettingsExogenousInformation.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    auxSettings = SettingsExogenousInformation.OriginalValue
                    SettingsExogenousInformation.ModificationUser = audit.CodeUser
                    SettingsExogenousInformation.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._ParametersRepository.SaveEntity(SettingsExogenousInformation)
                unitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of SettingsExogenousInformation)(SettingsExogenousInformation, audit, status, auxSettings)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                SettingsExogenousInformation.MarkAsUnchanged()

            Next

            Return New ActionResult(Of List(Of SettingsExogenousInformation)) With {.StateResult = True, .ObjectEmbbeded = ListSettingsExogenousInformation}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of List(Of SettingsExogenousInformation)) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of SettingsExogenousInformation)) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            Me._ParametersRepository = Nothing
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
