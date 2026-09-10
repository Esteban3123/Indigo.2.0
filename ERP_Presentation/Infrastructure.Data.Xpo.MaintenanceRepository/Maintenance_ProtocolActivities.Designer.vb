Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Maintenance.ProtocolActivities")>
Partial Public Class Maintenance_ProtocolActivities
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
    <Association("Maintenance_ProtocolActivitiesReferencesMaintenance_MaintenanceProtocol")>
    Public Property MaintenanceProtocolId() As Maintenance_MaintenanceProtocol
        Get
            Return fMaintenanceProtocolId
        End Get
        Set(ByVal value As Maintenance_MaintenanceProtocol)
            SetPropertyValue(Of Maintenance_MaintenanceProtocol)("MaintenanceProtocolId", fMaintenanceProtocolId, value)
        End Set
    End Property
    Dim fActivity As String
    <Indexed("MaintenanceProtocolId", Name:="IX_MaintenanceProtocolActivities", Unique:=True)>
    Public Property Activity() As String
        Get
            Return fActivity
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Activity", fActivity, value)
        End Set
    End Property
    Dim fTime As Integer
    Public Property Time() As Integer
        Get
            Return fTime
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Time", fTime, value)
        End Set
    End Property
    Dim fUnit As Byte
    Public Property Unit() As Byte
        Get
            Return fUnit
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Unit", fUnit, value)
        End Set
    End Property
End Class