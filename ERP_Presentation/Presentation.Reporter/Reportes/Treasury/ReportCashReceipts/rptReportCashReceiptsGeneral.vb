#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.TreasuryRepository
Imports Infrastructure.Data.Xpo
Imports DevExpress.XtraReports.UI
#End Region

Public Class rptReportCashReceiptsGeneral
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Dim filtroConsulta As String = "DocumentDate >= #" & Format(ParametrosReporte(0), "yyyy-MM-dd HH:mm:ss") & "# AND DocumentDate <= #" & Format(ParametrosReporte(1), "yyyy-MM-dd HH:mm:ss") & "#"

        Dim status = ParametrosReporte(2)
        Dim cashRegisters As String = ParametrosReporte(3)
        Dim entityBankAccounts As String = ParametrosReporte(4)
        Dim thirdParties As String = ParametrosReporte(5)
        Dim documents As String = ParametrosReporte(6)
        Dim users As String = ParametrosReporte(7)

        If status IsNot Nothing Then
            filtroConsulta &= String.Format(" AND Status IN ({0})", status)
        End If

        If Not String.IsNullOrEmpty(cashRegisters) AndAlso Not String.IsNullOrEmpty(entityBankAccounts) Then
            filtroConsulta &= String.Format(" AND (IdCashRegister.Id IN ({0}) OR IdBankAccount.Id IN ({1}))", cashRegisters, entityBankAccounts)
        ElseIf Not String.IsNullOrEmpty(cashRegisters) Then
            filtroConsulta &= String.Format(" AND IdCashRegister.Id IN ({0})", cashRegisters)
        ElseIf Not String.IsNullOrEmpty(entityBankAccounts) Then
            filtroConsulta &= String.Format(" AND IdBankAccount.Id IN ({0})", entityBankAccounts)
        End If

        If Not String.IsNullOrEmpty(thirdParties) Then
            filtroConsulta &= String.Format(" AND IdThirdParty.Id IN ({0})", thirdParties)
        End If

        If Not String.IsNullOrEmpty(documents) Then
            filtroConsulta &= String.Format(" AND Id IN ({0})", documents)
        End If

        If Not String.IsNullOrEmpty(users) Then
            filtroConsulta &= String.Format(" AND CreationUser IN ({0})", String.Join(",", users.Split(",").Select(Function(x) String.Format("'{0}'", x)).ToList()))
        End If

        Dim listReport As List(Of TreasuryCashReceiptsXpo) = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of TreasuryCashReceiptsXpo)(Nothing, filtroConsulta)
        For Each item In listReport
            If item.IdCashRegister Is Nothing Then
                item.GroupByCashEntityBank = item.IdBankAccount.Number & " - " & item.IdBankAccount.IdBank.Name & " - " & item.CurrencyAbbreviation
            Else
                item.GroupByCashEntityBank = item.IdCashRegister.CodeName & " - " & item.CurrencyAbbreviation
            End If
        Next

        Me.DataSource = listReport
        If listReport.Count > 0 Then
            Me.XrSubreport1.ReportSource.DataSource = listReport
        End If
    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte


    Private Sub rptReportCashReceiptsGeneral_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        Me.ParameterGroupBy.Value = ParametrosReporte(8)
        INDLblDate.Text = "Informe comprendido entre " & CDate(Me.ParametrosReporte(0)).ToString("dd De MMMM Del yyyy HH:mm:ss") & " " & CDate(Me.ParametrosReporte(1)).ToString("A dd De MMMM Del yyyy HH:mm:ss")
    End Sub

    Private Sub XrTableCellTotalCheck_SummaryGetResult(sender As Object, e As SummaryGetResultEventArgs) Handles XrTableCellTotalCheck.SummaryGetResult, XrTableCellTotalCash.SummaryGetResult, XrTableCellTotalBank.SummaryGetResult, XrTableCellTotalConsigment.SummaryGetResult, XrTableCell24.SummaryGetResult, XrTableCell27.SummaryGetResult, XrTableCell23.SummaryGetResult

        Dim row = GetCurrentRow()
        Dim total As Decimal = e.CalculatedValues.Cast(Of Object)().Where(Function(x) x IsNot Nothing AndAlso Not IsDBNull(x)).Select(Function(x) Convert.ToDecimal(x)).Sum()
        e.Result = Utils.GetMoneyWithISO4217(total, If(String.IsNullOrEmpty(row?.CurrencyAbbreviation),
                                             IndigoSessionValues.CurrencyISO4217, row?.CurrencyAbbreviation))
        e.Handled = True
    End Sub

    Private Sub XrTableCell13_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell13.BeforePrint
        Dim row = GetCurrentRow()
        XrTableCell13.Text = Utils.GetMoneyWithISO4217(row.Value, If(String.IsNullOrEmpty(row?.CurrencyAbbreviation),
                                             IndigoSessionValues.CurrencyISO4217, row?.CurrencyAbbreviation))
    End Sub

End Class