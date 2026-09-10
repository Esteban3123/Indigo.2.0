<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmUpdater
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmUpdater))
        Me.INDProgressBar = New System.Windows.Forms.ProgressBar()
        Me.INDlblMensaje1 = New System.Windows.Forms.Label()
        Me.INDlblMensaje2 = New System.Windows.Forms.Label()
        Me.INDBackWorkerArchivos = New System.ComponentModel.BackgroundWorker()
        Me.SuspendLayout()
        '
        'INDProgressBar
        '
        Me.INDProgressBar.Location = New System.Drawing.Point(12, 45)
        Me.INDProgressBar.Name = "INDProgressBar"
        Me.INDProgressBar.Size = New System.Drawing.Size(465, 26)
        Me.INDProgressBar.TabIndex = 0
        '
        'INDlblMensaje1
        '
        Me.INDlblMensaje1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlblMensaje1.Location = New System.Drawing.Point(12, 16)
        Me.INDlblMensaje1.Name = "INDlblMensaje1"
        Me.INDlblMensaje1.Size = New System.Drawing.Size(465, 16)
        Me.INDlblMensaje1.TabIndex = 1
        Me.INDlblMensaje1.Text = "Esta acción puede durar varios segundos."
        Me.INDlblMensaje1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'INDlblMensaje2
        '
        Me.INDlblMensaje2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlblMensaje2.Location = New System.Drawing.Point(12, 74)
        Me.INDlblMensaje2.Name = "INDlblMensaje2"
        Me.INDlblMensaje2.Size = New System.Drawing.Size(465, 43)
        Me.INDlblMensaje2.TabIndex = 2
        Me.INDlblMensaje2.Text = "Copiando..."
        Me.INDlblMensaje2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'INDBackWorkerArchivos
        '
        Me.INDBackWorkerArchivos.WorkerReportsProgress = True
        Me.INDBackWorkerArchivos.WorkerSupportsCancellation = True
        '
        'FrmUpdater
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(489, 143)
        Me.Controls.Add(Me.INDlblMensaje2)
        Me.Controls.Add(Me.INDlblMensaje1)
        Me.Controls.Add(Me.INDProgressBar)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmUpdater"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Actualizando..."
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDProgressBar As System.Windows.Forms.ProgressBar
    Friend WithEvents INDlblMensaje1 As System.Windows.Forms.Label
    Friend WithEvents INDlblMensaje2 As System.Windows.Forms.Label
    Friend WithEvents INDBackWorkerArchivos As System.ComponentModel.BackgroundWorker
End Class
