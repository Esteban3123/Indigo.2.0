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
''' asociacion entre procedureCups y MarketingUnitCups usado en los servicios Xpo
''' </summary>
<Persistent("Contract.VProcedureMarketingUnitCUPS")> _
Public Class VProcedureMarketingUnitCUPS
    Inherits XPLiteObject

#Region "Members"

    Dim fRow As Integer
    <Key(True)> _
    Public Property Row() As Integer
        Get
            Return fRow
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Row", fRow, value)
        End Set
    End Property

    Dim fProceduresTemplateId As Integer
    Public Property ProceduresTemplateId() As Integer
        Get
            Return fProceduresTemplateId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ProceduresTemplateId", fProceduresTemplateId, value)
        End Set
    End Property

    Dim fCUPSId As Integer
    Public Property CUPSId() As Integer
        Get
            Return fCUPSId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CUPSId", fCUPSId, value)
        End Set
    End Property

    Dim fCUPSCode As String
    <Size(20)> _
    Public Property CUPSCode() As String
        Get
            Return fCUPSCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CUPSCode", fCUPSCode, value)
        End Set
    End Property

    Dim fCUPSDescription As String
    <Size(300)> _
    Public Property CUPSDescription() As String
        Get
            Return fCUPSDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CUPSDescription", fCUPSDescription, value)
        End Set
    End Property

    Dim fSubGroupId As Integer
    Public Property SubGroupId() As Integer
        Get
            Return fSubGroupId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("SubGroupId", fSubGroupId, value)
        End Set
    End Property

    Dim fSubGroupCode As String
    <Size(20)> _
    Public Property SubGroupCode() As String
        Get
            Return fSubGroupCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SubGroupCode", fSubGroupCode, value)
        End Set
    End Property

    Dim fSubGroupName As String
    <Size(100)> _
    Public Property SubGroupName() As String
        Get
            Return fSubGroupName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SubGroupName", fSubGroupName, value)
        End Set
    End Property

    Dim fGroupId As Integer
    Public Property GroupId() As Integer
        Get
            Return fGroupId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("GroupId", fGroupId, value)
        End Set
    End Property

    Dim fGroupCode As String
    <Size(20)> _
    Public Property GroupCode() As String
        Get
            Return fGroupCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("GroupCode", fGroupCode, value)
        End Set
    End Property

    Dim fGroupName As String
    <Size(100)> _
    Public Property GroupName() As String
        Get
            Return fGroupName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("GroupName", fGroupName, value)
        End Set
    End Property

    'columna que devuelve el codigo y el nombre concatenado del grupo
    <Size(50)> _
    <PersistentAlias("concat(concat(GroupCode,' - '),GroupName)")>
    Public ReadOnly Property CodeNameGroup() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeNameGroup"))
        End Get
    End Property

    'columna que devuelve el codigo y el nombre concatenado del subGrupo
    <Size(50)> _
    <PersistentAlias("concat(concat(SubGroupCode,' - '),SubGroupName)")>
    Public ReadOnly Property CodeNameSubGroup() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeNameSubGroup"))
        End Get
    End Property

    'columna que devuelve el codigo y el nombre concatenado de la entidad CUPS
    <Size(50)> _
    <PersistentAlias("concat(concat(CUPSCode,' - '),CUPSDescription)")>
    Public ReadOnly Property CodeNameCUPS() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeNameCUPS"))
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
