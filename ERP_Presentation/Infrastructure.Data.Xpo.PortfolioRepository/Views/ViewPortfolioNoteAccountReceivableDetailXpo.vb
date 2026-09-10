Imports DevExpress.Xpo

<Persistent("Portfolio.ViewPortfolioNoteAccountReceivableDetail")>
Public Class ViewPortfolioNoteAccountReceivableDetailXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As Integer
    <Key()>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fPortfolioNoteAccountReceivableId As PortfolioNoteAccountReceivableAdvanceReportXpo
    <Association("Portfolio_PortfolioNoteAccountReceivableDetail_References_Portfolio_PortfolioNoteAccountReceivableAdvance")>
    Public Property PortfolioNoteAccountReceivableId() As PortfolioNoteAccountReceivableAdvanceReportXpo
        Get
            Return fPortfolioNoteAccountReceivableId
        End Get
        Set(ByVal value As PortfolioNoteAccountReceivableAdvanceReportXpo)
            SetPropertyValue(Of PortfolioNoteAccountReceivableAdvanceReportXpo)("PortfolioNoteAccountReceivableId", fPortfolioNoteAccountReceivableId, value)
        End Set
    End Property

    Dim fBillingGroupCodeName As String
    Public Property BillingGroupCodeName() As String
        Get
            Return fBillingGroupCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BillingGroupCodeName", fBillingGroupCodeName, value)
        End Set
    End Property

    Dim fCodeName As String
    Public Property CodeName() As String
        Get
            Return fCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeName", fCodeName, value)
        End Set
    End Property

    Dim fAlternativeCodeName As String
    Public Property AlternativeCodeName() As String
        Get
            Return fAlternativeCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AlternativeCodeName", fAlternativeCodeName, value)
        End Set
    End Property

    Dim fMainAccountNumberName As String
    Public Property MainAccountNumberName() As String
        Get
            Return fMainAccountNumberName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MainAccountNumberName", fMainAccountNumberName, value)
        End Set
    End Property

    Dim fCostCenterCodeName As String
    Public Property CostCenterCodeName() As String
        Get
            Return fCostCenterCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CostCenterCodeName", fCostCenterCodeName, value)
        End Set
    End Property

    Dim fQuantity As Integer
    Public Property Quantity() As Integer
        Get
            Return fQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Quantity", fQuantity, value)
        End Set
    End Property

    Dim fUnitSalesPrice As Decimal
    Public Property UnitSalesPrice() As Decimal
        Get
            Return fUnitSalesPrice
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("UnitSalesPrice", fUnitSalesPrice, value)
        End Set
    End Property

    Dim fTotalSalesPrice As Decimal
    Public Property TotalSalesPrice() As Decimal
        Get
            Return fTotalSalesPrice
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalSalesPrice", fTotalSalesPrice, value)
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

#End Region

#Region "Builder"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

End Class
