Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Partial Public Class AccountReceivableShare
    Inherits Entity(Of Domain.Entities.AccountReceivableShare)

    <DataMember>
    Property InvoiceNumber As String

    <DataMember>
    Property InvoiceBalance As Decimal

    <DataMember>
    Property AccountReceivableAccountingIdTmp As Integer
End Class
