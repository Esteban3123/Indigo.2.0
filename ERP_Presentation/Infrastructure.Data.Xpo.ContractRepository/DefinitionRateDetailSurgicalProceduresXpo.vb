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
<Persistent("Contract.DefinitionRateDetailSurgicalProcedures")>
Public Class DefinitionRateDetailSurgicalProceduresXpo
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

    Dim fDefinitionRateDetailId As DefinitionRateDetailXpo
    <Association("DefinitionRateDetailSurgicalReferencesDefinitionRateDetail")>
    Public Property DefinitionRateDetailId() As DefinitionRateDetailXpo
        Get
            Return fDefinitionRateDetailId
        End Get
        Set(ByVal value As DefinitionRateDetailXpo)
            SetPropertyValue(Of DefinitionRateDetailXpo)("DefinitionRateDetailId", fDefinitionRateDetailId, value)
        End Set
    End Property

    Dim fSurgicalProcedureServiceId As Integer
    Public Property SurgicalProcedureServiceId() As Integer
        Get
            Return fSurgicalProcedureServiceId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("SurgicalProcedureServiceId", fSurgicalProcedureServiceId, value)
        End Set
    End Property

    Dim fIPSServiceId As ContractIPSServiceXPO
    <Association("DefinitionRateDetailSurgicalReferencesIPSService")>
    Public Property IPSServiceId() As ContractIPSServiceXPO
        Get
            Return fIPSServiceId
        End Get
        Set(ByVal value As ContractIPSServiceXPO)
            SetPropertyValue(Of ContractIPSServiceXPO)("IPSServiceId", fIPSServiceId, value)
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
