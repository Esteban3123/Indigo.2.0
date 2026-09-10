Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetEntryDevolutionDetail")> _
Public Class FixedAssetFixedAssetEntryDevolutionDetailReportXpo
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
    Dim fFixedAssetEntryDevolutionId As FixedAssetFixedAssetEntryDevolutionReportXpo
    <Association("FixedAsset_FixedAssetEntryDevolutionDetailReferencesFixedAsset_FixedAssetEntryDevolution")> _
    Public Property FixedAssetEntryDevolutionId() As FixedAssetFixedAssetEntryDevolutionReportXpo
        Get
            Return fFixedAssetEntryDevolutionId
        End Get
        Set(ByVal value As FixedAssetFixedAssetEntryDevolutionReportXpo)
            SetPropertyValue(Of FixedAssetFixedAssetEntryDevolutionReportXpo)("FixedAssetEntryDevolutionId", fFixedAssetEntryDevolutionId, value)
        End Set
    End Property
    Dim fFixedAssetEntryItemId As FixedAssetFixedAssetEntryItemReportXpo
    <Association("FixedAsset_FixedAssetEntryDevolutionDetailReferencesFixedAsset_FixedAssetEntryItem")> _
    Public Property FixedAssetEntryItemId() As FixedAssetFixedAssetEntryItemReportXpo
        Get
            Return fFixedAssetEntryItemId
        End Get
        Set(ByVal value As FixedAssetFixedAssetEntryItemReportXpo)
            SetPropertyValue(Of FixedAssetFixedAssetEntryItemReportXpo)("FixedAssetEntryItemId", fFixedAssetEntryItemId, value)
        End Set
    End Property
    Dim fFixedAssetEntryItemDetailId As FixedAssetFixedAssetEntryItemDetailReportXpo
    <Association("FixedAsset_FixedAssetEntryDevolutionDetailReferencesFixedAsset_FixedAssetEntryItemDetail")> _
    Public Property FixedAssetEntryItemDetailId() As FixedAssetFixedAssetEntryItemDetailReportXpo
        Get
            Return fFixedAssetEntryItemDetailId
        End Get
        Set(ByVal value As FixedAssetFixedAssetEntryItemDetailReportXpo)
            SetPropertyValue(Of FixedAssetFixedAssetEntryItemDetailReportXpo)("FixedAssetEntryItemDetailId", fFixedAssetEntryItemDetailId, value)
        End Set
    End Property
    Dim fUnitValue As Decimal
    Public Property UnitValue() As Decimal
        Get
            Return fUnitValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("UnitValue", fUnitValue, value)
        End Set
    End Property
    Dim fSubTotalValue As Decimal
    Public Property SubTotalValue() As Decimal
        Get
            Return fSubTotalValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("SubTotalValue", fSubTotalValue, value)
        End Set
    End Property
    Dim fIvaPercentage As Decimal
    Public Property IvaPercentage() As Decimal
        Get
            Return fIvaPercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("IvaPercentage", fIvaPercentage, value)
        End Set
    End Property
    Dim fIvaValue As Decimal
    Public Property IvaValue() As Decimal
        Get
            Return fIvaValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("IvaValue", fIvaValue, value)
        End Set
    End Property
    Dim fDiscountPercentage As Decimal
    Public Property DiscountPercentage() As Decimal
        Get
            Return fDiscountPercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DiscountPercentage", fDiscountPercentage, value)
        End Set
    End Property
    Dim fDiscountValue As Decimal
    Public Property DiscountValue() As Decimal
        Get
            Return fDiscountValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DiscountValue", fDiscountValue, value)
        End Set
    End Property
    Dim fTotalValue As Decimal
    Public Property TotalValue() As Decimal
        Get
            Return fTotalValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalValue", fTotalValue, value)
        End Set
    End Property
    Dim fRTFPercentage As Decimal
    Public Property RTFPercentage() As Decimal
        Get
            Return fRTFPercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("RTFPercentage", fRTFPercentage, value)
        End Set
    End Property
    Dim fRTFValue As Decimal
    Public Property RTFValue() As Decimal
        Get
            Return fRTFValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("RTFValue", fRTFValue, value)
        End Set
    End Property


    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
