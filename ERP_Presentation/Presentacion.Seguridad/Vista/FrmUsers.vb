'***********************************************************************
' Assembly         : Presentacion.Seguridad.MVP
' Author           : Jorge Leonardo Vernaza 
' Created          : 16-07-2013
'
' Last Modified By :
' Last Modified On :
' Description      :
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Importaciones"
Imports DevExpress.XtraEditors
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Security.MVP
Imports Presentation.CloudAgent.IndigoReference.Security
Imports Presentation.Controls
Imports DevExpress.XtraEditors.Controls
Imports Domain.Base.Entities
Imports DevExpress.Utils
Imports System.ComponentModel
Imports Domain.Security.Entities
Imports System.Threading
Imports System.Data
Imports Infrastructure.CrossCutting.Base
Imports System.IO
Imports Infrastructure.CrossCutting.Exceptions
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports System.Threading.Tasks
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo.SecurityRepository
Imports Infrastructure.Data.Xpo

#End Region

''' <summary>
''' Clase que contiene propiedades y metodos para controlar comnportamientos de la vista
''' </summary>
Public Class FrmUsers
    Implements IUsuario

#Region "Variables y Load "
    Dim ListPermissionCompanies As List(Of GenesisPermissionCompanies)
    ''' <summary>
    ''' Evento que se utiliza para cargar las definiciones del funcional centro de atencion
    ''' </summary>
    WithEvents CargarDefinicionesAsincronas As BackgroundWorker

    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim presenter As PUsuario
    ''' <summary>
    ''' Variable para instanciar la entidad SeguridadUsuario
    ''' </summary>
    Dim usuario As User
    ''' <summary>
    ''' Variable que maneja el listado de los telefonos agregados al control
    ''' </summary>
    Dim ListadoEliminadosTelefono As New List(Of Domain.Security.Entities.Phone)
    ''' <summary>
    '''  Variable que maneja el listado de los Direccions agregados al control
    ''' </summary>
    Dim ListadoEliminadosDireccion As New List(Of Domain.Security.Entities.Address)
    ''' <summary>
    '''  Variable que maneja el listado de los Email agregados al control
    ''' </summary>
    Dim ListadoEliminadosEmail As New List(Of Domain.Security.Entities.Email)
    ''' <summary>
    ''' Variable que contiene el correo del usuario 
    ''' </summary>
    Dim CorreoUsuario As String
    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Dim indigo As SessionValues = SessionValues.Instance
    ''' <summary>
    ''' bandera para saber que se esta cargando el registro y evitar que se disparen los eventos del editvalue
    ''' </summary>
    ''' <remarks></remarks>
    Dim _flagLoadControls As Boolean
    ''' <summary>
    ''' 
    ''' </summary>
    Dim _userConfig As UserConfiguration

    ''' <summary>
    ''' Listado de tipos de usuario administrador
    ''' </summary>
    Dim ListUserType As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Listado de tipos de perfil
    ''' </summary>
    Dim ListProfileType As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Listado de tipo de control de alerta
    ''' </summary>
    Dim ListTypeAlertControl As List(Of Tuple(Of Byte, String))

    ''' <summary>
    ''' 
    ''' </summary>
    Dim ListContainers As List(Of Domain.Security.Entities.Containers)

    ''' <summary>
    ''' Modo de visualizacion del formulario
    ''' </summary>
    Dim _ViewForm As Boolean
    ''' <summary>
    ''' vairable para conocer el estado de la consulta de los formularios de DB.
    ''' </summary>
    Dim TaskloadListForms As Task


    ''' <summary>
    ''' Listado de tipos de ruta de reportes
    ''' </summary>
    Private _listReportPathType As List(Of Tuple(Of Byte, String)) = {
        New Tuple(Of Byte, String)(1, "Usuario"),
        New Tuple(Of Byte, String)(2, "Unidad operativa")
    }.ToList()



    Private _dateFormatList As List(Of Tuple(Of Integer, String)) = {
        New Tuple(Of Integer, String)(0, "dd/MM/yyyy"),
        New Tuple(Of Integer, String)(1, "MM/dd/yyyy"),
        New Tuple(Of Integer, String)(2, "yyy/MM/dd"),
        New Tuple(Of Integer, String)(3, "dd/MMM/yyyy"),
        New Tuple(Of Integer, String)(4, "dd \de MMMM \de yyyy")
    }.ToList()

    Private _timerFormatList As List(Of Tuple(Of Integer, String)) = {
        New Tuple(Of Integer, String)(0, "Militar (H-i)"),
        New Tuple(Of Integer, String)(1, "Normal (g:i AM - PM)")
    }.ToList()

    ''' <summary>
    ''' Metodo Constructor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        InitializeComponent()
        _ViewForm = indigo.UserViewMode
        indigo.UserViewMode = True
        AdditionalControlPanel.Controls.Add(CtrFoto)
        IndigoGridControl1.SetHoldSize(INDgcPermissionCompanies, True)
    End Sub

    ''' <summary>
    ''' Evento LOAD Ejecuta accciones al cargar el fotmulario FRMUsuario
    ''' </summary>
    Private Sub frmUsuario_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        indigo.UserViewMode = _ViewForm
        LoadStatus()
        'Cargamos de manera asincrona definiciones del funcional
        RutaDefinicionesFuncionales = String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name, "\ModuloSeguridad.", Me.Name, ".xml")

        CargarDefinicionesAsincronas = New BackgroundWorker
        If CargarDefinicionesAsincronas.IsBusy = False Then
            CargarDefinicionesAsincronas.RunWorkerAsync()
        End If

        If indigo.IndigoCompanyLyncIntegration = True Then
            INDlygLyncIntegration.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDlygLyncIntegration.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
        presenter = New PUsuario(Me)
        presenter.BloquearControles()

        ListProfileType = New List(Of Tuple(Of Integer, String))
        ListProfileType.Add(New Tuple(Of Integer, String)(1, "Administrativo"))
        ListProfileType.Add(New Tuple(Of Integer, String)(2, "Asistencial"))
        INDGleProfile.Properties.DataSource = ListProfileType

        ListTypeAlertControl = New List(Of Tuple(Of Byte, String))
        ListTypeAlertControl.Add(New Tuple(Of Byte, String)(1, "Alert Windows"))
        ListTypeAlertControl.Add(New Tuple(Of Byte, String)(2, "Toast Notification"))
        INDGleTypeAlertControl.Properties.DataSource = ListTypeAlertControl

        CtrContacts.OpenByFormUser = True

        LoadUserType(indigo.UserType)
        LoadTuples()
        LoadTimeZones()
        EnableControlsLoginPASS()
        TaskloadListForms = presenter.ConsultarTodosForms()
        LoadActions()
    End Sub

    Private Sub LoadActions()
        If indigo.UserType = UserType.GlobalQa Then
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Nuevo) = True
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = True
        End If
    End Sub

    Private Sub LoadTimeZones()
        INDSleTimeZone.Properties.DataSource = XpoServiceEx.Instance(indigo.SecurityContainer).SecurityService.ListXPInstantFeedbackSource(Of TimeZoneXpo)()
    End Sub

    Private Sub LoadTuples()
        INDGleDateFormat.Properties.DataSource = _dateFormatList
        INDGleTimeFormat.Properties.DataSource = _timerFormatList
        INDGleReportPathType.Properties.DataSource = _listReportPathType
    End Sub

    ''' <summary>
    ''' Loads the status.
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="_userType"></param>
    Private Sub LoadUserType(_userType As UserType)
        ListUserType = New List(Of Tuple(Of Integer, String))
        If Not ApplicationSetting.Instance.LoginAzure Then
            Select Case _userType
                Case UserType.GlobalAdmin, UserType.TenantAdmin, UserType.CompanyAdmin, UserType.GlobalQa
                    ListUserType.Add(New Tuple(Of Integer, String)(0, "Usuario estándar"))
                    ListUserType.Add(New Tuple(Of Integer, String)(1, "Administrador empresa"))
                    ListUserType.Add(New Tuple(Of Integer, String)(2, "Administrador tenant"))
                    ListUserType.Add(New Tuple(Of Integer, String)(4, "Global Interno Qa"))
                Case Else
                    Return
            End Select
        Else
            Select Case _userType
                Case UserType.GlobalAdmin, UserType.GlobalQa
                    INDColTenant.VisibleIndex = 4
                    INDColTenant.Visible = True
                    ListUserType.Add(New Tuple(Of Integer, String)(0, "Usuario estándar"))
                    ListUserType.Add(New Tuple(Of Integer, String)(1, "Administrador empresa"))
                    ListUserType.Add(New Tuple(Of Integer, String)(2, "Administrador tenant"))
                    ListUserType.Add(New Tuple(Of Integer, String)(3, "Administrador global"))
                    ListUserType.Add(New Tuple(Of Integer, String)(4, "Global Interno Qa"))
                Case UserType.TenantAdmin
                    INDColTenant.VisibleIndex = 4
                    INDColTenant.Visible = True
                    ListUserType.Add(New Tuple(Of Integer, String)(0, "Usuario estándar"))
                    ListUserType.Add(New Tuple(Of Integer, String)(1, "Administrador empresa"))
                Case UserType.CompanyAdmin
                    ListUserType.Add(New Tuple(Of Integer, String)(0, "Usuario estándar"))
                Case Else
                    Return
            End Select
        End If

        INDGleUserType.Properties.DataSource = ListUserType
    End Sub
#End Region

#Region "Propiedades de la Interfaz"
    ''' <summary>
    ''' Gets or sets a value indicating whether [state].
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [state]; otherwise, <c>false</c>.
    ''' </value>
    Public Property State As Boolean
        Get
            Return BarraBotones.StatusRecord
        End Get
        Set(value As Boolean)
            If value = True Then
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
            Else
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Inactive
            End If
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que obtiene y establece el usuario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property User As User Implements IUsuario.User
        Get
            Return usuario
        End Get
        Set(value As User)
            usuario = value
        End Set
    End Property


    ''' <summary>
    ''' Establece un valor que indica si se debe habilitar el grupo contraseña.
    ''' </summary>
    Public WriteOnly Property HabilitarGrupoContraseña As Boolean Implements MVP.IUsuario.HabilitarGrupoContraseña
        Set(ByVal value As Boolean)
            If value = True Then
                INDlyiPassword.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyiPasswordAgain.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                INDlyiPassword.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyiPasswordAgain.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        End Set
    End Property

    Public ReadOnly Property CorreoElectronicoDelUsuario As String Implements IUsuario.CorreoElectronicoDelUsuario
        Get
            Return CtrContacts.Email(0)
        End Get
    End Property


    ''' <summary>
    ''' Esta propiedad contiene el codigo del usuario.
    ''' </summary>
    ''' <value></value>
    Public Property CodigoDelUsuario As String Implements IUsuario.CodigoDelUsuario
        Get
            Return INDbtnUserCode.Text
        End Get
        Set(ByVal value As String)
            INDbtnUserCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad contiene el nombre del usuario.
    ''' </summary>
    ''' <value>The nombre.</value>
    Public Property Nombre As String Implements IUsuario.NombreDelUsuario
        Get
            Return INDtxtFirstName.Text
        End Get
        Set(ByVal value As String)
            INDtxtFirstName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad contiene el template de la huella
    ''' </summary>
    ''' <value>The huella.</value>
    Public Property Huella As Byte() Implements IUsuario.Huella
        Get
            Return BarraBotones.Huella
        End Get
        Set(ByVal value As Byte())
            BarraBotones.Huella = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad contiene la contraseña del usuario.
    ''' </summary>
    ''' <value></value>
    Public Property ContraseñaDelUsuario As String Implements IUsuario.ContraseñaDelUsuario
        Get
            Return INDtxtPassword.Text
        End Get
        Set(ByVal value As String)
            INDtxtPassword.Text = value
        End Set
    End Property

    ''' <summary>
    ''' contiene la contraseña confirmada de usuario.
    ''' </summary>
    ''' <value>The confirmar contraseña.</value>
    Public Property ContraseñaConfirmada As String Implements IUsuario.ContraseñaConfirmada
        Get
            Return INDtxtPasswordAgain.Text
        End Get
        Set(ByVal value As String)
            INDtxtPasswordAgain.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad contiene el cargo asignado.
    ''' </summary>
    ''' <value>The cargo.</value>
    Public Property Cargo As String Implements IUsuario.Cargo
        Get
            Return INDMeUserPosition.Text
        End Get
        Set(ByVal value As String)
            INDMeUserPosition.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad Obtiene o establece un la fecha de caducidad de la contraseña.
    ''' </summary>
    ''' <value></value>
    Public Property FechaCaducidadContraseña As Date Implements MVP.IUsuario.FechaCaducidadContraseña
        Get
            If Not INDdaeAccountExpires.EditValue Is String.Empty Then
                Return CDate(INDdaeAccountExpires.EditValue)
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal value As Date)
            If value = CDate(#12:00:00 AM#) Then
                INDdaeAccountExpires.Text = Nothing
            Else
                INDdaeAccountExpires.EditValue = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para exigir un cambio de contraseña.
    ''' </summary>
    ''' <value></value>
    Public Property ExigirCambioContraseña As Boolean Implements MVP.IUsuario.ExigirCambioContraseña
        Get
            Return CBool(INDrbgChangePasswordNext.EditValue)
        End Get
        Set(ByVal value As Boolean)
            INDrbgChangePasswordNext.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad obtiene o establece un valor del tiempo de caducidad de la contraseña
    ''' </summary>
    ''' <value></value>
    Public Property TiempoCaducidadContraseña As String Implements MVP.IUsuario.TiempoCaducidadContraseña
        Get
            Return INDtxtPasswordExpiresDays.Text.ToString
        End Get
        Set(ByVal value As String)
            INDtxtPasswordExpiresDays.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad contiene el ValueMember del rol que se carga en el gridlookup.
    ''' </summary>
    ''' <value></value>
    Public Property RolValueMember As Integer? Implements IUsuario.RolValueMember
        Get
            Return INDgleUserRoll.EditValue
        End Get
        Set(ByVal value As Integer?)
            INDgleUserRoll.EditValue = value
        End Set
    End Property


    ''' <summary>
    ''' Esta propiedad establece Mesajes de informacion en el visor de eventos.
    ''' </summary>
    ''' <value>The mensaje.</value>
    Public WriteOnly Property Mensaje(ByVal Icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
        Set(ByVal value As String)

            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad contiene el ValueMember al cargar el grupo al que pertenece el USUARIO.
    ''' </summary>
    Public Property GrupoValueMember As String Implements IUsuario.GrupoValueMember
        Get
            Return INDgleUserGroup.EditValue.ToString
        End Get
        Set(ByVal value As String)
            INDgleUserGroup.EditValue = value

        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad hace el datasource al gridlookup de los GRUPOS.
    ''' </summary>
    Public WriteOnly Property FuenteDEDatosGrupos As Object Implements IUsuario.FuenteDEDatosGrupos
        Set(ByVal value As Object)
            INDgleUserGroup.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad Obtiene o establece un valor al seleccionar una fila de la rejilla de centros de atencion 
    ''' </summary>
    ''' 
    Public Property CentroAtencionFilaSeleccionada As Integer Implements MVP.IUsuario.CentroAtencionFilaSeleccionada
        Get
            Return INDglevUserGroup.FocusedRowHandle
        End Get
        Set(ByVal value As Integer)
            INDglevUserGroup.FocusedRowHandle = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor que controla las acciones de los controles del frontal activa,desactivar,limpiar.
    ''' </summary>
    ''' <value></value>
    Public WriteOnly Property AccionesSobreLosControles As Boolean Implements IUsuario.AccionesSobreLosControles
        Set(ByVal value As Boolean)
            INDlycUsers.BeginUpdate()
            '   INDGrupoAcciones.Enabled = value
            BarraBotones.StatusRecordVisible = value
            INDTxeEmail.Enabled = Not value
            INDMeUserPosition.Enabled = value
            INDtxtFirstName.Enabled = value
            INDtxtUserFirstLastName.Enabled = value
            INDtxtUserSecondName.Enabled = value
            INDtxtUserSecondLastName.Enabled = value
            INDtxtCodeInterface.Enabled = value
            INDgleUserGroup.Enabled = value
            INDgleUserRoll.Enabled = value
            INDpceContactData.Enabled = value
            INDGleUserType.Enabled = value
            INDGleProfile.Enabled = value
            INDGleTypeAlertControl.Enabled = value
            INDlygSecurityData.Enabled = value
            INDtxtPasswordExpiresDays.Enabled = False
            INDdaeAccountExpires.Enabled = False
            'INDbtnUserCode.Enabled = Not value
            If Not ApplicationSetting.Instance.LoginAzure Then
                INDtxtPassword.Enabled = value
                INDtxtPasswordAgain.Enabled = value
            End If
            INDrbgUserTypeIdentification.Enabled = value
            INDrbgUserGender.Enabled = value
            INDtxtUserIdentification.Enabled = value
            INDLightweightVersion.Enabled = value
            INDgleCity.Enabled = value
            INDPathReportServerBte.Enabled = value
            INDImageComboLenguaje.Enabled = value
            INDRgShowThemeSkin.Enabled = value
            INDGleTypeAlertControl.Enabled = value
            INDRgReporteadorActivo.Enabled = value
            INDgcPermissionCompanies.Enabled = value
            INDSleTenant.Enabled = value
            INDSleRole.Enabled = value
            INDGleGroup.Enabled = value
            INDSmbAddTenant.Enabled = value
            INDSleCompanies.Enabled = value
            INDRgManageCompany.Enabled = value
            INDSmbAddPermissionCompany.Enabled = value
            INDGleDateFormat.Enabled = value
            INDGleTimeFormat.Enabled = value
            INDSleTimeZone.Enabled = value
            INDGleReportPathType.Enabled = value
            'BarraBotones.StatusRecord = True
            If value = True Then
                INDrbgUserTypeIdentification.Focus()
            End If
            INDlycUsers.EndUpdate()
        End Set
    End Property

    ''' <summary>
    ''' Variable que retorna el valor de la propiedad ActivarGrupoSeguridad.
    ''' </summary>
    Private _ActivarGrupoSeguridad As Boolean

    ''' <summary>
    ''' establece el valor para activa o desactivar el grupo seguridad.
    ''' </summary>
    ''' <value></value>
    Public Property ActivarGrupoSeguridad As Boolean Implements MVP.IUsuario.ActivarGrupoSeguridad
        Get
            Return _ActivarGrupoSeguridad
        End Get
        Set(ByVal value As Boolean)
            _ActivarGrupoSeguridad = value
            INDlygSecurityData.Enabled = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que me obtiene la fecha de caducidad de la contraseña del usuario .
    ''' </summary>
    Private _FechaCaducidad As Date
    ''' <summary>
    ''' Obtiene o establece la fecha de caducidad.	
    ''' </summary>
    ''' <value>la fecha caducidad.</value>
    ''' <remarks></remarks>
    Public Property FechaCaducidad As Date Implements MVP.IUsuario.FechaCaducidad
        Get
            Return _FechaCaducidad
        End Get
        Set(ByVal value As Date)
            _FechaCaducidad = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que obtiene o establece la fecha del servidor.
    ''' </summary>
    Private _FechaServidor As Date
    ''' <summary>
    ''' Obtiene o establece la fecha servidor.	
    ''' </summary>
    ''' <value>la fecha servidor.</value>
    ''' <remarks></remarks>
    Public Property FechaServidor As Date Implements MVP.IUsuario.FechaServidor
        Get
            Return _FechaServidor
        End Get
        Set(ByVal value As Date)
            _FechaServidor = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el foco del cursor.	
    ''' </summary>
    Public WriteOnly Property EstablecerFoco(ByVal NombreControl As String) As Boolean Implements MVP.IUsuario.EstablecerFoco
        Set(ByVal value As Boolean)
            For Each obj As Control In INDlycUsers.Controls
                If TypeOf obj Is TextEdit Then
                    If obj.Name = NombreControl Then obj.Focus()
                End If
            Next
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property TenantId As Short? Implements IUsuario.TenantId
        Get
            Return INDSleTenant.EditValue
        End Get
        Set(value As Short?)
            INDSleTenant.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property RollId As Integer? Implements IUsuario.RollId
        Get
            Return INDSleRole.EditValue
        End Get
        Set(value As Integer?)
            INDSleRole.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property GroupId As Integer? Implements IUsuario.GroupId
        Get
            Return INDGleGroup.EditValue
        End Get
        Set(value As Integer?)
            INDGleGroup.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property CompanyId As Integer? Implements IUsuario.CompanyId
        Get
            Return INDSleCompanies.EditValue
        End Get
        Set(value As Integer?)
            INDSleCompanies.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property Tenants As DevExpress.Xpo.XPServerCollectionSource Implements IUsuario.Tenants
        Get
            Return CType(INDSleTenant.Properties.DataSource, DevExpress.Xpo.XPServerCollectionSource)
        End Get
        Set(value As DevExpress.Xpo.XPServerCollectionSource)
            INDSleTenant.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Lista los roles consultados por tenant y tipo de rol
    ''' </summary>
    ''' <returns></returns>
    Public Property TenantRolls As List(Of TenantRollXpo) Implements IUsuario.TenantRolls
        Get
            Return CType(INDSleRole.Properties.DataSource, List(Of TenantRollXpo))
        End Get
        Set(value As List(Of TenantRollXpo))
            INDSleRole.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Lista los grupos consultados por tenant y tipo de grupo
    ''' </summary>
    ''' <returns></returns>
    Public Property TenantGroups As List(Of TenantGroupXpo) Implements IUsuario.TenantGroups
        Get
            Return CType(INDGleGroup.Properties.DataSource, List(Of TenantGroupXpo))
        End Get
        Set(value As List(Of TenantGroupXpo))
            INDGleGroup.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property Ciudad As String Implements IUsuario.Ciudad
        Get
            Return CStr(INDgleCity.EditValue)
        End Get
        Set(value As String)
            INDgleCity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que obtiene o establece el idioma de la aplicacion.
    ''' </summary>
    ''' <value></value>
    Public Property Idioma As String Implements IUsuario.Idioma
        Get
            If INDImageComboLenguaje.EditValue Is Nothing Then
                Return String.Empty
            Else
                Return INDImageComboLenguaje.EditValue.ToString
            End If
        End Get
        Set(ByVal value As String)
            INDImageComboLenguaje.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna un valor que indica si se usa la versión liviana
    ''' </summary>
    ''' <value>Valor que indica si se usa la versión liviana</value>
    ''' <returns>Un valor que indica si se usa la versión liviana</returns>
    Public Property LightweightVersion As Boolean Implements IUsuario.LightweightVersion
        Get
            Return Me.INDLightweightVersion.EditValue
        End Get
        Set(value As Boolean)
            Me.INDLightweightVersion.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' esta propiedad sirve para escribir la ruta de la carpeta en la cual vamos a guardar las definiciones de reportes personalizados
    ''' </summary>
    Public Property RutaReportesPersonalizados As String Implements IUsuario.RutaReportesPersonalizados
        Get
            Return INDPathReportServerBte.Text
        End Get
        Set(ByVal value As String)
            INDPathReportServerBte.Text = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property ReporteadorActivo As Boolean Implements IUsuario.ReporteadorActivo
        Get
            Return Me.INDRgReporteadorActivo.EditValue
        End Get
        Set(value As Boolean)
            Me.INDRgReporteadorActivo.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property ShowThemeSkin As Boolean Implements IUsuario.ShowThemeSkin
        Get
            Return Me.INDRgShowThemeSkin.EditValue
        End Get
        Set(value As Boolean)
            Me.INDRgShowThemeSkin.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad contiene el email del usuario.
    ''' </summary>
    ''' <value></value>
    Public Property Email As String Implements IUsuario.Email
        Get
            Return INDTxeEmail.Text
        End Get
        Set(ByVal value As String)
            INDTxeEmail.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para recuperar el tipo de usuario
    ''' </summary>
    ''' <returns></returns>
    Public Property PUserType() As Integer? Implements IUsuario.PUserType
        Get
            Return INDGleUserType.EditValue
        End Get
        Set(ByVal value As Integer?)
            INDGleUserType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para recuperar el tipo de perfil
    ''' </summary>
    ''' <returns></returns>
    Public Property PProfileType() As Integer? Implements IUsuario.PProfileType
        Get
            Return INDGleProfile.EditValue
        End Get
        Set(ByVal value As Integer?)
            INDGleProfile.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para recuperar el tipo de control de alerta
    ''' </summary>
    ''' <returns></returns>
    Public Property PTypeAlertControl() As Byte? Implements IUsuario.PTypeAlertControl
        Get
            Return INDGleTypeAlertControl.EditValue
        End Get
        Set(ByVal value As Byte?)
            INDGleTypeAlertControl.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo para ruta de reportes
    ''' </summary>
    ''' <returns></returns>
    Public Property ReportPathType As Byte? Implements IUsuario.ReportPathType
        Get
            Return INDGleReportPathType.EditValue
        End Get
        Set(value As Byte?)
            INDGleReportPathType.EditValue = value
        End Set
    End Property

#End Region

#Region "Metodos de la Interfaz"

    ''' <summary>
    ''' Metodo que se utiliza para limpiar los campos del funcional.
    ''' </summary>
    Private Sub Limpiar()
        INDlycUsers.BeginUpdate()
        BarraBotones.LimpiarBiometrico()
        BarraBotones.CleanAuditBasic()
        CtrFoto.LimpiarControles()
        INDTxeEmail.Text = String.Empty
        INDbtnUserCode.Text = String.Empty
        INDtxtFirstName.Text = String.Empty
        INDtxtUserSecondName.Text = String.Empty
        INDtxtUserFirstLastName.Text = String.Empty
        INDtxtUserSecondLastName.Text = String.Empty
        INDMeUserPosition.Text = String.Empty
        INDtxtCodeInterface.Text = String.Empty
        INDgleUserRoll.EditValue = Nothing
        INDgleUserRoll.Properties.NullText = String.Empty
        INDgleUserGroup.EditValue = Nothing
        INDgleUserGroup.Properties.NullText = String.Empty
        INDtxtPassword.Text = String.Empty
        INDtxtPasswordAgain.Text = String.Empty
        INDtxtPasswordExpiresDays.Text = String.Empty
        PUserType = Nothing
        PProfileType = Nothing
        PTypeAlertControl = Nothing
        INDrbgChangePasswordNext.EditValue = False
        INDdaeAccountExpires.EditValue = Nothing
        INDdaeAccountExpires.Text = String.Empty
        AccionesSobreLosControles = False
        INDtxtAddresLync.Text = String.Empty
        INDtxtPasswordLync.Text = String.Empty
        INDtxtUserLync.Text = String.Empty
        INDtxtUserIdentification.Text = String.Empty
        INDrbgUserGender.EditValue = Nothing
        INDrbgUserTypeIdentification.EditValue = Nothing
        INDgcPermissionCompanies.DataSource = Nothing
        INDGcTenantUser.DataSource = Nothing
        INDSleCompanies.Properties.DataSource = Nothing

        INDGleReportPathType.EditValue = Nothing
        INDgleCity.EditValue = Nothing
        RutaReportesPersonalizados = ""
        Idioma = ""
        LightweightVersion = False
        ReporteadorActivo = False
        INDSleTimeZone.EditValue = Nothing
        ShowThemeSkin = False
        PTypeAlertControl = False
        INDGleDateFormat.EditValue = Nothing
        INDGleTimeFormat.EditValue = Nothing

        Nombre = String.Empty
        ListPermissionCompanies = New List(Of GenesisPermissionCompanies)
        TenantId = Nothing
        INDSleTenant.Properties.NullText = Nothing
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        INDlycUsers.EndUpdate()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="_userId"></param>
    Private Async Sub GetUserConfiguration(_userId As Integer)
        Using modelo = New PAutenticacionUsuario(New MAutenticacionUsuario.ParametrosAzureFunctions() With {.AppFunctionURL = ApplicationSetting.Instance.AppFunctionURL})
            Dim respuesta = Await modelo.GetUserConfigurationByUserId(_userId)
            If respuesta.Fallo Then
                _userConfig = New UserConfiguration With {
                    .LightweightVersion = False,
                    .ActualCity = 368148,
                    .LanguageCulture = "es-CO",
                    .CustomReportPath = "C:\Reportes\",
                    .ReporteadorActivo = True,
                    .ShowThemeSkinSelector = True,
                    .DateFormat = 0,
                    .TimeFormat = 0,
                    .TypeAlertControl = 1,
                    .ReportPathType = 1
                }
            Else
                _userConfig = respuesta.Result
            End If

            LightweightVersion = _userConfig.LightweightVersion
            Ciudad = _userConfig.ActualCity
            Idioma = _userConfig.LanguageCulture
            RutaReportesPersonalizados = _userConfig.CustomReportPath
            ReporteadorActivo = _userConfig.ReporteadorActivo
            ShowThemeSkin = _userConfig.ShowThemeSkinSelector
            INDGleDateFormat.EditValue = _userConfig.DateFormat
            INDGleTimeFormat.EditValue = _userConfig.TimeFormat
            INDSleTimeZone.EditValue = _userConfig.IdTimezone
            INDGleReportPathType.EditValue = _userConfig.ReportPathType
            If _userConfig.TypeAlertControl Is Nothing Then
                _userConfig.TypeAlertControl = 1
            End If
            PTypeAlertControl = _userConfig.TypeAlertControl
            'usuario.UserConfiguration.Add(_userConfig)
        End Using
    End Sub

    Private tokenPermissionCompany As CancellationTokenSource

    ''' <summary>
    ''' Verifica que exista el usuario por el email
    ''' </summary>
    Private Async Sub ConsultUserByEmail()
        Dim _User As User = Nothing
        Using modelo As New MUsuario
            _User = Await modelo.GetUserByEmail(INDTxeEmail.Text.ToString.Trim)
        End Using

        If _User Is Nothing OrElse _User.Id = 0 Then
            INDbtnUserCode.Text = Nothing
            Await CargarControles()
        Else
            INDbtnUserCode.Text = _User.UserCode
            Await CargarControles()
        End If
    End Sub

    ''' <summary>
    '''  metodo que se utiliza para consultar el usuario y cargar los controles con la informacion del usuario
    ''' </summary>
    Private Async Function CargarControles() As Tasks.Task
        If Me.BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
            Exit Function
        End If
        INDlycUsers.BeginUpdate()
        Using model = New MUsuario
            AsyncLoader(True)

            If INDbtnUserCode.Text Is Nothing OrElse INDbtnUserCode.Text = String.Empty Then
                usuario = New User
            Else
                usuario = Await model.ConsultarUsuario(INDbtnUserCode.Text, INDTxeEmail.Text)
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), usuario.CreationUser)
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), usuario.CreationDate)
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), usuario.ModificationUser)
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), usuario.ModificationDate)
            End If

            If Object.Equals(usuario, Nothing) = True Then
                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                AsyncLoader(True)
                Exit Function
            ElseIf usuario.Id > 0 Then
                Select Case indigo.UserType
                    Case UserType.GlobalAdmin

                    Case UserType.TenantAdmin
                        Select Case CType(usuario.UserType, UserType)
                            Case UserType.GlobalAdmin, UserType.TenantAdmin
                                Mensaje(EeventViewerImages.Advertencia) = String.Format("El usuario existe pero no puede modificarlo. {0}", obtenerRecurso(ComunesContacteAdministrador))
                                INDTxeEmail.Text = Nothing
                                AsyncLoader(False)
                                Exit Function
                            Case UserType.CompanyAdmin
                            Case UserType.StandardUser

                        End Select

                        'elimina de memoria los tenant del usuario consultado que no correspondan a los tenant del usuario logeado
                        Dim _tenantUsers = From tu In usuario.TenantUsers.Where(Function(tu) tu.State)
                                           Group Join t In UnifiedConfiguration.Instance.ListCompanies.Select(Function(c) c.TenantId).Distinct On t Equals tu.TenantId Into Group
                                           From t In Group.DefaultIfEmpty()
                                           Where t = 0
                                           Select tu
                        For Each _TenantUser In _tenantUsers.ToList
                            usuario.TenantUsers.Remove(_TenantUser)
                        Next
                        If usuario.TenantUsers.Count = 0 Then
                            Mensaje(EeventViewerImages.Advertencia) = String.Format("El usuario existe pero tiene permiso a otros tenant. {0}", obtenerRecurso(ComunesContacteAdministrador))
                            INDTxeEmail.Text = Nothing
                            AsyncLoader(False)
                            Exit Function
                        End If
                    Case UserType.CompanyAdmin
                        Select Case CType(usuario.UserType, UserType)
                            Case UserType.GlobalAdmin, UserType.TenantAdmin, UserType.CompanyAdmin
                                Mensaje(EeventViewerImages.Advertencia) = String.Format("El usuario existe pero no puede modificarlo. {0}", obtenerRecurso(ComunesContacteAdministrador))
                                INDTxeEmail.Text = Nothing
                                AsyncLoader(False)
                                Exit Function
                            Case UserType.StandardUser

                        End Select
                        'elimina de memoria los tenant del usuario consultado que no correspondan a los tenant del usuario logeado
                        Dim _tenantUsers = From tu In usuario.TenantUsers.Where(Function(tu) tu.State)
                                           Group Join t In UnifiedConfiguration.Instance.ListCompanies.Select(Function(c) c.TenantId).Distinct On t Equals tu.TenantId Into Group
                                           From t In Group.DefaultIfEmpty()
                                           Where t = 0
                                           Select tu
                        For Each _TenantUser In _tenantUsers.ToList
                            usuario.TenantUsers.Remove(_TenantUser)
                        Next
                        If usuario.TenantUsers.Count = 0 Then
                            Mensaje(EeventViewerImages.Advertencia) = String.Format("El usuario existe pero tiene permiso a otros tenant. {0}", obtenerRecurso(ComunesContacteAdministrador))
                            INDTxeEmail.Text = Nothing
                            AsyncLoader(False)
                            Exit Function
                        End If

                    Case UserType.StandardUser

                        Mensaje(EeventViewerImages.Advertencia) = String.Format("El usuario logeado no es administrador. {0}", obtenerRecurso(ComunesContacteAdministrador))
                        INDTxeEmail.Text = Nothing
                        AsyncLoader(False)
                        Exit Function
                End Select
            End If

            _flagLoadControls = True

            LoadCompanies()

            RefreshGridTenantUser()

            If ApplicationSetting.Instance.LoginAzure Then
                Dim _userId As Integer = 0

                If usuario IsNot Nothing Then
                    _userId = usuario.Id
                End If
                GetUserConfiguration(_userId)
            End If

            If Object.Equals(usuario.UserCode, Nothing) = False Then
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                INDTxeEmail.Text = usuario.Email
                INDtxtFirstName.Text = usuario.Person.FirstName
                INDtxtUserSecondName.Text = usuario.Person.SecondName
                INDtxtUserFirstLastName.Text = usuario.Person.FirstLastName
                INDtxtUserSecondLastName.Text = usuario.Person.SecondLastName
                INDMeUserPosition.Text = usuario.Position

                INDtxtUserIdentification.Text = usuario.Person.Identification
                INDtxtCodeInterface.Text = usuario.CodeInterface
                INDgleUserRoll.EditValue = usuario.RollCode
                If usuario.Roll IsNot Nothing Then
                    INDgleUserRoll.Properties.NullText = usuario.Roll.Description
                End If
                INDgleUserGroup.EditValue = usuario.GroupCode
                If usuario.Group IsNot Nothing Then
                    INDgleUserGroup.Properties.NullText = usuario.Group.Description
                End If

                INDrbgChangePasswordNext.EditValue = CBool(usuario.ChangePassword)
                If CBool(INDrbgChangePasswordNext.EditValue) = False OrElse ApplicationSetting.Instance.LoginAzure Then
                    INDtxtPasswordExpiresDays.Enabled = False
                    INDdaeAccountExpires.Enabled = False
                Else
                    INDtxtPasswordExpiresDays.Enabled = True
                    INDdaeAccountExpires.Enabled = True
                End If
                INDtxtPasswordExpiresDays.Text = usuario.DaysChangePassword.ToString
                INDdaeAccountExpires.EditValue = usuario.DateExpiryAccount
                INDrbgUserTypeIdentification.EditValue = CInt(usuario.Person.IdentificationType)
                INDrbgUserGender.EditValue = CInt(usuario.Person.Gender)
                PUserType = CInt(usuario.UserType)
                PProfileType = CInt(usuario.ProfileType)
                State = usuario.State
                'Control Datos de Contacto

                'Cargamos los agregados en asincrono
                loadAggregatesAsync()




                INDlyiPassword.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyiPasswordAgain.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDtxtUserIdentification.Enabled = False
                INDrbgUserTypeIdentification.Focus()
                INDtxtAddresLync.Text = usuario.AddressSingInLync
                INDtxtUserLync.Text = usuario.UserNameLync
                INDtxtPasswordLync.Text = usuario.PasswordLync
                '*****************Cargar Huella*****************************
                BarraBotones.Huella = usuario.Person.Fingerprint
                '  **********************************************************
                '*****************Foto*****************************
                LogicaBotonActualizar(True)
                If indigo.UserIndigo.Trim = usuario.UserCode.Trim Then
                    BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
                End If
                '**********************************************************
                AsyncLoader(False)
                AccionesSobreLosControles = True
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ControlBiometrico) = True
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = True
            Else
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                INDtxtFirstName.Focus()

                usuario.State = True
                usuario.Person = New Domain.Security.Entities.Person
                usuario.Person.FilePerson.Add(New Domain.Security.Entities.FilePerson)
                If Not ApplicationSetting.Instance.LoginAzure Then
                    INDlyiPassword.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyiPasswordAgain.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                End If

                usuario.ViewForm = False

                LogicaBotonActualizar(False)
                INDtxtUserIdentification.Enabled = True
                AsyncLoader(False)
                INDtxtUserIdentification.Focus()
            End If
            If indigo.UserType = UserType.GlobalQa Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Actualizar) = True
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = True
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Nuevo) = True
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = True
            End If
        End Using
        _flagLoadControls = False
        INDlycUsers.EndUpdate()
    End Function

    Private Sub loadAggregatesAsync()
        INDgcvPermissionCompanies.ShowLoadingPanel()
        tokenPermissionCompany = New CancellationTokenSource
        Task.Factory.StartNew(Sub()
                                  LoadUserCompanies()

                                  If Not tokenPermissionCompany.IsCancellationRequested Then
                                      INDgcPermissionCompanies.SafeInvoke(Sub()
                                                                              RefreshGridPermissionCompany()
                                                                              INDgcvPermissionCompanies.HideLoadingPanel()

                                                                          End Sub)
                                  Else
                                      INDgcPermissionCompanies.SafeInvoke(Sub()
                                                                              INDgcvPermissionCompanies.HideLoadingPanel()
                                                                          End Sub)
                                  End If
                              End Sub, tokenPermissionCompany)
        Task.Factory.StartNew(Async Sub()
                                  Using model As New MUsuario
                                      Dim permissionsUser = Await model.ConsultarTodosPermisosUsuarios(usuario.UserCode)
                                      permissionsUser.ForEach(Sub(p As PermissionUser)
                                                                  usuario.PermissionUser.Add(p)
                                                              End Sub)
                                  End Using
                              End Sub, tokenPermissionCompany)
        Task.Factory.StartNew(Sub()
                                  Using model As New MUsuario
                                      Dim phones As List(Of Domain.Security.Entities.Phone) = model.ConsultarTelefonosPersona(usuario.IdPerson)
                                      If phones IsNot Nothing Then
                                          phones.ForEach(Sub(p As Domain.Security.Entities.Phone)
                                                             usuario.Person.Phone.Add(p)
                                                         End Sub)
                                      End If
                                      If Not tokenPermissionCompany.IsCancellationRequested Then
                                          CtrContacts.SafeInvoke(Sub()
                                                                     CtrContacts.EstablecerDataSourceTelefono = usuario.Person.Phone.ToList
                                                                 End Sub)
                                      End If
                                  End Using
                              End Sub, tokenPermissionCompany)
        Task.Factory.StartNew(Sub()
                                  Using model As New MUsuario
                                      Dim mails As List(Of Domain.Security.Entities.Email) = model.ConsultarMailsPersona(usuario.IdPerson)
                                      mails.ForEach(Sub(m As Domain.Security.Entities.Email)
                                                        usuario.Person.Email.Add(m)
                                                    End Sub)
                                      If Not tokenPermissionCompany.IsCancellationRequested Then
                                          CtrContacts.SafeInvoke(Sub()
                                                                     CtrContacts.EstablecerDataSourceEmail = usuario.Person.Email.ToList
                                                                 End Sub)

                                      End If
                                  End Using
                              End Sub, tokenPermissionCompany)
        Task.Factory.StartNew(Sub()
                                  Using model As New MUsuario
                                      Dim filePersons As Domain.Security.Entities.FilePerson = model.ConsultarFilePerson(usuario.IdPerson)
                                      usuario.Person.FilePerson.Add(filePersons)
                                      If Not tokenPermissionCompany.IsCancellationRequested Then
                                          CtrFoto.SafeInvoke(Sub()

                                                                 If usuario.Person IsNot Nothing Then
                                                                     If usuario.Person.FilePerson IsNot Nothing Then
                                                                         If usuario.Person.FilePerson.Count > 0 Then
                                                                             CtrFoto.Foto = usuario.Person.FilePerson(0).Photo
                                                                         End If
                                                                     End If
                                                                 End If
                                                             End Sub)
                                      End If
                                  End Using
                              End Sub, tokenPermissionCompany)
        Task.Factory.StartNew(Sub()
                                  Using model As New MUsuario
                                      Dim address As List(Of Domain.Security.Entities.Address) = model.ConsultarDirecciones(usuario.IdPerson)
                                      address.ForEach(Sub(a)
                                                          usuario.Person.Address.Add(a)
                                                      End Sub)
                                      If Not tokenPermissionCompany.IsCancellationRequested Then
                                          CtrContacts.SafeInvoke(Sub()
                                                                     CtrContacts.EstablecerDataSourceDireccion = usuario.Person.Address.ToList
                                                                 End Sub)
                                      End If
                                  End Using
                              End Sub, tokenPermissionCompany)


    End Sub

    ''' <summary>
    ''' Función que verifica los campos obligatorios de un usuario y devuelve una lista con aquellos que no han sido completados
    ''' </summary>
    ''' <returns>True = valido Correctamente</returns>   
    Function ValidateFields() As List(Of String)

        Dim errors As New List(Of String)

        If String.IsNullOrWhiteSpace(INDTxeEmail.Text) Then
            errors.Add("El email del usuario no puede ser vacio")
        End If

        If String.IsNullOrWhiteSpace(INDtxtFirstName.Text) Then
            errors.Add("El primer nombre del usuario no puede ser vacio")
        End If

        If INDrbgUserGender.EditValue Is Nothing Then
            errors.Add("Debe Seleccionar el sexo del Usuario")
        End If

        If INDrbgUserTypeIdentification.EditValue Is Nothing Then
            errors.Add("Debe Seleccionar el tipo de identificacion")
        End If

        If String.IsNullOrWhiteSpace(INDtxtUserIdentification.Text) Then
            errors.Add("La Identificacion del usuario no puede ser vacio")
        End If

        If String.IsNullOrWhiteSpace(INDtxtUserFirstLastName.Text) Then
            errors.Add("El primer apellido del usuario no puede ser vacio")
        End If

        If PUserType Is Nothing Then
            errors.Add("Especifique el tipo de usuario")
        End If

        If PProfileType Is Nothing OrElse PProfileType = 0 Then
            errors.Add("Especifique perfil de usuario")
        End If

        If String.IsNullOrWhiteSpace(INDMeUserPosition.Text) Then
            errors.Add("El cargo del usuario no puede ser vacio")
        End If

        If INDLciTimeZone.Visible Then
            If INDSleTimeZone.EditValue Is Nothing Or INDSleTimeZone.EditValue = 0 Then
                errors.Add("La Zona horaria no puede estar vacia")
            End If
        End If

        If PUserType IsNot Nothing Then
            If CType(PUserType, UserType) = UserType.GlobalAdmin Then
                If INDgleUserRoll.EditValue Is Nothing Then
                    errors.Add("El rol de usuario no puede ser vacio")
                End If
                If Object.Equals(INDgleUserGroup.EditValue, Nothing) = True Then
                    errors.Add("El Grupo de usuario no puede ser vacio")
                End If
            End If
        End If


        '******************validaciones Contraseñas***********************
        If INDlyiPassword.Visible Then
            Me.IndigoTextEdit.SetCampoObligatorio(Me.INDtxtPassword, True)
            Me.IndigoTextEdit.SetCampoObligatorio(Me.INDtxtPasswordAgain, True)

            If String.IsNullOrWhiteSpace(INDtxtPassword.Text) Then
                errors.Add("Especifique una contraseña para el usuario")
            End If
            If String.IsNullOrWhiteSpace(INDtxtPasswordAgain.Text) Then
                errors.Add("Confirme la contraseña del usuario")
            End If
            If INDtxtPassword.Text.Length >= 7 Then
                If INDtxtPassword.Text.Trim <> INDtxtPasswordAgain.Text.Trim Then
                    errors.Add(obtenerRecurso(ContrasenasNoCoinciden, CambiarContrasena))
                End If
            Else
                errors.Add("La contraseña debe ser mayor o igual a 7 caracteres.")
            End If
        Else
            Me.IndigoTextEdit.SetCampoObligatorio(Me.INDtxtPassword, False)
            Me.IndigoTextEdit.SetCampoObligatorio(Me.INDtxtPasswordAgain, False)
        End If
        '****************** Termina validaciones Contraseñas***********************

        If CtrContacts.VerificaExisteDatos(CtrContactos.TipoContacto.Correos) <> "False" Then
            CorreoUsuario = CtrContacts.VerificaExisteDatos(CtrContactos.TipoContacto.Correos)
        Else
            CtrContacts.EstablecerError(CtrContactos.TipoContacto.Correos)
            INDpceContactData.ShowPopup()
        End If

        If PUserType IsNot Nothing Then
            Select Case CType(PUserType, UserType)
                Case UserType.GlobalAdmin
                    If usuario.TenantUsers IsNot Nothing AndAlso usuario.TenantUsers.Count > 0 Then
                        usuario.TenantUsers.Where(Function(_TenantUser) _TenantUser.Id = 0).ToList.ForEach(Sub(_TenantUser) usuario.TenantUsers.Remove(_TenantUser))
                        usuario.TenantUsers.Where(Function(_TenantUser) _TenantUser.Id > 0).ToList.ForEach(Sub(_TenantUser) _TenantUser.MarkAsDeleted)
                    End If
                    If usuario.PermissionUser IsNot Nothing AndAlso usuario.PermissionUser.Count > 0 Then
                        usuario.PermissionUser.Where(Function(_PermissionUser) _PermissionUser.Id = 0).ToList.ForEach(Sub(_PermissionUser) usuario.PermissionUser.Remove(_PermissionUser))
                        usuario.PermissionUser.Where(Function(_PermissionUser) _PermissionUser.Id > 0).ToList.ForEach(Sub(_PermissionUser) _PermissionUser.ActionValue = False)
                    End If
                    If usuario.PermissionCompany IsNot Nothing AndAlso usuario.PermissionCompany.Count > 0 Then
                        usuario.PermissionCompany.Where(Function(_PermissionCompany) _PermissionCompany.Id = 0).ToList.ForEach(Sub(_PermissionCompany) usuario.PermissionCompany.Remove(_PermissionCompany))
                        usuario.PermissionCompany.Where(Function(_PermissionCompany) _PermissionCompany.Id > 0).ToList.ForEach(Sub(_PermissionCompany) _PermissionCompany.MarkAsDeleted)
                    End If
                    If usuario.UserOperatingUnit IsNot Nothing AndAlso usuario.UserOperatingUnit.Count > 0 Then
                        usuario.UserOperatingUnit.Where(Function(_UserOperatingUnit) _UserOperatingUnit.Id = 0).ToList.ForEach(Sub(_UserOperatingUnit) usuario.UserOperatingUnit.Remove(_UserOperatingUnit))
                        usuario.UserOperatingUnit.Where(Function(_UserOperatingUnit) _UserOperatingUnit.Id > 0).ToList.ForEach(Sub(_UserOperatingUnit) usuario.UserOperatingUnit.Add(_UserOperatingUnit.MarkAsDeleted))
                    End If
                Case UserType.TenantAdmin
                    If (usuario.TenantUsers Is Nothing OrElse usuario.TenantUsers.Count = 0 OrElse
                        Not usuario.TenantUsers.Any(Function(tr) tr.ChangeTracker.State <> ObjectState.Deleted)) Then
                        errors.Add("Debe dar permiso al menos un tenant")
                    End If
                    If usuario.PermissionCompany IsNot Nothing AndAlso usuario.PermissionCompany.Count > 0 Then
                        usuario.PermissionCompany.Where(Function(_PermissionCompany) _PermissionCompany.Id = 0).ToList.ForEach(Sub(_PermissionCompany) usuario.PermissionCompany.Remove(_PermissionCompany))
                        usuario.PermissionCompany.Where(Function(_PermissionCompany) _PermissionCompany.Id > 0).ToList.ForEach(Sub(_PermissionCompany) _PermissionCompany.MarkAsDeleted)
                    End If
                    If usuario.UserOperatingUnit IsNot Nothing AndAlso usuario.UserOperatingUnit.Count > 0 Then
                        usuario.UserOperatingUnit.Where(Function(_UserOperatingUnit) _UserOperatingUnit.Id = 0).ToList.ForEach(Sub(_UserOperatingUnit) usuario.UserOperatingUnit.Remove(_UserOperatingUnit))
                        usuario.UserOperatingUnit.Where(Function(_UserOperatingUnit) _UserOperatingUnit.Id > 0).ToList.ForEach(Sub(_UserOperatingUnit) usuario.UserOperatingUnit.Add(_UserOperatingUnit.MarkAsDeleted))
                    End If
                Case UserType.CompanyAdmin
                    If (usuario.TenantUsers Is Nothing OrElse usuario.TenantUsers.Count = 0 OrElse
                        Not usuario.TenantUsers.Any(Function(tr) tr.ChangeTracker.State <> ObjectState.Deleted)) Then
                        errors.Add("Debe dar permiso al menos un tenant")
                    ElseIf (usuario.PermissionCompany Is Nothing OrElse usuario.PermissionCompany.Count = 0 OrElse
                        Not usuario.PermissionCompany.Any(Function(pc) pc.ChangeTracker.State <> ObjectState.Deleted)) Then
                        errors.Add(obtenerRecurso(UsuarioPermisoUnaEmpresa, Eform.Usuario))
                    ElseIf (usuario.PermissionCompany Is Nothing OrElse usuario.PermissionCompany.Count = 0 OrElse
                        Not usuario.PermissionCompany.Any(Function(pc) pc.ChangeTracker.State <> ObjectState.Deleted AndAlso pc.Administrator.GetValueOrDefault)) Then
                        errors.Add("Debe dar permiso de administrador al menos a una compañia")
                    ElseIf ApplicationSetting.Instance.LoginAzure AndAlso (usuario.PermissionCompany Is Nothing OrElse usuario.PermissionCompany.Count = 0 OrElse
                        usuario.PermissionCompany.Any(Function(pc) pc.ChangeTracker.State <> ObjectState.Deleted AndAlso (Not pc.Administrator.GetValueOrDefault) AndAlso
                        (usuario.UserOperatingUnit Is Nothing OrElse usuario.UserOperatingUnit.Count = 0 OrElse
                        Not usuario.UserOperatingUnit.Any(Function(uou) uou.IdContainer = pc.IdContainer AndAlso uou.ChangeTracker.State <> ObjectState.Deleted)))) Then
                        errors.Add("Debe dar permiso al menos a una unidad operativa por compañia")
                    ElseIf ApplicationSetting.Instance.LoginAzure AndAlso usuario.PermissionCompany.Any(Function(pc) pc.ChangeTracker.State <> ObjectState.Deleted AndAlso (Not pc.Administrator.GetValueOrDefault) AndAlso pc.IdOperatingUnitDefault = 0) Then
                        errors.Add("Debe seleccionar una unidad operativa por defecto para cada compañia donde no es administrador")
                    ElseIf (Not ApplicationSetting.Instance.LoginAzure) AndAlso (usuario.PermissionCompany Is Nothing OrElse usuario.PermissionCompany.Count = 0 OrElse
                    usuario.PermissionCompany.Any(Function(pc) pc.ChangeTracker.State <> ObjectState.Deleted AndAlso
                    (usuario.UserOperatingUnit Is Nothing OrElse usuario.UserOperatingUnit.Count = 0 OrElse
                    Not usuario.UserOperatingUnit.Any(Function(uou) uou.IdContainer = pc.IdContainer AndAlso uou.ChangeTracker.State <> ObjectState.Deleted)))) Then
                        errors.Add("Debe dar permiso al menos a una unidad operativa por compañia")
                    ElseIf (Not ApplicationSetting.Instance.LoginAzure) AndAlso usuario.PermissionCompany.Any(Function(pc) pc.ChangeTracker.State <> ObjectState.Deleted AndAlso pc.IdOperatingUnitDefault = 0) Then
                        errors.Add("Debe seleccionar una unidad operativa por defecto para cada compañia")
                    ElseIf ReportPathType = 2 AndAlso usuario.PermissionCompany.Any(Function(pc) pc.ChangeTracker.State <> ObjectState.Deleted AndAlso
                            usuario.UserOperatingUnit.Any(Function(uou) String.IsNullOrEmpty(uou.ReportPath) AndAlso
                            uou.IdContainer = pc.IdContainer AndAlso
                            uou.ChangeTracker.State <> ObjectState.Deleted)) Then
                        errors.Add("Debe seleccionar una ruta de reporte para cada unidad operativa por compañia")
                    End If



                Case UserType.StandardUser
                    If (usuario.TenantUsers Is Nothing OrElse usuario.TenantUsers.Count = 0 OrElse
                        Not usuario.TenantUsers.Any(Function(tr) tr.ChangeTracker.State <> ObjectState.Deleted)) Then
                        errors.Add("Debe dar permiso al menos un tenant")
                    ElseIf (usuario.PermissionCompany Is Nothing OrElse usuario.PermissionCompany.Count = 0 OrElse
                        Not usuario.PermissionCompany.Any(Function(pc) pc.ChangeTracker.State <> ObjectState.Deleted)) Then
                        errors.Add(obtenerRecurso(UsuarioPermisoUnaEmpresa, Eform.Usuario))
                    ElseIf (usuario.PermissionCompany Is Nothing OrElse usuario.PermissionCompany.Count = 0 OrElse
                        usuario.PermissionCompany.Any(Function(pc) pc.ChangeTracker.State <> ObjectState.Deleted AndAlso
                        (usuario.UserOperatingUnit Is Nothing OrElse usuario.UserOperatingUnit.Count = 0 OrElse
                        Not usuario.UserOperatingUnit.Any(Function(uou) uou.IdContainer = pc.IdContainer AndAlso uou.ChangeTracker.State <> ObjectState.Deleted)))) Then
                        errors.Add("Debe dar permiso al menos a una unidad operativa por compañia")
                    ElseIf usuario.PermissionCompany.Any(Function(pc) pc.ChangeTracker.State <> ObjectState.Deleted AndAlso pc.IdOperatingUnitDefault = 0) Then
                        errors.Add("Debe seleccionar una unidad operativa por defecto para cada compañia")
                    ElseIf ReportPathType = 2 AndAlso usuario.PermissionCompany.Any(Function(pc) pc.ChangeTracker.State <> ObjectState.Deleted AndAlso
                            usuario.UserOperatingUnit.Any(Function(uou) String.IsNullOrEmpty(uou.ReportPath) AndAlso
                            uou.IdContainer = pc.IdContainer AndAlso
                            uou.ChangeTracker.State <> ObjectState.Deleted)) Then
                        errors.Add("Debe seleccionar una ruta de reporte para cada unidad operativa por compañia")
                    End If
            End Select
        End If
        Return errors
    End Function

    Dim persona As Domain.Security.Entities.Person
    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Private Async Sub Guardar() Implements ICrudBase.Guardar
        BarraBotones.Focus()
        presenter.ConsultarHoraServidor()
        '******Validamos 
        Dim listErrors = ValidateFields()

        If ValidateFields() IsNot Nothing AndAlso ValidateFields.Any() Then
            Using formulario As New FrmListErrors(ValidateFields)
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(formulario, False)
                transparent.ShowDialog(Me)
            End Using
            Exit Sub
        End If

        '******** Informacion del usuario
        If User.Id = 0 Then
            usuario.Password = INDtxtPassword.Text.Trim
        End If
        usuario.Email = INDTxeEmail.Text.ToString.Trim
        usuario.UserCode = INDbtnUserCode.Text.ToString.Trim
        usuario.Person.Identification = INDtxtUserIdentification.Text
        usuario.IdPerson = usuario.Person.Id
        usuario.Person.Identification = INDtxtUserIdentification.Text
        usuario.Person.Fullname = String.Concat(INDtxtFirstName.Text.Trim, " ", If(INDtxtUserSecondName.Text.Trim = "", "", String.Concat(INDtxtUserSecondName.Text.Trim, " ")), INDtxtUserFirstLastName.Text.Trim, " ", INDtxtUserSecondLastName.Text.Trim)
        usuario.Person.FirstName = INDtxtFirstName.Text.Trim
        usuario.Person.SecondName = INDtxtUserSecondName.Text.Trim
        usuario.Person.FirstLastName = INDtxtUserFirstLastName.Text.Trim

        usuario.Person.IdentificationType = CShort(INDrbgUserTypeIdentification.EditValue)
        usuario.Person.Gender = CShort(INDrbgUserGender.EditValue)
        usuario.Person.SecondLastName = INDtxtUserSecondLastName.Text.Trim
        usuario.CodeInterface = INDtxtCodeInterface.Text
        usuario.Position = INDMeUserPosition.Text.ToString.Trim

        Dim userType = CType(PUserType, UserType)

        If userType = UserType.GlobalAdmin OrElse userType = UserType.GlobalQa Then
            usuario.RollCode = CInt(INDgleUserRoll.EditValue)
            usuario.GroupCode = CInt(INDgleUserGroup.EditValue)
        Else
            usuario.RollCode = usuario.TenantUsers.FirstOrDefault(Function(tu) tu.ChangeTracker.State <> ObjectState.Deleted).RollId
            usuario.GroupCode = usuario.TenantUsers.FirstOrDefault(Function(tu) tu.ChangeTracker.State <> ObjectState.Deleted).GroupId
        End If


        usuario.UserType = PUserType.GetValueOrDefault.ToString()
        usuario.ProfileType = PProfileType.GetValueOrDefault.ToString()

        usuario.ChangePassword = CBool(INDrbgChangePasswordNext.EditValue)
        If INDtxtPasswordExpiresDays.Text.Trim <> "" Then
            usuario.DaysChangePassword = CInt(INDtxtPasswordExpiresDays.Text.Replace(obtenerRecurso(ComunesDias, Comunes), "").Trim)
        End If
        If INDdaeAccountExpires.Text <> "" Then
            If INDdaeAccountExpires.EditValue IsNot Nothing Then
                usuario.DateExpiryAccount = CDate(INDdaeAccountExpires.EditValue)
            End If
        Else
            usuario.DateExpiryAccount = Nothing
        End If

        '*********Biometrico
        usuario.Person.Fingerprint = BarraBotones.Huella

        '*********Biometrico
        If usuario.Person.FilePerson IsNot Nothing Then
            If usuario.Person.FilePerson.Count > 0 Then
                usuario.Person.FilePerson(0).Photo = CtrFoto.Foto
            Else
                usuario.Person.FilePerson.Add(New Domain.Security.Entities.FilePerson)
                usuario.Person.FilePerson(0).Photo = CtrFoto.Foto
            End If
        Else
            usuario.Person.FilePerson = New Domain.Base.Entities.TrackableCollection(Of Domain.Security.Entities.FilePerson)
            usuario.Person.FilePerson.Add(New Domain.Security.Entities.FilePerson)
            usuario.Person.FilePerson(0).Photo = CtrFoto.Foto
        End If

        usuario.AddressSingInLync = INDtxtAddresLync.Text
        usuario.UserNameLync = INDtxtUserLync.Text
        usuario.PasswordLync = INDtxtPasswordLync.Text

        '*********Datos Contacto
        If Object.Equals(usuario.Person.Address, Nothing) = False Then
            For i As Integer = 0 To ListadoEliminadosDireccion.Count - 1
                ListadoEliminadosDireccion.Item(i).ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
                usuario.Person.Address.Add(ListadoEliminadosDireccion.Item(i))
            Next
        End If
        If Object.Equals(usuario.Person.Phone, Nothing) = False Then
            For i As Integer = 0 To ListadoEliminadosTelefono.Count - 1
                ListadoEliminadosTelefono.Item(i).ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
                usuario.Person.Phone.Add(ListadoEliminadosTelefono.Item(i))
            Next
        End If

        ' Quitamos todos los permisos de usuarios que fueron denegados

        'Si hay permisos para eliminar
        If usuario.PermissionUser IsNot Nothing AndAlso usuario.PermissionUser.Any(Function(_PermissionUser) Not _PermissionUser.ActionValue) Then

            Dim linq = From _PermissionUser In usuario.PermissionUser.Where(Function(_PermissionUser) Not _PermissionUser.ActionValue)
            linq.ToList().ForEach(Sub(_PermissionUser) usuario.PermissionUser.Add(_PermissionUser.MarkAsDeleted()))
        End If

        If ApplicationSetting.Instance.LoginAzure Then
            _userConfig.ActualCity = INDgleCity.EditValue
            _userConfig.CustomReportPath = RutaReportesPersonalizados
            _userConfig.LanguageCulture = Idioma
            _userConfig.LightweightVersion = LightweightVersion
            _userConfig.ReporteadorActivo = ReporteadorActivo
            _userConfig.IdTimezone = INDSleTimeZone.EditValue
            _userConfig.ShowThemeSkinSelector = ShowThemeSkin
            _userConfig.TypeAlertControl = PTypeAlertControl
            _userConfig.DateFormat = INDGleDateFormat.EditValue
            _userConfig.TimeFormat = INDGleTimeFormat.EditValue
            _userConfig.ReportPathType = ReportPathType

            Select Case CType(PUserType, UserType)
                Case UserType.GlobalAdmin
                Case UserType.TenantAdmin
                Case UserType.CompanyAdmin, UserType.StandardUser
                    If usuario.PermissionCompany.Where(Function(pc) pc.ChangeTracker.State <> ObjectState.Deleted).Count = 1 Then 'una sola empresa
                        _userConfig.Containers = Nothing
                        _userConfig.DefaultCompany = usuario.PermissionCompany.Where(Function(pc) pc.ChangeTracker.State <> ObjectState.Deleted).FirstOrDefault.IdContainer
                    End If
            End Select
        End If

        '********* Termina Datos Contacto
        Dim Result As ActionResult
        Using model = New MUsuario
            If usuario.Id > 0 Then
                usuario.MarkAsModified()
            End If
            AsyncLoader(True)
            Result = Await model.GuardarListadoUsuarios(usuario)
            'BaseClass.GetActiveForms(SessionValues.Instance.UserIndigo.ToString.Trim, SessionValues.Instance.UserRol.ToString.Trim)
            AsyncLoader(False)
        End Using
        If Result.StateResult = True Then
            If ApplicationSetting.Instance.LoginAzure Then
                If _userConfig.UserId = 0 Then
                    If Result.MessageResult.Count = 1 AndAlso Result.MessageResult(0).StartsWith("USERID") Then
                        Dim _userId As Integer = CInt(Result.MessageResult(0).Split(":")(1))
                        _userConfig.UserId = _userId
                    End If
                ElseIf _userConfig.Id > 0 Then
                    _userConfig.ChangeTracker.State = ObjectState.Modified
                End If
                Using modelo = New PAutenticacionUsuario(New MAutenticacionUsuario.ParametrosAzureFunctions() With {.AppFunctionURL = ConfigurationFile.Instance.AppFunctionURL, .SaveUserConfigurationKey = ConfigurationFile.Instance.SaveUserConfigurationKey})
                    Await modelo.SaveUserConfiguration(_userConfig)
                End Using
            End If

            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
            Deshacer()
            AbrirBusqueda()
        Else
            If Result.MessageResult.Count > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = Result.MessageResult(0)
            Else
                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
            End If
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Private Async Sub Eliminar() Implements IUsuario.Eliminar
        If INDbtnUserCode.Text.Length > 0 Then
            If Object.Equals(usuario.UserCode, Nothing) = True Then
                Exit Sub
            End If
            If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then

                AsyncLoader(True)
                Using modelo = New MUsuario
                    Dim result = Await modelo.EliminarUsuarios(usuario)
                    If result.StateResult = True Then
                        AsyncLoader(False)
                        Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
                        Deshacer()
                        AbrirBusqueda()
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador, Comunes)
                    End If
                End Using
            End If
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements IUsuario.Buscar
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        If ListadoEliminadosDireccion.Count > 0 Then
            ListadoEliminadosDireccion.Clear()
        End If
        If ListadoEliminadosTelefono.Count > 0 Then
            ListadoEliminadosTelefono.Clear()
        End If
        If ListadoEliminadosEmail.Count > 0 Then
            ListadoEliminadosEmail.Clear()
        End If
        CtrContacts.LimpiarControles()
        Limpiar()
        LogicaBotonActualizar(False)
        'INDbtnUserCode.Focus()
        INDTxeEmail.Focus()
        If tokenPermissionCompany IsNot Nothing Then
            tokenPermissionCompany.Cancel()
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements ICrudBase.Nuevo
        Deshacer()
    End Sub

    ''' <summary>
    ''' METODO: abrir frontal de busquedas.
    ''' </summary>
    Public Sub AbrirBusqueda() Implements ICrudBase.OpenSearch
        indigo.UserViewMode = True
        Dim tipoCrud As Infrastructure.CrossCutting.Base.eDataSource = Infrastructure.CrossCutting.Base.eDataSource.UsersCRUD
        Dim listColumns As List(Of ColumnInfo) = {New ColumnInfo With {.Caption = "Código", .FieldName = "UserCode"},
            New ColumnInfo With {.Caption = "Email", .FieldName = "Email"},
            New ColumnInfo With {.Caption = "Identificación", .FieldName = "PersonIdentification"},
            New ColumnInfo With {.Caption = "Nombre", .FieldName = "PersonFullName"},
            New ColumnInfo With {.Caption = "Tipo Usuario", .FieldName = "UserTypeName"},
            New ColumnInfo With {.Caption = "Perfil", .FieldName = "ProfileTypeName"},
            New ColumnInfo With {.Caption = "Estado", .FieldName = "StatusName", .ColumnAligment = HorzAlignment.Center}}.ToList()

        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = listColumns
            .ValorSolicitado = "UserCode"
            .ListadoOrigenDatos = tipoCrud
            BarraBotones.PrepareToolbar(eAction.New)
            .FormParent = Me
            .ShowSearch()
        End With
        indigo.UserViewMode = _ViewForm
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        INDbtnUserCode.Text = ReturnValue
        INDTxeEmail.Text = ReturnObject.Email
        If INDbtnUserCode.Text <> String.Empty Then
            Await CargarControles()
            If INDbtnUserCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbtnUserCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' este permite establecer la logica para los permisos de Guardar y Actualizar
    ''' </summary>
    Public Sub LogicaBotonActualizar(ByVal existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
        BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub

#End Region

#Region "Metodos"
    ''' <summary>
    ''' Metodo keydown sobre el control de cargo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtxtUserPosition_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles INDMeUserPosition.KeyDown
        If e.KeyCode = Keys.Enter Then
            If CType(PUserType, UserType) = UserType.GlobalAdmin Then
                INDgleUserRoll.ShowPopup()
            End If

        End If
    End Sub

    ''' <summary>
    ''' Evento Cuando se cierra el popup de contacto
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CtrContacts_ClosePopUp()
        INDpceContactData.ClosePopup()
        INDrbgChangePasswordNext.Focus()
    End Sub

    ''' <summary>
    ''' Evento cuando se cambia el valor de check de password
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrbgChangePasswordNext_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles INDrbgChangePasswordNext.EditValueChanged
        If CBool(INDrbgChangePasswordNext.EditValue) = False Then
            INDtxtPasswordExpiresDays.Enabled = False
            INDdaeAccountExpires.Enabled = False
        Else
            INDtxtPasswordExpiresDays.Enabled = True
            INDdaeAccountExpires.Enabled = True
        End If
    End Sub

    ''' <summary>
    ''' Metodo para mostrar la barra de carga del base
    ''' </summary>
    ''' <param name="Value"></param>
    ''' <remarks></remarks>
    Public Sub AsyncLoader1(Value As Boolean) Implements IUsuario.AsyncLoader
        AsyncLoader(Value)
    End Sub

    ''' <summary>
    ''' Evento para cargar roles y grupos cuando cambia tenant
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleTenant_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleTenant.EditValueChanged
        Dim resultado As Short
        If INDSleTenant.EditValue Is Nothing OrElse (Not Int16.TryParse(INDSleTenant.EditValue, resultado)) OrElse resultado = 0 Then
            INDSleRole.EditValue = Nothing
            INDGleGroup.EditValue = Nothing
        Else
            INDSleRole.Properties.DataSource = Nothing
            INDGleGroup.Properties.DataSource = Nothing

        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    Private Sub LoadCompanies()
        If ListContainers Is Nothing OrElse ListContainers.Count = 0 Then
            Using Model As New MUsuario
                ListContainers = Model.GetCompaniesSimple()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Metodo para cargar las empresas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadUserCompanies()
        ListPermissionCompanies = New List(Of GenesisPermissionCompanies)


        If usuario.PermissionCompany Is Nothing Then
            usuario.PermissionCompany = New Domain.Base.Entities.TrackableCollection(Of PermissionCompany)
        End If

        If PUserType IsNot Nothing Then
            If usuario.Id > 0 Then
                Select Case indigo.UserType
                    Case UserType.GlobalAdmin, UserType.GlobalQa

                    Case UserType.TenantAdmin
                        Select Case CType(PUserType.GetValueOrDefault, UserType)
                            Case UserType.GlobalAdmin
                            Case UserType.GlobalQa
                            Case UserType.TenantAdmin
                            Case UserType.CompanyAdmin, UserType.StandardUser
                                'elimina de memoria las compañias del usuario consultado que no correspondan a los tenant del usuario logeado
                                Dim _PermissionCompanys = From pc In usuario.PermissionCompany
                                                          Group Join c In UnifiedConfiguration.Instance.ListCompanies.Select(Function(c) c.Id).Distinct On c Equals pc.IdContainer Into Group
                                                          From c In Group.DefaultIfEmpty()
                                                          Where c = 0
                                                          Select pc
                                For Each _PermissionCompany In _PermissionCompanys.ToList
                                    usuario.PermissionCompany.Remove(_PermissionCompany)
                                Next
                        End Select

                    Case UserType.CompanyAdmin
                        'elimina de memoria las compañias del usuario consultado que no correspondan a los tenant del usuario logeado
                        Dim _PermissionCompanys = From pc In usuario.PermissionCompany
                                                  Group Join c In UnifiedConfiguration.Instance.ListCompanies.Where(Function(c) c.Administrator).Select(Function(c) c.Id).Distinct On c Equals pc.IdContainer Into Group
                                                  From c In Group.DefaultIfEmpty()
                                                  Where c = 0
                                                  Select pc
                        For Each _PermissionCompany In _PermissionCompanys.ToList
                            usuario.PermissionCompany.Remove(_PermissionCompany)
                        Next

                    Case UserType.StandardUser
                End Select
            End If


            Select Case CType(PUserType.GetValueOrDefault, UserType)
                Case UserType.GlobalAdmin
                Case UserType.TenantAdmin
                Case UserType.CompanyAdmin, UserType.StandardUser
                    Dim _PermissionsCompany = From _PermissionCompany In usuario.PermissionCompany
                                              Join _Container In ListContainers On _Container.Id Equals _PermissionCompany.IdContainer
                                              Select _PermissionCompany, _Container
                    For Each _ContainerPermissionCompanyAux In _PermissionsCompany
                        ListPermissionCompanies.Add(New GenesisPermissionCompanies With {.CompanyId = _ContainerPermissionCompanyAux._Container.Id,
                                                                                            .CompanyCode = _ContainerPermissionCompanyAux._Container.Code,
                                                                                            .CompanyName = _ContainerPermissionCompanyAux._Container.Name,
                                                                                            .CompanyTransactionalContainer = _ContainerPermissionCompanyAux._Container.TransactionalContainer,
                                                                                            .CompanyProduction = _ContainerPermissionCompanyAux._Container.ProductionCompany,
                                                                                            .CompanyPermission = _ContainerPermissionCompanyAux._PermissionCompany.Permission,
                                                                                            .TenantId = _ContainerPermissionCompanyAux._Container.TenantId,
                                                                                            .TenantName = _ContainerPermissionCompanyAux._Container.TenantName,
                                                                                            .Administrator = _ContainerPermissionCompanyAux._PermissionCompany.Administrator.GetValueOrDefault,
                                                                                            .IdOperatingUnitDefault = _ContainerPermissionCompanyAux._PermissionCompany.IdOperatingUnitDefault,
                                                                                            .CompanyListOperatingUnit = New List(Of GenesisOperatingUnit)})
                    Next
                Case Else
            End Select
            RefreshGridPermissionCompany()
        End If
    End Sub

    ''' <summary>
    ''' Updates the state.
    ''' </summary>
    Public Async Sub UpdateState()
        If Not String.IsNullOrEmpty(usuario.UserCode) Then
            Try
                Using Model As New MUsuario
                    Dim status As Integer
                    Select Case usuario.State
                        Case True  'activo
                            status = False
                        Case False  'inactivo
                            status = True
                    End Select
                    AsyncLoader(True)
                    Dim Result = Await Model.UpdateStatus(usuario.Id, status)
                    If Result.StateResult = True Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
                        Me.usuario.State = True
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = "No se pudo cambiar el estado al usuario"
                    End If
                    BarraBotones.CleanAuditBasic()
                    Await CargarControles()
                    AsyncLoader(False)
                End Using
            Catch ex As Exception
                Throw ex
                AsyncLoader(False)
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Sub

    ''' <summary>
    ''' Metodo para cargar las ciudades del xml
    ''' </summary>
    Private Sub CargarCiudades()
        INDgleCity.Properties.DataSource = GetXmlWithAggregates(Of VieWoeidCities)(eDataXml.XMLWoeidCities)
    End Sub

    Private Sub INDPathReportServerBte_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDPathReportServerBte.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis Then
            INDXfbdRuta.ShowDialog()

            RutaReportesPersonalizados = INDXfbdRuta.SelectedPath
        End If
    End Sub

    ''' <summary>
    ''' Refresca la informacion de la rejilla tenantUser
    ''' </summary>
    Private Sub RefreshGridTenantUser()
        If usuario.TenantUsers Is Nothing Then
            usuario.TenantUsers = New Domain.Base.Entities.TrackableCollection(Of TenantUsers)
        End If
        INDGcTenantUser.Invalidate()
        INDGcTenantUser.DataSource = usuario.TenantUsers.Where(Function(tu) tu.ChangeTracker.State <> ObjectState.Deleted)
        INDGcTenantUser.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Refresca la informacion de la rejilla PermissionCompany
    ''' </summary>
    Private Sub RefreshGridPermissionCompany()
        If INDgcPermissionCompanies.InvokeRequired Then
            INDgcPermissionCompanies.Invoke(New MethodInvoker(AddressOf RefreshGridPermissionCompany))
        Else
            INDgcPermissionCompanies.DataSource = ListPermissionCompanies
            INDgcPermissionCompanies.RefreshDataSource()
        End If
    End Sub

    ''' <summary>
    ''' Quita permisos por compañia
    ''' </summary>
    ''' <param name="_GenesisPermissionCompany"></param>
    Private Sub RemoveCompanyPermissions(_GenesisPermissionCompany As GenesisPermissionCompanies)
        If _GenesisPermissionCompany IsNot Nothing Then
            Dim _PermissionCompany As PermissionCompany = usuario.PermissionCompany.Where(Function(pc) pc.IdContainer = _GenesisPermissionCompany.CompanyId).FirstOrDefault
            If _PermissionCompany IsNot Nothing Then
                If _PermissionCompany.Id > 0 Then
                    _PermissionCompany.ChangeTracker.State = ObjectState.Deleted
                Else
                    User.PermissionCompany.Remove(_PermissionCompany)
                End If
            End If
            For Each _UserOperatingUnit In usuario.UserOperatingUnit.Where(Function(uop) uop.IdContainer = _GenesisPermissionCompany.CompanyId).ToList
                _UserOperatingUnit.Status = False
                If _UserOperatingUnit.Id > 0 Then
                    _UserOperatingUnit.ChangeTracker.State = ObjectState.Deleted
                Else
                    User.UserOperatingUnit.Remove(_UserOperatingUnit)
                End If
            Next
            ListPermissionCompanies.Remove(_GenesisPermissionCompany)
        End If
    End Sub

    ''' <summary>
    ''' Habilita o inhabilita controles segun el tipo de usuario
    ''' </summary>
    ''' <param name="_Enabled"></param>
    Private Sub EnableControlsforUserType()
        Dim ViewState As DevExpress.XtraLayout.Utils.LayoutVisibility
        Dim ControlEnable As Boolean

        Select Case CType(PUserType, UserType)

            Case UserType.GlobalAdmin, UserType.GlobalQa

                ViewState = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                ControlEnable = True

                'Habilito controles de rol y grupos globales
                INDlyiUserRoll.Visibility = ViewState
                INDgleUserRoll.Enabled = ControlEnable
                INDlyiUserGroup.Visibility = ViewState
                INDgleUserGroup.Enabled = ControlEnable



                ViewState = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                ControlEnable = False

                'ControlTabPpal 
                INDTcgTenantCompanies.Visibility = ViewState

                'Controlo Seccion de Tenant
                INDLcgTenant.Visibility = ViewState
                INDLciTenant.Visibility = ViewState
                INDSleTenant.Enabled = ControlEnable
                INDLciTenantRole.Visibility = ViewState
                INDSleRole.Enabled = ControlEnable
                INDLciTenantGroup.Visibility = ViewState
                INDGleGroup.Enabled = ControlEnable
                INDLciTenantAdd.Visibility = ViewState
                INDSmbAddTenant.Enabled = ControlEnable
                INDLciGcTenantUser.Visibility = ViewState
                INDGcTenantUser.Enabled = ControlEnable

                'Controlo Seccion de Company
                INDlygPermissionCompanies.Visibility = ViewState
                INDLciCompanies.Visibility = ViewState
                INDSleCompanies.Enabled = ControlEnable
                INDLciManageCompany.Visibility = ViewState
                INDRgManageCompany.Enabled = ControlEnable
                INDLciAddPermissionCompany.Visibility = ViewState
                INDSmbAddPermissionCompany.Enabled = ControlEnable
                INDlyPermissionCompanies.Visibility = ViewState
                INDgcPermissionCompanies.Enabled = ControlEnable
                INDLciReportPathType.Visibility = ViewState

            Case UserType.TenantAdmin

                ViewState = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                ControlEnable = False

                'Habilito controles de rol y grupos globales
                INDlyiUserRoll.Visibility = ViewState
                INDgleUserRoll.Enabled = ControlEnable
                INDlyiUserGroup.Visibility = ViewState
                INDgleUserGroup.Enabled = ControlEnable



                ViewState = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                ControlEnable = True

                'ControlTabPpal 
                INDTcgTenantCompanies.Visibility = ViewState

                'Controlo Seccion de Tenant
                INDLcgTenant.Visibility = ViewState
                INDLciTenant.Visibility = ViewState
                INDSleTenant.Enabled = ControlEnable
                INDLciTenantRole.Visibility = ViewState
                INDSleRole.Enabled = ControlEnable
                INDLciTenantGroup.Visibility = ViewState
                INDGleGroup.Enabled = ControlEnable
                INDLciTenantAdd.Visibility = ViewState
                INDSmbAddTenant.Enabled = ControlEnable
                INDLciGcTenantUser.Visibility = ViewState
                INDGcTenantUser.Enabled = ControlEnable


                ViewState = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                ControlEnable = False


                'Controlo Seccion de Company
                INDlygPermissionCompanies.Visibility = ViewState
                INDLciCompanies.Visibility = ViewState
                INDSleCompanies.Enabled = ControlEnable
                INDLciManageCompany.Visibility = ViewState
                INDRgManageCompany.Enabled = ControlEnable
                INDLciAddPermissionCompany.Visibility = ViewState
                INDSmbAddPermissionCompany.Enabled = ControlEnable
                INDlyPermissionCompanies.Visibility = ViewState
                INDgcPermissionCompanies.Enabled = ControlEnable
                INDLciReportPathType.Visibility = ViewState
            Case UserType.CompanyAdmin

                ViewState = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                ControlEnable = False

                'Habilito controles de rol y grupos globales
                INDlyiUserRoll.Visibility = ViewState
                INDgleUserRoll.Enabled = ControlEnable
                INDlyiUserGroup.Visibility = ViewState
                INDgleUserGroup.Enabled = ControlEnable



                ViewState = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                ControlEnable = True

                'ControlTabPpal 
                INDTcgTenantCompanies.Visibility = ViewState

                'Controlo Seccion de Tenant
                INDLcgTenant.Visibility = ViewState
                INDLciTenant.Visibility = ViewState
                INDSleTenant.Enabled = ControlEnable
                INDLciTenantRole.Visibility = ViewState
                INDSleRole.Enabled = ControlEnable
                INDLciTenantGroup.Visibility = ViewState
                INDGleGroup.Enabled = ControlEnable
                INDLciTenantAdd.Visibility = ViewState
                INDSmbAddTenant.Enabled = ControlEnable
                INDLciGcTenantUser.Visibility = ViewState
                INDGcTenantUser.Enabled = ControlEnable


                'Controlo Seccion de Company
                INDlygPermissionCompanies.Visibility = ViewState
                INDLciCompanies.Visibility = ViewState
                INDSleCompanies.Enabled = ControlEnable
                INDLciManageCompany.Visibility = ViewState
                INDRgManageCompany.Enabled = ControlEnable
                INDLciAddPermissionCompany.Visibility = ViewState
                INDSmbAddPermissionCompany.Enabled = ControlEnable
                INDlyPermissionCompanies.Visibility = ViewState
                INDgcPermissionCompanies.Enabled = ControlEnable
                INDLciReportPathType.Visibility = ViewState


            Case UserType.StandardUser

                ViewState = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                ControlEnable = False

                'Habilito controles de rol y grupos globales
                INDlyiUserRoll.Visibility = ViewState
                INDgleUserRoll.Enabled = ControlEnable
                INDlyiUserGroup.Visibility = ViewState
                INDgleUserGroup.Enabled = ControlEnable



                ViewState = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                ControlEnable = True

                'ControlTabPpal 
                INDTcgTenantCompanies.Visibility = ViewState

                'Controlo Seccion de Tenant
                INDLcgTenant.Visibility = ViewState
                INDLciTenant.Visibility = ViewState
                INDSleTenant.Enabled = ControlEnable
                INDLciTenantRole.Visibility = ViewState
                INDSleRole.Enabled = ControlEnable
                INDLciTenantGroup.Visibility = ViewState
                INDGleGroup.Enabled = ControlEnable
                INDLciTenantAdd.Visibility = ViewState
                INDSmbAddTenant.Enabled = ControlEnable
                INDLciGcTenantUser.Visibility = ViewState
                INDGcTenantUser.Enabled = ControlEnable


                'Controlo Seccion de Company
                INDlygPermissionCompanies.Visibility = ViewState
                INDLciCompanies.Visibility = ViewState
                INDSleCompanies.Enabled = ControlEnable
                INDLciManageCompany.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDRgManageCompany.EditValue = False
                INDRgManageCompany.Enabled = Not ControlEnable
                INDLciAddPermissionCompany.Visibility = ViewState
                INDSmbAddPermissionCompany.Enabled = ControlEnable
                INDlyPermissionCompanies.Visibility = ViewState
                INDgcPermissionCompanies.Enabled = ControlEnable
                INDLciReportPathType.Visibility = ViewState

            Case Else
        End Select
    End Sub

    ''' <summary>
    ''' Habilita e inhabilita los controles cuando es un cliente PASS u OnPremise
    ''' </summary>
    Private Sub EnableControlsLoginPASS()
        If ApplicationSetting.Instance.LoginAzure Then

            INDLcgOtrosDatos.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            CargarCiudades()

            INDlygSecurityData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyiChangePasswordNext.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDrbgChangePasswordNext.Enabled = False
            INDlyiPasswordExpiresDays.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDtxtPasswordExpiresDays.Enabled = False
            INDlyiAccountExpires.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDdaeAccountExpires.Enabled = False
            INDlyiPassword.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDtxtPassword.Enabled = False
            INDlyiPasswordAgain.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDtxtPasswordAgain.Enabled = False
        End If
    End Sub
#End Region

#Region "Eventos de la Barra de Botones y Eventos Controles Formulario"

    ''' <summary>
    ''' Barras the botones_ click_ active inactive.
    ''' </summary>
    Private Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        UpdateState()
    End Sub

    Private Sub BarraBotones_ClicRestablecerLayout() Handles BarraBotones.ClicRestablecerLayout
        RestablecerLayout()
    End Sub


    ''' <summary>
    ''' Handles the LinkClicked event of the LinkLabel1 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.Windows.Forms.LinkLabelLinkClickedEventArgs" /> instance containing the event data.</param>
    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
        HabilitarGrupoContraseña = True
    End Sub

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    Private Sub BarraBotones_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' CTRs  barra botones_ click_ nuevo.
    ''' </summary>
    Private Sub CtrBarraBotones_Click_Nuevo() Handles BarraBotones.ClickNuevo
        Nuevo()
    End Sub

    ''' <summary>
    ''' CTRs the barra botones_ click_ deshacer.
    ''' </summary>
    Private Sub CtrBarraBotones_Click_Deshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
        AbrirBusqueda()
        LoadActions()
    End Sub

    ''' <summary>
    ''' CTRs the barra botones_ click_ eliminar.
    ''' </summary>
    Private Sub CtrBarraBotones_Click_Eliminar() Handles BarraBotones.ClickEliminar
        AsyncLoader(True)
        Eliminar()
        AsyncLoader(False)
    End Sub

    ''' <summary>
    ''' CTRs the barra botones_ click_ guardar.
    ''' </summary>
    Private Sub CtrBarraBotones_Click_Guardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' CTRs the barra botones_ click_ Actualizar.
    ''' </summary>
    Private Sub CtrBarraBotones_Click_Actualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' CTRs the barra botones_ click_ buscar.
    ''' </summary>
    Private Sub CtrBarraBotones_Click_Buscar() Handles BarraBotones.ClickBuscar
        AbrirBusqueda()
    End Sub

    '''' <summary>
    '''' Metodo que me controla el evento  KeyDown del control INDBtnCodigo y me dirige al presenter.
    '''' </summary>
    'Private Async Sub INDBtnEditCodigo_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles INDbtnUserCode.KeyDown
    '    If Not String.IsNullOrEmpty(INDbtnUserCode.Text.ToString) Then
    '        If e.KeyCode = Keys.Enter Then
    '            Await CargarControles()
    '            If INDbtnUserCode.Enabled = False Then
    '                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
    '                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ControlBiometrico) = True
    '            End If
    '            INDbtnUserCode.Enabled = False
    '        End If
    '    End If
    'End Sub

    '''' <summary>
    '''' Metodo que me abre el frontal de busqueda desde el control Btncodigo .
    '''' </summary>
    'Private Sub INDBtnEditCodigo_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDbtnUserCode.ButtonClick
    '    AbrirBusqueda()
    'End Sub

    ''' <summary>
    ''' Evento para eliminar las direcciones agregadas al control de datos de contacto
    ''' </summary>
    Private Sub CtrContactos1_EliminarDireccion() Handles CtrContacts.EliminarDireccion
        If usuario.Person.Address.Count > 0 Then
            ListadoEliminadosDireccion.Add(usuario.Person.Address.Item(CtrContacts.ItemSelecionadoDireccion))
            usuario.Person.Address.RemoveAt(CtrContacts.ItemSelecionadoDireccion)
            CtrContacts.EstablecerDataSourceDireccion = usuario.Person.Address
        End If
    End Sub
    ''' <summary>
    ''' Evento para eliminar los telefono agregados al control de datos de contacto
    ''' </summary>
    Private Sub CtrContactos1_EliminarTelefono() Handles CtrContacts.EliminarTelefono
        If usuario.Person.Phone.Count > 0 Then
            ListadoEliminadosTelefono.Add(usuario.Person.Phone.Item(CtrContacts.ItemSelecionadoTelefono))
            usuario.Person.Phone.RemoveAt(CtrContacts.ItemSelecionadoTelefono)
            CtrContacts.EstablecerDataSourceTelefono = usuario.Person.Phone
        End If
    End Sub
    ''' <summary>
    ''' Evento para eliminar los Email agregados al control de datos de contacto
    ''' </summary>
    Private Sub CtrContactos1_EliminarEmail() Handles CtrContacts.EliminarEmail
        If usuario.Person.Email.Count > 0 Then
            'ListadoEliminadosEmail.Add(usuario.Person.Email.Item(CtrContacts.ItemSelecionadoEmail))
            usuario.Person.Email(CtrContacts.ItemSelecionadoEmail).MarkAsDeleted()
            CtrContacts.EstablecerDataSourceEmail = usuario.Person.Email
        End If
    End Sub
    ''' <summary>
    ''' Evento para agregar al listado de direcciones  del control de datos de contacto
    ''' </summary>
    Private Sub CtrContactos1_InsertoNuevaDireccion() Handles CtrContacts.InsertoNuevaDireccion
        If usuario.Person.Address Is Nothing Then
            usuario.Person.Address = New Domain.Base.Entities.TrackableCollection(Of Domain.Security.Entities.Address)
        End If
        If usuario.Person.Address.Where(Function(e) e.Addresss = CtrContacts.Direccion).Count = 0 Then
            usuario.Person.Address.Add(New Domain.Security.Entities.Address With {.Addresss = CtrContacts.Direccion})
        End If
        CtrContacts.EstablecerDataSourceDireccion = Nothing
        CtrContacts.EstablecerDataSourceDireccion = usuario.Person.Address
    End Sub
    ''' <summary>
    ''' Evento para agregar al listado de correos  del control de datos de contacto
    ''' </summary>
    Private Sub CtrContactos1_InsertoNuevoEmail() Handles CtrContacts.InsertoNuevoEmail
        If usuario.Person.Email Is Nothing Then
            usuario.Person.Email = New Domain.Base.Entities.TrackableCollection(Of Domain.Security.Entities.Email)
        End If
        If usuario.Person.Email.Where(Function(e) e.Email1 = CtrContacts.Email).Count = 0 Then
            usuario.Person.Email.Add(New Domain.Security.Entities.Email With {.Email1 = CtrContacts.Email})
        End If
        CtrContacts.EstablecerDataSourceEmail = Nothing
        CtrContacts.EstablecerDataSourceEmail = usuario.Person.Email
    End Sub
    ''' <summary>
    ''' Evento para agregar al listado de Telefonos  del control de datos de contacto.
    ''' </summary>
    Private Sub CtrContactos1_InsertoNuevoTelefono() Handles CtrContacts.InsertoNuevoTelefono
        If usuario.Person.Phone Is Nothing Then
            usuario.Person.Phone = New Domain.Base.Entities.TrackableCollection(Of Domain.Security.Entities.Phone)
        End If
        If usuario.Person.Phone.Where(Function(e) e.Phone1 = CtrContacts.Telefono).Count = 0 Then
            usuario.Person.Phone.Add(New Domain.Security.Entities.Phone With {.Phone1 = CtrContacts.Telefono, .IdPhoneType = CShort(CtrContacts.TipoTelefono)})

        End If
        CtrContacts.EstablecerDataSourceTelefono = Nothing
        CtrContacts.EstablecerDataSourceTelefono = usuario.Person.Phone
    End Sub

    ''' <summary>
    ''' Visualiza un formulario para seleccionar los permisos por tenant
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSmbTenantPermission_Click(sender As Object, e As EventArgs) Handles INDSmbTenantPermission.Click
        TaskloadListForms.Wait()
        Dim _TenantUser = TryCast(INDGvTenantUser.GetFocusedRow, TenantUsers)
        If _TenantUser IsNot Nothing Then
            Using formulario As New PopupPermissionsTenant()
                formulario.OriginalUser = usuario
                formulario.TenantId = _TenantUser.TenantId
                formulario.RollId = _TenantUser.RollId
                formulario.listModulesForms = presenter.ListForm
                formulario.StartPosition = FormStartPosition.CenterParent
                formulario.Size = New Size(842, 768)
                Dim transparent = New FrmTransparent(formulario, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                transparent.ShowDialog(Me)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento para quitar la relacion de un usuario con un tenant
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSmbDeleteTenantPermission_Click(sender As Object, e As EventArgs) Handles INDSmbDeleteTenantPermission.Click
        Dim _TenantUser = TryCast(INDGvTenantUser.GetFocusedRow, TenantUsers)
        If _TenantUser IsNot Nothing Then
            If _TenantUser.Id > 0 Then
                _TenantUser.ChangeTracker.State = ObjectState.Deleted
            Else
                User.TenantUsers.Remove(_TenantUser)
            End If
            Select Case CType(PUserType, UserType)
                Case UserType.GlobalAdmin
                Case UserType.TenantAdmin
                Case UserType.CompanyAdmin, UserType.StandardUser
                    For Each _GenesisPermissionCompany In ListPermissionCompanies.Where(Function(lpc) lpc.TenantId = _TenantUser.TenantId).ToList
                        RemoveCompanyPermissions(_GenesisPermissionCompany)
                    Next
                    RefreshGridPermissionCompany()
            End Select
            'Quita permisos usuarios por tenant
            If usuario.PermissionUser IsNot Nothing AndAlso usuario.PermissionUser.Any(Function(_PermissionUser) _PermissionUser.TenantId = _TenantUser.TenantId AndAlso _PermissionUser.ActionValue) Then
                Dim linq = From _PermissionUser In usuario.PermissionUser.Where(Function(_PermissionUser) _PermissionUser.TenantId = _TenantUser.TenantId AndAlso _PermissionUser.ActionValue)
                           Select _PermissionUser
                If linq.Any(Function(_PermissionUser) _PermissionUser.Id = 0) Then
                    linq.Where(Function(_PermissionUser) _PermissionUser.Id = 0).ToList().ForEach(Sub(_PermissionUser) usuario.PermissionUser.Remove(_PermissionUser))
                End If
                If linq.Any(Function(_PermissionUser) _PermissionUser.Id > 0) Then
                    linq.Where(Function(_PermissionUser) _PermissionUser.Id > 0).ToList().ForEach(Sub(_PermissionUser) _PermissionUser.ActionValue = False)
                End If
            End If
            RefreshGridTenantUser()
            INDSleCompanies.Properties.DataSource = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Adiciona o actualiza tenantuser a la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSmbAdd_Click(sender As Object, e As EventArgs) Handles INDSmbAddTenant.Click
        Dim _Mensaje = "Seleccione: "
        If PUserType Is Nothing Then
            _Mensaje = String.Format("{0} {1} {2}", _Mensaje, Environment.NewLine, "Tipo usuario")
        End If
        If TenantId Is Nothing OrElse TenantId = 0 Then
            _Mensaje = String.Format("{0} {1} {2}", _Mensaje, Environment.NewLine, "Tenant")
        End If
        If RollId Is Nothing OrElse RollId = 0 Then
            _Mensaje = String.Format("{0} {1} {2}", _Mensaje, Environment.NewLine, "Rol")
        End If
        If GroupId Is Nothing OrElse GroupId = 0 Then
            _Mensaje = String.Format("{0} {1} {2}", _Mensaje, Environment.NewLine, "Grupo")
        End If
        If Not _Mensaje.Equals("Seleccione: ") Then
            Mensaje(EeventViewerImages.MensajeError) = _Mensaje
        Else

            Dim _TenantUser As TenantUsers = usuario.TenantUsers.Where(Function(tu) tu.TenantId = TenantId.GetValueOrDefault).FirstOrDefault
            If _TenantUser Is Nothing Then
                _TenantUser = New TenantUsers() With {.TenantId = TenantId.GetValueOrDefault, .UserType = PUserType}
                Dim _Tenant = Nothing
                If indigo.UserType = UserType.GlobalAdmin Then
                    _Tenant = TryCast(INDSleTenant.GetSelectedDataRow(), TenantXpo)
                Else
                    _Tenant = TryCast(INDSleTenant.GetSelectedDataRow(), TenantUsersXpo)
                End If

                If _Tenant IsNot Nothing Then
                    _TenantUser.TenantName = _Tenant.Name
                End If
                usuario.TenantUsers.Add(_TenantUser)
            End If
            _TenantUser.State = True
            If CType(PUserType, UserType) = UserType.TenantAdmin Then
                _TenantUser.ManageCompany = True
            End If
            Dim _Roll = TryCast(INDSleRole.GetSelectedDataRow(), TenantRollXpo)
            If _Roll IsNot Nothing Then
                _TenantUser.RollId = _Roll.Id
                _TenantUser.RoleName = _Roll.RollName
            End If
            Dim _Group = TryCast(INDGleGroup.GetSelectedDataRow(), TenantGroupXpo)
            If _Group IsNot Nothing Then
                _TenantUser.GroupId = _Group.Id
                _TenantUser.GroupName = _Group.GroupName
            End If
            If _TenantUser.ChangeTracker.State = ObjectState.Deleted Then
                _TenantUser.ChangeTracker.State = ObjectState.Modified
            End If
            RefreshGridTenantUser()
            INDSleCompanies.Properties.DataSource = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Agrega permiso a una compañia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSmbAddPermissionCompany_Click(sender As Object, e As EventArgs) Handles INDSmbAddPermissionCompany.Click
        Dim _Mensaje = "Seleccione: "
        If CompanyId Is Nothing OrElse CompanyId = 0 Then
            _Mensaje = String.Format("{0} {1} {2}", _Mensaje, Environment.NewLine, "Empresa")
        End If
        If Not _Mensaje.Equals("Seleccione: ") Then
            Mensaje(EeventViewerImages.MensajeError) = _Mensaje
        Else
            Dim _PermissionCompany As PermissionCompany = usuario.PermissionCompany.Where(Function(pc) pc.IdContainer = CInt(CompanyId)).FirstOrDefault
            Dim _GenesisPermissionCompany = ListPermissionCompanies.Where(Function(lpc) lpc.CompanyId = CompanyId).FirstOrDefault
            If _PermissionCompany Is Nothing Then
                _PermissionCompany = New PermissionCompany() With {.IdContainer = CompanyId.GetValueOrDefault, .IdUser = usuario.Id}
                _GenesisPermissionCompany = New GenesisPermissionCompanies With {.CompanyId = CompanyId.GetValueOrDefault()}
                Dim _Container As Containers = TryCast(INDSleCompanies.GetSelectedDataRow(), Containers)
                If _Container IsNot Nothing Then
                    _PermissionCompany.ContainerName = _Container.Name
                    _PermissionCompany.IdContainer = _Container.Id
                    _PermissionCompany.TenantId = _Container.TenantId
                    _PermissionCompany.TenantName = _Container.TenantName

                    _GenesisPermissionCompany.CompanyCode = _Container.Code
                    _GenesisPermissionCompany.CompanyName = _Container.Name
                    _GenesisPermissionCompany.CompanyTransactionalContainer = _Container.TransactionalContainer
                    _GenesisPermissionCompany.CompanyProduction = _Container.ProductionCompany
                    _GenesisPermissionCompany.TenantId = _Container.TenantId
                    _GenesisPermissionCompany.TenantName = _Container.TenantName
                    _GenesisPermissionCompany.CompanyListOperatingUnit = New List(Of GenesisOperatingUnit)
                    '.IdOperatingUnitDefault = _PermissionCompany.IdOperatingUnitDefault,
                End If
                usuario.PermissionCompany.Add(_PermissionCompany)
                ListPermissionCompanies.Add(_GenesisPermissionCompany)
            End If
            _PermissionCompany.Permission = True
            _PermissionCompany.Administrator = INDRgManageCompany.EditValue
            _GenesisPermissionCompany.CompanyPermission = True
            _GenesisPermissionCompany.Administrator = INDRgManageCompany.EditValue
            If _PermissionCompany.ChangeTracker.State = ObjectState.Deleted Then
                _PermissionCompany.ChangeTracker.State = ObjectState.Modified
            End If
            If CType(PUserType, UserType) = UserType.CompanyAdmin Then
                Dim _TenantUser = usuario.TenantUsers.Where(Function(tu) tu.TenantId = _GenesisPermissionCompany.TenantId).FirstOrDefault
                If _TenantUser IsNot Nothing Then
                    _TenantUser.ManageCompany = usuario.PermissionCompany.Any(Function(pc) pc.TenantId = _GenesisPermissionCompany.TenantId AndAlso pc.ChangeTracker.State <> ObjectState.Deleted AndAlso pc.Administrator)
                End If
            End If

            RefreshGridPermissionCompany()
        End If


    End Sub

    ''' <summary>
    ''' Asigna permisos a unidades operativas por compañia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSmbCompanyPermission_Click(sender As Object, e As EventArgs) Handles INDSmbCompanyPermission.Click
        Dim _GenesisPermissionCompany = TryCast(INDgcvPermissionCompanies.GetFocusedRow, GenesisPermissionCompanies)
        If _GenesisPermissionCompany IsNot Nothing Then
            Using formulario As New PopupOperatingUnitsPermits()
                formulario.OriginalUser = usuario
                formulario.PPermissionCompany = _GenesisPermissionCompany
                formulario.StartPosition = FormStartPosition.CenterParent
                formulario.Size = New Size(630, 510)
                formulario.ReportPathType = If(ReportPathType Is Nothing, 1, ReportPathType)
                Dim transparent = New FrmTransparent(formulario, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                transparent.ShowDialog(Me)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento para quitar permiso de un usuario a una compañia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSmbDeleteCompanyPermission_Click(sender As Object, e As EventArgs) Handles INDSmbDeleteCompanyPermission.Click
        Dim _GenesisPermissionCompany = TryCast(INDgcvPermissionCompanies.GetFocusedRow, GenesisPermissionCompanies)
        RemoveCompanyPermissions(_GenesisPermissionCompany)
        RefreshGridPermissionCompany()
    End Sub
#End Region

#Region "Validaciones de controles "

    'Evento para consultar la persona en el evento keydown de la identificacion
    Private Async Sub INDtxtUserIdentification_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles INDtxtUserIdentification.KeyDown
        If Not String.IsNullOrEmpty(INDtxtUserIdentification.Text.ToString) Then
            If e.KeyCode = Keys.Enter Then
                'consulto la persona
                AsyncLoader(True)
                Using modelo As New MUsuario
                    usuario.Person = Await modelo.ConsultarPersona(INDtxtUserIdentification.Text)
                End Using
                AsyncLoader(False)
                If Object.Equals(usuario.Person.Identification, Nothing) = False Then
                    INDtxtFirstName.Text = usuario.Person.FirstName
                    INDtxtUserSecondName.Text = usuario.Person.SecondName
                    INDtxtUserFirstLastName.Text = usuario.Person.FirstLastName
                    INDtxtUserSecondLastName.Text = usuario.Person.SecondLastName
                    INDrbgUserTypeIdentification.EditValue = usuario.Person.IdentificationType.ToString
                    INDrbgUserGender.EditValue = usuario.Person.Gender.ToString
                    If usuario.Person.FilePerson IsNot Nothing AndAlso usuario.Person.FilePerson.Count > 0 Then
                        CtrFoto.Foto = usuario.Person.FilePerson(0).Photo
                    End If

                    If usuario.Person.Address IsNot Nothing Then CtrContacts.EstablecerDataSourceDireccion = usuario.Person.Address.ToList
                    If usuario.Person.Phone IsNot Nothing Then CtrContacts.EstablecerDataSourceTelefono = usuario.Person.Phone.ToList
                    If usuario.Person.Email IsNot Nothing Then CtrContacts.EstablecerDataSourceEmail = usuario.Person.Email.ToList

                    AccionesSobreLosControles = True
                    INDtxtUserIdentification.Enabled = False
                Else
                    INDtxtUserIdentification.Enabled = False
                    usuario.Person = New Domain.Security.Entities.Person
                    AccionesSobreLosControles = True
                End If
            End If
        End If
    End Sub

#End Region

#Region "Customizacion"

    Dim dtCamposCustomizables As DataTable
    ''' <summary>
    ''' Bandera que se utiliza para verificar si existe o no una definicion del funcional 
    ''' </summary>
    Dim BanderaExisteDefinicionFrontal As Boolean
    ''' <summary>
    ''' Ruta de origen para las definiciones de los funcionales
    ''' </summary>
    Dim RutaDefinicionesFuncionales As String
    ''' <summary>
    ''' Evento DoWork que se utiliza para verificar si existe una definicion del xml del frontal
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs" /> instance containing the event data.</param>
    Private Sub CargarDefinicionesAsincronas_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles CargarDefinicionesAsincronas.DoWork
        If CustomizacionFrontales.VerificaExisteDefinicionFrontal(RutaDefinicionesFuncionales) = True Then
            BanderaExisteDefinicionFrontal = True
        End If
    End Sub
    ''' <summary>
    ''' Evento RunWorkerCompleted que se utiliza para preguntar si encontro una definicion del frontal para posteriormente cargarla
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs" /> instance containing the event data.</param>
    Private Sub CargarDefinicionesAsincronas_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles CargarDefinicionesAsincronas.RunWorkerCompleted
        If BanderaExisteDefinicionFrontal = True Then
            INDlycUsers.RestoreLayoutFromXml(RutaDefinicionesFuncionales)
        End If
    End Sub
    ''' <summary>
    ''' Evento que se utiliza para abrir el frontal de customizacion de funcionales verifica si tiene permisos para posteriormente permitir customizar
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Async Sub INDlycUsers_ShowCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDlycUsers.ShowCustomization
        Try
            'Ejecuatamos la consulta
            Using modelo As New MUsuario
                AsyncLoader(True)
                Dim dscampos As DataSet = Await modelo.GetFieldsNULLUsers
                AsyncLoader(False)
                dtCamposCustomizables = dscampos.Tables(0)
                For i As Integer = 0 To dtCamposCustomizables.Rows.Count - 1
                    For j As Integer = 0 To INDlycUsers.Items.Count - 1
                        If Object.Equals(INDlycUsers.Items.Item(j).Tag, Nothing) = False Then
                            If dtCamposCustomizables.Rows(i).Item("NAME").ToString.Trim = INDlycUsers.Items.Item(j).Tag.ToString.Trim Then
                                INDlycUsers.Items.Item(j).AllowHide = False
                            End If
                        End If
                    Next
                Next
            End Using
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
            Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesErrorGuardarDefinicion)
            MessageIndigo.Show(obtenerRecurso(ComunesErrorGuardarDefinicion), MessageType.Errores, Me.Text, Botones.Ok)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando se cierra el formulario de customizacion y que guarda la definicion del layout en la ruta especificada de cada usuario
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub INDLyCentroAtencion_HideCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDlycUsers.HideCustomization
        'Preguntamos si el layout ha sido modificado en el algun momento para posteriormente guardar la definicion
        If INDlycUsers.IsModified = True Then
            Try
                If CustomizacionFrontales.VerificaCarpetaDefinicionFuncionales(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
                    INDlycUsers.SaveLayoutToXml(RutaDefinicionesFuncionales)
                Else
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesErrorGuardarDefinicion)
                End If
            Catch ex As Exception
                IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesErrorGuardarDefinicion)
                MessageIndigo.Show(obtenerRecurso(ComunesErrorGuardarDefinicion), MessageType.Errores, Me.Text, Botones.Ok)
            End Try
        End If
    End Sub

    Private Sub RestablecerLayout()
        If My.Computer.FileSystem.DirectoryExists(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
            My.Computer.FileSystem.DeleteDirectory(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name), Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents)
            INDlycUsers.RestoreDefaultLayout()
            Mensaje(EeventViewerImages.Informacion) = "El formulario se restablecio correctamente"
        End If
    End Sub

#End Region

#Region "Eventos"
    Private Sub INDgleUserRoll_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDgleUserRoll.QueryPopUp
        If INDgleUserRoll.Properties.DataSource Is Nothing Then
            Using modelo As New MUsuario
                INDgleUserRoll.Properties.DataSource = modelo.ListGlobalRolls()
            End Using
        End If
    End Sub

    Private Sub INDgleUserGroup_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDgleUserGroup.QueryPopUp
        If INDgleUserGroup.Properties.DataSource Is Nothing Then
            Using modelo As New MUsuario
                INDgleUserGroup.Properties.DataSource = modelo.ListGlobalGroups()
            End Using
        End If
    End Sub

    Private Sub INDgleUserGroup_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDgleUserGroup.ButtonClick, INDGleGroup.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            If PermisosFormularios.GetPermissionUser(indigo.UserIndigo, "102") = True Then
                Using Formulario As New FrmGrupos
                    Formulario.ShowDialog()
                End Using
            Else
                Mensaje(EeventViewerImages.Advertencia) = "No Tiene Permisos Para esta Accion"
            End If
        End If
    End Sub

    Private Sub INDgleUserRoll_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDgleUserRoll.ButtonClick, INDSleRole.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("101", Nothing, True)
        End If
    End Sub

    ''' <summary>
    ''' Evento para ocultar o visualizar controles dependiendo del tipo de usuario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleUserType_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleUserType.EditValueChanged
        If INDGleUserType.EditValue IsNot Nothing Then
            EnableControlsforUserType()
            Me.LoadUserCompanies()
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleTenant_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleTenant.QueryPopUp
        If INDSleTenant.Properties.DataSource Is Nothing Then
            presenter.InitializeTenant()
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleRole_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleRole.QueryPopUp
        If INDSleRole.Properties.DataSource Is Nothing Then
            If TenantId Is Nothing OrElse TenantId = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Seleccione Tenant"
            Else
                presenter.ListRolls(TenantId)
            End If
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleGroup_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDGleGroup.QueryPopUp
        If INDGleGroup.Properties.DataSource Is Nothing Then
            If TenantId Is Nothing OrElse TenantId = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Seleccione Tenant"
            Else
                presenter.ListGroups(TenantId)
            End If
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleCompanies_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleCompanies.QueryPopUp
        If INDSleCompanies.Properties.DataSource Is Nothing Then
            Dim _Containers = Nothing
            Select Case indigo.UserType
                Case UserType.GlobalAdmin, UserType.TenantAdmin
                    _Containers = From _TenantUser In usuario.TenantUsers.Where(Function(tu) tu.ChangeTracker.State <> ObjectState.Deleted)
                                  Join _Container In ListContainers On _Container.TenantId Equals _TenantUser.TenantId
                                  Select _Container
                Case UserType.CompanyAdmin
                    _Containers = From _TenantUser In usuario.TenantUsers.Where(Function(tu) tu.ChangeTracker.State <> ObjectState.Deleted)
                                  Join _Container In ListContainers On _Container.TenantId Equals _TenantUser.TenantId
                                  Join _Company In UnifiedConfiguration.Instance.ListCompanies.Where(Function(c) c.Administrator) On _Company.Id Equals _Container.Id
                                  Select _Container
            End Select

            INDSleCompanies.Properties.DataSource = _Containers
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDTxeEmail_KeyDown(sender As Object, e As KeyEventArgs) Handles INDTxeEmail.KeyDown
        If Not String.IsNullOrEmpty(INDTxeEmail.Text) Then
            If e.KeyCode = Keys.Enter OrElse e.KeyCode = Keys.Tab Then
                If ApplicationSetting.Instance.LoginAzure Then
                    If INDTxeEmail.Text.Contains("@indigo.tech") Then
                        LoadUserType(UserType.GlobalAdmin)
                    ElseIf indigo.UserType = UserType.GlobalAdmin Then
                        LoadUserType(UserType.TenantAdmin)
                    Else

                        LoadUserType(indigo.UserType)
                    End If
                End If
                ConsultUserByEmail()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo que me abre el frontal de busqueda desde el control INDTxeEmail
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDTxeEmail_DoubleClick(sender As Object, e As EventArgs) Handles INDTxeEmail.DoubleClick
        AbrirBusqueda()
    End Sub


    ''' <summary>
    ''' Valida cantidad de caracteres en el control MemoEdit
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' 
    Private Sub INDMeUserPosition_TextChanged(sender As Object, e As EventArgs) Handles INDMeUserPosition.TextChanged
        Const maxLength As Integer = 150
        If INDMeUserPosition.Text.Length > maxLength Then
            Dim txt = INDMeUserPosition.Text.Substring(0, maxLength)
            INDMeUserPosition.Text = txt
            INDMeUserPosition.SelectionStart = INDMeUserPosition.Text.Length
            INDMeUserPosition.Refresh()
        End If
    End Sub

    ''' <summary>
    ''' Oculta el control de ruta de reporte cuando el tipo de reporte seleccionado corresponde a "Unidad Operativa"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleReportPathType_EditValueChanged(sender As Object, e As ChangingEventArgs) Handles INDGleReportPathType.EditValueChanged
        If INDGleReportPathType.EditValue = 2 Then
            INDLciRutaReportes.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            INDLciRutaReportes.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub


#End Region

End Class