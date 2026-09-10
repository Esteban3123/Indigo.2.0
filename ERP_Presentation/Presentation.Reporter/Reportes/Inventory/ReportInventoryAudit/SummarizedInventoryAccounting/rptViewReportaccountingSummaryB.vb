#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Infrastructure.Data.Xpo
Imports Domain.Entities
Imports DevExpress.XtraReports.UI
Imports System.Drawing.Printing
Imports DevExpress.XtraReports.Parameters
Imports Presentation.Base
Imports System.Data
Imports System.Data.SqlClient
Imports Presentation.CloudAgent
#End Region

Public Class rptViewReportaccountingSummaryB
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Variable par obtener la tabla de Trazabilidad
    ''' </summary>
    Dim dtReportDocumentsDisorganizedInventoryVsAccounting As DataTable
    Private dr As SqlDataReader
    Private ds As DataSet
    Private foundRows() As DataRow

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            Dim ds = IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetReportReportAccountingSummaryB(ParametrosReporte(0), Me.ParametrosReporte(1), Me.IndigoSessionValues)
            If ds IsNot Nothing Then
                dtReportDocumentsDisorganizedInventoryVsAccounting = ds.Tables("ReportAccountingSummaryB")
                Me.DataSource = dtReportDocumentsDisorganizedInventoryVsAccounting
                Me.DataMember = "ReportAccountingSummaryB"
            Else
                Me.DataSource = Nothing
            End If

        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try

        'Dim filtroConsulta As String = Nothing
        ''filtro por fechas
        'If ParametrosReporte(0) <> "#12:00:00 AM#" Then
        '    filtroConsulta = "GetDate(DocumentDate) >= #" & Format(ParametrosReporte(0), "yyyy-MM-dd") & "# AND GetDate(DocumentDate) <= #" & Format(ParametrosReporte(1), "yyyy-MM-dd") & "#"
        'End If
        'Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of InventoryViewReportaccountingSummaryBReportXpo)(Nothing, filtroConsulta)
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
            Return Nothing
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptViewReportaccountingSummaryB_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        'If Me.Parameters.Count > 0 And Me.Parameters(0).Value > 0 And Parameters(1).Value > 0 Then
        Dim ParametrosFilter As ParameterCollection = Me.Parameters
        ParametrosReporte = New Object() {ParametrosFilter("DateStart").Value, ParametrosFilter("DateEnd").Value}
        CargarDataSource()
        'End If
    End Sub
End Class