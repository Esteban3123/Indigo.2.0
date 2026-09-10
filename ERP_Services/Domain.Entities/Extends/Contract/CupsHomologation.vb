Imports System.Runtime.Serialization

Partial Public Class CupsHomologation

    <DataMember>
    Property GuidHomologation As String

    <DataMember>
    Property CodeNameCupsEntity As String

    <DataMember>
    Property CodeNameIpsService As String

    <DataMember>
    Property Activated As Boolean

    <DataMember>
    Property IPSServiceDescription As String

    <DataMember>
    Property ServiceManualName As String

    <DataMember()>
    Property Presentation As Byte?

    <DataMember()>
    Property SubTotalSalesValue As Decimal

    <DataMember()>
    Property SubTotalSalesValueWithSurcharge As Decimal

    <DataMember>
    Public Property RuleType As Byte?
End Class
