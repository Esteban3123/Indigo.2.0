#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.BudgetRepository
Imports Presentation.Base
Imports Presentation.CloudAgent
#End Region

Public Class rptReportReportbudgetExecutionByCategoryThird
    Implements IReport
    Implements IReportAsync

#Region "Properties"

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Dim criterias As Dictionary(Of String, String)

    Dim dtReportBudgetExecutionByCategoryThird As DataTable

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

            Dim ds As DataSet = Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetReportBudgetExecutionByCategoryThirdAsync(Me.criterias, Me.IndigoSessionValues)
            If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                dtReportBudgetExecutionByCategoryThird = ds.Tables("ReportBudgetExecutionByCategoryThird")
                Me.DataSource = dtReportBudgetExecutionByCategoryThird
                Me.DataMember = "ReportBudgetExecutionByCategoryThird"
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

    Private Sub rptReportExpenseRecordBook_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        INDLblReportData.Text = "Informe a corte " & CDate(criterias("CutoffDate")).ToString("dd De MMMM Del yyyy")
    End Sub

    Private Sub GroupHeader3_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles GroupHeader3.BeforePrint
        If IsDBNull(Me.GetCurrentColumnValue("ThirdPartyId")) Then
            e.Cancel = True
            Exit Sub
        End If
    End Sub

    Private Sub GroupHeader4_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles GroupHeader4.BeforePrint
        If IsDBNull(Me.GetCurrentColumnValue("Level")) OrElse Me.GetCurrentColumnValue("Level") <> 3 Then
            e.Cancel = True
            Exit Sub
        End If
    End Sub

    Private Sub GroupHeader5_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles GroupHeader5.BeforePrint
        If IsDBNull(Me.GetCurrentColumnValue("Level")) OrElse Me.GetCurrentColumnValue("Level") <> 5 Then
            e.Cancel = True
            Exit Sub
        End If
    End Sub

    Private Sub GroupHeader6_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles GroupHeader6.BeforePrint
        If IsDBNull(Me.GetCurrentColumnValue("Level")) OrElse Me.GetCurrentColumnValue("Level") <> 7 Then
            e.Cancel = True
            Exit Sub
        End If
    End Sub

#End Region

End Class