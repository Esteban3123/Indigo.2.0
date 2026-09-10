Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PopUpProductPurchaseRequest
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
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDPpccProduct = New DevExpress.XtraEditors.PopupContainerControl()
        Me.CtrProducts1 = New Presentation.Controls.CtrProducts()
        Me.INDPncAdd = New DevExpress.XtraEditors.PanelControl()
        Me.INDSmbAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.INDMmeDescription = New DevExpress.XtraEditors.MemoEdit()
        Me.INDSpeQuantity = New DevExpress.XtraEditors.SpinEdit()
        Me.INDTxeMeasureUnit = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxeUnit = New DevExpress.XtraEditors.TextEdit()
        Me.INDPpceProduct = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcgProduct = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciProduct = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciUnit = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciMeasureUnit = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciQuantity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciDescription = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoPopUpContainerEdit1 = New Presentation.Controls.IndigoPopUpContainerEdit(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        CType(Me.INDPanelControlBase,System.ComponentModel.ISupportInitialize).BeginInit
        Me.INDPanelControlBase.SuspendLayout
        CType(Me.ToolBars,System.ComponentModel.ISupportInitialize).BeginInit
        Me.ToolBars.SuspendLayout
        CType(Me.LayoutControls,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.CtrNavigationControlPanel1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControl1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.LayoutControl1.SuspendLayout
        CType(Me.INDPpccProduct,System.ComponentModel.ISupportInitialize).BeginInit
        Me.INDPpccProduct.SuspendLayout
        CType(Me.INDPncAdd,System.ComponentModel.ISupportInitialize).BeginInit
        Me.INDPncAdd.SuspendLayout
        CType(Me.INDMmeDescription.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDSpeQuantity.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDTxeMeasureUnit.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDTxeUnit.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDPpceProduct.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlGroup1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDLcgProduct,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDLciProduct,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDLciUnit,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDLciMeasureUnit,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDLciQuantity,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDLciDescription,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoLayoutControlGroup1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoPopUpContainerEdit1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoTextEdit1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoSimpleButton1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.LayoutControl1)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1008, 594)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1008, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(1008, 130)
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.LayoutControl1
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 585)
        Me.CtrNavigationControlPanel1.TabIndex = 0
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDPpccProduct)
        Me.LayoutControl1.Controls.Add(Me.INDPncAdd)
        Me.LayoutControl1.Controls.Add(Me.INDMmeDescription)
        Me.LayoutControl1.Controls.Add(Me.INDSpeQuantity)
        Me.LayoutControl1.Controls.Add(Me.INDTxeMeasureUnit)
        Me.LayoutControl1.Controls.Add(Me.INDTxeUnit)
        Me.LayoutControl1.Controls.Add(Me.INDPpceProduct)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(202, 7)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(804, 585)
        Me.LayoutControl1.TabIndex = 1
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDPpccProduct
        '
        Me.INDPpccProduct.Controls.Add(Me.CtrProducts1)
        Me.INDPpccProduct.Location = New System.Drawing.Point(446, 59)
        Me.INDPpccProduct.Name = "INDPpccProduct"
        Me.INDPpccProduct.Size = New System.Drawing.Size(701, 404)
        Me.INDPpccProduct.TabIndex = 11
        '
        'CtrProducts1
        '
        Me.CtrProducts1.DataSource = Nothing
        Me.CtrProducts1.Location = New System.Drawing.Point(3, 3)
        Me.CtrProducts1.Name = "CtrProducts1"
        Me.CtrProducts1.Size = New System.Drawing.Size(663, 376)
        Me.CtrProducts1.TabIndex = 0
        '
        'INDPncAdd
        '
        Me.INDPncAdd.Controls.Add(Me.INDSmbAdd)
        Me.INDPncAdd.Location = New System.Drawing.Point(12, 537)
        Me.INDPncAdd.MaximumSize = New System.Drawing.Size(0, 36)
        Me.INDPncAdd.MinimumSize = New System.Drawing.Size(0, 36)
        Me.INDPncAdd.Name = "INDPncAdd"
        Me.INDPncAdd.Size = New System.Drawing.Size(780, 36)
        Me.INDPncAdd.TabIndex = 10
        '
        'INDSmbAdd
        '
        Me.INDSmbAdd.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSmbAdd.Appearance.Options.UseFont = True
        Me.INDSmbAdd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDSmbAdd.Location = New System.Drawing.Point(2, 2)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDSmbAdd, True)
        Me.INDSmbAdd.Name = "INDSmbAdd"
        Me.INDSmbAdd.Size = New System.Drawing.Size(776, 32)
        Me.INDSmbAdd.TabIndex = 0
        Me.INDSmbAdd.Text = "Agregar"
        '
        'INDMmeDescription
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDMmeDescription, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDMmeDescription, False)
        Me.INDMmeDescription.EnterMoveNextControl = True
        Me.INDMmeDescription.Location = New System.Drawing.Point(24, 335)
        Me.IndigoTextEdit1.SetMascara(Me.INDMmeDescription, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDMmeDescription.Name = "INDMmeDescription"
        Me.INDMmeDescription.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDMmeDescription.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDMmeDescription.Properties.Appearance.Options.UseBackColor = True
        Me.INDMmeDescription.Properties.Appearance.Options.UseFont = True
        Me.INDMmeDescription.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDMmeDescription.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDMmeDescription.Properties.MaxLength = 300
        Me.INDMmeDescription.Size = New System.Drawing.Size(386, 186)
        Me.INDMmeDescription.StyleController = Me.LayoutControl1
        Me.INDMmeDescription.TabIndex = 8
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDMmeDescription, 0)
        '
        'INDSpeQuantity
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSpeQuantity, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSpeQuantity, True)
        Me.INDSpeQuantity.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDSpeQuantity.EnterMoveNextControl = True
        Me.INDSpeQuantity.Location = New System.Drawing.Point(24, 271)
        Me.INDSpeQuantity.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDSpeQuantity, Presentation.Controls.IndigoTextEdit.EMask.Numerico)
        Me.INDSpeQuantity.Name = "INDSpeQuantity"
        Me.INDSpeQuantity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSpeQuantity.Properties.Appearance.Options.UseFont = True
        Me.INDSpeQuantity.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSpeQuantity.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSpeQuantity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSpeQuantity.Properties.Mask.EditMask = "[0-9]+"
        Me.INDSpeQuantity.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDSpeQuantity.Properties.MaxLength = 9
        Me.INDSpeQuantity.Properties.MaxValue = New Decimal(New Integer() {2147483646, 0, 0, 0})
        Me.INDSpeQuantity.Size = New System.Drawing.Size(386, 28)
        Me.INDSpeQuantity.StyleController = Me.LayoutControl1
        Me.INDSpeQuantity.TabIndex = 7
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSpeQuantity, 0)
        '
        'INDTxeMeasureUnit
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxeMeasureUnit, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxeMeasureUnit, False)
        Me.INDTxeMeasureUnit.Enabled = False
        Me.INDTxeMeasureUnit.EnterMoveNextControl = True
        Me.INDTxeMeasureUnit.Location = New System.Drawing.Point(24, 207)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxeMeasureUnit, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxeMeasureUnit.Name = "INDTxeMeasureUnit"
        Me.INDTxeMeasureUnit.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxeMeasureUnit.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxeMeasureUnit.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxeMeasureUnit.Properties.Appearance.Options.UseFont = True
        Me.INDTxeMeasureUnit.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxeMeasureUnit.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxeMeasureUnit.Size = New System.Drawing.Size(386, 28)
        Me.INDTxeMeasureUnit.StyleController = Me.LayoutControl1
        Me.INDTxeMeasureUnit.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxeMeasureUnit, 0)
        '
        'INDTxeUnit
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxeUnit, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxeUnit, False)
        Me.INDTxeUnit.Enabled = False
        Me.INDTxeUnit.EnterMoveNextControl = True
        Me.INDTxeUnit.Location = New System.Drawing.Point(24, 143)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxeUnit, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxeUnit.Name = "INDTxeUnit"
        Me.INDTxeUnit.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxeUnit.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxeUnit.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxeUnit.Properties.Appearance.Options.UseFont = True
        Me.INDTxeUnit.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxeUnit.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxeUnit.Size = New System.Drawing.Size(386, 28)
        Me.INDTxeUnit.StyleController = Me.LayoutControl1
        Me.INDTxeUnit.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxeUnit, 0)
        '
        'INDPpceProduct
        '
        Me.IndigoPopUpContainerEdit1.SetButtonMoreOptions(Me.INDPpceProduct, False)
        Me.INDPpceProduct.EnterMoveNextControl = True
        Me.IndigoPopUpContainerEdit1.SetHostControl(Me.INDPpceProduct, Nothing)
        Me.INDPpceProduct.Location = New System.Drawing.Point(24, 79)
        Me.INDPpceProduct.MinimumSize = New System.Drawing.Size(386, 32)
        Me.INDPpceProduct.Name = "INDPpceProduct"
        Me.IndigoPopUpContainerEdit1.SetOpenForm(Me.INDPpceProduct, True)
        Me.IndigoPopUpContainerEdit1.SetPopUpAnimation(Me.INDPpceProduct, False)
        Me.INDPpceProduct.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDPpceProduct.Properties.Appearance.Options.UseFont = True
        Me.INDPpceProduct.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDPpceProduct.Properties.PopupControl = Me.INDPpccProduct
        Me.INDPpceProduct.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.INDPpceProduct.Size = New System.Drawing.Size(386, 32)
        Me.INDPpceProduct.StyleController = Me.LayoutControl1
        Me.INDPpceProduct.TabIndex = 4
        Me.IndigoPopUpContainerEdit1.SetTagForm(Me.INDPpceProduct, "304")
        Me.INDPpceProduct.ToolTip = "Este Campo es Necesario"
        Me.IndigoPopUpContainerEdit1.SetWpfControl(Me.INDPpceProduct, Nothing)
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgProduct, Me.LayoutControlItem1})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(804, 585)
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
        Me.INDLcgProduct.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciProduct, Me.INDLciUnit, Me.INDLciMeasureUnit, Me.INDLciQuantity, Me.INDLciDescription})
        Me.INDLcgProduct.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgProduct.Name = "INDLcgProduct"
        Me.INDLcgProduct.Size = New System.Drawing.Size(784, 525)
        Me.INDLcgProduct.Text = "Producto"
        '
        'INDLciProduct
        '
        Me.INDLciProduct.AllowHide = False
        Me.INDLciProduct.Control = Me.INDPpceProduct
        Me.INDLciProduct.Location = New System.Drawing.Point(0, 0)
        Me.INDLciProduct.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciProduct.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciProduct.Name = "INDLciProduct"
        Me.INDLciProduct.ShowInCustomizationForm = False
        Me.INDLciProduct.Size = New System.Drawing.Size(760, 64)
        Me.INDLciProduct.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciProduct.Text = "Nombre"
        Me.INDLciProduct.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciProduct.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciProduct.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciProduct.TextToControlDistance = 5
        '
        'INDLciUnit
        '
        Me.INDLciUnit.Control = Me.INDTxeUnit
        Me.INDLciUnit.Location = New System.Drawing.Point(0, 64)
        Me.INDLciUnit.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciUnit.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciUnit.Name = "INDLciUnit"
        Me.INDLciUnit.Size = New System.Drawing.Size(760, 64)
        Me.INDLciUnit.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciUnit.Text = "Unidad"
        Me.INDLciUnit.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciUnit.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciUnit.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciUnit.TextToControlDistance = 5
        '
        'INDLciMeasureUnit
        '
        Me.INDLciMeasureUnit.Control = Me.INDTxeMeasureUnit
        Me.INDLciMeasureUnit.Location = New System.Drawing.Point(0, 128)
        Me.INDLciMeasureUnit.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciMeasureUnit.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciMeasureUnit.Name = "INDLciMeasureUnit"
        Me.INDLciMeasureUnit.Size = New System.Drawing.Size(760, 64)
        Me.INDLciMeasureUnit.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciMeasureUnit.Text = "Unidad de Medida"
        Me.INDLciMeasureUnit.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciMeasureUnit.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciMeasureUnit.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciMeasureUnit.TextToControlDistance = 5
        '
        'INDLciQuantity
        '
        Me.INDLciQuantity.AllowHide = False
        Me.INDLciQuantity.Control = Me.INDSpeQuantity
        Me.INDLciQuantity.Location = New System.Drawing.Point(0, 192)
        Me.INDLciQuantity.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciQuantity.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciQuantity.Name = "INDLciQuantity"
        Me.INDLciQuantity.ShowInCustomizationForm = False
        Me.INDLciQuantity.Size = New System.Drawing.Size(760, 64)
        Me.INDLciQuantity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciQuantity.Text = "Cantidad"
        Me.INDLciQuantity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciQuantity.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciQuantity.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciQuantity.TextToControlDistance = 5
        '
        'INDLciDescription
        '
        Me.INDLciDescription.Control = Me.INDMmeDescription
        Me.INDLciDescription.Location = New System.Drawing.Point(0, 256)
        Me.INDLciDescription.MaxSize = New System.Drawing.Size(390, 0)
        Me.INDLciDescription.MinSize = New System.Drawing.Size(390, 80)
        Me.INDLciDescription.Name = "INDLciDescription"
        Me.INDLciDescription.Size = New System.Drawing.Size(760, 216)
        Me.INDLciDescription.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDescription.Text = "Descripción"
        Me.INDLciDescription.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciDescription.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDescription.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciDescription.TextToControlDistance = 5
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDPncAdd
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 525)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(784, 40)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'PopUpProductPurchaseRequest
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 729)
        Me.IconOptions.ShowIcon = False
        Me.Name = "PopUpProductPurchaseRequest"
        Me.Opacity = 1R
        Me.Text = "Agregar Productos"
        CType(Me.INDPanelControlBase,System.ComponentModel.ISupportInitialize).EndInit
        Me.INDPanelControlBase.ResumeLayout(false)
        CType(Me.ToolBars,System.ComponentModel.ISupportInitialize).EndInit
        Me.ToolBars.ResumeLayout(false)
        CType(Me.LayoutControls,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.CtrNavigationControlPanel1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControl1,System.ComponentModel.ISupportInitialize).EndInit
        Me.LayoutControl1.ResumeLayout(false)
        CType(Me.INDPpccProduct,System.ComponentModel.ISupportInitialize).EndInit
        Me.INDPpccProduct.ResumeLayout(false)
        CType(Me.INDPncAdd,System.ComponentModel.ISupportInitialize).EndInit
        Me.INDPncAdd.ResumeLayout(false)
        CType(Me.INDMmeDescription.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDSpeQuantity.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDTxeMeasureUnit.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDTxeUnit.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDPpceProduct.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDLcgProduct,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDLciProduct,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDLciUnit,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDLciMeasureUnit,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDLciQuantity,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDLciDescription,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoLayoutControlGroup1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoPopUpContainerEdit1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoTextEdit1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoSimpleButton1,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub

    Friend WithEvents CtrNavigationControlPanel1 As CtrNavigationControlPanel
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDPpceProduct As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents INDLciProduct As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDTxeMeasureUnit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxeUnit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciUnit As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciMeasureUnit As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDMmeDescription As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDSpeQuantity As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDLciQuantity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciDescription As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLcgProduct As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As IndigoTextEdit
    Friend WithEvents IndigoPopUpContainerEdit1 As IndigoPopUpContainerEdit
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents CtrProducts1 As CtrProducts
    Friend WithEvents INDPncAdd As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSmbAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents IndigoSimpleButton1 As IndigoSimpleButton
    Friend WithEvents INDPpccProduct As DevExpress.XtraEditors.PopupContainerControl
End Class
