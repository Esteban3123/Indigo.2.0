'***********************************************************************
' Assembly         : Presentacion.Payrrol
' Author           :Cristian C. Fierro R.
' Created          : 03-03-2023
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Payroll.MVP
Imports Domain.Payroll.Entities
Imports System.ComponentModel
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Domain.Base.Entities
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.Utils.Design.DesignTimeTools
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports DevExpress.Xpo
Imports DevExpress.XtraEditors
Imports Presentation.Controls.MVP

#End Region

Public Class FrmContractEdit

    Implements IContract, ICustomizableForm

#Region "Builders"
    Public Sub New(ByRef employee As Employee, Optional contractNumber As Integer = 0, Optional contractAction As ContractActions = Base.ContractActions.NewContractWithoutPayments)
        InitializeComponent()
        Me.EmployeeContract = employee
        Me.ContractNumber = contractNumber
    End Sub
#End Region

#Region "Const"
    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Payrrol"
#End Region

#Region "Globals & Properties"

    Dim ContractNumber As Integer

    Dim dtFieldsCustomizables As DataTable

    ''' <summary>
    ''' Bandera que se utiliza para verificar si existe o no una definicion del funcional 
    ''' </summary>
    Dim ExistDefinitionFront As Boolean

    ''' <summary>
    ''' Variable que contiene la ruta de las definiciones del layout
    ''' </summary>
    Dim PathFunctionalDefinitions As String

    ''' <summary>
    ''' Variable que contiene el empleado 
    ''' </summary>
    Dim EmployeeContract As Employee

    ''' <summary>
    ''' Variable que contiene el contrato 
    ''' </summary>
    Dim Contract As Contract

    ''' <summary>
    ''' Evento que se utiliza para cargar las definiciones del funcional
    ''' </summary>
    WithEvents LoadhronousDefinitions As BackgroundWorker

    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PContract

    ''' <summary>
    ''' Modelo de busqueda
    ''' </summary>
    ''' <remarks></remarks>
    Dim modelBusqueda As MBusqueda

    ''' <summary>
    ''' Asyncrono
    ''' </summary>
    ''' <remarks></remarks>
    Private bw As BackgroundWorker = New BackgroundWorker

    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Dim indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Almacena la clase de contrato usada con los valores de la enumeracion
    ''' </summary>
    Private _contractClass As Eresources

    ''' <summary>
    ''' Almacena la clase de contrato usada con los valores que se guardan en la base de datos
    ''' </summary>
    Private _contractClassDB As Byte

    Public Property Contingency As Byte? Implements IContract.Contingency
        Get
            If Contingency Is Nothing Then
                Return 0
            End If
            Return INDSlContingency.EditValue
        End Get
        Set(value As Byte?)
            INDSlContingency.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el datasource de bancos
    ''' </summary>
    Public WriteOnly Property BankDatasourceXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements IContract.BankDatasourceXPO
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleBankId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el datasource de tipos de contratos
    ''' </summary>
    Public WriteOnly Property ContractTypeDatasourceXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements IContract.ContractTypeDatasourceXPO
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleContractTypeId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el datasource de unidad funcional
    ''' </summary>
    Public WriteOnly Property FunctionalUnitDatasourceXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements IContract.FunctionalUnitDatasourceXPO
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleFunctionalUnitId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el datasource de grupos
    ''' </summary>
    Public WriteOnly Property GroupDatasourceXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements IContract.GroupDatasourceXPO
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleGroupId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el datasource de tipos de vinculacion
    ''' </summary>
    Public WriteOnly Property JobBondingDatasourceXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements IContract.JobBondingDatasourceXPO
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleJobBondingTypeId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el datasource de Cargos
    ''' </summary>
    Public WriteOnly Property PositionDatasourceXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements IContract.PositionDatasourceXPO
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDslePositionId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el datasource de fondos
    ''' </summary>
    Public WriteOnly Property FundDatasourceXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements IContract.FundDatasourceXPO
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleFundIds.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el datasource de centros de trabajo
    ''' </summary>
    Public WriteOnly Property WorkCentersSourceXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements IContract.WorkCentersSourceXPO
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleWorkCenterId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el datasource de centros de tipos de empleados
    ''' </summary>
    Public WriteOnly Property EmployeeTypeXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements IContract.EmployeeTypeXPO
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleEmployeeType.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el datasource de motivos de modificaciones de contratos, este metodo no se debe usar y solo es para el modulo de modificacion de contratos
    ''' </summary>
    Public WriteOnly Property ContractModificationReasonsXPO As XPInstantFeedbackSource Implements IContract.ContractModificationReasonsXPO
        Set(value As XPInstantFeedbackSource)

        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el datasource de fondos utilizado por la rejilla
    ''' </summary>
    Private _fundsDatasource As List(Of FundContract)
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
    Public Property FundsDatasource As List(Of FundContract)
        Get
            If _fundsDatasource Is Nothing Then
                _fundsDatasource = New List(Of FundContract)
            End If
            Return _fundsDatasource
        End Get
        Set(value As List(Of FundContract))
            _fundsDatasource = value
        End Set
    End Property

    ''' <summary>
    ''' Contiene el fondo a editar
    ''' </summary>
    Private FundToEdit As FundContract

    ''' <summary>
    ''' Contiene el valor del tipo de duracion del tipo de contrato
    ''' </summary>
    Private _isUndefinedEnable As Boolean
    Private Property IsUndefinedEnable As Boolean
        Get
            Return _isUndefinedEnable
        End Get
        Set(value As Boolean)
            If value = Nothing Then
                value = False
            End If

            If Not _isUndefinedEnable = value Then
                If value = True Then
                    INDlciContractLengthYear.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlciContractLengthMonth.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlciContractLengthDay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlciContractEndingDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDdteContractEndingDate.EditValue = #12/31/9999#
                Else
                    INDlciContractLengthYear.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlciContractLengthMonth.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlciContractLengthDay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlciContractEndingDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDdteContractEndingDate.EditValue = Nothing
                End If
                _isUndefinedEnable = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Contiene el valor del periodo de prueba del tipo de contrato
    ''' </summary>
    Private _isTrialPeriodEnable As Boolean = False
    Private Property IsTrialPeriodEnable As Boolean
        Get
            Return _isTrialPeriodEnable
        End Get
        Set(value As Boolean)
            If value = Nothing Then
                value = False
            End If

            If Not _isTrialPeriodEnable = value Then
                If value = True Then
                    INDlciTrialPeriodSalaryPercentage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlciTrialPeriodTime.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Else
                    INDlciTrialPeriodSalaryPercentage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlciTrialPeriodTime.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                End If
                _isTrialPeriodEnable = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Variable que contiene la accion a realizar sobre el contrato
    ''' </summary>
    Private ContractAction As ContractActions = ContractActions.NewContractWithoutPayments

    ''' <summary>
    ''' Variable que contiene el contrato a editar
    ''' </summary>
    Private ContractToEdit As Contract

    ''' <summary>
    ''' Contiene la informacion del fondo seleccionado
    ''' </summary>
    Private fundXPO As PayrollFundsXpo

    ''' <summary>
    ''' Contiene la informacion del cargo para realizar las validaciones
    ''' </summary>
    Private positionXPO As PayrollPositionXpo

    ''' <summary>
    ''' Variable que contiene la unidad funcional seleccionada
    ''' </summary>
    Private FunctionalUnitXPO As PayrollFunctionalUnit

    ''' <summary>
    ''' Variable que contiene el tiempo minimo de un contrato en meses
    ''' </summary>
    Private MinMonthsContractLength As Integer

    ''' <summary>
    ''' Variable que contiene el tiempo maximo de un contrato en meses
    ''' </summary>
    Private MaxMonthsContractLength As Integer

    ''' <summary>
    ''' The model
    ''' </summary>
    Dim model As MContract

#End Region

#Region "ICRUD"

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
        Set(value As String)
            ' 
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, Botones.Aceptar, Base.Icono.Errores)
            End If
        End Set
    End Property

#End Region

#Region "Methods"
    ''' <summary>
    ''' Inicializa los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Initializes()
        'Datasource de controles con fuente local
        INDgleSeveranceType.Properties.DataSource = EmployeeHelper.SeveranceType
        INDgleSalaryType.Properties.DataSource = EmployeeHelper.SalaryType
        INDglePaymentPeriod.Properties.DataSource = EmployeeHelper.PaymentPeriod
        INDglePaymentType.Properties.DataSource = EmployeeHelper.PaymentType
        INDgleAutoRenew.Properties.DataSource = EmployeeHelper.YesNoBoolean.ToList
        INDgleBankAccountType.Properties.DataSource = EmployeeHelper.BankAccountType
        INDgleVoluntaryContribution.Properties.DataSource = EmployeeHelper.YesNoBoolean.ToList

        INDgrdListOfFunds.DataSource = FundsDatasource

    End Sub

    ''' <summary>
    ''' Metodo que sirve para limpiar los controles del frontal
    ''' </summary>
    Public Sub CleanControls()
        CleanControlsBasic()
    End Sub

    ''' <summary>
    ''' Metodo que sirve para limpiar los controles del frontal basico
    ''' </summary>
    Public Sub CleanControlsBasic()
        INDtxtContractId.EditValue = Nothing

        INDSlContingency.EditValue = 0
        INDdteContractEndingDate.EditValue = Nothing
        INDdteContractInitialDate.EditValue = Nothing
        INDspnContractLengthYear.EditValue = 0
        INDspnContractLengthMonth.EditValue = 0
        INDspnContractLengthDay.EditValue = 0

        INDtxtResolutionNumber.EditValue = Nothing
        INDdtePosesionDate.EditValue = Nothing
        INDspnProfessionalRiskPercentage.EditValue = Nothing
        INDsleEmployeeType.EditValue = Nothing
        INDgleSeveranceType.EditValue = Nothing

        INDtxtCostCenterId.EditValue = Nothing
        INDsleWorkCenterId.EditValue = Nothing
        INDgleAutoRenew.EditValue = Nothing
        INDmemNotes.EditValue = Nothing
        INDgleBankAccountType.EditValue = Nothing
        INDglePaymentPeriod.EditValue = Nothing
        INDglePaymentType.EditValue = Nothing
        INDgleSalaryType.EditValue = Nothing


        INDsleBankId.EditValue = -1
        INDsleContractTypeId.EditValue = -1
        INDsleFunctionalUnitId.EditValue = -1
        INDsleGroupId.EditValue = -1
        INDsleJobBondingTypeId.EditValue = -1
        INDslePositionId.EditValue = -1

        INDtxtBankAccountNumber.EditValue = Nothing
        INDspnBasicSalary.EditValue = Nothing
        INDspnTrialPeriodSalaryPercentage.EditValue = Nothing
        INDspnTrialPeriodTime.EditValue = Nothing
        'Fondos
        FundsDatasource.Clear()
        INDgrdListOfFunds.RefreshDataSource()
        CleanFundsPopup()
    End Sub

    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    Private Function ValidateControls() As Boolean
        ValidateControls = True

        If indigo.IndigoCompanyType = "2" Then
            If INDtxtResolutionNumber.EditValue Is Nothing Then
                INDtxtResolutionNumber.Focus()
                ValidateControls = False
                Exit Function
            End If

            If INDdtePosesionDate.EditValue Is Nothing Then
                INDdtePosesionDate.Focus()
                ValidateControls = False
                Exit Function
            End If

        End If

        If INDSlContingency.EditValue Is Nothing OrElse INDSlContingency.EditValue.ToString.Trim.Equals(String.Empty) Then
            INDSlContingency.Focus()
            ValidateControls = False
            Exit Function
        End If

        If INDtxtContractId.EditValue Is Nothing OrElse INDtxtContractId.EditValue.ToString.Trim.Equals(String.Empty) Then
            INDtxtContractId.Focus()
            ValidateControls = False
            Exit Function
        End If

        If INDsleEmployeeType.EditValue Is Nothing OrElse INDsleEmployeeType.EditValue = -1 Then
            INDsleEmployeeType.Focus()
            ValidateControls = False
            Exit Function
        End If

        If IsUndefinedEnable = False Then

            If INDdteContractEndingDate.EditValue Is Nothing OrElse INDdteContractEndingDate.EditValue.ToString.Trim.Equals(String.Empty) Then
                INDdteContractEndingDate.Focus()
                ValidateControls = False
                Exit Function
            End If

        End If

        If INDdteContractInitialDate.EditValue Is Nothing OrElse INDdteContractInitialDate.EditValue.ToString.Trim.Equals(String.Empty) Then
            INDdteContractInitialDate.Focus()
            ValidateControls = False
            Exit Function
        End If

        If INDsleBankId.EditValue Is Nothing OrElse INDsleBankId.EditValue = -1 Then
            INDsleBankId.Focus()
            ValidateControls = False
            Exit Function
        End If

        If INDsleContractTypeId.EditValue Is Nothing OrElse INDsleContractTypeId.EditValue = -1 Then
            INDsleContractTypeId.Focus()
            ValidateControls = False
            Exit Function
        End If

        If INDsleFunctionalUnitId.EditValue Is Nothing OrElse INDsleFunctionalUnitId.EditValue = -1 Then
            INDsleFunctionalUnitId.Focus()
            ValidateControls = False
            Exit Function
        End If

        If INDsleGroupId.EditValue Is Nothing OrElse INDsleGroupId.EditValue = -1 Then
            INDsleGroupId.Focus()
            ValidateControls = False
            Exit Function
        End If

        If INDsleJobBondingTypeId.EditValue Is Nothing OrElse INDsleJobBondingTypeId.EditValue = -1 Then
            INDsleJobBondingTypeId.Focus()
            ValidateControls = False
            Exit Function
        End If

        If INDslePositionId.EditValue Is Nothing OrElse INDslePositionId.EditValue = -1 Then
            INDsleBankId.Focus()
            ValidateControls = False
            Exit Function
        End If

        If INDtxtBankAccountNumber.Text Is Nothing OrElse INDtxtBankAccountNumber.Text.Trim.Equals(String.Empty) Then
            INDtxtBankAccountNumber.Focus()
            ValidateControls = False
            Exit Function
        End If

        If INDspnBasicSalary.Text Is Nothing OrElse INDspnBasicSalary.Text.Trim.Equals(String.Empty) Then
            INDspnBasicSalary.Focus()
            ValidateControls = False
            Exit Function
        End If

        If IsTrialPeriodEnable = True Then

            If INDspnTrialPeriodSalaryPercentage.Text Is Nothing OrElse INDspnTrialPeriodSalaryPercentage.Text.Trim.Equals(String.Empty) Then
                INDspnTrialPeriodSalaryPercentage.Focus()
                ValidateControls = False
                Exit Function
            End If

            If INDspnTrialPeriodTime.Text Is Nothing OrElse INDspnTrialPeriodTime.Text.Trim.Equals(String.Empty) Then
                INDspnTrialPeriodTime.Focus()
                ValidateControls = False
                Exit Function
            End If
        End If
    End Function

    ''' <summary>
    ''' Valida que los campos del popup de fondos se encuentren diligenciados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateFundsControls() As Boolean
        ValidateFundsControls = True

        If INDsleFundIds.EditValue Is Nothing OrElse INDsleFundIds.EditValue = -1 Then
            INDsleFundIds.Focus()
            ValidateFundsControls = False
            Exit Function
        End If

        If INDgleFundType.EditValue Is Nothing Then
            INDgleFundType.Focus()
            ValidateFundsControls = False
            Exit Function
        End If

        If INDdteFundInitialDate.EditValue Is Nothing OrElse INDdteFundInitialDate.EditValue.Equals(String.Empty) Then
            INDdteFundInitialDate.Focus()
            ValidateFundsControls = False
            Exit Function
        End If

        If INDtxtMembershipNumber.Text Is Nothing OrElse INDtxtMembershipNumber.Text.Trim.Equals(String.Empty) Then
            INDtxtMembershipNumber.Focus()
            ValidateFundsControls = False
            Exit Function
        End If

        If INDgleVoluntaryContribution.EditValue Is Nothing Then
            INDgleVoluntaryContribution.Focus()
            ValidateFundsControls = False
            Exit Function
        ElseIf INDgleVoluntaryContribution.EditValue = True Then
            If INDtxtVoluntaryContributionValue.Text Is Nothing OrElse INDtxtVoluntaryContributionValue.Text.Trim.Equals(String.Empty) Then
                INDtxtVoluntaryContributionValue.Focus()
                ValidateFundsControls = False
                Exit Function
            End If
        End If
    End Function

    ''' <summary>
    ''' Valida que el fondo no se encuentre ya agregado al listado de fondos del contrato
    ''' </summary>
    ''' <returns>Falso si el fondo ya se encuentra agregado, Verdadero en el caso contrario</returns>
    Private Function ValidateMaxFundsTypes() As Boolean
        ValidateMaxFundsTypes = True

        If _contractClass = Eresources.ClaseContratoOtros Then
            'La clase de contrato otros no debe tener fondos
            ValidateMaxFundsTypes = False
            Exit Function
        Else
            If FundsDatasource.Where(Function(i) i.FundType = INDgleFundType.EditValue).Count > 0 Then
                ValidateMaxFundsTypes = False
                Exit Function
            End If
        End If

    End Function

    ''' <summary>
    ''' Metodo para relizar las validaciones de logica de funcionamiento de un contrato
    ''' </summary>
    Private Function ValidateContractSpecifics() As Boolean
        ValidateContractSpecifics = True

        Dim cont
        Dim tmpContractId = If(INDtxtContractId.EditValue = obtenerRecurso(LabelNuevo, RecepcionObjeciones), 0, INDtxtContractId.EditValue)
        'Verifica que no se creen 2 contratos vigentes activos
        cont = EmployeeContract.Contract.Where(Function(c) (Not c.Id = tmpContractId AndAlso (c.Valid = True AndAlso c.Status = CType(1, Byte)) AndAlso c.ChangeTracker.State <> ObjectState.Deleted)).FirstOrDefault
        If Not ContractAction = ContractActions.EditContract AndAlso cont IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ContratosNo2Vigentes, Contrato)
            ValidateContractSpecifics = False
            Exit Function
        End If

        'Fecha de inicio del contrato no puede estar metida en medio de un contrato vigente
        cont = EmployeeContract.Contract.Where(Function(c) Not c.Id = tmpContractId AndAlso c.ContractInitialDate.CompareTo(INDdteContractInitialDate.EditValue) <= 0 AndAlso c.Valid = True AndAlso c.Status = CType(1, Byte) AndAlso c.ChangeTracker.State <> ObjectState.Deleted).FirstOrDefault
        If Not ContractAction = ContractActions.EditContract AndAlso cont IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ContratosEnRangoOtroContrato, Contrato)
            ValidateContractSpecifics = False
            Exit Function
        End If

        If _contractClass = Eresources.ClaseContratoAprendizaje Then
            'minimo salud
            If FundsDatasource.Where(Function(i) i.Fund.Health = True).Count <= 0 Then
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(FondoSaludObligatorio, Contrato)
                ValidateContractSpecifics = False
                Exit Function
            End If
        ElseIf _contractClass = Eresources.ClaseContratoAprendizajePractica Then
            'minimo salud y arp
            If FundsDatasource.Where(Function(i) i.Fund.Risk = True).Count <= 0 Then
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(FondoArlObligatorio, Contrato)
                ValidateContractSpecifics = False
                Exit Function
            End If
            If FundsDatasource.Where(Function(i) i.Fund.Health = True).Count <= 0 Then
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(FondoSaludObligatorio, Contrato)
                ValidateContractSpecifics = False
                Exit Function
            End If
        ElseIf _contractClass = Eresources.ClaseContratoLaboralFijo OrElse _contractClass = Eresources.ClaseContratoLaboralIndefinido Then

            If EmployeeContract.Pensionary Then
                If indigo.IndigoCompanyType = 2 Then
                    If FundsDatasource.Where(Function(i) i.Fund.Health = True).Count <= 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(FondoSaludObligatorio, Contrato)
                        ValidateContractSpecifics = False
                        Exit Function
                    End If
                Else
                    If FundsDatasource.Where(Function(i) i.Fund.Health = True).Count <= 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(FondoSaludObligatorio, Contrato)
                        ValidateContractSpecifics = False
                        Exit Function
                    End If

                    If FundsDatasource.Where(Function(i) i.Fund.Risk = True).Count <= 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(FondoArlObligatorio, Contrato)
                        ValidateContractSpecifics = False
                        Exit Function
                    End If
                End If
            End If
            'minimo salud, pension y arp
            If FundsDatasource.Where(Function(i) i.Fund.Risk = True).Count <= 0 Then
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(FondoArlObligatorio, Contrato)
                ValidateContractSpecifics = False
                Exit Function
            End If


            If FundsDatasource.Where(Function(i) i.Fund.Health = True).Count <= 0 Then
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(FondoSaludObligatorio, Contrato)
                ValidateContractSpecifics = False
                Exit Function
            End If

            If FundsDatasource.Where(Function(i) i.Fund.Pension = True).Count <= 0 Then
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(FondoPensionesObligatorio, Contrato)
                ValidateContractSpecifics = False
                Exit Function
            End If
        End If
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        'Valores del contrato
        With Contract
            'Valores por defecto de contrato nuevo
            .RowType = 1 'Contrato Base
            .CreationUserId = indigo.UserIndigo 'Usuario de creacion
            .ContractCreationDate = Date.Today
            .ModificationUserId = indigo.UserIndigo 'Usuario de modificacion
            .ModificationDate = Date.Today 'Fecha de modificación
            .LastModificationDate = Date.Today 'Ultima fecha de modificacion
            .Status = 1 'Contrato Activo
            .Valid = 1 'Contrato vigente
            .LiquidationPayroll = 1 'Liquida nomina
            .InitialContractNumber = 0 'Contrato inicial 0, hace referencia a los contratos base
            .JobBondingDate = INDdteContractInitialDate.EditValue
            If .Contingency Is Nothing Then
                .Contingency = 0
            End If
            .Contingency = Contingency
            'Únicamente para Entidas Públicas
            .ResolutionNumber = INDtxtResolutionNumber.EditValue
            .ResolutionDate = INDdteResolutionDate.EditValue
            .PosesionDate = INDdtePosesionDate.EditValue
            .CertificateOfficeNumber = INDtxtCertificateOfficeNumber.EditValue
            '---------------------------------------------------------------------------------
            .BankId = INDsleBankId.EditValue
            .BankAccountNumber = INDtxtBankAccountNumber.EditValue
            .BankAccountType = INDgleBankAccountType.EditValue
            .BasicSalary = INDspnBasicSalary.EditValue
            .BaseIncome = CType(INDspnBasicSalary.EditValue, Decimal) 'Contrato nuevo tiene ingreso base el mismo del sueldo
            .ContractEndingDate = If(IsUndefinedEnable = True, #12/31/9999#, INDdteContractEndingDate.EditValue)
            .ContractInitialDate = INDdteContractInitialDate.EditValue
            .ContractTypeName = INDsleContractTypeId.Text
            .SalaryType = INDgleSalaryType.EditValue
            .JobBondingName = INDsleJobBondingTypeId.Text
            .ContractClass = _contractClassDB
            .ContractTypeId = INDsleContractTypeId.EditValue
            .FunctionalUnitName = INDsleFunctionalUnitId.Text
            .FunctionalUnitId = INDsleFunctionalUnitId.EditValue
            .GroupName = INDsleGroupId.Text
            .GroupId = INDsleGroupId.EditValue
            .Notes = INDmemNotes.EditValue
            .PaymentPeriod = INDglePaymentPeriod.EditValue
            .PaymentType = INDglePaymentType.EditValue
            .PositionName = INDslePositionId.Text
            .PositionId = INDslePositionId.EditValue
            .TrialPeriod = IsTrialPeriodEnable
            .TrialPeriodSalaryPercentage = CInt(INDspnTrialPeriodSalaryPercentage.EditValue)
            .TrialPeriodTime = CInt(INDspnTrialPeriodTime.EditValue)
            .HoursDaily = IIf(INDTxtHoursDaily.EditValue IsNot Nothing, INDTxtHoursDaily.EditValue, 8)

            For Each item As FundContract In FundsDatasource
                .FundContract.Add(item)
            Next
        End With

        'Valores del empleado
        With EmployeeContract
            .CostCenterId = FunctionalUnitXPO.CostCenterId.Id
            .CostCenter = New CostCenter With {.Id = FunctionalUnitXPO.CostCenterId.Id, .Name = FunctionalUnitXPO.CostCenterId.Descripcion}
            .WorkCenterId = CType(INDsleWorkCenterId.EditValue.ToString, Integer)
            .WorkCenter = New WorkCenter With {.Id = INDsleWorkCenterId.EditValue.ToString, .Name = INDsleWorkCenterId.Text}
            .ProfessionalRiskPercentage = If(INDspnProfessionalRiskPercentage.EditValue Is Nothing OrElse INDspnProfessionalRiskPercentage.EditValue.ToString.Trim.Equals(String.Empty), 0D, CType(INDspnProfessionalRiskPercentage.EditValue, Decimal))
            .EmployeeTypeId = INDsleEmployeeType.EditValue
            .State = 1 'El empleado con estado activo
            .Contract.Add(Contract) 'Agrega el contrato nuevo al empleado
            .VacationLastDateLiquidation = INDdteContractInitialDate.EditValue
        End With
    End Sub

    ''' <summary>
    ''' Metodo que habilita comportamiento del formulario en base a la clase de contrato
    ''' </summary>
    ''' <param name="contractClass"></param>
    Private Sub ContractClassSpecificBehavior(contractClass As Byte)
        'filtra los grupos de acuerdo a la clase de contrato
        Presenter.ChangeGroupDatasourceByContractClass(contractClass)
        'busca el valor de la enumeracion para el tipo de contrato
        Dim cc = EmployeeHelper.ContractClasses.Where(Function(i) i.Item2 = contractClass).FirstOrDefault.Item1

        Select Case cc
            Case Eresources.ClaseContratoOtros
                INDlciListOfFunds.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlcgFundsInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                MinMonthsContractLength = 0
                MaxMonthsContractLength = 999

            Case Eresources.ClaseContratoAprendizaje
                INDlciListOfFunds.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlcgFundsInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                MinMonthsContractLength = 0
                MaxMonthsContractLength = 24
                INDspnProfessionalRiskPercentage.EditValue = 0

            Case Eresources.ClaseContratoLaboralFijo

                INDlciListOfFunds.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlcgFundsInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                MinMonthsContractLength = 0
                MaxMonthsContractLength = 36

            Case Eresources.ClaseContratoLaboralIndefinido

                INDlciListOfFunds.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlcgFundsInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

            Case Else
                Return
        End Select

        _contractClass = cc
        _contractClassDB = contractClass

    End Sub


    Private Function ValidateContractLengths(initialDate As Date, endingDate As Date, minMonthsLength As Integer, maxMonthsLengths As Integer, Optional minAllowedDate As Date = #12:00:00 AM#, Optional canOverrideLength As Boolean = False) As Boolean
        Dim r = True

        If initialDate <> Nothing AndAlso endingDate <> Nothing Then

            Dim ts = DateDiff(DateInterval.Month, initialDate, endingDate)

            If canOverrideLength = True Then

                If endingDate < minAllowedDate Then
                    'TODO Agregar a recursos
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(ContratosFechaMinimaPermitida, Contrato), minAllowedDate)
                    r = False
                End If

                If ts > maxMonthsLengths Then
                    'TODO agregar recursos
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(ContratosFechaMaximaContratoFijo, Contrato), (maxMonthsLengths / 12))
                    r = False
                End If
            Else
                If ts < minMonthsLength OrElse ts > maxMonthsLengths Then
                    'TODO agregar recursos
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(ContratoRangoDeFechas, Contrato), (minMonthsLength / 12), (maxMonthsLengths / 12))
                    r = False
                End If
            End If

        End If
        Return r
    End Function

    Public Sub GetEmployeeToWorkWith()
        Me.ContractAction = ContractAction
        CleanControlsBasic()
        If Me.EmployeeContract IsNot Nothing Then
            'Me.EmployeeContract = Employee

            ContractToEdit = Me.EmployeeContract.Contract.Where(Function(c) c.Id = ContractNumber).FirstOrDefault()

            If ContractToEdit Is Nothing OrElse ContractAction = ContractActions.NewContractWithoutPayments Then
                Contract = New Contract
                INDtxtContractId.Text = obtenerRecurso(LabelNuevo, RecepcionObjeciones)
            End If
        Else
            Me.EmployeeContract = Nothing
            Contract = Nothing
        End If

        INDsleEmployeeType.Focus()
    End Sub


    ''' <summary>
    ''' Metodo que muestra la descripcion de los tipos de fondos de acuerdo al seleccionado
    ''' </summary>
    Private Sub INDrepFundsType_CustomDisplayText(sender As Object, e As CustomDisplayTextEventArgs) Handles INDrepFundsType.CustomDisplayText
        If e.Value IsNot Nothing Then
            e.DisplayText = EmployeeHelper.FundsTypes.Where(Function(i) i.Item2 = e.Value).FirstOrDefault.Item3
        End If
    End Sub

    Private Sub RepositoryItemTextEdit1_CustomDisplayText(sender As Object, e As CustomDisplayTextEventArgs)
        If e.Value IsNot Nothing Then
            e.DisplayText = EmployeeHelper.FundsTypes.Where(Function(i) i.Item2 = e.Value).FirstOrDefault.Item3
        End If
    End Sub



    Private Sub InitializeTuple()
        Dim ListTupleContingency = New List(Of Tuple(Of Byte?, String))

        ListTupleContingency.Add(New Tuple(Of Byte?, String)(0, "Ninguna"))
        ListTupleContingency.Add(New Tuple(Of Byte?, String)(1, "Licencias"))
        ListTupleContingency.Add(New Tuple(Of Byte?, String)(2, "Vacaciones"))
        ListTupleContingency.Add(New Tuple(Of Byte?, String)(3, "Incapacidades"))

        INDSlContingency.Properties.DataSource = ListTupleContingency.ToList()
        Contingency = 0

    End Sub

#End Region

#Region "Events"

    ''' <summary>
    ''' Evento que retorna la informacion al formulario padre
    ''' </summary>
    ''' <remarks></remarks>
    Public Event OnContractAdded(sender As Object, e As OnContractAddedEventArgs)
#End Region

#Region "Fondos"


    ''' <summary>
    ''' Metodo que limpia los valores que se encuentre dentro del popup de fondos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanFundsPopup()
        INDsleFundIds.EditValue = -1
        INDgleFundType.EditValue = Nothing
        INDgleFundType.Properties.DataSource = Nothing
        INDtxtMembershipNumber.EditValue = Nothing
        INDgleVoluntaryContribution.EditValue = Nothing
        INDtxtVoluntaryContributionValue.EditValue = Nothing
        INDlciVoluntaryContributionValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
    End Sub

    ''' <summary>
    ''' Metodo que limpia el popup de fondos si se cierra cuando se esta editando un fondo
    ''' </summary>
    Private Sub INDpopFunds_Closed(sender As Object, e As System.EventArgs)
        If FundToEdit IsNot Nothing Then
            CleanFundsPopup()
            FundToEdit = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Metodo que ubica el foco en el primer campo a diligenciar en el popup de fondos
    ''' </summary>
    Private Sub INDpopFunds_Popup(sender As Object, e As DevExpress.XtraEditors.ShowDropDownControlEventArgs)
        INDsleFundIds.Focus()
    End Sub

#End Region

#Region "QueryPopUp"
    Private Sub INDsleBankId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleBankId.QueryPopUp
        If INDsleBankId.Properties.DataSource Is Nothing Then
            BankDatasourceXPO = modelBusqueda.ConsultarEntidades(eDataSource.Bank)
        End If
    End Sub

    Private Sub INDsleContractTypeId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleContractTypeId.QueryPopUp
        If INDsleContractTypeId.Properties.DataSource Is Nothing Then
            ContractTypeDatasourceXPO = modelBusqueda.ConsultarEntidades(eDataSource.ContractType)
        End If
    End Sub

    Private Sub INDsleEmployeeType_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleEmployeeType.QueryPopUp
        If INDsleEmployeeType.Properties.DataSource Is Nothing Then
            EmployeeTypeXPO = modelBusqueda.ConsultarEntidades(eDataSource.EmployeeType)
        End If
    End Sub

    Private Sub INDsleFunctionalUnitId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleFunctionalUnitId.QueryPopUp
        If INDsleFunctionalUnitId.Properties.DataSource Is Nothing Then
            FunctionalUnitDatasourceXPO = modelBusqueda.ConsultarEntidades(eDataSource.ListFunctionalUnit, (True))
        End If
    End Sub

    Private Sub INDsleGroupId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleGroupId.QueryPopUp
        If INDsleGroupId.Properties.DataSource Is Nothing Then
            GroupDatasourceXPO = modelBusqueda.ConsultarEntidades(eDataSource.GroupsPayroll)
        End If
    End Sub

    Private Sub INDsleJobBondingTypeId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleJobBondingTypeId.QueryPopUp
        If INDsleJobBondingTypeId.Properties.DataSource Is Nothing Then
            JobBondingDatasourceXPO = modelBusqueda.ConsultarEntidades(eDataSource.JobBondingType)
        End If
    End Sub

    Private Sub INDslePositionId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDslePositionId.QueryPopUp
        If INDslePositionId.Properties.DataSource Is Nothing Then
            PositionDatasourceXPO = modelBusqueda.ConsultarEntidades(eDataSource.ListPositionByStatus, (True))
        End If
    End Sub

    Private Sub INDsleFundIds_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleFundIds.QueryPopUp
        If INDsleFundIds.Properties.DataSource Is Nothing Then
            FundDatasourceXPO = modelBusqueda.ConsultarEntidades(eDataSource.Funds)
        End If
    End Sub

    Private Sub INDsleWorkCenterId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleWorkCenterId.QueryPopUp
        If INDsleWorkCenterId.Properties.DataSource Is Nothing Then
            WorkCentersSourceXPO = modelBusqueda.ConsultarEntidades(eDataSource.WorkCenter)
        End If
    End Sub
#End Region

#Region "Click"
    Private Sub INDbtnAddDetailsBook_Click(sender As Object, e As EventArgs) Handles INDbtnAddDetailsBook.Click
        If ValidateFundsControls() = False Then
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesFormIncompleto)
            Exit Sub
        End If

        If ValidateMaxFundsTypes() = False Then
            'TODO Agregar a recursos
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(FondoYaSeEncuentra, Contrato)
            Exit Sub
        End If

        Dim ctrAdd = 0

        If FundToEdit Is Nothing Then
            FundToEdit = New FundContract()
            ctrAdd = 1
        End If

        With FundToEdit
            .FundId = INDsleFundIds.EditValue
            .Fund = New Fund() With {.Id = INDsleFundIds.EditValue, .Name = INDsleFundIds.Text, .Pension = fundXPO.Pension, .Health = fundXPO.Health, .Unemployment = fundXPO.Unemployment, .Risk = fundXPO.Risk}
            .FundType = INDgleFundType.EditValue
            .InitialDate = INDdteFundInitialDate.EditValue
            .MembershipNumber = INDtxtMembershipNumber.Text
            .State = True
            .VoluntaryContribution = INDgleVoluntaryContribution.EditValue
            .VoluntaryContributionValue = If(INDtxtVoluntaryContributionValue.Text.Trim.Equals(String.Empty), Nothing, INDtxtVoluntaryContributionValue.Text.Trim)
        End With

        If ctrAdd = 1 Then
            FundsDatasource.Add(FundToEdit)
        End If

        FundToEdit = Nothing
        fundXPO = Nothing
        CleanFundsPopup()
        INDgrdListOfFunds.DataSource = Nothing
        INDgrdListOfFunds.DataSource = FundsDatasource
        INDpceDetailsBook.ClosePopup()
    End Sub


    ''' <summary>
    ''' Dispara el evento de contrato añadido y devuelve en los argumentos el empleado
    ''' </summary>
    Private Sub INDbtnAddContract_Click(sender As Object, e As EventArgs) Handles INDbtnAddContract.Click
        Try
            'Valida los campos obligatorios
            If Not ValidateControls() Then
                Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesFormIncompleto)
                Exit Sub
            End If

            'Valida logica de contratos
            If Not ValidateContractSpecifics() Then
                Exit Sub
            End If

            AssigningValues() 'Asigna valores al contrato
            RaiseEvent OnContractAdded(Nothing, New OnContractAddedEventArgs(EmployeeContract)) 'Dispara evento para notificar que el contrato se ha diligenciado
            Me.Close()
        Catch ex As Exception
            INDbtnAddContract.Enabled = False
            Throw ex
        End Try
    End Sub


    ''' <summary>
    ''' Añade un fondo a la rejilla
    ''' </summary>
    Private Sub INDbtnAddFund_Click(sender As Object, e As EventArgs) Handles INDbtnAddFund.Click

        If ValidateFundsControls() = False Then
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesFormIncompleto)
            Exit Sub
        End If

        If ValidateMaxFundsTypes() = False Then
            'TODO Agregar a recursos
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(FondoYaSeEncuentra, Contrato)
            Exit Sub
        End If

        Dim ctrAdd = 0

        If FundToEdit Is Nothing Then
            FundToEdit = New FundContract()
            ctrAdd = 1
        End If

        With FundToEdit
            .FundId = INDsleFundIds.EditValue
            .Fund = New Fund() With {.Id = INDsleFundIds.EditValue, .Name = INDsleFundIds.Text, .Pension = fundXPO.Pension, .Health = fundXPO.Health, .Unemployment = fundXPO.Unemployment, .Risk = fundXPO.Risk}
            .FundType = INDgleFundType.EditValue
            .InitialDate = INDdteFundInitialDate.EditValue
            .MembershipNumber = INDtxtMembershipNumber.Text
            .State = True
            .VoluntaryContribution = INDgleVoluntaryContribution.EditValue
            .VoluntaryContributionValue = If(INDtxtVoluntaryContributionValue.Text.Trim.Equals(String.Empty), Nothing, INDtxtVoluntaryContributionValue.Text.Trim)
        End With

        If ctrAdd = 1 Then
            FundsDatasource.Add(FundToEdit)
        End If

        FundToEdit = Nothing
        fundXPO = Nothing

        INDsleFundIds.Focus()

        INDgrdListOfFunds.RefreshDataSource()
        CleanFundsPopup()
    End Sub

    ''' <summary>
    ''' Editar fondo
    ''' </summary>
    Private Sub INDbtnEditFund_Click(sender As Object, e As EventArgs) Handles INDbtnEditFund.Click
        FundToEdit = CType(INDgrdListOfFunds.DefaultView.GetRow(CType(INDgrdListOfFunds.DefaultView, DevExpress.XtraGrid.Views.Grid.GridView).FocusedRowHandle), FundContract)
        INDpceDetailsBook.ShowPopup()
        INDsleFundIds.EditValue = FundToEdit.FundId
        INDgleFundType.EditValue = FundToEdit.FundType
        INDdteFundInitialDate.EditValue = FundToEdit.InitialDate
        INDtxtMembershipNumber.Text = FundToEdit.MembershipNumber
        INDgleVoluntaryContribution.EditValue = FundToEdit.VoluntaryContribution
        INDtxtVoluntaryContributionValue.Text = FundToEdit.VoluntaryContributionValue
    End Sub

    ''' <summary>
    ''' Eliminar fondo
    ''' </summary>
    Private Sub INDbtnDeleteFund_Click(sender As Object, e As EventArgs) Handles INDbtnDeleteFund.Click
        If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim ToDelete = CType(INDgrdListOfFunds.DefaultView.GetRow(CType(INDgrdListOfFunds.DefaultView, DevExpress.XtraGrid.Views.Grid.GridView).FocusedRowHandle), FundContract)
            FundsDatasource.Remove(ToDelete)
            INDgrdListOfFunds.RefreshDataSource()
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
        'Deshacer()
    End Sub
#End Region


#Region "Handlers"
    Private Sub INDpceDetailsBook_CloseUp(sender As Object, e As CloseUpEventArgs) Handles INDpceDetailsBook.CloseUp
        If FlagEditModePopupBook Then
            CleanControlsPopup()
        End If
        If e.CloseMode = DevExpress.XtraEditors.PopupCloseMode.Cancel Then
            INDbtnAddContract.Focus()
        End If
    End Sub

    Private Sub CleanControlsPopup()
        INDsleFundIds.EditValue = Nothing

    End Sub

    Private Sub INDpceDetailsBook_Popup(sender As Object, e As EventArgs) Handles INDpceDetailsBook.Popup
        INDsleFundIds.Focus()
    End Sub

    ''' <summary>
    ''' Metodo que muestra el campo de valor de contribucion cuando se seleccione verdadero
    ''' </summary>
    Private Sub INDgleVoluntaryContribution_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleVoluntaryContribution.EditValueChanged
        If INDgleVoluntaryContribution.EditValue IsNot Nothing AndAlso INDgleVoluntaryContribution.EditValue = True Then
            INDlciVoluntaryContributionValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDlciVoluntaryContributionValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    ''' <summary>
    ''' Metodo que asigna el fondo seleccionado a la variable <see cref="fundXPO">fundXPO</see> para que se pueda validar al momento de guardar el fondo, ademas se muestra los tipos de fondos habilitados
    ''' </summary>
    Private Sub INDsleFundIds_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleFundIds.EditValueChanged

        Dim gv = CType(sender, SearchLookUpEdit)

        If INDsleFundIds.EditValue <> -1 Then
            'Fondo a agregar
            fundXPO = EmployeeHelper.ConvertFromSelectedRowInGridViewToXpoEntity(Of PayrollFundsXpo)(gv.Properties.View)

            Dim fundTypeDatasource As New List(Of Tuple(Of Integer, Byte, String))

            If fundXPO.Health Then
                fundTypeDatasource.Add(EmployeeHelper.FundsTypes.Where(Function(i) i.Item1 = Eresources.Salud).FirstOrDefault)
            End If

            If fundXPO.Pension Then
                fundTypeDatasource.Add(EmployeeHelper.FundsTypes.Where(Function(i) i.Item1 = Eresources.Pension).FirstOrDefault)
            End If

            If fundXPO.Unemployment Then
                fundTypeDatasource.Add(EmployeeHelper.FundsTypes.Where(Function(i) i.Item1 = Eresources.Cesantias).FirstOrDefault)
            End If

            If fundXPO.Risk Then
                fundTypeDatasource.Add(EmployeeHelper.FundsTypes.Where(Function(i) i.Item1 = Eresources.Riesgos).FirstOrDefault)
            End If

            If fundXPO.CompensationFund Then
                fundTypeDatasource.Add(EmployeeHelper.FundsTypes.Where(Function(i) i.Item1 = Eresources.CajaCompensacion).FirstOrDefault)
            End If

            'Se asigna el datasource 
            INDgleFundType.Properties.DataSource = fundTypeDatasource
            'Se selecciona por defecto el tipo si solo tiene un tipo de fondo
            If fundTypeDatasource.Count = 1 Then
                INDgleFundType.EditValue = fundTypeDatasource.FirstOrDefault.Item2
            End If

        End If
    End Sub


    Private Sub CtrContractEdit_Shown() Handles Me.GotFocus
        INDtxtContractId.Focus()
    End Sub

    ''' <summary>
    ''' Metodo que busca los parametros del tipo de contrato seleccionado
    ''' </summary>
    Private Sub INDsleContractTypeId_EditValueChanging(sender As Object, e As System.EventArgs) Handles INDsleContractTypeId.EditValueChanged
        If Not INDsleContractTypeId.EditValue = -1 Then
            Dim contractType As PayrollContractTypeXpo = EmployeeHelper.ConvertFromSelectedRowInGridViewToXpoEntity(Of PayrollContractTypeXpo)(INDgrvSleContractTypeId)
            IsTrialPeriodEnable = contractType.TestPeriod
            IsUndefinedEnable = contractType.Undefined
            INDgleSeveranceType.EditValue = contractType.SeveranceType
            INDsleJobBondingTypeId.EditValue = contractType.JobBondingTypeId.Id
            INDgleSalaryType.EditValue = contractType.SalaryType
            INDgleAutoRenew.EditValue = contractType.AutoRenew
            INDsleGroupId.Properties.DataSource = Nothing

            ContractClassSpecificBehavior(contractType.ContractClass)

            'Se limpian los otros campos si cambia el tipo de contrato
            INDsleGroupId.EditValue = -1
            INDsleFunctionalUnitId.EditValue = -1
            INDtxtCostCenterId.EditValue = Nothing
            'Se habilita la seleccion de fondos
            INDbtnAddFund.Enabled = True
        Else
            INDbtnAddFund.Enabled = False
            FundsDatasource.Clear()
            INDgrdListOfFunds.RefreshDataSource()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que busca los parametros del grupo seleccionado
    ''' </summary>
    Private Sub INDsleGroupId_EditValueChanging(sender As Object, e As System.EventArgs) Handles INDsleGroupId.EditValueChanged
        If Not INDsleGroupId.EditValue = -1 Then
            Dim groupSelected As PayrollGroupXpo = EmployeeHelper.ConvertFromSelectedRowInGridViewToXpoEntity(Of PayrollGroupXpo)(INDgrvSleGroup)
            INDglePaymentPeriod.EditValue = groupSelected.Liquidation
            INDsleFunctionalUnitId.Properties.DataSource = Nothing
            INDdteContractInitialDate.Properties.MinValue = groupSelected.FechaProximaLiquidacion
            Presenter.ChangeFunctionalUnitByCompany(groupSelected.CompanyId)

            INDsleFunctionalUnitId.EditValue = -1
            INDtxtCostCenterId.EditValue = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Metodo que busca los parametros de la unidad funcional seleccionada
    ''' </summary>
    Private Sub INDsleFunctionalUnitId_EditValueChanged(sender As Object, e As System.EventArgs) Handles INDsleFunctionalUnitId.EditValueChanged
        If Not INDsleFunctionalUnitId.EditValue = -1 Then
            FunctionalUnitXPO = EmployeeHelper.ConvertFromSelectedRowInGridViewToXpoEntity(Of PayrollFunctionalUnit)(INDgrvSleFunctionalUnit)
            INDtxtCostCenterId.EditValue = FunctionalUnitXPO.CostCenterId.Descripcion
        End If
    End Sub


    Private Sub INDdteContractInitialDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDdteContractInitialDate.EditValueChanged
        If INDdteContractInitialDate.EditValue IsNot Nothing Then
            INDdteContractEndingDate.Properties.MinValue = INDdteContractInitialDate.EditValue
            INDdteFundInitialDate.Properties.MinValue = INDdteContractInitialDate.EditValue
            INDdteFundInitialDate.EditValue = INDdteContractInitialDate.EditValue
        End If
    End Sub

    ''' <summary>
    ''' Metodo para leer los parametros del cargo y aplicar las validaciones necesarias
    ''' </summary>
    Private Sub INDslePositionId_EditValueChanged(sender As Object, e As EventArgs) Handles INDslePositionId.EditValueChanged
        If INDslePositionId.EditValue <> -1 Then
            positionXPO = EmployeeHelper.ConvertFromSelectedRowInGridViewToXpoEntity(Of PayrollPositionXpo)(INDslePositionId.Properties.View)
            INDspnBasicSalary.Properties.MinValue = positionXPO.MinBasicSalary
            INDspnBasicSalary.Properties.MaxValue = positionXPO.MaxBasicSalary
            INDspnProfessionalRiskPercentage.EditValue = positionXPO.ProfessionalRiskLevelId.Percentage
            'TODO Agregar Recurso
            Mensaje(EeventViewerImages.Informacion) = String.Format(obtenerRecurso(ContratosSalarioVsCargo, Contrato), positionXPO.MinBasicSalary, positionXPO.MaxBasicSalary)
        End If
    End Sub

    Private Sub INDspnContractLength_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDspnContractLengthYear.EditValueChanging, INDspnContractLengthMonth.EditValueChanging, INDspnContractLengthDay.EditValueChanging
        If String.IsNullOrEmpty(e.NewValue) OrElse CInt(e.NewValue) < 0 Then
            e.Cancel = True
        End If
    End Sub

    Private Sub INDspnContractLength_EditValueChanged(sender As Object, e As EventArgs) Handles INDspnContractLengthYear.EditValueChanged, INDspnContractLengthMonth.EditValueChanged, INDspnContractLengthDay.EditValueChanged, INDdteContractInitialDate.EditValueChanged

        RemoveHandler INDdteContractEndingDate.EditValueChanged, AddressOf INDdteContractDate_EditValueChanged
        If INDdteContractInitialDate.EditValue IsNot Nothing Then

            Dim r = EmployeeHelper.CalculateContractEndingDate(INDdteContractInitialDate.EditValue, INDspnContractLengthYear.EditValue, INDspnContractLengthMonth.EditValue, INDspnContractLengthDay.EditValue)

            If CInt(INDspnContractLengthYear.EditValue) > 0 OrElse CInt(INDspnContractLengthMonth.EditValue) > 0 OrElse CInt(INDspnContractLengthDay.EditValue) > 0 Then
                INDdteContractEndingDate.EditValue = r.AddDays(-1)

                Dim cd = If(ContractToEdit IsNot Nothing, ContractToEdit.ContractEndingDate, Nothing)

                If indigo.IndigoCompany = "2" Then

                    If ValidateContractLengths(INDdteContractInitialDate.EditValue, INDdteContractEndingDate.EditValue, MinMonthsContractLength, MaxMonthsContractLength, cd) = False Then
                        'INDdteContractInitialDate.EditValue = Nothing
                        INDspnContractLengthYear.EditValue = 0
                        INDspnContractLengthMonth.EditValue = 0
                        INDspnContractLengthDay.EditValue = 0
                        INDdteContractEndingDate.EditValue = Nothing
                    End If
                End If
            Else
                INDdteContractEndingDate.EditValue = Nothing
            End If
        Else
            INDdteContractEndingDate.EditValue = Nothing
        End If
        AddHandler INDdteContractEndingDate.EditValueChanged, AddressOf INDdteContractDate_EditValueChanged

    End Sub

    Private Sub INDdteContractDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDdteContractEndingDate.EditValueChanged, INDdteContractInitialDate.EditValueChanged

        If INDdteContractEndingDate.EditValue IsNot Nothing AndAlso INDdteContractInitialDate.EditValue IsNot Nothing AndAlso Not _contractClass = ClaseContratoLaboralIndefinido Then

            Dim cd = If(ContractToEdit IsNot Nothing, ContractToEdit.ContractEndingDate, Nothing)

            If ValidateContractLengths(INDdteContractInitialDate.EditValue, INDdteContractEndingDate.EditValue, MinMonthsContractLength, MaxMonthsContractLength, cd) = False Then
                INDdteContractInitialDate.EditValue = Nothing
                INDdteContractEndingDate.EditValue = Nothing
            Else
                Dim dts = DateTimeSpan.CompareDates(CDate(INDdteContractEndingDate.EditValue).AddDays(1), INDdteContractInitialDate.EditValue)

                INDspnContractLengthYear.EditValue = dts.Years
                INDspnContractLengthMonth.EditValue = dts.Months
                INDspnContractLengthDay.EditValue = dts.Days
            End If
        End If
    End Sub
#End Region

#Region "Load"
    ''' <summary>
    ''' Evento Load donde se ejecutan el asincrono para levantar la definicion del layout
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmContract_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Creamos un nuevo objeto con la cultura definida
        Dim currentCulture As Globalization.CultureInfo = New Globalization.CultureInfo(SessionValues.Instance.Culture.Name)
        'Cargamos de manera asincrona definiciones del funcional
        BarraBotones.PrepareToolbar(Presentation.Controls.eAction.OnlyUndo)
        BarraBotones.OperatingUnitVisible = False
        If indigo.IndigoCompanyType = "2" Then
            INDlciResolutionNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlciResolutionDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlciPosesionDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlciCertificateOfficeNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If

        PathFunctionalDefinitions = String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name, "\ModuloPayroll", Me.Name, ".xml")
        modelBusqueda = New MBusqueda()
        LoadhronousDefinitions = New BackgroundWorker
        If LoadhronousDefinitions.IsBusy = False Then
            LoadhronousDefinitions.RunWorkerAsync()
        End If

        'Verifica si esta en modo de diseño para no realizar los llamados de deshacer
        If Not IsDesignMode Then
            Presenter = New PContract(Me)
            Initializes()
            CleanControls()
        End If

        changeNumericFormatByCurrency(CurrencyAbbreviation.GetNumberFormat)
        INDspnBasicSalary.Properties.Mask.Culture = currentCulture
        INDtxtVoluntaryContributionValue.Properties.Mask.Culture = currentCulture
        INDtxtContractId.EditValue = obtenerRecurso(LabelNuevo, RecepcionObjeciones)

        InitializeTuple()

        GetEmployeeToWorkWith()
    End Sub
#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al oprimir escape para cerrar el popup de edición de contrato
    ''' </summary>
    Private Sub FrmContractEdit_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub INDpceDetailsBook_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDpceDetailsBook.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.F4 OrElse e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDpceDetailsBook.ShowPopup()
        End If
    End Sub

#End Region

#Region "FormClosing"

    Private Sub FrmContractEdit_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If EmployeeContract.Contract.Count <= 0 Then
            If Not MessageIndigo.Show("Al cerrar el formulario se perderá la información que habia registrado", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                e.Cancel = True
            End If
        End If
    End Sub

#End Region

#Region "Properthies"
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



End Class