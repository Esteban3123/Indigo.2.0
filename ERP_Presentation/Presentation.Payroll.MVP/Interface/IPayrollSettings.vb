'***********************************************************************
' Assembly         : Presentacion.FixedAsset.MVP
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 21-05-2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Controls
Imports Domain.Payroll.Entities
Imports DevExpress.Data.Linq

#End Region

Public Interface IPayrollSettings

    Inherits IcrudBase

    ''' <summary>
    ''' Impuesto del CREE
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CreeTax As Boolean

    ''' <summary>
    ''' Fórmula de Vacaciones
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property VacationFormulates As String

    ''' <summary>
    ''' Especifica si la entidad paga las vaciones adelantadas pero no se afecta el ibc reportado a la pila
    ''' </summary>
    ''' <returns></returns>
    Property HolidayWithoutAnticipateIBC As Boolean

    ''' <summary>
    ''' Fórmula de Bonificación de Vacaciones
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BonificationVacationFormulates As String

    ''' <summary>
    ''' Fórmula de Prima de Vacaciones
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property VacationIncentiveFormulates As String

    ''' <summary>
    ''' Fórmula de Incremento Vacacional
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property VacationalIncreaseFormulates As String

    ''' <summary>
    ''' Fórmula de Primas de Servicios / Junio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ServicesIncentivePaymentFormulates As String

    ''' <summary>
    ''' Fórmula de Primas de Diciembre / Navidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ChristmasIncentivePaymentFormulates As String

    ''' <summary>
    ''' Fecha Inicio de Prima de Servicios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property InitialDateServicesIncentivePayment As Date?

    ''' <summary>
    ''' Fecha Fin de Prima de Servicios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property EndDateServicesIncentivePayment As Date?

    ''' <summary>
    ''' Fecha Inicio de Prima de Navidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property InitialDateChristmasIncentivePayment As Date?

    ''' <summary>
    ''' Fecha Fin de Prima de Navidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property EndDateChristmasIncentivePayment As Date?

    ''' <summary>
    ''' Contribución de Retención en la Fuente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property HealthContributionRTF As Byte?

    ''' <summary>
    ''' Pago de Vacaciones en Fines de Semana
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property WeekendVacationPaid As Byte?

    Property ServicesIncentivePaymentConceptId As Integer?

    Property ChristmasIncentivePaymentConceptId As Integer?

    ''' <summary>
    ''' Propiedad de la Cuenta x Cobrar de Incapacidades
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property NoveltyMainAccountId As Integer?

    ''' <summary>
    ''' Propiedad de la Cuenta x Cobrar de Incapacidades
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property HealthPensionIBCVacation As Byte?

    ''' <summary>
    ''' Fórmula de Bonificación por Año de Servicio (Liq. de Contrato)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ContractLiquidationYearBonification As String

    ''' <summary>
    ''' Fórmula de Auxilio de Transporte (Liq. de Contrato)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ContractLiquidationTransportValue As String

    ''' <summary>
    ''' Formula de Auxilio de Alimentos (Liq. de Contrato)
    ''' </summary>
    ''' <returns></returns>
    Property ContractLiquidationFoodValue As String

    ''' <summary>
    ''' Formula de Vacaciones (Liq. de Contrato)
    ''' </summary>
    ''' <returns></returns>
    Property ContractLiquidationVacation As String

    ''' <summary>
    ''' Formula de Prima de Vacaciones (Liq. de Contrato)
    ''' </summary>
    ''' <returns></returns>
    Property ContractLiquidationIncentiveVacation As String

    ''' <summary>
    ''' Formula de Bonificación Especial para Recreación (Liq. de Contrato)
    ''' </summary>
    ''' <returns></returns>
    Property ContractLiquidationEspecialBonification As String

    ''' <summary>
    ''' Formula de Incremento Vacacional (Liq. de Contrato)
    ''' </summary>
    ''' <returns></returns>
    Property ContractLiquidationIncreaseVacational As String

    ''' <summary>
    ''' Formula de Prima de Servicios (Liq. de Contrato)
    ''' </summary>
    ''' <returns></returns>
    Property ContractLiquidationServiceIncentive As String

    ''' <summary>
    ''' Formula de Prima de Navidad (Liq. de Contrato)
    ''' </summary>
    ''' <returns></returns>
    Property ContractLiquidationChristmasIncentive As String

    ''' <summary>
    ''' Formula de Cesantias (Liq. de Contrato)
    ''' </summary>
    ''' <returns></returns>
    Property ContractLiquidationUnemployment As String

    ''' <summary>
    ''' Operador de PILA
    ''' </summary>
    ''' <returns></returns>
    Property PILAOperators As Byte?

    ''' <summary>
    ''' Tipo de Cálculo de las Cesantias
    ''' </summary>
    ''' <returns></returns>
    Property UnemploymentTypeCalculated As Byte?

    ''' <summary>
    ''' Id del Concepto de Retención
    ''' </summary>
    ''' <returns></returns>
    Property IdRetentionConcepts As Integer?

    ''' <summary>
    ''' Valor Salario Minimo Legal del Año Anterior
    ''' </summary>
    ''' <returns></returns>
    Property MinimunLegalSalaryLastYear As Decimal?

    ''' <summary>
    ''' Valor del Auxilio de Transporte del Año anterior
    ''' </summary>
    ''' <returns></returns>
    Property TransportValueLastYear As Decimal?

    ''' <summary>
    ''' Id del Concepto de Cesantias
    ''' </summary>
    ''' <returns></returns>
    Property IdConceptUnemployment As Integer?

    ''' <summary>
    ''' Id del Concepto de Intereses de Cesantias
    ''' </summary>
    ''' <returns></returns>
    Property IdConceptInterestUnemployment As Integer?

    ''' <summary>
    ''' ID del Concepto de Cuentas x Cobrar
    ''' </summary>
    ''' <returns></returns>
    Property IdAccountReceivableConcept As Integer?

    ''' <summary>
    ''' Establece el datasource de los conceptos
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property Concept_Datasource As Object

    ''' <summary>
    ''' Establece el datasource de los conceptos
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property ConceptVacation_Datasource As Object

    ''' <summary>
    ''' Obtiene o establece el datasource para las cuentas bancarias de entidades
    ''' </summary>
    ''' <value>
    ''' The entity account datasource.
    ''' </value>
    Property EntityAccountDatasource As LinqInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource para las cuentas bancarias de entidades
    ''' </summary>
    ''' <value>
    ''' The entity account datasource.
    ''' </value>
    Property EntityAccountDatasourceVacation As LinqInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource para las cuentas bancarias de entidades
    ''' </summary>
    ''' <value>
    ''' The entity account datasource.
    ''' </value>
    Property EntityAccountDatasourceLiquidation As LinqInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource de cajas
    ''' </summary>
    ''' <value>
    ''' The cash datasource.
    ''' </value>
    Property CashDatasource As LinqInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource de cajas (Para vacaciones)
    ''' </summary>
    ''' <value>
    ''' The cash datasource.
    ''' </value>
    Property CashDatasourceVacation As LinqInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource de cajas (Para Liquidación de Contrato)
    ''' </summary>
    ''' <value>
    ''' The cash datasource.
    ''' </value>
    Property CashDatasourceLiquidation As LinqInstantFeedbackSource

    ''' <summary>
    ''' Id de la Caja
    ''' </summary>
    ''' <returns></returns>
    Property IdCashRegister As Integer?

    ''' <summary>
    ''' Id de la Caja
    ''' </summary>
    ''' <returns></returns>
    Property IdCashRegisterVacation As Integer?

    ''' <summary>
    ''' Id de la Caja
    ''' </summary>
    ''' <returns></returns>
    Property IdCashRegisterLiquidation As Integer?

    ''' <summary>
    ''' Id de la Cuenta Bancaria
    ''' </summary>
    ''' <returns></returns>
    Property IdEntityBankAccount As Integer?

    ''' <summary>
    ''' Id de la Cuenta Bancaria
    ''' </summary>
    ''' <returns></returns>
    Property IdEntityBankAccountVacation As Integer?

    ''' <summary>
    ''' Id de la Cuenta Bancaria
    ''' </summary>
    ''' <returns></returns>
    Property IdEntityBankAccountLiquidation As Integer?

    ''' <summary>
    ''' Tipo de Egreso
    ''' </summary>
    ''' <returns></returns>
    Property ExpenseType As Byte?

    ''' <summary>
    ''' Tipo de Egreso
    ''' </summary>
    ''' <returns></returns>
    Property ExpenseTypeVacation As Byte?

    ''' <summary>
    ''' Tipo de Egreso
    ''' </summary>
    ''' <returns></returns>
    Property ExpenseTypeLiquidation As Byte?

    '''' <summary>
    '''' Método de Pago
    '''' </summary>
    '''' <returns></returns>
    'Property PaymentMethod As Byte?

    ''' <summary>
    ''' Obtiene o establece el datasource de cajas
    ''' </summary>
    ''' <value>
    ''' The cash datasource.
    ''' </value>
    Property ExpenseConceptDatasourceVacation As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource de cajas
    ''' </summary>
    ''' <value>
    ''' The cash datasource.
    ''' </value>
    Property ExpenseConceptDatasourceLiquidation As XPInstantFeedbackSource

    ''' <summary>
    ''' Contabilizacion de prestaciones sociales
    ''' </summary>
    ''' <returns></returns>
    Property InabilityAccountedBy As Byte

    ''' <summary>
    ''' Distribución de nómina (prorrateo centros de costo de nómina)
    ''' </summary>
    ''' <returns></returns>
    Property PayrollDistribution As Byte

    Property GenerateVoucherTransactionVacation As Boolean

    Property GenerateVoucherTransactionContractLiquidation As Boolean

    ''' <summary>
    ''' Obtiene el codigo del campo clase de aportante
    ''' </summary>
    ''' <returns></returns>
    Property ContributionClass As String


    ''' <summary>
    ''' Obtiene o establece datasource de las notas de concepto administracion d efectivo
    ''' </summary>
    Property NoteConceptsDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource de notas y traslados d cuentas x cobrar
    ''' </summary>
    Property PortfolioNoteConceptDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource del Concepto Flujo de Efectivo Recaudo Convenio CxC
    ''' </summary>
    Property CashFlowConceptDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el parámetro de si liquida o no cesantías
    ''' </summary>
    ''' <returns></returns>
    Property LiquidatesSeverancePayments As Boolean
    ''' <summary>
    ''' Propiedad para incapacidades salario Integral
    ''' </summary>
    ''' <returns></returns>
    Property IntegralSalaryInability As Boolean
    ''' <summary>
    ''' Concepto de Incapacidad Asumido Salario Integral 
    ''' </summary>
    ''' <returns></returns>
    Property IntegralSalaryInabilityConcept As Integer?

    ''' <summary>
    ''' Meses permitidos para ajustes extemporáneos de Cargo y Salario Básico (0-6)
    ''' </summary>
    ''' <returns>Cantidad de meses hacia atrás permitidos (0 = deshabilitado, 1-6 = meses permitidos)</returns>
    ''' <remarks>
    ''' Este parámetro define cuántos meses hacia atrás se pueden registrar ajustes extemporáneos
    ''' de Cargo y Salario Básico sin afectar nóminas ya liquidadas ni generar retroactivos.
    ''' Valor 0 = Ajustes extemporáneos deshabilitados.
    ''' </remarks>
    Property AllowedMonthsForExtemporaneousAdjustments As Byte

End Interface
