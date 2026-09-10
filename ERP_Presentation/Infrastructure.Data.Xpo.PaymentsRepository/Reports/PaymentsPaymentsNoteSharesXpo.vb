Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payments.PaymentsNoteShares")> _
Public Class PaymentsPaymentsNoteSharesXpo
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
    Dim fIdPaymentsNote As PaymentsPaymentNotes
    <Association("PaymentsPaymentsNoteSharesXpoReferencesPaymentsPaymentNotes")> _
    Public Property IdPaymentsNote() As PaymentsPaymentNotes
        Get
            Return fIdPaymentsNote
        End Get
        Set(ByVal value As PaymentsPaymentNotes)
            SetPropertyValue(Of PaymentsPaymentNotes)("IdPaymentsNote", fIdPaymentsNote, value)
        End Set
    End Property
    Dim fIdAccountPayableShares As PaymentsAccountPayableSharesXpoP
    <Association("PaymentsPaymentsNoteSharesXpoReferencesPaymentsAccountPayableSharesXpoP")> _
    Public Property IdAccountPayableShares() As PaymentsAccountPayableSharesXpoP
        Get
            Return fIdAccountPayableShares
        End Get
        Set(ByVal value As PaymentsAccountPayableSharesXpoP)
            SetPropertyValue(Of PaymentsAccountPayableSharesXpoP)("IdAccountPayableShares", fIdAccountPayableShares, value)
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
    Dim fIdAccountPayableConceptNotes As PaymentsAccountPayableConceptNotesXpo
    <Association("Payments_PaymentsNoteSharesXpoReferencesPayments_AccountPayableConceptNotes")> _
    Public Property IdAccountPayableConceptNotes() As PaymentsAccountPayableConceptNotesXpo
        Get
            Return fIdAccountPayableConceptNotes
        End Get
        Set(ByVal value As PaymentsAccountPayableConceptNotesXpo)
            SetPropertyValue(Of PaymentsAccountPayableConceptNotesXpo)("IdAccountPayableConceptNotes", fIdAccountPayableConceptNotes, value)
        End Set
    End Property
    Dim fValue As Decimal
    Public Property Value() As Decimal
        Get
            Return fValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Value", fValue, value)
        End Set
    End Property
    Dim fComments As String
    <Size(500)> _
    Public Property Comments() As String
        Get
            Return fComments
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Comments", fComments, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub


End Class
