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
<Persistent("Contract.ContractDescriptions")>
Public Class ContractDescriptionsXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As Integer
    <Key(True)>
    <Persistent("Id")>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fCode As String
    <Persistent("Code")>
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fName As String
    <Persistent("Name")>
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property

    Dim fStatus As Boolean
    <Persistent("Status")>
    Public Property Status() As Boolean
        Get
            Return fStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Status", fStatus, value)
        End Set
    End Property

    'columna que devuelve el nit y el nombre concatenado
    <PersistentAlias("concat(Code,' - ',Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

    <PersistentAlias("iif(Status, 'Activo', 'Inactivo')")>
    Public ReadOnly Property StatusName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
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

    <Association("CupsEntityDescriptionReferencesDescriptions", GetType(CUPSEntityContractDescriptionsXpo))>
    Public ReadOnly Property CUPSEntityContractDescriptionsXpo() As XPCollection(Of CUPSEntityContractDescriptionsXpo)
        Get
            Return GetCollection(Of CUPSEntityContractDescriptionsXpo)("CUPSEntityContractDescriptionsXpo")
        End Get
    End Property

    <Association("ProcedureCupsReferencesContractDescription", GetType(ProcedureCupsXpo))>
    Public ReadOnly Property ProcedureCupsXpo() As XPCollection(Of ProcedureCupsXpo)
        Get
            Return GetCollection(Of ProcedureCupsXpo)("ProcedureCupsXpo")
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
