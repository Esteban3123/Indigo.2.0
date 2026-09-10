#Region "Imports"
Imports Presentation.Reporter
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports Presentation.Common.MVP
Imports Infrastructure.Data.Xpo
Imports Presentation.CloudAgent
#End Region

Public Class FrmReportDeterioration

#Region "Properties"

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' propiedad para registar el mensaje en el visor
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

#End Region

#Region "Eventos"

#Region "Click"

    ''' <summary>
    ''' se ejecuta cuando den click en el boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        AsyncLoader(True)

        Dim reporte As New rptDeterioration

        reporte.ParametrosReporte = New Object() {INDDnMonthYear.GetYear,
                                                      INDDnMonthYear.GetMonth}

        INDDvViewReport.DocumentSource = reporte
        Await reporte.CargarDataSourceAsync()
        If reporte.DataSource IsNot Nothing Then
            reporte.CreateDocument(True)
            Me.INDLcBase.Visible = False
            Me.INDCncNavigation.Visible = False
            Me.INDPcViewReport.Visible = True
            INDDvViewReport.Show()
        Else
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
            Me.INDDnMonthYear.Focus()
        End If
        AsyncLoader(False)
    End Sub

    ''' <summary>
    ''' se ejecuta cuando den click en el boton generar excel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReports_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReports.Click
        Await chargueDatasource()
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento ClickBack del control INDCnBack
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnBack_ClickBack() Handles INDCnBack.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncNavigation.Visible = True
        Me.INDPcViewReport.Visible = False
        Me.INDDnMonthYear.Focus()
    End Sub

#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' creamos un datatable para generar el excel detallado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function chargueDatasource() As Task
        AsyncLoader(True)
        Dim dtReport As DataTable
        Try
            Dim IndList = Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetReportDeteriorationAsync(INDDnMonthYear.GetYear,
                                                      INDDnMonthYear.GetMonth,
                                                      Me.IndigoSessionValues)
            If IndList.Tables(0).Rows.Count > 0 Then
                dtReport = IndList.Tables("ReportDeterioration")
                INDGcExportExcel.DataSource = dtReport
                If Me.INDGcExportExcel.DataSource IsNot Nothing Then
                    generateExcel()
                End If
            Else
                INDGcExportExcel.DataSource = Nothing
            End If
            AsyncLoader(False)
        Catch ex As Exception
            AsyncLoader(False)
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try
    End Function

    ''' <summary>
    ''' metodo para generar excel
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub generateExcel()
        Dim _gridView = Me.INDGcExportExcel
        If _gridView IsNot Nothing Then
            Dim fileName As String = System.IO.Path.GetTempFileName() & ".xlsx"
            Dim param As New DevExpress.XtraPrinting.XlsxExportOptions(DevExpress.XtraPrinting.TextExportMode.Value, True, False)
            _gridView.ExportToXlsx(fileName, param)
            If System.IO.File.Exists(fileName) Then
                System.Diagnostics.Process.Start(fileName)
            End If
        End If
        Me.INDGcExportExcel.DataSource = Nothing
        Me.INDGcExportExcel.RefreshDataSource()
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