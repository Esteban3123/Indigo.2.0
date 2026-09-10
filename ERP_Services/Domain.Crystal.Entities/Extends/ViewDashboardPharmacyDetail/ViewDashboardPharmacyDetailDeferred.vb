Imports Domain.Entities
Imports System.Runtime.Serialization
Imports System.Text
Imports Infrastructure.CrossCutting.Base

Partial Public Class ViewDashboardPharmacyDetailDeferred

    <DataMember>
    Public Property Id As Integer

    <DataMember>
    Public Property FirstDeliveryDate As DateTime

    <DataMember>
    Public Property DeliveryQuantityDeferred As Integer

    <DataMember>
    Public Property Periodicity As Integer

    <DataMember>
    Public Property ProductCode As String

    <DataMember>
    Public Property Number As Integer

    <DataMember>
    Public Property DeliveryDate As DateTime

    <DataMember>
    Public Property DeliveryQuantity As Integer

    <DataMember>
    Public Property PendingQuantity As Integer

End Class
