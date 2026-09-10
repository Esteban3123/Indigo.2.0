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
Imports Infrastructure.CrossCutting.Resources

#End Region

''' <summary>
''' conceptos de nota usado en los servicios Xpo
''' </summary>
<Persistent("Contract.DefinitionRateDetail")> _
Public Class DefinitionRateDetailXpo
    Inherits XPLiteObject

#Region "Members"

    ''' <summary>
    ''' Nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME As String = "Contract"

    ''' <summary>
    ''' Prefijo del tipo de regla
    ''' </summary>
    Private Const RuleType_Prefix As String = "RuleType"

    ''' <summary>
    ''' Prefijo del tipo de condicion
    ''' </summary>
    Private Const ConditionType_Prefix As String = "ConditionType"

    ''' <summary>
    ''' Prefijo del tipo de liquidación
    ''' </summary>
    Private Const LiquidationType_Prefix As String = "LiquidationType"

    ''' <summary>
    ''' Prefijo del tipo de liquidación
    ''' </summary>
    Private Const ManualType_Prefix As String = "ManualType"

    ''' <summary>
    ''' Prefijo del operador logico
    ''' </summary>
    Private Const LogicalOperator_Prefix As String = "LogicalOperator"

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

    Dim fDefinitionRateId As DefinitionRateXpo
    <Association("DefinitionRateDetailReferencesDefinitionRate")> _
    Public Property DefinitionRateId() As DefinitionRateXpo
        Get
            Return fDefinitionRateId
        End Get
        Set(ByVal value As DefinitionRateXpo)
            SetPropertyValue(Of DefinitionRateXpo)("DefinitionRateId", fDefinitionRateId, value)
        End Set
    End Property

    Dim fRuleType As Integer
    <Persistent("RuleType")> _
    Public Property RuleType() As Integer
        Get
            Return fRuleType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RuleType", fRuleType, value)
        End Set
    End Property

    <NonPersistent()> _
    Public ReadOnly Property RuleTypeName() As String
        Get
            Return ResourceManager.GetString(RuleType_Prefix & fRuleType.ToString(), MODULE_NAME)
        End Get
    End Property

    Dim fIPSServiceId As ContractIPSServiceXPO
    <Association("DefinitionRateDetailReferencesIPSService")>
    Public Property IPSServiceId() As ContractIPSServiceXPO
        Get
            Return fIPSServiceId
        End Get
        Set(ByVal value As ContractIPSServiceXPO)
            SetPropertyValue(Of ContractIPSServiceXPO)("IPSServiceId", fIPSServiceId, value)
        End Set
    End Property

    Dim fCUPSEntityId As CupsEntityXpo
    <Association("DefinitionRateDetailReferencesCUPS")> _
    Public Property CUPSEntityId() As CupsEntityXpo
        Get
            Return fCUPSEntityId
        End Get
        Set(ByVal value As CupsEntityXpo)
            SetPropertyValue(Of CupsEntityXpo)("CUPSEntityId", fCUPSEntityId, value)
        End Set
    End Property

    Dim fCUPSSubgroupId As CupsSubGroupXpo
    <Association("DefinitionRateDetailReferencesSubGroup")> _
    Public Property CUPSSubgroupId() As CupsSubGroupXpo
        Get
            Return fCUPSSubgroupId
        End Get
        Set(ByVal value As CupsSubGroupXpo)
            SetPropertyValue(Of CupsSubGroupXpo)("CUPSSubgroupId", fCUPSSubgroupId, value)
        End Set
    End Property

    Dim fCUPSGroupId As CupsGroupXpo
    <Association("DefinitionRateDetailReferencesGroup")> _
    Public Property CUPSGroupId() As CupsGroupXpo
        Get
            Return fCUPSGroupId
        End Get
        Set(ByVal value As CupsGroupXpo)
            SetPropertyValue(Of CupsGroupXpo)("CUPSGroupId", fCUPSGroupId, value)
        End Set
    End Property

    Dim fConditionType As Integer
    <Persistent("ConditionType")> _
    Public Property ConditionType() As Integer
        Get
            Return fConditionType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ConditionType", fConditionType, value)
        End Set
    End Property

    <NonPersistent()> _
    Public ReadOnly Property ConditionTypeName() As String
        Get
            Return ResourceManager.GetString(ConditionType_Prefix & fConditionType.ToString(), MODULE_NAME)
        End Get
    End Property

    Dim fConditionType2 As Integer
    <Persistent("ConditionType2")> _
    Public Property ConditionType2() As Integer
        Get
            Return fConditionType2
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ConditionType2", fConditionType2, value)
        End Set
    End Property

    <NonPersistent()> _
    Public ReadOnly Property ConditionTypeName2() As String
        Get
            Return ResourceManager.GetString(ConditionType_Prefix & fConditionType2.ToString(), MODULE_NAME)
        End Get
    End Property

    Dim fLogicalOperator As Integer
    <Persistent("LogicalOperator")> _
    Public Property LogicalOperator() As Integer
        Get
            Return fLogicalOperator
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("LogicalOperator", fLogicalOperator, value)
        End Set
    End Property

    <NonPersistent()> _
    Public ReadOnly Property LogicalOperatorName() As String
        Get
            Return ResourceManager.GetString(LogicalOperator_Prefix & fLogicalOperator.ToString(), MODULE_NAME)
        End Get
    End Property

    Dim fWeight As Integer
    <Persistent("Weight")> _
    Public Property Weight() As Integer
        Get
            Return fWeight
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Weight", fWeight, value)
        End Set
    End Property

    Dim fLiquidationType As Integer
    <Persistent("LiquidationType")> _
    Public Property LiquidationType() As Integer
        Get
            Return fLiquidationType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("LiquidationType", fLiquidationType, value)
        End Set
    End Property

    <NonPersistent()> _
    Public ReadOnly Property LiquidationTypeName() As String
        Get
            Return ResourceManager.GetString(LiquidationType_Prefix & fLiquidationType.ToString(), MODULE_NAME)
        End Get
    End Property

    Dim fManualType As Integer
    <Persistent("ManualType")> _
    Public Property ManualType() As Integer
        Get
            Return fManualType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ManualType", fManualType, value)
        End Set
    End Property

    <NonPersistent()> _
    Public ReadOnly Property ManualTypeName() As String
        Get
            Return ResourceManager.GetString(ManualType_Prefix & fManualType.ToString(), MODULE_NAME)
        End Get
    End Property

    Dim fSalesValue As Decimal
    <Persistent("SalesValue")> _
    Public Property SalesValue() As Decimal
        Get
            Return fSalesValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("SalesValue", fSalesValue, value)
        End Set
    End Property

    Dim fSalesValueWithSurcharge As Decimal
    <Persistent("SalesValueWithSurcharge")> _
    Public Property SalesValueWithSurcharge() As Decimal
        Get
            Return fSalesValueWithSurcharge
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("SalesValueWithSurcharge", fSalesValueWithSurcharge, value)
        End Set
    End Property

    Dim fRateManualId As RateManualXpo
    <Association("DefinitionRateDetailReferencesRateManual")> _
    Public Property RateManualId() As RateManualXpo
        Get
            Return fRateManualId
        End Get
        Set(ByVal value As RateManualXpo)
            SetPropertyValue(Of RateManualXpo)("RateManualId", fRateManualId, value)
        End Set
    End Property

    Dim fRateVariation As Decimal
    <Persistent("RateVariation")> _
    Public Property RateVariation() As Decimal
        Get
            Return fRateVariation
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("RateVariation", fRateVariation, value)
        End Set
    End Property

    Dim fAllowValueChange As Boolean
    <Persistent("AllowValueChange")> _
    Public Property AllowValueChange() As Boolean
        Get
            Return fAllowValueChange
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("AllowValueChange", fAllowValueChange, value)
        End Set
    End Property

    <Association("DefinitionRateDetailSurgicalReferencesDefinitionRateDetail", GetType(DefinitionRateDetailSurgicalProceduresXpo))>
    Public ReadOnly Property DefinitionRateDetailSurgicalProceduresXpo() As XPCollection(Of DefinitionRateDetailSurgicalProceduresXpo)
        Get
            Return GetCollection(Of DefinitionRateDetailSurgicalProceduresXpo)("DefinitionRateDetailSurgicalProceduresXpo")
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
