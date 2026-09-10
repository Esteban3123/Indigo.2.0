'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.BillingRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 27/11/2014
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo

#End Region

<Persistent("Billing.ViewRIASCups")>
Public Class ViewRIASCupsXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fRiasCupsId As Integer
    <Key(True)>
    Public Property RiasCupsId() As Integer
        Get
            Return fRiasCupsId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RiasCupsId", fRiasCupsId, value)
        End Set
    End Property

    Dim fRiasId As Integer
    Public Property RiasId() As Integer
        Get
            Return fRiasId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RiasId", fRiasId, value)
        End Set
    End Property

    Dim fRiasCode As String
    Public Property RiasCode() As String
        Get
            Return fRiasCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RiasCode", fRiasCode, value)
        End Set
    End Property

    Dim fRiasName As String
    Public Property RiasName() As String
        Get
            Return fRiasName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RiasName", fRiasName, value)
        End Set
    End Property

    Dim fRiasCodeName As String
    Public Property RiasCodeName() As String
        Get
            Return fRiasCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RiasCodeName", fRiasCodeName, value)
        End Set
    End Property

    Dim fRiasStatus As Integer
    Public Property RiasStatus() As Integer
        Get
            Return fRiasStatus
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RiasStatus", fRiasStatus, value)
        End Set
    End Property

    Dim fCupsCode As String
    Public Property CupsCode() As String
        Get
            Return fCupsCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CupsCode", fCupsCode, value)
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
