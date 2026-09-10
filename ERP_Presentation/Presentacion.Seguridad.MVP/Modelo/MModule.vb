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
Imports DevExpress.Xpo
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.CloudAgent
Public Class MModule
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
    ''' Funcion para consultar modulo por codigo
    ''' </summary>
    Public Async Function ConsultarNombreModule(ByVal codigo As String) As Threading.Tasks.Task(Of Modules)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetModuleAsync(codigo, Me.Indigo)
    End Function

    ''' <summary>
    ''' Guardar el listado de objetos de Modules
    ''' </summary>
    Friend Async Function GuardarModule(ByVal modules As Modules) As Threading.Tasks.Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.SaveModuleAsync(modules, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para Eliminar modulo Seleccionado .
    ''' </summary>
    Friend Async Function EliminarModule(ByVal id As String) As Threading.Tasks.Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.DeleteModuleAsync(id, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para Cambiar estado
    ''' </summary>
    Friend Async Function CambiarEstado(ByVal id As Integer, ByVal estado As Byte) As Threading.Tasks.Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.ChangeStateModuleAsync(id, estado, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para consultar los productos por codigo
    ''' </summary>
    Public Async Function ListarProductos() As Threading.Tasks.Task(Of List(Of ProductCatalog))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.ListProductCatalogAsync(Me.Indigo)
    End Function


    ''' <summary>
    ''' Consualtar todos los titulos Xpo
    ''' </summary>
    ''' <param name="UserTenantId"></param>
    ''' <param name="pUserType"></param>
    ''' <returns></returns>
    Public Function ConsultarTodosTitle() As XPServerCollectionSource
        Return XpoServiceEx.Instance(Me.Indigo.SecurityContainer).SecurityService.ListTitle()
    End Function

    ''' <summary>
    ''' Consualtar todos los Formularios Xpo
    ''' </summary>
    ''' <param name="UserTenantId"></param>
    ''' <param name="pUserType"></param>
    ''' <returns></returns>
    Public Function ConsultarTodosVieForm() As XPServerCollectionSource
        Return XpoServiceEx.Instance(Me.Indigo.SecurityContainer).SecurityService.ListVieForm()
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
