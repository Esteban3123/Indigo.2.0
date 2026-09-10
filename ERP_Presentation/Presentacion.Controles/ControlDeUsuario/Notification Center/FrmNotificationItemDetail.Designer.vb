<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmNotificationItemDetail
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmNotificationItemDetail))
        Me.PnlButtons = New System.Windows.Forms.Panel()
        Me.BtnClose = New DevExpress.XtraEditors.SimpleButton()
        Me.TxtMessage = New DevExpress.XtraEditors.MemoEdit()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.PnlButtons.SuspendLayout()
        CType(Me.TxtMessage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PnlButtons
        '
        Me.PnlButtons.Controls.Add(Me.BtnClose)
        Me.PnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PnlButtons.Location = New System.Drawing.Point(0, 316)
        Me.PnlButtons.Name = "PnlButtons"
        Me.PnlButtons.Size = New System.Drawing.Size(1008, 45)
        Me.PnlButtons.TabIndex = 0
        '
        'BtnClose
        '
        Me.BtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnClose.Location = New System.Drawing.Point(439, 6)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.BtnClose, False)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(130, 35)
        Me.BtnClose.TabIndex = 0
        Me.BtnClose.Text = "Cerrar"
        '
        'TxtMessage
        '
        Me.TxtMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxtMessage.Location = New System.Drawing.Point(0, 0)
        Me.TxtMessage.Name = "TxtMessage"
        Me.TxtMessage.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMessage.Properties.Appearance.Options.UseFont = True
        Me.TxtMessage.Properties.ReadOnly = True
        Me.TxtMessage.Size = New System.Drawing.Size(1008, 316)
        Me.TxtMessage.TabIndex = 1
        '
        'FrmNotificationItemDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.BtnClose
        Me.ClientSize = New System.Drawing.Size(1008, 361)
        Me.ControlBox = False
        Me.Controls.Add(Me.TxtMessage)
        Me.Controls.Add(Me.PnlButtons)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.IconOptions.SvgImage = CType(resources.GetObject("FrmNotificationItemDetail.IconOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.IconOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.None
        Me.Name = "FrmNotificationItemDetail"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Mensaje detallado"
        Me.PnlButtons.ResumeLayout(False)
        CType(Me.TxtMessage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PnlButtons As Panel
    Friend WithEvents BtnClose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents IndigoSimpleButton1 As IndigoSimpleButton
    Public WithEvents TxtMessage As DevExpress.XtraEditors.MemoEdit
End Class
