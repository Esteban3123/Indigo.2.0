Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmBudgetCCPET
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
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlyBudgetItem = New DevExpress.XtraLayout.LayoutControl()
        Me.INDtlBudgetCCPET = New DevExpress.XtraTreeList.TreeList()
        Me.INDtColCode = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.INDtColName = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.INDtColFinancialSource = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.INDtColCode1 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.INDtColAuxiliary = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.INDbtnAddItem = New DevExpress.XtraEditors.SimpleButton()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyGrListItems = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemBudgetCCPET = New DevExpress.XtraLayout.LayoutControlItem()
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
        Me.BehaviorManager1 = New DevExpress.Utils.Behaviors.BehaviorManager(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyBudgetItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyBudgetItem.SuspendLayout()
        CType(Me.INDtlBudgetCCPET, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGrListItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemBudgetCCPET, System.ComponentModel.ISupportInitialize).BeginInit()
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
        CType(Me.BehaviorManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyBudgetItem)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1490, 554)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Location = New System.Drawing.Point(0, 24)
        Me.ToolBars.Size = New System.Drawing.Size(1490, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1490, 98)
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDlyBudgetItem
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 545)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDlyBudgetItem
        '
        Me.INDlyBudgetItem.AllowCustomization = False
        Me.INDlyBudgetItem.Controls.Add(Me.INDtlBudgetCCPET)
        Me.INDlyBudgetItem.Controls.Add(Me.INDbtnAddItem)
        Me.INDlyBudgetItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDlyBudgetItem, False)
        Me.INDlyBudgetItem.Location = New System.Drawing.Point(202, 7)
        Me.INDlyBudgetItem.Name = "INDlyBudgetItem"
        Me.INDlyBudgetItem.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(751, 342, 250, 350)
        Me.INDlyBudgetItem.Root = Me.LayoutControlGroup1
        Me.INDlyBudgetItem.Size = New System.Drawing.Size(1286, 545)
        Me.INDlyBudgetItem.TabIndex = 1
        Me.INDlyBudgetItem.Text = "LayoutControl1"
        '
        'INDtlBudgetCCPET
        '
        Me.INDtlBudgetCCPET.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDtlBudgetCCPET.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDtlBudgetCCPET.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.INDtlBudgetCCPET.Appearance.HeaderPanel.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.INDtlBudgetCCPET.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDtlBudgetCCPET.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.INDtlBudgetCCPET.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDtlBudgetCCPET.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDtlBudgetCCPET.Appearance.Row.ForeColor = System.Drawing.Color.Black
        Me.INDtlBudgetCCPET.Appearance.Row.Options.UseFont = True
        Me.INDtlBudgetCCPET.Appearance.Row.Options.UseForeColor = True
        Me.INDtlBudgetCCPET.Columns.AddRange(New DevExpress.XtraTreeList.Columns.TreeListColumn() {Me.INDtColCode, Me.INDtColName, Me.INDtColFinancialSource, Me.INDtColCode1, Me.INDtColAuxiliary})
        Me.INDtlBudgetCCPET.CustomizationFormBounds = New System.Drawing.Rectangle(1280, 536, 260, 312)
        Me.IndigoTreeList1.SetExpandedAllNodes(Me.INDtlBudgetCCPET, False)
        Me.INDtlBudgetCCPET.KeyFieldName = "Id"
        Me.INDtlBudgetCCPET.Location = New System.Drawing.Point(24, 95)
        Me.INDtlBudgetCCPET.Name = "INDtlBudgetCCPET"
        Me.INDtlBudgetCCPET.OptionsBehavior.PopulateServiceColumns = True
        Me.INDtlBudgetCCPET.OptionsFilter.FilterMode = DevExpress.XtraTreeList.FilterMode.Smart
        Me.INDtlBudgetCCPET.OptionsFind.AlwaysVisible = True
        Me.INDtlBudgetCCPET.OptionsView.EnableAppearanceEvenRow = True
        Me.INDtlBudgetCCPET.OptionsView.EnableAppearanceOddRow = True
        Me.INDtlBudgetCCPET.ParentFieldName = "CCPETOwnerId"
        Me.INDtlBudgetCCPET.Size = New System.Drawing.Size(824, 426)
        Me.INDtlBudgetCCPET.TabIndex = 15
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
        'INDtColName
        '
        Me.INDtColName.Caption = "Nombre"
        Me.INDtColName.FieldName = "Name"
        Me.INDtColName.Name = "INDtColName"
        Me.INDtColName.OptionsColumn.AllowEdit = False
        Me.INDtColName.OptionsColumn.AllowFocus = False
        Me.INDtColName.Visible = True
        Me.INDtColName.VisibleIndex = 1
        Me.INDtColName.Width = 97
        '
        'INDtColFinancialSource
        '
        Me.INDtColFinancialSource.Caption = "Rubro"
        Me.INDtColFinancialSource.FieldName = "ItemtypeName"
        Me.INDtColFinancialSource.Name = "INDtColFinancialSource"
        Me.INDtColFinancialSource.OptionsColumn.AllowEdit = False
        Me.INDtColFinancialSource.OptionsColumn.AllowFocus = False
        Me.INDtColFinancialSource.Visible = True
        Me.INDtColFinancialSource.VisibleIndex = 2
        Me.INDtColFinancialSource.Width = 95
        '
        'INDtColCode1
        '
        Me.INDtColCode1.Caption = "Tipo Cuenta"
        Me.INDtColCode1.FieldName = "AccountTypeName"
        Me.INDtColCode1.Name = "INDtColCode1"
        Me.INDtColCode1.OptionsColumn.AllowEdit = False
        Me.INDtColCode1.OptionsColumn.AllowFocus = False
        Me.INDtColCode1.Visible = True
        Me.INDtColCode1.VisibleIndex = 3
        Me.INDtColCode1.Width = 97
        '
        'INDtColAuxiliary
        '
        Me.INDtColAuxiliary.Caption = "CPC"
        Me.INDtColAuxiliary.FieldName = "LinkAccount"
        Me.INDtColAuxiliary.Name = "INDtColAuxiliary"
        Me.INDtColAuxiliary.OptionsColumn.AllowEdit = False
        Me.INDtColAuxiliary.OptionsColumn.AllowFocus = False
        Me.INDtColAuxiliary.SortOrder = System.Windows.Forms.SortOrder.Ascending
        Me.INDtColAuxiliary.Visible = True
        Me.INDtColAuxiliary.VisibleIndex = 4
        Me.INDtColAuxiliary.Width = 72
        '
        'INDbtnAddItem
        '
        Me.INDbtnAddItem.Location = New System.Drawing.Point(24, 59)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnAddItem, False)
        Me.INDbtnAddItem.Name = "INDbtnAddItem"
        Me.INDbtnAddItem.Size = New System.Drawing.Size(824, 32)
        Me.INDbtnAddItem.StyleController = Me.INDlyBudgetItem
        Me.INDbtnAddItem.TabIndex = 14
        Me.INDbtnAddItem.Text = "Agregar CCPET"
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyGrListItems})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1286, 545)
        Me.LayoutControlGroup1.TextVisible = False
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
        Me.INDlyGrListItems.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemBudgetCCPET, Me.INDlyItemAddItem})
        Me.INDlyGrListItems.Location = New System.Drawing.Point(0, 0)
        Me.INDlyGrListItems.Name = "INDlyGrListItems"
        Me.INDlyGrListItems.Size = New System.Drawing.Size(1266, 525)
        Me.INDlyGrListItems.Text = "Listado Rubros Registrados"
        '
        'INDlyItemBudgetCCPET
        '
        Me.INDlyItemBudgetCCPET.Control = Me.INDtlBudgetCCPET
        Me.INDlyItemBudgetCCPET.CustomizationFormText = "Rubros"
        Me.INDlyItemBudgetCCPET.Location = New System.Drawing.Point(0, 36)
        Me.INDlyItemBudgetCCPET.MaxSize = New System.Drawing.Size(828, 0)
        Me.INDlyItemBudgetCCPET.MinSize = New System.Drawing.Size(828, 24)
        Me.INDlyItemBudgetCCPET.Name = "INDlyItemBudgetCCPET"
        Me.INDlyItemBudgetCCPET.Size = New System.Drawing.Size(1242, 430)
        Me.INDlyItemBudgetCCPET.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemBudgetCCPET.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemBudgetCCPET.TextVisible = False
        '
        'INDlyItemAddItem
        '
        Me.INDlyItemAddItem.Control = Me.INDbtnAddItem
        Me.INDlyItemAddItem.CustomizationFormText = "Agregar Rubro"
        Me.INDlyItemAddItem.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemAddItem.MaxSize = New System.Drawing.Size(828, 36)
        Me.INDlyItemAddItem.MinSize = New System.Drawing.Size(828, 36)
        Me.INDlyItemAddItem.Name = "INDlyItemAddItem"
        Me.INDlyItemAddItem.Size = New System.Drawing.Size(1242, 36)
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
        '
        'INDpopUpMenu
        '
        Me.INDpopUpMenu.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.INDbarBtnAdd), New DevExpress.XtraBars.LinkPersistInfo(Me.INDbarBtnEdit)})
        Me.INDpopUpMenu.Manager = Me.BarManager1
        Me.INDpopUpMenu.Name = "INDpopUpMenu"
        '
        'FrmBudgetCCPET
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1490, 676)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmBudgetCCPET"
        Me.Opacity = 1.0R
        Me.Tag = "2207"
        Me.Text = "Conceptos CCPET"
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
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyBudgetItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyBudgetItem.ResumeLayout(False)
        CType(Me.INDtlBudgetCCPET, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGrListItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemBudgetCCPET, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.BehaviorManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents INDlyBudgetItem As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents INDlyGrListItems As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents INDbtnAddItem As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDlyItemAddItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtlBudgetCCPET As DevExpress.XtraTreeList.TreeList
    Friend WithEvents INDtColCode As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents INDtColCode1 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents INDtColFinancialSource As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents INDtColName As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents INDtColAuxiliary As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents INDlyItemBudgetCCPET As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDbarBtnAdd As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDbarBtnEdit As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDbarBtnClose As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDpopUpMenu As DevExpress.XtraBars.PopupMenu
    Friend WithEvents IndigoTreeList1 As Presentation.Controls.IndigoTreeList
    Friend WithEvents BehaviorManager1 As DevExpress.Utils.Behaviors.BehaviorManager
End Class
