Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmPopupVerifyParameters
    Inherits FormBase

    'Form overrides dispose to clean up the component list.
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
        Me.components = New System.ComponentModel.Container()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDLcRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDGcNPT = New DevExpress.XtraGrid.GridControl()
        Me.INDBgvNPT = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridView()
        Me.INDGbMainInfo = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.INDColItem = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.INDGbSecondaryInfo = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.GridColumn6 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gridBand1 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.GridColumn10 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcRoot.SuspendLayout()
        CType(Me.INDGcNPT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDBgvNPT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLcRoot)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 136)
        Me.INDPanelControlBase.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDPanelControlBase.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1110, 467)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Location = New System.Drawing.Point(0, 6)
        Me.ToolBars.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ToolBars.Size = New System.Drawing.Size(1110, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.BarraBotones.Size = New System.Drawing.Size(1110, 130)
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'INDLcRoot
        '
        Me.INDLcRoot.Controls.Add(Me.INDGcNPT)
        Me.INDLcRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcRoot.Location = New System.Drawing.Point(2, 8)
        Me.INDLcRoot.Name = "INDLcRoot"
        Me.INDLcRoot.Root = Me.Root
        Me.INDLcRoot.Size = New System.Drawing.Size(1106, 457)
        Me.INDLcRoot.TabIndex = 0
        Me.INDLcRoot.Text = "LayoutControl1"
        '
        'INDGcNPT
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcNPT, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcNPT, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcNPT, True)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcNPT, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcNPT, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcNPT, False)
        Me.INDGcNPT.Location = New System.Drawing.Point(11, 10)
        Me.INDGcNPT.MainView = Me.INDBgvNPT
        Me.INDGcNPT.Name = "INDGcNPT"
        Me.INDGcNPT.Size = New System.Drawing.Size(1084, 437)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcNPT, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcNPT.TabIndex = 4
        Me.INDGcNPT.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDBgvNPT})
        '
        'INDBgvNPT
        '
        Me.INDBgvNPT.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDBgvNPT.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDBgvNPT.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDBgvNPT.Appearance.FocusedRow.Options.UseFont = True
        Me.INDBgvNPT.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDBgvNPT.Appearance.GroupRow.Options.UseFont = True
        Me.INDBgvNPT.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDBgvNPT.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDBgvNPT.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.INDBgvNPT.Appearance.HeaderPanel.TextOptions.Trimming = DevExpress.Utils.Trimming.None
        Me.INDBgvNPT.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.INDBgvNPT.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.INDBgvNPT.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDBgvNPT.Appearance.Row.Options.UseFont = True
        Me.INDBgvNPT.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDBgvNPT.Appearance.ViewCaption.Options.UseFont = True
        Me.INDBgvNPT.Bands.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.GridBand() {Me.INDGbMainInfo, Me.INDGbSecondaryInfo, Me.gridBand1})
        Me.INDBgvNPT.Columns.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn() {Me.INDColItem, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn10, Me.GridColumn5, Me.GridColumn6, Me.GridColumn7, Me.GridColumn8, Me.GridColumn9})
        Me.INDBgvNPT.GridControl = Me.INDGcNPT
        Me.INDBgvNPT.Name = "INDBgvNPT"
        Me.INDBgvNPT.OptionsView.ColumnAutoWidth = False
        Me.INDBgvNPT.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDBgvNPT.OptionsView.EnableAppearanceEvenRow = True
        Me.INDBgvNPT.OptionsView.EnableAppearanceOddRow = True
        Me.INDBgvNPT.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.INDBgvNPT.OptionsView.ShowAutoFilterRow = True
        Me.INDBgvNPT.OptionsView.ShowDetailButtons = False
        Me.INDBgvNPT.OptionsView.ShowFooter = True
        Me.INDBgvNPT.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDBgvNPT, False)
        '
        'INDGbMainInfo
        '
        Me.INDGbMainInfo.AppearanceHeader.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.INDGbMainInfo.AppearanceHeader.Options.UseFont = True
        Me.INDGbMainInfo.AppearanceHeader.Options.UseTextOptions = True
        Me.INDGbMainInfo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDGbMainInfo.Caption = "Información general"
        Me.INDGbMainInfo.Columns.Add(Me.INDColItem)
        Me.INDGbMainInfo.Columns.Add(Me.GridColumn2)
        Me.INDGbMainInfo.Columns.Add(Me.GridColumn3)
        Me.INDGbMainInfo.Columns.Add(Me.GridColumn4)
        Me.INDGbMainInfo.Columns.Add(Me.GridColumn5)
        Me.INDGbMainInfo.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.INDGbMainInfo.Name = "INDGbMainInfo"
        Me.INDGbMainInfo.VisibleIndex = 0
        Me.INDGbMainInfo.Width = 413
        '
        'INDColItem
        '
        Me.INDColItem.Caption = "Ítem"
        Me.INDColItem.FieldName = "INDColItem"
        Me.INDColItem.Name = "INDColItem"
        Me.INDColItem.OptionsColumn.AllowEdit = False
        Me.INDColItem.OptionsColumn.AllowFocus = False
        Me.INDColItem.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColItem.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColItem.OptionsColumn.AllowMove = False
        Me.INDColItem.OptionsColumn.AllowShowHide = False
        Me.INDColItem.OptionsColumn.AllowSize = False
        Me.INDColItem.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColItem.OptionsColumn.FixedWidth = True
        Me.INDColItem.UnboundType = DevExpress.Data.UnboundColumnType.[Integer]
        Me.INDColItem.Visible = True
        Me.INDColItem.Width = 50
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Nombre"
        Me.GridColumn2.FieldName = "PatientName"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn2.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn2.OptionsColumn.AllowMove = False
        Me.GridColumn2.OptionsColumn.AllowShowHide = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.Width = 128
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Identificación"
        Me.GridColumn3.FieldName = "PatientCode"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        Me.GridColumn3.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn3.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn3.OptionsColumn.AllowMove = False
        Me.GridColumn3.OptionsColumn.AllowShowHide = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.Width = 104
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Cama"
        Me.GridColumn4.FieldName = "Bed"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        Me.GridColumn4.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn4.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn4.OptionsColumn.AllowMove = False
        Me.GridColumn4.OptionsColumn.AllowShowHide = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.Width = 65
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Peso (Kg)"
        Me.GridColumn5.FieldName = "PatientWeight"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.AllowEdit = False
        Me.GridColumn5.OptionsColumn.AllowFocus = False
        Me.GridColumn5.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn5.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn5.OptionsColumn.AllowMove = False
        Me.GridColumn5.OptionsColumn.AllowShowHide = False
        Me.GridColumn5.Visible = True
        Me.GridColumn5.Width = 66
        '
        'INDGbSecondaryInfo
        '
        Me.INDGbSecondaryInfo.AppearanceHeader.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.INDGbSecondaryInfo.AppearanceHeader.Options.UseFont = True
        Me.INDGbSecondaryInfo.AppearanceHeader.Options.UseTextOptions = True
        Me.INDGbSecondaryInfo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDGbSecondaryInfo.Caption = "Datos de administración"
        Me.INDGbSecondaryInfo.Columns.Add(Me.GridColumn6)
        Me.INDGbSecondaryInfo.Columns.Add(Me.GridColumn7)
        Me.INDGbSecondaryInfo.Columns.Add(Me.GridColumn8)
        Me.INDGbSecondaryInfo.Columns.Add(Me.GridColumn9)
        Me.INDGbSecondaryInfo.Name = "INDGbSecondaryInfo"
        Me.INDGbSecondaryInfo.VisibleIndex = 1
        Me.INDGbSecondaryInfo.Width = 420
        '
        'GridColumn6
        '
        Me.GridColumn6.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn6.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.GridColumn6.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn6.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.None
        Me.GridColumn6.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.GridColumn6.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.GridColumn6.Caption = "Vía de administración"
        Me.GridColumn6.FieldName = "AdministrationRouteDescription"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.AllowEdit = False
        Me.GridColumn6.OptionsColumn.AllowFocus = False
        Me.GridColumn6.OptionsColumn.FixedWidth = True
        Me.GridColumn6.Visible = True
        Me.GridColumn6.Width = 110
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Tiempo infusión (h)"
        Me.GridColumn7.FieldName = "InfusionTime"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsColumn.AllowEdit = False
        Me.GridColumn7.OptionsColumn.AllowFocus = False
        Me.GridColumn7.Visible = True
        Me.GridColumn7.Width = 100
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Volumen total (ml)"
        Me.GridColumn8.DisplayFormat.FormatString = "n2"
        Me.GridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn8.FieldName = "TotalVolume"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.OptionsColumn.AllowEdit = False
        Me.GridColumn8.OptionsColumn.AllowFocus = False
        Me.GridColumn8.Visible = True
        Me.GridColumn8.Width = 100
        '
        'GridColumn9
        '
        Me.GridColumn9.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn9.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.GridColumn9.Caption = "Velocidad de infusión (ml/h)"
        Me.GridColumn9.DisplayFormat.FormatString = "n2"
        Me.GridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn9.FieldName = "InfusionVelocity"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.OptionsColumn.AllowEdit = False
        Me.GridColumn9.OptionsColumn.AllowFocus = False
        Me.GridColumn9.Visible = True
        Me.GridColumn9.Width = 110
        '
        'gridBand1
        '
        Me.gridBand1.AppearanceHeader.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gridBand1.AppearanceHeader.Options.UseFont = True
        Me.gridBand1.AppearanceHeader.Options.UseTextOptions = True
        Me.gridBand1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gridBand1.Caption = "DEXTROSA AL 50% (mg/Kg/día)"
        Me.gridBand1.Name = "gridBand1"
        Me.gridBand1.Visible = False
        Me.gridBand1.VisibleIndex = -1
        Me.gridBand1.Width = 264
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "Unidad Funcional"
        Me.GridColumn10.FieldName = "FunctionalUnitCodeName"
        Me.GridColumn10.MinWidth = 21
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.Visible = True
        '
        'Root
        '
        Me.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.Root.GroupBordersVisible = False
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(1106, 457)
        Me.Root.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDGcNPT
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(1088, 441)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'FrmPopupVerifyParameters
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1110, 603)
        Me.IconOptions.ShowIcon = False
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmPopupVerifyParameters"
        Me.Opacity = 1.0R
        Me.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Verificar parámetros"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcRoot.ResumeLayout(False)
        CType(Me.INDGcNPT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDBgvNPT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents INDLcRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDGcNPT As DevExpress.XtraGrid.GridControl
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDBgvNPT As DevExpress.XtraGrid.Views.BandedGrid.BandedGridView
    Friend WithEvents INDColItem As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents INDGbMainInfo As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents INDGbSecondaryInfo As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gridBand1 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
End Class
