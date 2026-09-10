Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Payments.PaymentNotesAccountPayableAdvance")> _
Public Class PaymentsPaymentNotesAccountPayableAdvance
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
    Dim fPaymentNoteId As PaymentsPaymentNotes
    <Association("PaymentsPaymentNotesAccountPayableAdvanceReferencesPaymentsPaymentNotes")> _
    Public Property PaymentNoteId() As PaymentsPaymentNotes
        Get
            Return fPaymentNoteId
        End Get
        Set(ByVal value As PaymentsPaymentNotes)
            SetPropertyValue(Of PaymentsPaymentNotes)("PaymentNoteId", fPaymentNoteId, value)
        End Set
    End Property
    Dim fAccountPayableId As PaymentsAccountPayable
    <Association("PaymentsPaymentNotesAccountPayableAdvanceReferencesPaymentsAccountPayable")> _
    Public Property AccountPayableId() As PaymentsAccountPayable
        Get
            Return fAccountPayableId
        End Get
        Set(ByVal value As PaymentsAccountPayable)
            SetPropertyValue(Of PaymentsAccountPayable)("AccountPayableId", fAccountPayableId, value)
        End Set
    End Property
    Dim fAccountPayableShareId As PaymentsAccountPayableSharesXpoP
    <Association("PaymentsPaymentNotesAccountPayableAdvanceReferencesPaymentsAccountPayableSharesXpoP")> _
    Public Property AccountPayableShareId() As PaymentsAccountPayableSharesXpoP
        Get
            Return fAccountPayableShareId
        End Get
        Set(ByVal value As PaymentsAccountPayableSharesXpoP)
            SetPropertyValue(Of PaymentsAccountPayableSharesXpoP)("AccountPayableShareId", fAccountPayableShareId, value)
        End Set
    End Property
    Dim fAdvancePaymentId As PaymentsAdvancePaymentsXpo
    <Association("PaymentsPaymentNotesAccountPayableAdvanceReferencesPaymentsAdvancePayments")> _
    Public Property AdvancePaymentId() As PaymentsAdvancePaymentsXpo
        Get
            Return fAdvancePaymentId
        End Get
        Set(ByVal value As PaymentsAdvancePaymentsXpo)
            SetPropertyValue(Of PaymentsAdvancePaymentsXpo)("AdvancePaymentId", fAdvancePaymentId, value)
        End Set
    End Property
    Dim fAdjusmentValue As Decimal
    Public Property AdjusmentValue() As Decimal
        Get
            Return fAdjusmentValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("AdjusmentValue", fAdjusmentValue, value)
        End Set
    End Property
    Dim fPercentageValue As Decimal
    Public Property PercentageValue() As Decimal
        Get
            Return fPercentageValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PercentageValue", fPercentageValue, value)
        End Set
    End Property
    Dim fAdjustmentValueShare As Decimal
    Public Property AdjustmentValueShare() As Decimal
        Get
            Return fAdjustmentValueShare
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("AdjustmentValueShare", fAdjustmentValueShare, value)
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
