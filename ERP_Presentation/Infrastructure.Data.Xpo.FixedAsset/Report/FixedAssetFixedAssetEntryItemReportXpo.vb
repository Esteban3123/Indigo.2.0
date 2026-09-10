Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetEntryItem")> _
 Public Class FixedAssetFixedAssetEntryItemReportXpo
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
    Dim fFixedAssetEntryId As FixedAssetFixedAssetEntryReportXpo
    <Association("FixedAsset_FixedAssetEntryItemReferencesFixedAsset_FixedAssetEntry")> _
    Public Property FixedAssetEntryId() As FixedAssetFixedAssetEntryReportXpo
        Get
            Return fFixedAssetEntryId
        End Get
        Set(ByVal value As FixedAssetFixedAssetEntryReportXpo)
            SetPropertyValue(Of FixedAssetFixedAssetEntryReportXpo)("FixedAssetEntryId", fFixedAssetEntryId, value)
        End Set
    End Property
    Dim fRemissionSource As Byte
    Public Property RemissionSource() As Byte
        Get
            Return fRemissionSource
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("RemissionSource", fRemissionSource, value)
        End Set
    End Property
    Dim fSourceCode As String
    <Size(20)> _
    Public Property SourceCode() As String
        Get
            Return fSourceCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SourceCode", fSourceCode, value)
        End Set
    End Property
    Dim fPurchaseOrderItemId As FixedAssetFixedAssetPurchaseOrderItemReportXpo
    <Association("FixedAsset_FixedAssetEntryItemReferencesFixedAsset_FixedAssetPurchaseOrderItem")> _
    Public Property PurchaseOrderItemId() As FixedAssetFixedAssetPurchaseOrderItemReportXpo
        Get
            Return fPurchaseOrderItemId
        End Get
        Set(ByVal value As FixedAssetFixedAssetPurchaseOrderItemReportXpo)
            SetPropertyValue(Of FixedAssetFixedAssetPurchaseOrderItemReportXpo)("PurchaseOrderItemId", fPurchaseOrderItemId, value)
        End Set
    End Property
    Dim fRemissionEntranceItemId As FixedAssetFixedAssetRemissionEntranceItemReportXpo
    <Association("FixedAssetFixedAssetEntryItemReportXpoReferencesFixedAssetFixedAssetRemissionEntranceItemReportXpo")> _
    Public Property RemissionEntranceItemId() As FixedAssetFixedAssetRemissionEntranceItemReportXpo
        Get
            Return fRemissionEntranceItemId
        End Get
        Set(ByVal value As FixedAssetFixedAssetRemissionEntranceItemReportXpo)
            SetPropertyValue(Of FixedAssetFixedAssetRemissionEntranceItemReportXpo)("RemissionEntranceItemId", fRemissionEntranceItemId, value)
        End Set
    End Property
    Dim fItemId As FixedAssetItemReportXpo
    <Association("FixedAsset_FixedAssetEntryItemReferencesFixedAsset_FixedAssetItem")> _
    Public Property ItemId() As FixedAssetItemReportXpo
        Get
            Return fItemId
        End Get
        Set(ByVal value As FixedAssetItemReportXpo)
            SetPropertyValue(Of FixedAssetItemReportXpo)("ItemId", fItemId, value)
        End Set
    End Property
    Dim fIVAId As Integer
    Public Property IVAId() As Integer
        Get
            Return fIVAId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IVAId", fIVAId, value)
        End Set
    End Property
    Dim fTrademarkId As FixedAssetTrademarkReportXpo
    <Association("FixedAsset_FixedAssetEntryItemReferencesFixedAsset_FixedAssetTrademark")>
    Public Property TrademarkId() As FixedAssetTrademarkReportXpo
        Get
            Return fTrademarkId
        End Get
        Set(ByVal value As FixedAssetTrademarkReportXpo)
            SetPropertyValue(Of FixedAssetTrademarkReportXpo)("TrademarkId", fTrademarkId, value)
        End Set
    End Property
    Dim fModel As String
    Public Property Model() As String
        Get
            Return fModel
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Model", fModel, value)
        End Set
    End Property
    Dim fPolicyId As Integer
    Public Property PolicyId() As Integer
        Get
            Return fPolicyId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PolicyId", fPolicyId, value)
        End Set
    End Property
    Dim fQuantity As Integer
    Public Property Quantity() As Integer
        Get
            Return fQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Quantity", fQuantity, value)
        End Set
    End Property
    Dim fOutstandingQuantity As Integer
    Public Property OutstandingQuantity() As Integer
        Get
            Return fOutstandingQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("OutstandingQuantity", fOutstandingQuantity, value)
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
    <Association("FixedAsset_FixedAssetEntryItemDetailReferencesFixedAsset_FixedAssetEntryItem", GetType(FixedAssetFixedAssetEntryItemDetailReportXpo))> _
    Public ReadOnly Property FixedAssetFixedAssetEntryItemDetailReportXpo() As XPCollection(Of FixedAssetFixedAssetEntryItemDetailReportXpo)
        Get
            Return GetCollection(Of FixedAssetFixedAssetEntryItemDetailReportXpo)("FixedAssetFixedAssetEntryItemDetailReportXpo")
        End Get
    End Property
    <Association("FixedAsset_FixedAssetEntryDevolutionDetailReferencesFixedAsset_FixedAssetEntryItem", GetType(FixedAssetFixedAssetEntryDevolutionDetailReportXpo))> _
    Public ReadOnly Property FixedAsset_FixedAssetEntryDevolutionDetails() As XPCollection(Of FixedAssetFixedAssetEntryDevolutionDetailReportXpo)
        Get
            Return GetCollection(Of FixedAssetFixedAssetEntryDevolutionDetailReportXpo)("FixedAsset_FixedAssetEntryDevolutionDetails")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
