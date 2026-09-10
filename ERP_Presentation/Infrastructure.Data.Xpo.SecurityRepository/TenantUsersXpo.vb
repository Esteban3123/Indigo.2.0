Imports System
Imports DevExpress.Xpo

<Persistent("Security.ViewTenantUsers")>
Partial Public Class TenantUsersXpo
    Inherits XPLiteObject
    <Key(), Persistent()>
    Public Property Key As TenantUserKey
    Dim fId As Short
    '<Key(True)> generaria error, se devuelve como id el id de tenant
    Public Property Id() As Short
        Get
            Return fId
        End Get
        Set(ByVal value As Short)
            SetPropertyValue(Of Short)("Id", fId, value)
        End Set
    End Property
    Dim fUserId As Integer
    Public Property UserId() As Integer
        Get
            Return fUserId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("UserId", fUserId, value)
        End Set
    End Property
    Dim fName As String
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property
    Private fManageCompany As Boolean
    Public Property ManageCompany() As Boolean
        Get
            Return fManageCompany
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of String)("ManageCompany", fManageCompany, value)
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

Public Structure TenantUserKey
    <Persistent("Id")>
    Public Property Id As Short

    <Persistent("UserId")>
    Public Property UserId As Integer
End Structure
