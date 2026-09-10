#Region "Imports"

Imports System.ComponentModel
Imports System.IO
Imports System.Net
Imports DevExpress.XtraEditors.Controls
Imports Domain.Base.Entities
Imports Domain.Security.Entities
Imports IndigoEncript
Imports IndigoLibrary
Imports IndigoSingleton
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Base.Window
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Client.MVP
Imports Presentation.Security

#End Region

Public Class FrmLogin
    Implements IDisposable

#Region "Constructor y Variables Globales de Clase"


    'Definioms variables de tipo Task para retornar valores que deben ser accesados desde otros metodos
    'Y que se ejecutan de forma paralela
    Private taskGetCompanies As Task(Of List(Of Company))
    Private taskLoadSettingsHIS As Task
    Private listCompanies As List(Of Company)
    Private ValoresSesion As IndigoValoresSesion
    Private INDmeImagenWather As System.Windows.Controls.MediaElement = New System.Windows.Controls.MediaElement()

    ''' <summary>
    ''' Constructor del login 
    ''' </summary>
    ''' <remarks></remarks>
    Sub New()
        taskGetCompanies = GetCompaniesAsync()
        taskLoadSettingsHIS = LoadSettingsHISAsync()
        InitializeComponent()
        PnlProgreso.SendToBack()
        Me.TmrDateTime_Tick(Me, New EventArgs())
        GetResourceDaySpecial()
        Maximize()
        'ControlAnimator.StartAnimating(PbxBackgroundLogin, ControlAnimator.DrawMode.Lines, Color.Aqua, True)
    End Sub
#End Region

#Region "Eventos"

    ''' <summary>
    ''' Cerrar del todo la aplicacion 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnCerrar_Click(sender As Object, e As EventArgs) Handles INDbtnCerrar.Click, BtnClose.Click
        End
    End Sub

    ''' <summary>
    ''' Minimizar la aplicacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnMinimizar_Click(sender As Object, e As EventArgs) Handles INDbtnMinimizar.Click
        MyBase.WindowState = FormWindowState.Minimized
    End Sub

    ''' <summary>
    ''' Click en el boton Home de Indigo - Dirige a www.indigo.ms 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub GleOptions_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles GleOptions.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph AndAlso e.Button.Tag.ToString().Equals("Home") Then
            Using proceso As New System.Diagnostics.Process
                proceso.StartInfo.FileName = "http://www.indigo.ms/"
                proceso.Start()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Cambiar de Empresa
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub GleOptions_EditValueChanged(sender As Object, e As EventArgs) Handles GleOptions.EditValueChanged
        SetERPSingletonValues(CType(GleOptions.GetSelectedDataRow, Company))
    End Sub

    ''' <summary>
    ''' Aqui validamos la versión del esquema contra la versión de la aplicación
    ''' </summary>
    Private Sub GleOptions_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles GleOptions.EditValueChanging
        '#If Not DEBUG Then
        '        Dim comp As Company = CType(GleOptions.Properties.View.GetFocusedRow(), Company)
        '        Try
        '            If Not comp.Version.Trim().Equals(String.Empty) Then
        '                Dim vDbSchema As Version = New Version(comp.Version)
        '                If Not vDbSchema.Equals(BaseClass.GetAppVersion()) Then
        '                    e.Cancel = True
        '                    Dim frm As New Presentation.Controls.FrmNotificationItemDetail
        '                    frm.TxtMessage.Text = "La versión del esquema en la empresa " & comp.Code & " - " & comp.Name & ", NO corresponde a la versión de éste cliente (" & BaseClass.GetAppVersion().ToString() & ")." & vbCrLf & "Porfavor actualice la versión del cliente e intente de nuevo."
        '                    Using transparent As New FrmTransparent(frm, False)
        '                        transparent.ShowDialog(Me)
        '                    End Using
        '                End If
        '            Else
        '                e.Cancel = True
        '                Dim frm As New Presentation.Controls.FrmNotificationItemDetail
        '                frm.TxtMessage.Text = "La versión del esquema en la empresa " & comp.Code & " - " & comp.Name & ", NO corresponde a la versión de éste cliente (" & BaseClass.GetAppVersion().ToString() & ")." & vbCrLf & "Porfavor actualice la versión del cliente e intente de nuevo."
        '                Using transparent As New FrmTransparent(frm, False)
        '                    transparent.ShowDialog(Me)
        '                End Using
        '            End If
        '        Catch ex As Exception
        '            e.Cancel = True
        '            Dim frm As New Presentation.Controls.FrmNotificationItemDetail
        '            frm.TxtMessage.Text = "La versión del esquema en la empresa " & comp.Code & " - " & comp.Name & ", NO corresponde a la versión de éste cliente (" & BaseClass.GetAppVersion().ToString() & ")." & vbCrLf & "Porfavor actualice la versión del cliente e intente de nuevo."
        '            Using transparent As New FrmTransparent(frm, False)
        '                transparent.ShowDialog(Me)
        '            End Using
        '        End Try
        '#End If
    End Sub

    ''' <summary>
    ''' Lista las empresas disponibles - Evento Asicrono
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub GleOptions_QueryPopUpAsync(sender As Object, e As CancelEventArgs) Handles GleOptions.QueryPopUp
        Me.GdvCompanies.ShowLoadingPanel()
        If listCompanies Is Nothing OrElse listCompanies.Count = 0 Then
            listCompanies = Await taskGetCompanies
        End If
        GleOptions.Properties.DataSource = listCompanies
        Me.GdvCompanies.HideLoadingPanel()
    End Sub

    ''' <summary>
    ''' Metodo para establecer los valores de la Singleton del ERP en el proceso de login cuando se carga la empresa por default o cuando se dispara el evento change del Popup Empresas
    ''' </summary>
    ''' <param name="companySelected"></param>
    ''' <remarks></remarks>
    Private Sub SetERPSingletonValues(ByVal companySelected As Company)
        'TODO: Verificar todas las variagles de la singleton ERP y HIS
        SessionValues.Instance.IndigoContainerId = companySelected.Id
        SessionValues.Instance.HisContainer = companySelected.HISContainer
        SessionValues.Instance.IndigoCompany = companySelected.Code
        SessionValues.Instance.DocumentalContainer = companySelected.DocumentalContainer
        SessionValues.Instance.TransactionalContainer = companySelected.TransactionalContainer
        SessionValues.Instance.FoundationalContainer = companySelected.FoundationalContainer
        SessionValues.Instance.InteropCostContainer = companySelected.InteropCostContainer
        SessionValues.Instance.IndigoCompanyType = companySelected.CompanyType
        SessionValues.Instance.IndigoGlossesIntegration = companySelected.GlossesIntegration
        SessionValues.Instance.IntergrationHisStatus = If(companySelected.HISIntegration = 0, IntegrationStatus.NonIntegrated, IntegrationStatus.InProgress)
        SessionValues.Instance.IndigoPayrollIntegration = companySelected.PayrollIntegration
        SessionValues.Instance.IndigoHumanTalentIntegration = companySelected.HumanTalentIntegration
        SessionValues.Instance.IndigoDispensingIntegration = companySelected.DispensingIntegration
        SessionValues.Instance.VituelContainer = companySelected.VituelContainer
        SessionValues.Instance.IndigoCompanyAddress = companySelected.Address
        SessionValues.Instance.IndigoCompanyName = companySelected.Name
        SessionValues.Instance.IndigoCompanyNit = companySelected.CompanyNit
        SessionValues.Instance.IndigoVerificationDigitNit = companySelected.VerificationDigitNit
        SessionValues.Instance.IndigoCompanyPhoneNumber = companySelected.Telephone
        SessionValues.Instance.DecimalSeparator = companySelected.DecimalSeparator
        ValoresSesion.EmpresaDGH = companySelected.TransactionalContainer 'Obsoleto, si lo utiliza el his
        ValoresSesion.EmpresaIndigoNit = companySelected.CompanyNit + "-" + companySelected.VerificationDigitNit
        ValoresSesion.EmpresaIndigoFundamentales = companySelected.FoundationalContainer.Replace("INDIGO", "")
        ValoresSesion.EmpresaIndigo = companySelected.HISContainer.Trim.Replace("INDIGO", "")
        ValoresSesion.EmpresaIndigoNombre = companySelected.Name.Trim
        If SessionValues.Instance.SecurityContainer Is Nothing OrElse SessionValues.Instance.SecurityContainer.Trim().Equals(String.Empty) Then
            SessionValues.Instance.SecurityContainer = companySelected.SecurityContainer
        End If
    End Sub

    ''' <summary>
    ''' Click en Login
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub BtnLogin_Click(sender As Object, e As EventArgs) Handles BtnLogin.Click
        Me.Cursor = Presentation.Base.BaseClass.ChangeCursorIndigo
        BtnLogin.Enabled = False
        BtnClose.Enabled = False
        'PnlProgreso.BringToFront()
        Try
            'Validamos campos para el proceso de autenticacion
            If Me.TxtLoginBox.Text.Trim().Equals(String.Empty) OrElse Me.TxtPasswordBox.Text.Trim().Equals(String.Empty) Then
                MessageIndigo.Show(ResourceManager.GetString("FieldEmpty"), MessageType.Warning, Me.Text.Trim(), Botones.Ok)
                Me.TxtLoginBox.Focus()
                Me.TxtLoginBox.SelectAll()

            Else

                'Bloqueamos el boton login, debido a que en forma asincrona se envio a construir el MDI, y se realizan otros metodos asincronos
                'en caso de ser demorado el proceso y debido a que el await no bloquea la interfaz de usuario
                'se bloquea el boton para que el usuario no de click de nuevo
                'Listamos las empresas
                If listCompanies Is Nothing OrElse listCompanies.Count = 0 Then
                    listCompanies = Await taskGetCompanies
                    GleOptions.Properties.DataSource = listCompanies
                End If
                'Si no han seleccionado una empresa le trato de asignar la que viene por defecto
                If SessionValues.Instance.IndigoCompany Is Nothing Then
                    Dim varSession As SessionValues = SessionValues.Instance
                    Dim DefaultCompany As Company = listCompanies.Find(Function(x) x.Code.Equals(varSession.IndigoContainer.ToString))
                    If DefaultCompany IsNot Nothing Then
                        SetERPSingletonValues(DefaultCompany)
                    End If
                End If
                Dim result As ActionResult(Of UserLogin) = Nothing
                Using model As New MLogin
                    If SessionValues.Instance.IndigoCompany Is Nothing OrElse SessionValues.Instance.IndigoCompany.Trim().Equals(String.Empty) Then
                        MessageBox.Show("El contenedor " & ConfigurationFile.Instance.DefaultCompanyContainerCode & " no existe")
                        BtnLogin.Enabled = True
                        BtnClose.Enabled = True
                        PnlProgreso.SendToBack()
                        BtnLogin.Focus()
                        Me.Cursor = Cursors.Default
                        Return
                    Else
                        result = Await model.LoginUserCompany(Me.TxtLoginBox.Text.Trim(), Me.TxtPasswordBox.Text.Trim(), SessionValues.Instance.IndigoCompany, BaseClass.GetAppVersion())
                    End If
                    If result.StateResult Then
                        Dim usr As UserLogin = result.ObjectEmbbeded
                        Me.InitSesionValues(usr)

                        TmrDateTime.Enabled = False

                        Dim frmMdi = New FormMdi()
                        frmMdi.DialogResult = System.Windows.Forms.DialogResult.None
                        Await taskLoadSettingsHIS
                        'frmMdi.AutenticateHIS(Me.TxtLoginBox.Text.Trim())'se llama en cargar unidades operacionales
                        frmMdi.AssingCompanies(listCompanies, usr.ListCompanyPermissionCode)
                        Me.Hide()
                        Dim res = frmMdi.ShowDialog()
                        If res = System.Windows.Forms.DialogResult.Cancel Then
                            Me.Close()
                        End If
                        Me.TxtLoginBox.Text = String.Empty
                        Me.TxtPasswordBox.Text = String.Empty
                        Me.BeginInvoke(Sub() Me.TxtLoginBox.Focus())
                        Me.Show()
                    Else
                        Dim frm As New Presentation.Controls.FrmNotificationItemDetail
                        frm.TxtMessage.Text = result.Message
                        Using transparent As New FrmTransparent(frm, False)
                            transparent.ShowDialog(Me)
                        End Using
                    End If

                End Using
            End If

        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
        Finally
            BtnLogin.Enabled = True
            BtnClose.Enabled = True
            PnlProgreso.SendToBack()
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    ''' <summary>
    ''' Se eliminan las tareas paralelas para que el cierre sea correcto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmLogin_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed

    End Sub

    ''' <summary>
    ''' Se establece el primer foco al control de codigo usuario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmLogin_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        TxtLoginBox.Focus()
    End Sub

    ''' <summary>
    ''' Timer para el control de la Hora del frm Login
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub TmrDateTime_Tick(sender As Object, e As EventArgs) Handles TmrDateTime.Tick
        Dim DateNow As DateTime = DateTime.Now
        Me.LblTime.Text = DateNow.ToString("HH:mm")
        Me.LblDate.Text = DateNow.ToString("dddd, MMMM dd")
    End Sub

#End Region

#Region "Metodos y Funciones"

    Private Sub Maximize()
        Me.Location = Screen.PrimaryScreen.WorkingArea.Location
        Me.Size = Screen.PrimaryScreen.WorkingArea.Size
    End Sub

    ''' <summary>
    ''' Metodo para cargar el archivo de configuracion en las variables de sessiones
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetCompaniesAsync() As Task(Of List(Of Company))
        Return Task.Factory.StartNew(Of List(Of Company))(AddressOf GetCompanies)
    End Function

    ''' <summary>
    ''' Metodo para cargar el archivo de configuracion en las variables de sessiones
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetCompanies() As List(Of Company)
        If Not LoaderConfigurationFile.ConfigurationFileExists() Then
            If MessageIndigo.Show("No existe un archivo de configuracion, desea crearlo ahora", MessageType.Question, Me.Text, Botones.SiNo) = DialogResult.Yes Then
                Using tras As New FrmTransparent(New FrmConfigurarConexion(), False)
                    If tras.ShowDialog() <> System.Windows.Forms.DialogResult.OK Then
                        MessageIndigo.Show("Sin este archivo no se puede continuar", MessageType.Errores, Me.Text, Botones.Ok)
                        If LoaderConfigurationFile.ConfigurationFileExists() Then
                            File.Delete(LoaderConfigurationFile.GetPathConfigurationFile())
                        End If
                        End
                    End If
                End Using
            Else
                MessageIndigo.Show("Sin este archivo no se puede continuar", MessageType.Errores, Me.Text, Botones.Ok)
                End
            End If
        End If
        '''' Una tarea dentro de otra tarea se anida y es mas lento? / Pendiente averiguar porque este metodo comentado se rompe y segundo 
        'todo lo que se escriba antes del await del metodo async bloquea el hilo de ejecucion. probar con un sleep antes de await, despues de
        'await no bloquea
        'Dim xList = Await Task.Run(Async Function() As Task(Of List(Of Company))
        '                               Dim listCompanies As List(Of Company)
        '                               Using Model As New MLogin
        '                                   listCompanies = Await Model.ListCompanies()
        '                               End Using

        '                               Return listCompanies
        '                           End Function)
        Dim listCompanies As List(Of Company)
        Using Model As New MLogin
            listCompanies = Model.ListCompanies()
        End Using
        Return listCompanies
    End Function

    ''' <summary>
    ''' Metodo Asincrono para cargar Archivo de configuracion de servicios del HIS e Instanciamos la Singleton HIS
    ''' </summary>
    ''' <remarks></remarks>
    Private Function LoadSettingsHISAsync() As Task
        Return Task.Factory.StartNew(AddressOf LoadSettingsHIS)
    End Function

    ''' <summary>
    ''' Metodo Asincrono para cargar Archivo de configuracion de servicios del HIS e Instanciamos la Singleton HIS
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadSettingsHIS()

        Dim APPProtocoloWeb As String
        Dim APPProtocoloWebXPO As String
        Dim APPWSIndigo As String
        Dim APPWSIndigoXPO As String
        Dim APPTipoSession As String
        Dim APPRutaActualizacion As String
        Dim APPRutaReportes As String
        Dim APPRutaNotificacion As String
        Dim APPCacheLocal As Boolean
        ValoresSesion = IndigoValoresSesion.Instancia

        'Leo el archivo
        Dim dtConfiguracion As DataTable
        dtConfiguracion = IndigoConfiguracion.LeerArchivoConfiguracion

        'Descripto las cadenas
        If dtConfiguracion IsNot Nothing AndAlso dtConfiguracion.Rows.Count > 0 Then
            APPCacheLocal = Boolean.Parse(IndigoEncriptar.DesencritaCadena(dtConfiguracion.Rows(0).Item("CacheLocal").ToString))
            APPRutaReportes = IndigoEncriptar.DesencritaCadena(dtConfiguracion.Rows(0).Item("RutaReportes").ToString)
            APPProtocoloWeb = IndigoEncriptar.DesencritaCadena(dtConfiguracion.Rows(0).Item("ProtocoloWeb").ToString)
            APPWSIndigo = IndigoEncriptar.DesencritaCadena(dtConfiguracion.Rows(0).Item("ServicioWeb").ToString)
            APPProtocoloWebXPO = IndigoEncriptar.DesencritaCadena(dtConfiguracion.Rows(0).Item("ProtocoloXPO").ToString)
            APPWSIndigoXPO = IndigoEncriptar.DesencritaCadena(dtConfiguracion.Rows(0).Item("ServicioXPO").ToString)
            APPTipoSession = IndigoEncriptar.DesencritaCadena(dtConfiguracion.Rows(0).Item("BuscaActualizaciones").ToString)
            APPRutaActualizacion = IndigoEncriptar.DesencritaCadena(dtConfiguracion.Rows(0).Item("RutaActualizacion").ToString)
            APPRutaNotificacion = IndigoEncriptar.DesencritaCadena(dtConfiguracion.Rows(0).Item("ServicioNotificaciones").ToString)
        End If

        'Asigno valores en Singelton
        If APPProtocoloWeb = "netTcp" Then
            ValoresSesion.ProtocoloWebServices = IndigoValoresSesion.eProtocolos.netTcp
        Else
            ValoresSesion.ProtocoloWebServices = IndigoValoresSesion.eProtocolos.basicHttp
        End If
        ValoresSesion.UriWebServices = APPWSIndigo
        If APPProtocoloWebXPO = "netTcp" Then
            ValoresSesion.ProtocoloWebServicesXPO = IndigoValoresSesion.eProtocolos.netTcp
        Else
            ValoresSesion.ProtocoloWebServicesXPO = IndigoValoresSesion.eProtocolos.basicHttp
        End If
        ValoresSesion.UriWebServicesXPO = APPWSIndigoXPO

        ValoresSesion.TipoEjecucionSesion = CType(APPTipoSession, IndigoValoresSesion.EIndigoTipoEjecucionSesion)
        ValoresSesion.RutaActualizacion = APPRutaActualizacion
        ValoresSesion.RutadeReportes = APPRutaReportes
        ValoresSesion.CacheLocal = APPCacheLocal
        'ValoresSesion.RutaArchivosComunes = String.Concat(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "\Sistemas Asociados\Indigo Crystal.Net")
        'ValoresSesion.RutaArchivosPorUsuario = String.Concat(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "\Sistemas Asociados\Indigo Crystal.Net")
        ValoresSesion.RutaArchivosComunes = String.Concat(Window.Utils.LocalFolder(), "\HealtTech\Indigo Vie EHR")
        ValoresSesion.RutaArchivosPorUsuario = String.Concat(Infrastructure.CrossCutting.Base.Window.Utils.UserFolder(), "\HealtTech\Indigo Vie EHR")
        ValoresSesion.VersionDGH = IndigoValoresSesion.eVersionDGH.Nativo
        ValoresSesion.EmpresaDGH = SessionValues.Instance.TransactionalContainer 'Obsoleto, si lo utiliza el his
        'ValoresSesion.EmpresaIndigoNit = "900556261-7"
        ValoresSesion.VersionIndigoCrystal = "1.0.0.0"
        'ValoresSesion.EmpresaIndigo = "999"
        ValoresSesion.UrlWebServicesNoti = APPRutaNotificacion
        'ValoresSesion.UsuarioIndigo = "999"
        'ValoresSesion.UsuarioRol = "999"
        'ValoresSesion.UsuarioGrupo = "009"
        'ValoresSesion.Profesional = "999"
        'ValoresSesion.EspecialidadMedico1 = "002"
        'ValoresSesion.UsuarioCargo = ""
    End Sub

    ''' <summary>
    ''' Inicializa los Valores de sesion (Singleton ERP)
    ''' </summary>
    Public Sub InitSesionValues(ByVal usr As UserLogin)
        Dim Indigo As SessionValues = SessionValues.Instance
        Indigo.UserIndigo = usr.UserCode.Trim()
        Indigo.UserGroup = usr.GroupCode
        Indigo.UserEmail = usr.Email
        Indigo.UserIndigoId = usr.Id
        'Indigo.IdsFormVisible = usr.ListPermission  :TODO
        Dim MensajeAuditoria = New AuditMessage
        MensajeAuditoria.Company = Indigo.IndigoCompany
        MensajeAuditoria.CodeUser = usr.UserCode.Trim()
        MensajeAuditoria.ComputerName = Infrastructure.CrossCutting.Base.Utils.GetHostname
        MensajeAuditoria.WindowsUser = Infrastructure.CrossCutting.Base.Utils.GetOSUsername
        MensajeAuditoria.IdUser = usr.Id
        MensajeAuditoria.NameUser = Indigo.UserIndigoName
        MensajeAuditoria.CompanyType = Indigo.IndigoCompanyType
        MensajeAuditoria.DispensingIntegration = Indigo.IndigoDispensingIntegration
        Indigo.UserViewMode = usr.ViewForm
        Indigo.AuditMessageWcf = MensajeAuditoria
        Indigo.UserIndigoName = usr.Fullname
        Indigo.UserCharge = usr.Position
        Indigo.UserInterface = usr.CodeInterface
        Indigo.UserRol = usr.RollCode.ToString
        Indigo.AutoPersona = usr.IdPerson.ToString()
        SessionValues.Instance.UserType = CType(usr.UserType, UserType)
        SessionValues.Instance.ProfileType = CType(usr.ProfileType, eProfileType)

    End Sub

    ''' <summary>
    ''' Asigna la imagen de los dias especiales
    ''' </summary>
    Private Sub AssignImage(ByVal url As String, ByVal Title As String, ByVal Description As String)
        Me.PbxBackgroundLogin.Properties.BeginUpdate()
        Me.PbxBackgroundLogin.EditValue = Nothing
        Me.PbxBackgroundLogin.LoadAsync(url)
        Me.LblMessageDayTitle.Text = Title
        Me.LblMessageDayText.Text = Description
        Me.PbxBackgroundLogin.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.PbxBackgroundLogin.Properties.PictureAlignment = ContentAlignment.TopLeft
        Me.PbxBackgroundLogin.Properties.EndUpdate()
    End Sub

    ''' <summary>
    ''' Asigna la imagen de los dias especiales
    ''' </summary>
    Private Sub AssignImage(ByVal image As Bitmap, ByVal Title As String, ByVal Description As String)
        Me.PbxBackgroundLogin.Properties.BeginUpdate()
        Me.PbxBackgroundLogin.EditValue = Nothing
        Me.PbxBackgroundLogin.Image = image
        Me.LblMessageDayTitle.Text = Title
        Me.LblMessageDayText.Text = Description
        Me.PbxBackgroundLogin.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.PbxBackgroundLogin.Properties.PictureAlignment = ContentAlignment.TopLeft
        Me.PbxBackgroundLogin.Properties.EndUpdate()
    End Sub


    Private Function GetWeather(woeid As String) As Task(Of String)
        Dim request_str As String
        Dim ResultJS As String = ""
        Dim request As WebRequest
        Dim response As WebResponse
        request_str = "https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20weather.forecast%20where%20woeid%20%3D%20"
        request_str = request_str & woeid & "&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys"
        request = WebRequest.Create(request_str)
        response = request.GetResponse()
        Using SR As New StreamReader(response.GetResponseStream)
            ResultJS = SR.ReadToEnd
        End Using
        response.Close()
        Dim resultOb As Object = Infrastructure.CrossCutting.Base.Utils.DeserializeJsonToObject(ResultJS)
        Return resultOb.query.results.channel.item.condition.text
    End Function


#Region "Utilidades"
    Private Shared _modelAssembly As Reflection.Assembly = Nothing
    Public Shared ReadOnly Property ModelAssembly As Reflection.Assembly
        Get
            If _modelAssembly Is Nothing Then
                _modelAssembly = GetType(FrmLogin).Assembly
            End If
            Return _modelAssembly
        End Get
    End Property

    Private Shared resourceNames As String() = Nothing
    Public Shared Function GetResourcePath(resourceName As String) As String
        If resourceNames Is Nothing Then
            resourceNames = ModelAssembly.GetManifestResourceNames()
        End If
        For Each name As String In resourceNames
            If name.EndsWith(resourceName) Then
                Return name
            End If
        Next
        Return Nothing
    End Function

    Public Function LoadImage(imageName As String) As System.Drawing.Image
        Return DevExpress.Utils.ResourceImageHelper.CreateImageFromResources(FrmLogin.GetResourcePath(imageName), FrmLogin.ModelAssembly)
    End Function
#End Region

    ''' <summary>
    ''' Metodo para obtener las imagenes de los recursos de acuerdo al dia especial mostrar el clic en caso de que corresponda
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub GetResourceDaySpecial()
        If LoaderConfigurationFile.ConfigurationFileExists() Then
            If ConfigurationFile.Instance.DefaultCompanyType >= 0 And ConfigurationFile.Instance.DefaultCompanyType <= 2 Then
                Dim rutaServicio = ConfigurationFile.Instance.UrlNotificationWebServer
                Dim rutaImagenEspecial = $"{rutaServicio}Resources/{Date.Today.ToString("dd-MM")}.jpg"
                Select Case Date.Today.ToString("dd-MM")
                    Case "12-01"
                        AssignImage(rutaImagenEspecial, "Descubrimiento De La Penicilina", "Los fracasos son también útiles, porque, bien analizados, pueden conducir al éxito (Alexander Fleming)")
                    Case "15-01"
                        AssignImage(rutaImagenEspecial, "Aristoteles", "El fin de la ciencia especulativa es la verdad, y el fin de la ciencia práctica es la acción. (Aristóteles)")
                    Case "30-01"
                        AssignImage(rutaImagenEspecial, "Día Mundial De La No Violencia", "La violencia es el último recurso del incompetente. (Isaac Asimov)")
                    Case "15-02"
                        AssignImage(rutaImagenEspecial, "Dia Mundial del Niño con Cáncer", "La mejor receta para superar cualquier enfermedad, es una píldora de optimismo acompañada de una gran sonrisa")
                    Case "18-02"
                        AssignImage(rutaImagenEspecial, "Esculapio", "Únicamente la conciencia de aliviar males podrá sostenerte en tus fatigas (Esculapio)")
                    Case "01-03"
                        AssignImage(rutaImagenEspecial, "Día Del Contador", "El éxito de un negocio es directamente proporcional al buen manejo contable que tenga.")
                    Case "08-03"
                        AssignImage(rutaImagenEspecial, "Día De La Mujer", "La mujer es una obra de arte, que ilumina los ojos de quien la observa.")
                    Case "20-03"
                        AssignImage(rutaImagenEspecial, "Día Del Talento Familiar", "La importancia de una familia, radica en estar unidos, más que en vivir juntos.")
                    Case "21-03"
                        AssignImage(rutaImagenEspecial, "Día Mundial Del Síndrome De Down", "El amor es la energía que permite que los milagros se manifiesten día con día.")
                    Case "22-03"
                        AssignImage(rutaImagenEspecial, "Día Mundial Del Agua", "Agua = Vida. Conservación = Futuro")
                    Case "02-04"
                        AssignImage(rutaImagenEspecial, "Día Mundial De La Conciencia Para Los Autistas", "Conocer un poco más, te da más posibilidades de comprender y por ende de ayudar")
                    Case "07-04"
                        AssignImage(rutaImagenEspecial, "Día Mundial De La Salud", "La ignorancia es la peor enfermedad del ser humano, el conocimiento, la mejor medicina")
                    Case "10-04"
                        AssignImage(rutaImagenEspecial, "Día Del Investigador Científico", "La ciencia no es más que muchas repuestas fáciles a preguntas difíciles.")
                    Case "11-04"
                        AssignImage(rutaImagenEspecial, "Día Mundial Del Párkinson", "James Parkinson, médico clínico, sociólogo, botánico, geólogo, y paleontólogo británico, descubridor de la enfermedad que hoy lleva su nombre, enfermedad de Parkinson")
                    Case "13-04"
                        AssignImage(rutaImagenEspecial, "Día Del Quinesiologo", "La ciencia del movimiento")
                    Case "16-04"
                        AssignImage(rutaImagenEspecial, "Día Mundial Del Otorrinolaringólogo", "Otorrinolaringólogo, previene, diagnostica y trata las enfermedades que afectan a la garganta, la nariz y el oído.")
                    Case "17-04"
                        AssignImage(rutaImagenEspecial, "Día Mundial De La Hemofilia", "La coagulación es un proceso fundamental para la vida,  Un cambio en los hábitos de alimentación o de vida ayudan a mejorar el proceso.")
                    Case "22-04"
                        AssignImage(rutaImagenEspecial, "Día De La Tierra", "La tierra no es una herencia de nuestros padres, sino un préstamo de nuestros hijo. (Proverbio Indio)")
                    Case "25-04"
                        AssignImage(rutaImagenEspecial, "Día Mundial De La Malaria", "Controlar la malaria no solo sirve para mejorar la salud humana: potencia también el bienestar social y el desarrollo económico.")
                    Case "26-04"
                        AssignImage(rutaImagenEspecial, "Día De La Secretaria", "")
                    Case "27-04"
                        AssignImage(rutaImagenEspecial, "Día Del Diseñador Grafico", "La imaginación es más importante que el conocimiento. (Albert Einstein)")
                    Case "28-04"
                        AssignImage(rutaImagenEspecial, "Día Del Bacteriólogo", "Sorprendernos por algo es el primer paso de la mente hacia el descubrimiento.")
                    Case "29-04"
                        AssignImage(rutaImagenEspecial, "Día Del Árbol", "Los árboles, entre otros muchos beneficios, transforman el dióxido de carbono, responsable del efecto invernadero, en biomasa, minimizan los riesgos de inundación y, evita la erosión.")
                    Case "01-05"
                        AssignImage(rutaImagenEspecial, "Día Del Trabajo", "Elige un trabajo que te guste y no tendrás que trabajar ni un día de tu vida.")
                    Case "05-05"
                        AssignImage(rutaImagenEspecial, "Día Mundial Del Obstetra", "Promover la importancia de estos expertos para evitar la muerte de la madre y/o del bebé durante la gestación y los primeros meses de vida del pequeño.")
                    Case "10-05"
                        AssignImage(rutaImagenEspecial, "Día De La Madre", "El amor de una madre es el combustible que hace que un ser humano logre lo imposible")
                    Case "12-05"
                        AssignImage(rutaImagenEspecial, "Día Internacional De La Enfermera", "Un enfermo no sana por los medicamentos y tratamientos, sino por el amor, atención y cuidados de una buena enfermera.")
                    Case "14-05"
                        AssignImage(rutaImagenEspecial, "Vacuna Contra La Malaria", "En 1987, Manuel Elkin Patarroyo, desarrolló la primera vacuna sintética del mundo contra la malaria")
                    Case "17-05"
                        AssignImage(rutaImagenEspecial, "Día Mundial De La Hipertensión", "El mayor problema de ser hipertenso esta en desconocerlo.")
                    Case "21-05"
                        AssignImage(rutaImagenEspecial, "Día Mundial Del Asma", "El asma es una enfermedad de fácil diagnóstico y que, correctamente tratada, permite llevar una vida normal.")
                    Case "24-05"
                        AssignImage(rutaImagenEspecial, "Día Mundial De La Epilepsia", "El reconocimiento de las crisis y el conocimiento de qué hacer es importante puesto que es muy fácil confundir las crisis con otras enfermedades.")
                    Case "28-05"
                        AssignImage(rutaImagenEspecial, "Dia Mundial del La Nutrición", "La forma en cómo te alimentes hoy, será tu  estado de salud de mañana")
                    Case "31-05"
                        AssignImage(rutaImagenEspecial, "Día Mundial Sin Tabaco", "El tabaquismo mata cada año a casi 6 millones de personas, de las cuales más de 600 000 son no fumadores que mueren por respirar humo ajeno")
                    Case "05-06"
                        AssignImage(rutaImagenEspecial, "Día Del Medio Ambiente", "Empieza por ser tú, el cambio que exiges para el mundo.")
                    Case "06-06"
                        AssignImage(rutaImagenEspecial, "Día Mundial De Los Trasplantados", "Donar, es dar vida.")
                    Case "11-06"
                        AssignImage(rutaImagenEspecial, "Día Del Padre", "¡Qué grande riqueza es, aun entre los pobres, ser hijo de un buen padre!")
                    Case "14-06"
                        AssignImage(rutaImagenEspecial, "Día Mundial Del Donante De Sangre", "Dona sangre y salva una vida.")
                    Case "18-06"
                        AssignImage(rutaImagenEspecial, "Hipócrates Padres de la Medicina", "Juramento hipocrático, modelo de ética para los profesionales de la salud")
                    Case "22-06"
                        AssignImage(rutaImagenEspecial, "Día Del Abogado", "El derecho y el deber son como las palmeras: no dan frutos si no crecen uno al lado del otro. ( Félecité de Lamennais )")
                    Case "08-07"
                        AssignImage(rutaImagenEspecial, "Día Mundial De La Alergia", "El Tratamiento exitoso de las alergias incluye la detección puntual, el uso correcto de medicamentos y técnicas sencillas de evitar contacto con los alérgenos.")
                    Case "28-07"
                        AssignImage(rutaImagenEspecial, "Día Mundial De La Hepatitis", "el objetivo de este día es acrecentar la sensibilización y la comprensión de la hepatitis viral y las enfermedades que provoca.")
                    Case "01-08"
                        AssignImage(rutaServicio & "Resources/" & "01-08.jpg", "Semana Mundial De La Lactancia", "La lactancia es el vínculo de amor que vuelve sanos, fuertes y felices a nuestros hijos.")
                    Case "02-08"
                        AssignImage(rutaServicio & "Resources/" & "01-08.jpg", "Semana Mundial De La Lactancia", "La lactancia es el vínculo de amor que vuelve sanos, fuertes y felices a nuestros hijos.")
                    Case "03-08"
                        AssignImage(rutaServicio & "Resources/" & "01-08.jpg", "Semana Mundial De La Lactancia", "La lactancia es el vínculo de amor que vuelve sanos, fuertes y felices a nuestros hijos.")
                    Case "04-08"
                        AssignImage(rutaServicio & "Resources/" & "01-08.jpg", "Semana Mundial De La Lactancia", "La lactancia es el vínculo de amor que vuelve sanos, fuertes y felices a nuestros hijos.")
                    Case "05-08"
                        AssignImage(rutaServicio & "Resources/" & "01-08.jpg", "Semana Mundial De La Lactancia", "La lactancia es el vínculo de amor que vuelve sanos, fuertes y felices a nuestros hijos.")
                    Case "06-08"
                        AssignImage(rutaServicio & "Resources/" & "01-08.jpg", "Semana Mundial De La Lactancia", "La lactancia es el vínculo de amor que vuelve sanos, fuertes y felices a nuestros hijos.")
                    Case "07-08"
                        AssignImage(rutaServicio & "Resources/" & "01-08.jpg", "Semana Mundial De La Lactancia", "La lactancia es el vínculo de amor que vuelve sanos, fuertes y felices a nuestros hijos.")
                    Case "11-08"
                        AssignImage(rutaImagenEspecial, "Día Del Nutricionista", "Para comer bien hay que asesorarse aún mejor.")
                    Case "12-08"
                        AssignImage(rutaImagenEspecial, "Día Mundial De La Influenza", "Prevenir la influenza es responsabilidad mía, tuya, de él, de ella, de todos.")
                    Case "17-08"
                        AssignImage(rutaImagenEspecial, "Día Del Ingeniero", "Para concebir una perfección se requiere cierto nivel técnico y ético y es indispensable alguna educación intelectual. (José Ingenieros).")
                    Case "28-08"
                        AssignImage(rutaImagenEspecial, "Día Del Adulto Mayor", "Saber envejecer es la obra maestra de la vida, y una de las cosas más difíciles en el arte dificilísimo de la vida. (Amiel)")
                    Case "01-09"
                        AssignImage(rutaImagenEspecial, "Día Del Químico Farmacéutico", "Los laboratorios producen medicamentos, los farmacéutico entregamos esperanzas (Rony Condeña Parvina).")
                    Case "06-09"
                        AssignImage(rutaImagenEspecial, "Día Del Fonoaudiólogo", "La comunicación existe en cada detalle de la vida.")
                    Case "09-09"
                        AssignImage(rutaImagenEspecial, "Día Del Fisioterapeuta", "Pensar en rehabilitación física, es pensar en la humanidad- (Haward Rusk).")
                    Case "10-09"
                        AssignImage(rutaImagenEspecial, "Día Mundial para la Prevención del Suicidio", "Luchar por algo, le da significado a nuestra vida, y todos tenemos algo por lo cual luchar y armas para vencer.")
                    Case "15-09"
                        AssignImage(rutaImagenEspecial, "Día Del Gerontólogo", "Saber envejecer es una obra maestra de la sabiduría y una de las partes más difíciles del gran arte de vivir. (Henri Frederic Amiel)")
                    Case "21-09"
                        AssignImage(rutaImagenEspecial, "Día Mundial Del Alzheimer", "La enfermedad de Alzheimer no solo se trata con terapia, sino también con cariño.")
                    Case "23-09"
                        AssignImage(rutaImagenEspecial, "Día Mundial Del Corazón", "Todo cuenta a la hora de tener un corazón sano")
                    Case "01-10"
                        AssignImage(rutaImagenEspecial, "Día Internacional De La Personas Sordas", "Qué importa la sordera del oído cuando la mente oye, la verdadera sordera, la incurable sordera es la de la mente. (Victor Hugo)")
                    Case "03-10"
                        AssignImage(rutaImagenEspecial, "Día Del Odontólogo", "Es verdad que optamos por la risa en casi todas las situaciones, con excepción de una que otra visita al dentista (Joseph Heller).")
                    Case "07-10"
                        AssignImage(rutaImagenEspecial, "Dia del Terapeuta Respiratorio", "Más que una labor, es el sueño de servir")
                    Case "12-10"
                        AssignImage(rutaImagenEspecial, "Día Mundial De La Artritis", "Cuando hablamos de artritis, no nos referimos a una causa única, pues existen más de 100 enfermedades diferentes que pueden causarla")
                    Case "14-10"
                        AssignImage(rutaImagenEspecial, "Día Mundial De La Vista", "Cuida tus ojos, pues no estás preparado para vivir sin ver")
                    Case "15-10"
                        AssignImage(rutaImagenEspecial, "Día Mundial Del Lavado De Manos", "Lavarse continúa siendo una de las intervenciones más costo-efectiva contra la transmisión de enfermedades que se dispone en salud pública.")
                    Case "16-10"
                        AssignImage(rutaImagenEspecial, "Día Mundial Del Anestesiólogo", "El anestesiólogo representa un papel central en el quirófano, ya que protege y regula las funciones vitales básicas del paciente durante la cirugía.")
                    Case "17-10"
                        AssignImage(rutaImagenEspecial, "Día Mundial Del Dolor", "Si en el mundo no existiera el dolor, entonces la alegría no tendría valor. (José Ignacio)")
                    Case "18-10"
                        AssignImage(rutaImagenEspecial, "Día Mundial De la menopausia", "No es una enfermedad, es una etapa más en la de vida de toda mujer.")
                    Case "19-10"
                        AssignImage(rutaImagenEspecial, "Día Mundial Del Cáncer De Mama", "Prever el cáncer de mama está en tus manos.")
                    Case "20-10"
                        AssignImage(rutaImagenEspecial, "Día Mundial Del Pediatra", "Uno más de la familia")
                    Case "21-10"
                        AssignImage(rutaImagenEspecial, "Día Mundial Del Ahorro De Energía", "Dejar el cargador conectado, después de usarlo, también consume energía,  ¡desenchufalo!")
                    Case "25-10"
                        AssignImage(rutaImagenEspecial, "Día Del Instrumentador", "Asistir al cirujano y participar activamente en el procedimiento quirúrgico también es una profesión.")
                    Case "08-11"
                        AssignImage(rutaImagenEspecial, "Día Mundial Del Radiólogo", "Se conmemora el descubrimiento en 1895 de los rayos X por el Dr. Wilhelm Conrad Röntgen")
                    Case "14-11"
                        AssignImage(rutaImagenEspecial, "Día Mundial De La Diabetes", "Cada 10 segundos una persona muere por culpa de la Diabetes.")
                    Case "17-11"
                        AssignImage(rutaImagenEspecial, "Día Mundial De La Enfermedad Obstructiva Crónica", "Dejar de fumar es el primer paso en la prevención y tratamiento de esta enfermedad.")
                    Case "19-11"
                        AssignImage(rutaImagenEspecial, "Día Mundial Del Aire Puro", "Elegir vivir, es elegir respirar aire puro")
                    Case "24-11"
                        AssignImage(rutaImagenEspecial, "Día Del Psicólogo", "Nuestra psicología se construye sobre pocos seres: elegid bien al que se ama o al que se odia. (Jean Rostand)")
                    Case "25-11"
                        AssignImage(rutaImagenEspecial, "Día Mundial De La Anorexia", "La mejor inversión está en tu salud")
                    Case "01-12"
                        AssignImage(rutaImagenEspecial, "Día Mundial Del VIH Sida", "Lo mortal del VIH, es no saber que lo tienes")
                    Case "03-12"
                        AssignImage(rutaImagenEspecial, "Día Internacional De Los Discapacitados", "La discapacidad más grave es la indiferencia ante cualquier cosa")
                    Case "12-12"
                        AssignImage(rutaImagenEspecial, "Leonardo da Vinci Padres de la Medicina", "Aquel que más posee, más miedo tiene de perderlo (Leonardo Da Vinci)")
                    Case Else
                        'AssignImage(My.Resources.ImageVideoEye, "En Indigo Technologies estamos humanizando la salud", String.Empty)
                        Dim result As ActionMessageResult(Of WeatherDocument) = Nothing
                        Try
                            result = Await CloudAgent.IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetWeatherCityAsync(ConfigurationFile.Instance.City)
                        Catch
                        End Try
                        If result IsNot Nothing AndAlso result.StateResult = True Then
                            Dim rutaVideo = ConfigurationFile.Instance.UrlNotificationWebServer & "Resources/Weather/"
                            With result.ObjectEmbbeded
                                lb2.Text = result.ObjectEmbbeded.NameCity
                                Select Case .ConditionCode.ToString()
                                    Case "0"
                                        AssignImage("", "En Vie HealtTech estamos humanizando la salud", String.Empty)
                                        INDmeImagenWather.Source = New Uri(rutaVideo & "")
                                    Case "1"
                                        AssignImage(GetStringImgWeather(WeatherEnum.Dia_Nublado), "Cargando Información del Clima...", String.Empty)
                                        INDmeImagenWather.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Dia_Nublado))
                                    Case "2"
                                        AssignImage(GetStringImgWeather(WeatherEnum.Dia_Nublado), "Cargando Información del Clima...", String.Empty)
                                        INDmeImagenWather.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Dia_Nublado))
                                    Case "3"
                                        AssignImage(GetStringImgWeather(WeatherEnum.Lluvia_Trueno), "Cargando Información del Clima...", String.Empty)
                                        INDmeImagenWather.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Lluvia_Trueno))
                                    Case "4"
                                        AssignImage(GetStringImgWeather(WeatherEnum.Lluvia_Trueno), "Cargando Información del Clima...", String.Empty)
                                        INDmeImagenWather.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Lluvia_Trueno))
                                    Case "5"
                                        INDmeImagenWather.Source = New Uri("http://ak3.picdn.net/shutterstock/videos/3750878/preview/stock-footage-winter-landscape-with-falling-snow.mp4")
                                    Case "6"
                                        INDmeImagenWather.Source = New Uri("http://ak.picdn.net/shutterstock/videos/1873507/preview/stock-footage-the-rain-turned-into-sleet-on-the-leaves-of-ivy-close-up.mp4")
                                    Case "7"
                                        INDmeImagenWather.Source = New Uri("http://ak3.picdn.net/shutterstock/videos/3750878/preview/stock-footage-winter-landscape-with-falling-snow.mp4")
                                    Case "8"
                                        AssignImage(GetStringImgWeather(WeatherEnum.Dia_Nublado), "Cargando Información del Clima...", String.Empty)
                                        INDmeImagenWather.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Dia_Nublado))
                                    Case "9"
                                        AssignImage(GetStringImgWeather(WeatherEnum.Dia_Escampo), "Cargando Información del Clima...", String.Empty)
                                        INDmeImagenWather.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Dia_Escampo))
                                    Case "10"
                                        AssignImage(GetStringImgWeather(WeatherEnum.Dia_Nublado), "Cargando Información del Clima...", String.Empty)
                                        INDmeImagenWather.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Dia_Nublado))
                                    Case "11"
                                        AssignImage(GetStringImgWeather(WeatherEnum.Dia_Nublado), "Cargando Información del Clima...", String.Empty)
                                        INDmeImagenWather.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Dia_Nublado))
                                    Case "12"
                                        AssignImage(GetStringImgWeather(WeatherEnum.Dia_Nublado), "Cargando Información del Clima...", String.Empty)
                                        INDmeImagenWather.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Dia_Nublado))
                                    Case "13"
                                        INDmeImagenWather.Source = New Uri("http://ak3.picdn.net/shutterstock/videos/3750878/preview/stock-footage-winter-landscape-with-falling-snow.mp4")
                                    Case "14"
                                        AssignImage(GetStringImgWeather(WeatherEnum.Dia_Nublado), "Cargando Información del Clima...", String.Empty)
                                        INDmeImagenWather.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Dia_Nublado))
                                    Case "15"
                                        INDmeImagenWather.Source = New Uri("http://ak3.picdn.net/shutterstock/videos/3750878/preview/stock-footage-winter-landscape-with-falling-snow.mp4")
                                    Case "16"
                                        INDmeImagenWather.Source = New Uri("http://ak3.picdn.net/shutterstock/videos/3750878/preview/stock-footage-winter-landscape-with-falling-snow.mp4")
                                    Case "17"
                                        INDmeImagenWather.Source = New Uri("http://ak8.picdn.net/shutterstock/videos/4838402/preview/stock-footage-hail-raining-down-on-a-wood-deck-hockinson-washington.mp4")
                                    Case "18"
                                        INDmeImagenWather.Source = New Uri("http://ak.picdn.net/shutterstock/videos/1873507/preview/stock-footage-the-rain-turned-into-sleet-on-the-leaves-of-ivy-close-up.mp4")
                                    Case "19"
                                        AssignImage(GetStringImgWeather(WeatherEnum.Dia_Nublado), "Cargando Información del Clima...", String.Empty)
                                        INDmeImagenWather.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Dia_Nublado))
                                    Case "20"
                                        INDmeImagenWather.Source = New Uri("http://ak7.picdn.net/shutterstock/videos/3171649/preview/stock-footage-morning-sunrise-reflection-in-misty-fog-rise-from-flowing-river-water.mp4")
                                    Case "21"
                                        AssignImage(GetStringImgWeather(WeatherEnum.Dia_Nublado), "Cargando Información del Clima...", String.Empty)
                                        INDmeImagenWather.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Dia_Nublado))
                                    Case "22"
                                        AssignImage(GetStringImgWeather(WeatherEnum.Dia_Nublado), "Cargando Información del Clima...", String.Empty)
                                        INDmeImagenWather.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Dia_Nublado))
                                    Case "23"
                                        AssignImage(GetStringImgWeather(WeatherEnum.Dia_Nublado), "Cargando Información del Clima...", String.Empty)
                                        INDmeImagenWather.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Dia_Nublado))
                                    Case "24"
                                        AssignImage(GetStringImgWeather(WeatherEnum.Dia_Nublado), "Cargando Información del Clima...", String.Empty)
                                        INDmeImagenWather.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Dia_Nublado))
                                    Case "25"
                                        AssignImage(GetStringImgWeather(WeatherEnum.Dia_Nublado), "Cargando Información del Clima...", String.Empty)
                                        INDmeImagenWather.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Dia_Nublado))
                                    Case "26"
                                        AssignImage(GetStringImgWeather(WeatherEnum.Dia_Nublado), "Cargando Información del Clima...", String.Empty)
                                        INDmeImagenWather.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Dia_Nublado))
                                    Case "27"
                                        AssignImage(GetStringImgWeather(WeatherEnum.Noche_Nublada), "Cargando Información del Clima...", String.Empty)
                                        INDmeImagenWather.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Noche_Nublada))
                                    Case "28"
                                        AssignImage(GetStringImgWeather(WeatherEnum.Dia_Nublado), "Cargando Información del Clima...", String.Empty)
                                        INDmeImagenWather.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Dia_Nublado))
                                    Case "29"
                                        AssignImage(GetStringImgWeather(WeatherEnum.Noche_Parcial_Nublada), "Cargando Información del Clima...", String.Empty)
                                        INDmeImagenWather.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Noche_Parcial_Nublada))
                                    Case "30"
                                        AssignImage(GetStringImgWeather(WeatherEnum.Dia_Parcial_Nublado), "Cargando Información del Clima...", String.Empty)
                                        INDmeImagenWather.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Dia_Parcial_Nublado))
                                    Case "31"
                                        AssignImage(GetStringImgWeather(WeatherEnum.Noche_Despejada), "Cargando Información del Clima...", String.Empty)
                                        INDmeImagenWather.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Noche_Despejada))
                                    Case "32"
                                        AssignImage(GetStringImgWeather(WeatherEnum.Dia_Soleado), "Cargando Información del Clima...", String.Empty)
                                        INDmeImagenWather.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Dia_Soleado))
                                    Case "33"
                                        AssignImage(GetStringImgWeather(WeatherEnum.Dia_Nublado), "Cargando Información del Clima...", String.Empty)
                                        INDmeImagenWather.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Dia_Nublado))
                                    Case "34"
                                        AssignImage(GetStringImgWeather(WeatherEnum.Dia_Nublado), "Cargando Información del Clima...", String.Empty)
                                        INDmeImagenWather.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Dia_Nublado))
                                    Case "35"
                                        INDmeImagenWather.Source = New Uri("http://ak8.picdn.net/shutterstock/videos/4838402/preview/stock-footage-hail-raining-down-on-a-wood-deck-hockinson-washington.mp4")
                                    Case "36"
                                        AssignImage(GetStringImgWeather(WeatherEnum.Dia_Soleado), "Cargando Información del Clima...", String.Empty)
                                        INDmeImagenWather.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Dia_Soleado))
                                    Case "37"
                                        AssignImage(GetStringImgWeather(WeatherEnum.Lluvia_Trueno), "Cargando Información del Clima...", String.Empty)
                                        INDmeImagenWather.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Lluvia_Trueno))
                                    Case "38"
                                        AssignImage(GetStringImgWeather(WeatherEnum.Lluvia_Trueno), "Cargando Información del Clima...", String.Empty)
                                        INDmeImagenWather.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Lluvia_Trueno))
                                    Case "39"
                                        AssignImage(GetStringImgWeather(WeatherEnum.Lluvia_Trueno), "Cargando Información del Clima...", String.Empty)
                                        INDmeImagenWather.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Lluvia_Trueno))
                                    Case "40"
                                        AssignImage(GetStringImgWeather(WeatherEnum.Dia_Lloviendo), "Cargando Información del Clima...", String.Empty)
                                        INDmeImagenWather.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Dia_Lloviendo))
                                    Case "41"
                                        INDmeImagenWather.Source = New Uri("http://ak3.picdn.net/shutterstock/videos/3750878/preview/stock-footage-winter-landscape-with-falling-snow.mp4")
                                    Case "42"
                                        INDmeImagenWather.Source = New Uri("http://ak3.picdn.net/shutterstock/videos/3750878/preview/stock-footage-winter-landscape-with-falling-snow.mp4")
                                    Case "43"
                                        INDmeImagenWather.Source = New Uri("http://ak3.picdn.net/shutterstock/videos/3750878/preview/stock-footage-winter-landscape-with-falling-snow.mp4")
                                    Case "44"
                                        AssignImage(GetStringImgWeather(WeatherEnum.Dia_Nublado), "Cargando Información del Clima...", String.Empty)
                                        INDmeImagenWather.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Dia_Nublado))
                                    Case "45"
                                        AssignImage(GetStringImgWeather(WeatherEnum.Lluvia_Trueno), "Cargando Información del Clima...", String.Empty)
                                        INDmeImagenWather.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Lluvia_Trueno))
                                    Case "46"
                                        INDmeImagenWather.Source = New Uri("http://ak3.picdn.net/shutterstock/videos/3750878/preview/stock-footage-winter-landscape-with-falling-snow.mp4")
                                    Case "47"
                                        AssignImage(GetStringImgWeather(WeatherEnum.Dia_Lloviendo), "Cargando Información del Clima...", String.Empty)
                                        INDmeImagenWather.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Dia_Lloviendo))
                                End Select
                                ElementHost1.Dock = DockStyle.Fill
                                ElementHost1.Child = INDmeImagenWather
                                INDmeImagenWather.Visibility = System.Windows.Visibility.Visible
                                INDmeImagenWather.LoadedBehavior = System.Windows.Controls.MediaState.Manual
                                INDmeImagenWather.Stretch = System.Windows.Media.Stretch.Fill
                                AddHandler INDmeImagenWather.Loaded, AddressOf MediaElement_Loaded
                                'AddHandler md.BufferingStarted, AddressOf MediaElement_BufferingStarted
                                AddHandler INDmeImagenWather.MediaEnded, AddressOf MediaElement_MediaEnded
                                INDmeImagenWather.Play()
                                PbxBackgroundLogin.SendToBack()
                            End With
                        Else
                            AssignImage(My.Resources.ImageVideoEye, "En Vie HealtTech estamos humanizando la salud", String.Empty)
                        End If
                End Select
            Else
                AssignImage(My.Resources.ImageForMayors, "Ciudades Inteligentes", "En las ciudades inteligentes hay que tener muy claro qué se persigue y dónde están las rentabilidades, sociales y/o económicas.")
            End If
        Else
            AssignImage(My.Resources.ImageVideoEye, "En Vie HealtTech estamos humanizando la salud", String.Empty)
        End If
        'md = New System.Windows.Controls.MediaElement()
        'md.Source = New System.Uri("D:\dia-nublado.mov") 'http://ak7.picdn.net/shutterstock/videos/3671960/preview/stock-footage-flight-over-clouds-loop-able-animation.mp4")
        'ElementHost1.Location = Me.Location
        'ElementHost1.Child = md 'elthst.Size = New System.Drawing.Size(md.Width, md.Height)
        'Me.Controls.Add(ElementHost1)
        'md.LoadedBehavior = System.Windows.Controls.MediaState.Manual
        'md.Stretch = System.Windows.Media.Stretch.Fill
        'AddHandler md.Loaded, AddressOf MediaElement_Loaded
        ''AddHandler md.BufferingStarted, AddressOf MediaElement_BufferingStarted
        'AddHandler md.MediaEnded, AddressOf MediaElement_MediaEnded
        'md.Play()
    End Sub


    Private Sub MediaElement_MediaEnded(sender As Object, e As System.Windows.RoutedEventArgs)
        INDmeImagenWather.Stop()
        INDmeImagenWather.Position = TimeSpan.Zero
        INDmeImagenWather.Play()
    End Sub

    Private Sub MediaElement_BufferingStarted(sender As Object, e As System.Windows.RoutedEventArgs)
        ElementHost1.BringToFront()
    End Sub

    Private Sub MediaElement_Loaded(sender As Object, e As System.Windows.RoutedEventArgs)
        ElementHost1.BringToFront()
    End Sub

    Public Shared Function GetStringWeather(weather As WeatherEnum) As String
        Select Case weather
            Case WeatherEnum.Cielo_Despejado
                Return "dia-cielo-despejado.m4v"
            Case WeatherEnum.Dia_Lloviendo
                Return "dia-lloviendo.m4v"
            Case WeatherEnum.Dia_Nublado
                Return "dia-nublado.m4v"
            Case WeatherEnum.Dia_Soleado 'Falta
                Return "Día soleado.m4v"
                'Return "dia-cielo-despejado.mov"
            Case WeatherEnum.Dia_Parcial_Nublado 'Falta
                Return "dia-cielo-despejado.m4v"
            Case WeatherEnum.Dia_Escampo
                Return "dia-escampo.m4v"
            Case WeatherEnum.Noche_Despejada
                Return "noche-despejada.m4v"
            Case WeatherEnum.Lluvia_Trueno
                Return "lluvia-trueno.m4v"
            Case WeatherEnum.Noche_Parcial_Nublada
                Return "noche-parcialmente-nublada.m4v"
            Case WeatherEnum.Noche_Nublada
                Return "noche-nublada.m4v"
        End Select
    End Function

    Public Shared Function GetStringImgWeather(weather As WeatherEnum) As Bitmap
        Select Case weather
            Case WeatherEnum.Cielo_Despejado
                Return My.Resources.dia_cielo_despejado
            Case WeatherEnum.Dia_Lloviendo
                Return My.Resources.dia_lloviendo
            Case WeatherEnum.Dia_Nublado
                Return My.Resources.dia_nublado
            Case WeatherEnum.Dia_Soleado 'Falta
                Return My.Resources.dia_cielo_despejado
            Case WeatherEnum.Dia_Parcial_Nublado 'Falta
                Return My.Resources.dia_cielo_despejado
            Case WeatherEnum.Dia_Escampo
                Return My.Resources.dia_escampo
            Case WeatherEnum.Noche_Despejada
                Return My.Resources.noche_despejada
            Case WeatherEnum.Lluvia_Trueno
                Return My.Resources.lluvia_trueno
            Case WeatherEnum.Noche_Parcial_Nublada
                Return My.Resources.noche_parcialmente_nublada
            Case WeatherEnum.Noche_Nublada
                Return My.Resources.noche_nublada
        End Select
        Return My.Resources.ImageVideoEye
    End Function

    Public Enum WeatherEnum
        Cielo_Despejado
        Dia_Lloviendo
        Dia_Nublado
        Dia_Parcial_Nublado 'Pendiente
        Dia_Escampo
        Dia_Soleado 'Pendiente
        Noche_Despejada
        Lluvia_Trueno
        Noche_Parcial_Nublada
        Noche_Nublada
    End Enum

    Private Sub Txt_GotFocus(sender As Object, e As EventArgs) Handles TxtLoginBox.GotFocus, TxtPasswordBox.GotFocus
        If CType(sender, DevExpress.XtraEditors.TextEdit).Name.Equals("TxtLoginBox") Then
            PnlLoginBoxFocus.Appearance.BackColor = Color.FromArgb(244, 242, 242)
            PnlPasswordBoxFocus.Appearance.BackColor = Color.White
            PnlLoginBoxLineFocus.Appearance.BackColor = Color.FromArgb(0, 70, 109)
            PnlPasswordBoxLineFocus.Appearance.BackColor = Color.White
        Else
            PnlLoginBoxFocus.Appearance.BackColor = Color.White
            PnlPasswordBoxFocus.Appearance.BackColor = Color.FromArgb(244, 242, 242)
            PnlLoginBoxLineFocus.Appearance.BackColor = Color.White
            PnlPasswordBoxLineFocus.Appearance.BackColor = Color.FromArgb(0, 70, 109)
        End If
    End Sub

    Private Sub ProgressPanel1_Click(sender As Object, e As EventArgs)

    End Sub


#End Region

End Class