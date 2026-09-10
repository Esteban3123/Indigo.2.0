<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPopUpMasterAccount
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
        Me.RepositoryItemPopupContainerEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.LcRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSbExportExcel = New DevExpress.XtraEditors.SimpleButton()
        Me.INDSbGenReport = New DevExpress.XtraEditors.SimpleButton()
        Me.INDSleDateTRM = New DevExpress.XtraEditors.DateEdit()
        Me.INDSleCurrency = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LcgRoot = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciDateTRM = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciGenReport = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciExportExcel = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciCurrency = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LcRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LcRoot.SuspendLayout()
        CType(Me.INDSleDateTRM.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleDateTRM.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleCurrency.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LcgRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDateTRM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciGenReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciExportExcel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciCurrency, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RepositoryItemPopupContainerEdit2
        '
        Me.RepositoryItemPopupContainerEdit2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit2.Name = "RepositoryItemPopupContainerEdit2"
        '
        'LcRoot
        '
        Me.LcRoot.Controls.Add(Me.INDSbExportExcel)
        Me.LcRoot.Controls.Add(Me.INDSbGenReport)
        Me.LcRoot.Controls.Add(Me.INDSleDateTRM)
        Me.LcRoot.Controls.Add(Me.INDSleCurrency)
        Me.LcRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LcRoot.Location = New System.Drawing.Point(0, 0)
        Me.LcRoot.Name = "LcRoot"
        Me.LcRoot.Root = Me.LcgRoot
        Me.LcRoot.Size = New System.Drawing.Size(480, 206)
        Me.LcRoot.TabIndex = 0
        Me.LcRoot.Text = "LayoutControl1"
        '
        'INDSbExportExcel
        '
        Me.INDSbExportExcel.ImageOptions.Image = Global.Presentation.Billing.My.Resources.Resources.ICONO_EXCEL_02_
        Me.INDSbExportExcel.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.INDSbExportExcel.Location = New System.Drawing.Point(403, 155)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDSbExportExcel, False)
        Me.INDSbExportExcel.Name = "INDSbExportExcel"
        Me.INDSbExportExcel.Size = New System.Drawing.Size(66, 41)
        Me.INDSbExportExcel.StyleController = Me.LcRoot
        Me.INDSbExportExcel.TabIndex = 7
        '
        'INDSbGenReport
        '
        Me.INDSbGenReport.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSbGenReport.Appearance.Options.UseFont = True
        Me.INDSbGenReport.Location = New System.Drawing.Point(11, 155)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDSbGenReport, True)
        Me.INDSbGenReport.Name = "INDSbGenReport"
        Me.INDSbGenReport.Size = New System.Drawing.Size(388, 41)
        Me.INDSbGenReport.StyleController = Me.LcRoot
        Me.INDSbGenReport.TabIndex = 6
        Me.INDSbGenReport.Text = "Reporte"
        '
        'INDSleDateTRM
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleDateTRM, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleDateTRM, False)
        Me.INDSleDateTRM.EditValue = Nothing
        Me.INDSleDateTRM.Location = New System.Drawing.Point(11, 111)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleDateTRM, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleDateTRM.Name = "INDSleDateTRM"
        Me.INDSleDateTRM.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleDateTRM.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleDateTRM.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleDateTRM.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleDateTRM.Properties.Appearance.Options.UseFont = True
        Me.INDSleDateTRM.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleDateTRM.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleDateTRM.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSleDateTRM.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleDateTRM.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleDateTRM.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSleDateTRM.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSleDateTRM.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleDateTRM.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDSleDateTRM.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleDateTRM.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleDateTRM.Properties.EditFormat.FormatString = ""
        Me.INDSleDateTRM.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDSleDateTRM.Size = New System.Drawing.Size(458, 28)
        Me.INDSleDateTRM.StyleController = Me.LcRoot
        Me.INDSleDateTRM.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleDateTRM, 0)
        '
        'INDSleCurrency
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleCurrency, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleCurrency, False)
        Me.INDSleCurrency.Location = New System.Drawing.Point(11, 36)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleCurrency, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleCurrency.Name = "INDSleCurrency"
        Me.INDSleCurrency.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleCurrency.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleCurrency.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleCurrency.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleCurrency.Properties.Appearance.Options.UseFont = True
        Me.INDSleCurrency.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleCurrency.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleCurrency.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSleCurrency.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleCurrency.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleCurrency.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSleCurrency.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSleCurrency.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleCurrency.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDSleCurrency.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleCurrency.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDSleCurrency.Properties.DisplayMember = "Abbreviation"
        Me.INDSleCurrency.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDSleCurrency.Properties.NullText = ""
        Me.INDSleCurrency.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDSleCurrency.Properties.ValueMember = "Id"
        Me.INDSleCurrency.Size = New System.Drawing.Size(458, 28)
        Me.INDSleCurrency.StyleController = Me.LcRoot
        Me.INDSleCurrency.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleCurrency, 0)
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn2, Me.GridColumn1})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Nombre"
        Me.GridColumn2.FieldName = "CurrencyName"
        Me.GridColumn2.MinWidth = 17
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        '
        'LcgRoot
        '
        Me.LcgRoot.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LcgRoot.AppearanceGroup.Options.UseFont = True
        Me.LcgRoot.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LcgRoot.AppearanceItemCaption.Options.UseFont = True
        Me.LcgRoot.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LcgRoot.AppearanceTabPage.Header.Options.UseFont = True
        Me.LcgRoot.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LcgRoot.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LcgRoot.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LcgRoot.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LcgRoot.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LcgRoot.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LcgRoot.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LcgRoot.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LcgRoot, False)
        Me.LcgRoot.CustomizationFormText = "LcgRoot"
        Me.LcgRoot.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LcgRoot.GroupBordersVisible = False
        Me.LcgRoot.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciDateTRM, Me.INDLciGenReport, Me.INDLciExportExcel, Me.INDLciCurrency})
        Me.LcgRoot.Name = "Root"
        Me.LcgRoot.Size = New System.Drawing.Size(480, 206)
        Me.LcgRoot.TextVisible = False
        '
        'INDLciDateTRM
        '
        Me.INDLciDateTRM.Control = Me.INDSleDateTRM
        Me.INDLciDateTRM.CustomizationFormText = "Fecha TRM"
        Me.INDLciDateTRM.Location = New System.Drawing.Point(0, 75)
        Me.INDLciDateTRM.MaxSize = New System.Drawing.Size(0, 70)
        Me.INDLciDateTRM.MinSize = New System.Drawing.Size(40, 70)
        Me.INDLciDateTRM.Name = "INDLciDateTRM"
        Me.INDLciDateTRM.Size = New System.Drawing.Size(462, 70)
        Me.INDLciDateTRM.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDateTRM.Text = "Fecha TRM"
        Me.INDLciDateTRM.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciDateTRM.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDateTRM.TextSize = New System.Drawing.Size(96, 21)
        Me.INDLciDateTRM.TextToControlDistance = 5
        '
        'INDLciGenReport
        '
        Me.INDLciGenReport.Control = Me.INDSbGenReport
        Me.INDLciGenReport.CustomizationFormText = "Reporte"
        Me.INDLciGenReport.Location = New System.Drawing.Point(0, 145)
        Me.INDLciGenReport.MaxSize = New System.Drawing.Size(0, 45)
        Me.INDLciGenReport.MinSize = New System.Drawing.Size(390, 45)
        Me.INDLciGenReport.Name = "INDLciGenReport"
        Me.INDLciGenReport.Size = New System.Drawing.Size(392, 45)
        Me.INDLciGenReport.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciGenReport.Text = "Reporte"
        Me.INDLciGenReport.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciGenReport.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciGenReport.TextToControlDistance = 0
        Me.INDLciGenReport.TextVisible = False
        '
        'INDLciExportExcel
        '
        Me.INDLciExportExcel.Control = Me.INDSbExportExcel
        Me.INDLciExportExcel.CustomizationFormText = "Exportar a Excel"
        Me.INDLciExportExcel.Location = New System.Drawing.Point(392, 145)
        Me.INDLciExportExcel.MaxSize = New System.Drawing.Size(0, 45)
        Me.INDLciExportExcel.MinSize = New System.Drawing.Size(43, 45)
        Me.INDLciExportExcel.Name = "INDLciExportExcel"
        Me.INDLciExportExcel.Size = New System.Drawing.Size(70, 45)
        Me.INDLciExportExcel.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciExportExcel.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciExportExcel.TextVisible = False
        '
        'INDLciCurrency
        '
        Me.INDLciCurrency.Control = Me.INDSleCurrency
        Me.INDLciCurrency.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciCurrency.CustomizationFormText = "Moneda"
        Me.INDLciCurrency.Location = New System.Drawing.Point(0, 0)
        Me.INDLciCurrency.MinSize = New System.Drawing.Size(50, 25)
        Me.INDLciCurrency.Name = "INDLciCurrency"
        Me.INDLciCurrency.Size = New System.Drawing.Size(462, 75)
        Me.INDLciCurrency.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciCurrency.Text = "Moneda"
        Me.INDLciCurrency.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciCurrency.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciCurrency.TextSize = New System.Drawing.Size(96, 21)
        Me.INDLciCurrency.TextToControlDistance = 5
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit2
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Abreviación"
        Me.GridColumn1.FieldName = "Abbreviation"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 1
        Me.GridColumn1.Width = 64
        '
        'FrmPopUpMasterAccount
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(480, 206)
        Me.Controls.Add(Me.LcRoot)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.IconOptions.ShowIcon = False
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmPopUpMasterAccount"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Reporte cuenta madre"
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LcRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LcRoot.ResumeLayout(False)
        CType(Me.INDSleDateTRM.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleDateTRM.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleCurrency.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LcgRoot, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDateTRM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciGenReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciExportExcel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciCurrency, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LcRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LcgRoot As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciDateTRM As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents INDSbGenReport As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDLciGenReport As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents RepositoryItemPopupContainerEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDSleDateTRM As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDLciCurrency As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSbExportExcel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDLciExportExcel As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSleCurrency As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
End Class
