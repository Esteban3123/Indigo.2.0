<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrNotificationItem
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
        Me.INDlblTitleMessage = New DevExpress.XtraEditors.LabelControl()
        Me.INDlblDateMessage = New DevExpress.XtraEditors.LabelControl()
        Me.INDlblMessage = New DevExpress.XtraEditors.LabelControl()
        Me.INDpcFirstLine = New DevExpress.XtraEditors.PanelControl()
        Me.INDpcThirdLine = New DevExpress.XtraEditors.PanelControl()
        Me.INDpcWhiteLeft = New DevExpress.XtraEditors.PanelControl()
        Me.INDpcTypeMessage = New DevExpress.XtraEditors.PanelControl()
        Me.INDpcBlackButtom = New DevExpress.XtraEditors.PanelControl()
        Me.INDpc = New DevExpress.XtraEditors.PanelControl()
        CType(Me.INDpcFirstLine, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpcFirstLine.SuspendLayout()
        CType(Me.INDpcThirdLine, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpcThirdLine.SuspendLayout()
        CType(Me.INDpcWhiteLeft, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpcWhiteLeft.SuspendLayout()
        CType(Me.INDpcTypeMessage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpcBlackButtom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDlblTitleMessage
        '
        Me.INDlblTitleMessage.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 14.0!)
        Me.INDlblTitleMessage.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDlblTitleMessage.Appearance.Options.UseFont = True
        Me.INDlblTitleMessage.Appearance.Options.UseForeColor = True
        Me.INDlblTitleMessage.Appearance.Options.UseTextOptions = True
        Me.INDlblTitleMessage.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDlblTitleMessage.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.INDlblTitleMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlblTitleMessage.Location = New System.Drawing.Point(5, 0)
        Me.INDlblTitleMessage.Name = "INDlblTitleMessage"
        Me.INDlblTitleMessage.Padding = New System.Windows.Forms.Padding(3)
        Me.INDlblTitleMessage.Size = New System.Drawing.Size(696, 23)
        Me.INDlblTitleMessage.TabIndex = 0
        '
        'INDlblDateMessage
        '
        Me.INDlblDateMessage.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlblDateMessage.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDlblDateMessage.Appearance.Options.UseFont = True
        Me.INDlblDateMessage.Appearance.Options.UseForeColor = True
        Me.INDlblDateMessage.Appearance.Options.UseTextOptions = True
        Me.INDlblDateMessage.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDlblDateMessage.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Bottom
        Me.INDlblDateMessage.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.INDlblDateMessage.Dock = System.Windows.Forms.DockStyle.Right
        Me.INDlblDateMessage.Location = New System.Drawing.Point(701, 0)
        Me.INDlblDateMessage.Name = "INDlblDateMessage"
        Me.INDlblDateMessage.Padding = New System.Windows.Forms.Padding(0, 0, 10, 0)
        Me.INDlblDateMessage.Size = New System.Drawing.Size(104, 23)
        Me.INDlblDateMessage.TabIndex = 1
        '
        'INDlblMessage
        '
        Me.INDlblMessage.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.INDlblMessage.Appearance.Options.UseFont = True
        Me.INDlblMessage.Appearance.Options.UseTextOptions = True
        Me.INDlblMessage.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.INDlblMessage.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.INDlblMessage.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.INDlblMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlblMessage.Location = New System.Drawing.Point(0, 0)
        Me.INDlblMessage.Name = "INDlblMessage"
        Me.INDlblMessage.Padding = New System.Windows.Forms.Padding(0, 0, 10, 0)
        Me.INDlblMessage.Size = New System.Drawing.Size(800, 47)
        Me.INDlblMessage.TabIndex = 2
        '
        'INDpcFirstLine
        '
        Me.INDpcFirstLine.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDpcFirstLine.Appearance.Options.UseBackColor = True
        Me.INDpcFirstLine.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpcFirstLine.Controls.Add(Me.INDlblTitleMessage)
        Me.INDpcFirstLine.Controls.Add(Me.INDlblDateMessage)
        Me.INDpcFirstLine.Dock = System.Windows.Forms.DockStyle.Top
        Me.INDpcFirstLine.Location = New System.Drawing.Point(0, 5)
        Me.INDpcFirstLine.Name = "INDpcFirstLine"
        Me.INDpcFirstLine.Padding = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.INDpcFirstLine.Size = New System.Drawing.Size(810, 23)
        Me.INDpcFirstLine.TabIndex = 6
        '
        'INDpcThirdLine
        '
        Me.INDpcThirdLine.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpcThirdLine.Controls.Add(Me.INDlblMessage)
        Me.INDpcThirdLine.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDpcThirdLine.Location = New System.Drawing.Point(10, 28)
        Me.INDpcThirdLine.Name = "INDpcThirdLine"
        Me.INDpcThirdLine.Padding = New System.Windows.Forms.Padding(0, 0, 0, 2)
        Me.INDpcThirdLine.Size = New System.Drawing.Size(800, 49)
        Me.INDpcThirdLine.TabIndex = 8
        '
        'INDpcWhiteLeft
        '
        Me.INDpcWhiteLeft.Appearance.BackColor = System.Drawing.Color.White
        Me.INDpcWhiteLeft.Appearance.Options.UseBackColor = True
        Me.INDpcWhiteLeft.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpcWhiteLeft.Controls.Add(Me.INDpcTypeMessage)
        Me.INDpcWhiteLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.INDpcWhiteLeft.Location = New System.Drawing.Point(0, 28)
        Me.INDpcWhiteLeft.Name = "INDpcWhiteLeft"
        Me.INDpcWhiteLeft.Size = New System.Drawing.Size(10, 49)
        Me.INDpcWhiteLeft.TabIndex = 10
        '
        'INDpcTypeMessage
        '
        Me.INDpcTypeMessage.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDpcTypeMessage.Appearance.Options.UseBackColor = True
        Me.INDpcTypeMessage.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpcTypeMessage.Dock = System.Windows.Forms.DockStyle.Left
        Me.INDpcTypeMessage.Location = New System.Drawing.Point(0, 0)
        Me.INDpcTypeMessage.Name = "INDpcTypeMessage"
        Me.INDpcTypeMessage.Size = New System.Drawing.Size(5, 49)
        Me.INDpcTypeMessage.TabIndex = 11
        '
        'INDpcBlackButtom
        '
        Me.INDpcBlackButtom.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.INDpcBlackButtom.Appearance.Options.UseBackColor = True
        Me.INDpcBlackButtom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpcBlackButtom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.INDpcBlackButtom.Location = New System.Drawing.Point(0, 77)
        Me.INDpcBlackButtom.Name = "INDpcBlackButtom"
        Me.INDpcBlackButtom.Size = New System.Drawing.Size(810, 1)
        Me.INDpcBlackButtom.TabIndex = 12
        '
        'INDpc
        '
        Me.INDpc.Appearance.BackColor = System.Drawing.Color.White
        Me.INDpc.Appearance.Options.UseBackColor = True
        Me.INDpc.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpc.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.INDpc.Location = New System.Drawing.Point(0, 78)
        Me.INDpc.Name = "INDpc"
        Me.INDpc.Size = New System.Drawing.Size(810, 2)
        Me.INDpc.TabIndex = 13
        '
        'CtrNotificationItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.INDpcThirdLine)
        Me.Controls.Add(Me.INDpcWhiteLeft)
        Me.Controls.Add(Me.INDpcFirstLine)
        Me.Controls.Add(Me.INDpcBlackButtom)
        Me.Controls.Add(Me.INDpc)
        Me.Name = "CtrNotificationItem"
        Me.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.Size = New System.Drawing.Size(810, 80)
        CType(Me.INDpcFirstLine, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpcFirstLine.ResumeLayout(False)
        CType(Me.INDpcThirdLine, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpcThirdLine.ResumeLayout(False)
        CType(Me.INDpcWhiteLeft, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpcWhiteLeft.ResumeLayout(False)
        CType(Me.INDpcTypeMessage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpcBlackButtom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDlblTitleMessage As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlblDateMessage As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlblMessage As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDpcFirstLine As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDpcThirdLine As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDpcWhiteLeft As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDpcTypeMessage As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDpcBlackButtom As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDpc As DevExpress.XtraEditors.PanelControl

End Class
