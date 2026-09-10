<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CtrlCausationKPIs
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
        Me.panelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.layoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.groupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.lblValorReconocido = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.lblGrowthRate = New DevExpress.XtraEditors.LabelControl()
        Me.groupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.progressReconocido = New DevExpress.XtraEditors.ProgressBarControl()
        Me.lblPorcentajeReconocido = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.groupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.progressFallidas = New DevExpress.XtraEditors.ProgressBarControl()
        Me.lblPorcentajeFallidas = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.groupControl4 = New DevExpress.XtraEditors.GroupControl()
        Me.lblOrdenesSinCausar = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.panelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelControl1.SuspendLayout()
        CType(Me.layoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.layoutControl1.SuspendLayout()
        CType(Me.groupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.groupControl1.SuspendLayout()
        CType(Me.groupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.groupControl2.SuspendLayout()
        CType(Me.progressReconocido.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.groupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.groupControl3.SuspendLayout()
        CType(Me.progressFallidas.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.groupControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.groupControl4.SuspendLayout()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'panelControl1
        '
        Me.panelControl1.Controls.Add(Me.layoutControl1)
        Me.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelControl1.Location = New System.Drawing.Point(0, 0)
        Me.panelControl1.Name = "panelControl1"
        Me.panelControl1.Size = New System.Drawing.Size(1200, 120)
        Me.panelControl1.TabIndex = 0
        '
        'layoutControl1
        '
        Me.layoutControl1.Controls.Add(Me.groupControl1)
        Me.layoutControl1.Controls.Add(Me.groupControl2)
        Me.layoutControl1.Controls.Add(Me.groupControl3)
        Me.layoutControl1.Controls.Add(Me.groupControl4)
        Me.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.layoutControl1.Location = New System.Drawing.Point(2, 2)
        Me.layoutControl1.Name = "layoutControl1"
        Me.layoutControl1.Root = Me.Root
        Me.layoutControl1.Size = New System.Drawing.Size(1196, 116)
        Me.layoutControl1.TabIndex = 0
        Me.layoutControl1.Text = "LayoutControl1"
        '
        'groupControl1
        '
        Me.groupControl1.Controls.Add(Me.lblGrowthRate)
        Me.groupControl1.Controls.Add(Me.lblValorReconocido)
        Me.groupControl1.Controls.Add(Me.LabelControl1)
        Me.groupControl1.Location = New System.Drawing.Point(5, 5)
        Me.groupControl1.Name = "groupControl1"
        Me.groupControl1.Size = New System.Drawing.Size(215, 106)
        Me.groupControl1.TabIndex = 0
        Me.groupControl1.Text = "Valor Costo Reconocido"
        '
        'lblValorReconocido
        '
        Me.lblValorReconocido.Appearance.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold)
        Me.lblValorReconocido.Appearance.Options.UseFont = True
        Me.lblValorReconocido.Location = New System.Drawing.Point(10, 40)
        Me.lblValorReconocido.Name = "lblValorReconocido"
        Me.lblValorReconocido.Size = New System.Drawing.Size(30, 32)
        Me.lblValorReconocido.TabIndex = 0
        Me.lblValorReconocido.Text = "$0"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.Gray
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Appearance.Options.UseForeColor = True
        Me.LabelControl1.Location = New System.Drawing.Point(10, 78)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(112, 13)
        Me.LabelControl1.TabIndex = 1
        Me.LabelControl1.Text = "Crecimiento vs Mes Ant."
        '
        'lblGrowthRate
        '
        Me.lblGrowthRate.Appearance.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblGrowthRate.Appearance.Options.UseFont = True
        Me.lblGrowthRate.Location = New System.Drawing.Point(130, 76)
        Me.lblGrowthRate.Name = "lblGrowthRate"
        Me.lblGrowthRate.Size = New System.Drawing.Size(38, 17)
        Me.lblGrowthRate.TabIndex = 2
        Me.lblGrowthRate.Text = "+0.0%"
        '
        'groupControl2
        '
        Me.groupControl2.Controls.Add(Me.progressReconocido)
        Me.groupControl2.Controls.Add(Me.lblPorcentajeReconocido)
        Me.groupControl2.Controls.Add(Me.LabelControl2)
        Me.groupControl2.Location = New System.Drawing.Point(225, 5)
        Me.groupControl2.Name = "groupControl2"
        Me.groupControl2.Size = New System.Drawing.Size(215, 106)
        Me.groupControl2.TabIndex = 1
        Me.groupControl2.Text = "% Reconocido"
        '
        'progressReconocido
        '
        Me.progressReconocido.Location = New System.Drawing.Point(10, 78)
        Me.progressReconocido.Name = "progressReconocido"
        Me.progressReconocido.Properties.Maximum = 100
        Me.progressReconocido.Properties.ShowTitle = True
        Me.progressReconocido.Size = New System.Drawing.Size(195, 18)
        Me.progressReconocido.TabIndex = 2
        '
        'lblPorcentajeReconocido
        '
        Me.lblPorcentajeReconocido.Appearance.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold)
        Me.lblPorcentajeReconocido.Appearance.ForeColor = System.Drawing.Color.Green
        Me.lblPorcentajeReconocido.Appearance.Options.UseFont = True
        Me.lblPorcentajeReconocido.Appearance.Options.UseForeColor = True
        Me.lblPorcentajeReconocido.Location = New System.Drawing.Point(10, 30)
        Me.lblPorcentajeReconocido.Name = "lblPorcentajeReconocido"
        Me.lblPorcentajeReconocido.Size = New System.Drawing.Size(47, 32)
        Me.lblPorcentajeReconocido.TabIndex = 0
        Me.lblPorcentajeReconocido.Text = "0.0%"
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.LabelControl2.Appearance.ForeColor = System.Drawing.Color.Gray
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Appearance.Options.UseForeColor = True
        Me.LabelControl2.Location = New System.Drawing.Point(10, 64)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(96, 13)
        Me.LabelControl2.TabIndex = 1
        Me.LabelControl2.Text = "Causaciones exitosas"
        '
        'groupControl3
        '
        Me.groupControl3.Controls.Add(Me.progressFallidas)
        Me.groupControl3.Controls.Add(Me.lblPorcentajeFallidas)
        Me.groupControl3.Controls.Add(Me.LabelControl3)
        Me.groupControl3.Location = New System.Drawing.Point(445, 5)
        Me.groupControl3.Name = "groupControl3"
        Me.groupControl3.Size = New System.Drawing.Size(215, 106)
        Me.groupControl3.TabIndex = 2
        Me.groupControl3.Text = "% Causaciones Fallidas"
        '
        'progressFallidas
        '
        Me.progressFallidas.Location = New System.Drawing.Point(10, 78)
        Me.progressFallidas.Name = "progressFallidas"
        Me.progressFallidas.Properties.Maximum = 100
        Me.progressFallidas.Properties.ShowTitle = True
        Me.progressFallidas.Size = New System.Drawing.Size(195, 18)
        Me.progressFallidas.TabIndex = 2
        '
        'lblPorcentajeFallidas
        '
        Me.lblPorcentajeFallidas.Appearance.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold)
        Me.lblPorcentajeFallidas.Appearance.ForeColor = System.Drawing.Color.Red
        Me.lblPorcentajeFallidas.Appearance.Options.UseFont = True
        Me.lblPorcentajeFallidas.Appearance.Options.UseForeColor = True
        Me.lblPorcentajeFallidas.Location = New System.Drawing.Point(10, 30)
        Me.lblPorcentajeFallidas.Name = "lblPorcentajeFallidas"
        Me.lblPorcentajeFallidas.Size = New System.Drawing.Size(47, 32)
        Me.lblPorcentajeFallidas.TabIndex = 0
        Me.lblPorcentajeFallidas.Text = "0.0%"
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.LabelControl3.Appearance.ForeColor = System.Drawing.Color.Gray
        Me.LabelControl3.Appearance.Options.UseFont = True
        Me.LabelControl3.Appearance.Options.UseForeColor = True
        Me.LabelControl3.Location = New System.Drawing.Point(10, 64)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(88, 13)
        Me.LabelControl3.TabIndex = 1
        Me.LabelControl3.Text = "Con errores/alertas"
        '
        'groupControl4
        '
        Me.groupControl4.Controls.Add(Me.lblOrdenesSinCausar)
        Me.groupControl4.Controls.Add(Me.LabelControl4)
        Me.groupControl4.Location = New System.Drawing.Point(665, 5)
        Me.groupControl4.Name = "groupControl4"
        Me.groupControl4.Size = New System.Drawing.Size(226, 106)
        Me.groupControl4.TabIndex = 3
        Me.groupControl4.Text = "Órdenes Sin Causar"
        '
        'lblOrdenesSinCausar
        '
        Me.lblOrdenesSinCausar.Appearance.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold)
        Me.lblOrdenesSinCausar.Appearance.ForeColor = System.Drawing.Color.OrangeRed
        Me.lblOrdenesSinCausar.Appearance.Options.UseFont = True
        Me.lblOrdenesSinCausar.Appearance.Options.UseForeColor = True
        Me.lblOrdenesSinCausar.Location = New System.Drawing.Point(10, 30)
        Me.lblOrdenesSinCausar.Name = "lblOrdenesSinCausar"
        Me.lblOrdenesSinCausar.Size = New System.Drawing.Size(15, 32)
        Me.lblOrdenesSinCausar.TabIndex = 0
        Me.lblOrdenesSinCausar.Text = "0"
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.LabelControl4.Appearance.ForeColor = System.Drawing.Color.Gray
        Me.LabelControl4.Appearance.Options.UseFont = True
        Me.LabelControl4.Appearance.Options.UseForeColor = True
        Me.LabelControl4.Location = New System.Drawing.Point(10, 64)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(138, 13)
        Me.LabelControl4.TabIndex = 1
        Me.LabelControl4.Text = "Facturadas y sin facturar (Det.)"
        '
        'Root
        '
        Me.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.Root.GroupBordersVisible = False
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem3, Me.LayoutControlItem4})
        Me.Root.Name = "Root"
        Me.Root.Padding = New DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3)
        Me.Root.Size = New System.Drawing.Size(1200, 120)
        Me.Root.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.groupControl1
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(295, 114)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(200, 24)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.groupControl2
        Me.LayoutControlItem2.Location = New System.Drawing.Point(295, 0)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(295, 114)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(200, 24)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.groupControl3
        Me.LayoutControlItem3.Location = New System.Drawing.Point(590, 0)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(295, 114)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(200, 24)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = False
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.groupControl4
        Me.LayoutControlItem4.Location = New System.Drawing.Point(885, 0)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(309, 114)
        Me.LayoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem4.MinSize = New System.Drawing.Size(200, 24)
        Me.LayoutControlItem4.MaxSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextVisible = False
        '
        'CtrlCausationKPIs
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.panelControl1)
        Me.Name = "CtrlCausationKPIs"
        Me.Size = New System.Drawing.Size(1200, 120)
        CType(Me.panelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelControl1.ResumeLayout(False)
        CType(Me.layoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.layoutControl1.ResumeLayout(False)
        CType(Me.groupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.groupControl1.ResumeLayout(False)
        Me.groupControl1.PerformLayout()
        CType(Me.groupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.groupControl2.ResumeLayout(False)
        Me.groupControl2.PerformLayout()
        CType(Me.progressReconocido.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.groupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.groupControl3.ResumeLayout(False)
        Me.groupControl3.PerformLayout()
        CType(Me.progressFallidas.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.groupControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.groupControl4.ResumeLayout(False)
        Me.groupControl4.PerformLayout()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents panelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents layoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents groupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lblValorReconocido As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblGrowthRate As DevExpress.XtraEditors.LabelControl
    Friend WithEvents groupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents progressReconocido As DevExpress.XtraEditors.ProgressBarControl
    Friend WithEvents lblPorcentajeReconocido As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents groupControl3 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents progressFallidas As DevExpress.XtraEditors.ProgressBarControl
    Friend WithEvents lblPorcentajeFallidas As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents groupControl4 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lblOrdenesSinCausar As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
End Class

