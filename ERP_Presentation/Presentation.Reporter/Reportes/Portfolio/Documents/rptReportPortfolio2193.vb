#Region "Imports"

Imports DevExpress.XtraReports.UI
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PortfolioRepository
Imports Presentation.Base
Imports Presentation.CloudAgent

#End Region

Public Class rptReportPortfolio2193
    Implements IReport

#Region "Properties"

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Dim criterias As Dictionary(Of String, String)

    Dim filters As Dictionary(Of String, String)

    Private listSettingPortfolio As List(Of PortfolioAgesPortfolioXpo)

    Dim dtReportPortfolio2193 As DataTable

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

    Public Async Function CargarDataSourceAsync() As Task
        Try
            criterias = ParametrosReporte(0)
            filters = ParametrosReporte(1)

            Dim ds As DataSet = Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetListReportPortfolio2193Async(criterias, filters, Me.IndigoSessionValues)
            If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                dtReportPortfolio2193 = ds.Tables("ReportPortfolio2193")
                Me.DataSource = dtReportPortfolio2193
                Me.DataMember = "ReportPortfolio2193"

                LoadAgesPortfolio(CInt(Me.criterias("OperatingUnit")))
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

        'creamos los campos calculados para asignar el valor a las celdas que estan en la tabla AgesPayment
        Dim index = 1
        For Each item In listSettingPortfolio
            index += 1
            Dim INDCfDetail As New CalculatedField()
            Me.CalculatedFields.Add(INDCfDetail)
            INDCfDetail.DataSource = Me.DataSource
            INDCfDetail.DataMember = Me.DataMember
            INDCfDetail.Expression = "Iif([Age] >=" & item.InitialRange & " And [Age] <= " & item.EndRange & ", [Balance], 0)"
            INDCfDetail.Name = "INDCFDetail" & index
        Next

        'creamos el campo calculado para asignar el valor a la celda de las facturas que tienen el rango maximo de vencimiento
        Dim INDCfMax As New CalculatedField()
        Me.CalculatedFields.Add(INDCfMax)
        INDCfMax.DataSource = Me.DataSource
        INDCfMax.DataMember = Me.DataMember
        INDCfMax.Expression = "Iif([Age] > " & MaximunAgeRange & ", [Balance], 0)"
        INDCfMax.Name = "INDCFHigherValue"

        'creamos el campo calculado para asignar el valor a la celda de las facturas que tienen el rango maximo de vencimiento
        Dim INDCfRadicated As New CalculatedField()
        Me.CalculatedFields.Add(INDCfRadicated)
        INDCfRadicated.DataSource = Me.DataSource
        INDCfRadicated.DataMember = Me.DataMember
        INDCfRadicated.Expression = "Iif([Age] >= " & RangeMin & ", [Balance], 0)"
        INDCfRadicated.Name = "INDCfRadicated"

        'creamos el campo calculado para asignar el valor a la celda de las facturas no vencidas
        Dim INDCfJunior As New CalculatedField()
        Me.CalculatedFields.Add(INDCfJunior)
        INDCfJunior.DataSource = Me.DataSource
        INDCfJunior.DataMember = Me.DataMember
        INDCfJunior.Expression = "Iif([Age] < " & RangeMin & ", [Balance], 0)"
        INDCfJunior.Name = "INDCFLowerValue"
    End Sub

    Public Sub GenerateAgeRangesCells()
        Dim index = 1
        For Each item In listSettingPortfolio
            index += 1

            'imprimir las celdas de los registros que estan en la tabla portfolioages en el pageheader
            Dim cell As New XRTableCell()
            cell.Text = item.Name
            cell.Name = "INDCllPageHeader" & index
            XrTableRow1.Cells.Add(cell)

            'crear el campo suma por grupos por regimen
            Dim summaryRegime As XRSummary = New XRSummary()
            summaryRegime.FormatString = "{0:c0}"
            summaryRegime.IgnoreNullValues = True
            summaryRegime.Running = DevExpress.XtraReports.UI.SummaryRunning.Group

            'imprimir las celdas de los registros que estan en la tabla portfolioages en el footer de grupo por tercero
            Dim cellFooterThird As New XRTableCell()
            cellFooterThird.DataBindings.Add("Text", Nothing, "INDCFDetail" & index)
            cellFooterThird.Summary = summaryRegime
            XrTableRow2.Cells.Add(cellFooterThird)

            'crear el campo suma por agrupacion primaria
            Dim summaryDetailPrimary As XRSummary = New XRSummary()
            summaryDetailPrimary.FormatString = "{0:c0}"
            summaryDetailPrimary.IgnoreNullValues = True
            summaryDetailPrimary.Running = DevExpress.XtraReports.UI.SummaryRunning.Group

            'imprimir las celdas de los registros que estan en la tabla portfolioages en el footer de grupo por tercero
            Dim cellFooterPrimary As New XRTableCell()
            cellFooterPrimary.DataBindings.Add("Text", Nothing, "INDCFDetail" & index)
            cellFooterPrimary.Summary = summaryDetailPrimary
            XrTableRow5.Cells.Add(cellFooterPrimary)

            'crear el campo suma por reporte
            Dim summaryDetailReport As XRSummary = New XRSummary()
            summaryDetailReport.FormatString = "{0:c0}"
            summaryDetailReport.IgnoreNullValues = True
            summaryDetailReport.Running = DevExpress.XtraReports.UI.SummaryRunning.Report

            'imprimir las celdas de los registros que estan en la tabla portfolioages en el footer del reporte
            Dim cellReportValue As New XRTableCell()
            cellReportValue.DataBindings.Add("Text", Nothing, "INDCFDetail" & index)
            cellReportValue.Summary = summaryDetailReport
            XrTableRow12.Cells.Add(cellReportValue)
        Next
    End Sub

    Public Sub GenerateMaximumAgeRangeCells()
        'imprimir las celda rango maximo en el pageheader
        Dim cell100 As New XRTableCell()
        cell100.Text = CType(listSettingPortfolio(0), PortfolioAgesPortfolioXpo).SettingPortfolioId.NameMaximumAgeRange
        XrTableRow1.Cells.Add(cell100)

        'crear el campo suma por grupos
        Dim summaryMax As XRSummary = New XRSummary()
        summaryMax.FormatString = "{0:c0}"
        summaryMax.IgnoreNullValues = True
        summaryMax.Running = DevExpress.XtraReports.UI.SummaryRunning.Group

        'imprimir las celda rango maximo en el footer del  grupo por tercero
        Dim cell104 As New XRTableCell()
        cell104.DataBindings.Add("Text", Nothing, "INDCFHigherValue")
        cell104.Summary = summaryMax
        XrTableRow2.Cells.Add(cell104)

        'crear el campo suma por agrupacion primaria
        Dim summaryDetailPrimary As XRSummary = New XRSummary()
        summaryDetailPrimary.FormatString = "{0:c0}"
        summaryDetailPrimary.IgnoreNullValues = True
        summaryDetailPrimary.Running = DevExpress.XtraReports.UI.SummaryRunning.Group

        'imprimir las celdas de los registros que estan en la tabla portfolioages en el footer de grupo por tercero
        Dim cellFooterPrimary As New XRTableCell()
        cellFooterPrimary.DataBindings.Add("Text", Nothing, "INDCFHigherValue")
        cellFooterPrimary.Summary = summaryDetailPrimary
        XrTableRow5.Cells.Add(cellFooterPrimary)

        'crear el campo suma por reporte
        Dim summaryMaxReport As XRSummary = New XRSummary()
        summaryMaxReport.FormatString = "{0:c0}"
        summaryMaxReport.IgnoreNullValues = True
        summaryMaxReport.Running = DevExpress.XtraReports.UI.SummaryRunning.Report

        'imprimir las celda rango maximo en el footer del reporte
        Dim cell15 As New XRTableCell()
        cell15.DataBindings.Add("Text", Nothing, "INDCFHigherValue")
        cell15.Summary = summaryMaxReport
        XrTableRow12.Cells.Add(cell15)
    End Sub

    Public Sub GenerateRadicatedCells()
        'imprimir las celda rango maximo en el pageheader
        Dim cell100 As New XRTableCell()
        cell100.Text = "Total Cartera Radicada"
        XrTableRow1.Cells.Add(cell100)

        'crear el campo suma por grupos
        Dim summaryMax As XRSummary = New XRSummary()
        summaryMax.FormatString = "{0:c0}"
        summaryMax.IgnoreNullValues = True
        summaryMax.Running = DevExpress.XtraReports.UI.SummaryRunning.Group

        'imprimir las celda rango maximo en el footer del  grupo por tercero
        Dim cell104 As New XRTableCell()
        cell104.DataBindings.Add("Text", Nothing, "INDCfRadicated")
        cell104.Summary = summaryMax
        XrTableRow2.Cells.Add(cell104)

        'crear el campo suma por agrupacion primaria
        Dim summaryDetailPrimary As XRSummary = New XRSummary()
        summaryDetailPrimary.FormatString = "{0:c0}"
        summaryDetailPrimary.IgnoreNullValues = True
        summaryDetailPrimary.Running = DevExpress.XtraReports.UI.SummaryRunning.Group

        'imprimir las celdas de los registros que estan en la tabla portfolioages en el footer de grupo por tercero
        Dim cellFooterPrimary As New XRTableCell()
        cellFooterPrimary.DataBindings.Add("Text", Nothing, "INDCfRadicated")
        cellFooterPrimary.Summary = summaryDetailPrimary
        XrTableRow5.Cells.Add(cellFooterPrimary)

        'crear el campo suma por reporte
        Dim summaryMaxReport As XRSummary = New XRSummary()
        summaryMaxReport.FormatString = "{0:c0}"
        summaryMaxReport.IgnoreNullValues = True
        summaryMaxReport.Running = DevExpress.XtraReports.UI.SummaryRunning.Report

        'imprimir las celda rango maximo en el footer del reporte
        Dim cell15 As New XRTableCell()
        cell15.DataBindings.Add("Text", Nothing, "INDCfRadicated")
        cell15.Summary = summaryMaxReport
        XrTableRow12.Cells.Add(cell15)
    End Sub

    Public Sub GenerateMinimumAgeRangeCells()
        'imprimir las celda rango maximo en el pageheader
        Dim cell100 As New XRTableCell()
        cell100.Text = CType(listSettingPortfolio(0), PortfolioAgesPortfolioXpo).SettingPortfolioId.NameMinimumAgeRange
        XrTableRow1.Cells.Add(cell100)

        'crear el campo suma por grupos
        Dim summaryMax As XRSummary = New XRSummary()
        summaryMax.FormatString = "{0:c0}"
        summaryMax.IgnoreNullValues = True
        summaryMax.Running = DevExpress.XtraReports.UI.SummaryRunning.Group

        'imprimir las celda rango maximo en el footer del  grupo por tercero
        Dim cell104 As New XRTableCell()
        cell104.DataBindings.Add("Text", Nothing, "INDCFLowerValue")
        cell104.Summary = summaryMax
        XrTableRow2.Cells.Add(cell104)

        'crear el campo suma por agrupacion primaria
        Dim summaryDetailPrimary As XRSummary = New XRSummary()
        summaryDetailPrimary.FormatString = "{0:c0}"
        summaryDetailPrimary.IgnoreNullValues = True
        summaryDetailPrimary.Running = DevExpress.XtraReports.UI.SummaryRunning.Group

        'imprimir las celdas de los registros que estan en la tabla portfolioages en el footer de grupo por tercero
        Dim cellFooterPrimary As New XRTableCell()
        cellFooterPrimary.DataBindings.Add("Text", Nothing, "INDCFLowerValue")
        cellFooterPrimary.Summary = summaryDetailPrimary
        XrTableRow5.Cells.Add(cellFooterPrimary)

        'crear el campo suma por reporte
        Dim summaryMaxReport As XRSummary = New XRSummary()
        summaryMaxReport.FormatString = "{0:c0}"
        summaryMaxReport.IgnoreNullValues = True
        summaryMaxReport.Running = DevExpress.XtraReports.UI.SummaryRunning.Report

        'imprimir las celda rango maximo en el footer del reporte
        Dim cell15 As New XRTableCell()
        cell15.DataBindings.Add("Text", Nothing, "INDCFLowerValue")
        cell15.Summary = summaryMaxReport
        XrTableRow12.Cells.Add(cell15)
    End Sub

    Private Sub GenerateColumnGlosa()
        'imprimir las celda rango maximo en el pageheader
        Dim cell100 As New XRTableCell()
        cell100.Text = "Valor Glosado"
        XrTableRow1.Cells.Add(cell100)

        'crear el campo suma por grupos
        Dim summaryMax As XRSummary = New XRSummary()
        summaryMax.FormatString = "{0:c0}"
        summaryMax.IgnoreNullValues = True
        summaryMax.Running = DevExpress.XtraReports.UI.SummaryRunning.Group

        'imprimir las celda rango maximo en el footer del  grupo por tercero
        Dim cell104 As New XRTableCell()
        cell104.DataBindings.Add("Text", Nothing, "ValueGlosado")
        cell104.Summary = summaryMax
        XrTableRow2.Cells.Add(cell104)

        'crear el campo suma por agrupacion primaria
        Dim summaryDetailPrimary As XRSummary = New XRSummary()
        summaryDetailPrimary.FormatString = "{0:c0}"
        summaryDetailPrimary.IgnoreNullValues = True
        summaryDetailPrimary.Running = DevExpress.XtraReports.UI.SummaryRunning.Group

        'imprimir las celdas de los registros que estan en la tabla portfolioages en el footer de grupo por tercero
        Dim cellFooterPrimary As New XRTableCell()
        cellFooterPrimary.DataBindings.Add("Text", Nothing, "ValueGlosado")
        cellFooterPrimary.Summary = summaryDetailPrimary
        XrTableRow5.Cells.Add(cellFooterPrimary)

        'crear el campo suma por reporte
        Dim summaryMaxReport As XRSummary = New XRSummary()
        summaryMaxReport.FormatString = "{0:c0}"
        summaryMaxReport.IgnoreNullValues = True
        summaryMaxReport.Running = DevExpress.XtraReports.UI.SummaryRunning.Report

        'imprimir las celda rango maximo en el footer del reporte
        Dim cell15 As New XRTableCell()
        cell15.DataBindings.Add("Text", Nothing, "ValueGlosado")
        cell15.Summary = summaryMaxReport
        XrTableRow12.Cells.Add(cell15)
    End Sub

    Private Sub GenerateColumnDeterioration()
        'imprimir las celda rango maximo en el pageheader
        Dim cell100 As New XRTableCell()
        cell100.Text = "Deterioro"
        XrTableRow1.Cells.Add(cell100)

        'crear el campo suma por grupos
        Dim summaryMax As XRSummary = New XRSummary()
        summaryMax.FormatString = "{0:c0}"
        summaryMax.IgnoreNullValues = True
        summaryMax.Running = DevExpress.XtraReports.UI.SummaryRunning.Group

        'imprimir las celda rango maximo en el footer del  grupo por tercero
        Dim cell104 As New XRTableCell()
        cell104.DataBindings.Add("Text", Nothing, "DeteriorationBalance")
        cell104.Summary = summaryMax
        XrTableRow2.Cells.Add(cell104)

        'crear el campo suma por agrupacion primaria
        Dim summaryDetailPrimary As XRSummary = New XRSummary()
        summaryDetailPrimary.FormatString = "{0:c0}"
        summaryDetailPrimary.IgnoreNullValues = True
        summaryDetailPrimary.Running = DevExpress.XtraReports.UI.SummaryRunning.Group

        'imprimir las celdas de los registros que estan en la tabla portfolioages en el footer de grupo por tercero
        Dim cellFooterPrimary As New XRTableCell()
        cellFooterPrimary.DataBindings.Add("Text", Nothing, "DeteriorationBalance")
        cellFooterPrimary.Summary = summaryDetailPrimary
        XrTableRow5.Cells.Add(cellFooterPrimary)

        'crear el campo suma por reporte
        Dim summaryMaxReport As XRSummary = New XRSummary()
        summaryMaxReport.FormatString = "{0:c0}"
        summaryMaxReport.IgnoreNullValues = True
        summaryMaxReport.Running = DevExpress.XtraReports.UI.SummaryRunning.Report

        'imprimir las celda rango maximo en el footer del reporte
        Dim cell15 As New XRTableCell()
        cell15.DataBindings.Add("Text", Nothing, "DeteriorationBalance")
        cell15.Summary = summaryMaxReport
        XrTableRow12.Cells.Add(cell15)
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

    Private Sub rptReportPortfolio2193_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        Me.INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        Me.INDLblNitCompany.Text = "Nit : " & IndigoSessionValues.IndigoCompanyNit
        Me.INDLblDate.Text = "Fecha de Corte : " & CDate(Me.criterias("ClosingDate")).ToString("dd De MMMM Del yyyy")
        INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName

        GenerateAgeRangesCells()
        GenerateMaximumAgeRangeCells()
        GenerateRadicatedCells()
        GenerateMinimumAgeRangeCells()
        GenerateColumnGlosa()
        GenerateColumnDeterioration()
    End Sub

    Private Sub XrTable_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTable1.BeforePrint, XrTable2.BeforePrint, XrTable5.BeforePrint, XrTable12.BeforePrint
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

End Class