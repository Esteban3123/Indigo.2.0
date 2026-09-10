Imports System.Runtime.Serialization

Partial Public Class SurgicalProcedureService
    <DataMember>
    Public Property CodeNameService As String

    <DataMember>
    Public Property ClassService As String

    <DataMember>
    Public Property ValueItemServiceOrderDetail As Decimal

    <DataMember()>
    Public Property TaxPercent As Decimal

    <DataMember()>
    Public Property AllowValueChange As Boolean

End Class

