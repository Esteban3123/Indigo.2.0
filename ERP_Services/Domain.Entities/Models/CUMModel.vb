Imports System.Runtime.Serialization

Public Class CUMModel
    <DataMember>
    Public Property Id As Integer
    <DataMember>
    Public Property ProductId As Integer
    <DataMember>
    Public Property WarehouseId As Integer
    <DataMember>
    Public Property BatchSerialId As Integer
    <DataMember>
    Public Property Quantity As Integer
    <DataMember>
    Public Property ExpirationDate As DateTime?
    <DataMember>
    Public Property BatchCode As String
    <DataMember>
    Public Property CodeNameWarehouse As String
    <DataMember>
    Public Property CodeNameProduct As String
    <DataMember>
    Public Property Code As String
    <DataMember>
    Property ClassName As String
End Class

