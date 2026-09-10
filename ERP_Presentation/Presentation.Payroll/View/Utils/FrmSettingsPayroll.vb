Imports System.ComponentModel
Imports System.Windows.Forms
Imports DevExpress.Data
Imports DevExpress.Data.Linq
Imports DevExpress.Xpo
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraEditors.Design
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.Controls.MVP
Imports Presentation.Payroll.MVP

Public Class FrmSettingsPayroll
    Implements IPayrollSettings

    Implements IDataColumnInfo

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Payroll"

#Region "Variables Globales"

    ''' <summary>
    ''' Variable para controlar el presentador del frontal
    ''' </summary>
    ''' <remarks></remarks>
    Dim presenter As PPayrollSettings

    Dim PayrollSettings As PayrollSettings

    Dim FlagSave As Boolean = False

    ''' <summary>
    ''' Variable para poder acceder al Modelo
    ''' </summary>
    Dim Model As New MFunds(MyBase.Tag)

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListAdquisitionType As New List(Of Tuple(Of Byte, String))

    Dim ListPaidVacation As New List(Of Tuple(Of Byte, String))

    Dim ListIBCVacation As New List(Of Tuple(Of Byte, String))

    Dim ListPILAOperators As New List(Of Tuple(Of Byte, String))

    Dim ListTypeCalculationUnemployement As New List(Of Tuple(Of Byte, String))

    Dim ListComprobantType As List(Of Tuple(Of Byte, String))

    Dim ListGenerate As List(Of Tuple(Of Byte, String))

    Dim ListAccountedBy As List(Of Tuple(Of Byte, String))

    ''' <summary>
    ''' Variable que contiene la forma de distribuir centros de costos en nómina.
    ''' </summary>
    Dim ListPayrollDistribution As New List(Of Tuple(Of Byte, String))

    ''' <summary>
    ''' Listado de los registros de iva
    ''' </summary>
    Private _listContributionClass As List(Of Tuple(Of String, String))

#End Region

#Region "Properties"

    ''' <summary>
    ''' Especifica si la entidad paga las vacaciones adelantadas pero no se afecta el ibc reportado a la pila
    ''' </summary>
    ''' <returns></returns>
    Public Property HolidayWithoutAnticipateIBC As Boolean Implements IPayrollSettings.HolidayWithoutAnticipateIBC
        Get
            Return INDsleHolidayWithoutAnticipateIBC.EditValue
        End Get
        Set(value As Boolean)
            INDsleHolidayWithoutAnticipateIBC.EditValue = value
        End Set
    End Property

    Public Property BonificationVacationFormulates As String Implements IPayrollSettings.BonificationVacationFormulates
        Get
            Return INDMemoBonification.EditValue
        End Get
        Set(value As String)
            INDMemoBonification.EditValue = value
        End Set
    End Property

    Public Property CreeTax As Boolean Implements IPayrollSettings.CreeTax
        Get
            Return INDSleCreeTax.EditValue
        End Get
        Set(value As Boolean)
            INDSleCreeTax.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el nit o identificacion del tercero
    ''' </summary>
    Public Property ThirdPartyId As String
        Get
            Return INDSLUpThirdPartyId.EditValue
        End Get
        Set(value As String)
            INDSLUpThirdPartyId.EditValue = value
        End Set
    End Property

    Public Property VacationalIncreaseFormulates As String Implements IPayrollSettings.VacationalIncreaseFormulates
        Get
            Return INDMemoVacationalIncrease.EditValue
        End Get
        Set(value As String)
            INDMemoVacationalIncrease.EditValue = value
        End Set
    End Property

    Public Property VacationFormulates As String Implements IPayrollSettings.VacationFormulates
        Get
            Return INDMemoVacation.EditValue
        End Get
        Set(value As String)
            INDMemoVacation.EditValue = value
        End Set
    End Property

    Public Property VacationIncentiveFormulates As String Implements IPayrollSettings.VacationIncentiveFormulates
        Get
            Return INDMemoVacationIncentive.EditValue
        End Get
        Set(value As String)
            INDMemoVacationIncentive.EditValue = value
        End Set
    End Property

    Public Property ChristmasIncentivePaymentFormulates As String Implements IPayrollSettings.ChristmasIncentivePaymentFormulates
        Get
            Return INDMemoIncentivePaymentDecember.EditValue
        End Get
        Set(value As String)
            INDMemoIncentivePaymentDecember.EditValue = value
        End Set
    End Property

    Public Property ServicesIncentivePaymentFormulates As String Implements IPayrollSettings.ServicesIncentivePaymentFormulates
        Get
            Return INDMemoIncentivePaymentServices.EditValue
        End Get
        Set(value As String)
            INDMemoIncentivePaymentServices.EditValue = value
        End Set
    End Property

    Public Property EndDateChristmasIncentivePayment As Date? Implements IPayrollSettings.EndDateChristmasIncentivePayment
        Get
            Return INDDeDecemberIncentiveEndDate.EditValue
        End Get
        Set(value As Date?)
            INDDeDecemberIncentiveEndDate.EditValue = value
        End Set
    End Property

    Public Property EndDateServicesIncentivePayment As Date? Implements IPayrollSettings.EndDateServicesIncentivePayment
        Get
            Return INDDeServicesIncentiveEndDate.EditValue
        End Get
        Set(value As Date?)
            INDDeServicesIncentiveEndDate.EditValue = value
        End Set
    End Property

    Public Property InitialDateChristmasIncentivePayment As Date? Implements IPayrollSettings.InitialDateChristmasIncentivePayment
        Get
            Return INDDeDecemberIncentiveStarDate.EditValue
        End Get
        Set(value As Date?)
            INDDeDecemberIncentiveStarDate.EditValue = value
        End Set
    End Property

    Public Property InitialDateServicesIncentivePayment As Date? Implements IPayrollSettings.InitialDateServicesIncentivePayment
        Get
            Return INDDeServicesIncentiveStarDate.EditValue
        End Get
        Set(value As Date?)
            INDDeServicesIncentiveStarDate.EditValue = value
        End Set
    End Property

    Public Property HealthContributionRTF As Byte? Implements IPayrollSettings.HealthContributionRTF
        Get
            Return INDSlRTFHealthContribution.EditValue
        End Get
        Set(value As Byte?)
            INDSlRTFHealthContribution.EditValue = value
        End Set
    End Property

    Public Property WeekendVacationPaid As Byte? Implements IPayrollSettings.WeekendVacationPaid
        Get
            Return INDSlPaidVacation.EditValue
        End Get
        Set(value As Byte?)
            INDSlPaidVacation.EditValue = value
        End Set
    End Property

    Public Property ServicesIncentivePaymentConceptId As Integer? Implements IPayrollSettings.ServicesIncentivePaymentConceptId
        Get
            Return INDSlConceptServiceIncentive.EditValue
        End Get
        Set(value As Integer?)
            INDSlConceptServiceIncentive.EditValue = value
        End Set
    End Property

    Public Property IdConceptInterestUnemployment As Integer? Implements IPayrollSettings.IdConceptInterestUnemployment
        Get
            Return INDSlInterestUnemploymentConcept.EditValue
        End Get
        Set(value As Integer?)
            INDSlInterestUnemploymentConcept.EditValue = value
        End Set
    End Property

    Public Property IdConceptUnemployment As Integer? Implements IPayrollSettings.IdConceptUnemployment
        Get
            Return INDSlUnemploymentConcept.EditValue
        End Get
        Set(value As Integer?)
            INDSlUnemploymentConcept.EditValue = value
        End Set
    End Property

    Public Property ChristmasIncentivePaymentConceptId As Integer? Implements IPayrollSettings.ChristmasIncentivePaymentConceptId
        Get
            Return INDSlConceptChristmasIncentive.EditValue
        End Get
        Set(value As Integer?)
            INDSlConceptChristmasIncentive.EditValue = value
        End Set
    End Property

    Public Property NoveltyMainAccountId As Integer? Implements IPayrollSettings.NoveltyMainAccountId
        Get
            Return INDSlAccountNovelty.EditValue
        End Get
        Set(value As Integer?)
            INDSlAccountNovelty.EditValue = value
        End Set
    End Property

    Public Property HealthPensionIBCVacation As Byte? Implements IPayrollSettings.HealthPensionIBCVacation
        Get
            Return INDSlIBCVacation.EditValue
        End Get
        Set(value As Byte?)
            INDSlIBCVacation.EditValue = value
        End Set
    End Property


    Public Property ContractLiquidationYearBonification As String Implements IPayrollSettings.ContractLiquidationYearBonification
        Get
            Return INDMemoContractLiquidationYearBonification.EditValue
        End Get
        Set(value As String)
            INDMemoContractLiquidationYearBonification.EditValue = value
        End Set
    End Property

    Public Property ContractLiquidationTransportValue As String Implements IPayrollSettings.ContractLiquidationTransportValue
        Get
            Return INDMemoContractLiquidationTransportValue.EditValue
        End Get
        Set(value As String)
            INDMemoContractLiquidationTransportValue.EditValue = value
        End Set
    End Property

    Public Property ContractLiquidationFoodValue As String Implements IPayrollSettings.ContractLiquidationFoodValue
        Get
            Return INDMemoContractLiquidationFoodValue.EditValue
        End Get
        Set(value As String)
            INDMemoContractLiquidationFoodValue.EditValue = value
        End Set
    End Property

    Public Property ContractLiquidationVacation As String Implements IPayrollSettings.ContractLiquidationVacation
        Get
            Return INDMemoContractLiquidationVacation.EditValue
        End Get
        Set(value As String)
            INDMemoContractLiquidationVacation.EditValue = value
        End Set
    End Property

    Public Property ContractLiquidationIncentiveVacation As String Implements IPayrollSettings.ContractLiquidationIncentiveVacation
        Get
            Return INDMemoContractLiquidationIncentiveVacation.EditValue
        End Get
        Set(value As String)
            INDMemoContractLiquidationIncentiveVacation.EditValue = value
        End Set
    End Property

    Public Property ContractLiquidationEspecialBonification As String Implements IPayrollSettings.ContractLiquidationEspecialBonification
        Get
            Return INDMemoContractLiquidationEspecialBonification.EditValue
        End Get
        Set(value As String)
            INDMemoContractLiquidationEspecialBonification.EditValue = value
        End Set
    End Property

    Public Property ContractLiquidationIncreaseVacational As String Implements IPayrollSettings.ContractLiquidationIncreaseVacational
        Get
            Return INDMemoContractLiquidationIncreaseVacational.EditValue
        End Get
        Set(value As String)
            INDMemoContractLiquidationIncreaseVacational.EditValue = value
        End Set
    End Property

    Public Property ContractLiquidationServiceIncentive As String Implements IPayrollSettings.ContractLiquidationServiceIncentive
        Get
            Return INDMemoContractLiquidationServiceIncentive.EditValue
        End Get
        Set(value As String)
            INDMemoContractLiquidationServiceIncentive.EditValue = value
        End Set
    End Property

    Public Property ContractLiquidationChristmasIncentive As String Implements IPayrollSettings.ContractLiquidationChristmasIncentive
        Get
            Return INDMemoContractLiquidationChristmasIncentive.EditValue
        End Get
        Set(value As String)
            INDMemoContractLiquidationChristmasIncentive.EditValue = value
        End Set
    End Property
    Public Property ContractLiquidationUnemployment As String Implements IPayrollSettings.ContractLiquidationUnemployment
        Get
            Return INDMemoContractLiquidationUnemployment.EditValue
        End Get
        Set(value As String)
            INDMemoContractLiquidationUnemployment.EditValue = value
        End Set
    End Property

    Public Property PILAOperators As Byte? Implements IPayrollSettings.PILAOperators
        Get
            Return INDSlPILAOperators.EditValue
        End Get
        Set(value As Byte?)
            INDSlPILAOperators.EditValue = value
        End Set
    End Property

    Public Property IdRetentionConcepts As Integer? Implements IPayrollSettings.IdRetentionConcepts
        Get
            Return INDSlConceptRetention.EditValue
        End Get
        Set(value As Integer?)
            INDSlConceptRetention.EditValue = value
        End Set
    End Property

    Public Property UnemploymentTypeCalculated As Byte? Implements IPayrollSettings.UnemploymentTypeCalculated
        Get
            Return INDSlUnemployementCalculated.EditValue
        End Get
        Set(value As Byte?)
            INDSlUnemployementCalculated.EditValue = value
        End Set
    End Property

    Public Property MinimunLegalSalaryLastYear As Decimal? Implements IPayrollSettings.MinimunLegalSalaryLastYear
        Get
            Return INDSpMinimunLegalSalaryLastYear.EditValue
        End Get
        Set(value As Decimal?)
            INDSpMinimunLegalSalaryLastYear.EditValue = value
        End Set
    End Property

    Public Property TransportValueLastYear As Decimal? Implements IPayrollSettings.TransportValueLastYear
        Get
            Return INDSpTransportValueLastYear.EditValue
        End Get
        Set(value As Decimal?)
            INDSpTransportValueLastYear.EditValue = value
        End Set
    End Property

    Public Property IdAccountReceivableConcept As Integer? Implements IPayrollSettings.IdAccountReceivableConcept
        Get
            Return INDSlAccountReceivableConcept.EditValue
        End Get
        Set(value As Integer?)
            INDSlAccountReceivableConcept.EditValue = value
        End Set
    End Property

    Public Property AccountNoveltyXpo As XPInstantFeedbackSource
        Get
            Return CType(INDSlAccountNovelty.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSlAccountNovelty.Properties.DataSource = value
        End Set
    End Property

    Public Property ConceptRetentionXpo As XPInstantFeedbackSource
        Get
            Return CType(INDSlConceptRetention.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSlConceptRetention.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de los conceptos
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Concept_Datasource As Object Implements IPayrollSettings.Concept_Datasource
        Set(value As Object)
            INDSlConceptAdjustPrincipal.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de los conceptos
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ConceptVacation_Datasource As Object Implements IPayrollSettings.ConceptVacation_Datasource
        Set(value As Object)
            INDSlConceptVacation.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource para las cuentas bancarias de entidades
    ''' </summary>
    ''' <value>
    ''' The entity account datasource.
    ''' </value>
    Public Property EntityAccountDatasource As LinqInstantFeedbackSource Implements IPayrollSettings.EntityAccountDatasource
        Get
            Return CType(INDsleEntityAccount.Properties.DataSource, LinqInstantFeedbackSource)
        End Get
        Set(value As LinqInstantFeedbackSource)
            INDsleEntityAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource para las cuentas bancarias de entidades
    ''' </summary>
    ''' <value>
    ''' The entity account datasource.
    ''' </value>
    Public Property EntityAccountDatasourceVacation As LinqInstantFeedbackSource Implements IPayrollSettings.EntityAccountDatasourceVacation
        Get
            Return CType(INDsleEntityAccountVacation.Properties.DataSource, LinqInstantFeedbackSource)
        End Get
        Set(value As LinqInstantFeedbackSource)
            INDsleEntityAccountVacation.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource para las cuentas bancarias de entidades
    ''' </summary>
    ''' <value>
    ''' The entity account datasource.
    ''' </value>
    Public Property EntityAccountDatasourceLiquidation As LinqInstantFeedbackSource Implements IPayrollSettings.EntityAccountDatasourceLiquidation
        Get
            Return CType(INDsleEntityAccountLiquidation.Properties.DataSource, LinqInstantFeedbackSource)
        End Get
        Set(value As LinqInstantFeedbackSource)
            INDsleEntityAccountLiquidation.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de cajas
    ''' </summary>
    ''' <value>
    ''' The cash datasource.
    ''' </value>
    Public Property CashDatasource As LinqInstantFeedbackSource Implements IPayrollSettings.CashDatasource
        Get
            Return CType(INDsleCash.Properties.DataSource, LinqInstantFeedbackSource)
        End Get
        Set(value As LinqInstantFeedbackSource)
            INDsleCash.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de cajas (Vacaciones)
    ''' </summary>
    ''' <value>
    ''' The cash datasource.
    ''' </value>
    Public Property CashDatasourceVacation As LinqInstantFeedbackSource Implements IPayrollSettings.CashDatasourceVacation
        Get
            Return CType(INDsleCashVacation.Properties.DataSource, LinqInstantFeedbackSource)
        End Get
        Set(value As LinqInstantFeedbackSource)
            INDsleCashVacation.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de cajas (Liquidación de Contrato)
    ''' </summary>
    ''' <value>
    ''' The cash datasource.
    ''' </value>
    Public Property CashDatasourceLiquidation As LinqInstantFeedbackSource Implements IPayrollSettings.CashDatasourceLiquidation
        Get
            Return CType(INDsleCashLiquidation.Properties.DataSource, LinqInstantFeedbackSource)
        End Get
        Set(value As LinqInstantFeedbackSource)
            INDsleCashLiquidation.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Id de las Cajas
    ''' </summary>
    ''' <value>
    ''' The cash datasource.
    ''' </value>
    Public Property IdCashRegister As Integer? Implements IPayrollSettings.IdCashRegister
        Get
            Return INDsleCash.EditValue
        End Get
        Set(value As Integer?)
            INDsleCash.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Id de las Cajas
    ''' </summary>
    ''' <value>
    ''' The cash datasource.
    ''' </value>
    Public Property IdCashRegisterVacation As Integer? Implements IPayrollSettings.IdCashRegisterVacation
        Get
            Return INDsleCashVacation.EditValue
        End Get
        Set(value As Integer?)
            INDsleCashVacation.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Id de las Cajas
    ''' </summary>
    ''' <value>
    ''' The cash datasource.
    ''' </value>
    Public Property IdCashRegisterLiquidation As Integer? Implements IPayrollSettings.IdCashRegisterLiquidation
        Get
            Return INDsleCashLiquidation.EditValue
        End Get
        Set(value As Integer?)
            INDsleCashLiquidation.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Id de la Cuenta Bancaria
    ''' </summary>
    ''' <value>
    ''' The cash datasource.
    ''' </value>
    Public Property IdEntityBankAccount As Integer? Implements IPayrollSettings.IdEntityBankAccount
        Get
            Return INDsleEntityAccount.EditValue
        End Get
        Set(value As Integer?)
            INDsleEntityAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Id de la Cuenta Bancaria
    ''' </summary>
    ''' <value>
    ''' The cash datasource.
    ''' </value>
    Public Property IdEntityBankAccountVacation As Integer? Implements IPayrollSettings.IdEntityBankAccountVacation
        Get
            Return INDsleEntityAccountVacation.EditValue
        End Get
        Set(value As Integer?)
            INDsleEntityAccountVacation.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Id de la Cuenta Bancaria
    ''' </summary>
    ''' <value>
    ''' The cash datasource.
    ''' </value>
    Public Property IdEntityBankAccountLiquidation As Integer? Implements IPayrollSettings.IdEntityBankAccountLiquidation
        Get
            Return INDsleEntityAccountLiquidation.EditValue
        End Get
        Set(value As Integer?)
            INDsleEntityAccountLiquidation.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Tipo de Egreso
    ''' </summary>
    ''' <value>
    ''' The cash datasource.
    ''' </value>
    Public Property ExpenseType As Byte? Implements IPayrollSettings.ExpenseType
        Get
            Return INDSlComprobantType.EditValue
        End Get
        Set(value As Byte?)
            INDSlComprobantType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Tipo de Egreso
    ''' </summary>
    ''' <value>
    ''' The cash datasource.
    ''' </value>
    Public Property ExpenseTypeVacation As Byte? Implements IPayrollSettings.ExpenseTypeVacation
        Get
            Return INDSlComprobantTypeVacation.EditValue
        End Get
        Set(value As Byte?)
            INDSlComprobantTypeVacation.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Tipo de Egreso
    ''' </summary>
    ''' <value>
    ''' The cash datasource.
    ''' </value>
    Public Property ExpenseTypeLiquidation As Byte? Implements IPayrollSettings.ExpenseTypeLiquidation
        Get
            Return INDSlComprobantTypeLiquidation.EditValue
        End Get
        Set(value As Byte?)
            INDSlComprobantTypeLiquidation.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource para las cuentas bancarias de entidades
    ''' </summary>
    ''' <value>
    ''' The entity account datasource.
    ''' </value>
    Public Property ExpenseConceptDatasourceLiquidation As XPInstantFeedbackSource Implements IPayrollSettings.ExpenseConceptDatasourceLiquidation
        Get
            Return CType(INDSlExpenseConceptLiquidation.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSlExpenseConceptLiquidation.Properties.DataSource = value
        End Set
    End Property

    Public Property ExpenseConceptDatasourceVacation As XPInstantFeedbackSource Implements IPayrollSettings.ExpenseConceptDatasourceVacation
        Get
            Return CType(INDSlVacationExpenseConcept.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSlVacationExpenseConcept.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' metodo para Cargar el data source de las notas de concepto administracion d efectivo
    ''' </summary>
    ''' <value>
    ''' </value>
    Public Property NoteConceptsDatasource As XPInstantFeedbackSource Implements IPayrollSettings.NoteConceptsDatasource
        Get
            Return CType(INDSlNoteConcepts.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSlNoteConcepts.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' metodo para Cargar el data source de notas y traslados d cuentas x cobrar
    ''' </summary>
    ''' <remarks></remarks>
    Public Property PortfolioNoteConceptDatasource As XPInstantFeedbackSource Implements IPayrollSettings.PortfolioNoteConceptDatasource
        Get
            Return CType(INDSlPortfolioNoteConcept.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSlPortfolioNoteConcept.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' metodo para Cargar el data source del Concepto Flujo de Efectivo Recaudo Convenio CxC
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDSlCashFlowConcept_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSlCashFlowConcept.QueryPopUp
        If Me.INDSlCashFlowConcept.Properties.DataSource Is Nothing Then
            INDSlCashFlowConcept.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).TreasuryService.ListCashFlowConcept()
        End If
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source de notas y traslados d cuentas x cobrar
    ''' </summary>
    ''' <remarks></remarks>
    Public Property CashFlowConceptDatasource As XPInstantFeedbackSource Implements IPayrollSettings.CashFlowConceptDatasource
        Get
            Return CType(INDSlCashFlowConcept.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSlCashFlowConcept.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad para el concepto de incapacidad salario integral
    ''' </summary>
    ''' <returns></returns>
    Public Property IntegralSalaryInabilityConcept As Integer? Implements IPayrollSettings.IntegralSalaryInabilityConcept
        Get
            Return INDsleIntegralInabilityConcept.EditValue
        End Get
        Set(value As Integer?)
            INDsleIntegralInabilityConcept.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Contabilizacion de prestaciones sociales
    ''' </summary>
    ''' <value>
    ''' The cash datasource.
    ''' </value>
    Public Property InabilityAccountedBy As Byte Implements IPayrollSettings.InabilityAccountedBy
        Get
            Return INDsleInabilityAccountedBy.EditValue
        End Get
        Set(value As Byte)
            INDsleInabilityAccountedBy.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Distribución de nómina
    ''' </summary>
    Public Property PayrollDistribution As Byte Implements IPayrollSettings.PayrollDistribution
        Get
            Return INDslePayrollDistribution.EditValue
        End Get
        Set(value As Byte)
            INDslePayrollDistribution.EditValue = value
        End Set
    End Property

    Public Property GenerateVoucherTransactionVacation As Boolean Implements IPayrollSettings.GenerateVoucherTransactionVacation
        Get
            Return IndSlYesNoVacationVoucherTransaction.EditValue
        End Get
        Set(value As Boolean)
            IndSlYesNoVacationVoucherTransaction.EditValue = value
        End Set
    End Property

    Public Property GenerateVoucherTransactionContractLiquidation As Boolean Implements IPayrollSettings.GenerateVoucherTransactionContractLiquidation
        Get
            Return IndSlYesNoContractLiquidationVoucherTransaction.EditValue
        End Get
        Set(value As Boolean)
            IndSlYesNoContractLiquidationVoucherTransaction.EditValue = value
        End Set
    End Property

    Public Property ContributionClass As String Implements IPayrollSettings.ContributionClass
        Get
            Return INDsleContributionClass.EditValue
        End Get
        Set(value As String)
            INDsleContributionClass.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el parámetro de si liquida o no cesantías
    ''' </summary>
    ''' <returns></returns>
    Public Property LiquidatesSeverancePayments As Boolean Implements IPayrollSettings.LiquidatesSeverancePayments
        Get
            Return INDrgbLiquidatesSeverancePayments.EditValue
        End Get
        Set(value As Boolean)
            INDrgbLiquidatesSeverancePayments.EditValue = value
            If indigo.LanguageCulture = "es-CR" Then
                INDrgbLiquidatesSeverancePayments.EditValue = False
            End If
        End Set
    End Property

    ''' <summary>
    '''  Obtiene o establece el parametro de moneda
    ''' </summary>
    ''' <returns></returns>
    Public Property CurrencyId As Integer
        Get
            Return INDsleCurrency.EditValue
        End Get
        Set(value As Integer)
            INDsleCurrency.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad para el factor prestacional salario integral
    ''' </summary>
    ''' <returns></returns>
    Public Property IntegralSalaryInability As Boolean Implements IPayrollSettings.IntegralSalaryInability
        Get
            Return CtrYesNoIntegralInability.EditValue
        End Get
        Set(value As Boolean)
            CtrYesNoIntegralInability.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la cantidad de meses permitidos para ajustes extemporáneos (0-6)
    ''' </summary>
    ''' <returns></returns>
    Public Property AllowedMonthsForExtemporaneousAdjustments As Byte Implements IPayrollSettings.AllowedMonthsForExtemporaneousAdjustments
        Get
            If INDsleAllowedMonthsForExtemporaneousAdjustments.EditValue Is Nothing Then
                Return CByte(0)
            End If
            Return CByte(INDsleAllowedMonthsForExtemporaneousAdjustments.EditValue)
        End Get
        Set(value As Byte)
            INDsleAllowedMonthsForExtemporaneousAdjustments.EditValue = value
        End Set
    End Property

#End Region

#Region "Fields"
    ''' <summary>
    ''' Variable para el formulario de edicion de expresiones
    ''' </summary>
    ''' <remarks></remarks>
    Dim ExpressionEditForm As ExpressionEditorForm

    ''' <summary>
    ''' Variable que establece el diccionario de descripciones de las variables
    ''' </summary>
    Dim descriptions As Dictionary(Of String, String)

    ''' <summary>
    ''' Variable que contiene el nuevo listado de variables del formulario editor de expresiones
    ''' </summary>
    ''' <remarks></remarks>
    Public m_columns As New List(Of IDataColumnInfo)()
#End Region

#Region "Fields"
    ''' <summary>
    ''' Variable para el formulario de edicion de expresiones
    ''' </summary>
    ''' <remarks></remarks>
    Dim ExpressionEditFormContractLiquidation As ExpressionEditorForm

    ''' <summary>
    ''' Variable que establece el diccionario de descripciones de las variables
    ''' </summary>
    Dim descriptionsContractLiquidation As Dictionary(Of String, String)

    ''' <summary>
    ''' Variable que contiene el nuevo listado de variables del formulario editor de expresiones
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly m_columnsContractLiquidation As New List(Of IDataColumnInfo)()
#End Region

#Region "ENTER"
    Private Sub Formulate_Enter(sender As Object, e As EventArgs) Handles INDMemoVacation.Enter,
            INDMemoBonification.Enter,
            INDMemoVacationIncentive.Enter,
            INDMemoVacationalIncrease.Enter,
            INDMemoIncentivePaymentServices.Enter,
            INDMemoIncentivePaymentDecember.Enter,
            INDMeFirstTwoDaysFormulate.Enter
        OpenFormulates(sender)
    End Sub

    Private Sub LiquidationFormula_Enter(sender As Object, e As EventArgs) Handles INDMeSeveranceInterestFormula.Enter,
            INDMemoContractLiquidationYearBonification.Enter,
            INDMemoContractLiquidationTransportValue.Enter,
            INDMemoContractLiquidationFoodValue.Enter,
            INDMemoContractLiquidationVacation.Enter,
            INDMemoContractLiquidationIncentiveVacation.Enter,
            INDMemoContractLiquidationEspecialBonification.Enter,
            INDMemoContractLiquidationIncreaseVacational.Enter,
            INDMemoContractLiquidationServiceIncentive.Enter,
            INDMemoContractLiquidationChristmasIncentive.Enter,
            INDMemoContractLiquidationUnemployment.Enter, INDMePreNoticeFormula.Enter
        OpenFormulatesContractLiquidation(sender)
    End Sub

#End Region


#Region "Functions"
    Private Sub OpenFormulates(ByVal sender As Object)
        FormulateFieldsLoad()

        If FlagSave = False Then
            ExpressionEditForm = New UnboundColumnExpressionEditorForm(Me, Nothing)
            AddHandler CType(ExpressionEditForm.Controls.Item(4), ListBoxControl).SelectedValueChanged, AddressOf selectedChangue
            ExpressionEditForm.StartPosition = FormStartPosition.CenterParent

            ExpressionEditForm.Controls.Item(0).Text = sender.Text
            If ExpressionEditForm.ShowDialog(Me) = DialogResult.OK Then
                sender.Text = ExpressionEditForm.Expression
            End If
        End If
    End Sub


    Private Sub OpenFormulatesContractLiquidation(ByVal sender As Object)
        FormulateFieldsLoadContractLiquidation()

        If FlagSave = False Then
            ExpressionEditFormContractLiquidation = New UnboundColumnExpressionEditorForm(Me, Nothing)
            AddHandler CType(ExpressionEditFormContractLiquidation.Controls.Item(4), ListBoxControl).SelectedValueChanged, AddressOf selectedChangueContractLiquidation
            ExpressionEditFormContractLiquidation.StartPosition = FormStartPosition.CenterParent

            ExpressionEditFormContractLiquidation.Controls.Item(0).Text = sender.Text
            If ExpressionEditFormContractLiquidation.ShowDialog(Me) = DialogResult.OK Then
                sender.Text = ExpressionEditFormContractLiquidation.Expression
            End If
        End If
    End Sub

    Private Sub InitializeTuple()
        'Tipo de adquisición
        ListAdquisitionType = New List(Of Tuple(Of Byte, String))
        ListAdquisitionType.Add(New Tuple(Of Byte, String)(1, "Aporte a Salud Mes Actual"))
        ListAdquisitionType.Add(New Tuple(Of Byte, String)(2, "Aporte a Salud Promedio Año Anterior"))
        INDSlRTFHealthContribution.Properties.DataSource = ListAdquisitionType.ToList

        ListPaidVacation = New List(Of Tuple(Of Byte, String))
        ListPaidVacation.Add(New Tuple(Of Byte, String)(1, "Se paga hasta el último día Hábil (Viernes o sábado)"))
        ListPaidVacation.Add(New Tuple(Of Byte, String)(2, "Se paga hasta el último día previo a ingresar"))
        INDSlPaidVacation.Properties.DataSource = ListPaidVacation.ToList

        ListIBCVacation = New List(Of Tuple(Of Byte, String))
        ListIBCVacation.Add(New Tuple(Of Byte, String)(1, "Se calcula sobre Sueldo Básico"))
        ListIBCVacation.Add(New Tuple(Of Byte, String)(2, "Se calcula sobre el IBC del Mes Anterior"))
        INDSlIBCVacation.Properties.DataSource = ListIBCVacation.ToList

        ListPILAOperators = New List(Of Tuple(Of Byte, String))
        ListPILAOperators.Add(New Tuple(Of Byte, String)(83, "Mi Planilla"))
        ListPILAOperators.Add(New Tuple(Of Byte, String)(84, "Aportes en Línea"))
        ListPILAOperators.Add(New Tuple(Of Byte, String)(86, "Asopagos"))
        ListPILAOperators.Add(New Tuple(Of Byte, String)(87, "Fedecajas (Pila Fácil)"))
        ListPILAOperators.Add(New Tuple(Of Byte, String)(88, "Simple"))
        ListPILAOperators.Add(New Tuple(Of Byte, String)(89, "Arus"))
        INDSlPILAOperators.Properties.DataSource = ListPILAOperators.ToList

        ListTypeCalculationUnemployement = New List(Of Tuple(Of Byte, String))
        ListTypeCalculationUnemployement.Add(New Tuple(Of Byte, String)(1, "Cálculo por Fórmula Interna"))
        ListTypeCalculationUnemployement.Add(New Tuple(Of Byte, String)(2, "Cálculo por suma de Provisiones"))
        INDSlUnemployementCalculated.Properties.DataSource = ListTypeCalculationUnemployement.ToList()

        ListComprobantType = New List(Of Tuple(Of Byte, String))
        ListComprobantType.Add(New Tuple(Of Byte, String)(1, "Cuenta Bancaria"))
        ListComprobantType.Add(New Tuple(Of Byte, String)(2, "Caja Menor"))
        ListComprobantType.Add(New Tuple(Of Byte, String)(3, "Caja Mayor"))
        INDSlComprobantType.Properties.DataSource = ListComprobantType.ToList()
        INDSlComprobantTypeVacation.Properties.DataSource = ListComprobantType.ToList()
        INDSlComprobantTypeLiquidation.Properties.DataSource = ListComprobantType.ToList()

        ListGenerate = New List(Of Tuple(Of Byte, String))
        ListGenerate.Add(New Tuple(Of Byte, String)(2, "Nota Débito"))
        INDSlPaymentMethod.Properties.DataSource = ListGenerate.ToList()
        INDSlPaymentMethodVacation.Properties.DataSource = ListGenerate.ToList()
        INDSlPaymentMethodLiquidation.Properties.DataSource = ListGenerate.ToList()

        ListAccountedBy = New List(Of Tuple(Of Byte, String))
        ListAccountedBy.Add(New Tuple(Of Byte, String)(1, "Empleado"))
        ListAccountedBy.Add(New Tuple(Of Byte, String)(2, "Entidad"))
        INDsleInabilityAccountedBy.Properties.DataSource = ListAccountedBy.ToList()

        ListPayrollDistribution = New List(Of Tuple(Of Byte, String))
        ListPayrollDistribution.Add(New Tuple(Of Byte, String)(1, "Asignada al contrato"))
        ListPayrollDistribution.Add(New Tuple(Of Byte, String)(2, "Costeo por cuadro de turnos"))
        INDslePayrollDistribution.Properties.DataSource = ListPayrollDistribution.ToList()

        _listContributionClass = New List(Of Tuple(Of String, String))
        _listContributionClass.Add(New Tuple(Of String, String)("A", Infrastructure.CrossCutting.Resources.ResourceManager.GetString("ContributionClassA", NAME_MODULE))) 'Aportante con 200 o más cotizantes
        _listContributionClass.Add(New Tuple(Of String, String)("B", Infrastructure.CrossCutting.Resources.ResourceManager.GetString("ContributionClassB", NAME_MODULE))) 'Aportante con menos de 200 cotizantes 
        _listContributionClass.Add(New Tuple(Of String, String)("C", Infrastructure.CrossCutting.Resources.ResourceManager.GetString("ContributionClassC", NAME_MODULE))) 'Aportante Mipyme que se acoge a Ley 590 de 2000 
        _listContributionClass.Add(New Tuple(Of String, String)("D", Infrastructure.CrossCutting.Resources.ResourceManager.GetString("ContributionClassD", NAME_MODULE))) 'Aportante beneficiario del artículo 5° de la Ley 1429 de 2010 
        _listContributionClass.Add(New Tuple(Of String, String)("I", Infrastructure.CrossCutting.Resources.ResourceManager.GetString("ContributionClassI", NAME_MODULE))) 'Independiente
        INDsleContributionClass.Properties.DataSource = _listContributionClass

    End Sub


    ''' <summary>
    ''' Metodo que carga las variables de nómina para la construccion de formulas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FormulateFieldsLoadContractLiquidation()
        Dim decimalType = GetType(Decimal).ToString()
        descriptions = New Dictionary(Of String, String)
        descriptions.Add("Salario Mínimo", "Salario mínimo establecido para el grupo " & vbNewLine & "Tipo de Dato : " & decimalType)
        descriptions.Add("Auxilio Transporte", "Auxilio de transporte establecido para el grupo " & vbNewLine & "Tipo de Dato : " & decimalType)
        descriptions.Add("Sueldo Contrato", "Salario establecido en el contrato" & vbNewLine & "Tipo de Dato : " & decimalType)
        descriptions.Add("Dias Trabajados", "Dias trabajados en el Mes de la Nómina" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Dias Primas Servicios", "Dias de la Prima de Servicios" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Dias Sancion Primas Servicios", "Dias Sancion Primas Servicios" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Dias Licencias No Remueradas Primas Servicios", "Dias Licencias No Remueradas Primas Servicios" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Salario Variable Prima Servicios", "Salario Variable Prima Servicios" & vbNewLine & "Tipo de Dato : " & decimalType)
        descriptions.Add("Dias Primas Diciembre", "Dias de la Prima de Navidad" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Dias Sancion Primas Diciembre", "Dias Sancion Primas Diciembre" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Dias Licencias No Remueradas Primas Diciembre", "Dias Licencias No Remueradas Primas Diciembre" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Salario Variable Prima Diciembre", "Salario Variable Prima Diciembre" & vbNewLine & "Tipo de Dato : " & decimalType)
        descriptions.Add("Dias Cesantias", "Dias de las Cesantias" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Dias Sancion Cesantias", "Dias Sancion Cesantias" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Dias Licencias No Remueradas Cesantias", "Dias Licencias No Remueradas Cesantias" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Salario Variable Cesantias", "Salario Variable Cesantias" & vbNewLine & "Tipo de Dato : " & decimalType)
        descriptions.Add("Dias Vacaciones", "Dias de las Vacaciones" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Dias Vacaciones Retiro", $"Dias de las Vacaciones{vbNewLine}Tipo de Dato : {GetType(Integer)}")
        descriptions.Add("Dias Sancion Vacaciones", "Dias Sancion Vacaciones" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Dias Licencias No Remueradas Vacaciones", "Dias Licencias No Remueradas Vacaciones" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Salario Variable Vacaciones", "Salario Variable Vacaciones" & vbNewLine & "Tipo de Dato : " & decimalType)
        descriptions.Add("IBC Salud", "IBC Salud" & vbNewLine & "Tipo de Dato : " & decimalType)
        descriptions.Add("IBC Pension", "IBC Pension" & vbNewLine & "Tipo de Dato : " & decimalType)
        descriptions.Add("Valor Indemnizacion", "Valor Indemnizacion" & vbNewLine & "Tipo de Dato : " & decimalType)
        descriptions.Add("Valor Acumulado Aguinaldo Retiro", $"Variable para obtener el valor acumulado del Aguinaldo {vbNewLine}Tipo de Dato : {decimalType}")
        descriptions.Add("Valor Promedio Base CIMA", $"Variable para obtener el valor promedio base para CIMA {vbNewLine}Tipo de Dato : {decimalType}")
        descriptions.Add("Fecha Retiro", $"Variable para obtener la fecha de retiro {vbNewLine}Tipo de Dato : {GetType(Date)}")
        descriptions.Add("Fecha Ingreso", "Fecha en que ingreso el empleado" & vbNewLine & "Tipo de Dato : " & GetType(Date).ToString())
        descriptions.Add("Fecha Contratacion", "Fecha en que se contrato el empleado" & vbNewLine & "Tipo de Dato : " & GetType(Date).ToString())

        m_columns = New List(Of IDataColumnInfo)()
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARSALARIM", "Salario Mínimo", GetType(Decimal), m_columns, "Salario Minimo"))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARTRANSPO", "Auxilio Transporte", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARVSUELDO", "Sueldo Contrato", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARDIASTRA", "Dias Trabajados", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARDIAPRIS", "Dias Primas Servicios", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARDIASANP", "Dias Sancion Primas Servicios", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARDIALICP", "Dias Licencias No Remueradas Primas Servicios", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARSALVARP", "Salario Variable Prima Servicios", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARDIAPRIN", "Dias Primas Diciembre", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARDIASANN", "Dias Sancion Primas Diciembre", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARDIALICN", "Dias Licencias No Remueradas Primas Diciembre", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARSALVARD", "Salario Variable Prima Diciembre", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARDIACESA", "Dias Cesantias", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARDIASANC", "Dias Sancion Cesantias", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARDIALICC", "Dias Licencias No Remueradas Cesantias", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARSALVARC", "Salario Variable Cesantias", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARDIASVAC", "Dias Vacaciones", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARDIVACCM", "Dias Vacaciones Retiro", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARDIASANV", "Dias Sancion Vacaciones", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARDIALICV", "Dias Licencias No Remueradas Vacaciones", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARSALVARV", "Salario Variable Vacaciones", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARIBCSALU", "IBC Salud", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARIBCPENS", "IBC Pension", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARINDEMNI", "Valor Indemnizacion", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARVACAGRE", "Valor Acumulado Aguinaldo Retiro", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARVAPRBCM", "Valor Promedio Base CIMA", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARFECHRET", "Fecha Retiro", GetType(Date), m_columns, [String].Empty))
        m_columns.Add(New FrmConcepts.ColumnInfo("fieldVARFECHING", "Fecha Ingreso", GetType(Date), m_columns, [String].Empty))
        m_columns.Add(New FrmConcepts.ColumnInfo("fieldVARFECHCON", "Fecha Contratacion", GetType(Date), m_columns, [String].Empty))
    End Sub

    Private Sub FormulateFieldsLoad()
        Dim decimalType = GetType(Decimal).ToString()
        descriptions = New Dictionary(Of String, String)
        descriptions.Add("Salario Mínimo", "Salario mínimo establecido para el grupo " & vbNewLine & "Tipo de Dato : " & decimalType)
        descriptions.Add("Auxilio Transporte", "Auxilio de transporte establecido para el grupo " & vbNewLine & "Tipo de Dato : " & decimalType)
        descriptions.Add("Sueldo Contrato", "Salario establecido en el contrato" & vbNewLine & "Tipo de Dato : " & decimalType)
        descriptions.Add("Días Vacaciones", "Días de Vacaciones" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Gastos de Representación", "Gastos de Representación" & vbNewLine & "Tipo de Dato : " & decimalType)
        descriptions.Add("Bonificacion Año Servicio", "Bonificacion Año Servicio" & vbNewLine & "Tipo de Dato : " & decimalType)
        descriptions.Add("Valor Prima Servicio", "Valor Prima Servicio" & vbNewLine & "Tipo de Dato : " & decimalType)
        descriptions.Add("Valor Base Vacaciones", "Valor Base Vacaciones" & vbNewLine & "Tipo de Dato : " & decimalType)
        descriptions.Add("Dias del Periodo de Primas", "Dias del Periodo de Primas" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Salario Variable Primas", "Salario Variable Primas" & vbNewLine & "Tipo de Dato : " & decimalType)
        descriptions.Add("Sueldo Promedio", "Sueldo Promedio" & vbNewLine & "Tipo de Dato : " & decimalType)
        descriptions.Add("Dias de Sanción", "Dias de Sanción" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Dias de Licencias No Remuneradas", "Dias de Licencias No Remuneradas" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Auxilio Transporte Promedio", "Auxilio de transporte establecido para el grupo " & vbNewLine & "Tipo de Dato : " & decimalType)
        descriptions.Add("Meses Prima", "Meses Prima" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Dias Pagados Vacaciones", "Dias Pagados Vacaciones" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Valor Prima Vacaciones", "Valor Prima Vacaciones" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Fecha Ingreso", "Fecha en que ingreso el empleado" & vbNewLine & "Tipo de Dato : " & GetType(Date).ToString())
        descriptions.Add("Fecha Contratacion", "Fecha en que se contrato el empleado" & vbNewLine & "Tipo de Dato : " & GetType(Date).ToString())
        descriptions.Add("Meses Laborados Prima", "Variable que calcula los meses laborados por el empleado teninedo encuenta las licencias no remuneradas" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Meses Bonificacion Año Servicio", "Variable que calcula los meses laborados por el empleado para la Bonificación por Año de Servicio (Liq. Contrato)" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Horas Diarias Laboradas", "Variable Horas Diarias Laboradas" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Dias Trabajados Liq Contrato", "Variable Dias Trabajados Liquidación de Contrato" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Meses Vacaciones Liq Contrato", "Variable Meses Vacaciones Liquidación de Contrato" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Dias Vacaciones x Pagar Liq Contrato", "Variable Dias a pagar de Vacaiones en Liquidación de Contrato" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Meses Primas Liq Contrato", "Variable Dias a pagar de Vacaiones en Liquidación de Contrato" & vbNewLine & "Tipo de Dato : " & GetType(Integer).ToString())
        descriptions.Add("Bonificacion Año Servicio Liq Contrato", "Variable para obtener el valor de la Bonificación por Año de Servicio de Liq. de Contrato" & vbNewLine & "Tipo de Dato : " & decimalType)
        descriptions.Add("Vacaciones Compensadas", "Variable para saber si las vacaciones van a ser compensadas o disfrutadas (Vacaciones)" & vbNewLine & "Tipo de Dato : " & GetType(Boolean).ToString())
        descriptions.Add("Promedio Recargos Nocturnos Normales", "Variable para obtener el valor promedio de los Recargos Nocturnos Normales (Únicamente para Vacaciones)" & vbNewLine & "Tipo de Dato : " & decimalType)
        descriptions.Add("Recargos Promedio Nocturnos N", "Variable para obtener el valor promedio de los Recargos Nocturnos Normales a partir del mes actual (Únicamente para Vacaciones)" & vbNewLine & "Tipo de Dato : " & decimalType)
        descriptions.Add("Promedio Recargos Nocturnos Festivos", "Variable para obtener el valor promedio de los Recargos Nocturnos Festivos (Únicamente para Vacaciones)" & vbNewLine & "Tipo de Dato : " & decimalType)
        descriptions.Add("Promedio Recargos Dominicales", "Variable para obtener el valor promedio de los Recargos Dominicales (Únicamente para Vacaciones)" & vbNewLine & "Tipo de Dato : " & decimalType)
        descriptions.Add("Promedio Horas Extras", "Variable para obtener el valor promedio de las Horas Extras (Únicamente para Vacaciones)" & vbNewLine & "Tipo de Dato : " & decimalType)
        descriptions.Add("Sueldo Promedio Contrato", "Variable para obtener el valor promedio de los Sueldos del Empleado (Únicamente para Vacaciones)" & vbNewLine & "Tipo de Dato : " & decimalType)
        descriptions.Add("Promedio Bonificaciones Salarias", "Variable para obtener el valor promedio de las Bonificaciones Salariales (Únicamente para Vacaciones)" & vbNewLine & "Tipo de Dato : " & decimalType)
        descriptions.Add("Promedio Salario Variable Vacaciones", "Variable para obtener el valor promedio del Salario Variable Vacaciones" & vbNewLine & "Tipo de Dato : " & decimalType)
        descriptions.Add("Valor Acumulado Base Aguinaldo", $"Variable para obtener el valor acumulado base para el Aguinaldo {vbNewLine}Tipo de Dato : {decimalType}")

        m_columns = New List(Of IDataColumnInfo)()
        m_columns.Add(New FrmConcepts.ColumnInfo("fieldVARSALARIM", "Salario Mínimo", GetType(Decimal), m_columns, "Salario Minimo"))
        m_columns.Add(New FrmConcepts.ColumnInfo("fieldVARTRANSPO", "Auxilio Transporte", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New FrmConcepts.ColumnInfo("fieldVARVSUELDO", "Sueldo Contrato", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New FrmConcepts.ColumnInfo("fieldVARDIASVAC", "Días Vacaciones", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New FrmConcepts.ColumnInfo("fieldVARGASTREP", "Gastos de Representación", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New FrmConcepts.ColumnInfo("fieldVARBONANOS", "Bonificacion Año Servicio", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New FrmConcepts.ColumnInfo("fieldVARPRIMSER", "Valor Prima Servicio", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New FrmConcepts.ColumnInfo("fieldVARBASEVAC", "Valor Base Vacaciones", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New FrmConcepts.ColumnInfo("fieldVARDIAPERP", "Dias del Periodo de Primas", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New FrmConcepts.ColumnInfo("fieldVARSALVARP", "Salario Variable Primas", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New FrmConcepts.ColumnInfo("fieldVARAUXIALM", "Auxilio Alimentos", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New FrmConcepts.ColumnInfo("fieldVARSUELPRO", "Sueldo Promedio", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New FrmConcepts.ColumnInfo("fieldVARDIASANC", "Dias de Sanción", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New FrmConcepts.ColumnInfo("fieldVARDIASLIC", "Dias de Licencias No Remuneradas", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New FrmConcepts.ColumnInfo("fieldVARTRANSPR", "Auxilio Transporte Promedio", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New FrmConcepts.ColumnInfo("fieldVARMESPRIM", "Meses Prima", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New FrmConcepts.ColumnInfo("fieldVARVACPAGD", "Dias Pagados Vacaciones", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New FrmConcepts.ColumnInfo("fieldVARPRIMVAC", "Valor Prima Vacaciones", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New FrmConcepts.ColumnInfo("fieldVARFECHING", "Fecha Ingreso", GetType(Date), m_columns, [String].Empty))
        m_columns.Add(New FrmConcepts.ColumnInfo("fieldVARFECHCON", "Fecha Contratacion", GetType(Date), m_columns, [String].Empty))
        m_columns.Add(New FrmConcepts.ColumnInfo("fieldVARMONTHPR", "Meses Laborados Prima", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New FrmConcepts.ColumnInfo("fieldVARMONTHBS", "Meses Bonificacion Año Servicio", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New FrmConcepts.ColumnInfo("fieldVARHORASDI", "Horas Diarias Laboradas", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New FrmConcepts.ColumnInfo("fieldVARDIASLAB", "Dias Trabajados Liq Contrato", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New FrmConcepts.ColumnInfo("fieldVARMESVACL", "Meses Vacaciones Liq Contrato", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New FrmConcepts.ColumnInfo("fieldVARDIAVACL", "Dias Vacaciones x Pagar Liq Contrato", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New FrmConcepts.ColumnInfo("fieldVARMESPRIL", "Meses Primas Liq Contrato", GetType(Integer), m_columns, [String].Empty))
        m_columns.Add(New FrmConcepts.ColumnInfo("fieldVARBONIFLC", "Bonificacion Año Servicio Liq Contrato", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New FrmConcepts.ColumnInfo("fieldVARVACACOM", "Vacaciones Compensadas", GetType(Boolean), m_columns, [String].Empty))
        m_columns.Add(New FrmConcepts.ColumnInfo("fieldVARACURECN", "Promedio Recargos Nocturnos Normales", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New FrmConcepts.ColumnInfo("fieldVARACURECN", "Recargos Promedio Nocturnos N", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New FrmConcepts.ColumnInfo("fieldVARACURECF", "Promedio Recargos Nocturnos Festivos", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New FrmConcepts.ColumnInfo("fieldVARACUREDO", "Promedio Recargos Dominicales", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New FrmConcepts.ColumnInfo("fieldVARACUHOEX", "Promedio Horas Extras", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New FrmConcepts.ColumnInfo("fieldVARACUSUEL", "Sueldo Promedio Contrato", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New FrmConcepts.ColumnInfo("fieldVARSALVARP", "Promedio Salario Variable Vacaciones", GetType(Decimal), m_columns, [String].Empty))
        m_columns.Add(New Presentation.Payroll.FrmConcepts.ColumnInfo("fieldVARVAACBAA", "Valor Acumulado Base Aguinaldo", GetType(Decimal), m_columns, [String].Empty))
    End Sub

    ''' <summary>
    ''' Loads the controls.
    ''' </summary>
    Private Async Sub LoadControls()
        Me.BarraBotones.StatusRecordVisible = True
        Me.BarraBotones.StatusRecord = True
        AsyncLoader(True)
        Using Model As New MPayrollSettings(CStr(Me.Tag))
            PayrollSettings = Await Model.GetParameters()
        End Using
        If PayrollSettings IsNot Nothing Then
            If PayrollSettings.Id > 0 Then
                Using ModelRecord As New MBlockRecordAndSequensePayroll(CStr(Me.Tag))
                    Dim result = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(PayrollSettings.Id))

                    With PayrollSettings

                        CreeTax = .CreeTax
                        VacationFormulates = .VacationFormulates
                        BonificationVacationFormulates = .BonificationVacationFormulates
                        VacationalIncreaseFormulates = .VacationalIncreaseFormulates
                        VacationIncentiveFormulates = .VacationIncentiveFormulates

                        If .NameThirdPartyPayrollSettings IsNot Nothing Then
                            INDSLUpThirdPartyId.DisplayNullText = .NameThirdPartyPayrollSettings
                        End If

                        ServicesIncentivePaymentFormulates = .ServicesIncentivePaymentFormulates
                        ChristmasIncentivePaymentFormulates = .ChristmasIncentivePayment
                        InitialDateServicesIncentivePayment = .InitialDateServicesIncentivePayment
                        EndDateServicesIncentivePayment = .EndDateServicesIncentivePayment
                        InitialDateChristmasIncentivePayment = .InitialDateChristmasIncentivePayment
                        EndDateChristmasIncentivePayment = .EndDateChristmasIncentivePayment
                        INDSLUpThirdPartyId.EditValue = .PayrollChiefThirdPartyId
                        HealthContributionRTF = .RtfHealthContribution
                        WeekendVacationPaid = .WeekendVacationPaid
                        ServicesIncentivePaymentConceptId = .ServicesIncentivePaymentConceptId
                        ChristmasIncentivePaymentConceptId = .ChristmasIncentivePaymentConceptId
                        NoveltyMainAccountId = .NoveltyMainAccountId
                        HolidayWithoutAnticipateIBC = .HolidayWithoutAnticipateIBC
                        HealthPensionIBCVacation = .HealthPensionIBCVacation
                        ContractLiquidationYearBonification = .ContractLiquidationYearBonification
                        ContractLiquidationTransportValue = .ContractLiquidationTransportValue
                        ContractLiquidationFoodValue = .ContractLiquidationFoodValue
                        ContractLiquidationVacation = .ContractLiquidationVacation
                        ContractLiquidationIncentiveVacation = .ContractLiquidationIncentiveVacation
                        ContractLiquidationEspecialBonification = .ContractLiquidationEspecialBonification
                        ContractLiquidationIncreaseVacational = .ContractLiquidationIncreaseVacational
                        ContractLiquidationServiceIncentive = .ContractLiquidationServiceIncentive
                        ContractLiquidationChristmasIncentive = .ContractLiquidationChristmasIncentive
                        ContractLiquidationUnemployment = .ContractLiquidationUnemployment
                        PILAOperators = .PILAOperators
                        IdRetentionConcepts = .IdRetentionConcepts
                        UnemploymentTypeCalculated = .UnemploymentTypeCalculated
                        TransportValueLastYear = .TransportHealthValueLastYear
                        MinimunLegalSalaryLastYear = .MinimunLegalSalaryLastYear
                        IdConceptUnemployment = .IdConceptUnemployment
                        IdConceptInterestUnemployment = .IdConceptInterestUnemployment
                        IdAccountReceivableConcept = .IdAccountReceivableConcept

                        INDGcNoveltyConceptAdjust.DataSource = PayrollSettings.ConceptNoveltyAdjust.ToList()
                        IdCashRegister = .IdCashRegister
                        IdEntityBankAccount = .IdEntityBankAccount
                        ExpenseType = .ExpenseType
                        INDSlPaymentMethod.EditValue = .PaymentMethod

                        INDSlVacationExpenseConcept.EditValue = .IdExpenseConceptsVacation
                        IdCashRegisterVacation = .IdCashRegisterVacation
                        IdEntityBankAccountVacation = .IdEntityBankAccountVacation
                        ExpenseTypeVacation = .ExpenseTypeVacation
                        INDSlPaymentMethodVacation.EditValue = .PaymentMethodVacation

                        INDSlExpenseConceptLiquidation.EditValue = .IdExpenseConceptsLiquidation
                        IdCashRegisterLiquidation = .IdCashRegisterLiquidation
                        IdEntityBankAccountLiquidation = .IdEntityBankAccountLiquidation
                        ExpenseTypeLiquidation = .ExpenseTypeLiquidation
                        INDSlPaymentMethodLiquidation.EditValue = .PaymentMethodLiquidation

                        INDSlConceptVacation.EditValue = .IdVacationConcept
                        InabilityAccountedBy = .InabilityAccountedBy
                        PayrollDistribution = .PayrollDistribution

                        GenerateVoucherTransactionVacation = .GenerateVoucherTransactionVacation
                        GenerateVoucherTransactionContractLiquidation = .GenerateVoucherTransactionContractLiquidation

                        CtrYesNoRetentionFraction.EditValue = .RetentionFraction
                        INDSlYesNoLiquidarContratoFormula.EditValue = .LiquidateContractByFormulate

                        INDSpDelayMinutes.EditValue = .IngressDelayMinutes

                        INDSlNoteConcepts.EditValue = .NoteConceptsId
                        INDSlPortfolioNoteConcept.EditValue = .PortfolioNoteConceptId
                        INDSlCashFlowConcept.EditValue = .CashFlowConceptId

                        ContributionClass = .ContributionClass
                        LiquidatesSeverancePayments = .LiquidatesSeverancePayments

                        INDSleSeveranceConcept.EditValue = .SeveranceConceptId
                        INDSleSeveranceInterestConcept.EditValue = .SeveranceInterestConceptId
                        INDSlePreNoticeSettlementConcept.EditValue = .PreNoticeSettlementConceptId
                        INDSleBonusSettlementConcept.EditValue = .BonusSettlementConceptId
                        INDSleVacationSettlementConcept.EditValue = .VacationSettlementConceptId
                        INDMeSeveranceInterestFormula.EditValue = .SeveranceInterestFormula
                        INDMePreNoticeFormula.EditValue = .PreNoticeFormula
                        INDGleRetentionBiweeklyDiscount.EditValue = .WithholdingBiweeklyDiscount
                        INDSleWithholdingTaxRefundConcept.EditValue = .WithholdingTaxRefundConceptId
                        INDRgHandlesLiquidationFirstTwoDays.EditValue = .HandlesLiquidationFirstTwoDays
                        INDMeFirstTwoDaysFormulate.EditValue = .FirstTwoDaysFormulate
                        IntegralSalaryInabilityConcept = .ConceptBenefitFactortId
                        IntegralSalaryInability = If(.IsFullSalaryBenefitFactor Is Nothing, False, .IsFullSalaryBenefitFactor)

                        ' NUEVO: Cargar campo para ajustes extemporáneos
                        AllowedMonthsForExtemporaneousAdjustments = .AllowedMonthsForExtemporaneousAdjustments

						If .CurrencyId > 0 Then
							CurrencyId = .CurrencyId
						End If
						INDsleCurrency.Properties.ReadOnly = .CurrencyFieldEnabled
					End With

                    If result.Id = 0 Then

                        Dim state = New Domain.Base.Entities.ObjectChangeTracker
                        state.State = Domain.Base.Entities.ObjectState.Added
                        record = New Domain.Entities.BlockRecordPayroll With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .FormId = Me.Tag, .CodUser = Me.indigo.UserIndigo, .RecordId = PayrollSettings.Id}
                        Dim operation = Await ModelRecord.SaveBlockRecord(record)
                        record = operation.ObjectEmbbeded
                    Else
                        record = result
                        Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                        Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                    End If

                    Me.BarraBotones.SetDocuments(PayrollSettings.Id, Me.Tag.ToString(), Nothing, GetType(PayrollSettings).Name)


                End Using

                If LiquidatesSeverancePayments = False Then
                    INDLciUnemploymentTypeCalculated.HideControl()
                    INDLciUnemploymentConcept.HideControl()
                    INDLciInterestUnemploymentConcept.HideControl()
                End If
            End If

            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
        Else
            PayrollSettings = New PayrollSettings
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        End If
        ShowBasicInformationCR()
        FlagSave = False
        GetOfficialCurrency()
        AsyncLoader(False)
        ActionsOnControls = True
    End Sub

    ''' <summary>
    ''' Propiedad que establece el estado de los controles del formulario
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls
        Set(value)
            INDSleCreeTax.Enabled = value
            INDMemoVacation.Enabled = value
            INDMemoBonification.Enabled = value
            INDMemoVacationIncentive.Enabled = value
            INDMemoVacationalIncrease.Enabled = value
            INDsleHolidayWithoutAnticipateIBC.Enabled = value
            INDsleContributionClass.Enabled = value
        End Set
    End Property

    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        FlagSave = True

        With PayrollSettings
            .CreeTax = CreeTax
            .BonificationVacationFormulates = IIf(BonificationVacationFormulates = String.Empty, "0", BonificationVacationFormulates)
            .VacationalIncreaseFormulates = IIf(VacationalIncreaseFormulates = String.Empty, "0", VacationalIncreaseFormulates)
            .VacationIncentiveFormulates = IIf(VacationIncentiveFormulates = String.Empty, "0", VacationIncentiveFormulates)
            .VacationFormulates = IIf(VacationFormulates = String.Empty, "0", VacationFormulates)
            .PayrollChiefThirdPartyId = ThirdPartyId
            .ChristmasIncentivePayment = IIf(ChristmasIncentivePaymentFormulates = String.Empty, "0", ChristmasIncentivePaymentFormulates)
            .ServicesIncentivePaymentFormulates = IIf(ServicesIncentivePaymentFormulates = String.Empty, "0", ServicesIncentivePaymentFormulates)
            .InitialDateServicesIncentivePayment = InitialDateServicesIncentivePayment
            .EndDateServicesIncentivePayment = EndDateServicesIncentivePayment
            .InitialDateChristmasIncentivePayment = InitialDateChristmasIncentivePayment
            .EndDateChristmasIncentivePayment = EndDateChristmasIncentivePayment
            .RtfHealthContribution = HealthContributionRTF
            .WeekendVacationPaid = WeekendVacationPaid
            .ChristmasIncentivePaymentConceptId = ChristmasIncentivePaymentConceptId
            .ServicesIncentivePaymentConceptId = ServicesIncentivePaymentConceptId
            .NoveltyMainAccountId = NoveltyMainAccountId
            .HolidayWithoutAnticipateIBC = HolidayWithoutAnticipateIBC
            .HealthPensionIBCVacation = HealthPensionIBCVacation
            .ContractLiquidationYearBonification = IIf(ContractLiquidationYearBonification = String.Empty, "0", ContractLiquidationYearBonification)
            .ContractLiquidationTransportValue = IIf(ContractLiquidationTransportValue = String.Empty, "0", ContractLiquidationTransportValue)
            .ContractLiquidationFoodValue = IIf(ContractLiquidationFoodValue = String.Empty, "0", ContractLiquidationFoodValue)
            .ContractLiquidationVacation = IIf(ContractLiquidationVacation = String.Empty, "0", ContractLiquidationVacation)
            .ContractLiquidationIncentiveVacation = IIf(ContractLiquidationIncentiveVacation = String.Empty, "0", ContractLiquidationIncentiveVacation)
            .ContractLiquidationEspecialBonification = IIf(ContractLiquidationEspecialBonification = String.Empty, "0", ContractLiquidationEspecialBonification)
            .ContractLiquidationIncreaseVacational = IIf(ContractLiquidationIncreaseVacational = String.Empty, "0", ContractLiquidationIncreaseVacational)
            .ContractLiquidationServiceIncentive = IIf(ContractLiquidationServiceIncentive = String.Empty, "0", ContractLiquidationServiceIncentive)
            .ContractLiquidationChristmasIncentive = IIf(ContractLiquidationChristmasIncentive = String.Empty, "0", ContractLiquidationChristmasIncentive)
            .ContractLiquidationUnemployment = IIf(ContractLiquidationUnemployment = String.Empty, "0", ContractLiquidationUnemployment)
            .PILAOperators = PILAOperators
            .UnemploymentTypeCalculated = UnemploymentTypeCalculated
            .IdRetentionConcepts = IdRetentionConcepts
            .TransportHealthValueLastYear = TransportValueLastYear
            .MinimunLegalSalaryLastYear = MinimunLegalSalaryLastYear
            .IdConceptUnemployment = IdConceptUnemployment
            .IdConceptInterestUnemployment = IdConceptInterestUnemployment
            .IdAccountReceivableConcept = IdAccountReceivableConcept
            .IdCashRegister = IdCashRegister
            .IdEntityBankAccount = IdEntityBankAccount
            .ExpenseType = ExpenseType
            .PaymentMethod = CType(INDSlPaymentMethod.EditValue, Byte)

            .IdExpenseConceptsVacation = INDSlVacationExpenseConcept.EditValue
            .IdCashRegisterVacation = IdCashRegisterVacation
            .IdEntityBankAccountVacation = IdEntityBankAccountVacation
            .ExpenseTypeVacation = ExpenseTypeVacation
            .PaymentMethodVacation = CType(INDSlPaymentMethodVacation.EditValue, Byte)

            .IdExpenseConceptsLiquidation = INDSlExpenseConceptLiquidation.EditValue
            .IdCashRegisterLiquidation = IdCashRegisterLiquidation
            .IdEntityBankAccountLiquidation = IdEntityBankAccountLiquidation
            .ExpenseTypeLiquidation = ExpenseTypeLiquidation
            .PaymentMethodLiquidation = CType(INDSlPaymentMethodLiquidation.EditValue, Byte)

            .IdVacationConcept = INDSlConceptVacation.EditValue
            .InabilityAccountedBy = InabilityAccountedBy
            .PayrollDistribution = PayrollDistribution

            .GenerateVoucherTransactionVacation = GenerateVoucherTransactionVacation
            .GenerateVoucherTransactionContractLiquidation = GenerateVoucherTransactionContractLiquidation

            .RetentionFraction = CtrYesNoRetentionFraction.EditValue

            .LiquidateContractByFormulate = INDSlYesNoLiquidarContratoFormula.EditValue

            .IngressDelayMinutes = INDSpDelayMinutes.EditValue

            .NoteConceptsId = INDSlNoteConcepts.EditValue
            .PortfolioNoteConceptId = INDSlPortfolioNoteConcept.EditValue
            .CashFlowConceptId = INDSlCashFlowConcept.EditValue

            .ContributionClass = ContributionClass

            .LiquidatesSeverancePayments = LiquidatesSeverancePayments

            .CurrencyId = CurrencyId

            .SeveranceConceptId = INDSleSeveranceConcept.EditValue
            .SeveranceInterestConceptId = INDSleSeveranceInterestConcept.EditValue
            .PreNoticeSettlementConceptId = INDSlePreNoticeSettlementConcept.EditValue
            .BonusSettlementConceptId = INDSleBonusSettlementConcept.EditValue
            .VacationSettlementConceptId = INDSleVacationSettlementConcept.EditValue
            .SeveranceInterestFormula = IIf(INDMeSeveranceInterestFormula.EditValue = "", "0", INDMeSeveranceInterestFormula.EditValue)
            .PreNoticeFormula = IIf(INDMePreNoticeFormula.EditValue = "", "0", INDMePreNoticeFormula.EditValue)
            .WithholdingBiweeklyDiscount = INDGleRetentionBiweeklyDiscount.EditValue
            .WithholdingTaxRefundConceptId = INDSleWithholdingTaxRefundConcept.EditValue
            .HandlesLiquidationFirstTwoDays = INDRgHandlesLiquidationFirstTwoDays.EditValue
            .FirstTwoDaysFormulate = INDMeFirstTwoDaysFormulate.EditValue
            .IsFullSalaryBenefitFactor = IntegralSalaryInability
            .ConceptBenefitFactortId = IntegralSalaryInabilityConcept

            ' NUEVO: Guardar campo para ajustes extemporáneos
            ' Validar que esté en el rango permitido (0-6)
            Dim allowedMonths As Byte = AllowedMonthsForExtemporaneousAdjustments
            If allowedMonths > 6 Then
                allowedMonths = 6
            End If
            .AllowedMonthsForExtemporaneousAdjustments = allowedMonths

            If .Id > 0 Then
                .ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified
            End If
        End With
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using ModelRecord As New MBlockRecordAndSequensePayroll(CStr(Me.Tag))
                Await ModelRecord.DeleteBlockRecord(record)
                record = Nothing
            End Using

        End If
    End Sub

    '''' <summary>
    '''' Metodo que muestra/oculta controles según la cultura
    '''' </summary>
    Private Sub ShowBasicInformationCR()
        If Me.indigo.Culture.Name = "es-CR" Then
            INDLciCreeTax.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciPILAOperators.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciContributionClass.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            CreeTax = 0
            ContributionClass = String.Empty
            PILAOperators = Nothing
            INDlciLiquidatesSeverancePayments.HideControl()
            INDLciUnemploymentTypeCalculated.HideControl()
            INDLciUnemploymentConcept.HideControl()
            INDLciInterestUnemploymentConcept.HideControl()
        Else
            INDLciCreeTax.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciPILAOperators.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciContributionClass.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub

    ''' <summary>
    ''' Metodo para limpiar los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        CreeTax = Nothing
        VacationalIncreaseFormulates = Nothing
        VacationFormulates = Nothing
        VacationIncentiveFormulates = Nothing
        BonificationVacationFormulates = Nothing
        HolidayWithoutAnticipateIBC = Nothing
        ContractLiquidationYearBonification = Nothing
        ContractLiquidationTransportValue = Nothing
        ContractLiquidationFoodValue = Nothing
        ContractLiquidationVacation = Nothing
        ContractLiquidationIncentiveVacation = Nothing
        ContractLiquidationEspecialBonification = Nothing
        ContractLiquidationIncreaseVacational = Nothing
        ContractLiquidationServiceIncentive = Nothing
        ContractLiquidationChristmasIncentive = Nothing
        ContractLiquidationUnemployment = Nothing
        InabilityAccountedBy = 1
        PayrollDistribution = Nothing
        INDslePayrollDistribution.Properties.NullText = String.Empty
        ContributionClass = "A"
        LiquidatesSeverancePayments = Nothing
        INDSleSeveranceConcept.EditValue = Nothing
        INDSleSeveranceInterestConcept.EditValue = Nothing
        INDSlePreNoticeSettlementConcept.EditValue = Nothing
        INDSleBonusSettlementConcept.EditValue = Nothing
        INDSleVacationSettlementConcept.EditValue = Nothing
        INDMeSeveranceInterestFormula.EditValue = Nothing
        INDMePreNoticeFormula.EditValue = Nothing
        INDGleRetentionBiweeklyDiscount.EditValue = False
        INDSleWithholdingTaxRefundConcept.EditValue = Nothing
        INDRgHandlesLiquidationFirstTwoDays.EditValue = False
        IntegralSalaryInability = False
        ' NUEVO: Limpiar campo para ajustes extemporáneos
        AllowedMonthsForExtemporaneousAdjustments = 0
    End Sub


#End Region

    ''' <summary>
    ''' Evento que se dispara cuando cambia el item seleccionado de la lista de campos, para establecer la correcta descripción!
    ''' </summary>
    Private Sub selectedChangue(sender As Object, e As EventArgs)
        Dim lista As ListBoxControl = CType(sender, ListBoxControl)
        If lista.ItemCount > 0 Then
            Dim item As String = lista.SelectedItem.ToString().Replace("[", "").Replace("]", "")
            If descriptions.ContainsKey(item) Then
                ExpressionEditForm.Controls.Item(6).Text = descriptions(item)
            End If
        End If
    End Sub

    Private Sub selectedChangueContractLiquidation(sender As Object, e As EventArgs)
        Dim lista As ListBoxControl = CType(sender, ListBoxControl)
        If lista.ItemCount > 0 Then
            Dim item As String = lista.SelectedItem.ToString().Replace("[", "").Replace("]", "")
            If descriptions.ContainsKey(item) Then
                ExpressionEditFormContractLiquidation.Controls.Item(6).Text = descriptions(item)
            End If
        End If
    End Sub

#Region "ICrud"
    Public Sub Buscar() Implements Base.ICrudBase.Buscar

    End Sub

    Public Sub Deshacer() Implements Base.ICrudBase.Deshacer
        CleanControls()
        DeleteBlockedRecord()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    Public Sub Eliminar() Implements Base.ICrudBase.Eliminar

    End Sub

    Public Async Sub Guardar() Implements Base.ICrudBase.Guardar
        If ValidateControls() = False Then
            Exit Sub
        End If
        AssigningValues()
        Using model As New MPayrollSettings(CStr(Me.Tag))
            AsyncLoader(True)

            If PayrollSettings.ChangeTracker.State = ObjectState.Unchanged Then
                If PayrollSettings.ConceptNoveltyAdjust.Where(Function(x) x.ChangeTracker.State = ObjectState.Added _
                                                     Or x.ChangeTracker.State = ObjectState.Modified Or x.ChangeTracker.State = ObjectState.Deleted) IsNot Nothing Then
                    PayrollSettings.MarkAsModified()
                End If
            End If

            Dim Result = Await model.SaveParameters(PayrollSettings)
            AsyncLoader(False)
            If Result.StateResult = True Then

                FlagSave = True

                If PayrollSettings.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesGuardado)
                ElseIf PayrollSettings.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
                End If
                Me.PayrollSettings = Result.ObjectEmbbeded
                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                AsyncLoader(False)

                DeleteBlockedRecord()
                LoadControls()

                Me.BarraBotones.CleanAuditBasic()
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
            Else
                If Result.MessageResult(0) = ErrorConcurrencia Then
                    Mensaje(EeventViewerImages.MensajeError) = "Error de Concurrencia"
                Else
                    Mensaje(EeventViewerImages.MensajeError) = "Error Desconocido"
                End If
            End If
        End Using
        FlagSave = False
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.ICrudBase.LogicaBotonActualizar

    End Sub

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

    Public Sub Nuevo() Implements Base.ICrudBase.Nuevo

    End Sub

    Public Sub OpenSearch() Implements Base.ICrudBase.OpenSearch

    End Sub
#End Region


#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        PayrollSettings = Nothing
        FlagSave = Nothing
        Model = Nothing
        listadq = Nothing
        ListPaidVacation = Nothing
        ListIBCVacation = Nothing
        ListPILAOperators = Nothing
        ListPayrollDistribution = Nothing
        ExpressionEditForm = Nothing
        descriptions = Nothing
        INDSlNoteConcepts.EditValue = Nothing
    End Sub


    Private Sub FrmSettingsPayroll_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDLcPayrollSettings, True)
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        'Presenter = New PSettingFixedAsset(Me)
        Deshacer()
        'FormulateFieldsLoad()
        InitializeTuple()
        LoadXpoConceptDecemberIncentive()
        LoadXpoConceptServiceIncentive()
        LoadXpoConceptInterestUnemployment()
        LoadXpoConceptUnemployment()
        LoadAccountNovelty()
        LoadConceptRetention()
        LoadAccountReceivableConcept()
        LoadAllowedMonthsForExtemporaneousAdjustments()
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDGvNoveltyConceptAdjust, ListActions)
        presenter = New PPayrollSettings(Me)
        InitializeCurrency()
        AsyncLoader(True)
        presenter.ConceptsDatasource()
        presenter.InitializeEntityBankAccount()
        presenter.InitializeEntityBankAccountVacation()
        presenter.InitializeEntityBankAccountLiquidation()
        presenter.InitializeExpenseConceptLiquidation()
        presenter.InitializeExpenseConceptVacation()
        presenter.InitializeNoteConceptsDatasource()
        presenter.InitializePortfolioNoteConceptDatasource()
        presenter.InitializeCashFlowConceptDatasource()
        initializeLiquidationConceptDatasources()

        InabilityAccountedBy = 1
        AsyncLoader(False)
		LoadControls()
		SetCurrencyFormat(presenter.LoadPayrollSettings().CurrencyId.Abbreviation)
		'Ocultamos controles para Nómina de Entidad Privada
		If indigo.IndigoCompanyType = 1 Then
            HidePublicControls()
        End If

        'Ocultamos controles para Nóminas NO Integradas con VIE
        If indigo.IndigoPayrollIntegration = 2 Then
            HideIntegrationControls()
        End If

    End Sub

    Private Sub initializeLiquidationConceptDatasources()
        INDSleSeveranceConcept.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).PayrollService.GetConcept()
        INDSleSeveranceInterestConcept.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).PayrollService.GetConcept()
        INDSlePreNoticeSettlementConcept.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).PayrollService.GetConcept()
        INDSleBonusSettlementConcept.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).PayrollService.GetConcept()
        INDSleVacationSettlementConcept.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).PayrollService.GetConcept()
        INDSleWithholdingTaxRefundConcept.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).PayrollService.GetConcept()
    End Sub
#End Region

    Private Sub HidePublicControls()
        INDlciBonification.HideControl()
        INDLciVacationIncentivePayment.HideControl()
        INDLciVacationalIcrease.HideControl()
        INDLciContractLiquidationYearBonification.HideControl()
        INDLciContractLiquidationFoodValue.HideControl()
        INDLciContractLiquidationIncentiveVacation.HideControl()
        INDLciContractLiquidationEspecialBonification.HideControl()
        INDLciContractLiquidationIncreaseVacational.HideControl()
    End Sub

    Private Sub HideIntegrationControls()
        INDLcgContractLiquidationVoucherTransaction.HideControl()
        INDLcgPayrollDistribution.HideControl()
        INDLcgVacationVoucherTransaction.HideControl()
    End Sub

#Region "Barra Botones"

    ''' <summary>
    ''' Barra Botones: Activa o desactiva el estado
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive

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

#Region "Formulates"

    Class ColumnInfo
        Implements IDataColumnInfo
        ReadOnly m_name As String
        ReadOnly m_fieldName As String
        ReadOnly m_fieldType As Type
        ReadOnly m_columns As List(Of IDataColumnInfo)
        Private m_unboundExpression As String

        Public Sub New(name As String, fieldName As String, fieldType As Type, columns As List(Of IDataColumnInfo), unboundExpression As String)
            Me.m_name = name
            Me.m_fieldName = fieldName
            Me.m_fieldType = fieldType
            Me.m_columns = columns
            Me.m_unboundExpression = unboundExpression
        End Sub
        Public ReadOnly Property Caption() As String Implements IDataColumnInfo.Caption
            Get
                Return m_fieldName
            End Get
        End Property

        Public ReadOnly Property Columns() As List(Of IDataColumnInfo) Implements IDataColumnInfo.Columns
            Get
                Return m_columns
            End Get
        End Property

        Public ReadOnly Property Controller() As DataControllerBase Implements IDataColumnInfo.Controller
            Get
                Return Nothing
            End Get
        End Property

        Public ReadOnly Property FieldName() As String Implements IDataColumnInfo.FieldName
            Get
                Return m_fieldName
            End Get
        End Property

        Public ReadOnly Property FieldType() As Type Implements IDataColumnInfo.FieldType
            Get
                Return m_fieldType
            End Get
        End Property

        Public ReadOnly Property Name() As String Implements IDataColumnInfo.Name
            Get
                Return m_name
            End Get
        End Property

        Public ReadOnly Property UnboundExpression() As String Implements IDataColumnInfo.UnboundExpression
            Get
                Return m_unboundExpression
            End Get
        End Property
    End Class

#End Region

#Region "IDataColumnInfo Implementation"

    Public ReadOnly Property Caption() As String Implements IDataColumnInfo.Caption
        Get
            Return "MyExpression"
        End Get
    End Property

    Public ReadOnly Property Columns() As List(Of IDataColumnInfo) Implements IDataColumnInfo.Columns
        Get
            Return m_columns
        End Get
    End Property

    Public ReadOnly Property Controller() As DataControllerBase Implements IDataColumnInfo.Controller
        Get
            Return Nothing
        End Get
    End Property

    Public ReadOnly Property FieldName() As String Implements IDataColumnInfo.FieldName
        Get
            Return String.Empty
        End Get
    End Property

    Public ReadOnly Property FieldType() As Type Implements IDataColumnInfo.FieldType
        Get
            Return Nothing
        End Get
    End Property

    Public ReadOnly Property UnboundExpression() As String Implements IDataColumnInfo.UnboundExpression
        Get
            Return Nothing
        End Get
    End Property

    Public ReadOnly Property Name1 As String Implements IDataColumnInfo.Name
        Get
            Return Nothing
        End Get
    End Property

#End Region

    Private Sub INDSLUpThirdPartyId_OpenFormButtonClick(sender As Object, e As EventArgs) Handles INDSLUpThirdPartyId.OpenFormButtonClick
        OpenForm(532, Nothing, True)
        LoadXpoThirdParty()
    End Sub

#Region "QueryPopup"
    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleAccountStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoThirdParty()
        Using msearch As New MBusqueda
            DatasourceThirdPartyXpo = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ThirdParty)
            INDSLUpThirdPartyId.Datasource = DatasourceThirdPartyXpo
        End Using
    End Sub

    Private Sub INDSLUpThirdPartyId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSLUpThirdPartyId.QueryPopUp
        If INDSLUpThirdPartyId.Datasource Is Nothing Then
            LoadXpoThirdParty()
        End If
    End Sub
#End Region

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleAccountStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoConceptServiceIncentive()
        Using msearch As New MBusqueda
            DatasourceServiceIncentiveXpo = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.Concept)
            INDSlConceptServiceIncentive.Properties.DataSource = DatasourceServiceIncentiveXpo
        End Using
    End Sub

    Private Sub LoadAccountNovelty()
        Using msearch As New MBusqueda()
            Dim filter() As Object = {5, True}
            AccountNoveltyXpo = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Private Sub LoadConceptRetention()
        Using msearch As New MBusqueda()
            ConceptRetentionXpo = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListRetentionConcept)
        End Using
    End Sub

    Private Sub LoadAccountReceivableConcept()
        Using model As New MBusqueda
            Dim filter() As Object = {True}
            INDSlAccountReceivableConcept.Properties.DataSource = model.ConsultarEntidades(eDataSource.GetAllAccountReceivableConceptByStatus, filter)
        End Using
    End Sub

    ''' <summary>
    ''' Carga el DataSource para el control de meses permitidos para ajustes extemporáneos (0-6)
    ''' </summary>
    ''' <remarks>
    ''' Crea una lista con valores del 0 al 6 donde:
    ''' 0 = Ajustes extemporáneos deshabilitados
    ''' 1-6 = Cantidad de meses hacia atrás permitidos
    ''' </remarks>
    Private Sub LoadAllowedMonthsForExtemporaneousAdjustments()
        ' Crear una lista de objetos anónimos con Value y Description
        Dim allowedMonthsList As New List(Of Object) From {
            New With {.Value = CByte(0), .Description = "0 - Deshabilitado"},
            New With {.Value = CByte(1), .Description = "1 - Un mes"},
            New With {.Value = CByte(2), .Description = "2 - Dos meses"},
            New With {.Value = CByte(3), .Description = "3 - Tres meses"},
            New With {.Value = CByte(4), .Description = "4 - Cuatro meses"},
            New With {.Value = CByte(5), .Description = "5 - Cinco meses"},
            New With {.Value = CByte(6), .Description = "6 - Seis meses"}
        }

        ' Configurar el SearchLookUpEdit
        INDsleAllowedMonthsForExtemporaneousAdjustments.Properties.DataSource = allowedMonthsList
        INDsleAllowedMonthsForExtemporaneousAdjustments.Properties.DisplayMember = "Description"
        INDsleAllowedMonthsForExtemporaneousAdjustments.Properties.ValueMember = "Value"
        INDsleAllowedMonthsForExtemporaneousAdjustments.Properties.NullText = "Seleccione..."
        
        ' Configurar las columnas del GridView
        INDsleAllowedMonthsForExtemporaneousAdjustments.Properties.View.Columns.Clear()
        
        Dim colValue As New DevExpress.XtraGrid.Columns.GridColumn()
        colValue.FieldName = "Value"
        colValue.Caption = "Valor"
        colValue.Visible = True
        colValue.Width = 60
        INDsleAllowedMonthsForExtemporaneousAdjustments.Properties.View.Columns.Add(colValue)
        
        Dim colDescription As New DevExpress.XtraGrid.Columns.GridColumn()
        colDescription.FieldName = "Description"
        colDescription.Caption = "Descripción"
        colDescription.Visible = True
        colDescription.Width = 200
        INDsleAllowedMonthsForExtemporaneousAdjustments.Properties.View.Columns.Add(colDescription)
        
        ' Configurar opciones del GridView
        INDsleAllowedMonthsForExtemporaneousAdjustments.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains
        INDsleAllowedMonthsForExtemporaneousAdjustments.Properties.View.OptionsView.ShowGroupPanel = False
        INDsleAllowedMonthsForExtemporaneousAdjustments.Properties.View.OptionsView.ShowIndicator = False
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleAccountStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoConceptDecemberIncentive()
        Using msearch As New MBusqueda
            DatasourceDecemberIncentiveXpo = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.Concept)
            INDSlConceptChristmasIncentive.Properties.DataSource = DatasourceDecemberIncentiveXpo
        End Using
    End Sub

    Private Sub LoadXpoConceptUnemployment()
        Using msearch As New MBusqueda
            DatasourceUnemploymentXpo = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.Concept)
            INDSlUnemploymentConcept.Properties.DataSource = DatasourceUnemploymentXpo
        End Using
    End Sub

    Private Sub LoadXpoConceptInterestUnemployment()
        Using msearch As New MBusqueda
            DatasourceInterestUnemploymentXpo = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.Concept)
            INDSlInterestUnemploymentConcept.Properties.DataSource = DatasourceInterestUnemploymentXpo
        End Using
    End Sub
    ''' <summary>
    ''' Metodo que trae el Xpo de los conceptos
    ''' </summary>
    Public Sub LoadIntegralSalaryInability()
        Using Model As New MBusqueda
            DatasourceIntegralSalaryInabilityXpo = Model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.Concept)
            INDsleIntegralInabilityConcept.Properties.DataSource = DatasourceIntegralSalaryInabilityXpo
        End Using
    End Sub

    Private Sub FrmSettingsPayroll_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    Private Sub InitializeCurrency()
        INDsleCurrency.Properties.DataSource = presenter.GetListCurrency()
    End Sub

	''' <summary>
	''' Establece por defecto la moneda oficial  parametrizada en companySettings
	''' </summary>
	Private Sub GetOfficialCurrency()
		If INDsleCurrency.EditValue.ToString() = String.Empty Then
			INDsleCurrency.EditValue = presenter.GetOfficialCurrencyId()
		End If
	End Sub

	''' <summary>
	''' Establece a  los controles la moneda parametrizada
	''' </summary>
	''' <param name="_currencyAbbreviation"></param>
	Private Sub SetCurrencyFormat(_currencyAbbreviation As String)
		If String.IsNullOrEmpty(_currencyAbbreviation) Then
			Mensaje(EeventViewerImages.Advertencia) = "La abreviación de la moneda está vacía."
			Exit Sub
		End If
		Dim numberFormat = _currencyAbbreviation.GetNumberFormat()
		changeNumericFormatByCurrency(numberFormat)
	End Sub

#Region "Click"
	Private Sub INDBtnAgregar_Click(sender As Object, e As EventArgs) Handles INDBtnAgregar.Click
        If ValidateEvent() = False Then
            Exit Sub
        End If
        Dim newNoveltyConcept As ConceptNoveltyAdjust = New ConceptNoveltyAdjust()
        With newNoveltyConcept
            .IdConcept = INDSlConceptAdjustPrincipal.EditValue
            .IdAdjustConcept = INDSlConceptAdjust.EditValue
            .NameConceptPrincipal = INDSlConceptAdjustPrincipal.Text
            .NameConceptAdjust = INDSlConceptAdjust.Text
        End With
        PayrollSettings.ConceptNoveltyAdjust.Add(newNoveltyConcept)
        INDGcNoveltyConceptAdjust.DataSource = Nothing
        INDGcNoveltyConceptAdjust.DataSource = PayrollSettings.ConceptNoveltyAdjust.ToList()
        INDGcNoveltyConceptAdjust.RefreshDataSource()
        CleanControlsEventConcept()
    End Sub
#End Region

    ''' <summary>
    ''' MEtodo que limpia los controles de la seccion que registra eventos
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CleanControlsEventConcept()
        INDSlConceptAdjustPrincipal.EditValue = Nothing
        INDSlConceptAdjust.EditValue = Nothing
    End Sub

    ''' <summary>
    ''' Funcion para validar campos cuando se registran conceptos a el grupo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateEvent() As Boolean
        If INDSlConceptAdjustPrincipal.EditValue Is Nothing Then
            INDSlConceptAdjustPrincipal.Focus()
            Return False
        End If
        If INDSlConceptAdjust.EditValue Is Nothing Then
            INDSlConceptAdjustPrincipal.Focus()
            Return False
        End If

        If PayrollSettings.ConceptNoveltyAdjust.Count > 0 Then
            If PayrollSettings.ConceptNoveltyAdjust.Where(Function(x) x.IdConcept = INDSlConceptAdjustPrincipal.EditValue).FirstOrDefault IsNot Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "El Concepto ya se encuentra agregado"
                Return False
            End If
        End If
        indigo.LanguageCulture = ""
        Return True
    End Function

#Region "MenuContext"

    ''' <summary>
    ''' Despliega los botones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.ButtonEdit
        btn = DirectCast(sender, DevExpress.XtraEditors.ButtonEdit)
        Select Case btn.Text
            Case "Eliminar"
                RemoveDetail()
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
            Case "Remove"
                RemoveDetail()
        End Select
    End Sub

#End Region

    ''' <summary>
    ''' Elimina el detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RemoveDetail()
        Dim _row As ConceptNoveltyAdjust = INDGvNoveltyConceptAdjust.GetFocusedRow()
        _row.MarkAsDeleted()
        PayrollSettings.ConceptNoveltyAdjust.Remove(_row)
        INDGvNoveltyConceptAdjust.FocusedColumn = INDGvNoveltyConceptAdjust.Columns.Item(0)
        INDGcNoveltyConceptAdjust.DataSource = Nothing
        INDGcNoveltyConceptAdjust.DataSource = PayrollSettings.ConceptNoveltyAdjust.ToList()
        INDGcNoveltyConceptAdjust.RefreshDataSource()
    End Sub

#Region "EditValueChanged"
    Private Sub INDSlConceptAdjustPrincipal_EditValueChanged(sender As Object, e As EventArgs) Handles INDSlConceptAdjustPrincipal.EditValueChanged
        Dim _row As Concept = INDGvPrincipalAdjustConcept.GetFocusedRow()
        If _row IsNot Nothing Then
            INDSlConceptAdjust.Properties.DataSource = presenter.LoadConceptAdjustment(_row.ConceptClass)
        End If

    End Sub

    Private Sub INDGleRetentionBiweeklyDiscount_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleRetentionBiweeklyDiscount.EditValueChanged
        If INDGleRetentionBiweeklyDiscount.EditValue Then
            INDLciWithholdingTaxRefundConcept.ShowLayout()
        Else
            INDLciWithholdingTaxRefundConcept.HideLayout()
            INDSleWithholdingTaxRefundConcept.EditValue = Nothing
        End If
    End Sub

    Private Sub INDSlComprobantType_EditValueChanged(sender As Object, e As EventArgs) Handles INDSlComprobantType.EditValueChanged
        Select Case INDSlComprobantType.EditValue
            Case 1
                INDLciBankAccount.HideControl(False)
                INDLciGenerate.HideControl(False)
                INDLciCash.HideControl()
                INDSlPaymentMethod.EditValue = 2
                IdCashRegister = Nothing
            Case 2, 3
                INDLciCash.HideControl(False)
                INDLciBankAccount.HideControl()
                INDLciGenerate.HideControl()
                INDSlPaymentMethod.EditValue = Nothing
                IdEntityBankAccount = Nothing
                presenter.InitializeCash(INDSlComprobantType.EditValue - 1)
        End Select
    End Sub

    Private Sub INDSlComprobantTypeVacation_EditValueChanged(sender As Object, e As EventArgs) Handles INDSlComprobantTypeVacation.EditValueChanged
        HidControlsVoucherTransactionVacation()
    End Sub

    Private Sub INDSlComprobantTypeLiquidation_EditValueChanged(sender As Object, e As EventArgs) Handles INDSlComprobantTypeLiquidation.EditValueChanged
        HidControlsVoucherTransactionContractLiquidation()
    End Sub

    Private Sub IndSlYesNoVacationVoucherTransaction_EditValueChanged(sender As Object, e As EventArgs) Handles IndSlYesNoVacationVoucherTransaction.EditValueChanged
        If IndSlYesNoVacationVoucherTransaction.EditValue = True Then
            LayoutControlItem9.HideControl(False)
            INDLciComprobantTypeVacation.HideControl(False)
            HidControlsVoucherTransactionVacation()
        Else
            LayoutControlItem9.HideControl(True)
            INDLciComprobantTypeVacation.HideControl(True)
            INDLciBankAccountVacation.HideControl(True)
            INDLciGenerateVacation.HideControl(True)
            INDLciCashVacation.HideControl(True)
        End If

    End Sub

    Private Sub IndSlYesNoContractLiquidationVoucherTransaction_EditValueChanged(sender As Object, e As EventArgs) Handles IndSlYesNoContractLiquidationVoucherTransaction.EditValueChanged
        If IndSlYesNoContractLiquidationVoucherTransaction.EditValue = True Then
            LayoutControlItem11.HideControl(False)
            INDLciComprobantTypeLiquidation.HideControl(False)
            HidControlsVoucherTransactionContractLiquidation()
        Else
            LayoutControlItem11.HideControl(True)
            INDLciComprobantTypeLiquidation.HideControl(True)
            INDLciBankAccountLiquidation.HideControl(True)
            INDLciGenerateLiquidation.HideControl(True)
            INDLciCashLiquidation.HideControl(True)
        End If
    End Sub

#End Region

#Region "HideControls"
    Private Sub HidControlsVoucherTransactionVacation()
        Select Case INDSlComprobantTypeVacation.EditValue
            Case 1
                INDLciBankAccountVacation.HideControl(False)
                INDLciGenerateVacation.HideControl(False)
                INDLciCashVacation.HideControl()
                INDSlPaymentMethodVacation.EditValue = 2
                IdCashRegisterVacation = Nothing
            Case 2, 3
                INDLciCashVacation.HideControl(False)
                INDLciBankAccountVacation.HideControl()
                INDLciGenerateVacation.HideControl()
                INDSlPaymentMethodVacation.EditValue = Nothing
                IdEntityBankAccountVacation = Nothing
                presenter.InitializeCashVacation(INDSlComprobantTypeVacation.EditValue - 1)
        End Select
    End Sub

    Private Sub HidControlsVoucherTransactionContractLiquidation()
        Select Case INDSlComprobantTypeLiquidation.EditValue
            Case 1
                INDLciBankAccountLiquidation.HideControl(False)
                INDLciGenerateLiquidation.HideControl(False)
                INDLciCashLiquidation.HideControl()
                INDSlPaymentMethodLiquidation.EditValue = 2
                IdCashRegisterVacation = Nothing
            Case 2, 3
                INDLciCashLiquidation.HideControl(False)
                INDLciBankAccountLiquidation.HideControl()
                INDLciGenerateLiquidation.HideControl()
                INDSlPaymentMethodLiquidation.EditValue = Nothing
                IdEntityBankAccountVacation = Nothing
                presenter.InitializeCashLiquidation(INDSlComprobantTypeLiquidation.EditValue - 1)
        End Select
    End Sub

    Private Sub INDSlYesNoLiquidarContratoFormula_EditValueChanged(sender As Object, e As EventArgs) Handles INDSlYesNoLiquidarContratoFormula.EditValueChanged
        If INDSlYesNoLiquidarContratoFormula.EditValue Then
            INDLcgContractLiquidationPayrollConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLcgContractLiquidationIncentive.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLcgContractLiquidationVacation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

            'INDLciLiqCesantias.ShowLayout()
            'INDLciSeveranceInterestConcept.ShowLayout()
            INDLciPriorNoticeConcept.ShowLayout()
            'INDLciBonusLiquidationConcept.ShowLayout()
            'INDLciVacationConcept.ShowLayout()
            INDLciSeveranceInterest.ShowLayout()
            INDLciPriorNotice.ShowLayout()
            'INDLcgContractLiquidationConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDMeSeveranceInterestFormula.EditValue = "0"
            INDMePreNoticeFormula.EditValue = "0"
        Else
            INDLcgContractLiquidationPayrollConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLcgContractLiquidationIncentive.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLcgContractLiquidationVacation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

            INDLciLiqCesantias.HideLayout()
            INDLciSeveranceInterestConcept.HideLayout()
            INDLciPriorNoticeConcept.HideLayout()
            INDLciBonusLiquidationConcept.HideLayout()
            INDLciVacationConcept.HideLayout()
            INDLciSeveranceInterest.HideLayout()
            INDLciPriorNotice.HideLayout()
            INDLcgContractLiquidationConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDSleSeveranceConcept.EditValue = Nothing
            INDSleSeveranceInterestConcept.EditValue = Nothing
            INDSlePreNoticeSettlementConcept.EditValue = Nothing
            INDSleBonusSettlementConcept.EditValue = Nothing
            INDSleVacationSettlementConcept.EditValue = Nothing
            INDMeSeveranceInterestFormula.EditValue = Nothing
            INDMePreNoticeFormula.EditValue = Nothing
        End If
    End Sub

    Private Sub INDrgbLiquidatesSeverancePayments_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles INDrgbLiquidatesSeverancePayments.EditValueChanging
        If LiquidatesSeverancePayments = False Then
            INDLciUnemploymentTypeCalculated.HideControl(LiquidatesSeverancePayments)
            INDLciUnemploymentConcept.HideControl(LiquidatesSeverancePayments)
            INDLciInterestUnemploymentConcept.HideControl(LiquidatesSeverancePayments)
        Else
            INDLciUnemploymentTypeCalculated.HideControl(LiquidatesSeverancePayments)
            INDLciUnemploymentConcept.HideControl(LiquidatesSeverancePayments)
            INDLciInterestUnemploymentConcept.HideControl(LiquidatesSeverancePayments)
        End If
    End Sub

    Private Sub INDRgHandlesLiquidationFirstTwoDays_EditValueChanged(sender As Object, e As EventArgs) Handles INDRgHandlesLiquidationFirstTwoDays.EditValueChanged
        If INDRgHandlesLiquidationFirstTwoDays.EditValue = True Then
            INDLciFirstTwoDaysFormulate.ShowLayout()
        Else
            INDLciFirstTwoDaysFormulate.HideLayout()
            INDMeFirstTwoDaysFormulate.EditValue = Nothing
        End If
    End Sub
    ''' <summary>
    ''' Evento para mostar el campo de "Concepto de Incapacidad Asumido Salario Integral"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub CtrYesNoIntegralInability_EditValueChanged(sender As Object, e As EventArgs) Handles CtrYesNoIntegralInability.EditValueChanged
        If IntegralSalaryInability = True Then
            LoadIntegralSalaryInability()
            INDlciIntegralInabilityConcept.ShowLayout()
        Else
            INDsleIntegralInabilityConcept.Properties.DataSource = Nothing
            IntegralSalaryInabilityConcept = Nothing
            INDlciIntegralInabilityConcept.HideLayout()
        End If
    End Sub

#End Region


End Class