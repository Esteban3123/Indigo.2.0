Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Partial Public Class SettingPortfolio
    Inherits Entity(Of Domain.Entities.SettingPortfolio)
    <DataMember>
    Property CodeNameJournalVoucerTypeCreditNote As String

    <DataMember>
    Property CodeNameJournalVoucerTypeDebitNote As String

    <DataMember>
    Property CodeNameJournalVoucerTypeTransfer As String

    <DataMember>
    Property CodeNameJournalVoucerTypeProvision As String

    <DataMember>
    Property CodeNameJournalVoucerTypeFilingAccount As String

    <DataMember>
    Property CodeNameJournalVoucherTypeDocumentAccountReceivable As String

    <DataMember>
    Property CodeNameJournalVoucherTypeDeteriorationAccount As String

    <DataMember>
    Property Legalcol As Boolean

#Region "Budget Interface"

    <DataMember()>
    Public Property BudgetaryEntityId As Integer?

    <DataMember()>
    Public Property BudgetaryEntityDescription As String

    <DataMember()>
    Public Property BudgetaryValidityId As Integer?

    <DataMember()>
    Public Property BudgetaryValidityDescription As String

    <DataMember()>
    Public Property DependencyDescription As String

#End Region

End Class
