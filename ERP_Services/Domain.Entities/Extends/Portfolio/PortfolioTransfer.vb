Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Partial Public Class PortfolioTransfer
    Inherits Entity(Of Domain.Entities.PortfolioTransfer)

    <DataMember>
    Property CodeNameCustomer As String

    <DataMember>
    Property CodeNameMainAccount As String

    <DataMember>
    Property CodeNameCostCenter As String

    <DataMember>
    Property CodeNameAdvance As String

    <DataMember>
    Property CustomerIdTemp As Integer

    <DataMember>
    Property CurrencyId As Integer?


    <DataMember>
    Property CurrencyAbbreviation As String
End Class
