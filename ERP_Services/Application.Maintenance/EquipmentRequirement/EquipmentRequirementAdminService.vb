#Region "Imports"
Imports Domain.Base
Imports Infrastructure.CrossCutting.Base
Imports Application.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities

#End Region

Public Class EquipmentRequirementAdminService
    Implements IEquipmentRequirementAdminService

    'Repositorio de el requerimiento del equipo
    Private _EquipmentRequirementRepository As IEquipmentRequirementRepository

    ''' <summary>
    ''' inicia el repositorio de requerimiento del equipo
    ''' </summary>
    ''' <param name="EquipmentRequirementRepository">Repositorio de requerimiento del equipo</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal EquipmentRequirementRepository As IEquipmentRequirementRepository)
        If (EquipmentRequirementRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio del requerimiento del equipo")
        End If
        _EquipmentRequirementRepository = EquipmentRequirementRepository
    End Sub


    Public Function ListAllEquipmentRequirement() As List(Of EquipmentRequirement) Implements IEquipmentRequirementAdminService.ListAllEquipmentRequirement
        Try
            Return _EquipmentRequirementRepository.ListAllEquipmentRequirement
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
            _EquipmentRequirementRepository = Nothing
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
