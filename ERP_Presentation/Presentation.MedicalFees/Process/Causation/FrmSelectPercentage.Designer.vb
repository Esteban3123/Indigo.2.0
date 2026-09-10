Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSelectPercentage
    Inherits FormBase

    'Form overrides dispose to clean up the component list.
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
        Me.INDlyPrincipal = New DevExpress.XtraLayout.LayoutControl()
        Me.INDsePercentage = New DevExpress.XtraEditors.SpinEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemPercentage = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDpanelButtons = New DevExpress.XtraEditors.PanelControl()
        Me.INDbtnAccept = New DevExpress.XtraEditors.SimpleButton()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton()
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyPrincipal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyPrincipal.SuspendLayout()
        CType(Me.INDsePercentage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemPercentage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpanelButtons, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpanelButtons.SuspendLayout()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyPrincipal)
        Me.INDPanelControlBase.Controls.Add(Me.INDpanelButtons)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 118)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(236, 156)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(236, 94)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(236, 94)
        '
        'INDlyPrincipal
        '
        Me.INDlyPrincipal.Controls.Add(Me.INDsePercentage)
        Me.INDlyPrincipal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyPrincipal.Location = New System.Drawing.Point(2, 7)
        Me.INDlyPrincipal.Name = "INDlyPrincipal"
        Me.INDlyPrincipal.Root = Me.LayoutControlGroup1
        Me.INDlyPrincipal.Size = New System.Drawing.Size(232, 107)
        Me.INDlyPrincipal.TabIndex = 0
        Me.INDlyPrincipal.Text = "LayoutControl1"
        '
        'INDsePercentage
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsePercentage, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsePercentage, False)
        Me.INDsePercentage.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDsePercentage.EnterMoveNextControl = True
        Me.INDsePercentage.Location = New System.Drawing.Point(12, 38)
        Me.IndigoTextEdit1.SetMascara(Me.INDsePercentage, Presentation.Controls.IndigoTextEdit.EMask.Porcentaje)
        Me.INDsePercentage.Name = "INDsePercentage"
        Me.INDsePercentage.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDsePercentage.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsePercentage.Properties.Appearance.Options.UseBackColor = True
        Me.INDsePercentage.Properties.Appearance.Options.UseFont = True
        Me.INDsePercentage.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsePercentage.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsePercentage.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsePercentage.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsePercentage.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsePercentage.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsePercentage.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsePercentage.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.INDsePercentage.Properties.Mask.EditMask = "P"
        Me.INDsePercentage.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDsePercentage.Properties.MaxLength = 6
        Me.INDsePercentage.Size = New System.Drawing.Size(196, 28)
        Me.INDsePercentage.StyleController = Me.INDlyPrincipal
        Me.INDsePercentage.TabIndex = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsePercentage, 0)
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
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemPercentage})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(232, 107)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlyItemPercentage
        '
        Me.INDlyItemPercentage.Control = Me.INDsePercentage
        Me.INDlyItemPercentage.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemPercentage.MaxSize = New System.Drawing.Size(200, 64)
        Me.INDlyItemPercentage.MinSize = New System.Drawing.Size(200, 64)
        Me.INDlyItemPercentage.Name = "INDlyItemPercentage"
        Me.INDlyItemPercentage.Size = New System.Drawing.Size(212, 87)
        Me.INDlyItemPercentage.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemPercentage.Text = "Porcentaje"
        Me.INDlyItemPercentage.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemPercentage.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemPercentage.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemPercentage.TextToControlDistance = 5
        '
        'INDpanelButtons
        '
        Me.INDpanelButtons.Controls.Add(Me.INDbtnAccept)
        Me.INDpanelButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.INDpanelButtons.Location = New System.Drawing.Point(2, 114)
        Me.INDpanelButtons.Name = "INDpanelButtons"
        Me.INDpanelButtons.Size = New System.Drawing.Size(232, 40)
        Me.INDpanelButtons.TabIndex = 1
        '
        'INDbtnAccept
        '
        Me.INDbtnAccept.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnAccept.Appearance.Options.UseFont = True
        Me.INDbtnAccept.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDbtnAccept.Location = New System.Drawing.Point(2, 2)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnAccept, True)
        Me.INDbtnAccept.Name = "INDbtnAccept"
        Me.INDbtnAccept.Size = New System.Drawing.Size(228, 36)
        Me.INDbtnAccept.TabIndex = 0
        Me.INDbtnAccept.Text = "Aceptar"
        '
        'FrmSelectPercentage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(236, 274)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmSelectPercentage"
        Me.Opacity = 1.0R
        Me.Text = "Ingresar Porcentaje"
        Me.ViewModeEditHold = True
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyPrincipal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyPrincipal.ResumeLayout(False)
        CType(Me.INDsePercentage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemPercentage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpanelButtons, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpanelButtons.ResumeLayout(False)
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDpanelButtons As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDlyPrincipal As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDbtnAccept As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDsePercentage As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDlyItemPercentage As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
End Class
