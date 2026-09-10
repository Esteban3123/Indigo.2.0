Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmManageRawMaterial
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
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim GridFormatRule1 As DevExpress.XtraGrid.GridFormatRule = New DevExpress.XtraGrid.GridFormatRule()
        Dim FormatConditionRuleValue1 As DevExpress.XtraEditors.FormatConditionRuleValue = New DevExpress.XtraEditors.FormatConditionRuleValue()
        Dim GridFormatRule2 As DevExpress.XtraGrid.GridFormatRule = New DevExpress.XtraGrid.GridFormatRule()
        Dim FormatConditionRuleValue2 As DevExpress.XtraEditors.FormatConditionRuleValue = New DevExpress.XtraEditors.FormatConditionRuleValue()
        Dim GridFormatRule3 As DevExpress.XtraGrid.GridFormatRule = New DevExpress.XtraGrid.GridFormatRule()
        Dim FormatConditionRuleValue3 As DevExpress.XtraEditors.FormatConditionRuleValue = New DevExpress.XtraEditors.FormatConditionRuleValue()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmManageRawMaterial))
        Me.INGgvProductDetails = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRptSeQuantityDetails = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcDetail = New DevExpress.XtraGrid.GridControl()
        Me.INDviewDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.gcItem = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcRawMaterial = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcQuantityUsed = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRptSeQuantity = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.gcQuantityPending = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcQuantityCampaign = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcQuantityBalance = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColObservations = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRepositoryItemMemoExEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.INDlyRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemDetail = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoPopUpContainerEdit1 = New Presentation.Controls.IndigoPopUpContainerEdit(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDgcPatients = New DevExpress.XtraGrid.GridControl()
        Me.INDviewPatients = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BgwSetDatasourceAsync = New System.ComponentModel.BackgroundWorker()
        Me.BehaviorManager1 = New DevExpress.Utils.Behaviors.BehaviorManager(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INGgvProductDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRptSeQuantityDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRptSeQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRepositoryItemMemoExEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyRoot.SuspendLayout()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcPatients, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewPatients, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BehaviorManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyRoot)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1116, 550)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1116, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(1116, 130)
        '
        'INGgvProductDetails
        '
        Me.INGgvProductDetails.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INGgvProductDetails.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INGgvProductDetails.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INGgvProductDetails.Appearance.FocusedRow.Options.UseFont = True
        Me.INGgvProductDetails.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INGgvProductDetails.Appearance.GroupRow.Options.UseFont = True
        Me.INGgvProductDetails.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INGgvProductDetails.Appearance.HeaderPanel.Options.UseFont = True
        Me.INGgvProductDetails.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INGgvProductDetails.Appearance.Row.Options.UseFont = True
        Me.INGgvProductDetails.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn4, Me.GridColumn12, Me.GridColumn6, Me.GridColumn8, Me.GridColumn3, Me.GridColumn10, Me.GridColumn9, Me.GridColumn13})
        Me.INGgvProductDetails.GridControl = Me.INDgcDetail
        Me.INGgvProductDetails.Name = "INGgvProductDetails"
        Me.INGgvProductDetails.OptionsView.EnableAppearanceEvenRow = True
        Me.INGgvProductDetails.OptionsView.EnableAppearanceOddRow = True
        Me.INGgvProductDetails.OptionsView.RowAutoHeight = True
        Me.INGgvProductDetails.OptionsView.ShowAutoFilterRow = True
        Me.INGgvProductDetails.OptionsView.ShowDetailButtons = False
        Me.INGgvProductDetails.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INGgvProductDetails, False)
        Me.INGgvProductDetails.ViewCaption = "Detalle"
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Id"
        Me.GridColumn4.FieldName = "ProductId"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        Me.GridColumn4.Width = 106
        '
        'GridColumn12
        '
        Me.GridColumn12.Caption = "Código"
        Me.GridColumn12.FieldName = "ProductCode"
        Me.GridColumn12.Name = "GridColumn12"
        Me.GridColumn12.OptionsColumn.AllowEdit = False
        Me.GridColumn12.OptionsColumn.AllowFocus = False
        Me.GridColumn12.Visible = True
        Me.GridColumn12.VisibleIndex = 0
        Me.GridColumn12.Width = 247
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Producto"
        Me.GridColumn6.FieldName = "ProductFullName"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.AllowEdit = False
        Me.GridColumn6.OptionsColumn.AllowFocus = False
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 1
        Me.GridColumn6.Width = 549
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Cantidad Entregada"
        Me.GridColumn8.FieldName = "DeliveredQuantity"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.OptionsColumn.AllowEdit = False
        Me.GridColumn8.OptionsColumn.AllowFocus = False
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 3
        Me.GridColumn8.Width = 193
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "C. Utilizada"
        Me.GridColumn3.ColumnEdit = Me.INDRptSeQuantityDetails
        Me.GridColumn3.DisplayFormat.FormatString = "f2"
        Me.GridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn3.FieldName = "UsedQuantity"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "UsedQuantity", "{0:N0}")})
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 4
        Me.GridColumn3.Width = 144
        '
        'INDRptSeQuantityDetails
        '
        Me.INDRptSeQuantityDetails.AutoHeight = False
        Me.INDRptSeQuantityDetails.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDRptSeQuantityDetails.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDRptSeQuantityDetails.Mask.EditMask = "f2"
        Me.INDRptSeQuantityDetails.Mask.UseMaskAsDisplayFormat = True
        Me.INDRptSeQuantityDetails.MaxLength = 10
        Me.INDRptSeQuantityDetails.MaxValue = New Decimal(New Integer() {99999999, 0, 0, 0})
        Me.INDRptSeQuantityDetails.Name = "INDRptSeQuantityDetails"
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "C.  Campaña"
        Me.GridColumn10.DisplayFormat.FormatString = "f2"
        Me.GridColumn10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn10.FieldName = "CampaignQuantity"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.OptionsColumn.AllowEdit = False
        Me.GridColumn10.OptionsColumn.AllowFocus = False
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 5
        Me.GridColumn10.Width = 177
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "C. Saldo Campaña"
        Me.GridColumn9.DisplayFormat.FormatString = "f2"
        Me.GridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn9.FieldName = "CampaignBalanceQuantity"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.OptionsColumn.AllowEdit = False
        Me.GridColumn9.OptionsColumn.AllowFocus = False
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 6
        Me.GridColumn9.Width = 235
        '
        'GridColumn13
        '
        Me.GridColumn13.Caption = "U. Medida"
        Me.GridColumn13.FieldName = "MeasureUnitAbreviation"
        Me.GridColumn13.Name = "GridColumn13"
        Me.GridColumn13.OptionsColumn.AllowEdit = False
        Me.GridColumn13.OptionsColumn.AllowFocus = False
        Me.GridColumn13.Visible = True
        Me.GridColumn13.VisibleIndex = 2
        Me.GridColumn13.Width = 135
        '
        'INDgcDetail
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcDetail, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcDetail, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcDetail, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcDetail, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcDetail, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcDetail, False)
        GridLevelNode1.LevelTemplate = Me.INGgvProductDetails
        GridLevelNode1.RelationName = "CampaignDetailValidation"
        Me.INDgcDetail.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.INDgcDetail.Location = New System.Drawing.Point(12, 12)
        Me.INDgcDetail.MainView = Me.INDviewDetail
        Me.INDgcDetail.Name = "INDgcDetail"
        Me.INDgcDetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDRptSeQuantity, Me.INDRepositoryItemMemoExEdit1, Me.INDRptSeQuantityDetails})
        Me.INDgcDetail.Size = New System.Drawing.Size(1088, 517)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcDetail, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcDetail.TabIndex = 3
        Me.INDgcDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDviewDetail, Me.INGgvProductDetails})
        '
        'INDviewDetail
        '
        Me.INDviewDetail.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewDetail.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewDetail.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewDetail.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewDetail.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewDetail.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewDetail.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewDetail.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewDetail.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewDetail.Appearance.Row.Options.UseFont = True
        Me.INDviewDetail.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDviewDetail.Appearance.ViewCaption.Options.UseFont = True
        Me.INDviewDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.gcItem, Me.GridColumn11, Me.gcRawMaterial, Me.gcQuantityUsed, Me.gcQuantityPending, Me.gcQuantityCampaign, Me.gcQuantityBalance, Me.INDColObservations, Me.GridColumn1, Me.GridColumn2})
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
        Me.INDviewDetail.FormatRules.Add(GridFormatRule1)
        Me.INDviewDetail.FormatRules.Add(GridFormatRule2)
        Me.INDviewDetail.FormatRules.Add(GridFormatRule3)
        Me.INDviewDetail.GridControl = Me.INDgcDetail
        Me.INDviewDetail.Name = "INDviewDetail"
        Me.INDviewDetail.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewDetail.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewDetail.OptionsView.ShowAutoFilterRow = True
        Me.INDviewDetail.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewDetail, False)
        Me.INDviewDetail.ViewCaption = "Detalle"
        '
        'gcItem
        '
        Me.gcItem.Caption = "Materia Prima Paquetes"
        Me.gcItem.FieldName = "ProductFullName"
        Me.gcItem.Name = "gcItem"
        Me.gcItem.OptionsColumn.AllowEdit = False
        Me.gcItem.OptionsColumn.AllowFocus = False
        Me.gcItem.UnboundType = DevExpress.Data.UnboundColumnType.[String]
        Me.gcItem.Visible = True
        Me.gcItem.VisibleIndex = 0
        Me.gcItem.Width = 316
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "U. Medida"
        Me.GridColumn11.FieldName = "MeasureUnitAbreviation"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.OptionsColumn.AllowEdit = False
        Me.GridColumn11.OptionsColumn.AllowFocus = False
        Me.GridColumn11.Visible = True
        Me.GridColumn11.VisibleIndex = 1
        Me.GridColumn11.Width = 119
        '
        'gcRawMaterial
        '
        Me.gcRawMaterial.Caption = "C. Requerida"
        Me.gcRawMaterial.DisplayFormat.FormatString = "f2"
        Me.gcRawMaterial.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.gcRawMaterial.FieldName = "RequiredQuantity"
        Me.gcRawMaterial.Name = "gcRawMaterial"
        Me.gcRawMaterial.OptionsColumn.AllowEdit = False
        Me.gcRawMaterial.OptionsColumn.AllowFocus = False
        Me.gcRawMaterial.UnboundType = DevExpress.Data.UnboundColumnType.[String]
        Me.gcRawMaterial.Visible = True
        Me.gcRawMaterial.VisibleIndex = 2
        Me.gcRawMaterial.Width = 92
        '
        'gcQuantityUsed
        '
        Me.gcQuantityUsed.Caption = "C. Utilizada"
        Me.gcQuantityUsed.ColumnEdit = Me.INDRptSeQuantity
        Me.gcQuantityUsed.DisplayFormat.FormatString = "f2"
        Me.gcQuantityUsed.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.gcQuantityUsed.FieldName = "UsedQuantity"
        Me.gcQuantityUsed.Name = "gcQuantityUsed"
        Me.gcQuantityUsed.OptionsColumn.AllowEdit = False
        Me.gcQuantityUsed.OptionsColumn.AllowFocus = False
        Me.gcQuantityUsed.Visible = True
        Me.gcQuantityUsed.VisibleIndex = 3
        Me.gcQuantityUsed.Width = 85
        '
        'INDRptSeQuantity
        '
        Me.INDRptSeQuantity.AutoHeight = False
        Me.INDRptSeQuantity.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDRptSeQuantity.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDRptSeQuantity.Mask.EditMask = "f2"
        Me.INDRptSeQuantity.Mask.UseMaskAsDisplayFormat = True
        Me.INDRptSeQuantity.MaxLength = 10
        Me.INDRptSeQuantity.MaxValue = New Decimal(New Integer() {999999999, 0, 0, 0})
        Me.INDRptSeQuantity.Name = "INDRptSeQuantity"
        '
        'gcQuantityPending
        '
        Me.gcQuantityPending.Caption = "C. Pendiente"
        Me.gcQuantityPending.DisplayFormat.FormatString = "f2"
        Me.gcQuantityPending.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.gcQuantityPending.FieldName = "PendingQuantity"
        Me.gcQuantityPending.Name = "gcQuantityPending"
        Me.gcQuantityPending.OptionsColumn.AllowEdit = False
        Me.gcQuantityPending.OptionsColumn.AllowFocus = False
        Me.gcQuantityPending.UnboundType = DevExpress.Data.UnboundColumnType.[String]
        Me.gcQuantityPending.Visible = True
        Me.gcQuantityPending.VisibleIndex = 4
        Me.gcQuantityPending.Width = 88
        '
        'gcQuantityCampaign
        '
        Me.gcQuantityCampaign.Caption = "C. Campaña"
        Me.gcQuantityCampaign.DisplayFormat.FormatString = "f2"
        Me.gcQuantityCampaign.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.gcQuantityCampaign.FieldName = "CampaignQuantity"
        Me.gcQuantityCampaign.Name = "gcQuantityCampaign"
        Me.gcQuantityCampaign.OptionsColumn.AllowEdit = False
        Me.gcQuantityCampaign.OptionsColumn.AllowFocus = False
        Me.gcQuantityCampaign.Visible = True
        Me.gcQuantityCampaign.VisibleIndex = 5
        Me.gcQuantityCampaign.Width = 113
        '
        'gcQuantityBalance
        '
        Me.gcQuantityBalance.Caption = "C. Saldo Campaña"
        Me.gcQuantityBalance.DisplayFormat.FormatString = "f2"
        Me.gcQuantityBalance.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.gcQuantityBalance.FieldName = "CampaignBalanceQuantity"
        Me.gcQuantityBalance.Name = "gcQuantityBalance"
        Me.gcQuantityBalance.OptionsColumn.AllowEdit = False
        Me.gcQuantityBalance.OptionsColumn.AllowFocus = False
        Me.gcQuantityBalance.UnboundExpression = "[CampaignQuantity] - [UsedQuantity]"
        Me.gcQuantityBalance.UnboundType = DevExpress.Data.UnboundColumnType.[Decimal]
        Me.gcQuantityBalance.Visible = True
        Me.gcQuantityBalance.VisibleIndex = 6
        Me.gcQuantityBalance.Width = 132
        '
        'INDColObservations
        '
        Me.INDColObservations.Caption = "Observaciones "
        Me.INDColObservations.ColumnEdit = Me.INDRepositoryItemMemoExEdit1
        Me.INDColObservations.FieldName = "Observations"
        Me.INDColObservations.Name = "INDColObservations"
        Me.INDColObservations.Visible = True
        Me.INDColObservations.VisibleIndex = 7
        Me.INDColObservations.Width = 118
        '
        'INDRepositoryItemMemoExEdit1
        '
        Me.INDRepositoryItemMemoExEdit1.AutoHeight = False
        Me.INDRepositoryItemMemoExEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDRepositoryItemMemoExEdit1.Name = "INDRepositoryItemMemoExEdit1"
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "ItemId"
        Me.GridColumn1.FieldName = "Id"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Codigo Producto"
        Me.GridColumn2.FieldName = "ProductCode"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'INDlyRoot
        '
        Me.INDlyRoot.Controls.Add(Me.INDgcDetail)
        Me.INDlyRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyRoot.Location = New System.Drawing.Point(2, 7)
        Me.INDlyRoot.Name = "INDlyRoot"
        Me.INDlyRoot.Root = Me.Root
        Me.INDlyRoot.Size = New System.Drawing.Size(1112, 541)
        Me.INDlyRoot.TabIndex = 1
        Me.INDlyRoot.Text = "LayoutControl1"
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
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemDetail})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(1112, 541)
        Me.Root.TextVisible = False
        '
        'INDlyItemDetail
        '
        Me.INDlyItemDetail.Control = Me.INDgcDetail
        Me.INDlyItemDetail.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemDetail.MinSize = New System.Drawing.Size(209, 24)
        Me.INDlyItemDetail.Name = "INDlyItemDetail"
        Me.INDlyItemDetail.Size = New System.Drawing.Size(1092, 521)
        Me.INDlyItemDetail.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemDetail.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemDetail.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'GridView1
        '
        Me.GridView1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView1.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView1.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.GroupRow.Options.UseFont = True
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.GridControl = Me.INDgcPatients
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView1, False)
        '
        'INDgcPatients
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcPatients, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcPatients, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcPatients, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcPatients, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcPatients, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcPatients, False)
        Me.INDgcPatients.Location = New System.Drawing.Point(12, 12)
        Me.INDgcPatients.MainView = Me.INDviewPatients
        Me.INDgcPatients.Name = "INDgcPatients"
        Me.INDgcPatients.Size = New System.Drawing.Size(440, 192)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcPatients, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcPatients.TabIndex = 4
        Me.INDgcPatients.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDviewPatients, Me.GridView1})
        '
        'INDviewPatients
        '
        Me.INDviewPatients.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewPatients.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewPatients.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewPatients.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewPatients.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewPatients.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewPatients.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewPatients.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewPatients.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewPatients.Appearance.Row.Options.UseFont = True
        Me.INDviewPatients.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDviewPatients.Appearance.ViewCaption.Options.UseFont = True
        Me.INDviewPatients.GridControl = Me.INDgcPatients
        Me.INDviewPatients.Name = "INDviewPatients"
        Me.INDviewPatients.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewPatients.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewPatients.OptionsView.ShowAutoFilterRow = True
        Me.INDviewPatients.OptionsView.ShowDetailButtons = False
        Me.INDviewPatients.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewPatients, False)
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Paciente"
        Me.GridColumn7.FieldName = "PatientCodeName"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsColumn.AllowEdit = False
        Me.GridColumn7.OptionsColumn.AllowFocus = False
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 0
        Me.GridColumn7.Width = 1026
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Estado"
        Me.GridColumn5.FieldName = "RequestMixingStationDetailPatientsStatusName"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.AllowEdit = False
        Me.GridColumn5.OptionsColumn.AllowFocus = False
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 1
        Me.GridColumn5.Width = 349
        '
        'FrmManageRawMaterial
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1116, 685)
        Me.IconOptions.Icon = CType(resources.GetObject("FrmManageRawMaterial.IconOptions.Icon"), System.Drawing.Icon)
        Me.IconOptions.ShowIcon = False
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmManageRawMaterial"
        Me.Opacity = 1.0R
        Me.Tag = "2228"
        Me.Text = "Gestionar Materia Prima"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INGgvProductDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRptSeQuantityDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRptSeQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRepositoryItemMemoExEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyRoot.ResumeLayout(False)
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcPatients, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewPatients, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BehaviorManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents IndigoTextEdit1 As IndigoTextEdit
    Friend WithEvents IndigoSimpleButton1 As IndigoSimpleButton
    Friend WithEvents IndigoPopUpContainerEdit1 As IndigoPopUpContainerEdit
    Friend WithEvents IndigoLabelControl1 As IndigoLabelControl
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents IndigoGroupControl1 As IndigoGroupControl
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents IndigoDate1 As IndigoDate
    Friend WithEvents INDlyRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDgcDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDviewDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents gcItem As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlyItemDetail As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDgcPatients As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDviewPatients As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents gcRawMaterial As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcQuantityUsed As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcQuantityPending As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcQuantityCampaign As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcQuantityBalance As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BgwSetDatasourceAsync As ComponentModel.BackgroundWorker
    Friend WithEvents INDRptSeQuantity As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents INGgvProductDetails As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColObservations As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRepositoryItemMemoExEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRptSeQuantityDetails As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BehaviorManager1 As DevExpress.Utils.Behaviors.BehaviorManager
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
End Class
