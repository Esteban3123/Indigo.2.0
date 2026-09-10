Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetItemAccesory")>
Partial Public Class FixedAsset_FixedAssetItemAccesory
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
        <Association("FixedAsset_FixedAssetItemAccesoryReferencesFixedAsset_FixedAssetItem")>
        Public Property FixedAssetItemId() As FixedAsset_FixedAssetItem
            Get
                Return fFixedAssetItemId
            End Get
            Set(ByVal value As FixedAsset_FixedAssetItem)
                SetPropertyValue(Of FixedAsset_FixedAssetItem)("FixedAssetItemId", fFixedAssetItemId, value)
            End Set
        End Property
        Dim fAccesoryId As Maintenance_Accessory
        <Indexed("FixedAssetItemId", Name:="IX_FixedAssetItemAccesory", Unique:=True)>
        <Association("FixedAsset_FixedAssetItemAccesoryReferencesMaintenance_Accessory")>
        Public Property AccesoryId() As Maintenance_Accessory
            Get
                Return fAccesoryId
            End Get
            Set(ByVal value As Maintenance_Accessory)
                SetPropertyValue(Of Maintenance_Accessory)("AccesoryId", fAccesoryId, value)
            End Set
        End Property
End Class