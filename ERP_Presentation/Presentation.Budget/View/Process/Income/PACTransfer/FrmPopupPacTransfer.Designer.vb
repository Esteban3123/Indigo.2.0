<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPopupPacTransfer
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
        Me.components = New System.ComponentModel.Container()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.INDgvCategoryPac = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDgcolCategory = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcolRevuene = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcolMonth = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcolBalance = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGcCategoryPac = New DevExpress.XtraGrid.GridControl()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.INDbtnAddCategory = New DevExpress.XtraEditors.SimpleButton()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvCategoryPac, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcCategoryPac, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDgvCategoryPac
        '
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.INDgvCategoryPac.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDgvCategoryPac.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDgvCategoryPac.Appearance.GroupRow.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDgvCategoryPac.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDgvCategoryPac.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDgvCategoryPac.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvCategoryPac.Appearance.Row.Options.UseFont = True
        Me.INDgvCategoryPac.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgvCategoryPac.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvCategoryPac.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDgcolCategory, Me.INDgcolRevuene, Me.INDgcolMonth, Me.INDgcolBalance})
        Me.INDgvCategoryPac.GridControl = Me.INDGcCategoryPac
        Me.INDgvCategoryPac.Name = "INDgvCategoryPac"
        Me.INDgvCategoryPac.OptionsCustomization.AllowGroup = False
        Me.INDgvCategoryPac.OptionsDetail.EnableMasterViewMode = False
        Me.INDgvCategoryPac.OptionsDetail.ShowDetailTabs = False
        Me.INDgvCategoryPac.OptionsSelection.MultiSelect = True
        Me.INDgvCategoryPac.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvCategoryPac.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvCategoryPac.OptionsView.ShowAutoFilterRow = True
        Me.INDgvCategoryPac.OptionsView.ShowDetailButtons = False
        Me.INDgvCategoryPac.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvCategoryPac, False)
        '
        'INDgcolCategory
        '
        Me.INDgcolCategory.Caption = "Rubro"
        Me.INDgcolCategory.FieldName = "CodeNameCategory"
        Me.INDgcolCategory.Name = "INDgcolCategory"
        Me.INDgcolCategory.OptionsColumn.AllowEdit = False
        Me.INDgcolCategory.OptionsColumn.AllowFocus = False
        Me.INDgcolCategory.Visible = True
        Me.INDgcolCategory.VisibleIndex = 0
        '
        'INDgcolRevuene
        '
        Me.INDgcolRevuene.Caption = "Recurso"
        Me.INDgcolRevuene.FieldName = "CodeNameFinancialSource"
        Me.INDgcolRevuene.Name = "INDgcolRevuene"
        Me.INDgcolRevuene.OptionsColumn.AllowEdit = False
        Me.INDgcolRevuene.OptionsColumn.AllowFocus = False
        Me.INDgcolRevuene.Visible = True
        Me.INDgcolRevuene.VisibleIndex = 1
        '
        'INDgcolMonth
        '
        Me.INDgcolMonth.Caption = "Mes"
        Me.INDgcolMonth.FieldName = "Month"
        Me.INDgcolMonth.Name = "INDgcolMonth"
        Me.INDgcolMonth.OptionsColumn.AllowEdit = False
        Me.INDgcolMonth.OptionsColumn.AllowFocus = False
        Me.INDgcolMonth.Visible = True
        Me.INDgcolMonth.VisibleIndex = 2
        '
        'INDgcolBalance
        '
        Me.INDgcolBalance.Caption = "Saldo"
        Me.INDgcolBalance.DisplayFormat.FormatString = "c0"
        Me.INDgcolBalance.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDgcolBalance.FieldName = "Balance"
        Me.INDgcolBalance.Name = "INDgcolBalance"
        Me.INDgcolBalance.OptionsColumn.AllowEdit = False
        Me.INDgcolBalance.OptionsColumn.AllowFocus = False
        Me.INDgcolBalance.Visible = True
        Me.INDgcolBalance.VisibleIndex = 3
        '
        'INDGcCategoryPac
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcCategoryPac, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcCategoryPac, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcCategoryPac, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcCategoryPac, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcCategoryPac, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcCategoryPac, False)
        Me.INDGcCategoryPac.Location = New System.Drawing.Point(12, 12)
        Me.INDGcCategoryPac.MainView = Me.INDgvCategoryPac
        Me.INDGcCategoryPac.Name = "INDGcCategoryPac"
        Me.INDGcCategoryPac.Size = New System.Drawing.Size(856, 476)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcCategoryPac, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.IndigoGridControl1.SetSizeLayoutItem(Me.INDGcCategoryPac, New System.Drawing.Size(856, 0))
        Me.INDGcCategoryPac.TabIndex = 4
        Me.INDGcCategoryPac.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvCategoryPac})
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDGcCategoryPac)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(880, 500)
        Me.LayoutControl1.TabIndex = 0
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
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDGcCategoryPac
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(860, 480)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.INDbtnAddCategory)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(0, 500)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(880, 36)
        Me.PanelControl1.TabIndex = 1
        '
        'INDbtnAddCategory
        '
        Me.INDbtnAddCategory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDbtnAddCategory.Location = New System.Drawing.Point(2, 2)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnAddCategory, False)
        Me.INDbtnAddCategory.Name = "INDbtnAddCategory"
        Me.INDbtnAddCategory.Size = New System.Drawing.Size(876, 32)
        Me.INDbtnAddCategory.TabIndex = 0
        Me.INDbtnAddCategory.Text = "Agregar"
        '
        'FrmPopupPacTransfer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(880, 536)
        Me.Controls.Add(Me.LayoutControl1)
        Me.Controls.Add(Me.PanelControl1)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmPopupPacTransfer"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Rubros - PAC"
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvCategoryPac, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcCategoryPac, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDbtnAddCategory As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents INDGcCategoryPac As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvCategoryPac As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDgcolCategory As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgcolRevuene As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgcolMonth As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgcolBalance As DevExpress.XtraGrid.Columns.GridColumn
End Class
