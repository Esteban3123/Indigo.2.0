Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Partial Public Class ConstitutionCashSmaller
    Inherits Entity(Of Domain.Entities.ConstitutionCashSmaller)

    <DataMember>
    Property CashRegisterSmallerDescription As String

    <DataMember>
    Property CashRegisterDescription As String

    <DataMember>
    Property EntityBankAccountDescription As String

End Class