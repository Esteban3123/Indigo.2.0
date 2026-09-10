'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Yoe Andres Cardenas
' Created          : 16-04-2019
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("MixingStation.CMExternalCareCenter")>
Partial Public Class CMExternalCareCenterXpo
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

    Dim fCMConfigurationId As MixinStationCMConfigXpo
    <Association("CMExternalCareCenterReferencesCMConfigure")>
    Public Property CMConfigurationId() As MixinStationCMConfigXpo
        Get
            Return fCMConfigurationId
        End Get
        Set(ByVal value As MixinStationCMConfigXpo)
            SetPropertyValue(Of MixinStationCMConfigXpo)("CMConfigurationId", fCMConfigurationId, value)
        End Set
    End Property

    Dim fCustomerId As Integer
    Public Property CustomerId() As Integer
        Get
            Return fCustomerId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CustomerId", fCustomerId, value)
        End Set
    End Property

    Dim fProductionLineId As Integer
    <PersistentAlias("MixingStationProductionLineXpo.Id")>
    Public ReadOnly Property ProductionLineId() As Integer
        Get
            Return Convert.ToInt32(EvaluateAlias("ProductionLineId"))
        End Get
    End Property

    <PersistentAlias("ExternalCareCenter.Id")>
    Public ReadOnly Property ExternalCareCenterId() As Integer
        Get
            Return Convert.ToInt32(EvaluateAlias("ExternalCareCenterId"))
        End Get
    End Property

    Dim fExternalCareCenter As ExternalCareCenterXpo
    <Persistent("ExternalCareCenterId")>
    <Association("CMExternalCareCenterReferencesExternalCareCenter")>
    Public Property ExternalCareCenter() As ExternalCareCenterXpo
        Get
            Return fExternalCareCenter
        End Get
        Set(ByVal value As ExternalCareCenterXpo)
            SetPropertyValue(Of ExternalCareCenterXpo)("ExternalCareCenter", fExternalCareCenter, value)
        End Set
    End Property


    Dim fMixingStationProductionLineXpo As MixingStationProductionLineXpo
    <Persistent("ProductionLineId")>
    <Association("CMMixingProducitonLine_References_CMExternalCareCenter")>
    Public Property MixingStationProductionLineXpo() As MixingStationProductionLineXpo
        Get
            Return fMixingStationProductionLineXpo
        End Get
        Set(ByVal value As MixingStationProductionLineXpo)
            SetPropertyValue(Of MixingStationProductionLineXpo)("MixingStationProductionLineXpo", fMixingStationProductionLineXpo, value)
        End Set
    End Property

    Dim fStatus As Boolean
    Public Property Status() As Boolean
        Get
            Return fStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Status", fStatus, value)
        End Set
    End Property

End Class