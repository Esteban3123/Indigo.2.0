#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PaymentsRepository
Imports DevExpress.XtraReports.UI
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.CloudAgent
Imports System.Globalization

#End Region

Public Class rptReportListOfPaymentsAges
    Implements IReport

#Region "Properties"

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Dim criterias As Dictionary(Of String, String)

    Dim filters As Dictionary(Of String, String)

    Private listSettingPayments As List(Of PaymentsAgesPaymentsXpo)

    Dim dtReportPaymentsByAge As DataTable

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

#End Region

#Region "Variables"
    Public _currency As CommonCurrencyXpo
    Public _isValorization As Boolean
#End Region

#Region "Load Data"

    Public Sub CargarDataSource() Implements IReport.CargarDataSource

    End Sub

    Public Async Function CargarDataSourceAsync(Optional ds As DataSet = Nothing) As Task
        Try
            criterias = ParametrosReporte(0)
            filters = ParametrosReporte(1)
            If ds Is Nothing Then
                ds = Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetListReportPaymentsByAgeAsync(criterias, filters, Me.IndigoSessionValues)
            End If
            If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                dtReportPaymentsByAge = ds.Tables("ReportPaymentsByAge")
                If Not _isValorization Then
                    Dim filterByCurrency = (From item In dtReportPaymentsByAge.AsEnumerable Where item("CurrencyId") IsNot DBNull.Value AndAlso item("CurrencyId") = Me._currency.Id Select item)
                    If filterByCurrency.Count = 0 Then
                        Me.DataSource = Nothing
                        Exit Function
                    End If
                    dtReportPaymentsByAge = filterByCurrency.CopyToDataTable
                End If
                Me.DataSource = dtReportPaymentsByAge
                Me.DataMember = "ReportPaymentsByAge"
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
        Dim filtroConsultaSettingsPayment As String = "SettingPaymentId.IdOperatingUnit = " & OperatingUnitId
        listSettingPayments = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PaymentsService.GetCollection(Of PaymentsAgesPaymentsXpo)(Nothing, filtroConsultaSettingsPayment)
    End Sub

    Public Sub GenerateCalculatedFields()
        Dim RangeMin = listSettingPayments.Min(Function(x) x.InitialRange)
        Dim MaximunAgeRange = CType(listSettingPayments(0), PaymentsAgesPaymentsXpo).SettingPaymentId.MaximunAgeRange

        'creamos el campo calculado para asignar el valor a la celda de las facturas no vencidas
        Dim INDCfJunior As New CalculatedField()
        Me.CalculatedFields.Add(INDCfJunior)
        INDCfJunior.DataSource = Me.DataSource
        INDCfJunior.DataMember = Me.DataMember
        INDCfJunior.Expression = "Iif([Age] < " & RangeMin & ", [Balance], 0)"
        INDCfJunior.Name = "INDCFLowerValue"

        'creamos el campo calculado para asignar el valor a la celda de las facturas que tienen el rango maximo de vencimiento
        Dim INDCfMax As New CalculatedField()
        Me.CalculatedFields.Add(INDCfMax)
        INDCfMax.DataSource = Me.DataSource
        INDCfMax.DataMember = Me.DataMember
        INDCfMax.Expression = "Iif([Age] > " & MaximunAgeRange & ", [Balance], 0)"
        INDCfMax.Name = "INDCFHigherValue"

        'creamos los campos calculados para asignar el valor a las celdas que estan en la tabla AgesPayment
        Dim index = 1
        For Each item In listSettingPayments
            index += 1
            Dim INDCfDetail As New CalculatedField()
            Me.CalculatedFields.Add(INDCfDetail)
            INDCfDetail.DataSource = Me.DataSource
            INDCfDetail.DataMember = Me.DataMember
            INDCfDetail.Expression = "Iif([Age] >=" & item.InitialRange & " And [Age] <= " & item.EndRange & ", [Balance], 0)"
            INDCfDetail.Name = "INDCFDetail" & index
        Next
    End Sub

    Public Sub GenerateMinimumAgeRangeCells()
        If criterias("TypeReport") = 1 Then
            'imprimir la celda de facturas sin vencer pageheader
            Dim cell5 As New XRTableCell()
            cell5.Text = CType(listSettingPayments(0), PaymentsAgesPaymentsXpo).SettingPaymentId.NameMinimumAgeRange
            XrTableRow1.Cells.Add(cell5)

            'imprimir la celda de facturas sin vencer detalle
            Dim cell101 As New XRTableCell()
            cell101.FormattingRules.Add(Me.INDFrAdvance)
            XrTableRow2.Cells.Add(cell101)
            cell101.DataBindings.Add("Text", Nothing, "INDCFLowerValue", "{0:c" + IndigoSessionValues.CurrencyNumbertFormat.CurrencyDecimalDigits.ToString() + "}")

            'crear el campo suma por grupos
            Dim summaryMin As XRSummary = New XRSummary()
            summaryMin.FormatString = "{0:c" + IndigoSessionValues.CurrencyNumbertFormat.CurrencyDecimalDigits.ToString() + "}"
            summaryMin.IgnoreNullValues = True
            summaryMin.Running = DevExpress.XtraReports.UI.SummaryRunning.Group

            'imprimir la celda de facturas sin vencer en el footer de grupo por avances o cuentas por pagar
            Dim cell110 As New XRTableCell()
            XrTableRow7.Cells.Add(cell110)
            cell110.DataBindings.Add("Text", Nothing, "INDCFLowerValue")
            cell110.FormattingRules.Add(Me.INDFrAdvance)
            cell110.Summary = summaryMin

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
            cell5.Text = CType(listSettingPayments(0), PaymentsAgesPaymentsXpo).SettingPaymentId.NameMinimumAgeRange
            XrTableRow4.Cells.Add(cell5)

            'crear el campo suma por grupos
            Dim summaryMin As XRSummary = New XRSummary()
            summaryMin.FormatString = "{0:c" + IndigoSessionValues.CurrencyNumbertFormat.CurrencyDecimalDigits.ToString() + "}"
            summaryMin.IgnoreNullValues = True
            summaryMin.Running = DevExpress.XtraReports.UI.SummaryRunning.Group

            'imprimir la celda de facturas sin vencer en el footer de grupo por avances o cuentas por pagar
            Dim cell110 As New XRTableCell()
            cell110.DataBindings.Add("Text", Nothing, "INDCFLowerValue")
            cell110.FormattingRules.Add(Me.INDFrAdvance)
            cell110.Summary = summaryMin
            XrTableRow5.Cells.Add(cell110)

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
        For Each item In listSettingPayments
            index += 1

            If criterias("TypeReport") = 1 Then
                'imprimir las celdas de los registros que estan en la tabla portfolioages en el pageheader
                Dim cell As New XRTableCell()
                cell.Text = item.Name
                cell.Name = "INDCllPageHeader" & index
                XrTableRow1.Cells.Add(cell)

                'imprimir las celdas de los registros que estan en la tabla portfolioages en el detalle
                Dim cellDetail As New XRTableCell()
                cellDetail.DataBindings.Add("Text", Nothing, "INDCFDetail" & index, "{0:c" + IndigoSessionValues.CurrencyNumbertFormat.CurrencyDecimalDigits.ToString() + "}")
                cellDetail.Name = "INDCllDetail" & index
                cellDetail.FormattingRules.Add(Me.INDFrAdvance)
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
                XrTableRow4.Cells.Add(cell)

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
        If criterias("TypeReport") = 1 Then
            'imprimir las celda rango maximo en el pageheader
            Dim cell100 As New XRTableCell()
            cell100.Text = CType(listSettingPayments(0), PaymentsAgesPaymentsXpo).SettingPaymentId.NameMaximumAgeRange
            XrTableRow1.Cells.Add(cell100)

            'imprimir las celda rango maximo en el detalle
            Dim cell102 As New XRTableCell()
            cell102.DataBindings.Add("Text", Nothing, "INDCFHigherValue", "{0:c" + IndigoSessionValues.CurrencyNumbertFormat.CurrencyDecimalDigits.ToString() + "}")
            cell102.FormattingRules.Add(Me.INDFrAdvance)
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
            cell100.Text = CType(listSettingPayments(0), PaymentsAgesPaymentsXpo).SettingPaymentId.NameMaximumAgeRange
            XrTableRow4.Cells.Add(cell100)

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
        If criterias("TypeReport") <> 1 Then
            'imprimir las celda total en el pageheader
            Dim cell100 As New XRTableCell()
            cell100.Text = "Total"
            XrTableRow4.Cells.Add(cell100)

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

    Private Sub rptReportListOfPaymentsAges_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        Me.INDPrmTypeReport.Value = criterias("TypeReport")
        Me.INDPrmGroupBy.Value = criterias("GroupBy")

        Me.XrTable1.Visible = If(criterias("TypeReport") = 1, True, False)
        Me.XrTable12.Visible = If(criterias("TypeReport") = 1, True, False)
        Me.XrTable4.Visible = If(criterias("TypeReport") = 1, False, True)
        Me.XrTable6.Visible = If(criterias("TypeReport") = 1, False, True)

        Me.INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        Me.INDLblNitCompany.Text = "Nit : " & IndigoSessionValues.IndigoCompanyNit
        Me.INDLblDate.Text = "Fecha de Corte : " & CDate(Me.filters("ClosingDate")).ToString("dd De MMMM Del yyyy")
        INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName

        If Me._isValorization Then
            XrlCurrency.Text = String.Concat("Valorizado a Moneda: ", _currency.CurrencyName)
        Else
            XrlCurrency.Text = String.Concat("Moneda: ", _currency.CurrencyName)
        End If

        'Aplico el formato preestablecido
        If _currency IsNot Nothing AndAlso Not String.IsNullOrEmpty(_currency.Abbreviation) Then
            Dim _culture As CultureInfo = CultureInfo.CurrentCulture.Clone()
            _culture.NumberFormat = New CultureInfo(_currency.Abbreviation.GetCultureId).NumberFormat
            ApplyLocalization(_culture)
        End If

        GenerateMinimumAgeRangeCells()
        GenerateAgeRangesCells()
        GenerateMaximumAgeRangeCells()
        GenerateTotalRowCells()
    End Sub

#Region "Detailed"

    Private Sub XrTable1_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTable1.BeforePrint
        Dim table As XRTable = (CType(sender, XRTable))
        table.WidthF = Me.PageWidth - Me.Margins.Left - Me.Margins.Right - 1

        Dim row As XRTableRow = (CType(table.Rows(0), XRTableRow))

        'sacar el tamaño de las celdas
        Dim CellCount = XrTableRow1.Cells.Count
        Dim WidthCells = 1050 / CellCount

        Dim cont = 1
        For Each cel As XRTableCell In row.Cells
            cel.WidthF = WidthCells
        Next
    End Sub

    Private Sub XrTable2_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTable2.BeforePrint
        Dim table As XRTable = (CType(sender, XRTable))
        table.WidthF = Me.PageWidth - Me.Margins.Left - Me.Margins.Right - 1

        Dim row As XRTableRow = (CType(table.Rows(0), XRTableRow))

        'sacar el tamaño de las celdas
        Dim CellCount = XrTableRow2.Cells.Count
        Dim WidthCells = 1050 / CellCount

        Dim cont = 1
        For Each cel As XRTableCell In row.Cells
            cel.WidthF = WidthCells
        Next
    End Sub

    Private Sub XrTable7_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTable7.BeforePrint
        Dim table As XRTable = (CType(sender, XRTable))
        table.WidthF = Me.PageWidth - Me.Margins.Left - Me.Margins.Right - 1

        Dim row As XRTableRow = (CType(table.Rows(0), XRTableRow))

        'sacar el tamaño de las celdas
        Dim CellCount = XrTableRow7.Cells.Count
        Dim WidthCells = 1050 / (CellCount + 1)

        Dim cont = 1
        For Each cel As XRTableCell In row.Cells

            If cont = 1 Then
                cel.WidthF = WidthCells * 2
            Else
                cel.WidthF = WidthCells
            End If

            cont += 1
        Next
    End Sub

    Private Sub XrTable12_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTable12.BeforePrint
        Dim table As XRTable = (CType(sender, XRTable))
        table.WidthF = Me.PageWidth - Me.Margins.Left - Me.Margins.Right - 1

        Dim row As XRTableRow = (CType(table.Rows(0), XRTableRow))

        'sacar el tamaño de las celdas
        Dim CellCount = XrTableRow12.Cells.Count
        Dim WidthCells = 1050 / (CellCount + 1)

        Dim cont = 1
        For Each cel As XRTableCell In row.Cells

            If cont = 1 Then
                cel.WidthF = WidthCells * 2
            Else
                cel.WidthF = WidthCells
            End If

            cont += 1
        Next
    End Sub

#End Region

#Region "Summarized"

    Private Sub XrTable4_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTable4.BeforePrint
        Dim table As XRTable = (CType(sender, XRTable))
        table.WidthF = Me.PageWidth - Me.Margins.Left - Me.Margins.Right - 1

        Dim row As XRTableRow = (CType(table.Rows(0), XRTableRow))

        'sacar el tamaño de las celdas
        Dim CellCount = XrTableRow4.Cells.Count
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

    Private Sub XrTable5_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTable5.BeforePrint
        Dim table As XRTable = (CType(sender, XRTable))
        table.WidthF = Me.PageWidth - Me.Margins.Left - Me.Margins.Right - 1

        Dim row As XRTableRow = (CType(table.Rows(0), XRTableRow))

        'sacar el tamaño de las celdas
        Dim CellCount = XrTableRow5.Cells.Count
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

    Private Sub XrTable6_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTable6.BeforePrint
        Dim table As XRTable = (CType(sender, XRTable))
        table.WidthF = Me.PageWidth - Me.Margins.Left - Me.Margins.Right - 1

        Dim row As XRTableRow = (CType(table.Rows(0), XRTableRow))

        'sacar el tamaño de las celdas
        Dim CellCount = XrTableRow6.Cells.Count
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

End Class