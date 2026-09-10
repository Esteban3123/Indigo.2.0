#Region "Imports"
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel
#End Region


<Persistent("FixedAsset.FixedAssetItem")> _
Public Class FixedAssetItemReportXpo
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
#End Region

#Region "Association"

    Dim fCatalogOfPropertyandServicesId As FixedAssetCatalogOfPropertyandServicesXpo
    <Association("FixedAsset_FixedAssetItemReferencesFixedAsset_FixedAssetCatalogOfPropertyandServices")>
    Public Property CatalogOfPropertyandServicesId As FixedAssetCatalogOfPropertyandServicesXpo
        Get
            Return fCatalogOfPropertyandServicesId
        End Get
        Set(ByVal value As FixedAssetCatalogOfPropertyandServicesXpo)
            SetPropertyValue(Of FixedAssetCatalogOfPropertyandServicesXpo)("CatalogOfPropertyandServicesId", fCatalogOfPropertyandServicesId, value)
        End Set
    End Property

    Dim fItemTypeId As FixedAssetItemTypeReportXpo
    <Association("FixedAsset_FixedAssetItemReferencesFixedAsset_FixedAssetItemType")>
    Public Property ItemTypeId() As FixedAssetItemTypeReportXpo
        Get
            Return fItemTypeId
        End Get
        Set(ByVal value As FixedAssetItemTypeReportXpo)
            SetPropertyValue(Of FixedAssetItemTypeReportXpo)("ItemTypeId", fItemTypeId, value)
        End Set
    End Property

    Dim fItemCatalogId As FixedAssetItemCatalogReportXpo
    <Association("FixedAsset_FixedAssetItemCatalogReferencesFixedAsset_FixedAssetItemCatalog")>
    Public Property ItemCatalogId() As FixedAssetItemCatalogReportXpo
        Get
            Return fItemCatalogId
        End Get
        Set(ByVal value As FixedAssetItemCatalogReportXpo)
            SetPropertyValue(Of FixedAssetItemCatalogReportXpo)("ItemCatalogId", fItemCatalogId, value)
        End Set
    End Property

    <Association("FixedAsset_FixedAssetPhysicalAssetReferencesFixedAsset_FixedAssetItem", GetType(FixedAssetPhysicalAssetReportXpo))>
    Public ReadOnly Property FixedAssetPhysicalAssetReportXpo() As XPCollection(Of FixedAssetPhysicalAssetReportXpo)
        Get
            Return GetCollection(Of FixedAssetPhysicalAssetReportXpo)("FixedAssetPhysicalAssetReportXpo")
        End Get
    End Property
    <Association("FixedAsset_FixedAssetEntryItemReferencesFixedAsset_FixedAssetItem", GetType(FixedAssetFixedAssetEntryItemReportXpo))>
    Public ReadOnly Property FixedAssetFixedAssetEntryItemReportXpo() As XPCollection(Of FixedAssetFixedAssetEntryItemReportXpo)
        Get
            Return GetCollection(Of FixedAssetFixedAssetEntryItemReportXpo)("FixedAssetFixedAssetEntryItemReportXpo")
        End Get
    End Property
    <Association("FixedAsset_FixedAssetPurchaseOrderItemReferencesFixedAsset_FixedAssetItem", GetType(FixedAssetFixedAssetPurchaseOrderItemReportXpo))>
    Public ReadOnly Property FixedAssetFixedAssetPurchaseOrderItemReportXpo() As XPCollection(Of FixedAssetFixedAssetPurchaseOrderItemReportXpo)
        Get
            Return GetCollection(Of FixedAssetFixedAssetPurchaseOrderItemReportXpo)("FixedAssetFixedAssetPurchaseOrderItemReportXpo")
        End Get
    End Property
    <Association("FixedAssetFixedAssetRemissionEntranceItemReportXpoReferencesFixedAssetItemReportXpo", GetType(FixedAssetFixedAssetRemissionEntranceItemReportXpo))>
    Public ReadOnly Property FixedAssetFixedAssetRemissionEntranceItemReportXpo() As XPCollection(Of FixedAssetFixedAssetRemissionEntranceItemReportXpo)
        Get
            Return GetCollection(Of FixedAssetFixedAssetRemissionEntranceItemReportXpo)("FixedAssetFixedAssetRemissionEntranceItemReportXpo")
        End Get
    End Property
    <Association("FK_FixedAssetReclassification_FixedAssetItemPrevious", GetType(FixedAssetReclassificationReportXpo))>
    Public ReadOnly Property FixedAssetReclassificationWithItemPreviousReportXpo() As XPCollection(Of FixedAssetReclassificationReportXpo)
        Get
            Return GetCollection(Of FixedAssetReclassificationReportXpo)("FixedAssetReclassificationWithItemPreviousReportXpo")
        End Get
    End Property
    <Association("FK_FixedAssetReclassification_FixedAssetItem", GetType(FixedAssetReclassificationReportXpo))>
    Public ReadOnly Property FixedAssetReclassificationWithItemReportXpo() As XPCollection(Of FixedAssetReclassificationReportXpo)
        Get
            Return GetCollection(Of FixedAssetReclassificationReportXpo)("FixedAssetReclassificationWithItemReportXpo")
        End Get
    End Property

#End Region

#Region "CustomMembers"

    <PersistentAlias("ItemCatalogId.Description")>
    Public ReadOnly Property ItemCatalogIdDescription() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("ItemCatalogIdDescription"))
        End Get
    End Property

    <PersistentAlias("IIF(Status, 'Activo','Inactivo' )")>
    Public ReadOnly Property StatusName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

#End Region

#Region "Builder"
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
#End Region

End Class
