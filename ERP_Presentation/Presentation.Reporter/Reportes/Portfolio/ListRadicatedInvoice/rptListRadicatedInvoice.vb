#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Presentation.Base

#End Region

Public Class rptListRadicatedInvoice
    Implements IReport

#Region "Properties"

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Dim criterias As Dictionary(Of String, String)

    Dim filters As Dictionary(Of String, String)

    Dim dtReportRadicateInvoice As DataTable

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

    Public Async Function CargarDataSourceAsync() As Task
        Try
            criterias = ParametrosReporte(0)
            filters = ParametrosReporte(1)

            Dim ds As DataSet = Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetListReportRadicateInvoiceAsync(criterias, filters, Me.IndigoSessionValues)
            If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                dtReportRadicateInvoice = ds.Tables("ReportRadicateInvoice")
                Me.DataSource = dtReportRadicateInvoice
                Me.DataMember = "ReportRadicateInvoice"
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
        Me.INDPrmTypeReport.Value = criterias("TypeReport")
        Me.INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        Me.INDLblNitCompany.Text = "Nit : " & IndigoSessionValues.IndigoCompanyNit
        Me.INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName

        If Me.INDPrmTypeReport.Value = 1 Then
            Detail.Visible = True
            INDLblTitle.Text = String.Format(INDLblTitle.Text, "DETALLADO")
        Else
            Detail.Visible = False
            INDLblTitle.Text = String.Format(INDLblTitle.Text, "RESUMIDO")
        End If
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

#End Region

End Class