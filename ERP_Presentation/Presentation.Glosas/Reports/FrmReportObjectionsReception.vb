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

Public Class FrmReportObjectionsReception

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
                Using model As New Presentation.Glosas.MVP.MReports(Me.Tag)
                    Dim ds As DataSet = Await model.GetReportListObjectionsReception(criterias)
                    If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                        Dim dtReportObjectionsReception As DataTable = ds.Tables("ReportListObjectionsReception")

                        Await Task.Factory.StartNew(Sub()
                                                        chargueDatasource(dtReportObjectionsReception)
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
                Using model As New Presentation.Glosas.MVP.MReports(Me.Tag)
                    Dim ds As DataSet = Await model.GetReportListObjectionsReception(criterias)
                    If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                        Dim dtReportObjectionsReception As DataTable = ds.Tables("ReportListObjectionsReception")

                        Await Task.Factory.StartNew(Sub()
                                                        chargueDatasource(dtReportObjectionsReception)
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
    Private Sub chargueDatasource(ByVal dtReportObjectionsReception As DataTable)
        Dim dt As New DataTable
        dt.Columns.Add("Consecutivo")
        dt.Columns.Add("Fecha Radicación", GetType(DateTime))
        dt.Columns.Add("Fecha Oficio", GetType(DateTime))
        dt.Columns.Add("Oficio")
        dt.Columns.Add("Estado")
        dt.Columns.Add("Nit Cliente")
        dt.Columns.Add("Nombre Cliente")

        dt.Columns.Add("Factura")
        dt.Columns.Add("Fecha Factura", GetType(DateTime))
        dt.Columns.Add("Régimen")
        dt.Columns.Add("Centro de Costo")
        dt.Columns.Add("Valor Factura", GetType(Decimal))
        dt.Columns.Add("Saldo Factura", GetType(Decimal))

        dt.Columns.Add("Código del Servicio")
        dt.Columns.Add("Nombre del Servicio")
        dt.Columns.Add("Valor del Servicio", GetType(Decimal))
        dt.Columns.Add("Valor Unitario", GetType(Decimal))
        dt.Columns.Add("Cantidad", GetType(Decimal))
        dt.Columns.Add("Valor", GetType(Decimal))

        dt.Columns.Add("Código Glosa")
        dt.Columns.Add("Concepto Glosa")
        dt.Columns.Add("Código Respuesta")
        dt.Columns.Add("Concepto Respuesta")
        dt.Columns.Add("Responsable")
        dt.Columns.Add("Justificación")

        dt.Columns.Add("Valor Objetado EAPB", GetType(Decimal))
        dt.Columns.Add("Valor Reiterado", GetType(Decimal))
        dt.Columns.Add("Valor Aceptado IPS", GetType(Decimal))
        dt.Columns.Add("Valor Pagar EAPB", GetType(Decimal))

        dt.Columns.Add("Nota Código")
        dt.Columns.Add("Nota Fecha", GetType(DateTime))

        For Each item In dtReportObjectionsReception.Rows
            Dim row As DataRow = dt.NewRow()
            row.Item("Consecutivo") = item("RadicatedConsecutive")
            row.Item("Fecha Radicación") = CDate(item("RadicatedDate")).AsDate
            row.Item("Fecha Oficio") = CDate(item("DocumentDate")).AsDate
            row.Item("Oficio") = item("DocumentNumber")
            row.Item("Estado") = item("StatusName")
            row.Item("Nit Cliente") = item("CustomerNit")
            row.Item("Nombre Cliente") = item("CustomerName")

            row.Item("Factura") = item("InvoiceNumber")
            row.Item("Fecha Factura") = CDate(item("AccountReceivableDate")).AsDate
            row.Item("Régimen") = item("RegimenName")
            row.Item("Centro de Costo") = item("CostCenterCode")
            row.Item("Valor Factura") = item("Value")
            row.Item("Saldo Factura") = item("Balance")

            row.Item("Código del Servicio") = item("ServiceCode")
            row.Item("Nombre del Servicio") = item("ServiceName")
            row.Item("Valor del Servicio") = item("ValueServiceManual")
            row.Item("Valor Unitario") = item("UnitValue")
            row.Item("Cantidad") = item("Ammount")
            row.Item("Valor") = item("InvoicedValue")

            row.Item("Código Glosa") = item("CodeGlosa")
            row.Item("Concepto Glosa") = item("ConceptGlosa")
            row.Item("Código Respuesta") = item("CodeEvaluation")
            row.Item("Concepto Respuesta") = item("ConceptEvaluation")
            row.Item("Responsable") = item("Responsable")
            row.Item("Justificación") = item("Justification")

            row.Item("Valor Objetado EAPB") = item("ValueGlosado")
            row.Item("Valor Reiterado") = item("ValueReiterated")
            row.Item("Valor Aceptado IPS") = item("ValueAcceptedIPS")
            row.Item("Valor Pagar EAPB") = item("BalanceEAPB")

            row.Item("Nota Código") = item("NoteCode")
            row.Item("Nota Fecha") = item("NoteDate")
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