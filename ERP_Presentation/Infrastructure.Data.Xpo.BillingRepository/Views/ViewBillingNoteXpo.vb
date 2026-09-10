Imports DevExpress.Xpo

<Persistent("Billing.ViewBillingNote")>
Public Class ViewBillingNoteXpo
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

    Dim fNoteTypeName As String
    Public Property NoteTypeName() As String
        Get
            Return fNoteTypeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NoteTypeName", fNoteTypeName, value)
        End Set
    End Property

    Dim fCode As String
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fNoteDate As DateTime
    Public Property NoteDate() As DateTime
        Get
            Return fNoteDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("NoteDate", fNoteDate, value)
        End Set
    End Property

    Dim fObservations As String
    Public Property Observations() As String
        Get
            Return fObservations
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Observations", fObservations, value)
        End Set
    End Property

    Dim fCustomerPartyNit As String
    Public Property CustomerPartyNit() As String
        Get
            Return fCustomerPartyNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CustomerPartyNit", fCustomerPartyNit, value)
        End Set
    End Property

    Dim fCustomerPartyName As String
    Public Property CustomerPartyName() As String
        Get
            Return fCustomerPartyName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CustomerPartyName", fCustomerPartyName, value)
        End Set
    End Property

    Dim fCustomerAddress As String
    Public Property CustomerAddress() As String
        Get
            Return fCustomerAddress
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CustomerAddress", fCustomerAddress, value)
        End Set
    End Property

    Dim fCustomerCityName As String
    Public Property CustomerCityName() As String
        Get
            Return fCustomerCityName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CustomerCityName", fCustomerCityName, value)
        End Set
    End Property

    Dim fCUDE As String
    Public Property CUDE() As String
        Get
            Return fCUDE
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CUDE", fCUDE, value)
        End Set
    End Property

    Dim fQR As String
    Public Property QR() As String
        Get
            Return fQR
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("QR", fQR, value)
        End Set
    End Property

    Dim fStatusDIAN As String
    Public Property StatusDIAN() As String
        Get
            Return fStatusDIAN
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("StatusDIAN", fStatusDIAN, value)
        End Set
    End Property

    Dim fValidationDate As DateTime?
    Public Property ValidationDate() As DateTime?
        Get
            Return fValidationDate
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("ValidationDate", fValidationDate, value)
        End Set
    End Property

    Dim fNoteType As Byte?
    Public Property NoteType() As Byte?
        Get
            Return fNoteType
        End Get
        Set(ByVal value As Byte?)
            SetPropertyValue(Of Byte?)("NoteType", fNoteType, value)
        End Set
    End Property

    Dim fPortfolioNoteEntityName As String
    Public Property PortfolioNoteEntityName() As String
        Get
            Return fPortfolioNoteEntityName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PortfolioNoteEntityName", fPortfolioNoteEntityName, value)
        End Set
    End Property

#End Region

#Region "Navigations Properties"

    <Association("ViewBillingNote_References_ViewBillingNoteInvoice", GetType(ViewBillingNoteInvoiceXpo))>
    Public ReadOnly Property BillingViewBillingNoteInvoices() As XPCollection(Of ViewBillingNoteInvoiceXpo)
        Get
            Return GetCollection(Of ViewBillingNoteInvoiceXpo)("BillingViewBillingNoteInvoices")
        End Get
    End Property

    <Association("ViewBillingNote_References_ViewReportBillingNoteWithDetails", GetType(ViewReportBillingNoteWithDetailsXpo))>
    Public ReadOnly Property ViewReportBillingNoteWithDetailsXpo() As XPCollection(Of ViewReportBillingNoteWithDetailsXpo)
        Get
            Return GetCollection(Of ViewReportBillingNoteWithDetailsXpo)("ViewReportBillingNoteWithDetailsXpo")
        End Get
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
