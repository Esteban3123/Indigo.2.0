'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.ContractRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 24/09/2014
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo

#End Region

''' <summary>
''' conceptos de nota usado en los servicios Xpo
''' </summary>
<Persistent("Contract.ContractDetailNovelty")>
Public Class ContractDetailNoveltyXpo
    Inherits XPLiteObject

#Region "Members"

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

    Dim fContractDetailId As ContractDetailXpo
    <Association("ContractDetailNoveltyReferencesContractDetail")>
    Public Property ContractDetailId() As ContractDetailXpo
        Get
            Return fContractDetailId
        End Get
        Set(ByVal value As ContractDetailXpo)
            SetPropertyValue(Of ContractDetailXpo)("ContractDetailId", fContractDetailId, value)
        End Set
    End Property

    Dim fNoveltyDate As DateTime
    Public Property NoveltyDate() As DateTime
        Get
            Return fNoveltyDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("NoveltyDate", fNoveltyDate, value)
        End Set
    End Property

    Dim fNoveltySource As Integer
    Public Property NoveltySource() As Integer
        Get
            Return fNoveltySource
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("NoveltySource", fNoveltySource, value)
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

    Dim fDescription As String
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property

    Dim fStatus As Integer
    Public Property Status() As Integer
        Get
            Return fStatus
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Status", fStatus, value)
        End Set
    End Property

#End Region

#Region "Builders"

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
