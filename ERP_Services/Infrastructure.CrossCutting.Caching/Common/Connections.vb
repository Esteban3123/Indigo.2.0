'***********************************************************************
' Assembly         : Infrastructure.CrossCutting.Caching
' Author           : WalterSierra
' Created          : 10-05-2011
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-02-27
'
' Copyright        : (c) . All rights reserved.
'**********************************************************************

#Region "Imports"
Imports System.Configuration
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.ApplicationServer.Caching
#End Region

''' <summary>
''' clase con metodos estaticos para la conexion al repositorio de cache especifico 
''' </summary>
Public NotInheritable Class Connections

    ''' <summary>
    ''' funcion que configura la conexion a appfabric.
    ''' </summary>
    ''' <returns></returns>
    Friend Shared Function ConnectAppFabric() As DataCache
        Try
            'leo los parametros
            'Dim cacheServerName As String = ConfigurationManager.AppSettings("CacheServerName")
            Dim cacheServerName As String = ApplicationSetting.Instance.CacheServerName
            'leo el pc
            If String.IsNullOrEmpty(cacheServerName) = True Then
                Throw New ArgumentNullException("cacheServerName vacio")
            End If
            'Dim cachePort As Integer = CInt(ConfigurationManager.AppSettings("CachePort"))
            Dim cachePort As Integer = CInt(ApplicationSetting.Instance.CachePort)
            'leo el puerto
            If cachePort = 0 Then
                Throw New ArgumentNullException("cachePort vacio")
            End If
            'obtengo el nombre de la cache
            'Dim nameCache As String = ConfigurationManager.AppSettings("NombreCache")
            Dim nameCache As String = ApplicationSetting.Instance.NombreCache
            If String.IsNullOrEmpty(nameCache) = True Then
                Throw New ArgumentNullException("nameCache vacio")
            End If

            'inicio la configuracion
            Dim servers As List(Of DataCacheServerEndpoint) = New List(Of DataCacheServerEndpoint)(1)
            'adiciono el server
            servers.Add(New DataCacheServerEndpoint(cacheServerName, cachePort))
            'configuro
            Dim configuration As New DataCacheFactoryConfiguration()
            configuration.Servers = servers
            'seguridad
            configuration.SecurityProperties = New DataCacheSecurity(DataCacheSecurityMode.None, DataCacheProtectionLevel.None)
            'inicio la factoria
            Dim dataCacheFactory As New DataCacheFactory(configuration)
            'obtengo el objeto cache
            Return dataCacheFactory.GetCache(nameCache)
        Catch cach As DataCacheException
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' funcion que configura la conexion a appfabric. y habilita la cache local
    ''' </summary>
    ''' <returns></returns>
    Friend Shared Function ConnectLocalCache() As DataCache
        Try
            'leo los parametros
            'Dim cacheServerName As String = ConfigurationManager.AppSettings("CacheServerName")
            Dim cacheServerName As String = ApplicationSetting.Instance.CacheServerName
            'leo el pc
            If String.IsNullOrEmpty(cacheServerName) = True Then
                Throw New ArgumentNullException("cacheServerName vacio")
            End If
            'Dim cachePort As Integer = CInt(ConfigurationManager.AppSettings("CachePort"))
            Dim cachePort As Integer = CInt(ApplicationSetting.Instance.CachePort)
            'leo el puerto
            If cachePort = 0 Then
                Throw New ArgumentNullException("cachePort vacio")
            End If
            'obtengo el nombre de la cache
            'Dim nameCache As String = ConfigurationManager.AppSettings("NombreCache")
            Dim nameCache As String = ApplicationSetting.Instance.NombreCache
            If String.IsNullOrEmpty(nameCache) = True Then
                Throw New ArgumentNullException("nameCache vacio")
            End If

            'inicio la configuracion
            Dim servers As List(Of DataCacheServerEndpoint) = New List(Of DataCacheServerEndpoint)(1)
            'adiciono el server
            servers.Add(New DataCacheServerEndpoint(cacheServerName, cachePort))
            'configuro
            Dim configuration As New DataCacheFactoryConfiguration()
            configuration.Servers = servers
            'configuro la cache local
            configuration.LocalCacheProperties = New DataCacheLocalCacheProperties(10000, New TimeSpan(1, 0, 0), DataCacheLocalCacheInvalidationPolicy.TimeoutBased)
            'seguridad
            configuration.SecurityProperties = New DataCacheSecurity(DataCacheSecurityMode.None, DataCacheProtectionLevel.None)
            'inicio la factoria
            Dim dataCacheFactory As New DataCacheFactory(configuration)
            'obtengo el objeto cache
            Return dataCacheFactory.GetCache(nameCache)
        Catch cach As DataCacheException
            Return Nothing
        End Try
    End Function

End Class
