#Region "Imports"

Imports System.Text
Imports DevExpress.XtraGrid.Views.Grid
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Presentation.Base
Imports Presentation.Reporter

#End Region

Public Class FrmReportReconcileModule

#Region "Variables"

    ''' <summary>
    ''' Diccionario que almacena diferentes criterios 
    ''' </summary>
    Private criterias As Dictionary(Of String, String)

#End Region

#Region "Datasources"

    ''' <summary>
    ''' Propiedad que se usa para cargar la tupla de datos de módulos(Tesorería, Cuentas por Cobrar, Cuentas por Pagar, Inventario)
    ''' </summary>
    Private _FillingModule As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingModule As List(Of Tuple(Of Integer, String))
        Get
            If _FillingModule Is Nothing Then
                _FillingModule = New List(Of Tuple(Of Integer, String))
                _FillingModule.Add(New Tuple(Of Integer, String)(1, "Tesorería"))
                _FillingModule.Add(New Tuple(Of Integer, String)(2, "Cuentas por Cobrar"))
                _FillingModule.Add(New Tuple(Of Integer, String)(3, "Cuentas por Pagar"))
                _FillingModule.Add(New Tuple(Of Integer, String)(4, "Inventario"))
            End If
            Return _FillingModule
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que se usa para cargar la tupla de Datos de tipos de reporte(Sin Conciliar, Conciliado, Ambos)
    ''' </summary>
    Private _FillingTypeReport As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingTypeReport As List(Of Tuple(Of Integer, String))
        Get
            If _FillingTypeReport Is Nothing Then
                _FillingTypeReport = New List(Of Tuple(Of Integer, String))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(1, "Sin Conciliar"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(2, "Conciliado"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(3, "Ambos"))
            End If
            Return _FillingTypeReport
        End Get
    End Property

#End Region

#Region "Messages"
    ''' <summary>
    ''' Propiedad para registar el mensaje en el visor
    ''' </summary>
    ''' <param name="Icono"></param>
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
#End Region


#Region "BarraBotones"

    ''' <summary>
    ''' Se cargan los permisos que tiene el frontal en la barra
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Método para Realizar las validaciones del formulario
    ''' </summary>
    ''' <returns></returns>
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
        criterias.Add("Module", INDGleModule.EditValue)
        criterias.Add("TypeReport", INDGleTypeReport.EditValue)

        criterias.Add("EntityNames", _selectorEntityName.GetKeys())
        criterias.Add("JournalVoucherTypes", _selectorJournalVoucherType.GetKeys())
        criterias.Add("MainAccounts", _selectorMainAccount.GetKeys())

        Return True
    End Function

#Region "ToExcel"

    ''' <summary>
    ''' Prepara los datos para generar el reporte 
    ''' </summary>
    ''' <param name="dtReportReconcileModule"></param>
    Private Sub chargueDataSource(ByVal dtReportReconcileModule As DataTable)
        Dim dt As New DataTable
        dt.Columns.Add("Fecha Documento", GetType(Date))
        dt.Columns.Add("Fecha Comprobante", GetType(Date))
        dt.Columns.Add("Documento Origen")
        dt.Columns.Add("Código")
        dt.Columns.Add("Tipo de Comprobante")
        dt.Columns.Add("Consecutivo")
        dt.Columns.Add("Cuenta Contable")
        dt.Columns.Add("Valor Débito Origen", GetType(Decimal))
        dt.Columns.Add("Valor Débito Contable", GetType(Decimal))
        dt.Columns.Add("Valor Crédito Origen", GetType(Decimal))
        dt.Columns.Add("Valor Crédito Contable", GetType(Decimal))
        dt.Columns.Add("Moneda")

        For Each item In dtReportReconcileModule.Rows
            Dim row As DataRow = dt.NewRow()
            row.Item("Fecha Documento") = item("DocumentDate")
            row.Item("Fecha Comprobante") = item("AccountingDocumentDate")
            row.Item("Documento Origen") = item("Description")
            row.Item("Código") = item("EntityCode")
            row.Item("Tipo de Comprobante") = item("JournalVoucherType")
            row.Item("Consecutivo") = item("Consecutive")
            row.Item("Cuenta Contable") = item("MainAccount")
            row.Item("Valor Débito Origen") = item("DebitValue")
            row.Item("Valor Débito Contable") = item("AccountingDebitValue")
            row.Item("Valor Crédito Origen") = item("CreditValue")
            row.Item("Valor Crédito Contable") = item("AccountingCreditValue")
            row.Item("Moneda") = item("Abbreviation")
            dt.Rows.Add(row)
        Next

        INDGcExportExcell.DataSource = dt
    End Sub

    ''' <summary>
    ''' Método que genera el reporte en excel
    ''' </summary>
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

#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' se inicializan los miembros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportReconcileModule_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Cargar GridLookUpEdit
        Me.INDGleTypeReport.Properties.DataSource = FillingTypeReport
        Me.INDGleModule.Properties.DataSource = FillingModule

        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleTypeReport.EditValue = 1
        Me.INDGleModule.EditValue = 1
    End Sub

    ''' <summary>
    ''' Se ejecuta al cerrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _FillingTypeReport = Nothing
        _FillingModule = Nothing

        _selectorEntityName = Nothing
        _selectorJournalVoucherType = Nothing
        _selectorMainAccount = Nothing
    End Sub

#End Region

#Region "QueryPopUp"

    ''' <summary>
    '''  Se encarga de cargar los datos de las entidades en el control "Documento Origen"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleEntityName_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleEntityName.QueryPopUp
        If INDSleEntityName.Properties.DataSource Is Nothing Then
            INDSleEntityName.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).AccountingService.ListEntityNames()
        End If
    End Sub

    ''' <summary>
    ''' Se encarga de cargar los datos de las entidades en el control "Tipo de Comprobante"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleJournalVoucherType_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleJournalVoucherType.QueryPopUp
        If INDSleJournalVoucherType.Properties.DataSource Is Nothing Then
            INDSleJournalVoucherType.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).AccountingService.ListJournalVoucherTypes()
        End If
    End Sub

    ''' <summary>
    ''' Se encarga de cargar los datos de las entidades en el control "Cuenta Contable"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleMainAccount_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleMainAccount.QueryPopUp
        If INDSleMainAccount.Properties.DataSource Is Nothing Then
            INDSleMainAccount.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).AccountingService.ListMainAccountByBudgetControl(True, 1)
        End If
    End Sub

#End Region

#Region "Selector"

    ''' <summary>
    ''' Crea una instancia de la clase SelectorCache, esperando dos argumentos (EntityName, Description)
    ''' </summary>
    Private _selectorEntityName As SelectorCache = New SelectorCache("EntityName", "Description")

    ''' <summary>
    ''' Crea una instancia de la clase SelectorCache, esperando dos argumentos (Id, Code)
    ''' </summary>
    Private _selectorJournalVoucherType As SelectorCache = New SelectorCache("Id", "Code")

    ''' <summary>
    ''' Crea una instancia de la clase SelectorCache, esperando dos argumentos (Id, Number)
    ''' </summary>
    Private _selectorMainAccount As SelectorCache = New SelectorCache("Id", "Number")

    ''' <summary>
    ''' Se encarga de asignar valores a las celdas en columnas no enlazadas de tres rejillas diferentes(INDGvEntityName, INDGvJournalVoucherType, INDGvMainAccount)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvSelector_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvEntityName.CustomUnboundColumnData, INDGvJournalVoucherType.CustomUnboundColumnData, INDGvMainAccount.CustomUnboundColumnData
        If e.IsGetData Then
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvEntityName" Then
                e.Value = _selectorEntityName.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvJournalVoucherType" Then
                e.Value = _selectorJournalVoucherType.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvMainAccount" Then
                e.Value = _selectorMainAccount.ValidateExistsRow(e.Row)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Se encarga de manejar la selección de filas en una rejilla basada en una columna especial que se utiliza para la selección no enlazada.
    ''' dependiendo de si se hizo clic en una fila válida o fuera de las filas, se agrega o elimina la fila de la caché de selecciones, y luego se actualizan los datos en la rejilla para reflejar los cambios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvSelector_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles INDGvEntityName.RowCellClick, INDGvJournalVoucherType.RowCellClick, INDGvMainAccount.RowCellClick
        If e.Column.FieldName.Contains("UnboundSelection") Then
            Dim selector As SelectorCache = Nothing
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvEntityName" Then
                selector = _selectorEntityName
            ElseIf view.Name = "INDGvJournalVoucherType" Then
                selector = _selectorJournalVoucherType
            ElseIf view.Name = "INDGvMainAccount" Then
                selector = _selectorMainAccount
            End If

            If e.RowHandle >= 0 Then
                Dim row = view.GetRow(e.RowHandle)
                selector.SetValue(row)
            Else
                selector.Clear()
            End If
            view.RefreshData()
        End If
    End Sub

    ''' <summary>
    '''  Controlador de eventos que se ejecuta cuando se cierra el cuadro de diálogo de búsqueda y selección de un SearchLookUpEdit
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleSelector_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDSleEntityName.Closed, INDSleJournalVoucherType.Closed, INDSleMainAccount.Closed
        Dim searchLookupEdit = TryCast(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        searchLookupEdit.EditValue = Nothing
        If searchLookupEdit.Name = "INDSleEntityName" Then
            searchLookupEdit.Properties.NullText = _selectorEntityName.ToString()
        ElseIf searchLookupEdit.Name = "INDSleJournalVoucherType" Then
            searchLookupEdit.Properties.NullText = _selectorJournalVoucherType.ToString()
        ElseIf searchLookupEdit.Name = "INDSleMainAccount" Then
            searchLookupEdit.Properties.NullText = _selectorMainAccount.ToString()
        End If
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

                Dim reporte = New rptGeneralLedgerReconcileModule()
                reporte.ParametrosReporte = New Object() {criterias}
                INDDvReport.DocumentSource = reporte
                Await reporte.CargarDataSourceAsync()
                AsyncLoader(False)
                If reporte.DataSource IsNot Nothing Then
                    reporte.CreateDocument(True)
                    Me.INDLcBase.Visible = False
                    Me.INDCncReport.Visible = False
                    Me.INDPcReport.Visible = True
                    INDDvReport.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDDteDateStart.Focus()
                End If
            Catch ex As Exception
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
                Using model As New Presentation.Accounting.MVP.MReports(Me.Tag)
                    Dim ds As DataSet = Await model.GetReportReconcileModule(criterias)
                    If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                        Dim dtReportReconcileModule As DataTable = ds.Tables("ReportReconcileModule")

                        Await Task.Factory.StartNew(Sub()
                                                        chargueDataSource(dtReportReconcileModule)
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

End Class