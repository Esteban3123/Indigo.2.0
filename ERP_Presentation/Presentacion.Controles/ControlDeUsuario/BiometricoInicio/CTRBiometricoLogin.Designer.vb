<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CTRBiometricoLogin
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CTRBiometricoLogin))
        Me.AxGrFingerXCtrl1 = New Object
        Me.lstLogHuella = New System.Windows.Forms.ListBox()
        Me.ptbHuella1 = New System.Windows.Forms.PictureBox()
        Me.lbLog = New System.Windows.Forms.ListBox()
        Me.INDpnContenedor = New System.Windows.Forms.Panel()
        CType(Me.AxGrFingerXCtrl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ptbHuella1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpnContenedor.SuspendLayout()
        Me.SuspendLayout()
        '
        'AxGrFingerXCtrl1
        '
        Me.AxGrFingerXCtrl1.Enabled = True
        Me.AxGrFingerXCtrl1.Location = New System.Drawing.Point(487, 10)
        Me.AxGrFingerXCtrl1.Name = "AxGrFingerXCtrl1"
        Me.AxGrFingerXCtrl1.OcxState = CType(resources.GetObject("AxGrFingerXCtrl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxGrFingerXCtrl1.Size = New System.Drawing.Size(32, 32)
        Me.AxGrFingerXCtrl1.TabIndex = 20
        '
        'lstLogHuella
        '
        Me.lstLogHuella.FormattingEnabled = True
        Me.lstLogHuella.Location = New System.Drawing.Point(3, 3)
        Me.lstLogHuella.Name = "lstLogHuella"
        Me.lstLogHuella.Size = New System.Drawing.Size(44, 4)
        Me.lstLogHuella.TabIndex = 0
        Me.lstLogHuella.Visible = False
        '
        'ptbHuella1
        '
        'Me.ptbHuella1.Image = Global.Presentation.Controls.My.Resources.Resources.biometrico11
        Me.ptbHuella1.Location = New System.Drawing.Point(-3, -3)
        Me.ptbHuella1.Name = "ptbHuella1"
        Me.ptbHuella1.Size = New System.Drawing.Size(54, 77)
        Me.ptbHuella1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.ptbHuella1.TabIndex = 1
        Me.ptbHuella1.TabStop = False
        '
        'lbLog
        '
        Me.lbLog.FormattingEnabled = True
        Me.lbLog.Location = New System.Drawing.Point(0, 0)
        Me.lbLog.Name = "lbLog"
        Me.lbLog.Size = New System.Drawing.Size(150, 30)
        Me.lbLog.TabIndex = 0
        '
        'INDpnContenedor
        '
        Me.INDpnContenedor.Controls.Add(Me.ptbHuella1)
        Me.INDpnContenedor.Controls.Add(Me.AxGrFingerXCtrl1)
        Me.INDpnContenedor.Controls.Add(Me.lstLogHuella)
        Me.INDpnContenedor.Location = New System.Drawing.Point(0, 0)
        Me.INDpnContenedor.Name = "INDpnContenedor"
        Me.INDpnContenedor.Size = New System.Drawing.Size(54, 77)
        Me.INDpnContenedor.TabIndex = 2
        '
        'CTRBiometricoLogin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.INDpnContenedor)
        Me.Name = "CTRBiometricoLogin"
        Me.Size = New System.Drawing.Size(54, 77)
        CType(Me.AxGrFingerXCtrl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ptbHuella1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpnContenedor.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lstLogHuella As System.Windows.Forms.ListBox
    Friend WithEvents ptbHuella1 As System.Windows.Forms.PictureBox
    Friend WithEvents lbLog As System.Windows.Forms.ListBox
    Friend WithEvents INDpnContenedor As System.Windows.Forms.Panel
    Friend WithEvents AxGrFingerXCtrl1 As Object
End Class
