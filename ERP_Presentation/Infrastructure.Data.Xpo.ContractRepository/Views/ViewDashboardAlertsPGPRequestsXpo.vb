#Region "Imports"

Imports DevExpress.Xpo

#End Region

<Persistent("Contract.ViewDashboardAlertsPGPRequests")>
Public Class ViewDashboardAlertsPGPRequestsXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fRowId As String
    <Key(True)>
    Public Property RowId() As String
        Get
            Return fRowId
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RowId", fRowId, value)
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

    Dim fGroupDescription As String
    Public Property GroupDescription() As String
        Get
            Return fGroupDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("GroupDescription", fGroupDescription, value)
        End Set
    End Property

    Dim fUserMin As Integer
    Public Property UserMin() As Integer
        Get
            Return fUserMin
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("UserMin", fUserMin, value)
        End Set
    End Property

    Dim fUserMax As Integer
    Public Property UserMax() As Integer
        Get
            Return fUserMax
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("UserMax", fUserMax, value)
        End Set
    End Property

    Dim fTotalContract As Integer
    Public Property TotalContract() As Decimal
        Get
            Return fTotalContract
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalContract", fTotalContract, value)
        End Set
    End Property

    Dim fRequestDate As DateTime
    Public Property RequestDate() As DateTime
        Get
            Return fRequestDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("RequestDate", fRequestDate, value)
        End Set
    End Property

    Dim fPatient As String
    Public Property Patient() As String
        Get
            Return fPatient
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Patient", fPatient, value)
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

    Dim fFolio As String
    Public Property Folio() As String
        Get
            Return fFolio
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Folio", fFolio, value)
        End Set
    End Property

    Dim fCareCenter As String
    Public Property CareCenter() As String
        Get
            Return fCareCenter
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CareCenter", fCareCenter, value)
        End Set
    End Property

    Dim fFunctionalUnit As String
    Public Property FunctionalUnit() As String
        Get
            Return fFunctionalUnit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FunctionalUnit", fFunctionalUnit, value)
        End Set
    End Property

    Dim fProfessional As String
    Public Property Professional() As String
        Get
            Return fProfessional
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Professional", fProfessional, value)
        End Set
    End Property

    Dim fDiagnostic As String
    Public Property Diagnostic() As String
        Get
            Return fDiagnostic
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Diagnostic", fDiagnostic, value)
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

    Dim fCUPSEntityCodeName As String
    Public Property CUPSEntityCodeName() As String
        Get
            Return fCUPSEntityCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CUPSEntityCodeName", fCUPSEntityCodeName, value)
        End Set
    End Property

    Dim fDescriptionCodeName As String
    Public Property DescriptionCodeName() As String
        Get
            Return fDescriptionCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DescriptionCodeName", fDescriptionCodeName, value)
        End Set
    End Property

    Dim fQuantity As Integer
    Public Property Quantity() As Integer
        Get
            Return fQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Quantity", fQuantity, value)
        End Set
    End Property

    Dim fCMEValue As Integer
    Public Property CMEValue() As Decimal
        Get
            Return fCMEValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CMEValue", fCMEValue, value)
        End Set
    End Property

    Dim fTotalCME As Integer
    Public Property TotalCME() As Decimal
        Get
            Return fTotalCME
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalCME", fTotalCME, value)
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
