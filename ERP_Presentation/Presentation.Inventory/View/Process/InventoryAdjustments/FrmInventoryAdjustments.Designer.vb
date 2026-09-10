Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmInventoryAdjustments
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
        Dim ButtonImageOptions1 As DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions = New DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmInventoryAdjustments))
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject5 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject6 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDLyInventoryAdjustments = New DevExpress.XtraLayout.LayoutControl()
        Me.INDPccMoreInfoAdmission = New DevExpress.XtraEditors.PopupContainerControl()
        Me.LayoutControl2 = New DevExpress.XtraLayout.LayoutControl()
        Me.PopupContainerControl1 = New DevExpress.XtraEditors.PopupContainerControl()
        Me.INDFpAdmission = New DevExpress.Utils.FlyoutPanel()
        Me.LayoutControl6 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDTxtAdmissionPopup = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlGroup8 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem17 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.PanelControl5 = New DevExpress.XtraEditors.PanelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.INDTxtAuthorizationNumber = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtLiquidationType = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtEntity = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtAdmissionType = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtResponsiblePhone = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtAdmissionDate = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtAdmissionPlace = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtStay = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtPatient = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtAdmissionCode = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtBenefitsPlan = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtResponsibleName = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlGroup4 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.TabbedControlGroup1 = New DevExpress.XtraLayout.TabbedControlGroup()
        Me.LayoutControlGroup6 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem14 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem11 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem13 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem9 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem16 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem10 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciStay = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem19 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem15 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDSleThirdParty = New Presentation.Controls.SearchLookUpEditEx()
        Me.SearchLookUpEditExView8 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDDteDocumentDateInventoryControl = New DevExpress.XtraEditors.DateEdit()
        Me.INDTxtWarehouseInventoryControl = New DevExpress.XtraEditors.TextEdit()
        Me.INDSleInventoryControl = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDSleInventoryControlview = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColCodeInvetoryControl = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColDateInvetoryControl = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColWarehouseInventoryControl = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGcPhysicalInventory = New DevExpress.XtraGrid.GridControl()
        Me.INDGcPhysicalInventoryView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGclState = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.ColProductPhysical = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColBatchPhysical = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColInventoryQuantityPhysical = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColQuantityPhysical = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColStatusPhysical = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDBtnAddProducts = New DevExpress.XtraEditors.SimpleButton()
        Me.INDGcProducts = New DevExpress.XtraGrid.GridControl()
        Me.INDGvProducts = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColProduct = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColConcept = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColUnid = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColUnitValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDTxtDetail = New DevExpress.XtraEditors.MemoEdit()
        Me.INDSleWarehouse = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDSleWareHouseView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColCodeWarehouse = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColNameWarehouse = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleConcept = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGleConceptsView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColCodeConcept = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColNameConcept = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColTypeConcept = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGleAdjustmentType = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.INDGlvAdjustmentType = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColAdjustmentType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDDteDocumentDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDBtnCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDSleCostCenter = New DevExpress.XtraEditors.TextEdit()
        Me.INDsleAdmissionNumber = New Presentation.Controls.CtrSearchLookUpEditWithPopUp()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LyGroupMainData = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyBtnCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyDteDocumentDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyGleAdjustmentType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyTxtDetail = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciThirdParty = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LciAdmissionNumber = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LyGroupInfo = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLySleConcept = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLySleWarehouse = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLySleCostCenter = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LyGroupProducts = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyGcProducts = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyBtnAddProducts = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LyInventoryControl = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLySleInventoryControl = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyDteDocumentDateInventoryControl = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyTxtWarehouseInventoryControl = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LyInventoryControlProducts = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyGcPhysicalInventory = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoPopUpContainerEdit1 = New Presentation.Controls.IndigoPopUpContainerEdit(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyInventoryAdjustments, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLyInventoryAdjustments.SuspendLayout()
        CType(Me.INDPccMoreInfoAdmission, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPccMoreInfoAdmission.SuspendLayout()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl2.SuspendLayout()
        CType(Me.PopupContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PopupContainerControl1.SuspendLayout()
        CType(Me.INDFpAdmission, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDFpAdmission.SuspendLayout()
        CType(Me.LayoutControl6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl6.SuspendLayout()
        CType(Me.INDTxtAdmissionPopup.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtAuthorizationNumber.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtLiquidationType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtEntity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtAdmissionType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtResponsiblePhone.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtAdmissionDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtAdmissionPlace.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtStay.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtPatient.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtAdmissionCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtBenefitsPlan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtResponsibleName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TabbedControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciStay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleThirdParty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEditExView8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDteDocumentDateInventoryControl.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDteDocumentDateInventoryControl.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtWarehouseInventoryControl.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleInventoryControl.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleInventoryControlview, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcPhysicalInventory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcPhysicalInventoryView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcProducts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvProducts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtDetail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleWarehouse.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleWareHouseView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleConcept.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleConceptsView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleAdjustmentType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGlvAdjustmentType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDteDocumentDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDteDocumentDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDBtnCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleCostCenter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LyGroupMainData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyBtnCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyDteDocumentDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyGleAdjustmentType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyTxtDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciThirdParty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LciAdmissionNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LyGroupInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLySleConcept, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLySleWarehouse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLySleCostCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LyGroupProducts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyGcProducts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyBtnAddProducts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LyInventoryControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLySleInventoryControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyDteDocumentDateInventoryControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyTxtWarehouseInventoryControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LyInventoryControlProducts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyGcPhysicalInventory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLyInventoryAdjustments)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 136)
        Me.INDPanelControlBase.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDPanelControlBase.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1298, 601)
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
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDLyInventoryAdjustments
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 8)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 591)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDLyInventoryAdjustments
        '
        Me.INDLyInventoryAdjustments.AllowCustomization = False
        Me.INDLyInventoryAdjustments.Controls.Add(Me.INDPccMoreInfoAdmission)
        Me.INDLyInventoryAdjustments.Controls.Add(Me.INDSleThirdParty)
        Me.INDLyInventoryAdjustments.Controls.Add(Me.INDDteDocumentDateInventoryControl)
        Me.INDLyInventoryAdjustments.Controls.Add(Me.INDTxtWarehouseInventoryControl)
        Me.INDLyInventoryAdjustments.Controls.Add(Me.INDSleInventoryControl)
        Me.INDLyInventoryAdjustments.Controls.Add(Me.INDGcPhysicalInventory)
        Me.INDLyInventoryAdjustments.Controls.Add(Me.INDBtnAddProducts)
        Me.INDLyInventoryAdjustments.Controls.Add(Me.INDGcProducts)
        Me.INDLyInventoryAdjustments.Controls.Add(Me.INDTxtDetail)
        Me.INDLyInventoryAdjustments.Controls.Add(Me.INDSleWarehouse)
        Me.INDLyInventoryAdjustments.Controls.Add(Me.INDSleConcept)
        Me.INDLyInventoryAdjustments.Controls.Add(Me.INDGleAdjustmentType)
        Me.INDLyInventoryAdjustments.Controls.Add(Me.INDDteDocumentDate)
        Me.INDLyInventoryAdjustments.Controls.Add(Me.INDBtnCode)
        Me.INDLyInventoryAdjustments.Controls.Add(Me.INDSleCostCenter)
        Me.INDLyInventoryAdjustments.Controls.Add(Me.INDsleAdmissionNumber)
        Me.INDLyInventoryAdjustments.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDLyInventoryAdjustments, False)
        Me.INDLyInventoryAdjustments.Location = New System.Drawing.Point(202, 8)
        Me.INDLyInventoryAdjustments.Name = "INDLyInventoryAdjustments"
        Me.INDLyInventoryAdjustments.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(1920, 153, 574, 569)
        Me.INDLyInventoryAdjustments.Root = Me.LayoutControlGroup1
        Me.INDLyInventoryAdjustments.Size = New System.Drawing.Size(1094, 591)
        Me.INDLyInventoryAdjustments.TabIndex = 1
        Me.INDLyInventoryAdjustments.Text = "LayoutControl1"
        '
        'INDPccMoreInfoAdmission
        '
        Me.INDPccMoreInfoAdmission.Controls.Add(Me.LayoutControl2)
        Me.INDPccMoreInfoAdmission.Location = New System.Drawing.Point(56, 461)
        Me.INDPccMoreInfoAdmission.Name = "INDPccMoreInfoAdmission"
        Me.INDPccMoreInfoAdmission.Size = New System.Drawing.Size(750, 311)
        Me.INDPccMoreInfoAdmission.TabIndex = 21
        '
        'LayoutControl2
        '
        Me.LayoutControl2.AutoScroll = False
        Me.LayoutControl2.Controls.Add(Me.PopupContainerControl1)
        Me.LayoutControl2.Controls.Add(Me.LabelControl2)
        Me.LayoutControl2.Controls.Add(Me.INDTxtAuthorizationNumber)
        Me.LayoutControl2.Controls.Add(Me.INDTxtLiquidationType)
        Me.LayoutControl2.Controls.Add(Me.INDTxtEntity)
        Me.LayoutControl2.Controls.Add(Me.INDTxtAdmissionType)
        Me.LayoutControl2.Controls.Add(Me.INDTxtResponsiblePhone)
        Me.LayoutControl2.Controls.Add(Me.INDTxtAdmissionDate)
        Me.LayoutControl2.Controls.Add(Me.INDTxtAdmissionPlace)
        Me.LayoutControl2.Controls.Add(Me.INDTxtStay)
        Me.LayoutControl2.Controls.Add(Me.INDTxtPatient)
        Me.LayoutControl2.Controls.Add(Me.INDTxtAdmissionCode)
        Me.LayoutControl2.Controls.Add(Me.INDTxtBenefitsPlan)
        Me.LayoutControl2.Controls.Add(Me.INDTxtResponsibleName)
        Me.LayoutControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl2.Name = "LayoutControl2"
        Me.LayoutControl2.Root = Me.LayoutControlGroup4
        Me.LayoutControl2.Size = New System.Drawing.Size(750, 311)
        Me.LayoutControl2.TabIndex = 0
        Me.LayoutControl2.Text = "LayoutControl2"
        '
        'PopupContainerControl1
        '
        Me.PopupContainerControl1.Controls.Add(Me.INDFpAdmission)
        Me.PopupContainerControl1.Controls.Add(Me.PanelControl5)
        Me.PopupContainerControl1.Location = New System.Drawing.Point(219, 36)
        Me.PopupContainerControl1.Name = "PopupContainerControl1"
        Me.PopupContainerControl1.Size = New System.Drawing.Size(387, 55)
        Me.PopupContainerControl1.TabIndex = 28
        '
        'INDFpAdmission
        '
        Me.INDFpAdmission.Controls.Add(Me.LayoutControl6)
        Me.INDFpAdmission.Location = New System.Drawing.Point(11, 2)
        Me.INDFpAdmission.Name = "INDFpAdmission"
        Me.INDFpAdmission.OptionsBeakPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        ButtonImageOptions1.Image = CType(resources.GetObject("ButtonImageOptions1.Image"), System.Drawing.Image)
        Me.INDFpAdmission.OptionsButtonPanel.Buttons.AddRange(New DevExpress.XtraEditors.ButtonPanel.IBaseButton() {New DevExpress.Utils.PeekFormButton("", True, ButtonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "Cerrar", -1, True, Nothing, True, False, True, Nothing, -1, False)})
        Me.INDFpAdmission.Size = New System.Drawing.Size(373, 58)
        Me.INDFpAdmission.TabIndex = 0
        '
        'LayoutControl6
        '
        Me.LayoutControl6.Controls.Add(Me.INDTxtAdmissionPopup)
        Me.LayoutControl6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl6.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl6.Name = "LayoutControl6"
        Me.LayoutControl6.Root = Me.LayoutControlGroup8
        Me.LayoutControl6.Size = New System.Drawing.Size(373, 58)
        Me.LayoutControl6.TabIndex = 0
        Me.LayoutControl6.Text = "LayoutControl6"
        '
        'INDTxtAdmissionPopup
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtAdmissionPopup, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtAdmissionPopup, False)
        Me.INDTxtAdmissionPopup.Location = New System.Drawing.Point(12, 12)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtAdmissionPopup, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtAdmissionPopup.Name = "INDTxtAdmissionPopup"
        Me.INDTxtAdmissionPopup.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtAdmissionPopup.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtAdmissionPopup.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtAdmissionPopup.Properties.Appearance.Options.UseFont = True
        Me.INDTxtAdmissionPopup.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtAdmissionPopup.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtAdmissionPopup.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDTxtAdmissionPopup.Properties.ReadOnly = True
        Me.INDTxtAdmissionPopup.Size = New System.Drawing.Size(346, 26)
        Me.INDTxtAdmissionPopup.StyleController = Me.LayoutControl6
        Me.INDTxtAdmissionPopup.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtAdmissionPopup, 0)
        '
        'LayoutControlGroup8
        '
        Me.LayoutControlGroup8.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup8.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup8.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup8.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup8.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup8.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup8.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup8.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup8.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup8.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup8.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup8.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup8.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup8.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup8, False)
        Me.LayoutControlGroup8.CustomizationFormText = "LayoutControlGroup4"
        Me.LayoutControlGroup8.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup8.GroupBordersVisible = False
        Me.LayoutControlGroup8.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem17})
        Me.LayoutControlGroup8.Name = "LayoutControlGroup4"
        Me.LayoutControlGroup8.Size = New System.Drawing.Size(373, 58)
        Me.LayoutControlGroup8.TextVisible = False
        '
        'LayoutControlItem17
        '
        Me.LayoutControlItem17.Control = Me.INDTxtAdmissionPopup
        Me.LayoutControlItem17.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem17.MaxSize = New System.Drawing.Size(350, 36)
        Me.LayoutControlItem17.MinSize = New System.Drawing.Size(350, 36)
        Me.LayoutControlItem17.Name = "LayoutControlItem6"
        Me.LayoutControlItem17.Size = New System.Drawing.Size(353, 38)
        Me.LayoutControlItem17.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem17.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem17.TextVisible = False
        '
        'PanelControl5
        '
        Me.PanelControl5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl5.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl5.Name = "PanelControl5"
        Me.PanelControl5.Size = New System.Drawing.Size(387, 55)
        Me.PanelControl5.TabIndex = 25
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Segoe UI", 15.75!)
        Me.LabelControl2.Appearance.ForeColor = System.Drawing.Color.White
        Me.LabelControl2.Appearance.Options.UseBackColor = True
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Appearance.Options.UseForeColor = True
        Me.LabelControl2.Location = New System.Drawing.Point(12, 12)
        Me.LabelControl2.Margin = New System.Windows.Forms.Padding(0)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Padding = New System.Windows.Forms.Padding(14, 0, 0, 0)
        Me.LabelControl2.Size = New System.Drawing.Size(726, 30)
        Me.LabelControl2.StyleController = Me.LayoutControl2
        Me.LabelControl2.TabIndex = 5
        Me.LabelControl2.Text = "Más Información"
        '
        'INDTxtAuthorizationNumber
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtAuthorizationNumber, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtAuthorizationNumber, False)
        Me.INDTxtAuthorizationNumber.Location = New System.Drawing.Point(151, 239)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtAuthorizationNumber, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtAuthorizationNumber.Name = "INDTxtAuthorizationNumber"
        Me.INDTxtAuthorizationNumber.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtAuthorizationNumber.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtAuthorizationNumber.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtAuthorizationNumber.Properties.Appearance.Options.UseFont = True
        Me.INDTxtAuthorizationNumber.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtAuthorizationNumber.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtAuthorizationNumber.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtAuthorizationNumber.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtAuthorizationNumber.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtAuthorizationNumber.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtAuthorizationNumber.Properties.ReadOnly = True
        Me.INDTxtAuthorizationNumber.Size = New System.Drawing.Size(221, 24)
        Me.INDTxtAuthorizationNumber.StyleController = Me.LayoutControl2
        Me.INDTxtAuthorizationNumber.TabIndex = 11
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtAuthorizationNumber, 0)
        '
        'INDTxtLiquidationType
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtLiquidationType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtLiquidationType, False)
        Me.INDTxtLiquidationType.Location = New System.Drawing.Point(500, 179)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtLiquidationType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtLiquidationType.Name = "INDTxtLiquidationType"
        Me.INDTxtLiquidationType.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtLiquidationType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtLiquidationType.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtLiquidationType.Properties.Appearance.Options.UseFont = True
        Me.INDTxtLiquidationType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtLiquidationType.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtLiquidationType.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtLiquidationType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtLiquidationType.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtLiquidationType.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtLiquidationType.Properties.ReadOnly = True
        Me.INDTxtLiquidationType.Size = New System.Drawing.Size(226, 24)
        Me.INDTxtLiquidationType.StyleController = Me.LayoutControl2
        Me.INDTxtLiquidationType.TabIndex = 9
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtLiquidationType, 0)
        '
        'INDTxtEntity
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtEntity, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtEntity, False)
        Me.INDTxtEntity.Location = New System.Drawing.Point(151, 209)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtEntity, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtEntity.Name = "INDTxtEntity"
        Me.INDTxtEntity.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtEntity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtEntity.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtEntity.Properties.Appearance.Options.UseFont = True
        Me.INDTxtEntity.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtEntity.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtEntity.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtEntity.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtEntity.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtEntity.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtEntity.Properties.ReadOnly = True
        Me.INDTxtEntity.Size = New System.Drawing.Size(221, 24)
        Me.INDTxtEntity.StyleController = Me.LayoutControl2
        Me.INDTxtEntity.TabIndex = 10
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtEntity, 0)
        '
        'INDTxtAdmissionType
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtAdmissionType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtAdmissionType, False)
        Me.INDTxtAdmissionType.Location = New System.Drawing.Point(500, 149)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtAdmissionType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtAdmissionType.Name = "INDTxtAdmissionType"
        Me.INDTxtAdmissionType.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtAdmissionType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtAdmissionType.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtAdmissionType.Properties.Appearance.Options.UseFont = True
        Me.INDTxtAdmissionType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtAdmissionType.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtAdmissionType.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtAdmissionType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtAdmissionType.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtAdmissionType.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtAdmissionType.Properties.ReadOnly = True
        Me.INDTxtAdmissionType.Size = New System.Drawing.Size(226, 24)
        Me.INDTxtAdmissionType.StyleController = Me.LayoutControl2
        Me.INDTxtAdmissionType.TabIndex = 8
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtAdmissionType, 0)
        '
        'INDTxtResponsiblePhone
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtResponsiblePhone, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtResponsiblePhone, False)
        Me.INDTxtResponsiblePhone.Location = New System.Drawing.Point(151, 269)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtResponsiblePhone, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtResponsiblePhone.Name = "INDTxtResponsiblePhone"
        Me.INDTxtResponsiblePhone.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtResponsiblePhone.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtResponsiblePhone.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtResponsiblePhone.Properties.Appearance.Options.UseFont = True
        Me.INDTxtResponsiblePhone.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtResponsiblePhone.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtResponsiblePhone.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtResponsiblePhone.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtResponsiblePhone.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtResponsiblePhone.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtResponsiblePhone.Properties.ReadOnly = True
        Me.INDTxtResponsiblePhone.Size = New System.Drawing.Size(221, 24)
        Me.INDTxtResponsiblePhone.StyleController = Me.LayoutControl2
        Me.INDTxtResponsiblePhone.TabIndex = 12
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtResponsiblePhone, 0)
        '
        'INDTxtAdmissionDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtAdmissionDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtAdmissionDate, False)
        Me.INDTxtAdmissionDate.Location = New System.Drawing.Point(151, 149)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtAdmissionDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtAdmissionDate.Name = "INDTxtAdmissionDate"
        Me.INDTxtAdmissionDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtAdmissionDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtAdmissionDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtAdmissionDate.Properties.Appearance.Options.UseFont = True
        Me.INDTxtAdmissionDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtAdmissionDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtAdmissionDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtAdmissionDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtAdmissionDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtAdmissionDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtAdmissionDate.Properties.ReadOnly = True
        Me.INDTxtAdmissionDate.Size = New System.Drawing.Size(218, 24)
        Me.INDTxtAdmissionDate.StyleController = Me.LayoutControl2
        Me.INDTxtAdmissionDate.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtAdmissionDate, 0)
        '
        'INDTxtAdmissionPlace
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtAdmissionPlace, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtAdmissionPlace, False)
        Me.INDTxtAdmissionPlace.Location = New System.Drawing.Point(151, 179)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtAdmissionPlace, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtAdmissionPlace.Name = "INDTxtAdmissionPlace"
        Me.INDTxtAdmissionPlace.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtAdmissionPlace.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtAdmissionPlace.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtAdmissionPlace.Properties.Appearance.Options.UseFont = True
        Me.INDTxtAdmissionPlace.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtAdmissionPlace.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtAdmissionPlace.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtAdmissionPlace.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtAdmissionPlace.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtAdmissionPlace.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtAdmissionPlace.Properties.ReadOnly = True
        Me.INDTxtAdmissionPlace.Size = New System.Drawing.Size(218, 24)
        Me.INDTxtAdmissionPlace.StyleController = Me.LayoutControl2
        Me.INDTxtAdmissionPlace.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtAdmissionPlace, 0)
        '
        'INDTxtStay
        '
        Me.INDTxtStay.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.[True]
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtStay, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtStay, False)
        Me.INDTxtStay.Location = New System.Drawing.Point(151, 119)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtStay, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtStay.Name = "INDTxtStay"
        Me.INDTxtStay.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtStay.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtStay.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtStay.Properties.Appearance.Options.UseFont = True
        Me.INDTxtStay.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtStay.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtStay.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtStay.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtStay.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtStay.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtStay.Properties.ReadOnly = True
        Me.INDTxtStay.Size = New System.Drawing.Size(218, 24)
        Me.INDTxtStay.StyleController = Me.LayoutControl2
        Me.INDTxtStay.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtStay, 0)
        '
        'INDTxtPatient
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtPatient, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtPatient, False)
        Me.INDTxtPatient.Location = New System.Drawing.Point(151, 89)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtPatient, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtPatient.Name = "INDTxtPatient"
        Me.INDTxtPatient.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtPatient.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtPatient.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtPatient.Properties.Appearance.Options.UseFont = True
        Me.INDTxtPatient.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtPatient.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtPatient.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtPatient.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtPatient.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtPatient.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtPatient.Properties.ReadOnly = True
        Me.INDTxtPatient.Size = New System.Drawing.Size(575, 24)
        Me.INDTxtPatient.StyleController = Me.LayoutControl2
        Me.INDTxtPatient.TabIndex = 7
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtPatient, 0)
        '
        'INDTxtAdmissionCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtAdmissionCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtAdmissionCode, False)
        Me.INDTxtAdmissionCode.Location = New System.Drawing.Point(500, 119)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtAdmissionCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtAdmissionCode.Name = "INDTxtAdmissionCode"
        Me.INDTxtAdmissionCode.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtAdmissionCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtAdmissionCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtAdmissionCode.Properties.Appearance.Options.UseFont = True
        Me.INDTxtAdmissionCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtAdmissionCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtAdmissionCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtAdmissionCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtAdmissionCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtAdmissionCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtAdmissionCode.Properties.ReadOnly = True
        Me.INDTxtAdmissionCode.Size = New System.Drawing.Size(226, 24)
        Me.INDTxtAdmissionCode.StyleController = Me.LayoutControl2
        Me.INDTxtAdmissionCode.TabIndex = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtAdmissionCode, 0)
        '
        'INDTxtBenefitsPlan
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtBenefitsPlan, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtBenefitsPlan, False)
        Me.INDTxtBenefitsPlan.Location = New System.Drawing.Point(503, 209)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtBenefitsPlan, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtBenefitsPlan.Name = "INDTxtBenefitsPlan"
        Me.INDTxtBenefitsPlan.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtBenefitsPlan.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtBenefitsPlan.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtBenefitsPlan.Properties.Appearance.Options.UseFont = True
        Me.INDTxtBenefitsPlan.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtBenefitsPlan.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtBenefitsPlan.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtBenefitsPlan.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtBenefitsPlan.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtBenefitsPlan.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtBenefitsPlan.Properties.ReadOnly = True
        Me.INDTxtBenefitsPlan.Size = New System.Drawing.Size(223, 24)
        Me.INDTxtBenefitsPlan.StyleController = Me.LayoutControl2
        Me.INDTxtBenefitsPlan.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtBenefitsPlan, 0)
        '
        'INDTxtResponsibleName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtResponsibleName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtResponsibleName, False)
        Me.INDTxtResponsibleName.Location = New System.Drawing.Point(503, 239)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtResponsibleName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtResponsibleName.Name = "INDTxtResponsibleName"
        Me.INDTxtResponsibleName.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtResponsibleName.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtResponsibleName.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtResponsibleName.Properties.Appearance.Options.UseFont = True
        Me.INDTxtResponsibleName.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtResponsibleName.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtResponsibleName.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtResponsibleName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtResponsibleName.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtResponsibleName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtResponsibleName.Properties.ReadOnly = True
        Me.INDTxtResponsibleName.Size = New System.Drawing.Size(223, 24)
        Me.INDTxtResponsibleName.StyleController = Me.LayoutControl2
        Me.INDTxtResponsibleName.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtResponsibleName, 0)
        '
        'LayoutControlGroup4
        '
        Me.LayoutControlGroup4.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup4.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup4.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup4.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup4.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup4.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup4.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup4.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup4.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup4.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup4.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup4.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup4.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup4.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup4, False)
        Me.LayoutControlGroup4.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup4.GroupBordersVisible = False
        Me.LayoutControlGroup4.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.TabbedControlGroup1, Me.LayoutControlItem4})
        Me.LayoutControlGroup4.Name = "LayoutControlGroup4"
        Me.LayoutControlGroup4.Size = New System.Drawing.Size(750, 319)
        Me.LayoutControlGroup4.TextVisible = False
        '
        'TabbedControlGroup1
        '
        Me.TabbedControlGroup1.CustomizationFormText = "TabbedControlGroup1"
        Me.TabbedControlGroup1.Location = New System.Drawing.Point(0, 34)
        Me.TabbedControlGroup1.Name = "TabbedControlGroup1"
        Me.TabbedControlGroup1.SelectedTabPage = Me.LayoutControlGroup6
        Me.TabbedControlGroup1.Size = New System.Drawing.Size(730, 265)
        Me.TabbedControlGroup1.TabPages.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup6})
        '
        'LayoutControlGroup6
        '
        Me.LayoutControlGroup6.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup6.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup6.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup6.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup6.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup6.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup6.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup6.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup6.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup6.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup6.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup6.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup6.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup6.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup6, False)
        Me.LayoutControlGroup6.CustomizationFormText = "Datos del Ingreso"
        Me.LayoutControlGroup6.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem14, Me.LayoutControlItem11, Me.LayoutControlItem13, Me.LayoutControlItem9, Me.LayoutControlItem16, Me.LayoutControlItem5, Me.LayoutControlItem10, Me.INDLciStay, Me.LayoutControlItem6, Me.LayoutControlItem19, Me.LayoutControlItem8, Me.LayoutControlItem15, Me.EmptySpaceItem1})
        Me.LayoutControlGroup6.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup6.Name = "LayoutControlGroup6"
        Me.LayoutControlGroup6.Size = New System.Drawing.Size(706, 210)
        Me.LayoutControlGroup6.Text = "Datos del Ingreso"
        '
        'LayoutControlItem14
        '
        Me.LayoutControlItem14.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.LayoutControlItem14.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem14.Control = Me.INDTxtAuthorizationNumber
        Me.LayoutControlItem14.CustomizationFormText = "LayoutControlItem14"
        Me.LayoutControlItem14.Location = New System.Drawing.Point(0, 150)
        Me.LayoutControlItem14.MaxSize = New System.Drawing.Size(0, 30)
        Me.LayoutControlItem14.MinSize = New System.Drawing.Size(187, 30)
        Me.LayoutControlItem14.Name = "LayoutControlItem14"
        Me.LayoutControlItem14.Size = New System.Drawing.Size(352, 30)
        Me.LayoutControlItem14.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem14.Text = "Nº Autorización"
        Me.LayoutControlItem14.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem14.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem14.TextToControlDistance = 12
        '
        'LayoutControlItem11
        '
        Me.LayoutControlItem11.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.LayoutControlItem11.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem11.Control = Me.INDTxtLiquidationType
        Me.LayoutControlItem11.CustomizationFormText = "Tipo de Liquidación"
        Me.LayoutControlItem11.Location = New System.Drawing.Point(349, 90)
        Me.LayoutControlItem11.MaxSize = New System.Drawing.Size(0, 30)
        Me.LayoutControlItem11.MinSize = New System.Drawing.Size(187, 30)
        Me.LayoutControlItem11.Name = "LayoutControlItem11"
        Me.LayoutControlItem11.Size = New System.Drawing.Size(357, 30)
        Me.LayoutControlItem11.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem11.Text = "Tipo de Liquidación"
        Me.LayoutControlItem11.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem11.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem11.TextToControlDistance = 12
        '
        'LayoutControlItem13
        '
        Me.LayoutControlItem13.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.LayoutControlItem13.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem13.Control = Me.INDTxtEntity
        Me.LayoutControlItem13.CustomizationFormText = "LayoutControlItem13"
        Me.LayoutControlItem13.Location = New System.Drawing.Point(0, 120)
        Me.LayoutControlItem13.MaxSize = New System.Drawing.Size(0, 30)
        Me.LayoutControlItem13.MinSize = New System.Drawing.Size(187, 30)
        Me.LayoutControlItem13.Name = "LayoutControlItem13"
        Me.LayoutControlItem13.Size = New System.Drawing.Size(352, 30)
        Me.LayoutControlItem13.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem13.Text = "Entidad"
        Me.LayoutControlItem13.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem13.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem13.TextToControlDistance = 12
        '
        'LayoutControlItem9
        '
        Me.LayoutControlItem9.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.LayoutControlItem9.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem9.Control = Me.INDTxtAdmissionType
        Me.LayoutControlItem9.CustomizationFormText = "Tipo de Ingreso"
        Me.LayoutControlItem9.Location = New System.Drawing.Point(349, 60)
        Me.LayoutControlItem9.MaxSize = New System.Drawing.Size(0, 30)
        Me.LayoutControlItem9.MinSize = New System.Drawing.Size(187, 30)
        Me.LayoutControlItem9.Name = "LayoutControlItem9"
        Me.LayoutControlItem9.Size = New System.Drawing.Size(357, 30)
        Me.LayoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem9.Text = "Tipo de Ingreso"
        Me.LayoutControlItem9.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem9.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem9.TextToControlDistance = 12
        '
        'LayoutControlItem16
        '
        Me.LayoutControlItem16.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.LayoutControlItem16.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem16.Control = Me.INDTxtResponsiblePhone
        Me.LayoutControlItem16.CustomizationFormText = "LayoutControlItem16"
        Me.LayoutControlItem16.Location = New System.Drawing.Point(0, 180)
        Me.LayoutControlItem16.MaxSize = New System.Drawing.Size(0, 30)
        Me.LayoutControlItem16.MinSize = New System.Drawing.Size(187, 30)
        Me.LayoutControlItem16.Name = "LayoutControlItem16"
        Me.LayoutControlItem16.Size = New System.Drawing.Size(352, 30)
        Me.LayoutControlItem16.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem16.Text = "Telefono Acudiente"
        Me.LayoutControlItem16.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem16.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem16.TextToControlDistance = 12
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.LayoutControlItem5.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem5.Control = Me.INDTxtAdmissionDate
        Me.LayoutControlItem5.CustomizationFormText = "Fecha Ingreso"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 60)
        Me.LayoutControlItem5.MaxSize = New System.Drawing.Size(0, 30)
        Me.LayoutControlItem5.MinSize = New System.Drawing.Size(187, 30)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(349, 30)
        Me.LayoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem5.Text = "Fecha Ingreso"
        Me.LayoutControlItem5.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem5.TextToControlDistance = 12
        '
        'LayoutControlItem10
        '
        Me.LayoutControlItem10.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.LayoutControlItem10.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem10.Control = Me.INDTxtAdmissionPlace
        Me.LayoutControlItem10.CustomizationFormText = "Lugar Ingreso"
        Me.LayoutControlItem10.Location = New System.Drawing.Point(0, 90)
        Me.LayoutControlItem10.MaxSize = New System.Drawing.Size(0, 30)
        Me.LayoutControlItem10.MinSize = New System.Drawing.Size(187, 30)
        Me.LayoutControlItem10.Name = "LayoutControlItem10"
        Me.LayoutControlItem10.Size = New System.Drawing.Size(349, 30)
        Me.LayoutControlItem10.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem10.Text = "Lugar Ingreso"
        Me.LayoutControlItem10.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem10.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem10.TextToControlDistance = 12
        '
        'INDLciStay
        '
        Me.INDLciStay.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDLciStay.AppearanceItemCaption.Options.UseFont = True
        Me.INDLciStay.Control = Me.INDTxtStay
        Me.INDLciStay.CustomizationFormText = "Estancia (Cama)"
        Me.INDLciStay.Location = New System.Drawing.Point(0, 30)
        Me.INDLciStay.MaxSize = New System.Drawing.Size(0, 30)
        Me.INDLciStay.MinSize = New System.Drawing.Size(187, 30)
        Me.INDLciStay.Name = "INDLciStay"
        Me.INDLciStay.Size = New System.Drawing.Size(349, 30)
        Me.INDLciStay.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciStay.Text = "Estancia (Cama)"
        Me.INDLciStay.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciStay.TextSize = New System.Drawing.Size(115, 21)
        Me.INDLciStay.TextToControlDistance = 12
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.LayoutControlItem6.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem6.Control = Me.INDTxtPatient
        Me.LayoutControlItem6.CustomizationFormText = "Paciente"
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem6.MaxSize = New System.Drawing.Size(0, 30)
        Me.LayoutControlItem6.MinSize = New System.Drawing.Size(187, 30)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(706, 30)
        Me.LayoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem6.Text = "Paciente"
        Me.LayoutControlItem6.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem6.TextToControlDistance = 12
        '
        'LayoutControlItem19
        '
        Me.LayoutControlItem19.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.LayoutControlItem19.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem19.Control = Me.INDTxtAdmissionCode
        Me.LayoutControlItem19.CustomizationFormText = "Nº Ingreso"
        Me.LayoutControlItem19.Location = New System.Drawing.Point(349, 30)
        Me.LayoutControlItem19.MaxSize = New System.Drawing.Size(0, 30)
        Me.LayoutControlItem19.MinSize = New System.Drawing.Size(187, 30)
        Me.LayoutControlItem19.Name = "LayoutControlItem19"
        Me.LayoutControlItem19.Size = New System.Drawing.Size(357, 30)
        Me.LayoutControlItem19.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem19.Text = "Nº Ingreso"
        Me.LayoutControlItem19.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem19.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem19.TextToControlDistance = 12
        '
        'LayoutControlItem8
        '
        Me.LayoutControlItem8.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.LayoutControlItem8.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem8.Control = Me.INDTxtBenefitsPlan
        Me.LayoutControlItem8.CustomizationFormText = "Plan de Beneficios"
        Me.LayoutControlItem8.Location = New System.Drawing.Point(352, 120)
        Me.LayoutControlItem8.MaxSize = New System.Drawing.Size(0, 30)
        Me.LayoutControlItem8.MinSize = New System.Drawing.Size(187, 30)
        Me.LayoutControlItem8.Name = "LayoutControlItem8"
        Me.LayoutControlItem8.Size = New System.Drawing.Size(354, 30)
        Me.LayoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem8.Text = "Plan de Beneficios"
        Me.LayoutControlItem8.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem8.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem8.TextToControlDistance = 12
        '
        'LayoutControlItem15
        '
        Me.LayoutControlItem15.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.LayoutControlItem15.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem15.Control = Me.INDTxtResponsibleName
        Me.LayoutControlItem15.CustomizationFormText = "LayoutControlItem15"
        Me.LayoutControlItem15.Location = New System.Drawing.Point(352, 150)
        Me.LayoutControlItem15.MaxSize = New System.Drawing.Size(0, 30)
        Me.LayoutControlItem15.MinSize = New System.Drawing.Size(187, 30)
        Me.LayoutControlItem15.Name = "LayoutControlItem15"
        Me.LayoutControlItem15.Size = New System.Drawing.Size(354, 30)
        Me.LayoutControlItem15.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem15.Text = "Nombre Acudiente"
        Me.LayoutControlItem15.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem15.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem15.TextToControlDistance = 12
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(352, 180)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(354, 30)
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.LabelControl2
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem4.MinSize = New System.Drawing.Size(175, 34)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(730, 34)
        Me.LayoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextVisible = False
        '
        'INDSleThirdParty
        '
        Me.INDSleThirdParty.AllowQueryOne = True
        Me.INDSleThirdParty.Datasource = Nothing
        Me.INDSleThirdParty.DisplayMember = "{Nit} - {Name}"
        Me.INDSleThirdParty.DisplayNullText = ""
        Me.INDSleThirdParty.EditValue = Nothing
        Me.INDSleThirdParty.EnterMoveNextControl = True
        Me.INDSleThirdParty.FuncQueryOnKeyEnterPressed = Nothing
        Me.INDSleThirdParty.IdOpenForm = 532
        Me.INDSleThirdParty.IsReadOnly = False
        Me.INDSleThirdParty.Location = New System.Drawing.Point(132, 197)
        Me.INDSleThirdParty.Margin = New System.Windows.Forms.Padding(0)
        Me.INDSleThirdParty.MaximumSize = New System.Drawing.Size(5000, 28)
        Me.INDSleThirdParty.MinimumSize = New System.Drawing.Size(100, 28)
        Me.INDSleThirdParty.Name = "INDSleThirdParty"
        Me.INDSleThirdParty.PopUpFormSize = New System.Drawing.Size(500, 400)
        Me.INDSleThirdParty.Size = New System.Drawing.Size(278, 28)
        Me.INDSleThirdParty.TabIndex = 7
        Me.INDSleThirdParty.ValueMember = "Id"
        Me.INDSleThirdParty.View = Me.SearchLookUpEditExView8
        '
        'SearchLookUpEditExView8
        '
        Me.SearchLookUpEditExView8.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEditExView8.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEditExView8.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEditExView8.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEditExView8.Appearance.FocusedRow.Options.UseForeColor = True
        Me.SearchLookUpEditExView8.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView8.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEditExView8.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView8.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEditExView8.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEditExView8.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEditExView8.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.SearchLookUpEditExView8.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn2, Me.GridColumn3})
        Me.SearchLookUpEditExView8.Name = "SearchLookUpEditExView8"
        Me.SearchLookUpEditExView8.OptionsCustomization.AllowGroup = False
        Me.SearchLookUpEditExView8.OptionsDetail.EnableMasterViewMode = False
        Me.SearchLookUpEditExView8.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEditExView8.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEditExView8.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEditExView8.OptionsView.ShowDetailButtons = False
        Me.SearchLookUpEditExView8.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEditExView8, False)
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Numero Documento"
        Me.GridColumn2.FieldName = "Nit"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Nombre"
        Me.GridColumn3.FieldName = "Name"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 1
        '
        'INDDteDocumentDateInventoryControl
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDDteDocumentDateInventoryControl, True)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDDteDocumentDateInventoryControl, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDDteDocumentDateInventoryControl, False)
        Me.INDDteDocumentDateInventoryControl.EditValue = Nothing
        Me.INDDteDocumentDateInventoryControl.EnterMoveNextControl = True
        Me.INDDteDocumentDateInventoryControl.Location = New System.Drawing.Point(1812, 125)
        Me.IndigoTextEdit1.SetMascara(Me.INDDteDocumentDateInventoryControl, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDDteDocumentDateInventoryControl, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDDteDocumentDateInventoryControl.Name = "INDDteDocumentDateInventoryControl"
        Me.INDDteDocumentDateInventoryControl.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDDteDocumentDateInventoryControl.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDteDocumentDateInventoryControl.Properties.Appearance.Options.UseBackColor = True
        Me.INDDteDocumentDateInventoryControl.Properties.Appearance.Options.UseFont = True
        Me.INDDteDocumentDateInventoryControl.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDDteDocumentDateInventoryControl.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDDteDocumentDateInventoryControl.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDteDocumentDateInventoryControl.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDDteDocumentDateInventoryControl.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDDteDocumentDateInventoryControl.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDteDocumentDateInventoryControl.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDteDocumentDateInventoryControl.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDteDocumentDateInventoryControl.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.INDDteDocumentDateInventoryControl.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDteDocumentDateInventoryControl.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDteDocumentDateInventoryControl.Properties.ReadOnly = True
        Me.INDDteDocumentDateInventoryControl.Size = New System.Drawing.Size(278, 28)
        Me.INDDteDocumentDateInventoryControl.StyleController = Me.INDLyInventoryAdjustments
        Me.INDDteDocumentDateInventoryControl.TabIndex = 20
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDDteDocumentDateInventoryControl, 0)
        '
        'INDTxtWarehouseInventoryControl
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtWarehouseInventoryControl, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtWarehouseInventoryControl, False)
        Me.INDTxtWarehouseInventoryControl.EnterMoveNextControl = True
        Me.INDTxtWarehouseInventoryControl.Location = New System.Drawing.Point(1812, 89)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtWarehouseInventoryControl, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtWarehouseInventoryControl.Name = "INDTxtWarehouseInventoryControl"
        Me.INDTxtWarehouseInventoryControl.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtWarehouseInventoryControl.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtWarehouseInventoryControl.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtWarehouseInventoryControl.Properties.Appearance.Options.UseFont = True
        Me.INDTxtWarehouseInventoryControl.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtWarehouseInventoryControl.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtWarehouseInventoryControl.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtWarehouseInventoryControl.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtWarehouseInventoryControl.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtWarehouseInventoryControl.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtWarehouseInventoryControl.Properties.ReadOnly = True
        Me.INDTxtWarehouseInventoryControl.Size = New System.Drawing.Size(278, 28)
        Me.INDTxtWarehouseInventoryControl.StyleController = Me.INDLyInventoryAdjustments
        Me.INDTxtWarehouseInventoryControl.TabIndex = 19
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtWarehouseInventoryControl, 0)
        '
        'INDSleInventoryControl
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleInventoryControl, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleInventoryControl, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleInventoryControl, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleInventoryControl, True)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleInventoryControl, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleInventoryControl, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleInventoryControl, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleInventoryControl, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleInventoryControl, False)
        Me.INDSleInventoryControl.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleInventoryControl, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleInventoryControl, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleInventoryControl, False)
        Me.INDSleInventoryControl.Location = New System.Drawing.Point(1812, 53)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleInventoryControl, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleInventoryControl.Name = "INDSleInventoryControl"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleInventoryControl, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleInventoryControl, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleInventoryControl, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDSleInventoryControl, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleInventoryControl, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleInventoryControl, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleInventoryControl, False)
        Me.INDSleInventoryControl.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleInventoryControl.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleInventoryControl.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleInventoryControl.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleInventoryControl.Properties.Appearance.Options.UseFont = True
        Me.INDSleInventoryControl.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleInventoryControl.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleInventoryControl.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSleInventoryControl.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleInventoryControl.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSleInventoryControl.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSleInventoryControl.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleInventoryControl.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleInventoryControl.Properties.DisplayMember = "Code"
        Me.INDSleInventoryControl.Properties.NullText = ""
        Me.INDSleInventoryControl.Properties.PopupSizeable = False
        Me.INDSleInventoryControl.Properties.PopupView = Me.INDSleInventoryControlview
        Me.INDSleInventoryControl.Properties.ShowFooter = False
        Me.INDSleInventoryControl.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleInventoryControl, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleInventoryControl, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleInventoryControl, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleInventoryControl, True)
        Me.INDSleInventoryControl.Size = New System.Drawing.Size(278, 28)
        Me.INDSleInventoryControl.StyleController = Me.INDLyInventoryAdjustments
        Me.INDSleInventoryControl.TabIndex = 18
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleInventoryControl, "849")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleInventoryControl, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleInventoryControl, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleInventoryControl, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleInventoryControl, False)
        '
        'INDSleInventoryControlview
        '
        Me.INDSleInventoryControlview.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDSleInventoryControlview.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDSleInventoryControlview.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDSleInventoryControlview.Appearance.FocusedRow.Options.UseFont = True
        Me.INDSleInventoryControlview.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDSleInventoryControlview.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleInventoryControlview.Appearance.GroupRow.Options.UseFont = True
        Me.INDSleInventoryControlview.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleInventoryControlview.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDSleInventoryControlview.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDSleInventoryControlview.Appearance.Row.Options.UseFont = True
        Me.INDSleInventoryControlview.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColCodeInvetoryControl, Me.ColDateInvetoryControl, Me.ColWarehouseInventoryControl})
        Me.INDSleInventoryControlview.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDSleInventoryControlview.Name = "INDSleInventoryControlview"
        Me.INDSleInventoryControlview.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDSleInventoryControlview.OptionsView.EnableAppearanceEvenRow = True
        Me.INDSleInventoryControlview.OptionsView.EnableAppearanceOddRow = True
        Me.INDSleInventoryControlview.OptionsView.ShowAutoFilterRow = True
        Me.INDSleInventoryControlview.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDSleInventoryControlview, False)
        '
        'ColCodeInvetoryControl
        '
        Me.ColCodeInvetoryControl.Caption = "Código"
        Me.ColCodeInvetoryControl.FieldName = "Code"
        Me.ColCodeInvetoryControl.Name = "ColCodeInvetoryControl"
        Me.ColCodeInvetoryControl.Visible = True
        Me.ColCodeInvetoryControl.VisibleIndex = 0
        Me.ColCodeInvetoryControl.Width = 407
        '
        'ColDateInvetoryControl
        '
        Me.ColDateInvetoryControl.Caption = "Fecha"
        Me.ColDateInvetoryControl.FieldName = "DocumentDate"
        Me.ColDateInvetoryControl.Name = "ColDateInvetoryControl"
        Me.ColDateInvetoryControl.Visible = True
        Me.ColDateInvetoryControl.VisibleIndex = 1
        Me.ColDateInvetoryControl.Width = 280
        '
        'ColWarehouseInventoryControl
        '
        Me.ColWarehouseInventoryControl.Caption = "Almacén"
        Me.ColWarehouseInventoryControl.FieldName = "WarehouseId.CodeName"
        Me.ColWarehouseInventoryControl.Name = "ColWarehouseInventoryControl"
        Me.ColWarehouseInventoryControl.Visible = True
        Me.ColWarehouseInventoryControl.VisibleIndex = 2
        Me.ColWarehouseInventoryControl.Width = 625
        '
        'INDGcPhysicalInventory
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcPhysicalInventory, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcPhysicalInventory, Nothing)
        Me.INDGcPhysicalInventory.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.INDGcPhysicalInventory, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcPhysicalInventory, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcPhysicalInventory, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcPhysicalInventory, False)
        Me.INDGcPhysicalInventory.Location = New System.Drawing.Point(2118, 53)
        Me.INDGcPhysicalInventory.MainView = Me.INDGcPhysicalInventoryView
        Me.INDGcPhysicalInventory.Name = "INDGcPhysicalInventory"
        Me.INDGcPhysicalInventory.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1})
        Me.INDGcPhysicalInventory.Size = New System.Drawing.Size(824, 497)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcPhysicalInventory, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDGcPhysicalInventory.TabIndex = 17
        Me.INDGcPhysicalInventory.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGcPhysicalInventoryView})
        '
        'INDGcPhysicalInventoryView
        '
        Me.INDGcPhysicalInventoryView.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGcPhysicalInventoryView.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGcPhysicalInventoryView.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDGcPhysicalInventoryView.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGcPhysicalInventoryView.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGcPhysicalInventoryView.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGcPhysicalInventoryView.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGcPhysicalInventoryView.Appearance.GroupRow.Options.UseFont = True
        Me.INDGcPhysicalInventoryView.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGcPhysicalInventoryView.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGcPhysicalInventoryView.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGcPhysicalInventoryView.Appearance.Row.Options.UseFont = True
        Me.INDGcPhysicalInventoryView.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGcPhysicalInventoryView.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGcPhysicalInventoryView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGclState, Me.ColProductPhysical, Me.ColBatchPhysical, Me.ColInventoryQuantityPhysical, Me.ColQuantityPhysical, Me.ColStatusPhysical})
        Me.INDGcPhysicalInventoryView.GridControl = Me.INDGcPhysicalInventory
        Me.INDGcPhysicalInventoryView.Name = "INDGcPhysicalInventoryView"
        Me.INDGcPhysicalInventoryView.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGcPhysicalInventoryView.OptionsView.EnableAppearanceOddRow = True
        Me.INDGcPhysicalInventoryView.OptionsView.ShowAutoFilterRow = True
        Me.INDGcPhysicalInventoryView.OptionsView.ShowDetailButtons = False
        Me.INDGcPhysicalInventoryView.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGcPhysicalInventoryView, False)
        '
        'INDGclState
        '
        Me.INDGclState.Caption = "Sel."
        Me.INDGclState.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.INDGclState.FieldName = "Selected"
        Me.INDGclState.ImageOptions.Alignment = System.Drawing.StringAlignment.Center
        Me.INDGclState.ImageOptions.Image = Global.Presentation.Inventory.My.Resources.Resources.undcheck
        Me.INDGclState.Name = "INDGclState"
        Me.INDGclState.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGclState.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGclState.OptionsColumn.AllowMove = False
        Me.INDGclState.OptionsColumn.AllowShowHide = False
        Me.INDGclState.OptionsColumn.AllowSize = False
        Me.INDGclState.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGclState.OptionsColumn.FixedWidth = True
        Me.INDGclState.OptionsFilter.AllowAutoFilter = False
        Me.INDGclState.OptionsFilter.AllowFilter = False
        Me.INDGclState.Visible = True
        Me.INDGclState.VisibleIndex = 0
        Me.INDGclState.Width = 52
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        '
        'ColProductPhysical
        '
        Me.ColProductPhysical.Caption = "Producto"
        Me.ColProductPhysical.FieldName = "ProductCodeName"
        Me.ColProductPhysical.Name = "ColProductPhysical"
        Me.ColProductPhysical.OptionsColumn.AllowEdit = False
        Me.ColProductPhysical.OptionsColumn.AllowFocus = False
        Me.ColProductPhysical.OptionsColumn.AllowMove = False
        Me.ColProductPhysical.OptionsColumn.AllowSize = False
        Me.ColProductPhysical.Visible = True
        Me.ColProductPhysical.VisibleIndex = 1
        Me.ColProductPhysical.Width = 283
        '
        'ColBatchPhysical
        '
        Me.ColBatchPhysical.Caption = "Lote/Serial"
        Me.ColBatchPhysical.FieldName = "BatchSerialCode"
        Me.ColBatchPhysical.Name = "ColBatchPhysical"
        Me.ColBatchPhysical.OptionsColumn.AllowEdit = False
        Me.ColBatchPhysical.OptionsColumn.AllowFocus = False
        Me.ColBatchPhysical.OptionsColumn.AllowMove = False
        Me.ColBatchPhysical.OptionsColumn.AllowSize = False
        Me.ColBatchPhysical.Visible = True
        Me.ColBatchPhysical.VisibleIndex = 2
        Me.ColBatchPhysical.Width = 175
        '
        'ColInventoryQuantityPhysical
        '
        Me.ColInventoryQuantityPhysical.Caption = "Cant. Física"
        Me.ColInventoryQuantityPhysical.FieldName = "InventoryQuantity"
        Me.ColInventoryQuantityPhysical.Name = "ColInventoryQuantityPhysical"
        Me.ColInventoryQuantityPhysical.OptionsColumn.AllowEdit = False
        Me.ColInventoryQuantityPhysical.OptionsColumn.AllowFocus = False
        Me.ColInventoryQuantityPhysical.OptionsColumn.AllowMove = False
        Me.ColInventoryQuantityPhysical.OptionsColumn.AllowSize = False
        Me.ColInventoryQuantityPhysical.Visible = True
        Me.ColInventoryQuantityPhysical.VisibleIndex = 3
        Me.ColInventoryQuantityPhysical.Width = 107
        '
        'ColQuantityPhysical
        '
        Me.ColQuantityPhysical.Caption = "Cantidad"
        Me.ColQuantityPhysical.FieldName = "Quantity"
        Me.ColQuantityPhysical.Name = "ColQuantityPhysical"
        Me.ColQuantityPhysical.OptionsColumn.AllowEdit = False
        Me.ColQuantityPhysical.OptionsColumn.AllowFocus = False
        Me.ColQuantityPhysical.OptionsColumn.AllowMove = False
        Me.ColQuantityPhysical.OptionsColumn.AllowSize = False
        Me.ColQuantityPhysical.Visible = True
        Me.ColQuantityPhysical.VisibleIndex = 4
        Me.ColQuantityPhysical.Width = 96
        '
        'ColStatusPhysical
        '
        Me.ColStatusPhysical.Caption = "Estado"
        Me.ColStatusPhysical.FieldName = "StatusName"
        Me.ColStatusPhysical.Name = "ColStatusPhysical"
        Me.ColStatusPhysical.OptionsColumn.AllowEdit = False
        Me.ColStatusPhysical.OptionsColumn.AllowFocus = False
        Me.ColStatusPhysical.OptionsColumn.AllowMove = False
        Me.ColStatusPhysical.OptionsColumn.AllowSize = False
        Me.ColStatusPhysical.Visible = True
        Me.ColStatusPhysical.VisibleIndex = 5
        Me.ColStatusPhysical.Width = 93
        '
        'INDBtnAddProducts
        '
        Me.INDBtnAddProducts.Location = New System.Drawing.Point(852, 53)
        Me.INDBtnAddProducts.Name = "INDBtnAddProducts"
        Me.INDBtnAddProducts.Size = New System.Drawing.Size(824, 32)
        Me.INDBtnAddProducts.StyleController = Me.INDLyInventoryAdjustments
        Me.INDBtnAddProducts.TabIndex = 16
        Me.INDBtnAddProducts.Text = "Agregar Productos"
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
        Me.INDGcProducts.Location = New System.Drawing.Point(852, 89)
        Me.INDGcProducts.MainView = Me.INDGvProducts
        Me.INDGcProducts.Name = "INDGcProducts"
        Me.INDGcProducts.Size = New System.Drawing.Size(824, 461)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcProducts, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDGcProducts.TabIndex = 13
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
        Me.INDGvProducts.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColProduct, Me.ColConcept, Me.GridColumn4, Me.ColUnid, Me.ColUnitValue, Me.ColQuantity, Me.GridColumn1})
        Me.INDGvProducts.GridControl = Me.INDGcProducts
        Me.INDGvProducts.Name = "INDGvProducts"
        Me.INDGvProducts.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvProducts.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvProducts.OptionsView.ShowAutoFilterRow = True
        Me.INDGvProducts.OptionsView.ShowDetailButtons = False
        Me.INDGvProducts.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvProducts, False)
        '
        'ColProduct
        '
        Me.ColProduct.Caption = "Producto"
        Me.ColProduct.FieldName = "ProductCodeName"
        Me.ColProduct.Name = "ColProduct"
        Me.ColProduct.OptionsColumn.AllowEdit = False
        Me.ColProduct.OptionsColumn.AllowFocus = False
        Me.ColProduct.OptionsColumn.AllowMove = False
        Me.ColProduct.OptionsColumn.AllowSize = False
        Me.ColProduct.Visible = True
        Me.ColProduct.VisibleIndex = 0
        Me.ColProduct.Width = 300
        '
        'ColConcept
        '
        Me.ColConcept.Caption = "Concepto"
        Me.ColConcept.FieldName = "CodeNameAdjustmentConcept"
        Me.ColConcept.Name = "ColConcept"
        Me.ColConcept.OptionsColumn.AllowEdit = False
        Me.ColConcept.Visible = True
        Me.ColConcept.VisibleIndex = 1
        Me.ColConcept.Width = 181
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "U. Consumo"
        Me.GridColumn4.FieldName = "consumptionUnit"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        Me.GridColumn4.Width = 84
        '
        'ColUnid
        '
        Me.ColUnid.Caption = "Unidad"
        Me.ColUnid.FieldName = "ProductUnid"
        Me.ColUnid.Name = "ColUnid"
        Me.ColUnid.OptionsColumn.AllowEdit = False
        Me.ColUnid.OptionsColumn.AllowFocus = False
        Me.ColUnid.OptionsColumn.AllowMove = False
        Me.ColUnid.OptionsColumn.AllowSize = False
        Me.ColUnid.Visible = True
        Me.ColUnid.VisibleIndex = 2
        Me.ColUnid.Width = 131
        '
        'ColUnitValue
        '
        Me.ColUnitValue.Caption = "V. Unitario"
        Me.ColUnitValue.DisplayFormat.FormatString = "c2"
        Me.ColUnitValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ColUnitValue.FieldName = "UnitValue"
        Me.ColUnitValue.Name = "ColUnitValue"
        Me.ColUnitValue.OptionsColumn.AllowEdit = False
        Me.ColUnitValue.OptionsColumn.AllowFocus = False
        Me.ColUnitValue.OptionsColumn.AllowMove = False
        Me.ColUnitValue.OptionsColumn.AllowSize = False
        Me.ColUnitValue.Visible = True
        Me.ColUnitValue.VisibleIndex = 3
        Me.ColUnitValue.Width = 101
        '
        'ColQuantity
        '
        Me.ColQuantity.Caption = "Cantidad"
        Me.ColQuantity.FieldName = "Quantity"
        Me.ColQuantity.Name = "ColQuantity"
        Me.ColQuantity.OptionsColumn.AllowEdit = False
        Me.ColQuantity.OptionsColumn.AllowFocus = False
        Me.ColQuantity.OptionsColumn.AllowMove = False
        Me.ColQuantity.OptionsColumn.AllowSize = False
        Me.ColQuantity.Visible = True
        Me.ColQuantity.VisibleIndex = 4
        Me.ColQuantity.Width = 86
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Centro de costo"
        Me.GridColumn1.FieldName = "CodeNameCostCenter"
        Me.GridColumn1.Name = "GridColumn1"
        '
        'INDTxtDetail
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtDetail, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtDetail, True)
        Me.INDTxtDetail.Location = New System.Drawing.Point(132, 233)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtDetail, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtDetail.Name = "INDTxtDetail"
        Me.INDTxtDetail.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDTxtDetail.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtDetail.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDTxtDetail.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtDetail.Properties.Appearance.Options.UseFont = True
        Me.INDTxtDetail.Properties.Appearance.Options.UseForeColor = True
        Me.INDTxtDetail.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtDetail.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtDetail.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtDetail.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtDetail.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtDetail.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtDetail.Properties.MaxLength = 500
        Me.INDTxtDetail.Size = New System.Drawing.Size(278, 68)
        Me.INDTxtDetail.StyleController = Me.INDLyInventoryAdjustments
        Me.INDTxtDetail.TabIndex = 12
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtDetail, 0)
        Me.INDTxtDetail.ToolTip = "Este Campo es Necesario"
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
        Me.INDSleWarehouse.EditValue = "302"
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleWarehouse, False)
        Me.INDSleWarehouse.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleWarehouse, False)
        Me.INDSleWarehouse.Location = New System.Drawing.Point(546, 125)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleWarehouse, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleWarehouse.Name = "INDSleWarehouse"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleWarehouse, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleWarehouse, False)
        Me.INDSleWarehouse.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleWarehouse.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleWarehouse.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleWarehouse.Properties.Appearance.Options.UseFont = True
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
        Me.INDSleWarehouse.Properties.PopupView = Me.INDSleWareHouseView
        Me.INDSleWarehouse.Properties.ShowFooter = False
        Me.INDSleWarehouse.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleWarehouse, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleWarehouse, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleWarehouse, True)
        Me.INDSleWarehouse.Size = New System.Drawing.Size(278, 28)
        Me.INDSleWarehouse.StyleController = Me.INDLyInventoryAdjustments
        Me.INDSleWarehouse.TabIndex = 8
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleWarehouse, "302")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleWarehouse, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleWarehouse, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleWarehouse, False)
        '
        'INDSleWareHouseView
        '
        Me.INDSleWareHouseView.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDSleWareHouseView.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDSleWareHouseView.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDSleWareHouseView.Appearance.FocusedRow.Options.UseFont = True
        Me.INDSleWareHouseView.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDSleWareHouseView.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleWareHouseView.Appearance.GroupRow.Options.UseFont = True
        Me.INDSleWareHouseView.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleWareHouseView.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDSleWareHouseView.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDSleWareHouseView.Appearance.Row.Options.UseFont = True
        Me.INDSleWareHouseView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColCodeWarehouse, Me.ColNameWarehouse})
        Me.INDSleWareHouseView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDSleWareHouseView.Name = "INDSleWareHouseView"
        Me.INDSleWareHouseView.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDSleWareHouseView.OptionsView.EnableAppearanceEvenRow = True
        Me.INDSleWareHouseView.OptionsView.EnableAppearanceOddRow = True
        Me.INDSleWareHouseView.OptionsView.ShowAutoFilterRow = True
        Me.INDSleWareHouseView.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDSleWareHouseView, False)
        '
        'ColCodeWarehouse
        '
        Me.ColCodeWarehouse.Caption = "Código"
        Me.ColCodeWarehouse.FieldName = "Code"
        Me.ColCodeWarehouse.Name = "ColCodeWarehouse"
        Me.ColCodeWarehouse.OptionsColumn.AllowEdit = False
        Me.ColCodeWarehouse.OptionsColumn.AllowFocus = False
        Me.ColCodeWarehouse.OptionsColumn.AllowMove = False
        Me.ColCodeWarehouse.OptionsColumn.AllowSize = False
        Me.ColCodeWarehouse.Visible = True
        Me.ColCodeWarehouse.VisibleIndex = 0
        Me.ColCodeWarehouse.Width = 371
        '
        'ColNameWarehouse
        '
        Me.ColNameWarehouse.Caption = "Nombre"
        Me.ColNameWarehouse.FieldName = "Name"
        Me.ColNameWarehouse.Name = "ColNameWarehouse"
        Me.ColNameWarehouse.OptionsColumn.AllowEdit = False
        Me.ColNameWarehouse.OptionsColumn.AllowFocus = False
        Me.ColNameWarehouse.OptionsColumn.AllowMove = False
        Me.ColNameWarehouse.OptionsColumn.AllowSize = False
        Me.ColNameWarehouse.Visible = True
        Me.ColNameWarehouse.VisibleIndex = 1
        Me.ColNameWarehouse.Width = 941
        '
        'INDSleConcept
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleConcept, AppearanceObject5)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleConcept, AppearanceObject6)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleConcept, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleConcept, True)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleConcept, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleConcept, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleConcept, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleConcept, False)
        Me.INDSleConcept.EditValue = ""
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleConcept, False)
        Me.INDSleConcept.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleConcept, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleConcept, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleConcept, False)
        Me.INDSleConcept.Location = New System.Drawing.Point(546, 53)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleConcept, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleConcept.Name = "INDSleConcept"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleConcept, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleConcept, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleConcept, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDSleConcept, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleConcept, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleConcept, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleConcept, False)
        Me.INDSleConcept.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleConcept.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleConcept.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleConcept.Properties.Appearance.Options.UseFont = True
        Me.INDSleConcept.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleConcept.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSleConcept.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleConcept.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSleConcept.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSleConcept.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleConcept.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleConcept.Properties.DisplayMember = "CodeName"
        Me.INDSleConcept.Properties.NullText = ""
        Me.INDSleConcept.Properties.PopupSizeable = False
        Me.INDSleConcept.Properties.PopupView = Me.INDGleConceptsView
        Me.INDSleConcept.Properties.ShowFooter = False
        Me.INDSleConcept.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleConcept, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleConcept, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleConcept, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleConcept, True)
        Me.INDSleConcept.Size = New System.Drawing.Size(278, 28)
        Me.INDSleConcept.StyleController = Me.INDLyInventoryAdjustments
        Me.INDSleConcept.TabIndex = 7
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleConcept, "307")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleConcept, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleConcept, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleConcept, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleConcept, False)
        '
        'INDGleConceptsView
        '
        Me.INDGleConceptsView.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGleConceptsView.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGleConceptsView.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGleConceptsView.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGleConceptsView.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGleConceptsView.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGleConceptsView.Appearance.GroupRow.Options.UseFont = True
        Me.INDGleConceptsView.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGleConceptsView.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGleConceptsView.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGleConceptsView.Appearance.Row.Options.UseFont = True
        Me.INDGleConceptsView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColCodeConcept, Me.ColNameConcept, Me.ColTypeConcept})
        Me.INDGleConceptsView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGleConceptsView.Name = "INDGleConceptsView"
        Me.INDGleConceptsView.OptionsCustomization.AllowGroup = False
        Me.INDGleConceptsView.OptionsDetail.EnableMasterViewMode = False
        Me.INDGleConceptsView.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGleConceptsView.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGleConceptsView.OptionsView.EnableAppearanceOddRow = True
        Me.INDGleConceptsView.OptionsView.ShowAutoFilterRow = True
        Me.INDGleConceptsView.OptionsView.ShowDetailButtons = False
        Me.INDGleConceptsView.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGleConceptsView, False)
        '
        'ColCodeConcept
        '
        Me.ColCodeConcept.Caption = "Código"
        Me.ColCodeConcept.FieldName = "Code"
        Me.ColCodeConcept.Name = "ColCodeConcept"
        Me.ColCodeConcept.OptionsColumn.AllowEdit = False
        Me.ColCodeConcept.Visible = True
        Me.ColCodeConcept.VisibleIndex = 0
        Me.ColCodeConcept.Width = 348
        '
        'ColNameConcept
        '
        Me.ColNameConcept.Caption = "Nombre"
        Me.ColNameConcept.FieldName = "Name"
        Me.ColNameConcept.Name = "ColNameConcept"
        Me.ColNameConcept.OptionsColumn.AllowEdit = False
        Me.ColNameConcept.Visible = True
        Me.ColNameConcept.VisibleIndex = 1
        Me.ColNameConcept.Width = 736
        '
        'ColTypeConcept
        '
        Me.ColTypeConcept.Caption = "Tipo"
        Me.ColTypeConcept.FieldName = "ConceptTypeName"
        Me.ColTypeConcept.Name = "ColTypeConcept"
        Me.ColTypeConcept.OptionsColumn.AllowEdit = False
        Me.ColTypeConcept.Visible = True
        Me.ColTypeConcept.VisibleIndex = 2
        Me.ColTypeConcept.Width = 228
        '
        'INDGleAdjustmentType
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDGleAdjustmentType, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleAdjustmentType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleAdjustmentType, True)
        Me.INDGleAdjustmentType.EnterMoveNextControl = True
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDGleAdjustmentType, False)
        Me.INDGleAdjustmentType.Location = New System.Drawing.Point(132, 125)
        Me.IndigoTextEdit1.SetMascara(Me.INDGleAdjustmentType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleAdjustmentType.Name = "INDGleAdjustmentType"
        Me.INDGleAdjustmentType.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDGleAdjustmentType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGleAdjustmentType.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDGleAdjustmentType.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleAdjustmentType.Properties.Appearance.Options.UseFont = True
        Me.INDGleAdjustmentType.Properties.Appearance.Options.UseForeColor = True
        Me.INDGleAdjustmentType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDGleAdjustmentType.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDGleAdjustmentType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDGleAdjustmentType.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDGleAdjustmentType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleAdjustmentType.Properties.DisplayMember = "Item2"
        Me.INDGleAdjustmentType.Properties.ImmediatePopup = True
        Me.INDGleAdjustmentType.Properties.NullText = ""
        Me.INDGleAdjustmentType.Properties.PopupView = Me.INDGlvAdjustmentType
        Me.INDGleAdjustmentType.Properties.ValueMember = "Item1"
        Me.INDGleAdjustmentType.Size = New System.Drawing.Size(278, 28)
        Me.INDGleAdjustmentType.StyleController = Me.INDLyInventoryAdjustments
        Me.INDGleAdjustmentType.TabIndex = 6
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDGleAdjustmentType, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleAdjustmentType, 0)
        Me.INDGleAdjustmentType.ToolTip = "Este Campo es Necesario"
        '
        'INDGlvAdjustmentType
        '
        Me.INDGlvAdjustmentType.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGlvAdjustmentType.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGlvAdjustmentType.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGlvAdjustmentType.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGlvAdjustmentType.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGlvAdjustmentType.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGlvAdjustmentType.Appearance.GroupRow.Options.UseFont = True
        Me.INDGlvAdjustmentType.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGlvAdjustmentType.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGlvAdjustmentType.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGlvAdjustmentType.Appearance.Row.Options.UseFont = True
        Me.INDGlvAdjustmentType.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColAdjustmentType})
        Me.INDGlvAdjustmentType.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGlvAdjustmentType.Name = "INDGlvAdjustmentType"
        Me.INDGlvAdjustmentType.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGlvAdjustmentType.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGlvAdjustmentType.OptionsView.EnableAppearanceOddRow = True
        Me.INDGlvAdjustmentType.OptionsView.ShowAutoFilterRow = True
        Me.INDGlvAdjustmentType.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGlvAdjustmentType, False)
        '
        'ColAdjustmentType
        '
        Me.ColAdjustmentType.Caption = "Tipo de Ajuste"
        Me.ColAdjustmentType.FieldName = "Item2"
        Me.ColAdjustmentType.Name = "ColAdjustmentType"
        Me.ColAdjustmentType.Visible = True
        Me.ColAdjustmentType.VisibleIndex = 0
        '
        'INDDteDocumentDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDDteDocumentDate, True)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDDteDocumentDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDDteDocumentDate, True)
        Me.INDDteDocumentDate.EditValue = Nothing
        Me.INDDteDocumentDate.EnterMoveNextControl = True
        Me.INDDteDocumentDate.Location = New System.Drawing.Point(132, 89)
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
        Me.INDDteDocumentDate.Size = New System.Drawing.Size(278, 28)
        Me.INDDteDocumentDate.StyleController = Me.INDLyInventoryAdjustments
        Me.INDDteDocumentDate.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDDteDocumentDate, 0)
        Me.INDDteDocumentDate.ToolTip = "Este Campo es Necesario"
        '
        'INDBtnCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDBtnCode, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDBtnCode, True)
        Me.INDBtnCode.Location = New System.Drawing.Point(132, 53)
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
        Me.INDBtnCode.Size = New System.Drawing.Size(278, 28)
        Me.INDBtnCode.StyleController = Me.INDLyInventoryAdjustments
        Me.INDBtnCode.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDBtnCode, 0)
        Me.INDBtnCode.ToolTip = "Este Campo es Necesario"
        '
        'INDSleCostCenter
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleCostCenter, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleCostCenter, False)
        Me.INDSleCostCenter.EnterMoveNextControl = True
        Me.INDSleCostCenter.Location = New System.Drawing.Point(546, 89)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleCostCenter, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleCostCenter.Name = "INDSleCostCenter"
        Me.INDSleCostCenter.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleCostCenter.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleCostCenter.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleCostCenter.Properties.Appearance.Options.UseFont = True
        Me.INDSleCostCenter.Properties.ReadOnly = True
        Me.INDSleCostCenter.Size = New System.Drawing.Size(278, 28)
        Me.INDSleCostCenter.StyleController = Me.INDLyInventoryAdjustments
        Me.INDSleCostCenter.TabIndex = 15
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleCostCenter, 0)
        '
        'INDsleAdmissionNumber
        '
        Me.INDsleAdmissionNumber._flagLoadEditValue = False
        Me.INDsleAdmissionNumber.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleAdmissionNumber.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDsleAdmissionNumber.Appearance.Options.UseBackColor = True
        Me.INDsleAdmissionNumber.Appearance.Options.UseFont = True
        Me.INDsleAdmissionNumber.Datasource = Nothing
        Me.INDsleAdmissionNumber.IsReadOnly = False
        Me.INDsleAdmissionNumber.Location = New System.Drawing.Point(132, 161)
        Me.INDsleAdmissionNumber.Margin = New System.Windows.Forms.Padding(0)
        Me.INDsleAdmissionNumber.MaximumSize = New System.Drawing.Size(0, 28)
        Me.INDsleAdmissionNumber.MinimumSize = New System.Drawing.Size(70, 28)
        Me.INDsleAdmissionNumber.Name = "INDsleAdmissionNumber"
        Me.INDsleAdmissionNumber.OpenFormAction = Nothing
        Me.INDsleAdmissionNumber.PopupContainerControl = Nothing
        Me.INDsleAdmissionNumber.Size = New System.Drawing.Size(278, 28)
        Me.INDsleAdmissionNumber.TabIndex = 2
        Me.INDsleAdmissionNumber.TagForm = ""
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
        Me.LayoutControlGroup1.CustomizationFormText = "Ajustes de Inventario"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LyGroupMainData, Me.LyGroupInfo, Me.LyGroupProducts, Me.LyInventoryControl, Me.LyInventoryControlProducts})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(2966, 574)
        Me.LayoutControlGroup1.TextVisible = False
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
        Me.LyGroupMainData.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyBtnCode, Me.INDLyDteDocumentDate, Me.INDLyGleAdjustmentType, Me.INDLyTxtDetail, Me.INDlciThirdParty, Me.LciAdmissionNumber})
        Me.LyGroupMainData.Location = New System.Drawing.Point(0, 0)
        Me.LyGroupMainData.Name = "LyGroupMainData"
        Me.LyGroupMainData.Size = New System.Drawing.Size(414, 554)
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
        Me.INDLyBtnCode.TextSize = New System.Drawing.Size(105, 17)
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
        Me.INDLyDteDocumentDate.TextSize = New System.Drawing.Size(105, 17)
        '
        'INDLyGleAdjustmentType
        '
        Me.INDLyGleAdjustmentType.Control = Me.INDGleAdjustmentType
        Me.INDLyGleAdjustmentType.CustomizationFormText = "Tipo Ajuste"
        Me.INDLyGleAdjustmentType.Location = New System.Drawing.Point(0, 72)
        Me.INDLyGleAdjustmentType.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDLyGleAdjustmentType.MinSize = New System.Drawing.Size(390, 36)
        Me.INDLyGleAdjustmentType.Name = "INDLyGleAdjustmentType"
        Me.INDLyGleAdjustmentType.ShowInCustomizationForm = False
        Me.INDLyGleAdjustmentType.Size = New System.Drawing.Size(390, 36)
        Me.INDLyGleAdjustmentType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyGleAdjustmentType.Text = "Tipo Ajuste"
        Me.INDLyGleAdjustmentType.TextSize = New System.Drawing.Size(105, 17)
        '
        'INDLyTxtDetail
        '
        Me.INDLyTxtDetail.Control = Me.INDTxtDetail
        Me.INDLyTxtDetail.CustomizationFormText = "Detalle"
        Me.INDLyTxtDetail.Location = New System.Drawing.Point(0, 180)
        Me.INDLyTxtDetail.MaxSize = New System.Drawing.Size(390, 72)
        Me.INDLyTxtDetail.MinSize = New System.Drawing.Size(390, 36)
        Me.INDLyTxtDetail.Name = "INDLyTxtDetail"
        Me.INDLyTxtDetail.ShowInCustomizationForm = False
        Me.INDLyTxtDetail.Size = New System.Drawing.Size(390, 321)
        Me.INDLyTxtDetail.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyTxtDetail.Tag = ""
        Me.INDLyTxtDetail.Text = "Detalle"
        Me.INDLyTxtDetail.TextSize = New System.Drawing.Size(105, 17)
        '
        'INDlciThirdParty
        '
        Me.INDlciThirdParty.Control = Me.INDSleThirdParty
        Me.INDlciThirdParty.CustomizationFormText = "Tercero"
        Me.INDlciThirdParty.Location = New System.Drawing.Point(0, 144)
        Me.INDlciThirdParty.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlciThirdParty.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlciThirdParty.Name = "INDlciThirdParty"
        Me.INDlciThirdParty.Size = New System.Drawing.Size(390, 36)
        Me.INDlciThirdParty.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciThirdParty.Text = "Tercero"
        Me.INDlciThirdParty.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciThirdParty.TextSize = New System.Drawing.Size(105, 17)
        Me.INDlciThirdParty.TextToControlDistance = 3
        '
        'LciAdmissionNumber
        '
        Me.LciAdmissionNumber.Control = Me.INDsleAdmissionNumber
        Me.LciAdmissionNumber.CustomizationFormText = "Ingreso"
        Me.LciAdmissionNumber.Location = New System.Drawing.Point(0, 108)
        Me.LciAdmissionNumber.MaxSize = New System.Drawing.Size(390, 36)
        Me.LciAdmissionNumber.MinSize = New System.Drawing.Size(390, 36)
        Me.LciAdmissionNumber.Name = "LciAdmissionNumber"
        Me.LciAdmissionNumber.ShowInCustomizationForm = False
        Me.LciAdmissionNumber.Size = New System.Drawing.Size(390, 36)
        Me.LciAdmissionNumber.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LciAdmissionNumber.Text = "Ingreso"
        Me.LciAdmissionNumber.TextLocation = DevExpress.Utils.Locations.Left
        Me.LciAdmissionNumber.TextSize = New System.Drawing.Size(105, 17)
        Me.LciAdmissionNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'LyGroupInfo
        '
        Me.LyGroupInfo.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LyGroupInfo.AppearanceGroup.Options.UseFont = True
        Me.LyGroupInfo.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LyGroupInfo.AppearanceItemCaption.Options.UseFont = True
        Me.LyGroupInfo.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LyGroupInfo.AppearanceTabPage.Header.Options.UseFont = True
        Me.LyGroupInfo.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LyGroupInfo.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LyGroupInfo.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LyGroupInfo.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LyGroupInfo.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LyGroupInfo.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LyGroupInfo.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LyGroupInfo.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LyGroupInfo, False)
        Me.LyGroupInfo.CustomizationFormText = "Información Relacionada"
        Me.LyGroupInfo.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLySleConcept, Me.INDLySleWarehouse, Me.INDLySleCostCenter})
        Me.LyGroupInfo.Location = New System.Drawing.Point(414, 0)
        Me.LyGroupInfo.Name = "LyGroupInfo"
        Me.LyGroupInfo.Size = New System.Drawing.Size(414, 554)
        Me.LyGroupInfo.Text = "Información Relacionada"
        '
        'INDLySleConcept
        '
        Me.INDLySleConcept.Control = Me.INDSleConcept
        Me.INDLySleConcept.CustomizationFormText = "Concepto"
        Me.INDLySleConcept.Location = New System.Drawing.Point(0, 0)
        Me.INDLySleConcept.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDLySleConcept.MinSize = New System.Drawing.Size(390, 36)
        Me.INDLySleConcept.Name = "INDLySleConcept"
        Me.INDLySleConcept.ShowInCustomizationForm = False
        Me.INDLySleConcept.Size = New System.Drawing.Size(390, 36)
        Me.INDLySleConcept.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLySleConcept.Text = "Concepto"
        Me.INDLySleConcept.TextSize = New System.Drawing.Size(105, 17)
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
        Me.INDLySleWarehouse.Size = New System.Drawing.Size(390, 429)
        Me.INDLySleWarehouse.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLySleWarehouse.Text = "Almacén"
        Me.INDLySleWarehouse.TextSize = New System.Drawing.Size(105, 17)
        '
        'INDLySleCostCenter
        '
        Me.INDLySleCostCenter.Control = Me.INDSleCostCenter
        Me.INDLySleCostCenter.CustomizationFormText = "Centro de Costo"
        Me.INDLySleCostCenter.Location = New System.Drawing.Point(0, 36)
        Me.INDLySleCostCenter.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDLySleCostCenter.MinSize = New System.Drawing.Size(390, 36)
        Me.INDLySleCostCenter.Name = "INDLySleCostCenter"
        Me.INDLySleCostCenter.Size = New System.Drawing.Size(390, 36)
        Me.INDLySleCostCenter.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLySleCostCenter.Text = "Centro de Costo"
        Me.INDLySleCostCenter.TextSize = New System.Drawing.Size(105, 17)
        Me.INDLySleCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
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
        Me.LyGroupProducts.CustomizationFormText = "Productos"
        Me.LyGroupProducts.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyGcProducts, Me.INDLyBtnAddProducts})
        Me.LyGroupProducts.Location = New System.Drawing.Point(828, 0)
        Me.LyGroupProducts.Name = "LyGroupProducts"
        Me.LyGroupProducts.Size = New System.Drawing.Size(852, 554)
        Me.LyGroupProducts.Text = "Productos"
        '
        'INDLyGcProducts
        '
        Me.INDLyGcProducts.Control = Me.INDGcProducts
        Me.INDLyGcProducts.CustomizationFormText = "Productos"
        Me.INDLyGcProducts.Location = New System.Drawing.Point(0, 36)
        Me.INDLyGcProducts.MaxSize = New System.Drawing.Size(828, 0)
        Me.INDLyGcProducts.MinSize = New System.Drawing.Size(828, 1)
        Me.INDLyGcProducts.Name = "INDLyGcProducts"
        Me.INDLyGcProducts.Size = New System.Drawing.Size(828, 465)
        Me.INDLyGcProducts.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyGcProducts.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLyGcProducts.TextVisible = False
        '
        'INDLyBtnAddProducts
        '
        Me.INDLyBtnAddProducts.Control = Me.INDBtnAddProducts
        Me.INDLyBtnAddProducts.CustomizationFormText = "Agregar Productos"
        Me.INDLyBtnAddProducts.Location = New System.Drawing.Point(0, 0)
        Me.INDLyBtnAddProducts.MaxSize = New System.Drawing.Size(828, 36)
        Me.INDLyBtnAddProducts.MinSize = New System.Drawing.Size(828, 36)
        Me.INDLyBtnAddProducts.Name = "INDLyBtnAddProducts"
        Me.INDLyBtnAddProducts.Size = New System.Drawing.Size(828, 36)
        Me.INDLyBtnAddProducts.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyBtnAddProducts.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLyBtnAddProducts.TextVisible = False
        '
        'LyInventoryControl
        '
        Me.LyInventoryControl.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LyInventoryControl.AppearanceGroup.Options.UseFont = True
        Me.LyInventoryControl.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LyInventoryControl.AppearanceItemCaption.Options.UseFont = True
        Me.LyInventoryControl.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LyInventoryControl.AppearanceTabPage.Header.Options.UseFont = True
        Me.LyInventoryControl.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LyInventoryControl.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LyInventoryControl.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LyInventoryControl.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LyInventoryControl.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LyInventoryControl.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LyInventoryControl.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LyInventoryControl.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LyInventoryControl, False)
        Me.LyInventoryControl.CustomizationFormText = "Control de Inventario Físico"
        Me.LyInventoryControl.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLySleInventoryControl, Me.INDLyDteDocumentDateInventoryControl, Me.INDLyTxtWarehouseInventoryControl})
        Me.LyInventoryControl.Location = New System.Drawing.Point(1680, 0)
        Me.LyInventoryControl.Name = "LyInventoryControl"
        Me.LyInventoryControl.Size = New System.Drawing.Size(414, 554)
        Me.LyInventoryControl.Text = "Control de Inventario Físico"
        '
        'INDLySleInventoryControl
        '
        Me.INDLySleInventoryControl.Control = Me.INDSleInventoryControl
        Me.INDLySleInventoryControl.CustomizationFormText = "Inventario"
        Me.INDLySleInventoryControl.Location = New System.Drawing.Point(0, 0)
        Me.INDLySleInventoryControl.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDLySleInventoryControl.MinSize = New System.Drawing.Size(390, 36)
        Me.INDLySleInventoryControl.Name = "INDLySleInventoryControl"
        Me.INDLySleInventoryControl.Size = New System.Drawing.Size(390, 36)
        Me.INDLySleInventoryControl.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLySleInventoryControl.Text = "Inventario"
        Me.INDLySleInventoryControl.TextSize = New System.Drawing.Size(105, 17)
        '
        'INDLyDteDocumentDateInventoryControl
        '
        Me.INDLyDteDocumentDateInventoryControl.Control = Me.INDDteDocumentDateInventoryControl
        Me.INDLyDteDocumentDateInventoryControl.CustomizationFormText = "Fecha Doc."
        Me.INDLyDteDocumentDateInventoryControl.Location = New System.Drawing.Point(0, 72)
        Me.INDLyDteDocumentDateInventoryControl.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDLyDteDocumentDateInventoryControl.MinSize = New System.Drawing.Size(390, 36)
        Me.INDLyDteDocumentDateInventoryControl.Name = "INDLyDteDocumentDateInventoryControl"
        Me.INDLyDteDocumentDateInventoryControl.Size = New System.Drawing.Size(390, 429)
        Me.INDLyDteDocumentDateInventoryControl.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyDteDocumentDateInventoryControl.Text = "Fecha Doc."
        Me.INDLyDteDocumentDateInventoryControl.TextSize = New System.Drawing.Size(105, 17)
        '
        'INDLyTxtWarehouseInventoryControl
        '
        Me.INDLyTxtWarehouseInventoryControl.Control = Me.INDTxtWarehouseInventoryControl
        Me.INDLyTxtWarehouseInventoryControl.CustomizationFormText = "Almacén"
        Me.INDLyTxtWarehouseInventoryControl.Location = New System.Drawing.Point(0, 36)
        Me.INDLyTxtWarehouseInventoryControl.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDLyTxtWarehouseInventoryControl.MinSize = New System.Drawing.Size(390, 36)
        Me.INDLyTxtWarehouseInventoryControl.Name = "INDLyTxtWarehouseInventoryControl"
        Me.INDLyTxtWarehouseInventoryControl.Size = New System.Drawing.Size(390, 36)
        Me.INDLyTxtWarehouseInventoryControl.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyTxtWarehouseInventoryControl.Text = "Almacén"
        Me.INDLyTxtWarehouseInventoryControl.TextSize = New System.Drawing.Size(105, 17)
        '
        'LyInventoryControlProducts
        '
        Me.LyInventoryControlProducts.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LyInventoryControlProducts.AppearanceGroup.Options.UseFont = True
        Me.LyInventoryControlProducts.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LyInventoryControlProducts.AppearanceItemCaption.Options.UseFont = True
        Me.LyInventoryControlProducts.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LyInventoryControlProducts.AppearanceTabPage.Header.Options.UseFont = True
        Me.LyInventoryControlProducts.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LyInventoryControlProducts.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LyInventoryControlProducts.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LyInventoryControlProducts.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LyInventoryControlProducts.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LyInventoryControlProducts.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LyInventoryControlProducts.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LyInventoryControlProducts.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LyInventoryControlProducts, False)
        Me.LyInventoryControlProducts.CustomizationFormText = "Inventario Físico"
        Me.LyInventoryControlProducts.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyGcPhysicalInventory})
        Me.LyInventoryControlProducts.Location = New System.Drawing.Point(2094, 0)
        Me.LyInventoryControlProducts.Name = "LyInventoryControlProducts"
        Me.LyInventoryControlProducts.Size = New System.Drawing.Size(852, 554)
        Me.LyInventoryControlProducts.Text = "Inventario Físico"
        '
        'INDLyGcPhysicalInventory
        '
        Me.INDLyGcPhysicalInventory.Control = Me.INDGcPhysicalInventory
        Me.INDLyGcPhysicalInventory.CustomizationFormText = "Productos"
        Me.INDLyGcPhysicalInventory.Location = New System.Drawing.Point(0, 0)
        Me.INDLyGcPhysicalInventory.MaxSize = New System.Drawing.Size(828, 0)
        Me.INDLyGcPhysicalInventory.MinSize = New System.Drawing.Size(828, 1)
        Me.INDLyGcPhysicalInventory.Name = "INDLyGcPhysicalInventory"
        Me.INDLyGcPhysicalInventory.Size = New System.Drawing.Size(828, 501)
        Me.INDLyGcPhysicalInventory.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyGcPhysicalInventory.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLyGcPhysicalInventory.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'FrmInventoryAdjustments
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1298, 737)
        Me.IconOptions.ShowIcon = False
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "FrmInventoryAdjustments"
        Me.Opacity = 1.0R
        Me.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.Tag = "318"
        Me.Text = "Ajustes de Inventario"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyInventoryAdjustments, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLyInventoryAdjustments.ResumeLayout(False)
        CType(Me.INDPccMoreInfoAdmission, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPccMoreInfoAdmission.ResumeLayout(False)
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl2.ResumeLayout(False)
        CType(Me.PopupContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PopupContainerControl1.ResumeLayout(False)
        CType(Me.INDFpAdmission, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDFpAdmission.ResumeLayout(False)
        CType(Me.LayoutControl6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl6.ResumeLayout(False)
        CType(Me.INDTxtAdmissionPopup.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtAuthorizationNumber.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtLiquidationType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtEntity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtAdmissionType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtResponsiblePhone.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtAdmissionDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtAdmissionPlace.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtStay.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtPatient.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtAdmissionCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtBenefitsPlan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtResponsibleName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TabbedControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciStay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleThirdParty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEditExView8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDteDocumentDateInventoryControl.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDteDocumentDateInventoryControl.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtWarehouseInventoryControl.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleInventoryControl.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleInventoryControlview, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcPhysicalInventory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcPhysicalInventoryView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcProducts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvProducts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtDetail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleWarehouse.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleWareHouseView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleConcept.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleConceptsView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleAdjustmentType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGlvAdjustmentType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDteDocumentDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDteDocumentDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDBtnCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleCostCenter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LyGroupMainData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyBtnCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyDteDocumentDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyGleAdjustmentType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyTxtDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciThirdParty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LciAdmissionNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LyGroupInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLySleConcept, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLySleWarehouse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLySleCostCenter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LyGroupProducts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyGcProducts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyBtnAddProducts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LyInventoryControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLySleInventoryControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyDteDocumentDateInventoryControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyTxtWarehouseInventoryControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LyInventoryControlProducts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyGcPhysicalInventory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoDate1,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents INDLyInventoryAdjustments As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDDteDocumentDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDBtnCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDLyBtnCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyDteDocumentDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGcProducts As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvProducts As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDTxtDetail As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents INDSleWarehouse As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDSleWareHouseView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDSleConcept As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGleConceptsView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDGleAdjustmentType As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents INDGlvAdjustmentType As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLyGleAdjustmentType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLySleConcept As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLySleWarehouse As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyTxtDetail As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyGcProducts As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents LyGroupMainData As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LyGroupInfo As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LyGroupProducts As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoPopUpContainerEdit1 As Presentation.Controls.IndigoPopUpContainerEdit
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents ColProduct As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColUnid As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColAdjustmentType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLySleCostCenter As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDBtnAddProducts As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDLyBtnAddProducts As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents ColCodeWarehouse As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColNameWarehouse As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents ColCodeConcept As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColNameConcept As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColTypeConcept As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSleCostCenter As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ColUnitValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGcPhysicalInventory As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGcPhysicalInventoryView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LyInventoryControlProducts As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLyGcPhysicalInventory As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSleInventoryControl As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDSleInventoryControlview As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LyInventoryControl As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLySleInventoryControl As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDDteDocumentDateInventoryControl As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDTxtWarehouseInventoryControl As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLyTxtWarehouseInventoryControl As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyDteDocumentDateInventoryControl As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents ColCodeInvetoryControl As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColDateInvetoryControl As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColWarehouseInventoryControl As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGclState As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColProductPhysical As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColBatchPhysical As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColInventoryQuantityPhysical As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColQuantityPhysical As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColStatusPhysical As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents INDSleThirdParty As Presentation.Controls.SearchLookUpEditEx
    Friend WithEvents SearchLookUpEditExView8 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlciThirdParty As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDsleAdmissionNumber As CtrSearchLookUpEditWithPopUp
    Friend WithEvents LciAdmissionNumber As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDPccMoreInfoAdmission As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents LayoutControl2 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDTxtAuthorizationNumber As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtLiquidationType As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtEntity As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtAdmissionType As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtResponsiblePhone As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtAdmissionDate As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtAdmissionPlace As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtStay As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtPatient As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtAdmissionCode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtBenefitsPlan As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtResponsibleName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlGroup4 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents TabbedControlGroup1 As DevExpress.XtraLayout.TabbedControlGroup
    Friend WithEvents LayoutControlGroup6 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem14 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem11 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem13 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem16 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem10 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciStay As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem19 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem15 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents PopupContainerControl1 As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents INDFpAdmission As DevExpress.Utils.FlyoutPanel
    Friend WithEvents LayoutControl6 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDTxtAdmissionPopup As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlGroup8 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem17 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents PanelControl5 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents ColConcept As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
End Class
