Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Treasury.CashReceiptAdvancePayment")> _
Public Class TreasuryCashReceiptAdvancePaymentXpo
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
    Dim fCashReceiptDetailId As TreasuryCashReceiptDetailsXpo
    <Association("TreasuryCashReceiptAdvancePaymentXpoReferencesTreasuryCashReceiptDetailsXpo")> _
    Public Property CashReceiptDetailId() As TreasuryCashReceiptDetailsXpo
        Get
            Return fCashReceiptDetailId
        End Get
        Set(ByVal value As TreasuryCashReceiptDetailsXpo)
            SetPropertyValue(Of TreasuryCashReceiptDetailsXpo)("CashReceiptDetailId", fCashReceiptDetailId, value)
        End Set
    End Property
    Dim fAdvancePaymentId As Integer
    Public Property AdvancePaymentId() As Integer
        Get
            Return fAdvancePaymentId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AdvancePaymentId", fAdvancePaymentId, value)
        End Set
    End Property
    Dim fAdvancePaymentCode As String
    <Size(20)> _
    Public Property AdvancePaymentCode() As String
        Get
            Return fAdvancePaymentCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AdvancePaymentCode", fAdvancePaymentCode, value)
        End Set
    End Property
    Dim fPaymentValue As Decimal
    Public Property PaymentValue() As Decimal
        Get
            Return fPaymentValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PaymentValue", fPaymentValue, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
