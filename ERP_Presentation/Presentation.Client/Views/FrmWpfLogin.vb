'***********************************************************************
' Assembly         : Presentacion.Cliente.MVP
' Author           : Oscar Sierra
' Created          : 03-03-2011
'
' Last Modified By : Jorge Leonardo Vernaza
' Last Modified On : 11-09-2013
' Description      : 
'
' Copyright        : (c) . All rights reserved
'***********************************************************************
#Region "Importaciones"
Imports System.IO
Imports System.Linq
Imports System.Net
Imports System.Net.Sockets
Imports DevExpress.XtraEditors.Controls
Imports Domain.Base.Entities
Imports Domain.Security.Entities
Imports IndigoEncript
Imports IndigoLibrary
Imports IndigoSingleton
Imports Infrastructure.Base.Security
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Base.Window
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Client.MVP
Imports Presentation.Security

#End Region
''' <summary>
''' Clase que controla los comprotamientos del formulario FrmLogin
''' </summary>
Public Class FrmWpfLogin
    Implements IDisposable

#Region "Constructor y Variables Globales de Clase"

    Private appVersion As Version
    Private companyVersion As Version
    Private IsLogin As Boolean
    Private IsUpdating As Boolean
    Private updaterBackground As New System.ComponentModel.BackgroundWorker
    Public _flagLockedSession As Boolean
    Public _UserCode As String = ""

    'Definioms variables de tipo Task para retornar valores que deben ser accesados desde otros metodos
    'Y que se ejecutan de forma paralela
    Private taskGetCompanies As Task(Of List(Of Company))
    Private taskLoadSettingsHIS As Task
    Private ValoresSesion As IndigoValoresSesion
    Dim indigoApplicationSetting As Infrastructure.CrossCutting.Base.ApplicationSetting
    'Public Event AutenticateHIS(user As String)'se llama en el evento INDgleCompanies.EditValueChanged
    Public Event AssingCompanies(listCompanies As List(Of Company), listCodeCompanyPermission As List(Of CompanyPermission))
    Public Event LoginOk()

    ''' <summary>
    ''' Constructor del login 
    ''' </summary>
    ''' <remarks></remarks>
    Sub New()
        indigoApplicationSetting = ApplicationSetting.Instance
        appVersion = BaseClass.GetAppVersion()
        taskGetCompanies = GetCompaniesAsync()
        taskLoadSettingsHIS = LoadSettingsHISAsync()
        TokenManager.Instance.ClearToken()
        TokenManager.Instance.SetFlagValidateToken(False)
        InitializeComponent()
        Me.INDglvCompanies.ShowLoadingPanel()
        Me.WindowState = FormWindowState.Normal
        Me.Bounds = Screen.PrimaryScreen.WorkingArea
        Me.WindowState = FormWindowState.Normal
        Me.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size
        Me.MinimumSize = Screen.PrimaryScreen.WorkingArea.Size
        Me.Location = New Point(0, 0)
        Me.Opacity = 100

        AddHandler FrmLoginWPF1.LocationCompany, AddressOf PositionControl
        AddHandler FrmLoginWPF1.Login, AddressOf BtnLogin_Click
        AddHandler FrmLoginWPF1.Exit_, AddressOf INDbtnCerrar_Click
        AddHandler FrmLoginWPF1.Home, AddressOf GleOptions_ButtonClick
        AddHandler FrmLoginWPF1.Change_company, AddressOf GleOptions_QueryPopUpAsync
        AddHandler FrmLoginWPF1.LoadImage, AddressOf LoadImage

        AddHandler updaterBackground.DoWork, AddressOf updaterBackground_DoWork
        AddHandler updaterBackground.RunWorkerCompleted, AddressOf updaterBackground_RunWorkerCompleted

        Me.Focus()
    End Sub

#End Region

#Region "Eventos"
    ''' <summary>
    ''' Cerrar del todo la aplicacion 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDbtnCerrar_Click()
        End
    End Sub

    ''' <summary>
    ''' Click en el boton Home de Indigo - Dirige a www.indigo.ms 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GleOptions_ButtonClick()
        Using proceso As New System.Diagnostics.Process
            proceso.StartInfo.FileName = "http://www.indigo.ms/"
            proceso.Start()
        End Using
    End Sub

    ''' <summary>
    ''' Aqui validamos la versión del esquema contra la versión de la aplicación
    ''' </summary>
    Private Sub GleOptions_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles INDglCompanies.EditValueChanging
        If Not updaterBackground.IsBusy Then
            Me.Cursor = BaseClass.ChangeCursorIndigo
            LoadingWpf(True, "Buscando nuevas versiones...")

            IsUpdating = True
            updaterBackground.RunWorkerAsync()
        End If

#If Not DEBUG Then
        Dim comp As Company = CType(INDglCompanies.Properties.View.GetFocusedRow(), Company)
        Try
            If Not comp.Version.Trim().Equals(String.Empty) Then
                Dim vDbSchema As Version = New Version(comp.Version)
                If Not vDbSchema.Equals(BaseClass.GetAppVersion()) Then
                    e.Cancel = True
                    Dim frm As New Presentation.Controls.FrmNotificationItemDetail
                    frm.TxtMessage.Text = "La versión del esquema en la empresa " & comp.Code & " - " & comp.Name & ", NO corresponde a la versión de éste cliente (" & BaseClass.GetAppVersion().ToString() & ")." & vbCrLf & "Porfavor actualice la versión del cliente e intente de nuevo."
                    Using transparent As New FrmTransparent(frm, False)
                        transparent.ShowDialog(Me)
                    End Using
                End If
            Else
                e.Cancel = True
                Dim frm As New Presentation.Controls.FrmNotificationItemDetail
                frm.TxtMessage.Text = "La versión del esquema en la empresa " & comp.Code & " - " & comp.Name & ", NO corresponde a la versión de éste cliente (" & BaseClass.GetAppVersion().ToString() & ")." & vbCrLf & "Porfavor actualice la versión del cliente e intente de nuevo."
                Using transparent As New FrmTransparent(frm, False)
                    transparent.ShowDialog(Me)
                End Using
            End If
        Catch ex As Exception
            e.Cancel = True
            Dim frm As New Presentation.Controls.FrmNotificationItemDetail
            frm.TxtMessage.Text = "La versión del esquema en la empresa " & comp.Code & " - " & comp.Name & ", NO corresponde a la versión de éste cliente (" & BaseClass.GetAppVersion().ToString() & ")." & vbCrLf & "Porfavor actualice la versión del cliente e intente de nuevo."
            Using transparent As New FrmTransparent(frm, False)
                transparent.ShowDialog(Me)
            End Using
        End Try
#End If
    End Sub

    ''' <summary>
    ''' Lista las empresas disponibles - Evento Asicrono
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GleOptions_QueryPopUpAsync()
        Me.BeginInvoke(Sub()
                           INDglCompanies.ShowPopup()
                       End Sub)
    End Sub

    Private Async Sub GridLookUpEdit1_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDglCompanies.QueryPopUp
        INDglCompanies.Location = MousePosition
        If taskGetCompanies IsNot Nothing AndAlso taskGetCompanies.Status = TaskStatus.Faulted Then
            taskGetCompanies = GetCompaniesAsync()
        End If
        INDglCompanies.Properties.DataSource = Await taskGetCompanies
        Me.INDglvCompanies.HideLoadingPanel()
    End Sub


    ''' <summary>
    ''' Metodo para establecer los valores de la Singleton del ERP en el proceso de login cuando se carga la empresa por default o cuando se dispara el evento change del Popup Empresas
    ''' </summary>
    ''' <param name="companySelected"></param>
    ''' <remarks></remarks>
    Private Sub SetERPSingletonValues(ByVal companySelected As Company)
        'TODO: Verificar todas las variagles de la singleton ERP y HIS
        Dim ips = GetHostAndNetworkIP()
        With SessionValues.Instance
            .IndigoContainerId = companySelected.Id
            .HisContainer = companySelected.HISContainer
            .IndigoCompany = companySelected.Code
            .DocumentalContainer = companySelected.DocumentalContainer
            .TransactionalContainer = companySelected.TransactionalContainer
            .FoundationalContainer = companySelected.FoundationalContainer
            .InteropCostContainer = companySelected.InteropCostContainer
            .IndigoCompanyType = companySelected.CompanyType
            .IndigoGlossesIntegration = companySelected.GlossesIntegration
            .IntergrationHisStatus = If(companySelected.HISIntegration = 0, IntegrationStatus.NonIntegrated, IntegrationStatus.InProgress)
            .IndigoPayrollIntegration = companySelected.PayrollIntegration
            .IndigoHumanTalentIntegration = companySelected.HumanTalentIntegration
            .IndigoDispensingIntegration = companySelected.DispensingIntegration
            .VituelContainer = companySelected.VituelContainer
            .IndigoCompanyAddress = companySelected.Address
            .IndigoCompanyName = companySelected.Name
            .IndigoCompanyNit = companySelected.CompanyNit
            .IndigoVerificationDigitNit = companySelected.VerificationDigitNit
            .IndigoCompanyPhoneNumber = companySelected.Telephone
            .TenantId = companySelected.TenantId
            .ServiceConfigurationId = companySelected.ServiceConfigurationId
            .ArchitectureType = companySelected.ArchitectureType
            .DecimalSeparator = companySelected.DecimalSeparator
            .IndigoVersion = companySelected.Version
            .HostName = ips.HostName
            .NetworkIP = ips.NetworkIp
            ValoresSesion.EmpresaDGH = companySelected.TransactionalContainer 'obsoleto
            ValoresSesion.EmpresaIndigoNit = companySelected.CompanyNit + "-" + companySelected.VerificationDigitNit
            ValoresSesion.EmpresaIndigoFundamentales = companySelected.FoundationalContainer.Replace("INDIGO", "")
            ValoresSesion.EmpresaIndigo = companySelected.HISContainer.Trim.Replace("INDIGO", "")
            ValoresSesion.EmpresaIndigoNombre = companySelected.Name.Trim
            ValoresSesion.ArchitectureType = companySelected.ArchitectureType
            If .SecurityContainer Is Nothing OrElse .SecurityContainer.Trim().Equals(String.Empty) Then
                .SecurityContainer = companySelected.SecurityContainer
            End If
        End With
    End Sub

    Private Sub DoSomething(Of T1)(a As Action(Of T1), p As T1)
        If InvokeRequired Then
            Me.BeginInvoke(Sub()
                               a.Invoke(p)
                           End Sub)
        Else
            a.Invoke(p)
        End If
    End Sub

    Private Sub MostrarNotificacion(ByVal _mensaje As String)
        Dim frm As New Presentation.Controls.FrmNotificationItemDetail
        frm.TxtMessage.Text = _mensaje
        Using transparent As New FrmTransparent(frm, False)
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Click en Login
    ''' </summary>
    ''' <param name="User"></param>
    ''' <param name="Password"></param>
    ''' <remarks></remarks>
    Private Async Sub BtnLogin_Click(ByVal User As String, ByVal Password As String)
        If companyVersion Is Nothing Then
            If Not updaterBackground.IsBusy Then
                Me.Cursor = BaseClass.ChangeCursorIndigo
                LoadingWpf(True, "Buscando nuevas versiones...")

                IsLogin = True
                IsUpdating = True
                updaterBackground.RunWorkerAsync()
            End If
        End If

        'Si se esta validando la versión o se esta actualizando 
        If updaterBackground.IsBusy OrElse IsUpdating Then
            Exit Sub
        End If

        Me.Cursor = BaseClass.ChangeCursorIndigo
        LoadingWpf(True, "Validando usuario...")
        Try
            'Validamos campos para el proceso de autenticacion
            If User.Trim().Equals(String.Empty) OrElse Password.Trim().Equals(String.Empty) Then
                MessageIndigo.Show(ResourceManager.GetString("FieldEmpty"), MessageType.Warning, Me.Text.Trim(), Botones.Ok)
            Else
                If INDglCompanies.Properties.DataSource Is Nothing Then
                    'Listamos las empresas
                    INDglCompanies.Properties.DataSource = Await taskGetCompanies
                End If

                'Si no han seleccionado una empresa le trato de asignar la que viene por defecto
                If SessionValues.Instance.IndigoCompany Is Nothing Then
                    Dim varSession As SessionValues = SessionValues.Instance
                    Dim DefaultCompany As Company = CType(INDglCompanies.Properties.DataSource, List(Of Company)).Find(Function(x) x.Code.Equals(varSession.IndigoContainer.ToString))
                    If DefaultCompany IsNot Nothing Then
                        SetERPSingletonValues(DefaultCompany)
                    End If
                End If
                Dim result As ActionResult(Of UserLogin) = Nothing
                Using model As New MLogin
                    If SessionValues.Instance.IndigoCompany Is Nothing OrElse SessionValues.Instance.IndigoCompany.Trim().Equals(String.Empty) Then
                        MessageBox.Show("El contenedor " & ConfigurationFile.Instance.DefaultCompanyContainerCode & " no existe")
                        LoadingWpf(False, "")
                        Me.Cursor = Cursors.Default
                        Return
                    End If
                    If appVersion Is Nothing Then
                        appVersion = BaseClass.GetAppVersion()
                    End If
                    result = Await model.LoginUserCompany(User.Trim(), Password.Trim(), SessionValues.Instance.IndigoCompany, appVersion)
                    If result.StateResult Then
                        Dim usr As UserLogin = result.ObjectEmbbeded
                        'NO ES NECESARIO SE TOMA DEL ARCHIVO DE CONFIGURACION
                        'If SessionValues.Instance.ServiceConfigurationId = 0 Then
                        '    DoSomething(Of String)(AddressOf MostrarNotificacion, "No se encontró la configuración de servicios de la compañía seleccionada. Por favor comuníquese con el administrador del sistema.")
                        '    Return
                        'End If
                        'Dim sc = model.GetServiceConfiguration(SessionValues.Instance.ServiceConfigurationId)
                        'If sc Is Nothing Then
                        '    DoSomething(Of String)(AddressOf MostrarNotificacion, "No se encontró la configuración de servicios de la compañía seleccionada. Por favor comuníquese con el administrador del sistema.")
                        '    Return
                        'Else
                        '    ConfigurationFile.Instance.SetServiceConfiguration(sc)
                        'End If
                        Dim _userConfiguration = Await model.GetUserConfigurationByUserId(usr.Id)
                        If _userConfiguration Is Nothing OrElse _userConfiguration.Id = 0 Then
                            _userConfiguration = New UserConfiguration()
                            _userConfiguration.ActualCity = ConfigurationFile.Instance.City
                            _userConfiguration.CustomReportPath = ConfigurationFile.Instance.ReportsPath
                            _userConfiguration.LanguageCulture = ConfigurationFile.Instance.Culture.Name
                            _userConfiguration.LightweightVersion = ConfigurationFile.Instance.LightweightVersion
                            _userConfiguration.ReporteadorActivo = True
                            _userConfiguration.ShowThemeSkinSelector = True
                            _userConfiguration.UserId = usr.Id
                            _userConfiguration.TypeAlertControl = 1 'Alert Windows
                            _userConfiguration.ChangeTracker.State = ObjectState.Added
                            _userConfiguration.ReportPathType = 1
                        Else
                            _userConfiguration.ChangeTracker.State = ObjectState.Modified
                        End If
                        _userConfiguration.Containers = New Containers With {.Id = SessionValues.Instance.IndigoContainerId, .Code = SessionValues.Instance.IndigoCompany,
                            .Name = SessionValues.Instance.IndigoCompanyName, .CompanyType = SessionValues.Instance.IndigoCompanyType}
                        Dim objContainer As Company = CType(INDglCompanies.Properties.DataSource, List(Of Company)).Find(Function(x) x.Code.Equals(SessionValues.Instance.IndigoCompany.ToString))

                        SessionValues.Instance.ArchitectureType = objContainer.ArchitectureType
                        SessionValues.Instance.ProductionCompany = objContainer.ProductionCompany
                        SessionValues.Instance.IdTimeZone = _userConfiguration.IdTimezone
                        SessionValues.Instance.dateFormat = _userConfiguration.DateFormat
                        SessionValues.Instance.timeFormat = _userConfiguration.TimeFormat
                        SessionValues.Instance.LanguageCulture = _userConfiguration.LanguageCulture

                        ValoresSesion.IdTimeZone = _userConfiguration.IdTimezone
                        ValoresSesion.dateFormat = _userConfiguration.DateFormat
                        ValoresSesion.timeFormat = _userConfiguration.TimeFormat
                        ValoresSesion.dateFormatValue = Infrastructure.CrossCutting.Base.Utils.GetCustomDateFormat(_userConfiguration.DateFormat)
                        ValoresSesion.timeFormatValue = Infrastructure.CrossCutting.Base.Utils.GetCustomTimeFormat(_userConfiguration.TimeFormat)

                        'Asignar al hilo el formato de fecha
                        Dim dateFormatCulture = System.Globalization.CultureInfo.CurrentCulture
                        If ValoresSesion.dateFormat > 0 Then
                            dateFormatCulture.DateTimeFormat.LongTimePattern = ValoresSesion.timeFormatValue
                            dateFormatCulture.DateTimeFormat.LongDatePattern = ValoresSesion.dateFormatValue
                            dateFormatCulture.DateTimeFormat.ShortTimePattern = ValoresSesion.timeFormatValue
                            dateFormatCulture.DateTimeFormat.ShortDatePattern = ValoresSesion.dateFormatValue
                        End If

                        Dim endpoints = Await model.GetEndPointsByIdContainer(objContainer.Id)
                        SessionValues.Instance.EndPoints = endpoints

                        'NO ES NECESARIO SE TOMA DEL ARCHIVO DE CONFIGURACION
                        'ConfigurationFile.Instance.SetUserConfiguration(_userConfiguration) 
                        ConfigurationFile.Instance.TypeAlertControl = _userConfiguration.TypeAlertControl
                        ConfigurationFile.Instance.ReportPathType = _userConfiguration.ReportPathType
                        indigoApplicationSetting.SetUserConfiguration(_userConfiguration)
                        _userConfiguration.Containers = Nothing
                        _userConfiguration.DefaultCompany = SessionValues.Instance.IndigoContainerId
                        Await model.SaveUserConfiguration(_userConfiguration)
                        Dim appSet = Await model.GetApplicationSettingsByContainerId(SessionValues.Instance.IndigoContainerId)
                        indigoApplicationSetting.SetApplicationSetting(appSet)
                        model.IndigoConectaReset()
                        Me.InitSesionValues(usr)

                        FrmLoginWPF1.StopMedia()

                        'RaiseEvent AutenticateHIS(User.Trim()) 'se llama al cargar unidades operacionales
                        RaiseEvent AssingCompanies(Await taskGetCompanies, usr.ListCompanyPermissionCode)
                        RaiseEvent LoginOk()
                    Else
                        Dim frm As New Presentation.Controls.FrmNotificationItemDetail()
                        frm.TxtMessage.Text = result.Message
                        Using transparent As New FrmTransparent(frm, False)
                            transparent.ShowDialog(Me)
                        End Using
                    End If
                End Using
            End If
        Catch ex As Exception
            Throw ex
        Finally
            LoadingWpf(False, "")
            Me.Cursor = Cursors.Default
        End Try
    End Sub

#End Region

#Region "Procesos Asyncronos"

    Private Sub updaterBackground_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs)
        'devuelvo el resulatdo de la funcion
        e.Result = CompareVersion()
    End Sub

    Private Sub updaterBackground_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs)
        LoadingWpf(False, "")
        Me.Cursor = Cursors.Default

        If e.Error IsNot Nothing Then
            IsUpdating = False
            MessageBox.Show(Infrastructure.CrossCutting.Base.Utils.GetInnerExceptionMessageToString(e.Error))
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
            FrmLoginWPF1.Click_login(Nothing, Nothing)
        End If
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
                If InvokeRequired Then
                    Me.BeginInvoke(Sub()
                                       Using tras As New FrmTransparent(New FrmConfigurarConexion(), False)
                                           If tras.ShowDialog(Me) <> System.Windows.Forms.DialogResult.OK Then
                                               MessageIndigo.Show("Sin este archivo no se puede continuar", MessageType.Errores, Me.Text, Botones.Ok)
                                               If LoaderConfigurationFile.ConfigurationFileExists() Then
                                                   File.Delete(LoaderConfigurationFile.GetPathConfigurationFile())
                                               End If
                                               End
                                           End If
                                       End Using
                                   End Sub)
                Else
                    Using tras As New FrmTransparent(New FrmConfigurarConexion(), False)
                        If tras.ShowDialog(Me) <> System.Windows.Forms.DialogResult.OK Then
                            MessageIndigo.Show("Sin este archivo no se puede continuar", MessageType.Errores, Me.Text, Botones.Ok)
                            If LoaderConfigurationFile.ConfigurationFileExists() Then
                                File.Delete(LoaderConfigurationFile.GetPathConfigurationFile())
                            End If
                            End
                        End If
                    End Using
                End If
            Else
                MessageIndigo.Show("Sin este archivo no se puede continuar", MessageType.Errores, Me.Text, Botones.Ok)
                End
            End If
        End If
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
        ValoresSesion.RutaArchivosPorUsuario = String.Concat(Window.Utils.UserFolder(), "\HealtTech\Indigo Vie EHR")
        ValoresSesion.VersionDGH = IndigoValoresSesion.eVersionDGH.Nativo
        ValoresSesion.VersionIndigoCrystal = "1.0.0.0"
        ValoresSesion.UrlWebServicesNoti = APPRutaNotificacion
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
        Indigo.ListProductCatalog = usr.ListProductCatalog
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
        TokenManager.Instance.SetToken(usr.AccessToken)
        SessionValues.Instance.UserType = CType(usr.UserType, UserType)
        SessionValues.Instance.ProfileType = CType(usr.ProfileType, eProfileType)
        IndigoValoresSesion.Instancia.JwtToken = TokenManager.Instance.GetToken()
    End Sub

    ''' <summary>
    ''' Validar la versión con respecto a la versión de la ruta de actualización
    ''' </summary>
    ''' <returns></returns>
    Private Async Function CompareVersion() As Task(Of Boolean)
        Dim pathUpdate As String = Me.GetPathUpdate()

        If INDglCompanies.Properties.View.GetFocusedRow() IsNot Nothing Then
            companyVersion = New Version(CType(INDglCompanies.Properties.View.GetFocusedRow(), Company).Version)
        Else
            If INDglCompanies.Properties.DataSource Is Nothing Then
                If taskGetCompanies IsNot Nothing AndAlso taskGetCompanies.Status = TaskStatus.Faulted Then
                    taskGetCompanies = GetCompaniesAsync()
                End If
                INDglCompanies.Properties.DataSource = Await taskGetCompanies
            End If

            Dim currentCompany = CType(INDglCompanies.Properties.DataSource, List(Of Company)).Find(Function(o) o.Code = SessionValues.Instance.IndigoContainer)
            If currentCompany IsNot Nothing Then
                companyVersion = New Version(currentCompany.Version)
            End If
        End If

#If DEBUG Then
        appVersion = companyVersion
#End If

        If Not String.IsNullOrEmpty(pathUpdate) Then
            If companyVersion IsNot Nothing AndAlso Not companyVersion.Equals(appVersion) Then
                Dim clientToUpdate = pathUpdate & "\" & My.Application.Info.AssemblyName & ".exe"
                If File.Exists(clientToUpdate) Then
                    Dim clientToUpdateVersion = Version.Parse(FileVersionInfo.GetVersionInfo(clientToUpdate).FileVersion)
                    If companyVersion.Equals(clientToUpdateVersion) Then
                        'necesita actualizarse
                        Return True
                    End If
                End If
            Else
                companyVersion = New Version
            End If
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
        ejecutar = String.Concat("""", pathExecutable, """ """, pathUpdate, """ ""Indigo Vie ERP.exe""")

        Dim myProcess As New Process()
        ' Establece valores en las propiedades FileInfo
        myProcess.StartInfo.UseShellExecute = False
        myProcess.StartInfo.FileName = ejecutar
        myProcess.StartInfo.CreateNoWindow = True
        'Lanzando proceso
        myProcess.Start()
    End Sub

    Private Function GetPathUpdate() As String
        Dim pathUpdate As String = ConfigurationFile.Instance.PathUpdates
        If String.IsNullOrEmpty(pathUpdate) Then
            pathUpdate = ValoresSesion.RutaActualizacion
        End If
        Return pathUpdate
    End Function

#End Region

    ''' <summary>
    ''' Bandera para determinar si se han cargado los valores de aplicacion
    ''' </summary>
    ''' <remarks></remarks>
    Dim FlagLoadValuesApp As Boolean


    ''' <summary>
    ''' Metodo para cambiar de empresa
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDglCompanies_EditValueChanged(sender As Object, e As EventArgs) Handles INDglCompanies.EditValueChanged
        SetERPSingletonValues(CType(INDglCompanies.GetSelectedDataRow, Company))
    End Sub


    ''' <summary>
    ''' Metodo cuando termina de cargar la imagen
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadImage()
        Me.WindowState = FormWindowState.Normal
        Presentation.Base.BaseClass.ActiveSplashScreen(False)
        BaseClass.HideMDI(True)
        Me.Focus()
    End Sub

    ''' <summary>
    ''' Metodo para ubicar el gle de empreass
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PositionControl()
        INDglCompanies.Location = New Point(CInt(FrmLoginWPF1.PositionGridLookUp.X), CInt(FrmLoginWPF1.PositionGridLookUp.Y))
        INDglCompanies.Visible = True
    End Sub


    ''' <summary>
    ''' Metodo para poner el control del login en cargando y ocultar los botones
    ''' </summary>
    ''' <param name="Value"></param>
    ''' <remarks></remarks>
    Public Sub LoadingWpf(ByVal Value As Boolean, Optional Text As String = "")
        If Me.InvokeRequired = True Then
            Me.BeginInvoke(Sub()
                               FrmLoginWPF1.Load(Value, Text)
                           End Sub)
        Else
            FrmLoginWPF1.Load(Value, Text)
        End If
    End Sub

    Private Sub FrmWpfLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If _flagLockedSession Then
            FrmLoginWPF1._flagLockedSession = _flagLockedSession
            FrmLoginWPF1.User = _UserCode
            FrmLoginWPF1.Load(False, "")
            _flagLockedSession = False
        End If
    End Sub

    ''' <summary>
    ''' Retorna el host y la networkIp del cliente
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetHostAndNetworkIP() As (HostName As String, NetworkIp As String)
        Dim hostName As String = ""
        Dim networkIp As String = ""

        Try
            hostName = Dns.GetHostName()
            Dim addresses() As IPAddress = Dns.GetHostAddresses(hostName)
            networkIp = addresses.FirstOrDefault(Function(ip) ip.AddressFamily = AddressFamily.InterNetwork AndAlso Not IPAddress.IsLoopback(ip))?.ToString()
            If String.IsNullOrEmpty(networkIp) Then networkIp = ""
        Catch ex As Exception
            hostName = "Error: " & ex.Message
            networkIp = "Error: " & ex.Message
        End Try

        Return (hostName, networkIp)
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overrides Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then

            End If
            FrmLoginWPF1 = Nothing
            taskGetCompanies = Nothing
            taskLoadSettingsHIS = Nothing
            ValoresSesion = Nothing
        End If
        Me.disposedValue = True
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class

