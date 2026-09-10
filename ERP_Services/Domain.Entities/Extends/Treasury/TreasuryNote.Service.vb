Imports System.Runtime.Serialization

Public Class TreasuryNote

#Region "Properties"
    <DataMember()>
    Property FullNameVoucherTransaction As String
    <DataMember()>
    Property FullNameCashReceipt As String
    <DataMember()>
    Property VoucherTransactionValue As Decimal
    <DataMember()>
    Property FullNameCashRegister As String
    <DataMember>
    Property ConsignmentCode As String
    <DataMember()>
    Property FullNameEntityAccount As String
    <DataMember()>
    Property FullNameCostCenter As String
    <DataMember>
    Property CrossAccountCode As String
#End Region

#Region "Methods"
    Public Function GetValueNoteDetail() As Decimal
        Dim sumDebit As Decimal = TreasuryNoteDetail.Where(Function(y) y.Nature = 1).Sum(Function(x) x.Value)
        Dim sumCredit As Decimal = TreasuryNoteDetail.Where(Function(y) y.Nature = 2).Sum(Function(x) x.Value)
        Return Math.Abs(sumCredit - sumDebit)
    End Function
#End Region

End Class
