Imports System.Runtime.Serialization

<DataContract>
Public Class ManageRawMaterialModel
    <DataMember()>
    Public Property Id As Integer?
    <DataMember()>
    Public Property RequestMixingStationDetailId As Integer?
    <DataMember()>
    Public Property ProductId As Integer?
    <DataMember()>
    Public Property AtcId As Integer?
    <DataMember()>
    Public Property SuppliedId As Integer?
    <DataMember()>
    Public Property ProductFullName As String
    <DataMember()>
    Public Property RequiredQuantity As Decimal?
    <DataMember()>
    Public Property UsedQuantity As Decimal?
    <DataMember()>
    Public Property PendingQuantity As Decimal?
    <DataMember()>
    Public Property CampaignQuantity As Decimal?
    <DataMember()>
    Public Property CampaignBalanceQuantity As Decimal?
    <DataMember()>
    Public Property MeasureUnitId As Integer?
    <DataMember()>
    Public Property MeasureUnitAbreviation As String
    <DataMember()>
    Public Property Observations As String
    <DataMember()>
    Public Property CampaignDetailValidation As List(Of ProductTotalDelivered)
    <DataMember()>
    Public Property TotalProducts As Integer
    <DataMember()>
    Public Property TotalProductsProcess As Integer
    <DataMember()>
    Public Property totalQuantityRequired As Decimal?

End Class
