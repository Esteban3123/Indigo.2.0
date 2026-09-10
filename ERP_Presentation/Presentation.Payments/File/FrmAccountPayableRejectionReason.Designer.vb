Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmAccountPayableRejectionReason
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
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlyAccountPayableRejectionReason = New DevExpress.XtraLayout.LayoutControl()
        Me.INDmeDescription = New DevExpress.XtraEditors.MemoEdit()
        Me.INDtxtName = New DevExpress.XtraEditors.TextEdit()
        Me.INDbteCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlygGeneralInformation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemDescription = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyAccountPayableRejectionReason, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyAccountPayableRejectionReason.SuspendLayout()
        CType(Me.INDmeDescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbteCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygGeneralInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyAccountPayableRejectionReason)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(505, 349)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(505, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(505, 98)
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDlyAccountPayableRejectionReason
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 340)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDlyAccountPayableRejectionReason
        '
        Me.INDlyAccountPayableRejectionReason.AllowCustomization = False
        Me.INDlyAccountPayableRejectionReason.Controls.Add(Me.INDmeDescription)
        Me.INDlyAccountPayableRejectionReason.Controls.Add(Me.INDtxtName)
        Me.INDlyAccountPayableRejectionReason.Controls.Add(Me.INDbteCode)
        Me.INDlyAccountPayableRejectionReason.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDlyAccountPayableRejectionReason, False)
        Me.INDlyAccountPayableRejectionReason.Location = New System.Drawing.Point(202, 7)
        Me.INDlyAccountPayableRejectionReason.Name = "INDlyAccountPayableRejectionReason"
        Me.INDlyAccountPayableRejectionReason.Root = Me.LayoutControlGroup1
        Me.INDlyAccountPayableRejectionReason.Size = New System.Drawing.Size(301, 340)
        Me.INDlyAccountPayableRejectionReason.TabIndex = 1
        Me.INDlyAccountPayableRejectionReason.Text = "LayoutControl1"
        '
        'INDmeDescription
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDmeDescription, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDmeDescription, True)
        Me.INDmeDescription.Location = New System.Drawing.Point(24, 213)
        Me.IndigoTextEdit1.SetMascara(Me.INDmeDescription, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDmeDescription.Name = "INDmeDescription"
        Me.INDmeDescription.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDmeDescription.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmeDescription.Properties.Appearance.Options.UseBackColor = True
        Me.INDmeDescription.Properties.Appearance.Options.UseFont = True
        Me.INDmeDescription.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDmeDescription.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDmeDescription.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmeDescription.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDmeDescription.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDmeDescription.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDmeDescription.Properties.MaxLength = 500
        Me.INDmeDescription.Size = New System.Drawing.Size(386, 70)
        Me.INDmeDescription.StyleController = Me.INDlyAccountPayableRejectionReason
        Me.INDmeDescription.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDmeDescription, 0)
        Me.INDmeDescription.ToolTip = "Este Campo es Necesario"
        '
        'INDtxtName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtName, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtName, True)
        Me.INDtxtName.EnterMoveNextControl = True
        Me.INDtxtName.Location = New System.Drawing.Point(24, 149)
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
        Me.INDtxtName.Properties.MaxLength = 50
        Me.INDtxtName.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtName.StyleController = Me.INDlyAccountPayableRejectionReason
        Me.INDtxtName.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtName, 0)
        Me.INDtxtName.ToolTip = "Este Campo es Necesario"
        '
        'INDbteCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbteCode, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbteCode, True)
        Me.INDbteCode.Location = New System.Drawing.Point(24, 85)
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
        Me.INDbteCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Global.Presentation.Payments.My.Resources.Resources.BuscarMetro, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.INDbteCode.Properties.MaxLength = 20
        Me.INDbteCode.Size = New System.Drawing.Size(386, 28)
        Me.INDbteCode.StyleController = Me.INDlyAccountPayableRejectionReason
        Me.INDbteCode.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbteCode, 0)
        Me.INDbteCode.ToolTip = "Este Campo es Necesario"
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
        Me.LayoutControlGroup1.CustomizationFormText = "Razones Rechazos Facturas"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlygGeneralInformation})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(434, 323)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlygGeneralInformation
        '
        Me.INDlygGeneralInformation.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygGeneralInformation.AppearanceGroup.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygGeneralInformation.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygGeneralInformation.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygGeneralInformation.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygGeneralInformation, False)
        Me.INDlygGeneralInformation.CustomizationFormText = "Información General"
        Me.INDlygGeneralInformation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemCode, Me.INDlyItemName, Me.INDlyItemDescription})
        Me.INDlygGeneralInformation.Location = New System.Drawing.Point(0, 0)
        Me.INDlygGeneralInformation.Name = "INDlygGeneralInformation"
        Me.INDlygGeneralInformation.Size = New System.Drawing.Size(414, 303)
        Me.INDlygGeneralInformation.Text = "Información General"
        '
        'INDlyItemCode
        '
        Me.INDlyItemCode.Control = Me.INDbteCode
        Me.INDlyItemCode.CustomizationFormText = "Código"
        Me.INDlyItemCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCode.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemCode.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemCode.Name = "INDlyItemCode"
        Me.INDlyItemCode.ShowInCustomizationForm = False
        Me.INDlyItemCode.Size = New System.Drawing.Size(390, 64)
        Me.INDlyItemCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCode.Text = "Código"
        Me.INDlyItemCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemCode.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemCode.TextToControlDistance = 5
        '
        'INDlyItemName
        '
        Me.INDlyItemName.Control = Me.INDtxtName
        Me.INDlyItemName.CustomizationFormText = "Nombre"
        Me.INDlyItemName.Location = New System.Drawing.Point(0, 64)
        Me.INDlyItemName.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemName.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemName.Name = "INDlyItemName"
        Me.INDlyItemName.ShowInCustomizationForm = False
        Me.INDlyItemName.Size = New System.Drawing.Size(390, 64)
        Me.INDlyItemName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemName.Text = "Nombre"
        Me.INDlyItemName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemName.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemName.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemName.TextToControlDistance = 5
        '
        'INDlyItemDescription
        '
        Me.INDlyItemDescription.Control = Me.INDmeDescription
        Me.INDlyItemDescription.CustomizationFormText = "Descripción"
        Me.INDlyItemDescription.Location = New System.Drawing.Point(0, 128)
        Me.INDlyItemDescription.MaxSize = New System.Drawing.Size(390, 100)
        Me.INDlyItemDescription.MinSize = New System.Drawing.Size(390, 100)
        Me.INDlyItemDescription.Name = "INDlyItemDescription"
        Me.INDlyItemDescription.ShowInCustomizationForm = False
        Me.INDlyItemDescription.Size = New System.Drawing.Size(390, 116)
        Me.INDlyItemDescription.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemDescription.Text = "Descripción"
        Me.INDlyItemDescription.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemDescription.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemDescription.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemDescription.TextToControlDistance = 5
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'FrmAccountPayableRejectionReason
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(505, 471)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmAccountPayableRejectionReason"
        Me.Opacity = 1.0R
        Me.Tag = "722"
        Me.Text = "Razones Rechazos Facturas"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyAccountPayableRejectionReason, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyAccountPayableRejectionReason.ResumeLayout(False)
        CType(Me.INDmeDescription.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbteCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygGeneralInformation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDlyAccountPayableRejectionReason As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDbteCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDlyItemCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDlygGeneralInformation As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDtxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDmeDescription As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDlyItemDescription As DevExpress.XtraLayout.LayoutControlItem
End Class
