Imports System.Runtime.Serialization

Public Class Refunds

#Region "Properties"
    <DataMember()>
    Property FullNameCashRegister As String

    ''' <summary>
    ''' abrebviacion de la moneda segun ISO 4217
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Property CurrencyAbbreviation As String
#End Region

End Class
