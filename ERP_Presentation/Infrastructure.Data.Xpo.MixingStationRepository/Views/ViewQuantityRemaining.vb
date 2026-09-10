'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Andrea Coqueco
' Created          : 13-12-2022
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("MixingStation.ViewQuantityRemaining")>
Partial Public Class ViewQuantityRemainingXpo
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

    Dim fProductId As Integer
    Public Property ProductId() As Integer
        Get
            Return fProductId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ProductId", fProductId, value)
        End Set
    End Property

    Dim fBatchSerialId As Integer
    Public Property BatchSerialId() As Integer
        Get
            Return fBatchSerialId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("BatchSerialId", fBatchSerialId, value)
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

    Dim fStability As Integer
    Public Property Stability() As Integer
        Get
            Return fStability
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Stability", fStability, value)
        End Set
    End Property

    Dim fUnitTimeStability As Boolean
    Public Property UnitTimeStability() As Boolean
        Get
            Return fUnitTimeStability
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("UnitTimeStability", fUnitTimeStability, value)
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

    Dim fRemnantVolume As Decimal
    Public Property RemnantVolume() As Decimal
        Get
            Return fRemnantVolume
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("RemnantVolume", fRemnantVolume, value)
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

    Dim fQuantity As Decimal
    Public Property Quantity() As Decimal
        Get
            Return fQuantity
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Quantity", fQuantity, value)
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

    Dim fMaximumVolume As Decimal
    Public Property MaximumVolume() As Decimal
        Get
            Return fMaximumVolume
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("MaximumVolume", fMaximumVolume, value)
        End Set
    End Property
#End Region

End Class