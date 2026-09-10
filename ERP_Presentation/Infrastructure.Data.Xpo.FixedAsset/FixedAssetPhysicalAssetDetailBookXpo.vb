Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetPhysicalAssetDetailBook")> _
Public Class FixedAssetPhysicalAssetDetailBookXpo
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

    Dim fPhysicalAssetId As FixedAssetPhysicalAssetXpo
    <Association("FixedAssetPhysicalAssetReferenceFixedAssetPhysicalAssetDetailBook")>
    Public Property PhysicalAssetId() As FixedAssetPhysicalAssetXpo
        Get
            Return fPhysicalAssetId
        End Get
        Set(ByVal value As FixedAssetPhysicalAssetXpo)
            SetPropertyValue(Of FixedAssetPhysicalAssetXpo)("PhysicalAssetId", fPhysicalAssetId, value)
        End Set
    End Property

    Dim fLegalBookId As BookXpo
    <Association("BookItemReferencePhysicalAssetBook")>
    Public Property LegalBookId() As BookXpo
        Get
            Return fLegalBookId
        End Get
        Set(ByVal value As BookXpo)
            SetPropertyValue(Of BookXpo)("LegalBookId", fLegalBookId, value)
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

    Dim fUnitLifeTime As Byte
    Public Property UnitLifeTime() As Byte
        Get
            Return fUnitLifeTime
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("UnitLifeTime", fUnitLifeTime, value)
        End Set
    End Property

    Dim fValorizationDays As Integer
    Public Property ValorizationDays() As Integer
        Get
            Return fValorizationDays
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ValorizationDays", fValorizationDays, value)
        End Set
    End Property

    Dim fDaysPendingDepreciate As Integer
    Public Property DaysPendingDepreciate() As Integer
        Get
            Return fDaysPendingDepreciate
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("DaysPendingDepreciate", fDaysPendingDepreciate, value)
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

    Dim fDepreciationType As Byte
    Public Property DepreciationType() As Byte
        Get
            Return fDepreciationType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("DepreciationType", fDepreciationType, value)
        End Set
    End Property

    Dim fTotalProductionUnit As Decimal
    Public Property TotalProductionUnit() As Decimal
        Get
            Return fTotalProductionUnit
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalProductionUnit", fTotalProductionUnit, value)
        End Set
    End Property

    Dim fPercentageRescue As Decimal
    Public Property PercentageRescue() As Decimal
        Get
            Return fPercentageRescue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PercentageRescue", fPercentageRescue, value)
        End Set
    End Property

    Dim fValorization As Decimal
    Public Property Valorization() As Decimal
        Get
            Return fValorization
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Valorization", fValorization, value)
        End Set
    End Property

    Dim fDevaluation As Decimal
    Public Property Devaluation() As Decimal
        Get
            Return fDevaluation
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Devaluation", fDevaluation, value)
        End Set
    End Property

    Dim fAdjustedValue As Decimal
    Public Property AdjustedValue() As Decimal
        Get
            Return fAdjustedValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("AdjustedValue", fAdjustedValue, value)
        End Set
    End Property

    Dim fTransactionValue As Decimal
    Public Property TransactionValue() As Decimal
        Get
            Return fTransactionValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TransactionValue", fTransactionValue, value)
        End Set
    End Property

    Dim fDepreciatedValue As Decimal
    Public Property DepreciatedValue() As Decimal
        Get
            Return fDepreciatedValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DepreciatedValue", fDepreciatedValue, value)
        End Set
    End Property

    Dim fDepreciatedValuePart As Decimal
    Public Property DepreciatedValuePart() As Decimal
        Get
            Return fDepreciatedValuePart
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DepreciatedValuePart", fDepreciatedValuePart, value)
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

    Dim fResidualValuePart As Decimal
    Public Property ResidualValuePart() As Decimal
        Get
            Return fResidualValuePart
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ResidualValuePart", fResidualValuePart, value)
        End Set
    End Property

    Dim fHistoricalValue As Decimal
    Public Property HistoricalValue() As Decimal
        Get
            Return fHistoricalValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("HistoricalValue", fHistoricalValue, value)
        End Set
    End Property

    Dim fApplyMinimunAmount As Boolean
    Public Property ApplyMinimunAmount() As Boolean
        Get
            Return fApplyMinimunAmount
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ApplyMinimunAmount", fApplyMinimunAmount, value)
        End Set
    End Property

#End Region

#Region "Custom Members"

    <PersistentAlias("DaysPendingDepreciate")>
    Public ReadOnly Property LifeTimeInDays As Integer
        Get
            Return Convert.ToInt32(Me.EvaluateAlias("LifeTimeInDays"))
        End Get
    End Property

#End Region

#Region "Navigations"

    <Association("DepreciationDetailReferencePhysicalDetailBook", GetType(FixedAssetDepreciationDetailXpo))>
    Public ReadOnly Property FixedAssetDepreciationDetailXpo() As XPCollection(Of FixedAssetDepreciationDetailXpo)
        Get
            Return GetCollection(Of FixedAssetDepreciationDetailXpo)("FixedAssetDepreciationDetailXpo")
        End Get
    End Property

    <Association("FixedAssetAmortizationDetailReferencePhysicalDetailBook", GetType(FixedAssetAmortizationDetailXpo))>
    Public ReadOnly Property FixedAssetAmortizationDetailXpo() As XPCollection(Of FixedAssetAmortizationDetailXpo)
        Get
            Return GetCollection(Of FixedAssetAmortizationDetailXpo)("FixedAssetAmortizationDetailXpo")
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
