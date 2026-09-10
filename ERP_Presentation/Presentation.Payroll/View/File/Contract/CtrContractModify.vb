'***********************************************************************
' Assembly         : Presentacion.Payroll
' Author           : Jose Luis Rojas
' Created          : 23-09-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Base
Imports Presentation.Payroll.MVP
Imports Infrastructure.CrossCutting.Base
Imports DevExpress.Utils.Design.DesignTimeTools
Imports System.ComponentModel
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports Presentation.Controls.MVP
Imports Presentation.CloudAgent

''' <summary>
''' Clase que maneja el control del modificador de contratos
''' </summary>
''' <remarks></remarks>
Public Class CtrContractModify
    Implements IContract


#Region "Globals & Properties"

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
#End Region

#Region "ICRUD"

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String 'Implements Base.IcrudBase.Mensaje
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
    ''' Metodo que recibe la entidad Employee desde el formulario de Talento Humano
    ''' </summary>
    Public Sub GetEmployeeToWorkWith(ByRef employee As Employee, Optional contractNumber As Integer = 0, Optional contractAction As ContractActions = ContractActions.NewContractWithoutPayments)
        Me.ContractAction = contractAction
        CleanControlsBasic()


        If employee IsNot Nothing Then
            Me.EmployeeContract = employee

            ContractToEdit = employee.Contract.Where(Function(c) c.Id = contractNumber).FirstOrDefault()

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
    ''' Inicializa los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Initializes()
        'IndigoGridControl1.SetHoldSize(INDgrdListOfFunds, True)

        'Datasource de controles con fuente local



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
        'Fondos
        FundsDatasource.Clear()
        INDgrdListOfFunds.RefreshDataSource()
        CleanFundsPopup()

        CanOverrideContractLength = False
    End Sub

    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    Private Function ValidateControls(Optional clone As Boolean = False) As Boolean
        ValidateControls = True

        If clone = True Then
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
                    'Exit Function
                ElseIf daysBetween < 0 OrElse (daysBetween = 0 AndAlso fundOnEditId = 0) Then
                    'TODO agregar a recursos
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(FondoFechasEnRangoFondo, Contrato)
                    ValidateFundsLogic = False
                    'Exit Function
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
                msj += obtenerRecurso(FondoSaludObligatorio, Contrato) + vbCrLf 'obtenerRecurso(ComunesFormIncompleto)
                ValidateContractSpecifics = False
                'Exit Function
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
                msj += obtenerRecurso(FondoSaludObligatorio, Contrato) + vbCrLf 'obtenerRecurso(ComunesFormIncompleto)
                ValidateContractSpecifics = False
                'Exit Function
            End If
        ElseIf _contractClass = Eresources.ClaseContratoAprendizajePractica Then
            'minimo salud y arp
            If FundsDatasource.Where(Function(i) i.FundType = EmployeeHelper.FundsTypes.Where(Function(j) j.Item1 = Eresources.Riesgos).FirstOrDefault.Item2 AndAlso i.State = True).Count <= 0 Then
                'TODO agregar recursos
                msj += obtenerRecurso(FondoArlObligatorio, Contrato) + vbCrLf 'obtenerRecurso(ComunesFormIncompleto)
                ValidateContractSpecifics = False
                'Exit Function
            End If

            If FundsDatasource.Where(Function(i) i.FundType = EmployeeHelper.FundsTypes.Where(Function(j) j.Item1 = Eresources.Salud).FirstOrDefault.Item2 AndAlso i.State = True).Count <= 0 Then
                'TODO agregar recursos
                msj += obtenerRecurso(FondoSaludObligatorio, Contrato) + vbCrLf 'obtenerRecurso(ComunesFormIncompleto)
                ValidateContractSpecifics = False
                'Exit Function
            End If
        ElseIf _contractClass = Eresources.ClaseContratoLaboralFijo OrElse _contractClass = Eresources.ClaseContratoLaboralIndefinido Then
            'minimo salud, pension y arp
            If FundsDatasource.Where(Function(i) i.FundType = EmployeeHelper.FundsTypes.Where(Function(j) j.Item1 = Eresources.Riesgos).FirstOrDefault.Item2 AndAlso i.State = True).Count <= 0 Then
                'TODO agregar recursos
                msj += obtenerRecurso(FondoArlObligatorio, Contrato) + vbCrLf 'obtenerRecurso(ComunesFormIncompleto)
                ValidateContractSpecifics = False
                'Exit Function
            End If

            If FundsDatasource.Where(Function(i) i.FundType = EmployeeHelper.FundsTypes.Where(Function(j) j.Item1 = Eresources.Salud).FirstOrDefault.Item2 AndAlso i.State = True).Count <= 0 Then
                'TODO agregar recursos
                msj += obtenerRecurso(FondoSaludObligatorio, Contrato) + vbCrLf 'obtenerRecurso(ComunesFormIncompleto)
                ValidateContractSpecifics = False
                'Exit Function
            End If

            If EmployeeContract.Pensionary = False Then
                If FundsDatasource.Where(Function(i) i.FundType = EmployeeHelper.FundsTypes.Where(Function(j) j.Item1 = Eresources.Pension).FirstOrDefault.Item2 AndAlso i.State = True).Count <= 0 Then
                    'TODO agregar recursos
                    msj += obtenerRecurso(FondoPensionesObligatorio, Contrato) + vbCrLf 'obtenerRecurso(ComunesFormIncompleto)
                    ValidateContractSpecifics = False
                    'Exit Function
                End If
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
                .ContractType = New ContractType With {
                    .Id = INDsleContractTypeId.EditValue,
                    .Name = INDsleContractTypeId.Text,
                    .SalaryType = INDgleSalaryType.EditValue,
                    .JobBondingType = New JobBondingType With {.Id = INDsleJobBondingTypeId.EditValue, .Name = INDsleJobBondingTypeId.Text},
                    .ContractClass = _contractClassDB
                }
                .ContractTypeId = INDsleContractTypeId.EditValue
                .BasicSalary = CDec(INDspnBasicSalary.EditValue)
                .BaseIncome = CDec(INDspnBasicSalary.EditValue) 'Contrato nuevo tiene ingreso base el mismo del sueldo
                .Position = New Position With {.Id = INDslePositionId.EditValue, .Name = INDslePositionId.Text}
                .PositionId = INDslePositionId.EditValue

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
            'Error
            Return
        End If

        'Valores del contrato
        With Contract
            '---------------------------------------------------------------------------------
            .BankId = INDsleBankId.EditValue
            .BankAccountNumber = INDtxtBankAccountNumber.EditValue
            .BankAccountType = INDgleBankAccountType.EditValue

            .Contingency = Contingency
            .FunctionalUnit = New FunctionalUnit With {.Id = INDsleFunctionalUnitId.EditValue, .Name = INDsleFunctionalUnitId.Text}
            .FunctionalUnitId = INDsleFunctionalUnitId.EditValue
            .Group = New Group With {.Id = INDsleGroupId.EditValue, .Name = INDsleGroupId.Text, .CompanyId = GroupXPO.CompanyId}
            .GroupId = INDsleGroupId.EditValue
            .Notes = INDmemNotes.EditValue
            .PaymentPeriod = INDglePaymentPeriod.EditValue
            .PaymentType = INDglePaymentType.EditValue
            .HoursDaily = INDTxtDailyHours.EditValue
            .TrialPeriod = False
            .TrialPeriodSalaryPercentage = 0 'CInt(INDspnTrialPeriodSalaryPercentage.EditValue)
            .TrialPeriodTime = 0 ' CInt(INDspnTrialPeriodTime.EditValue)

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

            'FECHAS Y DURACION - SE COMENTA PARA OBLIGAR AL USUARIO A ELEGIR FECHAS DE MANERA CONSCIENTE

            'If _contract.ContractType.ContractClass = EmployeeHelper.ContractClasses.Where(Function(i) i.Item1 = ClaseContratoLaboralIndefinido).FirstOrDefault.Item2 Then
            INDdteContractInitialDate.EditValue = .ContractInitialDate
            INDdteContractEndingDate.EditValue = .ContractEndingDate

            'End If


            'INDdteContractEndingDate.EditValue = .ContractEndingDate

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

            'ContractClassSpecificBehavior(.ContractType.ContractClass)
            INDbtnAddFunds.Enabled = True

            'Grupo
            INDsleGroupId.Properties.NullText = .Group.Name
            INDsleGroupId.EditValue = .GroupId
            INDglePaymentPeriod.EditValue = .Group.Liquidation
            Presenter.ChangeFunctionalUnitByCompany(.Group.CompanyId)
            Dim g = .Group
            GroupXPO = New PayrollGroupXpo With {.Id = g.Id, .CompanyId = g.CompanyId, .Liquidation = g.Liquidation}
            INDdteContractInitialDate.Properties.MinValue = If(ContractToEdit.ContractInitialDate < .Group.NextDateLiquidation, .Group.NextDateLiquidation, ContractToEdit.ContractInitialDate)
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

            For Each item As FundContract In .FundContract
                FundsDatasource.Add(item)
                FundsPreviouslyCreatedDatasource.Add(item)
            Next
            INDgrdListOfFunds.RefreshDataSource()

            'ÚNICAMENTE PARA ENTIDAS PÚBLICAS

            INDtxtResolutionNumber.EditValue = .ResolutionNumber

            If .ResolutionDate IsNot Nothing Then
                INDdteResolutionDate.EditValue = .ResolutionDate
                'INDdteResolutionDate.Text = String.Format("{0:dd \de MMMM \de yyyy}", .ResolutionDate)
            End If

            INDtxtCertificateOfficeNumber.EditValue = .CertificateOfficeNumber

            If .PosesionDate IsNot Nothing Then
                INDdtePosesionDate.EditValue = .PosesionDate
                'INDdtePosesionDate.Text = String.Format("{0:dd \de MMMM \de yyyy}", .PosesionDate)
            End If

        End With

        'Valores del empleado
        With _contract.Employee
            'INDsleCostCenterId.EditValue = .CostCenterId
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


    Private Function ValidateContractLengths(initialDate As Date, endingDate As Date, minMonthsLength As Integer, maxMonthsLengths As Integer, minAllowedDate As Date, Optional canOverrideLength As Boolean = False) As Boolean
        Dim r = True

        'If initialDate <> Nothing AndAlso endingDate <> Nothing AndAlso _contractClass <> ClaseContratoLaboralIndefinido Then

        '    Dim ts = DateDiff(DateInterval.Month, initialDate, endingDate) + 1

        '    If canOverrideLength = True Then

        '        If endingDate < minAllowedDate Then
        '            'TODO Agregar a recursos
        '            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(ContratosFechaMinimaPermitida, Contrato), minAllowedDate)
        '            r = False
        '        End If

        '        If ts > maxMonthsLengths Then
        '            'TODO agregar recursos
        '            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(ContratosFechaMaximaContratoFijo, Contrato), (maxMonthsLengths / 12))
        '            r = False
        '        End If

        '    Else
        '        If ts < minMonthsLength OrElse ts > maxMonthsLengths Then
        '            'TODO agregar recursos
        '            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(ContratoRangoDeFechas, Contrato), (minMonthsLength / 12), (maxMonthsLengths / 12))
        '            r = False
        '        End If
        '    End If

        'End If

        Return r
    End Function

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

    ''' <summary>
    ''' Dispara el evento de contrato añadido y devuelve en los argumentos el empleado
    ''' </summary>
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

        End If

        If _contractClass = ClaseContratoLaboralFijo And _contractModification = True Then
            If ValidateContractLengths(INDdteContractInitialDate.EditValue, INDdteContractEndingDate.EditValue, MinMonthsContractLength, MaxMonthsContractLength, ContractToEdit.ContractEndingDate, CanOverrideContractLength) = False Then
                Exit Sub
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

        'ContractToEdit = Nothing
        ActionExecuted = True
        RaiseEvent OnContractAdded(Me, New OnContractAddedEventArgs(EmployeeContract)) 'Dispara evento para notificar que el contrato se ha diligenciado
    End Sub

#Region "Fondos"

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
        Else
            'INDbtnAddFunds.HideDropDown()
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


        'INDbtnAddFund.ShowDropDown()

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
        INDLciVoluntaryContributionValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
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
            INDLciVoluntaryContributionValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDLciVoluntaryContributionValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
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

        If s.State = False Then
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

    ' ''' <summary>
    ' ''' Metodo que coloca el foco en el control una vez que popup de fondos se abre
    ' ''' </summary>
    'Private Sub INDpccFunds_GotFocus() Handles INDpccFunds.GotFocus
    '    INDsleFundIds.Focus()
    'End Sub


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

#End Region

    ''' <summary>
    ''' Evento Load donde se ejecutan el asincrono para levantar la definicion del layout
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmContract_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Cargamos de manera asincrona definiciones del funcional

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
        If IsDesignMode = False Then
            'Presenter = New PContract(Me)
            Initializes()
            CleanControls()
        End If
        'TODO BORRAR
        'INDtxtContractId.EditValue = "Nuevo"
        InitializeTuple()


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
                'Se dispara la verificacion de fechas
                'ContractDateValidation_Leave(Nothing, EventArgs.Empty)
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
            INDdteContractInitialDate.Properties.MinValue = If(ContractToEdit.ContractInitialDate < GroupXPO.FechaProximaLiquidacion, GroupXPO.FechaProximaLiquidacion, ContractToEdit.ContractInitialDate)
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
    Private Sub INDdteContractInitialDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDdteContractInitialDate.EditValueChanged
        If INDdteContractInitialDate.EditValue IsNot Nothing Then
            INDdteContractEndingDate.Properties.MinValue = INDdteContractInitialDate.EditValue
            INDdteFundInitialDate.Properties.MinValue = ContractToEdit.JobBondingDate
            If INDdteContractEndingDate.EditValue IsNot Nothing Then
                INDdteFundInitialDate.Properties.MaxValue = INDdteContractEndingDate.EditValue
            Else
                INDdteFundInitialDate.Properties.MaxValue = INDdteContractInitialDate.EditValue
            End If
        End If

    End Sub

    ''' <summary>
    ''' Metodo que notifica que la fecha esta por fuera del rango
    ''' </summary>
    Private Sub ContractDateValidation_Leave(sender As Object, e As EventArgs) Handles INDdteContractInitialDate.Leave, INDdteContractEndingDate.Leave

        If INDdteContractInitialDate.EditValue <> Nothing AndAlso INDdteContractEndingDate.EditValue <> Nothing Then
            If _contractClass = ClaseContratoLaboralFijo Then
                If ValidateContractLengths(INDdteContractInitialDate.EditValue, INDdteContractEndingDate.EditValue, MinMonthsContractLength, MaxMonthsContractLength, ContractToEdit.ContractEndingDate, CanOverrideContractLength) = False Then
                    INDdteContractInitialDate.EditValue = Nothing
                    INDdteContractEndingDate.EditValue = Nothing
                End If
            End If
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
                    'Se dispara la verificacion de fechas
                    'ContractDateValidation_Leave(Nothing, EventArgs.Empty)
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
                'Se dispara la verificacion de fechas
                'ContractDateValidation_Leave(Nothing, EventArgs.Empty)
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

                    Dim cd = If(ContractToEdit IsNot Nothing, ContractToEdit.ContractEndingDate, Nothing)

                    If ValidateContractLengths(INDdteContractInitialDate.EditValue, INDdteContractEndingDate.EditValue, MinMonthsContractLength, MaxMonthsContractLength, cd, CanOverrideContractLength) = False Then
                        'INDdteContractInitialDate.EditValue = Nothing
                        INDspnContractLengthYear.EditValue = 0
                        INDspnContractLengthMonth.EditValue = 0
                        INDspnContractLengthDay.EditValue = 0
                        INDdteContractEndingDate.EditValue = Nothing
                    End If

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

            If ValidateContractLengths(INDdteContractInitialDate.EditValue, INDdteContractEndingDate.EditValue, MinMonthsContractLength, MaxMonthsContractLength, ContractToEdit.ContractEndingDate, CanOverrideContractLength) = False Then
                'INDdteContractInitialDate.EditValue = Nothing
                INDdteContractEndingDate.EditValue = Nothing
            Else

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
        End If

    End Sub
#End Region



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

#Region "CloseUp"

    Private Sub INDpceDetailsBook_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDpceDetailsBook.CloseUp
        If FlagEditModePopupBook Then
            'CleanControlsPopup()
        End If
        If e.CloseMode = DevExpress.XtraEditors.PopupCloseMode.Cancel Then
            'INDbtnAddParts.Focus()
        End If
    End Sub

#End Region

    Private Sub INDpceDetailsBook_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDpceDetailsBook.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.F4 OrElse e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDpceDetailsBook.ShowPopup()
        End If
    End Sub

    Private Sub INDpceDetailsBook_Popup(sender As Object, e As EventArgs) Handles INDpceDetailsBook.Popup
        INDsleFundIds.Focus()
    End Sub

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
        Else
            'INDbtnAddFunds.HideDropDown()
        End If

        CleanFundsPopup()
        INDgrdListOfFunds.DataSource = Nothing
        INDgrdListOfFunds.DataSource = FundsDatasource


        FundToEdit = Nothing
        deletingFundDate = Nothing

        INDpceDetailsBook.ClosePopup()
    End Sub
End Class
