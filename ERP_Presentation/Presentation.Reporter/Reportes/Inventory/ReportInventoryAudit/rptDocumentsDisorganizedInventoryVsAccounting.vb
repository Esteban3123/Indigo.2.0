#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Domain.Entities
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

Public Class rptDocumentsDisorganizedInventoryVsAccounting
    Implements IReport
    Implements IReportAsync

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
            Dim ds = IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetReportDocumentsDisorganizedInventoryVsAccounting(ParametrosReporte(0), Me.ParametrosReporte(1), Me.IndigoSessionValues)
            If ds.Tables(0).Rows.Count > 0 Then
                dtReportDocumentsDisorganizedInventoryVsAccounting = ds.Tables("ReportDocumentsDisorganizedInventoryVsAccounting")
                Me.DataSource = dtReportDocumentsDisorganizedInventoryVsAccounting
                Me.DataMember = "ReportDocumentsDisorganizedInventoryVsAccounting"
            Else
                Me.DataSource = Nothing
            End If

        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try
    End Sub
    Public Function CargarDataSourceAsync() As Task Implements IReportAsync.CargarDataSourceAsync
        Return Task.Factory.StartNew(AddressOf CargarDataSource)
    End Function
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

    Private Sub rptDocumentsDisorganizedInventoryVsAccounting_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        Me.INDLblNameCompany.Text = IndigoSessionValues.IndigoCompanyName
        Me.INDLblNitCompany.Text = "Nit : " & IndigoSessionValues.IndigoCompanyNit
        Me.INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName

        If Me.ParametrosReporte(1) IsNot Nothing Then
            Me.INDLblSubTitle.Text = "Informe comprendido entre " & CDate(Me.ParametrosReporte(0)).ToString("dd De MMMM Del yyyy") & " " & CDate(Me.ParametrosReporte(1)).ToString("A dd De MMMM Del yyyy")
        End If
    End Sub
End Class