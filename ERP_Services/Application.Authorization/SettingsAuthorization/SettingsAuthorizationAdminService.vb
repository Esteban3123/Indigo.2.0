Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions

Public Class SettingsAuthorizationAdminService
    Implements ISettingsAuthorizationAdminService

#Region "Builder"

    Private _settingsAuthorizationRepository As ISettingsAuthorizationRepository

    Public Sub New(ByVal settingsAuthorizationRepository As ISettingsAuthorizationRepository)
        If settingsAuthorizationRepository Is Nothing Then
            Throw New ArgumentNullException("settingsAuthorizationRepository")
        End If

        Me._settingsAuthorizationRepository = settingsAuthorizationRepository
    End Sub

#End Region

#Region "Methods"

    Public Function GetSettingsAuthorization() As ActionResult(Of SettingsAuthorization) Implements ISettingsAuthorizationAdminService.GetSettingsAuthorization
        Try
            Dim settingsAuthorization = Me._settingsAuthorizationRepository.GetSettingsAuthorization()

            Return New ActionResult(Of SettingsAuthorization) With {.StateResult = True, .ObjectEmbbeded = settingsAuthorization}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of SettingsAuthorization) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function SaveSettingsAuthorization(settingsAuthorization As SettingsAuthorization, audit As AuditMessage) As ActionResult(Of SettingsAuthorization) Implements ISettingsAuthorizationAdminService.SaveSettingsAuthorization
        If settingsAuthorization Is Nothing Then
            Throw New ArgumentNullException("SettingsAuthorization")
        End If
        Dim unitOfWork As IUnitWork = Me._settingsAuthorizationRepository.UnitWork
        Try

            Dim auxSettingsBilling As SettingsAuthorization = Nothing
            Dim status As Integer
            If settingsAuthorization.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                settingsAuthorization.CreationDate = Date.Now
                settingsAuthorization.CreationUser = audit.CodeUser
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                settingsAuthorization.ModificationDate = Date.Now
                settingsAuthorization.ModificationUser = audit.CodeUser
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            End If

            Me._settingsAuthorizationRepository.SaveEntity(settingsAuthorization)
            unitOfWork.Commit()

            Dim auditProcess As New IndigoAuditSimpleEntity(Of SettingsAuthorization)(settingsAuthorization, audit, status, settingsAuthorization.OriginalValue)
            auditProcess.Execute()

            Return New ActionResult(Of SettingsAuthorization) With {.StateResult = True, .ObjectEmbbeded = settingsAuthorization}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of SettingsAuthorization) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of SettingsAuthorization) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
