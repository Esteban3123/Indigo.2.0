'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Diego A. Roldan
' Created          : 2021-09-10
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports DevExpress.Xpo

<Persistent("MixingStation.CMConfiguration")>
Partial Public Class CMConfigurationXpo
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

    Dim fMixingStationType As Integer
    Public Property MixingStationType() As Integer
        Get
            Return fMixingStationType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MixingStationType", fMixingStationType, value)
        End Set
    End Property

    Dim fCode As String
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fName As String
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property

    Dim fPrefix As String
    Public Property Prefix() As String
        Get
            Return fPrefix
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Prefix", fPrefix, value)
        End Set
    End Property

    Dim fState As Boolean
    Public Property State() As Boolean
        Get
            Return fState
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("State", fState, value)
        End Set
    End Property

    Dim fIdDirector As Integer
    Public Property IdDirector() As Integer
        Get
            Return fIdDirector
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdDirector", fIdDirector, value)
        End Set
    End Property

    <PersistentAlias("Concat(Code, ' - ', Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

    <Association("Campaign_References_CMConfiguration", GetType(CampaignXpo))>
    Public ReadOnly Property Campaigns() As XPCollection(Of CampaignXpo)
        Get
            Return GetCollection(Of CampaignXpo)("Campaigns")
        End Get
    End Property

    <Association("CMMixingProducitonLine_References_CMConfiguration", GetType(CMMixingProducitonLineXpo))>
    Public ReadOnly Property CMMixingProducitonLineXpo() As XPCollection(Of CMMixingProducitonLineXpo)
        Get
            Return GetCollection(Of CMMixingProducitonLineXpo)("CMMixingProducitonLineXpo")
        End Get
    End Property
End Class