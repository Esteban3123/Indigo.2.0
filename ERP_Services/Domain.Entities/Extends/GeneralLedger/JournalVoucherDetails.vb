Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Partial Public Class JournalVoucherDetails
    Inherits Entity(Of Domain.Entities.JournalVoucherDetails)

    <DataMember>
    Property CodeNameMainAccount As String

    <DataMember>
    Property CodeNameThirdParty As String

    <DataMember>
    Property CodeNameCostCenter As String

    <DataMember>
    Property CodeNameRetention As String

    <DataMember>
    Property Nature As Integer

End Class
