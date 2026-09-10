Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Partial Public Class LegalBook
    Inherits Entity(Of Domain.Entities.LegalBook)

    <DataMember>
    Property hasJournalVouchers As Boolean = False

End Class
