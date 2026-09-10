Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmConceptsExecuteFormulate
    Inherits FormBase

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.INDlyControlExecute = New DevExpress.XtraLayout.LayoutControl()
        Me.INDMemoEditFormulate = New DevExpress.XtraEditors.MemoEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyGroupExecuteFormulate = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlycGroupFormulate = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit()
        'Me.IndigoLayoutControl1 = New Presentation.Controls.IndigoLayoutControl()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyControlExecute, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyControlExecute.SuspendLayout()
        CType(Me.INDMemoEditFormulate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGroupExecuteFormulate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlycGroupFormulate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        'CType(Me.IndigoLayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyControlExecute)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 23)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(615, 458)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolBars.Size = New System.Drawing.Size(586, 94)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(586, 94)
        Me.BarraBotones.Visible = False
        '
        'INDlyControlExecute
        '
        Me.INDlyControlExecute.Controls.Add(Me.INDMemoEditFormulate)
        Me.INDlyControlExecute.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyControlExecute.Location = New System.Drawing.Point(2, 7)
        Me.INDlyControlExecute.Name = "INDlyControlExecute"
        Me.INDlyControlExecute.Root = Me.LayoutControlGroup1
        Me.INDlyControlExecute.Size = New System.Drawing.Size(611, 449)
        Me.INDlyControlExecute.TabIndex = 0
        Me.INDlyControlExecute.Text = "LayoutControl1"
        '
        'INDMemoEditFormulate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDMemoEditFormulate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDMemoEditFormulate, False)
        Me.INDMemoEditFormulate.Enabled = False
        Me.INDMemoEditFormulate.Location = New System.Drawing.Point(24, 60)
        Me.IndigoTextEdit1.SetMascara(Me.INDMemoEditFormulate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDMemoEditFormulate.Name = "INDMemoEditFormulate"
        Me.INDMemoEditFormulate.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDMemoEditFormulate.Properties.Appearance.Options.UseBackColor = True
        Me.INDMemoEditFormulate.Size = New System.Drawing.Size(563, 96)
        Me.INDMemoEditFormulate.StyleController = Me.INDlyControlExecute
        Me.INDMemoEditFormulate.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDMemoEditFormulate, 0)
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, False)
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyGroupExecuteFormulate, Me.INDlycGroupFormulate})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(611, 449)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlyGroupExecuteFormulate
        '
        Me.INDlyGroupExecuteFormulate.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlyGroupExecuteFormulate.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlyGroupExecuteFormulate.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGroupExecuteFormulate.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyGroupExecuteFormulate.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGroupExecuteFormulate.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyGroupExecuteFormulate.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlyGroupExecuteFormulate.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlyGroupExecuteFormulate.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGroupExecuteFormulate.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyGroupExecuteFormulate.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlyGroupExecuteFormulate.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlyGroupExecuteFormulate.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGroupExecuteFormulate.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyGroupExecuteFormulate, False)
        Me.INDlyGroupExecuteFormulate.CustomizationFormText = "Ejecución de la fórmula"
        Me.INDlyGroupExecuteFormulate.Location = New System.Drawing.Point(0, 160)
        Me.INDlyGroupExecuteFormulate.Name = "INDlyGroupExecuteFormulate"
        Me.INDlyGroupExecuteFormulate.Size = New System.Drawing.Size(591, 269)
        Me.INDlyGroupExecuteFormulate.Text = "Ejecución de la fórmula"
        '
        'INDlycGroupFormulate
        '
        Me.INDlycGroupFormulate.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlycGroupFormulate.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlycGroupFormulate.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlycGroupFormulate.AppearanceItemCaption.Options.UseFont = True
        Me.INDlycGroupFormulate.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlycGroupFormulate.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlycGroupFormulate.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlycGroupFormulate.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlycGroupFormulate.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlycGroupFormulate.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlycGroupFormulate.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlycGroupFormulate.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlycGroupFormulate.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlycGroupFormulate.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlycGroupFormulate, False)
        Me.INDlycGroupFormulate.CustomizationFormText = "Formula"
        Me.INDlycGroupFormulate.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1})
        Me.INDlycGroupFormulate.Location = New System.Drawing.Point(0, 0)
        Me.INDlycGroupFormulate.Name = "INDlycGroupFormulate"
        Me.INDlycGroupFormulate.Size = New System.Drawing.Size(591, 160)
        Me.INDlycGroupFormulate.Text = "Formula"
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDMemoEditFormulate
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(14, 100)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(567, 100)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.Text = "LayoutControlItem1"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextToControlDistance = 0
        Me.LayoutControlItem1.TextVisible = False
        '
        'IndigoLayoutControl1
        '

        'Me.IndigoLayoutControl1.FormName = "FormBase20140806"

        '
        'FrmConceptsExecuteFormulate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(615, 481)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmConceptsExecuteFormulate"
        Me.Opacity = 1.0R
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Test de la Fórmula"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyControlExecute, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyControlExecute.ResumeLayout(False)
        CType(Me.INDMemoEditFormulate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGroupExecuteFormulate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlycGroupFormulate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        'CType(Me.IndigoLayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDlyControlExecute As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyGroupExecuteFormulate As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents INDMemoEditFormulate As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDlycGroupFormulate As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
End Class
