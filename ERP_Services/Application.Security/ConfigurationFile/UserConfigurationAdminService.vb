Imports System.Transactions
Imports Domain.Base
    Imports Domain.Base.Entities
    Imports Domain.Security
    Imports Domain.Security.Entities
    Imports Infrastructure.CrossCutting.Base
    Imports Infrastructure.CrossCutting.Exceptions
    Imports Infrastructure.CrossCutting.Resources

    Public Class UserConfigurationAdminService
        Implements IUserConfigurationAdminService

        Dim _UserConfigurationRepository As IUserConfigurationRepository

        Public Sub New(ByVal UserConfigurationRepository As IUserConfigurationRepository)
            If UserConfigurationRepository Is Nothing Then
                Throw New ArgumentNullException("UserConfigurationRepository Vacio")
            End If
            _UserConfigurationRepository = UserConfigurationRepository
        End Sub

        Public Function SaveUserConfiguration(UserConfiguration As UserConfiguration, audit As AuditMessage) As ActionResult(Of UserConfiguration) Implements IUserConfigurationAdminService.SaveUserConfiguration
            If UserConfiguration Is Nothing Then
                Throw New ArgumentNullException("UserConfiguration Vacio")
            End If
            Dim unitWorkSC As IUnitWork = TryCast(_UserConfigurationRepository.UnitWork, IUnitWork)
            Try
                'configuro la transaccion
                Dim MessageResult As String = ResourceManager.GetString("SaveMessage")
                Dim txSettings As New TransactionOptions()
                txSettings.Timeout = TransactionManager.DefaultTimeout
            txSettings.IsolationLevel = IsolationLevel.ReadCommitted
            If UserConfiguration.DefaultCompany = 0 Then
                UserConfiguration.DefaultCompany = Nothing
            End If
            'inicio la transaccion
            Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)
                    _UserConfigurationRepository.SaveEntity(UserConfiguration)
                    unitWorkSC.CommitAndRefreshChanges()
                    scope.Complete()
                End Using
                Return New ActionResult(Of UserConfiguration) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = UserConfiguration, .Message = MessageResult}
            Catch ex As Exception
                'devuelvo cambios usuario
                unitWorkSC.RollbackChanges()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of UserConfiguration) With {.StateResult = False, .MessageResult = New List(Of String) From {ex.Message}, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
            End Try
        End Function

    Public Function GetUserConfigurationByUserId(userId As Integer) As UserConfiguration Implements IUserConfigurationAdminService.GetUserConfigurationByUserId
        Try
            Dim sc As UserConfiguration = _UserConfigurationRepository.GetUserConfigurationByUserId(userId)
            If sc IsNot Nothing AndAlso sc.TypeAlertControl Is Nothing Then
                sc.TypeAlertControl = 1 'Alert Windows
            End If
            Return sc
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New UserConfiguration
        End Try
    End Function

    Public Function UpdateUserConfiguration(UserConfigurationCulture As UserConfigurationCulture) As Boolean Implements IUserConfigurationAdminService.UpdateUserConfiguration
        Try
            _UserConfigurationRepository.UpdateUserConfiguration(UserConfigurationCulture)
            Return True
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    ' TODO: elimine el estado administrado (objetos administrados).
                End If
                _UserConfigurationRepository = Nothing
                IndigoGC.Execute()
            End If
            disposedValue = True
        End Sub

        ' TODO: reemplace Finalize() solo si el anterior Dispose(disposing As Boolean) tiene código para liberar recursos no administrados.
        'Protected Overrides Sub Finalize()
        '    ' No cambie este código. Coloque el código de limpieza en el anterior Dispose(disposing As Boolean).
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' No cambie este código. Coloque el código de limpieza en el anterior Dispose(disposing As Boolean).
            Dispose(True)
            ' TODO: quite la marca de comentario de la siguiente línea si Finalize() se ha reemplazado antes.
            GC.SuppressFinalize(Me)
        End Sub
#End Region
    End Class

