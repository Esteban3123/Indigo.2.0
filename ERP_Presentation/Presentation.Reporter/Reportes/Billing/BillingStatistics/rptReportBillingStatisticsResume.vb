#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.AccountingRepository
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Infrastructure.Data.Xpo.SecurityRepository
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports Domain.Entities
Imports DevExpress.XtraReports.UI
Imports System.Drawing.Printing
Imports System.Drawing
Imports DevExpress.XtraPrinting.Drawing
Imports Presentation.Base
Imports Presentation.Reporter
Imports DevExpress.XtraReports.Parameters
Imports Presentation.CloudAgent
Imports System.Globalization

#End Region

Public Class rptReportBillingStatisticsResume
    Implements IReport
    Implements IReportAsync

#Region "Variables"

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' diccionario para obtenes los rangos
    ''' </summary>
    Private criterias As Dictionary(Of String, String)

    ''' <summary>
    ''' diccionario para obtenes los filtros
    ''' </summary>
    Private filters As Dictionary(Of String, String)

    ''' <summary>
    ''' Obtiene la lista de los datos a imprimir
    ''' </summary>
    Private result As List(Of SP_ReportBillingStadistics_Result)

    ''' <summary>
    ''' obtiene la moneda a la cual se realiza el reporte si el campo moneda esta seleccionado
    ''' </summary>
    ''' <returns></returns>
    Public Property Currency As Currency

#End Region

#Region "IReport"

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Public Sub CargarDataSource() Implements IReport.CargarDataSource

    End Sub

    Public Async Function CargarDataSourceAsync() As Task Implements IReportAsync.CargarDataSourceAsync
        Try
            criterias = ParametrosReporte(0)
            filters = ParametrosReporte(1)

            Dim XmlCriterias = Utils.DictionaryToXML(criterias)
            Dim XmlFilters = Utils.DictionaryToXML(filters)

            result = Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.ReportBillingStadisticsAsync(XmlCriterias, XmlFilters, IndigoSessionValues)
            If result IsNot Nothing AndAlso result.Count > 0 Then
                DataSource = result
            Else
                DataSource = Nothing
            End If
        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try
    End Function

    ''' <summary>
    ''' Valida los datos del reporte
    ''' </summary>
    ''' <returns></returns>
    Public Async Function ValidateData() As Task(Of (IsValid As Boolean, Message As String))
        criterias = ParametrosReporte(0)
        filters = ParametrosReporte(1)

        Dim XmlCriterias = Utils.DictionaryToXML(criterias)
        Dim XmlFilters = Utils.DictionaryToXML(filters)

        Dim resultValidation = Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.ReportBillingStadisticsCountAsync(XmlCriterias, XmlFilters, IndigoSessionValues)

        Return (resultValidation.IsValid, resultValidation.Message)
    End Function


    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

#End Region

#Region "Methods"

    Public Function GetExceptionDetails(exception As Exception) As String
        Dim properties = exception.[GetType]().GetProperties()
        Dim fields = properties.[Select](Function([property]) New With {
            Key .Name = [property].Name,
            Key .Value = [property].GetValue(exception, Nothing)
        }).[Select](Function(x) [String].Format("{0} : {1}", x.Name, If(x.Value IsNot Nothing, x.Value.ToString(), [String].Empty)))
        Return [String].Join(vbLf, fields)
    End Function

#End Region

#Region "Handlers"

    ''' <summary>
    ''' Evento que se dispara al pintar el reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub rptAccountPayable_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        If filters("ThirdParty") = "1" Then 'Tercero
            XrTableCell27.Text = "Tercero"
        ElseIf filters("Entity") = "2" Then 'Entidad
            XrTableCell27.Text = "Entidad"
            GroupHeader1.Visible = False
            GroupHeader3.Visible = False
        ElseIf filters("CareGroup") = "3" Then 'Grupo Atencion
            XrTableCell27.Text = "Grupo Atención"
            GroupHeader3.Visible = False
        ElseIf filters("Users") = "4" Then 'Usuario
            XrTableCell27.Text = "Usuario"
        End If

        If criterias("ReportType") = 1 Then 'Resumido
            XrTable9.Visible = True
            XrTable11.Visible = True

            XrTable2.Visible = False
            XrTable5.Visible = False

            GroupHeader4.Visible = False

            XrTable10.BackColor = System.Drawing.Color.Transparent
            Detail.Visible = False
        Else 'Detallado
            XrTable9.Visible = False
            XrTable11.Visible = False

            XrTable2.Visible = True
            XrTable5.Visible = True

            GroupHeader4.Visible = True

            XrTable10.BackColor = System.Drawing.Color.Beige
            Detail.Visible = True
        End If

        INDPrmTypeReport.Value = criterias("ReportType")
        INDPrmGroupBy.Value = criterias("GroupBy")

        If Currency IsNot Nothing AndAlso Not String.IsNullOrEmpty(Currency.Abbreviation) Then
            Dim _culture As CultureInfo = CultureInfo.CurrentCulture.Clone()
            Dim CurrencyAbbreviation As String = Currency.Abbreviation
            _culture.NumberFormat = CurrencyAbbreviation.GetNumberFormat
            ApplyLocalization(_culture)
        End If

        Me.INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        Me.INDLblNitCompany.Text = "Nit : " & IndigoSessionValues.IndigoCompanyNit
        INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        INDLblDate.Text = "Informe comprendido entre " & Me.criterias("DateStart").ToString() & " " & criterias("DateEnd").ToString()

    End Sub

    Private Sub XrTableCell16_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell16.BeforePrint
        If Currency Is Nothing Then
            Dim row = GetCurrentRow()

            Dim Value = DirectCast(row, SP_ReportBillingStadistics_Result)?.TotalValue
            Dim CurrencyAbbreviation = DirectCast(row, SP_ReportBillingStadistics_Result)?.Abbreviation
            If Value > 0 AndAlso Not String.IsNullOrEmpty(CurrencyAbbreviation) Then
                XrTableCell16.Text = Utils.GetMoneyWithISO4217(Value, CurrencyAbbreviation)
            End If
        End If
    End Sub

    Private Sub XrTableCell17_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell17.BeforePrint
        If Currency Is Nothing Then
            Dim row = GetCurrentRow()

            Dim Value = DirectCast(row, SP_ReportBillingStadistics_Result)?.ValueCopay
            Dim CurrencyAbbreviation = DirectCast(row, SP_ReportBillingStadistics_Result)?.Abbreviation
            If Value > 0 AndAlso Not String.IsNullOrEmpty(CurrencyAbbreviation) Then
                XrTableCell17.Text = Utils.GetMoneyWithISO4217(Value, CurrencyAbbreviation)
            End If
        End If
    End Sub

    Private Sub XrTableCell18_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell18.BeforePrint
        If Currency Is Nothing Then
            Dim row = GetCurrentRow()

            Dim Value = DirectCast(row, SP_ReportBillingStadistics_Result)?.EntityValue
            Dim CurrencyAbbreviation = DirectCast(row, SP_ReportBillingStadistics_Result)?.Abbreviation
            If Value > 0 AndAlso Not String.IsNullOrEmpty(CurrencyAbbreviation) Then
                XrTableCell18.Text = Utils.GetMoneyWithISO4217(Value, CurrencyAbbreviation)
            End If
        End If
    End Sub

    Private Sub PageHeader_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles PageHeader.BeforePrint
        If Currency Is Nothing Then
            Dim row = GetCurrentRow()

            Dim CurrencyName = DirectCast(row, SP_ReportBillingStadistics_Result)?.CurrencyName

            Me.INDLblTitle.Text = "ESTADÍSTICO DE FACTURACIÓN - " + IIf(criterias("ReportType") = 1, "RESUMIDO ", "DETALLADO - ") + UCase(CurrencyName)
        Else
            Me.INDLblTitle.Text = "ESTADÍSTICO DE FACTURACIÓN - " + IIf(criterias("ReportType") = 1, "RESUMIDO ", "DETALLADO - ") + UCase(Currency.Name)
        End If
    End Sub

    Private Sub XrTableGroup_SummaryGetResult(sender As Object, e As SummaryGetResultEventArgs) Handles XrTableCell23.SummaryGetResult, XrTableCell24.SummaryGetResult, XrTableCell25.SummaryGetResult,
                                                                                                        XrTableCell42.SummaryGetResult, XrTableCell43.SummaryGetResult, XrTableCell44.SummaryGetResult,
                                                                                                        XrTableCell40.SummaryGetResult, XrTableCell41.SummaryGetResult, XrTableCell45.SummaryGetResult,
                                                                                                        XrTableCell46.SummaryGetResult, XrTableCell47.SummaryGetResult, XrTableCell48.SummaryGetResult,
                                                                                                        XrTableCell50.SummaryGetResult, XrTableCell51.SummaryGetResult, XrTableCell52.SummaryGetResult
        Dim row = GetCurrentRow()

        Dim Abbreviation = DirectCast(row, SP_ReportBillingStadistics_Result)?.Abbreviation
        e.Result = Utils.GetMoneyWithISO4217(e.CalculatedValues.ToEntityList(Of Decimal).Sum(), If(String.IsNullOrEmpty(Abbreviation),
                                             IndigoSessionValues.CurrencyISO4217, Abbreviation))
        e.Handled = True

    End Sub

#End Region

End Class