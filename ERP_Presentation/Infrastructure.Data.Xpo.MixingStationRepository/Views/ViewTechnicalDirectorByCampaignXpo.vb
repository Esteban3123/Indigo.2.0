
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("MixingStation.ViewTechnicalDirectorByCampaign")>
Public Class ViewTechnicalDirectorByCampaignXpo
    Inherits XPLiteObject

    Dim fKeyView As String
    <Key(True)>
    Public Property KeyView() As String
        Get
            Return fKeyView
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("KeyView", fKeyView, value)
        End Set
    End Property

    Dim fCampaignDetailId As Integer
    <Persistent("CampaignDetailId")>
    Public Property CampaignDetailId() As Integer
        Get
            Return fCampaignDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CampaignDetailId", fCampaignDetailId, value)
        End Set
    End Property

    Dim fUserId As Integer
    <Persistent("UserId")>
    Public Property UserId() As Integer
        Get
            Return fUserId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("UserId", fUserId, value)
        End Set
    End Property

    Dim fUserCode As String
    <Persistent("UserCode")>
    Public Property UserCode() As String
        Get
            Return fUserCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UserCode", fUserCode, value)
        End Set
    End Property

    Dim fFullName As String
    <Persistent("FullName")>
    Public Property FullName() As String
        Get
            Return fFullName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FullName", fFullName, value)
        End Set
    End Property


    <PersistentAlias("Concat(UserCode, ' - ' ,FullName)")>
    Public ReadOnly Property UserCodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("UserCodeName"))
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
End Class
