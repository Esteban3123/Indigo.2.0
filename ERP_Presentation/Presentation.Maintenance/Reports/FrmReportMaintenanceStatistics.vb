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

#End Region

Public Class FrmReportMaintenanceStatistics

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
                _FillingGroupBy.Add(New Tuple(Of Integer, String)(4, "Estado"))
            End If
            Return _FillingGroupBy
        End Get
    End Property

    Private _LoadTypeReport As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property LoadTypeReport As List(Of Tuple(Of Integer, String))
        Get
            If _LoadTypeReport Is Nothing Then
                _LoadTypeReport = New List(Of Tuple(Of Integer, String))
                _LoadTypeReport.Add(New Tuple(Of Integer, String)(1, "Detallado"))
                _LoadTypeReport.Add(New Tuple(Of Integer, String)(2, "Resumido"))
            End If
            Return _LoadTypeReport
        End Get
    End Property

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

    Private Sub INDGvSelector_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvPlate.CustomUnboundColumnData, INDGvItem.CustomUnboundColumnData, INDGvItemCatalog.CustomUnboundColumnData, INDGvMaintenanceManager.CustomUnboundColumnData
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

    Private Sub INDGvSelector_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles INDGvPlate.RowCellClick, INDGvItem.RowCellClick, INDGvItemCatalog.RowCellClick, INDGvMaintenanceManager.RowCellClick
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

    Private Sub INDGvSelector_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDSlePlate.Closed, INDSleItem.Closed, INDSleItemCatalog.Closed, INDSleMaintenanceManager.Closed
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

    Private Sub chargueDatasource()
        'If INDGleTypeReport.EditValue = 1 Then
        Try
            Dim filtroConsulta As String = Nothing
            filtroConsulta = String.Format("ProgramDate >= #" & Format(INDDateEditDateStart.EditValue, "yyyy-MM-dd") & "# AND ProgramDate <= #" & Format(INDDateEditDateEnd.EditValue, "yyyy-MM-dd") & "#")
            ''Filtrar por placa
            If Not String.IsNullOrEmpty(_selectorPlate.GetKeys()) Then
                filtroConsulta &= String.Format(" AND PhysicalAssetId.Id IN ({0})", _selectorPlate.GetKeys())
            End If
            ''Filtrar por articulo
            If Not String.IsNullOrEmpty(_selectorItem.GetKeys()) Then
                filtroConsulta &= String.Format(" AND PhysicalAssetId.ItemId IN ({0})", _selectorItem.GetKeys())
            End If
            ''Filtrar por responsable
            If Not String.IsNullOrEmpty(_selectorMaintenanceManager.GetKeys()) Then
                filtroConsulta &= String.Format(" AND MaintenanceResponsibleId.Id IN ({0})", _selectorMaintenanceManager.GetKeys())
            End If
            ''Filtrar por catalogo de articulo
            If Not String.IsNullOrEmpty(_selectorItemCatalog.GetKeys()) Then
                filtroConsulta &= String.Format(" AND PhysicalAssetId.ItemId.ItemCatalogId IN ({0})", _selectorItemCatalog.GetKeys())
            End If
            ''Filtrar por estado
            If Not String.IsNullOrEmpty(INDGleStatusReport.EditValue) And INDGleStatusReport.EditValue <> 4 Then
                filtroConsulta &= " AND State = " & INDGleStatusReport.EditValue
            End If

            Dim listMaintenanceWorkOrderXpo = XpoServiceEx.Instance(indigo.TransactionalContainer).MaintenanceService.GetCollection(Of Maintenance_WorkOrder)(Nothing, filtroConsulta)

            Dim dt As New DataTable
            dt.Columns.Add("Placa")
            dt.Columns.Add("Cod - Articulo")
            dt.Columns.Add("Marca")
            dt.Columns.Add("Modelo")
            dt.Columns.Add("Serie")
            If INDGleTypeReport.EditValue = 1 Then
                dt.Columns.Add("Nro Orden de Trabajo")
                dt.Columns.Add("Encargado de Mantenimiento")
                dt.Columns.Add("Interno/Externo")
                dt.Columns.Add("Recibido")
                dt.Columns.Add("Estado")
            Else
                dt.Columns.Add("Instalación")
                dt.Columns.Add("Mantenimiento Programado")
                dt.Columns.Add("Mantenimiento Correctivo")
            End If

            For Each itemView In listMaintenanceWorkOrderXpo
                Dim row As DataRow = dt.NewRow()
                row.Item("Placa") = itemView.PhysicalAssetId.Plate
                row.Item("Cod - Articulo") = itemView.PhysicalAssetId.ItemId.CodeDescription
                row.Item("Marca") = itemView.PhysicalAssetId.TrademarkId.Descripcion
                row.Item("Modelo") = itemView.PhysicalAssetId.Model
                If INDGleTypeReport.EditValue = 1 Then
                    row.Item("Serie") = itemView.PhysicalAssetId.Serie
                    row.Item("Nro Orden de Trabajo") = itemView.Consecutive
                    row.Item("Encargado de Mantenimiento") = itemView.MaintenanceResponsibleId.ThirdPartyId.NitName
                    row.Item("Interno/Externo") = IIf(itemView.MaintenanceResponsibleId.ReponsibleTypeId = 5, "Externo", "Interno")
                    row.Item("Recibido") = IIf(itemView.State = 4, itemView.ModificationUser, "")
                    row.Item("Estado") = itemView.StateName
                Else
                    row.Item("Instalación") = ""
                    row.Item("Mantenimiento Programado") = IIf(itemView.EntityName = "MaintenancePlanProgramated", 1, 0)
                    row.Item("Mantenimiento Correctivo") = IIf(itemView.EntityName = "MaintenanceFailureRequest", 1, 0)
                End If

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
    Private Sub FrmReportMaintenanceStatistics_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Cargar GridLookUpEdit
        Me.INDGleGroupBy.Properties.DataSource = FillingGroupBy
        Me.INDGleTypeReport.Properties.DataSource = LoadTypeReport
        Me.INDGleStatusReport.Properties.DataSource = FillingStatus
        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleGroupBy.EditValue = 1
        Me.INDGleTypeReport.EditValue = 1
        '  Me.INDSleMaintenancePlan.EditValue = False
    End Sub

    ''' <summary>
    ''' Se inicializa los miembros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
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
            If INDGleTypeReport.EditValue = 1 Then
                AsyncLoader(True)

                Dim reporte As New rptReportMaintenanceStatistics
                reporte.ParametrosReporte = New Object() {INDDateEditDateStart.EditValue,
                                                           INDDateEditDateEnd.EditValue,
                                                          _selectorPlate.GetKeys(),
                                                          _selectorItem.GetKeys(),
                                                          _selectorMaintenanceManager.GetKeys(),
                                                          _selectorItemCatalog.GetKeys(),
                                                          INDGleGroupBy.EditValue,
                                                          INDGleStatusReport.EditValue
                                                          }
                INDDvDocumentViewer.DocumentSource = reporte
                Await reporte.CargarDataSourceAsync()
                If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                    reporte.CreateDocument(True)
                End If
                AsyncLoader(False)
                If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                    Me.INDLcBase.Visible = False
                    Me.INDCncNavigation.Visible = False
                    Me.INDPcDocumentViewer.Visible = True
                    INDDvDocumentViewer.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDDateEditDateStart.Focus()
                End If
            Else
                AsyncLoader(True)
                Dim reporte As New rptReportMaintenanceStatisticsSummary
                reporte.ParametrosReporte = New Object() {INDDateEditDateStart.EditValue,
                                                           INDDateEditDateEnd.EditValue,
                                                          _selectorPlate.GetKeys(),
                                                          _selectorItem.GetKeys(),
                                                          _selectorMaintenanceManager.GetKeys(),
                                                          _selectorItemCatalog.GetKeys(),
                                                          INDGleStatusReport.EditValue
                                                          }
                INDDvDocumentViewer.DocumentSource = reporte
                Await reporte.CargarDataSourceAsync()
                If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                    reporte.CreateDocument(True)
                End If
                AsyncLoader(False)
                If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                    Me.INDLcBase.Visible = False
                    Me.INDCncNavigation.Visible = False
                    Me.INDPcDocumentViewer.Visible = True
                    INDDvDocumentViewer.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDDateEditDateStart.Focus()
                End If
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
                Await Task.Factory.StartNew(AddressOf chargueDatasource)

                If Me.INDGcExportExcell.DataSource IsNot Nothing Then
                    generateExcel()
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