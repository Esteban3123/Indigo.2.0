Imports DevExpress.Xpo

<Persistent("Billing.InvoiceCopay")>
Public Class BillingInvoiceCopayXpo
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

    Dim fBasicBillingId As BasicBillingReportXpo
    <Association("InvoiceCopay_References_BasicBilling")>
    Public Property BasicBillingId() As BasicBillingReportXpo
        Get
            Return fBasicBillingId
        End Get
        Set(ByVal value As BasicBillingReportXpo)
            SetPropertyValue("BasicBillingId", fBasicBillingId, value)
        End Set
    End Property


    Dim fInvoiceId As BillingVReportInvoice
    <Association("InvoiceCopay_References_Invoice")>
    Public Property InvoiceId() As BillingVReportInvoice
        Get
            Return fInvoiceId
        End Get
        Set(ByVal value As BillingVReportInvoice)
            SetPropertyValue(Of BillingVReportInvoice)("InvoiceId", fInvoiceId, value)
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