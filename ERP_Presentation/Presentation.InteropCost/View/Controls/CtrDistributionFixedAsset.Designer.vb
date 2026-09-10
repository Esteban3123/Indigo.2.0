<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrDistributionFixedAsset
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
        Me.LblName = New DevExpress.XtraEditors.LabelControl()
        Me.LblDeprecationValue = New DevExpress.XtraEditors.LabelControl()
        Me.LblMonth = New DevExpress.XtraEditors.LabelControl()
        Me.LblYear = New DevExpress.XtraEditors.LabelControl()
        Me.SuspendLayout()
        '
        'LblName
        '
        Me.LblName.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 13.0!, System.Drawing.FontStyle.Bold)
        Me.LblName.Appearance.ForeColor = System.Drawing.Color.White
        Me.LblName.Location = New System.Drawing.Point(3, 8)
        Me.LblName.Name = "LblName"
        Me.LblName.Size = New System.Drawing.Size(128, 23)
        Me.LblName.TabIndex = 0
        Me.LblName.Text = "V. Depreciación"
        '
        'LblDeprecationValue
        '
        Me.LblDeprecationValue.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.LblDeprecationValue.Appearance.ForeColor = System.Drawing.Color.White
        Me.LblDeprecationValue.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LblDeprecationValue.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.LblDeprecationValue.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LblDeprecationValue.Location = New System.Drawing.Point(137, 9)
        Me.LblDeprecationValue.Name = "LblDeprecationValue"
        Me.LblDeprecationValue.Size = New System.Drawing.Size(152, 21)
        Me.LblDeprecationValue.TabIndex = 1
        Me.LblDeprecationValue.Text = "$0"
        '
        'LblMonth
        '
        Me.LblMonth.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.5!)
        Me.LblMonth.Appearance.ForeColor = System.Drawing.Color.White
        Me.LblMonth.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LblMonth.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LblMonth.Location = New System.Drawing.Point(43, 38)
        Me.LblMonth.Name = "LblMonth"
        Me.LblMonth.Size = New System.Drawing.Size(95, 22)
        Me.LblMonth.TabIndex = 2
        Me.LblMonth.Text = "Enero"
        '
        'LblYear
        '
        Me.LblYear.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.5!)
        Me.LblYear.Appearance.ForeColor = System.Drawing.Color.White
        Me.LblYear.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LblYear.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LblYear.Location = New System.Drawing.Point(161, 38)
        Me.LblYear.Name = "LblYear"
        Me.LblYear.Size = New System.Drawing.Size(86, 22)
        Me.LblYear.TabIndex = 3
        Me.LblYear.Text = "2016"
        '
        'CtrDistributionFixedAsset
        '
        Me.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LblYear)
        Me.Controls.Add(Me.LblMonth)
        Me.Controls.Add(Me.LblDeprecationValue)
        Me.Controls.Add(Me.LblName)
        Me.Margin = New System.Windows.Forms.Padding(0)
        Me.MaximumSize = New System.Drawing.Size(292, 62)
        Me.MinimumSize = New System.Drawing.Size(292, 62)
        Me.Name = "CtrDistributionFixedAsset"
        Me.Size = New System.Drawing.Size(292, 62)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LblName As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LblDeprecationValue As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LblMonth As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LblYear As DevExpress.XtraEditors.LabelControl

End Class
