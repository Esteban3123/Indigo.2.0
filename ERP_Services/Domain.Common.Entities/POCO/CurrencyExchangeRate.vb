
Imports System.Runtime.Serialization

<DataContract()>
Public Class CurrencyExchangeRate

    <DataMember()>
    Property FromCurrencyId As Integer

    <DataMember()>
    Property ToCurrencyId As Integer

    <DataMember()>
    Property TRMValue As Decimal

    <DataMember()>
    Property Value As Decimal
End Class
