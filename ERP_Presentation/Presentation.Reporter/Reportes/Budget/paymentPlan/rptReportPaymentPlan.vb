#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.BudgetRepository
Imports Domain.Entities
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraRichEdit.Model
Imports System.Configuration
Imports Presentation.Base
Imports System.Data.SqlClient
Imports Presentation.CloudAgent
#End Region

Public Class rptReportPaymentPlan
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            '    'Dim filtroConsulta As String = "GetDate(DocumentDate) >= #" & Format(ParametrosReporte(0), "yyyy-MM-dd") & "# AND GetDate(DocumentDate) <= #" & Format(ParametrosReporte(1), "yyyy-MM-dd") & "#"
            Dim filtroConsulta As String = "Validity = " & ParametrosReporte(0) & " And Type = 2"

            If ParametrosReporte(1) IsNot Nothing AndAlso ParametrosReporte(2) IsNot Nothing Then
                filtroConsulta &= " AND Category >= '" & ParametrosReporte(1) & "' And Category <= '" & ParametrosReporte(2) & "'"
            End If

            If ParametrosReporte(3) IsNot Nothing And ParametrosReporte(4) IsNot Nothing Then
                filtroConsulta &= " AND GetDate(PaymentDate) >= #" & Format(ParametrosReporte(3), "yyyy-MM-dd") & "# AND GetDate(PaymentDate) <= #" & Format(ParametrosReporte(4), "yyyy-MM-dd") & "#"
            End If

            Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of BudgetViewReportPaymentPlanReportXpo)(Nothing, filtroConsulta)
        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try

    End Sub
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

    Private Sub rptDispensation_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit: " & IndigoSessionValues.IndigoCompanyNit
        INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        If ParametrosReporte(3) IsNot Nothing And ParametrosReporte(4) IsNot Nothing Then
            INDLblDate.Text = "Informe comprendido entre " & CDate(Format(ParametrosReporte(3), "yyyy-MM-dd")).ToString("dd De MMMM Del yyyy") & " " & CDate(Format(ParametrosReporte(4), "yyyy-MM-dd")).ToString(" al   dd De MMMM Del yyyy")
        End If
    End Sub
End Class