Imports Presentation.Base   
Imports Presentation.Controls
'***********************************************************************
' Assembly         : Presentacion.Cliente.MVP
' Author           : Andres Bonilla
' Created          : 03-03-2011
'
' Last Modified By : Jose Paez
' Last Modified On : 23-03-2011
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

''' <summary>
''' Enumeracion para mostrar el estado de validacion del usuario
''' </summary>
Public Enum EstadoValidacionUsuario
    ''' <summary>
    ''' usuario sin autenticacion
    ''' </summary>
    SinAutenticacion = 1
    ''' <summary>
    ''' usuario autenticado
    ''' </summary>
    Autenticado = 2
    ''' <summary>
    ''' usuario no autenticado
    ''' </summary>
    NoAutenticado = 3
End Enum

''' <summary>
''' Definicion de Interfaz que contiene metodos y propiedades  para ser implementada en el formulario FrmLogin
''' </summary>
Public Interface ILogin

#Region "Propiedades"

    ''' <summary>
    ''' Esta propiedad contiene el nombre de usuario.
    ''' </summary>
    Property Usuario As String
    ''' <summary>
    ''' Esta propiedad contiene la contraseña del usuario
    ''' </summary>
    Property ContraseñaUsuario As String
    ''' <summary>
    ''' Esta propiedad contiene el Rol del usuario a ingresar.
    ''' </summary>
    ''' <value>The rol.</value>
    Property RolUsuario As String
    ''' <summary>
    ''' Esta propiedad contiene los mensaje de advertencia declarados.
    ''' </summary>
    WriteOnly Property MensajeAdvertencia As String
    ''' <summary>
    ''' Esta propiedad contiene los mensaje de informacion.
    ''' </summary>
    WriteOnly Property MensajeInformacion As String
    ''' <summary>
    ''' Esta propiedad contiene los mensaje de error.
    ''' </summary>
    WriteOnly Property MensajeError As String
    ''' <summary>
    ''' Obtiene o establece el perfil seleccionado en el login
    ''' </summary>
    Property PerfilSeleccionadoPropiedad As Integer
    ''' <summary>
    ''' Propiedad para Habilitar Controles
    ''' </summary>
    ''' <value><c>true</c> Habilita Controles; Deshabilita Controles, <c>false</c>.</value>
    Property HabilitarControles As Boolean
    ''' <summary>
    ''' Propiedad para Habilitar el Grupo de Seguridad
    ''' </summary>
    Property HabilitarGrupoSeguridad As Boolean
    ''' <summary>
    ''' Propiedad para Habilitar el control Unidad Funcional
    ''' </summary>
    ''' <value>
    ''' <c>true</c> Habilitar el Control; Deshabilita el Control, <c>false</c>.
    ''' </value>
    Property HabilitarUnidadFuncional As Boolean
    ''' <summary>
    ''' Propiedad para Habilitar el control Unidad Funcional
    ''' </summary>
    WriteOnly Property HabilitarGuardarComo As Boolean
    ''' <summary>
    ''' Establece el DataSource de la empresa
    ''' </summary>
    WriteOnly Property EmpresaDataSource As Object
    ''' <summary>
    ''' Establece si el Usuario es Administrativo
    ''' </summary>
    Property UsuarioAutenticadoCorrectamente As EstadoValidacionUsuario
    ''' <summary>
    ''' propiedad que me dice si debo mostrar formulario cambiar contraseña.
    ''' </summary>
    Property MostrarFormularioCambiarContrasena As Boolean
    ''' <summary>
    ''' Propiedad que me establece si el BtnAceptar a sido presionado
    ''' </summary>
    Property BtnAceptarEstaPresionado As Boolean
    ''' <summary>
    ''' Establece el foco en un campo especifico.
    ''' </summary>
    WriteOnly Property EstablecerFoco(NombreControl As String) As Boolean



#End Region

#Region "Metodos"

    ''' <summary>
    ''' MeTODO: cancela el inicio de de sesion y cierra la aplicacion.
    ''' </summary>
    Sub Cancelar()

    ''' <summary>
    ''' METODO: Limpia valores Ingresados en los campos de login.
    ''' </summary>
    Sub Limpiar()

    ''' <summary>
    ''' METODO:envia parametros para guardar Inicios de sesion.
    ''' </summary>
    Sub GuardarComo()



#End Region

End Interface
