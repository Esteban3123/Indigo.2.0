<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmItemPackageDetail
    Inherits DevExpress.XtraEditors.XtraForm

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
        Me.LyRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.GdcServices = New DevExpress.XtraGrid.GridControl()
        Me.GdvSmallServices = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColSmallServiceProductCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColSmallServiceProductName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColSmallInvoicedQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColSmallTotalSalesPrice = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepTxtUnitValue = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.ColSmallGrandTotalSalesPrice = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColSmallServiceBillingGroupCodeName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepICBDistributionType = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.RepICBRecoveryFeeType = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.RepChkApplyRecoveryFee = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepICBIsDistributed = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.GdvLargeServices = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColLargeServiceProductCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColLargeServiceProductName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColLargeInvoicedQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColLargeTotalSalesPrice = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColLargeGrandTotalSalesPrice = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColThirdPartySalesPrice = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColSubTotalPatientSalesPrice = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColPatientPercentage = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColRecoveryFeeType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColApplyRecoveryFee = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColAuthorizationNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColServiceDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColRateManualSalePrice = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColDistributionType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColThirdPartyDiscount = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColSurchargeApply = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColLargeServiceBillingGroupCodeName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LcgRoot = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LiPackagedItems = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl()
        ''Me.IndigoLayoutControl1 = New Presentation.Controls.IndigoLayoutControl()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup()
        CType(Me.LyRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LyRoot.SuspendLayout()
        CType(Me.GdcServices, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GdvSmallServices, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepTxtUnitValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepICBDistributionType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepICBRecoveryFeeType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepChkApplyRecoveryFee, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepICBIsDistributed, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GdvLargeServices, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LcgRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LiPackagedItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        'CType(Me.IndigoLayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LyRoot
        '
        Me.LyRoot.Controls.Add(Me.GdcServices)
        Me.LyRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LyRoot.Location = New System.Drawing.Point(0, 0)
        Me.LyRoot.Name = "LyRoot"
        Me.LyRoot.Root = Me.LcgRoot
        Me.LyRoot.Size = New System.Drawing.Size(698, 273)
        Me.LyRoot.TabIndex = 0
        Me.LyRoot.Text = "LayoutControl1"
        '
        'GdcServices
        '
        Me.IndigoGridControl1.SetAddActions(Me.GdcServices, Nothing)
        Me.GdcServices.AllowDrop = True
        Me.IndigoGridControl1.SetControlNextFocus(Me.GdcServices, Nothing)
        Me.GdcServices.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.GdcServices, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.GdcServices, True)
        Me.IndigoGridControl1.SetHoldSize(Me.GdcServices, False)
        Me.IndigoGridControl1.SetHotTrack(Me.GdcServices, False)
        Me.GdcServices.Location = New System.Drawing.Point(12, 12)
        Me.GdcServices.MainView = Me.GdvSmallServices
        Me.GdcServices.Name = "GdcServices"
        Me.GdcServices.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepICBDistributionType, Me.RepICBRecoveryFeeType, Me.RepChkApplyRecoveryFee, Me.RepICBIsDistributed, Me.RepTxtUnitValue})
        Me.GdcServices.Size = New System.Drawing.Size(674, 249)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.GdcServices, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.GdcServices.TabIndex = 9
        Me.GdcServices.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GdvSmallServices, Me.GdvLargeServices})
        '
        'GdvSmallServices
        '
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.GdvSmallServices.Appearance.FocusedRow.Options.UseBackColor = True
        Me.GdvSmallServices.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.GdvSmallServices.Appearance.GroupRow.Options.UseFont = True
        'Cadena reemplazada... True
        Me.GdvSmallServices.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.GdvSmallServices.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
        Me.GdvSmallServices.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GdvSmallServices.Appearance.Row.Options.UseFont = True
        Me.GdvSmallServices.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.GdvSmallServices.Appearance.ViewCaption.Options.UseFont = True
        Me.GdvSmallServices.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColSmallServiceProductCode, Me.ColSmallServiceProductName, Me.ColSmallInvoicedQuantity, Me.ColSmallTotalSalesPrice, Me.ColSmallGrandTotalSalesPrice, Me.ColSmallServiceBillingGroupCodeName})
        Me.GdvSmallServices.GridControl = Me.GdcServices
        Me.GdvSmallServices.GroupCount = 1
        Me.GdvSmallServices.Name = "GdvSmallServices"
        Me.GdvSmallServices.OptionsBehavior.AutoExpandAllGroups = True
        Me.GdvSmallServices.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseUp
        Me.GdvSmallServices.OptionsDetail.ShowDetailTabs = False
        Me.GdvSmallServices.OptionsMenu.EnableFooterMenu = False
        Me.GdvSmallServices.OptionsMenu.EnableGroupPanelMenu = False
        Me.GdvSmallServices.OptionsMenu.ShowAddNewSummaryItem = DevExpress.Utils.DefaultBoolean.[False]
        Me.GdvSmallServices.OptionsMenu.ShowAutoFilterRowItem = False
        Me.GdvSmallServices.OptionsMenu.ShowDateTimeGroupIntervalItems = False
        Me.GdvSmallServices.OptionsMenu.ShowGroupSortSummaryItems = False
        Me.GdvSmallServices.OptionsMenu.ShowSplitItem = False
        Me.GdvSmallServices.OptionsSelection.MultiSelect = True
        Me.GdvSmallServices.OptionsView.EnableAppearanceEvenRow = True
        Me.GdvSmallServices.OptionsView.EnableAppearanceOddRow = True
        Me.GdvSmallServices.OptionsView.ShowAutoFilterRow = True
        Me.GdvSmallServices.OptionsView.ShowFooter = True
        Me.GdvSmallServices.OptionsView.ShowGroupPanel = False
        Me.GdvSmallServices.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.ColSmallServiceBillingGroupCodeName, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GdvSmallServices, False)
        '
        'ColSmallServiceProductCode
        '
        Me.ColSmallServiceProductCode.AppearanceCell.Options.UseTextOptions = True
        Me.ColSmallServiceProductCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.ColSmallServiceProductCode.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColSmallServiceProductCode.AppearanceHeader.Options.UseTextOptions = True
        Me.ColSmallServiceProductCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColSmallServiceProductCode.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColSmallServiceProductCode.Caption = "Código"
        Me.ColSmallServiceProductCode.FieldName = "ItemCode"
        Me.ColSmallServiceProductCode.Name = "ColSmallServiceProductCode"
        Me.ColSmallServiceProductCode.OptionsColumn.AllowEdit = False
        Me.ColSmallServiceProductCode.OptionsColumn.AllowFocus = False
        Me.ColSmallServiceProductCode.OptionsFilter.AllowAutoFilter = False
        Me.ColSmallServiceProductCode.Visible = True
        Me.ColSmallServiceProductCode.VisibleIndex = 0
        Me.ColSmallServiceProductCode.Width = 68
        '
        'ColSmallServiceProductName
        '
        Me.ColSmallServiceProductName.AppearanceCell.Options.UseTextOptions = True
        Me.ColSmallServiceProductName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.ColSmallServiceProductName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColSmallServiceProductName.Caption = "Descripción"
        Me.ColSmallServiceProductName.FieldName = "ItemDescription"
        Me.ColSmallServiceProductName.MinWidth = 10
        Me.ColSmallServiceProductName.Name = "ColSmallServiceProductName"
        Me.ColSmallServiceProductName.OptionsColumn.AllowEdit = False
        Me.ColSmallServiceProductName.OptionsColumn.AllowFocus = False
        Me.ColSmallServiceProductName.OptionsFilter.AllowAutoFilter = False
        Me.ColSmallServiceProductName.Visible = True
        Me.ColSmallServiceProductName.VisibleIndex = 1
        Me.ColSmallServiceProductName.Width = 108
        '
        'ColSmallInvoicedQuantity
        '
        Me.ColSmallInvoicedQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.ColSmallInvoicedQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColSmallInvoicedQuantity.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColSmallInvoicedQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.ColSmallInvoicedQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColSmallInvoicedQuantity.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColSmallInvoicedQuantity.Caption = "Cantidad"
        Me.ColSmallInvoicedQuantity.FieldName = "ItemQuantity"
        Me.ColSmallInvoicedQuantity.MinWidth = 10
        Me.ColSmallInvoicedQuantity.Name = "ColSmallInvoicedQuantity"
        Me.ColSmallInvoicedQuantity.OptionsColumn.AllowEdit = False
        Me.ColSmallInvoicedQuantity.OptionsColumn.AllowFocus = False
        Me.ColSmallInvoicedQuantity.OptionsFilter.AllowAutoFilter = False
        Me.ColSmallInvoicedQuantity.Visible = True
        Me.ColSmallInvoicedQuantity.VisibleIndex = 2
        Me.ColSmallInvoicedQuantity.Width = 45
        '
        'ColSmallTotalSalesPrice
        '
        Me.ColSmallTotalSalesPrice.AppearanceCell.Options.UseTextOptions = True
        Me.ColSmallTotalSalesPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.ColSmallTotalSalesPrice.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColSmallTotalSalesPrice.AppearanceHeader.Options.UseTextOptions = True
        Me.ColSmallTotalSalesPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColSmallTotalSalesPrice.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColSmallTotalSalesPrice.Caption = "V. Unitario"
        Me.ColSmallTotalSalesPrice.ColumnEdit = Me.RepTxtUnitValue
        Me.ColSmallTotalSalesPrice.DisplayFormat.FormatString = "C0"
        Me.ColSmallTotalSalesPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ColSmallTotalSalesPrice.FieldName = "ItemTotalSalesPrice"
        Me.ColSmallTotalSalesPrice.MinWidth = 10
        Me.ColSmallTotalSalesPrice.Name = "ColSmallTotalSalesPrice"
        Me.ColSmallTotalSalesPrice.OptionsColumn.AllowEdit = False
        Me.ColSmallTotalSalesPrice.OptionsColumn.AllowFocus = False
        Me.ColSmallTotalSalesPrice.OptionsFilter.AllowAutoFilter = False
        Me.ColSmallTotalSalesPrice.Visible = True
        Me.ColSmallTotalSalesPrice.VisibleIndex = 3
        Me.ColSmallTotalSalesPrice.Width = 45
        '
        'RepTxtUnitValue
        '
        Me.RepTxtUnitValue.AutoHeight = False
        Me.RepTxtUnitValue.Mask.EditMask = "C0"
        Me.RepTxtUnitValue.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.RepTxtUnitValue.Mask.UseMaskAsDisplayFormat = True
        Me.RepTxtUnitValue.MaxLength = 18
        Me.RepTxtUnitValue.Name = "RepTxtUnitValue"
        '
        'ColSmallGrandTotalSalesPrice
        '
        Me.ColSmallGrandTotalSalesPrice.AppearanceCell.Options.UseTextOptions = True
        Me.ColSmallGrandTotalSalesPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.ColSmallGrandTotalSalesPrice.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColSmallGrandTotalSalesPrice.AppearanceHeader.Options.UseTextOptions = True
        Me.ColSmallGrandTotalSalesPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColSmallGrandTotalSalesPrice.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColSmallGrandTotalSalesPrice.Caption = "Total"
        Me.ColSmallGrandTotalSalesPrice.DisplayFormat.FormatString = "C0"
        Me.ColSmallGrandTotalSalesPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ColSmallGrandTotalSalesPrice.FieldName = "GrandTotalSalesPrice"
        Me.ColSmallGrandTotalSalesPrice.MinWidth = 10
        Me.ColSmallGrandTotalSalesPrice.Name = "ColSmallGrandTotalSalesPrice"
        Me.ColSmallGrandTotalSalesPrice.OptionsColumn.AllowEdit = False
        Me.ColSmallGrandTotalSalesPrice.OptionsColumn.AllowFocus = False
        Me.ColSmallGrandTotalSalesPrice.OptionsFilter.AllowAutoFilter = False
        Me.ColSmallGrandTotalSalesPrice.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "GrandTotalSalesPrice", "Total: {0:C0}")})
        Me.ColSmallGrandTotalSalesPrice.Visible = True
        Me.ColSmallGrandTotalSalesPrice.VisibleIndex = 4
        Me.ColSmallGrandTotalSalesPrice.Width = 50
        '
        'ColSmallServiceBillingGroupCodeName
        '
        Me.ColSmallServiceBillingGroupCodeName.Caption = "Grupo Facturación"
        Me.ColSmallServiceBillingGroupCodeName.FieldName = "ServiceBillingGroupCodeName"
        Me.ColSmallServiceBillingGroupCodeName.Name = "ColSmallServiceBillingGroupCodeName"
        Me.ColSmallServiceBillingGroupCodeName.Visible = True
        Me.ColSmallServiceBillingGroupCodeName.VisibleIndex = 5
        '
        'RepICBDistributionType
        '
        Me.RepICBDistributionType.AutoHeight = False
        Me.RepICBDistributionType.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepICBDistributionType.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Ninguno", CType(1, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Distribucion Normal", CType(2, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Distribucion por Carencia 1er Responsable", CType(3, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Distribucion por Carencia 2do Responsable", CType(4, Byte), -1)})
        Me.RepICBDistributionType.Name = "RepICBDistributionType"
        '
        'RepICBRecoveryFeeType
        '
        Me.RepICBRecoveryFeeType.AutoHeight = False
        Me.RepICBRecoveryFeeType.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Ninguna", CType(1, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Cuota Moderadora", CType(2, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Copago", CType(3, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Bono", CType(4, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Cuota de Recuperación", CType(5, Byte), -1)})
        Me.RepICBRecoveryFeeType.Name = "RepICBRecoveryFeeType"
        '
        'RepChkApplyRecoveryFee
        '
        Me.RepChkApplyRecoveryFee.AutoHeight = False
        Me.RepChkApplyRecoveryFee.Name = "RepChkApplyRecoveryFee"
        Me.RepChkApplyRecoveryFee.ValueChecked = CType(2, Byte)
        Me.RepChkApplyRecoveryFee.ValueGrayed = CType(1, Byte)
        Me.RepChkApplyRecoveryFee.ValueUnchecked = CType(0, Byte)
        '
        'RepICBIsDistributed
        '
        Me.RepICBIsDistributed.AutoHeight = False
        Me.RepICBIsDistributed.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", CType(2, Byte), 0), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", CType(3, Byte), 0), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", CType(4, Byte), 0), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", CType(5, Byte), 1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", CType(6, Byte), 2)})
        Me.RepICBIsDistributed.Name = "RepICBIsDistributed"
        '
        'GdvLargeServices
        '
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.GdvLargeServices.Appearance.FocusedRow.Options.UseBackColor = True
        Me.GdvLargeServices.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.GdvLargeServices.Appearance.GroupRow.Options.UseFont = True
        'Cadena reemplazada... True
        Me.GdvLargeServices.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.GdvLargeServices.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
        Me.GdvLargeServices.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GdvLargeServices.Appearance.Row.Options.UseFont = True
        Me.GdvLargeServices.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.GdvLargeServices.Appearance.ViewCaption.Options.UseFont = True
        Me.GdvLargeServices.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColLargeServiceProductCode, Me.ColLargeServiceProductName, Me.ColLargeInvoicedQuantity, Me.ColLargeTotalSalesPrice, Me.ColLargeGrandTotalSalesPrice, Me.ColThirdPartySalesPrice, Me.ColSubTotalPatientSalesPrice, Me.ColPatientPercentage, Me.ColRecoveryFeeType, Me.ColApplyRecoveryFee, Me.ColAuthorizationNumber, Me.ColServiceDate, Me.ColRateManualSalePrice, Me.ColDistributionType, Me.ColThirdPartyDiscount, Me.ColSurchargeApply, Me.ColLargeServiceBillingGroupCodeName})
        Me.GdvLargeServices.GridControl = Me.GdcServices
        Me.GdvLargeServices.GroupCount = 1
        Me.GdvLargeServices.Name = "GdvLargeServices"
        Me.GdvLargeServices.OptionsBehavior.AutoExpandAllGroups = True
        Me.GdvLargeServices.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown
        Me.GdvLargeServices.OptionsDetail.ShowDetailTabs = False
        Me.GdvLargeServices.OptionsMenu.EnableFooterMenu = False
        Me.GdvLargeServices.OptionsMenu.EnableGroupPanelMenu = False
        Me.GdvLargeServices.OptionsMenu.ShowAddNewSummaryItem = DevExpress.Utils.DefaultBoolean.[False]
        Me.GdvLargeServices.OptionsMenu.ShowAutoFilterRowItem = False
        Me.GdvLargeServices.OptionsMenu.ShowDateTimeGroupIntervalItems = False
        Me.GdvLargeServices.OptionsMenu.ShowGroupSortSummaryItems = False
        Me.GdvLargeServices.OptionsMenu.ShowSplitItem = False
        Me.GdvLargeServices.OptionsSelection.MultiSelect = True
        Me.GdvLargeServices.OptionsView.EnableAppearanceEvenRow = True
        Me.GdvLargeServices.OptionsView.EnableAppearanceOddRow = True
        Me.GdvLargeServices.OptionsView.ShowAutoFilterRow = True
        Me.GdvLargeServices.OptionsView.ShowGroupPanel = False
        Me.GdvLargeServices.OptionsView.ShowIndicator = False
        Me.GdvLargeServices.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.ColLargeServiceBillingGroupCodeName, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GdvLargeServices, False)
        '
        'ColLargeServiceProductCode
        '
        Me.ColLargeServiceProductCode.AppearanceCell.Options.UseTextOptions = True
        Me.ColLargeServiceProductCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.ColLargeServiceProductCode.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColLargeServiceProductCode.AppearanceHeader.Options.UseTextOptions = True
        Me.ColLargeServiceProductCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColLargeServiceProductCode.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColLargeServiceProductCode.Caption = "Código"
        Me.ColLargeServiceProductCode.Name = "ColLargeServiceProductCode"
        Me.ColLargeServiceProductCode.OptionsColumn.AllowEdit = False
        Me.ColLargeServiceProductCode.OptionsColumn.AllowFocus = False
        Me.ColLargeServiceProductCode.OptionsFilter.AllowAutoFilter = False
        Me.ColLargeServiceProductCode.Visible = True
        Me.ColLargeServiceProductCode.VisibleIndex = 0
        '
        'ColLargeServiceProductName
        '
        Me.ColLargeServiceProductName.AppearanceCell.Options.UseTextOptions = True
        Me.ColLargeServiceProductName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.ColLargeServiceProductName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColLargeServiceProductName.AppearanceHeader.Options.UseTextOptions = True
        Me.ColLargeServiceProductName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColLargeServiceProductName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColLargeServiceProductName.Caption = "Descripción"
        Me.ColLargeServiceProductName.MinWidth = 10
        Me.ColLargeServiceProductName.Name = "ColLargeServiceProductName"
        Me.ColLargeServiceProductName.OptionsColumn.AllowEdit = False
        Me.ColLargeServiceProductName.OptionsColumn.AllowFocus = False
        Me.ColLargeServiceProductName.OptionsFilter.AllowAutoFilter = False
        Me.ColLargeServiceProductName.Visible = True
        Me.ColLargeServiceProductName.VisibleIndex = 1
        '
        'ColLargeInvoicedQuantity
        '
        Me.ColLargeInvoicedQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.ColLargeInvoicedQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColLargeInvoicedQuantity.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColLargeInvoicedQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.ColLargeInvoicedQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColLargeInvoicedQuantity.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColLargeInvoicedQuantity.Caption = "Cantidad"
        Me.ColLargeInvoicedQuantity.FieldName = "InvoicedQuantity"
        Me.ColLargeInvoicedQuantity.MinWidth = 10
        Me.ColLargeInvoicedQuantity.Name = "ColLargeInvoicedQuantity"
        Me.ColLargeInvoicedQuantity.OptionsColumn.AllowEdit = False
        Me.ColLargeInvoicedQuantity.OptionsColumn.AllowFocus = False
        Me.ColLargeInvoicedQuantity.OptionsFilter.AllowAutoFilter = False
        Me.ColLargeInvoicedQuantity.Visible = True
        Me.ColLargeInvoicedQuantity.VisibleIndex = 2
        '
        'ColLargeTotalSalesPrice
        '
        Me.ColLargeTotalSalesPrice.AppearanceCell.Options.UseTextOptions = True
        Me.ColLargeTotalSalesPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColLargeTotalSalesPrice.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColLargeTotalSalesPrice.AppearanceHeader.Options.UseTextOptions = True
        Me.ColLargeTotalSalesPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColLargeTotalSalesPrice.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColLargeTotalSalesPrice.Caption = "V. Unitario"
        Me.ColLargeTotalSalesPrice.ColumnEdit = Me.RepTxtUnitValue
        Me.ColLargeTotalSalesPrice.DisplayFormat.FormatString = "C0"
        Me.ColLargeTotalSalesPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ColLargeTotalSalesPrice.FieldName = "TotalSalesPrice"
        Me.ColLargeTotalSalesPrice.MinWidth = 10
        Me.ColLargeTotalSalesPrice.Name = "ColLargeTotalSalesPrice"
        Me.ColLargeTotalSalesPrice.OptionsFilter.AllowAutoFilter = False
        Me.ColLargeTotalSalesPrice.Visible = True
        Me.ColLargeTotalSalesPrice.VisibleIndex = 4
        '
        'ColLargeGrandTotalSalesPrice
        '
        Me.ColLargeGrandTotalSalesPrice.AppearanceCell.Options.UseTextOptions = True
        Me.ColLargeGrandTotalSalesPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColLargeGrandTotalSalesPrice.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColLargeGrandTotalSalesPrice.AppearanceHeader.Options.UseTextOptions = True
        Me.ColLargeGrandTotalSalesPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColLargeGrandTotalSalesPrice.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColLargeGrandTotalSalesPrice.Caption = "Total"
        Me.ColLargeGrandTotalSalesPrice.DisplayFormat.FormatString = "C0"
        Me.ColLargeGrandTotalSalesPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ColLargeGrandTotalSalesPrice.FieldName = "GrandTotalSalesPrice"
        Me.ColLargeGrandTotalSalesPrice.MinWidth = 10
        Me.ColLargeGrandTotalSalesPrice.Name = "ColLargeGrandTotalSalesPrice"
        Me.ColLargeGrandTotalSalesPrice.OptionsColumn.AllowEdit = False
        Me.ColLargeGrandTotalSalesPrice.OptionsColumn.AllowFocus = False
        Me.ColLargeGrandTotalSalesPrice.OptionsFilter.AllowAutoFilter = False
        Me.ColLargeGrandTotalSalesPrice.Visible = True
        Me.ColLargeGrandTotalSalesPrice.VisibleIndex = 5
        '
        'ColThirdPartySalesPrice
        '
        Me.ColThirdPartySalesPrice.AppearanceCell.Options.UseTextOptions = True
        Me.ColThirdPartySalesPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColThirdPartySalesPrice.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColThirdPartySalesPrice.AppearanceHeader.Options.UseTextOptions = True
        Me.ColThirdPartySalesPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColThirdPartySalesPrice.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColThirdPartySalesPrice.Caption = "V. Entidad"
        Me.ColThirdPartySalesPrice.DisplayFormat.FormatString = "C0"
        Me.ColThirdPartySalesPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ColThirdPartySalesPrice.FieldName = "ThirdPartySalesPrice"
        Me.ColThirdPartySalesPrice.Name = "ColThirdPartySalesPrice"
        Me.ColThirdPartySalesPrice.OptionsColumn.AllowEdit = False
        Me.ColThirdPartySalesPrice.OptionsColumn.AllowFocus = False
        Me.ColThirdPartySalesPrice.Visible = True
        Me.ColThirdPartySalesPrice.VisibleIndex = 6
        '
        'ColSubTotalPatientSalesPrice
        '
        Me.ColSubTotalPatientSalesPrice.AppearanceCell.Options.UseTextOptions = True
        Me.ColSubTotalPatientSalesPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColSubTotalPatientSalesPrice.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColSubTotalPatientSalesPrice.Caption = "V. Cuota Paciente"
        Me.ColSubTotalPatientSalesPrice.DisplayFormat.FormatString = "C0"
        Me.ColSubTotalPatientSalesPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ColSubTotalPatientSalesPrice.FieldName = "SubTotalPatientSalesPrice"
        Me.ColSubTotalPatientSalesPrice.Name = "ColSubTotalPatientSalesPrice"
        Me.ColSubTotalPatientSalesPrice.OptionsColumn.AllowEdit = False
        Me.ColSubTotalPatientSalesPrice.OptionsColumn.AllowFocus = False
        Me.ColSubTotalPatientSalesPrice.Visible = True
        Me.ColSubTotalPatientSalesPrice.VisibleIndex = 7
        '
        'ColPatientPercentage
        '
        Me.ColPatientPercentage.AppearanceCell.Options.UseTextOptions = True
        Me.ColPatientPercentage.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColPatientPercentage.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColPatientPercentage.AppearanceHeader.Options.UseTextOptions = True
        Me.ColPatientPercentage.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColPatientPercentage.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColPatientPercentage.Caption = "% Paciente"
        Me.ColPatientPercentage.DisplayFormat.FormatString = "{0:p}"
        Me.ColPatientPercentage.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.ColPatientPercentage.FieldName = "PatientPercentage"
        Me.ColPatientPercentage.MinWidth = 10
        Me.ColPatientPercentage.Name = "ColPatientPercentage"
        Me.ColPatientPercentage.OptionsColumn.AllowEdit = False
        Me.ColPatientPercentage.OptionsColumn.AllowFocus = False
        Me.ColPatientPercentage.Visible = True
        Me.ColPatientPercentage.VisibleIndex = 8
        '
        'ColRecoveryFeeType
        '
        Me.ColRecoveryFeeType.AppearanceCell.Options.UseTextOptions = True
        Me.ColRecoveryFeeType.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColRecoveryFeeType.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColRecoveryFeeType.AppearanceHeader.Options.UseTextOptions = True
        Me.ColRecoveryFeeType.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColRecoveryFeeType.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColRecoveryFeeType.Caption = "Tipo Aplicado"
        Me.ColRecoveryFeeType.ColumnEdit = Me.RepICBRecoveryFeeType
        Me.ColRecoveryFeeType.FieldName = "RecoveryFeeType"
        Me.ColRecoveryFeeType.MinWidth = 10
        Me.ColRecoveryFeeType.Name = "ColRecoveryFeeType"
        Me.ColRecoveryFeeType.OptionsColumn.AllowEdit = False
        Me.ColRecoveryFeeType.OptionsColumn.AllowFocus = False
        Me.ColRecoveryFeeType.Visible = True
        Me.ColRecoveryFeeType.VisibleIndex = 9
        '
        'ColApplyRecoveryFee
        '
        Me.ColApplyRecoveryFee.AppearanceCell.Options.UseTextOptions = True
        Me.ColApplyRecoveryFee.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColApplyRecoveryFee.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColApplyRecoveryFee.Caption = "Cal. Cuota Paciente"
        Me.ColApplyRecoveryFee.ColumnEdit = Me.RepChkApplyRecoveryFee
        Me.ColApplyRecoveryFee.FieldName = "ApplyRecoveryFee"
        Me.ColApplyRecoveryFee.MinWidth = 10
        Me.ColApplyRecoveryFee.Name = "ColApplyRecoveryFee"
        Me.ColApplyRecoveryFee.Visible = True
        Me.ColApplyRecoveryFee.VisibleIndex = 10
        '
        'ColAuthorizationNumber
        '
        Me.ColAuthorizationNumber.AppearanceCell.Options.UseTextOptions = True
        Me.ColAuthorizationNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColAuthorizationNumber.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColAuthorizationNumber.AppearanceHeader.Options.UseTextOptions = True
        Me.ColAuthorizationNumber.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColAuthorizationNumber.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColAuthorizationNumber.Caption = "No. Autorización"
        Me.ColAuthorizationNumber.FieldName = "AuthorizationNumber"
        Me.ColAuthorizationNumber.MinWidth = 10
        Me.ColAuthorizationNumber.Name = "ColAuthorizationNumber"
        Me.ColAuthorizationNumber.OptionsColumn.AllowEdit = False
        Me.ColAuthorizationNumber.OptionsColumn.AllowFocus = False
        Me.ColAuthorizationNumber.Visible = True
        Me.ColAuthorizationNumber.VisibleIndex = 11
        '
        'ColServiceDate
        '
        Me.ColServiceDate.AppearanceCell.Options.UseTextOptions = True
        Me.ColServiceDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColServiceDate.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColServiceDate.AppearanceHeader.Options.UseTextOptions = True
        Me.ColServiceDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColServiceDate.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColServiceDate.Caption = "Fecha"
        Me.ColServiceDate.FieldName = "ServiceDate"
        Me.ColServiceDate.MinWidth = 10
        Me.ColServiceDate.Name = "ColServiceDate"
        Me.ColServiceDate.OptionsColumn.AllowEdit = False
        Me.ColServiceDate.OptionsColumn.AllowFocus = False
        Me.ColServiceDate.Visible = True
        Me.ColServiceDate.VisibleIndex = 12
        '
        'ColRateManualSalePrice
        '
        Me.ColRateManualSalePrice.AppearanceCell.Options.UseTextOptions = True
        Me.ColRateManualSalePrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColRateManualSalePrice.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColRateManualSalePrice.AppearanceHeader.Options.UseTextOptions = True
        Me.ColRateManualSalePrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColRateManualSalePrice.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColRateManualSalePrice.Caption = "Tarifa"
        Me.ColRateManualSalePrice.DisplayFormat.FormatString = "C0"
        Me.ColRateManualSalePrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ColRateManualSalePrice.FieldName = "RateManualSalePrice"
        Me.ColRateManualSalePrice.MinWidth = 10
        Me.ColRateManualSalePrice.Name = "ColRateManualSalePrice"
        Me.ColRateManualSalePrice.OptionsColumn.AllowEdit = False
        Me.ColRateManualSalePrice.OptionsColumn.AllowFocus = False
        Me.ColRateManualSalePrice.Visible = True
        Me.ColRateManualSalePrice.VisibleIndex = 13
        '
        'ColDistributionType
        '
        Me.ColDistributionType.AppearanceCell.Options.UseTextOptions = True
        Me.ColDistributionType.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColDistributionType.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColDistributionType.AppearanceHeader.Options.UseTextOptions = True
        Me.ColDistributionType.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColDistributionType.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColDistributionType.Caption = "Distribución"
        Me.ColDistributionType.ColumnEdit = Me.RepICBDistributionType
        Me.ColDistributionType.FieldName = "DistributionType"
        Me.ColDistributionType.MinWidth = 10
        Me.ColDistributionType.Name = "ColDistributionType"
        Me.ColDistributionType.OptionsColumn.AllowEdit = False
        Me.ColDistributionType.OptionsColumn.AllowFocus = False
        Me.ColDistributionType.Visible = True
        Me.ColDistributionType.VisibleIndex = 14
        '
        'ColThirdPartyDiscount
        '
        Me.ColThirdPartyDiscount.AppearanceCell.Options.UseTextOptions = True
        Me.ColThirdPartyDiscount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColThirdPartyDiscount.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColThirdPartyDiscount.AppearanceHeader.Options.UseTextOptions = True
        Me.ColThirdPartyDiscount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColThirdPartyDiscount.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColThirdPartyDiscount.Caption = "Des. Entidad"
        Me.ColThirdPartyDiscount.DisplayFormat.FormatString = "C0"
        Me.ColThirdPartyDiscount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ColThirdPartyDiscount.FieldName = "ThirdPartyDiscount"
        Me.ColThirdPartyDiscount.MinWidth = 10
        Me.ColThirdPartyDiscount.Name = "ColThirdPartyDiscount"
        Me.ColThirdPartyDiscount.OptionsColumn.AllowEdit = False
        Me.ColThirdPartyDiscount.OptionsColumn.AllowFocus = False
        Me.ColThirdPartyDiscount.Visible = True
        Me.ColThirdPartyDiscount.VisibleIndex = 3
        '
        'ColSurchargeApply
        '
        Me.ColSurchargeApply.AppearanceCell.Options.UseTextOptions = True
        Me.ColSurchargeApply.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColSurchargeApply.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColSurchargeApply.AppearanceHeader.Options.UseTextOptions = True
        Me.ColSurchargeApply.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColSurchargeApply.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColSurchargeApply.Caption = "Apli. Procedimiento"
        Me.ColSurchargeApply.FieldName = "SurchargeApply"
        Me.ColSurchargeApply.MinWidth = 10
        Me.ColSurchargeApply.Name = "ColSurchargeApply"
        Me.ColSurchargeApply.OptionsColumn.AllowEdit = False
        Me.ColSurchargeApply.OptionsColumn.AllowFocus = False
        Me.ColSurchargeApply.Visible = True
        Me.ColSurchargeApply.VisibleIndex = 15
        '
        'ColLargeServiceBillingGroupCodeName
        '
        Me.ColLargeServiceBillingGroupCodeName.Caption = "Grupo Facturación"
        Me.ColLargeServiceBillingGroupCodeName.FieldName = "ServiceBillingGroupCodeName"
        Me.ColLargeServiceBillingGroupCodeName.Name = "ColLargeServiceBillingGroupCodeName"
        Me.ColLargeServiceBillingGroupCodeName.Visible = True
        Me.ColLargeServiceBillingGroupCodeName.VisibleIndex = 12
        '
        'LcgRoot
        '
        Me.LcgRoot.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LcgRoot.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LcgRoot.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LcgRoot.AppearanceItemCaption.Options.UseFont = True
        Me.LcgRoot.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LcgRoot.AppearanceTabPage.Header.Options.UseFont = True
        Me.LcgRoot.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LcgRoot.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LcgRoot.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LcgRoot.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LcgRoot.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LcgRoot.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LcgRoot.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LcgRoot.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LcgRoot, False)
        Me.LcgRoot.CustomizationFormText = "LcgRoot"
        Me.LcgRoot.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LcgRoot.GroupBordersVisible = False
        Me.LcgRoot.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LiPackagedItems})
        Me.LcgRoot.Location = New System.Drawing.Point(0, 0)
        Me.LcgRoot.Name = "LcgRoot"
        Me.LcgRoot.Size = New System.Drawing.Size(698, 273)
        Me.LcgRoot.Text = "LcgRoot"
        Me.LcgRoot.TextVisible = False
        '
        'LiPackagedItems
        '
        Me.LiPackagedItems.Control = Me.GdcServices
        Me.LiPackagedItems.CustomizationFormText = "LiPackagedItems"
        Me.LiPackagedItems.Location = New System.Drawing.Point(0, 0)
        Me.LiPackagedItems.Name = "LiPackagedItems"
        Me.LiPackagedItems.Size = New System.Drawing.Size(678, 253)
        Me.LiPackagedItems.Text = "LiPackagedItems"
        Me.LiPackagedItems.TextSize = New System.Drawing.Size(0, 0)
        Me.LiPackagedItems.TextToControlDistance = 0
        Me.LiPackagedItems.TextVisible = False
        '
        'IndigoLayoutControl1
        '

        'Me.IndigoLayoutControl1.FormName = "FormBase20150508"

        '
        'FrmItemPackageDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(698, 273)
        Me.Controls.Add(Me.LyRoot)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmItemPackageDetail"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Tag = "756"
        Me.Text = "Detalle Item"
        CType(Me.LyRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LyRoot.ResumeLayout(False)
        CType(Me.GdcServices, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GdvSmallServices, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepTxtUnitValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepICBDistributionType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepICBRecoveryFeeType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepChkApplyRecoveryFee, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepICBIsDistributed, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GdvLargeServices, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LcgRoot, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LiPackagedItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        'CType(Me.IndigoLayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LyRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LcgRoot As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents GdcServices As DevExpress.XtraGrid.GridControl
    Friend WithEvents GdvSmallServices As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepICBIsDistributed As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents ColSmallServiceProductCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColSmallServiceProductName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColSmallInvoicedQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColSmallTotalSalesPrice As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepTxtUnitValue As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents ColSmallGrandTotalSalesPrice As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColSmallServiceBillingGroupCodeName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepICBDistributionType As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents RepICBRecoveryFeeType As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents RepChkApplyRecoveryFee As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GdvLargeServices As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ColLargeServiceProductCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColLargeServiceProductName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColLargeInvoicedQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColLargeTotalSalesPrice As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColLargeGrandTotalSalesPrice As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColThirdPartySalesPrice As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColSubTotalPatientSalesPrice As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColPatientPercentage As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColRecoveryFeeType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColApplyRecoveryFee As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColAuthorizationNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColServiceDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColRateManualSalePrice As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColDistributionType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColThirdPartyDiscount As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColSurchargeApply As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColLargeServiceBillingGroupCodeName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LiPackagedItems As DevExpress.XtraLayout.LayoutControlItem
End Class
