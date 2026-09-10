'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Judy Andrea Díaz Reyes
' Created          : 02-05-2019
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("MixingStation.CMConfiguration")>
Partial Public Class MixinStationCMConfigXpo
    Inherits XPLiteObject


#Region "Properties"
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

    Dim _manageMS As Integer
    <Persistent("ManageMS")>
    Public Property ManageMS() As Integer
        Get
            Return _manageMS
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ManageMS", _manageMS, value)
        End Set
    End Property

    Dim _msType As Integer
    <Persistent("MixingStationType")>
    Public Property MixingStationType() As Integer
        Get
            Return _msType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MixingStationType", _msType, value)
        End Set
    End Property

    <PersistentAlias("Iif(MixingStationType = 1, 'CMP Propia', MixingStationType = 2, 'CMP Propia y Atención IPS Externas', 'CMP Atención IPS Externas')")>
    Public ReadOnly Property TypeName As String
        Get
           Return Convert.ToString(Me.EvaluateAlias("TypeName"))
        End Get
    End Property

    Dim _Name As String
    <Persistent("Name")>
    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", _Name, value)
        End Set
    End Property

    Dim _Code As String
    <Persistent("Code")>
    Public Property Code() As String
        Get
            Return _Code
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", _Code, value)
        End Set
    End Property

    Dim _State As Boolean
    <Persistent("State")>
    Public Property State() As Boolean
        Get
            Return _State
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("State", _State, value)
        End Set
    End Property

    <PersistentAlias("Iif(State, 'Activo', 'Inactivo')")>
    Public ReadOnly Property StateName As String
        Get
           Return Convert.ToString(Me.EvaluateAlias("StateName"))
        End Get
    End Property

    Dim _IdDepmuncod As Integer
    <Persistent("Id_depmuncod")>
    Public Property Id_depmuncod() As Integer
        Get
            Return _IdDepmuncod
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id_depmuncod", _IdDepmuncod, value)
        End Set
    End Property

    Dim _CreationDate As Date
    <Persistent("CreationDate")>
    Public Property CreationDate() As Date
        Get
            Return _CreationDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("CreationDate", _CreationDate, value)
        End Set
    End Property

    <PersistentAlias("concat(concat(Code,' - '),Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

#End Region

#Region "Association"

    <Association("CMCareCenterReferencesCMConfigure", GetType(CMCenterAttentionXpo))>
    Public ReadOnly Property CMCenterAttentionXpo() As XPCollection(Of CMCenterAttentionXpo)
        Get
            Return GetCollection(Of CMCenterAttentionXpo)("CMCenterAttentionXpo")
        End Get
    End Property

    <Association("CMConfigurationUsersReferencesCMConfiguration", GetType(CMConfigurationUsersXpo))>
    Public ReadOnly Property CMConfigurationUsersXpo() As XPCollection(Of CMConfigurationUsersXpo)
        Get
            Return GetCollection(Of CMConfigurationUsersXpo)("CMConfigurationUsersXpo")
        End Get
    End Property

    <Association("CMExternalCareCenterReferencesCMConfigure", GetType(CMExternalCareCenterXpo))>
    Public ReadOnly Property CMExternalCareCenterXpo() As XPCollection(Of CMExternalCareCenterXpo)
        Get
            Return GetCollection(Of CMExternalCareCenterXpo)("CMExternalCareCenterXpo")
        End Get
    End Property

    <Association("MedicinesProductionReferencesCMConfiguration", GetType(MedicinesProductionXpo))>
    Public ReadOnly Property MedicinesProductionXpo() As XPCollection(Of MedicinesProductionXpo)
        Get
            Return GetCollection(Of MedicinesProductionXpo)("MedicinesProductionXpo")
        End Get
    End Property

    <Association("RequestUnitDoseInventoryReferencesCMConfiguration", GetType(RequestUnitDoseInventoryXpo))>
    Public ReadOnly Property RequestUnitDoseInventoryXpo() As XPCollection(Of RequestUnitDoseInventoryXpo)
        Get
            Return GetCollection(Of RequestUnitDoseInventoryXpo)("RequestUnitDoseInventoryXpo")
        End Get
    End Property

    <Association("RequestUnitDoseExternalCareCenterReferencesCMConfiguration", GetType(RequestUnitDoseExternalCareCenterXpo))>
    Public ReadOnly Property RequestUnitDoseExternalCareCenterXpo() As XPCollection(Of RequestUnitDoseExternalCareCenterXpo)
        Get
            Return GetCollection(Of RequestUnitDoseExternalCareCenterXpo)("RequestUnitDoseExternalCareCenterXpo")
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