Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetItem")>
Partial Public Class FixedAsset_FixedAssetItem
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
        Dim fCode As String
        <Indexed(Name:="IX_FixedAssetItem", Unique:=True)>
        <Size(20)>
        Public Property Code() As String
            Get
                Return fCode
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("Code", fCode, value)
            End Set
        End Property
        Dim fDescription As String
        <Size(300)>
        Public Property Description() As String
            Get
                Return fDescription
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("Description", fDescription, value)
            End Set
        End Property
        Dim fItemTypeId As FixedAsset_FixedAssetItemType
        <Association("FixedAsset_FixedAssetItemReferencesFixedAsset_FixedAssetItemType")>
        Public Property ItemTypeId() As FixedAsset_FixedAssetItemType
            Get
                Return fItemTypeId
            End Get
            Set(ByVal value As FixedAsset_FixedAssetItemType)
                SetPropertyValue(Of FixedAsset_FixedAssetItemType)("ItemTypeId", fItemTypeId, value)
            End Set
        End Property
        Dim fItemCatalogId As FixedAssetEquipmentCatalogXpo
        <Association("FixedAsset_FixedAssetItemReferencesFixedAsset_FixedAssetItemCatalog")>
        Public Property ItemCatalogId() As FixedAssetEquipmentCatalogXpo
            Get
                Return fItemCatalogId
            End Get
            Set(ByVal value As FixedAssetEquipmentCatalogXpo)
            SetPropertyValue(Of FixedAssetEquipmentCatalogXpo)("ItemCatalogId", fItemCatalogId, value)
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
        <Size(300)>
        Public Property Observations() As String
            Get
                Return fObservations
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("Observations", fObservations, value)
            End Set
        End Property
        Dim fFairValue As Decimal
        Public Property FairValue() As Decimal
            Get
                Return fFairValue
            End Get
            Set(ByVal value As Decimal)
                SetPropertyValue(Of Decimal)("FairValue", fFairValue, value)
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
        Dim fDepreciateByTimeUse As Boolean
        Public Property DepreciateByTimeUse() As Boolean
            Get
                Return fDepreciateByTimeUse
            End Get
            Set(ByVal value As Boolean)
                SetPropertyValue(Of Boolean)("DepreciateByTimeUse", fDepreciateByTimeUse, value)
            End Set
        End Property
        <Association("FixedAsset_FixedAssetItemAccesoryReferencesFixedAsset_FixedAssetItem")>
        Public ReadOnly Property FixedAsset_FixedAssetItemAccesorys() As XPCollection(Of FixedAsset_FixedAssetItemAccesory)
            Get
                Return GetCollection(Of FixedAsset_FixedAssetItemAccesory)("FixedAsset_FixedAssetItemAccesorys")
            End Get
        End Property
        <Association("FixedAsset_FixedAssetItemConsumibleReferencesFixedAsset_FixedAssetItem")>
        Public ReadOnly Property FixedAsset_FixedAssetItemConsumibles() As XPCollection(Of FixedAsset_FixedAssetItemConsumible)
            Get
                Return GetCollection(Of FixedAsset_FixedAssetItemConsumible)("FixedAsset_FixedAssetItemConsumibles")
            End Get
        End Property
        <Association("FixedAsset_FixedAssetItemPartReferencesFixedAsset_FixedAssetItem")>
        Public ReadOnly Property FixedAsset_FixedAssetItemParts() As XPCollection(Of FixedAsset_FixedAssetItemPart)
            Get
                Return GetCollection(Of FixedAsset_FixedAssetItemPart)("FixedAsset_FixedAssetItemParts")
            End Get
        End Property
        <Association("FixedAsset_FixedAssetItemProtocolReferencesFixedAsset_FixedAssetItem")>
        Public ReadOnly Property FixedAsset_FixedAssetItemProtocols() As XPCollection(Of FixedAsset_FixedAssetItemProtocol)
            Get
                Return GetCollection(Of FixedAsset_FixedAssetItemProtocol)("FixedAsset_FixedAssetItemProtocols")
            End Get
        End Property
        <Association("FixedAsset_FixedAssetItemTechnicalLogReferencesFixedAsset_FixedAssetItem")>
        Public ReadOnly Property FixedAsset_FixedAssetItemTechnicalLogs() As XPCollection(Of FixedAsset_FixedAssetItemTechnicalLog)
            Get
                Return GetCollection(Of FixedAsset_FixedAssetItemTechnicalLog)("FixedAsset_FixedAssetItemTechnicalLogs")
            End Get
        End Property
        <Association("FixedAsset_FixedAssetPhysicalAssetReferencesFixedAsset_FixedAssetItem")>
        Public ReadOnly Property FixedAsset_FixedAssetPhysicalAssets() As XPCollection(Of FixedAsset_FixedAssetPhysicalAsset)
            Get
                Return GetCollection(Of FixedAsset_FixedAssetPhysicalAsset)("FixedAsset_FixedAssetPhysicalAssets")
            End Get
        End Property
        <Association("Maintenance_MaintenanceProtocolReferencesFixedAsset_FixedAssetItem")>
        Public ReadOnly Property Maintenance_MaintenanceProtocols() As XPCollection(Of Maintenance_MaintenanceProtocol)
            Get
                Return GetCollection(Of Maintenance_MaintenanceProtocol)("Maintenance_MaintenanceProtocols")
            End Get
        End Property
End Class