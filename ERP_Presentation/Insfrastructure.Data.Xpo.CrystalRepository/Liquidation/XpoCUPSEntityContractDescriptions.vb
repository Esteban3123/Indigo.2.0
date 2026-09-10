'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.ContractRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 11/12/2019
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
<Persistent("Contract.CUPSEntityContractDescriptions")>
Public Class XpoCUPSEntityContractDescriptions
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

    Dim fCUPSEntityId As Integer
    Public Property CUPSEntityId() As Integer
        Get
            Return fCUPSEntityId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CUPSEntityId", fCUPSEntityId, value)
        End Set
    End Property

    Dim fContractDescriptionId As Integer
    Public Property ContractDescriptionId() As Integer
        Get
            Return fContractDescriptionId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ContractDescriptionId", fContractDescriptionId, value)
        End Set
    End Property

    Dim fIsDelete As Boolean
    Public Property IsDelete() As Boolean
        Get
            Return fIsDelete
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsDelete", fIsDelete, value)
        End Set
    End Property

#End Region
#Region "Association"
    <Association("AppointmentHemocomponentsCUPSXpo_Reference", GetType(AppointmentHemocomponentsCUPSXpo))>
    Public ReadOnly Property AppointmentHemocomponentsCUPSXpo() As XPCollection(Of AppointmentHemocomponentsCUPSXpo)
        Get
            Return GetCollection(Of AppointmentHemocomponentsCUPSXpo)("AppointmentHemocomponentsCUPSXpo")
        End Get
    End Property

    <Association("HCCOMSANDXpo_Reference", GetType(HCCOMSANDXpo))>
    Public ReadOnly Property HCCOMSANDXpo() As XPCollection(Of HCCOMSANDXpo)
        Get
            Return GetCollection(Of HCCOMSANDXpo)("HCCOMSANDXpo")
        End Get
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
