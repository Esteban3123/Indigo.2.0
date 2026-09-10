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
Imports Domain.Payroll.Entities
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Base
Imports Presentation.Payroll.MVP
Imports Infrastructure.CrossCutting.Base
Imports DevExpress.Utils.Design.DesignTimeTools
Imports System.ComponentModel
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports Presentation.Controls.MVP
Imports Presentation.CloudAgent
Imports Domain.Base.Entities
Imports Presentation.Common.MVP
#End Region

Public Class FrmContracModify
    Implements IContract
#Region "Builders"
    Public Sub New(ByRef employee As Employee, Optional contractNumber As Integer = 0, Optional contractAction As ContractActions = Base.ContractActions.NewContractWithoutPayments)
        InitializeComponent()
        Me.EmployeeContract = employee
        Me.ContractNumber = contractNumber
        Me.ContractAction = contractAction
    End Sub
#End Region

    Private Sub AddActionsColumns()
        Dim ListActions As New List(Of eAcciones)
        Dim s = CType(INDgrvGrdListOfFunds.GetRow(INDgrvGrdListOfFunds.FocusedRowHandle), FundContract)
        If s IsNot Nothing Then
            If s.State = False Then
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(FondoAccionesInactivos, Contrato)
                Exit Sub
            End If
            If FundsPreviouslyCreatedDatasource.Where(Function(i) i.FundId = s.FundId AndAlso i.FundType = s.FundType AndAlso i.State = s.State).Count > 0 Then

                ListActions.Add(eAcciones.Edit)
            End If
            IndigoGridView1.SetListAcction(INDgrvGrdListOfFunds, ListActions)
            For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDgrvGrdListOfFunds.Columns
                If col.Name = "colActions" OrElse col.Name = "MoreInfo" Then
                    col.Width = 75
                End If
            Next
        End If
    End Sub

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
    ''' Modelo del busqueda
    ''' </summary>
    ''' <remarks></remarks>
    Dim modelBusqueda As MBusqueda

    ''' <summary>
    ''' Evento que se utiliza para cargar las definiciones del funcional
    ''' </summary>
    WithEvents LoadhronousDefinitions As BackgroundWorker

    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Private _presenter As PContract
    Private ReadOnly Property Presenter As PContract
        Get
            If _presenter Is Nothing Then
                _presenter = New PContract(Me)
            End If
            Return _presenter
        End Get
    End Property

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

    ''' <summary>
    ''' Propiedad que contiene si la accion principal del control fue ejecutada
    ''' </summary>
    Private _actionExecuted As Boolean

    ''' <summary>
    ''' Propiedad 
    ''' </summary>
    Dim FlagDownSalary As Boolean = False

    Public Property ActionExecuted() As Boolean
        Get
            Return _actionExecuted
        End Get
        Private Set(ByVal value As Boolean)
            _actionExecuted = value
        End Set
    End Property

    Public Property Contingency As Byte? Implements IContract.Contingency
        Get
            Return INDSlContingency.EditValue
        End Get
        Set(value As Byte?)
            INDSlContingency.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Parámetro ACCAI
    ''' </summary>
    ''' <returns></returns>
    Public Property ACCAIParameter As Boolean?
        Get
            Return CtrYesNoACCAI.EditValue
        End Get
        Set(value As Boolean?)
            CtrYesNoACCAI.EditValue = value
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
    ''' Propiedad que contiene el datasource las motivos de modificaciones de contrato
    ''' </summary>
    Public WriteOnly Property ContractModificationReasonsXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements IContract.ContractModificationReasonsXPO
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleContractModificationReason.Properties.DataSource = value
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
    ''' Propiedad que contiene el registro de fondos creados anteriormente
    ''' </summary>
    Private _fundsPreviouslyCreatedDatasource As List(Of FundContract)
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
    Private Property FundsPreviouslyCreatedDatasource As List(Of FundContract)
        Get
            If _fundsPreviouslyCreatedDatasource Is Nothing Then
                _fundsPreviouslyCreatedDatasource = New List(Of FundContract)
            End If
            Return _fundsPreviouslyCreatedDatasource
        End Get
        Set(value As List(Of FundContract))
            _fundsPreviouslyCreatedDatasource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene si el tipo de accion con el contrato genera un otro si o solo modificacion de los valores del contrato a modificar
    ''' </summary>
    Private _contractModification As Boolean
    Public WriteOnly Property ContractModification() As Boolean
        Set(ByVal value As Boolean)
            _contractModification = value
            ContractModificationSpecificBehavior()
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
    ''' Variable que contiene el grupo seleccionado
    ''' </summary>
    Private GroupXPO As PayrollGroupXpo

    ''' <summary>
    ''' Variable que contiene si un fondo se encuentra en eliminacion
    ''' </summary>
    Private deletingFundDate As DateTime

    ''' <summary>
    ''' Variable que contiene el tiempo minimo de un contrato en meses
    ''' </summary>
    Private MinMonthsContractLength As Integer

    ''' <summary>
    ''' Variable que contiene el tiempo maximo de un contrato en meses
    ''' </summary>
    Private MaxMonthsContractLength As Integer

    ''' <summary>
    ''' 
    ''' </summary>
    Private CanOverrideContractLength As Boolean
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

    ''' <summary>
    ''' Meses permitidos para ajustes extemporáneos (desde PayrollSettings - Configuración Global)
    ''' </summary>
    ''' <remarks>
    ''' ANTES: No existía este parámetro
    ''' DESPUÉS: Se usa para configurar el rango de fechas permitidas en ajustes extemporáneos
    ''' Rango válido: 0 a 6 meses
    ''' 0 = Ajustes extemporáneos deshabilitados
    ''' NOTA: Se lee de PayrollSettings (configuración global), NO de PayrollParameter (por grupo)
    ''' </remarks>
    Private _allowedMonthsForExtemporaneousAdjustments As Byte = 0

    ''' <summary>
    ''' Indica si la fecha de inicio seleccionada es extemporánea
    ''' </summary>
    Private _isExtemporaneousDate As Boolean = False
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

    Public Sub GetEmployeeToWorkWith()
        Me.ContractAction = ContractAction
        CleanControlsBasic()

        ' ANTES: No se cargaban parámetros de ajustes extemporáneos
        ' DESPUÉS: Se cargan los parámetros de nómina para configurar ajustes extemporáneos
        LoadPayrollParameters()

        If Me.EmployeeContract IsNot Nothing Then
            'Me.EmployeeContract = Employee

            ContractToEdit = Me.EmployeeContract.Contract.Where(Function(c) c.Id = ContractNumber).FirstOrDefault()

            If ContractToEdit Is Nothing Then
                'TODO notificar el error de que no se encuentra el contrato a editar
                Return
            Else
                LoadInfoInControl(ContractToEdit)
                'Verificamos si genera otro si o solo modifica valores del contrato
                If _contractModification = True Then
                    INDtxtContractId.EditValue = obtenerRecurso(ContratosRenovacion, Contrato)
                    INDsleContractModificationReason.Focus()
                Else
                    INDtxtContractId.EditValue = ContractToEdit.Id
                    INDspnProfessionalRiskPercentage.Focus()
                End If
            End If
        Else
            Me.EmployeeContract = Nothing
            Contract = Nothing
        End If

        Dim ContractPermiso = IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.ListPermissionsUserToolbar(indigo.UserIndigoId, indigo.UserRol, "529", indigo)
        For i As Integer = 0 To ContractPermiso.Count() - 1
            If ContractPermiso.Item(i).TagButton = 117 Then
                FlagDownSalary = True
            End If
        Next
    End Sub

    ''' <summary>
    ''' Carga los parámetros de nómina, específicamente el de ajustes extemporáneos
    ''' </summary>
    ''' <remarks>
    ''' ANTES: No existía este método
    ''' DESPUÉS: Carga AllowedMonthsForExtemporaneousAdjustments desde PayrollSettings (configuración global)
    ''' para configurar el rango de fechas permitidas en el control de fecha de inicio
    ''' NOTA: Se cambió de PayrollParameter a PayrollSettings para usar configuración global única
    ''' </remarks>
    Private Sub LoadPayrollParameters()
        Try
            ' Usar el servicio XPO para obtener la configuración de nómina
            Dim payrollSettingsXpo As PayrollSettingsXpo = XpoServiceEx.Instance(indigo.TransactionalContainer).PayrollService.GetCollection(Of PayrollSettingsXpo)().FirstOrDefault()

            If payrollSettingsXpo IsNot Nothing Then
                _allowedMonthsForExtemporaneousAdjustments = payrollSettingsXpo.AllowedMonthsForExtemporaneousAdjustments
            Else
                _allowedMonthsForExtemporaneousAdjustments = 0
            End If
        Catch ex As Exception
            ' Si hay error al cargar parámetros, deshabilitar ajustes extemporáneos
            _allowedMonthsForExtemporaneousAdjustments = 0
            Mensaje(EeventViewerImages.Advertencia) = "No se pudieron cargar la configuración de nómina. Los ajustes extemporáneos estarán deshabilitados."
        End Try
    End Sub

    ''' <summary>
    ''' Inicializa los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Initializes()
        INDgleSeveranceType.Properties.DataSource = EmployeeHelper.SeveranceType
        INDgleSalaryType.Properties.DataSource = EmployeeHelper.SalaryType
        INDglePaymentPeriod.Properties.DataSource = EmployeeHelper.PaymentPeriod
        INDglePaymentType.Properties.DataSource = EmployeeHelper.PaymentType
        INDgleAutoRenew.Properties.DataSource = EmployeeHelper.YesNoBoolean.ToList
        INDgleBankAccountType.Properties.DataSource = EmployeeHelper.BankAccountType
        INDgleVoluntaryContribution.Properties.DataSource = EmployeeHelper.YesNoBoolean.ToList
        INDgrdListOfFunds.DataSource = FundsDatasource
        ActionExecuted = False
        CanOverrideContractLength = False
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
        INDsleContractModificationReason.EditValue = -1

        INDtxtContractId.EditValue = Nothing

        INDSlContingency.EditValue = 0
        Contingency = 0
        INDdteContractEndingDate.EditValue = Nothing
        INDdteContractInitialDate.EditValue = Nothing
        INDspnContractLengthYear.EditValue = 0
        INDspnContractLengthMonth.EditValue = 0
        INDspnContractLengthDay.EditValue = 0
        INDTxtDailyHours.EditValue = 8

        INDtxtResolutionNumber.EditValue = Nothing
        INDdteResolutionDate.EditValue = Nothing
        INDtxtCertificateOfficeNumber.EditValue = Nothing
        INDdtePosesionDate.EditValue = Nothing

        INDspnProfessionalRiskPercentage.EditValue = Nothing
        INDsleEmployeeType.Properties.NullText = String.Empty
        INDsleEmployeeType.EditValue = Nothing
        INDgleSeveranceType.EditValue = Nothing

        INDtxtCostCenterId.EditValue = Nothing
        INDsleWorkCenterId.Properties.NullText = String.Empty
        INDsleWorkCenterId.EditValue = Nothing
        INDgleAutoRenew.EditValue = Nothing
        INDmemNotes.EditValue = Nothing
        INDgleBankAccountType.EditValue = Nothing
        INDglePaymentPeriod.EditValue = Nothing
        INDglePaymentType.EditValue = Nothing
        INDgleSalaryType.EditValue = Nothing

        INDsleBankId.Properties.NullText = String.Empty
        INDsleBankId.EditValue = -1
        INDsleContractTypeId.Properties.NullText = String.Empty
        INDsleContractTypeId.EditValue = -1
        INDsleFunctionalUnitId.Properties.NullText = String.Empty
        INDsleFunctionalUnitId.EditValue = -1
        INDsleGroupId.Properties.NullText = String.Empty
        INDsleGroupId.EditValue = -1
        INDsleJobBondingTypeId.Properties.NullText = String.Empty
        INDsleJobBondingTypeId.EditValue = -1
        INDslePositionId.Properties.NullText = String.Empty
        INDslePositionId.EditValue = -1

        INDtxtBankAccountNumber.EditValue = Nothing
        INDspnBasicSalary.EditValue = Nothing
        INDspnTrialPeriodSalaryPercentage.EditValue = Nothing
        INDspnTrialPeriodTime.EditValue = Nothing
        ACCAIParameter = 0
        'Fondos
        FundsDatasource.Clear()
        INDgrdListOfFunds.RefreshDataSource()
        CleanFundsPopup()

        CanOverrideContractLength = False
        INDtxtBankAccountNumber.Properties.Mask.MaskType = Mask.MaskType.RegEx
        INDtxtBankAccountNumber.Properties.Mask.EditMask = "\d+"
    End Sub

    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    Private Function ValidateControls(Optional clone As Boolean = False) As Boolean
        ValidateControls = True

        If clone Then
            'genera otro si y se tiene que verificar los campos de generacion de otro si

            If INDsleContractModificationReason.EditValue Is Nothing OrElse INDsleContractModificationReason.EditValue = -1 Then
                INDsleContractModificationReason.Focus()
                ValidateControls = False
                Exit Function
            End If

            If INDsleContractTypeId.EditValue Is Nothing OrElse INDsleContractTypeId.EditValue = -1 Then
                INDsleContractTypeId.Focus()
                ValidateControls = False
                Exit Function
            End If

            If INDslePositionId.EditValue Is Nothing OrElse INDslePositionId.EditValue = -1 Then
                INDsleBankId.Focus()
                ValidateControls = False
                Exit Function
            End If

            If INDspnBasicSalary.EditValue Is Nothing OrElse INDspnBasicSalary.EditValue.ToString.Trim.Equals(String.Empty) Then
                INDspnBasicSalary.Focus()
                ValidateControls = False
                Exit Function
            End If

            If INDdteContractInitialDate.EditValue Is Nothing OrElse INDdteContractInitialDate.EditValue.ToString.Trim.Equals(String.Empty) Then
                INDdteContractInitialDate.Focus()
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

        End If

        '-------------------------- Modificacion de contrato basica

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

        If _contractClass = Eresources.ClaseContratoAprendizaje AndAlso (INDspnProfessionalRiskPercentage.EditValue Is Nothing OrElse INDspnProfessionalRiskPercentage.EditValue < 0) AndAlso EmployeeContract.Pensionary = False Then
            INDspnProfessionalRiskPercentage.Focus()
            ValidateControls = False
            Exit Function
        ElseIf _contractClass <> Eresources.ClaseContratoAprendizaje AndAlso (INDspnProfessionalRiskPercentage.EditValue Is Nothing OrElse INDspnProfessionalRiskPercentage.EditValue <= 0) Then
            INDspnProfessionalRiskPercentage.Focus()
            ValidateControls = False
            Exit Function
        End If

        If INDsleEmployeeType.EditValue Is Nothing OrElse INDsleEmployeeType.EditValue = -1 Then
            INDsleEmployeeType.Focus()
            ValidateControls = False
            Exit Function
        End If

        If INDsleBankId.EditValue Is Nothing OrElse INDsleBankId.EditValue = -1 Then
            INDsleBankId.Focus()
            ValidateControls = False
            Exit Function
        End If

        If INDsleGroupId.EditValue Is Nothing OrElse INDsleGroupId.EditValue = -1 Then
            INDsleGroupId.Focus()
            ValidateControls = False
            Exit Function
        End If

        If INDsleFunctionalUnitId.EditValue Is Nothing OrElse INDsleFunctionalUnitId.EditValue = -1 Then
            INDsleFunctionalUnitId.Focus()
            ValidateControls = False
            Exit Function
        End If

        If INDsleJobBondingTypeId.EditValue Is Nothing OrElse INDsleJobBondingTypeId.EditValue = -1 Then
            INDsleJobBondingTypeId.Focus()
            ValidateControls = False
            Exit Function
        End If

        If INDtxtBankAccountNumber.Text Is Nothing OrElse INDtxtBankAccountNumber.Text.Trim.Equals(String.Empty) Then
            INDtxtBankAccountNumber.Focus()
            ValidateControls = False
            Exit Function
        End If

        If IsTrialPeriodEnable Then

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

        If INDdteFundInitialDate.EditValue Is Nothing OrElse INDdteFundInitialDate.EditValue.Equals(String.Empty) Then
            INDdteFundInitialDate.Focus()
            ValidateFundsControls = False
            Exit Function
        End If

        If INDdteFundEndingDate.EditValue IsNot Nothing AndAlso INDdteFundInitialDate.EditValue IsNot Nothing Then
            Dim x = CType(INDdteFundEndingDate.EditValue, Date).Subtract(CType(INDdteFundInitialDate.EditValue, Date))
            If x.Days < 0 Then
                INDdteFundEndingDate.Focus()
                ValidateFundsControls = False
                Exit Function
            End If
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
    Private Function ValidateFundsLogic() As Boolean
        ValidateFundsLogic = True

        If _contractClass = Eresources.ClaseContratoOtros Then
            'La clase de contrato otros no debe tener fondos
            ValidateFundsLogic = False
            Exit Function
        Else
            If FundsDatasource.Where(Function(i) i.FundType = INDgleFundType.EditValue AndAlso i.State = True AndAlso deletingFundDate = Nothing And i.VoluntaryContribution = INDgleVoluntaryContribution.EditValue).Count > 0 Then
                'TODO agregar a recursos
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(FondoYaSeEncuentra, Contrato)
                ValidateFundsLogic = False
                Exit Function
            End If

            Dim fundOnEditId = If(FundToEdit IsNot Nothing, FundToEdit.Id, 0)

            Dim latestFund = FundsDatasource.Where(Function(i) i.FundType = INDgleFundType.EditValue AndAlso i.Id <> fundOnEditId And i.VoluntaryContribution = INDgleVoluntaryContribution.EditValue And i.State = True).OrderByDescending(Function(i) i.Id).FirstOrDefault

            If latestFund IsNot Nothing Then
                Dim endingDate = If(deletingFundDate = #12:00:00 AM#, latestFund.EndingDate, deletingFundDate)

                Dim daysBetween = DateDiff(DateInterval.Day, CType(endingDate, Date), CType(INDdteFundInitialDate.EditValue, Date))

                If daysBetween > 1 AndAlso INDgleVoluntaryContribution.EditValue = False Then
                    'TODO agregar a recursos
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(FondoFechasEntreAfiliaciones, Contrato)
                    ValidateFundsLogic = False
                ElseIf daysBetween < 0 OrElse (daysBetween = 0 AndAlso fundOnEditId = 0) Then
                    'TODO agregar a recursos
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(FondoFechasEnRangoFondo, Contrato)
                    ValidateFundsLogic = False
                End If
            End If

        End If

    End Function

    ''' <summary>
    ''' Metodo para relizar las validaciones de logica de funcionamiento de un contrato
    ''' </summary>
    Private Function ValidateContractSpecifics() As Boolean
        ValidateContractSpecifics = True

        Dim cont As Contract
        Dim tmpContractId = If(INDtxtContractId.EditValue.ToString.Equals(obtenerRecurso(ContratosRenovacion, Contrato)), 0, INDtxtContractId.EditValue)
        'Verifica que no se creen 2 contratos vigentes activos
        cont = EmployeeContract.Contract.Where(Function(c) (Not c.Id = tmpContractId AndAlso (c.Valid = True AndAlso c.Status = CType(1, Byte)))).FirstOrDefault
        If Not ContractAction = ContractActions.EditContract AndAlso cont IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ContratosNo2Vigentes, Contrato)
            ValidateContractSpecifics = False
            Exit Function
        End If

        'Fecha de inicio del contrato no puede estar metida en medio de un contrato vigente
        cont = EmployeeContract.Contract.Where(Function(c) Not c.Id = tmpContractId AndAlso c.ContractInitialDate.CompareTo(INDdteContractInitialDate.EditValue) <= 0 AndAlso c.Valid = True AndAlso c.Status = CType(1, Byte)).FirstOrDefault
        If Not ContractAction = ContractActions.EditContract AndAlso cont IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ContratosEnRangoOtroContrato, Contrato)
            ValidateContractSpecifics = False
            Exit Function
        End If

        Dim msj = String.Empty

        If EmployeeContract.PensionaryStatus Then
            If FundsDatasource.Where(Function(i) i.FundType = EmployeeHelper.FundsTypes.Where(Function(j) j.Item1 = Eresources.Salud).FirstOrDefault.Item2 AndAlso i.State = True).Count <= 0 Then
                'TODO agregar recursos
                msj += obtenerRecurso(FondoSaludObligatorio, Contrato) + vbCrLf
                ValidateContractSpecifics = False
            End If
            'Muestra los mensajes en caso de existir
            If Not msj.Equals(String.Empty) Then
                Mensaje(EeventViewerImages.Advertencia) = msj
            End If
            Exit Function
        End If

        If _contractClass = Eresources.ClaseContratoAprendizaje Then
            'minimo salud
            If FundsDatasource.Where(Function(i) i.FundType = EmployeeHelper.FundsTypes.Where(Function(j) j.Item1 = Eresources.Salud).FirstOrDefault.Item2 AndAlso i.State = True).Count <= 0 Then
                'TODO agregar recursos
                msj += obtenerRecurso(FondoSaludObligatorio, Contrato) + vbCrLf
                ValidateContractSpecifics = False
            End If
        ElseIf _contractClass = Eresources.ClaseContratoAprendizajePractica Then
            'minimo salud y arp
            If FundsDatasource.Where(Function(i) i.FundType = EmployeeHelper.FundsTypes.Where(Function(j) j.Item1 = Eresources.Riesgos).FirstOrDefault.Item2 AndAlso i.State = True).Count <= 0 Then
                'TODO agregar recursos
                msj += obtenerRecurso(FondoArlObligatorio, Contrato) + vbCrLf
                ValidateContractSpecifics = False
            End If

            If FundsDatasource.Where(Function(i) i.FundType = EmployeeHelper.FundsTypes.Where(Function(j) j.Item1 = Eresources.Salud).FirstOrDefault.Item2 AndAlso i.State = True).Count <= 0 Then
                'TODO agregar recursos
                msj += obtenerRecurso(FondoSaludObligatorio, Contrato) + vbCrLf
                ValidateContractSpecifics = False
            End If
        ElseIf _contractClass = Eresources.ClaseContratoLaboralFijo OrElse _contractClass = Eresources.ClaseContratoLaboralIndefinido Then
            'minimo salud, pension y arp
            If FundsDatasource.Where(Function(i) i.FundType = EmployeeHelper.FundsTypes.Where(Function(j) j.Item1 = Eresources.Riesgos).FirstOrDefault.Item2 AndAlso i.State = True).Count <= 0 Then
                'TODO agregar recursos
                msj += obtenerRecurso(FondoArlObligatorio, Contrato) + vbCrLf
                ValidateContractSpecifics = False
            End If

            If FundsDatasource.Where(Function(i) i.FundType = EmployeeHelper.FundsTypes.Where(Function(j) j.Item1 = Eresources.Salud).FirstOrDefault.Item2 AndAlso i.State = True).Count <= 0 Then
                'TODO agregar recursos
                msj += obtenerRecurso(FondoSaludObligatorio, Contrato) + vbCrLf
                ValidateContractSpecifics = False
            End If

            If EmployeeContract.Pensionary = False Then
                If FundsDatasource.Where(Function(i) i.FundType = EmployeeHelper.FundsTypes.Where(Function(j) j.Item1 = Eresources.Pension).FirstOrDefault.Item2 AndAlso i.State = True).Count <= 0 Then
                    'TODO agregar recursos
                    msj += obtenerRecurso(FondoPensionesObligatorio, Contrato) + vbCrLf
                    ValidateContractSpecifics = False
                End If
            End If
            If ACCAIParameter AndAlso Not FundsDatasource.Any(Function(x) x.Fund.PensionACCAI.GetValueOrDefault(False)) Then 'x.ParameterACCAI.GetValueOrDefault(False)
                msj += "Esta pendiente seleccionar la ACCAI" + vbCrLf
                ValidateContractSpecifics = False
            End If
        End If

        'Muestra los mensajes en caso de existir
        If Not msj.Equals(String.Empty) Then
            Mensaje(EeventViewerImages.Advertencia) = msj
        End If
    End Function

    ''' <summary>
    ''' Metodo para validar si se han hechos cambios en los campos que generan otro si
    ''' </summary>
    ''' <returns>Retorna Verdadero si se han realizado cambios, Falso en caso contrario</returns>
    Private Function ValidateRequiredChanges() As Boolean
        ValidateRequiredChanges = False

        If INDsleContractTypeId.EditValue IsNot Nothing AndAlso Not ContractToEdit.ContractTypeId.Equals(INDsleContractTypeId.EditValue) Then
            ValidateRequiredChanges = True
        ElseIf INDslePositionId.EditValue IsNot Nothing AndAlso Not ContractToEdit.PositionId.Equals(INDslePositionId.EditValue) Then
            ValidateRequiredChanges = True
        ElseIf INDspnBasicSalary.EditValue IsNot Nothing AndAlso Not ContractToEdit.BasicSalary.Equals(INDspnBasicSalary.EditValue) Then
            ValidateRequiredChanges = True
        ElseIf ACCAIParameter IsNot nothing Andalso Not ACCAIParameter.Equals(ACCAIParameter) Then
            ValidateRequiredChanges = True
        ElseIf CanOverrideContractLength = False Then
            If INDdteContractInitialDate.EditValue IsNot Nothing AndAlso Not ContractToEdit.ContractInitialDate.Equals(INDdteContractInitialDate.EditValue) Then
                ValidateRequiredChanges = True
            ElseIf INDdteContractEndingDate.EditValue IsNot Nothing AndAlso Not ContractToEdit.ContractEndingDate.Equals(INDdteContractEndingDate.EditValue) Then
                ValidateRequiredChanges = True
            End If
        ElseIf CanOverrideContractLength = True Then
            If INDdteContractInitialDate.EditValue IsNot Nothing AndAlso Not ContractToEdit.ContractInitialDate.Equals(INDdteContractInitialDate.EditValue) Then
                ValidateRequiredChanges = True
            End If
        End If

    End Function

    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues(Optional clone As Boolean = False)

        'Se verifica si es una modificacion del contrato o solo un cambio de los parametros del contrato actual
        If _contractModification = True AndAlso clone = True Then
            Contract = EmployeeHelper.CloneContract(ContractToEdit)
            'Otro si
            With Contract
                .RowType = 2 'Contrato Base
                .CreationUserId = indigo.UserIndigo 'Usuario de creacion
                .ContractCreationDate = Date.Today
                .ModificationUserId = indigo.UserIndigo 'Usuario de modificacion
                .ModificationDate = Date.Today 'Fecha de modificación
                .LastModificationDate = Date.Today 'Ultima fecha de modificacion
                .Status = 1 'Contrato Activo
                .InitialContractNumber = If(ContractToEdit.RowType = 1, ContractToEdit.Id, ContractToEdit.InitialContractNumber) 'Contrato inicial 0, hace referencia a los contratos base
                .JobBondingDate = ContractToEdit.JobBondingDate
                .LiquidationPayroll = 1 'Liquida nomina
                .Valid = 1 'Contrato vigente

                '--------------------------------- Campos que generan otro si
                .ContractEndingDate = If(_contractClass = ClaseContratoLaboralIndefinido, New Date(9999, 12, 31), INDdteContractEndingDate.EditValue)
                .ContractInitialDate = INDdteContractInitialDate.EditValue
                .ContractTypeName = INDsleContractTypeId.Text
                .SalaryType = INDgleSalaryType.EditValue
                .JobBondingName = INDsleJobBondingTypeId.Text
                .ContractClass = _contractClassDB
                .ContractTypeId = INDsleContractTypeId.EditValue
                .BasicSalary = CDec(INDspnBasicSalary.EditValue)
                .BaseIncome = CDec(INDspnBasicSalary.EditValue) 'Contrato nuevo tiene ingreso base el mismo del sueldo
                .PositionName = INDslePositionId.Text
                .PositionId = INDslePositionId.EditValue
                .ParameterACCAI = ACCAIParameter

                '' Contratos Públicos

                If INDdteResolutionDate.EditValue IsNot Nothing Then
                    .ResolutionDate = INDdteResolutionDate.EditValue
                End If

                If INDtxtResolutionNumber.EditValue IsNot Nothing Then
                    .ResolutionNumber = INDtxtResolutionNumber.EditValue
                End If

                If INDtxtCertificateOfficeNumber.EditValue IsNot Nothing Then
                    .CertificateOfficeNumber = INDtxtCertificateOfficeNumber.EditValue
                End If

                If INDdtePosesionDate.EditValue IsNot Nothing Then
                    .PosesionDate = INDdtePosesionDate.EditValue
                End If

            End With

            With ContractToEdit
                .Valid = 0 'Contrato no vigente
                .Status = 4
                .InitialContractNumber = If(ContractToEdit.RowType = 1, ContractToEdit.Id, ContractToEdit.InitialContractNumber)
                .ContractEndingDate = IIf(ContractToEdit.ContractInitialDate = Contract.ContractInitialDate, ContractToEdit.ContractInitialDate, CDate(INDdteContractInitialDate.EditValue).AddDays(-1))
                .ContractModificationReasonId = INDsleContractModificationReason.EditValue
            End With

            If ContractToEdit.ContractInitialDate = Contract.ContractInitialDate Then
                Mensaje(EeventViewerImages.Informacion) = "Ambos Contratos tienen la misma FECHA DE INICIO"
            End If

        ElseIf (_contractModification = False AndAlso clone = False) OrElse (_contractModification = True AndAlso clone = False) Then
            Contract = ContractToEdit
            'Modificacion de parametros
            With Contract

                .ModificationUserId = indigo.UserIndigo 'Usuario de modificacion
                .ModificationDate = Date.Today 'Fecha de modificación
                .LastModificationDate = Date.Today 'Ultima fecha de modificacion
            End With
        Else
            Return
        End If
        'Valores del contrato
        With Contract
            '---------------------------------------------------------------------------------
            .BankId = INDsleBankId.EditValue
            .BankAccountNumber = INDtxtBankAccountNumber.EditValue
            .BankAccountType = INDgleBankAccountType.EditValue

            .Contingency = Contingency
            .FunctionalUnitName = INDsleFunctionalUnitId.Text
            .FunctionalUnitId = INDsleFunctionalUnitId.EditValue
            .GroupName = INDsleGroupId.Text
            .GroupId = INDsleGroupId.EditValue
            .Notes = INDmemNotes.EditValue
            .PaymentPeriod = INDglePaymentPeriod.EditValue
            .PaymentType = INDglePaymentType.EditValue
            .HoursDaily = INDTxtDailyHours.EditValue
            .TrialPeriod = False
            .TrialPeriodSalaryPercentage = 0
            .TrialPeriodTime = 0
            .ParameterACCAI = ACCAIParameter
            '' Contratos Públicos
            If INDdteResolutionDate.EditValue IsNot Nothing Then
                .ResolutionDate = INDdteResolutionDate.EditValue
            End If

            If INDtxtResolutionNumber.EditValue IsNot Nothing Then
                .ResolutionNumber = INDtxtResolutionNumber.EditValue
            End If

            If INDtxtCertificateOfficeNumber.EditValue IsNot Nothing Then
                .CertificateOfficeNumber = INDtxtCertificateOfficeNumber.EditValue
            End If

            If INDdtePosesionDate.EditValue IsNot Nothing Then
                .PosesionDate = INDdtePosesionDate.EditValue
            End If

            If clone = False Then
                For Each item As FundContract In FundsDatasource
                    .FundContract.Add(item)
                Next
            Else
                For Each item As FundContract In FundsDatasource
                    Dim nf As New FundContract()
                    With nf
                        .EndingDate = item.EndingDate
                        .Fund = item.Fund
                        .FundId = item.FundId
                        .FundType = item.FundType
                        .InitialDate = item.InitialDate
                        .MembershipNumber = item.MembershipNumber
                        .State = item.State
                        .VoluntaryContribution = item.VoluntaryContribution
                        .VoluntaryContributionValue = item.VoluntaryContributionValue
                    End With
                    .FundContract.Add(nf)
                Next
            End If
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
        End With
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub LoadInfoInControl(_contract As Contract)

        CanOverrideContractLength = False

        RemoveHandler INDsleContractTypeId.EditValueChanged, AddressOf INDsleContractTypeId_EditValueChanging
        RemoveHandler INDsleGroupId.EditValueChanged, AddressOf INDsleGroupId_EditValueChanging
        RemoveHandler INDsleFunctionalUnitId.EditValueChanged, AddressOf INDsleFunctionalUnitId_EditValueChanged
        RemoveHandler INDslePositionId.EditValueChanged, AddressOf INDslePositionId_EditValueChanged
        RemoveHandler INDdteContractInitialDate.EditValueChanged, AddressOf INDdteContractDate_EditValueChanged
        RemoveHandler INDdteContractEndingDate.EditValueChanged, AddressOf INDdteContractDate_EditValueChanged

        RemoveHandler INDspnContractLengthYear.EditValueChanged, AddressOf INDspnContractLength_EditValueChanged
        RemoveHandler INDspnContractLengthMonth.EditValueChanged, AddressOf INDspnContractLength_EditValueChanged
        RemoveHandler INDspnContractLengthDay.EditValueChanged, AddressOf INDspnContractLength_EditValueChanged

        '------------------------ Validaciones para habilitar funcionamiento de acuerdo a los parametros que tiene el contrato
        ContractClassSpecificBehavior(_contract.ContractType.ContractClass, _contract)

        '--------------------------------------------------

        'Valores del contrato
        With _contract
            'Valores por defecto de contrato nuevo
            '---------------------------------------------------------------------------------
            INDsleBankId.Properties.NullText = .Bank.Name
            INDsleBankId.EditValue = .BankId
            INDtxtBankAccountNumber.Text = .BankAccountNumber
            INDgleBankAccountType.EditValue = .BankAccountType

            'Contingencia
            Contingency = .Contingency
            INDdteContractEndingDate.EditValue = .ContractEndingDate

            If (_contractClass <> ClaseContratoLaboralIndefinido) Then

                Dim ValidateDate As New Date(9999, 12, 31)
                If _contract.ContractEndingDate = ValidateDate Then
                    Exit Sub
                End If

                Dim endingDate = If(.RowType = 1, .ContractEndingDate.AddDays(1), .ContractEndingDate)

                Dim contractLengthDTS = DateTimeSpan.CompareDates(endingDate, Date.Today)

                INDspnContractLengthYear.EditValue = contractLengthDTS.Years
                INDspnContractLengthMonth.EditValue = contractLengthDTS.Months
                INDspnContractLengthDay.EditValue = contractLengthDTS.Days
            End If
            'Restricciones de fechas por contrato
            INDdteFundInitialDate.Properties.MinValue = ContractToEdit.JobBondingDate
            INDdteFundInitialDate.Properties.MaxValue = ContractToEdit.ContractEndingDate

            'Tipo de contrato
            INDsleContractTypeId.Properties.NullText = .ContractType.Name
            INDsleContractTypeId.EditValue = .ContractTypeId
            IsTrialPeriodEnable = .ContractType.TestPeriod
            IsUndefinedEnable = .ContractType.Undefined
            INDgleSeveranceType.EditValue = .ContractType.SeveranceType
            INDsleJobBondingTypeId.Properties.NullText = .ContractType.JobBondingType.Name
            INDsleJobBondingTypeId.EditValue = .ContractType.JobBondingTypeId
            INDgleSalaryType.EditValue = .ContractType.SalaryType
            INDgleAutoRenew.EditValue = .ContractType.AutoRenew
            INDbtnAddFunds.Enabled = True

            'Grupo
            INDsleGroupId.Properties.NullText = .Group.Name
            INDsleGroupId.EditValue = .GroupId
            INDglePaymentPeriod.EditValue = .Group.Liquidation
            Presenter.ChangeFunctionalUnitByCompany(.Group.CompanyId)
            Dim g = .Group
            GroupXPO = New PayrollGroupXpo With {.Id = g.Id, .CompanyId = g.CompanyId, .Liquidation = g.Liquidation}
            INDdteContractInitialDate.EditValue = .Group.NextDateLiquidation

            Dim calculatedMinDate As Date = CalculateMinAllowedDate(.Group.NextDateLiquidation)
            INDdteContractInitialDate.Properties.MinValue = If(ContractToEdit.ContractInitialDate < calculatedMinDate, calculatedMinDate, ContractToEdit.ContractInitialDate)
            
            INDTxtDailyHours.EditValue = .HoursDaily

            'Unidad funcional
            INDsleFunctionalUnitId.Properties.NullText = .FunctionalUnit.Name
            INDsleFunctionalUnitId.EditValue = .FunctionalUnitId
            INDtxtCostCenterId.EditValue = .Employee.CostCenter.Name
            INDmemNotes.EditValue = .Notes
            INDglePaymentPeriod.EditValue = .PaymentPeriod
            INDglePaymentType.EditValue = .PaymentType
            INDslePositionId.Properties.NullText = .Position.Name
            INDslePositionId.EditValue = .PositionId
            INDspnBasicSalary.Properties.MinValue = .Position.MinBasicSalary
            INDspnBasicSalary.Properties.MaxValue = .Position.MaxBasicSalary
            INDspnBasicSalary.EditValue = .BasicSalary
            IsTrialPeriodEnable = .TrialPeriod
            INDspnTrialPeriodSalaryPercentage.Text = .TrialPeriodSalaryPercentage
            INDspnTrialPeriodTime.Text = .TrialPeriodTime
            ACCAIParameter = If(.ParameterACCAI Is Nothing, False, .ParameterACCAI)
            For Each item As FundContract In .FundContract
                FundsDatasource.Add(item)
                FundsPreviouslyCreatedDatasource.Add(item)
            Next
            INDgrdListOfFunds.RefreshDataSource()

            'ÚNICAMENTE PARA ENTIDAS PÚBLICAS

            INDtxtResolutionNumber.EditValue = .ResolutionNumber

            If .ResolutionDate IsNot Nothing Then
                INDdteResolutionDate.EditValue = .ResolutionDate
            End If

            INDtxtCertificateOfficeNumber.EditValue = .CertificateOfficeNumber

            If .PosesionDate IsNot Nothing Then
                INDdtePosesionDate.EditValue = .PosesionDate
            End If
        End With

        'Valores del empleado
        With _contract.Employee
            Dim c = .CostCenter
            FunctionalUnitXPO = New PayrollFunctionalUnit With {.CostCenterId = New PayrollCostCenterXpo With {.Id = c.Id, .Descripcion = c.Name}}
            INDsleWorkCenterId.Properties.NullText = .WorkCenter.Name
            INDsleWorkCenterId.EditValue = .WorkCenterId
            INDspnProfessionalRiskPercentage.EditValue = .ProfessionalRiskPercentage
            INDsleEmployeeType.Properties.NullText = .EmployeeType.Name
            INDsleEmployeeType.EditValue = .EmployeeTypeId
        End With

        AddHandler INDsleContractTypeId.EditValueChanged, AddressOf INDsleContractTypeId_EditValueChanging
        AddHandler INDsleGroupId.EditValueChanged, AddressOf INDsleGroupId_EditValueChanging
        AddHandler INDsleFunctionalUnitId.EditValueChanged, AddressOf INDsleFunctionalUnitId_EditValueChanged
        AddHandler INDslePositionId.EditValueChanged, AddressOf INDslePositionId_EditValueChanged
        AddHandler INDdteContractInitialDate.EditValueChanged, AddressOf INDdteContractDate_EditValueChanged
        AddHandler INDdteContractEndingDate.EditValueChanged, AddressOf INDdteContractDate_EditValueChanged

        AddHandler INDspnContractLengthYear.EditValueChanged, AddressOf INDspnContractLength_EditValueChanged
        AddHandler INDspnContractLengthMonth.EditValueChanged, AddressOf INDspnContractLength_EditValueChanged
        AddHandler INDspnContractLengthDay.EditValueChanged, AddressOf INDspnContractLength_EditValueChanged
    End Sub

    ''' <summary>
    ''' Metodo que habilita comportamiento del formulario en base a la clase de contrato
    ''' </summary>
    ''' <param name="contractClass"></param>
    Private Sub ContractClassSpecificBehavior(contractClass As Byte, Optional _contract As Contract = Nothing)
        INDsleGroupId.Properties.DataSource = Nothing
        INDsleFunctionalUnitId.Properties.DataSource = Nothing

        'filtra los grupos de acuerdo a la clase de contrato
        Presenter.ChangeGroupDatasourceByContractClass(contractClass)

        'busca el valor de la enumeracion para el tipo de contrato
        Dim cc = EmployeeHelper.ContractClasses.Where(Function(i) i.Item2 = contractClass).FirstOrDefault.Item1

        Select Case cc
            Case Eresources.ClaseContratoOtros
                INDlciListOfFunds.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlcgFundsInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDsleContractTypeId.Properties.ReadOnly = True
                '-------------------------------------------------------------------------------
                MinMonthsContractLength = 0
                MaxMonthsContractLength = 999
            Case Eresources.ClaseContratoAprendizaje
                INDlciListOfFunds.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlcgFundsInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDsleContractTypeId.Properties.ReadOnly = True
                '-------------------------------------------------------------------------------

                MinMonthsContractLength = 0
                MaxMonthsContractLength = 24
                INDspnProfessionalRiskPercentage.EditValue = 0
            Case Eresources.ClaseContratoLaboralFijo
                INDlciListOfFunds.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlcgFundsInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDsleContractTypeId.Properties.ReadOnly = False
                Presenter.ChangeContractTypeDatasourceByContractClass(EmployeeHelper.ContractClasses.Where(Function(i) i.Item1 = Eresources.ClaseContratoLaboralFijo).FirstOrDefault.Item2.ToString, EmployeeHelper.ContractClasses.Where(Function(i) i.Item1 = Eresources.ClaseContratoLaboralIndefinido).FirstOrDefault.Item2.ToString)
                '-------------------------------------------------------------------------------
                If _contract IsNot Nothing Then
                    Dim initialContract As Contract

                    If _contract.RowType = 2 Then
                        'si es novedad se busca el contrato inicial
                        initialContract = EmployeeContract.Contract.Where(Function(i) i.Id = ContractToEdit.InitialContractNumber).FirstOrDefault
                    Else
                        'si es contrato base se asigna a la variable de contrato inicial
                        initialContract = _contract
                    End If

                    Dim ValidateDate As New Date(9999, 12, 31)
                    If _contract.ContractEndingDate = ValidateDate Then
                        Mensaje(EeventViewerImages.Advertencia) = "La Fecha Fin de Contrato " & _contract.ContractEndingDate.ToString() & " es de Término Indefinido, pero la Clase de Contrato es Laboral a Término Fijo"
                        Exit Sub
                    End If

                    'Duracion del contrato actual en años
                    Dim initialContractTimeSpan = DateTimeSpan.CompareDates(_contract.ContractEndingDate.AddDays(1), _contract.ContractInitialDate)

                    If initialContractTimeSpan.Years >= 1 Then 'Contrato minimo a 1 año maximo 3 años si el contrato actual es mayor o igual a un 1 año
                        MinMonthsContractLength = 12
                        MaxMonthsContractLength = 36
                    Else 'Se puede renovar hasta x 4 veces

                        Dim contractExtensions = (From emp In EmployeeContract.Contract.Where(Function(i) i.InitialContractNumber = ContractToEdit.InitialContractNumber OrElse i.Id = ContractToEdit.InitialContractNumber).ToList() Select emp.ContractInitialDate, emp.ContractEndingDate).ToList().Distinct().Count()

                        If contractExtensions >= 10 Then
                            MinMonthsContractLength = 12
                            MaxMonthsContractLength = 36
                        Else
                            MinMonthsContractLength = 0
                            MaxMonthsContractLength = 36
                        End If

                    End If
                Else
                    MinMonthsContractLength = 0
                    MaxMonthsContractLength = 36
                End If

            Case Eresources.ClaseContratoLaboralIndefinido 'en caso de no ser otros contratos
                INDlciListOfFunds.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlcgFundsInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDsleContractTypeId.Properties.ReadOnly = False
                Presenter.ChangeContractTypeDatasourceByContractClass(EmployeeHelper.ContractClasses.Where(Function(i) i.Item1 = Eresources.ClaseContratoLaboralIndefinido).FirstOrDefault.Item2.ToString)
                '-------------------------------------------------------------------------------
            Case Else
                Return
        End Select
        _contractClass = cc
        _contractClassDB = contractClass
    End Sub

    ''' <summary>
    ''' Metodo que habilita el comportamiento del formulario en base a si se genera o no un otro si
    ''' </summary>
    Private Sub ContractModificationSpecificBehavior()
        If _contractModification = True Then 'Genera Otro si
            INDlcgContractInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlcgDatesNJobBondingInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlcgContractInfo.Enabled = True
        Else 'No genera Otro Si
            INDlcgContractInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlcgDatesNJobBondingInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlcgContractInfo.Enabled = False
        End If
    End Sub
#End Region

#Region "Events"
    ''' <summary>
    ''' Delegado que contiene el tipo de argumento que devuelve el empleado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Delegate Sub OnContractHandler(sender As Object, e As OnContractAddedEventArgs)

    ''' <summary>
    ''' Evento que se dispara al 
    ''' </summary>
    ''' <remarks></remarks>
    Public Event OnContractAdded As OnContractHandler


#Region "Fondos"
    ''' <summary>
    ''' Metodo que limpia los valores que se encuentre dentro del popup de fondos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanFundsPopup()
        INDsleFundIds.Properties.NullText = String.Empty
        INDsleFundIds.EditValue = -1
        INDgleFundType.EditValue = Nothing
        INDdteFundEndingDate.EditValue = Nothing
        INDdteFundInitialDate.EditValue = Nothing
        INDtxtMembershipNumber.EditValue = Nothing
        INDgleVoluntaryContribution.EditValue = Nothing
        INDtxtVoluntaryContributionValue.EditValue = Nothing
        INDlciVoluntaryContributionValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
    End Sub

    ''' <summary>
    ''' Metodo que limpia el popup de fondos si se cierra cuando se esta editando un fondo
    ''' </summary>
    Private Sub INDpccFunds_CloseUp(sender As Object, e As System.EventArgs)
        If FundToEdit IsNot Nothing Then
            CleanFundsPopup()
            INDlciFundEndingDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            FundToEdit = Nothing
        End If
        INDbtnAddContract.Focus()
    End Sub

    ''' <summary>
    ''' Metodo que ubica el foco en el primer campo a diligenciar en el popup de fondos
    ''' </summary>
    Private Sub INDpopFunds_Popup(sender As Object, e As DevExpress.XtraEditors.ShowDropDownControlEventArgs)
        'TODO agregar recursos
        Mensaje(EeventViewerImages.Informacion) = String.Format(obtenerRecurso(FondoFechaMinimaContrato, Contrato), ContractToEdit.ContractInitialDate)
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
    ''' Metodo que asigna el fondo seleccionado a la variable <see cref="fundXPO">fundXPO</see> para que se pueda validar al momento de guardar el fondo
    ''' </summary>
    Private Sub INDsleFundIds_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleFundIds.EditValueChanged

        Dim gv = CType(sender, SearchLookUpEdit)

        If INDsleFundIds.EditValue <> -1 Then
            'Fondo a modificar/Agregar
            fundXPO = EmployeeHelper.ConvertFromSelectedRowInGridViewToXpoEntity(Of PayrollFundsXpo)(gv.Properties.View)

            Dim fundTypeDatasource As New List(Of Tuple(Of Integer, Byte, String))

            If fundXPO IsNot Nothing Then
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
            End If
            'Se asigna el datasource 
            INDgleFundType.Properties.DataSource = fundTypeDatasource
            'Se selecciona por defecto el tipo si solo tiene un tipo de fondo
            If fundTypeDatasource.Count = 1 Then
                INDgleFundType.EditValue = fundTypeDatasource.FirstOrDefault.Item2
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo que muestra la descripcion de los tipos de fondos de acuerdo al seleccionado
    ''' </summary>
    Private Sub INDrepFundsType_CustomDisplayText(sender As Object, e As CustomDisplayTextEventArgs) Handles INDrepFundsType.CustomDisplayText
        If e.Value IsNot Nothing Then
            Dim ft = EmployeeHelper.FundsTypes.Where(Function(i) i.Item2 = e.Value).FirstOrDefault
            If ft IsNot Nothing Then
                e.DisplayText = ft.Item3
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo para mostrar de manera adecuada los estados del fondo
    ''' </summary>
    Private Sub INDrepFundsState_CustomDisplayText(sender As Object, e As CustomDisplayTextEventArgs) Handles INDrepFundsState.CustomDisplayText
        If e.Value = True Then
            e.DisplayText = obtenerRecurso(ComunesActivo)
        Else
            e.DisplayText = obtenerRecurso(ComunesInactivo)
        End If
    End Sub

    ''' <summary>
    ''' Metodo para limitar la opciones de acciones que pueden ejecutarse sobre un fondo creado anteriormente
    ''' </summary>
    Private Sub INDrepFundsActions_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDrepFundsActions.QueryPopUp
        Dim s = CType(INDgrvGrdListOfFunds.GetRow(INDgrvGrdListOfFunds.FocusedRowHandle), FundContract)

        If Not s.State Then
            e.Cancel = True
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(FondoAccionesInactivos, Contrato)
            Exit Sub
        End If

        If FundsPreviouslyCreatedDatasource.Where(Function(i) i.FundId = s.FundId AndAlso i.FundType = s.FundType AndAlso i.State = s.State).Count > 0 Then
            INDbtnDeleteFund.Visible = False
            INDpccFundActions.Height = 40
        Else
            INDbtnDeleteFund.Visible = True
            INDpccFundActions.Height = 80
        End If
    End Sub

    ''' <summary>
    '''
    ''' </summary>
    Private Sub INDdteFundEndingDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDdteFundEndingDate.EditValueChanged
        If INDdteFundEndingDate.EditValue IsNot Nothing Then
            deletingFundDate = INDdteFundEndingDate.EditValue
        End If
    End Sub

    ''' <summary>
    ''' Metodo para habilitar los controles para nuevo fondo
    ''' </summary>
    Private Sub INDbtnAddFunds_Click(sender As Object, e As EventArgs)
        INDlciFundEndingDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    End Sub

    ''' <summary>
    ''' Metodo que posiciona el foco en el siguiente control al dar "escape" sobre el boton
    ''' </summary>
    Private Sub INDbtnAddProfession_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            INDpccFunds_CloseUp(Nothing, EventArgs.Empty)
        End If
    End Sub

    ''' <summary>
    ''' Metodo que posiciona el foco en el siguiente control al dar "escape" sobre la rejilla
    ''' </summary>
    Private Sub INDgrvProfessions_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDgrvGrdListOfFunds.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            INDpccFunds_CloseUp(Nothing, EventArgs.Empty)
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
        ' Deshacer()
    End Sub

#End Region
    ''' <summary>
    ''' Evento Load donde se ejecutan el asincrono para levantar la definicion del layout
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmContract_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Cargamos de manera asincrona definiciones del funcional
        BarraBotones.PrepareToolbar(Presentation.Controls.eAction.OnlyUndo)
        BarraBotones.OperatingUnitVisible = False


        If indigo.IndigoCompanyType = "2" Then
            INDlciResolutionNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlciResolutionDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlciCertificateOfficeNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlciPosesionDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If

        modelBusqueda = New MBusqueda()
        PathFunctionalDefinitions = String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name, "\ModuloPayroll", Me.Name, ".xml")
        LoadhronousDefinitions = New BackgroundWorker
        If LoadhronousDefinitions.IsBusy = False Then
            LoadhronousDefinitions.RunWorkerAsync()
        End If

        'Verifica si esta en modo de diseño para no realizar los llamados de deshacer
        If Not IsDesignMode Then
            Initializes()
            CleanControls()
        End If

        InitializeTuple()
        GetEmployeeToWorkWith()

        changeNumericFormatByCurrency(CurrencyAbbreviation.GetNumberFormat)
		If CurrencyAbbreviation IsNot Nothing AndAlso CurrencyAbbreviation IsNot String.Empty Then
			Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
			_culture.NumberFormat = (CurrencyAbbreviation).GetNumberFormat()
			INDspnBasicSalary.Properties.Mask.Culture = _culture
			INDtxtVoluntaryContributionValue.Properties.Mask.Culture = _culture
		End If
	End Sub

    Private Sub InitializeTuple()
        Dim ListTupleContingency = New List(Of Tuple(Of Byte, String))
        ListTupleContingency.Add(New Tuple(Of Byte, String)(0, "Ninguna"))
        ListTupleContingency.Add(New Tuple(Of Byte, String)(1, "Licencias"))
        ListTupleContingency.Add(New Tuple(Of Byte, String)(2, "Vacaciones"))
        ListTupleContingency.Add(New Tuple(Of Byte, String)(3, "Incapacidades"))
        INDSlContingency.Properties.DataSource = ListTupleContingency.ToList()
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
            INDsleJobBondingTypeId.Properties.NullText = contractType.JobBondingTypeId.Descripcion
            INDsleJobBondingTypeId.EditValue = contractType.JobBondingTypeId.Id
            INDgleSalaryType.EditValue = contractType.SalaryType
            INDgleAutoRenew.EditValue = contractType.AutoRenew
            INDsleGroupId.Properties.DataSource = Nothing

            ContractClassSpecificBehavior(contractType.ContractClass)

            If ContractToEdit.ContractTypeId <> CInt(INDsleContractTypeId.EditValue) Then
                CanOverrideContractLength = True
            Else
                CanOverrideContractLength = False
            End If

            'Se limpian los otros campos si cambia el tipo de contrato
            INDsleGroupId.Properties.NullText = String.Empty
            INDsleGroupId.EditValue = -1
            INDsleFunctionalUnitId.Properties.NullText = String.Empty
            INDsleFunctionalUnitId.EditValue = -1
            INDtxtCostCenterId.EditValue = Nothing
            'Se habilita la seleccion de fondos
            INDbtnAddFunds.Enabled = True
        Else
            INDbtnAddFunds.Enabled = False
            FundsDatasource.Clear()
            INDgrdListOfFunds.RefreshDataSource()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que busca los parametros del grupo seleccionado
    ''' </summary>
    Private Sub INDsleGroupId_EditValueChanging(sender As Object, e As System.EventArgs) Handles INDsleGroupId.EditValueChanged
        If Not INDsleGroupId.EditValue = -1 Then
            GroupXPO = EmployeeHelper.ConvertFromSelectedRowInGridViewToXpoEntity(Of PayrollGroupXpo)(INDgrvSleGroup)
            INDglePaymentPeriod.EditValue = GroupXPO.Liquidation
            INDsleFunctionalUnitId.Properties.DataSource = Nothing
            
            ' IMPORTANTE: Primero establecer el valor de la fecha (mes de liquidación)
            ' y luego configurar el MinValue. Esto asegura que el calendario abra en el mes correcto.
            INDdteContractInitialDate.EditValue = GroupXPO.FechaProximaLiquidacion
            
            ' Configurar fecha mínima permitida considerando los meses de ajustes extemporáneos
            ' Esto permite retroceder en el calendario según el parámetro configurado
            Dim calculatedMinDate As Date = CalculateMinAllowedDate(GroupXPO.FechaProximaLiquidacion)
            INDdteContractInitialDate.Properties.MinValue = If(ContractToEdit.ContractInitialDate < calculatedMinDate, calculatedMinDate, ContractToEdit.ContractInitialDate)
            
            Presenter.ChangeFunctionalUnitByCompany(GroupXPO.CompanyId)
            INDsleFunctionalUnitId.Properties.NullText = String.Empty
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

    ''' <summary>
    ''' Metodo que calcula la fecha final del contrato de acuerdo al tipo de unidad de tiempo seleccionada
    ''' </summary>
    ''' <remarks>
    ''' ANTES: Solo configuraba fechas mínimas/máximas de fondos
    ''' DESPUÉS: Además valida si la fecha es extemporánea y aplica restricciones
    ''' </remarks>
    Private Sub INDdteContractInitialDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDdteContractInitialDate.EditValueChanged
        If INDdteContractInitialDate.EditValue IsNot Nothing Then
            INDdteContractEndingDate.Properties.MinValue = INDdteContractInitialDate.EditValue
            INDdteFundInitialDate.Properties.MinValue = ContractToEdit.JobBondingDate
            If INDdteContractEndingDate.EditValue IsNot Nothing Then
                INDdteFundInitialDate.Properties.MaxValue = INDdteContractEndingDate.EditValue
            Else
                INDdteFundInitialDate.Properties.MaxValue = INDdteContractInitialDate.EditValue
            End If

            ' VALIDACIÓN DE FECHA EXTEMPORÁNEA
            ' Solo aplica cuando se está generando un Otro Sí
            If _contractModification = True Then
                ValidateExtemporaneousDate(CDate(INDdteContractInitialDate.EditValue))
            End If
        End If
    End Sub

    ''' <summary>
    ''' Calcula la fecha mínima permitida en el calendario considerando los meses permitidos para ajustes extemporáneos
    ''' </summary>
    ''' <param name="nextLiquidationDate">Fecha de la próxima liquidación del grupo</param>
    ''' <returns>Fecha mínima permitida en el calendario</returns>
    ''' <remarks>
    ''' Este método calcula cuántos meses se puede retroceder desde la fecha de próxima liquidación
    ''' basándose en el parámetro AllowedMonthsForExtemporaneousAdjustments
    ''' </remarks>
    Private Function CalculateMinAllowedDate(nextLiquidationDate As Date) As Date
        ' Si no se permiten ajustes extemporáneos, la fecha mínima es la próxima liquidación
        If _allowedMonthsForExtemporaneousAdjustments = 0 Then
            Return nextLiquidationDate
        End If
        
        ' Obtener el primer día del mes de la próxima liquidación
        Dim firstDayOfLiquidationMonth As Date = New Date(nextLiquidationDate.Year, nextLiquidationDate.Month, 1)
        
        ' Retroceder los meses permitidos
        Dim minAllowedDate As Date = firstDayOfLiquidationMonth.AddMonths(-_allowedMonthsForExtemporaneousAdjustments)
        
        Return minAllowedDate
    End Function
    ''' <summary>
    ''' Valida si la fecha seleccionada es extemporánea y aplica restricciones
    ''' </summary>
    ''' <param name="selectedDate">Fecha seleccionada</param>
    ''' <remarks>
    ''' ANTES: No existía validación de fechas extemporáneas
    ''' DESPUÉS: Valida el rango permitido y habilita/deshabilita campos según corresponda
    ''' </remarks>
    Private Sub ValidateExtemporaneousDate(selectedDate As Date)
        Dim firstDayOfCurrentMonth As Date = New Date(Date.Today.Year, Date.Today.Month, 1)
        
        ' Determinar si la fecha es extemporánea
        _isExtemporaneousDate = selectedDate < firstDayOfCurrentMonth

        If _isExtemporaneousDate Then
            ' Verificar si está dentro del rango permitido
            If _allowedMonthsForExtemporaneousAdjustments = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Los ajustes extemporáneos están deshabilitados. La fecha de inicio debe ser del mes actual o posterior."
                INDdteContractInitialDate.EditValue = firstDayOfCurrentMonth
                _isExtemporaneousDate = False
                Return
            End If

            Dim lowerBoundDate As Date = firstDayOfCurrentMonth.AddMonths(-_allowedMonthsForExtemporaneousAdjustments)
            
            If selectedDate < lowerBoundDate Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(
                    "La fecha seleccionada está fuera del rango permitido. " & _
                    "Solo se permiten fechas desde {0:dd/MM/yyyy} hasta {1:dd/MM/yyyy}.", 
                    lowerBoundDate, Date.Today)
                INDdteContractInitialDate.EditValue = lowerBoundDate
                _isExtemporaneousDate = True
            Else
                ' Fecha extemporánea válida: aplicar restricciones
                ApplyExtemporaneousRestrictions(True)
                Mensaje(EeventViewerImages.Informacion) = "AJUSTE EXTEMPORÁNEO: Solo puede modificar Cargo y Salario Básico. Los demás campos quedarán deshabilitados."
            End If
        Else
            ' Fecha normal (mes actual o futuro): quitar restricciones
            ApplyExtemporaneousRestrictions(False)
        End If
    End Sub

    ''' <summary>
    ''' Aplica o quita restricciones de campos para ajustes extemporáneos
    ''' </summary>
    ''' <param name="applyRestrictions">True para aplicar restricciones, False para quitarlas</param>
    ''' <remarks>
    ''' ANTES: No existían restricciones específicas para ajustes extemporáneos
    ''' DESPUÉS: Deshabilita todos los campos excepto Cargo y Salario Básico cuando es extemporáneo
    ''' 
    ''' Campos que PUEDEN modificarse en ajustes extemporáneos:
    ''' - INDslePositionId (Cargo)
    ''' - INDspnBasicSalary (Salario Básico)
    ''' 
    ''' Campos que NO pueden modificarse:
    ''' - Tipo de Contrato, Grupo, Unidad Funcional, Banco, Cuenta, etc.
    ''' </remarks>
    Private Sub ApplyExtemporaneousRestrictions(applyRestrictions As Boolean)
        If applyRestrictions Then
            ' DESHABILITAR campos que NO se pueden modificar en ajustes extemporáneos
            INDsleContractTypeId.Properties.ReadOnly = True
            INDsleGroupId.Properties.ReadOnly = True
            INDsleFunctionalUnitId.Properties.ReadOnly = True
            INDsleJobBondingTypeId.Properties.ReadOnly = True
            INDsleBankId.Properties.ReadOnly = True
            INDtxtBankAccountNumber.Properties.ReadOnly = True
            INDgleBankAccountType.Properties.ReadOnly = True
            INDglePaymentPeriod.Properties.ReadOnly = True
            INDglePaymentType.Properties.ReadOnly = True
            INDgleSalaryType.Properties.ReadOnly = True
            INDTxtDailyHours.Properties.ReadOnly = True
            INDgleAutoRenew.Properties.ReadOnly = True
            INDdteContractEndingDate.Properties.ReadOnly = True
            INDspnContractLengthYear.Properties.ReadOnly = True
            INDspnContractLengthMonth.Properties.ReadOnly = True
            INDspnContractLengthDay.Properties.ReadOnly = True
            INDmemNotes.Properties.ReadOnly = True
            INDspnTrialPeriodSalaryPercentage.Properties.ReadOnly = True
            INDspnTrialPeriodTime.Properties.ReadOnly = True
            INDbtnAddFunds.Enabled = False

            ' HABILITAR solo los campos permitidos
            INDslePositionId.Properties.ReadOnly = False
            INDspnBasicSalary.Properties.ReadOnly = False
        Else
            ' HABILITAR todos los campos (flujo normal)
            INDsleContractTypeId.Properties.ReadOnly = False
            INDsleGroupId.Properties.ReadOnly = False
            INDsleFunctionalUnitId.Properties.ReadOnly = False
            INDsleJobBondingTypeId.Properties.ReadOnly = False
            INDsleBankId.Properties.ReadOnly = False
            INDtxtBankAccountNumber.Properties.ReadOnly = False
            INDgleBankAccountType.Properties.ReadOnly = False
            INDglePaymentPeriod.Properties.ReadOnly = False
            INDglePaymentType.Properties.ReadOnly = False
            INDgleSalaryType.Properties.ReadOnly = False
            INDTxtDailyHours.Properties.ReadOnly = False
            INDgleAutoRenew.Properties.ReadOnly = False
            INDdteContractEndingDate.Properties.ReadOnly = False
            INDspnContractLengthYear.Properties.ReadOnly = False
            INDspnContractLengthMonth.Properties.ReadOnly = False
            INDspnContractLengthDay.Properties.ReadOnly = False
            INDmemNotes.Properties.ReadOnly = False
            INDspnTrialPeriodSalaryPercentage.Properties.ReadOnly = False
            INDspnTrialPeriodTime.Properties.ReadOnly = False
            INDbtnAddFunds.Enabled = True
            INDslePositionId.Properties.ReadOnly = False
            INDspnBasicSalary.Properties.ReadOnly = False
        End If
    End Sub

    ''' <summary>
    ''' Metodo para verificar que el sueldo a asignar al nuevo contrato no sea menor a ningun contrato que ya haya tenido bajo el mismo contrato base
    ''' </summary>
    Private Sub INDspnBasicSalary_Leave(sender As Object, e As EventArgs) Handles INDspnBasicSalary.Leave
        If Not (CType(INDspnBasicSalary.EditValue, Decimal) >= ContractToEdit.Position.MinBasicSalary And CType(INDspnBasicSalary.EditValue, Decimal) <= ContractToEdit.Position.MaxBasicSalary) Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(ContratosSalarioVsCargo, Contrato), ContractToEdit.Position.MinBasicSalary, ContractToEdit.Position.MaxBasicSalary)
        End If
        If INDspnBasicSalary.EditValue IsNot Nothing AndAlso Not INDspnBasicSalary.EditValue.ToString.Equals(String.Empty) Then

            If indigo.IndigoCompanyType = "1" Then
                If FlagDownSalary = False Then
                    If EmployeeContract.Contract.Where(Function(i) i.InitialContractNumber = ContractToEdit.InitialContractNumber AndAlso i.BasicSalary > CDec(INDspnBasicSalary.EditValue)).Count > 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ContratosSalarioVsAnteriorSalario, Contrato)
                        INDspnBasicSalary.EditValue = String.Empty
                        INDspnBasicSalary.Focus()
                        'TODO Mensaje de salario menor a otro dentro de la misma secuencia de contratos
                    End If
                End If
            Else
                If ContractToEdit.BasicSalary <> CDec(INDspnBasicSalary.EditValue) Then
                    CanOverrideContractLength = True
                Else
                    CanOverrideContractLength = False
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo para leer los parametros del cargo y aplicar las validaciones necesarias
    ''' </summary>
    Private Sub INDslePositionId_EditValueChanged(sender As Object, e As EventArgs) Handles INDslePositionId.EditValueChanged
        If INDslePositionId.EditValue IsNot Nothing AndAlso INDslePositionId.EditValue <> -1 Then

            positionXPO = EmployeeHelper.ConvertFromSelectedRowInGridViewToXpoEntity(Of PayrollPositionXpo)(INDslePositionId.Properties.View)
            INDspnBasicSalary.Properties.MinValue = positionXPO.MinBasicSalary
            INDspnBasicSalary.Properties.MaxValue = positionXPO.MaxBasicSalary
            INDspnProfessionalRiskPercentage.EditValue = positionXPO.ProfessionalRiskLevelId.Percentage
            'TODO Agregar Recurso
            Mensaje(EeventViewerImages.Informacion) = String.Format(obtenerRecurso(ContratosSalarioVsCargo, Contrato), positionXPO.MinBasicSalary, positionXPO.MaxBasicSalary)

            If ContractToEdit.PositionId <> CInt(INDslePositionId.EditValue) Then
                CanOverrideContractLength = True
            Else
                CanOverrideContractLength = False
            End If
        End If
    End Sub

    Private Sub INDspnContractLength_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDspnContractLengthYear.EditValueChanging, INDspnContractLengthMonth.EditValueChanging, INDspnContractLengthDay.EditValueChanging
        If String.IsNullOrEmpty(e.NewValue) OrElse CInt(e.NewValue) < 0 Then
            e.Cancel = True
        End If
    End Sub

    Private Sub INDspnContractLength_EditValueChanged(sender As Object, e As EventArgs) Handles INDspnContractLengthYear.EditValueChanged, INDspnContractLengthMonth.EditValueChanged, INDspnContractLengthDay.EditValueChanged

        'Type of contract is validated (ObraLabor or Indefinido = 12/31/9999)
        If CDate(INDdteContractEndingDate.EditValue).Year <> 9999 Then

            RemoveHandler INDdteContractEndingDate.EditValueChanged, AddressOf INDdteContractDate_EditValueChanged
            If INDdteContractInitialDate.EditValue IsNot Nothing Then

                Dim r = EmployeeHelper.CalculateContractEndingDate(INDdteContractInitialDate.EditValue, INDspnContractLengthYear.EditValue, INDspnContractLengthMonth.EditValue, INDspnContractLengthDay.EditValue)

                If CInt(INDspnContractLengthYear.EditValue) > 0 OrElse CInt(INDspnContractLengthMonth.EditValue) > 0 OrElse CInt(INDspnContractLengthDay.EditValue) > 0 Then
                    INDdteContractEndingDate.EditValue = r.AddDays(-1)
                Else
                    INDdteContractEndingDate.EditValue = Nothing
                End If
            Else
                INDdteContractEndingDate.EditValue = Nothing
            End If
            AddHandler INDdteContractEndingDate.EditValueChanged, AddressOf INDdteContractDate_EditValueChanged
        Else
            'If the type of contract is ObraLabor or indefinido, it is validated that it brings the object updated with the data corresponding to the difference between years, months and days.
            If sender.Name = "INDspnContractLengthDay" Then
                RemoveHandler INDdteContractEndingDate.EditValueChanged, AddressOf INDdteContractDate_EditValueChanged

                Dim r = EmployeeHelper.CalculateContractEndingDate(INDdteContractInitialDate.EditValue, INDspnContractLengthYear.EditValue, INDspnContractLengthMonth.EditValue, INDspnContractLengthDay.EditValue)

                INDdteContractEndingDate.EditValue = r

                AddHandler INDdteContractEndingDate.EditValueChanged, AddressOf INDdteContractDate_EditValueChanged
            End If
        End If
    End Sub

    Private Sub INDdteContractDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDdteContractEndingDate.EditValueChanged, INDdteContractInitialDate.EditValueChanged

        If INDdteContractEndingDate.EditValue IsNot Nothing AndAlso INDdteContractInitialDate.EditValue IsNot Nothing Then
            If _contractClass <> ClaseContratoLaboralIndefinido Then
                'Type of contract is validated (ObraLabor or Indefinido = 12/31/9999)
                If CDate(INDdteContractEndingDate.EditValue).Year = 9999 Then
                    Dim dts = DateTimeSpan.CompareDates(CDate(INDdteContractEndingDate.EditValue), INDdteContractInitialDate.EditValue)
                    INDspnContractLengthYear.EditValue = dts.Years
                    INDspnContractLengthMonth.EditValue = dts.Months
                    INDspnContractLengthDay.EditValue = dts.Days
                Else
                    Dim dts = DateTimeSpan.CompareDates(CDate(INDdteContractEndingDate.EditValue).AddDays(1), INDdteContractInitialDate.EditValue)
                    INDspnContractLengthYear.EditValue = dts.Years
                    INDspnContractLengthMonth.EditValue = dts.Months
                    INDspnContractLengthDay.EditValue = dts.Days
                End If
            End If
        End If
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

    Private Sub INDsleContractModificationReason_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleContractModificationReason.QueryPopUp
        ContractModificationReasonsXPO = modelBusqueda.ConsultarEntidades(eDataSource.ContractModificationReason)
    End Sub
#End Region

#Region "KeyDown"
    Private Sub INDpceDetailsBook_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDpceDetailsBook.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.F4 OrElse e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDpceDetailsBook.ShowPopup()
        End If
    End Sub
#End Region

#Region "FormClosing"
    Private Sub FrmContractEdit_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Not String.IsNullOrEmpty(INDtxtContractId.EditValue) Then
            If Not MessageIndigo.Show("Al cerrar el formulario se perderá la información que habia registrado", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                e.Cancel = True
            End If
        End If
    End Sub
#End Region

#Region "Popup"
    Private Sub INDpceDetailsBook_Popup(sender As Object, e As EventArgs) Handles INDpceDetailsBook.Popup
        INDsleFundIds.Focus()
    End Sub
#End Region
#Region "Click"
    Private Sub INDbtnAddDetailsBook_Click(sender As Object, e As EventArgs) Handles INDbtnAddDetailsBook.Click
        If ValidateFundsControls() = False Then
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesFormIncompleto)
            Exit Sub
        End If

        Dim ctrAdd = 0

        If FundToEdit Is Nothing Then
            FundToEdit = New FundContract()
            ctrAdd = 1

            If ValidateFundsLogic() = False Then
                'TODO Agregar recursos
                Exit Sub
            End If
            FundToEdit.ChangeTracker.State = ObjectState.Added
        Else
            FundToEdit.ChangeTracker.State = ObjectState.Modified

        End If

        With FundToEdit
            .EndingDate = INDdteFundEndingDate.EditValue
            .FundId = INDsleFundIds.EditValue

            If fundXPO IsNot Nothing Then
                .Fund = New Fund() With {.Id = INDsleFundIds.EditValue, .Name = INDsleFundIds.Text, .Pension = fundXPO.Pension, .Health = fundXPO.Health, .Unemployment = fundXPO.Unemployment, .Risk = fundXPO.Risk}
            End If
            .FundType = INDgleFundType.EditValue
            .InitialDate = INDdteFundInitialDate.EditValue
            .MembershipNumber = INDtxtMembershipNumber.Text
            If INDdteFundEndingDate.EditValue = New Date(9999, 12, 31) Then
                .State = If(INDdteFundEndingDate.EditValue IsNot Nothing, True, False)
            Else
                .State = If(INDdteFundEndingDate.EditValue IsNot Nothing, False, True)
            End If
            .VoluntaryContribution = INDgleVoluntaryContribution.EditValue
			.VoluntaryContributionValue = If(INDtxtVoluntaryContributionValue.EditValue IsNot Nothing, Convert.ToDecimal(INDtxtVoluntaryContributionValue.EditValue), 0D)
		End With

        If ctrAdd = 1 Then
            FundsDatasource.Add(FundToEdit)
        End If

        CleanFundsPopup()
        INDgrdListOfFunds.DataSource = Nothing
        INDgrdListOfFunds.DataSource = FundsDatasource

        FundToEdit = Nothing
        deletingFundDate = Nothing

        INDpceDetailsBook.ClosePopup()
    End Sub


    ''' <summary>
    ''' Dispara el evento de contrato añadido y devuelve en los argumentos el empleado
    ''' </summary>
    ''' <remarks>
    ''' ANTES: No validaba restricciones de ajustes extemporáneos
    ''' DESPUÉS: Valida que en ajustes extemporáneos solo se modifiquen Cargo y/o Salario
    ''' </remarks>
    Private Sub INDbtnAddContract_Click(sender As Object, e As EventArgs) Handles INDbtnAddContract.Click

        Dim clone = False

        'Valida que se hayan hecho cambios en los campos que generan otro si en caso de ser otro si
        If _contractModification = True Then

            clone = True

            If ValidateRequiredChanges() = False Then
                'TODO agregar a recursos
                If MessageIndigo.Show(String.Format(obtenerRecurso(ContratosSinCambio, Contrato), ContractToEdit.Id.ToString), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                    CleanControls()
                    Exit Sub
                Else
                    clone = False
                End If
            End If

            ' VALIDACIÓN ADICIONAL PARA AJUSTES EXTEMPORÁNEOS
            ' ANTES: No existía esta validación
            ' DESPUÉS: Verifica que solo se modifiquen Cargo y/o Salario en fechas extemporáneas
            If _isExtemporaneousDate Then
                If Not ValidateExtemporaneousChanges() Then
                    Mensaje(EeventViewerImages.Advertencia) = "Solo es posible registrar cambios extemporáneos para cargo o salario básico. Verifique la información."
                    Exit Sub
                End If
            End If

        End If
        'Valida los campos obligatorios
        If ValidateControls(clone) = False Then
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesFormIncompleto)
            Exit Sub
        End If

        'Valida logica de contratos
        If ValidateContractSpecifics() = False Then
            Exit Sub
        End If

        AssigningValues(clone) 'Asigna valores al contrato
        RaiseEvent OnContractAdded(Me, New OnContractAddedEventArgs(EmployeeContract)) 'Dispara evento para notificar que el contrato se ha diligenciado
        RaiseEvent OnAddContractModify(True)
        CleanControls()
        Me.Close()
    End Sub

    ''' <summary>
    ''' Valida que en ajustes extemporáneos solo se hayan modificado Cargo y/o Salario Básico
    ''' </summary>
    ''' <returns>True si solo se modificaron campos permitidos, False en caso contrario</returns>
    ''' <remarks>
    ''' ANTES: No existía esta validación
    ''' DESPUÉS: Compara el contrato original con los valores actuales del formulario
    ''' para asegurar que solo Cargo y/o Salario Básico hayan cambiado
    ''' </remarks>
    Private Function ValidateExtemporaneousChanges() As Boolean
        If ContractToEdit Is Nothing Then
            Return False
        End If

        ' Verificar que al menos uno de los campos permitidos haya cambiado
        Dim positionChanged As Boolean = ContractToEdit.PositionId <> INDslePositionId.EditValue
        Dim salaryChanged As Boolean = ContractToEdit.BasicSalary <> CDec(INDspnBasicSalary.EditValue)

        If Not positionChanged AndAlso Not salaryChanged Then
            ' No hay cambios en los campos permitidos
            Return False
        End If

        ' Verificar que NO se hayan modificado otros campos
        ' (Esta validación es redundante con las restricciones de UI, pero es una capa adicional de seguridad)
        
        If ContractToEdit.ContractTypeId <> INDsleContractTypeId.EditValue Then
            Return False
        End If

        If ContractToEdit.GroupId <> INDsleGroupId.EditValue Then
            Return False
        End If

        If ContractToEdit.FunctionalUnitId <> INDsleFunctionalUnitId.EditValue Then
            Return False
        End If

        If ContractToEdit.BankId <> INDsleBankId.EditValue Then
            Return False
        End If

        If ContractToEdit.BankAccountNumber <> INDtxtBankAccountNumber.EditValue Then
            Return False
        End If

        If ContractToEdit.SalaryType <> INDgleSalaryType.EditValue Then
            Return False
        End If

        ' Si llegamos aquí, solo se modificaron Cargo y/o Salario
        Return True
    End Function

    'Private consignmentTransferDetailEdit As Boolean

    ''' <summary>
    ''' Evento ejecutado para guardar los cambios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="ActionExecuted"></param>
    Public Event OnAddContractModify(ActionExecuted As Boolean)

    ''' <summary>
    ''' Añade un fondo a la rejilla
    ''' </summary>
    Private Sub INDbtnAddFund_Click(sender As Object, e As EventArgs) Handles INDbtnAddFunds.Click

        If ValidateFundsControls() = False Then
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesFormIncompleto)
            Exit Sub
        End If

        Dim ctrAdd = 0

        If FundToEdit Is Nothing Then
            FundToEdit = New FundContract()
            ctrAdd = 1

            If ValidateFundsLogic() = False Then
                'TODO Agregar recursos
                Exit Sub
            End If

        End If

        With FundToEdit
            .EndingDate = INDdteFundEndingDate.EditValue
            .FundId = INDsleFundIds.EditValue

            If fundXPO IsNot Nothing Then
                .Fund = New Fund() With {.Id = INDsleFundIds.EditValue, .Name = INDsleFundIds.Text, .Pension = fundXPO.Pension, .Health = fundXPO.Health, .Unemployment = fundXPO.Unemployment, .Risk = fundXPO.Risk}
            End If
            .FundType = INDgleFundType.EditValue
            .InitialDate = INDdteFundInitialDate.EditValue
            .MembershipNumber = INDtxtMembershipNumber.Text
            If INDdteFundEndingDate.EditValue = New Date(9999, 12, 31) Then
                .State = If(INDdteFundEndingDate.EditValue IsNot Nothing, True, False)
            Else
                .State = If(INDdteFundEndingDate.EditValue IsNot Nothing, False, True)
            End If
            .VoluntaryContribution = INDgleVoluntaryContribution.EditValue
            .VoluntaryContributionValue = If(INDtxtVoluntaryContributionValue.Text.Trim.Equals(String.Empty), Nothing, INDtxtVoluntaryContributionValue.Text.Trim)
        End With

        If ctrAdd = 1 Then
            FundsDatasource.Add(FundToEdit)
        End If

        CleanFundsPopup()
        INDgrdListOfFunds.DataSource = Nothing
        INDgrdListOfFunds.DataSource = FundsDatasource


        FundToEdit = Nothing
        deletingFundDate = Nothing
    End Sub

    ''' <summary>
    ''' Editar fondo
    ''' </summary>
    Private Sub INDbtnEditFund_Click(sender As Object, e As EventArgs) Handles INDbtnEditFund.Click
        FundToEdit = CType(INDgrdListOfFunds.DefaultView.GetRow(CType(INDgrdListOfFunds.DefaultView, DevExpress.XtraGrid.Views.Grid.GridView).FocusedRowHandle), FundContract)

        INDpceDetailsBook.ShowPopup()

        Dim fundTypeDatasource As New List(Of Tuple(Of Integer, Byte, String))

        If FundToEdit IsNot Nothing Then
            If FundToEdit.Fund.Health Then
                fundTypeDatasource.Add(EmployeeHelper.FundsTypes.Where(Function(i) i.Item1 = Eresources.Salud).FirstOrDefault)
            End If

            If FundToEdit.Fund.Pension Then
                fundTypeDatasource.Add(EmployeeHelper.FundsTypes.Where(Function(i) i.Item1 = Eresources.Pension).FirstOrDefault)
            End If

            If FundToEdit.Fund.Unemployment Then
                fundTypeDatasource.Add(EmployeeHelper.FundsTypes.Where(Function(i) i.Item1 = Eresources.Cesantias).FirstOrDefault)
            End If

            If FundToEdit.Fund.Risk Then
                fundTypeDatasource.Add(EmployeeHelper.FundsTypes.Where(Function(i) i.Item1 = Eresources.Riesgos).FirstOrDefault)
            End If

            If FundToEdit.Fund.CompensationFund Then
                fundTypeDatasource.Add(EmployeeHelper.FundsTypes.Where(Function(i) i.Item1 = Eresources.CajaCompensacion).FirstOrDefault)
            End If

            INDsleFundIds.Properties.NullText = FundToEdit.Fund.Name
            INDsleFundIds.EditValue = FundToEdit.FundId

            'Se asigna el datasource 
            INDgleFundType.Properties.DataSource = fundTypeDatasource
            'Se selecciona por defecto el tipo si solo tiene un tipo de fondo
            If fundTypeDatasource.Count = 1 Then
                INDgleFundType.EditValue = fundTypeDatasource.FirstOrDefault.Item2
            End If

            INDlciFundEndingDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDdteFundEndingDate.EditValue = FundToEdit.EndingDate

            INDgleFundType.EditValue = FundToEdit.FundType
            INDdteFundInitialDate.EditValue = FundToEdit.InitialDate
            INDdteFundEndingDate.Properties.MinValue = FundToEdit.InitialDate
            INDtxtMembershipNumber.Text = FundToEdit.MembershipNumber
            INDgleVoluntaryContribution.EditValue = FundToEdit.VoluntaryContribution
            INDtxtVoluntaryContributionValue.Text = FundToEdit.VoluntaryContributionValue

        End If
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

    Private Sub INDsleBankId_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleBankId.EditValueChanged
        Using modelBank As New MBank(MyTag)
            If INDsleBankId.EditValue IsNot Nothing Then
                Dim bank = modelBank.GetBankById(INDsleBankId.EditValue)
                If bank?.BankAccountRegistration Then
                    INDtxtBankAccountNumber.Properties.Mask.MaskType = Mask.MaskType.RegEx
                    INDtxtBankAccountNumber.Properties.Mask.EditMask = "\w+"
                Else
                    INDtxtBankAccountNumber.Properties.Mask.MaskType = Mask.MaskType.RegEx
                    INDtxtBankAccountNumber.Properties.Mask.EditMask = "\d+"
                End If
            Else
                INDtxtBankAccountNumber.Properties.Mask.MaskType = Mask.MaskType.RegEx
                INDtxtBankAccountNumber.Properties.Mask.EditMask = "\d+"
            End If
        End Using
    End Sub


#End Region

End Class