Imports System.Runtime.Serialization

Partial Public Class CrossingAccountDetailCxC

    <DataMember()>
    Property BillNumber As String

    <DataMember()>
    Property MainAccountDescription As String

    <DataMember()>
    Property Value As Decimal

    <DataMember()>
    Property Balance As Decimal

    <DataMember()>
    Property ThirdPartyDescription As String

    <DataMember()>
    Property CodeNameCashFlowConcept As String

    <DataMember()>
    Property InvoiceCurrencyId As Integer

    <DataMember()>
    Property InvoiceCurrencyAbbreviation As String

End Class
