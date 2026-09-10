Imports Domain.Security
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions

Public Class GeneralConfigurationAdminService
    Implements IGeneralConfigurationAdminService

    Dim _GeneralConfigurationRepository As IGeneralConfigurationRepository

    Public Sub New(ByVal GeneralConfigurationRepository As IGeneralConfigurationRepository)
        If GeneralConfigurationRepository Is Nothing Then
            Throw New ArgumentNullException("GeneralConfigurationRepository Vacio")
        End If
        _GeneralConfigurationRepository = GeneralConfigurationRepository
    End Sub

    Public Function GetGeneralConfiguration() As GeneralConfiguration Implements IGeneralConfigurationAdminService.GetGeneralConfiguration
        Try
            Dim sc As GeneralConfiguration = _GeneralConfigurationRepository.GetGeneralConfiguration()
            Return sc
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New GeneralConfiguration
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
            _GeneralConfigurationRepository = Nothing
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

