'***********************************************************************
' Assembly         : Presentacion.Security.MVP
' Author           : Jhon Tovar
' Created          : 10-03-2022
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent

Public Class MProductCatalog
    Implements IDisposable

    ''' <summary>
    ''' Variable de session
    ''' </summary>
    Private _indigoSession As SessionValues = SessionValues.Instance
    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    Private disposedValue As Boolean ' To detect redundant calls

    ''' <summary>
    ''' Funcion para consultar por codigo
    ''' </summary>
    Public Async Function ConsultarNombreProductCatalog(ByVal codigo As String) As Threading.Tasks.Task(Of ProductCatalog)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetProductCatalogAsync(codigo, Me.Indigo)
    End Function

    ''' <summary>
    ''' Guardar el listado de objetos
    ''' </summary>
    Friend Async Function GuardarProductCatalog(ByVal product As ProductCatalog) As Threading.Tasks.Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.SaveProductCatalogAsync(product, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para Eliminar Seleccionado .
    ''' </summary>
    Friend Async Function EliminarProductCatalog(ByVal id As String) As Threading.Tasks.Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.DeleteProductCatalogAsync(id, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para Cambiar estado
    ''' </summary>
    Friend Async Function CambiarEstado(ByVal id As Integer, ByVal estado As Byte) As Threading.Tasks.Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.ChangeStateProductCatalogAsync(id, estado, Me.Indigo)
    End Function

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
End Class
