<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CtrMinimizedCampaign
    Inherits DevExpress.XtraEditors.XtraUserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim SuperToolTip1 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
        Dim ToolTipTitleItem1 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
        Dim ToolTipItem1 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
        Me.LblInvoice = New DevExpress.XtraEditors.LabelControl()
        Me.SuspendLayout()
        '
        'LblInvoice
        '
        Me.LblInvoice.Appearance.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.LblInvoice.Appearance.ForeColor = System.Drawing.Color.White
        Me.LblInvoice.Appearance.Options.UseFont = True
        Me.LblInvoice.Appearance.Options.UseForeColor = True
        Me.LblInvoice.Appearance.Options.UseTextOptions = True
        Me.LblInvoice.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LblInvoice.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Show
        Me.LblInvoice.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.LblInvoice.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.LblInvoice.AppearanceDisabled.Font = New System.Drawing.Font("Tahoma", 7.25!)
        Me.LblInvoice.AppearanceDisabled.Options.UseFont = True
        Me.LblInvoice.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LblInvoice.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblInvoice.Location = New System.Drawing.Point(0, 0)
        Me.LblInvoice.Name = "LblInvoice"
        Me.LblInvoice.Size = New System.Drawing.Size(115, 26)
        ToolTipTitleItem1.Text = "Factura #"
        ToolTipItem1.LeftIndent = 6
        ToolTipItem1.Text = "Total Entidad {0}" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Total Paciente {1}"
        SuperToolTip1.Items.Add(ToolTipTitleItem1)
        SuperToolTip1.Items.Add(ToolTipItem1)
        Me.LblInvoice.SuperTip = SuperToolTip1
        Me.LblInvoice.TabIndex = 0
        Me.LblInvoice.Text = "Campaña # "
        '
        'CtrMinimizedCampaign
        '
        Me.Appearance.BackColor = System.Drawing.Color.Green
        Me.Appearance.BorderColor = System.Drawing.Color.White
        Me.Appearance.Options.UseBackColor = True
        Me.Appearance.Options.UseBorderColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LblInvoice)
        Me.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Name = "CtrMinimizedCampaign"
        Me.Size = New System.Drawing.Size(115, 26)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LblInvoice As DevExpress.XtraEditors.LabelControl

End Class
