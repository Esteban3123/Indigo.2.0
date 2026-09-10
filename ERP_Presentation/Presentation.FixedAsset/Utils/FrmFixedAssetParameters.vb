'***********************************************************************
' Assembly         : Presentacion.FixedAsset
' Author           : Carlos Mario Arias Rubiano
' Created          : 01/04/2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Drawing
Imports System.Text
Imports DevExpress.Xpo
Imports DevExpress.XtraEditors.Controls
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.PaymentsRepository
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.FixedAsset.MVP
Imports Presentation.Payments.MVP

#End Region

Public Class FrmSettingFixedAsset
    Implements ISettingFixedAsset

#Region "Properties"



    Public Property ReplacementMainAccountId As Integer? Implements ISettingFixedAsset.ReplacementMainAccountId
        Get
            Return INDsleReplacementMainAccount.EditValue
        End Get
        Set(value As Integer?)
            INDsleReplacementMainAccount.EditValue = value
        End Set
    End Property

    Public Property SalesMainAccountId As Integer? Implements ISettingFixedAsset.SalesMainAccountId
        Get
            Return INDsleSalesMainAccount.EditValue
        End Get
        Set(value As Integer?)
            INDsleSalesMainAccount.EditValue = value
        End Set
    End Property

    Public Property IdTransferJournalVoucherId As Integer? Implements ISettingFixedAsset.IdTransferJournalVoucherId
        Get
            Return INDsleTransferJournalVoucher.EditValue
        End Get
        Set(value As Integer?)
            INDsleTransferJournalVoucher.EditValue = value
        End Set
    End Property

    Public Property IdReclassificationJournalVoucherId As Integer? Implements ISettingFixedAsset.IdReclassificationJournalVoucherId
        Get
            Return INDsleReclassificationJournalVoucher.EditValue
        End Get
        Set(value As Integer?)
            INDsleReclassificationJournalVoucher.EditValue = value
        End Set
    End Property

    Public Property DevolutionJournalVoucherId As Integer? Implements ISettingFixedAsset.DevolutionJournalVoucherId
        Get
            Return INDsleDevolutionJournalVoucher.EditValue
        End Get
        Set(value As Integer?)
            INDsleDevolutionJournalVoucher.EditValue = value
        End Set
    End Property

    Public Property IVARetentionAccountPayableConceptId As Integer? Implements ISettingFixedAsset.IVARetentionAccountPayableConceptId
        Get
            Return INDsleIVARetentionAccountPayableConcept.EditValue
        End Get
        Set(value As Integer?)
            INDsleIVARetentionAccountPayableConcept.EditValue = value
        End Set
    End Property

    Public Property IVARetention As Integer? Implements ISettingFixedAsset.IVARetention
        Get
            Return INDsleIVARetention.EditValue
        End Get
        Set(value As Integer?)
            INDsleIVARetention.EditValue = value
        End Set
    End Property

    Public Property IVAAccountPayableConceptId As Integer? Implements ISettingFixedAsset.IVAAccountPayableConceptId
        Get
            Return INDsleIVAAccountPayableConcept.EditValue
        End Get
        Set(value As Integer?)
            INDsleIVAAccountPayableConcept.EditValue = value
        End Set
    End Property

    Public Property FreightAccountPayableConceptId As Integer? Implements ISettingFixedAsset.FreightAccountPayableConceptId
        Get
            Return INDsleFreightAccountPayableConcept.EditValue
        End Get
        Set(value As Integer?)
            INDsleFreightAccountPayableConcept.EditValue = value
        End Set
    End Property

    Public Property IVAFreightAccountPayableConceptId As Integer? Implements ISettingFixedAsset.IVAFreightAccountPayableConceptId
        Get
            Return INDsleIVAFreightAccountPayableConcept.EditValue
        End Get
        Set(value As Integer?)
            INDsleIVAFreightAccountPayableConcept.EditValue = value
        End Set
    End Property

    Public Property ServiceMainAccountId As Integer? Implements ISettingFixedAsset.ServiceMainAccountId
        Get
            Return INDsleServiceMainAccount.EditValue
        End Get
        Set(value As Integer?)
            INDsleServiceMainAccount.EditValue = value
        End Set
    End Property

    Public Property FilingUnitId As Integer? Implements ISettingFixedAsset.FilingUnitId
        Get
            Return INDsleFilingUnit.EditValue
        End Get
        Set(value As Integer?)
            INDsleFilingUnit.EditValue = value
        End Set
    End Property

    Public Property FixedAssetChief As Integer? Implements ISettingFixedAsset.FixedAssetChief
        Get
            Return INDSlActiveChief.EditValue
        End Get
        Set(value As Integer?)
            INDSlActiveChief.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que trae el Id de la moneda escogida
    ''' </summary>
    ''' <returns></returns>
    Public Property CurrencyId As Integer? Implements ISettingFixedAsset.CurrencyId
        Get
            Return INDSlCurrency.EditValue
        End Get
        Set(value As Integer?)
            INDSlCurrency.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para tomar la abreviacion de la moneda y guardarla temp para cuando se necesite editar
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property CurrencyAbbreviation As String
        Get
            Return INDSlCurrency.Text
        End Get
    End Property

    Public Property IdAditionAccountingVoucher As Integer? Implements ISettingFixedAsset.IdAditionAccountingVoucher
        Get
            Return INDSlAditionVoucher.EditValue
        End Get
        Set(value As Integer?)
            INDSlAditionVoucher.EditValue = value
        End Set
    End Property

    Public Property IdDepreciationAccountingVoucher As Integer? Implements ISettingFixedAsset.IdDepreciationAccountingVoucher
        Get
            Return INDSlDepreciationVoucher.EditValue
        End Get
        Set(value As Integer?)
            INDSlDepreciationVoucher.EditValue = value
        End Set
    End Property

    Public Property IdDonationAccountingAccount As Integer? Implements ISettingFixedAsset.IdDonationAccountingAccount
        Get
            Return INDSlDonationIngressAccount.EditValue
        End Get
        Set(value As Integer?)
            INDSlDonationIngressAccount.EditValue = value
        End Set
    End Property

    Public Property IdIngressAccountingVoucher As Integer? Implements ISettingFixedAsset.IdIngressAccountingVoucher
        Get
            Return INDSlIngressVoucher.EditValue
        End Get
        Set(value As Integer?)
            INDSlIngressVoucher.EditValue = value
        End Set
    End Property

    Public Property IdOtherConceptsAccountingAccount As Integer? Implements ISettingFixedAsset.IdOtherConceptsAccountingAccount
        Get
            Return INDSlOtherConceptIngressAccount.EditValue
        End Get
        Set(value As Integer?)
            INDSlOtherConceptIngressAccount.EditValue = value
        End Set
    End Property

    Public Property IdOtherIngressAccountingAccount As Integer? Implements ISettingFixedAsset.IdOtherIngressAccountingAccount
        Get
            Return INDSlOtherIngressAccountingAccount.EditValue
        End Get
        Set(value As Integer?)
            INDSlOtherIngressAccountingAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que trae el Id del comprobante amortizacion intangibles
    ''' </summary>
    Public Property IdIntangibleAmortizationVoucher As Integer? Implements ISettingFixedAsset.IdIntangibleAmortizationVoucher
        Get
            Return INDSleIntangibleAmortizationVoucher.EditValue
        End Get
        Set(value As Integer?)
            INDSleIntangibleAmortizationVoucher.EditValue = value
        End Set
    End Property

    Public Property IdOutputAccountingVoucher As Integer? Implements ISettingFixedAsset.IdOutputAccountingVoucher
        Get
            Return INDSlOutputVoucher.EditValue
        End Get
        Set(value As Integer?)
            INDSlOutputVoucher.EditValue = value
        End Set
    End Property

    Public Property IdRecuperationAccountingAccount As Integer? Implements ISettingFixedAsset.IdRecuperationAccountingAccount
        Get
            Return INDSlRecuperationIngressAccount.EditValue
        End Get
        Set(value As Integer?)
            INDSlRecuperationIngressAccount.EditValue = value
        End Set
    End Property

    Public Property IdTransferPropertyAccountingAccount As Integer? Implements ISettingFixedAsset.IdTransferPropertyAccountingAccount
        Get
            Return INDSlTransferOfPropertyIngressAccount.EditValue
        End Get
        Set(value As Integer?)
            INDSlTransferOfPropertyIngressAccount.EditValue = value
        End Set
    End Property

    Public Property IvaCost As Boolean Implements ISettingFixedAsset.IvaCost
        Get
            Return INDRgIncludeCostIVA.EditValue
        End Get
        Set(value As Boolean)
            INDRgIncludeCostIVA.EditValue = value
        End Set
    End Property

    Public Property LowBidAmount As Double Implements ISettingFixedAsset.LowBidAmount
        Get
            Return INDTxtValueMinimum.EditValue
        End Get
        Set(value As Double)
            INDTxtValueMinimum.EditValue = value
        End Set
    End Property

    Public Property ProcessDate As DateTime? Implements ISettingFixedAsset.ProcessDate
        Get
            Return IndDeProcessDate.EditValue
        End Get
        Set(value As DateTime?)
            IndDeProcessDate.EditValue = value
        End Set
    End Property

    Public Property TopMinorValue As Double Implements ISettingFixedAsset.TopMinorValue
        Get
            Return INDTxtTopValue.EditValue
        End Get
        Set(value As Double)
            INDTxtTopValue.EditValue = value
        End Set
    End Property

    Public ReadOnly Property MyTag As Object Implements ISettingFixedAsset.MyTag
        Get
            Return Me.Tag
        End Get
    End Property


    ''' <summary>
    ''' Obtiene o establece el id del tipo de comprobante contable de Valorización/Desvalorización
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ValorizationDevaluationAccountingVoucherId As Integer? Implements ISettingFixedAsset.ValorizationDevaluationAccountingVoucherId
        Get
            Return INDsleValorizationDevaluationAccountingVoucher.EditValue
        End Get
        Set(value As Integer?)
            INDsleValorizationDevaluationAccountingVoucher.EditValue = value
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
            Return INDGleCommitmentBudgetInterface.EditValue
        End Get
        Set(value As Boolean?)
            INDGleCommitmentBudgetInterface.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' indica si aplica iva al costo en compra
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TaxCostBuy As Boolean?
        Get
            Return INDsleTaxCostBuy.EditValue
        End Get
        Set(value As Boolean?)
            INDsleTaxCostBuy.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' indica si aplica iva al costo en compra
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Depreciation30Days As Boolean?
        Get
            Return INDRgDepreciation.EditValue
        End Get
        Set(value As Boolean?)
            INDRgDepreciation.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del libro oficial
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LegalBookId As Integer? Implements ISettingFixedAsset.LegalBookId
        Get
            Return INDsleLegalBook.EditValue
        End Get
        Set(value As Integer?)
            INDsleLegalBook.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor establecido de iva al costo por el libro indicado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IvaCostByLegalBook As Boolean? Implements ISettingFixedAsset.IvaCostByLegalBook
        Get
            Return INDsleIvaCost.EditValue
        End Get
        Set(value As Boolean?)
            INDsleIvaCost.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor establecido de manejo de cuantia mínima por el libro indicado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HandlesMinimumAmountByLegalBook As Boolean? Implements ISettingFixedAsset.HandlesMinimumAmountByLegalBook
        Get
            Return INDsleHandlesMinimumAmount.EditValue
        End Get
        Set(value As Boolean?)
            INDsleHandlesMinimumAmount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor establecido de Aplica Catalogo de bienes y servicios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AppliesCatalogPropertyandServices As Boolean? Implements ISettingFixedAsset.AppliesCatalogPropertyandServices
        Get
            Return INDSleAppliesCatalogPropertyandServices.EditValue
        End Get
        Set(value As Boolean?)
            INDSleAppliesCatalogPropertyandServices.EditValue = value
        End Set
    End Property

#Region "Datasource"

    Public Property ReplacementMainAccountXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingFixedAsset.ReplacementMainAccountXpo
        Get
            Return INDsleReplacementMainAccount.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleReplacementMainAccount.Properties.DataSource = value
        End Set
    End Property

    Public Property SalesMainAccountXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingFixedAsset.SalesMainAccountXpo
        Get
            Return INDsleSalesMainAccount.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleSalesMainAccount.Properties.DataSource = value
        End Set
    End Property

    Public Property TransferJournalVoucherXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingFixedAsset.TransferJournalVoucherXpo
        Get
            Return INDsleTransferJournalVoucher.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleTransferJournalVoucher.Properties.DataSource = value
        End Set
    End Property

    Public Property ReclassificationJournalVoucherXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingFixedAsset.ReclassificationJournalVoucherXpo
        Get
            Return INDsleReclassificationJournalVoucher.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleReclassificationJournalVoucher.Properties.DataSource = value
        End Set
    End Property

    Public Property DevolutionJournalVoucherXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingFixedAsset.DevolutionJournalVoucherXpo
        Get
            Return INDsleDevolutionJournalVoucher.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleDevolutionJournalVoucher.Properties.DataSource = value
        End Set
    End Property

    Public Property RefundAccountPayableConceptNoteXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingFixedAsset.RefundAccountPayableConceptNoteXpo
        Get
            Return CType(INDsleRefundAccountPayableConceptNoteId.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleRefundAccountPayableConceptNoteId.Properties.DataSource = value
        End Set
    End Property

    Public Property IVAAccountPayableConceptXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingFixedAsset.IVAAccountPayableConceptXpo
        Get
            Return INDsleIVAAccountPayableConcept.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleIVAAccountPayableConcept.Properties.DataSource = value
        End Set
    End Property

    Public Property IVARetentionAccountPayableConceptXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingFixedAsset.IVARetentionAccountPayableConceptXpo
        Get
            Return INDsleIVARetentionAccountPayableConcept.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleIVARetentionAccountPayableConcept.Properties.DataSource = value
        End Set
    End Property

    Public Property FreightAccountPayableConceptXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingFixedAsset.FreightAccountPayableConceptXpo
        Get
            Return INDsleFreightAccountPayableConcept.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleFreightAccountPayableConcept.Properties.DataSource = value
        End Set
    End Property

    Public Property ServiceMainAccountXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingFixedAsset.ServiceMainAccountXpo
        Get
            Return INDsleServiceMainAccount.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleServiceMainAccount.Properties.DataSource = value
        End Set
    End Property

    Public Property FilingUnitXpo As List(Of FilingUnit) Implements ISettingFixedAsset.FilingUnitXpo
        Get
            Return INDsleFilingUnit.Properties.DataSource
        End Get
        Set(value As List(Of FilingUnit))
            INDsleFilingUnit.Properties.DataSource = value
        End Set
    End Property

    Public Property IVAFreightAccountPayableConceptXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingFixedAsset.IVAFreightAccountPayableConceptXpo
        Get
            Return INDsleIVAFreightAccountPayableConcept.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleIVAFreightAccountPayableConcept.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad del campo moneda que asigna una lista de monedas desde la interfaz de la clase
    ''' </summary>
    ''' <returns></returns>
    Public Property ListCurrency As XPCollection Implements ISettingFixedAsset.ListCurrency
        Get
            Return INDSlCurrency.Properties.DataSource
        End Get
        Set(value As XPCollection)
            INDSlCurrency.Properties.DataSource = value
        End Set
    End Property

    Public Property IngressListAccountingVoucher As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingFixedAsset.IngressListAccountingVoucher
        Get
            Return CType(INDSlIngressVoucher.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSlIngressVoucher.Properties.DataSource = value
        End Set
    End Property

    Public Property DepreciationListAccountingVoucher As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingFixedAsset.DepreciationListAccountingVoucher
        Get
            Return CType(INDSlDepreciationVoucher.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSlDepreciationVoucher.Properties.DataSource = value
        End Set
    End Property

    Public Property AditionListAccountingVoucher As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingFixedAsset.AditionListAccountingVoucher
        Get
            Return CType(INDSlAditionVoucher.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSlAditionVoucher.Properties.DataSource = value
        End Set
    End Property

    Public Property IntangibleAmortizationListAccountingVoucher As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingFixedAsset.IntangibleAmortizationListAccountingVoucher
        Get
            Return CType(INDSleIntangibleAmortizationVoucher.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleIntangibleAmortizationVoucher.Properties.DataSource = value
        End Set
    End Property

    Public Property OutputListAccountingVoucher As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingFixedAsset.OutputListAccountingVoucher
        Get
            Return CType(INDSlOutputVoucher.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSlOutputVoucher.Properties.DataSource = value
        End Set
    End Property

    Public Property DonationIngressAccount As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingFixedAsset.DonationIngressAccount
        Get
            Return CType(INDSlDonationIngressAccount.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSlDonationIngressAccount.Properties.DataSource = value
        End Set
    End Property

    Public Property OtherConceptIngressAccount As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingFixedAsset.OtherConceptIngressAccount
        Get
            Return CType(INDSlOtherConceptIngressAccount.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSlOtherConceptIngressAccount.Properties.DataSource = value
        End Set
    End Property

    Public Property OtherIngressAccountingAccount As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingFixedAsset.OtherIngressAccountingAccount
        Get
            Return CType(INDSlOtherIngressAccountingAccount.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSlOtherIngressAccountingAccount.Properties.DataSource = value
        End Set
    End Property

    Public Property RecuperationIngressAccount As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingFixedAsset.RecuperationIngressAccount
        Get
            Return CType(INDSlRecuperationIngressAccount.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSlRecuperationIngressAccount.Properties.DataSource = value
        End Set
    End Property

    Public Property TransferOfPropertyIngressAccount As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingFixedAsset.TransferOfPropertyIngressAccount
        Get
            Return CType(INDSlTransferOfPropertyIngressAccount.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSlTransferOfPropertyIngressAccount.Properties.DataSource = value
        End Set
    End Property

    Public Property ThirdPartyXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingFixedAsset.ThirdParty
        Get
            Return CType(INDSlActiveChief.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSlActiveChief.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del tipo de comprobante contable de Valorización/Desvalorización
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ValorizationDevaluationAccountingVoucherXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISettingFixedAsset.ValorizationDevaluationAccountingVoucherXpo
        Get
            Return INDsleValorizationDevaluationAccountingVoucher.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleValorizationDevaluationAccountingVoucher.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del libro oficial
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LegalBookXpo As XPInstantFeedbackSource Implements ISettingFixedAsset.LegalBookXpo
        Get
            Return INDsleLegalBook.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleLegalBook.Properties.DataSource = value
        End Set
    End Property

#End Region
#End Region

#Region "Const"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "FixedAssets"

#End Region

#Region "Variables"

    ''' <summary>
    ''' Representa a la entidad de parámetros de cxp
    ''' </summary>
    Public PaymentsSettingPaymentsXpo As PaymentsSettingPaymentsXpo

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private _record As BlockRecordFixedAsset

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Variable para poder acceder al Modelo
    ''' </summary>
    Private Model As New MSettingFixedAsset(Me.MyTag)

    Private SettingFixedAsset As SettingFixedAsset

    Private Presenter As PSettingFixedAsset

    ''' <summary>
    ''' Bandera para search de unidad radicacion (True=Consulta, False=No Consulta)
    ''' </summary>
    ''' <remarks></remarks>
    Private banFilingUnit As Boolean = True

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Private ListIVARetention As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Private listYesNo As New List(Of Tuple(Of Boolean, String))

    ''' <summary>
    ''' Representa a la entidad del detalle de la configuracion cuando se esta editando
    ''' </summary>
    ''' <remarks></remarks>
    Private SettingFixedAssetByLegalBook As SettingFixedAssetByLegalBook

    ''' <summary>
    ''' Listado de detalles de la configuración
    ''' </summary>
    ''' <remarks></remarks>
    Private ListSettingFixedAssetByLegalBook As List(Of SettingFixedAssetByLegalBook)

    ''' <summary>
    ''' Listado de detalles de la configuración a eliminar
    ''' </summary>
    ''' <remarks></remarks>
    Private ListDeleteSettingFixedAssetByLegalBook As List(Of SettingFixedAssetByLegalBook)

    ''' <summary>
    ''' Permite saber si esta en modo de edición para el popup
    ''' (True=Edita, False=Guarda)
    ''' </summary>
    ''' <remarks></remarks>
    Private FlagEditMode As Boolean

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
        If Not ValidateControls() Then
            Exit Sub
        End If

        If INDlygDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If ListSettingFixedAssetByLegalBook Is Nothing OrElse ListSettingFixedAssetByLegalBook.Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe ingresar al menos un detalle."
                Exit Sub
            End If
        End If

        AssigningValues()

        Using model As New MSettingFixedAsset(Me.Tag.ToString())
            AsyncLoader(True)
            Dim Result = Await model.SaveParameters(SettingFixedAsset)
            AsyncLoader(False)

            If Result.StateResult Then
                If SettingFixedAsset.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                ElseIf SettingFixedAsset.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                End If
                Me.SettingFixedAsset = Result.ObjectEmbbeded
                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                AsyncLoader(False)

                DeleteBlockedRecord()
                CleanControls()
                LoadControls()
                IndDeProcessDate.Focus()

                Me.BarraBotones.CleanAuditBasic()
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
            Else
                If Result.MessageResult(0) = ErrorConcurrencia Then
                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                Else
                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                End If
            End If
        End Using
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
        Model = Nothing
        SettingFixedAsset = Nothing
        Presenter = Nothing
        banFilingUnit = Nothing
        ListIVARetention = Nothing
        listYesNo = Nothing
        SettingFixedAssetByLegalBook = Nothing
        ListSettingFixedAssetByLegalBook = Nothing
        ListDeleteSettingFixedAssetByLegalBook = Nothing
        FlagEditMode = Nothing
    End Sub

    ''' <summary>
    ''' Evento load del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmSettingFixedAsset_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDLySettingFixedAsset, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        Presenter = New PSettingFixedAsset(Me)

        IndigoGridControl1.RefreshGrid(INDgcDetail)
        IndigoGridView1.SetListAcction(ViewDetail, {eAcciones.Remove, eAcciones.Edit}.ToList())
        Presenter.InitializeRefundAccountPayableConceptNote()
        InitializeTuple()
        Deshacer()
        Await LoadPaymentsSetting()
        LoadControls()
    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento cerrar del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmSettingFixedAsset_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "ButtonClick"

    Private Sub INDSlIngressVoucher_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDSlIngressVoucher.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(607, Nothing, True)
            Using model As New MSettingFixedAsset(Me.MyTag)
                IngressListAccountingVoucher = model.ListJournalVoucher()
            End Using
        End If
    End Sub

    Private Sub INDSlDepreciationVoucher_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDSlDepreciationVoucher.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(607, Nothing, True)
            Using model As New MSettingFixedAsset(Me.MyTag)
                DepreciationListAccountingVoucher = model.ListJournalVoucher()
            End Using
        End If
    End Sub

    Private Sub INDSlAditionVoucher_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDSlAditionVoucher.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(607, Nothing, True)
            Using model As New MSettingFixedAsset(Me.MyTag)
                AditionListAccountingVoucher = model.ListJournalVoucher()
            End Using
        End If
    End Sub

    Private Sub INDSlOutputVoucher_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDSlOutputVoucher.ButtonClick, INDSleIntangibleAmortizationVoucher.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(607, Nothing, True)
            Using model As New MSettingFixedAsset(Me.MyTag)
                OutputListAccountingVoucher = model.ListJournalVoucher()
            End Using
        End If
    End Sub

    Private Sub INDSlOtherIngressAccountingAccount_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDSlOtherIngressAccountingAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            Using model As New MSettingFixedAsset(Me.MyTag)
                OtherIngressAccountingAccount = model.ListAccounts()
            End Using
        End If
    End Sub

    Private Sub INDSlDonationIngressAccount_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDSlDonationIngressAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            Using model As New MSettingFixedAsset(Me.MyTag)
                DonationIngressAccount = model.ListAccounts()
            End Using
        End If
    End Sub

    Private Sub INDSlTransferOfPropertyIngressAccount_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDSlTransferOfPropertyIngressAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            Using model As New MSettingFixedAsset(Me.MyTag)
                TransferOfPropertyIngressAccount = model.ListAccounts()
            End Using
        End If
    End Sub

    Private Sub INDSlOtherConceptIngressAccount_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDSlOtherConceptIngressAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            Using model As New MSettingFixedAsset(Me.MyTag)
                OtherConceptIngressAccount = model.ListAccounts()
            End Using
        End If
    End Sub

    Private Sub INDSlRecuperationIngressAccount_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDSlRecuperationIngressAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            Using model As New MSettingFixedAsset(Me.MyTag)
                RecuperationIngressAccount = model.ListAccounts()
            End Using
        End If
    End Sub

    Private Sub INDsleValorizationDevaluationAccountingVoucher_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleValorizationDevaluationAccountingVoucher.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(607, Nothing, True)
            Using model As New MSettingFixedAsset(Me.MyTag)
                ValorizationDevaluationAccountingVoucherXpo = model.ListJournalVoucher()
            End Using
        End If
    End Sub

    Private Async Sub INDsleFilingUnit_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleFilingUnit.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("723", "", True)
            Await InitializeFilingUnit()
        End If
    End Sub

    Private Sub INDsleServiceMainAccount_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleServiceMainAccount.ButtonClick
        If e.Button.Kind = ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            Presenter.InitializeServiceMainAccount()
        End If
    End Sub

    Private Sub INDsleIVAFreightAccountPayableConcept_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleIVAFreightAccountPayableConcept.ButtonClick
        If e.Button.Kind = ButtonPredefines.Plus Then
            OpenForm(725, Nothing, True)
            Presenter.InitializeIVAFreightAccountPayableConcept()
        End If
    End Sub

    Private Sub INDsleFreightAccountPayableConcept_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleFreightAccountPayableConcept.ButtonClick
        If e.Button.Kind = ButtonPredefines.Plus Then
            OpenForm(725, Nothing, True)
            Presenter.InitializeFreightAccountPayableConcept()
        End If
    End Sub

    Private Sub INDsleIVAAccountPayableConcept_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleIVAAccountPayableConcept.ButtonClick
        If e.Button.Kind = ButtonPredefines.Plus Then
            OpenForm(725, Nothing, True)
            Presenter.InitializeIVAAccountPayableConcept()
        End If
    End Sub

    Private Sub INDsleIVARetentionAccountPayableConcept_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleIVARetentionAccountPayableConcept.ButtonClick
        If e.Button.Kind = ButtonPredefines.Plus Then
            OpenForm(725, Nothing, True)
            Presenter.InitializeIVARetentionAccountPayableConcept()
        End If
    End Sub

    Private Sub INDsleTransferJournalVoucher_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleTransferJournalVoucher.ButtonClick
        If e.Button.Kind = ButtonPredefines.Plus Then
            OpenForm(607, Nothing, True)
            Using model As New MSettingFixedAsset(Me.MyTag)
                TransferJournalVoucherXpo = model.ListJournalVoucher()
            End Using
        End If
    End Sub

    Private Sub INDsleReclassificationJournalVoucher_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleReclassificationJournalVoucher.ButtonClick
        If e.Button.Kind = ButtonPredefines.Plus Then
            OpenForm(607, Nothing, True)
            Using model As New MSettingFixedAsset(Me.MyTag)
                ReclassificationJournalVoucherXpo = model.ListJournalVoucher()
            End Using
        End If
    End Sub

    Private Sub INDsleDevolutionJournalVoucher_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleDevolutionJournalVoucher.ButtonClick
        If e.Button.Kind = ButtonPredefines.Plus Then
            OpenForm(607, Nothing, True)
            Using model As New MSettingFixedAsset(Me.MyTag)
                DevolutionJournalVoucherXpo = model.ListJournalVoucher()
            End Using
        End If
    End Sub

    Private Sub INDsleSalesMainAccount_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleSalesMainAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            Using model As New MSettingFixedAsset(Me.MyTag)
                INDsleSalesMainAccount.Properties.DataSource = model.ListAccounts()
            End Using
        End If
    End Sub

    Private Sub INDsleReplacementMainAccount_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleReplacementMainAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            Using model As New MSettingFixedAsset(Me.MyTag)
                INDsleReplacementMainAccount.Properties.DataSource = model.ListAccounts()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que abre el form de libro oficial
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleLegalBook_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleLegalBook.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1682, Nothing, True)
            Presenter.InitializeLegalBook()
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton de agregar del popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddDetail_Click(sender As Object, e As EventArgs) Handles INDbtnAddDetail.Click
        AddDetail()
    End Sub

#End Region


#Region "QueryPopUp"

    Private Sub HandleQueryPopUp(control As DevExpress.XtraEditors.SearchLookUpEdit, ByRef dataSource As XPInstantFeedbackSource)
        If control.Properties.DataSource Is Nothing Then
            Using model As New MSettingFixedAsset(Me.MyTag)
                dataSource = model.ListJournalVoucher()
            End Using
        End If
    End Sub

    Private Sub INDsleTransferJournalVoucher_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleTransferJournalVoucher.QueryPopUp
        HandleQueryPopUp(INDsleTransferJournalVoucher, TransferJournalVoucherXpo)
    End Sub

    Private Sub INDsleReclassificationJournalVoucher_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleReclassificationJournalVoucher.QueryPopUp
        HandleQueryPopUp(INDsleReclassificationJournalVoucher, ReclassificationJournalVoucherXpo)
    End Sub

    Private Sub INDsleDevolutionJournalVoucher_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleDevolutionJournalVoucher.QueryPopUp
        HandleQueryPopUp(INDsleDevolutionJournalVoucher, DevolutionJournalVoucherXpo)
    End Sub

    Private Sub INDSlIngressVoucher_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlIngressVoucher.QueryPopUp
        HandleQueryPopUp(INDSlIngressVoucher, IngressListAccountingVoucher)
    End Sub

    Private Sub INDSlDepreciationVoucher_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlDepreciationVoucher.QueryPopUp
        HandleQueryPopUp(INDSlDepreciationVoucher, DepreciationListAccountingVoucher)
    End Sub

    Private Sub INDSlAditionVoucher_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlAditionVoucher.QueryPopUp
        HandleQueryPopUp(INDSlAditionVoucher, AditionListAccountingVoucher)
    End Sub

    Private Sub INDSleIntangibleAmortizationVoucher_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleIntangibleAmortizationVoucher.QueryPopUp
        HandleQueryPopUp(INDSleIntangibleAmortizationVoucher, IntangibleAmortizationListAccountingVoucher)
    End Sub

    Private Sub INDSlOutputVoucher_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlOutputVoucher.QueryPopUp
        HandleQueryPopUp(INDSlOutputVoucher, OutputListAccountingVoucher)
    End Sub

    Private Sub INDsleValorizationDevaluationAccountingVoucher_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleValorizationDevaluationAccountingVoucher.QueryPopUp
        HandleQueryPopUp(INDsleValorizationDevaluationAccountingVoucher, ValorizationDevaluationAccountingVoucherXpo)
    End Sub

    Private Sub INDSlOtherIngressAccountingAccount_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlOtherIngressAccountingAccount.QueryPopUp
        If INDSlOtherIngressAccountingAccount.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If INDSlOtherIngressAccountingAccount.Properties.DataSource Is Nothing Then
            Using model As New MSettingFixedAsset(Me.MyTag)
                OtherIngressAccountingAccount = model.ListAccounts()
            End Using
        End If
    End Sub

    Private Sub INDSlDonationIngressAccount_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlDonationIngressAccount.QueryPopUp
        If INDSlDonationIngressAccount.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If INDSlDonationIngressAccount.Properties.DataSource Is Nothing Then
            Using model As New MSettingFixedAsset(Me.MyTag)
                DonationIngressAccount = model.ListAccounts()
            End Using
        End If
    End Sub

    Private Sub INDSlTransferOfPropertyIngressAccount_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlTransferOfPropertyIngressAccount.QueryPopUp
        If INDSlTransferOfPropertyIngressAccount.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If INDSlTransferOfPropertyIngressAccount.Properties.DataSource Is Nothing Then
            Using model As New MSettingFixedAsset(Me.MyTag)
                TransferOfPropertyIngressAccount = model.ListAccounts()
            End Using
        End If
    End Sub

    Private Sub INDSlOtherConceptIngressAccount_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlOtherConceptIngressAccount.QueryPopUp
        If INDSlOtherConceptIngressAccount.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If INDSlOtherConceptIngressAccount.Properties.DataSource Is Nothing Then
            Using model As New MSettingFixedAsset(Me.MyTag)
                OtherConceptIngressAccount = model.ListAccounts()
            End Using
        End If
    End Sub

    Private Sub INDSlRecuperationIngressAccount_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlRecuperationIngressAccount.QueryPopUp
        If INDSlRecuperationIngressAccount.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If INDSlRecuperationIngressAccount.Properties.DataSource Is Nothing Then
            Using model As New MSettingFixedAsset(Me.MyTag)
                RecuperationIngressAccount = model.ListAccounts()
            End Using
        End If
    End Sub

    Private Sub INDSlActiveChief_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlActiveChief.QueryPopUp
        If INDSlActiveChief.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If INDSlActiveChief.Properties.DataSource Is Nothing Then
            Using model As New MSettingFixedAsset(Me.MyTag)
                ThirdPartyXpo = model.ListThirdParty()
            End Using
        End If
    End Sub

    Private Sub INDsleServiceMainAccount_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleServiceMainAccount.QueryPopUp
        If ServiceMainAccountXpo Is Nothing Then
            Presenter.InitializeServiceMainAccount()
        End If
    End Sub

    Private Sub INDsleIVAFreightAccountPayableConcept_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleIVAFreightAccountPayableConcept.QueryPopUp
        If IVAFreightAccountPayableConceptXpo Is Nothing Then
            Presenter.InitializeIVAFreightAccountPayableConcept()
        End If
    End Sub

    Private Sub INDsleFreightAccountPayableConcept_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleFreightAccountPayableConcept.QueryPopUp
        If FreightAccountPayableConceptXpo Is Nothing Then
            Presenter.InitializeFreightAccountPayableConcept()
        End If
    End Sub

    Private Sub INDsleIVAAccountPayableConcept_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleIVAAccountPayableConcept.QueryPopUp
        If IVAAccountPayableConceptXpo Is Nothing Then
            Presenter.InitializeIVAAccountPayableConcept()
        End If
    End Sub

    Private Sub INDsleIVARetentionAccountPayableConcept_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleIVARetentionAccountPayableConcept.QueryPopUp
        If IVARetentionAccountPayableConceptXpo Is Nothing Then
            Presenter.InitializeIVARetentionAccountPayableConcept()
        End If
    End Sub

    Private Sub INDsleSalesMainAccount_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleSalesMainAccount.QueryPopUp
        If INDsleSalesMainAccount.Properties.DataSource Is Nothing Then
            Using model As New MSettingFixedAsset(Me.MyTag)
                INDsleSalesMainAccount.Properties.DataSource = model.ListAccounts()
            End Using
        End If
    End Sub

    Private Sub INDsleReplacementMainAccount_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleReplacementMainAccount.QueryPopUp
        If INDsleReplacementMainAccount.Properties.DataSource Is Nothing Then
            Using model As New MSettingFixedAsset(Me.MyTag)
                INDsleReplacementMainAccount.Properties.DataSource = model.ListAccounts()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de libro oficial
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleLegalBook_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleLegalBook.QueryPopUp
        If LegalBookXpo Is Nothing Then
            Presenter.InitializeLegalBook()
        End If
    End Sub

    '' <summary>
    '' Metodo para mostrar los datos de de la moneda
    '' </summary>
    '' <param name="sender"></param>
    '' <param name="e"></param>
    Private Sub INDSlCurrency_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlCurrency.QueryPopUp
        If ListCurrency Is Nothing Then
            Presenter.GetListCurrency()
        End If
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmSettingFixedAsset_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        IndDeProcessDate.Focus()
        Await InitializeFilingUnit()
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDsleFilingUnit_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleFilingUnit.EditValueChanged
        If FilingUnitId IsNot Nothing AndAlso banFilingUnit = True Then
            INDsleFilingUnit.ValidateFilingUnit()
        End If
    End Sub

    Private Sub INDsleIVARetention_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleIVARetention.EditValueChanged
        If IVARetention IsNot Nothing Then
            If IVARetention = 1 Then 'Tercero
                INDlyItemIVARetentionAccountPayableConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemIVARetentionAccountPayableConcept.AllowHide = True
            Else 'Concepto
                INDlyItemIVARetentionAccountPayableConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemIVARetentionAccountPayableConcept.AllowHide = False
            End If
        End If
    End Sub

    Private Sub INDSlCurrency_EditValueChanged(sender As Object, e As EventArgs) Handles INDSlCurrency.EditValueChanged
        If INDSlCurrency.EditValue Is Nothing OrElse Me.CurrencyId = 0 Then
            Exit Sub
        End If

        If Not String.IsNullOrEmpty(Me.CurrencyAbbreviation) Then
            Me.SetCurrencyUI(Me.CurrencyAbbreviation)
        End If
    End Sub

#End Region

#Region "MenuContext"

    ''' <summary>
    ''' Despliega los botones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.SimpleButton
        btn = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        Select Case btn.Tag.ToString
            Case "Edit"
                EditDetail()
            Case "Remove"
                DeleteDetail()
        End Select
    End Sub

    ''' <summary>
    ''' Accion de click derecho del mouse
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Edit"
                EditDetail()
            Case "Remove"
                DeleteDetail()
        End Select
    End Sub

#End Region

#Region "KeyDown"
    ''' <summary>
    ''' Evento que se dispara al presionar enter sobre el control de popupContainerEdit
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceDetail_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDpceDetail.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter OrElse e.KeyCode = System.Windows.Forms.Keys.F4 Then
            INDpceDetail.ShowPopup()
            INDsleLegalBook.Focus()
        End If
    End Sub
#End Region

#Region "CloseUp"

    ''' <summary>
    ''' Evento que se dispara al cerrar el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceDetail_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDpceDetail.CloseUp
        If FlagEditMode = True Then
            CleanControlsPopup()
        End If
    End Sub

#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' Establece el formato de moneda en los controles de valores cuando cambia la moneda seleccionada
    ''' </summary>
    ''' <param name="_currencyAbbreviation"></param>
    Private Sub SetCurrencyUI(_currencyAbbreviation As String)

        If String.IsNullOrEmpty(_currencyAbbreviation) Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CurrencyAbbreviationEmpty")
            Exit Sub
        End If

        Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        _culture.NumberFormat = _currencyAbbreviation.GetNumberFormat

        Me.INDTxtValueMinimum.Properties.Mask.Culture = _culture
        Me.INDTxtTopValue.Properties.Mask.Culture = _culture
    End Sub

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
    ''' Inicializa los search que van quemados
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeTuple()
        ListIVARetention = New List(Of Tuple(Of Integer, String))
        ListIVARetention.Add(New Tuple(Of Integer, String)(1, "Tercero"))
        ListIVARetention.Add(New Tuple(Of Integer, String)(2, "Concepto"))
        INDsleIVARetention.Properties.DataSource = ListIVARetention.ToList

        listYesNo = New List(Of Tuple(Of Boolean, String))
        listYesNo.Add(New Tuple(Of Boolean, String)(True, "Si"))
        listYesNo.Add(New Tuple(Of Boolean, String)(False, "No"))
        INDsleIvaCost.Properties.DataSource = listYesNo.ToList
        INDsleHandlesMinimumAmount.Properties.DataSource = listYesNo.ToList
        INDGleCommitmentBudgetInterface.Properties.DataSource = listYesNo.ToList()
        INDsleTaxCostBuy.Properties.DataSource = listYesNo.ToList()
        INDRgDepreciation.Properties.DataSource = listYesNo.ToList()
        INDSleAppliesCatalogPropertyandServices.Properties.DataSource = listYesNo.ToList()
    End Sub

    ''' <summary>
    ''' Metodo que inicializa el datasource del control de unidad de radicación
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function InitializeFilingUnit() As Task
        Using model As New MFilingUnit(Tag)
            Dim x As ActionResult(Of List(Of FilingUnit)) = Await model.GetFilingUnitByUser(indigo.UserIndigo)
            Dim listFilingUnit As List(Of FilingUnit) = x.ObjectEmbbeded
            FilingUnitXpo = listFilingUnit
        End Using
    End Function

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
        ActionsOnControls = False

        IndDeProcessDate.EditValue = Nothing
        IndDeProcessDate.Enabled = True

        CurrencyId = Nothing
        INDSlCurrency.Properties.NullText = String.Empty

        AppliesCatalogPropertyandServices = False

        IdIngressAccountingVoucher = Nothing
        INDSlIngressVoucher.Properties.NullText = String.Empty

        IdDepreciationAccountingVoucher = Nothing
        INDSlDepreciationVoucher.Properties.NullText = String.Empty

        IdAditionAccountingVoucher = Nothing
        INDSlAditionVoucher.Properties.NullText = String.Empty

        IdOutputAccountingVoucher = Nothing
        INDSlOutputVoucher.Properties.NullText = String.Empty

        IdIntangibleAmortizationVoucher = Nothing
        INDSleIntangibleAmortizationVoucher.Properties.NullText = String.Empty

        ValorizationDevaluationAccountingVoucherId = Nothing
        INDsleValorizationDevaluationAccountingVoucher.Properties.NullText = String.Empty

        IdTransferJournalVoucherId = Nothing
        INDsleTransferJournalVoucher.Properties.NullText = String.Empty

        IdReclassificationJournalVoucherId = Nothing
        INDsleReclassificationJournalVoucher.Properties.NullText = String.Empty

        DevolutionJournalVoucherId = Nothing
        INDsleDevolutionJournalVoucher.Properties.NullText = String.Empty

        IdOtherIngressAccountingAccount = Nothing
        INDSlOtherIngressAccountingAccount.Properties.NullText = String.Empty

        IdDonationAccountingAccount = Nothing
        INDSlDonationIngressAccount.Properties.NullText = String.Empty

        IdTransferPropertyAccountingAccount = Nothing
        INDSlTransferOfPropertyIngressAccount.Properties.NullText = String.Empty

        IdOtherConceptsAccountingAccount = Nothing
        INDSlOtherConceptIngressAccount.Properties.NullText = String.Empty

        IdRecuperationAccountingAccount = Nothing
        INDSlRecuperationIngressAccount.Properties.NullText = String.Empty

        SalesMainAccountId = Nothing
        INDsleSalesMainAccount.Properties.NullText = String.Empty

        ReplacementMainAccountId = Nothing
        INDsleReplacementMainAccount.Properties.NullText = String.Empty

        LowBidAmount = Nothing
        TopMinorValue = Nothing
        IvaCost = Nothing
        INDSlActiveChief.Properties.NullText = String.Empty
        FixedAssetChief = Nothing

        FilingUnitId = Nothing

        ServiceMainAccountId = Nothing
        INDsleServiceMainAccount.Properties.NullText = String.Empty

        IVAFreightAccountPayableConceptId = Nothing
        INDsleIVAFreightAccountPayableConcept.Properties.NullText = String.Empty

        FreightAccountPayableConceptId = Nothing
        INDsleFreightAccountPayableConcept.Properties.NullText = String.Empty

        IVARetention = Nothing

        IVARetentionAccountPayableConceptId = Nothing
        INDsleIVARetentionAccountPayableConcept.Properties.NullText = String.Empty

        IVAAccountPayableConceptId = Nothing
        INDsleIVAAccountPayableConcept.Properties.NullText = String.Empty

        INDlyItemIVARetentionAccountPayableConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemIVARetentionAccountPayableConcept.AllowHide = True
        INDsleRefundAccountPayableConceptNoteId.EditValue = Nothing

        CommitmentBudgetInterface = False
        TaxCostBuy = False
        Depreciation30Days = False
        INDgcDetail.DataSource = Nothing
        ListSettingFixedAssetByLegalBook = Nothing
        ListDeleteSettingFixedAssetByLegalBook = Nothing
        CleanControlsPopup()
    End Sub

    ''' <summary>
    ''' Esta propiedad establece si los controles estan o no habilitados
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements ISettingFixedAsset.ActionsOnControls
        Set(value As Boolean)
            INDLySettingFixedAsset.BeginUpdate()

            'INDpceDetail.Enabled = value
            'INDgcDetail.Enabled = value

            INDLySettingFixedAsset.EndUpdate()
        End Set
    End Property

    ''' <summary>
    ''' Deletes the blocked record.
    ''' </summary>
    Public Async Sub DeleteBlockedRecord()
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
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
    Private Async Sub LoadControls()
        Me.BarraBotones.StatusRecordVisible = True
        Using Model As New MSettingFixedAsset(CStr(Me.Tag))
            AsyncLoader(True)
            SettingFixedAsset = Await Model.GetSettingFixedAssetByOperatingUnitId(_idOperativeUnit)
            AsyncLoader(False)
            If Not SettingFixedAsset Is Nothing Then
                If SettingFixedAsset.Id > 0 Then
                    Using ModelRecord As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
                        Dim result = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(SettingFixedAsset.Id))
                        With SettingFixedAsset
                            LayoutControls.SetCustomFieldsValue(.CustomProperties)

                            Me.BarraBotones.CleanAuditBasic()

                            IndDeProcessDate.EditValue = .ProcessDate
                            IndDeProcessDate.Enabled = False

                            CurrencyId = .CurrencyId
                            INDSlCurrency.Properties.NullText = .Currency.Abbreviation

                            If Not .CurrencyFieldEnabled Then
                                INDSlCurrency.Properties.ReadOnly = True
                            Else
                                INDSlCurrency.Properties.ReadOnly = False
                            End If

                            ' Aplicar formato de moneda a los campos de valores
                            If CurrencyId IsNot Nothing AndAlso CurrencyId > 0 AndAlso Not String.IsNullOrEmpty(Me.CurrencyAbbreviation) Then
                                Me.SetCurrencyUI(Me.CurrencyAbbreviation)
                            End If

                            AppliesCatalogPropertyandServices = .AppliesCatalogPropertyandServices

                            IdIngressAccountingVoucher = .IdIngressAccountingVoucher
                            INDSlIngressVoucher.Properties.NullText = .CodeNameIngressAccountingVoucher

                            IdDepreciationAccountingVoucher = .IdDepreciationAccountingVoucher
                            INDSlDepreciationVoucher.Properties.NullText = .CodeNameDepreciationAccountingVoucher

                            IdIntangibleAmortizationVoucher = .IdIntangibleAssetAmortizationVoucher
                            INDSleIntangibleAmortizationVoucher.Properties.NullText = .CodeNameIntangibleAssetAmortizationVoucher

                            IdOutputAccountingVoucher = .IdOutputAccountingVoucher
                            INDSlOutputVoucher.Properties.NullText = .CodeNameOutputAccountingVoucher

                            ValorizationDevaluationAccountingVoucherId = .IdValorizationDevaluationAccountingVoucher
                            INDsleValorizationDevaluationAccountingVoucher.Properties.NullText = .CodeNameValorizationDevaluationAccountingVoucher

                            IdTransferJournalVoucherId = .TransferJournalVoucherId
                            INDsleTransferJournalVoucher.Properties.NullText = .TransferJournalVoucherCodeName

                            IdReclassificationJournalVoucherId = .ReclassificationJournalVoucherId
                            INDsleReclassificationJournalVoucher.Properties.NullText = .ReclassificationJournalVoucherCodeName

                            DevolutionJournalVoucherId = .DevolutionJournalVoucherId
                            INDsleDevolutionJournalVoucher.Properties.NullText = .DevolutionJournalVoucherCodeName

                            IdOtherIngressAccountingAccount = .OtherIngressMainAccountId
                            INDSlOtherIngressAccountingAccount.Properties.NullText = .NumberNameOtherIngressAccountingAccount

                            IdDonationAccountingAccount = .DonationMainAccountId
                            INDSlDonationIngressAccount.Properties.NullText = .NumberNameDonationAccountingAccount

                            IdTransferPropertyAccountingAccount = .TransferPropertyMainAccountId
                            INDSlTransferOfPropertyIngressAccount.Properties.NullText = .NumberNameTransferPropertyAccountingAccount

                            IdOtherConceptsAccountingAccount = .OtherConceptsMainAccountId
                            INDSlOtherConceptIngressAccount.Properties.NullText = .NumberNameOtherConceptsAccountingAccount

                            IdRecuperationAccountingAccount = .RecuperationMainAccountId
                            INDSlRecuperationIngressAccount.Properties.NullText = .NumberNameRecuperationAccountingAccount

                            SalesMainAccountId = .SalesMainAccountId
                            INDsleSalesMainAccount.Properties.NullText = .SalesMainAccountCodeName

                            ReplacementMainAccountId = .ReplacementMainAccountId
                            INDsleReplacementMainAccount.Properties.NullText = .ReplacementMainAccountCodeName

                            LowBidAmount = .LowBidAmount
                            TopMinorValue = .TopMinorValue
                            IvaCost = .IvaCost
                            INDSlActiveChief.Properties.NullText = .NameResponsible
                            FixedAssetChief = .IdThirdPartyResponsible

                            banFilingUnit = False
                            FilingUnitId = .FilingUnitId
                            banFilingUnit = True

                            ServiceMainAccountId = .ServiceMainAccountId
                            INDsleServiceMainAccount.Properties.NullText = .ServiceMainAccountNumberName

                            IVAFreightAccountPayableConceptId = .IVAFreightAccountPayableConceptId
                            INDsleIVAFreightAccountPayableConcept.Properties.NullText = .IVAFreightAccountPayableConceptCodeName

                            FreightAccountPayableConceptId = .FreightAccountPayableConceptId
                            INDsleFreightAccountPayableConcept.Properties.NullText = .FreightAccountPayableConceptCodeName

                            IVARetention = .IVARetention
                            CommitmentBudgetInterface = .CommitmentBudgetInterface
                            TaxCostBuy = .TaxCostBuy
                            Depreciation30Days = .Depreciation30Days
                            IVARetentionAccountPayableConceptId = .IVARetentionAccountPayableConceptId
                            INDsleIVARetentionAccountPayableConcept.Properties.NullText = .IVARetentionAccountPayableConceptCodeName

                            IVAAccountPayableConceptId = .IVAAccountPayableConceptId
                            INDsleIVAAccountPayableConcept.Properties.NullText = .IVAAccountPayableConceptCodeName

                            INDsleRefundAccountPayableConceptNoteId.EditValue = .RefundAccountPayableConceptNoteId

                            ListSettingFixedAssetByLegalBook = .SettingFixedAssetByLegalBook.ToList
                            INDgcDetail.DataSource = Nothing
                            INDgcDetail.DataSource = ListSettingFixedAssetByLegalBook
                        End With

                        Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.SettingFixedAsset.Id)
                        If result.Id = 0 Then
                            Dim state = New Domain.Base.Entities.ObjectChangeTracker
                            state.State = Domain.Base.Entities.ObjectState.Added
                            _record = New BlockRecordFixedAsset With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = CInt(Me.Tag), .CodUser = Me.indigo.UserIndigo, .IdRecord = SettingFixedAsset.Id}
                            Dim operation = Await ModelRecord.SaveBlockRecord(_record)
                            _record = operation.ObjectEmbbeded
                        Else
                            Dim xtraMessage As String = String.Format(ResourceManager.GetString("RecordLocked"), result.CodUser, result.NameUser, result.BlockDate)
                            _record = result
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                        End If
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
                        Me.BarraBotones.SetDocuments(SettingFixedAsset.Id)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
                        ActionsOnControls = True
                    End Using
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                    BarraBotones.PrepareToolbar(eAction.OnlySave)
                    CleanControls()
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
                    IndDeProcessDate.Enabled = True
                    ProcessDate = GetDateServer()
                End If
            Else
                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                BarraBotones.PrepareToolbar(eAction.OnlySave)
                CleanControls()
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
                IndDeProcessDate.Enabled = True
                ProcessDate = GetDateServer()
            End If
        End Using
        IndDeProcessDate.Focus()
    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        If SettingFixedAsset Is Nothing Then
            SettingFixedAsset = New SettingFixedAsset
        End If

        With SettingFixedAsset
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .ProcessDate = ProcessDate
            .CurrencyId = CurrencyId
            .AppliesCatalogPropertyandServices = AppliesCatalogPropertyandServices
            .IdIngressAccountingVoucher = IdIngressAccountingVoucher
            .IdDepreciationAccountingVoucher = IdDepreciationAccountingVoucher
            .IdIntangibleAssetAmortizationVoucher = IdIntangibleAmortizationVoucher
            .IdOutputAccountingVoucher = IdOutputAccountingVoucher
            .TransferJournalVoucherId = IdTransferJournalVoucherId
            .ReclassificationJournalVoucherId = IdReclassificationJournalVoucherId
            .DevolutionJournalVoucherId = DevolutionJournalVoucherId
            .OtherIngressMainAccountId = IdOtherIngressAccountingAccount
            .DonationMainAccountId = IdDonationAccountingAccount
            .TransferPropertyMainAccountId = IdTransferPropertyAccountingAccount
            .OtherConceptsMainAccountId = IdOtherConceptsAccountingAccount
            .RecuperationMainAccountId = IdRecuperationAccountingAccount
            .SalesMainAccountId = SalesMainAccountId
            .ReplacementMainAccountId = ReplacementMainAccountId
            .LowBidAmount = LowBidAmount
            .TopMinorValue = TopMinorValue
            .IvaCost = IvaCost
            .IdThirdPartyResponsible = FixedAssetChief
            .IdValorizationDevaluationAccountingVoucher = ValorizationDevaluationAccountingVoucherId
            .OperatingUnitId = _idOperativeUnit
            .RefundAccountPayableConceptNoteId = INDsleRefundAccountPayableConceptNoteId.EditValue
            .FilingUnitId = FilingUnitId
            .ServiceMainAccountId = ServiceMainAccountId
            .IVAFreightAccountPayableConceptId = IVAFreightAccountPayableConceptId
            .FreightAccountPayableConceptId = FreightAccountPayableConceptId
            .IVARetention = IVARetention
            .CommitmentBudgetInterface = CommitmentBudgetInterface
            .TaxCostBuy = TaxCostBuy
            .Depreciation30Days = Depreciation30Days

            If INDlyItemIVARetentionAccountPayableConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .IVARetentionAccountPayableConceptId = IVARetentionAccountPayableConceptId
            Else
                .IVARetentionAccountPayableConceptId = Nothing
            End If
            .IVAAccountPayableConceptId = IVAAccountPayableConceptId

            If INDlygDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If ListSettingFixedAssetByLegalBook IsNot Nothing AndAlso ListSettingFixedAssetByLegalBook.Any() Then
                    ListSettingFixedAssetByLegalBook.ForEach(Sub(item)
                                                                 .SettingFixedAssetByLegalBook.Add(item)
                                                             End Sub)
                End If

                If ListDeleteSettingFixedAssetByLegalBook IsNot Nothing AndAlso ListDeleteSettingFixedAssetByLegalBook.Any() Then
                    ListDeleteSettingFixedAssetByLegalBook.ForEach(Sub(item)
                                                                       .SettingFixedAssetByLegalBook.Add(item)
                                                                   End Sub)
                End If
            End If

            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    ''' <summary>
    ''' metodo para mostrar los formulario en el evento buttonclik
    ''' </summary>
    ''' <param name="form"></param>
    ''' <remarks></remarks>
    Private Sub OpenFormDialog(form As FormBase)
        form.ViewModeEditHold = True
        form.Size = New Size(800, 700)
        form.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        form.MaximizeBox = False
        form.MinimizeBox = False
        Dim transparent = New FrmTransparent(form, False)
        transparent.ShowDialog(Me)
    End Sub

    ''' <summary>
    ''' Edita el detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EditDetail()
        SettingFixedAssetByLegalBook = CType(ViewDetail.GetFocusedRow, SettingFixedAssetByLegalBook)
        FlagEditMode = True
        With SettingFixedAssetByLegalBook
            LegalBookId = .LegalBookId
            INDsleLegalBook.Properties.NullText = .LegalBookCodeName
            IvaCostByLegalBook = .IvaCost
            HandlesMinimumAmountByLegalBook = .HandlesMinimumAmount

            INDsleLegalBook.Properties.ReadOnly = True
        End With
        INDpceDetail.ShowPopup()
        INDsleLegalBook.Focus()
    End Sub

    ''' <summary>
    ''' Elimina el detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDetail()
        SettingFixedAssetByLegalBook = CType(ViewDetail.GetFocusedRow, SettingFixedAssetByLegalBook)
        ListSettingFixedAssetByLegalBook.Remove(SettingFixedAssetByLegalBook)

        If SettingFixedAssetByLegalBook.Id > 0 Then
            If ListDeleteSettingFixedAssetByLegalBook Is Nothing Then
                ListDeleteSettingFixedAssetByLegalBook = New List(Of SettingFixedAssetByLegalBook)
            End If
            SettingFixedAssetByLegalBook.MarkAsDeleted()
            ListDeleteSettingFixedAssetByLegalBook.Add(SettingFixedAssetByLegalBook)
        End If

        INDgcDetail.DataSource = Nothing
        INDgcDetail.DataSource = ListSettingFixedAssetByLegalBook
    End Sub

    ''' <summary>
    ''' Metodo que agrega un detalle a la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddDetail()
        'Se valida que los controles esten diligenciados
        Dim errors As String = ValidateControlsPopup()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If

        If FlagEditMode = False Then 'Si se esta guardando
            If ListSettingFixedAssetByLegalBook Is Nothing Then 'Si no hay registros en la rejilla
                ListSettingFixedAssetByLegalBook = New List(Of SettingFixedAssetByLegalBook)
            Else 'Si ya hay registros en la rejilla
                'Se valida que el libro seleccionado en el search no exista en la rejilla
                If (From l In ListSettingFixedAssetByLegalBook Where l.LegalBookId = LegalBookId Select l).Count > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "El libro " + INDsleLegalBook.Text + " ya existe en la lista."
                    INDsleLegalBook.Focus()
                    Exit Sub
                End If
            End If

            Dim _SettingFixedAssetByLegalBook As New SettingFixedAssetByLegalBook
            'Se crea la nueva entidad para agregarlo al listado
            With _SettingFixedAssetByLegalBook
                .LegalBookId = LegalBookId
                .LegalBookCodeName = INDsleLegalBook.Text
                .IvaCost = IvaCostByLegalBook
                .HandlesMinimumAmount = HandlesMinimumAmountByLegalBook
            End With
            Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente."
            ListSettingFixedAssetByLegalBook.Add(_SettingFixedAssetByLegalBook)
        Else 'Si se esta editando
            'Se edita el objeto que se obiene cuando se edita
            With SettingFixedAssetByLegalBook
                .LegalBookId = LegalBookId
                .LegalBookCodeName = INDsleLegalBook.Text
                .IvaCost = IvaCostByLegalBook
                .HandlesMinimumAmount = HandlesMinimumAmountByLegalBook
            End With
            Mensaje(EeventViewerImages.Informacion) = "Detalle editado correctamente."
        End If

        INDgcDetail.DataSource = Nothing
        INDgcDetail.DataSource = ListSettingFixedAssetByLegalBook
        CleanControlsPopup()
        FlagEditMode = False
        INDpceDetail.ShowPopup()
        INDsleLegalBook.Focus()
    End Sub

    ''' <summary>
    ''' Metodo que valida los controles del popup
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsPopup() As String
        Dim listErrors As New StringBuilder
        If LegalBookId Is Nothing Then
            listErrors.AppendLine("Ingrese un Libro Oficial.")
        End If
        If IvaCostByLegalBook Is Nothing Then
            listErrors.AppendLine("Identifique si para el libro se lleva IVA al costo.")
        End If
        If HandlesMinimumAmountByLegalBook Is Nothing Then
            listErrors.AppendLine("Identifique si para el libro se maneja mínima cuantía.")
        End If
        Return listErrors.ToString
    End Function

    ''' <summary>
    ''' Limpia los controles del popup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsPopup()
        LegalBookId = Nothing
        INDsleLegalBook.Properties.NullText = String.Empty
        INDsleLegalBook.Properties.ReadOnly = False
        IvaCostByLegalBook = Nothing
        HandlesMinimumAmountByLegalBook = Nothing
        FlagEditMode = False
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
            LoadControls()
            If SettingFixedAsset IsNot Nothing AndAlso SettingFixedAsset.Id > 0 Then
                SettingFixedAsset.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified
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

End Class