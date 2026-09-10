Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PopupProductsContract
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
        Me.INDLyProductsContract = New DevExpress.XtraLayout.LayoutControl()
        Me.INDPupCProducto = New DevExpress.XtraEditors.PopupContainerControl()
        Me.CtrProducts1 = New Presentation.Controls.CtrProducts()
        Me.INDTxtSubTotal = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtLastPurcharse = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtUnitPurcharse = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtProductName = New DevExpress.XtraEditors.TextEdit()
        Me.INDPceProducts = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.INDTxtQuantity = New DevExpress.XtraEditors.SpinEdit()
        Me.INDTxtIvaPercent = New DevExpress.XtraEditors.SpinEdit()
        Me.INDTxtDiscountPercent = New DevExpress.XtraEditors.SpinEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcgProduct = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyPceProducts = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyTxtProductName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyTxtUnitPurcharse = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyTxtLastPurcharse = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgCostValue = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyTxtSubTotal = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyTxtIvaPercent = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyTxtQuantity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyTxtDiscountPercent = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.INDBtnAddProduct = New DevExpress.XtraEditors.SimpleButton()
        Me.IndigoPopUpContainerEdit1 = New Presentation.Controls.IndigoPopUpContainerEdit(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyProductsContract, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLyProductsContract.SuspendLayout()
        CType(Me.INDPupCProducto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPupCProducto.SuspendLayout()
        CType(Me.INDTxtSubTotal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtLastPurcharse.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtUnitPurcharse.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtProductName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPceProducts.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtQuantity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtIvaPercent.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtDiscountPercent.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyPceProducts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyTxtProductName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyTxtUnitPurcharse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyTxtLastPurcharse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgCostValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyTxtSubTotal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyTxtIvaPercent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyTxtQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyTxtDiscountPercent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLyProductsContract)
        Me.INDPanelControlBase.Controls.Add(Me.PanelControl1)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 134)
        Me.INDPanelControlBase.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDPanelControlBase.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1122, 392)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Location = New System.Drawing.Point(0, 4)
        Me.ToolBars.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ToolBars.Size = New System.Drawing.Size(1122, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.BarraBotones.Size = New System.Drawing.Size(1122, 130)
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDLyProductsContract
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 6)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 384)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDLyProductsContract
        '
        Me.INDLyProductsContract.AllowCustomization = False
        Me.INDLyProductsContract.Controls.Add(Me.INDPupCProducto)
        Me.INDLyProductsContract.Controls.Add(Me.INDTxtSubTotal)
        Me.INDLyProductsContract.Controls.Add(Me.INDTxtLastPurcharse)
        Me.INDLyProductsContract.Controls.Add(Me.INDTxtUnitPurcharse)
        Me.INDLyProductsContract.Controls.Add(Me.INDTxtProductName)
        Me.INDLyProductsContract.Controls.Add(Me.INDPceProducts)
        Me.INDLyProductsContract.Controls.Add(Me.INDTxtQuantity)
        Me.INDLyProductsContract.Controls.Add(Me.INDTxtIvaPercent)
        Me.INDLyProductsContract.Controls.Add(Me.INDTxtDiscountPercent)
        Me.INDLyProductsContract.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDLyProductsContract, False)
        Me.INDLyProductsContract.Location = New System.Drawing.Point(202, 6)
        Me.INDLyProductsContract.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDLyProductsContract.Name = "INDLyProductsContract"
        Me.INDLyProductsContract.Root = Me.LayoutControlGroup1
        Me.INDLyProductsContract.Size = New System.Drawing.Size(918, 348)
        Me.INDLyProductsContract.TabIndex = 0
        Me.INDLyProductsContract.Text = "LayoutControl1"
        '
        'INDPupCProducto
        '
        Me.INDPupCProducto.Controls.Add(Me.CtrProducts1)
        Me.INDPupCProducto.Location = New System.Drawing.Point(635, 314)
        Me.INDPupCProducto.Name = "INDPupCProducto"
        Me.INDPupCProducto.Size = New System.Drawing.Size(663, 376)
        Me.INDPupCProducto.TabIndex = 12
        '
        'CtrProducts1
        '
        Me.CtrProducts1.DataSource = Nothing
        Me.CtrProducts1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrProducts1.Location = New System.Drawing.Point(0, 0)
        Me.CtrProducts1.Name = "CtrProducts1"
        Me.CtrProducts1.Size = New System.Drawing.Size(663, 376)
        Me.CtrProducts1.TabIndex = 0
        '
        'INDTxtSubTotal
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtSubTotal, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtSubTotal, True)
        Me.INDTxtSubTotal.EnterMoveNextControl = True
        Me.INDTxtSubTotal.Location = New System.Drawing.Point(438, 139)
        Me.INDTxtSubTotal.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtSubTotal, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtSubTotal.Name = "INDTxtSubTotal"
        Me.INDTxtSubTotal.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDTxtSubTotal.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtSubTotal.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtSubTotal.Properties.Appearance.Options.UseFont = True
        Me.INDTxtSubTotal.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTxtSubTotal.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTxtSubTotal.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtSubTotal.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtSubTotal.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtSubTotal.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtSubTotal.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtSubTotal.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtSubTotal.Properties.Mask.EditMask = "c2"
        Me.INDTxtSubTotal.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDTxtSubTotal.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtSubTotal.Properties.MaxLength = 20
        Me.INDTxtSubTotal.Properties.ReadOnly = True
        Me.INDTxtSubTotal.Properties.UseReadOnlyAppearance = False
        Me.INDTxtSubTotal.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtSubTotal.StyleController = Me.INDLyProductsContract
        Me.INDTxtSubTotal.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtSubTotal, 0)
        '
        'INDTxtLastPurcharse
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtLastPurcharse, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtLastPurcharse, True)
        Me.INDTxtLastPurcharse.EnterMoveNextControl = True
        Me.INDTxtLastPurcharse.Location = New System.Drawing.Point(24, 259)
        Me.INDTxtLastPurcharse.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtLastPurcharse, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtLastPurcharse.Name = "INDTxtLastPurcharse"
        Me.INDTxtLastPurcharse.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDTxtLastPurcharse.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtLastPurcharse.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDTxtLastPurcharse.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtLastPurcharse.Properties.Appearance.Options.UseFont = True
        Me.INDTxtLastPurcharse.Properties.Appearance.Options.UseForeColor = True
        Me.INDTxtLastPurcharse.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTxtLastPurcharse.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTxtLastPurcharse.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtLastPurcharse.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtLastPurcharse.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtLastPurcharse.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtLastPurcharse.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtLastPurcharse.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtLastPurcharse.Properties.Mask.EditMask = "c2"
        Me.INDTxtLastPurcharse.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDTxtLastPurcharse.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtLastPurcharse.Properties.MaxLength = 20
        Me.INDTxtLastPurcharse.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtLastPurcharse.StyleController = Me.INDLyProductsContract
        Me.INDTxtLastPurcharse.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtLastPurcharse, 0)
        Me.INDTxtLastPurcharse.ToolTip = "Este Campo es Necesario"
        '
        'INDTxtUnitPurcharse
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtUnitPurcharse, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtUnitPurcharse, False)
        Me.INDTxtUnitPurcharse.EnterMoveNextControl = True
        Me.INDTxtUnitPurcharse.Location = New System.Drawing.Point(24, 199)
        Me.INDTxtUnitPurcharse.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtUnitPurcharse, Presentation.Controls.IndigoTextEdit.EMask.Numerico)
        Me.INDTxtUnitPurcharse.Name = "INDTxtUnitPurcharse"
        Me.INDTxtUnitPurcharse.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtUnitPurcharse.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtUnitPurcharse.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtUnitPurcharse.Properties.Appearance.Options.UseFont = True
        Me.INDTxtUnitPurcharse.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtUnitPurcharse.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtUnitPurcharse.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtUnitPurcharse.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtUnitPurcharse.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtUnitPurcharse.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtUnitPurcharse.Properties.Mask.EditMask = "[0-9]+"
        Me.INDTxtUnitPurcharse.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDTxtUnitPurcharse.Properties.ReadOnly = True
        Me.INDTxtUnitPurcharse.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtUnitPurcharse.StyleController = Me.INDLyProductsContract
        Me.INDTxtUnitPurcharse.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtUnitPurcharse, 0)
        '
        'INDTxtProductName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtProductName, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtProductName, False)
        Me.INDTxtProductName.EnterMoveNextControl = True
        Me.INDTxtProductName.Location = New System.Drawing.Point(24, 139)
        Me.INDTxtProductName.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtProductName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtProductName.Name = "INDTxtProductName"
        Me.INDTxtProductName.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtProductName.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtProductName.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtProductName.Properties.Appearance.Options.UseFont = True
        Me.INDTxtProductName.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtProductName.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtProductName.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtProductName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtProductName.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtProductName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtProductName.Properties.ReadOnly = True
        Me.INDTxtProductName.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtProductName.StyleController = Me.INDLyProductsContract
        Me.INDTxtProductName.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtProductName, 0)
        '
        'INDPceProducts
        '
        Me.IndigoPopUpContainerEdit1.SetButtonMoreOptions(Me.INDPceProducts, False)
        Me.INDPceProducts.EnterMoveNextControl = True
        Me.IndigoPopUpContainerEdit1.SetHostControl(Me.INDPceProducts, Nothing)
        Me.INDPceProducts.Location = New System.Drawing.Point(24, 79)
        Me.INDPceProducts.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDPceProducts.MinimumSize = New System.Drawing.Size(386, 32)
        Me.INDPceProducts.Name = "INDPceProducts"
        Me.IndigoPopUpContainerEdit1.SetOpenForm(Me.INDPceProducts, True)
        Me.IndigoPopUpContainerEdit1.SetPopUpAnimation(Me.INDPceProducts, False)
        Me.INDPceProducts.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDPceProducts.Properties.Appearance.Options.UseFont = True
        Me.INDPceProducts.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDPceProducts.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDPceProducts.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDPceProducts.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDPceProducts.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDPceProducts.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDPceProducts.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDPceProducts.Properties.PopupControl = Me.INDPupCProducto
        Me.INDPceProducts.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.INDPceProducts.Size = New System.Drawing.Size(386, 32)
        Me.INDPceProducts.StyleController = Me.INDLyProductsContract
        Me.INDPceProducts.TabIndex = 0
        Me.IndigoPopUpContainerEdit1.SetTagForm(Me.INDPceProducts, "304")
        Me.INDPceProducts.ToolTip = "Este Campo es Necesario"
        Me.IndigoPopUpContainerEdit1.SetWpfControl(Me.INDPceProducts, Nothing)
        '
        'INDTxtQuantity
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtQuantity, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtQuantity, True)
        Me.INDTxtQuantity.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDTxtQuantity.EnterMoveNextControl = True
        Me.INDTxtQuantity.Location = New System.Drawing.Point(438, 79)
        Me.INDTxtQuantity.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtQuantity, Presentation.Controls.IndigoTextEdit.EMask.Numerico)
        Me.INDTxtQuantity.Name = "INDTxtQuantity"
        Me.INDTxtQuantity.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtQuantity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtQuantity.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtQuantity.Properties.Appearance.Options.UseFont = True
        Me.INDTxtQuantity.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtQuantity.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtQuantity.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtQuantity.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtQuantity.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtQuantity.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtQuantity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDTxtQuantity.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.INDTxtQuantity.Properties.Mask.EditMask = "[0-9]+"
        Me.INDTxtQuantity.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDTxtQuantity.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtQuantity.StyleController = Me.INDLyProductsContract
        Me.INDTxtQuantity.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtQuantity, 0)
        '
        'INDTxtIvaPercent
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtIvaPercent, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtIvaPercent, True)
        Me.INDTxtIvaPercent.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDTxtIvaPercent.EnterMoveNextControl = True
        Me.INDTxtIvaPercent.Location = New System.Drawing.Point(438, 259)
        Me.INDTxtIvaPercent.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtIvaPercent, Presentation.Controls.IndigoTextEdit.EMask.Porcentaje)
        Me.INDTxtIvaPercent.Name = "INDTxtIvaPercent"
        Me.INDTxtIvaPercent.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtIvaPercent.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtIvaPercent.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtIvaPercent.Properties.Appearance.Options.UseFont = True
        Me.INDTxtIvaPercent.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtIvaPercent.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtIvaPercent.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtIvaPercent.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtIvaPercent.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtIvaPercent.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtIvaPercent.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDTxtIvaPercent.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.INDTxtIvaPercent.Properties.Mask.EditMask = "P"
        Me.INDTxtIvaPercent.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtIvaPercent.Properties.MaxLength = 6
        Me.INDTxtIvaPercent.Properties.ReadOnly = True
        Me.INDTxtIvaPercent.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtIvaPercent.StyleController = Me.INDLyProductsContract
        Me.INDTxtIvaPercent.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtIvaPercent, 0)
        '
        'INDTxtDiscountPercent
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtDiscountPercent, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtDiscountPercent, True)
        Me.INDTxtDiscountPercent.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDTxtDiscountPercent.EnterMoveNextControl = True
        Me.INDTxtDiscountPercent.Location = New System.Drawing.Point(438, 199)
        Me.INDTxtDiscountPercent.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtDiscountPercent, Presentation.Controls.IndigoTextEdit.EMask.Porcentaje)
        Me.INDTxtDiscountPercent.Name = "INDTxtDiscountPercent"
        Me.INDTxtDiscountPercent.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtDiscountPercent.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtDiscountPercent.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtDiscountPercent.Properties.Appearance.Options.UseFont = True
        Me.INDTxtDiscountPercent.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtDiscountPercent.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtDiscountPercent.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtDiscountPercent.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtDiscountPercent.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtDiscountPercent.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtDiscountPercent.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDTxtDiscountPercent.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.INDTxtDiscountPercent.Properties.Mask.EditMask = "P"
        Me.INDTxtDiscountPercent.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtDiscountPercent.Properties.MaxLength = 6
        Me.INDTxtDiscountPercent.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtDiscountPercent.StyleController = Me.INDLyProductsContract
        Me.INDTxtDiscountPercent.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtDiscountPercent, 0)
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgProduct, Me.INDLcgCostValue})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(918, 348)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDLcgProduct
        '
        Me.INDLcgProduct.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgProduct.AppearanceGroup.Options.UseFont = True
        Me.INDLcgProduct.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgProduct.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgProduct.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgProduct.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgProduct.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgProduct.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgProduct.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgProduct.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgProduct.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgProduct.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgProduct.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgProduct.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgProduct, False)
        Me.INDLcgProduct.CustomizationFormText = "LayoutControlGroup2"
        Me.INDLcgProduct.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyPceProducts, Me.INDLyTxtProductName, Me.INDLyTxtUnitPurcharse, Me.INDLyTxtLastPurcharse})
        Me.INDLcgProduct.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgProduct.Name = "INDLcgProduct"
        Me.INDLcgProduct.Size = New System.Drawing.Size(414, 328)
        Me.INDLcgProduct.Text = "Producto"
        '
        'INDLyPceProducts
        '
        Me.INDLyPceProducts.Control = Me.INDPceProducts
        Me.INDLyPceProducts.CustomizationFormText = "LayoutControlItem1"
        Me.INDLyPceProducts.Location = New System.Drawing.Point(0, 0)
        Me.INDLyPceProducts.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLyPceProducts.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLyPceProducts.Name = "INDLyPceProducts"
        Me.INDLyPceProducts.ShowInCustomizationForm = False
        Me.INDLyPceProducts.Size = New System.Drawing.Size(390, 60)
        Me.INDLyPceProducts.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyPceProducts.Text = "Producto"
        Me.INDLyPceProducts.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyPceProducts.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyPceProducts.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLyPceProducts.TextToControlDistance = 5
        '
        'INDLyTxtProductName
        '
        Me.INDLyTxtProductName.Control = Me.INDTxtProductName
        Me.INDLyTxtProductName.CustomizationFormText = "Nombre"
        Me.INDLyTxtProductName.Location = New System.Drawing.Point(0, 60)
        Me.INDLyTxtProductName.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLyTxtProductName.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLyTxtProductName.Name = "INDLyTxtProductName"
        Me.INDLyTxtProductName.Size = New System.Drawing.Size(390, 60)
        Me.INDLyTxtProductName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyTxtProductName.Text = "Nombre"
        Me.INDLyTxtProductName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyTxtProductName.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyTxtProductName.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLyTxtProductName.TextToControlDistance = 5
        '
        'INDLyTxtUnitPurcharse
        '
        Me.INDLyTxtUnitPurcharse.Control = Me.INDTxtUnitPurcharse
        Me.INDLyTxtUnitPurcharse.CustomizationFormText = "LayoutControlItem3"
        Me.INDLyTxtUnitPurcharse.Location = New System.Drawing.Point(0, 120)
        Me.INDLyTxtUnitPurcharse.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLyTxtUnitPurcharse.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLyTxtUnitPurcharse.Name = "INDLyTxtUnitPurcharse"
        Me.INDLyTxtUnitPurcharse.Size = New System.Drawing.Size(390, 60)
        Me.INDLyTxtUnitPurcharse.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyTxtUnitPurcharse.Text = "Unidad"
        Me.INDLyTxtUnitPurcharse.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyTxtUnitPurcharse.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyTxtUnitPurcharse.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLyTxtUnitPurcharse.TextToControlDistance = 5
        '
        'INDLyTxtLastPurcharse
        '
        Me.INDLyTxtLastPurcharse.Control = Me.INDTxtLastPurcharse
        Me.INDLyTxtLastPurcharse.CustomizationFormText = "LayoutControlItem4"
        Me.INDLyTxtLastPurcharse.Location = New System.Drawing.Point(0, 180)
        Me.INDLyTxtLastPurcharse.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLyTxtLastPurcharse.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLyTxtLastPurcharse.Name = "INDLyTxtLastPurcharse"
        Me.INDLyTxtLastPurcharse.ShowInCustomizationForm = False
        Me.INDLyTxtLastPurcharse.Size = New System.Drawing.Size(390, 95)
        Me.INDLyTxtLastPurcharse.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyTxtLastPurcharse.Text = "Valor Unitario"
        Me.INDLyTxtLastPurcharse.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyTxtLastPurcharse.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyTxtLastPurcharse.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLyTxtLastPurcharse.TextToControlDistance = 5
        '
        'INDLcgCostValue
        '
        Me.INDLcgCostValue.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgCostValue.AppearanceGroup.Options.UseFont = True
        Me.INDLcgCostValue.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgCostValue.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgCostValue.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgCostValue.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgCostValue.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgCostValue.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgCostValue.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgCostValue.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgCostValue.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgCostValue.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgCostValue.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgCostValue.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgCostValue, False)
        Me.INDLcgCostValue.CustomizationFormText = "LayoutControlGroup3"
        Me.INDLcgCostValue.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyTxtSubTotal, Me.INDLyTxtIvaPercent, Me.INDLyTxtQuantity, Me.INDLyTxtDiscountPercent})
        Me.INDLcgCostValue.Location = New System.Drawing.Point(414, 0)
        Me.INDLcgCostValue.Name = "INDLcgCostValue"
        Me.INDLcgCostValue.Size = New System.Drawing.Size(484, 328)
        Me.INDLcgCostValue.Text = "Valor"
        '
        'INDLyTxtSubTotal
        '
        Me.INDLyTxtSubTotal.Control = Me.INDTxtSubTotal
        Me.INDLyTxtSubTotal.CustomizationFormText = "LayoutControlItem6"
        Me.INDLyTxtSubTotal.Location = New System.Drawing.Point(0, 60)
        Me.INDLyTxtSubTotal.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLyTxtSubTotal.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLyTxtSubTotal.Name = "INDLyTxtSubTotal"
        Me.INDLyTxtSubTotal.Size = New System.Drawing.Size(460, 60)
        Me.INDLyTxtSubTotal.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyTxtSubTotal.Text = "Sub Total"
        Me.INDLyTxtSubTotal.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyTxtSubTotal.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyTxtSubTotal.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLyTxtSubTotal.TextToControlDistance = 5
        '
        'INDLyTxtIvaPercent
        '
        Me.INDLyTxtIvaPercent.Control = Me.INDTxtIvaPercent
        Me.INDLyTxtIvaPercent.CustomizationFormText = "LayoutControlItem7"
        Me.INDLyTxtIvaPercent.Location = New System.Drawing.Point(0, 180)
        Me.INDLyTxtIvaPercent.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLyTxtIvaPercent.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLyTxtIvaPercent.Name = "INDLyTxtIvaPercent"
        Me.INDLyTxtIvaPercent.ShowInCustomizationForm = False
        Me.INDLyTxtIvaPercent.Size = New System.Drawing.Size(460, 95)
        Me.INDLyTxtIvaPercent.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyTxtIvaPercent.Text = "Porcentaje IVA"
        Me.INDLyTxtIvaPercent.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyTxtIvaPercent.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyTxtIvaPercent.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLyTxtIvaPercent.TextToControlDistance = 5
        '
        'INDLyTxtQuantity
        '
        Me.INDLyTxtQuantity.Control = Me.INDTxtQuantity
        Me.INDLyTxtQuantity.CustomizationFormText = "Cantidad"
        Me.INDLyTxtQuantity.Location = New System.Drawing.Point(0, 0)
        Me.INDLyTxtQuantity.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLyTxtQuantity.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLyTxtQuantity.Name = "INDLyTxtQuantity"
        Me.INDLyTxtQuantity.ShowInCustomizationForm = False
        Me.INDLyTxtQuantity.Size = New System.Drawing.Size(460, 60)
        Me.INDLyTxtQuantity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyTxtQuantity.Text = "Cantidad"
        Me.INDLyTxtQuantity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyTxtQuantity.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyTxtQuantity.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLyTxtQuantity.TextToControlDistance = 5
        '
        'INDLyTxtDiscountPercent
        '
        Me.INDLyTxtDiscountPercent.Control = Me.INDTxtDiscountPercent
        Me.INDLyTxtDiscountPercent.CustomizationFormText = "LayoutControlItem8"
        Me.INDLyTxtDiscountPercent.Location = New System.Drawing.Point(0, 120)
        Me.INDLyTxtDiscountPercent.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLyTxtDiscountPercent.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLyTxtDiscountPercent.Name = "INDLyTxtDiscountPercent"
        Me.INDLyTxtDiscountPercent.ShowInCustomizationForm = False
        Me.INDLyTxtDiscountPercent.Size = New System.Drawing.Size(460, 60)
        Me.INDLyTxtDiscountPercent.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyTxtDiscountPercent.Text = "Porcentaje Descuento"
        Me.INDLyTxtDiscountPercent.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyTxtDiscountPercent.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyTxtDiscountPercent.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLyTxtDiscountPercent.TextToControlDistance = 5
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.INDBtnAddProduct)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(202, 354)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PanelControl1.MaximumSize = New System.Drawing.Size(0, 36)
        Me.PanelControl1.MinimumSize = New System.Drawing.Size(0, 36)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(918, 36)
        Me.PanelControl1.TabIndex = 1
        '
        'INDBtnAddProduct
        '
        Me.INDBtnAddProduct.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDBtnAddProduct.Location = New System.Drawing.Point(2, 2)
        Me.INDBtnAddProduct.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDBtnAddProduct.Name = "INDBtnAddProduct"
        Me.INDBtnAddProduct.Size = New System.Drawing.Size(914, 32)
        Me.INDBtnAddProduct.TabIndex = 0
        Me.INDBtnAddProduct.Text = "Agregar"
        '
        'PopupProductsContract
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1122, 526)
        Me.IconOptions.ShowIcon = False
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "PopupProductsContract"
        Me.Opacity = 1.0R
        Me.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.Text = "Agregar Producto"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyProductsContract, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLyProductsContract.ResumeLayout(False)
        CType(Me.INDPupCProducto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPupCProducto.ResumeLayout(False)
        CType(Me.INDTxtSubTotal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtLastPurcharse.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtUnitPurcharse.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtProductName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPceProducts.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtQuantity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtIvaPercent.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtDiscountPercent.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgProduct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyPceProducts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyTxtProductName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyTxtUnitPurcharse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyTxtLastPurcharse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgCostValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyTxtSubTotal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyTxtIvaPercent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyTxtQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyTxtDiscountPercent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDLyProductsContract As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents INDLcgProduct As DevExpress.XtraLayout.LayoutControlGroup
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDLyPceProducts As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDTxtProductName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLyTxtProductName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDTxtLastPurcharse As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtUnitPurcharse As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLyTxtUnitPurcharse As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyTxtLastPurcharse As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDTxtSubTotal As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLyTxtQuantity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyTxtSubTotal As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLcgCostValue As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLyTxtIvaPercent As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyTxtDiscountPercent As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDBtnAddProduct As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDPceProducts As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents INDPupCProducto As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents CtrProducts1 As Presentation.Controls.CtrProducts
    Friend WithEvents IndigoPopUpContainerEdit1 As Presentation.Controls.IndigoPopUpContainerEdit
    Friend WithEvents INDTxtQuantity As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDTxtIvaPercent As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDTxtDiscountPercent As DevExpress.XtraEditors.SpinEdit
End Class
