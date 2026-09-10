Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Public Class PaymentMethods
    Inherits Entity(Of Domain.Entities.PaymentMethods)

    <DataMember>
    Property CodeNameBank As String

    <DataMember>
    Property CodeNameBankAccount As String

    <DataMember>
    Property CodeNameCard As String

    <DataMember>
    Property CodeNameCostCenter As String

    <DataMember>
    Property CurrencyName As String
End Class
