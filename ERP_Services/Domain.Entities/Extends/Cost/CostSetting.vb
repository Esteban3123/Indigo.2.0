Imports System.Runtime.Serialization

Public Class CostSetting

    <DataMember()> _
    Public Property FullNameJournalVoucherType As String

    <DataMember()>
    Public Property AccountPayableConceptDescription As String


    <DataMember()>
    Public Property ProvisionJournalVoucherTypeDescription As String

    <DataMember()>
    Public Property ProvisionReversalJournalVoucherTypeDescription As String

End Class
