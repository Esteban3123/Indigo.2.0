<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrBatchSerial
    Inherits DevExpress.XtraEditors.XtraUserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDGcBatchSerial = New DevExpress.XtraGrid.GridControl()
        Me.INDGvBatchSerial = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGclExpiredDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRptSeQuantity = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.INDBtnAddBatch = New DevExpress.XtraEditors.SimpleButton()
        Me.INDDteExpiredDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDTxtCode = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciExperidDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciAddBatchSerial = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.INDColOutstandingQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDGcBatchSerial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvBatchSerial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRptSeQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDteExpiredDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDteExpiredDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciExperidDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciAddBatchSerial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDGcBatchSerial)
        Me.LayoutControl1.Controls.Add(Me.INDBtnAddBatch)
        Me.LayoutControl1.Controls.Add(Me.INDDteExpiredDate)
        Me.LayoutControl1.Controls.Add(Me.INDTxtCode)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(356, 240, 588, 552)
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(560, 468)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDGcBatchSerial
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcBatchSerial, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcBatchSerial, Nothing)
        Me.INDGcBatchSerial.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.INDGcBatchSerial, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcBatchSerial, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcBatchSerial, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcBatchSerial, False)
        Me.INDGcBatchSerial.Location = New System.Drawing.Point(12, 120)
        Me.INDGcBatchSerial.MainView = Me.INDGvBatchSerial
        Me.INDGcBatchSerial.Name = "INDGcBatchSerial"
        Me.INDGcBatchSerial.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDRptSeQuantity})
        Me.INDGcBatchSerial.Size = New System.Drawing.Size(536, 336)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcBatchSerial, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcBatchSerial.TabIndex = 8
        Me.INDGcBatchSerial.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvBatchSerial})
        '
        'INDGvBatchSerial
        '
        Me.INDGvBatchSerial.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvBatchSerial.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvBatchSerial.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDGvBatchSerial.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvBatchSerial.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvBatchSerial.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGvBatchSerial.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvBatchSerial.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvBatchSerial.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvBatchSerial.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvBatchSerial.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvBatchSerial.Appearance.Row.Options.UseFont = True
        Me.INDGvBatchSerial.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvBatchSerial.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvBatchSerial.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.INDGclExpiredDate, Me.INDColOutstandingQuantity, Me.GridColumn3})
        Me.INDGvBatchSerial.GridControl = Me.INDGcBatchSerial
        Me.INDGvBatchSerial.Name = "INDGvBatchSerial"
        Me.INDGvBatchSerial.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvBatchSerial.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvBatchSerial.OptionsView.ShowAutoFilterRow = True
        Me.INDGvBatchSerial.OptionsView.ShowDetailButtons = False
        Me.INDGvBatchSerial.OptionsView.ShowFooter = True
        Me.INDGvBatchSerial.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvBatchSerial, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Código"
        Me.GridColumn1.FieldName = "BatchCode"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 150
        '
        'INDGclExpiredDate
        '
        Me.INDGclExpiredDate.Caption = "Fecha Vencimiento"
        Me.INDGclExpiredDate.FieldName = "ExpirationDate"
        Me.INDGclExpiredDate.Name = "INDGclExpiredDate"
        Me.INDGclExpiredDate.OptionsColumn.AllowEdit = False
        Me.INDGclExpiredDate.OptionsColumn.AllowFocus = False
        Me.INDGclExpiredDate.Visible = True
        Me.INDGclExpiredDate.VisibleIndex = 1
        Me.INDGclExpiredDate.Width = 149
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Cantidad"
        Me.GridColumn3.ColumnEdit = Me.INDRptSeQuantity
        Me.GridColumn3.FieldName = "Quantity"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Quantity", "Total: {0}")})
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 3
        Me.GridColumn3.Width = 110
        '
        'INDRptSeQuantity
        '
        Me.INDRptSeQuantity.AutoHeight = False
        Me.INDRptSeQuantity.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDRptSeQuantity.Mask.EditMask = "[0-9]+"
        Me.INDRptSeQuantity.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDRptSeQuantity.Mask.UseMaskAsDisplayFormat = True
        Me.INDRptSeQuantity.MaxLength = 8
        Me.INDRptSeQuantity.MaxValue = New Decimal(New Integer() {99999999, 0, 0, 0})
        Me.INDRptSeQuantity.Name = "INDRptSeQuantity"
        '
        'INDBtnAddBatch
        '
        Me.INDBtnAddBatch.Location = New System.Drawing.Point(12, 84)
        Me.INDBtnAddBatch.Name = "INDBtnAddBatch"
        Me.INDBtnAddBatch.Size = New System.Drawing.Size(536, 32)
        Me.INDBtnAddBatch.StyleController = Me.LayoutControl1
        Me.INDBtnAddBatch.TabIndex = 7
        Me.INDBtnAddBatch.Text = "Agregar Lote"
        '
        'INDDteExpiredDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDDteExpiredDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDDteExpiredDate, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDDteExpiredDate, False)
        Me.INDDteExpiredDate.EditValue = Nothing
        Me.INDDteExpiredDate.EnterMoveNextControl = True
        Me.INDDteExpiredDate.Location = New System.Drawing.Point(152, 48)
        Me.IndigoTextEdit1.SetMascara(Me.INDDteExpiredDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDDteExpiredDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDDteExpiredDate.Name = "INDDteExpiredDate"
        Me.INDDteExpiredDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDDteExpiredDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDDteExpiredDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDteExpiredDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDDteExpiredDate.Properties.Appearance.Options.UseFont = True
        Me.INDDteExpiredDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDDteExpiredDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDDteExpiredDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDteExpiredDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDDteExpiredDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDDteExpiredDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDteExpiredDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDteExpiredDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDteExpiredDate.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDDteExpiredDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDteExpiredDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDteExpiredDate.Size = New System.Drawing.Size(396, 28)
        Me.INDDteExpiredDate.StyleController = Me.LayoutControl1
        Me.INDDteExpiredDate.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDDteExpiredDate, 0)
        '
        'INDTxtCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtCode, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtCode, False)
        Me.INDTxtCode.EnterMoveNextControl = True
        Me.INDTxtCode.Location = New System.Drawing.Point(152, 12)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtCode.Name = "INDTxtCode"
        Me.INDTxtCode.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtCode.Properties.Appearance.Options.UseFont = True
        Me.INDTxtCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtCode.Properties.MaxLength = 50
        Me.INDTxtCode.Size = New System.Drawing.Size(396, 28)
        Me.INDTxtCode.StyleController = Me.LayoutControl1
        Me.INDTxtCode.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtCode, 0)
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, False)
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciCode, Me.INDLciExperidDate, Me.INDLciAddBatchSerial, Me.LayoutControlItem5})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.ShowInCustomizationForm = False
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(560, 468)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDLciCode
        '
        Me.INDLciCode.Control = Me.INDTxtCode
        Me.INDLciCode.CustomizationFormText = "LayoutControlItem2"
        Me.INDLciCode.Location = New System.Drawing.Point(0, 0)
        Me.INDLciCode.MaxSize = New System.Drawing.Size(540, 36)
        Me.INDLciCode.MinSize = New System.Drawing.Size(540, 36)
        Me.INDLciCode.Name = "INDLciCode"
        Me.INDLciCode.Size = New System.Drawing.Size(540, 36)
        Me.INDLciCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciCode.Text = "Lote/Serial"
        Me.INDLciCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciCode.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciCode.TextToControlDistance = 5
        '
        'INDLciExperidDate
        '
        Me.INDLciExperidDate.Control = Me.INDDteExpiredDate
        Me.INDLciExperidDate.CustomizationFormText = "LayoutControlItem3"
        Me.INDLciExperidDate.Location = New System.Drawing.Point(0, 36)
        Me.INDLciExperidDate.MaxSize = New System.Drawing.Size(540, 36)
        Me.INDLciExperidDate.MinSize = New System.Drawing.Size(540, 36)
        Me.INDLciExperidDate.Name = "INDLciExperidDate"
        Me.INDLciExperidDate.Size = New System.Drawing.Size(540, 36)
        Me.INDLciExperidDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciExperidDate.Text = "Fecha Vencimiento"
        Me.INDLciExperidDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciExperidDate.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciExperidDate.TextToControlDistance = 5
        '
        'INDLciAddBatchSerial
        '
        Me.INDLciAddBatchSerial.Control = Me.INDBtnAddBatch
        Me.INDLciAddBatchSerial.CustomizationFormText = "LayoutControlItem4"
        Me.INDLciAddBatchSerial.Location = New System.Drawing.Point(0, 72)
        Me.INDLciAddBatchSerial.MaxSize = New System.Drawing.Size(0, 36)
        Me.INDLciAddBatchSerial.MinSize = New System.Drawing.Size(83, 36)
        Me.INDLciAddBatchSerial.Name = "INDLciAddBatchSerial"
        Me.INDLciAddBatchSerial.Size = New System.Drawing.Size(540, 36)
        Me.INDLciAddBatchSerial.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciAddBatchSerial.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciAddBatchSerial.TextVisible = False
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.INDGcBatchSerial
        Me.LayoutControlItem5.CustomizationFormText = "LayoutControlItem5"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 108)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(540, 340)
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'INDColOutstandingQuantity
        '
        Me.INDColOutstandingQuantity.Caption = "Existencia"
        Me.INDColOutstandingQuantity.FieldName = "OutstandingQuantity"
        Me.INDColOutstandingQuantity.Name = "INDColOutstandingQuantity"
        Me.INDColOutstandingQuantity.OptionsColumn.AllowEdit = False
        Me.INDColOutstandingQuantity.OptionsColumn.AllowFocus = False
        Me.INDColOutstandingQuantity.Visible = True
        Me.INDColOutstandingQuantity.VisibleIndex = 2
        Me.INDColOutstandingQuantity.Width = 109
        '
        'CtrBatchSerial
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LayoutControl1)
        Me.Name = "CtrBatchSerial"
        Me.Size = New System.Drawing.Size(560, 468)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDGcBatchSerial, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvBatchSerial, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRptSeQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDteExpiredDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDteExpiredDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciExperidDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciAddBatchSerial, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDGcBatchSerial As DevExpress.XtraGrid.GridControl
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents INDGvBatchSerial As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents INDBtnAddBatch As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDDteExpiredDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents INDTxtCode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents INDLciCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciExperidDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciAddBatchSerial As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGclExpiredDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRptSeQuantity As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents INDColOutstandingQuantity As DevExpress.XtraGrid.Columns.GridColumn
End Class
