Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PopUpProductsRequest
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
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDLyProducts = New DevExpress.XtraLayout.LayoutControl()
        Me.INDmeDescription = New DevExpress.XtraEditors.MemoEdit()
        Me.INDspnQuantity = New DevExpress.XtraEditors.SpinEdit()
        Me.INDPupCProducto = New DevExpress.XtraEditors.PopupContainerControl()
        Me.CtrProducts1 = New Presentation.Controls.CtrProducts()
        Me.INDPceProducts = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlcgData = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyPceProducts = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciQuantity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciDescription = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.INDBtnAddProduct = New DevExpress.XtraEditors.SimpleButton()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.IndigoPopUpContainerEdit1 = New Presentation.Controls.IndigoPopUpContainerEdit(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyProducts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLyProducts.SuspendLayout()
        CType(Me.INDmeDescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDspnQuantity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPupCProducto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPupCProducto.SuspendLayout()
        CType(Me.INDPceProducts.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyPceProducts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLyProducts)
        Me.INDPanelControlBase.Controls.Add(Me.PanelControl1)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 118)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1008, 611)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1008, 94)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1008, 94)
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDLyProducts
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 602)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDLyProducts
        '
        Me.INDLyProducts.AllowCustomization = False
        Me.INDLyProducts.Controls.Add(Me.INDmeDescription)
        Me.INDLyProducts.Controls.Add(Me.INDspnQuantity)
        Me.INDLyProducts.Controls.Add(Me.INDPupCProducto)
        Me.INDLyProducts.Controls.Add(Me.INDPceProducts)
        Me.INDLyProducts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDLyProducts, False)
        Me.INDLyProducts.Location = New System.Drawing.Point(202, 7)
        Me.INDLyProducts.Name = "INDLyProducts"
        Me.INDLyProducts.Root = Me.LayoutControlGroup1
        Me.INDLyProducts.Size = New System.Drawing.Size(804, 566)
        Me.INDLyProducts.TabIndex = 1
        Me.INDLyProducts.Text = "LayoutControl1"
        '
        'INDmeDescription
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDmeDescription, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDmeDescription, False)
        Me.INDmeDescription.EnterMoveNextControl = True
        Me.INDmeDescription.Location = New System.Drawing.Point(24, 213)
        Me.IndigoTextEdit1.SetMascara(Me.INDmeDescription, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDmeDescription.Name = "INDmeDescription"
        Me.INDmeDescription.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDmeDescription.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmeDescription.Properties.Appearance.Options.UseBackColor = True
        Me.INDmeDescription.Properties.Appearance.Options.UseFont = True
        Me.INDmeDescription.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDmeDescription.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDmeDescription.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmeDescription.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDmeDescription.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDmeDescription.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDmeDescription.Properties.MaxLength = 300
        Me.INDmeDescription.Size = New System.Drawing.Size(386, 50)
        Me.INDmeDescription.StyleController = Me.INDLyProducts
        Me.INDmeDescription.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDmeDescription, 0)
        '
        'INDspnQuantity
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDspnQuantity, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDspnQuantity, False)
        Me.INDspnQuantity.EditValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.INDspnQuantity.EnterMoveNextControl = True
        Me.INDspnQuantity.Location = New System.Drawing.Point(24, 149)
        Me.IndigoTextEdit1.SetMascara(Me.INDspnQuantity, Presentation.Controls.IndigoTextEdit.EMask.Numerico)
        Me.INDspnQuantity.Name = "INDspnQuantity"
        Me.INDspnQuantity.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
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
        Me.INDspnQuantity.Properties.MaxValue = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.INDspnQuantity.Properties.MinValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.INDspnQuantity.Size = New System.Drawing.Size(386, 28)
        Me.INDspnQuantity.StyleController = Me.INDLyProducts
        Me.INDspnQuantity.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDspnQuantity, 0)
        '
        'INDPupCProducto
        '
        Me.INDPupCProducto.Controls.Add(Me.CtrProducts1)
        Me.INDPupCProducto.Location = New System.Drawing.Point(195, 292)
        Me.INDPupCProducto.Name = "INDPupCProducto"
        Me.INDPupCProducto.Size = New System.Drawing.Size(663, 376)
        Me.INDPupCProducto.TabIndex = 4
        '
        'CtrProducts1
        '
        Me.CtrProducts1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrProducts1.Location = New System.Drawing.Point(0, 0)
        Me.CtrProducts1.Name = "CtrProducts1"
        Me.CtrProducts1.Size = New System.Drawing.Size(663, 376)
        Me.CtrProducts1.TabIndex = 0
        '
        'INDPceProducts
        '
        Me.IndigoPopUpContainerEdit1.SetButtonMoreOptions(Me.INDPceProducts, False)
        Me.INDPceProducts.EnterMoveNextControl = True
        Me.IndigoPopUpContainerEdit1.SetHostControl(Me.INDPceProducts, Nothing)
        Me.INDPceProducts.Location = New System.Drawing.Point(24, 85)
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
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.LayoutControlGroup1.CustomizationFormText = "Productos"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlcgData})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(804, 566)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlcgData
        '
        Me.INDlcgData.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgData.AppearanceGroup.Options.UseFont = True
        Me.INDlcgData.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgData.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgData.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgData.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgData.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgData.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgData.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgData.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgData.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgData.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgData.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgData.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgData, False)
        Me.INDlcgData.CustomizationFormText = "Datos Principales"
        Me.INDlcgData.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyPceProducts, Me.INDlciQuantity, Me.INDlciDescription})
        Me.INDlcgData.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgData.Name = "INDlcgData"
        Me.INDlcgData.Size = New System.Drawing.Size(784, 546)
        Me.INDlcgData.Text = "Datos Principales"
        '
        'INDLyPceProducts
        '
        Me.INDLyPceProducts.Control = Me.INDPceProducts
        Me.INDLyPceProducts.CustomizationFormText = "Producto"
        Me.INDLyPceProducts.Location = New System.Drawing.Point(0, 0)
        Me.INDLyPceProducts.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLyPceProducts.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLyPceProducts.Name = "INDLyPceProducts"
        Me.INDLyPceProducts.Size = New System.Drawing.Size(760, 64)
        Me.INDLyPceProducts.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyPceProducts.Text = "Producto"
        Me.INDLyPceProducts.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyPceProducts.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyPceProducts.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLyPceProducts.TextToControlDistance = 5
        '
        'INDlciQuantity
        '
        Me.INDlciQuantity.Control = Me.INDspnQuantity
        Me.INDlciQuantity.CustomizationFormText = "Cantidad"
        Me.INDlciQuantity.Location = New System.Drawing.Point(0, 64)
        Me.INDlciQuantity.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciQuantity.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciQuantity.Name = "INDlciQuantity"
        Me.INDlciQuantity.Size = New System.Drawing.Size(760, 64)
        Me.INDlciQuantity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciQuantity.Text = "Cantidad"
        Me.INDlciQuantity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciQuantity.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciQuantity.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciQuantity.TextToControlDistance = 5
        '
        'INDlciDescription
        '
        Me.INDlciDescription.Control = Me.INDmeDescription
        Me.INDlciDescription.CustomizationFormText = "Descripción"
        Me.INDlciDescription.Location = New System.Drawing.Point(0, 128)
        Me.INDlciDescription.MaxSize = New System.Drawing.Size(390, 80)
        Me.INDlciDescription.MinSize = New System.Drawing.Size(390, 80)
        Me.INDlciDescription.Name = "INDlciDescription"
        Me.INDlciDescription.Size = New System.Drawing.Size(760, 359)
        Me.INDlciDescription.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciDescription.Text = "Descripción"
        Me.INDlciDescription.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciDescription.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciDescription.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciDescription.TextToControlDistance = 5
        '
        'INDBtnAddProduct
        '
        Me.INDBtnAddProduct.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDBtnAddProduct.Location = New System.Drawing.Point(2, 2)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDBtnAddProduct, False)
        Me.INDBtnAddProduct.Name = "INDBtnAddProduct"
        Me.INDBtnAddProduct.Size = New System.Drawing.Size(800, 32)
        Me.INDBtnAddProduct.TabIndex = 0
        Me.INDBtnAddProduct.Text = "Agregar"
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.INDBtnAddProduct)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(202, 573)
        Me.PanelControl1.MaximumSize = New System.Drawing.Size(0, 36)
        Me.PanelControl1.MinimumSize = New System.Drawing.Size(0, 36)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(804, 36)
        Me.PanelControl1.TabIndex = 2
        '
        'PopUpProductsRequest
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 729)
        Me.KeyPreview = True
        Me.Name = "PopUpProductsRequest"
        Me.Opacity = 1.0R
        Me.Text = "Productos"
        Me.ViewModeEditHold = True
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyProducts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLyProducts.ResumeLayout(False)
        CType(Me.INDmeDescription.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDspnQuantity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPupCProducto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPupCProducto.ResumeLayout(False)
        CType(Me.INDPceProducts.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyPceProducts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDLyProducts As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents INDPceProducts As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents INDlcgData As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLyPceProducts As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDPupCProducto As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents CtrProducts1 As Presentation.Controls.CtrProducts
    Friend WithEvents INDmeDescription As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDspnQuantity As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDlciQuantity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciDescription As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDBtnAddProduct As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents IndigoPopUpContainerEdit1 As Presentation.Controls.IndigoPopUpContainerEdit
End Class
