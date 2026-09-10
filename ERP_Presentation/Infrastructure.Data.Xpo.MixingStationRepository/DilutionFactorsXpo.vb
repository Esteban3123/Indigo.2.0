'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Yoe Andres Cardenas
' Created          : 07-06-2019
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports Infrastructure.Data.Xpo.InventoryRepository

<Persistent("MixingStation.DilutionFactors")>
Partial Public Class DilutionFactorsXpo
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

    Dim fCode As String
    <Size(20)>
    <Persistent("Code")>
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    <PersistentAlias("ATC.Id")>
    Public ReadOnly Property ATCId() As Integer?
        Get
            Return Convert.ToInt32(EvaluateAlias("ATCId"))
        End Get
    End Property

    Dim fMeasurementUnitId As MixingStationMeasurementUnitXpo
    <Persistent("MeasurementUnitId")>
    <Association("DilutionFactorsReferencesMeasurementUnitId")>
    Public Property WeightMeasureUnit() As MixingStationMeasurementUnitXpo
        Get
            Return fMeasurementUnitId
        End Get
        Set(ByVal value As MixingStationMeasurementUnitXpo)
            SetPropertyValue(Of MixingStationMeasurementUnitXpo)("MeasurementUnitId", fMeasurementUnitId, value)
        End Set
    End Property

    Dim fATC As MixingStationATCXpo
    <Persistent("ATCId")>
    <Association("DilutionFactors_References_ATC")>
    Public Property ATC() As MixingStationATCXpo
        Get
            Return fATC
        End Get
        Set(ByVal value As MixingStationATCXpo)
            SetPropertyValue(Of MixingStationATCXpo)("ATC", fATC, value)
        End Set
    End Property

    <Association("DilutionFactorsDetail_References_DilutionFactorsId", GetType(DilutionFactorsDetailXpo))>
    Public ReadOnly Property DilutionFactorsDetailXpo() As XPCollection(Of DilutionFactorsDetailXpo)
        Get
            Return GetCollection(Of DilutionFactorsDetailXpo)("DilutionFactorsDetailXpo")
        End Get
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