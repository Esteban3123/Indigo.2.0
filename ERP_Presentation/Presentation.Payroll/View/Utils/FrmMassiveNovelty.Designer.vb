Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmMassiveNovelty  
    Inherits FormBase

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMassiveNovelty))
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDBtnImportFile = New DevExpress.XtraEditors.SimpleButton()
        Me.INDEsbBills = New DevExpress.XtraEditors.SimpleButton()
        Me.INDgcInformation = New DevExpress.XtraGrid.GridControl()
        Me.INDviewInfo = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColEmpleado = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColTipoNovedad = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColTipoIncapacidad = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFechaIncapacidad = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFechaFin = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColDias = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColDescripción = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColProrroga = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColTipoRiesgo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColCedulaEmpleado = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColTipoLiquidar = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColPostularValor = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.INDRepIcbeTipoNovedad = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.INDRepIcbeTipoIncapacidad = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        CType(Me.INDPanelControlBase,System.ComponentModel.ISupportInitialize).BeginInit
        Me.INDPanelControlBase.SuspendLayout
        CType(Me.ToolBars,System.ComponentModel.ISupportInitialize).BeginInit
        Me.ToolBars.SuspendLayout
        CType(Me.LayoutControls,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.CtrNavigationControlPanel1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControl1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.LayoutControl1.SuspendLayout
        CType(Me.INDgcInformation,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDviewInfo,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlGroup1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlGroup2,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem3,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem4,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoSimpleButton1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoGridControl1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoGroupControl1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoLabelControl1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoLayoutControlGroup1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoGridView1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDRepIcbeTipoNovedad,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDRepIcbeTipoIncapacidad,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.LayoutControl1)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = true
        '
        'BarraBotones
        '
        Me.BarraBotones.OperatingUnitVisible = true
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.LayoutControl1
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 570)
        Me.CtrNavigationControlPanel1.TabIndex = 0
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = false
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDBtnImportFile)
        Me.LayoutControl1.Controls.Add(Me.INDEsbBills)
        Me.LayoutControl1.Controls.Add(Me.INDgcInformation)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(202, 7)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(435, 231, 421, 552)
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(1261, 570)
        Me.LayoutControl1.TabIndex = 1
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDBtnImportFile
        '
        Me.INDBtnImportFile.ImageOptions.Image = CType(resources.GetObject("INDBtnImportFile.ImageOptions.Image"),System.Drawing.Image)
        Me.INDBtnImportFile.Location = New System.Drawing.Point(70, 59)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDBtnImportFile, false)
        Me.INDBtnImportFile.Name = "INDBtnImportFile"
        Me.INDBtnImportFile.Size = New System.Drawing.Size(42, 32)
        Me.INDBtnImportFile.StyleController = Me.LayoutControl1
        Me.INDBtnImportFile.TabIndex = 22
        Me.INDBtnImportFile.Text = "SimpleButton1"
        Me.INDBtnImportFile.ToolTip = "Importar Archivo"
        '
        'INDEsbBills
        '
        Me.INDEsbBills.ImageOptions.Image = CType(resources.GetObject("INDEsbBills.ImageOptions.Image"),System.Drawing.Image)
        Me.INDEsbBills.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.INDEsbBills.Location = New System.Drawing.Point(24, 59)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDEsbBills, false)
        Me.INDEsbBills.Name = "INDEsbBills"
        Me.INDEsbBills.Size = New System.Drawing.Size(42, 32)
        Me.INDEsbBills.StyleController = Me.LayoutControl1
        Me.INDEsbBills.TabIndex = 17
        Me.INDEsbBills.Text = "ExportStructureButton3"
        Me.INDEsbBills.ToolTip = "Exportar Estructura"
        '
        'INDgcInformation
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcInformation, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcInformation, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcInformation, false)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcInformation, true)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcInformation, false)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcInformation, false)
        Me.INDgcInformation.Location = New System.Drawing.Point(24, 95)
        Me.INDgcInformation.MainView = Me.INDviewInfo
        Me.INDgcInformation.Name = "INDgcInformation"
        Me.INDgcInformation.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDRepIcbeTipoNovedad, Me.INDRepIcbeTipoIncapacidad})
        Me.INDgcInformation.Size = New System.Drawing.Size(1213, 451)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcInformation, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcInformation.TabIndex = 4
        Me.INDgcInformation.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDviewInfo})
        '
        'INDviewInfo
        '
        Me.INDviewInfo.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewInfo.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewInfo.Appearance.FocusedRow.Options.UseBorderColor = true
        Me.INDviewInfo.Appearance.FocusedRow.Options.UseFont = true
        Me.INDviewInfo.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.INDviewInfo.Appearance.GroupRow.Options.UseFont = true
        Me.INDviewInfo.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.INDviewInfo.Appearance.HeaderPanel.Options.UseFont = true
        Me.INDviewInfo.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewInfo.Appearance.Row.Options.UseFont = true
        Me.INDviewInfo.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13!)
        Me.INDviewInfo.Appearance.ViewCaption.Options.UseFont = true
        Me.INDviewInfo.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColEmpleado, Me.ColTipoNovedad, Me.ColProrroga , Me.ColTipoIncapacidad, Me.ColTipoRiesgo , Me.ColFechaIncapacidad, Me.ColFechaFin, Me.ColDias, Me.ColDescripción, Me.ColCedulaEmpleado, Me.ColTipoLiquidar, Me.ColPostularValor})
        Me.INDviewInfo.GridControl = Me.INDgcInformation
        Me.INDviewInfo.Name = "INDviewInfo"
        Me.INDviewInfo.OptionsView.EnableAppearanceEvenRow = true
        Me.INDviewInfo.OptionsView.EnableAppearanceOddRow = true
        Me.INDviewInfo.OptionsView.ShowAutoFilterRow = true
        Me.INDviewInfo.OptionsView.ShowGroupPanel = false
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewInfo, false)
        '
        'ColEmpleado
        '
        Me.ColEmpleado.AppearanceCell.Options.UseTextOptions = true
        Me.ColEmpleado.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColEmpleado.Caption = "Empleado"
        Me.ColEmpleado.FieldName = "Employee"
        Me.ColEmpleado.Name = "ColEmpleado"
        Me.ColEmpleado.OptionsColumn.AllowEdit = false
        Me.ColEmpleado.OptionsColumn.AllowFocus = false
        Me.ColEmpleado.Visible = true
        Me.ColEmpleado.VisibleIndex = 0
        '
        'ColTipoNovedad
        '
        Me.ColTipoNovedad.Caption = "Tipo Novedad"
        Me.ColTipoNovedad.ColumnEdit = Me.INDRepIcbeTipoNovedad
        Me.ColTipoNovedad.FieldName = "TypeNovelty"
        Me.ColTipoNovedad.Name = "ColTipoNovedad"
        Me.ColTipoNovedad.OptionsColumn.AllowEdit = false
        Me.ColTipoNovedad.OptionsColumn.AllowFocus = false
        Me.ColTipoNovedad.Visible = true
        Me.ColTipoNovedad.VisibleIndex = 1
        '
        'ColTipoIncapacidad
        '
        Me.ColTipoIncapacidad.Caption = "Tipo Incapacidad"
        Me.ColTipoIncapacidad.ColumnEdit = Me.INDRepIcbeTipoIncapacidad
        Me.ColTipoIncapacidad.FieldName = "InabilityClass"
        Me.ColTipoIncapacidad.Name = "ColTipoIncapacidad"
        Me.ColTipoIncapacidad.OptionsColumn.AllowEdit = false
        Me.ColTipoIncapacidad.OptionsColumn.AllowFocus = false
        Me.ColTipoIncapacidad.Visible = true
        Me.ColTipoIncapacidad.VisibleIndex = 2
        '
        'ColFechaIncapacidad
        '
        Me.ColFechaIncapacidad.Caption = "Fecha Inicio"
        Me.ColFechaIncapacidad.FieldName = "RealDate"
        Me.ColFechaIncapacidad.Name = "ColFechaIncapacidad"
        Me.ColFechaIncapacidad.OptionsColumn.AllowEdit = false
        Me.ColFechaIncapacidad.OptionsColumn.AllowFocus = false
        Me.ColFechaIncapacidad.Visible = true
        Me.ColFechaIncapacidad.VisibleIndex = 3
        '
        'ColFechaFin
        '
        Me.ColFechaFin.Caption = "Fecha Fin"
        Me.ColFechaFin.FieldName = "EndDate"
        Me.ColFechaFin.Name = "ColFechaFin"
        Me.ColFechaFin.OptionsColumn.AllowEdit = false
        Me.ColFechaFin.OptionsColumn.AllowFocus = false
        Me.ColFechaFin.Visible = true
        Me.ColFechaFin.VisibleIndex = 4
        '
        'ColDias
        '
        Me.ColDias.Caption = "Dias"
        Me.ColDias.FieldName = "Days"
        Me.ColDias.Name = "ColDias"
        Me.ColDias.OptionsColumn.AllowEdit = false
        Me.ColDias.OptionsColumn.AllowFocus = false
        Me.ColDias.Visible = true
        Me.ColDias.VisibleIndex = 5
        '
        'ColDescripción
        '
        Me.ColDescripción.Caption = "Descripción"
        Me.ColDescripción.FieldName = "Reason"
        Me.ColDescripción.Name = "ColDescripción"
        Me.ColDescripción.OptionsColumn.AllowEdit = false
        Me.ColDescripción.OptionsColumn.AllowFocus = false
        Me.ColDescripción.Visible = true
        Me.ColDescripción.VisibleIndex = 6
        '
        'ColCedulaEmpleado
        '
        Me.ColCedulaEmpleado.Caption = "Cedula Empleado"
        Me.ColCedulaEmpleado.FieldName = "Nit"
        Me.ColCedulaEmpleado.Name = "ColCedulaEmpleado"
        Me.ColCedulaEmpleado.OptionsColumn.AllowEdit = false
        Me.ColCedulaEmpleado.OptionsColumn.AllowFocus = false
        Me.ColCedulaEmpleado.Visible = false

        'ColProrroga
        Me.ColProrroga.Caption = "Prórroga (0 - No, 1 - Si)"
        Me.ColProrroga.FieldName = "Extension"
        Me.ColProrroga.Name = "ColProrroga"
        Me.ColProrroga.OptionsColumn.AllowEdit = false
        Me.ColProrroga.OptionsColumn.AllowFocus = false
        Me.ColProrroga.Visible = false

        'ColTipoRiesgo
        Me.ColTipoRiesgo.Caption = "Tipo Riesgo"
        Me.ColTipoRiesgo.FieldName = "RiskType"
        Me.ColTipoRiesgo.Name = "ColTipoRiesgo"
        Me.ColTipoRiesgo.OptionsColumn.AllowEdit = false
        Me.ColTipoRiesgo.OptionsColumn.AllowFocus = false
        Me.ColTipoRiesgo.Visible = false

        'ColTipoLiquidar
        Me.ColTipoLiquidar.Caption = "Liquidar con: 1 - Sueldo, 2 - IBC Promedio Mes Anterior"
        Me.ColTipoLiquidar.FieldName = "TipoLiquidar"
        Me.ColTipoLiquidar.Name = "ColTipoLiquidar"
        Me.ColTipoLiquidar.OptionsColumn.AllowEdit = false
        Me.ColTipoLiquidar.OptionsColumn.AllowFocus = false
        Me.ColTipoLiquidar.Visible = false

        'ColPostularValor
        Me.ColPostularValor.Caption = "Postular Valor (1. 100% Todos los días, 2. 2/3 Todos los Días, 3. 2/3 a partir del 3er dia)"
        Me.ColPostularValor.FieldName = "PostularValor"
        Me.ColPostularValor.Name = "ColPostularValor"
        Me.ColPostularValor.OptionsColumn.AllowEdit = false
        Me.ColPostularValor.OptionsColumn.AllowFocus = false
        Me.ColPostularValor.Visible = false

        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = true
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = true
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Options.UseFont = true
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Options.UseFont = true
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Options.UseFont = true
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Options.UseFont = true
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Options.UseFont = true
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, false)
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = false
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup2})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1261, 570)
        Me.LayoutControlGroup1.TextVisible = false
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.LayoutControlGroup2.AppearanceGroup.Options.UseFont = true
        Me.LayoutControlGroup2.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.LayoutControlGroup2.AppearanceItemCaption.Options.UseFont = true
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Options.UseFont = true
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Options.UseFont = true
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Options.UseFont = true
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Options.UseFont = true
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Options.UseFont = true
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup2, false)
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem3, Me.LayoutControlItem4})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(1241, 550)
        Me.LayoutControlGroup2.Text = "Cargue Masivo de Novedades"
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDgcInformation
        Me.LayoutControlItem1.CustomizationFormText = "Listado de Empleados"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 36)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(1217, 455)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = false
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.INDEsbBills
        Me.LayoutControlItem3.CustomizationFormText = "Exportar Estructura"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(46, 36)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(46, 36)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(46, 36)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = false
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.INDBtnImportFile
        Me.LayoutControlItem4.CustomizationFormText = "Importar Información"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(46, 0)
        Me.LayoutControlItem4.MaxSize = New System.Drawing.Size(46, 36)
        Me.LayoutControlItem4.MinSize = New System.Drawing.Size(46, 36)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(1171, 36)
        Me.LayoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextVisible = false
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = true
        '
        'INDRepIcbeTipoNovedad
        '
        Me.INDRepIcbeTipoNovedad.AutoHeight = false
        Me.INDRepIcbeTipoNovedad.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDRepIcbeTipoNovedad.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Incapacidad", CType(1,String), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Sanción", CType(2,String), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Licencia", CType(3,String), -1)})
        Me.INDRepIcbeTipoNovedad.Name = "INDRepIcbeTipoNovedad"
        '
        'INDRepIcbeTipoIncapacidad
        '
        Me.INDRepIcbeTipoIncapacidad.AutoHeight = false
        Me.INDRepIcbeTipoIncapacidad.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDRepIcbeTipoIncapacidad.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Enfermedad general.", CType(1,String), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Licencia Meternidad/Paternidad.", CType(2,String), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Enfermedad profesional/ARL.", CType(3,String), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Licencia (Luto)", CType(4,String), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Calamidad doméstica.", CType(5,String), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Cargo a vacaciones", CType(6,String), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Permiso remunerado", CType(7,String), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("No remunerado.", CType(8,String), -1)})
        Me.INDRepIcbeTipoIncapacidad.Name = "INDRepIcbeTipoIncapacidad"
        '
        'FrmMassiveNovelty
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1465, 701)
        Me.Name = "FrmMassiveNovelty"
        Me.Opacity = 1R
        Me.Tag = "1971"
        Me.Text = "Cargue Masivo de Novedades"
        CType(Me.INDPanelControlBase,System.ComponentModel.ISupportInitialize).EndInit
        Me.INDPanelControlBase.ResumeLayout(false)
        CType(Me.ToolBars,System.ComponentModel.ISupportInitialize).EndInit
        Me.ToolBars.ResumeLayout(false)
        CType(Me.LayoutControls,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.CtrNavigationControlPanel1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControl1,System.ComponentModel.ISupportInitialize).EndInit
        Me.LayoutControl1.ResumeLayout(false)
        CType(Me.INDgcInformation,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDviewInfo,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup2,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem3,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem4,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoSimpleButton1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoGridControl1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoGroupControl1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoLabelControl1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoLayoutControlGroup1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoGridView1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDRepIcbeTipoNovedad,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDRepIcbeTipoIncapacidad,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub

    Friend WithEvents CtrNavigationControlPanel1 As CtrNavigationControlPanel
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDgcInformation As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDviewInfo As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDEsbBills As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDBtnImportFile As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoSimpleButton1 As IndigoSimpleButton
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents IndigoGroupControl1 As IndigoGroupControl
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents IndigoLabelControl1 As IndigoLabelControl
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents ColEmpleado As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColTipoNovedad As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColTipoIncapacidad As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFechaIncapacidad As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFechaFin As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColDias As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColProrroga As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColTipoRiesgo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColDescripción As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColCedulaEmpleado As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColTipoLiquidar As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColPostularValor As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRepIcbeTipoNovedad As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents INDRepIcbeTipoIncapacidad As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
End Class
