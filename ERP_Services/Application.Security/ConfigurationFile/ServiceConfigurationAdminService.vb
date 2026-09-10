Imports System.Transactions
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Security
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources

Public Class ServiceConfigurationAdminService
    Implements IServiceConfigurationAdminService

    Dim _ServiceConfigurationRepository As IServiceConfigurationRepository

    Public Sub New(ByVal serviceConfigurationRepository As IServiceConfigurationRepository)
        If serviceConfigurationRepository Is Nothing Then
            Throw New ArgumentNullException("serviceConfigurationRepository Vacio")
        End If
        _ServiceConfigurationRepository = serviceConfigurationRepository
    End Sub

    Public Function SaveServiceConfiguration(serviceConfiguration As ServiceConfiguration, audit As AuditMessage) As ActionResult(Of ServiceConfiguration) Implements IServiceConfigurationAdminService.SaveServiceConfiguration
        If serviceConfiguration Is Nothing Then
            Throw New ArgumentNullException("serviceConfiguration Vacio")
        End If
        Dim unitWorkSC As IUnitWork = TryCast(_ServiceConfigurationRepository.UnitWork, IUnitWork)
        Try
            'configuro la transaccion
            Dim MessageResult As String = ResourceManager.GetString("SaveMessage")
            Dim txSettings As New TransactionOptions()
            txSettings.Timeout = TransactionManager.DefaultTimeout
            txSettings.IsolationLevel = IsolationLevel.ReadCommitted
            'inicio la transaccion
            Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)
                _ServiceConfigurationRepository.SaveEntity(serviceConfiguration)
                unitWorkSC.CommitAndRefreshChanges()
                scope.Complete()
            End Using
            Return New ActionResult(Of ServiceConfiguration) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = serviceConfiguration, .Message = MessageResult}
        Catch ex As Exception
            'devuelvo cambios usuario
            unitWorkSC.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ServiceConfiguration) With {.StateResult = False, .MessageResult = New List(Of String) From {ex.Message}, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function GetServiceConfigurationById(id As Byte) As ServiceConfiguration Implements IServiceConfigurationAdminService.GetServiceConfigurationById
        Try
            Dim sc As ServiceConfiguration = _ServiceConfigurationRepository.GetServiceConfigurationById(id)
            Return sc
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ServiceConfiguration
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
            _ServiceConfigurationRepository = Nothing
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
