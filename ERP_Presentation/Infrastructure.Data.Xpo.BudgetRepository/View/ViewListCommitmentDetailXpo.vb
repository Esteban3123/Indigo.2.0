Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Budget.ViewListCommitmentDetail")>
Public Class ViewListCommitmentDetailXpo
    Inherits XPLiteObject

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

    Dim fCommitmentCode As String
    Public Property CommitmentCode() As String
        Get
            Return fCommitmentCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CommitmentCode", fCommitmentCode, value)
        End Set
    End Property

    Dim fCommitmentId As Integer
    Public Property CommitmentId() As Integer
        Get
            Return fCommitmentId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CommitmentId", fCommitmentId, value)
        End Set
    End Property

    Dim fThirdPartyId As Integer
    Public Property ThirdPartyId() As Integer
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property

    Dim fStatus As Integer
    Public Property Status() As Integer
        Get
            Return fStatus
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Status", fStatus, value)
        End Set
    End Property

    Dim fCommitmentDetailId As Integer
    Public Property CommitmentDetailId() As Integer
        Get
            Return fCommitmentDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CommitmentDetailId", fCommitmentDetailId, value)
        End Set
    End Property

    Dim fExpiredDate As DateTime
    Public Property ExpiredDate() As DateTime
        Get
            Return fExpiredDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ExpiredDate", fExpiredDate, value)
        End Set
    End Property

    Dim fInitialValue As Decimal
    Public Property InitialValue() As Decimal
        Get
            Return fInitialValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("InitialValue", fInitialValue, value)
        End Set
    End Property

    Dim fTotalCommitment As Decimal
    Public Property TotalCommitment() As Decimal
        Get
            Return fTotalCommitment
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalCommitment", fTotalCommitment, value)
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

    Dim fCategoryCodeName As String
    Public Property CategoryCodeName() As String
        Get
            Return fCategoryCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CategoryCodeName", fCategoryCodeName, value)
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

    Dim fRevenueTypeCodeName As String
    Public Property RevenueTypeCodeName() As String
        Get
            Return fRevenueTypeCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RevenueTypeCodeName", fRevenueTypeCodeName, value)
        End Set
    End Property

    Dim fNullText As String
    Public Property NullText() As String
        Get
            Return fNullText
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NullText", fNullText, value)
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

    Dim fFinancialSourceCodeName As String
    Public Property FinancialSourceCodeName() As String
        Get
            Return fFinancialSourceCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FinancialSourceCodeName", fFinancialSourceCodeName, value)
        End Set
    End Property

    Dim fDocument As String
    Public Property Document() As String
        Get
            Return fDocument
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Document", fDocument, value)
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

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
