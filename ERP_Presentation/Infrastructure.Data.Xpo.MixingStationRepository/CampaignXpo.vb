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

<Persistent("MixingStation.Campaign")>
Partial Public Class CampaignXpo
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

    'Dim fCMConfigurationId As Integer
    'Public Property CMConfigurationId() As Integer
    '    Get
    '        Return fCMConfigurationId
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("CMConfigurationId", fCMConfigurationId, value)
    '    End Set
    'End Property
    <PersistentAlias("CMConfiguration.Id")>
    Public ReadOnly Property CMConfigurationId() As Integer
        Get
            Return Convert.ToInt32(EvaluateAlias("CMConfigurationId"))
        End Get
    End Property

    Dim fCampaignQuantity As Integer
    Public Property CampaignQuantity() As Integer
        Get
            Return fCampaignQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CampaignQuantity", fCampaignQuantity, value)
        End Set
    End Property

    <Association("CampaignDetailReferencesCampaign", GetType(CampaignDetailXpo))>
    Public ReadOnly Property CampaignDetailXpo() As XPCollection(Of CampaignDetailXpo)
        Get
            Return GetCollection(Of CampaignDetailXpo)("CampaignDetailXpo")
        End Get
    End Property

    Dim fCMConfiguration As CMConfigurationXpo
    <Persistent("CMConfigurationId")>
    <Association("Campaign_References_CMConfiguration")>
    Public Property CMConfiguration() As CMConfigurationXpo
        Get
            Return fCMConfiguration
        End Get
        Set(ByVal value As CMConfigurationXpo)
            SetPropertyValue("CMConfiguration", fCMConfiguration, value)
        End Set
    End Property

End Class