Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Partial Class CHREGESTA
    Inherits Entity(Of CHREGESTA)

    <DataMember()>
    Public Property CupsId As Integer

    <DataMember()>
    Public Property CUPSEntityContractDescriptionId As Integer?

    <DataMember()>
    Public Property IPSServiceId As Integer

    <DataMember()>
    Public Property Value As Decimal

    <DataMember()>
    Public Property TotalTime As String

    <DataMember()>
    Public Property TotalUnits As Integer

    <DataMember()>
    Public Property ListFolios As List(Of StayFolios)

    <DataMember()>
    Public Property FunctionalUnitCodeName As String

    <DataMember()>
    Property RateManualId As Integer?

    <DataMember()>
    Property RateManualDetailId As Integer?

    <DataMember()>
    Property RateManualType As Byte?

    <DataMember>
    Public Property CUPsCodeName As String

End Class
