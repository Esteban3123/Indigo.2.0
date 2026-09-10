'***********************************************************************
' Assembly         : Presentacion.login
' Author           : Luis Felipe Pantoja Cerquera
' Created          : 21-09-2017
'
' Last Modified By : Diego Andrés Roldán Lozano
' Last Modified On : 2017-11-02
' Description      : 
'
' Copyright        : (c) . All rights reserved
'***********************************************************************

#Region "Librerias Importadas"
Imports System.Windows
Imports System.Windows.Media.Imaging
Imports System.Windows.Media
Imports System.Runtime.InteropServices
Imports System.Resources
Imports Presentation.CloudAgent
Imports Presentation.Base.BaseClass
Imports System.Windows.Controls
Imports Infrastructure.CrossCutting.Base
Imports System.Text
Imports System.Net.WebClient
Imports System.Net
Imports Newtonsoft.Json.Linq
Imports Domain.Base.Entities
Imports System.IO
Imports Domain.Security.Entities
Imports Presentation.Base
#End Region


''' <summary>
''' Clase con la funcionalidad del control wpf de login 
''' </summary>
''' <remarks></remarks>
Public Class FrmLoginWPF
#Region "Eventos"
    ''' <summary>
    ''' Evento que se dispara al hacer clic en el boton salir
    ''' </summary>
    ''' <remarks></remarks>
    Public Event Exit_()
    ''' <summary>
    ''' Evento que se dispara al hacer clic en el boton login
    ''' </summary>
    ''' <param name="User"></param>
    ''' <param name="Password"></param>
    ''' <remarks></remarks>
    Public Event Login(ByVal User As String, ByVal Password As String)
    ''' <summary>
    ''' Evento que se dispara al hacer clic en el boton home
    ''' </summary>
    ''' <remarks></remarks>
    Public Event Home()
    ''' <summary>
    ''' Evento que se dispara al hacer clic en el boton configurar
    ''' </summary>
    ''' <remarks></remarks>
    Public Event Change_company()
    ''' <summary>
    ''' Evento que se dispara para ubicar el control gle
    ''' </summary>
    ''' <remarks></remarks>
    Public Event LocationCompany()
    ''' <summary>
    ''' Evento que se dispara al cargar la imagen del login
    ''' </summary>
    ''' <remarks></remarks>
    Public Event LoadImage()
#End Region

#Region "Viariables Generales"

    ''' <summary>
    ''' Control timer para actualizar la hora
    ''' </summary>
    ''' <remarks></remarks>
    Dim Timer As New Timer

    ''' <summary>
    ''' Control timer para actualizar la hora
    ''' </summary>
    ''' <remarks></remarks>
    Dim Timer2 As New Timer
    ''' <summary>
    ''' Variable que almacena la instancia de la singelton
    ''' </summary>
    ''' <remarks></remarks>
    Dim indigo As Infrastructure.CrossCutting.Base.SessionValues

    ''' <summary>
    ''' Bandera para bloqueo de sesion
    ''' </summary>
    Public _flagLockedSession As Boolean
#End Region

#Region "Propiedades"
    ''' <summary>
    ''' Propeidad que obtiene o establece la popsion del gle
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PositionGridLookUp As Point

    ''' <summary>
    ''' propiedad que obtiene o establece la contraseña
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Password As String
        Get
            Return INDtxtContraseña.Password
        End Get
        Set(value As String)
            INDtxtContraseña.Password = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene o establce el codigo de usuario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property User As String
        Get
            Return INDtxtUsuario.Text
        End Get
        Set(value As String)
            If value = String.Empty Then
                INDtxtUsuario.Focus()
            End If
            INDtxtUsuario.Text = value
        End Set
    End Property
#End Region

#Region "Metodos"
    ''' <summary>
    ''' Metodo para poner el control del login en cargando y ocultar los botones
    ''' </summary>
    ''' <param name="Value"></param>
    ''' <remarks></remarks>
    Public Sub Load(ByVal Value As Boolean, Optional Text As String = "")
        If Value = True Then
            INDbtnConfiguration.Visibility = System.Windows.Visibility.Collapsed
            INDbtnHome.Visibility = System.Windows.Visibility.Collapsed
            INDgcContainer.Height = 250
            INDgcControls.Visibility = System.Windows.Visibility.Collapsed
            INDgcLoad.Visibility = System.Windows.Visibility.Visible
            INDLoader.Visibility = System.Windows.Visibility.Visible
            INDtxtTitle.Text = Text
        Else
            INDbtnConfiguration.Visibility = System.Windows.Visibility.Visible
            INDbtnHome.Visibility = System.Windows.Visibility.Visible
            INDgcContainer.Height = 400
            INDgcControls.Visibility = System.Windows.Visibility.Visible
            INDgcLoad.Visibility = System.Windows.Visibility.Collapsed
            INDLoader.Visibility = System.Windows.Visibility.Collapsed
            INDtxtUsuario.Focus()
        End If

        If _flagLockedSession Then
            LockedSesion()
        End If
    End Sub

    Public Sub LockedSesion()
        INDtxtUsuario.Text = User
        INDtxtUsuario.IsEnabled = False
        INDtxtContraseña.Focus()
        _flagLockedSession = False
    End Sub
    ''' <summary>
    ''' Metodo cuando se da el foco al codigo de usuario 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtxtUsuario_GotFocus(sender As Object, e As RoutedEventArgs) Handles INDtxtUsuario.GotFocus
        INDborderFocusUsurio.Visibility = System.Windows.Visibility.Visible
        INDborderFocusContraseña.Visibility = System.Windows.Visibility.Collapsed
    End Sub

    ''' <summary>
    ''' Metodo cuando se da el foco a la contraseña
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtxtContraseña_GotFocus(sender As Object, e As RoutedEventArgs) Handles INDtxtContraseña.GotFocus
        INDborderFocusUsurio.Visibility = System.Windows.Visibility.Collapsed
        INDborderFocusContraseña.Visibility = System.Windows.Visibility.Visible
    End Sub

    ''' <summary>
    ''' Metodo para actualizar la fecha y hora de los controles
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub UpdateDatePanel(ByVal Sender As Object, e As Object)
        Dim DateNow As DateTime = DateTime.Now
        INDtxtHora.Text = DateNow.ToString("HH:mm")
        INDtxtFecha.Text = DateNow.ToString("dddd, MMMM dd")
    End Sub

    Public Sub New()
        InitializeComponent()
        WeatherVideo.Volume = 0
        Dim indigo As Infrastructure.CrossCutting.Base.SessionValues = Infrastructure.CrossCutting.Base.SessionValues.Instance
        Dim DateNow As DateTime = DateTime.Now
        INDtxtHora.Text = DateNow.ToString("HH:mm")
        INDtxtFecha.Text = DateNow.ToString("dddd, MMMM dd")
        Timer.Interval = 20000
        Timer.Start()
        AddHandler Timer.Tick, AddressOf UpdateDatePanel
        Dim imageSource As ImageSource = loadBitmap(My.Resources.login_OnPremise)
        INDpeImageLogin.Source = imageSource
        'AssignImage(My.Resources.ImageVideoEye, "En Indigo Technologies estamos humanizando la salud", "")        RaiseEvent LoadImage()
        INDtxtUsuario.Focus()
        LoadMultimedia()
        Presentation.Base.BaseClass.FreeMemory()
    End Sub

    ''' <summary>
    ''' Evento loaded del control wpf donde cargamos los controles de fecha y hora y asignamos el video en caso de que la maquina tenga capacidad en memoria
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

    End Sub

    ''' <summary>
    ''' Metodo para obtener la informacion de multimedia desde azure
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function LoadMultimedia() As Task
        Await GetResourceDaySpecial()
    End Function

    ''' <summary>
    ''' Metodo cuando el reproductor a terminado de reproducir el video; repetimos el video
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReproductorDePrueba_MediaEnded(sender As Object, e As RoutedEventArgs) Handles WeatherVideo.MediaEnded
        CType(sender, MediaElement).Position = TimeSpan.Zero
        CType(sender, MediaElement).Play()
    End Sub

    ''' <summary>
    ''' Evento cuando el reproductor a terminado de cargar el vedeo ocultamos la imagen y mostramos el video
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub WeatherVideo_MediaOpened(sender As Object, e As RoutedEventArgs) Handles WeatherVideo.MediaOpened
        INDGImagen.Visibility = System.Windows.Visibility.Hidden
        INDGVideo.Visibility = System.Windows.Visibility.Visible
        INDpcWeather.Visibility = System.Windows.Visibility.Visible
        INDtxtTitleDay.Text = ""
        RaiseEvent LoadImage()
    End Sub

    Private Sub hideImage(sender As Object, e As EventArgs)
        Timer2.Stop()
        INDGImagen.Visibility = System.Windows.Visibility.Hidden
        INDGVideo.Visibility = System.Windows.Visibility.Visible
        'If flag Then
        '    INDpcWeather.Visibility = System.Windows.Visibility.Visible
        '    INDtxtTitleDay.Text = ""
        'End If
    End Sub

    ''' <summary>
    ''' Metodo clic en el boton salir
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Click_exit(sender As Object, e As RoutedEventArgs)
        RaiseEvent Exit_()
    End Sub

    ''' <summary>
    ''' Metodo clic en el boton login
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub Click_login(sender As Object, e As RoutedEventArgs)
        RaiseEvent Login(INDtxtUsuario.Text, INDtxtContraseña.Password)
    End Sub

    ''' <summary>
    ''' Metodo clic en boton Home
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Click_home(sender As Object, e As RoutedEventArgs)
        RaiseEvent Home()
    End Sub

    ''' <summary>
    ''' Metodo clic en el boton configurar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Click_companies(sender As Object, e As RoutedEventArgs)
        RaiseEvent Change_company()
    End Sub

    ''' <summary>
    ''' Metodo para asignar la imagenes de los dias especiales
    ''' </summary>
    ''' <param name="Image"></param>
    ''' <param name="Title"></param>
    ''' <param name="Description"></param>
    ''' <param name="Color"></param>
    ''' <remarks></remarks>
    Private Sub AssignImage(ByVal Image As Bitmap, ByVal Title As String, ByVal Description As String, Optional Color As String = "#7F000000")
        Dim converter = New System.Windows.Media.BrushConverter()
        Dim brush = converter.ConvertFromString(Color)
        INDicImagenVideo.Source = loadBitmap(Image)
        loadBitmap(Nothing) = Nothing
        INDtxtTitleDay.Text = Title
        INDtxtDescriptionDay.Text = Description
        INDpcButtonText.Background = CType(brush, Media.Brush)
        INDpcButtonText.Background = CType(brush, Media.Brush)
    End Sub

    ''' <summary>
    ''' Metodo para asignar la imagenes de los dias especiales
    ''' </summary>
    ''' <param name="Title"></param>
    ''' <param name="Description"></param>
    ''' <param name="Color"></param>
    ''' <remarks></remarks>
    Private Sub AssignImage(ByVal url As String, ByVal Title As String, ByVal Description As String, Optional Color As String = "#7F000000")
        Dim converter = New System.Windows.Media.BrushConverter()
        Dim brush = converter.ConvertFromString(Color)
        Dim bi As New BitmapImage()
        bi.BeginInit()
        bi.UriSource = New Uri(url, UriKind.RelativeOrAbsolute)
        bi.EndInit()
        INDicImagenVideo.Source = bi
        INDtxtTitleDay.Text = Title
        INDtxtDescriptionDay.Text = Description
        INDpcButtonText.Background = CType(brush, Media.Brush)
    End Sub

    ''' <summary>
    ''' Metodo para asignar la imagenes de los dias especiales
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssignIcon(ByVal url As String)
        Dim bi As New BitmapImage()
        bi.BeginInit()
        bi.UriSource = New Uri(url, UriKind.RelativeOrAbsolute)
        bi.EndInit()
        'INDImgWeatherIcon.Source = bi
    End Sub

    ''' <summary>
    ''' Metodo para obtener las imagenes de los recursos de acuerdo al dia especial mostrar el clic en caso de que corresponda
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function GetResourceDaySpecial() As System.Threading.Tasks.Task
        Dim rutaServicio = ConfigurationFile.Instance.UrlNotificationWebServer
        Dim rutaImagenEspecial = $"{rutaServicio}Resources/{Date.Today.ToString("dd-MM")}.jpg"
        Select Case Date.Today.ToString("dd-MM")
        'Dim rutaImagenEspecial = rutaServicio & "Resources/" & "15-02" & ".jpg"
        'Select Case "15-02"
            Case "12-01"
                AssignImage(rutaImagenEspecial, "Descubrimiento de la penicilina", "")
            Case "15-01"
                AssignImage(rutaImagenEspecial, "Aristoteles", "Padres de la medicina")
            Case "30-01"
                AssignImage(rutaImagenEspecial, "Día mundial de la no violencia", "")
            Case "15-02"
                AssignImage(rutaImagenEspecial, "Dia mundial del niño con cáncer", "")
            Case "18-02"
                AssignImage(rutaImagenEspecial, "Esculapio", "Padres de la medicina")
            Case "01-03"
                AssignImage(rutaImagenEspecial, "Día del contador", "")
            Case "08-03"
                AssignImage(rutaImagenEspecial, "Día de la mujer", "")
            Case "20-03"
                AssignImage(rutaImagenEspecial, "Día del talento familiar", "")
            Case "21-03"
                AssignImage(rutaImagenEspecial, "Día mundial del síndrome de down", "")
            Case "22-03"
                AssignImage(rutaImagenEspecial, "Día mundial del agua", "")
            Case "02-04"
                AssignImage(rutaImagenEspecial, "Día mundial de la conciencia para los autistas", "")
            Case "07-04"
                AssignImage(rutaImagenEspecial, "Día mundial de la Salud", "")
            Case "10-04"
                AssignImage(rutaImagenEspecial, "Día del investigador científico", "")
            Case "11-04"
                AssignImage(rutaImagenEspecial, "Día mundial del párkinson", "")
            Case "13-04"
                AssignImage(rutaImagenEspecial, "Día del quinesiologo", "")
            Case "16-04"
                AssignImage(rutaImagenEspecial, "Día mundial del otorrinolaringólogo", "")
            Case "17-04"
                AssignImage(rutaImagenEspecial, "Día mundial de la hemofilia", "")
            Case "22-04"
                AssignImage(rutaImagenEspecial, "Día de la tierra", "")
            Case "25-04"
                AssignImage(rutaImagenEspecial, "Día mundial de la malaria", "")
            Case "26-04"
                AssignImage(rutaImagenEspecial, "Día de la secretaria", "")
            Case "27-04"
                AssignImage(rutaImagenEspecial, "Día del diseñador grafico", "")
            Case "28-04"
                AssignImage(rutaImagenEspecial, "Día del bacteriólogo", "")
            Case "29-04"
                AssignImage(rutaImagenEspecial, "Día del árbol", "")
            Case "01-05"
                AssignImage(rutaImagenEspecial, "Día del trabajo", "")
            Case "05-05"
                AssignImage(rutaImagenEspecial, "Día mundial del obstetra", "")
            Case "10-05"
                AssignImage(rutaImagenEspecial, "Día de la madre", "")
            Case "12-05"
                AssignImage(rutaImagenEspecial, "Día internacional de la enfermería", "")
            Case "14-05"
                AssignImage(rutaImagenEspecial, "Vacuna contra la malaria", "")
            Case "17-05"
                AssignImage(rutaImagenEspecial, "Día mundial de la hipertensión", "")
            Case "21-05"
                AssignImage(rutaImagenEspecial, "Día mundial del asma", "")
            Case "24-05"
                AssignImage(rutaImagenEspecial, "Día mundial de la epilepsia", "")
            Case "28-05"
                AssignImage(rutaImagenEspecial, "Dia mundial de la nutrición", "")
            Case "31-05"
                AssignImage(rutaImagenEspecial, "Día mundial sin tabaco", "")
            Case "05-06"
                AssignImage(rutaImagenEspecial, "Día del medio ambiente", "")
            Case "06-06"
                AssignImage(rutaImagenEspecial, "Día mundial de los trasplantados", "")
            Case "11-06"
                AssignImage(rutaImagenEspecial, "Día del padre", "")
            Case "14-06"
                AssignImage(rutaImagenEspecial, "Día mundial del donante de sangre", "")
            Case "18-06"
                AssignImage(rutaImagenEspecial, "Hipocrates padre de la medicina", "")
            Case "22-06"
                AssignImage(rutaImagenEspecial, "Día del abogado", "")
            Case "08-07"
                AssignImage(rutaImagenEspecial, "Día mundial de la alergia", "")
            Case "28-07"
                AssignImage(rutaImagenEspecial, "Día mundial de la hepatitis", "")
            Case "01-08"
                AssignImage($"{rutaServicio}Resources/01-08.png", "Semana mundial de la lactancia", "")
            Case "02-08"
                AssignImage($"{rutaServicio}Resources/01-08.png", "Semana mundial de la lactancia", "")
            Case "03-08"
                AssignImage($"{rutaServicio}Resources/01-08.png", "Semana mundial de la lactancia", "")
            Case "04-08"
                AssignImage($"{rutaServicio}Resources/01-08.png", "Semana mundial de la lactancia", "")
            Case "05-08"
                AssignImage($"{rutaServicio}Resources/01-08.png", "Semana mundial de la lactancia", "")
            Case "06-08"
                AssignImage($"{rutaServicio}Resources/01-08.png", "Semana mundial de la lactancia", "")
            Case "07-08"
                AssignImage($"{rutaServicio}Resources/01-08.png", "Semana mundial de la lactancia", "")
            Case "11-08"
                AssignImage(rutaImagenEspecial, "Día del nutricionista", "")
            Case "12-08"
                AssignImage(rutaImagenEspecial, "Día mundial de la influenza", "")
            Case "17-08"
                AssignImage(rutaImagenEspecial, "Día del ingeniero", "")
            Case "28-08"
                AssignImage(rutaImagenEspecial, "Día del adulto mayor", "")
            Case "01-09"
                AssignImage(rutaImagenEspecial, "Día del químico farmacéutico", "")
            Case "06-09"
                AssignImage(rutaImagenEspecial, "Día del fonoaudiólogo", "")
            Case "09-09"
                AssignImage(rutaImagenEspecial, "Día del fisioterapeuta", "")
            Case "10-09"
                AssignImage(rutaImagenEspecial, "Día mundial del suicidio", "")
            Case "15-09"
                AssignImage(rutaImagenEspecial, "Día del gerontólogo", "")
            Case "21-09"
                AssignImage(rutaImagenEspecial, "Día mundial del alzheimer", "")
            Case "23-09"
                AssignImage(rutaImagenEspecial, "Día mundial del corazón", "")
            Case "01-10"
                AssignImage(rutaImagenEspecial, "Día internacional de las personas sordas", "")
            Case "03-10"
                AssignImage(rutaImagenEspecial, "Día del odontólogo", "")
            Case "07-10"
                AssignImage(rutaImagenEspecial, "Dia del terapeuta respiratorio", "")
            Case "12-10"
                AssignImage(rutaImagenEspecial, "Día mundial de la artritis", "")
            Case "14-10"
                AssignImage(rutaImagenEspecial, "Día mundial de la vista", "")
            Case "15-10"
                AssignImage(rutaImagenEspecial, "Día mundial del lavado de manos", "")
            Case "16-10"
                AssignImage(rutaImagenEspecial, "Día mundial del anestesiólogo", "")
            Case "17-10"
                AssignImage(rutaImagenEspecial, "Día mundial del dolor", "")
            Case "18-10"
                AssignImage(rutaImagenEspecial, "Día mundial de la menopausia", "")
            Case "19-10"
                AssignImage(rutaImagenEspecial, "Día mundial del cáncer de mama", "")
            Case "20-10"
                AssignImage(rutaImagenEspecial, "Día mundial del pediatra", "")
            Case "21-10"
                AssignImage(rutaImagenEspecial, "Día mundial del ahorro de energía", "")
            Case "25-10"
                AssignImage(rutaImagenEspecial, "Día del instrumentador", "")
            Case "08-11"
                AssignImage(rutaImagenEspecial, "Día mundial del radiólogo", "")
            Case "14-11"
                AssignImage(rutaImagenEspecial, "Día mundial de la diabetes", "")
            Case "17-11"
                AssignImage(rutaImagenEspecial, "Día mundial de la enfermedad obstructiva crónica", "")
            Case "19-11"
                AssignImage(rutaImagenEspecial, "Día mundial del aire Ppuro", "")
            Case "24-11"
                AssignImage(rutaImagenEspecial, "Día del psicólogo", "")
            Case "25-11"
                AssignImage(rutaImagenEspecial, "Día mundial de la anorexia", "")
            Case "01-12"
                AssignImage(rutaImagenEspecial, "Día mundial del VIH sida", "")
            Case "03-12"
                AssignImage(rutaImagenEspecial, "Día internacional de los discapacitados", "")
            Case "12-12"
                AssignImage(rutaImagenEspecial, "Leonardo da Vinci padre de la medicina", "")

            Case Else
                Try
                    AssignImage(My.Resources.sky_1441936_1920, "Cargando datos del clima...", "")
                    INDpcWeather.Visibility = System.Windows.Visibility.Visible
                    INDtxtTitleDay.Text = ""
                    INDImgWeatherIcon.FontFamily = New FontFamily(Directory.GetCurrentDirectory() & "\Resources\weathericons-regular-webfont.ttf#Weather Icons")
                    INDImgWeatherIcon.Text = Convert.ToChar(CInt("&HF002"))
                    'If File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Indigo Technologies\Indigo Vie ERP\Weather_data.wth") Then
                    If File.Exists(Infrastructure.CrossCutting.Base.Window.Utils.LocalFolder() & "\Vie HealtTech\Indigo Vie ERP\Weather_data.wth") Then
                        'Dim weatherdata = Utils.DeserializeJsonToEntity(Of WeatherDocument)(File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) _
                        Dim weatherdata = Utils.DeserializeJsonToEntity(Of WeatherDocument)(File.ReadAllText(Infrastructure.CrossCutting.Base.Window.Utils.LocalFolder() _
                        & "\Vie HealtTech\Indigo Vie ERP\Weather_data.wth"))
                        With weatherdata
                            INDCity.Text = .NameCity
                            INDTemHigh.Text = .TempHigh & "°"
                            INDTemLow.Text = .TempLow & "°"
                            INDTempNow.Text = .Temperature & "°"
                            INDHum.Text = "Humedad         " & .Humidity & "%"
                            INDVis.Text = "Visibilidad         " & .Visibility & " m"
                            INDPres.Text = "Presion             " & .Pressure & " hPa"
                            INDWind.Text = "Viento              " & .WindSpeed & " m/s"
                        End With
                    End If
                Catch ex As Exception
                End Try
                Dim result As ActionMessageResult(Of WeatherDocument) = Nothing
                Try

                    Dim ListCities As List(Of VieWoeidCities) = GetXmlWithAggregates(Of VieWoeidCities)(eDataXml.XMLWoeidCities)

                    If ConfigurationFile.Instance.City IsNot Nothing Then
                        Dim ObjCity = ListCities.FindAll(Function(x) x.WOEID = ConfigurationFile.Instance.City).Item(0).City

                        result = Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetWeatherCityAsync(ObjCity)
                    End If
                    'Dim filename As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Indigo Technologies\Indigo Vie ERP\Weather_data.wth"
                    Dim filename As String = Infrastructure.CrossCutting.Base.Window.Utils.LocalFolder() & "\Vie HealtTech\Indigo Vie ERP\Weather_data.wth"
                    If Not Directory.Exists(Path.GetDirectoryName(filename)) Then
                        Directory.CreateDirectory(Path.GetDirectoryName(filename))
                    End If
                    'Guardamos la lectura
                    File.WriteAllText(filename, Utils.SerializeObjectToJson(result.ObjectEmbbeded))
                Catch ex As Exception
                End Try
                If result IsNot Nothing AndAlso result.StateResult = True Then
                    Dim rutaVideo = $"{ConfigurationFile.Instance.UrlNotificationWebServer}Resources/Weather/"
                    With result.ObjectEmbbeded
                        INDCity.Text = .NameCity
                        If Not ConfigurationFile.Instance.LightweightVersion Then
                            'Dim filename As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Indigo Technologies\Indigo Vie ERP\WeatherVideo\weather_" & .ConditionCode & "_.mp4"
                            Dim filename As String = Infrastructure.CrossCutting.Base.Window.Utils.LocalFolder() & "\Vie HealtTech\Indigo Vie ERP\WeatherVideo\weather_" & .ConditionCode & "_.m4v"
                            If Not Directory.Exists(Path.GetDirectoryName(filename)) Then
                                Directory.CreateDirectory(Path.GetDirectoryName(filename))
                            End If
                            Dim videoBytes As Byte() = Nothing
                            Select Case .ConditionCode.ToString()

                                Case "200", "201"
                                    INDImgWeatherIcon.Text = Convert.ToChar(CInt("&HF01e"))
                                    INDWeather.Text = obtenerRecurso(Base.Eresources.Weatherthundershowers, Base.Eform.Weather)
                                    'WeatherVideo.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Lluvia_Trueno))
                                    videoBytes = Presentation.Resources.My.Resources.lluvia_trueno
                                Case "202"
                                    INDImgWeatherIcon.Text = Convert.ToChar(CInt("&HF01e"))
                                    INDWeather.Text = obtenerRecurso(Base.Eresources.Weatherthunderstorms, Base.Eform.Weather)
                                    'WeatherVideo.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Lluvia_Trueno))
                                    videoBytes = Presentation.Resources.My.Resources.lluvia_trueno
                                Case "210"
                                    INDImgWeatherIcon.Text = Convert.ToChar(CInt("&HF01e"))
                                    INDWeather.Text = obtenerRecurso(Base.Eresources.Weatherthundershowers, Base.Eform.Weather)
                                    'WeatherVideo.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Lluvia_Trueno))
                                    videoBytes = Presentation.Resources.My.Resources.lluvia_trueno
                                Case "211"
                                    INDImgWeatherIcon.Text = Convert.ToChar(CInt("&HF01e"))
                                    INDWeather.Text = obtenerRecurso(Base.Eresources.Weatherthunderstorms, Base.Eform.Weather)
                                    'WeatherVideo.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Lluvia_Trueno))
                                    videoBytes = Presentation.Resources.My.Resources.lluvia_trueno
                                Case "212"
                                    INDImgWeatherIcon.Text = Convert.ToChar(CInt("&HF01e"))
                                    INDWeather.Text = obtenerRecurso(Base.Eresources.Weatherseverethunderstorms, Base.Eform.Weather)
                                    'WeatherVideo.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Lluvia_Trueno))
                                    videoBytes = Presentation.Resources.My.Resources.lluvia_trueno
                                Case "221"
                                    INDImgWeatherIcon.Text = Convert.ToChar(CInt("&HF01e"))
                                    INDWeather.Text = obtenerRecurso(Base.Eresources.Weatherisolatedthunderstorms, Base.Eform.Weather)
                                    'WeatherVideo.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Lluvia_Trueno))
                                    videoBytes = Presentation.Resources.My.Resources.lluvia_trueno
                                Case "230"
                                    INDImgWeatherIcon.Text = Convert.ToChar(CInt("&HF01e"))
                                    INDWeather.Text = obtenerRecurso(Base.Eresources.Weatherscatteredthunderstorms, Base.Eform.Weather)
                                    'WeatherVideo.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Lluvia_Trueno))
                                    videoBytes = Presentation.Resources.My.Resources.lluvia_trueno
                                Case "231"
                                    INDImgWeatherIcon.Text = Convert.ToChar(CInt("&HF01e"))
                                    INDWeather.Text = obtenerRecurso(Base.Eresources.Weatherscatteredthunderstorms, Base.Eform.Weather)
                                    'WeatherVideo.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Lluvia_Trueno))
                                    videoBytes = Presentation.Resources.My.Resources.lluvia_trueno
                                Case "232"
                                    INDImgWeatherIcon.Text = Convert.ToChar(CInt("&HF01e"))
                                    INDWeather.Text = obtenerRecurso(Base.Eresources.Weatherseverethunderstorms, Base.Eform.Weather)
                                    'WeatherVideo.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Lluvia_Trueno))
                                    videoBytes = Presentation.Resources.My.Resources.lluvia_trueno
                                Case "300", "301"
                                    INDImgWeatherIcon.Text = Convert.ToChar(CInt("&HF04e"))
                                    INDWeather.Text = obtenerRecurso(Base.Eresources.Weatherdrizzle, Base.Eform.Weather)
                                    'WeatherVideo.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Lluvia_Trueno))
                                    videoBytes = Presentation.Resources.My.Resources.dia_escampo
                                Case "302"
                                    INDImgWeatherIcon.Text = Convert.ToChar(CInt("&HF04e"))
                                    INDWeather.Text = obtenerRecurso(Base.Eresources.Weatherdrizzle, Base.Eform.Weather)
                                    'WeatherVideo.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Lluvia_Trueno))
                                    videoBytes = Presentation.Resources.My.Resources.dia_lloviendo
                                Case "310"
                                    INDImgWeatherIcon.Text = Convert.ToChar(CInt("&HF01a"))
                                    INDWeather.Text = obtenerRecurso(Base.Eresources.Weathershowers, Base.Eform.Weather)
                                    'WeatherVideo.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Lluvia_Trueno))
                                    videoBytes = Presentation.Resources.My.Resources.dia_escampo
                                Case "311"
                                    INDImgWeatherIcon.Text = Convert.ToChar(CInt("&HF04e"))
                                    INDWeather.Text = obtenerRecurso(Base.Eresources.Weatherdrizzle, Base.Eform.Weather)
                                    'WeatherVideo.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Lluvia_Trueno))
                                    videoBytes = Presentation.Resources.My.Resources.dia_escampo
                                Case "312", "313"
                                    INDImgWeatherIcon.Text = Convert.ToChar(CInt("&HF01a"))
                                    INDWeather.Text = obtenerRecurso(Base.Eresources.Weathershowers, Base.Eform.Weather)
                                    'WeatherVideo.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Lluvia_Trueno))
                                    videoBytes = Presentation.Resources.My.Resources.dia_lloviendo
                                Case "314"
                                    INDImgWeatherIcon.Text = Convert.ToChar(CInt("&HF01a"))
                                    INDWeather.Text = obtenerRecurso(Base.Eresources.Weatherscatteredshowers, Base.Eform.Weather)
                                    'WeatherVideo.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Lluvia_Trueno))
                                    videoBytes = Presentation.Resources.My.Resources.dia_lloviendo
                                Case "321"
                                    INDImgWeatherIcon.Text = Convert.ToChar(CInt("&HF01e"))
                                    INDWeather.Text = obtenerRecurso(Base.Eresources.Weatherthunderstorms, Base.Eform.Weather)
                                    'WeatherVideo.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Lluvia_Trueno))
                                    videoBytes = Presentation.Resources.My.Resources.dia_lloviendo
                                Case "500", "531"
                                    INDImgWeatherIcon.Text = Convert.ToChar(CInt("&HF01a"))
                                    INDWeather.Text = obtenerRecurso(Base.Eresources.Weatherscatteredshowers, Base.Eform.Weather)
                                    'WeatherVideo.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Lluvia_Trueno))
                                    videoBytes = Presentation.Resources.My.Resources.dia_lloviendo
                                Case "501", "522"
                                    INDImgWeatherIcon.Text = Convert.ToChar(CInt("&HF00c"))
                                    INDWeather.Text = obtenerRecurso(Base.Eresources.Weatherpartlycloudyday, Base.Eform.Weather)
                                    'WeatherVideo.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Lluvia_Trueno))
                                    videoBytes = Presentation.Resources.My.Resources.dia_lloviendo
                                Case "502", "520", "521"
                                    INDImgWeatherIcon.Text = Convert.ToChar(CInt("&HF01e"))
                                    INDWeather.Text = obtenerRecurso(Base.Eresources.Weatherthunderstorms, Base.Eform.Weather)
                                    'WeatherVideo.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Lluvia_Trueno))
                                    videoBytes = Presentation.Resources.My.Resources.dia_lloviendo
                                Case "503", "504"
                                    INDImgWeatherIcon.Text = Convert.ToChar(CInt("&HF01e"))
                                    INDWeather.Text = obtenerRecurso(Base.Eresources.Weatherseverethunderstorms, Base.Eform.Weather)
                                    'WeatherVideo.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Lluvia_Trueno))
                                    videoBytes = Presentation.Resources.My.Resources.dia_lloviendo
                                Case "511"
                                    INDImgWeatherIcon.Text = Convert.ToChar(CInt("&HF019"))
                                    INDWeather.Text = obtenerRecurso(Base.Eresources.Weatherfreezingrain, Base.Eform.Weather)
                                    'WeatherVideo.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Lluvia_Trueno))
                                    videoBytes = Presentation.Resources.My.Resources.stock_footage_hail_raining_down_on_a_wood_deck_hockinson_washington
                                Case "600", "601"
                                    INDImgWeatherIcon.Text = Convert.ToChar(CInt("&HF01b"))
                                    INDWeather.Text = obtenerRecurso(Base.Eresources.Weathersnow, Base.Eform.Weather)
                                    videoBytes = Presentation.Resources.My.Resources.stock_footage_winter_landscape_with_falling_snow
                                    'WeatherVideo.Source = New Uri("http://ak3.picdn.net/shutterstock/videos/3750878/preview/stock-footage-winter-landscape-with-falling-snow.mp4")
                                Case "602"
                                    INDImgWeatherIcon.Text = Convert.ToChar(CInt("&HF01b"))
                                    INDWeather.Text = obtenerRecurso(Base.Eresources.Weatherheavysnow, Base.Eform.Weather)
                                    videoBytes = Presentation.Resources.My.Resources.stock_footage_the_rain_turned_into_sleet_on_the_leaves_of_ivy_close_up
                                    'WeatherVideo.Source = New Uri("http://ak3.picdn.net/shutterstock/videos/3750878/preview/stock-footage-winter-landscape-with-falling-snow.mp4")
                                Case "611", "612"
                                    INDImgWeatherIcon.Text = Convert.ToChar(CInt("&HF0b5"))
                                    INDWeather.Text = obtenerRecurso(Base.Eresources.Weatherscatteredsnowshowers, Base.Eform.Weather)
                                    videoBytes = Presentation.Resources.My.Resources.stock_footage_winter_landscape_with_falling_snow
                                    'WeatherVideo.Source = New Uri("http://ak3.picdn.net/shutterstock/videos/3750878/preview/stock-footage-winter-landscape-with-falling-snow.mp4")
                                Case "613"
                                    INDImgWeatherIcon.Text = Convert.ToChar(CInt("&HF01b"))
                                    INDWeather.Text = obtenerRecurso(Base.Eresources.Weathersnowshowers, Base.Eform.Weather)
                                    videoBytes = Presentation.Resources.My.Resources.stock_footage_the_rain_turned_into_sleet_on_the_leaves_of_ivy_close_up
                                    'WeatherVideo.Source = New Uri("http://ak3.picdn.net/shutterstock/videos/3750878/preview/stock-footage-winter-landscape-with-falling-snow.mp4")
                                Case "615"
                                    INDImgWeatherIcon.Text = Convert.ToChar(CInt("&HF01b"))
                                    INDWeather.Text = obtenerRecurso(Base.Eresources.Weathersnowflurries, Base.Eform.Weather)
                                    videoBytes = Presentation.Resources.My.Resources.stock_footage_winter_landscape_with_falling_snow
                                    'WeatherVideo.Source = New Uri("http://ak3.picdn.net/shutterstock/videos/3750878/preview/stock-footage-winter-landscape-with-falling-snow.mp4")
                                Case "616"
                                    INDImgWeatherIcon.Text = Convert.ToChar(CInt("&HF0b5"))
                                    INDWeather.Text = obtenerRecurso(Base.Eresources.Weathermixedsnowandsleet, Base.Eform.Weather)
                                    videoBytes = Presentation.Resources.My.Resources.stock_footage_the_rain_turned_into_sleet_on_the_leaves_of_ivy_close_up
                                    'WeatherVideo.Source = New Uri("http://ak3.picdn.net/shutterstock/videos/3750878/preview/stock-footage-winter-landscape-with-falling-snow.mp4")
                                Case "620"
                                    INDImgWeatherIcon.Text = Convert.ToChar(CInt("&HF0b5"))
                                    INDWeather.Text = obtenerRecurso(Base.Eresources.Weatherscatteredsnowshowers, Base.Eform.Weather)
                                    videoBytes = Presentation.Resources.My.Resources.stock_footage_the_rain_turned_into_sleet_on_the_leaves_of_ivy_close_up
                                    'WeatherVideo.Source = New Uri("http://ak3.picdn.net/shutterstock/videos/3750878/preview/stock-footage-winter-landscape-with-falling-snow.mp4")
                                Case "621"
                                    INDImgWeatherIcon.Text = Convert.ToChar(CInt("&HF01b"))
                                    INDWeather.Text = obtenerRecurso(Base.Eresources.Weatherheavysnow, Base.Eform.Weather)
                                    videoBytes = Presentation.Resources.My.Resources.stock_footage_the_rain_turned_into_sleet_on_the_leaves_of_ivy_close_up
                                    'WeatherVideo.Source = New Uri("http://ak3.picdn.net/shutterstock/videos/3750878/preview/stock-footage-winter-landscape-with-falling-snow.mp4")
                                Case "622"
                                    INDImgWeatherIcon.Text = Convert.ToChar(CInt("&HF01b"))
                                    INDWeather.Text = obtenerRecurso(Base.Eresources.Weathersnowshowers, Base.Eform.Weather)
                                    videoBytes = Presentation.Resources.My.Resources.stock_footage_the_rain_turned_into_sleet_on_the_leaves_of_ivy_close_up
                                    'WeatherVideo.Source = New Uri("http://ak3.picdn.net/shutterstock/videos/3750878/preview/stock-footage-winter-landscape-with-falling-snow.mp4")
                                Case "701", "741"
                                    INDImgWeatherIcon.Text = Convert.ToChar(CInt("&HF062"))
                                    INDWeather.Text = obtenerRecurso(Base.Eresources.Weathersmoky, Base.Eform.Weather)
                                    'WeatherVideo.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Dia_Nublado))
                                    videoBytes = Presentation.Resources.My.Resources.dia_nublado
                                Case "771"
                                    INDImgWeatherIcon.Text = Convert.ToChar(CInt("&HF050"))
                                    INDWeather.Text = obtenerRecurso(Base.Eresources.Weatherblustery, Base.Eform.Weather)
                                    'WeatherVideo.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Lluvia_Trueno))
                                    videoBytes = Presentation.Resources.My.Resources.lluvia_trueno
                                Case "781"
                                    INDImgWeatherIcon.Text = Convert.ToChar(CInt("&HF056"))
                                    INDWeather.Text = obtenerRecurso(Base.Eresources.Weathertornado, Base.Eform.Weather)
                                    videoBytes = Presentation.Resources.My.Resources.lluvia_trueno
                                    'WeatherVideo.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Lluvia_Trueno))
                                Case "800"
                                    INDImgWeatherIcon.Text = Convert.ToChar(CInt("&HF072"))
                                    INDWeather.Text = obtenerRecurso(Base.Eresources.Weatherhot, Base.Eform.Weather)
                                    'WeatherVideo.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Dia_Soleado))
                                    videoBytes = Presentation.Resources.My.Resources.dia_cielo_despejado
                                Case "801"
                                    INDImgWeatherIcon.Text = Convert.ToChar(CInt("&HF072"))
                                    INDWeather.Text = obtenerRecurso(Base.Eresources.Weatherhaze, Base.Eform.Weather)
                                    'WeatherVideo.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Dia_Soleado))
                                    videoBytes = Presentation.Resources.My.Resources.dia_soliado
                                Case "802"
                                    INDImgWeatherIcon.Text = Convert.ToChar(CInt("&HF0b6"))
                                    INDWeather.Text = obtenerRecurso(Base.Eresources.Weatherhaze, Base.Eform.Weather)
                                    'WeatherVideo.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Dia_Soleado))
                                    videoBytes = Presentation.Resources.My.Resources.dia_nublado
                                Case "803"
                                    INDImgWeatherIcon.Text = Convert.ToChar(CInt("&HF0b6"))
                                    INDWeather.Text = obtenerRecurso(Base.Eresources.Weatherhaze, Base.Eform.Weather)
                                    'WeatherVideo.Source = New Uri(rutaVideo & GetStringWeather(WeatherEnum.Dia_Nublado))
                                    videoBytes = Presentation.Resources.My.Resources.dia_escampo
                                Case "804"
                                    INDImgWeatherIcon.Text = Convert.ToChar(CInt("&HF014"))
                                    INDWeather.Text = obtenerRecurso(Base.Eresources.Weatherfoggy, Base.Eform.Weather)
                                    'WeatherVideo.Source = New Uri("http://ak7.picdn.net/shutterstock/videos/3171649/preview/stock-footage-morning-sunrise-reflection-in-misty-fog-rise-from-flowing-river-water.mp4")
                                    videoBytes = Presentation.Resources.My.Resources.stock_footage_morning_sunrise_reflection_in_misty_fog_rise_from_flowing_river_water

                            End Select
                            If Not File.Exists(filename) Then
                                System.IO.File.WriteAllBytes(filename, videoBytes)
                            End If
                            WeatherVideo.Source = New Uri(filename, UriKind.Relative)
                            WeatherVideo.Play()
                        End If
                        INDTemHigh.Text = .TempHigh & "°"
                        INDTemLow.Text = .TempLow & "°"
                        INDTempNow.Text = .Temperature & "°"
                        INDHum.Text = "Humedad         " & .Humidity & "%"
                        INDVis.Text = "Visibilidad         " & .Visibility & " m"
                        INDPres.Text = "Presion             " & .Pressure & " hPa"
                        INDWind.Text = "Viento              " & .WindSpeed & " m/s"
                    End With
                    'INDGImagen.Visibility = System.Windows.Visibility.Hidden
                    'INDGVideo.Visibility = System.Windows.Visibility.Visible
                    Me.UpdateLayout()
                Else
                    If (My.Computer.Info.TotalPhysicalMemory / (1024 * 1024)) > 3072 And ConfigurationFile.Instance.UrlNotificationWebServer <> "" Then
                        If Not ConfigurationFile.Instance.LightweightVersion Then
                            WeatherVideo.Source = New Uri(ConfigurationFile.Instance.UrlNotificationWebServer & "Resources/Video/Medico.mp4")
                            WeatherVideo.Play()
                        End If
                    Else
                        INDpcWeather.Visibility = System.Windows.Visibility.Collapsed
                        AssignImage(My.Resources.ImageVideoEye, "En Vie HealtTech estamos humanizando la salud", "")
                    End If
                End If
        End Select
    End Function

    Public Sub StopMedia()
        If INDpcWeather.Visibility = Visibility.Visible Then
            WeatherVideo.Stop()
        End If
    End Sub

    Public Sub PlayMedia()
        If INDpcWeather.Visibility = Visibility.Visible Then
            WeatherVideo.Play()
        End If
    End Sub

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
    Public Shared Function GetStringWeather(weather As WeatherEnum) As String
        Select Case weather
            Case WeatherEnum.Cielo_Despejado
                'Return "dia-cielo-despejado.mov"
                Return "dia-cielo-despejado.m4v"
            Case WeatherEnum.Dia_Lloviendo
                'Return "dia-lloviendo.mov"
                Return "dia-lloviendo.m4v"
            Case WeatherEnum.Dia_Nublado
                'Return "dia-nublado.mov"
                Return "dia-nublado.m4v"
            Case WeatherEnum.Dia_Soleado 'Falta
                'Return "dia-cielo-despejado.mov"
                Return "dia-soleado.m4v"
            Case WeatherEnum.Dia_Parcial_Nublado 'Falta
                'Return "dia-cielo-despejado.mov"
                Return "dia-cielo-despejado.m4v"
            Case WeatherEnum.Dia_Escampo
                'Return "dia-escampo.mov"
                Return "dia-escampo.m4v"
            Case WeatherEnum.Noche_Despejada
                'Return "noche-despejada.mov"
                Return "noche-despejada.m4v"
            Case WeatherEnum.Lluvia_Trueno
                'Return "lluvia-trueno.mov"
                Return "lluvia-trueno.m4v"
            Case WeatherEnum.Noche_Parcial_Nublada
                'Return "noche-parcialmente-nublada.mov"
                Return "noche-parcialmente-nublada.m4v"
            Case WeatherEnum.Noche_Nublada
                'Return "noche-nublada.mov"
                Return "noche-nublada.m4v"
        End Select
    End Function


    ''' <summary>
    ''' Evento keydown donde cambiamos de control en caso de presionar enter
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtxtUsuario_KeyDown(sender As Object, e As Input.KeyEventArgs) Handles INDtxtUsuario.KeyDown
        If e.Key = Input.Key.Enter Then
            INDtxtContraseña.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Evento keydown donde cambiamos de control en caso de presionar enter
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtxtContraseña_KeyDown(sender As Object, e As Input.KeyEventArgs) Handles INDtxtContraseña.KeyDown
        If e.Key = Input.Key.Enter Then
            INDbtnAceptar.Focus()
        End If
    End Sub
#End Region

#Region "Metodos de asignacion de imagenes de los recursos"
    <DllImport("gdi32")>
    Private Shared Function DeleteObject(o As IntPtr) As Integer
    End Function

    Private Shared _loadBitmap As BitmapSource
    ''' <summary>
    ''' Metodo que retorna una imagen desde los recursos
    ''' </summary>
    ''' <param name="source"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Property loadBitmap(source As System.Drawing.Bitmap) As BitmapSource
        Get
            Dim ip As IntPtr = source.GetHbitmap()
            Try
                _loadBitmap = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(ip, IntPtr.Zero, Int32Rect.Empty, System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions())
            Finally
                DeleteObject(ip)
            End Try
            Return _loadBitmap
        End Get
        Set(value As BitmapSource)
            _loadBitmap = value
        End Set
    End Property
#End Region

End Class
