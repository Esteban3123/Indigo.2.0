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
<Persistent("Contract.Contract")> _
Public Class ContractXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As Integer
    <Key(True)> _
    <Persistent("Id")> _
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fCode As String
    <Persistent("Code")> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fContractName As String
    <Persistent("ContractName")> _
    Public Property ContractName() As String
        Get
            Return fContractName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ContractName", fContractName, value)
        End Set
    End Property

    Dim fContractNumber As String
    <Persistent("ContractNumber")> _
    Public Property ContractNumber() As String
        Get
            Return fContractNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ContractNumber", fContractNumber, value)
        End Set
    End Property

    Dim fContractObject As String
    <Persistent("ContractObject")> _
    Public Property ContractObject() As String
        Get
            Return fContractObject
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ContractObject", fContractObject, value)
        End Set
    End Property

    Dim fStatus As Integer
    <Persistent("Status")> _
    Public Property Status() As Integer
        Get
            Return fStatus
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Status", fStatus, value)
        End Set
    End Property

    <PersistentAlias("concat(Code,' - ',ContractName)")>
    Public ReadOnly Property CodeContractName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeContractName"))
        End Get
    End Property

    Dim fHealthAdministratorId As HealthAdministratorXpo
    <Association("ContractReferencesHealthAdministrator")> _
    Public Property HealthAdministratorId() As HealthAdministratorXpo
        Get
            Return fHealthAdministratorId
        End Get
        Set(ByVal value As HealthAdministratorXpo)
            SetPropertyValue(Of HealthAdministratorXpo)("HealthAdministratorId", fHealthAdministratorId, value)
        End Set
    End Property

    <PersistentAlias("Iif(Status = 1, 'Activo', Iif(Status = 2, 'Suspendido',Iif(Status = 3, 'Terminado', '')))")>
    Public ReadOnly Property StatusName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

    <Association("ContractDetailReferencesContract", GetType(ContractDetailXpo))>
    Public ReadOnly Property ContractDetailXpo() As XPCollection(Of ContractDetailXpo)
        Get
            Return GetCollection(Of ContractDetailXpo)("ContractDetailXpo")
        End Get
    End Property


    <Association("Contract_CareGroupReferencesContractXpo", GetType(ContractCareGroupXpo))>
    Public ReadOnly Property ContractCareGroupXpo() As XPCollection(Of ContractCareGroupXpo)
        Get
            Return GetCollection(Of ContractCareGroupXpo)("ContractCareGroupXpo")
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
