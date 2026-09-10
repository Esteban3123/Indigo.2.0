Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSelectContract
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
        Me.INDlySelectContract = New DevExpress.XtraLayout.LayoutControl()
        Me.INDgcSelectContract = New DevExpress.XtraGrid.GridControl()
        Me.ViewSelectOption = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepCheckSelection = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepCbeOption = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemSelectContract = New DevExpress.XtraLayout.LayoutControlItem()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.INDbtnAccept = New DevExpress.XtraEditors.SimpleButton()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup()
        'Me.IndigoLayoutControl1 = New Presentation.Controls.IndigoLayoutControl()
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlySelectContract, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlySelectContract.SuspendLayout()
        CType(Me.INDgcSelectContract, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewSelectOption, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepCheckSelection, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepCbeOption, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemSelectContract, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        'CType(Me.IndigoLayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlySelectContract)
        Me.INDPanelControlBase.Controls.Add(Me.PanelControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 117)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(659, 280)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(659, 94)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(659, 94)
        '
        'INDlySelectContract
        '
        Me.INDlySelectContract.Controls.Add(Me.INDgcSelectContract)
        Me.INDlySelectContract.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlySelectContract.Location = New System.Drawing.Point(2, 7)
        Me.INDlySelectContract.Name = "INDlySelectContract"
        Me.INDlySelectContract.Root = Me.LayoutControlGroup1
        Me.INDlySelectContract.Size = New System.Drawing.Size(655, 233)
        Me.INDlySelectContract.TabIndex = 0
        Me.INDlySelectContract.Text = "LayoutControl1"
        '
        'INDgcSelectContract
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcSelectContract, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcSelectContract, Nothing)
        Me.INDgcSelectContract.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.INDgcSelectContract, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcSelectContract, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcSelectContract, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcSelectContract, False)
        Me.INDgcSelectContract.Location = New System.Drawing.Point(12, 12)
        Me.INDgcSelectContract.MainView = Me.ViewSelectOption
        Me.INDgcSelectContract.Name = "INDgcSelectContract"
        Me.INDgcSelectContract.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrepCheckSelection, Me.INDrepCbeOption})
        Me.INDgcSelectContract.Size = New System.Drawing.Size(631, 209)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcSelectContract, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcSelectContract.TabIndex = 4
        Me.INDgcSelectContract.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewSelectOption})
        '
        'ViewSelectOption
        '
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.ViewSelectOption.Appearance.FocusedRow.Options.UseBackColor = True
        Me.ViewSelectOption.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.ViewSelectOption.Appearance.GroupRow.Options.UseFont = True
        'Cadena reemplazada... True
        Me.ViewSelectOption.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.ViewSelectOption.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
        Me.ViewSelectOption.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.ViewSelectOption.Appearance.Row.Options.UseFont = True
        Me.ViewSelectOption.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.ViewSelectOption.Appearance.ViewCaption.Options.UseFont = True
        Me.ViewSelectOption.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4})
        Me.ViewSelectOption.GridControl = Me.INDgcSelectContract
        Me.ViewSelectOption.Name = "ViewSelectOption"
        Me.ViewSelectOption.OptionsView.EnableAppearanceEvenRow = True
        Me.ViewSelectOption.OptionsView.EnableAppearanceOddRow = True
        Me.ViewSelectOption.OptionsView.ShowAutoFilterRow = True
        Me.ViewSelectOption.OptionsView.ShowDetailButtons = False
        Me.ViewSelectOption.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.ViewSelectOption, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Sel."
        Me.GridColumn1.ColumnEdit = Me.INDrepCheckSelection
        Me.GridColumn1.FieldName = "SelectOption"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 20
        '
        'INDrepCheckSelection
        '
        Me.INDrepCheckSelection.AutoHeight = False
        Me.INDrepCheckSelection.Name = "INDrepCheckSelection"
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Código"
        Me.GridColumn2.FieldName = "MedicalFeesContractId.Code"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 90
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Nombre"
        Me.GridColumn3.FieldName = "MedicalFeesContractId.ContractName"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        Me.GridColumn3.Width = 397
        '
        'GridColumn4
        '
        Me.GridColumn4.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn4.Caption = "Liq. x Def."
        Me.GridColumn4.ColumnEdit = Me.INDrepCbeOption
        Me.GridColumn4.FieldName = "LiquidateDefault"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 3
        Me.GridColumn4.Width = 106
        '
        'INDrepCbeOption
        '
        Me.INDrepCbeOption.AutoHeight = False
        Me.INDrepCbeOption.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrepCbeOption.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Si", True, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("No", False, -1)})
        Me.INDrepCbeOption.Name = "INDrepCbeOption"
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemSelectContract})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(655, 233)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlyItemSelectContract
        '
        Me.INDlyItemSelectContract.Control = Me.INDgcSelectContract
        Me.INDlyItemSelectContract.CustomizationFormText = "INDlyItemSelectContract"
        Me.INDlyItemSelectContract.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemSelectContract.Name = "INDlyItemSelectContract"
        Me.INDlyItemSelectContract.Size = New System.Drawing.Size(635, 213)
        Me.INDlyItemSelectContract.Text = "INDlyItemSelectContract"
        Me.INDlyItemSelectContract.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemSelectContract.TextToControlDistance = 0
        Me.INDlyItemSelectContract.TextVisible = False
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.INDbtnAccept)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(2, 240)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(655, 38)
        Me.PanelControl1.TabIndex = 1
        '
        'INDbtnAccept
        '
        Me.INDbtnAccept.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDbtnAccept.Location = New System.Drawing.Point(2, 2)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnAccept, False)
        Me.INDbtnAccept.Name = "INDbtnAccept"
        Me.INDbtnAccept.Size = New System.Drawing.Size(651, 34)
        Me.INDbtnAccept.TabIndex = 0
        Me.INDbtnAccept.Text = "Aceptar"
        '
        'IndigoLayoutControl1
        '

        'Me.IndigoLayoutControl1.FormName = "FormBase20150527"

        '
        'FrmSelectContract
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(659, 397)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmSelectContract"
        Me.Opacity = 1.0R
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Seleccionar Contrato"
        Me.ViewModeEditHold = True
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlySelectContract, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlySelectContract.ResumeLayout(False)
        CType(Me.INDgcSelectContract, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewSelectOption, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepCheckSelection, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepCbeOption, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemSelectContract, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        'CType(Me.IndigoLayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDlySelectContract As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDbtnAccept As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDgcSelectContract As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewSelectOption As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItemSelectContract As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents INDrepCheckSelection As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepCbeOption As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
End Class
