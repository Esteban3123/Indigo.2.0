Imports System
Imports DevExpress.Xpo

<Persistent("Glosas.Responsible")> _
Public Class GlosasResponsibleXpo
    Inherits XPLiteObject

    Dim f_Id As Integer
    <Key(True)> _
    <Persistent("Id")> _
    Public Property Id() As Integer
        Get
            Return f_Id
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("_Id", f_Id, value)
        End Set
    End Property

    Dim fCode As String
    <Size(2)> _
    <Persistent("Code")> _
    Public Property Codigo() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fName As String
    <Size(40)> _
    <Persistent("Name")> _
    Public Property Descripcion() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property

    Dim fCodeUser As String
    <Size(3)> _
    <Persistent("CodeUser")> _
    Public Property CodeUser() As String
        Get
            Return fCodeUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeUser", fCodeUser, value)
        End Set
    End Property

    Dim fCharge As String
    <Size(60)> _
    <Persistent("Charge")> _
    Public Property Charge() As String
        Get
            Return fCharge
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Charge", fCharge, value)
        End Set
    End Property

    Dim fState As Boolean
    <Persistent("State")> _
    Public Property State() As Boolean
        Get
            Return fState
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("State", fState, value)
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
