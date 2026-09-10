Imports DevExpress.Xpo

<Persistent("Authorization.ViewReportAuthorizationOutsourcedServices")>
Public Class ViewReportAuthorizationOutsourcedServicesXpo
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

    Dim fTypeName As String
    Public Property TypeName() As String
        Get
            Return fTypeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("TypeName", fTypeName, value)
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

    Dim fCareGroupCodeName As String
    Public Property CareGroupCodeName() As String
        Get
            Return fCareGroupCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CareGroupCodeName", fCareGroupCodeName, value)
        End Set
    End Property

    Dim fFunctionalUnitCodeName As String
    Public Property FunctionalUnitCodeName() As String
        Get
            Return fFunctionalUnitCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FunctionalUnitCodeName", fFunctionalUnitCodeName, value)
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

#Region "Custom Members"

    <PersistentAlias("Concat(PatientCode, ' - ', PatientName)")>
    Public ReadOnly Property PatientDescription As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("PatientDescription"))
        End Get
    End Property

    <PersistentAlias("ViewReportAuthorizationOutsourcedServicesDetails.Sum(ThirdPartySalesPrice)")>
    Public ReadOnly Property ThirdPartySalesPrice As Decimal
        Get
            Return Convert.ToDecimal(Me.EvaluateAlias("ThirdPartySalesPrice"))
        End Get
    End Property

#End Region

#Region "Navigations Properties"

    <Association("ViewReportAuthorizationOutsourcedServices_References_ViewReportAuthorizationOutsourcedServicesDetails", GetType(ViewReportAuthorizationOutsourcedServicesDetailsXpo))>
    Public ReadOnly Property ViewReportAuthorizationOutsourcedServicesDetails() As XPCollection(Of ViewReportAuthorizationOutsourcedServicesDetailsXpo)
        Get
            Return GetCollection(Of ViewReportAuthorizationOutsourcedServicesDetailsXpo)("ViewReportAuthorizationOutsourcedServicesDetails")
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
