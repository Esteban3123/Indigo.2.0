Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Partial Public Class BankFileDetail

    <DataMember()>
    Property ThirdPartyId As Integer

    <DataMember()>
    Property ThirdPartyIdentificationType As Integer

    <DataMember()>
    Property ThirdPartyNit As String

    <DataMember()>
    Property ThirdPartyName As String

    <DataMember()>
    Property EmployeeBankCenitCode As String

    <DataMember()>
    Property EmployeeBankAchCode As String

End Class
