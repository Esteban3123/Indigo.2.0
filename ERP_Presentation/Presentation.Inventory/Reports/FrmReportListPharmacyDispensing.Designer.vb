Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmReportListPharmacyDispensing
    Inherits FormBase


    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim AppearanceObject7 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject8 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmReportListPharmacyDispensing))
        Me.INDLcBase = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSbReport = New DevExpress.XtraEditors.SimpleButton()
        Me.INDGlueTypeReport = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDDateEditDateEnd = New DevExpress.XtraEditors.DateEdit()
        Me.INDDateEditDateStart = New DevExpress.XtraEditors.DateEdit()
        Me.INDSleWarehouse = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvWarehouse = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColSourceWareHouse_UnboundSelection = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColSourceWarehouseCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColSourceWarehouseName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleProduct = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvProduct = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColProduct_UnboundSelection = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColProductCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColProductName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColProductType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColProductSubGroup = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColProductGroup = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleDocument = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvDocument = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColDocument_UnboundSelection = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColDocumentCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColDocumentName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColDocumentDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColDocumentState = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDLcgBase = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcgCriteria = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciDateStart = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciTypeReport = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciDateEnd = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgFiltros = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLblSourceWarehouse = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLblDocument = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDCncReport = New Presentation.Controls.CtrNavigationControlPanel()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoPopUpContainerEdit1 = New Presentation.Controls.IndigoPopUpContainerEdit(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoDocumentViewer1 = New Presentation.Controls.IndigoDocumentViewer()
        Me.INDDvReport = New DevExpress.XtraPrinting.Preview.DocumentViewer()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.DocumentViewerBarManager1 = New DevExpress.XtraPrinting.Preview.DocumentViewerBarManager(Me.components)
        Me.PreviewBar1 = New DevExpress.XtraPrinting.Preview.PreviewBar()
        Me.PrintPreviewBarItem2 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem3 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem4 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem5 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem6 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem7 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem8 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem9 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem10 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem11 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem12 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem13 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem14 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem15 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem16 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.ZoomBarEditItem1 = New DevExpress.XtraPrinting.Preview.ZoomBarEditItem()
        Me.PrintPreviewRepositoryItemComboBox1 = New DevExpress.XtraPrinting.Preview.PrintPreviewRepositoryItemComboBox()
        Me.PrintPreviewBarItem17 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem18 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem19 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem20 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem21 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem22 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem23 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem24 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem25 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem26 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem27 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PreviewBar2 = New DevExpress.XtraPrinting.Preview.PreviewBar()
        Me.PrintPreviewStaticItem1 = New DevExpress.XtraPrinting.Preview.PrintPreviewStaticItem()
        Me.BarStaticItem1 = New DevExpress.XtraBars.BarStaticItem()
        Me.ProgressBarEditItem1 = New DevExpress.XtraPrinting.Preview.ProgressBarEditItem()
        Me.RepositoryItemProgressBar1 = New DevExpress.XtraEditors.Repository.RepositoryItemProgressBar()
        Me.PrintPreviewBarItem1 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.PrintPreviewStaticItem2 = New DevExpress.XtraPrinting.Preview.PrintPreviewStaticItem()
        Me.ZoomTrackBarEditItem1 = New DevExpress.XtraPrinting.Preview.ZoomTrackBarEditItem()
        Me.RepositoryItemZoomTrackBar1 = New DevExpress.XtraEditors.Repository.RepositoryItemZoomTrackBar()
        Me.PreviewBar3 = New DevExpress.XtraPrinting.Preview.PreviewBar()
        Me.PrintPreviewSubItem1 = New DevExpress.XtraPrinting.Preview.PrintPreviewSubItem()
        Me.PrintPreviewSubItem2 = New DevExpress.XtraPrinting.Preview.PrintPreviewSubItem()
        Me.PrintPreviewSubItem4 = New DevExpress.XtraPrinting.Preview.PrintPreviewSubItem()
        Me.PrintPreviewBarItem28 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem29 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.BarToolbarsListItem1 = New DevExpress.XtraBars.BarToolbarsListItem()
        Me.PrintPreviewSubItem3 = New DevExpress.XtraPrinting.Preview.PrintPreviewSubItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.PrintPreviewBarCheckItem1 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        Me.PrintPreviewBarCheckItem2 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        Me.PrintPreviewBarCheckItem3 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        Me.PrintPreviewBarCheckItem4 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        Me.PrintPreviewBarCheckItem5 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        Me.PrintPreviewBarCheckItem6 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        Me.PrintPreviewBarCheckItem7 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        Me.PrintPreviewBarCheckItem8 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        Me.PrintPreviewBarCheckItem9 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        Me.PrintPreviewBarCheckItem10 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        Me.PrintPreviewBarCheckItem11 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        Me.PrintPreviewBarCheckItem12 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        Me.PrintPreviewBarCheckItem13 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        Me.PrintPreviewBarCheckItem14 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        Me.PrintPreviewBarCheckItem15 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        Me.PrintPreviewBarCheckItem16 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        Me.PrintPreviewBarCheckItem17 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.INDPcReport = New DevExpress.XtraEditors.PanelControl()
        Me.INDCnReport = New Presentation.Controls.CtrNavigation()
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcBase.SuspendLayout()
        CType(Me.INDGlueTypeReport.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDateEditDateEnd.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDateEditDateEnd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDateEditDateStart.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDateEditDateStart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleWarehouse.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvWarehouse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleProduct.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleDocument.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvDocument, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgBase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgCriteria, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDateStart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciTypeReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDateEnd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgFiltros, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLblSourceWarehouse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLblDocument, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDCncReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDocumentViewer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDDvReport.SuspendLayout()
        CType(Me.DocumentViewerBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PrintPreviewRepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemProgressBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemZoomTrackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPcReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPcReport.SuspendLayout()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLcBase)
        Me.INDPanelControlBase.Controls.Add(Me.INDCncReport)
        Me.INDPanelControlBase.Controls.Add(Me.INDPcReport)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 23)
        Me.INDPanelControlBase.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDPanelControlBase.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(862, 563)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Dock = System.Windows.Forms.DockStyle.None
        '
        'BarraBotones
        '
        Me.BarraBotones.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.BarraBotones.OperatingUnitVisible = True
        '
        'INDLcBase
        '
        Me.INDLcBase.Controls.Add(Me.INDSbReport)
        Me.INDLcBase.Controls.Add(Me.INDGlueTypeReport)
        Me.INDLcBase.Controls.Add(Me.INDDateEditDateEnd)
        Me.INDLcBase.Controls.Add(Me.INDDateEditDateStart)
        Me.INDLcBase.Controls.Add(Me.INDSleWarehouse)
        Me.INDLcBase.Controls.Add(Me.INDSleProduct)
        Me.INDLcBase.Controls.Add(Me.INDSleDocument)
        Me.INDLcBase.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcBase.Location = New System.Drawing.Point(202, 6)
        Me.INDLcBase.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDLcBase.Name = "INDLcBase"
        Me.INDLcBase.Root = Me.INDLcgBase
        Me.INDLcBase.Size = New System.Drawing.Size(658, 555)
        Me.INDLcBase.TabIndex = 0
        Me.INDLcBase.Text = "LayoutControl1"
        '
        'INDSbReport
        '
        Me.INDSbReport.Location = New System.Drawing.Point(408, 237)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDSbReport, False)
        Me.INDSbReport.Name = "INDSbReport"
        Me.INDSbReport.Size = New System.Drawing.Size(360, 32)
        Me.INDSbReport.StyleController = Me.INDLcBase
        Me.INDSbReport.TabIndex = 10
        Me.INDSbReport.Text = "Generar Reporte"
        '
        'INDGlueTypeReport
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDGlueTypeReport, False)
        Me.INDGlueTypeReport.EnterMoveNextControl = True
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDGlueTypeReport, True)
        Me.INDGlueTypeReport.Location = New System.Drawing.Point(24, 185)
        Me.INDGlueTypeReport.Name = "INDGlueTypeReport"
        Me.INDGlueTypeReport.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGlueTypeReport.Properties.Appearance.Options.UseFont = True
        Me.INDGlueTypeReport.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGlueTypeReport.Properties.DisplayMember = "Item2"
        Me.INDGlueTypeReport.Properties.ImmediatePopup = True
        Me.INDGlueTypeReport.Properties.NullText = ""
        Me.INDGlueTypeReport.Properties.PopupView = Me.GridLookUpEdit1View
        Me.INDGlueTypeReport.Properties.ValueMember = "Item1"
        Me.INDGlueTypeReport.Size = New System.Drawing.Size(356, 28)
        Me.INDGlueTypeReport.StyleController = Me.INDLcBase
        Me.INDGlueTypeReport.TabIndex = 3
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDGlueTypeReport, Nothing)
        '
        'GridLookUpEdit1View
        '
        Me.GridLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseForeColor = True
        Me.GridLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.GridLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1})
        Me.GridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit1View.Name = "GridLookUpEdit1View"
        Me.GridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridLookUpEdit1View, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Tipo Reporte"
        Me.GridColumn1.FieldName = "Item2"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'INDDateEditDateEnd
        '
        Me.IndigoDate1.SetCampoObligatorio(Me.INDDateEditDateEnd, False)
        Me.INDDateEditDateEnd.EditValue = Nothing
        Me.INDDateEditDateEnd.EnterMoveNextControl = True
        Me.INDDateEditDateEnd.Location = New System.Drawing.Point(24, 129)
        Me.IndigoDate1.SetMascaraDate(Me.INDDateEditDateEnd, Presentation.Controls.IndigoDate.EMask.FechaHoraSegundos)
        Me.INDDateEditDateEnd.Name = "INDDateEditDateEnd"
        Me.INDDateEditDateEnd.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDateEditDateEnd.Properties.Appearance.Options.UseFont = True
        Me.INDDateEditDateEnd.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDDateEditDateEnd.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDDateEditDateEnd.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDateEditDateEnd.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDDateEditDateEnd.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDDateEditDateEnd.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDateEditDateEnd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDateEditDateEnd.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDateEditDateEnd.Properties.Mask.EditMask = "dd \de MMMM \de yyyy  HH:mm:ss"
        Me.INDDateEditDateEnd.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDateEditDateEnd.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDateEditDateEnd.Size = New System.Drawing.Size(356, 28)
        Me.INDDateEditDateEnd.StyleController = Me.INDLcBase
        Me.INDDateEditDateEnd.TabIndex = 2
        '
        'INDDateEditDateStart
        '
        Me.IndigoDate1.SetCampoObligatorio(Me.INDDateEditDateStart, False)
        Me.INDDateEditDateStart.EditValue = Nothing
        Me.INDDateEditDateStart.EnterMoveNextControl = True
        Me.INDDateEditDateStart.Location = New System.Drawing.Point(24, 73)
        Me.IndigoDate1.SetMascaraDate(Me.INDDateEditDateStart, Presentation.Controls.IndigoDate.EMask.FechaHoraSegundos)
        Me.INDDateEditDateStart.Name = "INDDateEditDateStart"
        Me.INDDateEditDateStart.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDateEditDateStart.Properties.Appearance.Options.UseFont = True
        Me.INDDateEditDateStart.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDDateEditDateStart.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDDateEditDateStart.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDateEditDateStart.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDDateEditDateStart.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDDateEditDateStart.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDateEditDateStart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDateEditDateStart.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDateEditDateStart.Properties.Mask.EditMask = "dd \de MMMM \de yyyy  HH:mm:ss"
        Me.INDDateEditDateStart.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDateEditDateStart.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDateEditDateStart.Size = New System.Drawing.Size(356, 28)
        Me.INDDateEditDateStart.StyleController = Me.INDLcBase
        Me.INDDateEditDateStart.TabIndex = 1
        '
        'INDSleWarehouse
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleWarehouse, AppearanceObject7)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleWarehouse, AppearanceObject8)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleWarehouse, False)
        Me.INDSleWarehouse.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleWarehouse, False)
        Me.INDSleWarehouse.Location = New System.Drawing.Point(408, 73)
        Me.INDSleWarehouse.Name = "INDSleWarehouse"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleWarehouse, True)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleWarehouse, False)
        Me.INDSleWarehouse.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleWarehouse.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleWarehouse.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleWarehouse.Properties.Appearance.Options.UseFont = True
        Me.INDSleWarehouse.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleWarehouse.Properties.NullText = ""
        Me.INDSleWarehouse.Properties.PopupView = Me.INDGvWarehouse
        Me.INDSleWarehouse.Properties.ShowClearButton = False
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleWarehouse, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleWarehouse, True)
        Me.INDSleWarehouse.Size = New System.Drawing.Size(359, 28)
        Me.INDSleWarehouse.StyleController = Me.INDLcBase
        Me.INDSleWarehouse.TabIndex = 33
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleWarehouse, Nothing)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleWarehouse, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleWarehouse, False)
        '
        'INDGvWarehouse
        '
        Me.INDGvWarehouse.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvWarehouse.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvWarehouse.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvWarehouse.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvWarehouse.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvWarehouse.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvWarehouse.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvWarehouse.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvWarehouse.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvWarehouse.Appearance.Row.Options.UseFont = True
        Me.INDGvWarehouse.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColSourceWareHouse_UnboundSelection, Me.INDColSourceWarehouseCode, Me.INDColSourceWarehouseName})
        Me.INDGvWarehouse.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvWarehouse.Name = "INDGvWarehouse"
        Me.INDGvWarehouse.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvWarehouse.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.INDGvWarehouse.OptionsSelection.MultiSelect = True
        Me.INDGvWarehouse.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvWarehouse.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvWarehouse.OptionsView.ShowAutoFilterRow = True
        Me.INDGvWarehouse.OptionsView.ShowDetailButtons = False
        Me.INDGvWarehouse.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvWarehouse, False)
        '
        'INDColSourceWareHouse_UnboundSelection
        '
        Me.INDColSourceWareHouse_UnboundSelection.Caption = " "
        Me.INDColSourceWareHouse_UnboundSelection.FieldName = "INDColSourceWareHouse_UnboundSelection"
        Me.INDColSourceWareHouse_UnboundSelection.Name = "INDColSourceWareHouse_UnboundSelection"
        Me.INDColSourceWareHouse_UnboundSelection.UnboundType = DevExpress.Data.UnboundColumnType.[Boolean]
        Me.INDColSourceWareHouse_UnboundSelection.Visible = True
        Me.INDColSourceWareHouse_UnboundSelection.VisibleIndex = 0
        Me.INDColSourceWareHouse_UnboundSelection.Width = 20
        '
        'INDColSourceWarehouseCode
        '
        Me.INDColSourceWarehouseCode.Caption = "Código"
        Me.INDColSourceWarehouseCode.FieldName = "Code"
        Me.INDColSourceWarehouseCode.Name = "INDColSourceWarehouseCode"
        Me.INDColSourceWarehouseCode.OptionsColumn.AllowEdit = False
        Me.INDColSourceWarehouseCode.OptionsColumn.AllowFocus = False
        Me.INDColSourceWarehouseCode.Visible = True
        Me.INDColSourceWarehouseCode.VisibleIndex = 1
        '
        'INDColSourceWarehouseName
        '
        Me.INDColSourceWarehouseName.Caption = "Nombre"
        Me.INDColSourceWarehouseName.FieldName = "Name"
        Me.INDColSourceWarehouseName.Name = "INDColSourceWarehouseName"
        Me.INDColSourceWarehouseName.OptionsColumn.AllowEdit = False
        Me.INDColSourceWarehouseName.OptionsColumn.AllowFocus = False
        Me.INDColSourceWarehouseName.Visible = True
        Me.INDColSourceWarehouseName.VisibleIndex = 2
        '
        'INDSleProduct
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleProduct, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleProduct, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleProduct, False)
        Me.INDSleProduct.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleProduct, False)
        Me.INDSleProduct.Location = New System.Drawing.Point(408, 133)
        Me.INDSleProduct.Name = "INDSleProduct"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleProduct, True)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleProduct, False)
        Me.INDSleProduct.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleProduct.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleProduct.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleProduct.Properties.Appearance.Options.UseFont = True
        Me.INDSleProduct.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleProduct.Properties.NullText = ""
        Me.INDSleProduct.Properties.PopupView = Me.INDGvProduct
        Me.INDSleProduct.Properties.ShowClearButton = False
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleProduct, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleProduct, True)
        Me.INDSleProduct.Size = New System.Drawing.Size(359, 28)
        Me.INDSleProduct.StyleController = Me.INDLcBase
        Me.INDSleProduct.TabIndex = 26
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleProduct, Nothing)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleProduct, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleProduct, False)
        '
        'INDGvProduct
        '
        Me.INDGvProduct.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvProduct.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvProduct.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvProduct.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvProduct.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvProduct.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvProduct.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvProduct.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvProduct.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvProduct.Appearance.Row.Options.UseFont = True
        Me.INDGvProduct.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColProduct_UnboundSelection, Me.INDColProductCode, Me.INDColProductName, Me.INDColProductType, Me.INDColProductSubGroup, Me.INDColProductGroup})
        Me.INDGvProduct.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvProduct.Name = "INDGvProduct"
        Me.INDGvProduct.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvProduct.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.INDGvProduct.OptionsSelection.MultiSelect = True
        Me.INDGvProduct.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvProduct.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvProduct.OptionsView.ShowAutoFilterRow = True
        Me.INDGvProduct.OptionsView.ShowDetailButtons = False
        Me.INDGvProduct.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvProduct, False)
        '
        'INDColProduct_UnboundSelection
        '
        Me.INDColProduct_UnboundSelection.Caption = " "
        Me.INDColProduct_UnboundSelection.CustomizationCaption = " "
        Me.INDColProduct_UnboundSelection.FieldName = "INDColProduct_UnboundSelection"
        Me.INDColProduct_UnboundSelection.Name = "INDColProduct_UnboundSelection"
        Me.INDColProduct_UnboundSelection.UnboundType = DevExpress.Data.UnboundColumnType.[Boolean]
        Me.INDColProduct_UnboundSelection.Visible = True
        Me.INDColProduct_UnboundSelection.VisibleIndex = 0
        Me.INDColProduct_UnboundSelection.Width = 20
        '
        'INDColProductCode
        '
        Me.INDColProductCode.Caption = " Código"
        Me.INDColProductCode.FieldName = "Code"
        Me.INDColProductCode.Name = "INDColProductCode"
        Me.INDColProductCode.OptionsColumn.AllowEdit = False
        Me.INDColProductCode.OptionsColumn.AllowFocus = False
        Me.INDColProductCode.Visible = True
        Me.INDColProductCode.VisibleIndex = 1
        '
        'INDColProductName
        '
        Me.INDColProductName.Caption = "Nombre"
        Me.INDColProductName.FieldName = "Name"
        Me.INDColProductName.Name = "INDColProductName"
        Me.INDColProductName.OptionsColumn.AllowEdit = False
        Me.INDColProductName.OptionsColumn.AllowFocus = False
        Me.INDColProductName.Visible = True
        Me.INDColProductName.VisibleIndex = 2
        '
        'INDColProductType
        '
        Me.INDColProductType.Caption = "Tipo"
        Me.INDColProductType.FieldName = "ProductTypeIdName"
        Me.INDColProductType.Name = "INDColProductType"
        Me.INDColProductType.OptionsColumn.AllowEdit = False
        Me.INDColProductType.OptionsColumn.AllowFocus = False
        Me.INDColProductType.Visible = True
        Me.INDColProductType.VisibleIndex = 3
        '
        'INDColProductSubGroup
        '
        Me.INDColProductSubGroup.Caption = "SubGrupo"
        Me.INDColProductSubGroup.FieldName = "ProductSubGroupIdName"
        Me.INDColProductSubGroup.Name = "INDColProductSubGroup"
        Me.INDColProductSubGroup.OptionsColumn.AllowEdit = False
        Me.INDColProductSubGroup.OptionsColumn.AllowFocus = False
        Me.INDColProductSubGroup.Visible = True
        Me.INDColProductSubGroup.VisibleIndex = 4
        '
        'INDColProductGroup
        '
        Me.INDColProductGroup.Caption = "Grupo"
        Me.INDColProductGroup.FieldName = "ProductGroupIdName"
        Me.INDColProductGroup.Name = "INDColProductGroup"
        Me.INDColProductGroup.OptionsColumn.AllowEdit = False
        Me.INDColProductGroup.OptionsColumn.AllowFocus = False
        Me.INDColProductGroup.Visible = True
        Me.INDColProductGroup.VisibleIndex = 5
        '
        'INDSleDocument
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleDocument, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleDocument, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleDocument, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleDocument, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleDocument, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleDocument, False)
        Me.INDSleDocument.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleDocument, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleDocument, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleDocument, False)
        Me.INDSleDocument.Location = New System.Drawing.Point(408, 193)
        Me.INDSleDocument.Name = "INDSleDocument"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleDocument, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleDocument, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleDocument, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleDocument, True)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleDocument, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleDocument, False)
        Me.INDSleDocument.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleDocument.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleDocument.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleDocument.Properties.Appearance.Options.UseFont = True
        Me.INDSleDocument.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleDocument.Properties.NullText = ""
        Me.INDSleDocument.Properties.PopupView = Me.INDGvDocument
        Me.INDSleDocument.Properties.ShowClearButton = False
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleDocument, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleDocument, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleDocument, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleDocument, True)
        Me.INDSleDocument.Size = New System.Drawing.Size(359, 28)
        Me.INDSleDocument.StyleController = Me.INDLcBase
        Me.INDSleDocument.TabIndex = 26
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleDocument, Nothing)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleDocument, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleDocument, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleDocument, False)
        '
        'INDGvDocument
        '
        Me.INDGvDocument.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvDocument.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvDocument.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvDocument.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvDocument.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvDocument.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvDocument.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvDocument.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvDocument.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvDocument.Appearance.Row.Options.UseFont = True
        Me.INDGvDocument.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColDocument_UnboundSelection, Me.INDColDocumentCode, Me.INDColDocumentName, Me.INDColDocumentDate, Me.INDColDocumentState})
        Me.INDGvDocument.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvDocument.Name = "INDGvDocument"
        Me.INDGvDocument.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvDocument.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.INDGvDocument.OptionsSelection.MultiSelect = True
        Me.INDGvDocument.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvDocument.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvDocument.OptionsView.ShowAutoFilterRow = True
        Me.INDGvDocument.OptionsView.ShowDetailButtons = False
        Me.INDGvDocument.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvDocument, False)
        '
        'INDColDocument_UnboundSelection
        '
        Me.INDColDocument_UnboundSelection.Caption = " "
        Me.INDColDocument_UnboundSelection.CustomizationCaption = " "
        Me.INDColDocument_UnboundSelection.FieldName = "INDColDocument_UnboundSelection"
        Me.INDColDocument_UnboundSelection.Name = "INDColDocument_UnboundSelection"
        Me.INDColDocument_UnboundSelection.UnboundType = DevExpress.Data.UnboundColumnType.[Boolean]
        Me.INDColDocument_UnboundSelection.Visible = True
        Me.INDColDocument_UnboundSelection.VisibleIndex = 0
        Me.INDColDocument_UnboundSelection.Width = 20
        '
        'INDColDocumentCode
        '
        Me.INDColDocumentCode.Caption = " Código"
        Me.INDColDocumentCode.FieldName = "Code"
        Me.INDColDocumentCode.Name = "INDColDocumentCode"
        Me.INDColDocumentCode.OptionsColumn.AllowEdit = False
        Me.INDColDocumentCode.OptionsColumn.AllowFocus = False
        Me.INDColDocumentCode.Visible = True
        Me.INDColDocumentCode.VisibleIndex = 1
        '
        'INDColDocumentName
        '
        Me.INDColDocumentName.Caption = "Nombre"
        Me.INDColDocumentName.FieldName = "CodeNamePatient"
        Me.INDColDocumentName.Name = "INDColDocumentName"
        Me.INDColDocumentName.OptionsColumn.AllowEdit = False
        Me.INDColDocumentName.OptionsColumn.AllowFocus = False
        Me.INDColDocumentName.Visible = True
        Me.INDColDocumentName.VisibleIndex = 2
        '
        'INDColDocumentDate
        '
        Me.INDColDocumentDate.Caption = "Fecha"
        Me.INDColDocumentDate.FieldName = "DocumentDate"
        Me.INDColDocumentDate.Name = "INDColDocumentDate"
        Me.INDColDocumentDate.OptionsColumn.AllowEdit = False
        Me.INDColDocumentDate.OptionsColumn.AllowFocus = False
        Me.INDColDocumentDate.Visible = True
        Me.INDColDocumentDate.VisibleIndex = 3
        '
        'INDColDocumentState
        '
        Me.INDColDocumentState.Caption = "Estado"
        Me.INDColDocumentState.FieldName = "StatusName"
        Me.INDColDocumentState.Name = "INDColDocumentState"
        Me.INDColDocumentState.OptionsColumn.AllowEdit = False
        Me.INDColDocumentState.OptionsColumn.AllowFocus = False
        Me.INDColDocumentState.Visible = True
        Me.INDColDocumentState.VisibleIndex = 4
        '
        'INDLcgBase
        '
        Me.INDLcgBase.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgBase.AppearanceGroup.Options.UseFont = True
        Me.INDLcgBase.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgBase.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgBase, False)
        Me.INDLcgBase.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLcgBase.GroupBordersVisible = False
        Me.INDLcgBase.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgCriteria, Me.INDLcgFiltros})
        Me.INDLcgBase.Name = "Root"
        Me.INDLcgBase.Size = New System.Drawing.Size(792, 538)
        Me.INDLcgBase.TextVisible = False
        '
        'INDLcgCriteria
        '
        Me.INDLcgCriteria.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgCriteria.AppearanceGroup.Options.UseFont = True
        Me.INDLcgCriteria.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgCriteria.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgCriteria.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgCriteria.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgCriteria.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgCriteria.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgCriteria.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgCriteria.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgCriteria.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgCriteria.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgCriteria.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgCriteria.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgCriteria, False)
        Me.INDLcgCriteria.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciDateStart, Me.INDLciTypeReport, Me.INDLciDateEnd})
        Me.INDLcgCriteria.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgCriteria.Name = "INDLcgCriteria"
        Me.INDLcgCriteria.Size = New System.Drawing.Size(384, 518)
        Me.INDLcgCriteria.Text = "Criterios"
        '
        'INDLciDateStart
        '
        Me.INDLciDateStart.Control = Me.INDDateEditDateStart
        Me.INDLciDateStart.Location = New System.Drawing.Point(0, 0)
        Me.INDLciDateStart.MaxSize = New System.Drawing.Size(360, 56)
        Me.INDLciDateStart.MinSize = New System.Drawing.Size(360, 56)
        Me.INDLciDateStart.Name = "INDLciDateStart"
        Me.INDLciDateStart.Size = New System.Drawing.Size(360, 56)
        Me.INDLciDateStart.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDateStart.Text = "Fecha Inicial"
        Me.INDLciDateStart.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDateStart.TextSize = New System.Drawing.Size(83, 17)
        '
        'INDLciTypeReport
        '
        Me.INDLciTypeReport.Control = Me.INDGlueTypeReport
        Me.INDLciTypeReport.Location = New System.Drawing.Point(0, 112)
        Me.INDLciTypeReport.MaxSize = New System.Drawing.Size(360, 56)
        Me.INDLciTypeReport.MinSize = New System.Drawing.Size(360, 56)
        Me.INDLciTypeReport.Name = "INDLciTypeReport"
        Me.INDLciTypeReport.Size = New System.Drawing.Size(360, 353)
        Me.INDLciTypeReport.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciTypeReport.Text = "Tipo Reporte"
        Me.INDLciTypeReport.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciTypeReport.TextSize = New System.Drawing.Size(83, 17)
        '
        'INDLciDateEnd
        '
        Me.INDLciDateEnd.Control = Me.INDDateEditDateEnd
        Me.INDLciDateEnd.Location = New System.Drawing.Point(0, 56)
        Me.INDLciDateEnd.MaxSize = New System.Drawing.Size(360, 56)
        Me.INDLciDateEnd.MinSize = New System.Drawing.Size(360, 56)
        Me.INDLciDateEnd.Name = "INDLciDateEnd"
        Me.INDLciDateEnd.Size = New System.Drawing.Size(360, 56)
        Me.INDLciDateEnd.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDateEnd.Text = "Fecha Final"
        Me.INDLciDateEnd.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDateEnd.TextSize = New System.Drawing.Size(83, 17)
        '
        'INDLcgFiltros
        '
        Me.INDLcgFiltros.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgFiltros.AppearanceGroup.Options.UseFont = True
        Me.INDLcgFiltros.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgFiltros.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgFiltros.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgFiltros.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgFiltros.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgFiltros.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgFiltros.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgFiltros.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgFiltros.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgFiltros.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgFiltros.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgFiltros.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgFiltros, False)
        Me.INDLcgFiltros.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.INDLblSourceWarehouse, Me.LayoutControlItem2, Me.INDLblDocument})
        Me.INDLcgFiltros.Location = New System.Drawing.Point(384, 0)
        Me.INDLcgFiltros.Name = "INDLcgFiltros"
        Me.INDLcgFiltros.Size = New System.Drawing.Size(388, 518)
        Me.INDLcgFiltros.Text = "Filtros"
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDSbReport
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 180)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(364, 40)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(364, 40)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 2, 6, 2)
        Me.LayoutControlItem1.Size = New System.Drawing.Size(364, 285)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'INDLblSourceWarehouse
        '
        Me.INDLblSourceWarehouse.Control = Me.INDSleWarehouse
        Me.INDLblSourceWarehouse.CustomizationFormText = "Almacen Origen :"
        Me.INDLblSourceWarehouse.Location = New System.Drawing.Point(0, 0)
        Me.INDLblSourceWarehouse.MaxSize = New System.Drawing.Size(363, 60)
        Me.INDLblSourceWarehouse.MinSize = New System.Drawing.Size(360, 60)
        Me.INDLblSourceWarehouse.Name = "INDLblSourceWarehouse"
        Me.INDLblSourceWarehouse.Size = New System.Drawing.Size(364, 60)
        Me.INDLblSourceWarehouse.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLblSourceWarehouse.Text = "Almacén :"
        Me.INDLblSourceWarehouse.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLblSourceWarehouse.TextSize = New System.Drawing.Size(83, 17)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDSleProduct
        Me.LayoutControlItem2.CustomizationFormText = "Producto :"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 60)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(363, 60)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(360, 60)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(364, 60)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.Text = "Producto :"
        Me.LayoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(83, 17)
        '
        'INDLblDocument
        '
        Me.INDLblDocument.Control = Me.INDSleDocument
        Me.INDLblDocument.CustomizationFormText = "Producto :"
        Me.INDLblDocument.Location = New System.Drawing.Point(0, 120)
        Me.INDLblDocument.MaxSize = New System.Drawing.Size(363, 60)
        Me.INDLblDocument.MinSize = New System.Drawing.Size(360, 60)
        Me.INDLblDocument.Name = "INDLblDocument"
        Me.INDLblDocument.Size = New System.Drawing.Size(364, 60)
        Me.INDLblDocument.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLblDocument.Text = "Documento :"
        Me.INDLblDocument.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLblDocument.TextSize = New System.Drawing.Size(83, 17)
        '
        'INDCncReport
        '
        Me.INDCncReport.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDCncReport.Dock = System.Windows.Forms.DockStyle.Left
        Me.INDCncReport.LayoutControl = Me.INDLcBase
        Me.INDCncReport.Location = New System.Drawing.Point(2, 6)
        Me.INDCncReport.Margin = New System.Windows.Forms.Padding(0)
        Me.INDCncReport.Name = "INDCncReport"
        Me.INDCncReport.Size = New System.Drawing.Size(200, 555)
        Me.INDCncReport.TabIndex = 1
        Me.INDCncReport.UseDisabledStatePainter = False
        '
        'IndigoDocumentViewer1
        '
        Me.IndigoDocumentViewer1.ParentForm = Nothing
        Me.IndigoDocumentViewer1.Permissions = Nothing
        '
        'INDDvReport
        '
        Me.INDDvReport.Controls.Add(Me.barDockControlLeft)
        Me.INDDvReport.Controls.Add(Me.barDockControlRight)
        Me.INDDvReport.Controls.Add(Me.barDockControlBottom)
        Me.INDDvReport.Controls.Add(Me.barDockControlTop)
        Me.INDDvReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.IndigoDocumentViewer1.SetExtendProperties(Me.INDDvReport, True)
        Me.INDDvReport.IsMetric = True
        Me.INDDvReport.Location = New System.Drawing.Point(62, 2)
        Me.INDDvReport.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDDvReport.Name = "INDDvReport"
        Me.INDDvReport.Size = New System.Drawing.Size(794, 551)
        Me.INDDvReport.TabIndex = 1
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 45)
        Me.barDockControlLeft.Manager = Me.DocumentViewerBarManager1
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 484)
        '
        'DocumentViewerBarManager1
        '
        Me.DocumentViewerBarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.PreviewBar1, Me.PreviewBar2, Me.PreviewBar3})
        Me.DocumentViewerBarManager1.DockControls.Add(Me.barDockControlTop)
        Me.DocumentViewerBarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.DocumentViewerBarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.DocumentViewerBarManager1.DockControls.Add(Me.barDockControlRight)
        Me.DocumentViewerBarManager1.DocumentViewer = Me.INDDvReport
        Me.DocumentViewerBarManager1.Form = Me.INDDvReport
        Me.DocumentViewerBarManager1.ImageStream = CType(resources.GetObject("DocumentViewerBarManager1.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.DocumentViewerBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.PrintPreviewStaticItem1, Me.BarStaticItem1, Me.ProgressBarEditItem1, Me.PrintPreviewBarItem1, Me.BarButtonItem1, Me.PrintPreviewStaticItem2, Me.ZoomTrackBarEditItem1, Me.PrintPreviewBarItem2, Me.PrintPreviewBarItem3, Me.PrintPreviewBarItem4, Me.PrintPreviewBarItem5, Me.PrintPreviewBarItem6, Me.PrintPreviewBarItem7, Me.PrintPreviewBarItem8, Me.PrintPreviewBarItem9, Me.PrintPreviewBarItem10, Me.PrintPreviewBarItem11, Me.PrintPreviewBarItem12, Me.PrintPreviewBarItem13, Me.PrintPreviewBarItem14, Me.PrintPreviewBarItem15, Me.PrintPreviewBarItem16, Me.ZoomBarEditItem1, Me.PrintPreviewBarItem17, Me.PrintPreviewBarItem18, Me.PrintPreviewBarItem19, Me.PrintPreviewBarItem20, Me.PrintPreviewBarItem21, Me.PrintPreviewBarItem22, Me.PrintPreviewBarItem23, Me.PrintPreviewBarItem24, Me.PrintPreviewBarItem25, Me.PrintPreviewBarItem26, Me.PrintPreviewBarItem27, Me.PrintPreviewSubItem1, Me.PrintPreviewSubItem2, Me.PrintPreviewSubItem3, Me.PrintPreviewSubItem4, Me.PrintPreviewBarItem28, Me.PrintPreviewBarItem29, Me.BarToolbarsListItem1, Me.PrintPreviewBarCheckItem1, Me.PrintPreviewBarCheckItem2, Me.PrintPreviewBarCheckItem3, Me.PrintPreviewBarCheckItem4, Me.PrintPreviewBarCheckItem5, Me.PrintPreviewBarCheckItem6, Me.PrintPreviewBarCheckItem7, Me.PrintPreviewBarCheckItem8, Me.PrintPreviewBarCheckItem9, Me.PrintPreviewBarCheckItem10, Me.PrintPreviewBarCheckItem11, Me.PrintPreviewBarCheckItem12, Me.PrintPreviewBarCheckItem13, Me.PrintPreviewBarCheckItem14, Me.PrintPreviewBarCheckItem15, Me.PrintPreviewBarCheckItem16, Me.PrintPreviewBarCheckItem17})
        Me.DocumentViewerBarManager1.MainMenu = Me.PreviewBar3
        Me.DocumentViewerBarManager1.MaxItemId = 58
        Me.DocumentViewerBarManager1.PreviewBar = Me.PreviewBar1
        Me.DocumentViewerBarManager1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemProgressBar1, Me.RepositoryItemZoomTrackBar1, Me.PrintPreviewRepositoryItemComboBox1})
        Me.DocumentViewerBarManager1.StatusBar = Me.PreviewBar2
        Me.DocumentViewerBarManager1.TransparentEditorsMode = DevExpress.Utils.DefaultBoolean.[True]
        '
        'PreviewBar1
        '
        Me.PreviewBar1.BarName = "Toolbar"
        Me.PreviewBar1.DockCol = 0
        Me.PreviewBar1.DockRow = 1
        Me.PreviewBar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.PreviewBar1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem2), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem3), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem4), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem5), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem6, True), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem7, True), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem8), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem9, True), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem10), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem11), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem12), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem13), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem14, True), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem15), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem16, True), New DevExpress.XtraBars.LinkPersistInfo(Me.ZoomBarEditItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem17), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem18, True), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem19), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem20), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem21), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem22, True), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem23), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem24), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem25, True), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem26), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem27, True)})
        Me.PreviewBar1.Text = "Toolbar"
        '
        'PrintPreviewBarItem2
        '
        Me.PrintPreviewBarItem2.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
        Me.PrintPreviewBarItem2.Caption = "Document Map"
        Me.PrintPreviewBarItem2.Command = DevExpress.XtraPrinting.PrintingSystemCommand.DocumentMap
        Me.PrintPreviewBarItem2.Enabled = False
        Me.PrintPreviewBarItem2.Hint = "Document Map"
        Me.PrintPreviewBarItem2.Id = 7
        Me.PrintPreviewBarItem2.ImageOptions.ImageIndex = 19
        Me.PrintPreviewBarItem2.Name = "PrintPreviewBarItem2"
        '
        'PrintPreviewBarItem3
        '
        Me.PrintPreviewBarItem3.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
        Me.PrintPreviewBarItem3.Caption = "Parameters"
        Me.PrintPreviewBarItem3.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Parameters
        Me.PrintPreviewBarItem3.Enabled = False
        Me.PrintPreviewBarItem3.Hint = "Parameters"
        Me.PrintPreviewBarItem3.Id = 8
        Me.PrintPreviewBarItem3.ImageOptions.ImageIndex = 22
        Me.PrintPreviewBarItem3.Name = "PrintPreviewBarItem3"
        '
        'PrintPreviewBarItem4
        '
        Me.PrintPreviewBarItem4.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
        Me.PrintPreviewBarItem4.Caption = "Thumbnails"
        Me.PrintPreviewBarItem4.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Thumbnails
        Me.PrintPreviewBarItem4.Enabled = False
        Me.PrintPreviewBarItem4.Hint = "Thumbnails"
        Me.PrintPreviewBarItem4.Id = 9
        Me.PrintPreviewBarItem4.ImageOptions.ImageIndex = 23
        Me.PrintPreviewBarItem4.Name = "PrintPreviewBarItem4"
        '
        'PrintPreviewBarItem5
        '
        Me.PrintPreviewBarItem5.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
        Me.PrintPreviewBarItem5.Caption = "Search"
        Me.PrintPreviewBarItem5.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Find
        Me.PrintPreviewBarItem5.Enabled = False
        Me.PrintPreviewBarItem5.Hint = "Search"
        Me.PrintPreviewBarItem5.Id = 10
        Me.PrintPreviewBarItem5.ImageOptions.ImageIndex = 20
        Me.PrintPreviewBarItem5.Name = "PrintPreviewBarItem5"
        '
        'PrintPreviewBarItem6
        '
        Me.PrintPreviewBarItem6.Caption = "Customize"
        Me.PrintPreviewBarItem6.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Customize
        Me.PrintPreviewBarItem6.Enabled = False
        Me.PrintPreviewBarItem6.Hint = "Customize"
        Me.PrintPreviewBarItem6.Id = 11
        Me.PrintPreviewBarItem6.ImageOptions.ImageIndex = 14
        Me.PrintPreviewBarItem6.Name = "PrintPreviewBarItem6"
        '
        'PrintPreviewBarItem7
        '
        Me.PrintPreviewBarItem7.Caption = "Open"
        Me.PrintPreviewBarItem7.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Open
        Me.PrintPreviewBarItem7.Enabled = False
        Me.PrintPreviewBarItem7.Hint = "Open a document"
        Me.PrintPreviewBarItem7.Id = 12
        Me.PrintPreviewBarItem7.ImageOptions.ImageIndex = 24
        Me.PrintPreviewBarItem7.Name = "PrintPreviewBarItem7"
        '
        'PrintPreviewBarItem8
        '
        Me.PrintPreviewBarItem8.Caption = "Save"
        Me.PrintPreviewBarItem8.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Save
        Me.PrintPreviewBarItem8.Enabled = False
        Me.PrintPreviewBarItem8.Hint = "Save the document"
        Me.PrintPreviewBarItem8.Id = 13
        Me.PrintPreviewBarItem8.ImageOptions.ImageIndex = 25
        Me.PrintPreviewBarItem8.Name = "PrintPreviewBarItem8"
        '
        'PrintPreviewBarItem9
        '
        Me.PrintPreviewBarItem9.Caption = "&Print..."
        Me.PrintPreviewBarItem9.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Print
        Me.PrintPreviewBarItem9.Enabled = False
        Me.PrintPreviewBarItem9.Hint = "Print"
        Me.PrintPreviewBarItem9.Id = 14
        Me.PrintPreviewBarItem9.ImageOptions.ImageIndex = 0
        Me.PrintPreviewBarItem9.Name = "PrintPreviewBarItem9"
        '
        'PrintPreviewBarItem10
        '
        Me.PrintPreviewBarItem10.Caption = "P&rint"
        Me.PrintPreviewBarItem10.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PrintDirect
        Me.PrintPreviewBarItem10.Enabled = False
        Me.PrintPreviewBarItem10.Hint = "Quick Print"
        Me.PrintPreviewBarItem10.Id = 15
        Me.PrintPreviewBarItem10.ImageOptions.ImageIndex = 1
        Me.PrintPreviewBarItem10.Name = "PrintPreviewBarItem10"
        '
        'PrintPreviewBarItem11
        '
        Me.PrintPreviewBarItem11.Caption = "Page Set&up..."
        Me.PrintPreviewBarItem11.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PageSetup
        Me.PrintPreviewBarItem11.Enabled = False
        Me.PrintPreviewBarItem11.Hint = "Page Setup"
        Me.PrintPreviewBarItem11.Id = 16
        Me.PrintPreviewBarItem11.ImageOptions.ImageIndex = 2
        Me.PrintPreviewBarItem11.Name = "PrintPreviewBarItem11"
        '
        'PrintPreviewBarItem12
        '
        Me.PrintPreviewBarItem12.Caption = "Header And Footer"
        Me.PrintPreviewBarItem12.Command = DevExpress.XtraPrinting.PrintingSystemCommand.EditPageHF
        Me.PrintPreviewBarItem12.Enabled = False
        Me.PrintPreviewBarItem12.Hint = "Header And Footer"
        Me.PrintPreviewBarItem12.Id = 17
        Me.PrintPreviewBarItem12.ImageOptions.ImageIndex = 15
        Me.PrintPreviewBarItem12.Name = "PrintPreviewBarItem12"
        '
        'PrintPreviewBarItem13
        '
        Me.PrintPreviewBarItem13.ActAsDropDown = True
        Me.PrintPreviewBarItem13.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown
        Me.PrintPreviewBarItem13.Caption = "Scale"
        Me.PrintPreviewBarItem13.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Scale
        Me.PrintPreviewBarItem13.Enabled = False
        Me.PrintPreviewBarItem13.Hint = "Scale"
        Me.PrintPreviewBarItem13.Id = 18
        Me.PrintPreviewBarItem13.ImageOptions.ImageIndex = 26
        Me.PrintPreviewBarItem13.Name = "PrintPreviewBarItem13"
        '
        'PrintPreviewBarItem14
        '
        Me.PrintPreviewBarItem14.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
        Me.PrintPreviewBarItem14.Caption = "Hand Tool"
        Me.PrintPreviewBarItem14.Command = DevExpress.XtraPrinting.PrintingSystemCommand.HandTool
        Me.PrintPreviewBarItem14.Enabled = False
        Me.PrintPreviewBarItem14.Hint = "Hand Tool"
        Me.PrintPreviewBarItem14.Id = 19
        Me.PrintPreviewBarItem14.ImageOptions.ImageIndex = 16
        Me.PrintPreviewBarItem14.Name = "PrintPreviewBarItem14"
        '
        'PrintPreviewBarItem15
        '
        Me.PrintPreviewBarItem15.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
        Me.PrintPreviewBarItem15.Caption = "Magnifier"
        Me.PrintPreviewBarItem15.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Magnifier
        Me.PrintPreviewBarItem15.Enabled = False
        Me.PrintPreviewBarItem15.Hint = "Magnifier"
        Me.PrintPreviewBarItem15.Id = 20
        Me.PrintPreviewBarItem15.ImageOptions.ImageIndex = 3
        Me.PrintPreviewBarItem15.Name = "PrintPreviewBarItem15"
        '
        'PrintPreviewBarItem16
        '
        Me.PrintPreviewBarItem16.Caption = "Zoom Out"
        Me.PrintPreviewBarItem16.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ZoomOut
        Me.PrintPreviewBarItem16.Enabled = False
        Me.PrintPreviewBarItem16.Hint = "Zoom Out"
        Me.PrintPreviewBarItem16.Id = 21
        Me.PrintPreviewBarItem16.ImageOptions.ImageIndex = 5
        Me.PrintPreviewBarItem16.Name = "PrintPreviewBarItem16"
        '
        'ZoomBarEditItem1
        '
        Me.ZoomBarEditItem1.Caption = "Zoom"
        Me.ZoomBarEditItem1.Edit = Me.PrintPreviewRepositoryItemComboBox1
        Me.ZoomBarEditItem1.EditValue = "100%"
        Me.ZoomBarEditItem1.EditWidth = 70
        Me.ZoomBarEditItem1.Enabled = False
        Me.ZoomBarEditItem1.Hint = "Zoom"
        Me.ZoomBarEditItem1.Id = 22
        Me.ZoomBarEditItem1.Name = "ZoomBarEditItem1"
        '
        'PrintPreviewRepositoryItemComboBox1
        '
        Me.PrintPreviewRepositoryItemComboBox1.AutoComplete = False
        Me.PrintPreviewRepositoryItemComboBox1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.PrintPreviewRepositoryItemComboBox1.DropDownRows = 11
        Me.PrintPreviewRepositoryItemComboBox1.Name = "PrintPreviewRepositoryItemComboBox1"
        '
        'PrintPreviewBarItem17
        '
        Me.PrintPreviewBarItem17.Caption = "Zoom In"
        Me.PrintPreviewBarItem17.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ZoomIn
        Me.PrintPreviewBarItem17.Enabled = False
        Me.PrintPreviewBarItem17.Hint = "Zoom In"
        Me.PrintPreviewBarItem17.Id = 23
        Me.PrintPreviewBarItem17.ImageOptions.ImageIndex = 4
        Me.PrintPreviewBarItem17.Name = "PrintPreviewBarItem17"
        '
        'PrintPreviewBarItem18
        '
        Me.PrintPreviewBarItem18.Caption = "First Page"
        Me.PrintPreviewBarItem18.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ShowFirstPage
        Me.PrintPreviewBarItem18.Enabled = False
        Me.PrintPreviewBarItem18.Hint = "First Page"
        Me.PrintPreviewBarItem18.Id = 24
        Me.PrintPreviewBarItem18.ImageOptions.ImageIndex = 7
        Me.PrintPreviewBarItem18.Name = "PrintPreviewBarItem18"
        '
        'PrintPreviewBarItem19
        '
        Me.PrintPreviewBarItem19.Caption = "Previous Page"
        Me.PrintPreviewBarItem19.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ShowPrevPage
        Me.PrintPreviewBarItem19.Enabled = False
        Me.PrintPreviewBarItem19.Hint = "Previous Page"
        Me.PrintPreviewBarItem19.Id = 25
        Me.PrintPreviewBarItem19.ImageOptions.ImageIndex = 8
        Me.PrintPreviewBarItem19.Name = "PrintPreviewBarItem19"
        '
        'PrintPreviewBarItem20
        '
        Me.PrintPreviewBarItem20.Caption = "Next Page"
        Me.PrintPreviewBarItem20.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ShowNextPage
        Me.PrintPreviewBarItem20.Enabled = False
        Me.PrintPreviewBarItem20.Hint = "Next Page"
        Me.PrintPreviewBarItem20.Id = 26
        Me.PrintPreviewBarItem20.ImageOptions.ImageIndex = 9
        Me.PrintPreviewBarItem20.Name = "PrintPreviewBarItem20"
        '
        'PrintPreviewBarItem21
        '
        Me.PrintPreviewBarItem21.Caption = "Last Page"
        Me.PrintPreviewBarItem21.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ShowLastPage
        Me.PrintPreviewBarItem21.Enabled = False
        Me.PrintPreviewBarItem21.Hint = "Last Page"
        Me.PrintPreviewBarItem21.Id = 27
        Me.PrintPreviewBarItem21.ImageOptions.ImageIndex = 10
        Me.PrintPreviewBarItem21.Name = "PrintPreviewBarItem21"
        '
        'PrintPreviewBarItem22
        '
        Me.PrintPreviewBarItem22.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown
        Me.PrintPreviewBarItem22.Caption = "Multiple Pages"
        Me.PrintPreviewBarItem22.Command = DevExpress.XtraPrinting.PrintingSystemCommand.MultiplePages
        Me.PrintPreviewBarItem22.Enabled = False
        Me.PrintPreviewBarItem22.Hint = "Multiple Pages"
        Me.PrintPreviewBarItem22.Id = 28
        Me.PrintPreviewBarItem22.ImageOptions.ImageIndex = 11
        Me.PrintPreviewBarItem22.Name = "PrintPreviewBarItem22"
        '
        'PrintPreviewBarItem23
        '
        Me.PrintPreviewBarItem23.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown
        Me.PrintPreviewBarItem23.Caption = "&Color..."
        Me.PrintPreviewBarItem23.Command = DevExpress.XtraPrinting.PrintingSystemCommand.FillBackground
        Me.PrintPreviewBarItem23.Enabled = False
        Me.PrintPreviewBarItem23.Hint = "Background"
        Me.PrintPreviewBarItem23.Id = 29
        Me.PrintPreviewBarItem23.ImageOptions.ImageIndex = 12
        Me.PrintPreviewBarItem23.Name = "PrintPreviewBarItem23"
        '
        'PrintPreviewBarItem24
        '
        Me.PrintPreviewBarItem24.Caption = "&Watermark..."
        Me.PrintPreviewBarItem24.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Watermark
        Me.PrintPreviewBarItem24.Enabled = False
        Me.PrintPreviewBarItem24.Hint = "Watermark"
        Me.PrintPreviewBarItem24.Id = 30
        Me.PrintPreviewBarItem24.ImageOptions.ImageIndex = 21
        Me.PrintPreviewBarItem24.Name = "PrintPreviewBarItem24"
        '
        'PrintPreviewBarItem25
        '
        Me.PrintPreviewBarItem25.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown
        Me.PrintPreviewBarItem25.Caption = "Export Document..."
        Me.PrintPreviewBarItem25.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportFile
        Me.PrintPreviewBarItem25.Enabled = False
        Me.PrintPreviewBarItem25.Hint = "Export Document..."
        Me.PrintPreviewBarItem25.Id = 31
        Me.PrintPreviewBarItem25.ImageOptions.ImageIndex = 18
        Me.PrintPreviewBarItem25.Name = "PrintPreviewBarItem25"
        '
        'PrintPreviewBarItem26
        '
        Me.PrintPreviewBarItem26.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown
        Me.PrintPreviewBarItem26.Caption = "Send via E-Mail..."
        Me.PrintPreviewBarItem26.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendFile
        Me.PrintPreviewBarItem26.Enabled = False
        Me.PrintPreviewBarItem26.Hint = "Send via E-Mail..."
        Me.PrintPreviewBarItem26.Id = 32
        Me.PrintPreviewBarItem26.ImageOptions.ImageIndex = 17
        Me.PrintPreviewBarItem26.Name = "PrintPreviewBarItem26"
        '
        'PrintPreviewBarItem27
        '
        Me.PrintPreviewBarItem27.Caption = "E&xit"
        Me.PrintPreviewBarItem27.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ClosePreview
        Me.PrintPreviewBarItem27.Enabled = False
        Me.PrintPreviewBarItem27.Hint = "Close Preview"
        Me.PrintPreviewBarItem27.Id = 33
        Me.PrintPreviewBarItem27.ImageOptions.ImageIndex = 13
        Me.PrintPreviewBarItem27.Name = "PrintPreviewBarItem27"
        '
        'PreviewBar2
        '
        Me.PreviewBar2.BarName = "Status Bar"
        Me.PreviewBar2.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom
        Me.PreviewBar2.DockCol = 0
        Me.PreviewBar2.DockRow = 0
        Me.PreviewBar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom
        Me.PreviewBar2.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewStaticItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.BarStaticItem1, True), New DevExpress.XtraBars.LinkPersistInfo(Me.ProgressBarEditItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.BarButtonItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewStaticItem2, True), New DevExpress.XtraBars.LinkPersistInfo(Me.ZoomTrackBarEditItem1)})
        Me.PreviewBar2.OptionsBar.AllowQuickCustomization = False
        Me.PreviewBar2.OptionsBar.DrawDragBorder = False
        Me.PreviewBar2.OptionsBar.UseWholeRow = True
        Me.PreviewBar2.Text = "Status Bar"
        '
        'PrintPreviewStaticItem1
        '
        Me.PrintPreviewStaticItem1.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PrintPreviewStaticItem1.Caption = "Nothing"
        Me.PrintPreviewStaticItem1.Id = 0
        Me.PrintPreviewStaticItem1.LeftIndent = 1
        Me.PrintPreviewStaticItem1.Name = "PrintPreviewStaticItem1"
        Me.PrintPreviewStaticItem1.RightIndent = 1
        Me.PrintPreviewStaticItem1.Type = "PageOfPages"
        '
        'BarStaticItem1
        '
        Me.BarStaticItem1.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.BarStaticItem1.Id = 1
        Me.BarStaticItem1.Name = "BarStaticItem1"
        Me.BarStaticItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.OnlyInRuntime
        '
        'ProgressBarEditItem1
        '
        Me.ProgressBarEditItem1.Edit = Me.RepositoryItemProgressBar1
        Me.ProgressBarEditItem1.EditHeight = 12
        Me.ProgressBarEditItem1.EditWidth = 150
        Me.ProgressBarEditItem1.Id = 2
        Me.ProgressBarEditItem1.Name = "ProgressBarEditItem1"
        Me.ProgressBarEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'RepositoryItemProgressBar1
        '
        Me.RepositoryItemProgressBar1.Name = "RepositoryItemProgressBar1"
        '
        'PrintPreviewBarItem1
        '
        Me.PrintPreviewBarItem1.Caption = "Stop"
        Me.PrintPreviewBarItem1.Command = DevExpress.XtraPrinting.PrintingSystemCommand.StopPageBuilding
        Me.PrintPreviewBarItem1.Enabled = False
        Me.PrintPreviewBarItem1.Hint = "Stop"
        Me.PrintPreviewBarItem1.Id = 3
        Me.PrintPreviewBarItem1.Name = "PrintPreviewBarItem1"
        Me.PrintPreviewBarItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Left
        Me.BarButtonItem1.Enabled = False
        Me.BarButtonItem1.Id = 4
        Me.BarButtonItem1.Name = "BarButtonItem1"
        Me.BarButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.OnlyInRuntime
        '
        'PrintPreviewStaticItem2
        '
        Me.PrintPreviewStaticItem2.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right
        Me.PrintPreviewStaticItem2.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PrintPreviewStaticItem2.Caption = "100%"
        Me.PrintPreviewStaticItem2.Id = 5
        Me.PrintPreviewStaticItem2.Name = "PrintPreviewStaticItem2"
        Me.PrintPreviewStaticItem2.TextAlignment = System.Drawing.StringAlignment.Far
        Me.PrintPreviewStaticItem2.Type = "ZoomFactor"
        '
        'ZoomTrackBarEditItem1
        '
        Me.ZoomTrackBarEditItem1.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right
        Me.ZoomTrackBarEditItem1.Edit = Me.RepositoryItemZoomTrackBar1
        Me.ZoomTrackBarEditItem1.EditValue = 90
        Me.ZoomTrackBarEditItem1.EditWidth = 140
        Me.ZoomTrackBarEditItem1.Enabled = False
        Me.ZoomTrackBarEditItem1.Id = 6
        Me.ZoomTrackBarEditItem1.Name = "ZoomTrackBarEditItem1"
        Me.ZoomTrackBarEditItem1.Range = New Integer() {10, 500}
        '
        'RepositoryItemZoomTrackBar1
        '
        Me.RepositoryItemZoomTrackBar1.Alignment = DevExpress.Utils.VertAlignment.Center
        Me.RepositoryItemZoomTrackBar1.AllowFocused = False
        Me.RepositoryItemZoomTrackBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.RepositoryItemZoomTrackBar1.Maximum = 180
        Me.RepositoryItemZoomTrackBar1.Middle = 90
        Me.RepositoryItemZoomTrackBar1.Name = "RepositoryItemZoomTrackBar1"
        '
        'PreviewBar3
        '
        Me.PreviewBar3.BarName = "Main Menu"
        Me.PreviewBar3.DockCol = 0
        Me.PreviewBar3.DockRow = 0
        Me.PreviewBar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.PreviewBar3.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewSubItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewSubItem2), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewSubItem3)})
        Me.PreviewBar3.OptionsBar.MultiLine = True
        Me.PreviewBar3.OptionsBar.UseWholeRow = True
        Me.PreviewBar3.Text = "Main Menu"
        '
        'PrintPreviewSubItem1
        '
        Me.PrintPreviewSubItem1.Caption = "&File"
        Me.PrintPreviewSubItem1.Command = DevExpress.XtraPrinting.PrintingSystemCommand.File
        Me.PrintPreviewSubItem1.Id = 34
        Me.PrintPreviewSubItem1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem11), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem9), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem10), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem25, True), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem26), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem27, True)})
        Me.PrintPreviewSubItem1.Name = "PrintPreviewSubItem1"
        '
        'PrintPreviewSubItem2
        '
        Me.PrintPreviewSubItem2.Caption = "&View"
        Me.PrintPreviewSubItem2.Command = DevExpress.XtraPrinting.PrintingSystemCommand.View
        Me.PrintPreviewSubItem2.Id = 35
        Me.PrintPreviewSubItem2.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewSubItem4, True), New DevExpress.XtraBars.LinkPersistInfo(Me.BarToolbarsListItem1, True)})
        Me.PrintPreviewSubItem2.Name = "PrintPreviewSubItem2"
        '
        'PrintPreviewSubItem4
        '
        Me.PrintPreviewSubItem4.Caption = "&Page Layout"
        Me.PrintPreviewSubItem4.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PageLayout
        Me.PrintPreviewSubItem4.Id = 37
        Me.PrintPreviewSubItem4.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem28), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem29)})
        Me.PrintPreviewSubItem4.Name = "PrintPreviewSubItem4"
        '
        'PrintPreviewBarItem28
        '
        Me.PrintPreviewBarItem28.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
        Me.PrintPreviewBarItem28.Caption = "&Facing"
        Me.PrintPreviewBarItem28.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PageLayoutFacing
        Me.PrintPreviewBarItem28.Enabled = False
        Me.PrintPreviewBarItem28.GroupIndex = 100
        Me.PrintPreviewBarItem28.Id = 38
        Me.PrintPreviewBarItem28.Name = "PrintPreviewBarItem28"
        '
        'PrintPreviewBarItem29
        '
        Me.PrintPreviewBarItem29.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
        Me.PrintPreviewBarItem29.Caption = "&Continuous"
        Me.PrintPreviewBarItem29.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PageLayoutContinuous
        Me.PrintPreviewBarItem29.Enabled = False
        Me.PrintPreviewBarItem29.GroupIndex = 100
        Me.PrintPreviewBarItem29.Id = 39
        Me.PrintPreviewBarItem29.Name = "PrintPreviewBarItem29"
        '
        'BarToolbarsListItem1
        '
        Me.BarToolbarsListItem1.Caption = "Bars"
        Me.BarToolbarsListItem1.Id = 40
        Me.BarToolbarsListItem1.Name = "BarToolbarsListItem1"
        '
        'PrintPreviewSubItem3
        '
        Me.PrintPreviewSubItem3.Caption = "&Background"
        Me.PrintPreviewSubItem3.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Background
        Me.PrintPreviewSubItem3.Id = 36
        Me.PrintPreviewSubItem3.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem23), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem24)})
        Me.PrintPreviewSubItem3.Name = "PrintPreviewSubItem3"
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Manager = Me.DocumentViewerBarManager1
        Me.barDockControlTop.Size = New System.Drawing.Size(794, 45)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 529)
        Me.barDockControlBottom.Manager = Me.DocumentViewerBarManager1
        Me.barDockControlBottom.Size = New System.Drawing.Size(794, 22)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(794, 45)
        Me.barDockControlRight.Manager = Me.DocumentViewerBarManager1
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 484)
        '
        'PrintPreviewBarCheckItem1
        '
        Me.PrintPreviewBarCheckItem1.BindableChecked = True
        Me.PrintPreviewBarCheckItem1.Caption = "PDF File"
        Me.PrintPreviewBarCheckItem1.Checked = True
        Me.PrintPreviewBarCheckItem1.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportPdf
        Me.PrintPreviewBarCheckItem1.Enabled = False
        Me.PrintPreviewBarCheckItem1.GroupIndex = 2
        Me.PrintPreviewBarCheckItem1.Hint = "PDF File"
        Me.PrintPreviewBarCheckItem1.Id = 41
        Me.PrintPreviewBarCheckItem1.Name = "PrintPreviewBarCheckItem1"
        '
        'PrintPreviewBarCheckItem2
        '
        Me.PrintPreviewBarCheckItem2.Caption = "HTML File"
        Me.PrintPreviewBarCheckItem2.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportHtm
        Me.PrintPreviewBarCheckItem2.Enabled = False
        Me.PrintPreviewBarCheckItem2.GroupIndex = 2
        Me.PrintPreviewBarCheckItem2.Hint = "HTML File"
        Me.PrintPreviewBarCheckItem2.Id = 42
        Me.PrintPreviewBarCheckItem2.Name = "PrintPreviewBarCheckItem2"
        '
        'PrintPreviewBarCheckItem3
        '
        Me.PrintPreviewBarCheckItem3.Caption = "MHT File"
        Me.PrintPreviewBarCheckItem3.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportMht
        Me.PrintPreviewBarCheckItem3.Enabled = False
        Me.PrintPreviewBarCheckItem3.GroupIndex = 2
        Me.PrintPreviewBarCheckItem3.Hint = "MHT File"
        Me.PrintPreviewBarCheckItem3.Id = 43
        Me.PrintPreviewBarCheckItem3.Name = "PrintPreviewBarCheckItem3"
        '
        'PrintPreviewBarCheckItem4
        '
        Me.PrintPreviewBarCheckItem4.Caption = "RTF File"
        Me.PrintPreviewBarCheckItem4.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportRtf
        Me.PrintPreviewBarCheckItem4.Enabled = False
        Me.PrintPreviewBarCheckItem4.GroupIndex = 2
        Me.PrintPreviewBarCheckItem4.Hint = "RTF File"
        Me.PrintPreviewBarCheckItem4.Id = 44
        Me.PrintPreviewBarCheckItem4.Name = "PrintPreviewBarCheckItem4"
        '
        'PrintPreviewBarCheckItem5
        '
        Me.PrintPreviewBarCheckItem5.Caption = "XLS File"
        Me.PrintPreviewBarCheckItem5.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportXls
        Me.PrintPreviewBarCheckItem5.Enabled = False
        Me.PrintPreviewBarCheckItem5.GroupIndex = 2
        Me.PrintPreviewBarCheckItem5.Hint = "XLS File"
        Me.PrintPreviewBarCheckItem5.Id = 45
        Me.PrintPreviewBarCheckItem5.Name = "PrintPreviewBarCheckItem5"
        '
        'PrintPreviewBarCheckItem6
        '
        Me.PrintPreviewBarCheckItem6.Caption = "XLSX File"
        Me.PrintPreviewBarCheckItem6.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportXlsx
        Me.PrintPreviewBarCheckItem6.Enabled = False
        Me.PrintPreviewBarCheckItem6.GroupIndex = 2
        Me.PrintPreviewBarCheckItem6.Hint = "XLSX File"
        Me.PrintPreviewBarCheckItem6.Id = 46
        Me.PrintPreviewBarCheckItem6.Name = "PrintPreviewBarCheckItem6"
        '
        'PrintPreviewBarCheckItem7
        '
        Me.PrintPreviewBarCheckItem7.Caption = "CSV File"
        Me.PrintPreviewBarCheckItem7.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportCsv
        Me.PrintPreviewBarCheckItem7.Enabled = False
        Me.PrintPreviewBarCheckItem7.GroupIndex = 2
        Me.PrintPreviewBarCheckItem7.Hint = "CSV File"
        Me.PrintPreviewBarCheckItem7.Id = 47
        Me.PrintPreviewBarCheckItem7.Name = "PrintPreviewBarCheckItem7"
        '
        'PrintPreviewBarCheckItem8
        '
        Me.PrintPreviewBarCheckItem8.Caption = "Text File"
        Me.PrintPreviewBarCheckItem8.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportTxt
        Me.PrintPreviewBarCheckItem8.Enabled = False
        Me.PrintPreviewBarCheckItem8.GroupIndex = 2
        Me.PrintPreviewBarCheckItem8.Hint = "Text File"
        Me.PrintPreviewBarCheckItem8.Id = 48
        Me.PrintPreviewBarCheckItem8.Name = "PrintPreviewBarCheckItem8"
        '
        'PrintPreviewBarCheckItem9
        '
        Me.PrintPreviewBarCheckItem9.Caption = "Image File"
        Me.PrintPreviewBarCheckItem9.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportGraphic
        Me.PrintPreviewBarCheckItem9.Enabled = False
        Me.PrintPreviewBarCheckItem9.GroupIndex = 2
        Me.PrintPreviewBarCheckItem9.Hint = "Image File"
        Me.PrintPreviewBarCheckItem9.Id = 49
        Me.PrintPreviewBarCheckItem9.Name = "PrintPreviewBarCheckItem9"
        '
        'PrintPreviewBarCheckItem10
        '
        Me.PrintPreviewBarCheckItem10.BindableChecked = True
        Me.PrintPreviewBarCheckItem10.Caption = "PDF File"
        Me.PrintPreviewBarCheckItem10.Checked = True
        Me.PrintPreviewBarCheckItem10.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendPdf
        Me.PrintPreviewBarCheckItem10.Enabled = False
        Me.PrintPreviewBarCheckItem10.GroupIndex = 1
        Me.PrintPreviewBarCheckItem10.Hint = "PDF File"
        Me.PrintPreviewBarCheckItem10.Id = 50
        Me.PrintPreviewBarCheckItem10.Name = "PrintPreviewBarCheckItem10"
        '
        'PrintPreviewBarCheckItem11
        '
        Me.PrintPreviewBarCheckItem11.Caption = "MHT File"
        Me.PrintPreviewBarCheckItem11.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendMht
        Me.PrintPreviewBarCheckItem11.Enabled = False
        Me.PrintPreviewBarCheckItem11.GroupIndex = 1
        Me.PrintPreviewBarCheckItem11.Hint = "MHT File"
        Me.PrintPreviewBarCheckItem11.Id = 51
        Me.PrintPreviewBarCheckItem11.Name = "PrintPreviewBarCheckItem11"
        '
        'PrintPreviewBarCheckItem12
        '
        Me.PrintPreviewBarCheckItem12.Caption = "RTF File"
        Me.PrintPreviewBarCheckItem12.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendRtf
        Me.PrintPreviewBarCheckItem12.Enabled = False
        Me.PrintPreviewBarCheckItem12.GroupIndex = 1
        Me.PrintPreviewBarCheckItem12.Hint = "RTF File"
        Me.PrintPreviewBarCheckItem12.Id = 52
        Me.PrintPreviewBarCheckItem12.Name = "PrintPreviewBarCheckItem12"
        '
        'PrintPreviewBarCheckItem13
        '
        Me.PrintPreviewBarCheckItem13.Caption = "XLS File"
        Me.PrintPreviewBarCheckItem13.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendXls
        Me.PrintPreviewBarCheckItem13.Enabled = False
        Me.PrintPreviewBarCheckItem13.GroupIndex = 1
        Me.PrintPreviewBarCheckItem13.Hint = "XLS File"
        Me.PrintPreviewBarCheckItem13.Id = 53
        Me.PrintPreviewBarCheckItem13.Name = "PrintPreviewBarCheckItem13"
        '
        'PrintPreviewBarCheckItem14
        '
        Me.PrintPreviewBarCheckItem14.Caption = "XLSX File"
        Me.PrintPreviewBarCheckItem14.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendXlsx
        Me.PrintPreviewBarCheckItem14.Enabled = False
        Me.PrintPreviewBarCheckItem14.GroupIndex = 1
        Me.PrintPreviewBarCheckItem14.Hint = "XLSX File"
        Me.PrintPreviewBarCheckItem14.Id = 54
        Me.PrintPreviewBarCheckItem14.Name = "PrintPreviewBarCheckItem14"
        '
        'PrintPreviewBarCheckItem15
        '
        Me.PrintPreviewBarCheckItem15.Caption = "CSV File"
        Me.PrintPreviewBarCheckItem15.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendCsv
        Me.PrintPreviewBarCheckItem15.Enabled = False
        Me.PrintPreviewBarCheckItem15.GroupIndex = 1
        Me.PrintPreviewBarCheckItem15.Hint = "CSV File"
        Me.PrintPreviewBarCheckItem15.Id = 55
        Me.PrintPreviewBarCheckItem15.Name = "PrintPreviewBarCheckItem15"
        '
        'PrintPreviewBarCheckItem16
        '
        Me.PrintPreviewBarCheckItem16.Caption = "Text File"
        Me.PrintPreviewBarCheckItem16.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendTxt
        Me.PrintPreviewBarCheckItem16.Enabled = False
        Me.PrintPreviewBarCheckItem16.GroupIndex = 1
        Me.PrintPreviewBarCheckItem16.Hint = "Text File"
        Me.PrintPreviewBarCheckItem16.Id = 56
        Me.PrintPreviewBarCheckItem16.Name = "PrintPreviewBarCheckItem16"
        '
        'PrintPreviewBarCheckItem17
        '
        Me.PrintPreviewBarCheckItem17.Caption = "Image File"
        Me.PrintPreviewBarCheckItem17.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendGraphic
        Me.PrintPreviewBarCheckItem17.Enabled = False
        Me.PrintPreviewBarCheckItem17.GroupIndex = 1
        Me.PrintPreviewBarCheckItem17.Hint = "Image File"
        Me.PrintPreviewBarCheckItem17.Id = 57
        Me.PrintPreviewBarCheckItem17.Name = "PrintPreviewBarCheckItem17"
        '
        'INDPcReport
        '
        Me.INDPcReport.Controls.Add(Me.INDDvReport)
        Me.INDPcReport.Controls.Add(Me.INDCnReport)
        Me.INDPcReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDPcReport.Location = New System.Drawing.Point(2, 6)
        Me.INDPcReport.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDPcReport.Name = "INDPcReport"
        Me.INDPcReport.Size = New System.Drawing.Size(858, 555)
        Me.INDPcReport.TabIndex = 2
        '
        'INDCnReport
        '
        Me.INDCnReport.Dock = System.Windows.Forms.DockStyle.Left
        Me.INDCnReport.Location = New System.Drawing.Point(2, 2)
        Me.INDCnReport.Name = "INDCnReport"
        Me.INDCnReport.Size = New System.Drawing.Size(60, 551)
        Me.INDCnReport.TabIndex = 0
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'FrmReportListPharmacyDispensing
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(862, 586)
        Me.IconOptions.Icon = CType(resources.GetObject("FrmReportListPharmacyDispensing.IconOptions.Icon"), System.Drawing.Icon)
        Me.IconOptions.ShowIcon = False
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "FrmReportListPharmacyDispensing"
        Me.Opacity = 1.0R
        Me.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.Tag = "1740"
        Me.Text = "Listado de dispensación farmacéutica"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcBase.ResumeLayout(False)
        CType(Me.INDGlueTypeReport.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDateEditDateEnd.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDateEditDateEnd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDateEditDateStart.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDateEditDateStart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleWarehouse.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvWarehouse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleProduct.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvProduct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleDocument.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvDocument, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgBase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgCriteria, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDateStart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciTypeReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDateEnd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgFiltros, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLblSourceWarehouse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLblDocument, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDCncReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDocumentViewer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDDvReport.ResumeLayout(False)
        Me.INDDvReport.PerformLayout()
        CType(Me.DocumentViewerBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PrintPreviewRepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemProgressBar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemZoomTrackBar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPcReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPcReport.ResumeLayout(False)
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDLcBase As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDDateEditDateStart As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDLcgBase As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLcgCriteria As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciDateStart As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDCncReport As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDDateEditDateEnd As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDLciDateEnd As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGlueTypeReport As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciTypeReport As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLcgFiltros As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDSbReport As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoPopUpContainerEdit1 As Presentation.Controls.IndigoPopUpContainerEdit
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoDocumentViewer1 As Presentation.Controls.IndigoDocumentViewer
    Friend WithEvents INDPcReport As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDDvReport As DevExpress.XtraPrinting.Preview.DocumentViewer
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents INDCnReport As Presentation.Controls.CtrNavigation
    Friend WithEvents DocumentViewerBarManager1 As DevExpress.XtraPrinting.Preview.DocumentViewerBarManager
    Friend WithEvents PreviewBar1 As DevExpress.XtraPrinting.Preview.PreviewBar
    Friend WithEvents PrintPreviewBarItem2 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem3 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem4 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem5 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem6 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem7 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem8 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem9 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem10 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem11 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem12 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem13 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem14 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem15 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem16 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents ZoomBarEditItem1 As DevExpress.XtraPrinting.Preview.ZoomBarEditItem
    Friend WithEvents PrintPreviewRepositoryItemComboBox1 As DevExpress.XtraPrinting.Preview.PrintPreviewRepositoryItemComboBox
    Friend WithEvents PrintPreviewBarItem17 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem18 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem19 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem20 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem21 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem22 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem23 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem24 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem25 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem26 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem27 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PreviewBar2 As DevExpress.XtraPrinting.Preview.PreviewBar
    Friend WithEvents PrintPreviewStaticItem1 As DevExpress.XtraPrinting.Preview.PrintPreviewStaticItem
    Friend WithEvents BarStaticItem1 As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents ProgressBarEditItem1 As DevExpress.XtraPrinting.Preview.ProgressBarEditItem
    Friend WithEvents RepositoryItemProgressBar1 As DevExpress.XtraEditors.Repository.RepositoryItemProgressBar
    Friend WithEvents PrintPreviewBarItem1 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents PrintPreviewStaticItem2 As DevExpress.XtraPrinting.Preview.PrintPreviewStaticItem
    Friend WithEvents ZoomTrackBarEditItem1 As DevExpress.XtraPrinting.Preview.ZoomTrackBarEditItem
    Friend WithEvents RepositoryItemZoomTrackBar1 As DevExpress.XtraEditors.Repository.RepositoryItemZoomTrackBar
    Friend WithEvents PreviewBar3 As DevExpress.XtraPrinting.Preview.PreviewBar
    Friend WithEvents PrintPreviewSubItem1 As DevExpress.XtraPrinting.Preview.PrintPreviewSubItem
    Friend WithEvents PrintPreviewSubItem2 As DevExpress.XtraPrinting.Preview.PrintPreviewSubItem
    Friend WithEvents PrintPreviewSubItem4 As DevExpress.XtraPrinting.Preview.PrintPreviewSubItem
    Friend WithEvents PrintPreviewBarItem28 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem29 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents BarToolbarsListItem1 As DevExpress.XtraBars.BarToolbarsListItem
    Friend WithEvents PrintPreviewSubItem3 As DevExpress.XtraPrinting.Preview.PrintPreviewSubItem
    Friend WithEvents PrintPreviewBarCheckItem1 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
    Friend WithEvents PrintPreviewBarCheckItem2 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
    Friend WithEvents PrintPreviewBarCheckItem3 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
    Friend WithEvents PrintPreviewBarCheckItem4 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
    Friend WithEvents PrintPreviewBarCheckItem5 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
    Friend WithEvents PrintPreviewBarCheckItem6 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
    Friend WithEvents PrintPreviewBarCheckItem7 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
    Friend WithEvents PrintPreviewBarCheckItem8 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
    Friend WithEvents PrintPreviewBarCheckItem9 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
    Friend WithEvents PrintPreviewBarCheckItem10 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
    Friend WithEvents PrintPreviewBarCheckItem11 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
    Friend WithEvents PrintPreviewBarCheckItem12 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
    Friend WithEvents PrintPreviewBarCheckItem13 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
    Friend WithEvents PrintPreviewBarCheckItem14 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
    Friend WithEvents PrintPreviewBarCheckItem15 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
    Friend WithEvents PrintPreviewBarCheckItem16 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
    Friend WithEvents PrintPreviewBarCheckItem17 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents INDSleWarehouse As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvWarehouse As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColSourceWareHouse_UnboundSelection As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColSourceWarehouseCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColSourceWarehouseName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLblSourceWarehouse As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSleProduct As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvProduct As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColProduct_UnboundSelection As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColProductCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColProductName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColProductType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColProductSubGroup As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColProductGroup As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSleDocument As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvDocument As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColDocument_UnboundSelection As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColDocumentCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColDocumentName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColDocumentDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColDocumentState As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLblDocument As DevExpress.XtraLayout.LayoutControlItem
End Class
