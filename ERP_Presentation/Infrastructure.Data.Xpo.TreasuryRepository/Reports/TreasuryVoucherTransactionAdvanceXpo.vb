Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Treasury.VoucherTransactionAdvance")> _
Public Class TreasuryVoucherTransactionAdvanceXpo
    Inherits XPLiteObject
    Dim fId As Integer
    <Key(True)> _
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property
    Dim fIdVoucherTransactionD As TreasuryVoucherTransactionDetailsXpo
    <Association("TreasuryVoucherTransactionAdvanceXpoReferencesTreasuryVoucherTransactionDetailsXpo")> _
    Public Property IdVoucherTransactionD() As TreasuryVoucherTransactionDetailsXpo
        Get
            Return fIdVoucherTransactionD
        End Get
        Set(ByVal value As TreasuryVoucherTransactionDetailsXpo)
            SetPropertyValue(Of TreasuryVoucherTransactionDetailsXpo)("IdVoucherTransactionD", fIdVoucherTransactionD, value)
        End Set
    End Property
    Dim fPortfolioAdvanceId As PortfolioAdvanceReportXpo
    <Association("TreasuryVoucherTransactionAdvanceXpoReferencesPortfolioAdvanceReportXpo")> _
    Public Property PortfolioAdvanceId() As PortfolioAdvanceReportXpo
        Get
            Return fPortfolioAdvanceId
        End Get
        Set(ByVal value As PortfolioAdvanceReportXpo)
            SetPropertyValue(Of PortfolioAdvanceReportXpo)("PortfolioAdvanceId", fPortfolioAdvanceId, value)
        End Set
    End Property
    Dim fValue As Decimal
    Public Property Value() As Decimal
        Get
            Return fValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Value", fValue, value)
        End Set
    End Property
    Dim fPercentage As Decimal
    Public Property Percentage() As Decimal
        Get
            Return fPercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Percentage", fPercentage, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
