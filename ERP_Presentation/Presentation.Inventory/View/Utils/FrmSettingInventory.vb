'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Faiber Julian Mora Dussan
' Created          : 30-01-2015
'
' Last Modified By : Carlos Mario Arias Rubiano
' Last Modified On : 23/02/2015
' Description      : Terminar el frontal
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ComponentModel
Imports System.Text
Imports System.Windows
Imports System.Windows.Forms
Imports DevExpress.Xpo
Imports DevExpress.XtraEditors.Controls
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.PaymentsRepository
Imports Presentation.Accounting
Imports Presentation.Base
Imports Presentation.Common
Imports Presentation.Controls
Imports Presentation.Controls.MVP
Imports Presentation.Inventory.MVP
Imports Presentation.Payments
Imports Presentation.Payroll

#End Region

Public Class FrmSettingInventory
    Implements ISettingInventory

#Region "Properties"

    ''' <summary>
    ''' Establece si es de tipo imagenologia
    ''' </summary>
    ''' <returns></returns>

    Public Property TaxRegistration As Byte Implements ISettingInventory.TaxRegistration
        Get
            Return CByte(INDsleTaxRegistration.EditValue)
        End Get
        Set(value As Byte)
            INDsleTaxRegistration.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' obtiene o establece Dispensación de paquetes qx
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PackageDispensingMethod As Boolean? Implements ISettingInventory.PackageDispensingMethod
        Get
            Return INDslePackageDispensingMethod.EditValue
        End Get
        Set(value As Boolean?)
            INDslePackageDispensingMethod.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Representa a la entidad de parámetros de cxp
    ''' </summary>
    Public PaymentsSettingPaymentsXpo As PaymentsSettingPaymentsXpo

    ''' <summary>
    ''' Devolución parcial de venta
    ''' </summary>
    ''' <returns></returns>
    Public Property PartialReturnSalesJournalVoucherTypeXpo As XPInstantFeedbackSource Implements ISettingInventory.PartialReturnSalesJournalVoucherTypeXpo
        Get
            Return INDslePartialReturnSalesJournalVoucherType.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDslePartialReturnSalesJournalVoucherType.Properties.DataSource = value
        End Set
    End Property

    Public Property IVADatasource As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingInventory.IVADatasource
        Get
            Return CType(INDsleIva.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleIva.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece los terceros
    ''' </summary>
    ''' <value>
    ''' The third party xpo.
    ''' </value>
    Property ThirdPartyTransfersXPO As DevExpress.Xpo.XPInstantFeedbackSource
        Get
            Return CType(INDSleThirdPartyTransfers.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleThirdPartyTransfers.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece los terceros
    ''' </summary>
    ''' <value>
    ''' The third party xpo.
    ''' </value>
    Property ThirdPartyDispensingXPO As DevExpress.Xpo.XPInstantFeedbackSource
        Get
            Return CType(INDSleThirdPartyDispensing.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleThirdPartyDispensing.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta contable del costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CostAccountId As Integer? Implements ISettingInventory.CostAccountId
        Get
            Return INDsleCostAccount.EditValue
        End Get
        Set(value As Integer?)
            INDsleCostAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la cuenta contable del costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CostAccountIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingInventory.CostAccountIdXpo
        Get
            Return INDsleCostAccount.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleCostAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la unidad funcional
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FunctionalUnitId As Integer? Implements ISettingInventory.FunctionalUnitId
        Get
            Return INDsleFunctionalUnit.EditValue
        End Get
        Set(value As Integer?)
            INDsleFunctionalUnit.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la unidad funcional
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FunctionalUnitIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingInventory.FunctionalUnitIdXpo
        Get
            Return INDsleFunctionalUnit.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleFunctionalUnit.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta contable de ventas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SalesAccountId As Integer? Implements ISettingInventory.SalesAccountId
        Get
            Return INDsleSalesAccount.EditValue
        End Get
        Set(value As Integer?)
            INDsleSalesAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la cuenta contable de ventas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SalesAccountIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingInventory.SalesAccountIdXpo
        Get
            Return INDsleSalesAccount.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleSalesAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del concepto de pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AdjustmentAccountPayableConceptId As Integer? Implements ISettingInventory.AdjustmentAccountPayableConceptId
        Get
            Return INDsleAdjustmentAccountPayableConceptId.EditValue
        End Get
        Set(value As Integer?)
            INDsleAdjustmentAccountPayableConceptId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del concepto de pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AdjustmentAccountPayableConceptIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingInventory.AdjustmentAccountPayableConceptIdXpo
        Get
            Return INDsleAdjustmentAccountPayableConceptId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleAdjustmentAccountPayableConceptId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece de donde saca el centro de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AssociateCostCenter As Integer? Implements ISettingInventory.AssociateCostCenter
        Get
            Return INDsleAssociateCostCenter.EditValue
        End Get
        Set(value As Integer?)
            INDsleAssociateCostCenter.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece de donde saca la cuenta contable de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AssociateCostMainAccount As Integer? Implements ISettingInventory.AssociateCostMainAccount
        Get
            Return INDsleAssociateCostMainAccount.EditValue
        End Get
        Set(value As Integer?)
            INDsleAssociateCostMainAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DiscountSalesMainAccountId As Integer? Implements ISettingInventory.DiscountSalesMainAccountId
        Get
            Return INDsleDiscountSalesMainAccountId.EditValue
        End Get
        Set(value As Integer?)
            INDsleDiscountSalesMainAccountId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DiscountSalesMainAccountIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingInventory.DiscountSalesMainAccountIdXpo
        Get
            Return INDsleDiscountSalesMainAccountId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleDiscountSalesMainAccountId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del concepto de la nota de pago que se va usar cuando se haga una devolucion de comprobante de entrada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RefundAccountPayableConceptNoteId As Integer? Implements ISettingInventory.RefundAccountPayableConceptNoteId
        Get
            Return INDsleRefundAccountPayableConceptNoteId.EditValue
        End Get
        Set(value As Integer?)
            INDsleRefundAccountPayableConceptNoteId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RefundAccountPayableConceptNoteIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingInventory.RefundAccountPayableConceptNoteXpo
        Get
            Return INDsleRefundAccountPayableConceptNoteId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleRefundAccountPayableConceptNoteId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la unidad de radicacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FilingUnitId As Integer? Implements ISettingInventory.FilingUnitId
        Get
            Return INDsleFilingUnitId.EditValue
        End Get
        Set(value As Integer?)
            INDsleFilingUnitId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' establece el datasource de la unidad de radicacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FilingUnitIdXpo As DevExpress.Xpo.XPCollection Implements ISettingInventory.FilingUnitIdXpo
        Get
            Return INDsleFilingUnitId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPCollection)
            INDsleFilingUnitId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del concepto de pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FreightAccountPayableConceptId As Integer? Implements ISettingInventory.FreightAccountPayableConceptId
        Get
            Return INDsleFreightAccountPayableConceptId.EditValue
        End Get
        Set(value As Integer?)
            INDsleFreightAccountPayableConceptId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del concepto de pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FreightAccountPayableConceptIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingInventory.FreightAccountPayableConceptIdXpo
        Get
            Return INDsleFreightAccountPayableConceptId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleFreightAccountPayableConceptId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del concepto de pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IVAAccountPayableConceptId As Integer? Implements ISettingInventory.IVAAccountPayableConceptId
        Get
            Return INDsleIVAAccountPayableConceptId.EditValue
        End Get
        Set(value As Integer?)
            INDsleIVAAccountPayableConceptId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del concepto de pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IVAAccountPayableConceptIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingInventory.IVAAccountPayableConceptIdXpo
        Get
            Return INDsleIVAAccountPayableConceptId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleIVAAccountPayableConceptId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene permiso de bar code
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Nomanualbarcodepermission As Boolean? Implements ISettingInventory.Nomanualbarcodepermission
        Get
            Return INDSleManualbarCodePermission.EditValue
        End Get
        Set(value As Boolean?)
            INDSleManualbarCodePermission.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del concepto de pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IVAFreightAccountPayableConceptId As Integer? Implements ISettingInventory.IVAFreightAccountPayableConceptId
        Get
            Return INDsleIVAFreightAccountPayableConceptId.EditValue
        End Get
        Set(value As Integer?)
            INDsleIVAFreightAccountPayableConceptId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del concepto de pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IVAFreightAccountPayableConceptIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingInventory.IVAFreightAccountPayableConceptIdXpo
        Get
            Return INDsleIVAFreightAccountPayableConceptId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleIVAFreightAccountPayableConceptId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IVAGeneratedMainAccountId As Integer? Implements ISettingInventory.IVAGeneratedMainAccountId
        Get
            Return INDsleIVAGeneratedMainAccountId.EditValue
        End Get
        Set(value As Integer?)
            INDsleIVAGeneratedMainAccountId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IVAGeneratedMainAccountIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingInventory.IVAGeneratedMainAccountIdXpo
        Get
            Return INDsleIVAGeneratedMainAccountId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleIVAGeneratedMainAccountId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la retencion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IVARetention As Integer? Implements ISettingInventory.IVARetention
        Get
            Return INDsleIVARetention.EditValue
        End Get
        Set(value As Integer?)
            INDsleIVARetention.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el centro de costo para Insumos de Farmacia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PharmacySuppliesCostCenter As Integer Implements ISettingInventory.PharmacySuppliesCostCenter
        Get
            Return INDGlePharmacySuppliesCostCenter.EditValue
        End Get
        Set(value As Integer)
            INDGlePharmacySuppliesCostCenter.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del concepto de pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IVARetentionAccountPayableConceptId As Integer? Implements ISettingInventory.IVARetentionAccountPayableConceptId
        Get
            Return INDsleIVARetentionAccountPayableConceptId.EditValue
        End Get
        Set(value As Integer?)
            INDsleIVARetentionAccountPayableConceptId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la retencion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IVARetentionAccountPayableConceptIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingInventory.IVARetentionAccountPayableConceptIdXpo
        Get
            Return INDsleIVARetentionAccountPayableConceptId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleIVARetentionAccountPayableConceptId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del tipo de comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LoanJournalVoucherTypeId As Integer? Implements ISettingInventory.LoanJournalVoucherTypeId
        Get
            Return INDsleLoanJournalVoucherTypeId.EditValue
        End Get
        Set(value As Integer?)
            INDsleLoanJournalVoucherTypeId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' establece el datasource del tipo de comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LoanJournalVoucherTypeIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingInventory.LoanJournalVoucherTypeIdXpo
        Get
            Return INDsleLoanJournalVoucherTypeId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleLoanJournalVoucherTypeId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el mes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Month As Integer Implements ISettingInventory.Month
        Get
            Return INDctrDate.GetMonth
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el tag
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As String Implements ISettingInventory.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del tipo de comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ConsignmentMerchandiseJournalVoucherTypeId As Integer? Implements ISettingInventory.ConsignmentMerchandiseJournalVoucherTypeId
        Get
            Return INDsleConsignmentMerchandiseJournalVoucherTypeId.EditValue
        End Get
        Set(value As Integer?)
            INDsleConsignmentMerchandiseJournalVoucherTypeId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del tipo de comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ConsignmentMerchandiseJournalVoucherTypeIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingInventory.ConsignmentMerchandiseJournalVoucherTypeIdXpo
        Get
            Return INDsleConsignmentMerchandiseJournalVoucherTypeId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleConsignmentMerchandiseJournalVoucherTypeId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del tipo de comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ConsignmentMerchandiseReturnJournalVoucherTypeId As Integer? Implements ISettingInventory.ConsignmentMerchandiseReturnJournalVoucherTypeId
        Get
            Return INDsleConsignmentMerchandiseReturnJournalVoucherTypeId.EditValue
        End Get
        Set(value As Integer?)
            INDsleConsignmentMerchandiseReturnJournalVoucherTypeId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del tipo de comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ConsignmentMerchandiseReturnJournalVoucherTypeIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingInventory.ConsignmentMerchandiseReturnJournalVoucherTypeIdXpo
        Get
            Return INDsleConsignmentMerchandiseReturnJournalVoucherTypeId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleConsignmentMerchandiseReturnJournalVoucherTypeId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del tipo de comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ConsignmentInventoryUseJournalVoucherTypeId As Integer? Implements ISettingInventory.ConsignmentInventoryUseJournalVoucherTypeId
        Get
            Return INDsleConsignmentInventoryUseJournalVoucherTypeId.EditValue
        End Get
        Set(value As Integer?)
            INDsleConsignmentInventoryUseJournalVoucherTypeId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del tipo de comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ConsignmentInventoryUseJournalVoucherTypeIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingInventory.ConsignmentInventoryUseJournalVoucherTypeIdXpo
        Get
            Return INDsleConsignmentInventoryUseJournalVoucherTypeId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleConsignmentInventoryUseJournalVoucherTypeId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del tipo de comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ConsignmentInventoryUseDevolutionJournalVoucherTypeId As Integer? Implements ISettingInventory.ConsignmentInventoryUseDevolutionJournalVoucherTypeId
        Get
            Return INDsleConsignmentInventoryUseDevolutionJournalVoucherTypeId.EditValue
        End Get
        Set(value As Integer?)
            INDsleConsignmentInventoryUseDevolutionJournalVoucherTypeId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del tipo de comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ConsignmentInventoryUseDevolutionJournalVoucherTypeIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingInventory.ConsignmentInventoryUseDevolutionJournalVoucherTypeIdXpo
        Get
            Return INDsleConsignmentInventoryUseDevolutionJournalVoucherTypeId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleConsignmentInventoryUseDevolutionJournalVoucherTypeId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del tipo de comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property OrderDispatchReturnJournalVoucherTypeId As Integer? Implements ISettingInventory.OrderDispatchReturnJournalVoucherTypeId
        Get
            Return INDsleOrderDispatchReturnJournalVoucherTypeId.EditValue
        End Get
        Set(value As Integer?)
            INDsleOrderDispatchReturnJournalVoucherTypeId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del tipo de comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property OrderDispatchReturnJournalVoucherTypeIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingInventory.OrderDispatchReturnJournalVoucherTypeIdXpo
        Get
            Return INDsleOrderDispatchReturnJournalVoucherTypeId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleOrderDispatchReturnJournalVoucherTypeId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Especifica el id del tipo del comprobante que se debe generar al crear una devolucion prestamo de mercancia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LoanReturnJournalVoucherTypeId As Integer Implements ISettingInventory.LoanReturnJournalVoucherTypeId
        Get
            Return INDsleLoanReturnJournalVoucherTypeId.EditValue
        End Get
        Set(value As Integer)
            INDsleLoanReturnJournalVoucherTypeId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el listado de tipos de comprobantes que se deben generar al crear una devolucion prestamo de mercancia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LoanReturnJournalVoucherTypeIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingInventory.LoanReturnJournalVoucherTypeIdXpo
        Get
            Return INDsleLoanReturnJournalVoucherTypeId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleLoanReturnJournalVoucherTypeId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Id del tipo del comprobante contable que se genera al hacer una orden de Traslado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property OrderDispatchJournalVoucherTypeId As Integer Implements ISettingInventory.OrderDispatchJournalVoucherTypeId
        Get
            Return INDsleOrderDispatchJournalVoucherTypeId.EditValue
        End Get
        Set(value As Integer)
            INDsleOrderDispatchJournalVoucherTypeId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Id del tipo del comprobante contable para Traslado entre almacenes en consignación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TransferBetweenWarehousesConsignmentId As Integer? Implements ISettingInventory.TransferBetweenWarehousesConsignmentId
        Get
            Return INDsleTransferBetweenWarehousesConsignment.EditValue
        End Get
        Set(value As Integer?)
            INDsleTransferBetweenWarehousesConsignment.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Id del tipo del comprobante contable para Valorización por lista de precios-consignación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ValuationConsignmentPriceJournalVoucherTypesId As Integer? Implements ISettingInventory.ValuationConsignmentPriceJournalVoucherTypesId
        Get
            Return INDsleValuationConsignmentPriceJournalVoucherTypesId.EditValue
        End Get
        Set(value As Integer?)
            INDsleValuationConsignmentPriceJournalVoucherTypesId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Traslado entre almacenes en consignación
    ''' </summary>
    ''' <returns></returns>
    Public Property TransferBetweenWarehousesConsignmentXpo As XPInstantFeedbackSource Implements ISettingInventory.TransferBetweenWarehousesConsignmentXpo
        Get
            Return INDsleTransferBetweenWarehousesConsignment.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleTransferBetweenWarehousesConsignment.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Valorización por lista de precios-consignación
    ''' </summary>
    ''' <returns></returns>
    Public Property ValuationConsignmentPriceJournalVoucherTypesXpo As XPInstantFeedbackSource Implements ISettingInventory.ValuationConsignmentPriceJournalVoucherTypesIdXpo
        Get
            Return INDsleValuationConsignmentPriceJournalVoucherTypesId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleValuationConsignmentPriceJournalVoucherTypesId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el listado de comprobantes contables que se generan al hacer una orden de Traslado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property OrderDispatchJournalVoucherTypeIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingInventory.OrderDispatchJournalVoucherTypeIdXpo
        Get
            Return INDsleOrderDispatchJournalVoucherTypeId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleOrderDispatchJournalVoucherTypeId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Especifica el tipo de comprobante contable que se va a crear cuando se realice un ajuste de inventario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InventoryAdjustmentJournalVoucherTypeId As Integer Implements ISettingInventory.InventoryAdjustmentJournalVoucherTypeId
        Get
            Return INDsleInventoryAdjustmentJournalVoucherTypeId.EditValue
        End Get
        Set(value As Integer)
            INDsleInventoryAdjustmentJournalVoucherTypeId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el listado de los tipos de comprobantes para un ajuste de inventario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InventoryAdjustmentJournalVoucherTypeXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingInventory.InventoryAdjustmentJournalVoucherTypeXpo
        Get
            Return INDsleInventoryAdjustmentJournalVoucherTypeId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleInventoryAdjustmentJournalVoucherTypeId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Especifica el tipo de comprobante contable que se va a crear cuando se realice un ajuste en el cierre mensual de inventario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InventoryCloseAdjustmentJournalVoucherTypeId As Integer Implements ISettingInventory.InventoryCloseAdjustmentJournalVoucherTypeId
        Get
            Return INDsleInventoryCloseAdjustmentJournalVoucherTypeId.EditValue
        End Get
        Set(value As Integer)
            INDsleInventoryCloseAdjustmentJournalVoucherTypeId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el listado de los tipos de comprobantes para un ajuste en el cierre mensual de inventario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InventoryCloseAdjustmentJournalVoucherTypeXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingInventory.InventoryCloseAdjustmentJournalVoucherTypeXpo
        Get
            Return INDsleInventoryCloseAdjustmentJournalVoucherTypeId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleInventoryCloseAdjustmentJournalVoucherTypeId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el concepto de pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ProCultureAccountPayableConceptId As Integer? Implements ISettingInventory.ProCultureAccountPayableConceptId
        Get
            Return INDsleProCultureAccountPayableConceptId.EditValue
        End Get
        Set(value As Integer?)
            INDsleProCultureAccountPayableConceptId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' establece el datasource del concepto de pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ProCultureAccountPayableConceptIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingInventory.ProCultureAccountPayableConceptIdXpo
        Get
            Return INDsleProCultureAccountPayableConceptId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleProCultureAccountPayableConceptId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del concepto de pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ProDevelopmentAccountPayableConceptId As Integer? Implements ISettingInventory.ProDevelopmentAccountPayableConceptId
        Get
            Return INDsleProDevelopmentAccountPayableConceptId.EditValue
        End Get
        Set(value As Integer?)
            INDsleProDevelopmentAccountPayableConceptId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del concepto de pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ProDevelopmentAccountPayableConceptIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingInventory.ProDevelopmentAccountPayableConceptIdXpo
        Get
            Return INDsleProDevelopmentAccountPayableConceptId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleProDevelopmentAccountPayableConceptId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del concepto de pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ProElectrificationAccountPayableConceptId As Integer? Implements ISettingInventory.ProElectrificationAccountPayableConceptId
        Get
            Return INDsleProElectrificationAccountPayableConceptId.EditValue
        End Get
        Set(value As Integer?)
            INDsleProElectrificationAccountPayableConceptId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del concepto de pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ProElectrificationAccountPayableConceptIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingInventory.ProElectrificationAccountPayableConceptIdXpo
        Get
            Return INDsleProElectrificationAccountPayableConceptId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleProElectrificationAccountPayableConceptId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del concepto de pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ProGameAccountPayableConceptId1 As Integer? Implements ISettingInventory.ProGameAccountPayableConceptId1
        Get
            Return INDsleProGameAccountPayableConceptId1.EditValue
        End Get
        Set(value As Integer?)
            INDsleProGameAccountPayableConceptId1.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del concepto de pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ProGameAccountPayableConceptId1Xpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingInventory.ProGameAccountPayableConceptId1Xpo
        Get
            Return INDsleProGameAccountPayableConceptId1.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleProGameAccountPayableConceptId1.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del concepto de pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ProHospitalAccountPayableConceptId As Integer? Implements ISettingInventory.ProHospitalAccountPayableConceptId
        Get
            Return INDsleProHospitalAccountPayableConceptId.EditValue
        End Get
        Set(value As Integer?)
            INDsleProHospitalAccountPayableConceptId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del concepto de pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ProHospitalAccountPayableConceptIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingInventory.ProHospitalAccountPayableConceptIdXpo
        Get
            Return INDsleProHospitalAccountPayableConceptId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleProHospitalAccountPayableConceptId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del tipo de comprobante contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PurchaseJournalVoucherTypeId As Integer? Implements ISettingInventory.PurchaseJournalVoucherTypeId
        Get
            Return INDslePurchaseJournalVoucherTypeId.EditValue
        End Get
        Set(value As Integer?)
            INDslePurchaseJournalVoucherTypeId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del tipo de comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PurchaseJournalVoucherTypeIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingInventory.PurchaseJournalVoucherTypeIdXpo
        Get
            Return INDslePurchaseJournalVoucherTypeId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDslePurchaseJournalVoucherTypeId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del comprobante contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PurchaseReturnJournalVoucherTypeId As Integer? Implements ISettingInventory.PurchaseReturnJournalVoucherTypeId
        Get
            Return INDslePurchaseReturnJournalVoucherTypeId.EditValue
        End Get
        Set(value As Integer?)
            INDslePurchaseReturnJournalVoucherTypeId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' establece el datasource del tipo de comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PurchaseReturnJournalVoucherTypeIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingInventory.PurchaseReturnJournalVoucherTypeIdXpo
        Get
            Return INDslePurchaseReturnJournalVoucherTypeId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDslePurchaseReturnJournalVoucherTypeId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del tipo de comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RemissionEntranceJournalVoucherTypeId As Integer? Implements ISettingInventory.RemissionEntranceJournalVoucherTypeId
        Get
            Return INDsleRemissionEntranceJournalVoucherTypeId.EditValue
        End Get
        Set(value As Integer?)
            INDsleRemissionEntranceJournalVoucherTypeId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del tipo de comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RemissionEntranceJournalVoucherTypeIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingInventory.RemissionEntranceJournalVoucherTypeIdXpo
        Get
            Return INDsleRemissionEntranceJournalVoucherTypeId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleRemissionEntranceJournalVoucherTypeId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del tipo de comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RemissionEntranceDevolutionJournalVoucherTypeId As Integer? Implements ISettingInventory.RemissionEntranceDevolutionJournalVoucherTypeId
        Get
            Return INDSleRemissionEntranceDevolutionJournalVoucherTypeId.EditValue
        End Get
        Set(value As Integer?)
            INDSleRemissionEntranceDevolutionJournalVoucherTypeId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del tipo de comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RemissionEntranceDevolutionJournalVoucherTypeIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingInventory.RemissionEntranceDevolutionJournalVoucherTypeIdXpo
        Get
            Return INDSleRemissionEntranceDevolutionJournalVoucherTypeId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleRemissionEntranceDevolutionJournalVoucherTypeId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del tipo de comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RemissionOutputJournalVoucherTypeId As Integer? Implements ISettingInventory.RemissionOutputJournalVoucherTypeId
        Get
            Return INDsleRemissionOutputJournalVoucherTypeId.EditValue
        End Get
        Set(value As Integer?)
            INDsleRemissionOutputJournalVoucherTypeId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del tipo de comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RemissionOutputJournalVoucherTypeIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingInventory.RemissionOutputJournalVoucherTypeIdXpo
        Get
            Return INDsleRemissionOutputJournalVoucherTypeId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleRemissionOutputJournalVoucherTypeId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del tipo de comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RemissionOutputDevolutionJournalVoucherTypeId As Integer? Implements ISettingInventory.RemissionOutputDevolutionJournalVoucherTypeId
        Get
            Return INDSleRemissionOutputDevolutionJournalVoucherTypeId.EditValue
        End Get
        Set(value As Integer?)
            INDSleRemissionOutputDevolutionJournalVoucherTypeId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del tipo de comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RemissionOutputDevolutionJournalVoucherTypeIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingInventory.RemissionOutputDevolutionJournalVoucherTypeIdXpo
        Get
            Return INDSleRemissionOutputDevolutionJournalVoucherTypeId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleRemissionOutputDevolutionJournalVoucherTypeId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del tipo de comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ReclassificationRemissionJournalVoucherTypeId As Integer? Implements ISettingInventory.ReclassificationRemissionJournalVoucherTypeId
        Get
            Return INDSleReclassificationRemissionJournalVoucherTypeId.EditValue
        End Get
        Set(value As Integer?)
            INDSleReclassificationRemissionJournalVoucherTypeId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del tipo de comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ReclassificationRemissionJournalVoucherTypeIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingInventory.ReclassificationRemissionJournalVoucherTypeIdXpo
        Get
            Return INDSleReclassificationRemissionJournalVoucherTypeId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleReclassificationRemissionJournalVoucherTypeId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del tipo de comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SalesJournalVoucherTypeId As Integer? Implements ISettingInventory.SalesJournalVoucherTypeId
        Get
            Return INDsleSalesJournalVoucherTypeId.EditValue
        End Get
        Set(value As Integer?)
            INDsleSalesJournalVoucherTypeId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del tipo de comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SalesJournalVoucherTypeIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingInventory.SalesJournalVoucherTypeIdXpo
        Get
            Return INDsleSalesJournalVoucherTypeId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleSalesJournalVoucherTypeId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del tipo de comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SalesReturnJournalVoucherTypeId As Integer? Implements ISettingInventory.SalesReturnJournalVoucherTypeId
        Get
            Return INDsleSalesReturnJournalVoucherTypeId.EditValue
        End Get
        Set(value As Integer?)
            INDsleSalesReturnJournalVoucherTypeId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' establece el datasource del tipo de comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SalesReturnJournalVoucherTypeIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingInventory.SalesReturnJournalVoucherTypeIdXpo
        Get
            Return INDsleSalesReturnJournalVoucherTypeId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleSalesReturnJournalVoucherTypeId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del stock
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property StockControl As Integer? Implements ISettingInventory.StockControl
        Get
            Return INDsleStockControl.EditValue
        End Get
        Set(value As Integer?)
            INDsleStockControl.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el año
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Year As Integer Implements ISettingInventory.Year
        Get
            Return INDctrDate.GetYear
        End Get
    End Property

    Public WriteOnly Property ActionsOnControls As Boolean Implements ISettingInventory.ActionsOnControls
        Set(value As Boolean)
            INDsleTaxRegistration.Enabled = value
            INDslePackageDispensingMethod.Enabled = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece el id del concepto de ajuste de inventario de tipo entrada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InputAdjustmentConceptId As Integer Implements ISettingInventory.InputAdjustmentConceptId
        Get
            Return INDsleInputAdjustmentConceptId.EditValue
        End Get
        Set(value As Integer)
            INDsleInputAdjustmentConceptId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de los conceptos de ajuste de inventarios de tipo entrada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InputAdjustmentConceptXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingInventory.InputAdjustmentConceptXpo
        Get
            Return INDsleInputAdjustmentConceptId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleInputAdjustmentConceptId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece el id del concepto de ajuste de inventario de tipo entrada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property OutputAdjustmentConceptId As Integer Implements ISettingInventory.OutputAdjustmentConceptId
        Get
            Return INDsleOutputAdjustmentConceptId.EditValue
        End Get
        Set(value As Integer)
            INDsleOutputAdjustmentConceptId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de los conceptos de ajuste de inventarios de tipo entrada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property OutputAdjustmentConceptXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingInventory.OutputAdjustmentConceptXpo
        Get
            Return INDsleOutputAdjustmentConceptId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleOutputAdjustmentConceptId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece si se activa el proceso de solicitud automatica a farmacia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AutomaticPharmacyRequest As Boolean? Implements ISettingInventory.AutomaticPharmacyRequest
        Get
            Return INDGleAPSAF.EditValue
        End Get
        Set(value As Boolean?)
            INDGleAPSAF.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Validar productos próximos a vencer
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ValidateBatchSerialExpiredDate As Boolean? Implements ISettingInventory.ValidateBatchSerialExpiredDate
        Get
            Return INDGleValidateBatchSerialExpiredDate.EditValue
        End Get
        Set(value As Boolean?)
            INDGleValidateBatchSerialExpiredDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Indica si se valida patologías pos en medicamentos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ValidatePOSPathologies As Boolean? Implements ISettingInventory.ValidatePOSPathologies
        Get
            Return INDSleValidatePBSPathologies.EditValue
        End Get
        Set(value As Boolean?)
            INDSleValidatePBSPathologies.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Reporte a usar en el dashboard de farmacia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PharmacyDashboardReport As Byte? Implements ISettingInventory.PharmacyDashboardReport
        Get
            Return INDSlePharmacyDashboardReport.EditValue
        End Get
        Set(value As Byte?)
            INDSlePharmacyDashboardReport.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Indica si se valida dispensaciones de farmacia por unidad funcional
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PharmacyDashboardFromRequestWarehouse As Boolean? Implements ISettingInventory.PharmacyDashboardFromRequestWarehouse
        Get
            Return INDGlePharmacyDashboardFromRequestWarehouse.EditValue
        End Get
        Set(value As Boolean?)
            INDGlePharmacyDashboardFromRequestWarehouse.EditValue = value
        End Set
    End Property

    Public ReadOnly Property BudgetInterface As Boolean
        Get
            Return If(PaymentsSettingPaymentsXpo Is Nothing, False, PaymentsSettingPaymentsXpo.BudgetInterface)
        End Get
    End Property

    ''' <summary>
    ''' Indica si se valida dispensaciones de farmacia por unidad funcional
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CommitmentBudgetInterface As Boolean?
        Get
            If INDGleCommitmentBudgetInterface.EditValue Is Nothing Then
                Return False
            Else
                Return INDGleCommitmentBudgetInterface.EditValue
            End If
        End Get
        Set(value As Boolean?)
            INDGleCommitmentBudgetInterface.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del almacén principal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainWarehouse As Integer? Implements ISettingInventory.MainWarehouse
        Get
            Return INDsleMainWarehouse.EditValue
        End Get
        Set(value As Integer?)
            INDsleMainWarehouse.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de almacenes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property WarehouseXpo As XPInstantFeedbackSource Implements ISettingInventory.WarehouseXpo
        Get
            Return CType(INDsleMainWarehouse.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleMainWarehouse.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Indica si se maneja semaforización de control de medicamentos
    ''' </summary>
    ''' <remarks></remarks>
    Public Property AllowMedicamentsControl As Boolean
        Get
            If INDGleAllowMedicationsControls.EditValue Is Nothing Then
                Return False
            Else
                Return INDGleAllowMedicationsControls.EditValue
            End If
        End Get
        Set(value As Boolean)
            INDGleAllowMedicationsControls.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que maneja un objeto de tipo SettingInventoryMedicationsControl
    ''' </summary>
    ''' <remarks></remarks>
    Private _controlMedicationsEdit As SettingInventoryMedicationsControl

    ''' <summary>
    ''' Propiedad que maneja un listado de objetos de tipo SettingInventoryMedicationsControl
    ''' </summary>
    ''' <remarks></remarks>
    Private _settingInventoryMedicationsControl As Domain.Entities.TrackableCollection(Of SettingInventoryMedicationsControl)

    Public Property SettingInventoryMedicationsControl As Domain.Entities.TrackableCollection(Of SettingInventoryMedicationsControl)
        Get
            If _settingInventoryMedicationsControl Is Nothing Then
                _settingInventoryMedicationsControl = New Domain.Entities.TrackableCollection(Of SettingInventoryMedicationsControl)()
            End If
            Return _settingInventoryMedicationsControl
        End Get
        Set(value As Domain.Entities.TrackableCollection(Of SettingInventoryMedicationsControl))
            _settingInventoryMedicationsControl = value
        End Set
    End Property

#End Region

#Region "Const"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Inventory"

    Dim filter() As Object = {5, True}

#End Region

#Region "Variables"

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private _record As BlockRecordInventory

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PSettingInventory

    ''' <summary>
    ''' Representa la entidad de parametros de pago
    ''' </summary>
    ''' <remarks></remarks>
    Dim _inventorySettings As SettingInventory

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListStockControl As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListIVARetention As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListAssociatedCostCenter As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListAssociateCostMainAccount As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable que contiene la lista de centros de costo de insumos de farmacia
    ''' </summary>
    Dim ListPharmacySuppliesCostCenter As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Listado del detallo del paramtro de inventario
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListSettingInventoryFuntionalUnit As List(Of SettingInventoryFunctionalUnit)

    ''' <summary>
    ''' Listado de eliminados del parametro de inventario
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteSettingInventoryFuntionalUnit As List(Of SettingInventoryFunctionalUnit)

    ''' <summary>
    ''' Bandera para search de unidad radicacion (True=Consulta, False=No Consulta)
    ''' </summary>
    ''' <remarks></remarks>
    Private banFilingUnit As Boolean = True

    Dim isLoading As Boolean

    ''' <summary>
    ''' Listado de los registros de iva
    ''' </summary>
    Private _listTtaxRegistration As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Listado de las Dispensaciones de paquetes qx
    ''' </summary>
    Private _PackageDispensingMethod As List(Of Tuple(Of Integer, String))
#End Region

#Region "ICrud"

    ''' <summary>
    ''' Método: Deshacer
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
        DeleteBlockedRecord()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    Public Sub Buscar() Implements ICrudBase.Buscar

    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' Método: Guardar
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If ValidateControls() = False Then
            Exit Sub
        Else
            If AssociateCostMainAccount = 1 Then
                If ListSettingInventoryFuntionalUnit Is Nothing OrElse ListSettingInventoryFuntionalUnit.Count = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DontDetail", NAME_MODULE)
                    Exit Sub
                End If
            End If
        End If
        AssigningValues()
        Try
            Using model As New MSettingInventory(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await model.SaveSettingInventory(_inventorySettings)
                If Result.StateResult = True Then
                    If _inventorySettings.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                    ElseIf _inventorySettings.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                    End If
                    Me._inventorySettings = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)

                    DeleteBlockedRecord()
                    CleanControls()
                    Await LoadControls()
                    INDctrDate.Focus()

                    Me.BarraBotones.CleanAuditBasic()
                    'Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), Result.ObjectEmbbeded.CreationUser)
                    'Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), Result.ObjectEmbbeded.CreationDate)
                    'Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), Result.ObjectEmbbeded.ModificationUser)
                    'Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), Result.ObjectEmbbeded.ModificationDate)
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
                Else
                    AsyncLoader(False)
                    If Result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
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
        _inventorySettings = Nothing
        ListStockControl = Nothing
        ListIVARetention = Nothing
        ListAssociatedCostCenter = Nothing
        ListSettingInventoryFuntionalUnit = Nothing
        ListDeleteSettingInventoryFuntionalUnit = Nothing
        ListPharmacySuppliesCostCenter = Nothing
        banFilingUnit = Nothing
        AutomaticPharmacyRequest = Nothing
        editRangePopup = Nothing
        batchSerialRange = Nothing
        listBatchSerialRange = Nothing
        listDeleteBatchSerialRange = Nothing
    End Sub

    ''' <summary>
    ''' Evento load del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmSettingInventory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyInventorySettings, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        Presenter = New PSettingInventory(Me)
        Presenter.InitializeIva()
        IndigoGridControl1.RefreshGrid(INDgcDetail)
        InitializeTuples()
        LoadStatus()
        INDlyItemIVARetentionAccountPayableConceptId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemIVARetentionAccountPayableConceptId.AllowHide = True
        Await LoadPaymentsSetting()
        Await LoadControls()
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(viewGridControl, ListActions)

        ListActions.Add(eAcciones.Edit)
        IndigoGridView2.SetListAcction(INDGvBatchSerialRanges, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvBatchSerialRanges.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next

        IndigoGridControl1.RefreshGrid(INDGcBatchSerialRanges)
        IndigoGridViewPBS.SetListAcction(INDGvNoPBS, {eAcciones.Edit, eAcciones.Remove}.ToList())
        IndigoGridViewMedications.SetListAcction(INDGvMedicationsControls, {eAcciones.Edit, eAcciones.Remove}.ToList())


        Dim colActions = INDGvNoPBS.Columns.Single(Function(m) m.Name = "colActions")
        Dim colActionsM = INDGvMedicationsControls.Columns.Single(Function(m) m.Name = "colActions")
        If colActions IsNot Nothing Then colActions.Width = 90
        If colActionsM IsNot Nothing Then colActions.Width = 90
    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento cerrar del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmSettingInventory_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "Activated"

    ''' <summary>
    ''' Cuando se activa el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmSettingInventory_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        INDsleStockControl.Focus()
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario de tipo de comprobante contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDslePartialReturnSalesJournalVoucherType_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDslePartialReturnSalesJournalVoucherType.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmDocumentType With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.ShowDialog(Me)
            End Using
            Presenter.InitializePartialReturnSalesJournalVoucherType()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario de tipo de comprobante contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleTransferBetweenWarehousesConsignment_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleTransferBetweenWarehousesConsignment.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmDocumentType With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.ShowDialog(Me)
            End Using
            Presenter.InitializePartialReturnSalesJournalVoucherType()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario de tipo de comprobante contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleValuationConsignmentPriceJournalVoucherTypesId_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleValuationConsignmentPriceJournalVoucherTypesId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmDocumentType With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.ShowDialog(Me)
            End Using
            Presenter.InitializePartialReturnSalesJournalVoucherType()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario correspondiente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDslePurchaseJournalVoucherTypeId_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDslePurchaseJournalVoucherTypeId.ButtonClick, INDsleSalesJournalVoucherTypeId.ButtonClick, INDsleRemissionEntranceJournalVoucherTypeId.ButtonClick, INDsleRemissionOutputJournalVoucherTypeId.ButtonClick, INDsleLoanJournalVoucherTypeId.ButtonClick, INDsleSalesReturnJournalVoucherTypeId.ButtonClick, INDslePurchaseReturnJournalVoucherTypeId.ButtonClick, INDsleOrderDispatchReturnJournalVoucherTypeId.ButtonClick, INDSleRemissionEntranceDevolutionJournalVoucherTypeId.ButtonClick, INDSleRemissionOutputDevolutionJournalVoucherTypeId.ButtonClick, INDSleReclassificationRemissionJournalVoucherTypeId.ButtonClick, INDsleConsignmentMerchandiseJournalVoucherTypeId.ButtonClick, INDsleConsignmentMerchandiseReturnJournalVoucherTypeId.ButtonClick, INDsleConsignmentInventoryUseJournalVoucherTypeId.ButtonClick, INDsleConsignmentInventoryUseDevolutionJournalVoucherTypeId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmDocumentType With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.ShowDialog(Me)
            End Using
            Presenter.InitializeJournalVoucherType()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario correspondiente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleInventoryAdjustmentJournalVoucherTypeId_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleInventoryAdjustmentJournalVoucherTypeId.ButtonClick, INDsleInventoryCloseAdjustmentJournalVoucherTypeId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmDocumentType With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.ShowDialog(Me)
            End Using
            Presenter.InitializeJournalVoucherType()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario correspondiente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleIVAFreightAccountPayableConceptId_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleIVAFreightAccountPayableConceptId.ButtonClick, INDsleFreightAccountPayableConceptId.ButtonClick, INDsleProDevelopmentAccountPayableConceptId.ButtonClick, INDsleProElectrificationAccountPayableConceptId.ButtonClick, INDsleProCultureAccountPayableConceptId.ButtonClick, INDsleProHospitalAccountPayableConceptId.ButtonClick, INDsleProGameAccountPayableConceptId1.ButtonClick, INDsleIVARetentionAccountPayableConceptId.ButtonClick, INDsleIVAAccountPayableConceptId.ButtonClick, INDsleAdjustmentAccountPayableConceptId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmConceptsAccountsPayable With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.ShowDialog(Me)
            End Using
            Presenter.InitializeAccountPayableConcepts()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario correspondiente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleIVAGeneratedMainAccountId_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleIVAGeneratedMainAccountId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmPopupPUC With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.ShowDialog(Me)
            End Using
            Presenter.InitializeIVAGeneratedMainAccountId()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario correspondiente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleDiscountSalesMainAccountId_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleDiscountSalesMainAccountId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmPopupPUC With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.ShowDialog(Me)
            End Using
            Presenter.InitializeDiscountSalesMainAccountId()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario correspondiente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCostAccount_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleCostAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmPopupPUC With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.ShowDialog(Me)
            End Using
            Presenter.InitializeCostAccount()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario correspondiente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSalesAccount_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleSalesAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmPopupPUC With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.ShowDialog(Me)
            End Using
            Presenter.InitializeSalesAccount()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario correspondiente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleFunctionalUnit_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleFunctionalUnit.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmFunctionalUnit With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.ShowDialog(Me)
            End Using
            Presenter.InitializeFunctionalUnit()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario correspondiente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRefundAccountPayableConceptNoteId_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleRefundAccountPayableConceptNoteId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmConceptsNotes With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.ShowDialog(Me)
            End Using
            Presenter.InitializeFunctionalUnit()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario correspondiente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleInputAdjustmentConceptId_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleInputAdjustmentConceptId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmConceptsInventorySettings With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.ShowDialog(Me)
            End Using
            Presenter.InitializeInputAdjustmentConcept()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario correspondiente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleOutputAdjustmentConceptId_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleOutputAdjustmentConceptId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmConceptsInventorySettings With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.ShowDialog(Me)
            End Using
            Presenter.InitializeOutputAdjustmentConcept()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario correspondiente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleOrderDispatchJournalVoucherTypeId_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleOrderDispatchJournalVoucherTypeId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmDocumentType With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.ShowDialog(Me)
            End Using
            Presenter.InitializeJournalVoucherType()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario correspondiente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleLoanReturnJournalVoucherTypeId_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleLoanReturnJournalVoucherTypeId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmDocumentType With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.ShowDialog(Me)
            End Using
            Presenter.InitializeJournalVoucherType()
        End If
    End Sub

    Private Sub INDSleThirdPartyDispensing_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDSleThirdPartyDispensing.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using formulario As New FrmThirdParty
                formulario.ViewModeEditHold = True
                formulario.Size = New System.Drawing.Size(800, 700)
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent = New FrmTransparent(formulario, False)
                transparent.ShowDialog(Me)
                InitializeThirdPartyDispensingXPO()
            End Using
        End If
    End Sub

    Private Sub INDSleThirdPartyTransfers_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDSleThirdPartyTransfers.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using formulario As New FrmThirdParty
                formulario.ViewModeEditHold = True
                formulario.Size = New System.Drawing.Size(800, 700)
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent = New FrmTransparent(formulario, False)
                transparent.ShowDialog(Me)
                InitializeThirdPartyTransferXPO()
            End Using
        End If
    End Sub
#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de devolución parcial ventas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDslePartialReturnSalesJournalVoucherType_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDslePartialReturnSalesJournalVoucherType.QueryPopUp
        If PartialReturnSalesJournalVoucherTypeXpo Is Nothing Then
            Presenter.InitializePartialReturnSalesJournalVoucherType()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de Traslado entre almacenes en consignación
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleTransferBetweenWarehousesConsignment_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleTransferBetweenWarehousesConsignment.QueryPopUp
        If TransferBetweenWarehousesConsignmentXpo Is Nothing Then
            Presenter.InitializeTransferBetweenWarehousesConsignment()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de Traslado entre almacenes en consignación
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleValuationConsignmentPriceJournalVoucherTypesId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleValuationConsignmentPriceJournalVoucherTypesId.QueryPopUp
        If TransferBetweenWarehousesConsignmentXpo Is Nothing Then
            Presenter.InitializeValuationConsignmentPriceJournalVoucherTypes()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de tipo de comprobante
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDslePurchaseJournalVoucherTypeId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDslePurchaseJournalVoucherTypeId.QueryPopUp
        If PurchaseJournalVoucherTypeIdXpo Is Nothing Then
            Presenter.InitializePurchaseJournalVoucherTypeId()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de tipo de comprobante
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSalesJournalVoucherTypeId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleSalesJournalVoucherTypeId.QueryPopUp
        If SalesJournalVoucherTypeIdXpo Is Nothing Then
            Presenter.InitializeSalesJournalVoucherTypeId()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de tipo de comprobante
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRemissionEntranceJournalVoucherTypeId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleRemissionEntranceJournalVoucherTypeId.QueryPopUp
        If RemissionEntranceJournalVoucherTypeIdXpo Is Nothing Then
            Presenter.InitializeRemissionEntranceJournalVoucherTypeId()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de tipo de comprobante
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRemissionEntranceDevolutionJournalVoucherTypeId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleRemissionEntranceDevolutionJournalVoucherTypeId.QueryPopUp
        If RemissionEntranceDevolutionJournalVoucherTypeIdXpo Is Nothing Then
            Presenter.InitializeRemissionEntranceDevolutionJournalVoucherTypeId()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de tipo de comprobante
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRemissionOutputJournalVoucherTypeId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleRemissionOutputJournalVoucherTypeId.QueryPopUp
        If RemissionOutputJournalVoucherTypeIdXpo Is Nothing Then
            Presenter.InitializeRemissionOutputJournalVoucherTypeId()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de tipo de comprobante
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRemissionOutputDevolutionJournalVoucherTypeId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleRemissionOutputDevolutionJournalVoucherTypeId.QueryPopUp
        If RemissionOutputDevolutionJournalVoucherTypeIdXpo Is Nothing Then
            Presenter.InitializeRemissionOutputDevolutionJournalVoucherTypeId()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de tipo de comprobante
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleReclassificationRemissionJournalVoucherTypeId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleReclassificationRemissionJournalVoucherTypeId.QueryPopUp
        If ReclassificationRemissionJournalVoucherTypeIdXpo Is Nothing Then
            Presenter.InitializeReclassificationRemissionJournalVoucherTypeId()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de tipo de comprobante
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleLoanJournalVoucherTypeId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleLoanJournalVoucherTypeId.QueryPopUp
        If LoanJournalVoucherTypeIdXpo Is Nothing Then
            Presenter.InitializeLoanJournalVoucherTypeId()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de tipo de comprobante
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSalesReturnJournalVoucherTypeId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleSalesReturnJournalVoucherTypeId.QueryPopUp
        If SalesReturnJournalVoucherTypeIdXpo Is Nothing Then
            Presenter.InitializeSalesReturnJournalVoucherTypeId()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de tipo de comprobante
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDslePurchaseReturnJournalVoucherTypeId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDslePurchaseReturnJournalVoucherTypeId.QueryPopUp
        If PurchaseReturnJournalVoucherTypeIdXpo Is Nothing Then
            Presenter.InitializePurchaseReturnJournalVoucherTypeId()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de tipo de comprobante
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleConsignmentMerchandiseJournalVoucherTypeId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleConsignmentMerchandiseJournalVoucherTypeId.QueryPopUp
        If ConsignmentMerchandiseJournalVoucherTypeIdXpo Is Nothing Then
            Presenter.InitializeConsignmentMerchandiseJournalVoucherTypeId()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de tipo de comprobante
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleConsignmentMerchandiseReturnJournalVoucherTypeId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleConsignmentMerchandiseReturnJournalVoucherTypeId.QueryPopUp
        If ConsignmentMerchandiseReturnJournalVoucherTypeIdXpo Is Nothing Then
            Presenter.InitializeConsignmentMerchandiseReturnJournalVoucherTypeId()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de tipo de comprobante
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleConsignmentInventoryUseJournalVoucherTypeId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleConsignmentInventoryUseJournalVoucherTypeId.QueryPopUp
        If ConsignmentInventoryUseJournalVoucherTypeIdXpo Is Nothing Then
            Presenter.InitializeConsignmentInventoryUseJournalVoucherTypeId()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de tipo de comprobante
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleConsignmentInventoryUseDevolutionJournalVoucherTypeId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleConsignmentInventoryUseDevolutionJournalVoucherTypeId.QueryPopUp
        If ConsignmentInventoryUseDevolutionJournalVoucherTypeIdXpo Is Nothing Then
            Presenter.InitializeConsignmentInventoryUseDevolutionJournalVoucherTypeId()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de tipo de comprobante
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleOrderDispatchReturnJournalVoucherTypeId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleOrderDispatchReturnJournalVoucherTypeId.QueryPopUp
        If OrderDispatchReturnJournalVoucherTypeIdXpo Is Nothing Then
            Presenter.InitializeOrderDispatchReturnJournalVoucherTypeId()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de conceptos cxp
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleIVAFreightAccountPayableConceptId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleIVAFreightAccountPayableConceptId.QueryPopUp
        If IVAFreightAccountPayableConceptIdXpo Is Nothing Then
            Presenter.InitializeIVAFreightAccountPayableConceptId()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control tipo de comprobante para ajuste de inventario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleInventoryAdjustmentJournalVoucherTypeId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleInventoryAdjustmentJournalVoucherTypeId.QueryPopUp
        If InventoryAdjustmentJournalVoucherTypeXpo Is Nothing Then
            Presenter.InitializeInventoryAdjustmentJournalVoucherTypeId()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control tipo de comprobante para ajuste en el cierre mensual de inventario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleInventoryCloseAdjustmentJournalVoucherTypeId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleInventoryCloseAdjustmentJournalVoucherTypeId.QueryPopUp
        If InventoryCloseAdjustmentJournalVoucherTypeXpo Is Nothing Then
            Presenter.InitializeInventoryCloseAdjustmentJournalVoucherTypeId()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de conceptos cxp
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleFreightAccountPayableConceptId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleFreightAccountPayableConceptId.QueryPopUp
        If FreightAccountPayableConceptIdXpo Is Nothing Then
            Presenter.InitializeFreightAccountPayableConceptId()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de conceptos cxp
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleProDevelopmentAccountPayableConceptId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleProDevelopmentAccountPayableConceptId.QueryPopUp
        If ProDevelopmentAccountPayableConceptIdXpo Is Nothing Then
            Presenter.InitializeProDevelopmentAccountPayableConceptId()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de conceptos cxp
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleProElectrificationAccountPayableConceptId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleProElectrificationAccountPayableConceptId.QueryPopUp
        If ProElectrificationAccountPayableConceptIdXpo Is Nothing Then
            Presenter.InitializeProElectrificationAccountPayableConceptId()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de conceptos cxp
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleProCultureAccountPayableConceptId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleProCultureAccountPayableConceptId.QueryPopUp
        If ProCultureAccountPayableConceptIdXpo Is Nothing Then
            Presenter.InitializeProCultureAccountPayableConceptId()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de conceptos cxp
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleProHospitalAccountPayableConceptId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleProHospitalAccountPayableConceptId.QueryPopUp
        If ProHospitalAccountPayableConceptIdXpo Is Nothing Then
            Presenter.InitializeProHospitalAccountPayableConceptId()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de conceptos de cxp
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleProGameAccountPayableConceptId1_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleProGameAccountPayableConceptId1.QueryPopUp
        If ProGameAccountPayableConceptId1Xpo Is Nothing Then
            Presenter.InitializeProGameAccountPayableConceptId1()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de conceptos de cxp
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleIVARetentionAccountPayableConceptId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleIVARetentionAccountPayableConceptId.QueryPopUp
        If IVARetentionAccountPayableConceptIdXpo Is Nothing Then
            Presenter.InitializeIVARetentionAccountPayableConceptId()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de conceptos de cxp
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleIVAAccountPayableConceptId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleIVAAccountPayableConceptId.QueryPopUp
        If IVAAccountPayableConceptIdXpo Is Nothing Then
            Presenter.InitializeIVAAccountPayableConceptId()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de unidad de radicacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleFilingUnitId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleFilingUnitId.QueryPopUp
        If FilingUnitIdXpo Is Nothing Then
            Presenter.InitializeFilingUnit()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de cuenta contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleIVAGeneratedMainAccountId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleIVAGeneratedMainAccountId.QueryPopUp
        If IVAGeneratedMainAccountIdXpo Is Nothing Then
            Presenter.InitializeIVAGeneratedMainAccountId()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de concepto cxp
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleAdjustmentAccountPayableConceptId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleAdjustmentAccountPayableConceptId.QueryPopUp
        If AdjustmentAccountPayableConceptIdXpo Is Nothing Then
            Presenter.InitializeAdjustmentAccountPayableConceptId()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de cuenta contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleDiscountSalesMainAccountId_Properties_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleDiscountSalesMainAccountId.Properties.QueryPopUp
        If DiscountSalesMainAccountIdXpo Is Nothing Then
            Presenter.InitializeDiscountSalesMainAccountId()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de unidad funcional
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleFunctionalUnit_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleFunctionalUnit.QueryPopUp
        If FunctionalUnitIdXpo Is Nothing Then
            Presenter.InitializeFunctionalUnit()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de cuenta contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCostAccount_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCostAccount.QueryPopUp
        If CostAccountIdXpo Is Nothing Then
            Presenter.InitializeCostAccount()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de cuenta contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSalesAccount_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleSalesAccount.QueryPopUp
        If SalesAccountIdXpo Is Nothing Then
            Presenter.InitializeSalesAccount()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de concepto de nota devolución de comprobante de entrada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRefundAccountPayableConceptNoteId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleRefundAccountPayableConceptNoteId.QueryPopUp
        If RefundAccountPayableConceptNoteIdXpo Is Nothing Then
            Presenter.InitializeRefundAccountPayableConceptNote()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el concepto de ajuste de inventario tipo entrada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleInputAdjustmentConceptId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleInputAdjustmentConceptId.QueryPopUp
        If InputAdjustmentConceptXpo Is Nothing Then
            Presenter.InitializeInputAdjustmentConcept()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el concepto de ajuste de inventario tipo salida
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleOutputAdjustmentConceptId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleOutputAdjustmentConceptId.QueryPopUp
        If OutputAdjustmentConceptXpo Is Nothing Then
            Presenter.InitializeOutputAdjustmentConcept()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el tipo de comprobante de orden de traslado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleOrderDispatchJournalVoucherTypeId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleOrderDispatchJournalVoucherTypeId.QueryPopUp
        If OrderDispatchJournalVoucherTypeIdXpo Is Nothing Then
            Presenter.InitializeOrderDispatchJournalVoucherTypeId()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el tipo de comprobante de devolucion de prestamo de mercancia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleLoanReturnJournalVoucherTypeId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleLoanReturnJournalVoucherTypeId.QueryPopUp
        If LoanReturnJournalVoucherTypeIdXpo Is Nothing Then
            Presenter.InitializeLoanReturnJournalVoucherTypeId()
        End If
    End Sub

    Private Sub INDSleThirdPartyDispensing_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleThirdPartyDispensing.QueryPopUp
        If ThirdPartyDispensingXPO Is Nothing Then
            If INDSleThirdPartyDispensing.Properties.ReadOnly = False Then
                InitializeThirdPartyDispensingXPO()
            End If
        End If
    End Sub

    Private Sub INDSleThirdPartyTransfers_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleThirdPartyTransfers.QueryPopUp
        If ThirdPartyTransfersXPO Is Nothing Then
            If INDSleThirdPartyTransfers.Properties.ReadOnly = False Then
                InitializeThirdPartyTransferXPO()
            End If
        End If
    End Sub

    Private Sub INDPceAddBatchSerialRange_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDPceAddBatchSerialRange.QueryPopUp
        If editRangePopup = False Then
            If listBatchSerialRange IsNot Nothing AndAlso listBatchSerialRange.Count > 1 Then
                Dim age = listBatchSerialRange.ElementAt(listBatchSerialRange.Count - 1)
                If age.EndRange = 999 Then 'se deja quemado este valor de edad maxima
                    e.Cancel = True
                    Mensaje(EeventViewerImages.Advertencia) = "El valor maximo para las edades ya esta en uso, no se pueden agregar mas edades"
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que carga el datasource de los almacenes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleMainWarehouse_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleMainWarehouse.QueryPopUp
        If WarehouseXpo Is Nothing Then
            Presenter.ListAllActiveWarehouse()
        End If
    End Sub
#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de IVARetention
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleIVARetention_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleIVARetention.EditValueChanged
        If IVARetention IsNot Nothing Then
            If IVARetention = 1 Then
                INDlyItemIVARetentionAccountPayableConceptId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemIVARetentionAccountPayableConceptId.AllowHide = True
            Else
                INDlyItemIVARetentionAccountPayableConceptId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemIVARetentionAccountPayableConceptId.AllowHide = False
            End If
        Else
            INDlyItemIVARetentionAccountPayableConceptId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemIVARetentionAccountPayableConceptId.AllowHide = True
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de unidad de radicacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleFilingUnitId_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleFilingUnitId.EditValueChanged
        If FilingUnitId IsNot Nothing AndAlso banFilingUnit = True Then
            INDsleFilingUnitId.ValidateFilingUnit()
        End If
    End Sub

    Private Sub INDGleThirdPartySource_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleThirdPartySource.EditValueChanged
        If INDGleThirdPartySource.EditValue IsNot Nothing Then
            If INDGleThirdPartySource.EditValue = 1 Then
                INDLciThirdPartyDispensing.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciThirdPartyDispensing.AllowHide = True
                INDSleThirdPartyDispensing.EditValue = Nothing
                INDSleThirdPartyDispensing.Properties.NullText = String.Empty
            Else
                INDLciThirdPartyDispensing.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLciThirdPartyDispensing.AllowHide = False
            End If
        End If
    End Sub

    Private Sub INDsleAssociateCostMainAccount_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleAssociateCostMainAccount.EditValueChanged
        If AssociateCostMainAccount = 2 Then
            INDlygSettingInventoryFunctionalUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

            If Not isLoading Then
                If ListDeleteSettingInventoryFuntionalUnit Is Nothing Then
                    ListDeleteSettingInventoryFuntionalUnit = New List(Of SettingInventoryFunctionalUnit)
                End If

                If ListSettingInventoryFuntionalUnit IsNot Nothing Then
                    For Each settingInventoryFunctionalUnit In ListSettingInventoryFuntionalUnit
                        If settingInventoryFunctionalUnit.Id > 0 Then
                            settingInventoryFunctionalUnit.MarkAsDeleted()
                            ListDeleteSettingInventoryFuntionalUnit.Add(settingInventoryFunctionalUnit)
                        End If
                    Next
                End If

                ListSettingInventoryFuntionalUnit.Clear()
                INDgcDetail.DataSource = Nothing
                INDgcDetail.DataSource = ListSettingInventoryFuntionalUnit
            End If
        Else
            INDlygSettingInventoryFunctionalUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de INDGlePurchaseOrderInterface
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGlePurchaseOrderInterface_EditValueChanged(sender As Object, e As EventArgs) Handles INDGlePurchaseOrderInterface.EditValueChanged
        INDLcgPurchaseOrder.HideControl((INDGlePurchaseOrderInterface.EditValue = 0))
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton de agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAdd_Click(sender As Object, e As EventArgs) Handles INDbtnAdd.Click
        AddDetail()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton del popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddRange_Click(sender As Object, e As EventArgs) Handles INDbtnAddRange.Click
        AddRange()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar enter o f4 sobre el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpce_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDpce.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter OrElse e.KeyCode = System.Windows.Forms.Keys.F4 Then
            INDpce.ShowPopup()
            INDsleFunctionalUnit.Focus()
        End If
    End Sub

#End Region

#Region "ContextMenu"

    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        DeleteDetail()
    End Sub

    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        DeleteDetail()
    End Sub

    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction, IndigoGridView2.ContexMenuActions
        batchSerialRange = DirectCast(INDGvBatchSerialRanges.GetFocusedRow(), BatchSerialRange)
        Select Case sender.Tag.ToString
            Case "Edit"
                editRangePopup = True
                With batchSerialRange
                    INDseInitialRange.EditValue = .InitialRange
                    INDseEndRange.EditValue = .EndRange
                    If .EndRange = 999 Then
                        INDseEndRange.Properties.MaxValue = 999
                    Else
                        INDseEndRange.Properties.MinValue = .InitialRange + 1
                    End If
                    INDcpeColor.EditValue = .Color
                End With
                INDbtnAddRange.Text = ResourceManager.GetString("Edit")
                INDPceAddBatchSerialRange.ShowPopup()
                INDseInitialRange.Focus()
            Case "Remove"
                DeleteRange(batchSerialRange)
        End Select
    End Sub

#End Region

#End Region

#Region "Methods"

    Private Function LoadPaymentsSetting() As Task(Of Integer)
        Try
            AsyncLoader(True)
            'Se obtiene los parámetros de pagos por unidad operativa
            If BarraBotones.OperatingUnitValue <> Nothing AndAlso BarraBotones.OperatingUnitValue > 0 Then
                PaymentsSettingPaymentsXpo = Presenter.GetSettingsPaymentsByOperatingUnitId(BarraBotones.OperatingUnitValue)
            End If

            ShowHideBudgetInterface()
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = "Se presentó un error al cargar los parametros de pagos para la unidad operativa"
        Finally
            AsyncLoader(False)
        End Try

        Return Task.FromResult(Of Integer)(0)
    End Function

    ''' <summary>
    ''' Método que muestra/oculta el control de compromiso
    ''' </summary>
    Private Sub ShowHideBudgetInterface()
        INDLciCommitmentBudgetInterface.AllowHide = Not (BudgetInterface)
        INDLciCommitmentBudgetInterface.ShowInCustomizationForm = Not (BudgetInterface)
        INDLcgBudget.Visibility = If(BudgetInterface, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
    End Sub

    ''' <summary>
    ''' Metodo que inicializa el datasource de las tuplas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeTuples()
        ListStockControl = New List(Of Tuple(Of Integer, String))
        ListStockControl.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("General", NAME_MODULE)))
        ListStockControl.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("Stock", NAME_MODULE)))
        INDsleStockControl.Properties.DataSource = ListStockControl.ToList
        INDsleStockControl.Properties.Buttons(1).Visible = False

        ListIVARetention = New List(Of Tuple(Of Integer, String))
        ListIVARetention.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("ThirdParty", NAME_MODULE)))
        ListIVARetention.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("Concept", NAME_MODULE)))
        INDsleIVARetention.Properties.DataSource = ListIVARetention.ToList
        INDsleIVARetention.Properties.Buttons(1).Visible = False

        ListAssociatedCostCenter = New List(Of Tuple(Of Integer, String))
        ListAssociatedCostCenter.Add(New Tuple(Of Integer, String)(1, "Inventarios(Sin CC), Costo(Unidad Funcional)"))
        ListAssociatedCostCenter.Add(New Tuple(Of Integer, String)(2, "Inventarios(Grupo), Costo(Grupo)"))
        ListAssociatedCostCenter.Add(New Tuple(Of Integer, String)(3, "Inventarios(Almacen), Costo(Almacen)"))
        INDsleAssociateCostCenter.Properties.DataSource = ListAssociatedCostCenter.ToList

        ListAssociateCostMainAccount = New List(Of Tuple(Of Integer, String))
        ListAssociateCostMainAccount.Add(New Tuple(Of Integer, String)(1, "Parámetros de Inventario"))
        ListAssociateCostMainAccount.Add(New Tuple(Of Integer, String)(2, "Grupo del Producto"))
        INDsleAssociateCostMainAccount.Properties.DataSource = ListAssociateCostMainAccount.ToList

        ListPharmacySuppliesCostCenter = New List(Of Tuple(Of Integer, String))
        ListPharmacySuppliesCostCenter.Add(New Tuple(Of Integer, String)(1, "Unidad Funcional"))
        ListPharmacySuppliesCostCenter.Add(New Tuple(Of Integer, String)(2, "Concepto de facturación"))
        INDGlePharmacySuppliesCostCenter.Properties.DataSource = ListPharmacySuppliesCostCenter

        Dim _listThirdPartySource = New List(Of Tuple(Of Integer, String))
        _listThirdPartySource = New List(Of Tuple(Of Integer, String))
        _listThirdPartySource.Add(New Tuple(Of Integer, String)(1, "Tomar Tercero del paciente"))
        _listThirdPartySource.Add(New Tuple(Of Integer, String)(2, "Tercero Especifico"))
        INDGleThirdPartySource.Properties.DataSource = _listThirdPartySource

        Dim _listTakeTransferOrderThirdParty = New List(Of Tuple(Of Integer, String))
        _listTakeTransferOrderThirdParty.Add(New Tuple(Of Integer, String)(1, "Tercero del Documento"))
        _listTakeTransferOrderThirdParty.Add(New Tuple(Of Integer, String)(2, "Tercero del Parametro"))
        INDSleTakeTransferOrderThirdParty.Properties.DataSource = _listTakeTransferOrderThirdParty

        Dim listYesNo = New List(Of Tuple(Of Boolean, String))
        listYesNo.Add(New Tuple(Of Boolean, String)(False, "No"))
        listYesNo.Add(New Tuple(Of Boolean, String)(True, "Si"))
        INDSleManualbarCodePermission.Properties.DataSource = listYesNo.ToList()
        INDGleAPSAF.Properties.DataSource = listYesNo.ToList()
        INDGleValidateBatchSerialExpiredDate.Properties.DataSource = listYesNo.ToList()
        INDSleValidatePBSPathologies.Properties.DataSource = listYesNo.ToList()
        INDGlePharmacyDashboardFromRequestWarehouse.Properties.DataSource = listYesNo.ToList()
        INDGleCommitmentBudgetInterface.Properties.DataSource = listYesNo.ToList()
        INDGleAllowMedicationsControls.Properties.DataSource = listYesNo.ToList()

        Dim _listPharmacyDashboardReport = New List(Of Tuple(Of Byte, String))
        _listPharmacyDashboardReport.Add(New Tuple(Of Byte, String)(1, "Tirilla"))
        _listPharmacyDashboardReport.Add(New Tuple(Of Byte, String)(2, "Reporte"))
        INDSlePharmacyDashboardReport.Properties.DataSource = _listPharmacyDashboardReport

        Dim _listPurchaseOrderInterface = New List(Of Tuple(Of Byte, String))
        _listPurchaseOrderInterface.Add(New Tuple(Of Byte, String)(0, "Ninguna"))
        _listPurchaseOrderInterface.Add(New Tuple(Of Byte, String)(1, "SBS"))
        _listPurchaseOrderInterface.Add(New Tuple(Of Byte, String)(2, "Bionexo"))
        INDGlePurchaseOrderInterface.Properties.DataSource = _listPurchaseOrderInterface
        INDGleChangeAfterTimeUnit.Properties.DataSource = {
            New Tuple(Of Byte, String)(1, "Minutos"),
            New Tuple(Of Byte, String)(2, "Horas"),
            New Tuple(Of Byte, String)(3, "Días"),
            New Tuple(Of Byte, String)(4, "Semanas")
        }.ToList()

        _listTtaxRegistration = New List(Of Tuple(Of Integer, String))
        _listTtaxRegistration.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("TaxCost", NAME_MODULE)))
        _listTtaxRegistration.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("DiscountableTax", NAME_MODULE)))
        _listTtaxRegistration.Add(New Tuple(Of Integer, String)(3, ResourceManager.GetString("TaxMix", NAME_MODULE)))
        INDsleTaxRegistration.Properties.DataSource = _listTtaxRegistration

        Dim _PackageDispensingMethodAs = New List(Of Tuple(Of Boolean, String))
        _PackageDispensingMethodAs.Add(New Tuple(Of Boolean, String)(0, ResourceManager.GetString("PartialDispensation", NAME_MODULE)))
        _PackageDispensingMethodAs.Add(New Tuple(Of Boolean, String)(1, ResourceManager.GetString("TotalDispensation", NAME_MODULE)))
        INDslePackageDispensingMethod.Properties.DataSource = _PackageDispensingMethodAs

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
        StockControl = Nothing
        CleanControlsPopupPBS()
        CleanControlsPopupMedications()
        'oculto los campos de conceptos de pago pro si la empresa es privada

        If indigo.IndigoCompanyType = 1 Then
            INDlyItemProDevelopmentAccountPayableConceptId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemProElectrificationAccountPayableConceptId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemProCultureAccountPayableConceptId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemProCultureAccountPayableConceptId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemProGameAccountPayableConceptId1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            INDlyItemProDevelopmentAccountPayableConceptId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemProElectrificationAccountPayableConceptId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemProCultureAccountPayableConceptId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemProCultureAccountPayableConceptId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemProGameAccountPayableConceptId1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If

        PurchaseJournalVoucherTypeId = Nothing
        INDslePurchaseJournalVoucherTypeId.Properties.NullText = String.Empty

        SalesJournalVoucherTypeId = Nothing
        INDsleSalesJournalVoucherTypeId.Properties.NullText = String.Empty

        RemissionEntranceJournalVoucherTypeId = Nothing
        INDsleRemissionEntranceJournalVoucherTypeId.Properties.NullText = String.Empty

        RemissionOutputJournalVoucherTypeId = Nothing
        INDsleRemissionOutputJournalVoucherTypeId.Properties.NullText = String.Empty

        RemissionEntranceDevolutionJournalVoucherTypeId = Nothing
        INDSleRemissionEntranceDevolutionJournalVoucherTypeId.Properties.NullText = String.Empty

        RemissionOutputDevolutionJournalVoucherTypeId = Nothing
        INDSleRemissionOutputDevolutionJournalVoucherTypeId.Properties.NullText = String.Empty

        ReclassificationRemissionJournalVoucherTypeId = Nothing
        INDSleReclassificationRemissionJournalVoucherTypeId.Properties.NullText = String.Empty

        LoanJournalVoucherTypeId = Nothing
        INDsleLoanJournalVoucherTypeId.Properties.NullText = String.Empty

        LoanReturnJournalVoucherTypeId = Nothing
        INDsleLoanReturnJournalVoucherTypeId.Properties.NullText = String.Empty

        SalesReturnJournalVoucherTypeId = Nothing
        INDsleSalesReturnJournalVoucherTypeId.Properties.NullText = String.Empty

        PurchaseReturnJournalVoucherTypeId = Nothing
        INDslePurchaseReturnJournalVoucherTypeId.Properties.NullText = String.Empty

        ConsignmentMerchandiseJournalVoucherTypeId = Nothing
        INDsleConsignmentMerchandiseJournalVoucherTypeId.Properties.NullText = String.Empty

        ConsignmentMerchandiseReturnJournalVoucherTypeId = Nothing
        INDsleConsignmentMerchandiseReturnJournalVoucherTypeId.Properties.NullText = String.Empty

        ConsignmentInventoryUseJournalVoucherTypeId = Nothing
        INDsleConsignmentInventoryUseJournalVoucherTypeId.Properties.NullText = String.Empty

        ConsignmentInventoryUseDevolutionJournalVoucherTypeId = Nothing
        INDsleConsignmentInventoryUseDevolutionJournalVoucherTypeId.Properties.NullText = String.Empty

        OrderDispatchReturnJournalVoucherTypeId = Nothing
        INDsleOrderDispatchReturnJournalVoucherTypeId.Properties.NullText = String.Empty

        OrderDispatchJournalVoucherTypeId = Nothing
        INDsleOrderDispatchJournalVoucherTypeId.Properties.NullText = String.Empty

        TransferBetweenWarehousesConsignmentId = Nothing
        INDsleTransferBetweenWarehousesConsignment.Properties.NullText = String.Empty

        ValuationConsignmentPriceJournalVoucherTypesId = Nothing
        INDsleValuationConsignmentPriceJournalVoucherTypesId.Properties.NullText = String.Empty

        InventoryAdjustmentJournalVoucherTypeId = Nothing
        INDsleInventoryAdjustmentJournalVoucherTypeId.Properties.NullText = String.Empty

        InventoryCloseAdjustmentJournalVoucherTypeId = Nothing
        INDsleInventoryCloseAdjustmentJournalVoucherTypeId.Properties.NullText = String.Empty

        INDslePartialReturnSalesJournalVoucherType.EditValue = Nothing
        INDslePartialReturnSalesJournalVoucherType.Properties.NullText = String.Empty

        IVAFreightAccountPayableConceptId = Nothing
        INDsleIVAFreightAccountPayableConceptId.Properties.NullText = String.Empty

        FreightAccountPayableConceptId = Nothing
        INDsleFreightAccountPayableConceptId.Properties.NullText = String.Empty

        ProDevelopmentAccountPayableConceptId = Nothing
        INDsleProDevelopmentAccountPayableConceptId.Properties.NullText = String.Empty

        ProElectrificationAccountPayableConceptId = Nothing
        INDsleProElectrificationAccountPayableConceptId.Properties.NullText = String.Empty

        ProCultureAccountPayableConceptId = Nothing
        INDsleProCultureAccountPayableConceptId.Properties.NullText = String.Empty

        ProHospitalAccountPayableConceptId = Nothing
        INDsleProHospitalAccountPayableConceptId.Properties.NullText = String.Empty

        ProGameAccountPayableConceptId1 = Nothing
        INDsleProGameAccountPayableConceptId1.Properties.NullText = String.Empty

        IVARetention = Nothing

        PharmacySuppliesCostCenter = Nothing
        INDGlePharmacySuppliesCostCenter.Properties.NullText = String.Empty

        IVARetentionAccountPayableConceptId = Nothing
        INDsleIVARetentionAccountPayableConceptId.Properties.NullText = String.Empty

        FilingUnitId = Nothing
        INDsleFilingUnitId.Properties.NullText = String.Empty

        IVAAccountPayableConceptId = Nothing
        INDsleIVAAccountPayableConceptId.Properties.NullText = String.Empty

        IVAGeneratedMainAccountId = Nothing
        INDsleIVAGeneratedMainAccountId.Properties.NullText = String.Empty

        AdjustmentAccountPayableConceptId = Nothing
        INDsleAdjustmentAccountPayableConceptId.Properties.NullText = String.Empty

        AssociateCostCenter = Nothing
        AssociateCostMainAccount = Nothing

        Nomanualbarcodepermission = Nothing

        TaxRegistration = 1
        PackageDispensingMethod = 1

        DiscountSalesMainAccountId = Nothing
        INDsleDiscountSalesMainAccountId.Properties.NullText = String.Empty

        RefundAccountPayableConceptNoteId = Nothing
        INDsleRefundAccountPayableConceptNoteId.Properties.NullText = String.Empty

        InputAdjustmentConceptId = Nothing
        INDsleInputAdjustmentConceptId.Properties.NullText = String.Empty

        OutputAdjustmentConceptId = Nothing
        INDsleOutputAdjustmentConceptId.Properties.NullText = String.Empty


        INDSleThirdPartyDispensing.Properties.NullText = String.Empty
        INDSleThirdPartyDispensing.EditValue = Nothing

        INDLciThirdPartyDispensing.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        INDSleThirdPartyTransfers.Properties.NullText = String.Empty
        INDSleThirdPartyTransfers.EditValue = Nothing

        INDGleThirdPartySource.EditValue = Nothing

        INDSleTakeTransferOrderThirdParty.EditValue = Nothing

        INDsleIva.EditValue = Nothing
        INDgcDetail.DataSource = Nothing
        ListSettingInventoryFuntionalUnit = Nothing
        ListDeleteSettingInventoryFuntionalUnit = Nothing

        AutomaticPharmacyRequest = Nothing
        INDGleAPSAF.Properties.NullText = String.Empty

        ValidateBatchSerialExpiredDate = False
        ValidatePOSPathologies = False
        PharmacyDashboardReport = 1
        PharmacyDashboardFromRequestWarehouse = False
        CommitmentBudgetInterface = False

        INDGlePurchaseOrderInterface.EditValue = 0
        INDTePurchaseOrderURL.EditValue = Nothing
        INDTePurchaseOrderIdentifier.EditValue = Nothing
        INDTePurchaseOrderUser.EditValue = Nothing
        INDTePurchaseOrderPass.EditValue = Nothing

        INDGcNoPBS.DataSource = Nothing
        SettingInventoryMedicationsControl = Nothing
        INDGleDispensingWithoutAutorization.EditValue = True
        INDGleAllowDispensingWithExhaustedAuthorization.EditValue = True
        INDGleAllowBillingWithoutAuthorization.EditValue = True
        AllowMedicamentsControl = False
        INDColWithoutCurrentAuthorization.EditValue = Nothing
        INDSpnChangeAfter.EditValue = 0
        INDGleChangeAfterTimeUnit.EditValue = CByte(1)
        INDColWithoutAuthorizationManagementColor.EditValue = Nothing

        isLoading = False

        INDGcBatchSerialRanges.DataSource = Nothing
        listBatchSerialRange = Nothing
        listDeleteBatchSerialRange = Nothing

        MainWarehouse = Nothing
        INDsleMainWarehouse.Properties.NullText = String.Empty

        CleanRangeControlsPopup()
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
        Using Model As New MSettingInventory(CStr(Me.Tag))
            AsyncLoader(True)
            Try
                Dim resulOperation = Await Model.GetInventorySettingsRegister(Me._idOperativeUnit)
                _inventorySettings = resulOperation.ObjectEmbbeded
                If _inventorySettings IsNot Nothing AndAlso _inventorySettings.Id > 0 Then
                    Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                        Dim result = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(_inventorySettings.Id))
                        With _inventorySettings
                            LayoutControls.SetCustomFieldsValue(.CustomProperties)
                            Me.BarraBotones.CleanAuditBasic()
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                            isLoading = True

                            INDctrDate.SetYear = .Year
                            INDctrDate.SetMonth = .Month
                            StockControl = .StockControl
                            TaxRegistration = .TaxRegistration
                            PackageDispensingMethod = .PackageDispensingMethod
                            PurchaseJournalVoucherTypeId = .PurchaseJournalVoucherTypeId
                            INDslePurchaseJournalVoucherTypeId.Properties.NullText = .PurchaseJournalVoucherTypeDescription

                            SalesJournalVoucherTypeId = .SalesJournalVoucherTypeId
                            INDsleSalesJournalVoucherTypeId.Properties.NullText = .SalesJournalVoucherTypeDescription

                            RemissionEntranceJournalVoucherTypeId = .RemissionEntranceJournalVoucherTypeId
                            INDsleRemissionEntranceJournalVoucherTypeId.Properties.NullText = .RemissionEntranceJournalVoucherTypeDescription

                            RemissionOutputJournalVoucherTypeId = .RemissionOutputJournalVoucherTypeId
                            INDsleRemissionOutputJournalVoucherTypeId.Properties.NullText = .RemissionOutputJournalVoucherTypeDescription

                            RemissionEntranceDevolutionJournalVoucherTypeId = .RemissionEntranceDevolutionJournalVoucherTypeId
                            INDSleRemissionEntranceDevolutionJournalVoucherTypeId.Properties.NullText = .RemissionEntranceDevolutionJournalVoucherTypeDescription

                            RemissionOutputDevolutionJournalVoucherTypeId = .RemissionOutputDevolutionJournalVoucherTypeId
                            INDSleRemissionOutputDevolutionJournalVoucherTypeId.Properties.NullText = .RemissionOutputDevolutionJournalVoucherTypeDescription

                            ReclassificationRemissionJournalVoucherTypeId = .ReclassificationRemissionJournalVoucherTypeId
                            INDSleReclassificationRemissionJournalVoucherTypeId.Properties.NullText = .ReclassificationRemissionJournalVoucherTypeDescription

                            LoanJournalVoucherTypeId = .LoanJournalVoucherTypeId
                            INDsleLoanJournalVoucherTypeId.Properties.NullText = .LoanJournalVoucherTypeDescription

                            LoanReturnJournalVoucherTypeId = .LoanReturnJournalVoucherTypeId
                            INDsleLoanReturnJournalVoucherTypeId.Properties.NullText = .LoanReturnJournalVoucherTypeDescription

                            SalesReturnJournalVoucherTypeId = .SalesReturnJournalVoucherTypeId
                            INDsleSalesReturnJournalVoucherTypeId.Properties.NullText = .SalesReturnJournalVoucherTypeDescription

                            PurchaseReturnJournalVoucherTypeId = .PurchaseReturnJournalVoucherTypeId
                            INDslePurchaseReturnJournalVoucherTypeId.Properties.NullText = .PurchaseReturnJournalVoucherTypeDescription

                            ConsignmentMerchandiseJournalVoucherTypeId = .ConsignmentMerchandiseJournalVoucherTypeId
                            INDsleConsignmentMerchandiseJournalVoucherTypeId.Properties.NullText = .ConsignmentMerchandiseJournalVoucherTypeDescription

                            ConsignmentMerchandiseReturnJournalVoucherTypeId = .ConsignmentMerchandiseDevolutionJournalVoucherTypeId
                            INDsleConsignmentMerchandiseReturnJournalVoucherTypeId.Properties.NullText = .ConsignmentMerchandiseDevolutionJournalVoucherTypeDescription

                            ConsignmentInventoryUseJournalVoucherTypeId = .ConsignmentInventoryUseJournalVoucherTypeId
                            INDsleConsignmentInventoryUseJournalVoucherTypeId.Properties.NullText = .ConsignmentInventoryUseJournalVoucherTypDescription

                            ConsignmentInventoryUseDevolutionJournalVoucherTypeId = .ConsignmentInventoryUseDevolutionJournalVoucherTypeId
                            INDsleConsignmentInventoryUseDevolutionJournalVoucherTypeId.Properties.NullText = .ConsignmentInventoryUseDevolutionJournalVoucherTypDescription

                            OrderDispatchReturnJournalVoucherTypeId = .OrderDispatchReturnJournalVoucherTypeId
                            INDsleOrderDispatchReturnJournalVoucherTypeId.Properties.NullText = .OrderDispatchReturnJournalVoucherTypeDescription

                            OrderDispatchJournalVoucherTypeId = .OrderDispatchJournalVoucherTypeId
                            INDsleOrderDispatchJournalVoucherTypeId.Properties.NullText = .OrderDispatchJournalVoucherTypeDescription

                            TransferBetweenWarehousesConsignmentId = .TransferBetweenWarehousesConsignmentId
                            INDsleTransferBetweenWarehousesConsignment.Properties.NullText = .TransferBetweenWarehousesConsignmentDescription

                            ValuationConsignmentPriceJournalVoucherTypesId = .ValuationConsignmentPriceJournalVoucherTypesId
                            INDsleValuationConsignmentPriceJournalVoucherTypesId.Properties.NullText = .ValuationByPriceConsignmentDescription

                            InventoryAdjustmentJournalVoucherTypeId = .InventoryAdjustmentJournalVoucherTypeId
                            INDsleInventoryAdjustmentJournalVoucherTypeId.Properties.NullText = .InventoryAdjustmentJournalVoucherTypeDescription

                            InventoryCloseAdjustmentJournalVoucherTypeId = .InventoryCloseAdjustmentJournalVoucherTypeId
                            INDsleInventoryCloseAdjustmentJournalVoucherTypeId.Properties.NullText = .InventoryCloseAdjustmentJournalVoucherTypeDescription

                            INDslePartialReturnSalesJournalVoucherType.EditValue = .PartialReturnSalesJournalVoucherTypeId
                            INDslePartialReturnSalesJournalVoucherType.Properties.NullText = .PartialReturnSalesJournalVoucherTypeDescription

                            INDsleIva.EditValue = .FreightIVAId

                            IVAFreightAccountPayableConceptId = .IVAFreightAccountPayableConceptId
                            INDsleIVAFreightAccountPayableConceptId.Properties.NullText = .IVAFreightAccountPayableConceptDescription

                            FreightAccountPayableConceptId = .FreightAccountPayableConceptId
                            INDsleFreightAccountPayableConceptId.Properties.NullText = .FreightAccountPayableConceptDescription

                            ProDevelopmentAccountPayableConceptId = .ProDevelopmentAccountPayableConceptId
                            INDsleProDevelopmentAccountPayableConceptId.Properties.NullText = .ProDevelopmentAccountPayableConceptDescription

                            ProElectrificationAccountPayableConceptId = .ProElectrificationAccountPayableConceptId
                            INDsleProElectrificationAccountPayableConceptId.Properties.NullText = .ProElectrificationAccountPayableConceptDescription

                            ProCultureAccountPayableConceptId = .ProCultureAccountPayableConceptId
                            INDsleProCultureAccountPayableConceptId.Properties.NullText = .ProCultureAccountPayableConceptDescription

                            ProHospitalAccountPayableConceptId = .ProHospitalAccountPayableConceptId
                            INDsleProHospitalAccountPayableConceptId.Properties.NullText = .ProHospitalAccountPayableConceptDescription

                            ProGameAccountPayableConceptId1 = .ProGameAccountPayableConceptId1
                            INDsleProGameAccountPayableConceptId1.Properties.NullText = .ProGameAccountPayableConceptDescription

                            IVARetention = .IVARetention

                            PharmacySuppliesCostCenter = .PharmacySuppliesCostCenter

                            IVARetentionAccountPayableConceptId = .IVARetentionAccountPayableConceptId
                            INDsleIVARetentionAccountPayableConceptId.Properties.NullText = .IVARetentionAccountPayableConceptDescription

                            InputAdjustmentConceptId = .InputAdjustmentConceptId
                            INDsleInputAdjustmentConceptId.Properties.NullText = .InputAdjustmentConceptIdDescription

                            OutputAdjustmentConceptId = .OutputAdjustmentConceptId
                            INDsleOutputAdjustmentConceptId.Properties.NullText = .OutputAdjustmentConceptIdDescription

                            'Se carga el datasource de la unidad de radicacion porque el control no permite el NullText
                            Presenter.InitializeFilingUnit()
                            banFilingUnit = False
                            FilingUnitId = .FilingUnitId
                            INDsleFilingUnitId.Properties.NullText = .FilingUnitDescription
                            banFilingUnit = True

                            IVAAccountPayableConceptId = .IVAAccountPayableConceptId
                            INDsleIVAAccountPayableConceptId.Properties.NullText = .IVAAccountPayableConceptDescription

                            IVAGeneratedMainAccountId = .IVAGeneratedMainAccountId
                            INDsleIVAGeneratedMainAccountId.Properties.NullText = .IVAGeneratedMainAccountDescription

                            AdjustmentAccountPayableConceptId = .AdjustmentAccountPayableConceptId
                            INDsleAdjustmentAccountPayableConceptId.Properties.NullText = .AdjustmentAccountPayableConceptDescription

                            AssociateCostCenter = .AssociateCostCenter
                            AssociateCostMainAccount = .AssociateCostMainAccount
                            Nomanualbarcodepermission = .NomanualentybarCode

                            DiscountSalesMainAccountId = .DiscountSalesMainAccountId
                            INDsleDiscountSalesMainAccountId.Properties.NullText = .DiscountSalesMainAccountDescription

                            RefundAccountPayableConceptNoteId = .RefundAccountPayableConceptNoteId
                            INDsleRefundAccountPayableConceptNoteId.Properties.NullText = .RefundAccountPayableConceptNoteDescription

                            INDSleThirdPartyTransfers.EditValue = .TransferOrderThirdPartyId
                            INDSleThirdPartyTransfers.Properties.NullText = .NitNameTransferOrderThirdParty

                            INDSleTakeTransferOrderThirdParty.EditValue = .TakeTransferOrderThirdParty

                            INDGleThirdPartySource.EditValue = .PharmaceuticalDispensingGetThirdParty

                            INDSleThirdPartyDispensing.EditValue = .PharmaceuticalDispensingThirdPartyId
                            INDSleThirdPartyDispensing.Properties.NullText = .NitNamePharmaceuticalDispensingThirdParty

                            AutomaticPharmacyRequest = .ActivateAutomaticPharmacyRequestProcess
                            ValidateBatchSerialExpiredDate = .ValidateBatchSerialExpiredDate
                            ValidatePOSPathologies = .ValidatePOSPathologies
                            PharmacyDashboardReport = .PharmacyDashboardReport
                            PharmacyDashboardFromRequestWarehouse = .PharmacyDashboardFromRequestWarehouse
                            CommitmentBudgetInterface = .CommitmentBudgetInterface

                            INDGlePurchaseOrderInterface.EditValue = .PurchaseOrderInterface
                            INDTePurchaseOrderURL.EditValue = .PurchaseOrderURL
                            INDTePurchaseOrderIdentifier.EditValue = .PurchaseOrderIdentifier
                            INDTePurchaseOrderUser.EditValue = .PurchaseOrderUser
                            INDTePurchaseOrderPass.EditValue = .PurchaseOrderPass

                            ListSettingInventoryFuntionalUnit = .SettingInventoryFunctionalUnit.ToList
                            INDgcDetail.DataSource = Nothing
                            INDgcDetail.DataSource = ListSettingInventoryFuntionalUnit

                            listBatchSerialRange = .BatchSerialRange.ToList()
                            INDGcBatchSerialRanges.DataSource = listBatchSerialRange

                            INDGcNoPBS.DataSource = .SettingInventoryPBSControl.ToList()
                            SettingInventoryMedicationsControl = .SettingInventoryMedicationsControl
                            INDGcMedicationsControls.DataSource = SettingInventoryMedicationsControl
                            INDGleDispensingWithoutAutorization.EditValue = .DispensingWithoutAuthorization
                            INDGleAllowDispensingWithExhaustedAuthorization.EditValue = .AllowDispensingWithExhaustedAuthorization
                            INDGleAllowBillingWithoutAuthorization.EditValue = .AllowBillingWithoutAuthorization
                            AllowMedicamentsControl = .AllowMedicationsControls
                            INDColWithoutCurrentAuthorization.EditValue = .WithoutCurrentAuthorizationColor
                            INDSpnChangeAfter.EditValue = .ChangeAfter
                            INDGleChangeAfterTimeUnit.EditValue = .ChangeAfterTimeUnit
                            INDColWithoutAuthorizationManagementColor.EditValue = .WithoutAuthorizationManagementColor
                            MainWarehouse = .MainWarehouseId
                            INDsleMainWarehouse.Properties.NullText = .MainWarehouseCodeName

                            isLoading = False
                        End With
                        Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._inventorySettings.Id)
                        If result.Id = 0 Then
                            Dim state = New Domain.Base.Entities.ObjectChangeTracker
                            state.State = Domain.Base.Entities.ObjectState.Added
                            _record = New BlockRecordInventory With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .FormId = CInt(Me.Tag), .CodUser = Me.indigo.UserIndigo, .RecordId = _inventorySettings.Id}
                            Dim operation = Await ModelRecord.SaveBlockRecord(_record)
                            _record = operation.ObjectEmbbeded
                        Else
                            Dim xtraMessage As String = String.Format(ResourceManager.GetString("RecordLocked"), result.CodUser, result.NameUser, result.BlockDate)
                            _record = result
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                        End If
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
                        Me.BarraBotones.SetDocuments(_inventorySettings.Id)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
                    End Using
                Else
                    _inventorySettings = New SettingInventory
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
        INDctrDate.Focus()
    End Function

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With _inventorySettings
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .OperatingUnitId = _idOperativeUnit
            .Year = Year
            .Month = Month
            .StockControl = StockControl
            .PurchaseJournalVoucherTypeId = PurchaseJournalVoucherTypeId
            .SalesJournalVoucherTypeId = SalesJournalVoucherTypeId
            .RemissionEntranceJournalVoucherTypeId = RemissionEntranceJournalVoucherTypeId
            .RemissionOutputJournalVoucherTypeId = RemissionOutputJournalVoucherTypeId
            .RemissionEntranceDevolutionJournalVoucherTypeId = RemissionEntranceDevolutionJournalVoucherTypeId
            .RemissionOutputDevolutionJournalVoucherTypeId = RemissionOutputDevolutionJournalVoucherTypeId
            .ReclassificationRemissionJournalVoucherTypeId = ReclassificationRemissionJournalVoucherTypeId
            .LoanJournalVoucherTypeId = LoanJournalVoucherTypeId
            .LoanReturnJournalVoucherTypeId = LoanReturnJournalVoucherTypeId
            .SalesReturnJournalVoucherTypeId = SalesReturnJournalVoucherTypeId
            .PurchaseReturnJournalVoucherTypeId = PurchaseReturnJournalVoucherTypeId
            .ConsignmentMerchandiseJournalVoucherTypeId = ConsignmentMerchandiseJournalVoucherTypeId
            .ConsignmentMerchandiseDevolutionJournalVoucherTypeId = ConsignmentMerchandiseReturnJournalVoucherTypeId
            .ConsignmentInventoryUseJournalVoucherTypeId = ConsignmentInventoryUseJournalVoucherTypeId
            .ConsignmentInventoryUseDevolutionJournalVoucherTypeId = ConsignmentInventoryUseDevolutionJournalVoucherTypeId
            .OrderDispatchJournalVoucherTypeId = OrderDispatchJournalVoucherTypeId
            .OrderDispatchReturnJournalVoucherTypeId = OrderDispatchReturnJournalVoucherTypeId
            .TransferBetweenWarehousesConsignmentId = TransferBetweenWarehousesConsignmentId
            .ValuationConsignmentPriceJournalVoucherTypesId = ValuationConsignmentPriceJournalVoucherTypesId
            .InventoryAdjustmentJournalVoucherTypeId = InventoryAdjustmentJournalVoucherTypeId
            .InventoryCloseAdjustmentJournalVoucherTypeId = InventoryCloseAdjustmentJournalVoucherTypeId
            .PartialReturnSalesJournalVoucherTypeId = INDslePartialReturnSalesJournalVoucherType.EditValue
            .IVAFreightAccountPayableConceptId = IVAFreightAccountPayableConceptId
            .FreightAccountPayableConceptId = FreightAccountPayableConceptId
            .ProDevelopmentAccountPayableConceptId = ProDevelopmentAccountPayableConceptId
            .ProElectrificationAccountPayableConceptId = ProElectrificationAccountPayableConceptId
            .ProCultureAccountPayableConceptId = ProCultureAccountPayableConceptId
            .ProHospitalAccountPayableConceptId = ProHospitalAccountPayableConceptId
            .ProGameAccountPayableConceptId1 = ProGameAccountPayableConceptId1
            .IVARetention = IVARetention
            .PharmacySuppliesCostCenter = PharmacySuppliesCostCenter
            If INDlyItemIVARetentionAccountPayableConceptId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .IVARetentionAccountPayableConceptId = IVARetentionAccountPayableConceptId
            Else
                .IVARetentionAccountPayableConceptId = Nothing
            End If
            .FilingUnitId = FilingUnitId
            .IVAAccountPayableConceptId = IVAAccountPayableConceptId
            .IVAGeneratedMainAccountId = IVAGeneratedMainAccountId
            .AdjustmentAccountPayableConceptId = AdjustmentAccountPayableConceptId
            .NomanualentybarCode = Nomanualbarcodepermission
            .AssociateCostCenter = AssociateCostCenter
            .AssociateCostMainAccount = AssociateCostMainAccount
            .DiscountSalesMainAccountId = DiscountSalesMainAccountId
            .RefundAccountPayableConceptNoteId = RefundAccountPayableConceptNoteId
            .InputAdjustmentConceptId = InputAdjustmentConceptId
            .OutputAdjustmentConceptId = OutputAdjustmentConceptId
            .FreightIVAId = INDsleIva.EditValue
            .TakeTransferOrderThirdParty = INDSleTakeTransferOrderThirdParty.EditValue

            .TransferOrderThirdPartyId = INDSleThirdPartyTransfers.EditValue
            .PharmaceuticalDispensingGetThirdParty = INDGleThirdPartySource.EditValue
            .PharmaceuticalDispensingThirdPartyId = INDSleThirdPartyDispensing.EditValue
            .ActivateAutomaticPharmacyRequestProcess = AutomaticPharmacyRequest
            .ValidateBatchSerialExpiredDate = ValidateBatchSerialExpiredDate
            .ValidatePOSPathologies = ValidatePOSPathologies
            .PharmacyDashboardReport = PharmacyDashboardReport
            .PharmacyDashboardFromRequestWarehouse = PharmacyDashboardFromRequestWarehouse
            .CommitmentBudgetInterface = CommitmentBudgetInterface
            .TaxRegistration = TaxRegistration
            .PackageDispensingMethod = PackageDispensingMethod
            .MainWarehouseId = MainWarehouse
            .AllowMedicationsControls = AllowMedicamentsControl

            'NO PBS
            .DispensingWithoutAuthorization = INDGleDispensingWithoutAutorization.EditValue
            .AllowDispensingWithExhaustedAuthorization = INDGleAllowDispensingWithExhaustedAuthorization.EditValue
            .AllowBillingWithoutAuthorization = INDGleAllowBillingWithoutAuthorization.EditValue
            .WithoutCurrentAuthorizationColor = INDColWithoutCurrentAuthorization.Color.ToArgb()
            .ChangeAfter = INDSpnChangeAfter.EditValue
            .ChangeAfterTimeUnit = INDGleChangeAfterTimeUnit.EditValue
            .WithoutAuthorizationManagementColor = INDColWithoutAuthorizationManagementColor.Color.ToArgb()

            .PurchaseOrderInterface = INDGlePurchaseOrderInterface.EditValue
            .PurchaseOrderURL = If(.PurchaseOrderInterface <> 0, INDTePurchaseOrderURL.EditValue, Nothing)
            .PurchaseOrderIdentifier = If(.PurchaseOrderInterface <> 0, INDTePurchaseOrderIdentifier.EditValue, Nothing)
            .PurchaseOrderUser = If(.PurchaseOrderInterface <> 0, INDTePurchaseOrderUser.EditValue, Nothing)
            .PurchaseOrderPass = If(.PurchaseOrderInterface <> 0, INDTePurchaseOrderPass.EditValue, Nothing)

            If ListSettingInventoryFuntionalUnit IsNot Nothing Then
                For Each itemDetail As SettingInventoryFunctionalUnit In ListSettingInventoryFuntionalUnit
                    .SettingInventoryFunctionalUnit.Add(itemDetail)
                Next
            End If

            If ListDeleteSettingInventoryFuntionalUnit IsNot Nothing Then
                For Each itemDeleteDetail As SettingInventoryFunctionalUnit In ListDeleteSettingInventoryFuntionalUnit
                    .SettingInventoryFunctionalUnit.Add(itemDeleteDetail)
                Next
            End If

            If listBatchSerialRange IsNot Nothing Then
                For Each itemDetail As BatchSerialRange In listBatchSerialRange
                    .BatchSerialRange.Add(itemDetail)
                Next
            End If

            If listDeleteBatchSerialRange IsNot Nothing Then
                For Each itemDeleteDetail As BatchSerialRange In listDeleteBatchSerialRange
                    .BatchSerialRange.Add(itemDeleteDetail)
                Next
            End If

            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

#Region "Functional"

    ''' <summary>
    ''' Metodo que agrega los detalles a la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddDetail()
        Dim listErrors As String = ValidateControlsPopup()
        If listErrors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = listErrors
            INDsleFunctionalUnit.Focus()
            Exit Sub
        End If
        If ListSettingInventoryFuntionalUnit Is Nothing Then
            ListSettingInventoryFuntionalUnit = New List(Of SettingInventoryFunctionalUnit)
        Else
            Dim cont As Integer = ListSettingInventoryFuntionalUnit.FindAll(Function(item) item.FunctionalUnitId = FunctionalUnitId).Count
            If cont > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("ExistFuntionalUnit", NAME_MODULE)
                INDsleFunctionalUnit.Focus()
                Exit Sub
            End If
        End If
        Dim settingInventoryFunctionalUnit As New SettingInventoryFunctionalUnit
        With settingInventoryFunctionalUnit
            .FunctionalUnitId = FunctionalUnitId
            .CostAccountId = CostAccountId
            .SalesAccountId = SalesAccountId

            .FunctionalUnitDescription = INDsleFunctionalUnit.Text
            .CostAccountDescription = INDsleCostAccount.Text
            .SalesAccountDescription = INDsleSalesAccount.Text
        End With
        ListSettingInventoryFuntionalUnit.Add(settingInventoryFunctionalUnit)
        INDgcDetail.DataSource = Nothing
        INDgcDetail.DataSource = ListSettingInventoryFuntionalUnit
        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("DetailAgregatedSatisfactory", NAME_MODULE)
        CleanControlsPopup()
        INDsleFunctionalUnit.Focus()
    End Sub

    ''' <summary>
    ''' Metodo que elimina el detalle de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDetail()
        Dim sifu As SettingInventoryFunctionalUnit = CType(viewGridControl.GetFocusedRow, SettingInventoryFunctionalUnit)
        If sifu.Id <> 0 Then
            If ListDeleteSettingInventoryFuntionalUnit Is Nothing Then
                ListDeleteSettingInventoryFuntionalUnit = New List(Of SettingInventoryFunctionalUnit)
            End If
            sifu.MarkAsDeleted()
            ListDeleteSettingInventoryFuntionalUnit.Add(sifu)
        End If
        ListSettingInventoryFuntionalUnit.Remove(sifu)
        INDgcDetail.DataSource = Nothing
        INDgcDetail.DataSource = ListSettingInventoryFuntionalUnit
    End Sub

    ''' <summary>
    ''' Metodo que valida los controles del popup
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsPopup() As String
        Dim listErrors As New StringBuilder
        If FunctionalUnitId Is Nothing Then
            listErrors.Append(ResourceManager.GetString("SelectFuntionalUnit", NAME_MODULE))
        End If
        If CostAccountId Is Nothing Then
            listErrors.Append(ResourceManager.GetString("SelectCostAccount", NAME_MODULE))
        End If
        If SalesAccountId Is Nothing Then
            listErrors.Append(ResourceManager.GetString("SelectSalesAccount", NAME_MODULE))
        End If
        Return listErrors.ToString
    End Function

    ''' <summary>
    ''' Metodo que limpia los controles del popup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsPopup()
        FunctionalUnitId = Nothing
        CostAccountId = Nothing
        SalesAccountId = Nothing
    End Sub

#End Region

#Region "Range"

    Dim editRangePopup As Boolean
    Dim batchSerialRange As New BatchSerialRange
    Dim listBatchSerialRange As List(Of BatchSerialRange)
    Dim listDeleteBatchSerialRange As List(Of BatchSerialRange)

    ''' <summary>
    ''' Metodo que agrega un rango a la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddRange()
        Dim listErrors = ValidateRangeControlsPopup()
        If listErrors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = listErrors
            INDseInitialRange.Focus()
            Exit Sub
        End If

        If editRangePopup = False Then
            batchSerialRange = New BatchSerialRange()
        End If

        With batchSerialRange
            .InitialRange = INDseInitialRange.EditValue
            .EndRange = INDseEndRange.EditValue
            .Color = INDcpeColor.Color.ToArgb()
        End With

        If listBatchSerialRange Is Nothing Then
            listBatchSerialRange = New List(Of BatchSerialRange)
        End If

        If editRangePopup = False Then
            listBatchSerialRange.Add(batchSerialRange)
        Else
            Dim index = listBatchSerialRange.IndexOf(batchSerialRange)
            For i = index + 1 To listBatchSerialRange.Count - 1 Step 1
                Dim diference = listBatchSerialRange.ElementAt(i).EndRange - listBatchSerialRange.ElementAt(i).InitialRange
                If i = index + 1 Then
                    listBatchSerialRange.ElementAt(i).InitialRange = batchSerialRange.EndRange + 1
                Else
                    listBatchSerialRange.ElementAt(i).InitialRange = listBatchSerialRange.ElementAt(i - 1).EndRange + 1
                End If
                listBatchSerialRange.ElementAt(i).EndRange = listBatchSerialRange.ElementAt(i).InitialRange + diference
            Next
        End If

        INDGcBatchSerialRanges.DataSource = Nothing
        INDGcBatchSerialRanges.DataSource = listBatchSerialRange

        If batchSerialRange.EndRange = 999 Then
            INDPceAddBatchSerialRange.ClosePopup()
        Else
            CleanRangeControlsPopup()
            INDseInitialRange.Focus()
        End If

    End Sub

    ''' <summary>
    ''' Metodo que elimina un rango
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteRange(batchSerialRangeP As BatchSerialRange)
        If batchSerialRangeP.Id > 0 Then
            If listDeleteBatchSerialRange Is Nothing Then
                listDeleteBatchSerialRange = New List(Of BatchSerialRange)
            End If
            batchSerialRangeP.MarkAsDeleted()
            listDeleteBatchSerialRange.Add(batchSerialRangeP)
        End If
        Dim index = listBatchSerialRange.IndexOf(batchSerialRangeP)
        listBatchSerialRange.Remove(batchSerialRangeP)
        If listBatchSerialRange.Count > 1 Then
            For i = index To listBatchSerialRange.Count - 1 Step 1
                If i = index Then
                    listBatchSerialRange.ElementAt(i).InitialRange = batchSerialRangeP.InitialRange
                Else
                    Dim diference = listBatchSerialRange.ElementAt(i).EndRange - listBatchSerialRange.ElementAt(i).InitialRange
                    listBatchSerialRange.ElementAt(i).InitialRange = listBatchSerialRange.ElementAt(i - 1).EndRange + 1
                    listBatchSerialRange.ElementAt(i).EndRange = listBatchSerialRange.ElementAt(i).InitialRange + diference
                End If
            Next
        ElseIf listBatchSerialRange.Count = 1 Then
            listBatchSerialRange.ElementAt(0).InitialRange = 1
        End If

        INDGcBatchSerialRanges.DataSource = Nothing
        INDGcBatchSerialRanges.DataSource = listBatchSerialRange
        CleanRangeControlsPopup()
    End Sub

    ''' <summary>
    ''' Valida los controles del popup
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateRangeControlsPopup() As String
        Dim errors As New StringBuilder
        If INDseEndRange.EditValue = 0 Then
            errors.AppendLine(INDlyItemEndRange.CustomizationFormText + ResourceManager.GetString("Empty"))
        End If
        If INDcpeColor.Color.ToArgb() = 0 Then
            errors.AppendLine(INDlyItemColor.CustomizationFormText + ResourceManager.GetString("Empty"))
        End If
        If INDseInitialRange.EditValue > INDseEndRange.EditValue Then
            errors.AppendLine(String.Format(ResourceManager.GetString("Range", "Portfolio"), INDlyItemInitialRange.CustomizationFormText, INDlyItemEndRange.CustomizationFormText))
        End If
        Return errors.ToString()
    End Function

    ''' <summary>
    ''' Metodo que limpia los controles del popup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanRangeControlsPopup()
        editRangePopup = False
        INDbtnAddRange.Text = ResourceManager.GetString("Add")
        INDcpeColor.EditValue = Nothing
        INDseEndRange.Properties.ReadOnly = False

        batchSerialRange = Nothing
        SetRanges()
    End Sub

    ''' <summary>
    ''' Establece el rango de las edades
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetRanges()
        If listBatchSerialRange IsNot Nothing AndAlso listBatchSerialRange.Count > 0 Then
            INDseInitialRange.EditValue = listBatchSerialRange.ElementAt(listBatchSerialRange.Count - 1).EndRange + 1
            INDseEndRange.EditValue = INDseInitialRange.EditValue + 1
            If INDseEndRange.EditValue >= 999 Then
                INDseEndRange.EditValue = 999
                INDseEndRange.Properties.MaxValue = 999
            Else
                INDseEndRange.Properties.MinValue = INDseInitialRange.EditValue + 1
            End If
        Else
            INDseInitialRange.EditValue = 0
            INDseEndRange.EditValue = 1
            INDseEndRange.Properties.MinValue = 1
        End If
    End Sub

#End Region

    ''' <summary>
    ''' metodo para consultar el tercero
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeThirdPartyTransferXPO()
        If Not DesignMode Then
            Using model As New MBusqueda
                ThirdPartyTransfersXPO = model.ConsultarEntidades(eDataSource.ThirdParty)
            End Using

        End If
    End Sub


    ''' <summary>
    ''' metodo para consultar el tercero
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeThirdPartyDispensingXPO()
        If Not DesignMode Then
            Using model As New MBusqueda
                ThirdPartyDispensingXPO = model.ConsultarEntidades(eDataSource.ThirdParty)
            End Using
        End If
    End Sub
#End Region

#Region "Barra Botones"

    ''' <summary>
    ''' Barra Botones: Activa o desactiva el estado
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
    End Sub

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
            Await LoadPaymentsSetting()
            Await LoadControls()
            If _inventorySettings IsNot Nothing AndAlso _inventorySettings.Id > 0 Then
                _inventorySettings.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified
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
    ''' <param name="operatingUnit"></param>
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

#End Region


    Private Sub INDsleFilingUnitId_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleFilingUnitId.ButtonClick
        If e.Button.Kind = ButtonPredefines.Plus Then
            OpenForm("723", Nothing, True)
        End If
    End Sub

    Private Sub INDsleIva_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleIva.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
                Using Formulario As New FrmGeneralLedgerIVA
                    Formulario.ViewModeEditHold = True
                    Formulario.MinimizeBox = False
                    Formulario.MaximizeBox = False
                    Formulario.Size = New System.Drawing.Size(780, 700)
                    Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                    Dim transparent As New FrmTransparent(Formulario, False)
                    transparent.ShowDialog()
                    Presenter.InitializeIva()
                End Using
            End If
            'OpenForm("1509", INDsleIva.EditValue, True)
        End If
    End Sub

    Private _controlDosePBSEdit As SettingInventoryPBSControl

    Private Sub INDSbAcceptPBS_Click(sender As Object, e As EventArgs) Handles INDSbAcceptPBS.Click
        If _controlDosePBSEdit Is Nothing Then _controlDosePBSEdit = New SettingInventoryPBSControl()

        With _controlDosePBSEdit
            .DoseFrom = CInt(INDSpnFrom.EditValue)
            .DoseTo = CInt(INDSpnTo.EditValue)
            .Color = INDColPBS.Color.ToArgb()
        End With

        _inventorySettings.SettingInventoryPBSControl.Add(_controlDosePBSEdit)
        INDGcNoPBS.DataSource = _inventorySettings.SettingInventoryPBSControl.ToList()
        INDGcNoPBS.RefreshDataSource()
        CleanControlsPopupPBS()
        INDSpnFrom.Focus()
    End Sub

    Private Sub IndigoGridViewPBS_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridViewPBS.Click_ButtonAction, IndigoGridViewPBS.ContexMenuActions
        If sender.Tag = NameOf(eAcciones.Edit) Then
            _controlDosePBSEdit = INDGvNoPBS.GetFocusedObject(Of SettingInventoryPBSControl)()
            INDPceAddPBSControl.ShowPopup()
        ElseIf sender.Tag = NameOf(eAcciones.Remove) Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                DeleteControlPBS()
            End If
        End If
    End Sub

    Private Sub DeleteControlPBS()
        Dim controlDosePBSEdit = INDGvNoPBS.GetFocusedObject(Of SettingInventoryPBSControl)()
        If controlDosePBSEdit IsNot Nothing Then
            controlDosePBSEdit.MarkAsDeleted()
            INDGcNoPBS.DataSource = _inventorySettings.SettingInventoryPBSControl.ToList()
            INDGcNoPBS.RefreshDataSource()
        End If
    End Sub

    Private Sub INDPceAddPBSControl_Closed(sender As Object, e As ClosedEventArgs) Handles INDPceAddPBSControl.Closed
        CleanControlsPopupPBS()
    End Sub

    Private Sub CleanControlsPopupPBS()
        INDSpnFrom.EditValue = 0
        INDSpnTo.EditValue = 0
        INDColPBS.EditValue = Nothing
        _controlDosePBSEdit = Nothing
        INDSbAcceptPBS.Text = "Agregar"
        INDSpnFrom.Focus()
    End Sub

    Private Sub INDPceAddPBSControl_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDPceAddPBSControl.QueryPopUp
        If _controlDosePBSEdit IsNot Nothing Then
            With _controlDosePBSEdit
                INDSpnFrom.EditValue = .DoseFrom
                INDSpnTo.EditValue = .DoseTo
                INDColPBS.EditValue = .Color
            End With

            INDSbAcceptPBS.Text = "Editar"
        Else
            INDSbAcceptPBS.Text = "Agregar"
        End If

        INDSpnFrom.Focus()
    End Sub

    ''' <summary>
    ''' Ejecuta en el evento click del boton agregar/actualizar del popup de control de medicamentos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSbAcceptMedications_Click(sender As Object, e As EventArgs) Handles INDSbAcceptMedications.Click

        If _controlMedicationsEdit Is Nothing Then _controlMedicationsEdit = New SettingInventoryMedicationsControl()

        With _controlMedicationsEdit
            .DoseFrom = CInt(INDSpnFromM.EditValue)
            .DoseTo = CInt(INDSpnToM.EditValue)
            .Color = INDColMedications.Color.ToArgb()
        End With

        SettingInventoryMedicationsControl.Add(_controlMedicationsEdit)
        INDGcMedicationsControls.DataSource = SettingInventoryMedicationsControl
        INDGcMedicationsControls.RefreshDataSource()
        CleanControlsPopupMedications()

    End Sub

    ''' <summary>
    ''' se ejecutan los eventros click del boton derecho del mouse de la rejilla de la semaforización de medicamentos controlados
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridViewMedications_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridViewMedications.Click_ButtonAction, IndigoGridViewMedications.ContexMenuActions
        If sender.Tag = NameOf(eAcciones.Edit) Then
            _controlMedicationsEdit = INDGvMedicationsControls.GetFocusedObject(Of SettingInventoryMedicationsControl)()
            INDPceAddMedicationsControls.ShowPopup()
        ElseIf sender.Tag = NameOf(eAcciones.Remove) Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                DeleteControlMedications()
            End If
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta la acción de cerrar el popup del control de medicamentos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDPceAddMedicationsControls_Closed(sender As Object, e As ClosedEventArgs) Handles INDPceAddMedicationsControls.Closed
        CleanControlsPopupMedications()
    End Sub
    ''' <summary>
    ''' Método que ejecuta la accion de limpiar todos los controles del popup de semaforización de medicamentos controlados
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsPopupMedications()
        INDSpnFromM.EditValue = 0
        INDSpnToM.EditValue = 0
        INDColMedications.EditValue = Nothing
        _controlMedicationsEdit = Nothing
        If INDSbAcceptMedications.Text = "Editar" Then
            INDSbAcceptMedications.Text = "Agregar"
            INDPceAddMedicationsControls.ClosePopup()
            INDGvMedicationsControls.Focus()
        Else
            INDSpnFromM.Focus()
        End If
    End Sub
    ''' <summary>
    ''' Método que ejecuta la accion de eliminar un item de la rejilla de la semaforización de medicamentos controlados
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteControlMedications()
        Dim controlMedicationsEdit = INDGvMedicationsControls.GetFocusedObject(Of SettingInventoryMedicationsControl)()
        If controlMedicationsEdit IsNot Nothing Then
            controlMedicationsEdit.MarkAsDeleted()
            INDGcMedicationsControls.DataSource = SettingInventoryMedicationsControl
            INDGcMedicationsControls.RefreshDataSource()
        End If
    End Sub
    ''' <summary>
    ''' Evento query pop up de la semaforización de medicamentos controlados
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDPceAddMedicationsControls_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDPceAddMedicationsControls.QueryPopUp
        If _controlMedicationsEdit IsNot Nothing Then
            With _controlMedicationsEdit
                INDSpnFromM.EditValue = .DoseFrom
                INDSpnToM.EditValue = .DoseTo
                INDColMedications.EditValue = .Color
            End With

            INDSbAcceptMedications.Text = "Editar"
        Else
            INDSbAcceptMedications.Text = "Agregar"
        End If

        INDSpnFromM.Focus()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de semaforización de medicamentos controlados
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGleAllowMedicationsControls_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleAllowMedicationsControls.EditValueChanged
        INDLcgMedicationsControl.HideControl(Not AllowMedicamentsControl)
    End Sub

    ''' <summary>
    ''' Evento para limpiar el control "Almacén principal"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleMainWarehouse_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleMainWarehouse.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete Then
            INDsleMainWarehouse.Properties.NullText = Nothing
            MainWarehouse = Nothing
        End If
    End Sub
End Class