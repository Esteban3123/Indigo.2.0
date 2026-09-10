Imports DevExpress.Xpo

<Persistent("Billing.ViewBillingNoteInvoice")>
Public Class ViewBillingNoteInvoiceXpo
    Inherits XPLiteObject

#Region "Properties"

    Dim fId As Integer
    <Key()>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fBillingNoteId As ViewBillingNoteXpo
    <Association("ViewBillingNote_References_ViewBillingNoteInvoice")>
    Public Property BillingNoteId() As ViewBillingNoteXpo
        Get
            Return fBillingNoteId
        End Get
        Set(ByVal value As ViewBillingNoteXpo)
            SetPropertyValue(Of ViewBillingNoteXpo)("BillingNoteId", fBillingNoteId, value)
        End Set
    End Property

    Dim fInvoiceNumber As String
    Public Property InvoiceNumber() As String
        Get
            Return fInvoiceNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InvoiceNumber", fInvoiceNumber, value)
        End Set
    End Property

    Dim fDocumentDate As DateTime
    Public Property DocumentDate() As DateTime
        Get
            Return fDocumentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DocumentDate", fDocumentDate, value)
        End Set
    End Property

    Dim fBillingValue As Decimal
    Public Property BillingValue() As Decimal
        Get
            Return fBillingValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("BillingValue", fBillingValue, value)
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

    Dim fTaxValue As Decimal
    Public Property TaxValue() As Decimal
        Get
            Return fTaxValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TaxValue", fTaxValue, value)
        End Set
    End Property

    Dim fAccountNumberName As String
    Public Property AccountNumberName() As String
        Get
            Return fAccountNumberName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AccountNumberName", fAccountNumberName, value)
        End Set
    End Property

    Dim fInvoiceDate As DateTime
    Public Property InvoiceDate() As DateTime
        Get
            Return fInvoiceDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("InvoiceDate", fInvoiceDate, value)
        End Set
    End Property

    Dim fInvoiceExpirationDate As DateTime
    Public Property InvoiceExpirationDate() As DateTime
        Get
            Return fInvoiceExpirationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("InvoiceExpirationDate", fInvoiceExpirationDate, value)
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
