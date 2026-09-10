'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Diego Andrés Roldán
' Created          : 18-03-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Libraries imported"
Imports Presentation.Base
Imports Presentation.Controls
Imports DevExpress.Xpo

#End Region

Public Interface ISettingsTreasury
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Gets my layout control.
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Obtiene o establece el id de la unidad operativa
    ''' </summary>
    ''' <value>
    ''' The identifier operating unit.
    ''' </value>
    Property IdOperatingUnit As Integer

    ''' <summary>
    ''' Obtiene o establece si confirma presupuesto
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [budget confirm]; otherwise, <c>false</c>.
    ''' </value>
    Property BudgetConfirm As Boolean

    ''' <summary>
    ''' Obtiene o establece si parametriza presupuesto por concepto
    ''' </summary>
    ''' <value>
    ''' <c>true</c> if [setting budgetfor concept]; otherwise, <c>false</c>.
    ''' </value>
    Property SettingBudgetforConcept As Boolean

    ''' <summary>
    ''' Obtiene o establece si controla el consecutivo de las chequeras de cuentas bancarias
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [ckeck book control]; otherwise, <c>false</c>.
    ''' </value>
    Property CheckBookControl As Boolean

    ''' <summary>
    ''' Obtiene o establece si genera orden de pago automaticamente
    ''' </summary>
    ''' <value>
    ''' <c>true</c> if [build payment order automatic]; otherwise, <c>false</c>.
    ''' </value>
    Property BuildPaymentOrderAutomatic As Boolean

    ''' <summary>
    ''' Obtiene o establece si permite modificar las fechas de los documentos
    ''' </summary>
    ''' <value>
    ''' <c>true</c> if [allow modify documents dates]; otherwise, <c>false</c>.
    ''' </value>
    Property AllowModifyDocumentsDates As Boolean

    ''' <summary>
    ''' Obtiene o establece el tipo de comprobante de recibos de caja
    ''' </summary>
    ''' <value>
    ''' The journal voucher type cash receipts.
    ''' </value>
    Property JournalVoucherTypeCashReceipts As Integer

    ''' <summary>
    ''' Obtiene o establece el tipo de comprobante de comprobantes de egreso
    ''' </summary>
    ''' <value>
    ''' The journal voucher type voucher transaction.
    ''' </value>
    Property JournalVoucherTypeVoucherTransaction As Integer

    ''' <summary>
    ''' Obtiene o establece el tipo de comprobante de comprobantes de egreso cruce
    ''' </summary>
    ''' <value>
    ''' The journal voucher type voucher transaction crossing.
    ''' </value>
    Property JournalVoucherTypeVoucherTransactionCrossing As Integer

    ''' <summary>
    ''' Obtiene o establece el tipo de comprobante de consignaciones bancarias
    ''' </summary>
    ''' <value>
    ''' The journal voucher type bank appropriations.
    ''' </value>
    Property JournalVoucherTypeBankAppropriations As Integer

    ''' <summary>
    ''' Obtiene o establece el tipo de comprobante de notas de tesoreria
    ''' </summary>
    ''' <value>
    ''' The journal voucher type treasury notes.
    ''' </value>
    Property JournalVoucherTypeTreasuryNotes As Integer

    ''' <summary>
    ''' Id del fondo de caja menor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property JournalVoucherTypeConstitutionCashId As Integer?

    ''' <summary>
    ''' Datasource del fondo de caja menor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property JournalVoucherTypeConstitutionCashXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el gravemen Movimientos financieros (4x1000) - Id Cuenta Contable Cuenta por pagar
    ''' </summary>
    ''' <value>
    ''' The FMG main account payment.
    ''' </value>
    Property FMGMainAccountPayment As Integer

    ''' <summary>
    ''' Obtiene o establece el Gravamen Movimientos Financieros (4x1000) -  Id Cuenta Contable Gastos
    ''' </summary>
    ''' <value>
    ''' The FMG main account expenses.
    ''' </value>
    Property FMGMainAccountExpenses As Integer

    ''' <summary>
    ''' sabado habil
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [saturday]; otherwise, <c>false</c>.
    ''' </value>
    Property Saturday As Boolean

    ''' <summary>
    ''' domingo habil
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [sunday]; otherwise, <c>false</c>.
    ''' </value>
    Property Sunday As Boolean

    ''' <summary>
    ''' Obtiene o establece el datasource de recibos de caja
    ''' </summary>
    ''' <value>
    ''' The datasource cash receip.
    ''' </value>
    Property DatasourceCashReceipt As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource de comprobantes de egreso
    ''' </summary>
    ''' <value>
    ''' The datasource voucher transaction.
    ''' </value>
    Property DatasourceVoucherTransaction As XPInstantFeedbackSource

    ''' <summary>
    ''' Gets or sets the datasource voucher transaction crossing.
    ''' </summary>
    ''' <value>
    ''' The datasource voucher transaction crossing.
    ''' </value>
    Property DatasourceVoucherTransactionCrossing As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource de consignaciones
    ''' </summary>
    ''' <value>
    ''' The datasource bank appropriations.
    ''' </value>
    Property DatasourceBankAppropriation As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource de notas de tesoreria
    ''' </summary>
    ''' <value>
    ''' The datasource treasury notes.
    ''' </value>
    Property DatasourceTreasuryNote As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource de cuentas contables de pagos
    ''' </summary>
    ''' <value>
    ''' The datasource main account payment.
    ''' </value>
    Property DatasourceMainAccountPayment As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource de cuentas contables de gastos
    ''' </summary>
    ''' <value>
    ''' The datasource main account expense.
    ''' </value>
    Property DatasourceMainAccountExpense As XPInstantFeedbackSource

    ' ''' <summary>
    ' ''' Esta propiedad establece el valor ControlAcciones
    ' ''' </summary>
    ' ''' <value>
    ' '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ' ''' </value>
    'WriteOnly Property ActionsOnControls As Boolean

#End Region

End Interface
