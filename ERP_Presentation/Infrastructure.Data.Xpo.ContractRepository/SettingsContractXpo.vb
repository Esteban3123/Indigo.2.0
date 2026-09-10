'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.ContractRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 06/02/2020
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
<Persistent("Contract.SettingsContract")>
Public Class SettingsContractXpo
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

    Dim fOperatingUnitId As Integer
    Public Property OperatingUnitId() As Integer
        Get
            Return fOperatingUnitId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("OperatingUnitId", fOperatingUnitId, value)
        End Set
    End Property

    Dim fCUPSWithRelatedDescription As Boolean
    Public Property CUPSWithRelatedDescription() As Boolean
        Get
            Return fCUPSWithRelatedDescription
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("CUPSWithRelatedDescription", fCUPSWithRelatedDescription, value)
        End Set
    End Property

    Dim fRequestQuoteOutpatientServices As Boolean
    Public Property RequestQuoteOutpatientServices() As Boolean
        Get
            Return fRequestQuoteOutpatientServices
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("RequestQuoteOutpatientServices", fRequestQuoteOutpatientServices, value)
        End Set
    End Property

    Dim fRequestQuoteIntrahospitalServices As Boolean
    Public Property RequestQuoteIntrahospitalServices() As Boolean
        Get
            Return fRequestQuoteIntrahospitalServices
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("RequestQuoteIntrahospitalServices", fRequestQuoteIntrahospitalServices, value)
        End Set
    End Property

    Dim fRequestQuoteOutpatientProducts As Boolean
    Public Property RequestQuoteOutpatientProducts() As Boolean
        Get
            Return fRequestQuoteOutpatientProducts
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("RequestQuoteOutpatientProducts", fRequestQuoteOutpatientProducts, value)
        End Set
    End Property

    Dim fRequestQuoteIntrahospitalProducts As Boolean
    Public Property RequestQuoteIntrahospitalProducts() As Boolean
        Get
            Return fRequestQuoteIntrahospitalProducts
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("RequestQuoteIntrahospitalProducts", fRequestQuoteIntrahospitalProducts, value)
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
