Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmRejectionComment
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
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.GridLookUpEdit1 = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDbteCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.INDbtAccept = New DevExpress.XtraEditors.SimpleButton()
        Me.INDmeCommentRejection = New DevExpress.XtraEditors.MemoEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyiCommentConfirm = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyIbtAccept = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyibeCancel = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.INDlyiRejection = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl()
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate()
        'Me.IndigoLayoutControl1 = New Presentation.Controls.IndigoLayoutControl()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.GridLookUpEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDmeCommentRejection.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyiCommentConfirm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyIbtAccept, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyibeCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyiRejection, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        'CType(Me.IndigoLayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.LayoutControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 117)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(515, 178)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(515, 94)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(515, 94)
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.GridLookUpEdit1)
        Me.LayoutControl1.Controls.Add(Me.INDbteCancel)
        Me.LayoutControl1.Controls.Add(Me.INDbtAccept)
        Me.LayoutControl1.Controls.Add(Me.INDmeCommentRejection)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(2, 7)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(511, 169)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'GridLookUpEdit1
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.GridLookUpEdit1, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.GridLookUpEdit1, False)
        Me.GridLookUpEdit1.Location = New System.Drawing.Point(159, 12)
        Me.IndigoTextEdit1.SetMascara(Me.GridLookUpEdit1, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.GridLookUpEdit1.Name = "GridLookUpEdit1"
        Me.GridLookUpEdit1.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.GridLookUpEdit1.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.GridLookUpEdit1.Properties.Appearance.Options.UseBackColor = True
        Me.GridLookUpEdit1.Properties.Appearance.Options.UseFont = True
        Me.GridLookUpEdit1.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GridLookUpEdit1.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.GridLookUpEdit1.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.GridLookUpEdit1.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.GridLookUpEdit1.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.GridLookUpEdit1.Properties.AppearanceFocused.Options.UseFont = True
        Me.GridLookUpEdit1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.GridLookUpEdit1.Properties.DisplayMember = "Name"
        Me.GridLookUpEdit1.Properties.ImmediatePopup = True
        Me.GridLookUpEdit1.Properties.NullText = ""
        Me.GridLookUpEdit1.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains
        Me.GridLookUpEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.GridLookUpEdit1.Properties.ValueMember = "Id"
        Me.GridLookUpEdit1.Properties.View = Me.GridLookUpEdit1View
        Me.GridLookUpEdit1.Size = New System.Drawing.Size(239, 28)
        Me.GridLookUpEdit1.StyleController = Me.LayoutControl1
        Me.GridLookUpEdit1.TabIndex = 8
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.GridLookUpEdit1, 0)
        '
        'GridLookUpEdit1View
        '
        Me.GridLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2})
        Me.GridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit1View.Name = "GridLookUpEdit1View"
        Me.GridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Codigo"
        Me.GridColumn1.FieldName = "Code"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Nombre"
        Me.GridColumn2.FieldName = "Name"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
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
        'INDmeCommentRejection
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDmeCommentRejection, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDmeCommentRejection, False)
        Me.INDmeCommentRejection.Location = New System.Drawing.Point(159, 48)
        Me.IndigoTextEdit1.SetMascara(Me.INDmeCommentRejection, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDmeCommentRejection.Name = "INDmeCommentRejection"
        Me.INDmeCommentRejection.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDmeCommentRejection.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmeCommentRejection.Properties.Appearance.Options.UseBackColor = True
        Me.INDmeCommentRejection.Properties.Appearance.Options.UseFont = True
        Me.INDmeCommentRejection.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDmeCommentRejection.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDmeCommentRejection.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmeCommentRejection.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDmeCommentRejection.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDmeCommentRejection.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDmeCommentRejection.Size = New System.Drawing.Size(335, 96)
        Me.INDmeCommentRejection.StyleController = Me.LayoutControl1
        Me.INDmeCommentRejection.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDmeCommentRejection, 0)
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyiCommentConfirm, Me.INDLyIbtAccept, Me.INDlyibeCancel, Me.EmptySpaceItem1, Me.INDlyiRejection})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(506, 204)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlyiCommentConfirm
        '
        Me.INDlyiCommentConfirm.Control = Me.INDmeCommentRejection
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
        Me.INDLyIbtAccept.TextToControlDistance = 0
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
        Me.INDlyibeCancel.TextToControlDistance = 0
        Me.INDlyibeCancel.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 136)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(486, 10)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'INDlyiRejection
        '
        Me.INDlyiRejection.Control = Me.GridLookUpEdit1
        Me.INDlyiRejection.CustomizationFormText = "INDlyiRejection"
        Me.INDlyiRejection.Location = New System.Drawing.Point(0, 0)
        Me.INDlyiRejection.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyiRejection.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyiRejection.Name = "INDlyiRejection"
        Me.INDlyiRejection.Size = New System.Drawing.Size(486, 36)
        Me.INDlyiRejection.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyiRejection.Text = "Razón Rechazo"
        Me.INDlyiRejection.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyiRejection.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyiRejection.TextToControlDistance = 12
        '
        'IndigoLayoutControl1
        '

        'Me.IndigoLayoutControl1.FormName = "FormBase20140715"

        '
        'FrmRejectionComment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(515, 295)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmRejectionComment"
        Me.Opacity = 1.0R
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Razón de Rechazo"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.GridLookUpEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDmeCommentRejection.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyiCommentConfirm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyIbtAccept, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyibeCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyiRejection, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        'CType(Me.IndigoLayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDmeCommentRejection As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyiCommentConfirm As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents INDbteCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDbtAccept As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents INDLyIbtAccept As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyibeCancel As DevExpress.XtraLayout.LayoutControlItem
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents GridLookUpEdit1 As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyiRejection As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
End Class
