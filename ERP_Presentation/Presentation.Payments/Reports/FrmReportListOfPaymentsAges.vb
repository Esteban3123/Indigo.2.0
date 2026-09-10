#Region "Imports"

Imports System.Text
Imports DevExpress.XtraGrid.Views.Grid
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PaymentsRepository
Imports Presentation.Base
Imports Presentation.CloudAgent
Imports Presentation.Reporter

#End Region

Public Class FrmReportListOfPaymentsAges

#Region "Variables"

    Private _criterias As Dictionary(Of String, String)
    Private _filters As Dictionary(Of String, String)

#End Region

#Region "Datasources"

    Private _FillingTypeReport As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingTypeReport As List(Of Tuple(Of Integer, String))
        Get
            If _FillingTypeReport Is Nothing Then
                _FillingTypeReport = New List(Of Tuple(Of Integer, String))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(1, "Detallado"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(2, "Resumido"))
            End If
            Return _FillingTypeReport
        End Get
    End Property

    Private _FillingIncludeZero As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingIncludeZero As List(Of Tuple(Of Integer, String))
        Get
            If _FillingIncludeZero Is Nothing Then
                _FillingIncludeZero = New List(Of Tuple(Of Integer, String))
                _FillingIncludeZero.Add(New Tuple(Of Integer, String)(1, "Si"))
                _FillingIncludeZero.Add(New Tuple(Of Integer, String)(2, "No"))
            End If
            Return _FillingIncludeZero
        End Get
    End Property

    Private _FillingGroupBy As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingGroupBy As List(Of Tuple(Of Integer, String))
        Get
            If _FillingGroupBy Is Nothing Then
                _FillingGroupBy = New List(Of Tuple(Of Integer, String))
                _FillingGroupBy.Add(New Tuple(Of Integer, String)(1, "Tercero"))
                _FillingGroupBy.Add(New Tuple(Of Integer, String)(2, "Línea de Distribución"))
            End If
            Return _FillingGroupBy
        End Get
    End Property

    Private _FillingOrderBy As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingOrderBy As List(Of Tuple(Of Integer, String))
        Get
            If _FillingOrderBy Is Nothing Then
                _FillingOrderBy = New List(Of Tuple(Of Integer, String))
                If INDGleGroupBy.EditValue = 1 Then
                    _FillingOrderBy.Add(New Tuple(Of Integer, String)(1, "Codigo Proveedor"))
                    _FillingOrderBy.Add(New Tuple(Of Integer, String)(2, "Nombre Proveedor"))
                ElseIf INDGleGroupBy.EditValue = 2 Then
                    _FillingOrderBy.Add(New Tuple(Of Integer, String)(3, "Codigo Linea Distribucion"))
                    _FillingOrderBy.Add(New Tuple(Of Integer, String)(4, "Nombre Linea Distribucion"))
                End If
                If INDGleTypeReport.EditValue = 1 Then
                    _FillingOrderBy.Add(New Tuple(Of Integer, String)(5, "Fecha Factura"))
                    _FillingOrderBy.Add(New Tuple(Of Integer, String)(6, "Edad"))
                End If
            End If
            Return _FillingOrderBy
        End Get
    End Property

#End Region

#Region "BarraBotones"

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

#End Region

#Region "Propiedades"

    Public Property ValoritationCurrencyId As Integer?
        Get
            Return INDSleValoritation.EditValue
        End Get
        Set(value As Integer?)
            INDSleValoritation.EditValue = value
        End Set
    End Property
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

    Private Function ValidateControlsReports()
        Dim errors As New StringBuilder

        If INDDClosingDate.EditValue Is Nothing Then
            errors.AppendLine(String.Format(ResourceManager.GetString("ValidateClosingDateReport", "Commons")))
            Me.INDDClosingDate.Focus()
        End If

        If String.IsNullOrEmpty(_selectorOperatingUnit.GetKeys()) Then
            errors.AppendLine("Debe seleccionar al menos una unidad operativa")
            Me.INDSleOperatingUnit.Focus()
        End If

        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Return False
        End If

        _criterias = New Dictionary(Of String, String)
        _criterias.Add("TypeReport", INDGleTypeReport.EditValue)
        _criterias.Add("IncludeZero", INDGleCero.EditValue)
        _criterias.Add("GroupBy", INDGleGroupBy.EditValue)
        _criterias.Add("OrderBy", INDGleOrderBy.EditValue)
        If Me.ValoritationCurrencyId IsNot Nothing Then
            _criterias.Add("ValoritationCurrencyId", Me.ValoritationCurrencyId)
        End If

        _filters = New Dictionary(Of String, String)
        _filters.Add("ClosingDate", INDDClosingDate.EditValue)
        _filters.Add("OperatingUnitId", _selectorOperatingUnit.GetKey(0))
        _filters.Add("OperatingUnits", _selectorOperatingUnit.GetKeys())
        _filters.Add("Suppliers", _selectorSupplier.GetKeys())
        _filters.Add("CostCenters", _selectorCostCenter.GetKeys())

        Return True
    End Function

#Region "ToExcel"

    Private Sub chargueDataSourceSummary(ByVal listSettingPayments As List(Of PaymentsAgesPaymentsXpo), ByVal dtReportPaymentsByAge As DataTable)
        Dim dt As New DataTable
        Dim groupedResult As IEnumerable(Of Object) = Nothing
        If INDGleGroupBy.EditValue = 1 Then
            dt.Columns.Add("Identificacion Tercero")
            dt.Columns.Add("Nombre Tercero")

            groupedResult = From t In dtReportPaymentsByAge.AsEnumerable()
                            Group t By Key = New With {
                            Key .ThirdPartyNit = t.Field(Of String)("ThirdPartyNit"),
                            Key .ThirdPartyName = t.Field(Of String)("ThirdPartyName")
                            }
                            Into Group
                            Select New With {
                                .ThirdPartyNit = Key.ThirdPartyNit,
                                .ThirdPartyName = Key.ThirdPartyName
                            }
        Else
            dt.Columns.Add("Codigo Linea de Distribucion")
            dt.Columns.Add("Nombre Linea de Distribucion")

            groupedResult = From t In dtReportPaymentsByAge.AsEnumerable()
                            Group t By Key = New With {
                                Key .DistributionLineCode = t.Field(Of String)("DistributionLineCode"),
                                Key .DistributionLineName = t.Field(Of String)("DistributionLineName")
                            }
                            Into Group
                            Select New With {
                                .DistributionLineCode = Key.DistributionLineCode,
                                .DistributionLineName = Key.DistributionLineName
                            }
        End If

        Dim NameMinimumAgeRange = CType(listSettingPayments(0), PaymentsAgesPaymentsXpo).SettingPaymentId.NameMinimumAgeRange
        Dim RangeMin = listSettingPayments.Min(Function(x) x.InitialRange)
        dt.Columns.Add(NameMinimumAgeRange, GetType(Decimal))
        For Each item In listSettingPayments
            dt.Columns.Add(item.Name, GetType(Decimal))
        Next
        Dim NameMaximumAgeRange = CType(listSettingPayments(0), PaymentsAgesPaymentsXpo).SettingPaymentId.NameMaximumAgeRange
        Dim MaximunAgeRange = CType(listSettingPayments(0), PaymentsAgesPaymentsXpo).SettingPaymentId.MaximunAgeRange
        NameMaximumAgeRange = If(dt.Columns.Contains(NameMaximumAgeRange), String.Concat(NameMaximumAgeRange, "(1)"), NameMaximumAgeRange)
        dt.Columns.Add(NameMaximumAgeRange, GetType(Decimal))

        dt.Columns.Add("Valor Inicial", GetType(Decimal))
        dt.Columns.Add("Movimiento Inicial", GetType(Decimal))
        dt.Columns.Add("Notas Débito", GetType(Decimal))
        dt.Columns.Add("Notas Crédito", GetType(Decimal))
        dt.Columns.Add("Transferencias", GetType(Decimal))
        dt.Columns.Add("Comprobantes de Egreso", GetType(Decimal))
        dt.Columns.Add("Reintegros", GetType(Decimal))
        dt.Columns.Add("Cruces", GetType(Decimal))
        dt.Columns.Add("Saldo", GetType(Decimal))

        For Each item In groupedResult
            Dim row As DataRow = dt.NewRow()
            Dim filterResult As List(Of DataRow) = Nothing
            If INDGleGroupBy.EditValue = 1 Then
                filterResult = dtReportPaymentsByAge.AsEnumerable().Where(Function(t) t.Field(Of String)("ThirdPartyNit") = item.ThirdPartyNit AndAlso t.Field(Of String)("ThirdPartyName") = item.ThirdPartyName).ToList()
                row.Item("Identificacion Tercero") = item.ThirdPartyNit
                row.Item("Nombre Tercero") = item.ThirdPartyName
            Else
                filterResult = dtReportPaymentsByAge.AsEnumerable().Where(Function(t) t.Field(Of String)("DistributionLineCode") = item.DistributionLineCode AndAlso t.Field(Of String)("DistributionLineName") = item.DistributionLineName).ToList()
                row.Item("Codigo Linea de Distribucion") = item.DistributionLineCode
                row.Item("Nombre Linea de Distribucion") = item.DistributionLineName
            End If


            Dim value As Decimal = filterResult.Where(Function(d) d.Field(Of Integer)("Age") < RangeMin).Sum(Function(d) d.Field(Of Decimal)("Balance"))
            row.Item(NameMinimumAgeRange) = value
            For Each itemSettings In listSettingPayments
                value = filterResult.Where(Function(d) d.Field(Of Integer)("Age") >= itemSettings.InitialRange AndAlso d.Field(Of Integer)("Age") <= itemSettings.EndRange).Sum(Function(d) d.Field(Of Decimal)("Balance"))
                row.Item(itemSettings.Name) = value
            Next
            value = filterResult.Where(Function(d) d.Field(Of Integer)("Age") > MaximunAgeRange).Sum(Function(d) d.Field(Of Decimal)("Balance"))
            row.Item(NameMaximumAgeRange) = value

            row.Item("Valor Inicial") = filterResult.Sum(Function(d) d.Field(Of Decimal)("DocumentValue"))
            row.Item("Movimiento Inicial") = filterResult.Sum(Function(d) d.Field(Of Decimal)("InitialValue"))
            row.Item("Notas Débito") = filterResult.Sum(Function(d) d.Field(Of Decimal)("DebitValue"))
            row.Item("Notas Crédito") = filterResult.Sum(Function(d) d.Field(Of Decimal)("CreditValue"))
            row.Item("Transferencias") = filterResult.Sum(Function(d) d.Field(Of Decimal)("TransferValue"))
            row.Item("Comprobantes de Egreso") = filterResult.Sum(Function(d) d.Field(Of Decimal)("VoucherTransactionValue"))
            row.Item("Reintegros") = filterResult.Sum(Function(d) d.Field(Of Decimal)("CashReceiptValue"))
            row.Item("Cruces") = filterResult.Sum(Function(d) d.Field(Of Decimal)("CrossingValue"))
            row.Item("Saldo") = filterResult.Sum(Function(d) d.Field(Of Decimal)("Balance"))

            dt.Rows.Add(row)
        Next

        INDGcExportExcell.DataSource = dt
    End Sub

    Private Sub chargueDataSourceDetailed(ByVal listSettingPayments As List(Of PaymentsAgesPaymentsXpo), ByVal dtReportPaymentsByAge As DataTable)
        Dim dt As New DataTable
        dt.Columns.Add("Unidad Operativa")
        dt.Columns.Add("Identificacion Tercero")
        dt.Columns.Add("Nombre Tercero")
        dt.Columns.Add("Numero Factura")
        dt.Columns.Add("Moneda")
        dt.Columns.Add("Fecha Factura", GetType(DateTime))
        dt.Columns.Add("Días Plazo")
        dt.Columns.Add("Fecha Vencimiento", GetType(DateTime))
        dt.Columns.Add("Codigo Linea de Distribucion")
        dt.Columns.Add("Nombre Linea de Distribucion")
        dt.Columns.Add("Numero Cuenta Contable")
        dt.Columns.Add("Nombre Cuenta Contable")
        dt.Columns.Add("Codigo Centro de Costo")
        dt.Columns.Add("Nombre Centro de Costo")
        dt.Columns.Add("Numero Radicado")
        dt.Columns.Add("Fecha Radicado", GetType(DateTime))
        dt.Columns.Add("Usuario Radicado")

        Dim NameMinimumAgeRange = CType(listSettingPayments(0), PaymentsAgesPaymentsXpo).SettingPaymentId.NameMinimumAgeRange
        Dim RangeMin = listSettingPayments.Min(Function(x) x.InitialRange)
        dt.Columns.Add(NameMinimumAgeRange, GetType(Decimal))
        For Each item In listSettingPayments
            dt.Columns.Add(item.Name, GetType(Decimal))
        Next
        Dim NameMaximumAgeRange = CType(listSettingPayments(0), PaymentsAgesPaymentsXpo).SettingPaymentId.NameMaximumAgeRange
        Dim MaximunAgeRange = CType(listSettingPayments(0), PaymentsAgesPaymentsXpo).SettingPaymentId.MaximunAgeRange
        NameMaximumAgeRange = If(dt.Columns.Contains(NameMaximumAgeRange), String.Concat(NameMaximumAgeRange, "(1)"), NameMaximumAgeRange)
        dt.Columns.Add(NameMaximumAgeRange, GetType(Decimal))

        dt.Columns.Add("Valor Facturado", GetType(Decimal))
        dt.Columns.Add("Valor Inicial", GetType(Decimal))
        dt.Columns.Add("Movimiento Inicial", GetType(Decimal))
        dt.Columns.Add("Notas Débito", GetType(Decimal))
        dt.Columns.Add("Notas Crédito", GetType(Decimal))
        dt.Columns.Add("Transferencias", GetType(Decimal))
        dt.Columns.Add("Comprobantes de Egreso", GetType(Decimal))
        dt.Columns.Add("Reintegros", GetType(Decimal))
        dt.Columns.Add("Cruces", GetType(Decimal))
        dt.Columns.Add("Saldo", GetType(Decimal))
        dt.Columns.Add("Origen")

        For Each item In dtReportPaymentsByAge.Rows
            Dim row As DataRow = dt.NewRow()
            row.Item("Unidad Operativa") = item("OperatingUnitName")
            row.Item("Identificacion Tercero") = item("ThirdPartyNit")
            row.Item("Nombre Tercero") = item("ThirdPartyName")
            row.Item("Numero Factura") = item("BillNumber")
            row.Item("Moneda") = item("CurrencyAbbreviation")
            row.Item("Fecha Factura") = CDate(item("BillDate")).AsDate
            row.Item("Días Plazo") = item("Term")
            row.Item("Fecha Vencimiento") = CDate(item("ExpiredDate")).AsDate
            row.Item("Codigo Linea de Distribucion") = item("DistributionLineCode")
            row.Item("Nombre Linea de Distribucion") = item("DistributionLineName")
            row.Item("Numero Cuenta Contable") = item("MainAccountNumber")
            row.Item("Nombre Cuenta Contable") = item("MainAccountName")
            row.Item("Codigo Centro de Costo") = item("CostCenterCode")
            row.Item("Nombre Centro de Costo") = item("CostCenterName")
            row.Item("Numero Radicado") = item("RadicatedConsecutive")
            row.Item("Fecha Radicado") = CDate(item("RadicatedDate")).AsDate
            row.Item("Usuario Radicado") = item("RadicatedUser")

            row.Item(NameMinimumAgeRange) = If(item("Age") < RangeMin, item("Balance"), 0)
            For Each itemSettings In listSettingPayments
                row.Item(itemSettings.Name) = If(item("Age") >= itemSettings.InitialRange AndAlso item("Age") <= itemSettings.EndRange, item("Balance"), 0)
            Next
            row.Item(NameMaximumAgeRange) = If(item("Age") > MaximunAgeRange, item("Balance"), 0)

            row.Item("Valor Facturado") = item("InvoiceValue")
            row.Item("Valor Inicial") = item("DocumentValue")
            row.Item("Movimiento Inicial") = item("InitialValue")
            row.Item("Notas Débito") = item("DebitValue")
            row.Item("Notas Crédito") = item("CreditValue")
            row.Item("Transferencias") = item("TransferValue")
            row.Item("Comprobantes de Egreso") = item("VoucherTransactionValue")
            row.Item("Reintegros") = item("CashReceiptValue")
            row.Item("Cruces") = item("CrossingValue")
            row.Item("Saldo") = item("Balance")
            row.Item("Origen") = item("OriginCodeName")

            dt.Rows.Add(row)
        Next

        INDGcExportExcell.DataSource = dt
    End Sub

    Private Sub generateExcel()
        Dim _gridView = Me.INDGcExportExcell
        _gridView.MainView.PopulateColumns()
        If _gridView IsNot Nothing Then
            Dim fileName As String = System.IO.Path.GetTempFileName() & INDGleTypeReport.EditValue & ".xlsx"
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

    Private Sub FrmReportListOfPaymentsAges_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Cargar GridLookUpEdit
        Me.INDGleTypeReport.Properties.DataSource = FillingTypeReport
        Me.INDGleCero.Properties.DataSource = FillingIncludeZero
        Me.INDGleGroupBy.Properties.DataSource = FillingGroupBy

        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleTypeReport.EditValue = 1
        Me.INDGleCero.EditValue = 2
        Me.INDGleGroupBy.EditValue = 1
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _FillingTypeReport = Nothing
        _FillingIncludeZero = Nothing
        _FillingGroupBy = Nothing
        _FillingOrderBy = Nothing

        _criterias = Nothing
        _filters = Nothing

        _selectorOperatingUnit = Nothing
        _selectorSupplier = Nothing
        _selectorCostCenter = Nothing
    End Sub

#End Region

#Region "QueryPopUp"

    Private Sub INDSleOperatingUnit_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleOperatingUnit.QueryPopUp
        If INDSleOperatingUnit.Properties.DataSource Is Nothing Then
            INDSleOperatingUnit.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).CommonService.ListOperatingUnit()
        End If
    End Sub

    Private Sub INDSleSupplier_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleSupplier.QueryPopUp
        If INDSleSupplier.Properties.DataSource Is Nothing Then
            INDSleSupplier.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).PaymentsService.ListSupplierReport()
        End If
    End Sub

    Private Sub INDSleCostCenter_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleCostCenter.QueryPopUp
        If INDSleCostCenter.Properties.DataSource Is Nothing Then
            INDSleCostCenter.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).AccountingService.ListCostcenterReport()
        End If
    End Sub

#End Region

#Region "Selector"

    Private _selectorOperatingUnit As SelectorCache = New SelectorCache("Id", "UnitCode")
    Private _selectorSupplier As SelectorCache = New SelectorCache("Id", "Code")
    Private _selectorCostCenter As SelectorCache = New SelectorCache("Id", "Code")

    Private Sub INDGvSelector_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvOperatingUnit.CustomUnboundColumnData, INDGvSupplier.CustomUnboundColumnData, INDGvCostCenter.CustomUnboundColumnData
        If e.IsGetData Then
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvOperatingUnit" Then
                e.Value = _selectorOperatingUnit.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvSupplier" Then
                e.Value = _selectorSupplier.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvCostCenter" Then
                e.Value = _selectorCostCenter.ValidateExistsRow(e.Row)
            End If
        End If
    End Sub

    Private Sub INDGvSelector_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles INDGvOperatingUnit.RowCellClick, INDGvSupplier.RowCellClick, INDGvCostCenter.RowCellClick
        If e.Column.FieldName.Contains("UnboundSelection") Then
            Dim selector As SelectorCache = Nothing
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvOperatingUnit" Then
                selector = _selectorOperatingUnit
            ElseIf view.Name = "INDGvSupplier" Then
                selector = _selectorSupplier
            ElseIf view.Name = "INDGvCostCenter" Then
                selector = _selectorCostCenter
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

    Private Sub INDSleSelector_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDSleOperatingUnit.Closed, INDSleSupplier.Closed, INDSleCostCenter.Closed
        Dim searchLookupEdit = TryCast(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        searchLookupEdit.EditValue = Nothing
        If searchLookupEdit.Name = "INDSleOperatingUnit" Then
            searchLookupEdit.Properties.NullText = _selectorOperatingUnit.ToString()
        ElseIf searchLookupEdit.Name = "INDSleSupplier" Then
            searchLookupEdit.Properties.NullText = _selectorSupplier.ToString()
        ElseIf searchLookupEdit.Name = "INDSleCostCenter" Then
            searchLookupEdit.Properties.NullText = _selectorCostCenter.ToString()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDGleTypeReport_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleTypeReport.EditValueChanged
        If String.IsNullOrEmpty(INDGleTypeReport.EditValue) Then
            Exit Sub
        End If

        _FillingOrderBy = Nothing
        Me.INDGleOrderBy.Properties.DataSource = FillingOrderBy
        If INDGleGroupBy.EditValue = 1 Then
            Me.INDGleOrderBy.EditValue = 1
        Else
            Me.INDGleOrderBy.EditValue = 3
        End If
    End Sub

    Private Sub INDGleGroupBy_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleGroupBy.EditValueChanged
        If String.IsNullOrEmpty(INDGleGroupBy.EditValue) Then
            Exit Sub
        End If

        _FillingOrderBy = Nothing
        Me.INDGleOrderBy.Properties.DataSource = FillingOrderBy
        If INDGleGroupBy.EditValue = 1 Then
            Me.INDGleOrderBy.EditValue = 1
        Else
            Me.INDGleOrderBy.EditValue = 3
        End If
    End Sub

#End Region

#Region "Report"

    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports = True Then
            Try
                AsyncLoader(True)
                If Me.ValoritationCurrencyId Is Nothing Then
                    Dim report = New DevExpress.XtraReports.UI.XtraReport 'Report main
                    Dim listCurrencies = XpoServiceEx.Instance(indigo.TransactionalContainer).PaymentsService.GetPaymentsGroupByCurrencyXpo()
                    Dim ds As DataSet = Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetListReportPaymentsByAgeAsync(_criterias, _filters, indigo)
                    If ds Is Nothing OrElse ds.Tables(0).Rows.Count = 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    End If
                    If Not listCurrencies?.Any Then
                        Dim officialCurrency = XpoServiceEx.Instance(indigo.TransactionalContainer).CommonService.GetCollection(Of CommonCurrencyXpo)(Nothing, $"Id = {indigo.OfficialCurrencyId}").FirstOrDefault()
                        listCurrencies.Add(officialCurrency)
                    End If
                    For Each currency In listCurrencies
                        Dim reporte As New rptReportListOfPaymentsAges
                        reporte.ParametrosReporte = New Object() {_criterias, _filters}
                        reporte._currency = currency
                        reporte._isValorization = False
                        Await reporte.CargarDataSourceAsync(ds)
                        AsyncLoader(False)
                        If reporte.DataSource IsNot Nothing Then
                            reporte.CreateDocument()
                            Me.INDLcBase.Visible = False
                            Me.INDCncNavigation.Visible = False
                            Me.INDPcDocumentViewer.Visible = True
                            report.Pages.AddRange(reporte.Pages)
                        End If
                    Next
                    INDDvDocumentViewer.DocumentSource = report
                    INDDvDocumentViewer.Show()
                Else
                    Dim reporte As New rptReportListOfPaymentsAges
                    Dim currency = XpoServiceEx.Instance(indigo.TransactionalContainer).CommonService.GetCollection(Of CommonCurrencyXpo)(Nothing, $"Id = {ValoritationCurrencyId}").FirstOrDefault()
                    reporte.ParametrosReporte = New Object() {_criterias, _filters}
                    reporte._currency = currency
                    reporte._isValorization = True
                    INDDvDocumentViewer.DocumentSource = reporte
                    Await reporte.CargarDataSourceAsync()
                    AsyncLoader(False)
                    If reporte.DataSource IsNot Nothing Then
                        reporte.CreateDocument(True)
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                        Me.INDDClosingDate.Focus()
                    End If
                    Me.INDLcBase.Visible = False
                    Me.INDCncNavigation.Visible = False
                    Me.INDPcDocumentViewer.Visible = True
                    INDDvDocumentViewer.Show()
                End If
            Catch ex As Exception
                AsyncLoader(False)
            End Try
        End If
    End Sub

    Private Sub INDCnBack_ClickBack() Handles INDCnBack.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncNavigation.Visible = True
        Me.INDPcDocumentViewer.Visible = False
    End Sub

    Private Async Sub INDSbGenerateExcell_Click(sender As Object, e As EventArgs) Handles INDSbGenerateExcell.Click
        If Me.ValidateControlsReports = True Then
            Try
                AsyncLoader(True)
                Dim ds As DataSet = Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetListReportPaymentsByAgeAsync(_criterias, _filters, Me.indigo)
                If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                    Dim dtReportPaymentsByAge As DataTable = ds.Tables("ReportPaymentsByAge")

                    'cargamos el listado de las edades de cartera 
                    Dim filtroConsultaSettingsPayment As String = String.Format("SettingPaymentId.IdOperatingUnit = {0}", _selectorOperatingUnit.GetKey(0))
                    Dim listSettingPayments As List(Of PaymentsAgesPaymentsXpo) = XpoServiceEx.Instance(indigo.TransactionalContainer).PaymentsService.GetCollection(Of PaymentsAgesPaymentsXpo)(Nothing, filtroConsultaSettingsPayment)

                    Await Task.Factory.StartNew(Sub()
                                                    Select Case INDGleTypeReport.EditValue
                                                        Case 1
                                                            chargueDataSourceDetailed(listSettingPayments, dtReportPaymentsByAge)
                                                        Case 2
                                                            chargueDataSourceSummary(listSettingPayments, dtReportPaymentsByAge)
                                                        Case Else
                                                            Exit Sub
                                                    End Select
                                                End Sub)

                    If Me.INDGcExportExcell.DataSource IsNot Nothing Then
                        generateExcel()
                    End If
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

    Private Sub INDSleValoritation_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleValoritation.QueryPopUp
        If Me.INDSleValoritation.Properties.DataSource Is Nothing Then
            Me.INDSleValoritation.Properties.DataSource = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).CommonService.GetCurrency()
        End If
    End Sub

#End Region

#End Region

End Class