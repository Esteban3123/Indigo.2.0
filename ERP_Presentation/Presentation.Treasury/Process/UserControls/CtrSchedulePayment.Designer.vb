Imports DevExpress.XtraEditors

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrSchedulePayment
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
        Me.components = New System.ComponentModel.Container()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDpceAdvanceDetail = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.PopupContainerControl1 = New DevExpress.XtraEditors.PopupContainerControl()
        Me.LayoutControl2 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDgcInvoice = New DevExpress.XtraGrid.GridControl()
        Me.INDgvInvoice = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemTextEditValue = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDliAdvanceDatasource = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDlblAdvanceTitle = New DevExpress.XtraEditors.LabelControl()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDlcgAdvance = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDliAdvanceTitle = New DevExpress.XtraLayout.LayoutControlItem()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpceAdvanceDetail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PopupContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PopupContainerControl1.SuspendLayout()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl2.SuspendLayout()
        CType(Me.INDgcInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEditValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliAdvanceDatasource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDlcgAdvance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliAdvanceTitle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
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
        '
        'PopupContainerControl1
        '
        Me.PopupContainerControl1.Controls.Add(Me.LayoutControl2)
        Me.PopupContainerControl1.Location = New System.Drawing.Point(0, 119)
        Me.PopupContainerControl1.Name = "PopupContainerControl1"
        Me.PopupContainerControl1.Size = New System.Drawing.Size(713, 275)
        Me.PopupContainerControl1.TabIndex = 6
        '
        'LayoutControl2
        '
        Me.LayoutControl2.Controls.Add(Me.INDgcInvoice)
        Me.LayoutControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl2.Name = "LayoutControl2"
        Me.LayoutControl2.Root = Me.LayoutControlGroup1
        Me.LayoutControl2.Size = New System.Drawing.Size(713, 275)
        Me.LayoutControl2.TabIndex = 1
        Me.LayoutControl2.Text = "LayoutControl2"
        '
        'INDgcInvoice
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcInvoice, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcInvoice, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcInvoice, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcInvoice, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcInvoice, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcInvoice, False)
        Me.INDgcInvoice.Location = New System.Drawing.Point(12, 12)
        Me.INDgcInvoice.MainView = Me.INDgvInvoice
        Me.INDgcInvoice.Name = "INDgcInvoice"
        Me.INDgcInvoice.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemTextEditValue})
        Me.INDgcInvoice.Size = New System.Drawing.Size(689, 251)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcInvoice, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcInvoice.TabIndex = 0
        Me.INDgcInvoice.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvInvoice, Me.GridView1})
        '
        'INDgvInvoice
        '
        Me.INDgvInvoice.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvInvoice.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvInvoice.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDgvInvoice.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvInvoice.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvInvoice.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDgvInvoice.Appearance.GroupPanel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDgvInvoice.Appearance.GroupPanel.Options.UseForeColor = True
        Me.INDgvInvoice.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvInvoice.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvInvoice.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvInvoice.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvInvoice.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvInvoice.Appearance.Row.Options.UseFont = True
        Me.INDgvInvoice.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgvInvoice.Appearance.ViewCaption.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDgvInvoice.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvInvoice.Appearance.ViewCaption.Options.UseForeColor = True
        Me.INDgvInvoice.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn2, Me.GridColumn5, Me.GridColumn8, Me.GridColumn6, Me.GridColumn11, Me.GridColumn10, Me.GridColumn9, Me.GridColumn7})
        Me.INDgvInvoice.GridControl = Me.INDgcInvoice
        Me.INDgvInvoice.GroupCount = 1
        Me.INDgvInvoice.Name = "INDgvInvoice"
        Me.INDgvInvoice.OptionsFind.AlwaysVisible = True
        Me.INDgvInvoice.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvInvoice.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvInvoice.OptionsView.RowAutoHeight = True
        Me.INDgvInvoice.OptionsView.ShowAutoFilterRow = True
        Me.INDgvInvoice.OptionsView.ShowDetailButtons = False
        Me.INDgvInvoice.OptionsView.ShowFooter = True
        Me.INDgvInvoice.OptionsView.ShowGroupPanel = False
        Me.INDgvInvoice.OptionsView.ShowViewCaption = True
        Me.INDgvInvoice.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn2, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvInvoice, False)
        Me.INDgvInvoice.ViewCaption = "Detalle Facturas"
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Proveedor"
        Me.GridColumn2.FieldName = "SupplierName"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.OptionsColumn.AllowMove = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        Me.GridColumn2.Width = 244
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Nit"
        Me.GridColumn5.FieldName = "SupplierNit"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.AllowEdit = False
        Me.GridColumn5.OptionsColumn.AllowFocus = False
        Me.GridColumn5.OptionsColumn.AllowMove = False
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 0
        Me.GridColumn5.Width = 106
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "L. Distribución"
        Me.GridColumn8.FieldName = "DescriptionLine"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.OptionsColumn.AllowEdit = False
        Me.GridColumn8.OptionsColumn.AllowFocus = False
        Me.GridColumn8.OptionsColumn.AllowMove = False
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 1
        Me.GridColumn8.Width = 170
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Factura"
        Me.GridColumn6.FieldName = "Invoice"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.AllowEdit = False
        Me.GridColumn6.OptionsColumn.AllowFocus = False
        Me.GridColumn6.OptionsColumn.AllowMove = False
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 2
        Me.GridColumn6.Width = 133
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "Cuota"
        Me.GridColumn10.FieldName = "Share"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.OptionsColumn.AllowEdit = False
        Me.GridColumn10.OptionsColumn.AllowFocus = False
        Me.GridColumn10.OptionsColumn.AllowMove = False
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 3
        Me.GridColumn10.Width = 117
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Saldo"
        Me.GridColumn9.ColumnEdit = Me.RepositoryItemTextEditValue
        Me.GridColumn9.FieldName = "BalanceShare"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.OptionsColumn.AllowEdit = False
        Me.GridColumn9.OptionsColumn.AllowFocus = False
        Me.GridColumn9.OptionsColumn.AllowMove = False
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 5
        Me.GridColumn9.Width = 133
        '
        'RepositoryItemTextEditValue
        '
        Me.RepositoryItemTextEditValue.AutoHeight = False
        Me.RepositoryItemTextEditValue.Mask.EditMask = "n2"
        Me.RepositoryItemTextEditValue.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.RepositoryItemTextEditValue.Mask.UseMaskAsDisplayFormat = True
        Me.RepositoryItemTextEditValue.Name = "RepositoryItemTextEditValue"
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Valor a Pagar"
        Me.GridColumn7.ColumnEdit = Me.RepositoryItemTextEditValue
        Me.GridColumn7.FieldName = "PayValue"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsColumn.AllowEdit = False
        Me.GridColumn7.OptionsColumn.AllowFocus = False
        Me.GridColumn7.OptionsColumn.AllowMove = False
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 6
        Me.GridColumn7.Width = 180
        '
        'GridView1
        '
        Me.GridView1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView1.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView1.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView1.Appearance.FocusedRow.Options.UseForeColor = True
        Me.GridView1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.GroupRow.Options.UseFont = True
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.GridControl = Me.INDgcInvoice
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
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(713, 275)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDliAdvanceDatasource
        '
        Me.INDliAdvanceDatasource.Control = Me.INDgcInvoice
        Me.INDliAdvanceDatasource.CustomizationFormText = "LayoutControlItem1"
        Me.INDliAdvanceDatasource.Location = New System.Drawing.Point(0, 0)
        Me.INDliAdvanceDatasource.Name = "INDliAdvanceDatasource"
        Me.INDliAdvanceDatasource.Size = New System.Drawing.Size(693, 255)
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
        'INDlblAdvanceTitle
        '
        Me.INDlblAdvanceTitle.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlblAdvanceTitle.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDlblAdvanceTitle.Appearance.Options.UseFont = True
        Me.INDlblAdvanceTitle.Appearance.Options.UseForeColor = True
        Me.INDlblAdvanceTitle.Appearance.Options.UseTextOptions = True
        Me.INDlblAdvanceTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDlblAdvanceTitle.Location = New System.Drawing.Point(5, 1)
        Me.INDlblAdvanceTitle.MaximumSize = New System.Drawing.Size(270, 0)
        Me.INDlblAdvanceTitle.MinimumSize = New System.Drawing.Size(270, 0)
        Me.INDlblAdvanceTitle.Name = "INDlblAdvanceTitle"
        Me.INDlblAdvanceTitle.Size = New System.Drawing.Size(270, 13)
        Me.INDlblAdvanceTitle.StyleController = Me.LayoutControl1
        Me.INDlblAdvanceTitle.TabIndex = 4
        Me.INDlblAdvanceTitle.Text = "Valor Programado"
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDlblAdvanceTitle)
        Me.LayoutControl1.Controls.Add(Me.PopupContainerControl1)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.INDlcgAdvance
        Me.LayoutControl1.Size = New System.Drawing.Size(292, 62)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDlcgAdvance
        '
        Me.INDlcgAdvance.CustomizationFormText = "LayoutControlGroup1"
        Me.INDlcgAdvance.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlcgAdvance.GroupBordersVisible = False
        Me.INDlcgAdvance.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDliAdvanceTitle})
        Me.INDlcgAdvance.Name = "INDlcgAdvance"
        Me.INDlcgAdvance.Padding = New DevExpress.XtraLayout.Utils.Padding(4, 0, 0, 0)
        Me.INDlcgAdvance.Size = New System.Drawing.Size(292, 62)
        Me.INDlcgAdvance.TextVisible = False
        '
        'INDliAdvanceTitle
        '
        Me.INDliAdvanceTitle.Control = Me.INDlblAdvanceTitle
        Me.INDliAdvanceTitle.CustomizationFormText = "LayoutControlItem1"
        Me.INDliAdvanceTitle.Location = New System.Drawing.Point(0, 0)
        Me.INDliAdvanceTitle.MaxSize = New System.Drawing.Size(274, 15)
        Me.INDliAdvanceTitle.MinSize = New System.Drawing.Size(274, 15)
        Me.INDliAdvanceTitle.Name = "INDliAdvanceTitle"
        Me.INDliAdvanceTitle.Padding = New DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1)
        Me.INDliAdvanceTitle.Size = New System.Drawing.Size(288, 62)
        Me.INDliAdvanceTitle.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliAdvanceTitle.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliAdvanceTitle.TextSize = New System.Drawing.Size(0, 0)
        Me.INDliAdvanceTitle.TextToControlDistance = 0
        Me.INDliAdvanceTitle.TextVisible = False
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "Moneda"
        Me.GridColumn11.FieldName = "CurrencyAbbreviation"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.Visible = True
        Me.GridColumn11.VisibleIndex = 4
        '
        'CtrSchedulePayment
        '
        Me.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LayoutControl1)
        Me.Controls.Add(Me.INDpceAdvanceDetail)
        Me.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Margin = New System.Windows.Forms.Padding(0)
        Me.MaximumSize = New System.Drawing.Size(292, 0)
        Me.MinimumSize = New System.Drawing.Size(292, 62)
        Me.Name = "CtrSchedulePayment"
        Me.Size = New System.Drawing.Size(292, 62)
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpceAdvanceDetail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PopupContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PopupContainerControl1.ResumeLayout(False)
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl2.ResumeLayout(False)
        CType(Me.INDgcInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEditValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliAdvanceDatasource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDlcgAdvance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliAdvanceTitle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDpceAdvanceDetail As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlblAdvanceTitle As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents PopupContainerControl1 As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents LayoutControl2 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDgcInvoice As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvInvoice As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDliAdvanceDatasource As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlcgAdvance As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDliAdvanceTitle As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemTextEditValue As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents RepositoryItemPopupContainerEdit1 As Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
End Class
