#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.PortfolioRepository
Imports Infrastructure.Data.Xpo
Imports DevExpress.XtraReports.UI
Imports DevExpress.Xpo
Imports System.Globalization
#End Region

Public Class rptExtractResumeThirdParty
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Dim filtroConsulta As String = ""
        Dim nesting As String = ""

        'Valida si no hay datos en los filtros anticipos o cuentas por cobrar y realiza la validación de fecha
        If ParametrosReporte(6) Is Nothing And ParametrosReporte(7) Is Nothing And ParametrosReporte(12) Is Nothing And ParametrosReporte(13) Is Nothing Then

            filtroConsulta &= "MovesDate >= #" & Format(ParametrosReporte(0), "yyyy-MM-dd HH:mm:ss") & "# AND MovesDate <= #" & Format(ParametrosReporte(1), "yyyy-MM-dd HH:mm:ss") & "#"
            nesting = " AND "
        End If

        'Se filtra por Clientes
        If ParametrosReporte(4) IsNot Nothing And ParametrosReporte(5) IsNot Nothing Then
            filtroConsulta &= nesting & "ThirdPartyNit >= '" & ParametrosReporte(4) & "' AND ThirdPartyNit <= '" & ParametrosReporte(5) & "'"
            nesting = " AND "
        End If

        'Se filtra por Cuentas por Cobrar (Facturas)
        If ParametrosReporte(6) IsNot Nothing And ParametrosReporte(7) IsNot Nothing Then
            filtroConsulta &= nesting & "DocumentNumber >= '" & ParametrosReporte(6) & "' AND DocumentNumber <= '" & ParametrosReporte(7) & "'"
            nesting = " AND "
        End If

        'Se filtra por Grupos de Atención
        If ParametrosReporte(8) IsNot Nothing And ParametrosReporte(9) IsNot Nothing Then
            filtroConsulta &= nesting & "CareGroupCode >= '" & ParametrosReporte(8) & "' AND CareGroupCode <= '" & ParametrosReporte(9) & "'"
            nesting = " AND "
        End If

        'Se filtra por Cuenta Contable
        If ParametrosReporte(10) IsNot Nothing And ParametrosReporte(11) IsNot Nothing Then
            filtroConsulta &= nesting & "AccountNumber >= '" & ParametrosReporte(10) & "' AND AccountNumber <= '" & ParametrosReporte(11) & "'"
            nesting = " AND "
        End If

        If ParametrosReporte(2) <> 4 Then
            filtroConsulta &= nesting & "Status = " & ParametrosReporte(2)
            nesting = " AND "
        End If

        If ParametrosReporte(12) IsNot Nothing And ParametrosReporte(13) IsNot Nothing Then
            filtroConsulta &= nesting & "DocumentNumber >= '" & ParametrosReporte(12) & "' AND DocumentNumber <= '" & ParametrosReporte(13) & "'"
            nesting = " AND "
        End If

        If ParametrosReporte(14) IsNot Nothing Then
            filtroConsulta &= nesting & "AccountReceivableType in(" & ParametrosReporte(14).ToString() & ")"
            nesting = " AND "
        End If

        If ParametrosReporte(15) IsNot Nothing AndAlso ParametrosReporte(15) <> "Todos" Then
            filtroConsulta &= nesting & "TypeDocument = '" & ParametrosReporte(15) & "'"
            nesting = " AND "
        End If

        Dim listReport As XPCollection(Of VReportExtractAccountReceivableXpo) = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PortfolioService.GetAllVReportExtractAccountReceivableXpo(filtroConsulta)
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

    Private Sub rptExtractResumeThirdParty_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        Me.INDLBlSubtitle.Text = "Informe comprendido entre " & CDate(ParametrosReporte(0)).ToString("dd De MMMM Del yyyy") & " " & CDate(ParametrosReporte(1)).ToString("A dd De MMMM Del yyyy")
        INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
    End Sub

    Private Sub XrTableCell2_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles SumMovesDebitCellTP.BeforePrint
        Dim row = GetCurrentRow()
        Dim _culture As CultureInfo = CultureInfo.CurrentCulture.Clone()
        Dim CurrencyAbbreviation As String = row?.CurrencyName
        If String.IsNullOrEmpty(CurrencyAbbreviation) Then
            CurrencyAbbreviation = IndigoSessionValues.CurrencyISO4217
        End If
        _culture.NumberFormat = New CultureInfo(CurrencyAbbreviation.GetCultureId).NumberFormat
        SumMovesDebitCellTP.TextFormatString = _culture.NumberFormat.CurrencySymbol + "{0:n2}"
    End Sub

    Private Sub XrTableCell3_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles SumMovesCreditTPCell.BeforePrint
        Dim row = GetCurrentRow()
        Dim _culture As CultureInfo = CultureInfo.CurrentCulture.Clone()
        Dim CurrencyAbbreviation As String = row?.CurrencyName

        If String.IsNullOrEmpty(CurrencyAbbreviation) Then
            CurrencyAbbreviation = IndigoSessionValues.CurrencyISO4217
        End If

        _culture.NumberFormat = New CultureInfo(CurrencyAbbreviation.GetCultureId).NumberFormat
        SumMovesCreditTPCell.TextFormatString = _culture.NumberFormat.CurrencySymbol + "{0:n2}"
    End Sub
End Class