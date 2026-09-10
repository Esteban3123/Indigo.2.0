#Region "Imports"

Imports Presentation.Reporter
Imports DevExpress.Xpo
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports DevExpress.XtraGrid.Views.Grid

#End Region

Public Class FrmReportEntranceVoucherDevolution

#Region "Datasource"

    Private _FillingReportType As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingReportType As List(Of Tuple(Of Integer, String))
        Get
            If _FillingReportType Is Nothing Then
                _FillingReportType = New List(Of Tuple(Of Integer, String))
                _FillingReportType.Add(New Tuple(Of Integer, String)(1, "Resumido"))
                _FillingReportType.Add(New Tuple(Of Integer, String)(2, "Detallado"))
            End If
            Return _FillingReportType
        End Get
    End Property

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

    Private Property ThirdPartyXpo As XPInstantFeedbackSource
        Get
            Return INDSleThirdParties.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleThirdParties.Properties.DataSource = value
        End Set
    End Property

    Private Property EntranceVoucherDevolutionXpo As XPInstantFeedbackSource
        Get
            Return INDSleEntranceVoucherDevolutions.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleEntranceVoucherDevolutions.Properties.DataSource = value
        End Set
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
    Private Function ValidateControlsReports()
        Dim Validations As Boolean = True
        If INDDeDateStart.EditValue IsNot Nothing And INDDeDateEnd.EditValue Is Nothing Or INDDeDateStart.EditValue Is Nothing And INDDeDateEnd.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
            Me.INDDeDateStart.Focus()
            Validations = False
        ElseIf Me.INDDeDateStart.EditValue > INDDeDateEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("CompareRangeDate", "Commons"))
            Me.INDDeDateStart.Focus()
            Validations = False
        End If
        Return Validations
    End Function

#Region "Selector"

    Private _selectorThirdParty As SelectorCache = New SelectorCache("Id", "Nit")
    Private _selectorEntranceVoucherDevolution As SelectorCache = New SelectorCache("Id", "Code")

    Private Sub INDGv_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvThirdParties.CustomUnboundColumnData, INDGvEntranceVoucherDevolutions.CustomUnboundColumnData
        If e.IsGetData Then
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvThirdParties" Then
                e.Value = _selectorThirdParty.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvEntranceVoucherDevolutions" Then
                e.Value = _selectorEntranceVoucherDevolution.ValidateExistsRow(e.Row)
            End If
        End If
    End Sub

    Private Sub INDGv_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles INDGvThirdParties.RowCellClick, INDGvEntranceVoucherDevolutions.RowCellClick
        If e.Column.FieldName.Contains("UnboundSelection") Then
            Dim selector As SelectorCache = Nothing
            Dim view = TryCast(sender, GridView)

            If view.Name = "INDGvThirdParties" Then
                selector = _selectorThirdParty
            ElseIf view.Name = "INDGvEntranceVoucherDevolutions" Then
                selector = _selectorEntranceVoucherDevolution
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

    Private Sub INDSle_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDSleThirdParties.Closed, INDSleEntranceVoucherDevolutions.Closed
        Dim searchLookupEdit = TryCast(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        searchLookupEdit.EditValue = Nothing
        If searchLookupEdit.Name = "INDSleThirdParties" Then
            searchLookupEdit.Properties.NullText = _selectorThirdParty.ToString()
        ElseIf searchLookupEdit.Name = "INDSleEntranceVoucherDevolutions" Then
            searchLookupEdit.Properties.NullText = _selectorEntranceVoucherDevolution.ToString()
        End If
    End Sub

#End Region

#Region "ToExcel"

    ''' <summary>
    ''' creamos un datatable para generar el excel
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargueDatasource()
        Dim IndList = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.LoadDataSourceReportEntranceVoucherDevolution(INDDeDateStart.EditValue, INDDeDateEnd.EditValue, INDCcbeStatus.EditValue, _selectorThirdParty.GetKeys(), _selectorEntranceVoucherDevolution.GetKeys())

        Dim dt As New DataTable
        dt.Columns.Add("Código")
        dt.Columns.Add("Fecha", GetType(DateTime))
        dt.Columns.Add("Estado")

        dt.Columns.Add("Comprobante")
        dt.Columns.Add("Factura")
        dt.Columns.Add("Fecha Factura", GetType(DateTime))
        dt.Columns.Add("Nit")
        dt.Columns.Add("Tercero")
        dt.Columns.Add("Almacén")

        dt.Columns.Add("Código Producto")
        dt.Columns.Add("Nombre Producto")
        dt.Columns.Add("Causa Devolución de Mercancía")
        dt.Columns.Add("Lote / Serial")
        dt.Columns.Add("Cantidad")
        dt.Columns.Add("Moneda")
        dt.Columns.Add("Valor Neto", GetType(Decimal))
        dt.Columns.Add("Valor Impuestos", GetType(Decimal))
        dt.Columns.Add("Valor Total", GetType(Decimal))


        For Each item In IndList
            For Each detail In item.Inventory_EntranceVoucherDevolutionDetails
                Dim row As DataRow = dt.NewRow()
                Dim currencyAbbreviation = If(detail.EntranceVoucherDetailBatchSerialId.EntranceVoucherDetailId.EntranceVoucherId.CurrencyAbbreviation Is Nothing, indigo.CurrencyISO4217, detail.EntranceVoucherDetailBatchSerialId.EntranceVoucherDetailId.EntranceVoucherId.CurrencyAbbreviation)
                row.Item("Código") = item.Code
                row.Item("Fecha") = item.DocumentDate.AsDate
                row.Item("Estado") = item.StatusName

                row.Item("Comprobante") = item.EntranceVoucherId.Code
                row.Item("Factura") = item.EntranceVoucherId.InvoiceNumber
                row.Item("Fecha Factura") = item.EntranceVoucherId.InvoiceDate.AsDate
                row.Item("Nit") = item.EntranceVoucherId.SupplierId.IdThirdParty.Nit
                row.Item("Tercero") = item.EntranceVoucherId.SupplierId.IdThirdParty.Name
                row.Item("Almacén") = item.EntranceVoucherId.WarehouseId.CodeName

                row.Item("Código Producto") = detail.EntranceVoucherDetailBatchSerialId.EntranceVoucherDetailId.ProductId.Code
                row.Item("Nombre Producto") = detail.EntranceVoucherDetailBatchSerialId.EntranceVoucherDetailId.ProductId.Name
                row.Item("Causa Devolución de Mercancía") = If(detail.DevolutionCauseId IsNot Nothing, detail.DevolutionCauseId.Name, String.Empty)
                row.Item("Lote / Serial") = If(detail.EntranceVoucherDetailBatchSerialId.BatchSerialId IsNot Nothing, detail.EntranceVoucherDetailBatchSerialId.BatchSerialId.BatchCode, String.Empty)
                row.Item("Cantidad") = detail.Quantity
                row.Item("Moneda") = currencyAbbreviation
                row.Item("Valor Neto") = detail.EntranceVoucherDetailBatchSerialId.EntranceVoucherDetailId.SubTotalValue
                row.Item("Valor Impuestos") = detail.EntranceVoucherDetailBatchSerialId.EntranceVoucherDetailId.IvaValue
                row.Item("Valor Total") = detail.EntranceVoucherDetailBatchSerialId.EntranceVoucherDetailId.TotalValue

                dt.Rows.Add(row)
            Next
        Next

        INDGcExportExcel.DataSource = dt
    End Sub

    ''' <summary>
    ''' metodo para generar excel
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub generateExcel()
        Dim _gridView = Me.INDGcExportExcel
        If _gridView IsNot Nothing Then
            Dim fileName As String = System.IO.Path.GetTempFileName() & INDGleReportType.EditValue & ".xlsx"
            Dim param As New DevExpress.XtraPrinting.XlsxExportOptions(DevExpress.XtraPrinting.TextExportMode.Value, True, False)
            _gridView.ExportToXlsx(fileName, param)
            If System.IO.File.Exists(fileName) Then
                System.Diagnostics.Process.Start(fileName)
            End If
        End If
        Me.INDGcExportExcel.DataSource = Nothing
        Me.INDGcExportExcel.RefreshDataSource()
    End Sub

#End Region

#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' Se inicializa los miembros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportEntranceVoucherDevolution_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        INDDeDateStart.Focus()

        'Cargar GridLookUpEdit y dar un valor por defecto a los GridLookEdit
        Me.INDGleReportType.Properties.DataSource = FillingReportType
        Me.INDGleReportType.EditValue = 1

        Me.INDGleGroupBy.Properties.DataSource = FillingGroupBy
        Me.INDGleGroupBy.EditValue = 1
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _FillingReportType = Nothing
        _FillingGroupBy = Nothing

        ThirdPartyXpo = Nothing
        EntranceVoucherDevolutionXpo = Nothing

        _selectorThirdParty = Nothing
        _selectorEntranceVoucherDevolution = Nothing
    End Sub

#End Region

#Region "QueryPopup"

    Private Sub INDSleThirdParties_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleThirdParties.QueryPopUp
        If ThirdPartyXpo Is Nothing Then
            ThirdPartyXpo = XpoServiceEx.Instance(indigo.TransactionalContainer).CommonService.ListAllThirdParty()
        End If
    End Sub

    Private Sub INDSleEntranceVoucherDevolutions_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleEntranceVoucherDevolutions.QueryPopUp
        If EntranceVoucherDevolutionXpo Is Nothing Then
            EntranceVoucherDevolutionXpo = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListEntranceVoucherDevolution()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' se ejecuta en el evento EditValueChanged del control INDGleTypeDevolution
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGleReportType_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleReportType.EditValueChanged
        INDLciGroupBy.Visibility = If(INDGleReportType.EditValue = 1, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
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
            If INDGleReportType.EditValue = 1 Then
                reporte = New rptReportEntranceVoucherDevolution
            ElseIf INDGleReportType.EditValue = 2 Then
                reporte = New rptReportEntranceVoucherDevolutionResume
            Else
                AsyncLoader(False)
                Exit Sub
            End If

            reporte.ParametrosReporte = New Object() {INDDeDateStart.EditValue,
                                                      INDDeDateEnd.EditValue,
                                                      INDCcbeStatus.EditValue,
                                                      INDGleGroupBy.EditValue,
                                                      _selectorThirdParty.GetKeys(),
                                                      _selectorEntranceVoucherDevolution.GetKeys()}

            INDDvDocumentViewer.DocumentSource = reporte
            Await reporte.CargarDataSourceAsync()
            AsyncLoader(False)
            If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                reporte.CreateDocument(True)
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
    Private Async Sub INDSbGenerateExcel_Click(sender As Object, e As EventArgs) Handles INDSbGenerateExcel.Click
        If Me.ValidateControlsReports = True Then
            Try
                AsyncLoader(True)
                Await Task.Factory.StartNew(AddressOf chargueDatasource)

                If Me.INDGcExportExcel.DataSource IsNot Nothing Then
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