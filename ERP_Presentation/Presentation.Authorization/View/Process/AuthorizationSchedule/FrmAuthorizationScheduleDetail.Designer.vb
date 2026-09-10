Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmAuthorizationScheduleDetail
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
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.INDtxtScheduleTemplate = New DevExpress.XtraEditors.TextEdit()
        Me.INDlyRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDpopupEdit = New DevExpress.XtraEditors.PopupContainerControl()
        Me.INDlyRootEdit = New DevExpress.XtraLayout.LayoutControl()
        Me.INDsleNoveltyType = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit2View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleType = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcheckNextDay = New DevExpress.XtraEditors.CheckEdit()
        Me.INDdteEndingTime = New DevExpress.XtraEditors.TimeEdit()
        Me.INDdteInitialTime = New DevExpress.XtraEditors.TimeEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemInitialTime = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemEndingTime = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemNoveltyType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.INDbtnEdit = New DevExpress.XtraEditors.SimpleButton()
        Me.INDgcHours = New DevExpress.XtraGrid.GridControl()
        Me.INDviewHours = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepIcbeType = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepPceEdit = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDlbUserName = New DevExpress.XtraEditors.LabelControl()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemUserName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemScheduleTemplate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemHoursAndEvents = New DevExpress.XtraLayout.LayoutControlItem()
        Me.SchedulerStorage11 = New DevExpress.XtraScheduler.SchedulerStorage(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoPopUpContainerEdit1 = New Presentation.Controls.IndigoPopUpContainerEdit(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoCheckEdit1 = New Presentation.Controls.IndigoCheckEdit(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtScheduleTemplate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyRoot.SuspendLayout()
        CType(Me.INDpopupEdit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpopupEdit.SuspendLayout()
        CType(Me.INDlyRootEdit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyRootEdit.SuspendLayout()
        CType(Me.INDsleNoveltyType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit2View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDcheckNextDay.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteEndingTime.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteInitialTime.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemInitialTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemEndingTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemNoveltyType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.INDgcHours, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewHours, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepIcbeType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepPceEdit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemUserName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemScheduleTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemHoursAndEvents, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SchedulerStorage11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyRoot)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1026, 599)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1026, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1026, 98)
        '
        'INDtxtScheduleTemplate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtScheduleTemplate, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtScheduleTemplate, False)
        Me.INDtxtScheduleTemplate.Location = New System.Drawing.Point(152, 52)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtScheduleTemplate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtScheduleTemplate.Name = "INDtxtScheduleTemplate"
        Me.INDtxtScheduleTemplate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtScheduleTemplate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtScheduleTemplate.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtScheduleTemplate.Properties.Appearance.Options.UseFont = True
        Me.INDtxtScheduleTemplate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtScheduleTemplate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtScheduleTemplate.Properties.ReadOnly = True
        Me.INDtxtScheduleTemplate.Size = New System.Drawing.Size(858, 28)
        Me.INDtxtScheduleTemplate.StyleController = Me.INDlyRoot
        Me.INDtxtScheduleTemplate.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtScheduleTemplate, 0)
        '
        'INDlyRoot
        '
        Me.INDlyRoot.Controls.Add(Me.INDpopupEdit)
        Me.INDlyRoot.Controls.Add(Me.INDgcHours)
        Me.INDlyRoot.Controls.Add(Me.INDlbUserName)
        Me.INDlyRoot.Controls.Add(Me.INDtxtScheduleTemplate)
        Me.INDlyRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyRoot.Location = New System.Drawing.Point(2, 7)
        Me.INDlyRoot.Name = "INDlyRoot"
        Me.INDlyRoot.Root = Me.Root
        Me.INDlyRoot.Size = New System.Drawing.Size(1022, 590)
        Me.INDlyRoot.TabIndex = 0
        Me.INDlyRoot.Text = "LayoutControl1"
        '
        'INDpopupEdit
        '
        Me.INDpopupEdit.Controls.Add(Me.INDlyRootEdit)
        Me.INDpopupEdit.Controls.Add(Me.PanelControl1)
        Me.INDpopupEdit.Location = New System.Drawing.Point(137, 195)
        Me.INDpopupEdit.Name = "INDpopupEdit"
        Me.INDpopupEdit.Size = New System.Drawing.Size(276, 346)
        Me.INDpopupEdit.TabIndex = 7
        '
        'INDlyRootEdit
        '
        Me.INDlyRootEdit.Controls.Add(Me.INDsleNoveltyType)
        Me.INDlyRootEdit.Controls.Add(Me.INDsleType)
        Me.INDlyRootEdit.Controls.Add(Me.INDcheckNextDay)
        Me.INDlyRootEdit.Controls.Add(Me.INDdteEndingTime)
        Me.INDlyRootEdit.Controls.Add(Me.INDdteInitialTime)
        Me.INDlyRootEdit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyRootEdit.Location = New System.Drawing.Point(0, 0)
        Me.INDlyRootEdit.Name = "INDlyRootEdit"
        Me.INDlyRootEdit.Root = Me.LayoutControlGroup1
        Me.INDlyRootEdit.Size = New System.Drawing.Size(276, 306)
        Me.INDlyRootEdit.TabIndex = 0
        Me.INDlyRootEdit.Text = "LayoutControl1"
        '
        'INDsleNoveltyType
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleNoveltyType, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleNoveltyType, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleNoveltyType, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleNoveltyType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleNoveltyType, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleNoveltyType, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleNoveltyType, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleNoveltyType, False)
        Me.INDsleNoveltyType.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleNoveltyType, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleNoveltyType, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleNoveltyType, False)
        Me.INDsleNoveltyType.Location = New System.Drawing.Point(12, 98)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleNoveltyType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleNoveltyType.Name = "INDsleNoveltyType"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleNoveltyType, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleNoveltyType, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleNoveltyType, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleNoveltyType, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleNoveltyType, False)
        Me.INDsleNoveltyType.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleNoveltyType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleNoveltyType.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleNoveltyType.Properties.Appearance.Options.UseFont = True
        Me.INDsleNoveltyType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleNoveltyType.Properties.DisplayMember = "Item2"
        Me.INDsleNoveltyType.Properties.NullText = ""
        Me.INDsleNoveltyType.Properties.PopupSizeable = False
        Me.INDsleNoveltyType.Properties.PopupView = Me.SearchLookUpEdit2View
        Me.INDsleNoveltyType.Properties.ShowFooter = False
        Me.INDsleNoveltyType.Properties.ValueMember = "Item1"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleNoveltyType, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleNoveltyType, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleNoveltyType, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleNoveltyType, True)
        Me.INDsleNoveltyType.Size = New System.Drawing.Size(246, 28)
        Me.INDsleNoveltyType.StyleController = Me.INDlyRootEdit
        Me.INDsleNoveltyType.TabIndex = 1
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleNoveltyType, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleNoveltyType, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleNoveltyType, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleNoveltyType, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleNoveltyType, False)
        '
        'SearchLookUpEdit2View
        '
        Me.SearchLookUpEdit2View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit2View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit2View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit2View.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit2View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit2View.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit2View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit2View.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit2View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit2View.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit2View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn6})
        Me.SearchLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit2View.Name = "SearchLookUpEdit2View"
        Me.SearchLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit2View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit2View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit2View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit2View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit2View, False)
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Descripción"
        Me.GridColumn6.FieldName = "Item2"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 0
        '
        'INDsleType
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleType, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleType, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleType, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleType, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleType, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleType, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleType, False)
        Me.INDsleType.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleType, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleType, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleType, False)
        Me.INDsleType.Location = New System.Drawing.Point(12, 38)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleType.Name = "INDsleType"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleType, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleType, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleType, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleType, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleType, False)
        Me.INDsleType.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleType.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleType.Properties.Appearance.Options.UseFont = True
        Me.INDsleType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleType.Properties.DisplayMember = "Item2"
        Me.INDsleType.Properties.NullText = ""
        Me.INDsleType.Properties.PopupSizeable = False
        Me.INDsleType.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDsleType.Properties.ShowFooter = False
        Me.INDsleType.Properties.ValueMember = "Item1"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleType, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleType, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleType, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleType, True)
        Me.INDsleType.Size = New System.Drawing.Size(246, 28)
        Me.INDsleType.StyleController = Me.INDlyRootEdit
        Me.INDsleType.TabIndex = 0
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleType, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleType, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleType, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleType, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleType, False)
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
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn7})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Descripción"
        Me.GridColumn7.FieldName = "Item2"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 0
        '
        'INDcheckNextDay
        '
        Me.IndigoCheckEdit1.SetCampoObligatorio(Me.INDcheckNextDay, False)
        Me.INDcheckNextDay.EnterMoveNextControl = True
        Me.INDcheckNextDay.Location = New System.Drawing.Point(12, 252)
        Me.INDcheckNextDay.Name = "INDcheckNextDay"
        Me.INDcheckNextDay.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDcheckNextDay.Properties.Appearance.Options.UseFont = True
        Me.INDcheckNextDay.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDcheckNextDay.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDcheckNextDay.Properties.Caption = "Siguiente Día"
        Me.INDcheckNextDay.Size = New System.Drawing.Size(252, 25)
        Me.INDcheckNextDay.StyleController = Me.INDlyRootEdit
        Me.INDcheckNextDay.TabIndex = 4
        '
        'INDdteEndingTime
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdteEndingTime, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdteEndingTime, False)
        Me.INDdteEndingTime.EditValue = New Date(2020, 5, 27, 0, 0, 0, 0)
        Me.INDdteEndingTime.EnterMoveNextControl = True
        Me.INDdteEndingTime.Location = New System.Drawing.Point(12, 218)
        Me.IndigoTextEdit1.SetMascara(Me.INDdteEndingTime, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDdteEndingTime.Name = "INDdteEndingTime"
        Me.INDdteEndingTime.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDdteEndingTime.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteEndingTime.Properties.Appearance.Options.UseBackColor = True
        Me.INDdteEndingTime.Properties.Appearance.Options.UseFont = True
        Me.INDdteEndingTime.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteEndingTime.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdteEndingTime.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteEndingTime.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdteEndingTime.Size = New System.Drawing.Size(246, 28)
        Me.INDdteEndingTime.StyleController = Me.INDlyRootEdit
        Me.INDdteEndingTime.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdteEndingTime, 0)
        '
        'INDdteInitialTime
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdteInitialTime, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdteInitialTime, False)
        Me.INDdteInitialTime.EditValue = New Date(2020, 5, 27, 0, 0, 0, 0)
        Me.INDdteInitialTime.EnterMoveNextControl = True
        Me.INDdteInitialTime.Location = New System.Drawing.Point(12, 158)
        Me.IndigoTextEdit1.SetMascara(Me.INDdteInitialTime, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDdteInitialTime.Name = "INDdteInitialTime"
        Me.INDdteInitialTime.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDdteInitialTime.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteInitialTime.Properties.Appearance.Options.UseBackColor = True
        Me.INDdteInitialTime.Properties.Appearance.Options.UseFont = True
        Me.INDdteInitialTime.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteInitialTime.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdteInitialTime.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteInitialTime.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdteInitialTime.Size = New System.Drawing.Size(246, 28)
        Me.INDdteInitialTime.StyleController = Me.INDlyRootEdit
        Me.INDdteInitialTime.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdteInitialTime, 0)
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
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemInitialTime, Me.INDlyItemEndingTime, Me.LayoutControlItem3, Me.INDlyItemType, Me.INDlyItemNoveltyType})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(276, 306)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlyItemInitialTime
        '
        Me.INDlyItemInitialTime.Control = Me.INDdteInitialTime
        Me.INDlyItemInitialTime.Location = New System.Drawing.Point(0, 120)
        Me.INDlyItemInitialTime.MaxSize = New System.Drawing.Size(250, 60)
        Me.INDlyItemInitialTime.MinSize = New System.Drawing.Size(250, 60)
        Me.INDlyItemInitialTime.Name = "INDlyItemInitialTime"
        Me.INDlyItemInitialTime.Size = New System.Drawing.Size(256, 60)
        Me.INDlyItemInitialTime.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemInitialTime.Text = "Hora Inicial"
        Me.INDlyItemInitialTime.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemInitialTime.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemInitialTime.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemInitialTime.TextToControlDistance = 5
        '
        'INDlyItemEndingTime
        '
        Me.INDlyItemEndingTime.Control = Me.INDdteEndingTime
        Me.INDlyItemEndingTime.Location = New System.Drawing.Point(0, 180)
        Me.INDlyItemEndingTime.MaxSize = New System.Drawing.Size(250, 60)
        Me.INDlyItemEndingTime.MinSize = New System.Drawing.Size(250, 60)
        Me.INDlyItemEndingTime.Name = "INDlyItemEndingTime"
        Me.INDlyItemEndingTime.Size = New System.Drawing.Size(256, 60)
        Me.INDlyItemEndingTime.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemEndingTime.Text = "Hora Final"
        Me.INDlyItemEndingTime.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemEndingTime.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemEndingTime.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemEndingTime.TextToControlDistance = 5
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.INDcheckNextDay
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 240)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(256, 46)
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = False
        '
        'INDlyItemType
        '
        Me.INDlyItemType.Control = Me.INDsleType
        Me.INDlyItemType.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemType.MaxSize = New System.Drawing.Size(250, 60)
        Me.INDlyItemType.MinSize = New System.Drawing.Size(250, 60)
        Me.INDlyItemType.Name = "INDlyItemType"
        Me.INDlyItemType.Size = New System.Drawing.Size(256, 60)
        Me.INDlyItemType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemType.Text = "Tipo"
        Me.INDlyItemType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemType.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemType.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemType.TextToControlDistance = 5
        '
        'INDlyItemNoveltyType
        '
        Me.INDlyItemNoveltyType.Control = Me.INDsleNoveltyType
        Me.INDlyItemNoveltyType.Location = New System.Drawing.Point(0, 60)
        Me.INDlyItemNoveltyType.MaxSize = New System.Drawing.Size(250, 60)
        Me.INDlyItemNoveltyType.MinSize = New System.Drawing.Size(250, 60)
        Me.INDlyItemNoveltyType.Name = "INDlyItemNoveltyType"
        Me.INDlyItemNoveltyType.Size = New System.Drawing.Size(256, 60)
        Me.INDlyItemNoveltyType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemNoveltyType.Text = "Tipo Novedad"
        Me.INDlyItemNoveltyType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemNoveltyType.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemNoveltyType.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemNoveltyType.TextToControlDistance = 5
        Me.INDlyItemNoveltyType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.INDbtnEdit)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(0, 306)
        Me.PanelControl1.MaximumSize = New System.Drawing.Size(0, 40)
        Me.PanelControl1.MinimumSize = New System.Drawing.Size(0, 40)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(276, 40)
        Me.PanelControl1.TabIndex = 1
        '
        'INDbtnEdit
        '
        Me.INDbtnEdit.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnEdit.Appearance.Options.UseFont = True
        Me.INDbtnEdit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDbtnEdit.Location = New System.Drawing.Point(2, 2)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnEdit, True)
        Me.INDbtnEdit.Name = "INDbtnEdit"
        Me.INDbtnEdit.Size = New System.Drawing.Size(272, 36)
        Me.INDbtnEdit.TabIndex = 0
        Me.INDbtnEdit.Text = "Editar"
        '
        'INDgcHours
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcHours, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcHours, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcHours, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcHours, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcHours, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcHours, False)
        Me.INDgcHours.Location = New System.Drawing.Point(12, 88)
        Me.INDgcHours.MainView = Me.INDviewHours
        Me.INDgcHours.Name = "INDgcHours"
        Me.INDgcHours.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrepIcbeType, Me.INDrepPceEdit})
        Me.INDgcHours.Size = New System.Drawing.Size(998, 490)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcHours, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcHours.TabIndex = 6
        Me.INDgcHours.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDviewHours})
        '
        'INDviewHours
        '
        Me.INDviewHours.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewHours.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewHours.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewHours.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewHours.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewHours.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewHours.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewHours.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewHours.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewHours.Appearance.Row.Options.UseFont = True
        Me.INDviewHours.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDviewHours.Appearance.ViewCaption.Options.UseFont = True
        Me.INDviewHours.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5})
        Me.INDviewHours.GridControl = Me.INDgcHours
        Me.INDviewHours.Name = "INDviewHours"
        Me.INDviewHours.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewHours.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewHours.OptionsView.ShowAutoFilterRow = True
        Me.INDviewHours.OptionsView.ShowDetailButtons = False
        Me.INDviewHours.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewHours, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Hora Inicio"
        Me.GridColumn1.DisplayFormat.FormatString = "T"
        Me.GridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn1.FieldName = "InitialTime"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 253
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Hora Fin"
        Me.GridColumn2.DisplayFormat.FormatString = "T"
        Me.GridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn2.FieldName = "EndingTime"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 215
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Siguiente Día"
        Me.GridColumn3.FieldName = "NextDay"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        Me.GridColumn3.Width = 110
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Tipo"
        Me.GridColumn4.ColumnEdit = Me.INDrepIcbeType
        Me.GridColumn4.FieldName = "Type"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 3
        Me.GridColumn4.Width = 253
        '
        'INDrepIcbeType
        '
        Me.INDrepIcbeType.AutoHeight = False
        Me.INDrepIcbeType.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrepIcbeType.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Turno", CType(1, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Evento", CType(2, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Novedad", CType(3, Byte), -1)})
        Me.INDrepIcbeType.Name = "INDrepIcbeType"
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Editar"
        Me.GridColumn5.ColumnEdit = Me.INDrepPceEdit
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 4
        Me.GridColumn5.Width = 112
        '
        'INDrepPceEdit
        '
        Me.INDrepPceEdit.AutoHeight = False
        Me.INDrepPceEdit.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrepPceEdit.Name = "INDrepPceEdit"
        Me.INDrepPceEdit.PopupControl = Me.INDpopupEdit
        Me.INDrepPceEdit.PopupSizeable = False
        Me.INDrepPceEdit.ShowPopupCloseButton = False
        Me.INDrepPceEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'INDlbUserName
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDlbUserName, True)
        Me.INDlbUserName.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(233, Byte), Integer))
        Me.INDlbUserName.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlbUserName.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDlbUserName.Appearance.Options.UseBackColor = True
        Me.INDlbUserName.Appearance.Options.UseFont = True
        Me.INDlbUserName.Appearance.Options.UseForeColor = True
        Me.INDlbUserName.Appearance.Options.UseTextOptions = True
        Me.INDlbUserName.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDlbUserName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDlbUserName, False)
        Me.INDlbUserName.Location = New System.Drawing.Point(12, 12)
        Me.INDlbUserName.Name = "INDlbUserName"
        Me.INDlbUserName.Size = New System.Drawing.Size(998, 36)
        Me.INDlbUserName.StyleController = Me.INDlyRoot
        Me.INDlbUserName.TabIndex = 4
        Me.INDlbUserName.Text = "NOMBRE USUARIO"
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
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemUserName, Me.INDlyItemScheduleTemplate, Me.INDlyItemHoursAndEvents})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(1022, 590)
        Me.Root.TextVisible = False
        '
        'INDlyItemUserName
        '
        Me.INDlyItemUserName.Control = Me.INDlbUserName
        Me.INDlyItemUserName.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemUserName.MaxSize = New System.Drawing.Size(0, 40)
        Me.INDlyItemUserName.MinSize = New System.Drawing.Size(1, 40)
        Me.INDlyItemUserName.Name = "INDlyItemUserName"
        Me.INDlyItemUserName.Size = New System.Drawing.Size(1002, 40)
        Me.INDlyItemUserName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemUserName.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemUserName.TextVisible = False
        '
        'INDlyItemScheduleTemplate
        '
        Me.INDlyItemScheduleTemplate.Control = Me.INDtxtScheduleTemplate
        Me.INDlyItemScheduleTemplate.Location = New System.Drawing.Point(0, 40)
        Me.INDlyItemScheduleTemplate.MaxSize = New System.Drawing.Size(0, 36)
        Me.INDlyItemScheduleTemplate.MinSize = New System.Drawing.Size(1, 36)
        Me.INDlyItemScheduleTemplate.Name = "INDlyItemScheduleTemplate"
        Me.INDlyItemScheduleTemplate.Size = New System.Drawing.Size(1002, 36)
        Me.INDlyItemScheduleTemplate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemScheduleTemplate.Text = "Plantilla de Turnos"
        Me.INDlyItemScheduleTemplate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemScheduleTemplate.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemScheduleTemplate.TextToControlDistance = 5
        '
        'INDlyItemHoursAndEvents
        '
        Me.INDlyItemHoursAndEvents.Control = Me.INDgcHours
        Me.INDlyItemHoursAndEvents.Location = New System.Drawing.Point(0, 76)
        Me.INDlyItemHoursAndEvents.MinSize = New System.Drawing.Size(1, 1)
        Me.INDlyItemHoursAndEvents.Name = "INDlyItemHoursAndEvents"
        Me.INDlyItemHoursAndEvents.Size = New System.Drawing.Size(1002, 494)
        Me.INDlyItemHoursAndEvents.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemHoursAndEvents.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemHoursAndEvents.TextVisible = False
        '
        'SchedulerStorage11
        '
        Me.SchedulerStorage11.Appointments.Labels.Add(System.Drawing.SystemColors.Window, "None", "&None")
        Me.SchedulerStorage11.Appointments.Labels.Add(System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(190, Byte), Integer)), "Important", "&Important")
        Me.SchedulerStorage11.Appointments.Labels.Add(System.Drawing.Color.FromArgb(CType(CType(168, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(255, Byte), Integer)), "Business", "&Business")
        Me.SchedulerStorage11.Appointments.Labels.Add(System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(156, Byte), Integer)), "Personal", "&Personal")
        Me.SchedulerStorage11.Appointments.Labels.Add(System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(199, Byte), Integer)), "Vacation", "&Vacation")
        Me.SchedulerStorage11.Appointments.Labels.Add(System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(206, Byte), Integer), CType(CType(147, Byte), Integer)), "Must Attend", "Must &Attend")
        Me.SchedulerStorage11.Appointments.Labels.Add(System.Drawing.Color.FromArgb(CType(CType(199, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(255, Byte), Integer)), "Travel Required", "&Travel Required")
        Me.SchedulerStorage11.Appointments.Labels.Add(System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(152, Byte), Integer)), "Needs Preparation", "&Needs Preparation")
        Me.SchedulerStorage11.Appointments.Labels.Add(System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(233, Byte), Integer)), "Birthday", "&Birthday")
        Me.SchedulerStorage11.Appointments.Labels.Add(System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(223, Byte), Integer)), "Anniversary", "&Anniversary")
        Me.SchedulerStorage11.Appointments.Labels.Add(System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(165, Byte), Integer)), "Telefono", "Phone &Call")
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'FrmAuthorizationScheduleDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1026, 721)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmAuthorizationScheduleDetail"
        Me.Opacity = 1.0R
        Me.Tag = "2174"
        Me.Text = "Plantilla de Horarios"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtScheduleTemplate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyRoot.ResumeLayout(False)
        CType(Me.INDpopupEdit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpopupEdit.ResumeLayout(False)
        CType(Me.INDlyRootEdit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyRootEdit.ResumeLayout(False)
        CType(Me.INDsleNoveltyType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit2View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDcheckNextDay.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteEndingTime.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteInitialTime.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemInitialTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemEndingTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemNoveltyType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.INDgcHours, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewHours, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepIcbeType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepPceEdit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemUserName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemScheduleTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemHoursAndEvents, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SchedulerStorage11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents IndigoTextEdit1 As IndigoTextEdit
    Friend WithEvents IndigoSimpleButton1 As IndigoSimpleButton
    Friend WithEvents IndigoSearchLookUpControl1 As IndigoSearchLookUpControl
    Friend WithEvents IndigoPopUpContainerEdit1 As IndigoPopUpContainerEdit
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents IndigoLabelControl1 As IndigoLabelControl
    Friend WithEvents IndigoGroupControl1 As IndigoGroupControl
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents IndigoDate1 As IndigoDate
    Friend WithEvents INDlyRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents SchedulerStorage11 As DevExpress.XtraScheduler.SchedulerStorage
    Friend WithEvents INDlbUserName As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlyItemUserName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemScheduleTemplate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDgcHours As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDviewHours As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItemHoursAndEvents As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDtxtScheduleTemplate As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDrepIcbeType As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepPceEdit As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDpopupEdit As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents INDlyRootEdit As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDbtnEdit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDsleNoveltyType As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit2View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDsleType As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDcheckNextDay As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents INDdteEndingTime As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents INDdteInitialTime As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents INDlyItemInitialTime As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemEndingTime As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemNoveltyType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoCheckEdit1 As IndigoCheckEdit
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
End Class
