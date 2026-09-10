Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payments.AccountPayableConceptNotes")> _
Public Class PaymentsAccountPayableConceptNotesXpo
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
    '<Indexed(Name:="IX_PaymentsNoteConcept", Unique:=True)> _
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
    'columna que devuelve el codigo y el nombre concatenado
    <Size(120)> _
    <PersistentAlias("concat(concat(Code,' - '),Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property
    Dim fConceptType As Byte
    Public Property ConceptType() As Byte
        Get
            Return fConceptType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ConceptType", fConceptType, value)
        End Set
    End Property
    Dim fIdAccount As GeneralLedgerMainAccountsXpo
    <Association("PaymentsAccountPayableConceptNotesXpoReferencesGeneralLedgerMainAccountsXpo")> _
    Public Property IdAccount() As GeneralLedgerMainAccountsXpo
        Get
            Return fIdAccount
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsXpo)("IdAccount", fIdAccount, value)
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
    Dim fBehavior As Byte
    Public Property Behavior() As Byte
        Get
            Return fBehavior
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Behavior", fBehavior, value)
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
    <Association("Payments_PaymentsNoteDetailsReferencesPayments_AccountPayableConceptNotes", GetType(PaymentsPaymentsNoteDetailsXpo))> _
    Public ReadOnly Property Payments_PaymentsNoteDetailss() As XPCollection(Of PaymentsPaymentsNoteDetailsXpo)
        Get
            Return GetCollection(Of PaymentsPaymentsNoteDetailsXpo)("Payments_PaymentsNoteDetailss")
        End Get
    End Property
    <Association("Payments_PaymentsNoteSharesXpoReferencesPayments_AccountPayableConceptNotes", GetType(PaymentsPaymentsNoteSharesXpo))> _
    Public ReadOnly Property PaymentsPaymentsNoteSharesXpo() As XPCollection(Of PaymentsPaymentsNoteSharesXpo)
        Get
            Return GetCollection(Of PaymentsPaymentsNoteSharesXpo)("PaymentsPaymentsNoteSharesXpo")
        End Get
    End Property
    <Association("Payments_PaymentTransferOtherConceptReferencesPayments_AccountPayableConceptNotes", GetType(PaymentsPaymentTransferOtherConcept))> _
    Public ReadOnly Property PaymentsPaymentTransferOtherConcept() As XPCollection(Of PaymentsPaymentTransferOtherConcept)
        Get
            Return GetCollection(Of PaymentsPaymentTransferOtherConcept)("PaymentsPaymentTransferOtherConcept")
        End Get
    End Property
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
