Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmPopupGlosaMedicalFeesEvaluation
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
        Me.INDlyRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDTeGlossedValue = New DevExpress.XtraEditors.TextEdit()
        Me.INDTeSupplierAcceptedValue = New DevExpress.XtraEditors.TextEdit()
        Me.INDTeRaisedValue = New DevExpress.XtraEditors.TextEdit()
        Me.INDMmoObservation = New DevExpress.XtraEditors.MemoEdit()
        Me.INDTePedingValue = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcgProduct = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciGlossedValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciSupplierAcceptedValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciRaisedValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciObservation = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciPedingValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyRoot.SuspendLayout()
        CType(Me.INDTeGlossedValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTeSupplierAcceptedValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTeRaisedValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDMmoObservation.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTePedingValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciGlossedValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciSupplierAcceptedValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciRaisedValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciObservation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciPedingValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyRoot)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(666, 401)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(666, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(666, 130)
        '
        'INDlyRoot
        '
        Me.INDlyRoot.AllowCustomization = False
        Me.INDlyRoot.Controls.Add(Me.INDTeGlossedValue)
        Me.INDlyRoot.Controls.Add(Me.INDTeSupplierAcceptedValue)
        Me.INDlyRoot.Controls.Add(Me.INDTeRaisedValue)
        Me.INDlyRoot.Controls.Add(Me.INDMmoObservation)
        Me.INDlyRoot.Controls.Add(Me.INDTePedingValue)
        Me.INDlyRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDlyRoot, False)
        Me.INDlyRoot.Location = New System.Drawing.Point(202, 7)
        Me.INDlyRoot.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDlyRoot.Name = "INDlyRoot"
        Me.INDlyRoot.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(712, 105, 574, 569)
        Me.INDlyRoot.Root = Me.LayoutControlGroup1
        Me.INDlyRoot.Size = New System.Drawing.Size(462, 392)
        Me.INDlyRoot.TabIndex = 2
        Me.INDlyRoot.Text = "LayoutControl1"
        '
        'INDTeGlossedValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTeGlossedValue, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTeGlossedValue, False)
        Me.INDTeGlossedValue.EnterMoveNextControl = True
        Me.INDTeGlossedValue.Location = New System.Drawing.Point(171, 53)
        Me.IndigoTextEdit1.SetMascara(Me.INDTeGlossedValue, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTeGlossedValue.Name = "INDTeGlossedValue"
        Me.INDTeGlossedValue.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTeGlossedValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeGlossedValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDTeGlossedValue.Properties.Appearance.Options.UseFont = True
        Me.INDTeGlossedValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTeGlossedValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTeGlossedValue.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTeGlossedValue.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTeGlossedValue.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeGlossedValue.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTeGlossedValue.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTeGlossedValue.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTeGlossedValue.Properties.Mask.EditMask = "c2"
        Me.INDTeGlossedValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDTeGlossedValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTeGlossedValue.Properties.NullText = "$0"
        Me.INDTeGlossedValue.Size = New System.Drawing.Size(239, 28)
        Me.INDTeGlossedValue.StyleController = Me.INDlyRoot
        Me.INDTeGlossedValue.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTeGlossedValue, 0)
        '
        'INDTeSupplierAcceptedValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTeSupplierAcceptedValue, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTeSupplierAcceptedValue, False)
        Me.INDTeSupplierAcceptedValue.EnterMoveNextControl = True
        Me.INDTeSupplierAcceptedValue.Location = New System.Drawing.Point(171, 89)
        Me.IndigoTextEdit1.SetMascara(Me.INDTeSupplierAcceptedValue, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTeSupplierAcceptedValue.Name = "INDTeSupplierAcceptedValue"
        Me.INDTeSupplierAcceptedValue.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTeSupplierAcceptedValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeSupplierAcceptedValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDTeSupplierAcceptedValue.Properties.Appearance.Options.UseFont = True
        Me.INDTeSupplierAcceptedValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTeSupplierAcceptedValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTeSupplierAcceptedValue.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTeSupplierAcceptedValue.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTeSupplierAcceptedValue.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeSupplierAcceptedValue.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTeSupplierAcceptedValue.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTeSupplierAcceptedValue.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTeSupplierAcceptedValue.Properties.Mask.EditMask = "c2"
        Me.INDTeSupplierAcceptedValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDTeSupplierAcceptedValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTeSupplierAcceptedValue.Properties.NullText = "$0"
        Me.INDTeSupplierAcceptedValue.Size = New System.Drawing.Size(239, 28)
        Me.INDTeSupplierAcceptedValue.StyleController = Me.INDlyRoot
        Me.INDTeSupplierAcceptedValue.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTeSupplierAcceptedValue, 0)
        '
        'INDTeRaisedValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTeRaisedValue, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTeRaisedValue, False)
        Me.INDTeRaisedValue.EnterMoveNextControl = True
        Me.INDTeRaisedValue.Location = New System.Drawing.Point(171, 125)
        Me.IndigoTextEdit1.SetMascara(Me.INDTeRaisedValue, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTeRaisedValue.Name = "INDTeRaisedValue"
        Me.INDTeRaisedValue.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTeRaisedValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeRaisedValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDTeRaisedValue.Properties.Appearance.Options.UseFont = True
        Me.INDTeRaisedValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTeRaisedValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTeRaisedValue.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTeRaisedValue.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTeRaisedValue.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeRaisedValue.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTeRaisedValue.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTeRaisedValue.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTeRaisedValue.Properties.Mask.EditMask = "c2"
        Me.INDTeRaisedValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDTeRaisedValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTeRaisedValue.Properties.NullText = "$0"
        Me.INDTeRaisedValue.Size = New System.Drawing.Size(239, 28)
        Me.INDTeRaisedValue.StyleController = Me.INDlyRoot
        Me.INDTeRaisedValue.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTeRaisedValue, 0)
        '
        'INDMmoObservation
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDMmoObservation, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDMmoObservation, False)
        Me.INDMmoObservation.EnterMoveNextControl = True
        Me.INDMmoObservation.Location = New System.Drawing.Point(171, 197)
        Me.INDMmoObservation.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDMmoObservation, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDMmoObservation.Name = "INDMmoObservation"
        Me.INDMmoObservation.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDMmoObservation.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDMmoObservation.Properties.Appearance.Options.UseBackColor = True
        Me.INDMmoObservation.Properties.Appearance.Options.UseFont = True
        Me.INDMmoObservation.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDMmoObservation.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDMmoObservation.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDMmoObservation.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDMmoObservation.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDMmoObservation.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDMmoObservation.Properties.MaxLength = 300
        Me.INDMmoObservation.Size = New System.Drawing.Size(239, 124)
        Me.INDMmoObservation.StyleController = Me.INDlyRoot
        Me.INDMmoObservation.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDMmoObservation, 0)
        '
        'INDTePedingValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTePedingValue, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTePedingValue, False)
        Me.INDTePedingValue.Enabled = False
        Me.INDTePedingValue.EnterMoveNextControl = True
        Me.INDTePedingValue.Location = New System.Drawing.Point(171, 161)
        Me.IndigoTextEdit1.SetMascara(Me.INDTePedingValue, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTePedingValue.Name = "INDTePedingValue"
        Me.INDTePedingValue.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTePedingValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTePedingValue.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDTePedingValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDTePedingValue.Properties.Appearance.Options.UseFont = True
        Me.INDTePedingValue.Properties.Appearance.Options.UseForeColor = True
        Me.INDTePedingValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTePedingValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTePedingValue.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTePedingValue.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTePedingValue.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTePedingValue.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDTePedingValue.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTePedingValue.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTePedingValue.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTePedingValue.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDTePedingValue.Properties.Mask.EditMask = "c2"
        Me.INDTePedingValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDTePedingValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTePedingValue.Properties.NullText = "$0"
        Me.INDTePedingValue.Size = New System.Drawing.Size(239, 28)
        Me.INDTePedingValue.StyleController = Me.INDlyRoot
        Me.INDTePedingValue.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTePedingValue, 0)
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
        Me.LayoutControlGroup1.CustomizationFormText = "Agregar Producto"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgProduct})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(462, 392)
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
        Me.INDLcgProduct.CustomizationFormText = "Evaluacion"
        Me.INDLcgProduct.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciGlossedValue, Me.INDLciSupplierAcceptedValue, Me.INDLciRaisedValue, Me.INDLciObservation, Me.INDLciPedingValue})
        Me.INDLcgProduct.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgProduct.Name = "INDLcgProduct"
        Me.INDLcgProduct.Size = New System.Drawing.Size(442, 372)
        Me.INDLcgProduct.Text = "Datos Principales"
        '
        'INDLciGlossedValue
        '
        Me.INDLciGlossedValue.Control = Me.INDTeGlossedValue
        Me.INDLciGlossedValue.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciGlossedValue.CustomizationFormText = "Valor Glosado"
        Me.INDLciGlossedValue.Location = New System.Drawing.Point(0, 0)
        Me.INDLciGlossedValue.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDLciGlossedValue.MinSize = New System.Drawing.Size(390, 36)
        Me.INDLciGlossedValue.Name = "INDLciGlossedValue"
        Me.INDLciGlossedValue.Size = New System.Drawing.Size(418, 36)
        Me.INDLciGlossedValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciGlossedValue.Text = "Valor Glosado"
        Me.INDLciGlossedValue.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciGlossedValue.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciGlossedValue.TextToControlDistance = 12
        '
        'INDLciSupplierAcceptedValue
        '
        Me.INDLciSupplierAcceptedValue.Control = Me.INDTeSupplierAcceptedValue
        Me.INDLciSupplierAcceptedValue.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciSupplierAcceptedValue.CustomizationFormText = "Valor Aceptado Prov"
        Me.INDLciSupplierAcceptedValue.Location = New System.Drawing.Point(0, 36)
        Me.INDLciSupplierAcceptedValue.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDLciSupplierAcceptedValue.MinSize = New System.Drawing.Size(390, 36)
        Me.INDLciSupplierAcceptedValue.Name = "INDLciSupplierAcceptedValue"
        Me.INDLciSupplierAcceptedValue.Size = New System.Drawing.Size(418, 36)
        Me.INDLciSupplierAcceptedValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciSupplierAcceptedValue.Text = "Valor Aceptado Prov"
        Me.INDLciSupplierAcceptedValue.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciSupplierAcceptedValue.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciSupplierAcceptedValue.TextToControlDistance = 12
        '
        'INDLciRaisedValue
        '
        Me.INDLciRaisedValue.Control = Me.INDTeRaisedValue
        Me.INDLciRaisedValue.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciRaisedValue.CustomizationFormText = "Valor Levantado"
        Me.INDLciRaisedValue.Location = New System.Drawing.Point(0, 72)
        Me.INDLciRaisedValue.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDLciRaisedValue.MinSize = New System.Drawing.Size(390, 36)
        Me.INDLciRaisedValue.Name = "INDLciRaisedValue"
        Me.INDLciRaisedValue.Size = New System.Drawing.Size(418, 36)
        Me.INDLciRaisedValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciRaisedValue.Text = "Valor Levantado"
        Me.INDLciRaisedValue.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciRaisedValue.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciRaisedValue.TextToControlDistance = 12
        '
        'INDLciObservation
        '
        Me.INDLciObservation.Control = Me.INDMmoObservation
        Me.INDLciObservation.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciObservation.CustomizationFormText = "Observación"
        Me.INDLciObservation.Location = New System.Drawing.Point(0, 144)
        Me.INDLciObservation.MaxSize = New System.Drawing.Size(390, 128)
        Me.INDLciObservation.MinSize = New System.Drawing.Size(390, 128)
        Me.INDLciObservation.Name = "INDLciObservation"
        Me.INDLciObservation.Size = New System.Drawing.Size(418, 175)
        Me.INDLciObservation.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciObservation.Text = "Observación"
        Me.INDLciObservation.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciObservation.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciObservation.TextToControlDistance = 12
        '
        'INDLciPedingValue
        '
        Me.INDLciPedingValue.Control = Me.INDTePedingValue
        Me.INDLciPedingValue.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciPedingValue.CustomizationFormText = "Valor Pendiente"
        Me.INDLciPedingValue.Enabled = False
        Me.INDLciPedingValue.Location = New System.Drawing.Point(0, 108)
        Me.INDLciPedingValue.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDLciPedingValue.MinSize = New System.Drawing.Size(390, 36)
        Me.INDLciPedingValue.Name = "INDLciPedingValue"
        Me.INDLciPedingValue.Size = New System.Drawing.Size(418, 36)
        Me.INDLciPedingValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciPedingValue.Text = "Valor Pendiente"
        Me.INDLciPedingValue.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciPedingValue.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciPedingValue.TextToControlDistance = 12
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.INDlyRoot
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 392)
        Me.CtrNavigationControlPanel1.TabIndex = 3
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Opción"
        Me.GridColumn3.FieldName = "Item2"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowMove = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 0
        '
        'FrmPopupGlosaMedicalFeesEvaluation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(666, 536)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.IconOptions.ShowIcon = False
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmPopupGlosaMedicalFeesEvaluation"
        Me.Opacity = 1.0R
        Me.Text = "Asociar centro de atención y línea de producción"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyRoot.ResumeLayout(False)
        CType(Me.INDTeGlossedValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTeSupplierAcceptedValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTeRaisedValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDMmoObservation.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTePedingValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgProduct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciGlossedValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciSupplierAcceptedValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciRaisedValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciObservation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciPedingValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents IndigoTextEdit1 As Controls.IndigoTextEdit
    Friend WithEvents IndigoSimpleButton1 As Controls.IndigoSimpleButton
    Friend WithEvents IndigoSearchLookUpControl1 As Controls.IndigoSearchLookUpControl
    Friend WithEvents INDlyRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLcgProduct As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents CtrNavigationControlPanel1 As CtrNavigationControlPanel
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents INDTeGlossedValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciGlossedValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDTeSupplierAcceptedValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciSupplierAcceptedValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDTeRaisedValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciRaisedValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDMmoObservation As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDLciObservation As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDTePedingValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciPedingValue As DevExpress.XtraLayout.LayoutControlItem
End Class
