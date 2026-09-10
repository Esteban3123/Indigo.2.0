Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Indices("DateProgramated;MaintenancePlanAndMetrologyId;ProgramType", "DateProgramated;MaintenancePlanAndMetrologyId;ProgramType;FixedAssetPhysicalId")>
<Persistent("Maintenance.MaintenancePlanProgramated")>
Partial Public Class Maintenance_MaintenancePlanProgramated
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
    Dim fMaintenancePlanAndMetrologyId As Maintenance_MaintenancePlanAndMetrology
    <Association("Maintenance_MaintenancePlanProgramatedReferencesMaintenance_MaintenancePlanAndMetrology")>
    Public Property MaintenancePlanAndMetrologyId() As Maintenance_MaintenancePlanAndMetrology
        Get
            Return fMaintenancePlanAndMetrologyId
        End Get
        Set(ByVal value As Maintenance_MaintenancePlanAndMetrology)
            SetPropertyValue(Of Maintenance_MaintenancePlanAndMetrology)("MaintenancePlanAndMetrologyId", fMaintenancePlanAndMetrologyId, value)
        End Set
    End Property
    Dim fDateProgramated As DateTime
    <Indexed("MaintenancePlanAndMetrologyId", Name:="IX_MaintenancePlanProgramated", Unique:=True)>
    Public Property DateProgramated() As DateTime
        Get
            Return fDateProgramated
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DateProgramated", fDateProgramated, value)
        End Set
    End Property
    Dim fProgramType As Byte
    Public Property ProgramType() As Byte
        Get
            Return fProgramType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ProgramType", fProgramType, value)
        End Set
    End Property
    Dim fNotificated As Boolean
    Public Property Notificated() As Boolean
        Get
            Return fNotificated
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Notificated", fNotificated, value)
        End Set
    End Property
    Dim fState As Byte
    Public Property State() As Byte
        Get
            Return fState
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("State", fState, value)
        End Set
    End Property
    Dim fFixedAssetPhysicalId As FixedAsset_FixedAssetPhysicalAsset
    <Association("Maintenance_MaintenancePlanProgramatedReferencesFixedAsset_FixedAssetPhysicalAsset")>
    Public Property FixedAssetPhysicalId() As FixedAsset_FixedAssetPhysicalAsset
        Get
            Return fFixedAssetPhysicalId
        End Get
        Set(ByVal value As FixedAsset_FixedAssetPhysicalAsset)
            SetPropertyValue(Of FixedAsset_FixedAssetPhysicalAsset)("FixedAssetPhysicalId", fFixedAssetPhysicalId, value)
        End Set
    End Property
End Class