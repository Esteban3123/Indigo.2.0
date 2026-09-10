<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSelectPeriod
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
        Me.LcRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDgcImportData = New DevExpress.XtraGrid.GridControl()
        Me.INDgvImportData = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolEmployee = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolFixedAsset = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolProductionCenter = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolCheck = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrpchkImportData = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.ColEmployeeGroup = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDpcDateControl = New DevExpress.XtraEditors.PanelControl()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.INDsbAcept = New DevExpress.XtraEditors.SimpleButton()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl()
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl()
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LciMarquee = New DevExpress.XtraLayout.LayoutControlItem()
        Me.MarqueeProgressBarControl1 = New DevExpress.XtraEditors.MarqueeProgressBarControl()
        Me.LayoutControl2 = New DevExpress.XtraLayout.LayoutControl()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit()
        CType(Me.LcRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LcRoot.SuspendLayout()
        CType(Me.INDgcImportData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvImportData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrpchkImportData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpcDateControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LciMarquee, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MarqueeProgressBarControl1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl2.SuspendLayout()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LcRoot
        '
        Me.LcRoot.Controls.Add(Me.INDgcImportData)
        Me.LcRoot.Controls.Add(Me.INDpcDateControl)
        Me.LcRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LcRoot.Location = New System.Drawing.Point(0, 0)
        Me.LcRoot.Name = "LcRoot"
        Me.LcRoot.Root = Me.LayoutControlGroup1
        Me.LcRoot.Size = New System.Drawing.Size(835, 427)
        Me.LcRoot.TabIndex = 0
        Me.LcRoot.Text = "LayoutControl1"
        '
        'INDgcImportData
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcImportData, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcImportData, Nothing)
        Me.INDgcImportData.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.INDgcImportData, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcImportData, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcImportData, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcImportData, False)
        Me.INDgcImportData.Location = New System.Drawing.Point(12, 57)
        Me.INDgcImportData.MainView = Me.INDgvImportData
        Me.INDgcImportData.Name = "INDgcImportData"
        Me.INDgcImportData.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrpchkImportData})
        Me.INDgcImportData.Size = New System.Drawing.Size(811, 358)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcImportData, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.IndigoGridControl1.SetSizeLayoutItem(Me.INDgcImportData, New System.Drawing.Size(815, 0))
        Me.INDgcImportData.TabIndex = 4
        Me.INDgcImportData.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvImportData})
        '
        'INDgvImportData
        '
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        'Cadena reemplazada... System.Drawing.Color.White
        Me.INDgvImportData.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDgvImportData.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDgvImportData.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDgvImportData.Appearance.GroupRow.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDgvImportData.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDgvImportData.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDgvImportData.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvImportData.Appearance.Row.Options.UseFont = True
        Me.INDgvImportData.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgvImportData.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvImportData.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.INDcolEmployee, Me.INDcolFixedAsset, Me.INDcolProductionCenter, Me.INDcolCheck, Me.ColEmployeeGroup})
        Me.INDgvImportData.GridControl = Me.INDgcImportData
        Me.INDgvImportData.Name = "INDgvImportData"
        Me.INDgvImportData.OptionsFind.AlwaysVisible = True
        Me.INDgvImportData.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvImportData.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvImportData.OptionsView.ShowAutoFilterRow = True
        Me.INDgvImportData.OptionsView.ShowDetailButtons = False
        Me.INDgvImportData.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvImportData, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Código"
        Me.GridColumn1.FieldName = "Code"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 53
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Nombre"
        Me.GridColumn2.FieldName = "Description"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 63
        '
        'INDcolEmployee
        '
        Me.INDcolEmployee.Caption = "Empleado"
        Me.INDcolEmployee.FieldName = "FullNameEmployee"
        Me.INDcolEmployee.Name = "INDcolEmployee"
        Me.INDcolEmployee.OptionsColumn.AllowEdit = False
        Me.INDcolEmployee.OptionsColumn.AllowFocus = False
        Me.INDcolEmployee.Visible = True
        Me.INDcolEmployee.VisibleIndex = 2
        Me.INDcolEmployee.Width = 138
        '
        'INDcolFixedAsset
        '
        Me.INDcolFixedAsset.Caption = "Activo Fijo"
        Me.INDcolFixedAsset.FieldName = "FullNameFixedAsset"
        Me.INDcolFixedAsset.Name = "INDcolFixedAsset"
        Me.INDcolFixedAsset.OptionsColumn.AllowEdit = False
        Me.INDcolFixedAsset.OptionsColumn.AllowFocus = False
        Me.INDcolFixedAsset.Visible = True
        Me.INDcolFixedAsset.VisibleIndex = 4
        Me.INDcolFixedAsset.Width = 138
        '
        'INDcolProductionCenter
        '
        Me.INDcolProductionCenter.Caption = "Centro de Producción"
        Me.INDcolProductionCenter.FieldName = "FullNameProductionCenter"
        Me.INDcolProductionCenter.Name = "INDcolProductionCenter"
        Me.INDcolProductionCenter.OptionsColumn.AllowEdit = False
        Me.INDcolProductionCenter.OptionsColumn.AllowFocus = False
        Me.INDcolProductionCenter.Visible = True
        Me.INDcolProductionCenter.VisibleIndex = 5
        Me.INDcolProductionCenter.Width = 107
        '
        'INDcolCheck
        '
        Me.INDcolCheck.Caption = "Seleccionar"
        Me.INDcolCheck.ColumnEdit = Me.INDrpchkImportData
        Me.INDcolCheck.FieldName = "Checked"
        Me.INDcolCheck.Name = "INDcolCheck"
        Me.INDcolCheck.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDcolCheck.OptionsFilter.AllowAutoFilter = False
        Me.INDcolCheck.OptionsFilter.AllowFilter = False
        Me.INDcolCheck.OptionsFilter.ImmediateUpdateAutoFilter = False
        Me.INDcolCheck.Visible = True
        Me.INDcolCheck.VisibleIndex = 6
        Me.INDcolCheck.Width = 84
        '
        'INDrpchkImportData
        '
        Me.INDrpchkImportData.AutoHeight = False
        Me.INDrpchkImportData.Name = "INDrpchkImportData"
        '
        'ColEmployeeGroup
        '
        Me.ColEmployeeGroup.Caption = "Grupo"
        Me.ColEmployeeGroup.FieldName = "FullNameEmployeeGroup"
        Me.ColEmployeeGroup.Name = "ColEmployeeGroup"
        Me.ColEmployeeGroup.OptionsColumn.AllowEdit = False
        Me.ColEmployeeGroup.OptionsColumn.AllowFocus = False
        Me.ColEmployeeGroup.Visible = True
        Me.ColEmployeeGroup.VisibleIndex = 3
        Me.ColEmployeeGroup.Width = 113
        '
        'INDpcDateControl
        '
        Me.INDpcDateControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpcDateControl.Location = New System.Drawing.Point(12, 12)
        Me.INDpcDateControl.Margin = New System.Windows.Forms.Padding(0)
        Me.INDpcDateControl.MaximumSize = New System.Drawing.Size(447, 35)
        Me.INDpcDateControl.MinimumSize = New System.Drawing.Size(447, 35)
        Me.INDpcDateControl.Name = "INDpcDateControl"
        Me.INDpcDateControl.Size = New System.Drawing.Size(447, 35)
        Me.INDpcDateControl.TabIndex = 7
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem3})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(835, 427)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDpcDateControl
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(447, 45)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(447, 45)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(815, 45)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextToControlDistance = 0
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.INDgcImportData
        Me.LayoutControlItem3.CustomizationFormText = "LayoutControlItem3"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 45)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(815, 362)
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = False
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.INDsbAcept)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(0, 427)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(835, 39)
        Me.PanelControl1.TabIndex = 1
        '
        'INDsbAcept
        '
        Me.INDsbAcept.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDsbAcept.Appearance.Options.UseFont = True
        Me.INDsbAcept.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDsbAcept.Location = New System.Drawing.Point(2, 2)
        Me.INDsbAcept.Name = "INDsbAcept"
        Me.INDsbAcept.Size = New System.Drawing.Size(831, 35)
        Me.INDsbAcept.TabIndex = 0
        Me.INDsbAcept.Text = "Importar"
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
        Me.LayoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup2.GroupBordersVisible = False
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LciMarquee})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(835, 427)
        Me.LayoutControlGroup2.TextVisible = False
        '
        'LciMarquee
        '
        Me.LciMarquee.ContentVisible = False
        Me.LciMarquee.Control = Me.MarqueeProgressBarControl1
        Me.LciMarquee.Location = New System.Drawing.Point(0, 0)
        Me.LciMarquee.Name = "LciMarquee"
        Me.LciMarquee.Size = New System.Drawing.Size(835, 427)
        Me.LciMarquee.TextSize = New System.Drawing.Size(0, 0)
        Me.LciMarquee.TextVisible = False
        '
        'MarqueeProgressBarControl1
        '
        Me.MarqueeProgressBarControl1.EditValue = 0
        Me.MarqueeProgressBarControl1.Location = New System.Drawing.Point(2, 2)
        Me.MarqueeProgressBarControl1.Name = "MarqueeProgressBarControl1"
        Me.MarqueeProgressBarControl1.Size = New System.Drawing.Size(831, 18)
        Me.MarqueeProgressBarControl1.StyleController = Me.LayoutControl2
        Me.MarqueeProgressBarControl1.TabIndex = 4
        '
        'LayoutControl2
        '
        Me.LayoutControl2.Controls.Add(Me.MarqueeProgressBarControl1)
        Me.LayoutControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl2.Margin = New System.Windows.Forms.Padding(0)
        Me.LayoutControl2.Name = "LayoutControl2"
        Me.LayoutControl2.Root = Me.LayoutControlGroup2
        Me.LayoutControl2.Size = New System.Drawing.Size(835, 427)
        Me.LayoutControl2.TabIndex = 2
        Me.LayoutControl2.Text = "LayoutControl2"
        '
        'FrmSelectPeriod
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(835, 466)
        Me.Controls.Add(Me.LcRoot)
        Me.Controls.Add(Me.LayoutControl2)
        Me.Controls.Add(Me.PanelControl1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmSelectPeriod"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Tag = "1203"
        Me.Text = "Importar Datos"
        CType(Me.LcRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LcRoot.ResumeLayout(False)
        CType(Me.INDgcImportData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvImportData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrpchkImportData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpcDateControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LciMarquee, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MarqueeProgressBarControl1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl2.ResumeLayout(False)
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LcRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDsbAcept As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDgcImportData As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvImportData As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents INDpcDateControl As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolEmployee As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrpchkImportData As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents INDcolCheck As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDcolFixedAsset As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolProductionCenter As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControl2 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents MarqueeProgressBarControl1 As DevExpress.XtraEditors.MarqueeProgressBarControl
    Friend WithEvents LciMarquee As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents ColEmployeeGroup As DevExpress.XtraGrid.Columns.GridColumn
End Class
