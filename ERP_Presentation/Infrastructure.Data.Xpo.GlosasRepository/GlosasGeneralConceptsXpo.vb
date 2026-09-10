Imports System
Imports DevExpress.Xpo

<Persistent("Glosas.GeneralConcept")> _
Public Class GlosasGeneralConceptsXpo
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

    Dim fCode As Integer
    <Persistent("Code")> _
    Public Property Codigo() As Integer
        Get
            Return fCode
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Code", fCode, value)
        End Set
    End Property

    Dim fName As String
    <Size(50)> _
    <Persistent("Name")> _
    Public Property Descripcion() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property

    Dim fType As String
    <Size(1)> _
    <Persistent("Type")> _
    Public Property Type() As String
        Get
            Return fType
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Type", fType, value)
        End Set
    End Property

    Dim fApplication As String
    <Size(100)> _
    <Persistent("Application")> _
    Public Property Application() As String
        Get
            Return fApplication
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Application", fApplication, value)
        End Set
    End Property

    Dim fConceptType As String
    <Size(100)> _
    <Persistent("ConceptType")> _
    Public Property ConceptType() As String
        Get
            Return fConceptType
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ConceptType", fConceptType, value)
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
