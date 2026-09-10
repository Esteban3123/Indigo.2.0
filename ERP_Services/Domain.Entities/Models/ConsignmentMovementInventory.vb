Imports System.Runtime.Serialization

Public Class ConsignmentMovementInventory
    <DataMember>
    Property ProductId As Integer
    <DataMember>
    Property WarehouseId As Integer
    <DataMember>
    Property MaxLimitQuantity As Integer
    <DataMember>
    Property IncrementQuantity As Integer
    <DataMember>
    Property KardexQuantity As Integer
End Class
