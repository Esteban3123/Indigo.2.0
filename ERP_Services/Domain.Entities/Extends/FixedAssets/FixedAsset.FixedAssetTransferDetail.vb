#Region "Imports"

Imports System.Runtime.Serialization

#End Region

Partial Public Class FixedAssetTransferDetail

    ''' <summary>
    ''' Código y nombre del articulo
    ''' </summary>
    <DataMember()>
    Public Property ItemCodeName As String

    ''' <summary>
    ''' Serie del articulo
    ''' </summary>
    <DataMember()>
    Public Property Serie As String

    ''' <summary>
    ''' Placa del articulo
    ''' </summary>
    <DataMember()>
    Public Property Plate As String

End Class
