Imports System.Runtime.Serialization

Public Class ViewListFinalControlProductModel
    <DataMember()>
    Public Property Id As Integer
    <DataMember()>
    Public Property InventoryProductId As Integer
    <DataMember()>
    Public Property BatchSerialId As Integer
    <DataMember()>
    Public Property PhysicalInventoryWarehouseId As Integer
    <DataMember()>
    Public Property InventoryQuantity As Integer?
    <DataMember()>
    Public Property DeliveredQuantity As Integer
    <DataMember()>
    Public Property ProductFullName As String
    <DataMember()>
    Public Property CostProduct As Decimal?
    <DataMember()>
    Public Property ConsumptionUnit As String
    <DataMember()>
    Public Property PhysicalInventoryId As Integer
    <DataMember()>
    Public Property CampaignDetailId As Integer
    <DataMember()>
    Public Property RequestPackageDetailStatusIds As List(Of Integer)
    <DataMember()>
    Public Property MSClassUnitDoseType As Integer
End Class
