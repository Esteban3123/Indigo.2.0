Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPopUpProductTransferOrder
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
        Me.RepositoryItemPopupContainerEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDLyProducts = New DevExpress.XtraLayout.LayoutControl()
        Me.INDBtnAddProduct = New DevExpress.XtraEditors.SimpleButton()
        Me.INDPcePhysicalInventory = New DevExpress.XtraEditors.PopupContainerControl()
        Me.CtrPhysicalInventory1 = New Presentation.Controls.CtrPhysicalInventory()
        Me.INDPupCProducto = New DevExpress.XtraEditors.PopupContainerControl()
        Me.ElementHost1 = New System.Windows.Forms.Integration.ElementHost()
        Me.WpfMessageConversation1 = New Presentation.Base.WPFMessageConversation()
        Me.CtrProducts1 = New Presentation.Controls.CtrProducts()
        Me.INDSleMedicine = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDTxtCostProduct = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtMeasureUnit = New DevExpress.XtraEditors.TextEdit()
        Me.INDspnQuantity = New DevExpress.XtraEditors.SpinEdit()
        Me.INDMeDescription = New DevExpress.XtraEditors.MemoEdit()
        Me.INDPceBatchSerial = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.INDspnStocksProducts = New DevExpress.XtraEditors.SpinEdit()
        Me.INDPceProducts = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.INDSleSupply = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDspnOutstandingQuantity = New DevExpress.XtraEditors.SpinEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcgDataRequired = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyPceProducts = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciStocksProducts = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciBatchSerial = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciDescription = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciQuantity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciMeasureUnit = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciCostProduct = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciMedicine = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciSupply = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciOutstandingQuantity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoPopUpContainerEdit1 = New Presentation.Controls.IndigoPopUpContainerEdit(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyProducts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLyProducts.SuspendLayout()
        CType(Me.INDPcePhysicalInventory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPcePhysicalInventory.SuspendLayout()
        CType(Me.INDPupCProducto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPupCProducto.SuspendLayout()
        CType(Me.INDSleMedicine.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtCostProduct.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtMeasureUnit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDspnQuantity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDMeDescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPceBatchSerial.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDspnStocksProducts.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPceProducts.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleSupply.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDspnOutstandingQuantity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgDataRequired, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyPceProducts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciStocksProducts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciBatchSerial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciMeasureUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciCostProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciMedicine, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciSupply, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciOutstandingQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLyProducts)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1215, 616)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1215, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(1215, 130)
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
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDLyProducts
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 607)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDLyProducts
        '
        Me.INDLyProducts.AllowCustomization = False
        Me.INDLyProducts.Controls.Add(Me.INDBtnAddProduct)
        Me.INDLyProducts.Controls.Add(Me.INDPcePhysicalInventory)
        Me.INDLyProducts.Controls.Add(Me.INDPupCProducto)
        Me.INDLyProducts.Controls.Add(Me.INDSleMedicine)
        Me.INDLyProducts.Controls.Add(Me.INDTxtCostProduct)
        Me.INDLyProducts.Controls.Add(Me.INDTxtMeasureUnit)
        Me.INDLyProducts.Controls.Add(Me.INDspnQuantity)
        Me.INDLyProducts.Controls.Add(Me.INDMeDescription)
        Me.INDLyProducts.Controls.Add(Me.INDPceBatchSerial)
        Me.INDLyProducts.Controls.Add(Me.INDspnStocksProducts)
        Me.INDLyProducts.Controls.Add(Me.INDPceProducts)
        Me.INDLyProducts.Controls.Add(Me.INDSleSupply)
        Me.INDLyProducts.Controls.Add(Me.INDspnOutstandingQuantity)
        Me.INDLyProducts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDLyProducts, False)
        Me.INDLyProducts.Location = New System.Drawing.Point(202, 7)
        Me.INDLyProducts.Name = "INDLyProducts"
        Me.INDLyProducts.Root = Me.LayoutControlGroup1
        Me.INDLyProducts.Size = New System.Drawing.Size(1011, 607)
        Me.INDLyProducts.TabIndex = 1
        Me.INDLyProducts.Text = "LayoutControl1"
        '
        'INDBtnAddProduct
        '
        Me.INDBtnAddProduct.Location = New System.Drawing.Point(24, 552)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDBtnAddProduct, False)
        Me.INDBtnAddProduct.Name = "INDBtnAddProduct"
        Me.INDBtnAddProduct.Size = New System.Drawing.Size(946, 31)
        Me.INDBtnAddProduct.StyleController = Me.INDLyProducts
        Me.INDBtnAddProduct.TabIndex = 8
        Me.INDBtnAddProduct.Text = "Agregar"
        '
        'INDPcePhysicalInventory
        '
        Me.INDPcePhysicalInventory.Controls.Add(Me.CtrPhysicalInventory1)
        Me.INDPcePhysicalInventory.Location = New System.Drawing.Point(381, 88)
        Me.INDPcePhysicalInventory.Name = "INDPcePhysicalInventory"
        Me.INDPcePhysicalInventory.Size = New System.Drawing.Size(560, 352)
        Me.INDPcePhysicalInventory.TabIndex = 8
        '
        'CtrPhysicalInventory1
        '
        Me.CtrPhysicalInventory1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrPhysicalInventory1.Location = New System.Drawing.Point(0, 0)
        Me.CtrPhysicalInventory1.Name = "CtrPhysicalInventory1"
        Me.CtrPhysicalInventory1.Size = New System.Drawing.Size(560, 352)
        Me.CtrPhysicalInventory1.TabIndex = 0
        '
        'INDPupCProducto
        '
        Me.INDPupCProducto.Controls.Add(Me.ElementHost1)
        Me.INDPupCProducto.Controls.Add(Me.CtrProducts1)
        Me.INDPupCProducto.Location = New System.Drawing.Point(661, 51)
        Me.INDPupCProducto.Name = "INDPupCProducto"
        Me.INDPupCProducto.Size = New System.Drawing.Size(622, 369)
        Me.INDPupCProducto.TabIndex = 6
        '
        'ElementHost1
        '
        Me.ElementHost1.Location = New System.Drawing.Point(447, 219)
        Me.ElementHost1.Name = "ElementHost1"
        Me.ElementHost1.Size = New System.Drawing.Size(8, 8)
        Me.ElementHost1.TabIndex = 1
        Me.ElementHost1.Text = "ElementHost1"
        Me.ElementHost1.Child = Me.WpfMessageConversation1
        '
        'CtrProducts1
        '
        Me.CtrProducts1.DataSource = Nothing
        Me.CtrProducts1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrProducts1.Location = New System.Drawing.Point(0, 0)
        Me.CtrProducts1.Name = "CtrProducts1"
        Me.CtrProducts1.Size = New System.Drawing.Size(622, 369)
        Me.CtrProducts1.TabIndex = 0
        '
        'INDSleMedicine
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleMedicine, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleMedicine, True)
        Me.INDSleMedicine.EnterMoveNextControl = True
        Me.INDSleMedicine.Location = New System.Drawing.Point(24, -98)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleMedicine, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleMedicine.Name = "INDSleMedicine"
        Me.INDSleMedicine.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDSleMedicine.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleMedicine.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDSleMedicine.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleMedicine.Properties.Appearance.Options.UseFont = True
        Me.INDSleMedicine.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleMedicine.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleMedicine.Properties.DisplayMember = "Name"
        Me.INDSleMedicine.Properties.NullText = ""
        Me.INDSleMedicine.Properties.PopupSizeable = False
        Me.INDSleMedicine.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDSleMedicine.Properties.ReadOnly = True
        Me.INDSleMedicine.Properties.ValueMember = "Id"
        Me.INDSleMedicine.Size = New System.Drawing.Size(386, 28)
        Me.INDSleMedicine.StyleController = Me.INDLyProducts
        Me.INDSleMedicine.TabIndex = 9
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleMedicine, 0)
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
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsCustomization.AllowGroup = False
        Me.SearchLookUpEdit1View.OptionsDetail.EnableMasterViewMode = False
        Me.SearchLookUpEdit1View.OptionsDetail.ShowDetailTabs = False
        Me.SearchLookUpEdit1View.OptionsFind.AlwaysVisible = True
        Me.SearchLookUpEdit1View.OptionsFind.FindFilterColumns = "Code"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        '
        'INDTxtCostProduct
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtCostProduct, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtCostProduct, False)
        Me.INDTxtCostProduct.EnterMoveNextControl = True
        Me.INDTxtCostProduct.Location = New System.Drawing.Point(24, 216)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtCostProduct, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtCostProduct.Name = "INDTxtCostProduct"
        Me.INDTxtCostProduct.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDTxtCostProduct.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtCostProduct.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtCostProduct.Properties.Appearance.Options.UseFont = True
        Me.INDTxtCostProduct.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTxtCostProduct.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTxtCostProduct.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtCostProduct.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtCostProduct.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtCostProduct.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtCostProduct.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtCostProduct.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtCostProduct.Properties.Mask.EditMask = "c2"
        Me.INDTxtCostProduct.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDTxtCostProduct.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtCostProduct.Properties.ReadOnly = True
        Me.INDTxtCostProduct.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtCostProduct.StyleController = Me.INDLyProducts
        Me.INDTxtCostProduct.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtCostProduct, 0)
        '
        'INDTxtMeasureUnit
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtMeasureUnit, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtMeasureUnit, False)
        Me.INDTxtMeasureUnit.EnterMoveNextControl = True
        Me.INDTxtMeasureUnit.Location = New System.Drawing.Point(24, 152)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtMeasureUnit, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtMeasureUnit.Name = "INDTxtMeasureUnit"
        Me.INDTxtMeasureUnit.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDTxtMeasureUnit.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtMeasureUnit.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtMeasureUnit.Properties.Appearance.Options.UseFont = True
        Me.INDTxtMeasureUnit.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtMeasureUnit.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtMeasureUnit.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtMeasureUnit.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtMeasureUnit.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtMeasureUnit.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtMeasureUnit.Properties.ReadOnly = True
        Me.INDTxtMeasureUnit.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtMeasureUnit.StyleController = Me.INDLyProducts
        Me.INDTxtMeasureUnit.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtMeasureUnit, 0)
        '
        'INDspnQuantity
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDspnQuantity, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDspnQuantity, False)
        Me.INDspnQuantity.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDspnQuantity.EnterMoveNextControl = True
        Me.INDspnQuantity.Location = New System.Drawing.Point(24, 350)
        Me.IndigoTextEdit1.SetMascara(Me.INDspnQuantity, Presentation.Controls.IndigoTextEdit.EMask.Numerico)
        Me.INDspnQuantity.Name = "INDspnQuantity"
        Me.INDspnQuantity.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDspnQuantity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDspnQuantity.Properties.Appearance.Options.UseBackColor = True
        Me.INDspnQuantity.Properties.Appearance.Options.UseFont = True
        Me.INDspnQuantity.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDspnQuantity.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDspnQuantity.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDspnQuantity.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDspnQuantity.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDspnQuantity.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDspnQuantity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDspnQuantity.Properties.Mask.EditMask = "[0-9]+"
        Me.INDspnQuantity.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDspnQuantity.Properties.MaxValue = New Decimal(New Integer() {999999999, 0, 0, 0})
        Me.INDspnQuantity.Size = New System.Drawing.Size(386, 28)
        Me.INDspnQuantity.StyleController = Me.INDLyProducts
        Me.INDspnQuantity.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDspnQuantity, 0)
        '
        'INDMeDescription
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDMeDescription, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDMeDescription, False)
        Me.INDMeDescription.Location = New System.Drawing.Point(24, 477)
        Me.IndigoTextEdit1.SetMascara(Me.INDMeDescription, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDMeDescription.Name = "INDMeDescription"
        Me.INDMeDescription.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDMeDescription.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDMeDescription.Properties.Appearance.Options.UseBackColor = True
        Me.INDMeDescription.Properties.Appearance.Options.UseFont = True
        Me.INDMeDescription.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDMeDescription.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDMeDescription.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDMeDescription.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDMeDescription.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDMeDescription.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDMeDescription.Properties.MaxLength = 300
        Me.INDMeDescription.Size = New System.Drawing.Size(386, 71)
        Me.INDMeDescription.StyleController = Me.INDLyProducts
        Me.INDMeDescription.TabIndex = 7
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDMeDescription, 0)
        '
        'INDPceBatchSerial
        '
        Me.IndigoPopUpContainerEdit1.SetButtonMoreOptions(Me.INDPceBatchSerial, False)
        Me.INDPceBatchSerial.EnterMoveNextControl = True
        Me.IndigoPopUpContainerEdit1.SetHostControl(Me.INDPceBatchSerial, Nothing)
        Me.INDPceBatchSerial.Location = New System.Drawing.Point(24, 414)
        Me.INDPceBatchSerial.MinimumSize = New System.Drawing.Size(386, 32)
        Me.INDPceBatchSerial.Name = "INDPceBatchSerial"
        Me.IndigoPopUpContainerEdit1.SetOpenForm(Me.INDPceBatchSerial, False)
        Me.IndigoPopUpContainerEdit1.SetPopUpAnimation(Me.INDPceBatchSerial, False)
        Me.INDPceBatchSerial.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDPceBatchSerial.Properties.Appearance.Options.UseFont = True
        Me.INDPceBatchSerial.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDPceBatchSerial.Properties.PopupControl = Me.INDPcePhysicalInventory
        Me.INDPceBatchSerial.Properties.PopupSizeable = False
        Me.INDPceBatchSerial.Properties.ShowPopupCloseButton = False
        Me.INDPceBatchSerial.Size = New System.Drawing.Size(386, 32)
        Me.INDPceBatchSerial.StyleController = Me.INDLyProducts
        Me.INDPceBatchSerial.TabIndex = 6
        Me.IndigoPopUpContainerEdit1.SetTagForm(Me.INDPceBatchSerial, Nothing)
        Me.IndigoPopUpContainerEdit1.SetWpfControl(Me.INDPceBatchSerial, Nothing)
        '
        'INDspnStocksProducts
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDspnStocksProducts, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDspnStocksProducts, True)
        Me.INDspnStocksProducts.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDspnStocksProducts.Enabled = False
        Me.INDspnStocksProducts.EnterMoveNextControl = True
        Me.INDspnStocksProducts.Location = New System.Drawing.Point(24, 94)
        Me.IndigoTextEdit1.SetMascara(Me.INDspnStocksProducts, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDspnStocksProducts.Name = "INDspnStocksProducts"
        Me.INDspnStocksProducts.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDspnStocksProducts.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDspnStocksProducts.Properties.Appearance.Options.UseBackColor = True
        Me.INDspnStocksProducts.Properties.Appearance.Options.UseFont = True
        Me.INDspnStocksProducts.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDspnStocksProducts.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDspnStocksProducts.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDspnStocksProducts.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDspnStocksProducts.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDspnStocksProducts.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDspnStocksProducts.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDspnStocksProducts.Size = New System.Drawing.Size(386, 28)
        Me.INDspnStocksProducts.StyleController = Me.INDLyProducts
        Me.INDspnStocksProducts.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDspnStocksProducts, 0)
        '
        'INDPceProducts
        '
        Me.IndigoPopUpContainerEdit1.SetButtonMoreOptions(Me.INDPceProducts, False)
        Me.INDPceProducts.EnterMoveNextControl = True
        Me.IndigoPopUpContainerEdit1.SetHostControl(Me.INDPceProducts, Nothing)
        Me.INDPceProducts.Location = New System.Drawing.Point(24, 30)
        Me.INDPceProducts.MinimumSize = New System.Drawing.Size(386, 32)
        Me.INDPceProducts.Name = "INDPceProducts"
        Me.IndigoPopUpContainerEdit1.SetOpenForm(Me.INDPceProducts, True)
        Me.IndigoPopUpContainerEdit1.SetPopUpAnimation(Me.INDPceProducts, False)
        Me.INDPceProducts.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDPceProducts.Properties.Appearance.Options.UseFont = True
        Me.INDPceProducts.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDPceProducts.Properties.PopupControl = Me.INDPupCProducto
        Me.INDPceProducts.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.INDPceProducts.Size = New System.Drawing.Size(386, 32)
        Me.INDPceProducts.StyleController = Me.INDLyProducts
        Me.INDPceProducts.TabIndex = 1
        Me.IndigoPopUpContainerEdit1.SetTagForm(Me.INDPceProducts, "304")
        Me.IndigoPopUpContainerEdit1.SetWpfControl(Me.INDPceProducts, Nothing)
        '
        'INDSleSupply
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleSupply, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleSupply, False)
        Me.INDSleSupply.EnterMoveNextControl = True
        Me.INDSleSupply.Location = New System.Drawing.Point(24, -34)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleSupply, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleSupply.Name = "INDSleSupply"
        Me.INDSleSupply.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDSleSupply.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleSupply.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDSleSupply.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleSupply.Properties.Appearance.Options.UseFont = True
        Me.INDSleSupply.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleSupply.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleSupply.Properties.DisplayMember = "Name"
        Me.INDSleSupply.Properties.NullText = ""
        Me.INDSleSupply.Properties.PopupSizeable = False
        Me.INDSleSupply.Properties.PopupView = Me.SearchLookUpEdit1View1
        Me.INDSleSupply.Properties.ReadOnly = True
        Me.INDSleSupply.Properties.ValueMember = "Id"
        Me.INDSleSupply.Size = New System.Drawing.Size(386, 28)
        Me.INDSleSupply.StyleController = Me.INDLyProducts
        Me.INDSleSupply.TabIndex = 9
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleSupply, 0)
        Me.INDSleSupply.ToolTip = "Este Campo es Necesario"
        '
        'SearchLookUpEdit1View1
        '
        Me.SearchLookUpEdit1View1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit1View1.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit1View1.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit1View1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View1.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit1View1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View1.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit1View1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit1View1.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit1View1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View1.Name = "SearchLookUpEdit1View1"
        Me.SearchLookUpEdit1View1.OptionsCustomization.AllowGroup = False
        Me.SearchLookUpEdit1View1.OptionsDetail.EnableMasterViewMode = False
        Me.SearchLookUpEdit1View1.OptionsDetail.ShowDetailTabs = False
        Me.SearchLookUpEdit1View1.OptionsFind.AlwaysVisible = True
        Me.SearchLookUpEdit1View1.OptionsFind.FindFilterColumns = "Code"
        Me.SearchLookUpEdit1View1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View1.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View1.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View1.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View1.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View1, False)
        '
        'INDspnOutstandingQuantity
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDspnOutstandingQuantity, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDspnOutstandingQuantity, False)
        Me.INDspnOutstandingQuantity.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDspnOutstandingQuantity.EnterMoveNextControl = True
        Me.INDspnOutstandingQuantity.Location = New System.Drawing.Point(24, 286)
        Me.IndigoTextEdit1.SetMascara(Me.INDspnOutstandingQuantity, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDspnOutstandingQuantity.Name = "INDspnOutstandingQuantity"
        Me.INDspnOutstandingQuantity.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDspnOutstandingQuantity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDspnOutstandingQuantity.Properties.Appearance.Options.UseBackColor = True
        Me.INDspnOutstandingQuantity.Properties.Appearance.Options.UseFont = True
        Me.INDspnOutstandingQuantity.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDspnOutstandingQuantity.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDspnOutstandingQuantity.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDspnOutstandingQuantity.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDspnOutstandingQuantity.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDspnOutstandingQuantity.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDspnOutstandingQuantity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDspnOutstandingQuantity.Properties.Mask.EditMask = "[0-9]+"
        Me.INDspnOutstandingQuantity.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDspnOutstandingQuantity.Properties.MaxValue = New Decimal(New Integer() {999999999, 0, 0, 0})
        Me.INDspnOutstandingQuantity.Properties.ReadOnly = True
        Me.INDspnOutstandingQuantity.Size = New System.Drawing.Size(386, 28)
        Me.INDspnOutstandingQuantity.StyleController = Me.INDLyProducts
        Me.INDspnOutstandingQuantity.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDspnOutstandingQuantity, 0)
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
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgDataRequired})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(994, 784)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDLcgDataRequired
        '
        Me.INDLcgDataRequired.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgDataRequired.AppearanceGroup.Options.UseFont = True
        Me.INDLcgDataRequired.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgDataRequired.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgDataRequired.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgDataRequired.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgDataRequired.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgDataRequired.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgDataRequired.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgDataRequired.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgDataRequired.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgDataRequired.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgDataRequired.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgDataRequired.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgDataRequired, False)
        Me.INDLcgDataRequired.CustomizationFormText = "Datos Principales"
        Me.INDLcgDataRequired.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyPceProducts, Me.INDlciStocksProducts, Me.INDLciBatchSerial, Me.INDLciDescription, Me.INDLciQuantity, Me.INDLciMeasureUnit, Me.INDLciCostProduct, Me.INDLciMedicine, Me.INDLciSupply, Me.INDLciOutstandingQuantity, Me.LayoutControlItem1})
        Me.INDLcgDataRequired.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgDataRequired.Name = "INDLcgDataRequired"
        Me.INDLcgDataRequired.Size = New System.Drawing.Size(974, 764)
        Me.INDLcgDataRequired.Text = "Datos Principales"
        '
        'INDLyPceProducts
        '
        Me.INDLyPceProducts.Control = Me.INDPceProducts
        Me.INDLyPceProducts.CustomizationFormText = "Producto"
        Me.INDLyPceProducts.Location = New System.Drawing.Point(0, 128)
        Me.INDLyPceProducts.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLyPceProducts.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLyPceProducts.Name = "INDLyPceProducts"
        Me.INDLyPceProducts.Size = New System.Drawing.Size(950, 64)
        Me.INDLyPceProducts.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyPceProducts.Text = "Producto"
        Me.INDLyPceProducts.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyPceProducts.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyPceProducts.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLyPceProducts.TextToControlDistance = 5
        '
        'INDlciStocksProducts
        '
        Me.INDlciStocksProducts.Control = Me.INDspnStocksProducts
        Me.INDlciStocksProducts.CustomizationFormText = "Existencias"
        Me.INDlciStocksProducts.Location = New System.Drawing.Point(0, 192)
        Me.INDlciStocksProducts.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciStocksProducts.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciStocksProducts.Name = "INDlciStocksProducts"
        Me.INDlciStocksProducts.Size = New System.Drawing.Size(950, 64)
        Me.INDlciStocksProducts.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciStocksProducts.Text = "Existencias"
        Me.INDlciStocksProducts.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciStocksProducts.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciStocksProducts.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciStocksProducts.TextToControlDistance = 5
        Me.INDlciStocksProducts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLciBatchSerial
        '
        Me.INDLciBatchSerial.Control = Me.INDPceBatchSerial
        Me.INDLciBatchSerial.CustomizationFormText = "Cantidades"
        Me.INDLciBatchSerial.Location = New System.Drawing.Point(0, 512)
        Me.INDLciBatchSerial.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciBatchSerial.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciBatchSerial.Name = "INDLciBatchSerial"
        Me.INDLciBatchSerial.Size = New System.Drawing.Size(950, 64)
        Me.INDLciBatchSerial.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciBatchSerial.Text = "Lote/Serial"
        Me.INDLciBatchSerial.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciBatchSerial.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciBatchSerial.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciBatchSerial.TextToControlDistance = 5
        '
        'INDLciDescription
        '
        Me.INDLciDescription.Control = Me.INDMeDescription
        Me.INDLciDescription.CustomizationFormText = "Descripción"
        Me.INDLciDescription.Location = New System.Drawing.Point(0, 576)
        Me.INDLciDescription.MaxSize = New System.Drawing.Size(390, 0)
        Me.INDLciDescription.MinSize = New System.Drawing.Size(390, 100)
        Me.INDLciDescription.Name = "INDLciDescription"
        Me.INDLciDescription.Size = New System.Drawing.Size(950, 100)
        Me.INDLciDescription.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDescription.Text = "Descripción"
        Me.INDLciDescription.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciDescription.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDescription.TextSize = New System.Drawing.Size(50, 20)
        Me.INDLciDescription.TextToControlDistance = 5
        '
        'INDLciQuantity
        '
        Me.INDLciQuantity.Control = Me.INDspnQuantity
        Me.INDLciQuantity.CustomizationFormText = "Cantidad entregada"
        Me.INDLciQuantity.Location = New System.Drawing.Point(0, 448)
        Me.INDLciQuantity.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciQuantity.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciQuantity.Name = "INDLciQuantity"
        Me.INDLciQuantity.Size = New System.Drawing.Size(950, 64)
        Me.INDLciQuantity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciQuantity.Text = "Cantidad entregada"
        Me.INDLciQuantity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciQuantity.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciQuantity.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciQuantity.TextToControlDistance = 5
        '
        'INDLciMeasureUnit
        '
        Me.INDLciMeasureUnit.Control = Me.INDTxtMeasureUnit
        Me.INDLciMeasureUnit.Location = New System.Drawing.Point(0, 256)
        Me.INDLciMeasureUnit.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciMeasureUnit.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciMeasureUnit.Name = "INDLciMeasureUnit"
        Me.INDLciMeasureUnit.Size = New System.Drawing.Size(950, 64)
        Me.INDLciMeasureUnit.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciMeasureUnit.Text = "Unidad De Medida"
        Me.INDLciMeasureUnit.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciMeasureUnit.TextSize = New System.Drawing.Size(123, 17)
        Me.INDLciMeasureUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLciCostProduct
        '
        Me.INDLciCostProduct.Control = Me.INDTxtCostProduct
        Me.INDLciCostProduct.Location = New System.Drawing.Point(0, 320)
        Me.INDLciCostProduct.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciCostProduct.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciCostProduct.Name = "INDLciCostProduct"
        Me.INDLciCostProduct.Size = New System.Drawing.Size(950, 64)
        Me.INDLciCostProduct.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciCostProduct.Text = "Costo del Producto"
        Me.INDLciCostProduct.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciCostProduct.TextSize = New System.Drawing.Size(123, 17)
        '
        'INDLciMedicine
        '
        Me.INDLciMedicine.AllowHide = False
        Me.INDLciMedicine.Control = Me.INDSleMedicine
        Me.INDLciMedicine.Location = New System.Drawing.Point(0, 0)
        Me.INDLciMedicine.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciMedicine.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciMedicine.Name = "INDLciMedicine"
        Me.INDLciMedicine.ShowInCustomizationForm = False
        Me.INDLciMedicine.Size = New System.Drawing.Size(950, 64)
        Me.INDLciMedicine.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciMedicine.Text = "Medicamento"
        Me.INDLciMedicine.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciMedicine.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciMedicine.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciMedicine.TextToControlDistance = 5
        Me.INDLciMedicine.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLciSupply
        '
        Me.INDLciSupply.Control = Me.INDSleSupply
        Me.INDLciSupply.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciSupply.CustomizationFormText = "Insumo"
        Me.INDLciSupply.Location = New System.Drawing.Point(0, 64)
        Me.INDLciSupply.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciSupply.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciSupply.Name = "INDLciSupply"
        Me.INDLciSupply.ShowInCustomizationForm = False
        Me.INDLciSupply.Size = New System.Drawing.Size(950, 64)
        Me.INDLciSupply.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciSupply.Text = "Insumo"
        Me.INDLciSupply.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciSupply.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciSupply.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciSupply.TextToControlDistance = 5
        Me.INDLciSupply.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLciOutstandingQuantity
        '
        Me.INDLciOutstandingQuantity.Control = Me.INDspnOutstandingQuantity
        Me.INDLciOutstandingQuantity.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciOutstandingQuantity.CustomizationFormText = "Cantidad pendiente entrega"
        Me.INDLciOutstandingQuantity.Location = New System.Drawing.Point(0, 384)
        Me.INDLciOutstandingQuantity.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciOutstandingQuantity.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciOutstandingQuantity.Name = "INDLciOutstandingQuantity"
        Me.INDLciOutstandingQuantity.Size = New System.Drawing.Size(950, 64)
        Me.INDLciOutstandingQuantity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciOutstandingQuantity.Text = "Cantidad pendiente entrega"
        Me.INDLciOutstandingQuantity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciOutstandingQuantity.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciOutstandingQuantity.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciOutstandingQuantity.TextToControlDistance = 5
        Me.INDLciOutstandingQuantity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDBtnAddProduct
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 676)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(0, 35)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(50, 35)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(950, 35)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit2
        '
        'FrmPopUpProductTransferOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1215, 751)
        Me.IconOptions.ShowIcon = False
        Me.KeyPreview = True
        Me.Name = "FrmPopUpProductTransferOrder"
        Me.Opacity = 1.0R
        Me.Text = "Producto"
        Me.ViewModeEditHold = True
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyProducts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLyProducts.ResumeLayout(False)
        CType(Me.INDPcePhysicalInventory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPcePhysicalInventory.ResumeLayout(False)
        CType(Me.INDPupCProducto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPupCProducto.ResumeLayout(False)
        CType(Me.INDSleMedicine.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtCostProduct.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtMeasureUnit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDspnQuantity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDMeDescription.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPceBatchSerial.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDspnStocksProducts.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPceProducts.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleSupply.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDspnOutstandingQuantity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgDataRequired, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyPceProducts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciStocksProducts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciBatchSerial, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciMeasureUnit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciCostProduct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciMedicine, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciSupply, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciOutstandingQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDLyProducts As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents INDspnStocksProducts As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDPceProducts As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents INDLcgDataRequired As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLyPceProducts As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciStocksProducts As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDPupCProducto As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents CtrProducts1 As Presentation.Controls.CtrProducts
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents IndigoPopUpContainerEdit1 As Presentation.Controls.IndigoPopUpContainerEdit
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents INDBtnAddProduct As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDPceBatchSerial As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents INDLciBatchSerial As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDPcePhysicalInventory As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents CtrPhysicalInventory1 As Presentation.Controls.CtrPhysicalInventory
    Friend WithEvents INDMeDescription As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDLciDescription As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDspnQuantity As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDLciQuantity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDTxtMeasureUnit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciMeasureUnit As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDTxtCostProduct As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciCostProduct As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSleMedicine As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ElementHost1 As Windows.Forms.Integration.ElementHost
    Friend WpfMessageConversation1 As Base.WPFMessageConversation
    Friend WithEvents INDLciMedicine As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSleSupply As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciSupply As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDspnOutstandingQuantity As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDLciOutstandingQuantity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
End Class
