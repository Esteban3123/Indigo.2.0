Imports System.Runtime.Serialization

Public Class SettingInventory

    <DataMember()>
    Public Property IvaFreigthPercentage As Decimal

    <DataMember()>
    Public Property WithholdingIvaPercentage As Decimal

    <DataMember()>
    Public Property WithholdingIvaBase As Decimal

    <DataMember()>
    Public Property AccountIVAId As Integer?

    <DataMember()>
    Public Property RetentionConceptsIVAId As Integer?

    <DataMember()>
    Public Property AccountIVARetentionId As Integer?

    <DataMember()>
    Public Property RetentionConceptsIVARetentionId As Integer?

    <DataMember()>
    Public Property FreigthAcountId As Integer

    <DataMember()>
    Public Property IvaFreigthAccountId As Integer?

    <DataMember()>
    Public Property IvaFreigthRetentionConceptsId As Integer?

    <DataMember()>
    Public Property IVAHandlessCostCenter As Boolean?

    <DataMember()>
    Public Property AccountIVACodeName As String

    <DataMember()>
    Public Property HandlesCostCenterIVARetention As Boolean

    <DataMember()>
    Public Property FreightHandlesCostCenter As Boolean

    <DataMember()>
    Public Property FreightAccountCodeName As String

    <DataMember()>
    Public Property IvaFreightHandlesCostCenter As Boolean

    <DataMember()>
    Public Property IvaFreightAccountCodeName As String

    <DataMember()>
    Public Property AdjustmentInAccountId As Integer

    <DataMember()>
    Public Property AdjustmentInHandlessCostCenter As Boolean

    <DataMember()>
    Public Property AdjustmentInCostCenterId As Integer?

    <DataMember()>
    Public Property AdjustmentOutAccountId As Integer

    <DataMember()>
    Public Property AdjustmentOutHandlessCostCenter As Boolean

    <DataMember()>
    Public Property AdjustmentOutCostCenterId As Integer?

    <DataMember()>
    Public Property NitNameTransferOrderThirdParty As String

    <DataMember()>
    Public Property NitNamePharmaceuticalDispensingThirdParty As String

    ''' <summary>
    ''' Bandera que guarda el valor parametrizable de la compañia para sabe si es iva incluido o no
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property FlagTaxInclude As Boolean

    ''' <summary>
    ''' Propiedad para establecer el código y nombre del almacén
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property MainWarehouseCodeName As String

#Region "Propiedades utilizadas por el formulario"

    <DataMember()>
    Public Property PurchaseJournalVoucherTypeDescription As String

    <DataMember()>
    Public Property SalesJournalVoucherTypeDescription As String

    <DataMember()>
    Public Property RemissionEntranceJournalVoucherTypeDescription As String

    <DataMember()>
    Public Property RemissionOutputJournalVoucherTypeDescription As String

    <DataMember()>
    Public Property RemissionEntranceDevolutionJournalVoucherTypeDescription As String

    <DataMember()>
    Public Property RemissionOutputDevolutionJournalVoucherTypeDescription As String

    <DataMember()>
    Public Property ReclassificationRemissionJournalVoucherTypeDescription As String

    <DataMember()>
    Public Property LoanJournalVoucherTypeDescription As String

    <DataMember()>
    Public Property LoanReturnJournalVoucherTypeDescription As String

    <DataMember()>
    Public Property SalesReturnJournalVoucherTypeDescription As String

    <DataMember()>
    Public Property PurchaseReturnJournalVoucherTypeDescription As String

    <DataMember()>
    Public Property ConsignmentMerchandiseJournalVoucherTypeDescription As String

    <DataMember()>
    Public Property ConsignmentMerchandiseDevolutionJournalVoucherTypeDescription As String

    <DataMember()>
    Public Property ConsignmentInventoryUseJournalVoucherTypDescription As String

    <DataMember()>
    Public Property ConsignmentInventoryUseDevolutionJournalVoucherTypDescription As String

    <DataMember()>
    Public Property OrderDispatchJournalVoucherTypeDescription As String

    <DataMember()>
    Public Property OrderDispatchReturnJournalVoucherTypeDescription As String

    <DataMember()>
    Public Property TransferBetweenWarehousesConsignmentDescription As String

    <DataMember()>
    Public Property InventoryAdjustmentJournalVoucherTypeDescription As String

    <DataMember()>
    Public Property InventoryCloseAdjustmentJournalVoucherTypeDescription As String

    <DataMember()>
    Public Property IVAFreightAccountPayableConceptDescription As String

    <DataMember()>
    Public Property FreightAccountPayableConceptDescription As String

    <DataMember()>
    Public Property ProDevelopmentAccountPayableConceptDescription As String

    <DataMember()>
    Public Property ProElectrificationAccountPayableConceptDescription As String

    <DataMember()>
    Public Property ProCultureAccountPayableConceptDescription As String

    <DataMember()>
    Public Property ProHospitalAccountPayableConceptDescription As String

    <DataMember()>
    Public Property ProGameAccountPayableConceptDescription As String

    <DataMember()>
    Public Property IVARetentionAccountPayableConceptDescription As String

    <DataMember()>
    Public Property FilingUnitDescription As String

    <DataMember()>
    Public Property IVAAccountPayableConceptDescription As String

    <DataMember()>
    Public Property IVAGeneratedMainAccountDescription As String

    <DataMember()>
    Public Property AdjustmentAccountPayableConceptDescription As String

    <DataMember()>
    Public Property DiscountSalesMainAccountDescription As String

    <DataMember()>
    Public Property RefundAccountPayableConceptNoteDescription As String

    <DataMember()>
    Public Property InputAdjustmentConceptIdDescription As String

    <DataMember()>
    Public Property OutputAdjustmentConceptIdDescription As String

    <DataMember()>
    Public Property PartialReturnSalesJournalVoucherTypeDescription As String


    <DataMember()>
    Public Property ValuationByPriceConsignmentDescription As String
#End Region

End Class
