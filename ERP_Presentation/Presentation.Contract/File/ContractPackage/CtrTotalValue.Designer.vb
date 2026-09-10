<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CtrTotalValue
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
        Me.components = New System.ComponentModel.Container()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDlblTotalValue = New DevExpress.XtraEditors.LabelControl()
        Me.INDlblTxt = New DevExpress.XtraEditors.LabelControl()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.BehaviorManager1 = New DevExpress.Utils.Behaviors.BehaviorManager(Me.components)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BehaviorManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDlblTotalValue)
        Me.LayoutControl1.Controls.Add(Me.INDlblTxt)
        Me.LayoutControl1.Location = New System.Drawing.Point(2, 1)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.Root
        Me.LayoutControl1.Size = New System.Drawing.Size(309, 80)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDlblTotalValue
        '
        Me.INDlblTotalValue.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 15.0!, System.Drawing.FontStyle.Bold)
        Me.INDlblTotalValue.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDlblTotalValue.Appearance.Options.UseFont = True
        Me.INDlblTotalValue.Appearance.Options.UseForeColor = True
        Me.INDlblTotalValue.Location = New System.Drawing.Point(185, 2)
        Me.INDlblTotalValue.Name = "INDlblTotalValue"
        Me.INDlblTotalValue.Size = New System.Drawing.Size(122, 76)
        Me.INDlblTotalValue.StyleController = Me.LayoutControl1
        Me.INDlblTotalValue.TabIndex = 5
        Me.INDlblTotalValue.Text = "$ 0"
        '
        'INDlblTxt
        '
        Me.INDlblTxt.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.INDlblTxt.Appearance.Font = New System.Drawing.Font("Microsoft YaHei UI", 15.0!, System.Drawing.FontStyle.Bold)
        Me.INDlblTxt.Appearance.Options.UseFont = True
        Me.INDlblTxt.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
        Me.INDlblTxt.Location = New System.Drawing.Point(2, 2)
        Me.INDlblTxt.Margin = New System.Windows.Forms.Padding(0)
        Me.INDlblTxt.Name = "INDlblTxt"
        Me.INDlblTxt.Size = New System.Drawing.Size(179, 76)
        Me.INDlblTxt.StyleController = Me.LayoutControl1
        Me.INDlblTxt.TabIndex = 4
        Me.INDlblTxt.Text = "Valor elementos del paquete"
        '
        'Root
        '
        Me.Root.BestFitWeight = 50
        Me.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.Root.GroupBordersVisible = False
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2})
        Me.Root.Name = "Root"
        Me.Root.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.Root.Size = New System.Drawing.Size(309, 80)
        Me.Root.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDlblTxt
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(26, 58)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(183, 80)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDlblTotalValue
        Me.LayoutControlItem2.Location = New System.Drawing.Point(183, 0)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(32, 32)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(126, 80)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'CtrTotalValue
        '
        Me.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LayoutControl1)
        Me.MaximumSize = New System.Drawing.Size(312, 143)
        Me.MinimumSize = New System.Drawing.Size(312, 143)
        Me.Name = "CtrTotalValue"
        Me.Size = New System.Drawing.Size(312, 143)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BehaviorManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlblTxt As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlblTotalValue As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents BehaviorManager1 As DevExpress.Utils.Behaviors.BehaviorManager
End Class
