#Region "Imports"

Imports System.Runtime.Serialization

#End Region

Partial Public Class FixedAssetRemissionEntranceItem

    ''' <summary>
    ''' Obtiene o establece el código y nombre de la Ubicación concatenado
    ''' </summary>
    <DataMember()>
    Public Property NameEquipment As String

    ''' <summary>
    ''' Obtiene o establece el código y nombre de la Ubicación concatenado
    ''' </summary>
    <DataMember()>
    Public Property NameIva As String

    ''' <summary>
    ''' Obtiene o establece el código y nombre de la Ubicación concatenado
    ''' </summary>
    <DataMember()>
    Public Property NameTrademark As String

    ''' <summary>
    ''' Obtiene o establece el código y nombre de la Ubicación concatenado
    ''' </summary>
    <DataMember()>
    Public Property NamePoliza As String

    Property Activated As Boolean
    ''' <summary>
    ''' bandera para saber que el item que voy a importar cumpla con las validaciones que se hicieron (se usa en remision de entrada)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ItemInvalid As Boolean

End Class
