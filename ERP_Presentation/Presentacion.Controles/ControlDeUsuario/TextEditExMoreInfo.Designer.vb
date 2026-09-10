<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TextEditExMoreInfo
    Inherits DevExpress.XtraEditors.XtraUserControl

    'UserControl overrides dispose to clean up the component list.
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me._btnEdit = New DevExpress.XtraEditors.ButtonEdit()
        Me.PcePopUpEdit = New DevExpress.XtraEditors.PopupContainerEdit()
        CType(Me._btnEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PcePopUpEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        '_btnEdit
        '
        Me._btnEdit.Dock = System.Windows.Forms.DockStyle.Fill
        Me._btnEdit.Location = New System.Drawing.Point(0, 0)
        Me._btnEdit.Name = "_btnEdit"
        Me._btnEdit.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._btnEdit.Properties.Appearance.Options.UseFont = True
        EditorButtonImageOptions1.Image = Global.Presentation.Controls.My.Resources.Resources.MoreInfo_16x16_blue
        Me._btnEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", "MORE_INFO", Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me._btnEdit.Properties.Mask.EditMask = "[a-zA-Z0-9]+"
        Me._btnEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me._btnEdit.Properties.Mask.UseMaskAsDisplayFormat = True
        Me._btnEdit.Properties.MaxLength = 25
        Me._btnEdit.Size = New System.Drawing.Size(100, 28)
        Me._btnEdit.TabIndex = 0
        '
        'PcePopUpEdit
        '
        Me.PcePopUpEdit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PcePopUpEdit.Location = New System.Drawing.Point(0, 0)
        Me.PcePopUpEdit.Name = "PcePopUpEdit"
        Me.PcePopUpEdit.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.PcePopUpEdit.Properties.Appearance.Options.UseFont = True
        Me.PcePopUpEdit.Properties.PopupSizeable = False
        Me.PcePopUpEdit.Properties.ShowPopupCloseButton = False
        Me.PcePopUpEdit.Properties.ShowPopupShadow = False
        Me.PcePopUpEdit.Size = New System.Drawing.Size(100, 28)
        Me.PcePopUpEdit.TabIndex = 2
        '
        'TextEditExMoreInfo
        '
        Me.Appearance.BackColor = System.Drawing.SystemColors.Control
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me._btnEdit)
        Me.Controls.Add(Me.PcePopUpEdit)
        Me.Name = "TextEditExMoreInfo"
        Me.Size = New System.Drawing.Size(100, 28)
        CType(Me._btnEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PcePopUpEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents _btnEdit As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents PcePopUpEdit As DevExpress.XtraEditors.PopupContainerEdit
End Class
