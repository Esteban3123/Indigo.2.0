Imports System.Runtime.Serialization

Partial Public Class DetalleObject

#Region "Properties"

    <DataMember()>
    Public Property recetarioOMedica As Integer

    <DataMember()>
    Public Property idProducto As Integer

    <DataMember()>
    Public Property codigoProducto As String

    <DataMember()>
    Public Property nombreProducto As String

    <DataMember()>
    Public Property altoCosto As Boolean

    <DataMember()>
    Public Property pos As Boolean

    <DataMember()>
    Public Property cantidad As Integer

    <DataMember()>
    Public Property idUnidadMedida As Integer

    <DataMember()>
    Public Property posologia As String

#End Region

End Class
