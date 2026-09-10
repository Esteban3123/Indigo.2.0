Imports System.Runtime.Serialization

Public Class ProductTotalDelivered
    <DataMember()>
    Public Property ProductId As Integer
    <DataMember()>
    Public Property AtcId As Integer?
    <DataMember()>
    Public Property AtcEntityId As Integer?
    <DataMember()>
    Public Property SuppliedId As Integer?
    <DataMember()>
    Public Property BatchSerialId As Integer?
    <DataMember()>
    Public Property ProductFullName As String
    <DataMember()>
    Public Property TotaledDelivered As Integer?
    <DataMember()>
    Public Property PendingQuantity As Integer?
    <DataMember()>
    Public Property DeliveredQuantity As Decimal?
    <DataMember()>
    Public Property CampaignQuantity As Decimal?
    <DataMember()>
    Public Property UsedQuantity As Decimal?
    <DataMember()>
    Public Property InternalControlQuantity As Decimal = 0
    <DataMember()>
    Public Property CampaignBalanceQuantity As Decimal?
    <DataMember()>
    Public Property FormulationType As Integer?
    <DataMember()>
    Public Property Weight As Decimal?
    <DataMember()>
    Public Property WeightMeasureUnit As Integer?
    <DataMember()>
    Public Property Volume As Decimal?
    <DataMember()>
    Public Property VolumeMeasureUnit As Integer?
    <DataMember()>
    Public Property MeasureUnitId As Integer?
    <DataMember()>
    Public Property MeasureUnitAbreviation As String
    <DataMember()>
    Public Property ProducType As Integer?
    <DataMember()>
    Public Property ProductCode As String
    <DataMember()>
    Public Property productQuantity As Integer = 0
    <DataMember()>
    Public Property totalProductQuantity As Integer = 0
    <DataMember()>
    Public Property TotalUsed As Decimal
    <DataMember()>
    Public Property TotalRequiredQuantity As Decimal
    <DataMember()>
    Public Property Conversion As Decimal

    Public Function GetQuantityPerUnit() As Decimal
        Return (TotalRequiredQuantity / totalProductQuantity)
    End Function

    Public Function DistributeQuantity() As Decimal
        Return (CampaignQuantity / totalProductQuantity)
    End Function

    Public Function SetAvailableQuantity() As Decimal
        Return (UsedQuantity - productQuantity)
    End Function

End Class
