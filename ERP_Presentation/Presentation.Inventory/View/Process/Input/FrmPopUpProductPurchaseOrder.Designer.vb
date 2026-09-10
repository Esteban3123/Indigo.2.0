Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPopUpProductPurchaseOrder
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
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDTxtSubTotal = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtForeignCurrencyValue = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtProductMeasure = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtProductUnit = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtProductPresentation = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtProductName = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtValuePesos = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtUnitValue = New DevExpress.XtraEditors.TextEdit()
        Me.INDSpnQuantity = New DevExpress.XtraEditors.SpinEdit()
        Me.INDSleProduct = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDMmoDetail = New DevExpress.XtraEditors.MemoEdit()
        Me.INDSpnPercentDiscount = New DevExpress.XtraEditors.SpinEdit()
        Me.INDSpnVatPercent = New DevExpress.XtraEditors.SpinEdit()
        Me.INDlyPopupProductPurchaseOrder = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyGpPurchaseOrderProduct = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemSleProduct = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemMmoDetail = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemTxtProductName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemTxtProductPresentation = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemTxtProductUnit = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemTxtProductMeasure = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyGpPurchaseOrderDetail = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemTxtValuePesos = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemTxtForeignCurrencyValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemTxtSubTotal = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemSpnPercentDiscount = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemSpnVatPercent = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemTxtUnitValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemSpnQuantity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl()
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup()
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl()
        Me.INDPctrAceptar = New DevExpress.XtraEditors.PanelControl()
        Me.INDbtnAceptar = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDTxtSubTotal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtForeignCurrencyValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtProductMeasure.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtProductUnit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtProductPresentation.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtProductName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtValuePesos.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtUnitValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSpnQuantity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleProduct.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDMmoDetail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSpnPercentDiscount.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSpnVatPercent.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyPopupProductPurchaseOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyGpPurchaseOrderProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemSleProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemMmoDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemTxtProductName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemTxtProductPresentation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemTxtProductUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemTxtProductMeasure, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyGpPurchaseOrderDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemTxtValuePesos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemTxtForeignCurrencyValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemTxtSubTotal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemSpnPercentDiscount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemSpnVatPercent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemTxtUnitValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemSpnQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPctrAceptar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPctrAceptar.SuspendLayout()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.LayoutControl1)
        Me.INDPanelControlBase.Controls.Add(Me.INDPctrAceptar)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 117)
        Me.INDPanelControlBase.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDPanelControlBase.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1154, 433)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Location = New System.Drawing.Point(0, 4)
        Me.ToolBars.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ToolBars.Size = New System.Drawing.Size(1154, 94)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1154, 94)
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.LayoutControl1
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 6)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 425)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDTxtSubTotal)
        Me.LayoutControl1.Controls.Add(Me.INDTxtForeignCurrencyValue)
        Me.LayoutControl1.Controls.Add(Me.INDTxtProductMeasure)
        Me.LayoutControl1.Controls.Add(Me.INDTxtProductUnit)
        Me.LayoutControl1.Controls.Add(Me.INDTxtProductPresentation)
        Me.LayoutControl1.Controls.Add(Me.INDTxtProductName)
        Me.LayoutControl1.Controls.Add(Me.INDTxtValuePesos)
        Me.LayoutControl1.Controls.Add(Me.INDTxtUnitValue)
        Me.LayoutControl1.Controls.Add(Me.INDSpnQuantity)
        Me.LayoutControl1.Controls.Add(Me.INDSleProduct)
        Me.LayoutControl1.Controls.Add(Me.INDMmoDetail)
        Me.LayoutControl1.Controls.Add(Me.INDSpnPercentDiscount)
        Me.LayoutControl1.Controls.Add(Me.INDSpnVatPercent)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(202, 6)
        Me.LayoutControl1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.INDlyPopupProductPurchaseOrder
        Me.LayoutControl1.Size = New System.Drawing.Size(950, 396)
        Me.LayoutControl1.TabIndex = 1
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDTxtSubTotal
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtSubTotal, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtSubTotal, False)
        Me.INDTxtSubTotal.Location = New System.Drawing.Point(560, 203)
        Me.INDTxtSubTotal.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtSubTotal, Presentation.Controls.IndigoTextEdit.EMask.Moneda)
        Me.INDTxtSubTotal.Name = "INDTxtSubTotal"
        Me.INDTxtSubTotal.Properties.Appearance.BackColor = System.Drawing.Color.White
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
        Me.INDTxtSubTotal.Properties.Mask.EditMask = "c0"
        Me.INDTxtSubTotal.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDTxtSubTotal.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtSubTotal.Size = New System.Drawing.Size(264, 28)
        Me.INDTxtSubTotal.StyleController = Me.LayoutControl1
        Me.INDTxtSubTotal.TabIndex = 14
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtSubTotal, 0)
        '
        'INDTxtForeignCurrencyValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtForeignCurrencyValue, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtForeignCurrencyValue, False)
        Me.INDTxtForeignCurrencyValue.Location = New System.Drawing.Point(560, 167)
        Me.INDTxtForeignCurrencyValue.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtForeignCurrencyValue, Presentation.Controls.IndigoTextEdit.EMask.Moneda)
        Me.INDTxtForeignCurrencyValue.Name = "INDTxtForeignCurrencyValue"
        Me.INDTxtForeignCurrencyValue.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDTxtForeignCurrencyValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtForeignCurrencyValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtForeignCurrencyValue.Properties.Appearance.Options.UseFont = True
        Me.INDTxtForeignCurrencyValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTxtForeignCurrencyValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTxtForeignCurrencyValue.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtForeignCurrencyValue.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtForeignCurrencyValue.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtForeignCurrencyValue.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtForeignCurrencyValue.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtForeignCurrencyValue.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtForeignCurrencyValue.Properties.Mask.EditMask = "c0"
        Me.INDTxtForeignCurrencyValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDTxtForeignCurrencyValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtForeignCurrencyValue.Size = New System.Drawing.Size(264, 28)
        Me.INDTxtForeignCurrencyValue.StyleController = Me.LayoutControl1
        Me.INDTxtForeignCurrencyValue.TabIndex = 13
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtForeignCurrencyValue, 0)
        '
        'INDTxtProductMeasure
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtProductMeasure, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtProductMeasure, False)
        Me.INDTxtProductMeasure.Location = New System.Drawing.Point(146, 203)
        Me.INDTxtProductMeasure.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtProductMeasure, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtProductMeasure.Name = "INDTxtProductMeasure"
        Me.INDTxtProductMeasure.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDTxtProductMeasure.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtProductMeasure.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtProductMeasure.Properties.Appearance.Options.UseFont = True
        Me.INDTxtProductMeasure.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtProductMeasure.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtProductMeasure.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtProductMeasure.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtProductMeasure.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtProductMeasure.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtProductMeasure.Size = New System.Drawing.Size(264, 28)
        Me.INDTxtProductMeasure.StyleController = Me.LayoutControl1
        Me.INDTxtProductMeasure.TabIndex = 12
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtProductMeasure, 0)
        '
        'INDTxtProductUnit
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtProductUnit, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtProductUnit, False)
        Me.INDTxtProductUnit.Location = New System.Drawing.Point(146, 167)
        Me.INDTxtProductUnit.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtProductUnit, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtProductUnit.Name = "INDTxtProductUnit"
        Me.INDTxtProductUnit.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDTxtProductUnit.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtProductUnit.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtProductUnit.Properties.Appearance.Options.UseFont = True
        Me.INDTxtProductUnit.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtProductUnit.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtProductUnit.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtProductUnit.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtProductUnit.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtProductUnit.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtProductUnit.Size = New System.Drawing.Size(264, 28)
        Me.INDTxtProductUnit.StyleController = Me.LayoutControl1
        Me.INDTxtProductUnit.TabIndex = 11
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtProductUnit, 0)
        '
        'INDTxtProductPresentation
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtProductPresentation, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtProductPresentation, False)
        Me.INDTxtProductPresentation.Location = New System.Drawing.Point(146, 131)
        Me.INDTxtProductPresentation.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtProductPresentation, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtProductPresentation.Name = "INDTxtProductPresentation"
        Me.INDTxtProductPresentation.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDTxtProductPresentation.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtProductPresentation.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtProductPresentation.Properties.Appearance.Options.UseFont = True
        Me.INDTxtProductPresentation.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtProductPresentation.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtProductPresentation.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtProductPresentation.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtProductPresentation.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtProductPresentation.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtProductPresentation.Size = New System.Drawing.Size(264, 28)
        Me.INDTxtProductPresentation.StyleController = Me.LayoutControl1
        Me.INDTxtProductPresentation.TabIndex = 10
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtProductPresentation, 0)
        '
        'INDTxtProductName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtProductName, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtProductName, False)
        Me.INDTxtProductName.Location = New System.Drawing.Point(146, 95)
        Me.INDTxtProductName.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtProductName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtProductName.Name = "INDTxtProductName"
        Me.INDTxtProductName.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDTxtProductName.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtProductName.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtProductName.Properties.Appearance.Options.UseFont = True
        Me.INDTxtProductName.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtProductName.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtProductName.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtProductName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtProductName.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtProductName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtProductName.Size = New System.Drawing.Size(264, 28)
        Me.INDTxtProductName.StyleController = Me.LayoutControl1
        Me.INDTxtProductName.TabIndex = 9
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtProductName, 0)
        '
        'INDTxtValuePesos
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtValuePesos, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtValuePesos, False)
        Me.INDTxtValuePesos.Location = New System.Drawing.Point(560, 131)
        Me.INDTxtValuePesos.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtValuePesos, Presentation.Controls.IndigoTextEdit.EMask.Moneda)
        Me.INDTxtValuePesos.Name = "INDTxtValuePesos"
        Me.INDTxtValuePesos.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDTxtValuePesos.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtValuePesos.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtValuePesos.Properties.Appearance.Options.UseFont = True
        Me.INDTxtValuePesos.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTxtValuePesos.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTxtValuePesos.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtValuePesos.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtValuePesos.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtValuePesos.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtValuePesos.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtValuePesos.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtValuePesos.Properties.Mask.EditMask = "c0"
        Me.INDTxtValuePesos.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDTxtValuePesos.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtValuePesos.Size = New System.Drawing.Size(264, 28)
        Me.INDTxtValuePesos.StyleController = Me.LayoutControl1
        Me.INDTxtValuePesos.TabIndex = 8
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtValuePesos, 0)
        '
        'INDTxtUnitValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtUnitValue, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtUnitValue, False)
        Me.INDTxtUnitValue.Location = New System.Drawing.Point(560, 59)
        Me.INDTxtUnitValue.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtUnitValue, Presentation.Controls.IndigoTextEdit.EMask.Moneda)
        Me.INDTxtUnitValue.Name = "INDTxtUnitValue"
        Me.INDTxtUnitValue.Properties.Appearance.BackColor = System.Drawing.Color.White
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
        Me.INDTxtUnitValue.Properties.Mask.EditMask = "c0"
        Me.INDTxtUnitValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDTxtUnitValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtUnitValue.Size = New System.Drawing.Size(264, 28)
        Me.INDTxtUnitValue.StyleController = Me.LayoutControl1
        Me.INDTxtUnitValue.TabIndex = 7
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtUnitValue, 0)
        '
        'INDSpnQuantity
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSpnQuantity, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSpnQuantity, False)
        Me.INDSpnQuantity.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDSpnQuantity.Location = New System.Drawing.Point(560, 95)
        Me.INDSpnQuantity.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDSpnQuantity, Presentation.Controls.IndigoTextEdit.EMask.Numerico)
        Me.INDSpnQuantity.Name = "INDSpnQuantity"
        Me.INDSpnQuantity.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDSpnQuantity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSpnQuantity.Properties.Appearance.Options.UseBackColor = True
        Me.INDSpnQuantity.Properties.Appearance.Options.UseFont = True
        Me.INDSpnQuantity.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSpnQuantity.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSpnQuantity.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSpnQuantity.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSpnQuantity.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSpnQuantity.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSpnQuantity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSpnQuantity.Properties.Mask.EditMask = "[0-9]+"
        Me.INDSpnQuantity.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDSpnQuantity.Size = New System.Drawing.Size(264, 28)
        Me.INDSpnQuantity.StyleController = Me.LayoutControl1
        Me.INDSpnQuantity.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSpnQuantity, 0)
        '
        'INDSleProduct
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleProduct, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleProduct, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleProduct, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleProduct, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleProduct, False)
        Me.INDSleProduct.Location = New System.Drawing.Point(146, 59)
        Me.INDSleProduct.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleProduct, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleProduct.Name = "INDSleProduct"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleProduct, True)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleProduct, False)
        Me.INDSleProduct.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDSleProduct.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleProduct.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleProduct.Properties.Appearance.Options.UseFont = True
        Me.INDSleProduct.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleProduct.Properties.NullText = ""
        Me.INDSleProduct.Properties.PopupSizeable = False
        Me.INDSleProduct.Properties.ShowFooter = False
        Me.INDSleProduct.Properties.View = Me.SearchLookUpEdit1View
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleProduct, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleProduct, True)
        Me.INDSleProduct.Size = New System.Drawing.Size(264, 28)
        Me.INDSleProduct.StyleController = Me.LayoutControl1
        Me.INDSleProduct.TabIndex = 4
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleProduct, "304")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleProduct, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleProduct, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleProduct, False)
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        '
        'INDMmoDetail
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDMmoDetail, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDMmoDetail, False)
        Me.INDMmoDetail.Location = New System.Drawing.Point(146, 239)
        Me.INDMmoDetail.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDMmoDetail, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDMmoDetail.Name = "INDMmoDetail"
        Me.INDMmoDetail.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDMmoDetail.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDMmoDetail.Properties.Appearance.Options.UseBackColor = True
        Me.INDMmoDetail.Properties.Appearance.Options.UseFont = True
        Me.INDMmoDetail.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDMmoDetail.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDMmoDetail.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDMmoDetail.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDMmoDetail.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDMmoDetail.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDMmoDetail.Size = New System.Drawing.Size(264, 68)
        Me.INDMmoDetail.StyleController = Me.LayoutControl1
        Me.INDMmoDetail.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDMmoDetail, 0)
        '
        'INDSpnPercentDiscount
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSpnPercentDiscount, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSpnPercentDiscount, False)
        Me.INDSpnPercentDiscount.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDSpnPercentDiscount.Location = New System.Drawing.Point(560, 239)
        Me.INDSpnPercentDiscount.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDSpnPercentDiscount, Presentation.Controls.IndigoTextEdit.EMask.Porcentaje)
        Me.INDSpnPercentDiscount.Name = "INDSpnPercentDiscount"
        Me.INDSpnPercentDiscount.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDSpnPercentDiscount.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSpnPercentDiscount.Properties.Appearance.Options.UseBackColor = True
        Me.INDSpnPercentDiscount.Properties.Appearance.Options.UseFont = True
        Me.INDSpnPercentDiscount.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSpnPercentDiscount.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSpnPercentDiscount.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSpnPercentDiscount.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSpnPercentDiscount.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSpnPercentDiscount.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSpnPercentDiscount.Properties.AutoHeight = False
        Me.INDSpnPercentDiscount.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSpnPercentDiscount.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.INDSpnPercentDiscount.Properties.Mask.EditMask = "P"
        Me.INDSpnPercentDiscount.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDSpnPercentDiscount.Size = New System.Drawing.Size(264, 32)
        Me.INDSpnPercentDiscount.StyleController = Me.LayoutControl1
        Me.INDSpnPercentDiscount.TabIndex = 15
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSpnPercentDiscount, 0)
        '
        'INDSpnVatPercent
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSpnVatPercent, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSpnVatPercent, False)
        Me.INDSpnVatPercent.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDSpnVatPercent.Location = New System.Drawing.Point(560, 275)
        Me.INDSpnVatPercent.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDSpnVatPercent, Presentation.Controls.IndigoTextEdit.EMask.Porcentaje)
        Me.INDSpnVatPercent.Name = "INDSpnVatPercent"
        Me.INDSpnVatPercent.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDSpnVatPercent.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSpnVatPercent.Properties.Appearance.Options.UseBackColor = True
        Me.INDSpnVatPercent.Properties.Appearance.Options.UseFont = True
        Me.INDSpnVatPercent.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSpnVatPercent.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSpnVatPercent.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSpnVatPercent.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSpnVatPercent.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSpnVatPercent.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSpnVatPercent.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSpnVatPercent.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.INDSpnVatPercent.Properties.Mask.EditMask = "P"
        Me.INDSpnVatPercent.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDSpnVatPercent.Size = New System.Drawing.Size(264, 28)
        Me.INDSpnVatPercent.StyleController = Me.LayoutControl1
        Me.INDSpnVatPercent.TabIndex = 16
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSpnVatPercent, 0)
        '
        'INDlyPopupProductPurchaseOrder
        '
        Me.INDlyPopupProductPurchaseOrder.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlyPopupProductPurchaseOrder.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlyPopupProductPurchaseOrder.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyPopupProductPurchaseOrder.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyPopupProductPurchaseOrder.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyPopupProductPurchaseOrder.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyPopupProductPurchaseOrder.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlyPopupProductPurchaseOrder.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlyPopupProductPurchaseOrder.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyPopupProductPurchaseOrder.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyPopupProductPurchaseOrder.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlyPopupProductPurchaseOrder.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlyPopupProductPurchaseOrder.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyPopupProductPurchaseOrder.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyPopupProductPurchaseOrder, False)
        Me.INDlyPopupProductPurchaseOrder.CustomizationFormText = "INDlyPopupProductPurchaseOrder"
        Me.INDlyPopupProductPurchaseOrder.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlyPopupProductPurchaseOrder.GroupBordersVisible = False
        Me.INDlyPopupProductPurchaseOrder.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyGpPurchaseOrderProduct, Me.INDLyGpPurchaseOrderDetail})
        Me.INDlyPopupProductPurchaseOrder.Location = New System.Drawing.Point(0, 0)
        Me.INDlyPopupProductPurchaseOrder.Name = "INDlyPopupProductPurchaseOrder"
        Me.INDlyPopupProductPurchaseOrder.Size = New System.Drawing.Size(950, 396)
        Me.INDlyPopupProductPurchaseOrder.TextVisible = False
        '
        'INDLyGpPurchaseOrderProduct
        '
        Me.INDLyGpPurchaseOrderProduct.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDLyGpPurchaseOrderProduct.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDLyGpPurchaseOrderProduct.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLyGpPurchaseOrderProduct.AppearanceItemCaption.Options.UseFont = True
        Me.INDLyGpPurchaseOrderProduct.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLyGpPurchaseOrderProduct.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLyGpPurchaseOrderProduct.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDLyGpPurchaseOrderProduct.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDLyGpPurchaseOrderProduct.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLyGpPurchaseOrderProduct.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLyGpPurchaseOrderProduct.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDLyGpPurchaseOrderProduct.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDLyGpPurchaseOrderProduct.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLyGpPurchaseOrderProduct.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLyGpPurchaseOrderProduct, False)
        Me.INDLyGpPurchaseOrderProduct.CustomizationFormText = "LayoutControlGroup1"
        Me.INDLyGpPurchaseOrderProduct.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemSleProduct, Me.INDlyItemMmoDetail, Me.INDlyItemTxtProductName, Me.INDlyItemTxtProductPresentation, Me.INDlyItemTxtProductUnit, Me.INDlyItemTxtProductMeasure})
        Me.INDLyGpPurchaseOrderProduct.Location = New System.Drawing.Point(0, 0)
        Me.INDLyGpPurchaseOrderProduct.Name = "INDLyGpPurchaseOrderProduct"
        Me.INDLyGpPurchaseOrderProduct.Size = New System.Drawing.Size(414, 376)
        Me.INDLyGpPurchaseOrderProduct.Text = "Producto de la Orden de Compra"
        '
        'INDlyItemSleProduct
        '
        Me.INDlyItemSleProduct.Control = Me.INDSleProduct
        Me.INDlyItemSleProduct.CustomizationFormText = "LayoutControlItem1"
        Me.INDlyItemSleProduct.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemSleProduct.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemSleProduct.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemSleProduct.Name = "INDlyItemSleProduct"
        Me.INDlyItemSleProduct.Size = New System.Drawing.Size(390, 36)
        Me.INDlyItemSleProduct.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemSleProduct.Text = "Producto"
        Me.INDlyItemSleProduct.TextSize = New System.Drawing.Size(119, 21)
        '
        'INDlyItemMmoDetail
        '
        Me.INDlyItemMmoDetail.Control = Me.INDMmoDetail
        Me.INDlyItemMmoDetail.CustomizationFormText = "LayoutControlItem2"
        Me.INDlyItemMmoDetail.Location = New System.Drawing.Point(0, 180)
        Me.INDlyItemMmoDetail.MaxSize = New System.Drawing.Size(390, 72)
        Me.INDlyItemMmoDetail.MinSize = New System.Drawing.Size(390, 72)
        Me.INDlyItemMmoDetail.Name = "INDlyItemMmoDetail"
        Me.INDlyItemMmoDetail.Size = New System.Drawing.Size(390, 137)
        Me.INDlyItemMmoDetail.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemMmoDetail.Text = "Detalle"
        Me.INDlyItemMmoDetail.TextSize = New System.Drawing.Size(119, 21)
        '
        'INDlyItemTxtProductName
        '
        Me.INDlyItemTxtProductName.Control = Me.INDTxtProductName
        Me.INDlyItemTxtProductName.CustomizationFormText = "LayoutControlItem6"
        Me.INDlyItemTxtProductName.Location = New System.Drawing.Point(0, 36)
        Me.INDlyItemTxtProductName.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemTxtProductName.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemTxtProductName.Name = "INDlyItemTxtProductName"
        Me.INDlyItemTxtProductName.Size = New System.Drawing.Size(390, 36)
        Me.INDlyItemTxtProductName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemTxtProductName.Text = "Nombre"
        Me.INDlyItemTxtProductName.TextSize = New System.Drawing.Size(119, 21)
        '
        'INDlyItemTxtProductPresentation
        '
        Me.INDlyItemTxtProductPresentation.Control = Me.INDTxtProductPresentation
        Me.INDlyItemTxtProductPresentation.CustomizationFormText = "LayoutControlItem7"
        Me.INDlyItemTxtProductPresentation.Location = New System.Drawing.Point(0, 72)
        Me.INDlyItemTxtProductPresentation.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemTxtProductPresentation.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemTxtProductPresentation.Name = "INDlyItemTxtProductPresentation"
        Me.INDlyItemTxtProductPresentation.Size = New System.Drawing.Size(390, 36)
        Me.INDlyItemTxtProductPresentation.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemTxtProductPresentation.Text = "Presentacion"
        Me.INDlyItemTxtProductPresentation.TextSize = New System.Drawing.Size(119, 21)
        '
        'INDlyItemTxtProductUnit
        '
        Me.INDlyItemTxtProductUnit.Control = Me.INDTxtProductUnit
        Me.INDlyItemTxtProductUnit.CustomizationFormText = "LayoutControlItem8"
        Me.INDlyItemTxtProductUnit.Location = New System.Drawing.Point(0, 108)
        Me.INDlyItemTxtProductUnit.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemTxtProductUnit.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemTxtProductUnit.Name = "INDlyItemTxtProductUnit"
        Me.INDlyItemTxtProductUnit.Size = New System.Drawing.Size(390, 36)
        Me.INDlyItemTxtProductUnit.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemTxtProductUnit.Text = "Unidad"
        Me.INDlyItemTxtProductUnit.TextSize = New System.Drawing.Size(119, 21)
        '
        'INDlyItemTxtProductMeasure
        '
        Me.INDlyItemTxtProductMeasure.Control = Me.INDTxtProductMeasure
        Me.INDlyItemTxtProductMeasure.CustomizationFormText = "LayoutControlItem9"
        Me.INDlyItemTxtProductMeasure.Location = New System.Drawing.Point(0, 144)
        Me.INDlyItemTxtProductMeasure.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemTxtProductMeasure.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemTxtProductMeasure.Name = "INDlyItemTxtProductMeasure"
        Me.INDlyItemTxtProductMeasure.Size = New System.Drawing.Size(390, 36)
        Me.INDlyItemTxtProductMeasure.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemTxtProductMeasure.Text = "Medida"
        Me.INDlyItemTxtProductMeasure.TextSize = New System.Drawing.Size(119, 21)
        '
        'INDLyGpPurchaseOrderDetail
        '
        Me.INDLyGpPurchaseOrderDetail.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDLyGpPurchaseOrderDetail.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDLyGpPurchaseOrderDetail.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLyGpPurchaseOrderDetail.AppearanceItemCaption.Options.UseFont = True
        Me.INDLyGpPurchaseOrderDetail.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLyGpPurchaseOrderDetail.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLyGpPurchaseOrderDetail.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDLyGpPurchaseOrderDetail.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDLyGpPurchaseOrderDetail.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLyGpPurchaseOrderDetail.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLyGpPurchaseOrderDetail.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDLyGpPurchaseOrderDetail.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDLyGpPurchaseOrderDetail.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLyGpPurchaseOrderDetail.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLyGpPurchaseOrderDetail, False)
        Me.INDLyGpPurchaseOrderDetail.CustomizationFormText = "Detalle Compra"
        Me.INDLyGpPurchaseOrderDetail.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemTxtValuePesos, Me.INDlyItemTxtForeignCurrencyValue, Me.INDlyItemTxtSubTotal, Me.INDlyItemSpnPercentDiscount, Me.INDlyItemSpnVatPercent, Me.INDlyItemTxtUnitValue, Me.INDlyItemSpnQuantity})
        Me.INDLyGpPurchaseOrderDetail.Location = New System.Drawing.Point(414, 0)
        Me.INDLyGpPurchaseOrderDetail.Name = "INDLyGpPurchaseOrderDetail"
        Me.INDLyGpPurchaseOrderDetail.Size = New System.Drawing.Size(516, 376)
        Me.INDLyGpPurchaseOrderDetail.Text = "Detalle Orden de Compra"
        '
        'INDlyItemTxtValuePesos
        '
        Me.INDlyItemTxtValuePesos.Control = Me.INDTxtValuePesos
        Me.INDlyItemTxtValuePesos.CustomizationFormText = "Valor Pesos"
        Me.INDlyItemTxtValuePesos.Location = New System.Drawing.Point(0, 72)
        Me.INDlyItemTxtValuePesos.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemTxtValuePesos.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemTxtValuePesos.Name = "INDlyItemTxtValuePesos"
        Me.INDlyItemTxtValuePesos.Size = New System.Drawing.Size(492, 36)
        Me.INDlyItemTxtValuePesos.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemTxtValuePesos.Text = "Valor Pesos"
        Me.INDlyItemTxtValuePesos.TextSize = New System.Drawing.Size(119, 21)
        '
        'INDlyItemTxtForeignCurrencyValue
        '
        Me.INDlyItemTxtForeignCurrencyValue.Control = Me.INDTxtForeignCurrencyValue
        Me.INDlyItemTxtForeignCurrencyValue.CustomizationFormText = "LayoutControlItem10"
        Me.INDlyItemTxtForeignCurrencyValue.Location = New System.Drawing.Point(0, 108)
        Me.INDlyItemTxtForeignCurrencyValue.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemTxtForeignCurrencyValue.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemTxtForeignCurrencyValue.Name = "INDlyItemTxtForeignCurrencyValue"
        Me.INDlyItemTxtForeignCurrencyValue.Size = New System.Drawing.Size(492, 36)
        Me.INDlyItemTxtForeignCurrencyValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemTxtForeignCurrencyValue.Text = "Valor Moneda Ext"
        Me.INDlyItemTxtForeignCurrencyValue.TextSize = New System.Drawing.Size(119, 21)
        '
        'INDlyItemTxtSubTotal
        '
        Me.INDlyItemTxtSubTotal.Control = Me.INDTxtSubTotal
        Me.INDlyItemTxtSubTotal.CustomizationFormText = "SubTotal"
        Me.INDlyItemTxtSubTotal.Location = New System.Drawing.Point(0, 144)
        Me.INDlyItemTxtSubTotal.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemTxtSubTotal.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemTxtSubTotal.Name = "INDlyItemTxtSubTotal"
        Me.INDlyItemTxtSubTotal.Size = New System.Drawing.Size(492, 36)
        Me.INDlyItemTxtSubTotal.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemTxtSubTotal.Text = "SubTotal"
        Me.INDlyItemTxtSubTotal.TextSize = New System.Drawing.Size(119, 21)
        '
        'INDlyItemSpnPercentDiscount
        '
        Me.INDlyItemSpnPercentDiscount.Control = Me.INDSpnPercentDiscount
        Me.INDlyItemSpnPercentDiscount.CustomizationFormText = "% Descuento"
        Me.INDlyItemSpnPercentDiscount.Location = New System.Drawing.Point(0, 180)
        Me.INDlyItemSpnPercentDiscount.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemSpnPercentDiscount.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemSpnPercentDiscount.Name = "INDlyItemSpnPercentDiscount"
        Me.INDlyItemSpnPercentDiscount.Size = New System.Drawing.Size(492, 36)
        Me.INDlyItemSpnPercentDiscount.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemSpnPercentDiscount.Text = "% Descuento"
        Me.INDlyItemSpnPercentDiscount.TextSize = New System.Drawing.Size(119, 21)
        '
        'INDlyItemSpnVatPercent
        '
        Me.INDlyItemSpnVatPercent.Control = Me.INDSpnVatPercent
        Me.INDlyItemSpnVatPercent.CustomizationFormText = "% IVA"
        Me.INDlyItemSpnVatPercent.Location = New System.Drawing.Point(0, 216)
        Me.INDlyItemSpnVatPercent.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemSpnVatPercent.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemSpnVatPercent.Name = "INDlyItemSpnVatPercent"
        Me.INDlyItemSpnVatPercent.Size = New System.Drawing.Size(492, 101)
        Me.INDlyItemSpnVatPercent.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemSpnVatPercent.Text = "% IVA"
        Me.INDlyItemSpnVatPercent.TextSize = New System.Drawing.Size(119, 21)
        '
        'INDlyItemTxtUnitValue
        '
        Me.INDlyItemTxtUnitValue.Control = Me.INDTxtUnitValue
        Me.INDlyItemTxtUnitValue.CustomizationFormText = "LayoutControlItem4"
        Me.INDlyItemTxtUnitValue.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemTxtUnitValue.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemTxtUnitValue.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemTxtUnitValue.Name = "INDlyItemTxtUnitValue"
        Me.INDlyItemTxtUnitValue.Size = New System.Drawing.Size(492, 36)
        Me.INDlyItemTxtUnitValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemTxtUnitValue.Text = "Valor Unitario"
        Me.INDlyItemTxtUnitValue.TextSize = New System.Drawing.Size(119, 21)
        '
        'INDlyItemSpnQuantity
        '
        Me.INDlyItemSpnQuantity.Control = Me.INDSpnQuantity
        Me.INDlyItemSpnQuantity.CustomizationFormText = "LayoutControlItem3"
        Me.INDlyItemSpnQuantity.Location = New System.Drawing.Point(0, 36)
        Me.INDlyItemSpnQuantity.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemSpnQuantity.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemSpnQuantity.Name = "INDlyItemSpnQuantity"
        Me.INDlyItemSpnQuantity.Size = New System.Drawing.Size(492, 36)
        Me.INDlyItemSpnQuantity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemSpnQuantity.Text = "Cantidad"
        Me.INDlyItemSpnQuantity.TextSize = New System.Drawing.Size(119, 21)
        '
        'INDPctrAceptar
        '
        Me.INDPctrAceptar.Controls.Add(Me.INDbtnAceptar)
        Me.INDPctrAceptar.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.INDPctrAceptar.Location = New System.Drawing.Point(202, 402)
        Me.INDPctrAceptar.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDPctrAceptar.Name = "INDPctrAceptar"
        Me.INDPctrAceptar.Size = New System.Drawing.Size(950, 29)
        Me.INDPctrAceptar.TabIndex = 2
        '
        'INDbtnAceptar
        '
        Me.INDbtnAceptar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDbtnAceptar.Location = New System.Drawing.Point(2, 2)
        Me.INDbtnAceptar.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDbtnAceptar.Name = "INDbtnAceptar"
        Me.INDbtnAceptar.Size = New System.Drawing.Size(946, 25)
        Me.INDbtnAceptar.TabIndex = 0
        Me.INDbtnAceptar.Text = "Aceptar"
        '
        'FrmPopUpProductPurchaseOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1154, 550)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "FrmPopUpProductPurchaseOrder"
        Me.Opacity = 1.0R
        Me.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.Text = "FrmPopUpProductPurchaseOrder"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDTxtSubTotal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtForeignCurrencyValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtProductMeasure.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtProductUnit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtProductPresentation.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtProductName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtValuePesos.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtUnitValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSpnQuantity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleProduct.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDMmoDetail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSpnPercentDiscount.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSpnVatPercent.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyPopupProductPurchaseOrder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyGpPurchaseOrderProduct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemSleProduct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemMmoDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemTxtProductName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemTxtProductPresentation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemTxtProductUnit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemTxtProductMeasure, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyGpPurchaseOrderDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemTxtValuePesos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemTxtForeignCurrencyValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemTxtSubTotal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemSpnPercentDiscount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemSpnVatPercent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemTxtUnitValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemSpnQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPctrAceptar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPctrAceptar.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlyPopupProductPurchaseOrder As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents INDTxtUnitValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDSpnQuantity As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDSleProduct As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents INDPctrAceptar As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDbtnAceptar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDTxtForeignCurrencyValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtProductMeasure As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtProductUnit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtProductPresentation As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtProductName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtValuePesos As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLyGpPurchaseOrderProduct As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemSleProduct As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemMmoDetail As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemSpnQuantity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemTxtUnitValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemTxtValuePesos As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemTxtProductName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemTxtProductPresentation As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemTxtProductUnit As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemTxtProductMeasure As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemTxtForeignCurrencyValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyGpPurchaseOrderDetail As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDTxtSubTotal As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemTxtSubTotal As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemSpnPercentDiscount As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemSpnVatPercent As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDMmoDetail As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDSpnPercentDiscount As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDSpnVatPercent As DevExpress.XtraEditors.SpinEdit
End Class
