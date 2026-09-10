Imports System.Runtime.Serialization

Partial Public Class BankReconciliationAutomaticDetail

#Region "Properties"

    <DataMember>
    Property ThirdPartyNitName As String

    <DataMember>
    Property NitThirdParty As String

    <DataMember>
    Property DocumentNumber As String

    <DataMember>
    Property Observations As String

    <DataMember>
    Property CreationUser As String

    <DataMember>
    Property ConfirmationUser As String

    <DataMember>
    Property UploadStatementsDetailId As Integer

    <DataMember>
    Property Checked As Boolean

    <DataMember>
    Property Period As String

    <DataMember>
    Property ListCashReceipts As List(Of CashReceipts)

#End Region

End Class
