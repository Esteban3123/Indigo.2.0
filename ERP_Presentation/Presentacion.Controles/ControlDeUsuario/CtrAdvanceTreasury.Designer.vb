Imports DevExpress.XtraEditors

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrAdvanceTreasury
    Inherits XtraUserControl

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.PopupContainerControl1 = New DevExpress.XtraEditors.PopupContainerControl()
        Me.LayoutControl2 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDgcAdvance = New DevExpress.XtraGrid.GridControl()
        Me.INDgvAdvance = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemComment = New DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDliAdvanceDatasource = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlblAdvanceValue = New DevExpress.XtraEditors.LabelControl()
        Me.INDlblAdvanceTitle = New DevExpress.XtraEditors.LabelControl()
        Me.INDlcgAdvance = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDliAdvanceTitle = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliAdvance = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDpceAdvanceDetail = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.PopupContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PopupContainerControl1.SuspendLayout()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl2.SuspendLayout()
        CType(Me.INDgcAdvance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvAdvance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemComment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliAdvanceDatasource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgAdvance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliAdvanceTitle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliAdvance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpceAdvanceDetail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.PopupContainerControl1)
        Me.LayoutControl1.Controls.Add(Me.INDlblAdvanceValue)
        Me.LayoutControl1.Controls.Add(Me.INDlblAdvanceTitle)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.INDlcgAdvance
        Me.LayoutControl1.Size = New System.Drawing.Size(292, 62)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'PopupContainerControl1
        '
        Me.PopupContainerControl1.Controls.Add(Me.LayoutControl2)
        Me.PopupContainerControl1.Location = New System.Drawing.Point(0, 146)
        Me.PopupContainerControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PopupContainerControl1.Name = "PopupContainerControl1"
        Me.PopupContainerControl1.Size = New System.Drawing.Size(636, 338)
        Me.PopupContainerControl1.TabIndex = 6
        '
        'LayoutControl2
        '
        Me.LayoutControl2.Controls.Add(Me.INDgcAdvance)
        Me.LayoutControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LayoutControl2.Name = "LayoutControl2"
        Me.LayoutControl2.Root = Me.LayoutControlGroup1
        Me.LayoutControl2.Size = New System.Drawing.Size(636, 338)
        Me.LayoutControl2.TabIndex = 1
        Me.LayoutControl2.Text = "LayoutControl2"
        '
        'INDgcAdvance
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcAdvance, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcAdvance, Nothing)
        Me.INDgcAdvance.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcAdvance, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcAdvance, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcAdvance, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcAdvance, False)
        Me.INDgcAdvance.Location = New System.Drawing.Point(12, 12)
        Me.INDgcAdvance.MainView = Me.INDgvAdvance
        Me.INDgcAdvance.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDgcAdvance.Name = "INDgcAdvance"
        Me.INDgcAdvance.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemComment})
        Me.INDgcAdvance.Size = New System.Drawing.Size(612, 314)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcAdvance, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcAdvance.TabIndex = 0
        Me.INDgcAdvance.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvAdvance})
        '
        'INDgvAdvance
        '
        Me.INDgvAdvance.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvAdvance.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvAdvance.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDgvAdvance.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvAdvance.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvAdvance.Appearance.GroupPanel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDgvAdvance.Appearance.GroupPanel.Options.UseForeColor = True
        Me.INDgvAdvance.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvAdvance.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvAdvance.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvAdvance.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvAdvance.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvAdvance.Appearance.Row.Options.UseFont = True
        Me.INDgvAdvance.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgvAdvance.Appearance.ViewCaption.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDgvAdvance.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvAdvance.Appearance.ViewCaption.Options.UseForeColor = True
        Me.INDgvAdvance.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn2, Me.GridColumn5, Me.GridColumn7, Me.GridColumn6})
        Me.INDgvAdvance.GridControl = Me.INDgcAdvance
        Me.INDgvAdvance.Name = "INDgvAdvance"
        Me.INDgvAdvance.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvAdvance.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvAdvance.OptionsView.RowAutoHeight = True
        Me.INDgvAdvance.OptionsView.ShowAutoFilterRow = True
        Me.INDgvAdvance.OptionsView.ShowDetailButtons = False
        Me.INDgvAdvance.OptionsView.ShowGroupPanel = False
        Me.INDgvAdvance.OptionsView.ShowViewCaption = True
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvAdvance, False)
        Me.INDgvAdvance.ViewCaption = "Detalle Anticipos"
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Fecha"
        Me.GridColumn2.DisplayFormat.FormatString = "dd \de MMMM \de yyyy"
        Me.GridColumn2.FieldName = "DocumentDate"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.OptionsColumn.AllowMove = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        Me.GridColumn2.Width = 319
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Cuenta"
        Me.GridColumn5.FieldName = "FullNameMainAccount"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.AllowEdit = False
        Me.GridColumn5.OptionsColumn.AllowFocus = False
        Me.GridColumn5.OptionsColumn.AllowMove = False
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 1
        Me.GridColumn5.Width = 471
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Comentario"
        Me.GridColumn7.ColumnEdit = Me.RepositoryItemComment
        Me.GridColumn7.FieldName = "Comments"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsColumn.AllowMove = False
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 2
        Me.GridColumn7.Width = 264
        '
        'RepositoryItemComment
        '
        Me.RepositoryItemComment.AutoHeight = False
        Me.RepositoryItemComment.Name = "RepositoryItemComment"
        Me.RepositoryItemComment.ReadOnly = True
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Saldo"
        Me.GridColumn6.DisplayFormat.FormatString = "C0"
        Me.GridColumn6.FieldName = "Balance"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.AllowEdit = False
        Me.GridColumn6.OptionsColumn.AllowFocus = False
        Me.GridColumn6.OptionsColumn.AllowMove = False
        Me.GridColumn6.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Balance", "Total: {0:C0}")})
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 3
        Me.GridColumn6.Width = 320
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDliAdvanceDatasource})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(636, 338)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDliAdvanceDatasource
        '
        Me.INDliAdvanceDatasource.Control = Me.INDgcAdvance
        Me.INDliAdvanceDatasource.CustomizationFormText = "LayoutControlItem1"
        Me.INDliAdvanceDatasource.Location = New System.Drawing.Point(0, 0)
        Me.INDliAdvanceDatasource.Name = "INDliAdvanceDatasource"
        Me.INDliAdvanceDatasource.Size = New System.Drawing.Size(616, 318)
        Me.INDliAdvanceDatasource.TextSize = New System.Drawing.Size(0, 0)
        Me.INDliAdvanceDatasource.TextVisible = False
        '
        'INDlblAdvanceValue
        '
        Me.INDlblAdvanceValue.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 20.0!, System.Drawing.FontStyle.Bold)
        Me.INDlblAdvanceValue.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDlblAdvanceValue.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDlblAdvanceValue.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftTop
        Me.INDlblAdvanceValue.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.Horizontal
        Me.INDlblAdvanceValue.Location = New System.Drawing.Point(4, 23)
        Me.INDlblAdvanceValue.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDlblAdvanceValue.MaximumSize = New System.Drawing.Size(0, 37)
        Me.INDlblAdvanceValue.MinimumSize = New System.Drawing.Size(0, 37)
        Me.INDlblAdvanceValue.Name = "INDlblAdvanceValue"
        Me.INDlblAdvanceValue.Padding = New System.Windows.Forms.Padding(0, 0, 0, 4)
        Me.INDlblAdvanceValue.Size = New System.Drawing.Size(272, 37)
        Me.INDlblAdvanceValue.StyleController = Me.LayoutControl1
        Me.INDlblAdvanceValue.TabIndex = 5
        Me.INDlblAdvanceValue.Text = "$0"
        '
        'INDlblAdvanceTitle
        '
        Me.INDlblAdvanceTitle.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.INDlblAdvanceTitle.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDlblAdvanceTitle.Location = New System.Drawing.Point(6, 0)
        Me.INDlblAdvanceTitle.Margin = New System.Windows.Forms.Padding(1)
        Me.INDlblAdvanceTitle.Name = "INDlblAdvanceTitle"
        Me.INDlblAdvanceTitle.Size = New System.Drawing.Size(109, 23)
        Me.INDlblAdvanceTitle.StyleController = Me.LayoutControl1
        Me.INDlblAdvanceTitle.TabIndex = 4
        Me.INDlblAdvanceTitle.Text = "Anticipos Girados"
        '
        'INDlcgAdvance
        '
        Me.INDlcgAdvance.CustomizationFormText = "LayoutControlGroup1"
        Me.INDlcgAdvance.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlcgAdvance.GroupBordersVisible = False
        Me.INDlcgAdvance.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDliAdvanceTitle, Me.INDliAdvance})
        Me.INDlcgAdvance.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgAdvance.Name = "INDlcgAdvance"
        Me.INDlcgAdvance.Padding = New DevExpress.XtraLayout.Utils.Padding(4, 0, 0, 0)
        Me.INDlcgAdvance.ShowInCustomizationForm = False
        Me.INDlcgAdvance.Size = New System.Drawing.Size(292, 62)
        Me.INDlcgAdvance.TextVisible = False
        '
        'INDliAdvanceTitle
        '
        Me.INDliAdvanceTitle.Control = Me.INDlblAdvanceTitle
        Me.INDliAdvanceTitle.CustomizationFormText = "LayoutControlItem1"
        Me.INDliAdvanceTitle.Location = New System.Drawing.Point(0, 0)
        Me.INDliAdvanceTitle.MaxSize = New System.Drawing.Size(111, 23)
        Me.INDliAdvanceTitle.MinSize = New System.Drawing.Size(111, 23)
        Me.INDliAdvanceTitle.Name = "INDliAdvanceTitle"
        Me.INDliAdvanceTitle.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 0, 0, 0)
        Me.INDliAdvanceTitle.Size = New System.Drawing.Size(288, 23)
        Me.INDliAdvanceTitle.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliAdvanceTitle.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliAdvanceTitle.TextSize = New System.Drawing.Size(0, 0)
        Me.INDliAdvanceTitle.TextToControlDistance = 0
        Me.INDliAdvanceTitle.TextVisible = False
        '
        'INDliAdvance
        '
        Me.INDliAdvance.Control = Me.INDlblAdvanceValue
        Me.INDliAdvance.CustomizationFormText = "LayoutControlItem2"
        Me.INDliAdvance.Location = New System.Drawing.Point(0, 23)
        Me.INDliAdvance.MaxSize = New System.Drawing.Size(272, 39)
        Me.INDliAdvance.MinSize = New System.Drawing.Size(272, 39)
        Me.INDliAdvance.Name = "INDliAdvance"
        Me.INDliAdvance.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.INDliAdvance.Size = New System.Drawing.Size(288, 39)
        Me.INDliAdvance.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliAdvance.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliAdvance.TextSize = New System.Drawing.Size(0, 0)
        Me.INDliAdvance.TextToControlDistance = 0
        Me.INDliAdvance.TextVisible = False
        '
        'INDpceAdvanceDetail
        '
        Me.INDpceAdvanceDetail.Location = New System.Drawing.Point(2, 64)
        Me.INDpceAdvanceDetail.Name = "INDpceAdvanceDetail"
        Me.INDpceAdvanceDetail.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.NoBorder
        Me.INDpceAdvanceDetail.Properties.PopupControl = Me.PopupContainerControl1
        Me.INDpceAdvanceDetail.Properties.PopupSizeable = False
        Me.INDpceAdvanceDetail.Properties.ShowPopupCloseButton = False
        Me.INDpceAdvanceDetail.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        Me.INDpceAdvanceDetail.Size = New System.Drawing.Size(163, 10)
        Me.INDpceAdvanceDetail.TabIndex = 1
        Me.INDpceAdvanceDetail.Visible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
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
        'CtrAdvanceTreasury
        '
        Me.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LayoutControl1)
        Me.Controls.Add(Me.INDpceAdvanceDetail)
        Me.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Margin = New System.Windows.Forms.Padding(0)
        Me.MaximumSize = New System.Drawing.Size(292, 62)
        Me.MinimumSize = New System.Drawing.Size(292, 62)
        Me.Name = "CtrAdvanceTreasury"
        Me.Size = New System.Drawing.Size(292, 62)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.PopupContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PopupContainerControl1.ResumeLayout(False)
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl2.ResumeLayout(False)
        CType(Me.INDgcAdvance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvAdvance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemComment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliAdvanceDatasource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgAdvance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliAdvanceTitle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliAdvance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpceAdvanceDetail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlcgAdvance As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlblAdvanceTitle As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDliAdvanceTitle As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlblAdvanceValue As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDliAdvance As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDpceAdvanceDetail As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents PopupContainerControl1 As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents LayoutControl2 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDgcAdvance As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvAdvance As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDliAdvanceDatasource As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemComment As DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit

End Class
