Imports DevExpress.XtraEditors

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmPopUpOpcionCups
    Inherits XtraForm

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.INDRgCups = New DevExpress.XtraEditors.RadioGroup()
        Me.INDBtnAceptar = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.INDRgCups.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDRgCups
        '
        Me.INDRgCups.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDRgCups.Location = New System.Drawing.Point(0, 0)
        Me.INDRgCups.Name = "INDRgCups"
        Me.INDRgCups.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDRgCups.Properties.Appearance.Options.UseFont = True
        Me.INDRgCups.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(1, Byte), "CUPS Interconsulta", True, Nothing, "INDRgiInter"), New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(2, Byte), "CUPS Control", True, Nothing, "INDRgiControl"), New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(3, Byte), "CUPS Manejo", True, Nothing, "INDRgiManejo")})
        Me.INDRgCups.Size = New System.Drawing.Size(388, 110)
        Me.INDRgCups.TabIndex = 0
        '
        'INDBtnAceptar
        '
        Me.INDBtnAceptar.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDBtnAceptar.Appearance.Options.UseFont = True
        Me.INDBtnAceptar.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.INDBtnAceptar.Location = New System.Drawing.Point(0, 110)
        Me.INDBtnAceptar.Name = "INDBtnAceptar"
        Me.INDBtnAceptar.Size = New System.Drawing.Size(388, 33)
        Me.INDBtnAceptar.TabIndex = 1
        Me.INDBtnAceptar.Text = "ACEPTAR"
        '
        'FrmPopUpOpcionCups
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(388, 143)
        Me.Controls.Add(Me.INDRgCups)
        Me.Controls.Add(Me.INDBtnAceptar)
        Me.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Shadow
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.IconOptions.ShowIcon = False
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmPopUpOpcionCups"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Tipo CUPS"
        CType(Me.INDRgCups.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents INDRgCups As RadioGroup
    Friend WithEvents INDBtnAceptar As SimpleButton
End Class
