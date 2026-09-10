Imports DevExpress.Xpo

<Persistent("AccountManagement.ManagementAreasUser")>
Public Class ManagementAreasUserXpo
    Inherits XPLiteObject

#Region "Members"

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

    Dim fManagementAreasId As ManagementAreasXpo
    <Association("ManagementAreasUserReferencesManagementAreas")>
    Public Property ManagementAreasId() As ManagementAreasXpo
        Get
            Return fManagementAreasId
        End Get
        Set(ByVal value As ManagementAreasXpo)
            SetPropertyValue(Of ManagementAreasXpo)("ManagementAreasId", fManagementAreasId, value)
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
    <Size(50)>
    Public Property UserCode() As String
        Get
            Return fUserCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UserCode", fUserCode, value)
        End Set
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

#End Region

End Class