'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Duván Albeiro Mejia Cortes
' Created          : 2022-02-05
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("MixingStation.ViewListMedicinesProduction")>
Partial Public Class ViewListMedicinesProductionXpo
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

    Dim fCMConfigurationId As Integer
    Public Property CMConfigurationId() As Integer
        Get
            Return fCMConfigurationId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CMConfigurationId", fCMConfigurationId, value)
        End Set
    End Property

    Dim fATCId As Integer
    Public Property ATCId() As Integer
        Get
            Return fATCId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ATCId", fATCId, value)
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

    Dim fATCCodeName As String
    Public Property ATCCodeName() As String
        Get
            Return fATCCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ATCCodeName", fATCCodeName, value)
        End Set
    End Property

    Dim fUnitDoseTypeCodeName As String
    Public Property UnitDoseTypeCodeName() As String
        Get
            Return fUnitDoseTypeCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UnitDoseTypeCodeName", fUnitDoseTypeCodeName, value)
        End Set
    End Property

    Dim fCenterAttentionCodeName As String
    Public Property CenterAttentionCodeName() As String
        Get
            Return fCenterAttentionCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CenterAttentionCodeName", fCenterAttentionCodeName, value)
        End Set
    End Property

    Dim fAllowRemant As String
    Public Property AllowRemant() As String
        Get
            Return fAllowRemant
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AllowRemant", fAllowRemant, value)
        End Set
    End Property
End Class