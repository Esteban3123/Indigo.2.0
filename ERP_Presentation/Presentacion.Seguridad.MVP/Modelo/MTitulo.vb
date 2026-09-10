Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent

Public Class MTitulo
    Implements IDisposable

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    Private disposedValue As Boolean ' To detect redundant calls

    ''' <summary>
    ''' Funcion para consultar Titulo
    ''' </summary>
    Public Async Function ConsultarNombreTitulo(ByVal codigo As String) As Threading.Tasks.Task(Of Title)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetTitleAsync(codigo, Me.Indigo)
    End Function

    ''' <summary>
    ''' Guardar el listado de objetos de Title
    ''' </summary>
    Public Async Function GuardarTitulo(ByVal title As Title) As Threading.Tasks.Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.SaveTitleAsync(title, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para Eliminar titulo Seleccionado.
    ''' </summary>
    Friend Async Function EliminarTitulo(ByVal IdTitle As String) As Threading.Tasks.Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.DeleteTitleAsync(IdTitle, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para Cambiar estado
    ''' </summary>
    Friend Async Function CambiarEstado(ByVal id As Integer, ByVal estado As Byte) As Threading.Tasks.Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.ChangeStateTitleAsync(id, estado, Me.Indigo)
    End Function

#Region "IDisposable"
    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
