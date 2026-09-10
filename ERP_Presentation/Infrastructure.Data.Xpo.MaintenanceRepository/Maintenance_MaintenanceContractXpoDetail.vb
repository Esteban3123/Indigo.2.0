#Region "Imports"

Imports System
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository


#End Region

<Persistent("Maintenance.MaintenanceContractDetail")>
Public Class MaintenanceContractDetailXpo
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

    Dim fMaintenanceContractId As MaintenanceContractXpo
    <Association("MaintenanceContractDetailReferences_MaintenanceContract")>
    Public Property MaintenanceContractId() As MaintenanceContractXpo
        Get
            Return fMaintenanceContractId
        End Get
        Set(ByVal value As MaintenanceContractXpo)
            SetPropertyValue(Of MaintenanceContractXpo)("MaintenanceContractId", fMaintenanceContractId, value)
        End Set
    End Property

    Dim fPhysicalAssetId As FixedAssetPhysicalAssetXpo
    <Association("MaintenanceContractDetailReferences_FixedAssetPhysicalAsset")>
    Public Property PhysicalAssetId() As FixedAssetPhysicalAssetXpo
        Get
            Return fPhysicalAssetId
        End Get
        Set(ByVal value As FixedAssetPhysicalAssetXpo)
            SetPropertyValue(Of FixedAssetPhysicalAssetXpo)("PhysicalAssetId", fPhysicalAssetId, value)
        End Set
    End Property

#End Region


#Region "Builder"
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
#End Region

End Class
