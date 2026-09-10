<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmAdecuationLabelDialog
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
        Me.INDLcRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSbNutricionParenteral = New DevExpress.XtraEditors.SimpleButton()
        Me.INDSbBolsa = New DevExpress.XtraEditors.SimpleButton()
        Me.INDSbJeringa = New DevExpress.XtraEditors.SimpleButton()
        Me.INDSbTableteria4x4 = New DevExpress.XtraEditors.SimpleButton()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciJeringa = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciBolsa = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciNutricionParenteral = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciTableteria4x4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDSbMagistral = New DevExpress.XtraEditors.SimpleButton()
        Me.INDLciMagistral = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.INDLcRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcRoot.SuspendLayout()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciJeringa, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciBolsa, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciNutricionParenteral, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciTableteria4x4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciMagistral, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDLcRoot
        '
        Me.INDLcRoot.Controls.Add(Me.INDSbNutricionParenteral)
        Me.INDLcRoot.Controls.Add(Me.INDSbBolsa)
        Me.INDLcRoot.Controls.Add(Me.INDSbJeringa)
        Me.INDLcRoot.Controls.Add(Me.INDSbTableteria4x4)
        Me.INDLcRoot.Controls.Add(Me.INDSbMagistral)
        Me.INDLcRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcRoot.Location = New System.Drawing.Point(0, 0)
        Me.INDLcRoot.Name = "INDLcRoot"
        Me.INDLcRoot.Root = Me.Root
        Me.INDLcRoot.Size = New System.Drawing.Size(380, 272)
        Me.INDLcRoot.TabIndex = 0
        Me.INDLcRoot.Text = "LayoutControl1"
        '
        'INDSbNutricionParenteral
        '
        Me.INDSbNutricionParenteral.AllowFocus = False
        Me.INDSbNutricionParenteral.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSbNutricionParenteral.Appearance.Options.UseFont = True
        Me.INDSbNutricionParenteral.Location = New System.Drawing.Point(12, 112)
        Me.INDSbNutricionParenteral.Name = "INDSbNutricionParenteral"
        Me.INDSbNutricionParenteral.Size = New System.Drawing.Size(356, 46)
        Me.INDSbNutricionParenteral.StyleController = Me.INDLcRoot
        Me.INDSbNutricionParenteral.TabIndex = 6
        Me.INDSbNutricionParenteral.Text = "Nutrición parenteral"
        '
        'INDSbBolsa
        '
        Me.INDSbBolsa.AllowFocus = False
        Me.INDSbBolsa.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSbBolsa.Appearance.Options.UseFont = True
        Me.INDSbBolsa.Location = New System.Drawing.Point(12, 62)
        Me.INDSbBolsa.Name = "INDSbBolsa"
        Me.INDSbBolsa.Size = New System.Drawing.Size(356, 46)
        Me.INDSbBolsa.StyleController = Me.INDLcRoot
        Me.INDSbBolsa.TabIndex = 5
        Me.INDSbBolsa.Text = "Bolsas"
        '
        'INDSbJeringa
        '
        Me.INDSbJeringa.AllowFocus = False
        Me.INDSbJeringa.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSbJeringa.Appearance.Options.UseFont = True
        Me.INDSbJeringa.Location = New System.Drawing.Point(12, 12)
        Me.INDSbJeringa.Name = "INDSbJeringa"
        Me.INDSbJeringa.Size = New System.Drawing.Size(356, 46)
        Me.INDSbJeringa.StyleController = Me.INDLcRoot
        Me.INDSbJeringa.TabIndex = 4
        Me.INDSbJeringa.Text = "Jeringas "
        '
        'INDSbTableteria4x4
        '
        Me.INDSbTableteria4x4.AllowFocus = False
        Me.INDSbTableteria4x4.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSbTableteria4x4.Appearance.Options.UseFont = True
        Me.INDSbTableteria4x4.Location = New System.Drawing.Point(12, 162)
        Me.INDSbTableteria4x4.Name = "INDSbTableteria4x4"
        Me.INDSbTableteria4x4.Size = New System.Drawing.Size(356, 46)
        Me.INDSbTableteria4x4.StyleController = Me.INDLcRoot
        Me.INDSbTableteria4x4.TabIndex = 6
        Me.INDSbTableteria4x4.Text = "Tabletería 4x4 cm"
        '
        'Root
        '
        Me.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.Root.GroupBordersVisible = False
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciJeringa, Me.INDLciBolsa, Me.INDLciNutricionParenteral, Me.INDLciTableteria4x4, Me.INDLciMagistral})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(380, 272)
        Me.Root.TextVisible = False
        '
        'INDLciJeringa
        '
        Me.INDLciJeringa.Control = Me.INDSbJeringa
        Me.INDLciJeringa.Location = New System.Drawing.Point(0, 0)
        Me.INDLciJeringa.MaxSize = New System.Drawing.Size(0, 50)
        Me.INDLciJeringa.MinSize = New System.Drawing.Size(89, 50)
        Me.INDLciJeringa.Name = "INDLciJeringa"
        Me.INDLciJeringa.Size = New System.Drawing.Size(360, 50)
        Me.INDLciJeringa.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciJeringa.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciJeringa.TextVisible = False
        Me.INDLciJeringa.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLciBolsa
        '
        Me.INDLciBolsa.Control = Me.INDSbBolsa
        Me.INDLciBolsa.Location = New System.Drawing.Point(0, 50)
        Me.INDLciBolsa.MaxSize = New System.Drawing.Size(0, 50)
        Me.INDLciBolsa.MinSize = New System.Drawing.Size(91, 50)
        Me.INDLciBolsa.Name = "INDLciBolsa"
        Me.INDLciBolsa.Size = New System.Drawing.Size(360, 50)
        Me.INDLciBolsa.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciBolsa.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciBolsa.TextVisible = False
        Me.INDLciBolsa.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLciNutricionParenteral
        '
        Me.INDLciNutricionParenteral.Control = Me.INDSbNutricionParenteral
        Me.INDLciNutricionParenteral.Location = New System.Drawing.Point(0, 100)
        Me.INDLciNutricionParenteral.MaxSize = New System.Drawing.Size(0, 50)
        Me.INDLciNutricionParenteral.MinSize = New System.Drawing.Size(91, 50)
        Me.INDLciNutricionParenteral.Name = "INDLciNutricionParenteral"
        Me.INDLciNutricionParenteral.Size = New System.Drawing.Size(360, 50)
        Me.INDLciNutricionParenteral.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciNutricionParenteral.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciNutricionParenteral.TextVisible = False
        Me.INDLciNutricionParenteral.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLciTableteria4x4
        '
        Me.INDLciTableteria4x4.Control = Me.INDSbTableteria4x4
        Me.INDLciTableteria4x4.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciTableteria4x4.CustomizationFormText = "INDLciTableteria4x4"
        Me.INDLciTableteria4x4.Location = New System.Drawing.Point(0, 150)
        Me.INDLciTableteria4x4.MaxSize = New System.Drawing.Size(0, 50)
        Me.INDLciTableteria4x4.MinSize = New System.Drawing.Size(91, 50)
        Me.INDLciTableteria4x4.Name = "INDLciTableteria4x4"
        Me.INDLciTableteria4x4.Size = New System.Drawing.Size(360, 50)
        Me.INDLciTableteria4x4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciTableteria4x4.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciTableteria4x4.TextVisible = False
        Me.INDLciTableteria4x4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDSbMagistral
        '
        Me.INDSbMagistral.AllowFocus = False
        Me.INDSbMagistral.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSbMagistral.Appearance.Options.UseFont = True
        Me.INDSbMagistral.Location = New System.Drawing.Point(12, 212)
        Me.INDSbMagistral.Name = "INDSbMagistral"
        Me.INDSbMagistral.Size = New System.Drawing.Size(356, 46)
        Me.INDSbMagistral.StyleController = Me.INDLcRoot
        Me.INDSbMagistral.TabIndex = 6
        Me.INDSbMagistral.Text = "Magistral"
        '
        'INDLciMagistral
        '
        Me.INDLciMagistral.Control = Me.INDSbMagistral
        Me.INDLciMagistral.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciMagistral.CustomizationFormText = "INDLciTableteria4x4"
        Me.INDLciMagistral.Location = New System.Drawing.Point(0, 200)
        Me.INDLciMagistral.MaxSize = New System.Drawing.Size(0, 50)
        Me.INDLciMagistral.MinSize = New System.Drawing.Size(91, 50)
        Me.INDLciMagistral.Name = "INDLciMagistral"
        Me.INDLciMagistral.Size = New System.Drawing.Size(360, 52)
        Me.INDLciMagistral.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciMagistral.Text = "INDLciTableteria4x4"
        Me.INDLciMagistral.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciMagistral.TextVisible = False
        Me.INDLciMagistral.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'FrmAdecuationLabelDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(380, 272)
        Me.Controls.Add(Me.INDLcRoot)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmAdecuationLabelDialog"
        Me.ShowInTaskbar = False
        Me.Text = "Etiquetas de adecuaciones"
        CType(Me.INDLcRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcRoot.ResumeLayout(False)
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciJeringa, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciBolsa, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciNutricionParenteral, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciTableteria4x4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciMagistral, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents INDLcRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDSbNutricionParenteral As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDSbBolsa As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDSbJeringa As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDLciJeringa As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciBolsa As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciNutricionParenteral As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSbTableteria4x4 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDLciTableteria4x4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSbMagistral As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDLciMagistral As DevExpress.XtraLayout.LayoutControlItem
End Class
