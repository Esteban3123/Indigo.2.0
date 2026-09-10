Imports DevExpress.Xpo

<Persistent("Billing.VReportListInvoice")>
Public Class BillingVReportListInvoice
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

    Dim fInvoiceId As Integer
    Public Property InvoiceId() As Integer
        Get
            Return fInvoiceId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("InvoiceId", fInvoiceId, value)
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

    Dim fInvoiceNumber As String
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

    Dim fObservation As String
    Public Property Observation() As String
        Get
            Return fObservation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Observation", fObservation, value)
        End Set
    End Property

    Dim fThirdPartyNit As String
    Public Property ThirdPartyNit() As String
        Get
            Return fThirdPartyNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ThirdPartyNit", fThirdPartyNit, value)
        End Set
    End Property

    Dim fThirdPartyName As String
    Public Property ThirdPartyName() As String
        Get
            Return fThirdPartyName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ThirdPartyName", fThirdPartyName, value)
        End Set
    End Property

    Dim fCityName As String
    Public Property CityName() As String
        Get
            Return fCityName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CityName", fCityName, value)
        End Set
    End Property

    Dim fSubtotal As Decimal
    Public Property Subtotal() As Decimal
        Get
            Return fSubtotal
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Subtotal", fSubtotal, value)
        End Set
    End Property

    Dim fDiscountValue As Decimal
    Public Property DiscountValue() As Decimal
        Get
            Return fDiscountValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DiscountValue", fDiscountValue, value)
        End Set
    End Property

    Dim fPatientValue As Decimal
    Public Property PatientValue() As Decimal
        Get
            Return fPatientValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PatientValue", fPatientValue, value)
        End Set
    End Property

    Dim fValueTax As Decimal
    Public Property ValueTax() As Decimal
        Get
            Return fValueTax
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueTax", fValueTax, value)
        End Set
    End Property

    Dim fTotalValue As Decimal
    Public Property TotalValue() As Decimal
        Get
            Return fTotalValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalValue", fTotalValue, value)
        End Set
    End Property

    Dim fInvoicedUser As String
    Public Property InvoicedUser() As String
        Get
            Return fInvoicedUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InvoicedUser", fInvoicedUser, value)
        End Set
    End Property

    Dim fStatusName As String
    Public Property StatusName() As String
        Get
            Return fStatusName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("StatusName", fStatusName, value)
        End Set
    End Property

    Dim fCurrencyId As Integer
    Public Property CurrencyId() As Integer
        Get
            Return fCurrencyId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CurrencyId", fCurrencyId, value)
        End Set
    End Property

    Dim fIsMasterAccount As Integer
    Public Property IsMasterAccount() As Integer
        Get
            Return fIsMasterAccount
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IsMasterAccount", fIsMasterAccount, value)
        End Set
    End Property

    Dim fAbbreviation As String
    Public Property Abbreviation() As String
        Get
            Return fAbbreviation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Abbreviation", fAbbreviation, value)
        End Set
    End Property

    Dim fCUV As String
    Public Property CUV() As String
        Get
            Return fCUV
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CUV", fCUV, value)
        End Set
    End Property

#Region "Filters"

    Dim fDocumentType As Byte
    Public Property DocumentType() As Byte
        Get
            Return fDocumentType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("DocumentType", fDocumentType, value)
        End Set
    End Property

    Dim fCareGroupId As Integer
    Public Property CareGroupId() As Integer
        Get
            Return fCareGroupId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CareGroupId", fCareGroupId, value)
        End Set
    End Property

    Dim fHealthAdministratorId As Integer
    Public Property HealthAdministratorId() As Integer
        Get
            Return fHealthAdministratorId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("HealthAdministratorId", fHealthAdministratorId, value)
        End Set
    End Property

    Dim fInvoiceCategoryId As Integer
    Public Property InvoiceCategoryId() As Integer
        Get
            Return fInvoiceCategoryId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("InvoiceCategoryId", fInvoiceCategoryId, value)
        End Set
    End Property

    Dim fAdmissionNumber As String
    Public Property AdmissionNumber() As String
        Get
            Return fAdmissionNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AdmissionNumber", fAdmissionNumber, value)
        End Set
    End Property

    Dim fPatientCode As String
    Public Property PatientCode() As String
        Get
            Return fPatientCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientCode", fPatientCode, value)
        End Set
    End Property

    Dim fThirdPartyId As Integer
    Public Property ThirdPartyId() As Integer
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property

    Dim fSucursalId As Integer
    Public Property SucursalId() As Integer
        Get
            Return fSucursalId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("SucursalId", fSucursalId, value)
        End Set
    End Property

    Dim fRadicateInvoiceId As Integer
    Public Property RadicateInvoiceId() As Integer
        Get
            Return fRadicateInvoiceId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RadicateInvoiceId", fRadicateInvoiceId, value)
        End Set
    End Property

    Dim fStatus As Byte
    Public Property Status() As Byte
        Get
            Return fStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Status", fStatus, value)
        End Set
    End Property

#End Region

#End Region

#Region "Custom Members"

    <PersistentAlias("Iif(
DocumentType = 1, 'Factura EAPB con Contrato', 
DocumentType = 2, 'Factura EAPB sin Contrato',
DocumentType = 3, 'Factura Particular', 
DocumentType = 4, 'Factura Capitada', 
DocumentType = 5, 'Control de Capitación', 
DocumentType = 6, 'Factura Básica', 
DocumentType = 7, 'Factura de Venta de Productos', 
'')")>
    Public ReadOnly Property DocumentTypeName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("DocumentTypeName"))
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
