Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmInteropCostSettings
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmInteropCostSettings))
        Dim SuperToolTip1 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
        Dim ToolTipItem1 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDLcRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDsleAccountingCosts = New Presentation.Controls.CtrYesNo()
        Me.CtrYesNo4View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGleEstimationLaborType = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleJournalVoucherType = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CtrDateNavigator1 = New Presentation.Controls.CtrDateNavigator()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcgMainData = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciYearMonth = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciJournalVoucherType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciEstimationLaborType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemAccountingCost = New DevExpress.XtraLayout.LayoutControlItem()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.GridColumn267 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcRoot.SuspendLayout()
        CType(Me.INDsleAccountingCosts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleAccountingCosts.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrYesNo4View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleEstimationLaborType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleJournalVoucherType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgMainData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciYearMonth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciJournalVoucherType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciEstimationLaborType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemAccountingCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLcRoot)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 118)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1180, 419)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1180, 94)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1180, 94)
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.Appearance.BackColor = System.Drawing.SystemColors.Control
        Me.CtrNavigationControlPanel1.Appearance.Options.UseBackColor = True
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.INDLcRoot
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 410)
        Me.CtrNavigationControlPanel1.TabIndex = 0
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'INDLcRoot
        '
        Me.INDLcRoot.Controls.Add(Me.INDsleAccountingCosts)
        Me.INDLcRoot.Controls.Add(Me.INDGleEstimationLaborType)
        Me.INDLcRoot.Controls.Add(Me.INDSleJournalVoucherType)
        Me.INDLcRoot.Controls.Add(Me.CtrDateNavigator1)
        Me.INDLcRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcRoot.Location = New System.Drawing.Point(202, 7)
        Me.INDLcRoot.Name = "INDLcRoot"
        Me.INDLcRoot.Root = Me.LayoutControlGroup1
        Me.INDLcRoot.Size = New System.Drawing.Size(976, 410)
        Me.INDLcRoot.TabIndex = 1
        Me.INDLcRoot.Text = "LayoutControl1"
        '
        'INDsleAccountingCosts
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDsleAccountingCosts, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleAccountingCosts, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleAccountingCosts, False)
        Me.INDsleAccountingCosts.EnterMoveNextControl = True
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDsleAccountingCosts, False)
        Me.INDsleAccountingCosts.Location = New System.Drawing.Point(24, 155)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleAccountingCosts, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleAccountingCosts.Name = "INDsleAccountingCosts"
        Me.INDsleAccountingCosts.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleAccountingCosts.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleAccountingCosts.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleAccountingCosts.Properties.Appearance.Options.UseFont = True
        Me.INDsleAccountingCosts.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleAccountingCosts.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleAccountingCosts.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleAccountingCosts.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleAccountingCosts.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleAccountingCosts.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleAccountingCosts.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleAccountingCosts.Properties.DataSource = CType(resources.GetObject("INDsleAccountingCosts.Properties.DataSource"), Object)
        Me.INDsleAccountingCosts.Properties.DisplayMember = "Item2"
        Me.INDsleAccountingCosts.Properties.ImmediatePopup = True
        Me.INDsleAccountingCosts.Properties.NullText = ""
        Me.INDsleAccountingCosts.Properties.ValueMember = "Item1"
        Me.INDsleAccountingCosts.Properties.View = Me.CtrYesNo4View
        Me.INDsleAccountingCosts.Size = New System.Drawing.Size(386, 28)
        Me.INDsleAccountingCosts.StyleController = Me.INDLcRoot
        ToolTipItem1.Text = "Obligación única grupo CxP y CxP."
        SuperToolTip1.Items.Add(ToolTipItem1)
        Me.INDsleAccountingCosts.SuperTip = SuperToolTip1
        Me.INDsleAccountingCosts.TabIndex = 1
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDsleAccountingCosts, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleAccountingCosts, 0)
        '
        'CtrYesNo4View
        '
        Me.CtrYesNo4View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.CtrYesNo4View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.CtrYesNo4View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.CtrYesNo4View.Appearance.FocusedRow.Options.UseFont = True
        Me.CtrYesNo4View.Appearance.FocusedRow.Options.UseForeColor = True
        Me.CtrYesNo4View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CtrYesNo4View.Appearance.GroupRow.Options.UseFont = True
        Me.CtrYesNo4View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CtrYesNo4View.Appearance.HeaderPanel.Options.UseFont = True
        Me.CtrYesNo4View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.CtrYesNo4View.Appearance.Row.Options.UseFont = True
        Me.CtrYesNo4View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn6})
        Me.CtrYesNo4View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.CtrYesNo4View.Name = "CtrYesNo4View"
        Me.CtrYesNo4View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.CtrYesNo4View.OptionsView.EnableAppearanceEvenRow = True
        Me.CtrYesNo4View.OptionsView.EnableAppearanceOddRow = True
        Me.CtrYesNo4View.OptionsView.ShowAutoFilterRow = True
        Me.CtrYesNo4View.OptionsView.ShowDetailButtons = False
        Me.CtrYesNo4View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.CtrYesNo4View, False)
        '
        'GridColumn5
        '
        Me.GridColumn5.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn5.Caption = "Selección"
        Me.GridColumn5.FieldName = "Item2"
        Me.GridColumn5.Name = "GridColumn5"
        '
        'GridColumn6
        '
        Me.GridColumn6.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn6.Caption = "Selección"
        Me.GridColumn6.FieldName = "Item2"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 0
        '
        'INDGleEstimationLaborType
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDGleEstimationLaborType, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleEstimationLaborType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleEstimationLaborType, False)
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDGleEstimationLaborType, True)
        Me.INDGleEstimationLaborType.Location = New System.Drawing.Point(24, 275)
        Me.IndigoTextEdit1.SetMascara(Me.INDGleEstimationLaborType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleEstimationLaborType.Name = "INDGleEstimationLaborType"
        Me.INDGleEstimationLaborType.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDGleEstimationLaborType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGleEstimationLaborType.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleEstimationLaborType.Properties.Appearance.Options.UseFont = True
        Me.INDGleEstimationLaborType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleEstimationLaborType.Properties.DisplayMember = "Item2"
        Me.INDGleEstimationLaborType.Properties.ImmediatePopup = True
        Me.INDGleEstimationLaborType.Properties.NullText = ""
        Me.INDGleEstimationLaborType.Properties.ValueMember = "Item1"
        Me.INDGleEstimationLaborType.Properties.View = Me.GridLookUpEdit1View
        Me.INDGleEstimationLaborType.Size = New System.Drawing.Size(386, 28)
        Me.INDGleEstimationLaborType.StyleController = Me.INDLcRoot
        Me.INDGleEstimationLaborType.TabIndex = 3
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDGleEstimationLaborType, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleEstimationLaborType, 0)
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
        Me.GridColumn1.Caption = "Tipo"
        Me.GridColumn1.FieldName = "Item2"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'INDSleJournalVoucherType
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleJournalVoucherType, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleJournalVoucherType, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleJournalVoucherType, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleJournalVoucherType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleJournalVoucherType, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleJournalVoucherType, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleJournalVoucherType, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleJournalVoucherType, False)
        Me.INDSleJournalVoucherType.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleJournalVoucherType, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleJournalVoucherType, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleJournalVoucherType, False)
        Me.INDSleJournalVoucherType.Location = New System.Drawing.Point(24, 215)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleJournalVoucherType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleJournalVoucherType.Name = "INDSleJournalVoucherType"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleJournalVoucherType, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleJournalVoucherType, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleJournalVoucherType, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleJournalVoucherType, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleJournalVoucherType, False)
        Me.INDSleJournalVoucherType.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleJournalVoucherType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleJournalVoucherType.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleJournalVoucherType.Properties.Appearance.Options.UseFont = True
        Me.INDSleJournalVoucherType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "Limpiar selección (Supr)", Nothing, Nothing, True)})
        Me.INDSleJournalVoucherType.Properties.DisplayMember = "CodeName"
        Me.INDSleJournalVoucherType.Properties.NullText = ""
        Me.INDSleJournalVoucherType.Properties.PopupSizeable = False
        Me.INDSleJournalVoucherType.Properties.ShowFooter = False
        Me.INDSleJournalVoucherType.Properties.ValueMember = "OID"
        Me.INDSleJournalVoucherType.Properties.View = Me.SearchLookUpEdit1View
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleJournalVoucherType, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleJournalVoucherType, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleJournalVoucherType, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleJournalVoucherType, True)
        Me.INDSleJournalVoucherType.Size = New System.Drawing.Size(386, 28)
        Me.INDSleJournalVoucherType.StyleController = Me.INDLcRoot
        Me.INDSleJournalVoucherType.TabIndex = 2
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleJournalVoucherType, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleJournalVoucherType, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleJournalVoucherType, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleJournalVoucherType, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleJournalVoucherType, False)
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
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn2, Me.GridColumn3})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Código"
        Me.GridColumn2.FieldName = "TCCODIGO"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        Me.GridColumn2.Width = 386
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Nombre"
        Me.GridColumn3.FieldName = "TCNOMBRE"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 1
        Me.GridColumn3.Width = 1006
        '
        'CtrDateNavigator1
        '
        Me.CtrDateNavigator1.CtrCalendar = Nothing
        Me.CtrDateNavigator1.HowShowControl = Presentation.Controls.CtrDateNavigator.EHowShowControl.Both
        Me.CtrDateNavigator1.Location = New System.Drawing.Point(24, 59)
        Me.CtrDateNavigator1.Name = "CtrDateNavigator1"
        Me.CtrDateNavigator1.Size = New System.Drawing.Size(386, 66)
        Me.CtrDateNavigator1.TabIndex = 0
        Me.CtrDateNavigator1.WithEvent = True
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
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgMainData})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(976, 410)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDLcgMainData
        '
        Me.INDLcgMainData.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgMainData.AppearanceGroup.Options.UseFont = True
        Me.INDLcgMainData.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgMainData.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgMainData.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMainData.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgMainData.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgMainData.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgMainData.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMainData.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgMainData.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMainData.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgMainData.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMainData.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgMainData, False)
        Me.INDLcgMainData.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciYearMonth, Me.INDLciJournalVoucherType, Me.INDLciEstimationLaborType, Me.INDlyItemAccountingCost})
        Me.INDLcgMainData.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgMainData.Name = "INDLcgMainData"
        Me.INDLcgMainData.Size = New System.Drawing.Size(956, 390)
        Me.INDLcgMainData.Text = "Datos Principales"
        '
        'INDLciYearMonth
        '
        Me.INDLciYearMonth.Control = Me.CtrDateNavigator1
        Me.INDLciYearMonth.Location = New System.Drawing.Point(0, 0)
        Me.INDLciYearMonth.MaxSize = New System.Drawing.Size(390, 70)
        Me.INDLciYearMonth.MinSize = New System.Drawing.Size(390, 70)
        Me.INDLciYearMonth.Name = "INDLciYearMonth"
        Me.INDLciYearMonth.Size = New System.Drawing.Size(932, 70)
        Me.INDLciYearMonth.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciYearMonth.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciYearMonth.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciYearMonth.TextToControlDistance = 0
        Me.INDLciYearMonth.TextVisible = False
        '
        'INDLciJournalVoucherType
        '
        Me.INDLciJournalVoucherType.Control = Me.INDSleJournalVoucherType
        Me.INDLciJournalVoucherType.Location = New System.Drawing.Point(0, 130)
        Me.INDLciJournalVoucherType.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciJournalVoucherType.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciJournalVoucherType.Name = "INDLciJournalVoucherType"
        Me.INDLciJournalVoucherType.Size = New System.Drawing.Size(932, 60)
        Me.INDLciJournalVoucherType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciJournalVoucherType.Text = "Tipo Comprobante"
        Me.INDLciJournalVoucherType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciJournalVoucherType.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciJournalVoucherType.TextSize = New System.Drawing.Size(96, 21)
        Me.INDLciJournalVoucherType.TextToControlDistance = 5
        Me.INDLciJournalVoucherType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLciEstimationLaborType
        '
        Me.INDLciEstimationLaborType.Control = Me.INDGleEstimationLaborType
        Me.INDLciEstimationLaborType.Location = New System.Drawing.Point(0, 190)
        Me.INDLciEstimationLaborType.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciEstimationLaborType.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciEstimationLaborType.Name = "INDLciEstimationLaborType"
        Me.INDLciEstimationLaborType.ShowInCustomizationForm = False
        Me.INDLciEstimationLaborType.Size = New System.Drawing.Size(932, 141)
        Me.INDLciEstimationLaborType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciEstimationLaborType.Text = "Tipo Estimación Mano Obra"
        Me.INDLciEstimationLaborType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciEstimationLaborType.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciEstimationLaborType.TextSize = New System.Drawing.Size(96, 21)
        Me.INDLciEstimationLaborType.TextToControlDistance = 5
        '
        'INDlyItemAccountingCost
        '
        Me.INDlyItemAccountingCost.Control = Me.INDsleAccountingCosts
        Me.INDlyItemAccountingCost.Location = New System.Drawing.Point(0, 70)
        Me.INDlyItemAccountingCost.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemAccountingCost.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemAccountingCost.Name = "INDlyItemAccountingCost"
        Me.INDlyItemAccountingCost.ShowInCustomizationForm = False
        Me.INDlyItemAccountingCost.Size = New System.Drawing.Size(932, 60)
        Me.INDlyItemAccountingCost.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemAccountingCost.Text = "Contabilizar Costos"
        Me.INDlyItemAccountingCost.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemAccountingCost.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemAccountingCost.TextSize = New System.Drawing.Size(96, 21)
        Me.INDlyItemAccountingCost.TextToControlDistance = 5
        '
        'GridColumn4
        '
        Me.GridColumn4.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn4.Caption = "Selección"
        Me.GridColumn4.FieldName = "Item2"
        Me.GridColumn4.Name = "GridColumn4"
        '
        'GridColumn267
        '
        Me.GridColumn267.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn267.Caption = "Selección"
        Me.GridColumn267.FieldName = "Item2"
        Me.GridColumn267.Name = "GridColumn267"
        '
        'FrmInteropCostSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1180, 537)
        Me.Name = "FrmInteropCostSettings"
        Me.Opacity = 1.0R
        Me.Tag = "1211"
        Me.Text = "Parámetros"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcRoot.ResumeLayout(False)
        CType(Me.INDsleAccountingCosts.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleAccountingCosts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrYesNo4View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleEstimationLaborType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleJournalVoucherType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgMainData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciYearMonth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciJournalVoucherType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciEstimationLaborType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemAccountingCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControlPanel1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDLcRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDGleEstimationLaborType As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDSleJournalVoucherType As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents CtrDateNavigator1 As Presentation.Controls.CtrDateNavigator
    Friend WithEvents INDLcgMainData As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciYearMonth As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciJournalVoucherType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciEstimationLaborType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDsleAccountingCosts As Presentation.Controls.CtrYesNo
    Friend WithEvents CtrYesNo4View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlyItemAccountingCost As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn267 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
End Class
