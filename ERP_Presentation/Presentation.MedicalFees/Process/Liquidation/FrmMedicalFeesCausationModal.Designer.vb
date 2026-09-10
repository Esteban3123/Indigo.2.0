Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmMedicalFeesCausationModal
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
        Me.RepositoryItemPopupContainerEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.INDgcMedicalFeesCausation = New DevExpress.XtraGrid.GridControl()
        Me.ViewNoSurgical = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepTxtRateManualSalesPrice = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.INDcolHealthProfessional = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepSleHealthProfessionalGridNoSurgical = New DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit()
        Me.RepositoryItemSearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn36 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn37 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn38 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolCausedValueNoSurgical = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepTxtCausedValue = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.GridColumn21 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepTxtTotalAmountPayable = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.GridColumn40 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepTxtSubTotalSalesPriceNoSurgical = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.GridColumn41 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepTxtPercentageDiscountNoSurgical = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepTxtGrandTotalNoQx = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.GridColumn45 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn20 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn47 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn23 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn24 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn25 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn26 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn27 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn28 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolOnlyNoQx = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn49 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepTxtTotalAmountPayableRealNoSurgical = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.GridColumn50 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepTxtPercentageCashedNoSurgical = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.INDcolColorNoSurgical = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepColorNoSurgical = New DevExpress.XtraEditors.Repository.RepositoryItemColorPickEdit()
        Me.GridColumn55 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepCheckSelectOption = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcMedicalFeesCausation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewNoSurgical, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepTxtRateManualSalesPrice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepSleHealthProfessionalGridNoSurgical, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemSearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepTxtCausedValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepTxtTotalAmountPayable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepTxtSubTotalSalesPriceNoSurgical, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepTxtPercentageDiscountNoSurgical, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepTxtGrandTotalNoQx, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepTxtTotalAmountPayableRealNoSurgical, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepTxtPercentageCashedNoSurgical, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepColorNoSurgical, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepCheckSelectOption, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDgcMedicalFeesCausation)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(919, 408)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(919, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(919, 130)
        '
        'RepositoryItemPopupContainerEdit2
        '
        Me.RepositoryItemPopupContainerEdit2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit2.Name = "RepositoryItemPopupContainerEdit2"
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit2
        '
        'INDgcMedicalFeesCausation
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcMedicalFeesCausation, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcMedicalFeesCausation, Nothing)
        Me.INDgcMedicalFeesCausation.Cursor = System.Windows.Forms.Cursors.Default
        Me.INDgcMedicalFeesCausation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.IndigoGridControl1.SetExportButton(Me.INDgcMedicalFeesCausation, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcMedicalFeesCausation, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcMedicalFeesCausation, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcMedicalFeesCausation, False)
        Me.INDgcMedicalFeesCausation.Location = New System.Drawing.Point(2, 7)
        Me.INDgcMedicalFeesCausation.MainView = Me.ViewNoSurgical
        Me.INDgcMedicalFeesCausation.Name = "INDgcMedicalFeesCausation"
        Me.INDgcMedicalFeesCausation.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrepTxtRateManualSalesPrice, Me.INDrepCheckSelectOption, Me.INDrepTxtCausedValue, Me.INDrepTxtTotalAmountPayable, Me.INDrepSleHealthProfessionalGridNoSurgical, Me.INDrepTxtSubTotalSalesPriceNoSurgical, Me.INDrepTxtPercentageDiscountNoSurgical, Me.INDrepTxtGrandTotalNoQx, Me.INDrepTxtTotalAmountPayableRealNoSurgical, Me.INDrepTxtPercentageCashedNoSurgical, Me.INDrepColorNoSurgical})
        Me.INDgcMedicalFeesCausation.Size = New System.Drawing.Size(915, 399)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcMedicalFeesCausation, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcMedicalFeesCausation.TabIndex = 5
        Me.INDgcMedicalFeesCausation.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewNoSurgical})
        '
        'ViewNoSurgical
        '
        Me.ViewNoSurgical.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.ViewNoSurgical.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.ViewNoSurgical.Appearance.FocusedRow.Options.UseBackColor = True
        Me.ViewNoSurgical.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.ViewNoSurgical.Appearance.FocusedRow.Options.UseFont = True
        Me.ViewNoSurgical.Appearance.FocusedRow.Options.UseForeColor = True
        Me.ViewNoSurgical.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ViewNoSurgical.Appearance.GroupRow.Options.UseFont = True
        Me.ViewNoSurgical.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ViewNoSurgical.Appearance.HeaderPanel.Options.UseFont = True
        Me.ViewNoSurgical.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.ViewNoSurgical.Appearance.Row.Options.UseFont = True
        Me.ViewNoSurgical.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.ViewNoSurgical.Appearance.ViewCaption.Options.UseFont = True
        Me.ViewNoSurgical.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn7, Me.GridColumn8, Me.GridColumn9, Me.GridColumn10, Me.INDcolHealthProfessional, Me.GridColumn38, Me.INDcolCausedValueNoSurgical, Me.GridColumn21, Me.GridColumn40, Me.GridColumn41, Me.GridColumn13, Me.GridColumn45, Me.GridColumn20, Me.GridColumn47, Me.GridColumn23, Me.GridColumn24, Me.GridColumn25, Me.GridColumn26, Me.GridColumn27, Me.GridColumn28, Me.INDcolOnlyNoQx, Me.GridColumn49, Me.GridColumn50, Me.INDcolColorNoSurgical, Me.GridColumn55})
        GridFormatRule1.ApplyToRow = True
        GridFormatRule1.Column = Me.INDcolOnlyNoQx
        GridFormatRule1.Name = "Format0"
        FormatConditionRuleValue1.Appearance.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!)
        FormatConditionRuleValue1.Appearance.Options.UseFont = True
        FormatConditionRuleValue1.Condition = DevExpress.XtraEditors.FormatCondition.Equal
        FormatConditionRuleValue1.Value1 = True
        GridFormatRule1.Rule = FormatConditionRuleValue1
        Me.ViewNoSurgical.FormatRules.Add(GridFormatRule1)
        Me.ViewNoSurgical.GridControl = Me.INDgcMedicalFeesCausation
        Me.ViewNoSurgical.GroupCount = 1
        Me.ViewNoSurgical.Name = "ViewNoSurgical"
        Me.ViewNoSurgical.OptionsView.EnableAppearanceEvenRow = True
        Me.ViewNoSurgical.OptionsView.EnableAppearanceOddRow = True
        Me.ViewNoSurgical.OptionsView.ShowAutoFilterRow = True
        Me.ViewNoSurgical.OptionsView.ShowDetailButtons = False
        Me.ViewNoSurgical.OptionsView.ShowGroupPanel = False
        Me.ViewNoSurgical.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn7, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.ViewNoSurgical, False)
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Concepto Facturación"
        Me.GridColumn7.FieldName = "BillingGroupDescription"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsColumn.AllowEdit = False
        Me.GridColumn7.OptionsColumn.AllowFocus = False
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 1
        Me.GridColumn7.Width = 250
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Servicio IPS"
        Me.GridColumn8.FieldName = "IPSServiceDescription"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.OptionsColumn.AllowEdit = False
        Me.GridColumn8.OptionsColumn.AllowFocus = False
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 0
        Me.GridColumn8.Width = 219
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Cant."
        Me.GridColumn9.FieldName = "InvoicedQuantity"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.OptionsColumn.AllowEdit = False
        Me.GridColumn9.OptionsColumn.AllowFocus = False
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 1
        Me.GridColumn9.Width = 56
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "Val. Unit."
        Me.GridColumn10.ColumnEdit = Me.INDrepTxtRateManualSalesPrice
        Me.GridColumn10.DisplayFormat.FormatString = "c0"
        Me.GridColumn10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn10.FieldName = "TotalSalesPrice"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.OptionsColumn.AllowEdit = False
        Me.GridColumn10.OptionsColumn.AllowFocus = False
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 2
        Me.GridColumn10.Width = 72
        '
        'INDrepTxtRateManualSalesPrice
        '
        Me.INDrepTxtRateManualSalesPrice.AutoHeight = False
        Me.INDrepTxtRateManualSalesPrice.Mask.EditMask = "C2"
        Me.INDrepTxtRateManualSalesPrice.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDrepTxtRateManualSalesPrice.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepTxtRateManualSalesPrice.Name = "INDrepTxtRateManualSalesPrice"
        '
        'INDcolHealthProfessional
        '
        Me.INDcolHealthProfessional.Caption = "Médico"
        Me.INDcolHealthProfessional.ColumnEdit = Me.INDrepSleHealthProfessionalGridNoSurgical
        Me.INDcolHealthProfessional.FieldName = "PerformsHealthProfessionalCode"
        Me.INDcolHealthProfessional.Name = "INDcolHealthProfessional"
        Me.INDcolHealthProfessional.Visible = True
        Me.INDcolHealthProfessional.VisibleIndex = 3
        Me.INDcolHealthProfessional.Width = 237
        '
        'INDrepSleHealthProfessionalGridNoSurgical
        '
        Me.INDrepSleHealthProfessionalGridNoSurgical.AutoHeight = False
        Me.INDrepSleHealthProfessionalGridNoSurgical.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrepSleHealthProfessionalGridNoSurgical.DisplayMember = "CodeName"
        Me.INDrepSleHealthProfessionalGridNoSurgical.Name = "INDrepSleHealthProfessionalGridNoSurgical"
        Me.INDrepSleHealthProfessionalGridNoSurgical.NullText = ""
        Me.INDrepSleHealthProfessionalGridNoSurgical.PopupView = Me.RepositoryItemSearchLookUpEdit1View
        Me.INDrepSleHealthProfessionalGridNoSurgical.ValueMember = "CODPROSAL"
        '
        'RepositoryItemSearchLookUpEdit1View
        '
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.FocusedRow.Options.UseForeColor = True
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.RepositoryItemSearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn36, Me.GridColumn37})
        Me.RepositoryItemSearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.RepositoryItemSearchLookUpEdit1View.Name = "RepositoryItemSearchLookUpEdit1View"
        Me.RepositoryItemSearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.RepositoryItemSearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.RepositoryItemSearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.RepositoryItemSearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.RepositoryItemSearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.RepositoryItemSearchLookUpEdit1View, False)
        '
        'GridColumn36
        '
        Me.GridColumn36.Caption = "Código"
        Me.GridColumn36.FieldName = "CODPROSAL"
        Me.GridColumn36.Name = "GridColumn36"
        Me.GridColumn36.Visible = True
        Me.GridColumn36.VisibleIndex = 0
        Me.GridColumn36.Width = 289
        '
        'GridColumn37
        '
        Me.GridColumn37.Caption = "Nombre"
        Me.GridColumn37.FieldName = "NOMMEDICO"
        Me.GridColumn37.Name = "GridColumn37"
        Me.GridColumn37.Visible = True
        Me.GridColumn37.VisibleIndex = 1
        Me.GridColumn37.Width = 1343
        '
        'GridColumn38
        '
        Me.GridColumn38.Caption = "Contrato"
        Me.GridColumn38.FieldName = "MedicalFeesContractCodeName"
        Me.GridColumn38.Name = "GridColumn38"
        Me.GridColumn38.OptionsColumn.AllowEdit = False
        Me.GridColumn38.OptionsColumn.AllowFocus = False
        Me.GridColumn38.Visible = True
        Me.GridColumn38.VisibleIndex = 4
        Me.GridColumn38.Width = 80
        '
        'INDcolCausedValueNoSurgical
        '
        Me.INDcolCausedValueNoSurgical.Caption = "Valor Causado"
        Me.INDcolCausedValueNoSurgical.ColumnEdit = Me.INDrepTxtCausedValue
        Me.INDcolCausedValueNoSurgical.DisplayFormat.FormatString = "c0"
        Me.INDcolCausedValueNoSurgical.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDcolCausedValueNoSurgical.FieldName = "AmountPayable"
        Me.INDcolCausedValueNoSurgical.Name = "INDcolCausedValueNoSurgical"
        Me.INDcolCausedValueNoSurgical.Visible = True
        Me.INDcolCausedValueNoSurgical.VisibleIndex = 5
        Me.INDcolCausedValueNoSurgical.Width = 184
        '
        'INDrepTxtCausedValue
        '
        Me.INDrepTxtCausedValue.AutoHeight = False
        Me.INDrepTxtCausedValue.Mask.EditMask = "C2"
        Me.INDrepTxtCausedValue.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDrepTxtCausedValue.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepTxtCausedValue.Name = "INDrepTxtCausedValue"
        '
        'GridColumn21
        '
        Me.GridColumn21.Caption = "Val. Total"
        Me.GridColumn21.ColumnEdit = Me.INDrepTxtTotalAmountPayable
        Me.GridColumn21.DisplayFormat.FormatString = "c0"
        Me.GridColumn21.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn21.FieldName = "TotalAmountPayable"
        Me.GridColumn21.Name = "GridColumn21"
        Me.GridColumn21.OptionsColumn.AllowEdit = False
        Me.GridColumn21.OptionsColumn.AllowFocus = False
        Me.GridColumn21.Visible = True
        Me.GridColumn21.VisibleIndex = 6
        Me.GridColumn21.Width = 101
        '
        'INDrepTxtTotalAmountPayable
        '
        Me.INDrepTxtTotalAmountPayable.AutoHeight = False
        Me.INDrepTxtTotalAmountPayable.Mask.EditMask = "C2"
        Me.INDrepTxtTotalAmountPayable.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDrepTxtTotalAmountPayable.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepTxtTotalAmountPayable.Name = "INDrepTxtTotalAmountPayable"
        '
        'GridColumn40
        '
        Me.GridColumn40.Caption = "Sub Val. Unit."
        Me.GridColumn40.ColumnEdit = Me.INDrepTxtSubTotalSalesPriceNoSurgical
        Me.GridColumn40.FieldName = "SubTotalSalesPrice"
        Me.GridColumn40.Name = "GridColumn40"
        Me.GridColumn40.OptionsColumn.AllowEdit = False
        Me.GridColumn40.OptionsColumn.AllowFocus = False
        '
        'INDrepTxtSubTotalSalesPriceNoSurgical
        '
        Me.INDrepTxtSubTotalSalesPriceNoSurgical.AutoHeight = False
        Me.INDrepTxtSubTotalSalesPriceNoSurgical.Mask.EditMask = "C2"
        Me.INDrepTxtSubTotalSalesPriceNoSurgical.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDrepTxtSubTotalSalesPriceNoSurgical.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepTxtSubTotalSalesPriceNoSurgical.Name = "INDrepTxtSubTotalSalesPriceNoSurgical"
        '
        'GridColumn41
        '
        Me.GridColumn41.Caption = "% Descuento"
        Me.GridColumn41.ColumnEdit = Me.INDrepTxtPercentageDiscountNoSurgical
        Me.GridColumn41.FieldName = "ThirdPartyDiscountPercentage"
        Me.GridColumn41.Name = "GridColumn41"
        Me.GridColumn41.OptionsColumn.AllowEdit = False
        Me.GridColumn41.OptionsColumn.AllowFocus = False
        '
        'INDrepTxtPercentageDiscountNoSurgical
        '
        Me.INDrepTxtPercentageDiscountNoSurgical.AutoHeight = False
        Me.INDrepTxtPercentageDiscountNoSurgical.Mask.EditMask = "P2"
        Me.INDrepTxtPercentageDiscountNoSurgical.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDrepTxtPercentageDiscountNoSurgical.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepTxtPercentageDiscountNoSurgical.Name = "INDrepTxtPercentageDiscountNoSurgical"
        '
        'GridColumn13
        '
        Me.GridColumn13.Caption = "Total Cobrado a Entidad"
        Me.GridColumn13.ColumnEdit = Me.INDrepTxtGrandTotalNoQx
        Me.GridColumn13.DisplayFormat.FormatString = "C0"
        Me.GridColumn13.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn13.FieldName = "GrandTotalSalesPrice"
        Me.GridColumn13.Name = "GridColumn13"
        '
        'INDrepTxtGrandTotalNoQx
        '
        Me.INDrepTxtGrandTotalNoQx.AutoHeight = False
        Me.INDrepTxtGrandTotalNoQx.Mask.EditMask = "C0"
        Me.INDrepTxtGrandTotalNoQx.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDrepTxtGrandTotalNoQx.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepTxtGrandTotalNoQx.Name = "INDrepTxtGrandTotalNoQx"
        '
        'GridColumn45
        '
        Me.GridColumn45.Caption = "Unidad Funcional"
        Me.GridColumn45.FieldName = "FunctionalUnitDescription"
        Me.GridColumn45.Name = "GridColumn45"
        '
        'GridColumn20
        '
        Me.GridColumn20.Caption = "Fecha Servicio"
        Me.GridColumn20.FieldName = "ServiceDate"
        Me.GridColumn20.Name = "GridColumn20"
        '
        'GridColumn47
        '
        Me.GridColumn47.Caption = "Usuario Orden"
        Me.GridColumn47.FieldName = "CreationUserServiceOrder"
        Me.GridColumn47.Name = "GridColumn47"
        '
        'GridColumn23
        '
        Me.GridColumn23.Caption = "Usuario Creación"
        Me.GridColumn23.FieldName = "CreationUser"
        Me.GridColumn23.Name = "GridColumn23"
        '
        'GridColumn24
        '
        Me.GridColumn24.Caption = "Fecha Creación"
        Me.GridColumn24.FieldName = "CreationDate"
        Me.GridColumn24.Name = "GridColumn24"
        '
        'GridColumn25
        '
        Me.GridColumn25.Caption = "Usuario Modificación"
        Me.GridColumn25.FieldName = "ModificationUser"
        Me.GridColumn25.Name = "GridColumn25"
        '
        'GridColumn26
        '
        Me.GridColumn26.Caption = "Fecha Modificación"
        Me.GridColumn26.FieldName = "ModificationDate"
        Me.GridColumn26.Name = "GridColumn26"
        '
        'GridColumn27
        '
        Me.GridColumn27.Caption = "Usuario Confirmación"
        Me.GridColumn27.FieldName = "ConfirmationUser"
        Me.GridColumn27.Name = "GridColumn27"
        '
        'GridColumn28
        '
        Me.GridColumn28.Caption = "Fecha Confirmación"
        Me.GridColumn28.FieldName = "ConfirmationDate"
        Me.GridColumn28.Name = "GridColumn28"
        '
        'INDcolOnlyNoQx
        '
        Me.INDcolOnlyNoQx.Caption = "Agre. Manu."
        Me.INDcolOnlyNoQx.FieldName = "OnlyMedicalFees"
        Me.INDcolOnlyNoQx.Name = "INDcolOnlyNoQx"
        '
        'GridColumn49
        '
        Me.GridColumn49.Caption = "Valor Causado Real"
        Me.GridColumn49.ColumnEdit = Me.INDrepTxtTotalAmountPayableRealNoSurgical
        Me.GridColumn49.DisplayFormat.FormatString = "C0"
        Me.GridColumn49.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn49.FieldName = "TotalAmountPayableReal"
        Me.GridColumn49.Name = "GridColumn49"
        '
        'INDrepTxtTotalAmountPayableRealNoSurgical
        '
        Me.INDrepTxtTotalAmountPayableRealNoSurgical.AutoHeight = False
        Me.INDrepTxtTotalAmountPayableRealNoSurgical.Mask.EditMask = "C0"
        Me.INDrepTxtTotalAmountPayableRealNoSurgical.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDrepTxtTotalAmountPayableRealNoSurgical.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepTxtTotalAmountPayableRealNoSurgical.Name = "INDrepTxtTotalAmountPayableRealNoSurgical"
        '
        'GridColumn50
        '
        Me.GridColumn50.Caption = "Porcentaje"
        Me.GridColumn50.ColumnEdit = Me.INDrepTxtPercentageCashedNoSurgical
        Me.GridColumn50.DisplayFormat.FormatString = "P2"
        Me.GridColumn50.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn50.FieldName = "PercentageCashed"
        Me.GridColumn50.Name = "GridColumn50"
        '
        'INDrepTxtPercentageCashedNoSurgical
        '
        Me.INDrepTxtPercentageCashedNoSurgical.AutoHeight = False
        Me.INDrepTxtPercentageCashedNoSurgical.Mask.EditMask = "P2"
        Me.INDrepTxtPercentageCashedNoSurgical.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDrepTxtPercentageCashedNoSurgical.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepTxtPercentageCashedNoSurgical.Name = "INDrepTxtPercentageCashedNoSurgical"
        '
        'INDcolColorNoSurgical
        '
        Me.INDcolColorNoSurgical.Caption = "Estado"
        Me.INDcolColorNoSurgical.ColumnEdit = Me.INDrepColorNoSurgical
        Me.INDcolColorNoSurgical.FieldName = "Color"
        Me.INDcolColorNoSurgical.Name = "INDcolColorNoSurgical"
        Me.INDcolColorNoSurgical.OptionsColumn.AllowEdit = False
        Me.INDcolColorNoSurgical.OptionsColumn.AllowFocus = False
        '
        'INDrepColorNoSurgical
        '
        Me.INDrepColorNoSurgical.AutoHeight = False
        Me.INDrepColorNoSurgical.AutomaticColor = System.Drawing.Color.Black
        Me.INDrepColorNoSurgical.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrepColorNoSurgical.ColorAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDrepColorNoSurgical.Name = "INDrepColorNoSurgical"
        '
        'GridColumn55
        '
        Me.GridColumn55.Caption = "Fecha Causación"
        Me.GridColumn55.FieldName = "CausationDate"
        Me.GridColumn55.Name = "GridColumn55"
        Me.GridColumn55.OptionsColumn.AllowEdit = False
        Me.GridColumn55.OptionsColumn.AllowFocus = False
        '
        'INDrepCheckSelectOption
        '
        Me.INDrepCheckSelectOption.AutoHeight = False
        Me.INDrepCheckSelectOption.Name = "INDrepCheckSelectOption"
        '
        'FrmMedicalFeesCausationModal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(919, 543)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.IconOptions.ShowIcon = False
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmMedicalFeesCausationModal"
        Me.Opacity = 1.0R
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Detalle del pago"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcMedicalFeesCausation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewNoSurgical, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepTxtRateManualSalesPrice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepSleHealthProfessionalGridNoSurgical, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemSearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepTxtCausedValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepTxtTotalAmountPayable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepTxtSubTotalSalesPriceNoSurgical, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepTxtPercentageDiscountNoSurgical, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepTxtGrandTotalNoQx, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepTxtTotalAmountPayableRealNoSurgical, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepTxtPercentageCashedNoSurgical, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepColorNoSurgical, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepCheckSelectOption, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents IndigoGridControl1 As Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Controls.IndigoGridView
    Friend WithEvents IndigoSearchLookUpControl1 As Controls.IndigoSearchLookUpControl
    Friend WithEvents RepositoryItemPopupContainerEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDgcMedicalFeesCausation As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewNoSurgical As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepTxtRateManualSalesPrice As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents INDcolHealthProfessional As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepSleHealthProfessionalGridNoSurgical As DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit
    Friend WithEvents RepositoryItemSearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn38 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolCausedValueNoSurgical As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepTxtCausedValue As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents GridColumn21 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepTxtTotalAmountPayable As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents GridColumn40 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepTxtSubTotalSalesPriceNoSurgical As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents GridColumn41 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepTxtPercentageDiscountNoSurgical As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepTxtGrandTotalNoQx As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents GridColumn45 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn20 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn47 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn23 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn24 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn25 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn26 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn27 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn28 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolOnlyNoQx As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn49 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepTxtTotalAmountPayableRealNoSurgical As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents GridColumn50 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepTxtPercentageCashedNoSurgical As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents INDcolColorNoSurgical As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepColorNoSurgical As DevExpress.XtraEditors.Repository.RepositoryItemColorPickEdit
    Friend WithEvents GridColumn55 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepCheckSelectOption As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GridColumn36 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn37 As DevExpress.XtraGrid.Columns.GridColumn
End Class
