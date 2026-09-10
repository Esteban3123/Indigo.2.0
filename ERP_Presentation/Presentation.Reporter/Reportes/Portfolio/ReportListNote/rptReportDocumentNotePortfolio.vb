#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.PortfolioRepository
Imports Infrastructure.Data.Xpo
Imports Presentation.Base
Imports DevExpress.XtraReports.Parameters

#End Region

Public Class rptReportDocumentNotePortfolio
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            Dim filtroConsulta As String = "Id = " & ParametrosReporte(0)
            Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PaymentsService.GetCollection(Of PortfolioNoteReportXpo)(Nothing, filtroConsulta)
            'For Each item In listReport
            '    Dim filtroConsulta2 As String = "PortfolioNoteId.Id = " & item.Id
            '    Dim List As List(Of PortfolioNoteDetailReportXpo) = XpoServiceex.Instance(IndigoSessionValues.TransactionalContainer).PaymentsService.GetCollection(Of PortfolioNoteDetailReportXpo)(Nothing, filtroConsulta2)
            '    For Each item2 In List
            '        item.ValueTotal += item2.Value
            '    Next
            '    item.ValueLetters = Utils.Num2Text(Convert.ToDouble(item.ValueTotal)).ToString & " PESOS M/CTE."
            'Next
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

    Private Sub rptReportDocumentNotePortfolio_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        If Me.Parameters.Count > 0 And Me.Parameters(0).Value > 0 Then
            Dim ParametrosFilter As ParameterCollection = Me.Parameters
            ParametrosReporte = New Object() {ParametrosFilter("INDSubIdNote").Value}
            CargarDataSource()
            Me.XrTableCell13.Text = Utils.Num2Text(Convert.ToDouble(GetCurrentColumnValue("INDCfValueDetail"))).ToString & " PESOS M/Cte."
        End If
        Me.INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        Me.INDLblNitCompany.Text = "Nit : " & IndigoSessionValues.IndigoCompanyNit
        INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
    End Sub
End Class