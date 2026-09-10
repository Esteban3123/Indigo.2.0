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
<Persistent("Contract.ViewListDefinitionRateDetailSurgicalProcedures")>
Public Class ViewListDefinitionRateDetailSurgicalProceduresXpo
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

    Dim fDefinitionRateDetailId As Integer
    Public Property DefinitionRateDetailId() As Integer
        Get
            Return fDefinitionRateDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("DefinitionRateDetailId", fDefinitionRateDetailId, value)
        End Set
    End Property

    Dim fIPSServiceId As Integer
    Public Property IPSServiceId() As Integer
        Get
            Return fIPSServiceId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IPSServiceId", fIPSServiceId, value)
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

    Dim fIPSServiceCode As String
    Public Property IPSServiceCode() As String
        Get
            Return fIPSServiceCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IPSServiceCode", fIPSServiceCode, value)
        End Set
    End Property

    Dim fIPSServiceName As String
    Public Property IPSServiceName() As String
        Get
            Return fIPSServiceName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IPSServiceName", fIPSServiceName, value)
        End Set
    End Property

    Dim fServiceClassName As String
    Public Property ServiceClassName() As String
        Get
            Return fServiceClassName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ServiceClassName", fServiceClassName, value)
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
