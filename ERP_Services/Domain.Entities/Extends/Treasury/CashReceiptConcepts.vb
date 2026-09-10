Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Partial Public Class CashReceiptConcepts
    Inherits Entity(Of Domain.Entities.CashReceiptConcepts)

    <DataMember>
    Property CodeNameMainAccount As String

End Class
