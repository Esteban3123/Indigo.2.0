Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Maintenance.ProtocolTools")>
Partial Public Class Maintenance_ProtocolTools
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
        Dim fMaintenanceProtocolId As Maintenance_MaintenanceProtocol
        <Association("Maintenance_ProtocolToolsReferencesMaintenance_MaintenanceProtocol")>
        Public Property MaintenanceProtocolId() As Maintenance_MaintenanceProtocol
            Get
                Return fMaintenanceProtocolId
            End Get
            Set(ByVal value As Maintenance_MaintenanceProtocol)
                SetPropertyValue(Of Maintenance_MaintenanceProtocol)("MaintenanceProtocolId", fMaintenanceProtocolId, value)
            End Set
        End Property
        Dim fFixedAssetPhysicalAssetId As FixedAsset_FixedAssetPhysicalAsset
        <Indexed("MaintenanceProtocolId", Name:="IX_ProtocolTools", Unique:=True)>
        <Association("Maintenance_ProtocolToolsReferencesFixedAsset_FixedAssetPhysicalAsset")>
        Public Property FixedAssetPhysicalAssetId() As FixedAsset_FixedAssetPhysicalAsset
            Get
                Return fFixedAssetPhysicalAssetId
            End Get
            Set(ByVal value As FixedAsset_FixedAssetPhysicalAsset)
                SetPropertyValue(Of FixedAsset_FixedAssetPhysicalAsset)("FixedAssetPhysicalAssetId", fFixedAssetPhysicalAssetId, value)
            End Set
        End Property
End Class