'***********************************************************************
' Assembly         : Infraestructure.CrossCutting.Base
' Author           : Juan F. Tamayo
' Created          : 2013-09-02
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-09-02
' Description      : Encapsula y administra los datos de configuración almacenados en el archivo de configuración
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Globalization
Imports System.IO

#End Region

''' <summary>
''' Encapsula y administra los datos de configuración almacenados en el archivo de configuración
''' </summary>
Public Class ConfigurationFile

#Region "Consts"

    ''' <summary>
    ''' Nombre de la clave del valor de cabecera enviado
    ''' para el nombre del contenedor a inyectar
    ''' </summary>
    Public Const SESS_CONTAINER As String = "_ContainerName_"

    ''' <summary>
    ''' Nombre de la clave del valor de cabecera enviado
    ''' para el nombre del contenedor HIS a inyectar
    ''' </summary>
    Public Const SESS_CONTAINER_HIS As String = "_ContainerHISName_"

    ''' <summary>
    ''' Nombre de la clave del valor de cabecera enviado
    ''' para el nombre del contenedor de interoperabilidad de costros a inyectar
    ''' </summary>
    Public Const SESS_CONTAINER_INTEROPCOST As String = "_ContainerInteropCostName_"

    ''' <summary>
    ''' Nombre de la clave del valor de cabecera enviado
    ''' para el id de la secuencia a usar
    ''' </summary>
    Public Const SESS_IDSEQUENSE As String = "_IdSequense_"

    ''' <summary>
    ''' Nombre de la clave del valor de cabecera enviado
    ''' para la secuencia de cabecera a usar
    ''' </summary>
    Public Const SESS_SEQUENCEC As String = "_SequenceC_"

    ''' <summary>
    ''' Nombre de la clave del valor de cabecera enviado
    ''' para el mensage de auditoria
    ''' </summary>
    Public Const SESS_AUDITMESSAGE As String = "_AuditMessage_"

    ''' <summary>
    ''' Espacio de nombre usado para almacenar los valores
    ''' enviados por la cabecera
    ''' </summary>
    Public Const SESS_NAME_SPACE As String = "ns"

    ''' <summary>
    ''' Nombre de la cadena de conexion a la base transaccional
    ''' </summary>
    Public Const CONX_GENESIS As String = "CONX_GENESIS"

    ''' <summary>
    ''' Nombre del parametro en web.config que contiene el nombre del contenedor de seguridad
    ''' </summary>
    Public Const SECURITY_CONTAINER_PARAMETER_NAME As String = "containerSecurity"

    ''' <summary>
    ''' Nombre del parametro en web.config que contiene el nombre del contenedor de Indigo Crystal HIS
    ''' </summary>
    Public Const HIS_CONTAINER_PARAMETER_NAME As String = "containerHis"

    ''' <summary>
    ''' Nombre del parametro en web.config que contiene el nombre del contenedor de Indigo Crystal HIS
    ''' </summary>
    Public Const INTEROP_COST_CONTAINER_PARAMETER_NAME As String = "containerInteropCost"

#End Region

#Region "Singleton"

    ''' <summary>
    ''' Única instancia de la clase
    ''' </summary>
    Private Shared _instance As ConfigurationFile

    ''' <summary>
    ''' Obtiene la única instancia de la clase
    ''' </summary>
    ''' <returns>Instancia de la clase</returns>
    Public Shared ReadOnly Property Instance As ConfigurationFile
        Get
            If _instance Is Nothing Then
                _instance = New ConfigurationFile()
            End If
            Return _instance
        End Get
    End Property

#End Region

#Region "Shared"

    ''' <summary>
    ''' Nombre del archivo de configuración
    ''' </summary>
    Public Const FILE_NAME As String = "Indigo.Config"

    ''' <summary>
    ''' Obtiene la ruta completa del archivo de configuración del cliente
    ''' </summary>
    ''' <returns>Ruta del archivo de configuración</returns>
    Public Shared Function GetPathConfigurationFile() As String
        Return Path.Combine(Utils.GetApplicationPath, ConfigurationFile.FILE_NAME)
    End Function

    ''' <summary>
    ''' Obtiene un valor que indica si existe un archivo de configuración
    ''' </summary>
    ''' <returns>Valor que indica si existe un archivo de configuración</returns>
    Public Shared Function ConfigurationFileExists() As Boolean
        Return File.Exists(GetPathConfigurationFile())
    End Function

#End Region

#Region "Fields & Properties"

    ''' <summary>
    ''' Encapsula la estructura del archivo de configuración
    ''' </summary>
    Private _dt As DataTable
    ''' <summary>
    ''' Asigna el DataTable de configuración
    ''' </summary>
    Public WriteOnly Property DT As DataTable
        Set(value As DataTable)
            If value IsNot Nothing Then
                _instance = Nothing
                _instance = New ConfigurationFile(value)
            End If
        End Set
    End Property

    Private _culture As CultureInfo
    ''' <summary>
    ''' Obtiene la cultura usada en la aplicación
    ''' </summary>
    ''' <returns>Cultura de la aplicación</returns>
    Public Property Culture As CultureInfo
        Get
            Return Me._culture
        End Get
        Set(value As CultureInfo)
            If value IsNot Nothing Then
                Me._culture = value
                Me._dt.Rows(0).Item("Language") = value.Name
            End If
        End Set
    End Property

    Private _urlWebServer As String
    ''' <summary>
    ''' Obtiene la dirección URL al servidor web de servicios de negocio
    ''' </summary>
    ''' <returns>Dirección URL al servidor web de servicios de negocio</returns>
    Public Property UrlWebServer As String
        Get
            Return Me._urlWebServer
        End Get
        Set(value As String)
            If value IsNot Nothing Then
                Me._urlWebServer = value.Trim()
                Me._dt.Rows(0).Item("UrlWebServer") = value.Trim()
            End If
        End Set
    End Property

    Private _urlXpoWebServer As String
    ''' <summary>
    ''' Obtiene la dirección URL al servidor web de servicios de Xpo
    ''' </summary>
    ''' <returns>Dirección URL al servidor web de servicios de Xpo</returns>
    Public Property UrlXpoWebServer As String
        Get
            Return Me._urlXpoWebServer
        End Get
        Set(value As String)
            If value IsNot Nothing Then
                Me._urlXpoWebServer = value.Trim()
                Me._dt.Rows(0).Item("UrlXpoWebServer") = value.Trim()
            End If
        End Set
    End Property

    Private _urlNotificationWebServer As String
    ''' <summary>
    ''' Obtiene la dirección URL al servidor web de servicios de notificacion
    ''' </summary>
    ''' <returns>Dirección URL al servidor web de servicios de notificacion</returns>
    Public Property UrlNotificationWebServer As String
        Get
            Return Me._urlNotificationWebServer
        End Get
        Set(value As String)
            If value IsNot Nothing Then
                Me._urlNotificationWebServer = value.Trim()
                Me._dt.Rows(0).Item("UrlNotificationWebServer") = value.Trim()
            End If
        End Set
    End Property

    Private _urlDocumentalSystemWebServer As String
    ''' <summary>
    ''' Obtiene la dirección URL al servidor web de servicios del sistema documental. 
    ''' </summary>
    ''' <returns>Dirección URL al servidor web de servicios del sistema documental</returns>
    Public Property UrlDocumentalSystemWebServer As String
        Get
            Return Me._urlDocumentalSystemWebServer
        End Get
        Set(value As String)
            If value IsNot Nothing Then
                Me._urlDocumentalSystemWebServer = value.Trim()
                Me._dt.Rows(0).Item("UrlDocumentalSystemWebServer") = value.Trim()
            End If
        End Set
    End Property

    Private _urlIndexingWebServer As String
    ''' <summary>
    ''' Obtiene la dirección URL al servidor web de servicios de indexación
    ''' </summary>
    ''' <returns>Dirección URL al servidor web de servicios de indexación</returns>
    Public Property UrlIndexingWebServer As String
        Get
            Return Me._urlIndexingWebServer
        End Get
        Set(value As String)
            If value IsNot Nothing Then
                Me._urlIndexingWebServer = value.Trim()
                Me._dt.Rows(0).Item("UrlIndexingWebServer") = value.Trim()
            End If
        End Set
    End Property

    Private _protocolUrlWebServer As Protocol
    ''' <summary>
    ''' Obtiene el protocolo usado para la comunicación con los servicios de negocio
    ''' </summary>
    ''' <returns>Protocolo usado para la comunicación con los servicios de negocio</returns>
    Public Property ProtocolUrlWebServer As Protocol
        Get
            Return Me._protocolUrlWebServer
        End Get
        Set(value As Protocol)
            Me._protocolUrlWebServer = value
            Me._dt.Rows(0).Item("ProtocolUrlWebServer") = value.ToString()
        End Set
    End Property

    Private _protocolUrlXpoWebServer As Protocol
    ''' <summary>
    ''' Obtiene el protocolo usado para la comunicación con los servicios de Xpo
    ''' </summary>
    ''' <returns>Protocolo usado para la comunicación con los servicios de Xpo</returns>
    Public Property ProtocolUrlXpoWebServer As Protocol
        Get
            Return Me._protocolUrlXpoWebServer
        End Get
        Set(value As Protocol)
            Me._protocolUrlXpoWebServer = value
            Me._dt.Rows(0).Item("ProtocolUrlXpoWebServer") = value.ToString()
        End Set
    End Property

    Private _protocolUrlDocumentalSystemWebServer As Protocol
    ''' <summary>
    ''' Obtiene el protocolo usado para la comunicación con los servicios del sistema documental.
    ''' </summary>
    ''' <returns>Protocolo usado para la comunicación con los servicios del sistema documental</returns>
    Public Property ProtocolUrlDocumentalSystemWebServer As Protocol
        Get
            Return Me._protocolUrlDocumentalSystemWebServer
        End Get
        Set(value As Protocol)
            Me._protocolUrlDocumentalSystemWebServer = value
            Me._dt.Rows(0).Item("ProtocolUrlDocumentalSystemWebServer") = value.ToString()
        End Set
    End Property

    Private _protocolUrlIndexingWebServer As Protocol
    ''' <summary>
    ''' Obtiene el protocolo usado para la comunicación con los servicios de indexación.
    ''' </summary>
    ''' <returns>Protocolo usado para la comunicación con los servicios de indexación</returns>
    Public Property ProtocolUrlIndexingWebServer As Protocol
        Get
            Return Me._protocolUrlIndexingWebServer
        End Get
        Set(value As Protocol)
            Me._protocolUrlIndexingWebServer = value
            Me._dt.Rows(0).Item("ProtocolUrlIndexingWebServer") = value.ToString()
        End Set
    End Property

    Private _reportsPath As String
    ''' <summary>
    ''' Obtiene la ruta de las definiciones de los reportes 
    ''' </summary>
    ''' <returns>Ruta de los reportes</returns>
    Public Property ReportsPath As String
        Get
            Return Me._reportsPath
        End Get
        Set(value As String)
            If value IsNot Nothing Then
                Me._reportsPath = value.Trim()
                Me._dt.Rows(0).Item("ReportsPath") = value.Trim()
            End If
        End Set
    End Property

    Private _localReportsPath As String
    ''' <summary>
    ''' Obtiene la ruta local de las definiciones de los reportes 
    ''' </summary>
    ''' <returns>Ruta local de los reportes</returns>
    Public Property LocalReportsPath As String
        Get
            Return Me._localReportsPath
        End Get
        Set(value As String)
            If value IsNot Nothing Then
                Me._localReportsPath = value.Trim()
                Me._dt.Rows(0).Item("LocalReportsPath") = value.Trim()
            End If
        End Set
    End Property

    Private _serverReportsPath As String
    ''' <summary>
    ''' Obtiene la ruta en el servidor de las definiciones de los reportes 
    ''' </summary>
    ''' <returns>Ruta en el servidor de los reportes</returns>
    Public Property ServerReportsPath As String
        Get
            Return Me._serverReportsPath
        End Get
        Set(value As String)
            If value IsNot Nothing Then
                Me._serverReportsPath = value.Trim()
                Me._dt.Rows(0).Item("ServerReportsPath") = value.Trim()
            End If
        End Set
    End Property

    Private _pathUpdates As String
    ''' <summary>
    ''' Obtiene la ruta de las actualizaciones
    ''' </summary>
    ''' <returns>Ruta de las actualizaciones</returns>
    Public Property PathUpdates As String
        Get
            Return Me._pathUpdates
        End Get
        Set(value As String)
            If value IsNot Nothing Then
                Me._pathUpdates = value.Trim()
                Me._dt.Rows(0).Item("PathUpdates") = value.Trim()
            End If
        End Set
    End Property

    Private _checkForUpdates As Boolean
    ''' <summary>
    ''' Obtiene un valor que indica si se realiza chequeo por nuevas actualizaciones
    ''' </summary>
    ''' <returns>Valor que indica si se realiza chequeo por nuevas actualizaciones</returns>
    Public Property CheckForUpdates As Boolean
        Get
            Return Me._checkForUpdates
        End Get
        Set(value As Boolean)
            Me._checkForUpdates = value
            Me._dt.Rows(0).Item("CheckForUpdates") = value.ToString()
        End Set
    End Property

    Private _lightweightVersion As Boolean
    ''' <summary>
    ''' Obtiene un valor que indica si se usa la versión liviana o completa
    ''' </summary>
    ''' <returns>Valor que indica si se usa la versión liviana o completa</returns>
    Public Property LightweightVersion As Boolean
        Get
            Return Me._lightweightVersion
        End Get
        Set(value As Boolean)
            Me._lightweightVersion = value
            Me._dt.Rows(0).Item("LightweightVersion") = value.ToString()
        End Set
    End Property

    Private _notificationEnabled As Boolean
    ''' <summary>
    ''' Obtiene un valor que indica si se habilita la caracteriastica de notificación
    ''' </summary>
    ''' <returns>Valor que indica si se habilita la notificación</returns>
    Public Property NotificationEnabled As Boolean
        Get
            Return Me._notificationEnabled
        End Get
        Set(value As Boolean)
            Me._notificationEnabled = value
            Me._dt.Rows(0).Item("NotificationEnabled") = value.ToString()
        End Set
    End Property

    Private _timeOutApp As Integer
    ''' <summary>
    ''' Obtiene el tiempo máximo de espera usado por la aplicación antes de bloquear la sesión por inactividad
    ''' </summary>
    ''' <returns>El tiempo máximo de espera</returns>
    Public Property TimeOutApp As Integer
        Get
            Return Me._timeOutApp
        End Get
        Set(value As Integer)
            Me._timeOutApp = value
            Me._dt.Rows(0).Item("TimeOutApp") = Me._timeOutApp
        End Set
    End Property

    Private _defaultCompanyContainerCode As String
    ''' <summary>
    ''' Obtiene el código del contenedor usado por defecto para las consultas a los servicios de negocio
    ''' </summary>
    ''' <returns>Código del contenedor usado por defecto para las consultas a los servicios de negocio</returns>
    Public Property DefaultCompanyContainerCode As String
        Get
            Return Me._defaultCompanyContainerCode
        End Get
        Set(value As String)
            If value IsNot Nothing Then
                Me._defaultCompanyContainerCode = value.Trim()
                Me._dt.Rows(0).Item("DefaultCompanyContainerCode") = value.Trim()
            End If
        End Set
    End Property

    Private _defaultCompanyContainerName As String
    ''' <summary>
    ''' Obtiene el nombre del contenedor usado para seguridad
    ''' </summary>
    ''' <returns>Nombre del contenedor usado para seguridad</returns>
    Public Property DefaultCompanyContainerName As String
        Get
            Return Me._defaultCompanyContainerName
        End Get
        Set(value As String)
            If value IsNot Nothing Then
                Me._defaultCompanyContainerName = value.Trim()
                Me._dt.Rows(0).Item("DefaultCompanyContainerName") = value.Trim()
            End If
        End Set
    End Property

    Private _City As String
    Public Property City As String
        Get
            Return Me._City
        End Get
        Set(value As String)
            If value IsNot Nothing Then
                Me._City = value.Trim()
                Me._dt.Rows(0).Item("City") = value.Trim()
            End If
        End Set
    End Property

#End Region

#Region "Builders"

    ''' <summary>
    ''' Obtiene una nueva instancia de la clase
    ''' </summary>
    ''' <param name="dt">DataTable con los parametros del servicio</param>
    Private Sub New(Optional ByVal dt As DataTable = Nothing)
        If dt IsNot Nothing Then
            Me._dt = dt
        Else
            Me._dt = CreateDataTable()
            If Not ConfigurationFileExists() Then
                Me._dt.Rows.Add(Me._dt.NewRow())
                Me._checkForUpdates = False
                Me._culture = New CultureInfo("es-CO", False)
                Me._defaultCompanyContainerCode = ""
                Me._defaultCompanyContainerName = ""
                Me._City = ""
                Me._reportsPath = ""
                Me._localReportsPath = ""
                Me._serverReportsPath = ""
                Me._pathUpdates = ""
                Me._protocolUrlDocumentalSystemWebServer = Protocol.basicHttp
                Me._protocolUrlWebServer = Protocol.basicHttp
                Me._protocolUrlXpoWebServer = Protocol.basicHttp
                Me._timeOutApp = 60
                Me._lightweightVersion = False
                Me._urlNotificationWebServer = ""
                Me._urlDocumentalSystemWebServer = ""
                Me._urlWebServer = ""
                Me._urlXpoWebServer = ""
                Me._notificationEnabled = True
                Exit Sub
            Else
                Me._dt.ReadXml(GetPathConfigurationFile())
            End If
        End If

        If Not IsDBNull(Me._dt.Rows(0).Item("UrlWebServer")) AndAlso Not Me._dt.Rows(0).Item("UrlWebServer").ToString().Trim().Equals(String.Empty) Then
            Me._urlWebServer = Me._dt.Rows(0).Item("UrlWebServer").ToString().Trim()
        Else
            Me._urlWebServer = String.Empty
        End If
        If Not IsDBNull(Me._dt.Rows(0).Item("UrlXpoWebServer")) AndAlso Not Me._dt.Rows(0).Item("UrlXpoWebServer").ToString().Trim().Equals(String.Empty) Then
            Me._urlXpoWebServer = Me._dt.Rows(0).Item("UrlXpoWebServer").ToString().Trim()
        Else
            Me._urlXpoWebServer = String.Empty
        End If
        If Not IsDBNull(Me._dt.Rows(0).Item("UrlNotificationWebServer")) AndAlso Not Me._dt.Rows(0).Item("UrlNotificationWebServer").ToString().Trim().Equals(String.Empty) Then
            Me._urlNotificationWebServer = Me._dt.Rows(0).Item("UrlNotificationWebServer").ToString().Trim()
        Else
            Me._urlWebServer = String.Empty
        End If
        If Not IsDBNull(Me._dt.Rows(0).Item("UrlDocumentalSystemWebServer")) AndAlso Not Me._dt.Rows(0).Item("UrlDocumentalSystemWebServer").ToString().Trim().Equals(String.Empty) Then
            Me._urlDocumentalSystemWebServer = Me._dt.Rows(0).Item("UrlDocumentalSystemWebServer").ToString().Trim()
        Else
            Me._urlDocumentalSystemWebServer = String.Empty
        End If
        If Not IsDBNull(Me._dt.Rows(0).Item("UrlIndexingWebServer")) AndAlso Not Me._dt.Rows(0).Item("UrlIndexingWebServer").ToString().Trim().Equals(String.Empty) Then
            Me._urlIndexingWebServer = Me._dt.Rows(0).Item("UrlIndexingWebServer").ToString().Trim()
        Else
            Me._urlIndexingWebServer = String.Empty
        End If
        If Not IsDBNull(Me._dt.Rows(0).Item("ProtocolUrlWebServer")) AndAlso Not Me._dt.Rows(0).Item("ProtocolUrlWebServer").ToString().Trim().Equals(String.Empty) Then
            Me._protocolUrlWebServer = [Enum].Parse(GetType(Protocol), Me._dt.Rows(0).Item("ProtocolUrlWebServer"))
        Else
            Me._protocolUrlWebServer = Protocol.basicHttp
        End If
        If Not IsDBNull(Me._dt.Rows(0).Item("ProtocolUrlXpoWebServer")) AndAlso Not Me._dt.Rows(0).Item("ProtocolUrlXpoWebServer").ToString().Trim().Equals(String.Empty) Then
            Me._protocolUrlXpoWebServer = [Enum].Parse(GetType(Protocol), Me._dt.Rows(0).Item("ProtocolUrlXpoWebServer"))
        Else
            Me._protocolUrlXpoWebServer = Protocol.basicHttp
        End If
        If Not IsDBNull(Me._dt.Rows(0).Item("ProtocolUrlDocumentalSystemWebServer")) AndAlso Not Me._dt.Rows(0).Item("ProtocolUrlDocumentalSystemWebServer").ToString().Trim().Equals(String.Empty) Then
            Me._protocolUrlDocumentalSystemWebServer = [Enum].Parse(GetType(Protocol), Me._dt.Rows(0).Item("ProtocolUrlDocumentalSystemWebServer"))
        Else
            Me._protocolUrlDocumentalSystemWebServer = Protocol.basicHttp
        End If
        If Not IsDBNull(Me._dt.Rows(0).Item("ProtocolUrlIndexingWebServer")) AndAlso Not Me._dt.Rows(0).Item("ProtocolUrlIndexingWebServer").ToString().Trim().Equals(String.Empty) Then
            Me._protocolUrlIndexingWebServer = [Enum].Parse(GetType(Protocol), Me._dt.Rows(0).Item("ProtocolUrlIndexingWebServer"))
        Else
            Me._protocolUrlIndexingWebServer = Protocol.basicHttp
        End If
        If Not IsDBNull(Me._dt.Rows(0).Item("LightweightVersion")) Then
            If Me._dt.Rows(0).Item("LightweightVersion").ToString.Trim().Equals(Boolean.TrueString) Then
                Me._lightweightVersion = True
            Else
                Me._lightweightVersion = False
            End If
        Else
            Me._lightweightVersion = True
        End If
        If Not IsDBNull(Me._dt.Rows(0).Item("NotificationEnabled")) Then
            If Me._dt.Rows(0).Item("NotificationEnabled").ToString.Trim().Equals(Boolean.TrueString) Then
                Me._notificationEnabled = True
            Else
                Me._notificationEnabled = False
            End If
        Else
            Me._notificationEnabled = True
        End If
        If Not IsDBNull(Me._dt.Rows(0).Item("ReportsPath")) AndAlso Not Me._dt.Rows(0).Item("ReportsPath").ToString().Trim().Equals(String.Empty) Then
            Me._reportsPath = Me._dt.Rows(0).Item("ReportsPath").ToString().Trim()
        Else
            Me._reportsPath = String.Empty
        End If
        If Not IsDBNull(Me._dt.Rows(0).Item("LocalReportsPath")) AndAlso Not Me._dt.Rows(0).Item("LocalReportsPath").ToString().Trim().Equals(String.Empty) Then
            Me._localReportsPath = Me._dt.Rows(0).Item("LocalReportsPath").ToString().Trim()
        Else
            Me._localReportsPath = String.Empty
        End If
        If Not IsDBNull(Me._dt.Rows(0).Item("ServerReportsPath")) AndAlso Not Me._dt.Rows(0).Item("ServerReportsPath").ToString().Trim().Equals(String.Empty) Then
            Me._serverReportsPath = Me._dt.Rows(0).Item("ServerReportsPath").ToString().Trim()
        Else
            Me._serverReportsPath = String.Empty
        End If
        If Not IsDBNull(Me._dt.Rows(0).Item("CheckForUpdates")) AndAlso Me._dt.Rows(0).Item("CheckForUpdates").ToString.Trim().Equals(Boolean.TrueString) Then
            Me._checkForUpdates = True
        Else
            Me._checkForUpdates = False
        End If
        If Not IsDBNull(Me._dt.Rows(0).Item("PathUpdates")) AndAlso Not Me._dt.Rows(0).Item("PathUpdates").ToString().Trim().Equals(String.Empty) Then
            Me._pathUpdates = Me._dt.Rows(0).Item("PathUpdates").ToString().Trim()
        Else
            Me._pathUpdates = String.Empty
        End If
        If Not IsDBNull(Me._dt.Rows(0).Item("TimeOutApp")) Then
            Dim t As Int32
            Int32.TryParse(Me._dt.Rows(0).Item("TimeOutApp").ToString().Trim(), t)
            If t = 0 Then
                Me._timeOutApp = 60
            Else
                Me._timeOutApp = t
            End If
        Else
            Me._timeOutApp = 60
        End If
        If Not IsDBNull(Me._dt.Rows(0).Item("DefaultCompanyContainerCode")) AndAlso Not Me._dt.Rows(0).Item("DefaultCompanyContainerCode").ToString().Trim().Equals(String.Empty) Then
            Me._defaultCompanyContainerCode = Me._dt.Rows(0).Item("DefaultCompanyContainerCode").ToString().Trim()
        Else
            Me._defaultCompanyContainerCode = String.Empty
        End If
        If Not IsDBNull(Me._dt.Rows(0).Item("DefaultCompanyContainerName")) AndAlso Not Me._dt.Rows(0).Item("DefaultCompanyContainerName").ToString().Trim().Equals(String.Empty) Then
            Me._defaultCompanyContainerName = Me._dt.Rows(0).Item("DefaultCompanyContainerName").ToString().Trim()
        Else
            Me._defaultCompanyContainerName = String.Empty
        End If
        If Not IsDBNull(Me._dt.Rows(0).Item("Language")) AndAlso Not Me._dt.Rows(0).Item("Language").ToString().Trim().Equals(String.Empty) Then
            Me._culture = New CultureInfo(Me._dt.Rows(0).Item("Language").ToString().Trim(), False)
        Else
            Me._culture = New CultureInfo("es-CO", False)
        End If
        If Not IsDBNull(Me._dt.Rows(0).Item("City")) AndAlso Not Me._dt.Rows(0).Item("City").ToString().Trim().Equals(String.Empty) Then
            Me._City = Me._dt.Rows(0).Item("City").ToString().Trim()
        Else
            Me._City = String.Empty
        End If
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Crea un DataTable con la estructura necesaria para el archivo de configuración
    ''' </summary>
    ''' <returns>DataTable</returns>
    Public Shared Function CreateDataTable() As DataTable
        Dim dt As New DataTable("GenesisConfiguration")
        With dt
            .Columns.Add("Language")
            .Columns.Add("UrlWebServer")
            .Columns.Add("UrlXpoWebServer")
            .Columns.Add("UrlNotificationWebServer")
            .Columns.Add("UrlDocumentalSystemWebServer")
            .Columns.Add("UrlIndexingWebServer")
            .Columns.Add("ProtocolUrlWebServer")
            .Columns.Add("ProtocolUrlXpoWebServer")
            .Columns.Add("ProtocolUrlDocumentalSystemWebServer")
            .Columns.Add("ProtocolUrlIndexingWebServer")
            .Columns.Add("ReportsPath")
            .Columns.Add("LocalReportsPath")
            .Columns.Add("ServerReportsPath")
            .Columns.Add("PathUpdates")
            .Columns.Add("CheckForUpdates")
            .Columns.Add("LightweightVersion")
            .Columns.Add("NotificationEnabled")
            .Columns.Add("TimeOutApp")
            .Columns.Add("DefaultCompanyContainerCode")
            .Columns.Add("DefaultCompanyContainerName")
            .Columns.Add("City")
        End With
        Return dt
    End Function

    ''' <summary>
    ''' Graba los cambios realizados en la configuración
    ''' </summary>
    ''' <returns>Valor que indica si se pudo grabar con exito la configuración</returns>
    Public Function SaveChanges() As Boolean
        Try
            If Not Directory.Exists(Utils.GetPathConfigApplication()) Then
                Directory.CreateDirectory(Utils.GetPathConfigApplication())
            End If
            If ConfigurationFileExists() Then
                File.Delete(GetPathConfigurationFile())
            End If
            Me._dt.WriteXml(GetPathConfigurationFile())
            Return True
        Catch
            Return False
        End Try
    End Function

#End Region

End Class
