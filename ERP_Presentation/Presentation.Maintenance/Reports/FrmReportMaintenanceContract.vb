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

Public Class FrmReportMaintenanceContract

#Region "Datasource"
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
                _FillingStatus.Add(New Tuple(Of Integer, String)(2, "Confirmado"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(3, "Anulado"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(4, "Finalizado"))
            End If
            Return _FillingStatus
        End Get
    End Property

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
    Private _selectorSupplier As SelectorCache = New SelectorCache("Id", "Code")
    Private _selectorContract As SelectorCache = New SelectorCache("Id", "ContractNumber")
    Private Sub INDGvSelector_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvPlate.CustomUnboundColumnData, INDGvItem.CustomUnboundColumnData, INDGvItemCatalog.CustomUnboundColumnData, INDGvSupplier.CustomUnboundColumnData, INDGvContract.CustomUnboundColumnData
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
            ElseIf view.Name = "INDGvSupplier" Then
                e.Value = _selectorSupplier.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvContract" Then
                e.Value = _selectorContract.ValidateExistsRow(e.Row)
            End If
        End If
    End Sub

    Private Sub INDGvSelector_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles INDGvPlate.RowCellClick, INDGvItem.RowCellClick, INDGvItemCatalog.RowCellClick, INDGvSupplier.RowCellClick, INDGvContract.RowCellClick
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
            ElseIf view.Name = "INDGvSupplier" Then
                selector = _selectorSupplier
            ElseIf view.Name = "INDGvContract" Then
                selector = _selectorContract
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

    Private Sub INDGvSelector_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDSlePlate.Closed, INDSleItem.Closed, INDSleItemCatalog.Closed, INDSleSupplier.Closed, INDSleContract.Closed
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
        ElseIf searchLookupEdit.Name = "INDSleSupplier" Then
            searchLookupEdit.Properties.NullText = _selectorSupplier.ToString()
        ElseIf searchLookupEdit.Name = "INDSleContract" Then
            searchLookupEdit.Properties.NullText = _selectorContract.ToString()
        End If
    End Sub

#End Region

#Region "ToExcel"

    Private Sub chargueDatasource()

        Try
            Dim filtroConsulta As String = Nothing
            filtroConsulta = String.Format("MaintenanceContractId.InitialDate >= #" & Format(INDDateEditDateStart.EditValue, "yyyy-MM-dd") & "# AND MaintenanceContractId.EndDate <= #" & Format(INDDateEditDateEnd.EditValue, "yyyy-MM-dd") & "#")
            'Filtrar por placa
            If Not String.IsNullOrEmpty(_selectorPlate.GetKeys()) Then
                filtroConsulta &= String.Format(" AND PhysicalAssetId.Id IN ({0})", _selectorPlate.GetKeys())
            End If
            'Filtrar por articulo
            If Not String.IsNullOrEmpty(_selectorItem.GetKeys()) Then
                filtroConsulta &= String.Format(" AND PhysicalAssetId.ItemId.Id IN ({0})", _selectorItem.GetKeys())
            End If
            'Filtrar por proveedor
            If Not String.IsNullOrEmpty(_selectorSupplier.GetKeys()) Then
                filtroConsulta &= String.Format(" AND MaintenanceContractId.SupplierId.Id IN ({0})", _selectorSupplier.GetKeys())
            End If
            'Filtrar por contrato
            If Not String.IsNullOrEmpty(_selectorContract.GetKeys()) Then
                filtroConsulta &= String.Format(" AND MaintenanceContractId.Id IN ({0})", _selectorContract.GetKeys())
            End If
            'Filtrar por catalogo de articulo
            If Not String.IsNullOrEmpty(_selectorItem.GetKeys()) Then
                filtroConsulta &= String.Format(" AND PhysicalAssetId.ItemId.ItemCatalogId.Id IN ({0})", _selectorItem.GetKeys())
            End If
            'Filtrar por estado
            If Not String.IsNullOrEmpty(INDGleStatusReport.EditValue) And INDGleStatusReport.EditValue <> 4 Then
                filtroConsulta &= " AND MaintenanceContractId.Status = " & INDGleStatusReport.EditValue
            End If

            Dim ListContract = XpoServiceEx.Instance(indigo.TransactionalContainer).MaintenanceService.GetCollection(Of MaintenanceContractDetailXpo)(Nothing, filtroConsulta)

            Dim dt As New DataTable
            dt.Columns.Add("Nro Contrato")
            dt.Columns.Add("Fecha", GetType(Date))
            dt.Columns.Add("Nit")
            dt.Columns.Add("Proveedor")
            dt.Columns.Add("Descripcion del Contrato")
            dt.Columns.Add("Fecha Inicial", GetType(Date))
            dt.Columns.Add("Fecha Final", GetType(Date))
            dt.Columns.Add("Supervisor Tecnico")
            dt.Columns.Add("Supervisor de Ejecución")
            dt.Columns.Add("Placa")
            dt.Columns.Add("Cod Articulo")
            dt.Columns.Add("Ubicación")
            dt.Columns.Add("Estado")

            For Each itemView In ListContract
                Dim row As DataRow = dt.NewRow()
                row.Item("Nro Contrato") = itemView.MaintenanceContractId.ContractNumber
                row.Item("Fecha") = itemView.MaintenanceContractId.DocumentDate
                row.Item("Nit") = itemView.MaintenanceContractId.SupplierId.IdThirdParty.Nit
                row.Item("Proveedor") = itemView.MaintenanceContractId.SupplierId.CodeName
                row.Item("Descripcion del Contrato") = itemView.MaintenanceContractId.Description
                row.Item("Fecha Inicial") = itemView.MaintenanceContractId.InitialDate
                row.Item("Fecha Final") = itemView.MaintenanceContractId.EndDate
                row.Item("Supervisor Tecnico") = itemView.MaintenanceContractId.TechnicalSupervicion
                row.Item("Supervisor de Ejecución") = itemView.MaintenanceContractId.SupervisionExecution
                row.Item("Placa") = itemView.PhysicalAssetId.Plate
                row.Item("Cod Articulo") = itemView.PhysicalAssetId.ItemId.CodeDescription
                row.Item("Ubicación") = itemView.PhysicalAssetId.LocationId.Name
                row.Item("Estado") = IIf(itemView.MaintenanceContractId.Status = 1, "Registrado", IIf(itemView.MaintenanceContractId.Status = 2, "Confirmado", IIf(itemView.MaintenanceContractId.Status = 3, "Anulado", IIf(itemView.MaintenanceContractId.Status = 4, "Finalizado", "Otro"))))

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
    Private Sub FrmReportMaintenanceContract_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Cargar GridLookUpEdit
        Me.INDGleGroupBy.Properties.DataSource = FillingGroupBy
        Me.INDGleTypeReport.Properties.DataSource = LoadTypeReport
        Me.INDGleStatusReport.Properties.DataSource = FillingStatus
        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleGroupBy.EditValue = 1
        Me.INDGleTypeReport.EditValue = 1
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
        _selectorSupplier = Nothing
        _selectorItem = Nothing
        _selectorItemCatalog = Nothing
        _selectorMaintenanceManager = Nothing
        _selectorPlate = Nothing
    End Sub

#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control Placa
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
    ''' se ejecuta en el evento querypopup del control proveedor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleSupplier_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleSupplier.QueryPopUp
        If INDSleSupplier.Properties.DataSource Is Nothing Then
            INDSleSupplier.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).PaymentsService.ListSupplierReport()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control Articulo
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
    ''' se ejecuta en el evento querypopup del control del contrato
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleContract_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleContract.QueryPopUp
        If INDSleContract.Properties.DataSource Is Nothing Then
            INDSleContract.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).MaintenanceService.ListMaintenanceContract()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control Catalogo de articulos
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

                Dim reporte As New rptReportMaintenanceContract
                reporte.ParametrosReporte = New Object() {INDDateEditDateStart.EditValue,
                                                           INDDateEditDateEnd.EditValue,
                                                          _selectorPlate.GetKeys(),
                                                          _selectorItem.GetKeys(),
                                                          _selectorSupplier.GetKeys(),
                                                          _selectorContract.GetKeys(),
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

                Dim reporte As New rptReportMaintenanceContractDetail
                reporte.ParametrosReporte = New Object() {INDDateEditDateStart.EditValue,
                                                           INDDateEditDateEnd.EditValue,
                                                          _selectorPlate.GetKeys(),
                                                          _selectorItem.GetKeys(),
                                                          _selectorSupplier.GetKeys(),
                                                          _selectorContract.GetKeys(),
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