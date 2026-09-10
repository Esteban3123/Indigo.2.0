Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmBudgetItemsExpense
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmBudgetItemsExpense))
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlyBudgetItem = New DevExpress.XtraLayout.LayoutControl()
        Me.INDtlBudgetItems = New DevExpress.XtraTreeList.TreeList()
        Me.INDtColCode = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.INDtColCode1 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.INDtColFinancialSource = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.INDtColName = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.INDtColAuxiliary = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.INDtColUsed = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.INDbtnAddItem = New DevExpress.XtraEditors.SimpleButton()
        Me.INDsleBudgetEntity = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColBINameCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleValidity = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDgvValidity = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColVYear = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColVStatus = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyGrItem = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemBudgetEntity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemValidity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyGrListItems = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemBudgetItems = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemAddItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.INDbarBtnAdd = New DevExpress.XtraBars.BarButtonItem()
        Me.INDbarBtnEdit = New DevExpress.XtraBars.BarButtonItem()
        Me.INDbarBtnClose = New DevExpress.XtraBars.BarButtonItem()
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.INDpopUpMenu = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.IndigoTreeList1 = New Presentation.Controls.IndigoTreeList(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyBudgetItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyBudgetItem.SuspendLayout()
        CType(Me.INDtlBudgetItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleBudgetEntity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleValidity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvValidity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGrItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemBudgetEntity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemValidity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGrListItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemBudgetItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemAddItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpopUpMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTreeList1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyBudgetItem)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1490, 541)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1490, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(1490, 130)
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDlyBudgetItem
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 532)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDlyBudgetItem
        '
        Me.INDlyBudgetItem.AllowCustomization = False
        Me.INDlyBudgetItem.Controls.Add(Me.INDtlBudgetItems)
        Me.INDlyBudgetItem.Controls.Add(Me.INDbtnAddItem)
        Me.INDlyBudgetItem.Controls.Add(Me.INDsleBudgetEntity)
        Me.INDlyBudgetItem.Controls.Add(Me.INDsleValidity)
        Me.INDlyBudgetItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDlyBudgetItem, False)
        Me.INDlyBudgetItem.Location = New System.Drawing.Point(202, 7)
        Me.INDlyBudgetItem.Name = "INDlyBudgetItem"
        Me.INDlyBudgetItem.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(751, 342, 250, 350)
        Me.INDlyBudgetItem.Root = Me.LayoutControlGroup1
        Me.INDlyBudgetItem.Size = New System.Drawing.Size(1286, 532)
        Me.INDlyBudgetItem.TabIndex = 1
        Me.INDlyBudgetItem.Text = "LayoutControl1"
        '
        'INDtlBudgetItems
        '
        Me.INDtlBudgetItems.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDtlBudgetItems.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDtlBudgetItems.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.INDtlBudgetItems.Appearance.HeaderPanel.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.INDtlBudgetItems.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDtlBudgetItems.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.INDtlBudgetItems.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDtlBudgetItems.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDtlBudgetItems.Appearance.Row.ForeColor = System.Drawing.Color.Black
        Me.INDtlBudgetItems.Appearance.Row.Options.UseFont = True
        Me.INDtlBudgetItems.Appearance.Row.Options.UseForeColor = True
        Me.INDtlBudgetItems.Columns.AddRange(New DevExpress.XtraTreeList.Columns.TreeListColumn() {Me.INDtColCode, Me.INDtColCode1, Me.INDtColFinancialSource, Me.INDtColName, Me.INDtColAuxiliary, Me.INDtColUsed})
        Me.IndigoTreeList1.SetExpandedAllNodes(Me.INDtlBudgetItems, False)
        Me.INDtlBudgetItems.KeyFieldName = "Id"
        Me.INDtlBudgetItems.Location = New System.Drawing.Point(438, 89)
        Me.INDtlBudgetItems.Name = "INDtlBudgetItems"
        Me.INDtlBudgetItems.OptionsBehavior.PopulateServiceColumns = True
        Me.INDtlBudgetItems.OptionsFilter.FilterMode = DevExpress.XtraTreeList.FilterMode.Matches
        Me.INDtlBudgetItems.OptionsFind.AlwaysVisible = True
        Me.INDtlBudgetItems.OptionsView.EnableAppearanceEvenRow = True
        Me.INDtlBudgetItems.OptionsView.EnableAppearanceOddRow = True
        Me.INDtlBudgetItems.ParentFieldName = "CategoryOwnerId"
        Me.INDtlBudgetItems.Size = New System.Drawing.Size(824, 419)
        Me.INDtlBudgetItems.TabIndex = 15
        '
        'INDtColCode
        '
        Me.INDtColCode.Caption = "Código"
        Me.INDtColCode.FieldName = "Code"
        Me.INDtColCode.Name = "INDtColCode"
        Me.INDtColCode.OptionsColumn.AllowEdit = False
        Me.INDtColCode.OptionsColumn.AllowFocus = False
        Me.INDtColCode.Visible = True
        Me.INDtColCode.VisibleIndex = 0
        Me.INDtColCode.Width = 67
        '
        'INDtColCode1
        '
        Me.INDtColCode1.Caption = "Cod Alterno "
        Me.INDtColCode1.FieldName = "AlternativeCode"
        Me.INDtColCode1.Name = "INDtColCode1"
        Me.INDtColCode1.OptionsColumn.AllowEdit = False
        Me.INDtColCode1.OptionsColumn.AllowFocus = False
        Me.INDtColCode1.Visible = True
        Me.INDtColCode1.VisibleIndex = 1
        Me.INDtColCode1.Width = 97
        '
        'INDtColFinancialSource
        '
        Me.INDtColFinancialSource.Caption = "Recurso"
        Me.INDtColFinancialSource.FieldName = "FinancialSourceDescription"
        Me.INDtColFinancialSource.Name = "INDtColFinancialSource"
        Me.INDtColFinancialSource.OptionsColumn.AllowEdit = False
        Me.INDtColFinancialSource.OptionsColumn.AllowFocus = False
        Me.INDtColFinancialSource.Visible = True
        Me.INDtColFinancialSource.VisibleIndex = 2
        Me.INDtColFinancialSource.Width = 95
        '
        'INDtColName
        '
        Me.INDtColName.Caption = "Nombre"
        Me.INDtColName.FieldName = "Name"
        Me.INDtColName.Name = "INDtColName"
        Me.INDtColName.OptionsColumn.AllowEdit = False
        Me.INDtColName.OptionsColumn.AllowFocus = False
        Me.INDtColName.Visible = True
        Me.INDtColName.VisibleIndex = 3
        Me.INDtColName.Width = 97
        '
        'INDtColAuxiliary
        '
        Me.INDtColAuxiliary.Caption = "Auxiliar"
        Me.INDtColAuxiliary.FieldName = "Auxiliary"
        Me.INDtColAuxiliary.Name = "INDtColAuxiliary"
        Me.INDtColAuxiliary.OptionsColumn.AllowEdit = False
        Me.INDtColAuxiliary.OptionsColumn.AllowFocus = False
        Me.INDtColAuxiliary.SortOrder = System.Windows.Forms.SortOrder.Ascending
        Me.INDtColAuxiliary.Visible = True
        Me.INDtColAuxiliary.VisibleIndex = 4
        Me.INDtColAuxiliary.Width = 72
        '
        'INDtColUsed
        '
        Me.INDtColUsed.Caption = "Usado"
        Me.INDtColUsed.FieldName = "Used"
        Me.INDtColUsed.Name = "INDtColUsed"
        Me.INDtColUsed.OptionsColumn.AllowEdit = False
        Me.INDtColUsed.OptionsColumn.AllowFocus = False
        Me.INDtColUsed.Visible = True
        Me.INDtColUsed.VisibleIndex = 5
        Me.INDtColUsed.Width = 85
        '
        'INDbtnAddItem
        '
        Me.INDbtnAddItem.Location = New System.Drawing.Point(438, 53)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnAddItem, False)
        Me.INDbtnAddItem.Name = "INDbtnAddItem"
        Me.INDbtnAddItem.Size = New System.Drawing.Size(824, 32)
        Me.INDbtnAddItem.StyleController = Me.INDlyBudgetItem
        Me.INDbtnAddItem.TabIndex = 14
        Me.INDbtnAddItem.Text = "Agregar Rubro"
        '
        'INDsleBudgetEntity
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleBudgetEntity, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleBudgetEntity, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleBudgetEntity, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleBudgetEntity, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleBudgetEntity, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleBudgetEntity, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleBudgetEntity, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleBudgetEntity, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleBudgetEntity, False)
        Me.INDsleBudgetEntity.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleBudgetEntity, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleBudgetEntity, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleBudgetEntity, False)
        Me.INDsleBudgetEntity.Location = New System.Drawing.Point(24, 79)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleBudgetEntity, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleBudgetEntity.Name = "INDsleBudgetEntity"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleBudgetEntity, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleBudgetEntity, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleBudgetEntity, True)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleBudgetEntity, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleBudgetEntity, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleBudgetEntity, False)
        Me.INDsleBudgetEntity.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDsleBudgetEntity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleBudgetEntity.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleBudgetEntity.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleBudgetEntity.Properties.Appearance.Options.UseFont = True
        Me.INDsleBudgetEntity.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleBudgetEntity.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleBudgetEntity.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleBudgetEntity.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleBudgetEntity.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleBudgetEntity.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleBudgetEntity.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleBudgetEntity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleBudgetEntity.Properties.DisplayMember = "NameCode"
        Me.INDsleBudgetEntity.Properties.NullText = " "
        Me.INDsleBudgetEntity.Properties.PopupSizeable = False
        Me.INDsleBudgetEntity.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDsleBudgetEntity.Properties.ShowFooter = False
        Me.INDsleBudgetEntity.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleBudgetEntity, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleBudgetEntity, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleBudgetEntity, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleBudgetEntity, True)
        Me.INDsleBudgetEntity.Size = New System.Drawing.Size(386, 28)
        Me.INDsleBudgetEntity.StyleController = Me.INDlyBudgetItem
        Me.INDsleBudgetEntity.TabIndex = 4
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleBudgetEntity, "200")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleBudgetEntity, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleBudgetEntity, "{0} - {1}")
        Me.INDsleBudgetEntity.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleBudgetEntity, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleBudgetEntity, False)
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
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.INDColBINameCode})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Código"
        Me.GridColumn1.FieldName = "Code"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 239
        '
        'INDColBINameCode
        '
        Me.INDColBINameCode.Caption = "Nombre"
        Me.INDColBINameCode.FieldName = "Name"
        Me.INDColBINameCode.Name = "INDColBINameCode"
        Me.INDColBINameCode.Visible = True
        Me.INDColBINameCode.VisibleIndex = 1
        Me.INDColBINameCode.Width = 1393
        '
        'INDsleValidity
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleValidity, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleValidity, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleValidity, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleValidity, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleValidity, False)
        Me.INDsleValidity.Location = New System.Drawing.Point(24, 143)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleValidity, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleValidity.Name = "INDsleValidity"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleValidity, False)
        Me.INDsleValidity.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDsleValidity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleValidity.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleValidity.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleValidity.Properties.Appearance.Options.UseFont = True
        Me.INDsleValidity.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleValidity.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleValidity.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleValidity.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleValidity.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleValidity.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleValidity.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleValidity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleValidity.Properties.DisplayMember = "Year"
        Me.INDsleValidity.Properties.NullText = " "
        Me.INDsleValidity.Properties.PopupSizeable = False
        Me.INDsleValidity.Properties.PopupView = Me.INDgvValidity
        Me.INDsleValidity.Properties.ShowFooter = False
        Me.INDsleValidity.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleValidity, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleValidity, True)
        Me.INDsleValidity.Size = New System.Drawing.Size(386, 28)
        Me.INDsleValidity.StyleController = Me.INDlyBudgetItem
        Me.INDsleValidity.TabIndex = 12
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleValidity, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleValidity, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleValidity, "{0} - {1}")
        Me.INDsleValidity.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleValidity, False)
        '
        'INDgvValidity
        '
        Me.INDgvValidity.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvValidity.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvValidity.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvValidity.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvValidity.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvValidity.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvValidity.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvValidity.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvValidity.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvValidity.Appearance.Row.Options.UseFont = True
        Me.INDgvValidity.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColVYear, Me.INDColVStatus, Me.GridColumn2, Me.GridColumn3})
        Me.INDgvValidity.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDgvValidity.Name = "INDgvValidity"
        Me.INDgvValidity.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDgvValidity.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvValidity.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvValidity.OptionsView.ShowAutoFilterRow = True
        Me.INDgvValidity.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvValidity, False)
        '
        'INDColVYear
        '
        Me.INDColVYear.Caption = "Año"
        Me.INDColVYear.FieldName = "Year"
        Me.INDColVYear.Name = "INDColVYear"
        Me.INDColVYear.Visible = True
        Me.INDColVYear.VisibleIndex = 0
        Me.INDColVYear.Width = 245
        '
        'INDColVStatus
        '
        Me.INDColVStatus.Caption = "Estado"
        Me.INDColVStatus.FieldName = "Status"
        Me.INDColVStatus.Name = "INDColVStatus"
        Me.INDColVStatus.Visible = True
        Me.INDColVStatus.VisibleIndex = 1
        Me.INDColVStatus.Width = 299
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Resolución"
        Me.GridColumn2.FieldName = "ResolutionNumber"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 2
        Me.GridColumn2.Width = 541
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Valor"
        Me.GridColumn3.DisplayFormat.FormatString = "c0"
        Me.GridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn3.FieldName = "ResolutionValue"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 3
        Me.GridColumn3.Width = 547
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.LayoutControlGroup1.CustomizationFormText = "Rubros"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyGrItem, Me.INDlyGrListItems})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1286, 532)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlyGrItem
        '
        Me.INDlyGrItem.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGrItem.AppearanceGroup.Options.UseFont = True
        Me.INDlyGrItem.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGrItem.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyGrItem.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrItem.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyGrItem.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlyGrItem.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlyGrItem.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrItem.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyGrItem.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrItem.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlyGrItem.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrItem.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyGrItem, False)
        Me.INDlyGrItem.CustomizationFormText = "Información Rubro"
        Me.INDlyGrItem.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemBudgetEntity, Me.INDlyItemValidity})
        Me.INDlyGrItem.Location = New System.Drawing.Point(0, 0)
        Me.INDlyGrItem.Name = "INDlyGrItem"
        Me.INDlyGrItem.Size = New System.Drawing.Size(414, 512)
        Me.INDlyGrItem.Text = "Datos Principales"
        '
        'INDlyItemBudgetEntity
        '
        Me.INDlyItemBudgetEntity.AllowHide = False
        Me.INDlyItemBudgetEntity.Control = Me.INDsleBudgetEntity
        Me.INDlyItemBudgetEntity.CustomizationFormText = "Entidad Presupuestal"
        Me.INDlyItemBudgetEntity.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemBudgetEntity.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemBudgetEntity.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemBudgetEntity.Name = "INDlyItemBudgetEntity"
        Me.INDlyItemBudgetEntity.ShowInCustomizationForm = False
        Me.INDlyItemBudgetEntity.Size = New System.Drawing.Size(390, 64)
        Me.INDlyItemBudgetEntity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemBudgetEntity.Text = "Entidad Presupuestal"
        Me.INDlyItemBudgetEntity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemBudgetEntity.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemBudgetEntity.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemBudgetEntity.TextToControlDistance = 5
        '
        'INDlyItemValidity
        '
        Me.INDlyItemValidity.AllowHide = False
        Me.INDlyItemValidity.Control = Me.INDsleValidity
        Me.INDlyItemValidity.CustomizationFormText = "Vigencia"
        Me.INDlyItemValidity.Location = New System.Drawing.Point(0, 64)
        Me.INDlyItemValidity.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemValidity.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemValidity.Name = "INDlyItemValidity"
        Me.INDlyItemValidity.ShowInCustomizationForm = False
        Me.INDlyItemValidity.Size = New System.Drawing.Size(390, 395)
        Me.INDlyItemValidity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemValidity.Text = "Vigencia"
        Me.INDlyItemValidity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemValidity.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemValidity.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemValidity.TextToControlDistance = 5
        '
        'INDlyGrListItems
        '
        Me.INDlyGrListItems.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGrListItems.AppearanceGroup.Options.UseFont = True
        Me.INDlyGrListItems.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGrListItems.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyGrListItems.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrListItems.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyGrListItems.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlyGrListItems.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlyGrListItems.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrListItems.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyGrListItems.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrListItems.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlyGrListItems.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrListItems.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyGrListItems, False)
        Me.INDlyGrListItems.CustomizationFormText = "Listado De Rubros Registrados"
        Me.INDlyGrListItems.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemBudgetItems, Me.INDlyItemAddItem})
        Me.INDlyGrListItems.Location = New System.Drawing.Point(414, 0)
        Me.INDlyGrListItems.Name = "INDlyGrListItems"
        Me.INDlyGrListItems.Size = New System.Drawing.Size(852, 512)
        Me.INDlyGrListItems.Text = "Listado Rubros Registrados"
        '
        'INDlyItemBudgetItems
        '
        Me.INDlyItemBudgetItems.Control = Me.INDtlBudgetItems
        Me.INDlyItemBudgetItems.CustomizationFormText = "Rubros"
        Me.INDlyItemBudgetItems.Location = New System.Drawing.Point(0, 36)
        Me.INDlyItemBudgetItems.MinSize = New System.Drawing.Size(104, 24)
        Me.INDlyItemBudgetItems.Name = "INDlyItemBudgetItems"
        Me.INDlyItemBudgetItems.Size = New System.Drawing.Size(828, 423)
        Me.INDlyItemBudgetItems.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemBudgetItems.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemBudgetItems.TextVisible = False
        '
        'INDlyItemAddItem
        '
        Me.INDlyItemAddItem.Control = Me.INDbtnAddItem
        Me.INDlyItemAddItem.CustomizationFormText = "Agregar Rubro"
        Me.INDlyItemAddItem.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemAddItem.MaxSize = New System.Drawing.Size(828, 36)
        Me.INDlyItemAddItem.MinSize = New System.Drawing.Size(828, 36)
        Me.INDlyItemAddItem.Name = "INDlyItemAddItem"
        Me.INDlyItemAddItem.Size = New System.Drawing.Size(828, 36)
        Me.INDlyItemAddItem.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemAddItem.Text = " "
        Me.INDlyItemAddItem.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemAddItem.TextVisible = False
        '
        'BarManager1
        '
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.INDbarBtnAdd, Me.INDbarBtnEdit, Me.INDbarBtnClose})
        Me.BarManager1.MaxItemId = 3
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 5)
        Me.barDockControlTop.Manager = Me.BarManager1
        Me.barDockControlTop.Size = New System.Drawing.Size(1490, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 676)
        Me.barDockControlBottom.Manager = Me.BarManager1
        Me.barDockControlBottom.Size = New System.Drawing.Size(1490, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 5)
        Me.barDockControlLeft.Manager = Me.BarManager1
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 671)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(1490, 5)
        Me.barDockControlRight.Manager = Me.BarManager1
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 671)
        '
        'INDbarBtnAdd
        '
        Me.INDbarBtnAdd.Caption = "Agregar Rubro Hijo"
        Me.INDbarBtnAdd.Id = 0
        Me.INDbarBtnAdd.ImageOptions.Image = Global.Presentation.Budget.My.Resources.Resources.Add_16x16_blue
        Me.INDbarBtnAdd.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarBtnAdd.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDbarBtnAdd.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarBtnAdd.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDbarBtnAdd.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarBtnAdd.ItemAppearance.Normal.Options.UseFont = True
        Me.INDbarBtnAdd.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarBtnAdd.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDbarBtnAdd.ItemInMenuAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarBtnAdd.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.INDbarBtnAdd.ItemInMenuAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarBtnAdd.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.INDbarBtnAdd.ItemInMenuAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarBtnAdd.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.INDbarBtnAdd.ItemInMenuAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarBtnAdd.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.INDbarBtnAdd.Name = "INDbarBtnAdd"
        '
        'INDbarBtnEdit
        '
        Me.INDbarBtnEdit.Caption = "Modificar Rubro"
        Me.INDbarBtnEdit.Id = 1
        Me.INDbarBtnEdit.ImageOptions.Image = Global.Presentation.Budget.My.Resources.Resources.Add_16x16_blue
        Me.INDbarBtnEdit.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarBtnEdit.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDbarBtnEdit.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarBtnEdit.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDbarBtnEdit.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarBtnEdit.ItemAppearance.Normal.Options.UseFont = True
        Me.INDbarBtnEdit.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarBtnEdit.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDbarBtnEdit.ItemInMenuAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarBtnEdit.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.INDbarBtnEdit.ItemInMenuAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarBtnEdit.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.INDbarBtnEdit.ItemInMenuAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarBtnEdit.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.INDbarBtnEdit.ItemInMenuAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarBtnEdit.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.INDbarBtnEdit.Name = "INDbarBtnEdit"
        '
        'INDbarBtnClose
        '
        Me.INDbarBtnClose.Caption = "Cerrar"
        Me.INDbarBtnClose.Id = 2
        Me.INDbarBtnClose.Name = "INDbarBtnClose"
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'INDpopUpMenu
        '
        Me.INDpopUpMenu.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.INDbarBtnAdd), New DevExpress.XtraBars.LinkPersistInfo(Me.INDbarBtnEdit)})
        Me.INDpopUpMenu.Manager = Me.BarManager1
        Me.INDpopUpMenu.Name = "INDpopUpMenu"
        '
        'FrmBudgetItemsExpense
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1490, 676)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.IconOptions.Icon = CType(resources.GetObject("FrmBudgetItemsExpense.IconOptions.Icon"), System.Drawing.Icon)
        Me.IconOptions.ShowIcon = False
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmBudgetItemsExpense"
        Me.Opacity = 1.0R
        Me.Tag = "219"
        Me.Text = "Rubros - Gastos"
        Me.ViewModeEditHold = True
        Me.Controls.SetChildIndex(Me.barDockControlTop, 0)
        Me.Controls.SetChildIndex(Me.barDockControlBottom, 0)
        Me.Controls.SetChildIndex(Me.barDockControlRight, 0)
        Me.Controls.SetChildIndex(Me.barDockControlLeft, 0)
        Me.Controls.SetChildIndex(Me.ToolBars, 0)
        Me.Controls.SetChildIndex(Me.INDPanelControlBase, 0)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyBudgetItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyBudgetItem.ResumeLayout(False)
        CType(Me.INDtlBudgetItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleBudgetEntity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleValidity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvValidity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGrItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemBudgetEntity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemValidity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGrListItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemBudgetItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemAddItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpopUpMenu, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTreeList1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents INDlyBudgetItem As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents INDsleBudgetEntity As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItemBudgetEntity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents INDlyGrItem As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDlyItemValidity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents INDsleValidity As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDgvValidity As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyGrListItems As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents INDbtnAddItem As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDlyItemAddItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtlBudgetItems As DevExpress.XtraTreeList.TreeList
    Friend WithEvents INDtColCode As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents INDtColCode1 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents INDtColFinancialSource As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents INDtColName As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents INDtColAuxiliary As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents INDtColUsed As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents INDlyItemBudgetItems As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDColBINameCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColVYear As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColVStatus As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDbarBtnAdd As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDbarBtnEdit As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDbarBtnClose As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDpopUpMenu As DevExpress.XtraBars.PopupMenu
    Friend WithEvents IndigoTreeList1 As Presentation.Controls.IndigoTreeList
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
End Class
