'***********************************************************************
' Assembly         : Presentacion.Payroll
' Author           : Jose Luis Rojas
' Created          : 13-08-2011
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports System.ComponentModel
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Data.Async.Helpers
Imports DevExpress.XtraEditors.Controls
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.Payroll.MVP
Imports Presentation.Common.MVP

#End Region

''' <summary>
''' Formulario de liquidacion de nomina              
''' </summary>
Public Class FrmPayrollLiquidationDetail
    Implements IPayrollLiquidation

#Region "Worker"

    ''' <summary>
    ''' Variable para almacenar el listado de empleados
    ''' </summary>
    Dim ListEmployee As List(Of Employee)

    ''' <summary>
    ''' control donde se muestra el progreso de la operacion de importar archivos
    ''' </summary>
    ''' <remarks></remarks>
    Dim progress As CtrProgress

    ''' <summary>
    ''' total de los items a procesar
    ''' </summary>
    ''' <remarks></remarks>
    Dim totalItems As Integer

    ''' <summary>
    ''' items procesados
    ''' </summary>
    ''' <remarks></remarks>
    Dim totalProcessedItems As Integer

    ''' <summary>
    ''' items que se van a enviar en cada proceso
    ''' </summary>
    ''' <remarks></remarks>
    Dim itemsSend As Integer = 50

    ''' <summary>
    ''' Almacena detalles de la liquidación
    ''' </summary>
    Dim DetailLiquidation As IEnumerable(Of LiquidationDetail)

    ''' <summary>
    ''' Almacena el Id del empleado a consultar
    ''' </summary>
    Dim EmployeeId As Integer

    ''' <summary>
    ''' Almacena el Id del grupo del empleado
    ''' </summary>
    Dim IdGroup As Integer

    ''' <summary>
    ''' Almacena el Id del tercero
    ''' </summary>
    Dim IdThirdParty As Integer

    ''' <summary>
    ''' metodo para mostrar en el control cuantos items se han procesado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getInfo() As Tuple(Of String, String)
        Return New Tuple(Of String, String)(totalProcessedItems.ToString(), totalItems.ToString())
    End Function

#End Region

#Region "BUILDER"
    Public Sub New()
        InitializeComponent()
        ctrTmp = New CtrTotalPayrollLiquidation()
        ctrTmp.SetInfoFunction(AddressOf getInfoServiceOrder)
        ctrTmp.PrintInfo()
        ctrTmp.Dock = System.Windows.Forms.DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrTmp)
    End Sub

    ''' <summary>
    ''' retorna la informacion que se establece en el control de la barra
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getInfoServiceOrder() As Tuple(Of Double, Double, Double)

        If Not IsNumeric(INDteTotalPaid.EditValue) Then
            INDteTotalPaid.EditValue = 0
        End If

        If Not IsNumeric(INDteTotalAccrued.EditValue) Then
            INDteTotalAccrued.EditValue = 0
        End If

        If Not IsNumeric(INDteTotalDeducted.EditValue) Then
            INDteTotalDeducted.EditValue = 0
        End If
        Presenter = New PPayrollLiquidation(Me)
        SetCurrencyFormat(Presenter.LoadPayrollSettings().CurrencyId.Abbreviation)
        Return New Tuple(Of Double, Double, Double)(CDbl(INDteTotalPaid.EditValue), CDbl(INDteTotalAccrued.EditValue), CDbl(INDteTotalDeducted.EditValue))
    End Function
#End Region

    ''' <summary>
    ''' Evento que llama al cierre del popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event ClosePopup(sender As Object, e As EventArgs)

    ''' <summary>
    ''' Control para establecer informacion del ingreso
    ''' </summary>
    Private ctrTmp As CtrTotalPayrollLiquidation

    Private listLiquitadionEmployee As List(Of Liquidation)

    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PPayrollLiquidation

    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Dim indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Variable que establece un listado de los grupos
    ''' </summary>
    ''' <remarks></remarks>
    Dim GroupList As List(Of Group)

    ''' <summary>
    ''' Variable que contiene la ruta de las definiciones del layout
    ''' </summary>
    Dim PathFunctionalDefinitions As String

    ''' <summary>
    ''' Evento que se utiliza para cargar las definiciones del funcional
    ''' </summary>
    WithEvents LoadhronousDefinitions As BackgroundWorker

    ''' <summary>
    ''' Variable de Action Result para la Liquidación Procesada
    ''' </summary>
    ''' <remarks></remarks>
    Dim liquidationProcess As New ActionMessageResult(Of List(Of Liquidation))

    ''' <summary>
    ''' Variable para Almacenar la Liquidación a Mostrar
    ''' </summary>
    ''' <remarks></remarks>
    Private LiquidationToShow As Liquidation

    ''' <summary>
    ''' Variable para mostrar las Fórmulas
    ''' </summary>
    ''' <remarks></remarks>
    Dim FormulasToShow As LiquidationDetail

    ''' <summary>
    ''' Variable de Bandera de Consulta
    ''' </summary>
    ''' <remarks></remarks>
    Dim consultFlag As String = ""

    ''' <summary>
    ''' Variable de Lista de Liquidaciones
    ''' </summary>
    ''' <remarks></remarks>
    Dim liquidationEmployee As List(Of Liquidation) = New List(Of Liquidation)

    ''' <summary>
    ''' Listado de los empleados liquidados recién calculados
    ''' </summary>
    Dim EmployeeLiquidated As List(Of Liquidation)

    ''' <summary>
    ''' Variable donde se almacena los ID de Grupos
    ''' </summary>
    ''' <remarks></remarks>
    Dim StringIdGroup As String = ""

    ''' <summary>
    ''' Variable donde almaceno el Grupo que voy a mostrar en Consulta Grupos
    ''' </summary>
    ''' <remarks></remarks>
    Dim GroupShow As New List(Of Group)

    Dim PayrollDateLiquidated As Date

    Dim CargaInicialNomina As Boolean = False

    Dim objectMessageLiquidation As Liquidation

    Dim FlagLiquidation As String

    Dim GroupIdConfirmLiquidation As Integer = 0

    Dim LiquidationDetailShow As List(Of Liquidation)

#Region "Properties"
    Public Property StatusForeclousure As String
        Get
            Return Me.BarraBotones.StatusRecord
        End Get
        Set(value As String)
        End Set
    End Property
#End Region

    Public Sub Buscar() Implements ICrudBase.Buscar

    End Sub

    Public Sub Deshacer() Implements ICrudBase.Deshacer

    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar

    End Sub

    Public Sub Guardar() Implements ICrudBase.Guardar

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    Public Sub Nuevo() Implements ICrudBase.Nuevo

    End Sub

    ''' <summary>
    ''' Propiedad que establece el data source de los grupos de concepto
    ''' </summary>
    Public WriteOnly Property DatasourceGroup As List(Of Group) Implements IPayrollLiquidation.DatasourceGroup
        Set(value As List(Of Group))
            INDGcGroup.DataSource = value
            PutFalse(INDGcGroup.DataSource)
        End Set
    End Property

    ''' <summary>
    ''' Evento Load donde se ejecutan el asincrono para levantar la definicion del layout
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub FrmPayrollLiquidationDetail_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Cargamos de manera asincrona definiciones del funcional
        PathFunctionalDefinitions = String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name, "\ModuloPayrollLiquidation.", Me.Name, ".xml")
        LoadhronousDefinitions = New BackgroundWorker
        If LoadhronousDefinitions.IsBusy = False Then
            LoadhronousDefinitions.RunWorkerAsync()
        End If
        'Me.IndigoGridControl1.SetHoldSize(Me.INDGcDetailLiquidation, True)
        'Me.IndigoGridControl1.SetHoldSize(Me.INDGcDetailLiquidationPatrono, True)
        Presenter = New PPayrollLiquidation(Me)
        AsyncLoader(True)
        Await Presenter.Load_Group()
        Await CargarLiquidacionesPrevias()
        AsyncLoader(False)
        CtrNavigation1.Visible = False
        CtrNavigationControl1.Visible = False
        Me.LoadStatus()

    End Sub


    ''' <summary>
    ''' Método para Buscar
    ''' </summary>
    Public Sub AbrirBusqueda() Implements ICrudBase.OpenSearch
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.Employee
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Cedula", .FieldName = "Nit"}, New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name"}, New ColumnInfo() With {.Caption = "Grupo", .FieldName = "Descripcion"}, New ColumnInfo() With {.Caption = "IdGrupo", .FieldName = "GroupId", .Visible = False}}.ToList()
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub


    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        INDbteIdNumber.Text = ReturnValue
        Await LoadControls()
        Dim group = CType(ReturnObject, ReadonlyThreadSafeProxyForObjectFromAnotherThread)
        If group IsNot Nothing Then
            ' INDGcGroup.Enabled = False
            Dim GroupId = group.OriginalRow.GroupId
            GroupSelect(GroupId)
        End If
    End Sub

#Region "Methods"

    ''' <summary>
    ''' Función para Seleccionar el Grupo
    ''' </summary>
    ''' <param name="groupId"></param>
    ''' <remarks></remarks>
    Private Sub GroupSelect(ByVal groupId As System.Object)

        Dim list_group As List(Of Group) = INDGcGroup.DataSource

        For Each groupEnt As Group In list_group
            groupEnt.Apply = False
        Next

        For Each groupEnt As Group In list_group
            If groupId = groupEnt.Id Then
                groupEnt.Apply = True
            End If
        Next

        INDGcGroup.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Función para Colocar los Grupos en Falso
    ''' </summary>
    ''' <param name="Group"></param>
    ''' <remarks></remarks>
    Private Sub PutFalse(ByVal Group As List(Of Group))
        For i As Integer = 0 To (Group.Count() - 1)
            Group.Item(i).Apply = False
        Next

    End Sub

    Private Async Sub RepositoryItemButtonEdit1_Click(sender As Object, e As EventArgs)

        CtrNavigationControl1.Visible = True

        INDlyCtrNavigation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDlyLblEmployeeName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDlyGResultLiquidation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDlyGResultLiquidation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        LayoutControlItem10.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

        'INDGcGroup.Enabled = False
        INDbteIdNumber.Enabled = False

        'LiquidationToShow = INDGvEmployee.GetFocusedRow()
        Await AssignValuesLiquidation(LiquidationToShow)
    End Sub

    ''' <summary>
    ''' Función para Asignar los Valores de la Liquidación
    ''' </summary>
    ''' <param name="LiquidationToShow"></param>
    ''' <remarks></remarks>
    Private Async Function AssignValuesLiquidation(ByVal LiquidationToShow As Liquidation) As Task

        Me.EmployeeId = LiquidationToShow.EmployeeId
        Me.IdGroup = LiquidationToShow.GroupId

        objectMessageLiquidation = LiquidationToShow

        INDteBasicSalary.Focus()

        Dim LiquidationDetail As New List(Of LiquidationDetail)
        Dim BasicSalary As Double = LiquidationToShow.BasicSalary
        Dim PayrollDays As Integer = LiquidationToShow.PayrollDays

        Dim DailyBasicSalary As Double = BasicSalary / 30

        Dim DetailLiquidationConceptType As IEnumerable(Of LiquidationDetail)
        Dim DetailLiquidationPatronoConceptType As IEnumerable(Of LiquidationDetail)

        INDLblEmployeeName.Text = LiquidationToShow.FullNameEmployee & " ( Fecha Liquidación: " & LiquidationToShow.PayrollDateLiquidated & " ) "

        Dim StatusLiquidation As Integer = 0
        If LiquidationToShow.RegisterStatus = " " Then
            StatusLiquidation = 1
        ElseIf LiquidationToShow.RegisterStatus = "C" Then
            StatusLiquidation = 2
        ElseIf LiquidationToShow.RegisterStatus = "S" Then
            StatusLiquidation = 3
        End If
        BarraBotones.StatusRecord = StatusLiquidation
        INDteBasicSalary.Text = BasicSalary
        INDteDailyBasicSalary.Text = DailyBasicSalary
        INDteTotalAccrued.Text = LiquidationToShow.TotalAccrued
        INDteTotalDeducted.Text = LiquidationToShow.TotalDeducted
        INDteTotalPaid.Text = LiquidationToShow.TotalPaid

        INDteWorkDays.Text = LiquidationToShow.DaysWorked
        INDtePensionDays.Text = IIf(LiquidationToShow.PensionEnrollmentDays Is Nothing, 0, LiquidationToShow.PensionEnrollmentDays)
        INDteSanctionDays.Text = IIf(LiquidationToShow.SanctionDays Is Nothing, 0, LiquidationToShow.SanctionDays)
        INDteARLDays.Text = IIf(LiquidationToShow.OccupationalRisksDays Is Nothing, 0, LiquidationToShow.OccupationalRisksDays)
        INDteLicensesDays.Text = IIf(LiquidationToShow.LicenseDays Is Nothing, 0, LiquidationToShow.LicenseDays)
        INDteHealthDays.Text = IIf(LiquidationToShow.QuoteHealthDays Is Nothing, 0, LiquidationToShow.QuoteHealthDays)
        INDtePayrollDays.Text = LiquidationToShow.PayrollDays
        INDteVacationDays.Text = IIf(LiquidationToShow.VacationDays Is Nothing, 0, LiquidationToShow.VacationDays)
        INDteInabilitiesDays.Text = IIf(LiquidationToShow.DisabilityDays Is Nothing, 0, LiquidationToShow.DisabilityDays)
        INDteProvisionDays.Text = IIf(LiquidationToShow.ProvisionDays Is Nothing, 0, LiquidationToShow.ProvisionDays)
        INDteUnpaidLicensesDays.Text = IIf(LiquidationToShow.UnpaidLicenseDays Is Nothing, 0, LiquidationToShow.UnpaidLicenseDays)

        INDtePeriodIBC.Text = IIf(LiquidationToShow.PeriodJCB Is Nothing, 0, LiquidationToShow.PeriodJCB)
        INDtePensionIBC.Text = IIf(LiquidationToShow.PensionJCB Is Nothing, 0, LiquidationToShow.PensionJCB)
        INDteHealthIBC.Text = IIf(LiquidationToShow.HealthJCB Is Nothing, 0, LiquidationToShow.HealthJCB)
        INDteRTFIBC.Text = IIf(LiquidationToShow.TotalBaseRetention Is Nothing, 0, LiquidationToShow.TotalBaseRetention)
        INDteUnemploymentIBC.Text = IIf(LiquidationToShow.IBCUnemployment Is Nothing, 0, LiquidationToShow.IBCUnemployment)
        INDteARLIBC.Text = IIf(LiquidationToShow.IBCOccupationalRisks.ToString = String.Empty, 0, LiquidationToShow.IBCOccupationalRisks)
        INDteSENAIBC.Text = IIf(LiquidationToShow.IBCSENA Is Nothing, 0, LiquidationToShow.IBCSENA)
        INDteCompensationFundIBC.Text = IIf(LiquidationToShow.IBCCompensationFund Is Nothing, 0, LiquidationToShow.IBCCompensationFund)
        INDteICBFIBC.Text = IIf(LiquidationToShow.IBCICBF Is Nothing, 0, LiquidationToShow.IBCICBF)
        INDteVacationIBC.Text = IIf(LiquidationToShow.IBCVacation Is Nothing, 0, LiquidationToShow.IBCVacation)
        INDteIncentivePaymentIBC.Text = IIf(LiquidationToShow.IBCIncentivePayment Is Nothing, 0, LiquidationToShow.IBCIncentivePayment)

        If LiquidationToShow.LiquidationDetail IsNot Nothing Then
            LiquidationDetail = LiquidationToShow.LiquidationDetail.ToList()

            DetailLiquidationConceptType = LiquidationDetailWithQuantity(LiquidationToShow)
            DetailLiquidationPatronoConceptType = LiquidationToShow.LiquidationDetail.Where(Function(x) x.ConceptType = 3)

            DetailLiquidation = DetailLiquidationConceptType
            INDGcDetailLiquidation.DataSource = DetailLiquidationConceptType
            INDGcDetailLiquidationPatrono.DataSource = DetailLiquidationPatronoConceptType

            INDGcMessage.DataSource = LiquidationToShow.Message.ToList()
        End If

        CalculateValue()

    End Function

    ''' <summary>
    ''' Metodo que permite asignar las cantidades a los conceptos de liquidación
    ''' </summary>
    ''' <param name="liquidation"></param>
    ''' <returns></returns>
    Private Function LiquidationDetailWithQuantity(liquidation As Liquidation) As IEnumerable(Of LiquidationDetail)
        Dim detailLiquidationEmployeeConcepts = liquidation.LiquidationDetail.Where(Function(x) x.ConceptType <> 3).ToList()

        For Each itemDetail In detailLiquidationEmployeeConcepts
            If itemDetail.Quantity = 0 Then
                Select Case itemDetail.ConceptClass
                    Case "001", "012", "013", "051", "042", "043", "050"
                        itemDetail.Quantity = If(itemDetail.TotalNumberHours, 0)
                    Case "005", "006"
                        itemDetail.Quantity = (liquidation.DaysWorked)
                    Case "021", "022"
                        itemDetail.Quantity = If(liquidation.DisabilityDays, 0)
                    Case "027"
                        itemDetail.Quantity = If(liquidation.OccupationalRisksDays, 0)
                    Case "023"
                        itemDetail.Quantity = If(liquidation.MaternityLeaveDays, 0)
                    Case "024"
                        itemDetail.Quantity = If(liquidation.LicenseDays > 0, liquidation.LicenseDays, If(liquidation.DisabilityDays > 0, liquidation.DisabilityDays, 0))
                    Case "025"
                        itemDetail.Quantity = If(liquidation.UnpaidLicenseDays, 0)
                    Case "026"
                        itemDetail.Quantity = If(liquidation.SanctionDays, 0)
                    Case "017"
                        itemDetail.Quantity = If(liquidation.QuoteHealthDays, 0)
                    Case "014"
                        itemDetail.Quantity = If(liquidation.PensionEnrollmentDays, 0)
                    Case "030"
                        itemDetail.Quantity = If(liquidation.VacationDays, 0)
                    Case "067" 'Incapacidad Ambulatoria Patrono
                        itemDetail.Quantity = If(itemDetail.EmployeerDays, 0)
                    Case "068" 'Incapacidad Ambulatoria ERP
                        itemDetail.Quantity = If(itemDetail.ErpDays, 0)
                    Case "069" 'Incapacidad Hospitalaria Patrono
                        itemDetail.Quantity = If(itemDetail.EmployeerDays, 0)
                    Case "070" 'Incapacidad Hospitalaria ERP
                        itemDetail.Quantity = If(itemDetail.ErpDays, 0)
                    Case "071" 'Calamidad
                        itemDetail.Quantity = liquidation.CalamityDays
                    Case "028" 'Permiso
                        itemDetail.Quantity = liquidation.PermissionDays
                    Case "072" 'Licencia remunerada
                        itemDetail.Quantity = liquidation.PaidLeaveDays
                    Case "073" 'Vacaiones en dinero
                        itemDetail.Quantity = liquidation.VacationDaysInCash
                    Case "074" 'Licencia de luto
                        itemDetail.Quantity = If(itemDetail.LutoDays, 0)
                    Case "075" 'Riesgo profesional patrono
                        itemDetail.Quantity = liquidation.EmployerProfessionalDisabilityDays
                    Case "076" 'Riesgo profesional erp
                        itemDetail.Quantity = liquidation.ERPProfessionalDisabilityDays
                End Select
            End If
        Next
        Return detailLiquidationEmployeeConcepts
    End Function


    ''' <summary>
    ''' Limpiar Controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function CleanControls() As Task

        INDteBasicSalary.Text = String.Empty
        INDteDailyBasicSalary.Text = String.Empty
        INDteTotalAccrued.Text = String.Empty
        INDteTotalDeducted.Text = String.Empty
        INDteTotalPaid.Text = String.Empty

        'INDteHourDay.Text = String.Empty
        INDteWorkDays.Text = String.Empty
        INDtePensionDays.Text = String.Empty
        INDteSanctionDays.Text = String.Empty
        INDteARLDays.Text = String.Empty
        INDteLicensesDays.Text = String.Empty
        INDteHealthDays.Text = String.Empty
        INDtePayrollDays.Text = String.Empty
        INDteVacationDays.Text = String.Empty
        INDteInabilitiesDays.Text = String.Empty
        INDteProvisionDays.Text = String.Empty
        INDteUnpaidLicensesDays.Text = String.Empty

        INDtePeriodIBC.Text = String.Empty
        INDtePensionIBC.Text = String.Empty
        INDteHealthIBC.Text = String.Empty
        INDteARLIBC.Text = String.Empty
        INDteRTFIBC.Text = String.Empty
        INDteUnemploymentIBC.Text = String.Empty
        INDteSENAIBC.Text = String.Empty
        INDteCompensationFundIBC.Text = String.Empty
        INDteICBFIBC.Text = String.Empty
        INDteVacationIBC.Text = String.Empty
        INDteIncentivePaymentIBC.Text = String.Empty

        INDGcDetailLiquidation.DataSource = Nothing
        INDGcDetailLiquidationPatrono.DataSource = Nothing
        'INDGcEmployeeLiquidated.DataSource = Nothing
        INDGcMessage.DataSource = Nothing
        'INDGcMessagesLiquidation.DataSource = Nothing
        INDbteIdNumber.Text = String.Empty
        INDColGroupLiquidationView.Visible = False

        AsyncLoader(True)
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))

        Me.BarraBotones.PrepareToolbar(eAction.OnlyLiquidate)
        Me.BarraBotones.RibbonPageRejillas.Visible = False
        CtrNavigation1_ClickBack()

        Await CargarLiquidacionesPrevias()
        AsyncLoader(False)
    End Function

    Private Sub PartialCleanControls()
        INDteBasicSalary.Text = String.Empty
        INDteDailyBasicSalary.Text = String.Empty
        INDteTotalAccrued.Text = String.Empty
        INDteTotalDeducted.Text = String.Empty
        INDteTotalPaid.Text = String.Empty

        'INDteHourDay.Text = String.Empty
        INDteWorkDays.Text = String.Empty
        INDtePensionDays.Text = String.Empty
        INDteSanctionDays.Text = String.Empty
        INDteARLDays.Text = String.Empty
        INDteLicensesDays.Text = String.Empty
        INDteHealthDays.Text = String.Empty
        INDtePayrollDays.Text = String.Empty
        INDteVacationDays.Text = String.Empty
        INDteInabilitiesDays.Text = String.Empty
        INDteProvisionDays.Text = String.Empty
        INDteUnpaidLicensesDays.Text = String.Empty

        INDtePeriodIBC.Text = String.Empty
        INDtePensionIBC.Text = String.Empty
        INDteHealthIBC.Text = String.Empty
        INDteARLIBC.Text = String.Empty
        INDteRTFIBC.Text = String.Empty
        INDteUnemploymentIBC.Text = String.Empty
        INDteSENAIBC.Text = String.Empty
        INDteCompensationFundIBC.Text = String.Empty
        INDteICBFIBC.Text = String.Empty
        INDteVacationIBC.Text = String.Empty
        INDteIncentivePaymentIBC.Text = String.Empty

        INDGcDetailLiquidation.DataSource = Nothing
        INDGcDetailLiquidationPatrono.DataSource = Nothing
        'INDGcEmployeeLiquidated.DataSource = Nothing
        INDGcMessage.DataSource = Nothing
        'INDGcMessagesLiquidation.DataSource = Nothing
    End Sub

    Private Sub AssignValues(ByVal FormulasToShow As LiquidationDetail)
        INDteUsedFormula.Text = String.Empty
        INDteReplaceFormula.Text = String.Empty
        INDteResultFormula.Text = String.Empty
        INDteUsedFormula.Text = FormulasToShow.ConceptFormulate
        INDteReplaceFormula.Text = FormulasToShow.ReplaceConceptFormulate
        INDteResultFormula.Text = FormulasToShow.ConceptTotalValue
    End Sub


    Private Sub AssignValuesEmployer(ByVal FormulasToShow As LiquidationDetail)
        INDteUsedFormulaPatrono.Text = String.Empty
        INDteReplaceFormulaPatrono.Text = String.Empty
        INDteResultFormulaPatrono.Text = String.Empty
        INDteUsedFormulaPatrono.Text = FormulasToShow.ConceptFormulate
        INDteReplaceFormulaPatrono.Text = FormulasToShow.ReplaceConceptFormulate
        INDteResultFormulaPatrono.Text = FormulasToShow.ConceptTotalValue
    End Sub


    Private Sub INDbteIdNumber_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDbteIdNumber.ButtonClick
        AbrirBusqueda()
    End Sub


    Private Sub CtrNavigation1_ClickBack() Handles CtrNavigation1.ClickBack
        LayoutControlGroup12.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyCtrNavigation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyLblEmployeeName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyGResultLiquidation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        LayoutControlItem10.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        CtrNavigationControl1.Visible = False

        INDlyBteIdNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDlyGcGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        'INDGcGroup.Enabled = True
        INDbteIdNumber.Enabled = True
        Me.BarraBotones.FilterDataSource = Nothing
        Me.BarraBotones.PrepareToolbar(eAction.OnlyLiquidate)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = True

        INDColGroupLiquidationView.Visible = False
        IndColVisualizarLiquidacion.Visible = True
        GroupIdConfirmLiquidation = 0
        LiquidationDetailShow = Nothing
        objectMessageLiquidation = Nothing
        FlagLiquidation = "Nueva"
        Me.BarraBotones.StatusRecordVisible = False

    End Sub


    Private Async Sub INDbteIdNumber_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbteIdNumber.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If Not String.IsNullOrEmpty(INDbteIdNumber.Text.ToString) Then
                Await LoadControls()
                ' INDGcGroup.Enabled = True
            End If
        ElseIf e.KeyCode = Keys.F4 Then
            AbrirBusqueda()
        End If
    End Sub

    Private Async Function LoadControls() As Task
        Try

            AsyncLoader(True)
            Dim employee As Employee
            Dim groupId As String = 1

            Using Model As New MEmployee(MEmployee.TAG)
                'AsyncLoader(True)
                employee = Await Model.GetEmployeeAsync(INDbteIdNumber.Text)
                'AsyncLoader(False)
            End Using

            If employee Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(NoExisteEmpleado, Eform.LiquidacionNomina)
                AsyncLoader(False)
                Return
            End If

            If employee.Id <= 0 Then
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(NoExisteEmpleado, Eform.LiquidacionNomina)
                AsyncLoader(False)
                Return
            End If

            For i As Integer = 0 To (employee.Contract.Count() - 1)

                If employee.Contract.Item(i).Valid = True Then
                    groupId = employee.Contract.Item(i).GroupId
                End If

            Next

            Dim list_group As List(Of Group) = INDGcGroup.DataSource

            For Each groupEnt As Group In list_group
                groupEnt.Apply = False
            Next

            For Each groupEnt As Group In list_group
                If groupEnt.Id = groupId Then
                    groupEnt.Apply = True
                End If
            Next

            INDGcGroup.RefreshDataSource()

            Await ExecuteLiquidation()

            AsyncLoader(False)
        Catch ex As Exception
            AsyncLoader(False)
        End Try
    End Function

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


    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ctrTmp = Nothing
        listLiquitadionEmployee = Nothing
        Presenter = Nothing
        GroupList = Nothing
        PathFunctionalDefinitions = Nothing
        liquidationProcess = Nothing
        LiquidationToShow = Nothing
        FormulasToShow = Nothing
        consultFlag = Nothing
        liquidationEmployee = Nothing
        StringIdGroup = Nothing
        GroupShow = Nothing
        PayrollDateLiquidated = Nothing
        CargaInicialNomina = Nothing
        objectMessageLiquidation = Nothing
        FlagLiquidation = Nothing
        GroupIdConfirmLiquidation = Nothing
        LiquidationDetailShow = Nothing
        progress = Nothing
    End Sub


    ''' <summary>
    ''' Función para Mostrar u Ocultar en la Columna de Visualización de Liquidaciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGvGroup_CustomDrawCell(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles INDGvGroup.CustomDrawCell

        If INDColGroupLiquidationView.Name = e.Column.Name Then

            If liquidationEmployee IsNot Nothing Then
                Dim group = CType(INDGvGroup.GetRow(e.RowHandle), Group)
                If group IsNot Nothing Then
                    Dim LiquidationShow = liquidationEmployee.FindAll(Function(x) x.GroupId = group.Id).Count()
                    If LiquidationShow > 0 Then
                        e.DisplayText = "Visualizar"
                    Else
                        e.DisplayText = ""
                    End If
                Else
                    e.DisplayText = ""
                End If
            End If
        End If

        If IndColVisualizarLiquidacion.Name = e.Column.Name Then

            If liquidationEmployee IsNot Nothing Then
                Dim group = CType(INDGvGroup.GetRow(e.RowHandle), Group)
                If group IsNot Nothing Then
                    Dim LiquidationShow = liquidationEmployee.FindAll(Function(x) x.GroupId = group.Id).Count()

                    If LiquidationShow > 0 Then
                        e.DisplayText = "Visualizar"
                    Else
                        e.DisplayText = ""
                    End If
                Else
                    e.DisplayText = ""
                End If
            End If
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
        ctrTmp.CurrencyAbbreviation = _currencyAbbreviation
		changeNumericFormatByCurrency(numberFormat)
		'se establece la cultura especifica Solo a los controles de texto que muestran valores con formato moneda
		Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
		_culture.NumberFormat = _currencyAbbreviation.GetNumberFormat
		INDteResultFormula.Properties.Mask.Culture = _culture
		INDteResultFormulaPatrono.Properties.Mask.Culture = _culture
		INDteBasicSalary.Properties.Mask.Culture = _culture
		INDteDailyBasicSalary.Properties.Mask.Culture = _culture
		INDteTotalAccrued.Properties.Mask.Culture = _culture
		INDteTotalDeducted.Properties.Mask.Culture = _culture
		INDteTotalPaid.Properties.Mask.Culture = _culture
		INDtePeriodIBC.Properties.Mask.Culture = _culture
		INDtePensionIBC.Properties.Mask.Culture = _culture
		INDteHealthIBC.Properties.Mask.Culture = _culture
		INDteARLIBC.Properties.Mask.Culture = _culture
		INDteRTFIBC.Properties.Mask.Culture = _culture
		INDteUnemploymentIBC.Properties.Mask.Culture = _culture
		INDteSENAIBC.Properties.Mask.Culture = _culture
		INDteCompensationFundIBC.Properties.Mask.Culture = _culture
		INDteICBFIBC.Properties.Mask.Culture = _culture
		INDteVacationIBC.Properties.Mask.Culture = _culture
		INDteIncentivePaymentIBC.Properties.Mask.Culture = _culture

		INDColAccrued = Window.Utils.FormatGrid(INDColAccrued, _currencyAbbreviation)
        INDColDeducted = Window.Utils.FormatGrid(INDColDeducted, _currencyAbbreviation)
        INDColParafiscalConceptValue = Window.Utils.FormatGrid(INDColParafiscalConceptValue, _currencyAbbreviation)
    End Sub

#End Region

#Region "Bar buttons events"

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        AsyncLoader(True)
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        AsyncLoader(False)

        Me.BarraBotones.PrepareToolbar(eAction.OnlyLiquidate)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = True
        Me.BarraBotones.RibbonPageRejillas.Visible = False
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        Buscar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
        'Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        CleanControls()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click customizar.
    ''' </summary>
    Private Sub BarraBotones_ClickCustomizar() Handles BarraBotones.ClickCustomizar
        'CustomizationOpen()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ clic restablecer layout.
    ''' </summary>
    Private Sub BarraBotones_ClicRestablecerLayout() Handles BarraBotones.ClicRestablecerLayout
        'ResetLayout()
    End Sub

    ''' <summary>
    ''' Click liquidar del barrabotones
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_ClickLiquidar() Handles BarraBotones.ClickLiquidar
        Try
            Await ExecuteLiquidation()
        Catch ex As Exception
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' Click consultar liquidacion del barrabotones
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_ClickConsultLiquidar() Handles BarraBotones.ClickConsultLiquidar
        INDColGroupLiquidationView.Visible = False

        Dim VarGroupId As String = ""

        Dim FlagVisualizar As Boolean = False
        CargaInicialNomina = False

        Using Model As New MPayrollLiquidation

            Dim list_group As List(Of Group) = INDGcGroup.DataSource
            Dim list_group_chek As List(Of Group)
            Dim StringIdGroup As String = ""

            list_group_chek = list_group.FindAll(Function(x) x IsNot Nothing AndAlso x.Apply = True)

            If list_group_chek IsNot Nothing Then

                If list_group_chek.Count > 1 Then
                    Mensaje(EeventViewerImages.Advertencia) = "Para visualizar, debe seleccionar un solo grupo"
                    BarraBotones.Enabled = True
                    Return
                End If


                For i As Integer = 0 To (list_group_chek.Count() - 1)
                    BarraBotones.Enabled = False
                    If list_group_chek.Count() = 1 Then
                        StringIdGroup = list_group_chek.Item(i).Id.ToString()
                    End If
                Next
            Else
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(NoHayGruposSeleccionadosNomina, Eform.LiquidacionNomina)
                BarraBotones.Enabled = True
                Return
            End If

            If StringIdGroup = "" Or StringIdGroup = String.Empty Then
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(NoHayGruposSeleccionadosNomina, Eform.LiquidacionNomina)
                BarraBotones.Enabled = True
                Return
            End If


            Dim ListToShow As Object

            ListToShow = Await Model.GetLiquidationByGroupIdConsultLiquidation(StringIdGroup)
            Dim GroupLiquidation As New List(Of GroupLiquidation)

            If ListToShow IsNot Nothing Then

                Using Form1 As New FrmGroupLiquidated
                    Dim transparent As New FrmTransparent(Form1, False)
                    With Form1
                        .InfoDialogDatasource = ListToShow.list()
                        transparent.ShowDialog()
                        VarGroupId = .GroupId
                        PayrollDateLiquidated = .PayrollDateLiquidated
                        FlagVisualizar = .VisualizarFlag
                        .Dispose()
                    End With
                End Using
            End If


            BarraBotones.Enabled = True
            If FlagVisualizar = True And VarGroupId = "" Then
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(NoSeleccionoLiquidacionVisualizar, Eform.LiquidacionNomina)
                Return
            End If

            If VarGroupId = "" Then
                Return
            End If


            AsyncLoader(True)
            liquidationEmployee = New List(Of Liquidation)
            liquidationEmployee = Await Model.GetEmployeeLiquidatedAsync(PayrollDateLiquidated, VarGroupId)
            INDColGroupLiquidationView.Visible = True
            IndColVisualizarLiquidacion.Visible = False
            Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAndPrint)
            AsyncLoader(False)

        End Using

    End Sub

    ''' <summary>
    ''' Imprimir Reporte
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        'Dim reportDef As New Reporter.rptPaySlip(Me.BarraBotones.OperatingUnit)
        'reportDef.INDGrupos = GroupShow
        'reportDef.INDFechaFin = PayrollDateLiquidated
        'reportDef.INDFechaInicio = PayrollDateLiquidated
        'Me.BarraBotones.PrintReport(reportDef, 1, True, Me.Tag)
    End Sub

    Private Async Sub BarraBotones_ClickConfimarLiquidacion() Handles BarraBotones.ClickConfirmLiquidation
        Try
            If MessageIndigo.Show(String.Format(obtenerRecurso(NominaConfirmarNomina, Eform.LiquidacionNomina)), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Await ConfirmLiquidation()
            End If
        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = ex.Message.ToString()
            AsyncLoader(False)
        End Try

    End Sub

    Private Async Sub BarraBotones_RecordNavigationChangeEvent(Record As Object) Handles BarraBotones.RecordNavigationChangeEvent
        objectMessageLiquidation = (From e In liquidationEmployee Where e.EmployeeId = CType(Record, Liquidation).EmployeeId And e.GroupId = CType(Record, Liquidation).GroupId).FirstOrDefault()
        ctrTmp.PrintInfo()
        ctrTmp.Visible = True
        Await AssignValuesLiquidation(objectMessageLiquidation)
    End Sub

#End Region

#Region "Funciones"

    ''' <summary>
    ''' Ejecuto la Liquidación
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function ExecuteLiquidation() As Task

        Dim errors As New StringBuilder()

        Me.BarraBotones.StatusRecordVisible = True

        progress = New CtrProgress
        progress.SetInfoFunction(AddressOf getInfo)
        progress.PrintInfo()
        progress.Dock = DockStyle.Fill
        progress.SetTitle = "Empleados Liquidados"
        AdditionalControlPanel.SafeInvoke(Sub(x) x.Controls.Clear())
        AdditionalControlPanel.SafeInvoke(Sub(x) x.Controls.Add(progress))

        progress.Visible = False

        INDColGroupLiquidationView.Visible = False

        Dim MessageCode As String = "000"
        Try

            AsyncLoader(True)

            Using Model As New MPayrollLiquidation

                Dim list_group As List(Of Group) = INDGcGroup.DataSource
                Dim list_group_chek As List(Of Group)
                StringIdGroup = ""

                list_group_chek = list_group.FindAll(Function(x) x IsNot Nothing AndAlso x.Apply = True)

                If list_group_chek.Count = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "Seleccione el grupo o empleado que desea liquidar"
                    AsyncLoader(False)
                    Return
                End If

                For i As Integer = 0 To (list_group_chek.Count() - 1)

                    If list_group_chek.Count() = 1 Then
                        StringIdGroup = list_group_chek.Item(i).Id.ToString()
                    Else
                        StringIdGroup = StringIdGroup + list_group_chek.Item(i).Id.ToString()
                        If Not i = (list_group_chek.Count() - 1) Then
                            StringIdGroup = StringIdGroup + ","
                        End If

                    End If

                Next

                Dim CedulaEmpleado As String

                If FlagLiquidation = "Nueva" Then
                    If StringIdGroup = "" Then
                        Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(NoHayGruposSeleccionadosNomina, Eform.LiquidacionNomina)
                        Return
                    End If
                    CedulaEmpleado = INDbteIdNumber.Text
                Else
                    If objectMessageLiquidation IsNot Nothing Then
                        StringIdGroup = objectMessageLiquidation.GroupId
                        CedulaEmpleado = objectMessageLiquidation.NitEmployee
                    Else
                        StringIdGroup = StringIdGroup
                        CedulaEmpleado = INDbteIdNumber.Text
                    End If
                End If

                Dim CountEmployee = Await Model.GetEmployesPayrollLiquidation(StringIdGroup, CedulaEmpleado)

                If CountEmployee IsNot Nothing Then

                    totalProcessedItems = 0
                    totalItems = CountEmployee.Count

                    CountEliminated = 0

                    If (totalItems > 0) Then

                        Dim TmpListEmployeeMessage As New List(Of EmployeeMessage)


                        Dim indexSend = 0
                        progress.SafeInvoke(Sub(x)
                                                x.SetTitle = "Empleados Liquidados"
                                                x.PrintInfo()
                                            End Sub)

                        While CountEmployee.Count > 0

                            progress.Visible = True

                            Dim objLock As New Object()
                            'agregamos los items a la cabecera
                            ListEmployee = CountEmployee.Take(itemsSend).ToList()

                            Dim quantityDetailsToProcess = If(CountEmployee.Count < itemsSend, CountEmployee.Count, itemsSend)
                            indexSend = totalProcessedItems + 1
                            totalProcessedItems += quantityDetailsToProcess

                            Dim liquidationProcess = Await Model.CalculatePayrollLiquidationFragmentedAsync(ListEmployee, StringIdGroup, CountEliminated)

                            If liquidationProcess.StateResult Then

                                If liquidationProcess.MessageResult.Count > 0 Then
                                    TmpListEmployeeMessage.AddRange(MessageLiquidation(liquidationProcess.MessageResult))
                                End If

                                If FlagLiquidation = "Reliquidacion" Then
                                    Dim ObjLiquidationRemove = liquidationEmployee.Where(Function(x) x.EmployeeId = liquidationProcess.ObjectEmbbeded.FirstOrDefault.EmployeeId).FirstOrDefault()
                                    liquidationEmployee.Remove(ObjLiquidationRemove)
                                End If

                                liquidationEmployee.AddRange(liquidationProcess.ObjectEmbbeded.ToList())

                            Else
                                ' Validamos si el estado es falso
                                If liquidationProcess.StateResult = False Then
                                    If liquidationProcess.MessageResult.Count() > 0 Then
                                        Mensaje(EeventViewerImages.MensajeError) = liquidationProcess.MessageResult.ToString()
                                        AsyncLoader(False)
                                    End If
                                    Mensaje(EeventViewerImages.MensajeError) = liquidationProcess.Message
                                    AsyncLoader(False)
                                    Exit Function
                                End If


                                If liquidationProcess.MessageResult IsNot Nothing AndAlso liquidationProcess.MessageResult.Count > 0 Then
                                    TmpListEmployeeMessage.AddRange(liquidationProcess.MessageResult)
                                Else
                                    errors.AppendLine("Los items del " + (indexSend).ToString() + " hasta " + (totalProcessedItems).ToString() + " no se pudieron guardar porque:" + vbNewLine + liquidationProcess.Message)
                                End If
                            End If


                            'Se guardan los empleados recién liquidados con el valor base
                            Me.EmployeeLiquidated = liquidationProcess.ObjectEmbbeded.ToList()
                            If quantityDetailsToProcess > 0 Then
                                CountEmployee.RemoveRange(0, quantityDetailsToProcess)
                            End If

                            CountEliminated += 1

                            progress.PrintInfo()
                        End While

                        If liquidationEmployee IsNot Nothing AndAlso liquidationEmployee.Count > 0 Then

                            IndColVisualizarLiquidacion.Visible = True

                            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(LiquidacionNominaCorrecta, Eform.LiquidacionNomina)

                            For i As Integer = 0 To liquidationEmployee.Count() - 1

                                If FlagLiquidation = "Reliquidacion" Then
                                    liquidationEmployee = Await Model.GetDetailMessageLiquidation(StringIdGroup)
                                    Await AssignValuesLiquidation(liquidationEmployee.Where(Function(x) x.NitEmployee = CedulaEmpleado And x.GroupId = StringIdGroup).FirstOrDefault())
                                    Exit For
                                End If
                            Next

                            If TmpListEmployeeMessage IsNot Nothing And TmpListEmployeeMessage.Count > 0 Then
                                Dim frmMessage As FrmLiquidationMessage = New FrmLiquidationMessage()
                                frmMessage.StartPosition = FormStartPosition.CenterScreen
                                frmMessage.INDGcMessage.DataSource = TmpListEmployeeMessage
                                Dim frmTransparent As New FrmTransparent(frmMessage, False)
                                frmTransparent.ShowDialog()
                            End If

                        ElseIf TmpListEmployeeMessage IsNot Nothing AndAlso TmpListEmployeeMessage.Count > 0 Then
                            Dim frmMessage As FrmLiquidationMessage = New FrmLiquidationMessage()
                            frmMessage.StartPosition = FormStartPosition.CenterScreen
                            frmMessage.INDGcMessage.DataSource = TmpListEmployeeMessage
                            Dim frmTransparent As New FrmTransparent(frmMessage, False)
                            frmTransparent.ShowDialog()
                        Else
                            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(NoGeneraLiquidacion, Eform.LiquidacionNomina)
                        End If

                    End If

                    If FlagLiquidation <> "Reliquidacion" Then
                        Me.BarraBotones.StatusRecordVisible = False
                    End If
                    AdditionalControlPanel.Controls.Clear()
                    AdditionalControlPanel.Controls.Add(ctrTmp)
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "No existen empledos para liquidar"
                End If

                If errors.Length > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
                End If

            End Using
            AsyncLoader(False)
        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.MensajeError) = ex.Message
        End Try
    End Function

    ''' <summary>
    ''' Confirmar Liquidación de Nómina
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function ConfirmLiquidation() As Task
        Dim liquidationProcessSave = New List(Of Liquidation)
        liquidationProcessSave = liquidationEmployee.Where(Function(x) x.GroupId = GroupIdConfirmLiquidation).ToList()

        Using Model As New MPayrollLiquidation
            Dim CountMessage As Integer = 0
            If liquidationProcessSave.Count() > 0 Then
                CountMessage = liquidationProcessSave.Where(Function(x) x.Message.Sum(Function(y) y.Error = True)).Count()
                If CountMessage > 0 Then
                    Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ErroresGravesLiquidacion, Eform.LiquidacionNomina)
                    Return
                End If

                For Each liquidation In liquidationProcessSave
                    liquidation.OperatingUnitId = Me.BarraBotones.OperatingUnitValue
                Next

                AsyncLoader(True)
                Dim liquitadionConfirm = Await Model.SaveLiquidationAsync(liquidationProcessSave)
                AsyncLoader(False)
                If liquitadionConfirm.StateResult = True Then
                    INDColGroupLiquidationView.Visible = False
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(NominaConfirmadaCorrectamente, Eform.LiquidacionNomina)
                    Presenter = New PPayrollLiquidation(Me)
                    Await Presenter.Load_Group()
                    Await CleanControls()
                Else
                    If Not String.IsNullOrEmpty(liquitadionConfirm.Message) Then
                        Mensaje(EeventViewerImages.Advertencia) = liquitadionConfirm.Message
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(NoGeneraLiquidacion, Eform.LiquidacionNomina)
                    End If
                End If
            Else
                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(PrimeroNominaBorrador, Eform.LiquidacionNomina)
            End If
        End Using
    End Function


    ''' <summary>
    ''' Calcula el valor del control de la barra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CalculateValue()
        ctrTmp.PrintInfo()
    End Sub

    Private Function MessageLiquidation(ByVal ListMessageResult As List(Of MessageResult)) As List(Of EmployeeMessage)

        Dim ListEmployeeMessage As New List(Of EmployeeMessage)

        For Each ObjMessage As MessageResult In ListMessageResult
            Dim ObjEmployeeMessage As New EmployeeMessage

            ObjEmployeeMessage.CodeError = ObjMessage.CodeMessage

            If ObjMessage.CodeMessage = "-001: Parámetros de Nómina" Or ObjMessage.CodeMessage = "-002: Autorización de Conceptos" Or ObjMessage.CodeMessage = "-003: Grupos" Or ObjMessage.CodeMessage = "-004: Fondos de Empleados" Or ObjMessage.CodeMessage = "-006: Salarios de Empleados" Or ObjMessage.CodeMessage = "-005: Novedades" Or ObjMessage.CodeMessage = "-007: Total a Pagar" Or ObjMessage.CodeMessage = "-999: Error" Or ObjMessage.CodeMessage = "-008: Novedades Cuadros de Turno" Then
                ObjEmployeeMessage.TypeMessage = "1"
            ElseIf ObjMessage.CodeMessage = "002: Incapacidades" Or ObjMessage.CodeMessage = "004: Vacaciones" Or ObjMessage.CodeMessage = "006: Novedades Cuadros de Turno" Then
                ObjEmployeeMessage.TypeMessage = "2"
            Else
                ObjEmployeeMessage.TypeMessage = "3"
            End If

            ObjEmployeeMessage.ErrorMessage = ObjMessage.Parameters(0)

            ListEmployeeMessage.Add(ObjEmployeeMessage)
        Next

        Return ListEmployeeMessage.GroupBy(Function(x) x.ErrorMessage).Select(Function(x) x.First).ToList()
    End Function

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()

        listStates.Add(New StatusRecord With {.StatusValue = 1, .StatusName = ResourceManager.GetString("StateUnconfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = 2, .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = 3, .StatusName = "Saldo Inicial", .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})

        Me.BarraBotones.States = listStates

    End Sub

    Private Async Function CargarLiquidacionesPrevias() As Task

        Dim GroupLiquidated As Dictionary(Of String, Date)
        liquidationEmployee = New List(Of Liquidation)

        Using Model As New MPayrollLiquidation

            liquidationEmployee = Await Model.GetHeadLiquidation()
            If liquidationEmployee IsNot Nothing And liquidationEmployee.Count > 0 Then

                IndColVisualizarLiquidacion.Visible = True
                INDColGroupLiquidationView.Visible = False

            End If

        End Using
    End Function

#Region "Click"
    'Visualizar las Liquidaciones
    Private Async Sub INDRepButtonEditVisualizar_Click(sender As Object, e As EventArgs) Handles INDRepButtonEditVisualizar.Click
        Dim GroupToShow = INDGvGroup.GetFocusedRow()

        Using Model As New MPayrollLiquidation
            AsyncLoader(True)
            liquidationEmployee = Await Model.GetDetailMessageLiquidation(GroupToShow.Id)
            AsyncLoader(False)
        End Using

        If INDbteIdNumber.Text <> String.Empty Then
            If liquidationEmployee IsNot Nothing Then
                liquidationEmployee = liquidationEmployee.Where(Function(x) x.NitEmployee.Contains(INDbteIdNumber.EditValue)).ToList()
            End If
        End If

        '   StringIdGroup = GroupToShow.Id

        If liquidationEmployee IsNot Nothing AndAlso liquidationEmployee.Count() > 0 Then
            GroupShow.Add(liquidationEmployee.Item(0).Group)
            Me.BarraBotones.ColumnInfo = {New ColumnInfo With {.Caption = "Identificación -  Nombre", .FieldName = "FullNameEmployee"}}.ToList()
            Me.BarraBotones.PrepareToolbar(eAction.OnlyConfirmLiquidate)
            Me.BarraBotones.StatusRecordVisible = True
            Me.BarraBotones.FilterDataSource = liquidationEmployee
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Liquidar) = False

            FlagLiquidation = "Reliquidacion"

            CtrNavigationControl1.Visible = True

            INDlyCtrNavigation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyLblEmployeeName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyGResultLiquidation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyGResultLiquidation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            LayoutControlItem10.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

            INDlyBteIdNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyGcGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            'INDGcGroup.Enabled = False

            GroupIdConfirmLiquidation = liquidationEmployee.Item(0).GroupId

            Await AssignValuesLiquidation(liquidationEmployee.Item(0))
        Else
            Mensaje(EeventViewerImages.Advertencia) = "La cédula del Empleado digitada NO corresponde al Grupo que desea visualizar"
        End If
    End Sub

    ''' <summary>
    ''' Función para Visualizar las Liquidaciones de un Grupo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub RepositoryItemButtonEdit2_Click(sender As Object, e As EventArgs) Handles RepositoryItemButtonEdit2.Click
        AdditionalControlPanel.Controls.Add(ctrTmp)
        Dim GroupToShow = INDGvGroup.GetFocusedRow()
        Dim LiquidationShow As List(Of Liquidation)

        LiquidationShow = liquidationEmployee.FindAll(Function(x) x.GroupId = GroupToShow.Id)

        If INDbteIdNumber.Text <> String.Empty Then

            If LiquidationShow.Any(Function(x) x.NitEmployee = INDbteIdNumber.Text) = True Then
                LiquidationShow = LiquidationShow.FindAll(Function(x) x.NitEmployee = INDbteIdNumber.Text)
            Else
                Mensaje(EeventViewerImages.Advertencia) = "La cédula del Empleado digitada NO corresponde al Grupo que desea visualizar"
                Exit Sub
            End If

        End If

        StringIdGroup = GroupToShow.Id

        GroupShow.Add(LiquidationShow.Item(0).Group)

        If LiquidationShow.Count() > 0 Then
            Me.BarraBotones.ColumnInfo = {New ColumnInfo With {.Caption = "Identificación -  Nombre", .FieldName = "FullNameEmployee"}}.ToList()
            Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAndPrint)
            Me.BarraBotones.StatusRecordVisible = True
            Me.BarraBotones.FilterDataSource = LiquidationShow

            CtrNavigationControl1.Visible = True

            INDlyCtrNavigation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyLblEmployeeName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyGResultLiquidation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            LayoutControlItem10.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

            INDlyBteIdNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyGcGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            'INDGcGroup.Enabled = False

            Await AssignValuesLiquidation(LiquidationShow.Item(0))
        End If

    End Sub

    Private Sub RepositoryItemPopupContainerEdit1_Click(sender As Object, e As EventArgs) Handles RepositoryItemPopupContainerEdit1.Click
        ' Lógica para mostrar calculadora
        Dim focusedRow As LiquidationDetail = TryCast(INDGvLiquitadionDetail.GetFocusedRow(), LiquidationDetail)

        If focusedRow IsNot Nothing AndAlso focusedRow.ConceptCode = "701" Then
            LayoutControlItem1.ShowLayout
            INDBtnLiquidator.Visible = True
        Else
            LayoutControlItem1.HideLayout
            INDBtnLiquidator.Visible = False
        End If
        FormulasToShow = INDGvLiquitadionDetail.GetFocusedRow()
        AssignValues(FormulasToShow)
    End Sub

    Private Sub RepositoryItemPopupContainerEdit3_Click(sender As Object, e As EventArgs) Handles RepositoryItemPopupContainerEdit3.Click
        FormulasToShow = INDGvLiquitadionDetailParafiscales.GetFocusedRow()
        AssignValuesEmployer(FormulasToShow)
    End Sub
    ''' <summary>
    ''' Evento que muestra el Liquidador
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDBtnLiquidator_Click(sender As Object, e As EventArgs) Handles INDBtnLiquidator.Click
        'Envío datos al Liquidador para mostrar valores
        If Me.EmployeeLiquidated Is Nothing Then
            Await GetEmployeeRetentionValues()
        ElseIf Me.EmployeeLiquidated.FirstOrDefault(Function(liq) liq.EmployeeId = Me.EmployeeId) Is Nothing Then
            Await GetEmployeeRetentionValues()
        End If
        Using formulario As New FrmLiquidator(Me.ListEmployee, Me.EmployeeLiquidated, Me.EmployeeId)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent As New FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Función para obtener los valores del cálculo de la retención
    ''' </summary>
    Private Async Function GetEmployeeRetentionValues() As Task
        Dim _employee As Employee
        Using ModelE As New MEmployee(MEmployee.TAG)
            AsyncLoader(True)
            _employee = Await ModelE.GetEmployeeByIdAsync(Me.EmployeeId)
            If _employee IsNot Nothing Then
                Me.ListEmployee = {_employee}.ToList()
                If Me.ListEmployee.Count > 0 Then
                    Using ModelPL As New MPayrollLiquidation
                        Dim liquidationProcess = Await ModelPL.CalculatePayrollLiquidationFragmentedAsync(Me.ListEmployee, Me.IdGroup, 0)
                        'Se guardan los empleados recién liquidados con el valor base
                        Me.EmployeeLiquidated = liquidationProcess.ObjectEmbbeded.ToList()
                        AsyncLoader(False)
                    End Using
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "No se encontró el empleado"
                End If
            End If
        End Using
    End Function

#End Region

End Class
#End Region

