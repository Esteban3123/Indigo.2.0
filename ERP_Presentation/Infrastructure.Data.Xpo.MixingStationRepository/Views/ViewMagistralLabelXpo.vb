'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Andrea Coqueco
' Created          : 2025-05-05
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports DevExpress.Xpo

<Persistent("MixingStation.ViewMagistralLabel")>
Public Class ViewMagistralLabelXpo
    Inherits XPLiteObject

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

    Dim fRequestPackageDetailStatusId As Integer
    Public Property RequestPackageDetailStatusId() As Integer
        Get
            Return fRequestPackageDetailStatusId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RequestPackageDetailStatusId", fRequestPackageDetailStatusId, value)
        End Set
    End Property

    Dim fCampaignDetailId As Integer
    Public Property CampaignDetailId() As Integer
        Get
            Return fCampaignDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CampaignDetailId", fCampaignDetailId, value)
        End Set
    End Property

    Dim fPackageName As String
    Public Property PackageName() As String
        Get
            Return fPackageName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PackageName", fPackageName, value)
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

    Dim fPatientCode As String
    Public Property PatientCode() As String
        Get
            Return fPatientCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientCode", fPatientCode, value)
        End Set
    End Property

    Dim fBed As String
    Public Property Bed() As String
        Get
            Return fBed
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Bed", fBed, value)
        End Set
    End Property

    Dim fIndications As String
    Public Property Indications() As String
        Get
            Return fIndications
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Indications", fIndications, value)
        End Set
    End Property

    Dim fProcessingDate As Date?
    Public Property ProcessingDate() As Date?
        Get
            Return fProcessingDate
        End Get
        Set(ByVal value As Date?)
            SetPropertyValue(Of Date?)("ProcessingDate", fProcessingDate, value)
        End Set
    End Property

    Dim fExpirationDate As Date?
    Public Property ExpirationDate() As Date?
        Get
            Return fExpirationDate
        End Get
        Set(ByVal value As Date?)
            SetPropertyValue(Of Date?)("ExpirationDate", fExpirationDate, value)
        End Set
    End Property

    Dim fInternalBatchCode As String
    Public Property InternalBatchCode() As String
        Get
            Return fInternalBatchCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InternalBatchCode", fInternalBatchCode, value)
        End Set
    End Property

    Dim fAdministrationRoute As String
    Public Property AdministrationRoute() As String
        Get
            Return fAdministrationRoute
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AdministrationRoute", fAdministrationRoute, value)
        End Set
    End Property

    Dim fStorageConditions As String
    Public Property StorageConditions() As String
        Get
            Return fStorageConditions
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("StorageConditions", fStorageConditions, value)
        End Set
    End Property

    Dim fProductionChemist As String
    Public Property ProductionChemist() As String
        Get
            Return fProductionChemist
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductionChemist", fProductionChemist, value)
        End Set
    End Property

    Dim fSupervisor As String
    Public Property Supervisor() As String
        Get
            Return fSupervisor
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Supervisor", fSupervisor, value)
        End Set
    End Property

    Dim fContent As String
    Public Property Content() As String
        Get
            Return fContent
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Content", fContent, value)
        End Set
    End Property

    Dim fSpecialConsiderations As String
    Public Property SpecialConsiderations() As String
        Get
            Return fSpecialConsiderations
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SpecialConsiderations", fSpecialConsiderations, value)
        End Set
    End Property

#Region "Navigations Properties"

    <Association("ViewMagistralLabelXpo_References_ViewMagistralLabelDetailsXpo", GetType(ViewMagistralLabelDetailsXpo))>
    Public ReadOnly Property MagistralLabelDetails() As XPCollection(Of ViewMagistralLabelDetailsXpo)
        Get
            Return GetCollection(Of ViewMagistralLabelDetailsXpo)("MagistralLabelDetails")
        End Get
    End Property

    <NonPersistent>
    Public Property MainMedicines As List(Of MagistralLabels)

    <NonPersistent>
    Public Property Vehicles As List(Of MagistralLabels)

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

Public Class MagistralLabels
    Public Property Component1 As String
    Public Property Quantity1 As String
    Public Property Component2 As String
    Public Property Quantity2 As String
End Class