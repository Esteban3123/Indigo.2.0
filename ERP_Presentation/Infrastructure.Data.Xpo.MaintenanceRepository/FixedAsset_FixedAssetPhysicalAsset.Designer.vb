Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetPhysicalAsset")>
Partial Public Class FixedAsset_FixedAssetPhysicalAsset
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
    Dim fItemId As FixedAsset_FixedAssetItem
    <Association("FixedAsset_FixedAssetPhysicalAssetReferencesFixedAsset_FixedAssetItem")>
    Public Property ItemId() As FixedAsset_FixedAssetItem
        Get
            Return fItemId
        End Get
        Set(ByVal value As FixedAsset_FixedAssetItem)
            SetPropertyValue(Of FixedAsset_FixedAssetItem)("ItemId", fItemId, value)
        End Set
    End Property
    Dim fMainAccountId As Integer
    Public Property MainAccountId() As Integer
        Get
            Return fMainAccountId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MainAccountId", fMainAccountId, value)
        End Set
    End Property
    Dim fSerie As String
    <Size(50)>
    Public Property Serie() As String
        Get
            Return fSerie
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Serie", fSerie, value)
        End Set
    End Property
    Dim fPlate As String
    <Indexed(Name:="IX_FixedAssetPhysicalAsset", Unique:=True)>
    <Size(50)>
    Public Property Plate() As String
        Get
            Return fPlate
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Plate", fPlate, value)
        End Set
    End Property
    Dim fLocationId As FixedAssetFixedAssetLocationXpo
    '<Indexed("HasOutput", Name:="IX_FixedAssetPhysicalAsset_LocationIdHasOutput")>
    <Association("FixedAsset_FixedAssetPhysicalAssetReferencesFixedAsset_FixedAssetLocationXpo")>
    Public Property LocationId() As FixedAssetFixedAssetLocationXpo
        Get
            Return fLocationId
        End Get
        Set(ByVal value As FixedAssetFixedAssetLocationXpo)
            SetPropertyValue(Of FixedAssetFixedAssetLocationXpo)("LocationId", fLocationId, value)
        End Set
    End Property
    Dim fResponsibleId As FixedAsset_FixedAssetResponsible
    <Association("FixedAsset_FixedAssetPhysicalAssetReferencesFixedAsset_FixedAssetResponsible")>
    Public Property ResponsibleId() As FixedAsset_FixedAssetResponsible
        Get
            Return fResponsibleId
        End Get
        Set(ByVal value As FixedAsset_FixedAssetResponsible)
            SetPropertyValue(Of FixedAsset_FixedAssetResponsible)("ResponsibleId", fResponsibleId, value)
        End Set
    End Property
    Dim fSupplierId As Integer
    Public Property SupplierId() As Integer
        Get
            Return fSupplierId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("SupplierId", fSupplierId, value)
        End Set
    End Property
    Dim fHistoricalValue As Decimal
    Public Property HistoricalValue() As Decimal
        Get
            Return fHistoricalValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("HistoricalValue", fHistoricalValue, value)
        End Set
    End Property
    Dim fFairValue As Decimal
    Public Property FairValue() As Decimal
        Get
            Return fFairValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("FairValue", fFairValue, value)
        End Set
    End Property
    Dim fTrademarkId As FixedAssetTrademarkXpo
    <Association("FixedAsset_FixedAssetPhysicalAssetReferencesFixedAsset_FixedAssetTrademark")>
    Public Property TrademarkId() As FixedAssetTrademarkXpo
        Get
            Return fTrademarkId
        End Get
        Set(ByVal value As FixedAssetTrademarkXpo)
            SetPropertyValue(Of FixedAssetTrademarkXpo)("TrademarkId", fTrademarkId, value)
        End Set
    End Property
    Dim fModel As String
    Public Property Model() As String
        Get
            Return fModel
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Model", fModel, value)
        End Set
    End Property
    Dim fPolicyId As Integer
    Public Property PolicyId() As Integer
        Get
            Return fPolicyId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PolicyId", fPolicyId, value)
        End Set
    End Property
    Dim fHandlesWarranty As Boolean
    Public Property HandlesWarranty() As Boolean
        Get
            Return fHandlesWarranty
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("HandlesWarranty", fHandlesWarranty, value)
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
    Dim fAdquisitionDate As DateTime
    Public Property AdquisitionDate() As DateTime
        Get
            Return fAdquisitionDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("AdquisitionDate", fAdquisitionDate, value)
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
    Dim fDepreciate As Boolean
    Public Property Depreciate() As Boolean
        Get
            Return fDepreciate
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Depreciate", fDepreciate, value)
        End Set
    End Property
    Dim fHasOutput As Boolean
    <Indexed(Name:="IX_FixedAssetPhysicalAsset_HasOutput")>
    Public Property HasOutput() As Boolean
        Get
            Return fHasOutput
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("HasOutput", fHasOutput, value)
        End Set
    End Property
    Dim fOutputDate As DateTime
    Public Property OutputDate() As DateTime
        Get
            Return fOutputDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("OutputDate", fOutputDate, value)
        End Set
    End Property
    Dim fStatusAssetId As Integer
    Public Property StatusAssetId() As Integer
        Get
            Return fStatusAssetId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("StatusAssetId", fStatusAssetId, value)
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
    Dim fOutputRefund As Boolean
    Public Property OutputRefund() As Boolean
        Get
            Return fOutputRefund
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("OutputRefund", fOutputRefund, value)
        End Set
    End Property
    Dim fObservation As String
    <Size(1000)>
    Public Property Observation() As String
        Get
            Return fObservation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Observation", fObservation, value)
        End Set
    End Property
    Dim fAdquisitionType As Byte
    Public Property AdquisitionType() As Byte
        Get
            Return fAdquisitionType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("AdquisitionType", fAdquisitionType, value)
        End Set
    End Property
    Dim fNumberContractLeasing As String
    <Size(50)>
    Public Property NumberContractLeasing() As String
        Get
            Return fNumberContractLeasing
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NumberContractLeasing", fNumberContractLeasing, value)
        End Set
    End Property
    Dim fInitialDateLeasing As DateTime
    Public Property InitialDateLeasing() As DateTime
        Get
            Return fInitialDateLeasing
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("InitialDateLeasing", fInitialDateLeasing, value)
        End Set
    End Property
    Dim fEndDateLeasing As DateTime
    Public Property EndDateLeasing() As DateTime
        Get
            Return fEndDateLeasing
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("EndDateLeasing", fEndDateLeasing, value)
        End Set
    End Property
    Dim fApplyMinimunAmount As Boolean
    Public Property ApplyMinimunAmount() As Boolean
        Get
            Return fApplyMinimunAmount
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ApplyMinimunAmount", fApplyMinimunAmount, value)
        End Set
    End Property
    Dim fAdquisitionTypeReal As Byte
    Public Property AdquisitionTypeReal() As Byte
        Get
            Return fAdquisitionTypeReal
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("AdquisitionTypeReal", fAdquisitionTypeReal, value)
        End Set
    End Property
    Dim fPurchaseDate As DateTime
    Public Property PurchaseDate() As DateTime
        Get
            Return fPurchaseDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("PurchaseDate", fPurchaseDate, value)
        End Set
    End Property
    Dim fEntryNumber As String
    <Size(20)>
    Public Property EntryNumber() As String
        Get
            Return fEntryNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EntryNumber", fEntryNumber, value)
        End Set
    End Property
    Dim fVoucherTransactionNumber As String
    <Size(20)>
    Public Property VoucherTransactionNumber() As String
        Get
            Return fVoucherTransactionNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("VoucherTransactionNumber", fVoucherTransactionNumber, value)
        End Set
    End Property
    Dim fHasReclassified As Boolean
    Public Property HasReclassified() As Boolean
        Get
            Return fHasReclassified
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("HasReclassified", fHasReclassified, value)
        End Set
    End Property
    Dim fEndDateLeasingExecuted As DateTime
    Public Property EndDateLeasingExecuted() As DateTime
        Get
            Return fEndDateLeasingExecuted
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("EndDateLeasingExecuted", fEndDateLeasingExecuted, value)
        End Set
    End Property
    Dim fRecoverableValue As Decimal
    Public Property RecoverableValue() As Decimal
        Get
            Return fRecoverableValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("RecoverableValue", fRecoverableValue, value)
        End Set
    End Property
    Dim fHasHighTech As Boolean
    Public Property HasHighTech() As Boolean
        Get
            Return fHasHighTech
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("HasHighTech", fHasHighTech, value)
        End Set
    End Property
    <Association("Maintenance_ProtocolToolsReferencesFixedAsset_FixedAssetPhysicalAsset")>
    Public ReadOnly Property Maintenance_ProtocolToolss() As XPCollection(Of Maintenance_ProtocolTools)
        Get
            Return GetCollection(Of Maintenance_ProtocolTools)("Maintenance_ProtocolToolss")
        End Get
    End Property
    <Association("Maintenance_MaintenancePlanProgramatedReferencesFixedAsset_FixedAssetPhysicalAsset")>
    Public ReadOnly Property Maintenance_MaintenancePlanProgramateds() As XPCollection(Of Maintenance_MaintenancePlanProgramated)
        Get
            Return GetCollection(Of Maintenance_MaintenancePlanProgramated)("Maintenance_MaintenancePlanProgramateds")
        End Get
    End Property
    <Association("Maintenance_WorkOrderReferencesFixedAsset_FixedAssetPhysicalAsset")>
    Public ReadOnly Property Maintenance_WorkOrders() As XPCollection(Of Maintenance_WorkOrder)
        Get
            Return GetCollection(Of Maintenance_WorkOrder)("Maintenance_WorkOrders")
        End Get
    End Property
    <Association("Maintenance_EquipmentRegistrationReferencesFixedAsset_FixedAssetPhysicalAsset")>
    Public ReadOnly Property Maintenance_EquipmentRegistrations() As XPCollection(Of Maintenance_EquipmentRegistration)
        Get
            Return GetCollection(Of Maintenance_EquipmentRegistration)("Maintenance_EquipmentRegistrations")
        End Get
    End Property
End Class