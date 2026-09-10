Imports System.Runtime.Serialization

Partial Public Class EntranceVoucherDetail

    <DataMember()>
    Public Property GroupCodeName As String

    <DataMember()>
    Public Property ProductCode As String

    <DataMember()>
    Public Property ProductName As String

    <DataMember()>
    Public Property ProductCodeName As String

    <DataMember()>
    Public Property ManufacturerName As String

    <DataMember()>
    Public Property HealthRegistration As String

    <DataMember()>
    Public Property Presentation As String

    <DataMember()>
    Public Property DescriptionBatch As String

    <DataMember()>
    Public Property HandlesBatch As Boolean

    <DataMember>
    Public Property DeliveredDate As DateTime?

    <DataMember()>
    Public Property MinBase As Decimal

    <DataMember()>
    Public Property Rate As Decimal

    <DataMember()>
    Public Property TypeRounding As Byte

    <DataMember()>
    Public Property CostWithDescount As Decimal


End Class
