Imports DevExpress.Xpo

<Persistent("Portfolio.PortfolioNoteAccountReceivableAdvance")> _
Public Class PortfolioNoteAccountReceivableAdvanceReportXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As Integer
    <Key(True)>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fPortfolioNoteId As PortfolioNoteReportXpo
    <Association("Portfolio_PortfolioNoteAccountReceivableAdvanceReferencesPortfolio_PortfolioNote")>
    Public Property PortfolioNoteId() As PortfolioNoteReportXpo
        Get
            Return fPortfolioNoteId
        End Get
        Set(ByVal value As PortfolioNoteReportXpo)
            SetPropertyValue(Of PortfolioNoteReportXpo)("PortfolioNoteId", fPortfolioNoteId, value)
        End Set
    End Property

    Dim fAccountReceivableId As PortfolioAccountReceivableReportXpo
    <Association("PortfolioNoteAccountReceivableAdvanceReportXpoReferencesPortfolioAccountReceivableReportXpo")>
    Public Property AccountReceivableId() As PortfolioAccountReceivableReportXpo
        Get
            Return fAccountReceivableId
        End Get
        Set(ByVal value As PortfolioAccountReceivableReportXpo)
            SetPropertyValue(Of PortfolioAccountReceivableReportXpo)("AccountReceivableId", fAccountReceivableId, value)
        End Set
    End Property

    Dim fAccountReceivableShareId As Integer
    Public Property AccountReceivableShareId() As Integer
        Get
            Return fAccountReceivableShareId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AccountReceivableShareId", fAccountReceivableShareId, value)
        End Set
    End Property

    Dim fMainAccountId As GeneralLedgerMainAccountsXpo
    <Association("PortfolioNoteAccountReceivableAdvanceReportXpoReferencesGeneralLedgerMainAccountsXpo")>
    Public Property MainAccountId() As GeneralLedgerMainAccountsXpo
        Get
            Return fMainAccountId
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsXpo)("MainAccountId", fMainAccountId, value)
        End Set
    End Property

    Dim fAccountReceivableAccountingId As PortfolioAccountReceivableAccountingReportXpo
    <Association("PortfolioNoteAccountReceivableAdvanceReportXpoReferencesPortfolioAccountReceivableAccountingReportXpo")>
    Public Property AccountReceivableAccountingId() As PortfolioAccountReceivableAccountingReportXpo
        Get
            Return fAccountReceivableAccountingId
        End Get
        Set(ByVal value As PortfolioAccountReceivableAccountingReportXpo)
            SetPropertyValue(Of PortfolioAccountReceivableAccountingReportXpo)("AccountReceivableAccountingId", fAccountReceivableAccountingId, value)
        End Set
    End Property

    Dim fPortfolioAdvanceId As PortfolioAdvanceReportXpo
    <Association("PortfolioNoteAccountReceivableAdvanceReportXpoReferencesPortfolioAdvanceReportXpo")>
    Public Property PortfolioAdvanceId() As PortfolioAdvanceReportXpo
        Get
            Return fPortfolioAdvanceId
        End Get
        Set(ByVal value As PortfolioAdvanceReportXpo)
            SetPropertyValue(Of PortfolioAdvanceReportXpo)("PortfolioAdvanceId", fPortfolioAdvanceId, value)
        End Set
    End Property

    Dim fAdjusmentValue As Decimal
    Public Property AdjusmentValue() As Decimal
        Get
            Return fAdjusmentValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("AdjusmentValue", fAdjusmentValue, value)
        End Set
    End Property

    Dim fPercentageValue As Decimal
    Public Property PercentageValue() As Decimal
        Get
            Return fPercentageValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PercentageValue", fPercentageValue, value)
        End Set
    End Property

#End Region

#Region "Navigation Members"

    <Association("Portfolio_PortfolioNoteAccountReceivableDetail_References_Portfolio_PortfolioNoteAccountReceivableAdvance", GetType(ViewPortfolioNoteAccountReceivableDetailXpo))>
    Public ReadOnly Property Portfolio_PortfolioNoteAccountReceivableDetails() As XPCollection(Of ViewPortfolioNoteAccountReceivableDetailXpo)
        Get
            Return GetCollection(Of ViewPortfolioNoteAccountReceivableDetailXpo)("Portfolio_PortfolioNoteAccountReceivableDetails")
        End Get
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

End Class
