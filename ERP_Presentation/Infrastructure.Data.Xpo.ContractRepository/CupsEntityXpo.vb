'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.ContractRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 25/09/2014
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
<Persistent("Contract.CUPSEntity")> _
Public Class CupsEntityXpo
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

    Dim fDescription As String
    <Size(300)> _
    <Persistent("Description")> _
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property

    Dim fRIPSCode As String
    <Size(20)> _
    <Persistent("RIPSCode")> _
    Public Property RIPSCode() As String
        Get
            Return fRIPSCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RIPSCode", fRIPSCode, value)
        End Set
    End Property

    Dim fRIPSDescription As String
    <Size(300)>
    <Persistent("RIPSDescription")>
    Public Property RIPSDescription() As String
        Get
            Return fRIPSDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RIPSDescription", fRIPSDescription, value)
        End Set
    End Property

    Dim fApplyRIAS As Boolean
    Public Property ApplyRIAS() As Boolean
        Get
            Return fApplyRIAS
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ApplyRIAS", fApplyRIAS, value)
        End Set
    End Property

    Private foxigenService As Boolean
    Public Property OxigenService() As Boolean
        Get
            Return foxigenService
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("OxigenService", foxigenService, value)
        End Set
    End Property


    Dim fCUPSSubGroupId As CupsSubGroupXpo
    <Association("CupsEntityReferencesCupSubGroup")> _
    Public Property CUPSSubGroupId() As CupsSubGroupXpo
        Get
            Return fCUPSSubGroupId
        End Get
        Set(ByVal value As CupsSubGroupXpo)
            SetPropertyValue(Of CupsSubGroupXpo)("CUPSSubGroupId", fCUPSSubGroupId, value)
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

    <NonPersistent()>
    Public Property SelectOption As Boolean

#End Region

#Region "Custom Members"

    <Size(50)>
    <PersistentAlias("concat(concat(Code,' - '),Description)")>
    Public ReadOnly Property CodeDescription() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeDescription"))
        End Get
    End Property

    <PersistentAlias("CUPSSubGroupId.CodeName")>
    Public ReadOnly Property SubGroupCodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("SubGroupCodeName"))
        End Get
    End Property

    <PersistentAlias("CUPSSubGroupId.CupsGroupId.CodeName")>
    Public ReadOnly Property GroupCodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("GroupCodeName"))
        End Get
    End Property

#End Region

#Region "Navigation Memebers"

    <Association("Contract_CupsHomologationReferencesContract_CUPSEntity", GetType(ContractCupsHomologationXpo))>
    Public ReadOnly Property Contract_CupsHomologations() As XPCollection(Of ContractCupsHomologationXpo)
        Get
            Return GetCollection(Of ContractCupsHomologationXpo)("Contract_CupsHomologations")
        End Get
    End Property
    <Association("Contract_ProcedureCupsReferencesContract_CUPSEntity", GetType(ContractProcedureCupsXpo))>
    Public ReadOnly Property Contract_ProcedureCupss() As XPCollection(Of ContractProcedureCupsXpo)
        Get
            Return GetCollection(Of ContractProcedureCupsXpo)("Contract_ProcedureCupss")
        End Get
    End Property

    <Association("ProcedureCupsReferencesCupsEntity", GetType(ProcedureCupsXpo))>
    Public ReadOnly Property ProcedureCupsXpo() As XPCollection(Of ProcedureCupsXpo)
        Get
            Return GetCollection(Of ProcedureCupsXpo)("ProcedureCupsXpo")
        End Get
    End Property

    <Association("DefinitionRateDetailReferencesCUPS", GetType(DefinitionRateDetailXpo))>
    Public ReadOnly Property DefinitionRateDetailXpo() As XPCollection(Of DefinitionRateDetailXpo)
        Get
            Return GetCollection(Of DefinitionRateDetailXpo)("DefinitionRateDetailXpo")
        End Get
    End Property

    <Association("CupsEntityDescriptionReferencesCups", GetType(CUPSEntityContractDescriptionsXpo))>
    Public ReadOnly Property CUPSEntityContractDescriptionsXpo() As XPCollection(Of CUPSEntityContractDescriptionsXpo)
        Get
            Return GetCollection(Of CUPSEntityContractDescriptionsXpo)("CUPSEntityContractDescriptionsXpo")
        End Get
    End Property

    <Association("ContractPackagesReferencesCUPS", GetType(ContractPackageXpo))>
    Public ReadOnly Property ContractPackages() As XPCollection(Of ContractPackageXpo)
        Get
            Return GetCollection(Of ContractPackageXpo)("ContractPackages")
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
