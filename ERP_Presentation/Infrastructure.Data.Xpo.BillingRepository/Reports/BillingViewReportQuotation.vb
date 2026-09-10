Imports DevExpress.Xpo

<Persistent("Billing.ViewReportQuotation")>
Public Class BillingViewReportQuotation
    Inherits XPLiteObject

#Region "Members"

    Dim fId As Integer
    <Key>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
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

    Dim fDocumentDate As DateTime
    Public Property DocumentDate() As DateTime
        Get
            Return fDocumentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DocumentDate", fDocumentDate, value)
        End Set
    End Property

    Dim fQuotationType As Byte
    Public Property QuotationType() As Byte
        Get
            Return fQuotationType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("QuotationType", fQuotationType, value)
        End Set
    End Property

    Dim fQuotationTypeName As String
    Public Property QuotationTypeName() As String
        Get
            Return fQuotationTypeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("QuotationTypeName", fQuotationTypeName, value)
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

    Dim fDescription As String
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property

    Dim fOperatingUnitId As Integer
    Public Property OperatingUnitId() As Integer
        Get
            Return fOperatingUnitId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("OperatingUnitId", fOperatingUnitId, value)
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

    Dim fStatusName As String
    Public Property StatusName() As String
        Get
            Return fStatusName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("StatusName", fStatusName, value)
        End Set
    End Property

    Dim fStatusInAuthorization As Byte
    Public Property StatusInAuthorization() As Byte
        Get
            Return fStatusInAuthorization
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("StatusInAuthorization", fStatusInAuthorization, value)
        End Set
    End Property

    Dim fStatusInBilling As Byte
    Public Property StatusInBilling() As Byte
        Get
            Return fStatusInBilling
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("StatusInBilling", fStatusInBilling, value)
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

    Dim fNit As String
    Public Property Nit() As String
        Get
            Return fNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Nit", fNit, value)
        End Set
    End Property

    Dim fThirdPartyDescription As String
    Public Property ThirdPartyDescription() As String
        Get
            Return fThirdPartyDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ThirdPartyDescription", fThirdPartyDescription, value)
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

    Dim fPatientName As String
    Public Property PatientName() As String
        Get
            Return fPatientName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientName", fPatientName, value)
        End Set
    End Property

    Dim fPatientDescription As String
    Public Property PatientDescription() As String
        Get
            Return fPatientDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientDescription", fPatientDescription, value)
        End Set
    End Property

    Dim fCareGroupCodeName As String
    Public Property CareGroupCodeName() As String
        Get
            Return fCareGroupCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CareGroupCodeName", fCareGroupCodeName, value)
        End Set
    End Property

    <PersistentAlias("BillingViewReportQuotationDetails.Sum(ThirdPartySalesPrice)")>
    Public ReadOnly Property ThirdPartySalesPrice As Decimal
        Get
            Return Convert.ToDecimal(Me.EvaluateAlias("ThirdPartySalesPrice"))
        End Get
    End Property

    Dim fCreationUser As String
    Public Property CreationUser() As String
        Get
            Return fCreationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CreationUser", fCreationUser, value)
        End Set
    End Property

#End Region

#Region "Navigations Properties"

    <Association("BillingViewReportQuotation_References_BillingViewReportQuotationDetails", GetType(BillingViewReportQuotationDetails))>
    Public ReadOnly Property BillingViewReportQuotationDetails() As XPCollection(Of BillingViewReportQuotationDetails)
        Get
            Return GetCollection(Of BillingViewReportQuotationDetails)("BillingViewReportQuotationDetails")
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
