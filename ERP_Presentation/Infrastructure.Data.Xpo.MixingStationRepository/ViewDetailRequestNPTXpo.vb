'***********************************************************************
' Assembly         : Infrastructure.Data.Xpo.MixingStationRepostory
' Author           : Andres Alarcon
' Created          : 18/01/2023
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports DevExpress.Xpo

<Persistent("MixingStation.ViewDetailRequestNPT")>
Partial Public Class ViewDetailRequestNPTXpo
    Inherits XPLiteObject

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

    Dim fId As Integer
    <Key(True)>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("Id", fId, value)
        End Set
    End Property

    Dim fGroupingCodeDose As String
    Public Property GroupingCodeDose() As String
        Get
            Return fGroupingCodeDose
        End Get
        Set(ByVal value As String)
            SetPropertyValue("GroupingCodeDose", fGroupingCodeDose, value)
        End Set
    End Property

    Dim fRequestMixingStationDetailPatientsId As Integer
    Public Property RequestMixingStationDetailPatientsId() As Integer
        Get
            Return fRequestMixingStationDetailPatientsId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("RequestMixingStationDetailPatientsId", fRequestMixingStationDetailPatientsId, value)
        End Set
    End Property

    Dim fPatientName As String
    Public Property PatientName() As String
        Get
            Return fPatientName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("PatientName", fPatientName, value)
        End Set
    End Property

    Dim fPatientCode As String
    Public Property PatientCode() As String
        Get
            Return fPatientCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue("PatientCode", fPatientCode, value)
        End Set
    End Property

    Dim fBed As String
    Public Property Bed() As String
        Get
            Return fBed
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Bed", fBed, value)
        End Set
    End Property

    Dim fPatientWeight As Integer
    Public Property PatientWeight() As Integer
        Get
            Return fPatientWeight
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("PatientWeight", fPatientWeight, value)
        End Set
    End Property

    Dim fAdministrationRoute As String
    Public Property AdministrationRoute() As String
        Get
            Return fAdministrationRoute
        End Get
        Set(ByVal value As String)
            SetPropertyValue("AdministrationRoute", fAdministrationRoute, value)
        End Set
    End Property

    Dim fAdministrationRouteDescription As String
    Public Property AdministrationRouteDescription() As String
        Get
            Return fAdministrationRouteDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue("AdministrationRouteDescription", fAdministrationRouteDescription, value)
        End Set
    End Property

    Dim fInfusionTime As Integer
    Public Property InfusionTime() As Integer
        Get
            Return fInfusionTime
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("InfusionTime", fInfusionTime, value)
        End Set
    End Property

    Dim fTotalVolume As Decimal
    Public Property TotalVolume() As Decimal
        Get
            Return fTotalVolume
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue("TotalVolume", fTotalVolume, value)
        End Set
    End Property

    Dim fMaxValueVolume As Decimal?
    Public Property MaxValueVolume() As Decimal?
        Get
            Return fMaxValueVolume
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue("MaxValueVolume", fMaxValueVolume, value)
        End Set
    End Property

    Dim fMinValueVolume As Decimal?
    Public Property MinValueVolume() As Decimal?
        Get
            Return fMinValueVolume
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue("MinValueVolume", fMinValueVolume, value)
        End Set
    End Property

    Dim fInfusionVelocity As Decimal
    Public Property InfusionVelocity() As Decimal
        Get
            Return fInfusionVelocity
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue("InfusionVelocity", fInfusionVelocity, value)
        End Set
    End Property

    Dim fWater As Decimal
    Public Property Water() As Decimal
        Get
            Return fWater
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue("Water", fWater, value)
        End Set
    End Property

    Dim fWaterOsmolarity As Decimal
    Public Property WaterOsmolarity() As Decimal
        Get
            Return fWaterOsmolarity
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue("WaterOsmolarity", fWaterOsmolarity, value)
        End Set
    End Property

    Dim fNutritionId As Integer
    Public Property NutritionId() As Integer
        Get
            Return fNutritionId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("NutritionId", fNutritionId, value)
        End Set
    End Property

    Dim fNutritionName As String
    Public Property NutritionName() As String
        Get
            Return fNutritionName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("NutritionName", fNutritionName, value)
        End Set
    End Property

    Dim fProductCode As String
    Public Property ProductCode() As String
        Get
            Return fProductCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ProductCode", fProductCode, value)
        End Set
    End Property

    Dim fDose As Decimal
    Public Property Dose() As Decimal
        Get
            Return fDose
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue("Dose", fDose, value)
        End Set
    End Property

    Dim fVolume As Decimal
    Public Property Volume() As Decimal
        Get
            Return fVolume
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue("Volume", fVolume, value)
        End Set
    End Property

    Dim fMaxValueNutrition As Decimal?
    Public Property MaxValueNutrition() As Decimal?
        Get
            Return fMaxValueNutrition
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue("MaxValueNutrition", fMaxValueNutrition, value)
        End Set
    End Property

    Dim fMinValueNutrition As Decimal?
    Public Property MinValueNutrition() As Decimal?
        Get
            Return fMinValueNutrition
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue("MinValueNutrition", fMinValueNutrition, value)
        End Set
    End Property

    Dim fOsmolarity As Decimal
    Public Property Osmolarity() As Decimal
        Get
            Return fOsmolarity
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue("Osmolarity", fOsmolarity, value)
        End Set
    End Property

    Dim fCampaignDetailId As Integer
    Public Property CampaignDetailId() As Integer
        Get
            Return fCampaignDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("CampaignDetailId", fCampaignDetailId, value)
        End Set
    End Property

    Dim fRequestMixingStationDetailId As Integer
    Public Property RequestMixingStationDetailId() As Integer
        Get
            Return fRequestMixingStationDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("RequestMixingStationDetailId", fRequestMixingStationDetailId, value)
        End Set
    End Property

    Dim fFunctionalUnitCodeName As String
    Public Property FunctionalUnitCodeName() As String
        Get
            Return fFunctionalUnitCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("FunctionalUnitCodeName", fFunctionalUnitCodeName, value)
        End Set
    End Property

    Dim fTypeNutrition As String
    Public Property TypeNutrition() As String
        Get
            Return fTypeNutrition
        End Get
        Set(ByVal value As String)
            SetPropertyValue("TypeNutrition", fTypeNutrition, value)
        End Set
    End Property

    Dim fNumberLotAdequacy As String
    Public Property NumberLotAdequacy() As String
        Get
            Return fNumberLotAdequacy
        End Get
        Set(ByVal value As String)
            SetPropertyValue("NumberLotAdequacy", fNumberLotAdequacy, value)
        End Set
    End Property

    Dim fJustificationPrescription As String
    Public Property JustificationPrescription() As String
        Get
            Return fJustificationPrescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue("JustificationPrescription", fJustificationPrescription, value)
        End Set
    End Property

    Dim fRequest As Decimal?
    Public Property Request() As Decimal?
        Get
            Return fRequest
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue("Request", fRequest, value)
        End Set
    End Property

End Class
