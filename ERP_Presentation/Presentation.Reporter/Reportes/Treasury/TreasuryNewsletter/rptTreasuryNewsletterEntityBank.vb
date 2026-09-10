#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.TreasuryRepository
Imports Domain.Entities
Imports Presentation.Base
Imports DevExpress.XtraReports.UI

#End Region

Public Class rptTreasuryNewsletterEntityBank
    Implements IReport
    Implements IReportAsync

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            Dim filtroConsulta As String = Nothing
            filtroConsulta &= "DocumentDate >= #" & Format(ParametrosReporte(0), "yyyy-MM-dd HH:mm:ss") & "# AND DocumentDate <= #" & Format(ParametrosReporte(1), "yyyy-MM-dd HH:mm:ss") & "#"
            'filtro por cuentas bancarias
            If ParametrosReporte(3) <> "" And ParametrosReporte(4) <> "" Then
                filtroConsulta &= " AND EntityBankAccountCode >= '" & ParametrosReporte(3) & "' AND EntityBankAccountCode <= '" & ParametrosReporte(4) & "'"
            End If

            If ParametrosReporte(2) IsNot Nothing Then
                filtroConsulta &= " AND Status in (" & ParametrosReporte(2) & ")"
            End If

            If ParametrosReporte(5) IsNot Nothing Then
                filtroConsulta &= " AND EntityBankStatus in (" & ParametrosReporte(5) & ")"
            End If

            Dim INDList As List(Of TreasuryVReportTreasuryNewsletterEntityBankAccount) = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PayrollService.GetCollection(Of TreasuryVReportTreasuryNewsletterEntityBankAccount)(Nothing, filtroConsulta)
            Dim dictionaryBalance As New Dictionary(Of Integer, Decimal)
            For Each item In INDList.OrderBy(Function(x) x.DocumentDate)
                If Not dictionaryBalance.ContainsKey(item.EntityBankId) Then
                    dictionaryBalance.Add(item.EntityBankId, XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetReportTreasuryEntityBank(item.EntityBankId, item.DocumentDate, ParametrosReporte(2)))
                End If
                item.SaldoAnterior = dictionaryBalance(item.EntityBankId)
            Next

            Me.DataSource = INDList

        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try
    End Sub

    Public Function CargarDataSourceAsync() As Task Implements IReportAsync.CargarDataSourceAsync
        Return Task.Factory.StartNew(AddressOf CargarDataSource)
    End Function

    Public Function GetExceptionDetails(exception As Exception) As String
        Dim properties = exception.[GetType]().GetProperties()
        Dim fields = properties.[Select](Function([property]) New With {
            Key .Name = [property].Name,
            Key .Value = [property].GetValue(exception, Nothing)
        }).[Select](Function(x) [String].Format("{0} : {1}", x.Name, If(x.Value IsNot Nothing, x.Value.ToString(), [String].Empty)))
        Return [String].Join(vbLf, fields)
    End Function

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptTreasuryNewsletterEntityBank_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDLblDate.Text = "Informe comprendido entre " & CDate(ParametrosReporte(0)).ToString("dd De MMMM Del yyyy") & " " & CDate(ParametrosReporte(1)).ToString("A dd De MMMM Del yyyy")
        Me.INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
    End Sub

    Private Sub XrTable3_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTable3.BeforePrint
        Dim row = GetCurrentRow()
        Dim table8 As XRTable = CType(XrTable2, XRTable)
        Dim GroupHeader As GroupHeaderBand = CType(GroupHeader2, GroupHeaderBand)
        If String.IsNullOrEmpty(row.Detail.ToString.Trim) Then
            'table8.Rows.Remove(XrTableRow2)
            table8.Visible = False
            table8.HeightF = 0
            GroupHeader.HeightF = 23
        Else
            table8.Visible = True
            GroupHeader.HeightF = 49
            table8.HeightF = 20
        End If
    End Sub

#Region "Calculate PreviosBalance"

    Dim count As Integer = 0
    Dim balance As Decimal = 0
    Dim total As Decimal = 0

    Private Sub XrTableCell9_SummaryReset(sender As Object, e As EventArgs) Handles XrTableCell9.SummaryReset
        count = 0
        balance = 0
    End Sub

    Private Sub INDTcIncome_SummaryGetResult(sender As Object, e As SummaryGetResultEventArgs) Handles INDTcIncome.SummaryGetResult
        Dim row = GetCurrentRow()
        balance = If(count = 0, row.SaldoAnterior, balance)
        Dim incomeValue = e.CalculatedValues.ToEntityList(Of Decimal).Sum()
        balance = balance + incomeValue
        count = count + 1
        e.Result = Utils.GetMoneyWithISO4217(incomeValue, If(String.IsNullOrEmpty(row.CurrencyAbbreviation),
                                             IndigoSessionValues.CurrencyISO4217, row.CurrencyAbbreviation))
        e.Handled = True
    End Sub

    Private Sub INDTcExpense_SummaryGetResult(sender As Object, e As SummaryGetResultEventArgs) Handles INDTcExpense.SummaryGetResult
        Dim expenseValue = e.CalculatedValues.ToEntityList(Of Decimal).Sum()
        balance = balance - expenseValue
        Dim row = GetCurrentRow()
        e.Result = Utils.GetMoneyWithISO4217(expenseValue, If(String.IsNullOrEmpty(row.CurrencyAbbreviation),
                                             IndigoSessionValues.CurrencyISO4217, row.CurrencyAbbreviation))
        e.Handled = True
    End Sub

    Private Sub INDTcBalance_SummaryGetResult(sender As Object, e As SummaryGetResultEventArgs) Handles INDTcBalance.SummaryGetResult
        Dim row = GetCurrentRow()
        e.Result = Utils.GetMoneyWithISO4217(balance, If(String.IsNullOrEmpty(row.CurrencyAbbreviation),
                                             IndigoSessionValues.CurrencyISO4217, row.CurrencyAbbreviation))
        e.Handled = True
    End Sub

    Private Sub XrTableCell9_SummaryGetResult(sender As Object, e As SummaryGetResultEventArgs) Handles XrTableCell9.SummaryGetResult
        Dim row = GetCurrentRow()
        e.Result = Utils.GetMoneyWithISO4217(row.SaldoAnterior, If(String.IsNullOrEmpty(row.CurrencyAbbreviation),
                                             IndigoSessionValues.CurrencyISO4217, row.CurrencyAbbreviation))
        e.Handled = True
    End Sub

    Private Sub XrTableCell42_SummaryGetResult(sender As Object, e As SummaryGetResultEventArgs) Handles XrTableCell42.SummaryGetResult
        Dim row = GetCurrentRow()
        e.Result = Utils.GetMoneyWithISO4217(balance, If(String.IsNullOrEmpty(row.CurrencyAbbreviation),
                                             IndigoSessionValues.CurrencyISO4217, row.CurrencyAbbreviation))
        e.Handled = True
        total = total + balance
    End Sub

    Private Sub XrTableCell48_SummaryGetResult(sender As Object, e As SummaryGetResultEventArgs) Handles XrTableCell48.SummaryGetResult
        Dim row = GetCurrentRow()
        e.Result = Utils.GetMoneyWithISO4217(total, If(String.IsNullOrEmpty(row.CurrencyAbbreviation),
                                             IndigoSessionValues.CurrencyISO4217, row.CurrencyAbbreviation))
        e.Handled = True
    End Sub

    Private Sub XrTableCell41_SummaryGetResult(sender As Object, e As SummaryGetResultEventArgs) Handles XrTableCell41.SummaryGetResult
        Dim row = GetCurrentRow()
        e.Result = Utils.GetMoneyWithISO4217(e.CalculatedValues.ToEntityList(Of Decimal).Sum(), If(String.IsNullOrEmpty(row.CurrencyAbbreviation),
                                             IndigoSessionValues.CurrencyISO4217, row.CurrencyAbbreviation))
        e.Handled = True
    End Sub

    Private Sub XrTableCell40_SummaryGetResult(sender As Object, e As SummaryGetResultEventArgs) Handles XrTableCell40.SummaryGetResult
        Dim row = GetCurrentRow()
        e.Result = Utils.GetMoneyWithISO4217(e.CalculatedValues.ToEntityList(Of Decimal).Sum(), If(String.IsNullOrEmpty(row.CurrencyAbbreviation),
                                             IndigoSessionValues.CurrencyISO4217, row.CurrencyAbbreviation))
        e.Handled = True
    End Sub
#End Region

End Class