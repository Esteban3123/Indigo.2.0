'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Juan Carlos Bermudez Gutierrez
' Created          : 15-04-2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Base
Imports Domain.Entities
Imports DevExpress.Xpo

#End Region

Public Interface ISettingBilling
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Esta Propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Obtiene o asigna el tag del funcional
    ''' </summary>
    ''' <value>Tag del fucnional</value>
    ''' <returns></returns>
    ReadOnly Property MyTag As String

    ''' <summary>
    ''' Obtiene o establece el Id de la entidad administradora por defecto para particulares
    ''' </summary>
    Property ParticularHealthAdministratorId As Integer?

    ''' <summary>
    ''' Propiedad que contiene el listado de entidades administradoras
    ''' </summary>
    Property ParticularHealthAdministratorXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el Id de tipo de comprobante contable para facturas
    ''' </summary>
    Property InvoiceJournalVoucherTypeId As Integer

    ''' <summary>
    ''' Obtiene o establece el Id de tipo de comprobante contable para facturas de productos
    ''' </summary>
    Property ProductInvoiceJournalVoucherTypeId As Integer

    ''' <summary>
    ''' Obtiene o establece el Id de tipo de comprobante contable para facturación básica
    ''' </summary>
    Property BasicBillingJournalVoucherTypeId As Integer?

    ''' <summary>
    ''' Obtiene o establece el Id de tipo de comprobante contable para distribucion de ingresos de factura monto fijo
    ''' </summary>
    Property InvoiceEntityCapitatedDistributionJournalVoucherTypeId As Integer?

    ''' <summary>
    ''' Obtiene o establece el Id de tipo de comprobante contable para la reversión distribucion de ingresos de factura monto fijo
    ''' </summary>
    Property ReverseInvoiceEntityCapitatedDistributionJournalVoucherTypeId As Integer?

    ''' <summary>
    ''' Obtiene o establece el Id de Tipo de comprobante para reversion de traslado
    ''' </summary>
    Property ReverseTransferJournalVoucherTypeId As Integer

    Property ReverseRecognitionJournalVoucherTypeId As Integer

    Property RecognitionJournalVoucherTypeId As Integer

    ''' <summary>
    ''' Obtiene o establece el Id de tipo de comprobante contable para anulacion de facturas
    ''' </summary>
    Property InvoiceAnnulmentJournalVoucherTypeId As Integer

    ''' <summary>
    ''' Popiedad que permite saber si se debe validad mayoria de edad en liquidación
    ''' </summary>
    ''' <returns></returns>
    Property ValidateAgeOfMajority As Boolean

    ''' <summary>
    ''' Obtiene o establece el Id de tipo de comprobante contable para anulacion de facturas básicas
    ''' </summary>
    Property BasicBillingAnnulmentJournalVoucherTypeId As Integer?

    ''' <summary>
    ''' Especifica si se confirma los recibos de caja
    ''' </summary>
    Property CashReceiptsConfirm As Boolean

    ''' <summary>
    ''' Obiene o Establece el Tipo de Redondeo para la Cuota de Recuperacion 
    ''' </summary>
    ''' <value>1= Peso 10 - Decena 100 - Centena 1000 - Milesima</value>
    Property RoundingTypeRecoveryFeeType As Integer

    ''' <summary>
    ''' Obtiene o Establece el Id de la Cuenta Contable para el registro del Ingreso por Capitacion
    ''' </summary>
    Property CapitationRevenueMainAccountId As Integer

    ''' <summary>
    ''' Id de autorizacion para facturas capitadas
    ''' </summary>
    Property EntityCapitatedBillingAuthorizationId As Integer

    ''' <summary>
    ''' Gets or sets the entity capitated billing authorization xpo.
    ''' </summary>
    Property EntityCapitatedBillingAuthorizationXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el listado de las Cuentas Contables para el registro del Ingreso por Capitacion
    ''' </summary>
    Property CapitationRevenueMainAccountXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o Establece el Id de la Cuenta Contable para el registro de la utilidad por Capitacion
    ''' </summary>
    Property CapitationProfitMainAccountId As Integer

    ''' <summary>
    ''' Propiedad que contiene el listado de las Cuentas Contables para el registro del utilidad por Capitacion
    ''' </summary>
    Property CapitationProfitMainAccountXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o Establece el Id de la Cuenta Contable para el registro de Perdida por Capitacion
    ''' </summary>
    Property CapitationLossMainAccountId As Integer

    ''' <summary>
    ''' Propiedad que contiene el listado de las Cuentas Contables para el registro de Perdida por Capitacion
    ''' </summary>
    Property CapitationLossMainAccountXpo As XPInstantFeedbackSource

    ''' <summary>
    '''  Obtiene o Establece el Id del concepto de caja para realizar el recibo de caja automatico en la liquidacion correspondiente al recaudo de la cuota de recuperacion
    ''' </summary>
    Property PatientAdvanceCashReceiptConceptId As Integer

    ''' <summary>
    ''' Propiedad que contiene el listado de los conceptos de caja para realizar el recibo de caja automatico en la liquidacion correspondiente al recaudo de la cuota de recuperacion
    ''' </summary>
    Property PatientAdvanceCashReceiptConceptXpo As XPInstantFeedbackSource

    ''' <summary>
    '''  Obtiene o Establece el Id del concepto de caja para realizar el recibo de caja automatico en la liquidacion correspondiente al recaudo de la cuota de recuperacion para control de capitacion
    ''' </summary>
    Property CapitedPatientAdvanceCashReceiptConceptId As Integer?

    ''' <summary>
    ''' Propiedad que contiene el listado de los conceptos de caja para realizar el recibo de caja automatico en la liquidacion correspondiente al recaudo de la cuota de recuperacion para control de capitacion
    ''' </summary>
    Property CapitedPatientAdvanceCashReceiptConceptXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o Establece el Id del concepto de caja para realizar el recibo de caja automatico en la liquidacion correspondiente al recaudo de los valores facturados al particular
    ''' </summary>
    Property IndvidualAdvanceCashReceiptConceptId As Integer

    ''' <summary>
    ''' Propiedad que contiene el listado de los conceptos de caja para realizar el recibo de caja automatico en la liquidacion correspondiente al recaudo de los valores facturados al particular
    ''' </summary>
    Property IndvidualAdvanceCashReceiptConceptXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el Consecutivo de control de capitacion, Este campo se bloquea y no puede ser modificado por el usuario cuando haya almenos un registro de control de capitacion en las tablas de factura (Invoice) Campo (DocumentType=5)
    ''' </summary>
    Property ConsecutiveControlCapitation As Long

    ''' <summary>
    ''' Especifica que se requiere un permiso de usuario para poder crear una cuenta por cobrar a un paciente (Pagare). Este evento ocurre cuando el valor que le corresponde al paciente no puede ser pagado en su totalidad entonces se deberia poder crear un pagare  
    ''' </summary>
    Property RequiresPermissionForCxCPatient As Boolean

    Property AccountControlValidation As Boolean

    Property ProductSalesCashReceiptConceptXpo As XPInstantFeedbackSource

    Property RecoveryFeeDiscountMainAccountXpo As XPInstantFeedbackSource

    Property ApplyBasicBilling As Boolean
    ''' <summary>
    ''' Permite definir la creacion o no, de un pagare cuando no existe anticipo del paciente relacionado con copagos
    ''' </summary>
    ''' <returns></returns>
    Property GeneratePromissoryNote As Boolean
    ''' <summary>
    ''' Permite saber si se abre el formulario de liquidación en control de servicios ambulatorios
    ''' </summary>
    ''' <returns></returns>
    Property LiquidateSinceControlOutPatientService As Boolean

    Property AnulateInvoicesPreviousPeriods As Boolean

    Property CalculateTaxAdvance As Byte

    Property BudgetInterface As Boolean

    Property AuthorizationNumberControl As Boolean

    Property DistributeCapitationControls As Byte

    Property ClientMainAccountId As Integer?

    Property ClientMainAccountXpo As XPInstantFeedbackSource

    Property LiquidatedPackageJournalVoucherTypeId As Integer?

    Property ReversionLiquidatedPackageJournalVoucherTypeId As Integer?

    Property AccountingPackageMainAccountId As Integer?

    Property AccountingPackageMainAccountXpo As XPInstantFeedbackSource

    Property ProjectedVariationPriceMainAccountId As Integer?

    Property ProjectedVariationPriceMainAccountXpo As XPInstantFeedbackSource

    Property IVAPaymentMainAccountId As Integer?

    Property IVAPaymentMainAccountXpo As XPInstantFeedbackSource

    Property ReteIVAConceptId As Integer?

    Property ReteIVAConceptXpo As XPInstantFeedbackSource

    Property ReteIVAMainAccountId As Integer?

    Property ReteIVAMainAccountXpo As XPInstantFeedbackSource

    Property ReteICAMainAccountId As Integer?

    Property ReteICAMainAccountXpo As XPInstantFeedbackSource

    Property ReteFuenteMainAccountId As Integer?

    Property ReteFuenteMainAccountXpo As XPInstantFeedbackSource

    Property InvoiceProductDevolutionPartialConceptNoteId As Integer?

    ''' <summary>
    ''' Especifica de donde se va tomar el centro de costo para cuando se hacen los comprobantes contables de la factura de venta de producto
    '''1 - Costo (Unidad Funcional)
    '''2 - Costo (Grupo)
    ''' </summary>
    ''' <value></value>
    Property AssociateCostCenter As Integer?

    ''' <summary>
    ''' Id de la unidad funcional
    ''' </summary>
    ''' <returns></returns>
    Property FunctionalUnitId As Integer?

    ''' <summary>
    ''' Datasource unidades funcionales
    ''' </summary>
    ''' <returns></returns>
    Property FunctionalUnitXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Código del médico
    ''' </summary>
    ''' <returns></returns>
    Property HealthProfessionalCode As String

    ''' <summary>
    ''' Datasource médico
    ''' </summary>
    ''' <returns></returns>
    Property HealthProfessionalXpo As XPInstantFeedbackSource

    Property BasicBillingCashReceiptConceptId As Integer?

    Property BasicBillingCashReceiptConceptXpo As XPInstantFeedbackSource

    Property CostCenterXPO As XPInstantFeedbackSource

    Property RecoveryFeeCostCenterDiscountXPO As XPInstantFeedbackSource
    ''' <summary>
    ''' Establece el datasource Compr reconocimiento venta en consignación
    ''' </summary>
    ''' <returns></returns>
    Property ConsignmentSalereCognitionXpo As XPInstantFeedbackSource
    ''' <summary>
    ''' Establece el datasource Compr reversión reconocimiento venta en consignación
    ''' </summary>
    ''' <returns></returns>
    Property ReversalRecognitionConsignmentSaleXpo As XPInstantFeedbackSource
    ''' <summary>
    ''' Obtiene o establece el Compr reconocimiento venta en consignación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ConsignmentSalereCognition As Integer?
    ''' <summary>
    ''' Obtiene o establece el Compr reversión reconocimiento venta en consignación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ReversalRecognitionConsignmentSale As Integer?

    ''' <summary>
    ''' Obtiene o establece si liquida cuenta madre
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property LiquidateMasterAccount As Boolean

    ''' <summary>
    ''' Obtiene o establece si maneja TRM especifico
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property HasCustomTRM As Boolean

    ''' <summary>
    ''' Obtiene o establece si liquida el folio en una moneda en especifico
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property LiquidateFolioInSpecificCurrency As Boolean

    ''' <summary>
    ''' Obtiene o establece la moneda especifica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SpecificCurrencyId As Integer?

    ''' <summary>
    ''' Obtiene o establece si requiere condiciones de venta
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RequiresConditionsSale As Boolean

    ''' <summary>
    ''' Obtiene o establece si maneja diferentes tarifas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property HandlesDifferentRates As Boolean?

    ''' <summary>
    ''' Obtiene o establece si permite ejecutivo de ventas y proovedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AllowsSalesExecutiveAndSupplier As Boolean?

    ''' <summary>
    ''' Contabiliza descuento comercial No Condicionado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AccountsConditionalCommercialDiscount As Boolean?


    ''' <summary>
    ''' Establece el datasource tipos de movimiento 
    ''' </summary>
    Property GiftProductOutletConceptXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Establece el datasource Moneda especificazz
    ''' </summary>
    Property SpecificCurrencytXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' obtiene o establece tipos de movimiento 
    ''' </summary>
    Property GiftProductOutletConcept As Integer?

    ''' <summary>
    ''' limite maximo de items por factura
    ''' </summary>
    ''' <returns></returns>
    Property MaxInvoiceItems As Integer

    ''' <summary>
    ''' Obtiene las cuentas contables que se ANULAN de venta Operacional de vigencia anterior.
    ''' </summary>
    ''' <returns></returns>
    Property ReversalPreviousYearsMainAccountIdXpo As XPInstantFeedbackSource
    Property ReversalPreviousYearsMainAccountId As Integer?
    ''' <summary>
    ''' Obtiene las cuentas contables que se ANULAN de venta No Operacional de vigencia anterior.
    ''' </summary>
    ''' <returns></returns>
    Property ReversalPreviousYearsGenericBillingMainAccountIdXpo As XPInstantFeedbackSource
    Property ReversalPreviousYearsGenericBillingMainAccountId As Integer?
#Region "Budget Interface"

    ''' <summary>
    ''' Id de la entidad de presupuesto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetaryEntityId As Integer?

    ''' <summary>
    ''' Datasource de entidades de presupuesto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetaryEntityXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Id de las vigencias de presupuesto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetaryValidityId As Integer?

    ''' <summary>
    ''' Datasource de las vigencias de presupuesto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetaryValidityXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Id de la dependencia presupuestal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DependencyId As Integer?

    ''' <summary>
    ''' Datasource de las dependencias presupuestales
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DependencyXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Id de la dependencia presupuestal Factura Básica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BasicBillingDependencyId As Integer?

    ''' <summary>
    ''' Datasource de las dependencias presupuestales de Factura Básica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BasicBillingDependencyXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Id de la dependencia presupuestal Factura Básica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BasicBillingBudgetId As Integer?

    ''' <summary>
    ''' Datasource de las dependencias presupuestales de Factura Básica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BasicBillingBudgetXpo As XPInstantFeedbackSource
    Property ValidatePackaging As Boolean

    ''' <summary>
    ''' Validación (si - no) para contabilizar paquete
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AccountingPackage As Boolean

    ''' <summary>
    ''' Validación (si - no) para integración con Mipres
    ''' </summary>
    ''' <returns></returns>
    Property ValidateIntegrationMipres As Boolean

    ''' <summary>
    ''' Validación (si - no) para tiquete electronico de venta
    ''' </summary>
    ''' <returns></returns>
    Property ApplyElectronicSalesTicket As Boolean

    ''' <summary>
    ''' Setea el valor para la autorizacion de facturacion de los tiquets electronicos de venta
    ''' </summary>
    ''' <returns></returns>
    Property BillingAuthorizationId As Integer?

    ''' <summary>
    ''' Setea el valor para el tipo de comprobante contable para las reversiones de tiquets electronicos de venta
    ''' </summary>
    ''' <returns></returns>
    Property AccountingVoucherReversalId As Integer?

    ''' <summary>
    ''' Setea el valor para el tipo de comprobante contable de los tiquetes electronicos de venta
    ''' </summary>
    ''' <returns></returns>
    Property AccountingVoucherGenerationId As Integer?

    ''' <summary>
    ''' Nombre del parámetro Client_Id
    ''' </summary>
    ''' <returns></returns>
    Property ClientNameId As String

    ''' <summary>
    ''' Nombre del parámetro Client_Secret
    ''' </summary>
    ''' <returns></returns>
    Property ClientSecret As String
#End Region

#End Region

End Interface
