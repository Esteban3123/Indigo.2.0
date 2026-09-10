<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmKinship
    Inherits Presentation.Controls.FormBase

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
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.INDLyCtrKinship = New DevExpress.XtraLayout.LayoutControl()
        Me.INDTxtName = New DevExpress.XtraEditors.TextEdit()
        Me.INDBteCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDlyKinship = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.INDGrKinship = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyItemName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        CType(Me.INDPanelControlBase,System.ComponentModel.ISupportInitialize).BeginInit
        Me.INDPanelControlBase.SuspendLayout
        CType(Me.ToolBars,System.ComponentModel.ISupportInitialize).BeginInit
        Me.ToolBars.SuspendLayout
        CType(Me.LayoutControls,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDLyCtrKinship,System.ComponentModel.ISupportInitialize).BeginInit
        Me.INDLyCtrKinship.SuspendLayout
        CType(Me.INDTxtName.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDBteCode.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlyKinship,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.EmptySpaceItem1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDGrKinship,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlyItemCode,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDLyItemName,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoTextEdit1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.CtrNavigationControlPanel1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLyCtrKinship)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(734, 339)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = true
        Me.ToolBars.Size = New System.Drawing.Size(734, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = true
        Me.BarraBotones.Size = New System.Drawing.Size(734, 98)
        '
        'INDLyCtrKinship
        '
        Me.INDLyCtrKinship.AllowCustomization = false
        Me.INDLyCtrKinship.Controls.Add(Me.INDTxtName)
        Me.INDLyCtrKinship.Controls.Add(Me.INDBteCode)
        Me.INDLyCtrKinship.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDLyCtrKinship, false)
        Me.INDLyCtrKinship.Location = New System.Drawing.Point(202, 7)
        Me.INDLyCtrKinship.Name = "INDLyCtrKinship"
        Me.INDLyCtrKinship.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(536, 221, 250, 350)
        Me.INDLyCtrKinship.Root = Me.INDlyKinship
        Me.INDLyCtrKinship.Size = New System.Drawing.Size(530, 330)
        Me.INDLyCtrKinship.TabIndex = 0
        Me.INDLyCtrKinship.Text = "LayoutControl1"
        '
        'INDTxtName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtName, false)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtName, true)
        Me.INDTxtName.Location = New System.Drawing.Point(201, 95)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtName, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDTxtName.Name = "INDTxtName"
        Me.INDTxtName.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDTxtName.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDTxtName.Properties.Appearance.Options.UseBackColor = true
        Me.INDTxtName.Properties.Appearance.Options.UseFont = true
        Me.INDTxtName.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215,Byte),Integer), CType(CType(242,Byte),Integer), CType(CType(255,Byte),Integer))
        Me.INDTxtName.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0,Byte),Integer), CType(CType(148,Byte),Integer), CType(CType(223,Byte),Integer))
        Me.INDTxtName.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDTxtName.Properties.AppearanceFocused.Options.UseBackColor = true
        Me.INDTxtName.Properties.AppearanceFocused.Options.UseBorderColor = true
        Me.INDTxtName.Properties.AppearanceFocused.Options.UseFont = true
        Me.INDTxtName.Properties.Mask.EditMask = "[-a-zA-Z0-9|°¬!ñÑ""#$%&/()=?¡'¿\@¨´_.:,; ]+"
        Me.INDTxtName.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDTxtName.Properties.MaxLength = 50
        Me.INDTxtName.Size = New System.Drawing.Size(209, 28)
        Me.INDTxtName.StyleController = Me.INDLyCtrKinship
        Me.INDTxtName.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtName, 0)
        Me.INDTxtName.ToolTip = "Este Campo es Necesario"
        '
        'INDBteCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDBteCode, false)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDBteCode, true)
        Me.INDBteCode.Location = New System.Drawing.Point(201, 59)
        Me.IndigoTextEdit1.SetMascara(Me.INDBteCode, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDBteCode.MinimumSize = New System.Drawing.Size(184, 0)
        Me.INDBteCode.Name = "INDBteCode"
        Me.INDBteCode.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDBteCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDBteCode.Properties.Appearance.Options.UseBackColor = true
        Me.INDBteCode.Properties.Appearance.Options.UseFont = true
        Me.INDBteCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215,Byte),Integer), CType(CType(242,Byte),Integer), CType(CType(255,Byte),Integer))
        Me.INDBteCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0,Byte),Integer), CType(CType(148,Byte),Integer), CType(CType(223,Byte),Integer))
        Me.INDBteCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDBteCode.Properties.AppearanceFocused.Options.UseBackColor = true
        Me.INDBteCode.Properties.AppearanceFocused.Options.UseBorderColor = true
        Me.INDBteCode.Properties.AppearanceFocused.Options.UseFont = true
        Me.INDBteCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Global.Presentation.Payroll.My.Resources.Resources.BuscarMetro, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", Nothing, Nothing, true)})
        Me.INDBteCode.Properties.Mask.EditMask = "[-a-zA-Z0-9|°¬!ñÑ""#$%&/()=?¡'¿\@¨´_.:,; ]+"
        Me.INDBteCode.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDBteCode.Properties.MaxLength = 20
        Me.INDBteCode.Size = New System.Drawing.Size(184, 28)
        Me.INDBteCode.StyleController = Me.INDLyCtrKinship
        Me.INDBteCode.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDBteCode, 0)
        Me.INDBteCode.ToolTip = "Este Campo es Necesario"
        '
        'INDlyKinship
        '
        Me.INDlyKinship.CustomizationFormText = "Parentesco"
        Me.INDlyKinship.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlyKinship.GroupBordersVisible = false
        Me.INDlyKinship.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.EmptySpaceItem1, Me.INDGrKinship})
        Me.INDlyKinship.Location = New System.Drawing.Point(0, 0)
        Me.INDlyKinship.Name = "INDlyKinship"
        Me.INDlyKinship.Size = New System.Drawing.Size(530, 330)
        Me.INDlyKinship.TextVisible = false
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = false
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 131)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(510, 179)
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'INDGrKinship
        '
        Me.INDGrKinship.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!)
        Me.INDGrKinship.AppearanceGroup.Options.UseFont = true
        Me.INDGrKinship.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDGrKinship.AppearanceItemCaption.Options.UseFont = true
        Me.INDGrKinship.CustomizationFormText = "Grupo Parentesco"
        Me.INDGrKinship.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemCode, Me.INDLyItemName})
        Me.INDGrKinship.Location = New System.Drawing.Point(0, 0)
        Me.INDGrKinship.Name = "INDGrKinship"
        Me.INDGrKinship.Size = New System.Drawing.Size(510, 131)
        Me.INDGrKinship.Text = "Parentesco"
        '
        'INDlyItemCode
        '
        Me.INDlyItemCode.Control = Me.INDBteCode
        Me.INDlyItemCode.CustomizationFormText = "Código"
        Me.INDlyItemCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCode.MaxSize = New System.Drawing.Size(300, 36)
        Me.INDlyItemCode.MinSize = New System.Drawing.Size(300, 36)
        Me.INDlyItemCode.Name = "INDlyItemCode"
        Me.INDlyItemCode.Size = New System.Drawing.Size(486, 36)
        Me.INDlyItemCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCode.Tag = "Code"
        Me.INDlyItemCode.Text = "Código"
        Me.INDlyItemCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCode.TextSize = New System.Drawing.Size(165, 21)
        Me.INDlyItemCode.TextToControlDistance = 12
        '
        'INDLyItemName
        '
        Me.INDLyItemName.Control = Me.INDTxtName
        Me.INDLyItemName.CustomizationFormText = "Nombre"
        Me.INDLyItemName.Location = New System.Drawing.Point(0, 36)
        Me.INDLyItemName.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDLyItemName.MinSize = New System.Drawing.Size(390, 36)
        Me.INDLyItemName.Name = "INDLyItemName"
        Me.INDLyItemName.Size = New System.Drawing.Size(486, 36)
        Me.INDLyItemName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyItemName.Tag = "Name"
        Me.INDLyItemName.Text = "Nombre"
        Me.INDLyItemName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyItemName.TextSize = New System.Drawing.Size(165, 21)
        Me.INDLyItemName.TextToControlDistance = 12
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.INDLyCtrKinship
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 330)
        Me.CtrNavigationControlPanel1.TabIndex = 6
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = false
        '
        'FrmKinship
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(734, 461)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Name = "FrmKinship"
        Me.Opacity = 1R
        Me.Tag = "526"
        Me.Text = "Parentesco"
        CType(Me.INDPanelControlBase,System.ComponentModel.ISupportInitialize).EndInit
        Me.INDPanelControlBase.ResumeLayout(false)
        CType(Me.ToolBars,System.ComponentModel.ISupportInitialize).EndInit
        Me.ToolBars.ResumeLayout(false)
        CType(Me.LayoutControls,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDLyCtrKinship,System.ComponentModel.ISupportInitialize).EndInit
        Me.INDLyCtrKinship.ResumeLayout(false)
        CType(Me.INDTxtName.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDBteCode.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlyKinship,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.EmptySpaceItem1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDGrKinship,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlyItemCode,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDLyItemName,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoTextEdit1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.CtrNavigationControlPanel1,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents INDLyCtrKinship As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDTxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDBteCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDlyKinship As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents INDGrKinship As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyItemName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents CtrNavigationControlPanel1 As Controls.CtrNavigationControlPanel
End Class
