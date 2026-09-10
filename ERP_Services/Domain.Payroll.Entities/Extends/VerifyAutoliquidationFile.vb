Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Partial Public Class VerifyAutoliquidationFile
    <DataMember()>
    Property NitEmployee As String

    <DataMember()>
    Property NameEmployee As String

    <DataMember()>
    Property FirstName As String

    <DataMember()>
    Property SecondName As String

    <DataMember()>
    Property FirstLastName As String

    <DataMember()>
    Property SecondLastName As String

    <DataMember()>
    Property DocumentType As Integer

    <DataMember()>
    Property ContributorSubtype As Integer

    <DataMember()>
    Property LegalSalaryMinimum As Decimal

    <DataMember()>
    Property SMMLVAmountExemption As Integer
End Class
