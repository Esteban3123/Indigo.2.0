'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Andrea Coqueco
' Created          : 2024-02-02
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports DevExpress.Xpo

<Persistent("MixingStation.ViewParenteralNutritionLabel")>
Partial Public Class ViewParenteralNutritionLabelXpo
    Inherits XPLiteObject

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

    ''' <summary>
    ''' Key única - Corresponde a RequestPackageDetailStatusId en la vista SQL
    ''' </summary>
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

    Dim fCampaignDetailId As Integer
    Public Property CampaignDetailId() As Integer
        Get
            Return fCampaignDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CampaignDetailId", fCampaignDetailId, value)
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

    ''' <summary>
    ''' Id de la prescripción de nutrición parenteral (HCNUTPAREC.ID) - Usado para la relación con vistas hijas
    ''' </summary>
    Dim fParenteralNutritionId As Integer
    Public Property ParenteralNutritionId() As Integer
        Get
            Return fParenteralNutritionId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ParenteralNutritionId", fParenteralNutritionId, value)
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

    Dim fPatientDocumentType As String
    Public Property PatientDocumentType() As String
        Get
            Return fPatientDocumentType
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientDocumentType", fPatientDocumentType, value)
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

    Dim fPatientAge As String
    Public Property PatientAge() As String
        Get
            Return fPatientAge
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientAge", fPatientAge, value)
        End Set
    End Property

    Dim fPatientWeight As String
    Public Property PatientWeight() As String
        Get
            Return fPatientWeight
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientWeight", fPatientWeight, value)
        End Set
    End Property

    Dim fPatientLocation As String
    Public Property PatientLocation() As String
        Get
            Return fPatientLocation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientLocation", fPatientLocation, value)
        End Set
    End Property

    Dim fPatientBed As String
    Public Property PatientBed() As String
        Get
            Return fPatientBed
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientBed", fPatientBed, value)
        End Set
    End Property

    '--------Parámetros físico-químicos

    Dim fBatchCode As String
    Public Property BatchCode() As String
        Get
            Return fBatchCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BatchCode", fBatchCode, value)
        End Set
    End Property

    Dim fAdministrationRouteDescription As String
    Public Property AdministrationRouteDescription() As String
        Get
            Return fAdministrationRouteDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AdministrationRouteDescription", fAdministrationRouteDescription, value)
        End Set
    End Property

    Dim fInfusionTime As Integer
    Public Property InfusionTime() As Integer
        Get
            Return fInfusionTime
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("InfusionTime", fInfusionTime, value)
        End Set
    End Property

    Dim fTotalVolume As Decimal
    Public Property TotalVolume() As Decimal
        Get
            Return fTotalVolume
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalVolume", fTotalVolume, value)
        End Set
    End Property

    Dim fInfusionVelocity As Decimal
    Public Property InfusionVelocity() As Decimal
        Get
            Return fInfusionVelocity
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("InfusionVelocity", fInfusionVelocity, value)
        End Set
    End Property

    '-------- Datos de preparación

    Dim fProcessingDate As Date?
    Public Property ProcessingDate() As Date?
        Get
            Return fProcessingDate
        End Get
        Set(ByVal value As Date?)
            SetPropertyValue(Of Date?)("ProcessingDate", fProcessingDate, value)
        End Set
    End Property

    Dim fFinishDate As Date?
    Public Property FinishDate() As Date?
        Get
            Return fFinishDate
        End Get
        Set(ByVal value As Date?)
            SetPropertyValue(Of Date?)("FinishDate", fFinishDate, value)
        End Set
    End Property

    Dim fQualityChemical As String
    Public Property QualityChemical() As String
        Get
            Return fQualityChemical
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("QualityChemical", fQualityChemical, value)
        End Set
    End Property

    Dim fProductionChemical As String
    Public Property ProductionChemical() As String
        Get
            Return fProductionChemical
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductionChemical", fProductionChemical, value)
        End Set
    End Property

    Dim fRecommendations As String
    Public Property Recommendations() As String
        Get
            Return fRecommendations
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Recommendations", fRecommendations, value)
        End Set
    End Property

    Dim fParenteralNutritionName As String
    Public Property ParenteralNutritionName() As String
        Get
            Return fParenteralNutritionName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ParenteralNutritionName", fParenteralNutritionName, value)
        End Set
    End Property

    Dim fInfusionType As String
    Public Property InfusionType() As String
        Get
            Return fInfusionType
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InfusionType", fInfusionType, value)
        End Set
    End Property

    Dim fStorageName As String
    Public Property StorageName() As String
        Get
            Return fStorageName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("StorageName", fStorageName, value)
        End Set
    End Property

    Dim fPhotoProtection As String
    Public Property PhotoProtection() As String
        Get
            Return fPhotoProtection
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PhotoProtection", fPhotoProtection, value)
        End Set
    End Property

#Region "Navigations Properties"

    ''' <summary>
    ''' Componentes de la nutrición parenteral relacionados por ParenteralNutritionId
    ''' </summary>
    Public ReadOnly Property ViewParenteralNutritionLabelComponentsXpo() As XPCollection(Of ViewParenteralNutritionLabelComponentsXpo)
        Get
            Return New XPCollection(Of ViewParenteralNutritionLabelComponentsXpo)(Session, DevExpress.Data.Filtering.CriteriaOperator.Parse("ParenteralNutritionId = ?", ParenteralNutritionId))
        End Get
    End Property

    ''' <summary>
    ''' Parámetros farmacéuticos de la nutrición parenteral relacionados por ParenteralNutritionId
    ''' </summary>
    Public ReadOnly Property ViewParenteralNutritionLabelPharmaceuticalParametersXpo() As XPCollection(Of ViewParenteralNutritionLabelPharmaceuticalParametersXpo)
        Get
            Return New XPCollection(Of ViewParenteralNutritionLabelPharmaceuticalParametersXpo)(Session, DevExpress.Data.Filtering.CriteriaOperator.Parse("ParenteralNutritionId = ?", ParenteralNutritionId))
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
#End Region

End Class