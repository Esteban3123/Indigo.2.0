#Region "Imports"

Imports System.Text
Imports DevExpress.Xpo
Imports DevExpress.XtraGrid.Views.Grid
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Presentation.Base
Imports Presentation.Reporter

#End Region

Public Class FrmReportMonthlyExpenseExecution

#Region "Variables"

    Private criterias As Dictionary(Of String, String)

#End Region

#Region "Datasource"
    ''' <summary>
    ''' Propiedad al usada para el xpo de entidad
    ''' </summary>
    ''' <returns></returns>
    Private Property BudgetaryEntityXpo As XPInstantFeedbackSource
        Get
            Return INDsleEntity.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleEntity.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad a usar para el xpo de vigencia
    ''' </summary>
    ''' <returns></returns>
    Private Property BudgetaryValidityXpo As DevExpress.Xpo.XPCollection
        Get
            Return INDsleValidity.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPCollection)
            INDsleValidity.Properties.DataSource = value
        End Set
    End Property

    Private _FillingCodeToUse As List(Of Tuple(Of Integer, String))
    ''' <summary>
    ''' Propiedad para llenar el control de codigo a usar
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property FillingCodeToUse As List(Of Tuple(Of Integer, String))
        Get
            If _FillingCodeToUse Is Nothing Then
                _FillingCodeToUse = New List(Of Tuple(Of Integer, String))
                _FillingCodeToUse.Add(New Tuple(Of Integer, String)(1, "Código Rubro"))
                _FillingCodeToUse.Add(New Tuple(Of Integer, String)(2, "Código Alterno"))
            End If
            Return _FillingCodeToUse
        End Get
    End Property

    Private _FillingIncludeZero As List(Of Tuple(Of Boolean, String))
    ''' <summary>
    ''' Propiedad que llena el IncludeZero con una tupla
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property FillingIncludeZero As List(Of Tuple(Of Boolean, String))
        Get
            If _FillingIncludeZero Is Nothing Then
                _FillingIncludeZero = New List(Of Tuple(Of Boolean, String))
                _FillingIncludeZero.Add(New Tuple(Of Boolean, String)(True, "Si"))
                _FillingIncludeZero.Add(New Tuple(Of Boolean, String)(False, "No"))
            End If
            Return _FillingIncludeZero
        End Get
    End Property
    ''' <summary>
    ''' propiedad para hacer el xpo de categoria
    ''' </summary>
    ''' <returns></returns>
    Private Property CategoryXpo As XPInstantFeedbackSource
        Get
            Return INDSleCategory.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleCategory.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' propiedad para hacer el xpo de FinancialSourceXpo
    ''' </summary>
    ''' <returns></returns>
    Private Property FinancialSourceXpo As XPInstantFeedbackSource
        Get
            Return INDSleFinancialSource.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleFinancialSource.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' propiedad para hacer el xpo de RevenueType
    ''' </summary>
    ''' <returns></returns>

    Private Property RevenueTypeXpo As XPInstantFeedbackSource
        Get
            Return INDSleRevenueType.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleRevenueType.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "BarraBotones"
    ''' <summary>
    ''' Evento que carga la barra de permisos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

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
    ''' Metodo para seleccionar por defecto el primer registro si solo hay uno en vigencias
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetFirstOrDefaultValidity()
        If BudgetaryValidityXpo IsNot Nothing AndAlso BudgetaryValidityXpo.Count > 0 Then
            Dim item = (From l In BudgetaryValidityXpo Where l.Status = 2 Select l).FirstOrDefault
            If item IsNot Nothing Then
                INDsleValidity.EditValue = item.Id
                INDDnDate.SetYear = item.Year
                CleanControlsSelectValidity()
            End If
        End If
    End Sub
    ''' <summary>
    ''' limpia controles
    ''' </summary>
    Sub CleanControlsSelectValidity()
        INDsleValidity.Enabled = True
        INDDnDate.Enabled = True
        INDGleCodeToUse.Enabled = True
        INDGleIncludeZero.Enabled = True
        INDSleCategory.Enabled = True
        INDSleFinancialSource.Enabled = True
        INDSleRevenueType.Enabled = True
        INDSbGenerateReport.Enabled = True
        INDSbGenerateExcell.Enabled = True
    End Sub

    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim errors As New StringBuilder

        If INDsleValidity Is Nothing Then
            errors.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName", "Commons"), INDlciValidity.Text))
            Me.INDsleValidity.Focus()
        End If

        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Return False
        End If

        criterias = New Dictionary(Of String, String)
        criterias.Add("BudgetaryEntityId", INDsleEntity.EditValue)
        criterias.Add("BudgetaryValidityId", INDsleValidity.EditValue)
        criterias.Add("Year", INDDnDate.GetYear)
        criterias.Add("Month", INDDnDate.GetMonth)
        criterias.Add("CodeToUse", INDGleCodeToUse.EditValue)
        criterias.Add("IncludeZero", INDGleIncludeZero.EditValue)
        criterias.Add("Categories", _selectorCategory.GetKeys())
        criterias.Add("FinancialSources", _selectorFinancialSource.GetKeys())
        criterias.Add("RevenueTypes", _selectorRevenueType.GetKeys())

        Return True
    End Function

#Region "Selector"

    Private _selectorCategory As SelectorCache = New SelectorCache("Id", "Code")
    Private _selectorFinancialSource As SelectorCache = New SelectorCache("Id", "Code")
    Private _selectorRevenueType As SelectorCache = New SelectorCache("Id", "Code")
    ''' <summary>
    ''' evento cuando se customiza la columna de categoria
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvCategory_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvCategory.CustomUnboundColumnData, INDGvFinancialSource.CustomUnboundColumnData, INDGvRevenueType.CustomUnboundColumnData
        If e.IsGetData Then
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvCategory" Then
                e.Value = _selectorCategory.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvFinancialSource" Then
                e.Value = _selectorFinancialSource.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvRevenueType" Then
                e.Value = _selectorRevenueType.ValidateExistsRow(e.Row)
            End If
        End If
    End Sub
    ''' <summary>
    ''' se dispara cuando se da click a la celda RevenueType
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvCategory_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles INDGvCategory.RowCellClick, INDGvFinancialSource.RowCellClick, INDGvRevenueType.RowCellClick
        If e.Column.FieldName.Contains("UnboundSelection") Then
            Dim selector As SelectorCache = Nothing
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvCategory" Then
                selector = _selectorCategory
            ElseIf view.Name = "INDGvFinancialSource" Then
                selector = _selectorFinancialSource
            ElseIf view.Name = "INDGvRevenueType" Then
                selector = _selectorRevenueType
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
    ''' Se ejecuta cuandp se cierra los botones de category,financialSource y revenueType
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleCategory_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDSleCategory.Closed, INDSleFinancialSource.Closed, INDSleRevenueType.Closed
        Dim searchLookupEdit = TryCast(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        searchLookupEdit.EditValue = Nothing
        If searchLookupEdit.Name = "INDSleCategory" Then
            searchLookupEdit.Properties.NullText = _selectorCategory.ToString()
        ElseIf searchLookupEdit.Name = "INDSleFinancialSource" Then
            searchLookupEdit.Properties.NullText = _selectorFinancialSource.ToString()
        ElseIf searchLookupEdit.Name = "INDSleRevenueType" Then
            searchLookupEdit.Properties.NullText = _selectorRevenueType.ToString()
        End If
    End Sub

#End Region

#Region "ToExcel"
    ''' <summary>
    ''' Carga el datasource a excel
    ''' </summary>
    ''' <param name="dtReportMonthlyExpenseExecution"></param>
    Private Sub chargueDataSourceMonthlyExpenseExecution(ByVal dtReportMonthlyExpenseExecution As DataTable)
        Dim dt As New DataTable
        dt.Columns.Add("Codificación Presupuestal")
        dt.Columns.Add("Descripción")
        dt.Columns.Add("Fuente de Financiación")
        dt.Columns.Add("Tipo")
        dt.Columns.Add("Apropiación Vigente", GetType(Decimal))
        dt.Columns.Add("Compromisos Mes", GetType(Decimal))
        dt.Columns.Add("Compromisos Acumulados", GetType(Decimal))
        dt.Columns.Add("Obligaciones Mes", GetType(Decimal))
        dt.Columns.Add("Total Obligaciones Acumuladas", GetType(Decimal))
        dt.Columns.Add("Pagos Mes", GetType(Decimal))
        dt.Columns.Add("Total Pagos Acumulados", GetType(Decimal))

        For Each item In dtReportMonthlyExpenseExecution.Rows
            Dim row As DataRow = dt.NewRow
            row.Item("Codificación Presupuestal") = If(INDGleCodeToUse.EditValue = 1, item("CategoryCode"), item("AlternativeCode"))
            row.Item("Descripción") = item("CategoryName")
            row.Item("Fuente de Financiación") = String.Format("{0} -{1}", item("FinancialSourceCode"), item("FinancialSourceName"))
            row.Item("Tipo") = String.Format("{0} -{1}", item("RevenueTypeCode"), item("RevenueTypeName"))
            row.Item("Apropiación Vigente") = item("BudgetInitial") + item("BudgetTransferCredit") - item("BudgetTransferDebit") + item("BudgetModificationCredit") - item("BudgetModificationDebit")
            row.Item("Compromisos Mes") = item("CommitmentBalanceMonth")
            row.Item("Compromisos Acumulados") = item("CommitmentBalanceMonthPrevious") + item("CommitmentBalanceMonth")
            row.Item("Obligaciones Mes") = item("ObligationBalanceMonth")
            row.Item("Total Obligaciones Acumuladas") = item("ObligationBalanceMonthPrevious") + item("ObligationBalanceMonth")
            row.Item("Pagos Mes") = item("PaymentOrderBalanceMonth")
            row.Item("Total Pagos Acumulados") = item("PaymentOrderBalanceMonthPrevious") + item("PaymentOrderBalanceMonth")
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

#End Region

#Region "Events"

#Region "Load"
    ''' <summary>
    ''' Carga el frm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmReportTrialBalance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Cargar GridLookUpEdit        
        INDGleCodeToUse.Properties.DataSource = FillingCodeToUse
        INDGleIncludeZero.Properties.DataSource = FillingIncludeZero

        'Dar un valores por defecto
        INDGleCodeToUse.EditValue = 1
        INDGleIncludeZero.EditValue = False

        'Cargar
        Dim dateNow = Me.GetDateServer()
        INDDnDate.SetYear = Year(dateNow)
        INDDnDate.SetMonth = Month(dateNow)
    End Sub
    ''' <summary>
    ''' libera memoria al cerrar el frm 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _FillingCodeToUse = Nothing
        _FillingIncludeZero = Nothing
    End Sub

#End Region

#Region "QueryPopup"
    ''' <summary>
    ''' pop pup que genera la lista de entidades
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleEntity_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleEntity.QueryPopUp
        If BudgetaryEntityXpo Is Nothing Then
            BudgetaryEntityXpo = XpoServiceEx.Instance(indigo.TransactionalContainer).BudgetService.ListBudgetEntityByStatus(True)
        End If
    End Sub
    ''' <summary>
    ''' pop pup que genera la lista de categoria
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleCategory_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCategory.QueryPopUp
        If CategoryXpo Is Nothing Then
            CategoryXpo = XpoServiceEx.Instance(indigo.TransactionalContainer).BudgetService.ListBudgetCategoryByStatuAndValidityIdAndItemTypeAndFinancialSourceId(True, INDsleValidity.EditValue, 2)
        End If
    End Sub
    ''' <summary>
    ''' pop pup que genera la lista de FinancialSource
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleFinancialSource_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleFinancialSource.QueryPopUp
        If FinancialSourceXpo Is Nothing Then
            FinancialSourceXpo = XpoServiceEx.Instance(indigo.TransactionalContainer).BudgetService.ListFinancialSource(INDsleEntity.EditValue, INDsleValidity.EditValue)
        End If
    End Sub
    ''' <summary>
    ''' pop pup que genera la lista de RevenueType
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleRevenueType_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleRevenueType.QueryPopUp
        If RevenueTypeXpo Is Nothing Then
            RevenueTypeXpo = XpoServiceEx.Instance(indigo.TransactionalContainer).BudgetService.ListExpenseType(INDsleEntity.EditValue, INDsleValidity.EditValue)
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' evento para cargar resolucion , valor y estado.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleEntity_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleEntity.EditValueChanged
        If INDsleEntity.EditValue IsNot Nothing Then
            BudgetaryValidityXpo = XpoServiceEx.Instance(indigo.TransactionalContainer).BudgetService.ListValidityByBudgetEntityId(INDsleEntity.EditValue)
            SetFirstOrDefaultValidity()
        End If
    End Sub

    ''' <summary>
    ''' se dispara al cambiar el valor de la vigencia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleValidity_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleValidity.EditValueChanged
        If INDsleValidity.EditValue IsNot Nothing Then
            CleanControlsSelectValidity()
            Dim item = (From l In BudgetaryValidityXpo Where l.Id = INDsleValidity.EditValue Select l).FirstOrDefault
            If item IsNot Nothing Then
                INDDnDate.SetYear = item.Year
            End If
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
            AsyncLoader(True)
            Dim reporte As New rptReportMonthlyExpenseExecution()
            reporte.ParametrosReporte = New Object() {criterias}
            INDDvViewReport.DocumentSource = reporte
            Await reporte.CargarDataSourceAsync()
            AsyncLoader(False)
            If reporte.DataSource IsNot Nothing Then
                reporte.CreateDocument(True)
                Me.INDLcBase.Visible = False
                Me.INDCncNavigation.Visible = False
                Me.INDPcViewReport.Visible = True
                INDDvViewReport.Show()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                Me.INDDnDate.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento ClickBack en el control INDCnReturn
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnReturn_ClickBack() Handles INDCnReturn.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncNavigation.Visible = True
        Me.INDPcViewReport.Visible = False
    End Sub
    ''' <summary>
    ''' Evento al dar click al boton de excel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSbGenerateExcell_Click(sender As Object, e As EventArgs) Handles INDSbGenerateExcell.Click
        If Me.ValidateControlsReports = True Then
            Try
                AsyncLoader(True)
                Using model As New Presentation.Budget.MVP.MReports(Me.Tag)
                    Dim ds As DataSet = Await model.GetListReportMonthlyExpenseExecution(criterias)
                    If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                        Dim dtReportMonthlyExpenseExecution As DataTable = ds.Tables("ReportMonthlyExpenseExecution")

                        Await Task.Factory.StartNew(Sub()
                                                        chargueDataSourceMonthlyExpenseExecution(dtReportMonthlyExpenseExecution)
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