Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Maintenance.ConsumableDetail")>
Partial Public Class Maintenance_ConsumableDetail
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
        Dim fIdConsumable As Maintenance_Consumable
        <Association("Maintenance_ConsumableDetailReferencesMaintenance_Consumable")>
        Public Property IdConsumable() As Maintenance_Consumable
            Get
                Return fIdConsumable
            End Get
            Set(ByVal value As Maintenance_Consumable)
                SetPropertyValue(Of Maintenance_Consumable)("IdConsumable", fIdConsumable, value)
            End Set
        End Property
        Dim fIdEquipmentType As FixedAsset_FixedAssetItemType
        <Association("Maintenance_ConsumableDetailReferencesFixedAsset_FixedAssetItemType")>
        Public Property IdEquipmentType() As FixedAsset_FixedAssetItemType
            Get
                Return fIdEquipmentType
            End Get
            Set(ByVal value As FixedAsset_FixedAssetItemType)
                SetPropertyValue(Of FixedAsset_FixedAssetItemType)("IdEquipmentType", fIdEquipmentType, value)
            End Set
        End Property
End Class