Imports DevExpress.Xpo

<Persistent("Billing.VReportInvoiceCustomerRetention")>
Public Class BillingVReportInvoiceCustomerRetention
    Inherits XPLiteObject

#Region "Properties"

    Dim fId As String
    <Key>
    Public Property Id() As String
        Get
            Return fId
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Id", fId, value)
        End Set
    End Property

    Dim fInvoiceId As BillingVReportInvoice
    <Association("VReportInvoice_References_VReportInvoiceCustomerRetention")>
    Public Property InvoiceId() As BillingVReportInvoice
        Get
            Return fInvoiceId
        End Get
        Set(ByVal value As BillingVReportInvoice)
            SetPropertyValue(Of BillingVReportInvoice)("InvoiceId", fInvoiceId, value)
        End Set
    End Property

    Dim fCalculateTaxAdvance As Integer
    Public Property CalculateTaxAdvance() As Integer
        Get
            Return fCalculateTaxAdvance
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CalculateTaxAdvance", fCalculateTaxAdvance, value)
        End Set
    End Property

    Dim fPortfolioNoteConcept As String
    Public Property PortfolioNoteConcept() As String
        Get
            Return fPortfolioNoteConcept
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PortfolioNoteConcept", fPortfolioNoteConcept, value)
        End Set
    End Property

    Dim fRetentionConcept As String
    Public Property RetentionConcept() As String
        Get
            Return fRetentionConcept
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RetentionConcept", fRetentionConcept, value)
        End Set
    End Property

    Dim fRetentionTypeName As String
    Public Property RetentionTypeName() As String
        Get
            Return fRetentionTypeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RetentionTypeName", fRetentionTypeName, value)
        End Set
    End Property

    Dim fRetentionRate As Decimal
    Public Property RetentionRate() As Decimal
        Get
            Return fRetentionRate
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("RetentionRate", fRetentionRate, value)
        End Set
    End Property

    Dim fBaseValue As Decimal
    Public Property BaseValue() As Decimal
        Get
            Return fBaseValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("BaseValue", fBaseValue, value)
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
