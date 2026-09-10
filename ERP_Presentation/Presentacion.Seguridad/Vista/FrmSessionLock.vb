'***********************************************************************
' Assembly         : Presentacion.Seguridad
' Author           : Jorge Leonardo Vernaza
' Created          : 16-05-2013
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
Public Class FrmSessionLock

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
        INDpnTexto.Location = New Point(CInt((MyBase.Width / 2) - (INDpnTexto.Width / 2)), CInt((MyBase.Height / 2) - (INDpnTexto.Height / 2)))
        'lanzamos el frontal maximizado
        Me.WindowState = FormWindowState.Maximized
        'hacemos que el panel quede en el centro
    End Sub
#End Region

#Region "DISEÑO"

    ''' <summary>
    ''' Evento text changed de los controles usuario y contraseña para cambiar los colores 
    ''' </summary>
    Private Sub INDTxtUsuario_TextChanged(sender As Object, e As EventArgs) Handles INDtxtUsuario.TextChanged, INDtxtContraseña.TextChanged
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
    Private Async Sub btnContinuar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles INDbtnEntrar.Click
        Using Modelo As New MBloqueoSession
            'si el usuario y la contraseña no estan vacios entonces validamos el usuario
            If INDtxtUsuario.Text <> String.Empty And INDtxtContraseña.Text <> String.Empty Then
                If INDtxtUsuario.Text = indigo.UserIndigo Then
                    'si la validacion de usuario y contraseña es correcta entonces cerramos el frontal de bloqueo y activamos el timer
                    'If Modelo.ValidarUsuario(indigo.UserIndigo, INDtxtContraseña.Text) = True Then
                    If Modelo.ValidarUsuario(indigo.UserIndigoId, INDtxtContraseña.Text) = True Then
                        Me.Close()
                        _ActivarTimer = True
                    Else
                        'si la validar el usuario no es true entonces se ingreso mal la contraseña
                        INDtxtContraseña.Text = String.Empty
                        MessageIndigo.Show("El Nombre De Usuario o La Contraseña No Son Validos", MessageType.Warning, Me.Text, Botones.Aceptar)
                        INDtxtUsuario.Focus()
                        _ActivarTimer = False
                    End If
                Else
                    Using modelou = New MUsuario
                        Usuario = Await modelou.ConsultarUsuario(INDtxtUsuario.Text)
                    End Using
                    If Usuario Is Nothing Then
                        INDtxtUsuario.Text = String.Empty
                        INDtxtContraseña.Text = String.Empty
                        MessageIndigo.Show("No se encontró el usuario.", MessageType.Warning, Me.Text, Botones.Aceptar)
                        INDtxtUsuario.Focus()
                        Exit Sub
                    End If
                    'si la validacion de usuario y contraseña es correcta entonces consultamos el usuario
                    'If Modelo.ValidarUsuario(INDtxtUsuario.Text, INDtxtContraseña.Text) = True Then
                    If Modelo.ValidarUsuario(Usuario.Id, INDtxtContraseña.Text) = True Then
                        'Usuario = Modelo.ConsultarUsuario(INDtxtUsuario.Text)

                        If Not Usuario Is Nothing Then
                            'Elimino los espacion del objeto devuelto por el serivico
                            Usuario.Position = Usuario.Position.Trim
                            Usuario.Person.Fullname = Usuario.Person.Fullname.Trim
                        End If
                        'indigo.UserType = CType(UserType.Administrative, UserType)
                        'si el usuario es administrador entoces igualamos la propiedad cerrar sessio a true para cerrar la session desde el mdi
                        'If indigo.UserType = UserType.Administrative Then
                        If Usuario.UserType = "1" OrElse Usuario.UserType = "3" Then
                            Dim preguntar As New FrmConfirmacion
                            preguntar.ShowDialog()
                            If preguntar.valor = True Then
                                _CerrarSession = True
                                'cerramos el frontal de bloqueo
                                Me.Close()
                            End If
                        Else
                            INDtxtUsuario.Text = String.Empty
                            INDtxtContraseña.Text = String.Empty
                            MessageIndigo.Show("El Usuario Ingresador No Tiene Permisos De Administrador Del Sistema Para Cerrar la Sesion Actual", MessageType.Warning, Me.Text, Botones.Aceptar)
                            INDtxtUsuario.Focus()
                        End If
                    Else
                        'si el nombre de usuario y contraseña no son validos entonces me limpie los campos y me muestre un mensaje 
                        INDtxtUsuario.Text = String.Empty
                        INDtxtContraseña.Text = String.Empty
                        MessageIndigo.Show("El Nombre De Usuario o La Contraseña No Son Validos", MessageType.Warning, Me.Text, Botones.Aceptar)
                        INDtxtUsuario.Focus()
                        _ActivarTimer = False
                    End If
                End If
            Else
                'si no se han ingresado los datos en los textbox entonces me mande un mensaje
                INDtxtUsuario.Text = String.Empty
                INDtxtContraseña.Text = String.Empty
                INDtxtUsuario.Focus()
            End If
            'si el lauout usuario es visible entonces se va loguear al usuario que tiene la session activa
        End Using
    End Sub

    ''' <summary>
    ''' Evento Clic en la la flecha de "Atras" me hace visible los layouts para escoger si logear el usuario actual o finalizar la session
    ''' </summary>
    Private Sub INDpeFlecha_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles INDbtnSalir.Click
        INDpnTexto.Visible = True
        INDlySessionLock.Visible = False
    End Sub

#End Region

#Region "EVENTOS KEYDOWN"

    ''' <summary>
    ''' Funcion que me permite detectar una combinacion de teclas sin importar donde este el foco en este caso para lanzar el bloqueo de session desde la combinacion
    ''' windows + I
    ''' </summary>
    ''' <param name="msg">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the Win32 message to process.</param>
    ''' <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process.</param>
    ''' <returns>
    ''' true if the keystroke was processed and consumed by the control; otherwise, false to allow further processing.
    ''' </returns>
    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        If My.Computer.Keyboard.CtrlKeyDown Then
            If keyData = 131145 Then
                INDpnTexto.Visible = False
                INDlySessionLock.Visible = True
                INDtxtUsuario.Focus()
            End If
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    ''' <summary>
    ''' Evento Keydown del formulario donde se captura la combinacion de teclas para mostrar los controles de login
    ''' </summary>
    Private Sub FrmSesionBloqueada_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
        If e.Alt = True And e.Control = True And e.KeyCode = Keys.I Then
            INDpnTexto.Visible = False
            INDlySessionLock.Visible = True
            INDtxtUsuario.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Eventos keydown Para detectar que se estan presionando la combinacion de teclas alt + f4 e impedir que se cierre la aplicacion de
    ''' session bloqueada
    ''' </summary>
    Private Sub INDTxtContrasena2_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles INDtxtContraseña.KeyDown
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
            INDpnTexto.Visible = False
            INDlySessionLock.Visible = True
            INDtxtUsuario.Focus()
        End If
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub

    Private Sub PictureEdit4_KeyDown_1(sender As Object, e As KeyEventArgs) Handles PictureEdit4.KeyDown
        If e.Alt = True And e.Control = True And e.KeyCode = Keys.I Then
            INDpnTexto.Visible = False
            INDlySessionLock.Visible = True
            INDtxtUsuario.Focus()
        End If
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub

    Private Sub INDtxtUsuario_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles INDtxtUsuario.KeyDown
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub

    Private Sub btnContinuar_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles INDbtnEntrar.KeyDown
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub

    Private Sub PictureEdit4_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles INDbtnSalir.KeyDown
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub

#End Region

End Class