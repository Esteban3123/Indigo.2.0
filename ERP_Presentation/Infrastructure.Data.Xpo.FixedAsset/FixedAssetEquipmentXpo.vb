Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetItem")> _
Public Class FixedAssetEquipmentXpo
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
    Dim fCode As String
    <Size(20)> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fDescription As String
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property
    Dim fItemTypeId As Integer
    Public Property ItemTypeId() As Integer
        Get
            Return fItemTypeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ItemTypeId", fItemTypeId, value)
        End Set
    End Property

    Dim fItemCatalogId As FixedAssetEquipmentCatalogXpo
    <Association("ItemReferenceItemCatalog")> _
    Public Property ItemCatalogId() As FixedAssetEquipmentCatalogXpo
        Get
            Return fItemCatalogId
        End Get
        Set(ByVal value As FixedAssetEquipmentCatalogXpo)
            SetPropertyValue(Of FixedAssetEquipmentCatalogXpo)("ItemCatalogId", fItemCatalogId, value)
        End Set
    End Property

    Dim fIVAId As GeneralLedgerIVAXpo
    <Association("FixedAssetItemReferenceGeneralLegerIVA")> _
    Public Property IVAId() As GeneralLedgerIVAXpo
        Get
            Return fIVAId
        End Get
        Set(ByVal value As GeneralLedgerIVAXpo)
            SetPropertyValue(Of GeneralLedgerIVAXpo)("IVAId", fIVAId, value)
        End Set
    End Property

    Dim fLastCostItem As Decimal
    Public Property LastCostItem() As Decimal
        Get
            Return fLastCostItem
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("LastCostItem", fLastCostItem, value)
        End Set
    End Property
    Dim fObservations As String
    <Size(300)> _
    Public Property Observations() As String
        Get
            Return fObservations
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Observations", fObservations, value)
        End Set
    End Property
   
    Dim fStatus As Boolean
    Public Property Status() As Boolean
        Get
            Return fStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Status", fStatus, value)
        End Set
    End Property

    Dim fAllowDepreciate As Boolean
    Public Property AllowDepreciate() As Boolean
        Get
            Return fAllowDepreciate
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("AllowDepreciate", fAllowDepreciate, value)
        End Set
    End Property

    Dim fAmortizes As Boolean
    Public Property Amortizes() As Boolean
        Get
            Return fAmortizes
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Amortizes", fAmortizes, value)
        End Set
    End Property

    <PersistentAlias("Iif(Status = 1, 'Activo', 'Inactivo')")>
    Public ReadOnly Property StatusName As String
        Get
            'If fStatus Then
            '    Return "Activo"
            'Else
            '    Return "Inactivo"
            'End If
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

    Dim fCreationUser As String
    <Size(20)> _
    Public Property CreationUser() As String
        Get
            Return fCreationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CreationUser", fCreationUser, value)
        End Set
    End Property
    Dim fCreationDate As DateTime
    Public Property CreationDate() As DateTime
        Get
            Return fCreationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CreationDate", fCreationDate, value)
        End Set
    End Property
    Dim fModificationUser As String
    <Size(20)> _
    Public Property ModificationUser() As String
        Get
            Return fModificationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ModificationUser", fModificationUser, value)
        End Set
    End Property
    Dim fModificationDate As DateTime
    Public Property ModificationDate() As DateTime
        Get
            Return fModificationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ModificationDate", fModificationDate, value)
        End Set
    End Property

    'columna que devuelve el nit y el nombre concatenado
    <PersistentAlias("concat(Code,' - ',Description)")>
    Public ReadOnly Property CodeDescription() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeDescription"))
        End Get
    End Property

    <Association("FixedAssetEquipmentReferences", GetType(FixedAssetPurchaseOrderEquipmentXpo))> _
    Public ReadOnly Property FixedAssetPurchaseOrderEquipmentXpo() As XPCollection(Of FixedAssetPurchaseOrderEquipmentXpo)
        Get
            Return GetCollection(Of FixedAssetPurchaseOrderEquipmentXpo)("FixedAssetPurchaseOrderEquipmentXpo")
        End Get
    End Property

    <Association("FixedAssetItemDetailReferenceFixedAssetItem", GetType(FixedAssetItemDetailXpo))> _
    Public ReadOnly Property FixedAssetItemDetailXpo() As XPCollection(Of FixedAssetItemDetailXpo)
        Get
            Return GetCollection(Of FixedAssetItemDetailXpo)("FixedAssetItemDetailXpo")
        End Get
    End Property

    <Association("RemissionEntranceItemReferenceItem", GetType(FixedAssetRemissionEntranceItemXpo))> _
    Public ReadOnly Property FixedAssetRemissionEntranceItemXpo() As XPCollection(Of FixedAssetRemissionEntranceItemXpo)
        Get
            Return GetCollection(Of FixedAssetRemissionEntranceItemXpo)("FixedAssetRemissionEntranceItemXpo")
        End Get
    End Property

    <Association("PurchaseOrderItemReferenceItem", GetType(FixedAssetPurchaseOrderItemXpo))> _
    Public ReadOnly Property FixedAssetPurchaseOrderItemXpo() As XPCollection(Of FixedAssetPurchaseOrderItemXpo)
        Get
            Return GetCollection(Of FixedAssetPurchaseOrderItemXpo)("FixedAssetPurchaseOrderItemXpo")
        End Get
    End Property

    <Association("PhysicalReferenceItem", GetType(FixedAssetPhysicalAssetXpo))> _
    Public ReadOnly Property FixedAssetPhysicalAssetXpo() As XPCollection(Of FixedAssetPhysicalAssetXpo)
        Get
            Return GetCollection(Of FixedAssetPhysicalAssetXpo)("FixedAssetPhysicalAssetXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class


