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
Imports Infrastructure.Data.Xpo

#End Region


Public Class FrmReportKardex

#Region "Fields"


#End Region

#Region "Datasource"
    Public Property ProoftCloseXpoProducts As XPInstantFeedbackSource
    Public Property ProoftCloseXpoWarehouse As XPInstantFeedbackSource
    Public Property ProoftCloseXpoBatchSerial As XPInstantFeedbackSource

    Private _FillingTypeReport As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingTypeReport As List(Of Tuple(Of Integer, String))
        Get
            If _FillingTypeReport Is Nothing Then
                _FillingTypeReport = New List(Of Tuple(Of Integer, String))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(1, "Producto - Costo"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(2, "Almacen - Cantidad"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(3, "Lote/Serial - Cantidad"))
            End If
            Return _FillingTypeReport
        End Get
    End Property

    Private _FillingDateReport As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingDateReport As List(Of Tuple(Of Integer, String))
        Get
            If _FillingDateReport Is Nothing Then
                _FillingDateReport = New List(Of Tuple(Of Integer, String))
                _FillingDateReport.Add(New Tuple(Of Integer, String)(1, "Fecha Creación"))
                _FillingDateReport.Add(New Tuple(Of Integer, String)(2, "Fecha Documento"))
            End If
            Return _FillingDateReport
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

        Return Validations
    End Function

#Region "selector"

    ''' <summary>
    ''' funcionamiento del Multiselect
    ''' </summary>
    Private _selectorProduct As SelectorCache = New SelectorCache("Id", "Code")
    Private _selectorBatchSerial As SelectorCache = New SelectorCache("Id", "BatchCode")
    Private _selectorWarehouse As SelectorCache = New SelectorCache("Id", "Code")

    Private Sub INDGvSelector_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvProduct.CustomUnboundColumnData, INDGvWarehouse.CustomUnboundColumnData, INDGvBatchSerial.CustomUnboundColumnData
        If e.IsGetData Then
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvProduct" Then
                e.Value = _selectorProduct.ValidateExistsRow(e.Row)

            ElseIf view.Name = "INDGvBatchSerial" Then
                e.Value = _selectorBatchSerial.ValidateExistsRow(e.Row)

            ElseIf view.Name = "INDGvWarehouse" Then
                e.Value = _selectorWarehouse.ValidateExistsRow(e.Row)
            End If
        End If
    End Sub

    Private Sub INDGvSelector_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles INDGvProduct.RowCellClick, INDGvWarehouse.RowCellClick, INDGvBatchSerial.RowCellClick
        If e.Column.FieldName.Contains("UnboundSelection") Then
            Dim selector As SelectorCache = Nothing
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvProduct" Then
                selector = _selectorProduct

            ElseIf view.Name = "INDGvBatchSerial" Then
                selector = _selectorBatchSerial

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

    Private Sub INDGvSelector_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDSleProduct.Closed, INDSleWarehouse.Closed, INDSleBatchSerial.Closed
        Dim searchLookupEdit = TryCast(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        searchLookupEdit.EditValue = Nothing
        If searchLookupEdit.Name = "INDSleProduct" Then
            searchLookupEdit.Properties.NullText = _selectorProduct.ToString()

        ElseIf searchLookupEdit.Name = "INDSleBatchSerial" Then
            searchLookupEdit.Properties.NullText = _selectorBatchSerial.ToString()

        ElseIf searchLookupEdit.Name = "INDSleWarehouse" Then
            searchLookupEdit.Properties.NullText = _selectorWarehouse.ToString()

        End If
    End Sub

#End Region

#Region "ToExcel"



    ''' <summary>
    ''' metodo para generar excel
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GenerateExcel()
        Dim _gridView = INDgcExportExcel
        If _gridView IsNot Nothing Then
            Dim fileName As String = System.IO.Path.GetTempFileName() & INDGleTypeReport.EditValue & ".xlsx"
            Dim param As New DevExpress.XtraPrinting.XlsxExportOptions(DevExpress.XtraPrinting.TextExportMode.Value, True, False)
            _gridView.ExportToXlsx(fileName, param)
            If System.IO.File.Exists(fileName) Then
                System.Diagnostics.Process.Start(fileName)
            End If
        End If
        INDgcExportExcel.DataSource = Nothing
        INDgcExportExcel.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Método para ocultar/mostrar las columnas de la rejilla de exportación a excel
    ''' </summary>
    Private Sub ShowHideColumnsGridExportExcel()
        If INDGleTypeReport.EditValue IsNot Nothing Then
            If INDGleTypeReport.EditValue = 1 Then 'si tipo de reporte es igual a producto - costo 
                INDcolProduct.Visible = True
                INDcolWarehouse.Visible = True
                INDcolBatch.Visible = True
                INDcolExpirationDate.Visible = True
                INDcolDate.Visible = True
                INDcolDate.FieldName = If(INDGleDateReport.EditValue = 1, "CreationDate", "DocumentDate")
                INDcolDocumentName.Visible = True
                INDcolPatient.Visible = True
                INDcolPreviousCost.Visible = True
                INDcolValue.Visible = True
                INDcolInitialBalance.Visible = True
                INDcolInitialBalance.FieldName = "PreviousAverageCost"
                INDcolEntrance.Visible = True
                INDcolExit.Visible = True
                INDcolStock.Visible = True
                INDcolAverageCost.Visible = True
                INDcolTotalCost.Visible = True

                INDcolProduct.VisibleIndex = 0
                INDcolWarehouse.VisibleIndex = 1
                INDcolBatch.VisibleIndex = 2
                INDcolExpirationDate.VisibleIndex = 3
                INDcolDate.VisibleIndex = 4
                INDcolDocumentName.VisibleIndex = 5
                INDcolPatient.VisibleIndex = 6
                INDcolPreviousCost.VisibleIndex = 7
                INDcolValue.VisibleIndex = 8
                INDcolInitialBalance.VisibleIndex = 9
                INDcolEntrance.VisibleIndex = 10
                INDcolExit.VisibleIndex = 11
                INDcolStock.VisibleIndex = 12
                INDcolAverageCost.VisibleIndex = 13
                INDcolTotalCost.VisibleIndex = 14
            ElseIf INDGleTypeReport.EditValue = 2 Then 'si tipo de reporte es igual a almacen - cantidad
                INDcolProduct.Visible = True
                INDcolWarehouse.Visible = True
                INDcolBatch.Visible = True
                INDcolExpirationDate.Visible = True
                INDcolDate.Visible = True
                INDcolDate.FieldName = If(INDGleDateReport.EditValue = 1, "CreationDate", "DocumentDate")
                INDcolDocumentName.Visible = True
                INDcolPatient.Visible = True
                INDcolPreviousCost.Visible = False
                INDcolValue.Visible = False
                INDcolInitialBalance.Visible = True
                INDcolInitialBalance.FieldName = "PreviousAmountWarehouse"
                INDcolEntrance.Visible = True
                INDcolExit.Visible = True
                INDcolStock.Visible = True
                INDcolAverageCost.Visible = False
                INDcolTotalCost.Visible = False

                INDcolProduct.VisibleIndex = 0
                INDcolWarehouse.VisibleIndex = 1
                INDcolBatch.VisibleIndex = 2
                INDcolExpirationDate.VisibleIndex = 3
                INDcolDate.VisibleIndex = 4
                INDcolDocumentName.VisibleIndex = 5
                INDcolPatient.VisibleIndex = 6
                INDcolPreviousCost.VisibleIndex = -1
                INDcolValue.VisibleIndex = -1
                INDcolInitialBalance.VisibleIndex = 7
                INDcolEntrance.VisibleIndex = 8
                INDcolExit.VisibleIndex = 9
                INDcolStock.VisibleIndex = 10
                INDcolAverageCost.VisibleIndex = -1
                INDcolTotalCost.VisibleIndex = -1
            ElseIf INDGleTypeReport.EditValue = 3 Then 'si tipo de reporte es igual a lote/serial - cantidad
                INDcolProduct.Visible = True
                INDcolWarehouse.Visible = True
                INDcolBatch.Visible = True
                INDcolExpirationDate.Visible = True
                INDcolDate.Visible = True
                INDcolDate.FieldName = If(INDGleDateReport.EditValue = 1, "CreationDate", "DocumentDate")
                INDcolDocumentName.Visible = True
                INDcolPatient.Visible = True
                INDcolPreviousCost.Visible = False
                INDcolValue.Visible = False
                INDcolInitialBalance.Visible = True
                INDcolInitialBalance.FieldName = "PreviousAmountBatch"
                INDcolEntrance.Visible = True
                INDcolExit.Visible = True
                INDcolStock.Visible = True
                INDcolAverageCost.Visible = False
                INDcolTotalCost.Visible = False

                INDcolProduct.VisibleIndex = 0
                INDcolWarehouse.VisibleIndex = 1
                INDcolBatch.VisibleIndex = 2
                INDcolExpirationDate.VisibleIndex = 3
                INDcolDate.VisibleIndex = 4
                INDcolDocumentName.VisibleIndex = 5
                INDcolPatient.VisibleIndex = 6
                INDcolPreviousCost.VisibleIndex = -1
                INDcolValue.VisibleIndex = -1
                INDcolInitialBalance.VisibleIndex = 7
                INDcolEntrance.VisibleIndex = 8
                INDcolExit.VisibleIndex = 9
                INDcolStock.VisibleIndex = 10
                INDcolAverageCost.VisibleIndex = -1
                INDcolTotalCost.VisibleIndex = -1
            End If
        End If
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
    Private Sub FrmReportKardex_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Cargar GridLookUpEdit
        Me.INDGleTypeReport.Properties.DataSource = FillingTypeReport
        Me.INDGleDateReport.Properties.DataSource = FillingDateReport
        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleTypeReport.EditValue = 1
        Me.INDGleDateReport.EditValue = 1
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _FillingTypeReport = Nothing
        _FillingDateReport = Nothing
    End Sub
    ''' <summary>
    '''  Se inicializa los miembros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportKardex_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        INDDeDateStart.Focus()
    End Sub

#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleProduct
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleProduct_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleProduct.QueryPopUp
        If INDSleProduct.Properties.DataSource Is Nothing Then
            INDSleProduct.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListProductsReport()
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

    '

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleBatchSerial
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleBatchSerial_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleBatchSerial.QueryPopUp
        If INDSleBatchSerial.Properties.DataSource Is Nothing Then
            INDSleBatchSerial.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListBatchSerialReport()

        End If
    End Sub


#End Region

#Region "EditValueChange"
    ''' <summary>
    ''' se ejecuta en el evento EditValueChanged del control INDGleTypeReport
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGleTypeReport_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleTypeReport.EditValueChanged
        If INDGleTypeReport.EditValue = 1 Then
            'si tipo de reporte es igual a producto - costo mostrar el filtro por productos
            INDLciWarehouse.HideControl()
            INDLciBatchSerial.HideControl()
            INDcolInitialBalance.DisplayFormat.FormatString = "C2"



            'vaciar campos
            _selectorWarehouse.Clear()
            INDSleWarehouse.Text = ""
            _selectorBatchSerial.Clear()
            INDSleBatchSerial.Text = ""

        ElseIf INDGleTypeReport.EditValue = 2 Then
            'si tipo de reporte es igual a almacen - cantidad mostrar el filtro por productos y por almacenes
            INDLciWarehouse.HideControl(False)

            INDLciBatchSerial.HideControl()
            INDcolInitialBalance.DisplayFormat.FormatString = "n0"


            'vaciar campos
            _selectorBatchSerial.Clear()
            INDSleBatchSerial.Text = ""

        ElseIf INDGleTypeReport.EditValue = 3 Then
            'si tipo de reporte es igual a Lote - cantidad mostrar el filtro por productos, por almacenes y por lote
            INDLciWarehouse.HideControl(False)

            INDLciBatchSerial.HideControl(False)
            INDcolInitialBalance.DisplayFormat.FormatString = "n0"
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
    Private Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports = True Then
            AsyncLoader(True)
            Dim reporte As New rptReportKardex
            reporte.ParametrosReporte = New Object() {INDDeDateStart.EditValue,
                                                      INDDeDateEnd.EditValue,
                                                      _selectorProduct.GetKeys(),
                                                      _selectorWarehouse.GetKeys(),
                                                      _selectorBatchSerial.GetKeys(),
                                                      INDGleTypeReport.EditValue,
                                                      INDGleDateReport.EditValue}
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
    ''' se ejecuta al dar click en el control INDCnBack
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnBack_ClickBack() Handles INDCnBack.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncNavigation.Visible = True
        Me.INDPcDocumentViewer.Visible = False
    End Sub


    ''' <summary>
    ''' Se ejecuta al presionar click sobre el botón de excel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSbGenerateExcell_Click(sender As Object, e As EventArgs) Handles INDSbGenerateExcell.Click
        If Me.ValidateControlsReports = True Then
            Try
                AsyncLoader(True)
                Dim filter As String = Nothing

                ShowHideColumnsGridExportExcel()
                Dim orderBy As String = If(INDGleDateReport.EditValue = 1, "CreationDate", "DocumentDate")

                If INDDeDateStart.EditValue IsNot Nothing AndAlso INDDeDateEnd.EditValue IsNot Nothing Then
                    Dim fechaInicio As Date = CDate(INDDeDateStart.EditValue).Date
                    Dim fechaFinExclusivo As Date = CDate(INDDeDateEnd.EditValue).Date.AddDays(1).AddSeconds(-1)

                    filter = String.Format("{0} >= #{1:yyyy-MM-dd HH:mm:ss}# AND {0} <= #{2:yyyy-MM-dd HH:mm:ss}#", orderBy, fechaInicio, fechaFinExclusivo)

                End If
                'Filtro por producto
                If Not String.IsNullOrEmpty(_selectorProduct.GetKeys()) Then
                    filter &= If(String.IsNullOrEmpty(filter), "", " AND ") & String.Format("ProductId IN ({0})", _selectorProduct.GetKeys())
                End If

                'Filtro por Almacen
                If Not String.IsNullOrEmpty(_selectorWarehouse.GetKeys()) Then
                    filter &= If(String.IsNullOrEmpty(filter), "", " AND ") & String.Format("WarehouseId IN ({0})", _selectorWarehouse.GetKeys())
                End If

                'FILTRO POR LOTE
                If Not String.IsNullOrEmpty(_selectorBatchSerial.GetKeys()) Then
                    filter &= If(String.IsNullOrEmpty(filter), "", " AND ") & String.Format("BatchSerialId IN ({0})", _selectorBatchSerial.GetKeys())
                End If

                Dim listKardex As XPCollection(Of InventoryViewReportKardexXpo) = Nothing
                Await Task.Factory.StartNew(Sub()
                                                listKardex = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).InventoryService.ListKardex(Filter, orderBy)
                                            End Sub)

                If listKardex IsNot Nothing AndAlso listKardex.Count > 0 Then
                    If INDGleTypeReport.EditValue = 1 Then
                        Me.INDcolStock.FieldName = "PreviousAmountProductCalculated"
                    ElseIf INDGleTypeReport.EditValue = 2 Then
                        Me.INDcolStock.FieldName = "PreviousAmountWarehouseCalculated"
                    ElseIf INDGleTypeReport.EditValue = 3 Then
                        Me.INDcolStock.FieldName = "PreviousAmountBatchCalculated"
                    End If

                    INDgcExportExcel.DataSource = listKardex
                    GenerateExcel()
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