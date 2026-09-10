'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Hector Rodriguez Rubiano
' Created          : 17/01/2020
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("MixingStation.CMCenterAttention")>
Partial Public Class CMCenterAttentionXpo
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

    Dim _Id As String
    <Key(True)>
    Public Property Id() As String
        Get
            Return _Id
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Id", _Id, value)
        End Set
    End Property

    Dim _IdMixingStation As MixinStationCMConfigXpo
    <Association("CMCareCenterReferencesCMConfigure")>
    Public Property IdMixingStation() As MixinStationCMConfigXpo
        Get
            Return _IdMixingStation
        End Get
        Set(ByVal value As MixinStationCMConfigXpo)
            SetPropertyValue(Of MixinStationCMConfigXpo)("IdMixingStation", _IdMixingStation, value)
        End Set
    End Property

    Dim _IdCenterAttention As Integer
    <Persistent("IdCenterAttention")>
    Public Property IdCenterAttention() As Integer
        Get
            Return _IdCenterAttention
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdCenterAttention", _IdCenterAttention, value)
        End Set
    End Property

    <PersistentAlias("ProductionLine.Id")>
    Public ReadOnly Property IdProductionLine() As Integer
        Get
            Return Convert.ToInt32(EvaluateAlias("IdProductionLine"))
        End Get
    End Property

    Dim _StateCA As Boolean
    <Persistent("StateCA")>
    Public Property StateCA() As Boolean
        Get
            Return _StateCA
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("StateCA", _StateCA, value)
        End Set
    End Property

    Dim fCenterAttention As CentersXpo
    <Persistent("CodeCenterAttention")>
    <Association("CMCenterAttentionReferencesCareCenter")>
    Public Property CenterAttention() As CentersXpo
        Get
            Return fCenterAttention
        End Get
        Set(ByVal value As CentersXpo)
            SetPropertyValue(Of CentersXpo)("CenterAttention", fCenterAttention, value)
        End Set
    End Property

    <PersistentAlias("CenterAttention.CODCENATE")>
    Public ReadOnly Property CodeCenterAttention() As String
        Get
            Return Convert.ToString(EvaluateAlias("CodeCenterAttention")).Trim()
        End Get
    End Property


    Dim fProductionLine As MixingStationProductionLineXpo
    <Persistent("IdProductionLine")>
    <Association("CMCenterAttention_References_ProductionLine")>
    Public Property ProductionLine() As MixingStationProductionLineXpo
        Get
            Return fProductionLine
        End Get
        Set(ByVal value As MixingStationProductionLineXpo)
            SetPropertyValue("ProductionLine", fProductionLine, value)
        End Set
    End Property


End Class
