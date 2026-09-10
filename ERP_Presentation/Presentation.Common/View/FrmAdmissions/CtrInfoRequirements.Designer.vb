<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrInfoRequirements
    Inherits DevExpress.XtraEditors.XtraUserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.INDpceChangeData = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.INDlblCount = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.INDpceChangeData.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.White
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Appearance.Options.UseForeColor = True
        Me.LabelControl1.Location = New System.Drawing.Point(3, 3)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(150, 17)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Plantilla de Requerimientos"
        '
        'INDpceChangeData
        '
        Me.INDpceChangeData.CausesValidation = False
        Me.INDpceChangeData.Cursor = System.Windows.Forms.Cursors.Hand
        Me.INDpceChangeData.EditValue = "Plantilla"
        Me.INDpceChangeData.Location = New System.Drawing.Point(3, 24)
        Me.INDpceChangeData.Name = "INDpceChangeData"
        Me.INDpceChangeData.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDpceChangeData.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI", 14.0!)
        Me.INDpceChangeData.Properties.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDpceChangeData.Properties.Appearance.Options.UseBackColor = True
        Me.INDpceChangeData.Properties.Appearance.Options.UseFont = True
        Me.INDpceChangeData.Properties.Appearance.Options.UseForeColor = True
        Me.INDpceChangeData.Properties.AutoHeight = False
        Me.INDpceChangeData.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpceChangeData.Properties.PopupSizeable = False
        Me.INDpceChangeData.Properties.ShowPopupCloseButton = False
        Me.INDpceChangeData.Properties.ShowPopupShadow = False
        Me.INDpceChangeData.Size = New System.Drawing.Size(286, 35)
        Me.INDpceChangeData.TabIndex = 1
        '
        'INDlblCount
        '
        Me.INDlblCount.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlblCount.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDlblCount.Appearance.Options.UseFont = True
        Me.INDlblCount.Appearance.Options.UseForeColor = True
        Me.INDlblCount.Location = New System.Drawing.Point(264, 0)
        Me.INDlblCount.Name = "INDlblCount"
        Me.INDlblCount.Size = New System.Drawing.Size(8, 21)
        Me.INDlblCount.TabIndex = 2
        Me.INDlblCount.Text = "0"
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.LabelControl2.Appearance.ForeColor = System.Drawing.Color.White
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Appearance.Options.UseForeColor = True
        Me.LabelControl2.Location = New System.Drawing.Point(193, 3)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(56, 17)
        Me.LabelControl2.TabIndex = 3
        Me.LabelControl2.Text = "Cant. Req."
        '
        'CtrInfoRequirements
        '
        Me.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.INDlblCount)
        Me.Controls.Add(Me.INDpceChangeData)
        Me.Controls.Add(Me.LabelControl1)
        Me.Name = "CtrInfoRequirements"
        Me.Size = New System.Drawing.Size(292, 62)
        CType(Me.INDpceChangeData.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDpceChangeData As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents INDlblCount As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl

End Class
