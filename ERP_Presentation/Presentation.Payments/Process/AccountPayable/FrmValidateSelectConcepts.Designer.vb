<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmValidateSelectConcepts
    Inherits Presentation.Controls.FormBase

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
        Dim RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDlyPrincipal = New DevExpress.XtraLayout.LayoutControl()
        Me.INDgcConcepts = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemConcepts = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.INDlyButtons = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemAcept = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDbtnAcept = New DevExpress.XtraEditors.SimpleButton()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDbtnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.INDlyItemCancel = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDpanelButtons = New DevExpress.XtraEditors.PanelControl()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyPrincipal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyPrincipal.SuspendLayout()
        CType(Me.INDgcConcepts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemConcepts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyButtons, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemAcept, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDlyItemCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpanelButtons, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpanelButtons.SuspendLayout()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyPrincipal)
        Me.INDPanelControlBase.Controls.Add(Me.INDpanelButtons)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(416, 306)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(416, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(416, 130)
        '
        'INDlyPrincipal
        '
        Me.INDlyPrincipal.Controls.Add(Me.INDgcConcepts)
        Me.INDlyPrincipal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyPrincipal.Location = New System.Drawing.Point(2, 7)
        Me.INDlyPrincipal.Name = "INDlyPrincipal"
        Me.INDlyPrincipal.Root = Me.LayoutControlGroup1
        Me.INDlyPrincipal.Size = New System.Drawing.Size(412, 232)
        Me.INDlyPrincipal.TabIndex = 0
        Me.INDlyPrincipal.Text = "LayoutControl1"
        '
        'INDgcConcepts
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcConcepts, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcConcepts, Nothing)
        Me.INDgcConcepts.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.INDgcConcepts, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcConcepts, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcConcepts, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcConcepts, False)
        Me.INDgcConcepts.Location = New System.Drawing.Point(12, 74)
        Me.INDgcConcepts.MainView = Me.GridView1
        Me.INDgcConcepts.Name = "INDgcConcepts"
        Me.INDgcConcepts.Size = New System.Drawing.Size(388, 146)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcConcepts, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcConcepts.TabIndex = 4
        Me.INDgcConcepts.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView1.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView1.Appearance.FocusedRow.Options.UseBackColor = True
        Me.GridView1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView1.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.GroupRow.Options.UseFont = True
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.GridView1.Appearance.ViewCaption.Options.UseFont = True
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2})
        Me.GridView1.GridControl = Me.INDgcConcepts
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsSelection.MultiSelect = True
        Me.GridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowDetailButtons = False
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView1, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Código"
        Me.GridColumn1.FieldName = "Code"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 1
        Me.GridColumn1.Width = 74
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Nombre"
        Me.GridColumn2.FieldName = "Name"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 2
        Me.GridColumn2.Width = 296
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemConcepts})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(412, 232)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlyItemConcepts
        '
        Me.INDlyItemConcepts.AppearanceItemCaption.Options.UseTextOptions = True
        Me.INDlyItemConcepts.AppearanceItemCaption.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.INDlyItemConcepts.Control = Me.INDgcConcepts
        Me.INDlyItemConcepts.CustomizationFormText = "INDlyItemConcepts"
        Me.INDlyItemConcepts.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemConcepts.MinSize = New System.Drawing.Size(104, 49)
        Me.INDlyItemConcepts.Name = "INDlyItemConcepts"
        Me.INDlyItemConcepts.Size = New System.Drawing.Size(392, 212)
        Me.INDlyItemConcepts.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemConcepts.Text = "Cuál (es) conceptos de retención de la lista desea aplicar?"
        Me.INDlyItemConcepts.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemConcepts.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemConcepts.TextSize = New System.Drawing.Size(50, 50)
        Me.INDlyItemConcepts.TextToControlDistance = 12
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = RepositoryItemPopupContainerEdit1
        '
        'INDlyButtons
        '
        Me.INDlyButtons.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyButtons.AppearanceGroup.Options.UseFont = True
        Me.INDlyButtons.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyButtons.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyButtons.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyButtons.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyButtons.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlyButtons.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlyButtons.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyButtons.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyButtons.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyButtons.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlyButtons.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyButtons.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyButtons, False)
        Me.INDlyButtons.CustomizationFormText = "INDlyButtons"
        Me.INDlyButtons.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlyButtons.GroupBordersVisible = False
        Me.INDlyButtons.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemAcept, Me.INDlyItemCancel})
        Me.INDlyButtons.Name = "INDlyButtons"
        Me.INDlyButtons.Size = New System.Drawing.Size(408, 61)
        Me.INDlyButtons.TextVisible = False
        '
        'INDlyItemAcept
        '
        Me.INDlyItemAcept.Control = Me.INDbtnAcept
        Me.INDlyItemAcept.CustomizationFormText = "INDlyItemAcept"
        Me.INDlyItemAcept.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemAcept.MaxSize = New System.Drawing.Size(0, 36)
        Me.INDlyItemAcept.MinSize = New System.Drawing.Size(24, 36)
        Me.INDlyItemAcept.Name = "INDlyItemAcept"
        Me.INDlyItemAcept.Size = New System.Drawing.Size(194, 41)
        Me.INDlyItemAcept.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemAcept.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemAcept.TextVisible = False
        '
        'INDbtnAcept
        '
        Me.INDbtnAcept.Location = New System.Drawing.Point(12, 12)
        Me.INDbtnAcept.Name = "INDbtnAcept"
        Me.INDbtnAcept.Size = New System.Drawing.Size(190, 32)
        Me.INDbtnAcept.StyleController = Me.LayoutControl1
        Me.INDbtnAcept.TabIndex = 4
        Me.INDbtnAcept.Text = "Aplicar"
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDbtnCancel)
        Me.LayoutControl1.Controls.Add(Me.INDbtnAcept)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(2, 2)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.INDlyButtons
        Me.LayoutControl1.Size = New System.Drawing.Size(408, 61)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDbtnCancel
        '
        Me.INDbtnCancel.Location = New System.Drawing.Point(206, 12)
        Me.INDbtnCancel.Name = "INDbtnCancel"
        Me.INDbtnCancel.Size = New System.Drawing.Size(190, 32)
        Me.INDbtnCancel.StyleController = Me.LayoutControl1
        Me.INDbtnCancel.TabIndex = 5
        Me.INDbtnCancel.Text = "No Aplicar"
        '
        'INDlyItemCancel
        '
        Me.INDlyItemCancel.Control = Me.INDbtnCancel
        Me.INDlyItemCancel.CustomizationFormText = "INDlyItemCancel"
        Me.INDlyItemCancel.Location = New System.Drawing.Point(194, 0)
        Me.INDlyItemCancel.MaxSize = New System.Drawing.Size(0, 36)
        Me.INDlyItemCancel.MinSize = New System.Drawing.Size(28, 36)
        Me.INDlyItemCancel.Name = "INDlyItemCancel"
        Me.INDlyItemCancel.Size = New System.Drawing.Size(194, 41)
        Me.INDlyItemCancel.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCancel.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemCancel.TextVisible = False
        '
        'INDpanelButtons
        '
        Me.INDpanelButtons.Controls.Add(Me.LayoutControl1)
        Me.INDpanelButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.INDpanelButtons.Location = New System.Drawing.Point(2, 239)
        Me.INDpanelButtons.Name = "INDpanelButtons"
        Me.INDpanelButtons.Size = New System.Drawing.Size(412, 65)
        Me.INDpanelButtons.TabIndex = 1
        '
        'FrmValidateSelectConcepts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(416, 441)
        Me.ControlBox = False
        Me.IconOptions.ShowIcon = False
        Me.Name = "FrmValidateSelectConcepts"
        Me.Opacity = 1.0R
        Me.Text = "Conceptos No Agregados"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyPrincipal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyPrincipal.ResumeLayout(False)
        CType(Me.INDgcConcepts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemConcepts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyButtons, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemAcept, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDlyItemCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpanelButtons, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpanelButtons.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDlyPrincipal As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDgcConcepts As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlyItemConcepts As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDpanelButtons As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlyButtons As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDbtnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDbtnAcept As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDlyItemAcept As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemCancel As DevExpress.XtraLayout.LayoutControlItem
End Class
