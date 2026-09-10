#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.CloudAgent
Imports Presentation.Base
Imports System.Globalization
Imports DevExpress.XtraReports.UI

#End Region

Public Class rptExtractsAccountsByPay
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Variable par obtener la tabla de Trazabilidad
    ''' </summary>
    Dim dtReport As DataTable

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PaymentsService.GetCollectionReportExtractsAccountsByPay(ParametrosReporte(0), ParametrosReporte(1), ParametrosReporte(2), ParametrosReporte(3), ParametrosReporte(4), ParametrosReporte(5))
    End Sub

    Public Async Function CargarDataSourceAsync() As Task
        Try
            Dim ds As DataSet = Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetListReportExtractAccountPayableAsync(ParametrosReporte(0), ParametrosReporte(1), ParametrosReporte(2), ParametrosReporte(3), ParametrosReporte(4), ParametrosReporte(5), ParametrosReporte(6), Me.IndigoSessionValues)
            If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                dtReport = ds.Tables("ReportExtractAccountPayable")
                Me.DataSource = dtReport
                Me.DataMember = "ReportExtractAccountPayable"
            Else
                Me.DataSource = Nothing
            End If
        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try
    End Function

    Public Function GetExceptionDetails(exception As Exception) As String
        Dim properties = exception.[GetType]().GetProperties()
        Dim fields = properties.[Select](Function([property]) New With { _
            Key .Name = [property].Name, _
            Key .Value = [property].GetValue(exception, Nothing) _
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

    Private Sub rptExtractsAccountsByPay_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        Me.INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        Me.INDLblNitCompany.Text = "Nit : " & IndigoSessionValues.IndigoCompanyNit
        Me.INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        INDLblDate.Text = "Informe comprendido entre el " & CDate(Me.ParametrosReporte(0)).ToString("dd De MMMM Del yyyy") & " " & CDate(Me.ParametrosReporte(1)).ToString("Al dd De MMMM Del yyyy")

        If ParametrosReporte(6) = 1 Then ' Tercero 
            INDHeaderMovement.Visible = False
            DetailMovement.Visible = False
            INDGroupHeaderAccount.Visible = False
            INDGroupFooterAccount.Visible = False
            INDGroupFooterDocument.Visible = False
        ElseIf ParametrosReporte(6) = 2 Then ' Cuenta
            INDHeaderMovement.Visible = False
            DetailMovement.Visible = False
            INDGroupHeaderThirdParty.Visible = False
            INDGroupFooterThirdParty.Visible = False
            INDGroupFooterDocument.Visible = False
        Else
            INDGroupFooterThirdParty.Visible = False
            INDGroupFooterAccount.Visible = False
        End If
    End Sub

    'Valor inicial
    Private Sub XrTableCell21_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell21.BeforePrint
        Dim dataRow As DataRowView = GetCurrentRow()
        Dim Currency = dataRow.Row("CurrencyAbbreviation")
        Dim _culture As CultureInfo = CultureInfo.CurrentCulture.Clone()
        Dim CurrencyAbbreviation As String = If(String.IsNullOrEmpty(Currency), IndigoSessionValues.CurrencyISO4217, Currency)
        _culture.NumberFormat = New CultureInfo(CurrencyAbbreviation.GetCultureId).NumberFormat
        XrTableCell21.Text = String.Format(_culture.NumberFormat.CurrencySymbol + "{0:n2}", dataRow.Row("BillValueInitial"))
    End Sub

    'Saldo actual
    Private Sub XrTableCell23_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell23.BeforePrint
        Dim dataRow As DataRowView = GetCurrentRow()
        Dim Currency = dataRow.Row("CurrencyAbbreviation")
        Dim _culture As CultureInfo = CultureInfo.CurrentCulture.Clone()
        Dim CurrencyAbbreviation As String = If(String.IsNullOrEmpty(Currency), IndigoSessionValues.CurrencyISO4217, Currency)
        _culture.NumberFormat = New CultureInfo(CurrencyAbbreviation.GetCultureId).NumberFormat
        XrTableCell23.Text = String.Format(_culture.NumberFormat.CurrencySymbol + "{0:n2}", dataRow.Row("BillCurrentBalance"))
    End Sub

    'Valor debito
    Private Sub XrTableCell27_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell27.BeforePrint
        Dim dataRow As DataRowView = GetCurrentRow()
        Dim Currency = dataRow.Row("CurrencyAbbreviation")
        Dim _culture As CultureInfo = CultureInfo.CurrentCulture.Clone()
        Dim CurrencyAbbreviation As String = If(String.IsNullOrEmpty(Currency), IndigoSessionValues.CurrencyISO4217, Currency)
        _culture.NumberFormat = New CultureInfo(CurrencyAbbreviation.GetCultureId).NumberFormat
        XrTableCell27.Text = String.Format(_culture.NumberFormat.CurrencySymbol + "{0:n2}", dataRow.Row("MovesDebit"))
    End Sub

    'Valor credito
    Private Sub XrTableCell28_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell28.BeforePrint
        Dim dataRow As DataRowView = GetCurrentRow()
        Dim Currency = dataRow.Row("CurrencyAbbreviation")
        Dim _culture As CultureInfo = CultureInfo.CurrentCulture.Clone()
        Dim CurrencyAbbreviation As String = If(String.IsNullOrEmpty(Currency), IndigoSessionValues.CurrencyISO4217, Currency)
        _culture.NumberFormat = New CultureInfo(CurrencyAbbreviation.GetCultureId).NumberFormat
        XrTableCell28.Text = String.Format(_culture.NumberFormat.CurrencySymbol + "{0:n2}", dataRow.Row("MovesCredit"))
    End Sub

    ' Suma Valor debito
    Private Sub XrTableCell29_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell29.BeforePrint
        Dim dataRow As DataRowView = GetCurrentRow()
        Dim Currency = dataRow.Row("CurrencyAbbreviation")
        Dim _culture As CultureInfo = CultureInfo.CurrentCulture.Clone()
        Dim CurrencyAbbreviation As String = If(String.IsNullOrEmpty(Currency), IndigoSessionValues.CurrencyISO4217, Currency)
        _culture.NumberFormat = New CultureInfo(CurrencyAbbreviation.GetCultureId).NumberFormat
        XrTableCell29.TextFormatString = _culture.NumberFormat.CurrencySymbol + "{0:n2}"
    End Sub
    ' Suma Valor credito
    Private Sub XrTableCell30_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell30.BeforePrint
        Dim dataRow As DataRowView = GetCurrentRow()
        Dim Currency = dataRow.Row("CurrencyAbbreviation")
        Dim _culture As CultureInfo = CultureInfo.CurrentCulture.Clone()
        Dim CurrencyAbbreviation As String = If(String.IsNullOrEmpty(Currency), IndigoSessionValues.CurrencyISO4217, Currency)
        _culture.NumberFormat = New CultureInfo(CurrencyAbbreviation.GetCultureId).NumberFormat
        XrTableCell30.TextFormatString = _culture.NumberFormat.CurrencySymbol + "{0:n2}"
    End Sub

    ' Suma Valor debito por cuenta
    Private Sub XrTableCell32_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell32.BeforePrint
        Dim dataRow As DataRowView = GetCurrentRow()
        Dim Currency = dataRow.Row("CurrencyAbbreviation")
        Dim _culture As CultureInfo = CultureInfo.CurrentCulture.Clone()
        Dim CurrencyAbbreviation As String = If(String.IsNullOrEmpty(Currency), IndigoSessionValues.CurrencyISO4217, Currency)
        _culture.NumberFormat = New CultureInfo(CurrencyAbbreviation.GetCultureId).NumberFormat
        XrTableCell32.TextFormatString = _culture.NumberFormat.CurrencySymbol + "{0:n2}"
    End Sub
    ' Suma valor credito por cuenta
    Private Sub XrTableCell33_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell33.BeforePrint
        Dim dataRow As DataRowView = GetCurrentRow()
        Dim Currency = dataRow.Row("CurrencyAbbreviation")
        Dim _culture As CultureInfo = CultureInfo.CurrentCulture.Clone()
        Dim CurrencyAbbreviation As String = If(String.IsNullOrEmpty(Currency), IndigoSessionValues.CurrencyISO4217, Currency)
        _culture.NumberFormat = New CultureInfo(CurrencyAbbreviation.GetCultureId).NumberFormat
        XrTableCell33.TextFormatString = _culture.NumberFormat.CurrencySymbol + "{0:n2}"
    End Sub

    ' Suma valor debito por tercero
    Private Sub XrTableCell35_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell35.BeforePrint
        Dim dataRow As DataRowView = GetCurrentRow()
        Dim Currency = dataRow.Row("CurrencyAbbreviation")
        Dim _culture As CultureInfo = CultureInfo.CurrentCulture.Clone()
        Dim CurrencyAbbreviation As String = If(String.IsNullOrEmpty(Currency), IndigoSessionValues.CurrencyISO4217, Currency)
        _culture.NumberFormat = New CultureInfo(CurrencyAbbreviation.GetCultureId).NumberFormat
        XrTableCell35.TextFormatString = _culture.NumberFormat.CurrencySymbol + "{0:n2}"
    End Sub
    ' Suma valor credito por tercero
    Private Sub XrTableCell36_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell36.BeforePrint
        Dim dataRow As DataRowView = GetCurrentRow()
        Dim Currency = dataRow.Row("CurrencyAbbreviation")
        Dim _culture As CultureInfo = CultureInfo.CurrentCulture.Clone()
        Dim CurrencyAbbreviation As String = If(String.IsNullOrEmpty(Currency), IndigoSessionValues.CurrencyISO4217, Currency)
        _culture.NumberFormat = New CultureInfo(CurrencyAbbreviation.GetCultureId).NumberFormat
        XrTableCell36.TextFormatString = _culture.NumberFormat.CurrencySymbol + "{0:n2}"
    End Sub
End Class