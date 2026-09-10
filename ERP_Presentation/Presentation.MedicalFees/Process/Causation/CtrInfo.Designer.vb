<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrInfo
    Inherits System.Windows.Forms.UserControl

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
        Me.INDlyItemAdmissionNumber = New DevExpress.XtraEditors.LabelControl()
        Me.INDlbAdmissionNumber = New DevExpress.XtraEditors.LabelControl()
        Me.INDpcePacient = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.INDlyItemEntity = New DevExpress.XtraEditors.LabelControl()
        Me.INDlbEntity = New DevExpress.XtraEditors.LabelControl()
        CType(Me.INDpcePacient.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDlyItemAdmissionNumber
        '
        Me.INDlyItemAdmissionNumber.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.0!)
        Me.INDlyItemAdmissionNumber.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDlyItemAdmissionNumber.Appearance.Options.UseFont = True
        Me.INDlyItemAdmissionNumber.Appearance.Options.UseForeColor = True
        Me.INDlyItemAdmissionNumber.Location = New System.Drawing.Point(3, 2)
        Me.INDlyItemAdmissionNumber.Name = "INDlyItemAdmissionNumber"
        Me.INDlyItemAdmissionNumber.Size = New System.Drawing.Size(63, 15)
        Me.INDlyItemAdmissionNumber.TabIndex = 0
        Me.INDlyItemAdmissionNumber.Text = "No. Ingreso:"
        '
        'INDlbAdmissionNumber
        '
        Me.INDlbAdmissionNumber.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.0!)
        Me.INDlbAdmissionNumber.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDlbAdmissionNumber.Appearance.Options.UseFont = True
        Me.INDlbAdmissionNumber.Appearance.Options.UseForeColor = True
        Me.INDlbAdmissionNumber.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.INDlbAdmissionNumber.Location = New System.Drawing.Point(72, -1)
        Me.INDlbAdmissionNumber.Name = "INDlbAdmissionNumber"
        Me.INDlbAdmissionNumber.Size = New System.Drawing.Size(217, 21)
        Me.INDlbAdmissionNumber.TabIndex = 1
        Me.INDlbAdmissionNumber.Text = "Ingreso"
        '
        'INDpcePacient
        '
        Me.INDpcePacient.Cursor = System.Windows.Forms.Cursors.Hand
        Me.INDpcePacient.EditValue = "Paciente"
        Me.INDpcePacient.Location = New System.Drawing.Point(1, 18)
        Me.INDpcePacient.Name = "INDpcePacient"
        Me.INDpcePacient.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDpcePacient.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.INDpcePacient.Properties.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDpcePacient.Properties.Appearance.Options.UseBackColor = True
        Me.INDpcePacient.Properties.Appearance.Options.UseFont = True
        Me.INDpcePacient.Properties.Appearance.Options.UseForeColor = True
        Me.INDpcePacient.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpcePacient.Properties.PopupSizeable = False
        Me.INDpcePacient.Size = New System.Drawing.Size(289, 24)
        Me.INDpcePacient.TabIndex = 2
        '
        'INDlyItemEntity
        '
        Me.INDlyItemEntity.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.0!)
        Me.INDlyItemEntity.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDlyItemEntity.Appearance.Options.UseFont = True
        Me.INDlyItemEntity.Appearance.Options.UseForeColor = True
        Me.INDlyItemEntity.Location = New System.Drawing.Point(4, 45)
        Me.INDlyItemEntity.Name = "INDlyItemEntity"
        Me.INDlyItemEntity.Size = New System.Drawing.Size(41, 15)
        Me.INDlyItemEntity.TabIndex = 3
        Me.INDlyItemEntity.Text = "Entidad:"
        '
        'INDlbEntity
        '
        Me.INDlbEntity.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.0!)
        Me.INDlbEntity.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDlbEntity.Appearance.Options.UseFont = True
        Me.INDlbEntity.Appearance.Options.UseForeColor = True
        Me.INDlbEntity.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.INDlbEntity.Location = New System.Drawing.Point(54, 45)
        Me.INDlbEntity.Name = "INDlbEntity"
        Me.INDlbEntity.Size = New System.Drawing.Size(235, 15)
        Me.INDlbEntity.TabIndex = 4
        Me.INDlbEntity.Text = "Entidad"
        '
        'CtrInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.INDlbEntity)
        Me.Controls.Add(Me.INDlyItemEntity)
        Me.Controls.Add(Me.INDpcePacient)
        Me.Controls.Add(Me.INDlbAdmissionNumber)
        Me.Controls.Add(Me.INDlyItemAdmissionNumber)
        Me.Name = "CtrInfo"
        Me.Size = New System.Drawing.Size(292, 62)
        CType(Me.INDpcePacient.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents INDlyItemAdmissionNumber As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlbAdmissionNumber As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDpcePacient As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents INDlyItemEntity As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlbEntity As DevExpress.XtraEditors.LabelControl

End Class
