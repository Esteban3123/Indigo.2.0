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
<Persistent("Authorization.AuthorizationScheduleTemplateSchedule")>
Public Class AuthorizationScheduleTemplateScheduleXpo
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

    Dim fAuthorizationScheduleTemplateId As AuthorizationScheduleTemplateXpo
    <Association("ScheduleTemplateScheduleReferencesScheduleTemplate")>
    Public Property AuthorizationScheduleTemplateId() As AuthorizationScheduleTemplateXpo
        Get
            Return fAuthorizationScheduleTemplateId
        End Get
        Set(ByVal value As AuthorizationScheduleTemplateXpo)
            SetPropertyValue(Of AuthorizationScheduleTemplateXpo)("AuthorizationScheduleTemplateId", fAuthorizationScheduleTemplateId, value)
        End Set
    End Property

    Dim fNextDay As Boolean
    Public Property NextDay() As Boolean
        Get
            Return fNextDay
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("NextDay", fNextDay, value)
        End Set
    End Property

    Dim fInitialTime As TimeSpan
    Public Property InitialTime() As TimeSpan
        Get
            Return fInitialTime
        End Get
        Set(ByVal value As TimeSpan)
            SetPropertyValue(Of TimeSpan)("InitialTime", fInitialTime, value)
        End Set
    End Property

    Dim fEndingTime As TimeSpan
    Public Property EndingTime() As TimeSpan
        Get
            Return fEndingTime
        End Get
        Set(ByVal value As TimeSpan)
            SetPropertyValue(Of TimeSpan)("EndingTime", fEndingTime, value)
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
