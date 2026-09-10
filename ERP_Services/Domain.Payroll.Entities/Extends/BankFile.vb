Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Partial Public Class BankFile

    <DataMember()>
    Property CompanyNit As String

    <DataMember()>
    Property CompanyName As String

    <DataMember()>
    Property ThirdPartyId As Integer

    <DataMember()>
    Property ThirdPartyNit As String

    <DataMember()>
    Property ThirdPartyName As String

    <DataMember()>
    Property EntityBankAccountType As Byte

    <DataMember()>
    Property EntityBankAccountNumber As String

    <DataMember()>
    Property MainAccountId As Integer

    <DataMember()>
    Property BankName As String

    <DataMember()>
    Property BankFileCode As String

    <DataMember()>
    Property BankCenitCode As String

    <DataMember()>
    Property ExpenseConceptName As String

    <DataMember()>
    Property ExpenseConceptMainAccountId As Integer

    <DataMember()>
    Property VoucherTransactionCode As String


    <DataMember()>
    Property BankAccountCurrencyId As Integer?
End Class