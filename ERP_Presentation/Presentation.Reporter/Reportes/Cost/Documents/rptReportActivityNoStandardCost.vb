#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PaymentsRepository
Imports DevExpress.XtraReports.UI
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.CloudAgent
Imports Infrastructure.Data.Xpo.AccountingRepository
Imports System.Globalization

#End Region

Public Class rptReportActivityNoStandardCost
    Implements IReport

#Region "Properties"

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Dim filters As Dictionary(Of String, String)

    Private listSettingPayments As List(Of PaymentsAgesPaymentsXpo)

    Dim dtReportActivityCosts As DataTable

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

    ''' <summary>
    ''' Metodo asincronico que ejecutara la busqueda de datos para mostrar en el reporte
    ''' </summary>
    ''' <returns></returns>
    Public Async Function CargarDataSourceAsync() As Task
        Try
            filters = ParametrosReporte(0)

            Dim ds As DataSet = Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.GetListReportActivityCostsAsync(filters, Me.IndigoSessionValues)
            If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                dtReportActivityCosts = ds.Tables("ReportActivityCosts")
                Me.DataSource = dtReportActivityCosts
                Me.DataMember = "ReportActivityCosts"
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

    ''' <summary>
    ''' Obtiene datos importantes para mostrar en el reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub rptReportActivityNoStandardCost_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        InitializeReportLocalization()
        Me.INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        Me.INDLblNitCompany.Text = "Nit : " & IndigoSessionValues.IndigoCompanyNit
        Me.INDLblDate.Text = String.Format("Periodo: {0}-{1}", Me.filters("Year"), Me.filters("Month"))
        INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
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

End Class