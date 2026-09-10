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
<Persistent("Authorization.AuthorizationScheduleDetail")>
Public Class AuthorizationScheduleDetailXpo
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

    Dim fAuthorizationScheduleId As AuthorizationScheduleXpo
    <Association("ScheduleDetailReferencesSchedule")>
    Public Property AuthorizationScheduleId() As AuthorizationScheduleXpo
        Get
            Return fAuthorizationScheduleId
        End Get
        Set(ByVal value As AuthorizationScheduleXpo)
            SetPropertyValue(Of AuthorizationScheduleXpo)("AuthorizationScheduleId", fAuthorizationScheduleId, value)
        End Set
    End Property

    Dim fSchedule As Integer
    Public Property Schedule() As Integer
        Get
            Return fSchedule
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Schedule", fSchedule, value)
        End Set
    End Property

    Dim fDay As Integer
    Public Property Day() As Integer
        Get
            Return fDay
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Day", fDay, value)
        End Set
    End Property

    Dim fNumberHour As Decimal
    Public Property NumberHour() As Decimal
        Get
            Return fNumberHour
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("NumberHour", fNumberHour, value)
        End Set
    End Property

    Dim fStatus As Boolean
    Public Property Status() As Boolean
        Get
            Return fStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Status", fStatus, value)
        End Set
    End Property

    <Association("ScheduleDetailHourReferencesScheduleDetail", GetType(AuthorizationScheduleDetailHourXpo))>
    Public ReadOnly Property AuthorizationScheduleDetailHourXpo() As XPCollection(Of AuthorizationScheduleDetailHourXpo)
        Get
            Return GetCollection(Of AuthorizationScheduleDetailHourXpo)("AuthorizationScheduleDetailHourXpo")
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
