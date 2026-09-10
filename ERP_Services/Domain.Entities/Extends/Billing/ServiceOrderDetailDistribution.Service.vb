Imports System.Runtime.Serialization

Partial Public Class ServiceOrderDetailDistribution

    <DataMember()>
    Public Property ItemCode As String

    <DataMember()>
    Public Property ItemDescription As String

    <DataMember()>
    Public Property ItemQuantity As Integer

    <DataMember()>
    Public Property ItemTotalSalesPrice As Integer

    <DataMember()>
    Public Property QuotationServiceOrderDetailId As Integer

    <DataMember()>
    Public Property ApportionmentPercent As Decimal

    <DataMember()>
    Public Property CupsEntityId As Integer?

    <DataMember()>
    Public Property SubGroupCupsId As Integer?

    <DataMember()>
    Public Property GroupCupsId As Integer?

    <DataMember()>
    Public Property ProductId As Integer?

End Class
