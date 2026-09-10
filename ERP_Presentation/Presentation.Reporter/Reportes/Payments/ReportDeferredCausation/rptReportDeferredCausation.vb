#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.PaymentsRepository
Imports Infrastructure.Data.Xpo
Imports DevExpress.XtraReports.UI

#End Region

Public Class rptReportDeferredCausation
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Dim filtroConsulta As String = "GetDate('01/' + ToStr(paymentMonth) + '/' + ToStr(paymentYear)) >= #" & Format(ParametrosReporte(0), "yyyy-MM-dd") & "# AND GetDate('01/' + ToStr(paymentMonth) + '/' + ToStr(paymentYear)) <= #" & Format(ParametrosReporte(1), "yyyy-MM-dd") & "#"

        'Filtro por Terceros
        If ParametrosReporte(2) IsNot Nothing And ParametrosReporte(3) IsNot Nothing Then
            filtroConsulta &= " AND accountPayableBillNumber >= '" & ParametrosReporte(2) & "' AND accountPayableBillNumber <= '" & ParametrosReporte(3) & "'"
        End If

        Dim listReport As List(Of PaymentsVReportDeferredCausationXpo) = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PaymentsService.GetCollection(Of PaymentsVReportDeferredCausationXpo)(Nothing, filtroConsulta)
        
        Me.DataSource = listReport
    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptReportDeferredCausation_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        Me.INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        Me.INDLblNitCompany.Text = "Nit : " & IndigoSessionValues.IndigoCompanyNit
        Me.INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        INDLblSubTitle.Text = "Informe comprendido entre " & CDate(Me.ParametrosReporte(0)).ToString("MMMM Del yyyy") & " " & CDate(Me.ParametrosReporte(1)).ToString("A MMMM Del yyyy")

        Dim y As List(Of PaymentsVReportDeferredCausationXpo) = Me.DataSource
        Dim groupData = From i In y
                        Group By i.CurrencyAbbreviation Into g = Group
                        Select CurrencyAbbreviation, valueNotArmotize = g.Sum(Function(x) x.valueNotArmotize),
                            valueArmotize = g.Sum(Function(t) t.valueArmotize)
        For Each item In groupData
            XrTable3.InsertRowBelow(XrTable3.Rows.LastRow)
            Dim irow = XrTable3.Rows.LastRow.Index
            For Each Column As XRTableCell In XrTableRow3
                Dim cell = XrTable3.Rows(irow).Cells.Item(Column.Index)
                Select Case Column.Name
                    Case NameOf(XrTableCell14)
                        cell.Text = "Total de reporte en " + item.CurrencyAbbreviation + ":"
                        cell.Font = New Font("Arial", 10, FontStyle.Bold)
                    Case NameOf(XrTableCell15)
                        cell.Text = Utils.GetMoneyWithISO4217(CDec(item.valueNotArmotize), If(String.IsNullOrEmpty(item.CurrencyAbbreviation),
                                             IndigoSessionValues.CurrencyISO4217, item.CurrencyAbbreviation))
                    Case NameOf(XrTableCell16)
                        cell.Text = Utils.GetMoneyWithISO4217(CDec(item.valueArmotize), If(String.IsNullOrEmpty(item.CurrencyAbbreviation),
                                            IndigoSessionValues.CurrencyISO4217, item.CurrencyAbbreviation))
                End Select
                XrTable3.Rows(irow).Cells.Add(cell)
            Next
        Next
    End Sub

    Private Sub XrTableCell6_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell6.BeforePrint
        Dim row = GetCurrentRow()
        If row IsNot Nothing Then
            Dim currency = row.CurrencyAbbreviation
            If String.IsNullOrWhiteSpace(currency) Then currency = IndigoSessionValues.CurrencyISO4217
            XrTableCell6.Text = Utils.GetMoneyWithISO4217(row.valuePeriod, currency)
        End If
    End Sub

    Private Sub XrTableCell7_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell7.BeforePrint
        Dim row = GetCurrentRow()
        If row IsNot Nothing Then
            Dim currency = row.CurrencyAbbreviation
            If String.IsNullOrWhiteSpace(currency) Then currency = IndigoSessionValues.CurrencyISO4217
            XrTableCell7.Text = Utils.GetMoneyWithISO4217(row.valueNotArmotize, currency)
        End If
    End Sub

    Private Sub XrTableCell8_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell8.BeforePrint
        Dim row = GetCurrentRow()
        If row IsNot Nothing Then
            Dim currency = row.CurrencyAbbreviation
            If String.IsNullOrWhiteSpace(currency) Then currency = IndigoSessionValues.CurrencyISO4217
            XrTableCell8.Text = Utils.GetMoneyWithISO4217(row.valueArmotize, currency)
        End If
    End Sub

    Private Sub XrTableCell15_SummaryGetResult(sender As Object, e As SummaryGetResultEventArgs) Handles XrTableCell15.SummaryGetResult, XrTableCell16.SummaryGetResult
        Dim row = GetCurrentRow()
        e.Result = Utils.GetMoneyWithISO4217(e.CalculatedValues.ToEntityList(Of Decimal).Sum(), If(String.IsNullOrEmpty(row?.CurrencyAbbreviation),
                                             IndigoSessionValues.CurrencyISO4217, row?.CurrencyAbbreviation))
        e.Handled = True
    End Sub
End Class