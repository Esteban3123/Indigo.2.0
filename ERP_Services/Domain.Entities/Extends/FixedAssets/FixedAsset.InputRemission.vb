#Region "Imports"

Imports System.Runtime.Serialization

#End Region


Partial Public Class FixedAssetRemissionEntrance

    ''' <summary>
    ''' Obtiene o establece el código y nombre de la Ubicación concatenado
    ''' </summary>
    <DataMember()>
    Public Property NameSuplier As String

    <DataMember()>
    Public Property NameLocation As String

    <DataMember()>
    Public Property NameResponsible As String

End Class
