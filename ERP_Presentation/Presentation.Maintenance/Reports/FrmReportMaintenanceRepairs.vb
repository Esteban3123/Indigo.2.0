#Region "Imports"
Imports DevExpress.Xpo
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid
Imports System.Text
Imports Infrastructure.Data.Xpo
Imports Presentation.Reporter
Imports Infrastructure.Data.Xpo.MaintenanceRepository
Imports Presentation.CloudAgent

#End Region

Public Class FrmReportMaintenanceRepairs

#Region "Properties"
    Private _criterias As Dictionary(Of String, String)
    Private _filters As Dictionary(Of String, String)
    Dim DataSource As DataTable
#End Region

#Region "Datasource"

    Private _FillingStatus As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingStatus As List(Of Tuple(Of Integer, String))
        Get
            If _FillingStatus Is Nothing Then
                _FillingStatus = New List(Of Tuple(Of Integer, String))
                _FillingStatus.Add(New Tuple(Of Integer, String)(1, "Registrado"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(2, "Anulado"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(3, "Con Informe"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(4, "Todos"))
            End If
            Return _FillingStatus
        End Get
    End Property

    Private _FillingQuantity As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingQuantity As List(Of Tuple(Of Integer, String))
        Get
            If _FillingQuantity Is Nothing Then
                _FillingQuantity = New List(Of Tuple(Of Integer, String))
                _FillingQuantity.Add(New Tuple(Of Integer, String)(1, "Sin Mantenimientos"))
                _FillingQuantity.Add(New Tuple(Of Integer, String)(2, "Un Mantenimientos"))
                _FillingQuantity.Add(New Tuple(Of Integer, String)(3, "Dos Mantenimientos"))
                _FillingQuantity.Add(New Tuple(Of Integer, String)(4, "Tres Mantenimientos"))
                _FillingQuantity.Add(New Tuple(Of Integer, String)(5, "Cuatro Mantenimientos"))
                _FillingQuantity.Add(New Tuple(Of Integer, String)(6, "Cinco Mantenimientos"))
                _FillingQuantity.Add(New Tuple(Of Integer, String)(7, "Seis Mantenimientos"))
                _FillingQuantity.Add(New Tuple(Of Integer, String)(8, "Siete Mantenimientos"))
                _FillingQuantity.Add(New Tuple(Of Integer, String)(9, "Ocho Mantenimientos"))
                _FillingQuantity.Add(New Tuple(Of Integer, String)(10, "Nueve Mantenimientos"))
                _FillingQuantity.Add(New Tuple(Of Integer, String)(11, "Diez o mas Mantenimientos"))
            End If
            Return _FillingQuantity
        End Get
    End Property


    Public Async Function CargarDataSourceAsync() As Task
        Try
            AsyncLoader(True)
            Dim ds As DataSet = Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.GetListReportMaintenanceRepairsAsync(_criterias, _filters, Me.indigo)
            If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                Dim dtFixedAssetsMetrologyn As DataTable = ds.Tables("ReportMaintenanceRepairs")
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
        Dim errors As New StringBuilder
        Dim Validations As Boolean = True
        If INDDateEditDateStart.EditValue Is Nothing Or INDDateEditDateEnd.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
            Me.INDDateEditDateStart.Focus()
            Validations = False
        ElseIf Me.INDDateEditDateStart.EditValue > INDDateEditDateEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("FrmReportAuxiliary_CompareDate", "Accounting"))
            Me.INDDateEditDateStart.Focus()
            Validations = False
        End If

        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Return False
        End If

        _criterias = New Dictionary(Of String, String)
        _criterias.Add("DateStart", INDDateEditDateStart.EditValue)
        _criterias.Add("DateEnd", INDDateEditDateEnd.EditValue)

        _filters = New Dictionary(Of String, String)
        _filters.Add("PhysicalAssetId", _selectorPlate.GetKeys())
        _filters.Add("FixedAssetItemId", _selectorItem.GetKeys())
        _filters.Add("MaintenanceResponsibleId", _selectorMaintenanceManager.GetKeys())
        _filters.Add("FixedAssetItemCatalogId", _selectorItemCatalog.GetKeys())
        _filters.Add("State", If(INDGleStatusReport.EditValue = 4, Nothing, INDGleStatusReport.EditValue))
        _filters.Add("Quantity", If(INDGleQuantity.EditValue Is Nothing, 0, If(INDGleQuantity.EditValue = 1, -1, INDGleQuantity.EditValue - 1)))

        Return Validations
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

    Private Sub chargueDatasource(ByVal dtMaintenanceRapairs As DataTable)
        Try
            Dim dt As New DataTable
            dt.Columns.Add("Placa")
            dt.Columns.Add("Cod - Articulo")
            dt.Columns.Add("Marca")
            dt.Columns.Add("Modelo")
            dt.Columns.Add("Serie")
            dt.Columns.Add("Mant Programado")
            dt.Columns.Add("Mant Correctivo")
            dt.Columns.Add("Total Mante")
            For Each itemView In dtMaintenanceRapairs.Rows
                Dim row As DataRow = dt.NewRow()
                row.Item("Placa") = itemView("Plate")
                row.Item("Cod - Articulo") = itemView("ItemCodeName")
                row.Item("Marca") = itemView("TrademarkName")
                row.Item("Modelo") = itemView("Model")
                row.Item("Serie") = itemView("Serie")
                row.Item("Mant Programado") = itemView("Preventive")
                row.Item("Mant Correctivo") = itemView("Corrective")
                row.Item("Total Mante") = itemView("TotalMaintenance")
                dt.Rows.Add(row)
            Next
            AsyncLoader(False)
            INDGcExportExcell.DataSource = dt
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
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
    Private Sub FrmReportMaintenanceRepairs_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Cargar GridLookUpEdit
        Me.INDGleStatusReport.Properties.DataSource = FillingStatus
        Me.INDGleQuantity.Properties.DataSource = FillingQuantity
    End Sub

    ''' <summary>
    ''' Se inicializa los miembros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _selectorItem = Nothing
        _selectorItemCatalog = Nothing
        _selectorMaintenanceManager = Nothing
        _selectorPlate = Nothing
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
            reporte = New rptReportMaintenanceRepairs

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
                Me.INDDateEditDateStart.Focus()

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