#Region "Imports"

Imports System.Globalization
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.AccountingRepository
Imports Presentation.Base
Imports Presentation.CloudAgent

#End Region

Public Class rptCostListResultOperationDetail
    Implements IReport
    Implements IReportAsync

#Region "Properties"

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Dim criterias As Dictionary(Of String, String)

    Dim dtReportResultProductionCostsExpensesDetail As DataTable

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

            Dim ds As DataSet = Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.GetReportResultProductionCostsExpensesDetailAsync(criterias, Me.IndigoSessionValues)
            If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                dtReportResultProductionCostsExpensesDetail = ds.Tables("ReportResultProductionCostsExpensesDetail")
                Me.DataSource = dtReportResultProductionCostsExpensesDetail
                Me.DataMember = "ReportResultProductionCostsExpensesDetail"
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

    Private Sub rptListResultOperationDetail_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        InitializeReportLocalization()
        Me.INDPrDetailType.Value = CInt(Me.criterias("DetailType"))
        Dim fechaIni As Date = New Date(Me.criterias("Year"), Me.criterias("MonthStart"), 1)
        Dim fechafin As Date = New Date(Me.criterias("Year"), Me.criterias("MonthEnd"), 1)
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        Me.INDLblDate.Text = "DESDE " & fechaIni.ToString("MMMM DE yyyy").ToUpper() & " HASTA " & fechafin.ToString("MMMM DE yyyy").ToUpper()
        Me.INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName

        Dim detailTypeName As String = "N/A"
        Select Case CInt(Me.criterias("DetailType"))
            Case 1 : detailTypeName = "MANO DE OBRA"
            Case 2 : detailTypeName = "SUMINISTROS"
            Case 3 : detailTypeName = "CONSUMO"
            Case 4 : detailTypeName = "GASTOS GENERALES"
            Case 5 : detailTypeName = "ACTIVOS FIJOS"
            Case 6 : detailTypeName = "VENTAS"
        End Select
        Using rtb As New System.Windows.Forms.RichTextBox()
            rtb.Font = New System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold)
            rtb.SelectAll()
            rtb.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Center
            rtb.Text = "DETALLADO RESULTADO DE LA OPERACIÓN " & detailTypeName
            Me.XrRichText1.Rtf = rtb.Rtf
        End Using
    End Sub

#End Region

End Class