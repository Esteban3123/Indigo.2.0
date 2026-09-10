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
<Persistent("Contract.SurgicalProcedureService")>
Public Class SurgicalProcedureServiceXpo
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

    Dim fIPSServiceParentId As Integer
    Public Property IPSServiceParentId() As Integer
        Get
            Return fIPSServiceParentId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IPSServiceParentId", fIPSServiceParentId, value)
        End Set
    End Property

    Dim fIPSServiceId As ContractIPSServiceXPO
    <Association("SurgicalProceduresServiceReferencesIPSService")>
    Public Property IPSServiceId() As ContractIPSServiceXPO
        Get
            Return fIPSServiceId
        End Get
        Set(ByVal value As ContractIPSServiceXPO)
            SetPropertyValue(Of ContractIPSServiceXPO)("IPSServiceId", fIPSServiceId, value)
        End Set
    End Property

    Dim fServiceAmount As Integer
    Public Property ServiceAmount() As Integer
        Get
            Return fServiceAmount
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ServiceAmount", fServiceAmount, value)
        End Set
    End Property

    Dim fDefaultService As Boolean
    Public Property DefaultService() As Boolean
        Get
            Return fDefaultService
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("DefaultService", fDefaultService, value)
        End Set
    End Property

    Dim fPerformsHealthProfessionalCode As String
    Public Property PerformsHealthProfessionalCode() As String
        Get
            Return fPerformsHealthProfessionalCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PerformsHealthProfessionalCode", fPerformsHealthProfessionalCode, value)
        End Set
    End Property

    Dim fSelectOption As Boolean
    <NonPersistent()>
    Public Property SelectOption As Boolean
        Get
            Return fSelectOption
        End Get
        Set(value As Boolean)
            fSelectOption = value
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
