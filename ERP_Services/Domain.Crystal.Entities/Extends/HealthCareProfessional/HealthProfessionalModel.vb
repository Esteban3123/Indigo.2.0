Imports System.Runtime.Serialization
Imports Domain.Base.Entities
Imports Domain.Entities

<DataContract()>
Public Class HealthProfessionalModel

    <DataMember()>
    Public Property HealthProfessional As HealthProfessional

    <DataMember()>
    Public Property INPROFSAL As INPROFSAL

End Class
