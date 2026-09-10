'***********************************************************************
' Assembly         : Presentacion.Seguridad
' Author           : Jorge Leonardo Vernaza
' Created          : 21-12-2011
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Librerias Importadas"
Imports Presentation.Security.MVP
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent.IndigoReference.Security
Imports DevExpress.XtraEditors
Imports Domain.Security.Entities
#End Region

''' <summary>
''' Clase que contiene toda la funcionalidad del frontal session bloqueada
''' </summary>
''' 
Public Class FrmSesionBloqueada

#Region "VARIABLES Y LOAD"
    ''' <summary>
    ''' Instancia los valores de session
    ''' </summary>
    Dim indigo As SessionValues = SessionValues.Instance
    ''' <summary>
    ''' variable para instanciar el modelo
    ''' </summary>
    Dim Modelo As MBloqueoSession
    ''' <summary>
    ''' Variable para instanciar el objeto seguridad usuario
    ''' </summary>
    Dim Usuario As User
    ''' <summary>
    ''' Evento Load del formulario donde cargamos el control nombre de usuario con el nombre del usuario que tiene la session abierta
    ''' </summary>
    Private Sub FrmSesionBloqueada_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'obtenemos el nombre de usuario logeado
        INDlyUsuarioActual.Text = indigo.UserIndigoName
        INDlbNombreUsuario.Text = indigo.UserIndigoName
        'lanzamos el frontal maximizado
        Me.WindowState = FormWindowState.Maximized
        'hacemos que el panel quede en el centro
        INDpnTexto.Left = CInt((Me.Width - INDpnTexto.Width) / 2)
        INDpnTexto.Top = CInt((Me.Height - INDpnTexto.Height) / 2)
    End Sub
#End Region

#Region "DISEÑO"
    ''' <summary>
    ''' Evento del mouse sobre el boton continuar que cambia el color del boton
    ''' </summary>
    Private Sub btnyes_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs)
        btnContinuar.BackColor = Color.LightSkyBlue
    End Sub

    ''' <summary>
    ''' Evento del mouse que ya no esta sobre el control para que devuelva al color original
    ''' </summary>
    Private Sub btnyes_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        btnContinuar.BackColor = Color.Transparent
    End Sub

    ''' <summary>
    ''' Evento del mouse sobre el boton atras que cambia el color del boton
    ''' </summary>
    Private Sub PictureEdit2_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles INDpeFlecha.MouseEnter
        'INDpeFlecha.Image = Global.Presentation.Security.My.Resources.Resources.atras21
    End Sub

    ''' <summary>
    ''' Evento del mouse que ya no esta sobre el control para que devuelva al color original
    ''' </summary>
    Private Sub PictureEdit2_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles INDpeFlecha.MouseLeave
        'INDpeFlecha.Image = Global.Presentation.Security.My.Resources.Resources.atras11
    End Sub

    ''' <summary>
    ''' Evento text changed de los controles usuario y contraseña para cambiar los colores 
    ''' </summary>
    Private Sub INDTxtUsuario_TextChanged(sender As Object, e As EventArgs) Handles INDtxtUsuario.TextChanged, INDTxtContrasena.TextChanged, INDtxtContraseñaAdministrador.TextChanged
        Dim Obj = CType(sender, TextEdit)
        If Obj.Text = String.Empty Then
            Obj.BackColor = Color.MistyRose
        Else
            Obj.BackColor = Color.White
        End If
    End Sub
#End Region

#Region "PROPIEDADES"
    Property _ActivarTimer As Boolean
    ''' <summary>
    ''' Propiedad que obtiene si se activa o no el timer para comprovar la activida en el sistema
    ''' </summary>
    Public ReadOnly Property ActivarTimer As Boolean
        Get
            Return _ActivarTimer
        End Get
    End Property

    Property _CerrarSession As Boolean
    ''' <summary>
    ''' Propiedad que obtiene si se cierra la session en caso de que sea un usuario administrador
    ''' </summary>
    Public ReadOnly Property CerrarSession As Boolean
        Get
            Return _CerrarSession
        End Get
    End Property
#End Region

#Region "EVENTOS CLIC"
    ''' <summary>
    ''' Evento Clic en el boton continuar para desbloquear la session o cerrar la session con un administrador
    ''' </summary>
    Private Async Sub btnContinuar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnContinuar.Click
        Using Modelo As New MBloqueoSession
            'si el layout administrador es visible quiere decir que se va a autenticar un usuario administrador para cerrar la session
            If INDlayoutAdministrador.Visible = True Then
                Using modelou = New MUsuario
                    Usuario = Await modelou.ConsultarUsuario(INDtxtUsuario.Text)
                End Using
                If Usuario Is Nothing Then
                    INDtxtUsuario.Text = String.Empty
                    INDtxtContraseñaAdministrador.Text = String.Empty
                    INDlbMensaje.Text = "No se encontró el usuario."
                    INDtxtUsuario.Focus()
                    Exit Sub
                End If
                'si el usuario y la contraseña no estan vacios entonces validamos el usuario
                If INDtxtUsuario.Text <> String.Empty And INDtxtContraseñaAdministrador.Text <> String.Empty Then
                    'si la validacion de usuario y contraseña es correcta entonces consultamos el usuario
                    'If Modelo.ValidarUsuario(INDtxtUsuario.Text, INDtxtContraseñaAdministrador.Text) = True Then
                    If Modelo.ValidarUsuario(Usuario.Id, INDtxtContraseñaAdministrador.Text) = True Then
                        'Usuario = Modelo.ConsultarUsuario(INDtxtUsuario.Text)
                        If Not Usuario Is Nothing Then
                            'Elimino los espacion del objeto devuelto por el serivico
                            Usuario.Position = Usuario.Position.Trim
                            Usuario.Person.Fullname = Usuario.Person.Fullname.Trim
                        End If
                        'indigo.UserType = CType(UserType.Administrative, UserType)
                        'si el usuario es administrador entoces igualamos la propiedad cerrar sessio a true para cerrar la session desde el mdi
                        'If indigo.UserType = UserType.Administrative Then
                        If Usuario.UserType = "1" OrElse Usuario.UserType = "3" Then 'Administrativo o super administrador
                            Dim preguntar As New FrmConfirmacion
                            preguntar.ShowDialog()
                            If preguntar.valor = True Then
                                _CerrarSession = True
                                'cerramos el frontal de bloqueo
                                Me.Close()
                            Else

                            End If
                        Else
                            INDtxtUsuario.Text = String.Empty
                            INDtxtContraseñaAdministrador.Text = String.Empty
                            INDlbMensaje.Text = "El Usuario Ingresador No Tiene Permisos De Administrador Del Sistema"
                            INDtxtUsuario.Focus()
                        End If
                    Else
                        'si el nombre de usuario y contraseña no son validos entonces me limpie los campos y me muestre un mensaje 
                        INDtxtUsuario.Text = String.Empty
                        INDtxtContraseñaAdministrador.Text = String.Empty
                        INDlbMensaje.Text = "El Nombre De Usuario o La Contraseña No Son Validos"
                        INDtxtUsuario.Focus()
                        _ActivarTimer = False
                    End If
                Else
                    'si no se han ingresado los datos en los textbox entonces me mande un mensaje
                    INDtxtUsuario.Text = String.Empty
                    INDtxtContraseñaAdministrador.Text = String.Empty
                    INDlbMensaje.Text = "Por Favor Digite Los Datos Solicitados"
                    INDtxtUsuario.Focus()
                End If
                'si el lauout usuario es visible entonces se va loguear al usuario que tiene la session activa
            ElseIf INDlayoutUsuario.Visible = True Then
                'si ingresa la contraseña procedemos a validar los datos de usuario que estan en los valores de session y la contraseña que acaba de ingresar
                If INDTxtContrasena.Text <> String.Empty Then
                    'si la validacion de usuario y contraseña es correcta entonces cerramos el frontal de bloqueo y activamos el timer
                    'If Modelo.ValidarUsuario(indigo.UserIndigo, INDTxtContrasena.Text) = True Then
                    If Modelo.ValidarUsuario(indigo.UserIndigoId, INDTxtContrasena.Text) = True Then
                        Me.Close()
                        _ActivarTimer = True
                    Else
                        'si la validar el usuario no es true entonces se ingreso mal la contraseña
                        INDTxtContrasena.Text = String.Empty
                        INDlbMensaje.Text = "Contraseña Incorrecta"
                        INDTxtContrasena.Focus()
                        _ActivarTimer = False
                    End If
                Else
                    'si se da clic en continuar si igresar una contraseña mostramos un mensaje
                    INDlbMensaje.Text = "Por Favor Digite La Contraseña"
                    INDTxtContrasena.Focus()
                End If
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Evento Clic en la la flecha de "Atras" me hace visible los layouts para escoger si logear el usuario actual o finalizar la session
    ''' </summary>
    Private Sub INDpeFlecha_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles INDpeFlecha.Click
        INDlayoutAdministrador.Visible = False
        btnContinuar.Visible = False
        INDlayoutUsuario.Visible = False
        INDpeFlecha.Visible = False
        INDlyUser.Visible = True
        INDlyAdmon.Visible = True
        INDTxtContrasena.Text = String.Empty
        INDtxtUsuario.Text = String.Empty
        INDtxtContraseñaAdministrador.Text = String.Empty
        INDlbMensaje.Text = String.Empty
    End Sub

    ''' <summary>
    ''' Evento Clic en la en item Administrador para cerrar la session actual solo si es administrador
    ''' </summary>
    Private Sub Administrador_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureEdit4.Click
        INDlyUser.Visible = False
        INDlyAdmon.Visible = False
        INDlayoutUsuario.Visible = False
        INDlayoutAdministrador.Location = New Point(149, 58)
        btnContinuar.Location = New Point(727, 230)
        INDlbMensaje.Location = New Point(365, 14)
        INDlayoutAdministrador.Visible = True
        btnContinuar.Visible = True
        INDpeFlecha.Visible = True
        INDtxtUsuario.Focus()
        btnContinuar.BringToFront()
    End Sub

    ''' <summary>
    ''' Evento Clic en la en item Usuario Actual para iniciar session con el usuario que estaba logeado
    ''' </summary>
    Private Sub Usuario_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureEdit1.Click
        INDlyUser.Visible = False
        INDlyAdmon.Visible = False
        INDlayoutAdministrador.Visible = False
        INDlayoutUsuario.Location = New Point(149, 58)
        btnContinuar.Location = New Point(727, 207)
        INDlbMensaje.Location = New Point(365, 14)
        INDlayoutUsuario.Visible = True
        INDlbNombreUsuario.Text = indigo.UserIndigoName
        NombreUsuario.Text = indigo.UserIndigoName
        btnContinuar.Visible = True
        INDpeFlecha.Visible = True
        btnContinuar.Focus()
        INDTxtContrasena.Focus()
        btnContinuar.BringToFront()
    End Sub
#End Region

#Region "EVENTOS KEYDOWN"
    ''' <summary>
    ''' Evento Keydown del formulario donde se captura la combinacion de teclas para mostrar los controles de login
    ''' </summary>
    Private Sub FrmSesionBloqueada_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
        If e.Alt = True And e.Control = True And e.KeyCode = Keys.I Then
            INDlbPresione.Visible = False
            INDlyUser.Visible = True
            INDlyAdmon.Visible = True
        End If
    End Sub

    ''' <summary>
    ''' Eventos keydown Para detectar que se estan presionando la combinacion de teclas alt + f4 e impedir que se cierre la aplicacion de
    ''' session bloqueada
    ''' </summary>
    Private Sub INDTxtContrasena2_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        INDlbMensaje.Text = String.Empty
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub

    Private Sub LabelControl3_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles LabelControl3.KeyDown
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub

    Private Sub INDlbPresione_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles INDlbPresione.KeyDown
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub

    Private Sub PictureEdit2_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles PictureEdit2.KeyDown
        If e.Alt = True And e.Control = True And e.KeyCode = Keys.I Then
            Me.Size = New Size(572, 237)
            INDlbPresione.Visible = False
            INDlyUser.Visible = True
            INDlyAdmon.Visible = True
        End If
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub

    Private Sub INDtxtUsuario_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        INDlbMensaje.Text = String.Empty
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub

    Private Sub INDtxtContraseñaAdministrador_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        INDlbMensaje.Text = String.Empty
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub

    Private Sub btnContinuar_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub

    Private Sub PictureEdit4_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub

    Private Sub PictureEdit3_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub

    Private Sub PictureEdit1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles PictureEdit1.KeyDown
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub

    Private Sub INDlyAdmon_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles INDlyAdmon.KeyDown
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub

    Private Sub INDlyUser_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles INDlyUser.KeyDown
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub

    Private Sub INDpeFlecha_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles INDpeFlecha.KeyDown
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub
#End Region

End Class