#Region "Librerias Importadas"
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.FixedAssetRepository
Imports Domain.Entities
Imports Presentation.CloudAgent

#End Region

Public Class rptSubaccountFixedAssetSummarized
    Implements IReport
    Implements IReportAsync

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Variable par obtener la tabla de Trazabilidad
    ''' </summary>
    Dim dtReportSubAccount As DataTable

    ''' <summary>
    ''' Diccionario para contar la cantidad de elementos agrupados
    ''' </summary>
    ''' <remarks></remarks>
    Dim dictionarySubAccount As New Dictionary(Of String, String)

    ''' <summary>
    ''' Abreciación de la moneda
    ''' </summary>
    Private currencyAbbreviation As String

    ''' <summary>
    ''' Variable para darle formato de decimales a los valores del reporte
    ''' </summary>
    Private decimalFormat As String

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            GetCurrencyAbbreviationByBook(ParametrosReporte(6))
            Dim ds As DataSet = IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetListReportSubAccount(ParametrosReporte(0), ParametrosReporte(1), ParametrosReporte(2), ParametrosReporte(3), ParametrosReporte(4), ParametrosReporte(5), ParametrosReporte(6), Me.IndigoSessionValues)
            If ds.Tables(0).Rows.Count > 0 Then
                dtReportSubAccount = ds.Tables("ReportSubAccount")
                Me.DataSource = dtReportSubAccount
                Me.DataMember = "ReportSubAccount"

                Dim a As Integer = 0

                'Para contar la cantidad de regitros agrupados
                For Each row As DataRow In dtReportSubAccount.Rows
                    'Validamos el contenido del diccionario para conocer si la cuenta contable ya existe en el diccionario
                    If Not dictionarySubAccount.ContainsKey(row(2)) Then
                        a = a + 1
                        dictionarySubAccount.Add(row(2), row(2))
                    End If
                Next
                XrTableCell10.Text = a
            Else
                Me.DataSource = Nothing
            End If
        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try
    End Sub

    ''' <summary>
    ''' Metodo Asyncrono que llama el metodo principal
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CargarDataSourceAsync() As Task Implements IReportAsync.CargarDataSourceAsync
        Return Task.Factory.StartNew(AddressOf CargarDataSource)
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

    Private Sub rptSubaccountFixedAsset_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        Me.INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
    End Sub

    ''' <summary>
    ''' Método para obtener la abreviación de la moneda según el libro seleccionado
    ''' </summary>
    ''' <param name="bookId"></param>
    Private Sub GetCurrencyAbbreviationByBook(bookId)
        Dim selectedBook = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).AccountingService.GetBookById(bookId)
        Dim roundingType = selectedBook(0).CommonCurrency.RoundingType
        currencyAbbreviation = If(String.IsNullOrEmpty(selectedBook(0).CommonCurrency.Abbreviation), IndigoSessionValues.CurrencyISO4217, selectedBook(0).CommonCurrency.Abbreviation)

        If roundingType IsNot Nothing Then
            FormatValueWithDecimals(roundingType)
        End If
    End Sub

    ''' <summary>
    ''' Método para formatear los valores del reporte según el tipo de redondeo parametrizado a la moneda
    ''' </summary>
    ''' <param name="roundingType"></param>
    Private Sub FormatValueWithDecimals(roundingType As Integer)
        Select Case roundingType
            Case 1
                decimalFormat = 2
            Case 2
                decimalFormat = 1
            Case >= 3
                decimalFormat = 0
        End Select
    End Sub

    ''' <summary>
    ''' Eventos que controlan la visualización de los valores del reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
#Region "Cambio Símbolo Moneda"
    Private Sub XrTableCell12_SummaryGetResult(sender As Object, e As DevExpress.XtraReports.UI.SummaryGetResultEventArgs) Handles XrTableCell12.SummaryGetResult
        e.Result = Utils.GetMoneyWithISO4217(e.CalculatedValues.ToEntityList(Of Decimal).Sum(), currencyAbbreviation, decimalFormat)
        e.Handled = True
    End Sub

    Private Sub XrTableCell13_SummaryGetResult(sender As Object, e As DevExpress.XtraReports.UI.SummaryGetResultEventArgs) Handles XrTableCell13.SummaryGetResult
        e.Result = Utils.GetMoneyWithISO4217(e.CalculatedValues.ToEntityList(Of Decimal).Sum(), currencyAbbreviation, decimalFormat)
        e.Handled = True
    End Sub

    Private Sub XrTableCell14_SummaryGetResult(sender As Object, e As DevExpress.XtraReports.UI.SummaryGetResultEventArgs) Handles XrTableCell14.SummaryGetResult
        e.Result = Utils.GetMoneyWithISO4217(e.CalculatedValues.ToEntityList(Of Decimal).Sum(), currencyAbbreviation, decimalFormat)
        e.Handled = True
    End Sub

    Private Sub XrTableCell21_SummaryGetResult(sender As Object, e As DevExpress.XtraReports.UI.SummaryGetResultEventArgs) Handles XrTableCell21.SummaryGetResult
        e.Result = Utils.GetMoneyWithISO4217(e.CalculatedValues.ToEntityList(Of Decimal).Sum(), currencyAbbreviation, decimalFormat)
        e.Handled = True
    End Sub

    Private Sub XrTableCell23_SummaryGetResult(sender As Object, e As DevExpress.XtraReports.UI.SummaryGetResultEventArgs) Handles XrTableCell23.SummaryGetResult
        e.Result = Utils.GetMoneyWithISO4217(e.CalculatedValues.ToEntityList(Of Decimal).Sum(), currencyAbbreviation, decimalFormat)
        e.Handled = True
    End Sub

    Private Sub XrTableCell9_SummaryGetResult(sender As Object, e As DevExpress.XtraReports.UI.SummaryGetResultEventArgs) Handles XrTableCell9.SummaryGetResult
        e.Result = Utils.GetMoneyWithISO4217(e.CalculatedValues.ToEntityList(Of Decimal).Sum(), currencyAbbreviation, decimalFormat)
        e.Handled = True
    End Sub

    Private Sub XrTableCell8_SummaryGetResult(sender As Object, e As DevExpress.XtraReports.UI.SummaryGetResultEventArgs) Handles XrTableCell8.SummaryGetResult
        e.Result = Utils.GetMoneyWithISO4217(e.CalculatedValues.ToEntityList(Of Decimal).Sum(), currencyAbbreviation, decimalFormat)
        e.Handled = True
    End Sub

    Private Sub XrTableCell7_SummaryGetResult(sender As Object, e As DevExpress.XtraReports.UI.SummaryGetResultEventArgs) Handles XrTableCell7.SummaryGetResult
        e.Result = Utils.GetMoneyWithISO4217(e.CalculatedValues.ToEntityList(Of Decimal).Sum(), currencyAbbreviation, decimalFormat)
        e.Handled = True
    End Sub

    Private Sub XrTableCell70_SummaryGetResult(sender As Object, e As DevExpress.XtraReports.UI.SummaryGetResultEventArgs) Handles XrTableCell70.SummaryGetResult
        e.Result = Utils.GetMoneyWithISO4217(e.CalculatedValues.ToEntityList(Of Decimal).Sum(), currencyAbbreviation, decimalFormat)
        e.Handled = True
    End Sub

    Private Sub XrTableCell73_SummaryGetResult(sender As Object, e As DevExpress.XtraReports.UI.SummaryGetResultEventArgs) Handles XrTableCell73.SummaryGetResult
        e.Result = Utils.GetMoneyWithISO4217(e.CalculatedValues.ToEntityList(Of Decimal).Sum(), currencyAbbreviation, decimalFormat)
        e.Handled = True
    End Sub
#End Region
End Class