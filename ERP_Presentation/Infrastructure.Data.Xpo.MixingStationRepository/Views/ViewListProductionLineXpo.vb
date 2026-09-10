'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 04/02/2021
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("MixingStation.ViewListProductionLine")>
Partial Public Class ViewListProductionLineXpo
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

    Dim fCMConfigurationId As Integer
    Public Property CMConfigurationId() As Integer
        Get
            Return fCMConfigurationId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CMConfigurationId", fCMConfigurationId, value)
        End Set
    End Property

    Dim fCareCenterCode As String
    Public Property CareCenterCode() As String
        Get
            Return fCareCenterCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CareCenterCode", fCareCenterCode, value)
        End Set
    End Property

    Dim fProductionLineId As Integer
    Public Property ProductionLineId() As Integer
        Get
            Return fProductionLineId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ProductionLineId", fProductionLineId, value)
        End Set
    End Property

    Dim fProductionLineCode As String
    Public Property ProductionLineCode() As String
        Get
            Return fProductionLineCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductionLineCode", fProductionLineCode, value)
        End Set
    End Property

    Dim fProductionLineName As String
    Public Property ProductionLineName() As String
        Get
            Return fProductionLineName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductionLineName", fProductionLineName, value)
        End Set
    End Property

    Dim fProductionLineCodeName As String
    Public Property ProductionLineCodeName() As String
        Get
            Return fProductionLineCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductionLineCodeName", fProductionLineCodeName, value)
        End Set
    End Property

    Dim fUnitDoseTypeId As Integer
    Public Property UnitDoseTypeId() As Integer
        Get
            Return fUnitDoseTypeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("UnitDoseTypeId", fUnitDoseTypeId, value)
        End Set
    End Property

    Dim fSourceType As Integer
    Public Property SourceType() As Integer
        Get
            Return fSourceType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("SourceType", fSourceType, value)
        End Set
    End Property

End Class