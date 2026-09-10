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
<Persistent("Contract.ViewListDefinitionRateDetail")>
Public Class ViewListDefinitionRateDetailXpo
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

    Dim fDefinitionRateId As Integer
    Public Property DefinitionRateId() As Integer
        Get
            Return fDefinitionRateId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("DefinitionRateId", fDefinitionRateId, value)
        End Set
    End Property

    Dim fRuleType As Integer
    Public Property RuleType() As Integer
        Get
            Return fRuleType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RuleType", fRuleType, value)
        End Set
    End Property

    Dim fRuleTypeName As String
    Public Property RuleTypeName() As String
        Get
            Return fRuleTypeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RuleTypeName", fRuleTypeName, value)
        End Set
    End Property

    Dim fRuleDescription As String
    Public Property RuleDescription() As String
        Get
            Return fRuleDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RuleDescription", fRuleDescription, value)
        End Set
    End Property

    Dim fIPSServiceId As Integer?
    Public Property IPSServiceId() As Integer?
        Get
            Return fIPSServiceId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("IPSServiceId", fIPSServiceId, value)
        End Set
    End Property

    Dim fCUPSEntityId As Integer?
    Public Property CUPSEntityId() As Integer?
        Get
            Return fCUPSEntityId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("CUPSEntityId", fCUPSEntityId, value)
        End Set
    End Property

    Dim fCUPSSubgroupId As Integer?
    Public Property CUPSSubgroupId() As Integer?
        Get
            Return fCUPSSubgroupId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("CUPSSubgroupId", fCUPSSubgroupId, value)
        End Set
    End Property

    Dim fCUPSGroupId As Integer?
    Public Property CUPSGroupId() As Integer?
        Get
            Return fCUPSGroupId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("CUPSGroupId", fCUPSGroupId, value)
        End Set
    End Property

    Dim fConditionType As Integer
    Public Property ConditionType() As Integer
        Get
            Return fConditionType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ConditionType", fConditionType, value)
        End Set
    End Property

    Dim fLogicalOperator As Integer
    Public Property LogicalOperator() As Integer
        Get
            Return fLogicalOperator
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("LogicalOperator", fLogicalOperator, value)
        End Set
    End Property

    Dim fConditionType2 As Integer
    Public Property ConditionType2() As Integer
        Get
            Return fConditionType2
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ConditionType2", fConditionType2, value)
        End Set
    End Property

    Dim fConditionTypeName As String
    Public Property ConditionTypeName() As String
        Get
            Return fConditionTypeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ConditionTypeName", fConditionTypeName, value)
        End Set
    End Property

    Dim fConditionTypeName2 As String
    Public Property ConditionTypeName2() As String
        Get
            Return fConditionTypeName2
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ConditionTypeName2", fConditionTypeName2, value)
        End Set
    End Property

    Dim fConditionName As String
    Public Property ConditionName() As String
        Get
            Return fConditionName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ConditionName", fConditionName, value)
        End Set
    End Property

    Dim fWeight As Integer
    Public Property Weight() As Integer
        Get
            Return fWeight
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Weight", fWeight, value)
        End Set
    End Property

    Dim fAllowValueChange As Boolean
    Public Property AllowValueChange() As Boolean
        Get
            Return fAllowValueChange
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("AllowValueChange", fAllowValueChange, value)
        End Set
    End Property

    Dim fLiquidationType As Byte?
    Public Property LiquidationType() As Byte?
        Get
            Return fLiquidationType
        End Get
        Set(ByVal value As Byte?)
            SetPropertyValue(Of Byte?)("LiquidationType", fLiquidationType, value)
        End Set
    End Property

    Dim fRateManualId As Integer?
    Public Property RateManualId() As Integer?
        Get
            Return fRateManualId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("RateManualId", fRateManualId, value)
        End Set
    End Property

    Dim fRateManualDescription As String
    Public Property RateManualDescription() As String
        Get
            Return fRateManualDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RateManualDescription", fRateManualDescription, value)
        End Set
    End Property

    Dim fRateManualValidityId As Integer?
    Public Property RateManualValidityId() As Integer?
        Get
            Return fRateManualValidityId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("RateManualValidityId", fRateManualValidityId, value)
        End Set
    End Property

    Dim fRateManualValidityDescription As String
    Public Property RateManualValidityDescription() As String
        Get
            Return fRateManualValidityDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RateManualValidityDescription", fRateManualValidityDescription, value)
        End Set
    End Property

    Dim fManualType As Integer?
    Public Property ManualType() As Integer?
        Get
            Return fManualType
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("ManualType", fManualType, value)
        End Set
    End Property

    Dim fSalesValue As Decimal?
    Public Property SalesValue() As Decimal?
        Get
            Return fSalesValue
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("SalesValue", fSalesValue, value)
        End Set
    End Property

    Dim fSalesValueWithSurcharge As Decimal?
    Public Property SalesValueWithSurcharge() As Decimal?
        Get
            Return fSalesValueWithSurcharge
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("SalesValueWithSurcharge", fSalesValueWithSurcharge, value)
        End Set
    End Property

    Dim fRateVariation As Decimal?
    Public Property RateVariation() As Decimal?
        Get
            Return fRateVariation
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("RateVariation", fRateVariation, value)
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
