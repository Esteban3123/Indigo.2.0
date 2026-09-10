Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Treasury.CashReceiptAccountReceivable")>
Public Class TreasuryCashReceiptAccountReceivableXpo
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
    <Association("TreasuryCashReceiptAccountReceivableXpoReferencesTreasuryCashReceiptDetailsXpo")>
    Public Property CashReceiptDetailId() As TreasuryCashReceiptDetailsXpo
        Get
            Return fCashReceiptDetailId
        End Get
        Set(ByVal value As TreasuryCashReceiptDetailsXpo)
            SetPropertyValue(Of TreasuryCashReceiptDetailsXpo)("CashReceiptDetailId", fCashReceiptDetailId, value)
        End Set
    End Property
    Dim fAccountReceivableId As PortfolioAccountReceivableReportXpo
    <Association("TreasuryCashReceiptAccountReceivableXpoReferencesPortfolioAccountReceivableReportXpo")>
    Public Property AccountReceivableId() As PortfolioAccountReceivableReportXpo
        Get
            Return fAccountReceivableId
        End Get
        Set(ByVal value As PortfolioAccountReceivableReportXpo)
            SetPropertyValue(Of PortfolioAccountReceivableReportXpo)("AccountReceivableId", fAccountReceivableId, value)
        End Set
    End Property
    Dim fInvoiceNumber As String
    <Size(20)>
    Public Property InvoiceNumber() As String
        Get
            Return fInvoiceNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InvoiceNumber", fInvoiceNumber, value)
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

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
