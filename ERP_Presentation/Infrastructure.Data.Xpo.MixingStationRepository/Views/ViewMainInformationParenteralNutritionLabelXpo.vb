'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Andrea Coqueco
' Created          : 2025-05-11
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports DevExpress.Xpo

<Persistent("MixingStation.ViewMainInformationParenteralNutritionLabel")>
Public Class ViewMainInformationParenteralNutritionLabelXpo
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

    Dim fCampaignDetailId As Integer
    Public Property CampaignDetailId() As Integer
        Get
            Return fCampaignDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CampaignDetailId", fCampaignDetailId, value)
        End Set
    End Property

    Dim fInstitution As String
    Public Property Institution() As String
        Get
            Return fInstitution
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Institution", fInstitution, value)
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

    Dim fProcessingDate As Date?
    Public Property ProcessingDate() As Date?
        Get
            Return fProcessingDate
        End Get
        Set(ByVal value As Date?)
            SetPropertyValue(Of Date?)("ProcessingDate", fProcessingDate, value)
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

    Dim fPatientName As String
    Public Property PatientName() As String
        Get
            Return fPatientName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientName", fPatientName, value)
        End Set
    End Property

    Dim fPatientIdentification As String
    Public Property PatientIdentification() As String
        Get
            Return fPatientIdentification
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientIdentification", fPatientIdentification, value)
        End Set
    End Property

    Dim fHistoryNumber As String
    Public Property HistoryNumber() As String
        Get
            Return fHistoryNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("HistoryNumber", fHistoryNumber, value)
        End Set
    End Property

    Dim fPatientWeight As Decimal
    Public Property PatientWeight() As Decimal
        Get
            Return fPatientWeight
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PatientWeight", fPatientWeight, value)
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

    Dim fTotalCalories As Decimal
    Public Property TotalCalories() As Decimal
        Get
            Return fTotalCalories
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalCalories", fTotalCalories, value)
        End Set
    End Property

    Dim fCarbohydrates As Decimal
    Public Property Carbohydrates() As Decimal
        Get
            Return fCarbohydrates
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Carbohydrates", fCarbohydrates, value)
        End Set
    End Property

    Dim fLipidConcentration As Decimal
    Public Property LipidConcentration() As Decimal
        Get
            Return fLipidConcentration
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("LipidConcentration", fLipidConcentration, value)
        End Set
    End Property

    Dim fProteinConcentration As Decimal
    Public Property ProteinConcentration() As Decimal
        Get
            Return fProteinConcentration
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ProteinConcentration", fProteinConcentration, value)
        End Set
    End Property

    Dim fOsmolarity As Decimal
    Public Property Osmolarity() As Decimal
        Get
            Return fOsmolarity
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Osmolarity", fOsmolarity, value)
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

    Dim fSterileWater As Decimal
    Public Property SterileWater() As Decimal
        Get
            Return fSterileWater
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("SterileWater", fSterileWater, value)
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

    Dim fInfusionRate As Decimal
    Public Property InfusionRate() As Decimal
        Get
            Return fInfusionRate
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("InfusionRate", fInfusionRate, value)
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

    Dim fStability As Date?
    Public Property Stability() As Date?
        Get
            Return fStability
        End Get
        Set(ByVal value As Date?)
            SetPropertyValue(Of Date?)("Stability", fStability, value)
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

    Dim fNutritionistName As String
    Public Property NutritionistName() As String
        Get
            Return fNutritionistName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NutritionistName", fNutritionistName, value)
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

#Region "Navigations Properties"

    <Association("ViewMainInformationParenteralNutritionLabel_References_ViewDetailsParenteralNutritionLabel", GetType(ViewDetailsParenteralNutritionLabelXpo))>
    Public ReadOnly Property ViewDetailsParenteralNutritionLabelXpo() As XPCollection(Of ViewDetailsParenteralNutritionLabelXpo)
        Get
            Return GetCollection(Of ViewDetailsParenteralNutritionLabelXpo)("ViewDetailsParenteralNutritionLabelXpo")
        End Get
    End Property

    <NonPersistent>
    Public Property Micronutrients As List(Of NPTLabels)

    <NonPersistent>
    Public Property Macronutrients As List(Of NPTLabels)

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


Public Class NPTLabels
    Public Property Component1 As String
    Public Property Volume1 As String
    Public Property Component2 As String
    Public Property Volume2 As String
    Public Property Component3 As String
    Public Property Volume3 As String
End Class