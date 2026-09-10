Imports DevExpress.Xpo

<Persistent("Payments.ViewAccountPayableCommitment")>
Public Class ViewAccountPayableCommitmentXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As String
    <Key(True)>
    Public Property Id() As String
        Get
            Return fId
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Id", fId, value)
        End Set
    End Property

    Dim fAccountPayableId As PaymentsAccountPayable
    <Association("PaymentsViewAccountPayableCommitment_References_PaymentsAccountPayable")>
    Public Property AccountPayableId() As PaymentsAccountPayable
        Get
            Return fAccountPayableId
        End Get
        Set(ByVal value As PaymentsAccountPayable)
            SetPropertyValue(Of PaymentsAccountPayable)("AccountPayableId", fAccountPayableId, value)
        End Set
    End Property

    Dim fBudgetId As Integer
    Public Property BudgetId() As Integer
        Get
            Return fBudgetId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("BudgetId", fBudgetId, value)
        End Set
    End Property

    Dim fCategoryCode As String
    Public Property CategoryCode() As String
        Get
            Return fCategoryCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CategoryCode", fCategoryCode, value)
        End Set
    End Property

    Dim fCategoryName As String
    Public Property CategoryName() As String
        Get
            Return fCategoryName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CategoryName", fCategoryName, value)
        End Set
    End Property

    Dim fFinancialSourceCode As String
    Public Property FinancialSourceCode() As String
        Get
            Return fFinancialSourceCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FinancialSourceCode", fFinancialSourceCode, value)
        End Set
    End Property

    Dim fFinancialSourceName As String
    Public Property FinancialSourceName() As String
        Get
            Return fFinancialSourceName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FinancialSourceName", fFinancialSourceName, value)
        End Set
    End Property

    Dim fRevenueTypeCode As String
    Public Property RevenueTypeCode() As String
        Get
            Return fRevenueTypeCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RevenueTypeCode", fRevenueTypeCode, value)
        End Set
    End Property

    Dim fRevenueTypeName As String
    Public Property RevenueTypeName() As String
        Get
            Return fRevenueTypeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RevenueTypeName", fRevenueTypeName, value)
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
