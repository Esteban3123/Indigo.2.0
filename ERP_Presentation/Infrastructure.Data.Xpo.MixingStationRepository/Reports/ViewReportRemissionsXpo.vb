'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : giovanny Plazas Lozano
' Created          : 12/08/2022
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("MixingStation.ViewReportRemissions")>
Partial Public Class ViewReportRemissionsXpo
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
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fMixingStationName As String
    Public Property MixingStationName() As String
        Get
            Return fMixingStationName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MixingStationName", fMixingStationName, value)
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

    Dim fPatientCodeName As String
    Public Property PatientCodeName() As String
        Get
            Return fPatientCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientCodeName", fPatientCodeName, value)
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

    Dim fProductName As String
    Public Property ProductName() As String
        Get
            Return fProductName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductName", fProductName, value)
        End Set
    End Property

    Dim fProductConcentration As String
    Public Property ProductConcentration() As String
        Get
            Return fProductConcentration
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductConcentration", fProductConcentration, value)
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

    Dim fVehicleConcentration As String
    Public Property VehicleConcentration() As String
        Get
            Return fVehicleConcentration
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("VehicleConcentration", fVehicleConcentration, value)
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

    Dim fQuantity As Integer
    Public Property Quantity() As Integer
        Get
            Return fQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Quantity", fQuantity, value)
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

    Dim fCampaignDetailId As Integer
    Public Property CampaignDetailId() As Integer
        Get
            Return fCampaignDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CampaignDetailId", fCampaignDetailId, value)
        End Set
    End Property

    Dim fProcessingDate As DateTime?
    Public Property ProcessingDate() As DateTime?
        Get
            Return fProcessingDate
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("ProcessingDate", fProcessingDate, value)
        End Set
    End Property

    Dim fCodeFunctionalUnit As String
    Public Property CodeFunctionalUnit() As String
        Get
            Return fCodeFunctionalUnit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeFunctionalUnit", fCodeFunctionalUnit, value)
        End Set
    End Property

    Dim fMunicipalityName As String
    Public Property MunicipalityName() As String
        Get
            Return fMunicipalityName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MunicipalityName", fMunicipalityName, value)
        End Set
    End Property


    Dim fCampaignNumber As Integer
    Public Property CampaignNumber() As Integer
        Get
            Return fCampaignNumber
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CampaignNumber", fCampaignNumber, value)
        End Set
    End Property

    Dim fProductionScheduleCode As String
    Public Property ProductionScheduleCode() As String
        Get
            Return fProductionScheduleCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductionScheduleCode", fProductionScheduleCode, value)
        End Set
    End Property

    Dim fCenterAttention As String
    Public Property CenterAttention() As String
        Get
            Return fCenterAttention
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CenterAttention", fCenterAttention, value)
        End Set
    End Property

    Dim fUnitDoseTypeName As String
    Public Property UnitDoseTypeName() As String
        Get
            Return fUnitDoseTypeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UnitDoseTypeName", fUnitDoseTypeName, value)
        End Set
    End Property

    Dim fUnitDoseTypeMsClass As Integer
    Public Property UnitDoseTypeMsClass() As Integer
        Get
            Return fUnitDoseTypeMsClass
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("UnitDoseTypeMsClass", fUnitDoseTypeMsClass, value)
        End Set
    End Property

End Class