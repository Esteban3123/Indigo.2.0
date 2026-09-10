#Region "Imports"

Imports Presentation.Reporter
Imports DevExpress.Xpo
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports DevExpress.XtraGrid.Views.Grid

#End Region

Public Class FrmReportProductBarcode

#Region "Datasource"

    Private _FillingIncludeZero As List(Of Tuple(Of Boolean, String))
    Private ReadOnly Property FillingIncludeZero As List(Of Tuple(Of Boolean, String))
        Get
            If _FillingIncludeZero Is Nothing Then
                _FillingIncludeZero = New List(Of Tuple(Of Boolean, String))
                _FillingIncludeZero.Add(New Tuple(Of Boolean, String)(False, "No"))
                _FillingIncludeZero.Add(New Tuple(Of Boolean, String)(True, "Si"))
            End If
            Return _FillingIncludeZero
        End Get
    End Property

    Private _FillingTypeGenerateCode As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingTypeGenerateCode As List(Of Tuple(Of Integer, String))
        Get
            If _FillingTypeGenerateCode Is Nothing Then
                _FillingTypeGenerateCode = New List(Of Tuple(Of Integer, String))
                _FillingTypeGenerateCode.Add(New Tuple(Of Integer, String)(1, "Ninguno"))
                _FillingTypeGenerateCode.Add(New Tuple(Of Integer, String)(2, "Código de Barra"))
                _FillingTypeGenerateCode.Add(New Tuple(Of Integer, String)(3, "Código QR"))
            End If
            Return _FillingTypeGenerateCode
        End Get
    End Property

    Private Property WareHouseXpo As XPInstantFeedbackSource
        Get
            Return INDSleWarehouses.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleWarehouses.Properties.DataSource = value
        End Set
    End Property

    Private Property ProductXpo As XPInstantFeedbackSource
        Get
            Return INDSleProducts.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleProducts.Properties.DataSource = value
        End Set
    End Property

    Private Property BatchSerialXpo As XPInstantFeedbackSource
        Get
            Return INDSleBatchSerials.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleBatchSerials.Properties.DataSource = value
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


        Return Validations
    End Function

#Region "Selector"

    Private _selectorWarehouse As SelectorCache = New SelectorCache("Id", "Code")
    Private _selectorProduct As SelectorCache = New SelectorCache("Id", "Code")
    Private _selectorBatchSerial As SelectorCache = New SelectorCache("Id", "BatchCode")

    Private Sub INDGv_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvWarehouses.CustomUnboundColumnData, INDGvProducts.CustomUnboundColumnData, INDGvBatchSerials.CustomUnboundColumnData
        If e.IsGetData Then
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvWarehouses" Then
                e.Value = _selectorWarehouse.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvProducts" Then
                e.Value = _selectorProduct.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvBatchSerials" Then
                e.Value = _selectorBatchSerial.ValidateExistsRow(e.Row)
            End If
        End If
    End Sub

    Private Sub INDGv_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles INDGvWarehouses.RowCellClick, INDGvProducts.RowCellClick, INDGvBatchSerials.RowCellClick
        If e.Column.FieldName.Contains("UnboundSelection") Then
            Dim selector As SelectorCache = Nothing
            Dim view = TryCast(sender, GridView)

            If view.Name = "INDGvWarehouses" Then
                selector = _selectorWarehouse
            ElseIf view.Name = "INDGvProducts" Then
                selector = _selectorProduct
            ElseIf view.Name = "INDGvBatchSerials" Then
                selector = _selectorBatchSerial
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

    Private Sub INDSle_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDSleWarehouses.Closed, INDSleProducts.Closed, INDSleBatchSerials.Closed
        Dim searchLookupEdit = TryCast(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        searchLookupEdit.EditValue = Nothing
        If searchLookupEdit.Name = "INDSleWarehouses" Then
            searchLookupEdit.Properties.NullText = _selectorWarehouse.ToString()
        ElseIf searchLookupEdit.Name = "INDSleProducts" Then
            searchLookupEdit.Properties.NullText = _selectorProduct.ToString()
        ElseIf searchLookupEdit.Name = "INDSleBatchSerials" Then
            searchLookupEdit.Properties.NullText = _selectorBatchSerial.ToString()
        End If
    End Sub

#End Region

#Region "ToExcel"

    ''' <summary>
    ''' creamos un datatable para generar el excel
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargueDatasource()
        Dim IndList = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.LoadDataSourceReportProductBarcode(_selectorWarehouse.GetKeys(), _selectorProduct.GetKeys(), _selectorBatchSerial.GetKeys(), INDGleIncludeZero.EditValue)

        Dim dt As New DataTable
        dt.Columns.Add("Almacen")
        dt.Columns.Add("Código")
        dt.Columns.Add("Descripción")
        dt.Columns.Add("Lote")
        dt.Columns.Add("Fecha Vencimiento", GetType(DateTime))
        dt.Columns.Add("Cantidad", GetType(Decimal))
        dt.Columns.Add("Código de Barra")
        dt.Columns.Add("Código Generado")

        For Each item In IndList
            Dim row As DataRow = dt.NewRow()
            row.Item("Almacen") = item.WarehouseCodeName
            row.Item("Código") = item.ProductCode
            row.Item("Descripción") = item.ProducName
            row.Item("Lote") = item.BatchCode
            row.Item("Fecha Vencimiento") = item.ExpirationDate
            row.Item("Cantidad") = item.Quantity
            row.Item("Código de Barra") = item.Barcode
            row.Item("Código Generado") = item.CalculatedBarCode
            dt.Rows.Add(row)
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
            Dim fileName As String = System.IO.Path.GetTempFileName() & INDGleIncludeZero.EditValue & ".xlsx"
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
    Private Sub FrmReportProductBarcode_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Cargar GridLookUpEdit y dar un valor por defecto a los GridLookEdit
        Me.INDGleIncludeZero.Properties.DataSource = FillingIncludeZero
        Me.INDGleIncludeZero.EditValue = False

        Me.INDGleTypeGenerateCode.Properties.DataSource = FillingTypeGenerateCode
        Me.INDGleTypeGenerateCode.EditValue = 1
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _FillingIncludeZero = Nothing
        _FillingTypeGenerateCode = Nothing

        WareHouseXpo = Nothing
        ProductXpo = Nothing
        BatchSerialXpo = Nothing

        _selectorWarehouse = Nothing
        _selectorProduct = Nothing
        _selectorBatchSerial = Nothing
    End Sub

#End Region

#Region "QueryPopup"

    Private Sub INDSleWarehouses_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleWarehouses.QueryPopUp
        If WareHouseXpo Is Nothing Then
            WareHouseXpo = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListOwnWarehouseByStatusAndUser(True, Nothing)
        End If
    End Sub

    Private Sub INDSleProducts_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleProducts.QueryPopUp
        If ProductXpo Is Nothing Then
            ProductXpo = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListProductsReport()
        End If
    End Sub

    Private Sub INDSleBatchSerials_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleBatchSerials.QueryPopUp
        If BatchSerialXpo Is Nothing Then
            BatchSerialXpo = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListBatchSerialReport()
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
            Dim reporte As New rptReportProductBarcode
            reporte.ParametrosReporte = New Object() {_selectorWarehouse.GetKeys(),
                                                      _selectorProduct.GetKeys(),
                                                      _selectorBatchSerial.GetKeys(),
                                                      INDGleIncludeZero.EditValue,
                                                      INDGleTypeGenerateCode.EditValue}

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