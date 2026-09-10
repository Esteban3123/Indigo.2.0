'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Yoe Andres Cardenas
' Created          : 16-04-2019
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports DevExpress.Xpo

<Persistent("MixingStation.StabilityTableDetailReconstitution")>
Partial Public Class StabilityTableDetailReconstitutionXpo
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

    Dim fStabilityTableDetailId As StabilityTableDetailXpo
    <Association("StabilityTableDetailReconstitutionferencesStabilityTableDetail")>
    Public Property StabilityTableDetailId() As StabilityTableDetailXpo
        Get
            Return fStabilityTableDetailId
        End Get
        Set(ByVal value As StabilityTableDetailXpo)
            SetPropertyValue(Of StabilityTableDetailXpo)("StabilityTableDetailId", fStabilityTableDetailId, value)
        End Set
    End Property

    Dim fATCId As MixingStationATCXpo
    <Association("StabilityTableDetailReconstitutionferencesATC")>
    Public Property ATCId() As MixingStationATCXpo
        Get
            Return fATCId
        End Get
        Set(ByVal value As MixingStationATCXpo)
            SetPropertyValue(Of MixingStationATCXpo)("ATCId", fATCId, value)
        End Set
    End Property

    Dim fVolume As Decimal
    Public Property Volume() As Decimal
        Get
            Return fVolume
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Volume", fVolume, value)
        End Set
    End Property

    Dim fHourStability As Integer
    Public Property HourStability() As Integer
        Get
            Return fHourStability
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("HourStability", fHourStability, value)
        End Set
    End Property

    Dim fStorageTemperatureId As MixingStationStorageTemperatureXpo
    <Association("StabilityTableDetailReconstitutionreferencesStorageTemperature")>
    Public Property StorageTemperatureId() As MixingStationStorageTemperatureXpo
        Get
            Return fStorageTemperatureId
        End Get
        Set(ByVal value As MixingStationStorageTemperatureXpo)
            SetPropertyValue(Of MixingStationStorageTemperatureXpo)("StorageTemperatureId", fStorageTemperatureId, value)
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

End Class