Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAddAssets
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
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.INDbtnAddAssets = New DevExpress.XtraEditors.SimpleButton()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemAssets = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDgcAssets = New DevExpress.XtraGrid.GridControl()
        Me.INDviewAssets = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDcolSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepCheckSelectOption = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.INDpanelButtons = New DevExpress.XtraEditors.PanelControl()
        Me.INDlyAddAssets = New DevExpress.XtraLayout.LayoutControl()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemAssets, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcAssets, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewAssets, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepCheckSelectOption, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpanelButtons, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpanelButtons.SuspendLayout()
        CType(Me.INDlyAddAssets, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyAddAssets.SuspendLayout()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyAddAssets)
        Me.INDPanelControlBase.Controls.Add(Me.INDpanelButtons)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 118)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(710, 511)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(710, 94)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(710, 94)
        '
        'INDbtnAddAssets
        '
        Me.INDbtnAddAssets.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnAddAssets.Appearance.Options.UseFont = True
        Me.INDbtnAddAssets.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDbtnAddAssets.Location = New System.Drawing.Point(2, 2)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnAddAssets, True)
        Me.INDbtnAddAssets.Name = "INDbtnAddAssets"
        Me.INDbtnAddAssets.Size = New System.Drawing.Size(702, 36)
        Me.INDbtnAddAssets.TabIndex = 0
        Me.INDbtnAddAssets.Text = "Agregar"
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
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemAssets})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(706, 462)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlyItemAssets
        '
        Me.INDlyItemAssets.Control = Me.INDgcAssets
        Me.INDlyItemAssets.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemAssets.Name = "INDlyItemAssets"
        Me.INDlyItemAssets.Size = New System.Drawing.Size(686, 442)
        Me.INDlyItemAssets.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemAssets.TextVisible = False
        '
        'INDgcAssets
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcAssets, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcAssets, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcAssets, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcAssets, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcAssets, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcAssets, False)
        Me.INDgcAssets.Location = New System.Drawing.Point(12, 12)
        Me.INDgcAssets.MainView = Me.INDviewAssets
        Me.INDgcAssets.Name = "INDgcAssets"
        Me.INDgcAssets.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrepCheckSelectOption})
        Me.INDgcAssets.Size = New System.Drawing.Size(682, 438)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcAssets, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcAssets.TabIndex = 4
        Me.INDgcAssets.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDviewAssets})
        '
        'INDviewAssets
        '
        Me.INDviewAssets.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewAssets.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewAssets.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDviewAssets.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewAssets.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewAssets.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDviewAssets.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewAssets.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewAssets.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewAssets.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewAssets.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewAssets.Appearance.Row.Options.UseFont = True
        Me.INDviewAssets.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDviewAssets.Appearance.ViewCaption.Options.UseFont = True
        Me.INDviewAssets.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDcolSelect, Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4})
        Me.INDviewAssets.GridControl = Me.INDgcAssets
        Me.INDviewAssets.Name = "INDviewAssets"
        Me.INDviewAssets.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewAssets.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewAssets.OptionsView.ShowAutoFilterRow = True
        Me.INDviewAssets.OptionsView.ShowDetailButtons = False
        Me.INDviewAssets.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewAssets, False)
        '
        'INDcolSelect
        '
        Me.INDcolSelect.Caption = "Sel."
        Me.INDcolSelect.ColumnEdit = Me.INDrepCheckSelectOption
        Me.INDcolSelect.FieldName = "SelectOption"
        Me.INDcolSelect.Image = Global.Presentation.FixedAsset.My.Resources.Resources.undcheck
        Me.INDcolSelect.ImageAlignment = System.Drawing.StringAlignment.Center
        Me.INDcolSelect.Name = "INDcolSelect"
        Me.INDcolSelect.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDcolSelect.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDcolSelect.OptionsColumn.AllowMove = False
        Me.INDcolSelect.OptionsColumn.AllowShowHide = False
        Me.INDcolSelect.OptionsColumn.AllowSize = False
        Me.INDcolSelect.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDcolSelect.OptionsColumn.FixedWidth = True
        Me.INDcolSelect.OptionsFilter.AllowAutoFilter = False
        Me.INDcolSelect.OptionsFilter.AllowFilter = False
        Me.INDcolSelect.Visible = True
        Me.INDcolSelect.VisibleIndex = 0
        Me.INDcolSelect.Width = 27
        '
        'INDrepCheckSelectOption
        '
        Me.INDrepCheckSelectOption.AutoHeight = False
        Me.INDrepCheckSelectOption.Name = "INDrepCheckSelectOption"
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Articulo"
        Me.GridColumn1.FieldName = "ItemId.CodeDescription"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 1
        Me.GridColumn1.Width = 188
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Serie"
        Me.GridColumn2.FieldName = "Serie"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 2
        Me.GridColumn2.Width = 188
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Placa"
        Me.GridColumn3.FieldName = "Plate"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 3
        Me.GridColumn3.Width = 192
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Ubicación Actual"
        Me.GridColumn4.FieldName = "LocationId.Name"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 4
        Me.GridColumn4.Width = 211
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'INDpanelButtons
        '
        Me.INDpanelButtons.Controls.Add(Me.INDbtnAddAssets)
        Me.INDpanelButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.INDpanelButtons.Location = New System.Drawing.Point(2, 469)
        Me.INDpanelButtons.Name = "INDpanelButtons"
        Me.INDpanelButtons.Size = New System.Drawing.Size(706, 40)
        Me.INDpanelButtons.TabIndex = 0
        '
        'INDlyAddAssets
        '
        Me.INDlyAddAssets.Controls.Add(Me.INDgcAssets)
        Me.INDlyAddAssets.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyAddAssets.Location = New System.Drawing.Point(2, 7)
        Me.INDlyAddAssets.Name = "INDlyAddAssets"
        Me.INDlyAddAssets.Root = Me.LayoutControlGroup1
        Me.INDlyAddAssets.Size = New System.Drawing.Size(706, 462)
        Me.INDlyAddAssets.TabIndex = 1
        Me.INDlyAddAssets.Text = "LayoutControl1"
        '
        'FrmAddAssets
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(710, 629)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmAddAssets"
        Me.Opacity = 1.0R
        Me.Text = "Activos"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemAssets, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcAssets, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewAssets, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepCheckSelectOption, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpanelButtons, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpanelButtons.ResumeLayout(False)
        CType(Me.INDlyAddAssets, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyAddAssets.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents INDpanelButtons As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDbtnAddAssets As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDlyAddAssets As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDgcAssets As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDviewAssets As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItemAssets As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDcolSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepCheckSelectOption As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
End Class
