#Region "Imports"

Imports System.Globalization
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.CloudAgent

#End Region

Public Class rptGeneralLedgerReconcileModule
    Implements IReport
    Implements IReportAsync

#Region "Properties"

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Dim criterias As Dictionary(Of String, String)

    ''' <summary>
    ''' Variable para el datatable con los datos del reporte
    ''' </summary>
    Dim dtReportReconcileModule As DataTable

    Dim ds As DataSet

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
            Me.DataSource = Nothing
            criterias = ParametrosReporte(0)

            ds = Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetReportReconcileModuleAsync(Me.criterias, Me.IndigoSessionValues)
            If ds IsNot Nothing Then
                dtReportReconcileModule = ds.Tables("ReportReconcileModule")
                If dtReportReconcileModule.Rows.Count > 0 Then
                    Me.DataSource = dtReportReconcileModule
                    Me.DataMember = "ReportReconcileModule"
                End If
            End If
        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try
    End Function

#End Region

#Region "Methods"

    Private Sub rptPriceListRate_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.BeforePrint
        INDPrModule.Value = Me.criterias("Module")
        Dim DateStart As Date = Me.criterias("DateStart")
        Dim DateEnd As Date = Me.criterias("DateEnd")

        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblRangeDate.Text = "DESDE " & DateStart.ToString("dd DE MMMM DE yyyy").ToUpper() & " HASTA " & DateEnd.ToString("dd DE MMMM DE yyyy").ToUpper()
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        Me.INDLblUserPrint.Text = "Usuario Impresión: " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName

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

    Private Sub XrTableCell13_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell13.BeforePrint
        Dim row = GetCurrentRow()
        XrTableCell13.Text = Utils.GetMoneyWithISO4217(DirectCast(row, System.Data.DataRowView).Row?.ItemArray(8), If(String.IsNullOrEmpty(DirectCast(row, System.Data.DataRowView).Row?.ItemArray(13)),
                                             IndigoSessionValues.CurrencyISO4217, DirectCast(row, System.Data.DataRowView).Row?.ItemArray(13)))
    End Sub

    Private Sub XrTableCell19_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell19.BeforePrint
        Dim row = GetCurrentRow()
        XrTableCell19.Text = Utils.GetMoneyWithISO4217(DirectCast(row, System.Data.DataRowView).Row?.ItemArray(10), If(String.IsNullOrEmpty(DirectCast(row, System.Data.DataRowView).Row?.ItemArray(13)),
                                             IndigoSessionValues.CurrencyISO4217, DirectCast(row, System.Data.DataRowView).Row?.ItemArray(13)))
    End Sub

    Private Sub XrTableCell14_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell14.BeforePrint
        Dim row = GetCurrentRow()
        If TryCast(row, System.Data.DataRowView)?.Row Is Nothing OrElse TryCast(row, System.Data.DataRowView)?.Row?.ItemArray?.Length < 9 Then
            Exit Sub
        End If
        Dim value = If(IsDBNull(TryCast(row, System.Data.DataRowView)?.Row?.ItemArray(9)), 0, TryCast(row, System.Data.DataRowView)?.Row?.ItemArray(9))
        XrTableCell14.Text = Utils.GetMoneyWithISO4217(value, If(String.IsNullOrEmpty(DirectCast(row, System.Data.DataRowView).Row?.ItemArray(13)),
                                             IndigoSessionValues.CurrencyISO4217, DirectCast(row, System.Data.DataRowView).Row?.ItemArray(13)))
    End Sub

    Private Sub XrTableCell16_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell16.BeforePrint
        Dim row = GetCurrentRow()
        XrTableCell16.Text = Utils.GetMoneyWithISO4217(DirectCast(row, System.Data.DataRowView).Row?.ItemArray(11), If(String.IsNullOrEmpty(DirectCast(row, System.Data.DataRowView).Row?.ItemArray(13)),
                                             IndigoSessionValues.CurrencyISO4217, DirectCast(row, System.Data.DataRowView).Row?.ItemArray(13)))
    End Sub

#End Region

End Class