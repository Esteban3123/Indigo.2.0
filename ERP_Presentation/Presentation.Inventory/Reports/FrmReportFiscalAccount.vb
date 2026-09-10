#Region "Imports"

Imports System.Text
Imports DevExpress.Xpo
Imports DevExpress.XtraGrid.Views.Grid
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Presentation.Base
Imports Presentation.Inventory.MVP
Imports Presentation.Reporter

#End Region

Public Class FrmReportFiscalAccount

#Region "Variables"

    Private criterias As Dictionary(Of String, String)

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

    Private _FillingMovementType As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingMovementType As List(Of Tuple(Of Integer, String))
        Get
            If _FillingMovementType Is Nothing Then
                _FillingMovementType = New List(Of Tuple(Of Integer, String))
                _FillingMovementType.Add(New Tuple(Of Integer, String)(1, "Entradas"))
                _FillingMovementType.Add(New Tuple(Of Integer, String)(2, "Salidas"))
            End If
            Return _FillingMovementType
        End Get
    End Property

    Private _FillingGroupBy As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingGroupBy As List(Of Tuple(Of Integer, String))
        Get
            If _FillingGroupBy Is Nothing Then
                _FillingGroupBy = New List(Of Tuple(Of Integer, String))
                _FillingGroupBy.Add(New Tuple(Of Integer, String)(1, "Grupo"))
                _FillingGroupBy.Add(New Tuple(Of Integer, String)(2, "SubGrupo"))
                _FillingGroupBy.Add(New Tuple(Of Integer, String)(3, "Grupo y SubGrupo"))
            End If
            Return _FillingGroupBy
        End Get
    End Property

    Private _FillingIncludeWarehouseTransferOrder As List(Of Tuple(Of Boolean, String))
    Private ReadOnly Property FillingIncludeWarehouseTransferOrder As List(Of Tuple(Of Boolean, String))
        Get
            If _FillingIncludeWarehouseTransferOrder Is Nothing Then
                _FillingIncludeWarehouseTransferOrder = New List(Of Tuple(Of Boolean, String))
                _FillingIncludeWarehouseTransferOrder.Add(New Tuple(Of Boolean, String)(True, "Si"))
                _FillingIncludeWarehouseTransferOrder.Add(New Tuple(Of Boolean, String)(False, "No"))
            End If
            Return _FillingIncludeWarehouseTransferOrder
        End Get
    End Property

    Private Property WarehouseXpo As XPInstantFeedbackSource
        Get
            Return INDSleWarehouse.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleWarehouse.Properties.DataSource = value
        End Set
    End Property

    Private Property GroupXpo As XPInstantFeedbackSource
        Get
            Return INDSleGroup.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleGroup.Properties.DataSource = value
        End Set
    End Property

    Private Property SubGroupXpo As XPInstantFeedbackSource
        Get
            Return INDSleSubGroup.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleSubGroup.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "BarraBotones"

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

#End Region

#Region "Methods"

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

    Private Async Function LoadControls() As Task
        Using Model As New MSettingInventory(CStr(Me.Tag))
            AsyncLoader(True)
            Try
                Dim resulOperation = Await Model.GetInventorySettingsRegister(Me.BarraBotones.OperatingUnitValue)
                If resulOperation.ObjectEmbbeded Is Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SettingParameter", "Inventory"))
                    Exit Function
                End If

                Dim _inventorySettings = resulOperation.ObjectEmbbeded
                INDCdnDate.SetMonth = _inventorySettings.Month
                INDCdnDate.SetYear = _inventorySettings.Year
            Catch ex As Exception
                Throw ex
            Finally
                AsyncLoader(False)
            End Try
        End Using
    End Function

    Private Function ValidateControlsReports()
        Dim errors As New StringBuilder

        If Not {1, 2}.Contains(INDGleTypeReport.EditValue) Then
            errors.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName", "Commons"), INDLciTypeReport.Text))
            Me.INDGleTypeReport.Focus()
        End If

        If INDGleIncludeWarehouseTransferOrder.EditValue Is Nothing Then
            errors.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName", "Commons"), INDLciIncludeWarehouseTransferOrder.Text))
            Me.INDGleIncludeWarehouseTransferOrder.Focus()
        End If

        If INDGleMovementType.EditValue Is Nothing Then
            errors.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName", "Commons"), INDLciMovementType.Text))
            Me.INDGleMovementType.Focus()
        End If

        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Return False
        End If

        criterias = New Dictionary(Of String, String)
        criterias.Add("Year", INDCdnDate.GetYear)
        criterias.Add("Month", INDCdnDate.GetMonth)
        criterias.Add("TypeReport", INDGleTypeReport.EditValue)
        criterias.Add("MovementType", INDGleMovementType.EditValue)
        criterias.Add("GroupBy", INDGleGroupBy.EditValue)
        criterias.Add("IncludeWarehouseTransferOrder", INDGleIncludeWarehouseTransferOrder.EditValue)
        criterias.Add("Warehouses", _selectorWarehouse.GetKeys())
        criterias.Add("Groups", _selectorGroup.GetKeys())
        criterias.Add("SubGroups", _selectorSubGroup.GetKeys())

        Return True
    End Function

#Region "Selector"

    Private _selectorWarehouse As SelectorCache = New SelectorCache("Id", "Code")
    Private _selectorGroup As SelectorCache = New SelectorCache("Id", "Code")
    Private _selectorSubGroup As SelectorCache = New SelectorCache("Id", "Code")

    Private Sub INDGvSelector_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvWarehouse.CustomUnboundColumnData, INDGvGroup.CustomUnboundColumnData, INDgvSubGroup.CustomUnboundColumnData
        If e.IsGetData Then
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvWarehouse" Then
                e.Value = _selectorWarehouse.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvGroup" Then
                e.Value = _selectorGroup.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDgvSubGroup" Then
                e.Value = _selectorSubGroup.ValidateExistsRow(e.Row)
            End If
        End If
    End Sub

    Private Sub INDGvSelector_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles INDGvWarehouse.RowCellClick, INDGvGroup.RowCellClick, INDgvSubGroup.RowCellClick
        If e.Column.FieldName.Contains("UnboundSelection") Then
            Dim selector As SelectorCache = Nothing
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvWarehouse" Then
                selector = _selectorWarehouse
            ElseIf view.Name = "INDGvGroup" Then
                selector = _selectorGroup
            ElseIf view.Name = "INDgvSubGroup" Then
                selector = _selectorSubGroup
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

    Private Sub INDSleSelector_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDSleWarehouse.Closed, INDSleGroup.Closed, INDSleSubGroup.Closed
        Dim searchLookupEdit = TryCast(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        searchLookupEdit.EditValue = Nothing
        If searchLookupEdit.Name = "INDSleWarehouse" Then
            searchLookupEdit.Properties.NullText = _selectorWarehouse.ToString()
        ElseIf searchLookupEdit.Name = "INDSleGroup" Then
            searchLookupEdit.Properties.NullText = _selectorGroup.ToString()
        ElseIf searchLookupEdit.Name = "INDSleSubGroup" Then
            searchLookupEdit.Properties.NullText = _selectorSubGroup.ToString()
        End If
    End Sub

#End Region

#Region "ToExcel"

    Private Sub chargueDataSourceSummary(ByVal dtReportFiscalAccount As DataTable)
        Dim dt As New DataTable
        dt.Columns.Add("Almacén Código")
        dt.Columns.Add("Almacén Nombre")
        If {1, 3}.Contains(INDGleMovementType.EditValue) Then
            dt.Columns.Add("Grupo Código")
            dt.Columns.Add("Grupo Nombre")
        End If
        If {2, 3}.Contains(INDGleMovementType.EditValue) Then
            dt.Columns.Add("SubGrupo Código")
            dt.Columns.Add("SubGrupo Nombre")
        End If
        dt.Columns.Add("Saldo Anterior", GetType(Decimal))
        dt.Columns.Add("Inventario Físico", GetType(Decimal))
        dt.Columns.Add("Movimiento Entrada", GetType(Decimal))
        dt.Columns.Add("Movimiento Salida", GetType(Decimal))
        dt.Columns.Add("Nuevo Saldo", GetType(Decimal))
        dt.Columns.Add("Saldo Final", GetType(Decimal))

        For Each item In dtReportFiscalAccount.Rows
            Dim row As DataRow = dt.NewRow
            row.Item("Almacén Código") = item("WarehouseCode")
            row.Item("Almacén Nombre") = item("WarehouseName")
            If {1, 3}.Contains(INDGleMovementType.EditValue) Then
                row.Item("Grupo Código") = item("GroupCode")
                row.Item("Grupo Nombre") = item("GroupName")
            End If
            If {2, 3}.Contains(INDGleMovementType.EditValue) Then
                row.Item("SubGrupo Código") = item("SubGroupCode")
                row.Item("SubGrupo Nombre") = item("SubGroupName")
            End If
            row.Item("Saldo Anterior") = item("PreviousBalance")
            row.Item("Inventario Físico") = item("PhysicalInventory")
            row.Item("Movimiento Entrada") = item("MovementIn")
            row.Item("Movimiento Salida") = item("MovementOut")
            row.Item("Nuevo Saldo") = item("PreviousBalance") + item("MovementIn") - item("MovementOut")
            row.Item("Saldo Final") = item("Balance")
            dt.Rows.Add(row)
        Next
        INDGcExportExcel.DataSource = dt
    End Sub

    Private Sub chargueDataSourceDetailed(ByVal dtReportFiscalAccount As DataTable)
        Dim dt As New DataTable
        dt.Columns.Add("Almacén Código")
        dt.Columns.Add("Almacén Nombre")
        If {1, 3}.Contains(INDGleMovementType.EditValue) Then
            dt.Columns.Add("Grupo Código")
            dt.Columns.Add("Grupo Nombre")
        End If
        If {2, 3}.Contains(INDGleMovementType.EditValue) Then
            dt.Columns.Add("SubGrupo Código")
            dt.Columns.Add("SubGrupo Nombre")
        End If
        dt.Columns.Add("Tipo Documento")
        dt.Columns.Add("Nro Documento")
        dt.Columns.Add("Descripción")
        dt.Columns.Add("Valor", GetType(Decimal))

        For Each item In dtReportFiscalAccount.Rows
            Dim row As DataRow = dt.NewRow
            row.Item("Almacén Código") = item("WarehouseCode")
            row.Item("Almacén Nombre") = item("WarehouseName")
            If {1, 3}.Contains(INDGleMovementType.EditValue) Then
                row.Item("Grupo Código") = item("GroupCode")
                row.Item("Grupo Nombre") = item("GroupName")
            End If
            If {2, 3}.Contains(INDGleMovementType.EditValue) Then
                row.Item("SubGrupo Código") = item("SubGroupCode")
                row.Item("SubGrupo Nombre") = item("SubGroupName")
            End If
            row.Item("Tipo Documento") = item("EntityName")
            row.Item("Nro Documento") = item("EntityCode")
            row.Item("Descripción") = item("Description")
            row.Item("Valor") = item("Value")
            dt.Rows.Add(row)
        Next
        INDGcExportExcel.DataSource = dt
    End Sub

    Private Sub generateExcel()
        Dim _gridView = Me.INDGcExportExcel
        _gridView.MainView.PopulateColumns()
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

    Private Async Sub FrmReportFiscalAccount_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Cargar GridLookUpEdit        
        INDGleTypeReport.Properties.DataSource = FillingTypeReport
        INDGleMovementType.Properties.DataSource = FillingMovementType
        INDGleGroupBy.Properties.DataSource = FillingGroupBy
        INDGleIncludeWarehouseTransferOrder.Properties.DataSource = FillingIncludeWarehouseTransferOrder

        'Dar un valores por defecto
        INDGleTypeReport.EditValue = 1
        INDGleMovementType.EditValue = 1
        INDGleGroupBy.EditValue = 1
        INDGleIncludeWarehouseTransferOrder.EditValue = True

        'Cargar
        Await LoadControls()
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _FillingIncludeWarehouseTransferOrder = Nothing
        _FillingMovementType = Nothing
    End Sub

#End Region

#Region "QueryPopup"

    Private Sub INDSleWarehouse_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleWarehouse.QueryPopUp
        If WarehouseXpo Is Nothing Then
            WarehouseXpo = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListNoVirtualWarehouseByStatusAndUser(True, Nothing)
        End If
    End Sub

    Private Sub INDSleGroup_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleGroup.QueryPopUp
        If GroupXpo Is Nothing Then
            GroupXpo = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListProductGroup()
        End If
    End Sub

    Private Sub INDSleSubGroup_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleSubGroup.QueryPopUp
        If SubGroupXpo Is Nothing Then
            SubGroupXpo = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListProductSubGroup()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDGleTypeReport_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleTypeReport.EditValueChanged
        INDLciMovementType.Visibility = If(INDGleTypeReport.EditValue = 1, DevExpress.XtraLayout.Utils.LayoutVisibility.Never, DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
    End Sub

#End Region

#Region "Report"

    ''' <summary>
    ''' se ejecuta en el evento click del boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenrateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenrateReport.Click
        If Me.ValidateControlsReports = True Then
            AsyncLoader(True)
            Dim reporte As Object = Nothing

            If INDGleTypeReport.EditValue = 1 Then
                reporte = New rptReportFiscalAccountSummary()
            ElseIf INDGleTypeReport.EditValue = 2 Then
                reporte = New rptReportFiscalAccountDetailed()
            End If

            reporte.ParametrosReporte = New Object() {criterias}
            INDDvReport.DocumentSource = reporte
            Await reporte.CargarDataSourceAsync()
            AsyncLoader(False)
            If reporte.DataSource IsNot Nothing Then
                reporte.CreateDocument(True)
                Me.INDLcBase.Visible = False
                Me.INDCncNavigation.Visible = False
                Me.INDPcReport.Visible = True
                INDDvReport.Show()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                Me.INDCdnDate.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento ClickBack en el control INDCnReturn
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnNavigationReport_ClickBack() Handles INDCnNavigationReport.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncNavigation.Visible = True
        Me.INDPcReport.Visible = False
    End Sub

    Private Async Sub INDSbGenerateReports_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReports.Click
        If Me.ValidateControlsReports = True Then
            Try
                AsyncLoader(True)
                Using model As New Presentation.Inventory.MVP.MReports(Me.Tag)
                    Dim ds As DataSet = Await model.GetReportFiscalAccount(criterias)
                    If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                        Dim dtReportFiscalAccount As DataTable = ds.Tables("ReportFiscalAccount")

                        Await Task.Factory.StartNew(Sub()
                                                        If INDGleTypeReport.EditValue = 1 Then
                                                            chargueDataSourceSummary(dtReportFiscalAccount)
                                                        ElseIf INDGleTypeReport.EditValue = 2 Then
                                                            chargueDataSourceDetailed(dtReportFiscalAccount)
                                                        End If
                                                    End Sub)

                        If Me.INDGcExportExcel.DataSource IsNot Nothing Then
                            generateExcel()
                        End If
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    End If
                End Using
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