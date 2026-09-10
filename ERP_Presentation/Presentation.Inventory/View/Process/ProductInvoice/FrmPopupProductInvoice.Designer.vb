Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPopupProductInvoice
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
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.INDBtnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDPcePhysicalInventory = New DevExpress.XtraEditors.PopupContainerControl()
        Me.CtrPhysicalInventory1 = New Presentation.Controls.CtrPhysicalInventory()
        Me.INDTxtTotal = New DevExpress.XtraEditors.TextEdit()
        Me.INDSeIVAPercent = New DevExpress.XtraEditors.SpinEdit()
        Me.INDSeDiscountPercent = New DevExpress.XtraEditors.SpinEdit()
        Me.INDTxtSalePrice = New DevExpress.XtraEditors.TextEdit()
        Me.INDPceBatchSerial = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.INDSeQuantity = New DevExpress.XtraEditors.SpinEdit()
        Me.INDPccProduct = New DevExpress.XtraEditors.PopupContainerControl()
        Me.CtrProducts1 = New Presentation.Controls.CtrProducts()
        Me.INDPceProduct = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciQuantity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciBatchSerial = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit()
        Me.IndigoPopUpContainerEdit1 = New Presentation.Controls.IndigoPopUpContainerEdit()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDPcePhysicalInventory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPcePhysicalInventory.SuspendLayout()
        CType(Me.INDTxtTotal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSeIVAPercent.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSeDiscountPercent.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtSalePrice.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPceBatchSerial.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSeQuantity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPccProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPccProduct.SuspendLayout()
        CType(Me.INDPceProduct.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciBatchSerial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.LayoutControl1)
        Me.INDPanelControlBase.Controls.Add(Me.PanelControl1)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 118)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1465, 583)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1465, 94)
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.LayoutControl1
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 574)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.INDBtnAdd)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(202, 545)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(1261, 36)
        Me.PanelControl1.TabIndex = 1
        '
        'INDBtnAdd
        '
        Me.INDBtnAdd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDBtnAdd.Location = New System.Drawing.Point(2, 2)
        Me.INDBtnAdd.Name = "INDBtnAdd"
        Me.INDBtnAdd.Size = New System.Drawing.Size(1257, 32)
        Me.INDBtnAdd.TabIndex = 0
        Me.INDBtnAdd.Text = "Agregar"
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDPcePhysicalInventory)
        Me.LayoutControl1.Controls.Add(Me.INDTxtTotal)
        Me.LayoutControl1.Controls.Add(Me.INDSeIVAPercent)
        Me.LayoutControl1.Controls.Add(Me.INDSeDiscountPercent)
        Me.LayoutControl1.Controls.Add(Me.INDTxtSalePrice)
        Me.LayoutControl1.Controls.Add(Me.INDPceBatchSerial)
        Me.LayoutControl1.Controls.Add(Me.INDSeQuantity)
        Me.LayoutControl1.Controls.Add(Me.INDPccProduct)
        Me.LayoutControl1.Controls.Add(Me.INDPceProduct)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(202, 7)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(1261, 538)
        Me.LayoutControl1.TabIndex = 2
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDPcePhysicalInventory
        '
        Me.INDPcePhysicalInventory.Controls.Add(Me.CtrPhysicalInventory1)
        Me.INDPcePhysicalInventory.Location = New System.Drawing.Point(110, 376)
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
        'INDTxtTotal
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtTotal, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtTotal, False)
        Me.INDTxtTotal.EditValue = "0"
        Me.INDTxtTotal.Location = New System.Drawing.Point(438, 265)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtTotal, Presentation.Controls.IndigoTextEdit.EMask.Moneda)
        Me.INDTxtTotal.Name = "INDTxtTotal"
        Me.INDTxtTotal.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDTxtTotal.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtTotal.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtTotal.Properties.Appearance.Options.UseFont = True
        Me.INDTxtTotal.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTxtTotal.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTxtTotal.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtTotal.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtTotal.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtTotal.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtTotal.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtTotal.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtTotal.Properties.Mask.EditMask = "c0"
        Me.INDTxtTotal.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDTxtTotal.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtTotal.Properties.ReadOnly = True
        Me.INDTxtTotal.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtTotal.StyleController = Me.LayoutControl1
        Me.INDTxtTotal.TabIndex = 17
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtTotal, 0)
        '
        'INDSeIVAPercent
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSeIVAPercent, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSeIVAPercent, True)
        Me.INDSeIVAPercent.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDSeIVAPercent.EnterMoveNextControl = True
        Me.INDSeIVAPercent.Location = New System.Drawing.Point(438, 205)
        Me.INDSeIVAPercent.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDSeIVAPercent, Presentation.Controls.IndigoTextEdit.EMask.Porcentaje)
        Me.INDSeIVAPercent.Name = "INDSeIVAPercent"
        Me.INDSeIVAPercent.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDSeIVAPercent.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeIVAPercent.Properties.Appearance.Options.UseBackColor = True
        Me.INDSeIVAPercent.Properties.Appearance.Options.UseFont = True
        Me.INDSeIVAPercent.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSeIVAPercent.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSeIVAPercent.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeIVAPercent.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSeIVAPercent.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSeIVAPercent.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSeIVAPercent.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSeIVAPercent.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.INDSeIVAPercent.Properties.Mask.EditMask = "P"
        Me.INDSeIVAPercent.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDSeIVAPercent.Properties.MaxLength = 6
        Me.INDSeIVAPercent.Properties.ReadOnly = True
        Me.INDSeIVAPercent.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.INDSeIVAPercent.Size = New System.Drawing.Size(386, 28)
        Me.INDSeIVAPercent.StyleController = Me.LayoutControl1
        Me.INDSeIVAPercent.TabIndex = 16
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSeIVAPercent, 0)
        '
        'INDSeDiscountPercent
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSeDiscountPercent, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSeDiscountPercent, True)
        Me.INDSeDiscountPercent.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDSeDiscountPercent.EnterMoveNextControl = True
        Me.INDSeDiscountPercent.Location = New System.Drawing.Point(438, 145)
        Me.INDSeDiscountPercent.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDSeDiscountPercent, Presentation.Controls.IndigoTextEdit.EMask.Porcentaje)
        Me.INDSeDiscountPercent.Name = "INDSeDiscountPercent"
        Me.INDSeDiscountPercent.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDSeDiscountPercent.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeDiscountPercent.Properties.Appearance.Options.UseBackColor = True
        Me.INDSeDiscountPercent.Properties.Appearance.Options.UseFont = True
        Me.INDSeDiscountPercent.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSeDiscountPercent.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSeDiscountPercent.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeDiscountPercent.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSeDiscountPercent.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSeDiscountPercent.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSeDiscountPercent.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSeDiscountPercent.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.INDSeDiscountPercent.Properties.Mask.EditMask = "P"
        Me.INDSeDiscountPercent.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDSeDiscountPercent.Properties.MaxLength = 7
        Me.INDSeDiscountPercent.Properties.MaxValue = New Decimal(New Integer() {100, 0, 0, 0})
        Me.INDSeDiscountPercent.Size = New System.Drawing.Size(386, 28)
        Me.INDSeDiscountPercent.StyleController = Me.LayoutControl1
        Me.INDSeDiscountPercent.TabIndex = 15
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSeDiscountPercent, 0)
        '
        'INDTxtSalePrice
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtSalePrice, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtSalePrice, True)
        Me.INDTxtSalePrice.EditValue = "0"
        Me.INDTxtSalePrice.EnterMoveNextControl = True
        Me.INDTxtSalePrice.Location = New System.Drawing.Point(438, 85)
        Me.INDTxtSalePrice.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtSalePrice, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtSalePrice.Name = "INDTxtSalePrice"
        Me.INDTxtSalePrice.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDTxtSalePrice.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtSalePrice.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtSalePrice.Properties.Appearance.Options.UseFont = True
        Me.INDTxtSalePrice.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTxtSalePrice.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTxtSalePrice.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtSalePrice.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtSalePrice.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtSalePrice.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtSalePrice.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtSalePrice.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtSalePrice.Properties.Mask.EditMask = "c2"
        Me.INDTxtSalePrice.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDTxtSalePrice.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtSalePrice.Properties.MaxLength = 20
        Me.INDTxtSalePrice.Properties.UseReadOnlyAppearance = False
        Me.INDTxtSalePrice.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtSalePrice.StyleController = Me.LayoutControl1
        Me.INDTxtSalePrice.TabIndex = 14
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtSalePrice, 0)
        '
        'INDPceBatchSerial
        '
        Me.IndigoPopUpContainerEdit1.SetButtonMoreOptions(Me.INDPceBatchSerial, False)
        Me.INDPceBatchSerial.EnterMoveNextControl = True
        Me.IndigoPopUpContainerEdit1.SetHostControl(Me.INDPceBatchSerial, Nothing)
        Me.INDPceBatchSerial.Location = New System.Drawing.Point(24, 205)
        Me.INDPceBatchSerial.MinimumSize = New System.Drawing.Size(386, 32)
        Me.INDPceBatchSerial.Name = "INDPceBatchSerial"
        Me.IndigoPopUpContainerEdit1.SetOpenForm(Me.INDPceBatchSerial, False)
        Me.IndigoPopUpContainerEdit1.SetPopUpAnimation(Me.INDPceBatchSerial, False)
        Me.INDPceBatchSerial.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDPceBatchSerial.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDPceBatchSerial.Properties.Appearance.Options.UseBackColor = True
        Me.INDPceBatchSerial.Properties.Appearance.Options.UseFont = True
        Me.INDPceBatchSerial.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDPceBatchSerial.Properties.PopupControl = Me.INDPcePhysicalInventory
        Me.INDPceBatchSerial.Size = New System.Drawing.Size(386, 32)
        Me.INDPceBatchSerial.StyleController = Me.LayoutControl1
        Me.INDPceBatchSerial.TabIndex = 12
        Me.IndigoPopUpContainerEdit1.SetTagForm(Me.INDPceBatchSerial, Nothing)
        Me.IndigoPopUpContainerEdit1.SetWpfControl(Me.INDPceBatchSerial, Nothing)
        '
        'INDSeQuantity
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSeQuantity, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSeQuantity, True)
        Me.INDSeQuantity.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDSeQuantity.EnterMoveNextControl = True
        Me.INDSeQuantity.Location = New System.Drawing.Point(24, 145)
        Me.INDSeQuantity.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDSeQuantity, Presentation.Controls.IndigoTextEdit.EMask.Numerico)
        Me.INDSeQuantity.Name = "INDSeQuantity"
        Me.INDSeQuantity.Properties.Appearance.BackColor = System.Drawing.Color.White
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
        Me.INDSeQuantity.Size = New System.Drawing.Size(386, 28)
        Me.INDSeQuantity.StyleController = Me.LayoutControl1
        Me.INDSeQuantity.TabIndex = 11
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSeQuantity, 0)
        '
        'INDPccProduct
        '
        Me.INDPccProduct.Controls.Add(Me.CtrProducts1)
        Me.INDPccProduct.Location = New System.Drawing.Point(690, 374)
        Me.INDPccProduct.Name = "INDPccProduct"
        Me.INDPccProduct.Size = New System.Drawing.Size(657, 451)
        Me.INDPccProduct.TabIndex = 10
        '
        'CtrProducts1
        '
        Me.CtrProducts1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrProducts1.Location = New System.Drawing.Point(0, 0)
        Me.CtrProducts1.Name = "CtrProducts1"
        Me.CtrProducts1.Size = New System.Drawing.Size(657, 451)
        Me.CtrProducts1.TabIndex = 0
        '
        'INDPceProduct
        '
        Me.IndigoPopUpContainerEdit1.SetButtonMoreOptions(Me.INDPceProduct, False)
        Me.IndigoPopUpContainerEdit1.SetHostControl(Me.INDPceProduct, Nothing)
        Me.INDPceProduct.Location = New System.Drawing.Point(24, 85)
        Me.INDPceProduct.MinimumSize = New System.Drawing.Size(386, 32)
        Me.INDPceProduct.Name = "INDPceProduct"
        Me.IndigoPopUpContainerEdit1.SetOpenForm(Me.INDPceProduct, True)
        Me.IndigoPopUpContainerEdit1.SetPopUpAnimation(Me.INDPceProduct, False)
        Me.INDPceProduct.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDPceProduct.Properties.Appearance.Options.UseFont = True
        Me.INDPceProduct.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDPceProduct.Properties.PopupControl = Me.INDPccProduct
        Me.INDPceProduct.Properties.PopupSizeable = False
        Me.INDPceProduct.Properties.ShowPopupCloseButton = False
        Me.INDPceProduct.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.INDPceProduct.Size = New System.Drawing.Size(386, 32)
        Me.INDPceProduct.StyleController = Me.LayoutControl1
        Me.INDPceProduct.TabIndex = 4
        Me.IndigoPopUpContainerEdit1.SetTagForm(Me.INDPceProduct, "304")
        Me.IndigoPopUpContainerEdit1.SetWpfControl(Me.INDPceProduct, Nothing)
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
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup2, Me.LayoutControlGroup3})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1261, 538)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup2.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup2.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup2.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup2, False)
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.INDLciQuantity, Me.INDLciBatchSerial})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(414, 518)
        Me.LayoutControlGroup2.Text = "Datos Principales"
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDPceProduct
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.Text = "Producto"
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(135, 21)
        Me.LayoutControlItem1.TextToControlDistance = 5
        '
        'INDLciQuantity
        '
        Me.INDLciQuantity.Control = Me.INDSeQuantity
        Me.INDLciQuantity.Location = New System.Drawing.Point(0, 60)
        Me.INDLciQuantity.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciQuantity.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciQuantity.Name = "INDLciQuantity"
        Me.INDLciQuantity.Size = New System.Drawing.Size(390, 60)
        Me.INDLciQuantity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciQuantity.Text = "Cantidad"
        Me.INDLciQuantity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciQuantity.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciQuantity.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciQuantity.TextToControlDistance = 5
        '
        'INDLciBatchSerial
        '
        Me.INDLciBatchSerial.Control = Me.INDPceBatchSerial
        Me.INDLciBatchSerial.Location = New System.Drawing.Point(0, 120)
        Me.INDLciBatchSerial.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciBatchSerial.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciBatchSerial.Name = "INDLciBatchSerial"
        Me.INDLciBatchSerial.Size = New System.Drawing.Size(390, 339)
        Me.INDLciBatchSerial.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciBatchSerial.Text = "Lote / Serial"
        Me.INDLciBatchSerial.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciBatchSerial.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciBatchSerial.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciBatchSerial.TextToControlDistance = 5
        '
        'LayoutControlGroup3
        '
        Me.LayoutControlGroup3.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup3.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup3.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup3.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup3.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup3, False)
        Me.LayoutControlGroup3.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem4, Me.LayoutControlItem5, Me.LayoutControlItem6, Me.LayoutControlItem7})
        Me.LayoutControlGroup3.Location = New System.Drawing.Point(414, 0)
        Me.LayoutControlGroup3.Name = "LayoutControlGroup3"
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(827, 518)
        Me.LayoutControlGroup3.Text = "Valor"
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.INDTxtSalePrice
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem4.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem4.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(803, 60)
        Me.LayoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem4.Text = "Precio Unitario de Venta"
        Me.LayoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem4.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(135, 21)
        Me.LayoutControlItem4.TextToControlDistance = 5
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.INDSeDiscountPercent
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 60)
        Me.LayoutControlItem5.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem5.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(803, 60)
        Me.LayoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem5.Text = "Porcentaje Descuento"
        Me.LayoutControlItem5.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem5.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(135, 21)
        Me.LayoutControlItem5.TextToControlDistance = 5
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.INDSeIVAPercent
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 120)
        Me.LayoutControlItem6.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem6.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(803, 60)
        Me.LayoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem6.Text = "Porcentaje IVA"
        Me.LayoutControlItem6.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem6.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(135, 21)
        Me.LayoutControlItem6.TextToControlDistance = 5
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.INDTxtTotal
        Me.LayoutControlItem7.Location = New System.Drawing.Point(0, 180)
        Me.LayoutControlItem7.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem7.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(803, 279)
        Me.LayoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem7.Text = "Total"
        Me.LayoutControlItem7.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem7.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(135, 21)
        Me.LayoutControlItem7.TextToControlDistance = 5
        '
        'FrmPopupProductInvoice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1465, 701)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmPopupProductInvoice"
        Me.Opacity = 1.0R
        Me.Text = "Productos"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDPcePhysicalInventory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPcePhysicalInventory.ResumeLayout(False)
        CType(Me.INDTxtTotal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSeIVAPercent.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSeDiscountPercent.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtSalePrice.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPceBatchSerial.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSeQuantity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPccProduct, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPccProduct.ResumeLayout(False)
        CType(Me.INDPceProduct.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciBatchSerial, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDBtnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoPopUpContainerEdit1 As Presentation.Controls.IndigoPopUpContainerEdit
    Friend WithEvents INDPceProduct As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDPccProduct As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents CtrProducts1 As Presentation.Controls.CtrProducts
    Friend WithEvents INDSeQuantity As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDLciQuantity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDPceBatchSerial As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents INDLciBatchSerial As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDPcePhysicalInventory As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents CtrPhysicalInventory1 As Presentation.Controls.CtrPhysicalInventory
    Friend WithEvents INDTxtSalePrice As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSeDiscountPercent As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSeIVAPercent As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDTxtTotal As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
End Class
