'***********************************************************************
' Assembly         : Presentation.Controls
' Author           : Juan Diego Diaz Mosquera
' Created          : 07-02-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports DevExpress.Drawing
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraPrinting.Drawing
Imports DevExpress.XtraReports.UI
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Reporter

''' <summary>
''' Visualizador de reportes
''' </summary>
Public Class ReportPrintToolExt
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Objeto de tipo xtrareport
    ''' </summary>
    Private _report As DevExpress.XtraReports.UI.XtraReport
    ''' <summary>
    ''' Mes auditoria
    ''' </summary>
    Public _month As String
    Private _name As String
    Public TotalPrint As Integer
    ''' <summary>
    ''' Año auditoria
    ''' </summary>
    Public _year As String
    ''' <summary>
    ''' Tag del formulario
    ''' </summary>
    Private _tag As String
    ''' <summary>
    ''' Id del registro 
    ''' </summary>
    Private _idEntity As String
    Private _formParent As DevExpress.XtraEditors.XtraForm
    Private _pathReport As String
    Private _permissionsForm As Dictionary(Of Integer, String)
    Private _reportName As String
    Private _parameters As String

    Private _flagCustomizeButton As Boolean = False

#End Region

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDLoadAudit_Click(sender As Object, e As EventArgs) Handles INDLoadAudit.Click

        Me._month = CType(INDNavigatorDate.GetMonth, String)
        Me._year = CType(INDNavigatorDate.GetYear, String)
        If Me._month.Length = 1 Then
            Me._month = "0" & Me._month
        End If
        Using model = New MReporter(Me._tag)
            Dim listAudit = model.ListAuditBasic(Me._month, Me._year, Me._tag, Me._idEntity)
            Me.INDBasicAuditGc.DataSource = listAudit
            LayoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            LayoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            LayoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End Using
        Me.INDBasicAuditGc.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Evento para customizar valores en la rejilla de auditoria basica
    ''' </summary>
    Private Sub INDAuditBasicGv_CustomColumnDisplayText(sender As Object, e As CustomColumnDisplayTextEventArgs) Handles INDBasicAuditGv.CustomColumnDisplayText
        If e.Column.FieldName = "Operation" Then
            If e.Value IsNot Nothing Then
                Select Case e.Value.ToString.Trim()
                    Case "6"
                        e.DisplayText = "Ver Reporte"
                    Case "7"
                        e.DisplayText = "Imprimir Reporte"
                    Case "8"
                        e.DisplayText = "Exportar PDF"
                    Case "9"
                        e.DisplayText = "Exportar Xls"
                    Case "10"
                        e.DisplayText = "Exportar Xlsx"
                    Case "11"
                        e.DisplayText = "Exportar Text"
                    Case "12"
                        e.DisplayText = "Exportar Csv"
                    Case "13"
                        e.DisplayText = "Exportar Mht"
                    Case "14"
                        e.DisplayText = "Exportar Html"
                    Case "15"
                        e.DisplayText = "Exportar Rtf"
                    Case "16"
                        e.DisplayText = "Exportar Imagen"
                    Case "17"
                        e.DisplayText = "Exportar Xps"
                End Select
            End If
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        LayoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        LayoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        LayoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Me.INDBasicAuditGc.DataSource = Nothing
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="report"></param>
    ''' <remarks></remarks>
    Sub New(formAux As DevExpress.XtraEditors.XtraForm, report As DevExpress.XtraReports.UI.XtraReport, tag As String, idEntity As String, pathReport As String)
        InitializeComponent()
        Me._report = report
        Me._tag = tag
        Me.TotalPrint = 0
        Me._idEntity = idEntity
        Me._formParent = formAux
        Me._pathReport = pathReport
        Me._name = Me._formParent.Name

        AddHandler Me._report.PrintingSystem.CreateDocumentException, AddressOf Report_CreateDocumentException
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="tag"></param>
    ''' <param name="ReportName"></param>
    ''' <param name="PermissionsForm"></param>
    ''' <param name="Parameters"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Async Function ShowDocument(tag As String, ReportName As String, PermissionsForm As Dictionary(Of Integer, String), Parameters As String) As Threading.Tasks.Task
        Me._permissionsForm = PermissionsForm
        Await Me.EvalueTotalPrint()
        Me.ShowWaterMark()
        Dim frmTransparent As FrmTransparent = Nothing
        Await Me.CreateBatchedDocument()
        DocumentViewer1.DocumentSource = Me._report

        '***** AUDITORA ******'
        Using model = New MReporter(Me._tag)
            Me._reportName = ReportName
            Me._parameters = Parameters
            Await model.saveAudit(Me._idEntity, Me._formParent.Name, Parameters, ReportName, ActionsAudit.VerReporte, 1)
        End Using

        Me.EnsurePermiossions()
        Me.StartPosition = FormStartPosition.CenterScreen
        frmTransparent = New FrmTransparent(Me, False)
        CType(Me._formParent, FormBase).AsyncLoader(False)
        frmTransparent.ShowDialog()
    End Function

    ''' <summary>
    ''' Metodo asincrono que crea el documento usando subreportes para el manejo de 
    ''' grandes cantidades de datos
    ''' </summary>
    ''' <returns>La tarea correspondiente a crear el documento paginado</returns>
    Private Function CreateBatchedDocument() As System.Threading.Tasks.Task
        Return System.Threading.Tasks.Task.Run(Sub()
                                                   Try
                                                       SyncLock Me._report
                                                           For Each dataSource In GetBatchedData()
                                                               Dim subReport As New XtraReport()
                                                               subReport.DataSource = dataSource
                                                               subReport.CreateDocument()
                                                               Me._report.Pages.AddRange(subReport.Pages)
                                                           Next
                                                           Me._report.CreateDocument()
                                                       End SyncLock
                                                   Catch ex As Exception
                                                       Throw
                                                   End Try
                                               End Sub)
    End Function

    Async Function PrintDocument(tag As String, ReportName As String, PermissionsForm As Dictionary(Of Integer, String), Parameters As String) As Threading.Tasks.Task
        If _permissionsForm Is Nothing Then
            _permissionsForm = PermissionsForm
        End If
        Await Me.EvalueTotalPrint()
        Me.ShowWaterMark()
        '***** AUDITORA ******'
        Using model = New MReporter(Me._tag)
            Me._reportName = ReportName
            Me._parameters = Parameters
            Await model.saveAudit(Me._idEntity, Me._formParent.Name, Parameters, ReportName, ActionsAudit.ImprimirReporte, 1)
        End Using
        Me._report.Print()
    End Function

    ''' <summary>
    ''' Metodo iterator que divide los datos provenientes del dataSource del XtraReport en grupos de mil
    ''' </summary>
    ''' <returns></returns>
    Private Iterator Function GetBatchedData() As IEnumerable(Of IEnumerable(Of Object))
        Dim batchSize As Integer = 1000
        Dim currentBatch As New List(Of Object)(batchSize)

        Try
            If TypeOf _report.DataSource Is Data.DataSet Then
                Dim dataSet As Data.DataSet = CType(_report.DataSource, Data.DataSet)

                For Each table As System.Data.DataTable In dataSet.Tables
                    For Each row As Data.DataRow In table.Rows
                        currentBatch.Add(row)
                        If currentBatch.Count >= batchSize Then
                            Yield currentBatch.ToList()
                            currentBatch.Clear()
                        End If
                    Next
                Next

            ElseIf TypeOf _report.DataSource Is IEnumerable Then
                Dim enumerable As IEnumerable = CType(_report.DataSource, IEnumerable)

                For Each item In CType(Me._report.DataSource, IEnumerable)
                    currentBatch.Add(item)
                    If currentBatch.Count >= batchSize Then
                        Yield currentBatch.ToList()
                        currentBatch.Clear()
                    End If
                Next
            End If

            If currentBatch.Count > 0 Then
                Yield currentBatch.ToList()
            End If

        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Evalua la cantidad de copias impresas del reporte y muestra o no
    ''' la marca de agua con el numero de copia
    ''' </summary>
    Public Function EvalueTotalPrint() As System.Threading.Tasks.Task
        Return System.Threading.Tasks.Task.Factory.StartNew(Sub()
                                                                Dim _flagHideWatermark = False
                                                                If Me._permissionsForm IsNot Nothing AndAlso Me._permissionsForm.ContainsKey(PermissionsActionsForm.HideWatermark) Then
                                                                    _flagHideWatermark = True
                                                                End If

                                                                If Not _flagHideWatermark Then
                                                                    Using model = New MReporter(Me._tag)
                                                                        Dim res = model.GetTotalPrint(Me._formParent.Name, Me._idEntity)
                                                                        Me.TotalPrint = res
                                                                    End Using
                                                                End If
                                                            End Sub)
    End Function

    ''' <summary>
    ''' Muestra la marca de agua mostrando que el reporte es una copia
    ''' </summary>
    Public Sub ShowWaterMark()
        If Me.TotalPrint > 0 Then
            Me._report.Watermark.Text = "COPIA No. " & (Me.TotalPrint)
            Me._report.Watermark.TextDirection = DirectionMode.ForwardDiagonal
            Me._report.Watermark.Font = New DXFont(_report.Watermark.Font.Name, 40)
            Me._report.Watermark.ForeColor = Color.DodgerBlue
            Me._report.Watermark.TextTransparency = 150
            Me._report.Watermark.ShowBehind = False
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="ReportName"></param>
    ''' <param name="PermissionsForm"></param>
    ''' <param name="Parameters"></param>
    ''' <remarks></remarks>
    Async Sub ExecReport(ReportName As String, PermissionsForm As Dictionary(Of Integer, String), Optional Parameters As String = "", Optional isPrint As Boolean = False)
        Me._reportName = ReportName
        Me._parameters = Parameters
        If DocumentViewer1.InvokeRequired Then
            DocumentViewer1.BeginInvoke(Async Sub()
                                            If isPrint = True Then
                                                Await PrintDocument(Me._tag, ReportName, PermissionsForm, Parameters)
                                            Else
                                                Await ShowDocument(Me._tag, ReportName, PermissionsForm, Parameters)
                                            End If
                                        End Sub)
        Else
            If isPrint = True Then
                Await PrintDocument(Me._tag, ReportName, PermissionsForm, Parameters)
            Else
                Await ShowDocument(Me._tag, ReportName, PermissionsForm, Parameters)
            End If
        End If
    End Sub

    Private Sub BtnCustomize_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BtnCustomize.ItemClick
        Me.Cursor = BaseClass.ChangeCursorIndigo()
        Dim reportBase As New Reporter.ReportBase
        If reportBase.EditReportWithShowDialog(Me._report, Me._pathReport) = System.Windows.Forms.DialogResult.OK Then
            'Aqui refrescamos la lista de definiciones de reporte
            CType(Me._formParent, FormBase).BarraBotones.LoadReportsAndDefinitions()
        End If
        Me.Cursor = Cursors.Default
    End Sub

    ''' <summary>
    ''' Verifica los permisos del usuario
    ''' </summary>
    Public Sub EnsurePermiossions()
        Me._report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.Customize, CommandVisibility.None)
        Me._report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.Save, CommandVisibility.None)
        Me._report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.Open, CommandVisibility.None)
        Me._report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.SendFile, CommandVisibility.None)
        Me._report.PrintingSystem.AddCommandHandler(New PrintCommandHandler(Me._report, Me._idEntity, Me._name, Me._parameters, Me._reportName, Me._tag, Me))
        If Not Me._permissionsForm.ContainsKey(PermissionsActionsForm.ImprimirReporte) Then
            Me._report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.Print, CommandVisibility.None)
            Me._report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.PrintDirect, CommandVisibility.None)
        End If
        If Not Me._permissionsForm.ContainsKey(PermissionsActionsForm.ExportarFormatoLectura) Then
            Me._report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportPdf, CommandVisibility.None)
            Me._report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportGraphic, CommandVisibility.None)
            Me._report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportXps, CommandVisibility.None)
        End If
        If Not Me._permissionsForm.ContainsKey(PermissionsActionsForm.ExportarFormatoEscritura) Then
            Me._report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportCsv, CommandVisibility.None)
            Me._report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportHtm, CommandVisibility.None)
            Me._report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportMht, CommandVisibility.None)
            Me._report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportRtf, CommandVisibility.None)
            Me._report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportTxt, CommandVisibility.None)
            Me._report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportXls, CommandVisibility.None)
            Me._report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportXlsx, CommandVisibility.None)
        End If
        If Me._permissionsForm.ContainsKey(PermissionsActionsForm.PersonalizarReporte) Then
            Me._report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.Customize, CommandVisibility.All)
            Me.BtnCustomize.Enabled = True
            Me._flagCustomizeButton = True
        End If
    End Sub

    Protected Overrides Sub OnShown(e As EventArgs)
        If Me._flagCustomizeButton Then
            Me.BtnCustomize.Enabled = True
        End If
        MyBase.OnShown(e)
    End Sub

    Sub Report_CreateDocumentException(sender As Object, args As DevExpress.XtraPrinting.ExceptionEventArgs)
        Infrastructure.CrossCutting.Exceptions.IndigoManagementExceptions.HandleException(args.Exception, "UIPolicy")
    End Sub

End Class

''' <summary>
''' 
''' </summary>
Public Class PrintCommandHandler
    Implements DevExpress.XtraPrinting.ICommandHandler

    Private _report As XtraReport
    Private _id As Integer
    Private _parameters As String
    Private _reportName As String
    Private _name As String
    Private _tag As String
    Private _parent As ReportPrintToolExt

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="report"></param>
    ''' <param name="Id"></param>
    ''' <param name="Name"></param>
    ''' <param name="Parameters"></param>
    ''' <param name="ReportName"></param>
    ''' <param name="Tag"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal report As XtraReport, Id As Integer, Name As String, Parameters As String, ReportName As String, Tag As String, ByVal parent As ReportPrintToolExt)
        Me._report = report
        Me._id = Id
        Me._name = Name
        Me._parameters = Parameters
        Me._reportName = ReportName
        Me._tag = Tag
        Me._parent = parent
        AddHandler Me._report.BeforePrint, AddressOf Report_BeforePrint
    End Sub

    Private Sub Report_BeforePrint(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Me._parent.ShowWaterMark()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="command"></param>
    ''' <remarks></remarks>
    Private Async Sub Audit(command As PrintingSystemCommand, printControl As IPrintControl)
        Dim Action As ActionsAudit = ActionsAudit.None
        Dim count As Integer = 1
        Select Case command
            Case PrintingSystemCommand.Print
                Dim tool As New ReportPrintTool(Me._report)
                If tool.PrintDialog() Then
                    count = tool.PrinterSettings.Copies
                    Action = ActionsAudit.ImprimirReporte
                End If
            Case PrintingSystemCommand.PrintDirect
                Action = ActionsAudit.ImprimirReporte
            Case PrintingSystemCommand.ExportCsv
                Action = ActionsAudit.ExportarCsv
            Case PrintingSystemCommand.ExportGraphic
                Action = ActionsAudit.ExportarImagen
            Case PrintingSystemCommand.ExportHtm
                Action = ActionsAudit.ExportarHtml
            Case PrintingSystemCommand.ExportMht
                Action = ActionsAudit.ExportarMht
            Case PrintingSystemCommand.ExportPdf
                Action = ActionsAudit.ExportarPdf
            Case PrintingSystemCommand.ExportRtf
                Action = ActionsAudit.ExportarRtf
            Case PrintingSystemCommand.ExportTxt
                Action = ActionsAudit.ExportarText
            Case PrintingSystemCommand.ExportXls
                Action = ActionsAudit.ExportarXls
            Case PrintingSystemCommand.ExportXlsx
                Action = ActionsAudit.ExportarXlsx
            Case PrintingSystemCommand.ExportXps
                Action = ActionsAudit.ExportarXps
        End Select
        If Action <> ActionsAudit.None Then
            Await Me._parent.EvalueTotalPrint()
            Using model = New MReporter(Me._tag)
                Await model.saveAudit(Me._id, Me._name, Me._parameters, Me._reportName, Action, count)
            End Using

            Me._report.CreateDocument(True)
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="command"></param>
    ''' <param name="printControl"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CanHandleCommand(command As PrintingSystemCommand, printControl As IPrintControl) As Boolean Implements ICommandHandler.CanHandleCommand
        Return (command = PrintingSystemCommand.ExportPdf _
            OrElse command = PrintingSystemCommand.ExportGraphic _
            OrElse command = PrintingSystemCommand.Print _
            OrElse command = PrintingSystemCommand.PrintDirect _
            OrElse command = PrintingSystemCommand.PrintSelection _
            OrElse command = PrintingSystemCommand.ExportCsv _
            OrElse command = PrintingSystemCommand.ExportHtm _
            OrElse command = PrintingSystemCommand.ExportMht _
            OrElse command = PrintingSystemCommand.ExportRtf _
            OrElse command = PrintingSystemCommand.ExportTxt _
            OrElse command = PrintingSystemCommand.ExportXls _
            OrElse command = PrintingSystemCommand.ExportXlsx _
            OrElse command = PrintingSystemCommand.ExportXps)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="command"></param>
    ''' <param name="args"></param>
    ''' <param name="printControl"></param>
    ''' <param name="handled"></param>
    ''' <remarks></remarks>
    Public Sub HandleCommand(command As PrintingSystemCommand, args() As Object, printControl As IPrintControl, ByRef handled As Boolean) Implements ICommandHandler.HandleCommand
        handled = (command = PrintingSystemCommand.PrintSelection OrElse command = PrintingSystemCommand.Print OrElse command = PrintingSystemCommand.PrintDirect)
        Audit(command, printControl)
    End Sub

End Class
