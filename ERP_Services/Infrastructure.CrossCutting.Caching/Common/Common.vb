'***********************************************************************
' Assembly         : Infrastructure.CrossCutting.Caching
' Author           : WalterSierra
' Created          : 10-05-2011
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-02-27
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Configuration
Imports Microsoft.ApplicationServer.Caching
#End Region

''' <summary>
''' clase que contiene todo el codigo comun para el manejo de la cache
''' </summary>
Public Class Common
    Implements ICache

#Region "Fields"

    ''' <summary>
    ''' variable para el manejo global de la cache
    ''' </summary>
    Private _defaultCache As DataCache

#End Region

#Region "Builders"

    ''' <summary>
    ''' inicializa un nueva instancia de la clase <see cref="Common" /> .
    ''' </summary>
    ''' <param name="defaultCache">la variable que tiene la canexion al reopsitorio de cache.</param>
    Sub New(ByVal defaultCache As DataCache)
        _defaultCache = defaultCache
    End Sub

#End Region

#Region "Add Cache"

    ''' <summary>
    ''' Adicionar un valor a la cache
    ''' </summary>
    ''' <typeparam name="T">el tipo de dato.</typeparam>
    ''' <param name="keyCache">el nombre de la cache.</param>
    ''' <param name="data">el dato a guardar.</param>
    Public Sub AddCache(Of T)(ByVal keyCache As String, ByVal data As T) Implements ICache.AddCache
        If String.IsNullOrEmpty(keyCache) = True Then
            Throw New ArgumentNullException("keyCache vacio")
        End If
        If data Is Nothing Then
            Throw New ArgumentNullException("data vacio")
        End If
        If _defaultCache.GetCacheItem(keyCache) Is Nothing Then
            _defaultCache.Add(keyCache, data)
        Else
            Throw New InvalidOperationException("keyCache ya Existe!")
        End If
    End Sub

    ''' <summary>
    ''' Adicionar un valor a la cache en una region especifica
    ''' </summary>
    ''' <typeparam name="T">el tipo de dato.</typeparam>
    ''' <param name="keyCache">el nombre de la cache.</param>
    ''' <param name="data">el dato a guardar.</param>
    ''' <param name="region">El Nombre de la Region</param>
    Public Sub AddCacheRegion(Of T)(ByVal keyCache As String, ByVal region As String, ByVal data As T) Implements ICache.AddCacheRegion
        If String.IsNullOrEmpty(keyCache) = True Then
            Throw New ArgumentNullException("keyCache vacio")
        End If
        If String.IsNullOrEmpty(region) = True Then
            Throw New ArgumentNullException("region vacio")
        End If
        If data Is Nothing Then
            Throw New ArgumentNullException("data vacio")
        End If
        CreateRegion(region)
        If _defaultCache.GetCacheItem(keyCache, region) Is Nothing Then
            _defaultCache.Add(keyCache, data, region)
        Else
            Throw New InvalidOperationException("keyCache ya Existe!")
        End If
    End Sub

#End Region

#Region "Overwrite Cache"

    ''' <summary>
    ''' Sobrescribe un valor en la cache
    ''' </summary>
    ''' <typeparam name="T">el tipo de dato</typeparam>
    ''' <param name="keyCache">el nombre de la cache.</param>
    ''' <param name="data">el dato a escribir.</param>
    Public Sub OverwriteCache(Of T)(ByVal keyCache As String, ByVal data As T) Implements ICache.OverwriteCache
        If String.IsNullOrEmpty(keyCache) = True Then
            Throw New ArgumentNullException("keyCache vacio")
        End If
        If data Is Nothing Then
            Throw New ArgumentNullException("data vacio")
        End If
        Try
            _defaultCache.Put(keyCache, data)
        Catch
            LogEvent.LogEvent()
        End Try
    End Sub

    ''' <summary>
    ''' Sobrescribe un valor en la cache de una region especifica
    ''' </summary>
    ''' <typeparam name="T">el tipo de dato</typeparam>
    ''' <param name="keyCache">el nombre de la cache.</param>
    ''' <param name="data">el dato a escribir.</param>
    ''' <param name="region">el nombre de la region</param>
    Public Sub OverwriteCacheRegion(Of T)(ByVal keyCache As String, ByVal region As String, ByVal data As T, ByVal ttl As TimeSpan) Implements ICache.OverwriteCacheRegion
        If String.IsNullOrEmpty(keyCache) = True Then
            Throw New ArgumentNullException("keyCache vacio")
        End If
        If String.IsNullOrEmpty(region) = True Then
            Throw New ArgumentNullException("region vacio")
        End If
        If data Is Nothing Then
            Throw New ArgumentNullException("data vacio")
        End If
        If ttl = Nothing Then
            Throw New ArgumentNullException("ttl vacio")
        End If

        Try
            CreateRegion(region)
            _defaultCache.Put(keyCache, data, ttl, region)
        Catch
            LogEvent.LogEvent()
        End Try
    End Sub

#End Region

#Region "Get Cache"

    ''' <summary>
    ''' Obtener un valor de la cache
    ''' </summary>
    ''' <typeparam name="T">el tipo de dato.</typeparam>
    ''' <param name="keyCache">el nombre de la cache.</param>
    ''' <returns></returns>
    Function GetCache(Of T)(ByVal keyCache As String) As T Implements ICache.GetCache
        If String.IsNullOrEmpty(keyCache) = True Then
            Throw New ArgumentNullException("keyCache vacio")
        End If
        Try
            Dim objT As T = CType(_defaultCache.Get(keyCache), T)
            If objT Is Nothing Then
                Return Nothing
            Else
                Return objT
            End If

        Catch
            Return Nothing
        End Try
    End Function


    ''' <summary>
    ''' Obtener un valor de la cache
    ''' </summary>
    ''' <typeparam name="T">el tipo de dato.</typeparam>
    ''' <param name="keyCache">el nombre de la cache.</param>
    ''' <param name="region">El Nombre de la Region</param>
    ''' <returns></returns>
    Function GetCacheRegion(Of T)(ByVal keyCache As String, ByVal region As String) As T Implements ICache.GetCacheRegion
        If String.IsNullOrEmpty(keyCache) = True Then
            Throw New ArgumentNullException("keyCache vacio")
        End If
        If String.IsNullOrEmpty(region) = True Then
            Throw New ArgumentNullException("region vacio")
        End If
        Try

            CreateRegion(region)
            Dim objT As T = CType(_defaultCache.Get(keyCache, region), T)
            If objT Is Nothing Then
                Return Nothing
            Else
                Return objT
            End If
        Catch
            Return Nothing
        End Try
    End Function

#End Region

#Region "Delete Cache"

    ''' <summary>
    ''' Elimina una objeto de la cache.
    ''' </summary>
    ''' <param name="keyCache">el Nombre del Objeto.</param>
    Sub DeleteCache(ByVal keyCache As String) Implements ICache.DeleteCache
        If String.IsNullOrEmpty(KeyCache) = True Then
            Throw New ArgumentNullException("keyCache vacio")
        End If
        _defaultCache.Remove(KeyCache)
    End Sub

    ''' <summary>
    ''' Elimina una objeto de la cache.
    ''' </summary>
    ''' <param name="keyCache">el Nombre del Objeto.</param>
    ''' <param name="region">el Nombre de la Region</param>
    Sub DeleteCacheRegion(ByVal keyCache As String, ByVal region As String) Implements ICache.DeleteCacheRegion
        If String.IsNullOrEmpty(keyCache) = True Then
            Throw New ArgumentNullException("keyCache vacio")
        End If
        If String.IsNullOrEmpty(region) = True Then
            Throw New ArgumentNullException("region vacio")
        End If
        _defaultCache.Remove(keyCache, region)
    End Sub

#End Region

#Region "Purge Cache"

    ''' <summary>
    ''' Purgar la cahe de todas las regiones
    ''' </summary>
    Public Sub PurgeCache() Implements ICache.PurgeCache
        For Each region In _defaultCache.GetSystemRegions
            _defaultCache.ClearRegion(region)
        Next
    End Sub

    ''' <summary>
    ''' Purgar la Cache en una region especifica
    ''' </summary>
    ''' <param name="region">El Nombre de la Region</param>
    Public Sub PurgeCacheRegion(ByVal region As String) Implements ICache.PurgeCacheRegion
        If String.IsNullOrEmpty(region) = True Then
            Throw New ArgumentNullException("region vacio")
        End If
        CreateRegion(region)
        _defaultCache.ClearRegion(region)
    End Sub

#End Region

#Region "Validations"

    ''' <summary>
    ''' Crear una region en la cache si no existe
    ''' </summary>
    ''' <param name="nameRegion">el nombre de la region.</param>
    Private Sub CreateRegion(ByVal nameRegion As String)
        Try
            If _defaultCache.GetSystemRegions.Where(Function(e) e = nameRegion).Count = 0 Then
                _defaultCache.CreateRegion(nameRegion)
            End If
        Catch
            LogEvent.LogEvent()
        End Try
    End Sub

#End Region

End Class