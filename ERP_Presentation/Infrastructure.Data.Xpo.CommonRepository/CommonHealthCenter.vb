'************************************************************
' Assembly         : Infraestructure.Data.Xpo.CommonRepository
' Author           : Juan Diego Diaz
' Created          : 11-04-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

#End Region

''' <summary>
''' Clase Centro de Atención para servicios Xpo.
''' </summary>
''' <remarks></remarks>
<Persistent("Common.HealthCenter")> _
Partial Public Class CommonHealthCenter
    Inherits XPLiteObject
    Dim fId As Byte
    <Key()> _
    <Persistent("Id")> _
    Public Property Id() As Byte
        Get
            Return fId
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Id", fId, value)
        End Set
    End Property
    Dim fCode As String
    <Size(3)> _
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
    <Persistent("Name")> _
    Public Property Descripcion() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
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