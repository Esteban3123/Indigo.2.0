Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmInvoiceReversalCausation
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
        Me.INDpcButtons = New DevExpress.XtraEditors.PanelControl()
        Me.INDlyButtons = New DevExpress.XtraLayout.LayoutControl()
        Me.INDbtnNo = New DevExpress.XtraEditors.SimpleButton()
        Me.INDbtnYes = New DevExpress.XtraEditors.SimpleButton()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemYes = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemNo = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyCausations = New DevExpress.XtraLayout.LayoutControl()
        Me.INDgcCausations = New DevExpress.XtraGrid.GridControl()
        Me.viewCausations = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemCausations = New DevExpress.XtraLayout.LayoutControlItem()
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
        CType(Me.INDpcButtons, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpcButtons.SuspendLayout()
        CType(Me.INDlyButtons, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyButtons.SuspendLayout()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemYes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyCausations, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyCausations.SuspendLayout()
        CType(Me.INDgcCausations, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.viewCausations, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCausations, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.INDPanelControlBase.Controls.Add(Me.INDlyCausations)
        Me.INDPanelControlBase.Controls.Add(Me.INDpcButtons)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 117)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(749, 314)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(749, 94)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(749, 94)
        '
        'INDpcButtons
        '
        Me.INDpcButtons.Controls.Add(Me.INDlyButtons)
        Me.INDpcButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.INDpcButtons.Location = New System.Drawing.Point(2, 252)
        Me.INDpcButtons.Name = "INDpcButtons"
        Me.INDpcButtons.Size = New System.Drawing.Size(745, 60)
        Me.INDpcButtons.TabIndex = 0
        '
        'INDlyButtons
        '
        Me.INDlyButtons.Controls.Add(Me.INDbtnNo)
        Me.INDlyButtons.Controls.Add(Me.INDbtnYes)
        Me.INDlyButtons.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyButtons.Location = New System.Drawing.Point(2, 2)
        Me.INDlyButtons.Name = "INDlyButtons"
        Me.INDlyButtons.Root = Me.LayoutControlGroup1
        Me.INDlyButtons.Size = New System.Drawing.Size(741, 56)
        Me.INDlyButtons.TabIndex = 0
        Me.INDlyButtons.Text = "LayoutControl1"
        '
        'INDbtnNo
        '
        Me.INDbtnNo.Location = New System.Drawing.Point(372, 12)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnNo, False)
        Me.INDbtnNo.Name = "INDbtnNo"
        Me.INDbtnNo.Size = New System.Drawing.Size(357, 32)
        Me.INDbtnNo.StyleController = Me.INDlyButtons
        Me.INDbtnNo.TabIndex = 5
        Me.INDbtnNo.Text = "No"
        '
        'INDbtnYes
        '
        Me.INDbtnYes.Location = New System.Drawing.Point(12, 12)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnYes, False)
        Me.INDbtnYes.Name = "INDbtnYes"
        Me.INDbtnYes.Size = New System.Drawing.Size(356, 32)
        Me.INDbtnYes.StyleController = Me.INDlyButtons
        Me.INDbtnYes.TabIndex = 4
        Me.INDbtnYes.Text = "Si"
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemYes, Me.INDlyItemNo})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(741, 56)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlyItemYes
        '
        Me.INDlyItemYes.Control = Me.INDbtnYes
        Me.INDlyItemYes.CustomizationFormText = "INDlyItemYes"
        Me.INDlyItemYes.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemYes.MaxSize = New System.Drawing.Size(0, 36)
        Me.INDlyItemYes.MinSize = New System.Drawing.Size(83, 36)
        Me.INDlyItemYes.Name = "INDlyItemYes"
        Me.INDlyItemYes.Size = New System.Drawing.Size(360, 36)
        Me.INDlyItemYes.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemYes.Text = "INDlyItemYes"
        Me.INDlyItemYes.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemYes.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemYes.TextToControlDistance = 0
        Me.INDlyItemYes.TextVisible = False
        '
        'INDlyItemNo
        '
        Me.INDlyItemNo.Control = Me.INDbtnNo
        Me.INDlyItemNo.CustomizationFormText = "INDlyItemNo"
        Me.INDlyItemNo.Location = New System.Drawing.Point(360, 0)
        Me.INDlyItemNo.MaxSize = New System.Drawing.Size(0, 36)
        Me.INDlyItemNo.MinSize = New System.Drawing.Size(83, 36)
        Me.INDlyItemNo.Name = "INDlyItemNo"
        Me.INDlyItemNo.Size = New System.Drawing.Size(361, 36)
        Me.INDlyItemNo.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemNo.Text = "INDlyItemNo"
        Me.INDlyItemNo.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemNo.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemNo.TextToControlDistance = 0
        Me.INDlyItemNo.TextVisible = False
        '
        'INDlyCausations
        '
        Me.INDlyCausations.Controls.Add(Me.INDgcCausations)
        Me.INDlyCausations.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyCausations.Location = New System.Drawing.Point(2, 7)
        Me.INDlyCausations.Name = "INDlyCausations"
        Me.INDlyCausations.Root = Me.LayoutControlGroup2
        Me.INDlyCausations.Size = New System.Drawing.Size(745, 245)
        Me.INDlyCausations.TabIndex = 1
        Me.INDlyCausations.Text = "LayoutControl1"
        '
        'INDgcCausations
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcCausations, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcCausations, Nothing)
        Me.INDgcCausations.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.INDgcCausations, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcCausations, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcCausations, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcCausations, False)
        Me.INDgcCausations.Location = New System.Drawing.Point(12, 55)
        Me.INDgcCausations.MainView = Me.viewCausations
        Me.INDgcCausations.Name = "INDgcCausations"
        Me.INDgcCausations.Size = New System.Drawing.Size(721, 178)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcCausations, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcCausations.TabIndex = 4
        Me.INDgcCausations.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.viewCausations})
        '
        'viewCausations
        '
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.viewCausations.Appearance.FocusedRow.Options.UseBackColor = True
        Me.viewCausations.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.viewCausations.Appearance.GroupRow.Options.UseFont = True
        'Cadena reemplazada... True
        Me.viewCausations.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.viewCausations.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
        Me.viewCausations.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.viewCausations.Appearance.Row.Options.UseFont = True
        Me.viewCausations.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.viewCausations.Appearance.ViewCaption.Options.UseFont = True
        Me.viewCausations.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5})
        Me.viewCausations.GridControl = Me.INDgcCausations
        Me.viewCausations.Name = "viewCausations"
        Me.viewCausations.OptionsView.EnableAppearanceEvenRow = True
        Me.viewCausations.OptionsView.EnableAppearanceOddRow = True
        Me.viewCausations.OptionsView.ShowAutoFilterRow = True
        Me.viewCausations.OptionsView.ShowDetailButtons = False
        Me.viewCausations.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.viewCausations, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Admisión"
        Me.GridColumn1.FieldName = "AdmissionNumber"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 87
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Paciente"
        Me.GridColumn2.FieldName = "PatientCode"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 91
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Tercero"
        Me.GridColumn3.FieldName = "ThirdPartyDescription"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        Me.GridColumn3.Width = 289
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Orden Servicio"
        Me.GridColumn4.FieldName = "ServiceOrderCode"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 3
        Me.GridColumn4.Width = 121
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Valor Causado"
        Me.GridColumn5.DisplayFormat.FormatString = "c0"
        Me.GridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn5.FieldName = "MedicalFeesContractValue"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.AllowEdit = False
        Me.GridColumn5.OptionsColumn.AllowFocus = False
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 4
        Me.GridColumn5.Width = 115
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup2.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup2.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup2.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup2, False)
        Me.LayoutControlGroup2.CustomizationFormText = "LayoutControlGroup2"
        Me.LayoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup2.GroupBordersVisible = False
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemCausations})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(745, 245)
        Me.LayoutControlGroup2.Text = "LayoutControlGroup2"
        Me.LayoutControlGroup2.TextVisible = False
        '
        'INDlyItemCausations
        '
        Me.INDlyItemCausations.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 19.0!)
        Me.INDlyItemCausations.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyItemCausations.Control = Me.INDgcCausations
        Me.INDlyItemCausations.CustomizationFormText = "INDlyItemCausations"
        Me.INDlyItemCausations.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCausations.MaxSize = New System.Drawing.Size(725, 225)
        Me.INDlyItemCausations.MinSize = New System.Drawing.Size(725, 225)
        Me.INDlyItemCausations.Name = "INDlyItemCausations"
        Me.INDlyItemCausations.Size = New System.Drawing.Size(725, 225)
        Me.INDlyItemCausations.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCausations.Text = "Las siguientes causaciones han sido reversadas. Desea Recalcular ?"
        Me.INDlyItemCausations.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCausations.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemCausations.TextSize = New System.Drawing.Size(135, 38)
        Me.INDlyItemCausations.TextToControlDistance = 5
        '
        'IndigoLayoutControl1
        '

        'Me.IndigoLayoutControl1.FormName = "FormBase20150207"

        '
        'FrmInvoiceReversalCausation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(749, 431)
        Me.ControlBox = False
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmInvoiceReversalCausation"
        Me.Opacity = 1.0R
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Causaciones Reversadas"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpcButtons, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpcButtons.ResumeLayout(False)
        CType(Me.INDlyButtons, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyButtons.ResumeLayout(False)
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemYes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyCausations, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyCausations.ResumeLayout(False)
        CType(Me.INDgcCausations, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.viewCausations, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCausations, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        'CType(Me.IndigoLayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDpcButtons As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDlyButtons As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDbtnNo As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDbtnYes As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDlyItemYes As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemNo As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyCausations As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDgcCausations As DevExpress.XtraGrid.GridControl
    Friend WithEvents viewCausations As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItemCausations As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
End Class
