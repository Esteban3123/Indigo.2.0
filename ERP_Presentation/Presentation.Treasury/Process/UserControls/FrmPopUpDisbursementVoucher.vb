'***********************************************************************
' Assembly         : Presentacion.Treasury
' Author           : Diego Andr�s Rold�n Lozano
' Created          : 27/05/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : Control de usuario que permite agregar conceptos
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports System.ComponentModel
Imports Presentation.Controls
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Extension
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Common
Imports Presentation.Accounting
Imports Presentation.Accounting.MVP
Imports DevExpress.Xpo
Imports Presentation.Controls.MVP
Imports Presentation.Treasury.MVP
Imports DevExpress.Data.Linq
Imports Domain.Entities.Service
Imports Presentation.Maintenance.MVP
Imports Presentation.Payments.MVP
Imports System.Text
Imports DevExpress.XtraLayout
Imports System.Globalization
Imports Infrastructure.Data.Xpo.PaymentsRepository
Imports Infrastructure.Data.Xpo.TreasuryRepository
Imports Presentation.Portfolio.MVP
Imports DevExpress.XtraGrid
Imports Presentation.Common.MVP
Imports Presentation.Treasury.FrmVoucherTransaction
Imports Infrastructure.Data.Xpo
Imports DevExpress.Data.Async.Helpers

#End Region

Public Class FrmPopUpDisbursementVoucher

#Region "Constructor"

    Public Sub New()

        ' Llamada necesaria para el dise�ador.
        InitializeComponent()
        ctrAdvance = New CtrAdvanceCxC()
        ctrAdvance.SetAdvance(AddressOf getAdvanceTreasury)
        ctrAdvance.PrintAdvance()
        ctrAdvance.Dock = DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrAdvance)
    End Sub
#End Region

#Region "Variables"

    Private _advanceValue As Decimal

    ''' <summary>
    ''' cuenta contable del detalle
    ''' </summary>
    Private _mainAccountVoucherDetail As MainAccounts

    Private _generalLedgerIVA As ActionResult(Of GeneralLedgerIVA)

    ''' <summary>
    ''' Ontiene o establece los anticipos pagados de cartera
    ''' </summary>
    ''' <value>
    ''' The voucher transaction advance.
    ''' </value>
    Property VoucherTransactionAdvance As List(Of VoucherTransactionAdvance)

    ''' <summary>
    ''' Gets or sets the identifier cash register validate.
    ''' </summary>
    Property IdCashRegisterValidate As Integer?

    ''' <summary>
    ''' Obtiene o establece el tipo de egreso del comprobante
    ''' </summary>
    Property ExpenseType As Short

    ''' <summary>
    ''' Listado de los detalles del comprobante para validacion
    ''' </summary>
    Private _listVoucherDetail As List(Of VoucherTransactionDetails)

    ''' <summary>
    ''' detalle de comprobante de egreso que se pasa como parametro
    ''' </summary>
    Private _voucherTransactionDetail As VoucherTransactionDetails
    ''' <summary>
    ''' detalle de comprobante de egreso que se esta editando
    ''' </summary>
    Private _voucherTransactionDetailPrevius As VoucherTransactionDetails

    ''' <summary>
    ''' Control de usuario que muestra el anticipo que se le ha hecho al tercero
    ''' </summary>
    Dim ctrAdvance As CtrAdvanceCxC
    ''' <summary>
    ''' Almacena el listado de facturas a eliminar
    ''' </summary>
    Private _listDischargeBillDelete As List(Of DischargeBill)
    ''' <summary>
    ''' Obtiene o establece el id de la unidad operativa seleccionada
    ''' </summary>
    ''' <value>
    ''' The unit operative identifier.
    ''' </value>
    Public Property UnitOperativeId As Integer
    ''' <summary>
    ''' diccionario para almacenar las edades de pagos por unidad operativa
    ''' </summary>
    Property DictionaryAgesPayments As Dictionary(Of Integer, List(Of AgesPayments))
    ''' <summary>
    ''' The expense concept
    ''' </summary>
    Private _expenseConcept As ExpenseConcepts
    ''' <summary>
    ''' variable para conocer el id del detalle del comprobante para asi saber si se edita o crea
    ''' </summary>
    Private _idVoucherTransactionDetail As Integer
    ''' <summary>
    ''' Constante con el nombre del m�dulo
    ''' </summary>
    Private Const NAME_MODULE As String = "Treasury"

    ''' <summary>
    ''' Constante con el nombre del m�dulo de contabilidad
    ''' </summary>
    Private Const NAME_MODULE_ACCOUNTING As String = "Accounting"

    ''' <summary>
    ''' Constante con el nombre del modulo de pagos
    ''' </summary>
    Private Const NAME_MODULE_PAYMENT As String = "Payments"

    ''' <summary>
    ''' Contiene el tag del from principal
    ''' </summary>
    Private _myTag As String = "636"

    ''' <summary>
    ''' variable que contiene la lista de los campos obligatorios que no se han llenado
    ''' </summary>
    Private _listaValidaciones As StringBuilder

    ''' <summary>
    ''' bandera que indica si se est� editando un registro o es nuevo
    ''' </summary>
    Public IsEditMode As Boolean = False

    ''' <summary>
    ''' variable que indica que se est� editando por ende se cargan las facturas ya editadas
    ''' </summary>
    Private _datasourcePaymentConceptLoad As Boolean = False

    'variables obtenidas de la consulta del concepto de retencion
    Private _minBaseRetention As Decimal
    Private _retentiontype As Integer
    Private _listAccountingRetention As List(Of RetentionConceptRanges)

    ''' <summary>
    ''' contiene el concepto de retencion seleccionado
    ''' </summary>
    Private _retConcept As RetentionConcepts

    ''' <summary>
    ''' diccionario para validar el orden de pago de las facturas
    ''' </summary>
    Dim _dictionaryShares As Dictionary(Of String, List(Of Infrastructure.Data.Xpo.PaymentsRepository.AccountPayableSharesXpo))

    ''' <summary>
    ''' variable que contiene la cuota de factura seleccionada
    ''' </summary>
    Private _shareInvoice As Infrastructure.Data.Xpo.PaymentsRepository.AccountPayableSharesXpo

    ''' <summary>
    ''' obtiene o establece el tercero principal que esta en la cabecera
    ''' </summary>
    ''' <value>
    ''' The main third.
    ''' </value>
    Property MainThird As Integer

    ''' <summary>
    ''' Listado de las facturas agregadas
    ''' </summary>
    Dim _listAccountPayableShares As List(Of AccountPayableShares)

    ''' <summary>
    ''' Gets or sets the list account payable complex.
    ''' </summary>
    ''' <value>
    ''' The list account payable complex.
    ''' </value>
    Public Property ListDischargeBill As List(Of DischargeBill)

    ''' <summary>
    ''' Listado de las facturas
    ''' </summary>
    Private _listAccountPayableDetail As List(Of AccountPayableDetailConcept)

    ''' <summary>
    ''' Contiene el id de la caja seleccionada en el frontal de comprobantes de egreso
    ''' </summary>
    Dim _idCashRegister As Integer

    ''' <summary>
    ''' Contiene el id de la cuenta bancaria en el frontal de comprobantes de egreso
    ''' </summary>
    Dim _idEntityBankAccount As Integer

    ''' <summary>
    ''' entidad detalle del comprobante de egreso
    ''' </summary>
    Private _voucherDetail As VoucherTransactionDetails

    ''' <summary>
    ''' Almacena el comportamiento del concepto
    ''' </summary>
    Private _behavior As Integer

    ''' <summary>
    ''' Lista la Naturaleza de la cuenta
    ''' </summary>
    Dim ListNature As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' variable que contiene el estado del popUp de las facturas para cargar el datasource una sola vez
    ''' </summary>
    Private _statePopUpInvoice As Boolean
    ''' <summary>
    ''' The _state pop up payment concept
    ''' </summary>
    Private _statePopUpPaymentConcept As Boolean
    ''' <summary>
    ''' variable que contiene la fecha del servidor
    ''' </summary>
    Private _serverDate As Date

    ''' <summary>
    ''' bandera carga de controles
    ''' </summary>
    Private _flagLoadControls As Boolean

    ''' <summary>
    ''' Moneda de la cabecera del documento
    ''' </summary>
    Private _currencyHeader As Currency

    ''' <summary>
    ''' Lista de la tasa de cambio
    ''' </summary>
    Private _listTRM As List(Of TRM)

    Private _dataThirdParty As Object

    Private _dataDetail As List(Of VoucherTransactionDetailAccountInfo)

    ''' <summary>
    ''' datos de la cuenta compra/servicio
    ''' </summary>
    Private _purchaseService As Infrastructure.Data.Xpo.PaymentsRepository.GeneralLedgerMainAccountsXpo

    ''' <summary>
    ''' datos cuenta debito al iva descontable
    ''' </summary>
    Private _accountDebit As Infrastructure.Data.Xpo.PaymentsRepository.GeneralLedgerMainAccountsXpo

    ''' <summary>
    ''' datos cuenta credito al iva descontable
    ''' </summary>
    Private _accountCredit As Infrastructure.Data.Xpo.PaymentsRepository.GeneralLedgerMainAccountsXpo

    ''' <summary>
    ''' variable para cargar los datos de la Sesion
    ''' </summary>
    Private _sessionValue As SessionValues

    ''' <summary>
    ''' Tasa de cambio entre la moneda de la cabecera y la moneda de la caja menor de en tipo reemboslo
    ''' </summary>
    Private _tRMCashRegisterRefund As Decimal

    ''' <summary>
    ''' propiedad que establece el valor del TRM por defecto es 1
    ''' </summary>
    Private _tRMValueAdvanced As Decimal

    ''' <summary>
    ''' TRM de la factura a pagar, cuando esta en una moneda distinta a la de la cabecera
    ''' </summary>
    Private _trmInvoice As Decimal

    ''' <summary>
    ''' Valor en la moneda de la caja menor seleccionada para reembolsos
    ''' </summary>
    Private _valueInCurrencyCashRegisterR As Decimal

    ''' <summary>
    ''' lista que almacena los o el anticipo de tesoreria al EDITAR
    ''' </summary>
    Private _listTreasuryAdvances As List(Of TreasuryAdvances)
    ''' <summary>
    ''' Variables para bandera tenat 
    ''' </summary>
    Public flagTenant As Boolean
    ''' <summary>
    ''' Variables para tipo de pago 
    ''' </summary>
    Public flagClassPayment As Boolean
    ''' <summary>
    ''' Variables para caja menor
    ''' </summary>
    Public flagMinorCash As Boolean

#End Region

#Region "Properties"

    ''' <summary>
    ''' Contiene o establece el listado de los conceptos de egreso para validar que no se ingresen mas del mismo tipo
    ''' </summary>
    ''' <value>
    ''' The _list expense concept.
    ''' </value>
    Public Property ListVoucherDetails As List(Of VoucherTransactionDetails)

    ''' <summary>
    ''' Obtiene o establece la clase de comprobante que se va a generar
    ''' </summary>
    Property VoucherClass As Byte

    ''' <summary>
    ''' Obtiene o estabelece el tipo de comprobante que se va a generar 1 = cheque ,2 = nota, 3 = Pse 
    ''' </summary>
    ''' <returns></returns>
    Property PaymentMethod As Integer?

    ''' <summary>
    ''' Maneja documento soporte
    ''' </summary>
    ''' <returns></returns>
    Public Property HandlesDocumentSupport As Boolean?

    ''' <summary>
    '''   Parametros de cxp
    ''' </summary>
    ''' <returns></returns>
    Public Property PaymentsSettingPaymentsXpo As PaymentsSettingPaymentsXpo

    ''' <summary>
    ''' Obtiene o establece el listado de reembolsos
    ''' </summary>
    Public Property ListRefundsDatasource As List(Of Refunds)
        Get
            Return CType(INDgcRefund.DataSource, List(Of Refunds))
        End Get
        Set(value As List(Of Refunds))
            INDgcRefund.DataSource = value
        End Set
    End Property

    Private _statusVoucher As Byte
    Property StatusVoucher As Byte
        Get
            Return _statusVoucher
        End Get
        Set(value As Byte)
            _statusVoucher = value
            If value = 2 OrElse value = 3 OrElse value = 4 Then
                INDsbAdd.Enabled = False
            End If
        End Set
    End Property

    Public ReadOnly Property BudgetInterface As Boolean
        Get
            Return If(PaymentsSettingPaymentsXpo Is Nothing, False, PaymentsSettingPaymentsXpo.BudgetInterface)
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de cajas
    ''' </summary>
    Private Property CashDatasource As LinqInstantFeedbackSource
        Get
            Return CType(INDsleCashRegister.Properties.DataSource, LinqInstantFeedbackSource)
        End Get
        Set(value As LinqInstantFeedbackSource)
            INDsleCashRegister.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la caja
    ''' </summary>
    Private Property CashRegisterId As Integer?
        Get
            Return INDsleCashRegister.EditValue
        End Get
        Set(value As Integer?)
            INDsleCashRegister.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el detalle del anticipo
    ''' </summary>
    ''' <value>
    ''' The advance detail.
    ''' </value>
    Public Property AdvanceDetail As String
        Get
            Return INDMeAdvanceDetail.Text
        End Get
        Set(value As String)
            INDMeAdvanceDetail.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el iva descontable
    ''' </summary>
    ''' <value>
    ''' The concept.
    ''' </value>
    Public Property DiscountableIVA As Boolean?
        Get
            Return INDDiscountableIVA.EditValue
        End Get
        Set(value As Boolean?)
            INDDiscountableIVA.EditValue = value

        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la tarifa iva
    ''' </summary>
    ''' <value>
    ''' The concept.
    ''' </value>
    Public Property IdGeneralLedgerIVA As Integer?
        Get
            Return INDsleGeneralLedgerIVA.EditValue
        End Get
        Set(value As Integer?)
            INDsleGeneralLedgerIVA.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor del iva 
    ''' </summary>
    ''' <value>
    ''' The concept.
    ''' </value>
    Public Property ValueIVA As Decimal?
        Get
            Return INDtxtValueIVA.EditValue
        End Get
        Set(value As Decimal?)
            INDtxtValueIVA.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el concepto
    ''' </summary>
    ''' <value>
    ''' The concept.
    ''' </value>
    Public Property TotalConcept As Decimal?
        Get
            Return INDtxtTotalConcept.EditValue
        End Get
        Set(value As Decimal?)
            INDtxtTotalConcept.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el concepto
    ''' </summary>
    ''' <value>
    ''' The concept.
    ''' </value>
    Public Property ExpenseConcept As Integer?
        Get
            Return INDsleExpenseConcept.EditValue
        End Get
        Set(value As Integer?)
            INDsleExpenseConcept.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor del pago a la cuota
    ''' </summary>
    ''' <value>
    ''' The pay value.
    ''' </value>
    Property PayValue As Decimal
        Get
            Return CDec(INDtxtPayValue.EditValue)
        End Get
        Set(value As Decimal)
            INDtxtPayValue.EditValue = value
            Me.ValueBillInCurrencyHeader = Math.Round((value / Me.TRMInvoice), 2)
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor del porcentaje a la cuota
    ''' </summary>
    ''' <value>
    ''' The pay percent.
    ''' </value>
    Property PayPercent As Decimal
        Get
            Return CDec(INDtxtPayPercent.EditValue)
        End Get
        Set(value As Decimal)
            INDtxtPayPercent.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del concepto de pago
    ''' </summary>
    ''' <value>
    ''' The identifier payment concept.
    ''' </value>
    Property IdPaymentConcept As Integer
        Get
            Return INDslePaymentConcept.EditValue
        End Get
        Set(value As Integer)
            INDslePaymentConcept.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la caja en el comprobante de egreso
    ''' </summary>
    ''' <value>
    ''' The identifier cash.
    ''' </value>
    Public WriteOnly Property IdCash As Integer
        Set(value As Integer)
            _idCashRegister = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the account payable datasource.
    ''' </summary>
    ''' <value>
    ''' The account payable datasource.
    ''' </value>
    Public Property AccountPayableDatasource As List(Of DischargeBill)
        Get
            Return CType(INDGcInvoices.DataSource, List(Of DischargeBill))
        End Get
        Set(value As List(Of DischargeBill))
            INDGcInvoices.DataSource = value
            INDGcInvoices.Refresh()
            INDGcInvoices.RefreshDataSource()
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece una cuenta contable
    ''' </summary>
    Public Property MainAccountId As Integer
        Get
            Return INDsleAccount.EditValue
        End Get
        Set(value As Integer)
            INDsleAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tercero
    ''' </summary>
    ''' <value>
    ''' The third parthy.
    ''' </value>
    Public Property ThirdParthy As Integer?
        Get
            Return INDsleThirdParty.EditValue
        End Get
        Set(value As Integer?)
            INDsleThirdParty.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de Centro de Costo
    ''' </summary>
    ''' <value>
    ''' The cost center.
    ''' </value>
    Public Property CostCenter As Integer?
        Get
            Return INDsleCostCenter.EditValue
        End Get
        Set(value As Integer?)
            INDsleCostCenter.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la naturaleza
    ''' </summary>
    ''' <value>
    ''' The nature.
    ''' </value>
    Public Property Nature As Integer
        Get
            Return INDgleNature.EditValue
        End Get
        Set(value As Integer)
            INDgleNature.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece la actividad economica
    ''' </summary>
    ''' <returns></returns>
    Public Property EconomicActivity As Integer?
        Get
            Return INDsleEconomicActivity.EditValue
        End Get
        Set(value As Integer?)
            INDsleEconomicActivity.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece el datasource de la actividad economica
    ''' </summary>
    ''' <returns></returns>
    Public Property EconomicActivityDatasource As XPInstantFeedbackSource
        Get
            Return CType(INDsleEconomicActivity.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleEconomicActivity.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource para centros de costo
    ''' </summary>
    ''' <value>
    ''' The cost center datasource.
    ''' </value>
    Public Property CostCenterDatasource As XPInstantFeedbackSource
        Get
            Return CType(INDsleCostCenter.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleCostCenter.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource para las tarifa IVA
    ''' </summary>
    ''' <value>
    ''' The General Ledger datasource.
    ''' </value>
    Public Property GeneralLedgerIVADatasource As XPInstantFeedbackSource
        Get
            Return CType(INDsleGeneralLedgerIVA.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleGeneralLedgerIVA.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource para los terceros
    ''' </summary>
    ''' <value>
    ''' The third party datasource.
    ''' </value>
    Public Property ThirdPartyDatasource As XPInstantFeedbackSource
        Get
            Return CType(INDsleThirdParty.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleThirdParty.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource para las cuentas contables
    ''' </summary>
    ''' <value>
    ''' The account datasource.
    ''' </value>
    Public Property AccountDatasource As XPInstantFeedbackSource
        Get
            Return CType(INDsleAccount.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Edit value de la cuenta bancaria
    ''' </summary>
    ''' <value>
    ''' The identifier entity bank account.
    ''' </value>
    Public Property EntityBankAccountId As Integer?
        Get
            Return INDsleEntityBankAccount.EditValue
        End Get
        Set(value As Integer?)
            INDsleEntityBankAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del concepto de retencion
    ''' </summary>
    ''' <value>
    ''' The retention concept.
    ''' </value>
    Public Property IdRetentionConcept As Integer?
        Get
            Return INDsleConceptRetention.EditValue
        End Get
        Set(value As Integer?)
            INDsleConceptRetention.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece los comentarios
    ''' </summary>
    ''' <value>
    ''' The observation.
    ''' </value>
    Public Property Observation As String
        Get
            Return INDmemoComments.EditValue
        End Get
        Set(value As String)
            INDmemoComments.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el porcentaje de retencion
    ''' </summary>
    ''' <value>
    ''' The percent retention.
    ''' </value>
    Public Property PercentRetention As Decimal
        Get
            Return INDsePercentage.EditValue
        End Get
        Set(value As Decimal)
            INDsePercentage.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor facturado
    ''' </summary>
    ''' <value>
    ''' The invoiced value.
    ''' </value>
    Public Property BaseValueRetention As Decimal
        Get
            Return INDtxtBaseValue.EditValue
        End Get
        Set(value As Decimal)
            INDtxtBaseValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el id de la cuenta bancaria del form cabecera para validar que se elija una distinta en el concepto de egreso
    ''' </summary>
    ''' <value>
    ''' The identifier entity bank account.
    ''' </value>
    Public WriteOnly Property IdEntityBankAccount As Integer
        Set(value As Integer)
            _idEntityBankAccount = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene la naturaleza del concepto
    ''' </summary>
    ''' <value>
    ''' The nature expense concept.
    ''' </value>
    Public ReadOnly Property NatureExpenseConcept As Integer
        Get
            Return INDgleNature.EditValue
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el valor del anticipo
    ''' </summary>
    ''' <value>
    ''' The value advance payment.
    ''' </value>
    Public Property ValueAdvancePayment As Decimal
        Get
            Return CDec(INDtxtAdvancePayment.EditValue)
        End Get
        Set(value As Decimal)
            INDtxtAdvancePayment.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Devuelve el valor base
    ''' </summary>
    ''' <value>
    ''' The base value.
    ''' </value>
    Public Property ValueConcept As Decimal
        Get
            Return INDtxtValue.EditValue
        End Get
        Set(value As Decimal)
            INDtxtValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuota de una factura
    ''' </summary>
    ''' <value>
    ''' The identifier account payable share.
    ''' </value>
    Public Property IdAccountPayableShare As Integer
        Get
            Return INDsleInvoiceShare.EditValue
        End Get
        Set(value As Integer)
            INDsleInvoiceShare.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha de expiracion de la factura
    ''' </summary>
    ''' <value>
    ''' The date expires.
    ''' </value>
    Public WriteOnly Property DateExpires As DateTime
        Set(value As DateTime)
            INDlbDateExpire.Text = value.ToShortDateString()
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el saldo de la couta de la factura
    ''' </summary>
    Private _balance As Decimal
    Public Property Balance(Optional _numberFormat As NumberFormatInfo = Nothing) As Decimal
        Get
            Return _balance
        End Get
        Set(value As Decimal)
            _numberFormat = If(_numberFormat Is Nothing, CultureInfo.CurrentCulture.NumberFormat, _numberFormat)
            INDlbBalance.Text = value.ToString("c2", _numberFormat)
            _balance = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el comportamiento del concepto
    ''' </summary>
    ''' <value>
    ''' The behavior.
    ''' </value>
    Public ReadOnly Property Behavior As Integer
        Get
            Return _behavior
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource para las cuentas bancarias
    ''' </summary>
    ''' <value>
    ''' The entity bank account datasource.
    ''' </value>
    Public Property EntityBankAccountDatasource As LinqInstantFeedbackSource
        Get
            Return CType(INDsleEntityBankAccount.Properties.DataSource, LinqInstantFeedbackSource)
        End Get
        Set(value As LinqInstantFeedbackSource)
            INDsleEntityBankAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de conceptos de retencion
    ''' </summary>
    ''' <value>
    ''' The retention concept datasource.
    ''' </value>
    Public Property RetentionConceptDatasource As XPInstantFeedbackSource
        Get
            Return CType(INDsleConceptRetention.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleConceptRetention.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource para los conceptos
    ''' </summary>
    ''' <value>
    ''' The concept datasource.
    ''' </value>
    Public Property ConceptDatasource As XPInstantFeedbackSource
        Get
            Return CType(INDsleExpenseConcept.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleExpenseConcept.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de las cuotas de facturas
    ''' </summary>
    ''' <value>
    ''' The account payable share datasource.
    ''' </value>
    Public Property AccountPayableShareDatasource As LinqInstantFeedbackSource
        Get
            Return CType(INDsleInvoiceShare.Properties.DataSource, LinqInstantFeedbackSource)
        End Get
        Set(value As LinqInstantFeedbackSource)
            INDsleInvoiceShare.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de conceptos de pago
    ''' </summary>
    ''' <value>
    ''' The payment concept datasource.
    ''' </value>
    Public Property PaymentConceptDatasource As XPInstantFeedbackSource
        Get
            Return CType(INDslePaymentConcept.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDslePaymentConcept.Properties.DataSource = value
        End Set
    End Property

    ' ''' <summary>
    ' ''' Gets or sets the portfolio advance datasource.
    ' ''' </summary>
    Public Property PortfolioAdvanceDatasource As XPCollection
        Get
            Return CType(INDgcPortfolioAdvance.DataSource, XPCollection)
        End Get
        Set(value As XPCollection)
            INDgcPortfolioAdvance.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para los mensajes
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String
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

    Private _EntityCode As String
    ''' <summary>
    ''' Codigo de la entidad: Se hace esto porque al darle click en actualizar se borra de la rejilla del form principal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EntityCode As String
        Get
            Return _EntityCode
        End Get
        Set(value As String)
            _EntityCode = value
        End Set
    End Property

    Private _EntityName As String
    ''' <summary>
    ''' Nombre de la entidad: Se hace esto porque al darle click en actualizar se borra de la rejilla del form principal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EntityName As String
        Get
            Return _EntityName
        End Get
        Set(value As String)
            _EntityName = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property CashFlowConceptCodeName() As String
        Get
            Return INDLbCashFlowConcept.Text
        End Get
        Set(ByVal value As String)
            INDLbCashFlowConcept.Text = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property CashFlowConceptId() As Integer
        Get
            Return INDLbCashFlowConcept.Tag
        End Get
        Set(ByVal value As Integer)
            INDLbCashFlowConcept.Tag = value
        End Set
    End Property
    ''' <summary>
    ''' propiedad que guarda o asigna el valor del descuento de pronto pago
    ''' </summary>
    ''' <returns></returns>
    Public Property PromptPaymentDiscount As Decimal
        Get
            Return CDec(INDtxtPromptPaymentDiscount.EditValue)
        End Get
        Set(value As Decimal)
            INDtxtPromptPaymentDiscount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que almacena el valor del decuento aplicado
    ''' </summary>
    ''' <returns></returns>
    Private Property ApplyDiscountValue As Decimal = 0

    ''' <summary>
    ''' propiedad que establece la moneda de la cabecera del comprobante de egreso
    ''' </summary>
    Public WriteOnly Property CurrencyHeader As Currency
        Set(value As Currency)
            _currencyHeader = value
        End Set
    End Property

    ''' <summary>
    ''' Moneda del Anticipo esta sera diferente a la de la caja/banco si el concepto de gasto es diferente a ninguno
    ''' </summary>
    ''' <returns></returns>
    Private Property CurrencyAdvancedId As Integer?
        Get
            Return INDsleCurrency.EditValue
        End Get
        Set(value As Integer?)
            INDsleCurrency.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' datasource del combo Moneda del segmento anticipo
    ''' </summary>
    ''' <returns></returns>
    Private Property CurrencyAdvanceDatasource As XPInstantFeedbackSource
        Get
            Return TryCast(INDsleCurrency.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleCurrency.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Moneda seleccionada en el segmento anticipo
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property CurrencyAdvanceSelected As Infrastructure.Data.Xpo.CommonRepository.CommonCurrencyXpo
        Get
            Return TryCast(TryCast(INDGvCurrencyAdvance.GetFocusedRow, ReadonlyThreadSafeProxyForObjectFromAnotherThread)?.OriginalRow, Infrastructure.Data.Xpo.CommonRepository.CommonCurrencyXpo)
        End Get
    End Property

    ''' <summary>
    ''' propiedad que establece el valor del TRM por defecto es 1
    ''' </summary>
    ''' <returns></returns>
    Property TRMValueAdvanced As Decimal
        Get
            Return If(_tRMValueAdvanced = 0, 1, _tRMValueAdvanced)
        End Get
        Set(value As Decimal)
            _tRMValueAdvanced = value
            INDtxtTRM.EditValue = Math.Round(Utils.VisibleTRM(value), 2)
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene o establece el valor del anticipo en la moneda de la cabecera del documento
    ''' </summary>
    ''' <returns></returns>
    Private Property ValueInCurrencyHeaderAdvance As Decimal
        Get
            Return CDec(If(INDtxtValueinCurrencyH.EditValue Is Nothing,
                Me.ValueAdvancePayment, INDtxtValueinCurrencyH.EditValue))
        End Get
        Set(value As Decimal)
            INDtxtValueinCurrencyH.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene o establece el valor del saldo a pagar en la moneda de la cabecera del documento
    ''' </summary>
    ''' <returns></returns>
    Private Property ValueBillInCurrencyHeader As Decimal
        Get
            Return CDec(If(INDtxtValueBillCurrencyH.EditValue Is Nothing,
                Me.PayValue, INDtxtValueBillCurrencyH.EditValue))
        End Get
        Set(value As Decimal)
            INDtxtValueBillCurrencyH.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' TRM de la factura a pagar, cuando esta en una moneda distinta a la de la cabecera
    ''' </summary>
    ''' <returns></returns>
    Property TRMInvoice As Decimal
        Get
            Return If(_trmInvoice = 0, 1, _trmInvoice)
        End Get
        Set(value As Decimal)
            _trmInvoice = value
        End Set
    End Property

    ''' <summary>
    ''' Id de la moneda de de caja registradora del rembolso
    ''' </summary>
    ''' <returns></returns>
    Private Property CurrencyIdCashRegisterRefund(Optional _currencyAbbreviation As String = Nothing) As Integer?
        Get
            Return If(INDSleCurrencyCashRegister.EditValue Is Nothing, Me._sessionValue.OfficialCurrencyId, INDSleCurrencyCashRegister.EditValue)
        End Get
        Set(value As Integer?)
            INDSleCurrencyCashRegister.EditValue = value
            _currencyAbbreviation = If(_currencyAbbreviation Is Nothing, Me._sessionValue.CurrencyISO4217, _currencyAbbreviation)
            INDSleCurrencyCashRegister.Properties.NullText = _currencyAbbreviation
            INDLciValueinCashRegisterR.Text = $"Valor ({ _currencyAbbreviation})"
            Me.SetCultureUIRefund(_currencyAbbreviation)
        End Set
    End Property

    ''' <summary>
    ''' Tasa de cambio entre la moneda de la cabecera y la moneda de la caja menor de en tipo reemboslo
    ''' </summary>
    ''' <returns></returns>
    Private Property TRMCashRegisterRefund As Decimal
        Get
            Return If(_tRMCashRegisterRefund = 0, 1, _tRMCashRegisterRefund)
        End Get
        Set(value As Decimal)
            _tRMCashRegisterRefund = value
            INDtxtTRMCashRegisterR.EditValue = Math.Round(Utils.VisibleTRM(value), 2)
        End Set
    End Property

    ''' <summary>
    ''' Valor en la moneda de la caja menor seleccionada para reembolsos
    ''' </summary>
    ''' <returns></returns>
    Private Property ValueInCurrencyCashRegisterR As Decimal
        Get
            Return If(_valueInCurrencyCashRegisterR = 0, Me.ValueConcept, _valueInCurrencyCashRegisterR)
        End Get
        Set(value As Decimal)
            _valueInCurrencyCashRegisterR = value
            INDtxtValueinCashRegisterR.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene o asigna la lista de anticipos de tesoreria
    ''' </summary>
    ''' <returns></returns>
    Public Property ListTreasuryAdvances As List(Of TreasuryAdvances)
        Get
            Return _listTreasuryAdvances
        End Get
        Set(value As List(Of TreasuryAdvances))
            _listTreasuryAdvances = value
        End Set
    End Property

    ''' <summary>
    ''' Define si se muestra el container de iva descontable
    ''' </summary>
    Public Property showDeductibleIva As Boolean

    ''' <summary>
    ''' Define el tipo de iva registrado en los parametros
    ''' </summary>
    Public Property TaxRegistration As Integer?
#End Region

#Region "Public Events"
    ''' <summary>
    ''' evento para cerrar el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Public Event ClosePopup(sender As Object, e As EventArgs)

    ''' <summary>
    ''' Ocurre cuando se cierra el popup de conceptos de egreso
    ''' </summary>
    Public Event AddVoucherDetail(sender As Object, e As AddExpenseConceptEventArgs)

    ''' <summary>
    ''' Sale del popup
    ''' </summary>
    Public Event ExitPopUp()

#End Region

#Region "Methods and Functions"

    ''' <summary>
    ''' consultar los reembolsos de la caja asociado a la cuenta contable del concepto
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadRefundByCashRegister()
        Using Model As New MRefund(Me.Tag)
            ListRefundsDatasource = (Model.ListRefundByCashRegisterId(CashRegisterId)).ObjectEmbbeded
            If ListRefundsDatasource IsNot Nothing Then
                Me.ValueConcept = ListRefundsDatasource.Sum(Function(x As Refunds) x.Value)
            End If
            Me.ValueInCurrencyCashRegisterR = Me.ValueConcept
        End Using
    End Sub

    ''' <summary>
    ''' Refreshes the value concept.
    ''' </summary>
    Private Sub RefreshValueConcept()
        If INDtxtValue.Enabled Then
            Exit Sub
        End If
        If INDlgrAdvancePayments.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If PortfolioAdvanceDatasource IsNot Nothing AndAlso PortfolioAdvanceDatasource.Count > 0 Then
                ValueConcept = (From pa In PortfolioAdvanceDatasource Select pa).Cast(Of Infrastructure.Data.Xpo.PortfolioRepository.Portfolio_PortfolioAdvance).ToList().Where(Function(x) x.PayValue > 0).Sum(Function(x) x.PayValue)
            End If
        Else
            ValueConcept = If(ListDischargeBill Is Nothing, 0, ListDischargeBill.Sum(Function(x) x.ValueInCurrencyHeader)) + Me.ValueInCurrencyHeaderAdvance
        End If
    End Sub

    ''' <summary>
    ''' Loads the dictionary shares asynchronous.
    ''' </summary>
    ''' <returns></returns>
    Private Function LoadDictionarySharesAsync(listBillNumber As List(Of String)) As Task
        INDGvInvoices.ShowLoadingPanel()
        Return Task.Factory.StartNew(Sub()
                                         LoadDictionaryShares(listBillNumber)
                                     End Sub)
    End Function

    ''' <summary>
    ''' Loads the dictionary shares.
    ''' </summary>
    Private Sub LoadDictionaryShares(listBillNumber As List(Of String))
        Using Model As New MBusqueda
            Dim filter() As Object = {listBillNumber}
            Dim accPayable As XPCollection = Model.ConsultarEntidades(eDataSource.ListAccountPayableByBillNumber, filter)
            If accPayable IsNot Nothing Then
                If _dictionaryShares Is Nothing Then
                    _dictionaryShares = New Dictionary(Of String, List(Of Infrastructure.Data.Xpo.PaymentsRepository.AccountPayableSharesXpo))()
                End If
                For Each apXpo As Infrastructure.Data.Xpo.PaymentsRepository.AccountPayableXpo In accPayable
                    If Not _dictionaryShares.ContainsKey(apXpo.BillNumber) Then
                        _dictionaryShares.Add(apXpo.BillNumber, apXpo.AccountPayableSharesXpo.ToList())
                    End If
                Next
            End If
            If INDGcInvoices.InvokeRequired Then
                INDGcInvoices.BeginInvoke(Sub()
                                              INDGvInvoices.HideLoadingPanel()
                                          End Sub)
            Else
                INDGvInvoices.HideLoadingPanel()
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Focuses the First control.
    ''' </summary>
    Public Sub FocusFirstControl()
        INDsleExpenseConcept.Focus()
    End Sub

    ''' <summary>
    ''' Inicia el datasource de conceptos
    ''' </summary>
    Public Sub InitializeExpenseConceptByCash()
        ConceptDatasource = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).TreasuryService.ListExpenseConceptByCash1(CStr(_idCashRegister))
    End Sub

    ''' <summary>
    ''' Initializes the expense concept by major cash.
    ''' </summary>
    Sub InitializeExpenseConceptByMajorCash()
        ConceptDatasource = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).TreasuryService.ListExpenseConceptByMajorCash()
    End Sub

    ''' <summary>
    ''' Initializes the concept by entity account.
    ''' </summary>
    Public Sub InitializeExpenseConceptNotCash(HandlesDocumentSupport As Boolean?)
        ConceptDatasource = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).TreasuryService.ListExpenseConceptByNotCash1(HandlesDocumentSupport)
    End Sub

    Public Sub InitializeExpenseConceptEndorsement()
        Using Model As New MBusqueda
            Dim filter() As Object = {7}
            ConceptDatasource = Model.ConsultarEntidades(eDataSource.ListExpenseConceptEndorsement, filter)
        End Using
    End Sub

    ''' <summary>
    ''' Inicia el datasource de cuentas contables
    ''' </summary>
    Private Sub InitializeAccountAccounting()
        Using Model As New MBusqueda
            Dim filter() As Object = {5, True}
            AccountDatasource = Model.ConsultarEntidades(eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    ''' <summary>
    ''' Inicia el datasource de cuentas bancarias
    ''' </summary>
    Private Sub InitializeEntityBankAccount()
        Using Model As New MBusqueda
            EntityBankAccountDatasource = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).TreasuryService.ListEntityBankAccountByUser(SessionValues.Instance.UserIndigo, True)
        End Using
    End Sub

    ''' <summary>
    ''' Inicia datasource de terceros
    ''' </summary>
    Private Sub InitializeGeneralLedgerIVA()
        Using Model As New MBusqueda
            GeneralLedgerIVADatasource = Model.ConsultarEntidades(eDataSource.ListGeneralLedgerIva)
        End Using
    End Sub
    ''' <summary>
    ''' Inicia el datasource de la actividad economica
    ''' </summary>
    Private Sub InitializeEconomicActivity()
        EconomicActivityDatasource = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).TreasuryService.ListEconomicActivity()
    End Sub

    ''' <summary>
    ''' Inicia datasource de terceros
    ''' </summary>
    Private Sub InitializeThird()
        Using Model As New MBusqueda
            ThirdPartyDatasource = Model.ConsultarEntidades(eDataSource.ThirdParty)
        End Using
    End Sub

    ''' <summary>
    ''' Inicia el datasource de centros de costo
    ''' </summary>
    Private Sub InitializeCostCenter()
        CostCenterDatasource = Infrastructure.Data.Xpo.XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).PayrollService.GetCostCenterByState(True) 'Model.ConsultarEntidades(eDataSource.CostCenter)
    End Sub

    ''' <summary>
    ''' Inicia el datasource de conceptos de retenci�n
    ''' </summary>
    Private Sub InitializeRetentionConcept()
        Using Model As New MBusqueda
            RetentionConceptDatasource = Model.ConsultarEntidades(eDataSource.ListRetentionConceptByStatus, "True")
        End Using
    End Sub

    ''' <summary>
    ''' Limpia todos los controles
    ''' </summary>
    Public Sub CleanControls()
        INDsleExpenseConcept.EditValue = Nothing
        INDsleEntityBankAccount.EditValue = Nothing
        INDsleAccount.EditValue = Nothing
        INDsleCostCenter.EditValue = Nothing
        EconomicActivity = Nothing
        Observation = Nothing
        INDgleNature.EditValue = 1
        ValueConcept = 0F
        ValueAdvancePayment = 0
        flagClassPayment = False
        flagMinorCash = False
        flagTenant = False
        AccountPayableDatasource = Nothing
        ListDischargeBill = Nothing
        _mainAccountVoucherDetail = Nothing
        _valueChanging = False
        _percentChanging = False
        _idVoucherTransactionDetail = 0
        IsEditMode = False
        CleanLayouts()
        CleanControlsRetention()
        PortfolioAdvanceDatasource = Nothing
        VoucherTransactionAdvance = Nothing
        INDsleCashRegister.EditValue = Nothing
        Me.HideOrShowTRMControls(TRMControls:=eTRMControls.Advance)
        Me.HideOrShowTRMControls(TRMControls:=eTRMControls.Billing)
        Me.HideOrShowTRMControls(TRMControls:=eTRMControls.Refund)
        Me.HideOrShowIVAControls(False)
        Me.CurrencyIdCashRegisterRefund(Me._currencyHeader?.Abbreviation) = Me._currencyHeader?.Id
        Me._listTRM = Nothing
        'Controls Invoice
        INDsleInvoiceShare.EditValue = Nothing
        INDlbBalance.Text = String.Empty
        INDlbDateExpire.Text = String.Empty
        Me.PayValue = 0F
        INDtxtPayPercent.EditValue = 0
        INDslePaymentConcept.EditValue = Nothing
        PromptPaymentDiscount = 0
        ApplyDiscountValue = 0
        Me.ListTreasuryAdvances = Nothing
        INDliEntityBankAccount.HideControl(True)
        INDliAccountAccounting.HideControl(True)
        INDliCostCenter.HideControl(True)
        INDliCashRegister.HideControl(True)
        'Layouts
        If VoucherClass = eVoucherClass.Transfer Then
            If ExpenseType = 1 Then
                INDliEntityBankAccount.HideControl(False)
            Else
                INDliCashRegister.HideControl(False)
            End If
        ElseIf VoucherClass = eVoucherClass.Refund Then
            INDliCashRegister.HideControl(False)
        End If

        INDsleConceptRetention.EditValue = Nothing

        INDtxtValue.Enabled = True
    End Sub

    ''' <summary>
    ''' Creates the list nature.
    ''' </summary>
    Private Sub CreateListNature()
        ListNature = New List(Of Tuple(Of Integer, String))
        ListNature.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("AccountNatureDebit")))
        ListNature.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("AccountNatureCredit")))
        INDgleNature.Properties.DataSource = ListNature
        INDgleNature.EditValue = 1
    End Sub

    ''' <summary>
    ''' Initializes the payments concept.
    ''' </summary>
    Private Sub InitializePaymentsConcept()
        Using Model As New MBusqueda
            Dim result As LinqInstantFeedbackSource = Model.ConsultarEntidades(eDataSource.ListAllPaymentConceptByState, True)
            RepositoryItemPaymentConcepts.DataSource = result
        End Using
    End Sub

    ''' <summary>
    ''' Iniciars the combos.
    ''' </summary>
    Public Sub IniciarCombos()
        If Not DesignMode Then
            Me.InitializeAccountAccounting()
            Me.InitializeCostCenter()
            InitializeGeneralLedgerIVA()
            Me.InitializeThird()
            Me.InitializeEntityBankAccount()
            Me.InitializeRetentionConcept()
            Me.InitializePaymentsConcept()
            Me.CreateListNature()
            Me.InitializeEconomicActivity()
        End If
    End Sub

    ''' <summary>
    ''' Habilita o inhabilita los controles de conceptos de retencion
    ''' </summary>
    ''' <value>
    ''' <c>true</c> if [actions on controls concept retention]; otherwise, <c>false</c>.
    ''' </value>
    Public WriteOnly Property ActionsOnControlsConceptRetention As Boolean
        Set(value As Boolean)
            INDsleConceptRetention.Enabled = value
        End Set
    End Property

    ''' <summary>
    ''' Limpia los controles de tipo de retencion
    ''' </summary>
    Public Sub CleanControlsRetention()
        _minBaseRetention = 0
        _retentiontype = 0
        INDsePercentage.Enabled = False
        INDtxtBaseValue.EditValue = Nothing
        INDTxtBillingValue.EditValue = Nothing
        INDtxtRetentionValue.EditValue = Nothing
        INDsePercentage.EditValue = Nothing
        _retConcept = Nothing
    End Sub

    ''' <summary>
    ''' Carga los datos de la entidad en los controles
    ''' </summary>
    Public Async Function LoadDataVoucherTransactionDetail(ByVal voucherTransactionDetail As VoucherTransactionDetails) As Task
        AsyncLoader(True)
        Await Task.Factory.StartNew(Sub()
                                        Me.SafeInvoke(Sub()
                                                          _flagLoadControls = True
                                                          Me.IsEditMode = True
                                                          Me._datasourcePaymentConceptLoad = True
                                                          Me.ValidateCurrencyHeader(VoucherClass, Me._currencyHeader)
                                                          _voucherTransactionDetailPrevius = voucherTransactionDetail
                                                          _idVoucherTransactionDetail = voucherTransactionDetail.Id
                                                          ThirdParthy = voucherTransactionDetail.IdThirdParty
                                                          ExpenseConcept = voucherTransactionDetail.IdExpenseConcept
                                                          MainAccountId = voucherTransactionDetail.IdMainAccount
                                                          Nature = voucherTransactionDetail.Nature
                                                          If voucherTransactionDetail.EconomicActivityId IsNot Nothing Then
                                                              EconomicActivity = voucherTransactionDetail.EconomicActivityId
                                                          End If
                                                          ValueAdvancePayment = voucherTransactionDetail.AdvanceValue
                                                          Me.ValueInCurrencyHeaderAdvance = voucherTransactionDetail?.ValueAdvanceInCurrencyHeader
                                                          Me.CurrencyAdvancedId = voucherTransactionDetail?.CurrencyAdvance?.Id
                                                          Me.INDsleCurrency.Properties.NullText = voucherTransactionDetail?.CurrencyAdvance?.Abbreviation
                                                          SetCultureUIAdvanced(voucherTransactionDetail?.CurrencyAdvance?.Abbreviation)
                                                          AdvanceDetail = voucherTransactionDetail.AdvanceDetail
                                                          CostCenter = voucherTransactionDetail.IdCostCenter
                                                          CashFlowConceptCodeName = voucherTransactionDetail.CodeNameCashFlowConcept
                                                          CashFlowConceptId = voucherTransactionDetail.IdCashFlowConcept.GetValueOrDefault

                                                          If voucherTransactionDetail.IdEntityBankAccount <> 0 Then
                                                              EntityBankAccountId = voucherTransactionDetail.IdEntityBankAccount
                                                          End If
                                                          If voucherTransactionDetail.CashRegisterId <> 0 Then
                                                              CashRegisterId = voucherTransactionDetail.CashRegisterId
                                                              Me.LoadRefundByCashRegister()
                                                              Me.CurrencyIdCashRegisterRefund(voucherTransactionDetail?.CurrencyCashRegister?.Abbreviation) = voucherTransactionDetail?.CurrencyCashRegister?.Id
                                                              Me.EditValueChanged_CurrencyCashRegister(Not Me._flagLoadControls)
                                                          End If

                                                          ValueConcept = CStr(voucherTransactionDetail.Value - voucherTransactionDetail.ValueAdvanceInCurrencyHeader)
                                                          If voucherTransactionDetail.IdRetentionConcept <> 0 Then
                                                              IdRetentionConcept = voucherTransactionDetail.IdRetentionConcept
                                                              Using Model As New MRetentionConcept(Me._myTag)
                                                                  _retConcept = Model.GetRetentionConceptByIdSimple(IdRetentionConcept)
                                                                  PercentRetention = voucherTransactionDetail.PercentRetention
                                                                  BaseValueRetention = TreasuryStaticServices.CalculateInvoiceValue(PercentRetention, ValueConcept)
                                                                  INDtxtRetentionValue.EditValue = AccountingServices.CalculateRetention(BaseValueRetention, _retConcept)
                                                                  INDTxtBillingValue.EditValue = voucherTransactionDetail.BillingValue
                                                              End Using
                                                          End If
                                                          EntityCode = voucherTransactionDetail.EntityCode
                                                          EntityName = voucherTransactionDetail.EntityName
                                                          If TaxRegistration <> 3 AndAlso TaxRegistration <> voucherTransactionDetail.TaxRegistration Then
                                                              Mensaje(EeventViewerImages.Advertencia) = "El par�metro de Registro IVA actual no coincide con el que previamente se hizo la factura. Por lo anterior, el iva descontable ha sido modificado"

                                                          End If
                                                          If TaxRegistration = 3 Then
                                                              DiscountableIVA = voucherTransactionDetail.discountableIVA
                                                              TaxRegistration = voucherTransactionDetail.TaxRegistration
                                                          Else
                                                              DiscountableIVA = DiscountableIVA
                                                              TaxRegistration = TaxRegistration
                                                          End If

                                                          If voucherTransactionDetail.AdvanceValue > 0 Then
                                                              If Not (ListDischargeBill IsNot Nothing AndAlso ListDischargeBill.Count > 0) Then
                                                                  ValueConcept = voucherTransactionDetail.ValueAdvanceInCurrencyHeader
                                                              End If
                                                          End If
                                                          IdGeneralLedgerIVA = voucherTransactionDetail.IdGeneralLedgerIVA
                                                          ValueIVA = voucherTransactionDetail.ValueIVA
                                                          TotalConcept = voucherTransactionDetail.TotalConcept

                                                          Observation = voucherTransactionDetail.Detail
                                                          _listDischargeBillDelete = voucherTransactionDetail.DischargeBillDelete
                                                          AccountPayableDatasource = Me.ListDischargeBill

                                                          If voucherTransactionDetail?.TreasuryAdvances?.ToList()?.Any() Then
                                                              Me.ListTreasuryAdvances = voucherTransactionDetail.TreasuryAdvances.ToList()
                                                          End If

                                                          _flagLoadControls = False
                                                      End Sub)
                                    End Sub)
        AsyncLoader(False)

    End Function

    ''' <summary>
    ''' Obtiene el listado de las facturas pendientes por pagar
    ''' </summary>
    ''' <returns></returns>
    Public Function GetInvoices() As List(Of DischargeBill)
        Return ListDischargeBill
    End Function

    ''' <summary>
    ''' A�ade cada cuota de factura al listado
    ''' </summary>
    Private Sub AddAccountPayableShareToList()
        If ListDischargeBill Is Nothing Then
            ListDischargeBill = New List(Of DischargeBill)
        End If
        If DictionaryAgesPayments Is Nothing Then
            DictionaryAgesPayments = New Dictionary(Of Integer, List(Of AgesPayments))()
        End If
        Dim _dischargeBill As New DischargeBill()
        With _dischargeBill
            .IdAccountPayable = _shareInvoice.IdAccountPayable.Id
            .IdAccountPayableShare = _shareInvoice.Id
            .AccountPayableBillNumber = _shareInvoice.IdAccountPayable.BillNumber
            .AccountPayableShareDateExpires = _shareInvoice.DateExpires
            .AccountPayableShareBalance = _shareInvoice.Balance
            .AccountPayableShareShare = _shareInvoice.Share
            .AdvancePercent = PayPercent
            .AdvancedValue = PayValue
            .OperativeUnitId = _shareInvoice.IdAccountPayable.IdOperatingUnit
            .IdPaymentConcept = IdPaymentConcept
            .ExpenseConceptId = ExpenseConcept
            .ValueInCurrencyHeader = Me.ValueBillInCurrencyHeader
            .TRMValue = Me.TRMInvoice

            If ApplyDiscountValue > 0 Then
                .ValueDiscountInCurrencyHeader = ApplyDiscountValue / Me.TRMInvoice
                .BaseValueDiscount = ApplyDiscountValue
                .DiscountPercent = PromptPaymentDiscount
            Else ''si no se manejan dichos campos se envian en 0 para no tener problemas al guardar los registros
                .ValueDiscountInCurrencyHeader = 0
                .BaseValueDiscount = 0
                .DiscountPercent = 0
            End If

            'Consulto las edades de cartera
            If Not DictionaryAgesPayments.ContainsKey(.OperativeUnitId) Then
                Using Model As New MAgesPayment(Me.Tag)
                    Dim _agesPayment As List(Of AgesPayments) = (Model.ListAgesPaymentByUnitOperativeIdSimple(.OperativeUnitId)).ObjectEmbbeded
                    DictionaryAgesPayments.Add(.OperativeUnitId, _agesPayment)
                End Using
            End If

            Dim daysExpired As Integer = (_serverDate - .AccountPayableShareDateExpires).TotalDays
            If daysExpired <= 0 Then
                .ColorAgePortFolio = Convert.ToInt32(ePortfolioAge.ColorDefault) 'Color.FromArgb(Convert.ToInt32(ePortfolioAge.ColorDefault))
            Else
                .ColorAgePortFolio = (From ap As AgesPayments In DictionaryAgesPayments(.OperativeUnitId) Where ap.InitialRange <= daysExpired And ap.EndRange >= daysExpired Select ap.Color).FirstOrDefault()
            End If

        End With
        If Not _dictionaryShares.ContainsKey(_shareInvoice.IdAccountPayable.BillNumber) Then
            _dictionaryShares.Add(_shareInvoice.IdAccountPayable.BillNumber, _shareInvoice.IdAccountPayable.AccountPayableSharesXpo.ToList())
        End If

        If BudgetInterface Then
            'Obtenemos los detalles presupuestales asociados a la cuenta por pagar
            GetObligationDetailsByDischargeBill(_dischargeBill)
        End If

        ListDischargeBill.Add(_dischargeBill)
        AccountPayableDatasource = Nothing
        AccountPayableDatasource = ListDischargeBill
        RefreshValueConcept()

    End Sub

    ''' <summary>
    ''' Carga las facturas pendientes por pagar del tercero seleccionado
    ''' </summary>
    Private Sub LoadPaymentInvoicesByThird()
        Using Model As New MAccountPayable(Me._myTag)
            Dim number As Integer = Model.GetCountAccountPayableShareByAccountPayIdThirdIdAccountAndState(ThirdParthy, MainAccountId, CInt(eStatusAccountPayable.Confirmado))
            If number = 0 Then
                Dim nameMainAccount As String = String.Empty
                Using ModelT As New MPUC(Me.Tag)
                    Dim account = ModelT.GetAccountId(CType(INDsleAccount.EditValue, Integer))
                    nameMainAccount = String.Concat(account.Number, " - ", account.Name)
                End Using
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("NoInvoiceThird", NAME_MODULE_PAYMENT), INDsleThirdParty.Text.Split("-").ElementAt(0), nameMainAccount)
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Validates the controls expense.
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControlsExpense() As Boolean
        _listaValidaciones = New StringBuilder()

        Select Case VoucherClass
            Case eVoucherClass.Payment
                If INDsleExpenseConcept.EditValue Is Nothing AndAlso VoucherClass = eVoucherClass.Payment Then
                    _listaValidaciones.AppendLine(INDliConcept.Text)
                End If
                If INDsleThirdParty.EditValue Is Nothing AndAlso VoucherClass = eVoucherClass.Payment Then
                    _listaValidaciones.AppendLine(INDliThirdParty.Text)
                End If
                If INDliDiscountableIVA.Visible AndAlso Me.DiscountableIVA Is Nothing Then
                    _listaValidaciones.AppendLine(INDliDiscountableIVA.Text)
                End If
                If INDliGeneralLedgerIVA.Visible AndAlso Me.IdGeneralLedgerIVA Is Nothing Then
                    _listaValidaciones.AppendLine(INDliGeneralLedgerIVA.Text)
                End If
                If INDliEconomicActivity.Visible AndAlso EconomicActivity Is Nothing Then
                    _listaValidaciones.AppendLine(INDliEconomicActivity.Text)
                End If
            Case eVoucherClass.Refund

                If CashRegisterId Is Nothing Then
                    _listaValidaciones.AppendLine(INDliCashRegister.Text)
                Else
                    If (ListVoucherDetails IsNot Nothing AndAlso ListVoucherDetails.FindAll(Function(x) x.CashRegisterId = CashRegisterId).Count > 0) _
                    OrElse (_listVoucherDetail IsNot Nothing AndAlso _listVoucherDetail.FindAll(Function(x) x.CashRegisterId = CashRegisterId).Count > 0) Then
                        _listaValidaciones.AppendLine(ResourceManager.GetString("CashRegisterExist", NAME_MODULE))
                    End If
                End If

                If ListRefundsDatasource Is Nothing OrElse ListRefundsDatasource.Count = 0 Then
                    _listaValidaciones.AppendLine(ResourceManager.GetString("RefundRequired", NAME_MODULE))
                End If

            Case eVoucherClass.Transfer
                If ExpenseType = 1 Then
                    If EntityBankAccountId Is Nothing Then
                        _listaValidaciones.AppendLine(INDliEntityBankAccount.Text)
                    Else
                        If (_listVoucherDetail IsNot Nothing AndAlso _listVoucherDetail.Count > 0) Then
                            _listaValidaciones.AppendLine(ResourceManager.GetString("MoreOneDetailCannot"))
                        End If
                    End If

                ElseIf ExpenseType = 3 Then
                    If CashRegisterId Is Nothing Then
                        _listaValidaciones.AppendLine(INDliCashRegister.Text)
                    Else
                        If (ListVoucherDetails IsNot Nothing AndAlso ListVoucherDetails.FindAll(Function(x) x.CashRegisterId = CashRegisterId).Count > 0) _
                    OrElse (_listVoucherDetail IsNot Nothing AndAlso _listVoucherDetail.FindAll(Function(x) x.CashRegisterId = CashRegisterId).Count > 0) Then
                            _listaValidaciones.AppendLine(ResourceManager.GetString("CashRegisterExist", NAME_MODULE))
                        End If
                    End If

                End If
        End Select

        'validaciones comunes
        If INDgleNature.EditValue Is Nothing Then
            _listaValidaciones.AppendLine(INDlyItemNature.Text)
        End If
        If INDsleAccount.EditValue Is Nothing Then
            _listaValidaciones.AppendLine(INDliAccountAccounting.Text)
        End If
        If ValueConcept = 0 Then
            _listaValidaciones.AppendLine(INDlyItemValue.Text)
        End If
        If INDliCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDsleCostCenter.EditValue Is Nothing Then
                _listaValidaciones.AppendLine(INDliCostCenter.Text)
            End If
        End If

        'validaciones espec�ficas de cada comportamiento
        Select Case Behavior
            Case CInt(eBehavior.TransferBetweenBanks)  'Traslado entre bancos
                If INDsleEntityBankAccount.EditValue Is Nothing Then
                    _listaValidaciones.AppendLine(INDliEntityBankAccount.Text)
                End If
            Case CInt(eBehavior.PettyCash)
            Case CInt(eBehavior.PaymentAdvancePaymentInvoices)  'Pago/Anticipo Facturas CxP
                If ListDischargeBill IsNot Nothing AndAlso ListDischargeBill.Count > 0 Then
                    For Each acc As DischargeBill In ListDischargeBill
                        If acc.IdPaymentConcept = 0 Then
                            If acc.AdvancedValue <> 0 Then
                                _listaValidaciones.AppendLine(String.Format(ResourceManager.GetString("Invoice", NAME_MODULE), acc.AccountPayableBillNumber))
                                'Return False
                            End If
                        Else
                            If acc.AdvancedValue = 0 Then
                                _listaValidaciones.AppendLine(String.Format(ResourceManager.GetString("Invoice", NAME_MODULE), acc.AccountPayableBillNumber))
                            End If
                        End If
                    Next
                Else
                    If ValueAdvancePayment = 0 Then
                        _listaValidaciones.AppendLine(ResourceManager.GetString("Invoice", NAME_MODULE))
                    End If
                End If
            Case CInt(eBehavior.ReturningImprestRC)  'Devolutivos de Anticipos RC
            Case CInt(eBehavior.PettyCashReimbursement)  'Reembolso de Caja Menor
        End Select
        If _listaValidaciones.Length > 0 Then
            Return False
        Else
            'validacion de la retenci�n
            If INDlgrConceptRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                Return ValidateControlsRetentionConcept()
            End If
            Return True
        End If
    End Function

    ''' <summary>
    ''' Valida los controles de conceptos de retencion
    ''' </summary>
    Private Function ValidateControlsRetentionConcept() As Boolean
        If INDsleConceptRetention.EditValue Is Nothing Then
            _listaValidaciones.AppendLine(INDlyItemConceptRetention.Text)
        End If
        If INDsePercentage.EditValue Is Nothing Then
            _listaValidaciones.AppendLine(INDlyItemPercentage.Text)
        End If
        If INDtxtRetentionValue.EditValue Is Nothing Then
            _listaValidaciones.AppendLine(INDlyItemBaseValue.Text)
        End If
        If INDTxtBillingValue.EditValue Is Nothing Then
            _listaValidaciones.AppendLine(INDLciBillingValue.Text)
        End If
        If INDtxtBaseValue.EditValue Is Nothing Then
            _listaValidaciones.AppendLine(INDlyItemRetentionValue.Text)
        End If
        If INDsleConceptRetention.EditValue Is Nothing Or INDsePercentage.EditValue Is Nothing Or INDtxtRetentionValue.EditValue Is Nothing Or INDtxtBaseValue.EditValue Is Nothing Then
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' M�todo que convierte valor a porcentaje y lo asigna al campo porcentaje de la rejilla
    ''' </summary>
    Private Sub ConvertValueToPercent()
        Dim accountPayableComplx As DischargeBill = DirectCast(INDGvInvoices.GetFocusedRow, DischargeBill)
        If accountPayableComplx.AdvancedValue <> 0 Then
            If accountPayableComplx.AdvancedValue > accountPayableComplx.AccountPayableShareBalance Then
                accountPayableComplx.AdvancedValue = 0
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("PayValueMoreAccountPayableValue", NAME_MODULE)
            End If
            accountPayableComplx.AdvancePercent = TreasuryStaticServices.ConvertInvoiceValueToPercent(accountPayableComplx.AdvancedValue, accountPayableComplx.AccountPayableShareBalance)
        End If
    End Sub

    ''' <summary>
    ''' M�todo que convierte de porcentaje a valor en pesos
    ''' </summary>
    Private Sub ConvertPercentToValue()
        Dim accountPayableComplx As DischargeBill = DirectCast(INDGvInvoices.GetFocusedRow, DischargeBill)
        If accountPayableComplx.AdvancePercent <> 0 Then
            accountPayableComplx.AdvancedValue = TreasuryStaticServices.ConvertInvoicePercentToValue(accountPayableComplx.AdvancePercent, accountPayableComplx.AccountPayableShareBalance)
            INDGvInvoices.RefreshData()
        End If
    End Sub

    Public Function getVoucherTransactionD() As VoucherTransactionDetails
        Return _voucherDetail
    End Function

    ''' <summary>
    ''' Asigna los valores a la entidad detalle
    ''' </summary>
    Private Sub AssigningValues()
        _voucherDetail = New VoucherTransactionDetails()
        With _voucherDetail
            .Id = _idVoucherTransactionDetail
            .CashRegisterId = CashRegisterId
            .IdEntityBankAccount = EntityBankAccountId
            .IdMainAccount = MainAccountId
            .Nature = Nature
            .IdCostCenter = CostCenter
            .Value = ValueConcept
            .BaseValueDiscount = ApplyDiscountValue
            .Detail = Observation
            .FullNameMainAccount = INDsleAccount.Text
            .FullNameCostCenter = INDsleCostCenter.Text
            .TaxRegistration = TaxRegistration
            .EconomicActivityId = EconomicActivity

            If CashFlowConceptId > 0 Then
                .IdCashFlowConcept = CashFlowConceptId
                .CodeNameCashFlowConcept = CashFlowConceptCodeName
            Else
                .IdCashFlowConcept = Nothing
                .CodeNameCashFlowConcept = String.Empty
            End If

            .discountableIVA = DiscountableIVA
            .IdGeneralLedgerIVA = IdGeneralLedgerIVA
            .ValueIVA = ValueIVA

            If Me._behavior <> 6 Then
                .TotalConcept = ValueConcept
            Else
                If INDlyItemValueIVA.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .TotalConcept = TotalConcept
                Else
                    .TotalConcept = ValueConcept
                End If
            End If

            If .IdGeneralLedgerIVA IsNot Nothing Then
                _voucherDetail.VoucherTransactionDetailsAccountInfo.Clear()
                If Me.TaxRegistration = 2 Then
                    _voucherDetail.VoucherTransactionDetailsAccountInfo.Add(New VoucherTransactionDetailAccountInfo With {
                                                                                                                            .MainAccountCodeName = INDsleAccount.Text,
                                                                                                                            .CostCenterCodeName = INDsleCostCenter.Text,
                                                                                                                            .NatureName = IIf(Nature = 1, "Debito", "Credito"),
                                                                                                                            .ValueTotalConcept = ValueConcept
                                                                                                                        })

                    Using Model As New MAccountPayable(Me._myTag)
                        _purchaseService = Model.GetMainAccountById(_generalLedgerIVA.ObjectEmbbeded.IdAccountPurchaseService)
                    End Using

                    _voucherDetail.VoucherTransactionDetailsAccountInfo.Add(New VoucherTransactionDetailAccountInfo With {
                                                                                                                            .MainAccountCodeName = _purchaseService.NumberName,
                                                                                                                            .CostCenterCodeName = "",
                                                                                                                            .NatureName = IIf(Nature = 1, "Debito", "Credito"),
                                                                                                                            .ValueTotalConcept = ValueIVA
                                                                                                                        })
                ElseIf Me.TaxRegistration = 1 Then
                    _voucherDetail.VoucherTransactionDetailsAccountInfo.Add(New VoucherTransactionDetailAccountInfo With {
                                                                                                                            .MainAccountCodeName = INDsleAccount.Text,
                                                                                                                            .CostCenterCodeName = INDsleCostCenter.Text,
                                                                                                                            .NatureName = IIf(Nature = 1, "Debito", "Credito"),
                                                                                                                            .ValueTotalConcept = ValueConcept + ValueIVA
                                                                                                                        })

                    Using Model As New MAccountPayable(Me._myTag)
                        _accountDebit = Model.GetMainAccountById(_generalLedgerIVA.ObjectEmbbeded.IdAccountDebitControlFiscal)
                        _accountCredit = Model.GetMainAccountById(_generalLedgerIVA.ObjectEmbbeded.IdAccountCreditControlFiscal)
                    End Using

                    _voucherDetail.VoucherTransactionDetailsAccountInfo.Add(New VoucherTransactionDetailAccountInfo With {
                                                                                                                          .MainAccountCodeName = _accountDebit.NumberName,
                                                                                                                          .CostCenterCodeName = "",
                                                                                                                          .NatureName = ResourceManager.GetString("AccountNatureDebit"),
                                                                                                                          .ValueTotalConcept = ValueIVA
                                                                                                                            })

                    _voucherDetail.VoucherTransactionDetailsAccountInfo.Add(New VoucherTransactionDetailAccountInfo With {
                                                                                                                            .MainAccountCodeName = _accountCredit.NumberName,
                                                                                                                            .CostCenterCodeName = "",
                                                                                                                            .NatureName = ResourceManager.GetString("AccountNatureCredit"),
                                                                                                                            .ValueTotalConcept = ValueIVA
                                                                                                                            })
                ElseIf Me.TaxRegistration = 4 Then

                    _voucherDetail.VoucherTransactionDetailsAccountInfo.Add(New VoucherTransactionDetailAccountInfo With {
                                                                                                                           .MainAccountCodeName = INDsleAccount.Text,
                                                                                                                           .CostCenterCodeName = INDsleCostCenter.Text,
                                                                                                                           .NatureName = IIf(Nature = 1, "Debito", "Credito"),
                                                                                                                           .ValueTotalConcept = ValueConcept
                                                                                                                       })


                    _voucherDetail.VoucherTransactionDetailsAccountInfo.Add(New VoucherTransactionDetailAccountInfo With {
                                                                                                                            .MainAccountCodeName = INDsleAccount.Text,
                                                                                                                            .CostCenterCodeName = INDsleCostCenter.Text,
                                                                                                                            .NatureName = IIf(Nature = 1, "Debito", "Credito"),
                                                                                                                            .ValueTotalConcept = ValueIVA,
                                                                                                                            .Observation = $"IVA {_generalLedgerIVA?.ObjectEmbbeded?.Percentage} % - {_generalLedgerIVA?.ObjectEmbbeded?.Name}"
                                                                                                                        })

                End If
            End If

            Select Case VoucherClass
                Case eVoucherClass.Payment
                    .IdThirdParty = ThirdParthy
                    .IdExpenseConcept = ExpenseConcept
                    .PercentRetention = PercentRetention
                    .IdRetentionConcept = IdRetentionConcept
                    .FullNameThirdParty = INDsleThirdParty.Text
                    If _expenseConcept IsNot Nothing Then
                        .ExpenseConceptCode = _expenseConcept.Code
                        .ExpenseConceptName = _expenseConcept.Description
                        .ExpenseConceptBehavior = _expenseConcept.Behavior
                    Else
                        .ExpenseConceptCode = _voucherTransactionDetailPrevius.ExpenseConceptCode
                        .ExpenseConceptName = _voucherTransactionDetailPrevius.ExpenseConceptName
                        .ExpenseConceptBehavior = _voucherTransactionDetailPrevius.ExpenseConceptBehavior
                    End If

                    .AdvanceValue = CDec(Me.ValueAdvancePayment) 'valor del anticipo
                    .ValueAdvanceInCurrencyHeader = Me.ValueInCurrencyHeaderAdvance 'VALOR DEL ANTICIPO EN LA MONEDA DE LA CABECERA
                    .CurrencyAdvance = New Currency With {.Id = Me.CurrencyAdvancedId, .Abbreviation = INDsleCurrency.Text}  'moneda seleccionada para el anticipo
                    .TRMValueAdvance = Me.TRMValueAdvanced 'valor del TRM conrespecto a la moneda de la cabecera
                    .AdvanceDetail = AdvanceDetail  'detalle del anticipo
                    .BillingValue = INDTxtBillingValue.EditValue
                    .BaseValue = INDtxtBaseValue.EditValue
                    If .Nature = 1 Then
                        .NatureName = ResourceManager.GetString("AccountNatureDebit")
                    Else
                        .NatureName = ResourceManager.GetString("AccountNatureCredit")
                    End If
                    'Asignar el listado de las facturas a eliminar
                    .DischargeBillDelete = _listDischargeBillDelete

                    If Me.ListTreasuryAdvances?.Any() Then
                        Dim i = 0
                        For Each item In Me.ListTreasuryAdvances
                            If i > 1 Then
                                item.MarkAsDeleted()
                            End If
                            .TreasuryAdvances.Add(item)
                            i += 1
                        Next
                    End If
                Case eVoucherClass.Refund
                    .EntityCode = INDsleCashRegister.Text.ToString().Split("-").ElementAt(0)
                    .EntityName = INDsleCashRegister.Text.ToString().Split("-").ElementAt(1)
                    .CurrencyCashRegister = New Currency With {.Id = Me.CurrencyIdCashRegisterRefund, .Abbreviation = INDSleCurrencyCashRegister.Text}
                Case eVoucherClass.Transfer
                    If ExpenseType = 1 Then
                        If INDsleEntityBankAccount.Text.ToString() <> String.Empty Then
                            .EntityCode = INDsleEntityBankAccount.Text.ToString().Split("-").ElementAt(0)
                            .EntityName = INDsleEntityBankAccount.Text.ToString().Split("-").ElementAt(1)
                        Else
                            .EntityCode = EntityCode
                            .EntityName = EntityName
                        End If
                    ElseIf ExpenseType = 3 Then
                        .EntityCode = INDsleCashRegister.Text.ToString().Split("-").ElementAt(0)
                        .EntityName = INDsleCashRegister.Text.ToString().Split("-").ElementAt(1)
                    End If
            End Select
        End With
        If _listVoucherDetail Is Nothing Then
            _listVoucherDetail = New List(Of VoucherTransactionDetails)()
        End If
        _listVoucherDetail.Add(_voucherDetail)
    End Sub

    ''' <summary>
    ''' Initializes the invoice share.
    ''' </summary>
    Private Sub InitializeInvoiceShare()
        Using ModelXpo As New MBusqueda
            Dim filter() As Object = {ThirdParthy, MainAccountId, CInt(eStatusAccountPayable.Confirmado)}
            AccountPayableShareDatasource = ModelXpo.ConsultarEntidades(eDataSource.ListAccountPayableSharesByIdThirdIdAccountAndState, filter)
        End Using
    End Sub

    ''' <summary>
    ''' Inicia el datasource de los conceptos de pago
    ''' </summary>
    Private Sub InitializePaymentConcept()
        Using ModelXpo As New MBusqueda
            PaymentConceptDatasource = ModelXpo.ConsultarEntidades(eDataSource.ListAllPaymentConcept)
        End Using
    End Sub

    ''' <summary>
    ''' Carga el datasource de cajas
    ''' </summary>
    Private Sub InitializeCashRegister(ByVal type As Integer)
        Using Model As New MBusqueda
            Dim filter() As Object = {indigo.UserIndigoId, type, True} 'Cajas menores activas
            Me.CashDatasource = Model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCashRegisterByUser, filter)
        End Using
    End Sub

    ''' <summary>
    ''' Deshacers this instance.
    ''' </summary>
    Private Sub Deshacer()
        CleanControls()
        Select Case VoucherClass
            Case eVoucherClass.Payment
                INDsleExpenseConcept.Focus()
            Case eVoucherClass.Transfer
                If ExpenseType = 1 Then
                    INDsleEntityBankAccount.Focus()
                ElseIf ExpenseType = 3 Then
                    INDsleCashRegister.Focus()
                End If
            Case eVoucherClass.Refund
                INDsleCashRegister.Focus()
        End Select
    End Sub

    ''' <summary>
    ''' Oculta los layouts por defecto
    ''' </summary>
    Private Sub CleanLayouts()
        INDlgrInvoicePayment.HideControl(True)
        INDlgrAdvance.HideControl(True)
        INDliEntityBankAccount.HideControl(True)
        INDlgrAdvancePayments.HideControl(True)
        INDlgrRefound.HideControl(True)
        INDlgrConceptRetention.HideControl(True)
    End Sub

    ''' <summary>
    ''' Converts the value to percent text.
    ''' </summary>
    Public Sub ConvertValueToPercentText()
        If PayValue <> 0 AndAlso Balance <> 0 Then
            If PayValue > Balance Then
                PayValue = 0
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("PayValueMoreAccountPayableValue", NAME_MODULE)
            Else
                INDtxtPayPercent.EditValue = TreasuryStaticServices.ConvertInvoiceValueToPercent(PayValue, Balance)
                If _shareInvoice IsNot Nothing Then
                    ApplyDiscount(_shareInvoice)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Converts the percent to value text.
    ''' </summary>
    Public Sub ConvertPercentToValueText()
        If PayPercent <> 0 AndAlso Balance <> 0 Then
            If PayPercent > 100 Then
                PayPercent = 100
            End If
            Me.PayValue = TreasuryStaticServices.ConvertInvoicePercentToValue(PayPercent, Balance)
            If _shareInvoice IsNot Nothing Then
                ApplyDiscount(_shareInvoice)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        IsEditMode = False
        CleanControls()
        CleanControlsRetention()
    End Sub

    ''' <summary>
    ''' Elimina una cuota de factura del listado
    ''' </summary>
    Private Sub DeleteInvoiceShare()
        If _listDischargeBillDelete Is Nothing Then
            _listDischargeBillDelete = New List(Of DischargeBill)()
        End If
        Dim invoiceShar As DischargeBill = DirectCast(INDGvInvoices.GetFocusedRow(), DischargeBill)
        _listDischargeBillDelete.Add(invoiceShar)
        ListDischargeBill.Remove(invoiceShar)
        AccountPayableDatasource = Nothing
        AccountPayableDatasource = ListDischargeBill

        If ListDischargeBill.FindAll(Function(x) x.AccountPayableBillNumber.Equals(invoiceShar.AccountPayableBillNumber)).Count = 0 Then
            If _dictionaryShares.ContainsKey(invoiceShar.AccountPayableBillNumber) Then
                _dictionaryShares.Remove(invoiceShar.AccountPayableBillNumber)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Valida los controles de cuotas de facturas
    ''' </summary>
    ''' <returns></returns>
    Private Function validateControlsInvoiceShare() As Boolean
        _listaValidaciones = New StringBuilder()
        Dim ValidationResult As Boolean = True
        'validaciones comunes
        If INDsleInvoiceShare.EditValue Is Nothing Or CInt(INDsleInvoiceShare.EditValue) = 0 Then
            ValidationResult = False
            _listaValidaciones.AppendLine(INDliInvoiceShare.Text)
        End If
        If INDtxtPayValue.EditValue Is Nothing Or CDec(INDtxtPayValue.EditValue) = 0 Then
            ValidationResult = False
            _listaValidaciones.AppendLine(INDliPayValue.Text)
        End If
        If IdPaymentConcept = 0 Then
            ValidationResult = False
            _listaValidaciones.AppendLine(INDliPaymentConcept.Text)
        End If
        Return ValidationResult
    End Function

    ''' <summary>
    ''' Cleans the controls invoice.
    ''' </summary>
    Private Sub CleanControlsInvoice()
        INDlyPopupConcept.BeginUpdate()
        INDsleInvoiceShare.EditValue = Nothing
        INDlbBalance.Text = "$0"
        INDlbDateExpire.Text = String.Empty
        Me.PayValue = 0F
        INDtxtPayPercent.EditValue = 0
        INDsleInvoiceShare.Focus()
        _valueChanging = False
        _percentChanging = False
        INDslePaymentConcept.EditValue = Nothing
        Me.HideOrShowTRMControls(eTRMControls.Billing)
        INDlyPopupConcept.EndUpdate()
    End Sub

    ''' <summary>
    ''' valida el orden en que se deben pagar las facturas
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateInvoicesPaymentOrder() As Boolean
        Dim listError As New List(Of String)
        For Each accountSharedTmp In ListDischargeBill
            Dim listShared = _dictionaryShares(accountSharedTmp.AccountPayableBillNumber)
            Dim index As Integer
            For index = accountSharedTmp.AccountPayableShareShare - 1 To 1 Step -1
                Dim query = ListDischargeBill.Where(Function(y) y.AccountPayableShareShare = index And y.AccountPayableBillNumber = accountSharedTmp.AccountPayableBillNumber)
                If query.Count > 0 Then
                    Dim sharedComplexTmp = query.ToList().Item(0)
                    If sharedComplexTmp.AdvancePercent <> 100 Then
                        listError.Add(String.Format(ResourceManager.GetString("ErrorShareMessage", NAME_MODULE), accountSharedTmp.AccountPayableShareShare, accountSharedTmp.AccountPayableBillNumber))
                        Continue For
                    End If
                Else
                    Dim sharedXpoTmp = listShared.Where(Function(x) x.Share = index).ToList.Item(0)
                    If sharedXpoTmp.Balance <> 0 Then
                        listError.Add(String.Format(ResourceManager.GetString("ErrorShareMessage", NAME_MODULE), accountSharedTmp.AccountPayableShareShare, accountSharedTmp.AccountPayableBillNumber))
                        Continue For
                    End If
                End If
            Next
        Next
        If listError.Count > 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub GetThirdParty()
        Using model As New MThirdParty(_myTag)
            _dataThirdParty = model.GetThirdPartyByIdSimple(ThirdParthy)
        End Using
    End Sub

    ''' <summary>
    ''' Habilita los controles necesarios dependiendo del comportamiento del concepto
    ''' </summary>
    Private Sub LoadConceptControls()
        If ExpenseConcept <> 0 Then
            CleanLayouts()
            CleanControlsChangeConcept()
            INDtxtTotalConcept.ReadOnly = True
            INDtxtValueIVA.ReadOnly = True

            Dim expenseConceptXpo = Nothing
            Dim IdMainAccountExpense = Nothing
            Dim _nature As Integer = 1
            _expenseConcept = New ExpenseConcepts()
            If IsEditMode OrElse INDGvExpenseConcept.GetFocusedRow() Is Nothing OrElse INDGvExpenseConcept.GetFocusedRow().GetType().Name.Equals(GetType(DevExpress.Data.NotLoadedObject).Name) Then
                Using Model As New MExpenseConcepts(Me.Tag)
                    expenseConceptXpo = Model.GetExpenseConceptByIdSimple(INDsleExpenseConcept.EditValue)
                    IdMainAccountExpense = expenseConceptXpo.IdMainAccount
                    _expenseConcept.Nature = expenseConceptXpo.Nature
                    _nature = expenseConceptXpo.Nature
                End Using
            Else
                expenseConceptXpo = DirectCast(DirectCast(INDGvExpenseConcept.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, ExpenseConceptXpo)
                IdMainAccountExpense = expenseConceptXpo.IdMainAccount.Id
                _expenseConcept.Nature = expenseConceptXpo.Nature
                If expenseConceptXpo.Nature = 1 Then
                    _nature = 1
                Else
                    _nature = 2
                End If
            End If
            _expenseConcept.Code = expenseConceptXpo.Code
            _expenseConcept.Description = expenseConceptXpo.Description
            _expenseConcept.Behavior = expenseConceptXpo.Behavior
            Me._behavior = expenseConceptXpo.Behavior
            If IdMainAccountExpense > 0 Then
                INDliAccountAccounting.HideControl(False)
                INDsleAccount.EditValue = IdMainAccountExpense
            Else
                INDliAccountAccounting.HideControl(True)
                INDsleAccount.EditValue = Nothing
            End If
            If expenseConceptXpo IsNot Nothing Then
                If expenseConceptXpo.GetType Is GetType(ExpenseConcepts) Then
                    If expenseConceptXpo.CashFlowConcept IsNot Nothing Then
                        CashFlowConceptCodeName = String.Format("{0} - {1}", expenseConceptXpo.CashFlowConcept.Code, expenseConceptXpo.CashFlowConcept.NameConcept)
                        CashFlowConceptId = expenseConceptXpo.CashFlowConcept.Id
                    Else
                        CashFlowConceptCodeName = String.Empty
                        CashFlowConceptId = 0
                    End If
                ElseIf expenseConceptXpo.GetType Is GetType(ExpenseConceptXpo) Then
                    If expenseConceptXpo.IdCashFlowConcept IsNot Nothing Then
                        CashFlowConceptCodeName = String.Format("{0} - {1}", expenseConceptXpo.IdCashFlowConcept.Code, expenseConceptXpo.IdCashFlowConcept.NameConcept)
                        CashFlowConceptId = expenseConceptXpo.IdCashFlowConcept.Id
                    Else
                        CashFlowConceptCodeName = String.Empty
                        CashFlowConceptId = 0
                    End If
                End If

                If IsEditMode Then
                    INDgleNature.EditValue = Nature
                Else
                    INDgleNature.EditValue = _nature
                End If

                INDgleNature.Enabled = False
                Me.BarraBotones.StatusRecordVisible = False
                Me.BarraBotones.Minimizar(True)
                _statePopUpInvoice = False
                Select Case expenseConceptXpo.Behavior
                    Case CInt(eBehavior.TransferBetweenBanks), CInt(eBehavior.EndorsementBills)  'Traslado entre bancos
                        INDliEntityBankAccount.HideControl(False)
                        INDtxtValue.Enabled = True
                    Case CInt(eBehavior.PettyCash)  'Caja Menor
                        Me.BarraBotones.Minimizar(False)
                        Me.BarraBotones.StatusRecordVisible = True
                        INDlgrInvoicePayment.HideControl(False)
                        INDtxtValue.Enabled = False
                        LoadAdvance()
                        LoadPaymentInvoicesByThird()
                    Case CInt(eBehavior.PaymentAdvancePaymentInvoices)  'Pago/Anticipo Facturas CxP
                        Me.BarraBotones.Minimizar(False)
                        Me.BarraBotones.StatusRecordVisible = True
                        INDlgrInvoicePayment.HideControl(False)
                        INDlgrAdvance.HideControl(False)
                        ValueConcept = 0F
                        INDtxtValue.Enabled = False
                        LoadAdvance()
                        LoadPaymentInvoicesByThird()
                    Case CInt(eBehavior.ReturningImprestRC)  'Devolutivos de Anticipos RC
                        INDlgrAdvancePayments.HideControl(False)
                        ValueConcept = 0F
                        INDtxtValue.Enabled = False
                        LoadPortfolioAdvanceByTird(IdMainAccountExpense)
                    Case CInt(eBehavior.PettyCashReimbursement)  'Reembolso de Caja Menor
                        INDlgrRefound.HideControl(False)
                        ValueConcept = 0F
                        INDtxtValue.Enabled = False
                        LoadRefundByCashRegister()
                    Case CInt(eBehavior.None)
                        INDgleNature.Enabled = True
                End Select
                Me.RefreshValueConcept()
            End If
        End If
    End Sub

    Private Sub CleanControlsChangeConcept()
        INDsleEntityBankAccount.EditValue = Nothing
        INDsleAccount.EditValue = Nothing
        INDsleCostCenter.EditValue = Nothing
        Observation = Nothing
        INDgleNature.EditValue = 1
        ValueAdvancePayment = 0

        CleanControlsRetention()
        INDGcInvoices.DataSource = Nothing

        'Controls Invoice
        INDsleInvoiceShare.EditValue = Nothing
        INDlbBalance.Text = String.Empty
        INDlbDateExpire.Text = String.Empty
        Me.PayValue = 0F
        INDtxtPayPercent.EditValue = 0
        INDslePaymentConcept.EditValue = Nothing

        INDsleConceptRetention.EditValue = Nothing
        INDtxtValue.Enabled = True
        CashFlowConceptCodeName = Nothing
        CashFlowConceptId = 0
    End Sub

    ''' <summary>
    ''' carga todos los anticipos de cartera del usuario
    ''' </summary>
    Private Sub LoadPortfolioAdvanceByTird(Optional expenseMainAccountId As Integer? = Nothing)
        Using Model As New MBusqueda
            Dim filter() As Object = {ThirdParthy, Me._currencyHeader?.Id, expenseMainAccountId}
            PortfolioAdvanceDatasource = Model.ConsultarEntidades(eDataSource.GetAllPortfolioAdvanceByThirdId, filter)
            If PortfolioAdvanceDatasource IsNot Nothing AndAlso PortfolioAdvanceDatasource.Count > 0 AndAlso VoucherTransactionAdvance IsNot Nothing AndAlso VoucherTransactionAdvance.Count > 0 Then
                For Each itemAdvance As Infrastructure.Data.Xpo.PortfolioRepository.Portfolio_PortfolioAdvance In PortfolioAdvanceDatasource
                    Dim voucherAdvance As VoucherTransactionAdvance = VoucherTransactionAdvance.Where(Function(x) x.PortfolioAdvanceId = itemAdvance.Id).FirstOrDefault()
                    If voucherAdvance IsNot Nothing Then
                        itemAdvance.VoucherTransactionDId = voucherAdvance.IdVoucherTransactionD
                        itemAdvance.VoucherTransactionAdvanceId = voucherAdvance.Id
                        itemAdvance.PayValue = voucherAdvance.Value
                        itemAdvance.PercentValue = voucherAdvance.Percentage
                    End If
                Next
            End If
            If PortfolioAdvanceDatasource Is Nothing OrElse PortfolioAdvanceDatasource.Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("AdvanceBackNotFound", NAME_MODULE)
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Carga el valor total de los anticipos en el control de usuario en la barra de botones
    ''' </summary>
    Private Async Sub LoadAdvance()
        Using Model As New MAdvancePayments(Me.Tag)
            Dim _listAdvancePayment As List(Of AdvancePayments) = (Await Model.ListAdvancePaymentByThirdId(ThirdParthy)).ObjectEmbbeded
            If _listAdvancePayment IsNot Nothing AndAlso _listAdvancePayment.Count > 0 Then
                ctrAdvance.ThirdId = ThirdParthy
                ctrAdvance.PrintAdvance()
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Evento que asigna el footer si hay datos en la rejilla
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDgcAdvancePayment_DataSourceChanged(sender As Object, e As EventArgs) Handles INDgcPortfolioAdvance.DataSourceChanged
        If PortfolioAdvanceDatasource IsNot Nothing AndAlso PortfolioAdvanceDatasource.Count > 0 Then
            INDgvPortfolioAdvance.OptionsView.ShowFooter = True
        Else
            INDgvPortfolioAdvance.OptionsView.ShowFooter = False
        End If
    End Sub

    ''' <summary>
    ''' valida el listado devuelto por el Copy_Paste contra el listado de la rejilla
    ''' </summary>
    ''' <param name="listApComplex">The list ap complex.</param>
    ''' <returns></returns>
    Private Function validateListAccountPayableComplex(ByVal listApComplex As List(Of DischargeBill)) As List(Of String)
        Dim errorList As New List(Of String)
        Dim listValidate As New List(Of DischargeBill)()
        listValidate.AddRange(listApComplex)
        If ListDischargeBill IsNot Nothing AndAlso ListDischargeBill.Count > 0 Then
            For Each APC As String In listValidate.Select(Function(x) x.AccountPayableBillNumber).ToList().Distinct().ToList()
                If ListDischargeBill.FindAll(Function(x) x.AccountPayableBillNumber.Equals(APC)).Cast(Of DischargeBill).Count() > 0 Then
                    errorList.Add(String.Format(ResourceManager.GetString("InvoiceListExist", NAME_MODULE), APC))
                    listApComplex.RemoveAll(Function(x) x.AccountPayableBillNumber.Equals(APC))
                End If
            Next
        End If
        Return errorList
    End Function

    ''' <summary>
    ''' valida los datos copiados del excel
    ''' </summary>
    ''' <param name="rows">The rows.</param>
    ''' <returns></returns>
    Private Async Function ValidatePasteToGridInvoice(ByVal rows As List(Of List(Of String)), ByVal SupplierId As Integer) As Task(Of Tuple(Of List(Of DischargeBill), List(Of String)))
        Dim _accountPayableRepository As IAccountPayableRepository = Nothing
        Dim _paymentConceptRepository As IPaymentConceptRepository = Nothing


        Dim errorList As New List(Of String)
        Dim _ListAccountPayableComplexExcel As New List(Of DischargeBill)
        Dim accountPayable As AccountPayable = Nothing
        Dim _listAccountPayableShare As List(Of AccountPayableShares) = Nothing
        Dim _paymentConcept As TreasuryPaymentConcepts = Nothing

        For Each row As List(Of String) In rows
            If row.Count = 3 Then
                Dim invoice As String = row.ElementAt(0).Trim()
                If _ListAccountPayableComplexExcel.FindAll(Function(x) x.AccountPayableBillNumber = invoice).Cast(Of DischargeBill).ToList().Count = 0 Then
                    If IsNumeric(row.ElementAt(1)) Then
                        Dim paymentConceptPaste As String = row.ElementAt(2).Trim()
                        If Not String.IsNullOrEmpty(paymentConceptPaste) Then
                            Using ModelPayment As New MPaymentConcept(Me.Tag)
                                _paymentConcept = (Await ModelPayment.GetPaymentConcept(paymentConceptPaste)).ObjectEmbbeded
                                'consulta con el repositorio
                            End Using
                            If _paymentConcept IsNot Nothing AndAlso _paymentConcept.Id > 0 Then
                                Using Model As New MAccountPayable(Me.Tag)
                                    accountPayable = Await Model.GetAccountPayableByBillNumberAndMainAccount(invoice, SupplierId, MainAccountId)
                                End Using
                                If accountPayable IsNot Nothing AndAlso accountPayable.Id > 0 Then
                                    If accountPayable.AccountPayableShares IsNot Nothing AndAlso accountPayable.AccountPayableShares.Count > 0 Then
                                        _listAccountPayableShare = accountPayable.AccountPayableShares.Where(Function(x) x.Balance > 0).Cast(Of AccountPayableShares).ToList()
                                        Dim invoiceValue As Decimal = Convert.ToDecimal(row.ElementAt(1))
                                        If _listAccountPayableShare.Sum(Function(x) x.Balance) >= invoiceValue Then
                                            For Each shares As AccountPayableShares In _listAccountPayableShare
                                                If invoiceValue > 0 Then
                                                    Dim _accountPayableCompl As New DischargeBill()
                                                    With _accountPayableCompl
                                                        .IdAccountPayable = shares.IdAccountPayable
                                                        .IdAccountPayableShare = shares.Id
                                                        .AccountPayableBillNumber = shares.AccountPayable.BillNumber
                                                        .AccountPayableShareDateExpires = shares.DateExpires
                                                        .AccountPayableShareBalance = shares.Balance
                                                        .AccountPayableShareShare = shares.Share
                                                        If invoiceValue >= shares.Balance Then
                                                            .AdvancedValue = shares.Balance 'invoiceValue
                                                            .AdvancePercent = 100
                                                            .IdPaymentConcept = _paymentConcept.Id 'IdPaymentConcept
                                                            invoiceValue -= shares.Balance
                                                        Else
                                                            .AdvancedValue = invoiceValue
                                                            .AdvancePercent = TreasuryStaticServices.ConvertInvoiceValueToPercent(invoiceValue, shares.Balance)
                                                            .IdPaymentConcept = _paymentConcept.Id 'IdPaymentConcept
                                                            invoiceValue = 0
                                                        End If
                                                    End With
                                                    _ListAccountPayableComplexExcel.Add(_accountPayableCompl)
                                                    If BudgetInterface Then
                                                        'Obtenemos los detalles presupuestales asociados a la cuenta por pagar
                                                        GetObligationDetailsByDischargeBill(_accountPayableCompl)
                                                    End If
                                                Else
                                                    Exit For
                                                End If
                                            Next
                                        Else
                                            errorList.Add(String.Format(ResourceManager.GetString("ValuePayGreaterParameter", NAME_MODULE), invoice))
                                        End If
                                    Else
                                        errorList.Add(String.Format(ResourceManager.GetString("InvoiceNoShares", NAME_MODULE), invoice))
                                    End If
                                Else
                                    errorList.Add(String.Format(ResourceManager.GetString("ThirdPartyNoInvoiceAssigned", NAME_MODULE), invoice))
                                End If
                            Else
                                errorList.Add(String.Format(ResourceManager.GetString("PaymentConceptNotFound", NAME_MODULE), paymentConceptPaste))
                            End If
                        Else
                            errorList.Add(ResourceManager.GetString("PaymentConceptEmpty", NAME_MODULE))
                        End If
                    Else
                        errorList.Add(String.Format(ResourceManager.GetString("PayValueNotNumeric", NAME_MODULE), invoice))
                    End If
                Else
                    errorList.Add(String.Format(ResourceManager.GetString("InvoiceListExist", NAME_MODULE), invoice))
                End If
            Else
                errorList.Add(String.Format(ResourceManager.GetString("RegistryNoDefinedStructure", NAME_MODULE), rows.IndexOf(row) + 1))
            End If
        Next
        Return New Tuple(Of List(Of DischargeBill), List(Of String))(_ListAccountPayableComplexExcel, errorList)
    End Function

    ''' <summary>
    ''' Consulta las obligaciones presupuestales asociadas a una cuenta por pagar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetObligationDetailsByDischargeBill(dischargeBill As DischargeBill)
        If dischargeBill.DischargeBillBudget Is Nothing OrElse Not dischargeBill.DischargeBillBudget.Any() Then
            Using model As New MVoucherTransaction(Me.Tag)
                Dim collectionObligation = model.GetObligationDetails(dischargeBill.IdAccountPayable, 1)
                If collectionObligation IsNot Nothing Then
                    For Each obligation In collectionObligation
                        Dim value As Decimal = Math.Round(obligation.Balance * dischargeBill.AdvancePercent / 100, 0)

                        dischargeBill.DischargeBillBudget.Add(New Domain.Entities.DischargeBillBudget With {
                        .ObligationDetailId = obligation.Id,
                        .CommitmentCode = obligation.CommitmentCode,
                        .CommitmentDocument = obligation.CommitmentDocument,
                        .CategoryCodeName = obligation.CategoryCodeName,
                        .FinancialSourceCodeName = obligation.FinancialSourceCodeName,
                        .RevenueTypeCodeName = obligation.RevenueTypeCodeName,
                        .Balance = obligation.Balance,
                        .Value = IIf(value > obligation.Balance, obligation.Balance, value)
                    })
                    Next
                End If
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Lee el listado de las facturas que se env�a desde el formulario principal
    ''' </summary>
    Public Async Sub LoadListDischargeBill(ByVal list As List(Of DischargeBill))
        ListDischargeBill = list
        Dim ListBillNumber As List(Of String)
        If ListDischargeBill IsNot Nothing AndAlso ListDischargeBill.Count > 0 Then
            ListBillNumber = (From acp In ListDischargeBill Select acp.AccountPayableBillNumber).ToList().Distinct().ToList()
        Else
            ListBillNumber = New List(Of String)
        End If
        Await LoadDictionarySharesAsync(ListBillNumber)
    End Sub

    ''' <summary>
    ''' Obtiene el valor del anticipo
    ''' </summary>
    ''' <returns></returns>
    Private Function getAdvanceTreasury() As Decimal
        Dim _advance As Decimal = 0
        _advance = _advanceValue
        Return _advance
    End Function

    ''' <summary>
    '''  methodo que muestra u oculta los controles del TRM
    ''' </summary>
    ''' <param name="TRMControls"></param>
    ''' <param name="value"></param>
    Private Sub HideOrShowTRMControls(TRMControls As eTRMControls, Optional value As Boolean = False)
        Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        _culture.NumberFormat = New Globalization.CultureInfo(Me._currencyHeader.Abbreviation.GetCultureId).NumberFormat

        Select Case TRMControls
            Case eTRMControls.Advance
                If Not value Then
                    INDLciCurrency.Visibility = If(_behavior = 6, DevExpress.XtraLayout.Utils.LayoutVisibility.Never, DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
                    INDLciTRM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDLciValueinCurrencyH.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    Me.CurrencyAdvanceDatasource = Nothing
                    Me.TRMValueAdvanced = 1
                    Me.ValueInCurrencyHeaderAdvance = Me.ValueAdvancePayment
                    INDLciValueinCurrencyH.Text = $"Valor"
                    If Me._currencyHeader IsNot Nothing Then
                        Me.INDtxtAdvancePayment.Properties.Mask.Culture = _culture
                        Me.INDtxtValueinCurrencyH.Properties.Mask.Culture = _culture
                        Me.INDsleCurrency.Properties.NullText = Me._currencyHeader?.Abbreviation
                        Me.CurrencyAdvancedId = Me._currencyHeader?.Id
                    Else
                        Me.CurrencyAdvancedId = Nothing
                        Me.INDsleCurrency.Properties.NullText = Nothing
                    End If
                    RefreshValueConcept()
                Else
                    INDLciCurrency.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDLciTRM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDLciValueinCurrencyH.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                End If
            Case eTRMControls.Billing
                If Not value Then
                    Me.INDLciValueBillCurrencyH.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    Me.INDtxtValueBillCurrencyH.EditValue = Nothing
                    Me.INDtxtPayValue.Properties.Mask.Culture = _culture
                    Me.TRMInvoice = 1
                Else
                    Me.INDLciValueBillCurrencyH.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    Me.ValueBillInCurrencyHeader = Me.PayValue
                End If
            Case eTRMControls.Refund
                If Not value Then
                    Me.INDLciTRMCashRegisterR.HideControl(Not value)
                    Me.INDLciValueinCashRegisterR.HideControl(Not value)
                    Me.INDtxtValueinCashRegisterR.Properties.Mask.Culture = _culture
                    Me.TRMCashRegisterRefund = 1
                    Me.ValueInCurrencyCashRegisterR = 0
                    Me.INDLciValueinCashRegisterR.Text = "Valor"
                Else
                    Me.INDLciCurrencyCashRegister.HideControl(Not value)
                    Me.INDLciTRMCashRegisterR.HideControl(Not value)
                    Me.INDLciValueinCashRegisterR.HideControl(Not value)
                End If
        End Select
    End Sub

    ''' <summary>
    ''' funcion asincrona que obtiene el valor del TRM Actual
    ''' </summary>
    ''' <returns></returns>
    Private Async Function GetTRMValue(fromCurrencyId As Integer?, ToCurrencyId As Integer?, TRMControls As eTRMControls) As Task(Of Decimal)

        If fromCurrencyId Is Nothing OrElse ToCurrencyId Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = $"Error Moneda parametro {If(fromCurrencyId Is Nothing, NameOf(fromCurrencyId), NameOf(ToCurrencyId))} vacio"
            Me.HideOrShowTRMControls(TRMControls)
            Return 1
            Exit Function
        End If

        Me.HideOrShowTRMControls(TRMControls, True)

        'si ya existe el TRM se consulta el guardado para no perder tiempo en ir a consultarlo de nuevo debido a que el trm es diario
        If _listTRM?.Any(Function(x) x.CurrencyId = ToCurrencyId AndAlso x.OfficialCurrencyId = fromCurrencyId And x.MeasurementDate.Day = GetDateServer.Day) Then
            Return _listTRM?.FirstOrDefault(Function(x) x.CurrencyId = ToCurrencyId AndAlso x.OfficialCurrencyId = fromCurrencyId And x.MeasurementDate.Day = GetDateServer.Day).Value
        End If

        'si no se a consultado previamente, se manda a consultar
        Using Model As New MPortfolioTransfers("")
            Dim Result = Await Model.GetTRMbyCurrencyId(ToCurrencyId, fromCurrencyId)

            If Result Is Nothing OrElse Not Result?.StateResult Then
                Me.HideOrShowTRMControls(TRMControls)
                Me.Mensaje(EeventViewerImages.Advertencia) = Result?.Message
                Return 1
            End If

            _listTRM = If(_listTRM Is Nothing, New List(Of TRM), _listTRM)
            _listTRM.Add(Result.ObjectEmbbeded)
            Me.Mensaje(EeventViewerImages.Informacion) = Result?.Message
            Return Result.ObjectEmbbeded.Value
        End Using
    End Function

    ''' <summary>
    ''' establece a los controles de valor moneda el formato numerico de la cabecera
    ''' </summary>
    ''' <param name="Abbreviation"></param>
    Public Sub SetCultureUIHeader(Abbreviation As String)
        Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        _culture.NumberFormat = Abbreviation.GetNumberFormat

        Me.INDtxtValue.Properties.Mask.Culture = _culture
        Me.INDtxtTRMCashRegisterR.Properties.Mask.Culture = _culture
        Me.INDtxtValueIVA.Properties.Mask.Culture = _culture
        Me.INDtxtValueinCashRegisterR.Properties.Mask.Culture = _culture
        Me.INDtxtTotalConcept.Properties.Mask.Culture = _culture
        Me.INDTxtBillingValue.Properties.Mask.Culture = _culture
        Me.INDtxtBaseValue.Properties.Mask.Culture = _culture
        Me.INDtxtRetentionValue.Properties.Mask.Culture = _culture
        Me.INDtxtValueBillCurrencyH.Properties.Mask.Culture = _culture
        Me.INDLciValueBillCurrencyH.Text = $"Valor ({Abbreviation})"
        Me.INDColPayValueAd = Window.Utils.FormatGrid(Me.INDColPayValueAd, Abbreviation)
        Me.INDColBalanceAd = Window.Utils.FormatGrid(Me.INDColBalanceAd, Abbreviation)
        Me.RepositoryItemValue.Mask.Culture = _culture
    End Sub

    ''' <summary>
    ''' establece a los controles de valor moneda el formato numerico de la caja menor
    ''' </summary>
    Public Sub SetCultureUIRefund(Abbreviation As String)
        Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        _culture.NumberFormat = Abbreviation.GetNumberFormat
        INDtxtValueinCashRegisterR.Properties.Mask.Culture = _culture
        INDColValueRefund = Window.Utils.FormatGrid(Me.INDColValueRefund, Abbreviation)
    End Sub

    ''' <summary>
    ''' establece el formato numerico del campo de anticipos
    ''' </summary>
    ''' <param name="Abbreviation"></param>
    Public Sub SetCultureUIAdvanced(Abbreviation As String)
        Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        _culture.NumberFormat = Abbreviation.GetNumberFormat
        Me.INDtxtAdvancePayment.Properties.Mask.Culture = _culture
    End Sub

    ''' <summary>
    ''' valida que tenga venga la moneda de la cabecera del formulario, paga comprobante de tipo pagos y reembolsos
    ''' </summary>
    Private Sub ValidateCurrencyHeader(_voucherClass As Byte, _currencyH As Currency)
        If {eVoucherClass.Refund, eVoucherClass.Payment}.Contains(_voucherClass) AndAlso _currencyH Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "La cabecera No tiene Moneda"
            Deshacer()
            Me.Close()
            Exit Sub
        End If
    End Sub

    ''' <summary>
    ''' funcion para ocultar los controles del iva
    ''' </summary>
    ''' <param name="value">false-oculta; true- muestra</param>
    Private Sub HideOrShowIVAControls(value As Boolean)
        INDliDiscountableIVA.HideControl(If(TaxRegistration <> 3, True, Not value))
        INDlyItemValueIVA.HideControl(Not value)
        INDlyItemTotalConcept.HideControl(Not value)
        INDliGeneralLedgerIVA.HideControl(Not value)
        If Not value Then
            IdGeneralLedgerIVA = Nothing
            ValueIVA = 0
        End If

    End Sub

    ''' <summary>
    ''' funcion para ejecutar el editvaluechanged de forma manual
    ''' </summary>
    ''' <returns></returns>
    Private Async Function EditValueChanged_CurrencyCashRegister(value As Boolean) As Task
        If value Then
            Exit Function
        End If
        If Me.CurrencyIdCashRegisterRefund IsNot Nothing AndAlso Me._currencyHeader?.Id IsNot Nothing AndAlso (CurrencyIdCashRegisterRefund <> Me._currencyHeader?.Id) Then
            Me.TRMCashRegisterRefund = Await Me.GetTRMValue(Me.CurrencyIdCashRegisterRefund, Me._currencyHeader?.Id, eTRMControls.Refund)
            Me.ValueConcept = Math.Round((Me.ValueInCurrencyCashRegisterR / Me.TRMCashRegisterRefund), 2)
        Else
            Me.HideOrShowTRMControls(eTRMControls.Refund)
        End If
    End Function

    ''' <summary>
    ''' evento asincrono al cambiar de factura
    ''' </summary>
    ''' <returns></returns>
    Private Async Function EditvalueChangedInvoiceShare() As Task
        Try
            AsyncLoader(True)
            _shareInvoice = Await Task.Factory.StartNew(Function() As Infrastructure.Data.Xpo.PaymentsRepository.AccountPayableSharesXpo
                                                            Return XpoServiceEx.Instance(indigo.TransactionalContainer).PaymentsService.GetXPOObject(Of Infrastructure.Data.Xpo.PaymentsRepository.AccountPayableSharesXpo)($"Id ={IdAccountPayableShare}")
                                                        End Function)
            AsyncLoader(False)
            INDlbDateExpire.Text = _shareInvoice.DateExpires
            Dim _culture = New CultureInfo(_shareInvoice.IdAccountPayable.Abbreviation.GetCultureId)
            Balance(_culture.NumberFormat) = _shareInvoice.Balance
            Me.INDtxtPayValue.Properties.Mask.Culture = _culture

            If Me._currencyHeader?.Id <> _shareInvoice?.IdAccountPayable?.CurrencyId Then
                Me.TRMInvoice = Await Me.GetTRMValue(_shareInvoice?.IdAccountPayable?.CurrencyId, Me._currencyHeader?.Id, eTRMControls.Billing)
            Else
                Me.HideOrShowTRMControls(eTRMControls.Billing)
            End If

            Dim View_SchedulePayment = _shareInvoice.IdAccountPayable?.View_SchedulePaymentXpo?.FirstOrDefault
            If View_SchedulePayment IsNot Nothing AndAlso View_SchedulePayment.CXPValue = (View_SchedulePayment.BalanceShare + View_SchedulePayment.ValueNote) Then
                PromptPaymentDiscount = 0
                If _shareInvoice.IdAccountPayable Is Nothing OrElse _shareInvoice.IdAccountPayable.Id = 0 Then
                    Return
                End If
                Dim _discountDays = DateDiff("d", _shareInvoice.IdAccountPayable.ServicePeriodDate, GetDateServer())
                Dim PromptPayment = _shareInvoice.IdAccountPayable.IdSupplier?.PromptPaymentDiscountXpo.Where(Function(d) _discountDays >= d.InitialRank And _discountDays <= d.EndRank).ToList().FirstOrDefault
                If PromptPayment IsNot Nothing And PromptPayment?.Id > 0 Then
                    PromptPaymentDiscount = PromptPayment.DiscountRate
                    INDtxtPromptPaymentDiscount.Enabled = True
                    ApplyDiscount(_shareInvoice)
                Else
                    INDtxtPromptPaymentDiscount.Enabled = False
                End If
            End If

            If AccountPayableDatasource IsNot Nothing AndAlso ListDischargeBill.Count > 0 Then
                If ListDischargeBill.FindAll(Function(x) x.AccountPayableShareShare = _shareInvoice.Share And x.AccountPayableBillNumber.Equals(_shareInvoice.IdAccountPayable.BillNumber)).Cast(Of DischargeBill).ToList().Count > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvoiceExists", NAME_MODULE)
                    CleanControlsInvoice()
                    Return
                End If
            End If
        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = ex.Message
        End Try
    End Function
#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        VoucherTransactionAdvance = Nothing
        IdCashRegisterValidate = Nothing
        ExpenseType = Nothing
        _listVoucherDetail = Nothing
        _voucherTransactionDetail = Nothing
        _voucherTransactionDetailPrevius = Nothing
        ctrAdvance = Nothing
        _listDischargeBillDelete = Nothing
        UnitOperativeId = Nothing
        DictionaryAgesPayments = Nothing
        _expenseConcept = Nothing
        _idVoucherTransactionDetail = Nothing
        _listaValidaciones = Nothing
        IsEditMode = Nothing
        _datasourcePaymentConceptLoad = Nothing
        _minBaseRetention = Nothing
        _retentiontype = Nothing
        _listAccountingRetention = Nothing
        _retConcept = Nothing
        _dictionaryShares = Nothing
        _shareInvoice = Nothing
        MainThird = Nothing
        _listAccountPayableShares = Nothing
        ListDischargeBill = Nothing
        _listAccountPayableDetail = Nothing
        _idCashRegister = Nothing
        _idEntityBankAccount = Nothing
        _voucherDetail = Nothing
        _behavior = Nothing
        ListNature = Nothing
        _statePopUpInvoice = Nothing
        _statePopUpPaymentConcept = Nothing
        _serverDate = Nothing
        _flagLoadControls = Nothing
    End Sub
    ''' <summary>
    ''' Handles the Load event of the FrmPopUpDisbursementVoucher control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmPopUpDisbursementVoucher_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._sessionValue = SessionValues.Instance
        Me.LayoutControls.SetIsCustomizable(Me.INDlyPopupConcept, True)
        INDesbInvoice.AddRangeColumns(INDcolInvoice.Caption, INDcolPayValue.Caption, INDcolPaymentConcept.Caption)
        IndigoGridControl1.RefreshGrid(INDGcInvoices)
        IndigoGridControl1.RefreshGrid(INDgcPortfolioAdvance)
        IndigoGridControl1.RefreshGrid(INDgcRefund)
        ctrAdvance.PrintAdvance()
        _statePopUpInvoice = False
        _statePopUpPaymentConcept = False

        colBudgetInterface.Visible = BudgetInterface
        colBudgetInterface.OptionsColumn.ShowInCustomizationForm = BudgetInterface
        Dim ListAction As New List(Of eAcciones)
        ListAction.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDGvInvoices, ListAction)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvInvoices.Columns
            If col.Name = "colActions" Then
                col.Width = 60
            End If
        Next

        INDsleExpenseConcept.Properties.PopupFormSize = New Size(700, 500)

        ValidateCurrencyHeader(VoucherClass, Me._currencyHeader)

        If Me._currencyHeader IsNot Nothing Then
            Me.SetCultureUIHeader(Me._currencyHeader?.Abbreviation)
        End If
        setInitialValues()
    End Sub

    ''' <summary>
    ''' Sets the initial values.
    ''' </summary>
    Private Sub setInitialValues()
        CleanControlsInvoice()
        _serverDate = Me.GetServerDate()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        BarraBotones.StatusRecordVisible = False
        BarraBotones.Minimizar(True)
        BarraBotones.OperatingUnitVisible = False
        INDsleExpenseConcept.Focus()

        INDliConcept.HideControl(True)
        INDliThirdParty.HideControl(True)
        INDliCashRegister.HideControl(True)
        INDLciCurrencyCashRegister.HideControl(True)
        INDliEntityBankAccount.HideControl(True)
        INDLciCashFlowConcept.HideControl(True)

        If Not IsEditMode Then
            Me.HideOrShowTRMControls(eTRMControls.Advance)
            Me.HideOrShowTRMControls(eTRMControls.Refund)
            Me.HideOrShowIVAControls(False)
        End If
        Me.HideOrShowTRMControls(eTRMControls.Billing)

        Select Case VoucherClass
            Case eVoucherClass.Payment
                INDliConcept.HideControl(False)
                INDliThirdParty.HideControl(False)
                INDLciCashFlowConcept.HideControl(False)
            Case eVoucherClass.Refund
                INDliCashRegister.HideControl(False)
                Me.INDLciCurrencyCashRegister.HideControl(False)
                InitializeCashRegister(1) 'Cajas Menores
            Case eVoucherClass.Transfer
                If ExpenseType = 1 Then 'Bancos
                    INDliEntityBankAccount.HideControl(False)
                    InitializeEntityBankAccount()
                ElseIf ExpenseType = 3 Then 'Caja mayor
                    INDliCashRegister.HideControl(False)
                    InitializeCashRegister(2) 'Cajas Mayores
                End If
        End Select
        If _dictionaryShares Is Nothing Then
            _dictionaryShares = New Dictionary(Of String, List(Of Infrastructure.Data.Xpo.PaymentsRepository.AccountPayableSharesXpo))()
        End If
        CashFlowConceptId = 0
        If showDeductibleIva Then
            INDliDiscountableIVA.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDliDiscountableIVA.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    ''' <summary>
    ''' Handles the FormClosing event of the FrmPopUpDisbursementVoucher control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
    Private Sub FrmPopUpDisbursementVoucher_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If Not INDsleExpenseConcept.EditValue Is Nothing Then
            If Not MessageIndigo.Show(ResourceManager.GetString("CloseForm", "Payments"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                e.Cancel = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles the Closed event of the INDpceInvoiceShare control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ClosedEventArgs"/> instance containing the event data.</param>
    Private Sub INDpceInvoiceShare_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDpceInvoiceShare.Closed
        If Me.StatusVoucher = 1 OrElse Me.StatusVoucher = 0 Then
            INDsbAdd.Enabled = True
        End If
        If e.CloseMode = DevExpress.XtraEditors.PopupCloseMode.Cancel Then
            INDsbAdd.Focus()
        End If
    End Sub

#End Region

#Region "KeyDown"

    Private _valueChanging As Boolean
    Private _percentChanging As Boolean

    ''' <summary>
    ''' Handles the KeyDown event of the RepositoryItemPercentPay control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Sub RepositoryItemPercentPay_KeyDown(sender As Object, e As KeyEventArgs) Handles RepositoryItemPercentPay.KeyDown
        If e.KeyCode = Keys.Enter Then
            ConvertPercentToValue()
        End If
    End Sub

    ''' <summary>
    ''' Handles the KeyDown event of the INDtxtPayValue control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDtxtPayValue_KeyDown(sender As Object, e As KeyEventArgs) Handles INDtxtPayValue.KeyDown
        If e.KeyCode <> Keys.Enter Then
            _valueChanging = True
            _percentChanging = False
        End If
    End Sub

    ''' <summary>
    ''' Handles the KeyDown event of the INDtxtPayPercent control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDtxtPayPercent_KeyDown(sender As Object, e As KeyEventArgs) Handles INDtxtPayPercent.KeyDown
        If e.KeyCode <> Keys.Enter Then
            _valueChanging = False
            _percentChanging = True
        End If
    End Sub

    ''' <summary>
    ''' Handles the KeyDown event of the INDmemoComments control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDmemoComments_KeyDown(sender As Object, e As KeyEventArgs) Handles INDmemoComments.KeyDown
        If e.KeyCode = Keys.Enter Then
            If INDlgrConceptRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never And INDlgrInvoicePayment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never Then
                INDsbAdd.Focus()
            ElseIf INDlgrConceptRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                INDsleConceptRetention.Focus()
            Else
                INDtxtAdvancePayment.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles the KeyDown event of the RepositoryItemAmountPay control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Sub RepositoryItemAmountPay_KeyDown(sender As Object, e As KeyEventArgs) Handles RepositoryItemPaymentConcepts.KeyDown
        'Evita el copy paste
        If e.Modifiers = Keys.Control AndAlso e.KeyValue = Keys.V Then
            Exit Sub
        End If
        If e.KeyCode = Keys.Enter Then
            ConvertValueToPercent()
        End If
    End Sub

    ''' <summary>
    ''' Handles the KeyDown event of the INDtxtBaseValue control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDtxtBaseValue_KeyDown(sender As Object, e As KeyEventArgs) Handles INDtxtBaseValue.KeyDown
        If e.KeyCode = Keys.Enter Then
            If INDlgrInvoicePayment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never Then
                INDsbAdd.Focus()
            Else
                INDtxtAdvancePayment.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles the KeyDown event of the INDpceInvoiceShare control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDpceInvoiceShare_KeyDown(sender As Object, e As KeyEventArgs) Handles INDpceInvoiceShare.KeyDown
        If e.KeyCode = Keys.Enter Then
            INDpceInvoiceShare.ShowPopup()
        End If
    End Sub

    ''' <summary>
    ''' Handles the KeyDown event of the FrmPopUpDisbursementVoucher control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Sub FrmPopUpDisbursementVoucher_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
#End Region

#Region "EditValueChanging"

    Private Sub INDrptTxtValue_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrptTxtValue.EditValueChanging
        If e.NewValue = String.Empty Then
            Exit Sub
        End If

        Dim detail = DirectCast(INDgvBudgetInterface.GetFocusedRow, DischargeBillBudget)
        If Convert.ToDecimal(e.NewValue, CultureInfo.InvariantCulture) < 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "El valor no puede ser mayor al saldo"
            e.Cancel = True
        ElseIf Convert.ToDecimal(e.NewValue, CultureInfo.InvariantCulture) > detail.Balance Then
            Mensaje(EeventViewerImages.Advertencia) = "El valor no puede ser mayor al saldo"
            e.Cancel = True
        End If
    End Sub

    ''' <summary>
    ''' evento que se ejecuta al estar cambiando el valor del control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDtxtAdvancePayment_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDtxtAdvancePayment.EditValueChanging
        If INDtxtValue.Enabled Then
            Exit Sub
        End If
        Me.ValueInCurrencyHeaderAdvance = Math.Round(CDec(Convert.ToDecimal(e.NewValue, CultureInfo.InvariantCulture) / Me.TRMValueAdvanced), 5)
        If ListDischargeBill IsNot Nothing Then
            ValueConcept = ListDischargeBill.Sum(Function(x) x.ValueInCurrencyHeader) + Me.ValueInCurrencyHeaderAdvance
        Else
            ValueConcept = Me.ValueInCurrencyHeaderAdvance
        End If
    End Sub

    ''' <summary>
    ''' evento que se ejecuta al estar cambiando el valor del control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDtxtPayValue_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDtxtPayValue.EditValueChanging
        Me.ValueBillInCurrencyHeader = Math.Round(CDec(Convert.ToDecimal(e.NewValue, CultureInfo.InvariantCulture) / Me.TRMInvoice), 5)
    End Sub
#End Region

#Region "EditValueChanged"
    Private Sub INDgleNature_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleNature.EditValueChanged
        If _mainAccountVoucherDetail IsNot Nothing Then
            If _mainAccountVoucherDetail.RetencionType <> 0 AndAlso CByte(INDgleNature.EditValue) = _mainAccountVoucherDetail.Nature Then
                INDlgrConceptRetention.HideControl(False)
                ValueConcept = 0F
                INDtxtValue.Enabled = False
                ActionsOnControlsConceptRetention = True
            Else
                INDtxtValue.Enabled = True
                ActionsOnControlsConceptRetention = False
                INDsleConceptRetention.EditValue = Nothing
                CleanControlsRetention()
                INDlgrConceptRetention.HideControl(True)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDsleCashRegister control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub INDsleCashRegister_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCashRegister.EditValueChanged
        If CashRegisterId <> 0 Then
            If INDsleCashRegister.EditValue = IdCashRegisterValidate Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CashRegisterRepeat", NAME_MODULE)
                INDsleCashRegister.EditValue = Nothing
            ElseIf (ListVoucherDetails IsNot Nothing AndAlso ListVoucherDetails.FindAll(Function(x) x.CashRegisterId = CashRegisterId).Count > 0) _
                            OrElse (_listVoucherDetail IsNot Nothing AndAlso _listVoucherDetail.FindAll(Function(x) x.CashRegisterId = CashRegisterId).Count > 0) Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CashRegisterExist", NAME_MODULE)
                INDsleCashRegister.EditValue = Nothing
            Else
                INDliAccountAccounting.HideControl(False)
                If VoucherClass = eVoucherClass.Refund Then
                    INDlgrRefound.HideControl(False)
                    ValueConcept = 0F
                    INDtxtValue.Enabled = False
                    If Not Me._flagLoadControls Then
                        Await Task.Factory.StartNew(Sub()
                                                        Me.SafeInvoke(Sub()
                                                                          Me.LoadRefundByCashRegister()
                                                                      End Sub)
                                                    End Sub)
                    End If
                End If

                Dim _cashRegister As Infrastructure.Data.Xpo.TreasuryRepository.CashRegisterXpo = TryCast(TryCast(INDgvCashRegister.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)?.OriginalRow, Infrastructure.Data.Xpo.TreasuryRepository.CashRegisterXpo)
                If Me.CashDatasource IsNot Nothing AndAlso _cashRegister IsNot Nothing Then
                    MainAccountId = _cashRegister?.IdMainAccount?.Id
                    Me.CurrencyIdCashRegisterRefund(_cashRegister?.CurrencyAbbreviation) = _cashRegister?.CurrencyId
                    Await Me.EditValueChanged_CurrencyCashRegister(Me._flagLoadControls)
                End If
            End If
        Else
            Deshacer()
        End If
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDsleConcept control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsleConcept_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleExpenseConcept.EditValueChanged
        Try
            If INDsleExpenseConcept.EditValue IsNot Nothing Then
                If Not IsEditMode Then
                    ThirdParthy = MainThird
                End If
                Using model As New MVoucherTransaction(Me.Tag)
                    Dim obj = model.GetExpenseConceptById(INDsleExpenseConcept.EditValue)

                    If obj Is Nothing OrElse Not obj?.Status Then
                        Mensaje(EeventViewerImages.Advertencia) = $"{If(obj Is Nothing, "No existe el concepto", "El concepto seleccionado esta inactivo")}"
                        ExpenseConcept = Nothing
                        Me.HideOrShowIVAControls(False)
                        Exit Sub
                    End If

                    If obj?.Behavior = 6 Then
                        Me.HideOrShowTRMControls(eTRMControls.Advance)
                        Me.HideOrShowIVAControls(obj?.TaxManagement AndAlso _dataThirdParty IsNot Nothing _
                                                                                               AndAlso _dataThirdParty?.ContributionType <> 0)
                    Else
                        ThirdParthy = MainThird
                        INDLciCurrency.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        Me.HideOrShowIVAControls(False)
                    End If
                    If flagClassPayment AndAlso flagTenant AndAlso flagMinorCash AndAlso obj?.Behavior = 6 Then
                        INDliEconomicActivity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    End If
                    INDsleThirdParty.Properties.ReadOnly = Not (obj IsNot Nothing AndAlso obj?.Behavior = 6)
                    LoadConceptControls()
                End Using

            End If
        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = ex.Message
        End Try
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDsleAccount control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub INDsleAccount_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleAccount.EditValueChanged
        If INDsleAccount.EditValue IsNot Nothing Then
            Using Model As New MPUC(Me._myTag)
                _mainAccountVoucherDetail = Await Model.GetAccountById(INDsleAccount.EditValue)
                If _mainAccountVoucherDetail.HandlesCostCenter Then
                    INDliCostCenter.HideControl(False)
                Else
                    INDliCostCenter.HideControl(True)
                    INDsleCostCenter.EditValue = Nothing
                End If
                'reviso si la cuenta tiene retencion
                If _mainAccountVoucherDetail.RetencionType <> 0 AndAlso CByte(INDgleNature.EditValue) = _mainAccountVoucherDetail.Nature Then
                    INDlgrConceptRetention.HideControl(False)
                    If Not IsEditMode Then
                        ValueConcept = 0F
                    End If
                    INDtxtValue.Enabled = False
                    ActionsOnControlsConceptRetention = True
                Else
                    ActionsOnControlsConceptRetention = False
                    INDsleConceptRetention.EditValue = Nothing
                    CleanControlsRetention()
                    INDlgrConceptRetention.HideControl(True)
                End If
            End Using
        End If
    End Sub

    Private Sub ChangeValueIVAConcept()
        Using Model As New MGeneralLedgerIVA(Me._myTag)
            _generalLedgerIVA = Model.GetGeneralLedgerIVAById(INDsleGeneralLedgerIVA.EditValue)

            If _generalLedgerIVA?.ObjectEmbbeded?.IdAccountCreditControlFiscal Is Nothing OrElse _generalLedgerIVA?.ObjectEmbbeded?.IdAccountDebitControlFiscal Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "No se encontraron datos de las cuentas de control fiscal"
                Me.HideOrShowIVAControls(False)
                Exit Sub
            End If

            If IsEditMode Then
                ValueIVA = ValueConcept * (_generalLedgerIVA.ObjectEmbbeded.Percentage / 100)
                TotalConcept = ValueConcept + ValueIVA
            Else
                ValueIVA = ValueConcept * (_generalLedgerIVA.ObjectEmbbeded.Percentage / 100)
                TotalConcept = ValueConcept + ValueIVA
            End If
        End Using
    End Sub

    ''' <summary>
    ''' evento cuando se cambia el IVA
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleGeneralLedgerIVA_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleGeneralLedgerIVA.EditValueChanged
        If IdGeneralLedgerIVA IsNot Nothing AndAlso IdGeneralLedgerIVA > 0 Then
            ChangeValueIVAConcept()
        End If
    End Sub

    ''' <summary>
    ''' evento cuando valor del concepto cambia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDtxtValue_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtValue.EditValueChanged
        If IdGeneralLedgerIVA IsNot Nothing AndAlso IdGeneralLedgerIVA > 0 Then
            ChangeValueIVAConcept()
        End If
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDsleEntityAccount control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsleEntityAccount_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleEntityBankAccount.EditValueChanged
        If EntityBankAccountId <> 0 Then

            If INDsleEntityBankAccount.EditValue = _idEntityBankAccount Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("EntityBankAccountRepeat", NAME_MODULE)
                INDsleEntityBankAccount.EditValue = Nothing
            ElseIf VoucherClass <> eVoucherClass.Transfer AndAlso ExpenseType = 1 AndAlso (ListVoucherDetails IsNot Nothing AndAlso ListVoucherDetails.FindAll(Function(x) x.IdEntityBankAccount = EntityBankAccountId).Count > 0) Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("EntityBankAccountExist", NAME_MODULE)
                INDsleEntityBankAccount.EditValue = Nothing
            ElseIf (_listVoucherDetail IsNot Nothing AndAlso _listVoucherDetail.Count > 0) Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("MoreOneDetailCannot")
                INDsleEntityBankAccount.EditValue = Nothing
            Else
                INDliAccountAccounting.HideControl(False)
                Dim _entityBankAccount = Nothing
                If INDgvEntityBankAccount.GetFocusedRow() IsNot Nothing Then
                    _entityBankAccount = CType(CType(INDgvEntityBankAccount.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.TreasuryRepository.EntityBankAccountXpo)
                Else
                    Using m As New MVoucherTransaction(Me.Tag)
                        _entityBankAccount = m.GetEntityBankAccountById(_idEntityBankAccount)
                    End Using
                End If
                MainAccountId = _entityBankAccount.IdMainAccount.Id
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDsleInvoiceShare control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub INDsleInvoiceShare_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleInvoiceShare.EditValueChanged
        If Not String.IsNullOrEmpty(INDsleInvoiceShare.EditValue) AndAlso INDsleInvoiceShare.EditValue IsNot Nothing AndAlso INDsleInvoiceShare.EditValue > 0 Then
            Await EditvalueChangedInvoiceShare()
        Else
            CleanControlsInvoice()
        End If
    End Sub

    Private Sub ApplyDiscount(_shareInvoice As Infrastructure.Data.Xpo.PaymentsRepository.AccountPayableSharesXpo)
        ApplyDiscountValue = 0
        Dim Pay As Decimal = 0
        Dim tempPercent As Decimal = 0

        If PromptPaymentDiscount = 0 Then
            PayPercent = Math.Round(Convert.ToDecimal((PayValue * 100) / _shareInvoice.Balance), 2)
            Exit Sub
        End If
        ApplyDiscountValue = Math.Round(((_shareInvoice.IdAccountPayable.View_SchedulePaymentXpo.FirstOrDefault.BaseValue * PromptPaymentDiscount) / 100), 2)

        If PayValue + ApplyDiscountValue >= _shareInvoice.Balance Then
            Pay = _shareInvoice.Balance
        End If

        If PayPercent + PromptPaymentDiscount >= 100 Then
            tempPercent = 100
        End If

        If tempPercent <> 100 OrElse Pay <> _shareInvoice.Balance Then
            ApplyDiscountValue = 0
            Exit Sub
        End If
        PayValue = Pay - ApplyDiscountValue
        PayPercent = tempPercent
        Mensaje(EeventViewerImages.Advertencia) = "Se aplic� el descuento"
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDsleConceptRetention control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsleConceptRetention_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleConceptRetention.EditValueChanged
        If INDsleConceptRetention.EditValue IsNot Nothing AndAlso INDsleConceptRetention.EditValue > 0 Then
            Using Model As New MRetentionConcept(Me._myTag)
                CleanControlsRetention()
                _retConcept = Model.GetRetentionByIdSimple(INDsleConceptRetention.EditValue)
                _minBaseRetention = _retConcept.MinBase
                _retentiontype = _retConcept.Retention
                _listAccountingRetention = _retConcept.RetentionConceptRanges.ToList()
                If _retConcept.Id > 0 Then
                    Select Case _retConcept.Retention
                        Case CInt(eRetentionType.Base)
                            INDsePercentage.EditValue = _retConcept.Rate
                            INDTxtBillingValue.Focus()
                        Case CInt(eRetentionType.Rango)
                            INDTxtBillingValue.Focus()
                        Case CInt(eRetentionType.Variable)
                            INDsePercentage.Enabled = True
                            INDsePercentage.Focus()
                    End Select
                End If
            End Using
        End If
    End Sub

    ''' <summary>
    ''' evento que se dispara cuando se cambia la moneda del segmento Anticipos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDsleCurrency_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCurrency.EditValueChanged

        If Me.CurrencyAdvancedId Is Nothing OrElse Me._currencyHeader Is Nothing OrElse Me._currencyHeader?.Id = Me.CurrencyAdvancedId Then
            Me.HideOrShowTRMControls(eTRMControls.Advance)
            Exit Sub
        End If

        Me.TRMValueAdvanced = Await Me.GetTRMValue(Me.CurrencyAdvancedId, Me._currencyHeader?.Id, eTRMControls.Advance)
        Me.ValueInCurrencyHeaderAdvance = Math.Round((Me.ValueAdvancePayment / Me.TRMValueAdvanced), 2)
        RefreshValueConcept()
        INDLciValueinCurrencyH.Text = $"Valor ( {Me._currencyHeader?.Abbreviation} )"

        If CurrencyAdvanceSelected IsNot Nothing Then
            SetCultureUIAdvanced(CurrencyAdvanceSelected.Abbreviation)
        End If
    End Sub

    ''' <summary>
    ''' evento para cuando se cambia el tercero
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleThirdParty_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleThirdParty.EditValueChanged
        Try
            If ThirdParthy Is Nothing Then
                Exit Sub
            End If

            If IsEditMode OrElse TryCast(INDsleThirdParty.GetSelectedDataRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)?.OriginalRow Is Nothing Then
                GetThirdParty()
                Exit Sub
            Else
                _dataThirdParty = TryCast(TryCast(INDsleThirdParty.GetSelectedDataRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)?.OriginalRow, Infrastructure.Data.Xpo.CommonRepository.CommonThirdPartyXpo)
            End If

            If Me._expenseConcept Is Nothing OrElse _dataThirdParty Is Nothing Then
                Exit Sub
            Else
                Me.HideOrShowIVAControls(Me._expenseConcept?.Behavior = 6 AndAlso _dataThirdParty IsNot Nothing _
                                                                     AndAlso _dataThirdParty?.ContributionType <> 0)
            End If
        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = ex.Message
        End Try
    End Sub

#End Region

#Region "Leave"
    ''' <summary>
    ''' Handles the Leave event of the INDtxtPayPercent control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDtxtPayPercent_Leave(sender As Object, e As EventArgs) Handles INDtxtPayPercent.Leave
        If _percentChanging Then
            ConvertPercentToValueText()
        End If
    End Sub

    ''' <summary>
    ''' Handles the Leave event of the INDtxtPayValue control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDtxtPayValue_Leave(sender As Object, e As EventArgs) Handles INDtxtPayValue.Leave
        If _valueChanging Then
            ConvertValueToPercentText()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al perder el foco del control y que convierte de porcentaje a pagar a valor a pagar
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub RepositoryItemPercentPay_Leave(sender As Object, e As EventArgs) Handles RepositoryItemPercentPay.Leave
        ConvertPercentToValue()
        RefreshValueConcept()
    End Sub

    ''' <summary>
    ''' Handles the Leave event of the RepositoryItemValue control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub RepositoryItemValue_Leave(sender As Object, e As EventArgs) Handles RepositoryItemValue.Leave
        Dim _invoicePortfolio As Infrastructure.Data.Xpo.PortfolioRepository.Portfolio_PortfolioAdvance = CType(INDgvPortfolioAdvance.GetFocusedRow(), Infrastructure.Data.Xpo.PortfolioRepository.Portfolio_PortfolioAdvance)
        If Not String.IsNullOrEmpty(_invoicePortfolio.PayValue) Then
            If Convert.ToDecimal(_invoicePortfolio.Balance) < _invoicePortfolio.PayValue Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("PayValueMoreAccountPayableValue", NAME_MODULE)
                _invoicePortfolio.PayValue = _invoicePortfolio.Balance
                _invoicePortfolio.PercentValue = 100
            Else
                _invoicePortfolio.PercentValue = TreasuryStaticServices.ConvertInvoiceValueToPercent(_invoicePortfolio.PayValue, _invoicePortfolio.Balance).MoneyFormat(2)
            End If
            INDgvPortfolioAdvance.RefreshData()
            RefreshValueConcept()
        End If
    End Sub

    ''' <summary>
    ''' Handles the Leave event of the RepositoryItemPercent control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub RepositoryItemPercent_Leave(sender As Object, e As EventArgs) Handles RepositoryItemPercent.Leave
        Dim _invoicePortfolio As Infrastructure.Data.Xpo.PortfolioRepository.Portfolio_PortfolioAdvance = CType(INDgvPortfolioAdvance.GetFocusedRow(), Infrastructure.Data.Xpo.PortfolioRepository.Portfolio_PortfolioAdvance)
        If Not String.IsNullOrEmpty(_invoicePortfolio.PercentValue) Then
            _invoicePortfolio.PayValue = TreasuryStaticServices.ConvertInvoicePercentToValue(_invoicePortfolio.PercentValue, _invoicePortfolio.Balance).MoneyFormat(0)
            INDgvPortfolioAdvance.RefreshData()
            RefreshValueConcept()
        End If
    End Sub

    ''' <summary>
    ''' Handles the Leave event of the INDtxtInvoicedValue control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDtxtInvoicedValue_Leave(sender As Object, e As EventArgs) Handles INDtxtBaseValue.Leave
        If _retConcept IsNot Nothing AndAlso INDtxtBaseValue.EditValue > 0 Then
            Dim resulCalculateRetention As Decimal = 0
            If _retentiontype = 2 Then ' rangos
                If INDsePercentage.EditValue > 0 Then
                    resulCalculateRetention = AccountingServices.CalculateRetention(CDec(INDtxtBaseValue.EditValue), _retConcept, INDsePercentage.EditValue)
                End If
            ElseIf _retentiontype = 1 Then 'base
                resulCalculateRetention = AccountingServices.CalculateRetention(CDec(INDtxtBaseValue.EditValue), _retConcept)
            ElseIf _retentiontype = 3 Then 'variable
                If INDsePercentage.EditValue > 0 Then
                    resulCalculateRetention = AccountingServices.CalculateRetention(CDec(INDtxtBaseValue.EditValue), 0, INDsePercentage.EditValue, _retConcept.TypeRounding)
                End If
            End If

            INDtxtRetentionValue.EditValue = resulCalculateRetention
            ValueConcept = resulCalculateRetention
        End If
    End Sub

    ''' <summary>
    ''' evento que valida sobre la rejilla
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub RepositoryItemTextPayValue_Leave(sender As Object, e As EventArgs) Handles RepositoryItemTextPayValue.Leave
        Dim _invoice As DischargeBill = CType(INDGvInvoices.GetFocusedRow(), DischargeBill)
        If _invoice IsNot Nothing Then
            If Not String.IsNullOrEmpty(_invoice.AdvancedValue) Then
                If Convert.ToDecimal(_invoice.AccountPayableShareBalance) < _invoice.AdvancedValue Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("PayValueMoreAccountPayableValue", NAME_MODULE)
                    _invoice.AdvancedValue = _invoice.AccountPayableShareBalance
                    _invoice.AdvancePercent = 100
                Else
                    _invoice.AdvancePercent = TreasuryStaticServices.ConvertInvoiceValueToPercent(_invoice.AdvancedValue, _invoice.AccountPayableShareBalance)
                End If
                INDGvInvoices.RefreshData()
                RefreshValueConcept()
            End If
        End If
    End Sub

    ''' <summary>
    ''' evento cuando se cambia el porcentaje de descuento de pronto pago
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDtxtPromptPaymentDiscount_Leave(sender As Object, e As EventArgs) Handles INDtxtPromptPaymentDiscount.Leave
        If _shareInvoice Is Nothing Then
            Exit Sub
        End If
        If _shareInvoice.Balance = _shareInvoice.IdAccountPayable.Value Then
            If MessageIndigo.Show("�Est� seguro que desea cambiar el porcentaje del descuento (solo se aplica para porcentajes de pago del 100%)?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                PromptPaymentDiscount = 0
                ApplyDiscount(_shareInvoice)
                Exit Sub
            End If
            ApplyDiscount(_shareInvoice)
        End If
    End Sub
#End Region

#Region "ButtonClick"
    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleConcept control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleConcept_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleExpenseConcept.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmExpenseConcepts
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, Formulario.Size.Height)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                If _idCashRegister > 0 Then
                    Me.InitializeExpenseConceptByCash()
                Else
                    Me.InitializeExpenseConceptNotCash(HandlesDocumentSupport)
                End If
            End Using
        End If
    End Sub
    ''' <summary>
    ''' Evento que abre el frm de actividades economicas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleEconomicActivity_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleEconomicActivity.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmEconomicActivity
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False

                Dim proportionWidth As Double = 0.6 ' 60% del ancho de la pantalla
                Dim proportionHeight As Double = 0.8 ' 80% de la altura de la pantalla

                ' Calcula el tama�o proporcional
                Dim screenWidth As Integer = Screen.PrimaryScreen.WorkingArea.Width 'Calculamos el ancho de la pantalla actual
                Dim screenHeight As Integer = Screen.PrimaryScreen.WorkingArea.Height 'Obtenemos la altura de la pantalla actual
                'Nuevas medidas de nuestro popup
                Dim popUpWidth As Integer = CInt(screenWidth * proportionWidth)
                Dim popUpHeight As Integer = CInt(screenHeight * proportionHeight)

                ' Asignamos el nuevo tama�o al formulario de actividades Economicas
                Formulario.Size = New Size(popUpWidth, popUpHeight)
                Formulario.StartPosition = FormStartPosition.CenterParent 'Centramos el frm

                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
            End Using
        End If
    End Sub


    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleInvoiceShare control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleInvoiceShare_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleInvoiceShare.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New Payments.FrmAccountsPayable
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, Formulario.Size.Height)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                InitializeInvoiceShare()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDslePaymentConcept control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDslePaymentConcept_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDslePaymentConcept.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmPaymentConcepts
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, Formulario.Size.Height)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                InitializePaymentConcept()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleCashRegister control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleCashRegister_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCashRegister.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmCash
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, Formulario.Size.Height)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleEntityAccount control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleEntityAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleEntityBankAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmEntityAccount
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, Formulario.Size.Height)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                Me.InitializeEntityBankAccount()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleAccount control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmPopupPUC
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, Formulario.Size.Height)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                Me.InitializeAccountAccounting()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleConceptRetention control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleConceptRetention_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleConceptRetention.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmRetentionConcept
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, Formulario.Size.Height)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                Me.InitializeRetentionConcept()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleThirdParty control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleThirdParty_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleThirdParty.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmThirdParty
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, Formulario.Size.Height)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                Me.InitializeThird()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleCostCenter control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleCostCenter_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCostCenter.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmCostCenter
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, Formulario.Size.Height)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                Me.InitializeCostCenter()
            End Using
        End If
    End Sub
#End Region

#Region "Activated"
    ''' <summary>
    ''' Handles the Activated event of the FrmPopUpDisbursementVoucher control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmPopUpDisbursementVoucher_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        INDsleExpenseConcept.Focus()
    End Sub
#End Region

#Region "QueryPopUp"
    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleInvoiceShare control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleInvoiceShare_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleInvoiceShare.QueryPopUp
        If Not _statePopUpInvoice Then
            InitializeInvoiceShare()
            _statePopUpInvoice = True
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDslePaymentConcept control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDslePaymentConcept_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDslePaymentConcept.QueryPopUp
        If Not _statePopUpPaymentConcept Then
            InitializePaymentConcept()
            _statePopUpPaymentConcept = True
        End If
    End Sub

    Private Sub INDpceInvoiceShare_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDpceInvoiceShare.QueryPopUp
        INDsbAdd.Enabled = False
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el popup de presupuesto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrptPceBudgetInterface_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDrptPceBudgetInterface.QueryPopUp
        INDrptPceBudgetInterface.PopupControl = INDPccBudgetInterface
        If BudgetInterface Then
            Dim dischargeBill = CType(INDGvInvoices.GetFocusedRow, DischargeBill)
            INDgcBudgetInterface.DataSource = dischargeBill.DischargeBillBudget
        End If
    End Sub

    ''' <summary>
    ''' Evento cuando se cambia de moneda en la seccion de anticipo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCurrency_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCurrency.QueryPopUp
        If CurrencyAdvanceDatasource Is Nothing Then
            Using Model As New MBusqueda
                CurrencyAdvanceDatasource = Model.ConsultarEntidades(eDataSource.Currency)
            End Using
        End If
    End Sub
#End Region

#Region "Click"
    ''' <summary>
    ''' Handles the Click event of the INDsbAdd control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsbAdd_Click(sender As Object, e As EventArgs) Handles INDsbAdd.Click
        If ValidateControlsExpense() Then

            Dim description As String = String.Empty
            description &= "Comprobante de Egreso {0}, Concepto : " & INDsleExpenseConcept.Text
            Select Case VoucherClass
                Case eVoucherClass.Payment
                    Dim conceptDiferent As Boolean = False
                    Dim optionError As Integer = 0
                    If ListVoucherDetails Is Nothing Then
                        ListVoucherDetails = New List(Of VoucherTransactionDetails)()
                    End If
                    If INDTxtBillingValue.EditValue IsNot Nothing AndAlso INDtxtBaseValue.EditValue IsNot Nothing Then
                        Dim billingValue As Decimal = CDec(INDTxtBillingValue.EditValue)
                        Dim baseValue As Decimal = CDec(INDtxtBaseValue.EditValue)

                        If billingValue < baseValue Then
                            Mensaje(EeventViewerImages.Advertencia) = "El valor de facturación no puede ser menor al valor base."
                            INDTxtBillingValue.Focus()
                            INDTxtBillingValue.SelectAll()
                            Exit Sub
                        End If
                    End If
                    If Behavior <> CInt(eBehavior.None) Then
                        Dim cant As Integer = ListVoucherDetails.FindAll(Function(x) x.ExpenseConceptBehavior <> Behavior).Cast(Of VoucherTransactionDetails).ToList().Count
                        If cant <> 0 Then
                            optionError = 1
                            conceptDiferent = True
                        Else
                            Dim cantRepeat = ListVoucherDetails.FindAll(Function(x) x.ExpenseConceptBehavior = Behavior AndAlso x.IdMainAccount = MainAccountId).Cast(Of VoucherTransactionDetails).ToList().Count
                            If cantRepeat <> 0 AndAlso Behavior <> CInt(eBehavior.PaymentAdvancePaymentInvoices) Then
                                optionError = 2
                                conceptDiferent = True
                            Else
                                If Behavior = CInt(eBehavior.PaymentAdvancePaymentInvoices) Then
                                    Dim cantpayment = ListVoucherDetails.FindAll(Function(x) x.IdExpenseConcept = ExpenseConcept).Cast(Of VoucherTransactionDetails).ToList().Count
                                    If cantpayment <> 0 Then
                                        optionError = 3
                                        conceptDiferent = True
                                    Else
                                        Dim _detalle = New VoucherTransactionDetails() With {.IdExpenseConcept = ExpenseConcept, .ExpenseConceptBehavior = Behavior, .IdMainAccount = MainAccountId, .IdCostCenter = CostCenter, .discountableIVA = DiscountableIVA, .TaxRegistration = TaxRegistration, .IdGeneralLedgerIVA = IdGeneralLedgerIVA, .ValueIVA = ValueIVA, .TotalConcept = TotalConcept}
                                        If CashFlowConceptId > 0 Then
                                            _detalle.IdCashFlowConcept = CashFlowConceptId
                                            _detalle.CodeNameCashFlowConcept = CashFlowConceptCodeName
                                        Else
                                            _detalle.IdCashFlowConcept = Nothing
                                            _detalle.CodeNameCashFlowConcept = String.Empty
                                        End If
                                        ListVoucherDetails.Add(_detalle)
                                    End If
                                Else
                                    Dim _detalle = New VoucherTransactionDetails() With {.IdExpenseConcept = ExpenseConcept, .ExpenseConceptBehavior = Behavior, .IdMainAccount = MainAccountId, .IdCostCenter = CostCenter, .discountableIVA = DiscountableIVA, .TaxRegistration = TaxRegistration, .IdGeneralLedgerIVA = IdGeneralLedgerIVA, .ValueIVA = ValueIVA, .TotalConcept = TotalConcept}
                                    If CashFlowConceptId > 0 Then
                                        _detalle.IdCashFlowConcept = CashFlowConceptId
                                        _detalle.CodeNameCashFlowConcept = CashFlowConceptCodeName
                                    Else
                                        _detalle.IdCashFlowConcept = Nothing
                                        _detalle.CodeNameCashFlowConcept = String.Empty
                                    End If
                                    ListVoucherDetails.Add(_detalle)
                                End If
                            End If
                        End If
                    End If
                    If Not conceptDiferent Then
                        If Behavior = CInt(eBehavior.PaymentAdvancePaymentInvoices) Or Behavior = CInt(eBehavior.PettyCash) OrElse Behavior = CInt(eBehavior.EndorsementBills) Then
                            Dim payValueInvoice As Decimal = 0
                            If ListDischargeBill IsNot Nothing Then
                                If ValidateInvoicesPaymentOrder() Then
                                    payValueInvoice = ListDischargeBill.Sum(Function(ac) (ac.ValueInCurrencyHeader + If(ac.ValueDiscountInCurrencyHeader, 0)))
                                    ValueConcept = CStr(TreasuryStaticServices.CalculateExpenseValue(payValueInvoice, Me.ValueInCurrencyHeaderAdvance))
                                Else
                                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("IncorrectInvoicesPaymentOrder", NAME_MODULE)
                                    ListVoucherDetails.RemoveAt(ListVoucherDetails.Count - 1)
                                    Exit Sub
                                End If
                                description &= ", Facturas : (" & String.Join("-", ListDischargeBill.Select(Function(x) x.AccountPayableBillNumber).ToArray) & ")"
                            Else
                                ValueConcept = Me.ValueInCurrencyHeaderAdvance
                            End If
                        End If
                        AssigningValues()
                        Dim args As New AddExpenseConceptEventArgs()
                        args.VoucherTransactionD = _voucherDetail
                        args.ListDischargeBill = ListDischargeBill
                        args.IsEditMode = Me.IsEditMode
                        args.ListVoucherTransactionAdvance = New List(Of VoucherTransactionAdvance)()

                        Dim portfolioAdvanceToAdd As New List(Of Infrastructure.Data.Xpo.PortfolioRepository.Portfolio_PortfolioAdvance)
                        If (PortfolioAdvanceDatasource IsNot Nothing) Then
                            portfolioAdvanceToAdd = (From pa In PortfolioAdvanceDatasource Select pa).Cast(Of Infrastructure.Data.Xpo.PortfolioRepository.Portfolio_PortfolioAdvance).ToList().Where(Function(x) x.PayValue > 0).ToList()
                        End If
                        For Each portAdvance As Infrastructure.Data.Xpo.PortfolioRepository.Portfolio_PortfolioAdvance In portfolioAdvanceToAdd
                            Dim voucherTransactionAdv As New VoucherTransactionAdvance()
                            With voucherTransactionAdv
                                If portAdvance.VoucherTransactionAdvanceId <> 0 Then
                                    .Id = portAdvance.VoucherTransactionAdvanceId
                                    .MarkAsModified()
                                End If
                                .IdVoucherTransactionD = portAdvance.VoucherTransactionDId
                                .PortfolioAdvanceId = portAdvance.Id
                                .Value = portAdvance.PayValue
                                .Percentage = portAdvance.PercentValue
                                .PortfolioAdvanceCode = portAdvance.Code
                                .PortfolioAdvanceDocumentDate = portAdvance.DocumentDate
                                .PortfolioAdvanceBalance = portAdvance.Balance
                            End With
                            args.ListVoucherTransactionAdvance.Add(voucherTransactionAdv)
                        Next
                        If VoucherTransactionAdvance IsNot Nothing Then
                            For Each item In VoucherTransactionAdvance
                                If args.ListVoucherTransactionAdvance.Where(Function(x) x.PortfolioAdvanceId = item.PortfolioAdvanceId).Count() = 0 Then
                                    args.ListVoucherTransactionAdvance.Add(item.MarkAsDeleted())
                                End If
                            Next
                        End If
                        If args.ListVoucherTransactionAdvance IsNot Nothing AndAlso args.ListVoucherTransactionAdvance.Any() Then
                            description &= ", Reintegro de Anticipo : (" & String.Join("-", args.ListVoucherTransactionAdvance.Select(Function(x) x.PortfolioAdvanceCode).ToArray) & ")"
                        End If
                        args.VoucherTransactionD.Observation = description
                        Deshacer()
                        RaiseEvent AddVoucherDetail(Nothing, args)
                    Else
                        Select Case optionError
                            Case 1
                                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("BehaviorAddError", NAME_MODULE)
                            Case 2
                                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("BehaviorRepeat", NAME_MODULE)
                            Case 3
                                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("ExpenseConceptExists", NAME_MODULE)
                        End Select
                    End If
                Case eVoucherClass.Refund

                    AssigningValues()
                    Dim args As New AddExpenseConceptEventArgs()
                    args.IsEditMode = Me.IsEditMode
                    args.ListRefund = ListRefundsDatasource
                    If ListRefundsDatasource IsNot Nothing AndAlso ListRefundsDatasource.Any() Then
                        description &= ", Reembolsos de Caja menor : (" & String.Join("-", ListRefundsDatasource.Select(Function(x) x.Code).ToArray) & ")"
                    End If
                    _voucherDetail.Observation = description
                    args.VoucherTransactionD = _voucherDetail
                    Deshacer()
                    RaiseEvent AddVoucherDetail(Nothing, args)
                Case eVoucherClass.Transfer
                    AssigningValues()
                    Dim args As New AddExpenseConceptEventArgs()
                    args.IsEditMode = Me.IsEditMode
                    _voucherDetail.Detail = description
                    args.VoucherTransactionD = _voucherDetail
                    Deshacer()
                    RaiseEvent AddVoucherDetail(Nothing, args)
            End Select
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("EmptyFields", NAME_MODULE), _listaValidaciones)
            _listaValidaciones = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Agrega cuotas de facturas al listado
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsbAddInvoiceShare_Click(sender As Object, e As EventArgs) Handles INDsbAddInvoiceShare.Click
        INDlyPopupConcept.BeginUpdate()
        If validateControlsInvoiceShare() Then
            AddAccountPayableShareToList()
            CleanControlsInvoice()
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("EmptyFields", NAME_MODULE), _listaValidaciones)
            _listaValidaciones = Nothing
        End If
        INDsleInvoiceShare.Focus()
        INDlyPopupConcept.EndUpdate()
    End Sub

#End Region

#Region "ButtonAction"
    ''' <summary>
    ''' Handles the ButtonAction event of the IndigoGridView1_Click control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        DeleteInvoiceShare()
    End Sub
#End Region

#Region "ContexMenuActions"
    ''' <summary>
    ''' Handles the ContexMenuActions event of the IndigoGridView1 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        DeleteInvoiceShare()
    End Sub
#End Region

#Region "DatasourceChanged"
    ''' <summary>
    ''' Handles the DataSourceChanged event of the INDGcInvoices control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDGcInvoices_DataSourceChanged(sender As Object, e As EventArgs) Handles INDGcInvoices.DataSourceChanged
        If AccountPayableDatasource IsNot Nothing AndAlso AccountPayableDatasource.Count > 0 Then
            INDGvInvoices.OptionsView.ShowFooter = True
        Else
            INDGvInvoices.OptionsView.ShowFooter = False
        End If
    End Sub

    ''' <summary>
    ''' Handles the DataSourceChanged event of the INDgcRefund control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDgcRefund_DataSourceChanged(sender As Object, e As EventArgs) Handles INDgcRefund.DataSourceChanged
        If ListRefundsDatasource IsNot Nothing AndAlso ListRefundsDatasource.Count > 0 Then
            INDgvRefund.OptionsView.ShowFooter = True
        Else
            INDgvRefund.OptionsView.ShowFooter = False
        End If
    End Sub
#End Region

#Region "Paste to Grid"
    ''' <summary>
    ''' valida los registros copiados desde el archivo excel
    ''' </summary>
    ''' <param name="sender">The sender.</param>
    ''' <param name="e">The <see cref="PasteToGridEventArgs"/> instance containing the event data.</param>
    Private Async Sub IndigoGridControl1_PasteToGrid(sender As GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl1.PasteToGrid
        If sender.Name = INDGcInvoices.Name Then
            Using Model As New MSupplier(Me.Tag)
                Me.Cursor = ChangeCursorIndigo()
                Dim supplierTmp = Model.GetSupplierByIdThirdParty(ThirdParthy)
                Dim IdSupplier As Integer = 0
                If supplierTmp IsNot Nothing AndAlso supplierTmp.Id > 0 Then
                    IdSupplier = supplierTmp.Id
                End If
                If IdSupplier > 0 Then
                    Dim resultValidate As Tuple(Of List(Of DischargeBill), List(Of String)) = Await ValidatePasteToGridInvoice(e.Rows, IdSupplier)
                    resultValidate.Item2.AddRange(validateListAccountPayableComplex(resultValidate.Item1))
                    If resultValidate.Item2.Count > 0 Then
                        Using Formulario As New FrmListErrors(resultValidate.Item2)
                            Formulario.MinimizeBox = False
                            Formulario.MaximizeBox = False
                            Formulario.Size = New Size(780, 700)
                            Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                            Dim transparent As New FrmTransparent(Formulario, False)
                            transparent.ShowDialog(Me)
                        End Using
                    Else
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AddInvoiceCorrect", NAME_MODULE)
                    End If
                    Await LoadDictionarySharesAsync(resultValidate.Item1.Select(Function(x) x.AccountPayableBillNumber).ToList())
                    If ListDischargeBill Is Nothing Then
                        ListDischargeBill = New List(Of DischargeBill)()
                    End If
                    ListDischargeBill.AddRange(resultValidate.Item1)
                    If ListDischargeBill IsNot Nothing AndAlso ListDischargeBill.Count > 0 Then
                        ListDischargeBill.ForEach(Sub(item) item.ExpenseConceptId = ExpenseConcept)
                    End If
                    AccountPayableDatasource = Nothing
                    AccountPayableDatasource = ListDischargeBill
                    ValueConcept = ListDischargeBill.Sum(Function(x) x.AdvancedValue) + ValueAdvancePayment
                Else
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("ThirdSupplierNotFound", NAME_MODULE)
                End If
                Me.Cursor = System.Windows.Forms.Cursors.Default
            End Using
        End If
    End Sub
#End Region

#End Region

#Region "Enum"

    ''' <summary>
    ''' Enumeracion del comportamiento de los conceptos de caja menor
    ''' </summary>
    Public Enum eBehavior
        ''' <summary>
        ''' Traslado entre bancos
        ''' </summary>
        TransferBetweenBanks = 1
        ''' <summary>
        ''' Caja Menor
        ''' </summary>
        PettyCash = 2
        ''' <summary>
        ''' Pago/Anticipo de Facturas CxP
        ''' </summary>
        PaymentAdvancePaymentInvoices = 3
        ''' <summary>
        ''' Devolutivos de Anticipos RC
        ''' </summary>
        ReturningImprestRC = 4
        ''' <summary>
        '''  Reembolso de Caja Menor
        ''' </summary>
        PettyCashReimbursement = 5
        ''' <summary>
        ''' The none
        ''' </summary>
        None = 6
        ''' <summary>
        ''' Endoso de Facturas
        ''' </summary>
        EndorsementBills = 7
    End Enum

    Public Enum eStatusAccountPayable
        Registrado = 1
        Confirmado = 2
        Anulado = 3
    End Enum

    ''' <summary>
    ''' Obtiene el tipo de retencion
    ''' </summary>
    Public Enum eRetentionType
        Base = 1
        Rango = 2
        Variable = 3
    End Enum

    ''' <summary>
    ''' ENUMERADOR PARA IDENTIFICAR la seccion del TRM
    ''' </summary>
    Public Enum eTRMControls
        Advance
        Billing
        Refund
    End Enum

    Private Sub INDDiscountableIVA_SelectedIndexChanged(sender As Object, e As EventArgs) Handles INDDiscountableIVA.SelectedIndexChanged
        If TaxRegistration = 3 Then
            If DiscountableIVA Then
                Me.TaxRegistration = 2
            Else
                Me.TaxRegistration = 1
            End If
        End If
    End Sub
#End Region

End Class