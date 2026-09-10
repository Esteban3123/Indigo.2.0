Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Partial Class INPROFSAL
    Inherits Entity(Of INPROFSAL)

    <DataMember()>
    Public Property ContractDesc As String

    <DataMember()>
    Public Property SupplierDistributionLineDesc As String

    <DataMember()>
    Public Property MedicalFeesContractType As Byte

    <DataMember()>
    Public Property Specialty1Description As String

    <DataMember()>
    Public Property Specialty2Description As String

    <DataMember()>
    Public Property Specialty3Description As String

End Class
