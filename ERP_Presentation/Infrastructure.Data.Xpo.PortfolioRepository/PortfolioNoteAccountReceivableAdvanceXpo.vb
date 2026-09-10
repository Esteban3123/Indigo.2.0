Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Portfolio.PortfolioNoteAccountReceivableAdvance")> _
Partial Public Class PortfolioNoteAccountReceivableAdvanceXpo
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
    Dim fPortfolioNoteId As PortfolioNoteXpo
    <Association("Portfolio_PortfolioNoteAccountReceivableAdvanceReferencesPortfolio_PortfolioNote")> _
    Public Property PortfolioNoteId() As PortfolioNoteXpo
        Get
            Return fPortfolioNoteId
        End Get
        Set(ByVal value As PortfolioNoteXpo)
            SetPropertyValue(Of PortfolioNoteXpo)("PortfolioNoteId", fPortfolioNoteId, value)
        End Set
    End Property
    Dim fAccountReceivableId As Integer
    Public Property AccountReceivableId() As Integer
        Get
            Return fAccountReceivableId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AccountReceivableId", fAccountReceivableId, value)
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
    Dim fAccountReceivableAccountingId As Integer
    Public Property AccountReceivableAccountingId() As Integer
        Get
            Return fAccountReceivableAccountingId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AccountReceivableAccountingId", fAccountReceivableAccountingId, value)
        End Set
    End Property
    Dim fPortfolioAdvanceId As Integer
    Public Property PortfolioAdvanceId() As Integer
        Get
            Return fPortfolioAdvanceId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PortfolioAdvanceId", fPortfolioAdvanceId, value)
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
    Dim fAdjustmentValueShare As Decimal
    <NonPersistent()>
    Public Property AdjustmentValueShare() As Decimal
        Get
            Return fAdjustmentValueShare
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("AdjustmentValueShare", fAdjustmentValueShare, value)
        End Set
    End Property
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
