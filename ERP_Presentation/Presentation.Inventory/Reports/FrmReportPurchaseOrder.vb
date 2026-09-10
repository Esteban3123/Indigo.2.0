#Region "Imports"
Imports Presentation.Reporter
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports Presentation.Common.MVP
Imports System.Text
Imports Infrastructure.Data.Xpo

#End Region

Public Class FrmReportPurchaseOrder

#Region "Datasource"
    Public Property ProoftCloseXpoDocuments As XPInstantFeedbackSource
    Public Property ProoftCloseXpoSupplier As XPInstantFeedbackSource
    Private _FillingGroupBy As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingGroupBy As List(Of Tuple(Of Integer, String))
        Get
            If _FillingGroupBy Is Nothing Then
                _FillingGroupBy = New List(Of Tuple(Of Integer, String))
                _FillingGroupBy.Add(New Tuple(Of Integer, String)(1, "Proveedor"))
                _FillingGroupBy.Add(New Tuple(Of Integer, String)(2, "Fecha"))
            End If
            Return _FillingGroupBy
        End Get
    End Property

    Private _LoadTypeReport As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property LoadTypeReport As List(Of Tuple(Of Integer, String))
        Get
            If _LoadTypeReport Is Nothing Then
                _LoadTypeReport = New List(Of Tuple(Of Integer, String))
                _LoadTypeReport.Add(New Tuple(Of Integer, String)(1, "Resumido"))
                _LoadTypeReport.Add(New Tuple(Of Integer, String)(2, "Detallado"))
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

        Dim Validations As Boolean = True
        Dim errors As New StringBuilder

        'validaciones controles de fecha
        If INDDeDateStart.EditValue Is Nothing Or INDDeDateEnd.EditValue Is Nothing Then
            errors.AppendLine(String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons")))
            Me.INDDeDateStart.Focus()
            Validations = False
        ElseIf Me.INDDeDateStart.EditValue > INDDeDateEnd.EditValue Then
            errors.AppendLine(String.Format(ResourceManager.GetString("CompareRangeDate", "Commons")))
            Me.INDDeDateStart.Focus()
            Validations = False
        End If

        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Return False
        End If

        Return True
    End Function

#Region "Selector"

    ''' <summary>
    ''' funcionamiento del Multiselect
    ''' </summary>
    Private _selectorSupplier As SelectorCache = New SelectorCache("ThirdPartyId", "ThirdPartyNit")
    Private _selectorDocument As SelectorCache = New SelectorCache("Id", "Code")
    Private _selectorWarehouse As SelectorCache = New SelectorCache("Id", "Code")

    Private Sub INDGvSelector_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvSupplier.CustomUnboundColumnData, INDGvDocument.CustomUnboundColumnData, INDGvWarehouse.CustomUnboundColumnData
        If e.IsGetData Then
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvSupplier" Then
                e.Value = _selectorSupplier.ValidateExistsRow(e.Row)

            ElseIf view.Name = "INDGvDocument" Then
                e.Value = _selectorDocument.ValidateExistsRow(e.Row)

            ElseIf view.Name = "INDGvWarehouse" Then
                e.Value = _selectorWarehouse.ValidateExistsRow(e.Row)
            End If
        End If
    End Sub

    Private Sub INDGvSelector_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles INDGvSupplier.RowCellClick, INDGvDocument.RowCellClick, INDGvWarehouse.RowCellClick
        If e.Column.FieldName.Contains("UnboundSelection") Then
            Dim selector As SelectorCache = Nothing
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvSupplier" Then
                selector = _selectorSupplier

            ElseIf view.Name = "INDGvDocument" Then
                selector = _selectorDocument

            ElseIf view.Name = "INDGvWarehouse" Then
                selector = _selectorWarehouse

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

    Private Sub INDGvSelector_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDSleSupplier.Closed, INDSleDocument.Closed, INDSleWarehouse.Closed
        Dim searchLookupEdit = TryCast(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        searchLookupEdit.EditValue = Nothing
        If searchLookupEdit.Name = "INDSleSupplier" Then
            searchLookupEdit.Properties.NullText = _selectorSupplier.ToString()

        ElseIf searchLookupEdit.Name = "INDSleDocument" Then
            searchLookupEdit.Properties.NullText = _selectorDocument.ToString()

        ElseIf searchLookupEdit.Name = "INDSleWarehouse" Then
            searchLookupEdit.Properties.NullText = _selectorWarehouse.ToString()

        End If
    End Sub

#End Region

#Region "ToExcel"

    Private Sub chargueDatasource()
        Try
            Dim filter As String = Nothing

            'filtro por fechas
            If INDDeDateStart.EditValue IsNot Nothing AndAlso INDDeDateEnd.EditValue IsNot Nothing Then
                filter = "GetDate(DocumentDate) >= #" & Format(INDDeDateStart.EditValue, "yyyy-MM-dd") & "# AND GetDate(DocumentDate) <= #" & Format(INDDeDateEnd.EditValue, "yyyy-MM-dd") & "#"
            End If

            'si filtra por Estado
            If INDGleStatus.EditValue <> 4 Then
                filter &= If(String.IsNullOrEmpty(filter), "", " AND ") & String.Format("Status IN ({0})", INDGleStatus.EditValue)
            End If

            'si filtra por Almacen
            If Not String.IsNullOrEmpty(_selectorWarehouse.GetKeys()) Then
                filter &= If(String.IsNullOrEmpty(filter), "", " AND ") & String.Format(" WarehouseId.Id IN ({0})", _selectorWarehouse.GetKeys())
            End If

            'si filtra por proveedor
            If Not String.IsNullOrEmpty(_selectorSupplier.GetKeys()) Then
                filter &= If(String.IsNullOrEmpty(filter), "", " AND ") & String.Format("SupplierId.IdThirdParty.Id IN ({0})", _selectorSupplier.GetKeys())
            End If

            'si filtra por documento
            If Not String.IsNullOrEmpty(_selectorDocument.GetKeys()) Then
                filter &= If(String.IsNullOrEmpty(filter), "", " AND ") & String.Format("Id In ({0})", _selectorDocument.GetKeys())
            End If

            Dim listPurchaseOrder = XpoServiceEx.Instance(indigo.TransactionalContainer).TreasuryService.GetCollection(Of InventoryPurchaseOrderReportXpo)(Nothing, filter)
            If listPurchaseOrder IsNot Nothing AndAlso listPurchaseOrder.Count > 0 Then
                Dim dt As New DataTable
                dt.Columns.Add("Código")
                dt.Columns.Add("Fecha")
                dt.Columns.Add("Basado Contrato")
                dt.Columns.Add("Contrato")
                dt.Columns.Add("Proveedor")
                dt.Columns.Add("Tipo Orden")
                dt.Columns.Add("Detalle")
                dt.Columns.Add("Almacen")
                dt.Columns.Add("Producto")
                dt.Columns.Add("Moneda")
                dt.Columns.Add("Cantidad", GetType(Decimal))
                dt.Columns.Add("Descuento", GetType(Decimal))
                dt.Columns.Add("Subtotal", GetType(Decimal))
                dt.Columns.Add("IVA", GetType(Decimal))
                dt.Columns.Add("Estado")

                For Each itemView In listPurchaseOrder
                    If itemView.Inventory_PurchaseOrderDetails IsNot Nothing AndAlso itemView.Inventory_PurchaseOrderDetails.Count > 0 Then
                        For Each detail In itemView.Inventory_PurchaseOrderDetails
                            Dim row As DataRow = dt.NewRow()

                            row.Item("Código") = itemView.Code
                            row.Item("Fecha") = itemView.DocumentDate
                            row.Item("Basado Contrato") = If(itemView.IsBasedContract, "Si", "No")
                            If itemView.ContractId IsNot Nothing Then
                                row.Item("Contrato") = itemView.DocumentDate
                            End If
                            row.Item("Proveedor") = itemView.SupplierId.Name
                            row.Item("Tipo Orden") = If(itemView.OrderType = 1, "Productos", "Servicios")
                            row.Item("Detalle") = itemView.Description
                            If itemView.WarehouseId IsNot Nothing Then
                                row.Item("Almacen") = itemView.WarehouseId.CodeName
                            End If
                            row.Item("Producto") = detail.ProductId.CodeName
                            row.Item("Moneda") = itemView.CurrencyAbbreviation
                            row.Item("Cantidad") = detail.Quantity
                            row.Item("Descuento") = detail.DiscountValue
                            row.Item("Subtotal") = detail.SubTotalValue
                            row.Item("IVA") = detail.IvaValue
                            row.Item("Estado") = itemView.StatusName

                            dt.Rows.Add(row)
                        Next
                    Else
                        Dim row As DataRow = dt.NewRow()

                        row.Item("Código") = itemView.Code
                        row.Item("Fecha") = itemView.DocumentDate
                        row.Item("Basado Contrato") = If(itemView.IsBasedContract, "Si", "No")
                        If itemView.ContractId IsNot Nothing Then
                            row.Item("Contrato") = itemView.DocumentDate
                        End If
                        row.Item("Proveedor") = itemView.SupplierId.Name
                        row.Item("Tipo Orden") = If(itemView.OrderType = 1, "Productos", "Servicios")
                        row.Item("Detalle") = itemView.Description
                        row.Item("Moneda") = itemView.CurrencyAbbreviation
                        row.Item("Cantidad") = 1
                        row.Item("Descuento") = itemView.DiscountValue
                        row.Item("Subtotal") = itemView.Value
                        row.Item("IVA") = itemView.IvaValue
                        row.Item("Estado") = itemView.StatusName

                        dt.Rows.Add(row)
                    End If
                Next
                INDGcExportExcell.DataSource = dt
            End If
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
    Private Sub FrmReportPurchaseOrder_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Cargar GridLookUpEdit
        Me.INDGleGroupBy.Properties.DataSource = FillingGroupBy
        Me.INDGleTypeReport.Properties.DataSource = LoadTypeReport
        Me.INDGleStatus.Properties.DataSource = FillingStatus
        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleGroupBy.EditValue = 1
        Me.INDGleTypeReport.EditValue = 1
        Me.INDGleStatus.EditValue = 4
    End Sub


    ''' <summary>
    ''' Se inicializa los miembros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportPurchaseOrder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        INDDeDateStart.Focus()
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _FillingGroupBy = Nothing
        _LoadTypeReport = Nothing
        _FillingStatus = Nothing
    End Sub

#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleDocument
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleDocument_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleDocument.QueryPopUp
        If INDSleDocument.Properties.DataSource Is Nothing Then
            INDSleDocument.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListPurchaseOrderReportByFilter(Nothing)
        End If
    End Sub


    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleSupplier
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleSupplier_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleSupplier.QueryPopUp
        If INDSleSupplier.Properties.DataSource Is Nothing Then

            INDSleSupplier.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListSupplierInventoryReport()

        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleWarehouse
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleWarehouse_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleWarehouse.QueryPopUp
        If INDSleWarehouse.Properties.DataSource Is Nothing Then
            INDSleWarehouse.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListWarehouseReport()
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
        If Me.ValidateControlsReports Then
            If INDGleTypeReport.EditValue = 1 Then
                AsyncLoader(True)

                Dim reporte As New rptReportPurchaseOrder
                reporte.ParametrosReporte = New Object() {INDDeDateStart.EditValue,
                                                          INDDeDateEnd.EditValue,
                                                          _selectorDocument.GetKeys(),
                                                          _selectorSupplier.GetKeys(),
                                                          INDGleGroupBy.EditValue,
                                                          INDGleStatus.EditValue,
                                                          _selectorWarehouse.GetKeys()}
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
                    Me.INDDeDateStart.Focus()
                End If
            Else
                AsyncLoader(True)
                Dim reporte As New rptSubPurchaseOrder
                reporte.ParametrosReporte = New Object() {INDDeDateStart.EditValue,
                                                          INDDeDateEnd.EditValue,
                                                          _selectorDocument.GetKeys(),
                                                          _selectorSupplier.GetKeys(),
                                                          INDGleGroupBy.EditValue,
                                                          INDGleStatus.EditValue,
                                                          _selectorWarehouse.GetKeys()}
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
                    Me.INDDeDateStart.Focus()
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