
#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base
Imports Application.Base
Imports Infrastructure.CrossCutting.Exceptions

#End Region


''' <summary>
''' interface que especifica las metodos y funciones que manejara todas las acciones sobre la entidad poliza
''' </summary>
''' <remarks></remarks>
Public Class PhysicalRiskAdminService
    Implements IPhysicalRiskAdminService

    'Repositorio de tipo de fabricante
    Private _PhisicalRiskRepository As IPhisicalRiskRepository

    ''' <summary>
    ''' inicia el repositorio de fabricante
    ''' </summary>
    ''' <param name="PhisicalRiskRepository">Repositorio de fabricante</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal PhisicalRiskRepository As IPhisicalRiskRepository)
        If (PhisicalRiskRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de riesgo de equipo")
        End If
        _PhisicalRiskRepository = PhisicalRiskRepository
    End Sub

    Public Function ListAllPhysicalRisk() As List(Of PhysicalRisk) Implements IPhysicalRiskAdminService.ListAllPhysicalRisk
        Try
            Return _PhisicalRiskRepository.ListAllPhysicalRisk
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
            _PhisicalRiskRepository = Nothing
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
