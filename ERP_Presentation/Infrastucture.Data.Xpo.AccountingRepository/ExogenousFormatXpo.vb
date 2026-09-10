'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.AccountingRepository
' Author           : Miguel Angel Fonseca Castro
' Created          : 2019-01-09
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo

#End Region

''' <summary>
''' Formatos de exógena usado en los servicios Xpo
''' </summary>
<Persistent("GeneralLedger.ExogenousFormat")>
Public Class ExogenousFormatXpo
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

    Dim fCode As String
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fFormat As Integer
    Public Property Format() As Integer
        Get
            Return fFormat
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Format", fFormat, value)
        End Set
    End Property

    Dim fVersion As String
    Public Property Version() As Integer
        Get
            Return fVersion
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Version", fVersion, value)
        End Set
    End Property

    Dim fStatus As Boolean
    Public Property Status As Boolean
        Get
            Return fStatus
        End Get
        Set(value As Boolean)
            SetPropertyValue(Of Boolean)("Status", fStatus, value)
        End Set
    End Property

#End Region

#Region "Custom Members"

    <PersistentAlias("Iif(Status = 1, 'Activo', 'Inactivo')")>
    Public ReadOnly Property StatusName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
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
