#Region "Imports"

Imports Presentation.Reporter
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid
Imports Presentation.Common.MVP
Imports Presentation.Portfolio.MVP
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports System.Text

#End Region

Public Class FrmReportRadicateInvoice
    Implements IReportRadicateInvoice

#Region "Fields"

    ''' <summary>
    ''' Representa el presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PReportRadicateInvoice

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

#End Region

#Region "Properties"

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Private _FillingTypeReport As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingTypeReport As List(Of Tuple(Of Integer, String))
        Get
            If _FillingTypeReport Is Nothing Then
                _FillingTypeReport = New List(Of Tuple(Of Integer, String))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(1, "Detallado"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(2, "Resumido"))
            End If
            Return _FillingTypeReport
        End Get
    End Property

    Private _FilterDateBy As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FilterDateBy As List(Of Tuple(Of Integer, String))
        Get
            If _FilterDateBy Is Nothing Then
                _FilterDateBy = New List(Of Tuple(Of Integer, String))
                _FilterDateBy.Add(New Tuple(Of Integer, String)(1, "Fecha Radicación"))
                _FilterDateBy.Add(New Tuple(Of Integer, String)(2, "Fecha Oficio"))
                _FilterDateBy.Add(New Tuple(Of Integer, String)(3, "Fecha Factura"))
            End If
            Return _FilterDateBy
        End Get
    End Property

    Private criterias As Dictionary(Of String, String)
    Private filters As Dictionary(Of String, String)

    Private ListCustomerId As New List(Of Integer)()
    Private CustomerIds As String

    Private ListRadicateInvoiceId As New List(Of Integer)()
    Private RadicateInvoiceIds As String

#End Region

#Region "XPO"

    Public Property CustomerXpo As DevExpress.Xpo.XPCollection(Of Infrastructure.Data.Xpo.PortfolioRepository.CommonCustomerReportXpo) Implements IReportRadicateInvoice.CustomerXpo
        Get
            Return INDSleCustomer.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPCollection(Of Infrastructure.Data.Xpo.PortfolioRepository.CommonCustomerReportXpo))
            INDSleCustomer.Properties.DataSource = value
        End Set
    End Property

    Public Property RadicateInvoiceXpo As DevExpress.Xpo.XPCollection(Of Infrastructure.Data.Xpo.PortfolioRepository.PortfolioRadicateInvoiceCReportXpo) Implements IReportRadicateInvoice.RadicateInvoiceXpo
        Get
            Return INDSleRadicateInvoice.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPCollection(Of Infrastructure.Data.Xpo.PortfolioRepository.PortfolioRadicateInvoiceCReportXpo))
            INDSleRadicateInvoice.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "BarraBotones"

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' se ejecuta al cargar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportRadicateInvoice_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Cargar el GridLookUpEdit
        Me.INDGleTypeReport.Properties.DataSource = FillingTypeReport
        Me.INDGleFilterDateBy.Properties.DataSource = FilterDateBy

        'Asigna un valor por defecto a GridLookUpEdit
        Me.INDGleTypeReport.EditValue = 1
        Me.INDGleFilterDateBy.EditValue = 1

        'Inicializamos la referencia
        Presenter = New PReportRadicateInvoice(Me)
        Me._pucModel = New MCommon(Me.Tag)
    End Sub

    ''' <summary>
    ''' Se ejecuta al cerrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing

        _pucModel.Dispose()
        _pucModel = Nothing

        ListCustomerId = Nothing
        CustomerIds = Nothing
        ListRadicateInvoiceId = Nothing
        RadicateInvoiceIds = Nothing
    End Sub

#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleCustomer
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleCustomer_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCustomer.QueryPopUp
        If INDSleCustomer.Properties.DataSource Is Nothing Then
            Presenter.ListCollectionCustomerReportPortfolio()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleRadicateInvoice
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleRadicateInvoice_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleRadicateInvoice.QueryPopUp
        If INDSleRadicateInvoice.Properties.DataSource Is Nothing Then
            Presenter.ListCollectionRadicateInvoice()
        End If
    End Sub

#End Region

#Region "MouseDown"

    ''' <summary>
    ''' Evento que se dispara al checkear los items de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDgvCustomer_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles INDgvCustomer.MouseDown, INDgvRadicateInvoice.MouseDown
        Dim view As GridView = TryCast(sender, GridView)
        Dim hi As GridHitInfo = view.CalcHitInfo(e.Location)
        If hi.Column IsNot Nothing Then
            If hi.Column.FieldName = "DX$CheckboxSelectorColumn" Then
                If view.Name = "INDgvCustomer" Then
                    UpdateList(view, hi, ListCustomerId)
                ElseIf view.Name = "INDgvRadicateInvoice" Then
                    UpdateList(view, hi, ListRadicateInvoiceId)
                End If
            End If
        End If
    End Sub

#End Region

#Region "ColumnFilterChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del filtro
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDgvCustomer_ColumnFilterChanged(sender As Object, e As EventArgs) Handles INDgvCustomer.ColumnFilterChanged, INDgvRadicateInvoice.ColumnFilterChanged
        RestoreSelection(TryCast(sender, GridView))
    End Sub

#End Region

#Region "CloseUp"

    Private Sub INDSleCustomer_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDSleCustomer.CloseUp
        CustomerIds = RecuperarSeleccionados(sender, "Id", "NitName")
    End Sub

    Private Sub INDSleRadicateInvoice_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDSleRadicateInvoice.CloseUp
        RadicateInvoiceIds = RecuperarSeleccionados(sender, "Id", "RadicatedConsecutive")
    End Sub

#End Region

#Region "Report"

    ''' <summary>
    ''' se ejecuta cuando den click en el boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports = True Then
            AsyncLoader(True)

            Dim reporte As New rptListRadicatedInvoice
            reporte.ParametrosReporte = New Object() {criterias, filters}
            INDDvDocumentViewer.DocumentSource = reporte
            Await reporte.CargarDataSourceAsync()
            AsyncLoader(False)
            If reporte.DataSource IsNot Nothing Then
                reporte.CreateDocument(True)
                Me.INDLcBase.Visible = False
                Me.INDCncNavigation.Visible = False
                Me.INDPcDocumentViewer.Visible = True
                INDDvDocumentViewer.Show()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                Me.INDDeStart.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta al dar click en el control INDCnBack
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnBack_ClickBack() Handles INDCnBack.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncNavigation.Visible = True
        Me.INDPcDocumentViewer.Visible = False
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
                Using model As New MReports(Me.Tag)
                    Dim ds As DataSet = Await model.GetListReportRadicateInvoice(criterias, filters)
                    If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                        Dim dtReportRadicateInvoice As DataTable = ds.Tables("ReportRadicateInvoice")

                        If INDGleTypeReport.EditValue = 1 Then
                            chargueDatasource(dtReportRadicateInvoice)
                        Else
                            chargueDatasourceResum(dtReportRadicateInvoice)
                        End If

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

    Private Sub UpdateList(view As GridView, hi As GridHitInfo, ListId As List(Of Integer))
        If Not hi.InRow Then
            Dim allSelected As Boolean = view.DataController.Selection.Count = view.DataRowCount
            If Not allSelected Then
                For i As Integer = 0 To view.RowCount - 1
                    Dim sourceHandle As Integer = view.GetDataSourceRowIndex(i)
                    If Not ListId.Contains(sourceHandle) Then
                        ListId.Add(sourceHandle)
                    End If
                Next i
            Else
                ListId.Clear()
            End If
        Else
            Dim sourceHandle As Integer = view.GetDataSourceRowIndex(hi.RowHandle)
            If Not ListId.Contains(sourceHandle) Then
                ListId.Add(sourceHandle)
            Else
                ListId.Remove(sourceHandle)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo que reestablece el check de los items de la rejilla
    ''' </summary>
    ''' <param name="view"></param>
    Private Sub RestoreSelection(ByVal view As GridView)
        If view.Name = "INDgvCustomer" Then
            BeginInvoke(New Action(Sub()
                                       Dim i As Integer = 0
                                       Do While i < ListCustomerId.Count
                                           view.SelectRow(view.GetRowHandle(ListCustomerId(i)))
                                           i += 1
                                       Loop
                                   End Sub))
        ElseIf view.Name = "INDgvRadicateInvoice" Then
            BeginInvoke(New Action(Sub()
                                       Dim i As Integer = 0
                                       Do While i < ListRadicateInvoiceId.Count
                                           view.SelectRow(view.GetRowHandle(ListRadicateInvoiceId(i)))
                                           i += 1
                                       Loop
                                   End Sub))
        End If
    End Sub

    Private Function RecuperarSeleccionados(sender As Object, keyField As String, descripcionField As String) As String
        Dim edit As DevExpress.XtraEditors.SearchLookUpEdit = TryCast(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        Dim identificadores As String = String.Empty
        Dim identificador As String = String.Empty
        Dim descripciones As String = String.Empty
        Dim separador As String = String.Empty
        Dim ListId As Integer() = edit.Properties.View.GetSelectedRows()
        For Each selectionRow As Integer In ListId
            identificador = edit.Properties.View.GetRowCellValue(selectionRow, keyField).ToString()
            If Not String.IsNullOrEmpty(identificador) Then
                identificadores += separador & identificador
                descripciones += separador & edit.Properties.View.GetRowCellValue(selectionRow, descripcionField).ToString()
                separador = ", "
            End If
        Next
        edit.Properties.NullText = descripciones.ToString()
        edit.ToolTip = descripciones.ToString()
        Return identificadores.ToString()
    End Function

    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim Validations As Boolean = True
        Dim errors As New StringBuilder

        If Me.INDDeStart.EditValue Is Nothing Then
            errors.AppendLine("Debe seleccionar una fecha inicial")
        End If

        If INDDeDateEnd.EditValue Is Nothing Then
            errors.AppendLine("Debe seleccionar una fecha final")
        End If

        If Me.INDDeStart.EditValue IsNot Nothing AndAlso Me.INDDeDateEnd.EditValue IsNot Nothing AndAlso Me.INDDeStart.EditValue > INDDeDateEnd.EditValue Then
            errors.AppendLine(String.Format(ResourceManager.GetString("CompareRangeDate", "Commons")))
        End If

        If String.IsNullOrEmpty(Me.INDchkStatus.EditValue) Then
            errors.AppendLine("Debe seleccionar al menos un estado")
        End If

        If String.IsNullOrEmpty(Me.INDchkDevolution.EditValue) Then
            errors.AppendLine("Debe indicar si se mostrara o no la radicación de facturas por devolucion")
        End If

        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Return False
        End If

        criterias = New Dictionary(Of String, String)
        criterias.Add("TypeReport", INDGleTypeReport.EditValue)
        criterias.Add("FilterDateBy", INDGleFilterDateBy.EditValue)

        filters = New Dictionary(Of String, String)
        filters.Add("DateStart", INDDeStart.EditValue)
        filters.Add("DateEnd", INDDeDateEnd.EditValue)
        filters.Add("Customers", CustomerIds)
        filters.Add("RadicateInvoices", RadicateInvoiceIds)
        filters.Add("Status", INDchkStatus.EditValue)
        filters.Add("Devolution", INDchkDevolution.EditValue)

        Return True
    End Function

    ''' <summary>
    ''' creamos un datatable para generar el excel resumido
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargueDatasourceResum(ByVal dtReportRadicateInvoice As DataTable)
        Dim dt As New DataTable
        dt.Columns.Add("Nro Radicado")
        dt.Columns.Add("Nit")
        dt.Columns.Add("Entidad")
        dt.Columns.Add("Fecha Radicado", GetType(DateTime))
        dt.Columns.Add("Codigo G. Atencion")
        dt.Columns.Add("Nombre G. Atencion")
        dt.Columns.Add("Estado Radicado")
        dt.Columns.Add("Regimen")
        dt.Columns.Add("Facturas", GetType(Integer))
        dt.Columns.Add("Nota Credito", GetType(Decimal))
        dt.Columns.Add("Saldo", GetType(Decimal))

        For Each item In dtReportRadicateInvoice.Rows
            Dim row As DataRow = dt.NewRow()
            row.Item("Nro Radicado") = item("RadicatedConsecutive")
            row.Item("Nit") = item("NitCustomer")
            row.Item("Entidad") = item("NameCustomer")
            row.Item("Fecha Radicado") = CDate(item("RadicatedDate")).AsDate
            row.Item("Codigo G. Atencion") = item("CodeCareGroup")
            row.Item("Nombre G. Atencion") = item("NameCareGroup")
            row.Item("Estado Radicado") = item("StatusName")
            row.Item("Regimen") = item("RegimenName")
            row.Item("Facturas") = item("Invoices")
            row.Item("Nota Credito") = item("CreditNoteValue")
            row.Item("Saldo") = item("BalanceInvoice")
            dt.Rows.Add(row)
        Next

        INDGcExportExcell.DataSource = dt
    End Sub

    ''' <summary>
    ''' creamos un datatable para generar el excel detallado
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargueDatasource(ByVal dtReportRadicateInvoice As DataTable)
        Dim dt As New DataTable
        dt.Columns.Add("Nro Radicado")
        dt.Columns.Add("Nit")
        dt.Columns.Add("Entidad")
        dt.Columns.Add("Fecha Radicado", GetType(DateTime))
        dt.Columns.Add("Codigo G. Atencion")
        dt.Columns.Add("Nombre G. Atencion")
        dt.Columns.Add("Estado Radicado")
        dt.Columns.Add("Regimen")
        dt.Columns.Add("Nro Factura")
        dt.Columns.Add("Fecha Factura", GetType(DateTime))
        dt.Columns.Add("Identificacion")
        dt.Columns.Add("Nombre Paciente")
        dt.Columns.Add("Nota Credito", GetType(Decimal))
        dt.Columns.Add("Saldo", GetType(Decimal))
        dt.Columns.Add("Devolución")
        dt.Columns.Add("Concepto Devolución")

        For Each item In dtReportRadicateInvoice.Rows
            Dim row As DataRow = dt.NewRow()
            row.Item("Nro Radicado") = item("RadicatedConsecutive")
            row.Item("Nit") = item("NitCustomer")
            row.Item("Entidad") = item("NameCustomer")
            row.Item("Fecha Radicado") = CDate(item("RadicatedDate")).AsDate
            row.Item("Codigo G. Atencion") = item("CodeCareGroup")
            row.Item("Nombre G. Atencion") = item("NameCareGroup")
            row.Item("Estado Radicado") = item("StatusName")
            row.Item("Regimen") = item("RegimenName")
            row.Item("Nro Factura") = item("InvoiceNumber")
            row.Item("Fecha Factura") = CDate(item("InvoiceDate")).AsDate
            row.Item("Identificacion") = item("PatientCode")
            row.Item("Nombre Paciente") = item("PatientName")
            row.Item("Nota Credito") = item("CreditNoteValue")
            row.Item("Saldo") = item("BalanceInvoice")
            row.Item("Devolución") = item("Devolution")
            row.Item("Concepto Devolución") = item("ConceptDevolution")
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
            Dim fileName As String = System.IO.Path.GetTempFileName() & INDGleTypeReport.EditValue & ".xlsx"
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