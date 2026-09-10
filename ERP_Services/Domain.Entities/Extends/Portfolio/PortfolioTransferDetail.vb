Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Partial Public Class PortfolioTransferDetail
    Inherits Entity(Of Domain.Entities.PortfolioTransferDetail)

    <DataMember>
    Property CodeNameMainAccount As String

    <DataMember>
    Property InvoiceNumber As String

    <DataMember>
    Property ValueBill As Decimal

    <DataMember>
    Property Balance As Decimal

    <DataMember>
    Property PortfolioStatusName As String

    <DataMember>
    Property CurrencyAbbreviation As String

    <DataMember>
    Property Patient As String
End Class
