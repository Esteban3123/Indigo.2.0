Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Treasury.CashReceiptDetailAccountPayable")>
Public Class TreasuryCashReceiptDetailAccountPayableXpo
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
    Dim fCashReceiptDetailId As TreasuryCashReceiptDetailsXpo
    <Association("TreasuryCashReceiptAccountPayableXpoReferencesTreasuryCashReceiptDetailsXpo")>
    Public Property CashReceiptDetailId() As TreasuryCashReceiptDetailsXpo
        Get
            Return fCashReceiptDetailId
        End Get
        Set(ByVal value As TreasuryCashReceiptDetailsXpo)
            SetPropertyValue(Of TreasuryCashReceiptDetailsXpo)("CashReceiptDetailId", fCashReceiptDetailId, value)
        End Set
    End Property
    <Association("TreasuryCashReceiptDetailAccountPayableXpoReferencesPaymentsAccountPayableXpo")>
    Dim fAccountPayableId As PaymentsAccountPayableXpo
    Public Property AccountPayableId() As PaymentsAccountPayableXpo
        Get
            Return fAccountPayableId
        End Get
        Set(ByVal value As PaymentsAccountPayableXpo)
            SetPropertyValue(Of PaymentsAccountPayableXpo)("AccountPayableId", fAccountPayableId, value)
        End Set
    End Property
    Dim fRefundValue As Decimal
    Public Property RefundValue() As Decimal
        Get
            Return fRefundValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("RefundValue", fRefundValue, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
