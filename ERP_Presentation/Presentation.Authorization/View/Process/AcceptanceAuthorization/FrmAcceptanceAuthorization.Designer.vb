Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmAcceptanceAuthorization
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
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.INDlyRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDbtnLoad = New DevExpress.XtraEditors.SimpleButton()
        Me.INDsleFunctionalUnit = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvFunctionalUnit = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn88 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn89 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn28 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemImageComboBox1 = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.INDsleCareCenter = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvCentAtencion = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn24 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn25 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcInformation = New DevExpress.XtraGrid.GridControl()
        Me.INDviewInformation = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemInformation = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemCareCenter = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemFunctionalUnit = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemLoad = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.INDlygAcceptance = New DevExpress.XtraLayout.LayoutControlGroup()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyRoot.SuspendLayout()
        CType(Me.INDsleFunctionalUnit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvFunctionalUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemImageComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleCareCenter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvCentAtencion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCareCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemFunctionalUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemLoad, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygAcceptance, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyRoot)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1370, 617)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1370, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1370, 98)
        '
        'INDlyRoot
        '
        Me.INDlyRoot.Controls.Add(Me.INDbtnLoad)
        Me.INDlyRoot.Controls.Add(Me.INDsleFunctionalUnit)
        Me.INDlyRoot.Controls.Add(Me.INDsleCareCenter)
        Me.INDlyRoot.Controls.Add(Me.INDgcInformation)
        Me.INDlyRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyRoot.Location = New System.Drawing.Point(2, 7)
        Me.INDlyRoot.Name = "INDlyRoot"
        Me.INDlyRoot.Root = Me.Root
        Me.INDlyRoot.Size = New System.Drawing.Size(1366, 608)
        Me.INDlyRoot.TabIndex = 0
        Me.INDlyRoot.Text = "LayoutControl1"
        '
        'INDbtnLoad
        '
        Me.INDbtnLoad.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnLoad.Appearance.Options.UseFont = True
        Me.INDbtnLoad.Location = New System.Drawing.Point(804, 59)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnLoad, True)
        Me.INDbtnLoad.Name = "INDbtnLoad"
        Me.INDbtnLoad.Size = New System.Drawing.Size(196, 28)
        Me.INDbtnLoad.StyleController = Me.INDlyRoot
        Me.INDbtnLoad.TabIndex = 14
        Me.INDbtnLoad.Text = "Cargar"
        '
        'INDsleFunctionalUnit
        '
        Me.INDsleFunctionalUnit.EnterMoveNextControl = True
        Me.INDsleFunctionalUnit.Location = New System.Drawing.Point(554, 59)
        Me.INDsleFunctionalUnit.Name = "INDsleFunctionalUnit"
        Me.INDsleFunctionalUnit.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleFunctionalUnit.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleFunctionalUnit.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleFunctionalUnit.Properties.Appearance.Options.UseFont = True
        Me.INDsleFunctionalUnit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleFunctionalUnit.Properties.DisplayMember = "CodeDescription"
        Me.INDsleFunctionalUnit.Properties.NullText = ""
        Me.INDsleFunctionalUnit.Properties.PopupSizeable = False
        Me.INDsleFunctionalUnit.Properties.PopupView = Me.INDGvFunctionalUnit
        Me.INDsleFunctionalUnit.Properties.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemImageComboBox1})
        Me.INDsleFunctionalUnit.Properties.ShowClearButton = False
        Me.INDsleFunctionalUnit.Properties.ShowFooter = False
        Me.INDsleFunctionalUnit.Properties.ValueMember = "Id"
        Me.INDsleFunctionalUnit.Size = New System.Drawing.Size(246, 28)
        Me.INDsleFunctionalUnit.StyleController = Me.INDlyRoot
        Me.INDsleFunctionalUnit.TabIndex = 13
        '
        'INDGvFunctionalUnit
        '
        Me.INDGvFunctionalUnit.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvFunctionalUnit.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvFunctionalUnit.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvFunctionalUnit.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvFunctionalUnit.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGvFunctionalUnit.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvFunctionalUnit.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvFunctionalUnit.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvFunctionalUnit.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvFunctionalUnit.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvFunctionalUnit.Appearance.Row.Options.UseFont = True
        Me.INDGvFunctionalUnit.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn88, Me.GridColumn89, Me.GridColumn28})
        Me.INDGvFunctionalUnit.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvFunctionalUnit.Name = "INDGvFunctionalUnit"
        Me.INDGvFunctionalUnit.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvFunctionalUnit.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvFunctionalUnit.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvFunctionalUnit.OptionsView.ShowAutoFilterRow = True
        Me.INDGvFunctionalUnit.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvFunctionalUnit, False)
        '
        'GridColumn88
        '
        Me.GridColumn88.Caption = "Código"
        Me.GridColumn88.FieldName = "Codigo"
        Me.GridColumn88.Name = "GridColumn88"
        Me.GridColumn88.Visible = True
        Me.GridColumn88.VisibleIndex = 0
        Me.GridColumn88.Width = 372
        '
        'GridColumn89
        '
        Me.GridColumn89.Caption = "Descripción"
        Me.GridColumn89.FieldName = "Descripcion"
        Me.GridColumn89.Name = "GridColumn89"
        Me.GridColumn89.Visible = True
        Me.GridColumn89.VisibleIndex = 1
        Me.GridColumn89.Width = 586
        '
        'GridColumn28
        '
        Me.GridColumn28.Caption = "Tipo"
        Me.GridColumn28.ColumnEdit = Me.RepositoryItemImageComboBox1
        Me.GridColumn28.FieldName = "UnitType"
        Me.GridColumn28.Name = "GridColumn28"
        Me.GridColumn28.Visible = True
        Me.GridColumn28.VisibleIndex = 2
        Me.GridColumn28.Width = 434
        '
        'RepositoryItemImageComboBox1
        '
        Me.RepositoryItemImageComboBox1.AutoHeight = False
        Me.RepositoryItemImageComboBox1.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Apoyo Dx", CType(3, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Laboratorio", CType(20, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Cardiologia No Invasiva", CType(21, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Consulta Externa - Gineco-Obstetricia", CType(24, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Consulta Externa", CType(15, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Cardiologia Invasiva", CType(22, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Cirugia", CType(19, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Urgencias", CType(1, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Gineco-Obstetricia", CType(23, Byte), -1)})
        Me.RepositoryItemImageComboBox1.Name = "RepositoryItemImageComboBox1"
        '
        'INDsleCareCenter
        '
        Me.INDsleCareCenter.EnterMoveNextControl = True
        Me.INDsleCareCenter.Location = New System.Drawing.Point(164, 59)
        Me.INDsleCareCenter.Name = "INDsleCareCenter"
        Me.INDsleCareCenter.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleCareCenter.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleCareCenter.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleCareCenter.Properties.Appearance.Options.UseFont = True
        Me.INDsleCareCenter.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleCareCenter.Properties.DisplayMember = "CodeName"
        Me.INDsleCareCenter.Properties.NullText = ""
        Me.INDsleCareCenter.Properties.PopupSizeable = False
        Me.INDsleCareCenter.Properties.PopupView = Me.INDGvCentAtencion
        Me.INDsleCareCenter.Properties.ShowClearButton = False
        Me.INDsleCareCenter.Properties.ShowFooter = False
        Me.INDsleCareCenter.Properties.ValueMember = "CODCENATE"
        Me.INDsleCareCenter.Size = New System.Drawing.Size(246, 28)
        Me.INDsleCareCenter.StyleController = Me.INDlyRoot
        Me.INDsleCareCenter.TabIndex = 12
        '
        'INDGvCentAtencion
        '
        Me.INDGvCentAtencion.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvCentAtencion.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvCentAtencion.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvCentAtencion.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvCentAtencion.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGvCentAtencion.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvCentAtencion.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvCentAtencion.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvCentAtencion.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvCentAtencion.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvCentAtencion.Appearance.Row.Options.UseFont = True
        Me.INDGvCentAtencion.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn24, Me.GridColumn25})
        Me.INDGvCentAtencion.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvCentAtencion.Name = "INDGvCentAtencion"
        Me.INDGvCentAtencion.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvCentAtencion.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvCentAtencion.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvCentAtencion.OptionsView.ShowAutoFilterRow = True
        Me.INDGvCentAtencion.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvCentAtencion, False)
        '
        'GridColumn24
        '
        Me.GridColumn24.Caption = "Código"
        Me.GridColumn24.FieldName = "CODCENATE"
        Me.GridColumn24.Name = "GridColumn24"
        Me.GridColumn24.Visible = True
        Me.GridColumn24.VisibleIndex = 0
        Me.GridColumn24.Width = 143
        '
        'GridColumn25
        '
        Me.GridColumn25.Caption = "Nombre"
        Me.GridColumn25.FieldName = "NOMCENATE"
        Me.GridColumn25.Name = "GridColumn25"
        Me.GridColumn25.Visible = True
        Me.GridColumn25.VisibleIndex = 1
        Me.GridColumn25.Width = 277
        '
        'INDgcInformation
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcInformation, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcInformation, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcInformation, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcInformation, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcInformation, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcInformation, False)
        Me.INDgcInformation.Location = New System.Drawing.Point(24, 95)
        Me.INDgcInformation.MainView = Me.INDviewInformation
        Me.INDgcInformation.Name = "INDgcInformation"
        Me.INDgcInformation.Size = New System.Drawing.Size(1318, 489)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcInformation, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcInformation.TabIndex = 11
        Me.INDgcInformation.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDviewInformation})
        '
        'INDviewInformation
        '
        Me.INDviewInformation.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewInformation.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewInformation.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewInformation.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewInformation.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewInformation.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewInformation.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewInformation.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewInformation.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewInformation.Appearance.Row.Options.UseFont = True
        Me.INDviewInformation.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDviewInformation.Appearance.ViewCaption.Options.UseFont = True
        Me.INDviewInformation.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn9, Me.GridColumn1, Me.GridColumn2, Me.GridColumn8, Me.GridColumn10})
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
        Me.INDviewInformation.FormatRules.Add(GridFormatRule1)
        Me.INDviewInformation.FormatRules.Add(GridFormatRule2)
        Me.INDviewInformation.FormatRules.Add(GridFormatRule3)
        Me.INDviewInformation.GridControl = Me.INDgcInformation
        Me.INDviewInformation.Name = "INDviewInformation"
        Me.INDviewInformation.OptionsBehavior.AutoExpandAllGroups = True
        Me.INDviewInformation.OptionsSelection.CheckBoxSelectorColumnWidth = 30
        Me.INDviewInformation.OptionsSelection.MultiSelect = True
        Me.INDviewInformation.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect
        Me.INDviewInformation.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewInformation.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewInformation.OptionsView.ShowAutoFilterRow = True
        Me.INDviewInformation.OptionsView.ShowDetailButtons = False
        Me.INDviewInformation.OptionsView.ShowGroupPanel = False
        Me.INDviewInformation.OptionsView.ShowIndicator = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewInformation, False)
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Ingreso"
        Me.GridColumn9.FieldName = "AdmissionNumber"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.OptionsColumn.AllowEdit = False
        Me.GridColumn9.OptionsColumn.AllowFocus = False
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 1
        Me.GridColumn9.Width = 114
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Paciente"
        Me.GridColumn1.FieldName = "PatientCodeName"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 2
        Me.GridColumn1.Width = 270
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Servicio"
        Me.GridColumn2.FieldName = "ServiceCodeName"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 3
        Me.GridColumn2.Width = 361
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Unidad Funcional Origen"
        Me.GridColumn8.FieldName = "FunctionalUnitCodeName"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.OptionsColumn.AllowEdit = False
        Me.GridColumn8.OptionsColumn.AllowFocus = False
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 4
        Me.GridColumn8.Width = 278
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "Unidad Funcional Destino"
        Me.GridColumn10.FieldName = "FunctionalUnitTargetCodeName"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.OptionsColumn.AllowEdit = False
        Me.GridColumn10.OptionsColumn.AllowFocus = False
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 5
        Me.GridColumn10.Width = 287
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
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlygAcceptance})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(1366, 608)
        Me.Root.TextVisible = False
        '
        'INDlyItemInformation
        '
        Me.INDlyItemInformation.Control = Me.INDgcInformation
        Me.INDlyItemInformation.Location = New System.Drawing.Point(0, 36)
        Me.INDlyItemInformation.Name = "INDlyItemInformation"
        Me.INDlyItemInformation.Size = New System.Drawing.Size(1322, 493)
        Me.INDlyItemInformation.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemInformation.TextVisible = False
        '
        'INDlyItemCareCenter
        '
        Me.INDlyItemCareCenter.Control = Me.INDsleCareCenter
        Me.INDlyItemCareCenter.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCareCenter.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemCareCenter.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemCareCenter.Name = "INDlyItemCareCenter"
        Me.INDlyItemCareCenter.Size = New System.Drawing.Size(390, 36)
        Me.INDlyItemCareCenter.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCareCenter.Text = "Centro Atención"
        Me.INDlyItemCareCenter.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCareCenter.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemCareCenter.TextToControlDistance = 5
        '
        'INDlyItemFunctionalUnit
        '
        Me.INDlyItemFunctionalUnit.Control = Me.INDsleFunctionalUnit
        Me.INDlyItemFunctionalUnit.Location = New System.Drawing.Point(390, 0)
        Me.INDlyItemFunctionalUnit.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemFunctionalUnit.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemFunctionalUnit.Name = "INDlyItemFunctionalUnit"
        Me.INDlyItemFunctionalUnit.Size = New System.Drawing.Size(390, 36)
        Me.INDlyItemFunctionalUnit.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemFunctionalUnit.Text = "Unidad Funcional"
        Me.INDlyItemFunctionalUnit.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemFunctionalUnit.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemFunctionalUnit.TextToControlDistance = 5
        '
        'INDlyItemLoad
        '
        Me.INDlyItemLoad.Control = Me.INDbtnLoad
        Me.INDlyItemLoad.Location = New System.Drawing.Point(780, 0)
        Me.INDlyItemLoad.MaxSize = New System.Drawing.Size(200, 32)
        Me.INDlyItemLoad.MinSize = New System.Drawing.Size(200, 32)
        Me.INDlyItemLoad.Name = "INDlyItemLoad"
        Me.INDlyItemLoad.Size = New System.Drawing.Size(542, 36)
        Me.INDlyItemLoad.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemLoad.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemLoad.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'INDlygAcceptance
        '
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygAcceptance, False)
        Me.INDlygAcceptance.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemInformation, Me.INDlyItemCareCenter, Me.INDlyItemFunctionalUnit, Me.INDlyItemLoad})
        Me.INDlygAcceptance.Location = New System.Drawing.Point(0, 0)
        Me.INDlygAcceptance.Name = "INDlygAcceptance"
        Me.INDlygAcceptance.Size = New System.Drawing.Size(1346, 588)
        Me.INDlygAcceptance.Text = "Aceptación"
        '
        'FrmAcceptanceAuthorization
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1370, 739)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmAcceptanceAuthorization"
        Me.Opacity = 1.0R
        Me.Tag = "2182"
        Me.Text = "Aceptación Autorización"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyRoot.ResumeLayout(False)
        CType(Me.INDsleFunctionalUnit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvFunctionalUnit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemImageComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleCareCenter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvCentAtencion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcInformation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewInformation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemInformation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCareCenter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemFunctionalUnit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemLoad, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygAcceptance, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents IndigoGroupControl1 As IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As IndigoLabelControl
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents INDlyRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDgcInformation As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDviewInformation As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoSimpleButton1 As IndigoSimpleButton
    Friend WithEvents INDlyItemInformation As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsleCareCenter As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvCentAtencion As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn24 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn25 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlyItemCareCenter As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsleFunctionalUnit As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvFunctionalUnit As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn88 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn89 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn28 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemImageComboBox1 As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents INDlyItemFunctionalUnit As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDbtnLoad As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDlyItemLoad As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlygAcceptance As DevExpress.XtraLayout.LayoutControlGroup
End Class
