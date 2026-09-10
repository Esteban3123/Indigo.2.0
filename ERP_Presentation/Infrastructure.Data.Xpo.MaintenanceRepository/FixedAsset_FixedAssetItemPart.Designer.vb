Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetItemPart")>
Partial Public Class FixedAsset_FixedAssetItemPart
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
        Dim fFixedAssetItemId As FixedAsset_FixedAssetItem
        <Indexed("PartId", Name:="IX_FixedAssetItemPart", Unique:=True)>
        <Association("FixedAsset_FixedAssetItemPartReferencesFixedAsset_FixedAssetItem")>
        Public Property FixedAssetItemId() As FixedAsset_FixedAssetItem
            Get
                Return fFixedAssetItemId
            End Get
            Set(ByVal value As FixedAsset_FixedAssetItem)
                SetPropertyValue(Of FixedAsset_FixedAssetItem)("FixedAssetItemId", fFixedAssetItemId, value)
            End Set
        End Property
        Dim fPartId As FixedAsset_FixedAssetPartsAccesoriesConsumables
        <Association("FixedAsset_FixedAssetItemPartReferencesFixedAsset_FixedAssetPartsAccesoriesConsumables")>
        Public Property PartId() As FixedAsset_FixedAssetPartsAccesoriesConsumables
            Get
                Return fPartId
            End Get
            Set(ByVal value As FixedAsset_FixedAssetPartsAccesoriesConsumables)
                SetPropertyValue(Of FixedAsset_FixedAssetPartsAccesoriesConsumables)("PartId", fPartId, value)
            End Set
        End Property
End Class