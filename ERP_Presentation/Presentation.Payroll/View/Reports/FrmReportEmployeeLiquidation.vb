#Region "Imports"
Imports Presentation.Reporter
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports Presentation.Common.MVP
Imports Infrastructure.Data.Xpo

#End Region

Public Class FrmReportEmployeeLiquidation
#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

#End Region

#Region "Properties"
    Public Property ProoftCloseXpoGroup As XPInstantFeedbackSource
    Public Property ProoftCloseXpoUnit As XPInstantFeedbackSource
    Public Property ProoftCloseXpoCostCenter As XPInstantFeedbackSource
    Public Property ProoftCloseXpoConcept As XPInstantFeedbackSource
    Public Property ProoftCloseXpoEmployee As LinqInstantFeedbackSource
    Public Property ProoftCloseXpoBranchOffice As XPInstantFeedbackSource


    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Propiedad que se usa para cargar la Dupla de Datos de Tipos de Reporte
    ''' </summary>
    ''' <remarks></remarks>
    Private _LoadTypeLiquidation As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property LoadTypeLiquidation As List(Of Tuple(Of Integer, String))
        Get
            If _LoadTypeLiquidation Is Nothing Then
                _LoadTypeLiquidation = New List(Of Tuple(Of Integer, String))
                _LoadTypeLiquidation.Add(New Tuple(Of Integer, String)(1, "Pago Por Empleado"))
                _LoadTypeLiquidation.Add(New Tuple(Of Integer, String)(2, "Pago Por Centro de Costo"))
                _LoadTypeLiquidation.Add(New Tuple(Of Integer, String)(3, "Pago Por Concepto"))
            End If
            Return _LoadTypeLiquidation
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que se usa para cargar la Dupla de Datos de Estado de Comprobante
    ''' </summary>
    ''' <remarks></remarks>
    Private _FillingStatus As List(Of Tuple(Of String, String))
    Private ReadOnly Property FillingStatus As List(Of Tuple(Of String, String))
        Get
            If _FillingStatus Is Nothing Then
                _FillingStatus = New List(Of Tuple(Of String, String))
                _FillingStatus.Add(New Tuple(Of String, String)("C", "Confirmados"))
                _FillingStatus.Add(New Tuple(Of String, String)("", "Sin Confirmar"))
                _FillingStatus.Add(New Tuple(Of String, String)("S", "Saldo Inicial"))
                _FillingStatus.Add(New Tuple(Of String, String)("T", "Todos"))
            End If
            Return _FillingStatus
        End Get
    End Property

    Private criteria As String = Nothing

    ''' <summary>
    ''' Tipo de reporte
    ''' </summary>
    ''' <remarks></remarks>
    Private _FillingProcess As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingProcess As List(Of Tuple(Of Integer, String))
        Get
            If _FillingProcess Is Nothing Then
                _FillingProcess = New List(Of Tuple(Of Integer, String))
                _FillingProcess.Add(New Tuple(Of Integer, String)(1, "Nomina"))
                _FillingProcess.Add(New Tuple(Of Integer, String)(2, "Retroactivo"))
                _FillingProcess.Add(New Tuple(Of Integer, String)(3, "Primas"))
            End If
            Return _FillingProcess
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que se usa para cargar la Tupla de Datos de Periodo
    ''' </summary>
    ''' <remarks></remarks>
    Private _FillingPeriod As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingPeriod As List(Of Tuple(Of Integer, String))
        Get
            If _FillingPeriod Is Nothing Then
                _FillingPeriod = New List(Of Tuple(Of Integer, String))
                _FillingPeriod.Add(New Tuple(Of Integer, String)(1, "1"))
                _FillingPeriod.Add(New Tuple(Of Integer, String)(2, "2"))
                _FillingPeriod.Add(New Tuple(Of Integer, String)(3, "Todos"))
            End If
            Return _FillingPeriod
        End Get
    End Property
#End Region

    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim Validations As Boolean = True

        If INDDateStart.EditValue Is Nothing Or INDDateEnd.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
            Me.INDDateStart.Focus()
            Validations = False
        ElseIf Me.INDDateStart.EditValue > INDDateEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("FrmReportAuxiliary_CompareDate", "Accounting"))
            Me.INDDateStart.Focus()
            Validations = False
        End If

        'Validar Filtros
        'Valida Grupo
        If INDSleGroupStart.EditValue Is Nothing And INDSleGroupEnd.EditValue IsNot Nothing Or INDSleGroupEnd.EditValue Is Nothing And INDSleGroupStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblGroup.Text)
            Me.INDSleGroupStart.Focus()
            Validations = False
        ElseIf INDSleGroupEnd.EditValue < INDSleGroupStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblGroup.Text)
            Me.INDSleGroupStart.Focus()
            Validations = False
        End If

        'Valida Unidad funcional
        If INDSleUnitStart.EditValue Is Nothing And INDSleUnitEnd.EditValue IsNot Nothing Or INDSleUnitEnd.EditValue Is Nothing And INDSleUnitStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblUnit.Text)
            Me.INDSleUnitStart.Focus()
            Validations = False
        ElseIf INDSleUnitEnd.EditValue < INDSleUnitStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblUnit.Text)
            Me.INDSleUnitStart.Focus()
            Validations = False
        End If

        'Valida CostCenter
        If INDSleCostCenterStart.EditValue Is Nothing And INDSleCostCenterEnd.EditValue IsNot Nothing Or INDSleCostCenterEnd.EditValue Is Nothing And INDSleCostCenterStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblCostCenter.Text)
            Me.INDSleCostCenterStart.Focus()
            Validations = False
        ElseIf INDSleCostCenterEnd.EditValue < INDSleCostCenterStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblCostCenter.Text)
            Me.INDSleCostCenterStart.Focus()
            Validations = False
        End If

        'Valida Concepto
        If INDSleConceptStart.EditValue Is Nothing And INDSleConceptEnd.EditValue IsNot Nothing Or INDSleConceptEnd.EditValue Is Nothing And INDSleConceptStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblConcept.Text)
            Me.INDSleConceptStart.Focus()
            Validations = False
        ElseIf INDSleConceptEnd.EditValue < INDSleConceptStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblConcept.Text)
            Me.INDSleConceptStart.Focus()
            Validations = False
        End If
        Return Validations
    End Function

    ''' <summary>
    ''' propiedad para registar el mensaje en el visor
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String 'Implements IcrudBase.Mensaje
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

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleGroupStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoGroupStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoGroup = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.GroupsPayroll)
            INDSleGroupStart.Datasource = ProoftCloseXpoGroup
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleGroupEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoGroupEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoGroup = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.GroupsPayroll)
            INDSleGroupEnd.Datasource = ProoftCloseXpoGroup
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleCostCenterStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoCostCenterStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoCostCenter = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCostcenterReport)
            INDSleCostCenterStart.Datasource = ProoftCloseXpoCostCenter
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleCostCenterEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoCostCenterEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoCostCenter = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCostcenterReport)
            INDSleCostCenterEnd.Datasource = ProoftCloseXpoCostCenter
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleConceptStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoConceptStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoConcept = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListConceptsPayrollAll)
            INDSleConceptStart.Datasource = ProoftCloseXpoConcept
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleConceptEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoConceptEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoConcept = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListConceptsPayrollAll)
            INDSleConceptEnd.Datasource = ProoftCloseXpoConcept
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleUnitStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoUnitStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoUnit = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.FunctionalUnit)
            INDSleUnitStart.Datasource = ProoftCloseXpoUnit
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleUnitEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoUnitEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoUnit = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.FunctionalUnit)
            INDSleUnitEnd.Datasource = ProoftCloseXpoUnit
        End Using
    End Sub

    '' <summary>
    ''' metodo para Cargar el data source Del Control INDSleEmployee
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoEmployee()
        Using msearch As New MBusqueda
            ProoftCloseXpoEmployee = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAllEmployee)
            INDSleEmployee.Datasource = ProoftCloseXpoEmployee
        End Using
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento click del control INDSbGenerateReport
    ''' </summary>
    ''' ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>   
    Private Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports Then
            If INDGleProcess.EditValue = 1 Then 'Nomina
                If INDGleTypeLiquidation.EditValue = 1 Then
                    AsyncLoader(True)
                    Dim reporte As New rptTotalPaidEmployee()

                    reporte.ParametrosReporte = {
                        INDDateStart.EditValue,
                        INDDateEnd.EditValue,
                        INDGleStatus.EditValue,
                        INDSleEmployee.EditValue,
                        INDSleGroupStart.EditValue,
                        INDSleGroupEnd.EditValue,
                        INDSleUnitStart.EditValue,
                        INDSleUnitEnd.EditValue,
                        INDsleSucursalStart.EditValue,
                        INDsleSucursalEnd.EditValue}

                    INDDvViewReport.DocumentSource = reporte

                    reporte.CargarDataSource()
                    If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                        reporte.CreateDocument(True)
                    End If
                    AsyncLoader(False)
                    If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                        Me.INDLcBase.Visible = False
                        Me.INDCncNavigation.Visible = False
                        Me.INDPcViewReport.Visible = True
                        INDDvViewReport.Show()
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                        Me.INDDateStart.Focus()
                    End If
                ElseIf INDGleTypeLiquidation.EditValue = 2 Then
                    AsyncLoader(True)
                    Dim reporte As New rptPayrollLiquidation()

                    reporte.ParametrosReporte = {
                        INDDateStart.EditValue,
                        INDDateEnd.EditValue,
                        INDGleStatus.EditValue,
                        INDSleEmployee.EditValue,
                        INDSleGroupStart.EditValue,
                        INDSleGroupEnd.EditValue,
                        INDSleUnitStart.EditValue,
                        INDSleUnitEnd.EditValue,
                        INDSleCostCenterStart.EditValue,
                        INDSleCostCenterEnd.EditValue,
                        INDsleSucursalStart.EditValue,
                        INDsleSucursalEnd.EditValue}

                    INDDvViewReport.DocumentSource = reporte

                    reporte.CargarDataSource()
                    If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                        reporte.CreateDocument(True)
                    End If
                    AsyncLoader(False)
                    If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                        Me.INDLcBase.Visible = False
                        Me.INDCncNavigation.Visible = False
                        Me.INDPcViewReport.Visible = True
                        INDDvViewReport.Show()
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                        Me.INDDateStart.Focus()
                    End If
                ElseIf INDGleTypeLiquidation.EditValue = 3 Then
                    AsyncLoader(True)
                    Dim reporte As New rptPayrollLiquidationDetail()

                    reporte.ParametrosReporte = {
                        INDDateStart.EditValue,
                        INDDateEnd.EditValue,
                        INDGleStatus.EditValue,
                        INDSleEmployee.EditValue,
                        INDSleGroupStart.EditValue,
                        INDSleGroupEnd.EditValue,
                        INDSleUnitStart.EditValue,
                        INDSleUnitEnd.EditValue,
                        INDSleConceptStart.EditValue,
                        INDSleConceptEnd.EditValue,
                        INDsleSucursalStart.EditValue,
                        INDsleSucursalEnd.EditValue}

                    INDDvViewReport.DocumentSource = reporte

                    reporte.CargarDataSource()
                    If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                        reporte.CreateDocument(True)
                    End If
                    AsyncLoader(False)
                    If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                        Me.INDLcBase.Visible = False
                        Me.INDCncNavigation.Visible = False
                        Me.INDPcViewReport.Visible = True
                        INDDvViewReport.Show()
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                        Me.INDDateStart.Focus()
                    End If
                End If
            ElseIf INDGleProcess.EditValue = 2 Then 'Retroactividad
                If INDGleTypeLiquidation.EditValue = 1 Then
                    AsyncLoader(True)
                    Dim reporte As New rptTotalPaidEmployeeRetroactive()

                    reporte.ParametrosReporte = {
                        INDDateStart.EditValue,
                        INDDateEnd.EditValue,
                        INDGleStatus.EditValue,
                        INDSleEmployee.EditValue,
                        INDSleGroupStart.EditValue,
                        INDSleGroupEnd.EditValue,
                        INDSleUnitStart.EditValue,
                        INDSleUnitEnd.EditValue,
                        INDsleSucursalStart.EditValue,
                        INDsleSucursalEnd.EditValue}

                    INDDvViewReport.DocumentSource = reporte

                    reporte.CargarDataSource()
                    If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                        reporte.CreateDocument(True)
                    End If
                    AsyncLoader(False)
                    If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                        Me.INDLcBase.Visible = False
                        Me.INDCncNavigation.Visible = False
                        Me.INDPcViewReport.Visible = True
                        INDDvViewReport.Show()
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                        Me.INDDateStart.Focus()
                    End If
                ElseIf INDGleTypeLiquidation.EditValue = 2 Then
                    AsyncLoader(True)
                    Dim reporte As New rptPayrollLiquidationRetroactive()

                    reporte.ParametrosReporte = {
                        INDDateStart.EditValue,
                        INDDateEnd.EditValue,
                        INDGleStatus.EditValue,
                        INDSleEmployee.EditValue,
                        INDSleGroupStart.EditValue,
                        INDSleGroupEnd.EditValue,
                        INDSleUnitStart.EditValue,
                        INDSleUnitEnd.EditValue,
                        INDSleCostCenterStart.EditValue,
                        INDSleCostCenterEnd.EditValue,
                        INDsleSucursalStart.EditValue,
                        INDsleSucursalEnd.EditValue}

                    INDDvViewReport.DocumentSource = reporte

                    reporte.CargarDataSource()
                    If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                        reporte.CreateDocument(True)
                    End If
                    AsyncLoader(False)
                    If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                        Me.INDLcBase.Visible = False
                        Me.INDCncNavigation.Visible = False
                        Me.INDPcViewReport.Visible = True
                        INDDvViewReport.Show()
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                        Me.INDDateStart.Focus()
                    End If
                ElseIf INDGleTypeLiquidation.EditValue = 3 Then
                    AsyncLoader(True)
                    Dim reporte As New rptPayrollLiquidationDetailRetroactive()

                    reporte.ParametrosReporte = {
                        INDDateStart.EditValue,
                        INDDateEnd.EditValue,
                        INDGleStatus.EditValue,
                        INDSleEmployee.EditValue,
                        INDSleGroupStart.EditValue,
                        INDSleGroupEnd.EditValue,
                        INDSleUnitStart.EditValue,
                        INDSleUnitEnd.EditValue,
                        INDSleConceptStart.EditValue,
                        INDSleConceptEnd.EditValue,
                        INDsleSucursalStart.EditValue,
                        INDsleSucursalEnd.EditValue}

                    INDDvViewReport.DocumentSource = reporte

                    reporte.CargarDataSource()
                    If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                        reporte.CreateDocument(True)
                    End If
                    AsyncLoader(False)
                    If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                        Me.INDLcBase.Visible = False
                        Me.INDCncNavigation.Visible = False
                        Me.INDPcViewReport.Visible = True
                        INDDvViewReport.Show()
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                        Me.INDDateStart.Focus()
                    End If
                End If
            ElseIf INDGleProcess.EditValue = 3 Then 'Primas
                If INDGleTypeLiquidation.EditValue = 1 Then 'Por Empleado
                    AsyncLoader(True)
                    Dim registerStatus As Object = INDGleStatus.EditValue
                    ' Mapeo de valores: "C" - 2, "" - 1
                    Select Case CStr(registerStatus)
                        Case "C" ' Confirmado
                            registerStatus = 2
                        Case "" ' Sin Confirmar
                            registerStatus = 1
                        Case "T" ' Todos
                            registerStatus = Nothing
                        Case Else
                            registerStatus = Nothing
                    End Select
                    Dim reporte As New rptTotalPaidEmployeeIncentive()

                    reporte.ParametrosReporte = {
                        INDDateStart.EditValue,
                        INDDateEnd.EditValue,
                        registerStatus,
                        INDSleEmployee.EditValue,
                        INDSleGroupStart.EditValue,
                        INDSleGroupEnd.EditValue,
                        INDSleUnitStart.EditValue,
                        INDSleUnitEnd.EditValue,
                        INDGlePeriod.EditValue,
                        INDsleSucursalStart.EditValue,
                        INDsleSucursalEnd.EditValue}

                    INDDvViewReport.DocumentSource = reporte

                    reporte.CargarDataSource()
                    If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                        reporte.CreateDocument(True)
                    End If
                    AsyncLoader(False)
                    If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                        Me.INDLcBase.Visible = False
                        Me.INDCncNavigation.Visible = False
                        Me.INDPcViewReport.Visible = True
                        INDDvViewReport.Show()
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                        Me.INDDateStart.Focus()
                    End If
                ElseIf INDGleTypeLiquidation.EditValue = 2 Then
                    AsyncLoader(True)
                    Dim reporte As New rptPayrollIncentivePaymentCost()

                    reporte.ParametrosReporte = {
                        INDDateStart.EditValue,
                        INDDateEnd.EditValue,
                        INDGleStatus.EditValue,
                        INDSleEmployee.EditValue,
                        INDSleGroupStart.EditValue,
                        INDSleGroupEnd.EditValue,
                        INDSleUnitStart.EditValue,
                        INDSleUnitEnd.EditValue,
                        INDSleCostCenterStart.EditValue,
                        INDSleCostCenterEnd.EditValue,
                        INDGlePeriod.EditValue,
                        INDsleSucursalStart.EditValue,
                        INDsleSucursalEnd.EditValue}

                    INDDvViewReport.DocumentSource = reporte

                    reporte.CargarDataSource()
                    If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                        reporte.CreateDocument(True)
                    End If
                    AsyncLoader(False)
                    If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                        Me.INDLcBase.Visible = False
                        Me.INDCncNavigation.Visible = False
                        Me.INDPcViewReport.Visible = True
                        INDDvViewReport.Show()
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                        Me.INDDateStart.Focus()
                    End If
                ElseIf INDGleTypeLiquidation.EditValue = 3 Then
                    AsyncLoader(True)
                    Dim reporte As New rptPayrollLiquidationDetailIncentive()

                    reporte.ParametrosReporte = {
                        INDDateStart.EditValue,
                        INDDateEnd.EditValue,
                        INDGleStatus.EditValue,
                        INDSleEmployee.EditValue,
                        INDSleGroupStart.EditValue,
                        INDSleGroupEnd.EditValue,
                        INDSleUnitStart.EditValue,
                        INDSleUnitEnd.EditValue,
                        INDSleConceptStart.EditValue,
                        INDSleConceptEnd.EditValue,
                        INDGlePeriod.EditValue,
                        INDsleSucursalStart.EditValue,
                        INDsleSucursalEnd.EditValue}

                    INDDvViewReport.DocumentSource = reporte

                    reporte.CargarDataSource()
                    If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                        reporte.CreateDocument(True)
                    End If
                    AsyncLoader(False)
                    If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                        Me.INDLcBase.Visible = False
                        Me.INDCncNavigation.Visible = False
                        Me.INDPcViewReport.Visible = True
                        INDDvViewReport.Show()
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                        Me.INDDateStart.Focus()
                    End If
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento ClickBack del control INDCnBack
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnBack_ClickBack() Handles INDCnBack.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncNavigation.Visible = True
        Me.INDPcViewReport.Visible = False
        Me.INDDateStart.Focus()
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _pucModel.Dispose()
        _pucModel = Nothing
        ProoftCloseXpoConcept = Nothing
        ProoftCloseXpoCostCenter = Nothing
        ProoftCloseXpoEmployee = Nothing
        ProoftCloseXpoGroup = Nothing
        ProoftCloseXpoUnit = Nothing
    End Sub

    ''' <summary>
    ''' Se ejecuta en el evento Shown del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportEmployeeLiquidation_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Cargar GridLookUpEdit
        Me.INDGleTypeLiquidation.Properties.DataSource = LoadTypeLiquidation
        Me.INDGleStatus.Properties.DataSource = FillingStatus
        Me.INDGleProcess.Properties.DataSource = FillingProcess
        Me.INDGlePeriod.Properties.DataSource = FillingPeriod
        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleTypeLiquidation.EditValue = 1
        Me.INDGleStatus.EditValue = "T"
        Me.INDGleProcess.EditValue = 1
        Me.INDGlePeriod.EditValue = 3
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleGroupStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleGroupStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleGroupStart.QueryPopUp
        If INDSleGroupStart.Datasource Is Nothing Then
            LoadXpoGroupStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleGroupEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleGroupEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleGroupEnd.QueryPopUp
        If INDSleGroupEnd.Datasource Is Nothing Then
            LoadXpoGroupEnd()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleUnitStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleUnitStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleUnitStart.QueryPopUp
        If INDSleUnitStart.Datasource Is Nothing Then
            LoadXpoUnitStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleUnitEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleUnitEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleUnitEnd.QueryPopUp
        If INDSleUnitEnd.Datasource Is Nothing Then
            LoadXpoUnitEnd()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleCostCenterStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleCostCentertStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleCostCenterStart.QueryPopUp
        If INDSleCostCenterStart.Datasource Is Nothing Then
            LoadXpoCostCenterStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleCostCenterEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleCostCenterEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleCostCenterEnd.QueryPopUp
        If INDSleCostCenterEnd.Datasource Is Nothing Then
            LoadXpoCostCenterEnd()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleGroupStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleConceptStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleConceptStart.QueryPopUp
        If INDSleConceptStart.Datasource Is Nothing Then
            LoadXpoConceptStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleGroupEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleConceptEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleConceptEnd.QueryPopUp
        If INDSleConceptEnd.Datasource Is Nothing Then
            LoadXpoConceptEnd()
        End If
    End Sub


    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleEmployee
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleEmployee_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleEmployee.QueryPopUp
        If INDSleEmployee.Datasource Is Nothing Then
            LoadXpoEmployee()
        End If
    End Sub

    ''' <summary>
    ''' al cambiar el tipo de documento se muestra el filtro del documento seleccionado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGleTypeLiquidation_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleTypeLiquidation.EditValueChanged
        If INDGleTypeLiquidation.EditValue = 1 Then
            INDLciLblConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciConceptStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciConceptEnd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciLblCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciCostCenterStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciCostCenterEnd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDSleConceptStart.EditValue = Nothing
            INDSleConceptEnd.EditValue = Nothing
            INDSleCostCenterStart.EditValue = Nothing
            INDSleCostCenterEnd.EditValue = Nothing
        ElseIf INDGleTypeLiquidation.EditValue = 2 Then
            INDLciLblConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciConceptStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciConceptEnd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciLblCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciCostCenterStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciCostCenterEnd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDSleConceptStart.EditValue = Nothing
            INDSleConceptEnd.EditValue = Nothing
        ElseIf INDGleTypeLiquidation.EditValue = 3 Then
            INDLciLblConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciConceptStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciConceptEnd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciLblCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciCostCenterStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciCostCenterEnd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDSleCostCenterStart.EditValue = Nothing
            INDSleCostCenterEnd.EditValue = Nothing
        End If
    End Sub

    Private Sub FrmReportEmployeeLiquidation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._pucModel = New MCommon(Me.Tag)
        Me.INDSleUnitStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetFunctionalUnitByCode
        Me.INDSleUnitEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetFunctionalUnitByCode
    End Sub

    ''' <summary>
    ''' Al cambio del proceso mostrar el periodo u ocultarlo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGleProcess_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleProcess.EditValueChanged
        If INDGleProcess.EditValue = 1 Then
            INDLciStatus.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciPeriod.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Me.INDGleStatus.EditValue = "T"
            Me.INDGlePeriod.EditValue = Nothing
        ElseIf INDGleProcess.EditValue = 2 Then
            INDLciStatus.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciPeriod.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Me.INDGleStatus.EditValue = "T"
            Me.INDGlePeriod.EditValue = Nothing
        ElseIf INDGleProcess.EditValue = 3 Then
            INDLciStatus.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciPeriod.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Me.INDGleStatus.EditValue = "T"
            Me.INDGlePeriod.EditValue = 3
        End If
    End Sub

    Private Sub INDsleSucursalEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleSucursalEnd.QueryPopUp
        Using msearch As New MBusqueda
            ProoftCloseXpoBranchOffice = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListBranchOffice)
            INDsleSucursalEnd.Properties.DataSource = ProoftCloseXpoBranchOffice
        End Using
    End Sub

    Private Sub INDsleSucursalStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleSucursalStart.QueryPopUp
        Using msearch As New MBusqueda
            ProoftCloseXpoBranchOffice = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListBranchOffice)
            INDsleSucursalStart.Properties.DataSource = ProoftCloseXpoBranchOffice
        End Using
    End Sub
End Class