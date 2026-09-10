Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Budget.ViewListObligationOfEntranceVoucher")>
Public Class ViewListObligationOfEntranceVoucherXpo
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

    Dim fEntranceVoucherId As Integer
    Public Property EntranceVoucherId() As Integer
        Get
            Return fEntranceVoucherId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EntranceVoucherId", fEntranceVoucherId, value)
        End Set
    End Property

    Dim fAccountPayableId As Integer
    Public Property AccountPayableId() As Integer
        Get
            Return fAccountPayableId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AccountPayableId", fAccountPayableId, value)
        End Set
    End Property

    Dim fObligationDetailId As Integer
    Public Property ObligationDetailId() As Integer
        Get
            Return fObligationDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ObligationDetailId", fObligationDetailId, value)
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

    Dim fObligationId As Integer
    Public Property ObligationId() As Integer
        Get
            Return fObligationId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ObligationId", fObligationId, value)
        End Set
    End Property

    Dim fObligationCode As String
    Public Property ObligationCode() As String
        Get
            Return fObligationCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ObligationCode", fObligationCode, value)
        End Set
    End Property

    Dim fObligationDocument As String
    Public Property ObligationDocument() As String
        Get
            Return fObligationDocument
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ObligationDocument", fObligationDocument, value)
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

    Dim fCategoryName As String
    Public Property CategoryName() As String
        Get
            Return fCategoryName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CategoryName", fCategoryName, value)
        End Set
    End Property

    Dim fCategoryDescription As String
    Public Property CategoryDescription() As String
        Get
            Return fCategoryDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CategoryDescription", fCategoryDescription, value)
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

    Dim fRevenueTypeDescription As String
    Public Property RevenueTypeDescription() As String
        Get
            Return fRevenueTypeDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RevenueTypeDescription", fRevenueTypeDescription, value)
        End Set
    End Property

    Dim fFinancialSourceId As Integer
    Public Property FinancialSourceId() As Integer
        Get
            Return fFinancialSourceId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("FinancialSourceId", fFinancialSourceId, value)
        End Set
    End Property

    Dim fFinancialSourceDescription As String
    Public Property FinancialSourceDescription() As String
        Get
            Return fFinancialSourceDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FinancialSourceDescription", fFinancialSourceDescription, value)
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
