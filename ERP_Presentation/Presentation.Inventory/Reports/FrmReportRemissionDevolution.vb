#Region "Imports"

Imports Presentation.Reporter
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports DevExpress.XtraGrid.Views.Grid

#End Region

Public Class FrmReportRemissionDevolution

#Region "Datasource"

    Private _FillingDevolutionType As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingDevolutionType As List(Of Tuple(Of Integer, String))
        Get
            If _FillingDevolutionType Is Nothing Then
                _FillingDevolutionType = New List(Of Tuple(Of Integer, String))
                _FillingDevolutionType.Add(New Tuple(Of Integer, String)(1, "Remision Entrada"))
                _FillingDevolutionType.Add(New Tuple(Of Integer, String)(2, "Remision Salida"))
                _FillingDevolutionType.Add(New Tuple(Of Integer, String)(3, "Remision Inventario en Consignación"))
            End If
            Return _FillingDevolutionType
        End Get
    End Property

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
                If INDGleDevolutionType.EditValue = 1 Then
                    _FillingGroupBy.Add(New Tuple(Of Integer, String)(1, "Proveedor"))
                ElseIf INDGleDevolutionType.EditValue = 2 Then
                    _FillingGroupBy.Add(New Tuple(Of Integer, String)(1, "Cliente"))
                ElseIf INDGleDevolutionType.EditValue = 3 Then
                    _FillingGroupBy.Add(New Tuple(Of Integer, String)(1, "Proveedor"))
                End If

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

    Private Property RemissionDevolutionXpo As XPInstantFeedbackSource
        Get
            Return INDSleRemisionDevolutions.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleRemisionDevolutions.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene o establece el datasource de moneda
    ''' </summary>
    ''' <returns></returns>
    Private Property CurrencyXpo As XPInstantFeedbackSource
        Get
            Return INDSleCurrency.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleCurrency.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene o establece el id de la moneda
    ''' </summary>
    ''' <returns></returns>
    Private Property CurrencyId As Integer?
        Get
            Return INDSleCurrency.EditValue
        End Get
        Set(value As Integer?)
            INDSleCurrency.EditValue = value
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

        If INDGleReportType.EditValue = 2 AndAlso Me.CurrencyId Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("CompareRangeDate", "Commons"))
            Me.INDSleCurrency.Focus()
            Validations = False
        End If

        Return Validations
    End Function

#Region "Selector"

    Private _selectorThirdParty As SelectorCache = New SelectorCache("Id", "Nit")
    Private _selectorRemissionDevolution As SelectorCache = New SelectorCache("Id", "Code")

    Private Sub INDGv_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvThirdParties.CustomUnboundColumnData, INDGvRemisionDevolutions.CustomUnboundColumnData
        If e.IsGetData Then
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvThirdParties" Then
                e.Value = _selectorThirdParty.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvRemisionDevolutions" Then
                e.Value = _selectorRemissionDevolution.ValidateExistsRow(e.Row)
            End If
        End If
    End Sub

    Private Sub INDGv_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles INDGvThirdParties.RowCellClick, INDGvRemisionDevolutions.RowCellClick
        If e.Column.FieldName.Contains("UnboundSelection") Then
            Dim selector As SelectorCache = Nothing
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvThirdParties" Then
                selector = _selectorThirdParty
            ElseIf view.Name = "INDGvRemisionDevolutions" Then
                selector = _selectorRemissionDevolution
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

    Private Sub INDSle_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDSleThirdParties.Closed, INDSleRemisionDevolutions.Closed
        Dim searchLookupEdit = TryCast(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        searchLookupEdit.EditValue = Nothing
        If searchLookupEdit.Name = "INDSleThirdParties" Then
            searchLookupEdit.Properties.NullText = _selectorThirdParty.ToString()
        ElseIf searchLookupEdit.Name = "INDSleRemisionDevolutions" Then
            searchLookupEdit.Properties.NullText = _selectorRemissionDevolution.ToString()
        End If
    End Sub

#End Region

#Region "ToExcel"

    ''' <summary>
    ''' creamos un datatable para generar el excel
    ''' </summary>
    ''' <returns></returns>
    Private Sub chargueDatasource()
        Dim IndList = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.LoadDataSourceReportRemissionDevolution(INDDeDateStart.EditValue, INDDeDateEnd.EditValue, INDGleDevolutionType.EditValue, INDCcbeStatus.EditValue, _selectorThirdParty.GetKeys(), _selectorRemissionDevolution.GetKeys())

        Dim dt As New DataTable
        dt.Columns.Add("Código")
        dt.Columns.Add("Fecha", GetType(DateTime))
        dt.Columns.Add("Estado")
        dt.Columns.Add("Tipo Devolución")
        dt.Columns.Add("Remisión")

        dt.Columns.Add("Nit")
        dt.Columns.Add("Tercero")
        dt.Columns.Add("Almacén")
        dt.Columns.Add("Código Producto")
        dt.Columns.Add("Nombre Producto")
        dt.Columns.Add("Causa Devolución de Mercancía")
        dt.Columns.Add("Lote / Serial")
        dt.Columns.Add("Cantidad", GetType(Decimal))
        dt.Columns.Add("Moneda", GetType(String))
        dt.Columns.Add("Valor Neto", GetType(Decimal))
        dt.Columns.Add("Valor Impuestos", GetType(Decimal))
        dt.Columns.Add("Valor Total", GetType(Decimal))

        For Each item In IndList
            For Each detail In item.Inventory_RemissionDevolutionDetails
                Dim row As DataRow = dt.NewRow()
                row.Item("Código") = item.Code
                row.Item("Fecha") = item.RemissionDate.AsDate
                row.Item("Estado") = item.StatusName
                row.Item("Tipo Devolución") = item.DevolutionTypeName
                row.Item("Remisión") = item.RemissionCode
                row.Item("Moneda") = item.CurrencyAbbreviation
                If item.DevolutionType = 1 Then
                    row.Item("Nit") = item.RemissionEntranceId.SupplierId.IdThirdParty.Nit
                    row.Item("Tercero") = item.RemissionEntranceId.SupplierId.IdThirdParty.Name
                    row.Item("Almacén") = item.RemissionEntranceId.WarehouseId.CodeName

                    row.Item("Código Producto") = detail.RemissionEntranceDetailBatchSerialId.RemissionEntranceDetailId.ProductId.Code
                    row.Item("Nombre Producto") = detail.RemissionEntranceDetailBatchSerialId.RemissionEntranceDetailId.ProductId.Name
                    row.Item("Causa Devolución de Mercancía") = If(detail.DevolutionCauseId IsNot Nothing, detail.DevolutionCauseId.Name, String.Empty)
                    row.Item("Lote / Serial") = If(detail.RemissionEntranceDetailBatchSerialId.BatchSerialId IsNot Nothing, detail.RemissionEntranceDetailBatchSerialId.BatchSerialId.BatchCode, String.Empty)
                    row.Item("Cantidad") = detail.Quantity
                    row.Item("Valor Neto") = detail.RemissionEntranceDetailBatchSerialId.RemissionEntranceDetailId.SubTotalValue
                    row.Item("Valor Impuestos") = detail.RemissionEntranceDetailBatchSerialId.RemissionEntranceDetailId.IvaValue
                    row.Item("Valor Total") = detail.RemissionEntranceDetailBatchSerialId.RemissionEntranceDetailId.TotalValue
                ElseIf item.DevolutionType = 2 Then
                    row.Item("Nit") = item.RemissionOutputId.CustomerId.ThirdPartyId.Nit
                    row.Item("Tercero") = item.RemissionOutputId.CustomerId.ThirdPartyId.Name
                    row.Item("Almacén") = item.RemissionOutputId.WarehouseId.CodeName

                    row.Item("Código Producto") = detail.RemissionOutputDetailPhysicalId.RemissionOutputDetailId.ProductId.Code
                    row.Item("Nombre Producto") = detail.RemissionOutputDetailPhysicalId.RemissionOutputDetailId.ProductId.Name
                    row.Item("Causa Devolución de Mercancía") = If(detail.DevolutionCauseId IsNot Nothing, detail.DevolutionCauseId.Name, String.Empty)
                    row.Item("Lote / Serial") = If(detail.RemissionOutputDetailPhysicalId.PhysicalInventoryId.BatchSerialId IsNot Nothing, detail.RemissionOutputDetailPhysicalId.PhysicalInventoryId.BatchSerialId.BatchCode, String.Empty)
                    row.Item("Cantidad") = detail.Quantity
                    row.Item("Valor Neto") = detail.RemissionOutputDetailPhysicalId.RemissionOutputDetailId.TotalPriceWithDiscount
                    row.Item("Valor Impuestos") = 0
                    row.Item("Valor Total") = detail.RemissionOutputDetailPhysicalId.RemissionOutputDetailId.TotalPriceWithDiscount
                ElseIf item.DevolutionType = 3 Then
                    row.Item("Nit") = item.ConsignmentInventoryRemissionId.SupplierId.IdThirdParty.Nit
                    row.Item("Tercero") = item.ConsignmentInventoryRemissionId.SupplierId.IdThirdParty.Name
                    row.Item("Almacén") = item.ConsignmentInventoryRemissionId.WarehouseId.CodeName

                    row.Item("Código Producto") = detail.ConsignmentInventoryRemissionDetailBatchSerialId.ConsignmentInventoryRemissionDetailId.ProductId.Code
                    row.Item("Nombre Producto") = detail.ConsignmentInventoryRemissionDetailBatchSerialId.ConsignmentInventoryRemissionDetailId.ProductId.Name
                    row.Item("Causa Devolución de Mercancía") = If(detail.DevolutionCauseId IsNot Nothing, detail.DevolutionCauseId.Name, String.Empty)
                    row.Item("Lote / Serial") = If(detail.ConsignmentInventoryRemissionDetailBatchSerialId.BatchSerialId IsNot Nothing, detail.ConsignmentInventoryRemissionDetailBatchSerialId.BatchSerialId.BatchCode, String.Empty)
                    row.Item("Cantidad") = detail.Quantity
                    row.Item("Valor Neto") = detail.ConsignmentInventoryRemissionDetailBatchSerialId.ConsignmentInventoryRemissionDetailId.SubTotalValue
                    row.Item("Valor Impuestos") = detail.ConsignmentInventoryRemissionDetailBatchSerialId.ConsignmentInventoryRemissionDetailId.IvaValue
                    row.Item("Valor Total") = detail.ConsignmentInventoryRemissionDetailBatchSerialId.ConsignmentInventoryRemissionDetailId.TotalValue
                End If

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
            Dim fileName As String = System.IO.Path.GetTempFileName() & INDGleDevolutionType.EditValue & ".xlsx"
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
    Private Sub FrmReportRemissionDevolution_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        INDDeDateStart.Focus()

        'Cargar GridLookUpEdit y dar un valor por defecto a los GridLookEdit
        Me.INDGleDevolutionType.Properties.DataSource = FillingDevolutionType
        Me.INDGleDevolutionType.EditValue = 1

        Me.INDGleReportType.Properties.DataSource = FillingReportType
        Me.INDGleReportType.EditValue = 1

        Me.INDGleGroupBy.Properties.DataSource = FillingGroupBy
        Me.INDGleGroupBy.EditValue = 1

        Me.CurrencyId = Me.indigo.OfficialCurrencyId
        Me.INDSleCurrency.Properties.NullText = Me.indigo.CurrencyISO4217
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _FillingDevolutionType = Nothing
        _FillingGroupBy = Nothing

        ThirdPartyXpo = Nothing
        RemissionDevolutionXpo = Nothing

        _selectorThirdParty = Nothing
        _selectorRemissionDevolution = Nothing
    End Sub

#End Region

#Region "QueryPopup"

    Private Sub INDSleThirdParties_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleThirdParties.QueryPopUp
        If ThirdPartyXpo Is Nothing Then
            ThirdPartyXpo = XpoServiceEx.Instance(indigo.TransactionalContainer).CommonService.ListAllThirdParty()
        End If
    End Sub

    Private Sub INDSleRemisionDevolutions_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleRemisionDevolutions.QueryPopUp
        If RemissionDevolutionXpo Is Nothing Then
            Dim criteria = "DevolutionType = " & INDGleDevolutionType.EditValue
            RemissionDevolutionXpo = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListRemissionDevolutionByTypeReport(criteria)
        End If
    End Sub

    Private Sub INDSleCurrency_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleCurrency.QueryPopUp
        If CurrencyXpo Is Nothing Then
            CurrencyXpo = XpoServiceEx.Instance(indigo.TransactionalContainer).CommonService.GetCurrency()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDGleTypeDevolution_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleDevolutionType.EditValueChanged
        _FillingGroupBy = Nothing
        Me.INDGleGroupBy.Properties.DataSource = FillingGroupBy
        Me.INDGleGroupBy.EditValue = 1

        RemissionDevolutionXpo = Nothing
        INDSleRemisionDevolutions.Properties.NullText = String.Empty
        _selectorRemissionDevolution = New SelectorCache("Id", "Code")
    End Sub

    Private Sub INDGleReportType_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleReportType.EditValueChanged
        INDLciGroupBy.Visibility = If(INDGleReportType.EditValue = 1, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        INDLciCurrency.HideControl(INDGleReportType.EditValue = 1)
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
                reporte = New rptReportRemissionDevolution
            ElseIf INDGleReportType.EditValue = 2 Then
                reporte = New rptReportRemissionDevolutionResume
            Else
                AsyncLoader(False)
                Exit Sub
            End If

            reporte.ParametrosReporte = New Object() {INDDeDateStart.EditValue,
                                                      INDDeDateEnd.EditValue,
                                                      INDGleDevolutionType.EditValue,
                                                      INDCcbeStatus.EditValue,
                                                      INDGleGroupBy.EditValue,
                                                      _selectorThirdParty.GetKeys(),
                                                      _selectorRemissionDevolution.GetKeys(),
                                                      Me.CurrencyId}

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