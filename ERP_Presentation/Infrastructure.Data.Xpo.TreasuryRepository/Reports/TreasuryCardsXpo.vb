Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Treasury.Cards")> _
Public Class TreasuryCardsXpo
    Inherits XPLiteObject
    Dim fId As Integer
    <Key(True)> _
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property
    Dim fCode As String
    '<Indexed(Name:="IX_Card", Unique:=True)> _
    <Size(20)> _
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
    Dim fIdThirdParty As Integer
    Public Property IdThirdParty() As Integer
        Get
            Return fIdThirdParty
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdThirdParty", fIdThirdParty, value)
        End Set
    End Property
    Dim fIdCashReceiptConceptCommision As Integer
    Public Property IdCashReceiptConceptCommision() As Integer
        Get
            Return fIdCashReceiptConceptCommision
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdCashReceiptConceptCommision", fIdCashReceiptConceptCommision, value)
        End Set
    End Property
    Dim fIdRetentionConceptCommision As Integer
    Public Property IdRetentionConceptCommision() As Integer
        Get
            Return fIdRetentionConceptCommision
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdRetentionConceptCommision", fIdRetentionConceptCommision, value)
        End Set
    End Property
    Dim fIdCashReceiptConceptRTF As Integer
    Public Property IdCashReceiptConceptRTF() As Integer
        Get
            Return fIdCashReceiptConceptRTF
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdCashReceiptConceptRTF", fIdCashReceiptConceptRTF, value)
        End Set
    End Property
    Dim fIdRetentionConceptRTF As Integer
    Public Property IdRetentionConceptRTF() As Integer
        Get
            Return fIdRetentionConceptRTF
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdRetentionConceptRTF", fIdRetentionConceptRTF, value)
        End Set
    End Property
    Dim fIdCashReceiptConceptICA As Integer
    Public Property IdCashReceiptConceptICA() As Integer
        Get
            Return fIdCashReceiptConceptICA
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdCashReceiptConceptICA", fIdCashReceiptConceptICA, value)
        End Set
    End Property
    Dim fIdRetentionConceptICA As Integer
    Public Property IdRetentionConceptICA() As Integer
        Get
            Return fIdRetentionConceptICA
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdRetentionConceptICA", fIdRetentionConceptICA, value)
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
    Dim fCreationUser As String
    <Size(20)> _
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
    <Size(20)> _
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
    <Association("TreasuryPaymentMethodsXpoReferencesTreasuryCardsXpo", GetType(TreasuryPaymentMethodsXpo))> _
    Public ReadOnly Property TreasuryPaymentMethodsXpo() As XPCollection(Of TreasuryPaymentMethodsXpo)
        Get
            Return GetCollection(Of TreasuryPaymentMethodsXpo)("TreasuryPaymentMethodsXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
