#Region "Imports"

Imports DevExpress.XtraReports.UI
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.CloudAgent
Imports System.Globalization
#End Region

Public Class rptResultStatusComparativeGroupSubReports
    Implements IReport

#Region "Properties"

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Dim criterias As Dictionary(Of String, String)

    ''' <summary>
    ''' Variable par obtener la tabla de Trazabilidad
    ''' </summary>
    Dim dtReportResulStatus As DataTable

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public TotalValueInitial1 As Decimal = 0
    Public TotalValueInitial2 As Decimal = 0
    Public TotalValueInitial3 As Decimal = 0

    Public TotalValueFinal1 As Decimal = 0
    Public TotalValueFinal2 As Decimal = 0
    Public TotalValueFinal3 As Decimal = 0

    Public AbsoluteValue1 As Decimal = 0
    Public AbsoluteValue2 As Decimal = 0
    Public AbsoluteValue3 As Decimal = 0

    Public PercentageAbsolute1 As Decimal = 0
    Public PercentageAbsolute2 As Decimal = 0
    Public PercentageAbsolute3 As Decimal = 0

    ''' <summary>
    ''' Variable par obtener el nombre de la moneda del libro
    ''' </summary>
    Dim currencyName As String

    ''' <summary>
    ''' Variable par obtener la abreviacion de la moneda del libro
    ''' </summary>
    Dim currencyAbbreviation As String

#End Region

#Region "Load Data"

    Public Sub CargarDataSource() Implements IReport.CargarDataSource

    End Sub

    ''' <summary>
    ''' se ejecuta para cargar los datasource de los subreport
    ''' </summary>
    ''' <param name="Natures">1 = debito, 2 = credito</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function FillListResultStatus(ByVal Natures As String) As Task(Of DataTable)
        Try
            criterias = ParametrosReporte(0)
            criterias.Remove("Natures")
            criterias.Add("Natures", Natures)

            Dim ds As DataSet = Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetReportResulStatusComparativeAsync(criterias, IndigoSessionValues)
            If ds IsNot Nothing Then
                dtReportResulStatus = ds.Tables("ReportResulStatusComparative")
                currencyName = dtReportResulStatus.Rows(0).Field(Of String)("LegalBookCurrency")
                currencyAbbreviation = dtReportResulStatus.Rows(0).Field(Of String)("LegalBookCurrencyAbbreviation")
            Else
                dtReportResulStatus = Nothing
            End If
            Return dtReportResulStatus
        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
            Return dtReportResulStatus
        End Try
    End Function

#End Region

#Region "Methods"

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

    Private Sub rptResultStatus_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        Dim initialRangefechaIni As Date = New Date(Me.criterias("InitialRangeYear"), Me.criterias("InitialRangeMonthStart"), 1)
        Dim initialRangefechafin As Date = New Date(Me.criterias("InitialRangeYear"), Me.criterias("InitialRangeMonthEnd"), 1)
        Dim finalRangefechaIni As Date = New Date(Me.criterias("FinalRangeYear"), Me.criterias("FinalRangeMonthStart"), 1)
        Dim finalRangefechafin As Date = New Date(Me.criterias("FinalRangeYear"), Me.criterias("FinalRangeMonthEnd"), 1)

        Dim INDDateStart As String = initialRangefechaIni.ToString("MMMM Del yyyy") & " a " & initialRangefechafin.ToString("MMMM Del yyyy")
        Dim INDDateEnd As String = finalRangefechaIni.ToString("MMMM Del yyyy") & " a " & finalRangefechafin.ToString("MMMM Del yyyy")

        'Cargar los valores del titulo
        Me.INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        Me.INDLblNitCompany.Text = "Nit : " & IndigoSessionValues.IndigoCompanyNit
        Me.INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        INDLblDate.Text = "Informe comparativo entre " & INDDateStart & " y " & INDDateEnd
        INDLblTitle.Text = Me.criterias("Title")
        INDCllPeriodInitial.Text = INDDateStart
        INDCllPeriodFinal.Text = INDDateEnd
        INDLblBook.Text = Me.criterias("LegalBookName")
        Dim INDGroupSubAuxiliar As GroupHeaderBand = INDSbrComparative.ReportSource.FindControl("INDGhSubAuxiliar", True)

        Me.INDcellValueInitial1.Text = Utils.GetMoneyWithISO4217(Me.TotalValueInitial1, currencyAbbreviation)
        Me.INDcellValueInitial2.Text = Utils.GetMoneyWithISO4217(Me.TotalValueInitial2, currencyAbbreviation)
        Me.INDcellValueInitial3.Text = Utils.GetMoneyWithISO4217(Me.TotalValueInitial3, currencyAbbreviation)

        Me.INDcellValueFinal1.Text = Utils.GetMoneyWithISO4217(Me.TotalValueFinal1, currencyAbbreviation)
        Me.INDcellValueFinal2.Text = Utils.GetMoneyWithISO4217(Me.TotalValueFinal2, currencyAbbreviation)
        Me.INDcellValueFinal3.Text = Utils.GetMoneyWithISO4217(Me.TotalValueFinal3, currencyAbbreviation)

        Me.INDcellAbsoluteValue1.Text = Utils.GetMoneyWithISO4217(Me.AbsoluteValue1, currencyAbbreviation)
        Me.INDcellAbsoluteValue2.Text = Utils.GetMoneyWithISO4217(Me.AbsoluteValue2, currencyAbbreviation)
        Me.INDcellAbsoluteValue3.Text = Utils.GetMoneyWithISO4217(Me.AbsoluteValue3, currencyAbbreviation)

        Me.INDcellPercentage1.Text = FormatPercent(Me.PercentageAbsolute1)
        Me.INDcellPercentage2.Text = FormatPercent(Me.PercentageAbsolute2)
        Me.INDcellPercentage3.Text = FormatPercent(Me.PercentageAbsolute3)

        If Me.criterias("IncludedAccountNumber") = False Then
            Dim xrTableRow As XRTableRow = XrTable1.Rows(0)
            If xrTableRow.Cells(XrTableCell4.Name) IsNot Nothing Then
                Dim control = Me.FindControl(XrTableCell4.Name, True)
                xrTableRow.Cells.Remove(control)
            End If
        End If

        If currencyAbbreviation IsNot Nothing Then
            Dim _culture As CultureInfo = CultureInfo.CurrentCulture.Clone()
            _culture.NumberFormat = currencyAbbreviation.GetNumberFormat
            ApplyLocalization(_culture)
        End If
        Me.CurrencyLabel.Text = "Moneda: " + currencyName
    End Sub

#End Region

End Class