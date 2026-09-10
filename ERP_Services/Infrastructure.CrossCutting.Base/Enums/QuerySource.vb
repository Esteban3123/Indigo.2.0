''' <summary>
''' Enumeracion necesaria para conocer el origen a donde se van a relizar las consultas requeridas por la aplicacion
''' </summary>
Public Enum QuerySource As Integer
    ''' <summary>
    ''' Consulta el Servidor de Cache
    ''' </summary>
    CacheServer = 0
    ''' <summary>
    ''' Consulta El servidor de base de datos
    ''' </summary>
    DataBaseServer = 1
End Enum