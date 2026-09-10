#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.TreasuryRepository
Imports DevExpress.XtraReports.Parameters
Imports DevExpress.XtraReports.UI

#End Region

Public Class rptDispersionFunds
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Private ValueTotal As Integer

    Private INDUser As Object

    Private officialCurrencyAbbreviation = SessionValues.Instance.CurrencyISO4217

    Private List As List(Of TreasurySchedulePaymentDetailXpo)

    Public Sub CargarDataSource() Implements IReport.CargarDataSource

        Dim filtroConsulta As String = "SchedulePaymentId.Id = " & ParametrosReporte(0)
        List = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of TreasurySchedulePaymentDetailXpo)(Nothing, filtroConsulta)
        Dim INDCodeUser = CType(List(0), TreasurySchedulePaymentDetailXpo).SchedulePaymentId.CreationUser.Trim()
        INDUser = XpoServiceEx.Instance(IndigoSessionValues.SecurityContainer).TreasuryService.GetCollection(Of Infrastructure.Data.Xpo.SecurityRepository.UserXpo)(Nothing, "UserCode = '" & INDCodeUser & "'")
        Me.DataSource = List
    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptDispersionFunds_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        If Me.Parameters.Count > 0 And Me.Parameters(0).Value > 0 Then
            Dim ParametrosFilter As ParameterCollection = Me.Parameters
            ParametrosReporte = New Object() {ParametrosFilter("INDSubIdDispersionFunds").Value}
            CargarDataSource()
        End If

        Me.INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        Me.INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        Me.INDLblNitCompany.Text = "Nit : " & IndigoSessionValues.IndigoCompanyNit
        If INDUser.count() > 0 Then
            INDLblCreationUser.Text = INDUser(0).CodeName
        End If

    End Sub

    ''' <summary>
    ''' evento que establece el valor de los campos con su respectiva moneda
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub GroupHeader1_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles GroupHeader1.BeforePrint
        Dim currentRow = TryCast(GetCurrentRow(), TreasurySchedulePaymentDetailXpo)
        Dim currencyAbbreviation = If(String.IsNullOrEmpty(currentRow?.VoucherTransactionId?.CurrencyAbbreviation),
                                        Me.officialCurrencyAbbreviation, currentRow?.VoucherTransactionId?.CurrencyAbbreviation)

        If currentRow?.VoucherTransactionId IsNot Nothing Then

            XrTableCell13.Text = Utils.GetMoneyWithISO4217(currentRow?.VoucherTransactionId?.TaxByMilValue, currencyAbbreviation)
            XrTableCell2.Text = Utils.GetMoneyWithISO4217(currentRow?.VoucherTransactionId?.Value, currencyAbbreviation)

        End If
    End Sub

    ''' <summary>
    ''' evento para imprimir el totalizado por moneda
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ReportFooter_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ReportFooter.BeforePrint
        Dim GroupByCurrency = (From x In List
                               Group By x.CurrencyAbbreviation Into Group
                               Select CurrencyAbbreviation, SumCurrency = Group.Sum(Function(f) f.AmountPaid))
        Dim row = 0
        For Each ObjItem In GroupByCurrency
            If row = 0 Then
                XrTableCell3.Text = $"Valor Total {ObjItem.CurrencyAbbreviation}"
                XrTableCell5.Text = Utils.GetMoneyWithISO4217(ObjItem.SumCurrency, ObjItem.CurrencyAbbreviation)
                row += 1
                Continue For
            End If

            XrTable1.InsertRowBelow(XrTable1.Rows.LastRow)
            Dim irow = XrTable1.Rows.LastRow.Index
            For Each Column As XRTableCell In XrTableRow1
                Dim cell = XrTable1.Rows(irow).Cells.Item(Column.Index)
                Select Case Column.Name
                    Case NameOf(XrTableCell3)
                        cell.Text = $"Valor Total {ObjItem.CurrencyAbbreviation}"
                    Case NameOf(XrTableCell5)
                        cell.Text = Utils.GetMoneyWithISO4217(ObjItem.SumCurrency, ObjItem.CurrencyAbbreviation)
                End Select
            Next
        Next
    End Sub
End Class