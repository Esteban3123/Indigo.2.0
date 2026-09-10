#Region "Imports"

Imports DevExpress.Xpo

#End Region
<Persistent("Treasury.BankReconciliationAutomaticDetail")>
Public Class BankReconciliationAutomaticDetailXpo
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
    <Association("TreasuryBankReconciliationAutomaticDetail_Reference_TreasuryBankReconciliationAutomatic")>
    Public Property BankReconciliationAutomaticId() As BankReconciliationAutomaticXpo
        Get
            Return fBankReconciliationAutomaticId
        End Get
        Set(value As BankReconciliationAutomaticXpo)
            SetPropertyValue("BankReconciliationAutomaticId", fBankReconciliationAutomaticId, value)
        End Set
    End Property

    Dim fDocumentType As Integer
    Public Property DocumentType() As Integer
        Get
            Return fDocumentType
        End Get
        Set(value As Integer)
            SetPropertyValue(Of Integer)("DocumentType", fDocumentType, value)
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

    Dim fValue As Decimal
    Public Property Value() As Decimal
        Get
            Return fValue
        End Get
        Set(value As Decimal)
            SetPropertyValue(Of Decimal)("Value", fValue, value)
        End Set
    End Property

    Dim fEntityId As Integer
    Public Property EntityId() As Integer
        Get
            Return fEntityId
        End Get
        Set(value As Integer)
            SetPropertyValue(Of Integer)("EntityId", fEntityId, value)
        End Set
    End Property

    Dim fEntityCode As String
    Public Property EntityCode() As String
        Get
            Return fEntityCode
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("EntityCode", fEntityCode, value)
        End Set
    End Property

    Dim fEntityName As String
    Public Property EntityName() As String
        Get
            Return fEntityName
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("EntityName", fEntityName, value)
        End Set
    End Property

    Dim fReconciled As Boolean
    Public Property Reconciled() As Boolean
        Get
            Return fReconciled
        End Get
        Set(value As Boolean)
            SetPropertyValue(Of Boolean)("Reconciled", fReconciled, value)
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
