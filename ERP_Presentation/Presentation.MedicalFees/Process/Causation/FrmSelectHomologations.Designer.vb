Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSelectHomologations
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
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton()
        Me.INDbtnAddHomologations = New DevExpress.XtraEditors.SimpleButton()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView()
        Me.viewHomologations = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGclState = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepCheckSelectHomologations = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcHomologations = New DevExpress.XtraGrid.GridControl()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl()
        Me.INDpanelButton = New DevExpress.XtraEditors.PanelControl()
        Me.INDlyPrincipal = New DevExpress.XtraLayout.LayoutControl()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemHomologations = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.viewHomologations, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepCheckSelectHomologations, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcHomologations, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpanelButton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpanelButton.SuspendLayout()
        CType(Me.INDlyPrincipal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyPrincipal.SuspendLayout()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemHomologations, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyPrincipal)
        Me.INDPanelControlBase.Controls.Add(Me.INDpanelButton)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 117)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(649, 309)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(649, 94)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(649, 94)
        '
        'INDbtnAddHomologations
        '
        Me.INDbtnAddHomologations.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnAddHomologations.Appearance.Options.UseFont = True
        Me.INDbtnAddHomologations.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDbtnAddHomologations.Location = New System.Drawing.Point(2, 2)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnAddHomologations, True)
        Me.INDbtnAddHomologations.Name = "INDbtnAddHomologations"
        Me.INDbtnAddHomologations.Size = New System.Drawing.Size(641, 36)
        Me.INDbtnAddHomologations.TabIndex = 0
        Me.INDbtnAddHomologations.Text = "Aceptar"
        '
        'viewHomologations
        '
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.viewHomologations.Appearance.FocusedRow.Options.UseBackColor = True
        Me.viewHomologations.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.viewHomologations.Appearance.GroupRow.Options.UseFont = True
        'Cadena reemplazada... True
        Me.viewHomologations.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.viewHomologations.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
        Me.viewHomologations.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.viewHomologations.Appearance.Row.Options.UseFont = True
        Me.viewHomologations.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.viewHomologations.Appearance.ViewCaption.Options.UseFont = True
        Me.viewHomologations.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGclState, Me.GridColumn2, Me.GridColumn1})
        Me.viewHomologations.GridControl = Me.INDgcHomologations
        Me.viewHomologations.Name = "viewHomologations"
        Me.viewHomologations.OptionsView.EnableAppearanceEvenRow = True
        Me.viewHomologations.OptionsView.EnableAppearanceOddRow = True
        Me.viewHomologations.OptionsView.ShowAutoFilterRow = True
        Me.viewHomologations.OptionsView.ShowDetailButtons = False
        Me.viewHomologations.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.viewHomologations, False)
        '
        'INDGclState
        '
        Me.INDGclState.Caption = " "
        Me.INDGclState.ColumnEdit = Me.INDrepCheckSelectHomologations
        Me.INDGclState.FieldName = "Activated"
        Me.INDGclState.Name = "INDGclState"
        Me.INDGclState.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGclState.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGclState.OptionsColumn.AllowMove = False
        Me.INDGclState.OptionsColumn.AllowShowHide = False
        Me.INDGclState.OptionsColumn.AllowSize = False
        Me.INDGclState.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGclState.OptionsColumn.FixedWidth = True
        Me.INDGclState.OptionsFilter.AllowAutoFilter = False
        Me.INDGclState.OptionsFilter.AllowFilter = False
        Me.INDGclState.Visible = True
        Me.INDGclState.VisibleIndex = 0
        Me.INDGclState.Width = 36
        '
        'INDrepCheckSelectHomologations
        '
        Me.INDrepCheckSelectHomologations.AutoHeight = False
        Me.INDrepCheckSelectHomologations.Name = "INDrepCheckSelectHomologations"
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "CUPS"
        Me.GridColumn2.FieldName = "CodeNameCupsEntity"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 677
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Servicio IPS"
        Me.GridColumn1.FieldName = "CodeNameIpsService"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 2
        Me.GridColumn1.Width = 679
        '
        'INDgcHomologations
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcHomologations, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcHomologations, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcHomologations, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcHomologations, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcHomologations, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcHomologations, False)
        Me.INDgcHomologations.Location = New System.Drawing.Point(12, 12)
        Me.INDgcHomologations.MainView = Me.viewHomologations
        Me.INDgcHomologations.Name = "INDgcHomologations"
        Me.INDgcHomologations.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrepCheckSelectHomologations})
        Me.INDgcHomologations.Size = New System.Drawing.Size(621, 236)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcHomologations, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.IndigoGridControl1.SetSizeLayoutItem(Me.INDgcHomologations, New System.Drawing.Size(621, 0))
        Me.INDgcHomologations.TabIndex = 4
        Me.INDgcHomologations.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.viewHomologations})
        '
        'INDpanelButton
        '
        Me.INDpanelButton.Controls.Add(Me.INDbtnAddHomologations)
        Me.INDpanelButton.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.INDpanelButton.Location = New System.Drawing.Point(2, 267)
        Me.INDpanelButton.Name = "INDpanelButton"
        Me.INDpanelButton.Size = New System.Drawing.Size(645, 40)
        Me.INDpanelButton.TabIndex = 0
        '
        'INDlyPrincipal
        '
        Me.INDlyPrincipal.Controls.Add(Me.INDgcHomologations)
        Me.INDlyPrincipal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyPrincipal.Location = New System.Drawing.Point(2, 7)
        Me.INDlyPrincipal.Name = "INDlyPrincipal"
        Me.INDlyPrincipal.Root = Me.LayoutControlGroup1
        Me.INDlyPrincipal.Size = New System.Drawing.Size(645, 260)
        Me.INDlyPrincipal.TabIndex = 1
        Me.INDlyPrincipal.Text = "LayoutControl1"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemHomologations})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(645, 260)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlyItemHomologations
        '
        Me.INDlyItemHomologations.Control = Me.INDgcHomologations
        Me.INDlyItemHomologations.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemHomologations.Name = "INDlyItemHomologations"
        Me.INDlyItemHomologations.Size = New System.Drawing.Size(625, 240)
        Me.INDlyItemHomologations.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemHomologations.TextVisible = False
        '
        'FrmSelectHomologations
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(649, 426)
        Me.ControlBox = False
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmSelectHomologations"
        Me.Opacity = 1.0R
        Me.Text = "Homologaciones"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.viewHomologations, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepCheckSelectHomologations, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcHomologations, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpanelButton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpanelButton.ResumeLayout(False)
        CType(Me.INDlyPrincipal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyPrincipal.ResumeLayout(False)
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemHomologations, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents INDpanelButton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDbtnAddHomologations As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDlyPrincipal As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDgcHomologations As DevExpress.XtraGrid.GridControl
    Friend WithEvents viewHomologations As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItemHomologations As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGclState As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepCheckSelectHomologations As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
End Class
