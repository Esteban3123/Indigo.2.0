Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCodeIVA
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
        Me.INDlyCodeIVA = New DevExpress.XtraLayout.LayoutControl()
        Me.INDspeRate = New DevExpress.XtraEditors.SpinEdit()
        Me.INDspePercentage = New DevExpress.XtraEditors.SpinEdit()
        Me.INDtxtName = New DevExpress.XtraEditors.TextEdit()
        Me.INDbteCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDlygCodeIVA = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyCgCodeIVA = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemPercentage = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemRate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyCodeIVA, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyCodeIVA.SuspendLayout()
        CType(Me.INDspeRate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDspePercentage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbteCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygCodeIVA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyCgCodeIVA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemPercentage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyCodeIVA)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 118)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(738, 421)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(738, 94)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(738, 94)
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDlyCodeIVA
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 412)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDlyCodeIVA
        '
        Me.INDlyCodeIVA.Controls.Add(Me.INDspeRate)
        Me.INDlyCodeIVA.Controls.Add(Me.INDspePercentage)
        Me.INDlyCodeIVA.Controls.Add(Me.INDtxtName)
        Me.INDlyCodeIVA.Controls.Add(Me.INDbteCode)
        Me.INDlyCodeIVA.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyCodeIVA.Location = New System.Drawing.Point(202, 7)
        Me.INDlyCodeIVA.Name = "INDlyCodeIVA"
        Me.INDlyCodeIVA.Root = Me.INDlygCodeIVA
        Me.INDlyCodeIVA.Size = New System.Drawing.Size(534, 412)
        Me.INDlyCodeIVA.TabIndex = 1
        Me.INDlyCodeIVA.Text = "LayoutControl1"
        '
        'INDspeRate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDspeRate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDspeRate, True)
        Me.INDspeRate.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDspeRate.Location = New System.Drawing.Point(164, 167)
        Me.IndigoTextEdit1.SetMascara(Me.INDspeRate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDspeRate.Name = "INDspeRate"
        Me.INDspeRate.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDspeRate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDspeRate.Properties.Appearance.Options.UseBackColor = True
        Me.INDspeRate.Properties.Appearance.Options.UseFont = True
        Me.INDspeRate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDspeRate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDspeRate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDspeRate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDspeRate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDspeRate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDspeRate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDspeRate.Size = New System.Drawing.Size(246, 28)
        Me.INDspeRate.StyleController = Me.INDlyCodeIVA
        Me.INDspeRate.TabIndex = 7
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDspeRate, 0)
        '
        'INDspePercentage
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDspePercentage, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDspePercentage, True)
        Me.INDspePercentage.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDspePercentage.Location = New System.Drawing.Point(164, 131)
        Me.IndigoTextEdit1.SetMascara(Me.INDspePercentage, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDspePercentage.Name = "INDspePercentage"
        Me.INDspePercentage.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDspePercentage.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDspePercentage.Properties.Appearance.Options.UseBackColor = True
        Me.INDspePercentage.Properties.Appearance.Options.UseFont = True
        Me.INDspePercentage.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDspePercentage.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDspePercentage.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDspePercentage.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDspePercentage.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDspePercentage.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDspePercentage.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDspePercentage.Size = New System.Drawing.Size(246, 28)
        Me.INDspePercentage.StyleController = Me.INDlyCodeIVA
        Me.INDspePercentage.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDspePercentage, 0)
        '
        'INDtxtName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtName, True)
        Me.INDtxtName.Location = New System.Drawing.Point(164, 95)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtName.Name = "INDtxtName"
        Me.INDtxtName.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDtxtName.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtName.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtName.Properties.Appearance.Options.UseFont = True
        Me.INDtxtName.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtName.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtName.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtName.Size = New System.Drawing.Size(246, 28)
        Me.INDtxtName.StyleController = Me.INDlyCodeIVA
        Me.INDtxtName.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtName, 0)
        Me.INDtxtName.ToolTip = "Este Campo es Necesario"
        '
        'INDbteCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbteCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbteCode, True)
        Me.INDbteCode.Location = New System.Drawing.Point(164, 59)
        Me.IndigoTextEdit1.SetMascara(Me.INDbteCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDbteCode.Name = "INDbteCode"
        Me.INDbteCode.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDbteCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbteCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDbteCode.Properties.Appearance.Options.UseFont = True
        Me.INDbteCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDbteCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDbteCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDbteCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.INDbteCode.Size = New System.Drawing.Size(156, 28)
        Me.INDbteCode.StyleController = Me.INDlyCodeIVA
        Me.INDbteCode.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbteCode, 0)
        Me.INDbteCode.ToolTip = "Este Campo es Necesario"
        '
        'INDlygCodeIVA
        '
        Me.INDlygCodeIVA.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlygCodeIVA.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlygCodeIVA.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygCodeIVA.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygCodeIVA.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygCodeIVA.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygCodeIVA.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlygCodeIVA.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlygCodeIVA.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygCodeIVA.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygCodeIVA.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlygCodeIVA.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlygCodeIVA.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygCodeIVA.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygCodeIVA, False)
        Me.INDlygCodeIVA.CustomizationFormText = "INDlygCodeIVA"
        Me.INDlygCodeIVA.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlygCodeIVA.GroupBordersVisible = False
        Me.INDlygCodeIVA.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyCgCodeIVA})
        Me.INDlygCodeIVA.Location = New System.Drawing.Point(0, 0)
        Me.INDlygCodeIVA.Name = "INDlygCodeIVA"
        Me.INDlygCodeIVA.Size = New System.Drawing.Size(534, 412)
        Me.INDlygCodeIVA.TextVisible = False
        '
        'INDlyCgCodeIVA
        '
        Me.INDlyCgCodeIVA.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlyCgCodeIVA.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlyCgCodeIVA.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyCgCodeIVA.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyCgCodeIVA.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyCgCodeIVA.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyCgCodeIVA.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlyCgCodeIVA.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlyCgCodeIVA.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyCgCodeIVA.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyCgCodeIVA.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlyCgCodeIVA.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlyCgCodeIVA.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyCgCodeIVA.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyCgCodeIVA, False)
        Me.INDlyCgCodeIVA.CustomizationFormText = "Códigos de IVA"
        Me.INDlyCgCodeIVA.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemCode, Me.INDlyItemName, Me.INDlyItemPercentage, Me.INDlyItemRate})
        Me.INDlyCgCodeIVA.Location = New System.Drawing.Point(0, 0)
        Me.INDlyCgCodeIVA.Name = "INDlyCgCodeIVA"
        Me.INDlyCgCodeIVA.Size = New System.Drawing.Size(514, 392)
        Me.INDlyCgCodeIVA.Text = "Códigos de IVA"
        '
        'INDlyItemCode
        '
        Me.INDlyItemCode.Control = Me.INDbteCode
        Me.INDlyItemCode.CustomizationFormText = "Código"
        Me.INDlyItemCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCode.MaxSize = New System.Drawing.Size(300, 36)
        Me.INDlyItemCode.MinSize = New System.Drawing.Size(300, 36)
        Me.INDlyItemCode.Name = "INDlyItemCode"
        Me.INDlyItemCode.Size = New System.Drawing.Size(490, 36)
        Me.INDlyItemCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCode.Text = "Código"
        Me.INDlyItemCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCode.TextSize = New System.Drawing.Size(135, 20)
        Me.INDlyItemCode.TextToControlDistance = 5
        '
        'INDlyItemName
        '
        Me.INDlyItemName.Control = Me.INDtxtName
        Me.INDlyItemName.CustomizationFormText = "Nombre"
        Me.INDlyItemName.Location = New System.Drawing.Point(0, 36)
        Me.INDlyItemName.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemName.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemName.Name = "INDlyItemName"
        Me.INDlyItemName.Size = New System.Drawing.Size(490, 36)
        Me.INDlyItemName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemName.Text = "Nombre"
        Me.INDlyItemName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemName.TextSize = New System.Drawing.Size(135, 20)
        Me.INDlyItemName.TextToControlDistance = 5
        '
        'INDlyItemPercentage
        '
        Me.INDlyItemPercentage.Control = Me.INDspePercentage
        Me.INDlyItemPercentage.CustomizationFormText = "Porcentaje"
        Me.INDlyItemPercentage.Location = New System.Drawing.Point(0, 72)
        Me.INDlyItemPercentage.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemPercentage.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemPercentage.Name = "INDlyItemPercentage"
        Me.INDlyItemPercentage.Size = New System.Drawing.Size(490, 36)
        Me.INDlyItemPercentage.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemPercentage.Text = "Porcentaje"
        Me.INDlyItemPercentage.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemPercentage.TextSize = New System.Drawing.Size(135, 20)
        Me.INDlyItemPercentage.TextToControlDistance = 5
        '
        'INDlyItemRate
        '
        Me.INDlyItemRate.Control = Me.INDspeRate
        Me.INDlyItemRate.CustomizationFormText = "Tarifa"
        Me.INDlyItemRate.Location = New System.Drawing.Point(0, 108)
        Me.INDlyItemRate.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemRate.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemRate.Name = "INDlyItemRate"
        Me.INDlyItemRate.Size = New System.Drawing.Size(490, 225)
        Me.INDlyItemRate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemRate.Text = "Tarifa"
        Me.INDlyItemRate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemRate.TextSize = New System.Drawing.Size(135, 20)
        Me.INDlyItemRate.TextToControlDistance = 5
        '
        'FrmCodeIVA
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(738, 539)
        Me.Name = "FrmCodeIVA"
        Me.Opacity = 1.0R
        Me.Tag = "313"
        Me.Text = "Códigos de Iva"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyCodeIVA, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyCodeIVA.ResumeLayout(False)
        CType(Me.INDspeRate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDspePercentage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbteCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygCodeIVA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyCgCodeIVA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemPercentage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDlyCodeIVA As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlygCodeIVA As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDspeRate As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDspePercentage As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDtxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDbteCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDlyItemCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemPercentage As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemRate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyCgCodeIVA As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
End Class
