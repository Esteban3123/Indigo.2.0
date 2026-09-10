'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Angi Duran V.
' Created          : 2022-09-26
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports DevExpress.Xpo

<Persistent("MixingStation.ViewListUserByContainerCM")>
Partial Public Class ViewListUserByContainerCM
    Inherits XPLiteObject

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

    Dim fId As Long
    <Key(True)>
    Public Property Id() As Long
        Get
            Return fId
        End Get
        Set(ByVal value As Long)
            SetPropertyValue(Of Long)("Id", fId, value)
        End Set
    End Property


    Dim fIdPerson As Integer
    Public Property IdPerson() As Integer
        Get
            Return fIdPerson
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdPerson", fIdPerson, value)
        End Set
    End Property

    Dim fUserCode As String
    Public Property UserCode() As String
        Get
            Return fUserCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UserCode", fUserCode, value)
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

    Dim fFullName As String
    Public Property FullName() As String
        Get
            Return fFullName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FullName", fFullName, value)
        End Set
    End Property

    Dim fPositionName As String
    Public Property PositionName() As String
        Get
            Return fPositionName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PositionName", fPositionName, value)
        End Set
    End Property

    Dim fTenantId As Integer
    Public Property TenantId() As Integer
        Get
            Return fTenantId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("TenantId", fTenantId, value)
        End Set
    End Property

    Dim fIdContainer As Integer
    Public Property IdContainer() As Integer
        Get
            Return fIdContainer
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdContainer", fIdContainer, value)
        End Set
    End Property

#Region "Builders"
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
#End Region

End Class