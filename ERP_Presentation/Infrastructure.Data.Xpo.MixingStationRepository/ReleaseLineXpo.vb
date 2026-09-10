'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Diego A. Roldan
' Created          : 2021-11-04
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports DevExpress.Xpo

<Persistent("MixingStation.ReleaseLine")>
Partial Public Class ReleaseLineXpo
    Inherits XPLiteObject

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

    <PersistentAlias("CampaignDetail.Id")>
    Public ReadOnly Property CampaignDetailId() As Integer
        Get
            Return Convert.ToInt32(EvaluateAlias("CampaignDetailId"))
        End Get
    End Property

    Dim fWorkingAreaId As Integer
    Public Property WorkingAreaId() As Integer
        Get
            Return fWorkingAreaId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("WorkingAreaId", fWorkingAreaId, value)
        End Set
    End Property

    Dim fIsSterile As Boolean
    Public Property IsSterile() As Boolean
        Get
            Return fIsSterile
        End Get
        Set(value As Boolean)
            SetPropertyValue(Of Boolean)("IsSterile", fIsSterile, value)
        End Set
    End Property

    Dim fAdequacyItem1 As Boolean
    Public Property AdequacyItem1() As Boolean
        Get
            Return fAdequacyItem1
        End Get
        Set(value As Boolean)
            SetPropertyValue(Of Boolean)("AdequacyItem1", fAdequacyItem1, value)
        End Set
    End Property

    Dim fAdequacyItem2 As Boolean
    Public Property AdequacyItem2() As Boolean
        Get
            Return fAdequacyItem2
        End Get
        Set(value As Boolean)
            SetPropertyValue(Of Boolean)("AdequacyItem2", fAdequacyItem2, value)
        End Set
    End Property

    Dim fAdequacyItem3 As Boolean
    Public Property AdequacyItem3() As Boolean
        Get
            Return fAdequacyItem3
        End Get
        Set(value As Boolean)
            SetPropertyValue(Of Boolean)("AdequacyItem3", fAdequacyItem3, value)
        End Set
    End Property

    Dim fAdequacyItem4 As Boolean
    Public Property AdequacyItem4() As Boolean
        Get
            Return fAdequacyItem4
        End Get
        Set(value As Boolean)
            SetPropertyValue(Of Boolean)("AdequacyItem4", fAdequacyItem4, value)
        End Set
    End Property

    Dim fAdequacyItem5 As Boolean
    Public Property AdequacyItem5() As Boolean
        Get
            Return fAdequacyItem5
        End Get
        Set(value As Boolean)
            SetPropertyValue(Of Boolean)("AdequacyItem5", fAdequacyItem5, value)
        End Set
    End Property

    Dim fAdequacyItem6 As Boolean
    Public Property AdequacyItem6() As Boolean
        Get
            Return fAdequacyItem6
        End Get
        Set(value As Boolean)
            SetPropertyValue(Of Boolean)("AdequacyItem6", fAdequacyItem6, value)
        End Set
    End Property

    Dim fAdequacyItem7 As Boolean
    Public Property AdequacyItem7() As Boolean
        Get
            Return fAdequacyItem7
        End Get
        Set(value As Boolean)
            SetPropertyValue(Of Boolean)("AdequacyItem7", fAdequacyItem7, value)
        End Set
    End Property

    Dim fAdequacyItem8 As Boolean
    Public Property AdequacyItem8() As Boolean
        Get
            Return fAdequacyItem8
        End Get
        Set(value As Boolean)
            SetPropertyValue(Of Boolean)("AdequacyItem8", fAdequacyItem8, value)
        End Set
    End Property

    Dim fConditioningItem1 As Boolean
    Public Property ConditioningItem1() As Boolean
        Get
            Return fConditioningItem1
        End Get
        Set(value As Boolean)
            SetPropertyValue(Of Boolean)("ConditioningItem1", fConditioningItem1, value)
        End Set
    End Property

    Dim fConditioningItem2 As Boolean
    Public Property ConditioningItem2() As Boolean
        Get
            Return fConditioningItem2
        End Get
        Set(value As Boolean)
            SetPropertyValue(Of Boolean)("ConditioningItem2", fConditioningItem2, value)
        End Set
    End Property

    Dim fConditioningItem3 As Boolean
    Public Property ConditioningItem3() As Boolean
        Get
            Return fConditioningItem3
        End Get
        Set(value As Boolean)
            SetPropertyValue(Of Boolean)("ConditioningItem3", fConditioningItem3, value)
        End Set
    End Property

    Dim fConditioningItem4 As Boolean
    Public Property ConditioningItem4() As Boolean
        Get
            Return fConditioningItem4
        End Get
        Set(value As Boolean)
            SetPropertyValue(Of Boolean)("ConditioningItem4", fConditioningItem4, value)
        End Set
    End Property

    Dim fConditioningItem5 As Boolean
    Public Property ConditioningItem5() As Boolean
        Get
            Return fConditioningItem5
        End Get
        Set(value As Boolean)
            SetPropertyValue(Of Boolean)("ConditioningItem5", fConditioningItem5, value)
        End Set
    End Property

    Dim fConditioningItem6 As Boolean
    Public Property ConditioningItem6() As Boolean
        Get
            Return fConditioningItem6
        End Get
        Set(value As Boolean)
            SetPropertyValue(Of Boolean)("x", fConditioningItem6, value)
        End Set
    End Property

    Dim fConditioningItem7 As Boolean
    Public Property ConditioningItem7() As Boolean
        Get
            Return fConditioningItem7
        End Get
        Set(value As Boolean)
            SetPropertyValue(Of Boolean)("ConditioningItem7", fConditioningItem7, value)
        End Set
    End Property

    Dim fConditioningItem8 As Boolean
    Public Property ConditioningItem8() As Boolean
        Get
            Return fConditioningItem8
        End Get
        Set(value As Boolean)
            SetPropertyValue(Of Boolean)("ConditioningItem8", fConditioningItem8, value)
        End Set
    End Property

    'Dim fx
    'Public Property x() As Byte
    '    Get
    '        Return fx
    '    End Get
    '    Set(value As Byte)
    '        SetPropertyValue(Of Byte)("x", fx, value)
    '    End Set
    'End Property

#Region "Relations"
    Private fCampaignDetail As CampaignDetailXpo
    <Association("ReleaseLine_Reference_CampaigDetail")>
    <Persistent("CampaignDetailId")>
    Public Property CampaignDetail() As CampaignDetailXpo
        Get
            Return fCampaignDetail
        End Get
        Set(ByVal value As CampaignDetailXpo)
            SetPropertyValue("CampaignDetail", fCampaignDetail, value)
        End Set
    End Property
#End Region

#Region "Builders"
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
#End Region

End Class