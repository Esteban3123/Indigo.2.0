#Region "Imports"

Imports System.Globalization
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.AccountingRepository
Imports Presentation.Base
Imports Presentation.CloudAgent

#End Region

Public Class rptCostListResultOperation
    Implements IReport
    Implements IReportAsync

#Region "Properties"

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Dim criterias As Dictionary(Of String, String)

    Dim dtReportResultProductionCostsExpenses As DataTable

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

            Dim ds As DataSet = Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.GetReportResultProductionCostsExpensesAsync(criterias, Me.IndigoSessionValues)
            If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                dtReportResultProductionCostsExpenses = ds.Tables("ReportResultProductionCostsExpenses")
                Me.DataSource = dtReportResultProductionCostsExpenses
                Me.DataMember = "ReportResultProductionCostsExpenses"
            Else
                Me.DataSource = Nothing
            End If
        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try
    End Function

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

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

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

#Region "Events"

    Private Sub rptCostListResultOperation_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        InitializeReportLocalization()
        Dim fechaIni As Date = New Date(Me.criterias("Year"), Me.criterias("MonthStart"), 1)
        Dim fechafin As Date = New Date(Me.criterias("Year"), Me.criterias("MonthEnd"), 1)
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        Me.INDLblDate.Text = "DESDE " & fechaIni.ToString("MMMM DE yyyy").ToUpper() & " HASTA " & fechafin.ToString("MMMM DE yyyy").ToUpper()
        Me.INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        Me.XrTableCell45.Text = Utils.Num2Text(Convert.ToDouble(GetCurrentColumnValue("INDCfTotalValue"))).ToString & " PESOS M/Cte."
    End Sub

#End Region

End Class