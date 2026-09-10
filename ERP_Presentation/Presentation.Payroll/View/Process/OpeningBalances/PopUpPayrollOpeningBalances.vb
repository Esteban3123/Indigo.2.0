'***********************************************************************
' Assembly         : Presentacion.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 08-09-2014
'
' Last Modified By : Carlos Mario Arias Rubiano
' Last Modified On : 10/05/2017
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Payroll.MVP
Imports Domain.Payroll.Entities
Imports System.Drawing
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Common
Imports Presentation.Controls
Imports Domain.Payroll
Imports Infrastructure.CrossCutting.Base
Imports DevExpress.XtraLayout
Imports System.Text
Imports Domain.Entities

#End Region

Public Class PopUpPayrollOpeningBalances
    Implements IPayrollOpeningBalances

#Region "Builder"

    Public Sub New(Optional _isOpenSinceInitialBalance As Boolean = False)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        'Se asigna el valor para saber desde donde se esta abriendo el form
        IsOpenSinceInitialBalancePayroll = _isOpenSinceInitialBalance
    End Sub

#End Region

#Region "Variables"

    ''' <summary>
    ''' Modelo
    ''' </summary>
    Dim LiquidationModel As MPayrollLiquidation

    ''' <summary>
    ''' Tabla grupo
    ''' </summary>
    Dim Group As Group

    ''' <summary>
    ''' Entidad del store para poder enviar a guardar con el sp de saldo inicial
    ''' </summary>
    Private EntityStore As SP_ImportFileInitialBalancePayroll_Result

    ''' <summary>
    ''' Tabla empleado
    ''' </summary>
    Dim Employee As Domain.Payroll.Entities.Employee

    ''' <summary>
    ''' Tabla contrato
    ''' </summary>
    Dim ContractValid As Domain.Payroll.Entities.Contract

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Private Presenter As PPayrollOpeningBalances

    ''' <summary>
    ''' Me permite saber si el form se abrió desde el menú o desde el formulario de saldo inicial
    ''' </summary>
    Private IsOpenSinceInitialBalancePayroll As Boolean

    ''' <summary>
    ''' Listado de controles textEdit y searchLookUpEdit
    ''' </summary>
    Private ListControlsTextAndSearch As List(Of Object)

#End Region

#Region "Event"

    ''' <summary>
    ''' Evento para asignar la entidad del form a la rejilla de saldo inicial
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Public Event AddEntityStoreProcedureEventArgs(sender As Object, e As AddEntityStoreProcedure)

#End Region

#Region "Properties"

    ''' <summary>
    ''' Propiedad que contiene el listado de empleados
    ''' </summary>
    Public Property EmployeeXpo As DevExpress.Data.Linq.LinqInstantFeedbackSource Implements IPayrollOpeningBalances.EmployeeXpo
        Get
            Return INDsleEmployees.Properties.DataSource
        End Get
        Set(value As DevExpress.Data.Linq.LinqInstantFeedbackSource)
            INDsleEmployees.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Asigna el nombre del form
    ''' </summary>
    Private _textForm As String
    Public WriteOnly Property TextForm As String
        Set(value As String)
            Me.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Id del empleado
    ''' </summary>
    ''' <returns></returns>
    Public Property EmployeeId As Integer Implements IPayrollOpeningBalances.EmployeeId
        Get
            Return INDsleEmployees.EditValue
        End Get
        Set(value As Integer)
            INDsleEmployees.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Listado para validar al empleado
    ''' </summary>
    Private _listValidate As List(Of SP_ImportFileInitialBalancePayroll_Result)
    Public Property ListValidate As List(Of SP_ImportFileInitialBalancePayroll_Result)
        Get
            Return _listValidate
        End Get
        Set(value As List(Of SP_ImportFileInitialBalancePayroll_Result))
            _listValidate = value
        End Set
    End Property
    ''' <summary>
    ''' Indica la moneda oficial
    ''' </summary>
    Private _currencyAbbreviation As String
    Property CurrencyAbbreviation As String
        Get
            Return _currencyAbbreviation
        End Get
        Set(value As String)
            _currencyAbbreviation = value
        End Set
    End Property

#End Region

#Region "ICrud"

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.IcrudBase.Mensaje
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

    Public Sub Buscar() Implements Base.IcrudBase.Buscar

    End Sub

    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer

    End Sub

    Public Sub Eliminar() Implements Base.IcrudBase.Eliminar

    End Sub

    Public Sub Guardar() Implements Base.IcrudBase.Guardar

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar

    End Sub

    Public Sub Nuevo() Implements Base.IcrudBase.Nuevo

    End Sub

    Public Sub OpenSearch() Implements Base.IcrudBase.OpenSearch

    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Asigna los valores a la entidad
    ''' </summary>
    Private Sub AssignValues()
        EntityStore = New SP_ImportFileInitialBalancePayroll_Result
        With EntityStore
            .Nit = Employee.ThirdParty.Nit
            .NitName = Employee.ThirdParty.Nit + " - " + Employee.ThirdParty.Name
            .EmployeeId = Employee.Id
            .PayrollDate = INDDePayrollPeriod.EditValue.ToString
            .DaysWorked = INDTxtWorkDays.EditValue
            .TotalAccrued = INDTxtTotalAccrued.EditValue
            .TotalDeducted = INDTxtTodalDeducted.EditValue
            .TotalPaid = INDTxtTotalPaid.EditValue
            .PensionJCB = INDTxtPensionIBC.EditValue
            .PensionContributionValue = INDTxtPensionContributionEmployee.EditValue
            .ConceptIdPensionContributionValue = INDsleConceptPensionContributionValue.EditValue
            .EmployerPensionContributionValue = INDTxtPensionContributionEmployer.EditValue
            .ConceptIdEmployerPensionContributionValue = INDsleConceptEmployerPensionContributionValue.EditValue
            .HealthJCB = INDTxtHealthIBC.EditValue
            .EmployeeHealthContributionValue = INDTxtHealthContributionEmployee.EditValue
            .ConceptIdEmployeeHealthContributionValue = INDsleConceptEmployeeHealthContributionValue.EditValue
            .EmployerHealthContributionValue = INDTxtHealthContributionEmployer.EditValue
            .ConceptIdEmployerHealthContributionValue = INDsleConceptEmployerHealthContributionValue.EditValue
            .IBCIncentivePayment = INDTxtIncentiveIBC.EditValue
            .ProvisionIncentive = INDTxtIncentiveProvision.EditValue
            .ConceptIdProvisionIncentive = INDsleConceptProvisionIncentive.EditValue
            .IBCVacation = INDTxtVacationIBC.EditValue
            .ProvisionVacation = INDTxtProvisionVacation.EditValue
            .ConceptIdProvisionVacation = INDsleConceptProvisionVacation.EditValue
            .IBCUnemployment = INDTxtUnemploymentIBC.EditValue
            .UnemploymentAccumulated = INDTxtUnemployementProvision.EditValue
            .ConceptIdUnemploymentAccumulated = INDsleConceptUnemploymentAccumulated.EditValue
            .ProvisionInterestsUnemployment = INDTxtUnemployedInterestProvision.EditValue
            .ConceptIdProvisionInterestsUnemployment = INDsleConceptProvisionInterestsUnemployment.EditValue
            .AmbulatoryDisabilityValue = INDTxtAmbulatoryInabilityValue.EditValue
            .ConceptIdAmbulatoryDisabilityValue = INDsleConceptAmbulatoryDisabilityValue.EditValue
            .DisabilityHospitalValue = INDTxtHospitalInabilityValue.EditValue
            .ConceptIdDisabilityHospitalValue = INDsleConceptDisabilityHospitalValue.EditValue
            .MaternityLeaveValue = INDTxtMaternityInabilityValue.EditValue
            .ConceptIdMaternityLeaveValue = INDsleConceptMaternityLeaveValue.EditValue
            .IBCSENA = INDTxtSenaIBC.EditValue
            .SenaContributionValue = INDtxtSenaValue.EditValue
            .ConceptIdSenaContributionValue = INDsleConceptSenaContributionValue.EditValue
            .IBCICBF = INDTxtICBFIBC.EditValue
            .ICBFContributionValue = INDtxtICBFValue.EditValue
            .ConceptIdICBFContributionValue = INDsleConceptICBFContributionValue.EditValue
            .IBCCompensationFund = INDtxtCajaIBC.EditValue
            .FamilyCompensationFundContributionValue = INDtxtCajaValue.EditValue
            .ConceptIdFamilyCompensationFundContributionValue = INDsleConceptFamilyCompensationFundContributionValue.EditValue
            .RealBaseRetention = INDTxtBaseRetention.EditValue
            .CalculatedWithholdingValue = INDTxtRetentionValue.EditValue
            .ConceptIdCalculatedWithholdingValue = INDsleConceptCalculatedWithholdingValue.EditValue
            .RecargoNocturno = INDtxtRecargoNocturno.EditValue
            .ConceptIdRecargoNocturno = INDsleConceptRecargoNocturno.EditValue
            .SanctionDays = INDtxtSanctionDays.EditValue
            .Overtime = INDtxtOvertime.EditValue
            .ConceptIdOvertime = INDsleConceptOvertime.EditValue
        End With
    End Sub

    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    Private Function ValidateInformation() As String
        'Listado de errores
        Dim errors As New StringBuilder

        If _listValidate IsNot Nothing AndAlso _listValidate.Count > 0 Then 'Se valida que el empleado no este en el listado
            If (From x In _listValidate Where x.EmployeeId = INDsleEmployees.EditValue AndAlso CDate(x.PayrollDate).Month = CDate(INDDePayrollPeriod.EditValue).Month AndAlso CDate(x.PayrollDate).Year = CDate(INDDePayrollPeriod.EditValue).Year Select x).Count > 0 Then
                errors.AppendLine("La cédula del empleado " + Employee.ThirdParty.Nit + " ya existe en la lista con el mes " + CDate(INDDePayrollPeriod.EditValue).Month.ToString + " y el año " + CDate(INDDePayrollPeriod.EditValue).Year.ToString)
                Return errors.ToString
            End If
        End If

        If INDsleEmployees.EditValue Is Nothing OrElse INDsleEmployees.EditValue = 0 Then 'Si el empleado está vacio
            errors.AppendLine("Debe seleccionar un empleado")
        End If

        If INDDePayrollPeriod.EditValue Is Nothing Then 'Si la fecha de nómina esta vacia
            errors.AppendLine("Debe seleccionar una fecha nómina")
        Else 'Si no esta vacia la fecha
            Dim dateValidate As Date = INDDePayrollPeriod.EditValue
            dateValidate = New Date(CDate(INDDePayrollPeriod.EditValue).Year, CDate(INDDePayrollPeriod.EditValue).Month, 1)
            If CDate(INDDePayrollPeriod.EditValue).Day <> dateValidate.AddMonths(1).AddDays(-1).Day Then 'Si la fecha no tiene seleccionada el ultimo dia del mes
                errors.AppendLine("Debe seleccionar en la fecha nómina el último día del mes")
            End If
        End If

        If INDTxtWorkDays.EditValue Is Nothing OrElse INDTxtWorkDays.EditValue = 0 Then 'Si los dias trabajados viene vacio
            errors.AppendLine("Debe ingresar los días trabajados")
        Else
            If INDTxtWorkDays.EditValue = 0 OrElse INDTxtWorkDays.EditValue > 30 Then 'Si los dias trabajados son mayores a 30
                errors.AppendLine("Los días trabajados debe ser mayor a 0 y menor o igual 30")
            End If
        End If

        If INDTxtTotalAccrued.EditValue Is Nothing OrElse INDTxtTotalAccrued.EditValue = 0 Then 'Si el total devengado viene vacio
            errors.AppendLine("Debe ingresar el total devengado")
        End If

        'Se comenta esta validacion ya que el total deducido puede ir en cero
        'If INDTxtTodalDeducted.EditValue Is Nothing OrElse INDTxtTodalDeducted.EditValue = 0 Then 'Si el total deducido viene vacio
        '    errors.AppendLine("Debe ingresar el total deducido")
        'End If

        If INDTxtPensionContributionEmployee.EditValue IsNot Nothing AndAlso INDTxtPensionContributionEmployee.EditValue > 0 Then 'Si viene lleno se valida el concepto
            If INDsleConceptPensionContributionValue.EditValue Is Nothing OrElse INDsleConceptPensionContributionValue.EditValue = 0 Then 'Si el concepto viene vacio
                errors.AppendLine("El concepto de aporte pensión empleado está vacío")
            End If
        End If

        If INDTxtPensionContributionEmployer.EditValue IsNot Nothing AndAlso INDTxtPensionContributionEmployer.EditValue > 0 Then 'Si viene lleno se valida el concepto
            If INDsleConceptEmployerPensionContributionValue.EditValue Is Nothing OrElse INDsleConceptEmployerPensionContributionValue.EditValue = 0 Then 'Si el concepto viene vacio
                errors.AppendLine("El concepto de aporte pensión patrono está vacío")
            End If
        End If

        If INDTxtHealthContributionEmployee.EditValue IsNot Nothing AndAlso INDTxtHealthContributionEmployee.EditValue > 0 Then 'Si viene lleno se valida el concepto
            If INDsleConceptEmployeeHealthContributionValue.EditValue Is Nothing OrElse INDsleConceptEmployeeHealthContributionValue.EditValue = 0 Then 'Si el concepto viene vacio
                errors.AppendLine("El concepto de aporte salud empleado está vacío")
            End If
        End If

        If INDTxtHealthContributionEmployer.EditValue IsNot Nothing AndAlso INDTxtHealthContributionEmployer.EditValue > 0 Then 'Si viene lleno se valida el concepto
            If INDsleConceptEmployerHealthContributionValue.EditValue Is Nothing OrElse INDsleConceptEmployerHealthContributionValue.EditValue = 0 Then 'Si el concepto viene vacio
                errors.AppendLine("El concepto de aporte salud patrono está vacío")
            End If
        End If

        If INDTxtIncentiveProvision.EditValue IsNot Nothing AndAlso INDTxtIncentiveProvision.EditValue > 0 Then 'Si viene lleno se valida el concepto
            If INDsleConceptProvisionIncentive.EditValue Is Nothing OrElse INDsleConceptProvisionIncentive.EditValue = 0 Then 'Si el concepto viene vacio
                errors.AppendLine("El concepto de provisión primas está vacío")
            End If
        End If

        If INDTxtProvisionVacation.EditValue IsNot Nothing AndAlso INDTxtProvisionVacation.EditValue > 0 Then 'Si viene lleno se valida el concepto
            If INDsleConceptProvisionVacation.EditValue Is Nothing OrElse INDsleConceptProvisionVacation.EditValue = 0 Then 'Si el concepto viene vacio
                errors.AppendLine("El concepto de provisión vacaciones está vacío")
            End If
        End If

        If INDTxtUnemployementProvision.EditValue IsNot Nothing AndAlso INDTxtUnemployementProvision.EditValue > 0 Then 'Si viene lleno se valida el concepto
            If INDsleConceptUnemploymentAccumulated.EditValue Is Nothing OrElse INDsleConceptUnemploymentAccumulated.EditValue = 0 Then 'Si el concepto viene vacio
                errors.AppendLine("El concepto de provisión cesantías está vacío")
            End If
        End If

        If INDTxtUnemployedInterestProvision.EditValue IsNot Nothing AndAlso INDTxtUnemployedInterestProvision.EditValue > 0 Then 'Si viene lleno se valida el concepto
            If INDsleConceptProvisionInterestsUnemployment.EditValue Is Nothing OrElse INDsleConceptProvisionInterestsUnemployment.EditValue = 0 Then 'Si el concepto viene vacio
                errors.AppendLine("El concepto de provisión cesantías está vacío")
            End If
        End If

        If INDTxtAmbulatoryInabilityValue.EditValue IsNot Nothing AndAlso INDTxtAmbulatoryInabilityValue.EditValue > 0 Then 'Si viene lleno se valida el concepto
            If INDsleConceptAmbulatoryDisabilityValue.EditValue Is Nothing OrElse INDsleConceptAmbulatoryDisabilityValue.EditValue = 0 Then 'Si el concepto viene vacio
                errors.AppendLine("El concepto de valor incapacidad ambulatoria está vacío")
            End If
        End If

        If INDTxtHospitalInabilityValue.EditValue IsNot Nothing AndAlso INDTxtHospitalInabilityValue.EditValue > 0 Then 'Si viene lleno se valida el concepto
            If INDsleConceptDisabilityHospitalValue.EditValue Is Nothing OrElse INDsleConceptDisabilityHospitalValue.EditValue = 0 Then 'Si el concepto viene vacio
                errors.AppendLine("El concepto de valor incapacidad hospitalaria está vacío")
            End If
        End If

        If INDTxtMaternityInabilityValue.EditValue IsNot Nothing AndAlso INDTxtMaternityInabilityValue.EditValue > 0 Then 'Si viene lleno se valida el concepto
            If INDsleConceptMaternityLeaveValue.EditValue Is Nothing OrElse INDsleConceptMaternityLeaveValue.EditValue = 0 Then 'Si el concepto viene vacio
                errors.AppendLine("El concepto de valor licencia maternidad está vacío")
            End If
        End If

        If INDtxtSenaValue.EditValue IsNot Nothing AndAlso INDtxtSenaValue.EditValue > 0 Then 'Si viene lleno se valida el concepto
            If INDsleConceptSenaContributionValue.EditValue Is Nothing OrElse INDsleConceptSenaContributionValue.EditValue = 0 Then 'Si el concepto viene vacio
                errors.AppendLine("El concepto de aporte sena está vacío")
            End If
        End If

        If INDtxtICBFValue.EditValue IsNot Nothing AndAlso INDtxtICBFValue.EditValue > 0 Then 'Si viene lleno se valida el concepto
            If INDsleConceptICBFContributionValue.EditValue Is Nothing OrElse INDsleConceptICBFContributionValue.EditValue = 0 Then 'Si el concepto viene vacio
                errors.AppendLine("El concepto de aporte ICBF está vacío")
            End If
        End If

        If INDtxtCajaValue.EditValue IsNot Nothing AndAlso INDtxtCajaValue.EditValue > 0 Then 'Si viene lleno se valida el concepto
            If INDsleConceptFamilyCompensationFundContributionValue.EditValue Is Nothing OrElse INDsleConceptFamilyCompensationFundContributionValue.EditValue = 0 Then 'Si el concepto viene vacio
                errors.AppendLine("El concepto de aporte caja compensación está vacío")
            End If
        End If

        If INDTxtRetentionValue.EditValue IsNot Nothing AndAlso INDTxtRetentionValue.EditValue > 0 Then 'Si viene lleno se valida el concepto
            If INDsleConceptCalculatedWithholdingValue.EditValue Is Nothing OrElse INDsleConceptCalculatedWithholdingValue.EditValue = 0 Then 'Si el concepto viene vacio
                errors.AppendLine("El concepto de valor retención está vacío")
            End If
        End If

        If INDtxtRecargoNocturno.EditValue IsNot Nothing AndAlso INDtxtRecargoNocturno.EditValue > 0 Then 'Si viene lleno se valida el concepto
            If INDsleConceptRecargoNocturno.EditValue Is Nothing OrElse INDsleConceptRecargoNocturno.EditValue = 0 Then 'Si el concepto viene vacio
                errors.AppendLine("El concepto de recargos está vacío")
            End If
        End If

        If INDtxtOvertime.EditValue IsNot Nothing AndAlso INDtxtOvertime.EditValue > 0 Then 'Si viene lleno se valida el concepto
            If INDsleConceptOvertime.EditValue Is Nothing OrElse INDsleConceptOvertime.EditValue = 0 Then 'Si el concepto viene vacio
                errors.AppendLine("El concepto de horas extras está vacío")
            End If
        End If

        Return errors.ToString
    End Function

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Private Sub CleanControls()

        INDDePayrollPeriod.Text = String.Empty
        INDTxtWorkDays.EditValue = 0
        INDTxtTotalAccrued.EditValue = 0
        INDTxtTodalDeducted.EditValue = 0
        INDTxtTotalPaid.EditValue = 0
        INDTxtPensionIBC.EditValue = 0
        INDTxtPensionContributionEmployee.EditValue = 0
        INDTxtPensionContributionEmployer.EditValue = 0
        INDTxtHealthIBC.EditValue = 0
        INDTxtHealthContributionEmployee.EditValue = 0
        INDTxtHealthContributionEmployer.EditValue = 0
        INDTxtIncentiveIBC.EditValue = 0
        INDTxtIncentiveProvision.EditValue = 0
        INDTxtVacationIBC.EditValue = 0
        INDTxtProvisionVacation.EditValue = 0
        INDTxtUnemploymentIBC.EditValue = 0
        INDTxtUnemployementProvision.EditValue = 0
        INDTxtUnemployedInterestProvision.EditValue = 0
        INDTxtProvisionDays.EditValue = 0
        INDTxtAmbulatoryInabilityValue.EditValue = 0
        INDTxtHospitalInabilityValue.EditValue = 0
        INDTxtMaternityInabilityValue.EditValue = 0
        INDTxtSenaIBC.EditValue = 0
        INDtxtSenaValue.EditValue = 0
        INDTxtICBFIBC.EditValue = 0
        INDtxtICBFValue.EditValue = 0
        INDtxtCajaIBC.EditValue = 0
        INDtxtCajaValue.EditValue = 0
        INDTxtBaseRetention.EditValue = 0
        INDTxtRetentionValue.EditValue = 0
        INDTxtBasicSalary.EditValue = 0
        INDTxtContractEndingDate.EditValue = 0
        INDTxtContractInitialDate.EditValue = 0
        INDTxtNumContract.EditValue = 0
        INDsleEmployees.EditValue = 0

        LiquidationAdd = New Liquidation()

        Me.Close()

    End Sub

    ''' <summary>
    ''' Valida la info
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateData() As Boolean
        ValidateData = False
        If Group.Liquidation = 1 Then
            If INDTxtWorkDays.EditValue > 30 Then
                Exit Function
                'No puede superar los 30 dias
            End If
        Else
            If INDTxtWorkDays.EditValue > 15 Then
                Exit Function
                'No puede superar los 15 días
            End If
        End If

        Using Model As New MContractLiquidation("596")
            AsyncLoader(True)
            Dim Liquidation = Model.GetPaymentsByContractId(ContractValid.Id).Where(Function(x) Month(x.PayrollDateLiquidated) = Month(INDDePayrollPeriod.EditValue) And Year(x.PayrollDateLiquidated) = Year(INDDePayrollPeriod.EditValue)).Count()
            AsyncLoader(False)
            If Liquidation > 0 Then
                Exit Function
                'Ya existe una Liquidación para este mes

            End If

        End Using

        ValidateData = True

    End Function

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub PopUpPayrollOpeningBalances_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Presenter = New PPayrollOpeningBalances(Me)

        'Se oculta la barra botones si el form viene abierto desde saldo inicial
        If IsOpenSinceInitialBalancePayroll Then
            Me.ToolBar.Visible = False
            PanelControl1.Visible = True
        Else 'Si el form viene abierto desde el menú de la aplicación
            Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAndAudit)
            PanelControl1.Visible = False
        End If

        changeNumericFormatByCurrency(CurrencyAbbreviation.GetNumberFormat)
        'Se obtiene el layout del control
        Dim _layout As LayoutControl = (From x In Me.INDPanelControlBase.Controls Where x.GetType Is GetType(LayoutControl) Select x).FirstOrDefault()
        'Se obtiene los controles textEdit y searchLookUpEdit del layout
        ListControlsTextAndSearch = (From x In _layout.Controls Where x.GetType Is GetType(DevExpress.XtraEditors.TextEdit) OrElse x.GetType Is GetType(DevExpress.XtraEditors.SearchLookUpEdit) Select x).ToList()
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del empleado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDsleEmployees_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleEmployees.EditValueChanged
        If EmployeeId <> 0 Then
            AsyncLoader(True)

            'Se obtiene el empleado por id
            Employee = Await Presenter.GetSelectedEmployeeById(EmployeeId)

            'Se valida que el empleado venga lleno
            If Employee Is Nothing OrElse Employee.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "El empleado seleccionado no tiene contrato activo"
                EmployeeId = Nothing
                Exit Sub
            End If

            'Se asigna el contrato activo del empleado
            ContractValid = Employee.Contract.Where(Function(x) x.Valid = True).FirstOrDefault()

            'Se asigna el grupo del contrato activo
            Group = ContractValid.Group
            AsyncLoader(False)

            If Employee IsNot Nothing AndAlso Employee.Id > 0 Then
                INDTxtNumContract.EditValue = ContractValid.InitialContractNumber.ToString()
                INDTxtContractInitialDate.EditValue = ContractValid.JobBondingDate.ToString("dd/MM/yyyy")
                INDTxtContractEndingDate.EditValue = ContractValid.ContractEndingDate.ToString("dd/MM/yyyy")
                INDTxtBasicSalary.EditValue = ContractValid.BasicSalary.ToString()

                Dim ContractInitialDate = ContractValid.JobBondingDate

                If Day(ContractInitialDate) > 1 Then
                    ContractInitialDate = New Date(ContractInitialDate.Year, ContractInitialDate.Month, 1)
                End If
                INDDePayrollPeriod.Properties.MinValue = ContractInitialDate
                INDDePayrollPeriod.Properties.MaxValue = ContractValid.ContractEndingDate
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de total devengado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDTxtTotalAccrued_EditValueChanged(sender As Object, e As EventArgs) Handles INDTxtTotalAccrued.EditValueChanged
        INDTxtTotalPaid.EditValue = INDTxtTotalAccrued.EditValue - INDTxtTodalDeducted.EditValue
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de total deducido
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDTxtTodalDeducted_EditValueChanged(sender As Object, e As EventArgs) Handles INDTxtTodalDeducted.EditValueChanged
        INDTxtTotalPaid.EditValue = INDTxtTotalAccrued.EditValue - INDTxtTodalDeducted.EditValue
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor de los controles de valor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDTxtPensionContributionEmployee_EditValueChanged(sender As Object, e As EventArgs) Handles INDTxtPensionContributionEmployee.EditValueChanged, INDTxtUnemployementProvision.EditValueChanged, INDTxtUnemployedInterestProvision.EditValueChanged, INDtxtSenaValue.EditValueChanged, INDTxtRetentionValue.EditValueChanged, INDtxtRecargoNocturno.EditValueChanged, INDTxtProvisionVacation.EditValueChanged, INDTxtPensionContributionEmployer.EditValueChanged, INDTxtMaternityInabilityValue.EditValueChanged, INDTxtIncentiveProvision.EditValueChanged, INDtxtICBFValue.EditValueChanged, INDTxtHospitalInabilityValue.EditValueChanged, INDTxtHealthContributionEmployer.EditValueChanged, INDTxtHealthContributionEmployee.EditValueChanged, INDtxtCajaValue.EditValueChanged, INDTxtAmbulatoryInabilityValue.EditValueChanged, INDtxtOvertime.EditValueChanged
        'Obtengo el control search con el tabIndex siguiente al textedit
        Dim control = (From x As System.Windows.Forms.Control In ListControlsTextAndSearch Where x.TabIndex = DirectCast(sender, DevExpress.XtraEditors.TextEdit).TabIndex + 1 Select x).FirstOrDefault()
        If DirectCast(sender, DevExpress.XtraEditors.TextEdit).EditValue IsNot Nothing AndAlso DirectCast(sender, DevExpress.XtraEditors.TextEdit).EditValue > 0 Then 'Si el valor del control es mayor a 0
            'Asigno el readOnly en false
            DirectCast(control, DevExpress.XtraEditors.SearchLookUpEdit).Properties.ReadOnly = False
        Else 'Si el valor del control es 0
            'Asigno el readOnly en true
            DirectCast(control, DevExpress.XtraEditors.SearchLookUpEdit).Properties.ReadOnly = True
            'Asigno null al control
            DirectCast(control, DevExpress.XtraEditors.SearchLookUpEdit).EditValue = Nothing
        End If
    End Sub

#End Region

#Region "EditValueChanging"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del periodo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDDePayrollPeriod_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDDePayrollPeriod.EditValueChanging
        'If INDDePayrollPeriod.EditValue IsNot Nothing Then
        '    If Group.Liquidation = 1 Then
        '        If (Convert.ToDateTime(e.NewValue, System.Globalization.CultureInfo.InvariantCulture).Day <> 1) Then
        '            e.Cancel = True
        '        End If
        '    Else
        '        If (Convert.ToDateTime(e.NewValue, System.Globalization.CultureInfo.InvariantCulture).Day <> 1 And Convert.ToDateTime(e.NewValue, System.Globalization.CultureInfo.InvariantCulture).Day <> 16) Then
        '            e.Cancel = True
        '        End If
        '    End If
        'End If
    End Sub

#End Region

#Region "DrawItem"

    ''' <summary>
    ''' Evento que se dispara al pintar los valores al periodo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDDePayrollPeriod_DrawItem(sender As Object, e As DevExpress.XtraEditors.Calendar.CustomDrawDayNumberCellEventArgs) Handles INDDePayrollPeriod.DrawItem
        'If Group.Liquidation = 1 Then
        '    If (e.Date.Day <> 1) Then
        '        e.Style.ForeColor = Color.LightGray
        '        e.State = DevExpress.Utils.Drawing.ObjectState.Disabled
        '    End If
        'Else
        '    If (e.Date.Day <> 1 And e.Date.Day <> 16) Then
        '        e.Style.ForeColor = Color.LightGray
        '        e.State = DevExpress.Utils.Drawing.ObjectState.Disabled
        '    End If
        'End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton de aceptar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnAddEmployee_Click(sender As Object, e As EventArgs) Handles INDbtnAddEmployee.Click
        Dim errors = ValidateInformation() 'Se obtiene el resultado de la validación
        If errors.Length > 0 Then 'Si hay algun error se avisa al usuario
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If
        AssignValues()
        Dim args As New AddEntityStoreProcedure
        args.SP_ImportFileInitialBalancePayroll_Result = EntityStore
        RaiseEvent AddEntityStoreProcedureEventArgs(Nothing, args)
        Me.Close()
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de empleado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleEmployees_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleEmployees.QueryPopUp
        If EmployeeXpo Is Nothing Then
            Presenter.InitializeEmployee()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar los controles de concepto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleConceptPensionContributionValue_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleConceptPensionContributionValue.QueryPopUp, INDsleConceptEmployerPensionContributionValue.QueryPopUp, INDsleConceptUnemploymentAccumulated.QueryPopUp, INDsleConceptProvisionVacation.QueryPopUp, INDsleConceptProvisionInterestsUnemployment.QueryPopUp, INDsleConceptProvisionIncentive.QueryPopUp, INDsleConceptEmployerHealthContributionValue.QueryPopUp, INDsleConceptEmployeeHealthContributionValue.QueryPopUp, INDsleConceptSenaContributionValue.QueryPopUp, INDsleConceptRecargoNocturno.QueryPopUp, INDsleConceptMaternityLeaveValue.QueryPopUp, INDsleConceptICBFContributionValue.QueryPopUp, INDsleConceptFamilyCompensationFundContributionValue.QueryPopUp, INDsleConceptDisabilityHospitalValue.QueryPopUp, INDsleConceptCalculatedWithholdingValue.QueryPopUp, INDsleConceptAmbulatoryDisabilityValue.QueryPopUp, INDsleConceptOvertime.QueryPopUp
        If DirectCast(sender, DevExpress.XtraEditors.SearchLookUpEdit).Properties.DataSource Is Nothing Then
            DirectCast(sender, DevExpress.XtraEditors.SearchLookUpEdit).Properties.DataSource = Presenter.ListConceptPayrollXpo()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara al presionar el mas en el control de empleados
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleEmployees_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleEmployees.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(529, Nothing, True)
            Presenter.InitializeEmployee()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el mas de los controles de concepto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleConceptPensionContributionValue_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleConceptPensionContributionValue.ButtonClick, INDsleConceptEmployerPensionContributionValue.ButtonClick, INDsleConceptUnemploymentAccumulated.ButtonClick, INDsleConceptProvisionVacation.ButtonClick, INDsleConceptProvisionInterestsUnemployment.ButtonClick, INDsleConceptProvisionIncentive.ButtonClick, INDsleConceptEmployerHealthContributionValue.ButtonClick, INDsleConceptEmployeeHealthContributionValue.ButtonClick, INDsleConceptSenaContributionValue.ButtonClick, INDsleConceptRecargoNocturno.ButtonClick, INDsleConceptMaternityLeaveValue.ButtonClick, INDsleConceptICBFContributionValue.ButtonClick, INDsleConceptFamilyCompensationFundContributionValue.ButtonClick, INDsleConceptDisabilityHospitalValue.ButtonClick, INDsleConceptCalculatedWithholdingValue.ButtonClick, INDsleConceptAmbulatoryDisabilityValue.ButtonClick, INDsleConceptOvertime.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(549, Nothing, True)
            DirectCast(sender, DevExpress.XtraEditors.SearchLookUpEdit).Properties.DataSource = Presenter.ListConceptPayrollXpo()
        End If
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub PopUpPayrollOpeningBalances_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleEmployees.Focus()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar escape al form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub PopUpPayrollOpeningBalances_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            If IsOpenSinceInitialBalancePayroll Then 'Si el form se abrió desde saldo inicial
                Me.Close()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el control de concepto de horas extras
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleConceptOvertime_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDsleConceptOvertime.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDbtnAddEmployee.Focus()
        End If
    End Sub

#End Region

#End Region

End Class