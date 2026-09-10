Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmPopupProductDecreaseMaximumLimit
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
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.RepositoryItemPopupContainerEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDSbAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.INDLcRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDPupCProducto = New DevExpress.XtraEditors.PopupContainerControl()
        Me.CtrProducts1 = New Presentation.Controls.CtrProducts()
        Me.ElementHost1 = New System.Windows.Forms.Integration.ElementHost()
        Me.WpfMessageConversation1 = New Presentation.Base.WPFMessageConversation()
        Me.INDSpnDecreaseQuantity = New DevExpress.XtraEditors.SpinEdit()
        Me.INDPceProducts = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.INDMeDescription = New DevExpress.XtraEditors.MemoEdit()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciJustification = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciQuantity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciProduct = New DevExpress.XtraLayout.LayoutControlItem()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoPopUpContainerEdit1 = New Presentation.Controls.IndigoPopUpContainerEdit(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoTextEdit11 = New Presentation.Controls.IndigoTextEdit(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcRoot.SuspendLayout()
        CType(Me.INDPupCProducto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPupCProducto.SuspendLayout()
        CType(Me.INDSpnDecreaseQuantity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPceProducts.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDMeDescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciJustification, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit11, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLcRoot)
        Me.INDPanelControlBase.Controls.Add(Me.INDSbAdd)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(562, 397)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(562, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(562, 130)
        '
        'RepositoryItemPopupContainerEdit2
        '
        Me.RepositoryItemPopupContainerEdit2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit2.Name = "RepositoryItemPopupContainerEdit2"
        '
        'INDSbAdd
        '
        Me.INDSbAdd.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDSbAdd.Appearance.Options.UseFont = True
        Me.INDSbAdd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.INDSbAdd.Location = New System.Drawing.Point(2, 358)
        Me.INDSbAdd.Name = "INDSbAdd"
        Me.INDSbAdd.Size = New System.Drawing.Size(558, 37)
        Me.INDSbAdd.TabIndex = 0
        Me.INDSbAdd.Text = "Aceptar"
        '
        'INDLcRoot
        '
        Me.INDLcRoot.Controls.Add(Me.INDPupCProducto)
        Me.INDLcRoot.Controls.Add(Me.INDSpnDecreaseQuantity)
        Me.INDLcRoot.Controls.Add(Me.INDPceProducts)
        Me.INDLcRoot.Controls.Add(Me.INDMeDescription)
        Me.INDLcRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcRoot.Location = New System.Drawing.Point(2, 7)
        Me.INDLcRoot.Name = "INDLcRoot"
        Me.INDLcRoot.Root = Me.Root
        Me.INDLcRoot.Size = New System.Drawing.Size(558, 351)
        Me.INDLcRoot.TabIndex = 2
        Me.INDLcRoot.Text = "LayoutControl1"
        '
        'INDPupCProducto
        '
        Me.INDPupCProducto.Controls.Add(Me.CtrProducts1)
        Me.INDPupCProducto.Controls.Add(Me.ElementHost1)
        Me.INDPupCProducto.Location = New System.Drawing.Point(517, 397)
        Me.INDPupCProducto.Name = "INDPupCProducto"
        Me.INDPupCProducto.Size = New System.Drawing.Size(746, 431)
        Me.INDPupCProducto.TabIndex = 7
        '
        'CtrProducts1
        '
        Me.CtrProducts1.DataSource = Nothing
        Me.CtrProducts1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrProducts1.Location = New System.Drawing.Point(0, 0)
        Me.CtrProducts1.Name = "CtrProducts1"
        Me.CtrProducts1.Size = New System.Drawing.Size(746, 431)
        Me.CtrProducts1.TabIndex = 0
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
        'INDSpnDecreaseQuantity
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSpnDecreaseQuantity, True)
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDSpnDecreaseQuantity, False)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDSpnDecreaseQuantity, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSpnDecreaseQuantity, True)
        Me.INDSpnDecreaseQuantity.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDSpnDecreaseQuantity.EnterMoveNextControl = True
        Me.INDSpnDecreaseQuantity.Location = New System.Drawing.Point(12, 98)
        Me.IndigoTextEdit1.SetMascara(Me.INDSpnDecreaseQuantity, Presentation.Controls.IndigoTextEdit.EMask.Numerico)
        Me.IndigoTextEdit11.SetMascara(Me.INDSpnDecreaseQuantity, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSpnDecreaseQuantity.Name = "INDSpnDecreaseQuantity"
        Me.INDSpnDecreaseQuantity.Properties.AllowMouseWheel = False
        Me.INDSpnDecreaseQuantity.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDSpnDecreaseQuantity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSpnDecreaseQuantity.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDSpnDecreaseQuantity.Properties.Appearance.Options.UseBackColor = True
        Me.INDSpnDecreaseQuantity.Properties.Appearance.Options.UseFont = True
        Me.INDSpnDecreaseQuantity.Properties.Appearance.Options.UseForeColor = True
        Me.INDSpnDecreaseQuantity.Properties.AppearanceFocused.BackColor = System.Drawing.Color.White
        Me.INDSpnDecreaseQuantity.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSpnDecreaseQuantity.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Black
        Me.INDSpnDecreaseQuantity.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSpnDecreaseQuantity.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSpnDecreaseQuantity.Properties.AppearanceFocused.Options.UseForeColor = True
        EditorButtonImageOptions1.Image = Global.Presentation.Inventory.My.Resources.Resources.MoreInfo_16x16_blue
        Me.INDSpnDecreaseQuantity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, False, False, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDSpnDecreaseQuantity.Properties.Mask.EditMask = "[0-9]+"
        Me.INDSpnDecreaseQuantity.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDSpnDecreaseQuantity.Size = New System.Drawing.Size(386, 28)
        Me.INDSpnDecreaseQuantity.StyleController = Me.INDLcRoot
        Me.INDSpnDecreaseQuantity.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSpnDecreaseQuantity, 0)
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDSpnDecreaseQuantity, 0)
        '
        'INDPceProducts
        '
        Me.IndigoPopUpContainerEdit1.SetButtonMoreOptions(Me.INDPceProducts, False)
        Me.INDPceProducts.Enabled = False
        Me.INDPceProducts.EnterMoveNextControl = True
        Me.IndigoPopUpContainerEdit1.SetHostControl(Me.INDPceProducts, Nothing)
        Me.INDPceProducts.Location = New System.Drawing.Point(12, 34)
        Me.INDPceProducts.MinimumSize = New System.Drawing.Size(386, 32)
        Me.INDPceProducts.Name = "INDPceProducts"
        Me.IndigoPopUpContainerEdit1.SetOpenForm(Me.INDPceProducts, False)
        Me.IndigoPopUpContainerEdit1.SetPopUpAnimation(Me.INDPceProducts, False)
        Me.INDPceProducts.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDPceProducts.Properties.Appearance.Options.UseFont = True
        Me.INDPceProducts.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDPceProducts.Properties.PopupControl = Me.INDPupCProducto
        Me.INDPceProducts.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.INDPceProducts.Size = New System.Drawing.Size(386, 32)
        Me.INDPceProducts.StyleController = Me.INDLcRoot
        Me.INDPceProducts.TabIndex = 1
        Me.IndigoPopUpContainerEdit1.SetTagForm(Me.INDPceProducts, Nothing)
        Me.IndigoPopUpContainerEdit1.SetWpfControl(Me.INDPceProducts, Nothing)
        '
        'INDMeDescription
        '
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDMeDescription, True)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDMeDescription, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDMeDescription, True)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDMeDescription, False)
        Me.INDMeDescription.EnterMoveNextControl = True
        Me.INDMeDescription.Location = New System.Drawing.Point(12, 162)
        Me.IndigoTextEdit11.SetMascara(Me.INDMeDescription, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit1.SetMascara(Me.INDMeDescription, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDMeDescription.Name = "INDMeDescription"
        Me.INDMeDescription.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDMeDescription.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDMeDescription.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDMeDescription.Properties.Appearance.Options.UseBackColor = True
        Me.INDMeDescription.Properties.Appearance.Options.UseFont = True
        Me.INDMeDescription.Properties.Appearance.Options.UseForeColor = True
        Me.INDMeDescription.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDMeDescription.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDMeDescription.Properties.MaxLength = 500
        Me.INDMeDescription.Size = New System.Drawing.Size(386, 154)
        Me.INDMeDescription.StyleController = Me.INDLcRoot
        Me.INDMeDescription.TabIndex = 5
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDMeDescription, 0)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDMeDescription, 0)
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
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciJustification, Me.INDLciQuantity, Me.INDLciProduct})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(558, 351)
        Me.Root.TextVisible = False
        '
        'INDLciJustification
        '
        Me.INDLciJustification.AllowHide = False
        Me.INDLciJustification.Control = Me.INDMeDescription
        Me.INDLciJustification.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciJustification.CustomizationFormText = "Descripción"
        Me.INDLciJustification.Location = New System.Drawing.Point(0, 128)
        Me.INDLciJustification.MaxSize = New System.Drawing.Size(390, 180)
        Me.INDLciJustification.MinSize = New System.Drawing.Size(390, 180)
        Me.INDLciJustification.Name = "INDLciJustification"
        Me.INDLciJustification.ShowInCustomizationForm = False
        Me.INDLciJustification.Size = New System.Drawing.Size(538, 203)
        Me.INDLciJustification.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciJustification.Text = "Justificación"
        Me.INDLciJustification.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciJustification.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciJustification.TextSize = New System.Drawing.Size(126, 17)
        Me.INDLciJustification.TextToControlDistance = 5
        '
        'INDLciQuantity
        '
        Me.INDLciQuantity.AllowHide = False
        Me.INDLciQuantity.Control = Me.INDSpnDecreaseQuantity
        Me.INDLciQuantity.Location = New System.Drawing.Point(0, 64)
        Me.INDLciQuantity.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciQuantity.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciQuantity.Name = "INDLciQuantity"
        Me.INDLciQuantity.ShowInCustomizationForm = False
        Me.INDLciQuantity.Size = New System.Drawing.Size(538, 64)
        Me.INDLciQuantity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciQuantity.Text = "Cantidad a disminuir"
        Me.INDLciQuantity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciQuantity.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciQuantity.TextSize = New System.Drawing.Size(96, 17)
        Me.INDLciQuantity.TextToControlDistance = 5
        '
        'INDLciProduct
        '
        Me.INDLciProduct.Control = Me.INDPceProducts
        Me.INDLciProduct.Location = New System.Drawing.Point(0, 0)
        Me.INDLciProduct.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciProduct.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciProduct.Name = "INDLciProduct"
        Me.INDLciProduct.Size = New System.Drawing.Size(538, 64)
        Me.INDLciProduct.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciProduct.Text = "Producto"
        Me.INDLciProduct.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciProduct.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciProduct.TextSize = New System.Drawing.Size(96, 17)
        Me.INDLciProduct.TextToControlDistance = 5
        '
        'BarManager1
        '
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 5)
        Me.barDockControlTop.Manager = Me.BarManager1
        Me.barDockControlTop.Size = New System.Drawing.Size(562, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 532)
        Me.barDockControlBottom.Manager = Me.BarManager1
        Me.barDockControlBottom.Size = New System.Drawing.Size(562, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 5)
        Me.barDockControlLeft.Manager = Me.BarManager1
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 527)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(562, 5)
        Me.barDockControlRight.Manager = Me.BarManager1
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 527)
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit2
        '
        'FrmPopupProductDecreaseMaximumLimit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(562, 532)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.IconOptions.ShowIcon = False
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmPopupProductDecreaseMaximumLimit"
        Me.Opacity = 1.0R
        Me.Text = "Disminución límite máximo"
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
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcRoot.ResumeLayout(False)
        CType(Me.INDPupCProducto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPupCProducto.ResumeLayout(False)
        CType(Me.INDSpnDecreaseQuantity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPceProducts.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDMeDescription.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciJustification, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciProduct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit11, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents INDSbAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDLcRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDPupCProducto As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents ElementHost1 As Windows.Forms.Integration.ElementHost
    Friend WpfMessageConversation1 As Base.WPFMessageConversation
    Friend WithEvents CtrProducts1 As CtrProducts
    Friend WithEvents INDPceProducts As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents INDLciProduct As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoPopUpContainerEdit1 As IndigoPopUpContainerEdit
    Friend WithEvents IndigoSearchLookUpControl1 As IndigoSearchLookUpControl
    Friend WithEvents IndigoTextEdit1 As IndigoTextEdit
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents IndigoGroupControl1 As IndigoGroupControl
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents INDSpnDecreaseQuantity As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDLciQuantity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents RepositoryItemPopupContainerEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Private WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents IndigoTextEdit11 As IndigoTextEdit
    Friend WithEvents INDMeDescription As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDLciJustification As DevExpress.XtraLayout.LayoutControlItem
End Class
