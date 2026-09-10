Imports System.Runtime.Serialization
Imports Domain.Base.Entities
Partial Class ADINGRESO
    Inherits Entity(Of ADINGRESO)

    <DataMember()>
    Public Property ExtHospitalizationDate As Date

    <DataMember()>
    Public Property ExtHospitalizationBed As String

    <DataMember()>
    Public Property ExtFunctionalUnit As String

    <DataMember()>
    Public Property ExtCenter As String

    <DataMember()>
    Public Property ExtMinWage As String

    <DataMember()>
    Public Property ExtTown As String

    <DataMember()>
    Public Property ExtIPS As String

    <DataMember()>
    Public Property ExtCareGroup As String

    <DataMember()>
    Public Property ExtCupsEntity As String

    <DataMember()>
    Public Property AdmissionCode As String

End Class
