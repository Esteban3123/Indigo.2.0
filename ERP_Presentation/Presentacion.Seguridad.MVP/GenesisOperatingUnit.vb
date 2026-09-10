Public Class GenesisOperatingUnit

    ''' <summary>
    ''' Propiedad para el id de unidad operativa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property OperatingId As Integer

    ''' <summary>
    ''' Propiedad que obtiene el codigo de la unidad operativa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property OperatingCode As String

    ''' <summary>
    ''' Propiedad que obtiene el id del contenedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property OperatingContainerId As Integer

    ''' <summary>
    ''' Propiedad que obtiene el nombre de la unidad operativa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property OperatingName As String

    ''' <summary>
    ''' Propiedad para saber el padre de la unidad operativa|
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property OperatingParentId As Nullable(Of Integer)

    ''' <summary>
    ''' Propiedad que sirve para saber si la unidad operativa es la que esta por defecto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property OperatingDefault As Boolean
    ''' <summary>
    ''' Propiedad que sirve para saber si el usuario tiene permiso a esta
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property OperatingPermission As Boolean

End Class
