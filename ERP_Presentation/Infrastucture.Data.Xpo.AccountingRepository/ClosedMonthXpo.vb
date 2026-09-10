'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.ClosedMonth
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 07-02-2014
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"
Imports System
Imports DevExpress.Xpo
#End Region

''' <summary>
''' cierre de mes usado en los servicios Xpo
''' </summary>
<Persistent("GeneralLedger.ClosedMonth")> _
Public Class ClosedMonthXpo
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
    Dim fYear As Int64

    <Persistent("Year")> _
    Public Property Year() As Int64
        Get
            Return fYear
        End Get
        Set(ByVal value As Int64)
            SetPropertyValue(Of Int64)("Year", fYear, value)
        End Set
    End Property

    Dim fMonth As Integer
    <Persistent("Month")> _
    Public Property Month() As Integer
        Get
            Return fMonth
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Month", fMonth, value)
        End Set
    End Property

    Dim fStatus As Boolean
    <Persistent("Status")> _
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
