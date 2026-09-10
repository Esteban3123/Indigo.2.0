<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrButtonEditWithPopUp
    Inherits DevExpress.XtraEditors.XtraUserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CtrButtonEditWithPopUp))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.BteButtonEdit = New DevExpress.XtraEditors.ButtonEdit()
        Me.PcePopUpEdit = New DevExpress.XtraEditors.PopupContainerEdit()
        CType(Me.BteButtonEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PcePopUpEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BteButtonEdit
        '
        Me.BteButtonEdit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BteButtonEdit.Location = New System.Drawing.Point(0, 0)
        Me.BteButtonEdit.Margin = New System.Windows.Forms.Padding(0)
        Me.BteButtonEdit.Name = "BteButtonEdit"
        Me.BteButtonEdit.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.BteButtonEdit.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.BteButtonEdit.Properties.Appearance.Options.UseBackColor = True
        Me.BteButtonEdit.Properties.Appearance.Options.UseFont = True
        Me.BteButtonEdit.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BteButtonEdit.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.BteButtonEdit.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.BteButtonEdit.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.BteButtonEdit.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.BteButtonEdit.Properties.AppearanceFocused.Options.UseFont = True
        Me.BteButtonEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, CType(resources.GetObject("BteButtonEdit.Properties.Buttons"), System.Drawing.Image), New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F3), SerializableAppearanceObject2, "", Nothing, Nothing, True)})
        Me.BteButtonEdit.Size = New System.Drawing.Size(100, 28)
        Me.BteButtonEdit.TabIndex = 0
        '
        'PcePopUpEdit
        '
        Me.PcePopUpEdit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PcePopUpEdit.Location = New System.Drawing.Point(0, 0)
        Me.PcePopUpEdit.Name = "PcePopUpEdit"
        Me.PcePopUpEdit.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.PcePopUpEdit.Properties.Appearance.Options.UseFont = True
        Me.PcePopUpEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.PcePopUpEdit.Properties.PopupSizeable = False
        Me.PcePopUpEdit.Properties.ShowPopupCloseButton = False
        Me.PcePopUpEdit.Properties.ShowPopupShadow = False
        Me.PcePopUpEdit.Size = New System.Drawing.Size(100, 28)
        Me.PcePopUpEdit.TabIndex = 1
        '
        'CtrButtonEditWithPopUp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.BteButtonEdit)
        Me.Controls.Add(Me.PcePopUpEdit)
        Me.Margin = New System.Windows.Forms.Padding(0)
        Me.MaximumSize = New System.Drawing.Size(0, 28)
        Me.MinimumSize = New System.Drawing.Size(100, 28)
        Me.Name = "CtrButtonEditWithPopUp"
        Me.Size = New System.Drawing.Size(100, 28)
        CType(Me.BteButtonEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PcePopUpEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BteButtonEdit As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents PcePopUpEdit As DevExpress.XtraEditors.PopupContainerEdit

End Class
