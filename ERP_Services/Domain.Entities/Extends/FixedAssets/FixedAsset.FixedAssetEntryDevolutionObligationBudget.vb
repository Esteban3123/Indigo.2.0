#Region "Imports"
Imports System.Runtime.Serialization
#End Region

Partial Public Class FixedAssetEntryDevolutionObligationBudget

    <DataMember()>
    Public Property ObligationCode As String

    <DataMember()>
    Public Property ObligationDocument As String

    <DataMember()>
    Public Property CategoryName As String

    <DataMember()>
    Public Property FinancialSourceDescription As String

    <DataMember()>
    Public Property RevenueTypeDescription As String

    <DataMember()>
    Public Property ObligationBalance As String

    <DataMember()>
    Public Property CommitmentDetailId As Integer

End Class
