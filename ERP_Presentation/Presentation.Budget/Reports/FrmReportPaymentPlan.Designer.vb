Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmReportPaymentPlan
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmReportPaymentPlan))
        Me.INDCncReport = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDLcBase = New DevExpress.XtraLayout.LayoutControl()
        Me.INDDnDate = New Presentation.Controls.CtrDateNavigator()
        Me.INDLblFinancialSource = New DevExpress.XtraEditors.LabelControl()
        Me.INDSleFinancialSourceEnd = New Presentation.Controls.SearchLookUpEditEx()
        Me.SearchLookUpEditExView7 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn16 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn17 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleFinancialSourceStart = New Presentation.Controls.SearchLookUpEditEx()
        Me.SearchLookUpEditExView6 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn15 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGleReport = New DevExpress.XtraEditors.GridLookUpEdit()
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
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.INDDvReport = New DevExpress.XtraPrinting.Preview.DocumentViewer()
        Me.PrintPreviewSubItem1 = New DevExpress.XtraPrinting.Preview.PrintPreviewSubItem()
        Me.PrintPreviewSubItem2 = New DevExpress.XtraPrinting.Preview.PrintPreviewSubItem()
        Me.PrintPreviewSubItem4 = New DevExpress.XtraPrinting.Preview.PrintPreviewSubItem()
        Me.PrintPreviewBarItem28 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem29 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.BarToolbarsListItem1 = New DevExpress.XtraBars.BarToolbarsListItem()
        Me.PrintPreviewSubItem3 = New DevExpress.XtraPrinting.Preview.PrintPreviewSubItem()
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
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn18 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDDateEditDateEnd = New DevExpress.XtraEditors.DateEdit()
        Me.INDDateEditDateStart = New DevExpress.XtraEditors.DateEdit()
        Me.INDsleValidity = New Presentation.Controls.SearchLookUpEditEx()
        Me.SearchLookUpEditExView4 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcStatusValidity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn14 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleEntity = New Presentation.Controls.SearchLookUpEditEx()
        Me.SearchLookUpEditExView3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDLblCategory = New DevExpress.XtraEditors.LabelControl()
        Me.INDSbGenerateReport = New DevExpress.XtraEditors.SimpleButton()
        Me.INDSleCategoryEnd = New Presentation.Controls.SearchLookUpEditEx()
        Me.SearchLookUpEditExView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleCategoryStart = New Presentation.Controls.SearchLookUpEditEx()
        Me.SearchLookUpEditExView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDLcgBase = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlcgFilter = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlciCategoryStart = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciCategoryEnd = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INlciSbGenerateReport = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciLblCategory = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciFinancialSourceStart = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciFinancialSourceEnd = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciFinancialSource = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgCriteria = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciDateStart = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciDateEnd = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciEntity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciValidity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciGleReport = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoDocumentViewer1 = New Presentation.Controls.IndigoDocumentViewer()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoPivotGridControl1 = New Presentation.Controls.IndigoPivotGridControl(Me.components)
        Me.IndigoPopUpContainerEdit1 = New Presentation.Controls.IndigoPopUpContainerEdit(Me.components)
        Me.IndigoRichEditControl1 = New Presentation.Controls.IndigoRichEditControl(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.INDPcReport = New DevExpress.XtraEditors.PanelControl()
        Me.INDCnReport = New Presentation.Controls.CtrNavigation()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDCncReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcBase.SuspendLayout()
        CType(Me.INDSleFinancialSourceEnd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEditExView7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleFinancialSourceStart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEditExView6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleReport.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DocumentViewerBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PrintPreviewRepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemProgressBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemZoomTrackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDDvReport.SuspendLayout()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDateEditDateEnd.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDateEditDateEnd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDateEditDateStart.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDateEditDateStart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleValidity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEditExView4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleEntity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEditExView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleCategoryEnd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEditExView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleCategoryStart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEditExView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgBase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgFilter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciCategoryStart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciCategoryEnd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INlciSbGenerateReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciLblCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciFinancialSourceStart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciFinancialSourceEnd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciFinancialSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgCriteria, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDateStart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDateEnd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciEntity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciValidity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciGleReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDocumentViewer1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoPivotGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoRichEditControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPcReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPcReport.SuspendLayout()
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
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1465, 94)
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
        Me.INDCncReport.TabIndex = 0
        Me.INDCncReport.UseDisabledStatePainter = False
        '
        'INDLcBase
        '
        Me.INDLcBase.Controls.Add(Me.INDDnDate)
        Me.INDLcBase.Controls.Add(Me.INDLblFinancialSource)
        Me.INDLcBase.Controls.Add(Me.INDSleFinancialSourceEnd)
        Me.INDLcBase.Controls.Add(Me.INDSleFinancialSourceStart)
        Me.INDLcBase.Controls.Add(Me.INDGleReport)
        Me.INDLcBase.Controls.Add(Me.INDDateEditDateEnd)
        Me.INDLcBase.Controls.Add(Me.INDDateEditDateStart)
        Me.INDLcBase.Controls.Add(Me.INDsleValidity)
        Me.INDLcBase.Controls.Add(Me.INDsleEntity)
        Me.INDLcBase.Controls.Add(Me.INDLblCategory)
        Me.INDLcBase.Controls.Add(Me.INDSbGenerateReport)
        Me.INDLcBase.Controls.Add(Me.INDSleCategoryEnd)
        Me.INDLcBase.Controls.Add(Me.INDSleCategoryStart)
        Me.INDLcBase.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcBase.Location = New System.Drawing.Point(202, 6)
        Me.INDLcBase.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDLcBase.Name = "INDLcBase"
        Me.INDLcBase.Root = Me.INDLcgBase
        Me.INDLcBase.Size = New System.Drawing.Size(658, 555)
        Me.INDLcBase.TabIndex = 1
        Me.INDLcBase.Text = "LayoutControl1"
        '
        'INDDnDate
        '
        Me.INDDnDate.CtrCalendar = Nothing
        Me.INDDnDate.HowShowControl = Presentation.Controls.CtrDateNavigator.EHowShowControl.Both
        Me.INDDnDate.Location = New System.Drawing.Point(24, 339)
        Me.INDDnDate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDDnDate.Name = "INDDnDate"
        Me.INDDnDate.Size = New System.Drawing.Size(356, 61)
        Me.INDDnDate.TabIndex = 55
        Me.INDDnDate.WithEvent = True
        '
        'INDLblFinancialSource
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDLblFinancialSource, True)
        Me.INDLblFinancialSource.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDLblFinancialSource, False)
        Me.INDLblFinancialSource.Location = New System.Drawing.Point(408, 148)
        Me.INDLblFinancialSource.Name = "INDLblFinancialSource"
        Me.INDLblFinancialSource.Size = New System.Drawing.Size(159, 21)
        Me.INDLblFinancialSource.StyleController = Me.INDLcBase
        Me.INDLblFinancialSource.TabIndex = 11
        Me.INDLblFinancialSource.Text = "Fuente De Financiación:"
        '
        'INDSleFinancialSourceEnd
        '
        Me.INDSleFinancialSourceEnd.AllowQueryOne = True
        Me.INDSleFinancialSourceEnd.Datasource = Nothing
        Me.INDSleFinancialSourceEnd.DisplayMember = "{Code} - {Name}"
        Me.INDSleFinancialSourceEnd.DisplayNullText = ""
        Me.INDSleFinancialSourceEnd.EditValue = Nothing
        Me.INDSleFinancialSourceEnd.Enabled = False
        Me.INDSleFinancialSourceEnd.EnterMoveNextControl = True
        Me.INDSleFinancialSourceEnd.FuncQueryOnKeyEnterPressed = Nothing
        Me.INDSleFinancialSourceEnd.IdOpenForm = 0
        Me.INDSleFinancialSourceEnd.Location = New System.Drawing.Point(480, 205)
        Me.INDSleFinancialSourceEnd.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDSleFinancialSourceEnd.MaximumSize = New System.Drawing.Size(5000, 28)
        Me.INDSleFinancialSourceEnd.MinimumSize = New System.Drawing.Size(100, 28)
        Me.INDSleFinancialSourceEnd.Name = "INDSleFinancialSourceEnd"
        Me.INDSleFinancialSourceEnd.PopUpFormSize = New System.Drawing.Size(217, 317)
        Me.INDSleFinancialSourceEnd.Size = New System.Drawing.Size(288, 28)
        Me.INDSleFinancialSourceEnd.TabIndex = 9
        Me.INDSleFinancialSourceEnd.ValueMember = "Code"
        Me.INDSleFinancialSourceEnd.View = Me.SearchLookUpEditExView7
        '
        'SearchLookUpEditExView7
        '
        Me.SearchLookUpEditExView7.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEditExView7.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEditExView7.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEditExView7.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEditExView7.Appearance.FocusedRow.Options.UseForeColor = True
        Me.SearchLookUpEditExView7.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView7.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEditExView7.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView7.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEditExView7.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEditExView7.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEditExView7.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.SearchLookUpEditExView7.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn16, Me.GridColumn17})
        Me.SearchLookUpEditExView7.Name = "SearchLookUpEditExView7"
        Me.SearchLookUpEditExView7.OptionsCustomization.AllowGroup = False
        Me.SearchLookUpEditExView7.OptionsDetail.EnableMasterViewMode = False
        Me.SearchLookUpEditExView7.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEditExView7.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEditExView7.OptionsView.ShowAutoFilterRow = True
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEditExView7, False)
        '
        'GridColumn16
        '
        Me.GridColumn16.Caption = "Código"
        Me.GridColumn16.FieldName = "Code"
        Me.GridColumn16.Name = "GridColumn16"
        Me.GridColumn16.OptionsColumn.AllowEdit = False
        Me.GridColumn16.OptionsColumn.AllowFocus = False
        Me.GridColumn16.Visible = True
        Me.GridColumn16.VisibleIndex = 0
        '
        'GridColumn17
        '
        Me.GridColumn17.Caption = "Nombre"
        Me.GridColumn17.FieldName = "Name"
        Me.GridColumn17.Name = "GridColumn17"
        Me.GridColumn17.OptionsColumn.AllowEdit = False
        Me.GridColumn17.OptionsColumn.AllowFocus = False
        Me.GridColumn17.Visible = True
        Me.GridColumn17.VisibleIndex = 1
        '
        'INDSleFinancialSourceStart
        '
        Me.INDSleFinancialSourceStart.AllowQueryOne = True
        Me.INDSleFinancialSourceStart.Datasource = Nothing
        Me.INDSleFinancialSourceStart.DisplayMember = "{Code} - {Name}"
        Me.INDSleFinancialSourceStart.DisplayNullText = ""
        Me.INDSleFinancialSourceStart.EditValue = Nothing
        Me.INDSleFinancialSourceStart.Enabled = False
        Me.INDSleFinancialSourceStart.EnterMoveNextControl = True
        Me.INDSleFinancialSourceStart.FuncQueryOnKeyEnterPressed = Nothing
        Me.INDSleFinancialSourceStart.IdOpenForm = 0
        Me.INDSleFinancialSourceStart.Location = New System.Drawing.Point(480, 173)
        Me.INDSleFinancialSourceStart.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDSleFinancialSourceStart.MaximumSize = New System.Drawing.Size(5000, 28)
        Me.INDSleFinancialSourceStart.MinimumSize = New System.Drawing.Size(100, 28)
        Me.INDSleFinancialSourceStart.Name = "INDSleFinancialSourceStart"
        Me.INDSleFinancialSourceStart.PopUpFormSize = New System.Drawing.Size(217, 317)
        Me.INDSleFinancialSourceStart.Size = New System.Drawing.Size(288, 28)
        Me.INDSleFinancialSourceStart.TabIndex = 8
        Me.INDSleFinancialSourceStart.ValueMember = "Code"
        Me.INDSleFinancialSourceStart.View = Me.SearchLookUpEditExView6
        '
        'SearchLookUpEditExView6
        '
        Me.SearchLookUpEditExView6.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEditExView6.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEditExView6.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEditExView6.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEditExView6.Appearance.FocusedRow.Options.UseForeColor = True
        Me.SearchLookUpEditExView6.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView6.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEditExView6.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView6.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEditExView6.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEditExView6.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEditExView6.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.SearchLookUpEditExView6.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn10, Me.GridColumn15})
        Me.SearchLookUpEditExView6.Name = "SearchLookUpEditExView6"
        Me.SearchLookUpEditExView6.OptionsCustomization.AllowGroup = False
        Me.SearchLookUpEditExView6.OptionsDetail.EnableMasterViewMode = False
        Me.SearchLookUpEditExView6.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEditExView6.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEditExView6.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEditExView6.OptionsView.ShowDetailButtons = False
        Me.SearchLookUpEditExView6.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEditExView6, False)
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "Código"
        Me.GridColumn10.FieldName = "Code"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.OptionsColumn.AllowEdit = False
        Me.GridColumn10.OptionsColumn.AllowFocus = False
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 0
        '
        'GridColumn15
        '
        Me.GridColumn15.Caption = "Nombre"
        Me.GridColumn15.FieldName = "Name"
        Me.GridColumn15.Name = "GridColumn15"
        Me.GridColumn15.OptionsColumn.AllowEdit = False
        Me.GridColumn15.OptionsColumn.AllowFocus = False
        Me.GridColumn15.Visible = True
        Me.GridColumn15.VisibleIndex = 1
        '
        'INDGleReport
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDGleReport, False)
        Me.INDGleReport.EnterMoveNextControl = True
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDGleReport, True)
        Me.INDGleReport.Location = New System.Drawing.Point(24, 195)
        Me.INDGleReport.MenuManager = Me.DocumentViewerBarManager1
        Me.INDGleReport.Name = "INDGleReport"
        Me.INDGleReport.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGleReport.Properties.Appearance.Options.UseFont = True
        Me.INDGleReport.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleReport.Properties.DisplayMember = "Item2"
        Me.INDGleReport.Properties.ImmediatePopup = True
        Me.INDGleReport.Properties.NullText = ""
        Me.INDGleReport.Properties.ValueMember = "Item1"
        Me.INDGleReport.Properties.View = Me.GridLookUpEdit1View
        Me.INDGleReport.Size = New System.Drawing.Size(356, 28)
        Me.INDGleReport.StyleController = Me.INDLcBase
        Me.INDGleReport.TabIndex = 2
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDGleReport, Nothing)
        '
        'DocumentViewerBarManager1
        '
        Me.DocumentViewerBarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.PreviewBar1, Me.PreviewBar2})
        Me.DocumentViewerBarManager1.DockControls.Add(Me.barDockControlTop)
        Me.DocumentViewerBarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.DocumentViewerBarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.DocumentViewerBarManager1.DockControls.Add(Me.barDockControlRight)
        Me.DocumentViewerBarManager1.DocumentViewer = Me.INDDvReport
        Me.DocumentViewerBarManager1.Form = Me.INDDvReport
        Me.DocumentViewerBarManager1.ImageStream = CType(resources.GetObject("DocumentViewerBarManager1.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.DocumentViewerBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.PrintPreviewStaticItem1, Me.BarStaticItem1, Me.ProgressBarEditItem1, Me.PrintPreviewBarItem1, Me.BarButtonItem1, Me.PrintPreviewStaticItem2, Me.ZoomTrackBarEditItem1, Me.PrintPreviewBarItem2, Me.PrintPreviewBarItem3, Me.PrintPreviewBarItem4, Me.PrintPreviewBarItem5, Me.PrintPreviewBarItem6, Me.PrintPreviewBarItem7, Me.PrintPreviewBarItem8, Me.PrintPreviewBarItem9, Me.PrintPreviewBarItem10, Me.PrintPreviewBarItem11, Me.PrintPreviewBarItem12, Me.PrintPreviewBarItem13, Me.PrintPreviewBarItem14, Me.PrintPreviewBarItem15, Me.PrintPreviewBarItem16, Me.ZoomBarEditItem1, Me.PrintPreviewBarItem17, Me.PrintPreviewBarItem18, Me.PrintPreviewBarItem19, Me.PrintPreviewBarItem20, Me.PrintPreviewBarItem21, Me.PrintPreviewBarItem22, Me.PrintPreviewBarItem23, Me.PrintPreviewBarItem24, Me.PrintPreviewBarItem25, Me.PrintPreviewBarItem26, Me.PrintPreviewBarItem27, Me.PrintPreviewSubItem1, Me.PrintPreviewSubItem2, Me.PrintPreviewSubItem3, Me.PrintPreviewSubItem4, Me.PrintPreviewBarItem28, Me.PrintPreviewBarItem29, Me.BarToolbarsListItem1, Me.PrintPreviewBarCheckItem1, Me.PrintPreviewBarCheckItem2, Me.PrintPreviewBarCheckItem3, Me.PrintPreviewBarCheckItem4, Me.PrintPreviewBarCheckItem5, Me.PrintPreviewBarCheckItem6, Me.PrintPreviewBarCheckItem7, Me.PrintPreviewBarCheckItem8, Me.PrintPreviewBarCheckItem9, Me.PrintPreviewBarCheckItem10, Me.PrintPreviewBarCheckItem11, Me.PrintPreviewBarCheckItem12, Me.PrintPreviewBarCheckItem13, Me.PrintPreviewBarCheckItem14, Me.PrintPreviewBarCheckItem15, Me.PrintPreviewBarCheckItem16, Me.PrintPreviewBarCheckItem17})
        Me.DocumentViewerBarManager1.MaxItemId = 58
        Me.DocumentViewerBarManager1.PreviewBar = Me.PreviewBar1
        Me.DocumentViewerBarManager1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemProgressBar1, Me.RepositoryItemZoomTrackBar1, Me.PrintPreviewRepositoryItemComboBox1})
        Me.DocumentViewerBarManager1.StatusBar = Me.PreviewBar2
        Me.DocumentViewerBarManager1.TransparentEditors = True
        '
        'PreviewBar1
        '
        Me.PreviewBar1.BarName = "Toolbar"
        Me.PreviewBar1.DockCol = 0
        Me.PreviewBar1.DockRow = 0
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
        Me.PrintPreviewBarItem2.ImageIndex = 19
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
        Me.PrintPreviewBarItem3.ImageIndex = 22
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
        Me.PrintPreviewBarItem4.ImageIndex = 23
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
        Me.PrintPreviewBarItem5.ImageIndex = 20
        Me.PrintPreviewBarItem5.Name = "PrintPreviewBarItem5"
        '
        'PrintPreviewBarItem6
        '
        Me.PrintPreviewBarItem6.Caption = "Customize"
        Me.PrintPreviewBarItem6.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Customize
        Me.PrintPreviewBarItem6.Enabled = False
        Me.PrintPreviewBarItem6.Hint = "Customize"
        Me.PrintPreviewBarItem6.Id = 11
        Me.PrintPreviewBarItem6.ImageIndex = 14
        Me.PrintPreviewBarItem6.Name = "PrintPreviewBarItem6"
        '
        'PrintPreviewBarItem7
        '
        Me.PrintPreviewBarItem7.Caption = "Open"
        Me.PrintPreviewBarItem7.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Open
        Me.PrintPreviewBarItem7.Enabled = False
        Me.PrintPreviewBarItem7.Hint = "Open a document"
        Me.PrintPreviewBarItem7.Id = 12
        Me.PrintPreviewBarItem7.ImageIndex = 24
        Me.PrintPreviewBarItem7.Name = "PrintPreviewBarItem7"
        '
        'PrintPreviewBarItem8
        '
        Me.PrintPreviewBarItem8.Caption = "Save"
        Me.PrintPreviewBarItem8.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Save
        Me.PrintPreviewBarItem8.Enabled = False
        Me.PrintPreviewBarItem8.Hint = "Save the document"
        Me.PrintPreviewBarItem8.Id = 13
        Me.PrintPreviewBarItem8.ImageIndex = 25
        Me.PrintPreviewBarItem8.Name = "PrintPreviewBarItem8"
        '
        'PrintPreviewBarItem9
        '
        Me.PrintPreviewBarItem9.Caption = "&Print..."
        Me.PrintPreviewBarItem9.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Print
        Me.PrintPreviewBarItem9.Enabled = False
        Me.PrintPreviewBarItem9.Hint = "Print"
        Me.PrintPreviewBarItem9.Id = 14
        Me.PrintPreviewBarItem9.ImageIndex = 0
        Me.PrintPreviewBarItem9.Name = "PrintPreviewBarItem9"
        '
        'PrintPreviewBarItem10
        '
        Me.PrintPreviewBarItem10.Caption = "P&rint"
        Me.PrintPreviewBarItem10.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PrintDirect
        Me.PrintPreviewBarItem10.Enabled = False
        Me.PrintPreviewBarItem10.Hint = "Quick Print"
        Me.PrintPreviewBarItem10.Id = 15
        Me.PrintPreviewBarItem10.ImageIndex = 1
        Me.PrintPreviewBarItem10.Name = "PrintPreviewBarItem10"
        '
        'PrintPreviewBarItem11
        '
        Me.PrintPreviewBarItem11.Caption = "Page Set&up..."
        Me.PrintPreviewBarItem11.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PageSetup
        Me.PrintPreviewBarItem11.Enabled = False
        Me.PrintPreviewBarItem11.Hint = "Page Setup"
        Me.PrintPreviewBarItem11.Id = 16
        Me.PrintPreviewBarItem11.ImageIndex = 2
        Me.PrintPreviewBarItem11.Name = "PrintPreviewBarItem11"
        '
        'PrintPreviewBarItem12
        '
        Me.PrintPreviewBarItem12.Caption = "Header And Footer"
        Me.PrintPreviewBarItem12.Command = DevExpress.XtraPrinting.PrintingSystemCommand.EditPageHF
        Me.PrintPreviewBarItem12.Enabled = False
        Me.PrintPreviewBarItem12.Hint = "Header And Footer"
        Me.PrintPreviewBarItem12.Id = 17
        Me.PrintPreviewBarItem12.ImageIndex = 15
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
        Me.PrintPreviewBarItem13.ImageIndex = 26
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
        Me.PrintPreviewBarItem14.ImageIndex = 16
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
        Me.PrintPreviewBarItem15.ImageIndex = 3
        Me.PrintPreviewBarItem15.Name = "PrintPreviewBarItem15"
        '
        'PrintPreviewBarItem16
        '
        Me.PrintPreviewBarItem16.Caption = "Zoom Out"
        Me.PrintPreviewBarItem16.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ZoomOut
        Me.PrintPreviewBarItem16.Enabled = False
        Me.PrintPreviewBarItem16.Hint = "Zoom Out"
        Me.PrintPreviewBarItem16.Id = 21
        Me.PrintPreviewBarItem16.ImageIndex = 5
        Me.PrintPreviewBarItem16.Name = "PrintPreviewBarItem16"
        '
        'ZoomBarEditItem1
        '
        Me.ZoomBarEditItem1.Caption = "Zoom"
        Me.ZoomBarEditItem1.Edit = Me.PrintPreviewRepositoryItemComboBox1
        Me.ZoomBarEditItem1.EditValue = "100%"
        Me.ZoomBarEditItem1.Enabled = False
        Me.ZoomBarEditItem1.Hint = "Zoom"
        Me.ZoomBarEditItem1.Id = 22
        Me.ZoomBarEditItem1.Name = "ZoomBarEditItem1"
        Me.ZoomBarEditItem1.Width = 70
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
        Me.PrintPreviewBarItem17.ImageIndex = 4
        Me.PrintPreviewBarItem17.Name = "PrintPreviewBarItem17"
        '
        'PrintPreviewBarItem18
        '
        Me.PrintPreviewBarItem18.Caption = "First Page"
        Me.PrintPreviewBarItem18.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ShowFirstPage
        Me.PrintPreviewBarItem18.Enabled = False
        Me.PrintPreviewBarItem18.Hint = "First Page"
        Me.PrintPreviewBarItem18.Id = 24
        Me.PrintPreviewBarItem18.ImageIndex = 7
        Me.PrintPreviewBarItem18.Name = "PrintPreviewBarItem18"
        '
        'PrintPreviewBarItem19
        '
        Me.PrintPreviewBarItem19.Caption = "Previous Page"
        Me.PrintPreviewBarItem19.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ShowPrevPage
        Me.PrintPreviewBarItem19.Enabled = False
        Me.PrintPreviewBarItem19.Hint = "Previous Page"
        Me.PrintPreviewBarItem19.Id = 25
        Me.PrintPreviewBarItem19.ImageIndex = 8
        Me.PrintPreviewBarItem19.Name = "PrintPreviewBarItem19"
        '
        'PrintPreviewBarItem20
        '
        Me.PrintPreviewBarItem20.Caption = "Next Page"
        Me.PrintPreviewBarItem20.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ShowNextPage
        Me.PrintPreviewBarItem20.Enabled = False
        Me.PrintPreviewBarItem20.Hint = "Next Page"
        Me.PrintPreviewBarItem20.Id = 26
        Me.PrintPreviewBarItem20.ImageIndex = 9
        Me.PrintPreviewBarItem20.Name = "PrintPreviewBarItem20"
        '
        'PrintPreviewBarItem21
        '
        Me.PrintPreviewBarItem21.Caption = "Last Page"
        Me.PrintPreviewBarItem21.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ShowLastPage
        Me.PrintPreviewBarItem21.Enabled = False
        Me.PrintPreviewBarItem21.Hint = "Last Page"
        Me.PrintPreviewBarItem21.Id = 27
        Me.PrintPreviewBarItem21.ImageIndex = 10
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
        Me.PrintPreviewBarItem22.ImageIndex = 11
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
        Me.PrintPreviewBarItem23.ImageIndex = 12
        Me.PrintPreviewBarItem23.Name = "PrintPreviewBarItem23"
        '
        'PrintPreviewBarItem24
        '
        Me.PrintPreviewBarItem24.Caption = "&Watermark..."
        Me.PrintPreviewBarItem24.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Watermark
        Me.PrintPreviewBarItem24.Enabled = False
        Me.PrintPreviewBarItem24.Hint = "Watermark"
        Me.PrintPreviewBarItem24.Id = 30
        Me.PrintPreviewBarItem24.ImageIndex = 21
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
        Me.PrintPreviewBarItem25.ImageIndex = 18
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
        Me.PrintPreviewBarItem26.ImageIndex = 17
        Me.PrintPreviewBarItem26.Name = "PrintPreviewBarItem26"
        '
        'PrintPreviewBarItem27
        '
        Me.PrintPreviewBarItem27.Caption = "E&xit"
        Me.PrintPreviewBarItem27.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ClosePreview
        Me.PrintPreviewBarItem27.Enabled = False
        Me.PrintPreviewBarItem27.Hint = "Close Preview"
        Me.PrintPreviewBarItem27.Id = 33
        Me.PrintPreviewBarItem27.ImageIndex = 13
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
        Me.PrintPreviewStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near
        Me.PrintPreviewStaticItem1.Type = "PageOfPages"
        '
        'BarStaticItem1
        '
        Me.BarStaticItem1.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.BarStaticItem1.Id = 1
        Me.BarStaticItem1.Name = "BarStaticItem1"
        Me.BarStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near
        Me.BarStaticItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.OnlyInRuntime
        '
        'ProgressBarEditItem1
        '
        Me.ProgressBarEditItem1.Edit = Me.RepositoryItemProgressBar1
        Me.ProgressBarEditItem1.EditHeight = 12
        Me.ProgressBarEditItem1.Id = 2
        Me.ProgressBarEditItem1.Name = "ProgressBarEditItem1"
        Me.ProgressBarEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        Me.ProgressBarEditItem1.Width = 150
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
        Me.ZoomTrackBarEditItem1.Enabled = False
        Me.ZoomTrackBarEditItem1.Id = 6
        Me.ZoomTrackBarEditItem1.Name = "ZoomTrackBarEditItem1"
        Me.ZoomTrackBarEditItem1.Range = New Integer() {10, 500}
        Me.ZoomTrackBarEditItem1.Width = 140
        '
        'RepositoryItemZoomTrackBar1
        '
        Me.RepositoryItemZoomTrackBar1.Alignment = DevExpress.Utils.VertAlignment.Center
        Me.RepositoryItemZoomTrackBar1.AllowFocused = False
        Me.RepositoryItemZoomTrackBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.RepositoryItemZoomTrackBar1.Maximum = 180
        Me.RepositoryItemZoomTrackBar1.Middle = 5
        Me.RepositoryItemZoomTrackBar1.Name = "RepositoryItemZoomTrackBar1"
        Me.RepositoryItemZoomTrackBar1.ScrollThumbStyle = DevExpress.XtraEditors.Repository.ScrollThumbStyle.ArrowDownRight
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(794, 31)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 525)
        Me.barDockControlBottom.Size = New System.Drawing.Size(794, 26)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 31)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 494)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(794, 31)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 494)
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
        'GridLookUpEdit1View
        '
        Me.GridLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseForeColor = True
        Me.GridLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.GridLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn18})
        Me.GridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit1View.Name = "GridLookUpEdit1View"
        Me.GridLookUpEdit1View.OptionsCustomization.AllowGroup = False
        Me.GridLookUpEdit1View.OptionsDetail.EnableMasterViewMode = False
        Me.GridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowDetailButtons = False
        Me.GridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridLookUpEdit1View, False)
        '
        'GridColumn18
        '
        Me.GridColumn18.Caption = "Reporte"
        Me.GridColumn18.FieldName = "Item2"
        Me.GridColumn18.Name = "GridColumn18"
        Me.GridColumn18.OptionsColumn.AllowEdit = False
        Me.GridColumn18.OptionsColumn.AllowFocus = False
        Me.GridColumn18.Visible = True
        Me.GridColumn18.VisibleIndex = 0
        '
        'INDDateEditDateEnd
        '
        Me.IndigoDate1.SetCampoObligatorio(Me.INDDateEditDateEnd, False)
        Me.INDDateEditDateEnd.EditValue = Nothing
        Me.INDDateEditDateEnd.Enabled = False
        Me.INDDateEditDateEnd.EnterMoveNextControl = True
        Me.INDDateEditDateEnd.Location = New System.Drawing.Point(24, 307)
        Me.IndigoDate1.SetMascaraDate(Me.INDDateEditDateEnd, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDDateEditDateEnd.MenuManager = Me.DocumentViewerBarManager1
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
        Me.INDDateEditDateEnd.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDDateEditDateEnd.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDateEditDateEnd.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDateEditDateEnd.Size = New System.Drawing.Size(356, 28)
        Me.INDDateEditDateEnd.StyleController = Me.INDLcBase
        Me.INDDateEditDateEnd.TabIndex = 4
        '
        'INDDateEditDateStart
        '
        Me.IndigoDate1.SetCampoObligatorio(Me.INDDateEditDateStart, False)
        Me.INDDateEditDateStart.EditValue = Nothing
        Me.INDDateEditDateStart.Enabled = False
        Me.INDDateEditDateStart.EnterMoveNextControl = True
        Me.INDDateEditDateStart.Location = New System.Drawing.Point(24, 251)
        Me.IndigoDate1.SetMascaraDate(Me.INDDateEditDateStart, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDDateEditDateStart.MenuManager = Me.DocumentViewerBarManager1
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
        Me.INDDateEditDateStart.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDDateEditDateStart.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDateEditDateStart.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDateEditDateStart.Size = New System.Drawing.Size(356, 28)
        Me.INDDateEditDateStart.StyleController = Me.INDLcBase
        Me.INDDateEditDateStart.TabIndex = 3
        '
        'INDsleValidity
        '
        Me.INDsleValidity.AllowQueryOne = False
        Me.INDsleValidity.Datasource = Nothing
        Me.INDsleValidity.DisplayMember = "{Year}"
        Me.INDsleValidity.DisplayNullText = ""
        Me.INDsleValidity.EditValue = Nothing
        Me.INDsleValidity.EnterMoveNextControl = True
        Me.INDsleValidity.FuncQueryOnKeyEnterPressed = Nothing
        Me.INDsleValidity.IdOpenForm = 0
        Me.INDsleValidity.Location = New System.Drawing.Point(24, 139)
        Me.INDsleValidity.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDsleValidity.MaximumSize = New System.Drawing.Size(5000, 28)
        Me.INDsleValidity.MinimumSize = New System.Drawing.Size(100, 28)
        Me.INDsleValidity.Name = "INDsleValidity"
        Me.INDsleValidity.PopUpFormSize = New System.Drawing.Size(217, 317)
        Me.INDsleValidity.Size = New System.Drawing.Size(356, 28)
        Me.INDsleValidity.TabIndex = 1
        Me.INDsleValidity.ValueMember = "Id"
        Me.INDsleValidity.View = Me.SearchLookUpEditExView4
        '
        'SearchLookUpEditExView4
        '
        Me.SearchLookUpEditExView4.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEditExView4.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEditExView4.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEditExView4.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEditExView4.Appearance.FocusedRow.Options.UseForeColor = True
        Me.SearchLookUpEditExView4.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView4.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEditExView4.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView4.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEditExView4.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEditExView4.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEditExView4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.SearchLookUpEditExView4.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn9, Me.INDgcStatusValidity, Me.GridColumn11, Me.GridColumn12, Me.GridColumn13, Me.GridColumn14})
        Me.SearchLookUpEditExView4.Name = "SearchLookUpEditExView4"
        Me.SearchLookUpEditExView4.OptionsCustomization.AllowGroup = False
        Me.SearchLookUpEditExView4.OptionsDetail.EnableMasterViewMode = False
        Me.SearchLookUpEditExView4.OptionsView.ShowDetailButtons = False
        Me.SearchLookUpEditExView4.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEditExView4, False)
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Año"
        Me.GridColumn9.FieldName = "Year"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.OptionsColumn.AllowEdit = False
        Me.GridColumn9.OptionsColumn.AllowFocus = False
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 0
        '
        'INDgcStatusValidity
        '
        Me.INDgcStatusValidity.Caption = "Estado"
        Me.INDgcStatusValidity.FieldName = "StatusText"
        Me.INDgcStatusValidity.Name = "INDgcStatusValidity"
        Me.INDgcStatusValidity.OptionsColumn.AllowEdit = False
        Me.INDgcStatusValidity.OptionsColumn.AllowFocus = False
        Me.INDgcStatusValidity.Visible = True
        Me.INDgcStatusValidity.VisibleIndex = 1
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "Resolución"
        Me.GridColumn11.FieldName = "ResolutionNumber"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.OptionsColumn.AllowEdit = False
        Me.GridColumn11.OptionsColumn.AllowFocus = False
        Me.GridColumn11.Visible = True
        Me.GridColumn11.VisibleIndex = 2
        '
        'GridColumn12
        '
        Me.GridColumn12.Caption = "Valor"
        Me.GridColumn12.DisplayFormat.FormatString = "Numeric ""c0"""
        Me.GridColumn12.FieldName = "ResolutionValue"
        Me.GridColumn12.Name = "GridColumn12"
        Me.GridColumn12.OptionsColumn.AllowEdit = False
        Me.GridColumn12.OptionsColumn.AllowFocus = False
        Me.GridColumn12.Visible = True
        Me.GridColumn12.VisibleIndex = 3
        '
        'GridColumn13
        '
        Me.GridColumn13.Caption = "Mes Ingreso"
        Me.GridColumn13.FieldName = "IncomeMonth"
        Me.GridColumn13.Name = "GridColumn13"
        Me.GridColumn13.OptionsColumn.AllowEdit = False
        Me.GridColumn13.OptionsColumn.AllowFocus = False
        Me.GridColumn13.Visible = True
        Me.GridColumn13.VisibleIndex = 4
        '
        'GridColumn14
        '
        Me.GridColumn14.Caption = "P A C"
        Me.GridColumn14.FieldName = "PACControl"
        Me.GridColumn14.Name = "GridColumn14"
        Me.GridColumn14.OptionsColumn.AllowEdit = False
        Me.GridColumn14.OptionsColumn.AllowFocus = False
        Me.GridColumn14.Visible = True
        Me.GridColumn14.VisibleIndex = 5
        '
        'INDsleEntity
        '
        Me.INDsleEntity.AllowQueryOne = False
        Me.INDsleEntity.Datasource = Nothing
        Me.INDsleEntity.DisplayMember = "{NameCode}"
        Me.INDsleEntity.DisplayNullText = ""
        Me.INDsleEntity.EditValue = Nothing
        Me.INDsleEntity.EnterMoveNextControl = True
        Me.INDsleEntity.FuncQueryOnKeyEnterPressed = Nothing
        Me.INDsleEntity.IdOpenForm = 0
        Me.INDsleEntity.Location = New System.Drawing.Point(24, 83)
        Me.INDsleEntity.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDsleEntity.MaximumSize = New System.Drawing.Size(5000, 28)
        Me.INDsleEntity.MinimumSize = New System.Drawing.Size(100, 28)
        Me.INDsleEntity.Name = "INDsleEntity"
        Me.INDsleEntity.PopUpFormSize = New System.Drawing.Size(217, 317)
        Me.INDsleEntity.Size = New System.Drawing.Size(356, 28)
        Me.INDsleEntity.TabIndex = 0
        Me.INDsleEntity.ValueMember = "Id"
        Me.INDsleEntity.View = Me.SearchLookUpEditExView3
        '
        'SearchLookUpEditExView3
        '
        Me.SearchLookUpEditExView3.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEditExView3.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEditExView3.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEditExView3.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEditExView3.Appearance.FocusedRow.Options.UseForeColor = True
        Me.SearchLookUpEditExView3.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView3.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEditExView3.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView3.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEditExView3.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEditExView3.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEditExView3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.SearchLookUpEditExView3.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn7, Me.GridColumn8})
        Me.SearchLookUpEditExView3.Name = "SearchLookUpEditExView3"
        Me.SearchLookUpEditExView3.OptionsCustomization.AllowGroup = False
        Me.SearchLookUpEditExView3.OptionsDetail.EnableMasterViewMode = False
        Me.SearchLookUpEditExView3.OptionsView.ShowDetailButtons = False
        Me.SearchLookUpEditExView3.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEditExView3, False)
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Código"
        Me.GridColumn7.FieldName = "Code"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsColumn.AllowEdit = False
        Me.GridColumn7.OptionsColumn.AllowFocus = False
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 0
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Nombre"
        Me.GridColumn8.FieldName = "Name"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.OptionsColumn.AllowEdit = False
        Me.GridColumn8.OptionsColumn.AllowFocus = False
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 1
        '
        'INDLblCategory
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDLblCategory, True)
        Me.INDLblCategory.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDLblCategory, False)
        Me.INDLblCategory.Location = New System.Drawing.Point(408, 59)
        Me.INDLblCategory.Name = "INDLblCategory"
        Me.INDLblCategory.Size = New System.Drawing.Size(41, 21)
        Me.INDLblCategory.StyleController = Me.INDLcBase
        Me.INDLblCategory.TabIndex = 7
        Me.INDLblCategory.Text = "Rubro"
        '
        'INDSbGenerateReport
        '
        Me.INDSbGenerateReport.Enabled = False
        Me.INDSbGenerateReport.Location = New System.Drawing.Point(408, 245)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDSbGenerateReport, False)
        Me.INDSbGenerateReport.Name = "INDSbGenerateReport"
        Me.INDSbGenerateReport.Size = New System.Drawing.Size(360, 28)
        Me.INDSbGenerateReport.StyleController = Me.INDLcBase
        Me.INDSbGenerateReport.TabIndex = 10
        Me.INDSbGenerateReport.Text = "Generar Reporte"
        '
        'INDSleCategoryEnd
        '
        Me.INDSleCategoryEnd.AllowQueryOne = True
        Me.INDSleCategoryEnd.Datasource = Nothing
        Me.INDSleCategoryEnd.DisplayMember = "{Code}"
        Me.INDSleCategoryEnd.DisplayNullText = ""
        Me.INDSleCategoryEnd.EditValue = Nothing
        Me.INDSleCategoryEnd.Enabled = False
        Me.INDSleCategoryEnd.EnterMoveNextControl = True
        Me.INDSleCategoryEnd.FuncQueryOnKeyEnterPressed = Nothing
        Me.INDSleCategoryEnd.IdOpenForm = 0
        Me.INDSleCategoryEnd.Location = New System.Drawing.Point(480, 116)
        Me.INDSleCategoryEnd.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDSleCategoryEnd.MaximumSize = New System.Drawing.Size(5000, 28)
        Me.INDSleCategoryEnd.MinimumSize = New System.Drawing.Size(100, 28)
        Me.INDSleCategoryEnd.Name = "INDSleCategoryEnd"
        Me.INDSleCategoryEnd.PopUpFormSize = New System.Drawing.Size(300, 600)
        Me.INDSleCategoryEnd.Size = New System.Drawing.Size(288, 28)
        Me.INDSleCategoryEnd.TabIndex = 7
        Me.INDSleCategoryEnd.ValueMember = "Code"
        Me.INDSleCategoryEnd.View = Me.SearchLookUpEditExView2
        '
        'SearchLookUpEditExView2
        '
        Me.SearchLookUpEditExView2.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEditExView2.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEditExView2.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEditExView2.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEditExView2.Appearance.FocusedRow.Options.UseForeColor = True
        Me.SearchLookUpEditExView2.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView2.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEditExView2.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView2.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEditExView2.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEditExView2.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEditExView2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.SearchLookUpEditExView2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn5, Me.GridColumn4, Me.GridColumn6})
        Me.SearchLookUpEditExView2.Name = "SearchLookUpEditExView2"
        Me.SearchLookUpEditExView2.OptionsCustomization.AllowGroup = False
        Me.SearchLookUpEditExView2.OptionsDetail.EnableMasterViewMode = False
        Me.SearchLookUpEditExView2.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEditExView2.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEditExView2.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEditExView2.OptionsView.ShowDetailButtons = False
        Me.SearchLookUpEditExView2.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEditExView2, False)
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Código"
        Me.GridColumn4.FieldName = "Code"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 1
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Nombre"
        Me.GridColumn5.FieldName = "Name"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.AllowEdit = False
        Me.GridColumn5.OptionsColumn.AllowFocus = False
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 0
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Fuente de financiación"
        Me.GridColumn6.FieldName = "FinancialSourceId.NameCode"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.AllowEdit = False
        Me.GridColumn6.OptionsColumn.AllowFocus = False
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 2
        '
        'INDSleCategoryStart
        '
        Me.INDSleCategoryStart.AllowQueryOne = True
        Me.INDSleCategoryStart.Datasource = Nothing
        Me.INDSleCategoryStart.DisplayMember = "{Code}"
        Me.INDSleCategoryStart.DisplayNullText = ""
        Me.INDSleCategoryStart.EditValue = Nothing
        Me.INDSleCategoryStart.Enabled = False
        Me.INDSleCategoryStart.EnterMoveNextControl = True
        Me.INDSleCategoryStart.FuncQueryOnKeyEnterPressed = Nothing
        Me.INDSleCategoryStart.IdOpenForm = 0
        Me.INDSleCategoryStart.Location = New System.Drawing.Point(480, 84)
        Me.INDSleCategoryStart.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDSleCategoryStart.MaximumSize = New System.Drawing.Size(5000, 28)
        Me.INDSleCategoryStart.MinimumSize = New System.Drawing.Size(100, 28)
        Me.INDSleCategoryStart.Name = "INDSleCategoryStart"
        Me.INDSleCategoryStart.PopUpFormSize = New System.Drawing.Size(300, 600)
        Me.INDSleCategoryStart.Size = New System.Drawing.Size(288, 28)
        Me.INDSleCategoryStart.TabIndex = 6
        Me.INDSleCategoryStart.ValueMember = "Code"
        Me.INDSleCategoryStart.View = Me.SearchLookUpEditExView1
        '
        'SearchLookUpEditExView1
        '
        Me.SearchLookUpEditExView1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEditExView1.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEditExView1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEditExView1.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEditExView1.Appearance.FocusedRow.Options.UseForeColor = True
        Me.SearchLookUpEditExView1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView1.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEditExView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEditExView1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEditExView1.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEditExView1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.SearchLookUpEditExView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3})
        Me.SearchLookUpEditExView1.Name = "SearchLookUpEditExView1"
        Me.SearchLookUpEditExView1.OptionsCustomization.AllowGroup = False
        Me.SearchLookUpEditExView1.OptionsDetail.EnableMasterViewMode = False
        Me.SearchLookUpEditExView1.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEditExView1.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEditExView1.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEditExView1.OptionsView.ShowDetailButtons = False
        Me.SearchLookUpEditExView1.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEditExView1, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Código"
        Me.GridColumn1.FieldName = "Code"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Nombre"
        Me.GridColumn2.FieldName = "Name"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Fuente de financiación"
        Me.GridColumn3.FieldName = "FinancialSourceId.NameCode"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        '
        'INDLcgBase
        '
        Me.INDLcgBase.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgBase.AppearanceGroup.Options.UseFont = True
        Me.INDLcgBase.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.INDLcgBase.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlcgFilter, Me.INDLcgCriteria})
        Me.INDLcgBase.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgBase.Name = "INDLcgBase"
        Me.INDLcgBase.Size = New System.Drawing.Size(792, 538)
        Me.INDLcgBase.TextVisible = False
        '
        'INDlcgFilter
        '
        Me.INDlcgFilter.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgFilter.AppearanceGroup.Options.UseFont = True
        Me.INDlcgFilter.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgFilter.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgFilter.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgFilter.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgFilter.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgFilter.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgFilter.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgFilter.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgFilter.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgFilter.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgFilter.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgFilter.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgFilter, False)
        Me.INDlcgFilter.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciCategoryStart, Me.INDLciCategoryEnd, Me.INlciSbGenerateReport, Me.INDLciLblCategory, Me.INDLciFinancialSourceStart, Me.INDLciFinancialSourceEnd, Me.INDLciFinancialSource})
        Me.INDlcgFilter.Location = New System.Drawing.Point(384, 0)
        Me.INDlcgFilter.Name = "INDlcgFilter"
        Me.INDlcgFilter.Size = New System.Drawing.Size(388, 518)
        Me.INDlcgFilter.Text = "Filtros"
        '
        'INDlciCategoryStart
        '
        Me.INDlciCategoryStart.Control = Me.INDSleCategoryStart
        Me.INDlciCategoryStart.Location = New System.Drawing.Point(0, 25)
        Me.INDlciCategoryStart.MaxSize = New System.Drawing.Size(364, 32)
        Me.INDlciCategoryStart.MinSize = New System.Drawing.Size(364, 32)
        Me.INDlciCategoryStart.Name = "INDlciCategoryStart"
        Me.INDlciCategoryStart.Size = New System.Drawing.Size(364, 32)
        Me.INDlciCategoryStart.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciCategoryStart.Text = "Inicial:"
        Me.INDlciCategoryStart.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciCategoryStart.TextSize = New System.Drawing.Size(60, 21)
        Me.INDlciCategoryStart.TextToControlDistance = 12
        '
        'INDLciCategoryEnd
        '
        Me.INDLciCategoryEnd.Control = Me.INDSleCategoryEnd
        Me.INDLciCategoryEnd.Location = New System.Drawing.Point(0, 57)
        Me.INDLciCategoryEnd.MaxSize = New System.Drawing.Size(364, 32)
        Me.INDLciCategoryEnd.MinSize = New System.Drawing.Size(364, 32)
        Me.INDLciCategoryEnd.Name = "INDLciCategoryEnd"
        Me.INDLciCategoryEnd.Size = New System.Drawing.Size(364, 32)
        Me.INDLciCategoryEnd.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciCategoryEnd.Text = "Final:"
        Me.INDLciCategoryEnd.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciCategoryEnd.TextSize = New System.Drawing.Size(60, 21)
        Me.INDLciCategoryEnd.TextToControlDistance = 12
        '
        'INlciSbGenerateReport
        '
        Me.INlciSbGenerateReport.Control = Me.INDSbGenerateReport
        Me.INlciSbGenerateReport.Location = New System.Drawing.Point(0, 178)
        Me.INlciSbGenerateReport.MaxSize = New System.Drawing.Size(364, 40)
        Me.INlciSbGenerateReport.MinSize = New System.Drawing.Size(364, 40)
        Me.INlciSbGenerateReport.Name = "INlciSbGenerateReport"
        Me.INlciSbGenerateReport.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 2, 10, 2)
        Me.INlciSbGenerateReport.Size = New System.Drawing.Size(364, 281)
        Me.INlciSbGenerateReport.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INlciSbGenerateReport.TextSize = New System.Drawing.Size(0, 0)
        Me.INlciSbGenerateReport.TextVisible = False
        '
        'INDLciLblCategory
        '
        Me.INDLciLblCategory.Control = Me.INDLblCategory
        Me.INDLciLblCategory.Location = New System.Drawing.Point(0, 0)
        Me.INDLciLblCategory.Name = "INDLciLblCategory"
        Me.INDLciLblCategory.Size = New System.Drawing.Size(364, 25)
        Me.INDLciLblCategory.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciLblCategory.TextVisible = False
        '
        'INDLciFinancialSourceStart
        '
        Me.INDLciFinancialSourceStart.Control = Me.INDSleFinancialSourceStart
        Me.INDLciFinancialSourceStart.Location = New System.Drawing.Point(0, 114)
        Me.INDLciFinancialSourceStart.MaxSize = New System.Drawing.Size(364, 32)
        Me.INDLciFinancialSourceStart.MinSize = New System.Drawing.Size(364, 32)
        Me.INDLciFinancialSourceStart.Name = "INDLciFinancialSourceStart"
        Me.INDLciFinancialSourceStart.Size = New System.Drawing.Size(364, 32)
        Me.INDLciFinancialSourceStart.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciFinancialSourceStart.Text = "Inicial:"
        Me.INDLciFinancialSourceStart.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciFinancialSourceStart.TextSize = New System.Drawing.Size(60, 21)
        Me.INDLciFinancialSourceStart.TextToControlDistance = 12
        '
        'INDLciFinancialSourceEnd
        '
        Me.INDLciFinancialSourceEnd.Control = Me.INDSleFinancialSourceEnd
        Me.INDLciFinancialSourceEnd.Location = New System.Drawing.Point(0, 146)
        Me.INDLciFinancialSourceEnd.MaxSize = New System.Drawing.Size(364, 32)
        Me.INDLciFinancialSourceEnd.MinSize = New System.Drawing.Size(364, 32)
        Me.INDLciFinancialSourceEnd.Name = "INDLciFinancialSourceEnd"
        Me.INDLciFinancialSourceEnd.Size = New System.Drawing.Size(364, 32)
        Me.INDLciFinancialSourceEnd.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciFinancialSourceEnd.Text = "Final:"
        Me.INDLciFinancialSourceEnd.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciFinancialSourceEnd.TextSize = New System.Drawing.Size(60, 21)
        Me.INDLciFinancialSourceEnd.TextToControlDistance = 12
        '
        'INDLciFinancialSource
        '
        Me.INDLciFinancialSource.Control = Me.INDLblFinancialSource
        Me.INDLciFinancialSource.Location = New System.Drawing.Point(0, 89)
        Me.INDLciFinancialSource.Name = "INDLciFinancialSource"
        Me.INDLciFinancialSource.Size = New System.Drawing.Size(364, 25)
        Me.INDLciFinancialSource.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciFinancialSource.TextVisible = False
        '
        'INDLcgCriteria
        '
        Me.INDLcgCriteria.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgCriteria.AppearanceGroup.Options.UseFont = True
        Me.INDLcgCriteria.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.INDLcgCriteria.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciDateStart, Me.INDLciDateEnd, Me.INDlciEntity, Me.INDlciValidity, Me.INDLciGleReport, Me.INDLciDate})
        Me.INDLcgCriteria.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgCriteria.Name = "INDLcgCriteria"
        Me.INDLcgCriteria.Size = New System.Drawing.Size(384, 518)
        Me.INDLcgCriteria.Text = "Criterios"
        '
        'INDLciDateStart
        '
        Me.INDLciDateStart.Control = Me.INDDateEditDateStart
        Me.INDLciDateStart.Location = New System.Drawing.Point(0, 168)
        Me.INDLciDateStart.MaxSize = New System.Drawing.Size(360, 56)
        Me.INDLciDateStart.MinSize = New System.Drawing.Size(360, 56)
        Me.INDLciDateStart.Name = "INDLciDateStart"
        Me.INDLciDateStart.Size = New System.Drawing.Size(360, 56)
        Me.INDLciDateStart.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDateStart.Text = "Fecha Inicial"
        Me.INDLciDateStart.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDateStart.TextSize = New System.Drawing.Size(140, 21)
        '
        'INDLciDateEnd
        '
        Me.INDLciDateEnd.Control = Me.INDDateEditDateEnd
        Me.INDLciDateEnd.Location = New System.Drawing.Point(0, 224)
        Me.INDLciDateEnd.MaxSize = New System.Drawing.Size(360, 56)
        Me.INDLciDateEnd.MinSize = New System.Drawing.Size(360, 56)
        Me.INDLciDateEnd.Name = "INDLciDateEnd"
        Me.INDLciDateEnd.Size = New System.Drawing.Size(360, 56)
        Me.INDLciDateEnd.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDateEnd.Text = "Fecha Final"
        Me.INDLciDateEnd.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDateEnd.TextSize = New System.Drawing.Size(140, 21)
        '
        'INDlciEntity
        '
        Me.INDlciEntity.Control = Me.INDsleEntity
        Me.INDlciEntity.Location = New System.Drawing.Point(0, 0)
        Me.INDlciEntity.MaxSize = New System.Drawing.Size(360, 56)
        Me.INDlciEntity.MinSize = New System.Drawing.Size(360, 56)
        Me.INDlciEntity.Name = "INDlciEntity"
        Me.INDlciEntity.Size = New System.Drawing.Size(360, 56)
        Me.INDlciEntity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciEntity.Text = "Entidad Presupuestal"
        Me.INDlciEntity.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciEntity.TextSize = New System.Drawing.Size(140, 21)
        '
        'INDlciValidity
        '
        Me.INDlciValidity.Control = Me.INDsleValidity
        Me.INDlciValidity.Location = New System.Drawing.Point(0, 56)
        Me.INDlciValidity.MaxSize = New System.Drawing.Size(360, 56)
        Me.INDlciValidity.MinSize = New System.Drawing.Size(360, 56)
        Me.INDlciValidity.Name = "INDlciValidity"
        Me.INDlciValidity.Size = New System.Drawing.Size(360, 56)
        Me.INDlciValidity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciValidity.Text = "Vigencia"
        Me.INDlciValidity.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciValidity.TextSize = New System.Drawing.Size(140, 21)
        '
        'INDLciGleReport
        '
        Me.INDLciGleReport.Control = Me.INDGleReport
        Me.INDLciGleReport.Location = New System.Drawing.Point(0, 112)
        Me.INDLciGleReport.MaxSize = New System.Drawing.Size(360, 56)
        Me.INDLciGleReport.MinSize = New System.Drawing.Size(360, 56)
        Me.INDLciGleReport.Name = "INDLciGleReport"
        Me.INDLciGleReport.Size = New System.Drawing.Size(360, 56)
        Me.INDLciGleReport.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciGleReport.Text = "Reporte"
        Me.INDLciGleReport.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciGleReport.TextSize = New System.Drawing.Size(140, 21)
        '
        'INDLciDate
        '
        Me.INDLciDate.Control = Me.INDDnDate
        Me.INDLciDate.Location = New System.Drawing.Point(0, 280)
        Me.INDLciDate.MaxSize = New System.Drawing.Size(360, 65)
        Me.INDLciDate.MinSize = New System.Drawing.Size(360, 65)
        Me.INDLciDate.Name = "INDLciDate"
        Me.INDLciDate.Size = New System.Drawing.Size(360, 179)
        Me.INDLciDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDate.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciDate.TextVisible = False
        '
        'IndigoDocumentViewer1
        '
        Me.IndigoDocumentViewer1.ParentForm = Nothing
        Me.IndigoDocumentViewer1.Permissions = Nothing
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
        'FrmReportPaymentPlan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(862, 586)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "FrmReportPaymentPlan"
        Me.Opacity = 1.0R
        Me.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.Tag = "1763"
        Me.Text = "Informe de plan de pagos"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDCncReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcBase.ResumeLayout(False)
        CType(Me.INDSleFinancialSourceEnd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEditExView7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleFinancialSourceStart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEditExView6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleReport.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DocumentViewerBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PrintPreviewRepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemProgressBar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemZoomTrackBar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDDvReport.ResumeLayout(False)
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDateEditDateEnd.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDateEditDateEnd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDateEditDateStart.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDateEditDateStart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleValidity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEditExView4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleEntity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEditExView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleCategoryEnd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEditExView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleCategoryStart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEditExView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgBase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgFilter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciCategoryStart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciCategoryEnd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INlciSbGenerateReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciLblCategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciFinancialSourceStart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciFinancialSourceEnd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciFinancialSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgCriteria, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDateStart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDateEnd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciEntity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciValidity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciGleReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDocumentViewer1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoPivotGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoRichEditControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPcReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPcReport.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDLcBase As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDLcgBase As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDCncReport As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDSbGenerateReport As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDSleCategoryEnd As Presentation.Controls.SearchLookUpEditEx
    Friend WithEvents SearchLookUpEditExView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDSleCategoryStart As Presentation.Controls.SearchLookUpEditEx
    Friend WithEvents SearchLookUpEditExView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlcgFilter As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlciCategoryStart As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciCategoryEnd As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INlciSbGenerateReport As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLblCategory As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDLciLblCategory As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents IndigoDocumentViewer1 As Presentation.Controls.IndigoDocumentViewer
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoPivotGridControl1 As Presentation.Controls.IndigoPivotGridControl
    Friend WithEvents IndigoPopUpContainerEdit1 As Presentation.Controls.IndigoPopUpContainerEdit
    Friend WithEvents IndigoRichEditControl1 As Presentation.Controls.IndigoRichEditControl
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
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
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDsleEntity As Presentation.Controls.SearchLookUpEditEx
    Friend WithEvents SearchLookUpEditExView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlciEntity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsleValidity As Presentation.Controls.SearchLookUpEditEx
    Friend WithEvents SearchLookUpEditExView4 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlciValidity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgcStatusValidity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn14 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDDateEditDateStart As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDLcgCriteria As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciDateStart As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDDateEditDateEnd As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDLciDateEnd As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGleReport As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciGleReport As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLblFinancialSource As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDSleFinancialSourceEnd As Presentation.Controls.SearchLookUpEditEx
    Friend WithEvents SearchLookUpEditExView7 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDSleFinancialSourceStart As Presentation.Controls.SearchLookUpEditEx
    Friend WithEvents SearchLookUpEditExView6 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciFinancialSourceStart As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciFinancialSourceEnd As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciFinancialSource As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn16 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn17 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn15 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDDnDate As Presentation.Controls.CtrDateNavigator
    Friend WithEvents INDLciDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn18 As DevExpress.XtraGrid.Columns.GridColumn
End Class
