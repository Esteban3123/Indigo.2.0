#Region "Imports"

Imports DevExpress.Xpo

#End Region
<Persistent("Treasury.BankReconciliationAutomaticExtractDetail")>
Public Class BankReconciliationAutomaticExtractDetailXpo
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

    Dim fBankReconciliationAutomaticId As BankReconciliationAutomaticXpo
    <Association("TreasuryBankReconciliationAutomaticExtractDetail_Reference_TreasuryBankReconciliationAutomatic")>
    Public Property BankReconciliationAutomaticId() As BankReconciliationAutomaticXpo
        Get
            Return fBankReconciliationAutomaticId
        End Get
        Set(value As BankReconciliationAutomaticXpo)
            SetPropertyValue(Of BankReconciliationAutomaticXpo)("BankReconciliationAutomaticId", fBankReconciliationAutomaticId, value)
        End Set
    End Property

    Dim fValue As Decimal
    Public Property Value As Decimal
        Get
            Return fValue
        End Get
        Set(value As Decimal)
            SetPropertyValue(Of Decimal)("Value", fValue, value)
        End Set
    End Property

    Dim fDocumentDate As DateTime
    Public Property DocumentDate() As DateTime
        Get
            Return fDocumentDate
        End Get
        Set(value As DateTime)
            SetPropertyValue(Of DateTime)("DocumentDate", fDocumentDate, value)
        End Set
    End Property

    Dim fNature As Byte
    Public Property Nature() As Byte
        Get
            Return fNature
        End Get
        Set(value As Byte)
            SetPropertyValue(Of Byte)("Nature", fNature, value)
        End Set
    End Property

    Dim fReconciled As Boolean
    Public Property Reconciled As Boolean
        Get
            Return fReconciled
        End Get
        Set(value As Boolean)
            SetPropertyValue(Of Boolean)("Reconciled", fReconciled, value)
        End Set
    End Property
#End Region

#Region "Builders"

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
