'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Diego Andrés Roldán
' Created          : 23-05-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Libraries imported"
Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Controls
Imports DevExpress.Data.Linq
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.Data.Xpo.PaymentsRepository

#End Region

''' <summary>
''' 
''' </summary>
Public Interface IVoucherTransaction
    Inherits ICrudBase

#Region "Properties"

    ''' <summary>
    ''' Obteniene el tag del frontal
    ''' </summary>
    ''' <value>
    ''' My tag.
    ''' </value>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Obtiene o establece el layout para customizacion
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Obtiene o establece la secuencia de cabecera
    ''' </summary>
    ''' <value>
    ''' The sequense.
    ''' </value>
    Property Sequence As Domain.Entities.TreasurySequence

    ''' <summary>
    ''' Obtiene o establece el codigo del documento
    ''' </summary>
    ''' <value>
    ''' The code.
    ''' </value>
    Property Code As String

    ''' <summary>
    ''' Obtiene o establece el id del tercero
    ''' </summary>
    ''' <value>
    ''' The identifier third party.
    ''' </value>
    Property IdThirdParty As Integer?

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta contable
    ''' </summary>
    ''' <value>
    ''' The identifier main account.
    ''' </value>
    Property IdMainAccount As Integer

    ''' <summary>
    ''' Obtiene o establece el id del centro de costo
    ''' </summary>
    ''' <value>
    ''' The identifier cost center.
    ''' </value>
    Property IdCostCenter As Integer?

    ''' <summary>
    ''' Obtiene o establece si el comprobante afecta bancos o cajas
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [affects banks]; otherwise, <c>false</c>.
    ''' </value>
    Property ExpenseType As Short

    ''' <summary>
    ''' Obtiene o establece el detalle del comprobante
    ''' </summary>
    ''' <value>
    ''' The detail.
    ''' </value>
    Property obligation As String

    ''' <summary>
    ''' Obtiene o establece la fecha de confirmacion del documento
    ''' </summary>
    ''' <value>
    ''' The confirmation date.
    ''' </value>
    Property ConfirmationDate As DateTime?

    ''' <summary>
    ''' Obtiene o establece el Id de la caja
    ''' </summary>
    ''' <value>
    ''' The identifier cash register.
    ''' </value>
    Property IdCashRegister As Integer?

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta bancaria
    ''' </summary>
    ''' <value>
    ''' The identifier entity bank account.
    ''' </value>
    Property IdEntityBankAccount As Integer?

    ''' <summary>
    ''' Obtiene o establece el valor del comprobante
    ''' </summary>
    ''' <value>
    ''' The value.
    ''' </value>
    Property Value As Decimal

    ''' <summary>
    ''' Obtiene o establece el valor del iva del comprobante
    ''' </summary>
    ''' <value>
    ''' The value.
    ''' </value>
    Property ValueIVACtr As Decimal

    ''' <summary>
    ''' Obtiene o establece el método de pago si es con nota debito o cheque
    ''' </summary>
    ''' <value>
    ''' The payment method.
    ''' </value>
    Property PaymentMethod As Integer?

    ''' <summary>
    ''' Obtiene o establece el número de la nota
    ''' </summary>
    ''' <value>
    ''' The note number.
    ''' </value>
    Property NoteNumber As String

    ''' <summary>
    ''' Obtiene o establece el id del cheque
    ''' </summary>
    ''' <value>
    ''' The identifier checks.
    ''' </value>
    Property IdChecks As Integer?

    ''' <summary>
    ''' Obtiene o establece el numero del cheque
    ''' </summary>
    ''' <value>
    ''' The check number.
    ''' </value>
    Property CheckNumber As Long?

    ''' <summary>
    ''' Obtiene o establece la fecha de consignacion
    ''' </summary>
    ''' <value>
    ''' The transaction date.
    ''' </value>
    Property TransactionDate As DateTime?

    ''' <summary>
    ''' Obtiene o establece si aplica la tasa por mil
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [tax by mil]; otherwise, <c>false</c>.
    ''' </value>
    Property TaxByMil As Boolean

    ''' <summary>
    ''' Obtiene o establece la tasa por mil
    ''' </summary>
    ''' <value>
    ''' The tax by mil value.
    ''' </value>
    Property TaxByMilValue As Decimal?

    ''' <summary>
    ''' Obtiene o establece si hay egreso de caja menor
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [cash register expense]; otherwise, <c>false</c>.
    ''' </value>
    Property CashRegisterExpense As Boolean

    ''' <summary>
    ''' Obtiene o establece reembolso de egreso de caja menor
    ''' </summary>
    ''' <value>
    ''' <c>true</c> if [refound cash register expense]; otherwise, <c>false</c>.
    ''' </value>
    Property RefoundCashRegisterExpense As Boolean

    ''' <summary>
    ''' Obtiene o establece el id de la dispersion de fondos
    ''' </summary>
    ''' <value>
    ''' The identifier funds dispersion.
    ''' </value>
    Property IdFundsDispersion As Integer?

    ''' <summary>
    ''' Obtiene o establece la cedula del beneficiario
    ''' </summary>
    ''' <value>
    ''' The beneficiary identification.
    ''' </value>
    Property BeneficiaryIdentification As String

    ''' <summary>
    ''' Obtiene o establece el nombre del beneficiario
    ''' </summary>
    ''' <value>
    ''' The beneficiary.
    ''' </value>
    Property Beneficiary As String

    ''' <summary>
    ''' Obtiene o establece el egreso de la relacion de giro
    ''' </summary>
    ''' <value>
    ''' <c>true</c> if [transaction relation ship]; otherwise, <c>false</c>.
    ''' </value>
    Property TransactionRelationShip As Boolean

    ''' <summary>
    ''' Obtiene o establece el cheque conciliado
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [check reconciled]; otherwise, <c>false</c>.
    ''' </value>
    Property CheckReconciled As Boolean

    ''' <summary>
    ''' Obtiene o establece el id de la orden de pago
    ''' </summary>
    ''' <value>
    ''' The identifier payment order.
    ''' </value>
    Property IdPaymentOrder As Integer?

    ''' <summary>
    ''' Obtiene o establece si el comprobante es impreso
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [printed]; otherwise, <c>false</c>.
    ''' </value>
    Property Printed As Boolean

    ''' <summary>
    ''' Obtiene o establece el valor de retencion
    ''' </summary>
    ''' <value>
    ''' The rte value.
    ''' </value>
    Property RTEValue As Decimal

    ''' <summary>
    ''' Obtiene o establece el valor de iva
    ''' </summary>
    ''' <value>
    ''' The iva value.
    ''' </value>
    Property IVAValue As Decimal?

    ''' <summary>
    ''' Obtiene o establece el valor de ica
    ''' </summary>
    ''' <value>
    ''' The ica value.
    ''' </value>
    Property ICAValue As Decimal

    ''' <summary>
    ''' Obtiene o establece otros valores
    ''' </summary>
    ''' <value>
    ''' The other value.
    ''' </value>
    Property OtherValue As Decimal

    ''' <summary>
    ''' Obtiene o establece el numero de la cuenta bancaria
    ''' </summary>
    ''' <value>
    ''' The bank account number.
    ''' </value>
    Property BankAccountNumber As String

    ''' <summary>
    ''' Obtiene o establece el nombre del banco
    ''' </summary>
    ''' <value>
    ''' The name of the bank.
    ''' </value>
    Property BankName As String

    ''' <summary>
    ''' Detalles de la interface de presupuesto
    ''' </summary>
    ''' <value>
    ''' The details interface budget.
    ''' </value>
    Property DetailsInterfaceBudget As String

    ''' <summary>
    ''' Obtiene o establece el estado del comprobante de egreso
    ''' </summary>
    ''' <value>
    ''' The status.
    ''' </value>
    Property Status As String

    ''' <summary>
    ''' Obtiene la fecha del servidor
    ''' </summary>
    ''' <value>
    ''' The date server.
    ''' </value>
    Property DateServer As DateTime?

    ''' <summary>
    ''' Obtiene o establece el datasource de los terceros
    ''' </summary>
    ''' <value>
    ''' The third party datasource.
    ''' </value>
    Property ThirdPartyDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene las autorizaciones de resolución
    ''' </summary>
    ''' <returns></returns>
    Property listAuthorizationResolution As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource de cajas
    ''' </summary>
    ''' <value>
    ''' The cash datasource.
    ''' </value>
    Property CashDatasource As LinqInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece todos los centros de costo
    ''' </summary>
    ''' <value>
    ''' The cost center datasource.
    ''' </value>
    Property CostCenterDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource para las cuentas bancarias de entidades
    ''' </summary>
    ''' <value>
    ''' The entity account datasource.
    ''' </value>
    Property EntityAccountDatasource As LinqInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource de los conceptos de pago
    ''' </summary>
    ''' <value>
    ''' The payment concept datasource.
    ''' </value>
    Property PaymentConceptDatasource As LinqInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el parametro de de pagos
    ''' </summary>
    ''' <value>
    ''' The payment concept datasource.
    ''' </value>
    Property PaymentsSettingPaymentsXpo As PaymentsSettingPaymentsXpo

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' maneja documento soporte
    ''' </summary>
    ''' <returns></returns>
    Property HandlesDocumentSupport As Boolean?

    ''' <summary>
    ''' Obtiene o establece la autorizacion del documento soporte
    ''' </summary>
    ''' <returns></returns>
    Property AuthorizationResolutionId As Integer?

    ''' <summary>
    ''' Id de la moneda de la caja o la cuenta bancaria
    ''' </summary>
    ''' <returns></returns>
    Property CurrencyId As Integer?

    ''' <summary>
    ''' Abreviacion de la moneda segun standart ISO4217
    ''' </summary>
    ''' <returns></returns>
    Property CurrencyISO4217 As String

#End Region

End Interface
