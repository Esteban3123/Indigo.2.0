#Region "Imports"
Imports DevExpress.Xpo
Imports DevExpress.XtraGrid.Views.Grid
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Presentation.Base
Imports Presentation.CloudAgent
Imports Presentation.Controls.MVP
Imports Presentation.Reporter

#End Region

Public Class FrmReportAverageConsumptionTransfer
#Region "Datasource"
    Private _FillingOrderType As List(Of Tuple(Of Integer, String))
    ''' <summary>
    ''' se asignan los valores para el gridview
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property FillingOrderType As List(Of Tuple(Of Integer, String))
        Get
            If _FillingOrderType Is Nothing Then
                _FillingOrderType = New List(Of Tuple(Of Integer, String))
                _FillingOrderType.Add(New Tuple(Of Integer, String)(1, "Traslados"))
                _FillingOrderType.Add(New Tuple(Of Integer, String)(2, "Consumos"))
                _FillingOrderType.Add(New Tuple(Of Integer, String)(3, "Ambos"))
            End If
            Return _FillingOrderType
        End Get
    End Property

    Private _FillingReportType As List(Of Tuple(Of Integer, String))
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
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

    Private _FillingOfficeFilter As List(Of Tuple(Of Integer, String))
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property FillingOfficeFilter As List(Of Tuple(Of Integer, String))
        Get
            If _FillingOfficeFilter Is Nothing Then
                _FillingOfficeFilter = New List(Of Tuple(Of Integer, String))
                _FillingOfficeFilter.Add(New Tuple(Of Integer, String)(1, "Almacen"))
                _FillingOfficeFilter.Add(New Tuple(Of Integer, String)(2, "Unidad Funcional"))
                _FillingOfficeFilter.Add(New Tuple(Of Integer, String)(3, "Ambos"))
            End If
            Return _FillingOfficeFilter
        End Get
    End Property
#End Region

#Region "BarraBotones"

#End Region

#Region "Methods"

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



    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim _mensaje As String = String.Empty
        Dim control As Byte = 0
        If INDDateStart.EditValue Is Nothing Or INDDateEnd.EditValue Is Nothing Then
            _mensaje = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
            control = 1
            'Me.INDDateStart.Focus()
        ElseIf Me.INDDateStart.EditValue > INDDateEnd.EditValue Then
            _mensaje = String.Format("{0}{1}{2}", _mensaje, Environment.NewLine, ResourceManager.GetString("CompareRangeDate", "Commons"))
            If control = 0 Then control = 1
            'Me.INDDateStart.Focus()
        End If
        If Me.INDGleOrderType.EditValue Is Nothing Then
            _mensaje = String.Format("{0}{1}{2}", _mensaje, Environment.NewLine, "Seleccione tipo de orden.")
            If control = 0 Then control = 2
        End If
        If Me.INDGleOrderType.EditValue IsNot Nothing AndAlso Me.INDGleOrderType.EditValue = 2 AndAlso Me.INDGleOfficeFilter.EditValue Is Nothing Then
            _mensaje = String.Format("{0}{1}{2}", _mensaje, Environment.NewLine, "Seleccione despachado a.")
            If control = 0 Then control = 3
        End If
        If Me.INDGleReportType.EditValue Is Nothing Then
            _mensaje = String.Format("{0}{1}{2}", _mensaje, Environment.NewLine, "Seleccione tipo de reporte.")
            If control = 0 Then control = 4
        End If

        If Not String.IsNullOrEmpty(_mensaje) Then
            Mensaje(EeventViewerImages.Advertencia) = _mensaje

            Select Case control
                Case 1
                    Me.INDDateStart.Focus()
                Case 2
                    Me.INDGleOrderType.Focus()
                Case 3
                    Me.INDGleOfficeFilter.Focus()
                Case 4
                    Me.INDGleReportType.Focus()
            End Select
        End If

        Return String.IsNullOrEmpty(_mensaje)

    End Function


#Region "Selector"


    ''' <summary>
    ''' funcionamiento del Multiselect
    ''' </summary>
    Private _selectorOriginStore As SelectorCache = New SelectorCache("Id", "Code")
    Private _selectorDestinationStore As SelectorCache = New SelectorCache("Id", "Code")
    Private _selectorFunctionalUnit As SelectorCache = New SelectorCache("Id", "Codigo")
    Private _selectorProduct As SelectorCache = New SelectorCache("Id", "Code")


    Private Sub INDGvSelector_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvOriginStore.CustomUnboundColumnData, INDGvDestinationStore.CustomUnboundColumnData, INDGvFunctionalUnit.CustomUnboundColumnData, INDGvProduct.CustomUnboundColumnData
        If e.IsGetData Then
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvOriginStore" Then
                e.Value = _selectorOriginStore.ValidateExistsRow(e.Row)

            ElseIf view.Name = "INDGvDestinationStore" Then
                e.Value = _selectorDestinationStore.ValidateExistsRow(e.Row)

            ElseIf view.Name = "INDGvFunctionalUnit" Then
                e.Value = _selectorFunctionalUnit.ValidateExistsRow(e.Row)

            ElseIf view.Name = "INDGvProduct" Then
                e.Value = _selectorProduct.ValidateExistsRow(e.Row)
            End If
        End If
    End Sub

    Private Sub INDGvSelector_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles INDGvOriginStore.RowCellClick, INDGvDestinationStore.RowCellClick, INDGvFunctionalUnit.RowCellClick, INDGvProduct.RowCellClick
        If e.Column.FieldName.Contains("UnboundSelection") Then
            Dim selector As SelectorCache = Nothing
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvOriginStore" Then
                selector = _selectorOriginStore

            ElseIf view.Name = "INDGvDestinationStore" Then
                selector = _selectorDestinationStore

            ElseIf view.Name = "INDGvFunctionalUnit" Then
                selector = _selectorFunctionalUnit

            ElseIf view.Name = "INDGvProduct" Then
                selector = _selectorProduct

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

    Private Sub INDGvSelector_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDSleOriginStore.Closed, INDSleDestinationStore.Closed, INDSleFunctionalUnit.Closed, INDSleProduct.Closed
        Dim searchLookupEdit = TryCast(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        searchLookupEdit.EditValue = Nothing
        If searchLookupEdit.Name = "INDSleOriginStore" Then
            searchLookupEdit.Properties.NullText = _selectorOriginStore.ToString()

        ElseIf searchLookupEdit.Name = "INDSleDestinationStore" Then
            searchLookupEdit.Properties.NullText = _selectorDestinationStore.ToString()

        ElseIf searchLookupEdit.Name = "INDSleFunctionalUnit" Then
            searchLookupEdit.Properties.NullText = _selectorFunctionalUnit.ToString()

        ElseIf searchLookupEdit.Name = "INDSleProduct" Then
            searchLookupEdit.Properties.NullText = _selectorProduct.ToString()

        End If
    End Sub

#End Region

#Region "ToExcel"

    ''' <summary>
    ''' creamos un datatable para generar el excel detallado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function CargueDatasource(ByVal _gridControl As DevExpress.XtraGrid.GridControl) As Task


        Dim parametros As String = RecuperarParametros()
        Dim ds As DataSet = Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.ListAverageConsumptionTransferAsync(parametros, Me.IndigoSessionValues)
        Dim dt = ds.Tables("Inventory_SP_AverageConsumptionTransfer")
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            dt.Columns.Remove("ProductId")
            dt.Columns.Remove("TargetId")
            dt.Columns.Remove("SourceWarehouseId")
            dt.Columns.Remove("OrderType")
            dt.Columns.Remove("MeasurementUnitId")
            dt.Columns.Remove("ATCId")
            dt.Columns.Remove("TargetTypeId")
            dt.Columns.Remove("OrderTypeName")
            If INDGleReportType.EditValue = 1 Then 'RESUMIDO
                dt.Columns.Remove("ENE")
                dt.Columns.Remove("FEB")
                dt.Columns.Remove("MAR")
                dt.Columns.Remove("ABR")
                dt.Columns.Remove("MAY")
                dt.Columns.Remove("JUN")
                dt.Columns.Remove("JUL")
                dt.Columns.Remove("AGO")
                dt.Columns.Remove("SEP")
                dt.Columns.Remove("OCT")
                dt.Columns.Remove("NOV")
                dt.Columns.Remove("DIC")
                dt.Columns("ProductCode").SetOrdinal(0)
                dt.Columns("ProductName").SetOrdinal(1)
                dt.Columns("MeasurementUnitCode").SetOrdinal(2)
                dt.Columns("MeasurementUnitName").SetOrdinal(3)
                dt.Columns("ATCConcentration").SetOrdinal(4)
                dt.Columns("SourceCode").SetOrdinal(5)
                dt.Columns("SourceName").SetOrdinal(6)
                dt.Columns("TargetTypeName").SetOrdinal(7)
                dt.Columns("TargetCode").SetOrdinal(8)
                dt.Columns("TargetName").SetOrdinal(9)
                dt.Columns("ANIO").SetOrdinal(10)
                dt.Columns("Average").SetOrdinal(11)
            Else
                dt.Columns("ProductCode").SetOrdinal(0)
                dt.Columns("ProductName").SetOrdinal(1)
                dt.Columns("MeasurementUnitCode").SetOrdinal(2)
                dt.Columns("MeasurementUnitName").SetOrdinal(3)
                dt.Columns("ATCConcentration").SetOrdinal(4)
                dt.Columns("SourceCode").SetOrdinal(5)
                dt.Columns("SourceName").SetOrdinal(6)
                dt.Columns("TargetTypeName").SetOrdinal(7)
                dt.Columns("TargetCode").SetOrdinal(8)
                dt.Columns("TargetName").SetOrdinal(9)
                dt.Columns("ANIO").SetOrdinal(10)
                dt.Columns("ENE").SetOrdinal(11)
                dt.Columns("FEB").SetOrdinal(12)
                dt.Columns("MAR").SetOrdinal(13)
                dt.Columns("ABR").SetOrdinal(14)
                dt.Columns("MAY").SetOrdinal(15)
                dt.Columns("JUN").SetOrdinal(16)
                dt.Columns("JUL").SetOrdinal(17)
                dt.Columns("AGO").SetOrdinal(18)
                dt.Columns("SEP").SetOrdinal(19)
                dt.Columns("OCT").SetOrdinal(20)
                dt.Columns("NOV").SetOrdinal(21)
                dt.Columns("DIC").SetOrdinal(22)
                dt.Columns("Average").SetOrdinal(23)
            End If
            dt.Columns("ProductCode").Caption = "Código Producto"
            dt.Columns("ProductName").Caption = "Producto"
            dt.Columns("MeasurementUnitCode").Caption = "Código Unidad"
            dt.Columns("MeasurementUnitName").Caption = "Unidad"
            dt.Columns("ATCConcentration").Caption = "Concentración"
            dt.Columns("SourceCode").Caption = "Código Almacén"
            dt.Columns("SourceName").Caption = "Almacén Origen"
            dt.Columns("TargetTypeName").Caption = "Tipo Destino"
            dt.Columns("TargetCode").Caption = "Código Destino"
            dt.Columns("TargetName").Caption = "Destino"
            dt.Columns("ANIO").Caption = "Año"
            dt.Columns("Average").Caption = "Promedio"

        End If
        _gridControl.DataSource = dt
        'AsyncLoader(False)
        If dt Is Nothing Then
            'Return dt
            '_gridControl.DataSource = dt
            'Else
            'Return New DataTable
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
            Me.INDDateStart.Focus()
        End If
    End Function

    ''' <summary>
    ''' metodo para generar excel
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GenerateExcel(ByVal _gridControl As DevExpress.XtraGrid.GridControl)
        Dim _gridView = _gridControl
        If _gridView IsNot Nothing Then
            Dim fileName As String = System.IO.Path.GetTempFileName() & ".xlsx"
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

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

    End Sub

    ''' <summary>
    ''' Evento Load del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportAverageConsumptionTransfer_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Cargar GridLookUpEdit
        Me.INDGleOrderType.Properties.DataSource = FillingOrderType
        Me.INDGleOfficeFilter.Properties.DataSource = FillingOfficeFilter
        Me.INDGleReportType.Properties.DataSource = FillingReportType
        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleOfficeFilter.EditValue = 1
        Me.INDGleReportType.EditValue = 1

        INDSleFunctionalUnit.Properties.View.OptionsView.ShowGroupPanel = False
        INDSleOriginStore.Properties.View.OptionsView.ShowGroupPanel = False
        INDSleDestinationStore.Properties.View.OptionsView.ShowGroupPanel = False
        INDSleProduct.Properties.View.OptionsView.ShowGroupPanel = False

    End Sub
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed

        _FillingOfficeFilter = Nothing
        _FillingReportType = Nothing
        _FillingOrderType = Nothing
    End Sub

#End Region

#Region "QueryPopUp"


    ''' <summary>
    ''' Cargamos el datasource del search de unidad funcional QueryPopUp
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleFunctionalUnit_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleFunctionalUnit.QueryPopUp
        If INDSleFunctionalUnit.Properties.DataSource Is Nothing Then
            INDSleFunctionalUnit.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).PayrollService.ListFunctionalUnit(True)
        End If
    End Sub

    ''' <summary>
    ''' Cargamos el datasource del search de almacen origen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleOriginStore_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleOriginStore.QueryPopUp
        If INDSleOriginStore.Properties.DataSource Is Nothing Then
            INDSleOriginStore.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListWarehouseReport()
        End If
    End Sub

    ''' <summary>
    ''' Cargamos el datasource del search de almacen final
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleDestinationStore_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleDestinationStore.QueryPopUp
        If INDSleDestinationStore.Properties.DataSource Is Nothing Then
            INDSleDestinationStore.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListWarehouseReport()
        End If
    End Sub

    ''' <summary>
    ''' Cargamos el datasource del search de Producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleProduct_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleProduct.QueryPopUp
        If INDSleProduct.Properties.DataSource Is Nothing Then
            INDSleProduct.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListProductsReport()
        End If
    End Sub

#End Region

#Region "EditValueChange"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGleOrderType_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleOrderType.EditValueChanged
        If INDGleOrderType.EditValue = 1 Then
            INDLciOfficeFilter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

            INDLciFunctionalUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            _selectorFunctionalUnit.Clear()
            INDSleFunctionalUnit.Text = ""

            INDLciDestinationStore.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDGleOfficeFilter.EditValue = Nothing

        ElseIf INDGleOrderType.EditValue = 2 Then
            INDLciOfficeFilter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciOfficeFilter.Enabled = True

            INDLciFunctionalUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            _selectorFunctionalUnit.Clear()
            INDSleFunctionalUnit.Text = ""

            INDLciDestinationStore.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            _selectorDestinationStore.Clear()
            INDSleDestinationStore.Text = ""

            INDGleOfficeFilter.EditValue = Nothing

        ElseIf INDGleOrderType.EditValue = 3 Then
            INDLciOfficeFilter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDGleOfficeFilter.EditValue = 3
            INDLciOfficeFilter.Enabled = False
            INDLciFunctionalUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciDestinationStore.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleOfficeFilter_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleOfficeFilter.EditValueChanged
        If INDGleOfficeFilter.EditValue = 1 Then 'almacen
            INDLciDestinationStore.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciFunctionalUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDSleFunctionalUnit.EditValue = Nothing
        ElseIf INDGleOfficeFilter.EditValue = 2 Then 'unidad funcional
            INDLciDestinationStore.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciFunctionalUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDSleDestinationStore.EditValue = Nothing
        ElseIf INDGleOfficeFilter.EditValue = 3 Then 'ambos
            INDLciDestinationStore.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciFunctionalUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub

#End Region

#Region "Report"


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Private Function RecuperarParametros() As String
        Dim parametros As String = String.Format("<{0}>{1:dd/MM/yyyy hh:mm:ss}</{0}>", "InitialDate", INDDateStart.EditValue)
        parametros = String.Format("{0}<{1}>{2:dd/MM/yyyy hh:mm:ss}</{1}>", parametros, "EndDate", INDDateEnd.EditValue)
        parametros = String.Format("{0}<{1}>{2}</{1}>", parametros, "OrderType", INDGleOrderType.EditValue)
        parametros = String.Format("{0}<{1}>{2}</{1}>", parametros, "OfficeTo", INDGleOfficeFilter.EditValue)
        parametros = String.Format("{0}<{1}>{2}</{1}>", parametros, "ReportType", INDGleReportType.EditValue)

        If (Not String.IsNullOrEmpty(_selectorOriginStore.GetKeys())) AndAlso Not INDGvOriginStore.SelectedRowsCount = INDGvOriginStore.RowCount Then
            parametros = String.Format("{0}<{1}>{2}</{1}>", parametros, "OriginStore", _selectorOriginStore.GetKeys())
        End If
        If (Not String.IsNullOrEmpty(_selectorDestinationStore.GetKeys())) AndAlso Not INDGvDestinationStore.SelectedRowsCount = INDGvDestinationStore.RowCount Then
            parametros = String.Format("{0}<{1}>{2}</{1}>", parametros, "DestinationStore", _selectorDestinationStore.GetKeys())
        End If
        If (Not String.IsNullOrEmpty(_selectorFunctionalUnit.GetKeys())) AndAlso Not INDGvFunctionalUnit.SelectedRowsCount = INDGvFunctionalUnit.RowCount Then
            parametros = String.Format("{0}<{1}>{2}</{1}>", parametros, "FunctionalUnit", _selectorFunctionalUnit.GetKeys())
        End If
        If (Not String.IsNullOrEmpty(_selectorProduct.GetKeys())) AndAlso Not INDGvProduct.SelectedRowsCount = INDGvProduct.RowCount Then
            parametros = String.Format("{0}<{1}>{2}</{1}>", parametros, "Product", _selectorProduct.GetKeys())
        End If
        parametros = String.Format("<{0}>{1}</{0}>", "Parameters", parametros)
        Return parametros
    End Function


    ''' <summary>
    ''' se ejecuta para exportar a excel el reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReports_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReports.Click
        If Me.ValidateControlsReports = True Then
            AsyncLoader(True)
            Dim _gridControl As DevExpress.XtraGrid.GridControl
            If INDGleReportType.EditValue = 1 Then 'RESUMIDO
                _gridControl = Me.INDGcExportExcel
            Else
                _gridControl = Me.INDGcExportExcelDetail
            End If
            '_gridControl.DataSource = Await CargueDatasource()
            Await CargueDatasource(_gridControl)
            If _gridControl.DataSource IsNot Nothing Then
                GenerateExcel(_gridControl)
            End If
            AsyncLoader(False)
        End If
    End Sub





    ''' <summary>
    ''' se ejecuta cuando den click en el boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports Then


            AsyncLoader(True)
            Dim parametros As String = RecuperarParametros()

            Dim reporte As New rptReportAverageConsumptionTransfer
            reporte.ParametrosReporte = New Object() {parametros}
            INDDvDocumentViewer.DocumentSource = reporte
            Await reporte.CargarDataSource()
            If reporte.EncontroInformacion Then
                reporte.CreateDocument(True)
                Me.INDLcBase.Visible = False
                Me.INDCncNavigation.Visible = False
                Me.INDPcDocumentViewer.Visible = True
                INDDvDocumentViewer.Show()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                Me.INDDateStart.Focus()
            End If
            AsyncLoader(False)


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
        Me.INDDateStart.Focus()
    End Sub

    Private Sub INDDateStart_EditValueChanged(sender As Object, e As EventArgs) Handles INDDateStart.EditValueChanged

    End Sub

#End Region

#End Region

End Class