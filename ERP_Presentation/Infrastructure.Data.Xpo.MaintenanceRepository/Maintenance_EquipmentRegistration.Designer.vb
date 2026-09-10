Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Maintenance.EquipmentRegistration")>
Partial Public Class Maintenance_EquipmentRegistration
    Inherits XPLiteObject

#Region "Members"

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
        Dim fFixedAssetPhysicalAssetId As FixedAsset_FixedAssetPhysicalAsset
        <Indexed(Name:="IX_EquipmentRegistration_1", Unique:=True)>
        <Association("Maintenance_EquipmentRegistrationReferencesFixedAsset_FixedAssetPhysicalAsset")>
        Public Property FixedAssetPhysicalAssetId() As FixedAsset_FixedAssetPhysicalAsset
            Get
                Return fFixedAssetPhysicalAssetId
            End Get
            Set(ByVal value As FixedAsset_FixedAssetPhysicalAsset)
                SetPropertyValue(Of FixedAsset_FixedAssetPhysicalAsset)("FixedAssetPhysicalAssetId", fFixedAssetPhysicalAssetId, value)
            End Set
        End Property
        Dim fInventoryTypeId As Integer
        Public Property InventoryTypeId() As Integer
            Get
                Return fInventoryTypeId
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue(Of Integer)("InventoryTypeId", fInventoryTypeId, value)
            End Set
        End Property
        Dim fEquipmentTypeId As FixedAsset_FixedAssetItemType
        <Association("Maintenance_EquipmentRegistrationReferencesFixedAsset_FixedAssetItemType")>
        Public Property EquipmentTypeId() As FixedAsset_FixedAssetItemType
            Get
                Return fEquipmentTypeId
            End Get
            Set(ByVal value As FixedAsset_FixedAssetItemType)
                SetPropertyValue(Of FixedAsset_FixedAssetItemType)("EquipmentTypeId", fEquipmentTypeId, value)
            End Set
        End Property
        Dim fIdManufacturer As Integer
        Public Property IdManufacturer() As Integer
            Get
                Return fIdManufacturer
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue(Of Integer)("IdManufacturer", fIdManufacturer, value)
            End Set
        End Property
        Dim fIdSeller As Integer
        Public Property IdSeller() As Integer
            Get
                Return fIdSeller
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue(Of Integer)("IdSeller", fIdSeller, value)
            End Set
        End Property
        Dim fIdResponsible As FixedAsset_FixedAssetResponsible
        <Association("Maintenance_EquipmentRegistrationReferencesFixedAsset_FixedAssetResponsible")>
        Public Property IdResponsible() As FixedAsset_FixedAssetResponsible
            Get
                Return fIdResponsible
            End Get
            Set(ByVal value As FixedAsset_FixedAssetResponsible)
                SetPropertyValue(Of FixedAsset_FixedAssetResponsible)("IdResponsible", fIdResponsible, value)
            End Set
        End Property
        Dim fManufactureDate As DateTime
        Public Property ManufactureDate() As DateTime
            Get
                Return fManufactureDate
            End Get
            Set(ByVal value As DateTime)
                SetPropertyValue(Of DateTime)("ManufactureDate", fManufactureDate, value)
            End Set
        End Property
        Dim fInstallationDate As DateTime
        Public Property InstallationDate() As DateTime
            Get
                Return fInstallationDate
            End Get
            Set(ByVal value As DateTime)
                SetPropertyValue(Of DateTime)("InstallationDate", fInstallationDate, value)
            End Set
        End Property
        Dim fInitialOperationDate As DateTime
        Public Property InitialOperationDate() As DateTime
            Get
                Return fInitialOperationDate
            End Get
            Set(ByVal value As DateTime)
                SetPropertyValue(Of DateTime)("InitialOperationDate", fInitialOperationDate, value)
            End Set
        End Property
        Dim fWarrantyExpirationDate As DateTime
        Public Property WarrantyExpirationDate() As DateTime
            Get
                Return fWarrantyExpirationDate
            End Get
            Set(ByVal value As DateTime)
                SetPropertyValue(Of DateTime)("WarrantyExpirationDate", fWarrantyExpirationDate, value)
            End Set
        End Property
        Dim fLifeTime As Integer
        Public Property LifeTime() As Integer
            Get
                Return fLifeTime
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue(Of Integer)("LifeTime", fLifeTime, value)
            End Set
        End Property
        Dim fMeasurementUnit As Char
        Public Property MeasurementUnit() As Char
            Get
                Return fMeasurementUnit
            End Get
            Set(ByVal value As Char)
                SetPropertyValue(Of Char)("MeasurementUnit", fMeasurementUnit, value)
            End Set
        End Property
        Dim fFeedingSource As Char
        Public Property FeedingSource() As Char
            Get
                Return fFeedingSource
            End Get
            Set(ByVal value As Char)
                SetPropertyValue(Of Char)("FeedingSource", fFeedingSource, value)
            End Set
        End Property
        Dim fFrequencyComputerUse As Char
        Public Property FrequencyComputerUse() As Char
            Get
                Return fFrequencyComputerUse
            End Get
            Set(ByVal value As Char)
                SetPropertyValue(Of Char)("FrequencyComputerUse", fFrequencyComputerUse, value)
            End Set
        End Property
        Dim fPredominantTechnology As Char
        Public Property PredominantTechnology() As Char
            Get
                Return fPredominantTechnology
            End Get
            Set(ByVal value As Char)
                SetPropertyValue(Of Char)("PredominantTechnology", fPredominantTechnology, value)
            End Set
        End Property
        Dim fIdEquipmentFunction As Maintenance_EquipmentFunction
        <Association("Maintenance_EquipmentRegistrationReferencesMaintenance_EquipmentFunction")>
        Public Property IdEquipmentFunction() As Maintenance_EquipmentFunction
            Get
                Return fIdEquipmentFunction
            End Get
            Set(ByVal value As Maintenance_EquipmentFunction)
                SetPropertyValue(Of Maintenance_EquipmentFunction)("IdEquipmentFunction", fIdEquipmentFunction, value)
            End Set
        End Property
        Dim fUse As Char
        Public Property Use() As Char
            Get
                Return fUse
            End Get
            Set(ByVal value As Char)
                SetPropertyValue(Of Char)("Use", fUse, value)
            End Set
        End Property
        Dim fIdPhysicalRisk As Maintenance_PhysicalRisk
        <Association("Maintenance_EquipmentRegistrationReferencesMaintenance_PhysicalRisk")>
        Public Property IdPhysicalRisk() As Maintenance_PhysicalRisk
            Get
                Return fIdPhysicalRisk
            End Get
            Set(ByVal value As Maintenance_PhysicalRisk)
                SetPropertyValue(Of Maintenance_PhysicalRisk)("IdPhysicalRisk", fIdPhysicalRisk, value)
            End Set
        End Property
        Dim fIdEquipmentRequirement As Maintenance_EquipmentRequirement
        <Association("Maintenance_EquipmentRegistrationReferencesMaintenance_EquipmentRequirement")>
        Public Property IdEquipmentRequirement() As Maintenance_EquipmentRequirement
            Get
                Return fIdEquipmentRequirement
            End Get
            Set(ByVal value As Maintenance_EquipmentRequirement)
                SetPropertyValue(Of Maintenance_EquipmentRequirement)("IdEquipmentRequirement", fIdEquipmentRequirement, value)
            End Set
        End Property
        Dim fIdEquipmentHistory As Maintenance_EquipmentHistory
        <Association("Maintenance_EquipmentRegistrationReferencesMaintenance_EquipmentHistory")>
        Public Property IdEquipmentHistory() As Maintenance_EquipmentHistory
            Get
                Return fIdEquipmentHistory
            End Get
            Set(ByVal value As Maintenance_EquipmentHistory)
                SetPropertyValue(Of Maintenance_EquipmentHistory)("IdEquipmentHistory", fIdEquipmentHistory, value)
            End Set
        End Property
        Dim fPhoto() As Byte
        <Size(SizeAttribute.Unlimited)>
        <MemberDesignTimeVisibility(True)>
        Public Property Photo() As Byte()
            Get
                Return fPhoto
            End Get
            Set(ByVal value() As Byte)
                SetPropertyValue(Of Byte())("Photo", fPhoto, value)
            End Set
        End Property
        Dim fState As Boolean
    Public Property State() As Boolean
        Get
            Return fState
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("State", fState, value)
        End Set
    End Property

#End Region

#Region "Association Members"

    <Association("Maintenance_TechnicalLogDetailReferencesMaintenance_EquipmentRegistration")>
    Public ReadOnly Property Maintenance_TechnicalLogDetails() As XPCollection(Of Maintenance_TechnicalLogDetail)
        Get
            Return GetCollection(Of Maintenance_TechnicalLogDetail)("Maintenance_TechnicalLogDetails")
        End Get
    End Property

    <Association("Maintenance_EquipmentInvimaReferencesMaintenance_EquipmentRegistration")>
    Public ReadOnly Property Maintenance_MaintenanceEquipmentInvimas() As XPCollection(Of MaintenanceEquipmentInvimaXpo)
        Get
            Return GetCollection(Of MaintenanceEquipmentInvimaXpo)("Maintenance_MaintenanceEquipmentInvimas")
        End Get
    End Property

#End Region


End Class