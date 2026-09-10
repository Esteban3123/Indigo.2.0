#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Presentation.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.AccountingRepository
Imports System.Globalization

#End Region

Public Class rptComparativeCostDistribution
    Implements IReport
    Implements IReportAsync

#Region "Properties"

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Dim criterias As Dictionary(Of String, String)

    Dim filters As Dictionary(Of String, String)

    Dim dtReportComparativeCosts As DataTable

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

#End Region

#Region "Load Data"

    Public Sub CargarDataSource() Implements IReport.CargarDataSource

    End Sub

    Public Async Function CargarDataSourceAsync() As Task Implements IReportAsync.CargarDataSourceAsync
        Try
            criterias = ParametrosReporte(0)
            filters = ParametrosReporte(1)

            Dim ds As DataSet = Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.GetReportComparativeCostsAsync(criterias, filters, Me.IndigoSessionValues)
            If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                dtReportComparativeCosts = ds.Tables("ReportComparativeCosts")
                Me.DataSource = dtReportComparativeCosts
                Me.DataMember = "ReportComparativeCosts"
            Else
                Me.DataSource = Nothing
            End If
        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
            Me.DataSource = Nothing
        End Try
    End Function

#End Region

#Region "Methods"

    Private Sub rptListRadicatedInvoice_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        InitializeReportLocalization()
        Dim initialRangefechaIni As Date = New Date(Me.criterias("InitialRangeYearStart"), Me.criterias("InitialRangeMonthStart"), 1)
        Dim initialRangefechafin As Date = New Date(Me.criterias("InitialRangeYearEnd"), Me.criterias("InitialRangeMonthEnd"), 1)
        Dim finalRangefechaIni As Date = New Date(Me.criterias("FinalRangeYearStart"), Me.criterias("FinalRangeMonthStart"), 1)
        Dim finalRangefechafin As Date = New Date(Me.criterias("FinalRangeYearEnd"), Me.criterias("FinalRangeMonthEnd"), 1)
        Me.INDPrmGroupBy.Value = filters("GroupBy")
        Me.INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        Me.INDLblNitCompany.Text = "Nit : " & IndigoSessionValues.IndigoCompanyNit
        Me.INDLblInitialRange.Text = "RANGO INICIAL DESDE " & initialRangefechaIni.ToString("MMMM DE yyyy").ToUpper() & " HASTA " & initialRangefechafin.ToString("MMMM DE yyyy").ToUpper()
        Me.INDLblFinalRange.Text = "RANGO FINAL DESDE " & finalRangefechaIni.ToString("MMMM DE yyyy").ToUpper() & " HASTA " & finalRangefechafin.ToString("MMMM DE yyyy").ToUpper()
        Me.INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
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

    ''' <summary>
    ''' Inicializa la cultura del reporte
    ''' </summary>
    Private Sub InitializeReportLocalization()
        Dim companySettings As GeneralLedgerCompanySettingsXpo =
        XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).
        AccountingService.GetXPOObject(Of GeneralLedgerCompanySettingsXpo)(Nothing)
        If companySettings IsNot Nothing Then
            Dim culture As CultureInfo = CultureInfo.CurrentCulture.Clone()
            culture.NumberFormat = companySettings.OfficialCurrency.Abbreviation.GetNumberFormat()
            ApplyLocalization(culture)
        End If
    End Sub

#End Region

End Class