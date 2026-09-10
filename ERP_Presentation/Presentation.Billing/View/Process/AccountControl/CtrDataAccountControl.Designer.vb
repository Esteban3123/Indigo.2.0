<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrDataAccountControl
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
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDliEntity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliCareGroup = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliEntity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliCareGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.LabelControl2)
        Me.LayoutControl1.Controls.Add(Me.LabelControl1)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(292, 62)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl2.Appearance.ForeColor = System.Drawing.Color.White
        Me.LabelControl2.Location = New System.Drawing.Point(107, 33)
        Me.LabelControl2.MaximumSize = New System.Drawing.Size(180, 30)
        Me.LabelControl2.MinimumSize = New System.Drawing.Size(180, 30)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(180, 30)
        Me.LabelControl2.StyleController = Me.LayoutControl1
        Me.LabelControl2.TabIndex = 5
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.White
        Me.LabelControl1.Location = New System.Drawing.Point(107, 2)
        Me.LabelControl1.MaximumSize = New System.Drawing.Size(180, 30)
        Me.LabelControl1.MinimumSize = New System.Drawing.Size(180, 30)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(180, 30)
        Me.LabelControl1.StyleController = Me.LayoutControl1
        Me.LabelControl1.TabIndex = 4
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDliEntity, Me.INDliCareGroup})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(292, 62)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDliEntity
        '
        Me.INDliEntity.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.INDliEntity.AppearanceItemCaption.ForeColor = System.Drawing.Color.White
        Me.INDliEntity.AppearanceItemCaption.Options.UseFont = True
        Me.INDliEntity.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDliEntity.Control = Me.LabelControl1
        Me.INDliEntity.CustomizationFormText = "ENTIDAD"
        Me.INDliEntity.Location = New System.Drawing.Point(0, 0)
        Me.INDliEntity.MaxSize = New System.Drawing.Size(135, 32)
        Me.INDliEntity.MinSize = New System.Drawing.Size(135, 31)
        Me.INDliEntity.Name = "INDliEntity"
        Me.INDliEntity.Size = New System.Drawing.Size(292, 31)
        Me.INDliEntity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliEntity.Text = "ENTIDAD"
        Me.INDliEntity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliEntity.TextSize = New System.Drawing.Size(100, 20)
        Me.INDliEntity.TextToControlDistance = 5
        '
        'INDliCareGroup
        '
        Me.INDliCareGroup.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.INDliCareGroup.AppearanceItemCaption.ForeColor = System.Drawing.Color.White
        Me.INDliCareGroup.AppearanceItemCaption.Options.UseFont = True
        Me.INDliCareGroup.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDliCareGroup.Control = Me.LabelControl2
        Me.INDliCareGroup.CustomizationFormText = "GR. ATENCIÓN"
        Me.INDliCareGroup.Location = New System.Drawing.Point(0, 31)
        Me.INDliCareGroup.MaxSize = New System.Drawing.Size(135, 32)
        Me.INDliCareGroup.MinSize = New System.Drawing.Size(135, 31)
        Me.INDliCareGroup.Name = "INDliCareGroup"
        Me.INDliCareGroup.Size = New System.Drawing.Size(292, 31)
        Me.INDliCareGroup.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliCareGroup.Text = "GR. ATENCIÓN"
        Me.INDliCareGroup.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliCareGroup.TextSize = New System.Drawing.Size(100, 20)
        Me.INDliCareGroup.TextToControlDistance = 5
        '
        'CtrDataAccountControl
        '
        Me.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LayoutControl1)
        Me.Margin = New System.Windows.Forms.Padding(0)
        Me.MaximumSize = New System.Drawing.Size(292, 62)
        Me.MinimumSize = New System.Drawing.Size(292, 62)
        Me.Name = "CtrDataAccountControl"
        Me.Size = New System.Drawing.Size(292, 62)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliEntity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliCareGroup, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDliEntity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDliCareGroup As DevExpress.XtraLayout.LayoutControlItem

End Class
