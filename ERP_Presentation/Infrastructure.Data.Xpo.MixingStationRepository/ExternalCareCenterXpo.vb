'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Yoe Andres Cardenas
' Created          : 16-04-2019
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("MixingStation.ExternalCareCenter")>
Partial Public Class ExternalCareCenterXpo
    Inherits XPLiteObject

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

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

    Dim fDescription As String
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property

    Dim fCustomerId As Integer
    Public Property CustomerId() As Integer
        Get
            Return fCustomerId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CustomerId", fCustomerId, value)
        End Set
    End Property

    Dim fStatus As Boolean
    Public Property Status() As Boolean
        Get
            Return fStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Status", fStatus, value)
        End Set
    End Property

    <PersistentAlias("Iif(Status = 1, 'Activo', 'Inactivo')")>
    Public ReadOnly Property StatusName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

    <PersistentAlias("concat(concat(Code,' - '),Description)")>
    Public ReadOnly Property CodeDescription() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeDescription"))
        End Get
    End Property

    Dim fContractExternalClients As ContractExternalClientsXpo
    <Persistent("ContractExternalClientsId")>
    <Association("ContractExternalClientsReferencesExternalCareCenter")>
    Public Property ContractExternalClients() As ContractExternalClientsXpo
        Get
            Return fContractExternalClients
        End Get
        Set(ByVal value As ContractExternalClientsXpo)
            SetPropertyValue(Of ContractExternalClientsXpo)("ContractExternalClientsId", fContractExternalClients, value)
        End Set
    End Property

    <Association("ExternalCareCenterUsersReferencesExternalCareCenter", GetType(ExternalCareCenterUsersXpo))>
    Public ReadOnly Property ExternalCareCenterUsersXpo() As XPCollection(Of ExternalCareCenterUsersXpo)
        Get
            Return GetCollection(Of ExternalCareCenterUsersXpo)("ExternalCareCenterUsersXpo")
        End Get
    End Property

    <Association("RequestUnitDoseExternalCareCenterReferencesExternalCareCenter", GetType(RequestUnitDoseExternalCareCenterXpo))>
    Public ReadOnly Property RequestUnitDoseExternalCareCenterXpo() As XPCollection(Of RequestUnitDoseExternalCareCenterXpo)
        Get
            Return GetCollection(Of RequestUnitDoseExternalCareCenterXpo)("RequestUnitDoseExternalCareCenterXpo")
        End Get
    End Property

    <Association("CMExternalCareCenterReferencesExternalCareCenter", GetType(CMExternalCareCenterXpo))>
    Public ReadOnly Property CMExternalCareCenterXpo() As XPCollection(Of CMExternalCareCenterXpo)
        Get
            Return GetCollection(Of CMExternalCareCenterXpo)("CMExternalCareCenterXpo")
        End Get
    End Property
End Class