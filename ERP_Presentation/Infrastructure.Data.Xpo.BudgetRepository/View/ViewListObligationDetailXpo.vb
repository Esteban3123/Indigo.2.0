Imports DevExpress.Xpo

<Persistent("Budget.ViewListObligationDetail")>
Public Class ViewListObligationDetailXpo
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

    Dim fCommitmentDetailId As Integer
    Public Property CommitmentDetailId() As Integer
        Get
            Return fCommitmentDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CommitmentDetailId", fCommitmentDetailId, value)
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

    Dim fCommitmentDocument As String
    Public Property CommitmentDocument() As String
        Get
            Return fCommitmentDocument
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CommitmentDocument", fCommitmentDocument, value)
        End Set
    End Property

    Dim fCategoryId As Integer
    Public Property CategoryId() As Integer
        Get
            Return fCategoryId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CategoryId", fCategoryId, value)
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

    Dim fRevenueTypeId As Integer
    Public Property RevenueTypeId() As Integer
        Get
            Return fRevenueTypeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RevenueTypeId", fRevenueTypeId, value)
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

    Dim fInitialValue As Decimal
    Public Property InitialValue() As Decimal
        Get
            Return fInitialValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("InitialValue", fInitialValue, value)
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

    Dim fEntityId As Integer
    Public Property EntityId() As Integer
        Get
            Return fEntityId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EntityId", fEntityId, value)
        End Set
    End Property

    Dim fEntityCode As String
    Public Property EntityCode() As String
        Get
            Return fEntityCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EntityCode", fEntityCode, value)
        End Set
    End Property

    Dim fEntityName As String
    Public Property EntityName() As String
        Get
            Return fEntityName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EntityName", fEntityName, value)
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
