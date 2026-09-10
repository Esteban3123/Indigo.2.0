<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPopupCategory
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
        Me.components = New System.ComponentModel.Container()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.INDBtnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDGcCategoty = New DevExpress.XtraGrid.GridControl()
        Me.INDGvCategory = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemImageComboBox1 = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDGcCategoty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemImageComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.INDBtnAdd)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(0, 500)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(880, 36)
        Me.PanelControl1.TabIndex = 0
        '
        'INDBtnAdd
        '
        Me.INDBtnAdd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDBtnAdd.Location = New System.Drawing.Point(2, 2)
        Me.INDBtnAdd.Name = "INDBtnAdd"
        Me.INDBtnAdd.Size = New System.Drawing.Size(876, 32)
        Me.INDBtnAdd.TabIndex = 0
        Me.INDBtnAdd.Text = "Agregar"
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDGcCategoty)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(880, 500)
        Me.LayoutControl1.TabIndex = 1
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDGcCategoty
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcCategoty, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcCategoty, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcCategoty, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcCategoty, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcCategoty, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcCategoty, False)
        Me.INDGcCategoty.Location = New System.Drawing.Point(12, 12)
        Me.INDGcCategoty.MainView = Me.INDGvCategory
        Me.INDGcCategoty.Name = "INDGcCategoty"
        Me.INDGcCategoty.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemImageComboBox1})
        Me.INDGcCategoty.Size = New System.Drawing.Size(856, 476)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcCategoty, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.IndigoGridControl1.SetSizeLayoutItem(Me.INDGcCategoty, New System.Drawing.Size(860, 0))
        Me.INDGcCategoty.TabIndex = 4
        Me.INDGcCategoty.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvCategory})
        '
        'INDGvCategory
        '
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.INDGvCategory.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDGvCategory.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDGvCategory.Appearance.GroupRow.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDGvCategory.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDGvCategory.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDGvCategory.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvCategory.Appearance.Row.Options.UseFont = True
        Me.INDGvCategory.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvCategory.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvCategory.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4})
        Me.INDGvCategory.GridControl = Me.INDGcCategoty
        Me.INDGvCategory.Name = "INDGvCategory"
        Me.INDGvCategory.OptionsSelection.MultiSelect = True
        Me.INDGvCategory.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvCategory.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvCategory.OptionsView.ShowAutoFilterRow = True
        Me.INDGvCategory.OptionsView.ShowDetailButtons = False
        Me.INDGvCategory.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvCategory, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Rubro"
        Me.GridColumn1.FieldName = "CategoryId.NameCode"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 518
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Recurso"
        Me.GridColumn2.FieldName = "CategoryId.FinancialSourceId.NameCode"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 410
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Mes"
        Me.GridColumn3.ColumnEdit = Me.RepositoryItemImageComboBox1
        Me.GridColumn3.FieldName = "Month"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        Me.GridColumn3.Width = 219
        '
        'RepositoryItemImageComboBox1
        '
        Me.RepositoryItemImageComboBox1.AutoHeight = False
        Me.RepositoryItemImageComboBox1.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Enero", CType(1, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Febrero", CType(2, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Marzo", CType(3, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Abril", CType(4, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Mayo", CType(5, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Junio", CType(6, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Julio", CType(7, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Agosto", CType(8, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Septiembre", CType(9, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Octubre", CType(10, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Noviembre", CType(11, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Diciembre", CType(12, Byte), -1)})
        Me.RepositoryItemImageComboBox1.Name = "RepositoryItemImageComboBox1"
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Saldo"
        Me.GridColumn4.DisplayFormat.FormatString = "c0"
        Me.GridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn4.FieldName = "Balance"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 3
        Me.GridColumn4.Width = 245
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
        Me.LayoutControlItem1.Control = Me.INDGcCategoty
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(860, 480)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'FrmPopupCategory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(880, 536)
        Me.Controls.Add(Me.LayoutControl1)
        Me.Controls.Add(Me.PanelControl1)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmPopupCategory"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Rubros - PAC"
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDGcCategoty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvCategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemImageComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDBtnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDGcCategoty As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvCategory As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents RepositoryItemImageComboBox1 As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
End Class
