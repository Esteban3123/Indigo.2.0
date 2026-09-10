Imports System.Runtime.Serialization

Partial Public Class RawMaterialDevolutionDetail

    <DataMember>
    Public Property ProductId As Integer
    <DataMember>
    Public Property ProductCodeName As String
    <DataMember>
    Public Property BatchSerialId As Integer?
    <DataMember>
    Public Property BatchSerialCode As String

    Private _pendingQuantity As Integer
    <DataMember>
    Public Property PendingQuantity As Integer
        Get
            _pendingQuantity = DeliveredQuantity - DevolutionQuantity
            Return _pendingQuantity
        End Get
        Set(value As Integer)
            _pendingQuantity = value
        End Set
    End Property
    <DataMember>
    Public Property DeliveredQuantity As Integer
    <DataMember>
    Public Property DevolutionQuantity As Integer

End Class
