'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.ContractRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/10/2014
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo

#End Region

<Persistent("Contract.ProcedureTemplate")> _
Public Class ProcedureTemplateXpo
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
    <Size(20)> _
    <Persistent("Code")> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fName As String
    <Size(100)> _
    <Persistent("Name")> _
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property

    Dim fStatus As Boolean
    <Persistent("Status")> _
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
            'If fStatus = False Then
            '    Return "Inactivo"
            'Else
            '    Return "Activo"
            'End If
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

    'columna que devuelve el nit y el nombre concatenado
    <Size(50)> _
    <PersistentAlias("concat(concat(Code,' - '),Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

    <Association("Contract_CareGroupReferencesContract_ProcedureTemplate", GetType(ContractCareGroupXpo))> _
    Public ReadOnly Property Contract_CareGroups() As XPCollection(Of ContractCareGroupXpo)
        Get
            Return GetCollection(Of ContractCareGroupXpo)("Contract_CareGroups")
        End Get
    End Property
    <Association("Contract_ProcedureCupsReferencesContract_ProcedureTemplate", GetType(ContractProcedureCupsXpo))> _
    Public ReadOnly Property Contract_ProcedureCupss() As XPCollection(Of ContractProcedureCupsXpo)
        Get
            Return GetCollection(Of ContractProcedureCupsXpo)("Contract_ProcedureCupss")
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
