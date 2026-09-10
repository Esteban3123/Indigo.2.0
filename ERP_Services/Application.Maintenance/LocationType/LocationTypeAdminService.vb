

#Region "Imports"
Imports Domain.Maintenance
Imports Domain.Base
Imports Infrastructure.CrossCutting.Base
Imports Application.Base
Imports Domain.Maintenance.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities

#End Region
Public Class LocationTypeAdminService
    Implements ILocationTypeAdminService

    'Repositorio de tipo de ubicacion
    Private _LocationTypeRepository As ILocationTypeRepository

    ''' <summary>
    ''' inicia el repositorio de tipo de ubicacion
    ''' </summary>
    ''' <param name="LocationTypeRepository">Repositorio de tipo de ubicacion</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal LocationTypeRepository As ILocationTypeRepository)
        If (LocationTypeRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de Ubicacion")
        End If
        _LocationTypeRepository = LocationTypeRepository
    End Sub

    Public Function ListAllLocationType() As List(Of LocationType) Implements ILocationTypeAdminService.ListAllLocationType
        Try
            Return _LocationTypeRepository.ListAllLocationType
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
            Return Nothing
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _LocationTypeRepository = Nothing
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
