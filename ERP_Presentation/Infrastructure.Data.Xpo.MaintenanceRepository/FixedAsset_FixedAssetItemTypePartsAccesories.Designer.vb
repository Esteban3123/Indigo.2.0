Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetItemTypePartsAccesories")>
Partial Public Class FixedAsset_FixedAssetItemTypePartsAccesories
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
        Dim fEquipmentTypeId As FixedAsset_FixedAssetItemType
        <Association("FixedAsset_FixedAssetItemTypePartsAccesoriesReferencesFixedAsset_FixedAssetItemType")>
        Public Property EquipmentTypeId() As FixedAsset_FixedAssetItemType
            Get
                Return fEquipmentTypeId
            End Get
            Set(ByVal value As FixedAsset_FixedAssetItemType)
                SetPropertyValue(Of FixedAsset_FixedAssetItemType)("EquipmentTypeId", fEquipmentTypeId, value)
            End Set
        End Property
        Dim fPartsAccesoriesConsumiblesId As FixedAsset_FixedAssetPartsAccesoriesConsumables
        <Association("FixedAsset_FixedAssetItemTypePartsAccesoriesReferencesFixedAsset_FixedAssetPartsAccesoriesConsumables")>
        Public Property PartsAccesoriesConsumiblesId() As FixedAsset_FixedAssetPartsAccesoriesConsumables
            Get
                Return fPartsAccesoriesConsumiblesId
            End Get
            Set(ByVal value As FixedAsset_FixedAssetPartsAccesoriesConsumables)
                SetPropertyValue(Of FixedAsset_FixedAssetPartsAccesoriesConsumables)("PartsAccesoriesConsumiblesId", fPartsAccesoriesConsumiblesId, value)
            End Set
        End Property
End Class