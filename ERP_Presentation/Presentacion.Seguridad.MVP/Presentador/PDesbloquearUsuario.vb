
'***********************************************************************
' Assembly         : Presentacion.Seguridad.MVP
' Author           : Andres Bonilla
' Created          : 03-03-2011
'
' Last Modified By : Andres Bonilla
' Last Modified On : 29-03-2011
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Importaciones"
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform
Imports Presentation.CloudAgent.IndigoReference.Security
Imports Domain.Security.Entities
#End Region

''' <summary>
''' Este presentador captura toda la logica aplicada en el frontal FrmDesbloquearUsuario
''' </summary>
Public Class PDesbloquearUsuario

#Region "variables y Constructor"

    ''' <summary>
    ''' Constructor que permite la comunicacion con la interfaz IDesbloquearusuario
    ''' </summary>
    ''' <param name="iview">The iview.</param>
    Public Sub New(ByRef iview As IDesbloquearUsuario)
        If iview Is Nothing Then
            Throw New ArgumentException(obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.vista = iview
        End If
    End Sub

    ''' <summary>
    ''' Variable utilizada para instanciar la interfaz IDesbloquearUsuario
    ''' </summary>
    Dim vista As IDesbloquearUsuario
    ''' <summary>
    ''' Variable del Objeto de tipo SeguridadUsuario
    ''' </summary>
    Dim usuario As User
    ''' <summary>
    '''  Variable que se Utiliza para Instanciar el modelo
    ''' </summary>
    Dim modelo As MDesbloquearUsuario

#End Region

#Region "metodos"

    ''' <summary>
    ''' Metodo que se utiliza para iniciar componentes al cargar el funcional.
    ''' </summary>
    Public Sub InicializarComponentes()
        vista.HabilitarControles = False
    End Sub

    '''' <summary>
    '''' este metodo sirve para guardar la nueva configuracion del usuario
    '''' </summary>
    '''' <remarks></remarks>
    'Public Sub GuardarConfiguracionUsuario()
    '    ConsultarUsuario(vista.CodigoDelUsuario)
    'End Sub

    ''' <summary>
    ''' Este metodo sirve para borrar el contenido de los campos del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer()
        vista.CodigoDelUsuario = String.Empty
        vista.NombreDelUsuario = String.Empty
    End Sub

    ''' <summary>
    ''' Metodo que permite Consultar por el codigo a los usuarios existentes
    ''' </summary>
    Public Async Sub ConsultarUsuario(ByVal codigoUsuario As String)

        If String.IsNullOrEmpty(vista.CodigoDelUsuario) Then
            vista.Mensaje(EeventViewerImages.Advertencia) = String.Concat(obtenerRecurso(ComunesCodigoVacio), " ", obtenerRecurso(UsuarioMensajeComplemento, Eform.Usuario))
        Else

            Dim modelo As New MDesbloquearUsuario
            vista.AsyncLoader(True)
            'usuario = Await modelo.ConsultarUsuarioCommand(codigoUsuario)
            Using modelou = New MUsuario
                usuario = Await modelou.ConsultarUsuario(codigoUsuario)
            End Using

            If usuario Is Nothing OrElse usuario.Id = 0 Then
                vista.AsyncLoader(False)
                vista.Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(UsuarioNoExiste, Eform.Usuario)
                Deshacer()
            Else
                If vista.CodigoDelUsuario.Trim = usuario.UserCode.Trim OrElse vista.CodigoDelUsuario.Trim = usuario.Person.Identification Then
                    vista.NombreDelUsuario = usuario.Person.Fullname.Trim
                    If Await modelo.DesbloquearUsuario(usuario.UserCode) = True Then
                        Await modelo.DesbloquearRegistros(usuario.UserCode)
                        vista.Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(DesbloquearUsuarioDesbloqueado, DesbloquearUsuario)
                    Else
                        vista.Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(DesbloquearUsuarioNOdesbloqueado, DesbloquearUsuario)
                        'excepcion FALTA: capa excepciones.
                    End If
                    vista.AsyncLoader(False)
                Else
                    vista.AsyncLoader(False)
                End If
            End If
        End If
    End Sub

#End Region

End Class
