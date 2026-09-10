Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Partial Public Class Foreclousure
    <DataMember()>
    Property NameThirdPartyApplicant As String

    <DataMember()>
    Property NameConcept As String

    <DataMember()>
    Property NameCompanyJudgment As String

    <DataMember()>
    Property NameCity As String

    <DataMember()>
    Property NameThirdPartyBeneficiary As String
End Class
