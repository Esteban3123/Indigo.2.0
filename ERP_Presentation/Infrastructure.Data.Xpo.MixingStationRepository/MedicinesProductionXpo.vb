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

<Persistent("MixingStation.MedicinesProduction")>
Partial Public Class MedicinesProductionXpo
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
    <Association("MedicinesProductionReferencesCMConfiguration")>
    Public Property CMConfigurationId() As MixinStationCMConfigXpo
        Get
            Return fCMConfigurationId
        End Get
        Set(ByVal value As MixinStationCMConfigXpo)
            SetPropertyValue(Of MixinStationCMConfigXpo)("CMConfigurationId", fCMConfigurationId, value)
        End Set
    End Property

    Dim fATCId As MixingStationATCXpo
    <Association("MedicinesProductionReferencesATC")>
    Public Property ATCId() As MixingStationATCXpo
        Get
            Return fATCId
        End Get
        Set(ByVal value As MixingStationATCXpo)
            SetPropertyValue(Of MixingStationATCXpo)("ATCId", fATCId, value)
        End Set
    End Property

    Dim fUnitDoseTypeId As MixinStationUnitDoseTypeXpo
    <Association("MedicinesProductionReferencesUnitDoseType")>
    Public Property UnitDoseTypeId() As MixinStationUnitDoseTypeXpo
        Get
            Return fUnitDoseTypeId
        End Get
        Set(ByVal value As MixinStationUnitDoseTypeXpo)
            SetPropertyValue(Of MixinStationUnitDoseTypeXpo)("UnitDoseTypeId", fUnitDoseTypeId, value)
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

    Dim fCenterAttention As CentersXpo
    <Persistent("CenterAttentionId")>
    <Association("MedicinesProductionReferencesCareCenter")>
    Public Property CenterAttention() As CentersXpo
        Get
            Return fCenterAttention
        End Get
        Set(ByVal value As CentersXpo)
            SetPropertyValue(Of CentersXpo)("CenterAttention", fCenterAttention, value)
        End Set
    End Property

    <PersistentAlias("CenterAttention.CODCENATE")>
    Public ReadOnly Property CenterAttentionId() As String
        Get
            Return Convert.ToString(EvaluateAlias("CenterAttentionId")).Trim()
        End Get
    End Property


End Class