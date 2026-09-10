'***********************************************************************
' Assembly         : Presentacion.Seguridad.MVP
' Author           : Julian Cardozo
' Created          : 29-03-2011
'
' Last Modified By : Julian Andres Cardozo
' Last Modified On : 13-04-2011
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Importaciones"
Imports System.Globalization
Imports System.IO
Imports System.Threading
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Base.Window
Imports Infrastructure.CrossCutting.Exceptions
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.CloudAgent.IndigoReference.Security
Imports Presentation.Security.MVP

#End Region

''' <summary>
''' Clase que controla los comportamientos del formulario FrmConfigurarConexion.
''' </summary>
''' 
Public Class FrmConfigurarConexion
    Implements IConfigurarConexion

#Region "Variables y y Contructor"

    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance
    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session His
    ''' </summary>
    Dim IndigoHis As IndigoSingleton.IndigoValoresSesion = IndigoSingleton.IndigoValoresSesion.Instancia
    ''' <summary>
    ''' Esta variable es la comunicacion con el presentador.
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PConfigurarConexion

    ''' <summary>
    ''' entidad de configuracion de usuario
    ''' </summary>
    Dim UserConfiguration As UserConfiguration

    ''' <summary>
    ''' Inicializa los componentes del formulario y captura en el appconfig el idioma establecido
    ''' </summary>
    Public Sub New()
        InitializeComponent()
        Presenter = New PConfigurarConexion(Me)
        CargarProtocolos()
        CargarCiudades()
        LoadTuple()
        LoadTimezone()
        LoadUserConfiguration()
        LeerArchivoConfiguracion()
    End Sub

    ''' <summary>
    ''' Cargar zonas horarias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub LoadTimezone()
        If INDgleTimeZone.Properties.DataSource Is Nothing Then
            Using Model As New MConfigurarConexion
                INDgleTimeZone.Properties.DataSource = Await Model.GetTimezoneAsync
                INDgleTimeZone.Enabled = True
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Esta variable mantiene el nombre del lenguaje que se encuentra en el appconfig
    ''' si el usuario a cambiado el idioma, pero no lo cambio(cancelar).
    ''' </summary>
    Dim LenguajeAnterior As String
    ''' <summary>
    ''' Variable necesaria para mostrar el mensaje de reiniciar la aplicacion.
    ''' </summary>
    Dim MostrarMensaje As Integer

    'Dim dtEmpresas As DataTable

#End Region

#Region "Propiedades de la interfaz"

    ''' <summary>
    ''' Sets the mensaje xtra.
    ''' </summary>
    ''' <value>The mensaje xtra.</value>
    Public WriteOnly Property MensajeXtra As String Implements MVP.IConfigurarConexion.Mensajes
        Set(ByVal value As String)
            MessageIndigo.Show(value, MessageType.Information, obtenerRecurso(ComunesIndigoCrystal), Botones.Ok)
        End Set
    End Property

    Public Property Ciudad As String Implements IConfigurarConexion.Ciudad
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
    Public Property idioma As String Implements MVP.IConfigurarConexion.idioma
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
    ''' Esta propiedad obtiene o establece el protocolo URL del servidor.
    ''' </summary>
    ''' <value></value>
    Public Property ProtocoloUrlServidor As String Implements MVP.IConfigurarConexion.ProtocoloUrlServidor
        Get
            If INDComboProtocoloServidor.EditValue Is Nothing Then
                Return String.Empty
            Else
                Return INDComboProtocoloServidor.EditValue.ToString
            End If
        End Get
        Set(ByVal value As String)
            INDComboProtocoloServidor.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta Propiedad obtiene o establece el Protocolo URL del Servidor de entidades.
    ''' </summary>
    ''' <value></value>
    Public Property ProtocoloUrlServidorEntidades As String Implements MVP.IConfigurarConexion.ProtocoloUrlServidorEntidades
        Get
            If INDComboProtocoloServidorEntidades.EditValue Is Nothing Then
                Return String.Empty
            Else
                Return INDComboProtocoloServidorEntidades.EditValue.ToString
            End If

        End Get
        Set(ByVal value As String)
            INDComboProtocoloServidorEntidades.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad obtien o establece la URL del servidor de notificaciones.
    ''' </summary>
    ''' <value></value>
    Public Property UrlServidorNotificacion As String Implements IConfigurarConexion.UrlServidorNotificacion
        Get
            Return INDTxtURLServidorNotificacion.Text
        End Get
        Set(value As String)
            INDTxtURLServidorNotificacion.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna un valor que indica si se usa la versión liviana
    ''' </summary>
    ''' <value>Valor que indica si se usa la versión liviana</value>
    ''' <returns>Un valor que indica si se usa la versión liviana</returns>
    Public Property LightweightVersion As Boolean Implements IConfigurarConexion.LightweightVersion
        Get
            Return Me.INDLightweightVersion.EditValue
        End Get
        Set(value As Boolean)
            Me.INDLightweightVersion.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad obtien o establece la URL del servidor del Sistema Documental.
    ''' </summary>
    ''' <value></value>
    Public Property UrlServidorSistemaDocumental As String Implements IConfigurarConexion.UrlServidorSistemaDocumental
        Get
            Return INDTxtURLServidorSistemaDocumental.Text
        End Get
        Set(value As String)
            INDTxtURLServidorSistemaDocumental.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Esta Propiedad obtiene o establece el Protocolo URL del Servidor del Sistema Documental.
    ''' </summary>
    ''' <value></value>
    Public Property ProtocoloUrlServidorSistemaDocumental As String Implements IConfigurarConexion.ProtocoloUrlServidorSistemaDocumental
        Get
            If INDComboProtocoloServidorSistemaDocumental.EditValue Is Nothing Then
                Return String.Empty
            Else
                Return INDComboProtocoloServidorSistemaDocumental.EditValue.ToString
            End If
        End Get
        Set(value As String)
            INDComboProtocoloServidorSistemaDocumental.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta Propiedad obtiene o establece el Protocolo URL del Servidor del Servidor de indexación
    ''' </summary>
    ''' <value></value>
    Public Property ProtocoloUrlServidorIndexacion As String Implements IConfigurarConexion.ProtocoloUrlServidorIndexacion
        Get
            If INDComboProtocoloServidorIndexacion.EditValue Is Nothing Then
                Return String.Empty
            Else
                Return INDComboProtocoloServidorIndexacion.EditValue.ToString
            End If
        End Get
        Set(value As String)
            INDComboProtocoloServidorIndexacion.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad obtien o establece la URL del servidor de entidades.
    ''' </summary>
    ''' <value></value>
    Public Property UrlServidorEntidades As String Implements MVP.IConfigurarConexion.UrlServidorEntidades
        Get
            Return INDTxtServidorEntidades.Text
        End Get
        Set(ByVal value As String)
            INDTxtServidorEntidades.Text = value
        End Set
    End Property

    ''' <summary>
    ''' esta propiedad sirve para escribir la ruta de la carpeta en la cual vamos a guardar las definiciones de reportes personalizados
    ''' </summary>
    Public Property RutaReportesPersonalizados As String Implements IConfigurarConexion.RutaReportesPersonalizados
        Get
            Return INDPathReportServerBte.Text
        End Get
        Set(ByVal value As String)
            INDPathReportServerBte.Text = value
        End Set
    End Property

    Public Property EmpresaIndigoERP As String Implements IConfigurarConexion.EmpresaIndigoERP
        Get
            Return CStr(INDgleCompaniesERP.EditValue)
        End Get
        Set(value As String)
            INDgleCompaniesERP.EditValue = value
        End Set
    End Property

    Public Property TiempoInactividad As Integer Implements MVP.IConfigurarConexion.TiempoInactividad
        Get
            Return CInt(INDcbTiempoInactividad.EditValue)
        End Get
        Set(ByVal value As Integer)
            INDcbTiempoInactividad.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' esta propiedad sirve para escribir la direccion del servicio web
    ''' </summary>
    Public Property UrlServidor As String Implements IConfigurarConexion.UrlServidor
        Get
            Return INDTxtURLServidor.Text
        End Get
        Set(ByVal value As String)
            INDTxtURLServidor.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la Url del servidor de indexación
    ''' </summary>
    Public Property UrlServidorIndexacion As String Implements IConfigurarConexion.UrlServidorIndexacion
        Get
            Return INDTxtURLServidorIndexacion.Text
        End Get
        Set(ByVal value As String)
            INDTxtURLServidorIndexacion.Text = value
        End Set
    End Property

    ''' <summary>
    ''' esta propiedad contiene un mensaje para cualquier evento proporcionado
    ''' </summary>
    Public WriteOnly Property Mensaje(ByVal Icono As EeventViewerImages) As String Implements MVP.IConfigurarConexion.Mensaje
        Set(ByVal value As String)
            MessageIndigo.Show(value, MessageType.Information, obtenerRecurso(ComunesIndigoCrystal), Botones.Ok)
        End Set
    End Property


    ''' <summary>
    ''' Obtiene la zona horaria
    ''' </summary>
    Public Property TimezoneValue As Integer Implements IConfigurarConexion.TimezoneValue
        Get
            Return INDgleTimeZone.EditValue
        End Get
        Set(ByVal value As Integer)
            INDgleTimeZone.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene la zona horaria
    ''' </summary>
    Public Property TimezonegleName As String Implements IConfigurarConexion.TimezonegleName
        Get
            Return INDgleTimeZone.Properties.NullText
        End Get
        Set(ByVal value As String)
            INDgleTimeZone.Properties.NullText = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene formato fecha
    ''' </summary>
    Public Property DateFormat As Integer Implements IConfigurarConexion.DateFormat
        Get
            Return INDgleDateformat.EditValue
        End Get
        Set(ByVal value As Integer)
            INDgleDateformat.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene formato hora
    ''' </summary>
    Public Property Timeformat As Integer Implements IConfigurarConexion.Timeformat
        Get
            Return INDgleTimeformat.EditValue
        End Get
        Set(ByVal value As Integer)
            INDgleTimeformat.EditValue = value
        End Set
    End Property

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Ocurre cuando el usuario cambia lenguaje y cultura
    ''' </summary>
    Public Event LoadCultureUser()

    ''' <summary>
    ''' este metodo sirve para guardar la configuracion de los datos de conexion
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Aceptar() Implements IConfigurarConexion.Aceptar
        If ApplicationSetting.Instance.LoginAzure Then
            DialogResult = System.Windows.Forms.DialogResult.OK
        ElseIf Presenter.ValidarCampos() Then
            CrearArchivoConfiguracion()
        End If

        'Disparar evento
        RaiseEvent LoadCultureUser()
    End Sub

    ''' <summary>
    ''' Obtener entidad usuario usuario
    ''' </summary>
    Private Async Sub LoadUserConfiguration()
        Using model As New Presentation.Client.MVP.MLogin
            TimezoneValue = SessionValues.Instance.IdTimeZone
            DateFormat = SessionValues.Instance.dateFormat
            Timeformat = SessionValues.Instance.timeFormat
            idioma = SessionValues.Instance.LanguageCulture
            Dim TimeZoneObj = Await model.GetTimezoneByIdAsync(TimezoneValue)
            If TimeZoneObj IsNot Nothing AndAlso TimeZoneObj.Id > 0 Then
                TimezonegleName = TimeZoneObj.Name
                SessionValues.Instance.TimezoneName = TimezonegleName
            End If
        End Using
    End Sub

    ''' <summary>
    ''' cargar tuplas
    ''' </summary>
    Private Sub LoadTuple()
        'Cargar formato dia
        If INDgleDateformat.Properties.DataSource Is Nothing Then
            Dim ListDateformat As New List(Of Tuple(Of Integer, String))()
            ListDateformat.Add(New Tuple(Of Integer, String)(0, "dd/MM/yyyy"))
            ListDateformat.Add(New Tuple(Of Integer, String)(1, "MM/dd/yyyy"))
            ListDateformat.Add(New Tuple(Of Integer, String)(2, "yyy/MM/dd"))
            ListDateformat.Add(New Tuple(Of Integer, String)(3, "dd/MMM/yyyy"))
            ListDateformat.Add(New Tuple(Of Integer, String)(4, "dd /de MMMM /de yyyy"))
            INDgleDateformat.Properties.DataSource = ListDateformat
        End If
        'Cargar formato hora
        If INDgleTimeformat.Properties.DataSource Is Nothing Then
            Dim ListTimeformat As New List(Of Tuple(Of Integer, String))()
            ListTimeformat.Add(New Tuple(Of Integer, String)(0, "(12) hh:mm:ss tt"))
            ListTimeformat.Add(New Tuple(Of Integer, String)(1, "(24) HH:mm:ss"))
            INDgleTimeformat.Properties.DataSource = ListTimeformat
        End If
    End Sub

    ''' <summary>
    ''' Asignar valores a la entidad de las configuraciones del usuario
    ''' </summary>
    Public Async Sub AssigningValues()
        AssigningValuesInSession()
        Using model As New Presentation.Client.MVP.MLogin
            Dim UserConfigurationCulture As New UserConfigurationCulture()
            With UserConfigurationCulture
                .IdUser = SessionValues.Instance.UserIndigoId
                .Idtimezone = TimezoneValue
                .dateFormat = DateFormat
                .timeFormat = Timeformat
                .LanguageCulture = idioma
            End With
            Await model.UpdateUserConfiguration(UserConfigurationCulture)
        End Using

    End Sub

    ''' <summary>
    ''' Instanciar y asignar valores Zonahoraria,formato fecha y hora formato
    ''' </summary>
    Public Sub AssigningValuesInSession()
        SessionValues.Instance.IdTimeZone = TimezoneValue
        SessionValues.Instance.dateFormat = DateFormat
        SessionValues.Instance.timeFormat = Timeformat
        SessionValues.Instance.LanguageCulture = idioma

        SessionValues.Instance.Culture = New CultureInfo(INDImageComboLenguaje.EditValue.ToString())
        IndigoHis.IdTimeZone = TimezoneValue
        IndigoHis.dateFormat = DateFormat
        IndigoHis.timeFormat = Timeformat

        IndigoHis.dateFormatValue = Infrastructure.CrossCutting.Base.Utils.GetCustomDateFormat(DateFormat)
        IndigoHis.timeFormatValue = Infrastructure.CrossCutting.Base.Utils.GetCustomTimeFormat(Timeformat)

        Dim dateFormatCulture = System.Globalization.CultureInfo.CurrentCulture
        If DateFormat > 0 Then
            dateFormatCulture.DateTimeFormat.LongTimePattern = IndigoHis.timeFormatValue
            dateFormatCulture.DateTimeFormat.LongDatePattern = IndigoHis.dateFormatValue
            dateFormatCulture.DateTimeFormat.ShortTimePattern = IndigoHis.timeFormatValue
            dateFormatCulture.DateTimeFormat.ShortDatePattern = IndigoHis.dateFormatValue
        End If
    End Sub

    ''' <summary>
    ''' Captura el idioma establecido en el archivo de configuración
    ''' </summary>
    Public Sub CapturarIdiomaEstablecido()
        If LoaderConfigurationFile.ConfigurationFileExists() Then
            Thread.CurrentThread.CurrentUICulture = ConfigurationFile.Instance.Culture
            LenguajeAnterior = ConfigurationFile.Instance.Culture.Name
            idioma = SessionValues.Instance.LanguageCulture
        Else
            Thread.CurrentThread.CurrentUICulture = SessionValues.Instance.Culture
            LenguajeAnterior = SessionValues.Instance.Culture.Name
            idioma = SessionValues.Instance.LanguageCulture
        End If
    End Sub

    ''' <summary>
    ''' esta metodo sirve para cancelar el proceso de configuracion de los datos de conexion
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Cancelar() Implements IConfigurarConexion.Cancelar
        Cancelar()
    End Sub

    ''' <summary>
    ''' Controla el evento Click del control INDBtnAceptar y me dirije al metodo que guardar la configuracion de los datos de conexion.
    ''' </summary>
    Private Sub INDBtnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles INDBtnAceptar.Click
        AssigningValues()
        Aceptar()

    End Sub

    ''' <summary>
    ''' Metodo para cargar las ciudades del xml
    ''' </summary>
    Private Sub CargarCiudades()
        INDgleCity.Properties.DataSource = GetXmlWithAggregates(Of VieWoeidCities)(eDataXml.XMLWoeidCities)
    End Sub

    ''' <summary>
    ''' metodo que sirve para cargar los protocolos en los combox
    ''' </summary>
    Public Sub CargarProtocolos()
        For Each protocolo As String In [Enum].GetNames(GetType(Protocol))
            If Not protocolo = "Ninguno" Then
                INDComboProtocoloServidor.Properties.Items.Add(protocolo)
                INDComboProtocoloServidorSistemaDocumental.Properties.Items.Add(protocolo)
                INDComboProtocoloServidorIndexacion.Properties.Items.Add(protocolo)
            End If
        Next
        INDComboProtocoloServidorEntidades.Properties.Items.Add([Enum].GetName(GetType(Protocol), Protocol.basicHttp))
        INDComboProtocoloServidorEntidades.Properties.Items.Add([Enum].GetName(GetType(Protocol), Protocol.netTcp))
        INDComboProtocoloServidorEntidades.Properties.Items.Add([Enum].GetName(GetType(Protocol), Protocol.basicHttps))
    End Sub

    ''' <summary>
    ''' Metodo que me sirve para crear el archivo de configuracion.
    ''' </summary>
    Public Sub CrearArchivoConfiguracion()
        Try
            ConfigurationFile.Instance.UrlWebServer = UrlServidor
            ConfigurationFile.Instance.UrlXpoWebServer = UrlServidorEntidades
            ConfigurationFile.Instance.UrlNotificationWebServer = UrlServidorNotificacion
            ConfigurationFile.Instance.UrlDocumentalSystemWebServer = UrlServidorSistemaDocumental
            ConfigurationFile.Instance.UrlIndexingWebServer = UrlServidorIndexacion
            ConfigurationFile.Instance.ProtocolUrlWebServer = CType([Enum].Parse(GetType(Protocol), ProtocoloUrlServidor), Protocol)
            ConfigurationFile.Instance.ProtocolUrlXpoWebServer = CType([Enum].Parse(GetType(Protocol), ProtocoloUrlServidorEntidades), Protocol)
            ConfigurationFile.Instance.ProtocolUrlDocumentalSystemWebServer = CType([Enum].Parse(GetType(Protocol), ProtocoloUrlServidorSistemaDocumental), Protocol)
            ConfigurationFile.Instance.ProtocolUrlIndexingWebServer = CType([Enum].Parse(GetType(Protocol), ProtocoloUrlServidorIndexacion), Protocol)
            ConfigurationFile.Instance.ReportsPath = RutaReportesPersonalizados
            ConfigurationFile.Instance.LightweightVersion = Me.LightweightVersion
            ConfigurationFile.Instance.TimeOutApp = CInt(INDcbTiempoInactividad.EditValue)
            ConfigurationFile.Instance.DefaultCompanyContainerCode = EmpresaIndigoERP
            Dim ContainerAux = CType(Me.INDgleCompaniesERP.GetSelectedDataRow(), Containers)
            If ContainerAux IsNot Nothing Then
                ConfigurationFile.Instance.DefaultCompanyContainerName = ContainerAux.Name
                ConfigurationFile.Instance.DefaultCompanyType = ContainerAux.CompanyType
            End If
            ConfigurationFile.Instance.City = Ciudad
            ConfigurationFile.Instance.Culture = New CultureInfo(INDImageComboLenguaje.EditValue.ToString())

            If LoaderConfigurationFile.Instance.SaveChanges() Then
                MessageIndigo.Show(obtenerRecurso(ComunesActualizado), MessageType.Information, Me.Text, Botones.Ok)
                Presentation.CloudAgent.IndigoConecta.Reset()

                Indigo.City = ConfigurationFile.Instance.City
                Indigo.LocalReportsPath = ConfigurationFile.Instance.LocalReportsPath
                Indigo.ServerReportsPath = ConfigurationFile.Instance.ServerReportsPath

                '===========================
                DialogResult = System.Windows.Forms.DialogResult.OK
            Else
                MessageIndigo.Show("No se pudo grabar la configuración!", MessageType.Errores, Me.Text, Botones.Ok)
                DialogResult = System.Windows.Forms.DialogResult.Abort
            End If
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
            DialogResult = System.Windows.Forms.DialogResult.Abort
        End Try
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para leer la configuracion de conexion .
    ''' </summary>
    Private Async Sub LeerArchivoConfiguracion()
        If LoaderConfigurationFile.ConfigurationFileExists() OrElse ApplicationSetting.Instance.LoginAzure Then
            INDTxtURLServidor.Text = ConfigurationFile.Instance.UrlWebServer
            INDTxtServidorEntidades.Text = ConfigurationFile.Instance.UrlXpoWebServer
            INDTxtURLServidorNotificacion.Text = ConfigurationFile.Instance.UrlNotificationWebServer
            INDTxtURLServidorSistemaDocumental.Text = ConfigurationFile.Instance.UrlDocumentalSystemWebServer
            INDTxtURLServidorIndexacion.Text = ConfigurationFile.Instance.UrlIndexingWebServer
            INDComboProtocoloServidor.EditValue = ConfigurationFile.Instance.ProtocolUrlWebServer
            INDComboProtocoloServidorEntidades.EditValue = ConfigurationFile.Instance.ProtocolUrlXpoWebServer
            INDComboProtocoloServidorSistemaDocumental.EditValue = ConfigurationFile.Instance.ProtocolUrlDocumentalSystemWebServer
            INDComboProtocoloServidorIndexacion.EditValue = ConfigurationFile.Instance.ProtocolUrlIndexingWebServer
            INDPathReportServerBte.Text = ConfigurationFile.Instance.ReportsPath
            Me.LightweightVersion = ConfigurationFile.Instance.LightweightVersion
            INDcbTiempoInactividad.EditValue = ConfigurationFile.Instance.TimeOutApp
            Ciudad = ConfigurationFile.Instance.City
            If ApplicationSetting.Instance.LoginAzure Then
                INDTxtURLServidor.ReadOnly = True
                INDTxtServidorEntidades.ReadOnly = True
                INDTxtURLServidorNotificacion.ReadOnly = True
                INDTxtURLServidorSistemaDocumental.ReadOnly = True
                INDTxtURLServidorIndexacion.ReadOnly = True
                INDComboProtocoloServidor.ReadOnly = True
                INDComboProtocoloServidorEntidades.ReadOnly = True
                INDComboProtocoloServidorSistemaDocumental.ReadOnly = True
                INDComboProtocoloServidorIndexacion.ReadOnly = True
                INDPathReportServerBte.ReadOnly = True
                INDLightweightVersion.ReadOnly = True
                INDcbTiempoInactividad.ReadOnly = True
                INDgleCompaniesERP.Properties.NullText = ConfigurationFile.Instance.DefaultCompanyContainerName
                LayoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never 'cancelar
                CtrUpdateLoad2.Enabled = False
                INDgleCity.ReadOnly = True
                INDImageComboLenguaje.ReadOnly = True
                INDPathReportServerBte.Properties.Buttons(0).Enabled = False
            Else
                Await GetCompaniesERP()
                EmpresaIndigoERP = ConfigurationFile.Instance.DefaultCompanyContainerCode
            End If
        End If
        Me.CapturarIdiomaEstablecido()
    End Sub

    Private Sub INDComboProtocoloServidor_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles INDComboProtocoloServidor.EditValueChanged

        If LoaderConfigurationFile.ConfigurationFileExists() Then
            If CStr(INDComboProtocoloServidor.EditValue).Trim().ToLower() = "basicHttp".Trim().ToLower() Then
                INDTxtURLServidor.Focus()
                INDTxtURLServidor.SelectAll()
                INDTxtURLServidor.Properties.Mask.EditMask = "http[s]{0,1}://[0-9A-Za-z.:/]+/+"
            End If
            'If CStr(INDComboProtocoloServidor.EditValue).Trim().ToLower() = "netTcp".Trim().ToLower() Then
            '    INDTxtURLServidor.SelectAll()
            '    INDTxtURLServidor.Properties.Mask.EditMask = "net[.]{1}tcp://[0-9A-Za-z.:/]+/+"
            'End If
            If CStr(INDComboProtocoloServidor.EditValue).Trim().ToLower() = "wsHttp".Trim().ToLower() Then
                INDTxtURLServidor.Focus()
                INDTxtURLServidor.SelectAll()
                INDTxtURLServidor.Properties.Mask.EditMask = "http[s]{0,1}://[0-9A-Za-z.:/]+/+"
            End If
        Else
            If CStr(INDComboProtocoloServidor.EditValue).Trim().ToLower() = "basicHttp".Trim().ToLower() Then
                INDTxtURLServidor.Text = String.Empty
                INDTxtURLServidor.Properties.Mask.EditMask = "http[s]{0,1}://[0-9A-Za-z.:/]+/+"
            End If
            'If CStr(INDComboProtocoloServidor.EditValue).Trim().ToLower() = "netTcp".Trim().ToLower() Then
            '    INDTxtURLServidor.Text = String.Empty
            '    INDTxtURLServidor.Properties.Mask.EditMask = "net[.]{1}tcp://[0-9A-Za-z.:/]+/+"
            'End If
            If CStr(INDComboProtocoloServidor.EditValue).Trim().ToLower() = "wsHttp".Trim().ToLower() Then
                INDTxtURLServidor.Text = String.Empty
                INDTxtURLServidor.Properties.Mask.EditMask = "http[s]{0,1}://[0-9A-Za-z.:/]+/+"
            End If
        End If
    End Sub

    Private Sub INDComboProtocoloServidorEntidades_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles INDComboProtocoloServidorEntidades.EditValueChanged
        If LoaderConfigurationFile.ConfigurationFileExists() Then

            If CStr(INDComboProtocoloServidorEntidades.EditValue).Trim().ToLower() = "basicHttp".Trim().ToLower() Then
                INDTxtServidorEntidades.Focus()
                INDTxtServidorEntidades.SelectAll()
                INDTxtServidorEntidades.Properties.Mask.EditMask = "http[s]{0,1}://[0-9A-Za-z.:/]+/+"
            End If
            'If CStr(INDComboProtocoloServidorEntidades.EditValue).Trim().ToLower() = "netTcp".Trim().ToLower() Then
            '    INDTxtServidorEntidades.Focus()
            '    INDTxtServidorEntidades.SelectAll()
            '    INDTxtServidorEntidades.Properties.Mask.EditMask = "net[.]{1}tcp://[0-9A-Za-z.:/]+/+"
            'End If
            If CStr(INDComboProtocoloServidorEntidades.EditValue).Trim().ToLower() = "wsHttp".Trim().ToLower() Then
                INDTxtServidorEntidades.Focus()
                INDTxtServidorEntidades.SelectAll()
                INDTxtServidorEntidades.Properties.Mask.EditMask = "http[s]{0,1}://[0-9A-Za-z.:/]+/+"
            End If
        Else
            If CStr(INDComboProtocoloServidorEntidades.EditValue).Trim().ToLower() = "basicHttp".Trim().ToLower() Then
                INDTxtServidorEntidades.Text = String.Empty
                INDTxtServidorEntidades.Properties.Mask.EditMask = "http[s]{0,1}://[0-9A-Za-z.:/]+/+"
            End If
            'If CStr(INDComboProtocoloServidorEntidades.EditValue).Trim().ToLower() = "netTcp".Trim().ToLower() Then
            '    INDTxtServidorEntidades.Text = String.Empty
            '    INDTxtServidorEntidades.Properties.Mask.EditMask = "net[.]{1}tcp://[0-9A-Za-z.:/]+/+"
            'End If
            If CStr(INDComboProtocoloServidorEntidades.EditValue).Trim().ToLower() = "wsHttp".Trim().ToLower() Then
                INDTxtServidorEntidades.Text = String.Empty
                INDTxtServidorEntidades.Properties.Mask.EditMask = "http[s]{0,1}://[0-9A-Za-z.:/]+/+"
            End If
        End If
    End Sub

    Private Sub INDComboProtocoloServidorSistemaDocumental_EditValueChanged(sender As Object, e As EventArgs) Handles INDComboProtocoloServidorSistemaDocumental.EditValueChanged
        If LoaderConfigurationFile.ConfigurationFileExists() Then
            If CStr(INDComboProtocoloServidorSistemaDocumental.EditValue).Trim().ToLower() = "basicHttp".Trim().ToLower() Then
                INDTxtURLServidorSistemaDocumental.Focus()
                INDTxtURLServidorSistemaDocumental.SelectAll()
                INDTxtURLServidorSistemaDocumental.Properties.Mask.EditMask = "http[s]{0,1}://[0-9A-Za-z.:/]+/+"
            End If
            'If CStr(INDComboProtocoloServidorSistemaDocumental.EditValue).Trim().ToLower() = "netTcp".Trim().ToLower() Then
            '    INDTxtURLServidorSistemaDocumental.Focus()
            '    INDTxtURLServidorSistemaDocumental.SelectAll()
            '    INDTxtURLServidorSistemaDocumental.Properties.Mask.EditMask = "net[.]{1}tcp://[0-9A-Za-z.:/]+/+"
            'End If
            If CStr(INDComboProtocoloServidorSistemaDocumental.EditValue).Trim().ToLower() = "wsHttp".Trim().ToLower() Then
                INDTxtURLServidorSistemaDocumental.Focus()
                INDTxtURLServidorSistemaDocumental.SelectAll()
                INDTxtURLServidorSistemaDocumental.Properties.Mask.EditMask = "http[s]{0,1}://[0-9A-Za-z.:/]+/+"
            End If
        Else
            If CStr(INDComboProtocoloServidorSistemaDocumental.EditValue).Trim().ToLower() = "basicHttp".Trim().ToLower() Then
                INDTxtURLServidorSistemaDocumental.Text = String.Empty
                INDTxtURLServidorSistemaDocumental.Properties.Mask.EditMask = "http[s]{0,1}://[0-9A-Za-z.:/]+/+"
            End If
            'If CStr(INDComboProtocoloServidorSistemaDocumental.EditValue).Trim().ToLower() = "netTcp".Trim().ToLower() Then
            '    INDTxtURLServidorSistemaDocumental.Text = String.Empty
            '    INDTxtURLServidorSistemaDocumental.Properties.Mask.EditMask = "net[.]{1}tcp://[0-9A-Za-z.:/]+/+"
            'End If
            If CStr(INDComboProtocoloServidorSistemaDocumental.EditValue).Trim().ToLower() = "wsHttp".Trim().ToLower() Then
                INDTxtURLServidorSistemaDocumental.Text = String.Empty
                INDTxtURLServidorSistemaDocumental.Properties.Mask.EditMask = "http[s]{0,1}://[0-9A-Za-z.:/]+/+"
            End If
        End If
    End Sub

    Private Sub INDComboProtocoloServidorIndexacion_EditValueChanged(sender As Object, e As EventArgs) Handles INDComboProtocoloServidorIndexacion.EditValueChanged
        If LoaderConfigurationFile.ConfigurationFileExists() Then
            If CStr(INDComboProtocoloServidorIndexacion.EditValue).Trim().ToLower() = "basicHttp".Trim().ToLower() Then
                INDTxtURLServidorIndexacion.Focus()
                INDTxtURLServidorIndexacion.SelectAll()
                INDTxtURLServidorIndexacion.Properties.Mask.EditMask = "http[s]{0,1}://[0-9A-Za-z.:/]+/+"
            End If
            'If CStr(INDComboProtocoloServidorIndexacion.EditValue).Trim().ToLower() = "netTcp".Trim().ToLower() Then
            '    INDTxtURLServidorIndexacion.Focus()
            '    INDTxtURLServidorIndexacion.SelectAll()
            '    INDTxtURLServidorIndexacion.Properties.Mask.EditMask = "net[.]{1}tcp://[0-9A-Za-z.:/]+/+"
            'End If
            If CStr(INDComboProtocoloServidorIndexacion.EditValue).Trim().ToLower() = "wsHttp".Trim().ToLower() Then
                INDTxtURLServidorIndexacion.Focus()
                INDTxtURLServidorIndexacion.SelectAll()
                INDTxtURLServidorIndexacion.Properties.Mask.EditMask = "http[s]{0,1}://[0-9A-Za-z.:/]+/+"
            End If
        Else
            If CStr(INDComboProtocoloServidorIndexacion.EditValue).Trim().ToLower() = "basicHttp".Trim().ToLower() Then
                INDTxtURLServidorIndexacion.Text = String.Empty
                INDTxtURLServidorIndexacion.Properties.Mask.EditMask = "http[s]{0,1}://[0-9A-Za-z.:/]+/+"
            End If
            'If CStr(INDComboProtocoloServidorIndexacion.EditValue).Trim().ToLower() = "netTcp".Trim().ToLower() Then
            '    INDTxtURLServidorIndexacion.Text = String.Empty
            '    INDTxtURLServidorIndexacion.Properties.Mask.EditMask = "net[.]{1}tcp://[0-9A-Za-z.:/]+/+"
            'End If
            If CStr(INDComboProtocoloServidorIndexacion.EditValue).Trim().ToLower() = "wsHttp".Trim().ToLower() Then
                INDTxtURLServidorIndexacion.Text = String.Empty
                INDTxtURLServidorIndexacion.Properties.Mask.EditMask = "http[s]{0,1}://[0-9A-Za-z.:/]+/+"
            End If
        End If
    End Sub

    Private Sub INDPathReportServerBte_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDPathReportServerBte.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis Then
            INDActulizacion.ShowDialog()

            RutaReportesPersonalizados = INDActulizacion.SelectedPath
        End If
    End Sub

    Private Async Function GetCompaniesERP() As Tasks.Task
        INDgleCompaniesERP.EditValue = Nothing
        If INDComboProtocoloServidor.Text <> String.Empty And INDTxtURLServidor.Text <> String.Empty Then
            Indigo.UriWebServices = UrlServidor
            Indigo.WebServiceProtocol = CType([Enum].Parse(GetType(Protocol), ProtocoloUrlServidor), Protocol)
            Using Model As New MConfigurarConexion
                INDgleCompaniesERP.Properties.DataSource = Await Model.GetContainersAsync   'Model.GetCompaniesAsync()
                INDgleCompaniesERP.Enabled = True
            End Using
        End If
    End Function

    Private Async Sub CtrUpdateLoad2_Click() Handles CtrUpdateLoad2.LoadDataSource
        If Not String.IsNullOrEmpty(CStr(INDTxtURLServidor.Text)) AndAlso Not String.IsNullOrEmpty(INDComboProtocoloServidor.Text) Then
            If MessageIndigo.Show(obtenerRecurso(ComunesRemplazaInformacionArchivoConfiguracion, Comunes), MessageType.Question, Me.Text, Botones.SiNo) = DialogResult.Yes Then
                Me.Cursor = ChangeCursorIndigo()
                If LoaderConfigurationFile.ConfigurationFileExists() Then
                    File.Delete(LoaderConfigurationFile.GetPathConfigurationFile())
                End If
                Presentation.CloudAgent.IndigoConecta.Reset()
                ConfigurationFile.Instance.UrlWebServer = UrlServidor
                ConfigurationFile.Instance.ProtocolUrlWebServer = CType([Enum].Parse(GetType(Protocol), Me.ProtocoloUrlServidor), Protocol)
                LoaderConfigurationFile.Instance.SaveChanges()
                Await GetCompaniesERP()
                Me.Cursor = Cursors.Default
            End If
        Else
            MessageIndigo.Show(obtenerRecurso(ComunesDebeIngresarLaRutaDeServiciosYProtocolo, Comunes), MessageType.Warning, Me.Text, Botones.Ok)
        End If
    End Sub

#End Region

End Class