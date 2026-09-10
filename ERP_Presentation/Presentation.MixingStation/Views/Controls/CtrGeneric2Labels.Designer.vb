Imports DevExpress.XtraEditors

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CtrGeneric2Labels
    Inherits XtraUserControl

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDpceBatchCodes = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.PopupContainerControl1 = New DevExpress.XtraEditors.PopupContainerControl()
        Me.LayoutControl2 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDgcBatchCodes = New DevExpress.XtraGrid.GridControl()
        Me.INDgvBatchCodes = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemComment = New DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDliAdvanceDatasource = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDlbLabel2 = New DevExpress.XtraEditors.LabelControl()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDlbLabel1 = New DevExpress.XtraEditors.LabelControl()
        Me.INDlcgGeneric2Labels = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciLabel2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciLabel1 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpceBatchCodes.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PopupContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PopupContainerControl1.SuspendLayout()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl2.SuspendLayout()
        CType(Me.INDgcBatchCodes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvBatchCodes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemComment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliAdvanceDatasource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDlcgGeneric2Labels, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'INDpceBatchCodes
        '
        Me.INDpceBatchCodes.Location = New System.Drawing.Point(2, 64)
        Me.INDpceBatchCodes.Name = "INDpceBatchCodes"
        Me.INDpceBatchCodes.Properties.PopupControl = Me.PopupContainerControl1
        Me.INDpceBatchCodes.Properties.PopupSizeable = False
        Me.INDpceBatchCodes.Properties.ShowPopupCloseButton = False
        Me.INDpceBatchCodes.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        Me.INDpceBatchCodes.Size = New System.Drawing.Size(163, 10)
        Me.INDpceBatchCodes.TabIndex = 1
        '
        'PopupContainerControl1
        '
        Me.PopupContainerControl1.Controls.Add(Me.LayoutControl2)
        Me.PopupContainerControl1.Location = New System.Drawing.Point(0, 119)
        Me.PopupContainerControl1.Name = "PopupContainerControl1"
        Me.PopupContainerControl1.Size = New System.Drawing.Size(586, 275)
        Me.PopupContainerControl1.TabIndex = 6
        '
        'LayoutControl2
        '
        Me.LayoutControl2.Controls.Add(Me.INDgcBatchCodes)
        Me.LayoutControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl2.Name = "LayoutControl2"
        Me.LayoutControl2.Root = Me.LayoutControlGroup1
        Me.LayoutControl2.Size = New System.Drawing.Size(586, 275)
        Me.LayoutControl2.TabIndex = 1
        Me.LayoutControl2.Text = "LayoutControl2"
        '
        'INDgcBatchCodes
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcBatchCodes, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcBatchCodes, Nothing)
        Me.INDgcBatchCodes.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.INDgcBatchCodes, True)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcBatchCodes, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcBatchCodes, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcBatchCodes, False)
        Me.INDgcBatchCodes.Location = New System.Drawing.Point(12, 12)
        Me.INDgcBatchCodes.MainView = Me.INDgvBatchCodes
        Me.INDgcBatchCodes.Name = "INDgcBatchCodes"
        Me.INDgcBatchCodes.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemComment})
        Me.INDgcBatchCodes.Size = New System.Drawing.Size(562, 251)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcBatchCodes, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcBatchCodes.TabIndex = 0
        Me.INDgcBatchCodes.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvBatchCodes, Me.GridView1})
        '
        'INDgvBatchCodes
        '
        Me.INDgvBatchCodes.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvBatchCodes.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvBatchCodes.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDgvBatchCodes.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvBatchCodes.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvBatchCodes.Appearance.GroupPanel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDgvBatchCodes.Appearance.GroupPanel.Options.UseForeColor = True
        Me.INDgvBatchCodes.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvBatchCodes.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvBatchCodes.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvBatchCodes.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvBatchCodes.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvBatchCodes.Appearance.Row.Options.UseFont = True
        Me.INDgvBatchCodes.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgvBatchCodes.Appearance.ViewCaption.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDgvBatchCodes.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvBatchCodes.Appearance.ViewCaption.Options.UseForeColor = True
        Me.INDgvBatchCodes.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn8})
        Me.INDgvBatchCodes.GridControl = Me.INDgcBatchCodes
        Me.INDgvBatchCodes.Name = "INDgvBatchCodes"
        Me.INDgvBatchCodes.OptionsFind.AlwaysVisible = True
        Me.INDgvBatchCodes.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvBatchCodes.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvBatchCodes.OptionsView.RowAutoHeight = True
        Me.INDgvBatchCodes.OptionsView.ShowAutoFilterRow = True
        Me.INDgvBatchCodes.OptionsView.ShowDetailButtons = False
        Me.INDgvBatchCodes.OptionsView.ShowGroupPanel = False
        Me.INDgvBatchCodes.OptionsView.ShowViewCaption = True
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvBatchCodes, False)
        Me.INDgvBatchCodes.ViewCaption = "Lotes"
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Lote"
        Me.GridColumn8.FieldName = "BatchCode"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.OptionsColumn.AllowEdit = False
        Me.GridColumn8.OptionsColumn.AllowFocus = False
        Me.GridColumn8.OptionsColumn.AllowMove = False
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 0
        Me.GridColumn8.Width = 68
        '
        'RepositoryItemComment
        '
        Me.RepositoryItemComment.AutoHeight = False
        Me.RepositoryItemComment.Name = "RepositoryItemComment"
        Me.RepositoryItemComment.ReadOnly = True
        '
        'GridView1
        '
        Me.GridView1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView1.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView1.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.GroupRow.Options.UseFont = True
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.GridControl = Me.INDgcBatchCodes
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView1, False)
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDliAdvanceDatasource})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(586, 275)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDliAdvanceDatasource
        '
        Me.INDliAdvanceDatasource.Control = Me.INDgcBatchCodes
        Me.INDliAdvanceDatasource.CustomizationFormText = "LayoutControlItem1"
        Me.INDliAdvanceDatasource.Location = New System.Drawing.Point(0, 0)
        Me.INDliAdvanceDatasource.Name = "INDliAdvanceDatasource"
        Me.INDliAdvanceDatasource.Size = New System.Drawing.Size(566, 255)
        Me.INDliAdvanceDatasource.TextSize = New System.Drawing.Size(0, 0)
        Me.INDliAdvanceDatasource.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Código"
        Me.GridColumn1.FieldName = "Code"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 341
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Cuenta"
        Me.GridColumn3.FieldName = "FullNameMainAccount"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 1
        Me.GridColumn3.Width = 514
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Valor"
        Me.GridColumn4.FieldName = "Balance"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 2
        Me.GridColumn4.Width = 519
        '
        'INDlbLabel2
        '
        Me.INDlbLabel2.Appearance.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlbLabel2.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDlbLabel2.Appearance.Options.UseFont = True
        Me.INDlbLabel2.Appearance.Options.UseForeColor = True
        Me.INDlbLabel2.Location = New System.Drawing.Point(5, 31)
        Me.INDlbLabel2.Name = "INDlbLabel2"
        Me.INDlbLabel2.Size = New System.Drawing.Size(270, 23)
        Me.INDlbLabel2.StyleController = Me.LayoutControl1
        Me.INDlbLabel2.TabIndex = 4
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.PopupContainerControl1)
        Me.LayoutControl1.Controls.Add(Me.INDlbLabel1)
        Me.LayoutControl1.Controls.Add(Me.INDlbLabel2)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.INDlcgGeneric2Labels
        Me.LayoutControl1.Size = New System.Drawing.Size(292, 70)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDlbLabel1
        '
        Me.INDlbLabel1.Appearance.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlbLabel1.Appearance.ForeColor = System.Drawing.Color.SteelBlue
        Me.INDlbLabel1.Appearance.Options.UseFont = True
        Me.INDlbLabel1.Appearance.Options.UseForeColor = True
        Me.INDlbLabel1.Location = New System.Drawing.Point(4, 0)
        Me.INDlbLabel1.Name = "INDlbLabel1"
        Me.INDlbLabel1.Size = New System.Drawing.Size(272, 30)
        Me.INDlbLabel1.StyleController = Me.LayoutControl1
        Me.INDlbLabel1.TabIndex = 5
        '
        'INDlcgGeneric2Labels
        '
        Me.INDlcgGeneric2Labels.CustomizationFormText = "Generico"
        Me.INDlcgGeneric2Labels.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlcgGeneric2Labels.GroupBordersVisible = False
        Me.INDlcgGeneric2Labels.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciLabel2, Me.INDLciLabel1})
        Me.INDlcgGeneric2Labels.Name = "INDlcgGeneric2Labels"
        Me.INDlcgGeneric2Labels.Padding = New DevExpress.XtraLayout.Utils.Padding(4, 0, 0, 0)
        Me.INDlcgGeneric2Labels.Size = New System.Drawing.Size(292, 70)
        Me.INDlcgGeneric2Labels.TextVisible = False
        '
        'INDLciLabel2
        '
        Me.INDLciLabel2.Control = Me.INDlbLabel2
        Me.INDLciLabel2.CustomizationFormText = "LayoutControlItem1"
        Me.INDLciLabel2.Location = New System.Drawing.Point(0, 30)
        Me.INDLciLabel2.MaxSize = New System.Drawing.Size(272, 25)
        Me.INDLciLabel2.MinSize = New System.Drawing.Size(272, 25)
        Me.INDLciLabel2.Name = "INDLciLabel2"
        Me.INDLciLabel2.Padding = New DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1)
        Me.INDLciLabel2.Size = New System.Drawing.Size(288, 40)
        Me.INDLciLabel2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciLabel2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciLabel2.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciLabel2.TextToControlDistance = 0
        Me.INDLciLabel2.TextVisible = False
        '
        'INDLciLabel1
        '
        Me.INDLciLabel1.Control = Me.INDlbLabel1
        Me.INDLciLabel1.CustomizationFormText = "LayoutControlItem2"
        Me.INDLciLabel1.Location = New System.Drawing.Point(0, 0)
        Me.INDLciLabel1.MaxSize = New System.Drawing.Size(272, 30)
        Me.INDLciLabel1.MinSize = New System.Drawing.Size(272, 30)
        Me.INDLciLabel1.Name = "INDLciLabel1"
        Me.INDLciLabel1.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.INDLciLabel1.Size = New System.Drawing.Size(288, 30)
        Me.INDLciLabel1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciLabel1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciLabel1.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciLabel1.TextToControlDistance = 0
        Me.INDLciLabel1.TextVisible = False
        '
        'CtrGeneric2Labels
        '
        Me.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LayoutControl1)
        Me.Controls.Add(Me.INDpceBatchCodes)
        Me.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Margin = New System.Windows.Forms.Padding(0)
        Me.MaximumSize = New System.Drawing.Size(292, 70)
        Me.MinimumSize = New System.Drawing.Size(292, 70)
        Me.Name = "CtrGeneric2Labels"
        Me.Size = New System.Drawing.Size(292, 70)
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpceBatchCodes.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PopupContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PopupContainerControl1.ResumeLayout(False)
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl2.ResumeLayout(False)
        CType(Me.INDgcBatchCodes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvBatchCodes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemComment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliAdvanceDatasource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDlcgGeneric2Labels, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDpceBatchCodes As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlbLabel2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents PopupContainerControl1 As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents LayoutControl2 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDgcBatchCodes As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvBatchCodes As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryItemComment As DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDliAdvanceDatasource As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlbLabel1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlcgGeneric2Labels As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciLabel1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciLabel2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemPopupContainerEdit1 As Repository.RepositoryItemPopupContainerEdit
End Class
