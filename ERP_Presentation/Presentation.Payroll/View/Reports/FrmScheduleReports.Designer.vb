Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmScheduleReports
    Inherits FormBase

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim AppearanceObject5 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject6 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject7 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject8 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim AppearanceObject9 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject10 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject11 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject12 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmScheduleReports))
        Me.RecentlyUsedItemsComboBox1 = New DevExpress.XtraReports.UserDesigner.RecentlyUsedItemsComboBox()
        Me.DesignRepositoryItemComboBox1 = New DevExpress.XtraReports.UserDesigner.DesignRepositoryItemComboBox()
        Me.INDlyScheduleReports = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSleConcepts = New Presentation.Controls.SearchLookUpEditEx()
        Me.SearchLookUpEditExView6 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGleSearchBy = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGrcSearchBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleEmployee = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGrcDocumentEmployee = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGrcNameEmployee = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDProcessButton = New DevExpress.XtraEditors.SimpleButton()
        Me.INDCtrDateNavigator = New Presentation.Controls.CtrDateNavigator()
        Me.INDCmbReports = New DevExpress.XtraEditors.ImageComboBoxEdit()
        Me.INDsleGroup = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit2View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleFunctionalUnit = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDdeEndingDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDdeInitialDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDCmbStatusPayroll = New DevExpress.XtraEditors.ImageComboBoxEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyGrFiltros = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemFunctionalUnit = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemGroup = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemCmbReports = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemInitialDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemEndingDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemDateNavigator = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemButton = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemPayrollLiquidationConfirm = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciEmployee = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciSearchBy = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciConcept = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoRadioGroup1 = New Presentation.Controls.IndigoRadioGroup(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IndigoComboBoxEdit1 = New Presentation.Controls.IndigoComboBoxEdit(Me.components)
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoTextEdit2 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoComboBoxEdit2 = New Presentation.Controls.IndigoComboBoxEdit(Me.components)
        Me.IndigoSearchLookUpControl2 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoTextEdit3 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoTextEdit4 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoGridView2 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoDocumentViewer1 = New Presentation.Controls.IndigoDocumentViewer()
        Me.INDDvReport = New DevExpress.XtraPrinting.Preview.DocumentViewer()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.INDCtrNavigation = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDPcReport = New DevExpress.XtraEditors.PanelControl()
        Me.INDCnReport = New Presentation.Controls.CtrNavigation()
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
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RecentlyUsedItemsComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DesignRepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyScheduleReports, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyScheduleReports.SuspendLayout()
        CType(Me.INDSleConcepts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEditExView6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleSearchBy.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleEmployee.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDCmbReports.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleGroup.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit2View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleFunctionalUnit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdeEndingDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdeEndingDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdeInitialDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdeInitialDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDCmbStatusPayroll.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGrFiltros, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemFunctionalUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCmbReports, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemInitialDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemEndingDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDateNavigator, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemButton, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemPayrollLiquidationConfirm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciEmployee, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciSearchBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciConcept, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoRadioGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoComboBoxEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoComboBoxEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDocumentViewer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDDvReport.SuspendLayout()
        CType(Me.INDCtrNavigation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPcReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPcReport.SuspendLayout()
        CType(Me.DocumentViewerBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PrintPreviewRepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemProgressBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemZoomTrackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyScheduleReports)
        Me.INDPanelControlBase.Controls.Add(Me.INDCtrNavigation)
        Me.INDPanelControlBase.Controls.Add(Me.INDPcReport)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 25)
        Me.INDPanelControlBase.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDPanelControlBase.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1008, 692)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolBars.Location = New System.Drawing.Point(0, 6)
        Me.ToolBars.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ToolBars.Size = New System.Drawing.Size(1008, 94)
        '
        'BarraBotones
        '
        Me.BarraBotones.Dock = System.Windows.Forms.DockStyle.None
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1008, 94)
        '
        'RecentlyUsedItemsComboBox1
        '
        Me.RecentlyUsedItemsComboBox1.AppearanceDropDown.Font = New System.Drawing.Font("Tahoma", 11.25!)
        Me.RecentlyUsedItemsComboBox1.AppearanceDropDown.Options.UseFont = True
        Me.RecentlyUsedItemsComboBox1.AutoHeight = False
        Me.RecentlyUsedItemsComboBox1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RecentlyUsedItemsComboBox1.DropDownRows = 12
        Me.RecentlyUsedItemsComboBox1.Name = "RecentlyUsedItemsComboBox1"
        '
        'DesignRepositoryItemComboBox1
        '
        Me.DesignRepositoryItemComboBox1.AutoHeight = False
        Me.DesignRepositoryItemComboBox1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DesignRepositoryItemComboBox1.Name = "DesignRepositoryItemComboBox1"
        '
        'INDlyScheduleReports
        '
        Me.INDlyScheduleReports.Controls.Add(Me.INDSleConcepts)
        Me.INDlyScheduleReports.Controls.Add(Me.INDGleSearchBy)
        Me.INDlyScheduleReports.Controls.Add(Me.INDSleEmployee)
        Me.INDlyScheduleReports.Controls.Add(Me.INDProcessButton)
        Me.INDlyScheduleReports.Controls.Add(Me.INDCtrDateNavigator)
        Me.INDlyScheduleReports.Controls.Add(Me.INDCmbReports)
        Me.INDlyScheduleReports.Controls.Add(Me.INDsleGroup)
        Me.INDlyScheduleReports.Controls.Add(Me.INDsleFunctionalUnit)
        Me.INDlyScheduleReports.Controls.Add(Me.INDdeEndingDate)
        Me.INDlyScheduleReports.Controls.Add(Me.INDdeInitialDate)
        Me.INDlyScheduleReports.Controls.Add(Me.INDCmbStatusPayroll)
        Me.INDlyScheduleReports.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyScheduleReports.Location = New System.Drawing.Point(202, 8)
        Me.INDlyScheduleReports.Name = "INDlyScheduleReports"
        Me.INDlyScheduleReports.Root = Me.LayoutControlGroup1
        Me.INDlyScheduleReports.Size = New System.Drawing.Size(804, 682)
        Me.INDlyScheduleReports.TabIndex = 1
        Me.INDlyScheduleReports.Text = "LayoutControl1"
        '
        'INDSleConcepts
        '
        Me.INDSleConcepts.AllowQueryOne = False
        Me.INDSleConcepts.Datasource = Nothing
        Me.INDSleConcepts.DisplayMember = "{Descripcion}"
        Me.INDSleConcepts.DisplayNullText = ""
        Me.INDSleConcepts.EditValue = Nothing
        Me.INDSleConcepts.EnterMoveNextControl = False
        Me.INDSleConcepts.FuncQueryOnKeyEnterPressed = Nothing
        Me.INDSleConcepts.IdOpenForm = 0
        Me.INDSleConcepts.Location = New System.Drawing.Point(196, 419)
        Me.INDSleConcepts.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDSleConcepts.MaximumSize = New System.Drawing.Size(5000, 28)
        Me.INDSleConcepts.MinimumSize = New System.Drawing.Size(100, 28)
        Me.INDSleConcepts.Name = "INDSleConcepts"
        Me.INDSleConcepts.PopUpFormSize = New System.Drawing.Size(217, 317)
        Me.INDSleConcepts.Size = New System.Drawing.Size(324, 28)
        Me.INDSleConcepts.TabIndex = 16
        Me.INDSleConcepts.ValueMember = "Codigo"
        Me.INDSleConcepts.View = Me.SearchLookUpEditExView6
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
        Me.SearchLookUpEditExView6.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDCode, Me.INDName})
        Me.SearchLookUpEditExView6.Name = "SearchLookUpEditExView6"
        Me.SearchLookUpEditExView6.OptionsCustomization.AllowGroup = False
        Me.SearchLookUpEditExView6.OptionsDetail.EnableMasterViewMode = False
        Me.SearchLookUpEditExView6.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEditExView6.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEditExView6.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEditExView6.OptionsView.ShowDetailButtons = False
        Me.SearchLookUpEditExView6.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.SearchLookUpEditExView6, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEditExView6, False)
        '
        'INDCode
        '
        Me.INDCode.Caption = "Código"
        Me.INDCode.FieldName = "Codigo"
        Me.INDCode.Name = "INDCode"
        Me.INDCode.OptionsColumn.AllowEdit = False
        Me.INDCode.Visible = True
        Me.INDCode.VisibleIndex = 0
        '
        'INDName
        '
        Me.INDName.Caption = "Nombre"
        Me.INDName.FieldName = "Descripcion"
        Me.INDName.Name = "INDName"
        Me.INDName.OptionsColumn.AllowEdit = False
        Me.INDName.Visible = True
        Me.INDName.VisibleIndex = 1
        '
        'INDGleSearchBy
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDGleSearchBy, False)
        Me.IndigoTextEdit3.SetApplyStyle(Me.INDGleSearchBy, False)
        Me.IndigoTextEdit4.SetApplyStyle(Me.INDGleSearchBy, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleSearchBy, False)
        Me.IndigoTextEdit2.SetApplyStyle(Me.INDGleSearchBy, False)
        Me.IndigoTextEdit2.SetCampoObligatorio(Me.INDGleSearchBy, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleSearchBy, False)
        Me.IndigoTextEdit3.SetCampoObligatorio(Me.INDGleSearchBy, False)
        Me.IndigoTextEdit4.SetCampoObligatorio(Me.INDGleSearchBy, False)
        Me.INDGleSearchBy.EditValue = ""
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDGleSearchBy, False)
        Me.INDGleSearchBy.Location = New System.Drawing.Point(196, 275)
        Me.IndigoTextEdit4.SetMascara(Me.INDGleSearchBy, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit3.SetMascara(Me.INDGleSearchBy, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit1.SetMascara(Me.INDGleSearchBy, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit2.SetMascara(Me.INDGleSearchBy, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleSearchBy.Name = "INDGleSearchBy"
        Me.INDGleSearchBy.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDGleSearchBy.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGleSearchBy.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleSearchBy.Properties.Appearance.Options.UseFont = True
        Me.INDGleSearchBy.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleSearchBy.Properties.DisplayMember = "Item2"
        Me.INDGleSearchBy.Properties.ImmediatePopup = True
        Me.INDGleSearchBy.Properties.NullText = ""
        Me.INDGleSearchBy.Properties.ValueMember = "Item1"
        Me.INDGleSearchBy.Properties.View = Me.GridLookUpEdit1View
        Me.INDGleSearchBy.Size = New System.Drawing.Size(324, 28)
        Me.INDGleSearchBy.StyleController = Me.INDlyScheduleReports
        Me.INDGleSearchBy.TabIndex = 14
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDGleSearchBy, Nothing)
        Me.IndigoTextEdit3.SetTamañoMinimoString(Me.INDGleSearchBy, 0)
        Me.IndigoTextEdit2.SetTamañoMinimoString(Me.INDGleSearchBy, 0)
        Me.IndigoTextEdit4.SetTamañoMinimoString(Me.INDGleSearchBy, 0)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleSearchBy, 0)
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
        Me.GridLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGrcSearchBy})
        Me.GridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit1View.Name = "GridLookUpEdit1View"
        Me.GridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.GridLookUpEdit1View, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridLookUpEdit1View, False)
        '
        'INDGrcSearchBy
        '
        Me.INDGrcSearchBy.Caption = "Buscar Por"
        Me.INDGrcSearchBy.FieldName = "Item2"
        Me.INDGrcSearchBy.Name = "INDGrcSearchBy"
        Me.INDGrcSearchBy.Visible = True
        Me.INDGrcSearchBy.VisibleIndex = 0
        '
        'INDSleEmployee
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleEmployee, AppearanceObject1)
        Me.IndigoSearchLookUpControl2.SetAppearanceEmbeddedNavigator(Me.INDSleEmployee, AppearanceObject2)
        Me.IndigoSearchLookUpControl2.SetAppearanceTextFindControl(Me.INDSleEmployee, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleEmployee, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleEmployee, False)
        Me.IndigoSearchLookUpControl2.SetAppendButtonNavigator(Me.INDSleEmployee, False)
        Me.IndigoTextEdit2.SetApplyStyle(Me.INDSleEmployee, False)
        Me.IndigoTextEdit4.SetApplyStyle(Me.INDSleEmployee, False)
        Me.IndigoTextEdit3.SetApplyStyle(Me.INDSleEmployee, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleEmployee, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleEmployee, False)
        Me.IndigoTextEdit2.SetCampoObligatorio(Me.INDSleEmployee, False)
        Me.IndigoTextEdit3.SetCampoObligatorio(Me.INDSleEmployee, False)
        Me.IndigoTextEdit4.SetCampoObligatorio(Me.INDSleEmployee, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleEmployee, False)
        Me.IndigoSearchLookUpControl2.SetCancelEditButtonNavigator(Me.INDSleEmployee, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleEmployee, False)
        Me.IndigoSearchLookUpControl2.SetEditButtonNavigator(Me.INDSleEmployee, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleEmployee, False)
        Me.IndigoSearchLookUpControl2.SetEndEditButtonNavigator(Me.INDSleEmployee, False)
        Me.IndigoSearchLookUpControl2.SetExportButton(Me.INDSleEmployee, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleEmployee, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleEmployee, False)
        Me.IndigoSearchLookUpControl2.SetFirstButtonNavigator(Me.INDSleEmployee, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleEmployee, False)
        Me.IndigoSearchLookUpControl2.SetLastButtonNavigator(Me.INDSleEmployee, False)
        Me.INDSleEmployee.Location = New System.Drawing.Point(196, 347)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleEmployee, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit3.SetMascara(Me.INDSleEmployee, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit4.SetMascara(Me.INDSleEmployee, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit2.SetMascara(Me.INDSleEmployee, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleEmployee.Name = "INDSleEmployee"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleEmployee, False)
        Me.IndigoSearchLookUpControl2.SetNextButtonNavigator(Me.INDSleEmployee, False)
        Me.IndigoSearchLookUpControl2.SetNextPageButtonNavigator(Me.INDSleEmployee, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleEmployee, False)
        Me.IndigoSearchLookUpControl2.SetOpenForm(Me.INDSleEmployee, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleEmployee, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleEmployee, False)
        Me.IndigoSearchLookUpControl2.SetPrevButtonNavigator(Me.INDSleEmployee, False)
        Me.IndigoSearchLookUpControl2.SetPrevPageButtonNavigator(Me.INDSleEmployee, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleEmployee, False)
        Me.INDSleEmployee.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleEmployee.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleEmployee.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleEmployee.Properties.Appearance.Options.UseFont = True
        Me.INDSleEmployee.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "Limpiar selección (Supr)", Nothing, Nothing, True)})
        Me.INDSleEmployee.Properties.DisplayMember = "Name"
        Me.INDSleEmployee.Properties.NullText = ""
        Me.INDSleEmployee.Properties.PopupSizeable = False
        Me.INDSleEmployee.Properties.ShowFooter = False
        Me.INDSleEmployee.Properties.ValueMember = "Id"
        Me.INDSleEmployee.Properties.View = Me.GridView1
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleEmployee, False)
        Me.IndigoSearchLookUpControl2.SetRemoveButtonNavigator(Me.INDSleEmployee, False)
        Me.IndigoSearchLookUpControl2.SetSaveXmlGrid(Me.INDSleEmployee, True)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleEmployee, True)
        Me.IndigoSearchLookUpControl2.SetShowDeleteButton(Me.INDSleEmployee, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleEmployee, True)
        Me.IndigoSearchLookUpControl2.SetShowFindButton(Me.INDSleEmployee, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleEmployee, True)
        Me.INDSleEmployee.Size = New System.Drawing.Size(324, 28)
        Me.INDSleEmployee.StyleController = Me.INDlyScheduleReports
        Me.INDSleEmployee.TabIndex = 13
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleEmployee, Nothing)
        Me.IndigoSearchLookUpControl2.SetTagForm(Me.INDSleEmployee, Nothing)
        Me.IndigoTextEdit2.SetTamañoMinimoString(Me.INDSleEmployee, 0)
        Me.IndigoTextEdit3.SetTamañoMinimoString(Me.INDSleEmployee, 0)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleEmployee, 0)
        Me.IndigoTextEdit4.SetTamañoMinimoString(Me.INDSleEmployee, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleEmployee, "{0} - {1}")
        Me.IndigoSearchLookUpControl2.SetTextStringFormat(Me.INDSleEmployee, "{0} - {1}")
        Me.IndigoSearchLookUpControl2.SetTxtFindEnterEnabled(Me.INDSleEmployee, False)
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleEmployee, False)
        Me.IndigoSearchLookUpControl2.SetUseEmbeddedNavigator(Me.INDSleEmployee, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleEmployee, False)
        '
        'GridView1
        '
        Me.GridView1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView1.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView1.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView1.Appearance.FocusedRow.Options.UseForeColor = True
        Me.GridView1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.GroupRow.Options.UseFont = True
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGrcDocumentEmployee, Me.INDGrcNameEmployee})
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsCustomization.AllowGroup = False
        Me.GridView1.OptionsDetail.EnableMasterViewMode = False
        Me.GridView1.OptionsDetail.ShowDetailTabs = False
        Me.GridView1.OptionsFind.FindFilterColumns = "Nit"
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowDetailButtons = False
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.GridView1, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView1, False)
        '
        'INDGrcDocumentEmployee
        '
        Me.INDGrcDocumentEmployee.Caption = "Numero Identificación"
        Me.INDGrcDocumentEmployee.FieldName = "Nit"
        Me.INDGrcDocumentEmployee.Name = "INDGrcDocumentEmployee"
        Me.INDGrcDocumentEmployee.Visible = True
        Me.INDGrcDocumentEmployee.VisibleIndex = 0
        '
        'INDGrcNameEmployee
        '
        Me.INDGrcNameEmployee.Caption = "Nombre"
        Me.INDGrcNameEmployee.FieldName = "Name"
        Me.INDGrcNameEmployee.Name = "INDGrcNameEmployee"
        Me.INDGrcNameEmployee.Visible = True
        Me.INDGrcNameEmployee.VisibleIndex = 1
        '
        'INDProcessButton
        '
        Me.INDProcessButton.Enabled = False
        Me.INDProcessButton.Location = New System.Drawing.Point(195, 455)
        Me.INDProcessButton.MaximumSize = New System.Drawing.Size(324, 32)
        Me.INDProcessButton.MinimumSize = New System.Drawing.Size(324, 32)
        Me.INDProcessButton.Name = "INDProcessButton"
        Me.INDProcessButton.Size = New System.Drawing.Size(324, 32)
        Me.INDProcessButton.StyleController = Me.INDlyScheduleReports
        Me.INDProcessButton.TabIndex = 11
        Me.INDProcessButton.Text = "Procesar"
        '
        'INDCtrDateNavigator
        '
        Me.INDCtrDateNavigator.CtrCalendar = Nothing
        Me.INDCtrDateNavigator.HowShowControl = Presentation.Controls.CtrDateNavigator.EHowShowControl.Both
        Me.INDCtrDateNavigator.Location = New System.Drawing.Point(196, 167)
        Me.INDCtrDateNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDCtrDateNavigator.MaximumSize = New System.Drawing.Size(324, 65)
        Me.INDCtrDateNavigator.MinimumSize = New System.Drawing.Size(324, 0)
        Me.INDCtrDateNavigator.Name = "INDCtrDateNavigator"
        Me.INDCtrDateNavigator.Size = New System.Drawing.Size(324, 65)
        Me.INDCtrDateNavigator.TabIndex = 10
        Me.INDCtrDateNavigator.WithEvent = True
        '
        'INDCmbReports
        '
        Me.IndigoTextEdit3.SetApplyStyle(Me.INDCmbReports, False)
        Me.IndigoTextEdit4.SetApplyStyle(Me.INDCmbReports, False)
        Me.IndigoTextEdit2.SetApplyStyle(Me.INDCmbReports, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDCmbReports, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDCmbReports, False)
        Me.IndigoComboBoxEdit1.SetCampoObligatorio(Me.INDCmbReports, False)
        Me.IndigoTextEdit2.SetCampoObligatorio(Me.INDCmbReports, False)
        Me.IndigoTextEdit4.SetCampoObligatorio(Me.INDCmbReports, False)
        Me.IndigoComboBoxEdit2.SetCampoObligatorio(Me.INDCmbReports, False)
        Me.IndigoTextEdit3.SetCampoObligatorio(Me.INDCmbReports, False)
        Me.INDCmbReports.Location = New System.Drawing.Point(196, 59)
        Me.IndigoTextEdit4.SetMascara(Me.INDCmbReports, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit2.SetMascara(Me.INDCmbReports, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit3.SetMascara(Me.INDCmbReports, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit1.SetMascara(Me.INDCmbReports, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDCmbReports.MaximumSize = New System.Drawing.Size(0, 28)
        Me.INDCmbReports.MinimumSize = New System.Drawing.Size(0, 28)
        Me.INDCmbReports.Name = "INDCmbReports"
        Me.INDCmbReports.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDCmbReports.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDCmbReports.Properties.Appearance.Options.UseBackColor = True
        Me.INDCmbReports.Properties.Appearance.Options.UseFont = True
        Me.INDCmbReports.Properties.AppearanceDropDown.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDCmbReports.Properties.AppearanceDropDown.Options.UseFont = True
        Me.INDCmbReports.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDCmbReports.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDCmbReports.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDCmbReports.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDCmbReports.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDCmbReports.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDCmbReports.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDCmbReports.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Reporte Relación Nómina", "13", -1)})
        Me.INDCmbReports.Size = New System.Drawing.Size(324, 28)
        Me.INDCmbReports.StyleController = Me.INDlyScheduleReports
        Me.INDCmbReports.TabIndex = 9
        Me.IndigoTextEdit2.SetTamañoMinimoString(Me.INDCmbReports, 0)
        Me.IndigoTextEdit4.SetTamañoMinimoString(Me.INDCmbReports, 0)
        Me.IndigoTextEdit3.SetTamañoMinimoString(Me.INDCmbReports, 0)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDCmbReports, 0)
        '
        'INDsleGroup
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleGroup, AppearanceObject5)
        Me.IndigoSearchLookUpControl2.SetAppearanceEmbeddedNavigator(Me.INDsleGroup, AppearanceObject6)
        Me.IndigoSearchLookUpControl2.SetAppearanceTextFindControl(Me.INDsleGroup, AppearanceObject7)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleGroup, AppearanceObject8)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleGroup, False)
        Me.IndigoSearchLookUpControl2.SetAppendButtonNavigator(Me.INDsleGroup, False)
        Me.IndigoTextEdit2.SetApplyStyle(Me.INDsleGroup, False)
        Me.IndigoTextEdit4.SetApplyStyle(Me.INDsleGroup, False)
        Me.IndigoTextEdit3.SetApplyStyle(Me.INDsleGroup, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleGroup, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleGroup, False)
        Me.IndigoTextEdit2.SetCampoObligatorio(Me.INDsleGroup, False)
        Me.IndigoTextEdit3.SetCampoObligatorio(Me.INDsleGroup, False)
        Me.IndigoTextEdit4.SetCampoObligatorio(Me.INDsleGroup, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleGroup, False)
        Me.IndigoSearchLookUpControl2.SetCancelEditButtonNavigator(Me.INDsleGroup, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleGroup, False)
        Me.IndigoSearchLookUpControl2.SetEditButtonNavigator(Me.INDsleGroup, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleGroup, False)
        Me.IndigoSearchLookUpControl2.SetEndEditButtonNavigator(Me.INDsleGroup, False)
        Me.INDsleGroup.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl2.SetExportButton(Me.INDsleGroup, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleGroup, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleGroup, False)
        Me.IndigoSearchLookUpControl2.SetFirstButtonNavigator(Me.INDsleGroup, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleGroup, False)
        Me.IndigoSearchLookUpControl2.SetLastButtonNavigator(Me.INDsleGroup, False)
        Me.INDsleGroup.Location = New System.Drawing.Point(196, 311)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleGroup, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit3.SetMascara(Me.INDsleGroup, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit4.SetMascara(Me.INDsleGroup, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit2.SetMascara(Me.INDsleGroup, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleGroup.Name = "INDsleGroup"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleGroup, False)
        Me.IndigoSearchLookUpControl2.SetNextButtonNavigator(Me.INDsleGroup, False)
        Me.IndigoSearchLookUpControl2.SetNextPageButtonNavigator(Me.INDsleGroup, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleGroup, False)
        Me.IndigoSearchLookUpControl2.SetOpenForm(Me.INDsleGroup, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleGroup, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleGroup, False)
        Me.IndigoSearchLookUpControl2.SetPrevButtonNavigator(Me.INDsleGroup, False)
        Me.IndigoSearchLookUpControl2.SetPrevPageButtonNavigator(Me.INDsleGroup, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleGroup, False)
        Me.INDsleGroup.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleGroup.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleGroup.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleGroup.Properties.Appearance.Options.UseFont = True
        Me.INDsleGroup.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleGroup.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleGroup.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleGroup.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleGroup.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleGroup.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleGroup.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "Limpiar selección (Supr)", Nothing, Nothing, True)})
        Me.INDsleGroup.Properties.DisplayMember = "Descripcion"
        Me.INDsleGroup.Properties.NullText = " "
        Me.INDsleGroup.Properties.PopupSizeable = False
        Me.INDsleGroup.Properties.ShowFooter = False
        Me.INDsleGroup.Properties.ValueMember = "Id"
        Me.INDsleGroup.Properties.View = Me.SearchLookUpEdit2View
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleGroup, False)
        Me.IndigoSearchLookUpControl2.SetRemoveButtonNavigator(Me.INDsleGroup, False)
        Me.IndigoSearchLookUpControl2.SetSaveXmlGrid(Me.INDsleGroup, True)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleGroup, True)
        Me.IndigoSearchLookUpControl2.SetShowDeleteButton(Me.INDsleGroup, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleGroup, True)
        Me.IndigoSearchLookUpControl2.SetShowFindButton(Me.INDsleGroup, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleGroup, True)
        Me.INDsleGroup.Size = New System.Drawing.Size(324, 28)
        Me.INDsleGroup.StyleController = Me.INDlyScheduleReports
        Me.INDsleGroup.TabIndex = 8
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleGroup, Nothing)
        Me.IndigoSearchLookUpControl2.SetTagForm(Me.INDsleGroup, Nothing)
        Me.IndigoTextEdit2.SetTamañoMinimoString(Me.INDsleGroup, 0)
        Me.IndigoTextEdit3.SetTamañoMinimoString(Me.INDsleGroup, 0)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleGroup, 0)
        Me.IndigoTextEdit4.SetTamañoMinimoString(Me.INDsleGroup, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleGroup, "{0} - {1}")
        Me.IndigoSearchLookUpControl2.SetTextStringFormat(Me.INDsleGroup, "{0} - {1}")
        Me.IndigoSearchLookUpControl2.SetTxtFindEnterEnabled(Me.INDsleGroup, False)
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleGroup, False)
        Me.IndigoSearchLookUpControl2.SetUseEmbeddedNavigator(Me.INDsleGroup, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleGroup, False)
        '
        'SearchLookUpEdit2View
        '
        Me.SearchLookUpEdit2View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit2View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit2View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit2View.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit2View.Appearance.FocusedRow.Options.UseForeColor = True
        Me.SearchLookUpEdit2View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit2View.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit2View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit2View.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit2View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit2View.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit2View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn5, Me.GridColumn6})
        Me.SearchLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit2View.Name = "SearchLookUpEdit2View"
        Me.SearchLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit2View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit2View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit2View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit2View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.SearchLookUpEdit2View, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit2View, False)
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Código"
        Me.GridColumn5.FieldName = "Codigo"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 0
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Descripcion"
        Me.GridColumn6.FieldName = "Descripcion"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 1
        '
        'INDsleFunctionalUnit
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleFunctionalUnit, AppearanceObject9)
        Me.IndigoSearchLookUpControl2.SetAppearanceEmbeddedNavigator(Me.INDsleFunctionalUnit, AppearanceObject10)
        Me.IndigoSearchLookUpControl2.SetAppearanceTextFindControl(Me.INDsleFunctionalUnit, AppearanceObject11)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleFunctionalUnit, AppearanceObject12)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl2.SetAppendButtonNavigator(Me.INDsleFunctionalUnit, False)
        Me.IndigoTextEdit2.SetApplyStyle(Me.INDsleFunctionalUnit, False)
        Me.IndigoTextEdit4.SetApplyStyle(Me.INDsleFunctionalUnit, False)
        Me.IndigoTextEdit3.SetApplyStyle(Me.INDsleFunctionalUnit, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleFunctionalUnit, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleFunctionalUnit, False)
        Me.IndigoTextEdit2.SetCampoObligatorio(Me.INDsleFunctionalUnit, False)
        Me.IndigoTextEdit3.SetCampoObligatorio(Me.INDsleFunctionalUnit, False)
        Me.IndigoTextEdit4.SetCampoObligatorio(Me.INDsleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl2.SetCancelEditButtonNavigator(Me.INDsleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl2.SetEditButtonNavigator(Me.INDsleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl2.SetEndEditButtonNavigator(Me.INDsleFunctionalUnit, False)
        Me.INDsleFunctionalUnit.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl2.SetExportButton(Me.INDsleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl2.SetFirstButtonNavigator(Me.INDsleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl2.SetLastButtonNavigator(Me.INDsleFunctionalUnit, False)
        Me.INDsleFunctionalUnit.Location = New System.Drawing.Point(196, 239)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleFunctionalUnit, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit3.SetMascara(Me.INDsleFunctionalUnit, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit4.SetMascara(Me.INDsleFunctionalUnit, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit2.SetMascara(Me.INDsleFunctionalUnit, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleFunctionalUnit.Name = "INDsleFunctionalUnit"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl2.SetNextButtonNavigator(Me.INDsleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl2.SetNextPageButtonNavigator(Me.INDsleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl2.SetOpenForm(Me.INDsleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl2.SetPrevButtonNavigator(Me.INDsleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl2.SetPrevPageButtonNavigator(Me.INDsleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleFunctionalUnit, False)
        Me.INDsleFunctionalUnit.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleFunctionalUnit.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleFunctionalUnit.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleFunctionalUnit.Properties.Appearance.Options.UseFont = True
        Me.INDsleFunctionalUnit.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleFunctionalUnit.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleFunctionalUnit.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleFunctionalUnit.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleFunctionalUnit.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleFunctionalUnit.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleFunctionalUnit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "Limpiar selección (Supr)", Nothing, Nothing, True)})
        Me.INDsleFunctionalUnit.Properties.DisplayMember = "Descripcion"
        Me.INDsleFunctionalUnit.Properties.NullText = " "
        Me.INDsleFunctionalUnit.Properties.PopupSizeable = False
        Me.INDsleFunctionalUnit.Properties.ShowFooter = False
        Me.INDsleFunctionalUnit.Properties.ValueMember = "Id"
        Me.INDsleFunctionalUnit.Properties.View = Me.SearchLookUpEdit1View
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl2.SetRemoveButtonNavigator(Me.INDsleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl2.SetSaveXmlGrid(Me.INDsleFunctionalUnit, True)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleFunctionalUnit, True)
        Me.IndigoSearchLookUpControl2.SetShowDeleteButton(Me.INDsleFunctionalUnit, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleFunctionalUnit, True)
        Me.IndigoSearchLookUpControl2.SetShowFindButton(Me.INDsleFunctionalUnit, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleFunctionalUnit, True)
        Me.INDsleFunctionalUnit.Size = New System.Drawing.Size(324, 28)
        Me.INDsleFunctionalUnit.StyleController = Me.INDlyScheduleReports
        Me.INDsleFunctionalUnit.TabIndex = 7
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleFunctionalUnit, Nothing)
        Me.IndigoSearchLookUpControl2.SetTagForm(Me.INDsleFunctionalUnit, Nothing)
        Me.IndigoTextEdit2.SetTamañoMinimoString(Me.INDsleFunctionalUnit, 0)
        Me.IndigoTextEdit3.SetTamañoMinimoString(Me.INDsleFunctionalUnit, 0)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleFunctionalUnit, 0)
        Me.IndigoTextEdit4.SetTamañoMinimoString(Me.INDsleFunctionalUnit, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleFunctionalUnit, "{0} - {1}")
        Me.IndigoSearchLookUpControl2.SetTextStringFormat(Me.INDsleFunctionalUnit, "{0} - {1}")
        Me.IndigoSearchLookUpControl2.SetTxtFindEnterEnabled(Me.INDsleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl2.SetUseEmbeddedNavigator(Me.INDsleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleFunctionalUnit, False)
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseForeColor = True
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Código"
        Me.GridColumn1.FieldName = "Codigo"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 218
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Descripcion"
        Me.GridColumn2.FieldName = "Descripcion"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 990
        '
        'INDdeEndingDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdeEndingDate, False)
        Me.IndigoTextEdit2.SetApplyStyle(Me.INDdeEndingDate, False)
        Me.IndigoTextEdit4.SetApplyStyle(Me.INDdeEndingDate, False)
        Me.IndigoTextEdit3.SetApplyStyle(Me.INDdeEndingDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdeEndingDate, False)
        Me.IndigoTextEdit4.SetCampoObligatorio(Me.INDdeEndingDate, False)
        Me.IndigoTextEdit3.SetCampoObligatorio(Me.INDdeEndingDate, False)
        Me.IndigoTextEdit2.SetCampoObligatorio(Me.INDdeEndingDate, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDdeEndingDate, False)
        Me.INDdeEndingDate.EditValue = Nothing
        Me.INDdeEndingDate.EnterMoveNextControl = True
        Me.INDdeEndingDate.Location = New System.Drawing.Point(196, 131)
        Me.IndigoTextEdit3.SetMascara(Me.INDdeEndingDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit4.SetMascara(Me.INDdeEndingDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit2.SetMascara(Me.INDdeEndingDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit1.SetMascara(Me.INDdeEndingDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDdeEndingDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDdeEndingDate.Name = "INDdeEndingDate"
        Me.INDdeEndingDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDdeEndingDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdeEndingDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDdeEndingDate.Properties.Appearance.Options.UseFont = True
        Me.INDdeEndingDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDdeEndingDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDdeEndingDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdeEndingDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDdeEndingDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDdeEndingDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdeEndingDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeEndingDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeEndingDate.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDdeEndingDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDdeEndingDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdeEndingDate.Size = New System.Drawing.Size(324, 28)
        Me.INDdeEndingDate.StyleController = Me.INDlyScheduleReports
        Me.INDdeEndingDate.TabIndex = 5
        Me.IndigoTextEdit4.SetTamañoMinimoString(Me.INDdeEndingDate, 0)
        Me.IndigoTextEdit3.SetTamañoMinimoString(Me.INDdeEndingDate, 0)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdeEndingDate, 0)
        Me.IndigoTextEdit2.SetTamañoMinimoString(Me.INDdeEndingDate, 0)
        '
        'INDdeInitialDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdeInitialDate, False)
        Me.IndigoTextEdit2.SetApplyStyle(Me.INDdeInitialDate, False)
        Me.IndigoTextEdit4.SetApplyStyle(Me.INDdeInitialDate, False)
        Me.IndigoTextEdit3.SetApplyStyle(Me.INDdeInitialDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdeInitialDate, False)
        Me.IndigoTextEdit4.SetCampoObligatorio(Me.INDdeInitialDate, False)
        Me.IndigoTextEdit3.SetCampoObligatorio(Me.INDdeInitialDate, False)
        Me.IndigoTextEdit2.SetCampoObligatorio(Me.INDdeInitialDate, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDdeInitialDate, False)
        Me.INDdeInitialDate.EditValue = Nothing
        Me.INDdeInitialDate.EnterMoveNextControl = True
        Me.INDdeInitialDate.Location = New System.Drawing.Point(196, 95)
        Me.IndigoTextEdit3.SetMascara(Me.INDdeInitialDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit4.SetMascara(Me.INDdeInitialDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit2.SetMascara(Me.INDdeInitialDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit1.SetMascara(Me.INDdeInitialDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDdeInitialDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDdeInitialDate.Name = "INDdeInitialDate"
        Me.INDdeInitialDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDdeInitialDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdeInitialDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDdeInitialDate.Properties.Appearance.Options.UseFont = True
        Me.INDdeInitialDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDdeInitialDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDdeInitialDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdeInitialDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDdeInitialDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDdeInitialDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdeInitialDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeInitialDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeInitialDate.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDdeInitialDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDdeInitialDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdeInitialDate.Size = New System.Drawing.Size(324, 28)
        Me.INDdeInitialDate.StyleController = Me.INDlyScheduleReports
        Me.INDdeInitialDate.TabIndex = 4
        Me.IndigoTextEdit4.SetTamañoMinimoString(Me.INDdeInitialDate, 0)
        Me.IndigoTextEdit3.SetTamañoMinimoString(Me.INDdeInitialDate, 0)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdeInitialDate, 0)
        Me.IndigoTextEdit2.SetTamañoMinimoString(Me.INDdeInitialDate, 0)
        '
        'INDCmbStatusPayroll
        '
        Me.IndigoTextEdit3.SetApplyStyle(Me.INDCmbStatusPayroll, False)
        Me.IndigoTextEdit4.SetApplyStyle(Me.INDCmbStatusPayroll, False)
        Me.IndigoTextEdit2.SetApplyStyle(Me.INDCmbStatusPayroll, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDCmbStatusPayroll, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDCmbStatusPayroll, False)
        Me.IndigoComboBoxEdit1.SetCampoObligatorio(Me.INDCmbStatusPayroll, False)
        Me.IndigoTextEdit2.SetCampoObligatorio(Me.INDCmbStatusPayroll, False)
        Me.IndigoTextEdit4.SetCampoObligatorio(Me.INDCmbStatusPayroll, False)
        Me.IndigoComboBoxEdit2.SetCampoObligatorio(Me.INDCmbStatusPayroll, False)
        Me.IndigoTextEdit3.SetCampoObligatorio(Me.INDCmbStatusPayroll, False)
        Me.INDCmbStatusPayroll.Location = New System.Drawing.Point(196, 383)
        Me.IndigoTextEdit4.SetMascara(Me.INDCmbStatusPayroll, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit2.SetMascara(Me.INDCmbStatusPayroll, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit3.SetMascara(Me.INDCmbStatusPayroll, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit1.SetMascara(Me.INDCmbStatusPayroll, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDCmbStatusPayroll.MaximumSize = New System.Drawing.Size(0, 28)
        Me.INDCmbStatusPayroll.MinimumSize = New System.Drawing.Size(0, 28)
        Me.INDCmbStatusPayroll.Name = "INDCmbStatusPayroll"
        Me.INDCmbStatusPayroll.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDCmbStatusPayroll.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDCmbStatusPayroll.Properties.Appearance.Options.UseBackColor = True
        Me.INDCmbStatusPayroll.Properties.Appearance.Options.UseFont = True
        Me.INDCmbStatusPayroll.Properties.AppearanceDropDown.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDCmbStatusPayroll.Properties.AppearanceDropDown.Options.UseFont = True
        Me.INDCmbStatusPayroll.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDCmbStatusPayroll.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDCmbStatusPayroll.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDCmbStatusPayroll.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDCmbStatusPayroll.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDCmbStatusPayroll.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDCmbStatusPayroll.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDCmbStatusPayroll.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Sin Confirmar", " ", -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Confirmada", Global.Microsoft.VisualBasic.ChrW(67), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Saldo Inicial", Global.Microsoft.VisualBasic.ChrW(83), -1)})
        Me.INDCmbStatusPayroll.Size = New System.Drawing.Size(324, 28)
        Me.INDCmbStatusPayroll.StyleController = Me.INDlyScheduleReports
        Me.INDCmbStatusPayroll.TabIndex = 12
        Me.IndigoTextEdit2.SetTamañoMinimoString(Me.INDCmbStatusPayroll, 0)
        Me.IndigoTextEdit4.SetTamañoMinimoString(Me.INDCmbStatusPayroll, 0)
        Me.IndigoTextEdit3.SetTamañoMinimoString(Me.INDCmbStatusPayroll, 0)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDCmbStatusPayroll, 0)
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, False)
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyGrFiltros})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(804, 682)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlyGrFiltros
        '
        Me.INDlyGrFiltros.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGrFiltros.AppearanceGroup.Options.UseFont = True
        Me.INDlyGrFiltros.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGrFiltros.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyGrFiltros.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrFiltros.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyGrFiltros.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlyGrFiltros.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlyGrFiltros.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrFiltros.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyGrFiltros.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrFiltros.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlyGrFiltros.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrFiltros.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyGrFiltros, False)
        Me.INDlyGrFiltros.CustomizationFormText = "Filtros"
        Me.INDlyGrFiltros.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemFunctionalUnit, Me.INDlyItemGroup, Me.INDlyItemCmbReports, Me.INDlyItemInitialDate, Me.INDlyItemEndingDate, Me.INDlyItemDateNavigator, Me.INDlyItemButton, Me.INDlyItemPayrollLiquidationConfirm, Me.INDLciEmployee, Me.INDLciSearchBy, Me.INDLciConcept})
        Me.INDlyGrFiltros.Location = New System.Drawing.Point(0, 0)
        Me.INDlyGrFiltros.Name = "INDlyGrFiltros"
        Me.INDlyGrFiltros.Size = New System.Drawing.Size(784, 662)
        Me.INDlyGrFiltros.Text = "Filtros"
        '
        'INDlyItemFunctionalUnit
        '
        Me.INDlyItemFunctionalUnit.Control = Me.INDsleFunctionalUnit
        Me.INDlyItemFunctionalUnit.CustomizationFormText = "Unidad Funcional"
        Me.INDlyItemFunctionalUnit.Location = New System.Drawing.Point(0, 180)
        Me.INDlyItemFunctionalUnit.MaxSize = New System.Drawing.Size(500, 36)
        Me.INDlyItemFunctionalUnit.MinSize = New System.Drawing.Size(500, 36)
        Me.INDlyItemFunctionalUnit.Name = "INDlyItemFunctionalUnit"
        Me.INDlyItemFunctionalUnit.Size = New System.Drawing.Size(760, 36)
        Me.INDlyItemFunctionalUnit.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemFunctionalUnit.Text = "Unidad Funcional"
        Me.INDlyItemFunctionalUnit.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemFunctionalUnit.TextSize = New System.Drawing.Size(160, 21)
        Me.INDlyItemFunctionalUnit.TextToControlDistance = 12
        Me.INDlyItemFunctionalUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlyItemGroup
        '
        Me.INDlyItemGroup.Control = Me.INDsleGroup
        Me.INDlyItemGroup.CustomizationFormText = "Grupos"
        Me.INDlyItemGroup.Location = New System.Drawing.Point(0, 252)
        Me.INDlyItemGroup.MaxSize = New System.Drawing.Size(500, 36)
        Me.INDlyItemGroup.MinSize = New System.Drawing.Size(500, 36)
        Me.INDlyItemGroup.Name = "INDlyItemGroup"
        Me.INDlyItemGroup.Size = New System.Drawing.Size(760, 36)
        Me.INDlyItemGroup.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemGroup.Text = "Grupos"
        Me.INDlyItemGroup.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemGroup.TextSize = New System.Drawing.Size(160, 21)
        Me.INDlyItemGroup.TextToControlDistance = 12
        Me.INDlyItemGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlyItemCmbReports
        '
        Me.INDlyItemCmbReports.Control = Me.INDCmbReports
        Me.INDlyItemCmbReports.CustomizationFormText = "Reportes"
        Me.INDlyItemCmbReports.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCmbReports.MaxSize = New System.Drawing.Size(500, 36)
        Me.INDlyItemCmbReports.MinSize = New System.Drawing.Size(500, 36)
        Me.INDlyItemCmbReports.Name = "INDlyItemCmbReports"
        Me.INDlyItemCmbReports.Size = New System.Drawing.Size(760, 36)
        Me.INDlyItemCmbReports.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCmbReports.Text = "Reportes"
        Me.INDlyItemCmbReports.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCmbReports.TextSize = New System.Drawing.Size(160, 20)
        Me.INDlyItemCmbReports.TextToControlDistance = 12
        '
        'INDlyItemInitialDate
        '
        Me.INDlyItemInitialDate.Control = Me.INDdeInitialDate
        Me.INDlyItemInitialDate.CustomizationFormText = "Fecha Inicial"
        Me.INDlyItemInitialDate.Location = New System.Drawing.Point(0, 36)
        Me.INDlyItemInitialDate.MaxSize = New System.Drawing.Size(500, 36)
        Me.INDlyItemInitialDate.MinSize = New System.Drawing.Size(500, 36)
        Me.INDlyItemInitialDate.Name = "INDlyItemInitialDate"
        Me.INDlyItemInitialDate.Size = New System.Drawing.Size(760, 36)
        Me.INDlyItemInitialDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemInitialDate.Text = "Fecha Inicial"
        Me.INDlyItemInitialDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemInitialDate.TextSize = New System.Drawing.Size(160, 21)
        Me.INDlyItemInitialDate.TextToControlDistance = 12
        '
        'INDlyItemEndingDate
        '
        Me.INDlyItemEndingDate.Control = Me.INDdeEndingDate
        Me.INDlyItemEndingDate.CustomizationFormText = "Fecha Final"
        Me.INDlyItemEndingDate.Location = New System.Drawing.Point(0, 72)
        Me.INDlyItemEndingDate.MaxSize = New System.Drawing.Size(500, 36)
        Me.INDlyItemEndingDate.MinSize = New System.Drawing.Size(500, 36)
        Me.INDlyItemEndingDate.Name = "INDlyItemEndingDate"
        Me.INDlyItemEndingDate.Size = New System.Drawing.Size(760, 36)
        Me.INDlyItemEndingDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemEndingDate.Text = "Fecha Final"
        Me.INDlyItemEndingDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemEndingDate.TextSize = New System.Drawing.Size(160, 21)
        Me.INDlyItemEndingDate.TextToControlDistance = 12
        '
        'INDlyItemDateNavigator
        '
        Me.INDlyItemDateNavigator.Control = Me.INDCtrDateNavigator
        Me.INDlyItemDateNavigator.CustomizationFormText = "Fecha"
        Me.INDlyItemDateNavigator.Location = New System.Drawing.Point(0, 108)
        Me.INDlyItemDateNavigator.MaxSize = New System.Drawing.Size(760, 72)
        Me.INDlyItemDateNavigator.MinSize = New System.Drawing.Size(760, 72)
        Me.INDlyItemDateNavigator.Name = "INDlyItemDateNavigator"
        Me.INDlyItemDateNavigator.Size = New System.Drawing.Size(760, 72)
        Me.INDlyItemDateNavigator.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemDateNavigator.Text = "Fecha"
        Me.INDlyItemDateNavigator.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemDateNavigator.TextSize = New System.Drawing.Size(160, 21)
        Me.INDlyItemDateNavigator.TextToControlDistance = 12
        Me.INDlyItemDateNavigator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlyItemButton
        '
        Me.INDlyItemButton.Control = Me.INDProcessButton
        Me.INDlyItemButton.ControlAlignment = System.Drawing.ContentAlignment.TopRight
        Me.INDlyItemButton.CustomizationFormText = "INDlyItemButton"
        Me.INDlyItemButton.Location = New System.Drawing.Point(0, 396)
        Me.INDlyItemButton.MinSize = New System.Drawing.Size(479, 32)
        Me.INDlyItemButton.Name = "INDlyItemButton"
        Me.INDlyItemButton.Padding = New DevExpress.XtraLayout.Utils.Padding(173, 2, 2, 2)
        Me.INDlyItemButton.Size = New System.Drawing.Size(760, 207)
        Me.INDlyItemButton.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemButton.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemButton.TextVisible = False
        '
        'INDlyItemPayrollLiquidationConfirm
        '
        Me.INDlyItemPayrollLiquidationConfirm.Control = Me.INDCmbStatusPayroll
        Me.INDlyItemPayrollLiquidationConfirm.CustomizationFormText = "Estado Nómina"
        Me.INDlyItemPayrollLiquidationConfirm.Location = New System.Drawing.Point(0, 324)
        Me.INDlyItemPayrollLiquidationConfirm.MaxSize = New System.Drawing.Size(500, 36)
        Me.INDlyItemPayrollLiquidationConfirm.MinSize = New System.Drawing.Size(500, 36)
        Me.INDlyItemPayrollLiquidationConfirm.Name = "INDlyItemPayrollLiquidationConfirm"
        Me.INDlyItemPayrollLiquidationConfirm.Size = New System.Drawing.Size(760, 36)
        Me.INDlyItemPayrollLiquidationConfirm.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemPayrollLiquidationConfirm.Text = "Estado Nómina"
        Me.INDlyItemPayrollLiquidationConfirm.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemPayrollLiquidationConfirm.TextSize = New System.Drawing.Size(160, 21)
        Me.INDlyItemPayrollLiquidationConfirm.TextToControlDistance = 12
        '
        'INDLciEmployee
        '
        Me.INDLciEmployee.Control = Me.INDSleEmployee
        Me.INDLciEmployee.CustomizationFormText = "Empleados"
        Me.INDLciEmployee.Location = New System.Drawing.Point(0, 288)
        Me.INDLciEmployee.MaxSize = New System.Drawing.Size(500, 36)
        Me.INDLciEmployee.MinSize = New System.Drawing.Size(500, 36)
        Me.INDLciEmployee.Name = "INDLciEmployee"
        Me.INDLciEmployee.Size = New System.Drawing.Size(760, 36)
        Me.INDLciEmployee.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciEmployee.Text = "Empleados"
        Me.INDLciEmployee.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciEmployee.TextSize = New System.Drawing.Size(160, 21)
        Me.INDLciEmployee.TextToControlDistance = 12
        '
        'INDLciSearchBy
        '
        Me.INDLciSearchBy.Control = Me.INDGleSearchBy
        Me.INDLciSearchBy.CustomizationFormText = "Buscar Por"
        Me.INDLciSearchBy.Location = New System.Drawing.Point(0, 216)
        Me.INDLciSearchBy.MaxSize = New System.Drawing.Size(500, 36)
        Me.INDLciSearchBy.MinSize = New System.Drawing.Size(500, 36)
        Me.INDLciSearchBy.Name = "INDLciSearchBy"
        Me.INDLciSearchBy.Size = New System.Drawing.Size(760, 36)
        Me.INDLciSearchBy.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciSearchBy.Text = "Buscar Por"
        Me.INDLciSearchBy.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciSearchBy.TextSize = New System.Drawing.Size(160, 21)
        Me.INDLciSearchBy.TextToControlDistance = 12
        '
        'INDLciConcept
        '
        Me.INDLciConcept.Control = Me.INDSleConcepts
        Me.INDLciConcept.Location = New System.Drawing.Point(0, 360)
        Me.INDLciConcept.MaxSize = New System.Drawing.Size(500, 36)
        Me.INDLciConcept.MinSize = New System.Drawing.Size(500, 36)
        Me.INDLciConcept.Name = "INDLciConcept"
        Me.INDLciConcept.Size = New System.Drawing.Size(760, 36)
        Me.INDLciConcept.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciConcept.Text = "Concepto"
        Me.INDLciConcept.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciConcept.TextSize = New System.Drawing.Size(160, 21)
        Me.INDLciConcept.TextToControlDistance = 12
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Codigo"
        Me.GridColumn3.FieldName = "Codigo"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 0
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Descripcion"
        Me.GridColumn4.FieldName = "Descripcion"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 1
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
        Me.INDDvReport.Location = New System.Drawing.Point(72, 2)
        Me.INDDvReport.Name = "INDDvReport"
        Me.INDDvReport.Size = New System.Drawing.Size(930, 678)
        Me.INDDvReport.TabIndex = 1
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 31)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 621)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(930, 31)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 621)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 652)
        Me.barDockControlBottom.Size = New System.Drawing.Size(930, 26)
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(930, 31)
        '
        'INDCtrNavigation
        '
        Me.INDCtrNavigation.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDCtrNavigation.Dock = System.Windows.Forms.DockStyle.Left
        Me.INDCtrNavigation.LayoutControl = Me.INDlyScheduleReports
        Me.INDCtrNavigation.Location = New System.Drawing.Point(2, 8)
        Me.INDCtrNavigation.Margin = New System.Windows.Forms.Padding(0)
        Me.INDCtrNavigation.Name = "INDCtrNavigation"
        Me.INDCtrNavigation.Size = New System.Drawing.Size(200, 682)
        Me.INDCtrNavigation.TabIndex = 12
        Me.INDCtrNavigation.UseDisabledStatePainter = False
        '
        'INDPcReport
        '
        Me.INDPcReport.Controls.Add(Me.INDDvReport)
        Me.INDPcReport.Controls.Add(Me.INDCnReport)
        Me.INDPcReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDPcReport.Location = New System.Drawing.Point(2, 8)
        Me.INDPcReport.Name = "INDPcReport"
        Me.INDPcReport.Size = New System.Drawing.Size(1004, 682)
        Me.INDPcReport.TabIndex = 13
        '
        'INDCnReport
        '
        Me.INDCnReport.Dock = System.Windows.Forms.DockStyle.Left
        Me.INDCnReport.Location = New System.Drawing.Point(2, 2)
        Me.INDCnReport.Name = "INDCnReport"
        Me.INDCnReport.Size = New System.Drawing.Size(70, 678)
        Me.INDCnReport.TabIndex = 0
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
        'FrmScheduleReports
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 717)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "FrmScheduleReports"
        Me.Opacity = 1.0R
        Me.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.Tag = "592"
        Me.Text = "Reportes de Nómina"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RecentlyUsedItemsComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DesignRepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyScheduleReports, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyScheduleReports.ResumeLayout(False)
        CType(Me.INDSleConcepts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEditExView6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleSearchBy.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleEmployee.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDCmbReports.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleGroup.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit2View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleFunctionalUnit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdeEndingDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdeEndingDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdeInitialDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdeInitialDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDCmbStatusPayroll.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGrFiltros, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemFunctionalUnit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCmbReports, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemInitialDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemEndingDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDateNavigator, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemButton, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemPayrollLiquidationConfirm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciEmployee, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciSearchBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciConcept, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoRadioGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoComboBoxEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoComboBoxEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDocumentViewer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDDvReport.ResumeLayout(False)
        CType(Me.INDCtrNavigation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPcReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPcReport.ResumeLayout(False)
        CType(Me.DocumentViewerBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PrintPreviewRepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemProgressBar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemZoomTrackBar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDlyScheduleReports As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents INDsleGroup As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents SearchLookUpEdit2View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDsleFunctionalUnit As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents IndigoRadioGroup1 As Presentation.Controls.IndigoRadioGroup
    Friend WithEvents INDdeEndingDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents INDdeInitialDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents INDlyGrFiltros As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemInitialDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemEndingDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemFunctionalUnit As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemGroup As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents RecentlyUsedItemsComboBox1 As DevExpress.XtraReports.UserDesigner.RecentlyUsedItemsComboBox
    Friend WithEvents DesignRepositoryItemComboBox1 As DevExpress.XtraReports.UserDesigner.DesignRepositoryItemComboBox
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDCmbReports As DevExpress.XtraEditors.ImageComboBoxEdit
    Friend WithEvents INDlyItemCmbReports As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDCtrDateNavigator As Presentation.Controls.CtrDateNavigator
    Friend WithEvents INDlyItemDateNavigator As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDProcessButton As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDlyItemButton As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoComboBoxEdit1 As Presentation.Controls.IndigoComboBoxEdit
    Friend WithEvents INDlyItemPayrollLiquidationConfirm As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDCmbStatusPayroll As DevExpress.XtraEditors.ImageComboBoxEdit
    Friend WithEvents INDSleEmployee As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciEmployee As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGleSearchBy As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciSearchBy As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGrcSearchBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents INDGrcDocumentEmployee As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGrcNameEmployee As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoTextEdit2 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoComboBoxEdit2 As Presentation.Controls.IndigoComboBoxEdit
    Friend WithEvents IndigoTextEdit4 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoTextEdit3 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoGridView2 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoSearchLookUpControl2 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents INDSleConcepts As Presentation.Controls.SearchLookUpEditEx
    Friend WithEvents SearchLookUpEditExView6 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciConcept As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoDocumentViewer1 As Presentation.Controls.IndigoDocumentViewer
    Friend WithEvents INDCtrNavigation As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDPcReport As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDCnReport As Presentation.Controls.CtrNavigation
    Friend WithEvents INDDvReport As DevExpress.XtraPrinting.Preview.DocumentViewer
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
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
End Class
