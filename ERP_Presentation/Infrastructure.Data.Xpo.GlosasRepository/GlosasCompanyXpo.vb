'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.GlosasRepository
' Author           : Juan Diego Diaz
' Created          : 08-04-2013
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"
Imports System
Imports DevExpress.Xpo
#End Region

''' <summary>
''' Clase compañia para servicios Xpo.
''' </summary>
<Persistent("Common.Company")> _
Public Class GlosasCompanyXpo
    Inherits XPLiteObject
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
    Dim fContainerName As String
    <Size(50)> _
    Public Property ContainerName() As String
        Get
            Return fContainerName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ContainerName", fContainerName, value)
        End Set
    End Property
    Dim fState As Boolean
    Public Property State() As Boolean
        Get
            Return fState
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("State", fState, value)
        End Set
    End Property

#Region "Constructores"

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
