Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetResponsible")>
Partial Public Class FixedAsset_FixedAssetResponsible
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
        Dim fCode As String
        <Indexed(Name:="IX_FixedAssetResponsible", Unique:=True)>
        <Size(20)>
        Public Property Code() As String
            Get
                Return fCode
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("Code", fCode, value)
            End Set
        End Property
        Dim fThirdPartyId As Common_ThirdParty
        <Association("FixedAsset_FixedAssetResponsibleReferencesCommon_ThirdParty")>
        Public Property ThirdPartyId() As Common_ThirdParty
            Get
                Return fThirdPartyId
            End Get
            Set(ByVal value As Common_ThirdParty)
                SetPropertyValue(Of Common_ThirdParty)("ThirdPartyId", fThirdPartyId, value)
            End Set
        End Property
        Dim fVinculationTypeId As Integer
        Public Property VinculationTypeId() As Integer
            Get
                Return fVinculationTypeId
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue(Of Integer)("VinculationTypeId", fVinculationTypeId, value)
            End Set
        End Property
        Dim fReponsibleTypeId As FixedAsset_ResponsibleType
        <Association("FixedAsset_FixedAssetResponsibleReferencesFixedAsset_ResponsibleType")>
        Public Property ReponsibleTypeId() As FixedAsset_ResponsibleType
            Get
                Return fReponsibleTypeId
            End Get
            Set(ByVal value As FixedAsset_ResponsibleType)
                SetPropertyValue(Of FixedAsset_ResponsibleType)("ReponsibleTypeId", fReponsibleTypeId, value)
            End Set
        End Property
        Dim fStatus As Boolean
        Public Property Status() As Boolean
            Get
                Return fStatus
            End Get
            Set(ByVal value As Boolean)
                SetPropertyValue(Of Boolean)("Status", fStatus, value)
            End Set
        End Property
        Dim fCreationUser As String
        <Size(20)>
        Public Property CreationUser() As String
            Get
                Return fCreationUser
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("CreationUser", fCreationUser, value)
            End Set
        End Property
        Dim fCreationDate As DateTime
        Public Property CreationDate() As DateTime
            Get
                Return fCreationDate
            End Get
            Set(ByVal value As DateTime)
                SetPropertyValue(Of DateTime)("CreationDate", fCreationDate, value)
            End Set
        End Property
        Dim fModificationUser As String
        <Size(20)>
        Public Property ModificationUser() As String
            Get
                Return fModificationUser
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("ModificationUser", fModificationUser, value)
            End Set
        End Property
        Dim fModificationDate As DateTime
        Public Property ModificationDate() As DateTime
            Get
                Return fModificationDate
            End Get
            Set(ByVal value As DateTime)
                SetPropertyValue(Of DateTime)("ModificationDate", fModificationDate, value)
            End Set
        End Property
        Dim fCapacity As Byte
        Public Property Capacity() As Byte
            Get
                Return fCapacity
            End Get
            Set(ByVal value As Byte)
                SetPropertyValue(Of Byte)("Capacity", fCapacity, value)
            End Set
        End Property
        <Association("Maintenance_EquipmentRegistrationReferencesFixedAsset_FixedAssetResponsible")>
        Public ReadOnly Property Maintenance_EquipmentRegistrations() As XPCollection(Of Maintenance_EquipmentRegistration)
            Get
                Return GetCollection(Of Maintenance_EquipmentRegistration)("Maintenance_EquipmentRegistrations")
            End Get
        End Property
        <Association("FixedAsset_FixedAssetPhysicalAssetReferencesFixedAsset_FixedAssetResponsible")>
        Public ReadOnly Property FixedAsset_FixedAssetPhysicalAssets() As XPCollection(Of FixedAsset_FixedAssetPhysicalAsset)
            Get
                Return GetCollection(Of FixedAsset_FixedAssetPhysicalAsset)("FixedAsset_FixedAssetPhysicalAssets")
            End Get
        End Property
        <Association("Maintenance_MaintenancePlanAndMetrologyReferencesFixedAsset_FixedAssetResponsible")>
        Public ReadOnly Property Maintenance_MaintenancePlanAndMetrologys() As XPCollection(Of Maintenance_MaintenancePlanAndMetrology)
            Get
                Return GetCollection(Of Maintenance_MaintenancePlanAndMetrology)("Maintenance_MaintenancePlanAndMetrologys")
            End Get
        End Property
        <Association("Maintenance_MaintenancePlanAndMetrologyReferencesFixedAsset_FixedAssetResponsible1")>
        Public ReadOnly Property Maintenance_MaintenancePlanAndMetrologys1() As XPCollection(Of Maintenance_MaintenancePlanAndMetrology)
            Get
                Return GetCollection(Of Maintenance_MaintenancePlanAndMetrology)("Maintenance_MaintenancePlanAndMetrologys1")
            End Get
        End Property
End Class