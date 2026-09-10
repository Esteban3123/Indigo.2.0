Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Portfolio.ProvisionRanges")> _
Public Class PortfolioProvisionRangesXpo
    Inherits XPLiteObject

    Dim fId As Integer
    <Key(True)> _
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property
    Dim fCode As String
    <Size(20)> _
    <Persistent("Code")> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fInitial As Integer
    <Persistent("Initial")> _
    Public Property Initial() As Integer
        Get
            Return fInitial
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Initial", fInitial, value)
        End Set
    End Property
    Dim fFinal As Integer
    <Persistent("Final")> _
    Public Property Final() As Integer
        Get
            Return fFinal
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Final", fFinal, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
