'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.ContractRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 10/10/2014
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo

#End Region

''' <summary>
''' conceptos de nota usado en los servicios Xpo
''' </summary>
<Persistent("Contract.RateManual")> _
Public Class RateManualXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As Integer
    <Key(True)> _
    <Persistent("Id")> _
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fCode As String
    <Size(20)> _
    <Persistent("Code")> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fName As String
    <Size(100)> _
    <Persistent("Name")> _
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property

    Dim fStatus As Boolean
    <Persistent("Status")> _
    Public Property Status() As Boolean
        Get
            Return fStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Status", fStatus, value)
        End Set
    End Property

    <PersistentAlias("Iif(Status = 1, 'Activo', 'Inactivo')")>
    Public ReadOnly Property StatusName As String
        Get
            'If fStatus = False Then
            '    Return "Inactivo"
            'Else
            '    Return "Activo"
            'End If
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

    Dim fType As Integer
    <Persistent("Type")> _
    Public Property Type() As Integer
        Get
            Return fType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Type", fType, value)
        End Set
    End Property

    <PersistentAlias("Iif(Type = 1, 'ISS 2001', Type = 2, 'ISS 2004', Type = 3, 'SOAT', Type = 4, 'Institucional', '')")>
    Public ReadOnly Property TypeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("TypeName"))
        End Get
    End Property

    Dim fRoundService As Integer
    <Persistent("RoundService")> _
    Public Property RoundService() As Integer
        Get
            Return fRoundService
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RoundService", fRoundService, value)
        End Set
    End Property

    <PersistentAlias("Iif(RoundService = 10, 'Decima', Iif(RoundService = 100, 'Centecima',Iif(RoundService = 1000, 'Milesima', '')))")>
    Public ReadOnly Property RoundServiceName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("RoundServiceName"))
        End Get
    End Property

    'columna que devuelve el codigo y el nombre concatenado
    <Size(50)> _
    <PersistentAlias("concat(concat(Code,' - '),Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

    <Association("RateManualDetailReferencesRateManual", GetType(RateManualDetailXpo))> _
    Public ReadOnly Property RateManualDetailXpo() As XPCollection(Of RateManualDetailXpo)
        Get
            Return GetCollection(Of RateManualDetailXpo)("RateManualDetailXpo")
        End Get
    End Property

    <Association("DefinitionRateDetailReferencesRateManual", GetType(DefinitionRateDetailXpo))> _
    Public ReadOnly Property DefinitionRateDetailXpo() As XPCollection(Of DefinitionRateDetailXpo)
        Get
            Return GetCollection(Of DefinitionRateDetailXpo)("DefinitionRateDetailXpo")
        End Get
    End Property

#End Region

#Region "Builders"

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
