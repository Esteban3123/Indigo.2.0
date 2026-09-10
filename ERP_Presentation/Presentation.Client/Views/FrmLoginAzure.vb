'**********************************************************************
' Assembly         : Presentacion.Client
' Author           : Hector Rodriguez Rubiano
' Created          : 08-04-2021
'
' Description      : Formulario para login B2C
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Configuration
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Web.Security
Imports DevExpress.LookAndFeel
Imports DevExpress.Skins
Imports DevExpress.XtraEditors
Imports Domain.Security.Entities
Imports IndigoSingleton
Imports Infrastructure.Base.Security
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Web.WebView2.Core
Imports Microsoft.Web.WebView2.WinForms
Imports Newtonsoft.Json.Linq
Imports Presentation.Base
Imports Presentation.Security
Imports Presentation.Security.MVP

#End Region

Partial Public Class FrmLoginAzure
    'Implements IDisposable

#Region "Constructor y Variables Globales de Clase"
    Private _ValoresSesion As IndigoValoresSesion
    Private _Email As String
    Private _Usuario As MAutenticacionUsuario.Usuario

    Private _WebView As WebView2
    Private _ListCompanies As List(Of Company)
    Private _FrmMdi As FormMdi
    Private _CerrarSesion As Boolean
    Private _TokenId As String
    Private _UrlLogin As String
    Private _SessionLogoutUrl As String
    Private _RedirectUri As String
    Private _SignUpSignIn As String
    Private _PasswordReset As String
    Dim _IndigoApplicationSetting As Infrastructure.CrossCutting.Base.ApplicationSetting
    Private _UserConfiguration As UserConfiguration
    Private updaterBackground As New System.ComponentModel.BackgroundWorker
    Private IsUpdating As Boolean
    Private IsLogin As Boolean
    'Private _Consultando As Byte
#End Region

#Region "Propiedades"
    Public Property Usuario() As MAutenticacionUsuario.Usuario
        Get
            Return _Usuario
        End Get
        Set(ByVal value As MAutenticacionUsuario.Usuario)
            _Usuario = value
        End Set
    End Property
    Public Property Email() As String
        Get
            Return _Email
        End Get
        Set(ByVal value As String)
            _Email = value
        End Set
    End Property
#End Region

#Region "Tareas"
    Private taskLoadSettingsHIS As Task
#End Region

#Region "Constructor"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="frmMdi"></param>
    ''' <param name="cerrarSesion"></param>
    ''' <param name="tokenId"></param>
    Public Sub New(ByVal frmMdi As FormMdi, ByVal cerrarSesion As Boolean, ByVal tokenId As String)
        _FrmMdi = frmMdi
        _CerrarSesion = cerrarSesion
        _TokenId = tokenId
        InitializeComponent()
        Me.WindowState = FormWindowState.Normal
        Me.Bounds = Screen.PrimaryScreen.WorkingArea
        Me.WindowState = FormWindowState.Normal
        Me.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size
        Me.MinimumSize = Screen.PrimaryScreen.WorkingArea.Size
        Me.Location = New Point(0, 0)
        Me.Opacity = 100
        TokenManager.Instance.ClearToken()
        TokenManager.Instance.SetFlagValidateToken(False)

        'CefSharpSettings.LegacyJavascriptBindingEnabled = True
        Me.Focus()

        AddHandler updaterBackground.DoWork, AddressOf updaterBackground_DoWork
        AddHandler updaterBackground.RunWorkerCompleted, AddressOf updaterBackground_RunWorkerCompleted

    End Sub
#End Region

#Region "Eventos"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_load(sender As Object, e As EventArgs) Handles Me.Shown
        _IndigoApplicationSetting = ApplicationSetting.Instance
        Dim tenant As String = _IndigoApplicationSetting.Tenant
        Dim signUpSignIn As String = _IndigoApplicationSetting.SignUpSignIn
        Dim passwordReset As String = _IndigoApplicationSetting.PasswordReset
        Dim clientId As String = _IndigoApplicationSetting.ClientId.ToString
        Dim redirectUri As String = _IndigoApplicationSetting.RedirectUri
        Dim sessionLogoutUrl As String = _IndigoApplicationSetting.SessionLogoutUrl
        Dim urlWebSecurityServices As String = ConfigurationManager.AppSettings("UrlWebSecurityServices")
        Dim sinParametros As Boolean = False
        If String.IsNullOrEmpty(urlWebSecurityServices) Then
            MessageIndigo.Show("Especifique url web para el servicio de seguridad; revise el archivo de configuración de la aplicación.", MessageType.Errores, Me.Text, Botones.Ok)
            sinParametros = True
        ElseIf String.IsNullOrEmpty(tenant) OrElse String.IsNullOrEmpty(signUpSignIn) OrElse String.IsNullOrEmpty(clientId) OrElse String.IsNullOrEmpty(redirectUri) OrElse String.IsNullOrEmpty(sessionLogoutUrl) Then
            MessageIndigo.Show("Los parametros de login no estan completos; revise el archivo de configuración de la aplicación.", MessageType.Errores, Me.Text, Botones.Ok)
            sinParametros = True
        Else
            _UrlLogin = String.Format("https://{0}.b2clogin.com/{1}/oauth2/v2.0/authorize?p={2}&client_id={3}&nonce=defaultNonce&redirect_uri={4}&scope=openid&response_type=id_token&prompt=login",
            tenant.Split(".")(0), tenant, signUpSignIn, clientId, redirectUri)
            _RedirectUri = redirectUri
            _SessionLogoutUrl = String.Format("https://{0}.b2clogin.com/{1}/oauth2/v2.0/logout?p={2}&post_logout_redirect_uri={3}",
            tenant.Split(".")(0), tenant, signUpSignIn, sessionLogoutUrl)
            '_sessionLogoutUrl = String.Format("https://{0}/{1}/oauth2/v2.0/logout?post_logout_redirect_uri={2}",
            '"login.microsoftonline.com", tenant.Split(".")(0), sessionLogoutUrl)
            _SignUpSignIn = signUpSignIn
            _PasswordReset = passwordReset
        End If
        If sinParametros Then
            'se visualice la pagina de Indigo si falta parametrizacion
            If String.IsNullOrEmpty(sessionLogoutUrl) Then
                sessionLogoutUrl = "https://www.indigo.ms"
            End If
            _UrlLogin = sessionLogoutUrl
        End If

        'InitializeChromium()
        InitializeWebView()
        'TODO:HRR DESACTIVAR O ACTIVAR PARA HACER LOGIN CON UN USUARIO POR DEFECTO
        'Email = "bsalazar@indigo.tech"
        'CargarConfiguracionUsuario()
    End Sub


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDPce_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDPce.ButtonClick
        If e.Button.Caption = "Empresas" Then
        ElseIf e.Button.Caption = "Minimizar" Then
            Me.WindowState = FormWindowState.Minimized
        ElseIf e.Button.Caption = "Salir" Then
            End
        End If
    End Sub
#End Region

#Region "Procesos"
    ''' <summary>
    ''' Metodo Asincrono para cargar la configuracion del usuario y las compañias a las que tiene permiso en su tenant
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub CargarConfiguracionUsuario()
        Dim respuesta As MAutenticacionUsuario.Respuesta
        '_Consultando = 1
        Using modelo As New PAutenticacionUsuario(New MAutenticacionUsuario.ParametrosAzureFunctions() With {.AppFunctionURL = ApplicationSetting.Instance.AppFunctionURL})
            respuesta = Await modelo.GetUserByEmail(Email)
        End Using

        If respuesta.Fallo OrElse respuesta.Result Is Nothing OrElse respuesta.Result.Id = 0 Then
            '_Consultando = 0
            '_WebView.CoreWebView2.Navigate(String.Format("{0}&visibility_login=False", _UrlLogin))
            CerrarSesionWebView()
            _ListCompanies = New List(Of Company)
            DoSomething(Of String)(AddressOf MostrarNotificacion, String.Format("No se encontró la información del usuario con email {0}, por favor contacte al administrador del sistema", Email))
        Else
            Usuario = respuesta.Result
            If CType(Usuario.UserType, UserType) = UserType.GlobalAdmin AndAlso Not Usuario.Email.Contains("@indigo.tech") Then
                CerrarSesionWebView()
                _ListCompanies = New List(Of Company)
                DoSomething(Of String)(AddressOf MostrarNotificacion, "El usuario no es un administrador global válido")
            Else
                SessionValues.Instance.IndigoContainerId = 0
                Using modelo As New PAutenticacionUsuario(New MAutenticacionUsuario.ParametrosAzureFunctions() With {.AppFunctionURL = ApplicationSetting.Instance.AppFunctionURL})
                    respuesta = Await modelo.GetUserConfigurationByUserId(Usuario.Id)
                    If Not respuesta.Fallo Then
                        _UserConfiguration = respuesta.Result
                        SessionValues.Instance.IdTimeZone = _UserConfiguration.IdTimezone
                        SessionValues.Instance.dateFormat = _UserConfiguration.DateFormat
                        SessionValues.Instance.timeFormat = _UserConfiguration.TimeFormat
                        SessionValues.Instance.LanguageCulture = _UserConfiguration.LanguageCulture
                        UnifiedConfiguration.Instance.UserConfig = _UserConfiguration
                        TokenManager.Instance.SetToken(_UserConfiguration.AccessToken, _UserConfiguration.EncryptedKey)
                        IndigoValoresSesion.Instancia.JwtToken = TokenManager.GetInnerJwt(_UserConfiguration.AccessToken, _UserConfiguration.EncryptedKey)
                    End If
                End Using
                If _UserConfiguration IsNot Nothing Then
                    ApplicationSetting.Instance.ShowThemeSkinSelector = _UserConfiguration.ShowThemeSkinSelector
                    If _UserConfiguration.DefaultCompany > 0 Then
                        SessionValues.Instance.IndigoContainerId = _UserConfiguration.DefaultCompany
                    End If
                    ConfigurationFile.Instance.ReportPathType = _UserConfiguration.ReportPathType
                    ConfigurationFile.Instance.TypeAlertControl = _UserConfiguration.TypeAlertControl
                Else
                    ApplicationSetting.Instance.ShowThemeSkinSelector = False
                    ConfigurationFile.Instance.TypeAlertControl = 1
                    ConfigurationFile.Instance.ReportPathType = 1
                End If
                SessionValues.Instance.UserIndigo = Usuario.UserCode.Trim()
                '_FrmMdi.flagThemeChanged = False
                _FrmMdi.SetThemeSkinDefault()
                _ListCompanies = New List(Of Company)
                Using model As New Presentation.Client.MVP.MLogin
                    _ListCompanies = model.getCompaniesByUser(Usuario.Id, True)
                End Using
                ValidarEspacioTrabajo()
            End If
        End If
    End Sub
    ''' <summary>
    ''' Inicializa el control webview2
    ''' </summary>
    Private Async Sub InitializeWebView()
        Try
            If _WebView Is Nothing Then

                Dim SingleSignOn As Boolean = ConfigurationManager.AppSettings("ActiveSingleSignOn")
                Dim IndigoUpdate As Boolean = ConfigurationManager.AppSettings("IndigoUpdate")

                Dim AuthenticationType = System.Security.Principal.WindowsIdentity.GetCurrent().AuthenticationType.ToString()

                If SingleSignOn = True And AuthenticationType <> "CloudAP" Then
                    SingleSignOn = False
                End If

#If Not DEBUG Then
                If IndigoUpdate = True Then
                    If Directory.Exists(GetPathUpdate()) Then
                        If Await CompareVersion() Then
                            If Not updaterBackground.IsBusy Then
                                IsLogin = True
                                IsUpdating = True
                                updaterBackground.RunWorkerAsync()

                                'Si se esta validando la versión o se esta actualizando 
                                If updaterBackground.IsBusy OrElse IsUpdating Then
                                    Exit Sub
                                End If
                            End If
                        End If
                    End If
                End If
#End If


                Dim CacheEnabled As Boolean = ConfigurationManager.AppSettings("CacheEnabled")
                _WebView = New WebView2
                If CacheEnabled Then
                    'Guarda el correo y la ubicacion 
                    Dim cwv2e1 As CoreWebView2Environment
                    Dim c1 = New CoreWebView2EnvironmentOptions() With {.Language = "es-CO"}

                    cwv2e1 = Await CoreWebView2Environment.CreateAsync(Nothing, System.IO.Path.GetFullPath(Window.Utils.GetPathUserFiles() & "\cache"), c1)

                    Await _WebView.EnsureCoreWebView2Async(cwv2e1)
                    _WebView.CoreWebView2.Settings.IsPasswordAutosaveEnabled = True

                    AddHandler _WebView.NavigationStarting, AddressOf WebView2Control_NavigationStarting
                    Me.Controls.Add(_WebView)
                    'webView1.Size = New Size(600, 800)
                    'webView1.Location = New Point(200, 200)
                    _WebView.Dock = DockStyle.Fill

                Else
                    'forma que no guarda nada de informacion en la memoria cache
                    Dim cwv2e As CoreWebView2Environment
                    Dim c1 = New CoreWebView2EnvironmentOptions() With {.AllowSingleSignOnUsingOSPrimaryAccount = SingleSignOn}
                    Dim userdatafolder = Path.Combine(Window.Utils.GetPathUserFiles(), "cache", New Random().Next().ToString())

                    cwv2e = Await CoreWebView2Environment.CreateAsync(Nothing, System.IO.Path.GetFullPath(userdatafolder), c1)
                    'Dim cwv2e = Await CoreWebView2Environment.CreateAsync(String.Concat(Utils.GetApplicationPath(), "WebView2\"), System.IO.Path.GetFullPath(Window.Utils.GetPathUserFiles() & "\cache"))

                    Await _WebView.EnsureCoreWebView2Async(cwv2e)

                    AddHandler _WebView.NavigationStarting, AddressOf WebView2Control_NavigationStarting
                    Me.Controls.Add(_WebView)
                    'webView.Size = New Size(600, 800)
                    'webView.Location = New Point(200, 200)
                    _WebView.Dock = DockStyle.Fill

                    Await ClearAutofillDataAsync()
                End If


                'webView1.CoreWebView2.Navigate("https://fx-indira-core.azurewebsites.net/notificaciones/resources/24-11.jpg")
            End If

            If _CerrarSesion Then
                _CerrarSesion = False
                _WebView.CoreWebView2.Navigate(_SessionLogoutUrl)
            Else
                _WebView.CoreWebView2.Navigate(String.Format("{0}&visibility_login=False", _UrlLogin))
            End If
        Catch ex As Exception
            Dim [error] = ex.Message.ToString()
            MessageIndigo.Show("Ocurrio un error al intentar abrir la pagina de login: " & [error], MessageType.Errores, Me.Text)
        End Try
    End Sub

    Private Async Function ClearAutofillDataAsync() As Task
        Dim profile As CoreWebView2Profile

        If _WebView.CoreWebView2 IsNot Nothing Then
            profile = _WebView.CoreWebView2.Profile
            Dim endTime As System.DateTime = DateTime.Now
            Dim startTime As System.DateTime = DateTime.Now.AddMonths(-12)
            Dim dataKinds As CoreWebView2BrowsingDataKinds = CType((CoreWebView2BrowsingDataKinds.GeneralAutofill Or CoreWebView2BrowsingDataKinds.PasswordAutosave), CoreWebView2BrowsingDataKinds)
            Await profile.ClearBrowsingDataAsync(dataKinds, startTime, endTime)
        End If
    End Function

    ''' <summary>
    ''' Permite lanzar eventos y recibir un parametro, validando si se ejecuta o no en el mismo hilo
    ''' </summary>
    ''' <typeparam name="T1"></typeparam>
    ''' <param name="a"></param>
    ''' <param name="p"></param>
    Private Sub DoSomething(Of T1)(a As Action(Of T1), p As T1)
        If InvokeRequired Then
            Me.BeginInvoke(Sub()
                               a.Invoke(p)
                           End Sub)

        Else
            a.Invoke(p)
        End If
    End Sub

    ''' <summary>
    ''' Cambia el cursor
    ''' </summary>
    ''' <param name="_cursor"></param>
    Private Sub CursorChange(ByVal _cursor As Cursor)
        Me.Cursor = _cursor
    End Sub

    ''' <summary>
    ''' Mostrar mensaje
    ''' </summary>
    ''' <param name="_mensaje"></param>
    Private Sub MostrarNotificacion(ByVal _mensaje As String)
        MessageIndigo.Show(_mensaje, MessageType.Warning, Me.Text)
    End Sub

    ''' <summary>
    ''' Metodo Asincrono para cargar la configuracion del usuario y las compañias a las que tiene permiso en su tenant
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub ValidarEspacioTrabajo()
        DoSomething(Of Cursor)(AddressOf CursorChange, Cursors.WaitCursor)

        If SessionValues.Instance.IndigoContainerId > 0 Then
            If _UserConfiguration.DefaultConfiguration Then
                Dim _Continuar As ValidacionContinuar = ValidacionContinuar.DeniegaAcceso '0:no continua, 1:continua login, 2:muestra compañias

                Using WorkspaceUserForm As New WorkspaceUser("FrmLoginAzure")

                    WorkspaceUserForm.Usuario = Usuario
                    WorkspaceUserForm.UserConfig = _UserConfiguration
                    WorkspaceUserForm.CompanyList = _ListCompanies
                    ''WorkspaceUserForm.ListServiceConfiguration = _ListServiceConfiguration

                    'Verifica el perfil del usuario y los estados del profesional 
                    Dim _VerificaPerfil As WorkspaceUser.EValidacion = Await WorkspaceUserForm.VerificarPerfil()
                    Select Case _VerificaPerfil
                        Case WorkspaceUser.EValidacion.SinCompaniaDefault 'no encontro compañia
                            If _ListCompanies.Count() > 0 Then
                                _Continuar = ValidacionContinuar.IngresaWorkspace
                            Else
                                DoSomething(Of String)(AddressOf MostrarNotificacion, "No se encontró la información de la compañia por defecto")
                            End If
                        Case WorkspaceUser.EValidacion.UsuarioNoAsistencial 'Usuario no asistencial continua
                            _Continuar = ValidacionContinuar.ConcedeAcceso
                        Case WorkspaceUser.EValidacion.NoEncontroProfesional 'Asistencial sin profesional
                            If _ListCompanies.Count() > 1 Then
                                _Continuar = ValidacionContinuar.IngresaWorkspace
                            Else
                                DoSomething(Of String)(AddressOf MostrarNotificacion, "No se encontró la información del profesional")
                            End If
                        Case WorkspaceUser.EValidacion.ProfesionalInactivo 'profesional inactivo
                            If _ListCompanies.Count() > 1 Then
                                _Continuar = ValidacionContinuar.IngresaWorkspace
                            Else
                                DoSomething(Of String)(AddressOf MostrarNotificacion, "El profesional asociado a este usuario esta inactivo")
                            End If
                        Case WorkspaceUser.EValidacion.ProfesionalActivo 'profesional activo, sin cambios de grupo o rol continua
                            _Continuar = ValidacionContinuar.ConcedeAcceso
                        Case WorkspaceUser.EValidacion.CambioGrupoRol 'cambio datos de grupo o rol
                            _Continuar = ValidacionContinuar.IngresaWorkspace
                        Case WorkspaceUser.EValidacion.SinServiceConfiguration 'No se encontró la configuración de servicios
                            If _ListCompanies.Count() >= 1 Then
                                _Continuar = ValidacionContinuar.IngresaWorkspace
                            Else
                                DoSomething(Of String)(AddressOf MostrarNotificacion, "No se encontró la configuración de servicios de la compañía seleccionada. Por favor comuníquese con el administrador del sistema")
                            End If
                        Case WorkspaceUser.EValidacion.ErrorInterno
                            DoSomething(Of String)(AddressOf MostrarNotificacion, "Error interno al verificar perfil del usuario")
                    End Select
                    'MostrarCompanias:
                    If _Continuar = ValidacionContinuar.IngresaWorkspace Then
                        '_Consultando = 0
                        '_WebView.CoreWebView2.Navigate(_UrlLogin)
                        If WorkspaceUserForm.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                            _FrmMdi._tokenId = _TokenId
                            _FrmMdi.AssingCompanies(_ListCompanies, WorkspaceUserForm.PUserLogin.ListCompanyPermissionCode)
                            Me.DialogResult = DialogResult.OK
                        Else
                            CerrarSesionWebView()
                        End If
                    ElseIf _Continuar = ValidacionContinuar.DeniegaAcceso Then
                        '_Consultando = 0
                        CerrarSesionWebView()
                    ElseIf _Continuar = ValidacionContinuar.ConcedeAcceso Then
                        Using _PAutorizacionUsuario As New PAutorizacionUsuario()
                            _PAutorizacionUsuario.UserConfig = _UserConfiguration
                            _PAutorizacionUsuario.Usuario = Usuario
                            Dim _Respuesta As MAutenticacionUsuario.Respuesta = _PAutorizacionUsuario.AutorizarUsuario
                            If _Respuesta.Fallo Then
                                DoSomething(Of String)(AddressOf MostrarNotificacion, _Respuesta.Mensaje)
                                '_Consultando = 0
                                CerrarSesionWebView()
                            Else
                                If _UserConfiguration.OperatingUnitId.GetValueOrDefault() <> 0 Then
                                    SessionValues.Instance.IndigoOperatingUnitId = _UserConfiguration.OperatingUnitId.GetValueOrDefault()
                                    WorkspaceUserForm.CompanySelected.IdOperatingUnitDefault = SessionValues.Instance.IndigoOperatingUnitId
                                End If

                                UnifiedConfiguration.Instance.UsuarioEHR = WorkspaceUserForm.UsuarioEHR
                                UnifiedConfiguration.Instance.Profesional = WorkspaceUserForm.Profesional
                                UnifiedConfiguration.Instance.PServiceConfiguration = WorkspaceUserForm.PServiceConfiguration
                                UnifiedConfiguration.Instance.CompanySelected = WorkspaceUserForm.CompanySelected
                                _FrmMdi._tokenId = _TokenId
                                _FrmMdi.AssingCompanies(_ListCompanies, _PAutorizacionUsuario.PUserLogin.ListCompanyPermissionCode)
                                Me.DialogResult = DialogResult.OK
                                '_Consultando = 0
                            End If
                        End Using
                    End If
                    DoSomething(Of Cursor)(AddressOf CursorChange, Cursors.Default)
                End Using
                Return
            Else
                Using WorkspaceUserForm As New WorkspaceUser("FrmLoginAzure")
                    WorkspaceUserForm.Usuario = Usuario
                    WorkspaceUserForm.UserConfig = _UserConfiguration
                    WorkspaceUserForm.CompanyList = _ListCompanies
                    'WorkspaceUserForm.ListServiceConfiguration = _ListServiceConfiguration

                    DoSomething(Of Cursor)(AddressOf CursorChange, Cursors.Default)
                    '_Consultando = 0
                    '_WebView.CoreWebView2.Navigate(_UrlLogin)
                    If WorkspaceUserForm.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                        _FrmMdi._tokenId = _TokenId
                        _FrmMdi.AssingCompanies(_ListCompanies, WorkspaceUserForm.PUserLogin.ListCompanyPermissionCode)
                        Me.DialogResult = DialogResult.OK
                    Else
                        CerrarSesionWebView()
                    End If
                End Using
            End If
        Else
            Using WorkspaceUserForm As New WorkspaceUser("FrmLoginAzure")

                WorkspaceUserForm.Usuario = Usuario
                WorkspaceUserForm.UserConfig = _UserConfiguration
                WorkspaceUserForm.CompanyList = _ListCompanies

                DoSomething(Of Cursor)(AddressOf CursorChange, Cursors.Default)
                '_Consultando = 0
                '_WebView.CoreWebView2.Navigate(_UrlLogin)
                If WorkspaceUserForm.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                    _FrmMdi._tokenId = _TokenId
                    _FrmMdi.AssingCompanies(_ListCompanies, WorkspaceUserForm.PUserLogin.ListCompanyPermissionCode)
                    Me.DialogResult = DialogResult.OK
                Else
                    CerrarSesionWebView()
                End If

            End Using
        End If

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    Private Sub CerrarSesionWebView()
        'cierra seccion 
        _WebView.CoreWebView2.Navigate(_SessionLogoutUrl)
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="cerroSesion"></param>
    Private Sub FinalizoLoginWebView(ByVal cerroSesion As Boolean)
        If cerroSesion Then
            _WebView.CoreWebView2.Navigate(String.Format("{0}&visibility_login=False", _UrlLogin))
        Else
            If String.IsNullOrEmpty(Email) Then
                _WebView.CoreWebView2.Navigate(String.Format("{0}&visibility_login=False", _UrlLogin))
                DoSomething(Of String)(AddressOf MostrarNotificacion, "No se pudo recuperar la información de correo.")
            Else
                _WebView.CoreWebView2.Navigate(String.Format("{0}&visibility_login=True", _UrlLogin))
                CargarConfiguracionUsuario()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento para inicio de navegacion en el control WebView2
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub WebView2Control_NavigationStarting(ByVal sender As Object, ByVal e As CoreWebView2NavigationStartingEventArgs)
        'Dim _url As String = "https://indigoauth.b2clogin.com/oauth2/nativeclient"
        'If _Consultando > 1 Then
        '    e.Cancel = True
        '    Return
        'ElseIf _Consultando = 1 Then
        '    _Consultando += 1
        'End If

        If e.Uri.Contains("cancelled") Then
            e.Cancel = True
            _WebView.CoreWebView2.Navigate(String.Format("{0}&visibility_login=False", _UrlLogin))
            Exit Sub
        End If

        If e.Uri.Contains("forgotPassword") Then
            e.Cancel = True
            _WebView.CoreWebView2.Navigate(_UrlLogin.Replace(_SignUpSignIn, _PasswordReset))
            Exit Sub
        End If

        If e.Uri.StartsWith(_RedirectUri) AndAlso Not e.Uri.Contains("error=") Then
            Dim html As String = e.Uri
            Dim pos As Integer = html.IndexOf("id_token")

            If pos > 0 Then
                Dim posfin As Integer = html.Length

                If posfin >= 0 Then
                    Dim subhtml As String = html.Substring(pos + 9, posfin - pos - 9)
                    RecuperaToken("id_token:" + subhtml, True)
                End If
            End If
            FinalizoLoginWebView(False)
        ElseIf e.Uri.StartsWith(_SessionLogoutUrl) Then
            _WebView.CoreWebView2.CookieManager.DeleteAllCookies()
            FinalizoLoginWebView(True)
        ElseIf e.Uri.Contains("error=") Then
            _WebView.CoreWebView2.Navigate("https://www.indigo.ms")
            MessageIndigo.Show("Ocurrio un error al intentar abrir la pagina de login.", MessageType.Errores, Me.Text)
        End If
    End Sub

    ''' <summary>
    ''' Recupera la informacion de token
    ''' </summary>
    ''' <param name="IdToken"></param>
    ''' <param name="guardarToken"></param>
    Private Sub RecuperaToken(ByVal IdToken As String, ByVal guardarToken As String)
        Dim email2 As String = String.Empty
        Dim user As JObject = ParseIdToken(IdToken)

        _TokenId = IdToken
        email2 = user("email")?.ToString()
        'MessageIndigo.Show("Email recibido " + email2, MessageType.Information, Me.Text)
        If String.IsNullOrEmpty(email2) Then
            email2 = user("email2")?.ToString()
        End If
        If String.IsNullOrEmpty(email2) AndAlso user("emails") IsNot Nothing AndAlso Not String.IsNullOrEmpty(user("emails")?.ToString()) Then
            Dim emails = CType(user("emails"), JArray)
            If emails IsNot Nothing AndAlso emails.Count > 0 Then
                Email = emails(0).ToString()
            End If
        Else
            Email = email2
        End If
    End Sub

    Private Function GetPathUpdate() As String
        Dim pathUpdate As String = ConfigurationManager.AppSettings("IndigoUpdatePath")

        Return pathUpdate
    End Function

    ''' <summary>
    ''' Validar la versión con respecto a la versión de la ruta de actualización
    ''' </summary>
    ''' <returns></returns>
    Private Async Function CompareVersion() As Task(Of Boolean)
        Dim pathUpdate As String = Me.GetPathUpdate()
        Dim ThisApplication As String = My.Application.Info.AssemblyName & ".exe"

        Dim clientToUpdate = pathUpdate & "\" & My.Application.Info.AssemblyName & ".exe"
        Dim clientToUpdateVersion = Version.Parse(FileVersionInfo.GetVersionInfo(clientToUpdate).FileVersion)
        Dim thisClientVersion = Version.Parse(FileVersionInfo.GetVersionInfo(ThisApplication).FileVersion)

        Dim ClientToUpdateVersionNumber = clientToUpdateVersion.Major + clientToUpdateVersion.Minor + clientToUpdateVersion.Build + clientToUpdateVersion.Revision
        Dim ThisClientVersionNumber = thisClientVersion.Major + thisClientVersion.Minor + thisClientVersion.Build + thisClientVersion.Revision

        If ClientToUpdateVersionNumber > ThisClientVersionNumber Then
            Return True
        End If

        'No necesita actualización
        Return False
    End Function

    ''' <summary>
    ''' Ejecuta el actualizador de versionamiento
    ''' </summary>
    Private Sub RunUpdater()
        Dim pathExecutable = My.Application.Info.DirectoryPath & "\Indigo Update.exe"
        Dim pathUpdate As String = Me.GetPathUpdate()

        If File.Exists(pathExecutable) = False Then
            Throw New FileNotFoundException(String.Concat("El Actualizador Indigo Update.exe no esta presente en la carpeta ", My.Application.Info.DirectoryPath, ". La aplicacion no podra actualizarse"))
            Exit Sub
        End If

        Dim ejecutar As String
        ejecutar = String.Concat("""", pathExecutable, """ """, pathUpdate, """ ""Vie Cloud Platform.exe""")

        Dim myProcess As New Process()
        ' Establece valores en las propiedades FileInfo
        myProcess.StartInfo.UseShellExecute = False
        myProcess.StartInfo.FileName = ejecutar
        myProcess.StartInfo.CreateNoWindow = True
        'Lanzando proceso
        myProcess.Start()
    End Sub

    Private Sub updaterBackground_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs)
        'devuelvo el resulatdo de la funcion
        e.Result = CompareVersion()
    End Sub

    Private Sub updaterBackground_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs)
        DoSomething(Of Cursor)(AddressOf CursorChange, Cursors.WaitCursor)
        Me.Cursor = Cursors.Default

        If e.Error IsNot Nothing Then
            IsUpdating = False
            MessageBox.Show(Utils.GetInnerExceptionMessageToString(e.Error))
            Exit Sub
        End If
        ''si el resultado es igual a true se debe actualizar
        If CBool(e.Result.Result) = True Then
            RunUpdater()
            End
        End If

        IsUpdating = False

        If IsLogin Then
            IsLogin = False
        End If
    End Sub

#End Region

#Region "Procesos Asyncronos"
#End Region

#Region "Metodos y Funciones"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="idToken"></param>
    ''' <returns></returns>
    Private Function ParseIdToken(ByVal idToken As String) As JObject
        idToken = idToken.Split("."c)(1)
        idToken = Base64UrlDecode(idToken)
        Return JObject.Parse(idToken)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="s"></param>
    ''' <returns></returns>
    Private Function Base64UrlDecode(ByVal s As String) As String
        s = s.Replace("-"c, "+"c).Replace("_"c, "/"c)
        s = s.PadRight(s.Length + (4 - s.Length Mod 4) Mod 4, "="c)
        Dim byteArray = Convert.FromBase64String(s)
        Dim decoded = Encoding.UTF8.GetString(byteArray, 0, byteArray.Count())
        Return decoded
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Sub Dispose_Form(disposing As Boolean, disp As Boolean)
        If Not Me.disposedValue Then
            taskLoadSettingsHIS = Nothing
            _ValoresSesion = Nothing
            _Email = Nothing
            _Usuario = Nothing
            _TokenId = Nothing
            'chromeBrowser = Nothing
            'TODO:HRR REVISAR ERROR AL CERRAR
            'No se puede convertir el objeto COM del tipo 'System.__ComObject' al tipo de interfaz 'Microsoft.Web.WebView2.Core.Raw.ICoreWebView2Controller'. Ocurrió un error de operación debido a que la llamada QueryInterface en el componente COM para la interfaz con IID '{4D00C0D1-9434-4EB6-8078-8697A560334F}' generó el siguiente error: Interfaz no compatible (Excepción de HRESULT: 0x80004002 (E_NOINTERFACE)).
            '_WebView.CoreWebView2.CookieManager.DeleteAllCookies()
            '_WebView = Nothing
            _ListCompanies = Nothing
            _FrmMdi = Nothing
            _CerrarSesion = False
            _UrlLogin = Nothing
            _SessionLogoutUrl = Nothing
            _RedirectUri = Nothing
        End If
        Me.disposedValue = True
    End Sub

    '' This code added by Visual Basic to correctly implement the disposable pattern.
    'Public Overloads Sub Dispose() Implements IDisposable.Dispose
    '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    '    Dispose(True)
    '    GC.SuppressFinalize(Me)
    'End Sub
#End Region

#Region "Enumeradores"
    Enum ValidacionContinuar
        DeniegaAcceso
        ConcedeAcceso
        IngresaWorkspace
    End Enum
#End Region
End Class