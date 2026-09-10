<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPopupAddCommitment
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
        Me.INDGcCommitment = New DevExpress.XtraGrid.GridControl()
        Me.INDGvCommitment = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRpIcbCommitmentType = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRpIcbBudgetType = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDGcCommitment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvCommitment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRpIcbCommitmentType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRpIcbBudgetType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.INDBtnAdd)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(0, 602)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(907, 36)
        Me.PanelControl1.TabIndex = 0
        '
        'INDBtnAdd
        '
        Me.INDBtnAdd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDBtnAdd.Location = New System.Drawing.Point(2, 2)
        Me.INDBtnAdd.Name = "INDBtnAdd"
        Me.INDBtnAdd.Size = New System.Drawing.Size(903, 32)
        Me.INDBtnAdd.TabIndex = 0
        Me.INDBtnAdd.Text = "Agregar"
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDGcCommitment)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(907, 602)
        Me.LayoutControl1.TabIndex = 1
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDGcCommitment
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcCommitment, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcCommitment, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcCommitment, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcCommitment, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcCommitment, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcCommitment, False)
        Me.INDGcCommitment.Location = New System.Drawing.Point(12, 12)
        Me.INDGcCommitment.MainView = Me.INDGvCommitment
        Me.INDGcCommitment.Name = "INDGcCommitment"
        Me.INDGcCommitment.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDRpIcbBudgetType, Me.INDRpIcbCommitmentType})
        Me.INDGcCommitment.Size = New System.Drawing.Size(883, 578)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcCommitment, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.IndigoGridControl1.SetSizeLayoutItem(Me.INDGcCommitment, New System.Drawing.Size(887, 0))
        Me.INDGcCommitment.TabIndex = 4
        Me.INDGcCommitment.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvCommitment})
        '
        'INDGvCommitment
        '
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.INDGvCommitment.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDGvCommitment.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDGvCommitment.Appearance.GroupRow.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDGvCommitment.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDGvCommitment.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDGvCommitment.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvCommitment.Appearance.Row.Options.UseFont = True
        Me.INDGvCommitment.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvCommitment.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvCommitment.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6, Me.GridColumn7})
        Me.INDGvCommitment.GridControl = Me.INDGcCommitment
        Me.INDGvCommitment.GroupCount = 1
        Me.INDGvCommitment.Name = "INDGvCommitment"
        Me.INDGvCommitment.OptionsSelection.MultiSelect = True
        Me.INDGvCommitment.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvCommitment.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvCommitment.OptionsView.ShowAutoFilterRow = True
        Me.INDGvCommitment.OptionsView.ShowDetailButtons = False
        Me.INDGvCommitment.OptionsView.ShowGroupPanel = False
        Me.INDGvCommitment.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn1, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvCommitment, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Tipo de Compromiso"
        Me.GridColumn1.ColumnEdit = Me.INDRpIcbCommitmentType
        Me.GridColumn1.FieldName = "CommitmentId.CommitmentType"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'INDRpIcbCommitmentType
        '
        Me.INDRpIcbCommitmentType.AutoHeight = False
        Me.INDRpIcbCommitmentType.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Compromiso", CType(1, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Reserva", CType(2, Byte), -1)})
        Me.INDRpIcbCommitmentType.Name = "INDRpIcbCommitmentType"
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Código"
        Me.GridColumn2.FieldName = "CommitmentId.Code"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Documento"
        Me.GridColumn3.FieldName = "CommitmentId.Document"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 1
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Rubro"
        Me.GridColumn4.FieldName = "CategoryId.NameCode"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 2
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Recurso"
        Me.GridColumn5.FieldName = "CategoryId.FinancialSourceId.NameCode"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.AllowEdit = False
        Me.GridColumn5.OptionsColumn.AllowFocus = False
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 3
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Tipo"
        Me.GridColumn6.FieldName = "RevenueTypeId.NameCode"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.AllowEdit = False
        Me.GridColumn6.OptionsColumn.AllowFocus = False
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 4
        '
        'INDRpIcbBudgetType
        '
        Me.INDRpIcbBudgetType.AutoHeight = False
        Me.INDRpIcbBudgetType.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Ingreso", CType(1, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Gasto", CType(2, Byte), -1)})
        Me.INDRpIcbBudgetType.Name = "INDRpIcbBudgetType"
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Saldo"
        Me.GridColumn7.DisplayFormat.FormatString = "c0"
        Me.GridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn7.FieldName = "Balance"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsColumn.AllowEdit = False
        Me.GridColumn7.OptionsColumn.AllowFocus = False
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 5
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(907, 602)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDGcCommitment
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(887, 582)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'FrmPopupAddCommitment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(907, 638)
        Me.Controls.Add(Me.LayoutControl1)
        Me.Controls.Add(Me.PanelControl1)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmPopupAddCommitment"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Compromisos"
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDGcCommitment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvCommitment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRpIcbCommitmentType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRpIcbBudgetType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDBtnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDGcCommitment As DevExpress.XtraGrid.GridControl
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents INDGvCommitment As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRpIcbBudgetType As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRpIcbCommitmentType As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
End Class
