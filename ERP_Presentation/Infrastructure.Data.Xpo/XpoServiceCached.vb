'***********************************************************************
' Assembly         : Infrastructure.Data.Xpo
' Author           : Diego A. Roldan Lozano
' Created          : 2018-04-17
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System
Imports Infrastructure.CrossCutting.Base
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Xpo.Base
Imports System.ServiceModel
Imports System.Configuration
Imports DevExpress.Xpo.DB.Helpers
Imports DevExpress.Xpo.DB

#End Region

''' <summary>
''' Provee servicios de acceso a datos usando un almacén de datos
''' </summary>
Public Class XpoServiceCached
    Inherits XpoServiceEx
    Implements IDisposable

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(company As String)
        MyBase.New()
        XPODataLayerManager.Instance(company).Refresh()
        XPODataLayerManager.Company = company



        'Dim cached As String = ConfigurationManager.AppSettings.Get("CachedEnable")
        '_isCached = Not String.IsNullOrEmpty(cached) AndAlso cached.ToLower().Equals("true")
        'ReadConfiguration()
        'RefreshDataLayer(company)
    End Sub

#End Region

    '#Region "Singleton"
    '    Private Shared _isCached As Boolean = False
    '    Private Shared _cacheRoot As DataCacheRoot
    '    ''' <summary>
    '    ''' Referesca la configuración de la capa de datos
    '    ''' </summary>
    '    ''' <param name="company">Empresa a consultar</param>
    '    Private Shadows Sub RefreshDataLayer(ByVal company As String)
    '        Dim dataStore = New WCFServiceDataStoreCache(endpointConfiguration, remoteAddress, company)
    '        If _isCached Then
    '            _cacheRoot = New DataCacheRoot(dataStore)
    '            _cacheRoot.Configure(New DataCacheConfiguration(DataCacheConfigurationCaching.All))
    '            DataLayer = New SimpleDataLayer(_cacheRoot)
    '        Else
    '            DataLayer = New SimpleDataLayer(dataStore)
    '        End If
    '        'XpoDefault.DataLayer = DataLayer
    '    End Sub

    '    ''' <summary>
    '    ''' metodo necesario para leer la configuracion xml de la aplicacion
    '    ''' </summary>
    '    Private Shared Shadows Sub ReadConfiguration()
    '        uriServiceEntitiesXpo = ConfigurationFile.Instance.UrlXpoWebServer
    '        protocolServicesXpo = ConfigurationFile.Instance.ProtocolUrlXpoWebServer
    '        remoteAddress = GetRemoteAddress()
    '        endpointConfiguration = GetEndPoint()
    '    End Sub

    '    Private Shared Shadows Function GetEndPoint() As String
    '        Return $"{[Enum].GetName(GetType(Protocol), protocolServicesXpo)}_Endpoint_XPOGateCache"
    '    End Function

    '    ''' <summary>
    '    ''' funcion para contatenar el remoteaddress por cada protocolo
    '    ''' </summary>
    '    ''' <returns>El Nombre del remoteaddress del Endpoint Correspondiente</returns>
    '    Private Shared Shadows Function GetRemoteAddress() As String
    '        Return $"{uriServiceEntitiesXpo}XpoGate.svc/{[Enum].GetName(GetType(Protocol), protocolServicesXpo)}XPO"
    '    End Function

    '#End Region

End Class
