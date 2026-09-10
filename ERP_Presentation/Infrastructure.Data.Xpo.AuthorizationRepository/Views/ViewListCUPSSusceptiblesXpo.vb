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
<Persistent("Authorization.ViewListCUPSSusceptibles")>
Public Class ViewListCUPSSusceptiblesXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As String
    <Key(True)>
    Public Property Id() As String
        Get
            Return fId
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Id", fId, value)
        End Set
    End Property

    Dim fCUPSEntityId As Integer
    Public Property CUPSEntityId() As Integer
        Get
            Return fCUPSEntityId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CUPSEntityId", fCUPSEntityId, value)
        End Set
    End Property

    Dim fCUPSEntityCode As String
    Public Property CUPSEntityCode() As String
        Get
            Return fCUPSEntityCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CUPSEntityCode", fCUPSEntityCode, value)
        End Set
    End Property

    Dim fCUPSEntityName As String
    Public Property CUPSEntityName() As String
        Get
            Return fCUPSEntityName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CUPSEntityName", fCUPSEntityName, value)
        End Set
    End Property

    Dim fCUPSEntityCodeName As String
    Public Property CUPSEntityCodeName() As String
        Get
            Return fCUPSEntityCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CUPSEntityCodeName", fCUPSEntityCodeName, value)
        End Set
    End Property

    Dim fCareCenterCode As String
    Public Property CareCenterCode() As String
        Get
            Return fCareCenterCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CareCenterCode", fCareCenterCode, value)
        End Set
    End Property

    Dim fCareGroupId As Integer
    Public Property CareGroupId() As Integer
        Get
            Return fCareGroupId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CareGroupId", fCareGroupId, value)
        End Set
    End Property

    Dim fSusceptibleAuthorization As Boolean
    Public Property SusceptibleAuthorization() As Boolean
        Get
            Return fSusceptibleAuthorization
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("SusceptibleAuthorization", fSusceptibleAuthorization, value)
        End Set
    End Property

    Dim fContractDescriptionId As Integer?
    Public Property ContractDescriptionId() As Integer?
        Get
            Return fContractDescriptionId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("ContractDescriptionId", fContractDescriptionId, value)
        End Set
    End Property

    Dim fContractDescriptionCodeName As String
    Public Property ContractDescriptionCodeName() As String
        Get
            Return fContractDescriptionCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ContractDescriptionCodeName", fContractDescriptionCodeName, value)
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
