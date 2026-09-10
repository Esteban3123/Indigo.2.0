Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Partial Public Class AccountReceivable
    Inherits Entity(Of Domain.Entities.AccountReceivable)

    <DataMember>
    Property PaymentValue As Decimal

    <DataMember>
    Property Age As Integer
End Class
