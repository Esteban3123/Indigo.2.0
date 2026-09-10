#Region "Imports"
Imports Domain.Base
Imports Infrastructure.CrossCutting.Base
Imports Application.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities

#End Region
Public Class EquipmentHistoryAdminService
    Implements IEquipmentHistoryAdminService



    'Repositorio de la historia del equipo
    Private _EquipmentHistoryRepository As IEquipmentHistoryRepository

    ''' <summary>
    ''' inicia el repositorio de la historia del equipo
    ''' </summary>
    ''' <param name="EquipmentHistoryRepository">Repositorio de historia del equipo</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal EquipmentHistoryRepository As IEquipmentHistoryRepository)
        If (EquipmentHistoryRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de la historia del equipo")
        End If
        _EquipmentHistoryRepository = EquipmentHistoryRepository
    End Sub


    Public Function ListAllEquipmentHistory() As List(Of EquipmentHistory) Implements IEquipmentHistoryAdminService.ListAllEquipmentHistory
        Try
            Return _EquipmentHistoryRepository.ListAllEquipmentHistory
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
            _EquipmentHistoryRepository = Nothing
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
