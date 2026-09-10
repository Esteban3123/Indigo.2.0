<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SearchLookUpEditExAdmission
    Inherits DevExpress.XtraEditors.XtraUserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If Me._grid.MainView IsNot Nothing Then
            Me._grid.MainView.Dispose()
        End If
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
        Me._popUpEdit = New DevExpress.XtraEditors.PopupContainerEdit()
        Me._popUpContainer = New DevExpress.XtraEditors.PopupContainerControl()
        Me.PcePopUpEdit = New DevExpress.XtraEditors.PopupContainerEdit()
        CType(Me._popUpEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._popUpContainer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PcePopUpEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        '_popUpEdit
        '
        Me._popUpEdit.Dock = System.Windows.Forms.DockStyle.Fill
        Me._popUpEdit.Location = New System.Drawing.Point(0, 0)
        Me._popUpEdit.Name = "_popUpEdit"
        Me._popUpEdit.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._popUpEdit.Properties.Appearance.Options.UseFont = True
        EditorButtonImageOptions1.Image = Global.Presentation.Controls.My.Resources.Resources.MoreInfo_16x16_blue
        Me._popUpEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", "MORE_INFO", Nothing, DevExpress.Utils.ToolTipAnchor.[Default]), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.OK)})
        Me._popUpEdit.Properties.CloseUpKey = New DevExpress.Utils.KeyShortcut(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                Or System.Windows.Forms.Keys.E))
        Me._popUpEdit.Properties.PopupControl = Me._popUpContainer
        Me._popUpEdit.Properties.PopupFormMinSize = New System.Drawing.Size(1000, 300)
        Me._popUpEdit.Properties.PopupFormSize = New System.Drawing.Size(1000, 300)
        Me._popUpEdit.Properties.ShowPopupCloseButton = False
        Me._popUpEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me._popUpEdit.Size = New System.Drawing.Size(100, 28)
        Me._popUpEdit.TabIndex = 0
        '
        '_popUpContainer
        '
        Me._popUpContainer.Appearance.BackColor = System.Drawing.Color.Transparent
        Me._popUpContainer.Appearance.Options.UseBackColor = True
        Me._popUpContainer.Location = New System.Drawing.Point(0, 30)
        Me._popUpContainer.Margin = New System.Windows.Forms.Padding(0)
        Me._popUpContainer.Name = "_popUpContainer"
        Me._popUpContainer.Size = New System.Drawing.Size(150, 100)
        Me._popUpContainer.TabIndex = 1
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
        'SearchLookUpEditExAdmission
        '
        Me.Appearance.BackColor = System.Drawing.SystemColors.Control
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me._popUpContainer)
        Me.Controls.Add(Me._popUpEdit)
        Me.Controls.Add(Me.PcePopUpEdit)
        Me.Name = "SearchLookUpEditExAdmission"
        Me.Size = New System.Drawing.Size(100, 28)
        CType(Me._popUpEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._popUpContainer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PcePopUpEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents _popUpEdit As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents _popUpContainer As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents PcePopUpEdit As DevExpress.XtraEditors.PopupContainerEdit

End Class
