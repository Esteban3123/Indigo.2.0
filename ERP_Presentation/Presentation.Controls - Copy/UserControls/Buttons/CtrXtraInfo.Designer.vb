<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrXtraInfo
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
        Me.components = New System.ComponentModel.Container()
        Me.INDlycRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDlycgRoot = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        CType(Me.INDlycRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlycgRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDlycRoot
        '
        Me.INDlycRoot.AllowCustomizationMenu = False
        Me.INDlycRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlycRoot.Location = New System.Drawing.Point(0, 0)
        Me.INDlycRoot.Name = "INDlycRoot"
        Me.INDlycRoot.Root = Me.INDlycgRoot
        Me.INDlycRoot.Size = New System.Drawing.Size(376, 92)
        Me.INDlycRoot.TabIndex = 0
        Me.INDlycRoot.Text = "LayoutControl1"
        '
        'INDlycgRoot
        '
        Me.INDlycgRoot.AllowHide = False
        Me.INDlycgRoot.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlycgRoot.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlycgRoot.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlycgRoot.AppearanceItemCaption.Options.UseFont = True
        Me.INDlycgRoot.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlycgRoot.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlycgRoot.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlycgRoot.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlycgRoot.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlycgRoot.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlycgRoot.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlycgRoot.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlycgRoot.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlycgRoot.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlycgRoot, False)
        Me.INDlycgRoot.CustomizationFormText = "INDlycgRoot"
        Me.INDlycgRoot.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlycgRoot.Location = New System.Drawing.Point(0, 0)
        Me.INDlycgRoot.Name = "INDlycgRoot"
        Me.INDlycgRoot.ShowInCustomizationForm = False
        Me.INDlycgRoot.Size = New System.Drawing.Size(376, 92)
        Me.INDlycgRoot.Text = "CtrXtraInfo"
        '
        'CtrXtraInfo
        '
        Me.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.INDlycRoot)
        Me.MinimumSize = New System.Drawing.Size(376, 92)
        Me.Name = "CtrXtraInfo"
        Me.Size = New System.Drawing.Size(376, 92)
        CType(Me.INDlycRoot, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlycgRoot, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDlycRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlycgRoot As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl

End Class
