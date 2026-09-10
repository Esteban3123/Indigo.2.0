Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmPopupPackageQuery
	Inherits FormBase

	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()>
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
	<System.Diagnostics.DebuggerStepThrough()>
	Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.INDlyRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDGcPackageDetail = New DevExpress.XtraGrid.GridControl()
        Me.INDGvPackageDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDBtnDetail = New DevExpress.XtraEditors.SimpleButton()
        Me.INDslePackage = New Presentation.Controls.SearchLookUpEditEx()
        Me.INDGvPackage = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDpanelButtons = New DevExpress.XtraEditors.PanelControl()
        Me.INDbtnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyRoot.SuspendLayout()
        CType(Me.INDGcPackageDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvPackageDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDslePackage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvPackage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpanelButtons, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpanelButtons.SuspendLayout()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyRoot)
        Me.INDPanelControlBase.Controls.Add(Me.INDpanelButtons)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(702, 494)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(702, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(702, 98)
        '
        'INDlyRoot
        '
        Me.INDlyRoot.AllowCustomization = False
        Me.INDlyRoot.Controls.Add(Me.INDGcPackageDetail)
        Me.INDlyRoot.Controls.Add(Me.INDBtnDetail)
        Me.INDlyRoot.Controls.Add(Me.INDslePackage)
        Me.INDlyRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDlyRoot, False)
        Me.INDlyRoot.Location = New System.Drawing.Point(202, 7)
        Me.INDlyRoot.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDlyRoot.Name = "INDlyRoot"
        Me.INDlyRoot.Root = Me.LayoutControlGroup1
        Me.INDlyRoot.Size = New System.Drawing.Size(498, 447)
        Me.INDlyRoot.TabIndex = 2
        Me.INDlyRoot.Text = "LayoutControl1"
        '
        'INDGcPackageDetail
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcPackageDetail, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcPackageDetail, Nothing)
        Me.INDGcPackageDetail.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.INDGcPackageDetail, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcPackageDetail, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcPackageDetail, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcPackageDetail, False)
        Me.INDGcPackageDetail.Location = New System.Drawing.Point(24, 95)
        Me.INDGcPackageDetail.MainView = Me.INDGvPackageDetail
        Me.INDGcPackageDetail.Name = "INDGcPackageDetail"
        Me.INDGcPackageDetail.Size = New System.Drawing.Size(824, 311)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcPackageDetail, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDGcPackageDetail.TabIndex = 14
        Me.INDGcPackageDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvPackageDetail})
        '
        'INDGvPackageDetail
        '
        Me.INDGvPackageDetail.Appearance.Empty.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvPackageDetail.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvPackageDetail.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvPackageDetail.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDGvPackageDetail.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvPackageDetail.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvPackageDetail.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGvPackageDetail.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.INDGvPackageDetail.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvPackageDetail.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.INDGvPackageDetail.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvPackageDetail.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvPackageDetail.Appearance.Row.Options.UseFont = True
        Me.INDGvPackageDetail.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvPackageDetail.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvPackageDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn4, Me.GridColumn5, Me.GridColumn6, Me.GridColumn3, Me.GridColumn7})
        Me.INDGvPackageDetail.GridControl = Me.INDGcPackageDetail
        Me.INDGvPackageDetail.Name = "INDGvPackageDetail"
        Me.INDGvPackageDetail.OptionsCustomization.AllowGroup = False
        Me.INDGvPackageDetail.OptionsCustomization.AllowSort = False
        Me.INDGvPackageDetail.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvPackageDetail.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvPackageDetail.OptionsView.ShowAutoFilterRow = True
        Me.INDGvPackageDetail.OptionsView.ShowGroupPanel = False
        Me.INDGvPackageDetail.Tag = 137
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvPackageDetail, False)
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Componente"
        Me.GridColumn4.FieldName = "SourceCodeName"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 0
        Me.GridColumn4.Width = 572
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Tipo Componente"
        Me.GridColumn5.FieldName = "ComponentTypeName"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.AllowEdit = False
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 1
        Me.GridColumn5.Width = 166
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Cantidad"
        Me.GridColumn6.FieldName = "Quantity"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 2
        Me.GridColumn6.Width = 154
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Diluyente"
        Me.GridColumn3.FieldName = "Thinner"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.UnboundType = DevExpress.Data.UnboundColumnType.[Boolean]
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 3
        Me.GridColumn3.Width = 147
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Vehículo"
        Me.GridColumn7.FieldName = "Vehicle"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsColumn.AllowEdit = False
        Me.GridColumn7.UnboundType = DevExpress.Data.UnboundColumnType.[Boolean]
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 4
        Me.GridColumn7.Width = 159
        '
        'INDBtnDetail
        '
        Me.INDBtnDetail.Location = New System.Drawing.Point(752, 59)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDBtnDetail, False)
        Me.INDBtnDetail.Name = "INDBtnDetail"
        Me.INDBtnDetail.Size = New System.Drawing.Size(96, 28)
        Me.INDBtnDetail.StyleController = Me.INDlyRoot
        Me.INDBtnDetail.TabIndex = 8
        Me.INDBtnDetail.Text = "Detalles"
        '
        'INDslePackage
        '
        Me.INDslePackage.AllowQueryOne = True
        Me.INDslePackage.Datasource = Nothing
        Me.INDslePackage.DisplayMember = "{Code} - {Name}"
        Me.INDslePackage.DisplayNullText = ""
        Me.INDslePackage.EditValue = Nothing
        Me.INDslePackage.EnterMoveNextControl = True
        Me.INDslePackage.FuncQueryOnKeyEnterPressed = Nothing
        Me.INDslePackage.IdOpenForm = 0
        Me.INDslePackage.Location = New System.Drawing.Point(164, 59)
        Me.INDslePackage.MaximumSize = New System.Drawing.Size(5000, 28)
        Me.INDslePackage.MinimumSize = New System.Drawing.Size(100, 28)
        Me.INDslePackage.Name = "INDslePackage"
        Me.INDslePackage.PopUpFormSize = New System.Drawing.Size(200, 300)
        Me.INDslePackage.Size = New System.Drawing.Size(584, 28)
        Me.INDslePackage.TabIndex = 7
        Me.INDslePackage.ValueMember = "Code"
        Me.INDslePackage.View = Me.INDGvPackage
        '
        'INDGvPackage
        '
        Me.INDGvPackage.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvPackage.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvPackage.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvPackage.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvPackage.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGvPackage.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvPackage.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvPackage.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvPackage.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvPackage.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvPackage.Appearance.Row.Options.UseFont = True
        Me.INDGvPackage.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDGvPackage.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2})
        Me.INDGvPackage.Name = "INDGvPackage"
        Me.INDGvPackage.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvPackage.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvPackage.OptionsView.ShowAutoFilterRow = True
        Me.INDGvPackage.OptionsView.ShowDetailButtons = False
        Me.INDGvPackage.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvPackage, False)
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
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Nombre Paquete"
        Me.GridColumn2.FieldName = "Name"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
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
        Me.LayoutControlGroup1.CustomizationFormText = "Seleccionar Paquete"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup3})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(872, 430)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlGroup3
        '
        Me.LayoutControlGroup3.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup3.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup3.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup3, False)
        Me.LayoutControlGroup3.CustomizationFormText = "Paquetes Existentes"
        Me.LayoutControlGroup3.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem4, Me.LayoutControlItem5, Me.LayoutControlItem3})
        Me.LayoutControlGroup3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup3.Name = "LayoutControlGroup3"
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(852, 410)
        Me.LayoutControlGroup3.Text = "Paquetes Existentes"
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.INDGcPackageDetail
        Me.LayoutControlItem4.CustomizationFormText = "Usuarios"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 36)
        Me.LayoutControlItem4.MaxSize = New System.Drawing.Size(828, 0)
        Me.LayoutControlItem4.MinSize = New System.Drawing.Size(828, 25)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(828, 315)
        Me.LayoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextVisible = False
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.INDBtnDetail
        Me.LayoutControlItem5.CustomizationFormText = "LayoutControlItem5"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(728, 0)
        Me.LayoutControlItem5.MaxSize = New System.Drawing.Size(100, 32)
        Me.LayoutControlItem5.MinSize = New System.Drawing.Size(100, 32)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.ShowInCustomizationForm = False
        Me.LayoutControlItem5.Size = New System.Drawing.Size(100, 36)
        Me.LayoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.INDslePackage
        Me.LayoutControlItem3.CustomizationFormText = "Paquete"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(728, 36)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(728, 36)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(728, 36)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.Text = "Paquete"
        Me.LayoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(135, 21)
        Me.LayoutControlItem3.TextToControlDistance = 5
        '
        'INDpanelButtons
        '
        Me.INDpanelButtons.Controls.Add(Me.INDbtnAdd)
        Me.INDpanelButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.INDpanelButtons.Location = New System.Drawing.Point(202, 454)
        Me.INDpanelButtons.MaximumSize = New System.Drawing.Size(0, 38)
        Me.INDpanelButtons.MinimumSize = New System.Drawing.Size(0, 38)
        Me.INDpanelButtons.Name = "INDpanelButtons"
        Me.INDpanelButtons.Size = New System.Drawing.Size(498, 38)
        Me.INDpanelButtons.TabIndex = 1
        '
        'INDbtnAdd
        '
        Me.INDbtnAdd.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnAdd.Appearance.Options.UseFont = True
        Me.INDbtnAdd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDbtnAdd.Location = New System.Drawing.Point(2, 2)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnAdd, True)
        Me.INDbtnAdd.Name = "INDbtnAdd"
        Me.INDbtnAdd.Size = New System.Drawing.Size(494, 34)
        Me.INDbtnAdd.TabIndex = 0
        Me.INDbtnAdd.Text = "Seleccionar"
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.INDlyRoot
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 485)
        Me.CtrNavigationControlPanel1.TabIndex = 3
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'FrmPopupPackageQuery
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(702, 616)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmPopupPackageQuery"
        Me.Opacity = 1.0R
        Me.Text = "Paquetes Existentes"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyRoot.ResumeLayout(False)
        CType(Me.INDGcPackageDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvPackageDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDslePackage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvPackage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpanelButtons, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpanelButtons.ResumeLayout(False)
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDpanelButtons As DevExpress.XtraEditors.PanelControl
	Friend WithEvents INDbtnAdd As DevExpress.XtraEditors.SimpleButton
	Friend WithEvents IndigoTextEdit1 As Controls.IndigoTextEdit
	Friend WithEvents IndigoSimpleButton1 As Controls.IndigoSimpleButton
	Friend WithEvents IndigoSearchLookUpControl1 As Controls.IndigoSearchLookUpControl
	Friend WithEvents IndigoLayoutControlGroup1 As Controls.IndigoLayoutControlGroup
	Friend WithEvents IndigoLabelControl1 As Controls.IndigoLabelControl
	Friend WithEvents IndigoGroupControl1 As Controls.IndigoGroupControl
	Friend WithEvents IndigoGridView1 As Controls.IndigoGridView
	Friend WithEvents IndigoGridControl1 As Controls.IndigoGridControl
	Friend WithEvents INDlyRoot As DevExpress.XtraLayout.LayoutControl
	Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
	Friend WithEvents CtrNavigationControlPanel1 As CtrNavigationControlPanel
	Friend WithEvents INDGcPackageDetail As DevExpress.XtraGrid.GridControl
	Friend WithEvents INDGvPackageDetail As DevExpress.XtraGrid.Views.Grid.GridView
	Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents INDBtnDetail As DevExpress.XtraEditors.SimpleButton
	Friend WithEvents INDslePackage As SearchLookUpEditEx
	Friend WithEvents INDGvPackage As DevExpress.XtraGrid.Views.Grid.GridView
	Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents LayoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup
	Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
	Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
	Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
	Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
End Class
