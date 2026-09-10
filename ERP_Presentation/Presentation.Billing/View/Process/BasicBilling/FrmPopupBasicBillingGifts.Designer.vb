Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmPopupBasicBillingGifts
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
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDLciBasicBillingDetail = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSleBatchSerial = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.INDPcePhysicalInventory = New DevExpress.XtraEditors.PopupContainerControl()
        Me.CtrPhysicalInventory1 = New Presentation.Controls.CtrPhysicalInventory()
        Me.INDSeQuantity = New DevExpress.XtraEditors.SpinEdit()
        Me.INDPccProduct = New DevExpress.XtraEditors.PopupContainerControl()
        Me.CtrProductsGifts = New Presentation.Controls.CtrProducts()
        Me.INDSleProduct = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.INDSleWarehouse = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvWarehouse = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColWarehouseCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColWarehouseName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciProduct = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciQuantity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciBatchSerial = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciWarehouse = New DevExpress.XtraLayout.LayoutControlItem()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.INDBtnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoPopUpContainerEdit1 = New Presentation.Controls.IndigoPopUpContainerEdit(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.RepositoryItemPopupContainerEdit11 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciBasicBillingDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLciBasicBillingDetail.SuspendLayout()
        CType(Me.INDSleBatchSerial.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPcePhysicalInventory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPcePhysicalInventory.SuspendLayout()
        CType(Me.INDSeQuantity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPccProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPccProduct.SuspendLayout()
        CType(Me.INDSleProduct.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleWarehouse.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvWarehouse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciBatchSerial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciWarehouse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit11, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLciBasicBillingDetail)
        Me.INDPanelControlBase.Controls.Add(Me.PanelControl1)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1198, 633)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1198, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(1198, 130)
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
        Me.CtrNavigationControl1.LayoutControl = Me.INDLciBasicBillingDetail
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 624)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDLciBasicBillingDetail
        '
        Me.INDLciBasicBillingDetail.Controls.Add(Me.INDSleBatchSerial)
        Me.INDLciBasicBillingDetail.Controls.Add(Me.INDSeQuantity)
        Me.INDLciBasicBillingDetail.Controls.Add(Me.INDPccProduct)
        Me.INDLciBasicBillingDetail.Controls.Add(Me.INDSleProduct)
        Me.INDLciBasicBillingDetail.Controls.Add(Me.INDSleWarehouse)
        Me.INDLciBasicBillingDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLciBasicBillingDetail.Location = New System.Drawing.Point(202, 7)
        Me.INDLciBasicBillingDetail.Name = "INDLciBasicBillingDetail"
        Me.INDLciBasicBillingDetail.Root = Me.LayoutControlGroup1
        Me.INDLciBasicBillingDetail.Size = New System.Drawing.Size(994, 588)
        Me.INDLciBasicBillingDetail.TabIndex = 2
        Me.INDLciBasicBillingDetail.Text = "LayoutControl1"
        '
        'INDSleBatchSerial
        '
        Me.IndigoPopUpContainerEdit1.SetButtonMoreOptions(Me.INDSleBatchSerial, False)
        Me.INDSleBatchSerial.EnterMoveNextControl = True
        Me.IndigoPopUpContainerEdit1.SetHostControl(Me.INDSleBatchSerial, Nothing)
        Me.INDSleBatchSerial.Location = New System.Drawing.Point(24, 197)
        Me.INDSleBatchSerial.MinimumSize = New System.Drawing.Size(386, 32)
        Me.INDSleBatchSerial.Name = "INDSleBatchSerial"
        Me.IndigoPopUpContainerEdit1.SetOpenForm(Me.INDSleBatchSerial, False)
        Me.IndigoPopUpContainerEdit1.SetPopUpAnimation(Me.INDSleBatchSerial, False)
        Me.INDSleBatchSerial.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDSleBatchSerial.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleBatchSerial.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleBatchSerial.Properties.Appearance.Options.UseFont = True
        Me.INDSleBatchSerial.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleBatchSerial.Properties.PopupControl = Me.INDPcePhysicalInventory
        Me.INDSleBatchSerial.Size = New System.Drawing.Size(386, 28)
        Me.INDSleBatchSerial.StyleController = Me.INDLciBasicBillingDetail
        Me.INDSleBatchSerial.TabIndex = 2
        Me.IndigoPopUpContainerEdit1.SetTagForm(Me.INDSleBatchSerial, Nothing)
        Me.IndigoPopUpContainerEdit1.SetWpfControl(Me.INDSleBatchSerial, Nothing)
        '
        'INDPcePhysicalInventory
        '
        Me.INDPcePhysicalInventory.Controls.Add(Me.CtrPhysicalInventory1)
        Me.INDPcePhysicalInventory.Location = New System.Drawing.Point(37, 109)
        Me.INDPcePhysicalInventory.Name = "INDPcePhysicalInventory"
        Me.INDPcePhysicalInventory.Size = New System.Drawing.Size(560, 352)
        Me.INDPcePhysicalInventory.TabIndex = 13
        '
        'CtrPhysicalInventory1
        '
        Me.CtrPhysicalInventory1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrPhysicalInventory1.Location = New System.Drawing.Point(0, 0)
        Me.CtrPhysicalInventory1.Name = "CtrPhysicalInventory1"
        Me.CtrPhysicalInventory1.Size = New System.Drawing.Size(560, 352)
        Me.CtrPhysicalInventory1.TabIndex = 0
        '
        'INDSeQuantity
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSeQuantity, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSeQuantity, True)
        Me.INDSeQuantity.EditValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.INDSeQuantity.EnterMoveNextControl = True
        Me.INDSeQuantity.Location = New System.Drawing.Point(24, 257)
        Me.INDSeQuantity.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDSeQuantity, Presentation.Controls.IndigoTextEdit.EMask.Numerico)
        Me.INDSeQuantity.Name = "INDSeQuantity"
        Me.INDSeQuantity.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSeQuantity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeQuantity.Properties.Appearance.Options.UseBackColor = True
        Me.INDSeQuantity.Properties.Appearance.Options.UseFont = True
        Me.INDSeQuantity.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSeQuantity.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSeQuantity.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeQuantity.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSeQuantity.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSeQuantity.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSeQuantity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSeQuantity.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.INDSeQuantity.Properties.Mask.EditMask = "[0-9]+"
        Me.INDSeQuantity.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDSeQuantity.Properties.MaxValue = New Decimal(New Integer() {2147483646, 0, 0, 0})
        Me.INDSeQuantity.Properties.MinValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.INDSeQuantity.Size = New System.Drawing.Size(386, 28)
        Me.INDSeQuantity.StyleController = Me.INDLciBasicBillingDetail
        Me.INDSeQuantity.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSeQuantity, 0)
        '
        'INDPccProduct
        '
        Me.INDPccProduct.Controls.Add(Me.CtrProductsGifts)
        Me.INDPccProduct.Controls.Add(Me.INDPcePhysicalInventory)
        Me.INDPccProduct.Location = New System.Drawing.Point(846, 69)
        Me.INDPccProduct.Name = "INDPccProduct"
        Me.INDPccProduct.Size = New System.Drawing.Size(657, 451)
        Me.INDPccProduct.TabIndex = 10
        '
        'CtrProductsGifts
        '
        Me.CtrProductsGifts.DataSource = Nothing
        Me.CtrProductsGifts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrProductsGifts.Location = New System.Drawing.Point(0, 0)
        Me.CtrProductsGifts.Name = "CtrProductsGifts"
        Me.CtrProductsGifts.Size = New System.Drawing.Size(657, 451)
        Me.CtrProductsGifts.TabIndex = 0
        '
        'INDSleProduct
        '
        Me.IndigoPopUpContainerEdit1.SetButtonMoreOptions(Me.INDSleProduct, False)
        Me.INDSleProduct.EnterMoveNextControl = True
        Me.IndigoPopUpContainerEdit1.SetHostControl(Me.INDSleProduct, Nothing)
        Me.INDSleProduct.Location = New System.Drawing.Point(24, 79)
        Me.INDSleProduct.MaximumSize = New System.Drawing.Size(386, 28)
        Me.INDSleProduct.MinimumSize = New System.Drawing.Size(386, 32)
        Me.INDSleProduct.Name = "INDSleProduct"
        Me.IndigoPopUpContainerEdit1.SetOpenForm(Me.INDSleProduct, True)
        Me.IndigoPopUpContainerEdit1.SetPopUpAnimation(Me.INDSleProduct, False)
        Me.INDSleProduct.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleProduct.Properties.Appearance.Options.UseFont = True
        Me.INDSleProduct.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleProduct.Properties.PopupControl = Me.INDPccProduct
        Me.INDSleProduct.Properties.PopupSizeable = False
        Me.INDSleProduct.Properties.ShowPopupCloseButton = False
        Me.INDSleProduct.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.INDSleProduct.Size = New System.Drawing.Size(386, 28)
        Me.INDSleProduct.StyleController = Me.INDLciBasicBillingDetail
        Me.INDSleProduct.TabIndex = 1
        Me.IndigoPopUpContainerEdit1.SetTagForm(Me.INDSleProduct, "304")
        Me.IndigoPopUpContainerEdit1.SetWpfControl(Me.INDSleProduct, Nothing)
        '
        'INDSleWarehouse
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleWarehouse, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleWarehouse, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleWarehouse, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleWarehouse, False)
        Me.INDSleWarehouse.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleWarehouse, False)
        Me.INDSleWarehouse.Location = New System.Drawing.Point(24, 136)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleWarehouse, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleWarehouse.Name = "INDSleWarehouse"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleWarehouse, False)
        Me.INDSleWarehouse.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
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
        Me.INDSleWarehouse.Properties.PopupView = Me.INDGvWarehouse
        Me.INDSleWarehouse.Properties.ShowClearButton = False
        Me.INDSleWarehouse.Properties.ShowFooter = False
        Me.INDSleWarehouse.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleWarehouse, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleWarehouse, True)
        Me.INDSleWarehouse.Size = New System.Drawing.Size(386, 28)
        Me.INDSleWarehouse.StyleController = Me.INDLciBasicBillingDetail
        Me.INDSleWarehouse.TabIndex = 8
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleWarehouse, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleWarehouse, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleWarehouse, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleWarehouse, False)
        '
        'INDGvWarehouse
        '
        Me.INDGvWarehouse.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvWarehouse.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvWarehouse.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvWarehouse.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvWarehouse.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGvWarehouse.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvWarehouse.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvWarehouse.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvWarehouse.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvWarehouse.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvWarehouse.Appearance.Row.Options.UseFont = True
        Me.INDGvWarehouse.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColWarehouseCode, Me.INDColWarehouseName})
        Me.INDGvWarehouse.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvWarehouse.Name = "INDGvWarehouse"
        Me.INDGvWarehouse.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvWarehouse.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvWarehouse.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvWarehouse.OptionsView.ShowAutoFilterRow = True
        Me.INDGvWarehouse.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvWarehouse, False)
        '
        'INDColWarehouseCode
        '
        Me.INDColWarehouseCode.Caption = "Código"
        Me.INDColWarehouseCode.FieldName = "Code"
        Me.INDColWarehouseCode.Name = "INDColWarehouseCode"
        Me.INDColWarehouseCode.Visible = True
        Me.INDColWarehouseCode.VisibleIndex = 0
        Me.INDColWarehouseCode.Width = 219
        '
        'INDColWarehouseName
        '
        Me.INDColWarehouseName.Caption = "Nombre"
        Me.INDColWarehouseName.FieldName = "Name"
        Me.INDColWarehouseName.Name = "INDColWarehouseName"
        Me.INDColWarehouseName.Visible = True
        Me.INDColWarehouseName.VisibleIndex = 1
        Me.INDColWarehouseName.Width = 477
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup2})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(994, 588)
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
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciProduct, Me.INDLciQuantity, Me.INDLciBatchSerial, Me.INDLciWarehouse})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(974, 568)
        Me.LayoutControlGroup2.Text = "Datos Principales"
        '
        'INDLciProduct
        '
        Me.INDLciProduct.Control = Me.INDSleProduct
        Me.INDLciProduct.Location = New System.Drawing.Point(0, 0)
        Me.INDLciProduct.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciProduct.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciProduct.Name = "INDLciProduct"
        Me.INDLciProduct.Size = New System.Drawing.Size(950, 60)
        Me.INDLciProduct.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciProduct.Text = "Producto"
        Me.INDLciProduct.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciProduct.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciProduct.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciProduct.TextToControlDistance = 5
        '
        'INDLciQuantity
        '
        Me.INDLciQuantity.Control = Me.INDSeQuantity
        Me.INDLciQuantity.Location = New System.Drawing.Point(0, 178)
        Me.INDLciQuantity.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciQuantity.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciQuantity.Name = "INDLciQuantity"
        Me.INDLciQuantity.Size = New System.Drawing.Size(950, 337)
        Me.INDLciQuantity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciQuantity.Text = "Cantidad"
        Me.INDLciQuantity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciQuantity.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciQuantity.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciQuantity.TextToControlDistance = 5
        '
        'INDLciBatchSerial
        '
        Me.INDLciBatchSerial.Control = Me.INDSleBatchSerial
        Me.INDLciBatchSerial.Location = New System.Drawing.Point(0, 118)
        Me.INDLciBatchSerial.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciBatchSerial.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciBatchSerial.Name = "INDLciBatchSerial"
        Me.INDLciBatchSerial.Size = New System.Drawing.Size(950, 60)
        Me.INDLciBatchSerial.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciBatchSerial.Text = "Lote / Serial"
        Me.INDLciBatchSerial.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciBatchSerial.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciBatchSerial.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciBatchSerial.TextToControlDistance = 5
        '
        'INDLciWarehouse
        '
        Me.INDLciWarehouse.Control = Me.INDSleWarehouse
        Me.INDLciWarehouse.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciWarehouse.CustomizationFormText = "Almacen"
        Me.INDLciWarehouse.Location = New System.Drawing.Point(0, 60)
        Me.INDLciWarehouse.MaxSize = New System.Drawing.Size(390, 58)
        Me.INDLciWarehouse.MinSize = New System.Drawing.Size(390, 58)
        Me.INDLciWarehouse.Name = "INDLciWarehouse"
        Me.INDLciWarehouse.Size = New System.Drawing.Size(950, 58)
        Me.INDLciWarehouse.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciWarehouse.Text = "Almacen"
        Me.INDLciWarehouse.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciWarehouse.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciWarehouse.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciWarehouse.TextToControlDistance = 2
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.INDBtnAdd)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(202, 595)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(994, 36)
        Me.PanelControl1.TabIndex = 1
        '
        'INDBtnAdd
        '
        Me.INDBtnAdd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDBtnAdd.Location = New System.Drawing.Point(2, 2)
        Me.INDBtnAdd.Name = "INDBtnAdd"
        Me.INDBtnAdd.Size = New System.Drawing.Size(990, 32)
        Me.INDBtnAdd.TabIndex = 0
        Me.INDBtnAdd.Text = "Agregar"
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'RepositoryItemPopupContainerEdit11
        '
        Me.RepositoryItemPopupContainerEdit11.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit11.Name = "RepositoryItemPopupContainerEdit11"
        '
        'FrmPopupBasicBillingGifts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1198, 768)
        Me.IconOptions.ShowIcon = False
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmPopupBasicBillingGifts"
        Me.Opacity = 1.0R
        Me.Text = "Obsequios"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciBasicBillingDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLciBasicBillingDetail.ResumeLayout(False)
        CType(Me.INDSleBatchSerial.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPcePhysicalInventory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPcePhysicalInventory.ResumeLayout(False)
        CType(Me.INDSeQuantity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPccProduct, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPccProduct.ResumeLayout(False)
        CType(Me.INDSleProduct.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleWarehouse.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvWarehouse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciProduct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciBatchSerial, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciWarehouse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit11, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDLciBasicBillingDetail As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDBtnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoPopUpContainerEdit1 As Presentation.Controls.IndigoPopUpContainerEdit
    Friend WithEvents INDSleProduct As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciProduct As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDPccProduct As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents CtrProductsGifts As Presentation.Controls.CtrProducts
    Friend WithEvents INDSeQuantity As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDLciQuantity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSleBatchSerial As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents INDLciBatchSerial As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDPcePhysicalInventory As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents CtrPhysicalInventory1 As Presentation.Controls.CtrPhysicalInventory
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents IndigoSearchLookUpControl1 As IndigoSearchLookUpControl
    Friend WithEvents RepositoryItemPopupContainerEdit11 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDSleWarehouse As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvWarehouse As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColWarehouseCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColWarehouseName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLciWarehouse As DevExpress.XtraLayout.LayoutControlItem
End Class
