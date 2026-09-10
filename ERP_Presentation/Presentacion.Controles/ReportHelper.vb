#Region "Imports"

Imports DevExpress.XtraReports.UI
Imports Infrastructure.CrossCutting.Base
Imports System.IO
Imports Presentation.Reporter
Imports DevExpress.XtraPrinting
Imports Presentation.Base

#End Region

''' <summary>
''' Provee metodos para la carga y visualización de reportes
''' </summary>
Public Class ReportHelper

    ''' <summary>
    ''' función que carga la customización del reporte y de los subreporte
    ''' </summary>
    ''' <param name="report"></param>
    ''' <param name="reportPath"></param>
    Public Shared Sub LoadCustomizationReport(report As XtraReport, reportPath As String)
        If File.Exists(reportPath) Then
            report.LoadLayout(reportPath)
        End If
        For Each ctr In report.AllControls(Of XRSubreport)()
            Dim frmParentName = Path.GetFileName(reportPath).Split(".")(1)
            Dim pathSubReport = reportPath.Replace(frmParentName, ctr.ReportSource?.Name)
            Dim subReportControl As XRSubreport = ctr
            If File.Exists(pathSubReport) Then
                Dim subReport As New XtraReport()
                subReport.LoadLayout(pathSubReport)
                subReportControl.ReportSource = subReport
            End If
        Next
    End Sub

    ''' <summary>
    ''' Ejecuta un reporte y carga su definición si existe
    ''' </summary>
    ''' <param name="frm">Visor de reporte</param>
    ''' <param name="showDialog">Valor que indica si el visor se muestra como Dialogo</param>
    ''' <param name="report">Reporte a ejecutar</param>
    ''' <param name="form">Formulario propietario del reporte</param>
    ''' <param name="permissions">Permisos del usuario sobre el frontal propietario</param>
    ''' <param name="params">Parámetros del reporte</param>
    Public Shared Sub ExecuteReport(ByVal frm As FrmReportViewer, ByVal showDialog As Boolean, ByVal report As XtraReport, ByVal form As Form, ByVal permissions As Dictionary(Of Integer, String), ByVal ParamArray params As Object())
        Dim reportPath = Path.Combine(ConfigurationFile.Instance.ReportsPath, (form.Tag & "." & report.GetType().Name & "." & "Report.repx"))

        LoadCustomizationReport(report, reportPath)

        If TypeOf report Is IReport Then
            If params IsNot Nothing AndAlso params.Length > 0 Then
                CType(report, IReport).ParametrosReporte = params
            End If
            CType(report, IReport).CargarDataSource()
            CType(report, IReport).CargarImagenes()
        End If

        frm.DocViewer.DocumentSource = report

        report.CreateDocument(True)

        AddHandler report.PrintingSystem.CreateDocumentException, AddressOf Report_CreateDocumentException

        If permissions IsNot Nothing Then
            report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.Customize, CommandVisibility.None)
            report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.Save, CommandVisibility.None)
            report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.Open, CommandVisibility.None)
            report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.SendFile, CommandVisibility.None)
            report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.Print, CommandVisibility.None)
            report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.PrintDirect, CommandVisibility.None)
            report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportPdf, CommandVisibility.None)
            report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportGraphic, CommandVisibility.None)
            report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportXps, CommandVisibility.None)
            report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportCsv, CommandVisibility.None)
            report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportHtm, CommandVisibility.None)
            report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportMht, CommandVisibility.None)
            report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportRtf, CommandVisibility.None)
            report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportTxt, CommandVisibility.None)
            report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportXls, CommandVisibility.None)
            report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportXlsx, CommandVisibility.None)

            report.PrintingSystem.AddCommandHandler(New CustomizeCommandHandler(report, reportPath))

            If permissions.ContainsKey(PermissionsActionsForm.ImprimirReporte) OrElse permissions.ContainsKey(PermissionsActionsForm.ImprimirDetallado) OrElse permissions.ContainsKey(PermissionsActionsForm.ImprimirFacturaAnulada) OrElse permissions.ContainsKey(PermissionsActionsForm.ImprimirTirilla) Then
                report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.Print, CommandVisibility.All)
                report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.PrintDirect, CommandVisibility.All)
            End If
            If permissions.ContainsKey(PermissionsActionsForm.ExportarFormatoLectura) Then
                report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportPdf, CommandVisibility.All)
                report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportGraphic, CommandVisibility.All)
                report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportXps, CommandVisibility.All)
            End If
            If permissions.ContainsKey(PermissionsActionsForm.ExportarFormatoEscritura) Then
                report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportCsv, CommandVisibility.All)
                report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportHtm, CommandVisibility.All)
                report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportMht, CommandVisibility.All)
                report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportRtf, CommandVisibility.All)
                report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportTxt, CommandVisibility.All)
                report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportXls, CommandVisibility.All)
                report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportXlsx, CommandVisibility.All)
            End If
            If permissions.ContainsKey(PermissionsActionsForm.PersonalizarReporte) Then
                report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.Customize, CommandVisibility.All)
                frm.CustomizeButton.Enabled = True
            End If
        Else
            report.PrintingSystem.AddCommandHandler(New CustomizeCommandHandler(report, reportPath))
            report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.Customize, CommandVisibility.All)
            frm.CustomizeButton.Enabled = True
        End If

        If showDialog Then
            Using tras As New FrmTransparent(frm, False)
                tras.ShowDialog(form)
            End Using
        Else
            frm.Show()
        End If

    End Sub

    Shared Sub Report_CreateDocumentException(sender As Object, args As DevExpress.XtraPrinting.ExceptionEventArgs)
        Infrastructure.CrossCutting.Exceptions.IndigoManagementExceptions.HandleException(args.Exception, "UIPolicy")
    End Sub

    ''' <summary>
    ''' Ejecuta un reporte y carga su definición si existe
    ''' </summary>
    ''' <param name="frm">Visor de reporte</param>
    ''' <param name="report">Reporte a ejecutar</param>
    ''' <param name="form">Formulario propietario del reporte</param>
    ''' <param name="permissions">Permisos del usuario sobre el frontal propietario</param>
    ''' <param name="params">Parámetros del reporte</param>
    Public Shared Sub ExecuteReport(ByVal frm As FrmReportViewer, ByVal report As XtraReport, ByVal form As Form, ByVal permissions As Dictionary(Of Integer, String), ByVal ParamArray params As Object())
        ExecuteReport(frm, True, report, form, permissions, params)
    End Sub

    ''' <summary>
    ''' Ejecuta un reporte y carga su definición si existe
    ''' </summary>
    ''' <param name="report">Reporte a ejecutar</param>
    ''' <param name="form">Formulario propietario del reporte</param>
    ''' <param name="permissions">Permisos del usuario sobre el frontal propietario</param>
    ''' <param name="params">Parámetros del reporte</param>
    Public Shared Sub ExecuteReport(ByVal report As XtraReport, ByVal form As Form, ByVal permissions As Dictionary(Of Integer, String), ByVal ParamArray params As Object())
        Dim frm As New FrmReportViewer()
        ExecuteReport(frm, True, report, form, permissions, params)
    End Sub



End Class
