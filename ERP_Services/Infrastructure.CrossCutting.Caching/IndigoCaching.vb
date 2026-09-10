'***********************************************************************
' Assembly         : Infrastructure.CrossCuting.Caching
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
Imports Infrastructure.CrossCutting.Base
#End Region

''' <summary>
''' Clase con los Metodos Necesarios para consumir la cache de appfabric
''' </summary>
Public NotInheritable Class IndigoCaching

#Region "Static Methods"

    ''' <summary>
    ''' Obtiene un valor de la cache
    ''' </summary>
    ''' <typeparam name="T">el tipo de dato esperado.</typeparam>
    ''' <param name="keyCache">el key de la cache.</param>
    ''' <param name="mo">el modulo - define la region.</param>
    ''' <returns></returns>
    Public Shared Function GetCache(Of T)(ByVal mo As EModule, ByVal keyCache As String) As T
        If String.IsNullOrEmpty(keyCache) = True Then
            Throw New ArgumentNullException("keyCache Vacio")
        End If
        Return CacheFactory.Instance.CurrentCache.GetCacheRegion(Of T)(keyCache, ResolveRegion(mo))
    End Function

    ''' <summary>
    ''' Establece un valor en la cache
    ''' </summary>
    ''' <typeparam name="T">el tipo de dato a grabar.</typeparam>
    ''' <param name="keyCache">el key de la cache.</param>
    ''' <param name="mo">el modulo.</param>
    ''' <param name="length">Enumeracion qye determina la duracion de la cache</param>
    ''' <param name="cacheValue">el valor a cachear.</param>
    Public Shared Sub SetCache(Of T)(ByVal mo As EModule, ByVal length As Length, ByVal keyCache As String, ByVal cacheValue As T)
        If String.IsNullOrEmpty(keyCache) = True Then
            Throw New ArgumentNullException("keyCache Vacio")
        End If
        If cacheValue Is Nothing Then
            Throw New ArgumentNullException("cacheValues Vacio")
        End If
        CacheFactory.Instance.CurrentCache.OverwriteCacheRegion(keyCache, ResolveRegion(mo), cacheValue, ResolveTtl(length))
    End Sub

    ''' <summary>
    ''' Eliminar un elemento de la cache
    ''' </summary>
    ''' <param name="mo">el modulo.</param>
    ''' <param name="keyCache">el key de la cache.</param>
    Public Shared Sub DeleteCache(ByVal mo As EModule, ByVal keyCache As String)
        If String.IsNullOrEmpty(keyCache) = True Then
            Throw New ArgumentNullException("keyCache Vacio")
        End If
        CacheFactory.Instance.CurrentCache.DeleteCacheRegion(keyCache, ResolveRegion(mo))
    End Sub

    ''' <summary>
    ''' Eliminar toda la cache de un modulo
    ''' </summary>
    ''' <param name="mo">El Modulo</param>
    Public Shared Sub DeleteAllCache(ByVal mo As EModule)
        CacheFactory.Instance.CurrentCache.PurgeCacheRegion(ResolveRegion(mo))
    End Sub

    ''' <summary>
    ''' Eliminar toda la cache
    ''' </summary>
    Public Shared Sub DeleteAllCache()
        CacheFactory.Instance.CurrentCache.PurgeCache()
    End Sub

#End Region

#Region "Utilities"

    ''' <summary>
    ''' Obtiene el nombre de la region basado en la enumeracion del modulo invocado
    ''' </summary>
    ''' <param name="mo">el modulo.</param>
    ''' <returns></returns>
    Private Shared Function ResolveRegion(ByVal mo As EModule) As String
        Try
            Return [Enum].GetName(GetType(EModule), mo)
        Catch
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' obtiene el valor para el TTL del objeto insertado en la cache
    ''' </summary>
    ''' <param name="length">la duracion.</param>
    ''' <returns></returns>
    Private Shared Function ResolveTtl(ByVal length As Length) As TimeSpan
        Try
            Dim minutes As Integer
            Select Case length
                Case Caching.Length.ShortLength
                    'minutes = CInt(ConfigurationManager.AppSettings("DuraccionCacheCorta"))
                    minutes = ApplicationSetting.Instance.DuraccionCacheCorta
                    'valido configuracion
                    If minutes = 0 Then
                        Throw New ArgumentNullException("DuraccionCacheCorta Vacio")
                    End If
                    'establezco valor
                    Return New TimeSpan(0, minutes, 0)
                Case Caching.Length.MediumLength
                    'minutes = CInt(ConfigurationManager.AppSettings("DuraccionCacheMedia"))
                    minutes = ApplicationSetting.Instance.DuraccionCacheMedia
                    'valido configuracion
                    If minutes = 0 Then
                        Throw New ArgumentNullException("DuraccionCacheMedia Vacio")
                    End If
                    'establezco valor
                    Return New TimeSpan(0, minutes, 0)
                Case Caching.Length.LongLength
                    'minutes = CInt(ConfigurationManager.AppSettings("DuraccionCacheLarga"))
                    minutes = ApplicationSetting.Instance.DuraccionCacheLarga
                    'valido configuracion
                    If minutes = 0 Then
                        Throw New ArgumentNullException("DuraccionCacheLarga Vacio")
                    End If
                    'establezco valor
                    Return New TimeSpan(0, minutes, 0)
            End Select
        Catch
            Return Nothing
        End Try
    End Function

#End Region

End Class