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

''' <summary>
''' interface con los metodos necesarios para la administracion de la cache
''' </summary>
Public Interface ICache

#Region "Add Cache"

    ''' <summary>
    ''' Adicionar un valor a la cache
    ''' </summary>
    ''' <typeparam name="T">el tipo de dato.</typeparam>
    ''' <param name="keyCache">el nombre de la cache.</param>
    ''' <param name="data">el dato a guardar.</param>
    Sub AddCache(Of T)(ByVal keyCache As String, ByVal data As T)

    ''' <summary>
    ''' Adicionar un valor a la cache en una region especifica
    ''' </summary>
    ''' <typeparam name="T">el tipo de dato.</typeparam>
    ''' <param name="keyCache">el nombre de la cache.</param>
    ''' <param name="data">el dato a guardar.</param>
    ''' <param name="region">El Nombre de la Region</param>
    Sub AddCacheRegion(Of T)(ByVal keyCache As String, ByVal region As String, ByVal data As T)


#End Region

#Region "Overwrite Cache"

    ''' <summary>
    ''' Sobrescribe un valor en la cache
    ''' </summary>
    ''' <typeparam name="T">el tipo de dato</typeparam>
    ''' <param name="keyCache">el nombre de la cache.</param>
    ''' <param name="data">el dato a escribir.</param>
    Sub OverwriteCache(Of T)(ByVal keyCache As String, ByVal data As T)


    ''' <summary>
    ''' Sobrescribe un valor en la cache de una region especifica
    ''' </summary>
    ''' <typeparam name="T">el tipo de dato</typeparam>
    ''' <param name="keyCache">el nombre de la cache.</param>
    ''' <param name="data">el dato a escribir.</param>
    ''' <param name="region">el nombre de la region</param>
    ''' <param name="ttl">TTL de la cache en el repositorio</param>
    Sub OverwriteCacheRegion(Of T)(ByVal keyCache As String, ByVal region As String, ByVal data As T, ByVal ttl As TimeSpan)

#End Region

#Region "Get Cache"

    ''' <summary>
    ''' Obtener un valor de la cache
    ''' </summary>
    ''' <typeparam name="T">el tipo de dato.</typeparam>
    ''' <param name="keyCache">el nombre de la cache.</param>
    ''' <returns></returns>
    Function GetCache(Of T)(ByVal keyCache As String) As T

    ''' <summary>
    ''' Obtener un valor de la cache
    ''' </summary>
    ''' <typeparam name="T">el tipo de dato.</typeparam>
    ''' <param name="keyCache">el nombre de la cache.</param>
    ''' <param name="region">El Nombre de la Region</param>
    ''' <returns></returns>
    Function GetCacheRegion(Of T)(ByVal keyCache As String, ByVal region As String) As T


#End Region

#Region "Delete Cache"

    ''' <summary>
    ''' Elimina una objeto de la cache.
    ''' </summary>
    ''' <param name="keyCache">el Nombre del Objeto.</param>
    Sub DeleteCache(ByVal keyCache As String)

    ''' <summary>
    ''' Elimina una objeto de la cache.
    ''' </summary>
    ''' <param name="keyCache">el Nombre del Objeto.</param>
    ''' <param name="region">el Nombre de la Region</param>
    Sub DeleteCacheRegion(ByVal keyCache As String, ByVal region As String)

#End Region

#Region "Purge Cache"

    ''' <summary>
    ''' Purgar la cache de todas las regiones
    ''' </summary>
    Sub PurgeCache()

    ''' <summary>
    ''' Purgar la Cache en una region especifica
    ''' </summary>
    ''' <param name="region">El Nombre de la Region</param>
    Sub PurgeCacheRegion(ByVal region As String)

#End Region

End Interface