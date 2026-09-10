Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Treasury.CashReceiptConcepts")>
Public Class Treasury_CashReceiptConceptsXpo
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

    Dim fCode As String
    <Size(20)>
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fName As String
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property

    Dim fAffectation As Byte
    Public Property Affectation() As Byte
        Get
            Return fAffectation
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Affectation", fAffectation, value)
        End Set
    End Property

    Dim fAffectBudget As Boolean
    Public Property AffectBudget() As Boolean
        Get
            Return fAffectBudget
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("AffectBudget", fAffectBudget, value)
        End Set
    End Property

    Dim fDiscount As Boolean
    Public Property Discount() As Boolean
        Get
            Return fDiscount
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Discount", fDiscount, value)
        End Set
    End Property

    Dim fNature As Byte
    Public Property Nature() As Byte
        Get
            Return fNature
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Nature", fNature, value)
        End Set
    End Property

    Dim fIdMainAccount As Integer
    Public Property IdMainAccount() As Integer
        Get
            Return fIdMainAccount
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdMainAccount", fIdMainAccount, value)
        End Set
    End Property

    Dim fStatus As Boolean
    Public Property Status() As Boolean
        Get
            Return fStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Status", fStatus, value)
        End Set
    End Property

    Dim fIsFixedAmountInvoiceAdvance As Boolean
    Public Property IsFixedAmountInvoiceAdvance() As Boolean
        Get
            Return fIsFixedAmountInvoiceAdvance
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsFixedAmountInvoiceAdvance", fIsFixedAmountInvoiceAdvance, value)
        End Set
    End Property

    Dim fCreationUser As String
    <Size(20)>
    Public Property CreationUser() As String
        Get
            Return fCreationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CreationUser", fCreationUser, value)
        End Set
    End Property

    Dim fCreationDate As DateTime
    Public Property CreationDate() As DateTime
        Get
            Return fCreationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CreationDate", fCreationDate, value)
        End Set
    End Property

    Dim fModificationUser As String
    <Size(20)>
    Public Property ModificationUser() As String
        Get
            Return fModificationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ModificationUser", fModificationUser, value)
        End Set
    End Property

    Dim fModificationDate As DateTime
    Public Property ModificationDate() As DateTime
        Get
            Return fModificationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ModificationDate", fModificationDate, value)
        End Set
    End Property

    <PersistentAlias("concat(Code,' - ', Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

    <Association("Treasury_CashReceiptsDetailsReferencesTreasury_CashReceiptConceptsXpo", GetType(Treasury_CashReceiptsDetailsXpo))>
    Public ReadOnly Property Treasury_CashReceiptsDetailsXpo() As XPCollection(Of Treasury_CashReceiptsDetailsXpo)
        Get
            Return GetCollection(Of Treasury_CashReceiptsDetailsXpo)("Treasury_CashReceiptsDetailsXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
