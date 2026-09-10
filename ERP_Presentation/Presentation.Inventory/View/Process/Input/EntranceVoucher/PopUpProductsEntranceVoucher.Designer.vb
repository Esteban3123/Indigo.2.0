Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PopUpProductsEntranceVoucher
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
        Me.INDtxtSubtotalWithDiscount = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtSubtotal = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtMeasureUnit = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtBatchSerialRemision = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtTotal = New DevExpress.XtraEditors.TextEdit()
        Me.INDPupCBatchSerial = New DevExpress.XtraEditors.PopupContainerControl()
        Me.CtrBatchSerial1 = New Presentation.Inventory.CtrBatchSerial()
        Me.INDPupCProducto = New DevExpress.XtraEditors.PopupContainerControl()
        Me.CtrProducts1 = New Presentation.Controls.CtrProducts()
        Me.INDTxtLastValue = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtUnitValue = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtUnitPurcharse = New DevExpress.XtraEditors.TextEdit()
        Me.INDPceProducts = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.INDTxtQuantity = New DevExpress.XtraEditors.SpinEdit()
        Me.INDTxtIvaPercent = New DevExpress.XtraEditors.SpinEdit()
        Me.INDTxtDiscountPercent = New DevExpress.XtraEditors.SpinEdit()
        Me.INDPceBatchSerial = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcgProduct = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyPceProducts = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyTxtUnitPurcharse = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyTxtQuantity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyTxtBatchSerialRemision = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyPceBatchSerial = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciMeasureUnit = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgCostValue = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyTxtDiscountPercent = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyTxtIvaPercent = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyTxtLastValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyTxtTotal = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyTxtINDTxtUnitValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemSubtotal = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemSubtotalWithDiscount = New DevExpress.XtraLayout.LayoutControlItem()
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
        CType(Me.INDtxtSubtotalWithDiscount.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtSubtotal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtMeasureUnit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtBatchSerialRemision.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtTotal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPupCBatchSerial, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPupCBatchSerial.SuspendLayout()
        CType(Me.INDPupCProducto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPupCProducto.SuspendLayout()
        CType(Me.INDTxtLastValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtUnitValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtUnitPurcharse.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPceProducts.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtQuantity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtIvaPercent.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtDiscountPercent.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPceBatchSerial.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyPceProducts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyTxtUnitPurcharse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyTxtQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyTxtBatchSerialRemision, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyPceBatchSerial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciMeasureUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgCostValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyTxtDiscountPercent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyTxtIvaPercent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyTxtLastValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyTxtTotal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyTxtINDTxtUnitValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemSubtotal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemSubtotalWithDiscount, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1118, 574)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Location = New System.Drawing.Point(0, 4)
        Me.ToolBars.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ToolBars.Size = New System.Drawing.Size(1118, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.BarraBotones.Size = New System.Drawing.Size(1118, 130)
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDLyProductsContract
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 6)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 566)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDLyProductsContract
        '
        Me.INDLyProductsContract.Controls.Add(Me.INDtxtSubtotalWithDiscount)
        Me.INDLyProductsContract.Controls.Add(Me.INDtxtSubtotal)
        Me.INDLyProductsContract.Controls.Add(Me.INDTxtMeasureUnit)
        Me.INDLyProductsContract.Controls.Add(Me.INDTxtBatchSerialRemision)
        Me.INDLyProductsContract.Controls.Add(Me.INDTxtTotal)
        Me.INDLyProductsContract.Controls.Add(Me.INDPupCBatchSerial)
        Me.INDLyProductsContract.Controls.Add(Me.INDPupCProducto)
        Me.INDLyProductsContract.Controls.Add(Me.INDTxtLastValue)
        Me.INDLyProductsContract.Controls.Add(Me.INDTxtUnitValue)
        Me.INDLyProductsContract.Controls.Add(Me.INDTxtUnitPurcharse)
        Me.INDLyProductsContract.Controls.Add(Me.INDPceProducts)
        Me.INDLyProductsContract.Controls.Add(Me.INDTxtQuantity)
        Me.INDLyProductsContract.Controls.Add(Me.INDTxtIvaPercent)
        Me.INDLyProductsContract.Controls.Add(Me.INDTxtDiscountPercent)
        Me.INDLyProductsContract.Controls.Add(Me.INDPceBatchSerial)
        Me.INDLyProductsContract.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLyProductsContract.Location = New System.Drawing.Point(202, 6)
        Me.INDLyProductsContract.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDLyProductsContract.Name = "INDLyProductsContract"
        Me.INDLyProductsContract.Root = Me.LayoutControlGroup1
        Me.INDLyProductsContract.Size = New System.Drawing.Size(914, 530)
        Me.INDLyProductsContract.TabIndex = 0
        Me.INDLyProductsContract.Text = "LayoutControl1"
        '
        'INDtxtSubtotalWithDiscount
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtSubtotalWithDiscount, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtSubtotalWithDiscount, False)
        Me.INDtxtSubtotalWithDiscount.EnterMoveNextControl = True
        Me.INDtxtSubtotalWithDiscount.Location = New System.Drawing.Point(438, 319)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtSubtotalWithDiscount, Presentation.Controls.IndigoTextEdit.EMask.MonedaDecimales)
        Me.INDtxtSubtotalWithDiscount.Name = "INDtxtSubtotalWithDiscount"
        Me.INDtxtSubtotalWithDiscount.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtSubtotalWithDiscount.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtSubtotalWithDiscount.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtSubtotalWithDiscount.Properties.Appearance.Options.UseFont = True
        Me.INDtxtSubtotalWithDiscount.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtSubtotalWithDiscount.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtSubtotalWithDiscount.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtSubtotalWithDiscount.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtSubtotalWithDiscount.Properties.Mask.EditMask = "c2"
        Me.INDtxtSubtotalWithDiscount.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtSubtotalWithDiscount.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtSubtotalWithDiscount.Properties.ReadOnly = True
        Me.INDtxtSubtotalWithDiscount.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtSubtotalWithDiscount.StyleController = Me.INDLyProductsContract
        Me.INDtxtSubtotalWithDiscount.TabIndex = 10
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtSubtotalWithDiscount, 0)
        '
        'INDtxtSubtotal
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtSubtotal, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtSubtotal, False)
        Me.INDtxtSubtotal.EnterMoveNextControl = True
        Me.INDtxtSubtotal.Location = New System.Drawing.Point(438, 199)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtSubtotal, Presentation.Controls.IndigoTextEdit.EMask.MonedaDecimales)
        Me.INDtxtSubtotal.Name = "INDtxtSubtotal"
        Me.INDtxtSubtotal.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtSubtotal.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtSubtotal.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtSubtotal.Properties.Appearance.Options.UseFont = True
        Me.INDtxtSubtotal.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtSubtotal.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtSubtotal.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtSubtotal.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtSubtotal.Properties.Mask.EditMask = "c2"
        Me.INDtxtSubtotal.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtSubtotal.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtSubtotal.Properties.ReadOnly = True
        Me.INDtxtSubtotal.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtSubtotal.StyleController = Me.INDLyProductsContract
        Me.INDtxtSubtotal.TabIndex = 8
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtSubtotal, 0)
        '
        'INDTxtMeasureUnit
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtMeasureUnit, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtMeasureUnit, False)
        Me.INDTxtMeasureUnit.EnterMoveNextControl = True
        Me.INDTxtMeasureUnit.Location = New System.Drawing.Point(24, 193)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtMeasureUnit, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtMeasureUnit.Name = "INDTxtMeasureUnit"
        Me.INDTxtMeasureUnit.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtMeasureUnit.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtMeasureUnit.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtMeasureUnit.Properties.Appearance.Options.UseFont = True
        Me.INDTxtMeasureUnit.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtMeasureUnit.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtMeasureUnit.Properties.ReadOnly = True
        Me.INDTxtMeasureUnit.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtMeasureUnit.StyleController = Me.INDLyProductsContract
        Me.INDTxtMeasureUnit.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtMeasureUnit, 0)
        '
        'INDTxtBatchSerialRemision
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtBatchSerialRemision, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtBatchSerialRemision, False)
        Me.INDTxtBatchSerialRemision.Location = New System.Drawing.Point(24, 377)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtBatchSerialRemision, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtBatchSerialRemision.Name = "INDTxtBatchSerialRemision"
        Me.INDTxtBatchSerialRemision.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtBatchSerialRemision.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtBatchSerialRemision.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtBatchSerialRemision.Properties.Appearance.Options.UseFont = True
        Me.INDTxtBatchSerialRemision.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtBatchSerialRemision.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtBatchSerialRemision.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtBatchSerialRemision.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtBatchSerialRemision.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtBatchSerialRemision.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtBatchSerialRemision.Properties.ReadOnly = True
        Me.INDTxtBatchSerialRemision.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtBatchSerialRemision.StyleController = Me.INDLyProductsContract
        Me.INDTxtBatchSerialRemision.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtBatchSerialRemision, 0)
        '
        'INDTxtTotal
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtTotal, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtTotal, False)
        Me.INDTxtTotal.EditValue = "0"
        Me.INDTxtTotal.Location = New System.Drawing.Point(438, 433)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtTotal, Presentation.Controls.IndigoTextEdit.EMask.MonedaDecimales)
        Me.INDTxtTotal.Name = "INDTxtTotal"
        Me.INDTxtTotal.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
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
        Me.INDTxtTotal.Properties.Mask.EditMask = "c2"
        Me.INDTxtTotal.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDTxtTotal.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtTotal.Properties.ReadOnly = True
        Me.INDTxtTotal.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtTotal.StyleController = Me.INDLyProductsContract
        Me.INDTxtTotal.TabIndex = 12
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtTotal, 0)
        '
        'INDPupCBatchSerial
        '
        Me.INDPupCBatchSerial.Controls.Add(Me.CtrBatchSerial1)
        Me.INDPupCBatchSerial.Location = New System.Drawing.Point(777, 245)
        Me.INDPupCBatchSerial.Name = "INDPupCBatchSerial"
        Me.INDPupCBatchSerial.Size = New System.Drawing.Size(565, 313)
        Me.INDPupCBatchSerial.TabIndex = 14
        '
        'CtrBatchSerial1
        '
        Me.CtrBatchSerial1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrBatchSerial1.Location = New System.Drawing.Point(0, 0)
        Me.CtrBatchSerial1.Name = "CtrBatchSerial1"
        Me.CtrBatchSerial1.Size = New System.Drawing.Size(565, 313)
        Me.CtrBatchSerial1.TabIndex = 0
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
        'INDTxtLastValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtLastValue, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtLastValue, True)
        Me.INDTxtLastValue.EditValue = "0"
        Me.INDTxtLastValue.EnterMoveNextControl = True
        Me.INDTxtLastValue.Location = New System.Drawing.Point(438, 79)
        Me.INDTxtLastValue.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtLastValue, Presentation.Controls.IndigoTextEdit.EMask.MonedaDecimales)
        Me.INDTxtLastValue.Name = "INDTxtLastValue"
        Me.INDTxtLastValue.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtLastValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtLastValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtLastValue.Properties.Appearance.Options.UseFont = True
        Me.INDTxtLastValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTxtLastValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTxtLastValue.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtLastValue.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtLastValue.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtLastValue.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtLastValue.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtLastValue.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtLastValue.Properties.Mask.EditMask = "c2"
        Me.INDTxtLastValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDTxtLastValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtLastValue.Properties.MaxLength = 20
        Me.INDTxtLastValue.Properties.ReadOnly = True
        Me.INDTxtLastValue.Properties.UseReadOnlyAppearance = False
        Me.INDTxtLastValue.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtLastValue.StyleController = Me.INDLyProductsContract
        Me.INDTxtLastValue.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtLastValue, 0)
        '
        'INDTxtUnitValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtUnitValue, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtUnitValue, True)
        Me.INDTxtUnitValue.EditValue = "0"
        Me.INDTxtUnitValue.EnterMoveNextControl = True
        Me.INDTxtUnitValue.Location = New System.Drawing.Point(438, 139)
        Me.INDTxtUnitValue.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtUnitValue, Presentation.Controls.IndigoTextEdit.EMask.MonedaDecimales)
        Me.INDTxtUnitValue.Name = "INDTxtUnitValue"
        Me.INDTxtUnitValue.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtUnitValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtUnitValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtUnitValue.Properties.Appearance.Options.UseFont = True
        Me.INDTxtUnitValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTxtUnitValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTxtUnitValue.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtUnitValue.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtUnitValue.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtUnitValue.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtUnitValue.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtUnitValue.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtUnitValue.Properties.Mask.EditMask = "c2"
        Me.INDTxtUnitValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDTxtUnitValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtUnitValue.Properties.MaxLength = 20
        Me.INDTxtUnitValue.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtUnitValue.StyleController = Me.INDLyProductsContract
        Me.INDTxtUnitValue.TabIndex = 7
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtUnitValue, 0)
        '
        'INDTxtUnitPurcharse
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtUnitPurcharse, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtUnitPurcharse, False)
        Me.INDTxtUnitPurcharse.EnterMoveNextControl = True
        Me.INDTxtUnitPurcharse.Location = New System.Drawing.Point(24, 139)
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
        Me.INDTxtUnitPurcharse.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtUnitPurcharse.Properties.ReadOnly = True
        Me.INDTxtUnitPurcharse.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtUnitPurcharse.StyleController = Me.INDLyProductsContract
        Me.INDTxtUnitPurcharse.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtUnitPurcharse, 0)
        '
        'INDPceProducts
        '
        Me.IndigoPopUpContainerEdit1.SetButtonMoreOptions(Me.INDPceProducts, False)
        Me.IndigoPopUpContainerEdit1.SetHostControl(Me.INDPceProducts, Nothing)
        Me.INDPceProducts.Location = New System.Drawing.Point(24, 79)
        Me.INDPceProducts.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDPceProducts.MinimumSize = New System.Drawing.Size(386, 32)
        Me.INDPceProducts.Name = "INDPceProducts"
        Me.IndigoPopUpContainerEdit1.SetOpenForm(Me.INDPceProducts, True)
        Me.IndigoPopUpContainerEdit1.SetPopUpAnimation(Me.INDPceProducts, False)
        Me.INDPceProducts.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDPceProducts.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDPceProducts.Properties.Appearance.Options.UseBackColor = True
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
        Me.INDTxtQuantity.Location = New System.Drawing.Point(24, 323)
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
        Me.INDTxtQuantity.Properties.MaxLength = 9
        Me.INDTxtQuantity.Properties.MaxValue = New Decimal(New Integer() {2147483646, 0, 0, 0})
        Me.INDTxtQuantity.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtQuantity.StyleController = Me.INDLyProductsContract
        Me.INDTxtQuantity.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtQuantity, 0)
        '
        'INDTxtIvaPercent
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtIvaPercent, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtIvaPercent, True)
        Me.INDTxtIvaPercent.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDTxtIvaPercent.EnterMoveNextControl = True
        Me.INDTxtIvaPercent.Location = New System.Drawing.Point(438, 379)
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
        Me.INDTxtIvaPercent.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.INDTxtIvaPercent.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtIvaPercent.StyleController = Me.INDLyProductsContract
        Me.INDTxtIvaPercent.TabIndex = 11
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtIvaPercent, 0)
        '
        'INDTxtDiscountPercent
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtDiscountPercent, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtDiscountPercent, True)
        Me.INDTxtDiscountPercent.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDTxtDiscountPercent.EnterMoveNextControl = True
        Me.INDTxtDiscountPercent.Location = New System.Drawing.Point(438, 259)
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
        Me.INDTxtDiscountPercent.TabIndex = 9
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtDiscountPercent, 0)
        '
        'INDPceBatchSerial
        '
        Me.IndigoPopUpContainerEdit1.SetButtonMoreOptions(Me.INDPceBatchSerial, False)
        Me.INDPceBatchSerial.EnterMoveNextControl = True
        Me.IndigoPopUpContainerEdit1.SetHostControl(Me.INDPceBatchSerial, Nothing)
        Me.INDPceBatchSerial.Location = New System.Drawing.Point(24, 253)
        Me.INDPceBatchSerial.MinimumSize = New System.Drawing.Size(386, 32)
        Me.INDPceBatchSerial.Name = "INDPceBatchSerial"
        Me.IndigoPopUpContainerEdit1.SetOpenForm(Me.INDPceBatchSerial, False)
        Me.IndigoPopUpContainerEdit1.SetPopUpAnimation(Me.INDPceBatchSerial, False)
        Me.INDPceBatchSerial.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDPceBatchSerial.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDPceBatchSerial.Properties.Appearance.Options.UseBackColor = True
        Me.INDPceBatchSerial.Properties.Appearance.Options.UseFont = True
        Me.INDPceBatchSerial.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDPceBatchSerial.Properties.PopupControl = Me.INDPupCBatchSerial
        Me.INDPceBatchSerial.Size = New System.Drawing.Size(386, 32)
        Me.INDPceBatchSerial.StyleController = Me.INDLyProductsContract
        Me.INDPceBatchSerial.TabIndex = 4
        Me.IndigoPopUpContainerEdit1.SetTagForm(Me.INDPceBatchSerial, Nothing)
        Me.IndigoPopUpContainerEdit1.SetWpfControl(Me.INDPceBatchSerial, Nothing)
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
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(914, 530)
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
        Me.INDLcgProduct.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyPceProducts, Me.INDLyTxtUnitPurcharse, Me.INDLyTxtQuantity, Me.INDLyTxtBatchSerialRemision, Me.INDLciMeasureUnit, Me.INDLyPceBatchSerial})
        Me.INDLcgProduct.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgProduct.Name = "INDLcgProduct"
        Me.INDLcgProduct.Size = New System.Drawing.Size(414, 510)
        Me.INDLcgProduct.Text = "Datos Principales"
        '
        'INDLyPceProducts
        '
        Me.INDLyPceProducts.Control = Me.INDPceProducts
        Me.INDLyPceProducts.CustomizationFormText = "LayoutControlItem1"
        Me.INDLyPceProducts.Location = New System.Drawing.Point(0, 0)
        Me.INDLyPceProducts.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLyPceProducts.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLyPceProducts.Name = "INDLyPceProducts"
        Me.INDLyPceProducts.Size = New System.Drawing.Size(390, 60)
        Me.INDLyPceProducts.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyPceProducts.Text = "Producto"
        Me.INDLyPceProducts.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyPceProducts.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyPceProducts.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLyPceProducts.TextToControlDistance = 5
        '
        'INDLyTxtUnitPurcharse
        '
        Me.INDLyTxtUnitPurcharse.Control = Me.INDTxtUnitPurcharse
        Me.INDLyTxtUnitPurcharse.CustomizationFormText = "LayoutControlItem3"
        Me.INDLyTxtUnitPurcharse.Location = New System.Drawing.Point(0, 60)
        Me.INDLyTxtUnitPurcharse.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLyTxtUnitPurcharse.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLyTxtUnitPurcharse.Name = "INDLyTxtUnitPurcharse"
        Me.INDLyTxtUnitPurcharse.Size = New System.Drawing.Size(390, 60)
        Me.INDLyTxtUnitPurcharse.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyTxtUnitPurcharse.Text = "Unidad de Empaque"
        Me.INDLyTxtUnitPurcharse.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyTxtUnitPurcharse.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyTxtUnitPurcharse.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLyTxtUnitPurcharse.TextToControlDistance = 5
        '
        'INDLyTxtQuantity
        '
        Me.INDLyTxtQuantity.Control = Me.INDTxtQuantity
        Me.INDLyTxtQuantity.CustomizationFormText = "Cantidad"
        Me.INDLyTxtQuantity.Location = New System.Drawing.Point(0, 244)
        Me.INDLyTxtQuantity.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLyTxtQuantity.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLyTxtQuantity.Name = "INDLyTxtQuantity"
        Me.INDLyTxtQuantity.Size = New System.Drawing.Size(390, 60)
        Me.INDLyTxtQuantity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyTxtQuantity.Text = "Cantidad"
        Me.INDLyTxtQuantity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyTxtQuantity.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyTxtQuantity.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLyTxtQuantity.TextToControlDistance = 5
        '
        'INDLyTxtBatchSerialRemision
        '
        Me.INDLyTxtBatchSerialRemision.Control = Me.INDTxtBatchSerialRemision
        Me.INDLyTxtBatchSerialRemision.CustomizationFormText = "Lote / Serial"
        Me.INDLyTxtBatchSerialRemision.Location = New System.Drawing.Point(0, 304)
        Me.INDLyTxtBatchSerialRemision.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLyTxtBatchSerialRemision.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLyTxtBatchSerialRemision.Name = "INDLyTxtBatchSerialRemision"
        Me.INDLyTxtBatchSerialRemision.Size = New System.Drawing.Size(390, 153)
        Me.INDLyTxtBatchSerialRemision.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyTxtBatchSerialRemision.Text = "Lote / Serial"
        Me.INDLyTxtBatchSerialRemision.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyTxtBatchSerialRemision.TextSize = New System.Drawing.Size(96, 17)
        Me.INDLyTxtBatchSerialRemision.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLyPceBatchSerial
        '
        Me.INDLyPceBatchSerial.Control = Me.INDPceBatchSerial
        Me.INDLyPceBatchSerial.CustomizationFormText = "Lote / Serial"
        Me.INDLyPceBatchSerial.Location = New System.Drawing.Point(0, 180)
        Me.INDLyPceBatchSerial.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLyPceBatchSerial.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLyPceBatchSerial.Name = "INDLyPceBatchSerial"
        Me.INDLyPceBatchSerial.Size = New System.Drawing.Size(390, 64)
        Me.INDLyPceBatchSerial.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyPceBatchSerial.Text = "Lote / Serial"
        Me.INDLyPceBatchSerial.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyPceBatchSerial.TextSize = New System.Drawing.Size(96, 17)
        '
        'INDLciMeasureUnit
        '
        Me.INDLciMeasureUnit.Control = Me.INDTxtMeasureUnit
        Me.INDLciMeasureUnit.Location = New System.Drawing.Point(0, 120)
        Me.INDLciMeasureUnit.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciMeasureUnit.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciMeasureUnit.Name = "INDLciMeasureUnit"
        Me.INDLciMeasureUnit.Size = New System.Drawing.Size(390, 60)
        Me.INDLciMeasureUnit.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciMeasureUnit.Text = "Unidad Medida"
        Me.INDLciMeasureUnit.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciMeasureUnit.TextSize = New System.Drawing.Size(96, 17)
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
        Me.INDLcgCostValue.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyTxtDiscountPercent, Me.INDLyTxtIvaPercent, Me.INDLyTxtLastValue, Me.INDLyTxtTotal, Me.INDLyTxtINDTxtUnitValue, Me.INDlyItemSubtotal, Me.INDlyItemSubtotalWithDiscount})
        Me.INDLcgCostValue.Location = New System.Drawing.Point(414, 0)
        Me.INDLcgCostValue.Name = "INDLcgCostValue"
        Me.INDLcgCostValue.Size = New System.Drawing.Size(480, 510)
        Me.INDLcgCostValue.Text = "Valor"
        '
        'INDLyTxtDiscountPercent
        '
        Me.INDLyTxtDiscountPercent.Control = Me.INDTxtDiscountPercent
        Me.INDLyTxtDiscountPercent.CustomizationFormText = "LayoutControlItem8"
        Me.INDLyTxtDiscountPercent.Location = New System.Drawing.Point(0, 180)
        Me.INDLyTxtDiscountPercent.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLyTxtDiscountPercent.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLyTxtDiscountPercent.Name = "INDLyTxtDiscountPercent"
        Me.INDLyTxtDiscountPercent.Size = New System.Drawing.Size(456, 60)
        Me.INDLyTxtDiscountPercent.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyTxtDiscountPercent.Text = "Porcentaje Descuento"
        Me.INDLyTxtDiscountPercent.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyTxtDiscountPercent.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyTxtDiscountPercent.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLyTxtDiscountPercent.TextToControlDistance = 5
        '
        'INDLyTxtIvaPercent
        '
        Me.INDLyTxtIvaPercent.Control = Me.INDTxtIvaPercent
        Me.INDLyTxtIvaPercent.CustomizationFormText = "LayoutControlItem7"
        Me.INDLyTxtIvaPercent.Location = New System.Drawing.Point(0, 300)
        Me.INDLyTxtIvaPercent.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLyTxtIvaPercent.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLyTxtIvaPercent.Name = "INDLyTxtIvaPercent"
        Me.INDLyTxtIvaPercent.Size = New System.Drawing.Size(456, 60)
        Me.INDLyTxtIvaPercent.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyTxtIvaPercent.Text = "Porcentaje IVA"
        Me.INDLyTxtIvaPercent.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyTxtIvaPercent.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyTxtIvaPercent.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLyTxtIvaPercent.TextToControlDistance = 5
        '
        'INDLyTxtLastValue
        '
        Me.INDLyTxtLastValue.Control = Me.INDTxtLastValue
        Me.INDLyTxtLastValue.CustomizationFormText = "LayoutControlItem6"
        Me.INDLyTxtLastValue.Location = New System.Drawing.Point(0, 0)
        Me.INDLyTxtLastValue.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLyTxtLastValue.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLyTxtLastValue.Name = "INDLyTxtLastValue"
        Me.INDLyTxtLastValue.Size = New System.Drawing.Size(456, 60)
        Me.INDLyTxtLastValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyTxtLastValue.Text = "Ultimo costo"
        Me.INDLyTxtLastValue.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyTxtLastValue.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyTxtLastValue.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLyTxtLastValue.TextToControlDistance = 5
        '
        'INDLyTxtTotal
        '
        Me.INDLyTxtTotal.Control = Me.INDTxtTotal
        Me.INDLyTxtTotal.CustomizationFormText = "Total"
        Me.INDLyTxtTotal.Location = New System.Drawing.Point(0, 360)
        Me.INDLyTxtTotal.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLyTxtTotal.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLyTxtTotal.Name = "INDLyTxtTotal"
        Me.INDLyTxtTotal.Size = New System.Drawing.Size(456, 97)
        Me.INDLyTxtTotal.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyTxtTotal.Text = "Total"
        Me.INDLyTxtTotal.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyTxtTotal.TextSize = New System.Drawing.Size(96, 17)
        '
        'INDLyTxtINDTxtUnitValue
        '
        Me.INDLyTxtINDTxtUnitValue.Control = Me.INDTxtUnitValue
        Me.INDLyTxtINDTxtUnitValue.CustomizationFormText = "LayoutControlItem4"
        Me.INDLyTxtINDTxtUnitValue.Location = New System.Drawing.Point(0, 60)
        Me.INDLyTxtINDTxtUnitValue.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLyTxtINDTxtUnitValue.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLyTxtINDTxtUnitValue.Name = "INDLyTxtINDTxtUnitValue"
        Me.INDLyTxtINDTxtUnitValue.Size = New System.Drawing.Size(456, 60)
        Me.INDLyTxtINDTxtUnitValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyTxtINDTxtUnitValue.Text = "Valor Unitario"
        Me.INDLyTxtINDTxtUnitValue.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyTxtINDTxtUnitValue.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyTxtINDTxtUnitValue.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLyTxtINDTxtUnitValue.TextToControlDistance = 5
        '
        'INDlyItemSubtotal
        '
        Me.INDlyItemSubtotal.Control = Me.INDtxtSubtotal
        Me.INDlyItemSubtotal.Location = New System.Drawing.Point(0, 120)
        Me.INDlyItemSubtotal.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemSubtotal.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemSubtotal.Name = "INDlyItemSubtotal"
        Me.INDlyItemSubtotal.Size = New System.Drawing.Size(456, 60)
        Me.INDlyItemSubtotal.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemSubtotal.Text = "Subtotal"
        Me.INDlyItemSubtotal.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemSubtotal.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemSubtotal.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemSubtotal.TextToControlDistance = 5
        '
        'INDlyItemSubtotalWithDiscount
        '
        Me.INDlyItemSubtotalWithDiscount.Control = Me.INDtxtSubtotalWithDiscount
        Me.INDlyItemSubtotalWithDiscount.Location = New System.Drawing.Point(0, 240)
        Me.INDlyItemSubtotalWithDiscount.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemSubtotalWithDiscount.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemSubtotalWithDiscount.Name = "INDlyItemSubtotalWithDiscount"
        Me.INDlyItemSubtotalWithDiscount.Size = New System.Drawing.Size(456, 60)
        Me.INDlyItemSubtotalWithDiscount.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemSubtotalWithDiscount.Text = "Subtotal con Descuento"
        Me.INDlyItemSubtotalWithDiscount.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemSubtotalWithDiscount.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemSubtotalWithDiscount.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemSubtotalWithDiscount.TextToControlDistance = 5
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.INDBtnAddProduct)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(202, 536)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PanelControl1.MaximumSize = New System.Drawing.Size(0, 36)
        Me.PanelControl1.MinimumSize = New System.Drawing.Size(0, 36)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(914, 36)
        Me.PanelControl1.TabIndex = 1
        '
        'INDBtnAddProduct
        '
        Me.INDBtnAddProduct.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDBtnAddProduct.Location = New System.Drawing.Point(2, 2)
        Me.INDBtnAddProduct.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDBtnAddProduct.Name = "INDBtnAddProduct"
        Me.INDBtnAddProduct.Size = New System.Drawing.Size(910, 32)
        Me.INDBtnAddProduct.TabIndex = 0
        Me.INDBtnAddProduct.Text = "Agregar"
        '
        'PopUpProductsEntranceVoucher
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1118, 708)
        Me.IconOptions.ShowIcon = False
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "PopUpProductsEntranceVoucher"
        Me.Opacity = 1.0R
        Me.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.Text = "Agregar Producto"
        Me.ViewModeEditHold = True
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyProductsContract, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLyProductsContract.ResumeLayout(False)
        CType(Me.INDtxtSubtotalWithDiscount.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtSubtotal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtMeasureUnit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtBatchSerialRemision.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtTotal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPupCBatchSerial, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPupCBatchSerial.ResumeLayout(False)
        CType(Me.INDPupCProducto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPupCProducto.ResumeLayout(False)
        CType(Me.INDTxtLastValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtUnitValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtUnitPurcharse.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPceProducts.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtQuantity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtIvaPercent.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtDiscountPercent.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPceBatchSerial.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgProduct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyPceProducts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyTxtUnitPurcharse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyTxtQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyTxtBatchSerialRemision, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyPceBatchSerial, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciMeasureUnit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgCostValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyTxtDiscountPercent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyTxtIvaPercent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyTxtLastValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyTxtTotal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyTxtINDTxtUnitValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemSubtotal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemSubtotalWithDiscount, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents INDTxtUnitValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtUnitPurcharse As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLyTxtUnitPurcharse As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyTxtINDTxtUnitValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDTxtLastValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLyTxtQuantity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyTxtLastValue As DevExpress.XtraLayout.LayoutControlItem
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
    Friend WithEvents INDLyPceBatchSerial As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDPceBatchSerial As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents INDPupCBatchSerial As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents CtrBatchSerial1 As Presentation.Inventory.CtrBatchSerial
    Friend WithEvents INDTxtTotal As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLyTxtTotal As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDTxtBatchSerialRemision As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLyTxtBatchSerialRemision As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDTxtMeasureUnit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciMeasureUnit As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtSubtotalWithDiscount As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDtxtSubtotal As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemSubtotal As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemSubtotalWithDiscount As DevExpress.XtraLayout.LayoutControlItem
End Class
