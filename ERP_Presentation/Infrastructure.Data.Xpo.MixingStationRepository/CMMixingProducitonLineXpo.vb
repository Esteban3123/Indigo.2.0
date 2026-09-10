'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Ruben Dario Castañeda Giraldo
' Created          : 06-08-2019
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports DevExpress.Xpo

<Persistent("MixingStation.CMMixingProducitonLine")>
Partial Public Class CMMixingProducitonLineXpo
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

    Dim _Id As Integer
    <Key(True)>
    Public Property Id() As Integer
        Get
            Return _Id
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", _Id, value)
        End Set
    End Property

    Dim fCMConfiguration As CMConfigurationXpo
    <Persistent("Id_CMConfiguration")>
    <Association("Campaign_References_CMConfiguration")>
    Public Property CMConfiguration() As CMConfigurationXpo
        Get
            Return fCMConfiguration
        End Get
        Set(ByVal value As CMConfigurationXpo)
            SetPropertyValue(Of CMConfigurationXpo)("CMConfiguration", fCMConfiguration, value)
        End Set
    End Property

    <PersistentAlias("CMConfiguration.Id")>
    Public ReadOnly Property Id_CMConfiguration() As Integer
        Get
            Return Convert.ToInt32(EvaluateAlias("Id_CMConfiguration"))
        End Get
    End Property

    Dim fProductionLine As MixingStationProductionLineXpo
    <Persistent("Id_ProductionLine")>
    <Association("CMMixingProducitonLine_References_ProductionLine")>
    Public Property ProductionLine() As MixingStationProductionLineXpo
        Get
            Return fProductionLine
        End Get
        Set(ByVal value As MixingStationProductionLineXpo)
            SetPropertyValue(Of MixingStationProductionLineXpo)("ProductionLine", fProductionLine, value)
        End Set
    End Property

    <PersistentAlias("ProductionLine.Id")>
    Public ReadOnly Property Id_ProductionLine As Integer
        Get
            Return Convert.ToInt32(EvaluateAlias("Id_ProductionLine"))
        End Get
    End Property

    Dim fStatePI As Boolean
    Public Property StatePI() As Boolean
        Get
            Return fStatePI
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("StatePI", fStatePI, value)
        End Set
    End Property




End Class
