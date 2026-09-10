#Region "Imports"
Imports DevExpress.Data.Filtering
Imports DevExpress.Data.Linq
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Common.MVP
Imports Presentation.Controls
Imports Presentation.Controls.MVP
Imports Presentation.Reporter


#End Region

Public Class FrmReportKardex
#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

#End Region

#Region "Properties"
    Public Property ProoftCloseXpoGroup As XPInstantFeedbackSource
    Public Property ProoftCloseXpoUnit As XPInstantFeedbackSource
    Public Property ProoftCloseXpoEmployee As LinqInstantFeedbackSource

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

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

#End Region

    '''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim Validations As Boolean = True
        If INDSleGroupStart.EditValue Is Nothing And INDSleGroupEnd.EditValue IsNot Nothing Or INDSleGroupEnd.EditValue Is Nothing And INDSleGroupStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblGroup.Text)
            Me.INDSleGroupStart.Focus()
            Validations = False
        ElseIf INDSleGroupEnd.EditValue < INDSleGroupStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblGroup.Text)
            Me.INDSleGroupStart.Focus()
            Validations = False
        End If

        If INDSleEmployee.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDLciEmployee.Text)
            Me.INDSleEmployee.Focus()
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

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _pucModel.Dispose()
        _pucModel = Nothing
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento click del control INDSbGenerateReport
    ''' </summary>
    ''' ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports Then
            AsyncLoader(True)
            Dim reporte As New rptPayrollKardex()

            reporte.ParametrosReporte = {INDSleEmployee.EditValue, INDDnYearMonth.GetYear, INDDnYearMonth.GetMonth, INDGleStatus.EditValue,
                                         INDSleGroupStart.EditValue, INDSleGroupEnd.EditValue, INDSleUnitStart.EditValue, INDSleUnitEnd.EditValue, 1, 2}

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
                Me.INDDnYearMonth.Focus()
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
        Me.INDDnYearMonth.Focus()
    End Sub

    ''' <summary>
    ''' Se ejecuta en el evento Shown del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportKardex_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Cargar GridLookUpEdit
        Me.INDGleStatus.Properties.DataSource = FillingStatus
        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleStatus.EditValue = "T"
        INDDnYearMonth.HowShowControl = CtrDateNavigator.EHowShowControl.OnlyYear

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

    Private Sub FrmReportKardex_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ToolBar.Visible = False
        Me._pucModel = New MCommon(Me.Tag)
        Me.INDSleUnitStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetFunctionalUnitByCode
        Me.INDSleUnitEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetFunctionalUnitByCode
        Me.INDSleEmployee.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetEmployeeAsync


    End Sub

    Private Sub INDSbGenerateExcell_Click(sender As Object, e As EventArgs) Handles INDSbGenerateExcell.Click
        Try
            Dim year As Integer = INDDnYearMonth.GetYear
            Dim fechaInicial As New Date(year, 1, 1)
            Dim fechaFinal As New Date(year, 12, 31)
            Dim filtros As New List(Of CriteriaOperator)
            filtros.Add(CriteriaOperator.Parse("Mes >= ? AND Mes < ?", fechaInicial, fechaFinal))
            If INDSleEmployee.EditValue IsNot Nothing AndAlso INDSleEmployee.EditValue IsNot DBNull.Value AndAlso Not String.IsNullOrWhiteSpace(INDSleEmployee.EditValue.ToString()) Then
                Dim employeeId As Integer = Convert.ToInt32(INDSleEmployee.EditValue)
                filtros.Add(CriteriaOperator.Parse("IdEmployee = ?", employeeId))
            End If

            Dim criteriaFinal As CriteriaOperator = If(filtros.Count = 1, filtros(0), New GroupOperator(GroupOperatorType.And, filtros.ToArray()))
            Dim filtro As String = criteriaFinal.ToString()
            AsyncLoader(True)
            Me.INDGcExportExcell.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of PayrollViewXpoReportKardex)(Nothing, filtro)
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
End Class