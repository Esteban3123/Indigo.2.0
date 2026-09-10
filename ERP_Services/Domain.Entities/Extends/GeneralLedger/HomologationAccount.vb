Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Partial Public Class HomologationAccount
    Inherits Entity(Of Domain.Entities.HomologationAccount)

    <DataMember>
    Property NumberNameOfficialMainAccount As String

    <DataMember>
    Property HandlesThirdPartyOfficialMainAccount As Boolean

    <DataMember>
    Property HandlesCostCenterOfficialMainAccount As Boolean

End Class
