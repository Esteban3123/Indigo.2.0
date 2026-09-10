#Region "Imports"
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
#End Region
<Persistent("FixedAsset.Group")> _
Public Class GroupXpo
    Inherits XPLiteObject

    Dim _id As Integer
    <Key(True)>
    <Persistent("Id")> _
    Public Property Id() As Integer
        Get
            Return _id
        End Get
        Set(value As Integer)
            SetPropertyValue(Of Integer)("Id", _id, value)
        End Set
    End Property

    Dim _Code As String
    <Persistent("Code")> _
    Public Property Code() As String
        Get
            Return _Code
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("Code", _Code, value)
        End Set
    End Property

    Dim _Description As String
    <Persistent("Description")> _
    Public Property Description() As String
        Get
            Return _Description
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("Description", _Description, value)
        End Set
    End Property

    Dim _IdBudgetEntry As Integer
    <Persistent("IdBudgetEntry")> _
    Public Property IdBudgetEntry As Integer
        Get
            Return _IdBudgetEntry
        End Get
        Set(value As Integer)
            SetPropertyValue(Of Integer)("IdBudgetEntry", _IdBudgetEntry, value)
        End Set
    End Property

    Dim _Status As Boolean
    <Persistent("Status")> _
    Public Property Status As Boolean
        Get
            Return _Status
        End Get
        Set(value As Boolean)
            SetPropertyValue(Of Integer)("Status", _Status, value)
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
