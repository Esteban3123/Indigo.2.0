Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Partial Public Class CHREGESTADET
    Inherits Entity(Of CHREGESTADET)

    <DataMember()>
    Public Property CodeNameCups

    <DataMember()>
    Public Property CUPSEntityContractDescriptionId As Integer?

    <DataMember()>
    Public Property Liquidated As Boolean

End Class
