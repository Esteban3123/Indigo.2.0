#Region "Imports"

Imports DevExpress.Xpo

#End Region

<Persistent("Billing.InvoiceEntityCapitatedDistributionDetail")> _
Partial Public Class InvoiceEntityCapitatedDistributionDetailReportXpo
    Inherits XPLiteObject

#Region "Members"

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

    Dim fInvoiceEntityCapitatedDistributionId As InvoiceEntityCapitatedDistributionReportXpo
    <Association("Billing_InvoiceEntityCapitatedDistributionDetail_References_Billing_InvoiceEntityCapitatedDistribution")> _
    Public Property InvoiceEntityCapitatedDistributionId() As InvoiceEntityCapitatedDistributionReportXpo
        Get
            Return fInvoiceEntityCapitatedDistributionId
        End Get
        Set(ByVal value As InvoiceEntityCapitatedDistributionReportXpo)
            SetPropertyValue(Of InvoiceEntityCapitatedDistributionReportXpo)("InvoiceEntityCapitatedDistributionId", fInvoiceEntityCapitatedDistributionId, value)
        End Set
    End Property

    Dim fInvoiceNumber As String
    <Size(15)> _
    Public Property InvoiceNumber() As String
        Get
            Return fInvoiceNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InvoiceNumber", fInvoiceNumber, value)
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

    Dim fInvoiceValue As Decimal
    Public Property InvoiceValue() As Decimal
        Get
            Return fInvoiceValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("InvoiceValue", fInvoiceValue, value)
        End Set
    End Property

    Dim fHealthAdministratorId As AdministratorHealthXpo
    <Association("Billing_InvoiceEntityCapitatedDistributionDetailReferencesContract_HealthAdministrator")> _
    Public Property HealthAdministratorId() As AdministratorHealthXpo
        Get
            Return fHealthAdministratorId
        End Get
        Set(ByVal value As AdministratorHealthXpo)
            SetPropertyValue(Of AdministratorHealthXpo)("HealthAdministratorId", fHealthAdministratorId, value)
        End Set
    End Property

    Dim fInvoiceCategoryId As InvoiceCategoriesReportXpo
    <Association("Billing_InvoiceEntityCapitatedDistributionDetailReferencesBilling_InvoiceCategories")> _
    Public Property InvoiceCategoryId() As InvoiceCategoriesReportXpo
        Get
            Return fInvoiceCategoryId
        End Get
        Set(ByVal value As InvoiceCategoriesReportXpo)
            SetPropertyValue(Of InvoiceCategoriesReportXpo)("InvoiceCategoryId", fInvoiceCategoryId, value)
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

#End Region

End Class