Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmDashboardManagementMedicalOrder
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmDashboardManagementMedicalOrder))
        Dim GridFormatRule1 As DevExpress.XtraGrid.GridFormatRule = New DevExpress.XtraGrid.GridFormatRule()
        Dim FormatConditionRuleValue1 As DevExpress.XtraEditors.FormatConditionRuleValue = New DevExpress.XtraEditors.FormatConditionRuleValue()
        Dim GridFormatRule2 As DevExpress.XtraGrid.GridFormatRule = New DevExpress.XtraGrid.GridFormatRule()
        Dim FormatConditionRuleValue2 As DevExpress.XtraEditors.FormatConditionRuleValue = New DevExpress.XtraEditors.FormatConditionRuleValue()
        Dim GridFormatRule3 As DevExpress.XtraGrid.GridFormatRule = New DevExpress.XtraGrid.GridFormatRule()
        Dim FormatConditionRuleValue3 As DevExpress.XtraEditors.FormatConditionRuleValue = New DevExpress.XtraEditors.FormatConditionRuleValue()
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.BarManager = New DevExpress.XtraBars.BarManager(Me.components)
        Me.BarDockControl1 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl2 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl3 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl4 = New DevExpress.XtraBars.BarDockControl()
        Me.INDBarRefresh = New DevExpress.XtraBars.BarButtonItem()
        Me.INDddbOptionsMenu = New DevExpress.XtraEditors.DropDownButton()
        Me.INDPopMenuActions = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.INDlyRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDgcRequests = New DevExpress.XtraGrid.GridControl()
        Me.INDgvRequests = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDgvRequests_Patient = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgvRequests_SelectOption = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgvRequests_RequestDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgvRequests_PatientCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgvRequests_AdmissionNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgvRequests_Folio = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgvRequests_Type = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgvRequests_Item = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgvRequests_Description = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgvRequests_Quantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgvRequests_FunctionalUnit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgvRequests_CareGroup = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgvRequests_HealthAdministrator = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgvRequests_CareCenter = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgvRequests_Covered = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgvRequests_Contracted = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgvRequests_Quoted = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgvRequests_Autorized = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgvRequests_Professional = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgvRequests_Observations = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleCareCenter = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvCareCenter = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGvCareCenter_UnboundSelection = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvCareCenter_Code = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvCareCenter_Name = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemOptionsMenu = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciCareCenter = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlycgRequests = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemRequests = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.BarManager2 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.BarDockControl5 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl6 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl7 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl8 = New DevExpress.XtraBars.BarDockControl()
        Me.INDBbiSelection = New DevExpress.XtraBars.BarButtonItem()
        Me.INDBbiUnSelection = New DevExpress.XtraBars.BarButtonItem()
        Me.INDbbiOpenRequest = New DevExpress.XtraBars.BarButtonItem()
        Me.INDBbiConfirm = New DevExpress.XtraBars.BarButtonItem()
        Me.INDBbiCancel = New DevExpress.XtraBars.BarButtonItem()
        Me.INDBbiPrintWithdrawal = New DevExpress.XtraBars.BarButtonItem()
        Me.INDBbiPrintMedicalRecord = New DevExpress.XtraBars.BarButtonItem()
        Me.INDBbiPrintNursingRecord = New DevExpress.XtraBars.BarButtonItem()
        Me.INDBbiPrintAccountSupport = New DevExpress.XtraBars.BarButtonItem()
        Me.INDBbiPrintOrders = New DevExpress.XtraBars.BarButtonItem()
        Me.INDBbiAttach = New DevExpress.XtraBars.BarButtonItem()
        Me.INDPopMenuActions2 = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPopMenuActions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyRoot.SuspendLayout()
        CType(Me.INDgcRequests, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvRequests, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleCareCenter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvCareCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemOptionsMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciCareCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlycgRequests, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemRequests, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPopMenuActions2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyRoot)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 5)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1370, 734)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1370, 0)
        Me.ToolBars.Visible = False
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(1370, 0)
        '
        'BarManager
        '
        Me.BarManager.DockControls.Add(Me.BarDockControl1)
        Me.BarManager.DockControls.Add(Me.BarDockControl2)
        Me.BarManager.DockControls.Add(Me.BarDockControl3)
        Me.BarManager.DockControls.Add(Me.BarDockControl4)
        Me.BarManager.Form = Me
        Me.BarManager.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.INDBarRefresh})
        Me.BarManager.MaxItemId = 7
        '
        'BarDockControl1
        '
        Me.BarDockControl1.CausesValidation = False
        Me.BarDockControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.BarDockControl1.Location = New System.Drawing.Point(0, 5)
        Me.BarDockControl1.Manager = Me.BarManager
        Me.BarDockControl1.Size = New System.Drawing.Size(1370, 0)
        '
        'BarDockControl2
        '
        Me.BarDockControl2.CausesValidation = False
        Me.BarDockControl2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.BarDockControl2.Location = New System.Drawing.Point(0, 739)
        Me.BarDockControl2.Manager = Me.BarManager
        Me.BarDockControl2.Size = New System.Drawing.Size(1370, 0)
        '
        'BarDockControl3
        '
        Me.BarDockControl3.CausesValidation = False
        Me.BarDockControl3.Dock = System.Windows.Forms.DockStyle.Left
        Me.BarDockControl3.Location = New System.Drawing.Point(0, 5)
        Me.BarDockControl3.Manager = Me.BarManager
        Me.BarDockControl3.Size = New System.Drawing.Size(0, 734)
        '
        'BarDockControl4
        '
        Me.BarDockControl4.CausesValidation = False
        Me.BarDockControl4.Dock = System.Windows.Forms.DockStyle.Right
        Me.BarDockControl4.Location = New System.Drawing.Point(1370, 5)
        Me.BarDockControl4.Manager = Me.BarManager
        Me.BarDockControl4.Size = New System.Drawing.Size(0, 734)
        '
        'INDBarRefresh
        '
        Me.INDBarRefresh.Caption = "Refrescar"
        Me.INDBarRefresh.Id = 5
        Me.INDBarRefresh.ImageOptions.Image = Global.Presentation.Authorization.My.Resources.Resources.Refresh_16x16_blue
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
        Me.INDddbOptionsMenu.ImageOptions.Image = CType(resources.GetObject("INDddbOptionsMenu.ImageOptions.Image"), System.Drawing.Image)
        Me.INDddbOptionsMenu.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.INDddbOptionsMenu.Location = New System.Drawing.Point(1294, 12)
        Me.INDddbOptionsMenu.MenuManager = Me.BarManager
        Me.INDddbOptionsMenu.Name = "INDddbOptionsMenu"
        Me.INDddbOptionsMenu.Size = New System.Drawing.Size(60, 60)
        Me.INDddbOptionsMenu.StyleController = Me.INDlyRoot
        Me.INDddbOptionsMenu.TabIndex = 9
        '
        'INDPopMenuActions
        '
        Me.INDPopMenuActions.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.INDBarRefresh)})
        Me.INDPopMenuActions.Manager = Me.BarManager
        Me.INDPopMenuActions.Name = "INDPopMenuActions"
        '
        'INDlyRoot
        '
        Me.INDlyRoot.Controls.Add(Me.INDgcRequests)
        Me.INDlyRoot.Controls.Add(Me.INDSleCareCenter)
        Me.INDlyRoot.Controls.Add(Me.INDddbOptionsMenu)
        Me.INDlyRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyRoot.Location = New System.Drawing.Point(2, 7)
        Me.INDlyRoot.Name = "INDlyRoot"
        Me.INDlyRoot.Root = Me.Root
        Me.INDlyRoot.Size = New System.Drawing.Size(1366, 725)
        Me.INDlyRoot.TabIndex = 0
        Me.INDlyRoot.Text = "LayoutControl1"
        '
        'INDgcRequests
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcRequests, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcRequests, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcRequests, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcRequests, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcRequests, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcRequests, False)
        Me.INDgcRequests.Location = New System.Drawing.Point(24, 88)
        Me.INDgcRequests.MainView = Me.INDgvRequests
        Me.INDgcRequests.MenuManager = Me.BarManager
        Me.INDgcRequests.MinimumSize = New System.Drawing.Size(1, 1)
        Me.INDgcRequests.Name = "INDgcRequests"
        Me.INDgcRequests.Size = New System.Drawing.Size(1318, 613)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcRequests, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcRequests.TabIndex = 11
        Me.INDgcRequests.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvRequests})
        '
        'INDgvRequests
        '
        Me.INDgvRequests.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvRequests.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvRequests.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvRequests.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvRequests.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvRequests.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvRequests.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvRequests.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvRequests.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvRequests.Appearance.Row.Options.UseFont = True
        Me.INDgvRequests.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgvRequests.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvRequests.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDgvRequests_Patient, Me.INDgvRequests_SelectOption, Me.INDgvRequests_RequestDate, Me.INDgvRequests_PatientCode, Me.INDgvRequests_AdmissionNumber, Me.INDgvRequests_Folio, Me.INDgvRequests_Type, Me.INDgvRequests_Item, Me.INDgvRequests_Description, Me.INDgvRequests_Quantity, Me.INDgvRequests_FunctionalUnit, Me.INDgvRequests_CareGroup, Me.INDgvRequests_HealthAdministrator, Me.INDgvRequests_CareCenter, Me.INDgvRequests_Covered, Me.INDgvRequests_Contracted, Me.INDgvRequests_Quoted, Me.INDgvRequests_Autorized, Me.INDgvRequests_Professional, Me.INDgvRequests_Observations, Me.GridColumn1})
        GridFormatRule1.Name = "Format0"
        FormatConditionRuleValue1.Appearance.BackColor = System.Drawing.Color.Red
        FormatConditionRuleValue1.Appearance.BackColor2 = System.Drawing.Color.White
        FormatConditionRuleValue1.Appearance.ForeColor = System.Drawing.Color.Red
        FormatConditionRuleValue1.Appearance.Options.UseBackColor = True
        FormatConditionRuleValue1.Appearance.Options.UseForeColor = True
        FormatConditionRuleValue1.Appearance.Options.UseTextOptions = True
        FormatConditionRuleValue1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        FormatConditionRuleValue1.Condition = DevExpress.XtraEditors.FormatCondition.Equal
        FormatConditionRuleValue1.Expression = "[Color] = 3"
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
        FormatConditionRuleValue2.Expression = "[Color] = 2"
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
        FormatConditionRuleValue3.Expression = "[Color] = 1"
        FormatConditionRuleValue3.Value1 = 1
        GridFormatRule3.Rule = FormatConditionRuleValue3
        Me.INDgvRequests.FormatRules.Add(GridFormatRule1)
        Me.INDgvRequests.FormatRules.Add(GridFormatRule2)
        Me.INDgvRequests.FormatRules.Add(GridFormatRule3)
        Me.INDgvRequests.GridControl = Me.INDgcRequests
        Me.INDgvRequests.GroupCount = 1
        Me.INDgvRequests.Name = "INDgvRequests"
        Me.INDgvRequests.OptionsBehavior.AutoExpandAllGroups = True
        Me.INDgvRequests.OptionsSelection.CheckBoxSelectorColumnWidth = 30
        Me.INDgvRequests.OptionsSelection.MultiSelect = True
        Me.INDgvRequests.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvRequests.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvRequests.OptionsView.ShowAutoFilterRow = True
        Me.INDgvRequests.OptionsView.ShowDetailButtons = False
        Me.INDgvRequests.OptionsView.ShowGroupPanel = False
        Me.INDgvRequests.OptionsView.ShowIndicator = False
        Me.INDgvRequests.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.INDgvRequests_Patient, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvRequests, False)
        '
        'INDgvRequests_Patient
        '
        Me.INDgvRequests_Patient.Caption = "Paciente"
        Me.INDgvRequests_Patient.FieldName = "PatientNameAge"
        Me.INDgvRequests_Patient.Name = "INDgvRequests_Patient"
        Me.INDgvRequests_Patient.OptionsColumn.AllowEdit = False
        Me.INDgvRequests_Patient.OptionsColumn.AllowFocus = False
        Me.INDgvRequests_Patient.Visible = True
        Me.INDgvRequests_Patient.VisibleIndex = 1
        '
        'INDgvRequests_SelectOption
        '
        Me.INDgvRequests_SelectOption.Caption = " "
        Me.INDgvRequests_SelectOption.FieldName = "SelectOption"
        Me.INDgvRequests_SelectOption.ImageOptions.Alignment = System.Drawing.StringAlignment.Center
        Me.INDgvRequests_SelectOption.ImageOptions.Image = Global.Presentation.Authorization.My.Resources.Resources.undcheck
        Me.INDgvRequests_SelectOption.Name = "INDgvRequests_SelectOption"
        Me.INDgvRequests_SelectOption.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDgvRequests_SelectOption.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDgvRequests_SelectOption.OptionsColumn.AllowMove = False
        Me.INDgvRequests_SelectOption.OptionsColumn.AllowShowHide = False
        Me.INDgvRequests_SelectOption.OptionsColumn.AllowSize = False
        Me.INDgvRequests_SelectOption.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDgvRequests_SelectOption.OptionsFilter.AllowAutoFilter = False
        Me.INDgvRequests_SelectOption.OptionsFilter.AllowFilter = False
        Me.INDgvRequests_SelectOption.Visible = True
        Me.INDgvRequests_SelectOption.VisibleIndex = 0
        Me.INDgvRequests_SelectOption.Width = 41
        '
        'INDgvRequests_RequestDate
        '
        Me.INDgvRequests_RequestDate.Caption = "Fecha Solicitud"
        Me.INDgvRequests_RequestDate.FieldName = "RequestDate"
        Me.INDgvRequests_RequestDate.Name = "INDgvRequests_RequestDate"
        Me.INDgvRequests_RequestDate.OptionsColumn.AllowEdit = False
        Me.INDgvRequests_RequestDate.OptionsColumn.AllowFocus = False
        Me.INDgvRequests_RequestDate.Visible = True
        Me.INDgvRequests_RequestDate.VisibleIndex = 1
        Me.INDgvRequests_RequestDate.Width = 101
        '
        'INDgvRequests_PatientCode
        '
        Me.INDgvRequests_PatientCode.Caption = "Identificación"
        Me.INDgvRequests_PatientCode.FieldName = "PatientCode"
        Me.INDgvRequests_PatientCode.Name = "INDgvRequests_PatientCode"
        Me.INDgvRequests_PatientCode.OptionsColumn.AllowEdit = False
        Me.INDgvRequests_PatientCode.OptionsColumn.AllowFocus = False
        Me.INDgvRequests_PatientCode.Visible = True
        Me.INDgvRequests_PatientCode.VisibleIndex = 2
        Me.INDgvRequests_PatientCode.Width = 101
        '
        'INDgvRequests_AdmissionNumber
        '
        Me.INDgvRequests_AdmissionNumber.Caption = "Ingreso"
        Me.INDgvRequests_AdmissionNumber.FieldName = "AdmissionNumber"
        Me.INDgvRequests_AdmissionNumber.Name = "INDgvRequests_AdmissionNumber"
        Me.INDgvRequests_AdmissionNumber.OptionsColumn.AllowEdit = False
        Me.INDgvRequests_AdmissionNumber.OptionsColumn.AllowFocus = False
        '
        'INDgvRequests_Folio
        '
        Me.INDgvRequests_Folio.Caption = "Folio"
        Me.INDgvRequests_Folio.FieldName = "Folio"
        Me.INDgvRequests_Folio.Name = "INDgvRequests_Folio"
        Me.INDgvRequests_Folio.OptionsColumn.AllowEdit = False
        Me.INDgvRequests_Folio.OptionsColumn.AllowFocus = False
        '
        'INDgvRequests_Type
        '
        Me.INDgvRequests_Type.Caption = "Tipo"
        Me.INDgvRequests_Type.FieldName = "TypeName"
        Me.INDgvRequests_Type.Name = "INDgvRequests_Type"
        Me.INDgvRequests_Type.OptionsColumn.AllowEdit = False
        Me.INDgvRequests_Type.OptionsColumn.AllowFocus = False
        Me.INDgvRequests_Type.Width = 71
        '
        'INDgvRequests_Item
        '
        Me.INDgvRequests_Item.Caption = "Servicio"
        Me.INDgvRequests_Item.FieldName = "ItemCodeName"
        Me.INDgvRequests_Item.Name = "INDgvRequests_Item"
        Me.INDgvRequests_Item.OptionsColumn.AllowEdit = False
        Me.INDgvRequests_Item.OptionsColumn.AllowFocus = False
        Me.INDgvRequests_Item.Visible = True
        Me.INDgvRequests_Item.VisibleIndex = 6
        Me.INDgvRequests_Item.Width = 240
        '
        'INDgvRequests_Description
        '
        Me.INDgvRequests_Description.Caption = "Descripción Relacionada"
        Me.INDgvRequests_Description.FieldName = "DescriptionCodeName"
        Me.INDgvRequests_Description.Name = "INDgvRequests_Description"
        Me.INDgvRequests_Description.OptionsColumn.AllowEdit = False
        Me.INDgvRequests_Description.OptionsColumn.AllowFocus = False
        '
        'INDgvRequests_Quantity
        '
        Me.INDgvRequests_Quantity.Caption = "Cantidad"
        Me.INDgvRequests_Quantity.FieldName = "Quantity"
        Me.INDgvRequests_Quantity.Name = "INDgvRequests_Quantity"
        Me.INDgvRequests_Quantity.OptionsColumn.AllowEdit = False
        Me.INDgvRequests_Quantity.OptionsColumn.AllowFocus = False
        Me.INDgvRequests_Quantity.Visible = True
        Me.INDgvRequests_Quantity.VisibleIndex = 7
        Me.INDgvRequests_Quantity.Width = 64
        '
        'INDgvRequests_FunctionalUnit
        '
        Me.INDgvRequests_FunctionalUnit.Caption = "Unidad Funcional"
        Me.INDgvRequests_FunctionalUnit.FieldName = "FunctionalUnitCodeName"
        Me.INDgvRequests_FunctionalUnit.Name = "INDgvRequests_FunctionalUnit"
        Me.INDgvRequests_FunctionalUnit.OptionsColumn.AllowEdit = False
        Me.INDgvRequests_FunctionalUnit.OptionsColumn.AllowFocus = False
        Me.INDgvRequests_FunctionalUnit.Visible = True
        Me.INDgvRequests_FunctionalUnit.VisibleIndex = 3
        Me.INDgvRequests_FunctionalUnit.Width = 158
        '
        'INDgvRequests_CareGroup
        '
        Me.INDgvRequests_CareGroup.Caption = "Grupo Atención"
        Me.INDgvRequests_CareGroup.FieldName = "CareGroupCodeName"
        Me.INDgvRequests_CareGroup.Name = "INDgvRequests_CareGroup"
        Me.INDgvRequests_CareGroup.OptionsColumn.AllowEdit = False
        Me.INDgvRequests_CareGroup.OptionsColumn.AllowFocus = False
        Me.INDgvRequests_CareGroup.Visible = True
        Me.INDgvRequests_CareGroup.VisibleIndex = 4
        Me.INDgvRequests_CareGroup.Width = 163
        '
        'INDgvRequests_HealthAdministrator
        '
        Me.INDgvRequests_HealthAdministrator.Caption = "Entidad Responsable"
        Me.INDgvRequests_HealthAdministrator.FieldName = "HealthAdministratorCodeName"
        Me.INDgvRequests_HealthAdministrator.Name = "INDgvRequests_HealthAdministrator"
        Me.INDgvRequests_HealthAdministrator.OptionsColumn.AllowEdit = False
        Me.INDgvRequests_HealthAdministrator.OptionsColumn.AllowFocus = False
        Me.INDgvRequests_HealthAdministrator.Visible = True
        Me.INDgvRequests_HealthAdministrator.VisibleIndex = 5
        Me.INDgvRequests_HealthAdministrator.Width = 168
        '
        'INDgvRequests_CareCenter
        '
        Me.INDgvRequests_CareCenter.Caption = "Centro Atención"
        Me.INDgvRequests_CareCenter.FieldName = "CareCenterCodeName"
        Me.INDgvRequests_CareCenter.Name = "INDgvRequests_CareCenter"
        Me.INDgvRequests_CareCenter.OptionsColumn.AllowEdit = False
        Me.INDgvRequests_CareCenter.OptionsColumn.AllowFocus = False
        '
        'INDgvRequests_Covered
        '
        Me.INDgvRequests_Covered.Caption = "Cubierto"
        Me.INDgvRequests_Covered.FieldName = "Covered"
        Me.INDgvRequests_Covered.Name = "INDgvRequests_Covered"
        Me.INDgvRequests_Covered.OptionsColumn.AllowEdit = False
        Me.INDgvRequests_Covered.OptionsColumn.AllowFocus = False
        Me.INDgvRequests_Covered.Visible = True
        Me.INDgvRequests_Covered.VisibleIndex = 8
        Me.INDgvRequests_Covered.Width = 63
        '
        'INDgvRequests_Contracted
        '
        Me.INDgvRequests_Contracted.Caption = "Contratado"
        Me.INDgvRequests_Contracted.FieldName = "Contracted"
        Me.INDgvRequests_Contracted.Name = "INDgvRequests_Contracted"
        Me.INDgvRequests_Contracted.OptionsColumn.AllowEdit = False
        Me.INDgvRequests_Contracted.OptionsColumn.AllowFocus = False
        Me.INDgvRequests_Contracted.Visible = True
        Me.INDgvRequests_Contracted.VisibleIndex = 9
        Me.INDgvRequests_Contracted.Width = 62
        '
        'INDgvRequests_Quoted
        '
        Me.INDgvRequests_Quoted.Caption = "Requiere Cotización"
        Me.INDgvRequests_Quoted.FieldName = "Quoted"
        Me.INDgvRequests_Quoted.Name = "INDgvRequests_Quoted"
        Me.INDgvRequests_Quoted.OptionsColumn.AllowEdit = False
        Me.INDgvRequests_Quoted.OptionsColumn.AllowFocus = False
        Me.INDgvRequests_Quoted.Visible = True
        Me.INDgvRequests_Quoted.VisibleIndex = 10
        Me.INDgvRequests_Quoted.Width = 120
        '
        'INDgvRequests_Autorized
        '
        Me.INDgvRequests_Autorized.Caption = "Requiere Autorización"
        Me.INDgvRequests_Autorized.FieldName = "Authorized"
        Me.INDgvRequests_Autorized.Name = "INDgvRequests_Autorized"
        Me.INDgvRequests_Autorized.OptionsColumn.AllowEdit = False
        Me.INDgvRequests_Autorized.OptionsColumn.AllowFocus = False
        Me.INDgvRequests_Autorized.Visible = True
        Me.INDgvRequests_Autorized.VisibleIndex = 11
        Me.INDgvRequests_Autorized.Width = 120
        '
        'INDgvRequests_Professional
        '
        Me.INDgvRequests_Professional.Caption = "Medico Solicitante"
        Me.INDgvRequests_Professional.FieldName = "ProfessionalCodeName"
        Me.INDgvRequests_Professional.Name = "INDgvRequests_Professional"
        Me.INDgvRequests_Professional.OptionsColumn.AllowEdit = False
        Me.INDgvRequests_Professional.OptionsColumn.AllowFocus = False
        '
        'INDgvRequests_Observations
        '
        Me.INDgvRequests_Observations.Caption = "Observaciones"
        Me.INDgvRequests_Observations.FieldName = "Observations"
        Me.INDgvRequests_Observations.Name = "INDgvRequests_Observations"
        Me.INDgvRequests_Observations.OptionsColumn.AllowEdit = False
        Me.INDgvRequests_Observations.OptionsColumn.AllowFocus = False
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Grupo Autorización"
        Me.GridColumn1.FieldName = "AuthorizationGroupCodeName"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        '
        'INDSleCareCenter
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleCareCenter, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleCareCenter, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleCareCenter, False)
        Me.INDSleCareCenter.Location = New System.Drawing.Point(12, 12)
        Me.INDSleCareCenter.MaximumSize = New System.Drawing.Size(0, 60)
        Me.INDSleCareCenter.MinimumSize = New System.Drawing.Size(0, 60)
        Me.INDSleCareCenter.Name = "INDSleCareCenter"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleCareCenter, False)
        Me.INDSleCareCenter.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleCareCenter.Properties.Appearance.BackColor2 = System.Drawing.Color.Transparent
        Me.INDSleCareCenter.Properties.Appearance.BorderColor = System.Drawing.Color.Transparent
        Me.INDSleCareCenter.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 28.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleCareCenter.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleCareCenter.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleCareCenter.Properties.Appearance.Options.UseBorderColor = True
        Me.INDSleCareCenter.Properties.Appearance.Options.UseFont = True
        Me.INDSleCareCenter.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleCareCenter.Properties.Appearance.Options.UseTextOptions = True
        Me.INDSleCareCenter.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDSleCareCenter.Properties.DisplayMember = "CodeName"
        Me.INDSleCareCenter.Properties.NullText = "Seleccione un Centro de Atención"
        Me.INDSleCareCenter.Properties.PopupSizeable = False
        Me.INDSleCareCenter.Properties.PopupView = Me.INDGvCareCenter
        Me.INDSleCareCenter.Properties.ShowClearButton = False
        Me.INDSleCareCenter.Properties.ShowFooter = False
        Me.INDSleCareCenter.Properties.ValueMember = "CODCENATE"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleCareCenter, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleCareCenter, True)
        Me.INDSleCareCenter.Size = New System.Drawing.Size(1278, 60)
        Me.INDSleCareCenter.StyleController = Me.INDlyRoot
        Me.INDSleCareCenter.TabIndex = 10
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleCareCenter, Nothing)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleCareCenter, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleCareCenter, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleCareCenter, False)
        '
        'INDGvCareCenter
        '
        Me.INDGvCareCenter.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvCareCenter.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvCareCenter.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvCareCenter.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvCareCenter.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGvCareCenter.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvCareCenter.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvCareCenter.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvCareCenter.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvCareCenter.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvCareCenter.Appearance.Row.Options.UseFont = True
        Me.INDGvCareCenter.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGvCareCenter_UnboundSelection, Me.INDGvCareCenter_Code, Me.INDGvCareCenter_Name})
        Me.INDGvCareCenter.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvCareCenter.Name = "INDGvCareCenter"
        Me.INDGvCareCenter.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvCareCenter.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.INDGvCareCenter.OptionsSelection.MultiSelect = True
        Me.INDGvCareCenter.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvCareCenter.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvCareCenter.OptionsView.ShowAutoFilterRow = True
        Me.INDGvCareCenter.OptionsView.ShowDetailButtons = False
        Me.INDGvCareCenter.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvCareCenter, False)
        '
        'INDGvCareCenter_UnboundSelection
        '
        Me.INDGvCareCenter_UnboundSelection.Caption = " "
        Me.INDGvCareCenter_UnboundSelection.FieldName = "INDGvCareCenter_UnboundSelection"
        Me.INDGvCareCenter_UnboundSelection.Name = "INDGvCareCenter_UnboundSelection"
        Me.INDGvCareCenter_UnboundSelection.UnboundType = DevExpress.Data.UnboundColumnType.[Boolean]
        Me.INDGvCareCenter_UnboundSelection.Visible = True
        Me.INDGvCareCenter_UnboundSelection.VisibleIndex = 0
        Me.INDGvCareCenter_UnboundSelection.Width = 20
        '
        'INDGvCareCenter_Code
        '
        Me.INDGvCareCenter_Code.Caption = "Código"
        Me.INDGvCareCenter_Code.FieldName = "CODCENATE"
        Me.INDGvCareCenter_Code.Name = "INDGvCareCenter_Code"
        Me.INDGvCareCenter_Code.OptionsColumn.AllowEdit = False
        Me.INDGvCareCenter_Code.OptionsColumn.AllowFocus = False
        Me.INDGvCareCenter_Code.Visible = True
        Me.INDGvCareCenter_Code.VisibleIndex = 1
        Me.INDGvCareCenter_Code.Width = 122
        '
        'INDGvCareCenter_Name
        '
        Me.INDGvCareCenter_Name.Caption = "Nombre"
        Me.INDGvCareCenter_Name.FieldName = "NOMCENATE"
        Me.INDGvCareCenter_Name.Name = "INDGvCareCenter_Name"
        Me.INDGvCareCenter_Name.OptionsColumn.AllowEdit = False
        Me.INDGvCareCenter_Name.OptionsColumn.AllowFocus = False
        Me.INDGvCareCenter_Name.Visible = True
        Me.INDGvCareCenter_Name.VisibleIndex = 2
        Me.INDGvCareCenter_Name.Width = 242
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
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemOptionsMenu, Me.INDLciCareCenter, Me.INDlycgRequests})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(1366, 725)
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
        'INDLciCareCenter
        '
        Me.INDLciCareCenter.Control = Me.INDSleCareCenter
        Me.INDLciCareCenter.Location = New System.Drawing.Point(0, 0)
        Me.INDLciCareCenter.MaxSize = New System.Drawing.Size(0, 64)
        Me.INDLciCareCenter.MinSize = New System.Drawing.Size(1, 64)
        Me.INDLciCareCenter.Name = "INDLciCareCenter"
        Me.INDLciCareCenter.Size = New System.Drawing.Size(1282, 64)
        Me.INDLciCareCenter.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciCareCenter.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciCareCenter.TextVisible = False
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
        Me.INDlycgRequests.Location = New System.Drawing.Point(0, 64)
        Me.INDlycgRequests.Name = "INDlycgRequests"
        Me.INDlycgRequests.Size = New System.Drawing.Size(1346, 641)
        Me.INDlycgRequests.Text = "Solicitudes"
        Me.INDlycgRequests.TextVisible = False
        '
        'INDlyItemRequests
        '
        Me.INDlyItemRequests.Control = Me.INDgcRequests
        Me.INDlyItemRequests.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemRequests.Name = "INDlyItemRequests"
        Me.INDlyItemRequests.Size = New System.Drawing.Size(1322, 617)
        Me.INDlyItemRequests.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemRequests.TextVisible = False
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
        Me.BarManager2.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.INDBbiSelection, Me.INDBbiUnSelection, Me.INDbbiOpenRequest, Me.INDBbiConfirm, Me.INDBbiCancel, Me.INDBbiPrintWithdrawal, Me.INDBbiPrintMedicalRecord, Me.INDBbiPrintNursingRecord, Me.INDBbiPrintAccountSupport, Me.INDBbiPrintOrders, Me.INDBbiAttach})
        Me.BarManager2.MaxItemId = 18
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
        'INDBbiSelection
        '
        Me.INDBbiSelection.Caption = "Seleccionar"
        Me.INDBbiSelection.Id = 13
        Me.INDBbiSelection.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiSelection.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDBbiSelection.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiSelection.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDBbiSelection.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiSelection.ItemAppearance.Normal.Options.UseFont = True
        Me.INDBbiSelection.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiSelection.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDBbiSelection.Name = "INDBbiSelection"
        '
        'INDBbiUnSelection
        '
        Me.INDBbiUnSelection.Caption = "Deseleccionar"
        Me.INDBbiUnSelection.Id = 14
        Me.INDBbiUnSelection.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiUnSelection.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDBbiUnSelection.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiUnSelection.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDBbiUnSelection.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiUnSelection.ItemAppearance.Normal.Options.UseFont = True
        Me.INDBbiUnSelection.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiUnSelection.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDBbiUnSelection.Name = "INDBbiUnSelection"
        '
        'INDbbiOpenRequest
        '
        Me.INDbbiOpenRequest.Caption = "Control Autorización"
        Me.INDbbiOpenRequest.Id = 16
        Me.INDbbiOpenRequest.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDbbiOpenRequest.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDbbiOpenRequest.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDbbiOpenRequest.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDbbiOpenRequest.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDbbiOpenRequest.ItemAppearance.Normal.Options.UseFont = True
        Me.INDbbiOpenRequest.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDbbiOpenRequest.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDbbiOpenRequest.Name = "INDbbiOpenRequest"
        '
        'INDBbiConfirm
        '
        Me.INDBbiConfirm.Caption = "Confirmar"
        Me.INDBbiConfirm.Id = 5
        Me.INDBbiConfirm.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiConfirm.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDBbiConfirm.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiConfirm.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDBbiConfirm.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiConfirm.ItemAppearance.Normal.Options.UseFont = True
        Me.INDBbiConfirm.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiConfirm.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDBbiConfirm.Name = "INDBbiConfirm"
        '
        'INDBbiCancel
        '
        Me.INDBbiCancel.Caption = "Cancelar"
        Me.INDBbiCancel.Id = 7
        Me.INDBbiCancel.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiCancel.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDBbiCancel.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiCancel.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDBbiCancel.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiCancel.ItemAppearance.Normal.Options.UseFont = True
        Me.INDBbiCancel.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiCancel.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDBbiCancel.Name = "INDBbiCancel"
        '
        'INDBbiPrintWithdrawal
        '
        Me.INDBbiPrintWithdrawal.Caption = "Imprimir Desistimiento"
        Me.INDBbiPrintWithdrawal.Id = 15
        Me.INDBbiPrintWithdrawal.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiPrintWithdrawal.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDBbiPrintWithdrawal.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiPrintWithdrawal.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDBbiPrintWithdrawal.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiPrintWithdrawal.ItemAppearance.Normal.Options.UseFont = True
        Me.INDBbiPrintWithdrawal.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiPrintWithdrawal.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDBbiPrintWithdrawal.Name = "INDBbiPrintWithdrawal"
        '
        'INDBbiPrintMedicalRecord
        '
        Me.INDBbiPrintMedicalRecord.Caption = "Consultar / Imprimir - Reg. Médicos"
        Me.INDBbiPrintMedicalRecord.Id = 8
        Me.INDBbiPrintMedicalRecord.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiPrintMedicalRecord.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDBbiPrintMedicalRecord.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiPrintMedicalRecord.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDBbiPrintMedicalRecord.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiPrintMedicalRecord.ItemAppearance.Normal.Options.UseFont = True
        Me.INDBbiPrintMedicalRecord.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiPrintMedicalRecord.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDBbiPrintMedicalRecord.Name = "INDBbiPrintMedicalRecord"
        '
        'INDBbiPrintNursingRecord
        '
        Me.INDBbiPrintNursingRecord.Caption = "Consultar / Imprimir - Reg. Enfermería"
        Me.INDBbiPrintNursingRecord.Id = 10
        Me.INDBbiPrintNursingRecord.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiPrintNursingRecord.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDBbiPrintNursingRecord.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiPrintNursingRecord.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDBbiPrintNursingRecord.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiPrintNursingRecord.ItemAppearance.Normal.Options.UseFont = True
        Me.INDBbiPrintNursingRecord.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiPrintNursingRecord.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDBbiPrintNursingRecord.Name = "INDBbiPrintNursingRecord"
        '
        'INDBbiPrintAccountSupport
        '
        Me.INDBbiPrintAccountSupport.Caption = "Consultar / Imprimir - Soporte de Cuentas"
        Me.INDBbiPrintAccountSupport.Id = 11
        Me.INDBbiPrintAccountSupport.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiPrintAccountSupport.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDBbiPrintAccountSupport.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiPrintAccountSupport.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDBbiPrintAccountSupport.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiPrintAccountSupport.ItemAppearance.Normal.Options.UseFont = True
        Me.INDBbiPrintAccountSupport.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiPrintAccountSupport.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDBbiPrintAccountSupport.Name = "INDBbiPrintAccountSupport"
        '
        'INDBbiPrintOrders
        '
        Me.INDBbiPrintOrders.Caption = "Consultar / Imprimir - Seleccionar Ordenes"
        Me.INDBbiPrintOrders.Id = 17
        Me.INDBbiPrintOrders.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiPrintOrders.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDBbiPrintOrders.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiPrintOrders.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDBbiPrintOrders.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiPrintOrders.ItemAppearance.Normal.Options.UseFont = True
        Me.INDBbiPrintOrders.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiPrintOrders.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDBbiPrintOrders.Name = "INDBbiPrintOrders"
        '
        'INDBbiAttach
        '
        Me.INDBbiAttach.Caption = "Adjuntar"
        Me.INDBbiAttach.Id = 12
        Me.INDBbiAttach.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiAttach.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDBbiAttach.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiAttach.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDBbiAttach.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiAttach.ItemAppearance.Normal.Options.UseFont = True
        Me.INDBbiAttach.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDBbiAttach.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDBbiAttach.Name = "INDBbiAttach"
        '
        'INDPopMenuActions2
        '
        Me.INDPopMenuActions2.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.INDBbiSelection), New DevExpress.XtraBars.LinkPersistInfo(Me.INDBbiUnSelection), New DevExpress.XtraBars.LinkPersistInfo(Me.INDbbiOpenRequest), New DevExpress.XtraBars.LinkPersistInfo(Me.INDBbiConfirm), New DevExpress.XtraBars.LinkPersistInfo(Me.INDBbiCancel), New DevExpress.XtraBars.LinkPersistInfo(Me.INDBbiAttach), New DevExpress.XtraBars.LinkPersistInfo(Me.INDBbiPrintNursingRecord), New DevExpress.XtraBars.LinkPersistInfo(Me.INDBbiPrintMedicalRecord), New DevExpress.XtraBars.LinkPersistInfo(Me.INDBbiPrintAccountSupport), New DevExpress.XtraBars.LinkPersistInfo(Me.INDBbiPrintOrders), New DevExpress.XtraBars.LinkPersistInfo(Me.INDBbiPrintWithdrawal)})
        Me.INDPopMenuActions2.Manager = Me.BarManager2
        Me.INDPopMenuActions2.Name = "INDPopMenuActions2"
        '
        'FrmDashboardManagementMedicalOrder
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
        Me.Name = "FrmDashboardManagementMedicalOrder"
        Me.Opacity = 1.0R
        Me.Tag = "2177"
        Me.Text = "Dashboard Gestión Ordenes Médicas"
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
        CType(Me.BarManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPopMenuActions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyRoot.ResumeLayout(False)
        CType(Me.INDgcRequests, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvRequests, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleCareCenter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvCareCenter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemOptionsMenu, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciCareCenter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlycgRequests, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemRequests, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents BarManager As DevExpress.XtraBars.BarManager
    Friend WithEvents BarDockControl1 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl2 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl3 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl4 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents INDBarRefresh As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDPopMenuActions As DevExpress.XtraBars.PopupMenu
    Friend WithEvents INDSleCareCenter As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvCareCenter As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDGvCareCenter_Code As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvCareCenter_Name As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLciCareCenter As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDgcRequests As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvRequests As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDgvRequests_Patient As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgvRequests_RequestDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgvRequests_PatientCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgvRequests_Item As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgvRequests_Quantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgvRequests_FunctionalUnit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgvRequests_CareGroup As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgvRequests_HealthAdministrator As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgvRequests_Contracted As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgvRequests_Quoted As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgvRequests_AdmissionNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgvRequests_Covered As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgvRequests_Description As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgvRequests_Folio As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgvRequests_CareCenter As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvCareCenter_UnboundSelection As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgvRequests_Type As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgvRequests_Professional As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgvRequests_Autorized As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlycgRequests As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemRequests As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents BarDockControl5 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarManager2 As DevExpress.XtraBars.BarManager
    Friend WithEvents BarDockControl6 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl7 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl8 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents INDBbiConfirm As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDPopMenuActions2 As DevExpress.XtraBars.PopupMenu
    Friend WithEvents INDBbiCancel As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDBbiPrintMedicalRecord As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDBbiPrintNursingRecord As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDBbiPrintAccountSupport As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDBbiAttach As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDgvRequests_SelectOption As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDBbiSelection As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDBbiUnSelection As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDBbiPrintWithdrawal As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDbbiOpenRequest As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDgvRequests_Observations As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDBbiPrintOrders As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoSearchLookUpControl1 As IndigoSearchLookUpControl
End Class
