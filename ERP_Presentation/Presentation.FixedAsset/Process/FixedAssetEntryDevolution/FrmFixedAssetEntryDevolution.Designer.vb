Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmFixedAssetEntryDevolution
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
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.RepositoryItemPopupContainerEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlyEntryDevolution = New DevExpress.XtraLayout.LayoutControl()
        Me.PopUpSummarySettlement = New DevExpress.XtraEditors.PopupContainerControl()
        Me.LySummarySettlement = New DevExpress.XtraLayout.LayoutControl()
        Me.INDPopTxtDistricTaxes = New DevExpress.XtraEditors.TextEdit()
        Me.INDPopTxtDeductionOther = New DevExpress.XtraEditors.TextEdit()
        Me.INDPopTxtRetentionOther = New DevExpress.XtraEditors.TextEdit()
        Me.INDPopTxtRetentionSource = New DevExpress.XtraEditors.TextEdit()
        Me.INDPopSpnFreightIVA = New DevExpress.XtraEditors.TextEdit()
        Me.INDPopTxtWithholdingICA = New DevExpress.XtraEditors.TextEdit()
        Me.INDPopTxtWithholdingTax = New DevExpress.XtraEditors.TextEdit()
        Me.INDPopTxtFreightValue = New DevExpress.XtraEditors.TextEdit()
        Me.INDPopTxtValueTax = New DevExpress.XtraEditors.TextEdit()
        Me.INDPopTxtTotalCxp = New DevExpress.XtraEditors.TextEdit()
        Me.INDPopTxtDiscountValue = New DevExpress.XtraEditors.TextEdit()
        Me.INDPopTxtValue = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyPopTxtValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyPopTxtValueTax = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyPopTxtFreightValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyPopSpnFreightIVA = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.INDLyPopTxtTotalCxp = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyPopTxtDistricTaxes = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyPopTxtDeductionOther = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyPopTxtRetentionOther = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyPopTxtWithholdingTax = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyPopTxtDiscountValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyPopTxtWithholdingICA = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyPopTxtRetentionSource = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDtxtFreightIVAValue = New DevExpress.XtraEditors.TextEdit()
        Me.INDseFreightIVAPercentage = New DevExpress.XtraEditors.SpinEdit()
        Me.INDtxtFreightValueEntry = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtFreightValue = New DevExpress.XtraEditors.TextEdit()
        Me.INDgcItem = New DevExpress.XtraGrid.GridControl()
        Me.INDviewItem = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGcProduct = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepCheckSelectOption = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.INDdteBillDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDtxtBillNumber = New DevExpress.XtraEditors.TextEdit()
        Me.INDsleFixedAssetEntry = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDviewEntry = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDmemoDescription = New DevExpress.XtraEditors.MemoEdit()
        Me.INDdteDocumentDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDbtnCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDtxtSupplier = New DevExpress.XtraEditors.TextEdit()
        Me.INDseTerm = New DevExpress.XtraEditors.SpinEdit()
        Me.INDgcObligation = New DevExpress.XtraGrid.GridControl()
        Me.INDviewObligation = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn21 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn31 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn41 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn51 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn61 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn71 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepTxtValue = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlygPrincipalInformation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemDocumentDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemDescription = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygEntryData = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemFixedAssetEntry = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemBillNumber = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemBillDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemSupplier = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemTerm = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygFreigth = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemFreightValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemFreightValueEntry = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemFreightIVAPercentage = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemFreightIVAValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygItem = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygBudget = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemGridObligation = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoGridControl11 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView11 = New Presentation.Controls.IndigoGridView(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyEntryDevolution, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyEntryDevolution.SuspendLayout()
        CType(Me.PopUpSummarySettlement, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PopUpSummarySettlement.SuspendLayout()
        CType(Me.LySummarySettlement, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LySummarySettlement.SuspendLayout()
        CType(Me.INDPopTxtDistricTaxes.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPopTxtDeductionOther.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPopTxtRetentionOther.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPopTxtRetentionSource.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPopSpnFreightIVA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPopTxtWithholdingICA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPopTxtWithholdingTax.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPopTxtFreightValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPopTxtValueTax.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPopTxtTotalCxp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPopTxtDiscountValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPopTxtValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyPopTxtValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyPopTxtValueTax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyPopTxtFreightValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyPopSpnFreightIVA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyPopTxtTotalCxp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyPopTxtDistricTaxes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyPopTxtDeductionOther, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyPopTxtRetentionOther, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyPopTxtWithholdingTax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyPopTxtDiscountValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyPopTxtWithholdingICA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyPopTxtRetentionSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtFreightIVAValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDseFreightIVAPercentage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtFreightValueEntry.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtFreightValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepCheckSelectOption, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteBillDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteBillDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtBillNumber.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleFixedAssetEntry.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewEntry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDmemoDescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteDocumentDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteDocumentDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtSupplier.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDseTerm.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcObligation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewObligation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepTxtValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygPrincipalInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDocumentDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygEntryData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemFixedAssetEntry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemBillNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemBillDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemSupplier, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemTerm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygFreigth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemFreightValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemFreightValueEntry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemFreightIVAPercentage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemFreightIVAValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygBudget, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemGridObligation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView11, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyEntryDevolution)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 136)
        Me.INDPanelControlBase.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDPanelControlBase.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1465, 565)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Location = New System.Drawing.Point(0, 6)
        Me.ToolBars.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ToolBars.Size = New System.Drawing.Size(1465, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.BarraBotones.Size = New System.Drawing.Size(1465, 130)
        '
        'RepositoryItemPopupContainerEdit2
        '
        Me.RepositoryItemPopupContainerEdit2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit2.Name = "RepositoryItemPopupContainerEdit2"
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
        Me.CtrNavigationControlPanel1.LayoutControl = Me.INDlyEntryDevolution
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 8)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 555)
        Me.CtrNavigationControlPanel1.TabIndex = 0
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'INDlyEntryDevolution
        '
        Me.INDlyEntryDevolution.Controls.Add(Me.PopUpSummarySettlement)
        Me.INDlyEntryDevolution.Controls.Add(Me.INDtxtFreightIVAValue)
        Me.INDlyEntryDevolution.Controls.Add(Me.INDseFreightIVAPercentage)
        Me.INDlyEntryDevolution.Controls.Add(Me.INDtxtFreightValueEntry)
        Me.INDlyEntryDevolution.Controls.Add(Me.INDtxtFreightValue)
        Me.INDlyEntryDevolution.Controls.Add(Me.INDgcItem)
        Me.INDlyEntryDevolution.Controls.Add(Me.INDdteBillDate)
        Me.INDlyEntryDevolution.Controls.Add(Me.INDtxtBillNumber)
        Me.INDlyEntryDevolution.Controls.Add(Me.INDsleFixedAssetEntry)
        Me.INDlyEntryDevolution.Controls.Add(Me.INDmemoDescription)
        Me.INDlyEntryDevolution.Controls.Add(Me.INDdteDocumentDate)
        Me.INDlyEntryDevolution.Controls.Add(Me.INDbtnCode)
        Me.INDlyEntryDevolution.Controls.Add(Me.INDtxtSupplier)
        Me.INDlyEntryDevolution.Controls.Add(Me.INDseTerm)
        Me.INDlyEntryDevolution.Controls.Add(Me.INDgcObligation)
        Me.INDlyEntryDevolution.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyEntryDevolution.Location = New System.Drawing.Point(202, 8)
        Me.INDlyEntryDevolution.Name = "INDlyEntryDevolution"
        Me.INDlyEntryDevolution.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(659, 501, 250, 350)
        Me.INDlyEntryDevolution.Root = Me.LayoutControlGroup1
        Me.INDlyEntryDevolution.Size = New System.Drawing.Size(1261, 555)
        Me.INDlyEntryDevolution.TabIndex = 1
        Me.INDlyEntryDevolution.Text = "LayoutControl1"
        '
        'PopUpSummarySettlement
        '
        Me.PopUpSummarySettlement.Controls.Add(Me.LySummarySettlement)
        Me.PopUpSummarySettlement.Location = New System.Drawing.Point(1158, 447)
        Me.PopUpSummarySettlement.Name = "PopUpSummarySettlement"
        Me.PopUpSummarySettlement.Size = New System.Drawing.Size(299, 333)
        Me.PopUpSummarySettlement.TabIndex = 28
        '
        'LySummarySettlement
        '
        Me.LySummarySettlement.Controls.Add(Me.INDPopTxtDistricTaxes)
        Me.LySummarySettlement.Controls.Add(Me.INDPopTxtDeductionOther)
        Me.LySummarySettlement.Controls.Add(Me.INDPopTxtRetentionOther)
        Me.LySummarySettlement.Controls.Add(Me.INDPopTxtRetentionSource)
        Me.LySummarySettlement.Controls.Add(Me.INDPopSpnFreightIVA)
        Me.LySummarySettlement.Controls.Add(Me.INDPopTxtWithholdingICA)
        Me.LySummarySettlement.Controls.Add(Me.INDPopTxtWithholdingTax)
        Me.LySummarySettlement.Controls.Add(Me.INDPopTxtFreightValue)
        Me.LySummarySettlement.Controls.Add(Me.INDPopTxtValueTax)
        Me.LySummarySettlement.Controls.Add(Me.INDPopTxtTotalCxp)
        Me.LySummarySettlement.Controls.Add(Me.INDPopTxtDiscountValue)
        Me.LySummarySettlement.Controls.Add(Me.INDPopTxtValue)
        Me.LySummarySettlement.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LySummarySettlement.Location = New System.Drawing.Point(0, 0)
        Me.LySummarySettlement.Name = "LySummarySettlement"
        Me.LySummarySettlement.Root = Me.LayoutControlGroup2
        Me.LySummarySettlement.Size = New System.Drawing.Size(299, 333)
        Me.LySummarySettlement.TabIndex = 0
        Me.LySummarySettlement.Text = "LayoutControl1"
        '
        'INDPopTxtDistricTaxes
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDPopTxtDistricTaxes, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDPopTxtDistricTaxes, False)
        Me.INDPopTxtDistricTaxes.EditValue = "0"
        Me.INDPopTxtDistricTaxes.Location = New System.Drawing.Point(151, 226)
        Me.IndigoTextEdit1.SetMascara(Me.INDPopTxtDistricTaxes, Presentation.Controls.IndigoTextEdit.EMask.Moneda)
        Me.INDPopTxtDistricTaxes.Name = "INDPopTxtDistricTaxes"
        Me.INDPopTxtDistricTaxes.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDPopTxtDistricTaxes.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 8.0!)
        Me.INDPopTxtDistricTaxes.Properties.Appearance.Options.UseBackColor = True
        Me.INDPopTxtDistricTaxes.Properties.Appearance.Options.UseFont = True
        Me.INDPopTxtDistricTaxes.Properties.Appearance.Options.UseTextOptions = True
        Me.INDPopTxtDistricTaxes.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDPopTxtDistricTaxes.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDPopTxtDistricTaxes.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDPopTxtDistricTaxes.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 8.0!)
        Me.INDPopTxtDistricTaxes.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDPopTxtDistricTaxes.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDPopTxtDistricTaxes.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDPopTxtDistricTaxes.Properties.Mask.EditMask = "c0"
        Me.INDPopTxtDistricTaxes.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDPopTxtDistricTaxes.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDPopTxtDistricTaxes.Properties.ReadOnly = True
        Me.INDPopTxtDistricTaxes.Size = New System.Drawing.Size(135, 20)
        Me.INDPopTxtDistricTaxes.StyleController = Me.LySummarySettlement
        Me.INDPopTxtDistricTaxes.TabIndex = 15
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDPopTxtDistricTaxes, 0)
        '
        'INDPopTxtDeductionOther
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDPopTxtDeductionOther, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDPopTxtDeductionOther, False)
        Me.INDPopTxtDeductionOther.EditValue = "0"
        Me.INDPopTxtDeductionOther.Location = New System.Drawing.Point(151, 186)
        Me.IndigoTextEdit1.SetMascara(Me.INDPopTxtDeductionOther, Presentation.Controls.IndigoTextEdit.EMask.Moneda)
        Me.INDPopTxtDeductionOther.Name = "INDPopTxtDeductionOther"
        Me.INDPopTxtDeductionOther.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDPopTxtDeductionOther.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 8.0!)
        Me.INDPopTxtDeductionOther.Properties.Appearance.Options.UseBackColor = True
        Me.INDPopTxtDeductionOther.Properties.Appearance.Options.UseFont = True
        Me.INDPopTxtDeductionOther.Properties.Appearance.Options.UseTextOptions = True
        Me.INDPopTxtDeductionOther.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDPopTxtDeductionOther.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDPopTxtDeductionOther.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDPopTxtDeductionOther.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 8.0!)
        Me.INDPopTxtDeductionOther.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDPopTxtDeductionOther.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDPopTxtDeductionOther.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDPopTxtDeductionOther.Properties.Mask.EditMask = "c0"
        Me.INDPopTxtDeductionOther.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDPopTxtDeductionOther.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDPopTxtDeductionOther.Properties.ReadOnly = True
        Me.INDPopTxtDeductionOther.Size = New System.Drawing.Size(135, 20)
        Me.INDPopTxtDeductionOther.StyleController = Me.LySummarySettlement
        Me.INDPopTxtDeductionOther.TabIndex = 14
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDPopTxtDeductionOther, 0)
        '
        'INDPopTxtRetentionOther
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDPopTxtRetentionOther, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDPopTxtRetentionOther, False)
        Me.INDPopTxtRetentionOther.EditValue = "0"
        Me.INDPopTxtRetentionOther.Location = New System.Drawing.Point(151, 146)
        Me.IndigoTextEdit1.SetMascara(Me.INDPopTxtRetentionOther, Presentation.Controls.IndigoTextEdit.EMask.Moneda)
        Me.INDPopTxtRetentionOther.Name = "INDPopTxtRetentionOther"
        Me.INDPopTxtRetentionOther.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDPopTxtRetentionOther.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 8.0!)
        Me.INDPopTxtRetentionOther.Properties.Appearance.Options.UseBackColor = True
        Me.INDPopTxtRetentionOther.Properties.Appearance.Options.UseFont = True
        Me.INDPopTxtRetentionOther.Properties.Appearance.Options.UseTextOptions = True
        Me.INDPopTxtRetentionOther.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDPopTxtRetentionOther.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDPopTxtRetentionOther.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDPopTxtRetentionOther.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 8.0!)
        Me.INDPopTxtRetentionOther.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDPopTxtRetentionOther.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDPopTxtRetentionOther.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDPopTxtRetentionOther.Properties.Mask.EditMask = "c0"
        Me.INDPopTxtRetentionOther.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDPopTxtRetentionOther.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDPopTxtRetentionOther.Properties.ReadOnly = True
        Me.INDPopTxtRetentionOther.Size = New System.Drawing.Size(135, 20)
        Me.INDPopTxtRetentionOther.StyleController = Me.LySummarySettlement
        Me.INDPopTxtRetentionOther.TabIndex = 13
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDPopTxtRetentionOther, 0)
        '
        'INDPopTxtRetentionSource
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDPopTxtRetentionSource, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDPopTxtRetentionSource, False)
        Me.INDPopTxtRetentionSource.EditValue = "0"
        Me.INDPopTxtRetentionSource.Location = New System.Drawing.Point(151, 106)
        Me.IndigoTextEdit1.SetMascara(Me.INDPopTxtRetentionSource, Presentation.Controls.IndigoTextEdit.EMask.Moneda)
        Me.INDPopTxtRetentionSource.Name = "INDPopTxtRetentionSource"
        Me.INDPopTxtRetentionSource.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDPopTxtRetentionSource.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 8.0!)
        Me.INDPopTxtRetentionSource.Properties.Appearance.Options.UseBackColor = True
        Me.INDPopTxtRetentionSource.Properties.Appearance.Options.UseFont = True
        Me.INDPopTxtRetentionSource.Properties.Appearance.Options.UseTextOptions = True
        Me.INDPopTxtRetentionSource.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDPopTxtRetentionSource.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDPopTxtRetentionSource.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDPopTxtRetentionSource.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 8.0!)
        Me.INDPopTxtRetentionSource.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDPopTxtRetentionSource.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDPopTxtRetentionSource.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDPopTxtRetentionSource.Properties.Mask.EditMask = "c0"
        Me.INDPopTxtRetentionSource.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDPopTxtRetentionSource.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDPopTxtRetentionSource.Properties.ReadOnly = True
        Me.INDPopTxtRetentionSource.Size = New System.Drawing.Size(135, 20)
        Me.INDPopTxtRetentionSource.StyleController = Me.LySummarySettlement
        Me.INDPopTxtRetentionSource.TabIndex = 12
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDPopTxtRetentionSource, 0)
        '
        'INDPopSpnFreightIVA
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDPopSpnFreightIVA, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDPopSpnFreightIVA, False)
        Me.INDPopSpnFreightIVA.EditValue = "0"
        Me.INDPopSpnFreightIVA.Location = New System.Drawing.Point(11, 186)
        Me.IndigoTextEdit1.SetMascara(Me.INDPopSpnFreightIVA, Presentation.Controls.IndigoTextEdit.EMask.Moneda)
        Me.INDPopSpnFreightIVA.Name = "INDPopSpnFreightIVA"
        Me.INDPopSpnFreightIVA.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDPopSpnFreightIVA.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 8.0!)
        Me.INDPopSpnFreightIVA.Properties.Appearance.Options.UseBackColor = True
        Me.INDPopSpnFreightIVA.Properties.Appearance.Options.UseFont = True
        Me.INDPopSpnFreightIVA.Properties.Appearance.Options.UseTextOptions = True
        Me.INDPopSpnFreightIVA.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDPopSpnFreightIVA.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDPopSpnFreightIVA.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDPopSpnFreightIVA.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 8.0!)
        Me.INDPopSpnFreightIVA.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDPopSpnFreightIVA.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDPopSpnFreightIVA.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDPopSpnFreightIVA.Properties.Mask.EditMask = "c0"
        Me.INDPopSpnFreightIVA.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDPopSpnFreightIVA.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDPopSpnFreightIVA.Properties.ReadOnly = True
        Me.INDPopSpnFreightIVA.Size = New System.Drawing.Size(136, 20)
        Me.INDPopSpnFreightIVA.StyleController = Me.LySummarySettlement
        Me.INDPopSpnFreightIVA.TabIndex = 11
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDPopSpnFreightIVA, 0)
        '
        'INDPopTxtWithholdingICA
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDPopTxtWithholdingICA, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDPopTxtWithholdingICA, False)
        Me.INDPopTxtWithholdingICA.EditValue = "0"
        Me.INDPopTxtWithholdingICA.Location = New System.Drawing.Point(151, 66)
        Me.IndigoTextEdit1.SetMascara(Me.INDPopTxtWithholdingICA, Presentation.Controls.IndigoTextEdit.EMask.Moneda)
        Me.INDPopTxtWithholdingICA.Name = "INDPopTxtWithholdingICA"
        Me.INDPopTxtWithholdingICA.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDPopTxtWithholdingICA.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 8.0!)
        Me.INDPopTxtWithholdingICA.Properties.Appearance.Options.UseBackColor = True
        Me.INDPopTxtWithholdingICA.Properties.Appearance.Options.UseFont = True
        Me.INDPopTxtWithholdingICA.Properties.Appearance.Options.UseTextOptions = True
        Me.INDPopTxtWithholdingICA.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDPopTxtWithholdingICA.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDPopTxtWithholdingICA.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDPopTxtWithholdingICA.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 8.0!)
        Me.INDPopTxtWithholdingICA.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDPopTxtWithholdingICA.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDPopTxtWithholdingICA.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDPopTxtWithholdingICA.Properties.Mask.EditMask = "c0"
        Me.INDPopTxtWithholdingICA.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDPopTxtWithholdingICA.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDPopTxtWithholdingICA.Properties.ReadOnly = True
        Me.INDPopTxtWithholdingICA.Size = New System.Drawing.Size(135, 20)
        Me.INDPopTxtWithholdingICA.StyleController = Me.LySummarySettlement
        Me.INDPopTxtWithholdingICA.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDPopTxtWithholdingICA, 0)
        '
        'INDPopTxtWithholdingTax
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDPopTxtWithholdingTax, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDPopTxtWithholdingTax, False)
        Me.INDPopTxtWithholdingTax.EditValue = "0"
        Me.INDPopTxtWithholdingTax.Location = New System.Drawing.Point(151, 26)
        Me.IndigoTextEdit1.SetMascara(Me.INDPopTxtWithholdingTax, Presentation.Controls.IndigoTextEdit.EMask.Moneda)
        Me.INDPopTxtWithholdingTax.Name = "INDPopTxtWithholdingTax"
        Me.INDPopTxtWithholdingTax.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDPopTxtWithholdingTax.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 8.0!)
        Me.INDPopTxtWithholdingTax.Properties.Appearance.Options.UseBackColor = True
        Me.INDPopTxtWithholdingTax.Properties.Appearance.Options.UseFont = True
        Me.INDPopTxtWithholdingTax.Properties.Appearance.Options.UseTextOptions = True
        Me.INDPopTxtWithholdingTax.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDPopTxtWithholdingTax.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDPopTxtWithholdingTax.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDPopTxtWithholdingTax.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 8.0!)
        Me.INDPopTxtWithholdingTax.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDPopTxtWithholdingTax.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDPopTxtWithholdingTax.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDPopTxtWithholdingTax.Properties.Mask.EditMask = "c0"
        Me.INDPopTxtWithholdingTax.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDPopTxtWithholdingTax.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDPopTxtWithholdingTax.Properties.ReadOnly = True
        Me.INDPopTxtWithholdingTax.Size = New System.Drawing.Size(135, 20)
        Me.INDPopTxtWithholdingTax.StyleController = Me.LySummarySettlement
        Me.INDPopTxtWithholdingTax.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDPopTxtWithholdingTax, 0)
        '
        'INDPopTxtFreightValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDPopTxtFreightValue, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDPopTxtFreightValue, False)
        Me.INDPopTxtFreightValue.EditValue = "0"
        Me.INDPopTxtFreightValue.Location = New System.Drawing.Point(11, 146)
        Me.IndigoTextEdit1.SetMascara(Me.INDPopTxtFreightValue, Presentation.Controls.IndigoTextEdit.EMask.Moneda)
        Me.INDPopTxtFreightValue.Name = "INDPopTxtFreightValue"
        Me.INDPopTxtFreightValue.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDPopTxtFreightValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 8.0!)
        Me.INDPopTxtFreightValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDPopTxtFreightValue.Properties.Appearance.Options.UseFont = True
        Me.INDPopTxtFreightValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDPopTxtFreightValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDPopTxtFreightValue.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDPopTxtFreightValue.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDPopTxtFreightValue.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 8.0!)
        Me.INDPopTxtFreightValue.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDPopTxtFreightValue.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDPopTxtFreightValue.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDPopTxtFreightValue.Properties.Mask.EditMask = "c0"
        Me.INDPopTxtFreightValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDPopTxtFreightValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDPopTxtFreightValue.Properties.ReadOnly = True
        Me.INDPopTxtFreightValue.Size = New System.Drawing.Size(136, 20)
        Me.INDPopTxtFreightValue.StyleController = Me.LySummarySettlement
        Me.INDPopTxtFreightValue.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDPopTxtFreightValue, 0)
        '
        'INDPopTxtValueTax
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDPopTxtValueTax, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDPopTxtValueTax, False)
        Me.INDPopTxtValueTax.EditValue = "0"
        Me.INDPopTxtValueTax.Location = New System.Drawing.Point(11, 106)
        Me.IndigoTextEdit1.SetMascara(Me.INDPopTxtValueTax, Presentation.Controls.IndigoTextEdit.EMask.Moneda)
        Me.INDPopTxtValueTax.Name = "INDPopTxtValueTax"
        Me.INDPopTxtValueTax.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDPopTxtValueTax.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 8.0!)
        Me.INDPopTxtValueTax.Properties.Appearance.Options.UseBackColor = True
        Me.INDPopTxtValueTax.Properties.Appearance.Options.UseFont = True
        Me.INDPopTxtValueTax.Properties.Appearance.Options.UseTextOptions = True
        Me.INDPopTxtValueTax.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDPopTxtValueTax.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDPopTxtValueTax.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDPopTxtValueTax.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 8.0!)
        Me.INDPopTxtValueTax.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDPopTxtValueTax.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDPopTxtValueTax.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDPopTxtValueTax.Properties.Mask.EditMask = "c0"
        Me.INDPopTxtValueTax.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDPopTxtValueTax.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDPopTxtValueTax.Properties.ReadOnly = True
        Me.INDPopTxtValueTax.Size = New System.Drawing.Size(136, 20)
        Me.INDPopTxtValueTax.StyleController = Me.LySummarySettlement
        Me.INDPopTxtValueTax.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDPopTxtValueTax, 0)
        '
        'INDPopTxtTotalCxp
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDPopTxtTotalCxp, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDPopTxtTotalCxp, False)
        Me.INDPopTxtTotalCxp.EditValue = "0"
        Me.INDPopTxtTotalCxp.Location = New System.Drawing.Point(106, 250)
        Me.IndigoTextEdit1.SetMascara(Me.INDPopTxtTotalCxp, Presentation.Controls.IndigoTextEdit.EMask.Moneda)
        Me.INDPopTxtTotalCxp.Name = "INDPopTxtTotalCxp"
        Me.INDPopTxtTotalCxp.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDPopTxtTotalCxp.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 8.0!, System.Drawing.FontStyle.Bold)
        Me.INDPopTxtTotalCxp.Properties.Appearance.Options.UseBackColor = True
        Me.INDPopTxtTotalCxp.Properties.Appearance.Options.UseFont = True
        Me.INDPopTxtTotalCxp.Properties.Appearance.Options.UseTextOptions = True
        Me.INDPopTxtTotalCxp.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDPopTxtTotalCxp.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDPopTxtTotalCxp.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDPopTxtTotalCxp.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 8.0!, System.Drawing.FontStyle.Bold)
        Me.INDPopTxtTotalCxp.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDPopTxtTotalCxp.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDPopTxtTotalCxp.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDPopTxtTotalCxp.Properties.Mask.EditMask = "c0"
        Me.INDPopTxtTotalCxp.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDPopTxtTotalCxp.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDPopTxtTotalCxp.Properties.ReadOnly = True
        Me.INDPopTxtTotalCxp.Size = New System.Drawing.Size(180, 20)
        Me.INDPopTxtTotalCxp.StyleController = Me.LySummarySettlement
        Me.INDPopTxtTotalCxp.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDPopTxtTotalCxp, 0)
        '
        'INDPopTxtDiscountValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDPopTxtDiscountValue, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDPopTxtDiscountValue, False)
        Me.INDPopTxtDiscountValue.EditValue = "0"
        Me.INDPopTxtDiscountValue.Location = New System.Drawing.Point(11, 66)
        Me.IndigoTextEdit1.SetMascara(Me.INDPopTxtDiscountValue, Presentation.Controls.IndigoTextEdit.EMask.Moneda)
        Me.INDPopTxtDiscountValue.Name = "INDPopTxtDiscountValue"
        Me.INDPopTxtDiscountValue.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDPopTxtDiscountValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 8.0!)
        Me.INDPopTxtDiscountValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDPopTxtDiscountValue.Properties.Appearance.Options.UseFont = True
        Me.INDPopTxtDiscountValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDPopTxtDiscountValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDPopTxtDiscountValue.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDPopTxtDiscountValue.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDPopTxtDiscountValue.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 8.0!)
        Me.INDPopTxtDiscountValue.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDPopTxtDiscountValue.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDPopTxtDiscountValue.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDPopTxtDiscountValue.Properties.Mask.EditMask = "c0"
        Me.INDPopTxtDiscountValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDPopTxtDiscountValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDPopTxtDiscountValue.Properties.ReadOnly = True
        Me.INDPopTxtDiscountValue.Size = New System.Drawing.Size(135, 20)
        Me.INDPopTxtDiscountValue.StyleController = Me.LySummarySettlement
        Me.INDPopTxtDiscountValue.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDPopTxtDiscountValue, 0)
        '
        'INDPopTxtValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDPopTxtValue, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDPopTxtValue, False)
        Me.INDPopTxtValue.EditValue = "0"
        Me.INDPopTxtValue.Location = New System.Drawing.Point(11, 26)
        Me.IndigoTextEdit1.SetMascara(Me.INDPopTxtValue, Presentation.Controls.IndigoTextEdit.EMask.Moneda)
        Me.INDPopTxtValue.Name = "INDPopTxtValue"
        Me.INDPopTxtValue.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDPopTxtValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 8.0!)
        Me.INDPopTxtValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDPopTxtValue.Properties.Appearance.Options.UseFont = True
        Me.INDPopTxtValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDPopTxtValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDPopTxtValue.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDPopTxtValue.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDPopTxtValue.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 8.0!)
        Me.INDPopTxtValue.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDPopTxtValue.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDPopTxtValue.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDPopTxtValue.Properties.Mask.EditMask = "c0"
        Me.INDPopTxtValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDPopTxtValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDPopTxtValue.Properties.ReadOnly = True
        Me.INDPopTxtValue.Size = New System.Drawing.Size(136, 20)
        Me.INDPopTxtValue.StyleController = Me.LySummarySettlement
        Me.INDPopTxtValue.TabIndex = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDPopTxtValue, 0)
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup2.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup2.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup2, False)
        Me.LayoutControlGroup2.CustomizationFormText = "LayoutControlGroup2"
        Me.LayoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup2.GroupBordersVisible = False
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyPopTxtValue, Me.INDLyPopTxtValueTax, Me.INDLyPopTxtFreightValue, Me.INDLyPopSpnFreightIVA, Me.EmptySpaceItem1, Me.INDLyPopTxtTotalCxp, Me.INDLyPopTxtDistricTaxes, Me.INDLyPopTxtDeductionOther, Me.INDLyPopTxtRetentionOther, Me.INDLyPopTxtWithholdingTax, Me.INDLyPopTxtDiscountValue, Me.INDLyPopTxtWithholdingICA, Me.INDLyPopTxtRetentionSource})
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(299, 333)
        Me.LayoutControlGroup2.TextVisible = False
        '
        'INDLyPopTxtValue
        '
        Me.INDLyPopTxtValue.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 8.0!)
        Me.INDLyPopTxtValue.AppearanceItemCaption.Options.UseFont = True
        Me.INDLyPopTxtValue.Control = Me.INDPopTxtValue
        Me.INDLyPopTxtValue.CustomizationFormText = "Valor Neto"
        Me.INDLyPopTxtValue.Location = New System.Drawing.Point(0, 0)
        Me.INDLyPopTxtValue.MaxSize = New System.Drawing.Size(140, 40)
        Me.INDLyPopTxtValue.MinSize = New System.Drawing.Size(140, 40)
        Me.INDLyPopTxtValue.Name = "INDLyPopTxtValue"
        Me.INDLyPopTxtValue.Size = New System.Drawing.Size(140, 40)
        Me.INDLyPopTxtValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyPopTxtValue.Text = "Sub Total"
        Me.INDLyPopTxtValue.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyPopTxtValue.TextSize = New System.Drawing.Size(92, 13)
        '
        'INDLyPopTxtValueTax
        '
        Me.INDLyPopTxtValueTax.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 8.0!)
        Me.INDLyPopTxtValueTax.AppearanceItemCaption.Options.UseFont = True
        Me.INDLyPopTxtValueTax.Control = Me.INDPopTxtValueTax
        Me.INDLyPopTxtValueTax.CustomizationFormText = "IVA"
        Me.INDLyPopTxtValueTax.Location = New System.Drawing.Point(0, 80)
        Me.INDLyPopTxtValueTax.MaxSize = New System.Drawing.Size(140, 40)
        Me.INDLyPopTxtValueTax.MinSize = New System.Drawing.Size(140, 40)
        Me.INDLyPopTxtValueTax.Name = "INDLyPopTxtValueTax"
        Me.INDLyPopTxtValueTax.Size = New System.Drawing.Size(140, 40)
        Me.INDLyPopTxtValueTax.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyPopTxtValueTax.Text = "IVA"
        Me.INDLyPopTxtValueTax.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyPopTxtValueTax.TextSize = New System.Drawing.Size(92, 13)
        '
        'INDLyPopTxtFreightValue
        '
        Me.INDLyPopTxtFreightValue.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 8.0!)
        Me.INDLyPopTxtFreightValue.AppearanceItemCaption.Options.UseFont = True
        Me.INDLyPopTxtFreightValue.Control = Me.INDPopTxtFreightValue
        Me.INDLyPopTxtFreightValue.CustomizationFormText = "Flete"
        Me.INDLyPopTxtFreightValue.Location = New System.Drawing.Point(0, 120)
        Me.INDLyPopTxtFreightValue.MaxSize = New System.Drawing.Size(140, 40)
        Me.INDLyPopTxtFreightValue.MinSize = New System.Drawing.Size(140, 40)
        Me.INDLyPopTxtFreightValue.Name = "INDLyPopTxtFreightValue"
        Me.INDLyPopTxtFreightValue.Size = New System.Drawing.Size(140, 40)
        Me.INDLyPopTxtFreightValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyPopTxtFreightValue.Text = "Flete"
        Me.INDLyPopTxtFreightValue.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyPopTxtFreightValue.TextSize = New System.Drawing.Size(92, 13)
        '
        'INDLyPopSpnFreightIVA
        '
        Me.INDLyPopSpnFreightIVA.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 8.0!)
        Me.INDLyPopSpnFreightIVA.AppearanceItemCaption.Options.UseFont = True
        Me.INDLyPopSpnFreightIVA.Control = Me.INDPopSpnFreightIVA
        Me.INDLyPopSpnFreightIVA.CustomizationFormText = "Impuesto Flete"
        Me.INDLyPopSpnFreightIVA.Location = New System.Drawing.Point(0, 160)
        Me.INDLyPopSpnFreightIVA.MaxSize = New System.Drawing.Size(140, 40)
        Me.INDLyPopSpnFreightIVA.MinSize = New System.Drawing.Size(140, 40)
        Me.INDLyPopSpnFreightIVA.Name = "INDLyPopSpnFreightIVA"
        Me.INDLyPopSpnFreightIVA.Size = New System.Drawing.Size(140, 40)
        Me.INDLyPopSpnFreightIVA.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyPopSpnFreightIVA.Text = "Impuesto Flete"
        Me.INDLyPopSpnFreightIVA.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyPopSpnFreightIVA.TextSize = New System.Drawing.Size(92, 13)
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 200)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(140, 40)
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'INDLyPopTxtTotalCxp
        '
        Me.INDLyPopTxtTotalCxp.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 8.0!, System.Drawing.FontStyle.Bold)
        Me.INDLyPopTxtTotalCxp.AppearanceItemCaption.Options.UseFont = True
        Me.INDLyPopTxtTotalCxp.Control = Me.INDPopTxtTotalCxp
        Me.INDLyPopTxtTotalCxp.CustomizationFormText = "Total CxP"
        Me.INDLyPopTxtTotalCxp.Location = New System.Drawing.Point(0, 240)
        Me.INDLyPopTxtTotalCxp.MaxSize = New System.Drawing.Size(279, 29)
        Me.INDLyPopTxtTotalCxp.MinSize = New System.Drawing.Size(279, 29)
        Me.INDLyPopTxtTotalCxp.Name = "INDLyPopTxtTotalCxp"
        Me.INDLyPopTxtTotalCxp.Size = New System.Drawing.Size(281, 77)
        Me.INDLyPopTxtTotalCxp.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyPopTxtTotalCxp.Text = "Total"
        Me.INDLyPopTxtTotalCxp.TextSize = New System.Drawing.Size(92, 13)
        '
        'INDLyPopTxtDistricTaxes
        '
        Me.INDLyPopTxtDistricTaxes.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 8.0!)
        Me.INDLyPopTxtDistricTaxes.AppearanceItemCaption.Options.UseFont = True
        Me.INDLyPopTxtDistricTaxes.Control = Me.INDPopTxtDistricTaxes
        Me.INDLyPopTxtDistricTaxes.CustomizationFormText = "Impuestos Distritales"
        Me.INDLyPopTxtDistricTaxes.Location = New System.Drawing.Point(140, 200)
        Me.INDLyPopTxtDistricTaxes.MaxSize = New System.Drawing.Size(139, 40)
        Me.INDLyPopTxtDistricTaxes.MinSize = New System.Drawing.Size(139, 40)
        Me.INDLyPopTxtDistricTaxes.Name = "INDLyPopTxtDistricTaxes"
        Me.INDLyPopTxtDistricTaxes.Size = New System.Drawing.Size(141, 40)
        Me.INDLyPopTxtDistricTaxes.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyPopTxtDistricTaxes.Text = "Impuestos Distritales"
        Me.INDLyPopTxtDistricTaxes.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyPopTxtDistricTaxes.TextSize = New System.Drawing.Size(92, 13)
        '
        'INDLyPopTxtDeductionOther
        '
        Me.INDLyPopTxtDeductionOther.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 8.0!)
        Me.INDLyPopTxtDeductionOther.AppearanceItemCaption.Options.UseFont = True
        Me.INDLyPopTxtDeductionOther.Control = Me.INDPopTxtDeductionOther
        Me.INDLyPopTxtDeductionOther.CustomizationFormText = "Otras Deducciones"
        Me.INDLyPopTxtDeductionOther.Location = New System.Drawing.Point(140, 160)
        Me.INDLyPopTxtDeductionOther.MaxSize = New System.Drawing.Size(139, 40)
        Me.INDLyPopTxtDeductionOther.MinSize = New System.Drawing.Size(139, 40)
        Me.INDLyPopTxtDeductionOther.Name = "INDLyPopTxtDeductionOther"
        Me.INDLyPopTxtDeductionOther.Size = New System.Drawing.Size(141, 40)
        Me.INDLyPopTxtDeductionOther.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyPopTxtDeductionOther.Text = "Otras Deducciones"
        Me.INDLyPopTxtDeductionOther.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyPopTxtDeductionOther.TextSize = New System.Drawing.Size(92, 13)
        '
        'INDLyPopTxtRetentionOther
        '
        Me.INDLyPopTxtRetentionOther.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 8.0!)
        Me.INDLyPopTxtRetentionOther.AppearanceItemCaption.Options.UseFont = True
        Me.INDLyPopTxtRetentionOther.Control = Me.INDPopTxtRetentionOther
        Me.INDLyPopTxtRetentionOther.CustomizationFormText = "Otras Retenciones"
        Me.INDLyPopTxtRetentionOther.Location = New System.Drawing.Point(140, 120)
        Me.INDLyPopTxtRetentionOther.MaxSize = New System.Drawing.Size(139, 40)
        Me.INDLyPopTxtRetentionOther.MinSize = New System.Drawing.Size(139, 40)
        Me.INDLyPopTxtRetentionOther.Name = "INDLyPopTxtRetentionOther"
        Me.INDLyPopTxtRetentionOther.Size = New System.Drawing.Size(141, 40)
        Me.INDLyPopTxtRetentionOther.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyPopTxtRetentionOther.Text = "Otras Retenciones"
        Me.INDLyPopTxtRetentionOther.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyPopTxtRetentionOther.TextSize = New System.Drawing.Size(92, 13)
        '
        'INDLyPopTxtWithholdingTax
        '
        Me.INDLyPopTxtWithholdingTax.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 8.0!)
        Me.INDLyPopTxtWithholdingTax.AppearanceItemCaption.Options.UseFont = True
        Me.INDLyPopTxtWithholdingTax.Control = Me.INDPopTxtWithholdingTax
        Me.INDLyPopTxtWithholdingTax.CustomizationFormText = "Retencion IVA"
        Me.INDLyPopTxtWithholdingTax.Location = New System.Drawing.Point(140, 0)
        Me.INDLyPopTxtWithholdingTax.MaxSize = New System.Drawing.Size(139, 40)
        Me.INDLyPopTxtWithholdingTax.MinSize = New System.Drawing.Size(139, 40)
        Me.INDLyPopTxtWithholdingTax.Name = "INDLyPopTxtWithholdingTax"
        Me.INDLyPopTxtWithholdingTax.Size = New System.Drawing.Size(141, 40)
        Me.INDLyPopTxtWithholdingTax.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyPopTxtWithholdingTax.Text = "Retencion IVA"
        Me.INDLyPopTxtWithholdingTax.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyPopTxtWithholdingTax.TextSize = New System.Drawing.Size(92, 13)
        '
        'INDLyPopTxtDiscountValue
        '
        Me.INDLyPopTxtDiscountValue.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 8.0!)
        Me.INDLyPopTxtDiscountValue.AppearanceItemCaption.Options.UseFont = True
        Me.INDLyPopTxtDiscountValue.Control = Me.INDPopTxtDiscountValue
        Me.INDLyPopTxtDiscountValue.CustomizationFormText = "Descuento"
        Me.INDLyPopTxtDiscountValue.Location = New System.Drawing.Point(0, 40)
        Me.INDLyPopTxtDiscountValue.MaxSize = New System.Drawing.Size(139, 40)
        Me.INDLyPopTxtDiscountValue.MinSize = New System.Drawing.Size(139, 40)
        Me.INDLyPopTxtDiscountValue.Name = "INDLyPopTxtDiscountValue"
        Me.INDLyPopTxtDiscountValue.Size = New System.Drawing.Size(140, 40)
        Me.INDLyPopTxtDiscountValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyPopTxtDiscountValue.Text = "Descuento"
        Me.INDLyPopTxtDiscountValue.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyPopTxtDiscountValue.TextSize = New System.Drawing.Size(92, 13)
        '
        'INDLyPopTxtWithholdingICA
        '
        Me.INDLyPopTxtWithholdingICA.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 8.0!)
        Me.INDLyPopTxtWithholdingICA.AppearanceItemCaption.Options.UseFont = True
        Me.INDLyPopTxtWithholdingICA.Control = Me.INDPopTxtWithholdingICA
        Me.INDLyPopTxtWithholdingICA.CustomizationFormText = "Retencion ICA"
        Me.INDLyPopTxtWithholdingICA.Location = New System.Drawing.Point(140, 40)
        Me.INDLyPopTxtWithholdingICA.MaxSize = New System.Drawing.Size(139, 40)
        Me.INDLyPopTxtWithholdingICA.MinSize = New System.Drawing.Size(139, 40)
        Me.INDLyPopTxtWithholdingICA.Name = "INDLyPopTxtWithholdingICA"
        Me.INDLyPopTxtWithholdingICA.Size = New System.Drawing.Size(141, 40)
        Me.INDLyPopTxtWithholdingICA.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyPopTxtWithholdingICA.Text = "Retencion ICA"
        Me.INDLyPopTxtWithholdingICA.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyPopTxtWithholdingICA.TextSize = New System.Drawing.Size(92, 13)
        '
        'INDLyPopTxtRetentionSource
        '
        Me.INDLyPopTxtRetentionSource.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 8.0!)
        Me.INDLyPopTxtRetentionSource.AppearanceItemCaption.Options.UseFont = True
        Me.INDLyPopTxtRetentionSource.Control = Me.INDPopTxtRetentionSource
        Me.INDLyPopTxtRetentionSource.CustomizationFormText = "Retencion Fuente"
        Me.INDLyPopTxtRetentionSource.Location = New System.Drawing.Point(140, 80)
        Me.INDLyPopTxtRetentionSource.MaxSize = New System.Drawing.Size(139, 40)
        Me.INDLyPopTxtRetentionSource.MinSize = New System.Drawing.Size(139, 40)
        Me.INDLyPopTxtRetentionSource.Name = "INDLyPopTxtRetentionSource"
        Me.INDLyPopTxtRetentionSource.Size = New System.Drawing.Size(141, 40)
        Me.INDLyPopTxtRetentionSource.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyPopTxtRetentionSource.Text = "Retencion Fuente"
        Me.INDLyPopTxtRetentionSource.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyPopTxtRetentionSource.TextSize = New System.Drawing.Size(92, 13)
        '
        'INDtxtFreightIVAValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtFreightIVAValue, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtFreightIVAValue, False)
        Me.INDtxtFreightIVAValue.Enabled = False
        Me.INDtxtFreightIVAValue.EnterMoveNextControl = True
        Me.INDtxtFreightIVAValue.Location = New System.Drawing.Point(846, 255)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtFreightIVAValue, Presentation.Controls.IndigoTextEdit.EMask.Moneda)
        Me.INDtxtFreightIVAValue.Name = "INDtxtFreightIVAValue"
        Me.INDtxtFreightIVAValue.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtFreightIVAValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtFreightIVAValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtFreightIVAValue.Properties.Appearance.Options.UseFont = True
        Me.INDtxtFreightIVAValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtFreightIVAValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtFreightIVAValue.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtFreightIVAValue.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtFreightIVAValue.Properties.Mask.EditMask = "c0"
        Me.INDtxtFreightIVAValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtFreightIVAValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtFreightIVAValue.Properties.ReadOnly = True
        Me.INDtxtFreightIVAValue.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtFreightIVAValue.StyleController = Me.INDlyEntryDevolution
        Me.INDtxtFreightIVAValue.TabIndex = 11
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtFreightIVAValue, 0)
        '
        'INDseFreightIVAPercentage
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDseFreightIVAPercentage, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDseFreightIVAPercentage, False)
        Me.INDseFreightIVAPercentage.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDseFreightIVAPercentage.Enabled = False
        Me.INDseFreightIVAPercentage.EnterMoveNextControl = True
        Me.INDseFreightIVAPercentage.Location = New System.Drawing.Point(846, 195)
        Me.IndigoTextEdit1.SetMascara(Me.INDseFreightIVAPercentage, Presentation.Controls.IndigoTextEdit.EMask.Porcentaje)
        Me.INDseFreightIVAPercentage.Name = "INDseFreightIVAPercentage"
        Me.INDseFreightIVAPercentage.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDseFreightIVAPercentage.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseFreightIVAPercentage.Properties.Appearance.Options.UseBackColor = True
        Me.INDseFreightIVAPercentage.Properties.Appearance.Options.UseFont = True
        Me.INDseFreightIVAPercentage.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseFreightIVAPercentage.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDseFreightIVAPercentage.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDseFreightIVAPercentage.Properties.Mask.EditMask = "P"
        Me.INDseFreightIVAPercentage.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDseFreightIVAPercentage.Properties.ReadOnly = True
        Me.INDseFreightIVAPercentage.Size = New System.Drawing.Size(386, 28)
        Me.INDseFreightIVAPercentage.StyleController = Me.INDlyEntryDevolution
        Me.INDseFreightIVAPercentage.TabIndex = 10
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDseFreightIVAPercentage, 0)
        '
        'INDtxtFreightValueEntry
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtFreightValueEntry, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtFreightValueEntry, False)
        Me.INDtxtFreightValueEntry.Enabled = False
        Me.INDtxtFreightValueEntry.EnterMoveNextControl = True
        Me.INDtxtFreightValueEntry.Location = New System.Drawing.Point(846, 135)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtFreightValueEntry, Presentation.Controls.IndigoTextEdit.EMask.Moneda)
        Me.INDtxtFreightValueEntry.Name = "INDtxtFreightValueEntry"
        Me.INDtxtFreightValueEntry.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtFreightValueEntry.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtFreightValueEntry.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtFreightValueEntry.Properties.Appearance.Options.UseFont = True
        Me.INDtxtFreightValueEntry.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtFreightValueEntry.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtFreightValueEntry.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtFreightValueEntry.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtFreightValueEntry.Properties.Mask.EditMask = "c0"
        Me.INDtxtFreightValueEntry.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtFreightValueEntry.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtFreightValueEntry.Properties.ReadOnly = True
        Me.INDtxtFreightValueEntry.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtFreightValueEntry.StyleController = Me.INDlyEntryDevolution
        Me.INDtxtFreightValueEntry.TabIndex = 9
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtFreightValueEntry, 0)
        '
        'INDtxtFreightValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtFreightValue, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtFreightValue, False)
        Me.INDtxtFreightValue.EnterMoveNextControl = True
        Me.INDtxtFreightValue.Location = New System.Drawing.Point(846, 75)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtFreightValue, Presentation.Controls.IndigoTextEdit.EMask.Moneda)
        Me.INDtxtFreightValue.Name = "INDtxtFreightValue"
        Me.INDtxtFreightValue.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtFreightValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtFreightValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtFreightValue.Properties.Appearance.Options.UseFont = True
        Me.INDtxtFreightValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtFreightValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtFreightValue.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtFreightValue.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtFreightValue.Properties.Mask.EditMask = "c0"
        Me.INDtxtFreightValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtFreightValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtFreightValue.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtFreightValue.StyleController = Me.INDlyEntryDevolution
        Me.INDtxtFreightValue.TabIndex = 8
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtFreightValue, 0)
        '
        'INDgcItem
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcItem, Nothing)
        Me.IndigoGridControl11.SetAddActions(Me.INDgcItem, Nothing)
        Me.IndigoGridControl11.SetControlNextFocus(Me.INDgcItem, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcItem, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcItem, False)
        Me.IndigoGridControl11.SetExportButton(Me.INDgcItem, False)
        Me.IndigoGridControl11.SetGuardarXml(Me.INDgcItem, True)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcItem, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcItem, False)
        Me.IndigoGridControl11.SetHoldSize(Me.INDgcItem, False)
        Me.IndigoGridControl11.SetHotTrack(Me.INDgcItem, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcItem, False)
        Me.INDgcItem.Location = New System.Drawing.Point(1258, 49)
        Me.INDgcItem.MainView = Me.INDviewItem
        Me.INDgcItem.Name = "INDgcItem"
        Me.INDgcItem.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrepCheckSelectOption})
        Me.INDgcItem.Size = New System.Drawing.Size(824, 469)
        Me.IndigoGridControl11.SetSizeConstraintsType(Me.INDgcItem, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcItem, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcItem.TabIndex = 12
        Me.INDgcItem.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDviewItem})
        '
        'INDviewItem
        '
        Me.INDviewItem.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewItem.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewItem.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDviewItem.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewItem.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewItem.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDviewItem.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewItem.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewItem.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewItem.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewItem.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewItem.Appearance.Row.Options.UseFont = True
        Me.INDviewItem.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDviewItem.Appearance.ViewCaption.Options.UseFont = True
        Me.INDviewItem.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGcProduct, Me.GridColumn1, Me.GridColumn7, Me.GridColumn8, Me.GridColumn9})
        Me.INDviewItem.GridControl = Me.INDgcItem
        Me.INDviewItem.Name = "INDviewItem"
        Me.INDviewItem.OptionsSelection.MultiSelect = True
        Me.INDviewItem.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewItem.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewItem.OptionsView.ShowAutoFilterRow = True
        Me.INDviewItem.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView11.SetTemaIndigoMetro(Me.INDviewItem, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewItem, False)
        '
        'INDGcProduct
        '
        Me.INDGcProduct.Caption = "Articulo"
        Me.INDGcProduct.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDGcProduct.FieldName = "CodeNameItem"
        Me.INDGcProduct.Name = "INDGcProduct"
        Me.INDGcProduct.OptionsColumn.AllowEdit = False
        Me.INDGcProduct.OptionsColumn.AllowFocus = False
        Me.INDGcProduct.Visible = True
        Me.INDGcProduct.VisibleIndex = 0
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Placa"
        Me.GridColumn1.FieldName = "Plate"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 1
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Serie"
        Me.GridColumn7.FieldName = "Serie"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsColumn.AllowEdit = False
        Me.GridColumn7.OptionsColumn.AllowFocus = False
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 2
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Valor Unitario"
        Me.GridColumn8.DisplayFormat.FormatString = "C"
        Me.GridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn8.FieldName = "UnitValue"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.OptionsColumn.AllowEdit = False
        Me.GridColumn8.OptionsColumn.AllowFocus = False
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 3
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Sel."
        Me.GridColumn9.ColumnEdit = Me.INDrepCheckSelectOption
        Me.GridColumn9.FieldName = "SelectOption"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 4
        '
        'INDrepCheckSelectOption
        '
        Me.INDrepCheckSelectOption.AutoHeight = False
        Me.INDrepCheckSelectOption.Name = "INDrepCheckSelectOption"
        '
        'INDdteBillDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdteBillDate, True)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDdteBillDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdteBillDate, False)
        Me.INDdteBillDate.EditValue = Nothing
        Me.INDdteBillDate.EnterMoveNextControl = True
        Me.INDdteBillDate.Location = New System.Drawing.Point(434, 189)
        Me.IndigoTextEdit1.SetMascara(Me.INDdteBillDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDdteBillDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDdteBillDate.Name = "INDdteBillDate"
        Me.INDdteBillDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDdteBillDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteBillDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDdteBillDate.Properties.Appearance.Options.UseFont = True
        Me.INDdteBillDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDdteBillDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDdteBillDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteBillDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDdteBillDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDdteBillDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdteBillDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteBillDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteBillDate.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.INDdteBillDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDdteBillDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdteBillDate.Properties.ReadOnly = True
        Me.INDdteBillDate.Size = New System.Drawing.Size(386, 28)
        Me.INDdteBillDate.StyleController = Me.INDlyEntryDevolution
        Me.INDdteBillDate.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdteBillDate, 0)
        '
        'INDtxtBillNumber
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtBillNumber, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtBillNumber, False)
        Me.INDtxtBillNumber.EnterMoveNextControl = True
        Me.INDtxtBillNumber.Location = New System.Drawing.Point(434, 129)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtBillNumber, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtBillNumber.Name = "INDtxtBillNumber"
        Me.INDtxtBillNumber.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtBillNumber.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtBillNumber.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtBillNumber.Properties.Appearance.Options.UseFont = True
        Me.INDtxtBillNumber.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtBillNumber.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtBillNumber.Properties.ReadOnly = True
        Me.INDtxtBillNumber.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtBillNumber.StyleController = Me.INDlyEntryDevolution
        Me.INDtxtBillNumber.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtBillNumber, 0)
        '
        'INDsleFixedAssetEntry
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleFixedAssetEntry, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleFixedAssetEntry, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleFixedAssetEntry, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleFixedAssetEntry, True)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleFixedAssetEntry, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleFixedAssetEntry, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleFixedAssetEntry, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleFixedAssetEntry, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleFixedAssetEntry, False)
        Me.INDsleFixedAssetEntry.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleFixedAssetEntry, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleFixedAssetEntry, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleFixedAssetEntry, False)
        Me.INDsleFixedAssetEntry.Location = New System.Drawing.Point(434, 69)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleFixedAssetEntry, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleFixedAssetEntry.Name = "INDsleFixedAssetEntry"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleFixedAssetEntry, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleFixedAssetEntry, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleFixedAssetEntry, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDsleFixedAssetEntry, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleFixedAssetEntry, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleFixedAssetEntry, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleFixedAssetEntry, False)
        Me.INDsleFixedAssetEntry.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDsleFixedAssetEntry.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleFixedAssetEntry.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleFixedAssetEntry.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleFixedAssetEntry.Properties.Appearance.Options.UseFont = True
        Me.INDsleFixedAssetEntry.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleFixedAssetEntry.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleFixedAssetEntry.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleFixedAssetEntry.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleFixedAssetEntry.Properties.DisplayMember = "CodeSupplier"
        Me.INDsleFixedAssetEntry.Properties.NullText = ""
        Me.INDsleFixedAssetEntry.Properties.PopupFormMinSize = New System.Drawing.Size(800, 0)
        Me.INDsleFixedAssetEntry.Properties.PopupFormSize = New System.Drawing.Size(800, 0)
        Me.INDsleFixedAssetEntry.Properties.PopupSizeable = False
        Me.INDsleFixedAssetEntry.Properties.PopupView = Me.INDviewEntry
        Me.INDsleFixedAssetEntry.Properties.ShowFooter = False
        Me.INDsleFixedAssetEntry.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleFixedAssetEntry, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleFixedAssetEntry, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleFixedAssetEntry, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleFixedAssetEntry, True)
        Me.INDsleFixedAssetEntry.Size = New System.Drawing.Size(386, 28)
        Me.INDsleFixedAssetEntry.StyleController = Me.INDlyEntryDevolution
        Me.INDsleFixedAssetEntry.TabIndex = 3
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleFixedAssetEntry, "1116")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleFixedAssetEntry, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleFixedAssetEntry, "{0} - {1}")
        Me.INDsleFixedAssetEntry.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleFixedAssetEntry, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleFixedAssetEntry, False)
        '
        'INDviewEntry
        '
        Me.INDviewEntry.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewEntry.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewEntry.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewEntry.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewEntry.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDviewEntry.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewEntry.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewEntry.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewEntry.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewEntry.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewEntry.Appearance.Row.Options.UseFont = True
        Me.INDviewEntry.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6})
        Me.INDviewEntry.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDviewEntry.Name = "INDviewEntry"
        Me.INDviewEntry.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDviewEntry.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewEntry.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewEntry.OptionsView.ShowAutoFilterRow = True
        Me.INDviewEntry.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView11.SetTemaIndigoMetro(Me.INDviewEntry, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewEntry, False)
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Código"
        Me.GridColumn2.FieldName = "Code"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Proveedor"
        Me.GridColumn3.FieldName = "SupplierId.CodeName"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 1
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Fecha Ingreso"
        Me.GridColumn4.FieldName = "EntryDate"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 2
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "No. Ingreso"
        Me.GridColumn5.FieldName = "EntryNumber"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 3
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Tipo Adquisición"
        Me.GridColumn6.FieldName = "AdquisitionTypeName"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 4
        '
        'INDmemoDescription
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDmemoDescription, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDmemoDescription, True)
        Me.INDmemoDescription.EnterMoveNextControl = True
        Me.INDmemoDescription.Location = New System.Drawing.Point(22, 189)
        Me.IndigoTextEdit1.SetMascara(Me.INDmemoDescription, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDmemoDescription.Name = "INDmemoDescription"
        Me.INDmemoDescription.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDmemoDescription.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmemoDescription.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDmemoDescription.Properties.Appearance.Options.UseBackColor = True
        Me.INDmemoDescription.Properties.Appearance.Options.UseFont = True
        Me.INDmemoDescription.Properties.Appearance.Options.UseForeColor = True
        Me.INDmemoDescription.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmemoDescription.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDmemoDescription.Size = New System.Drawing.Size(386, 96)
        Me.INDmemoDescription.StyleController = Me.INDlyEntryDevolution
        Me.INDmemoDescription.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDmemoDescription, 0)
        Me.INDmemoDescription.ToolTip = "Este Campo es Necesario"
        '
        'INDdteDocumentDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdteDocumentDate, True)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDdteDocumentDate, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdteDocumentDate, True)
        Me.INDdteDocumentDate.EditValue = Nothing
        Me.INDdteDocumentDate.EnterMoveNextControl = True
        Me.INDdteDocumentDate.Location = New System.Drawing.Point(22, 129)
        Me.IndigoTextEdit1.SetMascara(Me.INDdteDocumentDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDdteDocumentDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDdteDocumentDate.Name = "INDdteDocumentDate"
        Me.INDdteDocumentDate.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDdteDocumentDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteDocumentDate.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDdteDocumentDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDdteDocumentDate.Properties.Appearance.Options.UseFont = True
        Me.INDdteDocumentDate.Properties.Appearance.Options.UseForeColor = True
        Me.INDdteDocumentDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDdteDocumentDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDdteDocumentDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteDocumentDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDdteDocumentDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDdteDocumentDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdteDocumentDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteDocumentDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteDocumentDate.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.INDdteDocumentDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDdteDocumentDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdteDocumentDate.Size = New System.Drawing.Size(386, 28)
        Me.INDdteDocumentDate.StyleController = Me.INDlyEntryDevolution
        Me.INDdteDocumentDate.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdteDocumentDate, 0)
        Me.INDdteDocumentDate.ToolTip = "Este Campo es Necesario"
        '
        'INDbtnCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbtnCode, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbtnCode, True)
        Me.INDbtnCode.Location = New System.Drawing.Point(22, 69)
        Me.IndigoTextEdit1.SetMascara(Me.INDbtnCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDbtnCode.Name = "INDbtnCode"
        Me.INDbtnCode.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDbtnCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDbtnCode.Properties.Appearance.Options.UseFont = True
        Me.INDbtnCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseFont = True
        EditorButtonImageOptions1.Image = Global.Presentation.FixedAsset.My.Resources.Resources.BuscarMetro
        Me.INDbtnCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDbtnCode.Properties.MaxLength = 20
        Me.INDbtnCode.Size = New System.Drawing.Size(386, 28)
        Me.INDbtnCode.StyleController = Me.INDlyEntryDevolution
        Me.INDbtnCode.TabIndex = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbtnCode, 0)
        Me.INDbtnCode.ToolTip = "Este Campo es Necesario"
        '
        'INDtxtSupplier
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtSupplier, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtSupplier, False)
        Me.INDtxtSupplier.EnterMoveNextControl = True
        Me.INDtxtSupplier.Location = New System.Drawing.Point(434, 249)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtSupplier, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtSupplier.Name = "INDtxtSupplier"
        Me.INDtxtSupplier.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtSupplier.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtSupplier.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtSupplier.Properties.Appearance.Options.UseFont = True
        Me.INDtxtSupplier.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtSupplier.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtSupplier.Properties.ReadOnly = True
        Me.INDtxtSupplier.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtSupplier.StyleController = Me.INDlyEntryDevolution
        Me.INDtxtSupplier.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtSupplier, 0)
        '
        'INDseTerm
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDseTerm, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDseTerm, False)
        Me.INDseTerm.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDseTerm.EnterMoveNextControl = True
        Me.INDseTerm.Location = New System.Drawing.Point(434, 309)
        Me.IndigoTextEdit1.SetMascara(Me.INDseTerm, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDseTerm.Name = "INDseTerm"
        Me.INDseTerm.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDseTerm.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseTerm.Properties.Appearance.Options.UseBackColor = True
        Me.INDseTerm.Properties.Appearance.Options.UseFont = True
        Me.INDseTerm.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseTerm.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDseTerm.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDseTerm.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.INDseTerm.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.INDseTerm.Properties.ReadOnly = True
        Me.INDseTerm.Size = New System.Drawing.Size(386, 28)
        Me.INDseTerm.StyleController = Me.INDlyEntryDevolution
        Me.INDseTerm.TabIndex = 7
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDseTerm, 0)
        '
        'INDgcObligation
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcObligation, Nothing)
        Me.IndigoGridControl11.SetAddActions(Me.INDgcObligation, Nothing)
        Me.IndigoGridControl11.SetControlNextFocus(Me.INDgcObligation, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcObligation, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcObligation, False)
        Me.IndigoGridControl11.SetExportButton(Me.INDgcObligation, False)
        Me.IndigoGridControl11.SetGuardarXml(Me.INDgcObligation, True)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcObligation, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcObligation, False)
        Me.IndigoGridControl11.SetHoldSize(Me.INDgcObligation, False)
        Me.IndigoGridControl11.SetHotTrack(Me.INDgcObligation, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcObligation, False)
        Me.INDgcObligation.Location = New System.Drawing.Point(2108, 49)
        Me.INDgcObligation.MainView = Me.INDviewObligation
        Me.INDgcObligation.Name = "INDgcObligation"
        Me.INDgcObligation.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrepTxtValue})
        Me.INDgcObligation.Size = New System.Drawing.Size(824, 469)
        Me.IndigoGridControl11.SetSizeConstraintsType(Me.INDgcObligation, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcObligation, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcObligation.TabIndex = 30
        Me.INDgcObligation.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDviewObligation})
        '
        'INDviewObligation
        '
        Me.INDviewObligation.Appearance.Empty.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
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
        Me.INDviewObligation.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn11, Me.GridColumn21, Me.GridColumn31, Me.GridColumn41, Me.GridColumn51, Me.GridColumn61, Me.GridColumn71})
        Me.INDviewObligation.GridControl = Me.INDgcObligation
        Me.INDviewObligation.Name = "INDviewObligation"
        Me.INDviewObligation.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewObligation.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewObligation.OptionsView.ShowAutoFilterRow = True
        Me.INDviewObligation.OptionsView.ShowDetailButtons = False
        Me.INDviewObligation.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView11.SetTemaIndigoMetro(Me.INDviewObligation, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewObligation, False)
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "Código"
        Me.GridColumn11.FieldName = "ObligationCode"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.OptionsColumn.AllowEdit = False
        Me.GridColumn11.OptionsColumn.AllowFocus = False
        Me.GridColumn11.Visible = True
        Me.GridColumn11.VisibleIndex = 0
        '
        'GridColumn21
        '
        Me.GridColumn21.Caption = "Documento"
        Me.GridColumn21.FieldName = "ObligationDocument"
        Me.GridColumn21.Name = "GridColumn21"
        Me.GridColumn21.OptionsColumn.AllowEdit = False
        Me.GridColumn21.OptionsColumn.AllowFocus = False
        Me.GridColumn21.Visible = True
        Me.GridColumn21.VisibleIndex = 1
        '
        'GridColumn31
        '
        Me.GridColumn31.Caption = "Rubro"
        Me.GridColumn31.FieldName = "CategoryName"
        Me.GridColumn31.Name = "GridColumn31"
        Me.GridColumn31.OptionsColumn.AllowEdit = False
        Me.GridColumn31.OptionsColumn.AllowFocus = False
        Me.GridColumn31.Visible = True
        Me.GridColumn31.VisibleIndex = 2
        '
        'GridColumn41
        '
        Me.GridColumn41.Caption = "Recurso"
        Me.GridColumn41.FieldName = "FinancialSourceDescription"
        Me.GridColumn41.Name = "GridColumn41"
        Me.GridColumn41.OptionsColumn.AllowEdit = False
        Me.GridColumn41.OptionsColumn.AllowFocus = False
        Me.GridColumn41.Visible = True
        Me.GridColumn41.VisibleIndex = 3
        '
        'GridColumn51
        '
        Me.GridColumn51.Caption = "Tipo"
        Me.GridColumn51.FieldName = "RevenueTypeDescription"
        Me.GridColumn51.Name = "GridColumn51"
        Me.GridColumn51.OptionsColumn.AllowEdit = False
        Me.GridColumn51.OptionsColumn.AllowFocus = False
        Me.GridColumn51.Visible = True
        Me.GridColumn51.VisibleIndex = 4
        '
        'GridColumn61
        '
        Me.GridColumn61.Caption = "Saldo"
        Me.GridColumn61.DisplayFormat.FormatString = "C0"
        Me.GridColumn61.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn61.FieldName = "ObligationBalance"
        Me.GridColumn61.Name = "GridColumn61"
        Me.GridColumn61.OptionsColumn.AllowEdit = False
        Me.GridColumn61.OptionsColumn.AllowFocus = False
        Me.GridColumn61.Visible = True
        Me.GridColumn61.VisibleIndex = 5
        '
        'GridColumn71
        '
        Me.GridColumn71.Caption = "Valor"
        Me.GridColumn71.ColumnEdit = Me.INDrepTxtValue
        Me.GridColumn71.DisplayFormat.FormatString = "C0"
        Me.GridColumn71.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn71.FieldName = "Value"
        Me.GridColumn71.Name = "GridColumn71"
        Me.GridColumn71.Visible = True
        Me.GridColumn71.VisibleIndex = 6
        '
        'INDrepTxtValue
        '
        Me.INDrepTxtValue.AutoHeight = False
        Me.INDrepTxtValue.Mask.EditMask = "C0"
        Me.INDrepTxtValue.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDrepTxtValue.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepTxtValue.Name = "INDrepTxtValue"
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlygPrincipalInformation, Me.INDlygEntryData, Me.INDlygFreigth, Me.INDlygItem, Me.INDlygBudget})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(2954, 538)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlygPrincipalInformation
        '
        Me.INDlygPrincipalInformation.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygPrincipalInformation.AppearanceGroup.Options.UseFont = True
        Me.INDlygPrincipalInformation.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygPrincipalInformation.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygPrincipalInformation.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygPrincipalInformation.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygPrincipalInformation.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygPrincipalInformation.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygPrincipalInformation.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygPrincipalInformation.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygPrincipalInformation.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygPrincipalInformation.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygPrincipalInformation.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygPrincipalInformation.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygPrincipalInformation, False)
        Me.INDlygPrincipalInformation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemCode, Me.INDlyItemDocumentDate, Me.INDlyItemDescription})
        Me.INDlygPrincipalInformation.Location = New System.Drawing.Point(0, 0)
        Me.INDlygPrincipalInformation.Name = "INDlygPrincipalInformation"
        Me.INDlygPrincipalInformation.Size = New System.Drawing.Size(412, 522)
        Me.INDlygPrincipalInformation.Text = "Información Principal"
        '
        'INDlyItemCode
        '
        Me.INDlyItemCode.Control = Me.INDbtnCode
        Me.INDlyItemCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCode.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemCode.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemCode.Name = "INDlyItemCode"
        Me.INDlyItemCode.ShowInCustomizationForm = False
        Me.INDlyItemCode.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCode.Text = "Código"
        Me.INDlyItemCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemCode.TextSize = New System.Drawing.Size(116, 17)
        '
        'INDlyItemDocumentDate
        '
        Me.INDlyItemDocumentDate.Control = Me.INDdteDocumentDate
        Me.INDlyItemDocumentDate.Location = New System.Drawing.Point(0, 60)
        Me.INDlyItemDocumentDate.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemDocumentDate.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemDocumentDate.Name = "INDlyItemDocumentDate"
        Me.INDlyItemDocumentDate.ShowInCustomizationForm = False
        Me.INDlyItemDocumentDate.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemDocumentDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemDocumentDate.Text = "Fecha Documento"
        Me.INDlyItemDocumentDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemDocumentDate.TextSize = New System.Drawing.Size(116, 17)
        '
        'INDlyItemDescription
        '
        Me.INDlyItemDescription.Control = Me.INDmemoDescription
        Me.INDlyItemDescription.Location = New System.Drawing.Point(0, 120)
        Me.INDlyItemDescription.MaxSize = New System.Drawing.Size(390, 120)
        Me.INDlyItemDescription.MinSize = New System.Drawing.Size(390, 120)
        Me.INDlyItemDescription.Name = "INDlyItemDescription"
        Me.INDlyItemDescription.ShowInCustomizationForm = False
        Me.INDlyItemDescription.Size = New System.Drawing.Size(390, 353)
        Me.INDlyItemDescription.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemDescription.Text = "Detalle"
        Me.INDlyItemDescription.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemDescription.TextSize = New System.Drawing.Size(116, 17)
        '
        'INDlygEntryData
        '
        Me.INDlygEntryData.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygEntryData.AppearanceGroup.Options.UseFont = True
        Me.INDlygEntryData.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygEntryData.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygEntryData.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygEntryData.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygEntryData.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygEntryData.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygEntryData.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygEntryData.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygEntryData.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygEntryData.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygEntryData.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygEntryData.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygEntryData, False)
        Me.INDlygEntryData.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemFixedAssetEntry, Me.INDlyItemBillNumber, Me.INDlyItemBillDate, Me.INDlyItemSupplier, Me.INDlyItemTerm})
        Me.INDlygEntryData.Location = New System.Drawing.Point(412, 0)
        Me.INDlygEntryData.Name = "INDlygEntryData"
        Me.INDlygEntryData.Size = New System.Drawing.Size(412, 522)
        Me.INDlygEntryData.Text = "Datos Ingreso"
        '
        'INDlyItemFixedAssetEntry
        '
        Me.INDlyItemFixedAssetEntry.Control = Me.INDsleFixedAssetEntry
        Me.INDlyItemFixedAssetEntry.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemFixedAssetEntry.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemFixedAssetEntry.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemFixedAssetEntry.Name = "INDlyItemFixedAssetEntry"
        Me.INDlyItemFixedAssetEntry.ShowInCustomizationForm = False
        Me.INDlyItemFixedAssetEntry.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemFixedAssetEntry.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemFixedAssetEntry.Text = "Ingreso"
        Me.INDlyItemFixedAssetEntry.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemFixedAssetEntry.TextSize = New System.Drawing.Size(116, 17)
        '
        'INDlyItemBillNumber
        '
        Me.INDlyItemBillNumber.Control = Me.INDtxtBillNumber
        Me.INDlyItemBillNumber.Location = New System.Drawing.Point(0, 60)
        Me.INDlyItemBillNumber.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemBillNumber.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemBillNumber.Name = "INDlyItemBillNumber"
        Me.INDlyItemBillNumber.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemBillNumber.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemBillNumber.Text = "Factura"
        Me.INDlyItemBillNumber.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemBillNumber.TextSize = New System.Drawing.Size(116, 17)
        '
        'INDlyItemBillDate
        '
        Me.INDlyItemBillDate.Control = Me.INDdteBillDate
        Me.INDlyItemBillDate.Location = New System.Drawing.Point(0, 120)
        Me.INDlyItemBillDate.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemBillDate.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemBillDate.Name = "INDlyItemBillDate"
        Me.INDlyItemBillDate.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemBillDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemBillDate.Text = "Fecha Factura"
        Me.INDlyItemBillDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemBillDate.TextSize = New System.Drawing.Size(116, 17)
        '
        'INDlyItemSupplier
        '
        Me.INDlyItemSupplier.Control = Me.INDtxtSupplier
        Me.INDlyItemSupplier.Location = New System.Drawing.Point(0, 180)
        Me.INDlyItemSupplier.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemSupplier.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemSupplier.Name = "INDlyItemSupplier"
        Me.INDlyItemSupplier.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemSupplier.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemSupplier.Text = "Proveedor"
        Me.INDlyItemSupplier.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemSupplier.TextSize = New System.Drawing.Size(116, 17)
        '
        'INDlyItemTerm
        '
        Me.INDlyItemTerm.Control = Me.INDseTerm
        Me.INDlyItemTerm.Location = New System.Drawing.Point(0, 240)
        Me.INDlyItemTerm.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemTerm.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemTerm.Name = "INDlyItemTerm"
        Me.INDlyItemTerm.Size = New System.Drawing.Size(390, 233)
        Me.INDlyItemTerm.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemTerm.Text = "Plazo"
        Me.INDlyItemTerm.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemTerm.TextSize = New System.Drawing.Size(116, 17)
        '
        'INDlygFreigth
        '
        Me.INDlygFreigth.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygFreigth.AppearanceGroup.Options.UseFont = True
        Me.INDlygFreigth.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygFreigth.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygFreigth.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygFreigth.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygFreigth.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygFreigth.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygFreigth.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygFreigth.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygFreigth.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygFreigth.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygFreigth.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygFreigth.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygFreigth, False)
        Me.INDlygFreigth.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemFreightValue, Me.INDlyItemFreightValueEntry, Me.INDlyItemFreightIVAPercentage, Me.INDlyItemFreightIVAValue})
        Me.INDlygFreigth.Location = New System.Drawing.Point(824, 0)
        Me.INDlygFreigth.Name = "INDlygFreigth"
        Me.INDlygFreigth.Size = New System.Drawing.Size(412, 522)
        Me.INDlygFreigth.Text = "Fletes"
        Me.INDlygFreigth.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlyItemFreightValue
        '
        Me.INDlyItemFreightValue.Control = Me.INDtxtFreightValue
        Me.INDlyItemFreightValue.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemFreightValue.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemFreightValue.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemFreightValue.Name = "INDlyItemFreightValue"
        Me.INDlyItemFreightValue.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemFreightValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemFreightValue.Text = "Valor Flete"
        Me.INDlyItemFreightValue.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemFreightValue.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemFreightValue.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemFreightValue.TextToControlDistance = 5
        '
        'INDlyItemFreightValueEntry
        '
        Me.INDlyItemFreightValueEntry.Control = Me.INDtxtFreightValueEntry
        Me.INDlyItemFreightValueEntry.Location = New System.Drawing.Point(0, 60)
        Me.INDlyItemFreightValueEntry.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemFreightValueEntry.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemFreightValueEntry.Name = "INDlyItemFreightValueEntry"
        Me.INDlyItemFreightValueEntry.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemFreightValueEntry.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemFreightValueEntry.Text = "Fletes Ingreso"
        Me.INDlyItemFreightValueEntry.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemFreightValueEntry.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemFreightValueEntry.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemFreightValueEntry.TextToControlDistance = 5
        '
        'INDlyItemFreightIVAPercentage
        '
        Me.INDlyItemFreightIVAPercentage.Control = Me.INDseFreightIVAPercentage
        Me.INDlyItemFreightIVAPercentage.Location = New System.Drawing.Point(0, 120)
        Me.INDlyItemFreightIVAPercentage.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemFreightIVAPercentage.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemFreightIVAPercentage.Name = "INDlyItemFreightIVAPercentage"
        Me.INDlyItemFreightIVAPercentage.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemFreightIVAPercentage.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemFreightIVAPercentage.Text = "% IVA Fletes"
        Me.INDlyItemFreightIVAPercentage.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemFreightIVAPercentage.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemFreightIVAPercentage.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemFreightIVAPercentage.TextToControlDistance = 5
        '
        'INDlyItemFreightIVAValue
        '
        Me.INDlyItemFreightIVAValue.Control = Me.INDtxtFreightIVAValue
        Me.INDlyItemFreightIVAValue.Location = New System.Drawing.Point(0, 180)
        Me.INDlyItemFreightIVAValue.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemFreightIVAValue.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemFreightIVAValue.Name = "INDlyItemFreightIVAValue"
        Me.INDlyItemFreightIVAValue.Size = New System.Drawing.Size(390, 293)
        Me.INDlyItemFreightIVAValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemFreightIVAValue.Text = "IVA Fletes"
        Me.INDlyItemFreightIVAValue.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemFreightIVAValue.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemFreightIVAValue.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemFreightIVAValue.TextToControlDistance = 5
        '
        'INDlygItem
        '
        Me.INDlygItem.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygItem.AppearanceGroup.Options.UseFont = True
        Me.INDlygItem.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygItem.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygItem.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygItem.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygItem.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygItem.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygItem.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygItem.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygItem.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygItem.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygItem.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygItem.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygItem, False)
        Me.INDlygItem.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemItem})
        Me.INDlygItem.Location = New System.Drawing.Point(1236, 0)
        Me.INDlygItem.Name = "INDlygItem"
        Me.INDlygItem.Size = New System.Drawing.Size(850, 522)
        Me.INDlygItem.Text = "Articulos"
        '
        'INDlyItemItem
        '
        Me.INDlyItemItem.Control = Me.INDgcItem
        Me.INDlyItemItem.CustomizationFormText = "Listado de Artículos"
        Me.INDlyItemItem.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemItem.MaxSize = New System.Drawing.Size(828, 0)
        Me.INDlyItemItem.MinSize = New System.Drawing.Size(828, 24)
        Me.INDlyItemItem.Name = "INDlyItemItem"
        Me.INDlyItemItem.Size = New System.Drawing.Size(828, 473)
        Me.INDlyItemItem.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemItem.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemItem.TextVisible = False
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
        Me.INDlygBudget.CustomizationFormText = "Interfaz Presupuesto"
        Me.INDlygBudget.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemGridObligation})
        Me.INDlygBudget.Location = New System.Drawing.Point(2086, 0)
        Me.INDlygBudget.Name = "INDlygBudget"
        Me.INDlygBudget.Size = New System.Drawing.Size(850, 522)
        Me.INDlygBudget.Text = "Interfaz Presupuesto"
        Me.INDlygBudget.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlyItemGridObligation
        '
        Me.INDlyItemGridObligation.Control = Me.INDgcObligation
        Me.INDlyItemGridObligation.CustomizationFormText = "INDlyItemGridObligation"
        Me.INDlyItemGridObligation.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemGridObligation.MaxSize = New System.Drawing.Size(828, 0)
        Me.INDlyItemGridObligation.MinSize = New System.Drawing.Size(828, 24)
        Me.INDlyItemGridObligation.Name = "INDlyItemGridObligation"
        Me.INDlyItemGridObligation.Size = New System.Drawing.Size(828, 473)
        Me.INDlyItemGridObligation.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemGridObligation.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemGridObligation.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit2
        '
        'IndigoGridView11
        '
        Me.IndigoGridView11.RaiseMenuPopUp = True
        Me.IndigoGridView11.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'FrmFixedAssetEntryDevolution
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1465, 701)
        Me.IconOptions.ShowIcon = False
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmFixedAssetEntryDevolution"
        Me.Opacity = 1.0R
        Me.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.Tag = "1119"
        Me.Text = "Devolución Ingreso de Activos"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyEntryDevolution, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyEntryDevolution.ResumeLayout(False)
        CType(Me.PopUpSummarySettlement, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PopUpSummarySettlement.ResumeLayout(False)
        CType(Me.LySummarySettlement, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LySummarySettlement.ResumeLayout(False)
        CType(Me.INDPopTxtDistricTaxes.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPopTxtDeductionOther.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPopTxtRetentionOther.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPopTxtRetentionSource.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPopSpnFreightIVA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPopTxtWithholdingICA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPopTxtWithholdingTax.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPopTxtFreightValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPopTxtValueTax.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPopTxtTotalCxp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPopTxtDiscountValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPopTxtValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyPopTxtValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyPopTxtValueTax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyPopTxtFreightValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyPopSpnFreightIVA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyPopTxtTotalCxp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyPopTxtDistricTaxes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyPopTxtDeductionOther, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyPopTxtRetentionOther, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyPopTxtWithholdingTax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyPopTxtDiscountValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyPopTxtWithholdingICA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyPopTxtRetentionSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtFreightIVAValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDseFreightIVAPercentage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtFreightValueEntry.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtFreightValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepCheckSelectOption, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteBillDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteBillDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtBillNumber.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleFixedAssetEntry.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewEntry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDmemoDescription.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteDocumentDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteDocumentDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtSupplier.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDseTerm.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcObligation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewObligation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepTxtValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygPrincipalInformation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDocumentDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygEntryData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemFixedAssetEntry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemBillNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemBillDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemSupplier, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemTerm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygFreigth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemFreightValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemFreightValueEntry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemFreightIVAPercentage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemFreightIVAValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygBudget, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemGridObligation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView11, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControlPanel1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDlyEntryDevolution As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDdteBillDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDtxtBillNumber As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDsleFixedAssetEntry As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDviewEntry As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDmemoDescription As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDdteDocumentDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDbtnCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDlygPrincipalInformation As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemDocumentDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemDescription As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlygEntryData As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemFixedAssetEntry As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemBillNumber As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemBillDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemSupplier As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemTerm As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDgcItem As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDviewItem As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDGcProduct As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlygItem As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtSupplier As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDseTerm As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepCheckSelectOption As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents INDtxtFreightValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlygFreigth As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemFreightValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtFreightValueEntry As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemFreightValueEntry As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDseFreightIVAPercentage As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDlyItemFreightIVAPercentage As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtFreightIVAValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemFreightIVAValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents PopUpSummarySettlement As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents LySummarySettlement As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDPopTxtDistricTaxes As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDPopTxtDeductionOther As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDPopTxtRetentionOther As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDPopTxtRetentionSource As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDPopSpnFreightIVA As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDPopTxtWithholdingICA As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDPopTxtWithholdingTax As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDPopTxtFreightValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDPopTxtValueTax As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDPopTxtTotalCxp As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDPopTxtDiscountValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDPopTxtValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLyPopTxtValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyPopTxtValueTax As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyPopTxtFreightValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyPopSpnFreightIVA As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents INDLyPopTxtTotalCxp As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyPopTxtDistricTaxes As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyPopTxtDeductionOther As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyPopTxtRetentionOther As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyPopTxtRetentionSource As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyPopTxtWithholdingICA As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyPopTxtWithholdingTax As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyPopTxtDiscountValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridControl11 As IndigoGridControl
    Friend WithEvents IndigoGridView11 As IndigoGridView
    Friend WithEvents INDgcObligation As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDviewObligation As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn21 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn31 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn41 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn51 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn61 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn71 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepTxtValue As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents INDlygBudget As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemGridObligation As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
End Class
