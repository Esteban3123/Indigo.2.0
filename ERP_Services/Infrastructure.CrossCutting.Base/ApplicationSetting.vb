'***********************************************************************
' Assembly         : Infraestructure.CrossCutting.Base
' Author           : Hector Rodriguez Rubiano
' Created          : 2021-01-26
'
' Description      : Encapsula y administra los datos de configuración de la aplicacion en la base de datos
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************


Imports System.Runtime.Serialization
Imports Domain.Security.Entities

<DataContract()>
Public Class ApplicationSetting
#Region "Builders"

    Private Shared _instance As ApplicationSetting
    ''' <summary>
    ''' Esta Propiedad instancia la clase IndigoSingleton por una unica vez.	
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property Instance As ApplicationSetting
        Get
            If _instance Is Nothing Then
                _instance = New ApplicationSetting()
            End If
            Return _instance
        End Get
    End Property

#End Region

#Region "Consts"
    Public Const DF_HISAssemblyFileName As String = "Indigo.Crystal.dll" '"Indigo Crystal.exe"
    Public Const DF_Tenant As String = "indigoauth.onmicrosoft.com"
    Public Const DF_SignUpSignIn As String = "B2C_1A_signup_signin_VIE"
    Public Const DF_ClientId = "{b1a4f6e2-569e-4c26-a535-b777454dd290}"
    Public Const DF_RedirectUri As String = "https://indigoauth.b2clogin.com/oauth2/nativeclient"
    Public Const DF_SessionLogoutUrl As String = "https://www.indigo.ms"
    Public Const DF_ChangeMessageBox As String = "0"
    Public Const DF_DuraccionCacheLarga As Short = 180
    Public Const DF_DuraccionCacheMedia As Short = 60
    Public Const DF_DuraccionCacheCorta As Short = 10
    Public Const DF_NombreCache As String = "IndigoCrystal"
    Public Const DF_PasswordReset As String = "B2C_1_IndigoReset"
    Public Const DF_ZEFLicenseName As String = "5699;101-indigo.tech"
    Public Const DF_ZEFLicenseKey = "{8f8224e0-03b7-c6d0-5877-84cb236c0b33}"
#End Region

#Region "Propiedades"
    Private _HISAssemblyFileName As String
    <DataMember()>
    Public Property HISAssemblyFileName() As String
        Get
            Return _HISAssemblyFileName
        End Get
        Set(ByVal value As String)
            _HISAssemblyFileName = value
        End Set
    End Property

    Private _Tenant As String
    <DataMember()>
    Public Property Tenant() As String
        Get
            Return _Tenant
        End Get
        Set(ByVal value As String)
            _Tenant = value
        End Set
    End Property

    Private _SignUpSignIn As String
    <DataMember()>
    Public Property SignUpSignIn() As String
        Get
            Return _SignUpSignIn
        End Get
        Set(ByVal value As String)
            _SignUpSignIn = value
        End Set
    End Property

    Private _PasswordReset As String
    <DataMember()>
    Public Property PasswordReset() As String
        Get
            Return _PasswordReset
        End Get
        Set(ByVal value As String)
            _PasswordReset = value
        End Set
    End Property


    Private _ClientId As Guid?
    Public Property ClientId() As Guid?
        Get
            Return _ClientId
        End Get
        Set(ByVal value As Guid?)
            _ClientId = value
        End Set
    End Property

    Private _RedirectUri As String
    Public Property RedirectUri() As String
        Get
            Return _RedirectUri
        End Get
        Set(ByVal value As String)
            _RedirectUri = value
        End Set
    End Property
    Private _SessionLogoutUrl As String
    Public Property SessionLogoutUrl() As String
        Get
            Return _SessionLogoutUrl
        End Get
        Set(ByVal value As String)
            _SessionLogoutUrl = value
        End Set
    End Property
    Private _UrlServiceDispensing As String
    Public Property UrlServiceDispensing() As String
        Get
            Return _UrlServiceDispensing
        End Get
        Set(ByVal value As String)
            _UrlServiceDispensing = value
        End Set
    End Property
    Private _ChangeMessageBox As String
    Public Property ChangeMessageBox() As String
        Get
            Return _ChangeMessageBox
        End Get
        Set(ByVal value As String)
            _ChangeMessageBox = value
        End Set
    End Property
    Private _NewServices As Boolean
    Public Property NewServices() As Boolean
        Get
            Return _NewServices
        End Get
        Set(ByVal value As Boolean)
            _NewServices = value
        End Set
    End Property
    Private _CachedEnable As Boolean
    Public Property CachedEnable() As Boolean
        Get
            Return _CachedEnable
        End Get
        Set(ByVal value As Boolean)
            _CachedEnable = value
        End Set
    End Property
    Private _BaseDatosMongo As String
    Public Property BaseDatosMongo() As String
        Get
            Return _BaseDatosMongo
        End Get
        Set(ByVal value As String)
            _BaseDatosMongo = value
        End Set
    End Property
    Private _ServidorMongo As String
    Public Property ServidorMongo() As String
        Get
            Return _ServidorMongo
        End Get
        Set(ByVal value As String)
            _ServidorMongo = value
        End Set
    End Property
    Private _DuraccionCacheLarga As Short
    Public Property DuraccionCacheLarga() As Short
        Get
            Return _DuraccionCacheLarga
        End Get
        Set(ByVal value As Short)
            _DuraccionCacheLarga = value
        End Set
    End Property
    Private _DuraccionCacheMedia As Short
    Public Property DuraccionCacheMedia() As Short
        Get
            Return _DuraccionCacheMedia
        End Get
        Set(ByVal value As Short)
            _DuraccionCacheMedia = value
        End Set
    End Property
    Private _DuraccionCacheCorta As Short
    Public Property DuraccionCacheCorta() As Short
        Get
            Return _DuraccionCacheCorta
        End Get
        Set(ByVal value As Short)
            _DuraccionCacheCorta = value
        End Set
    End Property
    Private _CachePort As String
    Public Property CachePort() As String
        Get
            Return _CachePort
        End Get
        Set(ByVal value As String)
            _CachePort = value
        End Set
    End Property
    Private _CacheServerName As String
    Public Property CacheServerName() As String
        Get
            Return _CacheServerName
        End Get
        Set(ByVal value As String)
            _CacheServerName = value
        End Set
    End Property
    Private _ContainerId As Integer
    Public Property ContainerId() As Integer
        Get
            Return _ContainerId
        End Get
        Set(ByVal value As Integer)
            _ContainerId = value
        End Set
    End Property
    Private _NombreCache As String
    Public Property NombreCache() As String
        Get
            Return _NombreCache
        End Get
        Set(ByVal value As String)
            _NombreCache = value
        End Set
    End Property
    Private _ShowThemeSkinSelector As Boolean
    Public Property ShowThemeSkinSelector() As Boolean
        Get
            Return _ShowThemeSkinSelector
        End Get
        Set(ByVal value As Boolean)
            _ShowThemeSkinSelector = value
        End Set
    End Property
    Private _ReporteadorActivo As Boolean
    Public Property ReporteadorActivo() As Boolean
        Get
            Return _ReporteadorActivo
        End Get
        Set(ByVal value As Boolean)
            _ReporteadorActivo = value
        End Set
    End Property
    Private _LoginAzure As Boolean
    Public Property LoginAzure() As Boolean
        Get
            Return _LoginAzure
        End Get
        Set(ByVal value As Boolean)
            _LoginAzure = value
        End Set
    End Property
    Private _AppFunctionURL As String
    Public Property AppFunctionURL() As String
        Get
            Return _AppFunctionURL
        End Get
        Set(ByVal value As String)
            _AppFunctionURL = value
        End Set
    End Property
    Private _FunctionKey1 As String
    Public Property GetUserByEmailKey() As String
        Get
            Return _FunctionKey1
        End Get
        Set(ByVal value As String)
            _FunctionKey1 = value
        End Set
    End Property
    Private _FunctionKey2 As String
    Public Property GetUserConfigurationByUserIdKey() As String
        Get
            Return _FunctionKey2
        End Get
        Set(ByVal value As String)
            _FunctionKey2 = value
        End Set
    End Property
    Private _ZEFLicenseName As String
    Public Property ZEFLicenseName() As String
        Get
            Return _ZEFLicenseName
        End Get
        Set(ByVal value As String)
            _ZEFLicenseName = value
        End Set
    End Property
    Private _ZEFLicenseKey As Guid?
    Public Property ZEFLicenseKey() As Guid?
        Get
            Return _ZEFLicenseKey
        End Get
        Set(ByVal value As Guid?)
            _ZEFLicenseKey = value
        End Set
    End Property

#End Region

#Region "Procesos"
    Public Sub SetDefaultValuesGC()
        If String.IsNullOrEmpty(_HISAssemblyFileName) Then
            HISAssemblyFileName = DF_HISAssemblyFileName
        End If
        If String.IsNullOrEmpty(_Tenant) Then
            Tenant = DF_Tenant
        End If
        If String.IsNullOrEmpty(_SignUpSignIn) Then
            SignUpSignIn = DF_SignUpSignIn
        End If
        If String.IsNullOrEmpty(_PasswordReset) Then
            PasswordReset = DF_PasswordReset
        End If
        If _ClientId Is Nothing Then
            ClientId = New Guid(DF_ClientId)
        End If
        If String.IsNullOrEmpty(_RedirectUri) Then
            RedirectUri = DF_RedirectUri
        End If
        If String.IsNullOrEmpty(_SessionLogoutUrl) Then
            SessionLogoutUrl = DF_SessionLogoutUrl
        End If
        If String.IsNullOrEmpty(_ZEFLicenseName) Then
            ZEFLicenseName = DF_ZEFLicenseName
        End If
        If _ZEFLicenseKey Is Nothing Then
            ZEFLicenseKey = New Guid(DF_ZEFLicenseKey)
        End If
    End Sub
    Public Sub SetDefaultValuesAS()
        If String.IsNullOrEmpty(_ChangeMessageBox) Then
            ChangeMessageBox = DF_ChangeMessageBox
        End If
        If DuraccionCacheLarga = 0 Then
            DuraccionCacheLarga = DF_DuraccionCacheLarga
        End If
        If DuraccionCacheMedia = 0 Then
            DuraccionCacheMedia = DF_DuraccionCacheMedia
        End If
        If DuraccionCacheCorta = 0 Then
            DuraccionCacheCorta = DF_DuraccionCacheCorta
        End If
        If String.IsNullOrEmpty(_NombreCache) Then
            NombreCache = DF_NombreCache
        End If
    End Sub
    Public Sub SetGeneralConfiguration(gc As GeneralConfiguration)
        If gc IsNot Nothing AndAlso gc.Id > 0 Then
            HISAssemblyFileName = gc.HISAssemblyFileName
            Tenant = gc.Tenant
            SignUpSignIn = gc.SignUpSignIn
            PasswordReset = gc.PasswordReset
            ClientId = gc.ClientId
            RedirectUri = gc.RedirectUri
            SessionLogoutUrl = gc.SessionLogoutUrl
            AppFunctionURL = gc.AppFunctionURL
            GetUserByEmailKey = gc.FunctionKey1
            GetUserConfigurationByUserIdKey = gc.FunctionKey2
            ZEFLicenseName = gc.ZEFLicenseName
            ZEFLicenseKey = gc.ZEFLicenseKey
        Else
            SetDefaultValuesGC()
        End If
    End Sub
    Public Sub SetApplicationSetting(appSet As ApplicationSettings)
        If appSet IsNot Nothing AndAlso appSet.Id > 0 Then
            UrlServiceDispensing = appSet.UrlServiceDispensing
            ChangeMessageBox = appSet.ChangeMessageBox
            NewServices = appSet.NewServices
            CachedEnable = appSet.CachedEnable
            BaseDatosMongo = appSet.BaseDatosMongo
            ServidorMongo = appSet.ServidorMongo
            DuraccionCacheLarga = appSet.DuraccionCacheLarga
            DuraccionCacheMedia = appSet.DuraccionCacheMedia
            DuraccionCacheCorta = appSet.DuraccionCacheCorta
            CachePort = appSet.CachePort
            CacheServerName = appSet.CacheServerName
            ContainerId = appSet.ContainerId
            NombreCache = appSet.NombreCache
        Else
            SetDefaultValuesAS()
        End If
    End Sub
    Public Sub SetUserConfiguration(uc As UserConfiguration)
        If uc IsNot Nothing Then
            ShowThemeSkinSelector = uc.ShowThemeSkinSelector
            ReporteadorActivo = uc.ReporteadorActivo
        End If
    End Sub
#End Region
End Class
