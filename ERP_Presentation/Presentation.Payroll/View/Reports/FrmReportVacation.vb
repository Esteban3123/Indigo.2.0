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

Public Class FrmReportVacation


#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon
    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Property ProoftCloseXpoGroup As XPInstantFeedbackSource

    Public Property ProoftCloseXpoBranchOffice As XPInstantFeedbackSource

#End Region

    ''' <summary>
    ''' Propiedad que se usa para cargar la Dupla de Datos de Estado de Comprobante
    ''' </summary>
    ''' <remarks></remarks>
    Private _VacationState As List(Of Tuple(Of String, String))
    Private ReadOnly Property VacationState As List(Of Tuple(Of String, String))
        Get
            If _VacationState Is Nothing Then
                _VacationState = New List(Of Tuple(Of String, String))
                _VacationState.Add(New Tuple(Of String, String)("1", "Esperando Pago"))
                _VacationState.Add(New Tuple(Of String, String)("2", "Pagadas"))
                _VacationState.Add(New Tuple(Of String, String)("3", "Aplazadas"))
                _VacationState.Add(New Tuple(Of String, String)("T", "Todos"))
            End If
            Return _VacationState
        End Get
    End Property

    '' <summary>
    ''' metodo para Cargar el data source Del Control INDSleEmployee
    ''' <remarks></remarks>
    Private Sub LoadXpoEmployee()
        Using msearch As New MBusqueda
            ProoftCloseXpoEmployee = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAllEmployee)
            INDSleEmployee.Datasource = ProoftCloseXpoEmployee
        End Using
    End Sub

    ''' <summary>
    ''' Se ejecuta en el evento QueryPopUp del Control INDSleEmployees
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleEmployee_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleEmployee.QueryPopUp
        If INDSleEmployee.Datasource Is Nothing Then
            LoadXpoEmployee()
        End If
    End Sub


    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed

    End Sub

    Private Sub FrmReportVacation_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Cargar GridLookUpEdit        
        Me.INDGleState.Properties.DataSource = VacationState
        Me.INDGleState.EditValue = "T"
        Me.LoadXpoEmployee()


    End Sub

    ''' <summary>
    ''' Propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports() As Boolean
        If INDDateEnd.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("ValidateRangeDateReport", "Commons")
            INDDateEnd.Focus()
            Return False
        End If


        Return True
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
    ''' Se genera el reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports Then
            If INDDateEnd.EditValue IsNot Nothing Then
                AsyncLoader(True)
                Dim reporte As New rptVacation()

                reporte.ParametrosReporte = {
                    INDDateEnd.EditValue,
                    INDGleState.EditValue,
                    INDSleEmployee.EditValue,
                    INDsleGroupStart.EditValue,
                    INDsleGroupEnd.EditValue,
                    INDsleSucursalStart.EditValue,
                    INDsleSucursalEnd.EditValue}

                INDDvReport.DocumentSource = reporte

                reporte.CargarDataSource()
                If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                    reporte.CreateDocument(True)
                End If
                AsyncLoader(False)
                If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                    Me.INDLcBase.Visible = False
                    Me.INDCncReport.Visible = False
                    Me.INDPcReport.Visible = True
                    INDDvReport.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDDateEnd.Focus()
                End If

            End If
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento ClickBack del control INDCnBack
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnBack_ClickBack() Handles INDCnReport.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncReport.Visible = True
        Me.INDPcReport.Visible = False
        Me.INDDateEnd.Focus()
    End Sub
    ''' <summary>
    ''' Consulta los grupos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleGroupStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleGroupStart.QueryPopUp
        Using msearch As New MBusqueda
            ProoftCloseXpoGroup = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.GroupsPayroll)
            INDsleGroupStart.Properties.DataSource = ProoftCloseXpoGroup
        End Using
    End Sub
    ''' <summary>
    ''' Consulta lso grupos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleGroupEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleGroupEnd.QueryPopUp
        Using msearch As New MBusqueda
            ProoftCloseXpoGroup = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.GroupsPayroll)
            INDsleGroupEnd.Properties.DataSource = ProoftCloseXpoGroup
        End Using
    End Sub
    ''' <summary>
    ''' Consulta las sucursales 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleSucursalStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleSucursalStart.QueryPopUp
        Using msearch As New MBusqueda
            ProoftCloseXpoBranchOffice = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListBranchOffice)
            INDsleSucursalStart.Properties.DataSource = ProoftCloseXpoBranchOffice
        End Using
    End Sub
    ''' <summary>
    ''' Consulta las sucursales
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleSucursalEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleSucursalEnd.QueryPopUp
        Using msearch As New MBusqueda
            ProoftCloseXpoBranchOffice = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListBranchOffice)
            INDsleSucursalEnd.Properties.DataSource = ProoftCloseXpoBranchOffice
        End Using
    End Sub

#Region "Methods"
    ''' <summary>
    ''' Obtiene los datos de la moneda oficial
    ''' </summary>
    ''' <returns></returns>
    Public Function LoadPayrollSettings() As PayrollSettingsXpo
        Return XpoServiceEx.Instance(indigo.TransactionalContainer).PayrollService.GetCollection(Of PayrollSettingsXpo).FirstOrDefault()
    End Function

#End Region
#Region "Excel"
    ''' <summary>
    ''' Evento para generar el Excel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSbGenerateExcell_Click(sender As Object, e As EventArgs) Handles INDSbGenerateExcell.Click
        Try
            Dim fechaFinal = INDDateEnd.EditValue
            Dim filtroConsulta As String = "VacationPeriodId.EndDatePeriod = '" & Format(fechaFinal, "yyyy-MM-dd") & "'"

            'filtro por Estado
            If INDGleState.EditValue IsNot Nothing And INDGleState.EditValue <> "T" Then
                filtroConsulta &= " AND State = " & INDGleState.EditValue
            End If

            'filtro Por Empleado
            If INDSleEmployee.EditValue IsNot Nothing Then
                filtroConsulta &= " And VacationPeriodId.EmployeeId = " & INDSleEmployee.EditValue

            End If

            'Filtro por grupo
            Dim groupIni As String = IIf(INDsleGroupStart.EditValue Is Nothing, "NULL", INDsleGroupStart.EditValue)
            Dim groupFin As String = IIf(INDsleGroupEnd.EditValue Is Nothing, "NULL", INDsleGroupEnd.EditValue)
            filtroConsulta = String.Format("{0} AND ((VacationPeriodId.ContractId.GroupId >= {1} AND VacationPeriodId.ContractId.GroupId <= {2}) OR ({1} IS NULL AND {2} IS NULL))", filtroConsulta, groupIni, groupFin)
            'Filtro por sucursal
            Dim sucursalIni As String = IIf(INDsleSucursalStart.EditValue Is Nothing, "NULL", INDsleSucursalStart.EditValue)
            Dim sucursalFin As String = IIf(INDsleSucursalEnd.EditValue Is Nothing, "NULL", INDsleSucursalEnd.EditValue)

            filtroConsulta = String.Format("{0} AND ((VacationPeriodId.ContractId.FunctionalUnitId >= {1} AND VacationPeriodId.ContractId.FunctionalUnitId <= {2}) OR ({1} IS NULL AND {2} IS NULL))", filtroConsulta, sucursalIni, sucursalFin)




            Me.INDGcExportExcell.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PayrollService.GetCollection(Of PayrollVacation)(Nothing, filtroConsulta)
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
    ''' <summary>
    ''' Método que genera el Excel
    ''' </summary>
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

#End Region
End Class