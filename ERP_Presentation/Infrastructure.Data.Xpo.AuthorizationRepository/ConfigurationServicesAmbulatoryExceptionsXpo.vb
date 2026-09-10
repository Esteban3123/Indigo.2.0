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
<Persistent("Authorization.ConfigurationServicesAmbulatoryExceptions")>
Public Class ConfigurationServicesAmbulatoryExceptionsXpo
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

    Dim fConfigurationServicesAmbulatoryId As ConfigurationServicesAmbulatoryXpo
    <Association("ConfigurationExceptionReferencesConfiguration")>
    Public Property ConfigurationServicesAmbulatoryId() As ConfigurationServicesAmbulatoryXpo
        Get
            Return fConfigurationServicesAmbulatoryId
        End Get
        Set(ByVal value As ConfigurationServicesAmbulatoryXpo)
            SetPropertyValue(Of ConfigurationServicesAmbulatoryXpo)("ConfigurationServicesAmbulatoryId", fConfigurationServicesAmbulatoryId, value)
        End Set
    End Property

    Dim fCareGroupId As AuthorizationCareGroupXpo
    <Association("ConfigurationExceptionReferencesCareGroup")>
    Public Property CareGroupId() As AuthorizationCareGroupXpo
        Get
            Return fCareGroupId
        End Get
        Set(ByVal value As AuthorizationCareGroupXpo)
            SetPropertyValue(Of AuthorizationCareGroupXpo)("CareGroupId", fCareGroupId, value)
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

    Dim fAssignment As Integer
    Public Property Assignment() As Integer
        Get
            Return fAssignment
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Assignment", fAssignment, value)
        End Set
    End Property

    Dim fAssignmentUnit As Integer
    Public Property AssignmentUnit() As Integer
        Get
            Return fAssignmentUnit
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AssignmentUnit", fAssignmentUnit, value)
        End Set
    End Property

    Dim fRequest As Integer
    Public Property Request() As Integer
        Get
            Return fRequest
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Request", fRequest, value)
        End Set
    End Property

    Dim fRequestUnit As Integer
    Public Property RequestUnit() As Integer
        Get
            Return fRequestUnit
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RequestUnit", fRequestUnit, value)
        End Set
    End Property

    Dim fRadicated As Integer
    Public Property Radicated() As Integer
        Get
            Return fRadicated
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Radicated", fRadicated, value)
        End Set
    End Property

    Dim fRadicatedUnit As Integer
    Public Property RadicatedUnit() As Integer
        Get
            Return fRadicatedUnit
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RadicatedUnit", fRadicatedUnit, value)
        End Set
    End Property

    Dim fDeliveryService As Integer
    Public Property DeliveryService() As Integer
        Get
            Return fDeliveryService
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("DeliveryService", fDeliveryService, value)
        End Set
    End Property

    Dim fDeliveryServiceUnit As Integer
    Public Property DeliveryServiceUnit() As Integer
        Get
            Return fDeliveryServiceUnit
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("DeliveryServiceUnit", fDeliveryServiceUnit, value)
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
