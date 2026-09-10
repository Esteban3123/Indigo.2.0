Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PopupAddProductsInventoryControl
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
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDLyAddProducts = New DevExpress.XtraLayout.LayoutControl()
        Me.INDPupCBatchSerial = New DevExpress.XtraEditors.PopupContainerControl()
        Me.INDPupCProducto = New DevExpress.XtraEditors.PopupContainerControl()
        Me.CtrProducts1 = New Presentation.Controls.CtrProducts()
        Me.CtrBatchSerial1 = New Presentation.Inventory.CtrBatchSerial()
        Me.INDTxtPhysicalInventoryQuantity = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtQuantity = New DevExpress.XtraEditors.TextEdit()
        Me.INDPceBatchSerial = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.INDTxtUnid = New DevExpress.XtraEditors.TextEdit()
        Me.INDPceProduct = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LyGroupProduct = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyPceProduct = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyTxtUnid = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyTxtQuantity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyPceBatchSerial = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyTxtPhysicalInventoryQuantity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl()
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.INDBtnAddProduct = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyAddProducts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLyAddProducts.SuspendLayout()
        CType(Me.INDPupCBatchSerial, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPupCBatchSerial.SuspendLayout()
        CType(Me.INDPupCProducto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPupCProducto.SuspendLayout()
        CType(Me.INDTxtPhysicalInventoryQuantity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtQuantity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPceBatchSerial.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtUnid.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPceProduct.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LyGroupProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyPceProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyTxtUnid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyTxtQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyPceBatchSerial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyTxtPhysicalInventoryQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLyAddProducts)
        Me.INDPanelControlBase.Controls.Add(Me.PanelControl1)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 118)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(958, 441)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(958, 94)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(958, 94)
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDLyAddProducts
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 432)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDLyAddProducts
        '
        Me.INDLyAddProducts.Controls.Add(Me.INDPupCBatchSerial)
        Me.INDLyAddProducts.Controls.Add(Me.INDTxtPhysicalInventoryQuantity)
        Me.INDLyAddProducts.Controls.Add(Me.INDTxtQuantity)
        Me.INDLyAddProducts.Controls.Add(Me.INDPceBatchSerial)
        Me.INDLyAddProducts.Controls.Add(Me.INDTxtUnid)
        Me.INDLyAddProducts.Controls.Add(Me.INDPceProduct)
        Me.INDLyAddProducts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLyAddProducts.Location = New System.Drawing.Point(202, 7)
        Me.INDLyAddProducts.Name = "INDLyAddProducts"
        Me.INDLyAddProducts.Root = Me.LayoutControlGroup1
        Me.INDLyAddProducts.Size = New System.Drawing.Size(754, 396)
        Me.INDLyAddProducts.TabIndex = 1
        Me.INDLyAddProducts.Text = "LayoutControl1"
        '
        'INDPupCBatchSerial
        '
        Me.INDPupCBatchSerial.Controls.Add(Me.INDPupCProducto)
        Me.INDPupCBatchSerial.Controls.Add(Me.CtrBatchSerial1)
        Me.INDPupCBatchSerial.Location = New System.Drawing.Point(518, 31)
        Me.INDPupCBatchSerial.Name = "INDPupCBatchSerial"
        Me.INDPupCBatchSerial.Size = New System.Drawing.Size(565, 313)
        Me.INDPupCBatchSerial.TabIndex = 15
        '
        'INDPupCProducto
        '
        Me.INDPupCProducto.Controls.Add(Me.CtrProducts1)
        Me.INDPupCProducto.Location = New System.Drawing.Point(183, 76)
        Me.INDPupCProducto.Name = "INDPupCProducto"
        Me.INDPupCProducto.Size = New System.Drawing.Size(663, 376)
        Me.INDPupCProducto.TabIndex = 13
        '
        'CtrProducts1
        '
        Me.CtrProducts1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrProducts1.Location = New System.Drawing.Point(0, 0)
        Me.CtrProducts1.Name = "CtrProducts1"
        Me.CtrProducts1.Size = New System.Drawing.Size(663, 376)
        Me.CtrProducts1.TabIndex = 0
        '
        'CtrBatchSerial1
        '
        Me.CtrBatchSerial1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrBatchSerial1.Location = New System.Drawing.Point(0, 0)
        Me.CtrBatchSerial1.Name = "CtrBatchSerial1"
        Me.CtrBatchSerial1.Size = New System.Drawing.Size(565, 313)
        Me.CtrBatchSerial1.TabIndex = 0
        '
        'INDTxtPhysicalInventoryQuantity
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtPhysicalInventoryQuantity, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtPhysicalInventoryQuantity, False)
        Me.INDTxtPhysicalInventoryQuantity.EnterMoveNextControl = True
        Me.INDTxtPhysicalInventoryQuantity.Location = New System.Drawing.Point(24, 207)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtPhysicalInventoryQuantity, Presentation.Controls.IndigoTextEdit.EMask.Numerico)
        Me.INDTxtPhysicalInventoryQuantity.Name = "INDTxtPhysicalInventoryQuantity"
        Me.INDTxtPhysicalInventoryQuantity.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDTxtPhysicalInventoryQuantity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtPhysicalInventoryQuantity.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtPhysicalInventoryQuantity.Properties.Appearance.Options.UseFont = True
        Me.INDTxtPhysicalInventoryQuantity.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtPhysicalInventoryQuantity.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtPhysicalInventoryQuantity.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtPhysicalInventoryQuantity.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtPhysicalInventoryQuantity.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtPhysicalInventoryQuantity.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtPhysicalInventoryQuantity.Properties.Mask.EditMask = "[0-9]+"
        Me.INDTxtPhysicalInventoryQuantity.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDTxtPhysicalInventoryQuantity.Properties.ReadOnly = True
        Me.INDTxtPhysicalInventoryQuantity.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtPhysicalInventoryQuantity.StyleController = Me.INDLyAddProducts
        Me.INDTxtPhysicalInventoryQuantity.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtPhysicalInventoryQuantity, 0)
        '
        'INDTxtQuantity
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtQuantity, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtQuantity, True)
        Me.INDTxtQuantity.EnterMoveNextControl = True
        Me.INDTxtQuantity.Location = New System.Drawing.Point(24, 269)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtQuantity, Presentation.Controls.IndigoTextEdit.EMask.Numerico)
        Me.INDTxtQuantity.Name = "INDTxtQuantity"
        Me.INDTxtQuantity.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDTxtQuantity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtQuantity.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtQuantity.Properties.Appearance.Options.UseFont = True
        Me.INDTxtQuantity.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtQuantity.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtQuantity.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtQuantity.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtQuantity.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtQuantity.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtQuantity.Properties.Mask.EditMask = "[0-9]+"
        Me.INDTxtQuantity.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDTxtQuantity.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtQuantity.StyleController = Me.INDLyAddProducts
        Me.INDTxtQuantity.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtQuantity, 0)
        Me.INDTxtQuantity.ToolTip = "Este Campo es Necesario"
        '
        'INDPceBatchSerial
        '
        Me.INDPceBatchSerial.Location = New System.Drawing.Point(24, 331)
        Me.INDPceBatchSerial.MinimumSize = New System.Drawing.Size(386, 32)
        Me.INDPceBatchSerial.Name = "INDPceBatchSerial"
        Me.INDPceBatchSerial.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDPceBatchSerial.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDPceBatchSerial.Properties.Appearance.Options.UseBackColor = True
        Me.INDPceBatchSerial.Properties.Appearance.Options.UseFont = True
        Me.INDPceBatchSerial.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDPceBatchSerial.Properties.PopupControl = Me.INDPupCBatchSerial
        Me.INDPceBatchSerial.Size = New System.Drawing.Size(386, 32)
        Me.INDPceBatchSerial.StyleController = Me.INDLyAddProducts
        Me.INDPceBatchSerial.TabIndex = 4
        '
        'INDTxtUnid
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtUnid, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtUnid, False)
        Me.INDTxtUnid.EnterMoveNextControl = True
        Me.INDTxtUnid.Location = New System.Drawing.Point(24, 145)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtUnid, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtUnid.Name = "INDTxtUnid"
        Me.INDTxtUnid.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDTxtUnid.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtUnid.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtUnid.Properties.Appearance.Options.UseFont = True
        Me.INDTxtUnid.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtUnid.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtUnid.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtUnid.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtUnid.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtUnid.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtUnid.Properties.ReadOnly = True
        Me.INDTxtUnid.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtUnid.StyleController = Me.INDLyAddProducts
        Me.INDTxtUnid.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtUnid, 0)
        '
        'INDPceProduct
        '
        Me.INDPceProduct.Location = New System.Drawing.Point(24, 83)
        Me.INDPceProduct.Name = "INDPceProduct"
        Me.INDPceProduct.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDPceProduct.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDPceProduct.Properties.Appearance.Options.UseBackColor = True
        Me.INDPceProduct.Properties.Appearance.Options.UseFont = True
        Me.INDPceProduct.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDPceProduct.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDPceProduct.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDPceProduct.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDPceProduct.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDPceProduct.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDPceProduct.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDPceProduct.Properties.PopupControl = Me.INDPupCProducto
        Me.INDPceProduct.Size = New System.Drawing.Size(386, 28)
        Me.INDPceProduct.StyleController = Me.INDLyAddProducts
        Me.INDPceProduct.TabIndex = 0
        Me.INDPceProduct.ToolTip = "Este Campo es Necesario"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, False)
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LyGroupProduct})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(754, 396)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LyGroupProduct
        '
        Me.LyGroupProduct.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LyGroupProduct.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LyGroupProduct.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LyGroupProduct.AppearanceItemCaption.Options.UseFont = True
        Me.LyGroupProduct.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LyGroupProduct.AppearanceTabPage.Header.Options.UseFont = True
        Me.LyGroupProduct.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LyGroupProduct.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LyGroupProduct.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LyGroupProduct.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LyGroupProduct.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LyGroupProduct.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LyGroupProduct.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LyGroupProduct.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LyGroupProduct, False)
        Me.LyGroupProduct.CustomizationFormText = "LayoutControlGroup2"
        Me.LyGroupProduct.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyPceProduct, Me.INDLyTxtUnid, Me.INDLyTxtQuantity, Me.INDLyPceBatchSerial, Me.INDLyTxtPhysicalInventoryQuantity})
        Me.LyGroupProduct.Location = New System.Drawing.Point(0, 0)
        Me.LyGroupProduct.Name = "LyGroupProduct"
        Me.LyGroupProduct.Size = New System.Drawing.Size(734, 376)
        Me.LyGroupProduct.Text = "Producto"
        '
        'INDLyPceProduct
        '
        Me.INDLyPceProduct.Control = Me.INDPceProduct
        Me.INDLyPceProduct.CustomizationFormText = "LayoutControlItem1"
        Me.INDLyPceProduct.Location = New System.Drawing.Point(0, 0)
        Me.INDLyPceProduct.MaxSize = New System.Drawing.Size(390, 62)
        Me.INDLyPceProduct.MinSize = New System.Drawing.Size(390, 62)
        Me.INDLyPceProduct.Name = "INDLyPceProduct"
        Me.INDLyPceProduct.ShowInCustomizationForm = False
        Me.INDLyPceProduct.Size = New System.Drawing.Size(710, 62)
        Me.INDLyPceProduct.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyPceProduct.Text = "Producto"
        Me.INDLyPceProduct.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyPceProduct.TextSize = New System.Drawing.Size(192, 21)
        '
        'INDLyTxtUnid
        '
        Me.INDLyTxtUnid.Control = Me.INDTxtUnid
        Me.INDLyTxtUnid.CustomizationFormText = "LayoutControlItem2"
        Me.INDLyTxtUnid.Location = New System.Drawing.Point(0, 62)
        Me.INDLyTxtUnid.MaxSize = New System.Drawing.Size(390, 62)
        Me.INDLyTxtUnid.MinSize = New System.Drawing.Size(390, 62)
        Me.INDLyTxtUnid.Name = "INDLyTxtUnid"
        Me.INDLyTxtUnid.Size = New System.Drawing.Size(710, 62)
        Me.INDLyTxtUnid.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyTxtUnid.Text = "Unidad"
        Me.INDLyTxtUnid.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyTxtUnid.TextSize = New System.Drawing.Size(192, 21)
        '
        'INDLyTxtQuantity
        '
        Me.INDLyTxtQuantity.Control = Me.INDTxtQuantity
        Me.INDLyTxtQuantity.CustomizationFormText = "LayoutControlItem4"
        Me.INDLyTxtQuantity.Location = New System.Drawing.Point(0, 186)
        Me.INDLyTxtQuantity.MaxSize = New System.Drawing.Size(390, 62)
        Me.INDLyTxtQuantity.MinSize = New System.Drawing.Size(390, 62)
        Me.INDLyTxtQuantity.Name = "INDLyTxtQuantity"
        Me.INDLyTxtQuantity.ShowInCustomizationForm = False
        Me.INDLyTxtQuantity.Size = New System.Drawing.Size(710, 62)
        Me.INDLyTxtQuantity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyTxtQuantity.Text = "Cantidad"
        Me.INDLyTxtQuantity.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyTxtQuantity.TextSize = New System.Drawing.Size(192, 21)
        '
        'INDLyPceBatchSerial
        '
        Me.INDLyPceBatchSerial.Control = Me.INDPceBatchSerial
        Me.INDLyPceBatchSerial.CustomizationFormText = "LayoutControlItem3"
        Me.INDLyPceBatchSerial.Location = New System.Drawing.Point(0, 248)
        Me.INDLyPceBatchSerial.MaxSize = New System.Drawing.Size(390, 62)
        Me.INDLyPceBatchSerial.MinSize = New System.Drawing.Size(390, 62)
        Me.INDLyPceBatchSerial.Name = "INDLyPceBatchSerial"
        Me.INDLyPceBatchSerial.Size = New System.Drawing.Size(710, 69)
        Me.INDLyPceBatchSerial.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyPceBatchSerial.Text = "Lote/Serial"
        Me.INDLyPceBatchSerial.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyPceBatchSerial.TextSize = New System.Drawing.Size(192, 21)
        '
        'INDLyTxtPhysicalInventoryQuantity
        '
        Me.INDLyTxtPhysicalInventoryQuantity.Control = Me.INDTxtPhysicalInventoryQuantity
        Me.INDLyTxtPhysicalInventoryQuantity.CustomizationFormText = "LayoutControlItem5"
        Me.INDLyTxtPhysicalInventoryQuantity.Location = New System.Drawing.Point(0, 124)
        Me.INDLyTxtPhysicalInventoryQuantity.MaxSize = New System.Drawing.Size(390, 62)
        Me.INDLyTxtPhysicalInventoryQuantity.MinSize = New System.Drawing.Size(390, 62)
        Me.INDLyTxtPhysicalInventoryQuantity.Name = "INDLyTxtPhysicalInventoryQuantity"
        Me.INDLyTxtPhysicalInventoryQuantity.Size = New System.Drawing.Size(710, 62)
        Me.INDLyTxtPhysicalInventoryQuantity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyTxtPhysicalInventoryQuantity.Text = "Cantidad En Inventario Fisico"
        Me.INDLyTxtPhysicalInventoryQuantity.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyTxtPhysicalInventoryQuantity.TextSize = New System.Drawing.Size(192, 21)
        Me.INDLyTxtPhysicalInventoryQuantity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.INDBtnAddProduct)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(202, 403)
        Me.PanelControl1.MaximumSize = New System.Drawing.Size(0, 36)
        Me.PanelControl1.MinimumSize = New System.Drawing.Size(0, 36)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(754, 36)
        Me.PanelControl1.TabIndex = 2
        '
        'INDBtnAddProduct
        '
        Me.INDBtnAddProduct.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDBtnAddProduct.Location = New System.Drawing.Point(2, 2)
        Me.INDBtnAddProduct.Name = "INDBtnAddProduct"
        Me.INDBtnAddProduct.Size = New System.Drawing.Size(750, 32)
        Me.INDBtnAddProduct.TabIndex = 0
        Me.INDBtnAddProduct.Text = "Agregar"
        '
        'PopupAddProductsInventoryControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(958, 559)
        Me.KeyPreview = True
        Me.Name = "PopupAddProductsInventoryControl"
        Me.Opacity = 1.0R
        Me.Text = "Agregar Productos"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyAddProducts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLyAddProducts.ResumeLayout(False)
        CType(Me.INDPupCBatchSerial, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPupCBatchSerial.ResumeLayout(False)
        CType(Me.INDPupCProducto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPupCProducto.ResumeLayout(False)
        CType(Me.INDTxtPhysicalInventoryQuantity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtQuantity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPceBatchSerial.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtUnid.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPceProduct.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LyGroupProduct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyPceProduct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyTxtUnid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyTxtQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyPceBatchSerial, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyTxtPhysicalInventoryQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDLyAddProducts As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents INDLyPceProduct As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDTxtUnid As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLyTxtUnid As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDTxtPhysicalInventoryQuantity As DevExpress.XtraEditors.TextEdit
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDTxtQuantity As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDPceBatchSerial As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents LyGroupProduct As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLyTxtQuantity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyTxtPhysicalInventoryQuantity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyPceBatchSerial As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDBtnAddProduct As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDPceProduct As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents INDPupCBatchSerial As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents CtrBatchSerial1 As Presentation.Inventory.CtrBatchSerial
    Friend WithEvents INDPupCProducto As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents CtrProducts1 As Presentation.Controls.CtrProducts
End Class
