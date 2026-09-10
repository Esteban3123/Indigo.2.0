Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetEntry")>
Public Class FixedAssetFixedAssetEntryReportXpo
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
    Dim fOperatingUnitId As CommonOperartionUnitReportXpo
    <Association("FixedAsset_FixedAssetEntryReferencesCommon_OperatingUnit")>
    Public Property OperatingUnitId() As CommonOperartionUnitReportXpo
        Get
            Return fOperatingUnitId
        End Get
        Set(ByVal value As CommonOperartionUnitReportXpo)
            SetPropertyValue(Of CommonOperartionUnitReportXpo)("OperatingUnitId", fOperatingUnitId, value)
        End Set
    End Property
    Dim fCode As String
    <Size(20)>
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fEntryDate As DateTime
    Public Property EntryDate() As DateTime
        Get
            Return fEntryDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("EntryDate", fEntryDate, value)
        End Set
    End Property
    Dim fEntryNumber As String
    Public Property EntryNumber() As String
        Get
            Return fEntryNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EntryNumber", fEntryNumber, value)
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
    Dim fSupplierId As CommonSupplierReportXpo
    <Association("FixedAssetFixedAssetEntryReportXpoReferencesCommonSupplierReportXpo")>
    Public Property SupplierId() As CommonSupplierReportXpo
        Get
            Return fSupplierId
        End Get
        Set(ByVal value As CommonSupplierReportXpo)
            SetPropertyValue(Of CommonSupplierReportXpo)("SupplierId", fSupplierId, value)
        End Set
    End Property
    Dim fSupplierDistributionLineId As Integer
    Public Property SupplierDistributionLineId() As Integer
        Get
            Return fSupplierDistributionLineId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("SupplierDistributionLineId", fSupplierDistributionLineId, value)
        End Set
    End Property
    Dim fSupplierTypeId As Integer
    Public Property SupplierTypeId() As Integer
        Get
            Return fSupplierTypeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("SupplierTypeId", fSupplierTypeId, value)
        End Set
    End Property
    Dim fCostCenterId As Integer
    Public Property CostCenterId() As Integer
        Get
            Return fCostCenterId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CostCenterId", fCostCenterId, value)
        End Set
    End Property
    Dim fAccountPayableId As PaymentsAccountPayableReportXpo
    <Association("FixedAsset_FixedAssetEntryReferencesPayments_AccountPayable")>
    Public Property AccountPayableId() As PaymentsAccountPayableReportXpo
        Get
            Return fAccountPayableId
        End Get
        Set(ByVal value As PaymentsAccountPayableReportXpo)
            SetPropertyValue(Of PaymentsAccountPayableReportXpo)("AccountPayableId", fAccountPayableId, value)
        End Set
    End Property
    Dim fDescription As String
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property
    Dim fGetLocationResponsible As Byte
    Public Property GetLocationResponsible() As Byte
        Get
            Return fGetLocationResponsible
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("GetLocationResponsible", fGetLocationResponsible, value)
        End Set
    End Property
    Dim fLocationId As FixedAssetLocationReportXpo
    <Association("FixedAsset_FixedAssetEntryReferencesFixedAsset_FixedAssetLocation")>
    Public Property LocationId() As FixedAssetLocationReportXpo
        Get
            Return fLocationId
        End Get
        Set(ByVal value As FixedAssetLocationReportXpo)
            SetPropertyValue(Of FixedAssetLocationReportXpo)("LocationId", fLocationId, value)
        End Set
    End Property
    Dim fResponsibleId As FixedAssetResponsibleReportXpo
    <Association("FixedAssetFixedAssetEntryReportXpoReferencesFixedAssetResponsibleReportXpo")>
    Public Property ResponsibleId() As FixedAssetResponsibleReportXpo
        Get
            Return fResponsibleId
        End Get
        Set(ByVal value As FixedAssetResponsibleReportXpo)
            SetPropertyValue(Of FixedAssetResponsibleReportXpo)("ResponsibleId", fResponsibleId, value)
        End Set
    End Property
    <PersistentAlias("CommonCurrency.Id")>
    Public ReadOnly Property CurrencyId() As Integer
        Get
            Return Convert.ToInt32(EvaluateAlias("CurrencyId"))
        End Get
    End Property

    Dim fCommonCurrency As CommonCurrencyXpo
    <Persistent("CurrencyId")>
    <Association("FixedAssetFixedAssetEntryReportXpo")>
    Public Property CommonCurrency() As CommonCurrencyXpo
        Get
            Return fCommonCurrency
        End Get
        Set(ByVal value As CommonCurrencyXpo)
            SetPropertyValue("CommonCurrency", fCommonCurrency, value)
        End Set
    End Property

    <PersistentAlias("CommonCurrency.Abbreviation")>
    Public ReadOnly Property CurrencyAbbreviation As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CurrencyAbbreviation"))
        End Get
    End Property

    Dim fRoundService As Integer
    Public Property RoundService() As Integer
        Get
            Return fRoundService
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RoundService", fRoundService, value)
        End Set
    End Property
    Dim fInvoiceNumber As String
    Public Property InvoiceNumber() As String
        Get
            Return fInvoiceNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InvoiceNumber", fInvoiceNumber, value)
        End Set
    End Property
    Dim fInvoiceDate As DateTime
    Public Property InvoiceDate() As DateTime
        Get
            Return fInvoiceDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("InvoiceDate", fInvoiceDate, value)
        End Set
    End Property
    Dim fDayPeriod As Integer
    Public Property DayPeriod() As Integer
        Get
            Return fDayPeriod
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("DayPeriod", fDayPeriod, value)
        End Set
    End Property
    Dim fIcaPercentage As Decimal
    Public Property IcaPercentage() As Decimal
        Get
            Return fIcaPercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("IcaPercentage", fIcaPercentage, value)
        End Set
    End Property
    Dim fFreightValue As Decimal
    Public Property FreightValue() As Decimal
        Get
            Return fFreightValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("FreightValue", fFreightValue, value)
        End Set
    End Property
    Dim fFreightIVAPercentage As Decimal
    Public Property FreightIVAPercentage() As Decimal
        Get
            Return fFreightIVAPercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("FreightIVAPercentage", fFreightIVAPercentage, value)
        End Set
    End Property
    Dim fFreightIVAValue As Decimal
    Public Property FreightIVAValue() As Decimal
        Get
            Return fFreightIVAValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("FreightIVAValue", fFreightIVAValue, value)
        End Set
    End Property
    Dim fValue As Decimal
    Public Property Value() As Decimal
        Get
            Return fValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Value", fValue, value)
        End Set
    End Property
    Dim fValueDiscount As Decimal
    Public Property ValueDiscount() As Decimal
        Get
            Return fValueDiscount
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueDiscount", fValueDiscount, value)
        End Set
    End Property
    Dim fValueTax As Decimal
    Public Property ValueTax() As Decimal
        Get
            Return fValueTax
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueTax", fValueTax, value)
        End Set
    End Property
    Dim fWithholdingTax As Decimal
    Public Property WithholdingTax() As Decimal
        Get
            Return fWithholdingTax
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("WithholdingTax", fWithholdingTax, value)
        End Set
    End Property
    Dim fWithholdingICA As Decimal
    Public Property WithholdingICA() As Decimal
        Get
            Return fWithholdingICA
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("WithholdingICA", fWithholdingICA, value)
        End Set
    End Property
    Dim fRetentionSource As Decimal
    Public Property RetentionSource() As Decimal
        Get
            Return fRetentionSource
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("RetentionSource", fRetentionSource, value)
        End Set
    End Property
    Dim fRetentionOther As Decimal
    Public Property RetentionOther() As Decimal
        Get
            Return fRetentionOther
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("RetentionOther", fRetentionOther, value)
        End Set
    End Property
    Dim fDeductionOther As Decimal
    Public Property DeductionOther() As Decimal
        Get
            Return fDeductionOther
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DeductionOther", fDeductionOther, value)
        End Set
    End Property
    Dim fTotalValue As Decimal
    Public Property TotalValue() As Decimal
        Get
            Return fTotalValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalValue", fTotalValue, value)
        End Set
    End Property
    Dim fStatus As Byte
    Public Property Status() As Byte
        Get
            Return fStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Status", fStatus, value)
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
    Dim fConfirmationUser As String
    <Size(20)>
    Public Property ConfirmationUser() As String
        Get
            Return fConfirmationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ConfirmationUser", fConfirmationUser, value)
        End Set
    End Property
    Dim fConfirmationDate As DateTime
    Public Property ConfirmationDate() As DateTime
        Get
            Return fConfirmationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ConfirmationDate", fConfirmationDate, value)
        End Set
    End Property
    Dim fAnnulmentUser As String
    <Size(20)>
    Public Property AnnulmentUser() As String
        Get
            Return fAnnulmentUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AnnulmentUser", fAnnulmentUser, value)
        End Set
    End Property
    Dim fAnnulmentDate As DateTime
    Public Property AnnulmentDate() As DateTime
        Get
            Return fAnnulmentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("AnnulmentDate", fAnnulmentDate, value)
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
    <Association("FixedAsset_FixedAssetEntryItemReferencesFixedAsset_FixedAssetEntry", GetType(FixedAssetFixedAssetEntryItemReportXpo))>
    Public ReadOnly Property FixedAssetFixedAssetEntryItemReportXpo() As XPCollection(Of FixedAssetFixedAssetEntryItemReportXpo)
        Get
            Return GetCollection(Of FixedAssetFixedAssetEntryItemReportXpo)("FixedAssetFixedAssetEntryItemReportXpo")
        End Get
    End Property
    <Association("FixedAsset_FixedAssetEntryDevolutionReferencesFixedAsset_FixedAssetEntry", GetType(FixedAssetFixedAssetEntryDevolutionReportXpo))>
    Public ReadOnly Property FixedAsset_FixedAssetEntryDevolutions() As XPCollection(Of FixedAssetFixedAssetEntryDevolutionReportXpo)
        Get
            Return GetCollection(Of FixedAssetFixedAssetEntryDevolutionReportXpo)("FixedAsset_FixedAssetEntryDevolutions")
        End Get
    End Property
    <Association("VReportFixedAssetEntryDocumentSupport_References_FixedAssetEntry", GetType(FixedAssetVReportFixedAssetEntryItemDetailPartReportXpo))>
    Public ReadOnly Property FixedAssetVReportFixedAssetEntryItemDetail() As XPCollection(Of FixedAssetVReportFixedAssetEntryItemDetailPartReportXpo)
        Get
            Return GetCollection(Of FixedAssetVReportFixedAssetEntryItemDetailPartReportXpo)("FixedAssetVReportFixedAssetEntryItemDetail")
        End Get
    End Property
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
