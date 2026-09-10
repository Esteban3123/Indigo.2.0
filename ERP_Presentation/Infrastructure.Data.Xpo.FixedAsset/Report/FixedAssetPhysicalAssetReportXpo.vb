Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetPhysicalAsset")> _
Public Class FixedAssetPhysicalAssetReportXpo
    Inherits XPLiteObject
    Dim fId As Integer
    <Key(True)> _
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property
    Dim fItemId As FixedAssetItemReportXpo
    <Association("FixedAsset_FixedAssetPhysicalAssetReferencesFixedAsset_FixedAssetItem")> _
    Public Property ItemId() As FixedAssetItemReportXpo
        Get
            Return fItemId
        End Get
        Set(ByVal value As FixedAssetItemReportXpo)
            SetPropertyValue(Of FixedAssetItemReportXpo)("ItemId", fItemId, value)
        End Set
    End Property
    Dim fMainAccountId As GeneralLedgerMainAccountsReportXpo
    <Association("FixedAsset_FixedAssetPhysicalAssetReferencesGeneralLedger_MainAccounts")> _
    Public Property MainAccountId() As GeneralLedgerMainAccountsReportXpo
        Get
            Return fMainAccountId
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsReportXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsReportXpo)("MainAccountId", fMainAccountId, value)
        End Set
    End Property
    Dim fSerie As String
    <Size(50)> _
    Public Property Serie() As String
        Get
            Return fSerie
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Serie", fSerie, value)
        End Set
    End Property
    Dim fPlate As String
    <Size(50)> _
    Public Property Plate() As String
        Get
            Return fPlate
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Plate", fPlate, value)
        End Set
    End Property
    Dim fLocationId As FixedAssetLocationReportXpo
    <Association("FixedAsset_FixedAssetPhysicalAssetReferencesFixedAsset_FixedAssetLocation")> _
    Public Property LocationId() As FixedAssetLocationReportXpo
        Get
            Return fLocationId
        End Get
        Set(ByVal value As FixedAssetLocationReportXpo)
            SetPropertyValue(Of FixedAssetLocationReportXpo)("LocationId", fLocationId, value)
        End Set
    End Property
    Dim fResponsibleId As FixedAssetResponsibleReportXpo
    <Association("FixedAsset_FixedAssetPhysicalAssetReferencesFixedAsset_FixedAssetResponsible")> _
    Public Property ResponsibleId() As FixedAssetResponsibleReportXpo
        Get
            Return fResponsibleId
        End Get
        Set(ByVal value As FixedAssetResponsibleReportXpo)
            SetPropertyValue(Of FixedAssetResponsibleReportXpo)("ResponsibleId", fResponsibleId, value)
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
    Dim fTrademarkId As FixedAssetTrademarkReportXpo
    <Association("FixedAsset_FixedAssetPhysicalAssetReferencesFixedAsset_FixedAssetTrademark")> _
    Public Property TrademarkId() As FixedAssetTrademarkReportXpo
        Get
            Return fTrademarkId
        End Get
        Set(ByVal value As FixedAssetTrademarkReportXpo)
            SetPropertyValue(Of FixedAssetTrademarkReportXpo)("TrademarkId", fTrademarkId, value)
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
    Dim fStatusAssetId As FixedAssetStatusAssetReportXpo
    <Association("FixedAsset_FixedAssetPhysicalAssetReferencesFixedAsset_FixedAssetStatusAsset")> _
    Public Property StatusAssetId() As FixedAssetStatusAssetReportXpo
        Get
            Return fStatusAssetId
        End Get
        Set(ByVal value As FixedAssetStatusAssetReportXpo)
            SetPropertyValue(Of FixedAssetStatusAssetReportXpo)("StatusAssetId", fStatusAssetId, value)
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

    <PersistentAlias("Iif(
AdquisitionType = 1, 'Compra Directa', 
AdquisitionType = 2, 'N/A',
AdquisitionType = 3, 'Comodato',
AdquisitionType = 4, 'Donado por Particulares',
AdquisitionType = 5, 'Traspaso de Bienes',
AdquisitionType = 6, 'Otro Concepto',
AdquisitionType = 7, 'Leasing Financiero',
AdquisitionType = 8, 'Comodato Tercerizado',
AdquisitionType = 9, 'Renting Financiero',
AdquisitionType = 10, 'Renting Operativo', '')")>
    Public ReadOnly Property AdquisitionTypeName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("AdquisitionTypeName"))
        End Get
    End Property

    <Association("FixedAsset_FixedAssetPhysicalAssetDetailBookReferencesFixedAsset_FixedAssetPhysicalAsset", GetType(FixedAssetPhysicalAssetDetailBookReportXpo))> _
    Public ReadOnly Property FixedAssetPhysicalAssetDetailBookReportXpo() As XPCollection(Of FixedAssetPhysicalAssetDetailBookReportXpo)
        Get
            Return GetCollection(Of FixedAssetPhysicalAssetDetailBookReportXpo)("FixedAssetPhysicalAssetDetailBookReportXpo")
        End Get
    End Property
    <Association("FixedAsset_FixedAssetTransferDetailReferencesFixedAsset_FixedAssetPhysicalAsset", GetType(FixedAssetTransferDetailReportXpo))> _
    Public ReadOnly Property FixedAssetTransferDetailReportXpo() As XPCollection(Of FixedAssetTransferDetailReportXpo)
        Get
            Return GetCollection(Of FixedAssetTransferDetailReportXpo)("FixedAssetTransferDetailReportXpo")
        End Get
    End Property
    <Association("FixedAsset_FixedAssetActiveOutputDetailReferencesFixedAsset_FixedAssetPhysicalAsset", GetType(FixedAssetFixedAssetActiveOutputDetailReportXpo))> _
    Public ReadOnly Property FixedAssetFixedAssetActiveOutputDetailReportXpo() As XPCollection(Of FixedAssetFixedAssetActiveOutputDetailReportXpo)
        Get
            Return GetCollection(Of FixedAssetFixedAssetActiveOutputDetailReportXpo)("FixedAssetFixedAssetActiveOutputDetailReportXpo")
        End Get
    End Property
    <Association("FixedAsset_FixedAssetPhysicalAssetPartsReferencesFixedAsset_FixedAssetPhysicalAsset", GetType(FixedAssetFixedAssetPhysicalAssetPartsReportXpo))> _
    Public ReadOnly Property FixedAssetFixedAssetPhysicalAssetPartsReportXpo() As XPCollection(Of FixedAssetFixedAssetPhysicalAssetPartsReportXpo)
        Get
            Return GetCollection(Of FixedAssetFixedAssetPhysicalAssetPartsReportXpo)("FixedAssetFixedAssetPhysicalAssetPartsReportXpo")
        End Get
    End Property
    <Association("FixedAsset_FixedAssetChangePlateDetailReferencesFixedAsset_FixedAssetPhysicalAsset", GetType(FixedAssetFixedAssetChangePlateDetailReportXpo))>
    Public ReadOnly Property FixedAssetFixedAssetChangePlateDetailReportXpo() As XPCollection(Of FixedAssetFixedAssetChangePlateDetailReportXpo)
        Get
            Return GetCollection(Of FixedAssetFixedAssetChangePlateDetailReportXpo)("FixedAssetFixedAssetChangePlateDetailReportXpo")
        End Get
    End Property
    <Association("FK_FixedAssetReclassificationDetail_FixedAssetPhysicalAsset", GetType(FixedAssetReclassificationDetailReportXpo))>
    Public ReadOnly Property FixedAssetReclassificationDetailReportXpo() As XPCollection(Of FixedAssetReclassificationDetailReportXpo)
        Get
            Return GetCollection(Of FixedAssetReclassificationDetailReportXpo)("FixedAssetReclassificationDetailReportXpo")
        End Get
    End Property
    <Association("FixedAsset_FixedAssetPhysicalAssetAccessoryReferencesFixedAsset_FixedAssetPhysicalAsset", GetType(FixedAssetPhysicalAssetAccessoryReportXpo))>
    Public ReadOnly Property FixedAssetPhysicalAssetAccessoryReportXpo() As XPCollection(Of FixedAssetPhysicalAssetAccessoryReportXpo)
        Get
            Return GetCollection(Of FixedAssetPhysicalAssetAccessoryReportXpo)("FixedAssetPhysicalAssetAccessoryReportXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
