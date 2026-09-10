'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.AccountingRepository
' Author           : Juan F. Tamayo
' Created          : 2014-01-19
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo

#End Region

''' <summary>
''' Tipo de documento usado en los servicios Xpo
''' </summary>
<Persistent("GeneralLedger.MainAccountLevels")> _
Public Class AccountLevelXpo
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
    Dim fLevel As Integer
    <Persistent("Level")> _
    Public Property Level() As Integer
        Get
            Return fLevel
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Level", fLevel, value)
        End Set
    End Property
    Dim fLenght As String
    <Persistent("Length")>
    Public Property Length() As Integer
        Get
            Return fLenght
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Length", fLenght, value)
        End Set
    End Property
    <Association("PUCXpoReferenceAccountLevelXpo")>
    Public ReadOnly Property PUCServiceXpo() As XPCollection(Of PUCServiceXpo)
        Get
            Return GetCollection(Of PUCServiceXpo)("PUCServiceXpo")
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
