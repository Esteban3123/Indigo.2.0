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
Public Class LoaderConfigurationFile

#Region "Singleton"

    ''' <summary>
    ''' Única instancia de la clase
    ''' </summary>
    Private Shared _instance As LoaderConfigurationFile

    ''' <summary>
    ''' Obtiene la única instancia de la clase
    ''' </summary>
    ''' <returns>Instancia de la clase</returns>
    Public Shared ReadOnly Property Instance As LoaderConfigurationFile
        Get
            If _instance Is Nothing Then
                _instance = New LoaderConfigurationFile()
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
        'Return Path.Combine(Utils.GetApplicationPath, ConfigurationFile.FILE_NAME) '
        If Not AppDomain.CurrentDomain.SetupInformation.ApplicationBase.Contains("WindowsApps") Then
            Return String.Concat(Utils.GetPathConfigApplication, FILE_NAME)
        Else
            Dim nombreArchivo As String = String.Concat(Utils.GetPathConfigApplication, FILE_NAME)
            Dim nombreArchivoReal As String = String.Concat(Utils.GetApplicationPath, FILE_NAME)
            If (Not IO.File.Exists(nombreArchivo)) AndAlso IO.File.Exists(nombreArchivoReal) Then
                Dim _dt As DataTable = CreateDataTable()
                _dt.Rows.Clear()
                _dt.ReadXml(nombreArchivoReal)
                _dt.WriteXml(nombreArchivo)
            End If
            Return nombreArchivo
        End If
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

#End Region

#Region "Builders"

    ''' <summary>
    ''' Obtiene una nueva instancia de la clase
    ''' </summary>
    ''' <param name="dt">DataTable con los parametros del servicio</param>
    Public Sub Initialize(Optional ByVal dt As DataTable = Nothing)
        Dim configurationFile = Base.ConfigurationFile.Instance
        configurationFile.UrlWebSecurityServer = System.Configuration.ConfigurationManager.AppSettings("UrlWebSecurityServices")
        If dt IsNot Nothing Then
            Me._dt = dt
        Else
            Me._dt = CreateDataTable()
            If Not ConfigurationFileExists() Then
                Me._dt.Rows.Add(Me._dt.NewRow())

                configurationFile.CheckForUpdates = False
                configurationFile.Culture = New CultureInfo("es-CO", False)
                configurationFile.DefaultCompanyContainerCode = ""
                configurationFile.DefaultCompanyContainerName = ""
                configurationFile.DefaultCompanyType = 0
                configurationFile.City = ""
                configurationFile.ReportsPath = ""
                configurationFile.LocalReportsPath = ""
                configurationFile.ServerReportsPath = ""
                configurationFile.PathUpdates = ""
                configurationFile.ProtocolUrlDocumentalSystemWebServer = Protocol.basicHttp
                configurationFile.ProtocolUrlWebServer = Protocol.basicHttp
                configurationFile.ProtocolUrlXpoWebServer = Protocol.basicHttp
                configurationFile.TimeOutApp = 60
                configurationFile.LightweightVersion = False
                configurationFile.UrlNotificationWebServer = ""
                configurationFile.UrlDocumentalSystemWebServer = ""
                configurationFile.UrlWebServer = ""
                configurationFile.UrlXpoWebServer = ""
                configurationFile.NotificationEnabled = True
                configurationFile.TypeAlertControl = 1 'Alert Windows
                Exit Sub
            Else
                Me._dt.ReadXml(GetPathConfigurationFile())
            End If
        End If

        If Not IsDBNull(Me._dt.Rows(0).Item("UrlWebServer")) AndAlso Not Me._dt.Rows(0).Item("UrlWebServer").ToString().Trim().Equals(String.Empty) Then
            configurationFile.UrlWebServer = Me._dt.Rows(0).Item("UrlWebServer").ToString().Trim()
        Else
            configurationFile.UrlWebServer = String.Empty
        End If
        If Not IsDBNull(Me._dt.Rows(0).Item("UrlXpoWebServer")) AndAlso Not Me._dt.Rows(0).Item("UrlXpoWebServer").ToString().Trim().Equals(String.Empty) Then
            configurationFile.UrlXpoWebServer = Me._dt.Rows(0).Item("UrlXpoWebServer").ToString().Trim()
        Else
            configurationFile.UrlXpoWebServer = String.Empty
        End If
        If Not IsDBNull(Me._dt.Rows(0).Item("UrlNotificationWebServer")) AndAlso Not Me._dt.Rows(0).Item("UrlNotificationWebServer").ToString().Trim().Equals(String.Empty) Then
            configurationFile.UrlNotificationWebServer = Me._dt.Rows(0).Item("UrlNotificationWebServer").ToString().Trim()
        Else
            configurationFile.UrlWebServer = String.Empty
        End If
        If Not IsDBNull(Me._dt.Rows(0).Item("UrlDocumentalSystemWebServer")) AndAlso Not Me._dt.Rows(0).Item("UrlDocumentalSystemWebServer").ToString().Trim().Equals(String.Empty) Then
            configurationFile.UrlDocumentalSystemWebServer = Me._dt.Rows(0).Item("UrlDocumentalSystemWebServer").ToString().Trim()
        Else
            configurationFile.UrlDocumentalSystemWebServer = String.Empty
        End If
        If Not IsDBNull(Me._dt.Rows(0).Item("UrlIndexingWebServer")) AndAlso Not Me._dt.Rows(0).Item("UrlIndexingWebServer").ToString().Trim().Equals(String.Empty) Then
            configurationFile.UrlIndexingWebServer = Me._dt.Rows(0).Item("UrlIndexingWebServer").ToString().Trim()
        Else
            configurationFile.UrlIndexingWebServer = String.Empty
        End If
        If Not IsDBNull(Me._dt.Rows(0).Item("ProtocolUrlWebServer")) AndAlso Not Me._dt.Rows(0).Item("ProtocolUrlWebServer").ToString().Trim().Equals(String.Empty) Then
            configurationFile.ProtocolUrlWebServer = [Enum].Parse(GetType(Protocol), Me._dt.Rows(0).Item("ProtocolUrlWebServer"))
        Else
            configurationFile.ProtocolUrlWebServer = Protocol.basicHttp
        End If
        If Not IsDBNull(Me._dt.Rows(0).Item("ProtocolUrlXpoWebServer")) AndAlso Not Me._dt.Rows(0).Item("ProtocolUrlXpoWebServer").ToString().Trim().Equals(String.Empty) Then
            configurationFile.ProtocolUrlXpoWebServer = [Enum].Parse(GetType(Protocol), Me._dt.Rows(0).Item("ProtocolUrlXpoWebServer"))
        Else
            configurationFile.ProtocolUrlXpoWebServer = Protocol.basicHttp
        End If
        If Not IsDBNull(Me._dt.Rows(0).Item("ProtocolUrlDocumentalSystemWebServer")) AndAlso Not Me._dt.Rows(0).Item("ProtocolUrlDocumentalSystemWebServer").ToString().Trim().Equals(String.Empty) Then
            configurationFile.ProtocolUrlDocumentalSystemWebServer = [Enum].Parse(GetType(Protocol), Me._dt.Rows(0).Item("ProtocolUrlDocumentalSystemWebServer"))
        Else
            configurationFile.ProtocolUrlDocumentalSystemWebServer = Protocol.basicHttp
        End If
        If Not IsDBNull(Me._dt.Rows(0).Item("ProtocolUrlIndexingWebServer")) AndAlso Not Me._dt.Rows(0).Item("ProtocolUrlIndexingWebServer").ToString().Trim().Equals(String.Empty) Then
            configurationFile.ProtocolUrlIndexingWebServer = [Enum].Parse(GetType(Protocol), Me._dt.Rows(0).Item("ProtocolUrlIndexingWebServer"))
        Else
            configurationFile.ProtocolUrlIndexingWebServer = Protocol.basicHttp
        End If
        If Not IsDBNull(Me._dt.Rows(0).Item("LightweightVersion")) Then
            If Me._dt.Rows(0).Item("LightweightVersion").ToString.Trim().Equals(Boolean.TrueString) Then
                configurationFile.LightweightVersion = True
            Else
                configurationFile.LightweightVersion = False
            End If
        Else
            configurationFile.LightweightVersion = True
        End If
        If Not IsDBNull(Me._dt.Rows(0).Item("NotificationEnabled")) Then
            If Me._dt.Rows(0).Item("NotificationEnabled").ToString.Trim().Equals(Boolean.TrueString) Then
                configurationFile.NotificationEnabled = True
            Else
                configurationFile.NotificationEnabled = False
            End If
        Else
            configurationFile.NotificationEnabled = True
        End If
        If Not IsDBNull(Me._dt.Rows(0).Item("ReportsPath")) AndAlso Not Me._dt.Rows(0).Item("ReportsPath").ToString().Trim().Equals(String.Empty) Then
            configurationFile.ReportsPath = Me._dt.Rows(0).Item("ReportsPath").ToString().Trim()
        Else
            configurationFile.ReportsPath = String.Empty
        End If
        If Not IsDBNull(Me._dt.Rows(0).Item("LocalReportsPath")) AndAlso Not Me._dt.Rows(0).Item("LocalReportsPath").ToString().Trim().Equals(String.Empty) Then
            configurationFile.LocalReportsPath = Me._dt.Rows(0).Item("LocalReportsPath").ToString().Trim()
        Else
            configurationFile.LocalReportsPath = String.Empty
        End If
        If Not IsDBNull(Me._dt.Rows(0).Item("ServerReportsPath")) AndAlso Not Me._dt.Rows(0).Item("ServerReportsPath").ToString().Trim().Equals(String.Empty) Then
            configurationFile.ServerReportsPath = Me._dt.Rows(0).Item("ServerReportsPath").ToString().Trim()
        Else
            configurationFile.ServerReportsPath = String.Empty
        End If
        If Not IsDBNull(Me._dt.Rows(0).Item("CheckForUpdates")) AndAlso Me._dt.Rows(0).Item("CheckForUpdates").ToString.Trim().Equals(Boolean.TrueString) Then
            configurationFile.CheckForUpdates = True
        Else
            configurationFile.CheckForUpdates = False
        End If
        If Not IsDBNull(Me._dt.Rows(0).Item("PathUpdates")) AndAlso Not Me._dt.Rows(0).Item("PathUpdates").ToString().Trim().Equals(String.Empty) Then
            configurationFile.PathUpdates = Me._dt.Rows(0).Item("PathUpdates").ToString().Trim()
        Else
            configurationFile.PathUpdates = String.Empty
        End If
        If Not IsDBNull(Me._dt.Rows(0).Item("TimeOutApp")) Then
            Dim t As Int32
            Int32.TryParse(Me._dt.Rows(0).Item("TimeOutApp").ToString().Trim(), t)
            If t = 0 Then
                configurationFile.TimeOutApp = 60
            Else
                configurationFile.TimeOutApp = t
            End If
        Else
            configurationFile.TimeOutApp = 60
        End If
        If Not IsDBNull(Me._dt.Rows(0).Item("DefaultCompanyContainerCode")) AndAlso Not Me._dt.Rows(0).Item("DefaultCompanyContainerCode").ToString().Trim().Equals(String.Empty) Then
            configurationFile.DefaultCompanyContainerCode = Me._dt.Rows(0).Item("DefaultCompanyContainerCode").ToString().Trim()
        Else
            configurationFile.DefaultCompanyContainerCode = String.Empty
        End If
        If Not IsDBNull(Me._dt.Rows(0).Item("DefaultCompanyContainerName")) AndAlso Not Me._dt.Rows(0).Item("DefaultCompanyContainerName").ToString().Trim().Equals(String.Empty) Then
            configurationFile.DefaultCompanyContainerName = Me._dt.Rows(0).Item("DefaultCompanyContainerName").ToString().Trim()
        Else
            configurationFile.DefaultCompanyContainerName = String.Empty
        End If
        If Not IsDBNull(Me._dt.Rows(0).Item("DefaultCompanyType")) Then
            Dim t As Int32
            Int32.TryParse(Me._dt.Rows(0).Item("DefaultCompanyType").ToString().Trim(), t)
            If t = 0 Then
                configurationFile.DefaultCompanyType = 0
            Else
                configurationFile.DefaultCompanyType = t
            End If
        Else
            configurationFile.DefaultCompanyType = 0
        End If
        If Not IsDBNull(Me._dt.Rows(0).Item("Language")) AndAlso Not Me._dt.Rows(0).Item("Language").ToString().Trim().Equals(String.Empty) Then
            configurationFile.Culture = New CultureInfo(Me._dt.Rows(0).Item("Language").ToString().Trim(), False)
        Else
            configurationFile.Culture = New CultureInfo("es-CO", False)
        End If
        If Not IsDBNull(Me._dt.Rows(0).Item("City")) AndAlso Not Me._dt.Rows(0).Item("City").ToString().Trim().Equals(String.Empty) Then
            configurationFile.City = Me._dt.Rows(0).Item("City").ToString().Trim()
        Else
            configurationFile.City = String.Empty
        End If
        configurationFile.TypeAlertControl = 1 'Alert Windows
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
            .Columns.Add("UrlWebServerCore")
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
            .Columns.Add("DefaultCompanyType")
            .Columns.Add("City")
            .Columns.Add("ReportPathType")
        End With
        Return dt
    End Function

    ''' <summary>
    ''' Graba los cambios realizados en la configuración
    ''' </summary>
    ''' <returns>Valor que indica si se pudo grabar con exito la configuración</returns>
    Public Shadows Function SaveChanges() As Boolean
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

#Region "Procesos"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sc"></param>
    Public Sub SetServiceConfiguration(sc As Domain.Security.Entities.ServiceConfiguration)
        If sc IsNot Nothing AndAlso sc.Id > 0 Then
            Dim configurationFile = Base.ConfigurationFile.Instance
            configurationFile.EHREntityServiceURL = sc.EHREntityServiceURL

            configurationFile.EHRWebServiceURL = sc.EHRWebServiceURL

            configurationFile.EHREntityServiceProtocol = sc.EHREntityServiceProtocol

            configurationFile.EHRWebServiceProtocol = sc.EHRWebServiceProtocol

            configurationFile.UrlNotificationWebServer = sc.NotificationServiceURL

            configurationFile.UrlIndexingWebServer = sc.IndexingServiceURL

            configurationFile.UrlDocumentalSystemWebServer = sc.DocumentServiceURL

            configurationFile.UrlXpoWebServer = sc.XPOServiceURL

            configurationFile.UrlWebServer = sc.EntityServiceURL

            configurationFile.ProtocolUrlDocumentalSystemWebServer = sc.DocumentServiceProtocol

            configurationFile.ProtocolUrlXpoWebServer = sc.XPOServiceProtocol

            configurationFile.ProtocolUrlWebServer = sc.EntityServiceProtocol

            configurationFile.ProtocolUrlIndexingWebServer = sc.IndexingServiceProtocol

            configurationFile.AppFunctionURL = sc.AppFunctionURL

            configurationFile.GetPerfilUbicacionKey = sc.FunctionKey1

            configurationFile.GetProfesionalKey = sc.FunctionKey2

            configurationFile.SaveUserConfigurationKey = sc.FunctionKey3

            configurationFile.GetApplicationSettingsByContainerIdKey = sc.FunctionKey4

            configurationFile.LoginUserCompanyKey = sc.FunctionKey5
        End If
    End Sub

    Public Sub SetUserConfiguration(uc As Domain.Security.Entities.UserConfiguration)
        If uc IsNot Nothing Then
            Dim configurationFile = Base.ConfigurationFile.Instance
            configurationFile.Culture = New CultureInfo(uc.LanguageCulture, False)
            configurationFile.ReportsPath = uc.CustomReportPath
            configurationFile.LightweightVersion = uc.LightweightVersion
            configurationFile.City = uc.ActualCity
            configurationFile.DefaultCompanyContainerCode = uc.Containers.Code
            configurationFile.DefaultCompanyContainerName = uc.Containers.Name
            configurationFile.DefaultCompanyType = uc.Containers.CompanyType
            configurationFile.CenterAttention = uc.CenterAttention
            configurationFile.DefaultConfiguration = uc.DefaultConfiguration
            configurationFile.Dashboard = uc.Dashboard
            configurationFile.FunctionalUnit = uc.FunctionalUnit
            configurationFile.FunctionalUnitName = uc.FunctionalUnitName
            configurationFile.GroupCode = uc.GroupCode
            configurationFile.NameCareCenter = uc.NameCareCenter
            configurationFile.RoleCode = uc.RoleCode
            configurationFile.SideFace = uc.SideFace
            configurationFile.TypeFunctionalUnit = uc.TypeFunctionalUnit
            configurationFile.TypeAlertControl = uc.TypeAlertControl
            configurationFile.ReportPathType = uc.ReportPathType

            _dt.Rows(0).Item("Language") = configurationFile.Culture
            _dt.Rows(0).Item("ReportsPath") = configurationFile.ReportsPath
            _dt.Rows(0).Item("LightweightVersion") = configurationFile.LightweightVersion
            _dt.Rows(0).Item("City") = configurationFile.City
            _dt.Rows(0).Item("DefaultCompanyContainerCode") = configurationFile.DefaultCompanyContainerCode
            _dt.Rows(0).Item("DefaultCompanyContainerName") = configurationFile.DefaultCompanyContainerName
            _dt.Rows(0).Item("DefaultCompanyType") = configurationFile.DefaultCompanyType
        End If
    End Sub
#End Region

End Class
