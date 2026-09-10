'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : giovanny Plazas Lozano
' Created          : 14/09/2021
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("MixingStation.ViewListPrintLabels")>
Partial Public Class ViewListPrintLabelsXpo
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

    Dim fId As Long?
    <Key(True)>
    Public Property Id() As Long?
        Get
            Return fId
        End Get
        Set(ByVal value As Long?)
            SetPropertyValue(Of Long?)("Id", fId, value)
        End Set
    End Property

    Dim fFunctionalUnitName As String
    Public Property FunctionalUnitName() As String
        Get
            Return fFunctionalUnitName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FunctionalUnitName", fFunctionalUnitName, value)
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

    Dim fPackageName As String
    Public Property PackageName() As String
        Get
            Return fPackageName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PackageName", fPackageName, value)
        End Set
    End Property

    Dim fDose As Decimal
    Public Property Dose() As Decimal
        Get
            Return fDose
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Dose", fDose, value)
        End Set
    End Property

    Dim fVehicleName As String
    Public Property VehicleName() As String
        Get
            Return fVehicleName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("VehicleName", fVehicleName, value)
        End Set
    End Property

    Dim fVolumeTotalOrder As String
    Public Property VolumeTotalOrder() As String
        Get
            Return fVolumeTotalOrder
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("VolumeTotalOrder", fVolumeTotalOrder, value)
        End Set
    End Property

    Dim fConcentration As String
    Public Property Concentration() As String
        Get
            Return fConcentration
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Concentration", fConcentration, value)
        End Set
    End Property

    Dim fBatchCode As String
    Public Property BatchCode() As String
        Get
            Return fBatchCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BatchCode", fBatchCode, value)
        End Set
    End Property

    Dim fElaborationDate As DateTime?
    Public Property ElaborationDate() As DateTime?
        Get
            Return fElaborationDate
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("ElaborationDate", fElaborationDate, value)
        End Set
    End Property

    Dim fElaborationHour As DateTime?
    Public Property ElaborationHour() As DateTime?
        Get
            Return fElaborationHour
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("ElaborationHour", fElaborationHour, value)
        End Set
    End Property

    Dim fExpirationDate As DateTime?
    Public Property ExpirationDate() As DateTime?
        Get
            Return fExpirationDate
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("ExpirationDate", fExpirationDate, value)
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

    Dim fIndications As String
    Public Property Indications() As String
        Get
            Return fIndications
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Indications", fIndications, value)
        End Set
    End Property

    Dim fSpecialIndications As String
    Public Property SpecialIndications() As String
        Get
            Return fSpecialIndications
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SpecialIndications", fSpecialIndications, value)
        End Set
    End Property

    Dim fStorage As String
    Public Property Storage() As String
        Get
            Return fStorage
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Storage", fStorage, value)
        End Set
    End Property

    Dim fNameRiskLevel As String
    Public Property NameRiskLevel() As String
        Get
            Return fNameRiskLevel
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NameRiskLevel", fNameRiskLevel, value)
        End Set
    End Property

    Dim fUnitDoseTypeDes As String
    Public Property UnitDoseTypeDes() As String
        Get
            Return fUnitDoseTypeDes
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UnitDoseTypeDes", fUnitDoseTypeDes, value)
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

    Dim fMainMedicines As String
    Public Property MainMedicines() As String
        Get
            Return fMainMedicines
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MainMedicines", fMainMedicines, value)
        End Set
    End Property

    Dim fMeasurementUnitAbbreviation As String
    Public Property MeasurementUnitAbbreviation() As String
        Get
            Return fMeasurementUnitAbbreviation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MeasurementUnitAbbreviation", fMeasurementUnitAbbreviation, value)
        End Set
    End Property

    Dim fNameQualityChemical As String
    Public Property NameQualityChemical() As String
        Get
            Return fNameQualityChemical
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NameQualityChemical", fNameQualityChemical, value)
        End Set
    End Property

    Dim fNameProductionChemical As String
    Public Property NameProductionChemical() As String
        Get
            Return fNameProductionChemical
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NameProductionChemical", fNameProductionChemical, value)
        End Set
    End Property

    Dim fLabelType As Integer
    Public Property LabelType() As Integer
        Get
            Return fLabelType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("LabelType", fLabelType, value)
        End Set
    End Property

End Class