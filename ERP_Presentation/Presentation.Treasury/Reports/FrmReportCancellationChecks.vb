#Region "Imports"
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.TreasuryRepository
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports Presentation.Reporter
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Common.MVP
Imports Infrastructure.Data.Xpo

#End Region

Public Class FrmReportCancellationChecks

#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

#End Region

#Region "Properties"
    Public Property ProoftCloseXpoCancellationChecks As XPInstantFeedbackSource

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

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

#End Region

#Region "Methods"

    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()

        Dim Validations As Boolean = True
        If INDDeDateStart.EditValue Is Nothing Or INDDeDateEnd.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
            Me.INDDeDateStart.Focus()
            Validations = False
        ElseIf Me.INDDeDateStart.EditValue > INDDeDateEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("CompareRangeDate", "Commons"))
            Me.INDDeDateStart.Focus()
            Validations = False
        End If
        'validaciones controles de cheques cancelados
        If INDSleCancellationCheckStart.EditValue IsNot Nothing And INDSleCancellationCheckEnd.EditValue Is Nothing Or INDSleCancellationCheckStart.EditValue Is Nothing And INDSleCancellationCheckEnd.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblCancellationCheck.Text)
            Me.INDSleCancellationCheckStart.Focus()
            Validations = False
        ElseIf INDSleCancellationCheckStart.EditValue > INDSleCancellationCheckEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblCancellationCheck.Text)
            Me.INDSleCancellationCheckStart.Focus()
            Validations = False
        End If
        Return Validations

    End Function

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleCancellationCheckStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoCancellationCheckStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoCancellationChecks = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCancellationChecksReportTreasury)
            INDSleCancellationCheckStart.Datasource = ProoftCloseXpoCancellationChecks
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleCancellationCheckEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoCancellationCheckEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoCancellationChecks = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCancellationChecksReportTreasury)
            INDSleCancellationCheckEnd.Datasource = ProoftCloseXpoCancellationChecks
        End Using
    End Sub

    ''' <summary>
    ''' creamos un datatable para generar el excel detallado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function chargueDatasource() As DataTable

        Dim filtroConsulta As String = "GetDate(CancellationDate) >= #" & Format(INDDeDateStart.EditValue, "yyyy-MM-dd") & "# AND GetDate(CancellationDate) <= #" & Format(INDDeDateEnd.EditValue, "yyyy-MM-dd") & "#"

        'si filtra por documentos
        If INDSleCancellationCheckStart.EditValue IsNot Nothing And INDSleCancellationCheckEnd.EditValue IsNot Nothing Then
            filtroConsulta &= "AND Id >= '" & INDSleCancellationCheckStart.EditValue & "' AND Id <= '" & INDSleCancellationCheckEnd.EditValue & "'"
        End If

        Dim IndList = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of TreasuryCancellationChecksXpo)(Nothing, filtroConsulta)

        Dim dt As New DataTable
        dt.Columns.Add("No Cheque")
        dt.Columns.Add("Cuenta Corriente Código")
        dt.Columns.Add("Cuenta Corriente Nombre")
        dt.Columns.Add("Fecha Cancelación")

        For Each itemView In IndList
            Dim row As DataRow = dt.NewRow()
            row.Item("No Cheque") = itemView.CheckNumber
            row.Item("Cuenta Corriente Código") = itemView.IdEntityAccount.Number
            row.Item("Cuenta Corriente Nombre") = itemView.IdEntityAccount.IdBank.Name
            row.Item("Fecha Cancelación") = itemView.CancellationDate

            dt.Rows.Add(row)
        Next
        AsyncLoader(False)
        If dt IsNot Nothing Then
            Return dt
        Else
            Return New DataTable
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
            Me.INDDeDateStart.EditValue.Focus()
        End If
    End Function

    ''' <summary>
    ''' metodo para generar excel
    ''' </summary>
    ''' <remarks></remarks>
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

#Region "Events"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _pucModel.Dispose()
        _pucModel = Nothing
        ProoftCloseXpoCancellationChecks = Nothing
    End Sub
    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDPceListCancellationChecks
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleCancellationCheckStart_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCancellationCheckStart.QueryPopUp
        If INDSleCancellationCheckStart.Datasource Is Nothing Then
            LoadXpoCancellationCheckStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDPceListCancellationChecks
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleCancellationCheckEnd_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCancellationCheckEnd.QueryPopUp
        If INDSleCancellationCheckEnd.Datasource Is Nothing Then
            LoadXpoCancellationCheckEnd()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta al dar click en el control INDCnBack
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnBack_ClickBack() Handles INDCnBack.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncNavigation.Visible = True
        Me.INDPcDocumentViewer.Visible = False
    End Sub

    ''' <summary>
    ''' se ejecuta cuando den click en el boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports = True Then
            AsyncLoader(True)
            Dim reporte As New rptReportCancellationChecks
            reporte.ParametrosReporte = New Object() {INDDeDateStart.EditValue,
                                                      INDDeDateEnd.EditValue,
                                                      INDSleCancellationCheckStart.EditValue,
                                                      INDSleCancellationCheckEnd.EditValue}
            INDDvDocumentViewer.DocumentSource = reporte
            reporte.CargarDataSource()
            reporte.CreateDocument(True)
            AsyncLoader(False)
            If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                Me.INDLcBase.Visible = False
                Me.INDCncNavigation.Visible = False
                Me.INDPcDocumentViewer.Visible = True
                INDDvDocumentViewer.Show()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                Me.INDDeDateStart.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Se inicializa los miembros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportCancellationChecks_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Inicializamos la referencia al modelo de puc
        Me._pucModel = New MCommon(Me.Tag)
        Me.INDSleCancellationCheckStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetCancellationCheckById
        Me.INDSleCancellationCheckEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetCancellationCheckById
    End Sub

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

    ''' <summary>
    ''' se ejecuta para exportar a excel el reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSbGenerateExcell_Click(sender As Object, e As EventArgs) Handles INDSbGenerateExcell.Click
        If Me.ValidateControlsReports = True Then
            AsyncLoader(True)
            INDGcExportExcell.DataSource = chargueDatasource()
            If Me.INDGcExportExcell.DataSource IsNot Nothing Then
                generateExcel()
            End If
            AsyncLoader(False)
        End If
    End Sub

#End Region

End Class