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
<Persistent("Contract.ViewListContractsSearchLookUp")>
Public Class ViewListContractsSearchLookUpXpo
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

    Dim fCode As String
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fContractName As String
    Public Property ContractName() As String
        Get
            Return fContractName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ContractName", fContractName, value)
        End Set
    End Property

    Dim fContractNumber As String
    Public Property ContractNumber() As String
        Get
            Return fContractNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ContractNumber", fContractNumber, value)
        End Set
    End Property

    Dim fContractObject As String
    Public Property ContractObject() As String
        Get
            Return fContractObject
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ContractObject", fContractObject, value)
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

    Dim fCodeContractName As String
    Public Property CodeContractName() As String
        Get
            Return fCodeContractName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeContractName", fCodeContractName, value)
        End Set
    End Property

    Dim fHealthAdministratorId As Integer
    Public Property HealthAdministratorId() As Integer
        Get
            Return fHealthAdministratorId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("HealthAdministratorId", fHealthAdministratorId, value)
        End Set
    End Property

    Dim fHealthAdministratorCodeName As String
    Public Property HealthAdministratorCodeName() As String
        Get
            Return fHealthAdministratorCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("HealthAdministratorCodeName", fHealthAdministratorCodeName, value)
        End Set
    End Property

    Dim fStatusName As String
    Public Property StatusName() As String
        Get
            Return fStatusName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("StatusName", fStatusName, value)
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
