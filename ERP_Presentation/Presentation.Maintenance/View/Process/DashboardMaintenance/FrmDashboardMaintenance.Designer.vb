Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmDashboardMaintenance
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
        Dim GridFormatRule1 As DevExpress.XtraGrid.GridFormatRule = New DevExpress.XtraGrid.GridFormatRule()
        Dim FormatConditionRuleValue1 As DevExpress.XtraEditors.FormatConditionRuleValue = New DevExpress.XtraEditors.FormatConditionRuleValue()
        Dim GridFormatRule2 As DevExpress.XtraGrid.GridFormatRule = New DevExpress.XtraGrid.GridFormatRule()
        Dim FormatConditionRuleValue2 As DevExpress.XtraEditors.FormatConditionRuleValue = New DevExpress.XtraEditors.FormatConditionRuleValue()
        Dim GridFormatRule3 As DevExpress.XtraGrid.GridFormatRule = New DevExpress.XtraGrid.GridFormatRule()
        Dim FormatConditionRuleValue3 As DevExpress.XtraEditors.FormatConditionRuleValue = New DevExpress.XtraEditors.FormatConditionRuleValue()
        Dim GridFormatRule4 As DevExpress.XtraGrid.GridFormatRule = New DevExpress.XtraGrid.GridFormatRule()
        Dim FormatConditionRuleValue4 As DevExpress.XtraEditors.FormatConditionRuleValue = New DevExpress.XtraEditors.FormatConditionRuleValue()
        Dim GridFormatRule5 As DevExpress.XtraGrid.GridFormatRule = New DevExpress.XtraGrid.GridFormatRule()
        Dim FormatConditionRuleValue5 As DevExpress.XtraEditors.FormatConditionRuleValue = New DevExpress.XtraEditors.FormatConditionRuleValue()
        Dim GridFormatRule6 As DevExpress.XtraGrid.GridFormatRule = New DevExpress.XtraGrid.GridFormatRule()
        Dim FormatConditionRuleValue6 As DevExpress.XtraEditors.FormatConditionRuleValue = New DevExpress.XtraEditors.FormatConditionRuleValue()
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.BarDockControl1 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl2 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl3 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl4 = New DevExpress.XtraBars.BarDockControl()
        Me.INDBarRefresh = New DevExpress.XtraBars.BarButtonItem()
        Me.INDddbOptionsMenu = New DevExpress.XtraEditors.DropDownButton()
        Me.INDPopMenuActions = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.INDlyRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDgcRequestsWithWorkOrder = New DevExpress.XtraGrid.GridControl()
        Me.INDviewRequestsWithWorkOrder = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDviewRequestsWithWorkOrder_ItemCatalog = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDviewRequestsWithWorkOrder_SelectOption = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDviewRequestsWithWorkOrder_Code = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDviewRequestsWithWorkOrder_DateFailure = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDviewRequestsWithWorkOrder_Plate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDviewRequestsWithWorkOrder_Item = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDviewRequestsWithWorkOrder_Location = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDviewRequestsWithWorkOrder_Part = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDviewRequestsWithWorkOrder_RequestUser = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDviewRequestsWithWorkOrder_AssignUser = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRepCbeRadicatedAlert = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.INDgcRequests = New DevExpress.XtraGrid.GridControl()
        Me.INDviewRequests = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDviewRequests_ItemCatalog = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDviewRequests_SelectOption = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDviewRequests_Code = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDviewRequests_DateFailure = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDviewRequests_Plate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDviewRequests_Item = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDviewRequests_Location = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDviewRequests_Part = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDviewRequests_RequestUser = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRepCheckSelectOption = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.INDSleItemCatalog = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDviewItemCatalog = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColItemCatalogCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColItemCatalogDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemOptionsMenu = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemCareCenter = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDtcgInformation = New DevExpress.XtraLayout.TabbedControlGroup()
        Me.INDlycgRequests = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemRequests = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlycgRequestsWithWorkOrder = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemRequestsWithWorkOrder = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.BarManager2 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.BarDockControl5 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl6 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl7 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl8 = New DevExpress.XtraBars.BarDockControl()
        Me.INDBbiAsign = New DevExpress.XtraBars.BarButtonItem()
        Me.INDPopMenuActions2 = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPopMenuActions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyRoot.SuspendLayout()
        CType(Me.INDgcRequestsWithWorkOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewRequestsWithWorkOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRepCbeRadicatedAlert, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcRequests, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewRequests, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRepCheckSelectOption, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleItemCatalog.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewItemCatalog, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemOptionsMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCareCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtcgInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlycgRequests, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemRequests, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlycgRequestsWithWorkOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemRequestsWithWorkOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPopMenuActions2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyRoot)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1370, 604)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1370, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(1370, 130)
        '
        'BarManager1
        '
        Me.BarManager1.DockControls.Add(Me.BarDockControl1)
        Me.BarManager1.DockControls.Add(Me.BarDockControl2)
        Me.BarManager1.DockControls.Add(Me.BarDockControl3)
        Me.BarManager1.DockControls.Add(Me.BarDockControl4)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.INDBarRefresh})
        Me.BarManager1.MaxItemId = 8
        '
        'BarDockControl1
        '
        Me.BarDockControl1.CausesValidation = False
        Me.BarDockControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.BarDockControl1.Location = New System.Drawing.Point(0, 5)
        Me.BarDockControl1.Manager = Me.BarManager1
        Me.BarDockControl1.Size = New System.Drawing.Size(1370, 0)
        '
        'BarDockControl2
        '
        Me.BarDockControl2.CausesValidation = False
        Me.BarDockControl2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.BarDockControl2.Location = New System.Drawing.Point(0, 739)
        Me.BarDockControl2.Manager = Me.BarManager1
        Me.BarDockControl2.Size = New System.Drawing.Size(1370, 0)
        '
        'BarDockControl3
        '
        Me.BarDockControl3.CausesValidation = False
        Me.BarDockControl3.Dock = System.Windows.Forms.DockStyle.Left
        Me.BarDockControl3.Location = New System.Drawing.Point(0, 5)
        Me.BarDockControl3.Manager = Me.BarManager1
        Me.BarDockControl3.Size = New System.Drawing.Size(0, 734)
        '
        'BarDockControl4
        '
        Me.BarDockControl4.CausesValidation = False
        Me.BarDockControl4.Dock = System.Windows.Forms.DockStyle.Right
        Me.BarDockControl4.Location = New System.Drawing.Point(1370, 5)
        Me.BarDockControl4.Manager = Me.BarManager1
        Me.BarDockControl4.Size = New System.Drawing.Size(0, 734)
        '
        'INDBarRefresh
        '
        Me.INDBarRefresh.Caption = "Refrescar"
        Me.INDBarRefresh.Id = 5
        Me.INDBarRefresh.ImageOptions.Image = Global.Presentation.Maintenance.My.Resources.Resources.Refresh_16x16_blue
        Me.INDBarRefresh.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBarRefresh.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDBarRefresh.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBarRefresh.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDBarRefresh.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBarRefresh.ItemAppearance.Normal.Options.UseFont = True
        Me.INDBarRefresh.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBarRefresh.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDBarRefresh.Name = "INDBarRefresh"
        '
        'INDddbOptionsMenu
        '
        Me.INDddbOptionsMenu.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.INDddbOptionsMenu.DropDownArrowStyle = DevExpress.XtraEditors.DropDownArrowStyle.Hide
        Me.INDddbOptionsMenu.DropDownControl = Me.INDPopMenuActions
        Me.INDddbOptionsMenu.ImageOptions.Image = Global.Presentation.Maintenance.My.Resources.Resources.Mmenu_de_acciones
        Me.INDddbOptionsMenu.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.INDddbOptionsMenu.Location = New System.Drawing.Point(1294, 12)
        Me.INDddbOptionsMenu.MenuManager = Me.BarManager1
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDddbOptionsMenu, False)
        Me.INDddbOptionsMenu.Name = "INDddbOptionsMenu"
        Me.INDddbOptionsMenu.Size = New System.Drawing.Size(60, 60)
        Me.INDddbOptionsMenu.StyleController = Me.INDlyRoot
        Me.INDddbOptionsMenu.TabIndex = 9
        '
        'INDPopMenuActions
        '
        Me.INDPopMenuActions.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.INDBarRefresh)})
        Me.INDPopMenuActions.Manager = Me.BarManager1
        Me.INDPopMenuActions.Name = "INDPopMenuActions"
        '
        'INDlyRoot
        '
        Me.INDlyRoot.Controls.Add(Me.INDgcRequestsWithWorkOrder)
        Me.INDlyRoot.Controls.Add(Me.INDgcRequests)
        Me.INDlyRoot.Controls.Add(Me.INDSleItemCatalog)
        Me.INDlyRoot.Controls.Add(Me.INDddbOptionsMenu)
        Me.INDlyRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyRoot.Location = New System.Drawing.Point(2, 7)
        Me.INDlyRoot.Name = "INDlyRoot"
        Me.INDlyRoot.Root = Me.Root
        Me.INDlyRoot.Size = New System.Drawing.Size(1366, 595)
        Me.INDlyRoot.TabIndex = 0
        Me.INDlyRoot.Text = "LayoutControl1"
        '
        'INDgcRequestsWithWorkOrder
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcRequestsWithWorkOrder, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcRequestsWithWorkOrder, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcRequestsWithWorkOrder, True)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcRequestsWithWorkOrder, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcRequestsWithWorkOrder, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcRequestsWithWorkOrder, False)
        Me.INDgcRequestsWithWorkOrder.Location = New System.Drawing.Point(22, 117)
        Me.INDgcRequestsWithWorkOrder.MainView = Me.INDviewRequestsWithWorkOrder
        Me.INDgcRequestsWithWorkOrder.MenuManager = Me.BarManager1
        Me.INDgcRequestsWithWorkOrder.Name = "INDgcRequestsWithWorkOrder"
        Me.INDgcRequestsWithWorkOrder.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDRepCbeRadicatedAlert})
        Me.INDgcRequestsWithWorkOrder.Size = New System.Drawing.Size(1322, 456)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcRequestsWithWorkOrder, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcRequestsWithWorkOrder.TabIndex = 12
        Me.INDgcRequestsWithWorkOrder.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDviewRequestsWithWorkOrder})
        '
        'INDviewRequestsWithWorkOrder
        '
        Me.INDviewRequestsWithWorkOrder.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewRequestsWithWorkOrder.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewRequestsWithWorkOrder.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewRequestsWithWorkOrder.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewRequestsWithWorkOrder.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewRequestsWithWorkOrder.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewRequestsWithWorkOrder.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewRequestsWithWorkOrder.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewRequestsWithWorkOrder.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewRequestsWithWorkOrder.Appearance.Row.Options.UseFont = True
        Me.INDviewRequestsWithWorkOrder.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDviewRequestsWithWorkOrder.Appearance.ViewCaption.Options.UseFont = True
        Me.INDviewRequestsWithWorkOrder.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDviewRequestsWithWorkOrder_ItemCatalog, Me.INDviewRequestsWithWorkOrder_SelectOption, Me.INDviewRequestsWithWorkOrder_Code, Me.INDviewRequestsWithWorkOrder_DateFailure, Me.INDviewRequestsWithWorkOrder_Plate, Me.INDviewRequestsWithWorkOrder_Item, Me.INDviewRequestsWithWorkOrder_Location, Me.INDviewRequestsWithWorkOrder_Part, Me.INDviewRequestsWithWorkOrder_RequestUser, Me.INDviewRequestsWithWorkOrder_AssignUser})
        GridFormatRule1.Name = "Format0"
        FormatConditionRuleValue1.Appearance.BackColor = System.Drawing.Color.Red
        FormatConditionRuleValue1.Appearance.BackColor2 = System.Drawing.Color.White
        FormatConditionRuleValue1.Appearance.ForeColor = System.Drawing.Color.Red
        FormatConditionRuleValue1.Appearance.Options.UseBackColor = True
        FormatConditionRuleValue1.Appearance.Options.UseForeColor = True
        FormatConditionRuleValue1.Appearance.Options.UseTextOptions = True
        FormatConditionRuleValue1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        FormatConditionRuleValue1.Condition = DevExpress.XtraEditors.FormatCondition.Equal
        FormatConditionRuleValue1.Expression = "[ColorRadicated] = 3"
        FormatConditionRuleValue1.Value1 = 3
        GridFormatRule1.Rule = FormatConditionRuleValue1
        GridFormatRule2.Name = "Format1"
        FormatConditionRuleValue2.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        FormatConditionRuleValue2.Appearance.BackColor2 = System.Drawing.Color.White
        FormatConditionRuleValue2.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        FormatConditionRuleValue2.Appearance.Options.UseBackColor = True
        FormatConditionRuleValue2.Appearance.Options.UseForeColor = True
        FormatConditionRuleValue2.Appearance.Options.UseTextOptions = True
        FormatConditionRuleValue2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        FormatConditionRuleValue2.Condition = DevExpress.XtraEditors.FormatCondition.Equal
        FormatConditionRuleValue2.Expression = "[ColorRadicated] = 2"
        FormatConditionRuleValue2.Value1 = 2
        GridFormatRule2.Rule = FormatConditionRuleValue2
        GridFormatRule3.Name = "Format2"
        FormatConditionRuleValue3.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        FormatConditionRuleValue3.Appearance.BackColor2 = System.Drawing.Color.White
        FormatConditionRuleValue3.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        FormatConditionRuleValue3.Appearance.Options.UseBackColor = True
        FormatConditionRuleValue3.Appearance.Options.UseForeColor = True
        FormatConditionRuleValue3.Appearance.Options.UseTextOptions = True
        FormatConditionRuleValue3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        FormatConditionRuleValue3.Condition = DevExpress.XtraEditors.FormatCondition.Equal
        FormatConditionRuleValue3.Expression = "[ColorRadicated] = 1"
        FormatConditionRuleValue3.Value1 = 1
        GridFormatRule3.Rule = FormatConditionRuleValue3
        Me.INDviewRequestsWithWorkOrder.FormatRules.Add(GridFormatRule1)
        Me.INDviewRequestsWithWorkOrder.FormatRules.Add(GridFormatRule2)
        Me.INDviewRequestsWithWorkOrder.FormatRules.Add(GridFormatRule3)
        Me.INDviewRequestsWithWorkOrder.GridControl = Me.INDgcRequestsWithWorkOrder
        Me.INDviewRequestsWithWorkOrder.GroupCount = 1
        Me.INDviewRequestsWithWorkOrder.Name = "INDviewRequestsWithWorkOrder"
        Me.INDviewRequestsWithWorkOrder.OptionsBehavior.AutoExpandAllGroups = True
        Me.INDviewRequestsWithWorkOrder.OptionsSelection.CheckBoxSelectorColumnWidth = 30
        Me.INDviewRequestsWithWorkOrder.OptionsSelection.MultiSelect = True
        Me.INDviewRequestsWithWorkOrder.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewRequestsWithWorkOrder.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewRequestsWithWorkOrder.OptionsView.ShowAutoFilterRow = True
        Me.INDviewRequestsWithWorkOrder.OptionsView.ShowDetailButtons = False
        Me.INDviewRequestsWithWorkOrder.OptionsView.ShowFooter = True
        Me.INDviewRequestsWithWorkOrder.OptionsView.ShowGroupPanel = False
        Me.INDviewRequestsWithWorkOrder.OptionsView.ShowIndicator = False
        Me.INDviewRequestsWithWorkOrder.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.INDviewRequestsWithWorkOrder_ItemCatalog, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewRequestsWithWorkOrder, False)
        '
        'INDviewRequestsWithWorkOrder_ItemCatalog
        '
        Me.INDviewRequestsWithWorkOrder_ItemCatalog.Caption = "Catálogo"
        Me.INDviewRequestsWithWorkOrder_ItemCatalog.FieldName = "ItemCatalogCodeDescription"
        Me.INDviewRequestsWithWorkOrder_ItemCatalog.Name = "INDviewRequestsWithWorkOrder_ItemCatalog"
        Me.INDviewRequestsWithWorkOrder_ItemCatalog.OptionsColumn.AllowEdit = False
        Me.INDviewRequestsWithWorkOrder_ItemCatalog.OptionsColumn.AllowFocus = False
        Me.INDviewRequestsWithWorkOrder_ItemCatalog.Visible = True
        Me.INDviewRequestsWithWorkOrder_ItemCatalog.VisibleIndex = 1
        '
        'INDviewRequestsWithWorkOrder_SelectOption
        '
        Me.INDviewRequestsWithWorkOrder_SelectOption.Caption = " "
        Me.INDviewRequestsWithWorkOrder_SelectOption.FieldName = "SelectOption"
        Me.INDviewRequestsWithWorkOrder_SelectOption.ImageOptions.Alignment = System.Drawing.StringAlignment.Center
        Me.INDviewRequestsWithWorkOrder_SelectOption.ImageOptions.Image = Global.Presentation.Maintenance.My.Resources.Resources.undcheck
        Me.INDviewRequestsWithWorkOrder_SelectOption.Name = "INDviewRequestsWithWorkOrder_SelectOption"
        Me.INDviewRequestsWithWorkOrder_SelectOption.OptionsColumn.AllowEdit = False
        Me.INDviewRequestsWithWorkOrder_SelectOption.OptionsColumn.AllowFocus = False
        Me.INDviewRequestsWithWorkOrder_SelectOption.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDviewRequestsWithWorkOrder_SelectOption.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDviewRequestsWithWorkOrder_SelectOption.OptionsColumn.AllowMove = False
        Me.INDviewRequestsWithWorkOrder_SelectOption.OptionsColumn.AllowShowHide = False
        Me.INDviewRequestsWithWorkOrder_SelectOption.OptionsColumn.AllowSize = False
        Me.INDviewRequestsWithWorkOrder_SelectOption.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDviewRequestsWithWorkOrder_SelectOption.OptionsFilter.AllowAutoFilter = False
        Me.INDviewRequestsWithWorkOrder_SelectOption.OptionsFilter.AllowFilter = False
        Me.INDviewRequestsWithWorkOrder_SelectOption.Visible = True
        Me.INDviewRequestsWithWorkOrder_SelectOption.VisibleIndex = 0
        Me.INDviewRequestsWithWorkOrder_SelectOption.Width = 40
        '
        'INDviewRequestsWithWorkOrder_Code
        '
        Me.INDviewRequestsWithWorkOrder_Code.Caption = "Solicitud"
        Me.INDviewRequestsWithWorkOrder_Code.FieldName = "Code"
        Me.INDviewRequestsWithWorkOrder_Code.Name = "INDviewRequestsWithWorkOrder_Code"
        Me.INDviewRequestsWithWorkOrder_Code.OptionsColumn.AllowEdit = False
        Me.INDviewRequestsWithWorkOrder_Code.OptionsColumn.AllowFocus = False
        Me.INDviewRequestsWithWorkOrder_Code.Visible = True
        Me.INDviewRequestsWithWorkOrder_Code.VisibleIndex = 1
        Me.INDviewRequestsWithWorkOrder_Code.Width = 120
        '
        'INDviewRequestsWithWorkOrder_DateFailure
        '
        Me.INDviewRequestsWithWorkOrder_DateFailure.Caption = "Fecha"
        Me.INDviewRequestsWithWorkOrder_DateFailure.FieldName = "DateFailure"
        Me.INDviewRequestsWithWorkOrder_DateFailure.Name = "INDviewRequestsWithWorkOrder_DateFailure"
        Me.INDviewRequestsWithWorkOrder_DateFailure.OptionsColumn.AllowEdit = False
        Me.INDviewRequestsWithWorkOrder_DateFailure.OptionsColumn.AllowFocus = False
        Me.INDviewRequestsWithWorkOrder_DateFailure.Visible = True
        Me.INDviewRequestsWithWorkOrder_DateFailure.VisibleIndex = 2
        Me.INDviewRequestsWithWorkOrder_DateFailure.Width = 100
        '
        'INDviewRequestsWithWorkOrder_Plate
        '
        Me.INDviewRequestsWithWorkOrder_Plate.Caption = "Placa"
        Me.INDviewRequestsWithWorkOrder_Plate.FieldName = "Plate"
        Me.INDviewRequestsWithWorkOrder_Plate.Name = "INDviewRequestsWithWorkOrder_Plate"
        Me.INDviewRequestsWithWorkOrder_Plate.OptionsColumn.AllowEdit = False
        Me.INDviewRequestsWithWorkOrder_Plate.OptionsColumn.AllowFocus = False
        Me.INDviewRequestsWithWorkOrder_Plate.Visible = True
        Me.INDviewRequestsWithWorkOrder_Plate.VisibleIndex = 3
        Me.INDviewRequestsWithWorkOrder_Plate.Width = 100
        '
        'INDviewRequestsWithWorkOrder_Item
        '
        Me.INDviewRequestsWithWorkOrder_Item.Caption = "Artículo"
        Me.INDviewRequestsWithWorkOrder_Item.FieldName = "ItemCodeDescription"
        Me.INDviewRequestsWithWorkOrder_Item.Name = "INDviewRequestsWithWorkOrder_Item"
        Me.INDviewRequestsWithWorkOrder_Item.OptionsColumn.AllowEdit = False
        Me.INDviewRequestsWithWorkOrder_Item.OptionsColumn.AllowFocus = False
        Me.INDviewRequestsWithWorkOrder_Item.Visible = True
        Me.INDviewRequestsWithWorkOrder_Item.VisibleIndex = 4
        Me.INDviewRequestsWithWorkOrder_Item.Width = 240
        '
        'INDviewRequestsWithWorkOrder_Location
        '
        Me.INDviewRequestsWithWorkOrder_Location.Caption = "Ubicación"
        Me.INDviewRequestsWithWorkOrder_Location.FieldName = "LocationCodeName"
        Me.INDviewRequestsWithWorkOrder_Location.Name = "INDviewRequestsWithWorkOrder_Location"
        Me.INDviewRequestsWithWorkOrder_Location.OptionsColumn.AllowEdit = False
        Me.INDviewRequestsWithWorkOrder_Location.OptionsColumn.AllowFocus = False
        Me.INDviewRequestsWithWorkOrder_Location.Visible = True
        Me.INDviewRequestsWithWorkOrder_Location.VisibleIndex = 5
        Me.INDviewRequestsWithWorkOrder_Location.Width = 240
        '
        'INDviewRequestsWithWorkOrder_Part
        '
        Me.INDviewRequestsWithWorkOrder_Part.Caption = "Parte"
        Me.INDviewRequestsWithWorkOrder_Part.FieldName = "PartCodeDescription"
        Me.INDviewRequestsWithWorkOrder_Part.Name = "INDviewRequestsWithWorkOrder_Part"
        Me.INDviewRequestsWithWorkOrder_Part.OptionsColumn.AllowEdit = False
        Me.INDviewRequestsWithWorkOrder_Part.OptionsColumn.AllowFocus = False
        Me.INDviewRequestsWithWorkOrder_Part.Visible = True
        Me.INDviewRequestsWithWorkOrder_Part.VisibleIndex = 6
        Me.INDviewRequestsWithWorkOrder_Part.Width = 240
        '
        'INDviewRequestsWithWorkOrder_RequestUser
        '
        Me.INDviewRequestsWithWorkOrder_RequestUser.Caption = "Usuario Solicitud"
        Me.INDviewRequestsWithWorkOrder_RequestUser.FieldName = "RequestUser"
        Me.INDviewRequestsWithWorkOrder_RequestUser.Name = "INDviewRequestsWithWorkOrder_RequestUser"
        Me.INDviewRequestsWithWorkOrder_RequestUser.OptionsColumn.AllowEdit = False
        Me.INDviewRequestsWithWorkOrder_RequestUser.OptionsColumn.AllowFocus = False
        Me.INDviewRequestsWithWorkOrder_RequestUser.Visible = True
        Me.INDviewRequestsWithWorkOrder_RequestUser.VisibleIndex = 7
        Me.INDviewRequestsWithWorkOrder_RequestUser.Width = 120
        '
        'INDviewRequestsWithWorkOrder_AssignUser
        '
        Me.INDviewRequestsWithWorkOrder_AssignUser.Caption = "Usuario Asignado"
        Me.INDviewRequestsWithWorkOrder_AssignUser.FieldName = "AssignUser"
        Me.INDviewRequestsWithWorkOrder_AssignUser.Name = "INDviewRequestsWithWorkOrder_AssignUser"
        Me.INDviewRequestsWithWorkOrder_AssignUser.OptionsColumn.AllowEdit = False
        Me.INDviewRequestsWithWorkOrder_AssignUser.OptionsColumn.AllowFocus = False
        Me.INDviewRequestsWithWorkOrder_AssignUser.Visible = True
        Me.INDviewRequestsWithWorkOrder_AssignUser.VisibleIndex = 8
        Me.INDviewRequestsWithWorkOrder_AssignUser.Width = 120
        '
        'INDRepCbeRadicatedAlert
        '
        Me.INDRepCbeRadicatedAlert.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDRepCbeRadicatedAlert.AutoHeight = False
        Me.INDRepCbeRadicatedAlert.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDRepCbeRadicatedAlert.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDRepCbeRadicatedAlert.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Normal", False, 6), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Alerta", True, 0)})
        Me.INDRepCbeRadicatedAlert.Name = "INDRepCbeRadicatedAlert"
        Me.INDRepCbeRadicatedAlert.ReadOnly = True
        '
        'INDgcRequests
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcRequests, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcRequests, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcRequests, True)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcRequests, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcRequests, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcRequests, False)
        Me.INDgcRequests.Location = New System.Drawing.Point(22, 117)
        Me.INDgcRequests.MainView = Me.INDviewRequests
        Me.INDgcRequests.MenuManager = Me.BarManager1
        Me.INDgcRequests.Name = "INDgcRequests"
        Me.INDgcRequests.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDRepCheckSelectOption})
        Me.INDgcRequests.Size = New System.Drawing.Size(1322, 456)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcRequests, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcRequests.TabIndex = 11
        Me.INDgcRequests.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDviewRequests})
        '
        'INDviewRequests
        '
        Me.INDviewRequests.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewRequests.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewRequests.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewRequests.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewRequests.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewRequests.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewRequests.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewRequests.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewRequests.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewRequests.Appearance.Row.Options.UseFont = True
        Me.INDviewRequests.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDviewRequests.Appearance.ViewCaption.Options.UseFont = True
        Me.INDviewRequests.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDviewRequests_ItemCatalog, Me.INDviewRequests_SelectOption, Me.INDviewRequests_Code, Me.INDviewRequests_DateFailure, Me.INDviewRequests_Plate, Me.INDviewRequests_Item, Me.INDviewRequests_Location, Me.INDviewRequests_Part, Me.INDviewRequests_RequestUser})
        GridFormatRule4.Name = "Format0"
        FormatConditionRuleValue4.Appearance.BackColor = System.Drawing.Color.Red
        FormatConditionRuleValue4.Appearance.BackColor2 = System.Drawing.Color.White
        FormatConditionRuleValue4.Appearance.ForeColor = System.Drawing.Color.Red
        FormatConditionRuleValue4.Appearance.Options.UseBackColor = True
        FormatConditionRuleValue4.Appearance.Options.UseForeColor = True
        FormatConditionRuleValue4.Appearance.Options.UseTextOptions = True
        FormatConditionRuleValue4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        FormatConditionRuleValue4.Condition = DevExpress.XtraEditors.FormatCondition.Equal
        FormatConditionRuleValue4.Expression = "[ColorRequest] = 3"
        FormatConditionRuleValue4.Value1 = 3
        GridFormatRule4.Rule = FormatConditionRuleValue4
        GridFormatRule5.Name = "Format1"
        FormatConditionRuleValue5.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        FormatConditionRuleValue5.Appearance.BackColor2 = System.Drawing.Color.White
        FormatConditionRuleValue5.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        FormatConditionRuleValue5.Appearance.Options.UseBackColor = True
        FormatConditionRuleValue5.Appearance.Options.UseForeColor = True
        FormatConditionRuleValue5.Appearance.Options.UseTextOptions = True
        FormatConditionRuleValue5.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        FormatConditionRuleValue5.Condition = DevExpress.XtraEditors.FormatCondition.Equal
        FormatConditionRuleValue5.Expression = "[ColorRequest] = 2"
        FormatConditionRuleValue5.Value1 = 2
        GridFormatRule5.Rule = FormatConditionRuleValue5
        GridFormatRule6.Name = "Format2"
        FormatConditionRuleValue6.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        FormatConditionRuleValue6.Appearance.BackColor2 = System.Drawing.Color.White
        FormatConditionRuleValue6.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        FormatConditionRuleValue6.Appearance.Options.UseBackColor = True
        FormatConditionRuleValue6.Appearance.Options.UseForeColor = True
        FormatConditionRuleValue6.Appearance.Options.UseTextOptions = True
        FormatConditionRuleValue6.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        FormatConditionRuleValue6.Condition = DevExpress.XtraEditors.FormatCondition.Equal
        FormatConditionRuleValue6.Expression = "[ColorRequest] = 1"
        FormatConditionRuleValue6.Value1 = 1
        GridFormatRule6.Rule = FormatConditionRuleValue6
        Me.INDviewRequests.FormatRules.Add(GridFormatRule4)
        Me.INDviewRequests.FormatRules.Add(GridFormatRule5)
        Me.INDviewRequests.FormatRules.Add(GridFormatRule6)
        Me.INDviewRequests.GridControl = Me.INDgcRequests
        Me.INDviewRequests.GroupCount = 1
        Me.INDviewRequests.Name = "INDviewRequests"
        Me.INDviewRequests.OptionsBehavior.AutoExpandAllGroups = True
        Me.INDviewRequests.OptionsSelection.CheckBoxSelectorColumnWidth = 30
        Me.INDviewRequests.OptionsSelection.MultiSelect = True
        Me.INDviewRequests.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewRequests.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewRequests.OptionsView.ShowAutoFilterRow = True
        Me.INDviewRequests.OptionsView.ShowDetailButtons = False
        Me.INDviewRequests.OptionsView.ShowFooter = True
        Me.INDviewRequests.OptionsView.ShowGroupPanel = False
        Me.INDviewRequests.OptionsView.ShowIndicator = False
        Me.INDviewRequests.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.INDviewRequests_ItemCatalog, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewRequests, False)
        '
        'INDviewRequests_ItemCatalog
        '
        Me.INDviewRequests_ItemCatalog.Caption = "Catálogo"
        Me.INDviewRequests_ItemCatalog.FieldName = "ItemCatalogCodeDescription"
        Me.INDviewRequests_ItemCatalog.Name = "INDviewRequests_ItemCatalog"
        Me.INDviewRequests_ItemCatalog.OptionsColumn.AllowEdit = False
        Me.INDviewRequests_ItemCatalog.OptionsColumn.AllowFocus = False
        Me.INDviewRequests_ItemCatalog.Visible = True
        Me.INDviewRequests_ItemCatalog.VisibleIndex = 0
        Me.INDviewRequests_ItemCatalog.Width = 169
        '
        'INDviewRequests_SelectOption
        '
        Me.INDviewRequests_SelectOption.Caption = " "
        Me.INDviewRequests_SelectOption.FieldName = "SelectOption"
        Me.INDviewRequests_SelectOption.ImageOptions.Alignment = System.Drawing.StringAlignment.Center
        Me.INDviewRequests_SelectOption.ImageOptions.Image = Global.Presentation.Maintenance.My.Resources.Resources.undcheck
        Me.INDviewRequests_SelectOption.Name = "INDviewRequests_SelectOption"
        Me.INDviewRequests_SelectOption.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDviewRequests_SelectOption.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDviewRequests_SelectOption.OptionsColumn.AllowMove = False
        Me.INDviewRequests_SelectOption.OptionsColumn.AllowShowHide = False
        Me.INDviewRequests_SelectOption.OptionsColumn.AllowSize = False
        Me.INDviewRequests_SelectOption.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDviewRequests_SelectOption.OptionsFilter.AllowAutoFilter = False
        Me.INDviewRequests_SelectOption.OptionsFilter.AllowFilter = False
        Me.INDviewRequests_SelectOption.Visible = True
        Me.INDviewRequests_SelectOption.VisibleIndex = 0
        Me.INDviewRequests_SelectOption.Width = 40
        '
        'INDviewRequests_Code
        '
        Me.INDviewRequests_Code.Caption = "Solicitud"
        Me.INDviewRequests_Code.FieldName = "Code"
        Me.INDviewRequests_Code.Name = "INDviewRequests_Code"
        Me.INDviewRequests_Code.OptionsColumn.AllowEdit = False
        Me.INDviewRequests_Code.OptionsColumn.AllowFocus = False
        Me.INDviewRequests_Code.Visible = True
        Me.INDviewRequests_Code.VisibleIndex = 1
        Me.INDviewRequests_Code.Width = 120
        '
        'INDviewRequests_DateFailure
        '
        Me.INDviewRequests_DateFailure.Caption = "Fecha"
        Me.INDviewRequests_DateFailure.FieldName = "DateFailure"
        Me.INDviewRequests_DateFailure.Name = "INDviewRequests_DateFailure"
        Me.INDviewRequests_DateFailure.OptionsColumn.AllowEdit = False
        Me.INDviewRequests_DateFailure.OptionsColumn.AllowFocus = False
        Me.INDviewRequests_DateFailure.Visible = True
        Me.INDviewRequests_DateFailure.VisibleIndex = 2
        Me.INDviewRequests_DateFailure.Width = 100
        '
        'INDviewRequests_Plate
        '
        Me.INDviewRequests_Plate.Caption = "Placa"
        Me.INDviewRequests_Plate.FieldName = "Plate"
        Me.INDviewRequests_Plate.Name = "INDviewRequests_Plate"
        Me.INDviewRequests_Plate.OptionsColumn.AllowEdit = False
        Me.INDviewRequests_Plate.OptionsColumn.AllowFocus = False
        Me.INDviewRequests_Plate.Visible = True
        Me.INDviewRequests_Plate.VisibleIndex = 3
        Me.INDviewRequests_Plate.Width = 100
        '
        'INDviewRequests_Item
        '
        Me.INDviewRequests_Item.Caption = "Artículo"
        Me.INDviewRequests_Item.FieldName = "ItemCodeDescription"
        Me.INDviewRequests_Item.Name = "INDviewRequests_Item"
        Me.INDviewRequests_Item.OptionsColumn.AllowEdit = False
        Me.INDviewRequests_Item.OptionsColumn.AllowFocus = False
        Me.INDviewRequests_Item.Visible = True
        Me.INDviewRequests_Item.VisibleIndex = 4
        Me.INDviewRequests_Item.Width = 240
        '
        'INDviewRequests_Location
        '
        Me.INDviewRequests_Location.Caption = "Ubicación"
        Me.INDviewRequests_Location.FieldName = "LocationCodeName"
        Me.INDviewRequests_Location.Name = "INDviewRequests_Location"
        Me.INDviewRequests_Location.OptionsColumn.AllowEdit = False
        Me.INDviewRequests_Location.OptionsColumn.AllowFocus = False
        Me.INDviewRequests_Location.Visible = True
        Me.INDviewRequests_Location.VisibleIndex = 5
        Me.INDviewRequests_Location.Width = 240
        '
        'INDviewRequests_Part
        '
        Me.INDviewRequests_Part.Caption = "Parte"
        Me.INDviewRequests_Part.FieldName = "PartCodeDescription"
        Me.INDviewRequests_Part.Name = "INDviewRequests_Part"
        Me.INDviewRequests_Part.OptionsColumn.AllowEdit = False
        Me.INDviewRequests_Part.OptionsColumn.AllowFocus = False
        Me.INDviewRequests_Part.Visible = True
        Me.INDviewRequests_Part.VisibleIndex = 6
        Me.INDviewRequests_Part.Width = 240
        '
        'INDviewRequests_RequestUser
        '
        Me.INDviewRequests_RequestUser.Caption = "Usuario Solicitud"
        Me.INDviewRequests_RequestUser.FieldName = "RequestUser"
        Me.INDviewRequests_RequestUser.Name = "INDviewRequests_RequestUser"
        Me.INDviewRequests_RequestUser.OptionsColumn.AllowEdit = False
        Me.INDviewRequests_RequestUser.OptionsColumn.AllowFocus = False
        Me.INDviewRequests_RequestUser.Visible = True
        Me.INDviewRequests_RequestUser.VisibleIndex = 7
        Me.INDviewRequests_RequestUser.Width = 120
        '
        'INDRepCheckSelectOption
        '
        Me.INDRepCheckSelectOption.AutoHeight = False
        Me.INDRepCheckSelectOption.Name = "INDRepCheckSelectOption"
        '
        'INDSleItemCatalog
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleItemCatalog, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleItemCatalog, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleItemCatalog, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleItemCatalog, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleItemCatalog, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleItemCatalog, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleItemCatalog, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleItemCatalog, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleItemCatalog, False)
        Me.INDSleItemCatalog.Location = New System.Drawing.Point(12, 12)
        Me.INDSleItemCatalog.MaximumSize = New System.Drawing.Size(0, 60)
        Me.INDSleItemCatalog.MinimumSize = New System.Drawing.Size(0, 60)
        Me.INDSleItemCatalog.Name = "INDSleItemCatalog"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleItemCatalog, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleItemCatalog, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleItemCatalog, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleItemCatalog, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleItemCatalog, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleItemCatalog, False)
        Me.INDSleItemCatalog.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleItemCatalog.Properties.Appearance.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSleItemCatalog.Properties.Appearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSleItemCatalog.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 28.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleItemCatalog.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleItemCatalog.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleItemCatalog.Properties.Appearance.Options.UseBorderColor = True
        Me.INDSleItemCatalog.Properties.Appearance.Options.UseFont = True
        Me.INDSleItemCatalog.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleItemCatalog.Properties.Appearance.Options.UseTextOptions = True
        Me.INDSleItemCatalog.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDSleItemCatalog.Properties.DisplayMember = "CodeDescription"
        Me.INDSleItemCatalog.Properties.NullText = "Seleccione un Catálogo de Artículo"
        Me.INDSleItemCatalog.Properties.PopupSizeable = False
        Me.INDSleItemCatalog.Properties.PopupView = Me.INDviewItemCatalog
        Me.INDSleItemCatalog.Properties.ShowClearButton = False
        Me.INDSleItemCatalog.Properties.ShowFooter = False
        Me.INDSleItemCatalog.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleItemCatalog, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleItemCatalog, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleItemCatalog, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleItemCatalog, True)
        Me.INDSleItemCatalog.Size = New System.Drawing.Size(1278, 60)
        Me.INDSleItemCatalog.StyleController = Me.INDlyRoot
        Me.INDSleItemCatalog.TabIndex = 10
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleItemCatalog, Nothing)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleItemCatalog, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleItemCatalog, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleItemCatalog, False)
        '
        'INDviewItemCatalog
        '
        Me.INDviewItemCatalog.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewItemCatalog.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewItemCatalog.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewItemCatalog.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewItemCatalog.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDviewItemCatalog.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewItemCatalog.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewItemCatalog.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewItemCatalog.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewItemCatalog.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewItemCatalog.Appearance.Row.Options.UseFont = True
        Me.INDviewItemCatalog.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColItemCatalogCode, Me.INDColItemCatalogDescription})
        Me.INDviewItemCatalog.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDviewItemCatalog.Name = "INDviewItemCatalog"
        Me.INDviewItemCatalog.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDviewItemCatalog.OptionsSelection.MultiSelect = True
        Me.INDviewItemCatalog.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect
        Me.INDviewItemCatalog.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewItemCatalog.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewItemCatalog.OptionsView.ShowAutoFilterRow = True
        Me.INDviewItemCatalog.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewItemCatalog, False)
        '
        'INDColItemCatalogCode
        '
        Me.INDColItemCatalogCode.Caption = "Código"
        Me.INDColItemCatalogCode.FieldName = "Code"
        Me.INDColItemCatalogCode.Name = "INDColItemCatalogCode"
        Me.INDColItemCatalogCode.Visible = True
        Me.INDColItemCatalogCode.VisibleIndex = 1
        Me.INDColItemCatalogCode.Width = 143
        '
        'INDColItemCatalogDescription
        '
        Me.INDColItemCatalogDescription.Caption = "Descripción"
        Me.INDColItemCatalogDescription.FieldName = "Description"
        Me.INDColItemCatalogDescription.Name = "INDColItemCatalogDescription"
        Me.INDColItemCatalogDescription.Visible = True
        Me.INDColItemCatalogDescription.VisibleIndex = 2
        Me.INDColItemCatalogDescription.Width = 277
        '
        'Root
        '
        Me.Root.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Root.AppearanceGroup.Options.UseFont = True
        Me.Root.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Root.AppearanceItemCaption.Options.UseFont = True
        Me.Root.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.Header.Options.UseFont = True
        Me.Root.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.Root.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.Root.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.Root.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.Root.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.Root, False)
        Me.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.Root.GroupBordersVisible = False
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemOptionsMenu, Me.INDlyItemCareCenter, Me.INDtcgInformation})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(1366, 595)
        Me.Root.TextVisible = False
        '
        'INDlyItemOptionsMenu
        '
        Me.INDlyItemOptionsMenu.Control = Me.INDddbOptionsMenu
        Me.INDlyItemOptionsMenu.Location = New System.Drawing.Point(1282, 0)
        Me.INDlyItemOptionsMenu.MaxSize = New System.Drawing.Size(64, 64)
        Me.INDlyItemOptionsMenu.MinSize = New System.Drawing.Size(64, 64)
        Me.INDlyItemOptionsMenu.Name = "INDlyItemOptionsMenu"
        Me.INDlyItemOptionsMenu.Size = New System.Drawing.Size(64, 64)
        Me.INDlyItemOptionsMenu.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemOptionsMenu.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemOptionsMenu.TextVisible = False
        '
        'INDlyItemCareCenter
        '
        Me.INDlyItemCareCenter.Control = Me.INDSleItemCatalog
        Me.INDlyItemCareCenter.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCareCenter.MaxSize = New System.Drawing.Size(0, 64)
        Me.INDlyItemCareCenter.MinSize = New System.Drawing.Size(5, 64)
        Me.INDlyItemCareCenter.Name = "INDlyItemCareCenter"
        Me.INDlyItemCareCenter.Size = New System.Drawing.Size(1282, 64)
        Me.INDlyItemCareCenter.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCareCenter.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemCareCenter.TextVisible = False
        '
        'INDtcgInformation
        '
        Me.INDtcgInformation.Location = New System.Drawing.Point(0, 64)
        Me.INDtcgInformation.Name = "INDtcgInformation"
        Me.INDtcgInformation.SelectedTabPage = Me.INDlycgRequests
        Me.INDtcgInformation.Size = New System.Drawing.Size(1346, 511)
        Me.INDtcgInformation.Spacing = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.INDtcgInformation.TabPages.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlycgRequests, Me.INDlycgRequestsWithWorkOrder})
        '
        'INDlycgRequests
        '
        Me.INDlycgRequests.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlycgRequests.AppearanceGroup.Options.UseFont = True
        Me.INDlycgRequests.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlycgRequests.AppearanceItemCaption.Options.UseFont = True
        Me.INDlycgRequests.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlycgRequests.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlycgRequests.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlycgRequests.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlycgRequests.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlycgRequests.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlycgRequests.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlycgRequests.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlycgRequests.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlycgRequests.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlycgRequests, False)
        Me.INDlycgRequests.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemRequests})
        Me.INDlycgRequests.Location = New System.Drawing.Point(0, 0)
        Me.INDlycgRequests.Name = "INDlycgRequests"
        Me.INDlycgRequests.Size = New System.Drawing.Size(1326, 460)
        Me.INDlycgRequests.Text = "Solicitudes"
        '
        'INDlyItemRequests
        '
        Me.INDlyItemRequests.Control = Me.INDgcRequests
        Me.INDlyItemRequests.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemRequests.MinSize = New System.Drawing.Size(5, 5)
        Me.INDlyItemRequests.Name = "INDlyItemRequests"
        Me.INDlyItemRequests.Size = New System.Drawing.Size(1326, 460)
        Me.INDlyItemRequests.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemRequests.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemRequests.TextVisible = False
        '
        'INDlycgRequestsWithWorkOrder
        '
        Me.INDlycgRequestsWithWorkOrder.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlycgRequestsWithWorkOrder.AppearanceGroup.Options.UseFont = True
        Me.INDlycgRequestsWithWorkOrder.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlycgRequestsWithWorkOrder.AppearanceItemCaption.Options.UseFont = True
        Me.INDlycgRequestsWithWorkOrder.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlycgRequestsWithWorkOrder.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlycgRequestsWithWorkOrder.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlycgRequestsWithWorkOrder.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlycgRequestsWithWorkOrder.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlycgRequestsWithWorkOrder.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlycgRequestsWithWorkOrder.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlycgRequestsWithWorkOrder.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlycgRequestsWithWorkOrder.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlycgRequestsWithWorkOrder.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlycgRequestsWithWorkOrder, False)
        Me.INDlycgRequestsWithWorkOrder.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemRequestsWithWorkOrder})
        Me.INDlycgRequestsWithWorkOrder.Location = New System.Drawing.Point(0, 0)
        Me.INDlycgRequestsWithWorkOrder.Name = "INDlycgRequestsWithWorkOrder"
        Me.INDlycgRequestsWithWorkOrder.Size = New System.Drawing.Size(1326, 460)
        Me.INDlycgRequestsWithWorkOrder.Text = "Solicitudes con Orden de Trabajo"
        '
        'INDlyItemRequestsWithWorkOrder
        '
        Me.INDlyItemRequestsWithWorkOrder.Control = Me.INDgcRequestsWithWorkOrder
        Me.INDlyItemRequestsWithWorkOrder.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemRequestsWithWorkOrder.MinSize = New System.Drawing.Size(5, 5)
        Me.INDlyItemRequestsWithWorkOrder.Name = "INDlyItemRequestsWithWorkOrder"
        Me.INDlyItemRequestsWithWorkOrder.Size = New System.Drawing.Size(1326, 460)
        Me.INDlyItemRequestsWithWorkOrder.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemRequestsWithWorkOrder.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemRequestsWithWorkOrder.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'BarManager2
        '
        Me.BarManager2.DockControls.Add(Me.BarDockControl5)
        Me.BarManager2.DockControls.Add(Me.BarDockControl6)
        Me.BarManager2.DockControls.Add(Me.BarDockControl7)
        Me.BarManager2.DockControls.Add(Me.BarDockControl8)
        Me.BarManager2.Form = Me
        Me.BarManager2.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.INDBbiAsign})
        Me.BarManager2.MaxItemId = 32
        '
        'BarDockControl5
        '
        Me.BarDockControl5.CausesValidation = False
        Me.BarDockControl5.Dock = System.Windows.Forms.DockStyle.Top
        Me.BarDockControl5.Location = New System.Drawing.Point(0, 5)
        Me.BarDockControl5.Manager = Me.BarManager2
        Me.BarDockControl5.Size = New System.Drawing.Size(1370, 0)
        '
        'BarDockControl6
        '
        Me.BarDockControl6.CausesValidation = False
        Me.BarDockControl6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.BarDockControl6.Location = New System.Drawing.Point(0, 739)
        Me.BarDockControl6.Manager = Me.BarManager2
        Me.BarDockControl6.Size = New System.Drawing.Size(1370, 0)
        '
        'BarDockControl7
        '
        Me.BarDockControl7.CausesValidation = False
        Me.BarDockControl7.Dock = System.Windows.Forms.DockStyle.Left
        Me.BarDockControl7.Location = New System.Drawing.Point(0, 5)
        Me.BarDockControl7.Manager = Me.BarManager2
        Me.BarDockControl7.Size = New System.Drawing.Size(0, 734)
        '
        'BarDockControl8
        '
        Me.BarDockControl8.CausesValidation = False
        Me.BarDockControl8.Dock = System.Windows.Forms.DockStyle.Right
        Me.BarDockControl8.Location = New System.Drawing.Point(1370, 5)
        Me.BarDockControl8.Manager = Me.BarManager2
        Me.BarDockControl8.Size = New System.Drawing.Size(0, 734)
        '
        'INDBbiAsign
        '
        Me.INDBbiAsign.Caption = "Asignar"
        Me.INDBbiAsign.Id = 19
        Me.INDBbiAsign.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiAsign.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDBbiAsign.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiAsign.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDBbiAsign.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiAsign.ItemAppearance.Normal.Options.UseFont = True
        Me.INDBbiAsign.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiAsign.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDBbiAsign.Name = "INDBbiAsign"
        '
        'INDPopMenuActions2
        '
        Me.INDPopMenuActions2.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.INDBbiAsign)})
        Me.INDPopMenuActions2.Manager = Me.BarManager2
        Me.INDPopMenuActions2.Name = "INDPopMenuActions2"
        '
        'FrmDashboardMaintenance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1370, 739)
        Me.Controls.Add(Me.BarDockControl3)
        Me.Controls.Add(Me.BarDockControl4)
        Me.Controls.Add(Me.BarDockControl2)
        Me.Controls.Add(Me.BarDockControl1)
        Me.Controls.Add(Me.BarDockControl7)
        Me.Controls.Add(Me.BarDockControl8)
        Me.Controls.Add(Me.BarDockControl6)
        Me.Controls.Add(Me.BarDockControl5)
        Me.IconOptions.ShowIcon = False
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmDashboardMaintenance"
        Me.Opacity = 1.0R
        Me.Tag = "2232"
        Me.Text = "Dashboard Mantenimiento"
        Me.Controls.SetChildIndex(Me.BarDockControl5, 0)
        Me.Controls.SetChildIndex(Me.BarDockControl6, 0)
        Me.Controls.SetChildIndex(Me.BarDockControl8, 0)
        Me.Controls.SetChildIndex(Me.BarDockControl7, 0)
        Me.Controls.SetChildIndex(Me.BarDockControl1, 0)
        Me.Controls.SetChildIndex(Me.BarDockControl2, 0)
        Me.Controls.SetChildIndex(Me.BarDockControl4, 0)
        Me.Controls.SetChildIndex(Me.BarDockControl3, 0)
        Me.Controls.SetChildIndex(Me.ToolBars, 0)
        Me.Controls.SetChildIndex(Me.INDPanelControlBase, 0)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPopMenuActions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyRoot.ResumeLayout(False)
        CType(Me.INDgcRequestsWithWorkOrder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewRequestsWithWorkOrder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRepCbeRadicatedAlert, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcRequests, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewRequests, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRepCheckSelectOption, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleItemCatalog.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewItemCatalog, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemOptionsMenu, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCareCenter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtcgInformation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlycgRequests, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemRequests, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlycgRequestsWithWorkOrder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemRequestsWithWorkOrder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPopMenuActions2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents IndigoGroupControl1 As IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As IndigoLabelControl
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents INDlyRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDddbOptionsMenu As DevExpress.XtraEditors.DropDownButton
    Friend WithEvents INDlyItemOptionsMenu As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents BarDockControl1 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl2 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl3 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl4 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents INDBarRefresh As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDPopMenuActions As DevExpress.XtraBars.PopupMenu
    Friend WithEvents INDSleItemCatalog As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDviewItemCatalog As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColItemCatalogCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColItemCatalogDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlyItemCareCenter As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDgcRequests As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDviewRequests As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDtcgInformation As DevExpress.XtraLayout.TabbedControlGroup
    Friend WithEvents INDlycgRequests As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemRequests As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlycgRequestsWithWorkOrder As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDgcRequestsWithWorkOrder As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDviewRequestsWithWorkOrder As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItemRequestsWithWorkOrder As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDviewRequests_DateFailure As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDviewRequests_Code As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDviewRequests_Plate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDviewRequests_ItemCatalog As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDviewRequests_Item As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDviewRequests_Location As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDviewRequests_Part As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDviewRequests_RequestUser As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDviewRequestsWithWorkOrder_ItemCatalog As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDviewRequestsWithWorkOrder_DateFailure As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDviewRequestsWithWorkOrder_Code As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDviewRequestsWithWorkOrder_Plate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDviewRequestsWithWorkOrder_Item As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDviewRequestsWithWorkOrder_Location As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDviewRequestsWithWorkOrder_Part As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDviewRequestsWithWorkOrder_RequestUser As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDviewRequestsWithWorkOrder_AssignUser As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoSimpleButton1 As IndigoSimpleButton
    Friend WithEvents INDviewRequests_SelectOption As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRepCheckSelectOption As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents INDRepCbeRadicatedAlert As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents INDviewRequestsWithWorkOrder_SelectOption As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BarDockControl7 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarManager2 As DevExpress.XtraBars.BarManager
    Friend WithEvents BarDockControl5 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl6 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl8 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents INDPopMenuActions2 As DevExpress.XtraBars.PopupMenu
    Friend WithEvents INDBbiAsign As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents IndigoSearchLookUpControl1 As IndigoSearchLookUpControl
End Class
