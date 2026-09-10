Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Base.Eresources
Imports Presentation.Base.BaseClass
Imports Infrastructure.CrossCutting.Resources

Public Class PFormulario

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IFormulario

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Variable utilizada para el objeto
    ''' </summary>
    Private _formulario As VieDBForm
    Property Formulario As VieDBForm
        Get
            Return _formulario
        End Get
        Set(value As VieDBForm)
            _formulario = value
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
    Public Sub New(ByRef iview As IFormulario)
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
    Public Async Function ConsultarNombreFormulario(ByVal codigo As Integer) As Threading.Tasks.Task
        Dim modelo As New MFormulario
        Formulario = Await modelo.ConsultarNombreFormulario(codigo)
    End Function

    ''' <summary>
    ''' este metodo sirve para guardar
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Function GuardarFormulario(vieDBForm As VieDBForm) As Threading.Tasks.Task(Of Boolean)
        Dim modelo As New MFormulario
        Dim resultado = Await modelo.GuardarFormulario(vieDBForm)
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
    Public Async Function EliminarFormulario(ByVal codigo As Integer) As Threading.Tasks.Task(Of Boolean)
        Dim modelo As New MFormulario
        Dim resultado = Await modelo.EliminarFormulario(codigo)
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
        Dim modelo As New MFormulario
        Dim resultado = Await modelo.CambiarEstado(codigo, estado)
        If resultado = True Then
            View.Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
        Else
            View.Mensaje(EeventViewerImages.MensajeError) = "Se presento un error"
        End If

        Return resultado
    End Function

    ''' <summary>
    ''' Este metodo Consulta el llstado de FormAction.
    ''' </summary>
    ''' <param name="codigo">The codigo.</param> 
    Public Async Function ConsultarFormActionFormulario(ByVal codigo As Integer) As Threading.Tasks.Task
        Dim modelo As New MFormulario
        Formulario.ListFormAction = Await modelo.ConsultarFormActionFormulario(codigo)
    End Function


End Class
