'**********************************************************************
' Assembly         : Infrastructure.Data.Xpo.SecurityRepository
' Author           : Hector Rodriguez Rubiano
' Created          : 15-04-2021
'
' Description      : Xpo para TenantRoll
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports DevExpress.Xpo

<Persistent("Security.ViewTenantRoll")>
Partial Public Class TenantRollXpo
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
    Dim fRollCode As String
    Public Property RollCode() As String
        Get
            Return fRollCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RollCode", fRollCode, value)
        End Set
    End Property
    Dim fRollName As String
    Public Property RollName() As String
        Get
            Return fRollName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RollName", fRollName, value)
        End Set
    End Property
    Dim fRollType As Byte
    Public Property RollType() As Byte
        Get
            Return fRollType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("RollType", fRollType, value)
        End Set
    End Property
    <PersistentAlias("concat(concat(RollCode,' - '),RollName)")>
    Public ReadOnly Property RollCodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("RollCodeName"))
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
