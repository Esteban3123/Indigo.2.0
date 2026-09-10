<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmNoPOSLiquidation
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmNoPOSLiquidation))
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.BtnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnDistribute = New DevExpress.XtraEditors.SimpleButton()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.MpbAsyncOperationBar = New DevExpress.XtraEditors.MarqueeProgressBarControl()
        Me.GdcProductService = New DevExpress.XtraGrid.GridControl()
        Me.INDBgvProducts = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridView()
        Me.GridBand1 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.ColProductServiceCode = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.ColProductServiceDescription = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.ColItemDate = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.ColValTotal = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.ColValueInSource = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gridBand2 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.BandedGridColumn1 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.BandedGridColumn2 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.BandedGridColumn3 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.BandedGridColumn4 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.GleTargetFolio = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColFolioNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LycgDistribution = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LyciTargetFolio = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LyciAsyncOperationBar = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.MpbAsyncOperationBar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GdcProductService, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDBgvProducts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GleTargetFolio.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LycgDistribution, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LyciTargetFolio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LyciAsyncOperationBar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        Me.PanelControl1.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.PanelControl1.Appearance.Options.UseBackColor = True
        Me.PanelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PanelControl1.Controls.Add(Me.BtnCancel)
        Me.PanelControl1.Controls.Add(Me.BtnDistribute)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(0, 454)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(1234, 56)
        Me.PanelControl1.TabIndex = 2
        '
        'BtnCancel
        '
        Me.BtnCancel.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCancel.Appearance.Options.UseFont = True
        Me.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnCancel.Location = New System.Drawing.Point(122, 8)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(100, 36)
        Me.BtnCancel.TabIndex = 1
        Me.BtnCancel.Text = "Cancelar"
        '
        'BtnDistribute
        '
        Me.BtnDistribute.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnDistribute.Appearance.Options.UseFont = True
        Me.BtnDistribute.Enabled = False
        Me.BtnDistribute.Location = New System.Drawing.Point(12, 8)
        Me.BtnDistribute.Name = "BtnDistribute"
        Me.BtnDistribute.Size = New System.Drawing.Size(100, 36)
        Me.BtnDistribute.TabIndex = 0
        Me.BtnDistribute.Text = "Distribuir"
        '
        'LayoutControl1
        '
        Me.LayoutControl1.AllowCustomization = False
        Me.LayoutControl1.Controls.Add(Me.MpbAsyncOperationBar)
        Me.LayoutControl1.Controls.Add(Me.GdcProductService)
        Me.LayoutControl1.Controls.Add(Me.GleTargetFolio)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(2722, 176, 250, 350)
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(1234, 454)
        Me.LayoutControl1.TabIndex = 3
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'MpbAsyncOperationBar
        '
        Me.MpbAsyncOperationBar.EditValue = 0
        Me.MpbAsyncOperationBar.Location = New System.Drawing.Point(0, 9)
        Me.MpbAsyncOperationBar.Name = "MpbAsyncOperationBar"
        Me.MpbAsyncOperationBar.Size = New System.Drawing.Size(1234, 10)
        Me.MpbAsyncOperationBar.StyleController = Me.LayoutControl1
        Me.MpbAsyncOperationBar.TabIndex = 11
        '
        'GdcProductService
        '
        Me.GdcProductService.Cursor = System.Windows.Forms.Cursors.Default
        Me.GdcProductService.Location = New System.Drawing.Point(14, 98)
        Me.GdcProductService.MainView = Me.INDBgvProducts
        Me.GdcProductService.Name = "GdcProductService"
        Me.GdcProductService.Size = New System.Drawing.Size(1206, 342)
        Me.GdcProductService.TabIndex = 10
        Me.GdcProductService.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDBgvProducts})
        '
        'INDBgvProducts
        '
        Me.INDBgvProducts.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDBgvProducts.Appearance.FooterPanel.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.INDBgvProducts.Appearance.FooterPanel.Options.UseFont = True
        Me.INDBgvProducts.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDBgvProducts.Appearance.GroupRow.Options.UseFont = True
        Me.INDBgvProducts.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.INDBgvProducts.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDBgvProducts.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDBgvProducts.Appearance.Row.Options.UseFont = True
        Me.INDBgvProducts.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDBgvProducts.Appearance.ViewCaption.Options.UseFont = True
        Me.INDBgvProducts.Bands.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.GridBand() {Me.GridBand1, Me.gridBand2})
        Me.INDBgvProducts.Columns.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn() {Me.ColProductServiceCode, Me.ColProductServiceDescription, Me.ColItemDate, Me.GridColumn2, Me.GridColumn1, Me.ColValTotal, Me.ColValueInSource, Me.BandedGridColumn1, Me.BandedGridColumn2, Me.BandedGridColumn3, Me.BandedGridColumn4})
        Me.INDBgvProducts.GridControl = Me.GdcProductService
        Me.INDBgvProducts.Name = "INDBgvProducts"
        Me.INDBgvProducts.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown
        Me.INDBgvProducts.OptionsDetail.ShowDetailTabs = False
        Me.INDBgvProducts.OptionsView.EnableAppearanceEvenRow = True
        Me.INDBgvProducts.OptionsView.EnableAppearanceOddRow = True
        Me.INDBgvProducts.OptionsView.ShowAutoFilterRow = True
        Me.INDBgvProducts.OptionsView.ShowDetailButtons = False
        Me.INDBgvProducts.OptionsView.ShowFooter = True
        Me.INDBgvProducts.OptionsView.ShowGroupPanel = False
        Me.INDBgvProducts.OptionsView.ShowIndicator = False
        '
        'GridBand1
        '
        Me.GridBand1.AppearanceHeader.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridBand1.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.GridBand1.AppearanceHeader.Options.UseFont = True
        Me.GridBand1.AppearanceHeader.Options.UseForeColor = True
        Me.GridBand1.AppearanceHeader.Options.UseTextOptions = True
        Me.GridBand1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridBand1.Caption = "Productos No POS"
        Me.GridBand1.Columns.Add(Me.ColProductServiceCode)
        Me.GridBand1.Columns.Add(Me.ColProductServiceDescription)
        Me.GridBand1.Columns.Add(Me.ColItemDate)
        Me.GridBand1.Columns.Add(Me.GridColumn2)
        Me.GridBand1.Columns.Add(Me.GridColumn1)
        Me.GridBand1.Columns.Add(Me.ColValTotal)
        Me.GridBand1.Columns.Add(Me.ColValueInSource)
        Me.GridBand1.Name = "GridBand1"
        Me.GridBand1.VisibleIndex = 0
        Me.GridBand1.Width = 826
        '
        'ColProductServiceCode
        '
        Me.ColProductServiceCode.AppearanceCell.Options.UseTextOptions = True
        Me.ColProductServiceCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColProductServiceCode.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColProductServiceCode.AppearanceHeader.Options.UseTextOptions = True
        Me.ColProductServiceCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColProductServiceCode.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColProductServiceCode.Caption = "Código"
        Me.ColProductServiceCode.FieldName = "Code"
        Me.ColProductServiceCode.Name = "ColProductServiceCode"
        Me.ColProductServiceCode.OptionsColumn.AllowEdit = False
        Me.ColProductServiceCode.OptionsColumn.AllowFocus = False
        Me.ColProductServiceCode.Visible = True
        Me.ColProductServiceCode.Width = 91
        '
        'ColProductServiceDescription
        '
        Me.ColProductServiceDescription.AppearanceHeader.Options.UseTextOptions = True
        Me.ColProductServiceDescription.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColProductServiceDescription.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColProductServiceDescription.Caption = "Descripción"
        Me.ColProductServiceDescription.FieldName = "Description"
        Me.ColProductServiceDescription.Name = "ColProductServiceDescription"
        Me.ColProductServiceDescription.OptionsColumn.AllowEdit = False
        Me.ColProductServiceDescription.OptionsColumn.AllowFocus = False
        Me.ColProductServiceDescription.Visible = True
        Me.ColProductServiceDescription.Width = 158
        '
        'ColItemDate
        '
        Me.ColItemDate.AppearanceHeader.Options.UseTextOptions = True
        Me.ColItemDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColItemDate.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColItemDate.Caption = "Fecha"
        Me.ColItemDate.FieldName = "ItemDate"
        Me.ColItemDate.Name = "ColItemDate"
        Me.ColItemDate.OptionsColumn.AllowEdit = False
        Me.ColItemDate.OptionsColumn.AllowFocus = False
        Me.ColItemDate.Visible = True
        Me.ColItemDate.Width = 96
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Unidad Funcional"
        Me.GridColumn2.FieldName = "FunctionalUnitCodeName"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.Width = 179
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Cantidad"
        Me.GridColumn1.FieldName = "InvoiceQuantity"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.Width = 73
        '
        'ColValTotal
        '
        Me.ColValTotal.AppearanceCell.Options.UseTextOptions = True
        Me.ColValTotal.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.ColValTotal.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColValTotal.AppearanceHeader.Options.UseTextOptions = True
        Me.ColValTotal.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColValTotal.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColValTotal.Caption = "Val. Unitario"
        Me.ColValTotal.FieldName = "TotalFolioValue"
        Me.ColValTotal.Name = "ColValTotal"
        Me.ColValTotal.OptionsColumn.AllowEdit = False
        Me.ColValTotal.OptionsColumn.AllowFocus = False
        Me.ColValTotal.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalFolioValue", "{0:c2}")})
        Me.ColValTotal.Visible = True
        Me.ColValTotal.Width = 97
        '
        'ColValueInSource
        '
        Me.ColValueInSource.AppearanceCell.Options.UseTextOptions = True
        Me.ColValueInSource.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.ColValueInSource.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColValueInSource.AppearanceHeader.Options.UseTextOptions = True
        Me.ColValueInSource.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColValueInSource.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColValueInSource.Caption = "Val. Total"
        Me.ColValueInSource.FieldName = "SourceFolioValue"
        Me.ColValueInSource.Name = "ColValueInSource"
        Me.ColValueInSource.OptionsColumn.AllowEdit = False
        Me.ColValueInSource.OptionsColumn.AllowFocus = False
        Me.ColValueInSource.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "SourceFolioValue", "{0:c2}")})
        Me.ColValueInSource.Visible = True
        Me.ColValueInSource.Width = 132
        '
        'gridBand2
        '
        Me.gridBand2.AppearanceHeader.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridBand2.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.gridBand2.AppearanceHeader.Options.UseFont = True
        Me.gridBand2.AppearanceHeader.Options.UseForeColor = True
        Me.gridBand2.AppearanceHeader.Options.UseTextOptions = True
        Me.gridBand2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gridBand2.Caption = "Productos POS"
        Me.gridBand2.Columns.Add(Me.BandedGridColumn1)
        Me.gridBand2.Columns.Add(Me.BandedGridColumn2)
        Me.gridBand2.Columns.Add(Me.BandedGridColumn3)
        Me.gridBand2.Columns.Add(Me.BandedGridColumn4)
        Me.gridBand2.Name = "gridBand2"
        Me.gridBand2.VisibleIndex = 1
        Me.gridBand2.Width = 570
        '
        'BandedGridColumn1
        '
        Me.BandedGridColumn1.Caption = "Código"
        Me.BandedGridColumn1.Name = "BandedGridColumn1"
        Me.BandedGridColumn1.Visible = True
        Me.BandedGridColumn1.Width = 135
        '
        'BandedGridColumn2
        '
        Me.BandedGridColumn2.Caption = "Nombre"
        Me.BandedGridColumn2.Name = "BandedGridColumn2"
        Me.BandedGridColumn2.Visible = True
        Me.BandedGridColumn2.Width = 205
        '
        'BandedGridColumn3
        '
        Me.BandedGridColumn3.Caption = "Val. Unitario"
        Me.BandedGridColumn3.Name = "BandedGridColumn3"
        Me.BandedGridColumn3.Visible = True
        Me.BandedGridColumn3.Width = 145
        '
        'BandedGridColumn4
        '
        Me.BandedGridColumn4.Caption = "Val. Total"
        Me.BandedGridColumn4.Name = "BandedGridColumn4"
        Me.BandedGridColumn4.Visible = True
        Me.BandedGridColumn4.Width = 85
        '
        'GleTargetFolio
        '
        Me.GleTargetFolio.EditValue = "-1"
        Me.GleTargetFolio.EnterMoveNextControl = True
        Me.GleTargetFolio.Location = New System.Drawing.Point(171, 66)
        Me.GleTargetFolio.Name = "GleTargetFolio"
        Me.GleTargetFolio.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.GleTargetFolio.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GleTargetFolio.Properties.Appearance.Options.UseBackColor = True
        Me.GleTargetFolio.Properties.Appearance.Options.UseFont = True
        Me.GleTargetFolio.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.GleTargetFolio.Properties.DisplayMember = "FolioNumber"
        Me.GleTargetFolio.Properties.ImmediatePopup = True
        Me.GleTargetFolio.Properties.NullText = ""
        Me.GleTargetFolio.Properties.PopupFormMinSize = New System.Drawing.Size(181, 100)
        Me.GleTargetFolio.Properties.PopupFormSize = New System.Drawing.Size(181, 100)
        Me.GleTargetFolio.Properties.PopupSizeable = False
        Me.GleTargetFolio.Properties.PopupView = Me.GridLookUpEdit1View
        Me.GleTargetFolio.Properties.ShowFooter = False
        Me.GleTargetFolio.Properties.ShowPopupShadow = False
        Me.GleTargetFolio.Properties.ValueMember = "FolioId"
        Me.GleTargetFolio.Size = New System.Drawing.Size(233, 28)
        Me.GleTargetFolio.StyleController = Me.LayoutControl1
        Me.GleTargetFolio.TabIndex = 8
        '
        'GridLookUpEdit1View
        '
        Me.GridLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.GridLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColFolioNumber})
        Me.GridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit1View.Name = "GridLookUpEdit1View"
        Me.GridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.GridLookUpEdit1View.OptionsView.ShowIndicator = False
        '
        'ColFolioNumber
        '
        Me.ColFolioNumber.AppearanceCell.Options.UseTextOptions = True
        Me.ColFolioNumber.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColFolioNumber.AppearanceHeader.Options.UseTextOptions = True
        Me.ColFolioNumber.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColFolioNumber.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColFolioNumber.Caption = "No. de Folio"
        Me.ColFolioNumber.FieldName = "FolioNumber"
        Me.ColFolioNumber.Name = "ColFolioNumber"
        Me.ColFolioNumber.Visible = True
        Me.ColFolioNumber.VisibleIndex = 0
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LycgDistribution, Me.LyciAsyncOperationBar, Me.EmptySpaceItem1})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1234, 454)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LycgDistribution
        '
        Me.LycgDistribution.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LycgDistribution.AppearanceGroup.Options.UseFont = True
        Me.LycgDistribution.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LycgDistribution.AppearanceItemCaption.Options.UseFont = True
        Me.LycgDistribution.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LycgDistribution.AppearanceTabPage.Header.Options.UseFont = True
        Me.LycgDistribution.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LycgDistribution.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LycgDistribution.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LycgDistribution.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LycgDistribution.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LycgDistribution.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LycgDistribution.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LycgDistribution.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.LycgDistribution.CustomizationFormText = "Distribución de Servicios y Productos"
        Me.LycgDistribution.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem3, Me.LyciTargetFolio})
        Me.LycgDistribution.Location = New System.Drawing.Point(0, 19)
        Me.LycgDistribution.Name = "LycgDistribution"
        Me.LycgDistribution.Size = New System.Drawing.Size(1234, 435)
        Me.LycgDistribution.Text = "Distribución de Productos No POS"
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.GdcProductService
        Me.LayoutControlItem3.CustomizationFormText = "LayoutControlItem3"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 32)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(104, 24)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(1210, 346)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = False
        '
        'LyciTargetFolio
        '
        Me.LyciTargetFolio.Control = Me.GleTargetFolio
        Me.LyciTargetFolio.CustomizationFormText = "LayoutControlItem5"
        Me.LyciTargetFolio.Location = New System.Drawing.Point(0, 0)
        Me.LyciTargetFolio.MaxSize = New System.Drawing.Size(394, 32)
        Me.LyciTargetFolio.MinSize = New System.Drawing.Size(394, 32)
        Me.LyciTargetFolio.Name = "LyciTargetFolio"
        Me.LyciTargetFolio.Size = New System.Drawing.Size(1210, 32)
        Me.LyciTargetFolio.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LyciTargetFolio.Text = "Folio Destino"
        Me.LyciTargetFolio.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LyciTargetFolio.TextSize = New System.Drawing.Size(145, 21)
        Me.LyciTargetFolio.TextToControlDistance = 12
        '
        'LyciAsyncOperationBar
        '
        Me.LyciAsyncOperationBar.Control = Me.MpbAsyncOperationBar
        Me.LyciAsyncOperationBar.CustomizationFormText = "LyciAsyncOperationBar"
        Me.LyciAsyncOperationBar.Location = New System.Drawing.Point(0, 9)
        Me.LyciAsyncOperationBar.MaxSize = New System.Drawing.Size(0, 10)
        Me.LyciAsyncOperationBar.MinSize = New System.Drawing.Size(1, 10)
        Me.LyciAsyncOperationBar.Name = "LyciAsyncOperationBar"
        Me.LyciAsyncOperationBar.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LyciAsyncOperationBar.Size = New System.Drawing.Size(1234, 10)
        Me.LyciAsyncOperationBar.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LyciAsyncOperationBar.TextSize = New System.Drawing.Size(0, 0)
        Me.LyciAsyncOperationBar.TextVisible = False
        Me.LyciAsyncOperationBar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 0)
        Me.EmptySpaceItem1.MinSize = New System.Drawing.Size(1, 1)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(1234, 9)
        Me.EmptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'FrmNoPOSLiquidation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1234, 510)
        Me.Controls.Add(Me.LayoutControl1)
        Me.Controls.Add(Me.PanelControl1)
        Me.IconOptions.SvgImage = CType(resources.GetObject("FrmNoPOSLiquidation.IconOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.IconOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.None
        Me.Name = "FrmNoPOSLiquidation"
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.MpbAsyncOperationBar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GdcProductService, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDBgvProducts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GleTargetFolio.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LycgDistribution, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LyciTargetFolio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LyciAsyncOperationBar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents BtnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnDistribute As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents MpbAsyncOperationBar As DevExpress.XtraEditors.MarqueeProgressBarControl
    Friend WithEvents GdcProductService As DevExpress.XtraGrid.GridControl
    Friend WithEvents GleTargetFolio As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ColFolioNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LycgDistribution As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LyciTargetFolio As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LyciAsyncOperationBar As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents INDBgvProducts As DevExpress.XtraGrid.Views.BandedGrid.BandedGridView
    Friend WithEvents GridBand1 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents ColProductServiceCode As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents ColProductServiceDescription As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents ColItemDate As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents ColValTotal As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents ColValueInSource As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gridBand2 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents BandedGridColumn1 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents BandedGridColumn2 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents BandedGridColumn3 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents BandedGridColumn4 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
End Class
