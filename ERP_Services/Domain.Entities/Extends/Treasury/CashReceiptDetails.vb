Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Partial Public Class CashReceiptDetails
    Inherits Entity(Of Domain.Entities.CashReceiptDetails)

    <DataMember>
    Property CodeNameCashReceiptConcept As String

    <DataMember>
    Property CodeNameThirdParty As String

    <DataMember>
    Property CodeNameMainAccount As String

    <DataMember>
    Property CodeNameCostCenter As String

    <DataMember>
    Property CodeNameRetentionConcept As String

    <DataMember>
    Property CodeNameCashFlowConcept As String

    <DataMember>
    Property CurrencyAbbreviation As String

    '<DataMember>
    'Property ValuePaymentAdvance As Decimal

    '<DataMember>
    'Property DetailPaymentAdvance As String
End Class
