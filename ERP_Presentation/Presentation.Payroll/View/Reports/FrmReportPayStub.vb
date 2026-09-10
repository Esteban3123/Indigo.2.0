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
Public Class FrmReportPayStub

#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

#End Region

#Region "Properties"
    Public Property ProoftCloseXpoGroupStart As XPInstantFeedbackSource
    Public Property ProoftCloseXpoGroupEnd As XPInstantFeedbackSource
    Public Property ProoftCloseXpoFunctionalUnitStart As XPInstantFeedbackSource
    Public Property ProoftCloseXpoFunctionalUnitEnd As XPInstantFeedbackSource
    Public Property ProoftCloseXpoEmployee As LinqInstantFeedbackSource
    Public Property ProoftCloseXpoBranchOffice As XPInstantFeedbackSource

#End Region

#Region "Method"
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
    ''' Propiedad que se usa para cargar la Dupla de Datos de Estado
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
    'FunctionalUnit


    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleFunctionalUnit
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadXpoFunctionalUnitsPayroll()
        Using msearch As New MBusqueda
            ProoftCloseXpoFunctionalUnitStart = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.FunctionalUnit)
            INDSleFunctionalUnitStart.Datasource = ProoftCloseXpoFunctionalUnitStart
        End Using
    End Sub

    '' <summary>
    '' se ejecuta en el evento querypopup del control INDSleFunctionalUnit
    '' </summary>
    '' <param name="sender"></param>
    '' <param name="e"></param>
    '' <remarks></remarks>
    Private Sub INDSleFunctionalUnitStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleFunctionalUnitStart.QueryPopUp
        If INDSleFunctionalUnitStart.Datasource Is Nothing Then
            LoadXpoFunctionalUnitsPayroll()
        End If
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleFunctionalUnit
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadXpoFunctionalUnitsPayrollEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoFunctionalUnitEnd = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.FunctionalUnit)
            INDSleFunctionalUnitEnd.Datasource = ProoftCloseXpoFunctionalUnitEnd
        End Using
    End Sub

    '' <summary>
    '' se ejecuta en el evento querypopup del control INDSleFunctionalUnit
    '' </summary>
    '' <param name="sender"></param>
    '' <param name="e"></param>
    '' <remarks></remarks>
    Private Sub INDSleFunctionalUnitEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleFunctionalUnitEnd.QueryPopUp
        If INDSleFunctionalUnitEnd.Datasource Is Nothing Then
            LoadXpoFunctionalUnitsPayrollEnd()
        End If
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSlueGroup
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadXpoGroupsPayroll()
        Using msearch As New MBusqueda
            ProoftCloseXpoGroupStart = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.GroupsPayroll)
            INDSleGroupStart.Datasource = ProoftCloseXpoGroupStart
        End Using
    End Sub
    '' <summary>
    '' se ejecuta en el evento querypopup del control INDSlueGroup
    '' </summary>
    '' <param name="sender"></param>
    '' <param name="e"></param>
    '' <remarks></remarks>
    Private Sub INDSlueGroupStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleGroupStart.QueryPopUp
        If INDSleGroupStart.Datasource Is Nothing Then
            LoadXpoGroupsPayroll()
        End If
    End Sub
    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSlueGroupEnd
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadXpoGroupsPayrollEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoGroupEnd = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.GroupsPayroll)
            INDSleGroupEnd.Datasource = ProoftCloseXpoGroupEnd
        End Using
    End Sub
    '' <summary>
    '' se ejecuta en el evento querypopup del control INDSlueGroupEnd
    '' </summary>
    '' <param name="sender"></param>
    '' <param name="e"></param>
    '' <remarks></remarks>
    Private Sub INDSlueGroupEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleGroupEnd.QueryPopUp
        If INDSleGroupEnd.Datasource Is Nothing Then
            LoadXpoGroupsPayrollEnd()
        End If
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
    ''' se ejecuta en el evento QueryPopUp del Control INDSleEmployees
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleEmployees_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleEmployee.QueryPopUp
        If INDSleEmployee.Datasource Is Nothing Then
            LoadXpoEmployee()
        End If
    End Sub
#End Region

#Region "Validate"
    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim Validations As Boolean = True
        'Valida Fechas
        If INDDateEditDateStart.EditValue Is Nothing Or INDDateEditDateEnd.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
            Me.INDDateEditDateStart.Focus()
            Validations = False
        ElseIf Me.INDDateEditDateStart.EditValue > INDDateEditDateEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("FrmReportAuxiliary_CompareDate", "Accounting"))
            Me.INDDateEditDateStart.Focus()
            Validations = False
        End If

        'Valida Grupo
        If INDSleGroupStart.EditValue IsNot Nothing And INDSleGroupEnd.EditValue Is Nothing Or INDSleGroupEnd.EditValue IsNot Nothing And INDSleGroupStart.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDlblGroup.Text)
            Me.INDSleGroupStart.Focus()
            Validations = False
        ElseIf INDSleGroupEnd.EditValue < INDSleGroupStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDlblGroup.Text)
            Me.INDSleGroupStart.Focus()
            Validations = False
        End If

        'Valida Unidad Funcional
        If INDSleFunctionalUnitStart.EditValue IsNot Nothing And INDSleFunctionalUnitEnd.EditValue Is Nothing Or INDSleFunctionalUnitEnd.EditValue IsNot Nothing And INDSleFunctionalUnitStart.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDlblGroup2.Text)
            Me.INDSleFunctionalUnitStart.Focus()
            Validations = False
        ElseIf INDSleFunctionalUnitEnd.EditValue < INDSleFunctionalUnitStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDlblGroup2.Text)
            Me.INDSleFunctionalUnitStart.Focus()
            Validations = False
        End If

        'If INDSleEmployees.EditValue Is Nothing Then
        '    Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDLciEmployees.Text)
        '    INDSleEmployees.Focus()
        '    ValidateControlsCuadroTurno = False
        'End If
        Return Validations
    End Function
#End Region

#Region "Events"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        If _pucModel IsNot Nothing Then
            _pucModel.Dispose()
            _pucModel = Nothing
        End If
        ProoftCloseXpoEmployee = Nothing
        ProoftCloseXpoGroupEnd = Nothing
        ProoftCloseXpoGroupStart = Nothing
    End Sub

    Private Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If ValidateControlsReports() = True Then
            AsyncLoader(True)
            Dim reporte As New rptPayStub
            reporte.ParametrosReporte = New Object() {INDDateEditDateStart.EditValue,
                                                      INDDateEditDateEnd.EditValue,
                                                      INDSleGroupStart.EditValue,
                                                      INDSleGroupEnd.EditValue,
                                                      INDSleEmployee.EditValue,
                                                      INDGleState.EditValue,
                                                      INDGleState.Text,
                                                      BarraBotones.OperatingUnit,
                                                      INDSleFunctionalUnitStart.EditValue,
                                                      INDSleFunctionalUnitEnd.EditValue,
                                                      INDsleSucursalStart.EditValue,
                                                      INDsleSucursalEnd.EditValue}

            INDDvReport.DocumentSource = reporte
            reporte.CargarDataSource()
            If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                reporte.CreateDocument()
            End If
            AsyncLoader(False)
            If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                Me.INDLcBase.Visible = False
                Me.INDCncReport.Visible = False
                Me.INDPcReport.Visible = True
                INDDvReport.Show()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                Me.INDDateEditDateStart.Focus()
            End If
        End If
    End Sub

    Private Sub INDCnReport_ClickBack() Handles INDCnReport.ClickBack
        Me.INDPcReport.Visible = False
        Me.INDLcBase.Visible = True
        Me.INDCncReport.Visible = True
        Me.INDDateEditDateStart.Focus()
    End Sub

    Private Sub FrmReportsConventionsEmployees_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Me.INDGleState.Properties.DataSource = FillingStatus
        Me.INDGleState.EditValue = "T"
    End Sub

    Private Sub INDSbGenerateExcell_Click(sender As Object, e As EventArgs) Handles INDSbGenerateExcell.Click
        Try

            Dim fechaInicial = INDDateEditDateStart.EditValue
            Dim fechaFinal = INDDateEditDateEnd.EditValue

            Dim filtroConsulta As String = "ConceptType <> 3 AND PayrollDateLiquidated >= '" & Format(fechaInicial, "yyyy-MM-dd") & "' And PayrollDateLiquidated <= '" & Format(fechaFinal, "yyyy-MM-dd") & "'"
            'filtro por Grupo
            If INDSleGroupStart.EditValue IsNot Nothing And INDSleGroupEnd.EditValue IsNot Nothing Then
                filtroConsulta &= " AND GroupId >= '" & INDSleGroupStart.EditValue & "' AND GroupId <= '" & INDSleGroupEnd.EditValue & "'"
            End If
            'filtro por Unidad Funcional
            If INDSleFunctionalUnitStart.EditValue IsNot Nothing And INDSleFunctionalUnitEnd.EditValue IsNot Nothing Then
                filtroConsulta &= " AND PayrollId.FunctionalUnitId >= '" & INDSleFunctionalUnitStart.EditValue & "' AND FunctionalUnitId <= '" & INDSleFunctionalUnitEnd.EditValue & "'"
            End If
            'filtro empleado
            If INDSleEmployee.EditValue IsNot Nothing Then
                filtroConsulta &= "And EmployeeId = " & INDSleEmployee.EditValue
            End If
            'filtro por estado
            If INDGleState.EditValue <> "T" Then
                filtroConsulta &= " AND RegisterStatus = '" & INDGleState.EditValue & "'"
            End If

            AsyncLoader(True)
            Me.INDGcExportExcell.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).TreasuryService.GetCollection(Of PayrollVPayStubReportXpo)(Nothing, filtroConsulta)
            AsyncLoader(False)
            If Me.INDGcExportExcell.DataSource IsNot Nothing AndAlso Me.INDGcExportExcell.DataSource.Count > 0 Then
                generateExcel()
            Else
                Mensaje(EeventViewerImages.Advertencia) = "No se encontraron datos con esos parámetros de búsqueda"
            End If

        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.MensajeError) = ex.Message
        End Try
    End Sub

    Private Sub generateExcel()
        Dim _gridView = Me.INDGcExportExcell
        If _gridView IsNot Nothing Then
            Dim fileName As String = System.IO.Path.GetTempFileName() & ".xlsx"
            Dim param As New DevExpress.XtraPrinting.XlsxExportOptions(DevExpress.XtraPrinting.TextExportMode.Value, True, False)
            _gridView.ExportToXlsx(fileName, param)
            If System.IO.File.Exists(fileName) Then
                System.Diagnostics.Process.Start(fileName)
            End If
        End If
        Me.INDGcExportExcell.DataSource = Nothing
        Me.INDGcExportExcell.RefreshDataSource()
    End Sub

    Private Sub INDsleSucursalStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleSucursalStart.QueryPopUp
        Using msearch As New MBusqueda
            ProoftCloseXpoBranchOffice = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListBranchOffice)
            INDsleSucursalStart.Properties.DataSource = ProoftCloseXpoBranchOffice
        End Using
    End Sub

    Private Sub INDsleSucursalEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleSucursalEnd.QueryPopUp
        Using msearch As New MBusqueda
            ProoftCloseXpoBranchOffice = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListBranchOffice)
            INDsleSucursalEnd.Properties.DataSource = ProoftCloseXpoBranchOffice
        End Using
    End Sub
#End Region
End Class