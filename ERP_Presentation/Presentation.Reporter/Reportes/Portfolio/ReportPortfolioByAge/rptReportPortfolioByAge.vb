#Region "Imports"

Imports DevExpress.XtraPrinting
Imports DevExpress.XtraReports.UI
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PortfolioRepository
Imports Presentation.Base
Imports Presentation.CloudAgent
Imports System.Globalization
Imports Presentation.Portfolio.MVP
Imports Infrastructure.CrossCutting.Resources

#End Region

Public Class rptReportPortfolioByAge
    Implements IReport

#Region "Properties"

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Dim criterias As Dictionary(Of String, String)

    Dim filters As Dictionary(Of String, String)

    Private listSettingPortfolio As List(Of PortfolioAgesPortfolioXpo)

    Dim dtReportPortfolioByAge As DataTable

    Public Property Currency As Currency

    Public Property IsValorization As Boolean

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

#End Region

#Region "Load Data"

    Public Sub CargarDataSource() Implements IReport.CargarDataSource

    End Sub

    Public Async Function CargarDataSourceAsync(ds As DataSet) As Task
        Try
            criterias = ParametrosReporte(0)
            filters = ParametrosReporte(1)
            If ds Is Nothing Then
                ds = Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetListReportPortfolioByAgeAsync(criterias, filters, Me.IndigoSessionValues)
            End If

            If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                dtReportPortfolioByAge = ds.Tables("ReportPortfolioByAge")
                If Not IsValorization Then
                    Dim x = (From item In dtReportPortfolioByAge.AsEnumerable Where item("CurrencyId") IsNot DBNull.Value AndAlso item("CurrencyId") = Currency.Id Select item)
                    If x.Count = 0 Then
                        Me.DataSource = Nothing
                        Exit Function
                    End If
                    dtReportPortfolioByAge = x.CopyToDataTable
                End If
                dtReportPortfolioByAge.TableName = "ReportPortfolioByAge"
                Me.DataSource = dtReportPortfolioByAge
                Me.DataMember = "ReportPortfolioByAge"
                LoadAgesPortfolio(CInt(Me.filters("OperatingUnitId")))
                GenerateCalculatedFields()
            Else
                Me.DataSource = Nothing
            End If
        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
            Me.DataSource = Nothing
        End Try
    End Function

#End Region

#Region "Methods"

    Public Sub LoadAgesPortfolio(ByVal OperatingUnitId As String)
        Dim filtroConsultaSettingsPayment As String = "SettingPortfolioId.OperatingUnitId = " & OperatingUnitId
        listSettingPortfolio = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PaymentsService.GetCollection(Of PortfolioAgesPortfolioXpo)(Nothing, filtroConsultaSettingsPayment)
    End Sub

    Public Sub GenerateCalculatedFields()
        Dim RangeMin = listSettingPortfolio.Min(Function(x) x.InitialRange)
        Dim MaximunAgeRange = CType(listSettingPortfolio(0), PortfolioAgesPortfolioXpo).SettingPortfolioId.MaximunAgeRange

        'creamos el campo calculado para asignar el valor a la celda de las facturas no vencidas
        Dim INDCfJunior As New CalculatedField()
        Me.CalculatedFields.Add(INDCfJunior)
        INDCfJunior.DataSource = Me.DataSource
        INDCfJunior.DataMember = Me.DataMember
        INDCfJunior.Expression = "Iif([AccountReceivableType] = 0, 0, Iif([Age] < " & RangeMin & ", [Balance], 0))"
        INDCfJunior.Name = "INDCFLowerValue"

        'creamos el campo calculado para asignar el valor a la celda de las facturas que tienen el rango maximo de vencimiento
        Dim INDCfMax As New CalculatedField()
        Me.CalculatedFields.Add(INDCfMax)
        INDCfMax.DataSource = Me.DataSource
        INDCfMax.DataMember = Me.DataMember
        INDCfMax.Expression = "Iif([AccountReceivableType] = 0, 0, Iif([Age] > " & MaximunAgeRange & ", [Balance], 0))"
        INDCfMax.Name = "INDCFHigherValue"

        'creamos los campos calculados para asignar el valor a las celdas que estan en la tabla AgesPayment
        Dim index = 1
        For Each item In listSettingPortfolio
            index += 1
            Dim INDCfDetail As New CalculatedField()
            Me.CalculatedFields.Add(INDCfDetail)
            INDCfDetail.DataSource = Me.DataSource
            INDCfDetail.DataMember = Me.DataMember
            INDCfDetail.Expression = "Iif([AccountReceivableType] = 0, 0, Iif([Age] >=" & item.InitialRange & " And [Age] <= " & item.EndRange & ", [CurrentBalance], 0))"
            INDCfDetail.Name = "INDCFDetail" & index
        Next
    End Sub

    Public Sub GenerateMinimumAgeRangeCells()
        If Me.INDPrmTypeReport.Value = 1 Then
            'imprimir la celda de facturas sin vencer pageheader
            Dim cell5 As New XRTableCell()
            cell5.Text = CType(listSettingPortfolio(0), PortfolioAgesPortfolioXpo).SettingPortfolioId.NameMinimumAgeRange
            If Me.INDPrmGroupBy.Value = 1 Then
                XrTableRow1.Cells.Add(cell5)
            Else
                XrTableRow9.Cells.Add(cell5)
            End If

            'imprimir la celda de facturas sin vencer detalle
            Dim cell101 As New XRTableCell()
            cell101.DataBindings.Add("Text", Nothing, "INDCFLowerValue", "{0:c" + IndigoSessionValues.CurrencyNumbertFormat.CurrencyDecimalDigits.ToString() + "}")
            XrTableRow2.Cells.Add(cell101)

            'crear el campo suma por grupos
            Dim summaryMin As XRSummary = New XRSummary()
            summaryMin.FormatString = "{0:c" + IndigoSessionValues.CurrencyNumbertFormat.CurrencyDecimalDigits.ToString() + "}"
            summaryMin.IgnoreNullValues = True
            summaryMin.Running = DevExpress.XtraReports.UI.SummaryRunning.Group

            'imprimir la celda de facturas sin vencer en el footer de grupo por avances o cuentas por pagar
            Dim cell110 As New XRTableCell()
            cell110.DataBindings.Add("Text", Nothing, "INDCFLowerValue")
            cell110.Summary = summaryMin
            XrTableRow7.Cells.Add(cell110)

            If Me.INDPrmGroupBy.Value <> 1 Then
                'crear el campo suma por grupos
                Dim summarySecundaryMin As XRSummary = New XRSummary()
                summarySecundaryMin.FormatString = "{0:c" + IndigoSessionValues.CurrencyNumbertFormat.CurrencyDecimalDigits.ToString() + "}"
                summarySecundaryMin.IgnoreNullValues = True
                summarySecundaryMin.Running = DevExpress.XtraReports.UI.SummaryRunning.Group

                'imprimir la celda de facturas sin vencer en el footer de grupo por avances o cuentas por pagar
                Dim cell111 As New XRTableCell()
                cell111.DataBindings.Add("Text", Nothing, "INDCFLowerValue")
                cell111.Summary = summarySecundaryMin
                XrTableRow11.Cells.Add(cell111)
            End If

            'crear el campo suma por reporte
            Dim summaryMinReport As XRSummary = New XRSummary()
            summaryMinReport.FormatString = "{0:c" + IndigoSessionValues.CurrencyNumbertFormat.CurrencyDecimalDigits.ToString() + "}"
            summaryMinReport.IgnoreNullValues = True
            summaryMinReport.Running = DevExpress.XtraReports.UI.SummaryRunning.Report

            'imprimir la celda de facturas sin vencer en el footer del reporte
            Dim cell113 As New XRTableCell()
            XrTableRow12.Cells.Add(cell113)
            cell113.DataBindings.Add("Text", Nothing, "INDCFLowerValue")
            cell113.Summary = summaryMinReport
        Else
            'imprimir la celda de facturas sin vencer pageheader
            Dim cell5 As New XRTableCell()
            cell5.Text = CType(listSettingPortfolio(0), PortfolioAgesPortfolioXpo).SettingPortfolioId.NameMinimumAgeRange
            If Me.INDPrmGroupBy.Value = 1 Then
                XrTableRow4.Cells.Add(cell5)
            Else
                XrTableRow10.Cells.Add(cell5)
            End If

            'crear el campo suma por grupos
            Dim summaryMin As XRSummary = New XRSummary()
            summaryMin.FormatString = "{0:c" + IndigoSessionValues.CurrencyNumbertFormat.CurrencyDecimalDigits.ToString() + "}"
            summaryMin.IgnoreNullValues = True
            summaryMin.Running = DevExpress.XtraReports.UI.SummaryRunning.Group

            'imprimir la celda de facturas sin vencer en el footer de grupo por avances o cuentas por pagar
            Dim cell110 As New XRTableCell()
            cell110.DataBindings.Add("Text", Nothing, "INDCFLowerValue")
            cell110.Summary = summaryMin
            XrTableRow5.Cells.Add(cell110)

            'crear el campo suma por agrupacion primaria
            Dim summaryDetailPrimary As XRSummary = New XRSummary()
            summaryDetailPrimary.FormatString = "{0:c" + IndigoSessionValues.CurrencyNumbertFormat.CurrencyDecimalDigits.ToString() + "}"
            summaryDetailPrimary.IgnoreNullValues = True
            summaryDetailPrimary.Running = DevExpress.XtraReports.UI.SummaryRunning.Group

            'imprimir las celdas de los registros que estan en la tabla portfolioages en el footer de grupo por tercero
            Dim cellFooterPrimary As New XRTableCell()
            cellFooterPrimary.DataBindings.Add("Text", Nothing, "INDCFLowerValue")
            cellFooterPrimary.Summary = summaryDetailPrimary
            XrTableRow15.Cells.Add(cellFooterPrimary)

            'crear el campo suma por reporte
            Dim summaryMinReport As XRSummary = New XRSummary()
            summaryMinReport.FormatString = "{0:c" + IndigoSessionValues.CurrencyNumbertFormat.CurrencyDecimalDigits.ToString() + "}"
            summaryMinReport.IgnoreNullValues = True
            summaryMinReport.Running = DevExpress.XtraReports.UI.SummaryRunning.Report

            'imprimir la celda de facturas sin vencer en el footer del reporte
            Dim cell113 As New XRTableCell()
            cell113.DataBindings.Add("Text", Nothing, "INDCFLowerValue")
            cell113.Summary = summaryMinReport
            XrTableRow6.Cells.Add(cell113)
        End If
    End Sub

    Public Sub GenerateAgeRangesCells()
        Dim index = 1
        For Each item In listSettingPortfolio
            index += 1

            If Me.INDPrmTypeReport.Value = 1 Then
                'imprimir las celdas de los registros que estan en la tabla portfolioages en el pageheader
                Dim cell As New XRTableCell()
                cell.Text = item.Name
                cell.Name = "INDCllPageHeader" & index
                If Me.INDPrmGroupBy.Value = 1 Then
                    XrTableRow1.Cells.Add(cell)
                Else
                    XrTableRow9.Cells.Add(cell)
                End If

                'imprimir las celdas de los registros que estan en la tabla portfolioages en el detalle
                Dim cellDetail As New XRTableCell()
                cellDetail.DataBindings.Add("Text", Nothing, "INDCFDetail" & index, "{0:c" + IndigoSessionValues.CurrencyNumbertFormat.CurrencyDecimalDigits.ToString() + "}")
                cellDetail.Name = "INDCllDetail" & index
                XrTableRow2.Cells.Add(cellDetail)

                'crear el campo suma por grupos por tercero
                Dim summaryDetailThird As XRSummary = New XRSummary()
                summaryDetailThird.FormatString = "{0:c" + IndigoSessionValues.CurrencyNumbertFormat.CurrencyDecimalDigits.ToString() + "}"
                summaryDetailThird.IgnoreNullValues = True
                summaryDetailThird.Running = DevExpress.XtraReports.UI.SummaryRunning.Group

                'imprimir las celdas de los registros que estan en la tabla portfolioages en el footer de grupo por tercero
                Dim cellFooterThird As New XRTableCell()
                cellFooterThird.DataBindings.Add("Text", Nothing, "INDCFDetail" & index)
                cellFooterThird.Summary = summaryDetailThird
                XrTableRow7.Cells.Add(cellFooterThird)

                If Me.INDPrmGroupBy.Value <> 1 Then
                    'crear el campo suma por grupos
                    Dim summarySecundaryMin As XRSummary = New XRSummary()
                    summarySecundaryMin.FormatString = "{0:c" + IndigoSessionValues.CurrencyNumbertFormat.CurrencyDecimalDigits.ToString() + "}"
                    summarySecundaryMin.IgnoreNullValues = True
                    summarySecundaryMin.Running = DevExpress.XtraReports.UI.SummaryRunning.Group

                    'imprimir la celda de facturas sin vencer en el footer de grupo por avances o cuentas por pagar
                    Dim cell111 As New XRTableCell()
                    cell111.DataBindings.Add("Text", Nothing, "INDCFDetail" & index)
                    cell111.Summary = summarySecundaryMin
                    XrTableRow11.Cells.Add(cell111)
                End If

                'crear el campo suma por reporte
                Dim summaryDetailReport As XRSummary = New XRSummary()
                summaryDetailReport.FormatString = "{0:c" + IndigoSessionValues.CurrencyNumbertFormat.CurrencyDecimalDigits.ToString() + "}"
                summaryDetailReport.IgnoreNullValues = True
                summaryDetailReport.Running = DevExpress.XtraReports.UI.SummaryRunning.Report

                'imprimir las celdas de los registros que estan en la tabla portfolioages en el footer del reporte
                Dim cellReportValue As New XRTableCell()
                cellReportValue.DataBindings.Add("Text", Nothing, "INDCFDetail" & index)
                cellReportValue.Summary = summaryDetailReport
                XrTableRow12.Cells.Add(cellReportValue)
            Else
                'imprimir las celdas de los registros que estan en la tabla portfolioages en el pageheader
                Dim cell As New XRTableCell()
                cell.Text = item.Name
                cell.Name = "INDCllPageHeader" & index
                If Me.INDPrmGroupBy.Value = 1 Then
                    XrTableRow4.Cells.Add(cell)
                Else
                    XrTableRow10.Cells.Add(cell)
                End If

                'crear el campo suma por grupos por tercero
                Dim summaryDetailThird As XRSummary = New XRSummary()
                summaryDetailThird.FormatString = "{0:c" + IndigoSessionValues.CurrencyNumbertFormat.CurrencyDecimalDigits.ToString() + "}"
                summaryDetailThird.IgnoreNullValues = True
                summaryDetailThird.Running = DevExpress.XtraReports.UI.SummaryRunning.Group

                'imprimir las celdas de los registros que estan en la tabla portfolioages en el footer de grupo por tercero
                Dim cellFooterThird As New XRTableCell()
                cellFooterThird.DataBindings.Add("Text", Nothing, "INDCFDetail" & index)
                cellFooterThird.Summary = summaryDetailThird
                XrTableRow5.Cells.Add(cellFooterThird)

                'crear el campo suma por agrupacion primaria
                Dim summaryDetailPrimary As XRSummary = New XRSummary()
                summaryDetailPrimary.FormatString = "{0:c" + IndigoSessionValues.CurrencyNumbertFormat.CurrencyDecimalDigits.ToString() + "}"
                summaryDetailPrimary.IgnoreNullValues = True
                summaryDetailPrimary.Running = DevExpress.XtraReports.UI.SummaryRunning.Group

                'imprimir las celdas de los registros que estan en la tabla portfolioages en el footer de grupo por tercero
                Dim cellFooterPrimary As New XRTableCell()
                cellFooterPrimary.DataBindings.Add("Text", Nothing, "INDCFDetail" & index)
                cellFooterPrimary.Summary = summaryDetailPrimary
                XrTableRow15.Cells.Add(cellFooterPrimary)

                'crear el campo suma por reporte
                Dim summaryDetailReport As XRSummary = New XRSummary()
                summaryDetailReport.FormatString = "{0:c" + IndigoSessionValues.CurrencyNumbertFormat.CurrencyDecimalDigits.ToString() + "}"
                summaryDetailReport.IgnoreNullValues = True
                summaryDetailReport.Running = DevExpress.XtraReports.UI.SummaryRunning.Report

                'imprimir las celdas de los registros que estan en la tabla portfolioages en el footer del reporte
                Dim cellReportValue As New XRTableCell()
                cellReportValue.DataBindings.Add("Text", Nothing, "INDCFDetail" & index)
                cellReportValue.Summary = summaryDetailReport
                XrTableRow6.Cells.Add(cellReportValue)
            End If
        Next
    End Sub

    Public Sub GenerateMaximumAgeRangeCells()
        If Me.INDPrmTypeReport.Value = 1 Then
            'imprimir las celda rango maximo en el pageheader
            Dim cell100 As New XRTableCell()
            cell100.Text = CType(listSettingPortfolio(0), PortfolioAgesPortfolioXpo).SettingPortfolioId.NameMaximumAgeRange
            If Me.INDPrmGroupBy.Value = 1 Then
                XrTableRow1.Cells.Add(cell100)
            Else
                XrTableRow9.Cells.Add(cell100)
            End If

            'imprimir las celda rango maximo en el detalle
            Dim cell102 As New XRTableCell()
            cell102.DataBindings.Add("Text", Nothing, "INDCFHigherValue", "{0:c" + IndigoSessionValues.CurrencyNumbertFormat.CurrencyDecimalDigits.ToString() + "}")
            XrTableRow2.Cells.Add(cell102)

            'crear el campo suma por grupos
            Dim summaryMax As XRSummary = New XRSummary()
            summaryMax.FormatString = "{0:c" + IndigoSessionValues.CurrencyNumbertFormat.CurrencyDecimalDigits.ToString() + "}"
            summaryMax.IgnoreNullValues = True
            summaryMax.Running = DevExpress.XtraReports.UI.SummaryRunning.Group

            'imprimir las celda rango maximo en el footer del  grupo por tercero
            Dim cell104 As New XRTableCell()
            XrTableRow7.Cells.Add(cell104)
            cell104.DataBindings.Add("Text", Nothing, "INDCFHigherValue")
            cell104.Summary = summaryMax

            If Me.INDPrmGroupBy.Value <> 1 Then
                'crear el campo suma por grupos
                Dim summarySecundaryMin As XRSummary = New XRSummary()
                summarySecundaryMin.FormatString = "{0:c" + IndigoSessionValues.CurrencyNumbertFormat.CurrencyDecimalDigits.ToString() + "}"
                summarySecundaryMin.IgnoreNullValues = True
                summarySecundaryMin.Running = DevExpress.XtraReports.UI.SummaryRunning.Group

                'imprimir la celda de facturas sin vencer en el footer de grupo por avances o cuentas por pagar
                Dim cell111 As New XRTableCell()
                cell111.DataBindings.Add("Text", Nothing, "INDCFHigherValue")
                cell111.Summary = summarySecundaryMin
                XrTableRow11.Cells.Add(cell111)
            End If

            'crear el campo suma por reporte
            Dim summaryMaxReport As XRSummary = New XRSummary()
            summaryMaxReport.FormatString = "{0:c" + IndigoSessionValues.CurrencyNumbertFormat.CurrencyDecimalDigits.ToString() + "}"
            summaryMaxReport.IgnoreNullValues = True
            summaryMaxReport.Running = DevExpress.XtraReports.UI.SummaryRunning.Report

            'imprimir las celda rango maximo en el footer del reporte
            Dim cell15 As New XRTableCell()
            XrTableRow12.Cells.Add(cell15)
            cell15.DataBindings.Add("Text", Nothing, "INDCFHigherValue")
            cell15.Summary = summaryMaxReport
        Else
            'imprimir las celda rango maximo en el pageheader
            Dim cell100 As New XRTableCell()
            cell100.Text = CType(listSettingPortfolio(0), PortfolioAgesPortfolioXpo).SettingPortfolioId.NameMaximumAgeRange
            If Me.INDPrmGroupBy.Value = 1 Then
                XrTableRow4.Cells.Add(cell100)
            Else
                XrTableRow10.Cells.Add(cell100)
            End If

            'crear el campo suma por grupos
            Dim summaryMax As XRSummary = New XRSummary()
            summaryMax.FormatString = "{0:c" + IndigoSessionValues.CurrencyNumbertFormat.CurrencyDecimalDigits.ToString() + "}"
            summaryMax.IgnoreNullValues = True
            summaryMax.Running = DevExpress.XtraReports.UI.SummaryRunning.Group

            'imprimir las celda rango maximo en el footer del  grupo por tercero
            Dim cell104 As New XRTableCell()
            cell104.DataBindings.Add("Text", Nothing, "INDCFHigherValue")
            cell104.Summary = summaryMax
            XrTableRow5.Cells.Add(cell104)

            'crear el campo suma por agrupacion primaria
            Dim summaryDetailPrimary As XRSummary = New XRSummary()
            summaryDetailPrimary.FormatString = "{0:c" + IndigoSessionValues.CurrencyNumbertFormat.CurrencyDecimalDigits.ToString() + "}"
            summaryDetailPrimary.IgnoreNullValues = True
            summaryDetailPrimary.Running = DevExpress.XtraReports.UI.SummaryRunning.Group

            'imprimir las celdas de los registros que estan en la tabla portfolioages en el footer de grupo por tercero
            Dim cellFooterPrimary As New XRTableCell()
            cellFooterPrimary.DataBindings.Add("Text", Nothing, "INDCFHigherValue")
            cellFooterPrimary.Summary = summaryDetailPrimary
            XrTableRow15.Cells.Add(cellFooterPrimary)

            'crear el campo suma por reporte
            Dim summaryMaxReport As XRSummary = New XRSummary()
            summaryMaxReport.FormatString = "{0:c" + IndigoSessionValues.CurrencyNumbertFormat.CurrencyDecimalDigits.ToString() + "}"
            summaryMaxReport.IgnoreNullValues = True
            summaryMaxReport.Running = DevExpress.XtraReports.UI.SummaryRunning.Report

            'imprimir las celda rango maximo en el footer del reporte
            Dim cell15 As New XRTableCell()
            cell15.DataBindings.Add("Text", Nothing, "INDCFHigherValue")
            cell15.Summary = summaryMaxReport
            XrTableRow6.Cells.Add(cell15)
        End If
    End Sub

    Public Sub GenerateTotalRowCells()
        If Me.INDPrmTypeReport.Value <> 1 Then
            'imprimir las celda total en el pageheader
            Dim cell100 As New XRTableCell()
            cell100.Text = "Total"
            If Me.INDPrmGroupBy.Value = 1 Then
                XrTableRow4.Cells.Add(cell100)
            Else
                XrTableRow10.Cells.Add(cell100)
            End If

            'crear el campo suma por grupos
            Dim summaryMax As XRSummary = New XRSummary()
            summaryMax.FormatString = "{0:c" + IndigoSessionValues.CurrencyNumbertFormat.CurrencyDecimalDigits.ToString() + "}"
            summaryMax.IgnoreNullValues = True
            summaryMax.Running = DevExpress.XtraReports.UI.SummaryRunning.Group

            'imprimir las celda total en el footer del  grupo por tercero
            Dim cell104 As New XRTableCell()
            cell104.DataBindings.Add("Text", Nothing, "INDCfTotal")
            cell104.Summary = summaryMax
            XrTableRow5.Cells.Add(cell104)

            'crear el campo suma por agrupacion primaria
            Dim summaryDetailPrimary As XRSummary = New XRSummary()
            summaryDetailPrimary.FormatString = "{0:c" + IndigoSessionValues.CurrencyNumbertFormat.CurrencyDecimalDigits.ToString() + "}"
            summaryDetailPrimary.IgnoreNullValues = True
            summaryDetailPrimary.Running = DevExpress.XtraReports.UI.SummaryRunning.Group

            'imprimir las celdas de los registros que estan en la tabla portfolioages en el footer de grupo por tercero
            Dim cellFooterPrimary As New XRTableCell()
            cellFooterPrimary.DataBindings.Add("Text", Nothing, "INDCfTotal")
            cellFooterPrimary.Summary = summaryDetailPrimary
            XrTableRow15.Cells.Add(cellFooterPrimary)

            'crear el campo suma por reporte
            Dim summaryMaxReport As XRSummary = New XRSummary()
            summaryMaxReport.FormatString = "{0:c" + IndigoSessionValues.CurrencyNumbertFormat.CurrencyDecimalDigits.ToString() + "}"
            summaryMaxReport.IgnoreNullValues = True
            summaryMaxReport.Running = DevExpress.XtraReports.UI.SummaryRunning.Report

            'imprimir las celda total en el footer del reporte
            Dim cell15 As New XRTableCell()
            cell15.DataBindings.Add("Text", Nothing, "INDCfTotal")
            cell15.Summary = summaryMaxReport
            XrTableRow6.Cells.Add(cell15)
        End If
    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public Function GetExceptionDetails(exception As Exception) As String
        Dim properties = exception.[GetType]().GetProperties()
        Dim fields = properties.[Select](Function([property]) New With {
            Key .Name = [property].Name,
            Key .Value = [property].GetValue(exception, Nothing)
        }).[Select](Function(x) [String].Format("{0} : {1}", x.Name, If(x.Value IsNot Nothing, x.Value.ToString(), [String].Empty)))
        Return [String].Join(vbLf, fields)
    End Function

#End Region

#Region "Events"

    Private Sub rptReportPortfolioByAge_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        Me.INDPrmTypeReport.Value = criterias("TypeReport")
        Me.INDPrmGroupBy.Value = criterias("GroupBy")

        ''Seccion de moneda
        If IsValorization Then
            CurrencyLabel.Text = "Valorizado a Moneda:" + Currency.Name
        Else
            CurrencyLabel.Text = "Moneda:" + Currency.Name
        End If
        If Currency IsNot Nothing AndAlso Not String.IsNullOrEmpty(Currency.Abbreviation) Then
            Dim _culture As CultureInfo = CultureInfo.CurrentCulture.Clone()
            Dim CurrencyAbbreviation As String = Currency.Abbreviation
            _culture.NumberFormat = CurrencyAbbreviation.GetNumberFormat
            ApplyLocalization(_culture)
        End If

        If Me.INDPrmTypeReport.Value = 1 Then
            If Me.INDPrmGroupBy.Value = 1 Then
                'Header
                Me.GroupHeader1.Visible = True
                Me.XrTable1.Visible = True
                'Detail
                Me.Detail.Visible = True
                Me.XrTable2.Visible = True
                'GroupFooter
                Me.GroupFooter2.Visible = True
                Me.XrTable7.Visible = True
                'Footer
                Me.XrTable12.Visible = True
            Else
                'Header
                Me.GroupHeader3.Visible = True
                Me.XrTable13.Visible = True
                Me.GroupHeader2.Visible = True
                Me.XrTable9.Visible = True
                'Detail
                Me.Detail.Visible = True
                Me.XrTable2.Visible = True
                'GroupFooter
                Me.GroupFooter1.Visible = True
                Me.XrTable11.Visible = True
                Me.GroupFooter2.Visible = True
                Me.XrTable7.Visible = True
                'Footer
                Me.XrTable12.Visible = True
            End If
        Else
            If Me.INDPrmGroupBy.Value = 1 Then
                Me.XrTable4.Visible = True 'PageHeader
                'Detail
                Me.GroupFooter1.Visible = True
                Me.XrTable5.Visible = True
                'Footer
                Me.XrTable6.Visible = True
            Else
                'Header
                Me.GroupHeader1.Visible = True
                Me.XrTable10.Visible = True
                'Detail
                Me.GroupFooter1.Visible = True
                Me.XrTable5.Visible = True
                'GroupFooter
                Me.GroupFooter2.Visible = True
                Me.XrTable14.Visible = True
                'Footer
                Me.XrTable6.Visible = True
            End If
        End If
        XrTableCell32.Text = ResourceManager.GetString("EntityTypeName", "Portfolio")
        XrTableCell4.Text = ResourceManager.GetString("EntityTypeName", "Portfolio")
        Me.INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        Me.INDLblNitCompany.Text = "Nit : " & IndigoSessionValues.IndigoCompanyNit
        Me.INDLblDate.Text = "Fecha de Corte : " & CDate(Me.filters("ClosingDate")).ToString("dd De MMMM Del yyyy")
        INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        GenerateMinimumAgeRangeCells()
        GenerateAgeRangesCells()
        GenerateMaximumAgeRangeCells()
        GenerateTotalRowCells()
    End Sub

#Region "Detailed"

#Region "Headers"

    Private Sub XrTable1_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTable1.BeforePrint, XrTable13.BeforePrint, XrTable9.BeforePrint
        Dim table As XRTable = (CType(sender, XRTable))
        If Not table.Visible Then
            e.Cancel = True
            Exit Sub
        End If

        table.WidthF = Me.PageWidth - Me.Margins.Left - Me.Margins.Right - 1

        Dim row As XRTableRow = (CType(table.Rows(0), XRTableRow))

        'sacar el tamaño de las celdas
        Dim CellCount = row.Cells.Count
        Dim WidthCells = 1050 / CellCount

        Dim cont = 1
        For Each cel As XRTableCell In row.Cells
            cel.WidthF = WidthCells
        Next
    End Sub

#End Region

#Region "Detailed"

    Private Sub XrTable2_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTable2.BeforePrint
        Dim table As XRTable = (CType(sender, XRTable))
        If Not table.Visible Then
            e.Cancel = True
            Exit Sub
        End If

        table.WidthF = Me.PageWidth - Me.Margins.Left - Me.Margins.Right - 1

        Dim row As XRTableRow = (CType(table.Rows(0), XRTableRow))

        'sacar el tamaño de las celdas
        Dim CellCount = row.Cells.Count
        Dim WidthCells = 1050 / CellCount

        Dim cont = 1
        For Each cel As XRTableCell In row.Cells
            cel.WidthF = WidthCells
        Next
    End Sub

#End Region

#Region "Footer"

    Private Sub XrTable11_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTable11.BeforePrint, XrTable7.BeforePrint, XrTable12.BeforePrint
        Dim table As XRTable = (CType(sender, XRTable))
        If Not table.Visible Then
            e.Cancel = True
            Exit Sub
        End If

        table.WidthF = Me.PageWidth - Me.Margins.Left - Me.Margins.Right - 1

        Dim row As XRTableRow = (CType(table.Rows(0), XRTableRow))

        'sacar el tamaño de las celdas
        Dim CellCount = row.Cells.Count
        Dim WidthCells = 1050 / (CellCount + 3)

        Dim cont = 1
        For Each cel As XRTableCell In row.Cells
            If cont = 1 Then
                cel.WidthF = WidthCells * 3.5
            ElseIf cont = 2 Then
                cel.WidthF = WidthCells * 1.5
            Else
                cel.WidthF = WidthCells
            End If

            cont += 1
        Next
    End Sub

#End Region

#End Region

#Region "Summarized"

#Region "Headers"

    Private Sub XrTable4_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTable4.BeforePrint, XrTable10.BeforePrint
        Dim table As XRTable = (CType(sender, XRTable))
        If Not table.Visible Then
            e.Cancel = True
            Exit Sub
        End If

        table.WidthF = Me.PageWidth - Me.Margins.Left - Me.Margins.Right - 1

        Dim row As XRTableRow = (CType(table.Rows(0), XRTableRow))

        'sacar el tamaño de las celdas
        Dim CellCount = row.Cells.Count
        Dim WidthCells = 1050 / (CellCount + 2)

        Dim cont = 1
        For Each cel As XRTableCell In row.Cells

            If cont = 1 Then
                cel.WidthF = WidthCells * 3
            Else
                cel.WidthF = WidthCells
            End If

            cont += 1
        Next
    End Sub

#End Region

#Region "Detailed"

    Private Sub XrTable5_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTable5.BeforePrint, XrTable14.BeforePrint
        Dim table As XRTable = (CType(sender, XRTable))
        If Not table.Visible Then
            e.Cancel = True
            Exit Sub
        End If

        table.WidthF = Me.PageWidth - Me.Margins.Left - Me.Margins.Right - 1

        Dim row As XRTableRow = (CType(table.Rows(0), XRTableRow))

        'sacar el tamaño de las celdas
        Dim CellCount = row.Cells.Count
        Dim WidthCells = 1050 / (CellCount + 2)

        Dim cont = 1
        For Each cel As XRTableCell In row.Cells

            If cont = 1 Then
                cel.WidthF = WidthCells * 3
            Else
                cel.WidthF = WidthCells
            End If

            cont += 1
        Next
    End Sub

#End Region

#Region "Footer"

    Private Sub XrTable6_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTable6.BeforePrint
        Dim table As XRTable = (CType(sender, XRTable))
        If Not table.Visible Then
            e.Cancel = True
            Exit Sub
        End If

        table.WidthF = Me.PageWidth - Me.Margins.Left - Me.Margins.Right - 1

        Dim row As XRTableRow = (CType(table.Rows(0), XRTableRow))

        'sacar el tamaño de las celdas
        Dim CellCount = row.Cells.Count
        Dim WidthCells = 1050 / (CellCount + 2)

        Dim cont = 1
        For Each cel As XRTableCell In row.Cells

            If cont = 1 Then
                cel.WidthF = WidthCells * 3
            Else
                cel.WidthF = WidthCells
            End If

            cont += 1
        Next
    End Sub



#End Region

#End Region

#End Region

End Class