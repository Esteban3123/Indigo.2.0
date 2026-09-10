Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmWarehouseStock
    Inherits FormBase

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
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
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.LyWarehouseStock = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSleWarehouse = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDSleViewWarehouse = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColCodeWarehouse = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColNameWarehouse = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGcProductList = New DevExpress.XtraGrid.GridControl()
        Me.INDGvProductList = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColProduct = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDriseQuantityMin = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDriseQuantityMax = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDriseResetPoint = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LcgSettingsWarehouseStock = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLySleWarehouse = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LyWarehouseStock, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LyWarehouseStock.SuspendLayout()
        CType(Me.INDSleWarehouse.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleViewWarehouse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcProductList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvProductList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDriseQuantityMin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDriseQuantityMax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDriseResetPoint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LcgSettingsWarehouseStock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLySleWarehouse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.LyWarehouseStock)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1008, 607)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1008, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1008, 98)
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.LyWarehouseStock
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(4, 9)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 593)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'LyWarehouseStock
        '
        Me.LyWarehouseStock.Controls.Add(Me.INDSleWarehouse)
        Me.LyWarehouseStock.Controls.Add(Me.INDGcProductList)
        Me.LyWarehouseStock.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LyWarehouseStock.Location = New System.Drawing.Point(204, 9)
        Me.LyWarehouseStock.Name = "LyWarehouseStock"
        Me.LyWarehouseStock.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(463, 292, 250, 350)
        Me.LyWarehouseStock.Root = Me.LayoutControlGroup1
        Me.LyWarehouseStock.Size = New System.Drawing.Size(800, 593)
        Me.LyWarehouseStock.TabIndex = 1
        Me.LyWarehouseStock.Text = "LayoutControl1"
        '
        'INDSleWarehouse
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleWarehouse, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleWarehouse, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleWarehouse, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleWarehouse, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleWarehouse, False)
        Me.INDSleWarehouse.Location = New System.Drawing.Point(104, 43)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleWarehouse, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleWarehouse.Name = "INDSleWarehouse"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleWarehouse, True)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleWarehouse, False)
        Me.INDSleWarehouse.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDSleWarehouse.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleWarehouse.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleWarehouse.Properties.Appearance.Options.UseFont = True
        Me.INDSleWarehouse.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleWarehouse.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSleWarehouse.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleWarehouse.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSleWarehouse.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSleWarehouse.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleWarehouse.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleWarehouse.Properties.DisplayMember = "CodeName"
        Me.INDSleWarehouse.Properties.NullText = ""
        Me.INDSleWarehouse.Properties.PopupSizeable = False
        Me.INDSleWarehouse.Properties.ShowFooter = False
        Me.INDSleWarehouse.Properties.ValueMember = "Id"
        Me.INDSleWarehouse.Properties.View = Me.INDSleViewWarehouse
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleWarehouse, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleWarehouse, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleWarehouse, True)
        Me.INDSleWarehouse.Size = New System.Drawing.Size(285, 28)
        Me.INDSleWarehouse.StyleController = Me.LyWarehouseStock
        Me.INDSleWarehouse.TabIndex = 5
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleWarehouse, "302")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleWarehouse, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleWarehouse, "{0} - {1}")
        Me.INDSleWarehouse.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleWarehouse, False)
        '
        'INDSleViewWarehouse
        '
        Me.INDSleViewWarehouse.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDSleViewWarehouse.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDSleViewWarehouse.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDSleViewWarehouse.Appearance.FocusedRow.Options.UseFont = True
        Me.INDSleViewWarehouse.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleViewWarehouse.Appearance.GroupRow.Options.UseFont = True
        Me.INDSleViewWarehouse.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleViewWarehouse.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDSleViewWarehouse.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDSleViewWarehouse.Appearance.Row.Options.UseFont = True
        Me.INDSleViewWarehouse.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColCodeWarehouse, Me.ColNameWarehouse})
        Me.INDSleViewWarehouse.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDSleViewWarehouse.Name = "INDSleViewWarehouse"
        Me.INDSleViewWarehouse.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDSleViewWarehouse.OptionsView.EnableAppearanceEvenRow = True
        Me.INDSleViewWarehouse.OptionsView.EnableAppearanceOddRow = True
        Me.INDSleViewWarehouse.OptionsView.ShowAutoFilterRow = True
        Me.INDSleViewWarehouse.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDSleViewWarehouse, False)
        '
        'ColCodeWarehouse
        '
        Me.ColCodeWarehouse.Caption = "Código"
        Me.ColCodeWarehouse.FieldName = "Code"
        Me.ColCodeWarehouse.Name = "ColCodeWarehouse"
        Me.ColCodeWarehouse.OptionsColumn.AllowEdit = False
        Me.ColCodeWarehouse.OptionsColumn.AllowFocus = False
        Me.ColCodeWarehouse.OptionsColumn.AllowMove = False
        Me.ColCodeWarehouse.OptionsColumn.AllowSize = False
        Me.ColCodeWarehouse.Visible = True
        Me.ColCodeWarehouse.VisibleIndex = 0
        Me.ColCodeWarehouse.Width = 285
        '
        'ColNameWarehouse
        '
        Me.ColNameWarehouse.Caption = "Nombre"
        Me.ColNameWarehouse.FieldName = "Name"
        Me.ColNameWarehouse.Name = "ColNameWarehouse"
        Me.ColNameWarehouse.OptionsColumn.AllowEdit = False
        Me.ColNameWarehouse.OptionsColumn.AllowFocus = False
        Me.ColNameWarehouse.OptionsColumn.AllowMove = False
        Me.ColNameWarehouse.OptionsColumn.AllowSize = False
        Me.ColNameWarehouse.Visible = True
        Me.ColNameWarehouse.VisibleIndex = 1
        Me.ColNameWarehouse.Width = 1027
        '
        'INDGcProductList
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcProductList, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcProductList, Nothing)
        Me.INDGcProductList.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.INDGcProductList, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcProductList, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcProductList, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcProductList, False)
        Me.INDGcProductList.Location = New System.Drawing.Point(9, 79)
        Me.INDGcProductList.MainView = Me.INDGvProductList
        Me.INDGcProductList.Name = "INDGcProductList"
        Me.INDGcProductList.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDriseQuantityMax, Me.INDriseQuantityMin, Me.INDriseResetPoint})
        Me.INDGcProductList.Size = New System.Drawing.Size(818, 487)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcProductList, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDGcProductList.TabIndex = 4
        Me.INDGcProductList.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvProductList})
        '
        'INDGvProductList
        '
        Me.INDGvProductList.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvProductList.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvProductList.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDGvProductList.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvProductList.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvProductList.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvProductList.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvProductList.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvProductList.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvProductList.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvProductList.Appearance.Row.Options.UseFont = True
        Me.INDGvProductList.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvProductList.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvProductList.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColProduct, Me.GridColumn2, Me.GridColumn4, Me.GridColumn6, Me.GridColumn7, Me.GridColumn8, Me.GridColumn1, Me.GridColumn3, Me.GridColumn5, Me.GridColumn9})
        Me.INDGvProductList.GridControl = Me.INDGcProductList
        Me.INDGvProductList.Name = "INDGvProductList"
        Me.INDGvProductList.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvProductList.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvProductList.OptionsView.ShowAutoFilterRow = True
        Me.INDGvProductList.OptionsView.ShowDetailButtons = False
        Me.INDGvProductList.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvProductList, False)
        '
        'ColProduct
        '
        Me.ColProduct.Caption = "Producto"
        Me.ColProduct.FieldName = "CodeName"
        Me.ColProduct.Name = "ColProduct"
        Me.ColProduct.OptionsColumn.AllowEdit = False
        Me.ColProduct.OptionsColumn.AllowFocus = False
        Me.ColProduct.Visible = True
        Me.ColProduct.VisibleIndex = 0
        Me.ColProduct.Width = 220
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Reposición"
        Me.GridColumn2.FieldName = "RepositionPoint"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 106
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "% Seguridad"
        Me.GridColumn4.FieldName = "ProductGroupId.SecurityPercentage"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 2
        Me.GridColumn4.Width = 104
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Mínima"
        Me.GridColumn6.ColumnEdit = Me.INDriseQuantityMin
        Me.GridColumn6.FieldName = "MinimumStockWarehouse"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 3
        Me.GridColumn6.Width = 71
        '
        'INDriseQuantityMin
        '
        Me.INDriseQuantityMin.AutoHeight = False
        Me.INDriseQuantityMin.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDriseQuantityMin.Mask.EditMask = "[0-9]+"
        Me.INDriseQuantityMin.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDriseQuantityMin.MaxLength = 5
        Me.INDriseQuantityMin.MaxValue = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.INDriseQuantityMin.Name = "INDriseQuantityMin"
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Máxima"
        Me.GridColumn7.ColumnEdit = Me.INDriseQuantityMax
        Me.GridColumn7.FieldName = "MaximumStockWarehouse"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 4
        Me.GridColumn7.Width = 71
        '
        'INDriseQuantityMax
        '
        Me.INDriseQuantityMax.AutoHeight = False
        Me.INDriseQuantityMax.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDriseQuantityMax.Mask.EditMask = "[0-9]+"
        Me.INDriseQuantityMax.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDriseQuantityMax.MaxLength = 5
        Me.INDriseQuantityMax.MaxValue = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.INDriseQuantityMax.Name = "INDriseQuantityMax"
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Punto Reposición"
        Me.GridColumn8.ColumnEdit = Me.INDriseResetPoint
        Me.GridColumn8.FieldName = "RepositionPointWarehouse"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 5
        Me.GridColumn8.Width = 130
        '
        'INDriseResetPoint
        '
        Me.INDriseResetPoint.AutoHeight = False
        Me.INDriseResetPoint.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDriseResetPoint.Mask.EditMask = "[0-9]+"
        Me.INDriseResetPoint.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDriseResetPoint.MaxLength = 5
        Me.INDriseResetPoint.MaxValue = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.INDriseResetPoint.Name = "INDriseResetPoint"
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Usuario Creación"
        Me.GridColumn1.FieldName = "CreationUserWarehouseStock"
        Me.GridColumn1.Name = "GridColumn1"
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Fecha Creación"
        Me.GridColumn3.FieldName = "CreationDateWarehouseStock"
        Me.GridColumn3.Name = "GridColumn3"
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Usuario Modificación"
        Me.GridColumn5.FieldName = "ModificationUserWarehouseStock"
        Me.GridColumn5.Name = "GridColumn5"
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Fecha Modificación"
        Me.GridColumn9.FieldName = "ModificationDateWarehouseStock"
        Me.GridColumn9.Name = "GridColumn9"
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
        Me.LayoutControlGroup1.CustomizationFormText = "Stock Almacén"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LcgSettingsWarehouseStock})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(836, 576)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LcgSettingsWarehouseStock
        '
        Me.LcgSettingsWarehouseStock.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LcgSettingsWarehouseStock.AppearanceGroup.Options.UseFont = True
        Me.LcgSettingsWarehouseStock.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LcgSettingsWarehouseStock.AppearanceItemCaption.Options.UseFont = True
        Me.LcgSettingsWarehouseStock.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LcgSettingsWarehouseStock.AppearanceTabPage.Header.Options.UseFont = True
        Me.LcgSettingsWarehouseStock.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LcgSettingsWarehouseStock.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LcgSettingsWarehouseStock.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LcgSettingsWarehouseStock.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LcgSettingsWarehouseStock.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LcgSettingsWarehouseStock.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LcgSettingsWarehouseStock.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LcgSettingsWarehouseStock.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LcgSettingsWarehouseStock, False)
        Me.LcgSettingsWarehouseStock.CustomizationFormText = "Listado de Productos"
        Me.LcgSettingsWarehouseStock.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.INDLySleWarehouse})
        Me.LcgSettingsWarehouseStock.Location = New System.Drawing.Point(0, 0)
        Me.LcgSettingsWarehouseStock.Name = "LcgSettingsWarehouseStock"
        Me.LcgSettingsWarehouseStock.Size = New System.Drawing.Size(836, 576)
        Me.LcgSettingsWarehouseStock.Text = "Listado de Productos"
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDGcProductList
        Me.LayoutControlItem1.CustomizationFormText = "Listado de Productos"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 36)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(828, 0)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(828, 1)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(828, 497)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'INDLySleWarehouse
        '
        Me.INDLySleWarehouse.Control = Me.INDSleWarehouse
        Me.INDLySleWarehouse.CustomizationFormText = "Almacén"
        Me.INDLySleWarehouse.Location = New System.Drawing.Point(0, 0)
        Me.INDLySleWarehouse.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDLySleWarehouse.MinSize = New System.Drawing.Size(390, 36)
        Me.INDLySleWarehouse.Name = "INDLySleWarehouse"
        Me.INDLySleWarehouse.Size = New System.Drawing.Size(828, 36)
        Me.INDLySleWarehouse.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLySleWarehouse.Text = "Almacén"
        Me.INDLySleWarehouse.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLySleWarehouse.TextSize = New System.Drawing.Size(90, 21)
        Me.INDLySleWarehouse.TextToControlDistance = 5
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'FrmWarehouseStock
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 729)
        Me.Name = "FrmWarehouseStock"
        Me.Opacity = 1.0R
        Me.Tag = "1416"
        Me.Text = "Stock Almacén"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LyWarehouseStock, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LyWarehouseStock.ResumeLayout(False)
        CType(Me.INDSleWarehouse.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleViewWarehouse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcProductList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvProductList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDriseQuantityMin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDriseQuantityMax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDriseResetPoint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LcgSettingsWarehouseStock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLySleWarehouse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents LyWarehouseStock As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents LcgSettingsWarehouseStock As DevExpress.XtraLayout.LayoutControlGroup
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents INDSleWarehouse As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents INDSleViewWarehouse As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDGcProductList As DevExpress.XtraGrid.GridControl
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents INDGvProductList As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLySleWarehouse As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents ColProduct As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents ColNameWarehouse As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColCodeWarehouse As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDriseQuantityMin As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents INDriseQuantityMax As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents INDriseResetPoint As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
End Class
