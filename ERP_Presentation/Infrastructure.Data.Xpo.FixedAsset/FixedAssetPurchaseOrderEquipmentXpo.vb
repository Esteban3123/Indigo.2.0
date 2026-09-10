Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetPurchaseOrderEquipment")> _
Public Class FixedAssetPurchaseOrderEquipmentXpo
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
    Dim fIdFixedAssetPurchaseOrder As FixedAssetPurchaseOrderXpo
    <Association("FixedAssetPurchaseOrderReferences")> _
    Public Property IdFixedAssetPurchaseOrder() As FixedAssetPurchaseOrderXpo
        Get
            Return fIdFixedAssetPurchaseOrder
        End Get
        Set(ByVal value As FixedAssetPurchaseOrderXpo)
            SetPropertyValue(Of FixedAssetPurchaseOrderXpo)("IdFixedAssetPurchaseOrder", fIdFixedAssetPurchaseOrder, value)
        End Set
    End Property
    Dim fIdInventoryType As FixedAssetInventoryTypeXpo
    <Association("FixedAssetInventoryTypeReferences")> _
    Public Property IdInventoryType() As FixedAssetInventoryTypeXpo
        Get
            Return fIdInventoryType
        End Get
        Set(ByVal value As FixedAssetInventoryTypeXpo)
            SetPropertyValue(Of FixedAssetInventoryTypeXpo)("IdInventoryType", fIdInventoryType, value)
        End Set
    End Property
    <Association("FixedAssetPurchaseOrderEquipmentEquipmentType")> _
    Dim fIdEquipmentType As FixedAssetEquipmentTypeXpo
    Public Property IdEquipmentType() As FixedAssetEquipmentTypeXpo
        Get
            Return fIdEquipmentType
        End Get
        Set(ByVal value As FixedAssetEquipmentTypeXpo)
            SetPropertyValue(Of FixedAssetEquipmentTypeXpo)("IdEquipmentType", fIdEquipmentType, value)
        End Set
    End Property
    Dim fIdEquipment As FixedAssetEquipmentXpo
    <Association("FixedAssetEquipmentReferences")> _
     Public Property IdEquipment() As FixedAssetEquipmentXpo
        Get
            Return fIdEquipment
        End Get
        Set(ByVal value As FixedAssetEquipmentXpo)
            SetPropertyValue(Of FixedAssetEquipmentXpo)("IdEquipment", fIdEquipment, value)
        End Set
    End Property
    Dim fIdIvaCode As GeneralLedgerIVAXpo
    Public Property IdIvaCode() As GeneralLedgerIVAXpo
        Get
            Return fIdIvaCode
        End Get
        Set(ByVal value As GeneralLedgerIVAXpo)
            SetPropertyValue(Of GeneralLedgerIVAXpo)("IdIvaCode", fIdIvaCode, value)
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
    Dim fProductValue As Decimal
    Public Property ProductValue() As Decimal
        Get
            Return fProductValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ProductValue", fProductValue, value)
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
    Dim fTaxesValue As Decimal
    Public Property TaxesValue() As Decimal
        Get
            Return fTaxesValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TaxesValue", fTaxesValue, value)
        End Set
    End Property
    Dim fIdTrademark As FixedAssetTrademarkXpo
    <Association("FixedAssetPurchaseOrderEquipmentTrademarkReference")> _
    Public Property IdTrademark() As FixedAssetTrademarkXpo
        Get
            Return fIdTrademark
        End Get
        Set(ByVal value As FixedAssetTrademarkXpo)
            SetPropertyValue(Of FixedAssetTrademarkXpo)("IdTrademark", fIdTrademark, value)
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
    <Association("FixedAssetPolizaReferences")> _
    Dim fIdPolize As FixedAssetPolizaXpo
    Public Property IdPolize() As FixedAssetPolizaXpo
        Get
            Return fIdPolize
        End Get
        Set(ByVal value As FixedAssetPolizaXpo)
            SetPropertyValue(Of FixedAssetPolizaXpo)("IdPolize", fIdPolize, value)
        End Set
    End Property

    <Association("FixedAssetPurchaseOrderEquipmentDetailReferences", GetType(FixedAssetPurchaseOrderEquipmentDetailXpo))> _
    Public ReadOnly Property FixedAssetPurchaseOrderEquipmentDetailXpo() As XPCollection(Of FixedAssetPurchaseOrderEquipmentDetailXpo)
        Get
            Return GetCollection(Of FixedAssetPurchaseOrderEquipmentDetailXpo)("FixedAssetPurchaseOrderEquipmentDetailXpo")
        End Get
    End Property


    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class

