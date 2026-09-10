''' <summary>
''' Clase para hacer almacenar la respuesta de la consulta del proceso de importacion del detalle de la regla
''' </summary>
Public Class DefinitionDetailQuery

    ''' <summary>
    ''' Diccionario que almacena los codigos de los diferente tipos de regla (CUPS,SERVICIO IPS etc..)
    ''' </summary>
    ''' <returns></returns>
    Public Property ListOfCodes As Dictionary(Of String, List(Of String))

    ''' <summary>
    ''' Diccionario que almacena los objetos con la respuesta de la busqueda de los codigos
    ''' </summary>
    ''' <returns></returns>
    Public Property ListOfObjects As Dictionary(Of String, Object)
End Class

