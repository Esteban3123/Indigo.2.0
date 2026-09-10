Imports System.Runtime.Serialization

Public Class DeliveriesByPharmacyProductTypeModel

    <DataMember>
    Public Property HCFARMEPDID As Integer

    <DataMember>
    Public Property ProductType As Boolean

    <DataMember>
    Public Property ProductId As Integer?

    <DataMember>
    Public Property Quantity As Integer

    <DataMember>
    Public Property ConcentrationByUnit As String

    <DataMember>
    Public Property TotalWeight As String

    <DataMember>
    Public Property QuantityDeliveryPT As Integer
End Class

