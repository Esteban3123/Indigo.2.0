#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.FixedAssetRepository
Imports Presentation.Base
Imports Presentation.CloudAgent

#End Region

Public Class rptDeterioration
    Implements IReport
    Implements IReportAsync

#Region "Properties"

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Variable para el datatable con los datos del reporte
    ''' </summary>
    Dim dtReportDeterioration As DataTable

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

#End Region

#Region "Events"

    Private Sub rptDeterioration_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
    End Sub

#End Region

#Region "Methods"

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
    End Sub

    Public Async Function CargarDataSourceAsync() As Task Implements IReportAsync.CargarDataSourceAsync
        Try
            Dim ds As DataSet = Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetReportDeteriorationAsync(ParametrosReporte(0), Me.ParametrosReporte(1), Me.IndigoSessionValues)
            If ds IsNot Nothing Then
                dtReportDeterioration = ds.Tables("ReportDeterioration")
                Me.DataSource = dtReportDeterioration
                Me.DataMember = "ReportDeterioration"
            Else
                Me.DataSource = Nothing
            End If

        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try
    End Function

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

End Class