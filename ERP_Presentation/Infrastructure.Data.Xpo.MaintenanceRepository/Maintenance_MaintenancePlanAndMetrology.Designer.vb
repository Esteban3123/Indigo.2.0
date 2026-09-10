Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Maintenance.MaintenancePlanAndMetrology")>
Partial Public Class Maintenance_MaintenancePlanAndMetrology
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
        Dim fInitialDateMaintenance As DateTime
        Public Property InitialDateMaintenance() As DateTime
            Get
                Return fInitialDateMaintenance
            End Get
            Set(ByVal value As DateTime)
                SetPropertyValue(Of DateTime)("InitialDateMaintenance", fInitialDateMaintenance, value)
            End Set
        End Property
        Dim fProtocolMaintenanceId As Maintenance_MaintenanceProtocol
        <Association("Maintenance_MaintenancePlanAndMetrologyReferencesMaintenance_MaintenanceProtocol")>
        Public Property ProtocolMaintenanceId() As Maintenance_MaintenanceProtocol
            Get
                Return fProtocolMaintenanceId
            End Get
            Set(ByVal value As Maintenance_MaintenanceProtocol)
                SetPropertyValue(Of Maintenance_MaintenanceProtocol)("ProtocolMaintenanceId", fProtocolMaintenanceId, value)
            End Set
        End Property
        Dim fResponsibleMaintenanceId As FixedAsset_FixedAssetResponsible
        <Association("Maintenance_MaintenancePlanAndMetrologyReferencesFixedAsset_FixedAssetResponsible")>
        Public Property ResponsibleMaintenanceId() As FixedAsset_FixedAssetResponsible
            Get
                Return fResponsibleMaintenanceId
            End Get
            Set(ByVal value As FixedAsset_FixedAssetResponsible)
                SetPropertyValue(Of FixedAsset_FixedAssetResponsible)("ResponsibleMaintenanceId", fResponsibleMaintenanceId, value)
            End Set
        End Property
        Dim fObservationMaintenance As String
        <Size(500)>
        Public Property ObservationMaintenance() As String
            Get
                Return fObservationMaintenance
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("ObservationMaintenance", fObservationMaintenance, value)
            End Set
        End Property
        Dim fSaturdaysMaintenance As Boolean
        Public Property SaturdaysMaintenance() As Boolean
            Get
                Return fSaturdaysMaintenance
            End Get
            Set(ByVal value As Boolean)
                SetPropertyValue(Of Boolean)("SaturdaysMaintenance", fSaturdaysMaintenance, value)
            End Set
        End Property
        Dim fSundaysMaintenance As Boolean
        Public Property SundaysMaintenance() As Boolean
            Get
                Return fSundaysMaintenance
            End Get
            Set(ByVal value As Boolean)
                SetPropertyValue(Of Boolean)("SundaysMaintenance", fSundaysMaintenance, value)
            End Set
        End Property
        Dim fHolidaysMaintenance As Boolean
        Public Property HolidaysMaintenance() As Boolean
            Get
                Return fHolidaysMaintenance
            End Get
            Set(ByVal value As Boolean)
                SetPropertyValue(Of Boolean)("HolidaysMaintenance", fHolidaysMaintenance, value)
            End Set
        End Property
        Dim fEquipmentFunction As Byte
        Public Property EquipmentFunction() As Byte
            Get
                Return fEquipmentFunction
            End Get
            Set(ByVal value As Byte)
                SetPropertyValue(Of Byte)("EquipmentFunction", fEquipmentFunction, value)
            End Set
        End Property
        Dim fRegisterApplication As Byte
        Public Property RegisterApplication() As Byte
            Get
                Return fRegisterApplication
            End Get
            Set(ByVal value As Byte)
                SetPropertyValue(Of Byte)("RegisterApplication", fRegisterApplication, value)
            End Set
        End Property
        Dim fMaintenanceRequirement As Byte
        Public Property MaintenanceRequirement() As Byte
            Get
                Return fMaintenanceRequirement
            End Get
            Set(ByVal value As Byte)
                SetPropertyValue(Of Byte)("MaintenanceRequirement", fMaintenanceRequirement, value)
            End Set
        End Property
        Dim fBackgrounds As Byte
        Public Property Backgrounds() As Byte
            Get
                Return fBackgrounds
            End Get
            Set(ByVal value As Byte)
                SetPropertyValue(Of Byte)("Backgrounds", fBackgrounds, value)
            End Set
        End Property
        Dim fDurationValueMaintenance As Integer
        Public Property DurationValueMaintenance() As Integer
            Get
                Return fDurationValueMaintenance
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue(Of Integer)("DurationValueMaintenance", fDurationValueMaintenance, value)
            End Set
        End Property
        Dim fDurationUnitMaintenance As Byte
        Public Property DurationUnitMaintenance() As Byte
            Get
                Return fDurationUnitMaintenance
            End Get
            Set(ByVal value As Byte)
                SetPropertyValue(Of Byte)("DurationUnitMaintenance", fDurationUnitMaintenance, value)
            End Set
        End Property
        Dim fEndDateMaintenance As DateTime
        Public Property EndDateMaintenance() As DateTime
            Get
                Return fEndDateMaintenance
            End Get
            Set(ByVal value As DateTime)
                SetPropertyValue(Of DateTime)("EndDateMaintenance", fEndDateMaintenance, value)
            End Set
        End Property
        Dim fInitialDateMetrology As DateTime
        Public Property InitialDateMetrology() As DateTime
            Get
                Return fInitialDateMetrology
            End Get
            Set(ByVal value As DateTime)
                SetPropertyValue(Of DateTime)("InitialDateMetrology", fInitialDateMetrology, value)
            End Set
        End Property
        Dim fProtocolMetrologyId As Maintenance_MaintenanceProtocol
        <Association("Maintenance_MaintenancePlanAndMetrologyReferencesMaintenance_MaintenanceProtocol1")>
        Public Property ProtocolMetrologyId() As Maintenance_MaintenanceProtocol
            Get
                Return fProtocolMetrologyId
            End Get
            Set(ByVal value As Maintenance_MaintenanceProtocol)
                SetPropertyValue(Of Maintenance_MaintenanceProtocol)("ProtocolMetrologyId", fProtocolMetrologyId, value)
            End Set
        End Property
        Dim fResponsibleMetrologyId As FixedAsset_FixedAssetResponsible
        <Association("Maintenance_MaintenancePlanAndMetrologyReferencesFixedAsset_FixedAssetResponsible1")>
        Public Property ResponsibleMetrologyId() As FixedAsset_FixedAssetResponsible
            Get
                Return fResponsibleMetrologyId
            End Get
            Set(ByVal value As FixedAsset_FixedAssetResponsible)
                SetPropertyValue(Of FixedAsset_FixedAssetResponsible)("ResponsibleMetrologyId", fResponsibleMetrologyId, value)
            End Set
        End Property
        Dim fObservationsMetrology As String
        <Size(500)>
        Public Property ObservationsMetrology() As String
            Get
                Return fObservationsMetrology
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("ObservationsMetrology", fObservationsMetrology, value)
            End Set
        End Property
        Dim fFrequenceMetrologyValue As Integer
        Public Property FrequenceMetrologyValue() As Integer
            Get
                Return fFrequenceMetrologyValue
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue(Of Integer)("FrequenceMetrologyValue", fFrequenceMetrologyValue, value)
            End Set
        End Property
        Dim fFrequenceMetrologyUnit As Byte
        Public Property FrequenceMetrologyUnit() As Byte
            Get
                Return fFrequenceMetrologyUnit
            End Get
            Set(ByVal value As Byte)
                SetPropertyValue(Of Byte)("FrequenceMetrologyUnit", fFrequenceMetrologyUnit, value)
            End Set
        End Property
        Dim fDurationValueMetrology As Integer
        Public Property DurationValueMetrology() As Integer
            Get
                Return fDurationValueMetrology
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue(Of Integer)("DurationValueMetrology", fDurationValueMetrology, value)
            End Set
        End Property
        Dim fDurationUnitMetrology As Byte
        Public Property DurationUnitMetrology() As Byte
            Get
                Return fDurationUnitMetrology
            End Get
            Set(ByVal value As Byte)
                SetPropertyValue(Of Byte)("DurationUnitMetrology", fDurationUnitMetrology, value)
            End Set
        End Property
        Dim fEndDateMetrology As DateTime
        Public Property EndDateMetrology() As DateTime
            Get
                Return fEndDateMetrology
            End Get
            Set(ByVal value As DateTime)
                SetPropertyValue(Of DateTime)("EndDateMetrology", fEndDateMetrology, value)
            End Set
        End Property
        <Association("Maintenance_MaintenancePlanProgramatedReferencesMaintenance_MaintenancePlanAndMetrology")>
        Public ReadOnly Property Maintenance_MaintenancePlanProgramateds() As XPCollection(Of Maintenance_MaintenancePlanProgramated)
            Get
                Return GetCollection(Of Maintenance_MaintenancePlanProgramated)("Maintenance_MaintenancePlanProgramateds")
            End Get
        End Property
End Class