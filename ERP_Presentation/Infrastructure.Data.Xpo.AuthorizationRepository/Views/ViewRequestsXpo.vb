Imports DevExpress.Xpo

<Persistent("Authorization.ViewRequests")>
Public Class ViewRequestsXpo
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

    Dim fEntityName As String
    Public Property EntityName() As String
        Get
            Return fEntityName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EntityName", fEntityName, value)
        End Set
    End Property

    Dim fEntityId As Integer
    Public Property EntityId() As Integer
        Get
            Return fEntityId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EntityId", fEntityId, value)
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

    Dim fCareGroup As String
    Public Property CareGroup() As String
        Get
            Return fCareGroup
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CareGroup", fCareGroup, value)
        End Set
    End Property

    Dim fHealthAdministrator As String
    Public Property HealthAdministrator() As String
        Get
            Return fHealthAdministrator
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("HealthAdministrator", fHealthAdministrator, value)
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

    Dim fProfessional As String
    Public Property Professional() As String
        Get
            Return fProfessional
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Professional", fProfessional, value)
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

    Dim fItemCodeOriginal As String
    Public Property ItemCodeOriginal() As String
        Get
            Return fItemCodeOriginal
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ItemCodeOriginal", fItemCodeOriginal, value)
        End Set
    End Property

    Dim fItemCodeName As String
    Public Property ItemCodeName() As String
        Get
            Return fItemCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ItemCodeName", fItemCodeName, value)
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

    Dim fFinancedResourceUPC As Boolean
    Public Property FinancedResourceUPC() As Boolean
        Get
            Return fFinancedResourceUPC
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("FinancedResourceUPC", fFinancedResourceUPC, value)
        End Set
    End Property

    Dim fCovered As Boolean
    Public Property Covered() As Boolean
        Get
            Return fCovered
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Covered", fCovered, value)
        End Set
    End Property

    Dim fContracted As Boolean
    Public Property Contracted() As Boolean
        Get
            Return fContracted
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Contracted", fContracted, value)
        End Set
    End Property

    Dim fQuoted As Boolean
    Public Property Quoted() As Boolean
        Get
            Return fQuoted
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Quoted", fQuoted, value)
        End Set
    End Property

    Dim fAuthorized As Boolean
    Public Property Authorized() As Boolean
        Get
            Return fAuthorized
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Authorized", fAuthorized, value)
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
