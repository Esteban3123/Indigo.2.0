#Region "Imports"
Imports System.Windows.Forms
Imports Domain.Payroll.Entities
Imports Presentation.Base

#End Region
Public Class FrmLiquidator

#Region "Variables"
    ''' <summary>
    ''' Variable que permite traer los valores de Retención
    ''' </summary>
    Dim _liquidation As Liquidation

    ''' <summary>
    ''' Variable que trae valores del Empleado
    ''' </summary>
    Dim _employee As Employee
    ''' <summary>
    ''' Variable que trae los valores de Employee
    ''' </summary>
    Dim _idEmployee As Integer

#End Region

#Region "Properties"
    ''' <summary>
    ''' Pagos mensuales
    ''' </summary>
    ''' <returns></returns>
    Private Property MonthlyPayments As Decimal
        Get
            Return INDtxtMonthlyPayments.EditValue
        End Get
        Set(value As Decimal)
            INDtxtMonthlyPayments.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Total ingresos mensuales
    ''' </summary>
    ''' <returns></returns>
    Private Property TotalIncomeMonthly As Decimal
        Get
            Return INDtxtTotalIncomeMonthly.EditValue
        End Get
        Set(value As Decimal)
            INDtxtTotalIncomeMonthly.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Valor de pago a pensiones
    ''' </summary>
    ''' <returns></returns>
    Private Property PensionContributionValue As Decimal
        Get
            Return INDtxtPensionContributionValue.EditValue
        End Get
        Set(value As Decimal)
            INDtxtPensionContributionValue.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Valor de pagos a salud
    ''' </summary>
    ''' <returns></returns>
    Private Property PaymentHealthObligatory As Decimal
        Get
            Return INDtxtPaymentHealthObligatory.EditValue
        End Get
        Set(value As Decimal)
            INDtxtPaymentHealthObligatory.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Pago a fondo solidario de pensiones
    ''' </summary>
    ''' <returns></returns>
    Private Property SolidarityPension As Decimal
        Get
            Return INDtxtSolidarityPension.EditValue
        End Get
        Set(value As Decimal)
            INDtxtSolidarityPension.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Pago individual a pension
    ''' </summary>
    ''' <returns></returns>
    Private Property PensionByIndividualSavingsRegime As Decimal
        Get
            Return INDtxtPensionByIndividualSavingsRegime.EditValue
        End Get
        Set(value As Decimal)
            INDtxtPensionByIndividualSavingsRegime.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Total de ingresos no constitutivos
    ''' </summary>
    ''' <returns></returns>
    Private Property TotalNoConstitutive As Decimal
        Get
            Return INDtxtTotalNoConstitutive.EditValue
        End Get
        Set(value As Decimal)
            INDtxtTotalNoConstitutive.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Intereses préstamos de vivienda
    ''' </summary>
    ''' <returns></returns>
    Private Property HousingInterests As Decimal
        Get
            Return INDtxtHousingInterests.EditValue
        End Get
        Set(value As Decimal)
            INDtxtHousingInterests.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Por Dependientes
    ''' </summary>
    ''' <returns></returns>
    Private Property Dependent As Decimal
        Get
            Return INDspDependent.EditValue
        End Get
        Set(value As Decimal)
            INDspDependent.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Pagos por salud
    ''' </summary>
    ''' <returns></returns>
    Private Property HealthPayments As Decimal
        Get
            Return INDtxtHealthPayments.EditValue
        End Get
        Set(value As Decimal)
            INDtxtHealthPayments.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Total deducciones
    ''' </summary>
    ''' <returns></returns>
    Private Property TotalDeductions As Decimal
        Get
            Return INDtxtTotalDeductions.EditValue
        End Get
        Set(value As Decimal)
            INDtxtTotalDeductions.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Aporte fondo pensiones voluntaria
    ''' </summary>
    ''' <returns></returns>
    Private Property VoluntaryContributions As Decimal
        Get
            Return INDspVoluntaryContributions.EditValue
        End Get
        Set(value As Decimal)
            INDspVoluntaryContributions.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Aporte destino a Cuentas AFC
    ''' </summary>
    ''' <returns></returns>
    Private Property AccountContributionsAFC As Decimal
        Get
            Return INDspAccountContributions.EditValue
        End Get
        Set(value As Decimal)
            INDspAccountContributions.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Total rentas exentas
    ''' </summary>
    ''' <returns></returns>
    Private Property TotalIncomeExent As Decimal
        Get
            Return INDtxtTotalIncomeExent.EditValue
        End Get
        Set(value As Decimal)
            INDtxtTotalIncomeExent.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Subtotal de la retención
    ''' </summary>
    ''' <returns></returns>
    Private Property Subtotal As Decimal
        Get
            Return INDtxtSubtotal.EditValue
        End Get
        Set(value As Decimal)
            INDtxtSubtotal.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Control renta exenta del 25%
    ''' </summary>
    ''' <returns></returns>
    Private Property ExemptIncomeControl As Decimal
        Get
            Return INDtxtExemptIncomeControl.EditValue
        End Get
        Set(value As Decimal)
            INDtxtExemptIncomeControl.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Renta exenta del 25%
    ''' </summary>
    ''' <returns></returns>
    Private Property ExemptIncome As Decimal
        Get
            Return INDtxExemptIncome.EditValue
        End Get
        Set(value As Decimal)
            INDtxExemptIncome.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Control rentas exentas y deducciones
    ''' </summary>
    ''' <returns></returns>
    Private Property ControlExemptIncomeAndDeductions As Decimal
        Get
            Return INDtxtControlExemptIncomeAndDeductions.EditValue
        End Get
        Set(value As Decimal)
            INDtxtControlExemptIncomeAndDeductions.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Total rentas exentas y deducciones
    ''' </summary>
    ''' <returns></returns>
    Private Property ExemptIncomeAndDeductions As Decimal
        Get
            Return INDtxtExemptIncomeAndDeductions.EditValue
        End Get
        Set(value As Decimal)
            INDtxtExemptIncomeAndDeductions.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Base gravable
    ''' </summary>
    ''' <returns></returns>
    Private Property TaxBase As Double
        Get
            Return INDtxtTaxBase.EditValue
        End Get
        Set(value As Double)
            INDtxtTaxBase.EditValue = value
        End Set
    End Property
#End Region

#Region "Builder"
    ''' <summary>
    ''' Constructor Con Valor Específico para FrmPayrollLiquidationDetail
    ''' </summary>
    Public Sub New(ByVal _listEmployee As List(Of Employee), ByVal _liquidationEmployee As List(Of Liquidation), ByVal IdEmployee As Integer)
        InitializeComponent()
        _employee = _listEmployee.FirstOrDefault(Function(emp) emp.Id = IdEmployee)
        _liquidation = _liquidationEmployee.FirstOrDefault(Function(liq) liq.EmployeeId = IdEmployee)
        LoadControls()
    End Sub
#End Region

#Region "Load"
    ''' <summary>
    ''' Evento que se dispara al cargar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmLiquidator_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ActionsOnControls = False
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = False

        INDtxtMonthlyPayments.ReadOnly = True
        INDtxtTotalIncomeMonthly.ReadOnly = True

        INDtxtMonthlyPayments.ReadOnly = True
        INDtxtPaymentHealthObligatory.ReadOnly = True
        INDtxtSolidarityPension.ReadOnly = True
        INDtxtPensionByIndividualSavingsRegime.ReadOnly = True
        INDtxtTotalNoConstitutive.ReadOnly = True

        INDtxtHousingInterests.ReadOnly = True
        INDspDependent.ReadOnly = True
        INDtxtHealthPayments.ReadOnly = True
        INDtxtTotalDeductions.ReadOnly = True

        INDspVoluntaryContributions.ReadOnly = True
        INDspAccountContributions.ReadOnly = True
        INDtxtTotalIncomeExent.ReadOnly = True

        INDtxtSubtotal.ReadOnly = True
        INDtxtExemptIncomeControl.ReadOnly = True
        INDtxExemptIncome.ReadOnly = True
        INDtxtControlExemptIncomeAndDeductions.ReadOnly = True
        INDtxtExemptIncomeAndDeductions.ReadOnly = True
        INDtxtTaxBase.ReadOnly = True

    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Deshace los cambios que hallan en el form
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CleanControls()
        MonthlyPayments = Nothing
        TotalIncomeMonthly = Nothing
        PensionContributionValue = Nothing
        PaymentHealthObligatory = Nothing
        SolidarityPension = Nothing
        PensionByIndividualSavingsRegime = Nothing
        TotalNoConstitutive = Nothing
        VoluntaryContributions = Nothing
        HousingInterests = Nothing
        Dependent = Nothing
        HealthPayments = Nothing
        TotalDeductions = Nothing
        AccountContributionsAFC = Nothing
        TotalIncomeExent = Nothing
    End Sub

    ''' <summary>
    ''' Carga los controles del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadControls()
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Nuevo) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Cerrar) = False

        With _liquidation
            MonthlyPayments = .BasicSalary
            TotalIncomeMonthly = .BasicSalary

            PensionContributionValue = .PensionContributionValue
            PaymentHealthObligatory = .EmployeeHealthContributionValue
            SolidarityPension = .PensionSolidarityFundValueContribution
            PensionByIndividualSavingsRegime = .VoluntaryContributionPensionValue
            VoluntaryContributions = .VoluntaryContributionPensionValue
            TotalNoConstitutive = PensionContributionValue + PaymentHealthObligatory + SolidarityPension + PensionByIndividualSavingsRegime

            AccountContributionsAFC = .AFCAccount

            Subtotal = .Subtotal
            ExemptIncome = .ExemptIncome
            ExemptIncomeAndDeductions = .TotalExemptIncomeandDeductions

            TaxBase = .TaxBase
        End With
        With _employee
            HousingInterests = .HousingDeductionValue
            Dependent = .BasicSalary * 0.1
            HealthPayments = .HealthContributorRTF
            TotalDeductions = HousingInterests + Dependent + HealthPayments
            TotalIncomeExent = VoluntaryContributions + AccountContributionsAFC

        End With
    End Sub

    ''' <summary>
    ''' Evento con botón cerrar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnCloseFrm_Click(sender As Object, e As EventArgs) Handles INDbtnCloseFrm.Click
        CleanControls()
        Me.Close()
    End Sub
    ''' <summary>
    ''' Cerrar formulario con Escape
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmLiquidator_KeyUp(sender As Object, e As KeyEventArgs) Handles MyBase.KeyUp
        ' Verificar si la tecla presionada es la tecla Escape (Esc)
        If e.KeyCode = Keys.Escape Then
            CleanControls()
            Me.Close()
        End If
    End Sub

#End Region

End Class