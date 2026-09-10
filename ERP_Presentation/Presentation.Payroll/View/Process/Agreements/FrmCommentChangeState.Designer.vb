Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCommentChangeState
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
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDdeEndSuspend = New DevExpress.XtraEditors.DateEdit()
        Me.INDbtnAceptar = New DevExpress.XtraEditors.SimpleButton()
        Me.INDmeCommentChangeState = New DevExpress.XtraEditors.MemoEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyiComment = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyiAceptar = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciDateEndSuspend = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup()
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl()
        Me.IndigoComboBoxEdit1 = New Presentation.Controls.IndigoComboBoxEdit()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDdeEndSuspend.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdeEndSuspend.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDmeCommentChangeState.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyiComment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyiAceptar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciDateEndSuspend, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoComboBoxEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.LayoutControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 117)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(436, 106)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(436, 94)
        Me.ToolBars.Visible = False
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(436, 94)
        Me.BarraBotones.Visible = False
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDdeEndSuspend)
        Me.LayoutControl1.Controls.Add(Me.INDbtnAceptar)
        Me.LayoutControl1.Controls.Add(Me.INDmeCommentChangeState)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(2, 7)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(432, 97)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDdeEndSuspend
        '
        Me.INDdeEndSuspend.EditValue = Nothing
        Me.INDdeEndSuspend.Location = New System.Drawing.Point(159, 12)
        Me.INDdeEndSuspend.Name = "INDdeEndSuspend"
        Me.INDdeEndSuspend.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDdeEndSuspend.Properties.Appearance.Options.UseFont = True
        Me.INDdeEndSuspend.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeEndSuspend.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeEndSuspend.Properties.DisplayFormat.FormatString = "dd \de MMMM \de yyyy"
        Me.INDdeEndSuspend.Properties.EditFormat.FormatString = "dd \de MMMM \de yyyy"
        Me.INDdeEndSuspend.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDdeEndSuspend.Size = New System.Drawing.Size(239, 28)
        Me.INDdeEndSuspend.StyleController = Me.LayoutControl1
        Me.INDdeEndSuspend.TabIndex = 6
        '
        'INDbtnAceptar
        '
        Me.INDbtnAceptar.Appearance.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDbtnAceptar.Appearance.Options.UseFont = True
        Me.INDbtnAceptar.Location = New System.Drawing.Point(207, 148)
        Me.INDbtnAceptar.Name = "INDbtnAceptar"
        Me.INDbtnAceptar.Size = New System.Drawing.Size(196, 28)
        Me.INDbtnAceptar.StyleController = Me.LayoutControl1
        Me.INDbtnAceptar.TabIndex = 5
        Me.INDbtnAceptar.Text = "Aceptar"
        '
        'INDmeCommentChangeState
        '
        Me.INDmeCommentChangeState.Location = New System.Drawing.Point(159, 48)
        Me.INDmeCommentChangeState.Name = "INDmeCommentChangeState"
        Me.INDmeCommentChangeState.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDmeCommentChangeState.Properties.Appearance.Options.UseFont = True
        Me.INDmeCommentChangeState.Properties.MaxLength = 250
        Me.INDmeCommentChangeState.Size = New System.Drawing.Size(244, 96)
        Me.INDmeCommentChangeState.StyleController = Me.LayoutControl1
        Me.INDmeCommentChangeState.TabIndex = 4
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyiComment, Me.INDlyiAceptar, Me.INDlciDateEndSuspend})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(415, 188)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlyiComment
        '
        Me.INDlyiComment.Control = Me.INDmeCommentChangeState
        Me.INDlyiComment.CustomizationFormText = "Comentario"
        Me.INDlyiComment.Location = New System.Drawing.Point(0, 36)
        Me.INDlyiComment.MaxSize = New System.Drawing.Size(500, 100)
        Me.INDlyiComment.MinSize = New System.Drawing.Size(320, 100)
        Me.INDlyiComment.Name = "INDlyiComment"
        Me.INDlyiComment.Size = New System.Drawing.Size(395, 100)
        Me.INDlyiComment.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyiComment.Text = "Comentario"
        Me.INDlyiComment.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyiComment.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyiComment.TextToControlDistance = 12
        '
        'INDlyiAceptar
        '
        Me.INDlyiAceptar.Control = Me.INDbtnAceptar
        Me.INDlyiAceptar.ControlAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.INDlyiAceptar.CustomizationFormText = "Aceptar"
        Me.INDlyiAceptar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.INDlyiAceptar.Location = New System.Drawing.Point(0, 136)
        Me.INDlyiAceptar.MaxSize = New System.Drawing.Size(200, 32)
        Me.INDlyiAceptar.MinSize = New System.Drawing.Size(200, 32)
        Me.INDlyiAceptar.Name = "INDlyiAceptar"
        Me.INDlyiAceptar.Size = New System.Drawing.Size(395, 32)
        Me.INDlyiAceptar.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyiAceptar.Text = "Aceptar"
        Me.INDlyiAceptar.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyiAceptar.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyiAceptar.TextToControlDistance = 0
        Me.INDlyiAceptar.TextVisible = False
        '
        'INDlciDateEndSuspend
        '
        Me.INDlciDateEndSuspend.Control = Me.INDdeEndSuspend
        Me.INDlciDateEndSuspend.CustomizationFormText = "Fecha Fin Suspencion"
        Me.INDlciDateEndSuspend.Location = New System.Drawing.Point(0, 0)
        Me.INDlciDateEndSuspend.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlciDateEndSuspend.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlciDateEndSuspend.Name = "INDlciDateEndSuspend"
        Me.INDlciDateEndSuspend.Size = New System.Drawing.Size(395, 36)
        Me.INDlciDateEndSuspend.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciDateEndSuspend.Text = "Fecha Fin "
        Me.INDlciDateEndSuspend.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciDateEndSuspend.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciDateEndSuspend.TextToControlDistance = 12
        '
        'FrmCommentChangeState
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(436, 223)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmCommentChangeState"
        Me.Opacity = 1.0R
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = "595"
        Me.Text = "Comentario"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDdeEndSuspend.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdeEndSuspend.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDmeCommentChangeState.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyiComment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyiAceptar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciDateEndSuspend, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoComboBoxEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDbtnAceptar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDmeCommentChangeState As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents INDlyiComment As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents INDlyiAceptar As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDdeEndSuspend As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDlciDateEndSuspend As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoComboBoxEdit1 As Presentation.Controls.IndigoComboBoxEdit
End Class
