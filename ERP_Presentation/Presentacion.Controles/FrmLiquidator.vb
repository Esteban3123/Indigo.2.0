'***********************************************************************
' Assembly         : Presentacion.Controls
' Author           : Carlos Mario Arias Rubiano
' Created          : 27/03/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Controls.MVP
Imports Domain.Entities
Imports System.Windows.Forms.DataFormats
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Presentation.Base

#End Region

Public Class FrmLiquidator
    Implements ILiquidator

    Dim ViewRetentionAccumulated As Infrastructure.Data.Xpo.PaymentsRepository.ViewThirdPartyRetentionAccumulatedXpo

#Region "Builder"

    Public ctrTmp As CtrValueLiquidator

    Public Sub New(ByVal _companySettings As CompanySettings, ByVal _listRange383 As List(Of RetentionConceptRanges), ByVal _listRange384 As List(Of RetentionConceptRanges),
                   accountPayableDetailConceptLiquidation As AccountPayableDetailConceptLiquidation)
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        CompanySettings = _companySettings
        UVT = CompanySettings.UVT
        SMLV = CompanySettings.SMLV
        ListRangeRetention383 = _listRange383

        'Parámetro para identificar cálculo de topes de rentas y deducciones (0 Anual - 1 Mensual)
        WorkIncomeControl = CompanySettings.WorkIncomeControl

        Presenter = New PLiquidator(Me)
        Presenter.WorkIncomeControl = CompanySettings.WorkIncomeControl

        ctrTmp = New CtrValueLiquidator
        ctrTmp.PopupContainerControlTotalValue = INDpopupTotalValues
        ctrTmp.SetTotalValues(AddressOf getValues)
        ctrTmp.RefreshTotalValues()
        ctrTmp.Dock = DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrTmp)

        If accountPayableDetailConceptLiquidation IsNot Nothing Then
            CleanConcept = False
            _accountPayableDetailConceptLiquidation = accountPayableDetailConceptLiquidation
            For Each item In _accountPayableDetailConceptLiquidation.AccountPayableDetailConceptLiquidationAdjusments
                item.UUID = Guid.NewGuid().ToString()
            Next
            LoadControls()
        Else
            CleanConcept = True
        End If
    End Sub

#End Region

#Region "Properties"

#Region "Fields"

    ''' <summary>
    ''' Obtiene o establece el Id del tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ThirdPartyId As Integer

    ''' <summary>
    ''' Obtiene o establece el año gravable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FiscalYear As Integer

    ''' <summary>
    ''' Fecha del documento que viene de la cabecera
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DocumentDate As DateTime

    ''' <summary>
    ''' Fecha de creación del documento que viene de la cabecera
    ''' </summary>
    Public Property CreationDate As DateTime

    ''' <summary>
    ''' Obtiene el estado de la cuenta por pagar
    ''' </summary>
    Public OriginStatus As Byte

    ''' <summary>
    ''' Obtiene el código de la cuenta por pagar / nota / documento
    ''' </summary>
    Public OriginCode As String

    ''' <summary>
    ''' Obtiene número de la factura
    ''' </summary>
    Public BillNumber As String

    ''' <summary>
    ''' Obtiene o establece el valor de los honorarios, comisiones, o servicios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FeeCommissionServiceData As Decimal Implements ILiquidator.FeeCommissionServiceData
        Get
            Return INDtxtFeeCommissionServiceData.EditValue
        End Get
        Set(value As Decimal)
            INDtxtFeeCommissionServiceData.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece los ingresos acumulados del mes
    ''' </summary>
    ''' <returns></returns>
    Public Property AccumulatedIncome As Decimal Implements ILiquidator.AccumulatedIncome
        Get
            Return INDtxtAccumulatedIncome.EditValue
        End Get
        Set(value As Decimal)
            INDtxtAccumulatedIncome.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor del aporte obligatiro al fondo de pension
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RequiredContributions As Decimal Implements ILiquidator.RequiredContributions
        Get
            Return INDspPensionContribution.EditValue
        End Get
        Set(value As Decimal)
            INDspPensionContribution.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor del pago obligatorio a salud
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PaymentHealthObligatory As Decimal Implements ILiquidator.PaymentHealthObligatory
        Get
            Return INDSPPaymentHealthObligatory.EditValue
        End Get
        Set(value As Decimal)
            INDSPPaymentHealthObligatory.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor de la pension solidaria
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SolidarityPension As Decimal Implements ILiquidator.SolidarityPension
        Get
            Return INDspSolidarityPension.EditValue
        End Get
        Set(value As Decimal)
            INDspSolidarityPension.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor de los Aportes Voluntarios a Pensión régimen ahorro individual
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PensionByIndividualSavingsRegime As Decimal Implements ILiquidator.PensionByIndividualSavingsRegime
        Get
            Return INDspPensionByIndividualSavingsRegime.EditValue
        End Get
        Set(value As Decimal)
            INDspPensionByIndividualSavingsRegime.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor del interes prestamos de vivienda
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Interests As Decimal Implements ILiquidator.Interests
        Get
            Return INDtxtInterests.EditValue
        End Get
        Set(value As Decimal)
            INDtxtInterests.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor dependiente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Dependent As Decimal Implements ILiquidator.Dependent
        Get
            Return INDspDependent.EditValue
        End Get
        Set(value As Decimal)
            INDspDependent.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor de la salud preparada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PreparedHealth As Decimal Implements ILiquidator.PreparedHealth
        Get
            Return INDtxtPreparedHealth.EditValue
        End Get
        Set(value As Decimal)
            INDtxtPreparedHealth.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor del riesgo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RiskWork As Decimal Implements ILiquidator.RiskWork
        Get
            Return INDspRiskWork.EditValue
        End Get
        Set(value As Decimal)
            INDspRiskWork.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor de la contribucion voluntaria
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property VoluntaryContributions As Decimal Implements ILiquidator.VoluntaryContributions
        Get
            Return INDspVoluntaryContributions.EditValue
        End Get
        Set(value As Decimal)
            INDspVoluntaryContributions.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor de la contribucion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountContributions As Decimal Implements ILiquidator.AccountContributions
        Get
            Return INDspAccountContributions.EditValue
        End Get
        Set(value As Decimal)
            INDspAccountContributions.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor retenido de la calculadora
    ''' </summary>
    ''' <returns></returns>
    Public Property RetArt383 As Decimal Implements ILiquidator.RetArt383
        Get
            Return INDtxtRetArt383.EditValue
        End Get
        Set(value As Decimal)
            INDtxtRetArt383.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece los valores retenidos anteriores
    ''' </summary>
    ''' <returns></returns>
    Public Property PreviousRetArt383 As Decimal Implements ILiquidator.PreviousRetArt383
        Get
            Return INDtxtPreviousDeductions.EditValue
        End Get
        Set(value As Decimal)
            INDtxtPreviousDeductions.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Retención a aplicar final
    ''' </summary>
    ''' <returns></returns>
    Public Property FinalRetention As Decimal Implements ILiquidator.FinalRetention
        Get
            Return INDtxtWithholdingApplied.EditValue
        End Get
        Set(value As Decimal)
            INDtxtWithholdingApplied.EditValue = value
        End Set
    End Property

#End Region

#Region "Totals"

    ''' <summary>
    ''' Obtiene o establece el valor del total de los ingresos mensuales
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TotalIncomeMonthlyData As Decimal Implements ILiquidator.TotalIncomeMonthlyData
        Get
            Return INDtxtTotalIncomeMonthlyData.EditValue
        End Get
        Set(value As Decimal)
            INDtxtTotalIncomeMonthlyData.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor total de los ingresos exentos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TotalNoConstitutive As Decimal Implements ILiquidator.TotalNoConstitutive
        Get
            Return INDtxtTotalNoConstitutive.EditValue
        End Get
        Set(value As Decimal)
            INDtxtTotalNoConstitutive.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor total de deducciones
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TotalDeductions As Decimal Implements ILiquidator.TotalDeductions
        Get
            Return INDtxtTotalDeductions.EditValue
        End Get
        Set(value As Decimal)
            INDtxtTotalDeductions.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor total de los ingresos exentos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TotalIncomeExent As Decimal Implements ILiquidator.TotalIncomeExent
        Get
            Return INDtxtTotalIncomeExent.EditValue
        End Get
        Set(value As Decimal)
            INDtxtTotalIncomeExent.EditValue = value
        End Set
    End Property

#End Region

#End Region

#Region "Variables"

#Region "Previous"

    Dim ExemptIncomeCalculated As Decimal

    Dim ExemptIncomePrevious As Decimal

    Dim ExemptIncomeAndDeductionsCalculated As Decimal

    Dim ExemptIncomeAndDeductionsPrevious As Decimal

#End Region

#Region "Real Values"

    Dim superToolTip As DevExpress.Utils.SuperToolTip
    Dim toolTipItemBody As New DevExpress.Utils.ToolTipItem

    ''' <summary>
    ''' Obtiene o establece el valor del aporte obligatiro al fondo de pension
    ''' Artículo 5 de la ley 797 de 2003: El límite de la base de cotización será de veinticinco (25) salarios mínimos legales mensuales
    ''' En ningún caso el ingreso base de cotización podrá ser inferior a un salario mínimo legal mensual vigente.
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property RequiredContributionsReal As Decimal
        Get
            Dim value = RequiredContributions
            Dim valueMaximum As Decimal = 25 * SMLV * (PensionPercentage / 100)
            value = Presenter.ChooseValue(valueMaximum, value)

            'Información util a mostrar
            superToolTip = New DevExpress.Utils.SuperToolTip
            toolTipItemBody = New DevExpress.Utils.ToolTipItem
            toolTipItemBody.Text = "Tope IBC Máximo 25 SMLMV: " + valueMaximum.ToString("C0")
            superToolTip.Items.Add(toolTipItemBody)

            toolTipItemBody = New DevExpress.Utils.ToolTipItem
            toolTipItemBody.Text = "Valor Real: " + value.ToString("C0")
            superToolTip.Items.Add(toolTipItemBody)
            INDspPensionContribution.SuperTip = superToolTip

            Return value
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor del pago obligatorio a salud
    ''' Artículo 5 de la ley 797 de 2003: El límite de la base de cotización será de veinticinco (25) salarios mínimos legales mensuales
    ''' En ningún caso el ingreso base de cotización podrá ser inferior a un salario mínimo legal mensual vigente.
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property PaymentHealthObligatoryReal As Decimal
        Get
            Dim value = PaymentHealthObligatory
            Dim valueMaximum As Decimal = 25 * SMLV * (HealthPercentage / 100)
            value = Presenter.ChooseValue(valueMaximum, value)

            'Información util a mostrar
            superToolTip = New DevExpress.Utils.SuperToolTip
            toolTipItemBody = New DevExpress.Utils.ToolTipItem
            toolTipItemBody.Text = "Tope IBC Máximo 25 SMLMV: " + valueMaximum.ToString("C0")
            superToolTip.Items.Add(toolTipItemBody)

            toolTipItemBody = New DevExpress.Utils.ToolTipItem
            toolTipItemBody.Text = "Valor Real: " + value.ToString("C0")
            superToolTip.Items.Add(toolTipItemBody)
            INDSPPaymentHealthObligatory.SuperTip = superToolTip

            Return value
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor de la pension solidaria
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property SolidarityPensionReal As Decimal
        Get
            Return SolidarityPension
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor de los Aportes Voluntarios a Pensión régimen ahorro individual
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property PensionByIndividualSavingsRegimeReal As Decimal
        Get
            Return PensionByIndividualSavingsRegime
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor del interes prestamos de vivienda
    ''' Artículo 119 Estatuto Tributario: El límite anual de 1200UVT que equivale a un limite Mensual de 100UVT
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property InterestsReal As Decimal
        Get
            Dim value = Interests
            Dim valueMaximum As Decimal = 100 * UVT
            value = Presenter.ChooseValue(valueMaximum, value)

            'Información util a mostrar
            superToolTip = New DevExpress.Utils.SuperToolTip
            toolTipItemBody = New DevExpress.Utils.ToolTipItem
            toolTipItemBody.Text = "Tope 100 UVT: " + valueMaximum.ToString("C0")
            superToolTip.Items.Add(toolTipItemBody)

            toolTipItemBody = New DevExpress.Utils.ToolTipItem
            toolTipItemBody.Text = "Valor Real: " + value.ToString("C0")
            superToolTip.Items.Add(toolTipItemBody)
            INDtxtInterests.SuperTip = superToolTip

            Return value
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor dependiente
    ''' Artículo 387 Estatuto Tributario: una deducción mensual de hasta el 10% del total de los ingresos brutos provenientes de la relación laboral o legal 
    ''' y reglamentaria del respectivo mes por concepto de dependientes, hasta un máximo de treinta y dos (32) UVT mensuales
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property DependentReal As Decimal
        Get
            Dim value = Dependent
            Dim valueMaximumForIncome As Decimal = TotalIncomeMonthlyData * (10 / 100)
            Dim valueMaximumUVT As Decimal = 32 * UVT
            value = Presenter.ChooseValue(valueMaximumForIncome, value)
            value = Presenter.ChooseValue(valueMaximumUVT, value)

            'Información util a mostrar
            superToolTip = New DevExpress.Utils.SuperToolTip
            toolTipItemBody = New DevExpress.Utils.ToolTipItem
            toolTipItemBody.Text = "Tope 10% Ingreso: " + valueMaximumForIncome.ToString("C0")
            superToolTip.Items.Add(toolTipItemBody)

            toolTipItemBody = New DevExpress.Utils.ToolTipItem
            toolTipItemBody.Text = "Tope 32 UVT: " + valueMaximumUVT.ToString("C0")
            superToolTip.Items.Add(toolTipItemBody)

            toolTipItemBody = New DevExpress.Utils.ToolTipItem
            toolTipItemBody.Text = "Valor Real: " + value.ToString("C0")
            superToolTip.Items.Add(toolTipItemBody)
            INDspDependent.SuperTip = superToolTip

            Return value
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor de la salud preparada
    ''' Artículo 387 Estatuto Tributario: los pagos por salud, siempre Que el valor a disminuir mensualmente en este último caso, no supere dieciséis (16) UVT mensuales
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property PreparedHealthReal As Decimal
        Get
            Dim value = PreparedHealth
            Dim valueMaximum As Decimal = 16 * UVT
            value = Presenter.ChooseValue(valueMaximum, value)

            'Información util a mostrar
            superToolTip = New DevExpress.Utils.SuperToolTip
            toolTipItemBody = New DevExpress.Utils.ToolTipItem
            toolTipItemBody.Text = "Tope 16 UVT: " + valueMaximum.ToString("C0")
            superToolTip.Items.Add(toolTipItemBody)

            toolTipItemBody = New DevExpress.Utils.ToolTipItem
            toolTipItemBody.Text = "Valor Real: " + value.ToString("C0")
            superToolTip.Items.Add(toolTipItemBody)
            INDtxtPreparedHealth.SuperTip = superToolTip

            Return value
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor del riesgo
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property RiskWorkReal As Decimal
        Get
            Return RiskWork
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor de la contribucion voluntaria
    ''' Artículo 126-1 Estatuto Tributario: serán considerados como una renta exenta, hasta una suma que adicionada al valor de los aportes a las Cuentas de Ahorro para el 
    ''' Fomento de la Construcción (AFC) de que trata el artículo 126-4 de este Estatuto, no exceda del treinta por ciento (30%) del ingreso laboral o ingreso tributario del año, 
    ''' según el caso, y hasta un monto máximo de tres mil ochocientas (3.800) UVT por año.
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property VoluntaryContributionsReal As Decimal
        Get
            Return VoluntaryContributions
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor de la contribucion
    ''' Obtiene o establece el valor de la contribucion voluntaria
    ''' Artículo 126-1 Estatuto Tributario: serán considerados como una renta exenta, hasta una suma que adicionada al valor de los aportes a las Cuentas de Ahorro para el 
    ''' Fomento de la Construcción (AFC) de que trata el artículo 126-4 de este Estatuto, no exceda del treinta por ciento (30%) del ingreso laboral o ingreso tributario del año, 
    ''' según el caso, y hasta un monto máximo de tres mil ochocientas (3.800) UVT por año.
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property AccountContributionsReal As Decimal
        Get
            Return AccountContributions
        End Get
    End Property

#End Region

#Region "Real Totals"

    ''' <summary>
    ''' Obtiene o establece el valor total de los ingresos exentos
    ''' Suma de los valores calculados de aportes obligatorias a fondos de pensiones, el fondo de solidarida pensional, el pago a salud obligatoria 
    ''' y los aportes voluntarios a pensión en el régimen de ahorro individual
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property TotalNoConstitutiveReal As Decimal
        Get
            Return RequiredContributionsReal + PaymentHealthObligatoryReal + SolidarityPensionReal + PensionByIndividualSavingsRegimeReal
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor total de deducciones
    ''' Suma de los valores calculados de Intereses por prestamos de vivienda, por dependientes y por pago de medicina prepagada
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property TotalDeductionsReal As Decimal
        Get
            Return InterestsReal + DependentReal + PreparedHealthReal
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor total de los ingresos exentos
    ''' Suma de los valores calculados de Aportes a fondos de pensiones voluntarias y aportes condestino a cuentas AFC
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property TotalIncomeExentReal As Decimal
        Get
            Dim value = VoluntaryContributionsReal + AccountContributionsReal
            Dim valueMaximumForIncome As Decimal = TotalIncomeMonthlyData * (30 / 100)
            Dim valueMaximumUVT As Decimal = 3800 * UVT / 12
            value = Presenter.ChooseValue(valueMaximumForIncome, value)
            value = Presenter.ChooseValue(valueMaximumUVT, value)

            'Información util a mostrar
            superToolTip = New DevExpress.Utils.SuperToolTip
            toolTipItemBody = New DevExpress.Utils.ToolTipItem
            toolTipItemBody.Text = "Tope 30% Ingreso: " + valueMaximumForIncome.ToString("C0")
            superToolTip.Items.Add(toolTipItemBody)

            toolTipItemBody = New DevExpress.Utils.ToolTipItem
            toolTipItemBody.Text = "Tope 3800 UVT al año: " + valueMaximumUVT.ToString("C0")
            superToolTip.Items.Add(toolTipItemBody)

            toolTipItemBody = New DevExpress.Utils.ToolTipItem
            toolTipItemBody.Text = "Valor Real: " + value.ToString("C0")
            superToolTip.Items.Add(toolTipItemBody)
            INDtxtTotalIncomeExent.SuperTip = superToolTip

            Return CDec(Utils.RoundValue(value, Utils.RoundLevel.Unit))
        End Get
    End Property

#End Region

#Region "Variables for calculations"

    ''' <summary>
    ''' Variable para saber si se limpia el control de concepto de pago o se deja como estaba en CtrConcepts
    ''' (True=Limpia, False=NoLimpia)
    ''' </summary>
    ''' <remarks></remarks>
    Public CleanConcept As Boolean

    ''' <summary>
    ''' Representa la entidad de detalle del detalle de la cxp
    ''' </summary>
    ''' <remarks></remarks>
    Public _accountPayableDetailConceptLiquidation As AccountPayableDetailConceptLiquidation

    ''' <summary>
    ''' Listado de rangos de retenciones 383
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListRangeRetention383 As List(Of RetentionConceptRanges)

    ''' <summary>
    ''' Representa a la entidad de parametros de empresa
    ''' </summary>
    ''' <remarks></remarks>
    Dim CompanySettings As CompanySettings

    ''' <summary>
    ''' Representa al presentador del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PLiquidator

    ''' <summary>
    ''' Controla el cambio de valores de las cajas de texto
    ''' </summary>
    ''' <remarks></remarks>
    Dim controlerChangedTextEdit As Boolean = False

    ''' <summary>
    ''' Obtiene la base gravable para mostrar en el control de conceptos
    ''' </summary>
    ''' <remarks></remarks>
    Public TaxBaseGeneral As Decimal
    ''' <summary>
    ''' Establece que retencion fue utilizada (0=Retencion383, 1=Retencion384)
    ''' </summary>
    ''' <remarks></remarks>
    Public TypeRetentionAplicated As Integer
    ''' <summary>
    ''' Valor de la retencion por el articulo 383
    ''' </summary>
    ''' <remarks></remarks>
    Public Retention383 As Decimal
    ''' <summary>
    ''' Valor de la retencion por el articulo 384
    ''' </summary>
    ''' <remarks></remarks>
    Public Retention384 As Decimal
    Public PercentageFinally As Decimal

    Private ValueModificatedExemptIncome As AccountPayableDetailConceptLiquidationValuesModificated = Nothing
    Private ValueModificatedExemptIncomeAndDeductions As AccountPayableDetailConceptLiquidationValuesModificated = Nothing

    '------- Variables para calculos ----------
    Dim UVT As Decimal
    Dim SMLV As Decimal
    Dim WorkIncomeControl As Boolean
    Dim HealthPercentage As Decimal = 12.5
    Dim PensionPercentage As Decimal = 16
    Dim SolidarityPensionPercentage As Decimal = 1
#End Region

#End Region

#Region "ICrud"

    Public Sub Buscar() Implements Base.ICrudBase.Buscar

    End Sub

    ''' <summary>
    ''' Deshace los cambios que hallan en el form
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer() Implements Base.ICrudBase.Deshacer
        CleanControls()
    End Sub

    Public Sub Eliminar() Implements Base.ICrudBase.Eliminar

    End Sub

    Public Sub Guardar() Implements Base.ICrudBase.Guardar

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Emite el mensaje en el slider
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
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

#Region "Methods"

    ''' <summary>
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        BarraBotones.PrepareToolbar(eAction.OnlyHideAudit)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Customizar) = False
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = False
        FeeCommissionServiceData = 0
        AccumulatedIncome = 0
        RequiredContributions = 0
        PaymentHealthObligatory = 0
        SolidarityPension = 0
        PensionByIndividualSavingsRegime = 0
        Interests = 0
        Dependent = 0
        PreparedHealth = 0
        RiskWork = 0
        VoluntaryContributions = 0
        AccountContributions = 0
        RetArt383 = 0
        PreviousRetArt383 = 0
        FinalRetention = 0
        TotalIncomeMonthlyData = 0
        TotalNoConstitutive = 0
        TotalDeductions = 0
        TotalIncomeExent = 0

    End Sub

    ''' <summary>
    ''' Metodo que calcula los totales
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CalculateAllTotals()
        TotalNoConstitutive = TotalNoConstitutiveReal
        TotalDeductions = TotalDeductionsReal
        TotalIncomeExent = VoluntaryContributions + AccountContributions
    End Sub

    ''' <summary>
    ''' Metodo que calcula los valores del popup del control
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CalculateValuesPopup()
        INDtxtPaymentMonthPopup.EditValue = TotalIncomeMonthlyData
        INDTxtPopUpNoConstitutivosRenta.EditValue = TotalNoConstitutiveReal
        INDtxtSubTotalAPopup.EditValue = INDtxtPaymentMonthPopup.EditValue - INDTxtPopUpNoConstitutivosRenta.EditValue
        INDtxtDeductionsPopup.EditValue = TotalDeductionsReal
        INDtxtRentExentPopup.EditValue = TotalIncomeExentReal
        INDtxtSubTotalBPopup.EditValue = INDtxtSubTotalAPopup.EditValue - INDtxtDeductionsPopup.EditValue - INDtxtRentExentPopup.EditValue
        ExemptIncomeCalculated = Presenter.CalculateLessRentsExentsPopup(INDtxtSubTotalBPopup.EditValue, UVT)
        If Me.ValueModificatedExemptIncome Is Nothing Then
            INDtxtMenosRents.EditValue = Presenter.CalculateExemptIncomeReal(ExemptIncomePrevious, ExemptIncomeCalculated, UVT)
        Else
            INDtxtMenosRents.EditValue = Me.ValueModificatedExemptIncome.NewValue
        End If
        INDtxtSubTotal3Popup.EditValue = INDtxtDeductionsPopup.EditValue + INDtxtRentExentPopup.EditValue + INDtxtMenosRents.EditValue
        ExemptIncomeAndDeductionsCalculated = Presenter.CalculateMaxDeductionsAndRentExents(INDtxtSubTotalAPopup.EditValue, INDtxtSubTotal3Popup.EditValue, UVT)
        If Me.ValueModificatedExemptIncomeAndDeductions Is Nothing Then
            INDtxtMaxDeductionsAndRentExentsPopup.EditValue = Presenter.CalculateExemptIncomeAndDeductionsReal(ExemptIncomeAndDeductionsPrevious, ExemptIncomeAndDeductionsCalculated, UVT)
        Else
            INDtxtMaxDeductionsAndRentExentsPopup.EditValue = Me.ValueModificatedExemptIncomeAndDeductions.NewValue
        End If
        INDtxtBasePopup.EditValue = Presenter.ValidarBaseRetenciones(INDtxtSubTotalAPopup.EditValue, INDtxtSubTotal3Popup.EditValue, INDtxtMaxDeductionsAndRentExentsPopup.EditValue)
        CalculateSubtotals()
    End Sub

    ''' <summary>
    ''' Metodo que calcula los valores de la sección Subtotal
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CalculateSubtotals()
        INDtxtSubtotal.EditValue = INDtxtSubTotalBPopup.EditValue
        INDtxExemptIncome25Percent.EditValue = INDtxtMenosRents.EditValue
        INDtxtExemptIncomeControl.EditValue = ExemptIncomePrevious + INDtxtMenosRents.EditValue
        INDtxtExemptIncomeAndDeductions.EditValue = INDtxtMaxDeductionsAndRentExentsPopup.EditValue
        INDtxtControlExemptIncomeAndDeductions.EditValue = ExemptIncomeAndDeductionsPrevious + INDtxtMaxDeductionsAndRentExentsPopup.EditValue
        INDtxtTaxBase.EditValue = INDtxtBasePopup.EditValue
    End Sub

    Private Function getValues() As Tuple(Of Decimal, Decimal, Decimal)
        Retention383 = Presenter.CalculateRetention383(INDtxtBasePopup.EditValue, UVT, ListRangeRetention383)
        RetArt383 = Retention383 'Asigno la propiedad-control el valor retener despues del calculo
        Retention384 = 0

        Dim totalRetentionPopup As Decimal = 0
        TypeRetentionAplicated = 0
        totalRetentionPopup = Retention383

        If INDtxtBasePopup.EditValue <> Nothing Then
            PercentageFinally = Presenter.CalculatePercentageFinallyValue(totalRetentionPopup, INDtxtPaymentMonthPopup.EditValue)
        End If

        Return New Tuple(Of Decimal, Decimal, Decimal)(totalRetentionPopup, Retention383, Retention384)
    End Function

    ''' <summary>
    ''' Metodo que asigna los valores
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValues()
        With _accountPayableDetailConceptLiquidation
            'Ingresos
            .TotalIncome = FeeCommissionServiceData
            'Ingresos no constitutivos de renta ni ganancia ocasional
            .PaymentCompulsoryHealth = PaymentHealthObligatory
            .PaymentCompulsoryHealthReal = PaymentHealthObligatoryReal
            .PensionFundContribution = RequiredContributions
            .PensionFundContributionReal = RequiredContributionsReal
            .SolidarityPensionFund = SolidarityPension
            .SolidarityPensionFundReal = SolidarityPensionReal
            .PensionByIndividualSavingsRegime = PensionByIndividualSavingsRegime
            .PensionByIndividualSavingsRegimeReal = PensionByIndividualSavingsRegimeReal
            'Deducciones
            .HousingLoanInterest = Interests
            .HousingLoanInterestReal = InterestsReal
            .PaymentForDependent = Dependent
            .PaymentForDependentReal = DependentReal
            .PaymentPrepaidMedical = PreparedHealth
            .PaymentPrepaidMedicalReal = PreparedHealthReal
            .OccupationalRiskContribution = RiskWork
            .OccupationalRiskContributionReal = RiskWorkReal
            'Total deducciones
            .TotalDeduction = Interests + Dependent + PreparedHealth
            .TotalDeductionReal = TotalDeductionsReal
            'Rentas Exentas
            .VoluntaryPensionFundContribution = VoluntaryContributions
            .VoluntaryPensionFundContributionReal = VoluntaryContributionsReal
            .ContributionAccountAFC = AccountContributions
            .ContributionAccountAFCReal = AccountContributionsReal
            'Total Rentas exentas
            .TotalIncomeExempt = TotalIncomeExent
            .TotalIncomeExemptReal = TotalIncomeExentReal
            'Renta Exenta del 25%
            .ExemptIncome = INDtxtMenosRents.EditValue
            'SubTotal antes de validar el tope de deducciones y rentas exentas
            .SubTotal = CDec(Utils.RoundValue(INDtxtSubTotalBPopup.EditValue - INDtxtMenosRents.EditValue, Utils.RoundLevel.Unit))
            'Valor máximo que podrá restarse por rentas exentas y deducciones
            .MaxDeductionsAndRentExents = INDtxtMaxDeductionsAndRentExentsPopup.EditValue
            'Base gravable
            .TaxableBase = INDtxtBasePopup.EditValue
            'Valores necesarios para los calculos
            .UVT = UVT
            .SMLV = SMLV
            .PreviousDeductionsForWithholdings = PreviousRetArt383
            .AccumulatedIncome = AccumulatedIncome

            If Me.ValueModificatedExemptIncome IsNot Nothing AndAlso Me.ValueModificatedExemptIncome.Id = 0 Then
                .AccountPayableDetailConceptLiquidationValuesModificated.Add(Me.ValueModificatedExemptIncome)
            End If
            If Me.ValueModificatedExemptIncomeAndDeductions IsNot Nothing AndAlso Me.ValueModificatedExemptIncomeAndDeductions.Id = 0 Then
                .AccountPayableDetailConceptLiquidationValuesModificated.Add(Me.ValueModificatedExemptIncomeAndDeductions)
            End If
        End With
    End Sub

    ''' <summary>
    ''' Obtiene el valor real y crea el superToolTip
    ''' </summary>
    ''' <param name="control"></param>
    ''' <remarks></remarks>
    Private Sub ShowValueReanInSuperToolTip(control As DevExpress.XtraEditors.BaseEdit, valueReal As Decimal)
        toolTipItemBody = New DevExpress.Utils.ToolTipItem
        toolTipItemBody.Text = "Valor Real: " + valueReal.ToString("C0")
        superToolTip.Items.Add(toolTipItemBody)
        control.SuperTip = superToolTip
    End Sub

    ''' <summary>
    ''' Carga los controles del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadControls()
        With _accountPayableDetailConceptLiquidation
            Me.ValueModificatedExemptIncome = .AccountPayableDetailConceptLiquidationValuesModificated.Where(Function(d) d.ConceptType = ExemptIncomeType.ExemptIncomeControl).LastOrDefault()
            Me.ValueModificatedExemptIncomeAndDeductions = .AccountPayableDetailConceptLiquidationValuesModificated.Where(Function(d) d.ConceptType = ExemptIncomeType.ExemptIncomeAndDeductionsControl).LastOrDefault()

            'Ingresos
            AccumulatedIncome = .AccumulatedIncome
            FeeCommissionServiceData = .TotalIncome
            TotalIncomeMonthlyData = .TotalIncome
            'Ingresos no constitutivos de renta ni ganancia ocasional
            PaymentHealthObligatory = .PaymentCompulsoryHealth
            ShowValueReanInSuperToolTip(INDSPPaymentHealthObligatory, .PaymentCompulsoryHealthReal)
            RequiredContributions = .PensionFundContribution
            ShowValueReanInSuperToolTip(INDspPensionContribution, .PensionFundContributionReal)
            SolidarityPension = .SolidarityPensionFund
            PensionByIndividualSavingsRegime = .PensionByIndividualSavingsRegime
            'Deducciones
            Interests = .HousingLoanInterest
            ShowValueReanInSuperToolTip(INDtxtInterests, .HousingLoanInterestReal)
            Dependent = .PaymentForDependent
            ShowValueReanInSuperToolTip(INDspDependent, .PaymentForDependentReal)
            PreparedHealth = .PaymentPrepaidMedical
            ShowValueReanInSuperToolTip(INDtxtPreparedHealth, .PaymentPrepaidMedicalReal)
            RiskWork = .OccupationalRiskContribution
            'Total deducciones
            TotalDeductions = .TotalDeductionReal
            'Rentas Exentas
            VoluntaryContributions = .VoluntaryPensionFundContribution
            AccountContributions = .ContributionAccountAFC
            'Total Rentas exentas
            TotalIncomeExent = .TotalIncomeExempt
            ShowValueReanInSuperToolTip(INDtxtTotalIncomeExent, .TotalIncomeExemptReal)
            'Renta Exenta del 25%
            INDtxtMenosRents.EditValue = .ExemptIncome
            'SubTotal antes de validar el tope de deducciones y rentas exentas
            'Valor máximo que podrá restarse por rentas exentas y deducciones
            INDtxtMaxDeductionsAndRentExentsPopup.EditValue = .MaxDeductionsAndRentExents
            'Base gravable
            INDtxtBasePopup.EditValue = .TaxableBase
            PreviousRetArt383 = .PreviousDeductionsForWithholdings
            'Valores necesarios para los calculos
            If .UVT Is Nothing Then
                UVT = CompanySettings.UVT
                SMLV = CompanySettings.SMLV
            Else
                UVT = .UVT
                SMLV = .SMLV
            End If
        End With
    End Sub

    ''' <summary>
    ''' Obtiene retenciones acumuladas previas del tercero para topes de rentas exentas (incluidas el 25%) y deducciones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetViewThirdPartyRetentionAccumulated()
        If ViewRetentionAccumulated Is Nothing Then
            ViewRetentionAccumulated = Presenter.GetViewThirdPartyRetentionAccumulated(ThirdPartyId, FiscalYear)
            If ViewRetentionAccumulated Is Nothing Then
                ViewRetentionAccumulated = New Infrastructure.Data.Xpo.PaymentsRepository.ViewThirdPartyRetentionAccumulatedXpo
            End If
        End If

        ExemptIncomePrevious = ViewRetentionAccumulated.ExemptIncome
        ExemptIncomeAndDeductionsPrevious = ViewRetentionAccumulated.MaxDeductionsAndRentExents
        If OriginStatus < 2 Then
            For Each item In _accountPayableDetailConceptLiquidation.AccountPayableDetailConceptLiquidationAdjusments
                If item.ConceptType = ExemptIncomeType.ExemptIncomeControl Then
                    ExemptIncomePrevious = ExemptIncomePrevious + (If(item.Nature = 1, 1, -1) * item.Value)
                Else
                    ExemptIncomeAndDeductionsPrevious = ExemptIncomeAndDeductionsPrevious + (If(item.Nature = 1, 1, -1) * item.Value)
                End If
            Next
        End If

        CalculateSubtotals()
    End Sub

    ''' <summary>
    ''' Agrega el registro al objeto del control de usuario del concepto
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddInformationInCtrConcept()
        If FeeCommissionServiceData = Nothing OrElse Not (FeeCommissionServiceData > 0) Then
            Mensaje(Base.EeventViewerImages.Advertencia) = "El valor de Honorarios, Comisiones o Servicios no puede ser cero."
            INDtxtFeeCommissionServiceData.Focus()
            Exit Sub
        End If

        If TaxBaseGeneral < 0 Then
            Mensaje(Base.EeventViewerImages.Advertencia) = "La base gravable es inválida, por favor verifique la información ingresada."
            INDtxtFeeCommissionServiceData.Focus()
            Exit Sub
        End If

        AssigningValues()
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub OpenPopupEditExemtIncome(_valueModificated As AccountPayableDetailConceptLiquidationValuesModificated)
        Using formulario As New Controls.FrmPopupEditExemptIncome(_valueModificated)
            formulario.StartPosition = FormStartPosition.CenterParent
            Dim transparent As New FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            AddHandler formulario.AddValuesModificated, AddressOf ReturnAddValuesModificated
            transparent.ShowDialog(Me)
        End Using
    End Sub

    Private Sub ReturnAddValuesModificated(sender As Object, e As AddValuesModificatedEventArgs)
        If e.ValueModificated.ConceptType = ExemptIncomeType.ExemptIncomeControl Then
            Me.ValueModificatedExemptIncome = e.ValueModificated
        Else
            Me.ValueModificatedExemptIncomeAndDeductions = e.ValueModificated
        End If
        CalculateValuesPopup()
    End Sub

    Private Sub OpenPopUpInfoControl(Source As ExemptIncomeType)
        Using formulario As New Controls.FrmPopupExemptIncomeDetail(Presenter, ThirdPartyId, FiscalYear, Source, DocumentDate)
            formulario.OriginStatus = OriginStatus
            formulario.OriginCode = OriginCode
            formulario.BillNumber = BillNumber
            formulario.CreationDate = CreationDate
            formulario.StartPosition = FormStartPosition.CenterParent
            Dim transparent As New FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            formulario.ListConceptLiquidationAdjustment = _accountPayableDetailConceptLiquidation.AccountPayableDetailConceptLiquidationAdjusments.Where(Function(a) a.ConceptType = Source).ToList()
            AddHandler formulario.AddConceptLiquidationAdjustment, AddressOf ReturnAddConceptLiquidationAdjustment
            AddHandler formulario.RemoveConceptLiquidationAdjustment, AddressOf ReturnRemoveConceptLiquidationAdjustment
            transparent.ShowDialog(Me)
        End Using
    End Sub

    Private Sub ReturnAddConceptLiquidationAdjustment(sender As Object, e As AddAdjustmentsEventArgs)
        _accountPayableDetailConceptLiquidation.AccountPayableDetailConceptLiquidationAdjusments.Add(e.NewAdjustment)
        GetViewThirdPartyRetentionAccumulated()
    End Sub

    Private Sub ReturnRemoveConceptLiquidationAdjustment(sender As Object, e As AddAdjustmentsEventArgs)
        Dim adjustment = e.NewAdjustment
        If adjustment.Id <> 0 Then
            adjustment = _accountPayableDetailConceptLiquidation.AccountPayableDetailConceptLiquidationAdjusments.First(Function(d) d.Id = adjustment.Id)
            adjustment.MarkAsDeleted()
        Else
            _accountPayableDetailConceptLiquidation.AccountPayableDetailConceptLiquidationAdjusments.Remove(adjustment)
        End If

        GetViewThirdPartyRetentionAccumulated()
    End Sub

#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' Evento que se dispara al cargar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmLiquidator_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ActionsOnControls = False
        If _accountPayableDetailConceptLiquidation Is Nothing Then
            Deshacer()
            Presenter.GetPreviousValues(ThirdPartyId, DocumentDate)
            _accountPayableDetailConceptLiquidation = New AccountPayableDetailConceptLiquidation
        End If
        BarraBotones.PrepareToolbar(eAction.OnlyHideAudit)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Customizar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = False
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara cuando se abre el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmLiquidator_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        GetViewThirdPartyRetentionAccumulated()
        INDtxtFeeCommissionServiceData.Focus()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmLiquidator_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.Close()
        End If
    End Sub

    Private Sub INDtxtFeeCommissionServiceData_KeyDown(sender As Object, e As KeyEventArgs) Handles INDtxtFeeCommissionServiceData.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            Me.ActionsOnControls = True
        End If
    End Sub

    Public WriteOnly Property ActionsOnControls As Boolean
        Set(value As Boolean)
            INDlyLiquidator.BeginUpdate()

            INDspPensionContribution.Enabled = value
            INDSPPaymentHealthObligatory.Enabled = value
            INDspSolidarityPension.Enabled = value
            INDtxtInterests.Enabled = value
            INDspDependent.Enabled = value
            INDtxtPreparedHealth.Enabled = value
            INDspRiskWork.Enabled = value
            INDspVoluntaryContributions.Enabled = value
            INDspPensionByIndividualSavingsRegime.Enabled = value
            INDspAccountContributions.Enabled = value
            INDSbEditExemptIncome.Enabled = value
            INDSbEditExemptIncomeAndDeductions.Enabled = value
            'INDspPensionContribution.Focus()

            INDlyLiquidator.EndUpdate()
        End Set
    End Property

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de honorarios, comisiones o servicios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtxtFeeCommissionServiceData_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtFeeCommissionServiceData.EditValueChanged
        TotalIncomeMonthlyData = FeeCommissionServiceData + AccumulatedIncome
        Dim SolidarityPensionValue = (TotalIncomeMonthlyData * 0.4)
        Dim SpokenEntryInSMMLV = Math.Round((SolidarityPensionValue / SMLV), 1, MidpointRounding.AwayFromZero)
        Select Case True
            Case SpokenEntryInSMMLV >= 4 AndAlso SpokenEntryInSMMLV < 16
                SolidarityPensionPercentage = 1
            Case SpokenEntryInSMMLV >= 16 AndAlso SpokenEntryInSMMLV < 17
                SolidarityPensionPercentage = 1.2
            Case SpokenEntryInSMMLV >= 17 AndAlso SpokenEntryInSMMLV < 18
                SolidarityPensionPercentage = 1.4
            Case SpokenEntryInSMMLV >= 18 AndAlso SpokenEntryInSMMLV < 19
                SolidarityPensionPercentage = 1.6
            Case SpokenEntryInSMMLV >= 19 AndAlso SpokenEntryInSMMLV < 20
                SolidarityPensionPercentage = 1.8
            Case SpokenEntryInSMMLV >= 20
                SolidarityPensionPercentage = 2
        End Select
        'Cálculo de los aportes obligatorios
        RequiredContributions = Presenter.CalculateRequiredContributions(TotalIncomeMonthlyData, SMLV, PensionPercentage)
        SolidarityPension = If(SolidarityPensionValue >= (SMLV * 4), Presenter.CalculateRequiredContributions(TotalIncomeMonthlyData, SMLV, SolidarityPensionPercentage), 0)
        PaymentHealthObligatory = Presenter.CalculateRequiredContributions(TotalIncomeMonthlyData, SMLV, HealthPercentage)

        'Recalculo de totales y actualización de resultados
        CalculateAllTotals()
        CalculateValuesPopup()
        ctrTmp.RefreshTotalValues()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor de los controles de pension obligatoria
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDspPensionContribution_EditValueChanged(sender As Object, e As EventArgs) Handles INDspPensionContribution.EditValueChanged, INDspSolidarityPension.EditValueChanged, INDspPensionByIndividualSavingsRegime.EditValueChanged
        TotalNoConstitutive = TotalNoConstitutiveReal
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de pagos salud obligatoria
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSPPaymentHealthObligatory_EditValueChanged(sender As Object, e As EventArgs) Handles INDSPPaymentHealthObligatory.EditValueChanged
        TotalNoConstitutive = TotalNoConstitutiveReal
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor total correspondiente a los INCRNGO
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtxtTotalNoConstitutive_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtTotalNoConstitutive.EditValueChanged
        If TotalIncomeMonthlyData < TotalNoConstitutive Then
            Mensaje(Base.EeventViewerImages.Advertencia) = "El Total de Ingresos No Constitutivos no puede ser mayor al Total de Ingreso del Mes"
            Return
        End If

        CalculateValuesPopup()
        ctrTmp.RefreshTotalValues()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de pagos por intereses por prestamos de vivienda
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtxtInterests_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtInterests.EditValueChanged
        TotalDeductions = TotalDeductionsReal
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de Dependientes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDspDependent_EditValueChanged(sender As Object, e As EventArgs) Handles INDspDependent.EditValueChanged
        TotalDeductions = TotalDeductionsReal
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de medicina prepagada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtxtPreparedHealth_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtPreparedHealth.EditValueChanged
        TotalDeductions = TotalDeductionsReal
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de riesgos laborales
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDspRiskWork_EditValueChanged(sender As Object, e As EventArgs) Handles INDspRiskWork.EditValueChanged
        'Actualmente no aplica
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor total correspondiente a las deducciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtxtTotalDeductions_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtTotalDeductions.EditValueChanged
        If TotalIncomeMonthlyData < TotalDeductionsReal Then
            Mensaje(Base.EeventViewerImages.Advertencia) = "El Total de Deducciones no puede ser mayor al Total de Ingreso del Mes"
            Return
        End If

        CalculateValuesPopup()
        ctrTmp.RefreshTotalValues()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de aportes a fondos de pensiones voluntarias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDspVoluntaryContributions_EditValueChanged(sender As Object, e As EventArgs) Handles INDspVoluntaryContributions.EditValueChanged
        TotalIncomeExent = VoluntaryContributions + AccountContributions
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de aportes con destino a cuentas AFC
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDspAccountContributions_EditValueChanged(sender As Object, e As EventArgs) Handles INDspAccountContributions.EditValueChanged
        TotalIncomeExent = VoluntaryContributions + AccountContributions
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor total correspondiente a las rentas exentas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtxtTotalIncomeExent_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtTotalIncomeExent.EditValueChanged, INDtxtTotalIncomeExent.EditValueChanged
        If TotalIncomeMonthlyData < TotalIncomeExentReal Then
            Mensaje(Base.EeventViewerImages.Advertencia) = "El Total de Rentas Exentas no puede ser mayor al Total de Ingreso del Mes"
            Return
        End If

        CalculateValuesPopup()
        ctrTmp.RefreshTotalValues()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor de la base gravable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtxtBasePopup_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtBasePopup.EditValueChanged
        TaxBaseGeneral = INDtxtBasePopup.EditValue
        ctrTmp.RefreshTotalValues()
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton de agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddRetention_Click(sender As Object, e As EventArgs) Handles INDbtnAddRetention.Click
        AddInformationInCtrConcept()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click en el ver detalle (+Info) de Control Renta Exenta 25%
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSbInfoExemptIncomeControl_Click(sender As Object, e As EventArgs) Handles INDSbInfoExemptIncomeControl.Click
        Me.OpenPopUpInfoControl(ExemptIncomeType.ExemptIncomeControl)
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click en el ver detalle (+Info) de Control Rentas Exentas y Deducciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSbInfoExemptIncomeAndDeductionsControl_Click(sender As Object, e As EventArgs) Handles INDSbInfoExemptIncomeAndDeductionsControl.Click
        Me.OpenPopUpInfoControl(ExemptIncomeType.ExemptIncomeAndDeductionsControl)
    End Sub

    Private Sub INDSbEditExemptIncome_Click(sender As Object, e As EventArgs) Handles INDSbEditExemptIncome.Click
        Dim _valueModificated = New AccountPayableDetailConceptLiquidationValuesModificated
        _valueModificated.ConceptType = ExemptIncomeType.ExemptIncomeControl
        _valueModificated.PreviousValue = INDtxExemptIncome25Percent.EditValue
        _valueModificated.NewValue = INDtxExemptIncome25Percent.EditValue

        If Me.ValueModificatedExemptIncome IsNot Nothing AndAlso Me.ValueModificatedExemptIncome.Id = 0 Then
            _valueModificated = Me.ValueModificatedExemptIncome
        End If

        Me.OpenPopupEditExemtIncome(_valueModificated)
    End Sub

    ''' <summary>
    ''' Abre el modal detalles de retención
    ''' </summary>
    Private Sub OpenpopUpExemptIncomeDetail(flagCall As Integer)
        Using formulario As New FrmPopupExemptIncomeDetail(Presenter, ThirdPartyId)
            formulario.DocumentDate = DocumentDate
            formulario.CreationDate = CreationDate
            formulario.flagCall = flagCall
            formulario.StartPosition = FormStartPosition.CenterParent
            Dim transparent As New FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    Private Sub INDSbEditExemptIncomeAndDeductions_Click(sender As Object, e As EventArgs) Handles INDSbEditExemptIncomeAndDeductions.Click
        Dim _valueModificated = New AccountPayableDetailConceptLiquidationValuesModificated
        _valueModificated.ConceptType = ExemptIncomeType.ExemptIncomeAndDeductionsControl
        _valueModificated.PreviousValue = INDtxtExemptIncomeAndDeductions.EditValue
        _valueModificated.NewValue = INDtxtExemptIncomeAndDeductions.EditValue

        If Me.ValueModificatedExemptIncomeAndDeductions IsNot Nothing AndAlso Me.ValueModificatedExemptIncomeAndDeductions.Id = 0 Then
            _valueModificated = Me.ValueModificatedExemptIncomeAndDeductions
        End If

        Me.OpenPopupEditExemtIncome(_valueModificated)
    End Sub

    ''' <summary>
    ''' clic boton +info ingresos acumuldos del mes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSbInfoIncomeByMonth_Click(sender As Object, e As EventArgs) Handles INDSbInfoIncomeByMonth.Click
        OpenpopUpExemptIncomeDetail(2)
    End Sub

    ''' <summary>
    ''' clic boton +info 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSbPrevoiusRetentions_Click(sender As Object, e As EventArgs) Handles INDSbPrevoiusRetentions.Click
        OpenpopUpExemptIncomeDetail(3)
    End Sub

    ''' <summary>
    ''' Si dispara cuando cambie el valor de retención calculado, actualiza la retención final
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDtxtRetArt383_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtRetArt383.EditValueChanged, INDtxtPreviousDeductions.EditValueChanged
        FinalRetention = (RetArt383 - PreviousRetArt383) * 1
    End Sub

    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()

    End Sub


#End Region

#End Region

End Class