Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDisability
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
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.INDlyCtrDisability = New DevExpress.XtraLayout.LayoutControl()
        Me.INDtxtDescription = New DevExpress.XtraEditors.TextEdit()
        Me.INDbteCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemDescription = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit()
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        CType(Me.INDPanelControlBase,System.ComponentModel.ISupportInitialize).BeginInit
        Me.INDPanelControlBase.SuspendLayout
        CType(Me.ToolBars,System.ComponentModel.ISupportInitialize).BeginInit
        Me.ToolBars.SuspendLayout
        CType(Me.LayoutControls,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlyCtrDisability,System.ComponentModel.ISupportInitialize).BeginInit
        Me.INDlyCtrDisability.SuspendLayout
        CType(Me.INDtxtDescription.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDbteCode.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlGroup1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlGroup2,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlyItemCode,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlyItemDescription,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoTextEdit1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.CtrNavigationControlPanel1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyCtrDisability)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(684, 172)
        Me.INDPanelControlBase.TabIndex = 0
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = true
        Me.ToolBars.Size = New System.Drawing.Size(684, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = true
        Me.BarraBotones.Size = New System.Drawing.Size(684, 98)
        '
        'INDlyCtrDisability
        '
        Me.INDlyCtrDisability.AllowCustomization = false
        Me.INDlyCtrDisability.Controls.Add(Me.INDtxtDescription)
        Me.INDlyCtrDisability.Controls.Add(Me.INDbteCode)
        Me.INDlyCtrDisability.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDlyCtrDisability, false)
        Me.INDlyCtrDisability.Location = New System.Drawing.Point(202, 7)
        Me.INDlyCtrDisability.Name = "INDlyCtrDisability"
        Me.INDlyCtrDisability.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(2029, 306, 671, 350)
        Me.INDlyCtrDisability.Root = Me.LayoutControlGroup1
        Me.INDlyCtrDisability.Size = New System.Drawing.Size(480, 163)
        Me.INDlyCtrDisability.TabIndex = 0
        Me.INDlyCtrDisability.Text = "LayoutControl1"
        '
        'INDtxtDescription
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtDescription, false)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtDescription, true)
        Me.INDtxtDescription.EnterMoveNextControl = true
        Me.INDtxtDescription.Location = New System.Drawing.Point(136, 95)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtDescription, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDtxtDescription.Name = "INDtxtDescription"
        Me.INDtxtDescription.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDtxtDescription.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDtxtDescription.Properties.Appearance.Options.UseBackColor = true
        Me.INDtxtDescription.Properties.Appearance.Options.UseFont = true
        Me.INDtxtDescription.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215,Byte),Integer), CType(CType(242,Byte),Integer), CType(CType(255,Byte),Integer))
        Me.INDtxtDescription.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0,Byte),Integer), CType(CType(148,Byte),Integer), CType(CType(223,Byte),Integer))
        Me.INDtxtDescription.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDtxtDescription.Properties.AppearanceFocused.Options.UseBackColor = true
        Me.INDtxtDescription.Properties.AppearanceFocused.Options.UseBorderColor = true
        Me.INDtxtDescription.Properties.AppearanceFocused.Options.UseFont = true
        Me.INDtxtDescription.Properties.Mask.EditMask = "[-a-zA-Z0-9|°¬!ñÑ""#$%&/()=?¡'¿\@¨´_.:,; ]+"
        Me.INDtxtDescription.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDtxtDescription.Properties.MaxLength = 80
        Me.INDtxtDescription.Size = New System.Drawing.Size(274, 28)
        Me.INDtxtDescription.StyleController = Me.INDlyCtrDisability
        Me.INDtxtDescription.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtDescription, 5)
        Me.INDtxtDescription.ToolTip = "Este Campo es Necesario"
        '
        'INDbteCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbteCode, false)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbteCode, true)
        Me.INDbteCode.EnterMoveNextControl = true
        Me.INDbteCode.Location = New System.Drawing.Point(136, 59)
        Me.IndigoTextEdit1.SetMascara(Me.INDbteCode, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDbteCode.Name = "INDbteCode"
        Me.INDbteCode.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDbteCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDbteCode.Properties.Appearance.Options.UseBackColor = true
        Me.INDbteCode.Properties.Appearance.Options.UseFont = true
        Me.INDbteCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215,Byte),Integer), CType(CType(242,Byte),Integer), CType(CType(255,Byte),Integer))
        Me.INDbteCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0,Byte),Integer), CType(CType(148,Byte),Integer), CType(CType(223,Byte),Integer))
        Me.INDbteCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseBackColor = true
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseBorderColor = true
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseFont = true
        Me.INDbteCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Global.Presentation.Common.My.Resources.Resources.BuscarMetro, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, true)})
        Me.INDbteCode.Properties.Mask.EditMask = "[-a-zA-Z0-9|°¬!ñÑ""#$%&/()=?¡'¿\@¨´_.:,; ]+"
        Me.INDbteCode.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDbteCode.Properties.MaxLength = 20
        Me.INDbteCode.Size = New System.Drawing.Size(184, 28)
        Me.INDbteCode.StyleController = Me.INDlyCtrDisability
        Me.INDbteCode.TabIndex = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbteCode, 1)
        Me.INDbteCode.ToolTip = "Este Campo es Necesario"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!)
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = true
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = true
        Me.LayoutControlGroup1.CustomizationFormText = "Discapacidades"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = false
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup2})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(480, 163)
        Me.LayoutControlGroup1.TextVisible = false
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.CustomizationFormText = "Discapacidades"
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemCode, Me.INDlyItemDescription})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(460, 143)
        Me.LayoutControlGroup2.Text = "Discapacidades"
        '
        'INDlyItemCode
        '
        Me.INDlyItemCode.Control = Me.INDbteCode
        Me.INDlyItemCode.CustomizationFormText = "Código"
        Me.INDlyItemCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCode.MaxSize = New System.Drawing.Size(300, 36)
        Me.INDlyItemCode.MinSize = New System.Drawing.Size(300, 36)
        Me.INDlyItemCode.Name = "INDlyItemCode"
        Me.INDlyItemCode.Size = New System.Drawing.Size(436, 36)
        Me.INDlyItemCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCode.Text = "Código"
        Me.INDlyItemCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCode.TextSize = New System.Drawing.Size(100, 21)
        Me.INDlyItemCode.TextToControlDistance = 12
        '
        'INDlyItemDescription
        '
        Me.INDlyItemDescription.Control = Me.INDtxtDescription
        Me.INDlyItemDescription.CustomizationFormText = "Descripción"
        Me.INDlyItemDescription.Location = New System.Drawing.Point(0, 36)
        Me.INDlyItemDescription.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemDescription.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemDescription.Name = "INDlyItemDescription"
        Me.INDlyItemDescription.Size = New System.Drawing.Size(436, 48)
        Me.INDlyItemDescription.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemDescription.Text = "Descripción"
        Me.INDlyItemDescription.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemDescription.TextSize = New System.Drawing.Size(100, 20)
        Me.INDlyItemDescription.TextToControlDistance = 12
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.INDlyCtrDisability
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 163)
        Me.CtrNavigationControlPanel1.TabIndex = 1
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = false
        '
        'FrmDisability
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(684, 294)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Name = "FrmDisability"
        Me.Opacity = 1R
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = "536"
        Me.Text = "Discapacidades"
        CType(Me.INDPanelControlBase,System.ComponentModel.ISupportInitialize).EndInit
        Me.INDPanelControlBase.ResumeLayout(false)
        CType(Me.ToolBars,System.ComponentModel.ISupportInitialize).EndInit
        Me.ToolBars.ResumeLayout(false)
        CType(Me.LayoutControls,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlyCtrDisability,System.ComponentModel.ISupportInitialize).EndInit
        Me.INDlyCtrDisability.ResumeLayout(false)
        CType(Me.INDtxtDescription.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDbteCode.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup2,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlyItemCode,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlyItemDescription,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoTextEdit1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.CtrNavigationControlPanel1,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents INDlyCtrDisability As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDtxtDescription As DevExpress.XtraEditors.TextEdit
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDbteCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDlyItemCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemDescription As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents CtrNavigationControlPanel1 As CtrNavigationControlPanel
End Class
