Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmDashboardRequestMixingStation
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
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
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
        Me.SummaryCtrl1 = New Presentation.MixingStation.SummaryCtrl()
        Me.INDviewRequestMixingStation = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColIconStateHis = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRptPic = New DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit()
        Me.INDgcRequestMixingStation = New DevExpress.XtraGrid.GridControl()
        Me.ToolTipController1 = New DevExpress.Utils.ToolTipController(Me.components)
        Me.SimpleButton1 = New DevExpress.XtraEditors.SimpleButton()
        Me.INDsleCM = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDviewSearchCM = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemOptionsMenu = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemCareCenter = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDtcgInformation = New DevExpress.XtraLayout.TabbedControlGroup()
        Me.INDlcgRequestMixingStation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemRequestMixingStation = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDpopMenuType = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.INDBarIntraHospitable = New DevExpress.XtraBars.BarButtonItem()
        Me.INDBarAmbulatory = New DevExpress.XtraBars.BarButtonItem()
        Me.BarManager2 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.BarDockControl5 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl6 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl7 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl8 = New DevExpress.XtraBars.BarDockControl()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPopMenuActions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyRoot.SuspendLayout()
        CType(Me.INDviewRequestMixingStation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRptPic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcRequestMixingStation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleCM.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewSearchCM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemOptionsMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCareCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtcgInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgRequestMixingStation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemRequestMixingStation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpopMenuType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
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
        Me.INDBarRefresh.ImageOptions.Image = Global.Presentation.MixingStation.My.Resources.Resources.Refresh_16x16_blue
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
        Me.INDddbOptionsMenu.ImageOptions.Image = Global.Presentation.MixingStation.My.Resources.Resources.Mmenu_de_acciones
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
        Me.INDlyRoot.Controls.Add(Me.SummaryCtrl1)
        Me.INDlyRoot.Controls.Add(Me.SimpleButton1)
        Me.INDlyRoot.Controls.Add(Me.INDgcRequestMixingStation)
        Me.INDlyRoot.Controls.Add(Me.INDsleCM)
        Me.INDlyRoot.Controls.Add(Me.INDddbOptionsMenu)
        Me.INDlyRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyRoot.Location = New System.Drawing.Point(2, 7)
        Me.INDlyRoot.Name = "INDlyRoot"
        Me.INDlyRoot.Root = Me.Root
        Me.INDlyRoot.Size = New System.Drawing.Size(1366, 595)
        Me.INDlyRoot.TabIndex = 0
        Me.INDlyRoot.Text = "LayoutControl1"
        '
        'SummaryCtrl1
        '
        Me.SummaryCtrl1.Location = New System.Drawing.Point(24, 539)
        Me.SummaryCtrl1.Name = "SummaryCtrl1"
        Me.SummaryCtrl1.Size = New System.Drawing.Size(1318, 32)
        Me.SummaryCtrl1.TabIndex = 13
        Me.SummaryCtrl1.View = Me.INDviewRequestMixingStation
        '
        'INDviewRequestMixingStation
        '
        Me.INDviewRequestMixingStation.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewRequestMixingStation.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewRequestMixingStation.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewRequestMixingStation.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewRequestMixingStation.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewRequestMixingStation.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewRequestMixingStation.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewRequestMixingStation.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewRequestMixingStation.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewRequestMixingStation.Appearance.Row.Options.UseFont = True
        Me.INDviewRequestMixingStation.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDviewRequestMixingStation.Appearance.ViewCaption.Options.UseFont = True
        Me.INDviewRequestMixingStation.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn2, Me.GridColumn1, Me.GridColumn9, Me.GridColumn6, Me.GridColumn5, Me.GridColumn7, Me.GridColumn8, Me.GridColumn10, Me.GridColumn12, Me.GridColumn11, Me.ColIconStateHis})
        GridFormatRule1.Name = "Format0"
        FormatConditionRuleValue1.Appearance.BackColor = System.Drawing.Color.Red
        FormatConditionRuleValue1.Appearance.BackColor2 = System.Drawing.Color.White
        FormatConditionRuleValue1.Appearance.ForeColor = System.Drawing.Color.Red
        FormatConditionRuleValue1.Appearance.Options.UseBackColor = True
        FormatConditionRuleValue1.Appearance.Options.UseForeColor = True
        FormatConditionRuleValue1.Appearance.Options.UseTextOptions = True
        FormatConditionRuleValue1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        FormatConditionRuleValue1.Condition = DevExpress.XtraEditors.FormatCondition.Equal
        FormatConditionRuleValue1.Expression = "[ColorRequest] = 3"
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
        FormatConditionRuleValue2.Expression = "[ColorRequest] = 2"
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
        FormatConditionRuleValue3.Expression = "[ColorRequest] = 1"
        FormatConditionRuleValue3.Value1 = 1
        GridFormatRule3.Rule = FormatConditionRuleValue3
        Me.INDviewRequestMixingStation.FormatRules.Add(GridFormatRule1)
        Me.INDviewRequestMixingStation.FormatRules.Add(GridFormatRule2)
        Me.INDviewRequestMixingStation.FormatRules.Add(GridFormatRule3)
        Me.INDviewRequestMixingStation.GridControl = Me.INDgcRequestMixingStation
        Me.INDviewRequestMixingStation.GroupCount = 1
        Me.INDviewRequestMixingStation.Name = "INDviewRequestMixingStation"
        Me.INDviewRequestMixingStation.OptionsBehavior.AutoExpandAllGroups = True
        Me.INDviewRequestMixingStation.OptionsSelection.CheckBoxSelectorColumnWidth = 30
        Me.INDviewRequestMixingStation.OptionsSelection.MultiSelect = True
        Me.INDviewRequestMixingStation.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect
        Me.INDviewRequestMixingStation.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewRequestMixingStation.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewRequestMixingStation.OptionsView.ShowAutoFilterRow = True
        Me.INDviewRequestMixingStation.OptionsView.ShowDetailButtons = False
        Me.INDviewRequestMixingStation.OptionsView.ShowGroupPanel = False
        Me.INDviewRequestMixingStation.OptionsView.ShowIndicator = False
        Me.INDviewRequestMixingStation.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn2, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewRequestMixingStation, False)
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Línea producción"
        Me.GridColumn2.FieldName = "ProductionLineCodeName"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 3
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Solicitud"
        Me.GridColumn1.FieldName = "RequestCode"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 2
        Me.GridColumn1.Width = 53
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Fecha solicitud"
        Me.GridColumn9.FieldName = "RequestDate"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.OptionsColumn.AllowEdit = False
        Me.GridColumn9.OptionsColumn.AllowFocus = False
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 3
        Me.GridColumn9.Width = 85
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Usuario solicitud"
        Me.GridColumn6.FieldName = "RequestUserCodeName"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.AllowEdit = False
        Me.GridColumn6.OptionsColumn.AllowFocus = False
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 4
        Me.GridColumn6.Width = 103
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Centro atención "
        Me.GridColumn5.FieldName = "CareCenterCodeName"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.AllowEdit = False
        Me.GridColumn5.OptionsColumn.AllowFocus = False
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 5
        Me.GridColumn5.Width = 162
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Cantidad"
        Me.GridColumn7.FieldName = "Quantity"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsColumn.AllowEdit = False
        Me.GridColumn7.OptionsColumn.AllowFocus = False
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 6
        Me.GridColumn7.Width = 57
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Paquete asignado"
        Me.GridColumn8.FieldName = "ItemCodeName"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.OptionsColumn.AllowEdit = False
        Me.GridColumn8.OptionsColumn.AllowFocus = False
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 7
        Me.GridColumn8.Width = 190
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "Tipo dosis unitaria"
        Me.GridColumn10.FieldName = "UnitDoseTypeCodeName"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.OptionsColumn.AllowEdit = False
        Me.GridColumn10.OptionsColumn.AllowFocus = False
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 8
        Me.GridColumn10.Width = 152
        '
        'GridColumn12
        '
        Me.GridColumn12.Caption = "Paciente"
        Me.GridColumn12.FieldName = "PatientCodeName"
        Me.GridColumn12.Name = "GridColumn12"
        Me.GridColumn12.OptionsColumn.AllowEdit = False
        Me.GridColumn12.OptionsColumn.AllowFocus = False
        Me.GridColumn12.Visible = True
        Me.GridColumn12.VisibleIndex = 10
        Me.GridColumn12.Width = 182
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "Origen "
        Me.GridColumn11.FieldName = "RequestTypeName"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.OptionsColumn.AllowEdit = False
        Me.GridColumn11.OptionsColumn.AllowFocus = False
        Me.GridColumn11.Visible = True
        Me.GridColumn11.VisibleIndex = 9
        Me.GridColumn11.Width = 175
        '
        'ColIconStateHis
        '
        Me.ColIconStateHis.Caption = " "
        Me.ColIconStateHis.ColumnEdit = Me.INDRptPic
        Me.ColIconStateHis.FieldName = "GridColumn14"
        Me.ColIconStateHis.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
        Me.ColIconStateHis.Name = "ColIconStateHis"
        Me.ColIconStateHis.OptionsColumn.AllowEdit = False
        Me.ColIconStateHis.OptionsColumn.AllowFocus = False
        Me.ColIconStateHis.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColIconStateHis.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColIconStateHis.OptionsColumn.AllowMove = False
        Me.ColIconStateHis.OptionsColumn.AllowShowHide = False
        Me.ColIconStateHis.OptionsColumn.AllowSize = False
        Me.ColIconStateHis.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColIconStateHis.OptionsColumn.FixedWidth = True
        Me.ColIconStateHis.UnboundType = DevExpress.Data.UnboundColumnType.[Object]
        Me.ColIconStateHis.Visible = True
        Me.ColIconStateHis.VisibleIndex = 1
        Me.ColIconStateHis.Width = 31
        '
        'INDRptPic
        '
        Me.INDRptPic.Name = "INDRptPic"
        Me.INDRptPic.NullText = " "
        '
        'INDgcRequestMixingStation
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcRequestMixingStation, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcRequestMixingStation, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcRequestMixingStation, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcRequestMixingStation, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcRequestMixingStation, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcRequestMixingStation, False)
        Me.INDgcRequestMixingStation.Location = New System.Drawing.Point(24, 119)
        Me.INDgcRequestMixingStation.MainView = Me.INDviewRequestMixingStation
        Me.INDgcRequestMixingStation.MenuManager = Me.BarManager1
        Me.INDgcRequestMixingStation.Name = "INDgcRequestMixingStation"
        Me.INDgcRequestMixingStation.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDRptPic})
        Me.INDgcRequestMixingStation.Size = New System.Drawing.Size(1318, 416)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcRequestMixingStation, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcRequestMixingStation.TabIndex = 11
        Me.INDgcRequestMixingStation.ToolTipController = Me.ToolTipController1
        Me.INDgcRequestMixingStation.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDviewRequestMixingStation})
        '
        'ToolTipController1
        '
        '
        'SimpleButton1
        '
        Me.SimpleButton1.ImageOptions.Image = Global.Presentation.MixingStation.My.Resources.Resources.Refresh_24x24_blue
        Me.SimpleButton1.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.SimpleButton1.Location = New System.Drawing.Point(1230, 12)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.SimpleButton1, False)
        Me.SimpleButton1.Name = "SimpleButton1"
        Me.SimpleButton1.Size = New System.Drawing.Size(60, 60)
        Me.SimpleButton1.StyleController = Me.INDlyRoot
        Me.SimpleButton1.TabIndex = 12
        '
        'INDsleCM
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleCM, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleCM, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleCM, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleCM, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleCM, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleCM, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleCM, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleCM, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleCM, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleCM, False)
        Me.INDsleCM.Location = New System.Drawing.Point(12, 12)
        Me.INDsleCM.MaximumSize = New System.Drawing.Size(0, 60)
        Me.INDsleCM.MinimumSize = New System.Drawing.Size(0, 60)
        Me.INDsleCM.Name = "INDsleCM"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleCM, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleCM, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleCM, False)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDsleCM, True)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleCM, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleCM, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleCM, False)
        Me.INDsleCM.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleCM.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleCM.Properties.Appearance.Options.UseFont = True
        Me.INDsleCM.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleCM.Properties.Appearance.Options.UseTextOptions = True
        Me.INDsleCM.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDsleCM.Properties.DisplayMember = "CodeName"
        Me.INDsleCM.Properties.NullText = "Seleccione una central de mezclas"
        Me.INDsleCM.Properties.PopupSizeable = False
        Me.INDsleCM.Properties.PopupView = Me.INDviewSearchCM
        Me.INDsleCM.Properties.ShowClearButton = False
        Me.INDsleCM.Properties.ShowFooter = False
        Me.INDsleCM.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleCM, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleCM, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleCM, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleCM, True)
        Me.INDsleCM.Size = New System.Drawing.Size(1214, 60)
        Me.INDsleCM.StyleController = Me.INDlyRoot
        Me.INDsleCM.TabIndex = 10
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleCM, Nothing)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleCM, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleCM, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleCM, False)
        '
        'INDviewSearchCM
        '
        Me.INDviewSearchCM.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewSearchCM.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewSearchCM.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewSearchCM.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewSearchCM.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDviewSearchCM.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewSearchCM.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewSearchCM.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewSearchCM.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewSearchCM.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewSearchCM.Appearance.Row.Options.UseFont = True
        Me.INDviewSearchCM.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3, Me.GridColumn4})
        Me.INDviewSearchCM.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDviewSearchCM.Name = "INDviewSearchCM"
        Me.INDviewSearchCM.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDviewSearchCM.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewSearchCM.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewSearchCM.OptionsView.ShowAutoFilterRow = True
        Me.INDviewSearchCM.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewSearchCM, False)
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Código"
        Me.GridColumn3.FieldName = "Code"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 0
        Me.GridColumn3.Width = 353
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Nombre"
        Me.GridColumn4.FieldName = "Name"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 1
        Me.GridColumn4.Width = 1029
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
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemOptionsMenu, Me.INDlyItemCareCenter, Me.INDtcgInformation, Me.LayoutControlItem1})
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
        Me.INDlyItemOptionsMenu.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlyItemCareCenter
        '
        Me.INDlyItemCareCenter.Control = Me.INDsleCM
        Me.INDlyItemCareCenter.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCareCenter.MaxSize = New System.Drawing.Size(0, 64)
        Me.INDlyItemCareCenter.MinSize = New System.Drawing.Size(1, 64)
        Me.INDlyItemCareCenter.Name = "INDlyItemCareCenter"
        Me.INDlyItemCareCenter.Size = New System.Drawing.Size(1218, 64)
        Me.INDlyItemCareCenter.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCareCenter.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemCareCenter.TextVisible = False
        '
        'INDtcgInformation
        '
        Me.INDtcgInformation.Location = New System.Drawing.Point(0, 64)
        Me.INDtcgInformation.Name = "INDtcgInformation"
        Me.INDtcgInformation.SelectedTabPage = Me.INDlcgRequestMixingStation
        Me.INDtcgInformation.Size = New System.Drawing.Size(1346, 511)
        Me.INDtcgInformation.TabPages.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlcgRequestMixingStation})
        '
        'INDlcgRequestMixingStation
        '
        Me.INDlcgRequestMixingStation.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgRequestMixingStation.AppearanceGroup.Options.UseFont = True
        Me.INDlcgRequestMixingStation.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgRequestMixingStation.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgRequestMixingStation.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgRequestMixingStation.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgRequestMixingStation.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgRequestMixingStation.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgRequestMixingStation.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgRequestMixingStation.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgRequestMixingStation.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgRequestMixingStation.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgRequestMixingStation.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgRequestMixingStation.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgRequestMixingStation, False)
        Me.INDlcgRequestMixingStation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemRequestMixingStation, Me.LayoutControlItem2})
        Me.INDlcgRequestMixingStation.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgRequestMixingStation.Name = "INDlcgRequestMixingStation"
        Me.INDlcgRequestMixingStation.Size = New System.Drawing.Size(1322, 456)
        Me.INDlcgRequestMixingStation.Text = "Solicitudes central mezclas"
        '
        'INDlyItemRequestMixingStation
        '
        Me.INDlyItemRequestMixingStation.Control = Me.INDgcRequestMixingStation
        Me.INDlyItemRequestMixingStation.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemRequestMixingStation.Name = "INDlyItemRequestMixingStation"
        Me.INDlyItemRequestMixingStation.Size = New System.Drawing.Size(1322, 420)
        Me.INDlyItemRequestMixingStation.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemRequestMixingStation.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.SummaryCtrl1
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 420)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(0, 36)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(5, 36)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(1322, 36)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.SimpleButton1
        Me.LayoutControlItem1.Location = New System.Drawing.Point(1218, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(64, 64)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(64, 64)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(64, 64)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'INDpopMenuType
        '
        Me.INDpopMenuType.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.INDBarIntraHospitable), New DevExpress.XtraBars.LinkPersistInfo(Me.INDBarAmbulatory)})
        Me.INDpopMenuType.Manager = Me.BarManager2
        Me.INDpopMenuType.Name = "INDpopMenuType"
        '
        'INDBarIntraHospitable
        '
        Me.INDBarIntraHospitable.Caption = "Intrahospitalario"
        Me.INDBarIntraHospitable.Id = 8
        Me.INDBarIntraHospitable.ImageOptions.Image = Global.Presentation.MixingStation.My.Resources.Resources.Add_24x24_blue
        Me.INDBarIntraHospitable.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBarIntraHospitable.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDBarIntraHospitable.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBarIntraHospitable.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDBarIntraHospitable.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBarIntraHospitable.ItemAppearance.Normal.Options.UseFont = True
        Me.INDBarIntraHospitable.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBarIntraHospitable.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDBarIntraHospitable.Name = "INDBarIntraHospitable"
        '
        'INDBarAmbulatory
        '
        Me.INDBarAmbulatory.Caption = "Ambulatorio"
        Me.INDBarAmbulatory.Id = 9
        Me.INDBarAmbulatory.ImageOptions.Image = Global.Presentation.MixingStation.My.Resources.Resources.Add_24x24_blue
        Me.INDBarAmbulatory.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBarAmbulatory.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDBarAmbulatory.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBarAmbulatory.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDBarAmbulatory.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBarAmbulatory.ItemAppearance.Normal.Options.UseFont = True
        Me.INDBarAmbulatory.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBarAmbulatory.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDBarAmbulatory.Name = "INDBarAmbulatory"
        '
        'BarManager2
        '
        Me.BarManager2.DockControls.Add(Me.BarDockControl5)
        Me.BarManager2.DockControls.Add(Me.BarDockControl6)
        Me.BarManager2.DockControls.Add(Me.BarDockControl7)
        Me.BarManager2.DockControls.Add(Me.BarDockControl8)
        Me.BarManager2.Form = Me
        Me.BarManager2.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.INDBarIntraHospitable, Me.INDBarAmbulatory})
        Me.BarManager2.MaxItemId = 10
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
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'FrmDashboardRequestMixingStation
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
        Me.Name = "FrmDashboardRequestMixingStation"
        Me.Opacity = 1.0R
        Me.Tag = "2224"
        Me.Text = "Solicitudes central mezclas"
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
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPopMenuActions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyRoot.ResumeLayout(False)
        CType(Me.INDviewRequestMixingStation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRptPic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcRequestMixingStation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleCM.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewSearchCM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemOptionsMenu, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCareCenter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtcgInformation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgRequestMixingStation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemRequestMixingStation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpopMenuType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents INDsleCM As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDviewSearchCM As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlyItemCareCenter As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDgcRequestMixingStation As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDviewRequestMixingStation As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents IndigoSimpleButton1 As IndigoSimpleButton
    Friend WithEvents BarDockControl5 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarManager2 As DevExpress.XtraBars.BarManager
    Friend WithEvents BarDockControl6 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl7 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl8 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents INDpopMenuType As DevExpress.XtraBars.PopupMenu
    Friend WithEvents INDBarIntraHospitable As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDBarAmbulatory As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDtcgInformation As DevExpress.XtraLayout.TabbedControlGroup
    Friend WithEvents INDlcgRequestMixingStation As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemRequestMixingStation As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents SimpleButton1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents SummaryCtrl1 As SummaryCtrl
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents ColIconStateHis As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRptPic As DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit
    Friend WithEvents ToolTipController1 As DevExpress.Utils.ToolTipController
    Friend WithEvents IndigoSearchLookUpControl1 As IndigoSearchLookUpControl
End Class
