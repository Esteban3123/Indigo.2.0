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
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eresources
Public Class PModule

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IModule

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Variable utilizada para el objeto Modules
    ''' </summary>
    Private _modules As Modules
    Property Modules As Modules
        Get
            Return _modules
        End Get
        Set(value As Modules)
            _modules = value
        End Set
    End Property

    ''' <summary>
    ''' Variable para almacenar ProductCatalog
    ''' </summary>
    Public listProductCatalog As List(Of ProductCatalog) = Nothing
    ''' <summary>
    ''' Variable para almacenar Forms
    ''' </summary>
    Public FormDataSource As DevExpress.Xpo.XPServerCollectionSource = Nothing

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IModule)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

    ''' <summary>
    ''' Este metodo Consulta el nombre.
    ''' </summary>
    ''' <param name="codigo">The codigo.</param> 
    Public Async Function ConsultarNombreModule(ByVal codigo As Integer) As Threading.Tasks.Task
        Dim modelo As New MModule
        Modules = Await modelo.ConsultarNombreModule(codigo)
    End Function

    ''' <summary>
    ''' este metodo sirve para guardar
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Function GuardarModule(modules As Modules) As Threading.Tasks.Task(Of Boolean)
        Dim modelo As New MModule
        Dim resultado = Await modelo.GuardarModule(modules)
        If resultado = True Then
            View.Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
        Else
            View.Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
        End If
        Return resultado
    End Function

    ''' <summary>
    ''' Este metodo elimina
    ''' </summary>
    ''' <param name="codigo"></param>
    ''' <remarks></remarks>
    Public Async Function EliminarModule(ByVal codigo As Integer) As Threading.Tasks.Task(Of Boolean)
        Dim modelo As New MModule
        Dim resultado = Await modelo.EliminarModule(codigo)
        If resultado = True Then
            View.Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
        Else
            View.Mensaje(EeventViewerImages.MensajeError) = "Se presento un error, el objeto no se pudo borrar"
        End If

        Return resultado
    End Function

    ''' <summary>
    ''' Este metodo actualiza estados
    ''' </summary>
    ''' <param name="codigo"></param>
    ''' <param name="estado"></param>
    ''' <remarks></remarks>
    Public Async Function CambiarEstado(ByVal codigo As Integer, ByVal estado As Byte) As Threading.Tasks.Task(Of Boolean)
        Dim modelo As New MModule
        Dim resultado = Await modelo.CambiarEstado(codigo, estado)
        If resultado = True Then
            View.Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
        Else
            View.Mensaje(EeventViewerImages.MensajeError) = "Se presento un error"
        End If

        Return resultado
    End Function


    ''' <summary>
    ''' Este metodo Consulta listado de titulos de Xpo.
    ''' </summary>
    ''' <param name="codigo">The codigo.</param> 
    Public Function ConsultarTodosTitle() As DevExpress.Xpo.XPServerCollectionSource
        Using modelo As New MModule
            Return modelo.ConsultarTodosTitle()
        End Using
    End Function

    ''' <summary>
    ''' Este metodo Consulta listado de Formularios Xpo.
    ''' </summary>
    ''' <param name="codigo">The codigo.</param> 
    Public Sub ConsultarTodosForm()
        If FormDataSource Is Nothing Then
            Using modelo As New MModule
                FormDataSource = modelo.ConsultarTodosVieForm()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Este metodo Consulta listado de Productos.
    ''' </summary>
    ''' <param name="codigo">The codigo.</param> 
    Public Async Function ListarProductos() As Threading.Tasks.Task(Of List(Of ProductCatalog))
        Dim modelo As New MModule
        If listProductCatalog Is Nothing Then
            listProductCatalog = Await modelo.ListarProductos()
        End If
        Return listProductCatalog
    End Function


End Class
