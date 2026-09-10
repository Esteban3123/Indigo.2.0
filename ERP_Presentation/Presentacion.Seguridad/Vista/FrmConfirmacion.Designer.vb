<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmConfirmacion
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
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
        Me.pnlappbar = New System.Windows.Forms.Panel()
        Me.btnno = New System.Windows.Forms.Button()
        Me.btnyes = New System.Windows.Forms.Button()
        Me.lblinfo2 = New System.Windows.Forms.Label()
        Me.lblinfo = New System.Windows.Forms.Label()
        Me.pnlappbar.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlappbar
        '
        Me.pnlappbar.BackColor = System.Drawing.Color.Black
        Me.pnlappbar.Controls.Add(Me.btnno)
        Me.pnlappbar.Controls.Add(Me.btnyes)
        Me.pnlappbar.Controls.Add(Me.lblinfo2)
        Me.pnlappbar.Controls.Add(Me.lblinfo)
        Me.pnlappbar.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlappbar.Location = New System.Drawing.Point(0, 1)
        Me.pnlappbar.Name = "pnlappbar"
        Me.pnlappbar.Size = New System.Drawing.Size(1047, 104)
        Me.pnlappbar.TabIndex = 1
        '
        'btnno
        '
        Me.btnno.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnno.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnno.ForeColor = System.Drawing.Color.White
        Me.btnno.Location = New System.Drawing.Point(909, 33)
        Me.btnno.Name = "btnno"
        Me.btnno.Size = New System.Drawing.Size(120, 45)
        Me.btnno.TabIndex = 5
        Me.btnno.Text = "No"
        Me.btnno.UseVisualStyleBackColor = True
        '
        'btnyes
        '
        Me.btnyes.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnyes.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnyes.ForeColor = System.Drawing.Color.White
        Me.btnyes.Location = New System.Drawing.Point(783, 33)
        Me.btnyes.Name = "btnyes"
        Me.btnyes.Size = New System.Drawing.Size(120, 45)
        Me.btnyes.TabIndex = 4
        Me.btnyes.Text = "Si"
        Me.btnyes.UseVisualStyleBackColor = True
        '
        'lblinfo2
        '
        Me.lblinfo2.AutoSize = True
        Me.lblinfo2.Font = New System.Drawing.Font("Segoe UI Light", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblinfo2.ForeColor = System.Drawing.Color.White
        Me.lblinfo2.Location = New System.Drawing.Point(12, 0)
        Me.lblinfo2.Name = "lblinfo2"
        Me.lblinfo2.Size = New System.Drawing.Size(1026, 30)
        Me.lblinfo2.TabIndex = 3
        Me.lblinfo2.Text = "Esta Accion Cerrara La Session Que Se Encontraba Activa Se Perderan Los Cambios Q" & _
            "ue No Hallan Sido Guardados"
        '
        'lblinfo
        '
        Me.lblinfo.AutoSize = True
        Me.lblinfo.Font = New System.Drawing.Font("Segoe UI Light", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblinfo.ForeColor = System.Drawing.Color.White
        Me.lblinfo.Location = New System.Drawing.Point(12, 30)
        Me.lblinfo.Name = "lblinfo"
        Me.lblinfo.Size = New System.Drawing.Size(167, 30)
        Me.lblinfo.TabIndex = 2
        Me.lblinfo.Text = "Desea Continuar?"
        '
        'FrmConfirmacion
        '
        Me.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1047, 105)
        Me.Controls.Add(Me.pnlappbar)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "FrmConfirmacion"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "FrmConfirmacion"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlappbar.ResumeLayout(False)
        Me.pnlappbar.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlappbar As System.Windows.Forms.Panel
    Friend WithEvents btnno As System.Windows.Forms.Button
    Friend WithEvents btnyes As System.Windows.Forms.Button
    Friend WithEvents lblinfo2 As System.Windows.Forms.Label
    Friend WithEvents lblinfo As System.Windows.Forms.Label
End Class
