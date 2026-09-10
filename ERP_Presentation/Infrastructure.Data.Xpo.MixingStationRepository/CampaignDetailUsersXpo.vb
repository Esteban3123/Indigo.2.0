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

<Persistent("MixingStation.CampaignDetailUsers")>
Partial Public Class CampaignDetailUsersXpo
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

    Dim fCampaignDetailId As CampaignDetailXpo
    <Association("CampaignDetailUsersReferencesCampaignDetail")>
    Public Property CampaignDetailId() As CampaignDetailXpo
        Get
            Return fCampaignDetailId
        End Get
        Set(ByVal value As CampaignDetailXpo)
            SetPropertyValue(Of CampaignDetailXpo)("CampaignDetailId", fCampaignDetailId, value)
        End Set
    End Property

    Dim fUserId As Integer
    Public Property UserId() As Integer
        Get
            Return fUserId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("UserId", fUserId, value)
        End Set
    End Property

    Dim fUserCode As String
    Public Property UserCode() As String
        Get
            Return fUserCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UserCode", fUserCode, value)
        End Set
    End Property

    Dim fUserRole As Byte
    Public Property UserRole() As Byte
        Get
            Return fUserRole
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("UserRole", fUserRole, value)
        End Set
    End Property

#Region "PersistentAlias"

    <PersistentAlias("IIF(UserRole=1,'Supervisor',UserRole=2,'QF Producción',UserRole=3, 'Auxiliar de Central de Mezclas', UserRole=4, 'Director técnico', '')")>
    Public ReadOnly Property UserRoleName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("UserRoleName"))
        End Get
    End Property

    <PersistentAlias("Concat(UserCode, ' - ' ,UserRoleName)")>
    Public ReadOnly Property UserCodeRoleName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("UserCodeRoleName"))
        End Get
    End Property
#End Region

End Class