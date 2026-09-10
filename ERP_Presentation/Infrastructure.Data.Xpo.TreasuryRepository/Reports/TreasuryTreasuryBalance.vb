Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Treasury.TreasuryBalance")> _
Public Class TreasuryTreasuryBalance
    Inherits XPLiteObject

#Region "Member"
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
    Dim fDocumentNumber As String
    <Size(20)>
    Public Property DocumentNumber() As String
        Get
            Return fDocumentNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DocumentNumber", fDocumentNumber, value)
        End Set
    End Property
    Dim fDocumentDate As DateTime
    Public Property DocumentDate() As DateTime
        Get
            Return fDocumentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DocumentDate", fDocumentDate, value)
        End Set
    End Property
    Dim fDocumentType As Byte
    Public Property DocumentType() As Byte
        Get
            Return fDocumentType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("DocumentType", fDocumentType, value)
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
    Dim fCashRegisterId As TreasuryCashRegistersXpo
    <Association("TreasuryTreasuryBalanceReferencesTreasuryCashRegistersXpo")>
    Public Property CashRegisterId() As TreasuryCashRegistersXpo
        Get
            Return fCashRegisterId
        End Get
        Set(ByVal value As TreasuryCashRegistersXpo)
            SetPropertyValue(Of TreasuryCashRegistersXpo)("CashRegisterId", fCashRegisterId, value)
        End Set
    End Property
    Dim fEntityBankAccountId As TreasuryEntityBankAccountsXpo
    <Association("TreasuryTreasuryBalanceReferencesTreasuryEntityBankAccountsXpo")>
    Public Property EntityBankAccountId() As TreasuryEntityBankAccountsXpo
        Get
            Return fEntityBankAccountId
        End Get
        Set(ByVal value As TreasuryEntityBankAccountsXpo)
            SetPropertyValue(Of TreasuryEntityBankAccountsXpo)("EntityBankAccountId", fEntityBankAccountId, value)
        End Set
    End Property
    Dim fPreviousBalance As Decimal
    Public Property PreviousBalance() As Decimal
        Get
            Return fPreviousBalance
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PreviousBalance", fPreviousBalance, value)
        End Set
    End Property
    Dim fValueMovement As Decimal
    Public Property ValueMovement() As Decimal
        Get
            Return fValueMovement
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueMovement", fValueMovement, value)
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
#End Region
#Region "NonPersistent"
    'Propiedad Añadida
    Dim fBalanceMonthStart As Long
    <NonPersistent()>
    Public Property BalanceMonthStart() As Long
        Get
            Return fBalanceMonthStart
        End Get
        Set(ByVal value As Long)
            Me.fBalanceMonthStart = value
        End Set
    End Property
    'Propiedad Añadida
    Dim fBalanceMonthEnd As Long
    <NonPersistent()>
    Public Property BalanceMonthEnd() As Long
        Get
            Return fBalanceMonthEnd
        End Get
        Set(ByVal value As Long)
            Me.fBalanceMonthEnd = value
        End Set
    End Property
#End Region
#Region "Navigation"
    <Association("TreasuryTreasuryBalance_References_TreasuryRevaluationDetailXpo", GetType(TreasuryRevaluationDetailXpo))>
    Public ReadOnly Property TreasuryRevaluationDetailXpo() As XPCollection(Of TreasuryRevaluationDetailXpo)
        Get
            Return GetCollection(Of TreasuryRevaluationDetailXpo)("TreasuryRevaluationDetailXpo")
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
