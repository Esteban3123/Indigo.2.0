Imports DevExpress.Xpo

<Persistent("Authorization.ViewAuthorizationOutsourcedServicesAdmissionInformation")>
Public Class ViewAuthorizationOutsourcedServicesAdmissionInformationXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As String
    <Key(True)>
    Public Property Id() As String
        Get
            Return fId
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Id", fId, value)
        End Set
    End Property

    Dim fAuthorizationOutsourcedServicesId As AuthorizationOutsourcedServicesXpo
    <Association("BillingViewAdmissionInformation_References_AuthorizationAuthorizationOutsourcedServices")>
    Public Property AuthorizationOutsourcedServicesId() As AuthorizationOutsourcedServicesXpo
        Get
            Return fAuthorizationOutsourcedServicesId
        End Get
        Set(ByVal value As AuthorizationOutsourcedServicesXpo)
            SetPropertyValue(Of AuthorizationOutsourcedServicesXpo)("AuthorizationOutsourcedServicesId", fAuthorizationOutsourcedServicesId, value)
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

    Dim fPatientCodeName As String
    Public Property PatientCodeName() As String
        Get
            Return fPatientCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientCodeName", fPatientCodeName, value)
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