Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Partial Public Class JournalVouchers
    Inherits Entity(Of Domain.Entities.JournalVouchers)

    <DataMember>
    Property CodeNameJournalVoucherType As String

    <DataMember>
    Property OriginEntityName As String

    <DataMember>
    Property BookCurrencyId As Integer?

    <DataMember>
    Property DateTRM As Date

End Class
