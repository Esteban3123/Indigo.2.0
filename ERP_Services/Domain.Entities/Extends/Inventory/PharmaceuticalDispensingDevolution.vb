Imports System.Runtime.Serialization
Partial Public Class PharmaceuticalDispensingDevolution
    <DataMember>
    Property CodeNameWarehouse As String
    <DataMember>
    Public Property CodePatient As String
    <DataMember>
    Public Property NamePatient As String

    <DataMember>
    Public Property ConsecutiveCrystal As Integer

    <DataMember>
    Public Property CareCenterCode As String
    <DataMember>
    Public Property FunctionUnitCode As String

    <DataMember>
    Public Property DevolutionOrigin As String

    <DataMember>
    Public Property Prefix As String
    <DataMember>
    Public Property CodeNameUser As String
End Class
