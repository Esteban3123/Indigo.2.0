Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmCostGeneralExpenses
    Inherits FormBase

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmCostGeneralExpenses))
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject5 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject6 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject7 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject8 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject9 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject10 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlcRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDsleCategory = New DevExpress.XtraEditors.TreeListLookUpEdit()
        Me.TreeListLookUpEdit1TreeList = New DevExpress.XtraTreeList.TreeList()
        Me.TreeListColumn1 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.INDsleExpenseType = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
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
        Me.INDtxtName = New DevExpress.XtraEditors.TextEdit()
        Me.INDbteCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDSleDistributionType = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn51 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleGenerateAccountPayable = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn52 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleDirectLaborDistribution = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View21 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumnCode1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnName1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleReversalDirectLaborDistribution = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View22 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn522 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDlcgRoot = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlcgMainData = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDliCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliDistribution = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemElementCostType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemExpenseType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemCategory = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemDistributionType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemGenerateAccountPayable = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlcgDistributionBase = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDliDistributionBase = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliAddDistribution = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygJournalVoucherGrouping = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyDirectLaborDistribution = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyReversalDirectLaborDistribution = New DevExpress.XtraLayout.LayoutControlItem()
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
        Me.IndigoGridView11 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.RepositoryItemPopupContainerEdit11 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit111 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit12 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit121 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit112 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit13 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit1111 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit122 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit113 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit14 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit11111 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit1112 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit1121 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit1211 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit131 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit123 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit11112 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit15 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit1113 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit114 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit12111 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit1131 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit1311 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit1122 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit1221 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit111111 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit141 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit11211 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit11121 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit132 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit1212 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit124 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit11113 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit16 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit1114 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit115 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit12112 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit1132 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit1312 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit1123 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit1222 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit111112 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit142 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit11212 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit11122 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit133 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit1213 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlcRoot.SuspendLayout()
        CType(Me.INDsleCategory.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TreeListLookUpEdit1TreeList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleExpenseType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleElementCostType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgleDistribution.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImageCollection1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcDsitribution, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvDistributionBase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemImageComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbteCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleDistributionType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleGenerateAccountPayable.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleDirectLaborDistribution.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleReversalDirectLaborDistribution.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgMainData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliDistribution, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemElementCostType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemExpenseType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDistributionType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemGenerateAccountPayable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgDistributionBase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliDistributionBase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliAddDistribution, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygJournalVoucherGrouping, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyDirectLaborDistribution, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyReversalDirectLaborDistribution, System.ComponentModel.ISupportInitialize).BeginInit()
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
        CType(Me.IndigoGridView11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit111, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit121, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit112, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1111, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit122, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit113, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit11111, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1112, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1121, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1211, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit131, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit123, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit11112, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1113, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit114, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit12111, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1131, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1311, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1122, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1221, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit111111, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit141, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit11211, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit11121, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit132, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1212, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit124, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit11113, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1114, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit115, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit12112, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1132, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1312, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1123, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1222, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit111112, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit142, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit11212, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit11122, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit133, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1213, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlcRoot)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 136)
        Me.INDPanelControlBase.Margin = New System.Windows.Forms.Padding(4)
        Me.INDPanelControlBase.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1078, 347)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Location = New System.Drawing.Point(0, 6)
        Me.ToolBars.Margin = New System.Windows.Forms.Padding(4)
        Me.ToolBars.Size = New System.Drawing.Size(1078, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Margin = New System.Windows.Forms.Padding(6)
        Me.BarraBotones.Size = New System.Drawing.Size(1078, 130)
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.Appearance.BackColor = System.Drawing.SystemColors.Control
        Me.CtrNavigationControl1.Appearance.Options.UseBackColor = True
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDlcRoot
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 8)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 337)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDlcRoot
        '
        Me.INDlcRoot.Controls.Add(Me.INDsleCategory)
        Me.INDlcRoot.Controls.Add(Me.INDsleExpenseType)
        Me.INDlcRoot.Controls.Add(Me.INDSleElementCostType)
        Me.INDlcRoot.Controls.Add(Me.INDgleDistribution)
        Me.INDlcRoot.Controls.Add(Me.INDsbAddDistribution)
        Me.INDlcRoot.Controls.Add(Me.INDgcDsitribution)
        Me.INDlcRoot.Controls.Add(Me.INDtxtName)
        Me.INDlcRoot.Controls.Add(Me.INDbteCode)
        Me.INDlcRoot.Controls.Add(Me.INDSleDistributionType)
        Me.INDlcRoot.Controls.Add(Me.INDSleGenerateAccountPayable)
        Me.INDlcRoot.Controls.Add(Me.INDSleDirectLaborDistribution)
        Me.INDlcRoot.Controls.Add(Me.INDSleReversalDirectLaborDistribution)
        Me.INDlcRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlcRoot.Location = New System.Drawing.Point(202, 8)
        Me.INDlcRoot.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDlcRoot.Name = "INDlcRoot"
        Me.INDlcRoot.Root = Me.INDlcgRoot
        Me.INDlcRoot.Size = New System.Drawing.Size(874, 337)
        Me.INDlcRoot.TabIndex = 1
        Me.INDlcRoot.Text = "LayoutControl1"
        '
        'INDsleCategory
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleCategory, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleCategory, True)
        Me.INDsleCategory.EnterMoveNextControl = True
        Me.INDsleCategory.Location = New System.Drawing.Point(24, 206)
        Me.INDsleCategory.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleCategory, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleCategory.Name = "INDsleCategory"
        Me.INDsleCategory.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDsleCategory.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleCategory.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDsleCategory.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleCategory.Properties.Appearance.Options.UseFont = True
        Me.INDsleCategory.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleCategory.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleCategory.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleCategory.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleCategory.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleCategory.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleCategory.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleCategory.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleCategory.Properties.DisplayMember = "CodeName"
        Me.INDsleCategory.Properties.NullText = ""
        Me.INDsleCategory.Properties.TreeList = Me.TreeListLookUpEdit1TreeList
        Me.INDsleCategory.Properties.ValueMember = "Id"
        Me.INDsleCategory.Size = New System.Drawing.Size(386, 28)
        Me.INDsleCategory.StyleController = Me.INDlcRoot
        Me.INDsleCategory.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleCategory, 0)
        Me.INDsleCategory.ToolTip = "Este Campo es Necesario"
        '
        'TreeListLookUpEdit1TreeList
        '
        Me.TreeListLookUpEdit1TreeList.Appearance.FocusedRow.Options.UseBackColor = True
        Me.TreeListLookUpEdit1TreeList.Appearance.FocusedRow.Options.UseForeColor = True
        Me.TreeListLookUpEdit1TreeList.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.TreeListLookUpEdit1TreeList.Appearance.HeaderPanel.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.TreeListLookUpEdit1TreeList.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TreeListLookUpEdit1TreeList.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.TreeListLookUpEdit1TreeList.Appearance.HeaderPanel.Options.UseFont = True
        Me.TreeListLookUpEdit1TreeList.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TreeListLookUpEdit1TreeList.Appearance.Row.ForeColor = System.Drawing.Color.Black
        Me.TreeListLookUpEdit1TreeList.Appearance.Row.Options.UseFont = True
        Me.TreeListLookUpEdit1TreeList.Appearance.Row.Options.UseForeColor = True
        Me.TreeListLookUpEdit1TreeList.Columns.AddRange(New DevExpress.XtraTreeList.Columns.TreeListColumn() {Me.TreeListColumn1})
        Me.TreeListLookUpEdit1TreeList.KeyFieldName = "Id"
        Me.TreeListLookUpEdit1TreeList.Location = New System.Drawing.Point(-85, 76)
        Me.TreeListLookUpEdit1TreeList.Name = "TreeListLookUpEdit1TreeList"
        Me.TreeListLookUpEdit1TreeList.OptionsFilter.FilterMode = DevExpress.XtraTreeList.FilterMode.Matches
        Me.TreeListLookUpEdit1TreeList.OptionsFind.AllowFindPanel = True
        Me.TreeListLookUpEdit1TreeList.OptionsFind.AlwaysVisible = True
        Me.TreeListLookUpEdit1TreeList.OptionsView.EnableAppearanceEvenRow = True
        Me.TreeListLookUpEdit1TreeList.OptionsView.EnableAppearanceOddRow = True
        Me.TreeListLookUpEdit1TreeList.OptionsView.ShowIndentAsRowStyle = True
        Me.TreeListLookUpEdit1TreeList.ParentFieldName = "PadreId"
        Me.TreeListLookUpEdit1TreeList.Size = New System.Drawing.Size(400, 200)
        Me.TreeListLookUpEdit1TreeList.TabIndex = 0
        '
        'TreeListColumn1
        '
        Me.TreeListColumn1.Caption = "Descripción"
        Me.TreeListColumn1.FieldName = "CodeName"
        Me.TreeListColumn1.Name = "TreeListColumn1"
        Me.TreeListColumn1.Visible = True
        Me.TreeListColumn1.VisibleIndex = 0
        '
        'INDsleExpenseType
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDsleExpenseType, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleExpenseType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleExpenseType, True)
        Me.INDsleExpenseType.EnterMoveNextControl = True
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDsleExpenseType, False)
        Me.INDsleExpenseType.Location = New System.Drawing.Point(24, 146)
        Me.INDsleExpenseType.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleExpenseType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleExpenseType.Name = "INDsleExpenseType"
        Me.INDsleExpenseType.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDsleExpenseType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleExpenseType.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDsleExpenseType.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleExpenseType.Properties.Appearance.Options.UseFont = True
        Me.INDsleExpenseType.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleExpenseType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDsleExpenseType.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDsleExpenseType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleExpenseType.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleExpenseType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleExpenseType.Properties.DisplayMember = "Item2"
        Me.INDsleExpenseType.Properties.ImmediatePopup = True
        Me.INDsleExpenseType.Properties.NullText = ""
        Me.INDsleExpenseType.Properties.PopupView = Me.GridLookUpEdit1View
        Me.INDsleExpenseType.Properties.ValueMember = "Item1"
        Me.INDsleExpenseType.Size = New System.Drawing.Size(386, 28)
        Me.INDsleExpenseType.StyleController = Me.INDlcRoot
        Me.INDsleExpenseType.TabIndex = 3
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDsleExpenseType, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleExpenseType, 0)
        Me.INDsleExpenseType.ToolTip = "Este Campo es Necesario"
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
        Me.GridLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn6})
        Me.GridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit1View.Name = "GridLookUpEdit1View"
        Me.GridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView11.SetTemaIndigoMetro(Me.GridLookUpEdit1View, False)
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
        'INDSleElementCostType
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleElementCostType, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleElementCostType, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleElementCostType, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleElementCostType, True)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleElementCostType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleElementCostType, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleElementCostType, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleElementCostType, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleElementCostType, False)
        Me.INDSleElementCostType.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleElementCostType, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleElementCostType, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleElementCostType, False)
        Me.INDSleElementCostType.Location = New System.Drawing.Point(24, 26)
        Me.INDSleElementCostType.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleElementCostType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleElementCostType.Name = "INDSleElementCostType"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleElementCostType, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleElementCostType, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleElementCostType, False)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDSleElementCostType, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleElementCostType, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleElementCostType, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleElementCostType, False)
        Me.INDSleElementCostType.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleElementCostType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleElementCostType.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleElementCostType.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleElementCostType.Properties.Appearance.Options.UseFont = True
        Me.INDSleElementCostType.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleElementCostType.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleElementCostType.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleElementCostType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleElementCostType.Properties.DisplayMember = "Item2"
        Me.INDSleElementCostType.Properties.NullText = ""
        Me.INDSleElementCostType.Properties.PopupSizeable = False
        Me.INDSleElementCostType.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDSleElementCostType.Properties.ShowFooter = False
        Me.INDSleElementCostType.Properties.ValueMember = "Item1"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleElementCostType, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleElementCostType, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleElementCostType, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleElementCostType, True)
        Me.INDSleElementCostType.Size = New System.Drawing.Size(386, 28)
        Me.INDSleElementCostType.StyleController = Me.INDlcRoot
        Me.INDSleElementCostType.TabIndex = 2
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleElementCostType, Nothing)
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
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.IndigoGridView11.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
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
        Me.IndigoComboBoxEdit1.SetCampoObligatorio(Me.INDgleDistribution, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDgleDistribution, True)
        Me.INDgleDistribution.EnterMoveNextControl = True
        Me.INDgleDistribution.Location = New System.Drawing.Point(24, 266)
        Me.INDgleDistribution.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDgleDistribution, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDgleDistribution.Name = "INDgleDistribution"
        Me.INDgleDistribution.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDgleDistribution.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDgleDistribution.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDgleDistribution.Properties.Appearance.Options.UseBackColor = True
        Me.INDgleDistribution.Properties.Appearance.Options.UseFont = True
        Me.INDgleDistribution.Properties.Appearance.Options.UseForeColor = True
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
        Me.INDgleDistribution.ToolTip = "Este Campo es Necesario"
        '
        'ImageCollection1
        '
        Me.ImageCollection1.ImageStream = CType(resources.GetObject("ImageCollection1.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.ImageCollection1.InsertImage(Global.Presentation.Cost.My.Resources.Resources.ICONOGRAFIA_1_6_011, "ICONOGRAFIA_1_6_011", GetType(Global.Presentation.Cost.My.Resources.Resources), 0)
        Me.ImageCollection1.Images.SetKeyName(0, "ICONOGRAFIA_1_6_011")
        Me.ImageCollection1.InsertImage(Global.Presentation.Cost.My.Resources.Resources.ICONOGRAFIA_1_6_021, "ICONOGRAFIA_1_6_021", GetType(Global.Presentation.Cost.My.Resources.Resources), 1)
        Me.ImageCollection1.Images.SetKeyName(1, "ICONOGRAFIA_1_6_021")
        Me.ImageCollection1.InsertImage(Global.Presentation.Cost.My.Resources.Resources.ICONOGRAFIA_1_6_031, "ICONOGRAFIA_1_6_031", GetType(Global.Presentation.Cost.My.Resources.Resources), 2)
        Me.ImageCollection1.Images.SetKeyName(2, "ICONOGRAFIA_1_6_031")
        Me.ImageCollection1.InsertImage(Global.Presentation.Cost.My.Resources.Resources.ICONOGRAFIA_1_6_041, "ICONOGRAFIA_1_6_041", GetType(Global.Presentation.Cost.My.Resources.Resources), 3)
        Me.ImageCollection1.Images.SetKeyName(3, "ICONOGRAFIA_1_6_041")
        Me.ImageCollection1.InsertImage(Global.Presentation.Cost.My.Resources.Resources.ICONOGRAFIA_1_6_051, "ICONOGRAFIA_1_6_051", GetType(Global.Presentation.Cost.My.Resources.Resources), 4)
        Me.ImageCollection1.Images.SetKeyName(4, "ICONOGRAFIA_1_6_051")
        Me.ImageCollection1.InsertImage(Global.Presentation.Cost.My.Resources.Resources.ICONOGRAFIA_1_6_061, "ICONOGRAFIA_1_6_061", GetType(Global.Presentation.Cost.My.Resources.Resources), 5)
        Me.ImageCollection1.Images.SetKeyName(5, "ICONOGRAFIA_1_6_061")
        '
        'INDsbAddDistribution
        '
        Me.INDsbAddDistribution.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDsbAddDistribution.Appearance.Options.UseFont = True
        Me.INDsbAddDistribution.Location = New System.Drawing.Point(852, -180)
        Me.INDsbAddDistribution.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDsbAddDistribution.Name = "INDsbAddDistribution"
        Me.INDsbAddDistribution.Size = New System.Drawing.Size(824, 33)
        Me.INDsbAddDistribution.StyleController = Me.INDlcRoot
        Me.INDsbAddDistribution.TabIndex = 6
        Me.INDsbAddDistribution.Text = "Agregar Distribución"
        '
        'INDgcDsitribution
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcDsitribution, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcDsitribution, Nothing)
        Me.INDgcDsitribution.Cursor = System.Windows.Forms.Cursors.Default
        Me.INDgcDsitribution.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcDsitribution, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcDsitribution, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcDsitribution, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcDsitribution, False)
        Me.INDgcDsitribution.Location = New System.Drawing.Point(852, -143)
        Me.INDgcDsitribution.MainView = Me.INDgvDistributionBase
        Me.INDgcDsitribution.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDgcDsitribution.Name = "INDgcDsitribution"
        Me.INDgcDsitribution.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemImageComboBox1})
        Me.INDgcDsitribution.Size = New System.Drawing.Size(824, 439)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcDsitribution, DevExpress.XtraLayout.SizeConstraintsType.Custom)
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
        Me.INDgvDistributionBase.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvDistributionBase.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvDistributionBase.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.IndigoGridView11.SetTemaIndigoMetro(Me.INDgvDistributionBase, False)
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
        'INDtxtName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtName, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtName, True)
        Me.INDtxtName.EnterMoveNextControl = True
        Me.INDtxtName.Location = New System.Drawing.Point(24, -94)
        Me.INDtxtName.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtName.Name = "INDtxtName"
        Me.INDtxtName.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDtxtName.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtName.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDtxtName.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtName.Properties.Appearance.Options.UseFont = True
        Me.INDtxtName.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxtName.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtName.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtName.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtName.Properties.MaxLength = 100
        Me.INDtxtName.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtName.StyleController = Me.INDlcRoot
        Me.INDtxtName.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtName, 0)
        Me.INDtxtName.ToolTip = "Este Campo es Necesario"
        '
        'INDbteCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbteCode, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbteCode, True)
        Me.INDbteCode.Location = New System.Drawing.Point(24, -154)
        Me.INDbteCode.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDbteCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDbteCode.Name = "INDbteCode"
        Me.INDbteCode.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDbteCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbteCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDbteCode.Properties.Appearance.Options.UseFont = True
        Me.INDbteCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDbteCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDbteCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseFont = True
        EditorButtonImageOptions1.Image = Global.Presentation.Cost.My.Resources.Resources.BuscarMetro
        Me.INDbteCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDbteCode.Properties.MaxLength = 20
        Me.INDbteCode.Size = New System.Drawing.Size(386, 28)
        Me.INDbteCode.StyleController = Me.INDlcRoot
        Me.INDbteCode.TabIndex = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbteCode, 0)
        Me.INDbteCode.ToolTip = "Este Campo es Necesario"
        '
        'INDSleDistributionType
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleDistributionType, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleDistributionType, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleDistributionType, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleDistributionType, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleDistributionType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleDistributionType, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleDistributionType, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleDistributionType, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleDistributionType, False)
        Me.INDSleDistributionType.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleDistributionType, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleDistributionType, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleDistributionType, False)
        Me.INDSleDistributionType.Location = New System.Drawing.Point(24, -34)
        Me.INDSleDistributionType.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleDistributionType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleDistributionType.Name = "INDSleDistributionType"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleDistributionType, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleDistributionType, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleDistributionType, False)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDSleDistributionType, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleDistributionType, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleDistributionType, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleDistributionType, False)
        Me.INDSleDistributionType.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleDistributionType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleDistributionType.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleDistributionType.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleDistributionType.Properties.Appearance.Options.UseFont = True
        Me.INDSleDistributionType.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleDistributionType.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleDistributionType.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleDistributionType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleDistributionType.Properties.DisplayMember = "Item2"
        Me.INDSleDistributionType.Properties.NullText = ""
        Me.INDSleDistributionType.Properties.PopupSizeable = False
        Me.INDSleDistributionType.Properties.PopupView = Me.SearchLookUpEdit1View1
        Me.INDSleDistributionType.Properties.ShowFooter = False
        Me.INDSleDistributionType.Properties.ValueMember = "Item1"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleDistributionType, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleDistributionType, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleDistributionType, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleDistributionType, True)
        Me.INDSleDistributionType.Size = New System.Drawing.Size(386, 28)
        Me.INDSleDistributionType.StyleController = Me.INDlcRoot
        Me.INDSleDistributionType.TabIndex = 2
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleDistributionType, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleDistributionType, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleDistributionType, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleDistributionType, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleDistributionType, False)
        '
        'SearchLookUpEdit1View1
        '
        Me.SearchLookUpEdit1View1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit1View1.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit1View1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit1View1.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit1View1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View1.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit1View1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View1.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit1View1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit1View1.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit1View1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn51})
        Me.SearchLookUpEdit1View1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View1.Name = "SearchLookUpEdit1View1"
        Me.SearchLookUpEdit1View1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View1.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View1.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View1.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View1.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView11.SetTemaIndigoMetro(Me.SearchLookUpEdit1View1, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View1, False)
        '
        'GridColumn51
        '
        Me.GridColumn51.Caption = "Tipo"
        Me.GridColumn51.FieldName = "Item2"
        Me.GridColumn51.Name = "GridColumn51"
        Me.GridColumn51.Visible = True
        Me.GridColumn51.VisibleIndex = 0
        '
        'INDSleGenerateAccountPayable
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleGenerateAccountPayable, AppearanceObject5)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleGenerateAccountPayable, AppearanceObject6)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleGenerateAccountPayable, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleGenerateAccountPayable, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleGenerateAccountPayable, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleGenerateAccountPayable, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleGenerateAccountPayable, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleGenerateAccountPayable, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleGenerateAccountPayable, False)
        Me.INDSleGenerateAccountPayable.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleGenerateAccountPayable, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleGenerateAccountPayable, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleGenerateAccountPayable, False)
        Me.INDSleGenerateAccountPayable.Location = New System.Drawing.Point(24, 86)
        Me.INDSleGenerateAccountPayable.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleGenerateAccountPayable, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleGenerateAccountPayable.Name = "INDSleGenerateAccountPayable"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleGenerateAccountPayable, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleGenerateAccountPayable, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleGenerateAccountPayable, False)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDSleGenerateAccountPayable, True)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleGenerateAccountPayable, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleGenerateAccountPayable, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleGenerateAccountPayable, False)
        Me.INDSleGenerateAccountPayable.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleGenerateAccountPayable.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleGenerateAccountPayable.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleGenerateAccountPayable.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleGenerateAccountPayable.Properties.Appearance.Options.UseFont = True
        Me.INDSleGenerateAccountPayable.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleGenerateAccountPayable.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleGenerateAccountPayable.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleGenerateAccountPayable.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleGenerateAccountPayable.Properties.DisplayMember = "Item2"
        Me.INDSleGenerateAccountPayable.Properties.NullText = ""
        Me.INDSleGenerateAccountPayable.Properties.PopupSizeable = False
        Me.INDSleGenerateAccountPayable.Properties.PopupView = Me.SearchLookUpEdit1View2
        Me.INDSleGenerateAccountPayable.Properties.ShowFooter = False
        Me.INDSleGenerateAccountPayable.Properties.ValueMember = "Item1"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleGenerateAccountPayable, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleGenerateAccountPayable, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleGenerateAccountPayable, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleGenerateAccountPayable, True)
        Me.INDSleGenerateAccountPayable.Size = New System.Drawing.Size(386, 28)
        Me.INDSleGenerateAccountPayable.StyleController = Me.INDlcRoot
        Me.INDSleGenerateAccountPayable.TabIndex = 2
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleGenerateAccountPayable, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleGenerateAccountPayable, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleGenerateAccountPayable, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleGenerateAccountPayable, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleGenerateAccountPayable, False)
        '
        'SearchLookUpEdit1View2
        '
        Me.SearchLookUpEdit1View2.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit1View2.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit1View2.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit1View2.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit1View2.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View2.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit1View2.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View2.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit1View2.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit1View2.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit1View2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn52})
        Me.SearchLookUpEdit1View2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View2.Name = "SearchLookUpEdit1View2"
        Me.SearchLookUpEdit1View2.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View2.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View2.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View2.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View2.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView11.SetTemaIndigoMetro(Me.SearchLookUpEdit1View2, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View2, False)
        '
        'GridColumn52
        '
        Me.GridColumn52.Caption = "Tipo"
        Me.GridColumn52.FieldName = "Item2"
        Me.GridColumn52.Name = "GridColumn52"
        Me.GridColumn52.Visible = True
        Me.GridColumn52.VisibleIndex = 0
        '
        'INDSleDirectLaborDistribution
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleDirectLaborDistribution, AppearanceObject7)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleDirectLaborDistribution, AppearanceObject8)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleDirectLaborDistribution, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleDirectLaborDistribution, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleDirectLaborDistribution, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleDirectLaborDistribution, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleDirectLaborDistribution, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleDirectLaborDistribution, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleDirectLaborDistribution, False)
        Me.INDSleDirectLaborDistribution.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleDirectLaborDistribution, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleDirectLaborDistribution, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleDirectLaborDistribution, False)
        Me.INDSleDirectLaborDistribution.Location = New System.Drawing.Point(438, -154)
        Me.INDSleDirectLaborDistribution.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleDirectLaborDistribution, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleDirectLaborDistribution.Name = "INDSleDirectLaborDistribution"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleDirectLaborDistribution, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleDirectLaborDistribution, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleDirectLaborDistribution, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDSleDirectLaborDistribution, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleDirectLaborDistribution, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleDirectLaborDistribution, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleDirectLaborDistribution, False)
        Me.INDSleDirectLaborDistribution.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleDirectLaborDistribution.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleDirectLaborDistribution.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleDirectLaborDistribution.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleDirectLaborDistribution.Properties.Appearance.Options.UseFont = True
        Me.INDSleDirectLaborDistribution.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleDirectLaborDistribution.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleDirectLaborDistribution.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleDirectLaborDistribution.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleDirectLaborDistribution.Properties.DisplayMember = "CodeName"
        Me.INDSleDirectLaborDistribution.Properties.NullText = ""
        Me.INDSleDirectLaborDistribution.Properties.PopupSizeable = False
        Me.INDSleDirectLaborDistribution.Properties.PopupView = Me.SearchLookUpEdit1View21
        Me.INDSleDirectLaborDistribution.Properties.ShowFooter = False
        Me.INDSleDirectLaborDistribution.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleDirectLaborDistribution, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleDirectLaborDistribution, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleDirectLaborDistribution, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleDirectLaborDistribution, True)
        Me.INDSleDirectLaborDistribution.Size = New System.Drawing.Size(386, 28)
        Me.INDSleDirectLaborDistribution.StyleController = Me.INDlcRoot
        Me.INDSleDirectLaborDistribution.TabIndex = 2
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleDirectLaborDistribution, "607")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleDirectLaborDistribution, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleDirectLaborDistribution, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleDirectLaborDistribution, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleDirectLaborDistribution, False)
        '
        'SearchLookUpEdit1View21
        '
        Me.SearchLookUpEdit1View21.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit1View21.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit1View21.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit1View21.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit1View21.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View21.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit1View21.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View21.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit1View21.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit1View21.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit1View21.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumnCode1, Me.GridColumnName1})
        Me.SearchLookUpEdit1View21.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View21.Name = "SearchLookUpEdit1View21"
        Me.SearchLookUpEdit1View21.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View21.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View21.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View21.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View21.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView11.SetTemaIndigoMetro(Me.SearchLookUpEdit1View21, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View21, False)
        '
        'GridColumnCode1
        '
        Me.GridColumnCode1.Caption = "Código"
        Me.GridColumnCode1.FieldName = "Code"
        Me.GridColumnCode1.Name = "GridColumnCode1"
        Me.GridColumnCode1.Visible = True
        Me.GridColumnCode1.VisibleIndex = 0
        '
        'GridColumnName1
        '
        Me.GridColumnName1.Caption = "Descripción"
        Me.GridColumnName1.FieldName = "Name"
        Me.GridColumnName1.MinWidth = 14
        Me.GridColumnName1.Name = "GridColumnName1"
        Me.GridColumnName1.Visible = True
        Me.GridColumnName1.VisibleIndex = 1
        Me.GridColumnName1.Width = 50
        '
        'INDSleReversalDirectLaborDistribution
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleReversalDirectLaborDistribution, AppearanceObject9)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleReversalDirectLaborDistribution, AppearanceObject10)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleReversalDirectLaborDistribution, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleReversalDirectLaborDistribution, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleReversalDirectLaborDistribution, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleReversalDirectLaborDistribution, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleReversalDirectLaborDistribution, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleReversalDirectLaborDistribution, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleReversalDirectLaborDistribution, False)
        Me.INDSleReversalDirectLaborDistribution.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleReversalDirectLaborDistribution, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleReversalDirectLaborDistribution, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleReversalDirectLaborDistribution, False)
        Me.INDSleReversalDirectLaborDistribution.Location = New System.Drawing.Point(438, -94)
        Me.INDSleReversalDirectLaborDistribution.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleReversalDirectLaborDistribution, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleReversalDirectLaborDistribution.Name = "INDSleReversalDirectLaborDistribution"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleReversalDirectLaborDistribution, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleReversalDirectLaborDistribution, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleReversalDirectLaborDistribution, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDSleReversalDirectLaborDistribution, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleReversalDirectLaborDistribution, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleReversalDirectLaborDistribution, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleReversalDirectLaborDistribution, False)
        Me.INDSleReversalDirectLaborDistribution.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleReversalDirectLaborDistribution.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleReversalDirectLaborDistribution.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleReversalDirectLaborDistribution.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleReversalDirectLaborDistribution.Properties.Appearance.Options.UseFont = True
        Me.INDSleReversalDirectLaborDistribution.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleReversalDirectLaborDistribution.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleReversalDirectLaborDistribution.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleReversalDirectLaborDistribution.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleReversalDirectLaborDistribution.Properties.DisplayMember = "CodeName"
        Me.INDSleReversalDirectLaborDistribution.Properties.NullText = ""
        Me.INDSleReversalDirectLaborDistribution.Properties.PopupSizeable = False
        Me.INDSleReversalDirectLaborDistribution.Properties.PopupView = Me.SearchLookUpEdit1View22
        Me.INDSleReversalDirectLaborDistribution.Properties.ShowFooter = False
        Me.INDSleReversalDirectLaborDistribution.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleReversalDirectLaborDistribution, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleReversalDirectLaborDistribution, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleReversalDirectLaborDistribution, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleReversalDirectLaborDistribution, True)
        Me.INDSleReversalDirectLaborDistribution.Size = New System.Drawing.Size(386, 28)
        Me.INDSleReversalDirectLaborDistribution.StyleController = Me.INDlcRoot
        Me.INDSleReversalDirectLaborDistribution.TabIndex = 2
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleReversalDirectLaborDistribution, "607")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleReversalDirectLaborDistribution, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleReversalDirectLaborDistribution, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleReversalDirectLaborDistribution, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleReversalDirectLaborDistribution, False)
        '
        'SearchLookUpEdit1View22
        '
        Me.SearchLookUpEdit1View22.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit1View22.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit1View22.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit1View22.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit1View22.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View22.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit1View22.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View22.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit1View22.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit1View22.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit1View22.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn522, Me.GridColumn7})
        Me.SearchLookUpEdit1View22.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View22.Name = "SearchLookUpEdit1View22"
        Me.SearchLookUpEdit1View22.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View22.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View22.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View22.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View22.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView11.SetTemaIndigoMetro(Me.SearchLookUpEdit1View22, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View22, False)
        '
        'GridColumn522
        '
        Me.GridColumn522.Caption = "Código"
        Me.GridColumn522.FieldName = "Code"
        Me.GridColumn522.Name = "GridColumn522"
        Me.GridColumn522.Visible = True
        Me.GridColumn522.VisibleIndex = 0
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Descripción"
        Me.GridColumn7.FieldName = "Name"
        Me.GridColumn7.MinWidth = 14
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 1
        Me.GridColumn7.Width = 50
        '
        'INDlcgRoot
        '
        Me.INDlcgRoot.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgRoot.AppearanceGroup.Options.UseFont = True
        Me.INDlcgRoot.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.INDlcgRoot.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlcgMainData, Me.INDlcgDistributionBase, Me.INDlygJournalVoucherGrouping})
        Me.INDlcgRoot.Name = "Root"
        Me.INDlcgRoot.Size = New System.Drawing.Size(1700, 553)
        Me.INDlcgRoot.TextVisible = False
        '
        'INDlcgMainData
        '
        Me.INDlcgMainData.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgMainData.AppearanceGroup.Options.UseFont = True
        Me.INDlcgMainData.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.INDlcgMainData.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDliCode, Me.INDliName, Me.INDliDistribution, Me.INDlyItemElementCostType, Me.INDlyItemExpenseType, Me.INDlyItemCategory, Me.INDlyItemDistributionType, Me.INDlyItemGenerateAccountPayable})
        Me.INDlcgMainData.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgMainData.Name = "INDlcgMainData"
        Me.INDlcgMainData.Size = New System.Drawing.Size(414, 533)
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
        'INDliDistribution
        '
        Me.INDliDistribution.AllowHide = False
        Me.INDliDistribution.Control = Me.INDgleDistribution
        Me.INDliDistribution.CustomizationFormText = "Distribución"
        Me.INDliDistribution.Location = New System.Drawing.Point(0, 420)
        Me.INDliDistribution.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDliDistribution.MinSize = New System.Drawing.Size(390, 60)
        Me.INDliDistribution.Name = "INDliDistribution"
        Me.INDliDistribution.ShowInCustomizationForm = False
        Me.INDliDistribution.Size = New System.Drawing.Size(390, 60)
        Me.INDliDistribution.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliDistribution.Text = "Distribución"
        Me.INDliDistribution.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliDistribution.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDliDistribution.TextSize = New System.Drawing.Size(133, 21)
        Me.INDliDistribution.TextToControlDistance = 5
        '
        'INDlyItemElementCostType
        '
        Me.INDlyItemElementCostType.Control = Me.INDSleElementCostType
        Me.INDlyItemElementCostType.Location = New System.Drawing.Point(0, 180)
        Me.INDlyItemElementCostType.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemElementCostType.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemElementCostType.Name = "INDlyItemElementCostType"
        Me.INDlyItemElementCostType.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemElementCostType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemElementCostType.Text = "Tipo de Costo"
        Me.INDlyItemElementCostType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemElementCostType.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemElementCostType.TextSize = New System.Drawing.Size(134, 21)
        Me.INDlyItemElementCostType.TextToControlDistance = 5
        '
        'INDlyItemExpenseType
        '
        Me.INDlyItemExpenseType.Control = Me.INDsleExpenseType
        Me.INDlyItemExpenseType.Location = New System.Drawing.Point(0, 300)
        Me.INDlyItemExpenseType.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemExpenseType.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemExpenseType.Name = "INDlyItemExpenseType"
        Me.INDlyItemExpenseType.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemExpenseType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemExpenseType.Text = "Clase de Costo"
        Me.INDlyItemExpenseType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemExpenseType.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemExpenseType.TextSize = New System.Drawing.Size(134, 21)
        Me.INDlyItemExpenseType.TextToControlDistance = 5
        '
        'INDlyItemCategory
        '
        Me.INDlyItemCategory.Control = Me.INDsleCategory
        Me.INDlyItemCategory.Location = New System.Drawing.Point(0, 360)
        Me.INDlyItemCategory.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemCategory.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemCategory.Name = "INDlyItemCategory"
        Me.INDlyItemCategory.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemCategory.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCategory.Text = "Categoría"
        Me.INDlyItemCategory.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCategory.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemCategory.TextSize = New System.Drawing.Size(134, 21)
        Me.INDlyItemCategory.TextToControlDistance = 5
        '
        'INDlyItemDistributionType
        '
        Me.INDlyItemDistributionType.Control = Me.INDSleDistributionType
        Me.INDlyItemDistributionType.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDlyItemDistributionType.CustomizationFormText = "Tipo de Distribución"
        Me.INDlyItemDistributionType.Location = New System.Drawing.Point(0, 120)
        Me.INDlyItemDistributionType.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemDistributionType.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemDistributionType.Name = "INDlyItemDistributionType"
        Me.INDlyItemDistributionType.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemDistributionType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemDistributionType.Text = "Tipo de Distribución"
        Me.INDlyItemDistributionType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemDistributionType.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemDistributionType.TextSize = New System.Drawing.Size(134, 21)
        Me.INDlyItemDistributionType.TextToControlDistance = 5
        '
        'INDlyItemGenerateAccountPayable
        '
        Me.INDlyItemGenerateAccountPayable.Control = Me.INDSleGenerateAccountPayable
        Me.INDlyItemGenerateAccountPayable.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDlyItemGenerateAccountPayable.CustomizationFormText = "Generar Cuenta por Pagar"
        Me.INDlyItemGenerateAccountPayable.Location = New System.Drawing.Point(0, 240)
        Me.INDlyItemGenerateAccountPayable.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemGenerateAccountPayable.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemGenerateAccountPayable.Name = "INDlyItemGenerateAccountPayable"
        Me.INDlyItemGenerateAccountPayable.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemGenerateAccountPayable.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemGenerateAccountPayable.Text = "Generar Cuenta por Pagar"
        Me.INDlyItemGenerateAccountPayable.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemGenerateAccountPayable.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemGenerateAccountPayable.TextSize = New System.Drawing.Size(134, 21)
        Me.INDlyItemGenerateAccountPayable.TextToControlDistance = 5
        '
        'INDlcgDistributionBase
        '
        Me.INDlcgDistributionBase.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgDistributionBase.AppearanceGroup.Options.UseFont = True
        Me.INDlcgDistributionBase.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.INDlcgDistributionBase.Location = New System.Drawing.Point(828, 0)
        Me.INDlcgDistributionBase.Name = "INDlcgDistributionBase"
        Me.INDlcgDistributionBase.Size = New System.Drawing.Size(852, 533)
        Me.INDlcgDistributionBase.Text = "Base de Distribución"
        '
        'INDliDistributionBase
        '
        Me.INDliDistributionBase.Control = Me.INDgcDsitribution
        Me.INDliDistributionBase.CustomizationFormText = "Base de Distribución"
        Me.INDliDistributionBase.Location = New System.Drawing.Point(0, 37)
        Me.INDliDistributionBase.MaxSize = New System.Drawing.Size(828, 0)
        Me.INDliDistributionBase.MinSize = New System.Drawing.Size(828, 24)
        Me.INDliDistributionBase.Name = "INDliDistributionBase"
        Me.INDliDistributionBase.Size = New System.Drawing.Size(828, 443)
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
        Me.INDliAddDistribution.MaxSize = New System.Drawing.Size(828, 37)
        Me.INDliAddDistribution.MinSize = New System.Drawing.Size(828, 37)
        Me.INDliAddDistribution.Name = "INDliAddDistribution"
        Me.INDliAddDistribution.Size = New System.Drawing.Size(828, 37)
        Me.INDliAddDistribution.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliAddDistribution.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliAddDistribution.TextSize = New System.Drawing.Size(0, 0)
        Me.INDliAddDistribution.TextToControlDistance = 0
        Me.INDliAddDistribution.TextVisible = False
        '
        'INDlygJournalVoucherGrouping
        '
        Me.INDlygJournalVoucherGrouping.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygJournalVoucherGrouping.AppearanceGroup.Options.UseFont = True
        Me.INDlygJournalVoucherGrouping.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygJournalVoucherGrouping.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygJournalVoucherGrouping.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygJournalVoucherGrouping.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygJournalVoucherGrouping.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygJournalVoucherGrouping.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygJournalVoucherGrouping.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygJournalVoucherGrouping.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygJournalVoucherGrouping.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygJournalVoucherGrouping.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygJournalVoucherGrouping.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygJournalVoucherGrouping.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygJournalVoucherGrouping, False)
        Me.INDlygJournalVoucherGrouping.CustomizationFormText = "Comprobantes Contables"
        Me.INDlygJournalVoucherGrouping.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyDirectLaborDistribution, Me.INDlyReversalDirectLaborDistribution})
        Me.INDlygJournalVoucherGrouping.Location = New System.Drawing.Point(414, 0)
        Me.INDlygJournalVoucherGrouping.Name = "INDlygJournalVoucherGrouping"
        Me.INDlygJournalVoucherGrouping.Size = New System.Drawing.Size(414, 533)
        Me.INDlygJournalVoucherGrouping.Text = "Comprobantes Contables"
        '
        'INDlyDirectLaborDistribution
        '
        Me.INDlyDirectLaborDistribution.Control = Me.INDSleDirectLaborDistribution
        Me.INDlyDirectLaborDistribution.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDlyDirectLaborDistribution.CustomizationFormText = "Distribución mano de obra directa"
        Me.INDlyDirectLaborDistribution.Location = New System.Drawing.Point(0, 0)
        Me.INDlyDirectLaborDistribution.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyDirectLaborDistribution.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyDirectLaborDistribution.Name = "INDlyDirectLaborDistribution"
        Me.INDlyDirectLaborDistribution.Size = New System.Drawing.Size(390, 60)
        Me.INDlyDirectLaborDistribution.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyDirectLaborDistribution.Text = "Distribución mano de obra directa"
        Me.INDlyDirectLaborDistribution.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyDirectLaborDistribution.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyDirectLaborDistribution.TextSize = New System.Drawing.Size(134, 21)
        Me.INDlyDirectLaborDistribution.TextToControlDistance = 5
        '
        'INDlyReversalDirectLaborDistribution
        '
        Me.INDlyReversalDirectLaborDistribution.Control = Me.INDSleReversalDirectLaborDistribution
        Me.INDlyReversalDirectLaborDistribution.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDlyReversalDirectLaborDistribution.CustomizationFormText = "Reversión distribución mano de obra directa"
        Me.INDlyReversalDirectLaborDistribution.Location = New System.Drawing.Point(0, 60)
        Me.INDlyReversalDirectLaborDistribution.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyReversalDirectLaborDistribution.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyReversalDirectLaborDistribution.Name = "INDlyReversalDirectLaborDistribution"
        Me.INDlyReversalDirectLaborDistribution.Size = New System.Drawing.Size(390, 420)
        Me.INDlyReversalDirectLaborDistribution.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyReversalDirectLaborDistribution.Text = "Reversión distribución mano de obra directa"
        Me.INDlyReversalDirectLaborDistribution.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyReversalDirectLaborDistribution.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyReversalDirectLaborDistribution.TextSize = New System.Drawing.Size(134, 21)
        Me.INDlyReversalDirectLaborDistribution.TextToControlDistance = 5
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'IndigoGridView11
        '
        Me.IndigoGridView11.RaiseMenuPopUp = True
        Me.IndigoGridView11.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit11
        '
        'RepositoryItemPopupContainerEdit11
        '
        Me.RepositoryItemPopupContainerEdit11.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit11.Name = "RepositoryItemPopupContainerEdit11"
        '
        'RepositoryItemPopupContainerEdit111
        '
        Me.RepositoryItemPopupContainerEdit111.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit111.Name = "RepositoryItemPopupContainerEdit111"
        '
        'RepositoryItemPopupContainerEdit12
        '
        Me.RepositoryItemPopupContainerEdit12.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit12.Name = "RepositoryItemPopupContainerEdit12"
        '
        'RepositoryItemPopupContainerEdit121
        '
        Me.RepositoryItemPopupContainerEdit121.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit121.Name = "RepositoryItemPopupContainerEdit121"
        '
        'RepositoryItemPopupContainerEdit112
        '
        Me.RepositoryItemPopupContainerEdit112.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit112.Name = "RepositoryItemPopupContainerEdit112"
        '
        'RepositoryItemPopupContainerEdit13
        '
        Me.RepositoryItemPopupContainerEdit13.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit13.Name = "RepositoryItemPopupContainerEdit13"
        '
        'RepositoryItemPopupContainerEdit1111
        '
        Me.RepositoryItemPopupContainerEdit1111.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1111.Name = "RepositoryItemPopupContainerEdit1111"
        '
        'RepositoryItemPopupContainerEdit122
        '
        Me.RepositoryItemPopupContainerEdit122.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit122.Name = "RepositoryItemPopupContainerEdit122"
        '
        'RepositoryItemPopupContainerEdit113
        '
        Me.RepositoryItemPopupContainerEdit113.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit113.Name = "RepositoryItemPopupContainerEdit113"
        '
        'RepositoryItemPopupContainerEdit14
        '
        Me.RepositoryItemPopupContainerEdit14.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit14.Name = "RepositoryItemPopupContainerEdit14"
        '
        'RepositoryItemPopupContainerEdit11111
        '
        Me.RepositoryItemPopupContainerEdit11111.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit11111.Name = "RepositoryItemPopupContainerEdit11111"
        '
        'RepositoryItemPopupContainerEdit1112
        '
        Me.RepositoryItemPopupContainerEdit1112.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1112.Name = "RepositoryItemPopupContainerEdit1112"
        '
        'RepositoryItemPopupContainerEdit1121
        '
        Me.RepositoryItemPopupContainerEdit1121.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1121.Name = "RepositoryItemPopupContainerEdit1121"
        '
        'RepositoryItemPopupContainerEdit1211
        '
        Me.RepositoryItemPopupContainerEdit1211.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1211.Name = "RepositoryItemPopupContainerEdit1211"
        '
        'RepositoryItemPopupContainerEdit131
        '
        Me.RepositoryItemPopupContainerEdit131.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit131.Name = "RepositoryItemPopupContainerEdit131"
        '
        'RepositoryItemPopupContainerEdit123
        '
        Me.RepositoryItemPopupContainerEdit123.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit123.Name = "RepositoryItemPopupContainerEdit123"
        '
        'RepositoryItemPopupContainerEdit11112
        '
        Me.RepositoryItemPopupContainerEdit11112.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit11112.Name = "RepositoryItemPopupContainerEdit11112"
        '
        'RepositoryItemPopupContainerEdit15
        '
        Me.RepositoryItemPopupContainerEdit15.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit15.Name = "RepositoryItemPopupContainerEdit15"
        '
        'RepositoryItemPopupContainerEdit1113
        '
        Me.RepositoryItemPopupContainerEdit1113.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1113.Name = "RepositoryItemPopupContainerEdit1113"
        '
        'RepositoryItemPopupContainerEdit114
        '
        Me.RepositoryItemPopupContainerEdit114.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit114.Name = "RepositoryItemPopupContainerEdit114"
        '
        'RepositoryItemPopupContainerEdit12111
        '
        Me.RepositoryItemPopupContainerEdit12111.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit12111.Name = "RepositoryItemPopupContainerEdit12111"
        '
        'RepositoryItemPopupContainerEdit1131
        '
        Me.RepositoryItemPopupContainerEdit1131.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1131.Name = "RepositoryItemPopupContainerEdit1131"
        '
        'RepositoryItemPopupContainerEdit1311
        '
        Me.RepositoryItemPopupContainerEdit1311.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1311.Name = "RepositoryItemPopupContainerEdit1311"
        '
        'RepositoryItemPopupContainerEdit1122
        '
        Me.RepositoryItemPopupContainerEdit1122.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1122.Name = "RepositoryItemPopupContainerEdit1122"
        '
        'RepositoryItemPopupContainerEdit1221
        '
        Me.RepositoryItemPopupContainerEdit1221.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1221.Name = "RepositoryItemPopupContainerEdit1221"
        '
        'RepositoryItemPopupContainerEdit111111
        '
        Me.RepositoryItemPopupContainerEdit111111.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit111111.Name = "RepositoryItemPopupContainerEdit111111"
        '
        'RepositoryItemPopupContainerEdit141
        '
        Me.RepositoryItemPopupContainerEdit141.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit141.Name = "RepositoryItemPopupContainerEdit141"
        '
        'RepositoryItemPopupContainerEdit11211
        '
        Me.RepositoryItemPopupContainerEdit11211.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit11211.Name = "RepositoryItemPopupContainerEdit11211"
        '
        'RepositoryItemPopupContainerEdit11121
        '
        Me.RepositoryItemPopupContainerEdit11121.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit11121.Name = "RepositoryItemPopupContainerEdit11121"
        '
        'RepositoryItemPopupContainerEdit132
        '
        Me.RepositoryItemPopupContainerEdit132.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit132.Name = "RepositoryItemPopupContainerEdit132"
        '
        'RepositoryItemPopupContainerEdit1212
        '
        Me.RepositoryItemPopupContainerEdit1212.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1212.Name = "RepositoryItemPopupContainerEdit1212"
        '
        'RepositoryItemPopupContainerEdit124
        '
        Me.RepositoryItemPopupContainerEdit124.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit124.Name = "RepositoryItemPopupContainerEdit124"
        '
        'RepositoryItemPopupContainerEdit11113
        '
        Me.RepositoryItemPopupContainerEdit11113.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit11113.Name = "RepositoryItemPopupContainerEdit11113"
        '
        'RepositoryItemPopupContainerEdit16
        '
        Me.RepositoryItemPopupContainerEdit16.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit16.Name = "RepositoryItemPopupContainerEdit16"
        '
        'RepositoryItemPopupContainerEdit1114
        '
        Me.RepositoryItemPopupContainerEdit1114.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1114.Name = "RepositoryItemPopupContainerEdit1114"
        '
        'RepositoryItemPopupContainerEdit115
        '
        Me.RepositoryItemPopupContainerEdit115.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit115.Name = "RepositoryItemPopupContainerEdit115"
        '
        'RepositoryItemPopupContainerEdit12112
        '
        Me.RepositoryItemPopupContainerEdit12112.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit12112.Name = "RepositoryItemPopupContainerEdit12112"
        '
        'RepositoryItemPopupContainerEdit1132
        '
        Me.RepositoryItemPopupContainerEdit1132.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1132.Name = "RepositoryItemPopupContainerEdit1132"
        '
        'RepositoryItemPopupContainerEdit1312
        '
        Me.RepositoryItemPopupContainerEdit1312.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1312.Name = "RepositoryItemPopupContainerEdit1312"
        '
        'RepositoryItemPopupContainerEdit1123
        '
        Me.RepositoryItemPopupContainerEdit1123.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1123.Name = "RepositoryItemPopupContainerEdit1123"
        '
        'RepositoryItemPopupContainerEdit1222
        '
        Me.RepositoryItemPopupContainerEdit1222.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1222.Name = "RepositoryItemPopupContainerEdit1222"
        '
        'RepositoryItemPopupContainerEdit111112
        '
        Me.RepositoryItemPopupContainerEdit111112.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit111112.Name = "RepositoryItemPopupContainerEdit111112"
        '
        'RepositoryItemPopupContainerEdit142
        '
        Me.RepositoryItemPopupContainerEdit142.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit142.Name = "RepositoryItemPopupContainerEdit142"
        '
        'RepositoryItemPopupContainerEdit11212
        '
        Me.RepositoryItemPopupContainerEdit11212.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit11212.Name = "RepositoryItemPopupContainerEdit11212"
        '
        'RepositoryItemPopupContainerEdit11122
        '
        Me.RepositoryItemPopupContainerEdit11122.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit11122.Name = "RepositoryItemPopupContainerEdit11122"
        '
        'RepositoryItemPopupContainerEdit133
        '
        Me.RepositoryItemPopupContainerEdit133.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit133.Name = "RepositoryItemPopupContainerEdit133"
        '
        'RepositoryItemPopupContainerEdit1213
        '
        Me.RepositoryItemPopupContainerEdit1213.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1213.Name = "RepositoryItemPopupContainerEdit1213"
        '
        'FrmCostGeneralExpenses
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1078, 483)
        Me.IconOptions.ShowIcon = False
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmCostGeneralExpenses"
        Me.Opacity = 1.0R
        Me.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.Tag = "1738"
        Me.Text = "Elementos del Costo"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlcRoot.ResumeLayout(False)
        CType(Me.INDsleCategory.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TreeListLookUpEdit1TreeList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleExpenseType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleElementCostType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgleDistribution.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImageCollection1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcDsitribution, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvDistributionBase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemImageComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbteCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleDistributionType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleGenerateAccountPayable.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleDirectLaborDistribution.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleReversalDirectLaborDistribution.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgRoot, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgMainData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliDistribution, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemElementCostType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemExpenseType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDistributionType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemGenerateAccountPayable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgDistributionBase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliDistributionBase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliAddDistribution, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygJournalVoucherGrouping, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyDirectLaborDistribution, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyReversalDirectLaborDistribution, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.IndigoGridView11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit111, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit121, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit112, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1111, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit122, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit113, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit11111, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1112, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1121, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1211, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit131, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit123, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit11112, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1113, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit114, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit12111, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1131, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1311, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1122, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1221, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit111111, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit141, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit11211, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit11121, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit132, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1212, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit124, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit11113, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1114, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit115, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit12112, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1132, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1312, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1123, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1222, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit111112, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit142, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit11212, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit11122, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit133, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1213, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDlcRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlcgRoot As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDtxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDbteCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDliCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDliName As DevExpress.XtraLayout.LayoutControlItem
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
    Friend WithEvents INDsbAddDistribution As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDliAddDistribution As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDgleDistribution As DevExpress.XtraEditors.ImageComboBoxEdit
    Friend WithEvents INDliDistribution As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoComboBoxEdit1 As Presentation.Controls.IndigoComboBoxEdit
    Friend WithEvents RepositoryItemImageComboBox1 As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents INDSleElementCostType As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlyItemElementCostType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsleExpenseType As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlyItemExpenseType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsleCategory As DevExpress.XtraEditors.TreeListLookUpEdit
    Friend WithEvents TreeListLookUpEdit1TreeList As DevExpress.XtraTreeList.TreeList
    Friend WithEvents TreeListColumn1 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents INDlyItemCategory As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSleDistributionType As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn51 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlyItemDistributionType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents IndigoGridView11 As IndigoGridView
    Friend WithEvents RepositoryItemPopupContainerEdit11 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDSleGenerateAccountPayable As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn52 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlyItemGenerateAccountPayable As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents RepositoryItemPopupContainerEdit12 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit111 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDlygJournalVoucherGrouping As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents RepositoryItemPopupContainerEdit1111 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit112 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit121 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit13 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit1211 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit113 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit131 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit122 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit11111 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit14 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit1121 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit1112 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit11211 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit11121 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit1212 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit1122 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit141 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit132 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit1221 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit1131 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit1311 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit114 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit12111 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit111111 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit11113 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit1114 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit16 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit12112 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit15 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit115 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit1312 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit1132 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit1222 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit1123 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit142 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit111112 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit11122 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit11212 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit124 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit1113 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit133 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit11112 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit1213 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit123 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDSleDirectLaborDistribution As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View21 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn510 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSleReversalDirectLaborDistribution As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View22 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn522 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlyDirectLaborDistribution As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyReversalDirectLaborDistribution As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn511 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnCode1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnName1 As DevExpress.XtraGrid.Columns.GridColumn
End Class

