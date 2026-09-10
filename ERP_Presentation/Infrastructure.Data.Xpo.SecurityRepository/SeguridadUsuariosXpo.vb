Imports System
Imports DevExpress.Xpo

'<Persistent("Security.User")> _
'Public Class SeguridadUsuariosXpo
'    Inherits XPLiteObject

'    Dim f_Id As Integer

'    <Key(True)> _
'    <Persistent("Id")> _
'    Public Property Id() As Integer
'        Get
'            Return f_Id
'        End Get
'        Set(ByVal value As Integer)
'            SetPropertyValue(Of Integer)("Id", f_Id, value)
'        End Set
'    End Property

'    Dim fIdPerson As Integer

'    Public Property IdPerson() As Integer
'        Get
'            Return fIdPerson
'        End Get
'        Set(ByVal value As Integer)
'            SetPropertyValue(Of Integer)("IdPerson", fIdPerson, value)
'        End Set
'    End Property

'    Dim fCodeUser As String

'    <Size(20)> _
'        <Persistent("UserCode")> _
'    Public Property UserCode() As String
'        Get
'            Return fCodeUser
'        End Get
'        Set(ByVal value As String)
'            SetPropertyValue(Of String)("UserCode", fCodeUser, value)
'        End Set
'    End Property

'    Dim fState As Boolean

'    <Persistent("State")> _
'    Public Property State() As Boolean
'        Get
'            Return fState
'        End Get
'        Set(ByVal value As Boolean)
'            SetPropertyValue(Of Boolean)("State", fState, value)
'        End Set
'    End Property

'    Public Sub New(ByVal session As Session)
'        MyBase.New(session)
'    End Sub

'    Public Sub New()
'        MyBase.New(Session.DefaultSession)
'    End Sub

'    Public Overrides Sub AfterConstruction()
'        MyBase.AfterConstruction()
'    End Sub


<Persistent("Security.User")> _
Partial Public Class Security_Users
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
    Dim fUserCode As String
    <Size(20)> _
    Public Property UserCode() As String
        Get
            Return fUserCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UserCode", fUserCode, value)
        End Set
    End Property
    Dim fIdPerson As Security_Person
    <Association("Security_UserReferencesSecurity_Person")> _
    Public Property IdPerson() As Security_Person
        Get
            Return fIdPerson
        End Get
        Set(ByVal value As Security_Person)
            SetPropertyValue(Of Security_Person)("IdPerson", fIdPerson, value)
        End Set
    End Property
    Dim fPosition As String
    Public Property Position() As String
        Get
            Return fPosition
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Position", fPosition, value)
        End Set
    End Property
    Dim fStateActive As Boolean
    <Persistent("State")> _
    Public Property StateActive() As Boolean
        Get
            Return fStateActive
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("StateActive", fStateActive, value)
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
