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

<Persistent("MixingStation.ContractExternalClients")>
Partial Public Class ContractExternalClientsXpo
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

    Dim fContractType As Integer
    Public Property ContractType() As Integer
        Get
            Return fContractType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ContractType", fContractType, value)
        End Set
    End Property

    Dim fDocumentDate As DateTime
    Public Property DocumentDate() As DateTime
        Get
            Return fDocumentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DocumentDate", fDocumentDate, value)
        End Set
    End Property

    Dim fInitialDate As DateTime
    Public Property InitialDate() As DateTime
        Get
            Return fInitialDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("InitialDate", fInitialDate, value)
        End Set
    End Property

    Dim fEndDate As DateTime
    Public Property EndDate() As DateTime
        Get
            Return fEndDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("EndDate", fEndDate, value)
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

    Dim fCustomer As CommonCustomerXpo
    <Persistent("CustomerId")>
    <Association("ContractExternalClientsReferencesCustomer")>
    Public Property Customer() As CommonCustomerXpo
        Get
            Return fCustomer
        End Get
        Set(ByVal value As CommonCustomerXpo)
            SetPropertyValue("Customer", fCustomer, value)
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

    <PersistentAlias("Customer.Id")>
    Public ReadOnly Property CustomerId() As Integer
        Get
            Return Convert.ToInt32(EvaluateAlias("CustomerId"))
        End Get
    End Property

    <PersistentAlias("Iif(ContractType = 1, 'Fijo', 'Variable')")>
    Public ReadOnly Property ContractTypeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("ContractTypeName"))
        End Get
    End Property

    <PersistentAlias("Iif(Status = 1, 'Activo', 'Inactivo')")>
    Public ReadOnly Property StatusName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

    <PersistentAlias("concat(concat(Code,' - '),ContractNumber)")>
    Public ReadOnly Property ContractExternalClientsCodeContractNumber() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("ContractExternalClientsCodeContractNumber"))
        End Get
    End Property

    <PersistentAlias("concat(concat(ContractNumber,' - '),Customer.Name)")>
    Public ReadOnly Property ContractExternalClientsNameContractNumber() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("ContractExternalClientsNameContractNumber"))
        End Get
    End Property

    <Association("ContractExternalClientsReferencesExternalCareCenter", GetType(ExternalCareCenterXpo))>
    Public ReadOnly Property ExternalCareCenterXpo() As XPCollection(Of ExternalCareCenterXpo)
        Get
            Return GetCollection(Of ExternalCareCenterXpo)("ExternalCareCenterXpo")
        End Get
    End Property

End Class