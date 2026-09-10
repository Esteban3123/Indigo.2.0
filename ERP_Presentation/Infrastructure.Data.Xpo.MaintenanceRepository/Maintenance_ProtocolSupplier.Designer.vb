Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Maintenance.ProtocolSupplier")>
Partial Public Class Maintenance_ProtocolSupplier
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
        <Indexed("ProductId", Name:="IX_ProtocolSupplier", Unique:=True)>
        <Association("Maintenance_ProtocolSupplierReferencesMaintenance_MaintenanceProtocol")>
        Public Property MaintenanceProtocolId() As Maintenance_MaintenanceProtocol
            Get
                Return fMaintenanceProtocolId
            End Get
            Set(ByVal value As Maintenance_MaintenanceProtocol)
                SetPropertyValue(Of Maintenance_MaintenanceProtocol)("MaintenanceProtocolId", fMaintenanceProtocolId, value)
            End Set
        End Property
    Dim fProductId As Inventory_InventoryProduct
    <Association("Maintenance_ProtocolSupplierReferencesInventory_InventoryProduct")>
    Public Property ProductId() As Inventory_InventoryProduct
        Get
            Return fProductId
        End Get
        Set(ByVal value As Inventory_InventoryProduct)
            SetPropertyValue(Of Inventory_InventoryProduct)("ProductId", fProductId, value)
        End Set
    End Property
End Class