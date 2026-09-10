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
Imports System.Text

#End Region

Public Class FrmReportTransferOrder

#Region "Variables"



#End Region

#Region "Datasource"

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

    Private _FillingTypeFilter As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingTypeFilter As List(Of Tuple(Of Integer, String))
        Get
            If _FillingTypeFilter Is Nothing Then
                _FillingTypeFilter = New List(Of Tuple(Of Integer, String))
                _FillingTypeFilter.Add(New Tuple(Of Integer, String)(1, "Almacen"))
                _FillingTypeFilter.Add(New Tuple(Of Integer, String)(2, "Unidad Funcional"))
            End If
            Return _FillingTypeFilter
        End Get
    End Property

    Private _FillingGroupBy As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingGroupBy As List(Of Tuple(Of Integer, String))
        Get
            If _FillingGroupBy Is Nothing Then
                _FillingGroupBy = New List(Of Tuple(Of Integer, String))
                _FillingGroupBy.Add(New Tuple(Of Integer, String)(1, "Unidad Funcional"))
                _FillingGroupBy.Add(New Tuple(Of Integer, String)(2, "Tercero"))
            End If
            Return _FillingGroupBy
        End Get
    End Property

    Private _FillingState As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingState As List(Of Tuple(Of Integer, String))
        Get
            If _FillingState Is Nothing Then
                _FillingState = New List(Of Tuple(Of Integer, String))
                _FillingState.Add(New Tuple(Of Integer, String)(1, "Registrado"))
                _FillingState.Add(New Tuple(Of Integer, String)(2, "Confirmado"))
                _FillingState.Add(New Tuple(Of Integer, String)(3, "Anulado"))
                _FillingState.Add(New Tuple(Of Integer, String)(4, "Todos"))
            End If
            Return _FillingState
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


        If INDDateStart.EditValue Is Nothing Or INDDateEnd.EditValue Is Nothing Then
            errors.AppendLine(String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons")))
            Me.INDDateStart.Focus()
        ElseIf Me.INDDateStart.EditValue > INDDateEnd.EditValue Then
            errors.AppendLine(String.Format(ResourceManager.GetString("CompareRangeDate", "Commons")))
            Me.INDDateStart.Focus()
        End If

        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Return False
        End If

        Return True
    End Function

#Region "Selector"

    Private _selectorProduct As SelectorCache = New SelectorCache("Id", "Code")
    Private _selectorGroup As SelectorCache = New SelectorCache("Id", "Code")
    Private _selectorSubGroup As SelectorCache = New SelectorCache("Id", "Code")
    Private _selectorThirdParty As SelectorCache = New SelectorCache("Id", "Nit")
    Private _selectorFunctionalUnit As SelectorCache = New SelectorCache("Id", "Codigo")
    Private _selectorSourceWarehouse As SelectorCache = New SelectorCache("Id", "Code")
    Private _selectorTargetWarehouse As SelectorCache = New SelectorCache("Id", "Code")
    Private _selectorDocument As SelectorCache = New SelectorCache("Id", "Code")


    Private Sub INDGv_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvProduct.CustomUnboundColumnData, INDGvGroup.CustomUnboundColumnData, INDGvSubGroup.CustomUnboundColumnData, INDGvThirdParty.CustomUnboundColumnData, INDGvFuncionalUnit.CustomUnboundColumnData, INDGvWareHouse.CustomUnboundColumnData, INDGvDocument.CustomUnboundColumnData, INDGvSourceWarehouse.CustomUnboundColumnData
        If e.IsGetData Then
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvProduct" Then
                e.Value = _selectorProduct.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvGroup" Then
                e.Value = _selectorGroup.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvSubGroup" Then
                e.Value = _selectorSubGroup.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvThirdParty" Then
                e.Value = _selectorThirdParty.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvFuncionalUnit" Then
                e.Value = _selectorFunctionalUnit.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvSourceWarehouse" Then
                e.Value = _selectorSourceWarehouse.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvWareHouse" Then
                e.Value = _selectorTargetWarehouse.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvDocument" Then
                e.Value = _selectorDocument.ValidateExistsRow(e.Row)

            End If
        End If
    End Sub

    Private Sub INDGv_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles INDGvProduct.RowCellClick, INDGvGroup.RowCellClick, INDGvSubGroup.RowCellClick, INDGvThirdParty.RowCellClick, INDGvFuncionalUnit.RowCellClick, INDGvWareHouse.RowCellClick, INDGvDocument.RowCellClick, INDGvSourceWarehouse.RowCellClick
        If e.Column.FieldName.Contains("UnboundSelection") Then
            Dim selector As SelectorCache = Nothing
            Dim view = TryCast(sender, GridView)

            If view.Name = "INDGvProduct" Then
                selector = _selectorProduct
            ElseIf view.Name = "INDGvGroup" Then
                selector = _selectorGroup
            ElseIf view.Name = "INDGvSubGroup" Then
                selector = _selectorSubGroup
            ElseIf view.Name = "INDGvThirdParty" Then
                selector = _selectorThirdParty
            ElseIf view.Name = "INDGvFuncionalUnit" Then
                selector = _selectorFunctionalUnit
            ElseIf view.Name = "INDGvSourceWarehouse" Then
                selector = _selectorSourceWarehouse
            ElseIf view.Name = "INDGvWareHouse" Then
                selector = _selectorTargetWarehouse
            ElseIf view.Name = "INDGvDocument" Then
                selector = _selectorDocument
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

    Private Sub INDSle_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDSleProduct.Closed, INDSleGroup.Closed, INDSleSubGroup.Closed, INDSleThirdParty.Closed, INDSleFunctionalUnit.Closed, INDSleWarehouse.Closed, INDSleDocument.Closed, INDSleSourceWarehouse.Closed
        Dim searchLookupEdit = TryCast(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        searchLookupEdit.EditValue = Nothing
        If searchLookupEdit.Name = "INDSleProduct" Then
            searchLookupEdit.Properties.NullText = _selectorProduct.ToString()
        ElseIf searchLookupEdit.Name = "INDSleGroup" Then
            searchLookupEdit.Properties.NullText = _selectorGroup.ToString()
        ElseIf searchLookupEdit.Name = "INDSleSubGroup" Then
            searchLookupEdit.Properties.NullText = _selectorSubGroup.ToString()
        ElseIf searchLookupEdit.Name = "INDSleThirdParty" Then
            searchLookupEdit.Properties.NullText = _selectorThirdParty.ToString()
        ElseIf searchLookupEdit.Name = "INDSleThirdParty" Then
            searchLookupEdit.Properties.NullText = _selectorThirdParty.ToString()
        ElseIf searchLookupEdit.Name = "INDSleFunctionalUnit" Then
            searchLookupEdit.Properties.NullText = _selectorFunctionalUnit.ToString()
        ElseIf searchLookupEdit.Name = "INDSleFunctionalUnit" Then
            searchLookupEdit.Properties.NullText = _selectorFunctionalUnit.ToString()
        ElseIf searchLookupEdit.Name = "INDSleSourceWarehouse" Then
            searchLookupEdit.Properties.NullText = _selectorSourceWarehouse.ToString()
        ElseIf searchLookupEdit.Name = "INDSleWarehouse" Then
            searchLookupEdit.Properties.NullText = _selectorTargetWarehouse.ToString()
        ElseIf searchLookupEdit.Name = "INDSleDocument" Then
            searchLookupEdit.Properties.NullText = _selectorDocument.ToString()
        End If
    End Sub

#End Region

#Region "ToExcel"

    ''' <summary>
    ''' creamos un datatable para generar el excel detallado
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargueDatasource()
        Try
            Dim filter As String = "GetDate(TransferOrderId.DocumentDate) >= #" & Format(INDDateStart.EditValue, "yyyy-MM-dd") & "# AND GetDate(TransferOrderId.DocumentDate) <= #" & Format(INDDateEnd.EditValue, "yyyy-MM-dd") & "#"

            If INDGleState.EditValue <> 4 Then
                filter &= " And TransferOrderId.Status = " & INDGleState.EditValue
            End If

            If Not String.IsNullOrEmpty(INDTxtDetail.EditValue) Then
                filter &= " And TransferOrderId.Description LIKE '" & INDTxtDetail.EditValue & "'"
            End If

            If Not String.IsNullOrEmpty(_selectorProduct.GetKeys()) Then
                filter &= String.Format(" AND ProductId.Id IN ({0})", _selectorProduct.GetKeys())
            End If

            If Not String.IsNullOrEmpty(_selectorGroup.GetKeys()) Then
                filter &= String.Format(" AND ProductId.ProductGroupId.Id IN ({0})", _selectorGroup.GetKeys())
            End If

            If Not String.IsNullOrEmpty(_selectorSubGroup.GetKeys()) Then
                filter &= String.Format(" AND ProductId.ProductSubGroupId.Id IN ({0})", _selectorSubGroup.GetKeys())
            End If

            If Not String.IsNullOrEmpty(_selectorThirdParty.GetKeys()) Then
                filter &= String.Format(" AND TransferOrderId.ThirdPartyId.Id In ({0})", _selectorThirdParty.GetKeys())
            End If

            If Not String.IsNullOrEmpty(_selectorSourceWarehouse.GetKeys()) Then
                filter &= String.Format(" AND TransferOrderId.SourceWarehouseId.Id IN ({0})", _selectorSourceWarehouse.GetKeys())
            End If

            If Not String.IsNullOrEmpty(_selectorTargetWarehouse.GetKeys()) Then
                filter &= String.Format(" AND TransferOrderId.TargetWarehouseId.Id IN ({0})", _selectorTargetWarehouse.GetKeys())
            End If

            If Not String.IsNullOrEmpty(_selectorFunctionalUnit.GetKeys()) Then
                filter &= String.Format(" AND TransferOrderId.TargetFunctionalUnitId.Id IN ({0})", _selectorFunctionalUnit.GetKeys())
            End If

            If Not String.IsNullOrEmpty(_selectorDocument.GetKeys()) Then
                filter &= String.Format(" AND TransferOrderId.Id In ({0})", _selectorDocument.GetKeys())
            End If

            Dim listTransferOrder = XpoServiceEx.Instance(indigo.TransactionalContainer).BillingService.GetCollection(Of InventoryTransferOrderDetailReportXpo)(Nothing, filter)
            If listTransferOrder IsNot Nothing AndAlso listTransferOrder.Count > 0 Then
                Dim dt As New DataTable
                dt.Columns.Add("Código")
                dt.Columns.Add("Fecha")
                dt.Columns.Add("Producto")
                dt.Columns.Add("Tipo Orden")
                dt.Columns.Add("Almacen Origen")
                dt.Columns.Add("Almacen Destino")
                dt.Columns.Add("Tercero")
                dt.Columns.Add("Estado")
                dt.Columns.Add("Cantidad")
                dt.Columns.Add("Total", GetType(Decimal))

                For Each itemView In listTransferOrder
                    Dim SourceWarehouse As String
                    SourceWarehouse = Nothing
                    If itemView.TransferOrderId.DispatchTo = 1 Then
                        SourceWarehouse = itemView.TransferOrderId.SourceWarehouseId.CodeName
                    ElseIf itemView.TransferOrderId.DispatchTo = 2 Then
                        SourceWarehouse = itemView.TransferOrderId.SourceWarehouseId.CodeName
                    End If

                    Dim TargetWarehouse As String
                    TargetWarehouse = Nothing
                    If itemView.TransferOrderId.DispatchTo = 1 Then
                        TargetWarehouse = itemView.TransferOrderId.TargetWarehouseId.CodeName
                    ElseIf itemView.TransferOrderId.DispatchTo = 2 Then
                        TargetWarehouse = itemView.TransferOrderId.TargetFunctionalUnitId.CodeName
                    End If

                    Dim TerceroNit As String
                    If itemView.TransferOrderId.ThirdPartyId Is Nothing Then
                        TerceroNit = ""
                    Else
                        TerceroNit = itemView.TransferOrderId.ThirdPartyId.NitName
                    End If

                    Dim row As DataRow = dt.NewRow()
                    row.Item("Código") = itemView.TransferOrderId.Code
                    row.Item("Fecha") = itemView.TransferOrderId.DocumentDate
                    row.Item("Producto") = itemView.ProductId.CodeName
                    row.Item("Tipo Orden") = itemView.TransferOrderId.OrderTypeName
                    row.Item("Almacen Origen") = SourceWarehouse
                    row.Item("Almacen Destino") = TargetWarehouse
                    row.Item("Tercero") = TerceroNit
                    row.Item("Estado") = itemView.TransferOrderId.StatusName
                    row.Item("Cantidad") = itemView.Quantity
                    row.Item("Total") = itemView.Value * itemView.Quantity

                    dt.Rows.Add(row)
                Next
                INDGcExportExcel.DataSource = dt
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
        Dim _gridView = Me.INDGcExportExcel
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

    ''' <summary>
    ''' Evento Load del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportTransferOrder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Cargar GridLookUpEdit
        Me.INDGleTypeReport.Properties.DataSource = FillingTypeReport
        Me.INDGleTypeFilter.Properties.DataSource = FillingTypeFilter
        Me.INDGleGroupBy.Properties.DataSource = FillingGroupBy
        Me.INDGleState.Properties.DataSource = FillingState

        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleTypeReport.EditValue = 1
        Me.INDGleTypeFilter.EditValue = 1
        Me.INDGleGroupBy.EditValue = 1
        Me.INDGleState.EditValue = 4
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _FillingGroupBy = Nothing
        _FillingState = Nothing
        _FillingTypeFilter = Nothing
        _FillingTypeReport = Nothing
    End Sub

#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' cargamos el datasource del search de tercero 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleThirdParty_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleThirdParty.QueryPopUp
        If INDSleThirdParty.Properties.DataSource Is Nothing Then
            INDSleThirdParty.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).AccountingService.ListThirdPartyReport()
        End If
    End Sub

    ''' <summary>
    ''' Cargamos el datasource del search de unidad funcional 
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
    Private Sub INDSleSourceWarehouse_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleSourceWarehouse.QueryPopUp
        If INDSleSourceWarehouse.Properties.DataSource Is Nothing Then
            INDSleSourceWarehouse.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListWarehouseReport()
        End If
    End Sub

    ''' <summary>
    ''' Cargamos el datasource del search de almacen destino
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleTargetWarehouse_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleWarehouse.QueryPopUp
        If INDSleWarehouse.Properties.DataSource Is Nothing Then
            INDSleWarehouse.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListWarehouseReport()
        End If
    End Sub

    ''' <summary>
    ''' cargamos el datasource del search del product
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
    ''' Cargamos el datasource del search de INDSleGroup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleGroup_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleGroup.QueryPopUp
        If INDSleGroup.Properties.DataSource Is Nothing Then
            INDSleGroup.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListGroupReport()
        End If
    End Sub

    ''' <summary>
    ''' Cargamos el datasource del search de INDSleSubGroup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleSubGroup_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleSubGroup.QueryPopUp
        If INDSleSubGroup.Properties.DataSource Is Nothing Then
            INDSleSubGroup.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListSubGroupReport()
        End If
    End Sub

    ''' <summary>
    ''' Cargamos el datasource del search de documento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleDocument_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleDocument.QueryPopUp
        If INDSleDocument.Properties.DataSource Is Nothing Then
            INDSleDocument.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListTransferOrderByFilter(Nothing)
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGleTypeFilter_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleTypeFilter.EditValueChanged
        If INDGleTypeFilter.EditValue = 1 Then
            INDLblFunctionalUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLblTargetWarehouse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            _selectorFunctionalUnit = New SelectorCache("Id", "Codigo")
        Else
            INDLblFunctionalUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLblTargetWarehouse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            _selectorTargetWarehouse = New SelectorCache("Id", "Code")
        End If
    End Sub

    Private Sub INDGleTypeReport_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleTypeReport.EditValueChanged
        If INDGleTypeReport.EditValue = 1 Then
            Me.INDGleGroupBy.EditValue = 1
            INDLciGroupBy.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDLciGroupBy.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDGleGroupBy.EditValue = Nothing
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
                Dim reporte As New rptListTransferOrder
                reporte.ParametrosReporte = New Object() {INDDateStart.EditValue,
                                                          INDDateEnd.EditValue,
                                                          INDGleTypeFilter.EditValue,
                                                          INDGleState.EditValue,
                                                          INDGleGroupBy.EditValue,
                                                          INDTxtDetail.EditValue,
                                                          _selectorProduct.GetKeys(),
                                                          _selectorGroup.GetKeys(),
                                                          _selectorSubGroup.GetKeys(),
                                                          _selectorThirdParty.GetKeys(),
                                                          _selectorFunctionalUnit.GetKeys(),
                                                          _selectorSourceWarehouse.GetKeys(),
                                                          _selectorTargetWarehouse.GetKeys(),
                                                          _selectorDocument.GetKeys()
                                                          }
                INDDvDocumentViewer.DocumentSource = reporte
                Await reporte.CargarDataSourceAsync()
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
                AsyncLoader(False)
            Else

                AsyncLoader(True)
                Dim reporte As New rptSubTransferOrder
                reporte.ParametrosReporte = New Object() {INDDateStart.EditValue,
                                                          INDDateEnd.EditValue,
                                                          INDGleTypeFilter.EditValue,
                                                          INDGleState.EditValue,
                                                          INDGleGroupBy.EditValue,
                                                          INDTxtDetail.EditValue,
                                                          _selectorProduct.GetKeys(),
                                                          _selectorGroup.GetKeys(),
                                                          _selectorSubGroup.GetKeys(),
                                                          _selectorThirdParty.GetKeys(),
                                                          _selectorFunctionalUnit.GetKeys(),
                                                          _selectorSourceWarehouse.GetKeys(),
                                                          _selectorTargetWarehouse.GetKeys(),
                                                          _selectorDocument.GetKeys()
                                                          }
                INDDvDocumentViewer.DocumentSource = reporte
                Await reporte.CargarDataSourceAsync()
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
                AsyncLoader(False)
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
        Me.INDDateStart.Focus()
    End Sub

    ''' <summary>
    ''' se ejecuta para exportar a excel el reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReports_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReports.Click
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