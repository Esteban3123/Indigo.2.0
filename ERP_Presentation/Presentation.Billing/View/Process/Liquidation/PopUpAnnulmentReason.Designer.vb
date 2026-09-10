<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PopUpAnnulmentReason
    Inherits DevExpress.XtraEditors.XtraForm

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
        Me.LcRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDsbAcept = New DevExpress.XtraEditors.SimpleButton()
        Me.INDmeDescription = New DevExpress.XtraEditors.MemoEdit()
        Me.SleAnnulmentReason = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LcgRoot = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LiAnnulateReason = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LiAnnulateReasonDescription = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LiAcept = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl()
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl()
        ''Me.IndigoLayoutControl1 = New Presentation.Controls.IndigoLayoutControl()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton()
        CType(Me.LcRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LcRoot.SuspendLayout()
        CType(Me.INDmeDescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SleAnnulmentReason.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LcgRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LiAnnulateReason, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LiAnnulateReasonDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LiAcept, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        'CType(Me.IndigoLayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LcRoot
        '
        Me.LcRoot.Controls.Add(Me.INDsbAcept)
        Me.LcRoot.Controls.Add(Me.INDmeDescription)
        Me.LcRoot.Controls.Add(Me.SleAnnulmentReason)
        Me.LcRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LcRoot.Location = New System.Drawing.Point(0, 0)
        Me.LcRoot.Name = "LcRoot"
        Me.LcRoot.Root = Me.LcgRoot
        Me.LcRoot.Size = New System.Drawing.Size(413, 271)
        Me.LcRoot.TabIndex = 0
        Me.LcRoot.Text = "LayoutControl1"
        '
        'INDsbAcept
        '
        Me.INDsbAcept.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsbAcept.Appearance.Options.UseFont = True
        Me.INDsbAcept.Location = New System.Drawing.Point(12, 222)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDsbAcept, True)
        Me.INDsbAcept.Name = "INDsbAcept"
        Me.INDsbAcept.Size = New System.Drawing.Size(386, 30)
        Me.INDsbAcept.StyleController = Me.LcRoot
        Me.INDsbAcept.TabIndex = 6
        Me.INDsbAcept.Text = "Aceptar"
        '
        'INDmeDescription
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDmeDescription, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDmeDescription, False)
        Me.INDmeDescription.EnterMoveNextControl = True
        Me.INDmeDescription.Location = New System.Drawing.Point(12, 98)
        Me.IndigoTextEdit1.SetMascara(Me.INDmeDescription, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDmeDescription.Name = "INDmeDescription"
        Me.INDmeDescription.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDmeDescription.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmeDescription.Properties.Appearance.Options.UseBackColor = True
        Me.INDmeDescription.Properties.Appearance.Options.UseFont = True
        Me.INDmeDescription.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDmeDescription.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDmeDescription.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmeDescription.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDmeDescription.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDmeDescription.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDmeDescription.Size = New System.Drawing.Size(386, 120)
        Me.INDmeDescription.StyleController = Me.LcRoot
        Me.INDmeDescription.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDmeDescription, 0)
        '
        'SleAnnulmentReason
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.SleAnnulmentReason, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.SleAnnulmentReason, False)
        Me.SleAnnulmentReason.EnterMoveNextControl = True
        Me.SleAnnulmentReason.Location = New System.Drawing.Point(12, 38)
        Me.IndigoTextEdit1.SetMascara(Me.SleAnnulmentReason, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.SleAnnulmentReason.Name = "SleAnnulmentReason"
        Me.SleAnnulmentReason.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.SleAnnulmentReason.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.SleAnnulmentReason.Properties.Appearance.Options.UseBackColor = True
        Me.SleAnnulmentReason.Properties.Appearance.Options.UseFont = True
        Me.SleAnnulmentReason.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.SleAnnulmentReason.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.SleAnnulmentReason.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.SleAnnulmentReason.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.SleAnnulmentReason.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.SleAnnulmentReason.Properties.AppearanceFocused.Options.UseFont = True
        Me.SleAnnulmentReason.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.SleAnnulmentReason.Properties.DisplayMember = "CodeName"
        Me.SleAnnulmentReason.Properties.NullText = ""
        Me.SleAnnulmentReason.Properties.ValueMember = "Id"
        Me.SleAnnulmentReason.Properties.View = Me.SearchLookUpEdit1View
        Me.SleAnnulmentReason.Size = New System.Drawing.Size(386, 28)
        Me.SleAnnulmentReason.StyleController = Me.LcRoot
        Me.SleAnnulmentReason.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.SleAnnulmentReason, 0)
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        'Cadena reemplazada... True
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
        Me.SearchLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Código"
        Me.GridColumn1.FieldName = "Code"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 381
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Nombre"
        Me.GridColumn2.FieldName = "Name"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 1011
        '
        'LcgRoot
        '
        Me.LcgRoot.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LcgRoot.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LcgRoot.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LcgRoot.AppearanceItemCaption.Options.UseFont = True
        Me.LcgRoot.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LcgRoot.AppearanceTabPage.Header.Options.UseFont = True
        Me.LcgRoot.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LcgRoot.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LcgRoot.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LcgRoot.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LcgRoot.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LcgRoot.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LcgRoot.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LcgRoot.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LcgRoot, False)
        Me.LcgRoot.CustomizationFormText = "LcgRoot"
        Me.LcgRoot.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LcgRoot.GroupBordersVisible = False
        Me.LcgRoot.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LiAnnulateReason, Me.LiAnnulateReasonDescription, Me.LiAcept})
        Me.LcgRoot.Location = New System.Drawing.Point(0, 0)
        Me.LcgRoot.Name = "LcgRoot"
        Me.LcgRoot.Size = New System.Drawing.Size(413, 271)
        Me.LcgRoot.Text = "LcgRoot"
        Me.LcgRoot.TextVisible = False
        '
        'LiAnnulateReason
        '
        Me.LiAnnulateReason.Control = Me.SleAnnulmentReason
        Me.LiAnnulateReason.CustomizationFormText = "LayoutControlItem1"
        Me.LiAnnulateReason.Location = New System.Drawing.Point(0, 0)
        Me.LiAnnulateReason.MaxSize = New System.Drawing.Size(390, 60)
        Me.LiAnnulateReason.MinSize = New System.Drawing.Size(390, 60)
        Me.LiAnnulateReason.Name = "LiAnnulateReason"
        Me.LiAnnulateReason.Size = New System.Drawing.Size(393, 60)
        Me.LiAnnulateReason.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LiAnnulateReason.Text = "Motivo"
        Me.LiAnnulateReason.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LiAnnulateReason.TextLocation = DevExpress.Utils.Locations.Top
        Me.LiAnnulateReason.TextSize = New System.Drawing.Size(96, 21)
        Me.LiAnnulateReason.TextToControlDistance = 5
        '
        'LiAnnulateReasonDescription
        '
        Me.LiAnnulateReasonDescription.Control = Me.INDmeDescription
        Me.LiAnnulateReasonDescription.CustomizationFormText = "LayoutControlItem2"
        Me.LiAnnulateReasonDescription.Location = New System.Drawing.Point(0, 60)
        Me.LiAnnulateReasonDescription.MaxSize = New System.Drawing.Size(390, 150)
        Me.LiAnnulateReasonDescription.MinSize = New System.Drawing.Size(390, 150)
        Me.LiAnnulateReasonDescription.Name = "LiAnnulateReasonDescription"
        Me.LiAnnulateReasonDescription.Size = New System.Drawing.Size(393, 150)
        Me.LiAnnulateReasonDescription.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LiAnnulateReasonDescription.Text = "Descripción"
        Me.LiAnnulateReasonDescription.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LiAnnulateReasonDescription.TextLocation = DevExpress.Utils.Locations.Top
        Me.LiAnnulateReasonDescription.TextSize = New System.Drawing.Size(133, 21)
        Me.LiAnnulateReasonDescription.TextToControlDistance = 5
        '
        'LiAcept
        '
        Me.LiAcept.Control = Me.INDsbAcept
        Me.LiAcept.CustomizationFormText = "LiAcept"
        Me.LiAcept.Location = New System.Drawing.Point(0, 210)
        Me.LiAcept.MaxSize = New System.Drawing.Size(390, 34)
        Me.LiAcept.MinSize = New System.Drawing.Size(390, 34)
        Me.LiAcept.Name = "LiAcept"
        Me.LiAcept.Size = New System.Drawing.Size(393, 41)
        Me.LiAcept.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LiAcept.Text = "LiAcept"
        Me.LiAcept.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LiAcept.TextSize = New System.Drawing.Size(0, 0)
        Me.LiAcept.TextToControlDistance = 0
        Me.LiAcept.TextVisible = False
        '
        'IndigoLayoutControl1
        '

        'Me.IndigoLayoutControl1.FormName = "FormBase20150512"

        '
        'PopUpAnnulmentReason
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(413, 271)
        Me.Controls.Add(Me.LcRoot)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "PopUpAnnulmentReason"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Motivo de Anulación"
        CType(Me.LcRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LcRoot.ResumeLayout(False)
        CType(Me.INDmeDescription.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SleAnnulmentReason.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LcgRoot, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LiAnnulateReason, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LiAnnulateReasonDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LiAcept, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        'CType(Me.IndigoLayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LcRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LcgRoot As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents SleAnnulmentReason As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LiAnnulateReason As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDmeDescription As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LiAnnulateReasonDescription As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents INDsbAcept As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LiAcept As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
End Class
