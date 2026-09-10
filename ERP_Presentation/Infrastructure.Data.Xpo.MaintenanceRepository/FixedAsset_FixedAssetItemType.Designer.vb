Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetItemType")>
Partial Public Class FixedAsset_FixedAssetItemType
        Inherits XPLiteObject
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
        Dim fParentId As FixedAsset_FixedAssetItemType
        <Association("FixedAsset_FixedAssetItemTypeReferencesFixedAsset_FixedAssetItemType")>
        Public Property ParentId() As FixedAsset_FixedAssetItemType
            Get
                Return fParentId
            End Get
            Set(ByVal value As FixedAsset_FixedAssetItemType)
                SetPropertyValue(Of FixedAsset_FixedAssetItemType)("ParentId", fParentId, value)
            End Set
        End Property
        Dim fCode As String
        <Size(20)>
        Public Property Code() As String
            Get
                Return fCode
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("Code", fCode, value)
            End Set
        End Property
        Dim fName As String
        <Size(50)>
        Public Property Name() As String
            Get
                Return fName
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("Name", fName, value)
            End Set
        End Property
        Dim fInventoryTypeId As Integer
        Public Property InventoryTypeId() As Integer
            Get
                Return fInventoryTypeId
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue(Of Integer)("InventoryTypeId", fInventoryTypeId, value)
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
        Dim fCreationUser As String
        <Size(20)>
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
        <Size(20)>
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
        <Association("FixedAsset_FixedAssetItemTypeReferencesFixedAsset_FixedAssetItemType")>
        Public ReadOnly Property FixedAsset_FixedAssetItemTypeCollection() As XPCollection(Of FixedAsset_FixedAssetItemType)
            Get
                Return GetCollection(Of FixedAsset_FixedAssetItemType)("FixedAsset_FixedAssetItemTypeCollection")
            End Get
        End Property
        <Association("Maintenance_EquipmentRegistrationReferencesFixedAsset_FixedAssetItemType")>
        Public ReadOnly Property Maintenance_EquipmentRegistrations() As XPCollection(Of Maintenance_EquipmentRegistration)
            Get
                Return GetCollection(Of Maintenance_EquipmentRegistration)("Maintenance_EquipmentRegistrations")
            End Get
        End Property
        <Association("Maintenance_AccesoryDetailReferencesFixedAsset_FixedAssetItemType")>
        Public ReadOnly Property Maintenance_AccesoryDetails() As XPCollection(Of Maintenance_AccesoryDetail)
            Get
                Return GetCollection(Of Maintenance_AccesoryDetail)("Maintenance_AccesoryDetails")
            End Get
        End Property
        <Association("Maintenance_ConsumableDetailReferencesFixedAsset_FixedAssetItemType")>
        Public ReadOnly Property Maintenance_ConsumableDetails() As XPCollection(Of Maintenance_ConsumableDetail)
            Get
                Return GetCollection(Of Maintenance_ConsumableDetail)("Maintenance_ConsumableDetails")
            End Get
        End Property
        <Association("FixedAsset_FixedAssetItemTypePartsAccesoriesReferencesFixedAsset_FixedAssetItemType")>
        Public ReadOnly Property FixedAsset_FixedAssetItemTypePartsAccesoriess() As XPCollection(Of FixedAsset_FixedAssetItemTypePartsAccesories)
            Get
                Return GetCollection(Of FixedAsset_FixedAssetItemTypePartsAccesories)("FixedAsset_FixedAssetItemTypePartsAccesoriess")
            End Get
        End Property
        <Association("FixedAsset_FixedAssetItemReferencesFixedAsset_FixedAssetItemType")>
        Public ReadOnly Property FixedAsset_FixedAssetItems() As XPCollection(Of FixedAsset_FixedAssetItem)
            Get
                Return GetCollection(Of FixedAsset_FixedAssetItem)("FixedAsset_FixedAssetItems")
            End Get
        End Property
End Class