Imports DevExpress.Xpo

<Persistent("Authorization.AuthorizationOutsourcedServices")>
Public Class AuthorizationOutsourcedServicesXpo
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

    Dim fType As Integer
    Public Property Type() As Integer
        Get
            Return fType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Type", fType, value)
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

    Dim fThirdPartyId As ThirdPartyXpo
    <Association("AuthorizationOutsourcedServicesReferencesCommon_ThirdParty")>
    Public Property ThirdPartyId() As ThirdPartyXpo
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As ThirdPartyXpo)
            SetPropertyValue(Of ThirdPartyXpo)("ThirdPartyId", fThirdPartyId, value)
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

#End Region

#Region "Custom Members"

    <PersistentAlias("Iif(Status = 1, 'Registrado', Iif(Status = 2, 'Confirmado',Iif(Status = 3, 'Anulado', '')))")>
    Public ReadOnly Property StatusName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

    <PersistentAlias("Iif(Type = 1, 'Intrahospitalario', 'Ambulatoria')")>
    Public ReadOnly Property TypeName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("TypeName"))
        End Get
    End Property

    <PersistentAlias("AdmissionInformations[].Single()")>
    Public ReadOnly Property AdmissionInformation() As ViewAuthorizationOutsourcedServicesAdmissionInformationXpo
        Get
            Return CType(Me.EvaluateAlias("AdmissionInformation"), ViewAuthorizationOutsourcedServicesAdmissionInformationXpo)
        End Get
    End Property

#End Region

#Region "Navigation"

    <Aggregated, Association("BillingViewAdmissionInformation_References_AuthorizationAuthorizationOutsourcedServices")>
    Public ReadOnly Property AdmissionInformations() As XPCollection(Of ViewAuthorizationOutsourcedServicesAdmissionInformationXpo)
        Get
            Return GetCollection(Of ViewAuthorizationOutsourcedServicesAdmissionInformationXpo)("AdmissionInformations")
        End Get
    End Property

    <Association("AuthorizationOutsourcedServicesServiceOrderDetailReferencesAuthorizationOutsourcedServices", GetType(AuthorizationOutsourcedServicesServiceOrderDetailXpo))>
    Public ReadOnly Property AuthorizationOutsourcedServicesServiceOrderDetailXpo() As XPCollection(Of AuthorizationOutsourcedServicesServiceOrderDetailXpo)
        Get
            Return GetCollection(Of AuthorizationOutsourcedServicesServiceOrderDetailXpo)("AuthorizationOutsourcedServicesServiceOrderDetailXpo")
        End Get
    End Property

    <Association("AuthorizationOutsourcedServicesPharmaceuticalDispensingDetailReferencesAuthorizationOutsourcedServices", GetType(AuthorizationOutsourcedServicesPharmaceuticalDispensingDetailXpo))>
    Public ReadOnly Property AuthorizationOutsourcedServicesPharmaceuticalDispensingDetailXpo() As XPCollection(Of AuthorizationOutsourcedServicesPharmaceuticalDispensingDetailXpo)
        Get
            Return GetCollection(Of AuthorizationOutsourcedServicesPharmaceuticalDispensingDetailXpo)("AuthorizationOutsourcedServicesPharmaceuticalDispensingDetailXpo")
        End Get
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

#End Region

End Class