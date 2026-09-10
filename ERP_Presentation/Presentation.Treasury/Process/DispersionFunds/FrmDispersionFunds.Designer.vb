Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDispersionFunds
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
        Dim PivotGridGroup1 As DevExpress.XtraPivotGrid.PivotGridGroup = New DevExpress.XtraPivotGrid.PivotGridGroup()
        Dim PivotGridGroup2 As DevExpress.XtraPivotGrid.PivotGridGroup = New DevExpress.XtraPivotGrid.PivotGridGroup()
        Dim PivotGridGroup3 As DevExpress.XtraPivotGrid.PivotGridGroup = New DevExpress.XtraPivotGrid.PivotGridGroup()
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.PivotGridField3 = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.PivotGridField4 = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.PivotGridField5 = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.PivotGridField8 = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.PivotGridField6 = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.INDcolPayValuePercent = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.RepositoryItemProgressBarPercent = New DevExpress.XtraEditors.Repository.RepositoryItemProgressBar()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDpgcSchedulePayment = New DevExpress.XtraPivotGrid.PivotGridControl()
        Me.PivotGridField1 = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.PivotGridField2 = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.PivotGridField7 = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.INDColDiscountApp = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.PivotGridField9 = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.INDgvEntityBankAccount = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.INDlcgRoot = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDliSchedulePayment = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlcgInfo = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDliCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDbteCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDlcRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDsleCostCenter = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDtxtNoteNumber = New DevExpress.XtraEditors.TextEdit()
        Me.INDspnCheck = New DevExpress.XtraEditors.SpinEdit()
        Me.INDrgTaxByMil = New DevExpress.XtraEditors.RadioGroup()
        Me.INDglePaymentType = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.INDsleEntityBankAccount = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDdeDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDliDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliEntityBankAccount = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliCostCenter = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlcgOthers = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDliPaymentType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliCheck = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliDebitNote = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliTaxByMil = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoPivotGridControl1 = New Presentation.Controls.IndigoPivotGridControl(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoRadioGroup1 = New Presentation.Controls.IndigoRadioGroup(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemProgressBarPercent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpgcSchedulePayment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvEntityBankAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliSchedulePayment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbteCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlcRoot.SuspendLayout()
        CType(Me.INDsleCostCenter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtNoteNumber.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDspnCheck.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrgTaxByMil.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDglePaymentType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleEntityBankAccount.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdeDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdeDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliEntityBankAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliCostCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgOthers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliPaymentType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliCheck, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliDebitNote, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliTaxByMil, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoPivotGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoRadioGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlcRoot)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1262, 482)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1262, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(1262, 130)
        '
        'PivotGridField3
        '
        Me.PivotGridField3.AreaIndex = 2
        Me.PivotGridField3.Caption = "Año"
        Me.PivotGridField3.FieldName = "ExpirationDate"
        Me.PivotGridField3.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.DateYear
        Me.PivotGridField3.Name = "PivotGridField3"
        Me.PivotGridField3.UnboundFieldName = "Año"
        '
        'PivotGridField4
        '
        Me.PivotGridField4.AreaIndex = 3
        Me.PivotGridField4.Caption = "Mes"
        Me.PivotGridField4.FieldName = "ExpirationDate"
        Me.PivotGridField4.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.DateMonth
        Me.PivotGridField4.Name = "PivotGridField4"
        Me.PivotGridField4.UnboundFieldName = "Mes"
        '
        'PivotGridField5
        '
        Me.PivotGridField5.AreaIndex = 0
        Me.PivotGridField5.Caption = "Factura"
        Me.PivotGridField5.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.PivotGridField5.FieldName = "Invoice"
        Me.PivotGridField5.Name = "PivotGridField5"
        '
        'PivotGridField8
        '
        Me.PivotGridField8.AreaIndex = 1
        Me.PivotGridField8.Caption = "Cuota"
        Me.PivotGridField8.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.PivotGridField8.FieldName = "Share"
        Me.PivotGridField8.Name = "PivotGridField8"
        '
        'PivotGridField6
        '
        Me.PivotGridField6.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.PivotGridField6.AreaIndex = 0
        Me.PivotGridField6.Caption = "Saldo Cuota Factura"
        Me.PivotGridField6.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.PivotGridField6.FieldName = "BalanceShare"
        Me.PivotGridField6.Name = "PivotGridField6"
        Me.PivotGridField6.Width = 140
        '
        'INDcolPayValuePercent
        '
        Me.INDcolPayValuePercent.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.INDcolPayValuePercent.AreaIndex = 1
        Me.INDcolPayValuePercent.Caption = "% Pago"
        Me.INDcolPayValuePercent.CellFormat.FormatString = "{0}%"
        Me.INDcolPayValuePercent.CellFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.INDcolPayValuePercent.FieldEdit = Me.RepositoryItemProgressBarPercent
        Me.INDcolPayValuePercent.FieldName = "PayValue"
        Me.INDcolPayValuePercent.Name = "INDcolPayValuePercent"
        '
        'RepositoryItemProgressBarPercent
        '
        Me.RepositoryItemProgressBarPercent.DisplayFormat.FormatString = "{0}%"
        Me.RepositoryItemProgressBarPercent.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.RepositoryItemProgressBarPercent.Name = "RepositoryItemProgressBarPercent"
        Me.RepositoryItemProgressBarPercent.ShowTitle = True
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'INDpgcSchedulePayment
        '
        Me.INDpgcSchedulePayment.Appearance.ColumnHeaderArea.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDpgcSchedulePayment.Appearance.ColumnHeaderArea.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDpgcSchedulePayment.Appearance.ColumnHeaderArea.Options.UseFont = True
        Me.INDpgcSchedulePayment.Appearance.ColumnHeaderArea.Options.UseForeColor = True
        Me.INDpgcSchedulePayment.Appearance.FieldHeader.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDpgcSchedulePayment.Appearance.FieldHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.INDpgcSchedulePayment.Appearance.FieldHeader.Options.UseFont = True
        Me.INDpgcSchedulePayment.Appearance.FieldHeader.Options.UseForeColor = True
        Me.INDpgcSchedulePayment.Appearance.FieldValue.Font = New System.Drawing.Font("Segoe UI Semibold", 10.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDpgcSchedulePayment.Appearance.FieldValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDpgcSchedulePayment.Appearance.FieldValue.Options.UseFont = True
        Me.INDpgcSchedulePayment.Appearance.FieldValue.Options.UseForeColor = True
        Me.INDpgcSchedulePayment.Appearance.FieldValueTotal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.INDpgcSchedulePayment.Appearance.FieldValueTotal.Options.UseForeColor = True
        Me.INDpgcSchedulePayment.Appearance.FilterHeaderArea.BackColor = System.Drawing.Color.White
        Me.INDpgcSchedulePayment.Appearance.FilterHeaderArea.BackColor2 = System.Drawing.Color.White
        Me.INDpgcSchedulePayment.Appearance.FilterHeaderArea.Options.UseBackColor = True
        Me.INDpgcSchedulePayment.Appearance.HeaderArea.BackColor = System.Drawing.Color.White
        Me.INDpgcSchedulePayment.Appearance.HeaderArea.BackColor2 = System.Drawing.Color.White
        Me.INDpgcSchedulePayment.Appearance.HeaderArea.Options.UseBackColor = True
        Me.INDpgcSchedulePayment.Fields.AddRange(New DevExpress.XtraPivotGrid.PivotGridField() {Me.PivotGridField1, Me.PivotGridField2, Me.PivotGridField3, Me.PivotGridField4, Me.PivotGridField5, Me.PivotGridField6, Me.PivotGridField7, Me.PivotGridField8, Me.INDcolPayValuePercent, Me.INDColDiscountApp, Me.PivotGridField9})
        PivotGridGroup1.Fields.Add(Me.PivotGridField3)
        PivotGridGroup1.Fields.Add(Me.PivotGridField4)
        PivotGridGroup2.Fields.Add(Me.PivotGridField5)
        PivotGridGroup2.Fields.Add(Me.PivotGridField8)
        PivotGridGroup3.Fields.Add(Me.PivotGridField6)
        PivotGridGroup3.Fields.Add(Me.INDcolPayValuePercent)
        Me.INDpgcSchedulePayment.Groups.AddRange(New DevExpress.XtraPivotGrid.PivotGridGroup() {PivotGridGroup1, PivotGridGroup2, PivotGridGroup3})
        Me.IndigoPivotGridControl1.SetGuardarXml(Me.INDpgcSchedulePayment, True)
        Me.INDpgcSchedulePayment.Location = New System.Drawing.Point(12, 209)
        Me.INDpgcSchedulePayment.Name = "INDpgcSchedulePayment"
        Me.INDpgcSchedulePayment.OptionsBehavior.UseAsyncMode = True
        Me.INDpgcSchedulePayment.OptionsView.ShowRowTotals = False
        Me.INDpgcSchedulePayment.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemProgressBarPercent})
        Me.INDpgcSchedulePayment.Size = New System.Drawing.Size(1234, 252)
        Me.INDpgcSchedulePayment.TabIndex = 7
        '
        'PivotGridField1
        '
        Me.PivotGridField1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.PivotGridField1.AreaIndex = 0
        Me.PivotGridField1.Caption = "Proveedor"
        Me.PivotGridField1.FieldName = "SupplierName"
        Me.PivotGridField1.Name = "PivotGridField1"
        Me.PivotGridField1.Width = 300
        '
        'PivotGridField2
        '
        Me.PivotGridField2.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.PivotGridField2.AreaIndex = 1
        Me.PivotGridField2.Caption = "L. Distribución"
        Me.PivotGridField2.FieldName = "DescriptionLine"
        Me.PivotGridField2.Name = "PivotGridField2"
        Me.PivotGridField2.Width = 250
        '
        'PivotGridField7
        '
        Me.PivotGridField7.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.PivotGridField7.AreaIndex = 0
        Me.PivotGridField7.Caption = "Edad"
        Me.PivotGridField7.FieldName = "AgePayment"
        Me.PivotGridField7.Name = "PivotGridField7"
        Me.PivotGridField7.Width = 200
        '
        'INDColDiscountApp
        '
        Me.INDColDiscountApp.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.INDColDiscountApp.AreaIndex = 2
        Me.INDColDiscountApp.Caption = "Descuento Apl."
        Me.INDColDiscountApp.CellFormat.FormatString = "n"
        Me.INDColDiscountApp.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColDiscountApp.FieldName = "ApplyDiscountValue"
        Me.INDColDiscountApp.Name = "INDColDiscountApp"
        Me.INDColDiscountApp.Options.AllowEdit = False
        '
        'PivotGridField9
        '
        Me.PivotGridField9.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.PivotGridField9.AreaIndex = 2
        Me.PivotGridField9.Caption = "Moneda"
        Me.PivotGridField9.FieldName = "CurrencyAbbreviation"
        Me.PivotGridField9.Name = "PivotGridField9"
        Me.PivotGridField9.Width = 200
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'INDgvEntityBankAccount
        '
        Me.INDgvEntityBankAccount.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvEntityBankAccount.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvEntityBankAccount.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvEntityBankAccount.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvEntityBankAccount.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvEntityBankAccount.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvEntityBankAccount.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvEntityBankAccount.Appearance.Row.Options.UseFont = True
        Me.INDgvEntityBankAccount.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6})
        Me.INDgvEntityBankAccount.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDgvEntityBankAccount.Name = "INDgvEntityBankAccount"
        Me.INDgvEntityBankAccount.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDgvEntityBankAccount.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvEntityBankAccount.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvEntityBankAccount.OptionsView.ShowAutoFilterRow = True
        Me.INDgvEntityBankAccount.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvEntityBankAccount, False)
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Código"
        Me.GridColumn3.FieldName = "Code"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 0
        Me.GridColumn3.Width = 232
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Nombre"
        Me.GridColumn4.FieldName = "IdBank.Name"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 1
        Me.GridColumn4.Width = 346
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "# Cuenta"
        Me.GridColumn5.FieldName = "Number"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 2
        Me.GridColumn5.Width = 241
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Cta Contable"
        Me.GridColumn6.FieldName = "IdMainAccount.NumberName"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 3
        Me.GridColumn6.Width = 264
        '
        'GridLookUpEdit1View
        '
        Me.GridLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.GridLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn2})
        Me.GridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit1View.Name = "GridLookUpEdit1View"
        Me.GridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridLookUpEdit1View, False)
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Tipo Pago"
        Me.GridColumn2.FieldName = "Item2"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.OptionsColumn.AllowMove = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        '
        'GridView1
        '
        Me.GridView1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView1.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.GroupRow.Options.UseFont = True
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn7, Me.GridColumn8})
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView1, False)
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Código"
        Me.GridColumn7.FieldName = "Codigo"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 0
        Me.GridColumn7.Width = 295
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Nombre"
        Me.GridColumn8.FieldName = "Descripcion"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 1
        Me.GridColumn8.Width = 788
        '
        'INDlcgRoot
        '
        Me.INDlcgRoot.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgRoot.AppearanceGroup.Options.UseFont = True
        Me.INDlcgRoot.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgRoot.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgRoot.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgRoot.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgRoot.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgRoot.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgRoot.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgRoot.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgRoot.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgRoot.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgRoot.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgRoot.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgRoot, False)
        Me.INDlcgRoot.CustomizationFormText = "Root"
        Me.INDlcgRoot.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlcgRoot.GroupBordersVisible = False
        Me.INDlcgRoot.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDliSchedulePayment, Me.INDlcgInfo, Me.INDlcgOthers})
        Me.INDlcgRoot.Name = "INDlcgRoot"
        Me.INDlcgRoot.Size = New System.Drawing.Size(1258, 473)
        Me.INDlcgRoot.TextVisible = False
        '
        'INDliSchedulePayment
        '
        Me.INDliSchedulePayment.Control = Me.INDpgcSchedulePayment
        Me.INDliSchedulePayment.CustomizationFormText = "Programación"
        Me.INDliSchedulePayment.Location = New System.Drawing.Point(0, 197)
        Me.INDliSchedulePayment.Name = "INDliSchedulePayment"
        Me.INDliSchedulePayment.Size = New System.Drawing.Size(1238, 256)
        Me.INDliSchedulePayment.TextSize = New System.Drawing.Size(0, 0)
        Me.INDliSchedulePayment.TextVisible = False
        '
        'INDlcgInfo
        '
        Me.INDlcgInfo.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgInfo.AppearanceGroup.Options.UseFont = True
        Me.INDlcgInfo.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgInfo.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgInfo.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgInfo.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgInfo.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgInfo.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgInfo.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgInfo.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgInfo.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgInfo.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgInfo.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgInfo.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgInfo, False)
        Me.INDlcgInfo.CustomizationFormText = "Datos"
        Me.INDlcgInfo.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDliCode, Me.INDliDate, Me.INDliEntityBankAccount, Me.INDliCostCenter})
        Me.INDlcgInfo.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgInfo.Name = "INDlcgInfo"
        Me.INDlcgInfo.Size = New System.Drawing.Size(414, 197)
        Me.INDlcgInfo.Text = "Datos Programación de Pagos"
        '
        'INDliCode
        '
        Me.INDliCode.AllowHide = False
        Me.INDliCode.Control = Me.INDbteCode
        Me.INDliCode.CustomizationFormText = "LayoutControlItem1"
        Me.INDliCode.Location = New System.Drawing.Point(0, 0)
        Me.INDliCode.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDliCode.MinSize = New System.Drawing.Size(390, 36)
        Me.INDliCode.Name = "INDliCode"
        Me.INDliCode.ShowInCustomizationForm = False
        Me.INDliCode.Size = New System.Drawing.Size(390, 36)
        Me.INDliCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliCode.Text = "Código"
        Me.INDliCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliCode.TextSize = New System.Drawing.Size(135, 21)
        Me.INDliCode.TextToControlDistance = 5
        '
        'INDbteCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbteCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbteCode, True)
        Me.INDbteCode.Location = New System.Drawing.Point(164, 53)
        Me.IndigoTextEdit1.SetMascara(Me.INDbteCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDbteCode.Name = "INDbteCode"
        Me.INDbteCode.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDbteCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbteCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDbteCode.Properties.Appearance.Options.UseFont = True
        Me.INDbteCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDbteCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDbteCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseFont = True
        EditorButtonImageOptions1.Image = Global.Presentation.Treasury.My.Resources.Resources.BuscarMetro
        Me.INDbteCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDbteCode.Properties.MaxLength = 20
        Me.INDbteCode.Size = New System.Drawing.Size(246, 28)
        Me.INDbteCode.StyleController = Me.INDlcRoot
        Me.INDbteCode.TabIndex = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbteCode, 0)
        Me.INDbteCode.ToolTip = "Este Campo es Necesario"
        '
        'INDlcRoot
        '
        Me.INDlcRoot.Controls.Add(Me.INDsleCostCenter)
        Me.INDlcRoot.Controls.Add(Me.INDtxtNoteNumber)
        Me.INDlcRoot.Controls.Add(Me.INDspnCheck)
        Me.INDlcRoot.Controls.Add(Me.INDrgTaxByMil)
        Me.INDlcRoot.Controls.Add(Me.INDglePaymentType)
        Me.INDlcRoot.Controls.Add(Me.INDsleEntityBankAccount)
        Me.INDlcRoot.Controls.Add(Me.INDdeDate)
        Me.INDlcRoot.Controls.Add(Me.INDbteCode)
        Me.INDlcRoot.Controls.Add(Me.INDpgcSchedulePayment)
        Me.INDlcRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlcRoot.Location = New System.Drawing.Point(2, 7)
        Me.INDlcRoot.Name = "INDlcRoot"
        Me.INDlcRoot.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(2102, 418, 666, 438)
        Me.INDlcRoot.Root = Me.INDlcgRoot
        Me.INDlcRoot.Size = New System.Drawing.Size(1258, 473)
        Me.INDlcRoot.TabIndex = 5
        Me.INDlcRoot.Text = "LayoutControl1"
        '
        'INDsleCostCenter
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleCostCenter, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleCostCenter, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleCostCenter, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleCostCenter, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleCostCenter, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleCostCenter, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleCostCenter, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleCostCenter, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleCostCenter, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleCostCenter, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleCostCenter, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleCostCenter, False)
        Me.INDsleCostCenter.Location = New System.Drawing.Point(164, 161)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleCostCenter, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleCostCenter.Name = "INDsleCostCenter"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleCostCenter, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleCostCenter, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleCostCenter, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDsleCostCenter, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleCostCenter, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleCostCenter, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleCostCenter, False)
        Me.INDsleCostCenter.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleCostCenter.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleCostCenter.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleCostCenter.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleCostCenter.Properties.Appearance.Options.UseFont = True
        Me.INDsleCostCenter.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleCostCenter.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleCostCenter.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleCostCenter.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleCostCenter.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleCostCenter.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleCostCenter.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleCostCenter.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleCostCenter.Properties.DisplayMember = "CodeName"
        Me.INDsleCostCenter.Properties.NullText = ""
        Me.INDsleCostCenter.Properties.PopupSizeable = False
        Me.INDsleCostCenter.Properties.PopupView = Me.GridView1
        Me.INDsleCostCenter.Properties.ShowFooter = False
        Me.INDsleCostCenter.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleCostCenter, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleCostCenter, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleCostCenter, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleCostCenter, True)
        Me.INDsleCostCenter.Size = New System.Drawing.Size(246, 28)
        Me.INDsleCostCenter.StyleController = Me.INDlcRoot
        Me.INDsleCostCenter.TabIndex = 8
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleCostCenter, "517")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleCostCenter, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleCostCenter, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleCostCenter, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleCostCenter, False)
        '
        'INDtxtNoteNumber
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtNoteNumber, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtNoteNumber, False)
        Me.INDtxtNoteNumber.EnterMoveNextControl = True
        Me.INDtxtNoteNumber.Location = New System.Drawing.Point(656, 125)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtNoteNumber, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDtxtNoteNumber.Name = "INDtxtNoteNumber"
        Me.INDtxtNoteNumber.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtNoteNumber.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtNoteNumber.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtNoteNumber.Properties.Appearance.Options.UseFont = True
        Me.INDtxtNoteNumber.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtNoteNumber.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtNoteNumber.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtNoteNumber.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtNoteNumber.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtNoteNumber.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtNoteNumber.Properties.Mask.EditMask = "[-a-zA-Z0-9|°¬!ñÑ""#$%&/()=?¡'¿\@¨´_.:,; ]+"
        Me.INDtxtNoteNumber.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDtxtNoteNumber.Properties.MaxLength = 50
        Me.INDtxtNoteNumber.Size = New System.Drawing.Size(238, 28)
        Me.INDtxtNoteNumber.StyleController = Me.INDlcRoot
        Me.INDtxtNoteNumber.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtNoteNumber, 0)
        '
        'INDspnCheck
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDspnCheck, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDspnCheck, False)
        Me.INDspnCheck.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDspnCheck.Enabled = False
        Me.INDspnCheck.EnterMoveNextControl = True
        Me.INDspnCheck.Location = New System.Drawing.Point(656, 89)
        Me.IndigoTextEdit1.SetMascara(Me.INDspnCheck, Presentation.Controls.IndigoTextEdit.EMask.Numerico)
        Me.INDspnCheck.Name = "INDspnCheck"
        Me.INDspnCheck.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDspnCheck.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDspnCheck.Properties.Appearance.Options.UseBackColor = True
        Me.INDspnCheck.Properties.Appearance.Options.UseFont = True
        Me.INDspnCheck.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDspnCheck.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDspnCheck.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDspnCheck.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDspnCheck.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDspnCheck.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDspnCheck.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDspnCheck.Properties.Mask.EditMask = "[0-9]+"
        Me.INDspnCheck.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDspnCheck.Size = New System.Drawing.Size(238, 28)
        Me.INDspnCheck.StyleController = Me.INDlcRoot
        Me.INDspnCheck.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDspnCheck, 0)
        '
        'INDrgTaxByMil
        '
        Me.IndigoRadioGroup1.SetCampoObligatorio(Me.INDrgTaxByMil, True)
        Me.INDrgTaxByMil.EditValue = False
        Me.INDrgTaxByMil.Location = New System.Drawing.Point(656, 161)
        Me.INDrgTaxByMil.Name = "INDrgTaxByMil"
        Me.INDrgTaxByMil.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDrgTaxByMil.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDrgTaxByMil.Properties.Appearance.Options.UseBackColor = True
        Me.INDrgTaxByMil.Properties.Appearance.Options.UseFont = True
        Me.INDrgTaxByMil.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDrgTaxByMil.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDrgTaxByMil.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDrgTaxByMil.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDrgTaxByMil.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDrgTaxByMil.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDrgTaxByMil.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(True, "Si"), New DevExpress.XtraEditors.Controls.RadioGroupItem(False, "No")})
        Me.INDrgTaxByMil.Size = New System.Drawing.Size(238, 32)
        Me.INDrgTaxByMil.StyleController = Me.INDlcRoot
        Me.INDrgTaxByMil.TabIndex = 6
        '
        'INDglePaymentType
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDglePaymentType, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDglePaymentType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDglePaymentType, True)
        Me.INDglePaymentType.EnterMoveNextControl = True
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDglePaymentType, False)
        Me.INDglePaymentType.Location = New System.Drawing.Point(656, 53)
        Me.IndigoTextEdit1.SetMascara(Me.INDglePaymentType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDglePaymentType.Name = "INDglePaymentType"
        Me.INDglePaymentType.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDglePaymentType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDglePaymentType.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDglePaymentType.Properties.Appearance.Options.UseBackColor = True
        Me.INDglePaymentType.Properties.Appearance.Options.UseFont = True
        Me.INDglePaymentType.Properties.Appearance.Options.UseForeColor = True
        Me.INDglePaymentType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDglePaymentType.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDglePaymentType.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDglePaymentType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDglePaymentType.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDglePaymentType.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDglePaymentType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDglePaymentType.Properties.DisplayMember = "Item2"
        Me.INDglePaymentType.Properties.ImmediatePopup = True
        Me.INDglePaymentType.Properties.NullText = ""
        Me.INDglePaymentType.Properties.PopupView = Me.GridLookUpEdit1View
        Me.INDglePaymentType.Properties.ValueMember = "Item1"
        Me.INDglePaymentType.Size = New System.Drawing.Size(238, 28)
        Me.INDglePaymentType.StyleController = Me.INDlcRoot
        Me.INDglePaymentType.TabIndex = 3
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDglePaymentType, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDglePaymentType, 0)
        Me.INDglePaymentType.ToolTip = "Este Campo es Necesario"
        '
        'INDsleEntityBankAccount
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleEntityBankAccount, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleEntityBankAccount, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleEntityBankAccount, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleEntityBankAccount, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleEntityBankAccount, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleEntityBankAccount, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleEntityBankAccount, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleEntityBankAccount, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleEntityBankAccount, False)
        Me.INDsleEntityBankAccount.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleEntityBankAccount, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleEntityBankAccount, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleEntityBankAccount, False)
        Me.INDsleEntityBankAccount.Location = New System.Drawing.Point(164, 125)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleEntityBankAccount, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleEntityBankAccount.Name = "INDsleEntityBankAccount"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleEntityBankAccount, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleEntityBankAccount, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleEntityBankAccount, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDsleEntityBankAccount, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleEntityBankAccount, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleEntityBankAccount, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleEntityBankAccount, False)
        Me.INDsleEntityBankAccount.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDsleEntityBankAccount.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleEntityBankAccount.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleEntityBankAccount.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleEntityBankAccount.Properties.Appearance.Options.UseFont = True
        Me.INDsleEntityBankAccount.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleEntityBankAccount.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleEntityBankAccount.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleEntityBankAccount.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleEntityBankAccount.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleEntityBankAccount.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleEntityBankAccount.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleEntityBankAccount.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleEntityBankAccount.Properties.DisplayMember = "CodeBankAccount"
        Me.INDsleEntityBankAccount.Properties.NullText = ""
        Me.INDsleEntityBankAccount.Properties.PopupSizeable = False
        Me.INDsleEntityBankAccount.Properties.PopupView = Me.INDgvEntityBankAccount
        Me.INDsleEntityBankAccount.Properties.ShowFooter = False
        Me.INDsleEntityBankAccount.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleEntityBankAccount, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleEntityBankAccount, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleEntityBankAccount, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleEntityBankAccount, True)
        Me.INDsleEntityBankAccount.Size = New System.Drawing.Size(246, 28)
        Me.INDsleEntityBankAccount.StyleController = Me.INDlcRoot
        Me.INDsleEntityBankAccount.TabIndex = 2
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleEntityBankAccount, "628")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleEntityBankAccount, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleEntityBankAccount, "{0} - {1}")
        Me.INDsleEntityBankAccount.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleEntityBankAccount, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleEntityBankAccount, False)
        '
        'INDdeDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdeDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdeDate, True)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDdeDate, False)
        Me.INDdeDate.EditValue = Nothing
        Me.INDdeDate.EnterMoveNextControl = True
        Me.INDdeDate.Location = New System.Drawing.Point(164, 89)
        Me.IndigoTextEdit1.SetMascara(Me.INDdeDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDdeDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDdeDate.Name = "INDdeDate"
        Me.INDdeDate.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDdeDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdeDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDdeDate.Properties.Appearance.Options.UseFont = True
        Me.INDdeDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDdeDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDdeDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdeDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDdeDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDdeDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdeDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeDate.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.INDdeDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDdeDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdeDate.Properties.ReadOnly = True
        Me.INDdeDate.Size = New System.Drawing.Size(246, 28)
        Me.INDdeDate.StyleController = Me.INDlcRoot
        Me.INDdeDate.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdeDate, 0)
        '
        'INDliDate
        '
        Me.INDliDate.AllowHide = False
        Me.INDliDate.Control = Me.INDdeDate
        Me.INDliDate.CustomizationFormText = "LayoutControlItem2"
        Me.INDliDate.Location = New System.Drawing.Point(0, 36)
        Me.INDliDate.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDliDate.MinSize = New System.Drawing.Size(390, 36)
        Me.INDliDate.Name = "INDliDate"
        Me.INDliDate.ShowInCustomizationForm = False
        Me.INDliDate.Size = New System.Drawing.Size(390, 36)
        Me.INDliDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliDate.Text = "Fecha"
        Me.INDliDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliDate.TextSize = New System.Drawing.Size(135, 21)
        Me.INDliDate.TextToControlDistance = 5
        '
        'INDliEntityBankAccount
        '
        Me.INDliEntityBankAccount.Control = Me.INDsleEntityBankAccount
        Me.INDliEntityBankAccount.CustomizationFormText = "LayoutControlItem1"
        Me.INDliEntityBankAccount.Location = New System.Drawing.Point(0, 72)
        Me.INDliEntityBankAccount.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDliEntityBankAccount.MinSize = New System.Drawing.Size(390, 36)
        Me.INDliEntityBankAccount.Name = "INDliEntityBankAccount"
        Me.INDliEntityBankAccount.ShowInCustomizationForm = False
        Me.INDliEntityBankAccount.Size = New System.Drawing.Size(390, 36)
        Me.INDliEntityBankAccount.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliEntityBankAccount.Text = "Cuenta Bancaria"
        Me.INDliEntityBankAccount.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliEntityBankAccount.TextSize = New System.Drawing.Size(135, 21)
        Me.INDliEntityBankAccount.TextToControlDistance = 5
        '
        'INDliCostCenter
        '
        Me.INDliCostCenter.AllowHide = False
        Me.INDliCostCenter.Control = Me.INDsleCostCenter
        Me.INDliCostCenter.CustomizationFormText = "Centro Costo"
        Me.INDliCostCenter.Location = New System.Drawing.Point(0, 108)
        Me.INDliCostCenter.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDliCostCenter.MinSize = New System.Drawing.Size(390, 36)
        Me.INDliCostCenter.Name = "INDliCostCenter"
        Me.INDliCostCenter.Size = New System.Drawing.Size(390, 36)
        Me.INDliCostCenter.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliCostCenter.Text = "Centro Costo"
        Me.INDliCostCenter.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliCostCenter.TextSize = New System.Drawing.Size(135, 21)
        Me.INDliCostCenter.TextToControlDistance = 5
        '
        'INDlcgOthers
        '
        Me.INDlcgOthers.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgOthers.AppearanceGroup.Options.UseFont = True
        Me.INDlcgOthers.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgOthers.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgOthers.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgOthers.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgOthers.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgOthers.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgOthers.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgOthers.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgOthers.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgOthers.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgOthers.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgOthers.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgOthers, False)
        Me.INDlcgOthers.CustomizationFormText = "Datos de Programación"
        Me.INDlcgOthers.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDliPaymentType, Me.INDliCheck, Me.INDliDebitNote, Me.INDliTaxByMil})
        Me.INDlcgOthers.Location = New System.Drawing.Point(414, 0)
        Me.INDlcgOthers.Name = "INDlcgOthers"
        Me.INDlcgOthers.Size = New System.Drawing.Size(824, 197)
        Me.INDlcgOthers.Text = " "
        '
        'INDliPaymentType
        '
        Me.INDliPaymentType.AllowHide = False
        Me.INDliPaymentType.Control = Me.INDglePaymentType
        Me.INDliPaymentType.CustomizationFormText = "LayoutControlItem2"
        Me.INDliPaymentType.Location = New System.Drawing.Point(0, 0)
        Me.INDliPaymentType.MaxSize = New System.Drawing.Size(460, 36)
        Me.INDliPaymentType.MinSize = New System.Drawing.Size(460, 36)
        Me.INDliPaymentType.Name = "INDliPaymentType"
        Me.INDliPaymentType.Padding = New DevExpress.XtraLayout.Utils.Padding(80, 2, 2, 2)
        Me.INDliPaymentType.ShowInCustomizationForm = False
        Me.INDliPaymentType.Size = New System.Drawing.Size(800, 36)
        Me.INDliPaymentType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliPaymentType.Text = "Tipo Pago"
        Me.INDliPaymentType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliPaymentType.TextSize = New System.Drawing.Size(135, 21)
        Me.INDliPaymentType.TextToControlDistance = 5
        '
        'INDliCheck
        '
        Me.INDliCheck.AllowHide = False
        Me.INDliCheck.Control = Me.INDspnCheck
        Me.INDliCheck.CustomizationFormText = "LayoutControlItem1"
        Me.INDliCheck.Location = New System.Drawing.Point(0, 36)
        Me.INDliCheck.MaxSize = New System.Drawing.Size(460, 36)
        Me.INDliCheck.MinSize = New System.Drawing.Size(460, 36)
        Me.INDliCheck.Name = "INDliCheck"
        Me.INDliCheck.Padding = New DevExpress.XtraLayout.Utils.Padding(80, 2, 2, 2)
        Me.INDliCheck.Size = New System.Drawing.Size(800, 36)
        Me.INDliCheck.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliCheck.Text = "Cheque Inicial"
        Me.INDliCheck.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliCheck.TextSize = New System.Drawing.Size(135, 21)
        Me.INDliCheck.TextToControlDistance = 5
        '
        'INDliDebitNote
        '
        Me.INDliDebitNote.AllowHide = False
        Me.INDliDebitNote.Control = Me.INDtxtNoteNumber
        Me.INDliDebitNote.CustomizationFormText = "LayoutControlItem2"
        Me.INDliDebitNote.Location = New System.Drawing.Point(0, 72)
        Me.INDliDebitNote.MaxSize = New System.Drawing.Size(460, 36)
        Me.INDliDebitNote.MinSize = New System.Drawing.Size(460, 36)
        Me.INDliDebitNote.Name = "INDliDebitNote"
        Me.INDliDebitNote.Padding = New DevExpress.XtraLayout.Utils.Padding(80, 2, 2, 2)
        Me.INDliDebitNote.Size = New System.Drawing.Size(800, 36)
        Me.INDliDebitNote.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliDebitNote.Text = "# Nota Débito"
        Me.INDliDebitNote.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliDebitNote.TextSize = New System.Drawing.Size(135, 21)
        Me.INDliDebitNote.TextToControlDistance = 5
        '
        'INDliTaxByMil
        '
        Me.INDliTaxByMil.Control = Me.INDrgTaxByMil
        Me.INDliTaxByMil.CustomizationFormText = "LayoutControlItem3"
        Me.INDliTaxByMil.Location = New System.Drawing.Point(0, 108)
        Me.INDliTaxByMil.MaxSize = New System.Drawing.Size(460, 36)
        Me.INDliTaxByMil.MinSize = New System.Drawing.Size(460, 36)
        Me.INDliTaxByMil.Name = "INDliTaxByMil"
        Me.INDliTaxByMil.Padding = New DevExpress.XtraLayout.Utils.Padding(80, 2, 2, 2)
        Me.INDliTaxByMil.ShowInCustomizationForm = False
        Me.INDliTaxByMil.Size = New System.Drawing.Size(800, 36)
        Me.INDliTaxByMil.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliTaxByMil.Text = "Tasa x Mil"
        Me.INDliTaxByMil.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliTaxByMil.TextSize = New System.Drawing.Size(135, 21)
        Me.INDliTaxByMil.TextToControlDistance = 5
        Me.INDliTaxByMil.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Tipo Pago"
        Me.GridColumn1.FieldName = "Item2"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'FrmDispersionFunds
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1262, 617)
        Me.IconOptions.ShowIcon = False
        Me.Name = "FrmDispersionFunds"
        Me.Opacity = 1.0R
        Me.Tag = "642"
        Me.Text = "Dispersión de Fondos"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemProgressBarPercent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpgcSchedulePayment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvEntityBankAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgRoot, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliSchedulePayment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbteCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlcRoot.ResumeLayout(False)
        CType(Me.INDsleCostCenter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtNoteNumber.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDspnCheck.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrgTaxByMil.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDglePaymentType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleEntityBankAccount.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdeDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdeDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliEntityBankAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliCostCenter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgOthers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliPaymentType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliCheck, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliDebitNote, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliTaxByMil, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoPivotGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoRadioGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDpgcSchedulePayment As DevExpress.XtraPivotGrid.PivotGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents PivotGridField1 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField2 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField3 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField4 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField5 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField6 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents PivotGridField7 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents IndigoPivotGridControl1 As Presentation.Controls.IndigoPivotGridControl
    Friend WithEvents INDlcRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlcgRoot As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDliSchedulePayment As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents PivotGridField8 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents INDcolPayValuePercent As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents RepositoryItemProgressBarPercent As DevExpress.XtraEditors.Repository.RepositoryItemProgressBar
    Friend WithEvents INDdeDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents INDbteCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDliCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDliDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlcgInfo As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDsleEntityBankAccount As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDgvEntityBankAccount As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDliEntityBankAccount As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDglePaymentType As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDrgTaxByMil As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents IndigoRadioGroup1 As Presentation.Controls.IndigoRadioGroup
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDspnCheck As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDtxtNoteNumber As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDliPaymentType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDliCheck As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDliDebitNote As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDliTaxByMil As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlcgOthers As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDsleCostCenter As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDliCostCenter As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColDiscountApp As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents PivotGridField9 As DevExpress.XtraPivotGrid.PivotGridField
End Class
