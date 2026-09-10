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

Public Class PProductCatalog

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IProductCatalog

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Variable utilizada para el objeto ProductCatalog
    ''' </summary>
    Private _productCatalog As ProductCatalog
    Property productCatalog As ProductCatalog
        Get
            Return _productCatalog
        End Get
        Set(value As ProductCatalog)
            _productCatalog = value
        End Set
    End Property

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IProductCatalog)
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
    Public Async Function ConsultarNombreProductCatalog(ByVal codigo As Integer) As Threading.Tasks.Task
        Dim modelo As New MProductCatalog
        productCatalog = Await modelo.ConsultarNombreProductCatalog(codigo)
    End Function

    ''' <summary>
    ''' este metodo sirve para guardar
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Function GuardarProductCatalog() As Threading.Tasks.Task(Of Boolean)
        Dim modelo As New MProductCatalog
        Dim productCatalog = New ProductCatalog() With {
            .IdProduct = View.IdProduct,
            .ProductName = View.ProductName,
            .PlatformName = View.PlatformName,
            .SuiteName = View.SuiteName,
            .Visible = View.Visible,
            .Crud = View.Crud
        }

        Dim resultado = Await modelo.GuardarProductCatalog(productCatalog)
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
    Public Async Function EliminarProductCatalog(ByVal codigo As Integer) As Threading.Tasks.Task(Of Boolean)
        Dim modelo As New MProductCatalog
        Dim resultado = Await modelo.EliminarProductCatalog(codigo)
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
        Dim modelo As New MProductCatalog
        Dim resultado = Await modelo.CambiarEstado(codigo, estado)
        If resultado = True Then
            View.Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
        Else
            View.Mensaje(EeventViewerImages.MensajeError) = "Se presento un error"
        End If

        Return resultado
    End Function

End Class
