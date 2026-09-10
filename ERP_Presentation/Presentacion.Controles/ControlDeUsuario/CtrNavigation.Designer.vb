<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrNavigation
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
        Me.INDpeBack = New DevExpress.XtraEditors.PictureEdit()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygGroup = New DevExpress.XtraLayout.LayoutControlGroup()
        CType(Me.INDpeBack.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDpeBack
        '
        Me.INDpeBack.EditValue = Global.Presentation.Controls.My.Resources.Resources.Arrow1
        Me.INDpeBack.Location = New System.Drawing.Point(0, 12)
        Me.INDpeBack.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDpeBack.Name = "INDpeBack"
        Me.INDpeBack.Properties.AllowFocused = False
        Me.INDpeBack.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDpeBack.Properties.Appearance.Options.UseBackColor = True
        Me.INDpeBack.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpeBack.Properties.ReadOnly = True
        Me.INDpeBack.Properties.ShowMenu = False
        Me.INDpeBack.Size = New System.Drawing.Size(56, 59)
        Me.INDpeBack.StyleController = Me.LayoutControl1
        Me.INDpeBack.TabIndex = 0
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDpeBack)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(540, 277)
        Me.LayoutControl1.TabIndex = 1
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AllowHide = False
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.INDlygGroup})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.ShowInCustomizationForm = False
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(540, 277)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.AllowHide = False
        Me.LayoutControlItem1.Control = Me.INDpeBack
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(56, 59)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(56, 59)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 12, -12)
        Me.LayoutControlItem1.ShowInCustomizationForm = False
        Me.LayoutControlItem1.Size = New System.Drawing.Size(56, 277)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'INDlygGroup
        '
        Me.INDlygGroup.AllowHide = False
        Me.INDlygGroup.AppearanceGroup.Font = New System.Drawing.Font("Arial Narrow", 1.0!)
        Me.INDlygGroup.AppearanceGroup.Options.UseFont = True
        Me.INDlygGroup.CustomizationFormText = "."
        Me.INDlygGroup.Location = New System.Drawing.Point(56, 0)
        Me.INDlygGroup.Name = "INDlygGroup"
        Me.INDlygGroup.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.INDlygGroup.ShowInCustomizationForm = False
        Me.INDlygGroup.Size = New System.Drawing.Size(484, 277)
        Me.INDlygGroup.Text = "."
        '
        'CtrNavigation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LayoutControl1)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "CtrNavigation"
        Me.Size = New System.Drawing.Size(540, 277)
        CType(Me.INDpeBack.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygGroup, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlygGroup As DevExpress.XtraLayout.LayoutControlGroup
    Public WithEvents INDpeBack As DevExpress.XtraEditors.PictureEdit

End Class
