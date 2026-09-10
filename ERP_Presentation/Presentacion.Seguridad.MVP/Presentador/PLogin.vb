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
#Region "Librerias Importadas"
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
#End Region
''' <summary>
''' Este presentador captura toda la logica aplicada en el frontal FrmLogin
''' </summary>
Public Class PLogin

#Region "Variables"

    ''' <summary>
    ''' Variable utilizada para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance
    ''' <summary>
    ''' Variable utilizada para instanciar la interfaz ILogin
    ''' </summary>
    Dim vista As ILogin
    ''' <summary>
    ''' Variable utilizada para la manipulacion del objeto [Entidad] SeguridadUsuario
    ''' </summary>
    Dim usuario As User
    ''' <summary>
    ''' Variable Controlar el numero de intentos de Contraseña Errada
    ''' </summary>
    Dim contadorIntentosContraseña As Integer
    ''' <summary>
    ''' Variable Controlar el codigo de usuario que esta intentando autenticarse
    ''' </summary>
    Dim ContadorIntentosContraseñaCodigoUsuario As String
    ''' <summary>
    ''' Variable para instanciar el Modelo
    ''' </summary>
    Dim modelo As MLogin

    ' Dim contenedor As Containers

#End Region

#Region "Metodos y Funciones"

    ' ''' <summary>
    ' ''' El metodo se dispara en el evento KeyDown del control de Empresa
    ' ''' </summary>
    'Public Sub ValidarUsuario()
    '    If vista.UsuarioAutenticadoCorrectamente = EstadoValidacionUsuario.NoAutenticado And vista.HabilitarGrupoSeguridad = True Then
    '        With vista
    '            If .Usuario = String.Empty Or .ContraseñaUsuario = String.Empty Then
    '                .MensajeAdvertencia = obtenerRecurso(ComunesDigiteDatos)
    '            Else
    '                Indigo.UserIndigo = vista.Usuario
    '                modelo = New MLogin

    '                If contadorIntentosContraseña = 3 And ContadorIntentosContraseñaCodigoUsuario = vista.Usuario Then
    '                    vista.MensajeAdvertencia = obtenerRecurso(LoginUsuarioBloqueado, Login)
    '                    Exit Sub
    '                ElseIf contadorIntentosContraseña = 3 And ContadorIntentosContraseñaCodigoUsuario <> vista.Usuario Then
    '                    contadorIntentosContraseña = 0
    '                End If

    '                If modelo.ValidarUsuario(vista.Usuario, vista.ContraseñaUsuario) = True Then
    '                    ConsultarUsuario()
    '                Else
    '                    vista.UsuarioAutenticadoCorrectamente = EstadoValidacionUsuario.NoAutenticado

    '                    If ContadorIntentosContraseñaCodigoUsuario = vista.Usuario Then
    '                        contadorIntentosContraseña += 1
    '                    Else
    '                        ContadorIntentosContraseñaCodigoUsuario = vista.Usuario
    '                        contadorIntentosContraseña = 1
    '                    End If
    '                    If contadorIntentosContraseña = 3 Then
    '                        vista.MensajeAdvertencia = obtenerRecurso(LoginUsuarioBloqueado, Login)
    '                    Else
    '                        vista.MensajeInformacion = String.Concat(obtenerRecurso(LoginIntentoContrasena, Login), " ", contadorIntentosContraseña, " ", obtenerRecurso(LoginIntentoContrasenaComplemento, Login))

    '                    End If
    '                    Limpiar()
    '                End If
    '            End If
    '        End With
    '    End If
    'End Sub


    'Public Sub ConsultarUsuario()
    '    usuario = New User
    '    modelo = New MLogin
    '    usuario = modelo.ConsultarUsuario(vista.Usuario)
    '    If usuario Is Nothing Then
    '        Throw New ArgumentNullException(BaseClass.obtenerExcepcion(EexceptionsResources.LoginUsuarioSinDatos))
    '        Exit Sub
    '    End If
    '    'Empresa = modelo.ConsultarEmpresa
    '    If CuentaEstaCaducada() = True Then
    '        Limpiar()
    '        vista.EstablecerFoco("INDTxtusuario") = True
    '        Exit Sub
    '    End If
    '    If usuario.State = False Then
    '        vista.MensajeAdvertencia = String.Concat(obtenerRecurso(LoginUsuarioInactivo, Login), ". ", obtenerRecurso(ComunesContacteAdministrador))
    '        Limpiar()
    '        vista.EstablecerFoco("INDTxtusuario") = True
    '        Exit Sub
    '    End If
    '    InicializarValoresdeSesion()
    '    If CambioContrasenaRequerido() = True Then    ' se dispara si requiere cambio de contraseña
    '        Limpiar()
    '        vista.EstablecerFoco("INDTxtusuario") = True
    '        Exit Sub
    '    End If

    '    Indigo.UserType = CType(usuario.UserType, UserType)
    '    If Indigo.UserType = UserType.Administrative Then
    '        Indigo.UserAdministrator = True
    '        UsuarioValidadoComoAdministrativo()
    '    Else
    '        Indigo.UserAdministrator = False
    '    End If
    'End Sub
    ''' <summary>
    ''' Este metodo es ejecutado cuando el usuario es de tipo administrativo
    ''' </summary>
    Private Sub UsuarioValidadoComoAdministrativo()
        VistaLogin(2)
        vista.UsuarioAutenticadoCorrectamente = EstadoValidacionUsuario.Autenticado
    End Sub




    ''' <summary>
    ''' Funcion que retorna true si el usuario requiere cambio de contraseña.
    ''' </summary>
    Private Function CambioContrasenaRequerido() As Boolean
        If usuario.ChangePassword = False Then

            If usuario.DateLastChangePassword Is Nothing Then
                Return False
            Else
                If usuario.DaysChangePassword > 0 Then

                    Dim fecha As Date = CDate(usuario.DateLastChangePassword).AddDays(CDbl(usuario.DaysChangePassword))
                    If modelo.ConsultarFechaServidor < fecha Then
                        Return False
                    Else
                        vista.MensajeInformacion = String.Concat(obtenerRecurso(LoginCambiarContrasena, Login), " ", obtenerRecurso(ComunesIndigoCrystal))
                        vista.MostrarFormularioCambiarContrasena = True
                        Return True
                    End If
                Else
                    Return False
                End If
            End If
        End If
        vista.MensajeInformacion = String.Concat(obtenerRecurso(LoginCambiarContrasena, Login), " ", obtenerRecurso(ComunesIndigoCrystal))
        vista.MostrarFormularioCambiarContrasena = True
        Return True
    End Function

    ''' <summary>
    ''' Funcion que retorna true si la cuenta ya a caducado
    ''' </summary>
    Private Function CuentaEstaCaducada() As Boolean
        Dim fechaFormato As Date = modelo.ConsultarFechaServidor
        If usuario.DateExpiryAccount Is Nothing Then
            Return False
        Else
            If fechaFormato > usuario.DateExpiryAccount Then

                vista.MensajeAdvertencia = String.Concat(obtenerRecurso(LoginCuentaCaduco, Login), " ", usuario.DateExpiryAccount, " .", obtenerRecurso(ComunesContacteAdministrador))
                Return True
            Else
                Return False
            End If
        End If
    End Function


    ''' <summary>
    ''' Inicializar los componentes.
    ''' </summary>
    Public Sub InicializarComponentes()
        vista.BtnAceptarEstaPresionado = False
        vista.UsuarioAutenticadoCorrectamente = EstadoValidacionUsuario.NoAutenticado
        VistaLogin(1)
    End Sub

    ''' <summary>
    ''' Este metodo Limpia todos los controles del frontal.
    ''' </summary>
    Public Sub Limpiar()
        With vista
            .Usuario = String.Empty
            .ContraseñaUsuario = String.Empty
            vista.BtnAceptarEstaPresionado = False
            vista.UsuarioAutenticadoCorrectamente = EstadoValidacionUsuario.NoAutenticado
            VistaLogin(1)
        End With
    End Sub


    ''' <summary>
    ''' Vistas del login: 1=Inicio  2=Administrativo  3=Asistencial
    ''' </summary>
    Private Sub VistaLogin(ByVal nivelPermiso As Integer)
        Select Case nivelPermiso
            Case 1
                vista.HabilitarControles = False
                vista.HabilitarGrupoSeguridad = True
                vista.HabilitarGuardarComo = False
            Case 2
                vista.HabilitarControles = False
                vista.HabilitarGrupoSeguridad = True
                vista.HabilitarGuardarComo = False
            Case Else
                vista.HabilitarControles = True
                vista.HabilitarGrupoSeguridad = False
                vista.HabilitarGuardarComo = True
        End Select
    End Sub

    ''' <summary>
    ''' Constructor de la vista.
    ''' </summary>
    ''' <param name="iview">The vista.</param>
    Public Sub New(ByVal iview As ILogin)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.vista = iview
        End If
    End Sub


#End Region

End Class
