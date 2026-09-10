<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrRssItem
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
        Me.INDlblTitle = New DevExpress.XtraEditors.LabelControl()
        Me.INDlblResume = New DevExpress.XtraEditors.LabelControl()
        Me.INDpeImage = New DevExpress.XtraEditors.PictureEdit()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.INDtxtNews = New DevExpress.XtraEditors.MemoExEdit()
        Me.INDbtnLink = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.INDpeImage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.INDtxtNews.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDlblTitle
        '
        Me.INDlblTitle.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlblTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.INDlblTitle.Location = New System.Drawing.Point(0, 5)
        Me.INDlblTitle.Name = "INDlblTitle"
        Me.INDlblTitle.Padding = New System.Windows.Forms.Padding(10, 0, 10, 0)
        Me.INDlblTitle.Size = New System.Drawing.Size(56, 30)
        Me.INDlblTitle.TabIndex = 0
        Me.INDlblTitle.Text = "Title"
        '
        'INDlblResume
        '
        Me.INDlblResume.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlblResume.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.INDlblResume.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.INDlblResume.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.INDlblResume.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.INDlblResume.Location = New System.Drawing.Point(0, 147)
        Me.INDlblResume.Name = "INDlblResume"
        Me.INDlblResume.Padding = New System.Windows.Forms.Padding(10, 0, 40, 0)
        Me.INDlblResume.Size = New System.Drawing.Size(398, 42)
        Me.INDlblResume.TabIndex = 1
        Me.INDlblResume.Text = "Resume"
        '
        'INDpeImage
        '
        Me.INDpeImage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDpeImage.Location = New System.Drawing.Point(0, 35)
        Me.INDpeImage.Name = "INDpeImage"
        Me.INDpeImage.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpeImage.Size = New System.Drawing.Size(398, 112)
        Me.INDpeImage.TabIndex = 2
        '
        'PanelControl1
        '
        Me.PanelControl1.Appearance.BackColor = System.Drawing.Color.White
        Me.PanelControl1.Appearance.Options.UseBackColor = True
        Me.PanelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PanelControl1.Controls.Add(Me.INDbtnLink)
        Me.PanelControl1.Controls.Add(Me.INDpeImage)
        Me.PanelControl1.Controls.Add(Me.INDlblResume)
        Me.PanelControl1.Controls.Add(Me.INDlblTitle)
        Me.PanelControl1.Controls.Add(Me.INDtxtNews)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl1.Location = New System.Drawing.Point(1, 1)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Padding = New System.Windows.Forms.Padding(0, 5, 0, 5)
        Me.PanelControl1.Size = New System.Drawing.Size(398, 194)
        Me.PanelControl1.TabIndex = 3
        '
        'INDtxtNews
        '
        Me.INDtxtNews.Location = New System.Drawing.Point(3, 208)
        Me.INDtxtNews.Name = "INDtxtNews"
        Me.INDtxtNews.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDtxtNews.Size = New System.Drawing.Size(507, 18)
        Me.INDtxtNews.TabIndex = 3
        '
        'INDbtnLink
        '
        Me.INDbtnLink.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.INDbtnLink.Appearance.BackColor = System.Drawing.Color.White
        Me.INDbtnLink.Appearance.BackColor2 = System.Drawing.Color.White
        Me.INDbtnLink.Appearance.BorderColor = System.Drawing.Color.White
        Me.INDbtnLink.Appearance.Options.UseBackColor = True
        Me.INDbtnLink.Appearance.Options.UseBorderColor = True
        Me.INDbtnLink.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.INDbtnLink.Image = Global.Presentation.Controls.My.Resources.Resources.suspensivos
        Me.INDbtnLink.Location = New System.Drawing.Point(355, 169)
        Me.INDbtnLink.Name = "INDbtnLink"
        Me.INDbtnLink.Size = New System.Drawing.Size(42, 23)
        Me.INDbtnLink.TabIndex = 4
        Me.INDbtnLink.ToolTip = "Ver noticia completa"
        '
        'CtrRssItem
        '
        Me.Appearance.BackColor = System.Drawing.Color.Silver
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.PanelControl1)
        Me.Name = "CtrRssItem"
        Me.Padding = New System.Windows.Forms.Padding(1, 1, 1, 5)
        Me.Size = New System.Drawing.Size(400, 200)
        CType(Me.INDpeImage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.INDtxtNews.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDlblTitle As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlblResume As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDpeImage As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDtxtNews As DevExpress.XtraEditors.MemoExEdit
    Friend WithEvents INDbtnLink As DevExpress.XtraEditors.SimpleButton

End Class
