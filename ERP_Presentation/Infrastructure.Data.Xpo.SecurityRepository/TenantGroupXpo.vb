'**********************************************************************
' Assembly         : Infrastructure.Data.Xpo.SecurityRepository
' Author           : Hector Rodriguez Rubiano
' Created          : 15-04-2021
'
' Description      : Xpo para TenantGroup
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports DevExpress.Xpo

<Persistent("Security.ViewTenantGroup")>
Partial Public Class TenantGroupXpo
    Inherits XPLiteObject
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
    Dim fTenantId As Short
    Public Property TenantId() As Short
        Get
            Return fTenantId
        End Get
        Set(ByVal value As Short)
            SetPropertyValue(Of Short)("TenantId", fTenantId, value)
        End Set
    End Property
    Dim fGroupCode As String
    Public Property GroupCode() As String
        Get
            Return fGroupCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("GroupCode", fGroupCode, value)
        End Set
    End Property
    Dim fGroupName As String
    Public Property GroupName() As String
        Get
            Return fGroupName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("GroupName", fGroupName, value)
        End Set
    End Property
    Dim fGroupType As Byte
    Public Property GroupType() As Byte
        Get
            Return fGroupType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("GroupType", fGroupType, value)
        End Set
    End Property
    Dim fGroupState As Boolean
    Public Property GroupState() As Boolean
        Get
            Return fGroupState
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("GroupState", fGroupState, value)
        End Set
    End Property
    <PersistentAlias("concat(concat(GroupCode,' - '),GroupName)")>
    Public ReadOnly Property GroupCodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("GroupCodeName"))
        End Get
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
