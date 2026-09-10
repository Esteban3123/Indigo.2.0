'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Andres Alarcon
' Created          : 15-09-2022
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports Infrastructure.Data.Xpo.InventoryRepository

<Persistent("MixingStation.DilutionFactorsDetail")>
Partial Public Class DilutionFactorsDetailXpo
    Inherits XPLiteObject

#Region "Fields"

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

    Dim fVolume As Decimal
    Public Property Volume() As Decimal
        Get
            Return fVolume
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Volume", fVolume, value)
        End Set
    End Property

    Dim fDilution As Decimal
    Public Property Dilution() As Decimal
        Get
            Return fDilution
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Dilution", fDilution, value)
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

    Dim fAmountTime As Integer
    Public Property AmountTime() As Integer
        Get
            Return fAmountTime
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AmountTime", fAmountTime, value)
        End Set
    End Property

    Dim fVolumeMeasureUnit As MixingStationMeasurementUnitXpo
    <Persistent("VolumeMeasureUnit")>
    <Association("DilutionFactorsDetailReferencesMeasurementUnitId")>
    Public Property VolumeMeasureUnit() As MixingStationMeasurementUnitXpo
        Get
            Return fVolumeMeasureUnit
        End Get
        Set(ByVal value As MixingStationMeasurementUnitXpo)
            SetPropertyValue(Of MixingStationMeasurementUnitXpo)("VolumeMeasureUnit", fVolumeMeasureUnit, value)
        End Set
    End Property

    Dim fByDefault As Boolean
    Public Property ByDefault() As Boolean
        Get
            Return fByDefault
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ByDefault", fByDefault, value)
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

    <PersistentAlias("ATC.Id")>
    Public ReadOnly Property AtcId() As Integer?
        Get
            Return Convert.ToInt32(EvaluateAlias("AtcId"))
        End Get
    End Property

    Dim fATC As MixingStationATCXpo
    <Persistent("AtcId")>
    <Association("DilutionFactorsDetail_References_ATC")>
    Public Property ATC() As MixingStationATCXpo
        Get
            Return fATC
        End Get
        Set(ByVal value As MixingStationATCXpo)
            SetPropertyValue(Of MixingStationATCXpo)("ATC", fATC, value)
        End Set
    End Property

    <PersistentAlias("DilutionFactors.Id")>
    Public ReadOnly Property DilutionFactorsId() As Integer?
        Get
            Return Convert.ToInt32(EvaluateAlias("DilutionFactorsId"))
        End Get
    End Property

    Dim fDilutionFactors As DilutionFactorsXpo
    <Persistent("DilutionFactorsId")>
    <Association("DilutionFactorsDetail_References_DilutionFactorsId")>
    Public Property DilutionFactors() As DilutionFactorsXpo
        Get
            Return fDilutionFactors
        End Get
        Set(ByVal value As DilutionFactorsXpo)
            SetPropertyValue(Of DilutionFactorsXpo)("DilutionFactors", fDilutionFactors, value)
        End Set
    End Property


#End Region
#Region "Builder"
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