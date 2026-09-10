<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrValueLiquidator
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
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.INDpceTotalValues = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.INDlbRete383 = New DevExpress.XtraEditors.LabelControl()
        Me.INDlbValue383 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.INDpceTotalValues.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LabelControl1.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(128, 21)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Retención a Aplicar"
        '
        'INDpceTotalValues
        '
        Me.INDpceTotalValues.CausesValidation = False
        Me.INDpceTotalValues.Cursor = System.Windows.Forms.Cursors.Hand
        Me.INDpceTotalValues.EditValue = "$0"
        Me.INDpceTotalValues.Location = New System.Drawing.Point(3, 15)
        Me.INDpceTotalValues.Name = "INDpceTotalValues"
        Me.INDpceTotalValues.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDpceTotalValues.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 14.0!)
        Me.INDpceTotalValues.Properties.Appearance.Options.UseBackColor = True
        Me.INDpceTotalValues.Properties.Appearance.Options.UseFont = True
        Me.INDpceTotalValues.Properties.Appearance.Options.UseTextOptions = True
        Me.INDpceTotalValues.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDpceTotalValues.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpceTotalValues.Properties.PopupSizeable = False
        Me.INDpceTotalValues.Size = New System.Drawing.Size(286, 30)
        Me.INDpceTotalValues.TabIndex = 1
        '
        'INDlbRete383
        '
        Me.INDlbRete383.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 7.0!)
        Me.INDlbRete383.Location = New System.Drawing.Point(4, 46)
        Me.INDlbRete383.Name = "INDlbRete383"
        Me.INDlbRete383.Size = New System.Drawing.Size(37, 12)
        Me.INDlbRete383.TabIndex = 2
        Me.INDlbRete383.Text = "Rete 383:"
        '
        'INDlbValue383
        '
        Me.INDlbValue383.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 8.25!)
        Me.INDlbValue383.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDlbValue383.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.INDlbValue383.Location = New System.Drawing.Point(47, 45)
        Me.INDlbValue383.Name = "INDlbValue383"
        Me.INDlbValue383.Size = New System.Drawing.Size(95, 13)
        Me.INDlbValue383.TabIndex = 4
        Me.INDlbValue383.Text = "$0"
        '
        'CtrValueLiquidator
        '
        Me.Appearance.BackColor = System.Drawing.SystemColors.Control
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.INDlbValue383)
        Me.Controls.Add(Me.INDlbRete383)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.INDpceTotalValues)
        Me.Name = "CtrValueLiquidator"
        Me.Size = New System.Drawing.Size(292, 62)
        CType(Me.INDpceTotalValues.Properties,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDpceTotalValues As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents INDlbRete383 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlbValue383 As DevExpress.XtraEditors.LabelControl

End Class
