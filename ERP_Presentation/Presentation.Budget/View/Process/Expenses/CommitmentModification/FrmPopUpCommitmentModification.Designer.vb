<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPopUpCommitmentModification
    Inherits DevExpress.XtraEditors.XtraForm

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
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.INDbtnAddCategory = New DevExpress.XtraEditors.SimpleButton()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDGcCategoryBudget = New DevExpress.XtraGrid.GridControl()
        Me.INDGvCategoryBudget = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDcolAvailability = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolCategoryCodePop = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolCategoryNamePop = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolFinancialSourcePop = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolRevuenePop = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolBalancePop = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcCategoryBudget, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvCategoryBudget, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.INDbtnAddCategory)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(0, 500)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(880, 36)
        Me.PanelControl1.TabIndex = 0
        '
        'INDbtnAddCategory
        '
        Me.INDbtnAddCategory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDbtnAddCategory.Location = New System.Drawing.Point(2, 2)
        Me.INDbtnAddCategory.Name = "INDbtnAddCategory"
        Me.INDbtnAddCategory.Size = New System.Drawing.Size(876, 32)
        Me.INDbtnAddCategory.TabIndex = 0
        Me.INDbtnAddCategory.Text = "Agregar"
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDGcCategoryBudget)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(880, 500)
        Me.LayoutControl1.TabIndex = 1
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(880, 500)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDGcCategoryBudget
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcCategoryBudget, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcCategoryBudget, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcCategoryBudget, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcCategoryBudget, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcCategoryBudget, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcCategoryBudget, False)
        Me.INDGcCategoryBudget.Location = New System.Drawing.Point(12, 12)
        Me.INDGcCategoryBudget.MainView = Me.INDGvCategoryBudget
        Me.INDGcCategoryBudget.Name = "INDGcCategoryBudget"
        Me.INDGcCategoryBudget.Size = New System.Drawing.Size(856, 476)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcCategoryBudget, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.IndigoGridControl1.SetSizeLayoutItem(Me.INDGcCategoryBudget, New System.Drawing.Size(860, 0))
        Me.INDGcCategoryBudget.TabIndex = 6
        Me.INDGcCategoryBudget.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvCategoryBudget})
        '
        'INDGvCategoryBudget
        '
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.INDGvCategoryBudget.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDGvCategoryBudget.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDGvCategoryBudget.Appearance.GroupRow.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDGvCategoryBudget.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDGvCategoryBudget.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDGvCategoryBudget.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvCategoryBudget.Appearance.Row.Options.UseFont = True
        Me.INDGvCategoryBudget.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvCategoryBudget.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvCategoryBudget.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDcolAvailability, Me.INDcolCategoryCodePop, Me.INDcolCategoryNamePop, Me.INDcolFinancialSourcePop, Me.INDcolRevuenePop, Me.INDcolBalancePop})
        Me.INDGvCategoryBudget.GridControl = Me.INDGcCategoryBudget
        Me.INDGvCategoryBudget.Name = "INDGvCategoryBudget"
        Me.INDGvCategoryBudget.OptionsCustomization.AllowGroup = False
        Me.INDGvCategoryBudget.OptionsDetail.EnableMasterViewMode = False
        Me.INDGvCategoryBudget.OptionsDetail.ShowDetailTabs = False
        Me.INDGvCategoryBudget.OptionsSelection.MultiSelect = True
        Me.INDGvCategoryBudget.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvCategoryBudget.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvCategoryBudget.OptionsView.ShowAutoFilterRow = True
        Me.INDGvCategoryBudget.OptionsView.ShowDetailButtons = False
        Me.INDGvCategoryBudget.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvCategoryBudget, False)
        '
        'INDcolAvailability
        '
        Me.INDcolAvailability.Caption = "No. Disponibilidad"
        Me.INDcolAvailability.FieldName = "CodeAvailability"
        Me.INDcolAvailability.Name = "INDcolAvailability"
        Me.INDcolAvailability.OptionsColumn.AllowEdit = False
        Me.INDcolAvailability.OptionsColumn.AllowFocus = False
        Me.INDcolAvailability.Visible = True
        Me.INDcolAvailability.VisibleIndex = 0
        '
        'INDcolCategoryCodePop
        '
        Me.INDcolCategoryCodePop.Caption = "Codigo Rubro"
        Me.INDcolCategoryCodePop.FieldName = "CodeCategory"
        Me.INDcolCategoryCodePop.Name = "INDcolCategoryCodePop"
        Me.INDcolCategoryCodePop.OptionsColumn.AllowEdit = False
        Me.INDcolCategoryCodePop.OptionsColumn.AllowFocus = False
        Me.INDcolCategoryCodePop.Visible = True
        Me.INDcolCategoryCodePop.VisibleIndex = 1
        '
        'INDcolCategoryNamePop
        '
        Me.INDcolCategoryNamePop.Caption = "Nombre Rubro"
        Me.INDcolCategoryNamePop.FieldName = "NameCategory"
        Me.INDcolCategoryNamePop.Name = "INDcolCategoryNamePop"
        Me.INDcolCategoryNamePop.OptionsColumn.AllowEdit = False
        Me.INDcolCategoryNamePop.OptionsColumn.AllowFocus = False
        Me.INDcolCategoryNamePop.Visible = True
        Me.INDcolCategoryNamePop.VisibleIndex = 2
        '
        'INDcolFinancialSourcePop
        '
        Me.INDcolFinancialSourcePop.Caption = "Recurso"
        Me.INDcolFinancialSourcePop.FieldName = "CodeNameFinancialSource"
        Me.INDcolFinancialSourcePop.Name = "INDcolFinancialSourcePop"
        Me.INDcolFinancialSourcePop.OptionsColumn.AllowEdit = False
        Me.INDcolFinancialSourcePop.OptionsColumn.AllowFocus = False
        Me.INDcolFinancialSourcePop.Visible = True
        Me.INDcolFinancialSourcePop.VisibleIndex = 3
        '
        'INDcolRevuenePop
        '
        Me.INDcolRevuenePop.Caption = "Tipo"
        Me.INDcolRevuenePop.FieldName = "CodeNameRevenueType"
        Me.INDcolRevuenePop.Name = "INDcolRevuenePop"
        Me.INDcolRevuenePop.OptionsColumn.AllowEdit = False
        Me.INDcolRevuenePop.OptionsColumn.AllowFocus = False
        Me.INDcolRevuenePop.Visible = True
        Me.INDcolRevuenePop.VisibleIndex = 4
        '
        'INDcolBalancePop
        '
        Me.INDcolBalancePop.Caption = "Saldo"
        Me.INDcolBalancePop.DisplayFormat.FormatString = "c0"
        Me.INDcolBalancePop.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.INDcolBalancePop.FieldName = "Balance"
        Me.INDcolBalancePop.Name = "INDcolBalancePop"
        Me.INDcolBalancePop.OptionsColumn.AllowEdit = False
        Me.INDcolBalancePop.OptionsColumn.AllowFocus = False
        Me.INDcolBalancePop.Visible = True
        Me.INDcolBalancePop.VisibleIndex = 5
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDGcCategoryBudget
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(860, 480)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'FrmPopUpCommitmentModification
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(880, 536)
        Me.Controls.Add(Me.LayoutControl1)
        Me.Controls.Add(Me.PanelControl1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmPopUpCommitmentModification"
        Me.ShowIcon = False
        Me.Text = "Disponibilidades y Rubros"
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcCategoryBudget, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvCategoryBudget, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDbtnAddCategory As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDGcCategoryBudget As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvCategoryBudget As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDcolAvailability As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolCategoryCodePop As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolCategoryNamePop As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolFinancialSourcePop As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolRevuenePop As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolBalancePop As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
End Class
