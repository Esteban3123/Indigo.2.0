'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.ContractRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 24/09/2014
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
<Persistent("Authorization.ViewListScheduleTemplateUsers")>
Public Class ViewListScheduleTemplateUsersXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fUserId As Integer
    <Key(True)>
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

    Dim fFullName As String
    Public Property FullName() As String
        Get
            Return fFullName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FullName", fFullName, value)
        End Set
    End Property

    Dim fUserCodeName As String
    Public Property UserCodeName() As String
        Get
            Return fUserCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UserCodeName", fUserCodeName, value)
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
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

End Class
