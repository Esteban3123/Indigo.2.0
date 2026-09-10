<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrResumptionHoliday
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
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.INDlbValue = New DevExpress.XtraEditors.LabelControl()
        Me.INDlbText = New DevExpress.XtraEditors.LabelControl()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        Me.PanelControl1.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.PanelControl1.Appearance.Options.UseBackColor = True
        Me.PanelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PanelControl1.Controls.Add(Me.INDlbValue)
        Me.PanelControl1.Controls.Add(Me.INDlbText)
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(292, 59)
        Me.PanelControl1.TabIndex = 0
        '
        'INDlbValue
        '
        Me.INDlbValue.Appearance.Font = New System.Drawing.Font("Segoe UI", 20.0!)
        Me.INDlbValue.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDlbValue.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDlbValue.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.INDlbValue.Location = New System.Drawing.Point(3, 21)
        Me.INDlbValue.Name = "INDlbValue"
        Me.INDlbValue.Size = New System.Drawing.Size(277, 37)
        Me.INDlbValue.TabIndex = 1
        Me.INDlbValue.Text = "0"
        '
        'INDlbText
        '
        Me.INDlbText.Appearance.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlbText.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDlbText.Location = New System.Drawing.Point(3, 0)
        Me.INDlbText.Name = "INDlbText"
        Me.INDlbText.Size = New System.Drawing.Size(185, 21)
        Me.INDlbText.TabIndex = 0
        Me.INDlbText.Text = "Días Pendientes Aplazados"
        '
        'CtrResumptionHoliday
        '
        Me.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.PanelControl1)
        Me.Name = "CtrResumptionHoliday"
        Me.Size = New System.Drawing.Size(292, 62)
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDlbText As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlbValue As DevExpress.XtraEditors.LabelControl

End Class
