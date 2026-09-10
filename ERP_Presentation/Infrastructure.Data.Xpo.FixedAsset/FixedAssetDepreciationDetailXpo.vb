Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetDepreciationDetail")> _
Public Class FixedAssetDepreciationDetailXpo
    Inherits XPLiteObject

    Dim fId As Integer
    <Key(True)> _
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fFixedAssetDepreciationId As FixedAssetDepreciationXpo
    <Association("DepreciationDetailReferenceDepreciation")> _
    Public Property FixedAssetDepreciationId() As FixedAssetDepreciationXpo
        Get
            Return fFixedAssetDepreciationId
        End Get
        Set(ByVal value As FixedAssetDepreciationXpo)
            SetPropertyValue(Of FixedAssetDepreciationXpo)("FixedAssetDepreciationId", fFixedAssetDepreciationId, value)
        End Set
    End Property

    Dim fLegalBookId As BookXpo
    <Association("DepreciationDetailReferenceBook")> _
    Public Property LegalBookId() As BookXpo
        Get
            Return fLegalBookId
        End Get
        Set(ByVal value As BookXpo)
            SetPropertyValue(Of BookXpo)("LegalBookId", fLegalBookId, value)
        End Set
    End Property

    Dim fActiveClass As Integer
    Public Property ActiveClass() As Integer
        Get
            Return fActiveClass
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ActiveClass", fActiveClass, value)
        End Set
    End Property

    Dim fFixedAssetPhysicalAssetId As FixedAssetPhysicalAssetXpo
    <Association("DepreciationDetailReferencePhysical")> _
    Public Property FixedAssetPhysicalAssetId() As FixedAssetPhysicalAssetXpo
        Get
            Return fFixedAssetPhysicalAssetId
        End Get
        Set(ByVal value As FixedAssetPhysicalAssetXpo)
            SetPropertyValue(Of FixedAssetPhysicalAssetXpo)("FixedAssetPhysicalAssetId", fFixedAssetPhysicalAssetId, value)
        End Set
    End Property

    Dim fFixedAssetPhysicalAssetDetailBookId As FixedAssetPhysicalAssetDetailBookXpo
    <Association("DepreciationDetailReferencePhysicalDetailBook")> _
    Public Property FixedAssetPhysicalAssetDetailBookId() As FixedAssetPhysicalAssetDetailBookXpo
        Get
            Return fFixedAssetPhysicalAssetDetailBookId
        End Get
        Set(ByVal value As FixedAssetPhysicalAssetDetailBookXpo)
            SetPropertyValue(Of FixedAssetPhysicalAssetDetailBookXpo)("FixedAssetPhysicalAssetDetailBookId", fFixedAssetPhysicalAssetDetailBookId, value)
        End Set
    End Property

    Dim fDepreciationValue As Decimal
    Public Property DepreciationValue() As Decimal
        Get
            Return fDepreciationValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DepreciationValue", fDepreciationValue, value)
        End Set
    End Property

    Dim fResidualValue As Decimal
    Public Property ResidualValue() As Decimal
        Get
            Return fResidualValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ResidualValue", fResidualValue, value)
        End Set
    End Property

    Dim fLifeTime As Integer
    Public Property LifeTime() As Integer
        Get
            Return fLifeTime
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("LifeTime", fLifeTime, value)
        End Set
    End Property

    Dim fDepreciatedDays As Integer
    Public Property DepreciatedDays() As Integer
        Get
            Return fDepreciatedDays
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("DepreciatedDays", fDepreciatedDays, value)
        End Set
    End Property

    Dim fFinantialDiscount As Decimal
    Public Property FinantialDiscount() As Decimal
        Get
            Return fFinantialDiscount
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("FinantialDiscount", fFinantialDiscount, value)
        End Set
    End Property

    Dim fFinantialDiscountAdjusment As Decimal
    Public Property FinantialDiscountAdjusment() As Decimal
        Get
            Return fFinantialDiscountAdjusment
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("FinantialDiscountAdjusment", fFinantialDiscountAdjusment, value)
        End Set
    End Property

    Dim fAccumulatedDepreciation As Decimal
    Public Property AccumulatedDepreciation() As Decimal
        Get
            Return fAccumulatedDepreciation
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("AccumulatedDepreciation", fAccumulatedDepreciation, value)
        End Set
    End Property

#Region "Custom Members"

    <PersistentAlias("FixedAssetPhysicalAssetDetailBookId.HistoricalValue - FinantialDiscount")>
    Public ReadOnly Property NetHistoricalValue As Decimal
        Get
            Return Convert.ToDecimal(Me.EvaluateAlias("NetHistoricalValue"))
        End Get
    End Property

    <PersistentAlias("DepreciationValue  - FinantialDiscountAdjusment")>
    Public ReadOnly Property NetDepreciationValue As Decimal
        Get
            Return Convert.ToDecimal(Me.EvaluateAlias("NetDepreciationValue"))
        End Get
    End Property

#End Region

#Region "Navigations"

    <Association("DepreciationDetailCostReferenceDepreciationDetail", GetType(FixedAssetDepreciationDetailCostXpo))>
    Public ReadOnly Property FixedAssetDepreciationDetailCostXpo() As XPCollection(Of FixedAssetDepreciationDetailCostXpo)
        Get
            Return GetCollection(Of FixedAssetDepreciationDetailCostXpo)("FixedAssetDepreciationDetailCostXpo")
        End Get
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

End Class

