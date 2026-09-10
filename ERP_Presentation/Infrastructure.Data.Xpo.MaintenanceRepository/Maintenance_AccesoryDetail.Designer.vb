Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Maintenance.AccesoryDetail")>
Partial Public Class Maintenance_AccesoryDetail
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
        Dim fIdAccessory As Maintenance_Accessory
        <Association("Maintenance_AccesoryDetailReferencesMaintenance_Accessory")>
        Public Property IdAccessory() As Maintenance_Accessory
            Get
                Return fIdAccessory
            End Get
            Set(ByVal value As Maintenance_Accessory)
                SetPropertyValue(Of Maintenance_Accessory)("IdAccessory", fIdAccessory, value)
            End Set
        End Property
        Dim fIdEquipmentType As FixedAsset_FixedAssetItemType
        <Association("Maintenance_AccesoryDetailReferencesFixedAsset_FixedAssetItemType")>
        Public Property IdEquipmentType() As FixedAsset_FixedAssetItemType
            Get
                Return fIdEquipmentType
            End Get
            Set(ByVal value As FixedAsset_FixedAssetItemType)
                SetPropertyValue(Of FixedAsset_FixedAssetItemType)("IdEquipmentType", fIdEquipmentType, value)
            End Set
        End Property
End Class