Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetItemProtocol")>
Partial Public Class FixedAsset_FixedAssetItemProtocol
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
        <Indexed("MaintenanceProtocolId", Name:="IX_FixedAssetItemProtocol", Unique:=True)>
        <Association("FixedAsset_FixedAssetItemProtocolReferencesFixedAsset_FixedAssetItem")>
        Public Property FixedAssetItemId() As FixedAsset_FixedAssetItem
            Get
                Return fFixedAssetItemId
            End Get
            Set(ByVal value As FixedAsset_FixedAssetItem)
                SetPropertyValue(Of FixedAsset_FixedAssetItem)("FixedAssetItemId", fFixedAssetItemId, value)
            End Set
        End Property
        Dim fMaintenanceProtocolId As Maintenance_MaintenanceProtocol
        <Association("FixedAsset_FixedAssetItemProtocolReferencesMaintenance_MaintenanceProtocol")>
        Public Property MaintenanceProtocolId() As Maintenance_MaintenanceProtocol
            Get
                Return fMaintenanceProtocolId
            End Get
            Set(ByVal value As Maintenance_MaintenanceProtocol)
                SetPropertyValue(Of Maintenance_MaintenanceProtocol)("MaintenanceProtocolId", fMaintenanceProtocolId, value)
            End Set
        End Property
End Class