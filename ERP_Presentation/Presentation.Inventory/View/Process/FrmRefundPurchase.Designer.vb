<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmRefundPurchase
    Inherits Presentation.Controls.FormBase

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
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.LyRefundPurchase = New DevExpress.XtraLayout.LayoutControl()
        Me.INDgcObligation = New DevExpress.XtraGrid.GridControl()
        Me.INDviewObligation = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepTxtValue = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.INDTxtFregithIVAValue = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtTotalValue = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtOtherDeduction = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtOtherRetention = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtWhithholdigSourceValue = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtWhithholdigICAValue = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtWhithholdigTaxValue = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtValueTax = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtDiscountValue = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtSubTotal = New DevExpress.XtraEditors.TextEdit()
        Me.INDGcProducts = New DevExpress.XtraGrid.GridControl()
        Me.INDGvProducts = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colProductsCodeName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colProductsBatch = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRptSleDevolutionCause = New DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit()
        Me.INDRptGvDevolutionCause = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colProductsOutstandingQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colProductsDevolutionQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepoSpeCantDevolutionProduct = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.INDGcOtherDeduction = New DevExpress.XtraGrid.GridControl()
        Me.INDGvOtherDeduction = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColDeductionConcept = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColBaseValueDeduction = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColDeductionValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepoSpeDeductionValueDevolution = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.INDGcOtherWithholding = New DevExpress.XtraGrid.GridControl()
        Me.INDGvOtherWithholding = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColConceptName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColRetentionPercentage = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemSpinEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.ColRetentionBaseValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemTextEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.ColWhitholdingValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.INDSpnFreightIvaPercentage = New DevExpress.XtraEditors.SpinEdit()
        Me.INDTxtDescription = New DevExpress.XtraEditors.MemoEdit()
        Me.INDTxtFreightInvoice = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtFreightValue = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtDayPeriod = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtInvoiceNumber = New DevExpress.XtraEditors.TextEdit()
        Me.INDDteDateEntranceVoucher = New DevExpress.XtraEditors.DateEdit()
        Me.INDTxtWarehouse = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtSupplier = New DevExpress.XtraEditors.TextEdit()
        Me.INDSleVoucherCode = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDSlvEntranceVoucher = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colSleEntranceVoucherDateDocumento = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colSleEntranceVoucherCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colSleEntranceVoucherSupplier = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colEntranceVoucherWarehouse = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleWarehouse = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGdvWarehouse = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colSleWareHouseCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colSleWareHouseName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDDteDocumentDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDBtnCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDTxtInvoiceDate = New DevExpress.XtraEditors.DateEdit()
        Me.LyGpRefundPurchase = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LyGroupMainData = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyBtnCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyDteDocumentDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLySleWarehouse = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyTxtDescription = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LyGroupEntranceVoucherInformation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLySleVoucherCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyTxtSupplier = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyTxtWarehouse = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyTxtInvoiceNumber = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyTxtDayPeriod = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyDteDateEntranceVoucher = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyTxtInvoiceDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LyGroupFreight = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyTxtFreightInvoice = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLySpnFreightIvaPercentage = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyTxtFreightValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyTxtFreightIVAValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LyGroupProducts = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyGcProducts = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygBudget = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemGridObligation = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LyGroupOtherRetention = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyGcOtherWithholding = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LyGroupOtherDeduction = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyGcOtherDeduction = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LyGroupLiquitationInvoice = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyTxtTotalValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyTxtOtherDeduction = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyTxtOtherRetention = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyTxtWhithholdigSourceValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyTxtWhithholdigICAValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyTxtWhithholdigTaxValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyTxtValueTax = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyTxtDiscountValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyTxtSubTotal = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LyRefundPurchase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LyRefundPurchase.SuspendLayout()
        CType(Me.INDgcObligation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewObligation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepTxtValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtFregithIVAValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtTotalValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtOtherDeduction.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtOtherRetention.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtWhithholdigSourceValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtWhithholdigICAValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtWhithholdigTaxValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtValueTax.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtDiscountValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtSubTotal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcProducts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvProducts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRptSleDevolutionCause, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRptGvDevolutionCause, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepoSpeCantDevolutionProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcOtherDeduction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvOtherDeduction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepoSpeDeductionValueDevolution, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcOtherWithholding, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvOtherWithholding, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemSpinEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSpnFreightIvaPercentage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtDescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtFreightInvoice.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtFreightValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtDayPeriod.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtInvoiceNumber.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDteDateEntranceVoucher.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDteDateEntranceVoucher.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtWarehouse.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtSupplier.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleVoucherCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSlvEntranceVoucher, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleWarehouse.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGdvWarehouse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDteDocumentDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDteDocumentDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDBtnCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtInvoiceDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtInvoiceDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LyGpRefundPurchase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LyGroupMainData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyBtnCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyDteDocumentDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLySleWarehouse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyTxtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LyGroupEntranceVoucherInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLySleVoucherCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyTxtSupplier, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyTxtWarehouse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyTxtInvoiceNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyTxtDayPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyDteDateEntranceVoucher, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyTxtInvoiceDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LyGroupFreight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyTxtFreightInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLySpnFreightIvaPercentage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyTxtFreightValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyTxtFreightIVAValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LyGroupProducts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyGcProducts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygBudget, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemGridObligation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LyGroupOtherRetention, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyGcOtherWithholding, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LyGroupOtherDeduction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyGcOtherDeduction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LyGroupLiquitationInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyTxtTotalValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyTxtOtherDeduction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyTxtOtherRetention, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyTxtWhithholdigSourceValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyTxtWhithholdigICAValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyTxtWhithholdigTaxValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyTxtValueTax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyTxtDiscountValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyTxtSubTotal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.LyRefundPurchase)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 136)
        Me.INDPanelControlBase.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDPanelControlBase.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1661, 572)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Location = New System.Drawing.Point(0, 6)
        Me.ToolBars.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ToolBars.Size = New System.Drawing.Size(1661, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.BarraBotones.Size = New System.Drawing.Size(1661, 130)
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.LyRefundPurchase
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 8)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 562)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'LyRefundPurchase
        '
        Me.LyRefundPurchase.AllowCustomization = False
        Me.LyRefundPurchase.Controls.Add(Me.INDgcObligation)
        Me.LyRefundPurchase.Controls.Add(Me.INDTxtFregithIVAValue)
        Me.LyRefundPurchase.Controls.Add(Me.INDTxtTotalValue)
        Me.LyRefundPurchase.Controls.Add(Me.INDTxtOtherDeduction)
        Me.LyRefundPurchase.Controls.Add(Me.INDTxtOtherRetention)
        Me.LyRefundPurchase.Controls.Add(Me.INDTxtWhithholdigSourceValue)
        Me.LyRefundPurchase.Controls.Add(Me.INDTxtWhithholdigICAValue)
        Me.LyRefundPurchase.Controls.Add(Me.INDTxtWhithholdigTaxValue)
        Me.LyRefundPurchase.Controls.Add(Me.INDTxtValueTax)
        Me.LyRefundPurchase.Controls.Add(Me.INDTxtDiscountValue)
        Me.LyRefundPurchase.Controls.Add(Me.INDTxtSubTotal)
        Me.LyRefundPurchase.Controls.Add(Me.INDGcProducts)
        Me.LyRefundPurchase.Controls.Add(Me.INDGcOtherDeduction)
        Me.LyRefundPurchase.Controls.Add(Me.INDGcOtherWithholding)
        Me.LyRefundPurchase.Controls.Add(Me.INDSpnFreightIvaPercentage)
        Me.LyRefundPurchase.Controls.Add(Me.INDTxtDescription)
        Me.LyRefundPurchase.Controls.Add(Me.INDTxtFreightInvoice)
        Me.LyRefundPurchase.Controls.Add(Me.INDTxtFreightValue)
        Me.LyRefundPurchase.Controls.Add(Me.INDTxtDayPeriod)
        Me.LyRefundPurchase.Controls.Add(Me.INDTxtInvoiceNumber)
        Me.LyRefundPurchase.Controls.Add(Me.INDDteDateEntranceVoucher)
        Me.LyRefundPurchase.Controls.Add(Me.INDTxtWarehouse)
        Me.LyRefundPurchase.Controls.Add(Me.INDTxtSupplier)
        Me.LyRefundPurchase.Controls.Add(Me.INDSleVoucherCode)
        Me.LyRefundPurchase.Controls.Add(Me.INDSleWarehouse)
        Me.LyRefundPurchase.Controls.Add(Me.INDDteDocumentDate)
        Me.LyRefundPurchase.Controls.Add(Me.INDBtnCode)
        Me.LyRefundPurchase.Controls.Add(Me.INDTxtInvoiceDate)
        Me.LyRefundPurchase.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.LyRefundPurchase, False)
        Me.LyRefundPurchase.Location = New System.Drawing.Point(202, 8)
        Me.LyRefundPurchase.Name = "LyRefundPurchase"
        Me.LyRefundPurchase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(1964, 464, 574, 569)
        Me.LyRefundPurchase.Root = Me.LyGpRefundPurchase
        Me.LyRefundPurchase.Size = New System.Drawing.Size(1457, 562)
        Me.LyRefundPurchase.TabIndex = 1
        Me.LyRefundPurchase.Text = "LayoutControl1"
        '
        'INDgcObligation
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcObligation, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcObligation, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcObligation, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcObligation, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcObligation, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcObligation, False)
        Me.INDgcObligation.Location = New System.Drawing.Point(2108, 49)
        Me.INDgcObligation.MainView = Me.INDviewObligation
        Me.INDgcObligation.Name = "INDgcObligation"
        Me.INDgcObligation.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrepTxtValue})
        Me.INDgcObligation.Size = New System.Drawing.Size(824, 476)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcObligation, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcObligation.TabIndex = 30
        Me.INDgcObligation.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDviewObligation})
        '
        'INDviewObligation
        '
        Me.INDviewObligation.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewObligation.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewObligation.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewObligation.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewObligation.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewObligation.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewObligation.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewObligation.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewObligation.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewObligation.Appearance.Row.Options.UseFont = True
        Me.INDviewObligation.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDviewObligation.Appearance.ViewCaption.Options.UseFont = True
        Me.INDviewObligation.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6, Me.GridColumn7})
        Me.INDviewObligation.GridControl = Me.INDgcObligation
        Me.INDviewObligation.Name = "INDviewObligation"
        Me.INDviewObligation.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewObligation.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewObligation.OptionsView.ShowAutoFilterRow = True
        Me.INDviewObligation.OptionsView.ShowDetailButtons = False
        Me.INDviewObligation.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewObligation, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Código"
        Me.GridColumn1.FieldName = "ObligationCode"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Documento"
        Me.GridColumn2.FieldName = "ObligationDocument"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Rubro"
        Me.GridColumn3.FieldName = "CategoryName"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Recurso"
        Me.GridColumn4.FieldName = "FinancialSourceDescription"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 3
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Tipo"
        Me.GridColumn5.FieldName = "RevenueTypeDescription"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.AllowEdit = False
        Me.GridColumn5.OptionsColumn.AllowFocus = False
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 4
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Saldo"
        Me.GridColumn6.DisplayFormat.FormatString = "C0"
        Me.GridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn6.FieldName = "ObligationBalance"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.AllowEdit = False
        Me.GridColumn6.OptionsColumn.AllowFocus = False
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 5
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Valor"
        Me.GridColumn7.ColumnEdit = Me.INDrepTxtValue
        Me.GridColumn7.DisplayFormat.FormatString = "C0"
        Me.GridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn7.FieldName = "Value"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 6
        '
        'INDrepTxtValue
        '
        Me.INDrepTxtValue.AutoHeight = False
        Me.INDrepTxtValue.Mask.EditMask = "C0"
        Me.INDrepTxtValue.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDrepTxtValue.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepTxtValue.Name = "INDrepTxtValue"
        '
        'INDTxtFregithIVAValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtFregithIVAValue, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtFregithIVAValue, False)
        Me.INDTxtFregithIVAValue.EnterMoveNextControl = True
        Me.INDTxtFregithIVAValue.Location = New System.Drawing.Point(970, 157)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtFregithIVAValue, Presentation.Controls.IndigoTextEdit.EMask.Moneda)
        Me.INDTxtFregithIVAValue.Name = "INDTxtFregithIVAValue"
        Me.INDTxtFregithIVAValue.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtFregithIVAValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtFregithIVAValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtFregithIVAValue.Properties.Appearance.Options.UseFont = True
        Me.INDTxtFregithIVAValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTxtFregithIVAValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTxtFregithIVAValue.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtFregithIVAValue.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtFregithIVAValue.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtFregithIVAValue.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtFregithIVAValue.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtFregithIVAValue.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtFregithIVAValue.Properties.Mask.EditMask = "c0"
        Me.INDTxtFregithIVAValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDTxtFregithIVAValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtFregithIVAValue.Size = New System.Drawing.Size(262, 28)
        Me.INDTxtFregithIVAValue.StyleController = Me.LyRefundPurchase
        Me.INDTxtFregithIVAValue.TabIndex = 17
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtFregithIVAValue, 0)
        '
        'INDTxtTotalValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtTotalValue, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtTotalValue, False)
        Me.INDTxtTotalValue.Location = New System.Drawing.Point(4782, 337)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtTotalValue, Presentation.Controls.IndigoTextEdit.EMask.MonedaDecimales)
        Me.INDTxtTotalValue.Name = "INDTxtTotalValue"
        Me.INDTxtTotalValue.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtTotalValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtTotalValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtTotalValue.Properties.Appearance.Options.UseFont = True
        Me.INDTxtTotalValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTxtTotalValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTxtTotalValue.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtTotalValue.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtTotalValue.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtTotalValue.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtTotalValue.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtTotalValue.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtTotalValue.Properties.Mask.EditMask = "c2"
        Me.INDTxtTotalValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDTxtTotalValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtTotalValue.Size = New System.Drawing.Size(262, 28)
        Me.INDTxtTotalValue.StyleController = Me.LyRefundPurchase
        Me.INDTxtTotalValue.TabIndex = 29
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtTotalValue, 0)
        '
        'INDTxtOtherDeduction
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtOtherDeduction, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtOtherDeduction, False)
        Me.INDTxtOtherDeduction.Location = New System.Drawing.Point(4782, 301)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtOtherDeduction, Presentation.Controls.IndigoTextEdit.EMask.MonedaDecimales)
        Me.INDTxtOtherDeduction.Name = "INDTxtOtherDeduction"
        Me.INDTxtOtherDeduction.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtOtherDeduction.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtOtherDeduction.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtOtherDeduction.Properties.Appearance.Options.UseFont = True
        Me.INDTxtOtherDeduction.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTxtOtherDeduction.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTxtOtherDeduction.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtOtherDeduction.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtOtherDeduction.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtOtherDeduction.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtOtherDeduction.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtOtherDeduction.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtOtherDeduction.Properties.Mask.EditMask = "c2"
        Me.INDTxtOtherDeduction.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDTxtOtherDeduction.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtOtherDeduction.Size = New System.Drawing.Size(262, 28)
        Me.INDTxtOtherDeduction.StyleController = Me.LyRefundPurchase
        Me.INDTxtOtherDeduction.TabIndex = 28
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtOtherDeduction, 0)
        '
        'INDTxtOtherRetention
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtOtherRetention, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtOtherRetention, False)
        Me.INDTxtOtherRetention.Location = New System.Drawing.Point(4782, 265)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtOtherRetention, Presentation.Controls.IndigoTextEdit.EMask.MonedaDecimales)
        Me.INDTxtOtherRetention.Name = "INDTxtOtherRetention"
        Me.INDTxtOtherRetention.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtOtherRetention.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtOtherRetention.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtOtherRetention.Properties.Appearance.Options.UseFont = True
        Me.INDTxtOtherRetention.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTxtOtherRetention.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTxtOtherRetention.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtOtherRetention.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtOtherRetention.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtOtherRetention.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtOtherRetention.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtOtherRetention.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtOtherRetention.Properties.Mask.EditMask = "c2"
        Me.INDTxtOtherRetention.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDTxtOtherRetention.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtOtherRetention.Size = New System.Drawing.Size(262, 28)
        Me.INDTxtOtherRetention.StyleController = Me.LyRefundPurchase
        Me.INDTxtOtherRetention.TabIndex = 27
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtOtherRetention, 0)
        '
        'INDTxtWhithholdigSourceValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtWhithholdigSourceValue, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtWhithholdigSourceValue, False)
        Me.INDTxtWhithholdigSourceValue.Location = New System.Drawing.Point(4782, 229)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtWhithholdigSourceValue, Presentation.Controls.IndigoTextEdit.EMask.MonedaDecimales)
        Me.INDTxtWhithholdigSourceValue.Name = "INDTxtWhithholdigSourceValue"
        Me.INDTxtWhithholdigSourceValue.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtWhithholdigSourceValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtWhithholdigSourceValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtWhithholdigSourceValue.Properties.Appearance.Options.UseFont = True
        Me.INDTxtWhithholdigSourceValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTxtWhithholdigSourceValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTxtWhithholdigSourceValue.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtWhithholdigSourceValue.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtWhithholdigSourceValue.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtWhithholdigSourceValue.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtWhithholdigSourceValue.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtWhithholdigSourceValue.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtWhithholdigSourceValue.Properties.Mask.EditMask = "c2"
        Me.INDTxtWhithholdigSourceValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDTxtWhithholdigSourceValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtWhithholdigSourceValue.Size = New System.Drawing.Size(262, 28)
        Me.INDTxtWhithholdigSourceValue.StyleController = Me.LyRefundPurchase
        Me.INDTxtWhithholdigSourceValue.TabIndex = 26
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtWhithholdigSourceValue, 0)
        '
        'INDTxtWhithholdigICAValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtWhithholdigICAValue, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtWhithholdigICAValue, False)
        Me.INDTxtWhithholdigICAValue.Location = New System.Drawing.Point(4782, 193)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtWhithholdigICAValue, Presentation.Controls.IndigoTextEdit.EMask.MonedaDecimales)
        Me.INDTxtWhithholdigICAValue.Name = "INDTxtWhithholdigICAValue"
        Me.INDTxtWhithholdigICAValue.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtWhithholdigICAValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtWhithholdigICAValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtWhithholdigICAValue.Properties.Appearance.Options.UseFont = True
        Me.INDTxtWhithholdigICAValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTxtWhithholdigICAValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTxtWhithholdigICAValue.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtWhithholdigICAValue.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtWhithholdigICAValue.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtWhithholdigICAValue.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtWhithholdigICAValue.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtWhithholdigICAValue.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtWhithholdigICAValue.Properties.Mask.EditMask = "c2"
        Me.INDTxtWhithholdigICAValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDTxtWhithholdigICAValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtWhithholdigICAValue.Size = New System.Drawing.Size(262, 28)
        Me.INDTxtWhithholdigICAValue.StyleController = Me.LyRefundPurchase
        Me.INDTxtWhithholdigICAValue.TabIndex = 25
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtWhithholdigICAValue, 0)
        '
        'INDTxtWhithholdigTaxValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtWhithholdigTaxValue, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtWhithholdigTaxValue, False)
        Me.INDTxtWhithholdigTaxValue.Location = New System.Drawing.Point(4782, 157)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtWhithholdigTaxValue, Presentation.Controls.IndigoTextEdit.EMask.MonedaDecimales)
        Me.INDTxtWhithholdigTaxValue.Name = "INDTxtWhithholdigTaxValue"
        Me.INDTxtWhithholdigTaxValue.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtWhithholdigTaxValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtWhithholdigTaxValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtWhithholdigTaxValue.Properties.Appearance.Options.UseFont = True
        Me.INDTxtWhithholdigTaxValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTxtWhithholdigTaxValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTxtWhithholdigTaxValue.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtWhithholdigTaxValue.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtWhithholdigTaxValue.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtWhithholdigTaxValue.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtWhithholdigTaxValue.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtWhithholdigTaxValue.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtWhithholdigTaxValue.Properties.Mask.EditMask = "c2"
        Me.INDTxtWhithholdigTaxValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDTxtWhithholdigTaxValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtWhithholdigTaxValue.Size = New System.Drawing.Size(262, 28)
        Me.INDTxtWhithholdigTaxValue.StyleController = Me.LyRefundPurchase
        Me.INDTxtWhithholdigTaxValue.TabIndex = 24
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtWhithholdigTaxValue, 0)
        '
        'INDTxtValueTax
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtValueTax, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtValueTax, False)
        Me.INDTxtValueTax.Location = New System.Drawing.Point(4782, 121)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtValueTax, Presentation.Controls.IndigoTextEdit.EMask.MonedaDecimales)
        Me.INDTxtValueTax.Name = "INDTxtValueTax"
        Me.INDTxtValueTax.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtValueTax.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtValueTax.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtValueTax.Properties.Appearance.Options.UseFont = True
        Me.INDTxtValueTax.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTxtValueTax.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTxtValueTax.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtValueTax.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtValueTax.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtValueTax.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtValueTax.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtValueTax.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtValueTax.Properties.Mask.EditMask = "c2"
        Me.INDTxtValueTax.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDTxtValueTax.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtValueTax.Size = New System.Drawing.Size(262, 28)
        Me.INDTxtValueTax.StyleController = Me.LyRefundPurchase
        Me.INDTxtValueTax.TabIndex = 23
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtValueTax, 0)
        '
        'INDTxtDiscountValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtDiscountValue, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtDiscountValue, False)
        Me.INDTxtDiscountValue.Location = New System.Drawing.Point(4782, 85)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtDiscountValue, Presentation.Controls.IndigoTextEdit.EMask.MonedaDecimales)
        Me.INDTxtDiscountValue.Name = "INDTxtDiscountValue"
        Me.INDTxtDiscountValue.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtDiscountValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtDiscountValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtDiscountValue.Properties.Appearance.Options.UseFont = True
        Me.INDTxtDiscountValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTxtDiscountValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTxtDiscountValue.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtDiscountValue.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtDiscountValue.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtDiscountValue.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtDiscountValue.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtDiscountValue.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtDiscountValue.Properties.Mask.EditMask = "c2"
        Me.INDTxtDiscountValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDTxtDiscountValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtDiscountValue.Size = New System.Drawing.Size(262, 28)
        Me.INDTxtDiscountValue.StyleController = Me.LyRefundPurchase
        Me.INDTxtDiscountValue.TabIndex = 22
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtDiscountValue, 0)
        '
        'INDTxtSubTotal
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtSubTotal, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtSubTotal, False)
        Me.INDTxtSubTotal.Location = New System.Drawing.Point(4782, 49)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtSubTotal, Presentation.Controls.IndigoTextEdit.EMask.MonedaDecimales)
        Me.INDTxtSubTotal.Name = "INDTxtSubTotal"
        Me.INDTxtSubTotal.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtSubTotal.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtSubTotal.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtSubTotal.Properties.Appearance.Options.UseFont = True
        Me.INDTxtSubTotal.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTxtSubTotal.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTxtSubTotal.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtSubTotal.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtSubTotal.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtSubTotal.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtSubTotal.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtSubTotal.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtSubTotal.Properties.Mask.EditMask = "c2"
        Me.INDTxtSubTotal.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDTxtSubTotal.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtSubTotal.Size = New System.Drawing.Size(262, 28)
        Me.INDTxtSubTotal.StyleController = Me.LyRefundPurchase
        Me.INDTxtSubTotal.TabIndex = 21
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtSubTotal, 0)
        '
        'INDGcProducts
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcProducts, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcProducts, Nothing)
        Me.INDGcProducts.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.INDGcProducts, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcProducts, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcProducts, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcProducts, False)
        Me.INDGcProducts.Location = New System.Drawing.Point(1258, 49)
        Me.INDGcProducts.MainView = Me.INDGvProducts
        Me.INDGcProducts.Name = "INDGcProducts"
        Me.INDGcProducts.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepoSpeCantDevolutionProduct, Me.INDRptSleDevolutionCause})
        Me.INDGcProducts.Size = New System.Drawing.Size(824, 476)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcProducts, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDGcProducts.TabIndex = 18
        Me.INDGcProducts.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvProducts})
        '
        'INDGvProducts
        '
        Me.INDGvProducts.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvProducts.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvProducts.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDGvProducts.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvProducts.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvProducts.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvProducts.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvProducts.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvProducts.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvProducts.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvProducts.Appearance.Row.Options.UseFont = True
        Me.INDGvProducts.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvProducts.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvProducts.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colProductsCodeName, Me.colProductsBatch, Me.GridColumn10, Me.colProductsOutstandingQuantity, Me.colProductsDevolutionQuantity})
        Me.INDGvProducts.GridControl = Me.INDGcProducts
        Me.INDGvProducts.Name = "INDGvProducts"
        Me.INDGvProducts.OptionsCustomization.AllowGroup = False
        Me.INDGvProducts.OptionsDetail.EnableMasterViewMode = False
        Me.INDGvProducts.OptionsDetail.ShowDetailTabs = False
        Me.INDGvProducts.OptionsView.AllowCellMerge = True
        Me.INDGvProducts.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvProducts.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvProducts.OptionsView.ShowAutoFilterRow = True
        Me.INDGvProducts.OptionsView.ShowDetailButtons = False
        Me.INDGvProducts.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvProducts, False)
        '
        'colProductsCodeName
        '
        Me.colProductsCodeName.Caption = "Producto"
        Me.colProductsCodeName.FieldName = "ProductCodeName"
        Me.colProductsCodeName.Name = "colProductsCodeName"
        Me.colProductsCodeName.OptionsColumn.AllowEdit = False
        Me.colProductsCodeName.OptionsColumn.AllowFocus = False
        Me.colProductsCodeName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.colProductsCodeName.Visible = True
        Me.colProductsCodeName.VisibleIndex = 0
        Me.colProductsCodeName.Width = 258
        '
        'colProductsBatch
        '
        Me.colProductsBatch.Caption = "Lote"
        Me.colProductsBatch.FieldName = "CodeBatchSerial"
        Me.colProductsBatch.Name = "colProductsBatch"
        Me.colProductsBatch.OptionsColumn.AllowEdit = False
        Me.colProductsBatch.OptionsColumn.AllowFocus = False
        Me.colProductsBatch.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.colProductsBatch.Visible = True
        Me.colProductsBatch.VisibleIndex = 1
        Me.colProductsBatch.Width = 114
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "Cau. Devol. Mercancía"
        Me.GridColumn10.ColumnEdit = Me.INDRptSleDevolutionCause
        Me.GridColumn10.FieldName = "DevolutionCauseId"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 2
        Me.GridColumn10.Width = 191
        '
        'INDRptSleDevolutionCause
        '
        Me.INDRptSleDevolutionCause.AutoHeight = False
        Me.INDRptSleDevolutionCause.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDRptSleDevolutionCause.DisplayMember = "Name"
        Me.INDRptSleDevolutionCause.Name = "INDRptSleDevolutionCause"
        Me.INDRptSleDevolutionCause.NullText = ""
        Me.INDRptSleDevolutionCause.PopupView = Me.INDRptGvDevolutionCause
        Me.INDRptSleDevolutionCause.ValueMember = "Id"
        '
        'INDRptGvDevolutionCause
        '
        Me.INDRptGvDevolutionCause.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDRptGvDevolutionCause.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDRptGvDevolutionCause.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDRptGvDevolutionCause.Appearance.FocusedRow.Options.UseFont = True
        Me.INDRptGvDevolutionCause.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDRptGvDevolutionCause.Appearance.GroupRow.Options.UseFont = True
        Me.INDRptGvDevolutionCause.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDRptGvDevolutionCause.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDRptGvDevolutionCause.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDRptGvDevolutionCause.Appearance.Row.Options.UseFont = True
        Me.INDRptGvDevolutionCause.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn8, Me.GridColumn9})
        Me.INDRptGvDevolutionCause.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDRptGvDevolutionCause.Name = "INDRptGvDevolutionCause"
        Me.INDRptGvDevolutionCause.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDRptGvDevolutionCause.OptionsView.EnableAppearanceEvenRow = True
        Me.INDRptGvDevolutionCause.OptionsView.EnableAppearanceOddRow = True
        Me.INDRptGvDevolutionCause.OptionsView.ShowAutoFilterRow = True
        Me.INDRptGvDevolutionCause.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDRptGvDevolutionCause, False)
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Código"
        Me.GridColumn8.FieldName = "Code"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 0
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Nombre"
        Me.GridColumn9.FieldName = "Name"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 1
        '
        'colProductsOutstandingQuantity
        '
        Me.colProductsOutstandingQuantity.Caption = "Cantidad"
        Me.colProductsOutstandingQuantity.FieldName = "OutstandingQuantity"
        Me.colProductsOutstandingQuantity.Name = "colProductsOutstandingQuantity"
        Me.colProductsOutstandingQuantity.OptionsColumn.AllowEdit = False
        Me.colProductsOutstandingQuantity.OptionsColumn.AllowFocus = False
        Me.colProductsOutstandingQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.colProductsOutstandingQuantity.Visible = True
        Me.colProductsOutstandingQuantity.VisibleIndex = 3
        Me.colProductsOutstandingQuantity.Width = 119
        '
        'colProductsDevolutionQuantity
        '
        Me.colProductsDevolutionQuantity.Caption = "Cant. Devolución"
        Me.colProductsDevolutionQuantity.ColumnEdit = Me.RepoSpeCantDevolutionProduct
        Me.colProductsDevolutionQuantity.FieldName = "DevolutionQuantity"
        Me.colProductsDevolutionQuantity.Name = "colProductsDevolutionQuantity"
        Me.colProductsDevolutionQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.colProductsDevolutionQuantity.Visible = True
        Me.colProductsDevolutionQuantity.VisibleIndex = 4
        Me.colProductsDevolutionQuantity.Width = 124
        '
        'RepoSpeCantDevolutionProduct
        '
        Me.RepoSpeCantDevolutionProduct.AutoHeight = False
        Me.RepoSpeCantDevolutionProduct.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepoSpeCantDevolutionProduct.Mask.EditMask = "d"
        Me.RepoSpeCantDevolutionProduct.Name = "RepoSpeCantDevolutionProduct"
        '
        'INDGcOtherDeduction
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcOtherDeduction, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcOtherDeduction, Nothing)
        Me.INDGcOtherDeduction.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.INDGcOtherDeduction, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcOtherDeduction, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcOtherDeduction, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcOtherDeduction, False)
        Me.INDGcOtherDeduction.Location = New System.Drawing.Point(3808, 49)
        Me.INDGcOtherDeduction.MainView = Me.INDGvOtherDeduction
        Me.INDGcOtherDeduction.Name = "INDGcOtherDeduction"
        Me.INDGcOtherDeduction.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit2, Me.RepoSpeDeductionValueDevolution})
        Me.INDGcOtherDeduction.Size = New System.Drawing.Size(824, 476)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcOtherDeduction, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDGcOtherDeduction.TabIndex = 19
        Me.INDGcOtherDeduction.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvOtherDeduction})
        '
        'INDGvOtherDeduction
        '
        Me.INDGvOtherDeduction.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvOtherDeduction.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvOtherDeduction.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDGvOtherDeduction.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvOtherDeduction.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvOtherDeduction.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvOtherDeduction.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvOtherDeduction.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvOtherDeduction.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvOtherDeduction.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvOtherDeduction.Appearance.Row.Options.UseFont = True
        Me.INDGvOtherDeduction.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvOtherDeduction.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvOtherDeduction.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColDeductionConcept, Me.ColBaseValueDeduction, Me.ColDeductionValue})
        Me.INDGvOtherDeduction.GridControl = Me.INDGcOtherDeduction
        Me.INDGvOtherDeduction.Name = "INDGvOtherDeduction"
        Me.INDGvOtherDeduction.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvOtherDeduction.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvOtherDeduction.OptionsView.ShowAutoFilterRow = True
        Me.INDGvOtherDeduction.OptionsView.ShowDetailButtons = False
        Me.INDGvOtherDeduction.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvOtherDeduction, False)
        '
        'ColDeductionConcept
        '
        Me.ColDeductionConcept.Caption = "Concepto Deducción"
        Me.ColDeductionConcept.FieldName = "ConceptName"
        Me.ColDeductionConcept.Name = "ColDeductionConcept"
        Me.ColDeductionConcept.OptionsColumn.AllowEdit = False
        Me.ColDeductionConcept.OptionsColumn.AllowFocus = False
        Me.ColDeductionConcept.OptionsColumn.AllowMove = False
        Me.ColDeductionConcept.OptionsColumn.AllowSize = False
        Me.ColDeductionConcept.Visible = True
        Me.ColDeductionConcept.VisibleIndex = 0
        Me.ColDeductionConcept.Width = 395
        '
        'ColBaseValueDeduction
        '
        Me.ColBaseValueDeduction.Caption = "Valor Dedución"
        Me.ColBaseValueDeduction.DisplayFormat.FormatString = "c0"
        Me.ColBaseValueDeduction.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ColBaseValueDeduction.FieldName = "Value"
        Me.ColBaseValueDeduction.Name = "ColBaseValueDeduction"
        Me.ColBaseValueDeduction.OptionsColumn.AllowEdit = False
        Me.ColBaseValueDeduction.OptionsColumn.AllowFocus = False
        Me.ColBaseValueDeduction.OptionsColumn.AllowMove = False
        Me.ColBaseValueDeduction.OptionsColumn.AllowSize = False
        Me.ColBaseValueDeduction.Visible = True
        Me.ColBaseValueDeduction.VisibleIndex = 1
        Me.ColBaseValueDeduction.Width = 144
        '
        'ColDeductionValue
        '
        Me.ColDeductionValue.Caption = "Valor Devolver"
        Me.ColDeductionValue.ColumnEdit = Me.RepoSpeDeductionValueDevolution
        Me.ColDeductionValue.DisplayFormat.FormatString = "c0"
        Me.ColDeductionValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ColDeductionValue.FieldName = "DevolutionValue"
        Me.ColDeductionValue.Name = "ColDeductionValue"
        Me.ColDeductionValue.OptionsColumn.AllowEdit = False
        Me.ColDeductionValue.OptionsColumn.AllowFocus = False
        Me.ColDeductionValue.OptionsColumn.AllowMove = False
        Me.ColDeductionValue.OptionsColumn.AllowSize = False
        Me.ColDeductionValue.Visible = True
        Me.ColDeductionValue.VisibleIndex = 2
        Me.ColDeductionValue.Width = 218
        '
        'RepoSpeDeductionValueDevolution
        '
        Me.RepoSpeDeductionValueDevolution.AutoHeight = False
        Me.RepoSpeDeductionValueDevolution.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepoSpeDeductionValueDevolution.Mask.BeepOnError = True
        Me.RepoSpeDeductionValueDevolution.Mask.EditMask = "c0"
        Me.RepoSpeDeductionValueDevolution.Name = "RepoSpeDeductionValueDevolution"
        '
        'RepositoryItemCheckEdit2
        '
        Me.RepositoryItemCheckEdit2.AutoHeight = False
        Me.RepositoryItemCheckEdit2.Name = "RepositoryItemCheckEdit2"
        '
        'INDGcOtherWithholding
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcOtherWithholding, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcOtherWithholding, Nothing)
        Me.INDGcOtherWithholding.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.INDGcOtherWithholding, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcOtherWithholding, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcOtherWithholding, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcOtherWithholding, False)
        Me.INDGcOtherWithholding.Location = New System.Drawing.Point(2958, 49)
        Me.INDGcOtherWithholding.MainView = Me.INDGvOtherWithholding
        Me.INDGcOtherWithholding.Name = "INDGcOtherWithholding"
        Me.INDGcOtherWithholding.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1, Me.RepositoryItemSpinEdit3, Me.RepositoryItemTextEdit1})
        Me.INDGcOtherWithholding.Size = New System.Drawing.Size(824, 476)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcOtherWithholding, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDGcOtherWithholding.TabIndex = 18
        Me.INDGcOtherWithholding.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvOtherWithholding})
        '
        'INDGvOtherWithholding
        '
        Me.INDGvOtherWithholding.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvOtherWithholding.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvOtherWithholding.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDGvOtherWithholding.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvOtherWithholding.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvOtherWithholding.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvOtherWithholding.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvOtherWithholding.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvOtherWithholding.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvOtherWithholding.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvOtherWithholding.Appearance.Row.Options.UseFont = True
        Me.INDGvOtherWithholding.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvOtherWithholding.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvOtherWithholding.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColConceptName, Me.ColRetentionPercentage, Me.ColRetentionBaseValue, Me.ColWhitholdingValue})
        Me.INDGvOtherWithholding.GridControl = Me.INDGcOtherWithholding
        Me.INDGvOtherWithholding.Name = "INDGvOtherWithholding"
        Me.INDGvOtherWithholding.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvOtherWithholding.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvOtherWithholding.OptionsView.ShowAutoFilterRow = True
        Me.INDGvOtherWithholding.OptionsView.ShowDetailButtons = False
        Me.INDGvOtherWithholding.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvOtherWithholding, False)
        '
        'ColConceptName
        '
        Me.ColConceptName.Caption = "Concepto Retención"
        Me.ColConceptName.FieldName = "ConceptName"
        Me.ColConceptName.Name = "ColConceptName"
        Me.ColConceptName.OptionsColumn.AllowEdit = False
        Me.ColConceptName.OptionsColumn.AllowFocus = False
        Me.ColConceptName.OptionsColumn.AllowMove = False
        Me.ColConceptName.OptionsColumn.AllowSize = False
        Me.ColConceptName.Visible = True
        Me.ColConceptName.VisibleIndex = 0
        Me.ColConceptName.Width = 310
        '
        'ColRetentionPercentage
        '
        Me.ColRetentionPercentage.Caption = "% Retención"
        Me.ColRetentionPercentage.ColumnEdit = Me.RepositoryItemSpinEdit3
        Me.ColRetentionPercentage.DisplayFormat.FormatString = "P"
        Me.ColRetentionPercentage.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ColRetentionPercentage.FieldName = "RetentionPercentage"
        Me.ColRetentionPercentage.Name = "ColRetentionPercentage"
        Me.ColRetentionPercentage.OptionsColumn.AllowEdit = False
        Me.ColRetentionPercentage.OptionsColumn.AllowFocus = False
        Me.ColRetentionPercentage.OptionsColumn.AllowMove = False
        Me.ColRetentionPercentage.OptionsColumn.AllowSize = False
        Me.ColRetentionPercentage.Visible = True
        Me.ColRetentionPercentage.VisibleIndex = 1
        Me.ColRetentionPercentage.Width = 107
        '
        'RepositoryItemSpinEdit3
        '
        Me.RepositoryItemSpinEdit3.AutoHeight = False
        Me.RepositoryItemSpinEdit3.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemSpinEdit3.Mask.EditMask = "P"
        Me.RepositoryItemSpinEdit3.Mask.UseMaskAsDisplayFormat = True
        Me.RepositoryItemSpinEdit3.Name = "RepositoryItemSpinEdit3"
        '
        'ColRetentionBaseValue
        '
        Me.ColRetentionBaseValue.Caption = "Valor Base"
        Me.ColRetentionBaseValue.ColumnEdit = Me.RepositoryItemTextEdit1
        Me.ColRetentionBaseValue.DisplayFormat.FormatString = "c0"
        Me.ColRetentionBaseValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ColRetentionBaseValue.FieldName = "ValueBase"
        Me.ColRetentionBaseValue.Name = "ColRetentionBaseValue"
        Me.ColRetentionBaseValue.OptionsColumn.AllowEdit = False
        Me.ColRetentionBaseValue.OptionsColumn.AllowFocus = False
        Me.ColRetentionBaseValue.OptionsColumn.AllowMove = False
        Me.ColRetentionBaseValue.OptionsColumn.AllowSize = False
        Me.ColRetentionBaseValue.Visible = True
        Me.ColRetentionBaseValue.VisibleIndex = 2
        Me.ColRetentionBaseValue.Width = 145
        '
        'RepositoryItemTextEdit1
        '
        Me.RepositoryItemTextEdit1.AutoHeight = False
        Me.RepositoryItemTextEdit1.DisplayFormat.FormatString = "c0"
        Me.RepositoryItemTextEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemTextEdit1.Mask.BeepOnError = True
        Me.RepositoryItemTextEdit1.Mask.EditMask = "c0"
        Me.RepositoryItemTextEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.RepositoryItemTextEdit1.Mask.UseMaskAsDisplayFormat = True
        Me.RepositoryItemTextEdit1.Name = "RepositoryItemTextEdit1"
        '
        'ColWhitholdingValue
        '
        Me.ColWhitholdingValue.Caption = "Valor Retención"
        Me.ColWhitholdingValue.ColumnEdit = Me.RepositoryItemTextEdit1
        Me.ColWhitholdingValue.DisplayFormat.FormatString = "c"
        Me.ColWhitholdingValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ColWhitholdingValue.FieldName = "DevolutionValue"
        Me.ColWhitholdingValue.Name = "ColWhitholdingValue"
        Me.ColWhitholdingValue.OptionsColumn.AllowEdit = False
        Me.ColWhitholdingValue.OptionsColumn.AllowFocus = False
        Me.ColWhitholdingValue.OptionsColumn.AllowMove = False
        Me.ColWhitholdingValue.OptionsColumn.AllowSize = False
        Me.ColWhitholdingValue.UnboundExpression = "[BaseValue] *( [RetentionPercentage] / 100)"
        Me.ColWhitholdingValue.UnboundType = DevExpress.Data.UnboundColumnType.[Decimal]
        Me.ColWhitholdingValue.Visible = True
        Me.ColWhitholdingValue.VisibleIndex = 3
        Me.ColWhitholdingValue.Width = 199
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        '
        'INDSpnFreightIvaPercentage
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSpnFreightIvaPercentage, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSpnFreightIvaPercentage, False)
        Me.INDSpnFreightIvaPercentage.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDSpnFreightIvaPercentage.EnterMoveNextControl = True
        Me.INDSpnFreightIvaPercentage.Location = New System.Drawing.Point(970, 121)
        Me.IndigoTextEdit1.SetMascara(Me.INDSpnFreightIvaPercentage, Presentation.Controls.IndigoTextEdit.EMask.Porcentaje)
        Me.INDSpnFreightIvaPercentage.Name = "INDSpnFreightIvaPercentage"
        Me.INDSpnFreightIvaPercentage.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSpnFreightIvaPercentage.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSpnFreightIvaPercentage.Properties.Appearance.Options.UseBackColor = True
        Me.INDSpnFreightIvaPercentage.Properties.Appearance.Options.UseFont = True
        Me.INDSpnFreightIvaPercentage.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSpnFreightIvaPercentage.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSpnFreightIvaPercentage.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSpnFreightIvaPercentage.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSpnFreightIvaPercentage.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSpnFreightIvaPercentage.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSpnFreightIvaPercentage.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSpnFreightIvaPercentage.Properties.Mask.EditMask = "P"
        Me.INDSpnFreightIvaPercentage.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDSpnFreightIvaPercentage.Size = New System.Drawing.Size(262, 28)
        Me.INDSpnFreightIvaPercentage.StyleController = Me.LyRefundPurchase
        Me.INDSpnFreightIvaPercentage.TabIndex = 16
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSpnFreightIvaPercentage, 0)
        '
        'INDTxtDescription
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtDescription, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtDescription, True)
        Me.INDTxtDescription.EnterMoveNextControl = True
        Me.INDTxtDescription.Location = New System.Drawing.Point(146, 157)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtDescription, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtDescription.Name = "INDTxtDescription"
        Me.INDTxtDescription.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDTxtDescription.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtDescription.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDTxtDescription.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtDescription.Properties.Appearance.Options.UseFont = True
        Me.INDTxtDescription.Properties.Appearance.Options.UseForeColor = True
        Me.INDTxtDescription.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtDescription.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtDescription.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtDescription.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtDescription.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtDescription.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtDescription.Size = New System.Drawing.Size(262, 68)
        Me.INDTxtDescription.StyleController = Me.LyRefundPurchase
        Me.INDTxtDescription.TabIndex = 16
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtDescription, 0)
        Me.INDTxtDescription.ToolTip = "Este Campo es Necesario"
        '
        'INDTxtFreightInvoice
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtFreightInvoice, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtFreightInvoice, False)
        Me.INDTxtFreightInvoice.EnterMoveNextControl = True
        Me.INDTxtFreightInvoice.Location = New System.Drawing.Point(970, 85)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtFreightInvoice, Presentation.Controls.IndigoTextEdit.EMask.Moneda)
        Me.INDTxtFreightInvoice.Name = "INDTxtFreightInvoice"
        Me.INDTxtFreightInvoice.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtFreightInvoice.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtFreightInvoice.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtFreightInvoice.Properties.Appearance.Options.UseFont = True
        Me.INDTxtFreightInvoice.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTxtFreightInvoice.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTxtFreightInvoice.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtFreightInvoice.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtFreightInvoice.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtFreightInvoice.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtFreightInvoice.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtFreightInvoice.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtFreightInvoice.Properties.Mask.EditMask = "c0"
        Me.INDTxtFreightInvoice.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDTxtFreightInvoice.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtFreightInvoice.Size = New System.Drawing.Size(262, 28)
        Me.INDTxtFreightInvoice.StyleController = Me.LyRefundPurchase
        Me.INDTxtFreightInvoice.TabIndex = 15
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtFreightInvoice, 0)
        '
        'INDTxtFreightValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtFreightValue, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtFreightValue, False)
        Me.INDTxtFreightValue.EnterMoveNextControl = True
        Me.INDTxtFreightValue.Location = New System.Drawing.Point(970, 49)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtFreightValue, Presentation.Controls.IndigoTextEdit.EMask.Moneda)
        Me.INDTxtFreightValue.Name = "INDTxtFreightValue"
        Me.INDTxtFreightValue.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtFreightValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtFreightValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtFreightValue.Properties.Appearance.Options.UseFont = True
        Me.INDTxtFreightValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTxtFreightValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTxtFreightValue.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtFreightValue.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtFreightValue.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtFreightValue.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtFreightValue.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtFreightValue.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtFreightValue.Properties.Mask.EditMask = "c0"
        Me.INDTxtFreightValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDTxtFreightValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtFreightValue.Size = New System.Drawing.Size(262, 28)
        Me.INDTxtFreightValue.StyleController = Me.LyRefundPurchase
        Me.INDTxtFreightValue.TabIndex = 14
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtFreightValue, 0)
        '
        'INDTxtDayPeriod
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtDayPeriod, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtDayPeriod, False)
        Me.INDTxtDayPeriod.EnterMoveNextControl = True
        Me.INDTxtDayPeriod.Location = New System.Drawing.Point(558, 265)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtDayPeriod, Presentation.Controls.IndigoTextEdit.EMask.Numerico)
        Me.INDTxtDayPeriod.Name = "INDTxtDayPeriod"
        Me.INDTxtDayPeriod.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtDayPeriod.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtDayPeriod.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtDayPeriod.Properties.Appearance.Options.UseFont = True
        Me.INDTxtDayPeriod.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtDayPeriod.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtDayPeriod.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtDayPeriod.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtDayPeriod.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtDayPeriod.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtDayPeriod.Properties.Mask.EditMask = "[0-9]+"
        Me.INDTxtDayPeriod.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDTxtDayPeriod.Properties.ReadOnly = True
        Me.INDTxtDayPeriod.Size = New System.Drawing.Size(262, 28)
        Me.INDTxtDayPeriod.StyleController = Me.LyRefundPurchase
        Me.INDTxtDayPeriod.TabIndex = 13
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtDayPeriod, 0)
        '
        'INDTxtInvoiceNumber
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtInvoiceNumber, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtInvoiceNumber, False)
        Me.INDTxtInvoiceNumber.EnterMoveNextControl = True
        Me.INDTxtInvoiceNumber.Location = New System.Drawing.Point(558, 193)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtInvoiceNumber, Presentation.Controls.IndigoTextEdit.EMask.Numerico)
        Me.INDTxtInvoiceNumber.Name = "INDTxtInvoiceNumber"
        Me.INDTxtInvoiceNumber.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtInvoiceNumber.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtInvoiceNumber.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtInvoiceNumber.Properties.Appearance.Options.UseFont = True
        Me.INDTxtInvoiceNumber.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtInvoiceNumber.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtInvoiceNumber.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtInvoiceNumber.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtInvoiceNumber.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtInvoiceNumber.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtInvoiceNumber.Properties.Mask.EditMask = "[0-9]+"
        Me.INDTxtInvoiceNumber.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDTxtInvoiceNumber.Properties.ReadOnly = True
        Me.INDTxtInvoiceNumber.Size = New System.Drawing.Size(262, 28)
        Me.INDTxtInvoiceNumber.StyleController = Me.LyRefundPurchase
        Me.INDTxtInvoiceNumber.TabIndex = 11
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtInvoiceNumber, 0)
        '
        'INDDteDateEntranceVoucher
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDDteDateEntranceVoucher, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDDteDateEntranceVoucher, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDDteDateEntranceVoucher, False)
        Me.INDDteDateEntranceVoucher.EditValue = Nothing
        Me.INDDteDateEntranceVoucher.EnterMoveNextControl = True
        Me.INDDteDateEntranceVoucher.Location = New System.Drawing.Point(558, 85)
        Me.IndigoTextEdit1.SetMascara(Me.INDDteDateEntranceVoucher, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDDteDateEntranceVoucher, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDDteDateEntranceVoucher.Name = "INDDteDateEntranceVoucher"
        Me.INDDteDateEntranceVoucher.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDDteDateEntranceVoucher.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDteDateEntranceVoucher.Properties.Appearance.Options.UseBackColor = True
        Me.INDDteDateEntranceVoucher.Properties.Appearance.Options.UseFont = True
        Me.INDDteDateEntranceVoucher.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDDteDateEntranceVoucher.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDDteDateEntranceVoucher.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDteDateEntranceVoucher.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDDteDateEntranceVoucher.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDDteDateEntranceVoucher.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDteDateEntranceVoucher.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDteDateEntranceVoucher.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.INDDteDateEntranceVoucher.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDteDateEntranceVoucher.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDteDateEntranceVoucher.Properties.ReadOnly = True
        Me.INDDteDateEntranceVoucher.Properties.ShowPopupShadow = False
        Me.INDDteDateEntranceVoucher.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.INDDteDateEntranceVoucher.Size = New System.Drawing.Size(262, 28)
        Me.INDDteDateEntranceVoucher.StyleController = Me.LyRefundPurchase
        Me.INDDteDateEntranceVoucher.TabIndex = 8
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDDteDateEntranceVoucher, 0)
        '
        'INDTxtWarehouse
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtWarehouse, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtWarehouse, False)
        Me.INDTxtWarehouse.EnterMoveNextControl = True
        Me.INDTxtWarehouse.Location = New System.Drawing.Point(558, 157)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtWarehouse, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtWarehouse.Name = "INDTxtWarehouse"
        Me.INDTxtWarehouse.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtWarehouse.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtWarehouse.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtWarehouse.Properties.Appearance.Options.UseFont = True
        Me.INDTxtWarehouse.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtWarehouse.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtWarehouse.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtWarehouse.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtWarehouse.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtWarehouse.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtWarehouse.Properties.ReadOnly = True
        Me.INDTxtWarehouse.Size = New System.Drawing.Size(262, 28)
        Me.INDTxtWarehouse.StyleController = Me.LyRefundPurchase
        Me.INDTxtWarehouse.TabIndex = 10
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtWarehouse, 0)
        '
        'INDTxtSupplier
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtSupplier, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtSupplier, False)
        Me.INDTxtSupplier.EnterMoveNextControl = True
        Me.INDTxtSupplier.Location = New System.Drawing.Point(558, 121)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtSupplier, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtSupplier.Name = "INDTxtSupplier"
        Me.INDTxtSupplier.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtSupplier.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtSupplier.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtSupplier.Properties.Appearance.Options.UseFont = True
        Me.INDTxtSupplier.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtSupplier.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtSupplier.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtSupplier.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtSupplier.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtSupplier.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtSupplier.Properties.ReadOnly = True
        Me.INDTxtSupplier.Size = New System.Drawing.Size(262, 28)
        Me.INDTxtSupplier.StyleController = Me.LyRefundPurchase
        Me.INDTxtSupplier.TabIndex = 9
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtSupplier, 0)
        '
        'INDSleVoucherCode
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleVoucherCode, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleVoucherCode, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleVoucherCode, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleVoucherCode, True)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleVoucherCode, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleVoucherCode, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleVoucherCode, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleVoucherCode, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleVoucherCode, False)
        Me.INDSleVoucherCode.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleVoucherCode, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleVoucherCode, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleVoucherCode, False)
        Me.INDSleVoucherCode.Location = New System.Drawing.Point(558, 49)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleVoucherCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleVoucherCode.Name = "INDSleVoucherCode"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleVoucherCode, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleVoucherCode, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleVoucherCode, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDSleVoucherCode, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleVoucherCode, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleVoucherCode, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleVoucherCode, False)
        Me.INDSleVoucherCode.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDSleVoucherCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleVoucherCode.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleVoucherCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleVoucherCode.Properties.Appearance.Options.UseFont = True
        Me.INDSleVoucherCode.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleVoucherCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleVoucherCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSleVoucherCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleVoucherCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSleVoucherCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSleVoucherCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleVoucherCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleVoucherCode.Properties.DisplayMember = "Code"
        Me.INDSleVoucherCode.Properties.NullText = ""
        Me.INDSleVoucherCode.Properties.PopupSizeable = False
        Me.INDSleVoucherCode.Properties.PopupView = Me.INDSlvEntranceVoucher
        Me.INDSleVoucherCode.Properties.ShowClearButton = False
        Me.INDSleVoucherCode.Properties.ShowFooter = False
        Me.INDSleVoucherCode.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleVoucherCode, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleVoucherCode, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleVoucherCode, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleVoucherCode, True)
        Me.INDSleVoucherCode.Size = New System.Drawing.Size(262, 28)
        Me.INDSleVoucherCode.StyleController = Me.LyRefundPurchase
        Me.INDSleVoucherCode.TabIndex = 7
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleVoucherCode, "1402")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleVoucherCode, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleVoucherCode, "{0} - {1}")
        Me.INDSleVoucherCode.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleVoucherCode, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleVoucherCode, False)
        '
        'INDSlvEntranceVoucher
        '
        Me.INDSlvEntranceVoucher.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDSlvEntranceVoucher.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDSlvEntranceVoucher.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDSlvEntranceVoucher.Appearance.FocusedRow.Options.UseFont = True
        Me.INDSlvEntranceVoucher.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSlvEntranceVoucher.Appearance.GroupRow.Options.UseFont = True
        Me.INDSlvEntranceVoucher.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSlvEntranceVoucher.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDSlvEntranceVoucher.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDSlvEntranceVoucher.Appearance.Row.Options.UseFont = True
        Me.INDSlvEntranceVoucher.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colSleEntranceVoucherDateDocumento, Me.colSleEntranceVoucherCode, Me.colSleEntranceVoucherSupplier, Me.colEntranceVoucherWarehouse})
        Me.INDSlvEntranceVoucher.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDSlvEntranceVoucher.Name = "INDSlvEntranceVoucher"
        Me.INDSlvEntranceVoucher.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDSlvEntranceVoucher.OptionsView.EnableAppearanceEvenRow = True
        Me.INDSlvEntranceVoucher.OptionsView.EnableAppearanceOddRow = True
        Me.INDSlvEntranceVoucher.OptionsView.ShowAutoFilterRow = True
        Me.INDSlvEntranceVoucher.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDSlvEntranceVoucher, False)
        '
        'colSleEntranceVoucherDateDocumento
        '
        Me.colSleEntranceVoucherDateDocumento.Caption = "Fecha"
        Me.colSleEntranceVoucherDateDocumento.FieldName = "DocumentDate"
        Me.colSleEntranceVoucherDateDocumento.Name = "colSleEntranceVoucherDateDocumento"
        Me.colSleEntranceVoucherDateDocumento.OptionsColumn.AllowEdit = False
        Me.colSleEntranceVoucherDateDocumento.OptionsColumn.AllowFocus = False
        Me.colSleEntranceVoucherDateDocumento.OptionsColumn.AllowMove = False
        Me.colSleEntranceVoucherDateDocumento.OptionsColumn.AllowSize = False
        Me.colSleEntranceVoucherDateDocumento.Visible = True
        Me.colSleEntranceVoucherDateDocumento.VisibleIndex = 0
        Me.colSleEntranceVoucherDateDocumento.Width = 253
        '
        'colSleEntranceVoucherCode
        '
        Me.colSleEntranceVoucherCode.Caption = "Código"
        Me.colSleEntranceVoucherCode.FieldName = "Code"
        Me.colSleEntranceVoucherCode.Name = "colSleEntranceVoucherCode"
        Me.colSleEntranceVoucherCode.OptionsColumn.AllowEdit = False
        Me.colSleEntranceVoucherCode.OptionsColumn.AllowFocus = False
        Me.colSleEntranceVoucherCode.OptionsColumn.AllowMove = False
        Me.colSleEntranceVoucherCode.OptionsColumn.AllowSize = False
        Me.colSleEntranceVoucherCode.Visible = True
        Me.colSleEntranceVoucherCode.VisibleIndex = 1
        Me.colSleEntranceVoucherCode.Width = 281
        '
        'colSleEntranceVoucherSupplier
        '
        Me.colSleEntranceVoucherSupplier.Caption = "Proveedor"
        Me.colSleEntranceVoucherSupplier.FieldName = "SupplierId.CodeName"
        Me.colSleEntranceVoucherSupplier.Name = "colSleEntranceVoucherSupplier"
        Me.colSleEntranceVoucherSupplier.OptionsColumn.AllowEdit = False
        Me.colSleEntranceVoucherSupplier.OptionsColumn.AllowFocus = False
        Me.colSleEntranceVoucherSupplier.OptionsColumn.AllowMove = False
        Me.colSleEntranceVoucherSupplier.OptionsColumn.AllowSize = False
        Me.colSleEntranceVoucherSupplier.Visible = True
        Me.colSleEntranceVoucherSupplier.VisibleIndex = 2
        Me.colSleEntranceVoucherSupplier.Width = 381
        '
        'colEntranceVoucherWarehouse
        '
        Me.colEntranceVoucherWarehouse.Caption = "Almacen"
        Me.colEntranceVoucherWarehouse.FieldName = "WarehouseId.CodeName"
        Me.colEntranceVoucherWarehouse.Name = "colEntranceVoucherWarehouse"
        Me.colEntranceVoucherWarehouse.OptionsColumn.AllowEdit = False
        Me.colEntranceVoucherWarehouse.OptionsColumn.AllowFocus = False
        Me.colEntranceVoucherWarehouse.OptionsColumn.AllowMove = False
        Me.colEntranceVoucherWarehouse.OptionsColumn.AllowSize = False
        Me.colEntranceVoucherWarehouse.Visible = True
        Me.colEntranceVoucherWarehouse.VisibleIndex = 3
        Me.colEntranceVoucherWarehouse.Width = 397
        '
        'INDSleWarehouse
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleWarehouse, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleWarehouse, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleWarehouse, True)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleWarehouse, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleWarehouse, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleWarehouse, False)
        Me.INDSleWarehouse.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleWarehouse, False)
        Me.INDSleWarehouse.Location = New System.Drawing.Point(146, 121)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleWarehouse, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleWarehouse.Name = "INDSleWarehouse"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleWarehouse, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleWarehouse, False)
        Me.INDSleWarehouse.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDSleWarehouse.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleWarehouse.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleWarehouse.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleWarehouse.Properties.Appearance.Options.UseFont = True
        Me.INDSleWarehouse.Properties.Appearance.Options.UseForeColor = True
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
        Me.INDSleWarehouse.Properties.PopupView = Me.INDGdvWarehouse
        Me.INDSleWarehouse.Properties.ShowClearButton = False
        Me.INDSleWarehouse.Properties.ShowFooter = False
        Me.INDSleWarehouse.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleWarehouse, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleWarehouse, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleWarehouse, True)
        Me.INDSleWarehouse.Size = New System.Drawing.Size(262, 28)
        Me.INDSleWarehouse.StyleController = Me.LyRefundPurchase
        Me.INDSleWarehouse.TabIndex = 6
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleWarehouse, "302")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleWarehouse, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleWarehouse, "{0} - {1}")
        Me.INDSleWarehouse.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleWarehouse, False)
        '
        'INDGdvWarehouse
        '
        Me.INDGdvWarehouse.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGdvWarehouse.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGdvWarehouse.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGdvWarehouse.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGdvWarehouse.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGdvWarehouse.Appearance.GroupRow.Options.UseFont = True
        Me.INDGdvWarehouse.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGdvWarehouse.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGdvWarehouse.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGdvWarehouse.Appearance.Row.Options.UseFont = True
        Me.INDGdvWarehouse.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colSleWareHouseCode, Me.colSleWareHouseName})
        Me.INDGdvWarehouse.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGdvWarehouse.Name = "INDGdvWarehouse"
        Me.INDGdvWarehouse.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGdvWarehouse.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGdvWarehouse.OptionsView.EnableAppearanceOddRow = True
        Me.INDGdvWarehouse.OptionsView.ShowAutoFilterRow = True
        Me.INDGdvWarehouse.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGdvWarehouse, False)
        '
        'colSleWareHouseCode
        '
        Me.colSleWareHouseCode.Caption = "Código"
        Me.colSleWareHouseCode.FieldName = "Code"
        Me.colSleWareHouseCode.Name = "colSleWareHouseCode"
        Me.colSleWareHouseCode.OptionsColumn.AllowEdit = False
        Me.colSleWareHouseCode.OptionsColumn.AllowFocus = False
        Me.colSleWareHouseCode.OptionsColumn.AllowMove = False
        Me.colSleWareHouseCode.OptionsColumn.AllowSize = False
        Me.colSleWareHouseCode.Visible = True
        Me.colSleWareHouseCode.VisibleIndex = 0
        Me.colSleWareHouseCode.Width = 334
        '
        'colSleWareHouseName
        '
        Me.colSleWareHouseName.Caption = "Nombre"
        Me.colSleWareHouseName.FieldName = "Name"
        Me.colSleWareHouseName.Name = "colSleWareHouseName"
        Me.colSleWareHouseName.OptionsColumn.AllowEdit = False
        Me.colSleWareHouseName.OptionsColumn.AllowFocus = False
        Me.colSleWareHouseName.OptionsColumn.AllowMove = False
        Me.colSleWareHouseName.OptionsColumn.AllowSize = False
        Me.colSleWareHouseName.Visible = True
        Me.colSleWareHouseName.VisibleIndex = 1
        Me.colSleWareHouseName.Width = 978
        '
        'INDDteDocumentDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDDteDocumentDate, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDDteDocumentDate, True)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDDteDocumentDate, True)
        Me.INDDteDocumentDate.EditValue = Nothing
        Me.INDDteDocumentDate.EnterMoveNextControl = True
        Me.INDDteDocumentDate.Location = New System.Drawing.Point(146, 85)
        Me.IndigoTextEdit1.SetMascara(Me.INDDteDocumentDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDDteDocumentDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDDteDocumentDate.Name = "INDDteDocumentDate"
        Me.INDDteDocumentDate.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDDteDocumentDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDteDocumentDate.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDDteDocumentDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDDteDocumentDate.Properties.Appearance.Options.UseFont = True
        Me.INDDteDocumentDate.Properties.Appearance.Options.UseForeColor = True
        Me.INDDteDocumentDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDDteDocumentDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDDteDocumentDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDteDocumentDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDDteDocumentDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDDteDocumentDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDteDocumentDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDteDocumentDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDteDocumentDate.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.INDDteDocumentDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDteDocumentDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDteDocumentDate.Size = New System.Drawing.Size(262, 28)
        Me.INDDteDocumentDate.StyleController = Me.LyRefundPurchase
        Me.INDDteDocumentDate.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDDteDocumentDate, 0)
        Me.INDDteDocumentDate.ToolTip = "Este Campo es Necesario"
        '
        'INDBtnCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDBtnCode, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDBtnCode, True)
        Me.INDBtnCode.Location = New System.Drawing.Point(146, 49)
        Me.IndigoTextEdit1.SetMascara(Me.INDBtnCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDBtnCode.Name = "INDBtnCode"
        Me.INDBtnCode.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDBtnCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBtnCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDBtnCode.Properties.Appearance.Options.UseFont = True
        Me.INDBtnCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDBtnCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDBtnCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBtnCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDBtnCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDBtnCode.Properties.AppearanceFocused.Options.UseFont = True
        EditorButtonImageOptions1.Image = Global.Presentation.Inventory.My.Resources.Resources.BuscarMetro
        Me.INDBtnCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDBtnCode.Properties.MaxLength = 20
        Me.INDBtnCode.Size = New System.Drawing.Size(262, 28)
        Me.INDBtnCode.StyleController = Me.LyRefundPurchase
        Me.INDBtnCode.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDBtnCode, 0)
        Me.INDBtnCode.ToolTip = "Este Campo es Necesario"
        '
        'INDTxtInvoiceDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtInvoiceDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtInvoiceDate, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDTxtInvoiceDate, False)
        Me.INDTxtInvoiceDate.EditValue = Nothing
        Me.INDTxtInvoiceDate.EnterMoveNextControl = True
        Me.INDTxtInvoiceDate.Location = New System.Drawing.Point(558, 229)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtInvoiceDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDTxtInvoiceDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDTxtInvoiceDate.Name = "INDTxtInvoiceDate"
        Me.INDTxtInvoiceDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtInvoiceDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtInvoiceDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtInvoiceDate.Properties.Appearance.Options.UseFont = True
        Me.INDTxtInvoiceDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtInvoiceDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtInvoiceDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtInvoiceDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtInvoiceDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtInvoiceDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtInvoiceDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDTxtInvoiceDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDTxtInvoiceDate.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.INDTxtInvoiceDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDTxtInvoiceDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtInvoiceDate.Properties.ReadOnly = True
        Me.INDTxtInvoiceDate.Properties.ShowPopupShadow = False
        Me.INDTxtInvoiceDate.Size = New System.Drawing.Size(262, 28)
        Me.INDTxtInvoiceDate.StyleController = Me.LyRefundPurchase
        Me.INDTxtInvoiceDate.TabIndex = 12
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtInvoiceDate, 0)
        '
        'LyGpRefundPurchase
        '
        Me.LyGpRefundPurchase.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LyGpRefundPurchase.AppearanceGroup.Options.UseFont = True
        Me.LyGpRefundPurchase.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LyGpRefundPurchase.AppearanceItemCaption.Options.UseFont = True
        Me.LyGpRefundPurchase.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LyGpRefundPurchase.AppearanceTabPage.Header.Options.UseFont = True
        Me.LyGpRefundPurchase.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LyGpRefundPurchase.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LyGpRefundPurchase.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LyGpRefundPurchase.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LyGpRefundPurchase.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LyGpRefundPurchase.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LyGpRefundPurchase.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LyGpRefundPurchase.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LyGpRefundPurchase, False)
        Me.LyGpRefundPurchase.CustomizationFormText = "Devolución de Compra"
        Me.LyGpRefundPurchase.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LyGpRefundPurchase.GroupBordersVisible = False
        Me.LyGpRefundPurchase.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LyGroupMainData, Me.LyGroupEntranceVoucherInformation, Me.LyGroupFreight, Me.LyGroupProducts, Me.INDlygBudget, Me.LyGroupOtherRetention, Me.LyGroupOtherDeduction, Me.LyGroupLiquitationInvoice})
        Me.LyGpRefundPurchase.Name = "Root"
        Me.LyGpRefundPurchase.Size = New System.Drawing.Size(5066, 545)
        Me.LyGpRefundPurchase.TextVisible = False
        '
        'LyGroupMainData
        '
        Me.LyGroupMainData.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LyGroupMainData.AppearanceGroup.Options.UseFont = True
        Me.LyGroupMainData.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LyGroupMainData.AppearanceItemCaption.Options.UseFont = True
        Me.LyGroupMainData.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LyGroupMainData.AppearanceTabPage.Header.Options.UseFont = True
        Me.LyGroupMainData.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LyGroupMainData.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LyGroupMainData.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LyGroupMainData.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LyGroupMainData.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LyGroupMainData.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LyGroupMainData.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LyGroupMainData.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LyGroupMainData, False)
        Me.LyGroupMainData.CustomizationFormText = "Datos Principales"
        Me.LyGroupMainData.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyBtnCode, Me.INDLyDteDocumentDate, Me.INDLySleWarehouse, Me.INDLyTxtDescription})
        Me.LyGroupMainData.Location = New System.Drawing.Point(0, 0)
        Me.LyGroupMainData.Name = "LyGroupMainData"
        Me.LyGroupMainData.Size = New System.Drawing.Size(412, 529)
        Me.LyGroupMainData.Text = "Datos Principales"
        '
        'INDLyBtnCode
        '
        Me.INDLyBtnCode.Control = Me.INDBtnCode
        Me.INDLyBtnCode.CustomizationFormText = "Código"
        Me.INDLyBtnCode.Location = New System.Drawing.Point(0, 0)
        Me.INDLyBtnCode.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDLyBtnCode.MinSize = New System.Drawing.Size(390, 36)
        Me.INDLyBtnCode.Name = "INDLyBtnCode"
        Me.INDLyBtnCode.ShowInCustomizationForm = False
        Me.INDLyBtnCode.Size = New System.Drawing.Size(390, 36)
        Me.INDLyBtnCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyBtnCode.Text = "Código"
        Me.INDLyBtnCode.TextSize = New System.Drawing.Size(121, 17)
        '
        'INDLyDteDocumentDate
        '
        Me.INDLyDteDocumentDate.Control = Me.INDDteDocumentDate
        Me.INDLyDteDocumentDate.CustomizationFormText = "Fecha"
        Me.INDLyDteDocumentDate.Location = New System.Drawing.Point(0, 36)
        Me.INDLyDteDocumentDate.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDLyDteDocumentDate.MinSize = New System.Drawing.Size(390, 36)
        Me.INDLyDteDocumentDate.Name = "INDLyDteDocumentDate"
        Me.INDLyDteDocumentDate.ShowInCustomizationForm = False
        Me.INDLyDteDocumentDate.Size = New System.Drawing.Size(390, 36)
        Me.INDLyDteDocumentDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyDteDocumentDate.Text = "Fecha"
        Me.INDLyDteDocumentDate.TextSize = New System.Drawing.Size(121, 17)
        '
        'INDLySleWarehouse
        '
        Me.INDLySleWarehouse.Control = Me.INDSleWarehouse
        Me.INDLySleWarehouse.CustomizationFormText = "Almacén"
        Me.INDLySleWarehouse.Location = New System.Drawing.Point(0, 72)
        Me.INDLySleWarehouse.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDLySleWarehouse.MinSize = New System.Drawing.Size(390, 36)
        Me.INDLySleWarehouse.Name = "INDLySleWarehouse"
        Me.INDLySleWarehouse.ShowInCustomizationForm = False
        Me.INDLySleWarehouse.Size = New System.Drawing.Size(390, 36)
        Me.INDLySleWarehouse.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLySleWarehouse.Text = "Almacén"
        Me.INDLySleWarehouse.TextSize = New System.Drawing.Size(121, 17)
        '
        'INDLyTxtDescription
        '
        Me.INDLyTxtDescription.Control = Me.INDTxtDescription
        Me.INDLyTxtDescription.CustomizationFormText = "Detalle"
        Me.INDLyTxtDescription.Location = New System.Drawing.Point(0, 108)
        Me.INDLyTxtDescription.MaxSize = New System.Drawing.Size(390, 72)
        Me.INDLyTxtDescription.MinSize = New System.Drawing.Size(390, 72)
        Me.INDLyTxtDescription.Name = "INDLyTxtDescription"
        Me.INDLyTxtDescription.Size = New System.Drawing.Size(390, 372)
        Me.INDLyTxtDescription.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyTxtDescription.Text = "Detalle"
        Me.INDLyTxtDescription.TextSize = New System.Drawing.Size(121, 17)
        '
        'LyGroupEntranceVoucherInformation
        '
        Me.LyGroupEntranceVoucherInformation.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LyGroupEntranceVoucherInformation.AppearanceGroup.Options.UseFont = True
        Me.LyGroupEntranceVoucherInformation.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LyGroupEntranceVoucherInformation.AppearanceItemCaption.Options.UseFont = True
        Me.LyGroupEntranceVoucherInformation.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LyGroupEntranceVoucherInformation.AppearanceTabPage.Header.Options.UseFont = True
        Me.LyGroupEntranceVoucherInformation.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LyGroupEntranceVoucherInformation.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LyGroupEntranceVoucherInformation.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LyGroupEntranceVoucherInformation.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LyGroupEntranceVoucherInformation.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LyGroupEntranceVoucherInformation.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LyGroupEntranceVoucherInformation.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LyGroupEntranceVoucherInformation.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LyGroupEntranceVoucherInformation, False)
        Me.LyGroupEntranceVoucherInformation.CustomizationFormText = "Información Comprobante de Entrada"
        Me.LyGroupEntranceVoucherInformation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLySleVoucherCode, Me.INDLyTxtSupplier, Me.INDLyTxtWarehouse, Me.INDLyTxtInvoiceNumber, Me.INDLyTxtDayPeriod, Me.INDLyDteDateEntranceVoucher, Me.INDLyTxtInvoiceDate})
        Me.LyGroupEntranceVoucherInformation.Location = New System.Drawing.Point(412, 0)
        Me.LyGroupEntranceVoucherInformation.Name = "LyGroupEntranceVoucherInformation"
        Me.LyGroupEntranceVoucherInformation.Size = New System.Drawing.Size(412, 529)
        Me.LyGroupEntranceVoucherInformation.Text = "Información Comprobante de Entrada"
        '
        'INDLySleVoucherCode
        '
        Me.INDLySleVoucherCode.Control = Me.INDSleVoucherCode
        Me.INDLySleVoucherCode.CustomizationFormText = "No. Comprobante"
        Me.INDLySleVoucherCode.Location = New System.Drawing.Point(0, 0)
        Me.INDLySleVoucherCode.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDLySleVoucherCode.MinSize = New System.Drawing.Size(390, 36)
        Me.INDLySleVoucherCode.Name = "INDLySleVoucherCode"
        Me.INDLySleVoucherCode.ShowInCustomizationForm = False
        Me.INDLySleVoucherCode.Size = New System.Drawing.Size(390, 36)
        Me.INDLySleVoucherCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLySleVoucherCode.Text = "No. Comprobante"
        Me.INDLySleVoucherCode.TextSize = New System.Drawing.Size(121, 17)
        '
        'INDLyTxtSupplier
        '
        Me.INDLyTxtSupplier.Control = Me.INDTxtSupplier
        Me.INDLyTxtSupplier.CustomizationFormText = "Proveedor"
        Me.INDLyTxtSupplier.Location = New System.Drawing.Point(0, 72)
        Me.INDLyTxtSupplier.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDLyTxtSupplier.MinSize = New System.Drawing.Size(390, 36)
        Me.INDLyTxtSupplier.Name = "INDLyTxtSupplier"
        Me.INDLyTxtSupplier.Size = New System.Drawing.Size(390, 36)
        Me.INDLyTxtSupplier.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyTxtSupplier.Text = "Proveedor"
        Me.INDLyTxtSupplier.TextSize = New System.Drawing.Size(121, 17)
        '
        'INDLyTxtWarehouse
        '
        Me.INDLyTxtWarehouse.Control = Me.INDTxtWarehouse
        Me.INDLyTxtWarehouse.CustomizationFormText = "Almacén"
        Me.INDLyTxtWarehouse.Location = New System.Drawing.Point(0, 108)
        Me.INDLyTxtWarehouse.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDLyTxtWarehouse.MinSize = New System.Drawing.Size(390, 36)
        Me.INDLyTxtWarehouse.Name = "INDLyTxtWarehouse"
        Me.INDLyTxtWarehouse.Size = New System.Drawing.Size(390, 36)
        Me.INDLyTxtWarehouse.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyTxtWarehouse.Text = "Almacén"
        Me.INDLyTxtWarehouse.TextSize = New System.Drawing.Size(121, 17)
        '
        'INDLyTxtInvoiceNumber
        '
        Me.INDLyTxtInvoiceNumber.Control = Me.INDTxtInvoiceNumber
        Me.INDLyTxtInvoiceNumber.CustomizationFormText = "No. Factura"
        Me.INDLyTxtInvoiceNumber.Location = New System.Drawing.Point(0, 144)
        Me.INDLyTxtInvoiceNumber.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDLyTxtInvoiceNumber.MinSize = New System.Drawing.Size(390, 36)
        Me.INDLyTxtInvoiceNumber.Name = "INDLyTxtInvoiceNumber"
        Me.INDLyTxtInvoiceNumber.Size = New System.Drawing.Size(390, 36)
        Me.INDLyTxtInvoiceNumber.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyTxtInvoiceNumber.Text = "No. Factura"
        Me.INDLyTxtInvoiceNumber.TextSize = New System.Drawing.Size(121, 17)
        '
        'INDLyTxtDayPeriod
        '
        Me.INDLyTxtDayPeriod.Control = Me.INDTxtDayPeriod
        Me.INDLyTxtDayPeriod.CustomizationFormText = "Plazo"
        Me.INDLyTxtDayPeriod.Location = New System.Drawing.Point(0, 216)
        Me.INDLyTxtDayPeriod.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDLyTxtDayPeriod.MinSize = New System.Drawing.Size(390, 36)
        Me.INDLyTxtDayPeriod.Name = "INDLyTxtDayPeriod"
        Me.INDLyTxtDayPeriod.Size = New System.Drawing.Size(390, 264)
        Me.INDLyTxtDayPeriod.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyTxtDayPeriod.Text = "Plazo"
        Me.INDLyTxtDayPeriod.TextSize = New System.Drawing.Size(121, 17)
        '
        'INDLyDteDateEntranceVoucher
        '
        Me.INDLyDteDateEntranceVoucher.Control = Me.INDDteDateEntranceVoucher
        Me.INDLyDteDateEntranceVoucher.CustomizationFormText = "Fecha"
        Me.INDLyDteDateEntranceVoucher.Location = New System.Drawing.Point(0, 36)
        Me.INDLyDteDateEntranceVoucher.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDLyDteDateEntranceVoucher.MinSize = New System.Drawing.Size(390, 36)
        Me.INDLyDteDateEntranceVoucher.Name = "INDLyDteDateEntranceVoucher"
        Me.INDLyDteDateEntranceVoucher.Size = New System.Drawing.Size(390, 36)
        Me.INDLyDteDateEntranceVoucher.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyDteDateEntranceVoucher.Text = "Fecha"
        Me.INDLyDteDateEntranceVoucher.TextSize = New System.Drawing.Size(121, 17)
        '
        'INDLyTxtInvoiceDate
        '
        Me.INDLyTxtInvoiceDate.Control = Me.INDTxtInvoiceDate
        Me.INDLyTxtInvoiceDate.CustomizationFormText = "Fecha Factura"
        Me.INDLyTxtInvoiceDate.Location = New System.Drawing.Point(0, 180)
        Me.INDLyTxtInvoiceDate.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDLyTxtInvoiceDate.MinSize = New System.Drawing.Size(390, 36)
        Me.INDLyTxtInvoiceDate.Name = "INDLyTxtInvoiceDate"
        Me.INDLyTxtInvoiceDate.Size = New System.Drawing.Size(390, 36)
        Me.INDLyTxtInvoiceDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyTxtInvoiceDate.Text = "Fecha Factura"
        Me.INDLyTxtInvoiceDate.TextSize = New System.Drawing.Size(121, 17)
        '
        'LyGroupFreight
        '
        Me.LyGroupFreight.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LyGroupFreight.AppearanceGroup.Options.UseFont = True
        Me.LyGroupFreight.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LyGroupFreight.AppearanceItemCaption.Options.UseFont = True
        Me.LyGroupFreight.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LyGroupFreight.AppearanceTabPage.Header.Options.UseFont = True
        Me.LyGroupFreight.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LyGroupFreight.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LyGroupFreight.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LyGroupFreight.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LyGroupFreight.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LyGroupFreight.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LyGroupFreight.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LyGroupFreight.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LyGroupFreight, False)
        Me.LyGroupFreight.CustomizationFormText = "Fletes"
        Me.LyGroupFreight.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyTxtFreightInvoice, Me.INDLySpnFreightIvaPercentage, Me.INDLyTxtFreightValue, Me.INDLyTxtFreightIVAValue})
        Me.LyGroupFreight.Location = New System.Drawing.Point(824, 0)
        Me.LyGroupFreight.Name = "LyGroupFreight"
        Me.LyGroupFreight.Size = New System.Drawing.Size(412, 529)
        Me.LyGroupFreight.Text = "Fletes"
        '
        'INDLyTxtFreightInvoice
        '
        Me.INDLyTxtFreightInvoice.Control = Me.INDTxtFreightInvoice
        Me.INDLyTxtFreightInvoice.CustomizationFormText = "Fletes Factura"
        Me.INDLyTxtFreightInvoice.Location = New System.Drawing.Point(0, 36)
        Me.INDLyTxtFreightInvoice.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDLyTxtFreightInvoice.MinSize = New System.Drawing.Size(390, 36)
        Me.INDLyTxtFreightInvoice.Name = "INDLyTxtFreightInvoice"
        Me.INDLyTxtFreightInvoice.Size = New System.Drawing.Size(390, 36)
        Me.INDLyTxtFreightInvoice.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyTxtFreightInvoice.Text = "Fletes Factura"
        Me.INDLyTxtFreightInvoice.TextSize = New System.Drawing.Size(121, 17)
        '
        'INDLySpnFreightIvaPercentage
        '
        Me.INDLySpnFreightIvaPercentage.Control = Me.INDSpnFreightIvaPercentage
        Me.INDLySpnFreightIvaPercentage.CustomizationFormText = "% IVA Fletes"
        Me.INDLySpnFreightIvaPercentage.Location = New System.Drawing.Point(0, 72)
        Me.INDLySpnFreightIvaPercentage.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDLySpnFreightIvaPercentage.MinSize = New System.Drawing.Size(390, 36)
        Me.INDLySpnFreightIvaPercentage.Name = "INDLySpnFreightIvaPercentage"
        Me.INDLySpnFreightIvaPercentage.Size = New System.Drawing.Size(390, 36)
        Me.INDLySpnFreightIvaPercentage.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLySpnFreightIvaPercentage.Text = "% IVA Fletes"
        Me.INDLySpnFreightIvaPercentage.TextSize = New System.Drawing.Size(121, 17)
        '
        'INDLyTxtFreightValue
        '
        Me.INDLyTxtFreightValue.Control = Me.INDTxtFreightValue
        Me.INDLyTxtFreightValue.CustomizationFormText = "Valor Flete"
        Me.INDLyTxtFreightValue.Location = New System.Drawing.Point(0, 0)
        Me.INDLyTxtFreightValue.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDLyTxtFreightValue.MinSize = New System.Drawing.Size(390, 36)
        Me.INDLyTxtFreightValue.Name = "INDLyTxtFreightValue"
        Me.INDLyTxtFreightValue.Size = New System.Drawing.Size(390, 36)
        Me.INDLyTxtFreightValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyTxtFreightValue.Text = "Valor Flete"
        Me.INDLyTxtFreightValue.TextSize = New System.Drawing.Size(121, 17)
        '
        'INDLyTxtFreightIVAValue
        '
        Me.INDLyTxtFreightIVAValue.Control = Me.INDTxtFregithIVAValue
        Me.INDLyTxtFreightIVAValue.CustomizationFormText = "IVA Fletes"
        Me.INDLyTxtFreightIVAValue.Location = New System.Drawing.Point(0, 108)
        Me.INDLyTxtFreightIVAValue.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDLyTxtFreightIVAValue.MinSize = New System.Drawing.Size(390, 36)
        Me.INDLyTxtFreightIVAValue.Name = "INDLyTxtFreightIVAValue"
        Me.INDLyTxtFreightIVAValue.Size = New System.Drawing.Size(390, 372)
        Me.INDLyTxtFreightIVAValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyTxtFreightIVAValue.Text = "IVA Fletes"
        Me.INDLyTxtFreightIVAValue.TextSize = New System.Drawing.Size(121, 17)
        '
        'LyGroupProducts
        '
        Me.LyGroupProducts.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LyGroupProducts.AppearanceGroup.Options.UseFont = True
        Me.LyGroupProducts.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LyGroupProducts.AppearanceItemCaption.Options.UseFont = True
        Me.LyGroupProducts.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LyGroupProducts.AppearanceTabPage.Header.Options.UseFont = True
        Me.LyGroupProducts.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LyGroupProducts.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LyGroupProducts.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LyGroupProducts.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LyGroupProducts.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LyGroupProducts.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LyGroupProducts.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LyGroupProducts.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LyGroupProducts, False)
        Me.LyGroupProducts.CustomizationFormText = "Listado de Productos"
        Me.LyGroupProducts.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyGcProducts})
        Me.LyGroupProducts.Location = New System.Drawing.Point(1236, 0)
        Me.LyGroupProducts.Name = "LyGroupProducts"
        Me.LyGroupProducts.Size = New System.Drawing.Size(850, 529)
        Me.LyGroupProducts.Text = "Listado de Productos"
        Me.LyGroupProducts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLyGcProducts
        '
        Me.INDLyGcProducts.Control = Me.INDGcProducts
        Me.INDLyGcProducts.CustomizationFormText = "Productos"
        Me.INDLyGcProducts.Location = New System.Drawing.Point(0, 0)
        Me.INDLyGcProducts.MaxSize = New System.Drawing.Size(828, 0)
        Me.INDLyGcProducts.MinSize = New System.Drawing.Size(828, 1)
        Me.INDLyGcProducts.Name = "INDLyGcProducts"
        Me.INDLyGcProducts.Size = New System.Drawing.Size(828, 480)
        Me.INDLyGcProducts.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyGcProducts.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLyGcProducts.TextVisible = False
        '
        'INDlygBudget
        '
        Me.INDlygBudget.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygBudget.AppearanceGroup.Options.UseFont = True
        Me.INDlygBudget.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygBudget.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygBudget.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygBudget.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygBudget.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygBudget.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygBudget.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygBudget.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygBudget.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygBudget.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygBudget.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygBudget.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygBudget, False)
        Me.INDlygBudget.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemGridObligation})
        Me.INDlygBudget.Location = New System.Drawing.Point(2086, 0)
        Me.INDlygBudget.Name = "INDlygBudget"
        Me.INDlygBudget.Size = New System.Drawing.Size(850, 529)
        Me.INDlygBudget.Text = "Interfaz Presupuesto"
        Me.INDlygBudget.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlyItemGridObligation
        '
        Me.INDlyItemGridObligation.Control = Me.INDgcObligation
        Me.INDlyItemGridObligation.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemGridObligation.MaxSize = New System.Drawing.Size(828, 0)
        Me.INDlyItemGridObligation.MinSize = New System.Drawing.Size(828, 24)
        Me.INDlyItemGridObligation.Name = "INDlyItemGridObligation"
        Me.INDlyItemGridObligation.Size = New System.Drawing.Size(828, 480)
        Me.INDlyItemGridObligation.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemGridObligation.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemGridObligation.TextVisible = False
        '
        'LyGroupOtherRetention
        '
        Me.LyGroupOtherRetention.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LyGroupOtherRetention.AppearanceGroup.Options.UseFont = True
        Me.LyGroupOtherRetention.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LyGroupOtherRetention.AppearanceItemCaption.Options.UseFont = True
        Me.LyGroupOtherRetention.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LyGroupOtherRetention.AppearanceTabPage.Header.Options.UseFont = True
        Me.LyGroupOtherRetention.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LyGroupOtherRetention.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LyGroupOtherRetention.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LyGroupOtherRetention.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LyGroupOtherRetention.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LyGroupOtherRetention.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LyGroupOtherRetention.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LyGroupOtherRetention.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LyGroupOtherRetention, False)
        Me.LyGroupOtherRetention.CustomizationFormText = "Otras Retenciones"
        Me.LyGroupOtherRetention.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyGcOtherWithholding})
        Me.LyGroupOtherRetention.Location = New System.Drawing.Point(2936, 0)
        Me.LyGroupOtherRetention.Name = "LyGroupOtherRetention"
        Me.LyGroupOtherRetention.Size = New System.Drawing.Size(850, 529)
        Me.LyGroupOtherRetention.Text = "Otras Retenciones"
        Me.LyGroupOtherRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLyGcOtherWithholding
        '
        Me.INDLyGcOtherWithholding.Control = Me.INDGcOtherWithholding
        Me.INDLyGcOtherWithholding.CustomizationFormText = "Retenciones"
        Me.INDLyGcOtherWithholding.Location = New System.Drawing.Point(0, 0)
        Me.INDLyGcOtherWithholding.MaxSize = New System.Drawing.Size(828, 0)
        Me.INDLyGcOtherWithholding.MinSize = New System.Drawing.Size(828, 1)
        Me.INDLyGcOtherWithholding.Name = "INDLyGcOtherWithholding"
        Me.INDLyGcOtherWithholding.Size = New System.Drawing.Size(828, 480)
        Me.INDLyGcOtherWithholding.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyGcOtherWithholding.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLyGcOtherWithholding.TextVisible = False
        '
        'LyGroupOtherDeduction
        '
        Me.LyGroupOtherDeduction.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LyGroupOtherDeduction.AppearanceGroup.Options.UseFont = True
        Me.LyGroupOtherDeduction.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LyGroupOtherDeduction.AppearanceItemCaption.Options.UseFont = True
        Me.LyGroupOtherDeduction.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LyGroupOtherDeduction.AppearanceTabPage.Header.Options.UseFont = True
        Me.LyGroupOtherDeduction.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LyGroupOtherDeduction.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LyGroupOtherDeduction.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LyGroupOtherDeduction.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LyGroupOtherDeduction.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LyGroupOtherDeduction.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LyGroupOtherDeduction.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LyGroupOtherDeduction.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LyGroupOtherDeduction, False)
        Me.LyGroupOtherDeduction.CustomizationFormText = "Otras Deducciones"
        Me.LyGroupOtherDeduction.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyGcOtherDeduction})
        Me.LyGroupOtherDeduction.Location = New System.Drawing.Point(3786, 0)
        Me.LyGroupOtherDeduction.Name = "LyGroupOtherDeduction"
        Me.LyGroupOtherDeduction.Size = New System.Drawing.Size(850, 529)
        Me.LyGroupOtherDeduction.Text = "Otras Deducciones"
        Me.LyGroupOtherDeduction.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLyGcOtherDeduction
        '
        Me.INDLyGcOtherDeduction.Control = Me.INDGcOtherDeduction
        Me.INDLyGcOtherDeduction.CustomizationFormText = "Deducciones"
        Me.INDLyGcOtherDeduction.Location = New System.Drawing.Point(0, 0)
        Me.INDLyGcOtherDeduction.MaxSize = New System.Drawing.Size(828, 0)
        Me.INDLyGcOtherDeduction.MinSize = New System.Drawing.Size(828, 1)
        Me.INDLyGcOtherDeduction.Name = "INDLyGcOtherDeduction"
        Me.INDLyGcOtherDeduction.Size = New System.Drawing.Size(828, 480)
        Me.INDLyGcOtherDeduction.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyGcOtherDeduction.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLyGcOtherDeduction.TextVisible = False
        '
        'LyGroupLiquitationInvoice
        '
        Me.LyGroupLiquitationInvoice.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LyGroupLiquitationInvoice.AppearanceGroup.Options.UseFont = True
        Me.LyGroupLiquitationInvoice.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LyGroupLiquitationInvoice.AppearanceItemCaption.Options.UseFont = True
        Me.LyGroupLiquitationInvoice.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LyGroupLiquitationInvoice.AppearanceTabPage.Header.Options.UseFont = True
        Me.LyGroupLiquitationInvoice.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LyGroupLiquitationInvoice.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LyGroupLiquitationInvoice.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LyGroupLiquitationInvoice.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LyGroupLiquitationInvoice.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LyGroupLiquitationInvoice.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LyGroupLiquitationInvoice.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LyGroupLiquitationInvoice.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LyGroupLiquitationInvoice, False)
        Me.LyGroupLiquitationInvoice.CustomizationFormText = "Liquidación"
        Me.LyGroupLiquitationInvoice.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyTxtTotalValue, Me.INDLyTxtOtherDeduction, Me.INDLyTxtOtherRetention, Me.INDLyTxtWhithholdigSourceValue, Me.INDLyTxtWhithholdigICAValue, Me.INDLyTxtWhithholdigTaxValue, Me.INDLyTxtValueTax, Me.INDLyTxtDiscountValue, Me.INDLyTxtSubTotal})
        Me.LyGroupLiquitationInvoice.Location = New System.Drawing.Point(4636, 0)
        Me.LyGroupLiquitationInvoice.Name = "LyGroupLiquitationInvoice"
        Me.LyGroupLiquitationInvoice.Size = New System.Drawing.Size(412, 529)
        Me.LyGroupLiquitationInvoice.Text = "Liquidación"
        '
        'INDLyTxtTotalValue
        '
        Me.INDLyTxtTotalValue.Control = Me.INDTxtTotalValue
        Me.INDLyTxtTotalValue.CustomizationFormText = "Total"
        Me.INDLyTxtTotalValue.Location = New System.Drawing.Point(0, 288)
        Me.INDLyTxtTotalValue.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDLyTxtTotalValue.MinSize = New System.Drawing.Size(390, 36)
        Me.INDLyTxtTotalValue.Name = "INDLyTxtTotalValue"
        Me.INDLyTxtTotalValue.Size = New System.Drawing.Size(390, 192)
        Me.INDLyTxtTotalValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyTxtTotalValue.Text = "Total"
        Me.INDLyTxtTotalValue.TextSize = New System.Drawing.Size(121, 17)
        '
        'INDLyTxtOtherDeduction
        '
        Me.INDLyTxtOtherDeduction.Control = Me.INDTxtOtherDeduction
        Me.INDLyTxtOtherDeduction.CustomizationFormText = "Otras Deducciones"
        Me.INDLyTxtOtherDeduction.Location = New System.Drawing.Point(0, 252)
        Me.INDLyTxtOtherDeduction.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDLyTxtOtherDeduction.MinSize = New System.Drawing.Size(390, 36)
        Me.INDLyTxtOtherDeduction.Name = "INDLyTxtOtherDeduction"
        Me.INDLyTxtOtherDeduction.Size = New System.Drawing.Size(390, 36)
        Me.INDLyTxtOtherDeduction.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyTxtOtherDeduction.Text = "Otras Deducciones"
        Me.INDLyTxtOtherDeduction.TextSize = New System.Drawing.Size(121, 17)
        '
        'INDLyTxtOtherRetention
        '
        Me.INDLyTxtOtherRetention.Control = Me.INDTxtOtherRetention
        Me.INDLyTxtOtherRetention.CustomizationFormText = "Otras Retenciones"
        Me.INDLyTxtOtherRetention.Location = New System.Drawing.Point(0, 216)
        Me.INDLyTxtOtherRetention.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDLyTxtOtherRetention.MinSize = New System.Drawing.Size(390, 36)
        Me.INDLyTxtOtherRetention.Name = "INDLyTxtOtherRetention"
        Me.INDLyTxtOtherRetention.Size = New System.Drawing.Size(390, 36)
        Me.INDLyTxtOtherRetention.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyTxtOtherRetention.Text = "Otras Retenciones"
        Me.INDLyTxtOtherRetention.TextSize = New System.Drawing.Size(121, 17)
        '
        'INDLyTxtWhithholdigSourceValue
        '
        Me.INDLyTxtWhithholdigSourceValue.Control = Me.INDTxtWhithholdigSourceValue
        Me.INDLyTxtWhithholdigSourceValue.CustomizationFormText = "Retención Fuente"
        Me.INDLyTxtWhithholdigSourceValue.Location = New System.Drawing.Point(0, 180)
        Me.INDLyTxtWhithholdigSourceValue.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDLyTxtWhithholdigSourceValue.MinSize = New System.Drawing.Size(390, 36)
        Me.INDLyTxtWhithholdigSourceValue.Name = "INDLyTxtWhithholdigSourceValue"
        Me.INDLyTxtWhithholdigSourceValue.Size = New System.Drawing.Size(390, 36)
        Me.INDLyTxtWhithholdigSourceValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyTxtWhithholdigSourceValue.Text = "Retención Fuente"
        Me.INDLyTxtWhithholdigSourceValue.TextSize = New System.Drawing.Size(121, 17)
        '
        'INDLyTxtWhithholdigICAValue
        '
        Me.INDLyTxtWhithholdigICAValue.Control = Me.INDTxtWhithholdigICAValue
        Me.INDLyTxtWhithholdigICAValue.CustomizationFormText = "Retención ICA"
        Me.INDLyTxtWhithholdigICAValue.Location = New System.Drawing.Point(0, 144)
        Me.INDLyTxtWhithholdigICAValue.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDLyTxtWhithholdigICAValue.MinSize = New System.Drawing.Size(390, 36)
        Me.INDLyTxtWhithholdigICAValue.Name = "INDLyTxtWhithholdigICAValue"
        Me.INDLyTxtWhithholdigICAValue.Size = New System.Drawing.Size(390, 36)
        Me.INDLyTxtWhithholdigICAValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyTxtWhithholdigICAValue.Text = "Retención ICA"
        Me.INDLyTxtWhithholdigICAValue.TextSize = New System.Drawing.Size(121, 17)
        '
        'INDLyTxtWhithholdigTaxValue
        '
        Me.INDLyTxtWhithholdigTaxValue.Control = Me.INDTxtWhithholdigTaxValue
        Me.INDLyTxtWhithholdigTaxValue.CustomizationFormText = "Retención IVA"
        Me.INDLyTxtWhithholdigTaxValue.Location = New System.Drawing.Point(0, 108)
        Me.INDLyTxtWhithholdigTaxValue.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDLyTxtWhithholdigTaxValue.MinSize = New System.Drawing.Size(390, 36)
        Me.INDLyTxtWhithholdigTaxValue.Name = "INDLyTxtWhithholdigTaxValue"
        Me.INDLyTxtWhithholdigTaxValue.Size = New System.Drawing.Size(390, 36)
        Me.INDLyTxtWhithholdigTaxValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyTxtWhithholdigTaxValue.Text = "Retención IVA"
        Me.INDLyTxtWhithholdigTaxValue.TextSize = New System.Drawing.Size(121, 17)
        '
        'INDLyTxtValueTax
        '
        Me.INDLyTxtValueTax.Control = Me.INDTxtValueTax
        Me.INDLyTxtValueTax.CustomizationFormText = "IVA"
        Me.INDLyTxtValueTax.Location = New System.Drawing.Point(0, 72)
        Me.INDLyTxtValueTax.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDLyTxtValueTax.MinSize = New System.Drawing.Size(390, 36)
        Me.INDLyTxtValueTax.Name = "INDLyTxtValueTax"
        Me.INDLyTxtValueTax.Size = New System.Drawing.Size(390, 36)
        Me.INDLyTxtValueTax.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyTxtValueTax.Text = "IVA"
        Me.INDLyTxtValueTax.TextSize = New System.Drawing.Size(121, 17)
        '
        'INDLyTxtDiscountValue
        '
        Me.INDLyTxtDiscountValue.Control = Me.INDTxtDiscountValue
        Me.INDLyTxtDiscountValue.CustomizationFormText = "Descuento"
        Me.INDLyTxtDiscountValue.Location = New System.Drawing.Point(0, 36)
        Me.INDLyTxtDiscountValue.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDLyTxtDiscountValue.MinSize = New System.Drawing.Size(390, 36)
        Me.INDLyTxtDiscountValue.Name = "INDLyTxtDiscountValue"
        Me.INDLyTxtDiscountValue.Size = New System.Drawing.Size(390, 36)
        Me.INDLyTxtDiscountValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyTxtDiscountValue.Text = "Descuento"
        Me.INDLyTxtDiscountValue.TextSize = New System.Drawing.Size(121, 17)
        '
        'INDLyTxtSubTotal
        '
        Me.INDLyTxtSubTotal.Control = Me.INDTxtSubTotal
        Me.INDLyTxtSubTotal.CustomizationFormText = "Valor Bruto"
        Me.INDLyTxtSubTotal.Location = New System.Drawing.Point(0, 0)
        Me.INDLyTxtSubTotal.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDLyTxtSubTotal.MinSize = New System.Drawing.Size(390, 36)
        Me.INDLyTxtSubTotal.Name = "INDLyTxtSubTotal"
        Me.INDLyTxtSubTotal.Size = New System.Drawing.Size(390, 36)
        Me.INDLyTxtSubTotal.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyTxtSubTotal.Text = "Valor Bruto"
        Me.INDLyTxtSubTotal.TextSize = New System.Drawing.Size(121, 17)
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'FrmRefundPurchase
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1661, 708)
        Me.IconOptions.ShowIcon = False
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "FrmRefundPurchase"
        Me.Opacity = 1.0R
        Me.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.Tag = "1403"
        Me.Text = "Devolución de Compra"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LyRefundPurchase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LyRefundPurchase.ResumeLayout(False)
        CType(Me.INDgcObligation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewObligation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepTxtValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtFregithIVAValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtTotalValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtOtherDeduction.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtOtherRetention.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtWhithholdigSourceValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtWhithholdigICAValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtWhithholdigTaxValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtValueTax.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtDiscountValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtSubTotal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcProducts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvProducts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRptSleDevolutionCause, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRptGvDevolutionCause, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepoSpeCantDevolutionProduct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcOtherDeduction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvOtherDeduction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepoSpeDeductionValueDevolution, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcOtherWithholding, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvOtherWithholding, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemSpinEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSpnFreightIvaPercentage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtDescription.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtFreightInvoice.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtFreightValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtDayPeriod.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtInvoiceNumber.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDteDateEntranceVoucher.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDteDateEntranceVoucher.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtWarehouse.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtSupplier.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleVoucherCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSlvEntranceVoucher, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleWarehouse.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGdvWarehouse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDteDocumentDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDteDocumentDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDBtnCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtInvoiceDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtInvoiceDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LyGpRefundPurchase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LyGroupMainData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyBtnCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyDteDocumentDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLySleWarehouse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyTxtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LyGroupEntranceVoucherInformation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLySleVoucherCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyTxtSupplier, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyTxtWarehouse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyTxtInvoiceNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyTxtDayPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyDteDateEntranceVoucher, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyTxtInvoiceDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LyGroupFreight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyTxtFreightInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLySpnFreightIvaPercentage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyTxtFreightValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyTxtFreightIVAValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LyGroupProducts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyGcProducts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygBudget, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemGridObligation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LyGroupOtherRetention, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyGcOtherWithholding, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LyGroupOtherDeduction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyGcOtherDeduction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LyGroupLiquitationInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyTxtTotalValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyTxtOtherDeduction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyTxtOtherRetention, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyTxtWhithholdigSourceValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyTxtWhithholdigICAValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyTxtWhithholdigTaxValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyTxtValueTax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyTxtDiscountValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyTxtSubTotal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LyRefundPurchase As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LyGpRefundPurchase As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents INDTxtDescription As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDTxtFreightInvoice As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtFreightValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtDayPeriod As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtInvoiceNumber As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDDteDateEntranceVoucher As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDTxtWarehouse As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtSupplier As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDSleVoucherCode As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDSlvEntranceVoucher As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDSleWarehouse As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGdvWarehouse As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDDteDocumentDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDLyBtnCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyDteDocumentDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLySleWarehouse As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLySleVoucherCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyTxtSupplier As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyTxtWarehouse As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyDteDateEntranceVoucher As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyTxtInvoiceNumber As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyTxtDayPeriod As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyTxtInvoiceDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyTxtFreightValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyTxtFreightInvoice As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyTxtDescription As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LyGroupMainData As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LyGroupEntranceVoucherInformation As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LyGroupFreight As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDBtnCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDSpnFreightIvaPercentage As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDLySpnFreightIvaPercentage As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGcOtherDeduction As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvOtherDeduction As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDGcOtherWithholding As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvOtherWithholding As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLyGcOtherWithholding As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyGcOtherDeduction As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LyGroupOtherRetention As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LyGroupOtherDeduction As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDTxtInvoiceDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDGcProducts As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvProducts As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LyGroupProducts As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLyGcProducts As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents colProductsCodeName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colProductsOutstandingQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colProductsDevolutionQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepoSpeCantDevolutionProduct As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents ColConceptName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColRetentionPercentage As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColRetentionBaseValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColWhitholdingValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents ColDeductionConcept As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColBaseValueDeduction As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColDeductionValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents INDTxtTotalValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtOtherDeduction As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtOtherRetention As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtWhithholdigSourceValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtWhithholdigICAValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtWhithholdigTaxValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtValueTax As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtDiscountValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtSubTotal As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LyGroupLiquitationInvoice As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLyTxtTotalValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyTxtOtherDeduction As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyTxtOtherRetention As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyTxtWhithholdigSourceValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyTxtWhithholdigICAValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyTxtWhithholdigTaxValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyTxtValueTax As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyTxtDiscountValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyTxtSubTotal As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents colSleEntranceVoucherCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colSleEntranceVoucherSupplier As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colEntranceVoucherWarehouse As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colSleWareHouseCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colSleWareHouseName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colSleEntranceVoucherDateDocumento As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepoSpeDeductionValueDevolution As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents INDTxtFregithIVAValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLyTxtFreightIVAValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents RepositoryItemSpinEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents RepositoryItemTextEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents colProductsBatch As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents INDgcObligation As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDviewObligation As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepTxtValue As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents INDlygBudget As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemGridObligation As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRptSleDevolutionCause As DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit
    Friend WithEvents INDRptGvDevolutionCause As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
End Class
