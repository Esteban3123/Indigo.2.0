<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrAdvancedFilterSearchField
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CtrAdvancedFilterSearchField))
        Me.INDlycRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDlycgRoot = New DevExpress.XtraLayout.LayoutControlGroup()
        CType(Me.INDlycRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlycgRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDlycRoot
        '
        Me.INDlycRoot.AllowCustomizationMenu = False
        resources.ApplyResources(Me.INDlycRoot, "INDlycRoot")
        Me.INDlycRoot.Name = "INDlycRoot"
        Me.INDlycRoot.OptionsFocus.EnableAutoTabOrder = False
        Me.INDlycRoot.Root = Me.INDlycgRoot
        '
        'INDlycgRoot
        '
        Me.INDlycgRoot.AllowHide = False
        resources.ApplyResources(Me.INDlycgRoot, "INDlycgRoot")
        Me.INDlycgRoot.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlycgRoot.GroupBordersVisible = False
        Me.INDlycgRoot.Location = New System.Drawing.Point(0, 0)
        Me.INDlycgRoot.Name = "INDlycgRoot"
        Me.INDlycgRoot.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.INDlycgRoot.ShowInCustomizationForm = False
        Me.INDlycgRoot.Size = New System.Drawing.Size(200, 39)
        Me.INDlycgRoot.TextVisible = False
        '
        'CtrAdvancedFilterSearchField
        '
        Me.Appearance.BackColor = CType(resources.GetObject("CtrAdvancedFilterSearchField.Appearance.BackColor"), System.Drawing.Color)
        Me.Appearance.Options.UseBackColor = True
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.INDlycRoot)
        Me.Name = "CtrAdvancedFilterSearchField"
        CType(Me.INDlycRoot, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlycgRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDlycRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlycgRoot As DevExpress.XtraLayout.LayoutControlGroup

End Class
