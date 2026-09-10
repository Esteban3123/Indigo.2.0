Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetPurchaseOrderItem")> _
Public Class FixedAssetFixedAssetPurchaseOrderItemReportXpo
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
    Dim fPurchaseOrderId As FixedAssetFixedAssetPurchaseOrderReportXpo
    <Association("FixedAsset_FixedAssetPurchaseOrderItemReferencesFixedAsset_FixedAssetPurchaseOrder")> _
    Public Property PurchaseOrderId() As FixedAssetFixedAssetPurchaseOrderReportXpo
        Get
            Return fPurchaseOrderId
        End Get
        Set(ByVal value As FixedAssetFixedAssetPurchaseOrderReportXpo)
            SetPropertyValue(Of FixedAssetFixedAssetPurchaseOrderReportXpo)("PurchaseOrderId", fPurchaseOrderId, value)
        End Set
    End Property
    Dim fItemId As FixedAssetItemReportXpo
    <Association("FixedAsset_FixedAssetPurchaseOrderItemReferencesFixedAsset_FixedAssetItem")> _
    Public Property ItemId() As FixedAssetItemReportXpo
        Get
            Return fItemId
        End Get
        Set(ByVal value As FixedAssetItemReportXpo)
            SetPropertyValue(Of FixedAssetItemReportXpo)("ItemId", fItemId, value)
        End Set
    End Property
    Dim fTrademarkId As Integer
    Public Property TrademarkId() As Integer
        Get
            Return fTrademarkId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("TrademarkId", fTrademarkId, value)
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
    Dim fIVAId As Integer
    Public Property IVAId() As Integer
        Get
            Return fIVAId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IVAId", fIVAId, value)
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
    Dim fCancelledQuantity As Integer
    Public Property CancelledQuantity() As Integer
        Get
            Return fCancelledQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CancelledQuantity", fCancelledQuantity, value)
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
    <Association("FixedAsset_FixedAssetEntryItemReferencesFixedAsset_FixedAssetPurchaseOrderItem", GetType(FixedAssetFixedAssetEntryItemReportXpo))> _
    Public ReadOnly Property FixedAssetFixedAssetEntryItemReportXpo() As XPCollection(Of FixedAssetFixedAssetEntryItemReportXpo)
        Get
            Return GetCollection(Of FixedAssetFixedAssetEntryItemReportXpo)("FixedAssetFixedAssetEntryItemReportXpo")
        End Get
    End Property


    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
