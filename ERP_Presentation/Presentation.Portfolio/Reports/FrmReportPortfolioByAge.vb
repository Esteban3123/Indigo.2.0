#Region "Imports"

Imports System.Text
Imports DevExpress.XtraGrid.Views.Grid
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.CommonRepository
Imports Infrastructure.Data.Xpo.PortfolioRepository
Imports Presentation.Base
Imports Presentation.CloudAgent
Imports Presentation.Common.MVP
Imports Presentation.Portfolio.MVP
Imports Presentation.Reporter

#End Region

Public Class FrmReportPortfolioByAge

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

    Private _FillingCalculateAgeBy As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingCalculateAgeBy As List(Of Tuple(Of Integer, String))
        Get
            If _FillingCalculateAgeBy Is Nothing Then
                _FillingCalculateAgeBy = New List(Of Tuple(Of Integer, String))
                _FillingCalculateAgeBy.Add(New Tuple(Of Integer, String)(1, "Fecha Factura"))
                _FillingCalculateAgeBy.Add(New Tuple(Of Integer, String)(2, "Fecha Radicación"))
            End If
            Return _FillingCalculateAgeBy
        End Get
    End Property

    Private _FillingGroupBy As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingGroupBy As List(Of Tuple(Of Integer, String))
        Get
            If _FillingGroupBy Is Nothing Then
                _FillingGroupBy = New List(Of Tuple(Of Integer, String))
                _FillingGroupBy.Add(New Tuple(Of Integer, String)(1, "Tercero"))
                _FillingGroupBy.Add(New Tuple(Of Integer, String)(2, "Tercero - Régimen"))
                _FillingGroupBy.Add(New Tuple(Of Integer, String)(3, "Régimen - Tercero"))
            End If
            Return _FillingGroupBy
        End Get
    End Property

    Private _FillingOrderBy As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingOrderBy As List(Of Tuple(Of Integer, String))
        Get
            If _FillingOrderBy Is Nothing Then
                _FillingOrderBy = New List(Of Tuple(Of Integer, String))
                If INDGleTypeReport.EditValue = 1 Then
                    _FillingOrderBy.Add(New Tuple(Of Integer, String)(1, "Codigo"))
                    _FillingOrderBy.Add(New Tuple(Of Integer, String)(2, "Fecha Factura"))
                    If INDGleGroupBy.EditValue = 1 Then
                        _FillingOrderBy.Add(New Tuple(Of Integer, String)(3, "Tercero"))
                    ElseIf INDGleGroupBy.EditValue = 2 Then
                        _FillingOrderBy.Add(New Tuple(Of Integer, String)(3, "Tercero"))
                    ElseIf INDGleGroupBy.EditValue = 3 Then
                        _FillingOrderBy.Add(New Tuple(Of Integer, String)(4, "Régimen"))
                    End If
                    _FillingOrderBy.Add(New Tuple(Of Integer, String)(5, "Edad"))
                ElseIf INDGleTypeReport.EditValue = 2 Then
                    If INDGleGroupBy.EditValue = 1 Then
                        _FillingOrderBy.Add(New Tuple(Of Integer, String)(3, "Tercero"))
                    ElseIf INDGleGroupBy.EditValue = 2 Then
                        _FillingOrderBy.Add(New Tuple(Of Integer, String)(3, "Tercero"))
                    ElseIf INDGleGroupBy.EditValue = 3 Then
                        _FillingOrderBy.Add(New Tuple(Of Integer, String)(4, "Régimen"))
                    End If
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
        _criterias.Add("CalculateAgeBy", INDGleCalculateAgeBy.EditValue)
        _criterias.Add("IncludeAdvance", INDSleIncludeAdvance.EditValue)
        _criterias.Add("OrderBy", INDGleOrderBy.EditValue)
        _criterias.Add("GroupBy", INDGleGroupBy.EditValue)
        _criterias.Add("GroupOrDetailByAccount", Me.INDSleGroupOrDetail.EditValue)
        _criterias.Add("isValorization", If(Me.INDsleValorization.EditValue IsNot Nothing, 1, 0))
        _criterias.Add("ToCurrency", Me.INDsleValorization.EditValue)

        _filters = New Dictionary(Of String, String)
        _filters.Add("ClosingDate", INDDClosingDate.EditValue)
        _filters.Add("OperatingUnitId", _selectorOperatingUnit.GetKey(0))
        _filters.Add("OperatingUnits", _selectorOperatingUnit.GetKeys())
        _filters.Add("PersonTypes", INDCcbePersonType.EditValue)
        _filters.Add("ThirdParties", _selectorCustomer.GetKeys())
        _filters.Add("DocumentTypes", INDCcbeDocumentType.EditValue)
        _filters.Add("Status", INDCcbeStatus.EditValue)

        Return True
    End Function

#Region "ToExcel"

    Private Sub chargueDataSourceSummary(ByVal listSettingPortfolio As List(Of PortfolioAgesPortfolioXpo), ByVal dtReportPortfolioByAge As DataTable)
        Dim dt As New DataTable
        Dim groupedResult As IEnumerable(Of Object) = Nothing
        If INDGleGroupBy.EditValue = 1 Then
            dt.Columns.Add("Numero Documento")
            dt.Columns.Add("Nombre Tercero")

            groupedResult = From t In dtReportPortfolioByAge.AsEnumerable()
                            Group t By Key = New With {
                            Key .ThirdPartyNit = t.Field(Of String)("ThirdPartyNit"),
                            Key .ThirdPartyName = t.Field(Of String)("ThirdPartyName"),
                            Key .CurrencyName = t.Field(Of String)("CurrencyName")
                            }
                            Into Group
                            Select New With {
                                .ThirdPartyNit = Key.ThirdPartyNit,
                                .ThirdPartyName = Key.ThirdPartyName,
                                .CurrencyName = Key.CurrencyName
                            }
        Else
            If INDGleGroupBy.EditValue = 2 Then
                dt.Columns.Add("Numero Documento")
                dt.Columns.Add("Nombre Tercero")
                dt.Columns.Add(ResourceManager.GetString("EntityTypeName", "Portfolio"))
            Else
                dt.Columns.Add(ResourceManager.GetString("EntityTypeName", "Portfolio"))
                dt.Columns.Add("Numero Documento")
                dt.Columns.Add("Nombre Tercero")
            End If

            groupedResult = From t In dtReportPortfolioByAge.AsEnumerable()
                            Group t By Key = New With {
                            Key .ThirdPartyNit = t.Field(Of String)("ThirdPartyNit"),
                            Key .ThirdPartyName = t.Field(Of String)("ThirdPartyName"),
                            Key .Regimen = t.Field(Of String)("Regimen"),
                            Key .CurrencyName = t.Field(Of String)("CurrencyName")
                            }
                            Into Group
                            Select New With {
                                .ThirdPartyNit = Key.ThirdPartyNit,
                                .ThirdPartyName = Key.ThirdPartyName,
                                .Regimen = Key.Regimen,
                                .CurrencyName = Key.CurrencyName
                            }
        End If

        Dim NameMinimumAgeRange = CType(listSettingPortfolio(0), PortfolioAgesPortfolioXpo).SettingPortfolioId.NameMinimumAgeRange
        Dim RangeMin = listSettingPortfolio.Min(Function(x) x.InitialRange)
        dt.Columns.Add(NameMinimumAgeRange, GetType(Decimal))
        For Each item In listSettingPortfolio
            If Not dt.Columns.Contains(item.Name) Then
                dt.Columns.Add(item.Name, GetType(Decimal))
            End If
        Next
        Dim NameMaximumAgeRange = CType(listSettingPortfolio(0), PortfolioAgesPortfolioXpo).SettingPortfolioId.NameMaximumAgeRange
        Dim MaximunAgeRange = CType(listSettingPortfolio(0), PortfolioAgesPortfolioXpo).SettingPortfolioId.MaximunAgeRange
        NameMaximumAgeRange = If(dt.Columns.Contains(NameMaximumAgeRange), String.Concat(NameMaximumAgeRange, "(1)"), NameMaximumAgeRange)
        dt.Columns.Add(NameMaximumAgeRange, GetType(Decimal))

        dt.Columns.Add("Moneda")
        dt.Columns.Add("Valor Inicial", GetType(Decimal))
        dt.Columns.Add("Retención", GetType(Decimal))
        dt.Columns.Add("Movimiento Inicial", GetType(Decimal))
        dt.Columns.Add("Notas Débito", GetType(Decimal))
        dt.Columns.Add("Notas Crédito", GetType(Decimal))
        dt.Columns.Add("Transferencias", GetType(Decimal))
        dt.Columns.Add("Recibos de Caja", GetType(Decimal))
        dt.Columns.Add("Cruces", GetType(Decimal))
        dt.Columns.Add("Anticipo", GetType(Decimal))
        dt.Columns.Add("Saldo", GetType(Decimal))

        For Each item In groupedResult
            Dim row As DataRow = dt.NewRow()
            Dim filterResult As List(Of DataRow) = Nothing
            If INDGleGroupBy.EditValue = 1 Then
                filterResult = dtReportPortfolioByAge.AsEnumerable().Where(Function(t) t.Field(Of String)("ThirdPartyNit") = item.ThirdPartyNit AndAlso t.Field(Of String)("ThirdPartyName") = item.ThirdPartyName).ToList()
                row.Item("Numero Documento") = item.ThirdPartyNit
                row.Item("Nombre Tercero") = item.ThirdPartyName
            Else
                filterResult = dtReportPortfolioByAge.AsEnumerable().Where(Function(t) t.Field(Of String)("ThirdPartyNit") = item.ThirdPartyNit AndAlso t.Field(Of String)("ThirdPartyName") = item.ThirdPartyName AndAlso t.Field(Of String)("Regimen") = item.Regimen).ToList()
                row.Item("Numero Documento") = item.ThirdPartyNit
                row.Item("Nombre Tercero") = item.ThirdPartyName
                row.Item(ResourceManager.GetString("EntityTypeName", "Portfolio")) = item.Regimen
            End If


            Dim value As Decimal = filterResult.Where(Function(d) d.Field(Of Integer)("AccountReceivableType") <> 0 AndAlso d.Field(Of Integer)("Age") < RangeMin).Sum(Function(d) d.Field(Of Decimal)("Balance"))
            row.Item(NameMinimumAgeRange) = value
            For Each itemSettings In listSettingPortfolio
                value = filterResult.Where(Function(d) d.Field(Of Integer)("AccountReceivableType") <> 0 AndAlso d.Field(Of Integer)("Age") >= itemSettings.InitialRange AndAlso d.Field(Of Integer)("Age") <= itemSettings.EndRange).Sum(Function(d) d.Field(Of Decimal)("Balance"))
                row.Item(itemSettings.Name) = value
            Next
            value = filterResult.Where(Function(d) d.Field(Of Integer)("AccountReceivableType") <> 0 AndAlso d.Field(Of Integer)("Age") > MaximunAgeRange).Sum(Function(d) d.Field(Of Decimal)("Balance"))
            row.Item(NameMaximumAgeRange) = value

            row.Item("Moneda") = item.CurrencyName
            row.Item("Valor Inicial") = filterResult.Where(Function(d) d.Field(Of Integer)("AccountReceivableType") <> 0).Sum(Function(d) d.Field(Of Decimal)("DocumentValue"))
            row.Item("Retención") = filterResult.Where(Function(d) d.Field(Of Integer)("AccountReceivableType") <> 0).Sum(Function(d) d.Field(Of Decimal)("RetentionValue"))
            row.Item("Movimiento Inicial") = filterResult.Where(Function(d) d.Field(Of Integer)("AccountReceivableType") <> 0).Sum(Function(d) d.Field(Of Decimal)("InitialValue"))
            row.Item("Notas Débito") = filterResult.Where(Function(d) d.Field(Of Integer)("AccountReceivableType") <> 0).Sum(Function(d) d.Field(Of Decimal)("DebitValue"))
            row.Item("Notas Crédito") = filterResult.Where(Function(d) d.Field(Of Integer)("AccountReceivableType") <> 0).Sum(Function(d) d.Field(Of Decimal)("CreditValue"))
            row.Item("Transferencias") = filterResult.Where(Function(d) d.Field(Of Integer)("AccountReceivableType") <> 0).Sum(Function(d) d.Field(Of Decimal)("TransferValue"))
            row.Item("Recibos de Caja") = filterResult.Where(Function(d) d.Field(Of Integer)("AccountReceivableType") <> 0).Sum(Function(d) d.Field(Of Decimal)("CashReceiptValue"))
            row.Item("Cruces") = filterResult.Where(Function(d) d.Field(Of Integer)("AccountReceivableType") <> 0).Sum(Function(d) d.Field(Of Decimal)("CrossingValue"))
            row.Item("Anticipo") = filterResult.Where(Function(d) d.Field(Of Integer)("AccountReceivableType") = 0).Sum(Function(d) d.Field(Of Decimal)("Balance"))
            row.Item("Saldo") = filterResult.Sum(Function(d) d.Field(Of Decimal)("CurrentBalance") * If(d.Field(Of Integer)("AccountReceivableType") = 0, -1, 1))

            dt.Rows.Add(row)
        Next

        INDGcExportExcell.DataSource = dt
    End Sub

    Private Sub chargueDataSourceDetailed(ByVal listSettingPortfolio As List(Of PortfolioAgesPortfolioXpo), ByVal dtReportPortfolioByAge As DataTable)
        Dim dt As New DataTable
        dt.Columns.Add("Numero Documento")
        dt.Columns.Add("Nombre Tercero")
        dt.Columns.Add("Numero Factura")
        dt.Columns.Add("Fecha Documento", GetType(DateTime))
        dt.Columns.Add("Plazo")
        dt.Columns.Add("Fecha Vencimiento", GetType(DateTime))
        dt.Columns.Add("Estado Cartera")
        dt.Columns.Add("Centro de Atencion")
        dt.Columns.Add("Codigo Grupo Atencion")
        dt.Columns.Add("Nombre Grupo Atencion")
        dt.Columns.Add("Codigo Contrato")
        dt.Columns.Add("Nombre Contrato")
        dt.Columns.Add("Numero Cuenta Contable")
        dt.Columns.Add("Nombre Cuenta Contable")
        dt.Columns.Add(ResourceManager.GetString("EntityTypeName", "Portfolio"))
        dt.Columns.Add("Categoría")
        dt.Columns.Add("Numero Radicado")
        dt.Columns.Add("Fecha Radicado", GetType(DateTime))
        dt.Columns.Add("Usuario Radicado")
        dt.Columns.Add("Moneda")

        Dim NameMinimumAgeRange = CType(listSettingPortfolio(0), PortfolioAgesPortfolioXpo).SettingPortfolioId.NameMinimumAgeRange
        If NameMinimumAgeRange = String.Empty Then
            ShowMessage(EeventViewerImages.MensajeError) = String.Format("Debe de parametrizar el nombre edad menor")
            Return
        End If
        Dim RangeMin = listSettingPortfolio.Min(Function(x) x.InitialRange)
        dt.Columns.Add(NameMinimumAgeRange, GetType(Decimal))
        For Each item In listSettingPortfolio
            If Not dt.Columns.Contains(item.Name) Then
                dt.Columns.Add(item.Name, GetType(Decimal))
            End If
        Next
        Dim NameMaximumAgeRange = CType(listSettingPortfolio(0), PortfolioAgesPortfolioXpo).SettingPortfolioId.NameMaximumAgeRange
        If NameMaximumAgeRange = String.Empty Then
            ShowMessage(EeventViewerImages.MensajeError) = String.Format("Debe de parametrizar el nombre edad mayor")
            Return
        End If
        Dim MaximunAgeRange = CType(listSettingPortfolio(0), PortfolioAgesPortfolioXpo).SettingPortfolioId.MaximunAgeRange
        NameMaximumAgeRange = If(dt.Columns.Contains(NameMaximumAgeRange), String.Concat(NameMaximumAgeRange, "(1)"), NameMaximumAgeRange)
        dt.Columns.Add(NameMaximumAgeRange, GetType(Decimal))


        dt.Columns.Add("Valor Inicial", GetType(Decimal))
        dt.Columns.Add("Retención", GetType(Decimal))
        dt.Columns.Add("Movimiento Inicial", GetType(Decimal))
        dt.Columns.Add("Notas Débito", GetType(Decimal))
        dt.Columns.Add("Notas Crédito", GetType(Decimal))
        dt.Columns.Add("Transferencias", GetType(Decimal))
        dt.Columns.Add("Recibos de Caja", GetType(Decimal))
        dt.Columns.Add("Cruces", GetType(Decimal))
        dt.Columns.Add("Anticipo", GetType(Decimal))
        dt.Columns.Add("Saldo", GetType(Decimal))
        dt.Columns.Add("Valor Glosado", GetType(Decimal))
        dt.Columns.Add("Valor Aceptado", GetType(Decimal))
        dt.Columns.Add("Estado de la Glosa")
        dt.Columns.Add("Saldo Inicial")

        For Each item In dtReportPortfolioByAge.Rows
            Dim row As DataRow = dt.NewRow()
            row.Item("Numero Documento") = item("ThirdPartyNit")
            row.Item("Nombre Tercero") = item("ThirdPartyName")
            row.Item("Numero Factura") = item("DocumentCode")
            row.Item("Fecha Documento") = CDate(item("AccountReceivableDate")).AsDate
            row.Item("Plazo") = item("Term")
            row.Item("Fecha Vencimiento") = CDate(item("ExpiredDate")).AsDate
            row.Item("Estado Cartera") = item("PortfolioStatusName")
            row.Item("Centro de Atencion") = item("CenterAttention")
            row.Item("Codigo Grupo Atencion") = item("CareGroupCode")
            row.Item("Nombre Grupo Atencion") = item("CareGroupName")
            row.Item("Codigo Contrato") = item("ContractCode")
            row.Item("Nombre Contrato") = item("ContractName")
            row.Item("Numero Cuenta Contable") = item("MainAccountNumber")
            row.Item("Nombre Cuenta Contable") = item("MainAccountName")
            row.Item(ResourceManager.GetString("EntityTypeName", "Portfolio")) = item("Regimen")
            row.Item("Categoría") = item("Category")
            row.Item("Numero Radicado") = item("RadicatedConsecutive")
            row.Item("Fecha Radicado") = If(IsDBNull(item("RadicatedDate")), DBNull.Value, CDate(item("RadicatedDate")).AsDate)
            row.Item("Usuario Radicado") = item("RadicatedUser")
            row.Item("Moneda") = item("CurrencyName")

            row.Item(NameMinimumAgeRange) = If(item("AccountReceivableType") = 0, 0, If(item("Age") < RangeMin, item("Balance"), 0))
            For Each itemSettings In listSettingPortfolio
                row.Item(itemSettings.Name) = If(item("AccountReceivableType") = 0, 0, If(item("Age") >= itemSettings.InitialRange AndAlso item("Age") <= itemSettings.EndRange, item("Balance"), 0))
            Next
            row.Item(NameMaximumAgeRange) = If(item("AccountReceivableType") = 0, 0, If(item("Age") > MaximunAgeRange, item("Balance"), 0))

            row.Item("Valor Inicial") = item("DocumentValue")
            row.Item("Retención") = item("RetentionValue")
            row.Item("Movimiento Inicial") = item("InitialValue")
            row.Item("Notas Débito") = item("DebitValue")
            row.Item("Notas Crédito") = item("CreditValue")
            row.Item("Transferencias") = item("TransferValue")
            row.Item("Recibos de Caja") = item("CashReceiptValue")
            row.Item("Cruces") = item("CrossingValue")
            row.Item("Anticipo") = If(item("AccountReceivableType") = 0, item("Balance"), 0)
            row.Item("Saldo") = item("Balance")
            row.Item("Valor Glosado") = item("ValueGlosado")
            row.Item("Valor Aceptado") = item("ValueAcceptedFirstInstance") + item("ValueAcceptedSecondInstance")
            row.Item("Estado de la Glosa") = item("GlosaStateName")
            row.Item("Saldo Inicial") = If(item("OpeningBalance") = 1, "SI", "")

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

    Private Sub FrmReportPortfolioByAge_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Cargar GridLookUpEdit
        Me.INDGleTypeReport.Properties.DataSource = FillingTypeReport
        Me.INDGleCalculateAgeBy.Properties.DataSource = FillingCalculateAgeBy
        Me.INDGleGroupBy.Properties.DataSource = FillingGroupBy

        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleTypeReport.EditValue = 1
        Me.INDGleCalculateAgeBy.EditValue = 1
        Me.INDSleIncludeAdvance.EditValue = False
        Me.INDGleGroupBy.EditValue = 1
        Me.INDSleGroupOrDetail.EditValue = False

        Task.Factory.StartNew(Sub()
                                  Dim _dataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).CommonService.ListOperatingUnitTreeList()

                                  Me.SafeInvoke(Sub()
                                                    If _dataSource?.ToEntityList(Of CommonOperatingUnitXpo)?.Any() AndAlso _dataSource.Count = 1 Then
                                                        With _dataSource?.ToEntityList(Of CommonOperatingUnitXpo)?.FirstOrDefault
                                                            INDSleOperatingUnit.EditValue = .Id
                                                            INDSleOperatingUnit.Properties.NullText = .UnitCode
                                                            Dim dictionary = New Dictionary(Of String, Object)
                                                            dictionary("UnitCode") = .UnitCode
                                                            _selectorOperatingUnit.ValuesCache(.Id) = dictionary
                                                        End With
                                                    End If
                                                End Sub)
                              End Sub)
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _FillingTypeReport = Nothing
        _FillingCalculateAgeBy = Nothing
        _FillingGroupBy = Nothing
        _FillingOrderBy = Nothing

        _criterias = Nothing
        _filters = Nothing

        _selectorOperatingUnit = Nothing
        _selectorCustomer = Nothing
    End Sub

#End Region

#Region "QueryPopUp"

    Private Sub INDSleOperatingUnit_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleOperatingUnit.QueryPopUp
        If INDSleOperatingUnit.Properties.DataSource Is Nothing Then
            INDSleOperatingUnit.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).CommonService.ListOperatingUnit()
        End If
    End Sub

    Private Sub INDSleThirdParty_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleThirdParty.QueryPopUp
        If INDSleThirdParty.Properties.DataSource Is Nothing Then
            INDSleThirdParty.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).PortfolioService.ListThirdPartyReportPortfolio()
        End If
    End Sub

    Private Sub INDsleValorization_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleValorization.QueryPopUp
        If INDsleValorization.Properties.DataSource Is Nothing Then
            INDsleValorization.Properties.DataSource = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).CommonService.GetCurrency()
        End If
    End Sub

#End Region

#Region "Selector"

    Private _selectorOperatingUnit As SelectorCache = New SelectorCache("Id", "UnitCode")
    Private _selectorCustomer As SelectorCache = New SelectorCache("Id", "Nit")

    Private Sub INDGvSelector_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvOperatingUnit.CustomUnboundColumnData, INDGvThirdParty.CustomUnboundColumnData
        If e.IsGetData Then
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvOperatingUnit" Then
                e.Value = _selectorOperatingUnit.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvThirdParty" Then
                e.Value = _selectorCustomer.ValidateExistsRow(e.Row)
            End If
        End If
    End Sub

    Private Sub INDGvSelector_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles INDGvOperatingUnit.RowCellClick, INDGvThirdParty.RowCellClick
        If e.Column.FieldName.Contains("UnboundSelection") Then
            Dim selector As SelectorCache = Nothing
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvOperatingUnit" Then
                selector = _selectorOperatingUnit
            ElseIf view.Name = "INDGvThirdParty" Then
                selector = _selectorCustomer
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

    Private Sub INDSleSelector_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDSleOperatingUnit.Closed, INDSleThirdParty.Closed
        Dim searchLookupEdit = TryCast(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        searchLookupEdit.EditValue = Nothing
        If searchLookupEdit.Name = "INDSleOperatingUnit" Then
            searchLookupEdit.Properties.NullText = _selectorOperatingUnit.ToString()
        ElseIf searchLookupEdit.Name = "INDSleThirdParty" Then
            searchLookupEdit.Properties.NullText = _selectorCustomer.ToString()
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
        If INDGleTypeReport.EditValue = 1 Then
            Me.INDGleOrderBy.EditValue = 2
        ElseIf INDGleTypeReport.EditValue = 2 Then
            If INDGleGroupBy.EditValue = 1 Then
                Me.INDGleOrderBy.EditValue = 3
            Else
                Me.INDGleOrderBy.EditValue = 4
            End If
        End If
    End Sub

    Private Sub INDGleGroupBy_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleGroupBy.EditValueChanged
        If String.IsNullOrEmpty(INDGleGroupBy.EditValue) Then
            Exit Sub
        End If

        _FillingOrderBy = Nothing
        Me.INDGleOrderBy.Properties.DataSource = FillingOrderBy
        If INDGleTypeReport.EditValue = 1 Then
            Me.INDGleOrderBy.EditValue = 2
        ElseIf INDGleTypeReport.EditValue = 2 Then
            If INDGleGroupBy.EditValue = 1 Then
                Me.INDGleOrderBy.EditValue = 3
            ElseIf INDGleGroupBy.EditValue = 2 Then
                Me.INDGleOrderBy.EditValue = 3
            Else
                Me.INDGleOrderBy.EditValue = 4
            End If
        End If
    End Sub

#End Region

#Region "Report"

    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports = True Then
            Try
                AsyncLoader(True)

                ''se obtienen todas las  monedas registradas para evaluar su separacion de paginas por este filtro
                Dim aCurrency As List(Of Currency) = New List(Of Currency)
                Dim currencyData = XpoServiceEx.Instance(indigo.TransactionalContainer).PortfolioService.PortfolioAccountReceivableGroupByCurrencyXpo()
                For Each id In currencyData
                    Using Model As New MCurrency("")
                        If id IsNot Nothing Then
                            Dim currency As Currency = Await Model.GetCurrencyById(Convert.ToInt32(id))
                            aCurrency.Add(currency)
                        End If
                    End Using
                Next

                Dim report = New DevExpress.XtraReports.UI.XtraReport

                ''en caso de que sea valorización solo se imprime con la moneda seleccionada
                If INDsleValorization.EditValue IsNot Nothing Then
                    ''Se hace consulta para determinar si la moneda tiene trm con la fecha de corte del reporte
                    For Each currency In aCurrency
                        Using Model As New MPortfolioTransfers("")
                            Dim Result = Await Model.GetTRMbyCurrencyId(Convert.ToInt32(currency.Id), INDsleValorization.EditValue, INDDClosingDate.EditValue)
                            If Result.ObjectEmbbeded Is Nothing Then
                                Mensaje(EeventViewerImages.Advertencia) = "Exiten monedas que no tienen TRM registrado para el cambio a la valorización"
                                AsyncLoader(False)
                                Exit Sub
                            End If
                        End Using
                    Next
                    Dim ds As DataSet = Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetListReportPortfolioByAgeAsync(_criterias, _filters, indigo)
                    Dim reporte As New rptReportPortfolioByAge
                    Using model As New Presentation.Common.MVP.MCurrency(Tag)
                        reporte.Currency = Await model.GetCurrencyById(INDsleValorization.EditValue)
                    End Using
                    reporte.ParametrosReporte = New Object() {_criterias, _filters}
                    reporte.IsValorization = True
                    Dim dtReportPortfolioByAge = ds.Tables("ReportPortfolioByAge")
                    ''se valida nuevamente en caso de que devuelva un valor en null por no tener trm
                    Dim x = (From item In dtReportPortfolioByAge.AsEnumerable Where item("Balance") Is DBNull.Value Select item)
                    If x.Count > 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = "Exiten monedas que no tienen TRM registrado para el cambio a la valorización"
                    End If
                    Await reporte.CargarDataSourceAsync(ds)
                    AsyncLoader(False)

                    If reporte.DataSource IsNot Nothing Then
                        reporte.CreateDocument()
                        Me.INDLcBase.Visible = False
                        Me.INDCncNavigation.Visible = False
                        Me.INDPcDocumentViewer.Visible = True

                        report.Pages.AddRange(reporte.Pages)
                    End If
                Else
                    ''en caso de que no seleccione valorizacion se imprimen paginas por cada moneda
                    Dim ds As DataSet = Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetListReportPortfolioByAgeAsync(_criterias, _filters, indigo)
                    For Each item In aCurrency
                        Dim reporte As New rptReportPortfolioByAge
                        reporte.ParametrosReporte = New Object() {_criterias, _filters}
                        reporte.Currency = item
                        reporte.IsValorization = False
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
                End If
                INDDvDocumentViewer.DocumentSource = report
                If report.Pages.Count = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDDClosingDate.Focus()
                End If
                INDDvDocumentViewer.Show()
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

                '' Validaciones para saber si se tienen el trm disponible (antes de hacer la consulta global) -- con valorizacion
                If INDsleValorization.EditValue IsNot Nothing Then
                    Dim aCurrency As List(Of Currency) = New List(Of Currency)
                    Dim currencyData = XpoServiceEx.Instance(indigo.TransactionalContainer).PortfolioService.PortfolioAccountReceivableGroupByCurrencyXpo()
                    For Each id In currencyData
                        Using Model As New MCurrency("")
                            If id IsNot Nothing Then
                                Dim currency As Currency = Await Model.GetCurrencyById(Convert.ToInt32(id))
                                aCurrency.Add(currency)
                            End If
                        End Using
                    Next
                    For Each currency In aCurrency
                        Using Model As New MPortfolioTransfers("")
                            Dim Result = Await Model.GetTRMbyCurrencyId(Convert.ToInt32(currency.Id), INDsleValorization.EditValue, INDDClosingDate.EditValue)
                            If Result.ObjectEmbbeded Is Nothing Then
                                Mensaje(EeventViewerImages.Advertencia) = "Exiten monedas que no tienen TRM registrado para el cambio a la valorización "
                                AsyncLoader(False)
                                Exit Sub
                            End If
                        End Using
                    Next
                End If

                Dim ds As DataSet = Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetListReportPortfolioByAgeAsync(_criterias, _filters, Me.indigo)
                If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                    Dim dtReportPortfolioByAge As DataTable = ds.Tables("ReportPortfolioByAge")
                    'cargamos el listado de las edades de cartera 
                    Dim filtroConsultaSettingsPayment As String = String.Format("SettingPortfolioId.OperatingUnitId =  {0}", _selectorOperatingUnit.GetKey(0))
                    Dim listSettingPortfolio As List(Of PortfolioAgesPortfolioXpo) = XpoServiceEx.Instance(indigo.TransactionalContainer).PortfolioService.GetCollection(Of PortfolioAgesPortfolioXpo)(Nothing, filtroConsultaSettingsPayment)

                    Await Task.Factory.StartNew(Sub()
                                                    Select Case INDGleTypeReport.EditValue
                                                        Case 1
                                                            chargueDataSourceDetailed(listSettingPortfolio, dtReportPortfolioByAge)
                                                        Case 2
                                                            chargueDataSourceSummary(listSettingPortfolio, dtReportPortfolioByAge)
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



#End Region

#End Region

End Class