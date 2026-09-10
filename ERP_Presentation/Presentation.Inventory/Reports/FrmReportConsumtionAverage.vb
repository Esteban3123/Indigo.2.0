'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Johan Sebastian Carranza Ramos
' Created          : 17-05-2019
'
' Last Modified By : Johan Sebastian Carranza Ramos
' Last Modified On : 22-10-2019
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

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
Imports DevExpress.XtraEditors.Repository
Imports System.Reflection
Imports DevExpress.Utils.Extensions
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Presentation.Inventory.MVP

#End Region

Public Class FrmReportConsumtionAverage

#Region "Variables"
    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance
    Private a As List(Of InventoryTransferOrderDetailReportXpo) '= New List(Of InventoryTransferOrderDetailReportXpo)
    Private b As List(Of InventoryTransferOrderDetailReportXpo) '= New List(Of InventoryTransferOrderDetailReportXpo)
    ''' <summary>
    ''' Presentador
    ''' </summary>
    Dim Presenter As PReportConsumtionAverage
#End Region

#Region "properties"


    Public Property ProoftCloseXpoThirdParty As XPInstantFeedbackSource
    Public Property ProoftCloseXpoWarehouse As XPInstantFeedbackSource
    Public Property ProoftCloseXpoFunctionalUnit As XPInstantFeedbackSource
    Public Property ProoftCloseXpoProduct As XPInstantFeedbackSource


#End Region

#Region "Datasouce"
    Private _FillingState As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingState As List(Of Tuple(Of Integer, String))
        Get
            If _FillingState Is Nothing Then
                _FillingState = New List(Of Tuple(Of Integer, String))
                _FillingState.Add(New Tuple(Of Integer, String)(1, "Confirmado"))
                _FillingState.Add(New Tuple(Of Integer, String)(2, "Sin confirmar"))
                _FillingState.Add(New Tuple(Of Integer, String)(3, "Todos"))
            End If
            Return _FillingState
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

    Private _FillingOfficeFilter As List(Of Tuple(Of Integer, String))
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


    Private criteria As String = Nothing

    Private _destinationStoreId As String
    Public Property DestinationStoreId As String
        Get
            Return _destinationStoreId
        End Get
        Set(value As String)
            _destinationStoreId = value
        End Set
    End Property

    Private _productId As String
    Public Property ProductId As String
        Get
            Return _productId
        End Get
        Set(value As String)
            _productId = value
        End Set
    End Property


    Private _SupplierId As String
    Public Property SupplierId As String
        Get
            Return _SupplierId
        End Get
        Set(value As String)
            _SupplierId = value
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
        Dim _mensaje As String = String.Empty
        If INDDateStart1.EditValue Is Nothing Or INDDateEnd1.EditValue Is Nothing Then
            _mensaje = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
        ElseIf Me.INDDateStart1.EditValue > INDDateEnd1.EditValue Then
            _mensaje = String.Format("{0}{1}{2}", _mensaje, Environment.NewLine, ResourceManager.GetString("CompareRangeDate", "Commons"))
        End If
        If Me.INDGleState.EditValue Is Nothing Then
            _mensaje = String.Format("{0}{1}{2}", _mensaje, Environment.NewLine, "Seleccione estado a filtrar.")
        End If
        If Me.INDGleReportType.EditValue Is Nothing Then
            _mensaje = String.Format("{0}{1}{2}", _mensaje, Environment.NewLine, "Seleccione tipo de reporte.")
        End If

        If Not String.IsNullOrEmpty(_mensaje) Then
            Mensaje(EeventViewerImages.Advertencia) = _mensaje
            INDDateStart1.Focus()
        End If
        Return String.IsNullOrEmpty(_mensaje)
    End Function



#Region "Selector"

    Private _selectorWarehouse As SelectorCache = New SelectorCache("Id", "Code")
    Private _selectorSupplier As SelectorCache = New SelectorCache("Id", "ThirdPartyNit")
    Private _selectorProduct As SelectorCache = New SelectorCache("Id", "Code")



    Private Sub INDGv_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvWarehouse.CustomUnboundColumnData, INGvSupplier.CustomUnboundColumnData, INDGvProduct.CustomUnboundColumnData
        If e.IsGetData Then
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvWarehouse" Then
                e.Value = _selectorWarehouse.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INGvSupplier" Then
                e.Value = _selectorSupplier.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvProduct" Then
                e.Value = _selectorProduct.ValidateExistsRow(e.Row)
            End If
        End If
    End Sub

    Private Sub INDGv_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles INDGvWarehouse.RowCellClick, INGvSupplier.RowCellClick, INDGvProduct.RowCellClick
        If e.Column.FieldName.Contains("UnboundSelection") Then
            Dim selector As SelectorCache = Nothing
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvWarehouse" Then
                selector = _selectorWarehouse
            ElseIf view.Name = "INGvSupplier" Then
                selector = _selectorSupplier
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

    Private Sub INDSle_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDSleWarehouse.Closed, INDSleSupplier.Closed, INDSleProduct.Closed
        Dim searchLookupEdit = TryCast(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        searchLookupEdit.EditValue = Nothing
        If searchLookupEdit.Name = "INDSleWarehouse" Then
            searchLookupEdit.Properties.NullText = _selectorWarehouse.ToString()
        ElseIf searchLookupEdit.Name = "INDSleSupplier" Then
            searchLookupEdit.Properties.NullText = _selectorSupplier.ToString()
        ElseIf searchLookupEdit.Name = "INDSleProduct" Then
            searchLookupEdit.Properties.NullText = _selectorProduct.ToString()

        End If
    End Sub

#End Region

#End Region

#Region "Events"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        a = Nothing
        b = Nothing
        ProoftCloseXpoFunctionalUnit = Nothing
        ProoftCloseXpoProduct = Nothing
        ProoftCloseXpoWarehouse = Nothing
        _FillingOfficeFilter = Nothing
        _FillingReportType = Nothing
        _FillingState = Nothing
    End Sub

    ''' <summary>
    ''' Evento Load del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportConsumtionAverage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'instanciamos el presentador.
        Presenter = New PReportConsumtionAverage
        'Cargar GridLookUpEdit
        Me.INDGleState.Properties.DataSource = FillingState
        Me.INDGleReportType.Properties.DataSource = FillingReportType
        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleState.EditValue = 3
        Me.INDGleReportType.EditValue = 1
        'INDSleDestinationStore.Properties.View.OptionsView.ShowGroupPanel = False
        'INDSleProducto.Properties.View.OptionsView.ShowGroupPanel = False

    End Sub

    ''' <summary>
    ''' se ejecuta al dar click en el control INDCnBack
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnBack_ClickBack() Handles INDCnBack.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncNavigation.Visible = True
        Me.INDPcDocumentViewer.Visible = False
        Me.INDDateStart1.Focus()
    End Sub



#End Region


#Region "Report "


    ''' <summary>
    ''' se ejecuta para exportar a excel el reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbExportExcel_Click(sender As Object, e As EventArgs) Handles INDSbExportExcel.Click
        If Me.ValidateControlsReports = True Then
            AsyncLoader(True)
            Dim parametros As String
            parametros = String.Format("<{0}>{1}</{0}>", "InitialDate", Format(INDDateStart1.EditValue, "dd/MM/yyyy"))
            parametros = String.Format("{0}<{1}>{2}</{1}>", parametros, "EndDate", Format(INDDateEnd1.EditValue, "dd/MM/yyyy"))
            parametros = String.Format("{0}<{1}>{2}</{1}>", parametros, "State", INDGleState.EditValue)
            parametros = String.Format("{0}<{1}>{2}</{1}>", parametros, "ReportType", INDGleReportType.Text)
            If Not String.IsNullOrEmpty(_selectorWarehouse.GetKeys()) Then
                parametros = String.Format("{0}<{1}>{2}</{1}>", parametros, "Warehouse", _selectorWarehouse.GetKeys())
            End If
            If Not String.IsNullOrEmpty(_selectorSupplier.GetKeys()) Then
                parametros = String.Format("{0}<{1}>{2}</{1}>", parametros, "Supplier", _selectorSupplier.GetKeys())
            End If
            If Not String.IsNullOrEmpty(_selectorProduct.GetKeys()) Then
                parametros = String.Format("{0}<{1}>{2}</{1}>", parametros, "Product", _selectorProduct.GetKeys())
            End If
            parametros = String.Format("<{0}>{1}</{0}>", "Parameters", parametros)
            Dim INDdsDataSource As DataSet = Await Presenter.LoadDataSourceEntranceAverage(parametros, Me.IndigoSessionValues)
            Dim INDdtDataSource As DataTable = INDdsDataSource.Tables("Inventory_SP_AverageReportEntranceOrder")
            If INDdtDataSource IsNot Nothing AndAlso INDdtDataSource.Rows.Count > 0 Then
                'Logica para esconder columnas del GridControl
                If INDGleReportType.Text = "Resumido" Then
                    Dim Count As Integer = 0
                    For Each item As GridColumn In INDGvExportExcel.Columns
                        item.Tag = Count
                        Count += 1
                    Next
                    For Each item As GridColumn In INDGvExportExcel.Columns
                        If item.Tag < 4 Or item.Tag > 15 Then
                            item.Visible = True
                        Else
                            item.Visible = False
                        End If
                    Next
                Else
                    For Each item As GridColumn In INDGvExportExcel.Columns
                        item.Visible = True
                    Next
                End If
                INDGcExportExcel.DataSource = INDdtDataSource
                INDGvExportExcel.OptionsView.ColumnAutoWidth = False
                INDGvExportExcel.HorzScrollVisibility = ScrollVisibility.Always
                INDGvExportExcel.BestFitColumns()

                If FolderBrowserDialog1.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                    If FolderBrowserDialog1.SelectedPath <> String.Empty Then
                        INDGcExportExcel.ExportToXlsx(FolderBrowserDialog1.SelectedPath + "\ReporteEntradasDeInventario.xlsx")
                        Mensaje(EeventViewerImages.Informacion) = "Archivo exportado correctamente."
                    End If
                    If System.IO.File.Exists(FolderBrowserDialog1.SelectedPath + "\ReporteEntradasDeInventario.xlsx") Then
                        System.Diagnostics.Process.Start(FolderBrowserDialog1.SelectedPath + "\ReporteEntradasDeInventario.xlsx")
                    End If
                End If
            Else
                Mensaje(EeventViewerImages.Advertencia) = "No hay datos para exportar a excel."
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
            Dim parametros As String
            parametros = String.Format("<{0}>{1}</{0}>", "InitialDate", Format(INDDateStart1.EditValue, "dd/MM/yyyy"))
            parametros = String.Format("{0}<{1}>{2}</{1}>", parametros, "EndDate", Format(INDDateEnd1.EditValue, "dd/MM/yyyy"))
            parametros = String.Format("{0}<{1}>{2}</{1}>", parametros, "State", INDGleState.EditValue)
            parametros = String.Format("{0}<{1}>{2}</{1}>", parametros, "ReportType", INDGleReportType.Text)
            If Not String.IsNullOrEmpty(_selectorWarehouse.GetKeys()) Then
                parametros = String.Format("{0}<{1}>{2}</{1}>", parametros, "Warehouse", _selectorWarehouse.GetKeys())
            End If
            If Not String.IsNullOrEmpty(_selectorSupplier.GetKeys()) Then
                parametros = String.Format("{0}<{1}>{2}</{1}>", parametros, "Supplier", _selectorSupplier.GetKeys())
            End If
            If Not String.IsNullOrEmpty(_selectorProduct.GetKeys()) Then
                parametros = String.Format("{0}<{1}>{2}</{1}>", parametros, "Product", _selectorProduct.GetKeys())
            End If
            parametros = String.Format("<{0}>{1}</{0}>", "Parameters", parametros)

            Dim reporte As New rptReportConsumtionAverage
            reporte.ParametrosReporte = New Object() {parametros}
            INDDvDocumentViewer.DocumentSource = reporte
            Await reporte.CargarDataSource()
            If reporte.DataSource IsNot Nothing Then
                reporte.CreateDocument(True)
                Me.INDLcBase.Visible = False
                Me.INDCncNavigation.Visible = False
                Me.INDPcDocumentViewer.Visible = True
                INDDvDocumentViewer.Show()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                Me.INDDateStart1.Focus()
            End If
            AsyncLoader(False)
        End If
    End Sub
#End Region


#Region "QueryPoUp"

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSlewarehouse
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDSleWarehouse_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleWarehouse.QueryPopUp
        If INDSleWarehouse.Properties.DataSource Is Nothing Then
            INDSleWarehouse.Properties.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).InventoryService.ListWarehouseReport()
        End If
    End Sub
    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleSupplier
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDSlesupplier_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleSupplier.QueryPopUp
        If INDSleSupplier.Properties.DataSource Is Nothing Then
            INDSleSupplier.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListSupplierInventoryReport()
        End If
    End Sub
    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleProduct
    ''' </summary>
    ''' <remarks></remarks>     
    Private Sub INDSleProduct_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleProduct.QueryPopUp
        If INDSleProduct.Properties.DataSource Is Nothing Then
            INDSleProduct.Properties.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).InventoryService.ListProductsReport()
        End If
    End Sub
#End Region

    ''' <summary>
    ''' Inicializar componentes.
    ''' </summary>
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
    End Sub




End Class