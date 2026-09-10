Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmBankConciliationAutomatic
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmBankConciliationAutomatic))
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim ButtonImageOptions1 As DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions = New DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions()
        Me.INDgvNoteCashReceipts = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcTreasury = New DevExpress.XtraGrid.GridControl()
        Me.INDgvTreasury = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDcolBankReconciliationDetail_Select = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepSelectOption = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.INDcolBankReconciliationDetail_DocumentDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolBankReconciliationDetail_Code = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolBankReconciliationDetail_DocumenType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepSleDetailDocumentType = New DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit()
        Me.INDcolBankReconciliationDetail_ThirdPartyNitName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolBankReconciliationDetail_DocumentNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolBankReconciliationDetail_Nature = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepSleDetailNature = New DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit()
        Me.INDcolBankReconciliationDetail_Value = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolBankReconciliationDetail_Observations = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolBankReconciliationDetail_CreationUser = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolBankReconciliationDetail_ConfirmationUser = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColComments = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColReconciled = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRepReconciled = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.INDColActions = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRipcActions = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDPccActions = New DevExpress.XtraEditors.PopupContainerControl()
        Me.INDSbDiscard = New DevExpress.XtraEditors.SimpleButton()
        Me.INDSbPendingToReconciled = New DevExpress.XtraEditors.SimpleButton()
        Me.INDSbManualReconciliation = New DevExpress.XtraEditors.SimpleButton()
        Me.INDSbComment = New DevExpress.XtraEditors.SimpleButton()
        Me.INDColPeriod = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemPopupContainerEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit4 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlyRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDPccPendingActions = New DevExpress.XtraEditors.PopupContainerControl()
        Me.INDSbMoveToBookBank = New DevExpress.XtraEditors.SimpleButton()
        Me.INDPcComments = New DevExpress.XtraBars.PopupControlContainer(Me.components)
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.LayoutControl2 = New DevExpress.XtraLayout.LayoutControl()
        Me.LblComments = New System.Windows.Forms.Label()
        Me.INDMeComments = New DevExpress.XtraEditors.MemoEdit()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.INDBbiComment = New DevExpress.XtraBars.BarButtonItem()
        Me.INDBbiManualReconciliation = New DevExpress.XtraBars.BarButtonItem()
        Me.INDBbiPendingToReconciled = New DevExpress.XtraBars.BarButtonItem()
        Me.INDBbiDiscard = New DevExpress.XtraBars.BarButtonItem()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LayoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.PanelControl4 = New DevExpress.XtraEditors.PanelControl()
        Me.INDBtnAccept = New DevExpress.XtraEditors.SimpleButton()
        Me.INDPcNoteType = New DevExpress.XtraBars.PopupControlContainer(Me.components)
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.INDSleNoteType = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSeValue = New DevExpress.XtraEditors.SpinEdit()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciNoteType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.PanelControl3 = New DevExpress.XtraEditors.PanelControl()
        Me.BtnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.INDGcBankReconciliation = New DevExpress.XtraGrid.GridControl()
        Me.INDGvConciliation = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGColConsecutiveStatement = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGColDateStatement = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGColValueStatement = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGColComment = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGColReconciled = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRiceReconciled = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.INDColDocumentCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGColNature = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGColDocumentType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemSearchLookUpEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.RepositoryItemSearchLookUpEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit()
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGcBankStatements = New DevExpress.XtraGrid.GridControl()
        Me.INDGvBankStatements = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDcolBankExtract_Select = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRiceExtractBank = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.INDcolBankReconciliationAutomaticExtractDetail_TransactionDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolBankReconciliationAutomaticExtractDetail_ConsecutiveBank = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColBankDocumentType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepSleDocumentType = New DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit()
        Me.RepositoryItemSearchLookUpEdit3View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolBankReconciliationAutomaticExtractDetail_TransactionCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolBankReconciliationAutomaticExtractDetail_DescriptionTransaction = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolBankReconciliationAutomaticExtractDetail_BankCheck = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolBankReconciliationAutomaticExtractDetail_PaymentReferenceOne = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolBankReconciliationAutomaticExtractDetail_PaymentReferenceTwo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolBankReconciliationAutomaticExtractDetail_Nature = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolBankReconciliationAutomaticExtractDetail_NatureName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolBankReconciliationAutomaticExtractDetail_Value = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColExtractPeriod = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepSleExtractDetailDocumentType = New DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit()
        Me.INDrepGvExtractDetailDocumentType = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDrepColExtractDetailDocumentType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepSleExtractDetailNature = New DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit()
        Me.INDrepGvExtractDetailNature = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDrepColExtractDetailNature = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepTxtExtractDetailValue = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
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
        Me.INDseExtractValueMovement = New DevExpress.XtraEditors.SpinEdit()
        Me.INDGcPendingItemsToReconciled = New DevExpress.XtraGrid.GridControl()
        Me.INDGvPendingItemsToReconciled = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDcolPendingItemsToReconciled_Select = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRiceExtractBank1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.INDcolPendingItemsToReconciled_Origin = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolPendingItemsToReconciled_Code = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolPendingItemsToReconciled_Date = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolPendingItemsToReconciled_DescriptionTransaction = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColPendingItemsToReconciled_DocumentType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColPendingItemsToReconciled_Detail = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolPendingItemsToReconciled_NatureName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolPendingItemsToReconciled_Value = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColPendingItems_ConsecutiveBank = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColPendingItems_TransactionalCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColPendingItems_BankCheck = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColPendingItems_PaymentReferenceOne = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColPendingItems_PaymentReferenceTwo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColPendingitems_Comment = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColPendingActions = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRipcePendingActions = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDColStatusReconciled = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlygPrincipalInformation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciEntityBankAccount = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciDocumentDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciEntityBankAccountValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciDifferenceReconcile = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciExtractValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciExtractValueMovement = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygEntityBankAccountInformation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciBankReconciliationDetails = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgBankStatements = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgPendingItemsToReconciled = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem9 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDPopupActions = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.INDRipc_PccBankStatementsDetail = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDIcb_PccBankStatementsDetail = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoGridViewBankBook = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridViewBankStatement = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoSimpleButton11 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.RepositoryItemPopupContainerEdit41 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit21 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit11 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.IndigoGridViewPendingItems = New Presentation.Controls.IndigoGridView(Me.components)
        Me.BarManager2 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.BarDockControl1 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl2 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl3 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl4 = New DevExpress.XtraBars.BarDockControl()
        Me.INDBbiMoveBankBook = New DevExpress.XtraBars.BarButtonItem()
        Me.INDPopupPendingActions = New DevExpress.XtraBars.PopupMenu(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvNoteCashReceipts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcTreasury, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvTreasury, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepSelectOption, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepSleDetailDocumentType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepSleDetailNature, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRepReconciled, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRipcActions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPccActions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPccActions.SuspendLayout()
        CType(Me.RepositoryItemPopupContainerEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyRoot.SuspendLayout()
        CType(Me.INDPccPendingActions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPccPendingActions.SuspendLayout()
        CType(Me.INDPcComments, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPcComments.SuspendLayout()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl2.SuspendLayout()
        CType(Me.INDMeComments.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl4.SuspendLayout()
        CType(Me.INDPcNoteType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPcNoteType.SuspendLayout()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDSleNoteType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSeValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciNoteType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl3.SuspendLayout()
        CType(Me.INDGcBankReconciliation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvConciliation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRiceReconciled, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemSearchLookUpEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemSearchLookUpEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcBankStatements, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvBankStatements, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRiceExtractBank, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepSleDocumentType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemSearchLookUpEdit3View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepSleExtractDetailDocumentType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepGvExtractDetailDocumentType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepSleExtractDetailNature, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepGvExtractDetailNature, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepTxtExtractDetailValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDseDifferenceReconcile.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDseEndEntityBankAccountValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDseExtractValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteDocumentDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteDocumentDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleEntityBankAccount.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDseExtractValueMovement.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcPendingItemsToReconciled, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvPendingItemsToReconciled, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRiceExtractBank1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRipcePendingActions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygPrincipalInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciEntityBankAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDocumentDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciEntityBankAccountValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDifferenceReconcile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciExtractValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciExtractValueMovement, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygEntityBankAccountInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciBankReconciliationDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgBankStatements, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgPendingItemsToReconciled, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPopupActions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRipc_PccBankStatementsDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDIcb_PccBankStatementsDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridViewBankBook, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridViewBankStatement, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit41, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridViewPendingItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPopupPendingActions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyRoot)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 136)
        Me.INDPanelControlBase.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDPanelControlBase.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1295, 586)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Location = New System.Drawing.Point(0, 6)
        Me.ToolBars.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ToolBars.Size = New System.Drawing.Size(1295, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.BarraBotones.Size = New System.Drawing.Size(1295, 130)
        '
        'INDgvNoteCashReceipts
        '
        Me.INDgvNoteCashReceipts.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvNoteCashReceipts.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvNoteCashReceipts.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvNoteCashReceipts.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvNoteCashReceipts.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvNoteCashReceipts.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvNoteCashReceipts.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvNoteCashReceipts.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvNoteCashReceipts.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvNoteCashReceipts.Appearance.Row.Options.UseFont = True
        Me.INDgvNoteCashReceipts.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn9, Me.GridColumn10, Me.GridColumn11, Me.GridColumn13, Me.GridColumn12})
        Me.INDgvNoteCashReceipts.GridControl = Me.INDgcTreasury
        Me.INDgvNoteCashReceipts.Name = "INDgvNoteCashReceipts"
        Me.INDgvNoteCashReceipts.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvNoteCashReceipts.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvNoteCashReceipts.OptionsView.ShowAutoFilterRow = True
        Me.INDgvNoteCashReceipts.OptionsView.ShowGroupPanel = False
        Me.IndigoGridViewBankStatement.SetTemaIndigoMetro(Me.INDgvNoteCashReceipts, False)
        Me.IndigoGridViewPendingItems.SetTemaIndigoMetro(Me.INDgvNoteCashReceipts, False)
        Me.IndigoGridViewBankBook.SetTemaIndigoMetro(Me.INDgvNoteCashReceipts, False)
        Me.INDgvNoteCashReceipts.ViewCaption = "Recibos de caja"
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Código"
        Me.GridColumn9.FieldName = "Code"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 0
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "Fecha"
        Me.GridColumn10.FieldName = "DocumentDate"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 1
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "Valor"
        Me.GridColumn11.DisplayFormat.FormatString = "C2"
        Me.GridColumn11.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn11.FieldName = "Value"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.Visible = True
        Me.GridColumn11.VisibleIndex = 3
        '
        'GridColumn13
        '
        Me.GridColumn13.Caption = "Tercero"
        Me.GridColumn13.FieldName = "NitNameThirdParty"
        Me.GridColumn13.Name = "GridColumn13"
        Me.GridColumn13.Visible = True
        Me.GridColumn13.VisibleIndex = 2
        '
        'GridColumn12
        '
        Me.GridColumn12.Caption = "Franquicia"
        Me.GridColumn12.FieldName = "CardName"
        Me.GridColumn12.Name = "GridColumn12"
        Me.GridColumn12.Visible = True
        Me.GridColumn12.VisibleIndex = 4
        '
        'INDgcTreasury
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcTreasury, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcTreasury, Nothing)
        Me.INDgcTreasury.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcTreasury, True)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcTreasury, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcTreasury, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcTreasury, False)
        GridLevelNode1.LevelTemplate = Me.INDgvNoteCashReceipts
        GridLevelNode1.RelationName = "ListCashReceipts"
        Me.INDgcTreasury.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.INDgcTreasury.Location = New System.Drawing.Point(438, 81)
        Me.INDgcTreasury.MainView = Me.INDgvTreasury
        Me.INDgcTreasury.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDgcTreasury.MinimumSize = New System.Drawing.Size(584, 1)
        Me.INDgcTreasury.Name = "INDgcTreasury"
        Me.INDgcTreasury.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDRepReconciled, Me.INDRipcActions})
        Me.INDgcTreasury.ShowOnlyPredefinedDetails = True
        Me.INDgcTreasury.Size = New System.Drawing.Size(677, 454)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcTreasury, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcTreasury.TabIndex = 6
        Me.INDgcTreasury.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvTreasury, Me.INDgvNoteCashReceipts})
        '
        'INDgvTreasury
        '
        Me.INDgvTreasury.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvTreasury.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvTreasury.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvTreasury.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvTreasury.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvTreasury.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvTreasury.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvTreasury.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvTreasury.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvTreasury.Appearance.Row.Options.UseFont = True
        Me.INDgvTreasury.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgvTreasury.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvTreasury.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDcolBankReconciliationDetail_Select, Me.INDcolBankReconciliationDetail_DocumentDate, Me.INDcolBankReconciliationDetail_Code, Me.INDcolBankReconciliationDetail_DocumenType, Me.INDcolBankReconciliationDetail_ThirdPartyNitName, Me.INDcolBankReconciliationDetail_DocumentNumber, Me.INDcolBankReconciliationDetail_Nature, Me.INDcolBankReconciliationDetail_Value, Me.INDcolBankReconciliationDetail_Observations, Me.INDcolBankReconciliationDetail_CreationUser, Me.INDcolBankReconciliationDetail_ConfirmationUser, Me.INDColComments, Me.INDColReconciled, Me.INDColActions, Me.INDColPeriod})
        Me.INDgvTreasury.GridControl = Me.INDgcTreasury
        Me.INDgvTreasury.GroupCount = 1
        Me.INDgvTreasury.Name = "INDgvTreasury"
        Me.INDgvTreasury.OptionsSelection.MultiSelect = True
        Me.INDgvTreasury.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvTreasury.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvTreasury.OptionsView.ShowAutoFilterRow = True
        Me.INDgvTreasury.OptionsView.ShowFooter = True
        Me.INDgvTreasury.OptionsView.ShowGroupPanel = False
        Me.INDgvTreasury.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.INDColPeriod, DevExpress.Data.ColumnSortOrder.Ascending), New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.INDColReconciled, DevExpress.Data.ColumnSortOrder.Descending), New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.INDcolBankReconciliationDetail_DocumentDate, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridViewBankStatement.SetTemaIndigoMetro(Me.INDgvTreasury, False)
        Me.IndigoGridViewPendingItems.SetTemaIndigoMetro(Me.INDgvTreasury, False)
        Me.IndigoGridViewBankBook.SetTemaIndigoMetro(Me.INDgvTreasury, False)
        '
        'INDcolBankReconciliationDetail_Select
        '
        Me.INDcolBankReconciliationDetail_Select.Caption = "Sel."
        Me.INDcolBankReconciliationDetail_Select.ColumnEdit = Me.INDrepSelectOption
        Me.INDcolBankReconciliationDetail_Select.FieldName = "Checked"
        Me.INDcolBankReconciliationDetail_Select.MinWidth = 21
        Me.INDcolBankReconciliationDetail_Select.Name = "INDcolBankReconciliationDetail_Select"
        Me.INDcolBankReconciliationDetail_Select.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDcolBankReconciliationDetail_Select.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDcolBankReconciliationDetail_Select.OptionsColumn.AllowMove = False
        Me.INDcolBankReconciliationDetail_Select.OptionsColumn.AllowShowHide = False
        Me.INDcolBankReconciliationDetail_Select.OptionsColumn.AllowSize = False
        Me.INDcolBankReconciliationDetail_Select.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDcolBankReconciliationDetail_Select.OptionsColumn.FixedWidth = True
        Me.INDcolBankReconciliationDetail_Select.UnboundType = DevExpress.Data.UnboundColumnType.[Boolean]
        Me.INDcolBankReconciliationDetail_Select.Visible = True
        Me.INDcolBankReconciliationDetail_Select.VisibleIndex = 0
        Me.INDcolBankReconciliationDetail_Select.Width = 37
        '
        'INDrepSelectOption
        '
        Me.INDrepSelectOption.AutoHeight = False
        Me.INDrepSelectOption.Name = "INDrepSelectOption"
        '
        'INDcolBankReconciliationDetail_DocumentDate
        '
        Me.INDcolBankReconciliationDetail_DocumentDate.Caption = "Fecha"
        Me.INDcolBankReconciliationDetail_DocumentDate.DisplayFormat.FormatString = "d"
        Me.INDcolBankReconciliationDetail_DocumentDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDcolBankReconciliationDetail_DocumentDate.FieldName = "DocumentDate"
        Me.INDcolBankReconciliationDetail_DocumentDate.MinWidth = 21
        Me.INDcolBankReconciliationDetail_DocumentDate.Name = "INDcolBankReconciliationDetail_DocumentDate"
        Me.INDcolBankReconciliationDetail_DocumentDate.OptionsColumn.AllowEdit = False
        Me.INDcolBankReconciliationDetail_DocumentDate.OptionsColumn.AllowFocus = False
        Me.INDcolBankReconciliationDetail_DocumentDate.SortMode = DevExpress.XtraGrid.ColumnSortMode.Custom
        Me.INDcolBankReconciliationDetail_DocumentDate.Visible = True
        Me.INDcolBankReconciliationDetail_DocumentDate.VisibleIndex = 1
        Me.INDcolBankReconciliationDetail_DocumentDate.Width = 91
        '
        'INDcolBankReconciliationDetail_Code
        '
        Me.INDcolBankReconciliationDetail_Code.Caption = "Código"
        Me.INDcolBankReconciliationDetail_Code.FieldName = "EntityCode"
        Me.INDcolBankReconciliationDetail_Code.MinWidth = 21
        Me.INDcolBankReconciliationDetail_Code.Name = "INDcolBankReconciliationDetail_Code"
        Me.INDcolBankReconciliationDetail_Code.OptionsColumn.AllowEdit = False
        Me.INDcolBankReconciliationDetail_Code.OptionsColumn.AllowFocus = False
        Me.INDcolBankReconciliationDetail_Code.Visible = True
        Me.INDcolBankReconciliationDetail_Code.VisibleIndex = 2
        Me.INDcolBankReconciliationDetail_Code.Width = 87
        '
        'INDcolBankReconciliationDetail_DocumenType
        '
        Me.INDcolBankReconciliationDetail_DocumenType.Caption = "Tipo documento"
        Me.INDcolBankReconciliationDetail_DocumenType.ColumnEdit = Me.INDrepSleDetailDocumentType
        Me.INDcolBankReconciliationDetail_DocumenType.FieldName = "DocumentType"
        Me.INDcolBankReconciliationDetail_DocumenType.MinWidth = 21
        Me.INDcolBankReconciliationDetail_DocumenType.Name = "INDcolBankReconciliationDetail_DocumenType"
        Me.INDcolBankReconciliationDetail_DocumenType.OptionsColumn.AllowEdit = False
        Me.INDcolBankReconciliationDetail_DocumenType.OptionsColumn.AllowFocus = False
        Me.INDcolBankReconciliationDetail_DocumenType.Visible = True
        Me.INDcolBankReconciliationDetail_DocumenType.VisibleIndex = 6
        Me.INDcolBankReconciliationDetail_DocumenType.Width = 93
        '
        'INDrepSleDetailDocumentType
        '
        Me.INDrepSleDetailDocumentType.AutoHeight = False
        Me.INDrepSleDetailDocumentType.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrepSleDetailDocumentType.DisplayMember = "Item2"
        Me.INDrepSleDetailDocumentType.Name = "INDrepSleDetailDocumentType"
        Me.INDrepSleDetailDocumentType.NullText = ""
        Me.INDrepSleDetailDocumentType.ValueMember = "Item1"
        '
        'INDcolBankReconciliationDetail_ThirdPartyNitName
        '
        Me.INDcolBankReconciliationDetail_ThirdPartyNitName.Caption = "Tercero"
        Me.INDcolBankReconciliationDetail_ThirdPartyNitName.FieldName = "ThirdPartyNitName"
        Me.INDcolBankReconciliationDetail_ThirdPartyNitName.MinWidth = 21
        Me.INDcolBankReconciliationDetail_ThirdPartyNitName.Name = "INDcolBankReconciliationDetail_ThirdPartyNitName"
        Me.INDcolBankReconciliationDetail_ThirdPartyNitName.OptionsColumn.AllowEdit = False
        Me.INDcolBankReconciliationDetail_ThirdPartyNitName.OptionsColumn.AllowFocus = False
        Me.INDcolBankReconciliationDetail_ThirdPartyNitName.Width = 93
        '
        'INDcolBankReconciliationDetail_DocumentNumber
        '
        Me.INDcolBankReconciliationDetail_DocumentNumber.Caption = "Documento"
        Me.INDcolBankReconciliationDetail_DocumentNumber.FieldName = "DocumentNumber"
        Me.INDcolBankReconciliationDetail_DocumentNumber.MinWidth = 21
        Me.INDcolBankReconciliationDetail_DocumentNumber.Name = "INDcolBankReconciliationDetail_DocumentNumber"
        Me.INDcolBankReconciliationDetail_DocumentNumber.OptionsColumn.AllowEdit = False
        Me.INDcolBankReconciliationDetail_DocumentNumber.OptionsColumn.AllowFocus = False
        Me.INDcolBankReconciliationDetail_DocumentNumber.Visible = True
        Me.INDcolBankReconciliationDetail_DocumentNumber.VisibleIndex = 3
        Me.INDcolBankReconciliationDetail_DocumentNumber.Width = 93
        '
        'INDcolBankReconciliationDetail_Nature
        '
        Me.INDcolBankReconciliationDetail_Nature.Caption = "Naturaleza"
        Me.INDcolBankReconciliationDetail_Nature.ColumnEdit = Me.INDrepSleDetailNature
        Me.INDcolBankReconciliationDetail_Nature.FieldName = "Nature"
        Me.INDcolBankReconciliationDetail_Nature.MinWidth = 21
        Me.INDcolBankReconciliationDetail_Nature.Name = "INDcolBankReconciliationDetail_Nature"
        Me.INDcolBankReconciliationDetail_Nature.OptionsColumn.AllowEdit = False
        Me.INDcolBankReconciliationDetail_Nature.OptionsColumn.AllowFocus = False
        Me.INDcolBankReconciliationDetail_Nature.Visible = True
        Me.INDcolBankReconciliationDetail_Nature.VisibleIndex = 4
        Me.INDcolBankReconciliationDetail_Nature.Width = 93
        '
        'INDrepSleDetailNature
        '
        Me.INDrepSleDetailNature.AutoHeight = False
        Me.INDrepSleDetailNature.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrepSleDetailNature.DisplayMember = "Item2"
        Me.INDrepSleDetailNature.Name = "INDrepSleDetailNature"
        Me.INDrepSleDetailNature.NullText = ""
        Me.INDrepSleDetailNature.ValueMember = "Item1"
        '
        'INDcolBankReconciliationDetail_Value
        '
        Me.INDcolBankReconciliationDetail_Value.Caption = "Valor"
        Me.INDcolBankReconciliationDetail_Value.DisplayFormat.FormatString = "c2"
        Me.INDcolBankReconciliationDetail_Value.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDcolBankReconciliationDetail_Value.FieldName = "Value"
        Me.INDcolBankReconciliationDetail_Value.MinWidth = 21
        Me.INDcolBankReconciliationDetail_Value.Name = "INDcolBankReconciliationDetail_Value"
        Me.INDcolBankReconciliationDetail_Value.OptionsColumn.AllowEdit = False
        Me.INDcolBankReconciliationDetail_Value.OptionsColumn.AllowFocus = False
        Me.INDcolBankReconciliationDetail_Value.Visible = True
        Me.INDcolBankReconciliationDetail_Value.VisibleIndex = 5
        Me.INDcolBankReconciliationDetail_Value.Width = 115
        '
        'INDcolBankReconciliationDetail_Observations
        '
        Me.INDcolBankReconciliationDetail_Observations.Caption = "Detalle"
        Me.INDcolBankReconciliationDetail_Observations.FieldName = "Observations"
        Me.INDcolBankReconciliationDetail_Observations.MinWidth = 21
        Me.INDcolBankReconciliationDetail_Observations.Name = "INDcolBankReconciliationDetail_Observations"
        Me.INDcolBankReconciliationDetail_Observations.OptionsColumn.AllowEdit = False
        Me.INDcolBankReconciliationDetail_Observations.OptionsColumn.AllowFocus = False
        '
        'INDcolBankReconciliationDetail_CreationUser
        '
        Me.INDcolBankReconciliationDetail_CreationUser.Caption = "Usuario Creación"
        Me.INDcolBankReconciliationDetail_CreationUser.FieldName = "CreationUser"
        Me.INDcolBankReconciliationDetail_CreationUser.MinWidth = 21
        Me.INDcolBankReconciliationDetail_CreationUser.Name = "INDcolBankReconciliationDetail_CreationUser"
        Me.INDcolBankReconciliationDetail_CreationUser.OptionsColumn.AllowEdit = False
        Me.INDcolBankReconciliationDetail_CreationUser.OptionsColumn.AllowFocus = False
        '
        'INDcolBankReconciliationDetail_ConfirmationUser
        '
        Me.INDcolBankReconciliationDetail_ConfirmationUser.Caption = "Usuario Confirmación"
        Me.INDcolBankReconciliationDetail_ConfirmationUser.FieldName = "ConfirmationUser"
        Me.INDcolBankReconciliationDetail_ConfirmationUser.MinWidth = 21
        Me.INDcolBankReconciliationDetail_ConfirmationUser.Name = "INDcolBankReconciliationDetail_ConfirmationUser"
        Me.INDcolBankReconciliationDetail_ConfirmationUser.OptionsColumn.AllowEdit = False
        Me.INDcolBankReconciliationDetail_ConfirmationUser.OptionsColumn.AllowFocus = False
        '
        'INDColComments
        '
        Me.INDColComments.Caption = "Comentario"
        Me.INDColComments.FieldName = "Comments"
        Me.INDColComments.MinWidth = 21
        Me.INDColComments.Name = "INDColComments"
        Me.INDColComments.Width = 81
        '
        'INDColReconciled
        '
        Me.INDColReconciled.Caption = "Conciliado"
        Me.INDColReconciled.ColumnEdit = Me.INDRepReconciled
        Me.INDColReconciled.FieldName = "Reconciled"
        Me.INDColReconciled.Name = "INDColReconciled"
        Me.INDColReconciled.OptionsColumn.AllowEdit = False
        Me.INDColReconciled.OptionsColumn.AllowFocus = False
        Me.INDColReconciled.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDColReconciled.SortMode = DevExpress.XtraGrid.ColumnSortMode.Custom
        '
        'INDRepReconciled
        '
        Me.INDRepReconciled.AutoHeight = False
        Me.INDRepReconciled.CheckBoxOptions.Style = DevExpress.XtraEditors.Controls.CheckBoxStyle.Custom
        Me.INDRepReconciled.ImageOptions.ImageChecked = CType(resources.GetObject("INDRepReconciled.ImageOptions.ImageChecked"), System.Drawing.Image)
        Me.INDRepReconciled.ImageOptions.ImageUnchecked = CType(resources.GetObject("INDRepReconciled.ImageOptions.ImageUnchecked"), System.Drawing.Image)
        Me.INDRepReconciled.Name = "INDRepReconciled"
        '
        'INDColActions
        '
        Me.INDColActions.Caption = "Acciones"
        Me.INDColActions.ColumnEdit = Me.INDRipcActions
        Me.INDColActions.Name = "INDColActions"
        Me.INDColActions.Visible = True
        Me.INDColActions.VisibleIndex = 7
        '
        'INDRipcActions
        '
        Me.INDRipcActions.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDRipcActions.Name = "INDRipcActions"
        Me.INDRipcActions.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.NoBorder
        Me.INDRipcActions.PopupControl = Me.INDPccActions
        Me.INDRipcActions.PopupSizeable = False
        Me.INDRipcActions.ShowPopupCloseButton = False
        Me.INDRipcActions.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'INDPccActions
        '
        Me.INDPccActions.AutoSize = True
        Me.INDPccActions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.INDPccActions.Controls.Add(Me.INDSbDiscard)
        Me.INDPccActions.Controls.Add(Me.INDSbPendingToReconciled)
        Me.INDPccActions.Controls.Add(Me.INDSbManualReconciliation)
        Me.INDPccActions.Controls.Add(Me.INDSbComment)
        Me.INDPccActions.Location = New System.Drawing.Point(453, 347)
        Me.INDPccActions.Name = "INDPccActions"
        Me.INDPccActions.Size = New System.Drawing.Size(208, 176)
        Me.INDPccActions.TabIndex = 59
        '
        'INDSbDiscard
        '
        Me.INDSbDiscard.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.INDSbDiscard.Appearance.Options.UseFont = True
        Me.INDSbDiscard.ImageOptions.Image = CType(resources.GetObject("INDSbDiscard.ImageOptions.Image"), System.Drawing.Image)
        Me.INDSbDiscard.Location = New System.Drawing.Point(4, 132)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDSbDiscard, False)
        Me.IndigoSimpleButton11.SetModernUiIndigo(Me.INDSbDiscard, False)
        Me.INDSbDiscard.Name = "INDSbDiscard"
        Me.INDSbDiscard.Size = New System.Drawing.Size(202, 42)
        Me.INDSbDiscard.TabIndex = 3
        Me.INDSbDiscard.Text = "Descartar"
        '
        'INDSbPendingToReconciled
        '
        Me.INDSbPendingToReconciled.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.INDSbPendingToReconciled.Appearance.Options.UseFont = True
        Me.INDSbPendingToReconciled.ImageOptions.Image = CType(resources.GetObject("INDSbPendingToReconciled.ImageOptions.Image"), System.Drawing.Image)
        Me.INDSbPendingToReconciled.Location = New System.Drawing.Point(4, 89)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDSbPendingToReconciled, False)
        Me.IndigoSimpleButton11.SetModernUiIndigo(Me.INDSbPendingToReconciled, False)
        Me.INDSbPendingToReconciled.Name = "INDSbPendingToReconciled"
        Me.INDSbPendingToReconciled.Size = New System.Drawing.Size(202, 42)
        Me.INDSbPendingToReconciled.TabIndex = 2
        Me.INDSbPendingToReconciled.Text = "Pendiente por conciliar"
        '
        'INDSbManualReconciliation
        '
        Me.INDSbManualReconciliation.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.INDSbManualReconciliation.Appearance.Options.UseFont = True
        Me.INDSbManualReconciliation.ImageOptions.SvgImage = Global.Presentation.Treasury.My.Resources.Resources.Conciliar
        Me.INDSbManualReconciliation.ImageOptions.SvgImageSize = New System.Drawing.Size(32, 32)
        Me.INDSbManualReconciliation.Location = New System.Drawing.Point(4, 46)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDSbManualReconciliation, False)
        Me.IndigoSimpleButton11.SetModernUiIndigo(Me.INDSbManualReconciliation, False)
        Me.INDSbManualReconciliation.Name = "INDSbManualReconciliation"
        Me.INDSbManualReconciliation.Size = New System.Drawing.Size(202, 42)
        Me.INDSbManualReconciliation.TabIndex = 1
        Me.INDSbManualReconciliation.Text = "Conciliación Manual"
        '
        'INDSbComment
        '
        Me.INDSbComment.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.INDSbComment.Appearance.Options.UseFont = True
        Me.INDSbComment.ImageOptions.SvgImage = CType(resources.GetObject("INDSbComment.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.INDSbComment.ImageOptions.SvgImageSize = New System.Drawing.Size(32, 32)
        Me.INDSbComment.Location = New System.Drawing.Point(3, 3)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDSbComment, False)
        Me.IndigoSimpleButton11.SetModernUiIndigo(Me.INDSbComment, False)
        Me.INDSbComment.Name = "INDSbComment"
        Me.INDSbComment.Size = New System.Drawing.Size(202, 42)
        Me.INDSbComment.TabIndex = 0
        Me.INDSbComment.Text = "Comentario"
        '
        'INDColPeriod
        '
        Me.INDColPeriod.Caption = "Periodo"
        Me.INDColPeriod.FieldName = "PeriodName"
        Me.INDColPeriod.Name = "INDColPeriod"
        Me.INDColPeriod.UnboundExpression = "Iif(IsNullOrEmpty([Period]), 'Registro del periodo actual', [Period])"
        Me.INDColPeriod.UnboundType = DevExpress.Data.UnboundColumnType.[String]
        Me.INDColPeriod.Visible = True
        Me.INDColPeriod.VisibleIndex = 8
        '
        'RepositoryItemPopupContainerEdit3
        '
        Me.RepositoryItemPopupContainerEdit3.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit3.Name = "RepositoryItemPopupContainerEdit3"
        '
        'RepositoryItemPopupContainerEdit4
        '
        Me.RepositoryItemPopupContainerEdit4.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit4.Name = "RepositoryItemPopupContainerEdit4"
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
        Me.CtrNavigationControlPanel1.LayoutControl = Me.INDlyRoot
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 8)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 576)
        Me.CtrNavigationControlPanel1.TabIndex = 0
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'INDlyRoot
        '
        Me.INDlyRoot.Controls.Add(Me.INDPccPendingActions)
        Me.INDlyRoot.Controls.Add(Me.INDPccActions)
        Me.INDlyRoot.Controls.Add(Me.INDPcComments)
        Me.INDlyRoot.Controls.Add(Me.INDPcNoteType)
        Me.INDlyRoot.Controls.Add(Me.INDGcBankReconciliation)
        Me.INDlyRoot.Controls.Add(Me.INDGcBankStatements)
        Me.INDlyRoot.Controls.Add(Me.INDgcTreasury)
        Me.INDlyRoot.Controls.Add(Me.INDseDifferenceReconcile)
        Me.INDlyRoot.Controls.Add(Me.INDseEndEntityBankAccountValue)
        Me.INDlyRoot.Controls.Add(Me.INDseExtractValue)
        Me.INDlyRoot.Controls.Add(Me.INDdteDocumentDate)
        Me.INDlyRoot.Controls.Add(Me.INDsleEntityBankAccount)
        Me.INDlyRoot.Controls.Add(Me.INDbtnCode)
        Me.INDlyRoot.Controls.Add(Me.INDseExtractValueMovement)
        Me.INDlyRoot.Controls.Add(Me.INDGcPendingItemsToReconciled)
        Me.INDlyRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyRoot.Location = New System.Drawing.Point(202, 8)
        Me.INDlyRoot.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDlyRoot.Name = "INDlyRoot"
        Me.INDlyRoot.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(1030, 451, 650, 400)
        Me.INDlyRoot.Root = Me.LayoutControlGroup1
        Me.INDlyRoot.Size = New System.Drawing.Size(1091, 576)
        Me.INDlyRoot.TabIndex = 1
        Me.INDlyRoot.Text = "LayoutControl1"
        '
        'INDPccPendingActions
        '
        Me.INDPccPendingActions.Controls.Add(Me.INDSbMoveToBookBank)
        Me.INDPccPendingActions.Location = New System.Drawing.Point(692, 415)
        Me.INDPccPendingActions.Name = "INDPccPendingActions"
        Me.INDPccPendingActions.Size = New System.Drawing.Size(207, 48)
        Me.INDPccPendingActions.TabIndex = 60
        '
        'INDSbMoveToBookBank
        '
        Me.INDSbMoveToBookBank.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.INDSbMoveToBookBank.Appearance.Options.UseFont = True
        Me.INDSbMoveToBookBank.ImageOptions.Image = CType(resources.GetObject("INDSbMoveToBookBank.ImageOptions.Image"), System.Drawing.Image)
        Me.INDSbMoveToBookBank.Location = New System.Drawing.Point(3, 3)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDSbMoveToBookBank, False)
        Me.IndigoSimpleButton11.SetModernUiIndigo(Me.INDSbMoveToBookBank, False)
        Me.INDSbMoveToBookBank.Name = "INDSbMoveToBookBank"
        Me.INDSbMoveToBookBank.Size = New System.Drawing.Size(202, 42)
        Me.INDSbMoveToBookBank.TabIndex = 0
        Me.INDSbMoveToBookBank.Text = "Mover al libro de bancos"
        '
        'INDPcComments
        '
        Me.INDPcComments.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDPcComments.Controls.Add(Me.PanelControl2)
        Me.INDPcComments.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.INDPcComments.Location = New System.Drawing.Point(303, 107)
        Me.INDPcComments.Manager = Me.BarManager1
        Me.INDPcComments.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDPcComments.Name = "INDPcComments"
        Me.INDPcComments.Size = New System.Drawing.Size(413, 219)
        Me.INDPcComments.TabIndex = 58
        Me.INDPcComments.Visible = False
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.LayoutControl2)
        Me.PanelControl2.Controls.Add(Me.PanelControl4)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl2.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(413, 219)
        Me.PanelControl2.TabIndex = 0
        '
        'LayoutControl2
        '
        Me.LayoutControl2.Controls.Add(Me.LblComments)
        Me.LayoutControl2.Controls.Add(Me.INDMeComments)
        Me.LayoutControl2.Controls.Add(Me.Label2)
        Me.LayoutControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl2.Location = New System.Drawing.Point(2, 2)
        Me.LayoutControl2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.LayoutControl2.Name = "LayoutControl2"
        Me.LayoutControl2.Root = Me.LayoutControlGroup3
        Me.LayoutControl2.Size = New System.Drawing.Size(409, 178)
        Me.LayoutControl2.TabIndex = 1
        Me.LayoutControl2.Text = "LayoutControl2"
        '
        'LblComments
        '
        Me.LblComments.Location = New System.Drawing.Point(12, 51)
        Me.LblComments.Name = "LblComments"
        Me.LblComments.Size = New System.Drawing.Size(143, 115)
        Me.LblComments.TabIndex = 8
        Me.LblComments.Text = "Partidas Conciliatorias"
        '
        'INDMeComments
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDMeComments, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDMeComments, False)
        Me.INDMeComments.Location = New System.Drawing.Point(159, 51)
        Me.IndigoTextEdit1.SetMascara(Me.INDMeComments, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDMeComments.MenuManager = Me.BarManager1
        Me.INDMeComments.Name = "INDMeComments"
        Me.INDMeComments.Size = New System.Drawing.Size(238, 115)
        Me.INDMeComments.StyleController = Me.LayoutControl2
        Me.INDMeComments.TabIndex = 7
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDMeComments, 0)
        '
        'BarManager1
        '
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.INDBbiComment, Me.INDBbiManualReconciliation, Me.INDBbiPendingToReconciled, Me.INDBbiDiscard})
        Me.BarManager1.MaxItemId = 4
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 6)
        Me.barDockControlTop.Manager = Me.BarManager1
        Me.barDockControlTop.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.barDockControlTop.Size = New System.Drawing.Size(1295, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 722)
        Me.barDockControlBottom.Manager = Me.BarManager1
        Me.barDockControlBottom.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.barDockControlBottom.Size = New System.Drawing.Size(1295, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 6)
        Me.barDockControlLeft.Manager = Me.BarManager1
        Me.barDockControlLeft.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 716)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(1295, 6)
        Me.barDockControlRight.Manager = Me.BarManager1
        Me.barDockControlRight.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 716)
        '
        'INDBbiComment
        '
        Me.INDBbiComment.Caption = "Comentario"
        Me.INDBbiComment.Id = 0
        Me.INDBbiComment.ImageOptions.SvgImage = CType(resources.GetObject("INDBbiComment.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.INDBbiComment.ImageOptions.SvgImageSize = New System.Drawing.Size(32, 32)
        Me.INDBbiComment.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDBbiComment.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDBbiComment.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.INDBbiComment.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDBbiComment.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.INDBbiComment.ItemAppearance.Normal.Options.UseFont = True
        Me.INDBbiComment.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.INDBbiComment.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDBbiComment.ItemInMenuAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.INDBbiComment.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.INDBbiComment.ItemInMenuAppearance.Normal.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.INDBbiComment.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.INDBbiComment.ItemInMenuAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.INDBbiComment.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.INDBbiComment.Name = "INDBbiComment"
        '
        'INDBbiManualReconciliation
        '
        Me.INDBbiManualReconciliation.Caption = "Conciliación manual"
        Me.INDBbiManualReconciliation.Id = 1
        Me.INDBbiManualReconciliation.ImageOptions.SvgImage = Global.Presentation.Treasury.My.Resources.Resources.Conciliar
        Me.INDBbiManualReconciliation.ImageOptions.SvgImageSize = New System.Drawing.Size(32, 32)
        Me.INDBbiManualReconciliation.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.INDBbiManualReconciliation.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDBbiManualReconciliation.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.INDBbiManualReconciliation.ItemAppearance.Normal.Options.UseFont = True
        Me.INDBbiManualReconciliation.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.INDBbiManualReconciliation.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDBbiManualReconciliation.ItemInMenuAppearance.Normal.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.INDBbiManualReconciliation.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.INDBbiManualReconciliation.ItemInMenuAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.INDBbiManualReconciliation.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.INDBbiManualReconciliation.Name = "INDBbiManualReconciliation"
        '
        'INDBbiPendingToReconciled
        '
        Me.INDBbiPendingToReconciled.Caption = "Pendiente por conciliar"
        Me.INDBbiPendingToReconciled.Id = 2
        Me.INDBbiPendingToReconciled.ImageOptions.Image = CType(resources.GetObject("INDBbiPendingToReconciled.ImageOptions.Image"), System.Drawing.Image)
        Me.INDBbiPendingToReconciled.ImageOptions.LargeImage = CType(resources.GetObject("INDBbiPendingToReconciled.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.INDBbiPendingToReconciled.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.INDBbiPendingToReconciled.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDBbiPendingToReconciled.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.INDBbiPendingToReconciled.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDBbiPendingToReconciled.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.INDBbiPendingToReconciled.ItemAppearance.Normal.Options.UseFont = True
        Me.INDBbiPendingToReconciled.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.INDBbiPendingToReconciled.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDBbiPendingToReconciled.Name = "INDBbiPendingToReconciled"
        '
        'INDBbiDiscard
        '
        Me.INDBbiDiscard.Caption = "Descartar"
        Me.INDBbiDiscard.Id = 3
        Me.INDBbiDiscard.ImageOptions.Image = CType(resources.GetObject("INDBbiDiscard.ImageOptions.Image"), System.Drawing.Image)
        Me.INDBbiDiscard.ImageOptions.LargeImage = CType(resources.GetObject("INDBbiDiscard.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.INDBbiDiscard.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.INDBbiDiscard.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDBbiDiscard.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.INDBbiDiscard.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDBbiDiscard.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.INDBbiDiscard.ItemAppearance.Normal.Options.UseFont = True
        Me.INDBbiDiscard.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.INDBbiDiscard.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDBbiDiscard.Name = "INDBbiDiscard"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(12, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(385, 35)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Comentarios"
        '
        'LayoutControlGroup3
        '
        Me.LayoutControlGroup3.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup3.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup3.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup3, False)
        Me.LayoutControlGroup3.CustomizationFormText = "LayoutControlGroup2"
        Me.LayoutControlGroup3.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup3.GroupBordersVisible = False
        Me.LayoutControlGroup3.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem3, Me.LayoutControlItem7, Me.LayoutControlItem8})
        Me.LayoutControlGroup3.Name = "Root"
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(409, 178)
        Me.LayoutControlGroup3.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.Label2
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem3.Name = "LayoutControlItem5"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(389, 39)
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = False
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.INDMeComments
        Me.LayoutControlItem7.Location = New System.Drawing.Point(147, 39)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(242, 119)
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem7.TextVisible = False
        '
        'LayoutControlItem8
        '
        Me.LayoutControlItem8.Control = Me.LblComments
        Me.LayoutControlItem8.Location = New System.Drawing.Point(0, 39)
        Me.LayoutControlItem8.Name = "LayoutControlItem8"
        Me.LayoutControlItem8.Size = New System.Drawing.Size(147, 119)
        Me.LayoutControlItem8.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem8.TextVisible = False
        '
        'PanelControl4
        '
        Me.PanelControl4.Controls.Add(Me.INDBtnAccept)
        Me.PanelControl4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl4.Location = New System.Drawing.Point(2, 180)
        Me.PanelControl4.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PanelControl4.Name = "PanelControl4"
        Me.PanelControl4.Size = New System.Drawing.Size(409, 37)
        Me.PanelControl4.TabIndex = 2
        '
        'INDBtnAccept
        '
        Me.INDBtnAccept.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDBtnAccept.Location = New System.Drawing.Point(2, 2)
        Me.INDBtnAccept.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDBtnAccept, False)
        Me.IndigoSimpleButton11.SetModernUiIndigo(Me.INDBtnAccept, False)
        Me.INDBtnAccept.Name = "INDBtnAccept"
        Me.INDBtnAccept.Size = New System.Drawing.Size(405, 33)
        Me.INDBtnAccept.TabIndex = 7
        Me.INDBtnAccept.Text = "Aceptar"
        '
        'INDPcNoteType
        '
        Me.INDPcNoteType.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDPcNoteType.Controls.Add(Me.PanelControl1)
        Me.INDPcNoteType.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.INDPcNoteType.Location = New System.Drawing.Point(711, 170)
        Me.INDPcNoteType.Manager = Me.BarManager1
        Me.INDPcNoteType.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDPcNoteType.Name = "INDPcNoteType"
        Me.INDPcNoteType.Size = New System.Drawing.Size(431, 219)
        Me.INDPcNoteType.TabIndex = 56
        Me.INDPcNoteType.Visible = False
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.LayoutControl1)
        Me.PanelControl1.Controls.Add(Me.PanelControl3)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(431, 219)
        Me.PanelControl1.TabIndex = 0
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.Label1)
        Me.LayoutControl1.Controls.Add(Me.INDSleNoteType)
        Me.LayoutControl1.Controls.Add(Me.INDSeValue)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(2, 2)
        Me.LayoutControl1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup2
        Me.LayoutControl1.Size = New System.Drawing.Size(427, 178)
        Me.LayoutControl1.TabIndex = 1
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(12, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(403, 34)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Notas de Tesorería"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'INDSleNoteType
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleNoteType, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleNoteType, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleNoteType, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleNoteType, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleNoteType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleNoteType, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleNoteType, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleNoteType, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleNoteType, False)
        Me.INDSleNoteType.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleNoteType, True)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleNoteType, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleNoteType, False)
        Me.INDSleNoteType.Location = New System.Drawing.Point(12, 76)
        Me.INDSleNoteType.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleNoteType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleNoteType.Name = "INDSleNoteType"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleNoteType, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleNoteType, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleNoteType, False)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDSleNoteType, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleNoteType, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleNoteType, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleNoteType, False)
        Me.INDSleNoteType.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleNoteType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleNoteType.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleNoteType.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleNoteType.Properties.Appearance.Options.UseFont = True
        Me.INDSleNoteType.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleNoteType.Properties.Appearance.Options.UseTextOptions = True
        Me.INDSleNoteType.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.INDSleNoteType.Properties.AppearanceDisabled.Options.UseTextOptions = True
        Me.INDSleNoteType.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.INDSleNoteType.Properties.AppearanceDropDown.Options.UseTextOptions = True
        Me.INDSleNoteType.Properties.AppearanceDropDown.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.INDSleNoteType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleNoteType.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSleNoteType.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleNoteType.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleNoteType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSleNoteType.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSleNoteType.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleNoteType.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDSleNoteType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleNoteType.Properties.DisplayMember = "Name"
        Me.INDSleNoteType.Properties.NullText = ""
        Me.INDSleNoteType.Properties.PopupFormMinSize = New System.Drawing.Size(501, 0)
        Me.INDSleNoteType.Properties.PopupSizeable = False
        Me.INDSleNoteType.Properties.PopupView = Me.GridView1
        Me.INDSleNoteType.Properties.ShowClearButton = False
        Me.INDSleNoteType.Properties.ShowFooter = False
        Me.INDSleNoteType.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleNoteType, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleNoteType, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleNoteType, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleNoteType, True)
        Me.INDSleNoteType.Size = New System.Drawing.Size(403, 28)
        Me.INDSleNoteType.StyleController = Me.LayoutControl1
        Me.INDSleNoteType.TabIndex = 0
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleNoteType, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleNoteType, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleNoteType, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleNoteType, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleNoteType, False)
        '
        'GridView1
        '
        Me.GridView1.Appearance.ColumnFilterButton.Options.UseTextOptions = True
        Me.GridView1.Appearance.ColumnFilterButton.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.GridView1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView1.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView1.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView1.Appearance.FocusedRow.Options.UseForeColor = True
        Me.GridView1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.GroupRow.Options.UseFont = True
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Appearance.ViewCaption.Options.UseTextOptions = True
        Me.GridView1.Appearance.ViewCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn8})
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsFind.FindFilterColumns = "AccountReceivableId.InvoiceNumber"
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.IndigoGridViewBankStatement.SetTemaIndigoMetro(Me.GridView1, False)
        Me.IndigoGridViewPendingItems.SetTemaIndigoMetro(Me.GridView1, False)
        Me.IndigoGridViewBankBook.SetTemaIndigoMetro(Me.GridView1, False)
        '
        'GridColumn8
        '
        Me.GridColumn8.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.GridColumn8.Caption = "Tipo"
        Me.GridColumn8.FieldName = "Name"
        Me.GridColumn8.MinWidth = 21
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 0
        Me.GridColumn8.Width = 244
        '
        'INDSeValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSeValue, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSeValue, False)
        Me.INDSeValue.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDSeValue.EnterMoveNextControl = True
        Me.INDSeValue.Location = New System.Drawing.Point(12, 130)
        Me.INDSeValue.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDSeValue, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSeValue.Name = "INDSeValue"
        Me.INDSeValue.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDSeValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSeValue.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDSeValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDSeValue.Properties.Appearance.Options.UseFont = True
        Me.INDSeValue.Properties.Appearance.Options.UseForeColor = True
        Me.INDSeValue.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDSeValue.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSeValue.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeValue.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDSeValue.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSeValue.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSeValue.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSeValue.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDSeValue.Properties.DisplayFormat.FormatString = "c2"
        Me.INDSeValue.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.INDSeValue.Properties.Mask.EditMask = "c2"
        Me.INDSeValue.Properties.MaxLength = 60
        Me.INDSeValue.Properties.ReadOnly = True
        Me.INDSeValue.Size = New System.Drawing.Size(395, 28)
        Me.INDSeValue.StyleController = Me.LayoutControl1
        Me.INDSeValue.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSeValue, 0)
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
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem5, Me.INDLciValue, Me.INDLciNoteType})
        Me.LayoutControlGroup2.Name = "Root"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(427, 178)
        Me.LayoutControlGroup2.TextVisible = False
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.Label1
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(407, 38)
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextVisible = False
        '
        'INDLciValue
        '
        Me.INDLciValue.Control = Me.INDSeValue
        Me.INDLciValue.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciValue.CustomizationFormText = "Código del Extracto"
        Me.INDLciValue.Location = New System.Drawing.Point(0, 98)
        Me.INDLciValue.MaxSize = New System.Drawing.Size(399, 60)
        Me.INDLciValue.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciValue.Name = "INDLciValue"
        Me.INDLciValue.Size = New System.Drawing.Size(407, 60)
        Me.INDLciValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciValue.Text = "Valor"
        Me.INDLciValue.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciValue.TextSize = New System.Drawing.Size(34, 17)
        '
        'INDLciNoteType
        '
        Me.INDLciNoteType.AllowHide = False
        Me.INDLciNoteType.Control = Me.INDSleNoteType
        Me.INDLciNoteType.CustomizationFormText = "Concepto de Conciliación"
        Me.INDLciNoteType.Location = New System.Drawing.Point(0, 38)
        Me.INDLciNoteType.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciNoteType.Name = "INDLciNoteType"
        Me.INDLciNoteType.Size = New System.Drawing.Size(407, 60)
        Me.INDLciNoteType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciNoteType.Text = "Tipo de Nota"
        Me.INDLciNoteType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciNoteType.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciNoteType.TextSize = New System.Drawing.Size(99, 21)
        Me.INDLciNoteType.TextToControlDistance = 5
        '
        'PanelControl3
        '
        Me.PanelControl3.Controls.Add(Me.BtnAdd)
        Me.PanelControl3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl3.Location = New System.Drawing.Point(2, 180)
        Me.PanelControl3.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PanelControl3.Name = "PanelControl3"
        Me.PanelControl3.Size = New System.Drawing.Size(427, 37)
        Me.PanelControl3.TabIndex = 2
        '
        'BtnAdd
        '
        Me.BtnAdd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BtnAdd.Location = New System.Drawing.Point(2, 2)
        Me.BtnAdd.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.BtnAdd, False)
        Me.IndigoSimpleButton11.SetModernUiIndigo(Me.BtnAdd, False)
        Me.BtnAdd.Name = "BtnAdd"
        Me.BtnAdd.Size = New System.Drawing.Size(423, 33)
        Me.BtnAdd.TabIndex = 7
        Me.BtnAdd.Text = "Aceptar"
        '
        'INDGcBankReconciliation
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcBankReconciliation, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcBankReconciliation, Nothing)
        Me.INDGcBankReconciliation.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcBankReconciliation, True)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcBankReconciliation, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcBankReconciliation, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcBankReconciliation, False)
        Me.INDGcBankReconciliation.Location = New System.Drawing.Point(1119, 81)
        Me.INDGcBankReconciliation.MainView = Me.INDGvConciliation
        Me.INDGcBankReconciliation.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDGcBankReconciliation.MinimumSize = New System.Drawing.Size(584, 1)
        Me.INDGcBankReconciliation.Name = "INDGcBankReconciliation"
        Me.INDGcBankReconciliation.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDRiceReconciled, Me.RepositoryItemSearchLookUpEdit1, Me.RepositoryItemSearchLookUpEdit2})
        Me.INDGcBankReconciliation.ShowOnlyPredefinedDetails = True
        Me.INDGcBankReconciliation.Size = New System.Drawing.Size(677, 454)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcBankReconciliation, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDGcBankReconciliation.TabIndex = 54
        Me.INDGcBankReconciliation.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvConciliation})
        '
        'INDGvConciliation
        '
        Me.INDGvConciliation.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvConciliation.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvConciliation.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvConciliation.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvConciliation.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvConciliation.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvConciliation.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvConciliation.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvConciliation.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvConciliation.Appearance.Row.Options.UseFont = True
        Me.INDGvConciliation.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvConciliation.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvConciliation.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGColConsecutiveStatement, Me.INDGColDateStatement, Me.INDGColValueStatement, Me.INDGColComment, Me.INDGColReconciled, Me.INDColDocumentCode, Me.INDGColNature, Me.INDGColDocumentType})
        Me.INDGvConciliation.GridControl = Me.INDGcBankReconciliation
        Me.INDGvConciliation.GroupCount = 1
        Me.INDGvConciliation.Name = "INDGvConciliation"
        Me.INDGvConciliation.OptionsSelection.MultiSelect = True
        Me.INDGvConciliation.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvConciliation.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvConciliation.OptionsView.ShowAutoFilterRow = True
        Me.INDGvConciliation.OptionsView.ShowFooter = True
        Me.INDGvConciliation.OptionsView.ShowGroupPanel = False
        Me.INDGvConciliation.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.INDColDocumentCode, DevExpress.Data.ColumnSortOrder.Ascending), New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.INDGColDateStatement, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridViewBankStatement.SetTemaIndigoMetro(Me.INDGvConciliation, False)
        Me.IndigoGridViewPendingItems.SetTemaIndigoMetro(Me.INDGvConciliation, False)
        Me.IndigoGridViewBankBook.SetTemaIndigoMetro(Me.INDGvConciliation, False)
        '
        'INDGColConsecutiveStatement
        '
        Me.INDGColConsecutiveStatement.Caption = "Cons. Extracto"
        Me.INDGColConsecutiveStatement.FieldName = "ConsecutiveBank"
        Me.INDGColConsecutiveStatement.MinWidth = 21
        Me.INDGColConsecutiveStatement.Name = "INDGColConsecutiveStatement"
        Me.INDGColConsecutiveStatement.OptionsColumn.AllowEdit = False
        Me.INDGColConsecutiveStatement.Visible = True
        Me.INDGColConsecutiveStatement.VisibleIndex = 0
        '
        'INDGColDateStatement
        '
        Me.INDGColDateStatement.Caption = "Fecha extracto"
        Me.INDGColDateStatement.FieldName = "DocumentDate"
        Me.INDGColDateStatement.MinWidth = 21
        Me.INDGColDateStatement.Name = "INDGColDateStatement"
        Me.INDGColDateStatement.OptionsColumn.AllowEdit = False
        Me.INDGColDateStatement.SortMode = DevExpress.XtraGrid.ColumnSortMode.Custom
        Me.INDGColDateStatement.Visible = True
        Me.INDGColDateStatement.VisibleIndex = 1
        '
        'INDGColValueStatement
        '
        Me.INDGColValueStatement.Caption = "Valor extracto"
        Me.INDGColValueStatement.DisplayFormat.FormatString = "c2"
        Me.INDGColValueStatement.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDGColValueStatement.FieldName = "Value"
        Me.INDGColValueStatement.MinWidth = 21
        Me.INDGColValueStatement.Name = "INDGColValueStatement"
        Me.INDGColValueStatement.OptionsColumn.AllowEdit = False
        Me.INDGColValueStatement.Visible = True
        Me.INDGColValueStatement.VisibleIndex = 2
        '
        'INDGColComment
        '
        Me.INDGColComment.Caption = "Comentario"
        Me.INDGColComment.MinWidth = 21
        Me.INDGColComment.Name = "INDGColComment"
        '
        'INDGColReconciled
        '
        Me.INDGColReconciled.Caption = "Conciliado"
        Me.INDGColReconciled.ColumnEdit = Me.INDRiceReconciled
        Me.INDGColReconciled.FieldName = "Reconciled"
        Me.INDGColReconciled.MinWidth = 21
        Me.INDGColReconciled.Name = "INDGColReconciled"
        Me.INDGColReconciled.Visible = True
        Me.INDGColReconciled.VisibleIndex = 3
        '
        'INDRiceReconciled
        '
        Me.INDRiceReconciled.Appearance.Options.UseImage = True
        Me.INDRiceReconciled.AutoHeight = False
        Me.INDRiceReconciled.CheckBoxOptions.Style = DevExpress.XtraEditors.Controls.CheckBoxStyle.Custom
        Me.INDRiceReconciled.CheckBoxOptions.SvgImageSize = New System.Drawing.Size(10, 10)
        Me.INDRiceReconciled.ImageOptions.ImageChecked = CType(resources.GetObject("INDRiceReconciled.ImageOptions.ImageChecked"), System.Drawing.Image)
        Me.INDRiceReconciled.ImageOptions.ImageUnchecked = CType(resources.GetObject("INDRiceReconciled.ImageOptions.ImageUnchecked"), System.Drawing.Image)
        Me.INDRiceReconciled.Name = "INDRiceReconciled"
        '
        'INDColDocumentCode
        '
        Me.INDColDocumentCode.Caption = "Código documento conciliado"
        Me.INDColDocumentCode.FieldName = "CodeNoteReconciled"
        Me.INDColDocumentCode.MinWidth = 21
        Me.INDColDocumentCode.Name = "INDColDocumentCode"
        Me.INDColDocumentCode.OptionsColumn.AllowEdit = False
        Me.INDColDocumentCode.Visible = True
        Me.INDColDocumentCode.VisibleIndex = 4
        Me.INDColDocumentCode.Width = 81
        '
        'INDGColNature
        '
        Me.INDGColNature.Caption = "Naturaleza"
        Me.INDGColNature.FieldName = "INDGColNature"
        Me.INDGColNature.Name = "INDGColNature"
        Me.INDGColNature.OptionsColumn.AllowEdit = False
        Me.INDGColNature.UnboundExpression = "Iif([Nature] = 1, 'Débito', 'Crédito')"
        Me.INDGColNature.UnboundType = DevExpress.Data.UnboundColumnType.[String]
        '
        'INDGColDocumentType
        '
        Me.INDGColDocumentType.Caption = "Tipo documento"
        Me.INDGColDocumentType.FieldName = "DocumentType"
        Me.INDGColDocumentType.Name = "INDGColDocumentType"
        Me.INDGColDocumentType.OptionsColumn.AllowEdit = False
        '
        'RepositoryItemSearchLookUpEdit1
        '
        Me.RepositoryItemSearchLookUpEdit1.AutoHeight = False
        Me.RepositoryItemSearchLookUpEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemSearchLookUpEdit1.DisplayMember = "Item2"
        Me.RepositoryItemSearchLookUpEdit1.Name = "RepositoryItemSearchLookUpEdit1"
        Me.RepositoryItemSearchLookUpEdit1.NullText = ""
        Me.RepositoryItemSearchLookUpEdit1.PopupView = Me.GridView2
        Me.RepositoryItemSearchLookUpEdit1.ValueMember = "Item1"
        '
        'GridView2
        '
        Me.GridView2.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView2.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView2.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView2.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView2.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView2.Appearance.GroupRow.Options.UseFont = True
        Me.GridView2.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView2.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView2.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView2.Appearance.Row.Options.UseFont = True
        Me.GridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView2.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView2.OptionsView.EnableAppearanceOddRow = True
        Me.GridView2.OptionsView.ShowAutoFilterRow = True
        Me.GridView2.OptionsView.ShowGroupPanel = False
        Me.IndigoGridViewBankStatement.SetTemaIndigoMetro(Me.GridView2, False)
        Me.IndigoGridViewPendingItems.SetTemaIndigoMetro(Me.GridView2, False)
        Me.IndigoGridViewBankBook.SetTemaIndigoMetro(Me.GridView2, False)
        '
        'RepositoryItemSearchLookUpEdit2
        '
        Me.RepositoryItemSearchLookUpEdit2.AutoHeight = False
        Me.RepositoryItemSearchLookUpEdit2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemSearchLookUpEdit2.DisplayMember = "Item2"
        Me.RepositoryItemSearchLookUpEdit2.Name = "RepositoryItemSearchLookUpEdit2"
        Me.RepositoryItemSearchLookUpEdit2.NullText = ""
        Me.RepositoryItemSearchLookUpEdit2.PopupView = Me.GridView3
        Me.RepositoryItemSearchLookUpEdit2.ValueMember = "Item1"
        '
        'GridView3
        '
        Me.GridView3.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView3.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView3.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView3.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView3.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView3.Appearance.GroupRow.Options.UseFont = True
        Me.GridView3.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView3.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView3.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView3.Appearance.Row.Options.UseFont = True
        Me.GridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView3.Name = "GridView3"
        Me.GridView3.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView3.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView3.OptionsView.EnableAppearanceOddRow = True
        Me.GridView3.OptionsView.ShowAutoFilterRow = True
        Me.GridView3.OptionsView.ShowGroupPanel = False
        Me.IndigoGridViewBankStatement.SetTemaIndigoMetro(Me.GridView3, False)
        Me.IndigoGridViewPendingItems.SetTemaIndigoMetro(Me.GridView3, False)
        Me.IndigoGridViewBankBook.SetTemaIndigoMetro(Me.GridView3, False)
        '
        'INDGcBankStatements
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcBankStatements, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcBankStatements, Nothing)
        Me.INDGcBankStatements.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcBankStatements, True)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcBankStatements, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcBankStatements, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcBankStatements, False)
        Me.INDGcBankStatements.Location = New System.Drawing.Point(1824, 53)
        Me.INDGcBankStatements.MainView = Me.INDGvBankStatements
        Me.INDGcBankStatements.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDGcBankStatements.MinimumSize = New System.Drawing.Size(584, 1)
        Me.INDGcBankStatements.Name = "INDGcBankStatements"
        Me.INDGcBankStatements.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrepSleExtractDetailDocumentType, Me.INDrepSleExtractDetailNature, Me.INDrepTxtExtractDetailValue, Me.INDRiceExtractBank, Me.INDrepSleDocumentType})
        Me.INDGcBankStatements.ShowOnlyPredefinedDetails = True
        Me.INDGcBankStatements.Size = New System.Drawing.Size(920, 482)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcBankStatements, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDGcBankStatements.TabIndex = 7
        Me.INDGcBankStatements.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvBankStatements})
        '
        'INDGvBankStatements
        '
        Me.INDGvBankStatements.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvBankStatements.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvBankStatements.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvBankStatements.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvBankStatements.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvBankStatements.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvBankStatements.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvBankStatements.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvBankStatements.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvBankStatements.Appearance.Row.Options.UseFont = True
        Me.INDGvBankStatements.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvBankStatements.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvBankStatements.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDcolBankExtract_Select, Me.INDcolBankReconciliationAutomaticExtractDetail_TransactionDate, Me.INDcolBankReconciliationAutomaticExtractDetail_ConsecutiveBank, Me.INDColBankDocumentType, Me.INDcolBankReconciliationAutomaticExtractDetail_TransactionCode, Me.INDcolBankReconciliationAutomaticExtractDetail_DescriptionTransaction, Me.INDcolBankReconciliationAutomaticExtractDetail_BankCheck, Me.INDcolBankReconciliationAutomaticExtractDetail_PaymentReferenceOne, Me.INDcolBankReconciliationAutomaticExtractDetail_PaymentReferenceTwo, Me.INDcolBankReconciliationAutomaticExtractDetail_Nature, Me.INDcolBankReconciliationAutomaticExtractDetail_NatureName, Me.INDcolBankReconciliationAutomaticExtractDetail_Value, Me.INDColExtractPeriod})
        Me.INDGvBankStatements.GridControl = Me.INDGcBankStatements
        Me.INDGvBankStatements.GroupCount = 1
        Me.INDGvBankStatements.Name = "INDGvBankStatements"
        Me.INDGvBankStatements.OptionsSelection.CheckBoxSelectorColumnWidth = 25
        Me.INDGvBankStatements.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvBankStatements.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvBankStatements.OptionsView.ShowAutoFilterRow = True
        Me.INDGvBankStatements.OptionsView.ShowFooter = True
        Me.INDGvBankStatements.OptionsView.ShowGroupPanel = False
        Me.INDGvBankStatements.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.INDColExtractPeriod, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridViewBankStatement.SetTemaIndigoMetro(Me.INDGvBankStatements, False)
        Me.IndigoGridViewPendingItems.SetTemaIndigoMetro(Me.INDGvBankStatements, False)
        Me.IndigoGridViewBankBook.SetTemaIndigoMetro(Me.INDGvBankStatements, False)
        '
        'INDcolBankExtract_Select
        '
        Me.INDcolBankExtract_Select.Caption = "Sel."
        Me.INDcolBankExtract_Select.ColumnEdit = Me.INDRiceExtractBank
        Me.INDcolBankExtract_Select.FieldName = "Checked"
        Me.INDcolBankExtract_Select.MinWidth = 21
        Me.INDcolBankExtract_Select.Name = "INDcolBankExtract_Select"
        Me.INDcolBankExtract_Select.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDcolBankExtract_Select.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDcolBankExtract_Select.OptionsColumn.AllowMove = False
        Me.INDcolBankExtract_Select.OptionsColumn.AllowShowHide = False
        Me.INDcolBankExtract_Select.OptionsColumn.AllowSize = False
        Me.INDcolBankExtract_Select.OptionsColumn.FixedWidth = True
        Me.INDcolBankExtract_Select.Visible = True
        Me.INDcolBankExtract_Select.VisibleIndex = 0
        Me.INDcolBankExtract_Select.Width = 37
        '
        'INDRiceExtractBank
        '
        Me.INDRiceExtractBank.AutoHeight = False
        Me.INDRiceExtractBank.Name = "INDRiceExtractBank"
        '
        'INDcolBankReconciliationAutomaticExtractDetail_TransactionDate
        '
        Me.INDcolBankReconciliationAutomaticExtractDetail_TransactionDate.Caption = "Fecha Transacción"
        Me.INDcolBankReconciliationAutomaticExtractDetail_TransactionDate.DisplayFormat.FormatString = "d"
        Me.INDcolBankReconciliationAutomaticExtractDetail_TransactionDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDcolBankReconciliationAutomaticExtractDetail_TransactionDate.FieldName = "DocumentDate"
        Me.INDcolBankReconciliationAutomaticExtractDetail_TransactionDate.MinWidth = 21
        Me.INDcolBankReconciliationAutomaticExtractDetail_TransactionDate.Name = "INDcolBankReconciliationAutomaticExtractDetail_TransactionDate"
        Me.INDcolBankReconciliationAutomaticExtractDetail_TransactionDate.OptionsColumn.AllowEdit = False
        Me.INDcolBankReconciliationAutomaticExtractDetail_TransactionDate.OptionsColumn.AllowFocus = False
        Me.INDcolBankReconciliationAutomaticExtractDetail_TransactionDate.Visible = True
        Me.INDcolBankReconciliationAutomaticExtractDetail_TransactionDate.VisibleIndex = 1
        Me.INDcolBankReconciliationAutomaticExtractDetail_TransactionDate.Width = 69
        '
        'INDcolBankReconciliationAutomaticExtractDetail_ConsecutiveBank
        '
        Me.INDcolBankReconciliationAutomaticExtractDetail_ConsecutiveBank.Caption = "Consecutivo Banco"
        Me.INDcolBankReconciliationAutomaticExtractDetail_ConsecutiveBank.FieldName = "ConsecutiveBank"
        Me.INDcolBankReconciliationAutomaticExtractDetail_ConsecutiveBank.MinWidth = 21
        Me.INDcolBankReconciliationAutomaticExtractDetail_ConsecutiveBank.Name = "INDcolBankReconciliationAutomaticExtractDetail_ConsecutiveBank"
        Me.INDcolBankReconciliationAutomaticExtractDetail_ConsecutiveBank.OptionsColumn.AllowEdit = False
        Me.INDcolBankReconciliationAutomaticExtractDetail_ConsecutiveBank.OptionsColumn.AllowFocus = False
        Me.INDcolBankReconciliationAutomaticExtractDetail_ConsecutiveBank.Width = 69
        '
        'INDColBankDocumentType
        '
        Me.INDColBankDocumentType.Caption = "Tipo de documento"
        Me.INDColBankDocumentType.ColumnEdit = Me.INDrepSleDocumentType
        Me.INDColBankDocumentType.FieldName = "DocumentType"
        Me.INDColBankDocumentType.MinWidth = 21
        Me.INDColBankDocumentType.Name = "INDColBankDocumentType"
        Me.INDColBankDocumentType.Visible = True
        Me.INDColBankDocumentType.VisibleIndex = 2
        '
        'INDrepSleDocumentType
        '
        Me.INDrepSleDocumentType.AutoHeight = False
        Me.INDrepSleDocumentType.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrepSleDocumentType.DisplayMember = "Name"
        Me.INDrepSleDocumentType.Name = "INDrepSleDocumentType"
        Me.INDrepSleDocumentType.NullText = ""
        Me.INDrepSleDocumentType.PopupView = Me.RepositoryItemSearchLookUpEdit3View
        Me.INDrepSleDocumentType.ShowNullValuePrompt = CType((DevExpress.XtraEditors.ShowNullValuePromptOptions.EditorFocused Or DevExpress.XtraEditors.ShowNullValuePromptOptions.EditorReadOnly), DevExpress.XtraEditors.ShowNullValuePromptOptions)
        Me.INDrepSleDocumentType.ValueMember = "Id"
        '
        'RepositoryItemSearchLookUpEdit3View
        '
        Me.RepositoryItemSearchLookUpEdit3View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.RepositoryItemSearchLookUpEdit3View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.RepositoryItemSearchLookUpEdit3View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.RepositoryItemSearchLookUpEdit3View.Appearance.FocusedRow.Options.UseFont = True
        Me.RepositoryItemSearchLookUpEdit3View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RepositoryItemSearchLookUpEdit3View.Appearance.GroupRow.Options.UseFont = True
        Me.RepositoryItemSearchLookUpEdit3View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RepositoryItemSearchLookUpEdit3View.Appearance.HeaderPanel.Options.UseFont = True
        Me.RepositoryItemSearchLookUpEdit3View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.RepositoryItemSearchLookUpEdit3View.Appearance.Row.Options.UseFont = True
        Me.RepositoryItemSearchLookUpEdit3View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn7})
        Me.RepositoryItemSearchLookUpEdit3View.DetailHeight = 240
        Me.RepositoryItemSearchLookUpEdit3View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.RepositoryItemSearchLookUpEdit3View.Name = "RepositoryItemSearchLookUpEdit3View"
        Me.RepositoryItemSearchLookUpEdit3View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.RepositoryItemSearchLookUpEdit3View.OptionsView.EnableAppearanceEvenRow = True
        Me.RepositoryItemSearchLookUpEdit3View.OptionsView.EnableAppearanceOddRow = True
        Me.RepositoryItemSearchLookUpEdit3View.OptionsView.ShowAutoFilterRow = True
        Me.RepositoryItemSearchLookUpEdit3View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridViewBankStatement.SetTemaIndigoMetro(Me.RepositoryItemSearchLookUpEdit3View, False)
        Me.IndigoGridViewPendingItems.SetTemaIndigoMetro(Me.RepositoryItemSearchLookUpEdit3View, False)
        Me.IndigoGridViewBankBook.SetTemaIndigoMetro(Me.RepositoryItemSearchLookUpEdit3View, False)
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Tipo Documento"
        Me.GridColumn7.FieldName = "Name"
        Me.GridColumn7.MinWidth = 15
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 0
        Me.GridColumn7.Width = 57
        '
        'INDcolBankReconciliationAutomaticExtractDetail_TransactionCode
        '
        Me.INDcolBankReconciliationAutomaticExtractDetail_TransactionCode.Caption = "Código Transacción"
        Me.INDcolBankReconciliationAutomaticExtractDetail_TransactionCode.FieldName = "TransactionCode"
        Me.INDcolBankReconciliationAutomaticExtractDetail_TransactionCode.MinWidth = 21
        Me.INDcolBankReconciliationAutomaticExtractDetail_TransactionCode.Name = "INDcolBankReconciliationAutomaticExtractDetail_TransactionCode"
        Me.INDcolBankReconciliationAutomaticExtractDetail_TransactionCode.OptionsColumn.AllowEdit = False
        Me.INDcolBankReconciliationAutomaticExtractDetail_TransactionCode.OptionsColumn.AllowFocus = False
        Me.INDcolBankReconciliationAutomaticExtractDetail_TransactionCode.Visible = True
        Me.INDcolBankReconciliationAutomaticExtractDetail_TransactionCode.VisibleIndex = 3
        Me.INDcolBankReconciliationAutomaticExtractDetail_TransactionCode.Width = 69
        '
        'INDcolBankReconciliationAutomaticExtractDetail_DescriptionTransaction
        '
        Me.INDcolBankReconciliationAutomaticExtractDetail_DescriptionTransaction.Caption = "Descripción Transacción"
        Me.INDcolBankReconciliationAutomaticExtractDetail_DescriptionTransaction.FieldName = "DescriptionTransaction"
        Me.INDcolBankReconciliationAutomaticExtractDetail_DescriptionTransaction.MinWidth = 21
        Me.INDcolBankReconciliationAutomaticExtractDetail_DescriptionTransaction.Name = "INDcolBankReconciliationAutomaticExtractDetail_DescriptionTransaction"
        Me.INDcolBankReconciliationAutomaticExtractDetail_DescriptionTransaction.OptionsColumn.AllowEdit = False
        Me.INDcolBankReconciliationAutomaticExtractDetail_DescriptionTransaction.OptionsColumn.AllowFocus = False
        Me.INDcolBankReconciliationAutomaticExtractDetail_DescriptionTransaction.Width = 69
        '
        'INDcolBankReconciliationAutomaticExtractDetail_BankCheck
        '
        Me.INDcolBankReconciliationAutomaticExtractDetail_BankCheck.Caption = "Cheque"
        Me.INDcolBankReconciliationAutomaticExtractDetail_BankCheck.FieldName = "BankCheck"
        Me.INDcolBankReconciliationAutomaticExtractDetail_BankCheck.MinWidth = 21
        Me.INDcolBankReconciliationAutomaticExtractDetail_BankCheck.Name = "INDcolBankReconciliationAutomaticExtractDetail_BankCheck"
        Me.INDcolBankReconciliationAutomaticExtractDetail_BankCheck.OptionsColumn.AllowEdit = False
        Me.INDcolBankReconciliationAutomaticExtractDetail_BankCheck.OptionsColumn.AllowFocus = False
        Me.INDcolBankReconciliationAutomaticExtractDetail_BankCheck.Visible = True
        Me.INDcolBankReconciliationAutomaticExtractDetail_BankCheck.VisibleIndex = 4
        Me.INDcolBankReconciliationAutomaticExtractDetail_BankCheck.Width = 69
        '
        'INDcolBankReconciliationAutomaticExtractDetail_PaymentReferenceOne
        '
        Me.INDcolBankReconciliationAutomaticExtractDetail_PaymentReferenceOne.Caption = "Ref pago 1"
        Me.INDcolBankReconciliationAutomaticExtractDetail_PaymentReferenceOne.FieldName = "PaymentReferenceOne"
        Me.INDcolBankReconciliationAutomaticExtractDetail_PaymentReferenceOne.MinWidth = 21
        Me.INDcolBankReconciliationAutomaticExtractDetail_PaymentReferenceOne.Name = "INDcolBankReconciliationAutomaticExtractDetail_PaymentReferenceOne"
        Me.INDcolBankReconciliationAutomaticExtractDetail_PaymentReferenceOne.OptionsColumn.AllowEdit = False
        Me.INDcolBankReconciliationAutomaticExtractDetail_PaymentReferenceOne.OptionsColumn.AllowFocus = False
        Me.INDcolBankReconciliationAutomaticExtractDetail_PaymentReferenceOne.Visible = True
        Me.INDcolBankReconciliationAutomaticExtractDetail_PaymentReferenceOne.VisibleIndex = 5
        Me.INDcolBankReconciliationAutomaticExtractDetail_PaymentReferenceOne.Width = 69
        '
        'INDcolBankReconciliationAutomaticExtractDetail_PaymentReferenceTwo
        '
        Me.INDcolBankReconciliationAutomaticExtractDetail_PaymentReferenceTwo.Caption = "Ref Pago 2"
        Me.INDcolBankReconciliationAutomaticExtractDetail_PaymentReferenceTwo.FieldName = "PaymentReferenceTwo"
        Me.INDcolBankReconciliationAutomaticExtractDetail_PaymentReferenceTwo.MinWidth = 21
        Me.INDcolBankReconciliationAutomaticExtractDetail_PaymentReferenceTwo.Name = "INDcolBankReconciliationAutomaticExtractDetail_PaymentReferenceTwo"
        Me.INDcolBankReconciliationAutomaticExtractDetail_PaymentReferenceTwo.OptionsColumn.AllowEdit = False
        Me.INDcolBankReconciliationAutomaticExtractDetail_PaymentReferenceTwo.OptionsColumn.AllowFocus = False
        Me.INDcolBankReconciliationAutomaticExtractDetail_PaymentReferenceTwo.Visible = True
        Me.INDcolBankReconciliationAutomaticExtractDetail_PaymentReferenceTwo.VisibleIndex = 6
        Me.INDcolBankReconciliationAutomaticExtractDetail_PaymentReferenceTwo.Width = 69
        '
        'INDcolBankReconciliationAutomaticExtractDetail_Nature
        '
        Me.INDcolBankReconciliationAutomaticExtractDetail_Nature.Caption = "Nature"
        Me.INDcolBankReconciliationAutomaticExtractDetail_Nature.FieldName = "Nature"
        Me.INDcolBankReconciliationAutomaticExtractDetail_Nature.MinWidth = 21
        Me.INDcolBankReconciliationAutomaticExtractDetail_Nature.Name = "INDcolBankReconciliationAutomaticExtractDetail_Nature"
        Me.INDcolBankReconciliationAutomaticExtractDetail_Nature.OptionsColumn.AllowEdit = False
        Me.INDcolBankReconciliationAutomaticExtractDetail_Nature.OptionsColumn.AllowFocus = False
        '
        'INDcolBankReconciliationAutomaticExtractDetail_NatureName
        '
        Me.INDcolBankReconciliationAutomaticExtractDetail_NatureName.Caption = "Naturaleza"
        Me.INDcolBankReconciliationAutomaticExtractDetail_NatureName.FieldName = "NatureName"
        Me.INDcolBankReconciliationAutomaticExtractDetail_NatureName.MinWidth = 21
        Me.INDcolBankReconciliationAutomaticExtractDetail_NatureName.Name = "INDcolBankReconciliationAutomaticExtractDetail_NatureName"
        Me.INDcolBankReconciliationAutomaticExtractDetail_NatureName.OptionsColumn.AllowEdit = False
        Me.INDcolBankReconciliationAutomaticExtractDetail_NatureName.OptionsColumn.AllowFocus = False
        Me.INDcolBankReconciliationAutomaticExtractDetail_NatureName.UnboundExpression = "Iif([Nature] = 1, 'Débito', 'Crédito')"
        Me.INDcolBankReconciliationAutomaticExtractDetail_NatureName.UnboundType = DevExpress.Data.UnboundColumnType.[String]
        Me.INDcolBankReconciliationAutomaticExtractDetail_NatureName.Visible = True
        Me.INDcolBankReconciliationAutomaticExtractDetail_NatureName.VisibleIndex = 7
        Me.INDcolBankReconciliationAutomaticExtractDetail_NatureName.Width = 93
        '
        'INDcolBankReconciliationAutomaticExtractDetail_Value
        '
        Me.INDcolBankReconciliationAutomaticExtractDetail_Value.Caption = "Valor"
        Me.INDcolBankReconciliationAutomaticExtractDetail_Value.DisplayFormat.FormatString = "C0"
        Me.INDcolBankReconciliationAutomaticExtractDetail_Value.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.INDcolBankReconciliationAutomaticExtractDetail_Value.FieldName = "Value"
        Me.INDcolBankReconciliationAutomaticExtractDetail_Value.MinWidth = 21
        Me.INDcolBankReconciliationAutomaticExtractDetail_Value.Name = "INDcolBankReconciliationAutomaticExtractDetail_Value"
        Me.INDcolBankReconciliationAutomaticExtractDetail_Value.OptionsColumn.AllowEdit = False
        Me.INDcolBankReconciliationAutomaticExtractDetail_Value.OptionsColumn.AllowFocus = False
        Me.INDcolBankReconciliationAutomaticExtractDetail_Value.Visible = True
        Me.INDcolBankReconciliationAutomaticExtractDetail_Value.VisibleIndex = 8
        '
        'INDColExtractPeriod
        '
        Me.INDColExtractPeriod.Caption = "Periodo"
        Me.INDColExtractPeriod.FieldName = "PeriodName"
        Me.INDColExtractPeriod.Name = "INDColExtractPeriod"
        Me.INDColExtractPeriod.UnboundExpression = "Iif(IsNullOrEmpty([Period]), 'Registro del periodo actual', [Period])"
        Me.INDColExtractPeriod.UnboundType = DevExpress.Data.UnboundColumnType.[String]
        Me.INDColExtractPeriod.Visible = True
        Me.INDColExtractPeriod.VisibleIndex = 9
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
        Me.INDrepGvExtractDetailDocumentType.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDrepGvExtractDetailDocumentType.Name = "INDrepGvExtractDetailDocumentType"
        Me.INDrepGvExtractDetailDocumentType.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDrepGvExtractDetailDocumentType.OptionsView.EnableAppearanceEvenRow = True
        Me.INDrepGvExtractDetailDocumentType.OptionsView.EnableAppearanceOddRow = True
        Me.INDrepGvExtractDetailDocumentType.OptionsView.ShowAutoFilterRow = True
        Me.INDrepGvExtractDetailDocumentType.OptionsView.ShowGroupPanel = False
        Me.IndigoGridViewBankStatement.SetTemaIndigoMetro(Me.INDrepGvExtractDetailDocumentType, False)
        Me.IndigoGridViewPendingItems.SetTemaIndigoMetro(Me.INDrepGvExtractDetailDocumentType, False)
        Me.IndigoGridViewBankBook.SetTemaIndigoMetro(Me.INDrepGvExtractDetailDocumentType, False)
        '
        'INDrepColExtractDetailDocumentType
        '
        Me.INDrepColExtractDetailDocumentType.Caption = "Descripción"
        Me.INDrepColExtractDetailDocumentType.FieldName = "Item2"
        Me.INDrepColExtractDetailDocumentType.MinWidth = 21
        Me.INDrepColExtractDetailDocumentType.Name = "INDrepColExtractDetailDocumentType"
        Me.INDrepColExtractDetailDocumentType.Visible = True
        Me.INDrepColExtractDetailDocumentType.VisibleIndex = 0
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
        Me.INDrepGvExtractDetailNature.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDrepGvExtractDetailNature.Name = "INDrepGvExtractDetailNature"
        Me.INDrepGvExtractDetailNature.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDrepGvExtractDetailNature.OptionsView.EnableAppearanceEvenRow = True
        Me.INDrepGvExtractDetailNature.OptionsView.EnableAppearanceOddRow = True
        Me.INDrepGvExtractDetailNature.OptionsView.ShowAutoFilterRow = True
        Me.INDrepGvExtractDetailNature.OptionsView.ShowGroupPanel = False
        Me.IndigoGridViewBankStatement.SetTemaIndigoMetro(Me.INDrepGvExtractDetailNature, False)
        Me.IndigoGridViewPendingItems.SetTemaIndigoMetro(Me.INDrepGvExtractDetailNature, False)
        Me.IndigoGridViewBankBook.SetTemaIndigoMetro(Me.INDrepGvExtractDetailNature, False)
        '
        'INDrepColExtractDetailNature
        '
        Me.INDrepColExtractDetailNature.Caption = "Descripción"
        Me.INDrepColExtractDetailNature.FieldName = "Item2"
        Me.INDrepColExtractDetailNature.MinWidth = 21
        Me.INDrepColExtractDetailNature.Name = "INDrepColExtractDetailNature"
        Me.INDrepColExtractDetailNature.Visible = True
        Me.INDrepColExtractDetailNature.VisibleIndex = 0
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
        'INDseDifferenceReconcile
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDseDifferenceReconcile, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDseDifferenceReconcile, False)
        Me.INDseDifferenceReconcile.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDseDifferenceReconcile.EnterMoveNextControl = True
        Me.INDseDifferenceReconcile.Location = New System.Drawing.Point(24, 439)
        Me.INDseDifferenceReconcile.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDseDifferenceReconcile, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDseDifferenceReconcile.Name = "INDseDifferenceReconcile"
        Me.INDseDifferenceReconcile.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDseDifferenceReconcile.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseDifferenceReconcile.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDseDifferenceReconcile.Properties.Appearance.Options.UseBackColor = True
        Me.INDseDifferenceReconcile.Properties.Appearance.Options.UseFont = True
        Me.INDseDifferenceReconcile.Properties.Appearance.Options.UseForeColor = True
        Me.INDseDifferenceReconcile.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDseDifferenceReconcile.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseDifferenceReconcile.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDseDifferenceReconcile.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDseDifferenceReconcile.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDseDifferenceReconcile.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDseDifferenceReconcile.Properties.Mask.EditMask = "C2"
        Me.INDseDifferenceReconcile.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDseDifferenceReconcile.Properties.MaxLength = 18
        Me.INDseDifferenceReconcile.Properties.ReadOnly = True
        Me.INDseDifferenceReconcile.Size = New System.Drawing.Size(386, 28)
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
        Me.INDseEndEntityBankAccountValue.Location = New System.Drawing.Point(24, 259)
        Me.INDseEndEntityBankAccountValue.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDseEndEntityBankAccountValue, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDseEndEntityBankAccountValue.Name = "INDseEndEntityBankAccountValue"
        Me.INDseEndEntityBankAccountValue.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDseEndEntityBankAccountValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseEndEntityBankAccountValue.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDseEndEntityBankAccountValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDseEndEntityBankAccountValue.Properties.Appearance.Options.UseFont = True
        Me.INDseEndEntityBankAccountValue.Properties.Appearance.Options.UseForeColor = True
        Me.INDseEndEntityBankAccountValue.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDseEndEntityBankAccountValue.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseEndEntityBankAccountValue.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDseEndEntityBankAccountValue.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDseEndEntityBankAccountValue.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDseEndEntityBankAccountValue.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDseEndEntityBankAccountValue.Properties.Mask.EditMask = "C2"
        Me.INDseEndEntityBankAccountValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDseEndEntityBankAccountValue.Properties.MaxLength = 18
        Me.INDseEndEntityBankAccountValue.Properties.ReadOnly = True
        Me.INDseEndEntityBankAccountValue.Size = New System.Drawing.Size(386, 28)
        Me.INDseEndEntityBankAccountValue.StyleController = Me.INDlyRoot
        Me.INDseEndEntityBankAccountValue.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDseEndEntityBankAccountValue, 0)
        '
        'INDseExtractValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDseExtractValue, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDseExtractValue, False)
        Me.INDseExtractValue.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDseExtractValue.EnterMoveNextControl = True
        Me.INDseExtractValue.Location = New System.Drawing.Point(24, 379)
        Me.INDseExtractValue.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDseExtractValue, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDseExtractValue.Name = "INDseExtractValue"
        Me.INDseExtractValue.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDseExtractValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseExtractValue.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDseExtractValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDseExtractValue.Properties.Appearance.Options.UseFont = True
        Me.INDseExtractValue.Properties.Appearance.Options.UseForeColor = True
        Me.INDseExtractValue.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDseExtractValue.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseExtractValue.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDseExtractValue.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDseExtractValue.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDseExtractValue.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDseExtractValue.Properties.Mask.EditMask = "C2"
        Me.INDseExtractValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDseExtractValue.Properties.MaxLength = 18
        Me.INDseExtractValue.Properties.ReadOnly = True
        Me.INDseExtractValue.Size = New System.Drawing.Size(386, 28)
        Me.INDseExtractValue.StyleController = Me.INDlyRoot
        Me.INDseExtractValue.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDseExtractValue, 0)
        '
        'INDdteDocumentDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdteDocumentDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdteDocumentDate, False)
        Me.INDdteDocumentDate.EditValue = Nothing
        Me.INDdteDocumentDate.EnterMoveNextControl = True
        Me.INDdteDocumentDate.Location = New System.Drawing.Point(24, 199)
        Me.INDdteDocumentDate.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDdteDocumentDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDdteDocumentDate.Name = "INDdteDocumentDate"
        Me.INDdteDocumentDate.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDdteDocumentDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteDocumentDate.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDdteDocumentDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDdteDocumentDate.Properties.Appearance.Options.UseFont = True
        Me.INDdteDocumentDate.Properties.Appearance.Options.UseForeColor = True
        Me.INDdteDocumentDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDdteDocumentDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteDocumentDate.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDdteDocumentDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDdteDocumentDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdteDocumentDate.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDdteDocumentDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteDocumentDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteDocumentDate.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDdteDocumentDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDdteDocumentDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdteDocumentDate.Size = New System.Drawing.Size(386, 28)
        Me.INDdteDocumentDate.StyleController = Me.INDlyRoot
        Me.INDdteDocumentDate.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdteDocumentDate, 0)
        '
        'INDsleEntityBankAccount
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleEntityBankAccount, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleEntityBankAccount, AppearanceObject4)
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
        Me.INDsleEntityBankAccount.Location = New System.Drawing.Point(24, 139)
        Me.INDsleEntityBankAccount.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
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
        Me.INDsleEntityBankAccount.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleEntityBankAccount.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleEntityBankAccount.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleEntityBankAccount.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleEntityBankAccount.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleEntityBankAccount.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDsleEntityBankAccount.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleEntityBankAccount.Properties.DisplayMember = "CodeBankAccount"
        Me.INDsleEntityBankAccount.Properties.NullText = ""
        Me.INDsleEntityBankAccount.Properties.PopupFormMinSize = New System.Drawing.Size(801, 0)
        Me.INDsleEntityBankAccount.Properties.PopupSizeable = False
        Me.INDsleEntityBankAccount.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDsleEntityBankAccount.Properties.ShowFooter = False
        Me.INDsleEntityBankAccount.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleEntityBankAccount, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleEntityBankAccount, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleEntityBankAccount, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleEntityBankAccount, True)
        Me.INDsleEntityBankAccount.Size = New System.Drawing.Size(386, 28)
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
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridViewBankStatement.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        Me.IndigoGridViewPendingItems.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        Me.IndigoGridViewBankBook.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Código"
        Me.GridColumn1.FieldName = "Code"
        Me.GridColumn1.MinWidth = 21
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 153
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Cuenta Contable"
        Me.GridColumn2.FieldName = "IdMainAccount.NumberName"
        Me.GridColumn2.MinWidth = 21
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 405
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Fecha Apertura"
        Me.GridColumn3.FieldName = "InitialDate"
        Me.GridColumn3.MinWidth = 21
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        Me.GridColumn3.Width = 147
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Tipo"
        Me.GridColumn4.FieldName = "TypeName"
        Me.GridColumn4.MinWidth = 21
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 3
        Me.GridColumn4.Width = 171
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Banco"
        Me.GridColumn5.FieldName = "IdBank.Name"
        Me.GridColumn5.MinWidth = 21
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 4
        Me.GridColumn5.Width = 267
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Fuente Finan."
        Me.GridColumn6.FieldName = "FinancialSourceId.CodeName"
        Me.GridColumn6.MinWidth = 21
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 5
        Me.GridColumn6.Width = 249
        '
        'INDbtnCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbtnCode, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbtnCode, True)
        Me.INDbtnCode.Location = New System.Drawing.Point(24, 79)
        Me.INDbtnCode.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDbtnCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDbtnCode.Name = "INDbtnCode"
        Me.INDbtnCode.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDbtnCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnCode.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDbtnCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDbtnCode.Properties.Appearance.Options.UseFont = True
        Me.INDbtnCode.Properties.Appearance.Options.UseForeColor = True
        Me.INDbtnCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDbtnCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnCode.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseForeColor = True
        EditorButtonImageOptions1.Image = Global.Presentation.Treasury.My.Resources.Resources.BuscarMetro
        Me.INDbtnCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDbtnCode.Properties.MaxLength = 20
        Me.INDbtnCode.Size = New System.Drawing.Size(386, 28)
        Me.INDbtnCode.StyleController = Me.INDlyRoot
        Me.INDbtnCode.TabIndex = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbtnCode, 0)
        Me.INDbtnCode.ToolTip = "Este Campo es Necesario"
        '
        'INDseExtractValueMovement
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDseExtractValueMovement, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDseExtractValueMovement, False)
        Me.INDseExtractValueMovement.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDseExtractValueMovement.EnterMoveNextControl = True
        Me.INDseExtractValueMovement.Location = New System.Drawing.Point(24, 319)
        Me.INDseExtractValueMovement.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDseExtractValueMovement, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDseExtractValueMovement.Name = "INDseExtractValueMovement"
        Me.INDseExtractValueMovement.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDseExtractValueMovement.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseExtractValueMovement.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDseExtractValueMovement.Properties.Appearance.Options.UseBackColor = True
        Me.INDseExtractValueMovement.Properties.Appearance.Options.UseFont = True
        Me.INDseExtractValueMovement.Properties.Appearance.Options.UseForeColor = True
        Me.INDseExtractValueMovement.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDseExtractValueMovement.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseExtractValueMovement.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDseExtractValueMovement.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDseExtractValueMovement.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDseExtractValueMovement.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDseExtractValueMovement.Properties.Mask.EditMask = "C2"
        Me.INDseExtractValueMovement.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDseExtractValueMovement.Properties.MaxLength = 18
        Me.INDseExtractValueMovement.Properties.ReadOnly = True
        Me.INDseExtractValueMovement.Size = New System.Drawing.Size(386, 28)
        Me.INDseExtractValueMovement.StyleController = Me.INDlyRoot
        Me.INDseExtractValueMovement.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDseExtractValueMovement, 0)
        '
        'INDGcPendingItemsToReconciled
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcPendingItemsToReconciled, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcPendingItemsToReconciled, Nothing)
        Me.INDGcPendingItemsToReconciled.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcPendingItemsToReconciled, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcPendingItemsToReconciled, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcPendingItemsToReconciled, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcPendingItemsToReconciled, False)
        Me.INDGcPendingItemsToReconciled.Location = New System.Drawing.Point(2772, 53)
        Me.INDGcPendingItemsToReconciled.MainView = Me.INDGvPendingItemsToReconciled
        Me.INDGcPendingItemsToReconciled.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDGcPendingItemsToReconciled.MinimumSize = New System.Drawing.Size(584, 1)
        Me.INDGcPendingItemsToReconciled.Name = "INDGcPendingItemsToReconciled"
        Me.INDGcPendingItemsToReconciled.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDRiceExtractBank1, Me.INDRipcePendingActions})
        Me.INDGcPendingItemsToReconciled.ShowOnlyPredefinedDetails = True
        Me.INDGcPendingItemsToReconciled.Size = New System.Drawing.Size(920, 482)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcPendingItemsToReconciled, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDGcPendingItemsToReconciled.TabIndex = 7
        Me.INDGcPendingItemsToReconciled.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvPendingItemsToReconciled})
        '
        'INDGvPendingItemsToReconciled
        '
        Me.INDGvPendingItemsToReconciled.Appearance.Empty.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvPendingItemsToReconciled.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvPendingItemsToReconciled.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvPendingItemsToReconciled.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvPendingItemsToReconciled.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvPendingItemsToReconciled.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvPendingItemsToReconciled.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvPendingItemsToReconciled.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvPendingItemsToReconciled.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvPendingItemsToReconciled.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvPendingItemsToReconciled.Appearance.Row.Options.UseFont = True
        Me.INDGvPendingItemsToReconciled.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvPendingItemsToReconciled.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvPendingItemsToReconciled.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDcolPendingItemsToReconciled_Select, Me.INDcolPendingItemsToReconciled_Origin, Me.INDcolPendingItemsToReconciled_Code, Me.INDcolPendingItemsToReconciled_Date, Me.INDcolPendingItemsToReconciled_DescriptionTransaction, Me.INDColPendingItemsToReconciled_DocumentType, Me.INDColPendingItemsToReconciled_Detail, Me.INDcolPendingItemsToReconciled_NatureName, Me.INDcolPendingItemsToReconciled_Value, Me.INDColPendingItems_ConsecutiveBank, Me.INDColPendingItems_TransactionalCode, Me.INDColPendingItems_BankCheck, Me.INDColPendingItems_PaymentReferenceOne, Me.INDColPendingItems_PaymentReferenceTwo, Me.INDColPendingitems_Comment, Me.INDColPendingActions, Me.INDColStatusReconciled})
        Me.INDGvPendingItemsToReconciled.GridControl = Me.INDGcPendingItemsToReconciled
        Me.INDGvPendingItemsToReconciled.GroupCount = 1
        Me.INDGvPendingItemsToReconciled.Name = "INDGvPendingItemsToReconciled"
        Me.INDGvPendingItemsToReconciled.OptionsSelection.CheckBoxSelectorColumnWidth = 25
        Me.INDGvPendingItemsToReconciled.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvPendingItemsToReconciled.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvPendingItemsToReconciled.OptionsView.ShowAutoFilterRow = True
        Me.INDGvPendingItemsToReconciled.OptionsView.ShowFooter = True
        Me.INDGvPendingItemsToReconciled.OptionsView.ShowGroupPanel = False
        Me.INDGvPendingItemsToReconciled.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.INDColStatusReconciled, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridViewBankStatement.SetTemaIndigoMetro(Me.INDGvPendingItemsToReconciled, False)
        Me.IndigoGridViewPendingItems.SetTemaIndigoMetro(Me.INDGvPendingItemsToReconciled, False)
        Me.IndigoGridViewBankBook.SetTemaIndigoMetro(Me.INDGvPendingItemsToReconciled, False)
        '
        'INDcolPendingItemsToReconciled_Select
        '
        Me.INDcolPendingItemsToReconciled_Select.Caption = "Sel."
        Me.INDcolPendingItemsToReconciled_Select.ColumnEdit = Me.INDRiceExtractBank1
        Me.INDcolPendingItemsToReconciled_Select.FieldName = "Checked"
        Me.INDcolPendingItemsToReconciled_Select.MinWidth = 21
        Me.INDcolPendingItemsToReconciled_Select.Name = "INDcolPendingItemsToReconciled_Select"
        Me.INDcolPendingItemsToReconciled_Select.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDcolPendingItemsToReconciled_Select.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDcolPendingItemsToReconciled_Select.OptionsColumn.AllowMove = False
        Me.INDcolPendingItemsToReconciled_Select.OptionsColumn.AllowShowHide = False
        Me.INDcolPendingItemsToReconciled_Select.OptionsColumn.AllowSize = False
        Me.INDcolPendingItemsToReconciled_Select.OptionsColumn.FixedWidth = True
        Me.INDcolPendingItemsToReconciled_Select.Visible = True
        Me.INDcolPendingItemsToReconciled_Select.VisibleIndex = 0
        Me.INDcolPendingItemsToReconciled_Select.Width = 37
        '
        'INDRiceExtractBank1
        '
        Me.INDRiceExtractBank1.AutoHeight = False
        Me.INDRiceExtractBank1.Name = "INDRiceExtractBank1"
        '
        'INDcolPendingItemsToReconciled_Origin
        '
        Me.INDcolPendingItemsToReconciled_Origin.Caption = "Origen"
        Me.INDcolPendingItemsToReconciled_Origin.FieldName = "OriginName"
        Me.INDcolPendingItemsToReconciled_Origin.MinWidth = 21
        Me.INDcolPendingItemsToReconciled_Origin.Name = "INDcolPendingItemsToReconciled_Origin"
        Me.INDcolPendingItemsToReconciled_Origin.OptionsColumn.AllowEdit = False
        Me.INDcolPendingItemsToReconciled_Origin.OptionsColumn.AllowFocus = False
        Me.INDcolPendingItemsToReconciled_Origin.Visible = True
        Me.INDcolPendingItemsToReconciled_Origin.VisibleIndex = 1
        Me.INDcolPendingItemsToReconciled_Origin.Width = 69
        '
        'INDcolPendingItemsToReconciled_Code
        '
        Me.INDcolPendingItemsToReconciled_Code.Caption = "Código"
        Me.INDcolPendingItemsToReconciled_Code.FieldName = "DocumentDetail.EntityCode"
        Me.INDcolPendingItemsToReconciled_Code.MinWidth = 21
        Me.INDcolPendingItemsToReconciled_Code.Name = "INDcolPendingItemsToReconciled_Code"
        Me.INDcolPendingItemsToReconciled_Code.OptionsColumn.AllowEdit = False
        Me.INDcolPendingItemsToReconciled_Code.OptionsColumn.AllowFocus = False
        Me.INDcolPendingItemsToReconciled_Code.Visible = True
        Me.INDcolPendingItemsToReconciled_Code.VisibleIndex = 2
        Me.INDcolPendingItemsToReconciled_Code.Width = 69
        '
        'INDcolPendingItemsToReconciled_Date
        '
        Me.INDcolPendingItemsToReconciled_Date.Caption = "Fecha"
        Me.INDcolPendingItemsToReconciled_Date.DisplayFormat.FormatString = "d"
        Me.INDcolPendingItemsToReconciled_Date.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDcolPendingItemsToReconciled_Date.FieldName = "DocumentDate"
        Me.INDcolPendingItemsToReconciled_Date.MinWidth = 21
        Me.INDcolPendingItemsToReconciled_Date.Name = "INDcolPendingItemsToReconciled_Date"
        Me.INDcolPendingItemsToReconciled_Date.OptionsColumn.AllowEdit = False
        Me.INDcolPendingItemsToReconciled_Date.OptionsColumn.AllowFocus = False
        Me.INDcolPendingItemsToReconciled_Date.Visible = True
        Me.INDcolPendingItemsToReconciled_Date.VisibleIndex = 3
        Me.INDcolPendingItemsToReconciled_Date.Width = 69
        '
        'INDcolPendingItemsToReconciled_DescriptionTransaction
        '
        Me.INDcolPendingItemsToReconciled_DescriptionTransaction.Caption = "Descripción Transacción"
        Me.INDcolPendingItemsToReconciled_DescriptionTransaction.FieldName = "ExtractDetail.DescriptionTransaction"
        Me.INDcolPendingItemsToReconciled_DescriptionTransaction.MinWidth = 21
        Me.INDcolPendingItemsToReconciled_DescriptionTransaction.Name = "INDcolPendingItemsToReconciled_DescriptionTransaction"
        Me.INDcolPendingItemsToReconciled_DescriptionTransaction.OptionsColumn.AllowEdit = False
        Me.INDcolPendingItemsToReconciled_DescriptionTransaction.OptionsColumn.AllowFocus = False
        Me.INDcolPendingItemsToReconciled_DescriptionTransaction.Visible = True
        Me.INDcolPendingItemsToReconciled_DescriptionTransaction.VisibleIndex = 4
        Me.INDcolPendingItemsToReconciled_DescriptionTransaction.Width = 69
        '
        'INDColPendingItemsToReconciled_DocumentType
        '
        Me.INDColPendingItemsToReconciled_DocumentType.Caption = "Tipo Documento"
        Me.INDColPendingItemsToReconciled_DocumentType.FieldName = "DocumentTypeName"
        Me.INDColPendingItemsToReconciled_DocumentType.MinWidth = 21
        Me.INDColPendingItemsToReconciled_DocumentType.Name = "INDColPendingItemsToReconciled_DocumentType"
        Me.INDColPendingItemsToReconciled_DocumentType.Visible = True
        Me.INDColPendingItemsToReconciled_DocumentType.VisibleIndex = 5
        '
        'INDColPendingItemsToReconciled_Detail
        '
        Me.INDColPendingItemsToReconciled_Detail.Caption = "Detalle"
        Me.INDColPendingItemsToReconciled_Detail.FieldName = "DocumentDetail.Observations"
        Me.INDColPendingItemsToReconciled_Detail.Name = "INDColPendingItemsToReconciled_Detail"
        Me.INDColPendingItemsToReconciled_Detail.Visible = True
        Me.INDColPendingItemsToReconciled_Detail.VisibleIndex = 6
        '
        'INDcolPendingItemsToReconciled_NatureName
        '
        Me.INDcolPendingItemsToReconciled_NatureName.Caption = "Naturaleza"
        Me.INDcolPendingItemsToReconciled_NatureName.FieldName = "NatureName"
        Me.INDcolPendingItemsToReconciled_NatureName.MinWidth = 21
        Me.INDcolPendingItemsToReconciled_NatureName.Name = "INDcolPendingItemsToReconciled_NatureName"
        Me.INDcolPendingItemsToReconciled_NatureName.OptionsColumn.AllowEdit = False
        Me.INDcolPendingItemsToReconciled_NatureName.OptionsColumn.AllowFocus = False
        Me.INDcolPendingItemsToReconciled_NatureName.UnboundExpression = "Iif([Nature] = 1, 'Débito', 'Crédito')"
        Me.INDcolPendingItemsToReconciled_NatureName.UnboundType = DevExpress.Data.UnboundColumnType.[String]
        Me.INDcolPendingItemsToReconciled_NatureName.Visible = True
        Me.INDcolPendingItemsToReconciled_NatureName.VisibleIndex = 7
        Me.INDcolPendingItemsToReconciled_NatureName.Width = 93
        '
        'INDcolPendingItemsToReconciled_Value
        '
        Me.INDcolPendingItemsToReconciled_Value.Caption = "Valor"
        Me.INDcolPendingItemsToReconciled_Value.DisplayFormat.FormatString = "c2"
        Me.INDcolPendingItemsToReconciled_Value.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDcolPendingItemsToReconciled_Value.FieldName = "Value"
        Me.INDcolPendingItemsToReconciled_Value.MinWidth = 21
        Me.INDcolPendingItemsToReconciled_Value.Name = "INDcolPendingItemsToReconciled_Value"
        Me.INDcolPendingItemsToReconciled_Value.OptionsColumn.AllowEdit = False
        Me.INDcolPendingItemsToReconciled_Value.OptionsColumn.AllowFocus = False
        Me.INDcolPendingItemsToReconciled_Value.Visible = True
        Me.INDcolPendingItemsToReconciled_Value.VisibleIndex = 8
        '
        'INDColPendingItems_ConsecutiveBank
        '
        Me.INDColPendingItems_ConsecutiveBank.Caption = "Consecutivo bancario"
        Me.INDColPendingItems_ConsecutiveBank.FieldName = "ExtractDetail.ConsecutiveBank"
        Me.INDColPendingItems_ConsecutiveBank.Name = "INDColPendingItems_ConsecutiveBank"
        '
        'INDColPendingItems_TransactionalCode
        '
        Me.INDColPendingItems_TransactionalCode.Caption = "Código transacción"
        Me.INDColPendingItems_TransactionalCode.FieldName = "ExtractDetail.TransactionCode"
        Me.INDColPendingItems_TransactionalCode.Name = "INDColPendingItems_TransactionalCode"
        '
        'INDColPendingItems_BankCheck
        '
        Me.INDColPendingItems_BankCheck.Caption = "Cheque"
        Me.INDColPendingItems_BankCheck.Name = "INDColPendingItems_BankCheck"
        '
        'INDColPendingItems_PaymentReferenceOne
        '
        Me.INDColPendingItems_PaymentReferenceOne.Caption = "Ref Pago 1"
        Me.INDColPendingItems_PaymentReferenceOne.FieldName = "ExtractDetail.PaymentReferenceOne"
        Me.INDColPendingItems_PaymentReferenceOne.Name = "INDColPendingItems_PaymentReferenceOne"
        '
        'INDColPendingItems_PaymentReferenceTwo
        '
        Me.INDColPendingItems_PaymentReferenceTwo.Caption = "Ref Pago 2"
        Me.INDColPendingItems_PaymentReferenceTwo.FieldName = "ExtractDetail.PaymentReferenceTwo"
        Me.INDColPendingItems_PaymentReferenceTwo.Name = "INDColPendingItems_PaymentReferenceTwo"
        '
        'INDColPendingitems_Comment
        '
        Me.INDColPendingitems_Comment.Caption = "Comentario"
        Me.INDColPendingitems_Comment.FieldName = "DocumentDetail.Comments"
        Me.INDColPendingitems_Comment.Name = "INDColPendingitems_Comment"
        '
        'INDColPendingActions
        '
        Me.INDColPendingActions.Caption = "Acciones"
        Me.INDColPendingActions.ColumnEdit = Me.INDRipcePendingActions
        Me.INDColPendingActions.Name = "INDColPendingActions"
        Me.INDColPendingActions.Visible = True
        Me.INDColPendingActions.VisibleIndex = 9
        '
        'INDRipcePendingActions
        '
        Me.INDRipcePendingActions.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDRipcePendingActions.Name = "INDRipcePendingActions"
        Me.INDRipcePendingActions.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.NoBorder
        Me.INDRipcePendingActions.PopupControl = Me.INDPccPendingActions
        Me.INDRipcePendingActions.PopupSizeable = False
        Me.INDRipcePendingActions.ShowPopupCloseButton = False
        Me.INDRipcePendingActions.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'INDColStatusReconciled
        '
        Me.INDColStatusReconciled.Caption = "Estado"
        Me.INDColStatusReconciled.FieldName = "ReconciledStatusName"
        Me.INDColStatusReconciled.Name = "INDColStatusReconciled"
        Me.INDColStatusReconciled.OptionsColumn.AllowEdit = False
        Me.INDColStatusReconciled.Visible = True
        Me.INDColStatusReconciled.VisibleIndex = 10
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlygPrincipalInformation, Me.INDlygEntityBankAccountInformation, Me.INDLcgBankStatements, Me.INDLcgPendingItemsToReconciled})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(3716, 559)
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
        Me.INDlygPrincipalInformation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciCode, Me.INDLciEntityBankAccount, Me.INDLciDocumentDate, Me.INDLciEntityBankAccountValue, Me.INDLciDifferenceReconcile, Me.INDLciExtractValue, Me.INDLciExtractValueMovement})
        Me.INDlygPrincipalInformation.Location = New System.Drawing.Point(0, 0)
        Me.INDlygPrincipalInformation.Name = "INDlygPrincipalInformation"
        Me.INDlygPrincipalInformation.Size = New System.Drawing.Size(414, 539)
        Me.INDlygPrincipalInformation.Text = "Información Principal"
        '
        'INDLciCode
        '
        Me.INDLciCode.Control = Me.INDbtnCode
        Me.INDLciCode.Location = New System.Drawing.Point(0, 0)
        Me.INDLciCode.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciCode.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciCode.Name = "INDLciCode"
        Me.INDLciCode.Size = New System.Drawing.Size(390, 60)
        Me.INDLciCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciCode.Text = "Código"
        Me.INDLciCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciCode.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciCode.TextToControlDistance = 5
        '
        'INDLciEntityBankAccount
        '
        Me.INDLciEntityBankAccount.Control = Me.INDsleEntityBankAccount
        Me.INDLciEntityBankAccount.Location = New System.Drawing.Point(0, 60)
        Me.INDLciEntityBankAccount.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciEntityBankAccount.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciEntityBankAccount.Name = "INDLciEntityBankAccount"
        Me.INDLciEntityBankAccount.Size = New System.Drawing.Size(390, 60)
        Me.INDLciEntityBankAccount.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciEntityBankAccount.Text = "Cuenta Bancaria"
        Me.INDLciEntityBankAccount.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciEntityBankAccount.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciEntityBankAccount.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciEntityBankAccount.TextToControlDistance = 5
        '
        'INDLciDocumentDate
        '
        Me.INDLciDocumentDate.Control = Me.INDdteDocumentDate
        Me.INDLciDocumentDate.Location = New System.Drawing.Point(0, 120)
        Me.INDLciDocumentDate.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciDocumentDate.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciDocumentDate.Name = "INDLciDocumentDate"
        Me.INDLciDocumentDate.Size = New System.Drawing.Size(390, 60)
        Me.INDLciDocumentDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDocumentDate.Text = "Fecha"
        Me.INDLciDocumentDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciDocumentDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDocumentDate.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciDocumentDate.TextToControlDistance = 5
        '
        'INDLciEntityBankAccountValue
        '
        Me.INDLciEntityBankAccountValue.Control = Me.INDseEndEntityBankAccountValue
        Me.INDLciEntityBankAccountValue.Location = New System.Drawing.Point(0, 180)
        Me.INDLciEntityBankAccountValue.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciEntityBankAccountValue.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciEntityBankAccountValue.Name = "INDLciEntityBankAccountValue"
        Me.INDLciEntityBankAccountValue.Size = New System.Drawing.Size(390, 60)
        Me.INDLciEntityBankAccountValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciEntityBankAccountValue.Text = "Saldo Final del Libro de Bancos"
        Me.INDLciEntityBankAccountValue.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciEntityBankAccountValue.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciEntityBankAccountValue.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciEntityBankAccountValue.TextToControlDistance = 5
        '
        'INDLciDifferenceReconcile
        '
        Me.INDLciDifferenceReconcile.Control = Me.INDseDifferenceReconcile
        Me.INDLciDifferenceReconcile.Location = New System.Drawing.Point(0, 360)
        Me.INDLciDifferenceReconcile.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciDifferenceReconcile.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciDifferenceReconcile.Name = "INDLciDifferenceReconcile"
        Me.INDLciDifferenceReconcile.Size = New System.Drawing.Size(390, 126)
        Me.INDLciDifferenceReconcile.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDifferenceReconcile.Text = "Diferencia a Conciliar"
        Me.INDLciDifferenceReconcile.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciDifferenceReconcile.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDifferenceReconcile.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciDifferenceReconcile.TextToControlDistance = 5
        '
        'INDLciExtractValue
        '
        Me.INDLciExtractValue.Control = Me.INDseExtractValue
        Me.INDLciExtractValue.Location = New System.Drawing.Point(0, 300)
        Me.INDLciExtractValue.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciExtractValue.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciExtractValue.Name = "INDLciExtractValue"
        Me.INDLciExtractValue.Size = New System.Drawing.Size(390, 60)
        Me.INDLciExtractValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciExtractValue.Text = "Saldo Final del Extracto"
        Me.INDLciExtractValue.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciExtractValue.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciExtractValue.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciExtractValue.TextToControlDistance = 5
        '
        'INDLciExtractValueMovement
        '
        Me.INDLciExtractValueMovement.Control = Me.INDseExtractValueMovement
        Me.INDLciExtractValueMovement.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciExtractValueMovement.CustomizationFormText = "Saldo Movimientos del Extracto"
        Me.INDLciExtractValueMovement.Location = New System.Drawing.Point(0, 240)
        Me.INDLciExtractValueMovement.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciExtractValueMovement.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciExtractValueMovement.Name = "INDLciExtractValueMovement"
        Me.INDLciExtractValueMovement.Size = New System.Drawing.Size(390, 60)
        Me.INDLciExtractValueMovement.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciExtractValueMovement.Text = "Saldo Movimientos del Extracto"
        Me.INDLciExtractValueMovement.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciExtractValueMovement.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciExtractValueMovement.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciExtractValueMovement.TextToControlDistance = 5
        Me.INDLciExtractValueMovement.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
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
        ButtonImageOptions1.Image = CType(resources.GetObject("ButtonImageOptions1.Image"), System.Drawing.Image)
        Me.INDlygEntityBankAccountInformation.CustomHeaderButtons.AddRange(New DevExpress.XtraEditors.ButtonPanel.IBaseButton() {New DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("", True, ButtonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, True, Nothing, True, False, True, Nothing, -1)})
        Me.INDlygEntityBankAccountInformation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciBankReconciliationDetails, Me.LayoutControlItem1})
        Me.INDlygEntityBankAccountInformation.Location = New System.Drawing.Point(414, 0)
        Me.INDlygEntityBankAccountInformation.Name = "INDlygEntityBankAccountInformation"
        Me.INDlygEntityBankAccountInformation.Size = New System.Drawing.Size(1386, 539)
        Me.INDlygEntityBankAccountInformation.Text = "Libro de Bancos"
        '
        'INDLciBankReconciliationDetails
        '
        Me.INDLciBankReconciliationDetails.AppearanceItemCaption.Options.UseTextOptions = True
        Me.INDLciBankReconciliationDetails.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDLciBankReconciliationDetails.Control = Me.INDgcTreasury
        Me.INDLciBankReconciliationDetails.Location = New System.Drawing.Point(0, 0)
        Me.INDLciBankReconciliationDetails.MinSize = New System.Drawing.Size(681, 1)
        Me.INDLciBankReconciliationDetails.Name = "INDLciBankReconciliationDetails"
        Me.INDLciBankReconciliationDetails.Size = New System.Drawing.Size(681, 480)
        Me.INDLciBankReconciliationDetails.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciBankReconciliationDetails.Text = "Tesorería"
        Me.INDLciBankReconciliationDetails.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.INDLciBankReconciliationDetails.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciBankReconciliationDetails.TextSize = New System.Drawing.Size(59, 17)
        Me.INDLciBankReconciliationDetails.TextToControlDistance = 5
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem1.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LayoutControlItem1.Control = Me.INDGcBankReconciliation
        Me.LayoutControlItem1.Location = New System.Drawing.Point(681, 0)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(681, 1)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(681, 480)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.Text = "Conciliación"
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.LayoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(78, 17)
        Me.LayoutControlItem1.TextToControlDistance = 5
        '
        'INDLcgBankStatements
        '
        Me.INDLcgBankStatements.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgBankStatements.AppearanceGroup.Options.UseFont = True
        Me.INDLcgBankStatements.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgBankStatements.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgBankStatements.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBankStatements.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgBankStatements.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgBankStatements.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgBankStatements.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBankStatements.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgBankStatements.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBankStatements.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgBankStatements.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBankStatements.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgBankStatements, False)
        Me.INDLcgBankStatements.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem2})
        Me.INDLcgBankStatements.Location = New System.Drawing.Point(1800, 0)
        Me.INDLcgBankStatements.Name = "INDLcgBankStatements"
        Me.INDLcgBankStatements.Size = New System.Drawing.Size(948, 539)
        Me.INDLcgBankStatements.Text = "Extracto Bancario"
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDGcBankStatements
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(924, 24)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(924, 486)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'INDLcgPendingItemsToReconciled
        '
        Me.INDLcgPendingItemsToReconciled.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgPendingItemsToReconciled.AppearanceGroup.Options.UseFont = True
        Me.INDLcgPendingItemsToReconciled.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgPendingItemsToReconciled.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgPendingItemsToReconciled.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPendingItemsToReconciled.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgPendingItemsToReconciled.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgPendingItemsToReconciled.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgPendingItemsToReconciled.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPendingItemsToReconciled.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgPendingItemsToReconciled.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPendingItemsToReconciled.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgPendingItemsToReconciled.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPendingItemsToReconciled.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgPendingItemsToReconciled, False)
        Me.INDLcgPendingItemsToReconciled.CustomizationFormText = "Extracto Bancario"
        Me.INDLcgPendingItemsToReconciled.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem9})
        Me.INDLcgPendingItemsToReconciled.Location = New System.Drawing.Point(2748, 0)
        Me.INDLcgPendingItemsToReconciled.Name = "INDLcgPendingItemsToReconciled"
        Me.INDLcgPendingItemsToReconciled.Size = New System.Drawing.Size(948, 539)
        Me.INDLcgPendingItemsToReconciled.Text = "Partidas pendientes por Conciliar / Otros"
        '
        'LayoutControlItem9
        '
        Me.LayoutControlItem9.Control = Me.INDGcPendingItemsToReconciled
        Me.LayoutControlItem9.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.LayoutControlItem9.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem9.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem9.MinSize = New System.Drawing.Size(924, 24)
        Me.LayoutControlItem9.Name = "LayoutControlItem9"
        Me.LayoutControlItem9.Size = New System.Drawing.Size(924, 486)
        Me.LayoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem9.Text = "LayoutControlItem2"
        Me.LayoutControlItem9.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem9.TextVisible = False
        '
        'INDPopupActions
        '
        Me.INDPopupActions.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.INDBbiComment), New DevExpress.XtraBars.LinkPersistInfo(Me.INDBbiManualReconciliation), New DevExpress.XtraBars.LinkPersistInfo(Me.INDBbiPendingToReconciled), New DevExpress.XtraBars.LinkPersistInfo(Me.INDBbiDiscard)})
        Me.INDPopupActions.Manager = Me.BarManager1
        Me.INDPopupActions.Name = "INDPopupActions"
        '
        'INDRipc_PccBankStatementsDetail
        '
        Me.INDRipc_PccBankStatementsDetail.AllowDropDownWhenReadOnly = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDRipc_PccBankStatementsDetail.AutoHeight = False
        Me.INDRipc_PccBankStatementsDetail.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDRipc_PccBankStatementsDetail.Name = "INDRipc_PccBankStatementsDetail"
        Me.INDRipc_PccBankStatementsDetail.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'INDIcb_PccBankStatementsDetail
        '
        Me.INDIcb_PccBankStatementsDetail.AutoHeight = False
        Me.INDIcb_PccBankStatementsDetail.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDIcb_PccBankStatementsDetail.Name = "INDIcb_PccBankStatementsDetail"
        '
        'IndigoGridViewBankBook
        '
        Me.IndigoGridViewBankBook.RaiseMenuPopUp = True
        Me.IndigoGridViewBankBook.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'IndigoGridViewBankStatement
        '
        Me.IndigoGridViewBankStatement.RaiseMenuPopUp = True
        Me.IndigoGridViewBankStatement.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit2
        '
        'RepositoryItemPopupContainerEdit41
        '
        Me.RepositoryItemPopupContainerEdit41.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit41.Name = "RepositoryItemPopupContainerEdit41"
        '
        'RepositoryItemPopupContainerEdit21
        '
        Me.RepositoryItemPopupContainerEdit21.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit21.Name = "RepositoryItemPopupContainerEdit21"
        '
        'RepositoryItemPopupContainerEdit11
        '
        Me.RepositoryItemPopupContainerEdit11.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit11.Name = "RepositoryItemPopupContainerEdit11"
        '
        'IndigoGridViewPendingItems
        '
        Me.IndigoGridViewPendingItems.RaiseMenuPopUp = True
        Me.IndigoGridViewPendingItems.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit3
        '
        'BarManager2
        '
        Me.BarManager2.DockControls.Add(Me.BarDockControl1)
        Me.BarManager2.DockControls.Add(Me.BarDockControl2)
        Me.BarManager2.DockControls.Add(Me.BarDockControl3)
        Me.BarManager2.DockControls.Add(Me.BarDockControl4)
        Me.BarManager2.Form = Me
        Me.BarManager2.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.INDBbiMoveBankBook})
        Me.BarManager2.MaxItemId = 1
        '
        'BarDockControl1
        '
        Me.BarDockControl1.CausesValidation = False
        Me.BarDockControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.BarDockControl1.Location = New System.Drawing.Point(0, 6)
        Me.BarDockControl1.Manager = Me.BarManager2
        Me.BarDockControl1.Size = New System.Drawing.Size(1295, 0)
        '
        'BarDockControl2
        '
        Me.BarDockControl2.CausesValidation = False
        Me.BarDockControl2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.BarDockControl2.Location = New System.Drawing.Point(0, 722)
        Me.BarDockControl2.Manager = Me.BarManager2
        Me.BarDockControl2.Size = New System.Drawing.Size(1295, 0)
        '
        'BarDockControl3
        '
        Me.BarDockControl3.CausesValidation = False
        Me.BarDockControl3.Dock = System.Windows.Forms.DockStyle.Left
        Me.BarDockControl3.Location = New System.Drawing.Point(0, 6)
        Me.BarDockControl3.Manager = Me.BarManager2
        Me.BarDockControl3.Size = New System.Drawing.Size(0, 716)
        '
        'BarDockControl4
        '
        Me.BarDockControl4.CausesValidation = False
        Me.BarDockControl4.Dock = System.Windows.Forms.DockStyle.Right
        Me.BarDockControl4.Location = New System.Drawing.Point(1295, 6)
        Me.BarDockControl4.Manager = Me.BarManager2
        Me.BarDockControl4.Size = New System.Drawing.Size(0, 716)
        '
        'INDBbiMoveBankBook
        '
        Me.INDBbiMoveBankBook.Caption = "Mover al libro de bancos"
        Me.INDBbiMoveBankBook.Id = 0
        Me.INDBbiMoveBankBook.ImageOptions.Image = CType(resources.GetObject("INDBbiMoveBankBook.ImageOptions.Image"), System.Drawing.Image)
        Me.INDBbiMoveBankBook.ImageOptions.LargeImage = CType(resources.GetObject("INDBbiMoveBankBook.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.INDBbiMoveBankBook.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.INDBbiMoveBankBook.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDBbiMoveBankBook.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.INDBbiMoveBankBook.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDBbiMoveBankBook.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.INDBbiMoveBankBook.ItemAppearance.Normal.Options.UseFont = True
        Me.INDBbiMoveBankBook.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.INDBbiMoveBankBook.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDBbiMoveBankBook.Name = "INDBbiMoveBankBook"
        '
        'INDPopupPendingActions
        '
        Me.INDPopupPendingActions.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.INDBbiMoveBankBook)})
        Me.INDPopupPendingActions.Manager = Me.BarManager2
        Me.INDPopupPendingActions.Name = "INDPopupPendingActions"
        '
        'FrmBankConciliationAutomatic
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1295, 722)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Controls.Add(Me.BarDockControl3)
        Me.Controls.Add(Me.BarDockControl4)
        Me.Controls.Add(Me.BarDockControl2)
        Me.Controls.Add(Me.BarDockControl1)
        Me.IconOptions.ShowIcon = False
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmBankConciliationAutomatic"
        Me.Opacity = 1.0R
        Me.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.Tag = "2236"
        Me.Text = "Conciliación Bancaria Automática"
        Me.Controls.SetChildIndex(Me.BarDockControl1, 0)
        Me.Controls.SetChildIndex(Me.BarDockControl2, 0)
        Me.Controls.SetChildIndex(Me.BarDockControl4, 0)
        Me.Controls.SetChildIndex(Me.BarDockControl3, 0)
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
        CType(Me.INDgvNoteCashReceipts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcTreasury, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvTreasury, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepSelectOption, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepSleDetailDocumentType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepSleDetailNature, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRepReconciled, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRipcActions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPccActions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPccActions.ResumeLayout(False)
        CType(Me.RepositoryItemPopupContainerEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyRoot.ResumeLayout(False)
        Me.INDlyRoot.PerformLayout()
        CType(Me.INDPccPendingActions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPccPendingActions.ResumeLayout(False)
        CType(Me.INDPcComments, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPcComments.ResumeLayout(False)
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl2.ResumeLayout(False)
        CType(Me.INDMeComments.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl4.ResumeLayout(False)
        CType(Me.INDPcNoteType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPcNoteType.ResumeLayout(False)
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDSleNoteType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSeValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciNoteType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl3.ResumeLayout(False)
        CType(Me.INDGcBankReconciliation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvConciliation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRiceReconciled, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemSearchLookUpEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemSearchLookUpEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcBankStatements, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvBankStatements, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRiceExtractBank, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepSleDocumentType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemSearchLookUpEdit3View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepSleExtractDetailDocumentType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepGvExtractDetailDocumentType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepSleExtractDetailNature, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepGvExtractDetailNature, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepTxtExtractDetailValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDseDifferenceReconcile.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDseEndEntityBankAccountValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDseExtractValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteDocumentDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteDocumentDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleEntityBankAccount.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDseExtractValueMovement.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcPendingItemsToReconciled, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvPendingItemsToReconciled, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRiceExtractBank1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRipcePendingActions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygPrincipalInformation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciEntityBankAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDocumentDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciEntityBankAccountValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDifferenceReconcile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciExtractValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciExtractValueMovement, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygEntityBankAccountInformation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciBankReconciliationDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgBankStatements, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgPendingItemsToReconciled, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPopupActions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRipc_PccBankStatementsDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDIcb_PccBankStatementsDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridViewBankBook, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridViewBankStatement, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit41, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridViewPendingItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPopupPendingActions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

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
    Friend WithEvents IndigoGridViewBankBook As IndigoGridView
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
    Friend WithEvents INDGcBankStatements As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvBankStatements As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDgcTreasury As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvTreasury As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlygEntityBankAccountInformation As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciBankReconciliationDetails As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLcgBankStatements As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDcolBankReconciliationDetail_Select As DevExpress.XtraGrid.Columns.GridColumn
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
    Friend WithEvents INDcolBankReconciliationAutomaticExtractDetail_TransactionDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolBankReconciliationAutomaticExtractDetail_ConsecutiveBank As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolBankReconciliationAutomaticExtractDetail_TransactionCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolBankReconciliationAutomaticExtractDetail_DescriptionTransaction As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepSleExtractDetailDocumentType As DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit
    Friend WithEvents INDrepGvExtractDetailDocumentType As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDrepColExtractDetailDocumentType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepSleExtractDetailNature As DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit
    Friend WithEvents INDrepGvExtractDetailNature As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDrepColExtractDetailNature As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepTxtExtractDetailValue As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents INDcolBankReconciliationAutomaticExtractDetail_BankCheck As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolBankReconciliationAutomaticExtractDetail_PaymentReferenceOne As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolBankReconciliationAutomaticExtractDetail_PaymentReferenceTwo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDIcb_PccBankStatementsDetail As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents INDRipc_PccBankStatementsDetail As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDRiceExtractBank As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents INDcolBankReconciliationAutomaticExtractDetail_NatureName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolBankExtract_Select As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDseExtractValueMovement As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDLciExtractValueMovement As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDcolBankReconciliationAutomaticExtractDetail_Nature As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolBankReconciliationAutomaticExtractDetail_Value As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDColBankDocumentType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGcBankReconciliation As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvConciliation As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDRiceReconciled As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemSearchLookUpEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryItemSearchLookUpEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGColConsecutiveStatement As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGColDateStatement As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGColValueStatement As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGColComment As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGColReconciled As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepSelectOption As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents INDrepSleDetailDocumentType As DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit
    Friend WithEvents INDrepSleDetailNature As DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit
    Friend WithEvents INDPcNoteType As DevExpress.XtraBars.PopupControlContainer
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDSleNoteType As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSeValue As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciNoteType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents PanelControl3 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents BtnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents IndigoGridViewBankStatement As IndigoGridView
    Friend WithEvents RepositoryItemPopupContainerEdit4 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents Label1 As Label
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents INDrepSleDocumentType As DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit
    Friend WithEvents RepositoryItemSearchLookUpEdit3View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColComments As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRepReconciled As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents INDColDocumentCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDPcComments As DevExpress.XtraBars.PopupControlContainer
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LayoutControl2 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LblComments As Label
    Friend WithEvents INDMeComments As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents Label2 As Label
    Friend WithEvents LayoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents PanelControl4 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDBtnAccept As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDColReconciled As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGColNature As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGColDocumentType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoSimpleButton11 As IndigoSimpleButton
    Friend WithEvents RepositoryItemPopupContainerEdit41 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit21 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit11 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDGcPendingItemsToReconciled As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvPendingItemsToReconciled As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDcolPendingItemsToReconciled_Select As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRiceExtractBank1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents INDcolPendingItemsToReconciled_Date As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolPendingItemsToReconciled_Origin As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColPendingItemsToReconciled_DocumentType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolPendingItemsToReconciled_Code As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolPendingItemsToReconciled_DescriptionTransaction As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolPendingItemsToReconciled_NatureName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolPendingItemsToReconciled_Value As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLcgPendingItemsToReconciled As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDColPendingItemsToReconciled_Detail As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColPendingItems_ConsecutiveBank As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColPendingitems_Comment As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridViewPendingItems As IndigoGridView
    Friend WithEvents RepositoryItemPopupContainerEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDColPendingItems_TransactionalCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColPendingItems_BankCheck As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColPendingItems_PaymentReferenceOne As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColPendingItems_PaymentReferenceTwo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDBbiComment As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDPopupActions As DevExpress.XtraBars.PopupMenu
    Friend WithEvents INDBbiManualReconciliation As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDPccActions As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents INDColActions As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRipcActions As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDSbManualReconciliation As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDSbComment As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDSbPendingToReconciled As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDSbDiscard As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDPccPendingActions As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents INDSbMoveToBookBank As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDColPendingActions As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRipcePendingActions As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents BarDockControl3 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarManager2 As DevExpress.XtraBars.BarManager
    Friend WithEvents BarDockControl1 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl2 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl4 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents INDBbiMoveBankBook As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDPopupPendingActions As DevExpress.XtraBars.PopupMenu
    Friend WithEvents INDBbiPendingToReconciled As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDBbiDiscard As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDColStatusReconciled As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColPeriod As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColExtractPeriod As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgvNoteCashReceipts As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
End Class
