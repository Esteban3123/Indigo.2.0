Imports System.Runtime.Serialization

Partial Public Class RemissionEntranceDetailBatchSerial
    <DataMember>
    Property CodeBatchSerial As String

    <DataMember>
    Property Code As String

    <DataMember()>
    Public Property GroupCodeName As String

    <DataMember>
    Property CodeNameProduct As String

    <DataMember>
    Property ProductId As Integer

    <DataMember>
    Property QuantityDeliver As Integer

    <DataMember>
    Property DevolutionCauseId As Integer?

    Property DocumentDate As DateTime

    Property Activated As Boolean

    Property ItemInvalid As Boolean

    Property CurrencyId As Integer

    Property CurrencyAbbreviation As String

    Property DiscountPercentage As Decimal


End Class
