#Region "Librerias Improtadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Domain.Entities
Imports Infrastructure.Data.Xpo.BudgetRepository
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraRichEdit.Model
Imports System.Configuration
Imports Presentation.Base
Imports System.Data.SqlClient
Imports Presentation.CloudAgent
Imports System.Data.SqlTypes
Imports DevExpress.Data
Imports DevExpress.Entity
Imports DevExpress.Xpo
Imports DevExpress.CodeParser
Imports DevExpress.Services
Imports System.Data

#End Region

Public Class rptNoveltiesPayroll
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance
    Dim dtReportPayrollNovelties As DataTable

    Private fechaIni As Date
    Private fechaFin As Date

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Dim fechaIni As Date = New Date(Me.ParametrosReporte(0), Me.ParametrosReporte(1), 1)
        Dim fecha As Date = fechaIni.AddMonths(+1)        
        Dim fechaFin As Date = fecha.AddDays(-1)



        Try
            Dim ds = IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetListReportPayrollReportNovelties(fechaIni, fechaFin, ParametrosReporte(2), ParametrosReporte(3), ParametrosReporte(4), ParametrosReporte(5), Me.IndigoSessionValues)
            If ds IsNot Nothing Then
                dtReportPayrollNovelties = ds.Tables("ReportPayrollNovelties")
                Me.DataSource = dtReportPayrollNovelties
                Me.DataMember = "ReportPayrollNovelties"
            Else
                Me.DataSource = Nothing
            End If

        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try

        INDLblDateMonth.Text = "Informe comprendido entre " & CDate(fechaIni).ToString("dd De MMMM Del yyyy") & " " & CDate(fechaFin).ToString("al dd De MMMM Del yyyy")        
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

    Private Sub GroupHeader3_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles GroupHeader3.BeforePrint
        Dim tipo = GetCurrentColumnValue("INDNoveltyClass")
        Dim GroupHeader As GroupHeaderBand = CType(GroupHeader3, GroupHeaderBand)

        If tipo Is Nothing Then
            XrTable3.Visible = False
            GroupHeader.HeightF = 43
        Else
            XrTable3.Visible = True
            GroupHeader.HeightF = 63
        End If
    End Sub

    Private Sub rptNoveltiesPayroll_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        ' Inicializar la localización del reporte (formato de moneda)
        UtilitiesReporter.InitializeReportLocalization(Me, IndigoSessionValues, 1)
    End Sub
End Class