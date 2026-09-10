<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrAppBarButton
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
        Me.INDContent = New DevExpress.XtraEditors.LabelControl()
        Me.INDTextLabel = New DevExpress.XtraEditors.LabelControl()
        Me.INDBackgroundGlyph = New DevExpress.XtraEditors.LabelControl()
        Me.INDBackgroundGlyphP = New DevExpress.XtraEditors.PictureEdit()
        CType(Me.INDBackgroundGlyphP.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDContent
        '
        Me.INDContent.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.INDContent.Appearance.Font = New System.Drawing.Font("Segoe UI Symbol", 18.0!)
        Me.INDContent.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDContent.Location = New System.Drawing.Point(38, 15)
        Me.INDContent.Name = "INDContent"
        Me.INDContent.Size = New System.Drawing.Size(0, 32)
        Me.INDContent.TabIndex = 2
        '
        'INDTextLabel
        '
        Me.INDTextLabel.Appearance.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.INDTextLabel.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDTextLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDTextLabel.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.INDTextLabel.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.INDTextLabel.Location = New System.Drawing.Point(6, 58)
        Me.INDTextLabel.Name = "INDTextLabel"
        Me.INDTextLabel.Size = New System.Drawing.Size(94, 41)
        Me.INDTextLabel.TabIndex = 3
        '
        'INDBackgroundGlyph
        '
        Me.INDBackgroundGlyph.Appearance.Font = New System.Drawing.Font("Segoe UI Symbol", 53.333!)
        Me.INDBackgroundGlyph.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDBackgroundGlyph.Cursor = System.Windows.Forms.Cursors.Hand
        Me.INDBackgroundGlyph.Location = New System.Drawing.Point(22, -19)
        Me.INDBackgroundGlyph.Name = "INDBackgroundGlyph"
        Me.INDBackgroundGlyph.Size = New System.Drawing.Size(0, 94)
        Me.INDBackgroundGlyph.TabIndex = 1
        '
        'INDBackgroundGlyphP
        '
        Me.INDBackgroundGlyphP.EditValue = Global.Presentation.Controls.My.Resources.Resources.icon2
        Me.INDBackgroundGlyphP.Location = New System.Drawing.Point(24, -1)
        Me.INDBackgroundGlyphP.Name = "INDBackgroundGlyphP"
        Me.INDBackgroundGlyphP.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDBackgroundGlyphP.Properties.Appearance.Options.UseBackColor = True
        Me.INDBackgroundGlyphP.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDBackgroundGlyphP.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.INDBackgroundGlyphP.Size = New System.Drawing.Size(55, 67)
        Me.INDBackgroundGlyphP.TabIndex = 4
        Me.INDBackgroundGlyphP.Visible = False
        '
        'CtrAppBarButton
        '
        Me.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.INDContent)
        Me.Controls.Add(Me.INDBackgroundGlyphP)
        Me.Controls.Add(Me.INDTextLabel)
        Me.Controls.Add(Me.INDBackgroundGlyph)
        Me.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Name = "CtrAppBarButton"
        Me.Size = New System.Drawing.Size(103, 106)
        CType(Me.INDBackgroundGlyphP.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents INDContent As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDTextLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDBackgroundGlyph As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDBackgroundGlyphP As DevExpress.XtraEditors.PictureEdit

End Class
