'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Yoe Andres Cardenas
' Created          : 16-04-2019
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports DevExpress.Xpo

<Persistent("MixingStation.StabilityTableDetailDilution")>
Partial Public Class StabilityTableDetailDilutionXpo
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
    <Association("StabilityTableDetailDilutionferencesStabilityTableDetail")>
    Public Property StabilityTableDetailId() As StabilityTableDetailXpo
        Get
            Return fStabilityTableDetailId
        End Get
        Set(ByVal value As StabilityTableDetailXpo)
            SetPropertyValue(Of StabilityTableDetailXpo)("StabilityTableDetailId", fStabilityTableDetailId, value)
        End Set
    End Property

    Dim fATCId As MixingStationATCXpo
    <Association("StabilityTableDetailDilutionferencesATC")>
    Public Property ATCId() As MixingStationATCXpo
        Get
            Return fATCId
        End Get
        Set(ByVal value As MixingStationATCXpo)
            SetPropertyValue(Of MixingStationATCXpo)("ATCId", fATCId, value)
        End Set
    End Property

    Dim fConcentrationMaximum As Decimal
    Public Property ConcentrationMaximum() As Decimal
        Get
            Return fConcentrationMaximum
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ConcentrationMaximum", fConcentrationMaximum, value)
        End Set
    End Property

    Dim fConcentrationMinimum As Decimal
    Public Property ConcentrationMinimum() As Decimal
        Get
            Return fConcentrationMinimum
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ConcentrationMinimum", fConcentrationMinimum, value)
        End Set
    End Property

    Dim fStorageTemperatureId As MixingStationStorageTemperatureXpo
    <Association("StabilityTableDetailDilutionreferencesStorageTemperature")>
    Public Property StorageTemperatureId() As MixingStationStorageTemperatureXpo
        Get
            Return fStorageTemperatureId
        End Get
        Set(ByVal value As MixingStationStorageTemperatureXpo)
            SetPropertyValue(Of MixingStationStorageTemperatureXpo)("StorageTemperatureId", fStorageTemperatureId, value)
        End Set
    End Property

    Dim fPhotoProtection As Boolean
    Public Property PhotoProtection() As Boolean
        Get
            Return fPhotoProtection
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("PhotoProtection", fPhotoProtection, value)
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

    Dim fBibliographicReference As String
    Public Property BibliographicReference() As String
        Get
            Return fBibliographicReference
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BibliographicReference", fBibliographicReference, value)
        End Set
    End Property

    Dim fContainer As String
    Public Property Container() As String
        Get
            Return fContainer
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Container", fContainer, value)
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

End Class