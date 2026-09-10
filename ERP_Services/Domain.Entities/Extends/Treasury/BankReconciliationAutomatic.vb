Imports System.Runtime.Serialization

Partial Public Class BankReconciliationAutomatic

#Region "Properties"

    <DataMember>
    Property EntityBankAccountCodeName As String

    <DataMember>
    Property Association As List(Of BankReconciliationAutomaticAssociation)

#End Region

End Class
