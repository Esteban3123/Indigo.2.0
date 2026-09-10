Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPurchaseOrder
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPurchaseOrder))
        Dim AppearanceObject5 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject6 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim AppearanceObject7 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject8 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject9 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject10 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject11 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject12 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject13 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject14 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject15 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject16 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject17 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject18 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlyPurchaseOrder = New DevExpress.XtraLayout.LayoutControl()
        Me.INDtxtValue = New DevExpress.XtraEditors.TextEdit()
        Me.INDSleBudgetaryValidityId = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit2View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleBudgetaryEntityId = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDBtnImportFile = New DevExpress.XtraEditors.SimpleButton()
        Me.INDEsbDetails = New Presentation.Controls.ExportStructureButton()
        Me.INDCmbContractBased = New DevExpress.XtraEditors.ImageComboBoxEdit()
        Me.INDSeLookEdContract = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColCodeContractInventory = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColCodeNameContractTypeInventory = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColContractNumberInventory = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDCmbOrderType = New DevExpress.XtraEditors.ImageComboBoxEdit()
        Me.INDbtnCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDGcProducts = New DevExpress.XtraGrid.GridControl()
        Me.INDGvProducts = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Quantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.OutstandingQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColDiscountPercent = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRepPercent = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.INDColIVA = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColTotalIva = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColUnitValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRepTexQuantityOrValues = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.INDColTotalValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn14 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn15 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn16 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDBtnAddProducts = New DevExpress.XtraEditors.SimpleButton()
        Me.INDdeDateDelivery = New DevExpress.XtraEditors.DateEdit()
        Me.INDteDeliveryPlace = New DevExpress.XtraEditors.TextEdit()
        Me.INDteDeliveryForm = New DevExpress.XtraEditors.TextEdit()
        Me.INDsleStock = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGdvStock = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColWarehouseCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColWarehouseName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDdeDateDocument = New DevExpress.XtraEditors.DateEdit()
        Me.INDsleSupplier = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.viewSupplier = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.IndColNitSupplier = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColSupplierName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColDistributionName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDmeDetail = New DevExpress.XtraEditors.MemoEdit()
        Me.INDgcAvailability = New DevExpress.XtraGrid.GridControl()
        Me.INDviewAvailability = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn154 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn155 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn156 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn158 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn159 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn160 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepTxtValue = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.INDsleAvailability = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDviewSearchAvailability = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn147 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn149 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn150 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn151 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn152 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDbtnAddAvailability = New DevExpress.XtraEditors.SimpleButton()
        Me.INDtxtIvaValue = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtTotalValue = New DevExpress.XtraEditors.TextEdit()
        Me.INDsleCurrency = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvCurrencyAdvance = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1179 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1180 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1181 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDteDaysTerm = New DevExpress.XtraEditors.TextEdit()
        Me.INDsleFunctionalUnitRequest = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGdvStock1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColWarehouseCode1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColWarehouseName1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleTypePaymentMethod = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGdvStock11 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColWarehouseName11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemContract = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemBasedContract = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemDateDocument = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemSupplier = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemDetail = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemOrderType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciCurrency = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemDeliveryForm = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemDeliveryPlace = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemDateDelivery = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciIvaValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciTotalValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemStock = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemDaysTerm = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemFunctionalUnitRequest = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemTypePaymentMethod = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyGpProducts = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyItemAddProducts = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyItemGcProducts = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygBudget = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemGridAvailability = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemAvailability = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemAddAvailability = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciBudgetaryEntityId = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciBudgetaryValidityId = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoComboBoxEdit1 = New Presentation.Controls.IndigoComboBoxEdit(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridView2 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.RepositoryItemPopupContainerEdit11 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit21 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit31 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit12 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit121 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit32 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit13 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit311 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyPurchaseOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyPurchaseOrder.SuspendLayout()
        CType(Me.INDtxtValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleBudgetaryValidityId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit2View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleBudgetaryEntityId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDCmbContractBased.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSeLookEdContract.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDCmbOrderType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcProducts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvProducts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRepPercent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRepTexQuantityOrValues, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdeDateDelivery.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdeDateDelivery.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDteDeliveryPlace.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDteDeliveryForm.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleStock.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGdvStock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdeDateDocument.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdeDateDocument.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleSupplier.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.viewSupplier, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDmeDetail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcAvailability, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewAvailability, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepTxtValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleAvailability.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewSearchAvailability, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtIvaValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtTotalValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleCurrency.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvCurrencyAdvance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDteDaysTerm.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleFunctionalUnitRequest.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGdvStock1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleTypePaymentMethod.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGdvStock11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemContract, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemBasedContract, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDateDocument, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemSupplier, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemOrderType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciCurrency, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDeliveryForm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDeliveryPlace, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDateDelivery, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciIvaValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciTotalValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemStock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDaysTerm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemFunctionalUnitRequest, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemTypePaymentMethod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGpProducts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemAddProducts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemGcProducts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygBudget, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemGridAvailability, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemAvailability, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemAddAvailability, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciBudgetaryEntityId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciBudgetaryValidityId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoComboBoxEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit31, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit121, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit32, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit311, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyPurchaseOrder)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 136)
        Me.INDPanelControlBase.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDPanelControlBase.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1298, 569)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Location = New System.Drawing.Point(0, 6)
        Me.ToolBars.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ToolBars.Size = New System.Drawing.Size(1298, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.BarraBotones.Size = New System.Drawing.Size(1298, 130)
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'RepositoryItemPopupContainerEdit3
        '
        Me.RepositoryItemPopupContainerEdit3.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit3.Name = "RepositoryItemPopupContainerEdit3"
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDlyPurchaseOrder
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 8)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 559)
        Me.CtrNavigationControl1.TabIndex = 10
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDlyPurchaseOrder
        '
        Me.INDlyPurchaseOrder.AllowCustomization = False
        Me.INDlyPurchaseOrder.Controls.Add(Me.INDtxtValue)
        Me.INDlyPurchaseOrder.Controls.Add(Me.INDSleBudgetaryValidityId)
        Me.INDlyPurchaseOrder.Controls.Add(Me.INDSleBudgetaryEntityId)
        Me.INDlyPurchaseOrder.Controls.Add(Me.INDBtnImportFile)
        Me.INDlyPurchaseOrder.Controls.Add(Me.INDEsbDetails)
        Me.INDlyPurchaseOrder.Controls.Add(Me.INDCmbContractBased)
        Me.INDlyPurchaseOrder.Controls.Add(Me.INDSeLookEdContract)
        Me.INDlyPurchaseOrder.Controls.Add(Me.INDCmbOrderType)
        Me.INDlyPurchaseOrder.Controls.Add(Me.INDbtnCode)
        Me.INDlyPurchaseOrder.Controls.Add(Me.INDGcProducts)
        Me.INDlyPurchaseOrder.Controls.Add(Me.INDBtnAddProducts)
        Me.INDlyPurchaseOrder.Controls.Add(Me.INDdeDateDelivery)
        Me.INDlyPurchaseOrder.Controls.Add(Me.INDteDeliveryPlace)
        Me.INDlyPurchaseOrder.Controls.Add(Me.INDteDeliveryForm)
        Me.INDlyPurchaseOrder.Controls.Add(Me.INDsleStock)
        Me.INDlyPurchaseOrder.Controls.Add(Me.INDdeDateDocument)
        Me.INDlyPurchaseOrder.Controls.Add(Me.INDsleSupplier)
        Me.INDlyPurchaseOrder.Controls.Add(Me.INDmeDetail)
        Me.INDlyPurchaseOrder.Controls.Add(Me.INDgcAvailability)
        Me.INDlyPurchaseOrder.Controls.Add(Me.INDsleAvailability)
        Me.INDlyPurchaseOrder.Controls.Add(Me.INDbtnAddAvailability)
        Me.INDlyPurchaseOrder.Controls.Add(Me.INDtxtIvaValue)
        Me.INDlyPurchaseOrder.Controls.Add(Me.INDtxtTotalValue)
        Me.INDlyPurchaseOrder.Controls.Add(Me.INDsleCurrency)
        Me.INDlyPurchaseOrder.Controls.Add(Me.INDteDaysTerm)
        Me.INDlyPurchaseOrder.Controls.Add(Me.INDsleFunctionalUnitRequest)
        Me.INDlyPurchaseOrder.Controls.Add(Me.INDsleTypePaymentMethod)
        Me.INDlyPurchaseOrder.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDlyPurchaseOrder, False)
        Me.INDlyPurchaseOrder.Location = New System.Drawing.Point(202, 8)
        Me.INDlyPurchaseOrder.Name = "INDlyPurchaseOrder"
        Me.INDlyPurchaseOrder.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(248, 143, 914, 668)
        Me.INDlyPurchaseOrder.Root = Me.LayoutControlGroup1
        Me.INDlyPurchaseOrder.Size = New System.Drawing.Size(1094, 559)
        Me.INDlyPurchaseOrder.TabIndex = 11
        Me.INDlyPurchaseOrder.Text = "LayoutControl1"
        '
        'INDtxtValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtValue, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtValue, False)
        Me.INDtxtValue.EnterMoveNextControl = True
        Me.INDtxtValue.Location = New System.Drawing.Point(-383, 527)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtValue, Presentation.Controls.IndigoTextEdit.EMask.MonedaDecimales)
        Me.INDtxtValue.Name = "INDtxtValue"
        Me.INDtxtValue.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtValue.Properties.Appearance.Options.UseFont = True
        Me.INDtxtValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtValue.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtValue.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtValue.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtValue.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtValue.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtValue.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtValue.Properties.Mask.EditMask = "c2"
        Me.INDtxtValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtValue.Properties.MaxLength = 20
        Me.INDtxtValue.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtValue.StyleController = Me.INDlyPurchaseOrder
        Me.INDtxtValue.TabIndex = 38
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtValue, 0)
        '
        'INDSleBudgetaryValidityId
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleBudgetaryValidityId, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleBudgetaryValidityId, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleBudgetaryValidityId, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleBudgetaryValidityId, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleBudgetaryValidityId, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleBudgetaryValidityId, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleBudgetaryValidityId, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleBudgetaryValidityId, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleBudgetaryValidityId, False)
        Me.INDSleBudgetaryValidityId.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleBudgetaryValidityId, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleBudgetaryValidityId, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleBudgetaryValidityId, False)
        Me.INDSleBudgetaryValidityId.Location = New System.Drawing.Point(1068, 89)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleBudgetaryValidityId, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleBudgetaryValidityId.Name = "INDSleBudgetaryValidityId"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleBudgetaryValidityId, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleBudgetaryValidityId, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleBudgetaryValidityId, False)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDSleBudgetaryValidityId, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleBudgetaryValidityId, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleBudgetaryValidityId, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleBudgetaryValidityId, False)
        Me.INDSleBudgetaryValidityId.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleBudgetaryValidityId.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleBudgetaryValidityId.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleBudgetaryValidityId.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleBudgetaryValidityId.Properties.Appearance.Options.UseFont = True
        Me.INDSleBudgetaryValidityId.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleBudgetaryValidityId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleBudgetaryValidityId.Properties.DisplayMember = "Year"
        Me.INDSleBudgetaryValidityId.Properties.NullText = ""
        Me.INDSleBudgetaryValidityId.Properties.PopupSizeable = False
        Me.INDSleBudgetaryValidityId.Properties.PopupView = Me.SearchLookUpEdit2View
        Me.INDSleBudgetaryValidityId.Properties.ShowFooter = False
        Me.INDSleBudgetaryValidityId.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleBudgetaryValidityId, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleBudgetaryValidityId, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleBudgetaryValidityId, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleBudgetaryValidityId, True)
        Me.INDSleBudgetaryValidityId.Size = New System.Drawing.Size(411, 28)
        Me.INDSleBudgetaryValidityId.StyleController = Me.INDlyPurchaseOrder
        Me.INDSleBudgetaryValidityId.TabIndex = 37
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleBudgetaryValidityId, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleBudgetaryValidityId, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleBudgetaryValidityId, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleBudgetaryValidityId, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleBudgetaryValidityId, False)
        '
        'SearchLookUpEdit2View
        '
        Me.SearchLookUpEdit2View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit2View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit2View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit2View.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit2View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit2View.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit2View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit2View.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit2View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit2View.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit2View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn10, Me.GridColumn11, Me.GridColumn12, Me.GridColumn13})
        Me.SearchLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit2View.Name = "SearchLookUpEdit2View"
        Me.SearchLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit2View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit2View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit2View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit2View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.SearchLookUpEdit2View, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit2View, False)
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "Año"
        Me.GridColumn10.FieldName = "Year"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 0
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "Resolución"
        Me.GridColumn11.FieldName = "ResolutionNumber"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.Visible = True
        Me.GridColumn11.VisibleIndex = 1
        '
        'GridColumn12
        '
        Me.GridColumn12.Caption = "Valor"
        Me.GridColumn12.DisplayFormat.FormatString = "c0"
        Me.GridColumn12.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn12.FieldName = "ResolutionValue"
        Me.GridColumn12.Name = "GridColumn12"
        Me.GridColumn12.Visible = True
        Me.GridColumn12.VisibleIndex = 2
        '
        'GridColumn13
        '
        Me.GridColumn13.Caption = "Estado"
        Me.GridColumn13.FieldName = "Status"
        Me.GridColumn13.Name = "GridColumn13"
        Me.GridColumn13.Visible = True
        Me.GridColumn13.VisibleIndex = 3
        '
        'INDSleBudgetaryEntityId
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleBudgetaryEntityId, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleBudgetaryEntityId, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleBudgetaryEntityId, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleBudgetaryEntityId, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleBudgetaryEntityId, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleBudgetaryEntityId, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleBudgetaryEntityId, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleBudgetaryEntityId, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleBudgetaryEntityId, False)
        Me.INDSleBudgetaryEntityId.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleBudgetaryEntityId, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleBudgetaryEntityId, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleBudgetaryEntityId, False)
        Me.INDSleBudgetaryEntityId.Location = New System.Drawing.Point(1068, 53)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleBudgetaryEntityId, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleBudgetaryEntityId.Name = "INDSleBudgetaryEntityId"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleBudgetaryEntityId, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleBudgetaryEntityId, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleBudgetaryEntityId, False)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDSleBudgetaryEntityId, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleBudgetaryEntityId, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleBudgetaryEntityId, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleBudgetaryEntityId, False)
        Me.INDSleBudgetaryEntityId.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleBudgetaryEntityId.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleBudgetaryEntityId.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleBudgetaryEntityId.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleBudgetaryEntityId.Properties.Appearance.Options.UseFont = True
        Me.INDSleBudgetaryEntityId.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleBudgetaryEntityId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleBudgetaryEntityId.Properties.DisplayMember = "NameCode"
        Me.INDSleBudgetaryEntityId.Properties.NullText = ""
        Me.INDSleBudgetaryEntityId.Properties.PopupSizeable = False
        Me.INDSleBudgetaryEntityId.Properties.PopupView = Me.GridView1
        Me.INDSleBudgetaryEntityId.Properties.ShowFooter = False
        Me.INDSleBudgetaryEntityId.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleBudgetaryEntityId, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleBudgetaryEntityId, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleBudgetaryEntityId, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleBudgetaryEntityId, True)
        Me.INDSleBudgetaryEntityId.Size = New System.Drawing.Size(411, 28)
        Me.INDSleBudgetaryEntityId.StyleController = Me.INDlyPurchaseOrder
        Me.INDSleBudgetaryEntityId.TabIndex = 36
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleBudgetaryEntityId, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleBudgetaryEntityId, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleBudgetaryEntityId, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleBudgetaryEntityId, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleBudgetaryEntityId, False)
        '
        'GridView1
        '
        Me.GridView1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView1.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView1.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.GroupRow.Options.UseFont = True
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn5, Me.GridColumn9})
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.GridView1, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView1, False)
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Código"
        Me.GridColumn5.FieldName = "Code"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.AllowEdit = False
        Me.GridColumn5.OptionsColumn.AllowFocus = False
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 0
        Me.GridColumn5.Width = 213
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Nombre"
        Me.GridColumn9.FieldName = "Name"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.OptionsColumn.AllowEdit = False
        Me.GridColumn9.OptionsColumn.AllowFocus = False
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 1
        Me.GridColumn9.Width = 1169
        '
        'INDBtnImportFile
        '
        Me.INDBtnImportFile.ImageOptions.Image = CType(resources.GetObject("INDBtnImportFile.ImageOptions.Image"), System.Drawing.Image)
        Me.INDBtnImportFile.Location = New System.Drawing.Point(821, 53)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDBtnImportFile, False)
        Me.INDBtnImportFile.Name = "INDBtnImportFile"
        Me.INDBtnImportFile.Size = New System.Drawing.Size(34, 32)
        Me.INDBtnImportFile.StyleController = Me.INDlyPurchaseOrder
        Me.INDBtnImportFile.TabIndex = 35
        Me.INDBtnImportFile.ToolTip = "Importar Archivo"
        '
        'INDEsbDetails
        '
        Me.INDEsbDetails.ImageOptions.Image = CType(resources.GetObject("INDEsbDetails.ImageOptions.Image"), System.Drawing.Image)
        Me.INDEsbDetails.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.INDEsbDetails.Location = New System.Drawing.Point(783, 53)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDEsbDetails, False)
        Me.INDEsbDetails.Name = "INDEsbDetails"
        Me.INDEsbDetails.Size = New System.Drawing.Size(34, 32)
        Me.INDEsbDetails.StyleController = Me.INDlyPurchaseOrder
        Me.INDEsbDetails.TabIndex = 34
        Me.INDEsbDetails.Text = "ExportStructureButton3"
        Me.INDEsbDetails.ToolTip = "Exportar Estructura"
        '
        'INDCmbContractBased
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDCmbContractBased, False)
        Me.IndigoComboBoxEdit1.SetCampoObligatorio(Me.INDCmbContractBased, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDCmbContractBased, False)
        Me.INDCmbContractBased.EnterMoveNextControl = True
        Me.INDCmbContractBased.Location = New System.Drawing.Point(-797, 207)
        Me.IndigoTextEdit1.SetMascara(Me.INDCmbContractBased, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDCmbContractBased.Name = "INDCmbContractBased"
        Me.INDCmbContractBased.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDCmbContractBased.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDCmbContractBased.Properties.Appearance.Options.UseBackColor = True
        Me.INDCmbContractBased.Properties.Appearance.Options.UseFont = True
        Me.INDCmbContractBased.Properties.AppearanceDropDown.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDCmbContractBased.Properties.AppearanceDropDown.Options.UseFont = True
        Me.INDCmbContractBased.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDCmbContractBased.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDCmbContractBased.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDCmbContractBased.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDCmbContractBased.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDCmbContractBased.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDCmbContractBased.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDCmbContractBased.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("No", False, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Si", True, -1)})
        Me.INDCmbContractBased.Size = New System.Drawing.Size(386, 28)
        Me.INDCmbContractBased.StyleController = Me.INDlyPurchaseOrder
        Me.INDCmbContractBased.TabIndex = 33
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDCmbContractBased, 0)
        '
        'INDSeLookEdContract
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSeLookEdContract, AppearanceObject5)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSeLookEdContract, AppearanceObject6)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSeLookEdContract, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSeLookEdContract, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSeLookEdContract, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSeLookEdContract, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSeLookEdContract, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSeLookEdContract, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSeLookEdContract, False)
        Me.INDSeLookEdContract.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSeLookEdContract, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSeLookEdContract, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSeLookEdContract, False)
        Me.INDSeLookEdContract.Location = New System.Drawing.Point(-797, 271)
        Me.IndigoTextEdit1.SetMascara(Me.INDSeLookEdContract, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSeLookEdContract.Name = "INDSeLookEdContract"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSeLookEdContract, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSeLookEdContract, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSeLookEdContract, False)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDSeLookEdContract, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSeLookEdContract, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSeLookEdContract, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSeLookEdContract, False)
        Me.INDSeLookEdContract.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSeLookEdContract.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSeLookEdContract.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSeLookEdContract.Properties.Appearance.Options.UseBackColor = True
        Me.INDSeLookEdContract.Properties.Appearance.Options.UseFont = True
        Me.INDSeLookEdContract.Properties.Appearance.Options.UseForeColor = True
        Me.INDSeLookEdContract.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSeLookEdContract.Properties.DisplayMember = "ContractNumber"
        Me.INDSeLookEdContract.Properties.NullText = ""
        Me.INDSeLookEdContract.Properties.PopupSizeable = False
        Me.INDSeLookEdContract.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDSeLookEdContract.Properties.ShowFooter = False
        Me.INDSeLookEdContract.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSeLookEdContract, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSeLookEdContract, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSeLookEdContract, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSeLookEdContract, True)
        Me.INDSeLookEdContract.Size = New System.Drawing.Size(386, 28)
        Me.INDSeLookEdContract.StyleController = Me.INDlyPurchaseOrder
        Me.INDSeLookEdContract.TabIndex = 32
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSeLookEdContract, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSeLookEdContract, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSeLookEdContract, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSeLookEdContract, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSeLookEdContract, False)
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseForeColor = True
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColCodeContractInventory, Me.INDColCodeNameContractTypeInventory, Me.INDColContractNumberInventory})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        '
        'INDColCodeContractInventory
        '
        Me.INDColCodeContractInventory.Caption = "Código"
        Me.INDColCodeContractInventory.FieldName = "Code"
        Me.INDColCodeContractInventory.Name = "INDColCodeContractInventory"
        Me.INDColCodeContractInventory.Visible = True
        Me.INDColCodeContractInventory.VisibleIndex = 0
        '
        'INDColCodeNameContractTypeInventory
        '
        Me.INDColCodeNameContractTypeInventory.Caption = "Tipo de Contrato"
        Me.INDColCodeNameContractTypeInventory.FieldName = "ContractTypeId.CodeName"
        Me.INDColCodeNameContractTypeInventory.Name = "INDColCodeNameContractTypeInventory"
        Me.INDColCodeNameContractTypeInventory.Visible = True
        Me.INDColCodeNameContractTypeInventory.VisibleIndex = 1
        '
        'INDColContractNumberInventory
        '
        Me.INDColContractNumberInventory.Caption = "Número Contrato"
        Me.INDColContractNumberInventory.FieldName = "ContractNumber"
        Me.INDColContractNumberInventory.Name = "INDColContractNumberInventory"
        Me.INDColContractNumberInventory.Visible = True
        Me.INDColContractNumberInventory.VisibleIndex = 2
        '
        'INDCmbOrderType
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDCmbOrderType, False)
        Me.IndigoComboBoxEdit1.SetCampoObligatorio(Me.INDCmbOrderType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDCmbOrderType, True)
        Me.INDCmbOrderType.EnterMoveNextControl = True
        Me.INDCmbOrderType.Location = New System.Drawing.Point(-797, 399)
        Me.IndigoTextEdit1.SetMascara(Me.INDCmbOrderType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDCmbOrderType.Name = "INDCmbOrderType"
        Me.INDCmbOrderType.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDCmbOrderType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDCmbOrderType.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDCmbOrderType.Properties.Appearance.Options.UseBackColor = True
        Me.INDCmbOrderType.Properties.Appearance.Options.UseFont = True
        Me.INDCmbOrderType.Properties.Appearance.Options.UseForeColor = True
        Me.INDCmbOrderType.Properties.AppearanceDropDown.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDCmbOrderType.Properties.AppearanceDropDown.Options.UseFont = True
        Me.INDCmbOrderType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDCmbOrderType.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDCmbOrderType.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDCmbOrderType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDCmbOrderType.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDCmbOrderType.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDCmbOrderType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDCmbOrderType.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Productos", 1, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Servicios", 2, -1)})
        Me.INDCmbOrderType.Size = New System.Drawing.Size(386, 28)
        Me.INDCmbOrderType.StyleController = Me.INDlyPurchaseOrder
        Me.INDCmbOrderType.TabIndex = 31
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDCmbOrderType, 0)
        Me.INDCmbOrderType.ToolTip = "Este Campo es Necesario"
        '
        'INDbtnCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbtnCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbtnCode, True)
        Me.INDbtnCode.Location = New System.Drawing.Point(-797, 79)
        Me.IndigoTextEdit1.SetMascara(Me.INDbtnCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDbtnCode.Name = "INDbtnCode"
        Me.INDbtnCode.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDbtnCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDbtnCode.Properties.Appearance.Options.UseFont = True
        EditorButtonImageOptions1.Image = Global.Presentation.Inventory.My.Resources.Resources.BuscarMetro
        Me.INDbtnCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDbtnCode.Properties.MaxLength = 20
        Me.INDbtnCode.Size = New System.Drawing.Size(386, 28)
        Me.INDbtnCode.StyleController = Me.INDlyPurchaseOrder
        Me.INDbtnCode.TabIndex = 30
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbtnCode, 0)
        Me.INDbtnCode.ToolTip = "Este Campo es Necesario"
        '
        'INDGcProducts
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcProducts, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcProducts, Nothing)
        Me.INDGcProducts.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcProducts, True)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcProducts, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcProducts, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcProducts, False)
        Me.INDGcProducts.Location = New System.Drawing.Point(31, 89)
        Me.INDGcProducts.MainView = Me.INDGvProducts
        Me.INDGcProducts.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDGcProducts.Name = "INDGcProducts"
        Me.INDGcProducts.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDRepTexQuantityOrValues, Me.INDRepPercent})
        Me.INDGcProducts.Size = New System.Drawing.Size(824, 600)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcProducts, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDGcProducts.TabIndex = 29
        Me.INDGcProducts.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvProducts})
        '
        'INDGvProducts
        '
        Me.INDGvProducts.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvProducts.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvProducts.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDGvProducts.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvProducts.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvProducts.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGvProducts.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvProducts.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvProducts.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvProducts.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvProducts.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvProducts.Appearance.Row.Options.UseFont = True
        Me.INDGvProducts.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvProducts.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvProducts.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.Quantity, Me.OutstandingQuantity, Me.INDColDiscountPercent, Me.INDColIVA, Me.INDColTotalIva, Me.INDColUnitValue, Me.INDColTotalValue, Me.GridColumn14, Me.GridColumn15, Me.GridColumn16})
        Me.INDGvProducts.GridControl = Me.INDGcProducts
        Me.INDGvProducts.Name = "INDGvProducts"
        Me.INDGvProducts.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvProducts.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvProducts.OptionsView.ShowAutoFilterRow = True
        Me.INDGvProducts.OptionsView.ShowDetailButtons = False
        Me.INDGvProducts.OptionsView.ShowFooter = True
        Me.INDGvProducts.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDGvProducts, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvProducts, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Código"
        Me.GridColumn1.FieldName = "ProductCode"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 89
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Producto"
        Me.GridColumn2.FieldName = "ProductName"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 302
        '
        'Quantity
        '
        Me.Quantity.Caption = "Unidades"
        Me.Quantity.FieldName = "Quantity"
        Me.Quantity.Name = "Quantity"
        Me.Quantity.OptionsColumn.AllowEdit = False
        Me.Quantity.OptionsColumn.AllowFocus = False
        Me.Quantity.Visible = True
        Me.Quantity.VisibleIndex = 2
        Me.Quantity.Width = 78
        '
        'OutstandingQuantity
        '
        Me.OutstandingQuantity.Caption = "Unidades"
        Me.OutstandingQuantity.FieldName = "OutstandingQuantity"
        Me.OutstandingQuantity.Name = "OutstandingQuantity"
        Me.OutstandingQuantity.Width = 64
        '
        'INDColDiscountPercent
        '
        Me.INDColDiscountPercent.Caption = "% Descuento"
        Me.INDColDiscountPercent.ColumnEdit = Me.INDRepPercent
        Me.INDColDiscountPercent.DisplayFormat.FormatString = "n2"
        Me.INDColDiscountPercent.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColDiscountPercent.FieldName = "DiscountPercentage"
        Me.INDColDiscountPercent.Name = "INDColDiscountPercent"
        Me.INDColDiscountPercent.OptionsColumn.AllowEdit = False
        Me.INDColDiscountPercent.OptionsColumn.AllowFocus = False
        Me.INDColDiscountPercent.Visible = True
        Me.INDColDiscountPercent.VisibleIndex = 3
        Me.INDColDiscountPercent.Width = 113
        '
        'INDRepPercent
        '
        Me.INDRepPercent.AutoHeight = False
        Me.INDRepPercent.Name = "INDRepPercent"
        '
        'INDColIVA
        '
        Me.INDColIVA.Caption = "% IVA"
        Me.INDColIVA.DisplayFormat.FormatString = "n2"
        Me.INDColIVA.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColIVA.FieldName = "IvaPercentage"
        Me.INDColIVA.Name = "INDColIVA"
        Me.INDColIVA.OptionsColumn.AllowEdit = False
        Me.INDColIVA.OptionsColumn.AllowFocus = False
        Me.INDColIVA.Visible = True
        Me.INDColIVA.VisibleIndex = 4
        Me.INDColIVA.Width = 59
        '
        'INDColTotalIva
        '
        Me.INDColTotalIva.Caption = "IVA Total"
        Me.INDColTotalIva.DisplayFormat.FormatString = "c2"
        Me.INDColTotalIva.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColTotalIva.FieldName = "TotalIva"
        Me.INDColTotalIva.Name = "INDColTotalIva"
        Me.INDColTotalIva.Visible = True
        Me.INDColTotalIva.VisibleIndex = 5
        '
        'INDColUnitValue
        '
        Me.INDColUnitValue.Caption = "V. Unitario"
        Me.INDColUnitValue.ColumnEdit = Me.INDRepTexQuantityOrValues
        Me.INDColUnitValue.DisplayFormat.FormatString = "c2"
        Me.INDColUnitValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColUnitValue.FieldName = "Value"
        Me.INDColUnitValue.Name = "INDColUnitValue"
        Me.INDColUnitValue.OptionsColumn.AllowEdit = False
        Me.INDColUnitValue.OptionsColumn.AllowFocus = False
        Me.INDColUnitValue.Visible = True
        Me.INDColUnitValue.VisibleIndex = 6
        Me.INDColUnitValue.Width = 96
        '
        'INDRepTexQuantityOrValues
        '
        Me.INDRepTexQuantityOrValues.AutoHeight = False
        Me.INDRepTexQuantityOrValues.DisplayFormat.FormatString = "C2"
        Me.INDRepTexQuantityOrValues.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDRepTexQuantityOrValues.Name = "INDRepTexQuantityOrValues"
        '
        'INDColTotalValue
        '
        Me.INDColTotalValue.Caption = "V. Total"
        Me.INDColTotalValue.ColumnEdit = Me.INDRepTexQuantityOrValues
        Me.INDColTotalValue.DisplayFormat.FormatString = "c2"
        Me.INDColTotalValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColTotalValue.FieldName = "TotalValue"
        Me.INDColTotalValue.Name = "INDColTotalValue"
        Me.INDColTotalValue.OptionsColumn.AllowEdit = False
        Me.INDColTotalValue.OptionsColumn.AllowFocus = False
        Me.INDColTotalValue.Visible = True
        Me.INDColTotalValue.VisibleIndex = 7
        Me.INDColTotalValue.Width = 90
        '
        'GridColumn14
        '
        Me.GridColumn14.Caption = "Fabricante"
        Me.GridColumn14.FieldName = "ManufacturerName"
        Me.GridColumn14.Name = "GridColumn14"
        Me.GridColumn14.OptionsColumn.AllowEdit = False
        Me.GridColumn14.OptionsColumn.AllowFocus = False
        '
        'GridColumn15
        '
        Me.GridColumn15.Caption = "Registro Sanitario"
        Me.GridColumn15.FieldName = "HealthRegistration"
        Me.GridColumn15.Name = "GridColumn15"
        Me.GridColumn15.OptionsColumn.AllowEdit = False
        Me.GridColumn15.OptionsColumn.AllowFocus = False
        '
        'GridColumn16
        '
        Me.GridColumn16.Caption = "Presentación"
        Me.GridColumn16.FieldName = "Presentation"
        Me.GridColumn16.Name = "GridColumn16"
        Me.GridColumn16.OptionsColumn.AllowEdit = False
        Me.GridColumn16.OptionsColumn.AllowFocus = False
        '
        'INDBtnAddProducts
        '
        Me.INDBtnAddProducts.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDBtnAddProducts.Appearance.Options.UseFont = True
        Me.INDBtnAddProducts.Location = New System.Drawing.Point(31, 53)
        Me.INDBtnAddProducts.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDBtnAddProducts, False)
        Me.INDBtnAddProducts.Name = "INDBtnAddProducts"
        Me.INDBtnAddProducts.Size = New System.Drawing.Size(748, 32)
        Me.INDBtnAddProducts.StyleController = Me.INDlyPurchaseOrder
        Me.INDBtnAddProducts.TabIndex = 28
        Me.INDBtnAddProducts.Text = "Agregar Productos"
        '
        'INDdeDateDelivery
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdeDateDelivery, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDdeDateDelivery, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdeDateDelivery, True)
        Me.INDdeDateDelivery.EditValue = Nothing
        Me.INDdeDateDelivery.EnterMoveNextControl = True
        Me.INDdeDateDelivery.Location = New System.Drawing.Point(-383, 207)
        Me.IndigoTextEdit1.SetMascara(Me.INDdeDateDelivery, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDdeDateDelivery, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDdeDateDelivery.Name = "INDdeDateDelivery"
        Me.INDdeDateDelivery.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDdeDateDelivery.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdeDateDelivery.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDdeDateDelivery.Properties.Appearance.Options.UseBackColor = True
        Me.INDdeDateDelivery.Properties.Appearance.Options.UseFont = True
        Me.INDdeDateDelivery.Properties.Appearance.Options.UseForeColor = True
        Me.INDdeDateDelivery.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDdeDateDelivery.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDdeDateDelivery.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdeDateDelivery.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDdeDateDelivery.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDdeDateDelivery.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdeDateDelivery.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeDateDelivery.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeDateDelivery.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.INDdeDateDelivery.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDdeDateDelivery.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdeDateDelivery.Size = New System.Drawing.Size(386, 28)
        Me.INDdeDateDelivery.StyleController = Me.INDlyPurchaseOrder
        Me.INDdeDateDelivery.TabIndex = 27
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdeDateDelivery, 0)
        Me.INDdeDateDelivery.ToolTip = "Este Campo es Necesario"
        '
        'INDteDeliveryPlace
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDteDeliveryPlace, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDteDeliveryPlace, False)
        Me.INDteDeliveryPlace.EnterMoveNextControl = True
        Me.INDteDeliveryPlace.Location = New System.Drawing.Point(-383, 399)
        Me.IndigoTextEdit1.SetMascara(Me.INDteDeliveryPlace, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDteDeliveryPlace.Name = "INDteDeliveryPlace"
        Me.INDteDeliveryPlace.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDteDeliveryPlace.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteDeliveryPlace.Properties.Appearance.Options.UseBackColor = True
        Me.INDteDeliveryPlace.Properties.Appearance.Options.UseFont = True
        Me.INDteDeliveryPlace.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDteDeliveryPlace.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDteDeliveryPlace.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteDeliveryPlace.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDteDeliveryPlace.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDteDeliveryPlace.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDteDeliveryPlace.Properties.MaxLength = 200
        Me.INDteDeliveryPlace.Size = New System.Drawing.Size(386, 28)
        Me.INDteDeliveryPlace.StyleController = Me.INDlyPurchaseOrder
        Me.INDteDeliveryPlace.TabIndex = 14
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDteDeliveryPlace, 0)
        '
        'INDteDeliveryForm
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDteDeliveryForm, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDteDeliveryForm, False)
        Me.INDteDeliveryForm.EnterMoveNextControl = True
        Me.INDteDeliveryForm.Location = New System.Drawing.Point(-383, 271)
        Me.IndigoTextEdit1.SetMascara(Me.INDteDeliveryForm, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDteDeliveryForm.Name = "INDteDeliveryForm"
        Me.INDteDeliveryForm.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDteDeliveryForm.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteDeliveryForm.Properties.Appearance.Options.UseBackColor = True
        Me.INDteDeliveryForm.Properties.Appearance.Options.UseFont = True
        Me.INDteDeliveryForm.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDteDeliveryForm.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDteDeliveryForm.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteDeliveryForm.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDteDeliveryForm.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDteDeliveryForm.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDteDeliveryForm.Properties.MaxLength = 200
        Me.INDteDeliveryForm.Size = New System.Drawing.Size(386, 28)
        Me.INDteDeliveryForm.StyleController = Me.INDlyPurchaseOrder
        Me.INDteDeliveryForm.TabIndex = 13
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDteDeliveryForm, 0)
        '
        'INDsleStock
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleStock, AppearanceObject7)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleStock, AppearanceObject8)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleStock, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleStock, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleStock, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleStock, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleStock, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleStock, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleStock, False)
        Me.INDsleStock.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleStock, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleStock, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleStock, False)
        Me.INDsleStock.Location = New System.Drawing.Point(-383, 79)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleStock, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleStock.Name = "INDsleStock"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleStock, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleStock, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleStock, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDsleStock, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleStock, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleStock, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleStock, False)
        Me.INDsleStock.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDsleStock.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleStock.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleStock.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleStock.Properties.Appearance.Options.UseFont = True
        Me.INDsleStock.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleStock.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleStock.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleStock.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleStock.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleStock.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleStock.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleStock.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleStock.Properties.DisplayMember = "CodeName"
        Me.INDsleStock.Properties.NullText = ""
        Me.INDsleStock.Properties.PopupSizeable = False
        Me.INDsleStock.Properties.PopupView = Me.INDGdvStock
        Me.INDsleStock.Properties.ShowFooter = False
        Me.INDsleStock.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleStock, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleStock, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleStock, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleStock, True)
        Me.INDsleStock.Size = New System.Drawing.Size(386, 28)
        Me.INDsleStock.StyleController = Me.INDlyPurchaseOrder
        Me.INDsleStock.TabIndex = 8
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleStock, "302")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleStock, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleStock, "{0} - {1}")
        Me.INDsleStock.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleStock, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleStock, False)
        '
        'INDGdvStock
        '
        Me.INDGdvStock.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGdvStock.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGdvStock.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGdvStock.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGdvStock.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGdvStock.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGdvStock.Appearance.GroupRow.Options.UseFont = True
        Me.INDGdvStock.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGdvStock.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGdvStock.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGdvStock.Appearance.Row.Options.UseFont = True
        Me.INDGdvStock.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColWarehouseCode, Me.INDColWarehouseName})
        Me.INDGdvStock.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGdvStock.Name = "INDGdvStock"
        Me.INDGdvStock.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGdvStock.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGdvStock.OptionsView.EnableAppearanceOddRow = True
        Me.INDGdvStock.OptionsView.ShowAutoFilterRow = True
        Me.INDGdvStock.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDGdvStock, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGdvStock, False)
        '
        'INDColWarehouseCode
        '
        Me.INDColWarehouseCode.Caption = "Código"
        Me.INDColWarehouseCode.FieldName = "Code"
        Me.INDColWarehouseCode.Name = "INDColWarehouseCode"
        Me.INDColWarehouseCode.Visible = True
        Me.INDColWarehouseCode.VisibleIndex = 0
        Me.INDColWarehouseCode.Width = 129
        '
        'INDColWarehouseName
        '
        Me.INDColWarehouseName.Caption = "Nombre"
        Me.INDColWarehouseName.FieldName = "Name"
        Me.INDColWarehouseName.Name = "INDColWarehouseName"
        Me.INDColWarehouseName.Visible = True
        Me.INDColWarehouseName.VisibleIndex = 1
        Me.INDColWarehouseName.Width = 255
        '
        'INDdeDateDocument
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdeDateDocument, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDdeDateDocument, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdeDateDocument, True)
        Me.INDdeDateDocument.EditValue = Nothing
        Me.INDdeDateDocument.EnterMoveNextControl = True
        Me.INDdeDateDocument.Location = New System.Drawing.Point(-797, 143)
        Me.IndigoTextEdit1.SetMascara(Me.INDdeDateDocument, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDdeDateDocument, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDdeDateDocument.Name = "INDdeDateDocument"
        Me.INDdeDateDocument.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDdeDateDocument.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdeDateDocument.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDdeDateDocument.Properties.Appearance.Options.UseBackColor = True
        Me.INDdeDateDocument.Properties.Appearance.Options.UseFont = True
        Me.INDdeDateDocument.Properties.Appearance.Options.UseForeColor = True
        Me.INDdeDateDocument.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDdeDateDocument.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDdeDateDocument.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdeDateDocument.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDdeDateDocument.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDdeDateDocument.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdeDateDocument.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeDateDocument.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeDateDocument.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.INDdeDateDocument.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDdeDateDocument.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdeDateDocument.Size = New System.Drawing.Size(386, 28)
        Me.INDdeDateDocument.StyleController = Me.INDlyPurchaseOrder
        Me.INDdeDateDocument.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdeDateDocument, 0)
        Me.INDdeDateDocument.ToolTip = "Este Campo es Necesario"
        '
        'INDsleSupplier
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleSupplier, AppearanceObject9)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleSupplier, AppearanceObject10)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleSupplier, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleSupplier, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleSupplier, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleSupplier, False)
        Me.INDsleSupplier.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleSupplier, False)
        Me.INDsleSupplier.Location = New System.Drawing.Point(-797, 335)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleSupplier, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleSupplier.Name = "INDsleSupplier"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleSupplier, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDsleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleSupplier, False)
        Me.INDsleSupplier.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDsleSupplier.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleSupplier.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleSupplier.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleSupplier.Properties.Appearance.Options.UseFont = True
        Me.INDsleSupplier.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleSupplier.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleSupplier.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleSupplier.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleSupplier.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleSupplier.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleSupplier.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleSupplier.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleSupplier.Properties.DisplayMember = "DisplaySupplier"
        Me.INDsleSupplier.Properties.NullText = ""
        Me.INDsleSupplier.Properties.PopupFormMinSize = New System.Drawing.Size(600, 0)
        Me.INDsleSupplier.Properties.PopupSizeable = False
        Me.INDsleSupplier.Properties.PopupView = Me.viewSupplier
        Me.INDsleSupplier.Properties.ShowFooter = False
        Me.INDsleSupplier.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleSupplier, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleSupplier, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleSupplier, True)
        Me.INDsleSupplier.Size = New System.Drawing.Size(386, 28)
        Me.INDsleSupplier.StyleController = Me.INDlyPurchaseOrder
        Me.INDsleSupplier.TabIndex = 7
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleSupplier, "558")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleSupplier, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleSupplier, "{0} - {1}")
        Me.INDsleSupplier.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleSupplier, False)
        '
        'viewSupplier
        '
        Me.viewSupplier.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.viewSupplier.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.viewSupplier.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.viewSupplier.Appearance.FocusedRow.Options.UseFont = True
        Me.viewSupplier.Appearance.FocusedRow.Options.UseForeColor = True
        Me.viewSupplier.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viewSupplier.Appearance.GroupRow.Options.UseFont = True
        Me.viewSupplier.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viewSupplier.Appearance.HeaderPanel.Options.UseFont = True
        Me.viewSupplier.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.viewSupplier.Appearance.Row.Options.UseFont = True
        Me.viewSupplier.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.IndColNitSupplier, Me.INDColSupplierName, Me.INDColDistributionName})
        Me.viewSupplier.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.viewSupplier.GroupCount = 1
        Me.viewSupplier.Name = "viewSupplier"
        Me.viewSupplier.OptionsBehavior.AutoExpandAllGroups = True
        Me.viewSupplier.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.viewSupplier.OptionsView.EnableAppearanceEvenRow = True
        Me.viewSupplier.OptionsView.EnableAppearanceOddRow = True
        Me.viewSupplier.OptionsView.ShowAutoFilterRow = True
        Me.viewSupplier.OptionsView.ShowGroupPanel = False
        Me.viewSupplier.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.INDColDistributionName, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.viewSupplier, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.viewSupplier, False)
        '
        'IndColNitSupplier
        '
        Me.IndColNitSupplier.Caption = "Nit"
        Me.IndColNitSupplier.FieldName = "IdSupplier.IdThirdParty.Nit"
        Me.IndColNitSupplier.Name = "IndColNitSupplier"
        Me.IndColNitSupplier.Visible = True
        Me.IndColNitSupplier.VisibleIndex = 0
        Me.IndColNitSupplier.Width = 129
        '
        'INDColSupplierName
        '
        Me.INDColSupplierName.Caption = "Proveedor"
        Me.INDColSupplierName.FieldName = "IdSupplier.CodeName"
        Me.INDColSupplierName.Name = "INDColSupplierName"
        Me.INDColSupplierName.Visible = True
        Me.INDColSupplierName.VisibleIndex = 1
        Me.INDColSupplierName.Width = 235
        '
        'INDColDistributionName
        '
        Me.INDColDistributionName.Caption = "Línea de Distribución"
        Me.INDColDistributionName.FieldName = "IdDistributionLine.CodeName"
        Me.INDColDistributionName.Name = "INDColDistributionName"
        Me.INDColDistributionName.Visible = True
        Me.INDColDistributionName.VisibleIndex = 2
        Me.INDColDistributionName.Width = 623
        '
        'INDmeDetail
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDmeDetail, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDmeDetail, True)
        Me.INDmeDetail.EnterMoveNextControl = True
        Me.INDmeDetail.Location = New System.Drawing.Point(-797, 527)
        Me.IndigoTextEdit1.SetMascara(Me.INDmeDetail, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDmeDetail.Name = "INDmeDetail"
        Me.INDmeDetail.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDmeDetail.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmeDetail.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDmeDetail.Properties.Appearance.Options.UseBackColor = True
        Me.INDmeDetail.Properties.Appearance.Options.UseFont = True
        Me.INDmeDetail.Properties.Appearance.Options.UseForeColor = True
        Me.INDmeDetail.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDmeDetail.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDmeDetail.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmeDetail.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDmeDetail.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDmeDetail.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDmeDetail.Properties.MaxLength = 300
        Me.INDmeDetail.Size = New System.Drawing.Size(386, 130)
        Me.INDmeDetail.StyleController = Me.INDlyPurchaseOrder
        Me.INDmeDetail.TabIndex = 11
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDmeDetail, 0)
        Me.INDmeDetail.ToolTip = "Este Campo es Necesario"
        '
        'INDgcAvailability
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcAvailability, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcAvailability, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcAvailability, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcAvailability, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcAvailability, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcAvailability, False)
        Me.INDgcAvailability.Location = New System.Drawing.Point(883, 161)
        Me.INDgcAvailability.MainView = Me.INDviewAvailability
        Me.INDgcAvailability.Name = "INDgcAvailability"
        Me.INDgcAvailability.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrepTxtValue})
        Me.INDgcAvailability.Size = New System.Drawing.Size(824, 528)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcAvailability, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcAvailability.TabIndex = 32
        Me.INDgcAvailability.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDviewAvailability})
        '
        'INDviewAvailability
        '
        Me.INDviewAvailability.Appearance.Empty.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDviewAvailability.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewAvailability.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewAvailability.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewAvailability.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewAvailability.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewAvailability.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewAvailability.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewAvailability.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewAvailability.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewAvailability.Appearance.Row.Options.UseFont = True
        Me.INDviewAvailability.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDviewAvailability.Appearance.ViewCaption.Options.UseFont = True
        Me.INDviewAvailability.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn154, Me.GridColumn155, Me.GridColumn156, Me.GridColumn158, Me.GridColumn159, Me.GridColumn160})
        Me.INDviewAvailability.GridControl = Me.INDgcAvailability
        Me.INDviewAvailability.Name = "INDviewAvailability"
        Me.INDviewAvailability.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewAvailability.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewAvailability.OptionsView.ShowAutoFilterRow = True
        Me.INDviewAvailability.OptionsView.ShowDetailButtons = False
        Me.INDviewAvailability.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDviewAvailability, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewAvailability, False)
        '
        'GridColumn154
        '
        Me.GridColumn154.Caption = "Código"
        Me.GridColumn154.FieldName = "AvailabilityCode"
        Me.GridColumn154.Name = "GridColumn154"
        Me.GridColumn154.OptionsColumn.AllowEdit = False
        Me.GridColumn154.OptionsColumn.AllowFocus = False
        Me.GridColumn154.Visible = True
        Me.GridColumn154.VisibleIndex = 0
        '
        'GridColumn155
        '
        Me.GridColumn155.Caption = "Rubro"
        Me.GridColumn155.FieldName = "CategoryCodeName"
        Me.GridColumn155.Name = "GridColumn155"
        Me.GridColumn155.OptionsColumn.AllowEdit = False
        Me.GridColumn155.OptionsColumn.AllowFocus = False
        Me.GridColumn155.Visible = True
        Me.GridColumn155.VisibleIndex = 1
        '
        'GridColumn156
        '
        Me.GridColumn156.Caption = "Recurso"
        Me.GridColumn156.FieldName = "FinancialSourceCodeName"
        Me.GridColumn156.Name = "GridColumn156"
        Me.GridColumn156.OptionsColumn.AllowEdit = False
        Me.GridColumn156.OptionsColumn.AllowFocus = False
        Me.GridColumn156.Visible = True
        Me.GridColumn156.VisibleIndex = 2
        '
        'GridColumn158
        '
        Me.GridColumn158.Caption = "Tipo"
        Me.GridColumn158.FieldName = "RevenueTypeCodeName"
        Me.GridColumn158.Name = "GridColumn158"
        Me.GridColumn158.OptionsColumn.AllowEdit = False
        Me.GridColumn158.OptionsColumn.AllowFocus = False
        Me.GridColumn158.Visible = True
        Me.GridColumn158.VisibleIndex = 3
        '
        'GridColumn159
        '
        Me.GridColumn159.Caption = "Saldo"
        Me.GridColumn159.DisplayFormat.FormatString = "c0"
        Me.GridColumn159.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn159.FieldName = "Balance"
        Me.GridColumn159.Name = "GridColumn159"
        Me.GridColumn159.OptionsColumn.AllowEdit = False
        Me.GridColumn159.OptionsColumn.AllowFocus = False
        Me.GridColumn159.Visible = True
        Me.GridColumn159.VisibleIndex = 4
        '
        'GridColumn160
        '
        Me.GridColumn160.Caption = "Valor a Ejecutar"
        Me.GridColumn160.ColumnEdit = Me.INDrepTxtValue
        Me.GridColumn160.FieldName = "Value"
        Me.GridColumn160.Name = "GridColumn160"
        Me.GridColumn160.Visible = True
        Me.GridColumn160.VisibleIndex = 5
        '
        'INDrepTxtValue
        '
        Me.INDrepTxtValue.AutoHeight = False
        Me.INDrepTxtValue.Mask.EditMask = "C0"
        Me.INDrepTxtValue.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDrepTxtValue.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepTxtValue.Name = "INDrepTxtValue"
        '
        'INDsleAvailability
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleAvailability, AppearanceObject11)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleAvailability, AppearanceObject12)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleAvailability, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleAvailability, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleAvailability, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleAvailability, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleAvailability, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleAvailability, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleAvailability, False)
        Me.INDsleAvailability.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleAvailability, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleAvailability, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleAvailability, False)
        Me.INDsleAvailability.Location = New System.Drawing.Point(1068, 125)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleAvailability, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleAvailability.Name = "INDsleAvailability"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleAvailability, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleAvailability, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleAvailability, False)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDsleAvailability, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleAvailability, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleAvailability, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleAvailability, False)
        Me.INDsleAvailability.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleAvailability.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleAvailability.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleAvailability.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleAvailability.Properties.Appearance.Options.UseFont = True
        Me.INDsleAvailability.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleAvailability.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleAvailability.Properties.DisplayMember = "Description"
        Me.INDsleAvailability.Properties.NullText = ""
        Me.INDsleAvailability.Properties.PopupFormMinSize = New System.Drawing.Size(600, 0)
        Me.INDsleAvailability.Properties.PopupFormSize = New System.Drawing.Size(600, 0)
        Me.INDsleAvailability.Properties.PopupSizeable = False
        Me.INDsleAvailability.Properties.PopupView = Me.INDviewSearchAvailability
        Me.INDsleAvailability.Properties.ShowFooter = False
        Me.INDsleAvailability.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleAvailability, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleAvailability, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleAvailability, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleAvailability, True)
        Me.INDsleAvailability.Size = New System.Drawing.Size(411, 28)
        Me.INDsleAvailability.StyleController = Me.INDlyPurchaseOrder
        Me.INDsleAvailability.TabIndex = 33
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleAvailability, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleAvailability, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleAvailability, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleAvailability, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleAvailability, False)
        '
        'INDviewSearchAvailability
        '
        Me.INDviewSearchAvailability.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewSearchAvailability.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewSearchAvailability.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewSearchAvailability.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewSearchAvailability.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewSearchAvailability.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewSearchAvailability.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewSearchAvailability.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewSearchAvailability.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewSearchAvailability.Appearance.Row.Options.UseFont = True
        Me.INDviewSearchAvailability.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn147, Me.GridColumn149, Me.GridColumn150, Me.GridColumn151, Me.GridColumn152})
        Me.INDviewSearchAvailability.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDviewSearchAvailability.Name = "INDviewSearchAvailability"
        Me.INDviewSearchAvailability.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDviewSearchAvailability.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewSearchAvailability.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewSearchAvailability.OptionsView.ShowAutoFilterRow = True
        Me.INDviewSearchAvailability.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDviewSearchAvailability, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewSearchAvailability, False)
        '
        'GridColumn147
        '
        Me.GridColumn147.Caption = "Código"
        Me.GridColumn147.FieldName = "AvailabilityCode"
        Me.GridColumn147.Name = "GridColumn147"
        Me.GridColumn147.Visible = True
        Me.GridColumn147.VisibleIndex = 0
        '
        'GridColumn149
        '
        Me.GridColumn149.Caption = "Rubro"
        Me.GridColumn149.FieldName = "CategoryCodeName"
        Me.GridColumn149.Name = "GridColumn149"
        Me.GridColumn149.Visible = True
        Me.GridColumn149.VisibleIndex = 1
        '
        'GridColumn150
        '
        Me.GridColumn150.Caption = "Recurso"
        Me.GridColumn150.FieldName = "FinancialSourceCodeName"
        Me.GridColumn150.Name = "GridColumn150"
        Me.GridColumn150.Visible = True
        Me.GridColumn150.VisibleIndex = 2
        '
        'GridColumn151
        '
        Me.GridColumn151.Caption = "Tipo"
        Me.GridColumn151.FieldName = "RevenueTypeCodeName"
        Me.GridColumn151.Name = "GridColumn151"
        Me.GridColumn151.Visible = True
        Me.GridColumn151.VisibleIndex = 3
        '
        'GridColumn152
        '
        Me.GridColumn152.Caption = "Saldo"
        Me.GridColumn152.DisplayFormat.FormatString = "C0"
        Me.GridColumn152.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn152.FieldName = "Balance"
        Me.GridColumn152.Name = "GridColumn152"
        Me.GridColumn152.Visible = True
        Me.GridColumn152.VisibleIndex = 4
        '
        'INDbtnAddAvailability
        '
        Me.INDbtnAddAvailability.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnAddAvailability.Appearance.Options.UseFont = True
        Me.INDbtnAddAvailability.Location = New System.Drawing.Point(1483, 125)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnAddAvailability, False)
        Me.INDbtnAddAvailability.Name = "INDbtnAddAvailability"
        Me.INDbtnAddAvailability.Size = New System.Drawing.Size(224, 28)
        Me.INDbtnAddAvailability.StyleController = Me.INDlyPurchaseOrder
        Me.INDbtnAddAvailability.TabIndex = 34
        Me.INDbtnAddAvailability.Text = "Agregar"
        '
        'INDtxtIvaValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtIvaValue, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtIvaValue, False)
        Me.INDtxtIvaValue.EnterMoveNextControl = True
        Me.INDtxtIvaValue.Location = New System.Drawing.Point(-383, 655)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtIvaValue, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtIvaValue.Name = "INDtxtIvaValue"
        Me.INDtxtIvaValue.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtIvaValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtIvaValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtIvaValue.Properties.Appearance.Options.UseFont = True
        Me.INDtxtIvaValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtIvaValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtIvaValue.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtIvaValue.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtIvaValue.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtIvaValue.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtIvaValue.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtIvaValue.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtIvaValue.Properties.Mask.EditMask = "c"
        Me.INDtxtIvaValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtIvaValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtIvaValue.Properties.MaxLength = 20
        Me.INDtxtIvaValue.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtIvaValue.StyleController = Me.INDlyPurchaseOrder
        Me.INDtxtIvaValue.TabIndex = 38
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtIvaValue, 0)
        '
        'INDtxtTotalValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtTotalValue, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtTotalValue, False)
        Me.INDtxtTotalValue.EnterMoveNextControl = True
        Me.INDtxtTotalValue.Location = New System.Drawing.Point(-383, 591)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtTotalValue, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtTotalValue.Name = "INDtxtTotalValue"
        Me.INDtxtTotalValue.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtTotalValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtTotalValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtTotalValue.Properties.Appearance.Options.UseFont = True
        Me.INDtxtTotalValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtTotalValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtTotalValue.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtTotalValue.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtTotalValue.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtTotalValue.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtTotalValue.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtTotalValue.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtTotalValue.Properties.Mask.EditMask = "c"
        Me.INDtxtTotalValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtTotalValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtTotalValue.Properties.MaxLength = 20
        Me.INDtxtTotalValue.Properties.ReadOnly = True
        Me.INDtxtTotalValue.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtTotalValue.StyleController = Me.INDlyPurchaseOrder
        Me.INDtxtTotalValue.TabIndex = 38
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtTotalValue, 0)
        '
        'INDsleCurrency
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleCurrency, AppearanceObject13)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleCurrency, AppearanceObject14)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleCurrency, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleCurrency, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleCurrency, False)
        Me.INDsleCurrency.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleCurrency, False)
        Me.INDsleCurrency.Location = New System.Drawing.Point(-797, 463)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleCurrency, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleCurrency.Name = "INDsleCurrency"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleCurrency, False)
        Me.INDsleCurrency.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleCurrency.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleCurrency.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleCurrency.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleCurrency.Properties.Appearance.Options.UseFont = True
        Me.INDsleCurrency.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleCurrency.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleCurrency.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleCurrency.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleCurrency.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleCurrency.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleCurrency.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleCurrency.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleCurrency.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDsleCurrency.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleCurrency.Properties.DisplayMember = "Abbreviation"
        Me.INDsleCurrency.Properties.NullText = ""
        Me.INDsleCurrency.Properties.PopupFormMinSize = New System.Drawing.Size(600, 0)
        Me.INDsleCurrency.Properties.PopupSizeable = False
        Me.INDsleCurrency.Properties.PopupView = Me.INDGvCurrencyAdvance
        Me.INDsleCurrency.Properties.ShowFooter = False
        Me.INDsleCurrency.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleCurrency, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleCurrency, True)
        Me.INDsleCurrency.Size = New System.Drawing.Size(386, 28)
        Me.INDsleCurrency.StyleController = Me.INDlyPurchaseOrder
        Me.INDsleCurrency.TabIndex = 0
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleCurrency, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleCurrency, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleCurrency, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleCurrency, False)
        '
        'INDGvCurrencyAdvance
        '
        Me.INDGvCurrencyAdvance.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvCurrencyAdvance.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvCurrencyAdvance.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvCurrencyAdvance.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvCurrencyAdvance.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGvCurrencyAdvance.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvCurrencyAdvance.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvCurrencyAdvance.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvCurrencyAdvance.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvCurrencyAdvance.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvCurrencyAdvance.Appearance.Row.Options.UseFont = True
        Me.INDGvCurrencyAdvance.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1179, Me.GridColumn1180, Me.GridColumn1181})
        Me.INDGvCurrencyAdvance.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvCurrencyAdvance.Name = "INDGvCurrencyAdvance"
        Me.INDGvCurrencyAdvance.OptionsFind.FindFilterColumns = ""
        Me.INDGvCurrencyAdvance.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvCurrencyAdvance.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvCurrencyAdvance.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvCurrencyAdvance.OptionsView.ShowAutoFilterRow = True
        Me.INDGvCurrencyAdvance.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDGvCurrencyAdvance, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvCurrencyAdvance, False)
        '
        'GridColumn1179
        '
        Me.GridColumn1179.Caption = "Codígo"
        Me.GridColumn1179.FieldName = "Codigo"
        Me.GridColumn1179.Name = "GridColumn1179"
        Me.GridColumn1179.Visible = True
        Me.GridColumn1179.VisibleIndex = 0
        '
        'GridColumn1180
        '
        Me.GridColumn1180.Caption = "Nombre"
        Me.GridColumn1180.FieldName = "CurrencyName"
        Me.GridColumn1180.Name = "GridColumn1180"
        Me.GridColumn1180.Visible = True
        Me.GridColumn1180.VisibleIndex = 1
        '
        'GridColumn1181
        '
        Me.GridColumn1181.Caption = "Abreviación"
        Me.GridColumn1181.FieldName = "Abbreviation"
        Me.GridColumn1181.Name = "GridColumn1181"
        Me.GridColumn1181.Visible = True
        Me.GridColumn1181.VisibleIndex = 2
        '
        'INDteDaysTerm
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDteDaysTerm, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDteDaysTerm, False)
        Me.INDteDaysTerm.EnterMoveNextControl = True
        Me.INDteDaysTerm.Location = New System.Drawing.Point(-383, 463)
        Me.IndigoTextEdit1.SetMascara(Me.INDteDaysTerm, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDteDaysTerm.Name = "INDteDaysTerm"
        Me.INDteDaysTerm.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDteDaysTerm.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteDaysTerm.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDteDaysTerm.Properties.Appearance.Options.UseBackColor = True
        Me.INDteDaysTerm.Properties.Appearance.Options.UseFont = True
        Me.INDteDaysTerm.Properties.Appearance.Options.UseForeColor = True
        Me.INDteDaysTerm.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDteDaysTerm.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDteDaysTerm.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteDaysTerm.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDteDaysTerm.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDteDaysTerm.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDteDaysTerm.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDteDaysTerm.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDteDaysTerm.Properties.Mask.EditMask = "n0"
        Me.INDteDaysTerm.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDteDaysTerm.Properties.MaxLength = 200
        Me.INDteDaysTerm.Properties.ReadOnly = True
        Me.INDteDaysTerm.Size = New System.Drawing.Size(386, 28)
        Me.INDteDaysTerm.StyleController = Me.INDlyPurchaseOrder
        Me.INDteDaysTerm.TabIndex = 14
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDteDaysTerm, 0)
        '
        'INDsleFunctionalUnitRequest
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleFunctionalUnitRequest, AppearanceObject15)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleFunctionalUnitRequest, AppearanceObject16)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleFunctionalUnitRequest, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleFunctionalUnitRequest, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleFunctionalUnitRequest, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleFunctionalUnitRequest, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleFunctionalUnitRequest, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleFunctionalUnitRequest, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleFunctionalUnitRequest, False)
        Me.INDsleFunctionalUnitRequest.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleFunctionalUnitRequest, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleFunctionalUnitRequest, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleFunctionalUnitRequest, False)
        Me.INDsleFunctionalUnitRequest.Location = New System.Drawing.Point(-383, 143)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleFunctionalUnitRequest, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleFunctionalUnitRequest.Name = "INDsleFunctionalUnitRequest"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleFunctionalUnitRequest, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleFunctionalUnitRequest, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleFunctionalUnitRequest, False)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDsleFunctionalUnitRequest, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleFunctionalUnitRequest, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleFunctionalUnitRequest, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleFunctionalUnitRequest, False)
        Me.INDsleFunctionalUnitRequest.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDsleFunctionalUnitRequest.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleFunctionalUnitRequest.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleFunctionalUnitRequest.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleFunctionalUnitRequest.Properties.Appearance.Options.UseFont = True
        Me.INDsleFunctionalUnitRequest.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleFunctionalUnitRequest.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleFunctionalUnitRequest.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleFunctionalUnitRequest.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleFunctionalUnitRequest.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleFunctionalUnitRequest.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleFunctionalUnitRequest.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleFunctionalUnitRequest.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleFunctionalUnitRequest.Properties.DisplayMember = "CodeDescription"
        Me.INDsleFunctionalUnitRequest.Properties.NullText = ""
        Me.INDsleFunctionalUnitRequest.Properties.PopupSizeable = False
        Me.INDsleFunctionalUnitRequest.Properties.PopupView = Me.INDGdvStock1
        Me.INDsleFunctionalUnitRequest.Properties.ShowFooter = False
        Me.INDsleFunctionalUnitRequest.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleFunctionalUnitRequest, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleFunctionalUnitRequest, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleFunctionalUnitRequest, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleFunctionalUnitRequest, True)
        Me.INDsleFunctionalUnitRequest.Size = New System.Drawing.Size(386, 28)
        Me.INDsleFunctionalUnitRequest.StyleController = Me.INDlyPurchaseOrder
        Me.INDsleFunctionalUnitRequest.TabIndex = 8
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleFunctionalUnitRequest, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleFunctionalUnitRequest, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleFunctionalUnitRequest, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleFunctionalUnitRequest, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleFunctionalUnitRequest, False)
        '
        'INDGdvStock1
        '
        Me.INDGdvStock1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGdvStock1.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGdvStock1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGdvStock1.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGdvStock1.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGdvStock1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGdvStock1.Appearance.GroupRow.Options.UseFont = True
        Me.INDGdvStock1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGdvStock1.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGdvStock1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGdvStock1.Appearance.Row.Options.UseFont = True
        Me.INDGdvStock1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColWarehouseCode1, Me.INDColWarehouseName1})
        Me.INDGdvStock1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGdvStock1.Name = "INDGdvStock1"
        Me.INDGdvStock1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGdvStock1.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGdvStock1.OptionsView.EnableAppearanceOddRow = True
        Me.INDGdvStock1.OptionsView.ShowAutoFilterRow = True
        Me.INDGdvStock1.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDGdvStock1, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGdvStock1, False)
        '
        'INDColWarehouseCode1
        '
        Me.INDColWarehouseCode1.Caption = "Código"
        Me.INDColWarehouseCode1.FieldName = "Codigo"
        Me.INDColWarehouseCode1.Name = "INDColWarehouseCode1"
        Me.INDColWarehouseCode1.Visible = True
        Me.INDColWarehouseCode1.VisibleIndex = 0
        Me.INDColWarehouseCode1.Width = 129
        '
        'INDColWarehouseName1
        '
        Me.INDColWarehouseName1.Caption = "Nombre"
        Me.INDColWarehouseName1.FieldName = "Descripcion"
        Me.INDColWarehouseName1.Name = "INDColWarehouseName1"
        Me.INDColWarehouseName1.Visible = True
        Me.INDColWarehouseName1.VisibleIndex = 1
        Me.INDColWarehouseName1.Width = 255
        '
        'INDsleTypePaymentMethod
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleTypePaymentMethod, AppearanceObject17)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleTypePaymentMethod, AppearanceObject18)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleTypePaymentMethod, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleTypePaymentMethod, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleTypePaymentMethod, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleTypePaymentMethod, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleTypePaymentMethod, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleTypePaymentMethod, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleTypePaymentMethod, False)
        Me.INDsleTypePaymentMethod.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleTypePaymentMethod, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleTypePaymentMethod, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleTypePaymentMethod, False)
        Me.INDsleTypePaymentMethod.Location = New System.Drawing.Point(-383, 335)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleTypePaymentMethod, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleTypePaymentMethod.Name = "INDsleTypePaymentMethod"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleTypePaymentMethod, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleTypePaymentMethod, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleTypePaymentMethod, False)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDsleTypePaymentMethod, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleTypePaymentMethod, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleTypePaymentMethod, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleTypePaymentMethod, False)
        Me.INDsleTypePaymentMethod.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDsleTypePaymentMethod.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleTypePaymentMethod.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleTypePaymentMethod.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleTypePaymentMethod.Properties.Appearance.Options.UseFont = True
        Me.INDsleTypePaymentMethod.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleTypePaymentMethod.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleTypePaymentMethod.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleTypePaymentMethod.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleTypePaymentMethod.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleTypePaymentMethod.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleTypePaymentMethod.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleTypePaymentMethod.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleTypePaymentMethod.Properties.DisplayMember = "Item2"
        Me.INDsleTypePaymentMethod.Properties.NullText = ""
        Me.INDsleTypePaymentMethod.Properties.PopupSizeable = False
        Me.INDsleTypePaymentMethod.Properties.PopupView = Me.INDGdvStock11
        Me.INDsleTypePaymentMethod.Properties.ShowFooter = False
        Me.INDsleTypePaymentMethod.Properties.ValueMember = "Item1"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleTypePaymentMethod, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleTypePaymentMethod, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleTypePaymentMethod, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleTypePaymentMethod, True)
        Me.INDsleTypePaymentMethod.Size = New System.Drawing.Size(386, 28)
        Me.INDsleTypePaymentMethod.StyleController = Me.INDlyPurchaseOrder
        Me.INDsleTypePaymentMethod.TabIndex = 8
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleTypePaymentMethod, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleTypePaymentMethod, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleTypePaymentMethod, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleTypePaymentMethod, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleTypePaymentMethod, False)
        '
        'INDGdvStock11
        '
        Me.INDGdvStock11.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGdvStock11.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGdvStock11.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGdvStock11.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGdvStock11.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGdvStock11.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGdvStock11.Appearance.GroupRow.Options.UseFont = True
        Me.INDGdvStock11.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGdvStock11.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGdvStock11.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGdvStock11.Appearance.Row.Options.UseFont = True
        Me.INDGdvStock11.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColWarehouseName11})
        Me.INDGdvStock11.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGdvStock11.Name = "INDGdvStock11"
        Me.INDGdvStock11.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGdvStock11.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGdvStock11.OptionsView.EnableAppearanceOddRow = True
        Me.INDGdvStock11.OptionsView.ShowAutoFilterRow = True
        Me.INDGdvStock11.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDGdvStock11, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGdvStock11, False)
        '
        'INDColWarehouseName11
        '
        Me.INDColWarehouseName11.Caption = "Nombre"
        Me.INDColWarehouseName11.FieldName = "Item2"
        Me.INDColWarehouseName11.Name = "INDColWarehouseName11"
        Me.INDColWarehouseName11.Visible = True
        Me.INDColWarehouseName11.VisibleIndex = 0
        Me.INDColWarehouseName11.Width = 255
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
        Me.LayoutControlGroup1.CustomizationFormText = "Orden de Compra"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup2, Me.LayoutControlGroup3, Me.INDlyGpProducts, Me.INDlygBudget})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(2552, 713)
        Me.LayoutControlGroup1.TextVisible = False
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
        Me.LayoutControlGroup2.CustomizationFormText = "Datos Principales"
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemCode, Me.INDlyItemContract, Me.INDlyItemBasedContract, Me.INDlyItemDateDocument, Me.INDlyItemSupplier, Me.INDlyItemDetail, Me.INDlyItemOrderType, Me.INDLciCurrency})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(414, 693)
        Me.LayoutControlGroup2.Text = "Datos Principales"
        '
        'INDlyItemCode
        '
        Me.INDlyItemCode.Control = Me.INDbtnCode
        Me.INDlyItemCode.CustomizationFormText = "Consecutivo"
        Me.INDlyItemCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCode.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemCode.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemCode.Name = "INDlyItemCode"
        Me.INDlyItemCode.ShowInCustomizationForm = False
        Me.INDlyItemCode.Size = New System.Drawing.Size(390, 64)
        Me.INDlyItemCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCode.Text = "Consecutivo"
        Me.INDlyItemCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemCode.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemCode.TextToControlDistance = 5
        '
        'INDlyItemContract
        '
        Me.INDlyItemContract.Control = Me.INDSeLookEdContract
        Me.INDlyItemContract.CustomizationFormText = "Contrato"
        Me.INDlyItemContract.Location = New System.Drawing.Point(0, 192)
        Me.INDlyItemContract.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemContract.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemContract.Name = "INDlyItemContract"
        Me.INDlyItemContract.Size = New System.Drawing.Size(390, 64)
        Me.INDlyItemContract.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemContract.Text = "Contrato"
        Me.INDlyItemContract.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemContract.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemContract.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemContract.TextToControlDistance = 5
        Me.INDlyItemContract.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlyItemBasedContract
        '
        Me.INDlyItemBasedContract.Control = Me.INDCmbContractBased
        Me.INDlyItemBasedContract.CustomizationFormText = "Basado en Contrato"
        Me.INDlyItemBasedContract.Location = New System.Drawing.Point(0, 128)
        Me.INDlyItemBasedContract.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemBasedContract.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemBasedContract.Name = "INDlyItemBasedContract"
        Me.INDlyItemBasedContract.Size = New System.Drawing.Size(390, 64)
        Me.INDlyItemBasedContract.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemBasedContract.Text = "Basado en Contrato"
        Me.INDlyItemBasedContract.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemBasedContract.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemBasedContract.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemBasedContract.TextToControlDistance = 5
        '
        'INDlyItemDateDocument
        '
        Me.INDlyItemDateDocument.Control = Me.INDdeDateDocument
        Me.INDlyItemDateDocument.CustomizationFormText = "Fecha Documento"
        Me.INDlyItemDateDocument.Location = New System.Drawing.Point(0, 64)
        Me.INDlyItemDateDocument.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemDateDocument.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemDateDocument.Name = "INDlyItemDateDocument"
        Me.INDlyItemDateDocument.ShowInCustomizationForm = False
        Me.INDlyItemDateDocument.Size = New System.Drawing.Size(390, 64)
        Me.INDlyItemDateDocument.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemDateDocument.Text = "Fecha Documento"
        Me.INDlyItemDateDocument.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemDateDocument.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemDateDocument.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemDateDocument.TextToControlDistance = 5
        '
        'INDlyItemSupplier
        '
        Me.INDlyItemSupplier.Control = Me.INDsleSupplier
        Me.INDlyItemSupplier.CustomizationFormText = "Proveedor"
        Me.INDlyItemSupplier.Location = New System.Drawing.Point(0, 256)
        Me.INDlyItemSupplier.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemSupplier.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemSupplier.Name = "INDlyItemSupplier"
        Me.INDlyItemSupplier.ShowInCustomizationForm = False
        Me.INDlyItemSupplier.Size = New System.Drawing.Size(390, 64)
        Me.INDlyItemSupplier.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemSupplier.Text = "Proveedor"
        Me.INDlyItemSupplier.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemSupplier.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemSupplier.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemSupplier.TextToControlDistance = 5
        '
        'INDlyItemDetail
        '
        Me.INDlyItemDetail.Control = Me.INDmeDetail
        Me.INDlyItemDetail.CustomizationFormText = "Detalle"
        Me.INDlyItemDetail.Location = New System.Drawing.Point(0, 448)
        Me.INDlyItemDetail.MaxSize = New System.Drawing.Size(390, 160)
        Me.INDlyItemDetail.MinSize = New System.Drawing.Size(390, 160)
        Me.INDlyItemDetail.Name = "INDlyItemDetail"
        Me.INDlyItemDetail.ShowInCustomizationForm = False
        Me.INDlyItemDetail.Size = New System.Drawing.Size(390, 192)
        Me.INDlyItemDetail.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemDetail.Text = "Detalle"
        Me.INDlyItemDetail.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemDetail.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemDetail.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemDetail.TextToControlDistance = 5
        '
        'INDlyItemOrderType
        '
        Me.INDlyItemOrderType.Control = Me.INDCmbOrderType
        Me.INDlyItemOrderType.CustomizationFormText = "Tipo de Orden"
        Me.INDlyItemOrderType.Location = New System.Drawing.Point(0, 320)
        Me.INDlyItemOrderType.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemOrderType.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemOrderType.Name = "INDlyItemOrderType"
        Me.INDlyItemOrderType.ShowInCustomizationForm = False
        Me.INDlyItemOrderType.Size = New System.Drawing.Size(390, 64)
        Me.INDlyItemOrderType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemOrderType.Text = "Tipo de Orden"
        Me.INDlyItemOrderType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemOrderType.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemOrderType.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemOrderType.TextToControlDistance = 5
        '
        'INDLciCurrency
        '
        Me.INDLciCurrency.Control = Me.INDsleCurrency
        Me.INDLciCurrency.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciCurrency.CustomizationFormText = "Moneda"
        Me.INDLciCurrency.Location = New System.Drawing.Point(0, 384)
        Me.INDLciCurrency.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciCurrency.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciCurrency.Name = "INDLciCurrency"
        Me.INDLciCurrency.Size = New System.Drawing.Size(390, 64)
        Me.INDLciCurrency.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciCurrency.Text = "Moneda"
        Me.INDLciCurrency.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciCurrency.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciCurrency.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciCurrency.TextToControlDistance = 5
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
        Me.LayoutControlGroup3.CustomizationFormText = "Inforación Adicional"
        Me.LayoutControlGroup3.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemDeliveryForm, Me.INDlyItemDeliveryPlace, Me.INDlyItemDateDelivery, Me.INDLciIvaValue, Me.INDLciValue, Me.INDLciTotalValue, Me.INDlyItemStock, Me.INDlyItemDaysTerm, Me.INDlyItemFunctionalUnitRequest, Me.INDlyItemTypePaymentMethod})
        Me.LayoutControlGroup3.Location = New System.Drawing.Point(414, 0)
        Me.LayoutControlGroup3.Name = "LayoutControlGroup3"
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(414, 693)
        Me.LayoutControlGroup3.Text = "Información Adicional"
        '
        'INDlyItemDeliveryForm
        '
        Me.INDlyItemDeliveryForm.Control = Me.INDteDeliveryForm
        Me.INDlyItemDeliveryForm.CustomizationFormText = "Forma Entrega"
        Me.INDlyItemDeliveryForm.Location = New System.Drawing.Point(0, 192)
        Me.INDlyItemDeliveryForm.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemDeliveryForm.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemDeliveryForm.Name = "INDlyItemDeliveryForm"
        Me.INDlyItemDeliveryForm.Size = New System.Drawing.Size(390, 64)
        Me.INDlyItemDeliveryForm.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemDeliveryForm.Text = "Forma Entrega"
        Me.INDlyItemDeliveryForm.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemDeliveryForm.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemDeliveryForm.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemDeliveryForm.TextToControlDistance = 5
        '
        'INDlyItemDeliveryPlace
        '
        Me.INDlyItemDeliveryPlace.Control = Me.INDteDeliveryPlace
        Me.INDlyItemDeliveryPlace.CustomizationFormText = "Lugar Entrega"
        Me.INDlyItemDeliveryPlace.Location = New System.Drawing.Point(0, 320)
        Me.INDlyItemDeliveryPlace.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemDeliveryPlace.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemDeliveryPlace.Name = "INDlyItemDeliveryPlace"
        Me.INDlyItemDeliveryPlace.Size = New System.Drawing.Size(390, 64)
        Me.INDlyItemDeliveryPlace.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemDeliveryPlace.Text = "Lugar Entrega"
        Me.INDlyItemDeliveryPlace.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemDeliveryPlace.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemDeliveryPlace.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemDeliveryPlace.TextToControlDistance = 5
        '
        'INDlyItemDateDelivery
        '
        Me.INDlyItemDateDelivery.Control = Me.INDdeDateDelivery
        Me.INDlyItemDateDelivery.CustomizationFormText = "Fecha Entrega"
        Me.INDlyItemDateDelivery.Location = New System.Drawing.Point(0, 128)
        Me.INDlyItemDateDelivery.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemDateDelivery.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemDateDelivery.Name = "INDlyItemDateDelivery"
        Me.INDlyItemDateDelivery.ShowInCustomizationForm = False
        Me.INDlyItemDateDelivery.Size = New System.Drawing.Size(390, 64)
        Me.INDlyItemDateDelivery.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemDateDelivery.Text = "Fecha Entrega"
        Me.INDlyItemDateDelivery.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemDateDelivery.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemDateDelivery.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemDateDelivery.TextToControlDistance = 5
        '
        'INDLciIvaValue
        '
        Me.INDLciIvaValue.Control = Me.INDtxtIvaValue
        Me.INDLciIvaValue.CustomizationFormText = "IVA"
        Me.INDLciIvaValue.Location = New System.Drawing.Point(0, 576)
        Me.INDLciIvaValue.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciIvaValue.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciIvaValue.Name = "INDLciIvaValue"
        Me.INDLciIvaValue.Size = New System.Drawing.Size(390, 64)
        Me.INDLciIvaValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciIvaValue.Text = "IVA"
        Me.INDLciIvaValue.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciIvaValue.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciIvaValue.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciIvaValue.TextToControlDistance = 5
        Me.INDLciIvaValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLciValue
        '
        Me.INDLciValue.Control = Me.INDtxtValue
        Me.INDLciValue.Location = New System.Drawing.Point(0, 448)
        Me.INDLciValue.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciValue.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciValue.Name = "INDLciValue"
        Me.INDLciValue.Size = New System.Drawing.Size(390, 64)
        Me.INDLciValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciValue.Text = "SubTotal"
        Me.INDLciValue.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciValue.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciValue.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciValue.TextToControlDistance = 5
        Me.INDLciValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLciTotalValue
        '
        Me.INDLciTotalValue.Control = Me.INDtxtTotalValue
        Me.INDLciTotalValue.CustomizationFormText = "Total"
        Me.INDLciTotalValue.Location = New System.Drawing.Point(0, 512)
        Me.INDLciTotalValue.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciTotalValue.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciTotalValue.Name = "INDLciTotalValue"
        Me.INDLciTotalValue.Size = New System.Drawing.Size(390, 64)
        Me.INDLciTotalValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciTotalValue.Text = "Total"
        Me.INDLciTotalValue.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciTotalValue.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciTotalValue.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciTotalValue.TextToControlDistance = 5
        Me.INDLciTotalValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlyItemStock
        '
        Me.INDlyItemStock.Control = Me.INDsleStock
        Me.INDlyItemStock.CustomizationFormText = "Almacén"
        Me.INDlyItemStock.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemStock.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemStock.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemStock.Name = "INDlyItemStock"
        Me.INDlyItemStock.ShowInCustomizationForm = False
        Me.INDlyItemStock.Size = New System.Drawing.Size(390, 64)
        Me.INDlyItemStock.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemStock.Text = "Almacén"
        Me.INDlyItemStock.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemStock.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemStock.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemStock.TextToControlDistance = 5
        '
        'INDlyItemDaysTerm
        '
        Me.INDlyItemDaysTerm.Control = Me.INDteDaysTerm
        Me.INDlyItemDaysTerm.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDlyItemDaysTerm.CustomizationFormText = "Dìas de Plazo"
        Me.INDlyItemDaysTerm.Enabled = False
        Me.INDlyItemDaysTerm.Location = New System.Drawing.Point(0, 384)
        Me.INDlyItemDaysTerm.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemDaysTerm.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemDaysTerm.Name = "INDlyItemDaysTerm"
        Me.INDlyItemDaysTerm.Size = New System.Drawing.Size(390, 64)
        Me.INDlyItemDaysTerm.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemDaysTerm.Text = "Días de Plazo"
        Me.INDlyItemDaysTerm.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemDaysTerm.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemDaysTerm.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemDaysTerm.TextToControlDistance = 5
        Me.INDlyItemDaysTerm.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlyItemFunctionalUnitRequest
        '
        Me.INDlyItemFunctionalUnitRequest.Control = Me.INDsleFunctionalUnitRequest
        Me.INDlyItemFunctionalUnitRequest.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDlyItemFunctionalUnitRequest.CustomizationFormText = "Unidad Funcional que Solicita"
        Me.INDlyItemFunctionalUnitRequest.Location = New System.Drawing.Point(0, 64)
        Me.INDlyItemFunctionalUnitRequest.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemFunctionalUnitRequest.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemFunctionalUnitRequest.Name = "INDlyItemFunctionalUnitRequest"
        Me.INDlyItemFunctionalUnitRequest.ShowInCustomizationForm = False
        Me.INDlyItemFunctionalUnitRequest.Size = New System.Drawing.Size(390, 64)
        Me.INDlyItemFunctionalUnitRequest.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemFunctionalUnitRequest.Text = "Unidad Funcional que Solicita"
        Me.INDlyItemFunctionalUnitRequest.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemFunctionalUnitRequest.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemFunctionalUnitRequest.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemFunctionalUnitRequest.TextToControlDistance = 5
        Me.INDlyItemFunctionalUnitRequest.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlyItemTypePaymentMethod
        '
        Me.INDlyItemTypePaymentMethod.Control = Me.INDsleTypePaymentMethod
        Me.INDlyItemTypePaymentMethod.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDlyItemTypePaymentMethod.CustomizationFormText = "Forma Pago"
        Me.INDlyItemTypePaymentMethod.Location = New System.Drawing.Point(0, 256)
        Me.INDlyItemTypePaymentMethod.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemTypePaymentMethod.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemTypePaymentMethod.Name = "INDlyItemTypePaymentMethod"
        Me.INDlyItemTypePaymentMethod.ShowInCustomizationForm = False
        Me.INDlyItemTypePaymentMethod.Size = New System.Drawing.Size(390, 64)
        Me.INDlyItemTypePaymentMethod.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemTypePaymentMethod.Text = "Forma Pago"
        Me.INDlyItemTypePaymentMethod.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemTypePaymentMethod.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemTypePaymentMethod.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemTypePaymentMethod.TextToControlDistance = 5
        '
        'INDlyGpProducts
        '
        Me.INDlyGpProducts.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGpProducts.AppearanceGroup.Options.UseFont = True
        Me.INDlyGpProducts.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGpProducts.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyGpProducts.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGpProducts.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyGpProducts.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlyGpProducts.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlyGpProducts.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGpProducts.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyGpProducts.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGpProducts.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlyGpProducts.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGpProducts.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyGpProducts, False)
        Me.INDlyGpProducts.CustomizationFormText = "Productos"
        Me.INDlyGpProducts.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyItemAddProducts, Me.INDLyItemGcProducts, Me.LayoutControlItem2, Me.LayoutControlItem1})
        Me.INDlyGpProducts.Location = New System.Drawing.Point(828, 0)
        Me.INDlyGpProducts.Name = "INDlyGpProducts"
        Me.INDlyGpProducts.Size = New System.Drawing.Size(852, 693)
        Me.INDlyGpProducts.Text = "Productos"
        '
        'INDLyItemAddProducts
        '
        Me.INDLyItemAddProducts.Control = Me.INDBtnAddProducts
        Me.INDLyItemAddProducts.CustomizationFormText = "Agregar Productos"
        Me.INDLyItemAddProducts.Location = New System.Drawing.Point(0, 0)
        Me.INDLyItemAddProducts.MaxSize = New System.Drawing.Size(0, 36)
        Me.INDLyItemAddProducts.MinSize = New System.Drawing.Size(121, 27)
        Me.INDLyItemAddProducts.Name = "INDLyItemAddProducts"
        Me.INDLyItemAddProducts.Size = New System.Drawing.Size(752, 36)
        Me.INDLyItemAddProducts.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyItemAddProducts.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLyItemAddProducts.TextVisible = False
        '
        'INDLyItemGcProducts
        '
        Me.INDLyItemGcProducts.Control = Me.INDGcProducts
        Me.INDLyItemGcProducts.CustomizationFormText = "Productos"
        Me.INDLyItemGcProducts.Location = New System.Drawing.Point(0, 36)
        Me.INDLyItemGcProducts.MaxSize = New System.Drawing.Size(828, 0)
        Me.INDLyItemGcProducts.MinSize = New System.Drawing.Size(828, 1)
        Me.INDLyItemGcProducts.Name = "INDLyItemGcProducts"
        Me.INDLyItemGcProducts.Size = New System.Drawing.Size(828, 604)
        Me.INDLyItemGcProducts.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyItemGcProducts.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLyItemGcProducts.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDEsbDetails
        Me.LayoutControlItem2.Location = New System.Drawing.Point(752, 0)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(38, 36)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(38, 36)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(38, 36)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDBtnImportFile
        Me.LayoutControlItem1.Location = New System.Drawing.Point(790, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(38, 36)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(38, 36)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(38, 36)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
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
        Me.INDlygBudget.CustomizationFormText = "Listado Presupuestal"
        Me.INDlygBudget.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemGridAvailability, Me.INDlyItemAvailability, Me.INDlyItemAddAvailability, Me.INDLciBudgetaryEntityId, Me.INDLciBudgetaryValidityId})
        Me.INDlygBudget.Location = New System.Drawing.Point(1680, 0)
        Me.INDlygBudget.Name = "INDlygBudget"
        Me.INDlygBudget.Size = New System.Drawing.Size(852, 693)
        Me.INDlygBudget.Text = "Listado Presupuestal"
        Me.INDlygBudget.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlyItemGridAvailability
        '
        Me.INDlyItemGridAvailability.Control = Me.INDgcAvailability
        Me.INDlyItemGridAvailability.CustomizationFormText = "INDlyItemGridAvailability"
        Me.INDlyItemGridAvailability.Location = New System.Drawing.Point(0, 108)
        Me.INDlyItemGridAvailability.MaxSize = New System.Drawing.Size(828, 0)
        Me.INDlyItemGridAvailability.MinSize = New System.Drawing.Size(828, 24)
        Me.INDlyItemGridAvailability.Name = "INDlyItemGridAvailability"
        Me.INDlyItemGridAvailability.Size = New System.Drawing.Size(828, 532)
        Me.INDlyItemGridAvailability.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemGridAvailability.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemGridAvailability.TextVisible = False
        '
        'INDlyItemAvailability
        '
        Me.INDlyItemAvailability.Control = Me.INDsleAvailability
        Me.INDlyItemAvailability.CustomizationFormText = "Rubro de Disponibilidades"
        Me.INDlyItemAvailability.Location = New System.Drawing.Point(0, 72)
        Me.INDlyItemAvailability.MaxSize = New System.Drawing.Size(600, 36)
        Me.INDlyItemAvailability.MinSize = New System.Drawing.Size(600, 36)
        Me.INDlyItemAvailability.Name = "INDlyItemAvailability"
        Me.INDlyItemAvailability.Size = New System.Drawing.Size(600, 36)
        Me.INDlyItemAvailability.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemAvailability.Text = "Rubro de Disponibilidades"
        Me.INDlyItemAvailability.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemAvailability.TextSize = New System.Drawing.Size(180, 21)
        Me.INDlyItemAvailability.TextToControlDistance = 5
        '
        'INDlyItemAddAvailability
        '
        Me.INDlyItemAddAvailability.Control = Me.INDbtnAddAvailability
        Me.INDlyItemAddAvailability.CustomizationFormText = "INDlyItemAddAvailability"
        Me.INDlyItemAddAvailability.Location = New System.Drawing.Point(600, 72)
        Me.INDlyItemAddAvailability.MaxSize = New System.Drawing.Size(228, 32)
        Me.INDlyItemAddAvailability.MinSize = New System.Drawing.Size(228, 32)
        Me.INDlyItemAddAvailability.Name = "INDlyItemAddAvailability"
        Me.INDlyItemAddAvailability.Size = New System.Drawing.Size(228, 36)
        Me.INDlyItemAddAvailability.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemAddAvailability.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemAddAvailability.TextVisible = False
        '
        'INDLciBudgetaryEntityId
        '
        Me.INDLciBudgetaryEntityId.Control = Me.INDSleBudgetaryEntityId
        Me.INDLciBudgetaryEntityId.Location = New System.Drawing.Point(0, 0)
        Me.INDLciBudgetaryEntityId.MaxSize = New System.Drawing.Size(600, 36)
        Me.INDLciBudgetaryEntityId.MinSize = New System.Drawing.Size(600, 36)
        Me.INDLciBudgetaryEntityId.Name = "INDLciBudgetaryEntityId"
        Me.INDLciBudgetaryEntityId.Size = New System.Drawing.Size(828, 36)
        Me.INDLciBudgetaryEntityId.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciBudgetaryEntityId.Text = "Entidad Presupuestal"
        Me.INDLciBudgetaryEntityId.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciBudgetaryEntityId.TextSize = New System.Drawing.Size(180, 21)
        Me.INDLciBudgetaryEntityId.TextToControlDistance = 5
        '
        'INDLciBudgetaryValidityId
        '
        Me.INDLciBudgetaryValidityId.Control = Me.INDSleBudgetaryValidityId
        Me.INDLciBudgetaryValidityId.Location = New System.Drawing.Point(0, 36)
        Me.INDLciBudgetaryValidityId.MaxSize = New System.Drawing.Size(600, 36)
        Me.INDLciBudgetaryValidityId.MinSize = New System.Drawing.Size(600, 36)
        Me.INDLciBudgetaryValidityId.Name = "INDLciBudgetaryValidityId"
        Me.INDLciBudgetaryValidityId.Size = New System.Drawing.Size(828, 36)
        Me.INDLciBudgetaryValidityId.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciBudgetaryValidityId.Text = "Vigencia"
        Me.INDLciBudgetaryValidityId.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciBudgetaryValidityId.TextSize = New System.Drawing.Size(180, 21)
        Me.INDLciBudgetaryValidityId.TextToControlDistance = 5
        '
        'IndigoGridControl1
        '
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'IndigoGridView2
        '
        Me.IndigoGridView2.RaiseMenuPopUp = True
        Me.IndigoGridView2.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit3
        '
        'RepositoryItemPopupContainerEdit11
        '
        Me.RepositoryItemPopupContainerEdit11.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit11.Name = "RepositoryItemPopupContainerEdit11"
        '
        'RepositoryItemPopupContainerEdit21
        '
        Me.RepositoryItemPopupContainerEdit21.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit21.Name = "RepositoryItemPopupContainerEdit21"
        '
        'RepositoryItemPopupContainerEdit31
        '
        Me.RepositoryItemPopupContainerEdit31.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit31.Name = "RepositoryItemPopupContainerEdit31"
        '
        'RepositoryItemPopupContainerEdit12
        '
        Me.RepositoryItemPopupContainerEdit12.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit12.Name = "RepositoryItemPopupContainerEdit12"
        '
        'RepositoryItemPopupContainerEdit121
        '
        Me.RepositoryItemPopupContainerEdit121.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit121.Name = "RepositoryItemPopupContainerEdit121"
        '
        'RepositoryItemPopupContainerEdit32
        '
        Me.RepositoryItemPopupContainerEdit32.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit32.Name = "RepositoryItemPopupContainerEdit32"
        '
        'RepositoryItemPopupContainerEdit13
        '
        Me.RepositoryItemPopupContainerEdit13.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit13.Name = "RepositoryItemPopupContainerEdit13"
        '
        'RepositoryItemPopupContainerEdit311
        '
        Me.RepositoryItemPopupContainerEdit311.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit311.Name = "RepositoryItemPopupContainerEdit311"
        '
        'FrmPurchaseOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1298, 705)
        Me.IconOptions.ShowIcon = False
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "FrmPurchaseOrder"
        Me.Opacity = 1.0R
        Me.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.Tag = "316"
        Me.Text = "Orden de Compra"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyPurchaseOrder, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyPurchaseOrder.ResumeLayout(False)
        CType(Me.INDtxtValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleBudgetaryValidityId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit2View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleBudgetaryEntityId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDCmbContractBased.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSeLookEdContract.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDCmbOrderType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcProducts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvProducts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRepPercent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRepTexQuantityOrValues, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdeDateDelivery.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdeDateDelivery.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDteDeliveryPlace.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDteDeliveryForm.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleStock.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGdvStock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdeDateDocument.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdeDateDocument.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleSupplier.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.viewSupplier, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDmeDetail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcAvailability, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewAvailability, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepTxtValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleAvailability.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewSearchAvailability, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtIvaValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtTotalValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleCurrency.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvCurrencyAdvance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDteDaysTerm.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleFunctionalUnitRequest.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGdvStock1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleTypePaymentMethod.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGdvStock11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemContract, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemBasedContract, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDateDocument, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemSupplier, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemOrderType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciCurrency, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDeliveryForm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDeliveryPlace, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDateDelivery, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciIvaValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciTotalValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemStock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDaysTerm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemFunctionalUnitRequest, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemTypePaymentMethod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGpProducts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemAddProducts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemGcProducts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygBudget, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemGridAvailability, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemAvailability, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemAddAvailability, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciBudgetaryEntityId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciBudgetaryValidityId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoComboBoxEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit31, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit121, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit32, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit311, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDlyPurchaseOrder As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDsleStock As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents INDGdvStock As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDdeDateDocument As DevExpress.XtraEditors.DateEdit
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents INDlyItemDateDocument As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemSupplier As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemStock As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents INDteDeliveryPlace As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDteDeliveryForm As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemDetail As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemDeliveryForm As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemDeliveryPlace As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDsleSupplier As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents viewSupplier As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDdeDateDelivery As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDlyItemDateDelivery As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents LayoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyGpProducts As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDBtnAddProducts As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDLyItemAddProducts As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDmeDetail As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDGcProducts As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvProducts As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLyItemGcProducts As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Quantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRepTexQuantityOrValues As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents INDColTotalValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColDiscountPercent As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColIVA As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDbtnCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDlyItemCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDCmbOrderType As DevExpress.XtraEditors.ImageComboBoxEdit
    Friend WithEvents INDlyItemOrderType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoComboBoxEdit1 As Presentation.Controls.IndigoComboBoxEdit
    Friend WithEvents IndColNitSupplier As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColSupplierName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColDistributionName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSeLookEdContract As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItemContract As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDColWarehouseCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColWarehouseName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDCmbContractBased As DevExpress.XtraEditors.ImageComboBoxEdit
    Friend WithEvents INDlyItemBasedContract As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDColCodeContractInventory As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColCodeNameContractTypeInventory As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColContractNumberInventory As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColUnitValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRepPercent As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents INDBtnImportFile As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDEsbDetails As ExportStructureButton
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDgcAvailability As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDviewAvailability As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn154 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn155 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn156 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn158 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn159 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn160 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepTxtValue As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents INDsleAvailability As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDviewSearchAvailability As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn147 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn149 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn150 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn151 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn152 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDbtnAddAvailability As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDlygBudget As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemGridAvailability As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemAvailability As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemAddAvailability As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridView2 As IndigoGridView
    Friend WithEvents INDSleBudgetaryValidityId As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit2View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDSleBudgetaryEntityId As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciBudgetaryEntityId As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciBudgetaryValidityId As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDtxtValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtIvaValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDtxtTotalValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciIvaValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciTotalValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn14 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn15 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn16 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemPopupContainerEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDsleCurrency As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvCurrencyAdvance As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1179 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1180 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1181 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLciCurrency As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents RepositoryItemPopupContainerEdit11 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit21 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDteDaysTerm As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemDaysTerm As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemFunctionalUnitRequest As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents RepositoryItemPopupContainerEdit12 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit31 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDGdvStock1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColWarehouseCode1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColWarehouseName1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDsleFunctionalUnitRequest As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents RepositoryItemPopupContainerEdit311 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit32 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit121 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit13 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDsleTypePaymentMethod As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGdvStock11 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColWarehouseName11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlyItemTypePaymentMethod As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDColTotalIva As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents OutstandingQuantity As DevExpress.XtraGrid.Columns.GridColumn
End Class
