#Region "Imports"

Imports System.Text
Imports DevExpress.Xpo
Imports DevExpress.XtraGrid.Views.Grid
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Presentation.Base
Imports Presentation.CloudAgent
Imports Presentation.Reporter

#End Region

Public Class FrmReportFixedAssetsMetrology
    Private _criterias As Dictionary(Of String, String)
    Private _filters As Dictionary(Of String, String)
    Dim DataSource As DataTable

#Region "Datasource"

    Public Property ProoftCloseXpoDocuments As XPInstantFeedbackSource
    Public Property ProoftCloseXpoSupplier As XPInstantFeedbackSource
    Private _FillingGroupBy As List(Of Tuple(Of Integer, String))

    Private ReadOnly Property FillingGroupBy As List(Of Tuple(Of Integer, String))
        Get
            If _FillingGroupBy Is Nothing Then
                _FillingGroupBy = New List(Of Tuple(Of Integer, String))
                _FillingGroupBy.Add(New Tuple(Of Integer, String)(1, "Tipo de Equipo"))
                _FillingGroupBy.Add(New Tuple(Of Integer, String)(2, "Tipo Catalogo de artículos"))
                _FillingGroupBy.Add(New Tuple(Of Integer, String)(3, "Articulo"))
                _FillingGroupBy.Add(New Tuple(Of Integer, String)(3, "Estado"))
            End If
            Return _FillingGroupBy
        End Get
    End Property

    Private _LoadTypeReport As List(Of Tuple(Of Integer, String))

    Private ReadOnly Property LoadTypeReport As List(Of Tuple(Of Integer, String))
        Get
            If _LoadTypeReport Is Nothing Then
                _LoadTypeReport = New List(Of Tuple(Of Integer, String))
                _LoadTypeReport.Add(New Tuple(Of Integer, String)(1, "Agrupado"))
                _LoadTypeReport.Add(New Tuple(Of Integer, String)(2, "Detallado"))
            End If
            Return _LoadTypeReport
        End Get
    End Property

#End Region

#Region "BarraBotones"

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

#End Region

#Region "Methods"

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
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports() As Boolean

        Dim Validations As Boolean = True
        Dim errors As New StringBuilder

        If INDDeCutOffDate.EditValue Is Nothing Then
            errors.AppendLine("Debe seleccionar una fecha de corte")
            Me.INDDeCutOffDate.Focus()
            Validations = False
        End If

        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Return False
        End If

        _criterias = New Dictionary(Of String, String)

        _criterias.Add("ReportType", INDGleTypeReport.EditValue)
        _criterias.Add("GroupBy", INDGleGroupBy.EditValue)
        _criterias.Add("MaintenancePlan", INDSleMaintenancePlan.EditValue)

        _filters = New Dictionary(Of String, String)
        _filters.Add("PhysicalAssetId", _selectorPlate.GetKeys())
        _filters.Add("FixedAssetItemId", _selectorItem.GetKeys())
        _filters.Add("MaintenanceResponsibleId", _selectorMaintenanceManager.GetKeys())
        _filters.Add("FixedAssetItemCatalogId", _selectorItemCatalog.GetKeys())
        _filters.Add("ClosingDate", INDDeCutOffDate.EditValue)

        Return True
    End Function

    Public Async Function CargarDataSourceAsync() As Task
        Try
            AsyncLoader(True)
            Dim ds As DataSet = Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.GetListReportFixedAssetsMetrologyAsync(_criterias, _filters, Me.indigo)
            If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                Dim dtFixedAssetsMetrologyn As DataTable = ds.Tables("ReportFixedAssetsMetrology")
                Me.DataSource = dtFixedAssetsMetrologyn
            Else
                Me.DataSource = Nothing
            End If
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
            Me.DataSource = Nothing
        Finally
            AsyncLoader(False)
        End Try
    End Function

#Region "Selector"

    ''' <summary>
    ''' funcionamiento del Multiselect
    ''' </summary>
    Private _selectorPlate As SelectorCache = New SelectorCache("Id", "FixedAssetItemCodeNameWithPlate")

    Private _selectorItem As SelectorCache = New SelectorCache("Id", "Description")
    Private _selectorMaintenanceManager As SelectorCache = New SelectorCache("Id", "CodeNitName")
    Private _selectorItemCatalog As SelectorCache = New SelectorCache("Id", "Code")

    Private Sub INDGvSelector_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvPlate.CustomUnboundColumnData, INDGvItem.CustomUnboundColumnData, INDGvMaintenanceManager.CustomUnboundColumnData, INDGvItemCatalog.CustomUnboundColumnData
        If e.IsGetData Then
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvPlate" Then
                e.Value = _selectorPlate.ValidateExistsRow(e.Row)

            ElseIf view.Name = "INDGvItem" Then
                e.Value = _selectorItem.ValidateExistsRow(e.Row)

            ElseIf view.Name = "INDGvMaintenanceManager" Then
                e.Value = _selectorMaintenanceManager.ValidateExistsRow(e.Row)

            ElseIf view.Name = "INDGvItemCatalog" Then
                e.Value = _selectorItemCatalog.ValidateExistsRow(e.Row)
            End If
        End If
    End Sub

    Private Sub INDGvSelector_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles INDGvPlate.RowCellClick, INDGvItem.RowCellClick, INDGvMaintenanceManager.RowCellClick, INDGvItemCatalog.RowCellClick
        If e.Column.FieldName.Contains("UnboundSelection") Then
            Dim selector As SelectorCache = Nothing
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvPlate" Then
                selector = _selectorPlate

            ElseIf view.Name = "INDGvItem" Then
                selector = _selectorItem

            ElseIf view.Name = "INDGvMaintenanceManager" Then
                selector = _selectorMaintenanceManager

            ElseIf view.Name = "INDGvItemCatalog" Then
                selector = _selectorItemCatalog

            End If

            If e.RowHandle >= 0 Then
                Dim row = view.GetRow(e.RowHandle)
                selector.SetValue(row)
            Else
                selector.Clear()
            End If
            view.RefreshData()
        End If
    End Sub

    Private Sub INDGvSelector_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDSlePlate.Closed, INDSleItem.Closed, INDSleMaintenanceManager.Closed, INDSleItemCatalog.Closed
        Dim searchLookupEdit = TryCast(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        searchLookupEdit.EditValue = Nothing
        If searchLookupEdit.Name = "INDSlePlate" Then
            searchLookupEdit.Properties.NullText = _selectorPlate.ToString()

        ElseIf searchLookupEdit.Name = "INDSleItem" Then
            searchLookupEdit.Properties.NullText = _selectorItem.ToString()

        ElseIf searchLookupEdit.Name = "INDSleMaintenanceManager" Then
            searchLookupEdit.Properties.NullText = _selectorMaintenanceManager.ToString()

        ElseIf searchLookupEdit.Name = "INDSleItemCatalog" Then
            searchLookupEdit.Properties.NullText = _selectorItemCatalog.ToString()

        End If
    End Sub

#End Region

#Region "ToExcel"

    Private Sub chargueDatasource(ByVal dtFixedAssetsMetrologyn As DataTable)
        Dim dt As New DataTable

        If INDGleTypeReport.EditValue = 2 Then
            dt.Columns.Add("Placa")
            dt.Columns.Add("Cod - Articulo")
            dt.Columns.Add("Marca")
            dt.Columns.Add("Modelo")
            dt.Columns.Add("Serie")
        End If

        dt.Columns.Add("Tipo de Equipo")
        dt.Columns.Add("Catalogo")
        dt.Columns.Add("Programado")
        If INDGleTypeReport.EditValue = 1 Then
            dt.Columns.Add("Cantidad de Equipos")
            dt.Columns.Add("Estado")
        End If

        If INDGleTypeReport.EditValue = 2 Then
            dt.Columns.Add("Fecha Progrmacion")
            dt.Columns.Add("Nro Calibracion")
            dt.Columns.Add("Fecha", GetType(DateTime))
        End If

        dt.Columns.Add("Dias por Vencer")

        For Each item In dtFixedAssetsMetrologyn.Rows
            Dim row As DataRow = dt.NewRow()

            If INDGleTypeReport.EditValue = 2 Then
                row.Item("Placa") = item("Plate")
                row.Item("Cod - Articulo") = item("ArticleCodeName")
                row.Item("Marca") = item("TrademarkCodeName")
                row.Item("Modelo") = item("Model")
                row.Item("Serie") = item("Serie")
            End If

            Select Case item("Type")
                Case 1
                    row.Item("Tipo de Equipo") = "Equipo Biomédico"
                Case 2
                    row.Item("Tipo de Equipo") = "Dispositivo Médico"
                Case 3
                    row.Item("Tipo de Equipo") = "Equipo Industrial"
                Case 4
                    row.Item("Tipo de Equipo") = "Construcciones y Edificaciones"
                Case 5
                    row.Item("Tipo de Equipo") = "Moviliario"
                Case 6
                    row.Item("Tipo de Equipo") = "Equipos de Oficina"
                Case 7
                    row.Item("Tipo de Equipo") = "Muebles de Oficina"
                Case 8
                    row.Item("Tipo de Equipo") = "Terrenos"
                Case 9
                    row.Item("Tipo de Equipo") = "Equipo de transporte"
                Case Else
                    row.Item("Tipo de Equipo") = "Otros"
            End Select

            row.Item("Catalogo") = item("ItemCatalog")
            row.Item("Programado") = item("programmed")

            If INDGleTypeReport.EditValue = 1 Then
                row.Item("Cantidad de Equipos") = item("CountType")

                Select Case item("ExpiredOrNot")
                    Case 1
                        row.Item("Estado") = "Sin Vencer"
                    Case 2
                        row.Item("Estado") = "Vencido"
                    Case Else
                        row.Item("Estado") = "Sin Estado"
                End Select
            End If
            If INDGleTypeReport.EditValue = 2 Then
                row.Item("Fecha Progrmacion") = item("NextMaintenance")
                row.Item("Nro Calibracion") = item("CalibrationNumber")
                row.Item("Fecha") = CDate(item("ClosingDate")).AsDate
            End If

            row.Item("Dias por Vencer") = item("DaysToExpire")

            dt.Rows.Add(row)
        Next

        INDGcExportExcell.DataSource = dt
    End Sub

    ''' <summary>
    ''' metodo para generar excel
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub generateExcel()
        Dim _gridView = Me.INDGcExportExcell
        _gridView.MainView.PopulateColumns()
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

#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' se ejecuta al mostrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportFixedAssetsMetrology_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Cargar GridLookUpEdit
        Me.INDGleGroupBy.Properties.DataSource = FillingGroupBy
        Me.INDGleTypeReport.Properties.DataSource = LoadTypeReport
        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleGroupBy.EditValue = 1
        Me.INDGleTypeReport.EditValue = 1
        Me.INDSleMaintenancePlan.EditValue = False
    End Sub

    ''' <summary>
    ''' Se inicializa los miembros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportFixedAssetsMetrology_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        INDDeCutOffDate.Focus()
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _FillingGroupBy = Nothing
        _LoadTypeReport = Nothing
    End Sub

#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSlePlate
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSlePlate_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlePlate.QueryPopUp
        If INDSlePlate.Properties.DataSource Is Nothing Then
            INDSlePlate.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).FixedAsset.ListFixedAssetPhysicalAsset()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleItem
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleItem_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleItem.QueryPopUp
        If INDSleItem.Properties.DataSource Is Nothing Then

            INDSleItem.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).FixedAsset.ListItem()

        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleMaintenanceManager
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleMaintenanceManager_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleMaintenanceManager.QueryPopUp
        If INDSleMaintenanceManager.Properties.DataSource Is Nothing Then
            INDSleMaintenanceManager.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).MaintenanceService.ListMaintenanceResponsible()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleItemCatalog
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleItemCatalog_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleItemCatalog.QueryPopUp
        If INDSleItemCatalog.Properties.DataSource Is Nothing Then
            INDSleItemCatalog.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).FixedAsset.ListCatalogItem()
        End If
    End Sub

#End Region

#Region "EditValueChange"

    Private Sub INDGleTypeReport_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleTypeReport.EditValueChanged
        If INDGleTypeReport.EditValue = 2 Then
            INDLciGroupBy.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDGleGroupBy.EditValue = Nothing
        End If

        If INDGleTypeReport.EditValue = 1 Then
            INDLciGroupBy.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDGleGroupBy.EditValue = 1
        End If
    End Sub

#End Region

#Region "Report"

    ''' <summary>
    ''' se ejecuta cuando den click en el boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports = True Then
            AsyncLoader(True)

            Dim reporte As Object

            Select Case INDGleTypeReport.EditValue
                Case 1
                    reporte = New rptFixedAssetsMetrologySummary
                Case 2
                    reporte = New rptFixedAssetsMetrologyDetail
            End Select

            reporte.ParametrosReporte = New Object() {_criterias, _filters}
            INDDvDocumentViewer.DocumentSource = reporte
            Await reporte.CargarDataSourceAsync()

            AsyncLoader(False)
            If reporte.DataSource IsNot Nothing Then
                reporte.CreateDocument(True)

                Me.INDLcBase.Visible = False
                Me.INDCncNavigation.Visible = False
                Me.INDPcDocumentViewer.Visible = True
                INDDvDocumentViewer.Show()
                Me.INDLcBase.Visible = False
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                Me.INDDeCutOffDate.Focus()

            End If

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
    ''' se ejecuta para exportar a excel el reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateExcell_Click(sender As Object, e As EventArgs) Handles INDSbGenerateExcell.Click
        If Me.ValidateControlsReports = True Then
            Try
                AsyncLoader(True)
                Await CargarDataSourceAsync()
                If DataSource IsNot Nothing Then

                    chargueDatasource(DataSource)

                    If Me.INDGcExportExcell.DataSource IsNot Nothing Then
                        generateExcel()
                    End If
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                End If
            Catch ex As Exception
                Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
            Finally
                AsyncLoader(False)
            End Try
        End If
    End Sub

#End Region

#End Region

End Class