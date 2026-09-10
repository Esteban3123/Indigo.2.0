Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base

Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform
Imports Infrastructure.CrossCutting.Resources
Imports Microsoft.VisualBasic

Public Class PTitulo

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As ITitulo

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Variable utilizada para el objeto
    ''' </summary>
    Private _titulo As Title
    Property Titulo As Title
        Get
            Return _titulo
        End Get
        Set(value As Title)
            _titulo = value
        End Set
    End Property
    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As ITitulo)
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
    Public Async Function ConsultarNombreTitulo(ByVal codigo As Integer) As Threading.Tasks.Task
        Using modelo As New MTitulo
            Titulo = Await modelo.ConsultarNombreTitulo(codigo)
        End Using
    End Function

    ''' <summary>
    ''' este metodo sirve para guardar
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Function GuardarTitulo() As Threading.Tasks.Task(Of Boolean)
        Dim resultado As Boolean = False
        Dim _title = New Title() With {
            .IdTitle = View.IdTitle,
            .TitleName = View.TitleName,
            .Crud = View.Crud
        }

        Using modelo As New MTitulo
            resultado = Await modelo.GuardarTitulo(_title)
            If resultado = True Then
                View.Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
            Else
                View.Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
            End If
        End Using

        Return resultado
    End Function

    ''' <summary>
    ''' Este metodo elimina
    ''' </summary>
    ''' <param name="codigo"></param>
    ''' <remarks></remarks>
    Public Async Function EliminarTitulo(ByVal codigo As Integer) As Threading.Tasks.Task(Of Boolean)
        Dim resultado As Boolean = False
        Using modelo As New MTitulo
            resultado = Await modelo.EliminarTitulo(codigo)
            If resultado = True Then
                View.Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
            Else
                View.Mensaje(EeventViewerImages.MensajeError) = "Se presento un error, el objeto no se pudo borrar"
            End If
        End Using
        Return resultado
    End Function

    ''' <summary>
    ''' Este metodo actualiza estados
    ''' </summary>
    ''' <param name="codigo"></param>
    ''' <param name="estado"></param>
    ''' <remarks></remarks>
    Public Async Function CambiarEstado(ByVal codigo As Integer, ByVal estado As Byte) As Threading.Tasks.Task(Of Boolean)
        Dim resultado As Boolean = False
        Using modelo As New MTitulo
            resultado = Await modelo.CambiarEstado(codigo, estado)
            If resultado = True Then
                View.Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
            Else
                View.Mensaje(EeventViewerImages.MensajeError) = "Se presento un error"
            End If
        End Using
        Return resultado
    End Function

End Class
