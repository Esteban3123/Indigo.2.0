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

Public Class FrmReportRemissionsEntrance

#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

#End Region

#Region "properties"
    Public Property ProoftCloseXpoDocuments As XPInstantFeedbackSource
    Public Property ProoftCloseXpoSupplier As XPInstantFeedbackSource
    Public Property ProoftCloseXpoProducts As XPInstantFeedbackSource
    Public Property ProoftCloseXpoWarehouseXP As XPCollection(Of InventoryWarehouseReportXpo)

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

    Private _FillingTypeReport As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingTypeReport As List(Of Tuple(Of Integer, String))
        Get
            If _FillingTypeReport Is Nothing Then
                _FillingTypeReport = New List(Of Tuple(Of Integer, String))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(1, "Resumido"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(2, "Detallado"))
            End If
            Return _FillingTypeReport
        End Get
    End Property

    Private _FillingLegalization As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingLegalization As List(Of Tuple(Of Integer, String))
        Get
            If _FillingLegalization Is Nothing Then
                _FillingLegalization = New List(Of Tuple(Of Integer, String))
                _FillingLegalization.Add(New Tuple(Of Integer, String)(1, "Todos"))
                _FillingLegalization.Add(New Tuple(Of Integer, String)(2, "Pendientes Por Legalizar"))
            End If
            Return _FillingLegalization
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
            End If
            Return _FillingStatus
        End Get
    End Property

    Private criteria As String = Nothing

#End Region

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

        If INDDateStart.EditValue Is Nothing Or INDDateEnd.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
            Me.INDDateStart.Focus()
            Validations = False
        ElseIf Me.INDDateStart.EditValue > INDDateEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("CompareRangeDate", "Commons"))
            Me.INDDateStart.Focus()
            Validations = False
        End If
        'validaciones controles de documento
        If INDSleDocumentsStart.EditValue IsNot Nothing And INDSleDocumentsEnd.EditValue Is Nothing Or INDSleDocumentsStart.EditValue Is Nothing And INDSleDocumentsEnd.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblDocuments.Text)
            Me.INDSleDocumentsStart.Focus()
            Validations = False
        ElseIf INDSleDocumentsStart.EditValue > INDSleDocumentsEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblDocuments.Text)
            Me.INDSleDocumentsStart.Focus()
            Validations = False
        End If

        'validaciones controles de proveedores
        If INDSleSuppliersStart.EditValue IsNot Nothing And INDSleSuppliersEnd.EditValue Is Nothing Or INDSleSuppliersStart.EditValue Is Nothing And INDSleSuppliersEnd.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblSuppliers.Text)
            Me.INDSleSuppliersStart.Focus()
            Validations = False
        ElseIf INDSleSuppliersStart.EditValue > INDSleSuppliersEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblSuppliers.Text)
            Me.INDSleSuppliersStart.Focus()
            Validations = False
        End If

        'validaciones controles de productos
        If INDSleProductStart.EditValue IsNot Nothing And INDSleProductEnd.EditValue Is Nothing Or INDSleProductStart.EditValue Is Nothing And INDSleProductEnd.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblProducts.Text)
            Me.INDSleProductStart.Focus()
            Validations = False
        ElseIf INDSleProductStart.EditValue > INDSleProductEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblProducts.Text)
            Me.INDSleProductStart.Focus()
            Validations = False
        End If

        Return Validations

    End Function

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleDocumentStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoDocumentStart()
        criteria = Nothing

        If (INDSleSuppliersStart.EditValue IsNot Nothing And INDSleSuppliersEnd.EditValue IsNot Nothing) Then
            criteria = "SupplierId.IdThirdParty.Nit >= '" & INDSleSuppliersStart.EditValue & "' AND SupplierId.IdThirdParty.Nit <= '" & INDSleSuppliersEnd.EditValue & "'"
        End If

        Using msearch As New MBusqueda
            ProoftCloseXpoDocuments = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListRemissionEntranceReportFilter, criteria)
            INDSleDocumentsStart.Datasource = ProoftCloseXpoDocuments
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleDocumentEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoDocumentEnd()
        criteria = Nothing

        If (INDSleSuppliersStart.EditValue IsNot Nothing And INDSleSuppliersEnd.EditValue IsNot Nothing) Then
            criteria = "SupplierId.IdThirdParty.Nit >= '" & INDSleSuppliersStart.EditValue & "' AND SupplierId.IdThirdParty.Nit <= '" & INDSleSuppliersEnd.EditValue & "'"
        End If

        Using msearch As New MBusqueda
            ProoftCloseXpoDocuments = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListRemissionEntranceReportFilter, criteria)
            INDSleDocumentsEnd.Datasource = ProoftCloseXpoDocuments
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleSupplierStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoSupplierStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoSupplier = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListSupplierInventoryReport)
            INDSleSuppliersStart.Datasource = ProoftCloseXpoSupplier
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleSupplierEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoSupplierEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoSupplier = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListSupplierInventoryReport)
            INDSleSuppliersEnd.Datasource = ProoftCloseXpoSupplier
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleProductStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoProductsStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoProducts = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListProductsReport)
            INDSleProductStart.Datasource = ProoftCloseXpoProducts
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleProductEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoProductsEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoProducts = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListProductsReport)
            INDSleProductEnd.Datasource = ProoftCloseXpoProducts
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleWarehouseEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoWarehouse()
        Using msearch As New MBusqueda
            ProoftCloseXpoWarehouseXP = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListWarehouseReportXP)
            ProoftCloseXpoWarehouseXP.Load()
            INDCcbWarehouses.Properties.DataSource = ProoftCloseXpoWarehouseXP
        End Using
    End Sub

    ''' <summary>
    ''' se ejecuta al mostrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportRemissionsEntrance_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        'Cargar GridLookUpEdit
        Me.INDGleGroup.Properties.DataSource = FillingGroupBy
        Me.INDGleTypeReport2.Properties.DataSource = FillingTypeReport
        Me.INDGleLegalization.Properties.DataSource = FillingLegalization
        Me.INDCcbWarehouseStatus.Properties.DataSource = FillingStatus
        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleGroup.EditValue = 1
        Me.INDGleTypeReport2.EditValue = 1
        Me.INDGleLegalization.EditValue = 1
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
    ''' se ejecuta cuando den click en el boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        Dim filterByWarehouse = getFilterWareHouse()
        If Me.ValidateControlsReports = True Then
            If INDGleTypeReport2.EditValue = 1 Then
                AsyncLoader(True)
                Dim reporte As New rptRemissionsEntrance
                reporte.ParametrosReporte = New Object() {INDDateStart.EditValue,
                                                          INDDateEnd.EditValue,
                                                          INDSleDocumentsStart.EditValue,
                                                          INDSleDocumentsEnd.EditValue,
                                                          INDSleSuppliersStart.EditValue,
                                                          INDSleSuppliersEnd.EditValue,
                                                          INDGleGroup.EditValue,
                                                          INDCcbWarehouses.EditValue,
                                                          filterByWarehouse,
                                                          INDCcbWarehouseStatus.EditValue}
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
                    Me.INDDateStart.Focus()
                End If
            Else
                AsyncLoader(True)
                Dim reporte As New rptRemissionsEntranceDetail
                reporte.ParametrosReporte = New Object() {INDDateStart.EditValue,
                                                          INDDateEnd.EditValue,
                                                          INDGleLegalization.EditValue,
                                                          INDSleDocumentsStart.EditValue,
                                                          INDSleDocumentsEnd.EditValue,
                                                          INDSleSuppliersStart.EditValue,
                                                          INDSleSuppliersEnd.EditValue,
                                                          INDSleProductStart.EditValue,
                                                          INDSleProductEnd.EditValue,
                                                          INDGleLegalization.EditValue,
                                                          INDGleGroup.EditValue,
                                                          INDCcbWarehouses.EditValue,
                                                          filterByWarehouse,
                                                          INDCcbWarehouseStatus.EditValue}
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
                    Me.INDDateStart.Focus()
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleDocumentsStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleDocumentsStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleDocumentsStart.QueryPopUp
        If INDSleDocumentsStart.Datasource Is Nothing Then
            LoadXpoDocumentStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control LoadXpoDocumentsEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleDocumentsEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleDocumentsEnd.QueryPopUp
        If INDSleDocumentsEnd.Datasource Is Nothing Then
            LoadXpoDocumentEnd()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleSuppliersStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleSuppliersStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleSuppliersStart.QueryPopUp
        If INDSleSuppliersStart.Datasource Is Nothing Then
            LoadXpoSupplierStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleSuppliersEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleSuppliersEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleSuppliersEnd.QueryPopUp
        If INDSleSuppliersEnd.Datasource Is Nothing Then
            LoadXpoSupplierEnd()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleProductStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleProductStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleProductStart.QueryPopUp
        If INDSleProductStart.Datasource Is Nothing Then
            LoadXpoProductsStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleProductEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleProductEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleProductEnd.QueryPopUp
        If INDSleProductEnd.Datasource Is Nothing Then
            LoadXpoProductsEnd()
        End If
    End Sub

    Private Sub INDSleSuppliersStart_EditValueChanged(sender As Object, e As Presentation.Controls.EditValueChangedEventArgs) Handles INDSleSuppliersStart.EditValueChanged
        LoadXpoDocumentStart()
        LoadXpoDocumentEnd()
    End Sub

    Private Sub INDSleSuppliersEnd_EditValueChanged(sender As Object, e As Presentation.Controls.EditValueChangedEventArgs) Handles INDSleSuppliersEnd.EditValueChanged
        LoadXpoDocumentStart()
        LoadXpoDocumentEnd()
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _pucModel.Dispose()
        _pucModel = Nothing
        ProoftCloseXpoDocuments = Nothing
        ProoftCloseXpoProducts = Nothing
        ProoftCloseXpoSupplier = Nothing
        ProoftCloseXpoWarehouseXP = Nothing
        _FillingGroupBy = Nothing
        _FillingTypeReport = Nothing
        _FillingLegalization = Nothing
        _FillingStatus = Nothing
    End Sub

#Region "Handdles"

    ''' <summary>
    ''' Evento clic sobre boton exportar excel - cargamos los datos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSbExportData_Click(sender As Object, e As EventArgs) Handles INDSbExportData.Click
        If Me.ValidateControlsReports Then
            'LLenar el datasource
            INDGcExportData.DataSource = Nothing
            Dim rpt As New rptRemissionsEntranceDetail
            Dim filterByWarehouse = getFilterWareHouse()
            rpt.ParametrosReporte = New Object() {INDDateStart.EditValue,
                                                  INDDateEnd.EditValue,
                                                  INDGleLegalization.EditValue,
                                                  INDSleDocumentsStart.EditValue,
                                                  INDSleDocumentsEnd.EditValue,
                                                  INDSleSuppliersStart.EditValue,
                                                  INDSleSuppliersEnd.EditValue,
                                                  INDSleProductStart.EditValue,
                                                  INDSleProductEnd.EditValue,
                                                  INDGleLegalization.EditValue,
                                                  INDGleGroup.EditValue,
                                                  INDCcbWarehouses.EditValue,
                                                  filterByWarehouse,
                                                  INDCcbWarehouseStatus.EditValue}
            Dim filtroConsulta = rpt.GetFilter()
            Dim dataSource As XPCollection(Of InventoryViewRemissionEntranceDetailReportXpo) = Nothing
            Await Task.Factory.StartNew(Sub()
                                            dataSource = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).InventoryService.ListRemissionEntranceDeatilReport(filtroConsulta)
                                        End Sub)
            If dataSource IsNot Nothing AndAlso dataSource.Count > 0 Then
                INDGcExportData.DataSource = dataSource
                GenerateExcel()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo que devuelve  el Código de los Almacenes Selecionados, para enviar y mostrarlos en el reporte
    ''' </summary>
    ''' <returns></returns>
    Private Function getFilterWareHouse()
        Dim GetCheckedValue = INDCcbWarehouses.Properties.GetItems().GetCheckedValues()
        Dim stringWarehouse As String = String.Empty
        If GetCheckedValue.Count > 0 Then
            Dim dataSourceWarehouse = TryCast(INDCcbWarehouses.Properties.DataSource, XPCollection(Of InventoryWarehouseReportXpo))
            If dataSourceWarehouse IsNot Nothing Then
                Dim queryWarehouse = (From x In dataSourceWarehouse
                                      Where GetCheckedValue.Contains(x.Id)
                                      Select x.Code).ToList()
                stringWarehouse = String.Join(",", queryWarehouse)
            End If
        End If
        Return stringWarehouse
    End Function
    '-> 1, 2, 3
    ''' <summary>
    ''' Metodo para generar el archivo en excel
    ''' </summary>
    Private Sub GenerateExcel()
        Dim _gridControl = INDGcExportData
        If _gridControl IsNot Nothing Then
            Dim fileName As String = System.IO.Path.GetTempFileName & ".xlsx"
            Dim param As New DevExpress.XtraPrinting.XlsxExportOptions(DevExpress.XtraPrinting.TextExportMode.Value, True, False)
            _gridControl.ExportToXlsx(fileName, param)
            If System.IO.File.Exists(fileName) Then
                System.Diagnostics.Process.Start(fileName)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmReportRemissionsEntrance_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        INDDateStart.Focus()
        LoadXpoWarehouse()

        Me._pucModel = New MCommon(Me.Tag)
        Me.INDSleDocumentsStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetRemissionEntranceByCode
        Me.INDSleDocumentsEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetRemissionEntranceByCode
        Me.INDSleSuppliersStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetSupplierByNitThirdParty
        Me.INDSleSuppliersEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetSupplierByNitThirdParty
        Me.INDSleProductStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetInventoryProduct
        Me.INDSleProductEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetInventoryProduct

        INDSleDocumentsStart.View.OptionsView.ShowGroupPanel = False
        INDSleDocumentsEnd.View.OptionsView.ShowGroupPanel = False
        INDSleSuppliersStart.View.OptionsView.ShowGroupPanel = False
        INDSleSuppliersEnd.View.OptionsView.ShowGroupPanel = False
        INDSleProductStart.View.OptionsView.ShowGroupPanel = False
        INDSleProductEnd.View.OptionsView.ShowGroupPanel = False
    End Sub

    ''' <summary>
    ''' Load de barra de botones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

    ''' <summary>
    ''' Valida si el tipo de reporte es detallado, de lo contrario desabilita o habilita opciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleTypeReport2_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleTypeReport2.EditValueChanged
        If INDGleTypeReport2.EditValue = 1 Then
            INDLciExportData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciLegalization.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciProducts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciProductStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciProductEnd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

            INDLciWarehouse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciSuppliersStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciSuppliersEnd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciDocuments.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciDocumentsStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciDocumentsEnd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

            INDSleProductStart.EditValue = Nothing
            INDSleProductEnd.EditValue = Nothing
            INDGleLegalization.EditValue = Nothing
        Else
            INDLciExportData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciLegalization.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciProducts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciProductStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciProductEnd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

            INDLciWarehouse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciSuppliersStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciSuppliersEnd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciDocuments.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciDocumentsStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciDocumentsEnd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDGleLegalization.EditValue = 1
        End If
    End Sub
#End Region


End Class