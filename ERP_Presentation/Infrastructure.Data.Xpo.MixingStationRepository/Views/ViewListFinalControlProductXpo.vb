'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Duván Albeiro Mejia Cortes
' Created          : 30/09/2021
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("MixingStation.ViewListFinalControlProduct")>
Partial Public Class ViewListFinalControlProductXpo
    Inherits XPLiteObject

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub


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

    Dim fRequestPackageDetailStatusIds As String
    Public Property RequestPackageDetailStatusIds() As String
        Get
            Return fRequestPackageDetailStatusIds
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RequestPackageDetailStatusIds", fRequestPackageDetailStatusIds, value)
        End Set
    End Property

    Dim fUserName As String
    Public Property UserName() As String
        Get
            Return fUserName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UserName", fUserName, value)
        End Set
    End Property

    Dim fProductionScheduleCode As String
    Public Property ProductionScheduleCode() As String
        Get
            Return fProductionScheduleCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductionScheduleCode", fProductionScheduleCode, value)
        End Set
    End Property

    Dim fItemId As Integer
    Public Property ItemId() As Integer
        Get
            Return fItemId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ItemId", fItemId, value)
        End Set
    End Property

    Dim fItemCode As String
    Public Property ItemCode() As String
        Get
            Return fItemCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ItemCode", fItemCode, value)
        End Set
    End Property

    Dim fItemCodeName As String
    Public Property ItemCodeName() As String
        Get
            Return fItemCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ItemCodeName", fItemCodeName, value)
        End Set
    End Property

    Dim fBatchSerialId As Integer
    Public Property BatchSerialId() As Integer
        Get
            Return fBatchSerialId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("BatchSerialId", fBatchSerialId, value)
        End Set
    End Property

    Dim fBatchCode As String
    Public Property BatchCode() As String
        Get
            Return fBatchCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BatchCode", fBatchCode, value)
        End Set
    End Property

    Dim fRequestDate As Date
    Public Property RequestDate As Date
        Get
            Return fRequestDate
        End Get
        Set(value As Date)
            SetPropertyValue(Of Date)("RequestDate", fRequestDate, value)
        End Set
    End Property

    Dim fDispensingWarehouseId As Integer?
    Public Property DispensingWarehouseId() As Integer?
        Get
            Return fDispensingWarehouseId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("DispensingWarehouseId", fDispensingWarehouseId, value)
        End Set
    End Property

    Dim fDispensingWarehouseCodeName As String
    Public Property DispensingWarehouseCodeName() As String
        Get
            Return fDispensingWarehouseCodeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DispensingWarehouseCodeName", fDispensingWarehouseCodeName, value)
        End Set
    End Property

    Dim fSource As Byte
    Public Property Source() As Byte
        Get
            Return fSource
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Source", fSource, value)
        End Set
    End Property

    Dim fSourceName As String
    Public Property SourceName() As String
        Get
            Return fSourceName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SourceName", fSourceName, value)
        End Set
    End Property

    Dim fHCFARMEPDId As Integer
    Public Property HCFARMEPDId() As Integer
        Get
            Return fHCFARMEPDId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("HCFARMEPDId", fHCFARMEPDId, value)
        End Set
    End Property

    Dim fNOMCENATE As String
    Public Property NOMCENATE() As String
        Get
            Return fNOMCENATE
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NOMCENATE", fNOMCENATE, value)
        End Set
    End Property

    Dim fUFUDESCRI As String
    Public Property UFUDESCRI() As String
        Get
            Return fUFUDESCRI
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UFUDESCRI", fUFUDESCRI, value)
        End Set
    End Property

    Dim fIPCODPACI As String
    Public Property IPCODPACI() As String
        Get
            Return fIPCODPACI
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IPCODPACI", fIPCODPACI, value)
        End Set
    End Property

    Dim fIPNOMCOMP As String
    Public Property IPNOMCOMP() As String
        Get
            Return fIPNOMCOMP
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IPNOMCOMP", fIPNOMCOMP, value)
        End Set
    End Property

    Dim fRequestPackageDetailstatus As String
    Public Property RequestPackageDetailstatus() As String
        Get
            Return fRequestPackageDetailstatus
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RequestPackageDetailstatus", fRequestPackageDetailstatus, value)
        End Set
    End Property

    Dim fCampaignNumber As Integer
    Public Property CampaignNumber() As Integer
        Get
            Return fCampaignNumber
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CampaignNumber", fCampaignNumber, value)
        End Set
    End Property

    Dim fInventoryQuantity As Integer
    Public Property InventoryQuantity() As Integer
        Get
            Return fInventoryQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("InventoryQuantity", fInventoryQuantity, value)
        End Set
    End Property

    Dim fPhysicalInventoryWarehouseId As Integer
    Public Property PhysicalInventoryWarehouseId() As Integer
        Get
            Return fPhysicalInventoryWarehouseId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PhysicalInventoryWarehouseId", fPhysicalInventoryWarehouseId, value)
        End Set
    End Property

    Dim fCampaignDetailId As Integer
    Public Property CampaignDetailId() As Integer
        Get
            Return fCampaignDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CampaignDetailId", fCampaignDetailId, value)
        End Set
    End Property

    Dim fProductCost As Decimal
    Public Property ProductCost() As Decimal
        Get
            Return fProductCost
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ProductCost", fProductCost, value)
        End Set
    End Property

    Dim fPackageUnitDescription As String
    Public Property PackageUnitDescription() As String
        Get
            Return fPackageUnitDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PackageUnitDescription", fPackageUnitDescription, value)
        End Set
    End Property

    Dim fPhysicalInventoryId As Integer
    Public Property PhysicalInventoryId() As Integer
        Get
            Return fPhysicalInventoryId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PhysicalInventoryId", fPhysicalInventoryId, value)
        End Set
    End Property

    Dim fRequestMixingStationDetailId As Integer
    Public Property RequestMixingStationDetailId() As Integer
        Get
            Return fRequestMixingStationDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RequestMixingStationDetailId", fRequestMixingStationDetailId, value)
        End Set
    End Property

    Dim fProductionLineId As Integer
    Public Property ProductionLineId() As Integer
        Get
            Return fProductionLineId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ProductionLineId", fProductionLineId, value)
        End Set
    End Property

    Dim fCMconfigurationId As Integer
    Public Property CMconfigurationId() As Integer
        Get
            Return fCMconfigurationId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CMconfigurationId", fCMconfigurationId, value)
        End Set
    End Property

    Dim fPackageDescription As String
    Public Property PackageDescription() As String
        Get
            Return fPackageDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PackageDescription", fPackageDescription, value)
        End Set
    End Property
End Class