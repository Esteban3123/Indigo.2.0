Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmGeneralExpenses
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
        Dim AppearanceObject5 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject6 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject7 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject8 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmGeneralExpenses))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlcRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDpccProductionCenter = New DevExpress.XtraEditors.PopupContainerControl()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDsleAccountId = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn15 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn16 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSLeCostCenterConcept = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView4 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn209 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn210 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsbAddProductionCenter = New DevExpress.XtraEditors.SimpleButton()
        Me.INDspnDistributAmount = New DevExpress.XtraEditors.SpinEdit()
        Me.INDsleProductionCenter = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDliProductionCenter = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliDistributedAmount = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciCostCenter = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciMainAccount = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDSleElementCostType = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgleDistribution = New DevExpress.XtraEditors.ImageComboBoxEdit()
        Me.ImageCollection1 = New DevExpress.Utils.ImageCollection(Me.components)
        Me.INDsbAddDistribution = New DevExpress.XtraEditors.SimpleButton()
        Me.INDgcDsitribution = New DevExpress.XtraGrid.GridControl()
        Me.INDgvDistributionBase = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemImageComboBox1 = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgleExpenseType = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDtxtName = New DevExpress.XtraEditors.TextEdit()
        Me.INDbteCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDgleCategory = New DevExpress.XtraEditors.TreeListLookUpEdit()
        Me.TreeList1 = New DevExpress.XtraTreeList.TreeList()
        Me.TreeListColumn1 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.INDlcgRoot = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlcgMainData = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDliCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliExpenseType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliDistribution = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciElementCostType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemCategory = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlcgDistributionBase = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDliDistributionBase = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliAddDistribution = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoComboBoxEdit1 = New Presentation.Controls.IndigoComboBoxEdit(Me.components)
        Me.IndigoTextEdit2 = New Presentation.Controls.IndigoTextEdit(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlcRoot.SuspendLayout()
        CType(Me.INDpccProductionCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpccProductionCenter.SuspendLayout()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDsleAccountId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSLeCostCenterConcept.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDspnDistributAmount.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleProductionCenter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliProductionCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliDistributedAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciCostCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciMainAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleElementCostType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgleDistribution.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImageCollection1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcDsitribution, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvDistributionBase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemImageComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgleExpenseType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbteCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgleCategory.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TreeList1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgMainData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliExpenseType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliDistribution, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciElementCostType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgDistributionBase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliDistributionBase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliAddDistribution, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoComboBoxEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlcRoot)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 118)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1324, 519)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1324, 94)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1324, 94)
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.Appearance.BackColor = System.Drawing.SystemColors.Control
        Me.CtrNavigationControl1.Appearance.Options.UseBackColor = True
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDlcRoot
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 510)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDlcRoot
        '
        Me.INDlcRoot.Controls.Add(Me.INDpccProductionCenter)
        Me.INDlcRoot.Controls.Add(Me.INDSleElementCostType)
        Me.INDlcRoot.Controls.Add(Me.INDgleDistribution)
        Me.INDlcRoot.Controls.Add(Me.INDsbAddDistribution)
        Me.INDlcRoot.Controls.Add(Me.INDgcDsitribution)
        Me.INDlcRoot.Controls.Add(Me.INDgleExpenseType)
        Me.INDlcRoot.Controls.Add(Me.INDtxtName)
        Me.INDlcRoot.Controls.Add(Me.INDbteCode)
        Me.INDlcRoot.Controls.Add(Me.INDgleCategory)
        Me.INDlcRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlcRoot.Location = New System.Drawing.Point(202, 7)
        Me.INDlcRoot.Name = "INDlcRoot"
        Me.INDlcRoot.Root = Me.INDlcgRoot
        Me.INDlcRoot.Size = New System.Drawing.Size(1120, 510)
        Me.INDlcRoot.TabIndex = 1
        Me.INDlcRoot.Text = "LayoutControl1"
        '
        'INDpccProductionCenter
        '
        Me.INDpccProductionCenter.Controls.Add(Me.LayoutControl1)
        Me.INDpccProductionCenter.Location = New System.Drawing.Point(487, 229)
        Me.INDpccProductionCenter.Name = "INDpccProductionCenter"
        Me.INDpccProductionCenter.Size = New System.Drawing.Size(432, 206)
        Me.INDpccProductionCenter.TabIndex = 16
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDsleAccountId)
        Me.LayoutControl1.Controls.Add(Me.INDSLeCostCenterConcept)
        Me.LayoutControl1.Controls.Add(Me.INDsbAddProductionCenter)
        Me.LayoutControl1.Controls.Add(Me.INDspnDistributAmount)
        Me.LayoutControl1.Controls.Add(Me.INDsleProductionCenter)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup2
        Me.LayoutControl1.Size = New System.Drawing.Size(432, 206)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDsleAccountId
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleAccountId, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleAccountId, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleAccountId, False)
        Me.IndigoTextEdit2.SetApplyStyle(Me.INDsleAccountId, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleAccountId, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleAccountId, False)
        Me.IndigoTextEdit2.SetCampoObligatorio(Me.INDsleAccountId, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleAccountId, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleAccountId, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleAccountId, False)
        Me.INDsleAccountId.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleAccountId, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleAccountId, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleAccountId, False)
        Me.INDsleAccountId.Location = New System.Drawing.Point(172, 84)
        Me.IndigoTextEdit2.SetMascara(Me.INDsleAccountId, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleAccountId, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleAccountId.Name = "INDsleAccountId"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleAccountId, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleAccountId, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleAccountId, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleAccountId, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleAccountId, False)
        Me.INDsleAccountId.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleAccountId.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleAccountId.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleAccountId.Properties.Appearance.Options.UseFont = True
        Me.INDsleAccountId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleAccountId.Properties.DisplayMember = "CodeName"
        Me.INDsleAccountId.Properties.NullText = ""
        Me.INDsleAccountId.Properties.PopupSizeable = False
        Me.INDsleAccountId.Properties.ShowClearButton = False
        Me.INDsleAccountId.Properties.ShowFooter = False
        Me.INDsleAccountId.Properties.ValueMember = "OID"
        Me.INDsleAccountId.Properties.View = Me.GridView1
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleAccountId, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleAccountId, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleAccountId, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleAccountId, True)
        Me.INDsleAccountId.Size = New System.Drawing.Size(246, 28)
        Me.INDsleAccountId.StyleController = Me.LayoutControl1
        Me.INDsleAccountId.TabIndex = 15
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleAccountId, Nothing)
        Me.IndigoTextEdit2.SetTamañoMinimoString(Me.INDsleAccountId, 0)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleAccountId, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleAccountId, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleAccountId, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleAccountId, False)
        '
        'GridView1
        '
        Me.GridView1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView1.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView1.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.GroupRow.Options.UseFont = True
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn15, Me.GridColumn16})
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView1, False)
        '
        'GridColumn15
        '
        Me.GridColumn15.Caption = "Código"
        Me.GridColumn15.FieldName = "CUECODIGO"
        Me.GridColumn15.Name = "GridColumn15"
        Me.GridColumn15.Visible = True
        Me.GridColumn15.VisibleIndex = 0
        Me.GridColumn15.Width = 321
        '
        'GridColumn16
        '
        Me.GridColumn16.Caption = "Nombre"
        Me.GridColumn16.FieldName = "CUENOMBRE"
        Me.GridColumn16.Name = "GridColumn16"
        Me.GridColumn16.Visible = True
        Me.GridColumn16.VisibleIndex = 1
        Me.GridColumn16.Width = 1071
        '
        'INDSLeCostCenterConcept
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSLeCostCenterConcept, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSLeCostCenterConcept, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSLeCostCenterConcept, False)
        Me.IndigoTextEdit2.SetApplyStyle(Me.INDSLeCostCenterConcept, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSLeCostCenterConcept, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSLeCostCenterConcept, False)
        Me.IndigoTextEdit2.SetCampoObligatorio(Me.INDSLeCostCenterConcept, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSLeCostCenterConcept, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSLeCostCenterConcept, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSLeCostCenterConcept, False)
        Me.INDSLeCostCenterConcept.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSLeCostCenterConcept, True)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSLeCostCenterConcept, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSLeCostCenterConcept, False)
        Me.INDSLeCostCenterConcept.Location = New System.Drawing.Point(172, 120)
        Me.IndigoTextEdit2.SetMascara(Me.INDSLeCostCenterConcept, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit1.SetMascara(Me.INDSLeCostCenterConcept, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSLeCostCenterConcept.Name = "INDSLeCostCenterConcept"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSLeCostCenterConcept, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSLeCostCenterConcept, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSLeCostCenterConcept, True)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSLeCostCenterConcept, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSLeCostCenterConcept, False)
        Me.INDSLeCostCenterConcept.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSLeCostCenterConcept.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSLeCostCenterConcept.Properties.Appearance.Options.UseBackColor = True
        Me.INDSLeCostCenterConcept.Properties.Appearance.Options.UseFont = True
        Me.INDSLeCostCenterConcept.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSLeCostCenterConcept.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSLeCostCenterConcept.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSLeCostCenterConcept.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSLeCostCenterConcept.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSLeCostCenterConcept.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSLeCostCenterConcept.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSLeCostCenterConcept.Properties.DisplayMember = "CodeName"
        Me.INDSLeCostCenterConcept.Properties.NullText = ""
        Me.INDSLeCostCenterConcept.Properties.PopupSizeable = False
        Me.INDSLeCostCenterConcept.Properties.ShowClearButton = False
        Me.INDSLeCostCenterConcept.Properties.ShowFooter = False
        Me.INDSLeCostCenterConcept.Properties.ValueMember = "OID"
        Me.INDSLeCostCenterConcept.Properties.View = Me.GridView4
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSLeCostCenterConcept, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSLeCostCenterConcept, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSLeCostCenterConcept, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSLeCostCenterConcept, True)
        Me.INDSLeCostCenterConcept.Size = New System.Drawing.Size(246, 28)
        Me.INDSLeCostCenterConcept.StyleController = Me.LayoutControl1
        Me.INDSLeCostCenterConcept.TabIndex = 8
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSLeCostCenterConcept, "517")
        Me.IndigoTextEdit2.SetTamañoMinimoString(Me.INDSLeCostCenterConcept, 0)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSLeCostCenterConcept, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSLeCostCenterConcept, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSLeCostCenterConcept, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSLeCostCenterConcept, False)
        '
        'GridView4
        '
        Me.GridView4.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView4.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView4.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView4.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView4.Appearance.FocusedRow.Options.UseForeColor = True
        Me.GridView4.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView4.Appearance.GroupRow.Options.UseFont = True
        Me.GridView4.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView4.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView4.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView4.Appearance.Row.Options.UseFont = True
        Me.GridView4.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn209, Me.GridColumn210})
        Me.GridView4.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView4.Name = "GridView4"
        Me.GridView4.OptionsFind.FindFilterColumns = "Codigo"
        Me.GridView4.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView4.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView4.OptionsView.EnableAppearanceOddRow = True
        Me.GridView4.OptionsView.ShowAutoFilterRow = True
        Me.GridView4.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView4, False)
        '
        'GridColumn209
        '
        Me.GridColumn209.Caption = "Código"
        Me.GridColumn209.FieldName = "CCCODIGO"
        Me.GridColumn209.Name = "GridColumn209"
        Me.GridColumn209.Visible = True
        Me.GridColumn209.VisibleIndex = 0
        Me.GridColumn209.Width = 308
        '
        'GridColumn210
        '
        Me.GridColumn210.Caption = "Nombre"
        Me.GridColumn210.FieldName = "CCNOMBRE"
        Me.GridColumn210.Name = "GridColumn210"
        Me.GridColumn210.Visible = True
        Me.GridColumn210.VisibleIndex = 1
        Me.GridColumn210.Width = 1004
        '
        'INDsbAddProductionCenter
        '
        Me.INDsbAddProductionCenter.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDsbAddProductionCenter.Appearance.Options.UseFont = True
        Me.INDsbAddProductionCenter.Location = New System.Drawing.Point(12, 156)
        Me.INDsbAddProductionCenter.Name = "INDsbAddProductionCenter"
        Me.INDsbAddProductionCenter.Size = New System.Drawing.Size(406, 32)
        Me.INDsbAddProductionCenter.StyleController = Me.LayoutControl1
        Me.INDsbAddProductionCenter.TabIndex = 6
        Me.INDsbAddProductionCenter.Text = "Agregar"
        '
        'INDspnDistributAmount
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDspnDistributAmount, True)
        Me.IndigoTextEdit2.SetApplyStyle(Me.INDspnDistributAmount, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDspnDistributAmount, False)
        Me.IndigoTextEdit2.SetCampoObligatorio(Me.INDspnDistributAmount, False)
        Me.INDspnDistributAmount.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDspnDistributAmount.EnterMoveNextControl = True
        Me.INDspnDistributAmount.Location = New System.Drawing.Point(172, 48)
        Me.IndigoTextEdit2.SetMascara(Me.INDspnDistributAmount, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit1.SetMascara(Me.INDspnDistributAmount, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDspnDistributAmount.Name = "INDspnDistributAmount"
        Me.INDspnDistributAmount.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDspnDistributAmount.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDspnDistributAmount.Properties.Appearance.Options.UseBackColor = True
        Me.INDspnDistributAmount.Properties.Appearance.Options.UseFont = True
        Me.INDspnDistributAmount.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDspnDistributAmount.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDspnDistributAmount.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDspnDistributAmount.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDspnDistributAmount.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDspnDistributAmount.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDspnDistributAmount.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDspnDistributAmount.Size = New System.Drawing.Size(246, 28)
        Me.INDspnDistributAmount.StyleController = Me.LayoutControl1
        Me.INDspnDistributAmount.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDspnDistributAmount, 0)
        Me.IndigoTextEdit2.SetTamañoMinimoString(Me.INDspnDistributAmount, 0)
        '
        'INDsleProductionCenter
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleProductionCenter, AppearanceObject5)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleProductionCenter, AppearanceObject6)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleProductionCenter, False)
        Me.IndigoTextEdit2.SetApplyStyle(Me.INDsleProductionCenter, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleProductionCenter, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleProductionCenter, False)
        Me.IndigoTextEdit2.SetCampoObligatorio(Me.INDsleProductionCenter, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleProductionCenter, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleProductionCenter, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleProductionCenter, False)
        Me.INDsleProductionCenter.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleProductionCenter, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleProductionCenter, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleProductionCenter, False)
        Me.INDsleProductionCenter.Location = New System.Drawing.Point(172, 12)
        Me.IndigoTextEdit2.SetMascara(Me.INDsleProductionCenter, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleProductionCenter, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleProductionCenter.Name = "INDsleProductionCenter"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleProductionCenter, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleProductionCenter, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleProductionCenter, True)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleProductionCenter, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleProductionCenter, False)
        Me.INDsleProductionCenter.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleProductionCenter.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleProductionCenter.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleProductionCenter.Properties.Appearance.Options.UseFont = True
        Me.INDsleProductionCenter.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleProductionCenter.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleProductionCenter.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleProductionCenter.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleProductionCenter.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleProductionCenter.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleProductionCenter.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleProductionCenter.Properties.DisplayMember = "CodeName"
        Me.INDsleProductionCenter.Properties.NullText = ""
        Me.INDsleProductionCenter.Properties.PopupSizeable = False
        Me.INDsleProductionCenter.Properties.ShowClearButton = False
        Me.INDsleProductionCenter.Properties.ShowFooter = False
        Me.INDsleProductionCenter.Properties.ValueMember = "Id"
        Me.INDsleProductionCenter.Properties.View = Me.GridView3
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleProductionCenter, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleProductionCenter, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleProductionCenter, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleProductionCenter, True)
        Me.INDsleProductionCenter.Size = New System.Drawing.Size(246, 28)
        Me.INDsleProductionCenter.StyleController = Me.LayoutControl1
        Me.INDsleProductionCenter.TabIndex = 4
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleProductionCenter, "1201")
        Me.IndigoTextEdit2.SetTamañoMinimoString(Me.INDsleProductionCenter, 0)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleProductionCenter, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleProductionCenter, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleProductionCenter, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleProductionCenter, False)
        '
        'GridView3
        '
        Me.GridView3.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView3.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView3.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView3.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView3.Appearance.FocusedRow.Options.UseForeColor = True
        Me.GridView3.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView3.Appearance.GroupRow.Options.UseFont = True
        Me.GridView3.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView3.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView3.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView3.Appearance.Row.Options.UseFont = True
        Me.GridView3.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn7, Me.GridColumn8})
        Me.GridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView3.Name = "GridView3"
        Me.GridView3.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView3.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView3.OptionsView.EnableAppearanceOddRow = True
        Me.GridView3.OptionsView.ShowAutoFilterRow = True
        Me.GridView3.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView3, False)
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Código"
        Me.GridColumn7.FieldName = "Code"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 0
        Me.GridColumn7.Width = 232
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Nombre"
        Me.GridColumn8.FieldName = "Name"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 1
        Me.GridColumn8.Width = 464
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup2.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup2.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup2, False)
        Me.LayoutControlGroup2.CustomizationFormText = "LayoutControlGroup2"
        Me.LayoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup2.GroupBordersVisible = False
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDliProductionCenter, Me.INDliDistributedAmount, Me.LayoutControlItem3, Me.INDLciCostCenter, Me.INDLciMainAccount})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(432, 206)
        Me.LayoutControlGroup2.TextVisible = False
        '
        'INDliProductionCenter
        '
        Me.INDliProductionCenter.Control = Me.INDsleProductionCenter
        Me.INDliProductionCenter.CustomizationFormText = "Centro de Producción"
        Me.INDliProductionCenter.Location = New System.Drawing.Point(0, 0)
        Me.INDliProductionCenter.MaxSize = New System.Drawing.Size(410, 36)
        Me.INDliProductionCenter.MinSize = New System.Drawing.Size(410, 36)
        Me.INDliProductionCenter.Name = "INDliProductionCenter"
        Me.INDliProductionCenter.Size = New System.Drawing.Size(412, 36)
        Me.INDliProductionCenter.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliProductionCenter.Text = "Centro de Producción"
        Me.INDliProductionCenter.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliProductionCenter.TextSize = New System.Drawing.Size(155, 13)
        Me.INDliProductionCenter.TextToControlDistance = 5
        '
        'INDliDistributedAmount
        '
        Me.INDliDistributedAmount.Control = Me.INDspnDistributAmount
        Me.INDliDistributedAmount.CustomizationFormText = "Cantidad Distribuir"
        Me.INDliDistributedAmount.Location = New System.Drawing.Point(0, 36)
        Me.INDliDistributedAmount.MaxSize = New System.Drawing.Size(410, 36)
        Me.INDliDistributedAmount.MinSize = New System.Drawing.Size(410, 36)
        Me.INDliDistributedAmount.Name = "INDliDistributedAmount"
        Me.INDliDistributedAmount.Size = New System.Drawing.Size(412, 36)
        Me.INDliDistributedAmount.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliDistributedAmount.Text = "Cantidad Distribuir"
        Me.INDliDistributedAmount.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliDistributedAmount.TextSize = New System.Drawing.Size(155, 21)
        Me.INDliDistributedAmount.TextToControlDistance = 5
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.INDsbAddProductionCenter
        Me.LayoutControlItem3.CustomizationFormText = "LayoutControlItem3"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 144)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(410, 36)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(410, 36)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(412, 42)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextToControlDistance = 0
        Me.LayoutControlItem3.TextVisible = False
        '
        'INDLciCostCenter
        '
        Me.INDLciCostCenter.Control = Me.INDSLeCostCenterConcept
        Me.INDLciCostCenter.Location = New System.Drawing.Point(0, 108)
        Me.INDLciCostCenter.MaxSize = New System.Drawing.Size(410, 36)
        Me.INDLciCostCenter.MinSize = New System.Drawing.Size(410, 36)
        Me.INDLciCostCenter.Name = "INDLciCostCenter"
        Me.INDLciCostCenter.Size = New System.Drawing.Size(412, 36)
        Me.INDLciCostCenter.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciCostCenter.Text = "Centro de Costo"
        Me.INDLciCostCenter.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciCostCenter.TextSize = New System.Drawing.Size(155, 21)
        Me.INDLciCostCenter.TextToControlDistance = 5
        '
        'INDLciMainAccount
        '
        Me.INDLciMainAccount.Control = Me.INDsleAccountId
        Me.INDLciMainAccount.Location = New System.Drawing.Point(0, 72)
        Me.INDLciMainAccount.MaxSize = New System.Drawing.Size(410, 36)
        Me.INDLciMainAccount.MinSize = New System.Drawing.Size(410, 36)
        Me.INDLciMainAccount.Name = "INDLciMainAccount"
        Me.INDLciMainAccount.Size = New System.Drawing.Size(412, 36)
        Me.INDLciMainAccount.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciMainAccount.Text = "Cuenta Contable"
        Me.INDLciMainAccount.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciMainAccount.TextSize = New System.Drawing.Size(155, 21)
        Me.INDLciMainAccount.TextToControlDistance = 5
        '
        'INDSleElementCostType
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleElementCostType, AppearanceObject7)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleElementCostType, AppearanceObject8)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleElementCostType, False)
        Me.IndigoTextEdit2.SetApplyStyle(Me.INDSleElementCostType, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleElementCostType, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleElementCostType, False)
        Me.IndigoTextEdit2.SetCampoObligatorio(Me.INDSleElementCostType, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleElementCostType, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleElementCostType, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleElementCostType, False)
        Me.INDSleElementCostType.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleElementCostType, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleElementCostType, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleElementCostType, False)
        Me.INDSleElementCostType.Location = New System.Drawing.Point(24, 205)
        Me.IndigoTextEdit2.SetMascara(Me.INDSleElementCostType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleElementCostType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleElementCostType.Name = "INDSleElementCostType"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleElementCostType, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleElementCostType, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleElementCostType, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleElementCostType, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleElementCostType, False)
        Me.INDSleElementCostType.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleElementCostType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleElementCostType.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleElementCostType.Properties.Appearance.Options.UseFont = True
        Me.INDSleElementCostType.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleElementCostType.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleElementCostType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleElementCostType.Properties.DisplayMember = "Item2"
        Me.INDSleElementCostType.Properties.NullText = ""
        Me.INDSleElementCostType.Properties.PopupSizeable = False
        Me.INDSleElementCostType.Properties.ShowFooter = False
        Me.INDSleElementCostType.Properties.ValueMember = "Item1"
        Me.INDSleElementCostType.Properties.View = Me.SearchLookUpEdit1View
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleElementCostType, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleElementCostType, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleElementCostType, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleElementCostType, True)
        Me.INDSleElementCostType.Size = New System.Drawing.Size(386, 28)
        Me.INDSleElementCostType.StyleController = Me.INDlcRoot
        Me.INDSleElementCostType.TabIndex = 2
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleElementCostType, Nothing)
        Me.IndigoTextEdit2.SetTamañoMinimoString(Me.INDSleElementCostType, 0)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleElementCostType, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleElementCostType, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleElementCostType, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleElementCostType, False)
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn5})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Tipo"
        Me.GridColumn5.FieldName = "Item2"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 0
        '
        'INDgleDistribution
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDgleDistribution, True)
        Me.IndigoTextEdit2.SetApplyStyle(Me.INDgleDistribution, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDgleDistribution, True)
        Me.IndigoComboBoxEdit1.SetCampoObligatorio(Me.INDgleDistribution, False)
        Me.IndigoTextEdit2.SetCampoObligatorio(Me.INDgleDistribution, False)
        Me.INDgleDistribution.EnterMoveNextControl = True
        Me.INDgleDistribution.Location = New System.Drawing.Point(24, 385)
        Me.IndigoTextEdit2.SetMascara(Me.INDgleDistribution, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit1.SetMascara(Me.INDgleDistribution, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDgleDistribution.Name = "INDgleDistribution"
        Me.INDgleDistribution.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDgleDistribution.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDgleDistribution.Properties.Appearance.Options.UseBackColor = True
        Me.INDgleDistribution.Properties.Appearance.Options.UseFont = True
        Me.INDgleDistribution.Properties.AppearanceDropDown.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDgleDistribution.Properties.AppearanceDropDown.Options.UseFont = True
        Me.INDgleDistribution.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDgleDistribution.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDgleDistribution.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDgleDistribution.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDgleDistribution.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDgleDistribution.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDgleDistribution.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDgleDistribution.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Distribucion (1)", "1", 0), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Distribucion (2)", "2", 1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Distribucion (3)", "3", 2), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Distribucion (4)", "4", 3)})
        Me.INDgleDistribution.Properties.LargeImages = Me.ImageCollection1
        Me.INDgleDistribution.Properties.SmallImages = Me.ImageCollection1
        Me.INDgleDistribution.Size = New System.Drawing.Size(386, 28)
        Me.INDgleDistribution.StyleController = Me.INDlcRoot
        Me.INDgleDistribution.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDgleDistribution, 0)
        Me.IndigoTextEdit2.SetTamañoMinimoString(Me.INDgleDistribution, 0)
        '
        'ImageCollection1
        '
        Me.ImageCollection1.ImageStream = CType(resources.GetObject("ImageCollection1.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.ImageCollection1.InsertImage(Global.Presentation.InteropCost.My.Resources.Resources.ICONOGRAFIA_1_6_011, "ICONOGRAFIA_1_6_011", GetType(Global.Presentation.InteropCost.My.Resources.Resources), 0)
        Me.ImageCollection1.Images.SetKeyName(0, "ICONOGRAFIA_1_6_011")
        Me.ImageCollection1.InsertImage(Global.Presentation.InteropCost.My.Resources.Resources.ICONOGRAFIA_1_6_021, "ICONOGRAFIA_1_6_021", GetType(Global.Presentation.InteropCost.My.Resources.Resources), 1)
        Me.ImageCollection1.Images.SetKeyName(1, "ICONOGRAFIA_1_6_021")
        Me.ImageCollection1.InsertImage(Global.Presentation.InteropCost.My.Resources.Resources.ICONOGRAFIA_1_6_031, "ICONOGRAFIA_1_6_031", GetType(Global.Presentation.InteropCost.My.Resources.Resources), 2)
        Me.ImageCollection1.Images.SetKeyName(2, "ICONOGRAFIA_1_6_031")
        Me.ImageCollection1.InsertImage(Global.Presentation.InteropCost.My.Resources.Resources.ICONOGRAFIA_1_6_041, "ICONOGRAFIA_1_6_041", GetType(Global.Presentation.InteropCost.My.Resources.Resources), 3)
        Me.ImageCollection1.Images.SetKeyName(3, "ICONOGRAFIA_1_6_041")
        Me.ImageCollection1.InsertImage(Global.Presentation.InteropCost.My.Resources.Resources.ICONOGRAFIA_1_6_051, "ICONOGRAFIA_1_6_051", GetType(Global.Presentation.InteropCost.My.Resources.Resources), 4)
        Me.ImageCollection1.Images.SetKeyName(4, "ICONOGRAFIA_1_6_051")
        Me.ImageCollection1.InsertImage(Global.Presentation.InteropCost.My.Resources.Resources.ICONOGRAFIA_1_6_061, "ICONOGRAFIA_1_6_061", GetType(Global.Presentation.InteropCost.My.Resources.Resources), 5)
        Me.ImageCollection1.Images.SetKeyName(5, "ICONOGRAFIA_1_6_061")
        '
        'INDsbAddDistribution
        '
        Me.INDsbAddDistribution.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDsbAddDistribution.Appearance.Options.UseFont = True
        Me.INDsbAddDistribution.Location = New System.Drawing.Point(438, 59)
        Me.INDsbAddDistribution.Name = "INDsbAddDistribution"
        Me.INDsbAddDistribution.Size = New System.Drawing.Size(824, 32)
        Me.INDsbAddDistribution.StyleController = Me.INDlcRoot
        Me.INDsbAddDistribution.TabIndex = 6
        Me.INDsbAddDistribution.Text = "Agregar Distribución"
        '
        'INDgcDsitribution
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcDsitribution, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcDsitribution, Nothing)
        Me.INDgcDsitribution.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.INDgcDsitribution, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcDsitribution, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcDsitribution, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcDsitribution, False)
        Me.INDgcDsitribution.Location = New System.Drawing.Point(438, 95)
        Me.INDgcDsitribution.MainView = Me.INDgvDistributionBase
        Me.INDgcDsitribution.Name = "INDgcDsitribution"
        Me.INDgcDsitribution.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemImageComboBox1})
        Me.INDgcDsitribution.Size = New System.Drawing.Size(824, 374)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcDsitribution, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.IndigoGridControl1.SetSizeLayoutItem(Me.INDgcDsitribution, New System.Drawing.Size(828, 0))
        Me.INDgcDsitribution.TabIndex = 7
        Me.INDgcDsitribution.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvDistributionBase})
        '
        'INDgvDistributionBase
        '
        Me.INDgvDistributionBase.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvDistributionBase.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvDistributionBase.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDgvDistributionBase.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvDistributionBase.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvDistributionBase.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDgvDistributionBase.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvDistributionBase.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvDistributionBase.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvDistributionBase.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvDistributionBase.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvDistributionBase.Appearance.Row.Options.UseFont = True
        Me.INDgvDistributionBase.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgvDistributionBase.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvDistributionBase.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4})
        Me.INDgvDistributionBase.GridControl = Me.INDgcDsitribution
        Me.INDgvDistributionBase.Name = "INDgvDistributionBase"
        Me.INDgvDistributionBase.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvDistributionBase.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvDistributionBase.OptionsView.ShowAutoFilterRow = True
        Me.INDgvDistributionBase.OptionsView.ShowDetailButtons = False
        Me.INDgvDistributionBase.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvDistributionBase, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Base Distribución"
        Me.GridColumn1.ColumnEdit = Me.RepositoryItemImageComboBox1
        Me.GridColumn1.FieldName = "MultipleBase"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'RepositoryItemImageComboBox1
        '
        Me.RepositoryItemImageComboBox1.AutoHeight = False
        Me.RepositoryItemImageComboBox1.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.RepositoryItemImageComboBox1.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Distribución (1)", CType(1, Byte), 0), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Distribución (2)", CType(2, Byte), 1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Distribución (3)", CType(3, Byte), 2), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Distribución (4)", CType(4, Byte), 3)})
        Me.RepositoryItemImageComboBox1.Name = "RepositoryItemImageComboBox1"
        Me.RepositoryItemImageComboBox1.SmallImages = Me.ImageCollection1
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Tipo Distribución"
        Me.GridColumn2.FieldName = "DistributionTypeName"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Incidencia"
        Me.GridColumn3.FieldName = "ImpactPoints"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Unidad de Medida"
        Me.GridColumn4.FieldName = "MeasureUnitName"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 3
        '
        'INDgleExpenseType
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDgleExpenseType, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDgleExpenseType, False)
        Me.IndigoTextEdit2.SetApplyStyle(Me.INDgleExpenseType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDgleExpenseType, True)
        Me.IndigoTextEdit2.SetCampoObligatorio(Me.INDgleExpenseType, False)
        Me.INDgleExpenseType.EnterMoveNextControl = True
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDgleExpenseType, False)
        Me.INDgleExpenseType.Location = New System.Drawing.Point(24, 265)
        Me.IndigoTextEdit2.SetMascara(Me.INDgleExpenseType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit1.SetMascara(Me.INDgleExpenseType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDgleExpenseType.Name = "INDgleExpenseType"
        Me.INDgleExpenseType.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDgleExpenseType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgleExpenseType.Properties.Appearance.Options.UseBackColor = True
        Me.INDgleExpenseType.Properties.Appearance.Options.UseFont = True
        Me.INDgleExpenseType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDgleExpenseType.Properties.DisplayMember = "Item2"
        Me.INDgleExpenseType.Properties.ImmediatePopup = True
        Me.INDgleExpenseType.Properties.NullText = ""
        Me.INDgleExpenseType.Properties.ValueMember = "Item1"
        Me.INDgleExpenseType.Properties.View = Me.GridLookUpEdit1View
        Me.INDgleExpenseType.Size = New System.Drawing.Size(386, 28)
        Me.INDgleExpenseType.StyleController = Me.INDlcRoot
        Me.INDgleExpenseType.TabIndex = 3
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDgleExpenseType, Nothing)
        Me.IndigoTextEdit2.SetTamañoMinimoString(Me.INDgleExpenseType, 0)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDgleExpenseType, 0)
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
        Me.GridLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn6})
        Me.GridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit1View.Name = "GridLookUpEdit1View"
        Me.GridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridLookUpEdit1View, False)
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Tipo Egreso"
        Me.GridColumn6.FieldName = "Item2"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 0
        '
        'INDtxtName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtName, True)
        Me.IndigoTextEdit2.SetApplyStyle(Me.INDtxtName, False)
        Me.IndigoTextEdit2.SetCampoObligatorio(Me.INDtxtName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtName, True)
        Me.INDtxtName.EnterMoveNextControl = True
        Me.INDtxtName.Location = New System.Drawing.Point(24, 145)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit2.SetMascara(Me.INDtxtName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtName.Name = "INDtxtName"
        Me.INDtxtName.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtName.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtName.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtName.Properties.Appearance.Options.UseFont = True
        Me.INDtxtName.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtName.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtName.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtName.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtName.StyleController = Me.INDlcRoot
        Me.INDtxtName.TabIndex = 1
        Me.IndigoTextEdit2.SetTamañoMinimoString(Me.INDtxtName, 0)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtName, 0)
        '
        'INDbteCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbteCode, True)
        Me.IndigoTextEdit2.SetApplyStyle(Me.INDbteCode, False)
        Me.IndigoTextEdit2.SetCampoObligatorio(Me.INDbteCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbteCode, True)
        Me.INDbteCode.Location = New System.Drawing.Point(24, 85)
        Me.IndigoTextEdit2.SetMascara(Me.INDbteCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit1.SetMascara(Me.INDbteCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDbteCode.Name = "INDbteCode"
        Me.INDbteCode.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDbteCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbteCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDbteCode.Properties.Appearance.Options.UseFont = True
        Me.INDbteCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDbteCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDbteCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDbteCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Global.Presentation.InteropCost.My.Resources.Resources.BuscarMetro, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.INDbteCode.Size = New System.Drawing.Size(386, 28)
        Me.INDbteCode.StyleController = Me.INDlcRoot
        Me.INDbteCode.TabIndex = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbteCode, 0)
        Me.IndigoTextEdit2.SetTamañoMinimoString(Me.INDbteCode, 0)
        '
        'INDgleCategory
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDgleCategory, False)
        Me.IndigoTextEdit2.SetApplyStyle(Me.INDgleCategory, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDgleCategory, False)
        Me.IndigoTextEdit2.SetCampoObligatorio(Me.INDgleCategory, True)
        Me.INDgleCategory.EnterMoveNextControl = True
        Me.INDgleCategory.Location = New System.Drawing.Point(24, 325)
        Me.IndigoTextEdit1.SetMascara(Me.INDgleCategory, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit2.SetMascara(Me.INDgleCategory, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDgleCategory.Name = "INDgleCategory"
        Me.INDgleCategory.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDgleCategory.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDgleCategory.Properties.Appearance.Options.UseBackColor = True
        Me.INDgleCategory.Properties.Appearance.Options.UseFont = True
        Me.INDgleCategory.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDgleCategory.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDgleCategory.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDgleCategory.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDgleCategory.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDgleCategory.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDgleCategory.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDgleCategory.Properties.DisplayMember = "CodeName"
        Me.INDgleCategory.Properties.NullText = ""
        Me.INDgleCategory.Properties.TreeList = Me.TreeList1
        Me.INDgleCategory.Properties.ValueMember = "Id"
        Me.INDgleCategory.Size = New System.Drawing.Size(386, 28)
        Me.INDgleCategory.StyleController = Me.INDlcRoot
        Me.INDgleCategory.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDgleCategory, 0)
        Me.IndigoTextEdit2.SetTamañoMinimoString(Me.INDgleCategory, 0)
        Me.INDgleCategory.ToolTip = "Este Campo es Necesario"
        '
        'TreeList1
        '
        Me.TreeList1.Appearance.FocusedRow.Options.UseBackColor = True
        Me.TreeList1.Appearance.FocusedRow.Options.UseForeColor = True
        Me.TreeList1.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.TreeList1.Appearance.HeaderPanel.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.TreeList1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TreeList1.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.TreeList1.Appearance.HeaderPanel.Options.UseFont = True
        Me.TreeList1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TreeList1.Appearance.Row.ForeColor = System.Drawing.Color.Black
        Me.TreeList1.Appearance.Row.Options.UseFont = True
        Me.TreeList1.Appearance.Row.Options.UseForeColor = True
        Me.TreeList1.Columns.AddRange(New DevExpress.XtraTreeList.Columns.TreeListColumn() {Me.TreeListColumn1})
        Me.TreeList1.KeyFieldName = "Id"
        Me.TreeList1.Location = New System.Drawing.Point(-85, 76)
        Me.TreeList1.Name = "TreeList1"
        Me.TreeList1.OptionsBehavior.EnableFiltering = True
        Me.TreeList1.OptionsFilter.FilterMode = DevExpress.XtraTreeList.FilterMode.Smart
        Me.TreeList1.OptionsFind.AllowFindPanel = True
        Me.TreeList1.OptionsFind.AlwaysVisible = True
        Me.TreeList1.OptionsView.EnableAppearanceEvenRow = True
        Me.TreeList1.OptionsView.EnableAppearanceOddRow = True
        Me.TreeList1.OptionsView.ShowIndentAsRowStyle = True
        Me.TreeList1.ParentFieldName = "PadreId"
        Me.TreeList1.Size = New System.Drawing.Size(400, 200)
        Me.TreeList1.TabIndex = 0
        '
        'TreeListColumn1
        '
        Me.TreeListColumn1.Caption = "Descripción"
        Me.TreeListColumn1.FieldName = "CodeName"
        Me.TreeListColumn1.Name = "TreeListColumn1"
        Me.TreeListColumn1.Visible = True
        Me.TreeListColumn1.VisibleIndex = 0
        '
        'INDlcgRoot
        '
        Me.INDlcgRoot.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgRoot.AppearanceGroup.Options.UseFont = True
        Me.INDlcgRoot.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgRoot.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgRoot.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgRoot.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgRoot.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgRoot.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgRoot.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgRoot.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgRoot.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgRoot.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgRoot.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgRoot.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgRoot, False)
        Me.INDlcgRoot.CustomizationFormText = "INDlcgRoot"
        Me.INDlcgRoot.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlcgRoot.GroupBordersVisible = False
        Me.INDlcgRoot.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlcgMainData, Me.INDlcgDistributionBase})
        Me.INDlcgRoot.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgRoot.Name = "INDlcgRoot"
        Me.INDlcgRoot.Size = New System.Drawing.Size(1286, 493)
        Me.INDlcgRoot.TextVisible = False
        '
        'INDlcgMainData
        '
        Me.INDlcgMainData.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgMainData.AppearanceGroup.Options.UseFont = True
        Me.INDlcgMainData.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgMainData.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgMainData.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgMainData.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgMainData.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgMainData.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgMainData.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgMainData.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgMainData.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgMainData.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgMainData.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgMainData.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgMainData, False)
        Me.INDlcgMainData.CustomizationFormText = "Información General"
        Me.INDlcgMainData.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDliCode, Me.INDliName, Me.INDliExpenseType, Me.INDliDistribution, Me.INDLciElementCostType, Me.INDlyItemCategory})
        Me.INDlcgMainData.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgMainData.Name = "INDlcgMainData"
        Me.INDlcgMainData.Size = New System.Drawing.Size(414, 473)
        Me.INDlcgMainData.Text = "Información General"
        '
        'INDliCode
        '
        Me.INDliCode.AllowHide = False
        Me.INDliCode.Control = Me.INDbteCode
        Me.INDliCode.CustomizationFormText = "Código"
        Me.INDliCode.Location = New System.Drawing.Point(0, 0)
        Me.INDliCode.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDliCode.MinSize = New System.Drawing.Size(390, 60)
        Me.INDliCode.Name = "INDliCode"
        Me.INDliCode.ShowInCustomizationForm = False
        Me.INDliCode.Size = New System.Drawing.Size(390, 60)
        Me.INDliCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliCode.Text = "Código"
        Me.INDliCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDliCode.TextSize = New System.Drawing.Size(134, 21)
        Me.INDliCode.TextToControlDistance = 5
        '
        'INDliName
        '
        Me.INDliName.AllowHide = False
        Me.INDliName.Control = Me.INDtxtName
        Me.INDliName.CustomizationFormText = "Nombre"
        Me.INDliName.Location = New System.Drawing.Point(0, 60)
        Me.INDliName.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDliName.MinSize = New System.Drawing.Size(390, 60)
        Me.INDliName.Name = "INDliName"
        Me.INDliName.ShowInCustomizationForm = False
        Me.INDliName.Size = New System.Drawing.Size(390, 60)
        Me.INDliName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliName.Text = "Nombre"
        Me.INDliName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliName.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDliName.TextSize = New System.Drawing.Size(134, 21)
        Me.INDliName.TextToControlDistance = 5
        '
        'INDliExpenseType
        '
        Me.INDliExpenseType.AllowHide = False
        Me.INDliExpenseType.Control = Me.INDgleExpenseType
        Me.INDliExpenseType.CustomizationFormText = "Tipo de Gasto"
        Me.INDliExpenseType.Location = New System.Drawing.Point(0, 180)
        Me.INDliExpenseType.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDliExpenseType.MinSize = New System.Drawing.Size(390, 60)
        Me.INDliExpenseType.Name = "INDliExpenseType"
        Me.INDliExpenseType.ShowInCustomizationForm = False
        Me.INDliExpenseType.Size = New System.Drawing.Size(390, 60)
        Me.INDliExpenseType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliExpenseType.Text = "Clase de Costo"
        Me.INDliExpenseType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliExpenseType.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDliExpenseType.TextSize = New System.Drawing.Size(134, 21)
        Me.INDliExpenseType.TextToControlDistance = 5
        '
        'INDliDistribution
        '
        Me.INDliDistribution.AllowHide = False
        Me.INDliDistribution.Control = Me.INDgleDistribution
        Me.INDliDistribution.CustomizationFormText = "Distribución"
        Me.INDliDistribution.Location = New System.Drawing.Point(0, 300)
        Me.INDliDistribution.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDliDistribution.MinSize = New System.Drawing.Size(390, 60)
        Me.INDliDistribution.Name = "INDliDistribution"
        Me.INDliDistribution.ShowInCustomizationForm = False
        Me.INDliDistribution.Size = New System.Drawing.Size(390, 114)
        Me.INDliDistribution.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliDistribution.Text = "Distribución"
        Me.INDliDistribution.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliDistribution.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDliDistribution.TextSize = New System.Drawing.Size(133, 21)
        Me.INDliDistribution.TextToControlDistance = 5
        '
        'INDLciElementCostType
        '
        Me.INDLciElementCostType.Control = Me.INDSleElementCostType
        Me.INDLciElementCostType.Location = New System.Drawing.Point(0, 120)
        Me.INDLciElementCostType.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciElementCostType.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciElementCostType.Name = "INDLciElementCostType"
        Me.INDLciElementCostType.ShowInCustomizationForm = False
        Me.INDLciElementCostType.Size = New System.Drawing.Size(390, 60)
        Me.INDLciElementCostType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciElementCostType.Text = "Tipo de Costo"
        Me.INDLciElementCostType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciElementCostType.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciElementCostType.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciElementCostType.TextToControlDistance = 5
        '
        'INDlyItemCategory
        '
        Me.INDlyItemCategory.Control = Me.INDgleCategory
        Me.INDlyItemCategory.CustomizationFormText = "Categoría"
        Me.INDlyItemCategory.Location = New System.Drawing.Point(0, 240)
        Me.INDlyItemCategory.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemCategory.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemCategory.Name = "INDlyItemCategory"
        Me.INDlyItemCategory.ShowInCustomizationForm = False
        Me.INDlyItemCategory.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemCategory.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCategory.Text = "Categoría"
        Me.INDlyItemCategory.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCategory.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemCategory.TextSize = New System.Drawing.Size(134, 21)
        Me.INDlyItemCategory.TextToControlDistance = 5
        '
        'INDlcgDistributionBase
        '
        Me.INDlcgDistributionBase.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgDistributionBase.AppearanceGroup.Options.UseFont = True
        Me.INDlcgDistributionBase.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgDistributionBase.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgDistributionBase.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgDistributionBase.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgDistributionBase.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgDistributionBase.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgDistributionBase.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgDistributionBase.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgDistributionBase.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgDistributionBase.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgDistributionBase.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgDistributionBase.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgDistributionBase, False)
        Me.INDlcgDistributionBase.CustomizationFormText = "Base de Distribución"
        Me.INDlcgDistributionBase.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDliDistributionBase, Me.INDliAddDistribution})
        Me.INDlcgDistributionBase.Location = New System.Drawing.Point(414, 0)
        Me.INDlcgDistributionBase.Name = "INDlcgDistributionBase"
        Me.INDlcgDistributionBase.Size = New System.Drawing.Size(852, 473)
        Me.INDlcgDistributionBase.Text = "Base de Distribución"
        '
        'INDliDistributionBase
        '
        Me.INDliDistributionBase.Control = Me.INDgcDsitribution
        Me.INDliDistributionBase.CustomizationFormText = "Base de Distribución"
        Me.INDliDistributionBase.Location = New System.Drawing.Point(0, 36)
        Me.INDliDistributionBase.MaxSize = New System.Drawing.Size(828, 0)
        Me.INDliDistributionBase.MinSize = New System.Drawing.Size(828, 24)
        Me.INDliDistributionBase.Name = "INDliDistributionBase"
        Me.INDliDistributionBase.Size = New System.Drawing.Size(828, 378)
        Me.INDliDistributionBase.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliDistributionBase.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliDistributionBase.TextSize = New System.Drawing.Size(0, 0)
        Me.INDliDistributionBase.TextToControlDistance = 0
        Me.INDliDistributionBase.TextVisible = False
        '
        'INDliAddDistribution
        '
        Me.INDliAddDistribution.Control = Me.INDsbAddDistribution
        Me.INDliAddDistribution.CustomizationFormText = "Agregar Distribucion base"
        Me.INDliAddDistribution.Location = New System.Drawing.Point(0, 0)
        Me.INDliAddDistribution.MaxSize = New System.Drawing.Size(828, 36)
        Me.INDliAddDistribution.MinSize = New System.Drawing.Size(828, 36)
        Me.INDliAddDistribution.Name = "INDliAddDistribution"
        Me.INDliAddDistribution.Size = New System.Drawing.Size(828, 36)
        Me.INDliAddDistribution.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliAddDistribution.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliAddDistribution.TextSize = New System.Drawing.Size(0, 0)
        Me.INDliAddDistribution.TextToControlDistance = 0
        Me.INDliAddDistribution.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'FrmGeneralExpenses
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1324, 637)
        Me.Name = "FrmGeneralExpenses"
        Me.Opacity = 1.0R
        Me.Tag = "1210"
        Me.Text = "Elementos del Costo"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlcRoot.ResumeLayout(False)
        CType(Me.INDpccProductionCenter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpccProductionCenter.ResumeLayout(False)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDsleAccountId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSLeCostCenterConcept.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDspnDistributAmount.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleProductionCenter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliProductionCenter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliDistributedAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciCostCenter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciMainAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleElementCostType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgleDistribution.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImageCollection1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcDsitribution, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvDistributionBase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemImageComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgleExpenseType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbteCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgleCategory.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TreeList1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgRoot, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgMainData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliExpenseType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliDistribution, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciElementCostType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgDistributionBase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliDistributionBase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliAddDistribution, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoComboBoxEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDlcRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlcgRoot As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDgleExpenseType As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDtxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDbteCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDliCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDliName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDliExpenseType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlcgMainData As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents INDgcDsitribution As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvDistributionBase As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlcgDistributionBase As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDliDistributionBase As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ImageCollection1 As DevExpress.Utils.ImageCollection
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDsbAddDistribution As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDliAddDistribution As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDgleDistribution As DevExpress.XtraEditors.ImageComboBoxEdit
    Friend WithEvents INDliDistribution As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoComboBoxEdit1 As Presentation.Controls.IndigoComboBoxEdit
    Friend WithEvents RepositoryItemImageComboBox1 As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents INDSleElementCostType As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciElementCostType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDpccProductionCenter As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDsleAccountId As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn15 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn16 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSLeCostCenterConcept As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView4 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn209 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn210 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDsbAddProductionCenter As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDspnDistributAmount As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDsleProductionCenter As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDliProductionCenter As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDliDistributedAmount As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciCostCenter As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciMainAccount As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoTextEdit2 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDgleCategory As DevExpress.XtraEditors.TreeListLookUpEdit
    Friend WithEvents TreeList1 As DevExpress.XtraTreeList.TreeList
    Friend WithEvents TreeListColumn1 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents INDlyItemCategory As DevExpress.XtraLayout.LayoutControlItem
End Class
