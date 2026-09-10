Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmTransferOrder
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
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject5 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject6 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
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
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlcTransferOrder = New DevExpress.XtraLayout.LayoutControl()
        Me.INDsleTransitWarehouseId = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView4 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn14 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn15 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDPccDetailPhysicalInventory = New DevExpress.XtraEditors.PopupContainerControl()
        Me.INDGcDetailPhysicalInventory = New DevExpress.XtraGrid.GridControl()
        Me.INDGvDetailPhysicalInventory = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGcdProduct = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGcdBatchSerial = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGcdQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcProducts = New DevExpress.XtraGrid.GridControl()
        Me.INDGvProducts = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDDescriptionProduct = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGcoInventoryQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGcoQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGcoDetailPhysical = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRiPcePhysicalInventory = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDGcoCostProduct = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemTextEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.INDBtnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.INDmeDescription = New DevExpress.XtraEditors.MemoEdit()
        Me.INDsleThirdPartyId = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleAdjustmentConceptId = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleTargetFunctionalUnitId = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleTargetWarehouseId = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit4View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleSourceWarehouseId = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGdvSourceWarehouseId = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColInConsignment = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRptIsConsignment = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.RepositoryItemGridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn16 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleDispatchTo = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit2View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleOrderType = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDdeDocumentDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDbtnCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDlcgtransferOrder = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlcgGeneralData = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlciCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciDocumentDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciOrderType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciDescription = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlcgDataOptional = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlciThirdPartyId = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciTargetWarehouseId = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciTargetFunctionalUnitId = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciAdjustmentConceptId = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciTransitWarehouseId = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciDispatchTo = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciSourceWarehouseId = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgProducts = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcTransferOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlcTransferOrder.SuspendLayout()
        CType(Me.INDsleTransitWarehouseId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPccDetailPhysicalInventory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPccDetailPhysicalInventory.SuspendLayout()
        CType(Me.INDGcDetailPhysicalInventory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvDetailPhysicalInventory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcProducts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvProducts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRiPcePhysicalInventory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDmeDescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleThirdPartyId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleAdjustmentConceptId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleTargetFunctionalUnitId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleTargetWarehouseId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit4View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleSourceWarehouseId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGdvSourceWarehouseId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRptIsConsignment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemGridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleDispatchTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit2View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleOrderType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdeDocumentDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdeDocumentDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgtransferOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgGeneralData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciDocumentDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciOrderType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgDataOptional, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciThirdPartyId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciTargetWarehouseId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciTargetFunctionalUnitId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciAdjustmentConceptId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciTransitWarehouseId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciDispatchTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciSourceWarehouseId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgProducts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlcTransferOrder)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1298, 594)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1298, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(1298, 130)
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
        Me.CtrNavigationControl1.LayoutControl = Me.INDlcTransferOrder
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 585)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDlcTransferOrder
        '
        Me.INDlcTransferOrder.AllowCustomization = False
        Me.INDlcTransferOrder.Controls.Add(Me.INDsleTransitWarehouseId)
        Me.INDlcTransferOrder.Controls.Add(Me.INDPccDetailPhysicalInventory)
        Me.INDlcTransferOrder.Controls.Add(Me.INDgcProducts)
        Me.INDlcTransferOrder.Controls.Add(Me.INDBtnAdd)
        Me.INDlcTransferOrder.Controls.Add(Me.INDmeDescription)
        Me.INDlcTransferOrder.Controls.Add(Me.INDsleThirdPartyId)
        Me.INDlcTransferOrder.Controls.Add(Me.INDsleAdjustmentConceptId)
        Me.INDlcTransferOrder.Controls.Add(Me.INDsleTargetFunctionalUnitId)
        Me.INDlcTransferOrder.Controls.Add(Me.INDsleTargetWarehouseId)
        Me.INDlcTransferOrder.Controls.Add(Me.INDsleSourceWarehouseId)
        Me.INDlcTransferOrder.Controls.Add(Me.INDsleDispatchTo)
        Me.INDlcTransferOrder.Controls.Add(Me.INDsleOrderType)
        Me.INDlcTransferOrder.Controls.Add(Me.INDdeDocumentDate)
        Me.INDlcTransferOrder.Controls.Add(Me.INDbtnCode)
        Me.INDlcTransferOrder.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDlcTransferOrder, False)
        Me.INDlcTransferOrder.Location = New System.Drawing.Point(202, 7)
        Me.INDlcTransferOrder.Name = "INDlcTransferOrder"
        Me.INDlcTransferOrder.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(2981, 310, 574, 569)
        Me.INDlcTransferOrder.Root = Me.INDlcgtransferOrder
        Me.INDlcTransferOrder.Size = New System.Drawing.Size(1094, 585)
        Me.INDlcTransferOrder.TabIndex = 1
        Me.INDlcTransferOrder.Text = "LayoutControl1"
        '
        'INDsleTransitWarehouseId
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleTransitWarehouseId, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleTransitWarehouseId, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleTransitWarehouseId, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleTransitWarehouseId, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleTransitWarehouseId, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleTransitWarehouseId, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleTransitWarehouseId, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleTransitWarehouseId, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleTransitWarehouseId, False)
        Me.INDsleTransitWarehouseId.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleTransitWarehouseId, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleTransitWarehouseId, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleTransitWarehouseId, False)
        Me.INDsleTransitWarehouseId.Location = New System.Drawing.Point(438, 207)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleTransitWarehouseId, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleTransitWarehouseId.Name = "INDsleTransitWarehouseId"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleTransitWarehouseId, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleTransitWarehouseId, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleTransitWarehouseId, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDsleTransitWarehouseId, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleTransitWarehouseId, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleTransitWarehouseId, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleTransitWarehouseId, False)
        Me.INDsleTransitWarehouseId.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleTransitWarehouseId.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleTransitWarehouseId.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleTransitWarehouseId.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleTransitWarehouseId.Properties.Appearance.Options.UseFont = True
        Me.INDsleTransitWarehouseId.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleTransitWarehouseId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleTransitWarehouseId.Properties.DisplayMember = "CodeName"
        Me.INDsleTransitWarehouseId.Properties.NullText = ""
        Me.INDsleTransitWarehouseId.Properties.PopupSizeable = False
        Me.INDsleTransitWarehouseId.Properties.PopupView = Me.GridView4
        Me.INDsleTransitWarehouseId.Properties.ShowFooter = False
        Me.INDsleTransitWarehouseId.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleTransitWarehouseId, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleTransitWarehouseId, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleTransitWarehouseId, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleTransitWarehouseId, True)
        Me.INDsleTransitWarehouseId.Size = New System.Drawing.Size(386, 28)
        Me.INDsleTransitWarehouseId.StyleController = Me.INDlcTransferOrder
        Me.INDsleTransitWarehouseId.TabIndex = 13
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleTransitWarehouseId, "302")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleTransitWarehouseId, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleTransitWarehouseId, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleTransitWarehouseId, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleTransitWarehouseId, False)
        '
        'GridView4
        '
        Me.GridView4.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView4.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView4.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView4.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView4.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView4.Appearance.GroupRow.Options.UseFont = True
        Me.GridView4.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView4.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView4.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView4.Appearance.Row.Options.UseFont = True
        Me.GridView4.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn14, Me.GridColumn15})
        Me.GridView4.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView4.Name = "GridView4"
        Me.GridView4.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView4.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView4.OptionsView.EnableAppearanceOddRow = True
        Me.GridView4.OptionsView.ShowAutoFilterRow = True
        Me.GridView4.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView4, False)
        '
        'GridColumn14
        '
        Me.GridColumn14.Caption = "Código"
        Me.GridColumn14.FieldName = "Code"
        Me.GridColumn14.Name = "GridColumn14"
        Me.GridColumn14.Visible = True
        Me.GridColumn14.VisibleIndex = 0
        Me.GridColumn14.Width = 249
        '
        'GridColumn15
        '
        Me.GridColumn15.Caption = "Nombre"
        Me.GridColumn15.FieldName = "Name"
        Me.GridColumn15.Name = "GridColumn15"
        Me.GridColumn15.Visible = True
        Me.GridColumn15.VisibleIndex = 1
        Me.GridColumn15.Width = 909
        '
        'INDPccDetailPhysicalInventory
        '
        Me.INDPccDetailPhysicalInventory.Controls.Add(Me.INDGcDetailPhysicalInventory)
        Me.INDPccDetailPhysicalInventory.Location = New System.Drawing.Point(46, 510)
        Me.INDPccDetailPhysicalInventory.Name = "INDPccDetailPhysicalInventory"
        Me.INDPccDetailPhysicalInventory.Size = New System.Drawing.Size(450, 150)
        Me.INDPccDetailPhysicalInventory.TabIndex = 12
        '
        'INDGcDetailPhysicalInventory
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcDetailPhysicalInventory, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcDetailPhysicalInventory, Nothing)
        Me.INDGcDetailPhysicalInventory.Cursor = System.Windows.Forms.Cursors.Default
        Me.INDGcDetailPhysicalInventory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.IndigoGridControl1.SetExportButton(Me.INDGcDetailPhysicalInventory, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcDetailPhysicalInventory, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcDetailPhysicalInventory, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcDetailPhysicalInventory, False)
        Me.INDGcDetailPhysicalInventory.Location = New System.Drawing.Point(0, 0)
        Me.INDGcDetailPhysicalInventory.MainView = Me.INDGvDetailPhysicalInventory
        Me.INDGcDetailPhysicalInventory.Name = "INDGcDetailPhysicalInventory"
        Me.INDGcDetailPhysicalInventory.Size = New System.Drawing.Size(450, 150)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcDetailPhysicalInventory, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcDetailPhysicalInventory.TabIndex = 0
        Me.INDGcDetailPhysicalInventory.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvDetailPhysicalInventory})
        '
        'INDGvDetailPhysicalInventory
        '
        Me.INDGvDetailPhysicalInventory.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvDetailPhysicalInventory.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvDetailPhysicalInventory.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDGvDetailPhysicalInventory.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvDetailPhysicalInventory.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvDetailPhysicalInventory.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGvDetailPhysicalInventory.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvDetailPhysicalInventory.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvDetailPhysicalInventory.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvDetailPhysicalInventory.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvDetailPhysicalInventory.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvDetailPhysicalInventory.Appearance.Row.Options.UseFont = True
        Me.INDGvDetailPhysicalInventory.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvDetailPhysicalInventory.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvDetailPhysicalInventory.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGcdProduct, Me.INDGcdBatchSerial, Me.INDGcdQuantity})
        Me.INDGvDetailPhysicalInventory.GridControl = Me.INDGcDetailPhysicalInventory
        Me.INDGvDetailPhysicalInventory.Name = "INDGvDetailPhysicalInventory"
        Me.INDGvDetailPhysicalInventory.OptionsCustomization.AllowGroup = False
        Me.INDGvDetailPhysicalInventory.OptionsDetail.EnableMasterViewMode = False
        Me.INDGvDetailPhysicalInventory.OptionsDetail.ShowDetailTabs = False
        Me.INDGvDetailPhysicalInventory.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvDetailPhysicalInventory.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvDetailPhysicalInventory.OptionsView.ShowAutoFilterRow = True
        Me.INDGvDetailPhysicalInventory.OptionsView.ShowDetailButtons = False
        Me.INDGvDetailPhysicalInventory.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvDetailPhysicalInventory, False)
        '
        'INDGcdProduct
        '
        Me.INDGcdProduct.Caption = "Producto"
        Me.INDGcdProduct.FieldName = "CodeNameProduct"
        Me.INDGcdProduct.Name = "INDGcdProduct"
        Me.INDGcdProduct.OptionsColumn.AllowEdit = False
        Me.INDGcdProduct.OptionsColumn.AllowFocus = False
        Me.INDGcdProduct.Visible = True
        Me.INDGcdProduct.VisibleIndex = 0
        Me.INDGcdProduct.Width = 200
        '
        'INDGcdBatchSerial
        '
        Me.INDGcdBatchSerial.Caption = "Lote"
        Me.INDGcdBatchSerial.FieldName = "CodeBatchSerial"
        Me.INDGcdBatchSerial.Name = "INDGcdBatchSerial"
        Me.INDGcdBatchSerial.OptionsColumn.AllowEdit = False
        Me.INDGcdBatchSerial.OptionsColumn.AllowFocus = False
        Me.INDGcdBatchSerial.Visible = True
        Me.INDGcdBatchSerial.VisibleIndex = 1
        Me.INDGcdBatchSerial.Width = 150
        '
        'INDGcdQuantity
        '
        Me.INDGcdQuantity.Caption = "Cantidad"
        Me.INDGcdQuantity.FieldName = "Quantity"
        Me.INDGcdQuantity.Name = "INDGcdQuantity"
        Me.INDGcdQuantity.OptionsColumn.AllowEdit = False
        Me.INDGcdQuantity.OptionsColumn.AllowFocus = False
        Me.INDGcdQuantity.Visible = True
        Me.INDGcdQuantity.VisibleIndex = 2
        Me.INDGcdQuantity.Width = 82
        '
        'INDgcProducts
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcProducts, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcProducts, Nothing)
        Me.INDgcProducts.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.INDgcProducts, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcProducts, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcProducts, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcProducts, False)
        Me.INDgcProducts.Location = New System.Drawing.Point(852, 89)
        Me.INDgcProducts.MainView = Me.INDGvProducts
        Me.INDgcProducts.Name = "INDgcProducts"
        Me.INDgcProducts.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDRiPcePhysicalInventory, Me.RepositoryItemTextEdit1})
        Me.INDgcProducts.Size = New System.Drawing.Size(824, 455)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcProducts, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcProducts.TabIndex = 11
        Me.INDgcProducts.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvProducts})
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
        Me.INDGvProducts.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDDescriptionProduct, Me.INDGcoInventoryQuantity, Me.INDGcoQuantity, Me.INDGcoDetailPhysical, Me.INDGcoCostProduct})
        Me.INDGvProducts.GridControl = Me.INDgcProducts
        Me.INDGvProducts.Name = "INDGvProducts"
        Me.INDGvProducts.OptionsCustomization.AllowGroup = False
        Me.INDGvProducts.OptionsDetail.EnableMasterViewMode = False
        Me.INDGvProducts.OptionsDetail.ShowDetailTabs = False
        Me.INDGvProducts.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvProducts.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvProducts.OptionsView.ShowAutoFilterRow = True
        Me.INDGvProducts.OptionsView.ShowDetailButtons = False
        Me.INDGvProducts.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvProducts, False)
        '
        'INDDescriptionProduct
        '
        Me.INDDescriptionProduct.Caption = "Producto"
        Me.INDDescriptionProduct.FieldName = "DescriptionProduct"
        Me.INDDescriptionProduct.Name = "INDDescriptionProduct"
        Me.INDDescriptionProduct.OptionsColumn.AllowEdit = False
        Me.INDDescriptionProduct.OptionsColumn.AllowFocus = False
        Me.INDDescriptionProduct.Visible = True
        Me.INDDescriptionProduct.VisibleIndex = 0
        Me.INDDescriptionProduct.Width = 325
        '
        'INDGcoInventoryQuantity
        '
        Me.INDGcoInventoryQuantity.Caption = "C. en Inventario Físico"
        Me.INDGcoInventoryQuantity.FieldName = "InventoryQuantity"
        Me.INDGcoInventoryQuantity.Name = "INDGcoInventoryQuantity"
        Me.INDGcoInventoryQuantity.OptionsColumn.AllowEdit = False
        Me.INDGcoInventoryQuantity.OptionsColumn.AllowFocus = False
        Me.INDGcoInventoryQuantity.Visible = True
        Me.INDGcoInventoryQuantity.VisibleIndex = 1
        Me.INDGcoInventoryQuantity.Width = 141
        '
        'INDGcoQuantity
        '
        Me.INDGcoQuantity.Caption = "C. A Despachar"
        Me.INDGcoQuantity.FieldName = "Quantity"
        Me.INDGcoQuantity.Name = "INDGcoQuantity"
        Me.INDGcoQuantity.OptionsColumn.AllowEdit = False
        Me.INDGcoQuantity.OptionsColumn.AllowFocus = False
        Me.INDGcoQuantity.Visible = True
        Me.INDGcoQuantity.VisibleIndex = 2
        Me.INDGcoQuantity.Width = 108
        '
        'INDGcoDetailPhysical
        '
        Me.INDGcoDetailPhysical.Caption = "Inventario Fisico"
        Me.INDGcoDetailPhysical.ColumnEdit = Me.INDRiPcePhysicalInventory
        Me.INDGcoDetailPhysical.Name = "INDGcoDetailPhysical"
        Me.INDGcoDetailPhysical.Visible = True
        Me.INDGcoDetailPhysical.VisibleIndex = 3
        Me.INDGcoDetailPhysical.Width = 132
        '
        'INDRiPcePhysicalInventory
        '
        Me.INDRiPcePhysicalInventory.AutoHeight = False
        Me.INDRiPcePhysicalInventory.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDRiPcePhysicalInventory.Name = "INDRiPcePhysicalInventory"
        Me.INDRiPcePhysicalInventory.PopupControl = Me.INDPccDetailPhysicalInventory
        Me.INDRiPcePhysicalInventory.PopupSizeable = False
        Me.INDRiPcePhysicalInventory.ShowPopupCloseButton = False
        Me.INDRiPcePhysicalInventory.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'INDGcoCostProduct
        '
        Me.INDGcoCostProduct.Caption = "Costo P."
        Me.INDGcoCostProduct.ColumnEdit = Me.RepositoryItemTextEdit1
        Me.INDGcoCostProduct.FieldName = "CostProduct"
        Me.INDGcoCostProduct.Name = "INDGcoCostProduct"
        Me.INDGcoCostProduct.OptionsColumn.AllowEdit = False
        Me.INDGcoCostProduct.OptionsColumn.AllowFocus = False
        '
        'RepositoryItemTextEdit1
        '
        Me.RepositoryItemTextEdit1.AutoHeight = False
        Me.RepositoryItemTextEdit1.DisplayFormat.FormatString = "c2"
        Me.RepositoryItemTextEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.RepositoryItemTextEdit1.Name = "RepositoryItemTextEdit1"
        '
        'INDBtnAdd
        '
        Me.INDBtnAdd.Enabled = False
        Me.INDBtnAdd.Location = New System.Drawing.Point(852, 53)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDBtnAdd, False)
        Me.INDBtnAdd.Name = "INDBtnAdd"
        Me.INDBtnAdd.Size = New System.Drawing.Size(824, 32)
        Me.INDBtnAdd.StyleController = Me.INDlcTransferOrder
        Me.INDBtnAdd.TabIndex = 10
        Me.INDBtnAdd.Text = "Agregar Producto"
        '
        'INDmeDescription
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDmeDescription, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDmeDescription, True)
        Me.INDmeDescription.EnterMoveNextControl = True
        Me.INDmeDescription.Location = New System.Drawing.Point(24, 270)
        Me.IndigoTextEdit1.SetMascara(Me.INDmeDescription, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDmeDescription.Name = "INDmeDescription"
        Me.INDmeDescription.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDmeDescription.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmeDescription.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDmeDescription.Properties.Appearance.Options.UseBackColor = True
        Me.INDmeDescription.Properties.Appearance.Options.UseFont = True
        Me.INDmeDescription.Properties.Appearance.Options.UseForeColor = True
        Me.INDmeDescription.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDmeDescription.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDmeDescription.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmeDescription.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDmeDescription.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDmeDescription.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDmeDescription.Properties.MaxLength = 300
        Me.INDmeDescription.Size = New System.Drawing.Size(386, 51)
        Me.INDmeDescription.StyleController = Me.INDlcTransferOrder
        Me.INDmeDescription.TabIndex = 9
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDmeDescription, 0)
        Me.INDmeDescription.ToolTip = "Este Campo es Necesario"
        '
        'INDsleThirdPartyId
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleThirdPartyId, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleThirdPartyId, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleThirdPartyId, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleThirdPartyId, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleThirdPartyId, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleThirdPartyId, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleThirdPartyId, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleThirdPartyId, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleThirdPartyId, False)
        Me.INDsleThirdPartyId.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleThirdPartyId, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleThirdPartyId, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleThirdPartyId, False)
        Me.INDsleThirdPartyId.Location = New System.Drawing.Point(438, 463)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleThirdPartyId, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleThirdPartyId.Name = "INDsleThirdPartyId"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleThirdPartyId, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleThirdPartyId, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleThirdPartyId, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDsleThirdPartyId, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleThirdPartyId, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleThirdPartyId, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleThirdPartyId, False)
        Me.INDsleThirdPartyId.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleThirdPartyId.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleThirdPartyId.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleThirdPartyId.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleThirdPartyId.Properties.Appearance.Options.UseFont = True
        Me.INDsleThirdPartyId.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleThirdPartyId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleThirdPartyId.Properties.DisplayMember = "NitName"
        Me.INDsleThirdPartyId.Properties.NullText = ""
        Me.INDsleThirdPartyId.Properties.PopupSizeable = False
        Me.INDsleThirdPartyId.Properties.PopupView = Me.GridView3
        Me.INDsleThirdPartyId.Properties.ShowFooter = False
        Me.INDsleThirdPartyId.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleThirdPartyId, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleThirdPartyId, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleThirdPartyId, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleThirdPartyId, True)
        Me.INDsleThirdPartyId.Size = New System.Drawing.Size(386, 28)
        Me.INDsleThirdPartyId.StyleController = Me.INDlcTransferOrder
        Me.INDsleThirdPartyId.TabIndex = 8
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleThirdPartyId, "532")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleThirdPartyId, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleThirdPartyId, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleThirdPartyId, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleThirdPartyId, False)
        '
        'GridView3
        '
        Me.GridView3.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView3.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView3.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView3.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView3.Appearance.FocusedRow.Options.UseForeColor = True
        Me.GridView3.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView3.Appearance.GroupRow.Options.UseFont = True
        Me.GridView3.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView3.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView3.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView3.Appearance.Row.Options.UseFont = True
        Me.GridView3.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn11, Me.GridColumn12})
        Me.GridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView3.Name = "GridView3"
        Me.GridView3.OptionsCustomization.AllowGroup = False
        Me.GridView3.OptionsDetail.EnableMasterViewMode = False
        Me.GridView3.OptionsDetail.ShowDetailTabs = False
        Me.GridView3.OptionsFind.AlwaysVisible = True
        Me.GridView3.OptionsFind.FindFilterColumns = "Nit"
        Me.GridView3.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView3.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView3.OptionsView.EnableAppearanceOddRow = True
        Me.GridView3.OptionsView.ShowAutoFilterRow = True
        Me.GridView3.OptionsView.ShowDetailButtons = False
        Me.GridView3.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView3, False)
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "Nit"
        Me.GridColumn11.FieldName = "Nit"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.OptionsColumn.AllowEdit = False
        Me.GridColumn11.Visible = True
        Me.GridColumn11.VisibleIndex = 0
        Me.GridColumn11.Width = 280
        '
        'GridColumn12
        '
        Me.GridColumn12.Caption = "Nombre"
        Me.GridColumn12.FieldName = "Name"
        Me.GridColumn12.Name = "GridColumn12"
        Me.GridColumn12.OptionsColumn.AllowEdit = False
        Me.GridColumn12.Visible = True
        Me.GridColumn12.VisibleIndex = 1
        Me.GridColumn12.Width = 878
        '
        'INDsleAdjustmentConceptId
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleAdjustmentConceptId, AppearanceObject5)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleAdjustmentConceptId, AppearanceObject6)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleAdjustmentConceptId, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleAdjustmentConceptId, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleAdjustmentConceptId, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleAdjustmentConceptId, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleAdjustmentConceptId, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleAdjustmentConceptId, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleAdjustmentConceptId, False)
        Me.INDsleAdjustmentConceptId.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleAdjustmentConceptId, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleAdjustmentConceptId, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleAdjustmentConceptId, False)
        Me.INDsleAdjustmentConceptId.Location = New System.Drawing.Point(438, 399)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleAdjustmentConceptId, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleAdjustmentConceptId.Name = "INDsleAdjustmentConceptId"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleAdjustmentConceptId, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleAdjustmentConceptId, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleAdjustmentConceptId, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDsleAdjustmentConceptId, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleAdjustmentConceptId, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleAdjustmentConceptId, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleAdjustmentConceptId, False)
        Me.INDsleAdjustmentConceptId.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleAdjustmentConceptId.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleAdjustmentConceptId.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleAdjustmentConceptId.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleAdjustmentConceptId.Properties.Appearance.Options.UseFont = True
        Me.INDsleAdjustmentConceptId.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleAdjustmentConceptId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleAdjustmentConceptId.Properties.DisplayMember = "CodeName"
        Me.INDsleAdjustmentConceptId.Properties.NullText = ""
        Me.INDsleAdjustmentConceptId.Properties.PopupSizeable = False
        Me.INDsleAdjustmentConceptId.Properties.PopupView = Me.GridView2
        Me.INDsleAdjustmentConceptId.Properties.ShowFooter = False
        Me.INDsleAdjustmentConceptId.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleAdjustmentConceptId, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleAdjustmentConceptId, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleAdjustmentConceptId, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleAdjustmentConceptId, True)
        Me.INDsleAdjustmentConceptId.Size = New System.Drawing.Size(386, 28)
        Me.INDsleAdjustmentConceptId.StyleController = Me.INDlcTransferOrder
        Me.INDsleAdjustmentConceptId.TabIndex = 7
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleAdjustmentConceptId, "307")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleAdjustmentConceptId, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleAdjustmentConceptId, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleAdjustmentConceptId, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleAdjustmentConceptId, False)
        '
        'GridView2
        '
        Me.GridView2.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView2.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView2.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView2.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView2.Appearance.FocusedRow.Options.UseForeColor = True
        Me.GridView2.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView2.Appearance.GroupRow.Options.UseFont = True
        Me.GridView2.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView2.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView2.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView2.Appearance.Row.Options.UseFont = True
        Me.GridView2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn9, Me.GridColumn10})
        Me.GridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsCustomization.AllowGroup = False
        Me.GridView2.OptionsDetail.EnableMasterViewMode = False
        Me.GridView2.OptionsDetail.ShowDetailTabs = False
        Me.GridView2.OptionsFind.AlwaysVisible = True
        Me.GridView2.OptionsFind.FindFilterColumns = "Code"
        Me.GridView2.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView2.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView2.OptionsView.EnableAppearanceOddRow = True
        Me.GridView2.OptionsView.ShowAutoFilterRow = True
        Me.GridView2.OptionsView.ShowDetailButtons = False
        Me.GridView2.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView2, False)
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Código"
        Me.GridColumn9.FieldName = "Code"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.OptionsColumn.AllowEdit = False
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 0
        Me.GridColumn9.Width = 246
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "Descripción"
        Me.GridColumn10.FieldName = "Name"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.OptionsColumn.AllowEdit = False
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 1
        Me.GridColumn10.Width = 912
        '
        'INDsleTargetFunctionalUnitId
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleTargetFunctionalUnitId, AppearanceObject7)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleTargetFunctionalUnitId, AppearanceObject8)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleTargetFunctionalUnitId, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleTargetFunctionalUnitId, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleTargetFunctionalUnitId, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleTargetFunctionalUnitId, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleTargetFunctionalUnitId, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleTargetFunctionalUnitId, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleTargetFunctionalUnitId, False)
        Me.INDsleTargetFunctionalUnitId.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleTargetFunctionalUnitId, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleTargetFunctionalUnitId, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleTargetFunctionalUnitId, False)
        Me.INDsleTargetFunctionalUnitId.Location = New System.Drawing.Point(438, 335)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleTargetFunctionalUnitId, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleTargetFunctionalUnitId.Name = "INDsleTargetFunctionalUnitId"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleTargetFunctionalUnitId, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleTargetFunctionalUnitId, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleTargetFunctionalUnitId, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDsleTargetFunctionalUnitId, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleTargetFunctionalUnitId, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleTargetFunctionalUnitId, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleTargetFunctionalUnitId, False)
        Me.INDsleTargetFunctionalUnitId.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleTargetFunctionalUnitId.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleTargetFunctionalUnitId.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleTargetFunctionalUnitId.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleTargetFunctionalUnitId.Properties.Appearance.Options.UseFont = True
        Me.INDsleTargetFunctionalUnitId.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleTargetFunctionalUnitId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleTargetFunctionalUnitId.Properties.DisplayMember = "CodeDescription"
        Me.INDsleTargetFunctionalUnitId.Properties.NullText = ""
        Me.INDsleTargetFunctionalUnitId.Properties.PopupSizeable = False
        Me.INDsleTargetFunctionalUnitId.Properties.PopupView = Me.GridView1
        Me.INDsleTargetFunctionalUnitId.Properties.ShowFooter = False
        Me.INDsleTargetFunctionalUnitId.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleTargetFunctionalUnitId, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleTargetFunctionalUnitId, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleTargetFunctionalUnitId, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleTargetFunctionalUnitId, True)
        Me.INDsleTargetFunctionalUnitId.Size = New System.Drawing.Size(386, 28)
        Me.INDsleTargetFunctionalUnitId.StyleController = Me.INDlcTransferOrder
        Me.INDsleTargetFunctionalUnitId.TabIndex = 6
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleTargetFunctionalUnitId, "523")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleTargetFunctionalUnitId, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleTargetFunctionalUnitId, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleTargetFunctionalUnitId, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleTargetFunctionalUnitId, False)
        '
        'GridView1
        '
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
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn7, Me.GridColumn8})
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsCustomization.AllowGroup = False
        Me.GridView1.OptionsDetail.EnableMasterViewMode = False
        Me.GridView1.OptionsDetail.ShowDetailTabs = False
        Me.GridView1.OptionsFind.AlwaysVisible = True
        Me.GridView1.OptionsFind.FindFilterColumns = "Codigo"
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowDetailButtons = False
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView1, False)
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Código"
        Me.GridColumn7.FieldName = "Codigo"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsColumn.AllowEdit = False
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 0
        Me.GridColumn7.Width = 278
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Descripción"
        Me.GridColumn8.FieldName = "Descripcion"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.OptionsColumn.AllowEdit = False
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 1
        Me.GridColumn8.Width = 880
        '
        'INDsleTargetWarehouseId
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleTargetWarehouseId, AppearanceObject9)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleTargetWarehouseId, AppearanceObject10)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleTargetWarehouseId, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleTargetWarehouseId, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleTargetWarehouseId, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleTargetWarehouseId, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleTargetWarehouseId, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleTargetWarehouseId, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleTargetWarehouseId, False)
        Me.INDsleTargetWarehouseId.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleTargetWarehouseId, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleTargetWarehouseId, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleTargetWarehouseId, False)
        Me.INDsleTargetWarehouseId.Location = New System.Drawing.Point(438, 271)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleTargetWarehouseId, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleTargetWarehouseId.Name = "INDsleTargetWarehouseId"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleTargetWarehouseId, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleTargetWarehouseId, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleTargetWarehouseId, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDsleTargetWarehouseId, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleTargetWarehouseId, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleTargetWarehouseId, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleTargetWarehouseId, False)
        Me.INDsleTargetWarehouseId.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleTargetWarehouseId.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleTargetWarehouseId.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleTargetWarehouseId.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleTargetWarehouseId.Properties.Appearance.Options.UseFont = True
        Me.INDsleTargetWarehouseId.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleTargetWarehouseId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleTargetWarehouseId.Properties.DisplayMember = "CodeName"
        Me.INDsleTargetWarehouseId.Properties.NullText = ""
        Me.INDsleTargetWarehouseId.Properties.PopupSizeable = False
        Me.INDsleTargetWarehouseId.Properties.PopupView = Me.SearchLookUpEdit4View
        Me.INDsleTargetWarehouseId.Properties.ShowFooter = False
        Me.INDsleTargetWarehouseId.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleTargetWarehouseId, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleTargetWarehouseId, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleTargetWarehouseId, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleTargetWarehouseId, True)
        Me.INDsleTargetWarehouseId.Size = New System.Drawing.Size(386, 28)
        Me.INDsleTargetWarehouseId.StyleController = Me.INDlcTransferOrder
        Me.INDsleTargetWarehouseId.TabIndex = 5
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleTargetWarehouseId, "302")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleTargetWarehouseId, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleTargetWarehouseId, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleTargetWarehouseId, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleTargetWarehouseId, False)
        '
        'SearchLookUpEdit4View
        '
        Me.SearchLookUpEdit4View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit4View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit4View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit4View.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit4View.Appearance.FocusedRow.Options.UseForeColor = True
        Me.SearchLookUpEdit4View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit4View.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit4View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit4View.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit4View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit4View.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit4View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn5, Me.GridColumn6})
        Me.SearchLookUpEdit4View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit4View.Name = "SearchLookUpEdit4View"
        Me.SearchLookUpEdit4View.OptionsCustomization.AllowGroup = False
        Me.SearchLookUpEdit4View.OptionsDetail.EnableMasterViewMode = False
        Me.SearchLookUpEdit4View.OptionsDetail.ShowDetailTabs = False
        Me.SearchLookUpEdit4View.OptionsFind.AlwaysVisible = True
        Me.SearchLookUpEdit4View.OptionsFind.FindFilterColumns = "Code"
        Me.SearchLookUpEdit4View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit4View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit4View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit4View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit4View.OptionsView.ShowDetailButtons = False
        Me.SearchLookUpEdit4View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit4View, False)
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Código"
        Me.GridColumn5.FieldName = "Code"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.AllowEdit = False
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 0
        Me.GridColumn5.Width = 259
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Nombre"
        Me.GridColumn6.FieldName = "Name"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.AllowEdit = False
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 1
        Me.GridColumn6.Width = 899
        '
        'INDsleSourceWarehouseId
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleSourceWarehouseId, AppearanceObject11)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleSourceWarehouseId, AppearanceObject12)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleSourceWarehouseId, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleSourceWarehouseId, True)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleSourceWarehouseId, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleSourceWarehouseId, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleSourceWarehouseId, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleSourceWarehouseId, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleSourceWarehouseId, False)
        Me.INDsleSourceWarehouseId.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleSourceWarehouseId, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleSourceWarehouseId, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleSourceWarehouseId, False)
        Me.INDsleSourceWarehouseId.Location = New System.Drawing.Point(438, 79)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleSourceWarehouseId, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleSourceWarehouseId.Name = "INDsleSourceWarehouseId"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleSourceWarehouseId, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleSourceWarehouseId, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleSourceWarehouseId, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDsleSourceWarehouseId, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleSourceWarehouseId, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleSourceWarehouseId, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleSourceWarehouseId, False)
        Me.INDsleSourceWarehouseId.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDsleSourceWarehouseId.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleSourceWarehouseId.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleSourceWarehouseId.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleSourceWarehouseId.Properties.Appearance.Options.UseFont = True
        Me.INDsleSourceWarehouseId.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleSourceWarehouseId.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleSourceWarehouseId.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleSourceWarehouseId.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleSourceWarehouseId.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleSourceWarehouseId.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleSourceWarehouseId.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleSourceWarehouseId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleSourceWarehouseId.Properties.DisplayMember = "CodeName"
        Me.INDsleSourceWarehouseId.Properties.NullText = ""
        Me.INDsleSourceWarehouseId.Properties.PopupSizeable = False
        Me.INDsleSourceWarehouseId.Properties.PopupView = Me.INDGdvSourceWarehouseId
        Me.INDsleSourceWarehouseId.Properties.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDRptIsConsignment})
        Me.INDsleSourceWarehouseId.Properties.ShowFooter = False
        Me.INDsleSourceWarehouseId.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleSourceWarehouseId, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleSourceWarehouseId, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleSourceWarehouseId, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleSourceWarehouseId, True)
        Me.INDsleSourceWarehouseId.Size = New System.Drawing.Size(386, 28)
        Me.INDsleSourceWarehouseId.StyleController = Me.INDlcTransferOrder
        Me.INDsleSourceWarehouseId.TabIndex = 4
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleSourceWarehouseId, "302")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleSourceWarehouseId, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleSourceWarehouseId, "{0} - {1}")
        Me.INDsleSourceWarehouseId.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleSourceWarehouseId, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleSourceWarehouseId, False)
        '
        'INDGdvSourceWarehouseId
        '
        Me.INDGdvSourceWarehouseId.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGdvSourceWarehouseId.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGdvSourceWarehouseId.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGdvSourceWarehouseId.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGdvSourceWarehouseId.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGdvSourceWarehouseId.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGdvSourceWarehouseId.Appearance.GroupRow.Options.UseFont = True
        Me.INDGdvSourceWarehouseId.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGdvSourceWarehouseId.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGdvSourceWarehouseId.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGdvSourceWarehouseId.Appearance.Row.Options.UseFont = True
        Me.INDGdvSourceWarehouseId.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3, Me.GridColumn4, Me.INDColInConsignment})
        Me.INDGdvSourceWarehouseId.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGdvSourceWarehouseId.Name = "INDGdvSourceWarehouseId"
        Me.INDGdvSourceWarehouseId.OptionsCustomization.AllowGroup = False
        Me.INDGdvSourceWarehouseId.OptionsDetail.EnableMasterViewMode = False
        Me.INDGdvSourceWarehouseId.OptionsDetail.ShowDetailTabs = False
        Me.INDGdvSourceWarehouseId.OptionsFind.AlwaysVisible = True
        Me.INDGdvSourceWarehouseId.OptionsFind.FindFilterColumns = "Code"
        Me.INDGdvSourceWarehouseId.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGdvSourceWarehouseId.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGdvSourceWarehouseId.OptionsView.EnableAppearanceOddRow = True
        Me.INDGdvSourceWarehouseId.OptionsView.ShowAutoFilterRow = True
        Me.INDGdvSourceWarehouseId.OptionsView.ShowDetailButtons = False
        Me.INDGdvSourceWarehouseId.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGdvSourceWarehouseId, False)
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Código"
        Me.GridColumn3.FieldName = "Code"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 0
        Me.GridColumn3.Width = 251
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Nombre"
        Me.GridColumn4.FieldName = "Name"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 1
        Me.GridColumn4.Width = 645
        '
        'INDColInConsignment
        '
        Me.INDColInConsignment.Caption = "En Consignación"
        Me.INDColInConsignment.ColumnEdit = Me.INDRptIsConsignment
        Me.INDColInConsignment.FieldName = "WarehouseConsignment"
        Me.INDColInConsignment.Name = "INDColInConsignment"
        Me.INDColInConsignment.OptionsColumn.AllowEdit = False
        Me.INDColInConsignment.OptionsColumn.AllowFocus = False
        Me.INDColInConsignment.Visible = True
        Me.INDColInConsignment.VisibleIndex = 2
        Me.INDColInConsignment.Width = 262
        '
        'INDRptIsConsignment
        '
        Me.INDRptIsConsignment.AutoHeight = False
        Me.INDRptIsConsignment.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDRptIsConsignment.DisplayMember = "Item2"
        Me.INDRptIsConsignment.Name = "INDRptIsConsignment"
        Me.INDRptIsConsignment.PopupView = Me.RepositoryItemGridLookUpEdit1View
        Me.INDRptIsConsignment.ValueMember = "Item1"
        '
        'RepositoryItemGridLookUpEdit1View
        '
        Me.RepositoryItemGridLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.RepositoryItemGridLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.RepositoryItemGridLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.RepositoryItemGridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.RepositoryItemGridLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RepositoryItemGridLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.RepositoryItemGridLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RepositoryItemGridLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.RepositoryItemGridLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.RepositoryItemGridLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.RepositoryItemGridLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn16})
        Me.RepositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.RepositoryItemGridLookUpEdit1View.Name = "RepositoryItemGridLookUpEdit1View"
        Me.RepositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.RepositoryItemGridLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.RepositoryItemGridLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.RepositoryItemGridLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.RepositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.RepositoryItemGridLookUpEdit1View, False)
        '
        'GridColumn16
        '
        Me.GridColumn16.Caption = "GridColumn16"
        Me.GridColumn16.FieldName = "Item2"
        Me.GridColumn16.Name = "GridColumn16"
        Me.GridColumn16.Visible = True
        Me.GridColumn16.VisibleIndex = 0
        '
        'INDsleDispatchTo
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleDispatchTo, AppearanceObject13)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleDispatchTo, AppearanceObject14)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleDispatchTo, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleDispatchTo, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleDispatchTo, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleDispatchTo, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleDispatchTo, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleDispatchTo, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleDispatchTo, False)
        Me.INDsleDispatchTo.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleDispatchTo, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleDispatchTo, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleDispatchTo, False)
        Me.INDsleDispatchTo.Location = New System.Drawing.Point(438, 143)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleDispatchTo, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleDispatchTo.Name = "INDsleDispatchTo"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleDispatchTo, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleDispatchTo, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleDispatchTo, False)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDsleDispatchTo, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleDispatchTo, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleDispatchTo, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleDispatchTo, False)
        Me.INDsleDispatchTo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleDispatchTo.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleDispatchTo.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleDispatchTo.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleDispatchTo.Properties.Appearance.Options.UseFont = True
        Me.INDsleDispatchTo.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleDispatchTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleDispatchTo.Properties.DisplayMember = "Item2"
        Me.INDsleDispatchTo.Properties.NullText = ""
        Me.INDsleDispatchTo.Properties.PopupSizeable = False
        Me.INDsleDispatchTo.Properties.PopupView = Me.SearchLookUpEdit2View
        Me.INDsleDispatchTo.Properties.ShowFooter = False
        Me.INDsleDispatchTo.Properties.ValueMember = "Item1"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleDispatchTo, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleDispatchTo, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleDispatchTo, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleDispatchTo, True)
        Me.INDsleDispatchTo.Size = New System.Drawing.Size(386, 28)
        Me.INDsleDispatchTo.StyleController = Me.INDlcTransferOrder
        Me.INDsleDispatchTo.TabIndex = 3
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleDispatchTo, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleDispatchTo, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleDispatchTo, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleDispatchTo, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleDispatchTo, False)
        '
        'SearchLookUpEdit2View
        '
        Me.SearchLookUpEdit2View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit2View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit2View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit2View.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit2View.Appearance.FocusedRow.Options.UseForeColor = True
        Me.SearchLookUpEdit2View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit2View.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit2View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit2View.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit2View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit2View.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit2View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn2})
        Me.SearchLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit2View.Name = "SearchLookUpEdit2View"
        Me.SearchLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit2View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit2View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit2View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit2View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit2View, False)
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Descripción"
        Me.GridColumn2.FieldName = "Item2"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        '
        'INDsleOrderType
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleOrderType, AppearanceObject15)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleOrderType, AppearanceObject16)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleOrderType, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleOrderType, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleOrderType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleOrderType, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleOrderType, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleOrderType, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleOrderType, False)
        Me.INDsleOrderType.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleOrderType, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleOrderType, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleOrderType, False)
        Me.INDsleOrderType.Location = New System.Drawing.Point(24, 207)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleOrderType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleOrderType.Name = "INDsleOrderType"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleOrderType, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleOrderType, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleOrderType, False)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDsleOrderType, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleOrderType, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleOrderType, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleOrderType, False)
        Me.INDsleOrderType.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDsleOrderType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleOrderType.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleOrderType.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleOrderType.Properties.Appearance.Options.UseFont = True
        Me.INDsleOrderType.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleOrderType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleOrderType.Properties.DisplayMember = "Item2"
        Me.INDsleOrderType.Properties.NullText = ""
        Me.INDsleOrderType.Properties.PopupSizeable = False
        Me.INDsleOrderType.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDsleOrderType.Properties.ShowFooter = False
        Me.INDsleOrderType.Properties.ValueMember = "Item1"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleOrderType, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleOrderType, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleOrderType, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleOrderType, True)
        Me.INDsleOrderType.Size = New System.Drawing.Size(386, 28)
        Me.INDsleOrderType.StyleController = Me.INDlcTransferOrder
        Me.INDsleOrderType.TabIndex = 2
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleOrderType, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleOrderType, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleOrderType, "{0} - {1}")
        Me.INDsleOrderType.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleOrderType, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleOrderType, False)
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
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1})
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
        Me.GridColumn1.Caption = "Descripción"
        Me.GridColumn1.FieldName = "Item2"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'INDdeDocumentDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdeDocumentDate, True)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDdeDocumentDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdeDocumentDate, True)
        Me.INDdeDocumentDate.EditValue = Nothing
        Me.INDdeDocumentDate.EnterMoveNextControl = True
        Me.INDdeDocumentDate.Location = New System.Drawing.Point(24, 143)
        Me.IndigoTextEdit1.SetMascara(Me.INDdeDocumentDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDdeDocumentDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDdeDocumentDate.Name = "INDdeDocumentDate"
        Me.INDdeDocumentDate.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDdeDocumentDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdeDocumentDate.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDdeDocumentDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDdeDocumentDate.Properties.Appearance.Options.UseFont = True
        Me.INDdeDocumentDate.Properties.Appearance.Options.UseForeColor = True
        Me.INDdeDocumentDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDdeDocumentDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDdeDocumentDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdeDocumentDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDdeDocumentDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDdeDocumentDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdeDocumentDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeDocumentDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeDocumentDate.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.INDdeDocumentDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDdeDocumentDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdeDocumentDate.Size = New System.Drawing.Size(386, 28)
        Me.INDdeDocumentDate.StyleController = Me.INDlcTransferOrder
        Me.INDdeDocumentDate.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdeDocumentDate, 0)
        Me.INDdeDocumentDate.ToolTip = "Este Campo es Necesario"
        '
        'INDbtnCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbtnCode, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbtnCode, True)
        Me.INDbtnCode.Location = New System.Drawing.Point(24, 79)
        Me.IndigoTextEdit1.SetMascara(Me.INDbtnCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDbtnCode.Name = "INDbtnCode"
        Me.INDbtnCode.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDbtnCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDbtnCode.Properties.Appearance.Options.UseFont = True
        Me.INDbtnCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDbtnCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDbtnCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseFont = True
        EditorButtonImageOptions1.Image = Global.Presentation.Inventory.My.Resources.Resources.BuscarMetro
        Me.INDbtnCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDbtnCode.Properties.MaxLength = 20
        Me.INDbtnCode.Size = New System.Drawing.Size(386, 28)
        Me.INDbtnCode.StyleController = Me.INDlcTransferOrder
        Me.INDbtnCode.TabIndex = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbtnCode, 0)
        Me.INDbtnCode.ToolTip = "Este Campo es Necesario"
        '
        'INDlcgtransferOrder
        '
        Me.INDlcgtransferOrder.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgtransferOrder.AppearanceGroup.Options.UseFont = True
        Me.INDlcgtransferOrder.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgtransferOrder.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgtransferOrder.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgtransferOrder.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgtransferOrder.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgtransferOrder.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgtransferOrder.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgtransferOrder.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgtransferOrder.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgtransferOrder.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgtransferOrder.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgtransferOrder.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgtransferOrder, False)
        Me.INDlcgtransferOrder.CustomizationFormText = "Orden de Traslado"
        Me.INDlcgtransferOrder.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlcgtransferOrder.GroupBordersVisible = False
        Me.INDlcgtransferOrder.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlcgGeneralData, Me.INDlcgDataOptional, Me.INDLcgProducts})
        Me.INDlcgtransferOrder.Name = "Root"
        Me.INDlcgtransferOrder.Size = New System.Drawing.Size(1700, 568)
        Me.INDlcgtransferOrder.TextVisible = False
        '
        'INDlcgGeneralData
        '
        Me.INDlcgGeneralData.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgGeneralData.AppearanceGroup.Options.UseFont = True
        Me.INDlcgGeneralData.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgGeneralData.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgGeneralData.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgGeneralData.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgGeneralData.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgGeneralData.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgGeneralData.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgGeneralData.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgGeneralData.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgGeneralData.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgGeneralData.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgGeneralData.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgGeneralData, False)
        Me.INDlcgGeneralData.CustomizationFormText = "Datos Principales"
        Me.INDlcgGeneralData.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciCode, Me.INDlciDocumentDate, Me.INDlciOrderType, Me.INDlciDescription})
        Me.INDlcgGeneralData.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgGeneralData.Name = "INDlcgGeneralData"
        Me.INDlcgGeneralData.Size = New System.Drawing.Size(414, 548)
        Me.INDlcgGeneralData.Text = "Datos Principales"
        '
        'INDlciCode
        '
        Me.INDlciCode.Control = Me.INDbtnCode
        Me.INDlciCode.CustomizationFormText = "Código"
        Me.INDlciCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlciCode.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciCode.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciCode.Name = "INDlciCode"
        Me.INDlciCode.ShowInCustomizationForm = False
        Me.INDlciCode.Size = New System.Drawing.Size(390, 64)
        Me.INDlciCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciCode.Text = "Código"
        Me.INDlciCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciCode.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciCode.TextToControlDistance = 5
        '
        'INDlciDocumentDate
        '
        Me.INDlciDocumentDate.Control = Me.INDdeDocumentDate
        Me.INDlciDocumentDate.CustomizationFormText = "Fecha Documento"
        Me.INDlciDocumentDate.Location = New System.Drawing.Point(0, 64)
        Me.INDlciDocumentDate.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciDocumentDate.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciDocumentDate.Name = "INDlciDocumentDate"
        Me.INDlciDocumentDate.ShowInCustomizationForm = False
        Me.INDlciDocumentDate.Size = New System.Drawing.Size(390, 64)
        Me.INDlciDocumentDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciDocumentDate.Text = "Fecha Documento"
        Me.INDlciDocumentDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciDocumentDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciDocumentDate.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciDocumentDate.TextToControlDistance = 5
        '
        'INDlciOrderType
        '
        Me.INDlciOrderType.Control = Me.INDsleOrderType
        Me.INDlciOrderType.CustomizationFormText = "Tipo"
        Me.INDlciOrderType.Location = New System.Drawing.Point(0, 128)
        Me.INDlciOrderType.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciOrderType.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciOrderType.Name = "INDlciOrderType"
        Me.INDlciOrderType.ShowInCustomizationForm = False
        Me.INDlciOrderType.Size = New System.Drawing.Size(390, 64)
        Me.INDlciOrderType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciOrderType.Text = "Tipo"
        Me.INDlciOrderType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciOrderType.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciOrderType.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciOrderType.TextToControlDistance = 5
        '
        'INDlciDescription
        '
        Me.INDlciDescription.AllowHide = False
        Me.INDlciDescription.Control = Me.INDmeDescription
        Me.INDlciDescription.CustomizationFormText = "Descripción"
        Me.INDlciDescription.Location = New System.Drawing.Point(0, 192)
        Me.INDlciDescription.MaxSize = New System.Drawing.Size(390, 80)
        Me.INDlciDescription.MinSize = New System.Drawing.Size(390, 80)
        Me.INDlciDescription.Name = "INDlciDescription"
        Me.INDlciDescription.ShowInCustomizationForm = False
        Me.INDlciDescription.Size = New System.Drawing.Size(390, 303)
        Me.INDlciDescription.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciDescription.Text = "Descripción"
        Me.INDlciDescription.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciDescription.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciDescription.TextSize = New System.Drawing.Size(50, 20)
        Me.INDlciDescription.TextToControlDistance = 5
        '
        'INDlcgDataOptional
        '
        Me.INDlcgDataOptional.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgDataOptional.AppearanceGroup.Options.UseFont = True
        Me.INDlcgDataOptional.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgDataOptional.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgDataOptional.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgDataOptional.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgDataOptional.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgDataOptional.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgDataOptional.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgDataOptional.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgDataOptional.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgDataOptional.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgDataOptional.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgDataOptional.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgDataOptional, False)
        Me.INDlcgDataOptional.CustomizationFormText = "Información Relacionada"
        Me.INDlcgDataOptional.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciThirdPartyId, Me.INDlciTargetWarehouseId, Me.INDlciTargetFunctionalUnitId, Me.INDlciAdjustmentConceptId, Me.INDlciTransitWarehouseId, Me.INDlciDispatchTo, Me.INDlciSourceWarehouseId})
        Me.INDlcgDataOptional.Location = New System.Drawing.Point(414, 0)
        Me.INDlcgDataOptional.Name = "INDlcgDataOptional"
        Me.INDlcgDataOptional.Size = New System.Drawing.Size(414, 548)
        Me.INDlcgDataOptional.Text = "Información Relacionada"
        '
        'INDlciThirdPartyId
        '
        Me.INDlciThirdPartyId.Control = Me.INDsleThirdPartyId
        Me.INDlciThirdPartyId.CustomizationFormText = "Tercero"
        Me.INDlciThirdPartyId.Location = New System.Drawing.Point(0, 384)
        Me.INDlciThirdPartyId.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciThirdPartyId.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciThirdPartyId.Name = "INDlciThirdPartyId"
        Me.INDlciThirdPartyId.Size = New System.Drawing.Size(390, 111)
        Me.INDlciThirdPartyId.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciThirdPartyId.Text = "Tercero"
        Me.INDlciThirdPartyId.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciThirdPartyId.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciThirdPartyId.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciThirdPartyId.TextToControlDistance = 5
        Me.INDlciThirdPartyId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlciTargetWarehouseId
        '
        Me.INDlciTargetWarehouseId.Control = Me.INDsleTargetWarehouseId
        Me.INDlciTargetWarehouseId.CustomizationFormText = "Almacén Destino"
        Me.INDlciTargetWarehouseId.Location = New System.Drawing.Point(0, 192)
        Me.INDlciTargetWarehouseId.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciTargetWarehouseId.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciTargetWarehouseId.Name = "INDlciTargetWarehouseId"
        Me.INDlciTargetWarehouseId.Size = New System.Drawing.Size(390, 64)
        Me.INDlciTargetWarehouseId.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciTargetWarehouseId.Text = "Almacén Destino"
        Me.INDlciTargetWarehouseId.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciTargetWarehouseId.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciTargetWarehouseId.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciTargetWarehouseId.TextToControlDistance = 5
        Me.INDlciTargetWarehouseId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlciTargetFunctionalUnitId
        '
        Me.INDlciTargetFunctionalUnitId.Control = Me.INDsleTargetFunctionalUnitId
        Me.INDlciTargetFunctionalUnitId.CustomizationFormText = "Unidad Funcional"
        Me.INDlciTargetFunctionalUnitId.Location = New System.Drawing.Point(0, 256)
        Me.INDlciTargetFunctionalUnitId.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciTargetFunctionalUnitId.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciTargetFunctionalUnitId.Name = "INDlciTargetFunctionalUnitId"
        Me.INDlciTargetFunctionalUnitId.Size = New System.Drawing.Size(390, 64)
        Me.INDlciTargetFunctionalUnitId.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciTargetFunctionalUnitId.Text = "Unidad Funcional"
        Me.INDlciTargetFunctionalUnitId.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciTargetFunctionalUnitId.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciTargetFunctionalUnitId.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciTargetFunctionalUnitId.TextToControlDistance = 5
        Me.INDlciTargetFunctionalUnitId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlciAdjustmentConceptId
        '
        Me.INDlciAdjustmentConceptId.Control = Me.INDsleAdjustmentConceptId
        Me.INDlciAdjustmentConceptId.CustomizationFormText = "Concepto"
        Me.INDlciAdjustmentConceptId.Location = New System.Drawing.Point(0, 320)
        Me.INDlciAdjustmentConceptId.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciAdjustmentConceptId.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciAdjustmentConceptId.Name = "INDlciAdjustmentConceptId"
        Me.INDlciAdjustmentConceptId.Size = New System.Drawing.Size(390, 64)
        Me.INDlciAdjustmentConceptId.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciAdjustmentConceptId.Text = "Concepto"
        Me.INDlciAdjustmentConceptId.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciAdjustmentConceptId.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciAdjustmentConceptId.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciAdjustmentConceptId.TextToControlDistance = 5
        Me.INDlciAdjustmentConceptId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlciTransitWarehouseId
        '
        Me.INDlciTransitWarehouseId.Control = Me.INDsleTransitWarehouseId
        Me.INDlciTransitWarehouseId.Location = New System.Drawing.Point(0, 128)
        Me.INDlciTransitWarehouseId.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciTransitWarehouseId.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciTransitWarehouseId.Name = "INDlciTransitWarehouseId"
        Me.INDlciTransitWarehouseId.Size = New System.Drawing.Size(390, 64)
        Me.INDlciTransitWarehouseId.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciTransitWarehouseId.Text = "Almacén de Tránsito"
        Me.INDlciTransitWarehouseId.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciTransitWarehouseId.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciTransitWarehouseId.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciTransitWarehouseId.TextToControlDistance = 5
        Me.INDlciTransitWarehouseId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlciDispatchTo
        '
        Me.INDlciDispatchTo.Control = Me.INDsleDispatchTo
        Me.INDlciDispatchTo.CustomizationFormText = "Despachar a"
        Me.INDlciDispatchTo.Location = New System.Drawing.Point(0, 64)
        Me.INDlciDispatchTo.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciDispatchTo.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciDispatchTo.Name = "INDlciDispatchTo"
        Me.INDlciDispatchTo.Size = New System.Drawing.Size(390, 64)
        Me.INDlciDispatchTo.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciDispatchTo.Text = "Despachar a"
        Me.INDlciDispatchTo.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciDispatchTo.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciDispatchTo.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciDispatchTo.TextToControlDistance = 5
        Me.INDlciDispatchTo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlciSourceWarehouseId
        '
        Me.INDlciSourceWarehouseId.Control = Me.INDsleSourceWarehouseId
        Me.INDlciSourceWarehouseId.CustomizationFormText = "Almacén Origen"
        Me.INDlciSourceWarehouseId.Location = New System.Drawing.Point(0, 0)
        Me.INDlciSourceWarehouseId.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciSourceWarehouseId.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciSourceWarehouseId.Name = "INDlciSourceWarehouseId"
        Me.INDlciSourceWarehouseId.ShowInCustomizationForm = False
        Me.INDlciSourceWarehouseId.Size = New System.Drawing.Size(390, 64)
        Me.INDlciSourceWarehouseId.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciSourceWarehouseId.Text = "Almacén Origen"
        Me.INDlciSourceWarehouseId.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciSourceWarehouseId.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciSourceWarehouseId.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciSourceWarehouseId.TextToControlDistance = 5
        '
        'INDLcgProducts
        '
        Me.INDLcgProducts.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgProducts.AppearanceGroup.Options.UseFont = True
        Me.INDLcgProducts.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgProducts.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgProducts.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgProducts.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgProducts.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgProducts.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgProducts.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgProducts.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgProducts.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgProducts.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgProducts.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgProducts.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgProducts, False)
        Me.INDLcgProducts.CustomizationFormText = "Listado de Productos"
        Me.INDLcgProducts.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2})
        Me.INDLcgProducts.Location = New System.Drawing.Point(828, 0)
        Me.INDLcgProducts.Name = "INDLcgProducts"
        Me.INDLcgProducts.Size = New System.Drawing.Size(852, 548)
        Me.INDLcgProducts.Text = "Listado de Productos"
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDBtnAdd
        Me.LayoutControlItem1.CustomizationFormText = "Agregar Producto"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(828, 36)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(828, 36)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(828, 36)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextToControlDistance = 0
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDgcProducts
        Me.LayoutControlItem2.CustomizationFormText = "Productos"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 36)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(828, 0)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(828, 1)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(828, 459)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextToControlDistance = 0
        Me.LayoutControlItem2.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'FrmTransferOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1298, 729)
        Me.IconOptions.ShowIcon = False
        Me.Name = "FrmTransferOrder"
        Me.Opacity = 1.0R
        Me.Tag = "1519"
        Me.Text = "Orden de Traslado"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcTransferOrder, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlcTransferOrder.ResumeLayout(False)
        CType(Me.INDsleTransitWarehouseId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPccDetailPhysicalInventory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPccDetailPhysicalInventory.ResumeLayout(False)
        CType(Me.INDGcDetailPhysicalInventory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvDetailPhysicalInventory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcProducts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvProducts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRiPcePhysicalInventory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDmeDescription.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleThirdPartyId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleAdjustmentConceptId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleTargetFunctionalUnitId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleTargetWarehouseId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit4View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleSourceWarehouseId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGdvSourceWarehouseId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRptIsConsignment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemGridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleDispatchTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit2View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleOrderType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdeDocumentDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdeDocumentDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgtransferOrder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgGeneralData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciDocumentDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciOrderType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgDataOptional, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciThirdPartyId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciTargetWarehouseId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciTargetFunctionalUnitId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciAdjustmentConceptId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciTransitWarehouseId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciDispatchTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciSourceWarehouseId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgProducts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDlcTransferOrder As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlcgtransferOrder As DevExpress.XtraLayout.LayoutControlGroup
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents INDbtnCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDlcgGeneralData As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlciCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents INDdeDocumentDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDlciDocumentDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsleOrderType As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlciOrderType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDsleDispatchTo As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit2View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlciDispatchTo As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsleTargetWarehouseId As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit4View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDsleSourceWarehouseId As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGdvSourceWarehouseId As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlciSourceWarehouseId As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciTargetWarehouseId As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDsleTargetFunctionalUnitId As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlciTargetFunctionalUnitId As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDsleAdjustmentConceptId As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlciAdjustmentConceptId As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDsleThirdPartyId As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlcgDataOptional As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlciThirdPartyId As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDmeDescription As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDlciDescription As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDBtnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDLcgProducts As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDgcProducts As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvProducts As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDDescriptionProduct As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGcoInventoryQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGcoQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDPccDetailPhysicalInventory As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents INDGcoDetailPhysical As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRiPcePhysicalInventory As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDGcDetailPhysicalInventory As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvDetailPhysicalInventory As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDGcdProduct As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGcdBatchSerial As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGcdQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGcoCostProduct As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemTextEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents INDsleTransitWarehouseId As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView4 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlciTransitWarehouseId As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn14 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn15 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColInConsignment As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDRptIsConsignment As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents RepositoryItemGridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn16 As DevExpress.XtraGrid.Columns.GridColumn
End Class
