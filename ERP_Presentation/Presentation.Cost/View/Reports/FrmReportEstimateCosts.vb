#Region "Imports"

Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Cost.MVP
Imports Presentation.Controls.MVP
Imports Presentation.Reporter
Imports Infrastructure.Data.Xpo.CostRepository
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports System.Text

#End Region

Public Class FrmReportEstimateCosts
    Implements IReportEstimateCosts

#Region "Variables"

    ''' <summary>
    ''' Representa el presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim _presenter As PReportEstimateCosts

    Private _FillingTypeReport As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingTypeReport As List(Of Tuple(Of Integer, String))
        Get
            If _FillingTypeReport Is Nothing Then
                _FillingTypeReport = New List(Of Tuple(Of Integer, String))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(1, "Estimación Primaria Resumida"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(2, "Estimación Primaria Detallada"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(3, "Estimación Secundaria Resumida"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(4, "Estimación Secundaria Detallada"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(5, "Estimación Final Resumida"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(6, "Estimación Final Detallada"))
            End If
            Return _FillingTypeReport
        End Get
    End Property

    Private _FillingGroupBy As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingGroupBy As List(Of Tuple(Of Integer, String))
        Get
            If _FillingGroupBy Is Nothing Then
                _FillingGroupBy = New List(Of Tuple(Of Integer, String))
                _FillingGroupBy.Add(New Tuple(Of Integer, String)(1, "Centro de Producción"))
                _FillingGroupBy.Add(New Tuple(Of Integer, String)(2, "Categoría"))
                _FillingGroupBy.Add(New Tuple(Of Integer, String)(3, "Estructura Organizacional"))
            End If
            Return _FillingGroupBy
        End Get
    End Property

    Private _FillingLevel As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingLevel As List(Of Tuple(Of Integer, String))
        Get
            If _FillingLevel Is Nothing Then
                _FillingLevel = New List(Of Tuple(Of Integer, String))
                For i = 1 To _LevelMax
                    _FillingLevel.Add(New Tuple(Of Integer, String)(i, i.ToString))
                Next
            End If
            Return _FillingLevel
        End Get
    End Property

    Private _FillingSecondaryDetailType As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingSecondaryDetailType As List(Of Tuple(Of Integer, String))
        Get
            If _FillingSecondaryDetailType Is Nothing Then
                _FillingSecondaryDetailType = New List(Of Tuple(Of Integer, String))
                _FillingSecondaryDetailType.Add(New Tuple(Of Integer, String)(1, "Origen No Operativos"))
                _FillingSecondaryDetailType.Add(New Tuple(Of Integer, String)(2, "Origen Operativos"))
            End If
            Return _FillingSecondaryDetailType
        End Get
    End Property

    Private _Fillingvisualization As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property Fillingvisualization As List(Of Tuple(Of Integer, String))
        Get
            If _Fillingvisualization Is Nothing Then
                _Fillingvisualization = New List(Of Tuple(Of Integer, String))
                _Fillingvisualization.Add(New Tuple(Of Integer, String)(1, "General"))
                _Fillingvisualization.Add(New Tuple(Of Integer, String)(2, "Grafica"))
            End If
            Return _Fillingvisualization
        End Get
    End Property

    Private ListCategoryIndex As New List(Of Integer)()
    Private ListProductionCenterIndex As New List(Of Integer)()

    Private _categoryIds As String
    Private _productionCenterIds As String

    Private criterias As Dictionary(Of String, String)
    Private filters As Dictionary(Of String, String)

#End Region

#Region "Properties"

    Public ReadOnly Property YearStart As Integer Implements IReportEstimateCosts.YearStart
        Get
            Return INDDnYearMonthStart.GetYear
        End Get
    End Property

    Public ReadOnly Property MonthStart As Integer Implements IReportEstimateCosts.MonthStart
        Get
            Return INDDnYearMonthStart.GetMonth
        End Get
    End Property

    Public ReadOnly Property YearEnd As Integer Implements IReportEstimateCosts.YearEnd
        Get
            Return INDDnYearMonthEnd.GetYear
        End Get
    End Property

    Public ReadOnly Property MonthEnd As Integer Implements IReportEstimateCosts.MonthEnd
        Get
            Return INDDnYearMonthEnd.GetMonth
        End Get
    End Property

    Public ReadOnly Property TypeReport As Integer Implements IReportEstimateCosts.TypeReport
        Get
            Return INDGleTypeReport.EditValue
        End Get
    End Property

    Public ReadOnly Property GroupBy As Integer Implements IReportEstimateCosts.GroupBy
        Get
            Return INDGleGroupBy.EditValue
        End Get
    End Property

    Public Property LevelMax As Integer Implements IReportEstimateCosts.LevelMax

    Public ReadOnly Property Level As Integer Implements IReportEstimateCosts.Level
        Get
            Return INDGleLevel.EditValue
        End Get
    End Property

    Public ReadOnly Property SecondaryDetailType As Integer Implements IReportEstimateCosts.SecondaryDetailType
        Get
            Return INDGleSecondaryDetailType.EditValue
        End Get
    End Property

    Public ReadOnly Property Visualization As Integer Implements IReportEstimateCosts.Visualization
        Get
            Return INDGleVisualization.EditValue
        End Get
    End Property

    Public ReadOnly Property CenterType As String Implements IReportEstimateCosts.CenterType
        Get
            Return INDGleCenterType.EditValue
        End Get
    End Property

    Public ReadOnly Property CategoryIds As String Implements IReportEstimateCosts.CategoryIds
        Get
            Return _categoryIds
        End Get
    End Property

    Public ReadOnly Property ProductionCenterIds As String Implements IReportEstimateCosts.ProductionCenterIds
        Get
            Return _productionCenterIds
        End Get
    End Property

    Public ReadOnly Property DetailType As String Implements IReportEstimateCosts.DetailType
        Get
            Return INDCcbeDetailType.EditValue
        End Get
    End Property

#End Region

#Region "XPO"

    Public Property CategoriesXpo As XPCollection(Of CostProductionCenterCategoryXpo) Implements IReportEstimateCosts.CategoriesXpo
        Get
            Return INDSleCategory.Properties.DataSource
        End Get
        Set(value As XPCollection(Of CostProductionCenterCategoryXpo))
            INDSleCategory.Properties.DataSource = value
        End Set
    End Property

    Public Property ProductionCentersXpo As XPCollection(Of CostProductionCenterXpo) Implements IReportEstimateCosts.ProductionCentersXpo
        Get
            Return INDSleProductionCenter.Properties.DataSource
        End Get
        Set(value As XPCollection(Of CostProductionCenterXpo))
            INDSleProductionCenter.Properties.DataSource = value
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
        'Inicializamos la referencia
        _presenter = New PReportEstimateCosts(Me)
        _presenter.GetMaxLevelOrganizationalStructure()

        'Cargar GridLookUpEdit        
        INDGleTypeReport.Properties.DataSource = FillingTypeReport
        INDGleGroupBy.Properties.DataSource = FillingGroupBy
        INDGleLevel.Properties.DataSource = FillingLevel
        INDGleSecondaryDetailType.Properties.DataSource = FillingSecondaryDetailType
        INDGleVisualization.Properties.DataSource = Fillingvisualization

        'Dar un valor por defecto a los GridLookEdit        
        INDGleTypeReport.EditValue = 1
        INDGleGroupBy.EditValue = 1
        INDGleLevel.EditValue = 1
        INDGleSecondaryDetailType.EditValue = 1
        INDGleVisualization.EditValue = 1
    End Sub

    ''' <summary>
    ''' Se ejecuta al cerrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _presenter = Nothing

        ListCategoryIndex = Nothing
        _categoryIds = Nothing
        ListProductionCenterIndex = Nothing
        _productionCenterIds = Nothing
    End Sub

#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleCategory
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleCategory_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCategory.QueryPopUp
        If INDSleCategory.Properties.DataSource Is Nothing Then
            _presenter.ListCollectionCategories()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleProductionCenter
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleProductionCenter_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleProductionCenter.QueryPopUp
        If INDSleProductionCenter.Properties.DataSource Is Nothing Then
            _presenter.ListCollectionProductionCenters()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDGleTypeReport_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleTypeReport.EditValueChanged
        If TypeReport = 2 OrElse TypeReport = 6 Then
            INDLciDetailType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDLciDetailType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If

        If TypeReport = 4 Then
            INDLciSecondaryDetailType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDLciSecondaryDetailType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    Private Sub INDGleGroupBy_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleGroupBy.EditValueChanged
        If GroupBy = 3 Then
            INDLciLevel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDLciLevel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

#End Region

#Region "MouseDown"

    ''' <summary>
    ''' Evento que se dispara al checkear los items de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDgvCategory_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles INDgvCategory.MouseDown, INDgvProductionCenter.MouseDown
        Dim view As GridView = TryCast(sender, GridView)
        Dim hi As GridHitInfo = view.CalcHitInfo(e.Location)
        If hi.Column IsNot Nothing Then
            If hi.Column.FieldName = "DX$CheckboxSelectorColumn" Then
                If view.Name = "INDgvCategory" Then
                    UpdateList(view, hi, ListCategoryIndex)
                ElseIf view.Name = "INDgvProductionCenter" Then
                    UpdateList(view, hi, ListProductionCenterIndex)
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
    Private Sub INDgvCategory_ColumnFilterChanged(sender As Object, e As EventArgs) Handles INDgvCategory.ColumnFilterChanged, INDgvProductionCenter.ColumnFilterChanged
        RestoreSelection(TryCast(sender, GridView))
    End Sub

#End Region

#Region "CloseUp"

    Private Sub INDSleCategory_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDSleCategory.CloseUp
        _categoryIds = RecuperarSeleccionados(sender, "Id", "CodeName")
    End Sub

    Private Sub INDSleProductionCenter_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDSleProductionCenter.CloseUp
        _productionCenterIds = RecuperarSeleccionados(sender, "Id", "CodeName")
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

            Dim reporte As Object
            Select Case TypeReport
                Case 1
                    reporte = New rptEstimateCostsPrimarySummary
                Case 2
                    reporte = New rptEstimateCostsPrimaryDetailed
                Case 3
                    reporte = New rptEstimateCostsSecondarySummary
                Case 4
                    reporte = New rptEstimateCostsSecondaryDetailed
                Case 5
                    reporte = New rptEstimateCostsFinalSummary
                Case 6
                    reporte = New rptEstimateCostsFinalDetailed
                Case Else
                    Exit Sub
            End Select

            reporte.ParametrosReporte = New Object() {criterias, filters}
            INDDvReport.DocumentSource = reporte
            Await reporte.CargarDataSourceAsync()
            AsyncLoader(False)
            If reporte.DataSource IsNot Nothing Then
                reporte.CreateDocument(True)
                Me.INDLcBase.Visible = False
                Me.INDNcpReport.Visible = False
                Me.INDPcReport.Visible = True
                INDDvReport.Show()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                Me.INDDnYearMonthStart.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta al dar click en el control INDCnBack
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnBack_ClickBack() Handles INDCnReport.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDNcpReport.Visible = True
        Me.INDPcReport.Visible = False
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
                    Dim ds As DataSet = Await model.GetReportEstimateCosts(criterias, filters)
                    If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                        Dim dtReportEstimateCosts As DataTable = ds.Tables("ReportEstimateCosts")

                        Select Case TypeReport
                            Case 1
                                chargueDatasourcePrimarySummary(dtReportEstimateCosts)
                            Case 2
                                chargueDatasourcePrimaryDetailed(dtReportEstimateCosts)
                            Case 3
                                chargueDatasourceSecondarySummary(dtReportEstimateCosts)
                            Case 4
                                chargueDatasourceSecondaryDetailed(dtReportEstimateCosts)
                            Case 5
                                chargueDatasourceFinalSummary(dtReportEstimateCosts)
                            Case 6
                                chargueDatasourceFinalDetailed(dtReportEstimateCosts)
                            Case Else
                                Exit Sub
                        End Select

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

#Region "Method"

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
        If view.Name = "INDgvCategory" Then
            BeginInvoke(New Action(Sub()
                                       Dim i As Integer = 0
                                       Do While i < ListCategoryIndex.Count
                                           view.SelectRow(view.GetRowHandle(ListCategoryIndex(i)))
                                           i += 1
                                       Loop
                                   End Sub))
        ElseIf view.Name = "INDgvProductionCenter" Then
            BeginInvoke(New Action(Sub()
                                       Dim i As Integer = 0
                                       Do While i < ListProductionCenterIndex.Count
                                           view.SelectRow(view.GetRowHandle(ListProductionCenterIndex(i)))
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

        If YearEnd > YearStart OrElse (YearEnd = YearStart AndAlso MonthStart > MonthEnd) Then
            errors.AppendLine(String.Format(ResourceManager.GetString("CompareRangeDate", "Commons")))
        End If

        If String.IsNullOrEmpty(Me.INDGleCenterType.EditValue) Then
            errors.AppendLine("Debe seleccionar al menos un tipo de centro")
        End If

        If TypeReport = 2 OrElse TypeReport = 6 Then
            If String.IsNullOrEmpty(Me.INDCcbeDetailType.EditValue) Then
                errors.AppendLine("Debe seleccionar al menos un tipo de detalle")
            End If
        End If

        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Return False
        End If

        criterias = New Dictionary(Of String, String)
        criterias.Add("YearStart", YearStart)
        criterias.Add("MonthStart", MonthStart)
        criterias.Add("YearEnd", YearEnd)
        criterias.Add("MonthEnd", MonthEnd)
        criterias.Add("TypeReport", TypeReport)
        criterias.Add("GroupBy", GroupBy)
        criterias.Add("Level", Level)
        criterias.Add("Visualization", Visualization)
        criterias.Add("SecondaryDetailType", SecondaryDetailType)

        filters = New Dictionary(Of String, String)
        filters.Add("CenterType", CenterType)
        filters.Add("Categories", CategoryIds)
        filters.Add("ProductionCenters", ProductionCenterIds)
        filters.Add("DetailType", DetailType)

        Return True
    End Function

    ''' <summary>
    ''' creamos un datatable para generar el excel resumido
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargueDatasourcePrimarySummary(ByVal dtReportEstimateCosts As DataTable)
        Dim dt As New DataTable
        If GroupBy = 3 Then
            dt.Columns.Add("Código Padre")
            dt.Columns.Add("Nombre Padre")
        End If
        dt.Columns.Add("Código")
        dt.Columns.Add("Nombre")
        dt.Columns.Add("Tipo de Centro")
        dt.Columns.Add("Gastos Directos", GetType(Decimal))
        dt.Columns.Add("Gastos Variables", GetType(Decimal))
        dt.Columns.Add("Mano de Obra Directa", GetType(Decimal))
        dt.Columns.Add("Mano de Obra Indirecta", GetType(Decimal))
        dt.Columns.Add("Activos Fijos", GetType(Decimal))
        dt.Columns.Add("Dispensación", GetType(Decimal))
        dt.Columns.Add("Consumo", GetType(Decimal))
        dt.Columns.Add("Total", GetType(Decimal))
        dt.Columns.Add("Ventas", GetType(Decimal))

        For Each item In dtReportEstimateCosts.Rows
            Dim row As DataRow = dt.NewRow()
            If GroupBy = 3 Then
                row.Item("Código Padre") = item("ParentCode")
                row.Item("Nombre Padre") = item("ParentName")
            End If
            row.Item("Código") = item("Code")
            row.Item("Nombre") = item("Name")
            row.Item("Tipo de Centro") = item("CenterTypeName")
            row.Item("Gastos Directos") = item("DirectCostDistribution")
            row.Item("Gastos Variables") = item("AutoCostDistribution")
            row.Item("Mano de Obra Directa") = item("ManPowerDistributionDirect")
            row.Item("Mano de Obra Indirecta") = item("ManPowerDistributionInDirect")
            row.Item("Activos Fijos") = item("FixedAssetDistribution")
            row.Item("Dispensación") = item("DispensingDistribution")
            row.Item("Consumo") = item("TransferDistribution")
            row.Item("Total") = item("InitialDistribution")
            row.Item("Ventas") = item("TotalSales")
            dt.Rows.Add(row)
        Next

        INDGcExportExcell.DataSource = dt
    End Sub

    ''' <summary>
    ''' creamos un datatable para generar el excel detallado
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargueDatasourcePrimaryDetailed(ByVal dtReportEstimateCosts As DataTable)
        Dim dt As New DataTable
        If GroupBy = 3 Then
            dt.Columns.Add("Código Padre")
            dt.Columns.Add("Nombre Padre")
        End If
        dt.Columns.Add("Código")
        dt.Columns.Add("Nombre")
        dt.Columns.Add("Tipo de Centro")
        dt.Columns.Add("Tipo de Detalle")
        dt.Columns.Add("Cuenta Contable Número")
        dt.Columns.Add("Cuenta Contable Nombre")
        dt.Columns.Add("Tercero Nit")
        dt.Columns.Add("Tercero Nombre")
        dt.Columns.Add("Valor", GetType(Decimal))

        For Each item In dtReportEstimateCosts.Rows
            Dim row As DataRow = dt.NewRow()
            If GroupBy = 3 Then
                row.Item("Código Padre") = item("ParentCode")
                row.Item("Nombre Padre") = item("ParentName")
            End If
            row.Item("Código") = item("Code")
            row.Item("Nombre") = item("Name")
            row.Item("Tipo de Centro") = item("CenterTypeName")
            row.Item("Tipo de Detalle") = item("HomologationTypeName")
            row.Item("Cuenta Contable Número") = item("AccountNumber")
            row.Item("Cuenta Contable Nombre") = item("AccountName")
            row.Item("Tercero Nit") = item("ThirdPartyNit")
            row.Item("Tercero Nombre") = item("ThirdPartyName")
            row.Item("Valor") = item("Value")
            dt.Rows.Add(row)
        Next

        INDGcExportExcell.DataSource = dt
    End Sub

    ''' <summary>
    ''' creamos un datatable para generar el excel resumido
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargueDatasourceSecondarySummary(ByVal dtReportEstimateCosts As DataTable)
        Dim dt As New DataTable
        If GroupBy = 3 Then
            dt.Columns.Add("Código Padre")
            dt.Columns.Add("Nombre Padre")
        End If
        dt.Columns.Add("Código")
        dt.Columns.Add("Nombre")
        dt.Columns.Add("Tipo de Centro")
        dt.Columns.Add("Distribución Inicial", GetType(Decimal))
        dt.Columns.Add("Gastos Directos", GetType(Decimal))
        dt.Columns.Add("Gastos Variables", GetType(Decimal))
        dt.Columns.Add("Mano de Obra Directa", GetType(Decimal))
        dt.Columns.Add("Mano de Obra Indirecta", GetType(Decimal))
        dt.Columns.Add("Activos Fijos", GetType(Decimal))
        dt.Columns.Add("Dispensación", GetType(Decimal))
        dt.Columns.Add("Consumo", GetType(Decimal))
        dt.Columns.Add("Total", GetType(Decimal))
        dt.Columns.Add("Ventas", GetType(Decimal))

        For Each item In dtReportEstimateCosts.Rows
            Dim row As DataRow = dt.NewRow()
            If GroupBy = 3 Then
                row.Item("Código Padre") = item("ParentCode")
                row.Item("Nombre Padre") = item("ParentName")
            End If
            row.Item("Código") = item("Code")
            row.Item("Nombre") = item("Name")
            row.Item("Tipo de Centro") = item("CenterTypeName")
            row.Item("Distribución Inicial") = item("InitialDistribution")
            row.Item("Gastos Directos") = item("SecondaryDirectCostDistribution")
            row.Item("Gastos Variables") = item("SecondaryAutoCostDistribution")
            row.Item("Mano de Obra Directa") = item("SecondaryManPowerDistributionDirect")
            row.Item("Mano de Obra Indirecta") = item("SecondaryManPowerDistributionInDirect")
            row.Item("Activos Fijos") = item("SecondaryFixedAssetDistribution")
            row.Item("Dispensación") = item("SecondaryDispensingDistribution")
            row.Item("Consumo") = item("SecondaryTransferDistribution")
            row.Item("Total") = item("SecondaryDistribution")
            row.Item("Ventas") = item("TotalSales")
            dt.Rows.Add(row)
        Next

        INDGcExportExcell.DataSource = dt
    End Sub

    ''' <summary>
    ''' creamos un datatable para generar el excel detallado
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargueDatasourceSecondaryDetailed(ByVal dtReportEstimateCosts As DataTable)
        Dim dt As New DataTable
        If GroupBy = 3 Then
            dt.Columns.Add("Código Padre")
            dt.Columns.Add("Nombre Padre")
        End If
        dt.Columns.Add("Código Origen")
        dt.Columns.Add("Nombre Origen")
        dt.Columns.Add("Tipo de Centro Origen")
        dt.Columns.Add("Distribucion Inicial Origen")
        dt.Columns.Add("Código Destino")
        dt.Columns.Add("Nombre Destino")
        dt.Columns.Add("Tipo de Centro Destino")
        dt.Columns.Add("Valor", GetType(Decimal))

        For Each item In dtReportEstimateCosts.Rows
            Dim row As DataRow = dt.NewRow()
            If GroupBy = 3 Then
                row.Item("Código Padre") = item("ParentCode")
                row.Item("Nombre Padre") = item("ParentName")
            End If
            row.Item("Código Origen") = item("SourceCode")
            row.Item("Nombre Origen") = item("SourceName")
            row.Item("Tipo de Centro Origen") = item("SourceCenterTypeName")
            row.Item("Distribucion Inicial Origen") = item("SourceInitialDistribution")
            row.Item("Código Destino") = item("TargetCode")
            row.Item("Nombre Destino") = item("TargetName")
            row.Item("Tipo de Centro Destino") = item("TargetCenterTypeName")
            row.Item("Valor") = item("Value")
            dt.Rows.Add(row)
        Next

        INDGcExportExcell.DataSource = dt
    End Sub

    ''' <summary>
    ''' creamos un datatable para generar el excel resumido
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargueDatasourceFinalSummary(ByVal dtReportEstimateCosts As DataTable)
        Dim dt As New DataTable
        If GroupBy = 3 Then
            dt.Columns.Add("Código Padre")
            dt.Columns.Add("Nombre Padre")
        End If
        dt.Columns.Add("Código")
        dt.Columns.Add("Nombre")
        dt.Columns.Add("Tipo de Centro")
        dt.Columns.Add("Gastos Directos", GetType(Decimal))
        dt.Columns.Add("Gastos Variables", GetType(Decimal))
        dt.Columns.Add("Mano de Obra Directa", GetType(Decimal))
        dt.Columns.Add("Mano de Obra Indirecta", GetType(Decimal))
        dt.Columns.Add("Activos Fijos", GetType(Decimal))
        dt.Columns.Add("Dispensación", GetType(Decimal))
        dt.Columns.Add("Consumo", GetType(Decimal))
        dt.Columns.Add("Distribución Primaria", GetType(Decimal))
        dt.Columns.Add("Ingresos distribucion primaria", GetType(Decimal))
        dt.Columns.Add("Distribución Secundaria", GetType(Decimal))
        dt.Columns.Add("Ingresos distribucion secundaria", GetType(Decimal))
        dt.Columns.Add("% Distribucion secundaria", GetType(String))
        dt.Columns.Add("Costo Total", GetType(Decimal))
        dt.Columns.Add("Facturado", GetType(Decimal))
        dt.Columns.Add("Perdida / Ganancia", GetType(Decimal))
        dt.Columns.Add("% Margen", GetType(Decimal))

        Dim totalSecondaryDistribution = Convert.ToDecimal(dtReportEstimateCosts.Compute("SUM(TotalSalesSecondary)", String.Empty))

        For Each item In dtReportEstimateCosts.Rows
            Dim row As DataRow = dt.NewRow()
            If GroupBy = 3 Then
                row.Item("Código Padre") = item("ParentCode")
                row.Item("Nombre Padre") = item("ParentName")
            End If
            row.Item("Código") = item("Code")
            row.Item("Nombre") = item("Name")
            row.Item("Tipo de Centro") = item("CenterTypeName")
            row.Item("Gastos Directos") = item("DirectCostDistribution")
            row.Item("Gastos Variables") = item("AutoCostDistribution")
            row.Item("Mano de Obra Directa") = item("ManPowerDistributionDirect")
            row.Item("Mano de Obra Indirecta") = item("ManPowerDistributionInDirect")
            row.Item("Activos Fijos") = item("FixedAssetDistribution")
            row.Item("Dispensación") = item("DispensingDistribution")
            row.Item("Consumo") = item("TransferDistribution")
            row.Item("Distribución Primaria") = item("InitialDistribution")
            row.Item("Ingresos distribucion primaria") = item("TotalSales")
            row.Item("Distribución Secundaria") = item("SecondaryDistribution")
            row.Item("Ingresos distribucion secundaria") = item("TotalSalesSecondary")
            row.Item("% Distribucion secundaria") = If(totalSecondaryDistribution = 0, 0, Math.Round((Convert.ToDecimal(item("TotalSalesSecondary")) / totalSecondaryDistribution) * 100.0, 2)) & "%"
            row.Item("Costo Total") = item("TotalDistribution")
            row.Item("Facturado") = item("TotalSales")
            row.Item("Perdida / Ganancia") = item("AbsoluteProfitabilityMargin")
            row.Item("% Margen") = item("ProfitabilityMargin")
            dt.Rows.Add(row)
        Next

        INDGcExportExcell.DataSource = dt
    End Sub

    ''' <summary>
    ''' creamos un datatable para generar el excel detallado
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargueDatasourceFinalDetailed(ByVal dtReportEstimateCosts As DataTable)
        Dim dt As New DataTable
        If GroupBy = 3 Then
            dt.Columns.Add("Código Padre")
            dt.Columns.Add("Nombre Padre")
        End If
        dt.Columns.Add("Código")
        dt.Columns.Add("Nombre")
        dt.Columns.Add("Tipo de Centro")
        dt.Columns.Add("Tipo de Detalle")
        dt.Columns.Add("Cuenta Contable Número")
        dt.Columns.Add("Cuenta Contable Nombre")
        dt.Columns.Add("Tercero Nit")
        dt.Columns.Add("Tercero Nombre")
        dt.Columns.Add("Valor", GetType(Decimal))

        For Each item In dtReportEstimateCosts.Rows
            Dim row As DataRow = dt.NewRow()
            If GroupBy = 3 Then
                row.Item("Código Padre") = item("ParentCode")
                row.Item("Nombre Padre") = item("ParentName")
            End If
            row.Item("Código") = item("Code")
            row.Item("Nombre") = item("Name")
            row.Item("Tipo de Centro") = item("CenterTypeName")
            row.Item("Tipo de Detalle") = item("HomologationTypeName")
            row.Item("Cuenta Contable Número") = item("AccountNumber")
            row.Item("Cuenta Contable Nombre") = item("AccountName")
            row.Item("Tercero Nit") = item("ThirdPartyNit")
            row.Item("Tercero Nombre") = item("ThirdPartyName")
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