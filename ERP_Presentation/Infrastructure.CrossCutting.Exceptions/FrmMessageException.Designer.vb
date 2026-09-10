<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMessageException
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMessageException))
        Me.INDpceLogo = New DevExpress.XtraEditors.PictureEdit()
        Me.INDbtnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.INDbtnDetails = New DevExpress.XtraEditors.SimpleButton()
        Me.INDlblMessage = New DevExpress.XtraEditors.LabelControl()
        CType(Me.INDpceLogo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDpceLogo
        '
        resources.ApplyResources(Me.INDpceLogo, "INDpceLogo")
        Me.INDpceLogo.Name = "INDpceLogo"
        Me.INDpceLogo.Properties.Appearance.BackColor = CType(resources.GetObject("INDpceLogo.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDpceLogo.Properties.Appearance.Options.UseBackColor = True
        Me.INDpceLogo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpceLogo.Properties.ShowMenu = False
        Me.INDpceLogo.Properties.ShowZoomSubMenu = DevExpress.Utils.DefaultBoolean.[False]
        '
        'INDbtnCancel
        '
        Me.INDbtnCancel.AllowFocus = False
        resources.ApplyResources(Me.INDbtnCancel, "INDbtnCancel")
        Me.INDbtnCancel.Appearance.Font = CType(resources.GetObject("INDbtnCancel.Appearance.Font"), System.Drawing.Font)
        Me.INDbtnCancel.Appearance.Options.UseFont = True
        Me.INDbtnCancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.INDbtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.INDbtnCancel.Name = "INDbtnCancel"
        '
        'INDbtnDetails
        '
        Me.INDbtnDetails.AllowFocus = False
        resources.ApplyResources(Me.INDbtnDetails, "INDbtnDetails")
        Me.INDbtnDetails.Appearance.Font = CType(resources.GetObject("INDbtnDetails.Appearance.Font"), System.Drawing.Font)
        Me.INDbtnDetails.Appearance.Options.UseFont = True
        Me.INDbtnDetails.Cursor = System.Windows.Forms.Cursors.Hand
        Me.INDbtnDetails.Name = "INDbtnDetails"
        Me.INDbtnDetails.Tag = "0"
        '
        'INDlblMessage
        '
        resources.ApplyResources(Me.INDlblMessage, "INDlblMessage")
        Me.INDlblMessage.Appearance.Font = CType(resources.GetObject("INDlblMessage.Appearance.Font"), System.Drawing.Font)
        Me.INDlblMessage.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.INDlblMessage.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.INDlblMessage.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDlblMessage.Name = "INDlblMessage"
        '
        'FrmMessageException
        '
        Me.AcceptButton = Me.INDbtnCancel
        Me.Appearance.BackColor = CType(resources.GetObject("FrmMessageException.Appearance.BackColor"), System.Drawing.Color)
        Me.Appearance.Options.UseBackColor = True
        Me.Appearance.Options.UseFont = True
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.INDbtnCancel
        Me.Controls.Add(Me.INDlblMessage)
        Me.Controls.Add(Me.INDbtnDetails)
        Me.Controls.Add(Me.INDbtnCancel)
        Me.Controls.Add(Me.INDpceLogo)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "FrmMessageException"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        CType(Me.INDpceLogo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDpceLogo As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents INDbtnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDbtnDetails As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDlblMessage As DevExpress.XtraEditors.LabelControl
End Class
