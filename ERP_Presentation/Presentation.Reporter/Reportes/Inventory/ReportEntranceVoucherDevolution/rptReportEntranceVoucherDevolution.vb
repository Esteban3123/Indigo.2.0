#Region "Imports"

Imports DevExpress.Xpo
Imports DevExpress.XtraReports.UI
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Presentation.Base

#End Region

Public Class rptReportEntranceVoucherDevolution
    Implements IReport
    Implements IReportAsync

#Region "Properties"

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

#End Region

#Region "Load Data"

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).InventoryService.LoadDataSourceReportEntranceVoucherDevolution(ParametrosReporte(0), ParametrosReporte(1), ParametrosReporte(2), ParametrosReporte(4), ParametrosReporte(5))
        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try
    End Sub

    Public Function CargarDataSourceAsync() As Task Implements IReportAsync.CargarDataSourceAsync
        Return Task.Factory.StartNew(AddressOf CargarDataSource)
    End Function

#End Region

#Region "Methods"

    Private Sub rptTraceabilityInvoice_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        Me.INDLblNameCompany.Text = IndigoSessionValues.IndigoCompanyName
        Me.INDLblNitCompany.Text = "Nit : " & IndigoSessionValues.IndigoCompanyNit
        Me.INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName

        Me.INDPrmGroupBy.Value = ParametrosReporte(3)
        If ParametrosReporte(0) IsNot Nothing And ParametrosReporte(1) IsNot Nothing Then
            Me.INDLblSubTitle.Text = "Informe comprendido entre " & CDate(Me.ParametrosReporte(0)).ToString("dd De MMMM Del yyyy") & " " & CDate(Me.ParametrosReporte(1)).ToString("A dd De MMMM Del yyyy")
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

    Private Sub XrTableCell16_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell16.BeforePrint
        Dim row = GetCurrentRow()
        XrTableCell16.Text = Utils.GetMoneyWithISO4217(XrTableCell16.Text, If(String.IsNullOrEmpty(row.EntranceVoucherId.CurrencyAbbreviation),
                                             IndigoSessionValues.CurrencyISO4217, row.EntranceVoucherId.CurrencyAbbreviation))
    End Sub

    Private Sub XrTableCell17_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell17.BeforePrint
        Dim row = GetCurrentRow()
        XrTableCell17.Text = Utils.GetMoneyWithISO4217(XrTableCell17.Text, If(String.IsNullOrEmpty(row.EntranceVoucherId.CurrencyAbbreviation),
                                             IndigoSessionValues.CurrencyISO4217, row.EntranceVoucherId.CurrencyAbbreviation))
    End Sub

    Private Sub XrTableCell18_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell18.BeforePrint
        Dim row = GetCurrentRow()
        XrTableCell18.Text = Utils.GetMoneyWithISO4217(XrTableCell18.Text, If(String.IsNullOrEmpty(row.EntranceVoucherId.CurrencyAbbreviation),
                                             IndigoSessionValues.CurrencyISO4217, row.EntranceVoucherId.CurrencyAbbreviation))
    End Sub

    Private Sub XrTableCell30_SummaryGetResult(sender As Object, e As DevExpress.XtraReports.UI.SummaryGetResultEventArgs) Handles XrTableCell30.SummaryGetResult, XrTableCell31.SummaryGetResult, XrTableCell32.SummaryGetResult
        Dim row = GetCurrentRow()
        e.Result = Utils.GetMoneyWithISO4217(e.CalculatedValues.ToEntityList(Of Decimal).Sum(), If(String.IsNullOrEmpty(row?.EntranceVoucherId.CurrencyAbbreviation),
                                             IndigoSessionValues.CurrencyISO4217, row?.EntranceVoucherId.CurrencyAbbreviation))
        e.Handled = True
    End Sub

    Private Sub ReportFooter_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ReportFooter.BeforePrint
        Dim List As XPCollection(Of InventoryEntranceVouhcerDevolutionReportXpo) = Me.DataSource
        Dim GroupByCurrency = (From x In List
                               Group By x.EntranceVoucherId.CurrencyAbbreviation Into Group
                               Select CurrencyAbbreviation, SumValue = Group.Sum(Function(f) f.EntranceVoucherId.Value),
                                   SumIva = Group.Sum(Function(f) f.EntranceVoucherId.ValueTax), SumTotal = Group.Sum(Function(f) f.EntranceVoucherId.TotalValue))
        Dim row = 0
        For Each ObjItem In GroupByCurrency
            INDTblFooterReportEntrance.InsertRowBelow(INDTblFooterReportEntrance.Rows.LastRow)
            Dim irow = INDTblFooterReportEntrance.Rows.LastRow.Index
            For Each Column As XRTableCell In XrTableRow11
                Dim cell = INDTblFooterReportEntrance.Rows(irow).Cells.Item(Column.Index)
                Select Case Column.Name
                    Case NameOf(XrTableCell40)
                        cell.Text = $"Total - {ObjItem.CurrencyAbbreviation}"
                    Case NameOf(XrTableCell41)
                        cell.Text = Utils.GetMoneyWithISO4217(ObjItem.SumValue, ObjItem.CurrencyAbbreviation)
                    Case NameOf(XrTableCell42)
                        cell.Text = Utils.GetMoneyWithISO4217(ObjItem.SumIva, ObjItem.CurrencyAbbreviation)
                    Case NameOf(XrTableCell43)
                        cell.Text = Utils.GetMoneyWithISO4217(ObjItem.SumTotal, ObjItem.CurrencyAbbreviation)
                End Select
            Next
        Next
    End Sub

#End Region

End Class