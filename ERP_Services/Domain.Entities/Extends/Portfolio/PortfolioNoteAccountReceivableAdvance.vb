Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Partial Public Class PortfolioNoteAccountReceivableAdvance
    Inherits Entity(Of Domain.Entities.PortfolioNoteAccountReceivableAdvance)

    <DataMember>
    Property InvoiceNumber As String

    <DataMember>
    Property NumberShare As Integer

    <DataMember>
    Property CodeNameMainAccount As String

    '<DataMember>
    'Property Balance As Decimal

    <DataMember>
    Property Value As Decimal

    <DataMember>
    Property CodeAdvance As String

    <DataMember>
    Property Nature As Integer

    <DataMember>
    Property PortfolioStatusName As String

    <DataMember>
    Property FlagTotalNote As Boolean

End Class
