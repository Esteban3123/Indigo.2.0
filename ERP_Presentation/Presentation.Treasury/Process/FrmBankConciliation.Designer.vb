Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmBankConciliation
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
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim AppearanceObject5 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject6 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim EditorButtonImageOptions2 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject5 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject6 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject7 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject8 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlyRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDpccExtractDetails = New DevExpress.XtraEditors.PopupContainerControl()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDtxtExtractDocumentValue = New DevExpress.XtraEditors.TextEdit()
        Me.INDSleExtractNature = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDgvExtractNature = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDcolExtractNature = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDtxtExtractDocument = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtExtractDetail = New DevExpress.XtraEditors.TextEdit()
        Me.INDdeExtractDocumentDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDSleExtractDocumentType = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDgvExtractDocumentType = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDcolExtractDocumentType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciExtractDocumentType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciExtractDocumentDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciExtractDetail = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciExtractDocument = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciExtractNature = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciExtractDocumentValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.INDbtnAddExtractDetail = New DevExpress.XtraEditors.SimpleButton()
        Me.INDPceAddExtractDetail = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.INDgcBankReconciliationExtractDetails = New DevExpress.XtraGrid.GridControl()
        Me.INDgvBankReconciliationExtractDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDcolBankReconciliationExtractDetail_DocumentType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepSleExtractDetailDocumentType = New DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit()
        Me.INDrepGvExtractDetailDocumentType = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDrepColExtractDetailDocumentType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolBankReconciliationExtractDetail_DocumentDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolBankReconciliationExtractDetail_Description = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolBankReconciliationExtractDetail_DocumentNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolBankReconciliationExtractDetail_Nature = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepSleExtractDetailNature = New DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit()
        Me.INDrepGvExtractDetailNature = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDrepColExtractDetailNature = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolBankReconciliationExtractDetail_Value = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepTxtExtractDetailValue = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.INDgcBankReconciliationDetails = New DevExpress.XtraGrid.GridControl()
        Me.INDgvBankReconciliationDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDcolBankReconciliationDetail_Select = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepCheckSelectOption = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.INDcolBankReconciliationDetail_Code = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolBankReconciliationDetail_DocumenType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepSleDetailDocumentType = New DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit()
        Me.INDrepGvDetailDocumentType = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDcolBankReconciliationDetail_DocumentDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolBankReconciliationDetail_ThirdPartyNitName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolBankReconciliationDetail_DocumentNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolBankReconciliationDetail_Nature = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepSleDetailNature = New DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit()
        Me.INDrepGvDetailNature = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDcolBankReconciliationDetail_Value = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolBankReconciliationDetail_Observations = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolBankReconciliationDetail_CreationUser = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolBankReconciliationDetail_ConfirmationUser = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDseDifferenceReconcile = New DevExpress.XtraEditors.SpinEdit()
        Me.INDseEndEntityBankAccountValue = New DevExpress.XtraEditors.SpinEdit()
        Me.INDseExtractValue = New DevExpress.XtraEditors.SpinEdit()
        Me.INDdteDocumentDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDsleEntityBankAccount = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDbtnCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlygPrincipalInformation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciEntityBankAccount = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciDocumentDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciEntityBankAccountValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciDifferenceReconcile = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciExtractValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygEntityBankAccountInformation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciBankReconciliationDetails = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygExtractInformation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciAddExtractDetail = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyRoot.SuspendLayout()
        CType(Me.INDpccExtractDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpccExtractDetails.SuspendLayout()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDtxtExtractDocumentValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleExtractNature.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvExtractNature, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtExtractDocument.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtExtractDetail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdeExtractDocumentDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdeExtractDocumentDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleExtractDocumentType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvExtractDocumentType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciExtractDocumentType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciExtractDocumentDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciExtractDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciExtractDocument, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciExtractNature, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciExtractDocumentValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.INDPceAddExtractDetail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcBankReconciliationExtractDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvBankReconciliationExtractDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepSleExtractDetailDocumentType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepGvExtractDetailDocumentType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepSleExtractDetailNature, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepGvExtractDetailNature, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepTxtExtractDetailValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcBankReconciliationDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvBankReconciliationDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepCheckSelectOption, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepSleDetailDocumentType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepGvDetailDocumentType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepSleDetailNature, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepGvDetailNature, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDseDifferenceReconcile.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDseEndEntityBankAccountValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDseExtractValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteDocumentDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteDocumentDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleEntityBankAccount.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygPrincipalInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciEntityBankAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDocumentDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciEntityBankAccountValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDifferenceReconcile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciExtractValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygEntityBankAccountInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciBankReconciliationDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygExtractInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciAddExtractDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyRoot)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 137)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1709, 726)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Location = New System.Drawing.Point(0, 7)
        Me.ToolBars.Size = New System.Drawing.Size(1709, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.BarraBotones.Size = New System.Drawing.Size(1709, 130)
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
        Me.CtrNavigationControlPanel1.LayoutControl = Me.INDlyRoot
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 8)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 716)
        Me.CtrNavigationControlPanel1.TabIndex = 0
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'INDlyRoot
        '
        Me.INDlyRoot.Controls.Add(Me.INDpccExtractDetails)
        Me.INDlyRoot.Controls.Add(Me.INDPceAddExtractDetail)
        Me.INDlyRoot.Controls.Add(Me.INDgcBankReconciliationExtractDetails)
        Me.INDlyRoot.Controls.Add(Me.INDgcBankReconciliationDetails)
        Me.INDlyRoot.Controls.Add(Me.INDseDifferenceReconcile)
        Me.INDlyRoot.Controls.Add(Me.INDseEndEntityBankAccountValue)
        Me.INDlyRoot.Controls.Add(Me.INDseExtractValue)
        Me.INDlyRoot.Controls.Add(Me.INDdteDocumentDate)
        Me.INDlyRoot.Controls.Add(Me.INDsleEntityBankAccount)
        Me.INDlyRoot.Controls.Add(Me.INDbtnCode)
        Me.INDlyRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyRoot.Location = New System.Drawing.Point(202, 8)
        Me.INDlyRoot.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDlyRoot.Name = "INDlyRoot"
        Me.INDlyRoot.Root = Me.LayoutControlGroup1
        Me.INDlyRoot.Size = New System.Drawing.Size(1505, 716)
        Me.INDlyRoot.TabIndex = 1
        Me.INDlyRoot.Text = "LayoutControl1"
        '
        'INDpccExtractDetails
        '
        Me.INDpccExtractDetails.Controls.Add(Me.LayoutControl1)
        Me.INDpccExtractDetails.Controls.Add(Me.PanelControl1)
        Me.INDpccExtractDetails.Location = New System.Drawing.Point(989, 103)
        Me.INDpccExtractDetails.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDpccExtractDetails.Name = "INDpccExtractDetails"
        Me.INDpccExtractDetails.Size = New System.Drawing.Size(443, 542)
        Me.INDpccExtractDetails.TabIndex = 9
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDtxtExtractDocumentValue)
        Me.LayoutControl1.Controls.Add(Me.INDSleExtractNature)
        Me.LayoutControl1.Controls.Add(Me.INDtxtExtractDocument)
        Me.LayoutControl1.Controls.Add(Me.INDtxtExtractDetail)
        Me.LayoutControl1.Controls.Add(Me.INDdeExtractDocumentDate)
        Me.LayoutControl1.Controls.Add(Me.INDSleExtractDocumentType)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.Root
        Me.LayoutControl1.Size = New System.Drawing.Size(443, 498)
        Me.LayoutControl1.TabIndex = 1
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDtxtExtractDocumentValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtExtractDocumentValue, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtExtractDocumentValue, False)
        Me.INDtxtExtractDocumentValue.EnterMoveNextControl = True
        Me.INDtxtExtractDocumentValue.Location = New System.Drawing.Point(12, 438)
        Me.INDtxtExtractDocumentValue.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtExtractDocumentValue, Presentation.Controls.IndigoTextEdit.EMask.MonedaDecimales)
        Me.INDtxtExtractDocumentValue.Name = "INDtxtExtractDocumentValue"
        Me.INDtxtExtractDocumentValue.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtExtractDocumentValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtExtractDocumentValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtExtractDocumentValue.Properties.Appearance.Options.UseFont = True
        Me.INDtxtExtractDocumentValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtExtractDocumentValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtExtractDocumentValue.Properties.Mask.EditMask = "c2"
        Me.INDtxtExtractDocumentValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtExtractDocumentValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtExtractDocumentValue.Properties.MaxLength = 20
        Me.INDtxtExtractDocumentValue.Size = New System.Drawing.Size(416, 34)
        Me.INDtxtExtractDocumentValue.StyleController = Me.LayoutControl1
        Me.INDtxtExtractDocumentValue.TabIndex = 9
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtExtractDocumentValue, 0)
        '
        'INDSleExtractNature
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleExtractNature, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleExtractNature, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleExtractNature, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleExtractNature, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleExtractNature, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleExtractNature, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleExtractNature, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleExtractNature, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleExtractNature, False)
        Me.INDSleExtractNature.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleExtractNature, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleExtractNature, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleExtractNature, False)
        Me.INDSleExtractNature.Location = New System.Drawing.Point(12, 359)
        Me.INDSleExtractNature.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleExtractNature, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleExtractNature.Name = "INDSleExtractNature"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleExtractNature, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleExtractNature, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleExtractNature, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleExtractNature, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleExtractNature, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleExtractNature, False)
        Me.INDSleExtractNature.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleExtractNature.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleExtractNature.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleExtractNature.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleExtractNature.Properties.Appearance.Options.UseFont = True
        Me.INDSleExtractNature.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleExtractNature.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleExtractNature.Properties.DisplayMember = "Item2"
        Me.INDSleExtractNature.Properties.NullText = ""
        Me.INDSleExtractNature.Properties.PopupSizeable = False
        Me.INDSleExtractNature.Properties.PopupView = Me.INDgvExtractNature
        Me.INDSleExtractNature.Properties.ShowFooter = False
        Me.INDSleExtractNature.Properties.ValueMember = "Item1"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleExtractNature, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleExtractNature, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleExtractNature, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleExtractNature, True)
        Me.INDSleExtractNature.Size = New System.Drawing.Size(416, 34)
        Me.INDSleExtractNature.StyleController = Me.LayoutControl1
        Me.INDSleExtractNature.TabIndex = 8
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleExtractNature, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleExtractNature, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleExtractNature, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleExtractNature, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleExtractNature, False)
        '
        'INDgvExtractNature
        '
        Me.INDgvExtractNature.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvExtractNature.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvExtractNature.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvExtractNature.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvExtractNature.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvExtractNature.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvExtractNature.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvExtractNature.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvExtractNature.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvExtractNature.Appearance.Row.Options.UseFont = True
        Me.INDgvExtractNature.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDcolExtractNature})
        Me.INDgvExtractNature.DetailHeight = 431
        Me.INDgvExtractNature.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDgvExtractNature.Name = "INDgvExtractNature"
        Me.INDgvExtractNature.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDgvExtractNature.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvExtractNature.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvExtractNature.OptionsView.ShowAutoFilterRow = True
        Me.INDgvExtractNature.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvExtractNature, False)
        '
        'INDcolExtractNature
        '
        Me.INDcolExtractNature.Caption = "Descripción"
        Me.INDcolExtractNature.FieldName = "Item2"
        Me.INDcolExtractNature.MinWidth = 23
        Me.INDcolExtractNature.Name = "INDcolExtractNature"
        Me.INDcolExtractNature.Visible = True
        Me.INDcolExtractNature.VisibleIndex = 0
        Me.INDcolExtractNature.Width = 87
        '
        'INDtxtExtractDocument
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtExtractDocument, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtExtractDocument, False)
        Me.INDtxtExtractDocument.EnterMoveNextControl = True
        Me.INDtxtExtractDocument.Location = New System.Drawing.Point(12, 280)
        Me.INDtxtExtractDocument.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtExtractDocument, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtExtractDocument.Name = "INDtxtExtractDocument"
        Me.INDtxtExtractDocument.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtExtractDocument.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtExtractDocument.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtExtractDocument.Properties.Appearance.Options.UseFont = True
        Me.INDtxtExtractDocument.Size = New System.Drawing.Size(416, 34)
        Me.INDtxtExtractDocument.StyleController = Me.LayoutControl1
        Me.INDtxtExtractDocument.TabIndex = 7
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtExtractDocument, 0)
        '
        'INDtxtExtractDetail
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtExtractDetail, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtExtractDetail, False)
        Me.INDtxtExtractDetail.EnterMoveNextControl = True
        Me.INDtxtExtractDetail.Location = New System.Drawing.Point(12, 201)
        Me.INDtxtExtractDetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtExtractDetail, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtExtractDetail.Name = "INDtxtExtractDetail"
        Me.INDtxtExtractDetail.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtExtractDetail.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtExtractDetail.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtExtractDetail.Properties.Appearance.Options.UseFont = True
        Me.INDtxtExtractDetail.Size = New System.Drawing.Size(416, 34)
        Me.INDtxtExtractDetail.StyleController = Me.LayoutControl1
        Me.INDtxtExtractDetail.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtExtractDetail, 0)
        '
        'INDdeExtractDocumentDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdeExtractDocumentDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdeExtractDocumentDate, False)
        Me.INDdeExtractDocumentDate.EditValue = Nothing
        Me.INDdeExtractDocumentDate.EnterMoveNextControl = True
        Me.INDdeExtractDocumentDate.Location = New System.Drawing.Point(12, 122)
        Me.INDdeExtractDocumentDate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.INDdeExtractDocumentDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDdeExtractDocumentDate.Name = "INDdeExtractDocumentDate"
        Me.INDdeExtractDocumentDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDdeExtractDocumentDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdeExtractDocumentDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDdeExtractDocumentDate.Properties.Appearance.Options.UseFont = True
        Me.INDdeExtractDocumentDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeExtractDocumentDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeExtractDocumentDate.Size = New System.Drawing.Size(416, 34)
        Me.INDdeExtractDocumentDate.StyleController = Me.LayoutControl1
        Me.INDdeExtractDocumentDate.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdeExtractDocumentDate, 0)
        '
        'INDSleExtractDocumentType
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleExtractDocumentType, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleExtractDocumentType, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleExtractDocumentType, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleExtractDocumentType, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleExtractDocumentType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleExtractDocumentType, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleExtractDocumentType, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleExtractDocumentType, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleExtractDocumentType, False)
        Me.INDSleExtractDocumentType.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleExtractDocumentType, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleExtractDocumentType, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleExtractDocumentType, False)
        Me.INDSleExtractDocumentType.Location = New System.Drawing.Point(12, 43)
        Me.INDSleExtractDocumentType.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleExtractDocumentType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleExtractDocumentType.Name = "INDSleExtractDocumentType"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleExtractDocumentType, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleExtractDocumentType, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleExtractDocumentType, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleExtractDocumentType, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleExtractDocumentType, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleExtractDocumentType, False)
        Me.INDSleExtractDocumentType.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleExtractDocumentType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleExtractDocumentType.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleExtractDocumentType.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleExtractDocumentType.Properties.Appearance.Options.UseFont = True
        Me.INDSleExtractDocumentType.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleExtractDocumentType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleExtractDocumentType.Properties.DisplayMember = "Item2"
        Me.INDSleExtractDocumentType.Properties.NullText = ""
        Me.INDSleExtractDocumentType.Properties.PopupSizeable = False
        Me.INDSleExtractDocumentType.Properties.PopupView = Me.INDgvExtractDocumentType
        Me.INDSleExtractDocumentType.Properties.ShowFooter = False
        Me.INDSleExtractDocumentType.Properties.ValueMember = "Item1"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleExtractDocumentType, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleExtractDocumentType, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleExtractDocumentType, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleExtractDocumentType, True)
        Me.INDSleExtractDocumentType.Size = New System.Drawing.Size(416, 34)
        Me.INDSleExtractDocumentType.StyleController = Me.LayoutControl1
        Me.INDSleExtractDocumentType.TabIndex = 4
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleExtractDocumentType, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleExtractDocumentType, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleExtractDocumentType, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleExtractDocumentType, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleExtractDocumentType, False)
        '
        'INDgvExtractDocumentType
        '
        Me.INDgvExtractDocumentType.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvExtractDocumentType.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvExtractDocumentType.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvExtractDocumentType.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvExtractDocumentType.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvExtractDocumentType.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvExtractDocumentType.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvExtractDocumentType.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvExtractDocumentType.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvExtractDocumentType.Appearance.Row.Options.UseFont = True
        Me.INDgvExtractDocumentType.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDcolExtractDocumentType})
        Me.INDgvExtractDocumentType.DetailHeight = 431
        Me.INDgvExtractDocumentType.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDgvExtractDocumentType.Name = "INDgvExtractDocumentType"
        Me.INDgvExtractDocumentType.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDgvExtractDocumentType.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvExtractDocumentType.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvExtractDocumentType.OptionsView.ShowAutoFilterRow = True
        Me.INDgvExtractDocumentType.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvExtractDocumentType, False)
        '
        'INDcolExtractDocumentType
        '
        Me.INDcolExtractDocumentType.Caption = "Descripción"
        Me.INDcolExtractDocumentType.FieldName = "Item2"
        Me.INDcolExtractDocumentType.MinWidth = 23
        Me.INDcolExtractDocumentType.Name = "INDcolExtractDocumentType"
        Me.INDcolExtractDocumentType.Visible = True
        Me.INDcolExtractDocumentType.VisibleIndex = 0
        Me.INDcolExtractDocumentType.Width = 87
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
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciExtractDocumentType, Me.INDLciExtractDocumentDate, Me.INDLciExtractDetail, Me.INDLciExtractDocument, Me.INDLciExtractNature, Me.INDLciExtractDocumentValue})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(443, 498)
        Me.Root.TextVisible = False
        '
        'INDLciExtractDocumentType
        '
        Me.INDLciExtractDocumentType.Control = Me.INDSleExtractDocumentType
        Me.INDLciExtractDocumentType.Location = New System.Drawing.Point(0, 0)
        Me.INDLciExtractDocumentType.MaxSize = New System.Drawing.Size(420, 79)
        Me.INDLciExtractDocumentType.MinSize = New System.Drawing.Size(420, 79)
        Me.INDLciExtractDocumentType.Name = "INDLciExtractDocumentType"
        Me.INDLciExtractDocumentType.Size = New System.Drawing.Size(423, 79)
        Me.INDLciExtractDocumentType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciExtractDocumentType.Text = "Tipo Documento"
        Me.INDLciExtractDocumentType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciExtractDocumentType.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciExtractDocumentType.TextSize = New System.Drawing.Size(157, 26)
        Me.INDLciExtractDocumentType.TextToControlDistance = 5
        '
        'INDLciExtractDocumentDate
        '
        Me.INDLciExtractDocumentDate.Control = Me.INDdeExtractDocumentDate
        Me.INDLciExtractDocumentDate.Location = New System.Drawing.Point(0, 79)
        Me.INDLciExtractDocumentDate.MaxSize = New System.Drawing.Size(420, 79)
        Me.INDLciExtractDocumentDate.MinSize = New System.Drawing.Size(420, 79)
        Me.INDLciExtractDocumentDate.Name = "INDLciExtractDocumentDate"
        Me.INDLciExtractDocumentDate.Size = New System.Drawing.Size(423, 79)
        Me.INDLciExtractDocumentDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciExtractDocumentDate.Text = "Fecha"
        Me.INDLciExtractDocumentDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciExtractDocumentDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciExtractDocumentDate.TextSize = New System.Drawing.Size(157, 26)
        Me.INDLciExtractDocumentDate.TextToControlDistance = 5
        '
        'INDLciExtractDetail
        '
        Me.INDLciExtractDetail.Control = Me.INDtxtExtractDetail
        Me.INDLciExtractDetail.Location = New System.Drawing.Point(0, 158)
        Me.INDLciExtractDetail.MaxSize = New System.Drawing.Size(420, 79)
        Me.INDLciExtractDetail.MinSize = New System.Drawing.Size(420, 79)
        Me.INDLciExtractDetail.Name = "INDLciExtractDetail"
        Me.INDLciExtractDetail.Size = New System.Drawing.Size(423, 79)
        Me.INDLciExtractDetail.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciExtractDetail.Text = "Detalle"
        Me.INDLciExtractDetail.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciExtractDetail.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciExtractDetail.TextSize = New System.Drawing.Size(157, 26)
        Me.INDLciExtractDetail.TextToControlDistance = 5
        '
        'INDLciExtractDocument
        '
        Me.INDLciExtractDocument.Control = Me.INDtxtExtractDocument
        Me.INDLciExtractDocument.Location = New System.Drawing.Point(0, 237)
        Me.INDLciExtractDocument.MaxSize = New System.Drawing.Size(420, 79)
        Me.INDLciExtractDocument.MinSize = New System.Drawing.Size(420, 79)
        Me.INDLciExtractDocument.Name = "INDLciExtractDocument"
        Me.INDLciExtractDocument.Size = New System.Drawing.Size(423, 79)
        Me.INDLciExtractDocument.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciExtractDocument.Text = "Documento"
        Me.INDLciExtractDocument.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciExtractDocument.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciExtractDocument.TextSize = New System.Drawing.Size(157, 26)
        Me.INDLciExtractDocument.TextToControlDistance = 5
        '
        'INDLciExtractNature
        '
        Me.INDLciExtractNature.Control = Me.INDSleExtractNature
        Me.INDLciExtractNature.Location = New System.Drawing.Point(0, 316)
        Me.INDLciExtractNature.MaxSize = New System.Drawing.Size(420, 79)
        Me.INDLciExtractNature.MinSize = New System.Drawing.Size(420, 79)
        Me.INDLciExtractNature.Name = "INDLciExtractNature"
        Me.INDLciExtractNature.Size = New System.Drawing.Size(423, 79)
        Me.INDLciExtractNature.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciExtractNature.Text = "Naturaleza"
        Me.INDLciExtractNature.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciExtractNature.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciExtractNature.TextSize = New System.Drawing.Size(157, 26)
        Me.INDLciExtractNature.TextToControlDistance = 5
        '
        'INDLciExtractDocumentValue
        '
        Me.INDLciExtractDocumentValue.Control = Me.INDtxtExtractDocumentValue
        Me.INDLciExtractDocumentValue.Location = New System.Drawing.Point(0, 395)
        Me.INDLciExtractDocumentValue.MaxSize = New System.Drawing.Size(420, 79)
        Me.INDLciExtractDocumentValue.MinSize = New System.Drawing.Size(420, 79)
        Me.INDLciExtractDocumentValue.Name = "INDLciExtractDocumentValue"
        Me.INDLciExtractDocumentValue.Size = New System.Drawing.Size(423, 83)
        Me.INDLciExtractDocumentValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciExtractDocumentValue.Text = "Valor"
        Me.INDLciExtractDocumentValue.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciExtractDocumentValue.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciExtractDocumentValue.TextSize = New System.Drawing.Size(157, 26)
        Me.INDLciExtractDocumentValue.TextToControlDistance = 5
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.INDbtnAddExtractDetail)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(0, 498)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(443, 44)
        Me.PanelControl1.TabIndex = 0
        '
        'INDbtnAddExtractDetail
        '
        Me.INDbtnAddExtractDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDbtnAddExtractDetail.Location = New System.Drawing.Point(2, 2)
        Me.INDbtnAddExtractDetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnAddExtractDetail, False)
        Me.INDbtnAddExtractDetail.Name = "INDbtnAddExtractDetail"
        Me.INDbtnAddExtractDetail.Size = New System.Drawing.Size(439, 40)
        Me.INDbtnAddExtractDetail.TabIndex = 0
        Me.INDbtnAddExtractDetail.Text = "Agregar"
        '
        'INDPceAddExtractDetail
        '
        Me.INDPceAddExtractDetail.Location = New System.Drawing.Point(1372, 59)
        Me.INDPceAddExtractDetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDPceAddExtractDetail.MaximumSize = New System.Drawing.Size(0, 39)
        Me.INDPceAddExtractDetail.MinimumSize = New System.Drawing.Size(0, 39)
        Me.INDPceAddExtractDetail.Name = "INDPceAddExtractDetail"
        Me.INDPceAddExtractDetail.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDPceAddExtractDetail.Properties.Appearance.Options.UseFont = True
        Me.INDPceAddExtractDetail.Properties.AutoHeight = False
        Me.INDPceAddExtractDetail.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        EditorButtonImageOptions1.Image = Global.Presentation.Treasury.My.Resources.Resources.Agregar16
        Me.INDPceAddExtractDetail.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDPceAddExtractDetail.Properties.CloseUpKey = New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None)
        Me.INDPceAddExtractDetail.Properties.PopupControl = Me.INDpccExtractDetails
        Me.INDPceAddExtractDetail.Properties.PopupSizeable = False
        Me.INDPceAddExtractDetail.Properties.ShowPopupCloseButton = False
        Me.INDPceAddExtractDetail.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        Me.INDPceAddExtractDetail.Size = New System.Drawing.Size(841, 39)
        Me.INDPceAddExtractDetail.StyleController = Me.INDlyRoot
        Me.INDPceAddExtractDetail.TabIndex = 8
        '
        'INDgcBankReconciliationExtractDetails
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcBankReconciliationExtractDetails, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcBankReconciliationExtractDetails, Nothing)
        Me.INDgcBankReconciliationExtractDetails.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcBankReconciliationExtractDetails, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcBankReconciliationExtractDetails, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcBankReconciliationExtractDetails, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcBankReconciliationExtractDetails, False)
        Me.INDgcBankReconciliationExtractDetails.Location = New System.Drawing.Point(1372, 103)
        Me.INDgcBankReconciliationExtractDetails.MainView = Me.INDgvBankReconciliationExtractDetail
        Me.INDgcBankReconciliationExtractDetails.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDgcBankReconciliationExtractDetails.Name = "INDgcBankReconciliationExtractDetails"
        Me.INDgcBankReconciliationExtractDetails.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrepSleExtractDetailDocumentType, Me.INDrepSleExtractDetailNature, Me.INDrepTxtExtractDetailValue})
        Me.INDgcBankReconciliationExtractDetails.Size = New System.Drawing.Size(841, 568)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcBankReconciliationExtractDetails, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcBankReconciliationExtractDetails.TabIndex = 7
        Me.INDgcBankReconciliationExtractDetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvBankReconciliationExtractDetail})
        '
        'INDgvBankReconciliationExtractDetail
        '
        Me.INDgvBankReconciliationExtractDetail.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvBankReconciliationExtractDetail.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvBankReconciliationExtractDetail.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvBankReconciliationExtractDetail.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvBankReconciliationExtractDetail.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvBankReconciliationExtractDetail.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvBankReconciliationExtractDetail.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvBankReconciliationExtractDetail.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvBankReconciliationExtractDetail.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvBankReconciliationExtractDetail.Appearance.Row.Options.UseFont = True
        Me.INDgvBankReconciliationExtractDetail.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgvBankReconciliationExtractDetail.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvBankReconciliationExtractDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDcolBankReconciliationExtractDetail_DocumentType, Me.INDcolBankReconciliationExtractDetail_DocumentDate, Me.INDcolBankReconciliationExtractDetail_Description, Me.INDcolBankReconciliationExtractDetail_DocumentNumber, Me.INDcolBankReconciliationExtractDetail_Nature, Me.INDcolBankReconciliationExtractDetail_Value})
        Me.INDgvBankReconciliationExtractDetail.DetailHeight = 431
        Me.INDgvBankReconciliationExtractDetail.GridControl = Me.INDgcBankReconciliationExtractDetails
        Me.INDgvBankReconciliationExtractDetail.Name = "INDgvBankReconciliationExtractDetail"
        Me.INDgvBankReconciliationExtractDetail.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvBankReconciliationExtractDetail.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvBankReconciliationExtractDetail.OptionsView.ShowAutoFilterRow = True
        Me.INDgvBankReconciliationExtractDetail.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvBankReconciliationExtractDetail, False)
        '
        'INDcolBankReconciliationExtractDetail_DocumentType
        '
        Me.INDcolBankReconciliationExtractDetail_DocumentType.Caption = "Tipo Documento"
        Me.INDcolBankReconciliationExtractDetail_DocumentType.ColumnEdit = Me.INDrepSleExtractDetailDocumentType
        Me.INDcolBankReconciliationExtractDetail_DocumentType.FieldName = "DocumentType"
        Me.INDcolBankReconciliationExtractDetail_DocumentType.MinWidth = 23
        Me.INDcolBankReconciliationExtractDetail_DocumentType.Name = "INDcolBankReconciliationExtractDetail_DocumentType"
        Me.INDcolBankReconciliationExtractDetail_DocumentType.Visible = True
        Me.INDcolBankReconciliationExtractDetail_DocumentType.VisibleIndex = 0
        Me.INDcolBankReconciliationExtractDetail_DocumentType.Width = 87
        '
        'INDrepSleExtractDetailDocumentType
        '
        Me.INDrepSleExtractDetailDocumentType.AutoHeight = False
        Me.INDrepSleExtractDetailDocumentType.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrepSleExtractDetailDocumentType.DisplayMember = "Item2"
        Me.INDrepSleExtractDetailDocumentType.Name = "INDrepSleExtractDetailDocumentType"
        Me.INDrepSleExtractDetailDocumentType.NullText = ""
        Me.INDrepSleExtractDetailDocumentType.PopupView = Me.INDrepGvExtractDetailDocumentType
        Me.INDrepSleExtractDetailDocumentType.ValueMember = "Item1"
        '
        'INDrepGvExtractDetailDocumentType
        '
        Me.INDrepGvExtractDetailDocumentType.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDrepGvExtractDetailDocumentType.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDrepGvExtractDetailDocumentType.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDrepGvExtractDetailDocumentType.Appearance.FocusedRow.Options.UseFont = True
        Me.INDrepGvExtractDetailDocumentType.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDrepGvExtractDetailDocumentType.Appearance.GroupRow.Options.UseFont = True
        Me.INDrepGvExtractDetailDocumentType.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDrepGvExtractDetailDocumentType.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDrepGvExtractDetailDocumentType.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDrepGvExtractDetailDocumentType.Appearance.Row.Options.UseFont = True
        Me.INDrepGvExtractDetailDocumentType.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDrepColExtractDetailDocumentType})
        Me.INDrepGvExtractDetailDocumentType.DetailHeight = 431
        Me.INDrepGvExtractDetailDocumentType.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDrepGvExtractDetailDocumentType.Name = "INDrepGvExtractDetailDocumentType"
        Me.INDrepGvExtractDetailDocumentType.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDrepGvExtractDetailDocumentType.OptionsView.EnableAppearanceEvenRow = True
        Me.INDrepGvExtractDetailDocumentType.OptionsView.EnableAppearanceOddRow = True
        Me.INDrepGvExtractDetailDocumentType.OptionsView.ShowAutoFilterRow = True
        Me.INDrepGvExtractDetailDocumentType.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDrepGvExtractDetailDocumentType, False)
        '
        'INDrepColExtractDetailDocumentType
        '
        Me.INDrepColExtractDetailDocumentType.Caption = "Descripción"
        Me.INDrepColExtractDetailDocumentType.FieldName = "Item2"
        Me.INDrepColExtractDetailDocumentType.MinWidth = 23
        Me.INDrepColExtractDetailDocumentType.Name = "INDrepColExtractDetailDocumentType"
        Me.INDrepColExtractDetailDocumentType.Visible = True
        Me.INDrepColExtractDetailDocumentType.VisibleIndex = 0
        Me.INDrepColExtractDetailDocumentType.Width = 87
        '
        'INDcolBankReconciliationExtractDetail_DocumentDate
        '
        Me.INDcolBankReconciliationExtractDetail_DocumentDate.Caption = "Fecha"
        Me.INDcolBankReconciliationExtractDetail_DocumentDate.DisplayFormat.FormatString = "d"
        Me.INDcolBankReconciliationExtractDetail_DocumentDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDcolBankReconciliationExtractDetail_DocumentDate.FieldName = "DocumentDate"
        Me.INDcolBankReconciliationExtractDetail_DocumentDate.MinWidth = 23
        Me.INDcolBankReconciliationExtractDetail_DocumentDate.Name = "INDcolBankReconciliationExtractDetail_DocumentDate"
        Me.INDcolBankReconciliationExtractDetail_DocumentDate.Visible = True
        Me.INDcolBankReconciliationExtractDetail_DocumentDate.VisibleIndex = 1
        Me.INDcolBankReconciliationExtractDetail_DocumentDate.Width = 87
        '
        'INDcolBankReconciliationExtractDetail_Description
        '
        Me.INDcolBankReconciliationExtractDetail_Description.Caption = "Detalle"
        Me.INDcolBankReconciliationExtractDetail_Description.FieldName = "Description"
        Me.INDcolBankReconciliationExtractDetail_Description.MinWidth = 23
        Me.INDcolBankReconciliationExtractDetail_Description.Name = "INDcolBankReconciliationExtractDetail_Description"
        Me.INDcolBankReconciliationExtractDetail_Description.Visible = True
        Me.INDcolBankReconciliationExtractDetail_Description.VisibleIndex = 2
        Me.INDcolBankReconciliationExtractDetail_Description.Width = 87
        '
        'INDcolBankReconciliationExtractDetail_DocumentNumber
        '
        Me.INDcolBankReconciliationExtractDetail_DocumentNumber.Caption = "Documento"
        Me.INDcolBankReconciliationExtractDetail_DocumentNumber.FieldName = "DocumentNumber"
        Me.INDcolBankReconciliationExtractDetail_DocumentNumber.MinWidth = 23
        Me.INDcolBankReconciliationExtractDetail_DocumentNumber.Name = "INDcolBankReconciliationExtractDetail_DocumentNumber"
        Me.INDcolBankReconciliationExtractDetail_DocumentNumber.Visible = True
        Me.INDcolBankReconciliationExtractDetail_DocumentNumber.VisibleIndex = 3
        Me.INDcolBankReconciliationExtractDetail_DocumentNumber.Width = 87
        '
        'INDcolBankReconciliationExtractDetail_Nature
        '
        Me.INDcolBankReconciliationExtractDetail_Nature.Caption = "Naturaleza"
        Me.INDcolBankReconciliationExtractDetail_Nature.ColumnEdit = Me.INDrepSleExtractDetailNature
        Me.INDcolBankReconciliationExtractDetail_Nature.FieldName = "Nature"
        Me.INDcolBankReconciliationExtractDetail_Nature.MinWidth = 23
        Me.INDcolBankReconciliationExtractDetail_Nature.Name = "INDcolBankReconciliationExtractDetail_Nature"
        Me.INDcolBankReconciliationExtractDetail_Nature.Visible = True
        Me.INDcolBankReconciliationExtractDetail_Nature.VisibleIndex = 4
        Me.INDcolBankReconciliationExtractDetail_Nature.Width = 87
        '
        'INDrepSleExtractDetailNature
        '
        Me.INDrepSleExtractDetailNature.AutoHeight = False
        Me.INDrepSleExtractDetailNature.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrepSleExtractDetailNature.DisplayMember = "Item2"
        Me.INDrepSleExtractDetailNature.Name = "INDrepSleExtractDetailNature"
        Me.INDrepSleExtractDetailNature.NullText = ""
        Me.INDrepSleExtractDetailNature.PopupView = Me.INDrepGvExtractDetailNature
        Me.INDrepSleExtractDetailNature.ValueMember = "Item1"
        '
        'INDrepGvExtractDetailNature
        '
        Me.INDrepGvExtractDetailNature.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDrepGvExtractDetailNature.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDrepGvExtractDetailNature.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDrepGvExtractDetailNature.Appearance.FocusedRow.Options.UseFont = True
        Me.INDrepGvExtractDetailNature.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDrepGvExtractDetailNature.Appearance.GroupRow.Options.UseFont = True
        Me.INDrepGvExtractDetailNature.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDrepGvExtractDetailNature.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDrepGvExtractDetailNature.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDrepGvExtractDetailNature.Appearance.Row.Options.UseFont = True
        Me.INDrepGvExtractDetailNature.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDrepColExtractDetailNature})
        Me.INDrepGvExtractDetailNature.DetailHeight = 431
        Me.INDrepGvExtractDetailNature.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDrepGvExtractDetailNature.Name = "INDrepGvExtractDetailNature"
        Me.INDrepGvExtractDetailNature.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDrepGvExtractDetailNature.OptionsView.EnableAppearanceEvenRow = True
        Me.INDrepGvExtractDetailNature.OptionsView.EnableAppearanceOddRow = True
        Me.INDrepGvExtractDetailNature.OptionsView.ShowAutoFilterRow = True
        Me.INDrepGvExtractDetailNature.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDrepGvExtractDetailNature, False)
        '
        'INDrepColExtractDetailNature
        '
        Me.INDrepColExtractDetailNature.Caption = "Descripción"
        Me.INDrepColExtractDetailNature.FieldName = "Item2"
        Me.INDrepColExtractDetailNature.MinWidth = 23
        Me.INDrepColExtractDetailNature.Name = "INDrepColExtractDetailNature"
        Me.INDrepColExtractDetailNature.Visible = True
        Me.INDrepColExtractDetailNature.VisibleIndex = 0
        Me.INDrepColExtractDetailNature.Width = 87
        '
        'INDcolBankReconciliationExtractDetail_Value
        '
        Me.INDcolBankReconciliationExtractDetail_Value.Caption = "Valor"
        Me.INDcolBankReconciliationExtractDetail_Value.ColumnEdit = Me.INDrepTxtExtractDetailValue
        Me.INDcolBankReconciliationExtractDetail_Value.FieldName = "Value"
        Me.INDcolBankReconciliationExtractDetail_Value.MinWidth = 23
        Me.INDcolBankReconciliationExtractDetail_Value.Name = "INDcolBankReconciliationExtractDetail_Value"
        Me.INDcolBankReconciliationExtractDetail_Value.Visible = True
        Me.INDcolBankReconciliationExtractDetail_Value.VisibleIndex = 5
        Me.INDcolBankReconciliationExtractDetail_Value.Width = 87
        '
        'INDrepTxtExtractDetailValue
        '
        Me.INDrepTxtExtractDetailValue.AutoHeight = False
        Me.INDrepTxtExtractDetailValue.DisplayFormat.FormatString = "c2"
        Me.INDrepTxtExtractDetailValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDrepTxtExtractDetailValue.EditFormat.FormatString = "c2"
        Me.INDrepTxtExtractDetailValue.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDrepTxtExtractDetailValue.Mask.EditMask = "c2"
        Me.INDrepTxtExtractDetailValue.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDrepTxtExtractDetailValue.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepTxtExtractDetailValue.MaxLength = 20
        Me.INDrepTxtExtractDetailValue.Name = "INDrepTxtExtractDetailValue"
        '
        'INDgcBankReconciliationDetails
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcBankReconciliationDetails, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcBankReconciliationDetails, Nothing)
        Me.INDgcBankReconciliationDetails.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcBankReconciliationDetails, True)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcBankReconciliationDetails, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcBankReconciliationDetails, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcBankReconciliationDetails, False)
        Me.INDgcBankReconciliationDetails.Location = New System.Drawing.Point(503, 59)
        Me.INDgcBankReconciliationDetails.MainView = Me.INDgvBankReconciliationDetail
        Me.INDgcBankReconciliationDetails.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDgcBankReconciliationDetails.Name = "INDgcBankReconciliationDetails"
        Me.INDgcBankReconciliationDetails.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrepCheckSelectOption, Me.INDrepSleDetailDocumentType, Me.INDrepSleDetailNature})
        Me.INDgcBankReconciliationDetails.Size = New System.Drawing.Size(841, 612)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcBankReconciliationDetails, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcBankReconciliationDetails.TabIndex = 6
        Me.INDgcBankReconciliationDetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvBankReconciliationDetail})
        '
        'INDgvBankReconciliationDetail
        '
        Me.INDgvBankReconciliationDetail.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvBankReconciliationDetail.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvBankReconciliationDetail.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvBankReconciliationDetail.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvBankReconciliationDetail.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvBankReconciliationDetail.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvBankReconciliationDetail.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvBankReconciliationDetail.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvBankReconciliationDetail.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvBankReconciliationDetail.Appearance.Row.Options.UseFont = True
        Me.INDgvBankReconciliationDetail.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgvBankReconciliationDetail.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvBankReconciliationDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDcolBankReconciliationDetail_Select, Me.INDcolBankReconciliationDetail_Code, Me.INDcolBankReconciliationDetail_DocumenType, Me.INDcolBankReconciliationDetail_DocumentDate, Me.INDcolBankReconciliationDetail_ThirdPartyNitName, Me.INDcolBankReconciliationDetail_DocumentNumber, Me.INDcolBankReconciliationDetail_Nature, Me.INDcolBankReconciliationDetail_Value, Me.INDcolBankReconciliationDetail_Observations, Me.INDcolBankReconciliationDetail_CreationUser, Me.INDcolBankReconciliationDetail_ConfirmationUser})
        Me.INDgvBankReconciliationDetail.DetailHeight = 431
        Me.INDgvBankReconciliationDetail.GridControl = Me.INDgcBankReconciliationDetails
        Me.INDgvBankReconciliationDetail.Name = "INDgvBankReconciliationDetail"
        Me.INDgvBankReconciliationDetail.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvBankReconciliationDetail.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvBankReconciliationDetail.OptionsView.ShowAutoFilterRow = True
        Me.INDgvBankReconciliationDetail.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvBankReconciliationDetail, False)
        '
        'INDcolBankReconciliationDetail_Select
        '
        Me.INDcolBankReconciliationDetail_Select.Caption = "Sel."
        Me.INDcolBankReconciliationDetail_Select.ColumnEdit = Me.INDrepCheckSelectOption
        Me.INDcolBankReconciliationDetail_Select.FieldName = "Reconciled"
        Me.INDcolBankReconciliationDetail_Select.MinWidth = 23
        Me.INDcolBankReconciliationDetail_Select.Name = "INDcolBankReconciliationDetail_Select"
        Me.INDcolBankReconciliationDetail_Select.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDcolBankReconciliationDetail_Select.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDcolBankReconciliationDetail_Select.OptionsColumn.AllowMove = False
        Me.INDcolBankReconciliationDetail_Select.OptionsColumn.AllowShowHide = False
        Me.INDcolBankReconciliationDetail_Select.OptionsColumn.AllowSize = False
        Me.INDcolBankReconciliationDetail_Select.OptionsColumn.FixedWidth = True
        Me.INDcolBankReconciliationDetail_Select.Visible = True
        Me.INDcolBankReconciliationDetail_Select.VisibleIndex = 0
        Me.INDcolBankReconciliationDetail_Select.Width = 43
        '
        'INDrepCheckSelectOption
        '
        Me.INDrepCheckSelectOption.AutoHeight = False
        Me.INDrepCheckSelectOption.Name = "INDrepCheckSelectOption"
        '
        'INDcolBankReconciliationDetail_Code
        '
        Me.INDcolBankReconciliationDetail_Code.Caption = "Código"
        Me.INDcolBankReconciliationDetail_Code.FieldName = "EntityCode"
        Me.INDcolBankReconciliationDetail_Code.MinWidth = 23
        Me.INDcolBankReconciliationDetail_Code.Name = "INDcolBankReconciliationDetail_Code"
        Me.INDcolBankReconciliationDetail_Code.OptionsColumn.AllowEdit = False
        Me.INDcolBankReconciliationDetail_Code.OptionsColumn.AllowFocus = False
        Me.INDcolBankReconciliationDetail_Code.Visible = True
        Me.INDcolBankReconciliationDetail_Code.VisibleIndex = 1
        Me.INDcolBankReconciliationDetail_Code.Width = 101
        '
        'INDcolBankReconciliationDetail_DocumenType
        '
        Me.INDcolBankReconciliationDetail_DocumenType.Caption = "Tipo Documento"
        Me.INDcolBankReconciliationDetail_DocumenType.ColumnEdit = Me.INDrepSleDetailDocumentType
        Me.INDcolBankReconciliationDetail_DocumenType.FieldName = "DocumentType"
        Me.INDcolBankReconciliationDetail_DocumenType.MinWidth = 23
        Me.INDcolBankReconciliationDetail_DocumenType.Name = "INDcolBankReconciliationDetail_DocumenType"
        Me.INDcolBankReconciliationDetail_DocumenType.OptionsColumn.AllowEdit = False
        Me.INDcolBankReconciliationDetail_DocumenType.OptionsColumn.AllowFocus = False
        Me.INDcolBankReconciliationDetail_DocumenType.Visible = True
        Me.INDcolBankReconciliationDetail_DocumenType.VisibleIndex = 2
        Me.INDcolBankReconciliationDetail_DocumenType.Width = 108
        '
        'INDrepSleDetailDocumentType
        '
        Me.INDrepSleDetailDocumentType.AutoHeight = False
        Me.INDrepSleDetailDocumentType.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrepSleDetailDocumentType.DisplayMember = "Item2"
        Me.INDrepSleDetailDocumentType.Name = "INDrepSleDetailDocumentType"
        Me.INDrepSleDetailDocumentType.NullText = ""
        Me.INDrepSleDetailDocumentType.PopupView = Me.INDrepGvDetailDocumentType
        Me.INDrepSleDetailDocumentType.ValueMember = "Item1"
        '
        'INDrepGvDetailDocumentType
        '
        Me.INDrepGvDetailDocumentType.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDrepGvDetailDocumentType.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDrepGvDetailDocumentType.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDrepGvDetailDocumentType.Appearance.FocusedRow.Options.UseFont = True
        Me.INDrepGvDetailDocumentType.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDrepGvDetailDocumentType.Appearance.GroupRow.Options.UseFont = True
        Me.INDrepGvDetailDocumentType.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDrepGvDetailDocumentType.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDrepGvDetailDocumentType.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDrepGvDetailDocumentType.Appearance.Row.Options.UseFont = True
        Me.INDrepGvDetailDocumentType.DetailHeight = 431
        Me.INDrepGvDetailDocumentType.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDrepGvDetailDocumentType.Name = "INDrepGvDetailDocumentType"
        Me.INDrepGvDetailDocumentType.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDrepGvDetailDocumentType.OptionsView.EnableAppearanceEvenRow = True
        Me.INDrepGvDetailDocumentType.OptionsView.EnableAppearanceOddRow = True
        Me.INDrepGvDetailDocumentType.OptionsView.ShowAutoFilterRow = True
        Me.INDrepGvDetailDocumentType.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDrepGvDetailDocumentType, False)
        '
        'INDcolBankReconciliationDetail_DocumentDate
        '
        Me.INDcolBankReconciliationDetail_DocumentDate.Caption = "Fecha"
        Me.INDcolBankReconciliationDetail_DocumentDate.DisplayFormat.FormatString = "d"
        Me.INDcolBankReconciliationDetail_DocumentDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDcolBankReconciliationDetail_DocumentDate.FieldName = "DocumentDate"
        Me.INDcolBankReconciliationDetail_DocumentDate.MinWidth = 23
        Me.INDcolBankReconciliationDetail_DocumentDate.Name = "INDcolBankReconciliationDetail_DocumentDate"
        Me.INDcolBankReconciliationDetail_DocumentDate.OptionsColumn.AllowEdit = False
        Me.INDcolBankReconciliationDetail_DocumentDate.OptionsColumn.AllowFocus = False
        Me.INDcolBankReconciliationDetail_DocumentDate.Visible = True
        Me.INDcolBankReconciliationDetail_DocumentDate.VisibleIndex = 3
        Me.INDcolBankReconciliationDetail_DocumentDate.Width = 106
        '
        'INDcolBankReconciliationDetail_ThirdPartyNitName
        '
        Me.INDcolBankReconciliationDetail_ThirdPartyNitName.Caption = "Tercero"
        Me.INDcolBankReconciliationDetail_ThirdPartyNitName.FieldName = "ThirdPartyNitName"
        Me.INDcolBankReconciliationDetail_ThirdPartyNitName.MinWidth = 23
        Me.INDcolBankReconciliationDetail_ThirdPartyNitName.Name = "INDcolBankReconciliationDetail_ThirdPartyNitName"
        Me.INDcolBankReconciliationDetail_ThirdPartyNitName.OptionsColumn.AllowEdit = False
        Me.INDcolBankReconciliationDetail_ThirdPartyNitName.OptionsColumn.AllowFocus = False
        Me.INDcolBankReconciliationDetail_ThirdPartyNitName.Visible = True
        Me.INDcolBankReconciliationDetail_ThirdPartyNitName.VisibleIndex = 4
        Me.INDcolBankReconciliationDetail_ThirdPartyNitName.Width = 108
        '
        'INDcolBankReconciliationDetail_DocumentNumber
        '
        Me.INDcolBankReconciliationDetail_DocumentNumber.Caption = "Documento"
        Me.INDcolBankReconciliationDetail_DocumentNumber.FieldName = "DocumentNumber"
        Me.INDcolBankReconciliationDetail_DocumentNumber.MinWidth = 23
        Me.INDcolBankReconciliationDetail_DocumentNumber.Name = "INDcolBankReconciliationDetail_DocumentNumber"
        Me.INDcolBankReconciliationDetail_DocumentNumber.OptionsColumn.AllowEdit = False
        Me.INDcolBankReconciliationDetail_DocumentNumber.OptionsColumn.AllowFocus = False
        Me.INDcolBankReconciliationDetail_DocumentNumber.Visible = True
        Me.INDcolBankReconciliationDetail_DocumentNumber.VisibleIndex = 5
        Me.INDcolBankReconciliationDetail_DocumentNumber.Width = 108
        '
        'INDcolBankReconciliationDetail_Nature
        '
        Me.INDcolBankReconciliationDetail_Nature.Caption = "Naturaleza"
        Me.INDcolBankReconciliationDetail_Nature.ColumnEdit = Me.INDrepSleDetailNature
        Me.INDcolBankReconciliationDetail_Nature.FieldName = "Nature"
        Me.INDcolBankReconciliationDetail_Nature.MinWidth = 23
        Me.INDcolBankReconciliationDetail_Nature.Name = "INDcolBankReconciliationDetail_Nature"
        Me.INDcolBankReconciliationDetail_Nature.OptionsColumn.AllowEdit = False
        Me.INDcolBankReconciliationDetail_Nature.OptionsColumn.AllowFocus = False
        Me.INDcolBankReconciliationDetail_Nature.Visible = True
        Me.INDcolBankReconciliationDetail_Nature.VisibleIndex = 6
        Me.INDcolBankReconciliationDetail_Nature.Width = 108
        '
        'INDrepSleDetailNature
        '
        Me.INDrepSleDetailNature.AutoHeight = False
        Me.INDrepSleDetailNature.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrepSleDetailNature.DisplayMember = "Item2"
        Me.INDrepSleDetailNature.Name = "INDrepSleDetailNature"
        Me.INDrepSleDetailNature.NullText = ""
        Me.INDrepSleDetailNature.PopupView = Me.INDrepGvDetailNature
        Me.INDrepSleDetailNature.ValueMember = "Item1"
        '
        'INDrepGvDetailNature
        '
        Me.INDrepGvDetailNature.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDrepGvDetailNature.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDrepGvDetailNature.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDrepGvDetailNature.Appearance.FocusedRow.Options.UseFont = True
        Me.INDrepGvDetailNature.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDrepGvDetailNature.Appearance.GroupRow.Options.UseFont = True
        Me.INDrepGvDetailNature.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDrepGvDetailNature.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDrepGvDetailNature.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDrepGvDetailNature.Appearance.Row.Options.UseFont = True
        Me.INDrepGvDetailNature.DetailHeight = 431
        Me.INDrepGvDetailNature.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDrepGvDetailNature.Name = "INDrepGvDetailNature"
        Me.INDrepGvDetailNature.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDrepGvDetailNature.OptionsView.EnableAppearanceEvenRow = True
        Me.INDrepGvDetailNature.OptionsView.EnableAppearanceOddRow = True
        Me.INDrepGvDetailNature.OptionsView.ShowAutoFilterRow = True
        Me.INDrepGvDetailNature.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDrepGvDetailNature, False)
        '
        'INDcolBankReconciliationDetail_Value
        '
        Me.INDcolBankReconciliationDetail_Value.Caption = "Valor"
        Me.INDcolBankReconciliationDetail_Value.DisplayFormat.FormatString = "c2"
        Me.INDcolBankReconciliationDetail_Value.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDcolBankReconciliationDetail_Value.FieldName = "Value"
        Me.INDcolBankReconciliationDetail_Value.MinWidth = 23
        Me.INDcolBankReconciliationDetail_Value.Name = "INDcolBankReconciliationDetail_Value"
        Me.INDcolBankReconciliationDetail_Value.OptionsColumn.AllowEdit = False
        Me.INDcolBankReconciliationDetail_Value.OptionsColumn.AllowFocus = False
        Me.INDcolBankReconciliationDetail_Value.Visible = True
        Me.INDcolBankReconciliationDetail_Value.VisibleIndex = 7
        Me.INDcolBankReconciliationDetail_Value.Width = 134
        '
        'INDcolBankReconciliationDetail_Observations
        '
        Me.INDcolBankReconciliationDetail_Observations.Caption = "Detalle"
        Me.INDcolBankReconciliationDetail_Observations.FieldName = "Observations"
        Me.INDcolBankReconciliationDetail_Observations.MinWidth = 23
        Me.INDcolBankReconciliationDetail_Observations.Name = "INDcolBankReconciliationDetail_Observations"
        Me.INDcolBankReconciliationDetail_Observations.OptionsColumn.AllowEdit = False
        Me.INDcolBankReconciliationDetail_Observations.OptionsColumn.AllowFocus = False
        Me.INDcolBankReconciliationDetail_Observations.Width = 87
        '
        'INDcolBankReconciliationDetail_CreationUser
        '
        Me.INDcolBankReconciliationDetail_CreationUser.Caption = "Usuario Creación"
        Me.INDcolBankReconciliationDetail_CreationUser.FieldName = "CreationUser"
        Me.INDcolBankReconciliationDetail_CreationUser.MinWidth = 23
        Me.INDcolBankReconciliationDetail_CreationUser.Name = "INDcolBankReconciliationDetail_CreationUser"
        Me.INDcolBankReconciliationDetail_CreationUser.OptionsColumn.AllowEdit = False
        Me.INDcolBankReconciliationDetail_CreationUser.OptionsColumn.AllowFocus = False
        Me.INDcolBankReconciliationDetail_CreationUser.Width = 87
        '
        'INDcolBankReconciliationDetail_ConfirmationUser
        '
        Me.INDcolBankReconciliationDetail_ConfirmationUser.Caption = "Usuario Confirmación"
        Me.INDcolBankReconciliationDetail_ConfirmationUser.FieldName = "ConfirmationUser"
        Me.INDcolBankReconciliationDetail_ConfirmationUser.MinWidth = 23
        Me.INDcolBankReconciliationDetail_ConfirmationUser.Name = "INDcolBankReconciliationDetail_ConfirmationUser"
        Me.INDcolBankReconciliationDetail_ConfirmationUser.OptionsColumn.AllowEdit = False
        Me.INDcolBankReconciliationDetail_ConfirmationUser.OptionsColumn.AllowFocus = False
        Me.INDcolBankReconciliationDetail_ConfirmationUser.Width = 87
        '
        'INDseDifferenceReconcile
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDseDifferenceReconcile, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDseDifferenceReconcile, False)
        Me.INDseDifferenceReconcile.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDseDifferenceReconcile.EnterMoveNextControl = True
        Me.INDseDifferenceReconcile.Location = New System.Drawing.Point(24, 460)
        Me.INDseDifferenceReconcile.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.INDseDifferenceReconcile, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDseDifferenceReconcile.Name = "INDseDifferenceReconcile"
        Me.INDseDifferenceReconcile.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDseDifferenceReconcile.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseDifferenceReconcile.Properties.Appearance.Options.UseBackColor = True
        Me.INDseDifferenceReconcile.Properties.Appearance.Options.UseFont = True
        Me.INDseDifferenceReconcile.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseDifferenceReconcile.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDseDifferenceReconcile.Properties.Mask.EditMask = "C2"
        Me.INDseDifferenceReconcile.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDseDifferenceReconcile.Properties.MaxLength = 18
        Me.INDseDifferenceReconcile.Properties.ReadOnly = True
        Me.INDseDifferenceReconcile.Size = New System.Drawing.Size(451, 34)
        Me.INDseDifferenceReconcile.StyleController = Me.INDlyRoot
        Me.INDseDifferenceReconcile.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDseDifferenceReconcile, 0)
        '
        'INDseEndEntityBankAccountValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDseEndEntityBankAccountValue, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDseEndEntityBankAccountValue, False)
        Me.INDseEndEntityBankAccountValue.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDseEndEntityBankAccountValue.EnterMoveNextControl = True
        Me.INDseEndEntityBankAccountValue.Location = New System.Drawing.Point(24, 312)
        Me.INDseEndEntityBankAccountValue.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.INDseEndEntityBankAccountValue, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDseEndEntityBankAccountValue.Name = "INDseEndEntityBankAccountValue"
        Me.INDseEndEntityBankAccountValue.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDseEndEntityBankAccountValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseEndEntityBankAccountValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDseEndEntityBankAccountValue.Properties.Appearance.Options.UseFont = True
        Me.INDseEndEntityBankAccountValue.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseEndEntityBankAccountValue.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDseEndEntityBankAccountValue.Properties.Mask.EditMask = "C2"
        Me.INDseEndEntityBankAccountValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDseEndEntityBankAccountValue.Properties.MaxLength = 18
        Me.INDseEndEntityBankAccountValue.Properties.ReadOnly = True
        Me.INDseEndEntityBankAccountValue.Size = New System.Drawing.Size(451, 34)
        Me.INDseEndEntityBankAccountValue.StyleController = Me.INDlyRoot
        Me.INDseEndEntityBankAccountValue.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDseEndEntityBankAccountValue, 0)
        '
        'INDseExtractValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDseExtractValue, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDseExtractValue, False)
        Me.INDseExtractValue.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDseExtractValue.EnterMoveNextControl = True
        Me.INDseExtractValue.Location = New System.Drawing.Point(24, 386)
        Me.INDseExtractValue.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.INDseExtractValue, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDseExtractValue.Name = "INDseExtractValue"
        Me.INDseExtractValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseExtractValue.Properties.Appearance.Options.UseFont = True
        Me.INDseExtractValue.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseExtractValue.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDseExtractValue.Properties.Mask.EditMask = "C2"
        Me.INDseExtractValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDseExtractValue.Properties.MaxLength = 18
        Me.INDseExtractValue.Size = New System.Drawing.Size(451, 34)
        Me.INDseExtractValue.StyleController = Me.INDlyRoot
        Me.INDseExtractValue.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDseExtractValue, 0)
        '
        'INDdteDocumentDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdteDocumentDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdteDocumentDate, True)
        Me.INDdteDocumentDate.EditValue = Nothing
        Me.INDdteDocumentDate.EnterMoveNextControl = True
        Me.INDdteDocumentDate.Location = New System.Drawing.Point(24, 238)
        Me.INDdteDocumentDate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.INDdteDocumentDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDdteDocumentDate.Name = "INDdteDocumentDate"
        Me.INDdteDocumentDate.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDdteDocumentDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteDocumentDate.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDdteDocumentDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDdteDocumentDate.Properties.Appearance.Options.UseFont = True
        Me.INDdteDocumentDate.Properties.Appearance.Options.UseForeColor = True
        Me.INDdteDocumentDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteDocumentDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdteDocumentDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteDocumentDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteDocumentDate.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDdteDocumentDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDdteDocumentDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdteDocumentDate.Size = New System.Drawing.Size(451, 34)
        Me.INDdteDocumentDate.StyleController = Me.INDlyRoot
        Me.INDdteDocumentDate.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdteDocumentDate, 0)
        Me.INDdteDocumentDate.ToolTip = "Este Campo es Necesario"
        '
        'INDsleEntityBankAccount
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleEntityBankAccount, AppearanceObject5)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleEntityBankAccount, AppearanceObject6)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleEntityBankAccount, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleEntityBankAccount, True)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleEntityBankAccount, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleEntityBankAccount, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleEntityBankAccount, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleEntityBankAccount, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleEntityBankAccount, False)
        Me.INDsleEntityBankAccount.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleEntityBankAccount, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleEntityBankAccount, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleEntityBankAccount, False)
        Me.INDsleEntityBankAccount.Location = New System.Drawing.Point(24, 164)
        Me.INDsleEntityBankAccount.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleEntityBankAccount, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleEntityBankAccount.Name = "INDsleEntityBankAccount"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleEntityBankAccount, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleEntityBankAccount, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleEntityBankAccount, True)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleEntityBankAccount, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleEntityBankAccount, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleEntityBankAccount, False)
        Me.INDsleEntityBankAccount.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDsleEntityBankAccount.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleEntityBankAccount.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleEntityBankAccount.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleEntityBankAccount.Properties.Appearance.Options.UseFont = True
        Me.INDsleEntityBankAccount.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleEntityBankAccount.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleEntityBankAccount.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleEntityBankAccount.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleEntityBankAccount.Properties.DisplayMember = "CodeBankAccount"
        Me.INDsleEntityBankAccount.Properties.NullText = ""
        Me.INDsleEntityBankAccount.Properties.PopupFormMinSize = New System.Drawing.Size(933, 0)
        Me.INDsleEntityBankAccount.Properties.PopupSizeable = False
        Me.INDsleEntityBankAccount.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDsleEntityBankAccount.Properties.ShowFooter = False
        Me.INDsleEntityBankAccount.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleEntityBankAccount, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleEntityBankAccount, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleEntityBankAccount, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleEntityBankAccount, True)
        Me.INDsleEntityBankAccount.Size = New System.Drawing.Size(451, 34)
        Me.INDsleEntityBankAccount.StyleController = Me.INDlyRoot
        Me.INDsleEntityBankAccount.TabIndex = 1
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleEntityBankAccount, "628")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleEntityBankAccount, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleEntityBankAccount, "{0} - {1}")
        Me.INDsleEntityBankAccount.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleEntityBankAccount, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleEntityBankAccount, False)
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6})
        Me.SearchLookUpEdit1View.DetailHeight = 431
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Código"
        Me.GridColumn1.FieldName = "Code"
        Me.GridColumn1.MinWidth = 23
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 177
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Cuenta Contable"
        Me.GridColumn2.FieldName = "IdMainAccount.NumberName"
        Me.GridColumn2.MinWidth = 23
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 474
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Fecha Apertura"
        Me.GridColumn3.FieldName = "InitialDate"
        Me.GridColumn3.MinWidth = 23
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        Me.GridColumn3.Width = 173
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Tipo"
        Me.GridColumn4.FieldName = "TypeName"
        Me.GridColumn4.MinWidth = 23
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 3
        Me.GridColumn4.Width = 201
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Banco"
        Me.GridColumn5.FieldName = "IdBank.Name"
        Me.GridColumn5.MinWidth = 23
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 4
        Me.GridColumn5.Width = 310
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Fuente Finan."
        Me.GridColumn6.FieldName = "FinancialSourceId.CodeName"
        Me.GridColumn6.MinWidth = 23
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 5
        Me.GridColumn6.Width = 289
        '
        'INDbtnCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbtnCode, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbtnCode, True)
        Me.INDbtnCode.Location = New System.Drawing.Point(24, 90)
        Me.INDbtnCode.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.INDbtnCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDbtnCode.Name = "INDbtnCode"
        Me.INDbtnCode.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDbtnCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDbtnCode.Properties.Appearance.Options.UseFont = True
        Me.INDbtnCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseFont = True
        EditorButtonImageOptions2.Image = Global.Presentation.Treasury.My.Resources.Resources.BuscarMetro
        Me.INDbtnCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions2, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject5, SerializableAppearanceObject6, SerializableAppearanceObject7, SerializableAppearanceObject8, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDbtnCode.Properties.MaxLength = 20
        Me.INDbtnCode.Size = New System.Drawing.Size(451, 34)
        Me.INDbtnCode.StyleController = Me.INDlyRoot
        Me.INDbtnCode.TabIndex = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbtnCode, 0)
        Me.INDbtnCode.ToolTip = "Este Campo es Necesario"
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlygPrincipalInformation, Me.INDlygEntityBankAccountInformation, Me.INDlygExtractInformation})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(2237, 695)
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
        Me.INDlygPrincipalInformation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciCode, Me.INDLciEntityBankAccount, Me.INDLciDocumentDate, Me.INDLciEntityBankAccountValue, Me.INDLciDifferenceReconcile, Me.INDLciExtractValue})
        Me.INDlygPrincipalInformation.Location = New System.Drawing.Point(0, 0)
        Me.INDlygPrincipalInformation.Name = "INDlygPrincipalInformation"
        Me.INDlygPrincipalInformation.Size = New System.Drawing.Size(479, 675)
        Me.INDlygPrincipalInformation.Text = "Información Principal"
        '
        'INDLciCode
        '
        Me.INDLciCode.Control = Me.INDbtnCode
        Me.INDLciCode.Location = New System.Drawing.Point(0, 0)
        Me.INDLciCode.MaxSize = New System.Drawing.Size(455, 74)
        Me.INDLciCode.MinSize = New System.Drawing.Size(455, 74)
        Me.INDLciCode.Name = "INDLciCode"
        Me.INDLciCode.Size = New System.Drawing.Size(455, 74)
        Me.INDLciCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciCode.Text = "Código"
        Me.INDLciCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciCode.TextSize = New System.Drawing.Size(157, 26)
        Me.INDLciCode.TextToControlDistance = 5
        '
        'INDLciEntityBankAccount
        '
        Me.INDLciEntityBankAccount.Control = Me.INDsleEntityBankAccount
        Me.INDLciEntityBankAccount.Location = New System.Drawing.Point(0, 74)
        Me.INDLciEntityBankAccount.MaxSize = New System.Drawing.Size(455, 74)
        Me.INDLciEntityBankAccount.MinSize = New System.Drawing.Size(455, 74)
        Me.INDLciEntityBankAccount.Name = "INDLciEntityBankAccount"
        Me.INDLciEntityBankAccount.Size = New System.Drawing.Size(455, 74)
        Me.INDLciEntityBankAccount.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciEntityBankAccount.Text = "Cuenta Bancaria"
        Me.INDLciEntityBankAccount.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciEntityBankAccount.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciEntityBankAccount.TextSize = New System.Drawing.Size(157, 26)
        Me.INDLciEntityBankAccount.TextToControlDistance = 5
        '
        'INDLciDocumentDate
        '
        Me.INDLciDocumentDate.Control = Me.INDdteDocumentDate
        Me.INDLciDocumentDate.Location = New System.Drawing.Point(0, 148)
        Me.INDLciDocumentDate.MaxSize = New System.Drawing.Size(455, 74)
        Me.INDLciDocumentDate.MinSize = New System.Drawing.Size(455, 74)
        Me.INDLciDocumentDate.Name = "INDLciDocumentDate"
        Me.INDLciDocumentDate.Size = New System.Drawing.Size(455, 74)
        Me.INDLciDocumentDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDocumentDate.Text = "Fecha"
        Me.INDLciDocumentDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciDocumentDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDocumentDate.TextSize = New System.Drawing.Size(157, 26)
        Me.INDLciDocumentDate.TextToControlDistance = 5
        '
        'INDLciEntityBankAccountValue
        '
        Me.INDLciEntityBankAccountValue.Control = Me.INDseEndEntityBankAccountValue
        Me.INDLciEntityBankAccountValue.Location = New System.Drawing.Point(0, 222)
        Me.INDLciEntityBankAccountValue.MaxSize = New System.Drawing.Size(455, 74)
        Me.INDLciEntityBankAccountValue.MinSize = New System.Drawing.Size(455, 74)
        Me.INDLciEntityBankAccountValue.Name = "INDLciEntityBankAccountValue"
        Me.INDLciEntityBankAccountValue.Size = New System.Drawing.Size(455, 74)
        Me.INDLciEntityBankAccountValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciEntityBankAccountValue.Text = "Saldo Final del Libro de Bancos"
        Me.INDLciEntityBankAccountValue.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciEntityBankAccountValue.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciEntityBankAccountValue.TextSize = New System.Drawing.Size(157, 26)
        Me.INDLciEntityBankAccountValue.TextToControlDistance = 5
        '
        'INDLciDifferenceReconcile
        '
        Me.INDLciDifferenceReconcile.Control = Me.INDseDifferenceReconcile
        Me.INDLciDifferenceReconcile.Location = New System.Drawing.Point(0, 370)
        Me.INDLciDifferenceReconcile.MaxSize = New System.Drawing.Size(455, 74)
        Me.INDLciDifferenceReconcile.MinSize = New System.Drawing.Size(455, 74)
        Me.INDLciDifferenceReconcile.Name = "INDLciDifferenceReconcile"
        Me.INDLciDifferenceReconcile.Size = New System.Drawing.Size(455, 246)
        Me.INDLciDifferenceReconcile.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDifferenceReconcile.Text = "Diferencia a Conciliar"
        Me.INDLciDifferenceReconcile.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciDifferenceReconcile.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDifferenceReconcile.TextSize = New System.Drawing.Size(157, 26)
        Me.INDLciDifferenceReconcile.TextToControlDistance = 5
        '
        'INDLciExtractValue
        '
        Me.INDLciExtractValue.Control = Me.INDseExtractValue
        Me.INDLciExtractValue.Location = New System.Drawing.Point(0, 296)
        Me.INDLciExtractValue.MaxSize = New System.Drawing.Size(455, 74)
        Me.INDLciExtractValue.MinSize = New System.Drawing.Size(455, 74)
        Me.INDLciExtractValue.Name = "INDLciExtractValue"
        Me.INDLciExtractValue.Size = New System.Drawing.Size(455, 74)
        Me.INDLciExtractValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciExtractValue.Text = "Saldo Final del Extracto"
        Me.INDLciExtractValue.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciExtractValue.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciExtractValue.TextSize = New System.Drawing.Size(157, 26)
        Me.INDLciExtractValue.TextToControlDistance = 5
        '
        'INDlygEntityBankAccountInformation
        '
        Me.INDlygEntityBankAccountInformation.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygEntityBankAccountInformation.AppearanceGroup.Options.UseFont = True
        Me.INDlygEntityBankAccountInformation.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygEntityBankAccountInformation.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygEntityBankAccountInformation.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygEntityBankAccountInformation.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygEntityBankAccountInformation.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygEntityBankAccountInformation.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygEntityBankAccountInformation.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygEntityBankAccountInformation.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygEntityBankAccountInformation.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygEntityBankAccountInformation.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygEntityBankAccountInformation.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygEntityBankAccountInformation.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygEntityBankAccountInformation, False)
        Me.INDlygEntityBankAccountInformation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciBankReconciliationDetails})
        Me.INDlygEntityBankAccountInformation.Location = New System.Drawing.Point(479, 0)
        Me.INDlygEntityBankAccountInformation.Name = "INDlygEntityBankAccountInformation"
        Me.INDlygEntityBankAccountInformation.Size = New System.Drawing.Size(869, 675)
        Me.INDlygEntityBankAccountInformation.Text = "Libro de Bancos"
        '
        'INDLciBankReconciliationDetails
        '
        Me.INDLciBankReconciliationDetails.Control = Me.INDgcBankReconciliationDetails
        Me.INDLciBankReconciliationDetails.Location = New System.Drawing.Point(0, 0)
        Me.INDLciBankReconciliationDetails.MinSize = New System.Drawing.Size(845, 30)
        Me.INDLciBankReconciliationDetails.Name = "INDLciBankReconciliationDetails"
        Me.INDLciBankReconciliationDetails.Size = New System.Drawing.Size(845, 616)
        Me.INDLciBankReconciliationDetails.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciBankReconciliationDetails.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciBankReconciliationDetails.TextVisible = False
        '
        'INDlygExtractInformation
        '
        Me.INDlygExtractInformation.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygExtractInformation.AppearanceGroup.Options.UseFont = True
        Me.INDlygExtractInformation.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygExtractInformation.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygExtractInformation.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygExtractInformation.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygExtractInformation.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygExtractInformation.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygExtractInformation.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygExtractInformation.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygExtractInformation.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygExtractInformation.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygExtractInformation.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygExtractInformation.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygExtractInformation, False)
        Me.INDlygExtractInformation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem2, Me.INDLciAddExtractDetail})
        Me.INDlygExtractInformation.Location = New System.Drawing.Point(1348, 0)
        Me.INDlygExtractInformation.Name = "INDlygExtractInformation"
        Me.INDlygExtractInformation.Size = New System.Drawing.Size(869, 675)
        Me.INDlygExtractInformation.Text = "Extracto Bancario"
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDgcBankReconciliationExtractDetails
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 44)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(845, 30)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(845, 572)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'INDLciAddExtractDetail
        '
        Me.INDLciAddExtractDetail.Control = Me.INDPceAddExtractDetail
        Me.INDLciAddExtractDetail.Location = New System.Drawing.Point(0, 0)
        Me.INDLciAddExtractDetail.MaxSize = New System.Drawing.Size(0, 44)
        Me.INDLciAddExtractDetail.MinSize = New System.Drawing.Size(845, 44)
        Me.INDLciAddExtractDetail.Name = "INDLciAddExtractDetail"
        Me.INDLciAddExtractDetail.Size = New System.Drawing.Size(845, 44)
        Me.INDLciAddExtractDetail.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciAddExtractDetail.Text = "Agregar Detalles"
        Me.INDLciAddExtractDetail.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciAddExtractDetail.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'FrmBankConciliation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1709, 863)
        Me.IconOptions.ShowIcon = False
        Me.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmBankConciliation"
        Me.Opacity = 1.0R
        Me.Padding = New System.Windows.Forms.Padding(0, 7, 0, 0)
        Me.Tag = "1956"
        Me.Text = "Conciliación Bancaria"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyRoot.ResumeLayout(False)
        CType(Me.INDpccExtractDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpccExtractDetails.ResumeLayout(False)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDtxtExtractDocumentValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleExtractNature.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvExtractNature, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtExtractDocument.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtExtractDetail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdeExtractDocumentDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdeExtractDocumentDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleExtractDocumentType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvExtractDocumentType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciExtractDocumentType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciExtractDocumentDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciExtractDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciExtractDocument, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciExtractNature, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciExtractDocumentValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.INDPceAddExtractDetail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcBankReconciliationExtractDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvBankReconciliationExtractDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepSleExtractDetailDocumentType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepGvExtractDetailDocumentType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepSleExtractDetailNature, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepGvExtractDetailNature, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepTxtExtractDetailValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcBankReconciliationDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvBankReconciliationDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepCheckSelectOption, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepSleDetailDocumentType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepGvDetailDocumentType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepSleDetailNature, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepGvDetailNature, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDseDifferenceReconcile.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDseEndEntityBankAccountValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDseExtractValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteDocumentDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteDocumentDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleEntityBankAccount.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygPrincipalInformation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciEntityBankAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDocumentDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciEntityBankAccountValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDifferenceReconcile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciExtractValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygEntityBankAccountInformation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciBankReconciliationDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygExtractInformation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciAddExtractDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents INDlyRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents CtrNavigationControlPanel1 As CtrNavigationControlPanel
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As IndigoTextEdit
    Friend WithEvents IndigoSimpleButton1 As IndigoSimpleButton
    Friend WithEvents IndigoSearchLookUpControl1 As IndigoSearchLookUpControl
    Friend WithEvents IndigoLabelControl1 As IndigoLabelControl
    Friend WithEvents IndigoGroupControl1 As IndigoGroupControl
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents INDbtnCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDLciCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlygPrincipalInformation As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDsleEntityBankAccount As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciEntityBankAccount As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDdteDocumentDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDLciDocumentDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDseExtractValue As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDLciExtractValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDseEndEntityBankAccountValue As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDLciEntityBankAccountValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDseDifferenceReconcile As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDLciDifferenceReconcile As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgcBankReconciliationExtractDetails As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvBankReconciliationExtractDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDgcBankReconciliationDetails As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvBankReconciliationDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlygEntityBankAccountInformation As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciBankReconciliationDetails As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlygExtractInformation As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDcolBankReconciliationDetail_Select As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepCheckSelectOption As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents INDcolBankReconciliationDetail_Code As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolBankReconciliationDetail_DocumenType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolBankReconciliationDetail_DocumentDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolBankReconciliationDetail_ThirdPartyNitName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolBankReconciliationDetail_DocumentNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolBankReconciliationDetail_Nature As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolBankReconciliationDetail_Value As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolBankReconciliationDetail_Observations As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolBankReconciliationDetail_CreationUser As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolBankReconciliationDetail_ConfirmationUser As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolBankReconciliationExtractDetail_DocumentType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolBankReconciliationExtractDetail_DocumentDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolBankReconciliationExtractDetail_Description As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolBankReconciliationExtractDetail_DocumentNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolBankReconciliationExtractDetail_Nature As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolBankReconciliationExtractDetail_Value As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDPceAddExtractDetail As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents INDLciAddExtractDetail As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDpccExtractDetails As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDbtnAddExtractDetail As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDtxtExtractDocumentValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDSleExtractNature As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDgvExtractNature As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDtxtExtractDocument As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDtxtExtractDetail As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDdeExtractDocumentDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDSleExtractDocumentType As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDgvExtractDocumentType As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciExtractDocumentType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciExtractDocumentDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciExtractDetail As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciExtractDocument As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciExtractNature As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciExtractDocumentValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDcolExtractDocumentType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolExtractNature As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepSleDetailDocumentType As DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit
    Friend WithEvents INDrepGvDetailDocumentType As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDrepSleDetailNature As DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit
    Friend WithEvents INDrepGvDetailNature As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDrepSleExtractDetailDocumentType As DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit
    Friend WithEvents INDrepGvExtractDetailDocumentType As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDrepColExtractDetailDocumentType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepSleExtractDetailNature As DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit
    Friend WithEvents INDrepGvExtractDetailNature As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDrepColExtractDetailNature As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepTxtExtractDetailValue As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
End Class
