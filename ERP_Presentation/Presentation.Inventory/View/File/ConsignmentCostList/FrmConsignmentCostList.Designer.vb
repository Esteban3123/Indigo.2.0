Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmConsignmentCostList
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
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmConsignmentCostList))
        Me.INDgvConsignmetCostListDetailRecord = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcProduct = New DevExpress.XtraGrid.GridControl()
        Me.INDgvProducts = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDProCheckSelectOption = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRiCostNew = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlcRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDPccPorcentualIncrement = New DevExpress.XtraBars.PopupControlContainer(Me.components)
        Me.LayoutControl2 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSeIncrement = New DevExpress.XtraEditors.SpinEdit()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.INDlcIncrementPorcentual = New DevExpress.XtraEditors.LabelControl()
        Me.INDSbCalculation = New DevExpress.XtraEditors.SimpleButton()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem3 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.EmptySpaceItem4 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.INDbtnAddProduct = New DevExpress.XtraEditors.SimpleButton()
        Me.INDBtnImportFileProducts = New DevExpress.XtraEditors.SimpleButton()
        Me.INDEsbProductRate = New Presentation.Controls.ExportStructureButton()
        Me.INDDeDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDSluSupplier = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.viewSupplier = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemTextEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlcgMain = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlciSupplier = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgProducts = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciListProducts = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoCheckEdit1 = New Presentation.Controls.IndigoCheckEdit(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvConsignmetCostListDetailRecord, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvProducts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDProCheckSelectOption, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRiCostNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlcRoot.SuspendLayout()
        CType(Me.INDPccPorcentualIncrement, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPccPorcentualIncrement.SuspendLayout()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl2.SuspendLayout()
        CType(Me.INDSeIncrement.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSluSupplier.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.viewSupplier, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciSupplier, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgProducts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciListProducts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlcRoot)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1447, 793)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1447, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(1447, 130)
        '
        'INDgvConsignmetCostListDetailRecord
        '
        Me.INDgvConsignmetCostListDetailRecord.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvConsignmetCostListDetailRecord.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvConsignmetCostListDetailRecord.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvConsignmetCostListDetailRecord.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvConsignmetCostListDetailRecord.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvConsignmetCostListDetailRecord.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvConsignmetCostListDetailRecord.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvConsignmetCostListDetailRecord.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvConsignmetCostListDetailRecord.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvConsignmetCostListDetailRecord.Appearance.Row.Options.UseFont = True
        Me.INDgvConsignmetCostListDetailRecord.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn5, Me.GridColumn6, Me.GridColumn7})
        Me.INDgvConsignmetCostListDetailRecord.GridControl = Me.INDgcProduct
        Me.INDgvConsignmetCostListDetailRecord.Name = "INDgvConsignmetCostListDetailRecord"
        Me.INDgvConsignmetCostListDetailRecord.OptionsBehavior.Editable = False
        Me.INDgvConsignmetCostListDetailRecord.OptionsBehavior.ReadOnly = True
        Me.INDgvConsignmetCostListDetailRecord.OptionsDetail.AllowExpandEmptyDetails = True
        Me.INDgvConsignmetCostListDetailRecord.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDgvConsignmetCostListDetailRecord.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvConsignmetCostListDetailRecord.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvConsignmetCostListDetailRecord.OptionsView.RowAutoHeight = True
        Me.INDgvConsignmetCostListDetailRecord.OptionsView.ShowAutoFilterRow = True
        Me.INDgvConsignmetCostListDetailRecord.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvConsignmetCostListDetailRecord, False)
        Me.INDgvConsignmetCostListDetailRecord.ViewCaption = "Registro Histórico"
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Fecha"
        Me.GridColumn5.FieldName = "CreationDate"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 0
        Me.GridColumn5.Width = 20
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Usuario"
        Me.GridColumn6.FieldName = "CodeNameUser"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 1
        Me.GridColumn6.Width = 30
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Valor"
        Me.GridColumn7.DisplayFormat.FormatString = "c2"
        Me.GridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn7.FieldName = "Cost"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 2
        Me.GridColumn7.Width = 20
        '
        'INDgcProduct
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcProduct, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcProduct, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcProduct, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcProduct, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcProduct, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcProduct, False)
        GridLevelNode1.LevelTemplate = Me.INDgvConsignmetCostListDetailRecord
        GridLevelNode1.RelationName = "ConsignmetCostListDetailRecord"
        Me.INDgcProduct.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.INDgcProduct.Location = New System.Drawing.Point(438, 93)
        Me.INDgcProduct.MainView = Me.INDgvProducts
        Me.INDgcProduct.Name = "INDgcProduct"
        Me.INDgcProduct.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDProCheckSelectOption, Me.INDRiCostNew})
        Me.INDgcProduct.Size = New System.Drawing.Size(781, 667)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcProduct, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcProduct.TabIndex = 4
        Me.INDgcProduct.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvProducts, Me.INDgvConsignmetCostListDetailRecord})
        '
        'INDgvProducts
        '
        Me.INDgvProducts.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvProducts.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvProducts.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvProducts.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvProducts.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvProducts.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvProducts.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvProducts.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvProducts.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvProducts.Appearance.Row.Options.UseFont = True
        Me.INDgvProducts.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgvProducts.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvProducts.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColSelect, Me.GridColumn3, Me.GridColumn4, Me.GridColumn8})
        Me.INDgvProducts.GridControl = Me.INDgcProduct
        Me.INDgvProducts.GroupCount = 1
        Me.INDgvProducts.GroupFormat = "{0} ({1})"
        Me.INDgvProducts.Name = "INDgvProducts"
        Me.INDgvProducts.OptionsBehavior.AutoExpandAllGroups = True
        Me.INDgvProducts.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvProducts.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvProducts.OptionsView.GroupDrawMode = DevExpress.XtraGrid.Views.Grid.GroupDrawMode.Standard
        Me.INDgvProducts.OptionsView.ShowAutoFilterRow = True
        Me.INDgvProducts.OptionsView.ShowGroupPanel = False
        Me.INDgvProducts.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn8, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvProducts, False)
        '
        'INDColSelect
        '
        Me.INDColSelect.Caption = "Sel."
        Me.INDColSelect.ColumnEdit = Me.INDProCheckSelectOption
        Me.INDColSelect.FieldName = "SelectOption"
        Me.INDColSelect.ImageOptions.Alignment = System.Drawing.StringAlignment.Center
        Me.INDColSelect.ImageOptions.Image = Global.Presentation.Inventory.My.Resources.Resources.undcheck
        Me.INDColSelect.Name = "INDColSelect"
        Me.INDColSelect.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColSelect.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColSelect.OptionsColumn.AllowMove = False
        Me.INDColSelect.OptionsColumn.AllowShowHide = False
        Me.INDColSelect.OptionsColumn.AllowSize = False
        Me.INDColSelect.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColSelect.OptionsColumn.FixedWidth = True
        Me.INDColSelect.Visible = True
        Me.INDColSelect.VisibleIndex = 0
        Me.INDColSelect.Width = 50
        '
        'INDProCheckSelectOption
        '
        Me.INDProCheckSelectOption.AutoHeight = False
        Me.INDProCheckSelectOption.Name = "INDProCheckSelectOption"
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Producto"
        Me.GridColumn3.FieldName = "ProductCodeName"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.ReadOnly = True
        Me.GridColumn3.OptionsEditForm.UseEditorColRowSpan = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 1
        Me.GridColumn3.Width = 418
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Costo nuevo"
        Me.GridColumn4.ColumnEdit = Me.INDRiCostNew
        Me.GridColumn4.DisplayFormat.FormatString = "c2"
        Me.GridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn4.FieldName = "CostNew"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 2
        Me.GridColumn4.Width = 279
        '
        'INDRiCostNew
        '
        Me.INDRiCostNew.AutoHeight = False
        Me.INDRiCostNew.DisplayFormat.FormatString = "c2"
        Me.INDRiCostNew.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDRiCostNew.EditFormat.FormatString = "c2"
        Me.INDRiCostNew.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDRiCostNew.Mask.EditMask = "############;"
        Me.INDRiCostNew.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDRiCostNew.MaxLength = 12
        Me.INDRiCostNew.Name = "INDRiCostNew"
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Tipo"
        Me.GridColumn8.FieldName = "ProductType"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 3
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.INDlcRoot
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 784)
        Me.CtrNavigationControlPanel1.TabIndex = 0
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'INDlcRoot
        '
        Me.INDlcRoot.Controls.Add(Me.INDPccPorcentualIncrement)
        Me.INDlcRoot.Controls.Add(Me.INDbtnAddProduct)
        Me.INDlcRoot.Controls.Add(Me.INDBtnImportFileProducts)
        Me.INDlcRoot.Controls.Add(Me.INDEsbProductRate)
        Me.INDlcRoot.Controls.Add(Me.INDgcProduct)
        Me.INDlcRoot.Controls.Add(Me.INDDeDate)
        Me.INDlcRoot.Controls.Add(Me.INDSluSupplier)
        Me.INDlcRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlcRoot.Location = New System.Drawing.Point(202, 7)
        Me.INDlcRoot.Name = "INDlcRoot"
        Me.INDlcRoot.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(822, 373, 574, 569)
        Me.INDlcRoot.Root = Me.Root
        Me.INDlcRoot.Size = New System.Drawing.Size(1243, 784)
        Me.INDlcRoot.TabIndex = 1
        Me.INDlcRoot.Text = "LayoutControl1"
        '
        'INDPccPorcentualIncrement
        '
        Me.INDPccPorcentualIncrement.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDPccPorcentualIncrement.Controls.Add(Me.LayoutControl2)
        Me.INDPccPorcentualIncrement.Location = New System.Drawing.Point(991, 236)
        Me.INDPccPorcentualIncrement.Manager = Me.BarManager1
        Me.INDPccPorcentualIncrement.Name = "INDPccPorcentualIncrement"
        Me.INDPccPorcentualIncrement.Size = New System.Drawing.Size(194, 139)
        Me.INDPccPorcentualIncrement.TabIndex = 28
        Me.INDPccPorcentualIncrement.Visible = False
        '
        'LayoutControl2
        '
        Me.LayoutControl2.Controls.Add(Me.INDSeIncrement)
        Me.LayoutControl2.Controls.Add(Me.INDlcIncrementPorcentual)
        Me.LayoutControl2.Controls.Add(Me.INDSbCalculation)
        Me.LayoutControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl2.Name = "LayoutControl2"
        Me.LayoutControl2.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(171, 212, 574, 569)
        Me.LayoutControl2.Root = Me.LayoutControlGroup1
        Me.LayoutControl2.Size = New System.Drawing.Size(194, 139)
        Me.LayoutControl2.TabIndex = 0
        Me.LayoutControl2.Text = "LayoutControl2"
        '
        'INDSeIncrement
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSeIncrement, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSeIncrement, False)
        Me.INDSeIncrement.EditValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.INDSeIncrement.Location = New System.Drawing.Point(12, 64)
        Me.IndigoTextEdit1.SetMascara(Me.INDSeIncrement, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSeIncrement.MenuManager = Me.BarManager1
        Me.INDSeIncrement.Name = "INDSeIncrement"
        Me.INDSeIncrement.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSeIncrement.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSeIncrement.Properties.Appearance.Options.UseBackColor = True
        Me.INDSeIncrement.Properties.Appearance.Options.UseForeColor = True
        Me.INDSeIncrement.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSeIncrement.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSeIncrement.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSeIncrement.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDSeIncrement.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSeIncrement.Properties.DisplayFormat.FormatString = "p2"
        Me.INDSeIncrement.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDSeIncrement.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDSeIncrement.Properties.Increment = New Decimal(New Integer() {1, 0, 0, 131072})
        Me.INDSeIncrement.Properties.Mask.EditMask = "p2"
        Me.INDSeIncrement.Properties.MaxLength = 5
        Me.INDSeIncrement.Properties.MaxValue = New Decimal(New Integer() {100, 0, 0, 0})
        Me.INDSeIncrement.Size = New System.Drawing.Size(170, 20)
        Me.INDSeIncrement.StyleController = Me.LayoutControl2
        Me.INDSeIncrement.TabIndex = 7
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSeIncrement, 0)
        '
        'BarManager1
        '
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 5)
        Me.barDockControlTop.Manager = Me.BarManager1
        Me.barDockControlTop.Size = New System.Drawing.Size(1447, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 928)
        Me.barDockControlBottom.Manager = Me.BarManager1
        Me.barDockControlBottom.Size = New System.Drawing.Size(1447, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 5)
        Me.barDockControlLeft.Manager = Me.BarManager1
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 923)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(1447, 5)
        Me.barDockControlRight.Manager = Me.BarManager1
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 923)
        '
        'INDlcIncrementPorcentual
        '
        Me.INDlcIncrementPorcentual.Appearance.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcIncrementPorcentual.Appearance.Options.UseFont = True
        Me.INDlcIncrementPorcentual.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Horizontal
        Me.INDlcIncrementPorcentual.Location = New System.Drawing.Point(12, 12)
        Me.INDlcIncrementPorcentual.Name = "INDlcIncrementPorcentual"
        Me.INDlcIncrementPorcentual.Size = New System.Drawing.Size(170, 18)
        Me.INDlcIncrementPorcentual.StyleController = Me.LayoutControl2
        Me.INDlcIncrementPorcentual.TabIndex = 6
        Me.INDlcIncrementPorcentual.Text = "Incremento porcentual"
        '
        'INDSbCalculation
        '
        Me.INDSbCalculation.Location = New System.Drawing.Point(12, 105)
        Me.INDSbCalculation.Name = "INDSbCalculation"
        Me.INDSbCalculation.Size = New System.Drawing.Size(170, 22)
        Me.INDSbCalculation.StyleController = Me.LayoutControl2
        Me.INDSbCalculation.TabIndex = 5
        Me.INDSbCalculation.Text = "Calcular"
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem3, Me.EmptySpaceItem3, Me.EmptySpaceItem4})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(194, 139)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDlcIncrementPorcentual
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(174, 22)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDSbCalculation
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 93)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(174, 26)
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.INDSeIncrement
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 32)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(174, 44)
        Me.LayoutControlItem3.Text = "Cantidad"
        Me.LayoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(56, 17)
        '
        'EmptySpaceItem3
        '
        Me.EmptySpaceItem3.AllowHotTrack = False
        Me.EmptySpaceItem3.Location = New System.Drawing.Point(0, 76)
        Me.EmptySpaceItem3.Name = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Size = New System.Drawing.Size(174, 17)
        Me.EmptySpaceItem3.TextSize = New System.Drawing.Size(0, 0)
        '
        'EmptySpaceItem4
        '
        Me.EmptySpaceItem4.AllowHotTrack = False
        Me.EmptySpaceItem4.Location = New System.Drawing.Point(0, 22)
        Me.EmptySpaceItem4.Name = "EmptySpaceItem4"
        Me.EmptySpaceItem4.Size = New System.Drawing.Size(174, 10)
        Me.EmptySpaceItem4.TextSize = New System.Drawing.Size(0, 0)
        '
        'INDbtnAddProduct
        '
        Me.INDbtnAddProduct.Location = New System.Drawing.Point(438, 53)
        Me.INDbtnAddProduct.MaximumSize = New System.Drawing.Size(0, 36)
        Me.INDbtnAddProduct.MinimumSize = New System.Drawing.Size(0, 36)
        Me.INDbtnAddProduct.Name = "INDbtnAddProduct"
        Me.INDbtnAddProduct.Size = New System.Drawing.Size(697, 36)
        Me.INDbtnAddProduct.StyleController = Me.INDlcRoot
        Me.INDbtnAddProduct.TabIndex = 27
        Me.INDbtnAddProduct.Text = "Agregar"
        '
        'INDBtnImportFileProducts
        '
        Me.INDBtnImportFileProducts.ImageOptions.Image = CType(resources.GetObject("INDBtnImportFileProducts.ImageOptions.Image"), System.Drawing.Image)
        Me.INDBtnImportFileProducts.Location = New System.Drawing.Point(1139, 53)
        Me.INDBtnImportFileProducts.Name = "INDBtnImportFileProducts"
        Me.INDBtnImportFileProducts.Size = New System.Drawing.Size(38, 36)
        Me.INDBtnImportFileProducts.StyleController = Me.INDlcRoot
        Me.INDBtnImportFileProducts.TabIndex = 26
        Me.INDBtnImportFileProducts.ToolTip = "Importar Archivo"
        '
        'INDEsbProductRate
        '
        Me.INDEsbProductRate.ImageOptions.Image = CType(resources.GetObject("INDEsbProductRate.ImageOptions.Image"), System.Drawing.Image)
        Me.INDEsbProductRate.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.INDEsbProductRate.Location = New System.Drawing.Point(1181, 53)
        Me.INDEsbProductRate.Name = "INDEsbProductRate"
        Me.INDEsbProductRate.Size = New System.Drawing.Size(38, 36)
        Me.INDEsbProductRate.StyleController = Me.INDlcRoot
        Me.INDEsbProductRate.TabIndex = 25
        Me.INDEsbProductRate.Text = "ExportStructureButton3"
        Me.INDEsbProductRate.ToolTip = "Exportar Estructura"
        '
        'INDDeDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDDeDate, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDDeDate, True)
        Me.INDDeDate.EditValue = Nothing
        Me.INDDeDate.Location = New System.Drawing.Point(24, 136)
        Me.IndigoTextEdit1.SetMascara(Me.INDDeDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDDeDate.Name = "INDDeDate"
        Me.INDDeDate.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDDeDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeDate.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDDeDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDDeDate.Properties.Appearance.Options.UseFont = True
        Me.INDDeDate.Properties.Appearance.Options.UseForeColor = True
        Me.INDDeDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDDeDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeDate.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDDeDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDDeDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDeDate.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDDeDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeDate.Properties.MaxValue = New Date(9999, 12, 31, 23, 59, 0, 0)
        Me.INDDeDate.Size = New System.Drawing.Size(386, 28)
        Me.INDDeDate.StyleController = Me.INDlcRoot
        Me.INDDeDate.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDDeDate, 0)
        Me.INDDeDate.ToolTip = "Este Campo es Necesario"
        '
        'INDSluSupplier
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSluSupplier, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSluSupplier, True)
        Me.INDSluSupplier.EditValue = ""
        Me.INDSluSupplier.Location = New System.Drawing.Point(24, 72)
        Me.IndigoTextEdit1.SetMascara(Me.INDSluSupplier, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSluSupplier.Name = "INDSluSupplier"
        Me.INDSluSupplier.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSluSupplier.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSluSupplier.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSluSupplier.Properties.Appearance.Options.UseBackColor = True
        Me.INDSluSupplier.Properties.Appearance.Options.UseFont = True
        Me.INDSluSupplier.Properties.Appearance.Options.UseForeColor = True
        Me.INDSluSupplier.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSluSupplier.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSluSupplier.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSluSupplier.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSluSupplier.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSluSupplier.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDSluSupplier.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSluSupplier.Properties.DisplayMember = "SupplierId.CodeName"
        Me.INDSluSupplier.Properties.NullText = ""
        Me.INDSluSupplier.Properties.PopupView = Me.viewSupplier
        Me.INDSluSupplier.Properties.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemTextEdit1})
        Me.INDSluSupplier.Properties.ValueMember = "SupplierId.Id"
        Me.INDSluSupplier.Size = New System.Drawing.Size(386, 28)
        Me.INDSluSupplier.StyleController = Me.INDlcRoot
        Me.INDSluSupplier.TabIndex = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSluSupplier, 1)
        '
        'viewSupplier
        '
        Me.viewSupplier.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.viewSupplier.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.viewSupplier.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.viewSupplier.Appearance.FocusedRow.Options.UseFont = True
        Me.viewSupplier.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viewSupplier.Appearance.GroupRow.Options.UseFont = True
        Me.viewSupplier.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viewSupplier.Appearance.HeaderPanel.Options.UseFont = True
        Me.viewSupplier.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.viewSupplier.Appearance.Row.Options.UseFont = True
        Me.viewSupplier.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2})
        Me.viewSupplier.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.viewSupplier.Name = "viewSupplier"
        Me.viewSupplier.OptionsFind.FindNullPrompt = "Buscar proveedor"
        Me.viewSupplier.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.viewSupplier.OptionsView.EnableAppearanceEvenRow = True
        Me.viewSupplier.OptionsView.EnableAppearanceOddRow = True
        Me.viewSupplier.OptionsView.ShowAutoFilterRow = True
        Me.viewSupplier.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.viewSupplier, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Nit"
        Me.GridColumn1.FieldName = "SupplierId.Code"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Nombre"
        Me.GridColumn2.FieldName = "SupplierId.CodeName"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        '
        'RepositoryItemTextEdit1
        '
        Me.RepositoryItemTextEdit1.AutoHeight = False
        Me.RepositoryItemTextEdit1.Name = "RepositoryItemTextEdit1"
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
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlcgMain, Me.INDLcgProducts})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(1243, 784)
        Me.Root.TextVisible = False
        '
        'INDlcgMain
        '
        Me.INDlcgMain.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgMain.AppearanceGroup.Options.UseFont = True
        Me.INDlcgMain.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgMain.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgMain.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgMain.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgMain.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgMain.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgMain.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgMain.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgMain.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgMain.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgMain.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgMain.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgMain, False)
        Me.INDlcgMain.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciSupplier, Me.INDLciDate})
        Me.INDlcgMain.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgMain.Name = "INDlcgMain"
        Me.INDlcgMain.Size = New System.Drawing.Size(414, 764)
        Me.INDlcgMain.Text = "Datos principales"
        '
        'INDlciSupplier
        '
        Me.INDlciSupplier.Control = Me.INDSluSupplier
        Me.INDlciSupplier.Location = New System.Drawing.Point(0, 0)
        Me.INDlciSupplier.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlciSupplier.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlciSupplier.Name = "INDlciSupplier"
        Me.INDlciSupplier.Size = New System.Drawing.Size(390, 60)
        Me.INDlciSupplier.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciSupplier.Text = "Proveedor"
        Me.INDlciSupplier.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciSupplier.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciSupplier.TextSize = New System.Drawing.Size(96, 13)
        Me.INDlciSupplier.TextToControlDistance = 6
        '
        'INDLciDate
        '
        Me.INDLciDate.Control = Me.INDDeDate
        Me.INDLciDate.Location = New System.Drawing.Point(0, 60)
        Me.INDLciDate.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciDate.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciDate.Name = "INDLciDate"
        Me.INDLciDate.Size = New System.Drawing.Size(390, 651)
        Me.INDLciDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDate.Text = "Fecha vigencia"
        Me.INDLciDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDate.TextSize = New System.Drawing.Size(96, 20)
        Me.INDLciDate.TextToControlDistance = 3
        '
        'INDLcgProducts
        '
        Me.INDLcgProducts.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgProducts.AppearanceGroup.Options.UseFont = True
        Me.INDLcgProducts.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgProducts.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgProducts.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgProducts.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgProducts.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgProducts.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgProducts.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgProducts.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgProducts.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgProducts.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgProducts.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgProducts.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgProducts, False)
        Me.INDLcgProducts.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciListProducts, Me.LayoutControlItem5, Me.LayoutControlItem8, Me.LayoutControlItem7})
        Me.INDLcgProducts.Location = New System.Drawing.Point(414, 0)
        Me.INDLcgProducts.Name = "INDLcgProducts"
        Me.INDLcgProducts.Size = New System.Drawing.Size(809, 764)
        Me.INDLcgProducts.Text = "Lista de productos"
        '
        'INDLciListProducts
        '
        Me.INDLciListProducts.Control = Me.INDgcProduct
        Me.INDLciListProducts.Location = New System.Drawing.Point(0, 40)
        Me.INDLciListProducts.MinSize = New System.Drawing.Size(1, 1)
        Me.INDLciListProducts.Name = "INDLciListProducts"
        Me.INDLciListProducts.Size = New System.Drawing.Size(785, 671)
        Me.INDLciListProducts.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciListProducts.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciListProducts.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciListProducts.TextToControlDistance = 0
        Me.INDLciListProducts.TextVisible = False
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.INDbtnAddProduct
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem5.MaxSize = New System.Drawing.Size(0, 26)
        Me.LayoutControlItem5.MinSize = New System.Drawing.Size(79, 26)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(701, 40)
        Me.LayoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextVisible = False
        '
        'LayoutControlItem8
        '
        Me.LayoutControlItem8.Control = Me.INDEsbProductRate
        Me.LayoutControlItem8.Location = New System.Drawing.Point(743, 0)
        Me.LayoutControlItem8.MaxSize = New System.Drawing.Size(42, 40)
        Me.LayoutControlItem8.MinSize = New System.Drawing.Size(42, 40)
        Me.LayoutControlItem8.Name = "LayoutControlItem8"
        Me.LayoutControlItem8.Size = New System.Drawing.Size(42, 40)
        Me.LayoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem8.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem8.TextVisible = False
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.INDBtnImportFileProducts
        Me.LayoutControlItem7.Location = New System.Drawing.Point(701, 0)
        Me.LayoutControlItem7.MaxSize = New System.Drawing.Size(42, 40)
        Me.LayoutControlItem7.MinSize = New System.Drawing.Size(42, 40)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(42, 40)
        Me.LayoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem7.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'IndigoGridControl1
        '
        '
        'FrmConsignmentCostList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1447, 928)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.IconOptions.ShowIcon = False
        Me.Name = "FrmConsignmentCostList"
        Me.Opacity = 1.0R
        Me.Tag = "89027"
        Me.Text = "FrmConsignmentCostList"
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
        CType(Me.INDgvConsignmetCostListDetailRecord, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcProduct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvProducts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDProCheckSelectOption, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRiCostNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlcRoot.ResumeLayout(False)
        CType(Me.INDPccPorcentualIncrement, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPccPorcentualIncrement.ResumeLayout(False)
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl2.ResumeLayout(False)
        CType(Me.INDSeIncrement.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSluSupplier.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.viewSupplier, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciSupplier, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgProducts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciListProducts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents INDlcRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDSluSupplier As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents viewSupplier As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlcgMain As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlciSupplier As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents CtrNavigationControlPanel1 As CtrNavigationControlPanel
    Friend WithEvents INDDeDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents IndigoTextEdit1 As IndigoTextEdit
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents INDLciDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGroupControl1 As IndigoGroupControl
    Friend WithEvents INDgcProduct As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvProducts As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLcgProducts As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciListProducts As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoCheckEdit1 As IndigoCheckEdit
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemTextEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDSbCalculation As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDlcIncrementPorcentual As DevExpress.XtraEditors.LabelControl
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents INDColSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDProCheckSelectOption As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents INDSeIncrement As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDgvConsignmetCostListDetailRecord As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDBtnImportFileProducts As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDEsbProductRate As ExportStructureButton
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents INDbtnAddProduct As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRiCostNew As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents INDPccPorcentualIncrement As DevExpress.XtraBars.PopupControlContainer
    Friend WithEvents LayoutControl2 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem3 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem4 As DevExpress.XtraLayout.EmptySpaceItem
End Class
