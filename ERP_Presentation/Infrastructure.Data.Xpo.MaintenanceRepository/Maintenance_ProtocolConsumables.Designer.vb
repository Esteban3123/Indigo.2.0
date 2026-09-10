Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Maintenance.ProtocolConsumables")>
Partial Public Class Maintenance_ProtocolConsumables
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
        <Indexed("ConsumableId", Name:="IX_ProtocolConsumables", Unique:=True)>
        <Association("Maintenance_ProtocolConsumablesReferencesMaintenance_MaintenanceProtocol")>
        Public Property MaintenanceProtocolId() As Maintenance_MaintenanceProtocol
            Get
                Return fMaintenanceProtocolId
            End Get
            Set(ByVal value As Maintenance_MaintenanceProtocol)
                SetPropertyValue(Of Maintenance_MaintenanceProtocol)("MaintenanceProtocolId", fMaintenanceProtocolId, value)
            End Set
        End Property
        Dim fConsumableId As Maintenance_Consumable
        <Association("Maintenance_ProtocolConsumablesReferencesMaintenance_Consumable")>
        Public Property ConsumableId() As Maintenance_Consumable
            Get
                Return fConsumableId
            End Get
            Set(ByVal value As Maintenance_Consumable)
                SetPropertyValue(Of Maintenance_Consumable)("ConsumableId", fConsumableId, value)
            End Set
        End Property
End Class