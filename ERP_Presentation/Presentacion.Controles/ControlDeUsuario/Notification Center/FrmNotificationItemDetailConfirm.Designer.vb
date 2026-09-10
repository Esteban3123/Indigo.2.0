<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmNotificationItemDetailConfirm
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmNotificationItemDetailConfirm))
        Me.PnlButtons = New System.Windows.Forms.Panel()
        Me.BtnAccept = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.TxtMessage = New DevExpress.XtraEditors.MemoEdit()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.PnlButtons.SuspendLayout()
        CType(Me.TxtMessage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PnlButtons
        '
        Me.PnlButtons.Controls.Add(Me.BtnAccept)
        Me.PnlButtons.Controls.Add(Me.BtnCancel)
        Me.PnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PnlButtons.Location = New System.Drawing.Point(0, 316)
        Me.PnlButtons.Name = "PnlButtons"
        Me.PnlButtons.Size = New System.Drawing.Size(1008, 45)
        Me.PnlButtons.TabIndex = 0
        '
        'BtnAccept
        '
        Me.BtnAccept.Location = New System.Drawing.Point(354, 4)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.BtnAccept, False)
        Me.BtnAccept.Name = "BtnAccept"
        Me.BtnAccept.Size = New System.Drawing.Size(130, 35)
        Me.BtnAccept.TabIndex = 1
        Me.BtnAccept.Text = "Aceptar"
        '
        'BtnCancel
        '
        Me.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnCancel.Location = New System.Drawing.Point(520, 4)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.BtnCancel, False)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(130, 35)
        Me.BtnCancel.TabIndex = 0
        Me.BtnCancel.Text = "Cancelar"
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
        'FrmNotificationItemDetailConfirm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.BtnCancel
        Me.ClientSize = New System.Drawing.Size(1008, 361)
        Me.ControlBox = False
        Me.Controls.Add(Me.TxtMessage)
        Me.Controls.Add(Me.PnlButtons)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.IconOptions.SvgImage = CType(resources.GetObject("FrmNotificationItemDetailConfirm.IconOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.IconOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.None
        Me.Name = "FrmNotificationItemDetailConfirm"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Mensaje detallado"
        Me.PnlButtons.ResumeLayout(False)
        CType(Me.TxtMessage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PnlButtons As Panel
    Friend WithEvents BtnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents IndigoSimpleButton1 As IndigoSimpleButton
    Public WithEvents TxtMessage As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents BtnAccept As DevExpress.XtraEditors.SimpleButton
End Class
