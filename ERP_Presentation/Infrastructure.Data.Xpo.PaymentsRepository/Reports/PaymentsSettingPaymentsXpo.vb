Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payments.SettingPayments")> _
Public Class PaymentsSettingPaymentsXpo
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
    Dim fIdJournalVoucherAccountPayable As Integer
    Public Property IdJournalVoucherAccountPayable() As Integer
        Get
            Return fIdJournalVoucherAccountPayable
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdJournalVoucherAccountPayable", fIdJournalVoucherAccountPayable, value)
        End Set
    End Property
    Dim fIdJournalVoucherTranslation As Integer
    Public Property IdJournalVoucherTranslation() As Integer
        Get
            Return fIdJournalVoucherTranslation
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdJournalVoucherTranslation", fIdJournalVoucherTranslation, value)
        End Set
    End Property
    Dim fIdJournalVoucherCreditNotes As Integer
    Public Property IdJournalVoucherCreditNotes() As Integer
        Get
            Return fIdJournalVoucherCreditNotes
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdJournalVoucherCreditNotes", fIdJournalVoucherCreditNotes, value)
        End Set
    End Property
    Dim fIdJournalVoucherDebitNotes As Integer
    Public Property IdJournalVoucherDebitNotes() As Integer
        Get
            Return fIdJournalVoucherDebitNotes
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdJournalVoucherDebitNotes", fIdJournalVoucherDebitNotes, value)
        End Set
    End Property
    Dim fIdJournalVocuherAmortization As Integer
    Public Property IdJournalVocuherAmortization() As Integer
        Get
            Return fIdJournalVocuherAmortization
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdJournalVocuherAmortization", fIdJournalVocuherAmortization, value)
        End Set
    End Property
    Dim fObligationDebitValue As Boolean
    Public Property ObligationDebitValue() As Boolean
        Get
            Return fObligationDebitValue
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ObligationDebitValue", fObligationDebitValue, value)
        End Set
    End Property
    Dim fBudgetInterface As Boolean
    Public Property BudgetInterface() As Boolean
        Get
            Return fBudgetInterface
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("BudgetInterface", fBudgetInterface, value)
        End Set
    End Property
    Dim fOnlyObligation As Boolean
    Public Property OnlyObligation() As Boolean
        Get
            Return fOnlyObligation
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("OnlyObligation", fOnlyObligation, value)
        End Set
    End Property
    Dim fObligationBudgetInterface As Boolean
    Public Property ObligationBudgetInterface() As Boolean
        Get
            Return fObligationBudgetInterface
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ObligationBudgetInterface", fObligationBudgetInterface, value)
        End Set
    End Property
    Dim fPostulateBudgetInterfaceBy As Integer
    Public Property PostulateBudgetInterfaceBy() As Integer
        Get
            Return fPostulateBudgetInterfaceBy
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PostulateBudgetInterfaceBy", fPostulateBudgetInterfaceBy, value)
        End Set
    End Property
    Dim fIdOperatingUnit As Integer
    Public Property IdOperatingUnit() As Integer
        Get
            Return fIdOperatingUnit
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdOperatingUnit", fIdOperatingUnit, value)
        End Set
    End Property
    Dim fNameMinimumAgeRange As String
    <Size(100)> _
    Public Property NameMinimumAgeRange() As String
        Get
            Return fNameMinimumAgeRange
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NameMinimumAgeRange", fNameMinimumAgeRange, value)
        End Set
    End Property
    Dim fNameMaximumAgeRange As String
    <Size(100)> _
    Public Property NameMaximumAgeRange() As String
        Get
            Return fNameMaximumAgeRange
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NameMaximumAgeRange", fNameMaximumAgeRange, value)
        End Set
    End Property
    Dim fMaximunAgeRange As Integer
    Public Property MaximunAgeRange() As Integer
        Get
            Return fMaximunAgeRange
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MaximunAgeRange", fMaximunAgeRange, value)
        End Set
    End Property
    Dim fState As Boolean
    Public Property State() As Boolean
        Get
            Return fState
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("State", fState, value)
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
    <Association("PaymentsAgesPaymentsXpoReferencesPaymentsSettingPaymentsXpo", GetType(PaymentsAgesPaymentsXpo))> _
    Public ReadOnly Property PaymentsAgesPaymentsXpo() As XPCollection(Of PaymentsAgesPaymentsXpo)
        Get
            Return GetCollection(Of PaymentsAgesPaymentsXpo)("PaymentsAgesPaymentsXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
