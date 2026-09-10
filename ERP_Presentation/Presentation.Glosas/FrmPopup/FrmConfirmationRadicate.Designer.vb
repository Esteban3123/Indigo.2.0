Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmConfirmationRadicate
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
        Me.components = New System.ComponentModel.Container()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDbteCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.INDbtAccept = New DevExpress.XtraEditors.SimpleButton()
        Me.INDmeCommentConfirm = New DevExpress.XtraEditors.MemoEdit()
        Me.INDdteDateConfirm = New DevExpress.XtraEditors.DateEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyiDateConfirm = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyiCommentConfirm = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyIbtAccept = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyibeCancel = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoGroupControl2 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDmeCommentConfirm.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteDateConfirm.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteDateConfirm.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyiDateConfirm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyiCommentConfirm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyIbtAccept, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyibeCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.LayoutControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 118)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(510, 162)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(510, 94)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(510, 94)
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDbteCancel)
        Me.LayoutControl1.Controls.Add(Me.INDbtAccept)
        Me.LayoutControl1.Controls.Add(Me.INDmeCommentConfirm)
        Me.LayoutControl1.Controls.Add(Me.INDdteDateConfirm)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(2, 7)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(506, 153)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDbteCancel
        '
        Me.INDbteCancel.Appearance.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDbteCancel.Appearance.Options.UseFont = True
        Me.INDbteCancel.Location = New System.Drawing.Point(255, 158)
        Me.INDbteCancel.Name = "INDbteCancel"
        Me.INDbteCancel.Size = New System.Drawing.Size(239, 34)
        Me.INDbteCancel.StyleController = Me.LayoutControl1
        Me.INDbteCancel.TabIndex = 7
        Me.INDbteCancel.Text = "Cancelar"
        '
        'INDbtAccept
        '
        Me.INDbtAccept.Appearance.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDbtAccept.Appearance.Options.UseFont = True
        Me.INDbtAccept.Location = New System.Drawing.Point(12, 158)
        Me.INDbtAccept.Name = "INDbtAccept"
        Me.INDbtAccept.Size = New System.Drawing.Size(239, 34)
        Me.INDbtAccept.StyleController = Me.LayoutControl1
        Me.INDbtAccept.TabIndex = 6
        Me.INDbtAccept.Text = "Aceptar"
        '
        'INDmeCommentConfirm
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDmeCommentConfirm, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDmeCommentConfirm, False)
        Me.INDmeCommentConfirm.Location = New System.Drawing.Point(159, 48)
        Me.IndigoTextEdit1.SetMascara(Me.INDmeCommentConfirm, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDmeCommentConfirm.Name = "INDmeCommentConfirm"
        Me.INDmeCommentConfirm.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDmeCommentConfirm.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmeCommentConfirm.Properties.Appearance.Options.UseBackColor = True
        Me.INDmeCommentConfirm.Properties.Appearance.Options.UseFont = True
        Me.INDmeCommentConfirm.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDmeCommentConfirm.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDmeCommentConfirm.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmeCommentConfirm.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDmeCommentConfirm.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDmeCommentConfirm.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDmeCommentConfirm.Size = New System.Drawing.Size(335, 96)
        Me.INDmeCommentConfirm.StyleController = Me.LayoutControl1
        Me.INDmeCommentConfirm.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDmeCommentConfirm, 0)
        '
        'INDdteDateConfirm
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdteDateConfirm, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDdteDateConfirm, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdteDateConfirm, False)
        Me.INDdteDateConfirm.EditValue = Nothing
        Me.INDdteDateConfirm.Location = New System.Drawing.Point(159, 12)
        Me.IndigoTextEdit1.SetMascara(Me.INDdteDateConfirm, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDdteDateConfirm, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDdteDateConfirm.Name = "INDdteDateConfirm"
        Me.INDdteDateConfirm.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDdteDateConfirm.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteDateConfirm.Properties.Appearance.Options.UseBackColor = True
        Me.INDdteDateConfirm.Properties.Appearance.Options.UseFont = True
        Me.INDdteDateConfirm.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDdteDateConfirm.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDdteDateConfirm.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteDateConfirm.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDdteDateConfirm.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDdteDateConfirm.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdteDateConfirm.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteDateConfirm.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteDateConfirm.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDdteDateConfirm.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDdteDateConfirm.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdteDateConfirm.Size = New System.Drawing.Size(239, 28)
        Me.INDdteDateConfirm.StyleController = Me.LayoutControl1
        Me.INDdteDateConfirm.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdteDateConfirm, 0)
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, False)
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyiDateConfirm, Me.INDlyiCommentConfirm, Me.INDLyIbtAccept, Me.INDlyibeCancel, Me.EmptySpaceItem1})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(506, 204)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlyiDateConfirm
        '
        Me.INDlyiDateConfirm.Control = Me.INDdteDateConfirm
        Me.INDlyiDateConfirm.CustomizationFormText = "Fecha Confirmacion"
        Me.INDlyiDateConfirm.Location = New System.Drawing.Point(0, 0)
        Me.INDlyiDateConfirm.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyiDateConfirm.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyiDateConfirm.Name = "INDlyiDateConfirm"
        Me.INDlyiDateConfirm.Size = New System.Drawing.Size(486, 36)
        Me.INDlyiDateConfirm.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyiDateConfirm.Text = "Fecha Confirmacion"
        Me.INDlyiDateConfirm.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyiDateConfirm.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyiDateConfirm.TextToControlDistance = 12
        '
        'INDlyiCommentConfirm
        '
        Me.INDlyiCommentConfirm.Control = Me.INDmeCommentConfirm
        Me.INDlyiCommentConfirm.CustomizationFormText = "Comentario"
        Me.INDlyiCommentConfirm.Location = New System.Drawing.Point(0, 36)
        Me.INDlyiCommentConfirm.MaxSize = New System.Drawing.Size(500, 100)
        Me.INDlyiCommentConfirm.MinSize = New System.Drawing.Size(390, 100)
        Me.INDlyiCommentConfirm.Name = "INDlyiCommentConfirm"
        Me.INDlyiCommentConfirm.Size = New System.Drawing.Size(486, 100)
        Me.INDlyiCommentConfirm.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyiCommentConfirm.Text = "Comentario"
        Me.INDlyiCommentConfirm.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyiCommentConfirm.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyiCommentConfirm.TextToControlDistance = 12
        '
        'INDLyIbtAccept
        '
        Me.INDLyIbtAccept.Control = Me.INDbtAccept
        Me.INDLyIbtAccept.CustomizationFormText = "Aceptar"
        Me.INDLyIbtAccept.Location = New System.Drawing.Point(0, 146)
        Me.INDLyIbtAccept.MaxSize = New System.Drawing.Size(243, 38)
        Me.INDLyIbtAccept.MinSize = New System.Drawing.Size(243, 38)
        Me.INDLyIbtAccept.Name = "INDLyIbtAccept"
        Me.INDLyIbtAccept.Size = New System.Drawing.Size(243, 38)
        Me.INDLyIbtAccept.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyIbtAccept.Text = "Aceptar"
        Me.INDLyIbtAccept.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLyIbtAccept.TextVisible = False
        '
        'INDlyibeCancel
        '
        Me.INDlyibeCancel.Control = Me.INDbteCancel
        Me.INDlyibeCancel.CustomizationFormText = "Cancelar"
        Me.INDlyibeCancel.Location = New System.Drawing.Point(243, 146)
        Me.INDlyibeCancel.MaxSize = New System.Drawing.Size(243, 38)
        Me.INDlyibeCancel.MinSize = New System.Drawing.Size(243, 38)
        Me.INDlyibeCancel.Name = "INDlyibeCancel"
        Me.INDlyibeCancel.Size = New System.Drawing.Size(243, 38)
        Me.INDlyibeCancel.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyibeCancel.Text = "Cancelar"
        Me.INDlyibeCancel.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyibeCancel.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 136)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(486, 10)
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'FrmConfirmationRadicate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(510, 280)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmConfirmationRadicate"
        Me.Opacity = 1.0R
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Confirmación de Radicado"
        Me.ViewModeEditHold = True
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDmeCommentConfirm.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteDateConfirm.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteDateConfirm.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyiDateConfirm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyiCommentConfirm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyIbtAccept, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyibeCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDmeCommentConfirm As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDdteDateConfirm As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyiDateConfirm As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyiCommentConfirm As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents INDbteCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDbtAccept As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents INDLyIbtAccept As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyibeCancel As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoGroupControl2 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
End Class
