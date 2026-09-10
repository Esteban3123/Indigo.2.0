Imports DevExpress.Xpo

<Persistent("Budget.ViewListAvailabilityDetail")>
Public Class ViewListAvailabilityDetailXpo
    Inherits XPLiteObject

#Region "Properties"

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

    Dim fBudgetaryValidityId As Integer
    Public Property BudgetaryValidityId() As Integer
        Get
            Return fBudgetaryValidityId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("BudgetaryValidityId", fBudgetaryValidityId, value)
        End Set
    End Property

    Dim fAvailabilityCode As String
    Public Property AvailabilityCode() As String
        Get
            Return fAvailabilityCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AvailabilityCode", fAvailabilityCode, value)
        End Set
    End Property

    Dim fCategoryCodeName As String
    Public Property CategoryCodeName() As String
        Get
            Return fCategoryCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CategoryCodeName", fCategoryCodeName, value)
        End Set
    End Property

    Dim fFinancialSourceCodeName As String
    Public Property FinancialSourceCodeName() As String
        Get
            Return fFinancialSourceCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FinancialSourceCodeName", fFinancialSourceCodeName, value)
        End Set
    End Property

    Dim fRevenueTypeCodeName As String
    Public Property RevenueTypeCodeName() As String
        Get
            Return fRevenueTypeCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RevenueTypeCodeName", fRevenueTypeCodeName, value)
        End Set
    End Property

    Dim fBalance As Decimal
    Public Property Balance() As Decimal
        Get
            Return fBalance
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Balance", fBalance, value)
        End Set
    End Property

#End Region

#Region "Custom Members"

    <PersistentAlias("Concat(AvailabilityCode, ' - ', CategoryCodeName, ' - ', FinancialSourceCodeName, ' - ', RevenueTypeCodeName)")>
    Public ReadOnly Property Description As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("Description"))
        End Get
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
