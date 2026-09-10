#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.CloudAgent

#End Region

Public Class rptThirdBalance
    Implements IReport
    Implements IReportAsync

#Region "Properties"

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Dim criterias As Dictionary(Of String, String)

    Dim dtReportThirdPartyBalance As DataTable

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
    ''' Carga el datasource para generar el reporte
    ''' </summary>
    ''' <returns></returns>
    Public Async Function CargarDataSourceAsync() As Task Implements IReportAsync.CargarDataSourceAsync
        Try
            criterias = ParametrosReporte(0)

            Dim ds As DataSet = Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetReportThirdPartyBalanceAsync(criterias, Me.IndigoSessionValues)
            If ds.Tables(0).Rows.Count > 0 Then
                dtReportThirdPartyBalance = ds.Tables("ReportThirdPartyBalance")
                Me.DataSource = dtReportThirdPartyBalance
                Me.DataMember = "ReportThirdPartyBalance"
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

#End Region

#Region "Events"

    Private Sub rptThirdBalance_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        Dim INDFecha As Date = "01/" & criterias("Month") & "/" & criterias("Year")

        'Cargar los valores del titulo
        Me.INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        Me.INDLblNitCompany.Text = "Nit : " & IndigoSessionValues.IndigoCompanyNit
        Me.INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        Me.INDLblDate.Text = "Del Mes de " & INDFecha.ToString("MMMM Del yyyy")
    End Sub

#End Region

End Class