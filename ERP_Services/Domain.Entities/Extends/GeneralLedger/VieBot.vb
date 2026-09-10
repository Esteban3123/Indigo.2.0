Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Partial Public Class VieBot
    Inherits Entity(Of Domain.Entities.VieBot)

    <DataMember>
    Property CodeNameLegalBook As String

    <DataMember>
    Property IsOfficialBook As Boolean

    <DataMember>
    Property OfficialCurrencyId As Integer

End Class
