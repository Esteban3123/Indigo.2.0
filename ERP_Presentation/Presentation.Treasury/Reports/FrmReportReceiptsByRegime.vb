#Region "Imports"

Imports System.Text
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Presentation.Base
Imports Presentation.Common.MVP
Imports Presentation.Controls.MVP
Imports Presentation.Reporter

#End Region

Public Class FrmReportReceiptsByRegime

#Region "Variables"

    Private criterias As Dictionary(Of String, String)

#End Region

#Region "BarraBotones"

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' Se ejecuta al cerrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        criterias = Nothing
    End Sub

#End Region

#Region "Report"

    ''' <summary>
    ''' se ejecuta en el evento click del boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports = True Then
            Try
                AsyncLoader(True)
                Using model As New Presentation.Treasury.MVP.MReports(Me.Tag)
                    Dim ds As DataSet = Await model.GetReportReceiptsByRegime(criterias)
                    If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                        Dim dtReportReceiptsByRegime As DataTable = ds.Tables("ReportReceiptsByRegime")

                        Await Task.Factory.StartNew(Sub()
                                                        chargueDatasource(dtReportReceiptsByRegime)
                                                    End Sub)

                        If Me.INDGcExportExcell.DataSource IsNot Nothing Then
                            generateExcel()
                        End If
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    End If
                End Using
            Catch ex As Exception
                Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
            Finally
                AsyncLoader(False)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento ClickBack en el control INDCnReturn
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnReturn_ClickBack() Handles INDCnReturn.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncReport.Visible = True
        Me.INDPcReport.Visible = False
        Me.INDDteDateStart.Focus()
    End Sub

    ''' <summary>
    ''' se ejecuta para exportar a excel el reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateExcell_Click(sender As Object, e As EventArgs) Handles INDSbGenerateExcell.Click
        If Me.ValidateControlsReports = True Then
            Try
                AsyncLoader(True)
                Using model As New Presentation.Treasury.MVP.MReports(Me.Tag)
                    Dim ds As DataSet = Await model.GetReportReceiptsByRegime(criterias)
                    If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                        Dim dtReportReceiptsByRegime As DataTable = ds.Tables("ReportReceiptsByRegime")

                        Await Task.Factory.StartNew(Sub()
                                                        chargueDatasource(dtReportReceiptsByRegime)
                                                    End Sub)

                        If Me.INDGcExportExcell.DataSource IsNot Nothing Then
                            generateExcel()
                        End If
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    End If
                End Using
            Catch ex As Exception
                Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
            Finally
                AsyncLoader(False)
            End Try
        End If
    End Sub

#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' propiedad para registar el mensaje en el visor
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String 'Implements IcrudBase.Mensaje
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

    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim errors As New StringBuilder

        If INDDteDateStart.EditValue Is Nothing Or INDDteDateEnd.EditValue Is Nothing Then
            errors.AppendLine(String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons")))
            Me.INDDteDateStart.Focus()
        ElseIf Me.INDDteDateStart.EditValue > INDDteDateEnd.EditValue Then
            errors.AppendLine(String.Format(ResourceManager.GetString("FrmReportAuxiliary_CompareDate", "Accounting")))
            Me.INDDteDateStart.Focus()
        End If

        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Return False
        End If

        criterias = New Dictionary(Of String, String)
        criterias.Add("DateStart", INDDteDateStart.EditValue)
        criterias.Add("DateEnd", INDDteDateEnd.EditValue)

        Return True
    End Function

    ''' <summary>
    ''' creamos un datatable para generar el excel de reconocimientos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargueDatasource(ByVal dtReportReceiptsByRegime As DataTable)
        Dim dt As New DataTable
        dt.Columns.Add("Código")
        dt.Columns.Add("Régimen")
        dt.Columns.Add("Valor", GetType(Decimal))

        For Each item In dtReportReceiptsByRegime.Rows
            Dim row As DataRow = dt.NewRow()
            row.Item("Código") = item("Code")
            row.Item("Régimen") = item("Regime")
            row.Item("Valor") = item("Value")
            dt.Rows.Add(row)
        Next

        INDGcExportExcell.DataSource = dt
    End Sub

    ''' <summary>
    ''' metodo para generar excel
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub generateExcel()
        Dim _gridView = Me.INDGcExportExcell
        _gridView.MainView.PopulateColumns()
        If _gridView IsNot Nothing Then
            Dim fileName As String = System.IO.Path.GetTempFileName() & ".xlsx"
            Dim param As New DevExpress.XtraPrinting.XlsxExportOptions(DevExpress.XtraPrinting.TextExportMode.Value, True, False)
            _gridView.ExportToXlsx(fileName, param)
            If System.IO.File.Exists(fileName) Then
                System.Diagnostics.Process.Start(fileName)
            End If
        End If
        Me.INDGcExportExcell.DataSource = Nothing
        Me.INDGcExportExcell.RefreshDataSource()
    End Sub

#End Region

End Class