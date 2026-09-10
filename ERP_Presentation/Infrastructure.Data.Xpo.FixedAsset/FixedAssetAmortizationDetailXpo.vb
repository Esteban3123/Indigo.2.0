Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("FixedAsset.FixedAssetAmortizationDetail")>
Partial Public Class FixedAssetAmortizationDetailXpo
    Inherits XPLiteObject

#Region "Builder"
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

    Dim fFixedAssetDepreciationId As FixedAssetDepreciationXpo
    <Association("FixedAssetAmortizationDetailReferenceDepreciation")>
    Public Property FixedAssetDepreciationId() As FixedAssetDepreciationXpo
        Get
            Return fFixedAssetDepreciationId
        End Get
        Set(ByVal value As FixedAssetDepreciationXpo)
            SetPropertyValue(Of FixedAssetDepreciationXpo)("FixedAssetDepreciationId", fFixedAssetDepreciationId, value)
        End Set
    End Property

    Dim fFixedAssetPhysicalAssetDetailBookId As FixedAssetPhysicalAssetDetailBookXpo
    <Association("FixedAssetAmortizationDetailReferencePhysicalDetailBook")>
    Public Property FixedAssetPhysicalAssetDetailBookId() As FixedAssetPhysicalAssetDetailBookXpo
        Get
            Return fFixedAssetPhysicalAssetDetailBookId
        End Get
        Set(ByVal value As FixedAssetPhysicalAssetDetailBookXpo)
            SetPropertyValue(Of FixedAssetPhysicalAssetDetailBookXpo)("FixedAssetPhysicalAssetDetailBookId", fFixedAssetPhysicalAssetDetailBookId, value)
        End Set
    End Property

    Dim fAccumulatedAmortization As Decimal
    Public Property AccumulatedAmortization() As Decimal
        Get
            Return fAccumulatedAmortization
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("AccumulatedAmortization", fAccumulatedAmortization, value)
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

    Dim fAmortizedDays As Integer
    Public Property AmortizedDays() As Integer
        Get
            Return fAmortizedDays
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AmortizedDays", fAmortizedDays, value)
        End Set
    End Property

    Dim fAmortizedValue As Decimal
    Public Property AmortizedValue() As Decimal
        Get
            Return fAmortizedValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("AmortizedValue", fAmortizedValue, value)
        End Set
    End Property

#End Region

#Region "Association"
    <Association("FixedAssetAmortizationDetailCostReferenceFixedAssetAmortizationDetail", GetType(FixedAssetAmortizationDetailCostXpo))>
    Public ReadOnly Property FixedAssetAmortizationDetailCostXpo() As XPCollection(Of FixedAssetAmortizationDetailCostXpo)
        Get
            Return GetCollection(Of FixedAssetAmortizationDetailCostXpo)("FixedAssetAmortizationDetailCostXpo")
        End Get
    End Property
#End Region
End Class