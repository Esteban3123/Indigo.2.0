#Region "Imports"

Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports System.Text
Imports System.Threading
Imports DevExpress.LookAndFeel
Imports DevExpress.XtraBars.Alerter
Imports DevExpress.XtraBars.Helpers
Imports DevExpress.XtraBars.Ribbon
Imports DevExpress.XtraBars.ToastNotifications
Imports DevExpress.XtraEditors
Imports DevExpress.XtraLayout.Utils
Imports DevExpress.XtraSplashScreen
Imports Domain.Security.Entities
Imports Infrastructure.Base
Imports Infrastructure.Base.Security
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Base.Window
Imports Microsoft.Toolkit.Uwp.Notifications
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Client.MVP
Imports Presentation.Common
Imports Presentation.Controls
Imports Presentation.Inventory
Imports Presentation.Resources
Imports Presentation.Security
Imports Presentation.Security.MVP
#End Region

Public Class FormMdi
    Implements Window.IObservador, IOpenForm

#Region "Fields"
    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim presenter As PmdiPrincipal
    Private myFavorites As List(Of VieDBForm)
    Private taskFormPermission As Task
    ''' <summary>
    ''' Tarea la cual se encarga de consumir un metodo que autentica al usuario del lado del HIS
    ''' </summary>
    ''' <remarks></remarks>
    Private taskAutenticationHis As Task
    Private taskMyFavorites As Task
    Private taskListOperatingUnit As Task
    Private TaskloadListForms As Task
    Private listCompanyPermission As List(Of Company)
    Friend WithEvents CtrNotificationCenter As Presentation.Controls.CtrNotificationCenter
    Friend WithEvents CtrSearching As Presentation.Controls.CtrSearching
    ''' <summary>
    ''' Bandera para controlar el cambio de tema
    ''' </summary>
    Public flagThemeChanged As Boolean
    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Dim indigo As Infrastructure.CrossCutting.Base.SessionValues

    Dim FolderAccesible As Boolean = True
    Friend _tokenId As String
    Dim indigoApplicationSetting As Infrastructure.CrossCutting.Base.ApplicationSetting
    Private IndigoContainerId As Integer

    Private HisContainer As String

    Private IndigoCompany As String

    Private TransactionalContainer As String

    Private ServiceConfigurationId As Byte

    Private SecurityContainer As String

    Private Cargando As Boolean

    Private Login As Boolean

    Private validatedSuscription As Boolean

    ''' <summary>
    ''' Entidad configuracion del usuario cultura
    ''' </summary>
    Private UserConfigurationCulture As New UserConfigurationCulture

    ''' <summary>
    ''' Evento que se dispara despues de que se a cambiado cultira
    ''' </summary>
    ''' <remarks></remarks>
    Public Event ChangePassword(context As FrmCambiarContrasena)

    ''' <summary>
    ''' Propiedad que obtiene o establece el idioma de la aplicacion.
    ''' </summary>
    ''' <value></value>
    Public Property idioma As String
        Get
            If INDCbeCulture.EditValue Is Nothing Then
                Return String.Empty
            Else
                Return INDCbeCulture.EditValue.ToString
            End If
        End Get
        Set(ByVal value As String)
            INDCbeCulture.EditValue = value
        End Set
    End Property
#End Region

#Region "Builders"
    Private _alertControl As CustomAlertControl

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        InicializarAlertControl()
        Me.indigo = SessionValues.Instance
        presenter = New PmdiPrincipal
        If Window.LoaderConfigurationFile.ConfigurationFileExists() Then
            'modelo.LoadGeneralConfiguration() 'NO SE REQUIERE
            ApplicationSetting.Instance.SetGeneralConfiguration(Nothing)
        Else
            TokenManager.Instance.SetFlagValidateToken(False)
            'utiliza el servicio de seguridad default
            presenter.LoadGeneralConfiguration()
            TokenManager.Instance.ClearToken()
        End If
        Me.indigoApplicationSetting = ApplicationSetting.Instance
        'TODO:HRR DESACTIVAR
        Me.indigoApplicationSetting.ZEFLicenseName = "5699;101-indigo.tech"
        Me.indigoApplicationSetting.ZEFLicenseKey = New Guid("{8f8224e0-03b7-c6d0-5877-84cb236c0b33}")

        If Me.indigoApplicationSetting.ZEFLicenseKey Is Nothing Then
            Throw New Exception("Configure la clave de la licencia de Z Entity Framework")
        End If
        Z.BulkOperations.LicenseManager.AddLicense(Me.indigoApplicationSetting.ZEFLicenseName, Me.indigoApplicationSetting.ZEFLicenseKey.ToString)
        Z.EntityFramework.Extensions.LicenseManager.AddLicense(Me.indigoApplicationSetting.ZEFLicenseName, Me.indigoApplicationSetting.ZEFLicenseKey.ToString)

        'Dim loginAzure As String = ConfigurationManager.AppSettings("LoginAzure")
        'If String.IsNullOrEmpty(loginAzure) OrElse loginAzure = "0" Then
        LogoutManager.OnLogout = AddressOf Me.LogoutUser

        If Window.LoaderConfigurationFile.ConfigurationFileExists() Then
            OpenLoginFrm()
        Else
            Me.LoadNotificationCenter()
            Using Mensaje As New Mensajes
                Mensaje.Registrar(Me)
            End Using
            OpenLoginFrmPass(False)
        End If
        LoadCultureUser()
    End Sub

    Private Sub InicializarAlertControl()
        Dim _color As Color
        Select Case DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName
            Case "The Bezier", "Office 2019 Dark Gray", "Black", "Office 2007 Black", "Pumpkin", "Office 2019 Colorful"
                _color = Color.White
            Case Else
                _color = DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinMaskColor
        End Select
        _alertControl = New CustomAlertControl()
        AddHandler _alertControl.BeforeFormShow, AddressOf INDAlertControl_BeforeFormShow
        AddHandler _alertControl.AlertClick, AddressOf INDAlertControl_AlertClick
        _alertControl.AppearanceText.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        _alertControl.AppearanceText.ForeColor = _color 'Color.White
        _alertControl.FormLocation = AlertFormLocation.BottomLeft
        _alertControl.AutoFormDelay = 5000
        _alertControl.AppearanceHotTrackedText.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        _alertControl.FormShowingEffect = AlertFormShowingEffect.SlideHorizontal
    End Sub

    Public Sub InitionMDIValues()
        'Token Firma Electronica
        Dim userToken = Task.Factory.StartNew(Function() As Guid
                                                  Return GetUserElectronicSignatureToken(indigo.UserIndigoId)
                                              End Function
                                                  )
        If ApplicationSetting.Instance.LoginAzure Then
            INDLciTenant.Visibility = LayoutVisibility.Always
            INDLciCompany.Visibility = LayoutVisibility.Always
            INDTeTenant.Text = UnifiedConfiguration.Instance.CompanySelected.TenantName
            INDTeCompany.Text = UnifiedConfiguration.Instance.CompanySelected.Name
            LayoutControlItem24.Visibility = LayoutVisibility.Never
            INDPceChangePerfil.Visible = True
        Else
            INDLciTenant.Visibility = LayoutVisibility.Never
            INDLciCompany.Visibility = LayoutVisibility.Never
            LayoutControlItem24.Visibility = LayoutVisibility.Always
            INDPceChangePerfil.Visible = False
        End If

        If Me.indigo.IndigoCompanyType = 3 Then
            BaseClass.ListFormHide = New List(Of Integer)({322, 344, 342, 341, 303, 340, 343, 838, 842, 855, 1516, 1680, 1740, 1762, 1764})
        End If
        VersionAplicacion = Infrastructure.CrossCutting.Base.Utils.GetAppVersion().ToString()
        Dim container = Me.indigo.HisContainer
        LblVersion.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        LblVersion.Properties.ShowPopupCloseButton = False
        LblVersion.Properties.PopupSizeable = False
        LblVersion.Properties.AutoHeight = False
        LblVersion.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        LblVersion.Properties.Buttons.Clear()

        LblContainer.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        LblContainer.Properties.ShowPopupCloseButton = False
        LblContainer.Properties.PopupSizeable = False
        LblContainer.Properties.AutoHeight = False
        LblContainer.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        LblContainer.Properties.Buttons.Clear()

        LblUserName.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        LblUserName.Properties.ShowPopupCloseButton = False
        LblUserName.Properties.PopupSizeable = False
        LblUserName.Properties.AutoHeight = False
        LblUserName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        LblUserName.Properties.Buttons.Clear()
        Using SerializableAppearance As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
            SerializableAppearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
            LblUserName.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, Me.indigo.UserIndigoName, -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearance, Me.indigo.UserIndigoName, Nothing, Nothing, True)})
            LblVersion.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, VersionAplicacion, -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearance, VersionAplicacion, Nothing, Nothing, True)})
            LblContainer.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, container, -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearance, container, Nothing, Nothing, True)})
        End Using
        LblUserName.MaximumSize = New Size(Me.indigo.UserIndigoName.Length * 9, 0)
        LblUserName.MinimumSize = New Size(Me.indigo.UserIndigoName.Length * 9, LblUserName.Size.Height)

        LblVersion.MaximumSize = New Size(VersionAplicacion.Length * 9, 0)
        LblVersion.MinimumSize = New Size(VersionAplicacion.Length * 9, LblVersion.Size.Height)

        LblContainer.MaximumSize = New Size(container.Length * 9, 0)
        LblContainer.MinimumSize = New Size(container.Length * 9, LblContainer.Size.Height)

        Me.INDrbgViewForm.EditValue = indigo.UserViewMode
        BackstageViewCostIntegrated.Visible = Not String.IsNullOrEmpty(indigo.InteropCostContainer)
        BackstageViewCostNative.Visible = String.IsNullOrEmpty(indigo.InteropCostContainer)
        INDsbDashboard.Visible = (indigo.UserType <> UserType.StandardUser)
        INDbtnCrearReportes.Visible = (indigo.UserType <> UserType.StandardUser)
        SkinHelper.InitSkinGallery(Me.PpgThemeSkins)
        BackstageVCPrincipal.SelectedTab = Nothing
        RenderMenuProductModule()
        'Carga de objetos iniciales
        'taskAutenticationHis = AutenticateHISAsync(indigo.UserIndigo)
        TaskloadListForms = presenter.ConsultarTodosForms()
        taskMyFavorites = Me.LoadMyFavoritesAsync()
        taskFormPermission = LoadListMenuAsync()
        INDlblUserWin.Text = Environment.UserName
        INDlblWorkStation.Text = Environment.UserDomainName
        '----------Empresa----------
        INDgleCompanies.EditValue = indigo.IndigoCompany
        '----------Version----------
        INDlblServices.Text = indigo.UriWebServices
        INDlblEntities.Text = indigo.UriWebServicesXpo
        If indigo.ExcecutionType = ExcecutionSessionType.Local Then
            INDlblSessionType.Text = obtenerRecurso(Eresources.ComunesSesionLocal, Eform.Comunes)
        Else
            INDlblSessionType.Text = obtenerRecurso(Eresources.ComunesSesionRemota, Eform.Comunes)
        End If
        '----------Fecha----------
        INDlblFormatDate.Text = Globalization.DateTimeFormatInfo.CurrentInfo.ShortDatePattern
        INDlblLanguage.Text = System.Threading.Thread.CurrentThread.CurrentUICulture.NativeName
        'Aqui validamos el tipo de empresa seleccionado
        If SessionValues.Instance.IndigoCompanyType = 3 Then 'Alcaldias
            Me.BvtAdministracionImpuestos.Visible = True
            Me.BvtVerticalSaludAdministrativo.Visible = False
            Me.BvtVerticalSaludAsistencial.Visible = False
            Me.BvtCalidad.Visible = False
        Else 'IPS
            Me.BvtAdministracionImpuestos.Visible = False
            Me.BvtVerticalSaludAdministrativo.Visible = True
            Me.BvtVerticalSaludAsistencial.Visible = True
            Me.BvtCalidad.Visible = True
        End If
        Me.PceMessages.Visible = False
        If ConfigurationFile.Instance.TypeAlertControl = eTypeAlertControl.ToastNotification Then
            If ((New DesktopBridge.Helpers()).IsRunningAsUwp()) Then
                Me.DisableToastNotifications()
                Me.CheckWindowsNotification()
                If (Not Me.PceMessages.Visible) Then
                    AddHandler ToastNotificationManagerCompat.OnActivated, New OnActivated(AddressOf Me.ToastNotificationManagerCompat_Activated)
                    Return
                End If
            ElseIf (Me.ToastNotificationsManager1 Is Nothing) Then
                ToastNotificationsManager1 = New ToastNotificationsManager(Me.components) With {
                .ApplicationId = "fa2f0c5e-99b9-40a1-a96d-a58e3960b44d"}
                If ToastNotificationsManager1.Notifications.Count = 0 Then
                    ToastNotificationsManager1.Notifications.AddRange(New DevExpress.XtraBars.ToastNotifications.IToastNotificationProperties() {New DevExpress.XtraBars.ToastNotifications.ToastNotification("ce0a6258-78a4-418b-a000-dbcad30dbde8", Nothing, Nothing, Global.Presentation.Client.My.Resources.Resources.formapplicationbutton1, Nothing, Nothing, Nothing, "Vie Cloud Platform - by IndiGO", "", "", "Vie Cloud Platform", DevExpress.XtraBars.ToastNotifications.ToastNotificationSound.[Default], DevExpress.XtraBars.ToastNotifications.ToastNotificationDuration.[Default], Nothing, DevExpress.XtraBars.ToastNotifications.AppLogoCrop.[Default], DevExpress.XtraBars.ToastNotifications.ToastNotificationTemplate.Generic)})
                End If
                RemoveHandler ToastNotificationsManager1.Activated, AddressOf ToastNotificationsManager1_Activated
                AddHandler ToastNotificationsManager1.Activated, AddressOf ToastNotificationsManager1_Activated
                RemoveHandler ToastNotificationsManager1.Failed, AddressOf ToastNotificationsManager1_Failed
                AddHandler ToastNotificationsManager1.Failed, AddressOf ToastNotificationsManager1_Failed
            End If
        Else
            DisableToastNotifications()
        End If
        Task.WaitAll(userToken)
        IndigoSingleton.IndigoValoresSesion.Instancia.User_token = userToken.Result
    End Sub

    ''' <summary>
    ''' Obtiene el token de la firma electronica por id de usuario y lo parsea a Guid.
    ''' </summary>
    ''' <param name="userId"></param>
    ''' <returns></returns>
    Private Function GetUserElectronicSignatureToken(userId As Integer) As Guid
        Using m As New MUsuario
            Dim res = m.GetElectronicSignatureToken(userId)
            If res IsNot Nothing Then
                Dim user As New User With {
                    .ElectronicSignatureToken = res
                    }
                If user.ValidateTokenFormat() Then
                    Dim token As Guid = user.ParseToken()
                    Return token
                End If
            End If
        End Using
        Return Guid.Empty
    End Function

    ''' <summary>
    ''' Realiza el render del menu, en productos y modulos, es requerido la variable  indigo.ListProductCatalog
    ''' </summary>
    Private Sub RenderMenuProductModule()
        If indigo.ListProductCatalog Is Nothing OrElse indigo.ListProductCatalog.Count = 0 Then
            Exit Sub
        End If

        'Limpiar control menu, menos favoritos ya que son fijos
        For Each item In Me.BackstageVCPrincipal.Items.ToList
            If TypeOf item Is BackstageViewTabItem Then
                If item.Name <> "INDBsvtiFavorites" Then
                    Me.BackstageVCPrincipal.Items.Remove(item)
                End If
            End If
            If TypeOf item Is BackstageViewItemSeparator Then
                If item.Name <> "INDBsvisFavorites" Then
                    Me.BackstageVCPrincipal.Items.Remove(item)
                End If
            End If
        Next
        Me.BackstageVCPrincipal.Refresh()

        'BackstageVCPrincipal as BackstageViewControl 'Lista Productos   
        'Productos Control
        Dim BackstageViewClientControl As DevExpress.XtraBars.Ribbon.BackstageViewClientControl
        'Productos Item
        Dim BackstageViewTabItem As DevExpress.XtraBars.Ribbon.BackstageViewTabItem
        'Producto Separador 
        Dim BackstageViewItemSeparator As DevExpress.XtraBars.Ribbon.BackstageViewItemSeparator
        'Lista Modulo
        Dim BackstageViewControl As DevExpress.XtraBars.Ribbon.BackstageViewControl
        'Modulo Control
        Dim BackstageViewClientControlChild As DevExpress.XtraBars.Ribbon.BackstageViewClientControl
        'Modulo Item
        Dim BackstageViewTabItemChild As DevExpress.XtraBars.Ribbon.BackstageViewTabItem
        'For Each item As ProductCatalog In indigo.ListProductCatalog.ToList
        For Each item As ProductCatalog In indigo.ListProductCatalog.Where(Function(x) x.Visible = 1).ToList
            BackstageViewClientControl = New DevExpress.XtraBars.Ribbon.BackstageViewClientControl()
            BackstageViewTabItem = New DevExpress.XtraBars.Ribbon.BackstageViewTabItem()
            BackstageViewControl = New DevExpress.XtraBars.Ribbon.BackstageViewControl()
            BackstageViewItemSeparator = New DevExpress.XtraBars.Ribbon.BackstageViewItemSeparator()

            'Control de producto
            BackstageViewClientControl.Location = New System.Drawing.Point(267, 0)
            BackstageViewClientControl.Name = String.Format("BackstageViewClientControl{0}", item.IdProduct)
            BackstageViewClientControl.Tag = item.IdProduct.ToString()

            'Lista Modulos
            BackstageViewControl.Dock = System.Windows.Forms.DockStyle.Fill
            BackstageViewControl.Location = New System.Drawing.Point(0, 0)
            BackstageViewControl.Name = String.Format("BackstageViewControl{0}", item.IdProduct)

            For Each _module As Modules In item.ListModules.ToList
                BackstageViewClientControlChild = New DevExpress.XtraBars.Ribbon.BackstageViewClientControl()
                BackstageViewTabItemChild = New DevExpress.XtraBars.Ribbon.BackstageViewTabItem()
                'Control Modulo
                BackstageViewClientControlChild.Location = New System.Drawing.Point(190, 0)
                BackstageViewClientControlChild.Name = String.Format("BackstageViewClientControlChild{0}", _module.IdModule)
                'Item Modulo
                BackstageViewTabItemChild.Caption = _module.ModuleName
                BackstageViewTabItemChild.ContentControl = BackstageViewClientControlChild
                BackstageViewTabItemChild.Name = String.Format("BackstageViewTabItemChild{0}", _module.IdModule)
                BackstageViewTabItemChild.Tag = _module.IdModule
                AddHandler BackstageViewTabItemChild.SelectedChanged, AddressOf BackstageVItem_ItemPressed

                BackstageViewControl.Items.Add(BackstageViewTabItemChild)
            Next
            'Add lista modulos a Control de producto
            BackstageViewClientControl.Controls.Add(BackstageViewControl)

            'Item Product
            BackstageViewTabItem.Caption = item.ProductName
            BackstageViewTabItem.ContentControl = BackstageViewClientControl
            BackstageViewTabItem.Name = String.Format("BackstageViewsvti{0}", item.IdProduct)
            BackstageViewTabItem.Tag = item.IdProduct.ToString()
            BackstageViewTabItem.Visible = True
            'Separador
            BackstageViewItemSeparator.Name = String.Format("BackstageViewsvis{0}", item.IdProduct)

            Me.BackstageVCPrincipal.Items.Add(BackstageViewTabItem)
            Me.BackstageVCPrincipal.Items.Add(BackstageViewItemSeparator)
        Next
        Me.BackstageVCPrincipal.Refresh()
    End Sub

    Private Sub DisableToastNotifications()
        PceMessages.Visible = True
        If ToastNotificationsManager1 IsNot Nothing Then
            RemoveHandler ToastNotificationsManager1.Activated, AddressOf ToastNotificationsManager1_Activated
            RemoveHandler ToastNotificationsManager1.Failed, AddressOf ToastNotificationsManager1_Failed
            ToastNotificationsManager1.Notifications.Clear()
            ToastNotificationsManager1 = Nothing
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="args"></param>
    Private Sub MostrarDashboard(sender As Object, args As EventArgs)
        If Login Then

            If ApplicationSetting.Instance.LoginAzure AndAlso ConfigurationFile.Instance.Dashboard.HasValue AndAlso Not String.IsNullOrEmpty(ConfigurationFile.Instance.CenterAttention) Then
                Dim idForm As Integer = 0
                Select Case ConfigurationFile.Instance.Dashboard.GetValueOrDefault
                    Case IndigoSingleton.IndigoValoresSesion.EIndigoTipoDashboardDefault.DashBoard_Medico '"Especialistas
                        Select Case ConfigurationFile.Instance.SideFace.GetValueOrDefault
                            Case 0
                                idForm = 1534 'Medicos
                                Exit Select
                            Case 1 '"Especialistas
                                idForm = 1572
                                Exit Select
                            Case 10
                                idForm = 1568 'Internos
                                Exit Select
                        End Select
                        Exit Select
                    Case IndigoSingleton.IndigoValoresSesion.EIndigoTipoDashboardDefault.DashBoard_Enfermeria '"frmHCDashBoardEnfermeria"
                        idForm = 1571
                        Exit Select
                    Case IndigoSingleton.IndigoValoresSesion.EIndigoTipoDashboardDefault.DashBoard_Terapias '"frmHCDashboardTerapias"
                        idForm = 1580
                        Exit Select
                    Case IndigoSingleton.IndigoValoresSesion.EIndigoTipoDashboardDefault.DashBoard_Laboratorio '"frmLISDashBoardLaboratorio"
                        idForm = 1576
                        Select Case ConfigurationFile.Instance.SideFace.GetValueOrDefault
                            Case 0
                                idForm = 1576 'laboratorio
                                Exit Select
                            Case 1 '"HemoComponentes
                                idForm = 1898
                                Exit Select
                        End Select
                        Exit Select
                    Case IndigoSingleton.IndigoValoresSesion.EIndigoTipoDashboardDefault.DashBoard_Imagenologia '"frmRISDashBoardImagenologia"
                        idForm = 2678
                        Exit Select
                    Case IndigoSingleton.IndigoValoresSesion.EIndigoTipoDashboardDefault.DashBoard_Patologias '"frmPATDashBoardPatologias"
                        idForm = 1993
                        Exit Select
                    Case IndigoSingleton.IndigoValoresSesion.EIndigoTipoDashboardDefault.DashBoard_ServiciosApoyo '"frmHCDashboardServiciosApoyo"
                        idForm = 1579
                        Exit Select
                    Case IndigoSingleton.IndigoValoresSesion.EIndigoTipoDashboardDefault.DashBoard_MedicoInternos '"frmHCDashBoardInternos"
                        idForm = 1568
                        Exit Select
                End Select
                If idForm > 0 Then
                    OpenForm(idForm, Nothing, False, False)
                End If
            End If

            Login = False
        End If
    End Sub

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Agrega un formulario a mis favoritos
    ''' </summary>
    ''' <param name="form">Formulario a agregar</param>
    Public Async Sub AddToMyFavorites(ByVal grid As DevExpress.XtraGrid.GridControl, ByVal form As VieDBForm)
        If Not Me.myFavorites.Any(Function(f) f.IdForm = form.IdForm) Then
            Me.myFavorites.Add(form)
            grid.RefreshDataSource()
            Me.SafeInvoke(Sub(m) m.INDBsvtiFavorites.Visible = True)
            Me.SafeInvoke(Sub(m) m.INDBsvisFavorites.Visible = True)
            Await Me.SaveMyFavoritesAsync()
        End If
    End Sub

    ''' <summary>
    ''' Elimina un formulario de mis favoritos
    ''' </summary>
    ''' <param name="form">Formulario a eliminar</param>
    Public Async Sub DeleteFromMyFavorites(ByVal grid As DevExpress.XtraGrid.GridControl, ByVal form As VieDBForm)
        If Me.myFavorites.Any(Function(f) f.IdForm = form.IdForm) Then
            Me.myFavorites.Remove(Me.myFavorites.Find(Function(f) f.IdForm = form.IdForm))
            grid.DataSource = Me.myFavorites
            grid.RefreshDataSource()
            If Not Me.myFavorites.Any() Then
                Me.SafeInvoke(Sub(m) m.INDBsvtiFavorites.Visible = False)
                Me.SafeInvoke(Sub(m) m.INDBsvisFavorites.Visible = False)
            End If
            Await Me.SaveMyFavoritesAsync()
        End If
    End Sub

    ''' <summary>
    ''' Carga el menú favoritos del usuario de forma asíncrona
    ''' </summary>
    Private Function LoadMyFavoritesAsync() As Task
        Return Task.Factory.StartNew(AddressOf LoadMyFavorites)
    End Function

    ''' <summary>
    ''' Carga el menú favoritos del usuario
    ''' </summary>
    Private Sub LoadMyFavorites()
        Me.myFavorites = New List(Of VieDBForm)()
        Dim body As String = Window.Utils.GetMyFavorites(indigo.UserIndigo)
        If body IsNot Nothing AndAlso Not body.Trim().Equals(String.Empty) Then
            Try
                Using ms As MemoryStream = New MemoryStream(Encoding.UTF8.GetBytes(body))
                    Dim xs = New System.Xml.Serialization.XmlSerializer(GetType(List(Of VieDBForm)))
                    Me.myFavorites = CType(xs.Deserialize(ms), List(Of VieDBForm))
                    If Me.myFavorites.Count > 0 Then
                        Me.SafeInvoke(Sub(m) m.INDBsvtiFavorites.Visible = True)
                        Me.SafeInvoke(Sub(m) m.INDBsvisFavorites.Visible = True)
                    Else
                        Me.SafeInvoke(Sub(m) m.INDBsvtiFavorites.Visible = False)
                    End If
                End Using
            Catch
                Me.SafeInvoke(Sub(m) m.MostrarMensajeSlide("Ocurrió un error y no se ha podido cargar el menú favoritos del usuario.", MessageType.Warning))
            End Try
        Else
            Me.SafeInvoke(Sub(m) m.INDBsvtiFavorites.Visible = False)
        End If
    End Sub

    ''' <summary>
    ''' Salva el menú favoritos del usuario de forma asíncrona
    ''' </summary>
    Private Function SaveMyFavoritesAsync() As Task
        Return Task.Factory.StartNew(AddressOf SaveMyFavorites)
    End Function

    ''' <summary>
    ''' Salva el menú favoritos del usuario
    ''' </summary>
    Private Sub SaveMyFavorites()
        Using ms As New MemoryStream()
            Dim xs = New System.Xml.Serialization.XmlSerializer(GetType(List(Of VieDBForm)))
            xs.Serialize(ms, Me.myFavorites)
            Window.Utils.SetMyFavorites(indigo.UserIndigo, Encoding.UTF8.GetString(ms.ToArray()))
        End Using
    End Sub

    ''' <summary>
    ''' Metodo para abrir un formulario
    ''' </summary>
    ''' <param name="form"></param>
    ''' <remarks></remarks>
    Private Sub ClickOpenForm(form As VieDBForm)
        OpenForm(form.IdForm)
    End Sub

    ''' <summary>
    ''' Ejecuta en una tarea el metodo de autenticar usuario en el HIS
    ''' </summary>
    ''' <param name="userCode"></param>
    ''' <remarks></remarks>
    Public Sub AutenticateHIS(ByVal userCode As String)
        'TODO:HRR ACTIVAR
#If Not DEBUG Then
        'Realizamos login en en el sistema asistencial
        Using model As New Presentation.Client.MVP.MLogin
            If SessionValues.Instance.IntergrationHisStatus = IntegrationStatus.InProgress OrElse SessionValues.Instance.IntergrationHisStatus = IntegrationStatus.Disconnected Then
                If SessionValues.Instance.HisContainer IsNot Nothing AndAlso Not SessionValues.Instance.HisContainer.Trim().Equals(String.Empty) Then
                    Dim resultSessionHis As HisSessionValues = model.AutenticateUserHis(userCode, SessionValues.Instance.HisContainer.Trim().Replace("INDIGO", ""), SessionValues.Instance.TransactionalContainer)
                    SessionValues.Instance.SessionHis = resultSessionHis
                    If resultSessionHis.IsError Then
                        Infrastructure.CrossCutting.Exceptions.IndigoManagementExceptions.HandleExceptionUI(New Exception(resultSessionHis.ResponseMessage), "UIPolicy")
                    End If
                End If
            Else
                SessionValues.Instance.IntergrationHisStatus = IntegrationStatus.NonIntegrated
            End If
        End Using
#End If
    End Sub

    ''' <summary>
    ''' Abre un formulario por su identificación
    ''' </summary>
    ''' <param name="idForm">Identificación del formulario</param>
    ''' <param name="idEntity">Id de la entidad que se va a consultar en el formulario luego de abrirlo</param>
    ''' <param name="isModal">Valor que indica si el formulario se va a abrir como modal</param>
    Public Sub OpenForm(idForm As String, Optional idEntity As String = Nothing, Optional isModal As Boolean = False, Optional validateVisible As Boolean = True) Implements IOpenForm.OpenForm
        'If SplashScreenManager.Default Is Nothing Then
        '    SplashScreenManager.ShowForm(Me, GetType(wfMain), False, True)
        'End If
        'RibbonControl.HideApplicationButtonContentControl()
        Try

#If Not DEBUG Then
            If FolderAccesible = True Then
                If CompareVersion() = False Then
                    MessageIndigo.Show("Su cliente está desactualizado. Por favor cierre Indigo Vie Cloud Platform y lo reinicia", MessageType.Warning, "Error")
                End If
            End If
#End If

            BaseClass.FreeMemory()
            If Not Login Then
                ValidateLoadOperatingUnit()
            End If
            Me.Cursor = Presentation.Base.BaseClass.ChangeCursorIndigo
            Dim result = (From C In indigo.ListProductCatalog
                          From M In C.ListModules
                          From F In M.ListForm
                          Where F.IdForm = idForm
                          Select New With {.Form = F, .IdModule = M.IdModule}).FirstOrDefault()

            If result Is Nothing Then
                MessageIndigo.Show("El usuario no tiene acceso al formulario", MessageType.Information, obtenerRecurso(Eresources.ComunesIndigoCrystal))
                Return
            End If

            If TokenManager.Instance.IsTokenExpired() Then
                Me.LogoutUser()
                Return
            End If

            Dim form = result.Form
            Dim idModule = result.IdModule

            If form IsNot Nothing Then
                Dim listChildenMdi = Me.MdiChildren.Where(Function(frm) frm IsNot Nothing AndAlso frm.GetType().Name.Equals(form.ClassName)).ToList()
                If listChildenMdi.Count > 0 AndAlso Not isModal Then 'Si ya esta en el MDI entonces lo abro
                    listChildenMdi(0).Activate()
                    'Valido que si es un formBase
                    If TypeOf listChildenMdi(0) Is FormBase Then
                        Dim formBaseOpen = CType(listChildenMdi(0), FormBase)
                        'Valir si envian un idEntity entonces le asignos los valores para que cargue el registro
                        If idEntity IsNot Nothing AndAlso Not idEntity.Trim().Equals(String.Empty) Then
                            formBaseOpen.BarraBotones.ActualizarPermisosBarra(idForm)
                            formBaseOpen.IdEntity = idEntity.Trim()
                            formBaseOpen.OnIdEntityLoaded()
                        End If
                        'Agregamos manejador al evento cuando se eliminen las preferencias
                        AddHandler Me.CustomizationDeleted, AddressOf formBaseOpen.DeleteCustomizationControls
                    End If
                    'HideLoading(listChildenMdi(0))
                Else ' Si no entonces genero una nueva instancia y la abro
                    'Se valida que se haya definido el proyecto y la clase relacionada
                    If form.AssemblyName IsNot Nothing AndAlso form.ClassName IsNot Nothing Then
                        If form.IsNativeForm Then
                            Dim objHandle = Activator.CreateInstance(form.AssemblyName, form.AssemblyName & "." & form.ClassName)
                            Dim formOpen = CType(objHandle.Unwrap(), Form)
                            If TypeOf formOpen Is FormBase Then
                                'Agregamos el Id del módulo al que pertenece el formulario en el Xml
                                CType(formOpen, FormBase).IdModuleSource = idModule
                            End If
                            formOpen.Text = form.FormName
                            AddHandler formOpen.FormClosing, Sub()
                                                                 BaseClass.FreeMemory()
                                                             End Sub
                            'AddHandler formOpen.Shown, Sub()
                            '                               HideLoading(formOpen)
                            '                           End Sub
                            If isModal Then
                                formOpen.StartPosition = FormStartPosition.CenterParent
                                Dim size As System.Drawing.Size
                                size.Width = 1090
                                size.Height = 768
                                formOpen.Size = size
                                formOpen.Owner = Me
                                If idEntity Is Nothing Then
                                    formOpen.Tag = idForm
                                Else
                                    formOpen.Tag = idForm & "__" & idEntity
                                End If
                                AddHandler formOpen.Shown, AddressOf OnLoadFinishIdEntity
                                Using tras As New FrmTransparent(formOpen, False)
                                    tras.ShowDialog(Me)
                                End Using
                            Else
                                formOpen.MdiParent = Me
                                'If ApplicationSetting.Instance.LoginAzure Then
                                formOpen.Tag = idForm
                                'End If
                                formOpen.Show()
                                'Valido que si es un formBase
                                If TypeOf formOpen Is FormBase Then
                                    Dim formBaseOpen = CType(formOpen, FormBase)
                                    'Valir si envian un idEntity entonces le asignos los valores para que cargue el registro
                                    If idEntity IsNot Nothing AndAlso Not idEntity.Trim().Equals(String.Empty) Then
                                        formBaseOpen.BarraBotones.ActualizarPermisosBarra(idForm)
                                        formBaseOpen.IdEntity = idEntity.Trim()
                                        formBaseOpen.OnIdEntityLoaded()
                                    End If
                                    'AddHandler formOpen.Shown, Sub()
                                    '                               HideLoading(formBaseOpen)
                                    '                           End Sub
                                    'Agregamos manejador al evento cuando se eliminen las preferencias
                                    AddHandler Me.CustomizationDeleted, AddressOf formBaseOpen.DeleteCustomizationControls
                                End If
                            End If
                        Else 'Si son integrados con el _HIS
                            'Await taskAutenticationHis
                            Dim objForm = GetInstanceHisSystem(form.AssemblyName, form.ClassName)
                            If objForm IsNot Nothing Then
                                Dim formOpen = CType(objForm, Form)
                                'If ApplicationSetting.Instance.LoginAzure Then
                                formOpen.Tag = idForm
                                'End If
                                Dim entro As Boolean
                                Select Case form.ClassName
                                    Case "frmHCDashboardEspecialistas"
                                        ValidarAbrirDashBoard(formOpen, IndigoSingleton.IndigoValoresSesion.EIndigoTipoDashboardDefault.DashBoard_Medico, CByte(1), entro)
                                        Exit Select
                                    Case "frmHCDashBoardMedicos"
                                        ValidarAbrirDashBoard(formOpen, IndigoSingleton.IndigoValoresSesion.EIndigoTipoDashboardDefault.DashBoard_Medico, CByte(0), entro)

                                        Exit Select
                                    Case "frmHCDashBoardEnfermeria"
                                        ValidarAbrirDashBoard(formOpen, IndigoSingleton.IndigoValoresSesion.EIndigoTipoDashboardDefault.DashBoard_Enfermeria, CByte(0), entro)
                                        Exit Select
                                    Case "frmHCDashboardTerapias"
                                        ValidarAbrirDashBoard(formOpen, IndigoSingleton.IndigoValoresSesion.EIndigoTipoDashboardDefault.DashBoard_Terapias, CByte(0), entro)
                                        Exit Select
                                    Case "frmLISDashBoardLaboratorio"
                                        ValidarAbrirDashBoard(formOpen, IndigoSingleton.IndigoValoresSesion.EIndigoTipoDashboardDefault.DashBoard_Laboratorio, CByte(0), entro)
                                        Exit Select
                                    Case "frmHCDashboardHemoComponentes"
                                        ValidarAbrirDashBoard(formOpen, IndigoSingleton.IndigoValoresSesion.EIndigoTipoDashboardDefault.DashBoard_Laboratorio, CByte(1), entro)
                                        Exit Select
                                    Case "frmRISDashBoardImagenologia"
                                        ValidarAbrirDashBoard(formOpen, IndigoSingleton.IndigoValoresSesion.EIndigoTipoDashboardDefault.DashBoard_Imagenologia, CByte(0), entro)
                                        Exit Select
                                    Case "frmPATDashBoardPatologias"
                                        ValidarAbrirDashBoard(formOpen, IndigoSingleton.IndigoValoresSesion.EIndigoTipoDashboardDefault.DashBoard_Patologias, CByte(0), entro)
                                        Exit Select
                                    Case "frmHCDashboardServiciosApoyo"
                                        ValidarAbrirDashBoard(formOpen, IndigoSingleton.IndigoValoresSesion.EIndigoTipoDashboardDefault.DashBoard_ServiciosApoyo, CByte(0), entro)
                                        Exit Select
                                    Case "frmHCDashBoardInternos"
                                        ValidarAbrirDashBoard(formOpen, IndigoSingleton.IndigoValoresSesion.EIndigoTipoDashboardDefault.DashBoard_MedicoInternos, CByte(10), entro)
                                        If Not entro Then
                                            ValidarAbrirDashBoard(formOpen, IndigoSingleton.IndigoValoresSesion.EIndigoTipoDashboardDefault.DashBoard_Medico, CByte(10), entro)
                                        End If
                                        Exit Select
                                    Case "frmINVDashboardMaterialAsteosintesiss"
                                        AddHandler CType(formOpen, IndigoInventarios.frmINVDashboardMaterialAsteosintesiss).EventoGenerarOrden, AddressOf EventoGenerarOrdenMaterialOsteosintesis
                                        Exit Select
                                End Select
                                If entro Then
                                    formOpen.WindowState = FormWindowState.Maximized
                                End If

                                formOpen.Text = form.FormName

                                AddHandler formOpen.FormClosing, Sub()
                                                                     BaseClass.FreeMemory()
                                                                 End Sub

                                If isModal Then
                                    formOpen.StartPosition = FormStartPosition.CenterParent
                                    Dim size As System.Drawing.Size
                                    If idForm = 1563 Then 'Archivo de Configuracion de crystal
                                        size.Width = 796
                                        size.Height = 360
                                    Else
                                        size.Width = 1090
                                        size.Height = 768
                                    End If
                                    formOpen.Size = size
                                    formOpen.Owner = Me
                                    Using tras As New FrmTransparent(formOpen, False)
                                        tras.ShowDialog(Me)
                                    End Using
                                Else
                                    formOpen.MdiParent = Me
                                    formOpen.Show()
                                End If
                            End If
                        End If
                    End If
                End If
            Else
                'HideLoading(Nothing)
            End If
        Catch e As Exception
            Throw e
        Finally
            Me.Cursor = Cursors.Default
            RibbonControl.HideApplicationButtonContentControl()
        End Try
    End Sub

    Private Sub ValidarAbrirDashBoard(ByRef obj As Form, ByVal dashboard As IndigoSingleton.IndigoValoresSesion.EIndigoTipoDashboardDefault, ByVal perfil As Byte, ByRef entro As Boolean)
        Dim cf As ConfigurationFile = ConfigurationFile.Instance
        If (If(String.IsNullOrEmpty(cf.CenterAttention), False, cf.Dashboard.HasValue)) Then
            If (If(cf.Dashboard.GetValueOrDefault() <> CByte(dashboard), False, perfil = cf.SideFace.GetValueOrDefault())) Then
                entro = True
                Dim value As Byte = cf.Dashboard.Value
                Dim valueOrDefault As Byte = cf.SideFace.GetValueOrDefault()
                Dim centerAttention As String = cf.CenterAttention
                Dim nameCareCenter As String = cf.NameCareCenter
                Dim functionalUnit As String = cf.FunctionalUnit
                Dim functionalUnitName As String = cf.FunctionalUnitName
                Dim typeFunctionalUnit As Nullable(Of Integer) = cf.TypeFunctionalUnit
                Dim _form As Form = Me.AbrirDashBoard(CType(value, IndigoSingleton.IndigoValoresSesion.EIndigoTipoDashboardDefault), CInt(valueOrDefault), centerAttention, nameCareCenter, functionalUnit, functionalUnitName, typeFunctionalUnit.GetValueOrDefault(), cf.GroupCode, cf.RoleCode)
                If (_form IsNot Nothing) Then
                    _form.Tag = obj.Tag
                    obj = _form
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo para Abrir los diferentes Dashboard's
    ''' </summary>     
    Private Function AbrirDashBoard(ByVal DashboardDefault As IndigoSingleton.IndigoValoresSesion.EIndigoTipoDashboardDefault, ByVal Perfil As Integer, ByVal CentroAtencion As String, ByVal CentroAtencionNombre As String, ByVal UnidadFuncional As String, ByVal UnidadFuncionalNombre As String, ByVal TipoUnidadFuncional As Integer, ByVal Grupo As String, ByVal Rol As String) As System.Windows.Forms.Form
        Dim form As System.Windows.Forms.Form = Nothing
        Select Case DashboardDefault
            Case IndigoSingleton.IndigoValoresSesion.EIndigoTipoDashboardDefault.DashBoard_Medico
                If (Perfil = 1) Then
                    Dim frmHCDashboardEspecialista As IndigoeHistorias.frmHCDashboardEspecialistas = New IndigoeHistorias.frmHCDashboardEspecialistas(IndigoSingleton.IndigoValoresSesion.Instancia.UriWebServices,
                        SessionValues.Instance.HisContainer.Trim.Replace("INDIGO", ""), SessionValues.Instance.UserIndigo, Grupo, Rol,
                        CentroAtencion, UnidadFuncional, IndigoSingleton.IndigoValoresSesion.Instancia.EmpresaDGH, IndigoSingleton.IndigoValoresSesion.Instancia.UsuarioDGH) With
                        {
                            .INDorigen = IndigoeHistorias.clsFuncionesHC.eOrigen.OrigenLogin
                        }
                    frmHCDashboardEspecialista.AbrirDashBoard()
                    form = frmHCDashboardEspecialista
                    Exit Select
                ElseIf (Perfil <> 0) Then
                    Dim frmHCDashBoardInterno As IndigoeHistorias.frmHCDashBoardInternos = New IndigoeHistorias.frmHCDashBoardInternos() With
                        {
                            .INDCentroAtencion = CentroAtencion,
                            .INDCentroAtencionDescripcion = CentroAtencionNombre,
                            .INDUnidadFuncional = UnidadFuncional,
                            .INDTipoUnidadFuncional = TipoUnidadFuncional,
                            .INDorigen = IndigoeHistorias.clsFuncionesHC.eOrigen.OrigenLogin
                        }
                    frmHCDashBoardInterno.AbrirDashBoardAsincrono()
                    frmHCDashBoardInterno.Text = "DashBoard Academicos"
                    form = frmHCDashBoardInterno
                    Exit Select
                Else
                    Dim frmHCDashBoardMedico As IndigoeHistorias.frmHCDashBoardMedicos = New IndigoeHistorias.frmHCDashBoardMedicos() With
                        {
                            .INDorigen = IndigoeHistorias.clsFuncionesHC.eOrigen.OrigenLogin,
                            .INDCentroAtencion = CentroAtencion,
                            .INDUnidadFuncional = UnidadFuncional,
                            .INDCentroAtencionDescripcion = CentroAtencionNombre,
                            .INDUnidadFuncionalDescripcion = UnidadFuncionalNombre,
                            .INDTipoUnidadFuncional = TipoUnidadFuncional
                        }
                    frmHCDashBoardMedico.AbrirDashBoardAsincrono()
                    form = frmHCDashBoardMedico
                    Exit Select
                End If

            Case IndigoSingleton.IndigoValoresSesion.EIndigoTipoDashboardDefault.DashBoard_Enfermeria
                Dim frmHCDashBoardEnfermerium As IndigoeHistorias.frmHCDashBoardEnfermeria = New IndigoeHistorias.frmHCDashBoardEnfermeria() With
                    {
                        .INDorigen = IndigoeHistorias.clsFuncionesHC.eOrigen.OrigenLogin,
                        .INDCentroAtencion = CentroAtencion,
                        .INDUnidadFuncional = UnidadFuncional,
                        .INDCentroAtencionDescripcion = CentroAtencionNombre,
                        .INDUnidadFuncionalDescripcion = UnidadFuncionalNombre,
                        .INDTipoUnidadFuncional = TipoUnidadFuncional
                    }
                form = frmHCDashBoardEnfermerium
                Exit Select
            Case IndigoSingleton.IndigoValoresSesion.EIndigoTipoDashboardDefault.DashBoard_Interconsultas
                form = Nothing
                Exit Select

            Case IndigoSingleton.IndigoValoresSesion.EIndigoTipoDashboardDefault.DashBoard_Terapias
                Dim frmHCDashboardTerapia As IndigoeHistorias.frmHCDashboardTerapias = New IndigoeHistorias.frmHCDashboardTerapias(IndigoSingleton.IndigoValoresSesion.Instancia.UriWebServices,
                    SessionValues.Instance.HisContainer.Trim.Replace("INDIGO", ""), SessionValues.Instance.UserIndigo, Grupo, Rol, CentroAtencion,
                    UnidadFuncional, IndigoSingleton.IndigoValoresSesion.Instancia.EmpresaDGH, IndigoSingleton.IndigoValoresSesion.Instancia.UsuarioDGH) With
                    {
                        .INDorigen = IndigoeHistorias.clsFuncionesHC.eOrigen.OrigenLogin,
                        .Text = "DashBoard Terapia"
                    }
                frmHCDashboardTerapia.AbrirDashBoard()
                form = frmHCDashboardTerapia
                Exit Select

            Case IndigoSingleton.IndigoValoresSesion.EIndigoTipoDashboardDefault.DashBoard_Laboratorio
                If (Perfil <> 1) Then
                    Dim _frmLISDashBoardLaboratorio As IndigoLaboratorio.frmLISDashBoardLaboratorio = New IndigoLaboratorio.frmLISDashBoardLaboratorio() With
                        {
                            .INDCentroAtencion = CentroAtencion,
                            .CodCentrosAtencion = CentroAtencion,
                            .INDIntraHospitalario = True,
                            .INDCentroAtencionDescripcion = CentroAtencionNombre
                        }
                    _frmLISDashBoardLaboratorio.AbrirDashBoardAsincrono()
                    _frmLISDashBoardLaboratorio.Text = "DashBoard Laboratorio"
                    form = _frmLISDashBoardLaboratorio
                    Exit Select
                Else
                    Dim frmHCDashboardHemoComponente As IndigoeHistorias.frmHCDashboardHemoComponentes = New IndigoeHistorias.frmHCDashboardHemoComponentes()
                    frmHCDashboardHemoComponente.ConsultaCentros()
                    frmHCDashboardHemoComponente.INDglCentroAtencion.EditValue = CentroAtencion
                    frmHCDashboardHemoComponente.CargarDashboard()
                    frmHCDashboardHemoComponente.Text = "DashBoard Hemocomponentes"
                    form = frmHCDashboardHemoComponente
                    Exit Select
                End If

            Case IndigoSingleton.IndigoValoresSesion.EIndigoTipoDashboardDefault.DashBoard_Imagenologia
                Dim frmRISDashBoardImagenologium As IndigoRIS.frmRISDashBoardImagenologia = New IndigoRIS.frmRISDashBoardImagenologia() With
                    {
                        .INDCentroAtencion = CentroAtencion,
                        .INDCentroAtencionDescripcion = CentroAtencionNombre,
                        .GrupoDescripcion = UnidadFuncionalNombre,
                        .INDSubgrupo = UnidadFuncional,
                        .Origen = IndigoRIS.frmRISDashBoardImagenologia.eOrigen.Intrahospitalario
                    }
                frmRISDashBoardImagenologium.AbrirDashBoardAsincrono()
                frmRISDashBoardImagenologium.Text = "DashBoard Imagenologia Tecnologo"
                form = frmRISDashBoardImagenologium
                Exit Select

            Case IndigoSingleton.IndigoValoresSesion.EIndigoTipoDashboardDefault.DashBoard_Patologias
                Dim frmPATDashBoardPatologia As IndigoPatologias.frmPATDashBoardPatologias = New IndigoPatologias.frmPATDashBoardPatologias() With
                    {
                        .INDCentroAtencion = CentroAtencion,
                        .INDCentroAtencionDescripcion = CentroAtencionNombre
                    }
                frmPATDashBoardPatologia.Intrahospitalario()
                frmPATDashBoardPatologia.Text = "DashBoard Patologias"
                form = frmPATDashBoardPatologia
                Exit Select

            Case IndigoSingleton.IndigoValoresSesion.EIndigoTipoDashboardDefault.DashBoard_ServiciosApoyo
                Dim _frmHCDashboardServiciosApoyo As IndigoeHistorias.frmHCDashboardServiciosApoyo = New IndigoeHistorias.frmHCDashboardServiciosApoyo() With
                    {
                        ._INDCentroAtencion = CentroAtencion,
                        ._INDCentroAtencionDescripcion = CentroAtencionNombre,
                        ._INDUnidadFuncional = UnidadFuncional,
                        .TipoUnidadFuncional = TipoUnidadFuncional.ToString(),
                        .INDorigen = IndigoeHistorias.clsFuncionesHC.eOrigen.OrigenLogin,
                        .Text = "DashBoard Servicios de Apoyo"
                    }
                _frmHCDashboardServiciosApoyo.AbrirDashBoard()
                form = _frmHCDashboardServiciosApoyo
                Exit Select
            Case IndigoSingleton.IndigoValoresSesion.EIndigoTipoDashboardDefault.DashBoard_MedicoInternos
                Dim Dashboard As IndigoeHistorias.frmHCDashBoardInternos = New IndigoeHistorias.frmHCDashBoardInternos() With
                    {
                        .INDCentroAtencion = CentroAtencion,
                        .INDCentroAtencionDescripcion = CentroAtencionNombre,
                        .INDUnidadFuncional = UnidadFuncional,
                        .INDTipoUnidadFuncional = TipoUnidadFuncional,
                        .INDorigen = IndigoeHistorias.clsFuncionesHC.eOrigen.OrigenLogin
                    }
                Dashboard.AbrirDashBoardAsincrono()
                Dashboard.Text = "DashBoard Academicos"
                form = Dashboard
                Exit Select
            Case Else
        End Select
        Return form
    End Function

    Private Sub HideLoading(frm As Form)
        If SplashScreenManager.Default IsNot Nothing Then
            If frm IsNot Nothing Then
                If SplashScreenManager.FormInPendingState Then
                    SplashScreenManager.CloseForm()
                Else
                    SplashScreenManager.CloseForm(False, 100, frm)
                End If
            Else
                SplashScreenManager.CloseForm()
            End If
        End If
    End Sub

    Private Sub OnLoadFinishIdEntity(sender As Object, e As EventArgs)
        Dim formOpen As Form = CType(sender, Form)
        Dim formData = formOpen.Tag.ToString().Split("__")
        Dim idForm As String
        Dim idEntity As String = ""
        If formData.Count > 1 Then
            idForm = formData(0)
            idEntity = formData(2)
        Else
            idForm = formData(0)
        End If

        formOpen.Tag = idForm
        'Valido que si es un formBase
        If TypeOf formOpen Is FormBase Then
            Dim formBaseOpen = CType(formOpen, FormBase)
            'Valir si envian un idEntity entonces le asignos los valores para que cargue el registro
            If idEntity IsNot Nothing AndAlso Not idEntity.Trim().Equals(String.Empty) Then
                formBaseOpen.BarraBotones.ActualizarPermisosBarra(idForm)
                formBaseOpen.IdEntity = idEntity.Trim()
                formBaseOpen.OnIdEntityLoaded()
            End If
            'Agregamos manejador al evento cuando se eliminen las preferencias
            AddHandler Me.CustomizationDeleted, AddressOf formBaseOpen.DeleteCustomizationControls
        End If
    End Sub

    ''' <summary>
    ''' Ocurre cuando el usuario actual elimina sus preferencias
    ''' </summary>
    Protected Sub OnCustomizationDeleted()
        RaiseEvent CustomizationDeleted()
    End Sub

    ''' <summary>
    ''' Obtiene una instancia de un tipo en el sistema asistencial
    ''' </summary>
    ''' <param name="assemblyName">Nombre del ensamblado donde se encuentra el tipo</param>
    ''' <param name="typeName">Nombre del tipo</param>
    ''' <returns>Instancia del tipo</returns>
    Public Shared Function GetInstanceHisSystem(ByVal assemblyName As String, ByVal typeName As String) As Object
        Try
            'If File.Exists(assemblyName) Then
            'Dim asm As Assembly = Assembly.LoadFrom(assemblyName)
            Dim archivo As String = String.Concat(Window.Utils.GetApplicationPath(), assemblyName)
            If File.Exists(archivo) Then
                Dim asm As Assembly = Assembly.LoadFrom(archivo)
                Dim t As Type = asm.GetTypes().Where(Function(f) f.Name.Equals(typeName)).FirstOrDefault()
                If t IsNot Nothing Then
                    Return Activator.CreateInstance(t)
                End If
            End If
            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Metodo que valida que las unidades operativas ya esten cargadas osea que se haya ejecutado la tarea
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ValidateLoadOperatingUnit()
        If indigo.ListOperatingUnitPermission Is Nothing OrElse indigo.ListOperatingUnitPermission.Count = 0 Then
            'Await taskListOperatingUnit 
            Dim companyPermission = TryCast(INDgleCompanies.GetSelectedDataRow, Company)
            CargarUnidadesOperativas(companyPermission)
        End If
    End Sub

    ''' <summary>
    ''' Funcion para asignar las empresas a las cuales tiene permiso
    ''' </summary>
    ''' <param name="listCompanies">Lista de compañias</param>
    ''' <param name="listCodeCompanyPermission">Lista de codigos de compañias a las cuales el usuario tiene permiso</param>
    ''' <remarks></remarks>
    Public Sub AssingCompanies(listCompanies As List(Of Company), listCodeCompanyPermission As List(Of CompanyPermission))
        Dim _CompanyPermission As CompanyPermission = Nothing
        Cargando = True
        listCompanyPermission = listCompanies.Where(Function(x) listCodeCompanyPermission.Any(Function(y) y.CompanyCode.Equals(x.Code))).ToList
        For Each company In listCompanyPermission
            _CompanyPermission = listCodeCompanyPermission.Where(Function(x) x.CompanyCode = company.Code).SingleOrDefault
            If company.IdOperatingUnitDefault = 0 Then
                company.IdOperatingUnitDefault = _CompanyPermission.IdOperatingUnitDefault
            End If
            If Not ApplicationSetting.Instance.LoginAzure Then
                company.Administrator = _CompanyPermission.Administrator
            End If
        Next
        UnifiedConfiguration.Instance.ListCompanies = listCompanyPermission
        If indigo.UserType = UserType.GlobalAdmin Then
            INDgleCompanies.Properties.DataSource = Me.listCompanyPermission
        ElseIf indigo.UserType = UserType.GlobalQa Then
            INDgleCompanies.Properties.DataSource = Me.listCompanyPermission.Where(Function(_Company) _Company.ProductionCompany = 0).ToList
        Else 'si no se filtra por tenant se debe ajustar el cambio de empresa para cargar y asignar el rol, el grupo, los permisos del usuario en el tenant de la compañia si es de otro tenant
            INDgleCompanies.Properties.DataSource = Me.listCompanyPermission.Where(Function(_Company) _Company.TenantId = indigo.TenantId).ToList
        End If

        INDgleCompanies.EditValue = indigo.IndigoCompany
        taskListOperatingUnit = LoadOperatingUnitAsync(False) 'lo llama el evento INDgleCompanies.EditValueChanged
        Cargando = False
    End Sub

    ''' <summary>
    ''' Realiza la carga de las unidades operativas
    ''' a las cuales tiene permiso el usuario en la empresa seleccionada, de forma asíncrona
    ''' </summary>
    Private Function LoadOperatingUnitAsync(_setSession As Boolean) As Task
        Return Task.Factory.StartNew(Sub()
                                         LoadOperatingUnit(_setSession)
                                     End Sub)
    End Function

    ''' <summary>
    ''' Realiza la carga de las unidades operativas
    ''' a las cuales tiene permiso el usuario en la empresa seleccionada
    ''' </summary>
    Private Sub LoadOperatingUnit(_setSession As Boolean)
        If INDgleCompanies.GetSelectedDataRow IsNot Nothing Then

            Dim companyPermission = TryCast(INDgleCompanies.GetSelectedDataRow, Company)
            If _setSession Then
                SetSessionValues(companyPermission)
            End If

            CargarUnidadesOperativas(companyPermission)
            If Not ApplicationSetting.Instance.LoginAzure Then
                AutenticateHIS(indigo.UserIndigo) 'Cuando se cambiaba de empresa no se actualizaba el contenedor en crystal

            End If
        End If
    End Sub

    Private Sub SetSessionValues(ByVal companyPermission As Company)
        Me.indigo.IndigoContainerId = companyPermission.Id
        Me.indigo.IndigoCompany = companyPermission.Code
        Me.indigo.IndigoCompanyName = companyPermission.Name
        Me.indigo.IndigoCompanyNit = companyPermission.CompanyNit
        Me.indigo.IndigoVerificationDigitNit = companyPermission.VerificationDigitNit
        Me.indigo.IndigoCompanyType = companyPermission.CompanyType
        Me.indigo.IndigoContainer = companyPermission.Code
        Me.indigo.IndigoGlossesIntegration = companyPermission.GlossesIntegration
        Me.indigo.IndigoPayrollIntegration = companyPermission.PayrollIntegration
        Me.indigo.IndigoConnectionString = companyPermission.IndigoConnectionString
        Me.indigo.IndigoCompanyPhoneNumber = companyPermission.Telephone
        Me.indigo.IndigoCompanyAddress = companyPermission.Address
        Me.indigo.DocumentalContainer = companyPermission.DocumentalContainer
        Me.indigo.HisContainer = companyPermission.HISContainer
        Me.indigo.IntergrationHisStatus = CType(companyPermission.HISIntegration, IntegrationStatus)
        Me.indigo.InteropCostContainer = companyPermission.InteropCostContainer
        Me.indigo.SecurityContainer = companyPermission.SecurityContainer
        Me.indigo.TransactionalContainer = companyPermission.TransactionalContainer
        Me.indigo.FoundationalContainer = companyPermission.FoundationalContainer
        Me.indigo.VituelContainer = companyPermission.VituelContainer
        Me.indigo.IndigoHumanTalentIntegration = companyPermission.HumanTalentIntegration
        Me.indigo.IndigoDispensingIntegration = companyPermission.DispensingIntegration
        Me.indigo.TenantId = companyPermission.TenantId
        Me.indigo.ServiceConfigurationId = companyPermission.ServiceConfigurationId
        Me.indigo.IndigoOperatingUnitId = companyPermission.IdOperatingUnitDefault
        Me.indigo.DecimalSeparator = companyPermission.DecimalSeparator
    End Sub

    Private Sub CargarUnidadesOperativas(ByVal companyPermission As Company)
        Dim operatingUnitByContainerPermission As List(Of Domain.Entities.OperatingUnit)
        Using modelPrincipal As MmdiPrincipal = New MmdiPrincipal()
            operatingUnitByContainerPermission = modelPrincipal.GetOperatingUnitByContainerPermission(companyPermission.Id)
        End Using
        Me.indigo.ListOperatingUnitPermission = operatingUnitByContainerPermission
        If companyPermission.IdOperatingUnitDefault = 0 AndAlso operatingUnitByContainerPermission IsNot Nothing AndAlso operatingUnitByContainerPermission.Count > 0 Then
            companyPermission.IdOperatingUnitDefault = operatingUnitByContainerPermission.FirstOrDefault().Id
        End If
        Me.INDglePermissionOperating.SafeInvoke(Sub(gle As GridLookUpEdit) gle.Properties.DataSource = operatingUnitByContainerPermission)
        Me.INDglePermissionOperating.SafeInvoke(Sub(gle As GridLookUpEdit) gle.EditValue = companyPermission.IdOperatingUnitDefault)
    End Sub

    ''' <summary>
    ''' Funcion que se utiliza para consultar los modulos y pintarlos en el MDI de forma asíncrona
    ''' </summary>
    ''' <returns></returns>
    Public Function LoadListMenuAsync() As Task
        Return Task.Factory.StartNew(AddressOf LoadListMenu)
    End Function

    ''' <summary>
    ''' Funcion que se utiliza para consultar los modulos y pintarlos en el MDI
    ''' </summary>
    Public Sub LoadListMenu()
        TaskloadListForms.Wait()
        'Se cambia el origen datos por DB
        Dim forms As List(Of VieForm) = presenter.ListForm
        Dim res = forms.Where(Function(f) f.HasForm AndAlso SessionValues.Instance.IsAllowPermissionForm(f.Id.ToString())).ToList()

#If DEBUG Then
        indigo.ListFormPermission = res
#Else
        If indigo.IndigoCompanyNit = "900433437" OrElse indigo.IndigoCompanyNit = "900622551" Then 'Si el nit de la compañía es igual al de FarmaQx o Jersalud, se muestran todos los forms
            indigo.ListFormPermission = res
        Else 'Si no es igual, no se muestran los forms de dispensación por paciente
            indigo.ListFormPermission = (From x In res Where x.Id <> 1984 And x.Id <> 2032 And x.Id <> 2033 Select x).ToList()
        End If
#End If
        If ApplicationSetting.Instance.LoginAzure Then
            'Se valida si el usuario tiene permiso para el botón de Autorización - EHR
            INDbtnAuthorizationEHR.SafeInvoke(Sub()
                                                  INDlygSecurityEHR.Visibility = LayoutVisibility.Never
                                                  INDlyItemRolesEHR.Visibility = LayoutVisibility.Never
                                                  INDlyItemGroupEHR.Visibility = LayoutVisibility.Never
                                                  INDlyItemUsersEHR.Visibility = LayoutVisibility.Never
                                                  INDlyItemAuthorizationEHR.Visibility = LayoutVisibility.Never

                                                  If indigo.ListFormPermission.Where(Function(x) x.Id = 2055).Count() > 0 Then
                                                      INDlygSecurityEHR.Visibility = LayoutVisibility.Always
                                                      INDlyItemAuthorizationEHR.Visibility = LayoutVisibility.Always
                                                  End If
                                              End Sub)
        Else
            'Se valida si el usuario tiene permiso para el botón de Autorización - EHR
            INDbtnAuthorizationEHR.SafeInvoke(Sub()
                                                  INDlygSecurityEHR.Visibility = LayoutVisibility.Never
                                                  INDlyItemRolesEHR.Visibility = LayoutVisibility.Never
                                                  INDlyItemGroupEHR.Visibility = LayoutVisibility.Never
                                                  INDlyItemUsersEHR.Visibility = LayoutVisibility.Never
                                                  INDlyItemAuthorizationEHR.Visibility = LayoutVisibility.Never

                                                  If indigo.ListFormPermission.Where(Function(x) x.Id = 2188).Count() > 0 Then
                                                      INDlygSecurityEHR.Visibility = LayoutVisibility.Always
                                                      INDlyItemRolesEHR.Visibility = LayoutVisibility.Always
                                                  End If

                                                  If indigo.ListFormPermission.Where(Function(x) x.Id = 2189).Count() > 0 Then
                                                      INDlygSecurityEHR.Visibility = LayoutVisibility.Always
                                                      INDlyItemGroupEHR.Visibility = LayoutVisibility.Always
                                                  End If

                                                  If indigo.ListFormPermission.Where(Function(x) x.Id = 2190).Count() > 0 Then
                                                      INDlygSecurityEHR.Visibility = LayoutVisibility.Always
                                                      INDlyItemUsersEHR.Visibility = LayoutVisibility.Always
                                                  End If

                                                  If indigo.ListFormPermission.Where(Function(x) x.Id = 2055).Count() > 0 Then
                                                      INDlygSecurityEHR.Visibility = LayoutVisibility.Always
                                                      INDlyItemAuthorizationEHR.Visibility = LayoutVisibility.Always
                                                  End If
                                              End Sub)
        End If


        'Se valida si el usuario tiene permiso para el botón de Auditoria Historia Clinica
        INDbtnAuditoriaHC.SafeInvoke(Sub()
                                         INDlyItemAuditoriaHC.Visibility = LayoutVisibility.Never
                                         If indigo.ListFormPermission.Where(Function(x) x.Id = 2066).Count() > 0 Then
                                             INDlyItemAuditoriaHC.Visibility = LayoutVisibility.Always
                                         End If
                                     End Sub)
    End Sub

    ''' <summary>
    ''' Mostrar mensaje slide este evento se dispara cuando se envia un mensaje desde cualquier parte de la aplicacion haciendo uso del patron observador suscriptor
    ''' </summary>
    ''' <param name="Mensaje">The mensaje.</param>
    ''' <param name="Tipo">The tipo.</param>
    Public Sub MostrarMensajeSlide(ByVal Mensaje As String, ByVal Tipo As MessageType, Optional form As Form = Nothing) Implements Window.IObservador.MostrarMensajeSlide
        Dim colorMsg As Color
        Dim img As Image = Nothing
        Dim caption As String = ""
        If form IsNot Nothing Then
            caption = form.Text
        End If
        Dim flag As Boolean = True
        If ConfigurationFile.Instance.TypeAlertControl = eTypeAlertControl.ToastNotification Then
            If (New DesktopBridge.Helpers()).IsRunningAsUwp Then
                Me.CheckWindowsNotification()
                If (Not Me.PceMessages.Visible) Then
                    flag = False
                    Dim toastContentBuilder As Microsoft.Toolkit.Uwp.Notifications.ToastContentBuilder = New Microsoft.Toolkit.Uwp.Notifications.ToastContentBuilder()
                    toastContentBuilder.AddArgument("Header", Text)
                    toastContentBuilder.AddArgument("Body", Mensaje)
                    Dim guid As System.Guid = System.Guid.NewGuid()
                    toastContentBuilder.AddArgument("ID", guid.ToString())
                    toastContentBuilder.AddAppLogoOverride(New Uri(String.Concat(Window.Utils.GetApplicationPath(), "Indigo.ico")), New Nullable(Of ToastGenericAppLogoCrop)(ToastGenericAppLogoCrop.Circle), Nothing, Nothing)
                    toastContentBuilder.AddText(caption)
                    toastContentBuilder.AddText(Mensaje)
                    toastContentBuilder.AddAttributionText("Vie Cloud Platform")
                    toastContentBuilder.Show()
                End If
            Else
                Me.CheckWindowsNotification()
                If (Not Me.PceMessages.Visible) Then
                    If ToastNotificationsManager1 Is Nothing Then
                        DisableToastNotifications()
                    Else
                        flag = False
                        Dim _toastNotification = ToastNotificationsManager1.Notifications(0)
                        Dim _toastNotificationNew = New ToastNotification()
                        _toastNotificationNew.Assign(_toastNotification)
                        _toastNotificationNew.Header = caption
                        _toastNotificationNew.Body = Text
                        '_toastNotificationNew.Image = image
                        _toastNotificationNew.ID = Guid.NewGuid
                        ToastNotificationsManager1.Notifications.Add(_toastNotificationNew)
                        ToastNotificationsManager1.ShowNotification(_toastNotificationNew)
                    End If
                Else
                    DisableToastNotifications()
                End If
            End If
        End If

        If flag Then
            Select Case Tipo
                Case MessageType.Errores
                    img = ThemeResourceManager.GetIconMessageIndigo("Sharp Plus", Tipo, False)
                    colorMsg = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(46, Byte), Integer))
                Case MessageType.Information
                    img = INDicIconosMensaje.Images(0)
                    colorMsg = System.Drawing.Color.FromArgb(CType(CType(87, Byte), Integer), CType(CType(172, Byte), Integer), CType(CType(214, Byte), Integer))
                Case MessageType.Messages
                    img = ThemeResourceManager.GetIconMessageIndigo("Sharp Plus", Tipo, False)
                    colorMsg = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(202, Byte), Integer), CType(CType(0, Byte), Integer))
                Case MessageType.MessagesLync
                    img = ThemeResourceManager.GetIconMessageIndigo("Sharp Plus", Tipo, False)
                    colorMsg = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(202, Byte), Integer), CType(CType(0, Byte), Integer))
                Case MessageType.Question
                    img = ThemeResourceManager.GetIconMessageIndigo("Sharp Plus", Tipo, False)
                    colorMsg = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(77, Byte), Integer))
                Case MessageType.Warning
                    img = INDicIconosMensaje.Images(1)
                    colorMsg = System.Drawing.Color.FromArgb(CType(CType(214, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(48, Byte), Integer))
            End Select
            ShowMessageAlert(Me, caption, Mensaje, "", img, "", colorMsg, Tipo)
        End If
        BaseClass.FreeMemory()
    End Sub

    Private Sub CheckWindowsNotification()
        Dim _ToastNotifierCompat As ToastNotifierCompat = ToastNotificationManagerCompat.CreateToastNotifier()
        If _ToastNotifierCompat IsNot Nothing AndAlso _ToastNotifierCompat.Setting = Windows.UI.Notifications.NotificationSetting.Enabled Then
            Me.PceMessages.Visible = False
            Return
        End If
        RemoveHandler ToastNotificationManagerCompat.OnActivated, New OnActivated(AddressOf Me.ToastNotificationManagerCompat_Activated)
        ToastNotificationManagerCompat.History.Clear()
        ConfigurationFile.Instance.TypeAlertControl = New Nullable(Of eTypeAlertControl)(eTypeAlertControl.AlertWindows)
        Me.PceMessages.Visible = True
    End Sub

    Private Sub ShowMessageAlert(form As Form, caption As String, text As String, hotTracked As String, image As Image, tag As Object, color As Color, typeMessage As MessageType)
        Me.SafeInvoke(Sub()
                          _alertControl.Show(form, caption, text, hotTracked, image, tag, color)
                      End Sub)
    End Sub

    ''' <summary>
    ''' Metodo para informarle al centro de notificaciones
    ''' </summary>
    ''' <param name="Title"></param>
    ''' <param name="Message"></param>
    ''' <param name="TypeMessage"></param>
    ''' <param name="MessageDate"></param>
    ''' <param name="AditionalMessage"></param>
    ''' <remarks></remarks>
    Public Sub NotificationCenter(Title As String, Message As String, TypeMessage As MessageType, MessageDate As Date, AditionalMessage As String) Implements Window.IObservador.NotificationCenter
        If LblNewMessage.InvokeRequired = True Then
            LblNewMessage.BeginInvoke(Sub()
                                          LblNewMessage.Visible = True
                                          Dim MessageNotification As New CtrNotificationItem(Title, Message, TypeMessage, MessageDate, AditionalMessage)
                                          MessageNotification.Dock = DockStyle.Top
                                          CtrNotificationCenter.INDxcMessages.Controls.Add(MessageNotification)
                                      End Sub)
        Else
            LblNewMessage.Visible = True
            PositionLabelNearPopup()
            Dim MessageNotification As New CtrNotificationItem(Title, Message, TypeMessage, MessageDate, AditionalMessage)
            MessageNotification.Dock = DockStyle.Top
            CtrNotificationCenter.INDxcMessages.Controls.Add(MessageNotification)
        End If

        Me.SafeInvoke(Sub()
                          If CtrNotificationCenter?.INDxcMessages?.Controls IsNot Nothing AndAlso CtrNotificationCenter?.INDxcMessages?.Controls?.Count >= 10 Then
                              CtrNotificationCenter?.INDxcMessages?.Controls?.RemoveAt(0)
                          End If
                      End Sub)

    End Sub

    Private Sub PositionLabelNearPopup()
        If LblNewMessage.Parent IsNot Nothing AndAlso PceMessages.Parent IsNot Nothing Then
            LblNewMessage.Location = New Point(PceMessages.Location.X + PceMessages.Width - 12, PceMessages.Location.Y)
        End If
    End Sub
    ''' <summary>
    ''' Metodo para caragr el control de busquedas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadCtrSearching()
        If Me.CtrSearching Is Nothing Then
            Me.CtrSearching = New Presentation.Controls.CtrSearching()
            Me.PccV12.Size = New Size(My.Computer.Screen.Bounds.Width, My.Computer.Screen.Bounds.Height - 45)
            Me.PccV12.Controls.Add(Me.CtrSearching)
            Me.CtrSearching.Appearance.BackColor = System.Drawing.Color.White
            Me.CtrSearching.Appearance.Options.UseBackColor = True
            Me.CtrSearching.Location = New System.Drawing.Point(2, 2)
            Me.CtrSearching.Margin = New System.Windows.Forms.Padding(0)
            Me.CtrSearching.MinimumSize = New System.Drawing.Size(1000, 700)
            Me.CtrSearching.Name = "CtrSearching"
            Me.CtrSearching.Size = New System.Drawing.Size(1000, 700)
            Me.CtrSearching.TabIndex = 4
            Me.CtrSearching.Dock = DockStyle.Fill
        End If
    End Sub

    ''' <summary>
    ''' Carga el centro de mensajes
    ''' </summary>
    Private Sub LoadNotificationCenter()
        Me.CtrNotificationCenter = New Presentation.Controls.CtrNotificationCenter()
        Me.PccNotificationCenter.Controls.Add(Me.CtrNotificationCenter)
        CtrNotificationCenter.Dock = DockStyle.Fill
        Me.CtrNotificationCenter.Appearance.Options.UseBackColor = True
        Me.CtrNotificationCenter.Name = "CtrNotificationCenter"
    End Sub

    ''' <summary>
    ''' Asigna el tema del usuario
    ''' </summary>
    Public Sub SetThemeSkinDefault()
        If Infrastructure.CrossCutting.Base.Utils.ShowThemeSkinSelector() Then
            AddHandler DevExpress.LookAndFeel.UserLookAndFeel.Default.StyleChanged, AddressOf PpgThemeSkins_StyleChanged
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(Window.Utils.GetThemeSkinName(indigo.UserIndigo))
        Else
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(Infrastructure.CrossCutting.Base.Utils.DEFAULT_SKIN_NAME)
        End If
    End Sub

    ''' <summary>
    ''' Abrir formulario cambiar contraseña.
    ''' </summary>
    Private Sub AbrirFormCambiarContraseña(ByVal ChangePasswordAllUsers As Boolean)
        Me.Cursor = Presentation.Base.BaseClass.ChangeCursorIndigo
        For Each f As Form In Me.MdiChildren
            If f.Name = "FrmCambiarContrasena" Then
                f.Activate()
                Me.Cursor = Cursors.Default
                Exit Sub
            End If
        Next
        Dim cambiarContraseña As New FrmCambiarContrasena
        cambiarContraseña.ChangePasswordAllUsers = ChangePasswordAllUsers
        cambiarContraseña.MdiParent = Me
        cambiarContraseña.Show()
        Me.Cursor = Cursors.Default
    End Sub

    ''' <summary>
    ''' Abrir Formulario de configurar Conexion 
    ''' </summary>
    Private Sub AbrirFormConfigurarConexion()
        Me.Cursor = Presentation.Base.BaseClass.ChangeCursorIndigo
        Dim formularioConfigurar As New FrmConfigurarConexion
        AddHandler formularioConfigurar.LoadCultureUser, AddressOf LoadCultureUser
        Using frmTransparent As New FrmTransparent(formularioConfigurar, False)
            Me.Cursor = Cursors.Default
            frmTransparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Abre el formulario modal del token de la firma electronica
    ''' </summary>
    Private Sub AbrirFormElectronicSignatureToken()
        Me.Cursor = Presentation.Base.BaseClass.ChangeCursorIndigo
        Dim formElectronicSignatureToken As New FrmElectronicSignatureToken(indigo.UserIndigoId, IndigoSingleton.IndigoValoresSesion.Instancia.User_token)
        Using FrmTransparent As New FrmTransparent(formElectronicSignatureToken, False)
            Me.Cursor = Cursors.Default
            FrmTransparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Abrir Formularios para desbloquear Usuarios
    ''' </summary>
    Private Sub AbrirFormDesbloquearUsuarios()
        Me.Cursor = Presentation.Base.BaseClass.ChangeCursorIndigo
        For Each f As Form In Me.MdiChildren
            If f.Name = "FrmDesbloquearUsuarios" Then
                f.Activate()
                Me.Cursor = Cursors.Default
                Exit Sub
            End If
        Next
        Dim formularioDesbloquear As New FrmDesbloquearUsuarios
        formularioDesbloquear.MdiParent = Me
        formularioDesbloquear.Show()
        Me.Cursor = Cursors.Default
    End Sub

    ''' <summary>
    ''' Metodo que se ejecuta cuando el formulario de cristal llamado gestion de material de osteosintesis trata de crear una orden de servicio
    ''' </summary>
    ''' <param name="IdCabecera"></param>
    ''' <param name="idProveedorLinea"></param>
    ''' <param name="codeWareHouse"></param>
    ''' <param name="listaCodigoProductos"></param>
    ''' <remarks></remarks>
    Private Sub EventoGenerarOrdenMaterialOsteosintesis(IdCabecera As Integer, idProveedorLinea As Integer, codeWareHouse As String, listaCodigoProductos As List(Of Tuple(Of String, Integer)), ByVal Optional MesseageDescription As String = "")
        Dim frmOrdenServicio As New FrmPurchaseOrder(IdCabecera, idProveedorLinea, codeWareHouse, listaCodigoProductos, MesseageDescription)
        frmOrdenServicio.StartPosition = FormStartPosition.CenterParent
        Dim size As System.Drawing.Size
        size.Width = 1090
        size.Height = 768
        frmOrdenServicio.Size = size
        frmOrdenServicio.Owner = Me
        Using tras As New FrmTransparent(frmOrdenServicio, False)
            tras.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Obtiene o establece la version de la aplicacion.
    ''' </summary>
    ''' <value>Version</value>
    Public Property VersionAplicacion As String
        Get
            Return INDlygVersion.Text.Substring(8)
        End Get
        Set(ByVal value As String)
            INDlygVersion.Text = "Version " & value
        End Set
    End Property

    ''' <summary>
    ''' Metodo que se encargará de levantar todos los temas de cultura de del aplicativo
    ''' </summary>
    ''' <returns></returns>
    Private Function LoadCultureUser()
        LoadTimeZone()
        LoadFlag(SessionValues.Instance.LanguageCulture)
    End Function

    '''' <summary>
    '''' Captura el idioma establecido en el archivo de configuración
    '''' </summary>
    Public Sub LoadFlag(nameflag As String)
        If String.IsNullOrEmpty(nameflag) Then
            nameflag = "es-CO"
        End If

        SessionValues.Instance.Culture = New CultureInfo(nameflag)
        Thread.CurrentThread.CurrentUICulture = SessionValues.Instance.Culture

        '''' Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
        '''' Thread.CurrentThread.CurrentUICulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"

        Dim flag = String.Format("{0}.png", nameflag)
        Dim index = INDListaImagenes.Images.IndexOfKey(flag)
        INDPcCulture.Properties.Buttons(0).ImageOptions.ImageIndex = index
        INDPcCulture.Properties.Buttons(0).ToolTip = ConfigurationFile.Instance.Culture.DisplayName
        idioma = nameflag
    End Sub
    ''' <summary>
    ''' Captura el idioma establecido en el archivo de configuración
    ''' </summary>
    Public Sub CapturarIdiomaEstablecido()
        If Window.LoaderConfigurationFile.ConfigurationFileExists() Then
            Thread.CurrentThread.CurrentUICulture = ConfigurationFile.Instance.Culture
            idioma = ConfigurationFile.Instance.Culture.Name
        Else
            Thread.CurrentThread.CurrentUICulture = SessionValues.Instance.Culture
            idioma = SessionValues.Instance.Culture.Name
        End If
        Dim flag = String.Format("{0}.png", idioma)
        Dim index = INDListaImagenes.Images.IndexOfKey(flag)
        INDPcCulture.Properties.Buttons(0).ImageOptions.ImageIndex = index
        INDPcCulture.Properties.Buttons(0).ToolTip = ConfigurationFile.Instance.Culture.DisplayName
    End Sub

    Private Sub INDCbeCulture_EditValueChanged(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDCbeCulture.EditValueChanged
        SessionValues.Instance.LanguageCulture = idioma
        LoadFlag(idioma)
        LoadOfficialCurrency()
    End Sub

    ''' <summary>
    ''' Cargar zonahoraria inicial
    ''' </summary>
    Private Async Sub LoadTimeZone()
        If INDGlTimezone.Properties.DataSource Is Nothing Then
            Using modelPrincipal As MmdiPrincipal = New MmdiPrincipal()
                Dim ListTimeZone = Await modelPrincipal.GetTimezone()
                Me.INDGlTimezone.Properties.DataSource = ListTimeZone
            End Using
        End If
        Dim Session = SessionValues.Instance
        If Session Is Nothing AndAlso Session.IdTimeZone = 0 Then
            Exit Sub
        End If
        Me.INDGlTimezone.EditValue = Session.IdTimeZone
        Me.INDPcTimeZone.ToolTip = Me.INDGlTimezone.Text
        Me.INDGlTimezone.Properties.NullText = Me.INDGlTimezone.Text
        setAbbreviatedMonth()
    End Sub

    ''' <summary>
    ''' Actualizar configuración del usuario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDGlTimezone_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDGlTimezone.Closed
        If Me.indigo IsNot Nothing AndAlso Me.INDGlTimezone.EditValue > 0 Then
            Using model As New Presentation.Client.MVP.MLogin
                Me.indigo.IdTimeZone = Me.INDGlTimezone.EditValue
                AssigningValuesUserConfiguration()
                Await model.UpdateUserConfiguration(UserConfigurationCulture)
            End Using
        End If
    End Sub

    Public Sub AssigningValuesUserConfiguration()
        With UserConfigurationCulture
            .IdUser = Me.indigo.UserIndigoId
            .Idtimezone = Me.indigo.IdTimeZone
            .dateFormat = Me.indigo.dateFormat
            .timeFormat = Me.indigo.timeFormat
            .LanguageCulture = idioma
        End With
    End Sub

    ''' <summary>
    ''' Actualizar configuración del usuario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDCbeCulture_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDCbeCulture.Closed
        If Me.indigo IsNot Nothing AndAlso Me.INDCbeCulture.EditValue IsNot String.Empty Then
            Using model As New Presentation.Client.MVP.MLogin
                AssigningValuesUserConfiguration()
                Await model.UpdateUserConfiguration(UserConfigurationCulture)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Actualiza variables de sessión 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGlTimezone_EditValueChanged(sender As Object, e As EventArgs) Handles INDGlTimezone.EditValueChanged
        If (CInt(MyBase.MdiChildren.Length) <= 0) AndAlso Me.indigo.IdTimeZone > 0 Then
            Dim idTimeZone = Me.INDGlTimezone.EditValue
            Me.indigo.IdTimeZone = idTimeZone
            Me.indigo.TimezoneName = Me.INDGlTimezone.Text
            Me.INDPcTimeZone.ToolTip = Me.indigo.TimezoneName
            Me.INDGlTimezone.SafeInvoke(Sub(gle As GridLookUpEdit) gle.EditValue = idTimeZone)
        End If
    End Sub

    ''' <summary>
    ''' Carga el formato numerico en el hilo principal (para todo el aplicativo), depende de los parametros de empresa
    ''' </summary>
    ''' <returns></returns>
    Private Async Function LoadOfficialCurrency() As Task
        Using Model As New MmdiPrincipal()
            Dim _currency = Await Model.GetOfficialCurrencyAsync()
            If _currency Is Nothing OrElse _currency?.Id = 0 Then
                MessageIndigo.Show("No se logro establecer la moneda oficial", MessageType.Errores, "Vie ERP")
                Return
            End If
            indigo.OfficialCurrencyId = _currency?.Id
            indigo.CurrencyISO4217 = _currency?.Abbreviation
            indigo.CurrencyName = _currency?.ISO4217?.CurrencyName
            Dim format As NumberFormatInfo = indigo.CurrencyNumbertFormat
            Thread.CurrentThread.CurrentUICulture.NumberFormat = format
            Thread.CurrentThread.CurrentCulture.NumberFormat = format
        End Using
    End Function

    ''' <summary>
    ''' Levantar los meses abreviados para formato de fecha dd/mmm/yyyy
    ''' </summary>
    Public Shared Sub setAbbreviatedMonth()
        Thread.CurrentThread.CurrentCulture.DateTimeFormat.AbbreviatedMonthGenitiveNames = New String() {"ene", "feb", "mar", "abr", "may", "jun", "jul", "ago", "sep", "oct", "nov", "dec", ""}
        Thread.CurrentThread.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames = New String() {"ene", "feb", "mar", "abr", "may", "jun", "jul", "ago", "sep", "oct", "nov", "dec", ""}
    End Sub

    ''' <summary>
    ''' metodo que borra la carpeta (para ejercer la accion de limpiar cache)
    ''' </summary>
    Private Sub ClearCache()
        Dim userdatafolder = Path.Combine(Window.Utils.GetPathUserFiles(), "cache")
        If (System.IO.Directory.Exists(userdatafolder)) Then
            Try
                DeleteRecursiveDir(userdatafolder)
            Catch ex As Exception
                'MessageIndigo.Show("Error eliminando la cache " & ex.Message.ToString(), MessageType.Errores, obtenerRecurso(Eresources.ComunesIndigoCrystal))
            End Try
        End If
        MessageIndigo.Show("Operación ejecutada con éxito", MessageType.Information, obtenerRecurso(Eresources.ComunesIndigoCrystal))
    End Sub

    Private Sub DeleteRecursiveDir(path As String)
        Dim files = Directory.GetFiles(path)
        Dim dirs = Directory.GetDirectories(path)

        For Each file In files
            System.IO.File.SetAttributes(file, FileAttributes.Normal)
            System.IO.File.Delete(file)
        Next

        For Each folder In dirs
            DeleteRecursiveDir(folder)
        Next

        Directory.Delete(path, False)
    End Sub

#End Region

#Region "Eventos"

    ''' <summary>
    ''' Ocurre cuando el usuario actual elimina sus preferencias
    ''' </summary>
    Protected Event CustomizationDeleted()

#End Region

#Region "Manejadores"

    ''' <summary>
    ''' Aqui se modifica el modo de visualización en los formularios
    ''' </summary>
    Private Async Sub PceConfig_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles PceConfig.CloseUp
        If indigo.UserViewMode <> CBool(INDrbgViewForm.EditValue) Then
            Using Model As New MmdiPrincipal
                Dim Result As Boolean
                Result = Await Model.ChangeViewModeAsync(indigo.UserIndigo, CBool(INDrbgViewForm.EditValue))
                If Result = True Then
                    MessageIndigo.Show(obtenerRecurso(Eresources.ComunesActualizado), MessageType.Information, obtenerRecurso(Eresources.ComunesIndigoCrystal))
                    indigo.UserViewMode = CBool(INDrbgViewForm.EditValue)
                Else
                    INDrbgViewForm.EditValue = INDrbgViewForm.OldEditValue
                    MessageIndigo.Show(obtenerRecurso(Eresources.ComunesError), MessageType.Information, obtenerRecurso(Eresources.ComunesIndigoCrystal))
                End If
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Aqui se muestra el menú de favoritos
    ''' </summary>
    Private Sub INDBsvtiFavorites_ItemPressed(sender As Object, e As BackstageViewItemEventArgs) Handles INDBsvtiFavorites.ItemPressed
        Me.RibbonControl_ApplicationButtonClick(sender, New EventArgs())
    End Sub
    Private Async Sub RibbonControl_ApplicationButtonClick(sender As Object, e As EventArgs) Handles RibbonControl.ApplicationButtonClick
        If Me.INDBsvtiFavorites.Visible Then
            If BackstageVCPrincipal.SelectedTabIndex = -1 Then
                Me.BackstageVCPrincipal.SelectedTab = Me.INDBsvtiFavorites
            End If
            If indigo.ListFormPermission Is Nothing OrElse indigo.ListFormPermission.Count = 0 Then
                Await taskFormPermission
            End If
            If Not taskMyFavorites.IsCompleted Then
                Await taskMyFavorites
            End If
            Dim ctrMenuForm As CtrMenuForm
            If Not INDBsvtiFavorites?.ContentControl?.Controls?.ToEntityList(Of Object)?.ToList()?.Any(Function(x) TypeOf (x) Is CtrMenuForm) Then
                ctrMenuForm = New CtrMenuForm()
                ctrMenuForm.IsMyFavorites = True
                ctrMenuForm.Dock = DockStyle.Fill
                AddHandler ctrMenuForm.ItemFormSelected, AddressOf ClickOpenForm
                AddHandler ctrMenuForm.ItemFormDeletedFromMyFavorites, AddressOf DeleteFromMyFavorites
                AddHandler ctrMenuForm.ItemFormAddToMyFavorites, AddressOf AddToMyFavorites
                INDBsvtiFavorites.ContentControl.Controls.Add(ctrMenuForm)
            Else
                ctrMenuForm = INDBsvtiFavorites.ContentControl.Controls?.ToEntityList(Of Object)?.FirstOrDefault(Function(x) TypeOf (x) Is CtrMenuForm)
            End If
            'Eliminamos los formularios a los que ya no tenemos permiso
            Dim idxOnDelete As New List(Of Integer)()
            For f As Integer = 0 To Me.myFavorites.Count - 1
                Dim idx As Integer = f
                If Not indigo.ListFormPermission.Any(Function(p) p.Id = Me.myFavorites(idx).IdForm) Then
                    idxOnDelete.Add(idx)
                End If
            Next
            For Each r As Integer In idxOnDelete
                If Me.myFavorites(r) IsNot Nothing AndAlso Me.myFavorites(r).IdForm > 0 Then
                    Me.myFavorites.RemoveAt(r)
                End If
            Next
            If idxOnDelete.Count > 0 Then
                Await Me.SaveMyFavoritesAsync()
            End If

            ctrMenuForm.GridControl1.DataSource = Me.myFavorites.Where(Function(f) Not BaseClass.ListFormHide.Contains(f.IdForm)).ToList()
            ctrMenuForm.GridControl1.RefreshDataSource()
        End If
        If Not validatedSuscription Then
            ValidateSuscriptions()
        End If
    End Sub

    ''' <summary>
    ''' Aqui se valida y se realiza el cambio de empresa
    ''' </summary>
    Private Sub INDgleCompanies_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDgleCompanies.EditValueChanging
        If Me.MdiChildren.Length > 0 Then
            e.Cancel = True
            MessageIndigo.Show("Para cambiar de empresa primero termine lo que está haciendo y cierre todos los formularios", MessageType.Warning, "Vie ERP")
        Else
            Dim companyPermission = TryCast(INDgleCompanies.Properties.View.GetFocusedRow(), Company)
            If companyPermission IsNot Nothing Then
                Try
                    Dim vDbSchema As Version = New Version(companyPermission.Version)
                    If Not vDbSchema.Equals(BaseClass.GetAppVersion()) Then
                        e.Cancel = True
                        Dim frm As New Presentation.Controls.FrmNotificationItemDetail
                        frm.TxtMessage.Text = "La versión del esquema en la empresa " & companyPermission.Code & " - " & companyPermission.Name & ", NO corresponde a la versión de éste cliente (" & BaseClass.GetAppVersion().ToString() & ")." & vbCrLf & "Porfavor actualice la versión del cliente e intente de nuevo."
                        Using transparent As New FrmTransparent(frm, False)
                            transparent.ShowDialog(Me)
                        End Using
                    End If
                Catch ex As Exception
                    e.Cancel = True
                    Dim frm As New Presentation.Controls.FrmNotificationItemDetail
                    frm.TxtMessage.Text = "La versión del esquema en la empresa " & companyPermission.Code & " - " & companyPermission.Name & ", NO corresponde a la versión de éste cliente (" & BaseClass.GetAppVersion().ToString() & ")." & vbCrLf & "Porfavor actualice la versión del cliente e intente de nuevo."
                    Using transparent As New FrmTransparent(frm, False)
                        transparent.ShowDialog(Me)
                    End Using
                End Try
            End If
        End If
    End Sub
    Private Async Sub INDgleCompanies_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleCompanies.EditValueChanged
        If Not Cargando Then
            Await LoadOperatingUnitAsync(True)
            If INDgleCompanies.GetSelectedDataRow IsNot Nothing Then
                validatedSuscription = False
                indigo.ListFormPermission = Nothing
                FilterFormBySuscription()
                taskFormPermission = LoadListMenuAsync()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Aqui se modifica el tamaño del dashboard
    ''' </summary>
    Private Sub LblUserName_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles LblUserName.QueryPopUp
        Me.INDPopupContainerDashBoard.Size = New Size(My.Computer.Screen.Bounds.Width, My.Computer.Screen.Bounds.Height - 45)
    End Sub

    Private Sub LblVersion_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles LblVersion.QueryPopUp
        e.Cancel = True
    End Sub

    Private Sub LblContainer_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles LblContainer.QueryPopUp
        e.Cancel = True
    End Sub

    ''' <summary>
    ''' Aqui se redimensiona el tamaño del PopUp
    ''' </summary>
    Private Sub PceV12_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles PceV12.QueryPopUp
        Me.LoadCtrSearching()
        Me.PccV12.Size = New Size(My.Computer.Screen.Bounds.Width, My.Computer.Screen.Bounds.Height - 45)
        Me.CtrSearching.SetFocusOnTextSearch()
    End Sub

    ''' <summary>
    ''' Aqui se ejecuta la apertura de los formularios
    ''' </summary>
    Private Sub CtrSearching_QueryOpenForm(sender As Object, e As QueryOpenFormEventArgs) Handles CtrSearching.QueryOpenForm
        Me.OpenForm(e.IdForm, e.IdEntity)
        Me.PceV12.ClosePopup()
    End Sub

    ''' <summary>
    ''' Se oculta la imagen que indica mensaje nuevo
    ''' </summary>
    Private Sub PceMessages_Popup(sender As Object, e As EventArgs) Handles PceMessages.Popup
        LblNewMessage.Visible = False
    End Sub

    ''' <summary>
    ''' Aqui se controla el cambio de tema
    ''' </summary>
    Private Sub PpgThemeSkins_StyleChanged(sender As Object, e As EventArgs)
        If Me.flagThemeChanged Then
            Me.flagThemeChanged = False
            'Cambiamos el icono del boton inicio
            'Dim skin = RibbonSkins.GetSkin(UserLookAndFeel.Default.ActiveLookAndFeel)
            'Dim element = skin(RibbonSkins.SkinFormApplicationButton)
            Me.RibbonControl.ApplicationIcon = My.Resources.Logo_Transparente

            'element.Image.Image = Presentation.Resources.ThemeResourceManager.GetLogoImageByTheme(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
            'Me.RibbonControl.ApplicationButtonImageOptions.SvgImage = Presentation.Resources.ThemeResourceManager.GetLogoImageByTheme(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
            Me.RibbonControl.ApplicationButtonImageOptions.Image = Presentation.Resources.ThemeResourceManager.GetLogoImageByThemePng(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
            'Me.Icon = My.Resources.indigo1

            'Cambiamos los iconos de los botones de configuración
            Me.PceV12.Properties.Buttons(0).Image = ThemeResourceManager.GetVituelImageByTheme(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
            Me.PceConfig.Properties.Buttons(0).Image = ThemeResourceManager.GetConfigButtonImageByTheme(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
            Me.PceMessages.Properties.Buttons(0).Image = ThemeResourceManager.GetNotificationButtonImageByTheme(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
            Me.INDPceChangePerfil.Properties.Buttons(0).Image = ThemeResourceManager.GetImageByThemeChangePerfil(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
            Me.INDBtnBloqueoSession.Image = ThemeResourceManager.GetBlockSessionButtonImageByTheme(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
            Me.INDBtnSegPassword.Image = ThemeResourceManager.GetChangePasswdButtonImageByTheme(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
            Me.INDBtnCerrarSession.Image = ThemeResourceManager.GetCloseSessionButtonImageByTheme(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
            Me.INDBtnSalir.Image = ThemeResourceManager.GetExitButtonImageByTheme(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
            Me.INDBtnConexionesERP.Image = ThemeResourceManager.GetConnectionButtonImageByTheme(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
            Me.INDBtnConexionHIS.Image = ThemeResourceManager.GetConnectionButtonImageByTheme(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
            Me.INDbtnUsers.Image = ThemeResourceManager.GetUsersButtonImageByTheme(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
            Me.INDbtnRoll.Image = ThemeResourceManager.GetRolesButtonImageByTheme(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
            Me.INDbtnGroups.Image = ThemeResourceManager.GetGroupsButtonImageByTheme(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
            Me.INDbtnChangePasswd.Image = ThemeResourceManager.GetChangePasswdButtonImageByTheme(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
            Me.INDBtnDesbloquearUsuario.Image = ThemeResourceManager.GetUnlockUserButtonImageByTheme(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
            Me.BtnClearCache.Image = ThemeResourceManager.GetImageByTheme(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName, 26)
            Me.INDbtnCleanCustom.Image = ThemeResourceManager.GetImageByTheme(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName, 25)
            Me.BtnThemeSkins.Image = ThemeResourceManager.GetImageByTheme(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName, 23)
            Me.INDsbDashboard.Image = ThemeResourceManager.GetImageByTheme(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName, 22)
            Me.SimpleButton8.Image = ThemeResourceManager.GetImageByTheme(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName, 27) 'numeric secuence

            Me.INDbtnRolesEHR.Image = ThemeResourceManager.GetImageByTheme(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName, 21)
            Me.INDbtnGroupEHR.Image = ThemeResourceManager.GetImageByTheme(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName, 21)
            Me.INDbtnUsersEHR.Image = ThemeResourceManager.GetImageByTheme(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName, 21)
            Me.INDbtnAuthorizationEHR.Image = ThemeResourceManager.GetImageByTheme(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName, 21)

            Me.INDbtnAuditoriaHC.Image = ThemeResourceManager.GetImageByTheme(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName, 20)
            Me.SimpleButton13.Image = ThemeResourceManager.GetImageByTheme(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName, 24) 'configurar empresa


            LookAndFeelHelper.ForceDefaultLookAndFeelChanged()
            Window.Utils.SetThemeSkinName(indigo.UserIndigo, DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
            Me.SetThemeSkinDefault()

            DefaultLookAndFeel1.LookAndFeel.UpdateStyleSettings()
            Select Case DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName
                Case "The Bezier", "Office 2019 Dark Gray", "Black", "Office 2007 Black", "Pumpkin", "Office 2019 Colorful"
                    LblUserName.Properties.Buttons(0).Appearance.ForeColor = Color.White
                    LblVersion.Properties.Buttons(0).Appearance.ForeColor = Color.White
                    LblContainer.Properties.Buttons(0).Appearance.ForeColor = Color.White
                    'INDTeTenant.Properties.Appearance.ForeColor = Color.White
                    'INDTeCompany.Properties.Appearance.ForeColor = Color.White
                    INDTeTenant.Properties.Appearance.ForeColor = DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinMaskColor
                    INDTeCompany.Properties.Appearance.ForeColor = DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinMaskColor
                Case Else
                    LblUserName.Properties.Buttons(0).Appearance.ForeColor = DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinMaskColor
                    LblContainer.Properties.Buttons(0).Appearance.ForeColor = DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinMaskColor
                    LblVersion.Properties.Buttons(0).Appearance.ForeColor = DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinMaskColor
                    INDTeTenant.Properties.Appearance.ForeColor = DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinMaskColor
                    INDTeCompany.Properties.Appearance.ForeColor = DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinMaskColor
            End Select
        End If
    End Sub

    Private Sub PpgThemeSkins_Gallery_ItemClick(sender As Object, e As GalleryItemClickEventArgs) Handles PpgThemeSkins.Gallery.ItemClick
        Me.flagThemeChanged = True
    End Sub

    Public Sub INDAlertControl_BeforeFormShow(sender As Object, e As AlertFormEventArgs)
        e.AlertForm.Width = 320
    End Sub

    Public Sub INDAlertControl_AlertClick(sender As Object, e As AlertClickEventArgs)
        Dim Formulario = New FrmTransparent(New FrmFlyoutIndigo(e.Info.Text, Botones.Cancelar, CType(sender, CustomAlertControl).TypeMessage, Nothing)).ShowDialog(MDISingleInstance.Instance.InstanceMDI)
    End Sub

    Private Sub ToastNotificationsManager1_Activated(sender As Object, e As DevExpress.XtraBars.ToastNotifications.ToastNotificationEventArgs) Handles ToastNotificationsManager1.Activated
        Dim _toastNotification As IToastNotificationProperties = ToastNotificationsManager1.GetNotificationByID(e.NotificationID)
        If _toastNotification IsNot Nothing Then
            Dim Formulario = New FrmTransparent(New FrmFlyoutIndigo(_toastNotification.Body, Botones.Cancelar, MessageType.Information, _toastNotification.Image)).ShowDialog(MDISingleInstance.Instance.InstanceMDI)
        End If
    End Sub

    Private Sub ToastNotificationsManager1_Failed(ByVal sender As Object, ByVal e As ToastNotificationFailedEventArgs) Handles ToastNotificationsManager1.Failed
        If ToastNotificationsManager1 IsNot Nothing Then
            Dim _toastNotification As IToastNotificationProperties = ToastNotificationsManager1.GetNotificationByID(e.NotificationID)
            If _toastNotification IsNot Nothing Then
                ConfigurationFile.Instance.TypeAlertControl = eTypeAlertControl.AlertWindows
                MostrarMensajeSlide(_toastNotification.Body, MessageType.Information, New Form() With {.Text = _toastNotification.Header})
                NotificationCenter(_toastNotification.Header, _toastNotification.Body, MessageType.Information, Date.Now, "")
                DisableToastNotifications()
            End If
        End If
    End Sub

    Private Sub ToastNotificationManagerCompat_Activated(ByVal e As ToastNotificationActivatedEventArgsCompat)
        Dim _ToastArguments As ToastArguments = ToastArguments.Parse(e.Argument)
        Me.SafeInvoke(Sub()
                          Dim Formulario = New FrmTransparent(New FrmFlyoutIndigo(_ToastArguments.Item("Body"), Botones.Cancelar, MessageType.Information, Nothing)).ShowDialog(MDISingleInstance.Instance.InstanceMDI)
                      End Sub)
    End Sub

    Private Async Sub BackstageVItem_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BackstageVItemContabilidad.ItemPressed, BvtAdministracionImpuestos.ItemPressed, BackstageViewTabItem4.ItemPressed, BackstageViewTabItem3.ItemPressed, BackstageViewTabItem1.ItemPressed, BackstageViewTabItem9.ItemPressed, BackstageViewTabItem8.ItemPressed, BackstageViewTabItem7.ItemPressed, BackstageViewTabItem6.ItemPressed, BackstageViewTabItem2.ItemPressed, BackstageViewCostIntegrated.ItemPressed, BackstageViewTabItem12.ItemPressed, BackstageViewTabItem11.ItemPressed, BackstageViewTabItem10.ItemPressed, BackstageViewTabItem14.ItemPressed, BackstageViewTabItem19.ItemPressed, BackstageViewTabItem18.ItemPressed, BackstageViewTabItem20.ItemPressed, BackstageViewTabItem21.ItemPressed, BackstageViewTabItem22.ItemPressed, BackstageViewTabItem23.ItemPressed, BackstageViewTabItem24.ItemPressed, BackstageViewCostNative.ItemPressed, BackstageViewTabItem34.ItemPressed, BackstageViewTabItem5.ItemPressed, BackstageViewTabItem33.ItemPressed, BackstageViewTabItem13.ItemPressed, BvtCentralMezclas.ItemPressed,
            BackstageViewTabItem36.ItemPressed, BackstageViewTabItem37.ItemPressed, BackstageViewTabItem38.ItemPressed, BackstageViewTabItem39.ItemPressed, BackstageViewTabItem40.ItemPressed,
            BackstageViewTabItem41.ItemPressed, BackstageViewTabItem42.ItemPressed, BackstageViewTabItem43.ItemPressed, BackstageViewTabItem44.ItemPressed, BackstageViewTabItem45.ItemPressed

        Dim item As BackstageViewTabItem = CType(sender, BackstageViewTabItem)
        'If indigo.ListFormPermission Is Nothing OrElse indigo.ListFormPermission.Count = 0 Then
        '    Await taskFormPermission
        'End If
        Dim ctrMenuForm As CtrMenuForm
        If item.ContentControl.Controls.Count = 0 Then
            ctrMenuForm = New CtrMenuForm()
            ctrMenuForm.Dock = DockStyle.Fill
            AddHandler ctrMenuForm.ItemFormSelected, AddressOf ClickOpenForm
            AddHandler ctrMenuForm.ItemFormDeletedFromMyFavorites, AddressOf DeleteFromMyFavorites
            AddHandler ctrMenuForm.ItemFormAddToMyFavorites, AddressOf AddToMyFavorites
            item.ContentControl.Controls.Add(ctrMenuForm)
        Else
            ctrMenuForm = item.ContentControl.Controls(0)
        End If
        'If ApplicationSetting.Instance.LoginAzure Then
        Dim _listForms = (From pc In indigo.ListProductCatalog
                          From m In pc.ListModules
                          From f In m.ListForm
                          Where Not String.IsNullOrEmpty(f.AssemblyName) AndAlso m.IdModule = item.Tag AndAlso Not BaseClass.ListFormHide.Contains(f.IdForm)
                          Select f).ToList()

        ctrMenuForm.GridControl1.DataSource = Nothing
        ctrMenuForm.GridControl1.DataSource = _listForms
        'ctrMenuForm.GridControl1.DataSource = {New VieForm With {.Order = 1, .Name = "Prueba"}}.ToList()
        'Else
        '    If indigo.ListFormPermission.Where(Function(x) x.Modules.Any(Function(m) m.Id = item.Tag)).ToList().Find(Function(o) o.Type = 5) IsNot Nothing Then
        '        indigo.ListFormPermission.Where(Function(x) x.Modules.Any(Function(m) m.Id = item.Tag)).ToList().ForEach(Sub(x)
        '                                                                                                                     Select Case x.Type
        '                                                                                                                         Case 2
        '                                                                                                                             x.Type = 7
        '                                                                                                                     End Select
        '                                                                                                                 End Sub)
        '    End If
        '    ctrMenuForm.GridControl1.DataSource = indigo.ListFormPermission.
        'Where(Function(x) Not BaseClass.ListFormHide.Contains(x.Id) AndAlso x.Modules.Any(Function(m) m.Id = item.Tag)).OrderBy(Function(o) o.Name).ToList()
        'End If
    End Sub

    Private Async Sub BackstageViewTabItem15_ItemPressed(sender As Object, e As BackstageViewItemEventArgs) Handles BackstageViewTabItem15.ItemPressed
        Dim item As BackstageViewTabItem = CType(sender, BackstageViewTabItem)
        If indigo.ListFormPermission Is Nothing OrElse indigo.ListFormPermission.Count = 0 Then
            Await taskFormPermission
        End If
        Dim ctrMenuForm As CtrMenuForm
        If item.ContentControl.Controls.Count = 0 Then
            ctrMenuForm = New CtrMenuForm()
            ctrMenuForm.Dock = DockStyle.Fill
            AddHandler ctrMenuForm.ItemFormSelected, AddressOf ClickOpenForm
            AddHandler ctrMenuForm.ItemFormDeletedFromMyFavorites, AddressOf DeleteFromMyFavorites
            AddHandler ctrMenuForm.ItemFormAddToMyFavorites, AddressOf AddToMyFavorites
            item.ContentControl.Controls.Add(ctrMenuForm)
        Else
            ctrMenuForm = item.ContentControl.Controls(0)
        End If
        'If ApplicationSetting.Instance.LoginAzure Then
        Dim _listForms = indigo.ListFormPermission.Where(Function(x) x.Module IsNot Nothing AndAlso (Not String.IsNullOrEmpty(x.AssemblyName) AndAlso x.AssemblyName <> "") AndAlso x.Module.Id = item.Tag).ToList()
        ctrMenuForm.GridControl1.DataSource = Nothing
        ctrMenuForm.GridControl1.DataSource = _listForms.Where(Function(x) Not BaseClass.ListFormHide.Contains(x.Id)).ToList()
        'Else
        '    If indigo.ListFormPermission.Where(Function(x) x.Modules.Any(Function(m) m.Id = item.Tag)).ToList().Find(Function(o) o.Type = 5) IsNot Nothing Then
        '        indigo.ListFormPermission.Where(Function(x) x.Modules.Any(Function(m) m.Id = item.Tag)).ToList().ForEach(Sub(x)
        '                                                                                                                     Select Case x.Type
        '                                                                                                                         Case 2
        '                                                                                                                             x.Type = 7
        '                                                                                                                     End Select
        '                                                                                                                 End Sub)
        '    End If
        '    ctrMenuForm.GridControl1.DataSource = indigo.ListFormPermission.
        '    Where(Function(x) Not BaseClass.ListFormHide.Contains(x.Id) AndAlso x.Modules.Any(Function(m) m.Id = item.Tag)).OrderBy(Function(o) o.Name).ToList()
        'End If
    End Sub

    ''' <summary>
    ''' Evento click del boton de salir
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDBtnSalir_Click(sender As Object, e As EventArgs) Handles INDBtnSalir.Click
        If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesSeguroCerrarSesion, Eform.Comunes), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub OpenLoginFrm()
        Me.Hide()
        Using frmLogin As New FrmWpfLogin()
            'AddHandler frmLogin.AutenticateHIS, AddressOf AutenticateHIS 'se llama en el evento INDgleCompanies.EditValueChanged
            AddHandler frmLogin.AssingCompanies, AddressOf AssingCompanies
            AddHandler frmLogin.LoginOk, Sub()
                                             FreeMemory()
                                             InitionMDIValues()
                                             ValidateSuscriptions()
                                             FilterFormBySuscription()
                                             frmLogin.DialogResult = DialogResult.OK
                                             Me.Show()
                                         End Sub
            If _flagLockedSession Then
                frmLogin._flagLockedSession = _flagLockedSession
                frmLogin._UserCode = indigo.UserIndigo
            End If
            frmLogin.DialogResult = DialogResult.None
            If frmLogin.ShowDialog() <> DialogResult.OK Then
                Me.Close()
            End If
        End Using
    End Sub

    ''' <summary>
    ''' visualiza el menu dependiendo de la suscripcion
    ''' </summary>
    Private Async Sub ValidateSuscriptions()
        If ApplicationSetting.Instance.LoginAzure AndAlso Not validatedSuscription Then
            validatedSuscription = True
            Dim _BackstageViewTabItem As BackstageViewTabItem = Nothing
            Dim _SubBackstageViewTabItem As BackstageViewTabItem = Nothing
            Dim _BackstageViewControl As BackstageViewControl = Nothing
            Dim _SubItemVisible As Boolean = False
            'UnifiedConfiguration.Instance.PServiceConfiguration = New ServiceConfiguration
            'UnifiedConfiguration.Instance.PServiceConfiguration.AppFunctionURL = "https://loginb2cappfunc.azurewebsites.net"
            'UnifiedConfiguration.Instance.PServiceConfiguration.FunctionKey6 = "GZFCoanQ/m4G0hCKGIvC67si/F82HB5qjegJFMPEZe0TZWoV4oHkUA=="
            Using modelo = New PAutenticacionUsuario(New MAutenticacionUsuario.ParametrosAzureFunctions() With {.AppFunctionURL = UnifiedConfiguration.Instance.PServiceConfiguration.AppFunctionURL,
                                                 .GetSuscriptionsKey = UnifiedConfiguration.Instance.PServiceConfiguration.FunctionKey6})
                Dim _Respuesta = Await modelo.GetSuscriptions()
                'If _Respuesta IsNot Nothing Then
                If _Respuesta.Result IsNot Nothing AndAlso Not _Respuesta.Fallo Then
                    Dim _Suscriptions As List(Of MAutenticacionUsuario.Suscriptions) = _Respuesta.Result
                    If _Suscriptions IsNot Nothing AndAlso _Suscriptions.Count > 0 Then
                        Dim _ProductCatalogIds = _Suscriptions.Select(Function(_suscription) _suscription.ProductCatalogId).ToList
                        Dim _modules = BaseClass.GetXmlWithAggregates(Of VieModule)(eDataXml.XMLModules).Where(Function(m) _ProductCatalogIds.Contains(m.ProductCatalogId)).Select(Function(m) m.Id).ToList
                        Dim _ctrlSeparator = Nothing
                        BackstageVCPrincipal.Items.ToList.ForEach(Sub(item)
                                                                      If TypeOf item Is BackstageViewTabItem Then
                                                                          _BackstageViewTabItem = CType(item, BackstageViewTabItem)
                                                                          If _BackstageViewTabItem.Tag IsNot Nothing Then
                                                                              If _ProductCatalogIds.Contains(_BackstageViewTabItem.Tag) OrElse _BackstageViewTabItem.Tag.Equals("0") Then
                                                                                  _BackstageViewTabItem.Visible = True
                                                                              Else
                                                                                  _BackstageViewTabItem.Visible = False
                                                                              End If
                                                                          Else
                                                                              _SubItemVisible = False
                                                                              For Each _control In _BackstageViewTabItem.ContentControl.Controls
                                                                                  If TypeOf _control Is BackstageViewControl Then
                                                                                      _BackstageViewControl = CType(_control, BackstageViewControl)
                                                                                      For Each subitem In _BackstageViewControl.Items
                                                                                          If TypeOf subitem Is BackstageViewTabItem Then
                                                                                              _SubBackstageViewTabItem = CType(subitem, BackstageViewTabItem)
                                                                                              If _modules.Contains(_SubBackstageViewTabItem.Tag) Then
                                                                                                  _SubBackstageViewTabItem.Visible = True
                                                                                                  _SubItemVisible = True
                                                                                              Else
                                                                                                  _SubBackstageViewTabItem.Visible = False
                                                                                              End If
                                                                                          End If
                                                                                      Next
                                                                                  End If
                                                                              Next
                                                                              If _SubItemVisible Then
                                                                                  _BackstageViewTabItem.Visible = True
                                                                              Else
                                                                                  _BackstageViewTabItem.Visible = False
                                                                              End If
                                                                          End If
                                                                          If Not _BackstageViewTabItem.Visible Then
                                                                              _ctrlSeparator = Me.BackstageVCPrincipal.Items.Where(Function(ctrl) ctrl.Name = _BackstageViewTabItem.Name.Replace("svti", "svis")).FirstOrDefault()
                                                                              If _ctrlSeparator IsNot Nothing AndAlso TypeOf _ctrlSeparator Is BackstageViewItemSeparator Then
                                                                                  CType(_ctrlSeparator, BackstageViewItemSeparator).Visible = False
                                                                              End If
                                                                          End If
                                                                      End If
                                                                  End Sub)
                    Else
                        BackstageVCPrincipal.Items.ToList.ForEach(Sub(item) item.Visible = False)
                    End If
                Else
                    BackstageVCPrincipal.Items.ToList.ForEach(Sub(item) item.Visible = False)
                End If
                'End If
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Filtra los formularios dependiendo de la suscripcion
    ''' </summary>
    Private Async Sub FilterFormBySuscription()
        If ApplicationSetting.Instance.LoginAzure Then
            Await Task.Factory.StartNew(Async Function()
                                            BaseClass._listXmlFormsNuevo = BaseClass._listXmlForms
                                            Using modelo = New PAutenticacionUsuario(New MAutenticacionUsuario.ParametrosAzureFunctions() With {.AppFunctionURL = UnifiedConfiguration.Instance.PServiceConfiguration.AppFunctionURL,
                                                .GetSuscriptionsKey = UnifiedConfiguration.Instance.PServiceConfiguration.FunctionKey6})
                                                Dim _Respuesta = Await modelo.GetSuscriptions()
                                                'If _Respuesta IsNot Nothing Then
                                                If _Respuesta.Result IsNot Nothing AndAlso Not _Respuesta.Fallo Then
                                                    Dim _Suscriptions As List(Of MAutenticacionUsuario.Suscriptions) = _Respuesta.Result
                                                    If _Suscriptions IsNot Nothing AndAlso _Suscriptions.Count > 0 Then
                                                        Dim _ProductCatalogIds = _Suscriptions.Select(Function(_suscription) _suscription.ProductCatalogId).ToList
                                                        Dim _modules = BaseClass.GetXmlWithAggregates(Of VieModule)(eDataXml.XMLModules).Where(Function(m) _ProductCatalogIds.Contains(m.ProductCatalogId)).Select(Function(m) m.Id).ToList
                                                        BaseClass._listXmlFormsNuevo = BaseClass.GetXmlWithAggregates(Of VieForm)(eDataXml.XMLForms).Where(Function(f) f.Module IsNot Nothing AndAlso (_modules.Contains(f.Module.Id) OrElse f.Module.Id = 99)).ToList
                                                    Else
                                                        BaseClass._listXmlFormsNuevo = New List(Of VieForm)
                                                    End If
                                                Else
                                                    BaseClass._listXmlFormsNuevo = New List(Of VieForm)
                                                End If
                                            End Using
                                        End Function)

        Else
            Await Task.Factory.StartNew(Async Sub()
                                            BaseClass._listXmlFormsNuevo = BaseClass.GetXmlWithAggregates(Of VieForm)(eDataXml.XMLForms).Where(Function(f) f.Module IsNot Nothing AndAlso ((f.Module.Id > 0 AndAlso f.Module.Id <= 67) OrElse f.Module.ProductCatalogId = 0)).ToList 'f.Module.Id = 99
                                        End Sub)
        End If
    End Sub

    Private Sub OpenLoginFrmPass(ByVal cerrarSesion As Boolean)
        Me.Hide()
        Login = True
        Dim userCode As String = indigo.UserIndigo
        Using browser As New FrmLoginAzure(Me, cerrarSesion, _tokenId)
            browser.SendToBack()
            browser.DialogResult = System.Windows.Forms.DialogResult.None
            AddHandler Me.Activated, AddressOf MostrarDashboard
            Dim res = browser.ShowDialog()
            If res = System.Windows.Forms.DialogResult.OK Then
                Presentation.Base.BaseClass.FreeMemory()
                If userCode <> indigo.UserIndigo Then
                    CloseOpenForms()
                End If
                InitionMDIValues()
                ValidateSuscriptions()
                FilterFormBySuscription()
                Me.Show()
            Else
                Me.Close()
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Funcion para cerrar los formularios abiertos
    ''' </summary>
    Private Sub CloseOpenForms()

        For Each f As Form In Me.MdiChildren
            f.Close()
            f.Dispose()
        Next

        For i As Integer = Application.OpenForms.Count - 1 To 0 Step -1
            Dim frm As Form = Application.OpenForms(i)
            ' Evitamos cerrar el formulario MDI principal
            If frm IsNot Me AndAlso frm.Owner Is Me Then
                frm.Close()
                frm.Dispose()
            End If

        Next
    End Sub

    ''' <summary>
    ''' Evento click del boton de cerrar session
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDBtnCerrarSession_Click(sender As Object, e As EventArgs) Handles INDBtnCerrarSession.Click
        PceConfig.ClosePopup()
        If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesSeguroCerrarSesion, Eform.Comunes), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Me.LogoutUser()
        End If
    End Sub

    ''' <summary>
    ''' Metodo para Cerrar session
    ''' </summary>
    Public Sub LogoutUser()
        Me.SafeInvoke(Sub()

                          If TokenManager.Instance.IsTokenExpired() Then
                              ' Si hay un manejador de logout registrado, llamarlo
                              MessageIndigo.Show("Token expirado. Se ha cerrado la sesión automáticamente.", MessageType.Warning, obtenerRecurso(Eresources.Advertencia))
                          End If

                          PceConfig.ClosePopup()
                          validatedSuscription = False
                          CloseOpenForms()
                          Infrastructure.Data.Xpo.XpoServiceEx.DisposeInstance()
                          FolderAccesible = True
                          _flagLockedSession = False
                          Me.ClearCache()
                          ClearCacheReportCache()
                          If Window.LoaderConfigurationFile.ConfigurationFileExists() Then
                              OpenLoginFrm()
                          Else
                              OpenLoginFrmPass(True)
                          End If
                          LogoutManager.Reset()
                      End Sub)
    End Sub

    ''' <summary>
    ''' Evento click del boton de cambiar contraseña
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDBtnSegPassword_Click(sender As Object, e As EventArgs) Handles INDBtnSegPassword.Click
        PceConfig.ClosePopup()
        AbrirFormCambiarContraseña(False)
    End Sub

    Private _flagLockedSession As Boolean

    ''' <summary>
    ''' Evento Clic en el boton bloquear session del mdi
    ''' </summary>
    Private Sub INDBtnBloqueoSession_Click(sender As Object, e As EventArgs) Handles INDBtnBloqueoSession.Click
        PceConfig.ClosePopup()
        _flagLockedSession = True
        LoginFrm()
    End Sub
    Private Sub LoginFrm()
        If Window.LoaderConfigurationFile.ConfigurationFileExists() Then
            OpenLoginFrm()
        Else
            Me.LoadNotificationCenter()
            Using Mensaje As New Mensajes
                Mensaje.Registrar(Me)
            End Using
            OpenLoginFrmPass(False)
        End If
    End Sub

    ''' <summary>
    ''' Evento click del boton de eliminar personalizacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnCleanCustom_Click(sender As Object, e As EventArgs) Handles INDbtnCleanCustom.Click
        Try
            If MessageIndigo.Show("Está seguro que desea eliminar las preferencias de usuario?", MessageType.Question, "Eliminar Preferencias", Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Window.Utils.DeleteThemeSkinName(indigo.UserIndigo)
                Me.flagThemeChanged = True
                Me.SetThemeSkinDefault()
                Window.Utils.DeletePrintProfiles(indigo.UserIndigo)
                Window.Utils.DeleteCustomization(indigo.UserIndigo)
                Window.Utils.DeleteCustomizationEHR(indigo.UserIndigoName)
                'Lanzamos el evento para restaurar todos los formularios
                OnCustomizationDeleted()
                MessageIndigo.Show(obtenerRecurso(Eresources.ComunesEliminado), MessageType.Information, obtenerRecurso(Eresources.ComunesIndigoCrystal))
            End If
        Catch ex As Exception
            MessageIndigo.Show(obtenerRecurso(Eresources.ComunesError), MessageType.Errores, obtenerRecurso(Eresources.ComunesIndigoCrystal))
        End Try
    End Sub

    ''' <summary>
    ''' Aqui se muestran los temas disponibles
    ''' </summary>
    Private Sub BtnThemeSkins_CheckedChanged(sender As Object, e As EventArgs) Handles BtnThemeSkins.CheckedChanged
        If Me.BtnThemeSkins.Checked Then
            Me.LyciPpgThemeSkins.Visibility = LayoutVisibility.Always
        Else
            Me.LyciPpgThemeSkins.Visibility = LayoutVisibility.Never
        End If
    End Sub

    Private Sub INDBtnConexionesERP_Click(sender As Object, e As EventArgs) Handles INDBtnConexionesERP.Click
        AbrirFormConfigurarConexion()
    End Sub

    ''' <summary>
    ''' Evento OnClick sobre el botón de token firma electronica
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnToken_Click(sender As Object, e As EventArgs) Handles INDbtnToken.Click
        AbrirFormElectronicSignatureToken()
    End Sub

    Private Sub FormMdi_Closing(sender As Object, e As FormClosingEventArgs) Handles MyBase.Closing
        If e.Cancel Then

        End If
    End Sub

    Private Sub INDbtnOpenForm_Click(sender As Object, e As EventArgs) Handles INDbtnUsers.Click, INDbtnRoll.Click, INDbtnChangePasswd.Click, SimpleButton8.Click, INDbtnGroups.Click, INDBtnConexionHIS.Click, SimpleButton13.Click
        Dim btnSender = CType(sender, SimpleButton)
        If btnSender.Tag = 1563 Then
            OpenForm(btnSender.Tag, Nothing, True, False)
        Else
            OpenForm(btnSender.Tag, Nothing, False, False)
        End If
    End Sub

    Private Sub INDBtnDesbloquearUsuario_Click(sender As Object, e As EventArgs) Handles INDBtnDesbloquearUsuario.Click
        AbrirFormDesbloquearUsuarios()
    End Sub

    ''' <summary>
    ''' Metodo onshow del formulario del mdi
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Overrides Sub OnShown(e As EventArgs)
        VerifyApplicationTheme()

        Using Mensaje As New Mensajes
            Mensaje.Registrar(Me)
        End Using

        If Not ApplicationSetting.Instance.LoginAzure Then
            Me.LoadNotificationCenter()
        End If

        INDPopupContainerConfiguration.Height = My.Computer.Screen.Bounds.Height - 70
        Me.INDPopupContainerDashBoard.Size = New Size(My.Computer.Screen.Bounds.Width, My.Computer.Screen.Bounds.Height - 45)
        Presentation.Base.BaseClass.FreeMemory()
        RibbonControl.ShowApplicationButtonContentControl()
        Me.RibbonControl_ApplicationButtonClick(INDBsvtiFavorites, New EventArgs())
    End Sub

    ''' <summary>
    ''' verifica el tema
    ''' </summary>
    Private Sub VerifyApplicationTheme()
        Try
            Me.flagThemeChanged = True
            Me.SetThemeSkinDefault()
            If ApplicationSetting.Instance.LoginAzure OrElse Infrastructure.CrossCutting.Base.Utils.DEFAULT_SKIN_NAME = DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName Then
                PpgThemeSkins_StyleChanged(Nothing, Nothing)
            End If
        Catch ex As Exception
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al cambiar la unidad operativa
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDglePermissionOperating_EditValueChanged(sender As Object, e As EventArgs) Handles INDglePermissionOperating.EditValueChanged
        If INDgleCompanies.GetSelectedDataRow IsNot Nothing Then
            Dim companyPermission = TryCast(INDgleCompanies.GetSelectedDataRow, Company)
            companyPermission.IdOperatingUnitDefault = CType(INDglePermissionOperating.EditValue, Integer)
            SessionValues.Instance.IndigoOperatingUnitId = companyPermission.IdOperatingUnitDefault
            Select Case SessionValues.Instance.UserType
                Case UserType.CompanyAdmin, UserType.StandardUser
                    Using modelPermission As New MmdiPrincipal()
                        Await modelPermission.SavePermissionUserAsync(companyPermission)
                    End Using
            End Select
            ''Actualizacion ruta de reportes
            UpdateReportPath(indigo.UserIndigoId, indigo.IndigoContainerId, INDglePermissionOperating.EditValue)
        End If
    End Sub


    ''' <summary>
    ''' Actualiza la ruta de reportes por usuario 
    ''' </summary>
    ''' <param name="idUser"></param>
    ''' <param name="idContainer"></param>
    ''' <param name="idOperatingUnit"></param>
    Private Sub UpdateReportPath(idUser As String, idContainer As Integer, idOperatingUnit As Integer)
        If ApplicationSetting.Instance.LoginAzure Then
            Dim cache As ReportCache = ReportCache.GetInstance()
            Dim cacheKey As String = $"{idUser}-{idContainer}-{idOperatingUnit}"
            Dim cachedReportPath As String = cache.GetValue(cacheKey)
            If String.IsNullOrEmpty(cachedReportPath) Then
                Using modelo = New PAutenticacionUsuario(Nothing)
                    Dim reportPath = modelo.GetReportPathByUser(idUser, idContainer, idOperatingUnit)
                    cache.SetValue(cacheKey, reportPath)
                    ConfigurationFile.Instance.ReportsPath = reportPath
                End Using
            Else
                ConfigurationFile.Instance.ReportsPath = cachedReportPath
            End If
        End If
    End Sub
    ''' <summary>
    ''' Evento que se ejecuta al abrir el popup de configuracion que se muestra en el MDI
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub PceConfig_Popup(sender As Object, e As EventArgs) Handles PceConfig.Popup
        ValidateLoadOperatingUnit()
    End Sub

    ''' <summary>
    ''' Abre el formulario de diseño
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsbDashboard_Click(sender As Object, e As EventArgs) Handles INDsbDashboard.Click
        Me.Cursor = Presentation.Base.BaseClass.ChangeCursorIndigo
        For Each f As Form In Me.MdiChildren
            If f.Name = "FrmDashboardDesigner" Then
                f.Activate()
                Me.Cursor = Cursors.Default
                Exit Sub
            End If
        Next
        Dim frmDashboard As New FrmDashboardDesigner()
        frmDashboard.MdiParent = Me
        frmDashboard.Show()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub PceConfig_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles PceConfig.QueryPopUp
        INDPopupContainerConfiguration.Height = Screen.PrimaryScreen.WorkingArea.Size.Height
    End Sub

    Private Sub BtnClearCache_Click(sender As Object, e As EventArgs) Handles BtnClearCache.Click
        Me.ClearCache()
    End Sub

    Private Sub INDbtnRolesEHR_Click(sender As Object, e As EventArgs) Handles INDbtnRolesEHR.Click
        OpenForm("2188", Nothing, False, False)
    End Sub

    Private Sub INDbtnGroupEHR_Click(sender As Object, e As EventArgs) Handles INDbtnGroupEHR.Click
        OpenForm("2189", Nothing, False, False)
    End Sub

    Private Sub INDbtnUsersEHR_Click(sender As Object, e As EventArgs) Handles INDbtnUsersEHR.Click
        OpenForm("2190", Nothing, False, False)
    End Sub

    Private Sub INDbtnAuthorizationEHR_Click(sender As Object, e As EventArgs) Handles INDbtnAuthorizationEHR.Click
        OpenForm("2055", Nothing, False, False)
    End Sub

    Private Sub INDbtnAuditoriaHC_Click_1(sender As Object, e As EventArgs) Handles INDbtnAuditoriaHC.Click
        OpenForm("2066", Nothing, False, False)
    End Sub

    Private Sub BvtVerticalSaludAsistencial_ItemPressed(sender As Object, e As BackstageViewItemEventArgs) Handles BvtVerticalSaludAsistencial.ItemPressed
        'Using Model As New MmdiPrincipal()
        '	For Each cmObj As Domain.Entities.CMConfiguration In Model.ListAllCMConfig()
        '		If cmObj IsNot Nothing Then
        '			With cmObj
        '				If (.ManageMS = 0) Then
        '					BvtCentralMezclas.Visible = False
        '				Else
        '					BvtCentralMezclas.Visible = True
        '				End If
        '			End With
        '		End If
        '	Next cmObj
        'End Using
    End Sub

    Private Sub INDbtnCrearReportes_Click(sender As Object, e As EventArgs) Handles INDbtnCrearReportes.Click
        OpenForm("1688", Nothing, False, False)
    End Sub

    Private Sub INDbtnReportesUsuario_Click(sender As Object, e As EventArgs) Handles INDbtnReportesUsuario.Click
        OpenForm("1687", Nothing, False, False)
    End Sub

    Private Async Sub INDPceChangePerfil_Click(ByVal sender As Object, ByVal e As EventArgs) Handles INDPceChangePerfil.Click
        If (CInt(MyBase.MdiChildren.Length) <= 0) Then
            Using WorkspaceUserForm As New WorkspaceUser("FormMdi")
                Dim _ListServiceConfiguration As New List(Of ServiceConfiguration)
                WorkspaceUserForm.Usuario = New MAutenticacionUsuario.Usuario() With
                    {.Id = SessionValues.Instance.UserIndigoId,
                        .UserCode = SessionValues.Instance.UserIndigo,
                        .Email = SessionValues.Instance.UserEmail,
                        .ProfileType = SessionValues.Instance.ProfileType.GetHashCode.ToString,
                        .UserType = SessionValues.Instance.UserType.GetHashCode.ToString
                    }
                WorkspaceUserForm.UserConfig = UnifiedConfiguration.Instance.UserConfig
                WorkspaceUserForm.UsuarioEHR = UnifiedConfiguration.Instance.UsuarioEHR
                WorkspaceUserForm.Profesional = UnifiedConfiguration.Instance.Profesional
                If indigo.UserType = UserType.GlobalAdmin Then
                    WorkspaceUserForm.CompanyList = Me.listCompanyPermission
                ElseIf indigo.UserType = UserType.GlobalQa Then
                    WorkspaceUserForm.CompanyList = Me.listCompanyPermission.Where(Function(_Company) _Company.ProductionCompany = 0).ToList
                Else 'si no se filtra por tenant se debe ajustar el aceptar del formulario WorkspaceUserForm para ajustar el rol, el grupo del usuario y consultar los permisos de la compañia si es de otro tenant
                    WorkspaceUserForm.CompanyList = Me.listCompanyPermission.Where(Function(_Company) _Company.TenantId = indigo.TenantId).ToList
                End If
                Using modelo = New PAutenticacionUsuario(Nothing)
                    For Each _ServiceConfigurationId As Byte In WorkspaceUserForm.CompanyList.Select(Of Byte)(Function(c) c.ServiceConfigurationId).Distinct
                        If _ServiceConfigurationId <> 0 Then
                            _ListServiceConfiguration.Add(Await modelo.GetServiceConfigurationSeg(_ServiceConfigurationId))
                        End If
                    Next
                End Using
                'WorkspaceUserForm.ListServiceConfiguration = _ListServiceConfiguration
                WorkspaceUserForm.CargarEspacioTrabajoActual()
                If WorkspaceUserForm.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                    validatedSuscription = False
                    INDTeTenant.Text = UnifiedConfiguration.Instance.CompanySelected.TenantName
                    INDTeCompany.Text = UnifiedConfiguration.Instance.CompanySelected.Name
                    Me.CargarUnidadesOperativas(WorkspaceUserForm.CompanySelected)
                    indigo.ListFormPermission = Nothing
                    InitionMDIValues()
                    FilterFormBySuscription()
                    taskFormPermission = LoadListMenuAsync()
                End If
            End Using
        Else
            MessageIndigo.Show("Para cambiar de empresa primero termine lo que está haciendo y cierre todos los formularios", MessageType.Warning, "Vie ERP")
        End If
    End Sub

    Private Function CompareVersion() As Boolean

        Try
            Dim pathUpdate As String = ConfigurationFile.Instance.PathUpdates

            If String.IsNullOrEmpty(pathUpdate) Then
                Return True
            End If

            Dim ValidateFolder = New DirectoryInfo(pathUpdate)

            If ValidateFolder.Exists = False Then
                'Si no existe la ruta o es innaccesible, inactivo la bandera de ruta accesible para que no vuelva a ingresar a esta función, y retorno un true para evitar que el usuario tenga bloqueos
                FolderAccesible = False
                Return True
            End If

            Dim validateClient = pathUpdate & "\" & My.Application.Info.Version.ToString & ".erp"

            If File.Exists(validateClient) Then
                Return True
            Else
                Return False
            End If

            Return True
        Catch ex As UnauthorizedAccessException
            Return True
        End Try

    End Function

    Private Sub XtraTabbedMdiManager1_PageRemoved(sender As Object, e As DevExpress.XtraTabbedMdi.MdiTabPageEventArgs) Handles XtraTabbedMdiManager1.PageRemoved
        If XtraTabbedMdiManager1.Pages.Count = 0 Then
            RibbonControl.ShowApplicationButtonContentControl()
        End If
    End Sub

    ''' <summary>
    ''' cuando se cierra el formulario base (se cierra el aplicativo) se manda a limpiar la cache del usuario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FormMdi_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.ClearCache()
        ClearCacheReportCache()
    End Sub

    ''' <summary>
    ''' Obtiene instancia de clase report y limpia cache
    ''' </summary>
    Private Sub ClearCacheReportCache()
        Dim cache As ReportCache = ReportCache.GetInstance()
        cache.Clear()
    End Sub

#End Region

End Class