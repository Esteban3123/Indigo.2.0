#Region "Imports"

Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Cost.MVP
Imports Infrastructure.Data.Xpo.CostRepository
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports System.Text
Imports Presentation.Reporter

#End Region

Public Class FrmReportComparativeCosts
    Implements IReportComparativeCosts

#Region "Variables"

    ''' <summary>
    ''' Representa el presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim _presenter As PReportComparativeCosts

    Private _FillingTypeReport As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingTypeReport As List(Of Tuple(Of Integer, String))
        Get
            If _FillingTypeReport Is Nothing Then
                _FillingTypeReport = New List(Of Tuple(Of Integer, String))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(4, "Gastos Generales"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(1, "Mano de Obra"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(5, "Activos Fijos"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(2, "Dispensación"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(3, "Consumo"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(6, "Ventas"))
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

    Private ListCategoryIndex As New List(Of Integer)()
    Private ListProductionCenterIndex As New List(Of Integer)()

    Private _categoryIds As String
    Private _productionCenterIds As String

    Private criterias As Dictionary(Of String, String)
    Private filters As Dictionary(Of String, String)

#End Region

#Region "Properties"

    Public ReadOnly Property InitialRangeYearStart As Integer Implements IReportComparativeCosts.InitialRangeYearStart
        Get
            Return INDDnInitialRangeDateStart.GetYear
        End Get
    End Property

    Public ReadOnly Property InitialRangeMonthStart As Integer Implements IReportComparativeCosts.InitialRangeMonthStart
        Get
            Return INDDnInitialRangeDateStart.GetMonth
        End Get
    End Property

    Public ReadOnly Property InitialRangeYearEnd As Integer Implements IReportComparativeCosts.InitialRangeYearEnd
        Get
            Return INDDnInitialRangeDateEnd.GetYear
        End Get
    End Property

    Public ReadOnly Property InitialRangeMonthEnd As Integer Implements IReportComparativeCosts.InitialRangeMonthEnd
        Get
            Return INDDnInitialRangeDateEnd.GetMonth
        End Get
    End Property

    Public ReadOnly Property FinalRangeYearStart As Integer Implements IReportComparativeCosts.FinalRangeYearStart
        Get
            Return INDDnFinalRangeDateStart.GetYear
        End Get
    End Property

    Public ReadOnly Property FinalRangeMonthStart As Integer Implements IReportComparativeCosts.FinalRangeMonthStart
        Get
            Return INDDnFinalRangeDateStart.GetMonth
        End Get
    End Property

    Public ReadOnly Property FinalRangeYearEnd As Integer Implements IReportComparativeCosts.FinalRangeYearEnd
        Get
            Return INDDnFinalRangeDateEnd.GetYear
        End Get
    End Property

    Public ReadOnly Property FinalRangeMonthEnd As Integer Implements IReportComparativeCosts.FinalRangeMonthEnd
        Get
            Return INDDnFinalRangeDateEnd.GetMonth
        End Get
    End Property

    Public ReadOnly Property TypeReport As Integer Implements IReportComparativeCosts.TypeReport
        Get
            Return INDGleTypeReport.EditValue
        End Get
    End Property

    Public ReadOnly Property GroupBy As Integer Implements IReportComparativeCosts.GroupBy
        Get
            Return INDGleGroupBy.EditValue
        End Get
    End Property

    Public Property LevelMax As Integer Implements IReportComparativeCosts.LevelMax

    Public ReadOnly Property Level As Integer Implements IReportComparativeCosts.Level
        Get
            Return INDGleLevel.EditValue
        End Get
    End Property

    Public ReadOnly Property CenterType As String Implements IReportComparativeCosts.CenterType
        Get
            Return INDGleCenterType.EditValue
        End Get
    End Property

    Public ReadOnly Property CategoryIds As String Implements IReportComparativeCosts.CategoryIds
        Get
            Return _categoryIds
        End Get
    End Property

    Public ReadOnly Property ProductionCenterIds As String Implements IReportComparativeCosts.ProductionCenterIds
        Get
            Return _productionCenterIds
        End Get
    End Property

#End Region

#Region "XPO"

    Public Property CategoriesXpo As XPCollection(Of CostProductionCenterCategoryXpo) Implements IReportComparativeCosts.CategoriesXpo
        Get
            Return INDSleCategory.Properties.DataSource
        End Get
        Set(value As XPCollection(Of CostProductionCenterCategoryXpo))
            INDSleCategory.Properties.DataSource = value
        End Set
    End Property

    Public Property ProductionCentersXpo As XPCollection(Of CostProductionCenterXpo) Implements IReportComparativeCosts.ProductionCentersXpo
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
        _presenter = New PReportComparativeCosts(Me)
        _presenter.GetMaxLevelOrganizationalStructure()

        'Cargar GridLookUpEdit        
        INDGleTypeReport.Properties.DataSource = FillingTypeReport
        INDGleGroupBy.Properties.DataSource = FillingGroupBy
        INDGleLevel.Properties.DataSource = FillingLevel

        'Dar un valor por defecto a los GridLookEdit        
        INDGleTypeReport.EditValue = 4
        INDGleGroupBy.EditValue = 1
        INDGleLevel.EditValue = 1
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
            Dim reporte As Object
            Select Case TypeReport
                Case 1
                    reporte = New rptComparativeManpowerDistribution
                Case 2
                    reporte = New rptComparativeDispensingDistribution
                Case 3
                    reporte = New rptComparativeTransferDistribution
                Case 4
                    reporte = New rptComparativeCostDistribution
                Case 5
                    reporte = New rptComparativeFixedAssetDistribution
                Case 6
                    reporte = New rptComparativeSales
                Case Else
                    Exit Sub
            End Select

            AsyncLoader(True)
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
                Me.INDDnInitialRangeDateStart.Focus()
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
                    Dim ds As DataSet = Await model.GetReportComparativeCosts(criterias, filters)
                    If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                        Dim dtReportComparativeCosts As DataTable = ds.Tables("ReportComparativeCosts")

                        Select Case TypeReport
                            Case 1
                                chargueDatasourceManPowerDistribution(dtReportComparativeCosts)
                            Case 2
                                chargueDatasourceDispensingDistribution(dtReportComparativeCosts)
                            Case 3
                                chargueDatasourceTransferDistribution(dtReportComparativeCosts)
                            Case 4
                                chargueDatasourceCostDistribution(dtReportComparativeCosts)
                            Case 5
                                chargueDatasourceFixedAssetDistribution(dtReportComparativeCosts)
                            Case 6
                                chargueDatasourceSales(dtReportComparativeCosts)
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

        If InitialRangeYearEnd > InitialRangeYearStart OrElse (InitialRangeYearEnd = InitialRangeYearStart AndAlso InitialRangeMonthStart > InitialRangeMonthEnd) Then
            errors.AppendLine(String.Format(ResourceManager.GetString("CompareRangeDate", "Commons")))
        End If

        If FinalRangeYearEnd > FinalRangeYearStart OrElse (FinalRangeYearEnd = FinalRangeYearStart AndAlso FinalRangeMonthStart > FinalRangeMonthEnd) Then
            errors.AppendLine(String.Format(ResourceManager.GetString("CompareRangeDate", "Commons")))
        End If

        If String.IsNullOrEmpty(Me.INDGleCenterType.EditValue) Then
            errors.AppendLine("Debe seleccionar al menos un tipo de centro")
        End If

        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Return False
        End If

        criterias = New Dictionary(Of String, String)
        criterias.Add("InitialRangeYearStart", InitialRangeYearStart)
        criterias.Add("InitialRangeMonthStart", InitialRangeMonthStart)
        criterias.Add("InitialRangeYearEnd", InitialRangeYearEnd)
        criterias.Add("InitialRangeMonthEnd", InitialRangeMonthEnd)
        criterias.Add("FinalRangeYearStart", FinalRangeYearStart)
        criterias.Add("FinalRangeMonthStart", FinalRangeMonthStart)
        criterias.Add("FinalRangeYearEnd", FinalRangeYearEnd)
        criterias.Add("FinalRangeMonthEnd", FinalRangeMonthEnd)

        filters = New Dictionary(Of String, String)
        filters.Add("TypeReport", TypeReport)
        filters.Add("GroupBy", GroupBy)
        filters.Add("Level", Level)
        filters.Add("CenterType", CenterType)
        filters.Add("Categories", CategoryIds)
        filters.Add("ProductionCenters", ProductionCenterIds)

        Return True
    End Function

    ''' <summary>
    ''' creamos un datatable para generar el excel de mano de obra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargueDatasourceManPowerDistribution(ByVal dtReportComparativeCosts As DataTable)
        Dim dt As New DataTable
        If GroupBy = 3 Then
            dt.Columns.Add("Código Padre")
            dt.Columns.Add("Nombre Padre")
        End If
        dt.Columns.Add("Código")
        dt.Columns.Add("Nombre")
        dt.Columns.Add("Tipo de Centro")

        dt.Columns.Add("Tipo")
        dt.Columns.Add("Tercero Nit")
        dt.Columns.Add("Tercero Nombre")
        dt.Columns.Add("Cargo Código")
        dt.Columns.Add("Cargo Nombre")

        dt.Columns.Add("Rango Inicial: Devengado", GetType(Decimal))
        dt.Columns.Add("Rango Inicial: Seguridad Social Patrono", GetType(Decimal))
        dt.Columns.Add("Rango Inicial: Parafiscales", GetType(Decimal))
        dt.Columns.Add("Rango Inicial: Provisiones", GetType(Decimal))

        dt.Columns.Add("Rango Final: Devengado", GetType(Decimal))
        dt.Columns.Add("Rango Final: Seguridad Social Patrono", GetType(Decimal))
        dt.Columns.Add("Rango Final: Parafiscales", GetType(Decimal))
        dt.Columns.Add("Rango Final: Provisiones", GetType(Decimal))

        For Each item In dtReportComparativeCosts.Rows
            Dim row As DataRow = dt.NewRow()
            If GroupBy = 3 Then
                row.Item("Código Padre") = item("ParentCode")
                row.Item("Nombre Padre") = item("ParentName")
            End If
            row.Item("Código") = item("Code")
            row.Item("Nombre") = item("Name")
            row.Item("Tipo de Centro") = item("CenterTypeName")

            row.Item("Tipo") = item("ManpowerTypeName")
            row.Item("Tercero Nit") = item("ThirdPartyNit")
            row.Item("Tercero Nombre") = item("ThirdPartyName")
            row.Item("Cargo Código") = item("PositionCode")
            row.Item("Cargo Nombre") = item("PositionName")

            row.Item("Rango Inicial: Devengado") = item("InitialRangeAccruedValue")
            row.Item("Rango Inicial: Seguridad Social Patrono") = item("InitialRangeEmployerContributionValue")
            row.Item("Rango Inicial: Parafiscales") = item("InitialRangeParafiscalValue")
            row.Item("Rango Inicial: Provisiones") = item("InitialRangeProvisionValue")

            row.Item("Rango Final: Devengado") = item("FinalRangeAccruedValue")
            row.Item("Rango Final: Seguridad Social Patrono") = item("FinalRangeEmployerContributionValue")
            row.Item("Rango Final: Parafiscales") = item("FinalRangeParafiscalValue")
            row.Item("Rango Final: Provisiones") = item("FinalRangeProvisionValue")
            dt.Rows.Add(row)
        Next

        INDGcExportExcell.DataSource = dt
    End Sub

    ''' <summary>
    ''' creamos un datatable para generar el excel de suministros
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargueDatasourceDispensingDistribution(ByVal dtReportComparativeCosts As DataTable)
        Dim dt As New DataTable
        If GroupBy = 3 Then
            dt.Columns.Add("Código Padre")
            dt.Columns.Add("Nombre Padre")
        End If
        dt.Columns.Add("Código")
        dt.Columns.Add("Nombre")
        dt.Columns.Add("Tipo de Centro")

        dt.Columns.Add("Cuenta Contable Número")
        dt.Columns.Add("Cuenta Contable Nombre")
        dt.Columns.Add("Tercero Nit")
        dt.Columns.Add("Tercero Nombre")
        dt.Columns.Add("Rango Inicial", GetType(Decimal))
        dt.Columns.Add("Rango Final", GetType(Decimal))

        For Each item In dtReportComparativeCosts.Rows
            Dim row As DataRow = dt.NewRow()
            If GroupBy = 3 Then
                row.Item("Código Padre") = item("ParentCode")
                row.Item("Nombre Padre") = item("ParentName")
            End If
            row.Item("Código") = item("Code")
            row.Item("Nombre") = item("Name")
            row.Item("Tipo de Centro") = item("CenterTypeName")

            row.Item("Cuenta Contable Número") = item("AccountNumber")
            row.Item("Cuenta Contable Nombre") = item("AccountName")
            row.Item("Tercero Nit") = item("ThirdPartyNit")
            row.Item("Tercero Nombre") = item("ThirdPartyName")
            row.Item("Rango Inicial") = item("InitialRangeValue")
            row.Item("Rango Final") = item("FinalRangeValue")
            dt.Rows.Add(row)
        Next

        INDGcExportExcell.DataSource = dt
    End Sub

    ''' <summary>
    ''' creamos un datatable para generar el excel de consumo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargueDatasourceTransferDistribution(ByVal dtReportComparativeCosts As DataTable)
        Dim dt As New DataTable
        If GroupBy = 3 Then
            dt.Columns.Add("Código Padre")
            dt.Columns.Add("Nombre Padre")
        End If
        dt.Columns.Add("Código")
        dt.Columns.Add("Nombre")
        dt.Columns.Add("Tipo de Centro")

        dt.Columns.Add("Cuenta Contable Número")
        dt.Columns.Add("Cuenta Contable Nombre")
        dt.Columns.Add("Tercero Nit")
        dt.Columns.Add("Tercero Nombre")

        dt.Columns.Add("Rango Inicial", GetType(Decimal))
        dt.Columns.Add("Rango Final", GetType(Decimal))

        For Each item In dtReportComparativeCosts.Rows
            Dim row As DataRow = dt.NewRow()
            If GroupBy = 3 Then
                row.Item("Código Padre") = item("ParentCode")
                row.Item("Nombre Padre") = item("ParentName")
            End If
            row.Item("Código") = item("Code")
            row.Item("Nombre") = item("Name")
            row.Item("Tipo de Centro") = item("CenterTypeName")

            row.Item("Cuenta Contable Número") = item("AccountNumber")
            row.Item("Cuenta Contable Nombre") = item("AccountName")
            row.Item("Tercero Nit") = item("ThirdPartyNit")
            row.Item("Tercero Nombre") = item("ThirdPartyName")

            row.Item("Rango Inicial") = item("InitialRangeValue")
            row.Item("Rango Final") = item("FinalRangeValue")
            dt.Rows.Add(row)
        Next

        INDGcExportExcell.DataSource = dt
    End Sub

    ''' <summary>
    ''' creamos un datatable para generar el excel de gastos generales
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargueDatasourceCostDistribution(ByVal dtReportComparativeCosts As DataTable)
        Dim dt As New DataTable
        If GroupBy = 3 Then
            dt.Columns.Add("Código Padre")
            dt.Columns.Add("Nombre Padre")
        End If
        dt.Columns.Add("Código")
        dt.Columns.Add("Nombre")
        dt.Columns.Add("Tipo de Centro")

        dt.Columns.Add("Cuenta Contable Número")
        dt.Columns.Add("Cuenta Contable Nombre")
        dt.Columns.Add("Tercero Nit")
        dt.Columns.Add("Tercero Nombre")

        dt.Columns.Add("Rango Inicial", GetType(Decimal))
        dt.Columns.Add("Rango Final", GetType(Decimal))

        For Each item In dtReportComparativeCosts.Rows
            Dim row As DataRow = dt.NewRow()
            If GroupBy = 3 Then
                row.Item("Código Padre") = item("ParentCode")
                row.Item("Nombre Padre") = item("ParentName")
            End If
            row.Item("Código") = item("Code")
            row.Item("Nombre") = item("Name")
            row.Item("Tipo de Centro") = item("CenterTypeName")

            row.Item("Cuenta Contable Número") = item("AccountNumber")
            row.Item("Cuenta Contable Nombre") = item("AccountName")
            row.Item("Tercero Nit") = item("ThirdPartyNit")
            row.Item("Tercero Nombre") = item("ThirdPartyName")

            row.Item("Rango Inicial") = item("InitialRangeValue")
            row.Item("Rango Final") = item("FinalRangeValue")
            dt.Rows.Add(row)
        Next

        INDGcExportExcell.DataSource = dt
    End Sub

    ''' <summary>
    ''' creamos un datatable para generar el excel de activos fijos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargueDatasourceFixedAssetDistribution(ByVal dtReportComparativeCosts As DataTable)
        Dim dt As New DataTable
        If GroupBy = 3 Then
            dt.Columns.Add("Código Padre")
            dt.Columns.Add("Nombre Padre")
        End If
        dt.Columns.Add("Código")
        dt.Columns.Add("Nombre")
        dt.Columns.Add("Tipo de Centro")

        dt.Columns.Add("Placa")
        dt.Columns.Add("Artículo Código")
        dt.Columns.Add("Artículo Nombre")

        dt.Columns.Add("Rango Inicial", GetType(Decimal))
        dt.Columns.Add("Rango Final", GetType(Decimal))

        For Each item In dtReportComparativeCosts.Rows
            Dim row As DataRow = dt.NewRow()
            If GroupBy = 3 Then
                row.Item("Código Padre") = item("ParentCode")
                row.Item("Nombre Padre") = item("ParentName")
            End If
            row.Item("Código") = item("Code")
            row.Item("Nombre") = item("Name")
            row.Item("Tipo de Centro") = item("CenterTypeName")

            row.Item("Placa") = item("PhysicalAssetPlate")
            row.Item("Artículo Código") = item("ItemCode")
            row.Item("Artículo Nombre") = item("ItemDescription")

            row.Item("Rango Inicial") = item("InitialRangeValue")
            row.Item("Rango Final") = item("FinalRangeValue")
            dt.Rows.Add(row)
        Next

        INDGcExportExcell.DataSource = dt
    End Sub

    ''' <summary>
    ''' creamos un datatable para generar el excel de ventas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargueDatasourceSales(ByVal dtReportComparativeCosts As DataTable)
        Dim dt As New DataTable
        If GroupBy = 3 Then
            dt.Columns.Add("Código Padre")
            dt.Columns.Add("Nombre Padre")
        End If
        dt.Columns.Add("Código")
        dt.Columns.Add("Nombre")
        dt.Columns.Add("Tipo de Centro")

        dt.Columns.Add("Cuenta Contable Número")
        dt.Columns.Add("Cuenta Contable Nombre")
        dt.Columns.Add("Tercero Nit")
        dt.Columns.Add("Tercero Nombre")

        dt.Columns.Add("Rango Inicial", GetType(Decimal))
        dt.Columns.Add("Rango Final", GetType(Decimal))

        For Each item In dtReportComparativeCosts.Rows
            Dim row As DataRow = dt.NewRow()
            If GroupBy = 3 Then
                row.Item("Código Padre") = item("ParentCode")
                row.Item("Nombre Padre") = item("ParentName")
            End If
            row.Item("Código") = item("Code")
            row.Item("Nombre") = item("Name")
            row.Item("Tipo de Centro") = item("CenterTypeName")

            row.Item("Cuenta Contable Número") = item("AccountNumber")
            row.Item("Cuenta Contable Nombre") = item("AccountName")
            row.Item("Tercero Nit") = item("ThirdPartyNit")
            row.Item("Tercero Nombre") = item("ThirdPartyName")

            row.Item("Rango Inicial") = item("InitialRangeValue")
            row.Item("Rango Final") = item("FinalRangeValue")
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