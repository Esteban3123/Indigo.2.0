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
''' Interfaz Formulario principal frmMDIPpal
''' </summary>
Public Interface IMdiPrincipal

#Region "Metodos"
    ''' <summary>
    ''' Metodo cambiar contraseña.
    ''' </summary>
    Sub AbrirFormCambiarContraseña(ByVal ChangePasswordAllUsers As Boolean)
    ''' <summary>
    ''' Metodo grupos.
    ''' </summary>
    Sub AbrirFormGrupos()
    ''' <summary>
    ''' Metodo configurar conexion.
    ''' </summary>
    Sub AbrirFormConfigurarConexion()
    ''' <summary>
    ''' Metodo roles.
    ''' </summary>
    Sub AbrirFormRoles()
    ''' <summary>
    ''' Metodo usuarios.
    ''' </summary>
    Sub AbrirFormUsuarios()
    ''' <summary>
    ''' Metodo desbloquear usuarios.
    ''' </summary>
    Sub AbrirFormDesbloquearUsuarios()
    ''' <summary>
    ''' Metodo cuando finaliza la autenticacion.
    ''' </summary>
    Sub AuthenticationFinalize()


#End Region

#Region "Propiedades"

    ''' <summary>
    ''' Obtiene o establece una empresa.
    ''' </summary>
    ''' <value>Empresa.</value>
    Property Empresa As String
    ''' <summary>
    ''' Obtiene o establece un usuario.
    ''' </summary>
    ''' <value>Usuario</value>
    Property Usuario As String
    ''' <summary>
    ''' Obtiene o establece el tema de devezpress guardado en un XML de configuracion
    ''' </summary>
    ''' <value>Fecha</value>
    Property TemaDevexpressGuardadoConfiguracion As String
    ''' <summary>
    ''' Obtiene o establece la version de la aplicacion.
    ''' </summary>
    ''' <value>Version</value>
    Property VersionAplicacion1 As String

#End Region

#Region "Complementos"

#End Region

End Interface
