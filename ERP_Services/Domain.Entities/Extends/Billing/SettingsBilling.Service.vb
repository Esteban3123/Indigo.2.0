#Region "Imports"

Imports System.Runtime.Serialization

#End Region

Public Class SettingsBilling

#Region "Propiedades utilizadas por el formulario"

    <DataMember()>
    Public Property ParticularHealthAdministratorDescription As String

    <DataMember()>
    Public Property InvoiceJournalVoucherTypeDescription As String

    <DataMember()>
    Public Property ProductInvoiceJournalVoucherTypeDescription As String

    <DataMember()>
    Public Property BasicBillingJournalVoucherTypeDescription As String

    <DataMember()>
    Public Property BasicBillingAnnulmentJournalVoucherTypeDescription As String

    <DataMember()>
    Public Property InvoiceEntityCapitatedDistributionJournalVoucherTypeDescription As String

    <DataMember()>
    Public Property ReverseInvoiceEntityCapitatedDistributionJournalVoucherTypeDescription As String

    <DataMember()>
    Public Property ReverseRecognitionJournalVoucherTypeDescription As String

    <DataMember()>
    Public Property ReverseTransferJournalVoucherTypeDescription As String

    <DataMember()>
    Public Property RecognitionJournalVoucherTypeDescription As String

    <DataMember()>
    Public Property InvoiceAnnulmentJournalVoucherTypeDescription As String

    <DataMember()>
    Public Property CapitationRevenueMainAccountDescription As String

    <DataMember()>
    Public Property LiquidatedPackageJournalVoucherTypeDescription As String

    <DataMember()>
    Public Property ReversionLiquidatedPackageJournalVoucherTypeDescription As String

    <DataMember()>
    Public Property AccountingPackageMainAccountDescription As String

    <DataMember()>
    Public Property ProjectedVariationPriceMainAccountDescription As String

    <DataMember()>
    Public Property RecoveryFeeDiscountMainAccountDescription As String

    <DataMember()>
    Public Property RecoveryFeeDiscountCostCenterDescription As String

    <DataMember()>
    Public Property CapitationProfitMainAccountDescription As String

    <DataMember()>
    Public Property CapitationLossMainAccountDescription As String

    <DataMember()>
    Public Property PatientAdvanceCashReceiptConceptDescription As String

    <DataMember()>
    Public Property CapitedPatientAdvanceCashReceiptConceptDescription As String

    <DataMember()>
    Public Property IndvidualAdvanceCashReceiptConceptDescription As String

    <DataMember()>
    Public Property EntityCapitatedBillingAuthorizationDescription As String

    <DataMember>
    Public Property CodeNameProductSalesCashReceiptConcept As String

    <DataMember()>
    Public Property BasicBillingCashReceiptConceptCodeName As String

    <DataMember>
    Property NumberNameProductSalesMainAccount As String

    <DataMember>
    Property CodeNameCostCenter As String

    <DataMember>
    Property ClientMainAccountDescription As String

    <DataMember>
    Property IVAPaymentMainAccountDescription As String

    <DataMember>
    Property ReteIVAConceptDescription As String

    <DataMember>
    Property ReteIVAMainAccountDescription As String

    <DataMember>
    Property ReteICAMainAccountDescription As String

    <DataMember>
    Property ReteFuenteMainAccountDescription As String

    <DataMember>
    Property FunctionalUnitDescription As String

    ''' <summary>
    ''' Porcentaje de Retención de IVA
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property RetentionPercentageIVA As Decimal

    ''' <summary>
    ''' Base de Retención de IVA
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property RetentionBaseIVA As Decimal

    ''' <summary>
    ''' Descripción del concepto de nota para la devolución parcial de factura de productos
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property InvoiceProductDevolutionPartialConceptNoteDescription As String

    ''' <summary>
    ''' Descripcion del concepto de estado de folio nuevo
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property StatusFolioNewDescription As String

    ''' <summary>
    ''' Descripcion del concepto de estado de folio Cerrado
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property StatusFolioClosedDescription As String
    ''' <summary>
    ''' Descripcion del concepto de Compr reconocimiento venta en consignación
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Public Property ConsignmentSalereCognitionName As String

    ''' <summary>
    ''' Descripcion del tipo de comprobante contable para los tickets electronicos de venta
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Public Property AccountingVoucherGenerationName As String

    ''' <summary>
    ''' Descripcion del tipo de comprobante contable para la reversion de los tickets electronicos de venta
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Public Property AccountingVoucherReversalName As String

    ''' <summary>
    ''' Descripcion de la autorizacion de facturacion para los tickets electronicos de venta
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Public Property BillingAuthorizationName As String

    ''' <summary>
    ''' Descripcion del concepto de Compr reversión reconocimiento venta en consignación
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Public Property ReversalRecognitionConsignmentSaleName As String
    ''' <summary>
    ''' Descripcion del Concepto salida producto obsequio
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Public Property GiftProductOutletConceptDescription As String
    ''' <summary>
    ''' Descripcion de las cuentas de movimientos en el campo de la Cuenta Contable Anulación Factura Operacional Vigencia Anterior
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Public Property ReversalPreviousYearsMainAccountIdDescription As String
    ''' <summary>
    ''' Descripcionde las cuentas de movimientos en el campo de la Cuenta Contable Anulación Factura NO Operacional Vigencia Anterior
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Public Property ReversalPreviousYearsGenericBillingMainAccountIdDescription As String

    <DataMember>
    Public Property BillingAuthorizationCopayCodeName As String

    ''' <summary>
    ''' Codigo y nombre de la moneda seleccionada
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Public Property CodeNameSpecificCurrency As String

#Region "Budget Interface"

    <DataMember()>
    Public Property BudgetaryEntityId As Integer?

    <DataMember()>
    Public Property BudgetaryEntityDescription As String

    <DataMember()>
    Public Property BudgetaryValidityId As Integer?

    <DataMember()>
    Public Property BudgetaryValidityDescription As String

    <DataMember()>
    Public Property DependencyDescription As String

    <DataMember()>
    Public Property BasicBillingDependencyDescription As String

    <DataMember()>
    Public Property BasicBillingBudgetDescription As String

#End Region

#End Region

End Class
