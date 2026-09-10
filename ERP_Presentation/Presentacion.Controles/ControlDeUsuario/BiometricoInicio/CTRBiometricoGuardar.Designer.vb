<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CTRBiometricoGuardar
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CTRBiometricoGuardar))
        Me.AxGrFingerXCtrl1 = New Object
        Me.lstLogHuella = New System.Windows.Forms.ListBox()
        Me.ptbHuella1 = New System.Windows.Forms.PictureBox()
        Me.lbLog = New System.Windows.Forms.ListBox()
        Me.INDpnContenedor = New System.Windows.Forms.Panel()
        Me.INDbtnCerrarDispositivo = New DevExpress.XtraEditors.SimpleButton()
        Me.INDBtnIniciar = New DevExpress.XtraEditors.SimpleButton()
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
        Me.lstLogHuella.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstLogHuella.FormattingEnabled = True
        Me.lstLogHuella.Location = New System.Drawing.Point(0, 1)
        Me.lstLogHuella.Name = "lstLogHuella"
        Me.lstLogHuella.Size = New System.Drawing.Size(170, 17)
        Me.lstLogHuella.TabIndex = 0
        '
        'ptbHuella1
        '
        Me.ptbHuella1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        'Me.ptbHuella1.Image = Global.Presentation.Controls.My.Resources.Resources.biometrico11
        Me.ptbHuella1.Location = New System.Drawing.Point(0, 15)
        Me.ptbHuella1.Name = "ptbHuella1"
        Me.ptbHuella1.Size = New System.Drawing.Size(170, 92)
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
        Me.INDpnContenedor.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.INDpnContenedor.Controls.Add(Me.INDbtnCerrarDispositivo)
        Me.INDpnContenedor.Controls.Add(Me.INDBtnIniciar)
        Me.INDpnContenedor.Controls.Add(Me.ptbHuella1)
        Me.INDpnContenedor.Controls.Add(Me.AxGrFingerXCtrl1)
        Me.INDpnContenedor.Controls.Add(Me.lstLogHuella)
        Me.INDpnContenedor.Location = New System.Drawing.Point(0, 0)
        Me.INDpnContenedor.Name = "INDpnContenedor"
        Me.INDpnContenedor.Size = New System.Drawing.Size(170, 134)
        Me.INDpnContenedor.TabIndex = 2
        '
        'INDbtnCerrarDispositivo
        '
        Me.INDbtnCerrarDispositivo.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.INDbtnCerrarDispositivo.Enabled = False
        Me.INDbtnCerrarDispositivo.Image = Global.Presentation.Controls.My.Resources.Resources.cancel_16
        Me.INDbtnCerrarDispositivo.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.INDbtnCerrarDispositivo.Location = New System.Drawing.Point(91, 111)
        Me.INDbtnCerrarDispositivo.Name = "INDbtnCerrarDispositivo"
        Me.INDbtnCerrarDispositivo.Size = New System.Drawing.Size(61, 20)
        Me.INDbtnCerrarDispositivo.TabIndex = 22
        Me.INDbtnCerrarDispositivo.ToolTip = "Detener El Dispositivo"
        Me.INDbtnCerrarDispositivo.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information
        '
        'INDBtnIniciar
        '
        Me.INDBtnIniciar.Anchor = System.Windows.Forms.AnchorStyles.Top
        'Me.INDBtnIniciar.Image = Global.Presentation.Controls.My.Resources.Resources.PLAY16X16
        Me.INDBtnIniciar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.INDBtnIniciar.Location = New System.Drawing.Point(14, 111)
        Me.INDBtnIniciar.Name = "INDBtnIniciar"
        Me.INDBtnIniciar.Size = New System.Drawing.Size(61, 20)
        Me.INDBtnIniciar.TabIndex = 21
        Me.INDBtnIniciar.ToolTip = "Iniciar El Dispositivo"
        Me.INDBtnIniciar.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information
        '
        'CTRBiometricoGuardar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.INDpnContenedor)
        Me.Name = "CTRBiometricoGuardar"
        Me.Size = New System.Drawing.Size(170, 134)
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
    Friend WithEvents INDbtnCerrarDispositivo As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDBtnIniciar As DevExpress.XtraEditors.SimpleButton
End Class

