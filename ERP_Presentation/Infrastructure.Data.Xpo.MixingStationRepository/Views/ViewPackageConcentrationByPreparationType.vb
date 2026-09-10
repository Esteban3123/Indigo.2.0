'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Andrea Coqueco
' Created          : 09-12-2022
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("MixingStation.ViewPackageConcentrationByPreparationType")>
Partial Public Class ViewPackageConcentrationByPreparationTypeXpo
    Inherits XPLiteObject

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

#Region "Properties"
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

    Dim fCampaignDetailId As Integer
    Public Property CampaignDetailId() As Integer
        Get
            Return fCampaignDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CampaignDetailId", fCampaignDetailId, value)
        End Set
    End Property

    Dim fPreparationType As Integer
    Public Property PreparationType() As Integer
        Get
            Return fPreparationType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PreparationType", fPreparationType, value)
        End Set
    End Property

    Dim fProductId As Integer
    Public Property ProductId() As Integer
        Get
            Return fProductId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ProductId", fProductId, value)
        End Set
    End Property

    Dim fStability As Decimal
    Public Property Stability() As Decimal
        Get
            Return fStability
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Stability", fStability, value)
        End Set
    End Property

    Dim fTimeUnit As Integer
    Public Property TimeUnit() As Integer
        Get
            Return fTimeUnit
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("TimeUnit", fTimeUnit, value)
        End Set
    End Property

    Dim fTimeUnitNameStability As String
    Public Property TimeUnitNameStability() As String
        Get
            Return fTimeUnitNameStability
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("TimeUnitNameStability", fTimeUnitNameStability, value)
        End Set
    End Property

    Dim fConcentration As Decimal
    Public Property Concentration() As Decimal
        Get
            Return fConcentration
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Concentration", fConcentration, value)
        End Set
    End Property

    Dim fMeasurementUnitMainMedicine As String
    Public Property MeasurementUnitMainMedicine() As String
        Get
            Return fMeasurementUnitMainMedicine
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MeasurementUnitMainMedicine", fMeasurementUnitMainMedicine, value)
        End Set
    End Property

    Dim fMeasurementUnitMainMedicineId As Integer
    Public Property MeasurementUnitMainMedicineId() As Integer
        Get
            Return fMeasurementUnitMainMedicineId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MeasurementUnitMainMedicineId", fMeasurementUnitMainMedicineId, value)
        End Set
    End Property

    Dim fMeasurementUnitVolume As String
    Public Property MeasurementUnitVolume() As String
        Get
            Return fMeasurementUnitVolume
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MeasurementUnitVolume", fMeasurementUnitVolume, value)
        End Set
    End Property

    Dim fMeasurementUnitVolumeId As Integer
    Public Property MeasurementUnitVolumeId() As Integer
        Get
            Return fMeasurementUnitVolumeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MeasurementUnitVolumeId", fMeasurementUnitVolumeId, value)
        End Set
    End Property

    Dim fMaximumVolume As Decimal
    Public Property MaximumVolume() As Decimal
        Get
            Return fMaximumVolume
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("MaximumVolume", fMaximumVolume, value)
        End Set
    End Property

    Dim fAtcIdReconstituent As Integer?
    Public Property AtcIdReconstituent() As Integer?
        Get
            Return fAtcIdReconstituent
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("AtcIdReconstituent", fAtcIdReconstituent, value)
        End Set
    End Property

    Dim fAtcIdVehicle As Integer?
    Public Property AtcIdVehicle() As Integer?
        Get
            Return fAtcIdVehicle
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("AtcIdVehicle", fAtcIdVehicle, value)
        End Set
    End Property

#End Region

End Class