Imports System.Runtime.Serialization
Imports Domain.Common
Imports Domain.Common.Entities

Partial Public Class PortfolioAdvance

    <DataMember()>
    Property CashReceiptCode As String

    <DataMember()>
    Property FullNameMainAccount As String

    <DataMember()>
    Property CrossingValue As Decimal

    Property CashReceiptDetailIdTmp As Integer

    <DataMember()>
    Property CurrencyAbbreviation As String

    <DataMember()>
    Property PaymentMethodsType As Byte

    <DataMember()>
    Property PaymentMethodName As String

    ''' <summary>
    ''' Entity Related to return the value or balance in different curency
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Property BalanceInCurrencyConverted As CurrencyExchangeRate

    <DataMember()>
    Property PortfolioAdvanceType As Byte

    <DataMember()>
    Property PortfolioAdvanceTypeDescription As String
End Class
