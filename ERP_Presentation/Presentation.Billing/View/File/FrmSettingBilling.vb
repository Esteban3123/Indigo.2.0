'***********************************************************************
' Assembly         : Presentacion.Billing
' Author           : Juan Carlos Bermudez Gutierrez
' Created          : 16-04-2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Billing.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Presentation.Controls
Imports System.ComponentModel
Imports DevExpress.XtraEditors.Controls
Imports Presentation.Treasury
Imports Presentation.Accounting
Imports Presentation.Common
Imports DevExpress.Xpo
Imports Presentation.Common.MVP
Imports Infrastructure.Data.Xpo

#End Region

Public Class FrmSettingBilling
    Implements ISettingBilling

#Region "Properties"

    Public Property InvoiceProductDevolutionPartialConceptNoteId As Integer? Implements ISettingBilling.InvoiceProductDevolutionPartialConceptNoteId
        Get
            Return INDsleInvoiceProductDevolutionPartialConceptNote.EditValue
        End Get
        Set(value As Integer?)
            INDsleInvoiceProductDevolutionPartialConceptNote.EditValue = value
        End Set
    End Property

    Public Property ParticularHealthAdministratorId As Integer? Implements ISettingBilling.ParticularHealthAdministratorId
        Get
            Return INDSleParticularHealthAdministratorId.EditValue
        End Get
        Set(value As Integer?)
            INDSleParticularHealthAdministratorId.EditValue = value
        End Set
    End Property

    Public Property ParticularHealthAdministratorXpo As XPInstantFeedbackSource Implements ISettingBilling.ParticularHealthAdministratorXpo
        Get
            Return INDSleParticularHealthAdministratorId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleParticularHealthAdministratorId.Properties.DataSource = value
        End Set
    End Property

    Public Property FunctionalUnitId As Integer? Implements ISettingBilling.FunctionalUnitId
        Get
            Return INDsleFunctionalUnit.EditValue
        End Get
        Set(value As Integer?)
            INDsleFunctionalUnit.EditValue = value
        End Set
    End Property

    Public Property FunctionalUnitXpo As XPInstantFeedbackSource Implements ISettingBilling.FunctionalUnitXpo
        Get
            Return INDsleFunctionalUnit.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleFunctionalUnit.Properties.DataSource = value
        End Set
    End Property

    Public Property HealthProfessionalCode As String Implements ISettingBilling.HealthProfessionalCode
        Get
            Return INDsleHealthProfessional.EditValue
        End Get
        Set(value As String)
            INDsleHealthProfessional.EditValue = value
        End Set
    End Property

    Public Property HealthProfessionalXpo As XPInstantFeedbackSource Implements ISettingBilling.HealthProfessionalXpo
        Get
            Return INDsleHealthProfessional.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleHealthProfessional.Properties.DataSource = value
        End Set
    End Property

    Public Property ApplyBasicBilling As Boolean Implements ISettingBilling.ApplyBasicBilling
        Get
            Return INDsleApplyBasicBilling.EditValue
        End Get
        Set(value As Boolean)
            INDsleApplyBasicBilling.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Permite definir la creacion o no, de un pagare cuando no existe anticipo del paciente relacionado con copagos
    ''' </summary>
    ''' <returns></returns>
    Public Property GeneratePromissoryNote As Boolean Implements ISettingBilling.GeneratePromissoryNote
        Get
            Return INDGleGeneratePromissoryNote.EditValue
        End Get
        Set(value As Boolean)
            INDGleGeneratePromissoryNote.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad de lectura y escritura para obtener si valida mayoria de edad en liquidación.
    ''' </summary>
    ''' <returns></returns>
    Public Property ValidateAgeOfMajority As Boolean Implements ISettingBilling.ValidateAgeOfMajority
        Get
            Return INDGleValidateAgeOfMajority.EditValue
        End Get
        Set(value As Boolean)
            INDGleValidateAgeOfMajority.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Permite saber si se abre el formulario de liquidación en control de servicios ambulatorios
    ''' </summary>
    ''' <returns></returns>
    Public Property LiquidateSinceControlOutPatientService As Boolean Implements ISettingBilling.LiquidateSinceControlOutPatientService
        Get
            Return INDsleLiquidateSinceControlOutPatientService.EditValue
        End Get
        Set(value As Boolean)
            INDsleLiquidateSinceControlOutPatientService.EditValue = value
        End Set
    End Property

    Public Property AnulateInvoicesPreviousPeriods As Boolean Implements ISettingBilling.AnulateInvoicesPreviousPeriods
        Get
            Return INDsleAnulateInvoicesPreviousPeriods.EditValue
        End Get
        Set(value As Boolean)
            INDsleAnulateInvoicesPreviousPeriods.EditValue = value
        End Set
    End Property

    Public Property BudgetInterface As Boolean Implements ISettingBilling.BudgetInterface
        Get
            Return INDsleBudgetInterface.EditValue
        End Get
        Set(value As Boolean)
            INDsleBudgetInterface.EditValue = value
        End Set
    End Property

    Public Property AuthorizationNumberControl As Boolean Implements ISettingBilling.AuthorizationNumberControl
        Get
            Return INDGleAuthorizationNumberControl.EditValue
        End Get
        Set(value As Boolean)
            INDGleAuthorizationNumberControl.EditValue = value
        End Set
    End Property

    Public Property ValidatePackaging As Boolean Implements ISettingBilling.ValidatePackaging
        Get
            Return INDGleValidatePacking.EditValue
        End Get
        Set(value As Boolean)
            INDGleValidatePacking.EditValue = value
        End Set
    End Property

    Public Property AccountingPackage As Boolean Implements ISettingBilling.AccountingPackage
        Get
            Return INDGleAccountingPackage.EditValue
        End Get
        Set(value As Boolean)
            INDGleAccountingPackage.EditValue = value
        End Set
    End Property

    Public Property DistributeCapitationControls As Byte Implements ISettingBilling.DistributeCapitationControls
        Get
            Return INDsleDistributeCapitationControls.EditValue
        End Get
        Set(value As Byte)
            INDsleDistributeCapitationControls.EditValue = value
        End Set
    End Property

    Public Property ClientMainAccountId As Integer? Implements ISettingBilling.ClientMainAccountId
        Get
            Return INDsleClientMainAccount.EditValue
        End Get
        Set(value As Integer?)
            INDsleClientMainAccount.EditValue = value
        End Set
    End Property

    Public Property ClientMainAccountXpo As XPInstantFeedbackSource Implements ISettingBilling.ClientMainAccountXpo
        Get
            Return INDsleClientMainAccount.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleClientMainAccount.Properties.DataSource = value
        End Set
    End Property

    Public Property LiquidatedPackageJournalVoucherTypeId As Integer? Implements ISettingBilling.LiquidatedPackageJournalVoucherTypeId
        Get
            Return INDSleLiquidatedPackageJournalVoucherTypeId.EditValue
        End Get
        Set(value As Integer?)
            INDSleLiquidatedPackageJournalVoucherTypeId.EditValue = value
        End Set
    End Property

    Public Property ReversionLiquidatedPackageJournalVoucherTypeId As Integer? Implements ISettingBilling.ReversionLiquidatedPackageJournalVoucherTypeId
        Get
            Return INDSleReversionLiquidatedPackageJournalVoucherTypeId.EditValue
        End Get
        Set(value As Integer?)
            INDSleReversionLiquidatedPackageJournalVoucherTypeId.EditValue = value
        End Set
    End Property

    Public Property AccountingPackageMainAccountId As Integer? Implements ISettingBilling.AccountingPackageMainAccountId
        Get
            Return INDSleAccountingPackageMainAccountId.EditValue
        End Get
        Set(value As Integer?)
            INDSleAccountingPackageMainAccountId.EditValue = value
        End Set
    End Property

    Public Property AccountingPackageMainAccountXpo As XPInstantFeedbackSource Implements ISettingBilling.AccountingPackageMainAccountXpo
        Get
            Return INDSleAccountingPackageMainAccountId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleAccountingPackageMainAccountId.Properties.DataSource = value
        End Set
    End Property

    Public Property ProjectedVariationPriceMainAccountId As Integer? Implements ISettingBilling.ProjectedVariationPriceMainAccountId
        Get
            Return INDSleProjectedVariationPriceMainAccountId.EditValue
        End Get
        Set(value As Integer?)
            INDSleProjectedVariationPriceMainAccountId.EditValue = value
        End Set
    End Property

    Public Property ProjectedVariationPriceMainAccountXpo As XPInstantFeedbackSource Implements ISettingBilling.ProjectedVariationPriceMainAccountXpo
        Get
            Return INDSleProjectedVariationPriceMainAccountId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleProjectedVariationPriceMainAccountId.Properties.DataSource = value
        End Set
    End Property
    '-------------------------------------------------------------

    Public Property IVAPaymentMainAccountId As Integer? Implements ISettingBilling.IVAPaymentMainAccountId
        Get
            Return INDsleIVAPaymentMainAccount.EditValue
        End Get
        Set(value As Integer?)
            INDsleIVAPaymentMainAccount.EditValue = value
        End Set
    End Property

    Public Property IVAPaymentMainAccountXpo As XPInstantFeedbackSource Implements ISettingBilling.IVAPaymentMainAccountXpo
        Get
            Return INDsleIVAPaymentMainAccount.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleIVAPaymentMainAccount.Properties.DataSource = value
        End Set
    End Property

    Public Property ReteIVAConceptId As Integer? Implements ISettingBilling.ReteIVAConceptId
        Get
            Return INDsleReteIVAConcept.EditValue
        End Get
        Set(value As Integer?)
            INDsleReteIVAConcept.EditValue = value
        End Set
    End Property

    Public Property ReteIVAConceptXpo As XPInstantFeedbackSource Implements ISettingBilling.ReteIVAConceptXpo
        Get
            Return INDsleReteIVAConcept.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleReteIVAConcept.Properties.DataSource = value
        End Set
    End Property

    Public Property ReteIVAMainAccountId As Integer? Implements ISettingBilling.ReteIVAMainAccountId
        Get
            Return INDsleReteIVAMainAccount.EditValue
        End Get
        Set(value As Integer?)
            INDsleReteIVAMainAccount.EditValue = value
        End Set
    End Property

    Public Property ReteIVAMainAccountXpo As XPInstantFeedbackSource Implements ISettingBilling.ReteIVAMainAccountXpo
        Get
            Return INDsleReteIVAMainAccount.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleReteIVAMainAccount.Properties.DataSource = value
        End Set
    End Property

    Public Property ReteICAMainAccountId As Integer? Implements ISettingBilling.ReteICAMainAccountId
        Get
            Return INDsleReteICAMainAccount.EditValue
        End Get
        Set(value As Integer?)
            INDsleReteICAMainAccount.EditValue = value
        End Set
    End Property

    Public Property ReteICAMainAccountXpo As XPInstantFeedbackSource Implements ISettingBilling.ReteICAMainAccountXpo
        Get
            Return INDsleReteICAMainAccount.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleReteICAMainAccount.Properties.DataSource = value
        End Set
    End Property

    Public Property ReteFuenteMainAccountId As Integer? Implements ISettingBilling.ReteFuenteMainAccountId
        Get
            Return INDsleReteFuenteMainAccount.EditValue
        End Get
        Set(value As Integer?)
            INDsleReteFuenteMainAccount.EditValue = value
        End Set
    End Property

    Public Property ReteFuenteMainAccountXpo As XPInstantFeedbackSource Implements ISettingBilling.ReteFuenteMainAccountXpo
        Get
            Return INDsleReteFuenteMainAccount.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleReteFuenteMainAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece de donde saca el centro de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AssociateCostCenter As Integer? Implements ISettingBilling.AssociateCostCenter
        Get
            Return INDSleAssociateCostCenter.EditValue
        End Get
        Set(value As Integer?)
            INDSleAssociateCostCenter.EditValue = value
        End Set
    End Property

    Private _loadingControls As Boolean

    ''' <summary>
    ''' Obtiene o establece el tag
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As String Implements ISettingBilling.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Esta Propiedad establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements ISettingBilling.ActionsOnControls
        Set(value As Boolean)

        End Set
    End Property

    ''' <summary>
    ''' Id de autorizacion para facturas capitadas
    ''' </summary>
    Public Property EntityCapitatedBillingAuthorizationId As Integer Implements ISettingBilling.EntityCapitatedBillingAuthorizationId
        Get
            Return CInt(INDsleEntityCapitatedAuthorization.EditValue)
        End Get
        Set(value As Integer)
            INDsleEntityCapitatedAuthorization.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the entity capitated billing authorization xpo.
    ''' </summary>
    Public Property EntityCapitatedBillingAuthorizationXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingBilling.EntityCapitatedBillingAuthorizationXpo
        Get
            Return INDsleEntityCapitatedAuthorization.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleEntityCapitatedAuthorization.Properties.DataSource = value
        End Set
    End Property

    Public Property AccountingForSurgical As Byte
        Get
            Return CType(INDGleAccountingQx.EditValue, Byte)
        End Get
        Set(value As Byte)
            INDGleAccountingQx.EditValue = value
        End Set
    End Property

    Public Property CalculateTaxAdvance As Byte Implements ISettingBilling.CalculateTaxAdvance
        Get
            Return CType(INDGleCalculateTaxAdvance.EditValue, Byte)
        End Get
        Set(value As Byte)
            INDGleCalculateTaxAdvance.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o Establece el Id de la Cuenta Contable para el registro de Perdida por Capitacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CapitationLossMainAccountId As Integer Implements ISettingBilling.CapitationLossMainAccountId
        Get
            Return INDsleCapitationLossMainAccountId.EditValue
        End Get
        Set(value As Integer)
            INDsleCapitationLossMainAccountId.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que contiene el listado de las Cuentas Contables para el registro de Perdida por Capitacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CapitationLossMainAccountXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingBilling.CapitationLossMainAccountXpo
        Get
            Return INDsleCapitationLossMainAccountId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleCapitationLossMainAccountId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o Establece el Id de la Cuenta Contable para el registro de la utilidad por Capitacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CapitationProfitMainAccountId As Integer Implements ISettingBilling.CapitationProfitMainAccountId
        Get
            Return INDsleCapitationProfitMainAccountId.EditValue
        End Get
        Set(value As Integer)
            INDsleCapitationProfitMainAccountId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el listado de las Cuentas Contables para el registro del utilidad por Capitacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CapitationProfitMainAccountXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingBilling.CapitationProfitMainAccountXpo
        Get
            Return INDsleCapitationProfitMainAccountId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleCapitationProfitMainAccountId.Properties.DataSource = value
        End Set
    End Property

    Public Property RecoveryFeeDiscountMainAccountXpo As XPInstantFeedbackSource Implements ISettingBilling.RecoveryFeeDiscountMainAccountXpo
        Get
            Return INDSleRecoveryFeeDiscountMainAccount.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleRecoveryFeeDiscountMainAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o Establece el Id de la Cuenta Contable para el registro del Ingreso por Capitacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CapitationRevenueMainAccountId As Integer Implements ISettingBilling.CapitationRevenueMainAccountId
        Get
            Return INDsleCapitationRevenueMainAccountId.EditValue
        End Get
        Set(value As Integer)
            INDsleCapitationRevenueMainAccountId.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que contiene el listado de las Cuentas Contables para el registro del Ingreso por Capitacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CapitationRevenueMainAccountXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingBilling.CapitationRevenueMainAccountXpo
        Get
            Return INDsleCapitationRevenueMainAccountId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleCapitationRevenueMainAccountId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    '''  Obtiene o Establece el Id del concepto de caja para realizar el recibo de caja automatico en la liquidacion correspondiente al recaudo de los valores facturados al particular
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IndvidualAdvanceCashReceiptConceptId As Integer Implements ISettingBilling.IndvidualAdvanceCashReceiptConceptId
        Get
            Return INDSleIndvidualAdvanceCashReceiptConceptId.EditValue
        End Get
        Set(value As Integer)
            INDSleIndvidualAdvanceCashReceiptConceptId.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que contiene el listado de los conceptos de caja para realizar el recibo de caja automatico en la liquidacion correspondiente al recaudo de los valores facturados al particular
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IndvidualAdvanceCashReceiptConceptXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingBilling.IndvidualAdvanceCashReceiptConceptXpo
        Get
            Return INDSleIndvidualAdvanceCashReceiptConceptId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleIndvidualAdvanceCashReceiptConceptId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Id de tipo de comprobante contable para anulacion de facturas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InvoiceAnnulmentJournalVoucherTypeId As Integer Implements ISettingBilling.InvoiceAnnulmentJournalVoucherTypeId
        Get
            Return INDsleInvoiceAnnulmentJournalVoucherTypeId.EditValue
        End Get
        Set(value As Integer)
            INDsleInvoiceAnnulmentJournalVoucherTypeId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Id de tipo de comprobante contable para anulacion de facturas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BasicBillingAnnulmentJournalVoucherTypeId As Integer? Implements ISettingBilling.BasicBillingAnnulmentJournalVoucherTypeId
        Get
            Return INDSleBasicBillingAnnulmentJournalVoucherTypeId.EditValue
        End Get
        Set(value As Integer?)
            INDSleBasicBillingAnnulmentJournalVoucherTypeId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Id de Tipo de comprobante para reversion de traslado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ReverseTransferJournalVoucherTypeId As Integer Implements ISettingBilling.ReverseTransferJournalVoucherTypeId
        Get
            Return INDsleReverseTransferJournalVoucherTypeId.EditValue
        End Get
        Set(value As Integer)
            INDsleReverseTransferJournalVoucherTypeId.EditValue = value
        End Set
    End Property

    Public Property ReverseRecognitionJournalVoucherTypeId As Integer Implements ISettingBilling.ReverseRecognitionJournalVoucherTypeId
        Get
            Return INDsleReverseRecognitionJournalVoucherTypeId.EditValue
        End Get
        Set(value As Integer)
            INDsleReverseRecognitionJournalVoucherTypeId.EditValue = value
        End Set
    End Property

    Public Property RecognitionJournalVoucherTypeId As Integer Implements ISettingBilling.RecognitionJournalVoucherTypeId
        Get
            Return INDsleRecognitionJournalVoucherTypeId.EditValue
        End Get
        Set(value As Integer)
            INDsleRecognitionJournalVoucherTypeId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Id de tipo de comprobante contable para facturas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InvoiceJournalVoucherTypeId As Integer Implements ISettingBilling.InvoiceJournalVoucherTypeId
        Get
            Return INDsleInvoiceJournalVoucherTypeId.EditValue
        End Get
        Set(value As Integer)
            INDsleInvoiceJournalVoucherTypeId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Id de tipo de comprobante contable para facturas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ProductInvoiceJournalVoucherTypeId As Integer Implements ISettingBilling.ProductInvoiceJournalVoucherTypeId
        Get
            Return INDSleProductInvoiceJournalVourcherTypeId.EditValue
        End Get
        Set(value As Integer)
            INDSleProductInvoiceJournalVourcherTypeId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Id de tipo de comprobante contable para facturacion basica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BasicBillingJournalVoucherTypeId As Integer? Implements ISettingBilling.BasicBillingJournalVoucherTypeId
        Get
            Return INDSleBasicBillingJournalVourcherTypeId.EditValue
        End Get
        Set(value As Integer?)
            INDSleBasicBillingJournalVourcherTypeId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Id de tipo de comprobante contable para distribucion de ingresos de factura monto fijo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InvoiceEntityCapitatedDistributionJournalVoucherTypeId As Integer? Implements ISettingBilling.InvoiceEntityCapitatedDistributionJournalVoucherTypeId
        Get
            Return INDsleInvoiceEntityCapitatedDistributionJournalVoucherType.EditValue
        End Get
        Set(value As Integer?)
            INDsleInvoiceEntityCapitatedDistributionJournalVoucherType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Id de tipo de comprobante contable para la reversión distribucion de ingresos de factura monto fijo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ReverseInvoiceEntityCapitatedDistributionJournalVoucherTypeId As Integer? Implements ISettingBilling.ReverseInvoiceEntityCapitatedDistributionJournalVoucherTypeId
        Get
            Return INDsleReverseInvoiceEntityCapitatedDistributionJournalVoucherTypeId.EditValue
        End Get
        Set(value As Integer?)
            INDsleReverseInvoiceEntityCapitatedDistributionJournalVoucherTypeId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o Establece el Id del concepto de caja para realizar el recibo de caja automatico en la liquidacion correspondiente al recaudo de la cuota de recuperacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PatientAdvanceCashReceiptConceptId As Integer Implements ISettingBilling.PatientAdvanceCashReceiptConceptId
        Get
            Return INDSlePatientAdvanceCashReceiptConceptId.EditValue
        End Get
        Set(value As Integer)
            INDSlePatientAdvanceCashReceiptConceptId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el listado de los conceptos de caja para realizar el recibo de caja automatico en la liquidacion correspondiente al recaudo de la cuota de recuperacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PatientAdvanceCashReceiptConceptXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingBilling.PatientAdvanceCashReceiptConceptXpo
        Get
            Return INDSlePatientAdvanceCashReceiptConceptId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSlePatientAdvanceCashReceiptConceptId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o Establece el Id del concepto de caja para realizar el recibo de caja automatico en la liquidacion correspondiente al recaudo de la cuota de recuperacion para control de capitacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CapitedPatientAdvanceCashReceiptConceptId As Integer? Implements ISettingBilling.CapitedPatientAdvanceCashReceiptConceptId
        Get
            Return INDSleCapitedPatientAdvanceCashReceiptConceptId.EditValue
        End Get
        Set(value As Integer?)
            INDSleCapitedPatientAdvanceCashReceiptConceptId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el listado de los conceptos de caja para realizar el recibo de caja automatico en la liquidacion correspondiente al recaudo de la cuota de recuperacion para control de capitacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CapitedPatientAdvanceCashReceiptConceptXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingBilling.CapitedPatientAdvanceCashReceiptConceptXpo
        Get
            Return INDSleCapitedPatientAdvanceCashReceiptConceptId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleCapitedPatientAdvanceCashReceiptConceptId.Properties.DataSource = value
        End Set
    End Property

    Public Property RecoveryFeeDiscountMainAccountId As Integer
        Get
            Return INDSleRecoveryFeeDiscountMainAccount.EditValue
        End Get
        Set(value As Integer)
            INDSleRecoveryFeeDiscountMainAccount.EditValue = value
        End Set
    End Property

    Private Property RecoveryFeeDiscountCostCenterId As Integer?
        Get
            Return INDSleCostCenterRecoveryFeeDiscount.EditValue
        End Get
        Set(value As Integer?)
            INDSleCostCenterRecoveryFeeDiscount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Especifica que se requiere un permiso de usuario para poder crear una cuenta por cobrar a un paciente (Pagare). Este evento ocurre cuando el valor que le corresponde al paciente no puede ser pagado en su totalidad entonces se deberia poder crear un pagare  
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RequiresPermissionForCxCPatient As Boolean Implements ISettingBilling.RequiresPermissionForCxCPatient
        Get
            Return INDsleRequiresPermissionForCxCPatient.EditValue
        End Get
        Set(value As Boolean)
            INDsleRequiresPermissionForCxCPatient.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' validacion de control de cuentas
    ''' </summary>
    ''' <returns></returns>
    Public Property AccountControlValidation As Boolean Implements ISettingBilling.AccountControlValidation
        Get
            Return INDGleAccountControlValidation.EditValue
        End Get
        Set(value As Boolean)
            INDGleAccountControlValidation.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si liquida cuenta madre o no
    ''' </summary>
    ''' <returns></returns>
    Public Property LiquidateMasterAccount As Boolean Implements ISettingBilling.LiquidateMasterAccount
        Get
            Return INDsleLiquidateMasterAccount.EditValue
        End Get
        Set(value As Boolean)
            INDsleLiquidateMasterAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si maneja trm especifico
    ''' </summary>
    ''' <returns></returns>
    Public Property HasCustomTRM As Boolean Implements ISettingBilling.HasCustomTRM
        Get
            Return INDsleHasCustomTRM.EditValue
        End Get
        Set(value As Boolean)
            INDsleHasCustomTRM.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si liquida el folio en una moneda en especifico
    ''' </summary>
    ''' <returns></returns>
    Public Property LiquidateFolioInSpecificCurrency As Boolean Implements ISettingBilling.LiquidateFolioInSpecificCurrency
        Get
            Return INDsleLiquidateFolioInSpecificCurrency.EditValue
        End Get
        Set(value As Boolean)
            INDsleLiquidateFolioInSpecificCurrency.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la moneda especifica
    ''' </summary>
    ''' <returns></returns>
    Public Property SpecificCurrencyId As Integer? Implements ISettingBilling.SpecificCurrencyId
        Get
            Return INDSleSpecificCurrency.EditValue
        End Get
        Set(value As Integer?)
            INDSleSpecificCurrency.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si liquida el folio en una moneda en especifico
    ''' </summary>
    ''' <returns></returns>
    Public Property requiresConditionsSale As Boolean Implements ISettingBilling.RequiresConditionsSale
        Get
            Return INDSleRequiresConditionsSale.EditValue
        End Get
        Set(value As Boolean)
            INDSleRequiresConditionsSale.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si maneja diferentes tarifas
    ''' </summary>
    ''' <returns></returns>
    Public Property HandlesDifferentRates As Boolean? Implements ISettingBilling.HandlesDifferentRates
        Get
            Return INDsleHandlesDifferentRates.EditValue
        End Get
        Set(value As Boolean?)
            INDsleHandlesDifferentRates.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si permite ejecutivo de ventas y proovedor
    ''' </summary>
    ''' <returns></returns>
    Public Property AllowsSalesExecutiveAndSupplier As Boolean? Implements ISettingBilling.AllowsSalesExecutiveAndSupplier
        Get
            Return INDsleAllowsSalesExecutiveAndSupplier.EditValue
        End Get
        Set(value As Boolean?)
            INDsleAllowsSalesExecutiveAndSupplier.EditValue = value
        End Set
    End Property

    ''' <summary>
    '''  Contabiliza descuento comercial No Condicionado
    ''' </summary>
    ''' <returns></returns>
    Public Property AccountsConditionalCommercialDiscount As Boolean? Implements ISettingBilling.AccountsConditionalCommercialDiscount
        Get
            Return INDsleAccountsConditionalCommercialDiscount.EditValue
        End Get
        Set(value As Boolean?)
            INDsleAccountsConditionalCommercialDiscount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obiene o Establece el Tipo de Redondeo para la Cuota de Recuperacion 
    ''' </summary>
    ''' <value>1= Peso 10 - Decena 100 - Centena 1000 - Milesima</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RoundingTypeRecoveryFeeType As Integer Implements ISettingBilling.RoundingTypeRecoveryFeeType
        Get
            Return INDsleRoundingTypeRecoveryFeeType.EditValue
        End Get
        Set(value As Integer)
            INDsleRoundingTypeRecoveryFeeType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Especifica si se confirma los recibos de caja
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CashReceiptsConfirm As Boolean Implements ISettingBilling.CashReceiptsConfirm
        Get
            Return INDSleCashReceiptsConfirm.EditValue
        End Get
        Set(value As Boolean)
            INDSleCashReceiptsConfirm.EditValue = True
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Consecutivo de control de capitacion, Este campo se bloquea y no puede ser modificado por el usuario cuando haya almenos un registro de control de capitacion en las tablas de factura (Invoice) Campo (DocumentType=5)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ConsecutiveControlCapitation As Long Implements ISettingBilling.ConsecutiveControlCapitation
        Get
            Return INDtxtConsecutiveControlCapitation.EditValue
        End Get
        Set(value As Long)
            INDtxtConsecutiveControlCapitation.EditValue = value
        End Set
    End Property

    Property ProductSalesCashReceiptConceptXpo As XPInstantFeedbackSource Implements ISettingBilling.ProductSalesCashReceiptConceptXpo
        Get
            Return CType(INDSleProductSalesCashReceiptConcept.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleProductSalesCashReceiptConcept.Properties.DataSource = value
        End Set
    End Property

    Public Property BasicBillingCashReceiptConceptId As Integer? Implements ISettingBilling.BasicBillingCashReceiptConceptId
        Get
            Return INDsleBasicBillingCashReceiptConcept.EditValue
        End Get
        Set(value As Integer?)
            INDsleBasicBillingCashReceiptConcept.EditValue = value
        End Set
    End Property

    Public Property BasicBillingCashReceiptConceptXpo As XPInstantFeedbackSource Implements ISettingBilling.BasicBillingCashReceiptConceptXpo
        Get
            Return INDsleBasicBillingCashReceiptConcept.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleBasicBillingCashReceiptConcept.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece los centros de costo
    ''' </summary>
    ''' <value>
    ''' The cost center xpo.
    ''' </value>
    Property CostCenterXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingBilling.CostCenterXPO
        Get
            Return CType(INDSleCostCenter.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleCostCenter.Properties.DataSource = value
        End Set
    End Property

    Property RecoveryFeeCostCenterDiscountXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingBilling.RecoveryFeeCostCenterDiscountXPO
        Get
            Return CType(INDSleCostCenterRecoveryFeeDiscount.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleCostCenterRecoveryFeeDiscount.Properties.DataSource = value
        End Set
    End Property

    Public Property ValidateIntegrationMipres As Boolean Implements ISettingBilling.ValidateIntegrationMipres
        Get
            Return INDsleMiPres.EditValue
        End Get
        Set(value As Boolean)
            INDsleMiPres.EditValue = value
        End Set
    End Property

    Public Property ApplyElectronicSalesTicket As Boolean Implements ISettingBilling.ApplyElectronicSalesTicket
        Get
            Return INDSleElectronicSalesTicket.EditValue
        End Get
        Set(value As Boolean)
            INDSleElectronicSalesTicket.EditValue = value
        End Set
    End Property

    Public Property BillingAuthorizationId As Integer? Implements ISettingBilling.BillingAuthorizationId
        Get
            Return INDsleBillingAuthorization.EditValue
        End Get
        Set(value As Integer?)
            INDsleBillingAuthorization.EditValue = value
        End Set
    End Property

    Public Property AccountingVoucherReversalId As Integer? Implements ISettingBilling.AccountingVoucherReversalId
        Get
            Return INDSleAccountingVoucherReversal.EditValue
        End Get
        Set(value As Integer?)
            INDSleAccountingVoucherReversal.EditValue = value
        End Set
    End Property

    Public Property AccountingVoucherGenerationId As Integer? Implements ISettingBilling.AccountingVoucherGenerationId
        Get
            Return INDSleAccountingVoucherGeneration.EditValue
        End Get
        Set(value As Integer?)
            INDSleAccountingVoucherGeneration.EditValue = value
        End Set
    End Property

    Public Property ClientNameId As String Implements ISettingBilling.ClientNameId
        Get
            Return INDTxtClientId.EditValue
        End Get
        Set(value As String)
            INDTxtClientId.EditValue = value
        End Set
    End Property

    Public Property ClientSecret As String Implements ISettingBilling.ClientSecret
        Get
            Return INDTxtClientSecret.EditValue
        End Get
        Set(value As String)
            INDTxtClientSecret.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' numero maximo de items que permite una factura ( 0 sin limites)
    ''' </summary>
    ''' <returns></returns>
    Public Property MaxInvoiceItems As Integer Implements ISettingBilling.MaxInvoiceItems
        Get
            Return INDSpeMaxInvoiceItems.EditValue
        End Get
        Set(value As Integer)
            INDSpeMaxInvoiceItems.EditValue = value
        End Set
    End Property

    ''' <summary>
    '''  XPO que obtiene las facturas de cuentas contables que se ANULAN de venta Operacional de vigencia anterior.
    ''' </summary>
    ''' <returns></returns>
    Public Property ReversalPreviousYearsMainAccountIdXpo As XPInstantFeedbackSource Implements ISettingBilling.ReversalPreviousYearsMainAccountIdXpo
        Get
            Return INDSleNullifyBillingOperationalCurrent.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleNullifyBillingOperationalCurrent.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' propiedad  que obtiene las facturas de cuentas contables que se ANULAN de venta Operacional de vigencia anterior.
    ''' </summary>
    ''' <returns></returns>
    Public Property ReversalPreviousYearsMainAccountId As Integer? Implements ISettingBilling.ReversalPreviousYearsMainAccountId
        Get
            Return INDSleNullifyBillingOperationalCurrent.EditValue
        End Get
        Set(value As Integer?)
            INDSleNullifyBillingOperationalCurrent.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' XPO que obtiene las facturas de cuentas contables que se ANULAN de venta NO Operacional de vigencia anterior.
    ''' </summary>
    ''' <returns></returns>
    Public Property ReversalPreviousYearsGenericBillingMainAccountIdXpo As XPInstantFeedbackSource Implements ISettingBilling.ReversalPreviousYearsGenericBillingMainAccountIdXpo
        Get
            Return INDSleNullifyBillingNoOperationalCurrent.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleNullifyBillingNoOperationalCurrent.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' propiedad que obtiene las facturas de cuentas contables que se ANULAN de venta NO Operacional de vigencia anterior.
    ''' </summary>
    ''' <returns></returns>
    Public Property ReversalPreviousYearsGenericBillingMainAccountId As Integer? Implements ISettingBilling.ReversalPreviousYearsGenericBillingMainAccountId
        Get
            Return INDSleNullifyBillingNoOperationalCurrent.EditValue
        End Get
        Set(value As Integer?)
            INDSleNullifyBillingNoOperationalCurrent.EditValue = value
        End Set
    End Property

#Region "Budget Interface"

    Public Property BudgetaryEntityId As Integer? Implements ISettingBilling.BudgetaryEntityId
        Get
            Return INDSleBudgetaryEntityId.EditValue
        End Get
        Set(value As Integer?)
            INDSleBudgetaryEntityId.EditValue = value
        End Set
    End Property

    Public Property BudgetaryEntityXpo As XPInstantFeedbackSource Implements ISettingBilling.BudgetaryEntityXpo
        Get
            Return INDSleBudgetaryEntityId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleBudgetaryEntityId.Properties.DataSource = value
        End Set
    End Property

    Public Property BudgetaryValidityId As Integer? Implements ISettingBilling.BudgetaryValidityId
        Get
            Return INDSleBudgetaryValidityId.EditValue
        End Get
        Set(value As Integer?)
            INDSleBudgetaryValidityId.EditValue = value
        End Set
    End Property

    Public Property BudgetaryValidityXpo As XPInstantFeedbackSource Implements ISettingBilling.BudgetaryValidityXpo
        Get
            Return INDSleBudgetaryValidityId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleBudgetaryValidityId.Properties.DataSource = value
        End Set
    End Property

    Public Property DependencyId As Integer? Implements ISettingBilling.DependencyId
        Get
            Return INDSleDependencyId.EditValue
        End Get
        Set(value As Integer?)
            INDSleDependencyId.EditValue = value
        End Set
    End Property

    Public Property DependencyXpo As XPInstantFeedbackSource Implements ISettingBilling.DependencyXpo
        Get
            Return INDSleDependencyId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleDependencyId.Properties.DataSource = value
        End Set
    End Property

    Public Property BasicBillingDependencyId As Integer? Implements ISettingBilling.BasicBillingDependencyId
        Get
            Return INDSleBasicBillingDependencyId.EditValue
        End Get
        Set(value As Integer?)
            INDSleBasicBillingDependencyId.EditValue = value
        End Set
    End Property

    Public Property BasicBillingDependencyXpo As XPInstantFeedbackSource Implements ISettingBilling.BasicBillingDependencyXpo
        Get
            Return INDSleBasicBillingDependencyId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleBasicBillingDependencyId.Properties.DataSource = value
        End Set
    End Property

    Public Property BasicBillingBudgetId As Integer? Implements ISettingBilling.BasicBillingBudgetId
        Get
            Return INDSleBasicBillingBudgetId.EditValue
        End Get
        Set(value As Integer?)
            INDSleBasicBillingBudgetId.EditValue = value
        End Set
    End Property

    Public Property BasicBillingBudgetXpo As XPInstantFeedbackSource Implements ISettingBilling.BasicBillingBudgetXpo
        Get
            Return INDSleBasicBillingBudgetId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleBasicBillingBudgetId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de entidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ConsignmentSalereCognition As Integer? Implements ISettingBilling.ConsignmentSalereCognition
        Get
            Return INDsleConsignmentSalereCognition.EditValue
        End Get
        Set(value As Integer?)
            INDsleConsignmentSalereCognition.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de entidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ReversalRecognitionConsignmentSale As Integer? Implements ISettingBilling.ReversalRecognitionConsignmentSale
        Get
            Return INDsleReversalRecognitionConsignmentSale.EditValue
        End Get
        Set(value As Integer?)
            INDsleReversalRecognitionConsignmentSale.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece el Compr reconocimiento venta en consignación
    ''' </summary>
    ''' <returns></returns>
    Public Property ConsignmentSalereCognitionXpo As XPInstantFeedbackSource Implements ISettingBilling.ConsignmentSalereCognitionXpo
        Get
            Return TryCast(INDsleConsignmentSalereCognition.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleConsignmentSalereCognition.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' obtiene o establece el Compr reversión reconocimiento venta en consignación
    ''' </summary>
    ''' <returns></returns>
    Public Property ReversalRecognitionConsignmentSaleXpo As XPInstantFeedbackSource Implements ISettingBilling.ReversalRecognitionConsignmentSaleXpo
        Get
            Return TryCast(INDsleReversalRecognitionConsignmentSale.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleReversalRecognitionConsignmentSale.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de movimiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property GiftProductOutletConcept As Integer? Implements ISettingBilling.GiftProductOutletConcept
        Get
            Return INDsleGiftProductOutletConcept.EditValue
        End Get
        Set(value As Integer?)
            INDsleGiftProductOutletConcept.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece el Compr reversión reconocimiento venta en consignación
    ''' </summary>
    ''' <returns></returns>
    Public Property GiftProductOutletConceptXpo As XPInstantFeedbackSource Implements ISettingBilling.GiftProductOutletConceptXpo
        Get
            Return TryCast(INDsleGiftProductOutletConcept.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleGiftProductOutletConcept.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece el datasource del campo moneda especifica
    ''' </summary>
    ''' <returns></returns>
    Public Property SpecificCurrencytXpo As XPInstantFeedbackSource Implements ISettingBilling.SpecificCurrencytXpo
        Get
            Return INDSleSpecificCurrency.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleSpecificCurrency.Properties.DataSource = value
        End Set
    End Property

#End Region

#End Region

#Region "Const"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Billing"

    Dim filter() As Object = {5, True}

#End Region

#Region "Variables"

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private _record As BlockRecordBilling

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PSettingBilling

    ''' <summary>
    ''' Representa la entidad de parametros de pago
    ''' </summary>
    ''' <remarks></remarks>
    Dim settingBilling As SettingsBilling

    ''' <summary>
    ''' Variable que contiene la lista de tipos de redondeos para la cuota de recuperación
    ''' </summary>
    Dim ListRoundingTypeRecoveryFeeType As New List(Of Tuple(Of Integer, String))

    Dim listPermissionCategories As List(Of Tuple(Of Byte, String))

    ''' <summary>
    ''' Variable que contiene la lista de tipos de asociacion de centro de costo
    ''' </summary>
    Dim ListAssociatedCostCenter As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable que contiene la lista de tipos de distribucion de monto fijo
    ''' </summary>
    Dim listDistributeCapitationControls As New List(Of Tuple(Of Byte, String))
#End Region

#Region "ICrud"

    Public Sub Buscar() Implements ICrudBase.Buscar

    End Sub

    ''' <summary>
    ''' Método : Deshacer
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
        DeleteBlockedRecord()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' Método : Guardar
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If ValidateControls() = False Then
            Exit Sub
        End If

        AssigningValues()

        Try
            Using model As New MSettingBilling(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await model.SaveSettingBilling(settingBilling)
                If Result.StateResult = True Then
                    If settingBilling.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                    ElseIf settingBilling.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                    End If
                    Me.settingBilling = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)

                    DeleteBlockedRecord()
                    CleanControls()
                    Await LoadControls()
                    INDsleRoundingTypeRecoveryFeeType.Focus()

                    Me.BarraBotones.CleanAuditBasic()
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), Result.ObjectEmbbeded.CreationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), Result.ObjectEmbbeded.CreationDate)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), Result.ObjectEmbbeded.ModificationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), Result.ObjectEmbbeded.ModificationDate)
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
                Else
                    AsyncLoader(False)
                    If Result.MessageResult Is Nothing Then
                        Mensaje(EeventViewerImages.Advertencia) = Result.Message
                    Else
                        If Result.MessageResult(0) = ErrorConcurrencia Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                        End If
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    Public Sub Nuevo() Implements ICrudBase.Nuevo

    End Sub

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch

    End Sub

#End Region

#Region "Events"

#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _record = Nothing
        _idOperativeUnit = Nothing
        Presenter = Nothing
        settingBilling = Nothing
        ListRoundingTypeRecoveryFeeType = Nothing
        listPermissionCategories = Nothing
        ListAssociatedCostCenter = Nothing
        ConsignmentSalereCognition = Nothing
        ConsignmentSalereCognitionXpo = Nothing
        ReversalRecognitionConsignmentSale = Nothing
        ReversalRecognitionConsignmentSaleXpo = Nothing
        GiftProductOutletConcept = Nothing
        GiftProductOutletConceptXpo = Nothing
        SpecificCurrencytXpo = Nothing
    End Sub


    ''' <summary>
    ''' Se dispara al cargar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmSettingBilling_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDLcBase, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        Presenter = New PSettingBilling(Me)
        InitializeTuples()
        'LoadStatus()
        If INDSleMainAccount.Properties.Buttons.Count > 1 Then
            INDSleMainAccount.Properties.Buttons(1).Visible = False
        End If
        ApplyBasicBilling = False
        GeneratePromissoryNote = False
        ValidateAgeOfMajority = False
        DistributeCapitationControls = False
        AnulateInvoicesPreviousPeriods = True
        BudgetInterface = False
        AuthorizationNumberControl = False
        ValidatePackaging = False
        AccountingPackage = False
        ValidateIntegrationMipres = False
        INDGleIncomeLockType.EditValue = 1
        Await LoadControls()
    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento cerrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmSettingBilling_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "Activated"

    ''' <summary>
    ''' se dispara cuando se activa el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmSettingBilling_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        INDsleRoundingTypeRecoveryFeeType.Focus()
    End Sub

#End Region

#Region "ButtonClick"

    Private Sub INDsleInvoiceProductDevolutionPartialConceptNote_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleInvoiceProductDevolutionPartialConceptNote.ButtonClick
        If e.Button.Kind = ButtonPredefines.Plus Then
            OpenForm(677, Nothing, True)
            INDsleInvoiceProductDevolutionPartialConceptNote.Properties.DataSource = Presenter.InitializeConceptNote()
        End If
    End Sub

    Private Sub INDSleParticularHealthAdministratorId_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDSleParticularHealthAdministratorId.ButtonClick
        If e.Button.Kind = ButtonPredefines.Plus Then
            OpenForm("972", Nothing, True)
        End If
    End Sub

    Private Sub INDsleFunctionalUnit_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleFunctionalUnit.ButtonClick
        If e.Button.Kind = ButtonPredefines.Plus Then
            OpenForm(523, Nothing, True)
            Presenter.InitializeFunctionalUnit()
        End If
    End Sub

    Private Sub INDSleRecoveryFeeDiscountMainAccount_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDSleRecoveryFeeDiscountMainAccount.ButtonClick
        If e.Button.Kind = ButtonPredefines.Plus Then
            OpenForm("602", Nothing, True)
        End If
    End Sub

    Private Sub SearchLookUpEdit1_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDSleCostCenterRecoveryFeeDiscount.ButtonClick
        If e.Button.Kind = ButtonPredefines.Plus Then
            OpenForm("517", Nothing, True)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al abrir el formulario correspondiente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleInvoiceJournalVoucherTypeId_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleInvoiceJournalVoucherTypeId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmDocumentType With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al abrir el formulario correspondiente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleProductInvoiceJournalVourcherTypeId_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDSleProductInvoiceJournalVourcherTypeId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmDocumentType With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al abrir el formulario correspondiente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleBasicBillingJournalVourcherTypeId_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDSleBasicBillingJournalVourcherTypeId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmDocumentType With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al abrir el formulario correspondiente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleInvoiceEntityCapitatedDistributionJournalVoucherTypeId_ButtonClick(sender As Object, e As ButtonPressedEventArgs)
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmDocumentType With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al abrir el formulario correspondiente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleReverseInvoiceEntityCapitatedDistributionJournalVoucherTypeId_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleReverseInvoiceEntityCapitatedDistributionJournalVoucherTypeId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmDocumentType With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al abrir el formulario correspondiente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleInvoiceAnnulmentJournalVoucherTypeId_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleInvoiceAnnulmentJournalVoucherTypeId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmDocumentType With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al abrir el formulario correspondiente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleBasicBillingAnnulmentJournalVoucherTypeId_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDSleBasicBillingAnnulmentJournalVoucherTypeId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmDocumentType With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al abrir el formulario correspondiente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleReverseTransferJournalVoucherTypeId_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleReverseTransferJournalVoucherTypeId.ButtonClick, INDsleReverseRecognitionJournalVoucherTypeId.ButtonClick, INDsleRecognitionJournalVoucherTypeId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmDocumentType With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario correspondiente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCapitationRevenueMainAccountId_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleCapitationRevenueMainAccountId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmPopupPUC With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario correspondiente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCapitationProfitMainAccountId_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleCapitationProfitMainAccountId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmPopupPUC With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario correspondiente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCapitationLossMainAccountId_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleCapitationLossMainAccountId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmPopupPUC With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario correspondiente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDslePatientAdvanceCashReceiptConceptId_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDSlePatientAdvanceCashReceiptConceptId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmCashReceiptsConcepts With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializePatientAdvanceCashReceiptConceptId()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario correspondiente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCapitedPatientAdvanceCashReceiptConceptId_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDSleCapitedPatientAdvanceCashReceiptConceptId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmCashReceiptsConcepts With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeCapitedPatientAdvanceCashReceiptConceptId()
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleEntityCapitatedAuthorization control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleEntityCapitatedAuthorization_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleEntityCapitatedAuthorization.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("1517", INDsleEntityCapitatedAuthorization.EditValue, True)
            Presenter.InitializeEntityCapitatedBillingAuthorization()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario correspondiente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleIndvidualAdvanceCashReceiptConceptId_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDSleIndvidualAdvanceCashReceiptConceptId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmCashReceiptsConcepts With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeJournalVoucherType()
        End If
    End Sub

    Private Sub INDSleProductSalesCashReceiptConcept_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDSleProductSalesCashReceiptConcept.ButtonClick
        If e.Button.Kind = ButtonPredefines.Plus Then
            OpenForm("630", Nothing, True)
        End If
    End Sub

    Private Sub INDsleBasicBillingCashReceiptConcept_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleBasicBillingCashReceiptConcept.ButtonClick
        If e.Button.Kind = ButtonPredefines.Plus Then
            OpenForm("630", Nothing, True)
        End If
        Presenter.InitializeBasicBillingCashReceiptConceptXpo()
    End Sub

    Private Sub INDSleCostCenter_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDSleCostCenter.ButtonClick
        If e.Button.Kind = ButtonPredefines.Plus Then
            OpenForm("517", Nothing, True)
        End If
    End Sub

    Private Sub INDsleClientMainAccount_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleClientMainAccount.ButtonClick
        If e.Button.Kind = ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            ClientMainAccountXpo = Presenter.InitializeMainAccount()
        End If
    End Sub

    Private Sub INDsleIVAPaymentMainAccount_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleIVAPaymentMainAccount.ButtonClick
        If e.Button.Kind = ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            IVAPaymentMainAccountXpo = Presenter.InitializeMainAccount()
        End If
    End Sub

    Private Sub INDsleReteIVAConcept_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleReteIVAConcept.ButtonClick
        If e.Button.Kind = ButtonPredefines.Plus Then
            OpenForm(605, Nothing, True)
            Presenter.InitializeFunctionalUnit()
        End If
    End Sub

    Private Sub INDsleReteIVAMainAccount_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleReteIVAMainAccount.ButtonClick
        If e.Button.Kind = ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            ReteIVAMainAccountXpo = Presenter.InitializeMainAccount()
        End If
    End Sub

    Private Sub INDsleReteICAMainAccount_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleReteICAMainAccount.ButtonClick
        If e.Button.Kind = ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            ReteICAMainAccountXpo = Presenter.InitializeMainAccount()
        End If
    End Sub

    Private Sub INDsleReteFuenteMainAccount_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleReteFuenteMainAccount.ButtonClick
        If e.Button.Kind = ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            ReteFuenteMainAccountXpo = Presenter.InitializeMainAccount()
        End If
    End Sub

    Private Sub INDSleNullifyBillingOperationalCurrent_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDSleNullifyBillingOperationalCurrent.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmPopupPUC With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeJournalVoucherType()
        End If
    End Sub

    Private Sub INDSleNullifyBillingNoOperationalCurrent_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDSleNullifyBillingNoOperationalCurrent.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmPopupPUC With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeJournalVoucherType()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    Private Sub INDsleInvoiceProductDevolutionPartialConceptNote_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleInvoiceProductDevolutionPartialConceptNote.QueryPopUp
        If INDsleInvoiceProductDevolutionPartialConceptNote.Properties.DataSource Is Nothing Then
            INDsleInvoiceProductDevolutionPartialConceptNote.Properties.DataSource = Presenter.InitializeConceptNote()
        End If
    End Sub

    Private Sub INDSleParticularHealthAdministratorId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleParticularHealthAdministratorId.QueryPopUp
        If ParticularHealthAdministratorXpo Is Nothing Then
            Presenter.InitializeParticularHealthAdministrator()
        End If
    End Sub

    Private Sub INDsleFunctionalUnit_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleFunctionalUnit.QueryPopUp
        If FunctionalUnitXpo Is Nothing Then
            Presenter.InitializeFunctionalUnit()
        End If
    End Sub

    Private Sub INDsleHealthProfessional_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleHealthProfessional.QueryPopUp
        If HealthProfessionalXpo Is Nothing Then
            Presenter.InitializeHelathProfessional()
        End If
    End Sub

    Private Sub INDSleRecoveryFeeDiscountMainAccount_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleRecoveryFeeDiscountMainAccount.QueryPopUp
        If INDSleRecoveryFeeDiscountMainAccount.Properties.DataSource Is Nothing Then
            Presenter.InitializeRecoveryFeeDiscountMainAccount()
        End If
    End Sub

    Private Sub INDSleCostCenterRecoveryFeeDiscount_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleCostCenterRecoveryFeeDiscount.QueryPopUp
        If RecoveryFeeCostCenterDiscountXPO Is Nothing Then
            Presenter.InitializeRecoveryFeeDiscountCostCenter()
        End If
    End Sub


    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleEntityCapitatedAuthorization control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleEntityCapitatedAuthorization_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleEntityCapitatedAuthorization.QueryPopUp
        If EntityCapitatedBillingAuthorizationXpo Is Nothing Then
            Presenter.InitializeEntityCapitatedBillingAuthorization()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de cuentas contables
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCapitationRevenueMainAccountId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCapitationRevenueMainAccountId.QueryPopUp
        If CapitationRevenueMainAccountXpo Is Nothing Then
            Presenter.InitializeCapitationRevenueMainAccountId()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de cuentas contables
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCapitationProfitMainAccountId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCapitationProfitMainAccountId.QueryPopUp
        If CapitationProfitMainAccountXpo Is Nothing Then
            Presenter.InitializeCapitationProfitMainAccountId()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de cuentas contables
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCapitationLossMainAccountId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCapitationLossMainAccountId.QueryPopUp
        If CapitationLossMainAccountXpo Is Nothing Then
            Presenter.InitializeCapitationLossMainAccountId()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de conceptos de recibos de cajas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDslePatientAdvanceCashReceiptConceptId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSlePatientAdvanceCashReceiptConceptId.QueryPopUp
        If PatientAdvanceCashReceiptConceptXpo Is Nothing Then
            Presenter.InitializePatientAdvanceCashReceiptConceptId()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de conceptos de recibos de cajas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCapitedPatientAdvanceCashReceiptConceptId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleCapitedPatientAdvanceCashReceiptConceptId.QueryPopUp
        If CapitedPatientAdvanceCashReceiptConceptXpo Is Nothing Then
            Presenter.InitializeCapitedPatientAdvanceCashReceiptConceptId()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de conceptos de recibos de cajas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleIndvidualAdvanceCashReceiptConceptId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleIndvidualAdvanceCashReceiptConceptId.QueryPopUp
        If IndvidualAdvanceCashReceiptConceptXpo Is Nothing Then
            Presenter.InitializeIndvidualAdvanceCashReceiptConceptId()
        End If
    End Sub

    Private Sub INDSleProductSalesCashReceiptConcept_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleProductSalesCashReceiptConcept.QueryPopUp
        If ProductSalesCashReceiptConceptXpo Is Nothing Then
            Presenter.InitializeProductSalesCashReceiptConcept()
        End If
    End Sub

    Private Sub INDsleBasicBillingCashReceiptConcept_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleBasicBillingCashReceiptConcept.QueryPopUp
        If BasicBillingCashReceiptConceptXpo Is Nothing Then
            Presenter.InitializeBasicBillingCashReceiptConceptXpo()
        End If
    End Sub

    Private Sub INDSleCostCenter_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleCostCenter.QueryPopUp
        If CostCenterXPO Is Nothing Then
            If INDSleCostCenter.Properties.ReadOnly = False Then
                Presenter.InitializeCostCenterXPO()
            End If
        End If
    End Sub
    ''' <summary>
    ''' Evento que trae las cuentas de movimientos que despliega la Cuenta Contable Anulación Factura Operacional Vigencia Anterior
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleNullifyBillingOperationalCurrent_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleNullifyBillingOperationalCurrent.QueryPopUp
        If INDSleNullifyBillingOperationalCurrent.Properties.DataSource Is Nothing Then
            Presenter.InitializeNullifyBillingOperationalCurrent()
        End If
    End Sub
    ''' <summary>
    '''  Evento que trae las cuentas de movimientos que despliega la Cuenta Contable Anulación Factura NO Operacional Vigencia Anterior
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleNullifyBillingNoOperationalCurrent_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleNullifyBillingNoOperationalCurrent.QueryPopUp
        If INDSleNullifyBillingNoOperationalCurrent.Properties.DataSource Is Nothing Then
            Presenter.InitializNullifyBillingNoOperationalCurrent()
        End If
    End Sub


    Private Sub INDsleClientMainAccount_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleClientMainAccount.QueryPopUp
        If ClientMainAccountXpo Is Nothing Then
            ClientMainAccountXpo = Presenter.InitializeMainAccount()
        End If
    End Sub

    Private Sub INDsleIVAPaymentMainAccount_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleIVAPaymentMainAccount.QueryPopUp
        If IVAPaymentMainAccountXpo Is Nothing Then
            IVAPaymentMainAccountXpo = Presenter.InitializeMainAccount()
        End If
    End Sub

    Private Sub INDsleReteIVAConcept_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleReteIVAConcept.QueryPopUp
        If ReteIVAConceptXpo Is Nothing Then
            ReteIVAConceptXpo = Presenter.InitializeConcept()
        End If
    End Sub

    Private Sub INDsleReteIVAMainAccount_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleReteIVAMainAccount.QueryPopUp
        If ReteIVAMainAccountXpo Is Nothing Then
            ReteIVAMainAccountXpo = Presenter.InitializeMainAccount()
        End If
    End Sub

    Private Sub INDsleReteICAMainAccount_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleReteICAMainAccount.QueryPopUp
        If ReteICAMainAccountXpo Is Nothing Then
            ReteICAMainAccountXpo = Presenter.InitializeMainAccount()
        End If
    End Sub

    Private Sub INDsleReteFuenteMainAccount_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleReteFuenteMainAccount.QueryPopUp
        If ReteFuenteMainAccountXpo Is Nothing Then
            ReteFuenteMainAccountXpo = Presenter.InitializeMainAccount()
        End If
    End Sub

    Private Sub INDsleBudgetaryEntityId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleBudgetaryEntityId.QueryPopUp
        If BudgetaryEntityXpo Is Nothing Then
            Presenter.InitializeBudgetaryEntity()
        End If
    End Sub

    Private Sub INDsleBudgetaryValidityId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleBudgetaryValidityId.QueryPopUp
        If BudgetaryValidityXpo Is Nothing Then
            Presenter.InitializeBudgetaryValidity(BudgetaryEntityId)
        End If
    End Sub

    Private Sub INDsleBillingBudgetId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleDependencyId.QueryPopUp
        If DependencyXpo Is Nothing Then
            Presenter.InitializeDependency(BudgetaryValidityId)
        End If
    End Sub

    Private Sub INDSleBasicBillingDependencyId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleBasicBillingDependencyId.QueryPopUp
        If BasicBillingDependencyXpo Is Nothing Then
            Presenter.InitializeBasicBillingDependency(BudgetaryValidityId)
        End If
    End Sub

    Private Sub INDSleBasicBillingBudgetId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleBasicBillingBudgetId.QueryPopUp
        If BasicBillingBudgetXpo Is Nothing Then
            Presenter.InitializeBasicBillingBudget(BudgetaryValidityId)
        End If
    End Sub

    Private Sub INDSleStatusFolioNew_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleStatusFolioNew.QueryPopUp, INDSleStatusFolioClosed.QueryPopUp
        If INDSleStatusFolioNew.Properties.DataSource Is Nothing OrElse INDSleStatusFolioClosed.Properties.DataSource Is Nothing Then
            Dim Status As Boolean = True
            Dim Datasource As XPInstantFeedbackSource = XpoServiceEx.Instance(indigo.TransactionalContainer).BillingService.ListConceptsCausesStatusFolio(Status)
            INDSleStatusFolioNew.Properties.DataSource = Datasource
        End If
    End Sub

    Private Sub INDSleStatusFolioClosed_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleStatusFolioClosed.QueryPopUp
        If INDSleStatusFolioClosed.Properties.DataSource Is Nothing Then
            Dim Status As Boolean = True
            INDSleStatusFolioClosed.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).BillingService.ListConceptsCausesStatusFolio(Status)
        End If
    End Sub

    ''' <summary>
    ''' Se encarga de setear el Datasource de todos los contoles dispuestos, con los tipos de comprobantes contables
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ControlsByJournalVoucherType_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleReversionLiquidatedPackageJournalVoucherTypeId.QueryPopUp, INDSleLiquidatedPackageJournalVoucherTypeId.QueryPopUp,
                                                                                                INDsleReverseTransferJournalVoucherTypeId.QueryPopUp, INDsleRecognitionJournalVoucherTypeId.QueryPopUp,
                                                                                                INDsleReverseRecognitionJournalVoucherTypeId.QueryPopUp, INDSleBasicBillingAnnulmentJournalVoucherTypeId.QueryPopUp,
                                                                                                INDsleInvoiceAnnulmentJournalVoucherTypeId.QueryPopUp, INDsleReverseInvoiceEntityCapitatedDistributionJournalVoucherTypeId.QueryPopUp,
                                                                                                INDsleInvoiceEntityCapitatedDistributionJournalVoucherType.QueryPopUp, INDSleBasicBillingJournalVourcherTypeId.QueryPopUp,
                                                                                                INDSleProductInvoiceJournalVourcherTypeId.QueryPopUp, INDsleInvoiceJournalVoucherTypeId.QueryPopUp,
                                                                                                INDSleAccountingVoucherGeneration.QueryPopUp, INDSleAccountingVoucherReversal.QueryPopUp

        ChargeDatasourceJournalVoucherTypes(sender)
    End Sub

    ''' <summary>
    ''' Se encarga de cargar los tipos de comprobantes contables
    ''' </summary>
    ''' <param name="_Control"></param>
    Public Sub ChargeDatasourceJournalVoucherTypes(_Control As Object, Optional _flag As Boolean = False)
        If _Control.Properties.DataSource Is Nothing OrElse _flag Then
            _Control.Properties.DataSource = Presenter.InitializeJournalVoucherType()
        End If
    End Sub

    Private Sub INDsleAccountingPackageMainAccountId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleAccountingPackageMainAccountId.QueryPopUp
        If AccountingPackageMainAccountXpo Is Nothing Then
            AccountingPackageMainAccountXpo = Presenter.InitializeOrderMainAccount()
        End If
    End Sub

    Private Sub INDsleProjectedVariationPriceMainAccountId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleProjectedVariationPriceMainAccountId.QueryPopUp
        If ProjectedVariationPriceMainAccountXpo Is Nothing Then
            ProjectedVariationPriceMainAccountXpo = Presenter.InitializeOrderMainAccount()
        End If
    End Sub
    ''' <summary>
    ''' Evento que se dispara al desplegar el Compr reconocimiento venta en consignación
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleConsignmentSalereCognition_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleConsignmentSalereCognition.QueryPopUp
        If ConsignmentSalereCognitionXpo Is Nothing Then
            Presenter.InitializeConsignmentSalereCognitionXpo()
        End If
    End Sub
    ''' <summary>
    ''' Evento que se dispara al desplegar el Compr reversión reconocimiento venta en consignación
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleReversalRecognitionConsignmentSale_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleReversalRecognitionConsignmentSale.QueryPopUp
        If ReversalRecognitionConsignmentSaleXpo Is Nothing Then
            Presenter.InitializeReversalRecognitionConsignmentSaleXpo()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar Concepto salida producto obsequio
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleGiftProductOutletConcept_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleGiftProductOutletConcept.QueryPopUp
        If GiftProductOutletConceptXpo Is Nothing Then
            Presenter.InitializeGiftProductOutletConceptXpo()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar Moneda especifica
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleSpecificCurrency_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleSpecificCurrency.QueryPopUp
        If SpecificCurrencytXpo Is Nothing Then
            Presenter.InitializeSpecificCurrency()
        End If
    End Sub

    Private Sub INDSleBillingAuthorizationCopay_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleBillingAuthorizationCopay.QueryPopUp
        If INDSleBillingAuthorizationCopay.Properties.DataSource Is Nothing Then
            INDSleBillingAuthorizationCopay.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).BillingService.ListXPInstantFeedbackSource(Of BillingRepository.BillingAuthorizationXpo)("Status=True")
        End If
    End Sub

    Private Sub INDSleBillingAuthorization_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleBillingAuthorization.QueryPopUp
        If INDsleBillingAuthorization.Properties.DataSource Is Nothing Then
            INDsleBillingAuthorization.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).BillingService.ListXPInstantFeedbackSource(Of BillingRepository.BillingAuthorizationXpo)("Status=True")
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de si aplica Integración con Mipres
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleMiPres_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleMiPres.EditValueChanged
        If ValidateIntegrationMipres Then
            INDLciClientId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciClientSecret.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else

            ClientNameId = Nothing
            ClientSecret = Nothing
            INDLciClientId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciClientSecret.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de si aplica tiquete electronica de venta
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleElectronicSalesTicket_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleElectronicSalesTicket.EditValueChanged
        If ApplyElectronicSalesTicket Then
            INDlygElectronicSalesTicket.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciBillingAuthorization.ShowLayout()
            INDLciAccountingVoucherGeneration.ShowLayout()
            INDLciAccountingVoucherReversal.ShowLayout()
        Else
            INDlygElectronicSalesTicket.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciBillingAuthorization.HideLayout()
            INDLciAccountingVoucherGeneration.HideLayout()
            INDLciAccountingVoucherReversal.HideLayout()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de si aplica facturacion basica
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleApplyBasicBilling_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleApplyBasicBilling.EditValueChanged
        HideGroup()
    End Sub

    Private Sub INDSleProductSalesCashReceiptConcept_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleProductSalesCashReceiptConcept.EditValueChanged
        If INDSleProductSalesCashReceiptConcept.EditValue IsNot Nothing Then
            Using model As New MCashReceiptsConcepts(MyTag)
                Dim concept = model.GetCashReceiptConceptById(INDSleProductSalesCashReceiptConcept.EditValue)
                Using modelPuc As New MPUC(MyTag)
                    Dim account = modelPuc.GetAccountByIdSimple(concept.IdMainAccount)
                    INDSleMainAccount.EditValue = account.Id
                    INDSleMainAccount.Properties.NullText = account.Number + " - " + account.Name
                    If account.HandlesCostCenter Then
                        INDLciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        INDLciCostCenter.ShowInCustomizationForm = False
                        INDLciCostCenter.AllowHide = False
                    Else
                        INDLciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        INDLciCostCenter.ShowInCustomizationForm = True
                        INDLciCostCenter.AllowHide = True
                        INDSleCostCenter.EditValue = Nothing
                    End If
                End Using
            End Using
        End If
    End Sub

    Private Sub INDSleRecoveryFeeDiscountMainAccount_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleRecoveryFeeDiscountMainAccount.EditValueChanged
        If _loadingControls Then
            Exit Sub
        End If
        If INDSleRecoveryFeeDiscountMainAccount.EditValue IsNot Nothing Then
            Dim MainAccountXpo = CType(CType(INDGVMainAccountRecoveryFeeDiscount.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.AccountingRepository.PUCServiceXpo)
            If MainAccountXpo.HandlesCostCenter Then
                INDLciCostCenterRecoveryFeeDiscount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                INDLciCostCenterRecoveryFeeDiscount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDSleCostCenterRecoveryFeeDiscount.EditValue = Nothing
            End If
        Else
            INDLciCostCenterRecoveryFeeDiscount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDSleCostCenterRecoveryFeeDiscount.EditValue = Nothing
        End If
    End Sub

    Private Sub INDsleBudgetInterface_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleBudgetInterface.EditValueChanged
        INDLciBudgetaryEntityId.AllowHide = Not BudgetInterface
        INDLciBudgetaryEntityId.ShowInCustomizationForm = Not BudgetInterface
        INDLciBudgetaryValidityId.AllowHide = Not BudgetInterface
        INDLciBudgetaryValidityId.ShowInCustomizationForm = Not BudgetInterface
        INDLciDependencyId.AllowHide = Not BudgetInterface
        INDLciDependencyId.ShowInCustomizationForm = Not BudgetInterface
        INDLciBasicBillingDependencyId.AllowHide = Not BudgetInterface
        INDLciBasicBillingDependencyId.ShowInCustomizationForm = Not BudgetInterface
        INDLciBasicBillingBudgetId.AllowHide = Not BudgetInterface
        INDLciBasicBillingBudgetId.ShowInCustomizationForm = Not BudgetInterface
        INDlygBudgetInterface.Visibility = If(BudgetInterface, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)

        If Not BudgetInterface Then
            CleanBudgetInterface(0)
        End If
    End Sub

    Private Sub INDsleBudgetaryEntityId_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleBudgetaryEntityId.EditValueChanged
        CleanBudgetInterface(1)
    End Sub

    Private Sub INDsleBudgetaryValidityId_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleBudgetaryValidityId.EditValueChanged
        CleanBudgetInterface(2)
    End Sub

    Private Sub INDGleValidatePacking_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleValidatePacking.EditValueChanged
        INDLciAccountingPackage.HideControl(Not ValidatePackaging)

        If Not ValidatePackaging Then
            AccountingPackage = False
        End If
    End Sub

    Private Sub INDGleAccountingPackage_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleAccountingPackage.EditValueChanged
        INDLciLiquidatedPackageJournalVoucherTypeId.HideControl(Not AccountingPackage)
        INDLciReversionLiquidatedPackageJournalVoucherTypeId.HideControl(Not AccountingPackage)
        INDLciAccountingPackageMainAccountId.HideControl(Not AccountingPackage)
        INDLciProjectedVariationPriceMainAccountId.HideControl(Not AccountingPackage)

        If Not AccountingPackage Then
            LiquidatedPackageJournalVoucherTypeId = Nothing
            INDSleLiquidatedPackageJournalVoucherTypeId.Properties.NullText = String.Empty
            ReversionLiquidatedPackageJournalVoucherTypeId = Nothing
            INDSleReversionLiquidatedPackageJournalVoucherTypeId.Properties.NullText = String.Empty
            AccountingPackageMainAccountId = Nothing
            INDSleAccountingPackageMainAccountId.Properties.NullText = String.Empty
            ProjectedVariationPriceMainAccountId = Nothing
            INDSleProjectedVariationPriceMainAccountId.Properties.NullText = String.Empty
        End If
    End Sub

    Private Sub INDsleHasCustomTRM_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleHasCustomTRM.EditValueChanged
        If HasCustomTRM Then
            INDLyTrm.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDLyTrm.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    Private Sub INDsleLiquidateFolioInSpecificCurrency_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleLiquidateFolioInSpecificCurrency.EditValueChanged
        If LiquidateFolioInSpecificCurrency Then
            INDLyItemSpecificCurrency.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDLyItemSpecificCurrency.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub
#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo que oculta o muestra los controles de facturacion basica
    ''' </summary>
    Private Sub HideGroup()
        If ApplyBasicBilling Then
            INDlyItemClientMainAccount.AllowHide = False
            INDlyItemClientMainAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemIVAPaymentMainAccount.AllowHide = False
            INDlyItemIVAPaymentMainAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemReteICAMainAccount.AllowHide = False
            INDlyItemReteICAMainAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemReteFuenteMainAccount.AllowHide = False
            INDlyItemReteFuenteMainAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciConsignmentSalereCognition.AllowHide = False
            INDLciConsignmentSalereCognition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciReversalRecognitionConsignmentSale.AllowHide = False
            INDLciReversalRecognitionConsignmentSale.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemHandlesDifferentRates.AllowHide = False
            INDlyItemHandlesDifferentRates.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDlyItemClientMainAccount.AllowHide = True
            INDlyItemClientMainAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemIVAPaymentMainAccount.AllowHide = True
            INDlyItemIVAPaymentMainAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemReteICAMainAccount.AllowHide = True
            INDlyItemReteICAMainAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemReteFuenteMainAccount.AllowHide = True
            INDlyItemReteFuenteMainAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciConsignmentSalereCognition.AllowHide = True
            INDLciConsignmentSalereCognition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciReversalRecognitionConsignmentSale.AllowHide = True
            INDLciReversalRecognitionConsignmentSale.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemHandlesDifferentRates.AllowHide = True
            INDlyItemHandlesDifferentRates.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    ''' <summary>
    ''' Metodo que inicializa el datasource de las tuplas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeTuples()
        ListRoundingTypeRecoveryFeeType = New List(Of Tuple(Of Integer, String))
        ListRoundingTypeRecoveryFeeType.Add(New Tuple(Of Integer, String)(1, "Unidad"))
        ListRoundingTypeRecoveryFeeType.Add(New Tuple(Of Integer, String)(10, "Decena"))
        ListRoundingTypeRecoveryFeeType.Add(New Tuple(Of Integer, String)(100, "Centena"))
        ListRoundingTypeRecoveryFeeType.Add(New Tuple(Of Integer, String)(1000, "Unidad de Mil"))
        INDsleRoundingTypeRecoveryFeeType.Properties.DataSource = ListRoundingTypeRecoveryFeeType.ToList

        Dim listAccountingQx As New List(Of Tuple(Of Byte, String))()
        listAccountingQx.Add(New Tuple(Of Byte, String)(1, "Por Procedimiento"))
        listAccountingQx.Add(New Tuple(Of Byte, String)(2, "Detallada"))
        INDGleAccountingQx.Properties.DataSource = listAccountingQx

        Dim listCalculateTaxAdvance As New List(Of Tuple(Of Byte, String))()
        listCalculateTaxAdvance.Add(New Tuple(Of Byte, String)(0, "No"))
        listCalculateTaxAdvance.Add(New Tuple(Of Byte, String)(1, "Solo Informar"))
        listCalculateTaxAdvance.Add(New Tuple(Of Byte, String)(2, "Reconocer"))
        INDGleCalculateTaxAdvance.Properties.DataSource = listCalculateTaxAdvance

        Dim listApply As New List(Of Tuple(Of Boolean, String))()
        listApply.Add(New Tuple(Of Boolean, String)(True, "Si"))
        listApply.Add(New Tuple(Of Boolean, String)(False, "No"))

        INDsleApplyBasicBilling.Properties.DataSource = listApply.ToList()
        INDSleElectronicSalesTicket.Properties.DataSource = listApply.ToList()
        INDsleLiquidateSinceControlOutPatientService.Properties.DataSource = listApply.ToList()
        INDsleAnulateInvoicesPreviousPeriods.Properties.DataSource = listApply.ToList()
        INDsleBudgetInterface.Properties.DataSource = listApply.ToList()
        INDGleAuthorizationNumberControl.Properties.DataSource = listApply.ToList()
        INDGleValidateAgeOfMajority.Properties.DataSource = listApply.ToList()
        INDGleValidatePacking.Properties.DataSource = listApply
        INDGleAccountingPackage.Properties.DataSource = listApply
        INDsleMiPres.Properties.DataSource = listApply.ToList()
        INDsleHandlesDifferentRates.Properties.DataSource = listApply.ToList()
        INDsleHasCustomTRM.Properties.DataSource = listApply.ToList()
        INDsleAllowsSalesExecutiveAndSupplier.Properties.DataSource = listApply.ToList()
        INDsleAccountsConditionalCommercialDiscount.Properties.DataSource = listApply.ToList()
        INDsleLiquidateFolioInSpecificCurrency.Properties.DataSource = listApply.ToList()
        INDSleRequiresConditionsSale.Properties.DataSource = listApply.ToList()

        listPermissionCategories = New List(Of Tuple(Of Byte, String))
        listPermissionCategories.Add(New Tuple(Of Byte, String)(1, "Permiso de Usuario"))
        listPermissionCategories.Add(New Tuple(Of Byte, String)(2, "Permiso de Grupo de Atención"))
        listPermissionCategories.Add(New Tuple(Of Byte, String)(3, "Permiso de Grupo de Atención y Usuario"))
        INDGlePermissionCategories.Properties.DataSource = listPermissionCategories

        ListAssociatedCostCenter = New List(Of Tuple(Of Integer, String))
        ListAssociatedCostCenter.Add(New Tuple(Of Integer, String)(1, "Costo (Unidad Funcional)"))
        ListAssociatedCostCenter.Add(New Tuple(Of Integer, String)(2, "Costo (Grupo)"))
        INDSleAssociateCostCenter.Properties.DataSource = ListAssociatedCostCenter.ToList

        listDistributeCapitationControls = New List(Of Tuple(Of Byte, String))
        listDistributeCapitationControls.Add(New Tuple(Of Byte, String)(1, "Usando la cuenta contable de ingreso por Monto Fijo"))
        listDistributeCapitationControls.Add(New Tuple(Of Byte, String)(2, "Usando la cuenta de ingreso asociada a los Controles"))
        INDsleDistributeCapitationControls.Properties.DataSource = listDistributeCapitationControls.ToList()

        Dim listIncomeLockType As New List(Of Tuple(Of Byte, String))()
        listIncomeLockType.Add(New Tuple(Of Byte, String)(1, "Farmacia"))
        listIncomeLockType.Add(New Tuple(Of Byte, String)(2, "Facturación"))
        listIncomeLockType.Add(New Tuple(Of Byte, String)(3, "Farmacia y Facturación"))
        INDGleIncomeLockType.Properties.DataSource = listIncomeLockType

        Dim listGeneratePromissoryNote As New List(Of Tuple(Of Boolean, String))()
        listGeneratePromissoryNote.Add(New Tuple(Of Boolean, String)(True, "Si"))
        listGeneratePromissoryNote.Add(New Tuple(Of Boolean, String)(False, "No"))
        INDGleGeneratePromissoryNote.Properties.DataSource = listGeneratePromissoryNote.ToList()
    End Sub

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Metodo para limpiar los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        RoundingTypeRecoveryFeeType = Nothing
        INDsleRoundingTypeRecoveryFeeType.Properties.NullText = String.Empty

        RequiresPermissionForCxCPatient = Nothing
        INDsleRequiresPermissionForCxCPatient.Properties.NullText = String.Empty

        ParticularHealthAdministratorId = Nothing
        INDSleParticularHealthAdministratorId.Properties.NullText = String.Empty

        InvoiceJournalVoucherTypeId = Nothing
        INDsleInvoiceJournalVoucherTypeId.Properties.NullText = String.Empty

        INDSleProductInvoiceJournalVourcherTypeId.EditValue = Nothing
        INDSleProductInvoiceJournalVourcherTypeId.Properties.NullText = String.Empty

        BasicBillingJournalVoucherTypeId = Nothing
        INDSleBasicBillingJournalVourcherTypeId.Properties.NullText = String.Empty

        InvoiceEntityCapitatedDistributionJournalVoucherTypeId = Nothing
        INDsleInvoiceEntityCapitatedDistributionJournalVoucherType.Properties.NullText = String.Empty

        ReverseInvoiceEntityCapitatedDistributionJournalVoucherTypeId = Nothing
        INDsleReverseInvoiceEntityCapitatedDistributionJournalVoucherTypeId.Properties.NullText = String.Empty

        InvoiceAnnulmentJournalVoucherTypeId = Nothing
        INDsleInvoiceAnnulmentJournalVoucherTypeId.Properties.NullText = String.Empty

        BasicBillingAnnulmentJournalVoucherTypeId = Nothing
        INDSleBasicBillingAnnulmentJournalVoucherTypeId.Properties.NullText = String.Empty

        ReverseTransferJournalVoucherTypeId = Nothing
        INDsleReverseTransferJournalVoucherTypeId.Properties.NullText = String.Empty

        RecognitionJournalVoucherTypeId = Nothing
        INDsleRecognitionJournalVoucherTypeId.Properties.NullText = String.Empty

        ReverseRecognitionJournalVoucherTypeId = Nothing
        INDsleReverseRecognitionJournalVoucherTypeId.Properties.NullText = String.Empty

        CapitationRevenueMainAccountId = Nothing
        INDsleCapitationRevenueMainAccountId.Properties.NullText = String.Empty

        LiquidatedPackageJournalVoucherTypeId = Nothing
        INDSleLiquidatedPackageJournalVoucherTypeId.Properties.NullText = String.Empty
        ReversionLiquidatedPackageJournalVoucherTypeId = Nothing
        INDSleReversionLiquidatedPackageJournalVoucherTypeId.Properties.NullText = String.Empty
        AccountingPackageMainAccountId = Nothing
        INDSleAccountingPackageMainAccountId.Properties.NullText = String.Empty
        ProjectedVariationPriceMainAccountId = Nothing
        INDSleProjectedVariationPriceMainAccountId.Properties.NullText = String.Empty

        CapitationProfitMainAccountId = Nothing
        INDsleCapitationProfitMainAccountId.Properties.NullText = String.Empty

        CapitationLossMainAccountId = Nothing
        INDsleCapitationLossMainAccountId.Properties.NullText = String.Empty

        ReversalPreviousYearsMainAccountId = Nothing
        INDSleNullifyBillingOperationalCurrent.Properties.NullText = String.Empty
        ReversalPreviousYearsGenericBillingMainAccountId = Nothing
        INDSleNullifyBillingNoOperationalCurrent.Properties.NullText = String.Empty

        ConsecutiveControlCapitation = Nothing

        ConsignmentSalereCognition = Nothing
        ConsignmentSalereCognitionXpo = Nothing
        INDsleConsignmentSalereCognition.Properties.NullText = String.Empty
        ReversalRecognitionConsignmentSale = Nothing
        ReversalRecognitionConsignmentSaleXpo = Nothing
        INDsleReversalRecognitionConsignmentSale.Properties.NullText = String.Empty

        SpecificCurrencytXpo = Nothing


        GiftProductOutletConcept = Nothing
        GiftProductOutletConceptXpo = Nothing
        INDsleGiftProductOutletConcept.Properties.NullText = String.Empty

        CashReceiptsConfirm = Nothing
        INDSleCashReceiptsConfirm.Properties.NullText = String.Empty

        PatientAdvanceCashReceiptConceptId = Nothing
        INDSlePatientAdvanceCashReceiptConceptId.Properties.NullText = String.Empty

        CapitedPatientAdvanceCashReceiptConceptId = Nothing
        INDSleCapitedPatientAdvanceCashReceiptConceptId.Properties.NullText = String.Empty

        EntityCapitatedBillingAuthorizationId = Nothing
        INDsleEntityCapitatedAuthorization.Properties.NullText = String.Empty

        INDSleBillingAuthorizationCopay.EditValue = Nothing
        INDSleBillingAuthorizationCopay.Properties.NullText = String.Empty

        IndvidualAdvanceCashReceiptConceptId = Nothing
        INDSleIndvidualAdvanceCashReceiptConceptId.Properties.NullText = String.Empty

        INDSleProductSalesCashReceiptConcept.EditValue = Nothing
        INDSleProductSalesCashReceiptConcept.Properties.NullText = String.Empty

        INDsleBasicBillingCashReceiptConcept.EditValue = Nothing
        INDsleBasicBillingCashReceiptConcept.Properties.NullText = String.Empty

        InvoiceProductDevolutionPartialConceptNoteId = Nothing
        INDsleInvoiceProductDevolutionPartialConceptNote.Properties.NullText = String.Empty

        ' RecoveryFeeDiscountMainAccountId = Nothing
        INDSleRecoveryFeeDiscountMainAccount.EditValue = Nothing
        INDSleRecoveryFeeDiscountMainAccount.Properties.NullText = String.Empty

        RecoveryFeeDiscountCostCenterId = Nothing
        INDSleCostCenterRecoveryFeeDiscount.Properties.NullText = String.Empty

        INDSleMainAccount.EditValue = Nothing
        INDSleMainAccount.Properties.NullText = String.Empty

        INDTxtPrefixConsecutiveCapitation.EditValue = String.Empty

        INDGlePermissionCategories.EditValue = Nothing

        INDGleIncomeLockType.EditValue = 1
        ApplyElectronicSalesTicket = False
        INDlygGeneralInformation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        INDSleCostCenter.EditValue = Nothing
        INDSleCostCenter.Properties.NullText = String.Empty
        INDLciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciCostCenter.ShowInCustomizationForm = True
        INDLciCostCenter.AllowHide = True

        INDGleAccountingQx.EditValue = Nothing
        INDGleCalculateTaxAdvance.EditValue = Nothing

        ClientMainAccountId = Nothing
        INDsleClientMainAccount.Properties.NullText = String.Empty

        IVAPaymentMainAccountId = Nothing
        INDsleIVAPaymentMainAccount.Properties.NullText = String.Empty

        ReteIVAMainAccountId = Nothing
        INDsleReteIVAMainAccount.Properties.NullText = String.Empty

        ReteICAMainAccountId = Nothing
        INDsleReteICAMainAccount.Properties.NullText = String.Empty

        ReteFuenteMainAccountId = Nothing
        INDsleReteFuenteMainAccount.Properties.NullText = String.Empty

        AssociateCostCenter = Nothing
        ApplyBasicBilling = False
        ValidateAgeOfMajority = False
        GeneratePromissoryNote = False
        LiquidateSinceControlOutPatientService = False
        DistributeCapitationControls = False
        AnulateInvoicesPreviousPeriods = True
        BudgetInterface = False
        AuthorizationNumberControl = False
        ValidatePackaging = False
        AccountingPackage = False
        AccountControlValidation = False

        LiquidateMasterAccount = False
        HasCustomTRM = Nothing
        HandlesDifferentRates = False
        LiquidateFolioInSpecificCurrency = False
        SpecificCurrencyId = Nothing
        requiresConditionsSale = False
        AllowsSalesExecutiveAndSupplier = False
        AccountsConditionalCommercialDiscount = False
        FunctionalUnitId = Nothing
        INDsleFunctionalUnit.Properties.NullText = String.Empty
        HealthProfessionalCode = Nothing
        INDsleHealthProfessional.Properties.NullText = String.Empty

        BudgetaryEntityId = Nothing
        INDSleBudgetaryEntityId.Properties.NullText = String.Empty
        BudgetaryValidityId = Nothing
        INDSleBudgetaryValidityId.Properties.NullText = String.Empty
        DependencyId = Nothing
        INDSleDependencyId.Properties.NullText = String.Empty
        BasicBillingDependencyId = Nothing
        INDSleBasicBillingDependencyId.Properties.NullText = String.Empty
        BasicBillingBudgetId = Nothing
        INDSleBasicBillingBudgetId.Properties.NullText = String.Empty
        INDSleStatusFolioClosed.EditValue = Nothing
        INDSleStatusFolioClosed.Properties.NullText = String.Empty
        INDSleStatusFolioNew.EditValue = Nothing
        INDSleStatusFolioClosed.Properties.NullText = String.Empty

        ValidateIntegrationMipres = False
        ClientNameId = String.Empty
        ClientSecret = String.Empty
        INDsleAccountsConditionalCommercialDiscount.EditValue = indigo.LanguageCulture = "es-CR"
        Me.MaxInvoiceItems = 0
    End Sub

    ''' <summary>
    ''' Deletes the blocked record.
    ''' </summary>
    Public Async Sub DeleteBlockedRecord()
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MBlockRecordAndSequense(CStr(Me.Tag))
                Await Model.DeleteBlockRecord(_record)
                _record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.ICrudBase.Mensaje
        Set(value As String)

            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    ''' <summary>
    ''' Loads the controls.
    ''' </summary>
    Private Async Function LoadControls() As Task
        Using Model As New MSettingBilling(CStr(Me.Tag))
            AsyncLoader(True)
            Try
                Dim resulOperation = Await Model.GetSettingsBillingByIdUnitOperative(Me._idOperativeUnit)
                settingBilling = resulOperation.ObjectEmbbeded
                If settingBilling IsNot Nothing AndAlso settingBilling.Id > 0 Then
                    Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                        Dim result = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(settingBilling.Id))

                        _loadingControls = True
                        With settingBilling
                            LayoutControls.SetCustomFieldsValue(.CustomProperties)

                            Me.BarraBotones.CleanAuditBasic()
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                            ParticularHealthAdministratorId = .ParticularHealthAdministratorId
                            INDSleParticularHealthAdministratorId.Properties.NullText = .ParticularHealthAdministratorDescription

                            InvoiceJournalVoucherTypeId = .InvoiceJournalVoucherTypeId
                            INDsleInvoiceJournalVoucherTypeId.Properties.NullText = .InvoiceJournalVoucherTypeDescription

                            INDSleProductInvoiceJournalVourcherTypeId.EditValue = .ProductInvoiceJournalVoucherTypeId
                            INDSleProductInvoiceJournalVourcherTypeId.Properties.NullText = .ProductInvoiceJournalVoucherTypeDescription

                            BasicBillingJournalVoucherTypeId = .BasicBillingJournalVoucherTypeId
                            INDSleBasicBillingJournalVourcherTypeId.Properties.NullText = .BasicBillingJournalVoucherTypeDescription

                            InvoiceEntityCapitatedDistributionJournalVoucherTypeId = .InvoiceEntityCapitatedDistributionJournalVoucherTypeId
                            INDsleInvoiceEntityCapitatedDistributionJournalVoucherType.Properties.NullText = .InvoiceEntityCapitatedDistributionJournalVoucherTypeDescription

                            ReverseInvoiceEntityCapitatedDistributionJournalVoucherTypeId = .ReverseInvoiceEntityCapitatedDistributionJournalVoucherTypeId
                            INDsleReverseInvoiceEntityCapitatedDistributionJournalVoucherTypeId.Properties.NullText = .ReverseInvoiceEntityCapitatedDistributionJournalVoucherTypeDescription

                            InvoiceAnnulmentJournalVoucherTypeId = .InvoiceAnnulmentJournalVoucherTypeId
                            INDsleInvoiceAnnulmentJournalVoucherTypeId.Properties.NullText = .InvoiceAnnulmentJournalVoucherTypeDescription

                            BasicBillingAnnulmentJournalVoucherTypeId = .BasicBillingAnnulmentJournalVoucherTypeId
                            INDSleBasicBillingAnnulmentJournalVoucherTypeId.Properties.NullText = .BasicBillingAnnulmentJournalVoucherTypeDescription

                            ReverseTransferJournalVoucherTypeId = .ReverseTransferJournalVoucherTypeId
                            INDsleReverseTransferJournalVoucherTypeId.Properties.NullText = .ReverseTransferJournalVoucherTypeDescription

                            RecognitionJournalVoucherTypeId = .RecognitionJournalVoucherTypeId
                            INDsleRecognitionJournalVoucherTypeId.Properties.NullText = .RecognitionJournalVoucherTypeDescription

                            ReverseRecognitionJournalVoucherTypeId = .ReverseRecognitionJournalVoucherTypeId
                            INDsleReverseRecognitionJournalVoucherTypeId.Properties.NullText = .ReverseRecognitionJournalVoucherTypeDescription

                            CashReceiptsConfirm = .CashReceiptsConfirm

                            RoundingTypeRecoveryFeeType = .RoundingTypeRecoveryFeeType

                            If indigo.LanguageCulture <> "es-CO" Then
                                INDlygInvoiceCopaymentParameters.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                            End If

                            INDSleBillingAuthorizationCopay.EditValue = .BillingAuthorizationCopayId
                            INDSleBillingAuthorizationCopay.Properties.NullText = .BillingAuthorizationCopayCodeName

                            CapitationRevenueMainAccountId = .CapitationRevenueMainAccountId
                            INDsleCapitationRevenueMainAccountId.Properties.NullText = .CapitationRevenueMainAccountDescription

                            LiquidatedPackageJournalVoucherTypeId = .LiquidatedPackageJournalVoucherTypeId
                            INDSleLiquidatedPackageJournalVoucherTypeId.Properties.NullText = .LiquidatedPackageJournalVoucherTypeDescription
                            ReversionLiquidatedPackageJournalVoucherTypeId = .ReversionLiquidatedPackageJournalVoucherTypeId
                            INDSleReversionLiquidatedPackageJournalVoucherTypeId.Properties.NullText = .ReversionLiquidatedPackageJournalVoucherTypeDescription
                            AccountingPackageMainAccountId = .AccountingPackageMainAccountId
                            INDSleAccountingPackageMainAccountId.Properties.NullText = .AccountingPackageMainAccountDescription
                            ProjectedVariationPriceMainAccountId = .ProjectedVariationPriceMainAccountId
                            INDSleProjectedVariationPriceMainAccountId.Properties.NullText = .ProjectedVariationPriceMainAccountDescription

                            CapitationProfitMainAccountId = .CapitationProfitMainAccountId
                            INDsleCapitationProfitMainAccountId.Properties.NullText = .CapitationProfitMainAccountDescription

                            CapitationLossMainAccountId = .CapitationLossMainAccountId
                            INDsleCapitationLossMainAccountId.Properties.NullText = .CapitationLossMainAccountDescription

                            ReversalPreviousYearsMainAccountId = .ReversalPreviousYearsMainAccountId
                            INDSleNullifyBillingOperationalCurrent.Properties.NullText = .ReversalPreviousYearsMainAccountIdDescription

                            ReversalPreviousYearsGenericBillingMainAccountId = .ReversalPreviousYearsGenericBillingMainAccountId
                            INDSleNullifyBillingNoOperationalCurrent.Properties.NullText = .ReversalPreviousYearsGenericBillingMainAccountIdDescription

                            PatientAdvanceCashReceiptConceptId = .PatientAdvanceCashReceiptConceptId
                            INDSlePatientAdvanceCashReceiptConceptId.Properties.NullText = .PatientAdvanceCashReceiptConceptDescription

                            CapitedPatientAdvanceCashReceiptConceptId = .CapitedPatientAdvanceCashReceiptConceptId
                            INDSleCapitedPatientAdvanceCashReceiptConceptId.Properties.NullText = .CapitedPatientAdvanceCashReceiptConceptDescription

                            RecoveryFeeDiscountMainAccountId = .RecoveryFeeDiscountMainAccountId
                            INDSleRecoveryFeeDiscountMainAccount.Properties.NullText = .RecoveryFeeDiscountMainAccountDescription

                            If .RecoveryFeeDiscountCostCenterId IsNot Nothing Then
                                RecoveryFeeDiscountCostCenterId = .RecoveryFeeDiscountCostCenterId
                                INDSleCostCenterRecoveryFeeDiscount.Properties.NullText = .RecoveryFeeDiscountCostCenterDescription
                                INDLciCostCenterRecoveryFeeDiscount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                            Else
                                INDLciCostCenterRecoveryFeeDiscount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                            End If

                            IndvidualAdvanceCashReceiptConceptId = .IndvidualAdvanceCashReceiptConceptId
                            INDSleIndvidualAdvanceCashReceiptConceptId.Properties.NullText = .IndvidualAdvanceCashReceiptConceptDescription

                            INDSleMainAccount.EditValue = .ProductSalesMainAccountId
                            INDSleMainAccount.Properties.NullText = .NumberNameProductSalesMainAccount

                            ConsignmentSalereCognition = .ConsignmentSalereCognition
                            ReversalRecognitionConsignmentSale = .ReversalRecognitionConsignmentSale
                            INDsleConsignmentSalereCognition.Properties.NullText = .ConsignmentSalereCognitionName
                            INDsleReversalRecognitionConsignmentSale.Properties.NullText = .ReversalRecognitionConsignmentSaleName

                            If .ProductSalesCostCenterId IsNot Nothing Then
                                INDLciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                                INDLciCostCenter.ShowInCustomizationForm = False
                                INDLciCostCenter.AllowHide = False
                                INDSleCostCenter.EditValue = .ProductSalesCostCenterId
                                INDSleCostCenter.Properties.NullText = .CodeNameCostCenter
                            Else
                                INDLciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                                INDLciCostCenter.ShowInCustomizationForm = True
                                INDLciCostCenter.AllowHide = True
                                INDSleCostCenter.EditValue = Nothing
                                INDSleCostCenter.Properties.NullText = String.Empty
                            End If

                            INDSleProductSalesCashReceiptConcept.EditValue = .ProductSalesCashReceiptConceptId
                            INDSleProductSalesCashReceiptConcept.Properties.NullText = .CodeNameProductSalesCashReceiptConcept
                            BasicBillingCashReceiptConceptId = .BasicBillingCashReceiptConceptId
                            INDsleBasicBillingCashReceiptConcept.Properties.NullText = .BasicBillingCashReceiptConceptCodeName
                            EntityCapitatedBillingAuthorizationId = .EntityCapitatedBillingAuthorizationId
                            INDsleEntityCapitatedAuthorization.Properties.NullText = .EntityCapitatedBillingAuthorizationDescription
                            INDGlePermissionCategories.EditValue = .PermissionCategories
                            INDGleIncomeLockType.EditValue = .IncomeLockType

                            ApplyElectronicSalesTicket = .ApplyElectronicSalesTicket
                            If ApplyElectronicSalesTicket Then

                                BillingAuthorizationId = .BillingAuthorizationId
                                INDsleBillingAuthorization.Properties.NullText = .BillingAuthorizationName

                                AccountingVoucherGenerationId = .AccountingVoucherGenerationId
                                INDSleAccountingVoucherGeneration.Properties.NullText = .AccountingVoucherGenerationName

                                AccountingVoucherReversalId = .AccountingVoucherReversalId
                                INDSleAccountingVoucherReversal.Properties.NullText = .AccountingVoucherReversalName
                            End If

                            AccountingForSurgical = .AccountingForSurgical
                            CalculateTaxAdvance = .CalculateTaxAdvance
                            Dim cantInvoice = Model.CountBillingInvoice()
                            If cantInvoice > 0 Then
                                INDtxtConsecutiveControlCapitation.Properties.ReadOnly = True
                            End If

                            INDTxtPrefixConsecutiveCapitation.EditValue = .PrefixConsecutiveCapitation

                            ConsecutiveControlCapitation = .ConsecutiveControlCapitation

                            RequiresPermissionForCxCPatient = .RequiresPermissionForCxCPatient

                            ApplyBasicBilling = .ApplyBasicBilling
                            LiquidateSinceControlOutPatientService = .LiquidateSinceControlOutPatientService
                            ValidateAgeOfMajority = .ValidateAgeOfMajority
                            GeneratePromissoryNote = .GeneratePromissoryNote
                            DistributeCapitationControls = .DistributeCapitationControls
                            AnulateInvoicesPreviousPeriods = .AnulateInvoicesPreviousPeriods
                            BudgetInterface = .BudgetInterface
                            AuthorizationNumberControl = .AuthorizationNumberControl
                            ValidatePackaging = .ValidatePackaging
                            AccountingPackage = .AccountingPackage

                            ClientMainAccountId = .ClientMainAccountId
                            INDsleClientMainAccount.Properties.NullText = .ClientMainAccountDescription

                            IVAPaymentMainAccountId = .IVAPaymentMainAccountId
                            INDsleIVAPaymentMainAccount.Properties.NullText = .IVAPaymentMainAccountDescription

                            ReteIVAConceptId = .ReteIVAConceptId
                            INDsleReteIVAConcept.Properties.NullText = .ReteIVAConceptDescription

                            ReteIVAMainAccountId = .ReteIVAMainAccountId
                            INDsleReteIVAMainAccount.Properties.NullText = .ReteIVAMainAccountDescription

                            ReteICAMainAccountId = .ReteICAMainAccountId
                            INDsleReteICAMainAccount.Properties.NullText = .ReteICAMainAccountDescription

                            ReteFuenteMainAccountId = .ReteFuenteMainAccountId
                            INDsleReteFuenteMainAccount.Properties.NullText = .ReteFuenteMainAccountDescription

                            InvoiceProductDevolutionPartialConceptNoteId = .InvoiceProductDevolutionPartialConceptNoteId
                            INDsleInvoiceProductDevolutionPartialConceptNote.Properties.NullText = .InvoiceProductDevolutionPartialConceptNoteDescription

                            GiftProductOutletConcept = .GiftProductOutletConcept
                            INDsleGiftProductOutletConcept.Properties.NullText = .GiftProductOutletConceptDescription

                            AssociateCostCenter = .AssociateCostCenter
                            AccountControlValidation = .AccountControlValidation

                            LiquidateMasterAccount = .LiquidateMasterAccount
                            HasCustomTRM = .HasCustomTRM

                            HandlesDifferentRates = .HandlesDifferentRates
                            LiquidateFolioInSpecificCurrency = .LiquidateFolioInSpecificCurrency

                            requiresConditionsSale = .requiresConditionsSale

                            SpecificCurrencyId = .SpecificCurrencyId
                            INDSleSpecificCurrency.Properties.NullText = .CodeNameSpecificCurrency

                            AllowsSalesExecutiveAndSupplier = .AllowsSalesExecutiveAndSupplier
                            AccountsConditionalCommercialDiscount = .AccountsConditionalCommercialDiscount

                            ValidateIntegrationMipres = .IntegrationMiPres
                            ClientNameId = .ClientIdName
                            ClientSecret = .ClientSecret
                            Me.MaxInvoiceItems = .MaxInvoiceItems

                            'Si el nit de la compañía es FarmaQx o Jersalud se muestra el grupo de dispensación por paciente
                            If indigo.IndigoCompanyNit = "900433437" OrElse indigo.IndigoCompanyNit = "900622551" Then
                                INDlygDispensingByPatient.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                                INDlyItemFunctionalUnit.AllowHide = False
                                INDlyItemHealthProfessional.AllowHide = False

                                FunctionalUnitId = .FunctionalUnitId
                                'INDsleFunctionalUnit.Properties.NullText = .FunctionalUnitDescription
                                HealthProfessionalCode = .HealthProfessionalCode
                                INDsleHealthProfessional.Properties.NullText = .HealthProfessionalCode
                            Else
                                INDlygDispensingByPatient.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                                INDlyItemFunctionalUnit.AllowHide = True
                                INDlyItemHealthProfessional.AllowHide = True

                                FunctionalUnitId = Nothing
                                INDsleFunctionalUnit.Properties.NullText = String.Empty
                                HealthProfessionalCode = Nothing
                                INDsleHealthProfessional.Properties.NullText = String.Empty
                            End If

                            If BudgetInterface Then
                                BudgetaryEntityId = .BudgetaryEntityId
                                INDSleBudgetaryEntityId.Properties.NullText = .BudgetaryEntityDescription
                                BudgetaryValidityId = .BudgetaryValidityId
                                INDSleBudgetaryValidityId.Properties.NullText = .BudgetaryValidityDescription
                                DependencyId = .DependencyId
                                INDSleDependencyId.Properties.NullText = .DependencyDescription
                                BasicBillingDependencyId = .BasicBillingDependencyId
                                INDSleBasicBillingDependencyId.Properties.NullText = .BasicBillingDependencyDescription
                                BasicBillingBudgetId = .BasicBillingBudgetId
                                INDSleBasicBillingBudgetId.Properties.NullText = .BasicBillingBudgetDescription
                            End If
                            INDSleStatusFolioNew.EditValue = .StatusFolioNewId
                            INDSleStatusFolioNew.Properties.NullText = .StatusFolioNewDescription
                            INDSleStatusFolioClosed.EditValue = .StatusFolioClosedId
                            INDSleStatusFolioClosed.Properties.NullText = .StatusFolioClosedDescription

                        End With
                        Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.settingBilling.Id)
                        If result.Id = 0 Then
                            Dim state = New Domain.Base.Entities.ObjectChangeTracker
                            state.State = Domain.Base.Entities.ObjectState.Added
                            _record = New BlockRecordBilling With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = CInt(Me.Tag), .CodUser = Me.indigo.UserIndigo, .IdRecord = settingBilling.Id}
                            Dim operation = Await ModelRecord.SaveBlockRecord(_record)
                            _record = operation.ObjectEmbbeded
                        Else
                            Dim xtraMessage As String = String.Format(ResourceManager.GetString("RecordLocked"), result.CodUser, result.NameUser, result.BlockDate)
                            _record = result
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                        End If
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
                        Me.BarraBotones.SetDocuments(settingBilling.Id)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
                    End Using
                    _loadingControls = False
                Else
                    settingBilling = New SettingsBilling
                    BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
                End If
            Catch ex As Exception
                Throw ex
            Finally
                AsyncLoader(False)
            End Try
        End Using
        INDsleInvoiceJournalVoucherTypeId.Focus()
    End Function

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With settingBilling
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .IdOperatingUnit = _idOperativeUnit
            .ParticularHealthAdministratorId = ParticularHealthAdministratorId
            .InvoiceJournalVoucherTypeId = InvoiceJournalVoucherTypeId
            .ProductInvoiceJournalVoucherTypeId = ProductInvoiceJournalVoucherTypeId
            .BasicBillingJournalVoucherTypeId = BasicBillingJournalVoucherTypeId
            .InvoiceEntityCapitatedDistributionJournalVoucherTypeId = InvoiceEntityCapitatedDistributionJournalVoucherTypeId
            .ReverseInvoiceEntityCapitatedDistributionJournalVoucherTypeId = ReverseInvoiceEntityCapitatedDistributionJournalVoucherTypeId
            .InvoiceAnnulmentJournalVoucherTypeId = InvoiceAnnulmentJournalVoucherTypeId
            .BasicBillingAnnulmentJournalVoucherTypeId = BasicBillingAnnulmentJournalVoucherTypeId
            .CashReceiptsConfirm = CashReceiptsConfirm
            .RoundingTypeRecoveryFeeType = RoundingTypeRecoveryFeeType
            .CapitationRevenueMainAccountId = CapitationRevenueMainAccountId
            .CapitationProfitMainAccountId = CapitationProfitMainAccountId
            .CapitationLossMainAccountId = CapitationLossMainAccountId
            .ReversalPreviousYearsMainAccountId = ReversalPreviousYearsMainAccountId
            .ReversalPreviousYearsGenericBillingMainAccountId = ReversalPreviousYearsGenericBillingMainAccountId
            .PatientAdvanceCashReceiptConceptId = PatientAdvanceCashReceiptConceptId
            .CapitedPatientAdvanceCashReceiptConceptId = CapitedPatientAdvanceCashReceiptConceptId
            .IndvidualAdvanceCashReceiptConceptId = IndvidualAdvanceCashReceiptConceptId
            .ProductSalesCashReceiptConceptId = INDSleProductSalesCashReceiptConcept.EditValue
            .BasicBillingCashReceiptConceptId = BasicBillingCashReceiptConceptId
            .ProductSalesMainAccountId = INDSleMainAccount.EditValue
            .ProductSalesCostCenterId = INDSleCostCenter.EditValue
            .ReverseTransferJournalVoucherTypeId = ReverseTransferJournalVoucherTypeId
            .ReverseRecognitionJournalVoucherTypeId = ReverseRecognitionJournalVoucherTypeId
            .RecognitionJournalVoucherTypeId = RecognitionJournalVoucherTypeId
            .AccountingForSurgical = AccountingForSurgical
            .CalculateTaxAdvance = CalculateTaxAdvance
            .RecoveryFeeDiscountMainAccountId = RecoveryFeeDiscountMainAccountId
            .RecoveryFeeDiscountCostCenterId = RecoveryFeeDiscountCostCenterId
            .ApplyBasicBilling = ApplyBasicBilling
            .ValidateAgeOfMajority = ValidateAgeOfMajority
            .GeneratePromissoryNote = GeneratePromissoryNote
            .LiquidateSinceControlOutPatientService = LiquidateSinceControlOutPatientService
            .DistributeCapitationControls = DistributeCapitationControls
            .AnulateInvoicesPreviousPeriods = AnulateInvoicesPreviousPeriods
            .BudgetInterface = BudgetInterface
            .AuthorizationNumberControl = AuthorizationNumberControl
            .ValidatePackaging = ValidatePackaging
            .AccountingPackage = AccountingPackage
            .LiquidatedPackageJournalVoucherTypeId = LiquidatedPackageJournalVoucherTypeId
            .ReversionLiquidatedPackageJournalVoucherTypeId = ReversionLiquidatedPackageJournalVoucherTypeId
            .AccountingPackageMainAccountId = AccountingPackageMainAccountId
            .ProjectedVariationPriceMainAccountId = ProjectedVariationPriceMainAccountId
            .ReteIVAConceptId = ReteIVAConceptId
            .ReteIVAMainAccountId = ReteIVAMainAccountId
            .IntegrationMiPres = ValidateIntegrationMipres
            .ClientIdName = ClientNameId
            .ClientSecret = ClientSecret
            .ConsignmentSalereCognition = ConsignmentSalereCognition
            .ReversalRecognitionConsignmentSale = ReversalRecognitionConsignmentSale
            .GiftProductOutletConcept = GiftProductOutletConcept
            .HasCustomTRM = HasCustomTRM
            .LiquidateFolioInSpecificCurrency = LiquidateFolioInSpecificCurrency

            If INDLyItemSpecificCurrency.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .SpecificCurrencyId = SpecificCurrencyId
            Else
                .SpecificCurrencyId = Nothing
            End If

            .ApplyElectronicSalesTicket = ApplyElectronicSalesTicket
            If INDlygElectronicSalesTicket.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .BillingAuthorizationId = BillingAuthorizationId
                .AccountingVoucherGenerationId = AccountingVoucherGenerationId
                .AccountingVoucherReversalId = AccountingVoucherReversalId
            Else
                .BillingAuthorizationId = Nothing
                .AccountingVoucherGenerationId = Nothing
                .AccountingVoucherReversalId = Nothing
            End If

            .requiresConditionsSale = requiresConditionsSale
            .MaxInvoiceItems = Me.MaxInvoiceItems
            .BillingAuthorizationCopayId = INDSleBillingAuthorizationCopay.EditValue
            If ApplyBasicBilling Then
                .ClientMainAccountId = ClientMainAccountId
                .IVAPaymentMainAccountId = IVAPaymentMainAccountId
                .ReteICAMainAccountId = ReteICAMainAccountId
                .ReteFuenteMainAccountId = ReteFuenteMainAccountId
                .ConsignmentSalereCognition = ConsignmentSalereCognition
                .ReversalRecognitionConsignmentSale = ReversalRecognitionConsignmentSale
                .HandlesDifferentRates = HandlesDifferentRates
                .AllowsSalesExecutiveAndSupplier = AllowsSalesExecutiveAndSupplier
                .AccountsConditionalCommercialDiscount = AccountsConditionalCommercialDiscount
            Else
                .ClientMainAccountId = Nothing
                .IVAPaymentMainAccountId = Nothing
                .ReteICAMainAccountId = Nothing
                .ReteFuenteMainAccountId = Nothing
                .ConsignmentSalereCognition = Nothing
                .ReversalRecognitionConsignmentSale = Nothing
                .HandlesDifferentRates = False
            End If
            If INDtxtConsecutiveControlCapitation.Properties.ReadOnly = False Then
                .ConsecutiveControlCapitation = ConsecutiveControlCapitation
            End If
            .InvoiceProductDevolutionPartialConceptNoteId = InvoiceProductDevolutionPartialConceptNoteId
            .PrefixConsecutiveCapitation = INDTxtPrefixConsecutiveCapitation.EditValue
            .RequiresPermissionForCxCPatient = RequiresPermissionForCxCPatient
            .EntityCapitatedBillingAuthorizationId = EntityCapitatedBillingAuthorizationId
            .PermissionCategories = INDGlePermissionCategories.EditValue
            .IncomeLockType = INDGleIncomeLockType.EditValue
            .AssociateCostCenter = AssociateCostCenter
            .AccountControlValidation = AccountControlValidation
            .LiquidateMasterAccount = LiquidateMasterAccount

            If INDlygDispensingByPatient.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .FunctionalUnitId = FunctionalUnitId
                .HealthProfessionalCode = HealthProfessionalCode
            Else
                .FunctionalUnitId = Nothing
                .HealthProfessionalCode = Nothing
            End If

            If BudgetInterface Then
                .DependencyId = DependencyId
                .BasicBillingDependencyId = BasicBillingDependencyId
                .BasicBillingBudgetId = BasicBillingBudgetId
            End If

            If INDSleStatusFolioNew.EditValue IsNot Nothing AndAlso INDSleStatusFolioNew.EditValue > 0 Then
                .StatusFolioNewId = INDSleStatusFolioNew.EditValue
            End If
            If INDSleStatusFolioClosed.EditValue IsNot Nothing AndAlso INDSleStatusFolioClosed.EditValue > 0 Then
                .StatusFolioClosedId = INDSleStatusFolioClosed.EditValue
            End If
            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    Private Sub CleanBudgetInterface(level As Integer)
        If level < 1 Then
            BudgetaryEntityId = Nothing
            INDSleBudgetaryEntityId.Properties.NullText = String.Empty
        End If
        If level < 2 Then
            BudgetaryValidityId = Nothing
            INDSleBudgetaryValidityId.Properties.NullText = String.Empty
            BudgetaryValidityXpo = Nothing
        End If
        If level < 3 Then
            DependencyId = Nothing
            INDSleDependencyId.Properties.NullText = String.Empty
            DependencyXpo = Nothing

            BasicBillingDependencyId = Nothing
            INDSleBasicBillingDependencyId.Properties.NullText = String.Empty
            BasicBillingBudgetXpo = Nothing

            BasicBillingBudgetId = Nothing
            INDSleBasicBillingBudgetId.Properties.NullText = String.Empty
            BasicBillingBudgetXpo = Nothing
        End If
    End Sub

#End Region

#Region "Bar Buttons"

    ''' <summary>
    ''' Barra botones: cambia la unidad operativa
    ''' </summary>
    ''' <param name="operatingUnit"></param>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            BarraBotones.StatusRecordVisible = False
            DeleteBlockedRecord()
            CleanControls()
            Await LoadControls()
            If settingBilling IsNot Nothing AndAlso settingBilling.Id > 0 Then
                settingBilling.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified
            End If
        End If
    End Sub

    ''' <summary>
    ''' Barra botones: Guardar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barra botones: Actualizar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Load de la barra de botones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

    ''' <summary>
    ''' Abre el popup pagar visualizar o agregar trm especificos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub PopupContainerEdit1_Click(sender As Object, e As EventArgs) Handles PopupContainerEdit1.Click
        Using Formulario As New FrmSettingBillingCurrency
            Dim size As System.Drawing.Size
            size.Width = 900
            size.Height = 800
            Formulario.Size = size
            Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent As New FrmTransparent(Formulario, False)
            transparent.ShowDialog(Me)
        End Using
    End Sub




#End Region

End Class