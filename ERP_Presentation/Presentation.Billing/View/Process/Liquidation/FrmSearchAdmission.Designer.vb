<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSearchAdmission
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
        Me.LycSearch = New DevExpress.XtraLayout.LayoutControl()
        Me.BtnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnOpen = New DevExpress.XtraEditors.SimpleButton()
        Me.GdcSearch = New DevExpress.XtraGrid.GridControl()
        Me.GdvSearch = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColAdmissionCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColPatienCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColPatienName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColAdmissionType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.repIceAdmissionType = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.ColLiquidationType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.repIceLiquidationType = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.ColBedStay = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColResponsibleName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColStatusName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LycgSearch = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LycgSearchGroup = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        CType(Me.LycSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LycSearch.SuspendLayout()
        CType(Me.GdcSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GdvSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.repIceAdmissionType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.repIceLiquidationType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LycgSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LycgSearchGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LycSearch
        '
        Me.LycSearch.AllowCustomization = False
        Me.LycSearch.Controls.Add(Me.BtnCancel)
        Me.LycSearch.Controls.Add(Me.BtnOpen)
        Me.LycSearch.Controls.Add(Me.GdcSearch)
        Me.LycSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LycSearch.Location = New System.Drawing.Point(0, 0)
        Me.LycSearch.Name = "LycSearch"
        Me.LycSearch.Root = Me.LycgSearch
        Me.LycSearch.Size = New System.Drawing.Size(1008, 729)
        Me.LycSearch.TabIndex = 0
        Me.LycSearch.Text = "LayoutControl1"
        '
        'BtnCancel
        '
        Me.BtnCancel.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.BtnCancel.Appearance.Options.UseFont = True
        Me.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnCancel.Location = New System.Drawing.Point(152, 686)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(110, 30)
        Me.BtnCancel.StyleController = Me.LycSearch
        Me.BtnCancel.TabIndex = 5
        Me.BtnCancel.Text = "Cancelar"
        '
        'BtnOpen
        '
        Me.BtnOpen.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.BtnOpen.Appearance.Options.UseFont = True
        Me.BtnOpen.Location = New System.Drawing.Point(22, 686)
        Me.BtnOpen.Name = "BtnOpen"
        Me.BtnOpen.Size = New System.Drawing.Size(110, 30)
        Me.BtnOpen.StyleController = Me.LycSearch
        Me.BtnOpen.TabIndex = 4
        Me.BtnOpen.Text = "Abrir"
        '
        'GdcSearch
        '
        Me.IndigoGridControl1.SetAddActions(Me.GdcSearch, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.GdcSearch, Nothing)
        Me.GdcSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.GdcSearch, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.GdcSearch, True)
        Me.IndigoGridControl1.SetHoldSize(Me.GdcSearch, False)
        Me.IndigoGridControl1.SetHotTrack(Me.GdcSearch, False)
        Me.GdcSearch.Location = New System.Drawing.Point(24, 40)
        Me.GdcSearch.MainView = Me.GdvSearch
        Me.GdcSearch.Name = "GdcSearch"
        Me.GdcSearch.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.repIceLiquidationType, Me.repIceAdmissionType})
        Me.GdcSearch.Size = New System.Drawing.Size(960, 626)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.GdcSearch, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.IndigoGridControl1.SetSizeLayoutItem(Me.GdcSearch, New System.Drawing.Size(964, 0))
        Me.GdcSearch.TabIndex = 1
        Me.GdcSearch.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GdvSearch})
        '
        'GdvSearch
        '
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.GdvSearch.Appearance.FocusedRow.Options.UseBackColor = True
        Me.GdvSearch.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.GdvSearch.Appearance.GroupRow.Options.UseFont = True
        'Cadena reemplazada... True
        Me.GdvSearch.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.GdvSearch.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
        Me.GdvSearch.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GdvSearch.Appearance.Row.Options.UseFont = True
        Me.GdvSearch.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.GdvSearch.Appearance.ViewCaption.Options.UseFont = True
        Me.GdvSearch.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColAdmissionCode, Me.ColPatienCode, Me.ColPatienName, Me.ColAdmissionType, Me.ColLiquidationType, Me.ColBedStay, Me.ColResponsibleName, Me.ColStatusName})
        Me.GdvSearch.GridControl = Me.GdcSearch
        Me.GdvSearch.GroupCount = 1
        Me.GdvSearch.Name = "GdvSearch"
        Me.GdvSearch.OptionsBehavior.AutoExpandAllGroups = True
        Me.GdvSearch.OptionsDetail.ShowDetailTabs = False
        Me.GdvSearch.OptionsFind.AlwaysVisible = True
        Me.GdvSearch.OptionsFind.FindDelay = 1500
        Me.GdvSearch.OptionsFind.FindFilterColumns = "AdmissionCode"
        Me.GdvSearch.OptionsFind.FindNullPrompt = "Número de Ingreso..."
        Me.GdvSearch.OptionsFind.ShowClearButton = False
        Me.GdvSearch.OptionsFind.ShowCloseButton = False
        Me.GdvSearch.OptionsView.EnableAppearanceEvenRow = True
        Me.GdvSearch.OptionsView.EnableAppearanceOddRow = True
        Me.GdvSearch.OptionsView.ShowAutoFilterRow = True
        Me.GdvSearch.OptionsView.ShowDetailButtons = False
        Me.GdvSearch.OptionsView.ShowGroupPanel = False
        Me.GdvSearch.OptionsView.ShowIndicator = False
        Me.GdvSearch.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.ColStatusName, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GdvSearch, False)
        '
        'ColAdmissionCode
        '
        Me.ColAdmissionCode.AppearanceHeader.Options.UseTextOptions = True
        Me.ColAdmissionCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColAdmissionCode.Caption = "No. Ingreso"
        Me.ColAdmissionCode.FieldName = "AdmissionCode"
        Me.ColAdmissionCode.MinWidth = 100
        Me.ColAdmissionCode.Name = "ColAdmissionCode"
        Me.ColAdmissionCode.OptionsColumn.AllowEdit = False
        Me.ColAdmissionCode.OptionsColumn.AllowMove = False
        Me.ColAdmissionCode.OptionsFilter.AllowAutoFilter = False
        Me.ColAdmissionCode.Visible = True
        Me.ColAdmissionCode.VisibleIndex = 0
        Me.ColAdmissionCode.Width = 121
        '
        'ColPatienCode
        '
        Me.ColPatienCode.AppearanceHeader.Options.UseTextOptions = True
        Me.ColPatienCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColPatienCode.Caption = "Identificación"
        Me.ColPatienCode.FieldName = "PatientCode"
        Me.ColPatienCode.MinWidth = 120
        Me.ColPatienCode.Name = "ColPatienCode"
        Me.ColPatienCode.OptionsColumn.AllowEdit = False
        Me.ColPatienCode.OptionsColumn.AllowMove = False
        Me.ColPatienCode.OptionsFilter.AllowFilter = False
        Me.ColPatienCode.Visible = True
        Me.ColPatienCode.VisibleIndex = 1
        Me.ColPatienCode.Width = 120
        '
        'ColPatienName
        '
        Me.ColPatienName.AppearanceHeader.Options.UseTextOptions = True
        Me.ColPatienName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColPatienName.Caption = "Paciente"
        Me.ColPatienName.FieldName = "PatientName"
        Me.ColPatienName.MinWidth = 100
        Me.ColPatienName.Name = "ColPatienName"
        Me.ColPatienName.OptionsColumn.AllowEdit = False
        Me.ColPatienName.OptionsColumn.AllowMove = False
        Me.ColPatienName.OptionsFilter.AllowFilter = False
        Me.ColPatienName.Visible = True
        Me.ColPatienName.VisibleIndex = 2
        Me.ColPatienName.Width = 100
        '
        'ColAdmissionType
        '
        Me.ColAdmissionType.AppearanceHeader.Options.UseTextOptions = True
        Me.ColAdmissionType.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColAdmissionType.Caption = "T. Ingreso"
        Me.ColAdmissionType.ColumnEdit = Me.repIceAdmissionType
        Me.ColAdmissionType.FieldName = "AdmissionType"
        Me.ColAdmissionType.MinWidth = 100
        Me.ColAdmissionType.Name = "ColAdmissionType"
        Me.ColAdmissionType.OptionsColumn.AllowEdit = False
        Me.ColAdmissionType.OptionsColumn.AllowMove = False
        Me.ColAdmissionType.OptionsFilter.AllowAutoFilter = False
        Me.ColAdmissionType.OptionsFilter.AllowFilter = False
        Me.ColAdmissionType.Visible = True
        Me.ColAdmissionType.VisibleIndex = 3
        Me.ColAdmissionType.Width = 100
        '
        'repIceAdmissionType
        '
        Me.repIceAdmissionType.AutoHeight = False
        Me.repIceAdmissionType.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Ambulatorio", 1, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Hospitalario", 2, -1)})
        Me.repIceAdmissionType.Name = "repIceAdmissionType"
        Me.repIceAdmissionType.ReadOnly = True
        '
        'ColLiquidationType
        '
        Me.ColLiquidationType.AppearanceHeader.Options.UseTextOptions = True
        Me.ColLiquidationType.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColLiquidationType.Caption = "T. Liquidación"
        Me.ColLiquidationType.ColumnEdit = Me.repIceLiquidationType
        Me.ColLiquidationType.FieldName = "LiquidationType"
        Me.ColLiquidationType.MinWidth = 120
        Me.ColLiquidationType.Name = "ColLiquidationType"
        Me.ColLiquidationType.OptionsColumn.AllowEdit = False
        Me.ColLiquidationType.OptionsColumn.AllowMove = False
        Me.ColLiquidationType.OptionsFilter.AllowAutoFilter = False
        Me.ColLiquidationType.OptionsFilter.AllowFilter = False
        Me.ColLiquidationType.Visible = True
        Me.ColLiquidationType.VisibleIndex = 5
        Me.ColLiquidationType.Width = 120
        '
        'repIceLiquidationType
        '
        Me.repIceLiquidationType.AutoHeight = False
        Me.repIceLiquidationType.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Copago", 1, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Cuota Moderadora", 2, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("No Aplica", 3, -1)})
        Me.repIceLiquidationType.Name = "repIceLiquidationType"
        Me.repIceLiquidationType.ReadOnly = True
        '
        'ColBedStay
        '
        Me.ColBedStay.AppearanceHeader.Options.UseTextOptions = True
        Me.ColBedStay.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColBedStay.Caption = "Estancia (Cama)"
        Me.ColBedStay.FieldName = "BedStay"
        Me.ColBedStay.MinWidth = 120
        Me.ColBedStay.Name = "ColBedStay"
        Me.ColBedStay.OptionsColumn.AllowEdit = False
        Me.ColBedStay.OptionsColumn.AllowMove = False
        Me.ColBedStay.OptionsFilter.AllowFilter = False
        Me.ColBedStay.Visible = True
        Me.ColBedStay.VisibleIndex = 4
        Me.ColBedStay.Width = 120
        '
        'ColResponsibleName
        '
        Me.ColResponsibleName.AppearanceHeader.Options.UseTextOptions = True
        Me.ColResponsibleName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColResponsibleName.Caption = "Acudiente"
        Me.ColResponsibleName.FieldName = "ResponsibleName"
        Me.ColResponsibleName.MinWidth = 100
        Me.ColResponsibleName.Name = "ColResponsibleName"
        Me.ColResponsibleName.OptionsColumn.AllowEdit = False
        Me.ColResponsibleName.OptionsColumn.AllowMove = False
        Me.ColResponsibleName.OptionsFilter.AllowFilter = False
        Me.ColResponsibleName.Visible = True
        Me.ColResponsibleName.VisibleIndex = 6
        Me.ColResponsibleName.Width = 100
        '
        'ColStatusName
        '
        Me.ColStatusName.AppearanceHeader.Options.UseTextOptions = True
        Me.ColStatusName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColStatusName.Caption = "Estado"
        Me.ColStatusName.FieldName = "StatusName"
        Me.ColStatusName.MinWidth = 100
        Me.ColStatusName.Name = "ColStatusName"
        Me.ColStatusName.OptionsColumn.AllowEdit = False
        Me.ColStatusName.OptionsColumn.AllowMove = False
        Me.ColStatusName.OptionsFilter.AllowAutoFilter = False
        Me.ColStatusName.OptionsFilter.AllowFilter = False
        Me.ColStatusName.Visible = True
        Me.ColStatusName.VisibleIndex = 7
        Me.ColStatusName.Width = 100
        '
        'LycgSearch
        '
        Me.LycgSearch.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LycgSearch.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LycgSearch.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LycgSearch.AppearanceItemCaption.Options.UseFont = True
        Me.LycgSearch.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LycgSearch.AppearanceTabPage.Header.Options.UseFont = True
        Me.LycgSearch.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LycgSearch.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LycgSearch.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LycgSearch.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LycgSearch.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LycgSearch.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LycgSearch.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LycgSearch.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LycgSearch, False)
        Me.LycgSearch.CustomizationFormText = "LycgSearch"
        Me.LycgSearch.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LycgSearch.GroupBordersVisible = False
        Me.LycgSearch.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LycgSearchGroup})
        Me.LycgSearch.Location = New System.Drawing.Point(0, 0)
        Me.LycgSearch.Name = "LycgSearch"
        Me.LycgSearch.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 10, 0, 10)
        Me.LycgSearch.Size = New System.Drawing.Size(1008, 729)
        Me.LycgSearch.TextVisible = False
        '
        'LycgSearchGroup
        '
        Me.LycgSearchGroup.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LycgSearchGroup.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LycgSearchGroup.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LycgSearchGroup.AppearanceItemCaption.Options.UseFont = True
        Me.LycgSearchGroup.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LycgSearchGroup.AppearanceTabPage.Header.Options.UseFont = True
        Me.LycgSearchGroup.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LycgSearchGroup.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LycgSearchGroup.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LycgSearchGroup.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LycgSearchGroup.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LycgSearchGroup.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LycgSearchGroup.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LycgSearchGroup.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LycgSearchGroup, False)
        Me.LycgSearchGroup.CustomizationFormText = "LayoutControlGroup1"
        Me.LycgSearchGroup.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem3, Me.EmptySpaceItem2})
        Me.LycgSearchGroup.Location = New System.Drawing.Point(0, 0)
        Me.LycgSearchGroup.Name = "LycgSearchGroup"
        Me.LycgSearchGroup.Padding = New DevExpress.XtraLayout.Utils.Padding(9, 9, 0, 0)
        Me.LycgSearchGroup.Size = New System.Drawing.Size(988, 719)
        Me.LycgSearchGroup.Text = "Busqueda de Ingresos"
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.GdcSearch
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(104, 42)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 20)
        Me.LayoutControlItem1.Size = New System.Drawing.Size(964, 648)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.BtnOpen
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 648)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(120, 30)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(120, 30)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 10, 0, 0)
        Me.LayoutControlItem2.Size = New System.Drawing.Size(120, 30)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.BtnCancel
        Me.LayoutControlItem3.CustomizationFormText = "LayoutControlItem3"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(120, 648)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(120, 30)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(120, 30)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 0, 0, 0)
        Me.LayoutControlItem3.Size = New System.Drawing.Size(120, 30)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = False
        '
        'EmptySpaceItem2
        '
        Me.EmptySpaceItem2.AllowHotTrack = False
        Me.EmptySpaceItem2.CustomizationFormText = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Location = New System.Drawing.Point(240, 648)
        Me.EmptySpaceItem2.Name = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Size = New System.Drawing.Size(724, 30)
        Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
        '
        'FrmSearchAdmission
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 729)
        Me.Controls.Add(Me.LycSearch)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "FrmSearchAdmission"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.LycSearch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LycSearch.ResumeLayout(False)
        CType(Me.GdcSearch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GdvSearch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.repIceAdmissionType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.repIceLiquidationType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LycgSearch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LycgSearchGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LycSearch As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LycgSearch As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents GdcSearch As DevExpress.XtraGrid.GridControl
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents GdvSearch As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LycgSearchGroup As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents BtnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnOpen As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents ColAdmissionCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColPatienCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColPatienName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColAdmissionType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColBedStay As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColLiquidationType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColResponsibleName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColStatusName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents repIceLiquidationType As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents repIceAdmissionType As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
End Class
