Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Partial Public Class CashReceiptAdvancePayment
    Inherits Entity(Of Domain.Entities.CashReceiptAdvancePayment)

    <DataMember>
    Property Code As String

    <DataMember>
    Property Value As Decimal

    <DataMember>
    Property Balance As Decimal

End Class
