#Region "Imports"

Imports System.Runtime.Serialization

#End Region

Partial Public Class FixedAssetLocation

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el código y nombre de la Ubicación concatenado
    ''' </summary>
    <DataMember()>
    Public Property CodeNameParentLocation As String

    ''' <summary>
    ''' Obtiene o establece el código y nombre de la Ubicación concatenado
    ''' </summary>
    <DataMember()>
    Public Property CodeNameLocationType As String

    ''' <summary>
    ''' Obtiene o establece el código y nombre de la Ubicación concatenado
    ''' </summary>
    <DataMember()>
    Public Property CodeNameCostCenter As String

    ''' <summary>
    ''' Obtiene o establece el código y nombre de la Ubicación concatenado
    ''' </summary>
    <DataMember()>
    Public Property CodeNameMainAccount As String

#End Region

End Class
