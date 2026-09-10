<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrConversationItem
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
        Me.INDpcTop = New DevExpress.XtraEditors.PanelControl()
        Me.INDlblName = New DevExpress.XtraEditors.LabelControl()
        Me.INDlblDateMessage = New DevExpress.XtraEditors.LabelControl()
        Me.INDrchMeesage = New DevExpress.XtraRichEdit.RichEditControl()
        CType(Me.INDpcTop, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpcTop.SuspendLayout()
        Me.SuspendLayout()
        '
        'INDpcTop
        '
        Me.INDpcTop.Appearance.BackColor = System.Drawing.Color.White
        Me.INDpcTop.Appearance.Options.UseBackColor = True
        Me.INDpcTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpcTop.Controls.Add(Me.INDlblName)
        Me.INDpcTop.Controls.Add(Me.INDlblDateMessage)
        Me.INDpcTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.INDpcTop.FireScrollEventOnMouseWheel = True
        Me.INDpcTop.Location = New System.Drawing.Point(0, 5)
        Me.INDpcTop.Name = "INDpcTop"
        Me.INDpcTop.Padding = New System.Windows.Forms.Padding(0, 0, 0, 5)
        Me.INDpcTop.Size = New System.Drawing.Size(325, 22)
        Me.INDpcTop.TabIndex = 4
        '
        'INDlblName
        '
        Me.INDlblName.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlblName.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.INDlblName.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Bottom
        Me.INDlblName.Dock = System.Windows.Forms.DockStyle.Top
        Me.INDlblName.Location = New System.Drawing.Point(0, 0)
        Me.INDlblName.Name = "INDlblName"
        Me.INDlblName.Size = New System.Drawing.Size(0, 20)
        Me.INDlblName.TabIndex = 1
        '
        'INDlblDateMessage
        '
        Me.INDlblDateMessage.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.INDlblDateMessage.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 8.0!)
        Me.INDlblDateMessage.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDlblDateMessage.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.INDlblDateMessage.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.INDlblDateMessage.Location = New System.Drawing.Point(141, 0)
        Me.INDlblDateMessage.Name = "INDlblDateMessage"
        Me.INDlblDateMessage.Size = New System.Drawing.Size(184, 13)
        Me.INDlblDateMessage.TabIndex = 0
        '
        'INDrchMeesage
        '
        Me.INDrchMeesage.ActiveViewType = DevExpress.XtraRichEdit.RichEditViewType.Simple
        Me.INDrchMeesage.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDrchMeesage.Dock = System.Windows.Forms.DockStyle.Top
        Me.INDrchMeesage.Location = New System.Drawing.Point(0, 27)
        Me.INDrchMeesage.Name = "INDrchMeesage"
        Me.INDrchMeesage.Options.Fields.UseCurrentCultureDateTimeFormat = False
        Me.INDrchMeesage.Options.MailMerge.KeepLastParagraph = False
        Me.INDrchMeesage.Options.VerticalScrollbar.Visibility = DevExpress.XtraRichEdit.RichEditScrollbarVisibility.Hidden
        Me.INDrchMeesage.ReadOnly = True
        Me.INDrchMeesage.Size = New System.Drawing.Size(325, 23)
        Me.INDrchMeesage.TabIndex = 6
        '
        'CtrConversationItem
        '
        Me.Appearance.BackColor = System.Drawing.Color.White
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.INDrchMeesage)
        Me.Controls.Add(Me.INDpcTop)
        Me.Name = "CtrConversationItem"
        Me.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.Size = New System.Drawing.Size(325, 50)
        CType(Me.INDpcTop, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpcTop.ResumeLayout(False)
        Me.INDpcTop.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDpcTop As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDlblName As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlblDateMessage As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDrchMeesage As DevExpress.XtraRichEdit.RichEditControl

End Class
