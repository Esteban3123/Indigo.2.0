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

<Persistent("MixingStation.ExternalCareCenterUsers")>
Partial Public Class ExternalCareCenterUsersXpo
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

    Dim fExternalCareCenterId As ExternalCareCenterXpo
    <Association("ExternalCareCenterUsersReferencesExternalCareCenter")>
    Public Property ExternalCareCenterId() As ExternalCareCenterXpo
        Get
            Return fExternalCareCenterId
        End Get
        Set(ByVal value As ExternalCareCenterXpo)
            SetPropertyValue(Of ExternalCareCenterXpo)("ExternalCareCenterId", fExternalCareCenterId, value)
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

End Class