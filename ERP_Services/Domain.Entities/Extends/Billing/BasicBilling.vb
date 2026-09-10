Imports System.Runtime.Serialization

Partial Public Class BasicBilling

#Region " Properties"

    <DataMember>
    Property BillingAuthorizationName As String

    <DataMember>
    Property CustomerName As String

    <DataMember>
    Property AddressName As String

    <DataMember>
    Property WarehouseName As String

    <DataMember>
    Property RetentionBaseIVA As Decimal

    <DataMember>
    Property ThirdPartyCustomerId As String

    <DataMember>
    Property CurrencyAbbreviation As String

    ''' <summary>
    ''' related entity to make balance crossing
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property PortfolioAdvanceInvoicePayment As PortfolioAdvanceInvoicePayment

#Region "Budget Interface"

    <DataMember()>
    Public Property BudgetaryEntityId As Integer?

    <DataMember()>
    Public Property BudgetaryEntityDescription As String

    <DataMember()>
    Public Property BudgetaryValidityId As Integer?

    <DataMember()>
    Public Property BudgetaryValidityDescription As String

    <DataMember()>
    Public Property BudgetDescription As String

    <DataMember()>
    Public Property CodeNameConditionSales As String

    <DataMember()>
    Public Property CodeNameEconomicActivity As String

#End Region

#End Region

End Class
